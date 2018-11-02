///////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////
//本窗体实现监视相机相片标注。
//在标注坐标轴时先有相片像素坐标（xc,yc）计算得到虚拟成像位置（xv,yv），进而可以得到监视相机坐标系中的位置（xi,yi）
//通过变换矩阵M321和Me可以求出该点在着陆器月面天东北坐标系的位置，M312为着陆器月面天东北坐标系到着陆器本体机械坐标系的
//变换矩阵，Me为着陆器本体坐标系到监视相机坐标系的坐标转换矩阵
///////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmJSCameraLabel : OfficeForm
    {
        IMapControl3 pMapControl;
        string CamImageFromFile;
        IRasterLayer pRasterImageLayer=new RasterLayerClass();
        Matrix M312 = new Matrix(3);//由着陆器月面天东北坐标系到着陆器本体机械坐标系变换矩阵
        Matrix ME = new Matrix(3);//有着陆器本体机械坐标系到监视相机坐标系坐标转换矩阵

        double[] XCoordLander = new double[4];// { 2.2, 2.2, 2.2, 2.2 };
        double[] YCoordLander = new double[4];// { 1.553, -1.899, 1.553, -1.899 };
        double[] ZCoordLander = new double[4];// { 0, 0, -3.452, -3.452 };
        double[] XCoordLanderE = new double[4];// { 1.7, 1.7, 1.7, 1.7 };
        double[] YCoordLanderE = new double[4];// { 1.553, -1.899, 1.553, -1.899 };
        double[] ZCoordLanderE = new double[4];// { 0, 0, -3.452, -3.452 };

        double[] LanderXYZmax = new double[3];// { 1.8, 3.3, 0 };//太阳翼帆板右前点在太阳照射下的的坐标位置
        double[] LanderXYZmaxE = new double[3];// { 1.3, 3.3, 0 };//太阳翼帆板右前点在地球通信下的的坐标位置
        double[] LanderXYZmin = new double[3];// { 1.8, 1.553, 0 };//太阳翼帆板右前内侧点在太阳照射下的的坐标位置
        double[] LanderXYZminE = new double[3];// { 1.3, 1.553, 0 };//太阳翼帆板右前内侧点在地球通信下的的坐标位置


        IRawBlocks pRawBlocks;
        IRasterInfo pRasterInfo;

        //px,py为相机在x,y方向的像素个数
        int px ;
        int py ;

        [DllImport("MatrixDLL.dll", EntryPoint = "mlCalcMonitorPicXY", CallingConvention = CallingConvention.Cdecl)]
        static extern bool mlCalcMonitorPicXY( double dAngle1, double dAngle2, double dAngle3, double dPitchAngle, double dCamX, double dCamY, double dCamZ, double dPlaneGround,
                                    double dFocal, double dObjX, double dObjY, double dObjZ, out double pPicX, out double pPicY);
        [DllImport("MatrixDLL.dll", EntryPoint = "mlCalcObjXY", CallingConvention = CallingConvention.Cdecl)]
        static extern bool mlCalcObjXY( double  dAngle1, double dAngle2, double dAngle3, double dPitchAngle, double dCamX, double dCamY, double dCamZ, double dPlaneGround,
									double dFocal, double dPicX, double dPicY ,  out double pObjX, out double pObjY, out double pObjZ);

        public FrmJSCameraLabel(IMapControl3 mapcontrol)
        {
            InitializeComponent();
            pMapControl = mapcontrol;
        }

        private void FrmJSCameraLabel_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pMapControl.LayerCount;i++ )
            {
                if (pMapControl.get_Layer(i) is IRasterLayer)
                {
                    cmbCamImage.Items.Add(pMapControl.get_Layer(i).Name);
                }
            }
        }

        private void BtnOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            OpenDialog.Title = "选择监视相机图像";
            OpenDialog.Filter = "bmp|*.bmp|jpg|*.jpg|tif|*.tif|All Files|*.*";
            OpenDialog.RestoreDirectory=true;
            if (OpenDialog.ShowDialog()==DialogResult.OK)
            {
                cmbCamImage.Text = OpenDialog.FileName;
                CamImageFromFile = OpenDialog.FileName;
                pRasterImageLayer.CreateFromFilePath(OpenDialog.FileName);
            }
        }

        private void cmbCamImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < pMapControl.LayerCount; i++)
            {
                if (pMapControl.get_Layer(i).Name==cmbCamImage.Text)
                {
                    pRasterImageLayer = pMapControl.get_Layer(i) as IRasterLayer;
                }
            }
        }

        private void btnSaveLabel_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDialog = new SaveFileDialog();
            SaveDialog.Title = "存储标注图层";
            SaveDialog.Filter = "shp|*.shp";
            SaveDialog.RestoreDirectory = true;
            if (SaveDialog.ShowDialog()==DialogResult.OK)
            {
                txtSaveLabel.Text = SaveDialog.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            double ori1 = Deg2Rad(Convert.ToDouble(txtOri1.Text));
            double ori2 = Deg2Rad(Convert.ToDouble(txtOri2.Text));
            double ori3 = Deg2Rad(Convert.ToDouble(txtOri3.Text));

            //M312.SetNum(0, 0, 1 - ori1 * ori2 * ori3);
            //M312.SetNum(0, 1, ori3 + ori1 * ori2);
            //M312.SetNum(0, 2, -ori2);
            //M312.SetNum(1, 0, -ori3);
            //M312.SetNum(1, 1, 1);
            //M312.SetNum(1, 2, ori1);
            //M312.SetNum(2, 0, ori2 + ori1 * ori3);
            //M312.SetNum(2, 1, ori2 * ori3 - ori1);
            //M312.SetNum(2, 2, 1);

            M312.SetNum(0, 0, Math.Cos(ori2)*Math.Cos(ori3) - Math.Sin(ori1)*Math.Sin(ori2) * Math.Sin(ori3));
            M312.SetNum(0, 1, Math.Cos(ori2)*Math.Sin(ori3) +Math.Sin(ori2)*Math.Sin(ori1) * Math.Cos(ori3));
            M312.SetNum(0, 2, -Math.Sin(ori2)*Math.Cos(ori1));
            M312.SetNum(1, 0, -Math.Cos(ori1)*Math.Sin(ori3));
            M312.SetNum(1, 1, Math.Cos(ori1)*Math.Cos(ori3));
            M312.SetNum(1, 2, Math.Sin(ori1));
            M312.SetNum(2, 0, Math.Sin(ori2) * Math.Cos(ori3) + Math.Cos(ori2)*Math.Sin(ori1)*Math.Sin(ori3));
            M312.SetNum(2, 1, Math.Sin(ori2) * Math.Sin(ori3) - Math.Cos(ori2) * Math.Sin(ori1) * Math.Cos(ori3));
            M312.SetNum(2, 2, Math.Cos(ori1) * Math.Cos(ori2));

            ME.SetNum(0, 0, Math.Cos(Deg2Rad(-15)));
            ME.SetNum(0, 1, 0);
            ME.SetNum(0, 2, -Math.Sin(Deg2Rad(-15)));
            ME.SetNum(1, 0, 0);
            ME.SetNum(1, 1, 1);
            ME.SetNum(1, 2, 0);
            ME.SetNum(2, 0, Math.Sin(Deg2Rad(-15)));
            ME.SetNum(2, 1, 0);
            ME.SetNum(2, 2, Math.Cos(Deg2Rad(-15)));

            ///////////////////////////////////////////////////////
            //着陆器的最大包络体（没考虑巡视器）四角点坐标为右前（2.626,1.726,1.850）,左前(2.626,-1.726,1.850)
            //右后（2.626,1.726,-1.850）,左后(2.626,-1.726,-1.850)，（x,y,z）坐标为天-东-北坐标系
            
            //读取参数
            readFile();
            
            List<double> xImageList = new List<double>();//记录转换结果
            List<double> yImageList = new List<double>();

            //首先创建shp图层
            IFeatureLayer pFeatureLayer = creatSHPfile(txtSaveLabel.Text);//创建图层,地面遮挡涂层
            IFeatureLayer pFeatureLayerLabelLow = creatSHPfile(System.IO.Path.GetDirectoryName(txtSaveLabel.Text) + "\\" + System.IO.Path.GetFileNameWithoutExtension(txtSaveLabel.Text) + "_LowLabel.shp");//地面下边缘标注
            IFeatureLayer pFeatureLayerLabelLeft = creatSHPfile(System.IO.Path.GetDirectoryName(txtSaveLabel.Text) + "\\" + System.IO.Path.GetFileNameWithoutExtension(txtSaveLabel.Text) + "_LeftLabel.shp");//地面左边缘标注
            IFeatureLayer pFeatureLayerSun = creatSHPfile(System.IO.Path.GetDirectoryName(txtSaveLabel.Text) +"\\"+ System.IO.Path.GetFileNameWithoutExtension(txtSaveLabel.Text)+"_Sun.shp");//太阳板为地平面图层
            IFeatureLayer pFeatureLayerEar = creatSHPfile(System.IO.Path.GetDirectoryName(txtSaveLabel.Text) + "\\" + System.IO.Path.GetFileNameWithoutExtension(txtSaveLabel.Text) + "_Ear.shp");//天线为地平面图层
            IFeatureLayer pFeatureLayerLander = creatSHPPolygonfile(System.IO.Path.GetDirectoryName(txtSaveLabel.Text) + "\\" + System.IO.Path.GetFileNameWithoutExtension(txtSaveLabel.Text) + "_Polygon.shp");//天线为地平面图层

            List<double> zCoordear = new List<double>();//记录左面Z值刻度，用于进行不同平面线平移
            List<double> zCoordsun = new List<double>();
            List<double> zCoordGround = new List<double>();
            //计算着陆器坐标系坐标并绘制标注
            //Image2LanderUNE(pFeatureLayer, pRasterImageLayer,null,3);//计算地面,地面图层不添加标注
            Image2LanderUNE(pFeatureLayerLabelLow, pRasterImageLayer, zCoordGround, 3);//下边缘标注
            Image2LanderUNE(pFeatureLayerLabelLeft, pRasterImageLayer, null, 4);//左边缘标注
            Image2LanderUNE(pFeatureLayerSun, pRasterImageLayer, zCoordsun,1);
            Image2LanderUNE(pFeatureLayerEar, pRasterImageLayer,zCoordear, 2);

            //计算右前左前两点对应的相片像素,计算太阳遮挡
            for (int i=0;i<=1;i++)
            {
                LanderUEN2Image(pRasterImageLayer, XCoordLander[i], YCoordLander[i], ZCoordLander[i], Convert.ToDouble(txtExpAngleSun.Text), Convert.ToDouble(txtYawAngleSun.Text),xImageList,yImageList,1);
            }
            //计算右前左前两点对应的相片像素,计算地球
            for (int i = 0; i <= 1; i++)
            {
                LanderUEN2Image(pRasterImageLayer, XCoordLanderE[i], YCoordLanderE[i], ZCoordLander[i], Convert.ToDouble(txtExpAngleEarth.Text), Convert.ToDouble(txtYawAngleEarth.Text), xImageList, yImageList, 2);
            }
            //计算太阳翼帆板在太阳照射下的相片像素
            LanderUEN2Image(pRasterImageLayer, LanderXYZmax[0], LanderXYZmax[1], LanderXYZmax[2], Convert.ToDouble(txtExpAngleSun.Text), Convert.ToDouble(txtYawAngleSun.Text), xImageList, yImageList, 1);
            LanderUEN2Image(pRasterImageLayer, LanderXYZmin[0], LanderXYZmin[1], LanderXYZmin[2], Convert.ToDouble(txtExpAngleSun.Text), Convert.ToDouble(txtYawAngleSun.Text), xImageList, yImageList, 1);
            
            //计算太阳翼帆板在地球通信下的相片像素
            LanderUEN2Image(pRasterImageLayer, LanderXYZmaxE[0], LanderXYZmaxE[1], LanderXYZmaxE[2], Convert.ToDouble(txtExpAngleEarth.Text), Convert.ToDouble(txtYawAngleEarth.Text), xImageList, yImageList, 2);
            LanderUEN2Image(pRasterImageLayer, LanderXYZminE[0], LanderXYZminE[1], LanderXYZminE[2], Convert.ToDouble(txtExpAngleEarth.Text), Convert.ToDouble(txtYawAngleEarth.Text), xImageList, yImageList, 2);


            CreatSunShell(pFeatureLayerSun, pRasterImageLayer, xImageList, yImageList);
            creatEarthShell(pFeatureLayerEar, pRasterImageLayer, xImageList, yImageList);

            //计算巡视器行驶路径点的坐标,并绘制形式路径
            RoverPathPoint(pRasterImageLayer, pFeatureLayer, xImageList, yImageList);
            //平移线
            MoveShellLine(pFeatureLayerSun, pFeatureLayer, pRasterImageLayer, zCoordsun, zCoordGround, "太阳阴影");
            MoveShellLine(pFeatureLayerEar, pFeatureLayer, pRasterImageLayer, zCoordear, zCoordGround, "通信遮挡包络");
            //绘制巡视器
            CreatLanderPoly(pRasterImageLayer, pFeatureLayerLander, xImageList, yImageList);

            pMapControl.AddLayer(pFeatureLayer as ILayer);//.AddShapeFile(System.IO.Path.GetDirectoryName(txtSaveLabel.Text), System.IO.Path.GetFileName(txtSaveLabel.Text));
            pMapControl.AddLayer(pFeatureLayerLabelLeft as ILayer);
            pMapControl.AddLayer(pFeatureLayerLabelLow as ILayer);
            pMapControl.AddLayer(pFeatureLayerSun as ILayer);//.AddShapeFile(System.IO.Path.GetDirectoryName(txtSaveLabel.Text), System.IO.Path.GetFileName(txtSaveLabel.Text));
            pMapControl.AddLayer(pFeatureLayerEar as ILayer);//.AddShapeFile(System.IO.Path.GetDirectoryName(txtSaveLabel.Text), System.IO.Path.GetFileName(txtSaveLabel.Text));
            pMapControl.AddLayer(pFeatureLayerLander as ILayer);
            this.Close();
        }

        private void readFile()
        {
            string filepath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Temp\JSCamera.txt";
            try
            {
                //string strFileName = dlg.FileName;
                StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default);
                string strTemp = "";
                while (sr.Peek() >= 0)
                {
                    strTemp = sr.ReadLine();
                    string[] strArray = strTemp.Split(' ');
                    if (strArray[0] == "XCoordLander")
                    {
                        for (int i = 1; i < strArray.Length; i++)
                        {
                            XCoordLander[i-1] = double.Parse(strArray[i]);
                        }
                    }
                    else if (strArray[0] == "YCoordLander")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            YCoordLander[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                    else if (strArray[0] == "ZCoordLander")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ZCoordLander[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                    else if (strArray[0] == "XCoordLanderE")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            XCoordLanderE[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                    else if (strArray[0] == "YCoordLanderE")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            YCoordLanderE[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                    else if (strArray[0] == "ZCoordLanderE")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ZCoordLanderE[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                    else if (strArray[0] == "LanderXYZmax")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LanderXYZmax[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                    else if (strArray[0] == "LanderXYZmaxE")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LanderXYZmaxE[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                    else if (strArray[0] == "LanderXYZmin")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LanderXYZmin[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                    else if (strArray[0] == "LanderXYZminE")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            LanderXYZminE[i] = double.Parse(strArray[i + 1]);
                        }
                    }
                }
                sr.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public IFeatureLayer creatSHPfile(string FileFloder)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(FileFloder);
                string filefolder = di.Parent.FullName;

                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                //设置字段   
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;

                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                pGeoDefEdit.HasM_2 = false;
                pGeoDefEdit.HasZ_2 = false;
                //设置空间参考
                ISpatialReference pSpatialRef = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。
                pSpatialRef.SetDomain(-8000000, 8000000, -8000000, 8000000);//没有这句就抛异常来自HRESULT：0x8004120E。
                pSpatialRef.SetZDomain(-8000000, 8000000);
                pGeoDefEdit.SpatialReference_2 = pSpatialRef;
                pFieldEdit.Name_2 = "SHAPE";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = true;
                pFieldsEdit.AddField(pField);

                //新建字段用于标注
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "CoordLabel"; //原始图层名称
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                ClsGDBDataCommon comm = new ClsGDBDataCommon();
                IWorkspace inmemWor = comm.OpenFromShapefile(filefolder);
                IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
                IFeatureClass pFeatureClass = pFeatureWorkspace.CreateFeatureClass(di.Name, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");

                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pFeatureClass;
                pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(di.Name);

                return pFeatureLayer;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

                return null;
            }
        }

         public IFeatureLayer creatSHPPolygonfile(string FileFloder)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(FileFloder);
                string filefolder = di.Parent.FullName;

                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                //设置字段   
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;

                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                pGeoDefEdit.HasM_2 = false;
                pGeoDefEdit.HasZ_2 = false;
                //设置空间参考
                ISpatialReference pSpatialRef = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。
                pSpatialRef.SetDomain(-8000000, 8000000, -8000000, 8000000);//没有这句就抛异常来自HRESULT：0x8004120E。
                pSpatialRef.SetZDomain(-8000000, 8000000);
                pGeoDefEdit.SpatialReference_2 = pSpatialRef;
                pFieldEdit.Name_2 = "SHAPE";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = true;
                pFieldsEdit.AddField(pField);

                //新建字段用于标注
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "CoordLabel"; //原始图层名称
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                ClsGDBDataCommon comm = new ClsGDBDataCommon();
                IWorkspace inmemWor = comm.OpenFromShapefile(filefolder);
                IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
                IFeatureClass pFeatureClass = pFeatureWorkspace.CreateFeatureClass(di.Name, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");

                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pFeatureClass;
                pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(di.Name);

                return pFeatureLayer;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

                return null;
            }
        }

        private void Image2LanderUNE(IFeatureLayer pFLayer, IRasterLayer pRLayer,List<double>zcoord,int Mode)
        {
                double x = 0.0;//着陆器月面天东北坐标系位置
                double y = 0.0;
                double z = 0.0;
                px = pRLayer.RowCount;
                py = pRLayer.ColumnCount;

                //记录左边缘和下边缘的个像素代表的值
                List<double> lowCoord = new List<double>();
                List<double> leftCoord = new List<double>();

                double surface = 0;//地面高程
                switch (Mode)
                {
                    case 1:
                        surface = -0.994;
                        break;
                    case 2:
                        surface = -0.494;
                        break;
                    case 3:
                    case 4:
                        surface = -1.42;
                        break;
                }
                //下面计算图像左边缘和下边缘任意像元像素坐标(xc,yc)对应的着陆器月面天东北坐标系位置(x,y)
                //像素坐标系以相片中心点为坐标原点，x向上，y水平方向
                //标注的坐标系为着陆器月面天东北坐标系，因此标注时水平向右为y轴，水平向上为z轴，x在相片无法标示
                for (int i = -px / 2; i < px / 2; i++)//计算左边缘
                {
                    if (false == mlCalcObjXY(Convert.ToDouble(txtOri1.Text), Convert.ToDouble(txtOri2.Text), Convert.ToDouble(txtOri3.Text), -15, 0, 0.173, 0, surface, 886.784,
                                Convert.ToDouble(-py / 2), Convert.ToDouble(i), out x, out y, out z))
                    {
                        leftCoord.Add(-9999);
                    }
                    else
                    {
                        leftCoord.Add(z);
                        if (zcoord!=null)
                        {
                            zcoord.Add(z);//用于进行不同平面线平移
                        }
                    }

                }

                for (int j = -py / 2; j < py / 2; j++)//计算下边缘
                {
                    if (false == mlCalcObjXY(Convert.ToDouble(txtOri1.Text), Convert.ToDouble(txtOri2.Text), Convert.ToDouble(txtOri3.Text), -15, 0, 0.173, 0, surface, 886.784,
                                 Convert.ToDouble(j), Convert.ToDouble(-px / 2), out x, out y, out z))
                    {
                        lowCoord.Add(-9999);
                    }
                    else
                    {
                        lowCoord.Add(y);
                    }
                }
                for (int p = 0; p < leftCoord.Count; p++)
                {
                    if (leftCoord[p] > 14.5)//计算过程中，达到一定的值之后会出现负值，趋向于无穷远，删去
                    {
                        for (int m = p; m < leftCoord.Count; m++)
                        {
                            leftCoord[m] = -100;
                        }
                        break;
                    }
                }
                switch (Mode)
                {
                    case 1:
                        createLabel(pFLayer, pRLayer, lowCoord, leftCoord);
                        break;
                    case 2:
                        createLabel(pFLayer, pRLayer, lowCoord, leftCoord);
                        break;
                    case 3://将刻度标注分为两个图层，便于标注
                        createLabel(pFLayer, pRLayer, lowCoord, null);
                        break;
                    case 4:
                        createLabel(pFLayer, pRLayer, null, leftCoord);
                        break;
                }
        }

        public double Deg2Rad(double d)  //!<角度转弧度
        {
            return d / 180.0 * Math.PI;
        }

        public double Rad2Deg(double r)
        {
            return r * 180 / Math.PI;
        }

        /// <summary>
        /// 用于计算图像像元像素(xc,yc)对应的着陆器月面天东北坐标系位置(x,y)
        /// </summary>
        /// <param name="xv"></param>
        /// <param name="yv"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CoordCalculate(double xc, double yc, ref double x, ref double y, ref double z)
        {
            //先计算计算图像像元像素(xc,yc)对应的虚拟成像位置（xv，yv）
            double xv = 2 * 1.42 * Math.Tan(Deg2Rad(30)) * (xc / px) / Math.Sin(Deg2Rad(15));
            double yv = 2 * 1.42 * Math.Tan(Deg2Rad(30)) * (yc / py) / Math.Sin(Deg2Rad(15));
            //下面计算实际成像位置在件事相机坐标系中的位置（xi,yi）
            double a = Rad2Deg(Math.Atan(xv * Math.Sin(Deg2Rad(15)) / 1.42));
            double b = Rad2Deg(Math.Atan(yv * Math.Sin(Deg2Rad(15)) / 1.42));
            //double xi = 1.42 * Math.Sin(Deg2Rad(a)) / Math.Sin(Deg2Rad(15 + Math.Abs(a)));
            //double yi = 1.42 * Math.Sin(Deg2Rad(b)) / Math.Sin(Deg2Rad(15 + Math.Abs(b)));
            double xi = 0;
            double yi = 0;
            if (a<10)
            {
                xi = 1.42 * Math.Sin(Deg2Rad(a)) / Math.Sin(Deg2Rad(15 - a));
            }

            //yi = 1.42 * Math.Sin(Deg2Rad(b)) / Math.Sin(Deg2Rad(15 - b));
            yi = 1.42 * Math.Tan(Deg2Rad(b)) / Math.Sin(Deg2Rad(15 - a));
                     
            //最后可以计算得到该点在着陆器月面天东北坐标系位置
            Matrix m=new Matrix();
            Matrix Mmultp;
            Matrix Mxiyi = new Matrix(3,1);
            Matrix Mxy ;
            Mxiyi.SetNum(0, 0, xi);
            Mxiyi.SetNum(1, 0, yi);

            if (a<10)//当a等于15时相机已经于地面平行，点趋向于无穷远
            {
                 Mxiyi.SetNum(2, 0, Math.Cos(Deg2Rad(Math.Abs(a))) * 1.42 / Math.Sin(Deg2Rad(15 -a)));
            }
           

            Mmultp = m.Mutiply(M312.Transpose(), ME.Transpose());
            Mxy = m.Mutiply(Mmultp, Mxiyi);
            x = Mxy.getNum(0, 0);
            y = Mxy.getNum(1, 0);
            z = Mxy.getNum(2, 0);  
        }

        private void LanderUEN2Image(IRasterLayer pRLayer, double x, double y, double z, double dExpAngel, double dYawAngle,
                                     List<double> xImageResult, List<double> yImageResult ,int Mode)
        {
            double xc=0;
            double yc=0;
            double xHeight = 0;//高度
            double XImage, YImage, ZImage;//计算出的着陆器天东北坐标系下坐标
            double XSurface = 0;
            double YSurface = 0;
            double ZSurface = 0;//地面投影点

            Matrix m = new Matrix();
            Matrix Mxyz = new Matrix(3, 1);
            Mxyz.SetNum(0, 0, x);
            Mxyz.SetNum(1, 0, y);
            Mxyz.SetNum(2, 0, z);
            Matrix Mmultp = m.Mutiply(M312.Transpose(), Mxyz);
            XImage = Mmultp.getNum(0, 0);
            YImage = Mmultp.getNum(1, 0);
            ZImage = Mmultp.getNum(2, 0);

            if (x > 0)//要计算出点对应的实际高度（x）
            {
                xHeight = x + 1.42;
            }
            else
            {
                xHeight = 1.42 - Math.Abs(x);
            }
            switch (Mode)
            {
                case 1://计算太阳遮挡包络,认为地面高度是巡视器电池板的高度为426CM
                    XSurface = -0.994;
                    xHeight = xHeight - 0.426;
                    break;
                case 2://计算地球通信包络,认为地面高度是巡视器天线的高度为926CM
                    XSurface = -0.494;
                    xHeight = xHeight - 0.926;
                    break;
                case 3://计算车辙
                    //XSurface = -0.878;//地面高度为-1.42m
                    XSurface = -1.42;
                    break;
            }


            //首先计算该点投射到对应的地面上的点着陆器坐标系下的坐标
            if (dExpAngel < 90 || dExpAngel >= 270)//当太阳或地球方位位于第一第四象限，则不会存在太阳和地球阴影
            {

            }
            else if (dExpAngel >= 90 && dExpAngel <= 180)
            {
                double l = Math.Abs(xHeight) / Math.Tan(Deg2Rad(dYawAngle));
                double aEXP = 180 - dExpAngel;
                ZSurface = z + l * Math.Cos(Deg2Rad(aEXP));
                YSurface = y - l * Math.Sin(Deg2Rad(aEXP));

            }
            else if (dExpAngel > 180 && dExpAngel <= 270)
            {
                double l = Math.Abs(xHeight) / Math.Tan(Deg2Rad(dYawAngle));
                double aEXP = dExpAngel - 180;
                ZSurface = z + l * Math.Cos(Deg2Rad(aEXP));
                YSurface = y + l * Math.Sin(Deg2Rad(aEXP));
            }

            switch(Mode)
            {
                case 1://地面假设为太阳帆板

                    mlCalcMonitorPicXY(Convert.ToDouble(txtOri1.Text), Convert.ToDouble(txtOri2.Text), Convert.ToDouble(txtOri3.Text), -15, 0, 0.173, 0, -0.994, 886.784,
                            XSurface, YSurface, ZSurface, out xc, out yc);
                    break;
                case 2:
                    mlCalcMonitorPicXY(Convert.ToDouble(txtOri1.Text), Convert.ToDouble(txtOri2.Text), Convert.ToDouble(txtOri3.Text), -15, 0, 0.173, 0, -0.494, 886.784,
                            XSurface, YSurface, ZSurface, out xc, out yc);
                    break;
                case 3:
                    mlCalcMonitorPicXY(Convert.ToDouble(txtOri1.Text), Convert.ToDouble(txtOri2.Text), Convert.ToDouble(txtOri3.Text), -15, 0, 0.173, 0, -1.42, 886.784,
                            x, y, z, out xc, out yc);
                    break;
            }



            //计算出该点的像元坐标转换成ArcGIS系统的屏幕坐标系用于后面绘制遮挡阴影
            xc = xc + 512;
            yc = yc - 512;
            xImageResult.Add(xc);
            yImageResult.Add(yc);
           
        }



        private void CreatSunShell(IFeatureLayer pFLayer, IRasterLayer pRLayer, List<double> xImageResult, List<double> yImageResult)
        {
            pRawBlocks = pRLayer.Raster as IRawBlocks;
            pRasterInfo = pRawBlocks.RasterInfo;

            IPoint SunFromPoint = new PointClass();//着陆器点对应的相片坐标
            IPoint SunToPoint = new PointClass();

            SunFromPoint.X = xImageResult[0];//右前点
            SunFromPoint.Y = yImageResult[0];
            SunToPoint.X = xImageResult[1];//左前点
            SunToPoint.Y = yImageResult[1];
            IPoint SunXYZMaxPoint = new PointClass();//太阳翼帆板的相片坐标
            IPoint sunXYZMinPoint = new PointClass();
            IPoint EarthXYZMaxPoint = new PointClass();
            IPoint EarthXYZMinPoint = new PointClass();
            SunXYZMaxPoint.X = xImageResult[4];//帆板外侧
            SunXYZMaxPoint.Y = yImageResult[4];
            sunXYZMinPoint.X = xImageResult[5];//帆板内侧
            sunXYZMinPoint.Y = yImageResult[5];


            //计算出包络的最长延长线
            double ksun = 0;//斜率
            double psun = 0;//截值
            double kearth = 0;//斜率
            double pearth = 0;//截值
            ksun = (SunToPoint.Y - SunFromPoint.Y) / (SunToPoint.X - SunFromPoint.X);
            psun = (SunFromPoint.Y * SunToPoint.X - SunToPoint.Y * SunFromPoint.X) / (SunToPoint.X - SunFromPoint.X);

            IPoint sunLeftPoint = new PointClass();
            IPoint sunRightPoint = new PointClass();

            sunLeftPoint.X = 0;
            sunLeftPoint.Y = psun;
            sunRightPoint.X = 1024;
            sunRightPoint.Y = 1024 * ksun + psun;

            if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
            {
                SunFromPoint.X = SunFromPoint.X * pRasterInfo.CellSize.X;
                SunFromPoint.Y = SunFromPoint.Y * pRasterInfo.CellSize.Y;
                SunToPoint.X = SunToPoint.X * pRasterInfo.CellSize.X;
                SunToPoint.Y = SunToPoint.Y * pRasterInfo.CellSize.Y;

                sunLeftPoint.X = sunLeftPoint.X * pRasterInfo.CellSize.X;
                sunLeftPoint.Y = sunLeftPoint.Y * pRasterInfo.CellSize.Y;
                sunRightPoint.X = sunRightPoint.X * pRasterInfo.CellSize.X;
                sunRightPoint.Y = sunRightPoint.Y * pRasterInfo.CellSize.Y;
            }
            //为了让画的线都在相片范围内

            if (SunFromPoint.X > 0)//着陆器右前点在相片范围内
            {
                if (sunXYZMinPoint.X < 1024 && sunXYZMinPoint.X > 0)//太阳翼帆板左前点在相片范围内
                {
                    if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                    {
                        sunXYZMinPoint.X = sunXYZMinPoint.X * pRasterInfo.CellSize.X;
                        sunXYZMinPoint.Y = sunXYZMinPoint.Y * pRasterInfo.CellSize.Y;
                    }
                    DrawLine(pFLayer, Convert.ToString("太阳阴影"), SunFromPoint, sunXYZMinPoint);
                    if (SunXYZMaxPoint.X < 1023)//太阳翼帆板右前点在相片范围内
                    {
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            SunXYZMaxPoint.X = SunXYZMaxPoint.X * pRasterInfo.CellSize.X;
                            SunXYZMaxPoint.Y = SunXYZMaxPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString("太阳阴影"), sunXYZMinPoint, SunXYZMaxPoint);
                    }
                    else
                    {
                        IPoint pointRight = new PointClass();
                        pointRight.X = 1023 * pRasterInfo.CellSize.X;
                        pointRight.Y = SunXYZMaxPoint.Y * pRasterInfo.CellSize.X;
                        DrawLine(pFLayer, Convert.ToString("太阳阴影"), sunXYZMinPoint, pointRight);
                    }
                }
                else if (sunXYZMinPoint.X >= 1024)
                {
                    double ksunMin = 0;//斜率
                    double psunMin = 0;//截值
                    ksunMin = (sunXYZMinPoint.Y - SunFromPoint.Y) / (sunXYZMinPoint.X - SunFromPoint.X);
                    psunMin = (SunFromPoint.Y * sunXYZMinPoint.X - sunXYZMinPoint.Y * SunFromPoint.X) / (sunXYZMinPoint.X - SunFromPoint.X);
                    IPoint sunPointMin = new PointClass();
                    sunPointMin.X = 1023 * pRasterInfo.CellSize.X;
                    sunPointMin.Y = (1023 * ksunMin + psunMin) * pRasterInfo.CellSize.X;
                    DrawLine(pFLayer, Convert.ToString("太阳阴影"), SunFromPoint, sunPointMin);
                }
                else
                {
                    ;
                }
                if (SunToPoint.X > 0)//着陆器左前点在相片范围内
                {
                    DrawLine(pFLayer, Convert.ToString("太阳阴影"), SunFromPoint, SunToPoint);
                }
                else
                {
                    DrawLine(pFLayer, Convert.ToString("太阳阴影"), SunFromPoint, sunLeftPoint);
                }
            }
            else//着陆器右前点在相片范围外
            {
                if (sunXYZMinPoint.X < 1024 && sunXYZMinPoint.X > 0)//太阳翼帆板左前点在相片范围内
                {
                    double ksunMin = 0;//斜率
                    double psunMin = 0;//截值
                    ksunMin = (sunXYZMinPoint.Y - SunFromPoint.Y) / (sunXYZMinPoint.X - SunFromPoint.X);
                    psunMin = (SunFromPoint.Y * sunXYZMinPoint.X - sunXYZMinPoint.Y * SunFromPoint.X) / (sunXYZMinPoint.X - SunFromPoint.X);
                    IPoint sunPointMin = new PointClass();
                    sunPointMin.X = 0;
                    sunPointMin.Y = psunMin * pRasterInfo.CellSize.X;
                    DrawLine(pFLayer, Convert.ToString("太阳阴影"), sunXYZMinPoint, sunPointMin);
                    if (SunXYZMaxPoint.X < 1024)//太阳翼帆板右前点在相片范围内
                    {
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            SunXYZMaxPoint.X = SunXYZMaxPoint.X * pRasterInfo.CellSize.X;
                            SunXYZMaxPoint.Y = SunXYZMaxPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString("太阳阴影"), sunXYZMinPoint, SunXYZMaxPoint);
                    }
                    else
                    {
                        IPoint pointRight = new PointClass();
                        pointRight.X = 1023 * pRasterInfo.CellSize.X;
                        pointRight.Y = SunXYZMaxPoint.Y * pRasterInfo.CellSize.X;
                        DrawLine(pFLayer, Convert.ToString("太阳阴影"), sunXYZMinPoint, pointRight);
                    }
                }
                else//太阳翼帆板左前点在相片范围外
                {
                    double ksunMin = 0;//斜率
                    double psunMin = 0;//截值
                    ksunMin = (sunXYZMinPoint.Y - SunXYZMaxPoint.Y) / (sunXYZMinPoint.X - SunXYZMaxPoint.X);
                    psunMin = (SunXYZMaxPoint.Y * sunXYZMinPoint.X - sunXYZMinPoint.Y * SunXYZMaxPoint.X) / (sunXYZMinPoint.X - SunXYZMaxPoint.X);
                    IPoint sunPointMin = new PointClass();
                    IPoint sunPointMax = new PointClass();
                    sunPointMin.X = 0;
                    sunPointMin.Y = psunMin * pRasterInfo.CellSize.X;
                    //sunPointMax.X = 1023 * pRasterInfo.CellSize.X; ;
                    //sunPointMax.Y = (1023 * ksunMin + psunMin) * pRasterInfo.CellSize.X;
                    DrawLine(pFLayer, Convert.ToString("太阳阴影"), sunPointMin, SunXYZMaxPoint);
                }
            }
        }

        private void creatEarthShell(IFeatureLayer pFLayer, IRasterLayer pRLayer, List<double> xImageResult, List<double> yImageResult)
        {
            pRawBlocks = pRLayer.Raster as IRawBlocks;
            pRasterInfo = pRawBlocks.RasterInfo;

            IPoint EarthFromPoint = new PointClass();//着陆器点对应的相片坐标
            IPoint EarthToPoint = new PointClass();
            EarthFromPoint.X = xImageResult[2];
            EarthFromPoint.Y = yImageResult[2];
            EarthToPoint.X = xImageResult[3];
            EarthToPoint.Y = yImageResult[3];

            IPoint EarthXYZMaxPoint = new PointClass();//太阳翼帆板的相片坐标
            IPoint EarthXYZMinPoint = new PointClass();
            EarthXYZMaxPoint.X = xImageResult[6];//帆板外侧
            EarthXYZMaxPoint.Y = yImageResult[6];
            EarthXYZMinPoint.X = xImageResult[7];//帆板内侧
            EarthXYZMinPoint.Y = yImageResult[7];

            //计算出两条通信包络的最长延长线
            double ksun = 0;//斜率
            double psun = 0;//截值
            double kearth = 0;//斜率
            double pearth = 0;//截值
            kearth = (EarthToPoint.Y - EarthFromPoint.Y) / (EarthToPoint.X - EarthFromPoint.X);
            pearth = (EarthFromPoint.Y * EarthToPoint.X - EarthToPoint.Y * EarthFromPoint.X) / (EarthToPoint.X - EarthFromPoint.X);

            IPoint earthLeftPoint = new PointClass();
            IPoint earthRightPoint = new PointClass();

            earthLeftPoint.X = 0;
            earthLeftPoint.Y = pearth;
            earthRightPoint.X = 1024;
            earthRightPoint.Y = 1024 * kearth + pearth;

            if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
            {
                EarthFromPoint.X = EarthFromPoint.X * pRasterInfo.CellSize.X;
                EarthFromPoint.Y = EarthFromPoint.Y * pRasterInfo.CellSize.Y;
                EarthToPoint.X = EarthToPoint.X * pRasterInfo.CellSize.X;
                EarthToPoint.Y = EarthToPoint.Y * pRasterInfo.CellSize.Y;

                earthLeftPoint.X = earthLeftPoint.X * pRasterInfo.CellSize.X;
                earthLeftPoint.Y = earthLeftPoint.Y * pRasterInfo.CellSize.Y;
                earthRightPoint.X = earthRightPoint.X * pRasterInfo.CellSize.X;
                earthRightPoint.Y = earthRightPoint.Y * pRasterInfo.CellSize.Y;
            }

            if (EarthFromPoint.X > 0)//着陆器右前点在相片范围内
            {
                if (EarthXYZMinPoint.X < 1024 && EarthXYZMinPoint.X > 0)//太阳翼帆板左前点在相片范围内
                {
                    if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                    {
                        EarthXYZMinPoint.X = EarthXYZMinPoint.X * pRasterInfo.CellSize.X;
                        EarthXYZMinPoint.Y = EarthXYZMinPoint.Y * pRasterInfo.CellSize.Y;
                    }
                    DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthFromPoint, EarthXYZMinPoint);
                    if (EarthXYZMaxPoint.X < 1023)//太阳翼帆板右前点在相片范围内
                    {
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            EarthXYZMaxPoint.X = EarthXYZMaxPoint.X * pRasterInfo.CellSize.X;
                            EarthXYZMaxPoint.Y = EarthXYZMaxPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthXYZMinPoint, EarthXYZMaxPoint);
                    }
                    else
                    {
                        IPoint pointRight = new PointClass();
                        pointRight.X = 1023 * pRasterInfo.CellSize.X;
                        pointRight.Y = EarthXYZMaxPoint.Y * pRasterInfo.CellSize.X;
                        DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthXYZMinPoint, pointRight);
                    }
                }
                else if (EarthXYZMinPoint.X >= 1024)
                {
                    
                    double ksunMin = 0;//斜率
                    double psunMin = 0;//截值
                    ksunMin = (EarthXYZMinPoint.Y - EarthFromPoint.Y) / (EarthXYZMinPoint.X - EarthFromPoint.X);
                    psunMin = (EarthFromPoint.Y * EarthXYZMinPoint.X - EarthXYZMinPoint.Y * EarthFromPoint.X) / (EarthXYZMinPoint.X - EarthFromPoint.X);
                    IPoint earPointMin = new PointClass();
                    earPointMin.X = 1023 * pRasterInfo.CellSize.X;
                    earPointMin.Y = (1023 * ksunMin + psunMin) * pRasterInfo.CellSize.X;
                    DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthFromPoint, earPointMin);
                }
                else
                {
                    ;
                }
                if (EarthToPoint.X > 0)//着陆器左前点在相片范围内
                {
                    DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthFromPoint, EarthToPoint);
                }
                else
                {
                    DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthFromPoint, earthLeftPoint);
                }
            }
            else//着陆器右前点在相片范围外
            {
                if (EarthXYZMinPoint.X < 1024 && EarthXYZMinPoint.X > 0)//太阳翼帆板左前点在相片范围内
                {
                    double ksunMin = 0;//斜率
                    double psunMin = 0;//截值
                    ksunMin = (EarthXYZMinPoint.Y - EarthFromPoint.Y) / (EarthXYZMinPoint.X - EarthFromPoint.X);
                    psunMin = (EarthFromPoint.Y * EarthXYZMinPoint.X - EarthXYZMinPoint.Y * EarthFromPoint.X) / (EarthXYZMinPoint.X - EarthFromPoint.X);
                    IPoint earPointMin = new PointClass();
                    earPointMin.X = 0;
                    earPointMin.Y = psunMin * pRasterInfo.CellSize.X;
                    DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthFromPoint, earPointMin);
                    if (EarthXYZMaxPoint.X < 1024)//太阳翼帆板右前点在相片范围内
                    {
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            EarthXYZMaxPoint.X = EarthXYZMaxPoint.X * pRasterInfo.CellSize.X;
                            EarthXYZMaxPoint.Y = EarthXYZMaxPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthXYZMinPoint, EarthXYZMaxPoint);
                    }
                    else
                    {
                        IPoint pointRight = new PointClass();
                        pointRight.X = 1023 * pRasterInfo.CellSize.X;
                        pointRight.Y = EarthXYZMaxPoint.Y * pRasterInfo.CellSize.X;
                        DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), EarthXYZMinPoint, pointRight);
                    }
                }
                else//太阳翼帆板左前点在相片范围外
                {
                    double ksunMin = 0;//斜率
                    double psunMin = 0;//截值
                    ksunMin = (EarthXYZMinPoint.Y - EarthFromPoint.Y) / (EarthXYZMinPoint.X - EarthFromPoint.X);
                    psunMin = (EarthFromPoint.Y * EarthXYZMinPoint.X - EarthXYZMinPoint.Y * EarthFromPoint.X) / (EarthXYZMinPoint.X - EarthFromPoint.X);
                    IPoint earPointMin = new PointClass();
                    IPoint earPointMax = new PointClass();
                    earPointMin.X = 0;
                    earPointMin.Y = psunMin * pRasterInfo.CellSize.X;
                    earPointMax.X = 1023 * pRasterInfo.CellSize.X; ;
                    earPointMax.Y = (1023 * ksunMin + psunMin) * pRasterInfo.CellSize.X;
                    DrawLine(pFLayer, Convert.ToString("通信遮挡包络"), earPointMin, earPointMax);
                }
            }
        }

        private void createLabel(IFeatureLayer pFLayer, IRasterLayer pRLayer, List<double> LowCoord, List<double> LeftCoord)
        {
            //for (int i = 0; i < LowCoord.Count;i++ )
            //{
            //    LowCoord[i] += 0.173;
            //}

            IPoint FromPoint = new PointClass();
            IPoint ToPoint = new PointClass();

            pRawBlocks = pRLayer.Raster as IRawBlocks;
            pRasterInfo = pRawBlocks.RasterInfo;

            for (int i = 1; i < 1024; i++)
            {
                if (LeftCoord != null)
                {
                    if (LeftCoord[i] >= 1.5 && LeftCoord[i - 1] < 1.5)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 40;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(1.5), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 2.0 && LeftCoord[i - 1] < 2.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 40;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(2.0), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 3.0 && LeftCoord[i - 1] < 3.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 40;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(3.0), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 4.0 && LeftCoord[i - 1] < 4.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 40;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(4.0), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 5.0 && LeftCoord[i - 1] < 5.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 50;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(5.0), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 6.0 && LeftCoord[i - 1] < 6.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 60;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(6.0), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 7.0 && LeftCoord[i - 1] < 7.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 70;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(7.0), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 8.0 && LeftCoord[i - 1] < 8.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 80;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(8.0), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 10.0 && LeftCoord[i - 1] < 10.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 100;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(10.0), FromPoint, ToPoint);
                    }
                    if (LeftCoord[i] >= 12.0 && LeftCoord[i - 1] < 12.0)
                    {
                        FromPoint.X = 0;
                        FromPoint.Y = -1 * (1024 - i);
                        ToPoint.X = 120;
                        ToPoint.Y = -1 * (1024 - i);
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(12.0), FromPoint, ToPoint);
                    }
                }

                if (LowCoord != null)
                {

                    if (LowCoord[i] >= -1.2 && LowCoord[i - 1] < -1.2)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1000;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(-1.2), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= -1.0 && LowCoord[i - 1] < -1.0)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1008;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(-1.0), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= -0.8 && LowCoord[i - 1] < -0.8)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1000;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(-0.8), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= -0.6 && LowCoord[i - 1] < -0.6)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1008;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(-0.6), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= -0.4 && LowCoord[i - 1] < -0.4)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1000;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(-0.4), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= -0.2 && LowCoord[i - 1] < -0.2)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1008;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(-0.2), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 0 && LowCoord[i - 1] < 0)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -992;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(0), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 0.2 && LowCoord[i - 1] < 0.2)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1008;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(0.2), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 0.4 && LowCoord[i - 1] < 0.4)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1000;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(0.4), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 0.6 && LowCoord[i - 1] < 0.6)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1008;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(0.6), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 0.8 && LowCoord[i - 1] < 0.8)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1000;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(0.8), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 1.0 && LowCoord[i - 1] < 1.0)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1008;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(1.0), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 1.2 && LowCoord[i - 1] < 1.2)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1000;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(1.2), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 1.4 && LowCoord[i - 1] < 1.4)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1008;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(1.4), FromPoint, ToPoint);
                    }
                    if (LowCoord[i] >= 1.6 && LowCoord[i - 1] < 1.6)
                    {
                        FromPoint.X = i;
                        FromPoint.Y = -1023;
                        ToPoint.X = i;
                        ToPoint.Y = -1000;
                        if (System.IO.Path.GetFileName(cmbCamImage.Text).Contains(".bmp"))
                        {
                            FromPoint.X = FromPoint.X * pRasterInfo.CellSize.X;
                            FromPoint.Y = FromPoint.Y * pRasterInfo.CellSize.Y;
                            ToPoint.X = ToPoint.X * pRasterInfo.CellSize.X;
                            ToPoint.Y = ToPoint.Y * pRasterInfo.CellSize.Y;
                        }
                        DrawLine(pFLayer, Convert.ToString(1.6), FromPoint, ToPoint);
                    }
                }
            }

        }

        private void RoverPathPoint(IRasterLayer pRLayer,IFeatureLayer pFlayer,List<double> xPointResult, List<double> yPointResult)
        {   
            List<double[]>PointList=new List<double[]>();
            double[] PointLeft1 = new double[3] {-1.42, -0.425, 1.5};//1.5米左轮坐标。下同
            PointList.Add(PointLeft1);
            double[] PointLeft3 = new double[3] { -1.42, -0.425, 12 };
            PointList.Add(PointLeft3);
            double[] PointRight1 = new double[3] { -1.42, 0.425, 1.5 };
            PointList.Add(PointRight1);
            double[] PointRight3 = new double[3] { -1.42, 0.425, 12 };
            PointList.Add(PointRight3);
            for (int i=0;i<PointList.Count;i++)
            {
                //计算车辙的相片像素位置
                LanderUEN2Image(pRLayer, PointList[i][0], PointList[i][1], PointList[i][2], Convert.ToDouble(txtExpAngleEarth.Text), Convert.ToDouble(txtYawAngleEarth.Text), xPointResult, yPointResult,3);
            }

            List<IPoint> PathPointList = new List<IPoint>();
            for (int i = 0; i < 4;i++ )
            {
                IPoint pathPoint = new PointClass();
                pathPoint.X = xPointResult[8 + i];
                pathPoint.Y = yPointResult[8 + i];
                PathPointList.Add(pathPoint);
            }

                DrawLine(pFlayer, "行驶路径", PathPointList[0], PathPointList[1]);
                DrawLine(pFlayer, "行驶路径", PathPointList[2], PathPointList[3]);
        }

        private void CreatLanderPoly(IRasterLayer pRLayer, IFeatureLayer pFlayer, List<double> xPointResult, List<double> yPointResult)
        {
            List<double[]> PointList = new List<double[]>();
            double[] PointLeft1 = new double[3] { -1.42, -0.425, 0.890 };//1.5米左轮坐标。下同
            PointList.Add(PointLeft1);
            double[] PointLeft3 = new double[3] { -1.42, -0.425, 2.39 };
            PointList.Add(PointLeft3);
            double[] PointRight1 = new double[3] { -1.42, 0.425, 2.39 };
            PointList.Add(PointRight1);
            double[] PointRight3 = new double[3] { -1.42, 0.425, 0.890 };
            PointList.Add(PointRight3);
            for (int i = 0; i < PointList.Count; i++)
            {
                //计算车辙的相片像素位置
                LanderUEN2Image(pRLayer, PointList[i][0], PointList[i][1], PointList[i][2], Convert.ToDouble(txtExpAngleEarth.Text), Convert.ToDouble(txtYawAngleEarth.Text), xPointResult, yPointResult, 3);
            }
            List<IPoint> PathPointList = new List<IPoint>();
            for (int i = 0; i < 4; i++)
            {
                IPoint pathPoint = new PointClass();
                pathPoint.X = xPointResult[12 + i];
                pathPoint.Y = yPointResult[12 + i];
                PathPointList.Add(pathPoint);
            }
            DrawPolygon(pFlayer, "巡视器", PathPointList);
        }

        private void DrawLine(IFeatureLayer pFLayer,string strLabel,IPoint FromPoint,IPoint ToPoint)
        {
            IFeatureClass pFeatureClass = pFLayer.FeatureClass;
            ILine pL = new LineClass();
            pL.FromPoint = FromPoint;
            pL.ToPoint = ToPoint;
            ISegment pSeg = pL as ISegment;
            ISegmentCollection pSC = new PolylineClass();
            pSC.AddSegment(pSeg);
            IFeature pFTemp = pFeatureClass.CreateFeature();
            pFTemp.Shape = pSC as IGeometry;

            pFTemp.set_Value(pFTemp.Fields.FindField("CoordLabel"), strLabel);
            pFTemp.Store();
        }

         private void DrawPolygon(IFeatureLayer pFLayer,string strLabel,List<IPoint>PointList)
        {
            IFeatureClass pFeatureClass = pFLayer.FeatureClass;
            IPolygon  pPolygon = new PolygonClass();
            IPointCollection pPC = pPolygon as IPointCollection;
            for (int i = 0; i < PointList.Count; i++)
            {
                pPC.AddPoint(PointList[i]);
             }
            pPolygon.Close();
            IFeature pFeatureTemp = pFeatureClass.CreateFeature();
            pFeatureTemp.Shape = pPolygon as IGeometry;
            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("CoordLabel"), strLabel);
            pFeatureTemp.Store();


            //IFeatureClass pFeatureClass = pFLayer.FeatureClass;
            //ISegmentCollection pSCollection = new RingClass();
            //for (int i = 1; i < PointList.Count; i++)
            //{
            //    ISegment pSegment = new LineClass();
            //    pSegment.ToPoint.X = PointList[i].X;
            //    pSegment.ToPoint.Y = PointList[i].Y;
            //    pSegment.FromPoint.X = PointList[i - 1].X;
            //    pSegment.FromPoint.Y = PointList[i - 1].Y;
            //    pSCollection.AddSegment(pSegment);
            //}
            //ISegment pSegmentLast = new LineClass();
            //pSegmentLast.ToPoint.X = PointList[0].X;
            //pSegmentLast.ToPoint.Y = PointList[0].Y;
            //pSegmentLast.FromPoint.X = PointList[3].X;
            //pSegmentLast.FromPoint.Y = PointList[3].Y;
            //pSCollection.AddSegment(pSegmentLast);

            //IGeometryCollection pGCollectionNew = new PolygonClass();
            //IRing pRing = pSCollection as IRing;
            //pRing.Close();
            //object ob = Type.Missing;
            //pGCollectionNew.AddGeometry(pRing as IGeometry, ref ob, ref ob);

            //IFeature pFeatureTemp = pFeatureClass.CreateFeature();
            //pFeatureTemp.Shape = pGCollectionNew as IGeometry;
            //pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("CoordLabel"), strLabel);
            //pFeatureTemp.Store();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MoveShellLine(IFeatureLayer pFLayer,IFeatureLayer pFLayerDes, IRasterLayer pRlayer,List<double> zCoordHeight, List<double> zCoordGround,string strLabel)
        {
            //首先挑出图层中所有线的最左边的点，借助该点进行线的整体平移
            pRawBlocks = pRlayer.Raster as IRawBlocks;
            pRasterInfo = pRawBlocks.RasterInfo;
            double xMin = 1500 * pRasterInfo.CellSize.X;
            double yMin = 1500 * pRasterInfo.CellSize.X;//记录最左边点的xy值

            List<IPolyline> LineSoc = new List<IPolyline>();
            List<IPolyline> LineDes = new List<IPolyline>();

            IFeatureCursor pFCursor = pFLayer.Search(null, false);
            IFeature pFeature = pFCursor.NextFeature();
            ITable pTable = pFLayerDes as ITable;
            IFields pFields = pTable.Fields;
            int nFieldIndex = pFields.FindField("CoordLabel");//获取标示类型的字段

            
            while (pFeature!=null)
            {
                if (pFeature.get_Value(nFieldIndex).ToString() == "太阳阴影" || pFeature.get_Value(nFieldIndex).ToString() == "通信遮挡包络")
                {

                    IGeometry pGeometry = pFeature.Shape;

                  
                    if (pGeometry != null && pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        IPolyline pPolyLine = pGeometry as IPolyline;
                        //double YTempF = pPolyLine.FromPoint.Y;
                        //double YTempT = pPolyLine.ToPoint.Y;
                        //if (YTempF <= yMin)
                        //{
                        //    yMin = YTempF;
                        //    xMin = pPolyLine.FromPoint.X;
                        //}
                        //if (YTempT <= yMin)
                        //{
                        //    yMin = YTempT;
                        //    xMin = pPolyLine.ToPoint.X;
                        //}
                        double XTempF = pPolyLine.FromPoint.X;
                        double XTempT = pPolyLine.ToPoint.X;
                        if (XTempF <= xMin)
                        {
                            xMin = XTempF;
                            yMin = pPolyLine.FromPoint.Y;
                        }
                        if (XTempT <= xMin)
                        {
                            xMin = XTempT;
                            yMin = pPolyLine.ToPoint.Y;
                        }
                        LineSoc.Add(pPolyLine);
                    }
                   
                }
                pFeature = pFCursor.NextFeature();
            }

            //下面遍历zCoordHeight中的值得到（xMin,Ymin）点对应的在高处平面上的距离
            double zHeight = 0;
            double MoveFrom = 0;//记录移动前的起始位置
            yMin = 1024 - Math.Abs(yMin / pRasterInfo.CellSize.X);
            for (int i = 1; i < zCoordHeight.Count;i++ )
            {
                if (i >= yMin  && (i - 1) < yMin )
                {
                    zHeight = zCoordHeight[i];
                    MoveFrom = i;
                }
            }
            //遍历zCoordGround中的值得到zHeight
            double MoveTo = 0;//记录要移动到位置
            for (int i = 1; i < zCoordGround.Count;i++ )
            {
                if (zCoordGround[i] >= zHeight && zCoordGround[i - 1] < zHeight)
                {
                    MoveTo = i;
                }
            }

            double MoveDistance = -Math.Abs((MoveTo - MoveFrom) * pRasterInfo.CellSize.X);

            for (int i=0;i<LineSoc.Count;i++)
            {
                IPolyline lineDesMember = ConstructOffset(LineSoc[i],MoveDistance);
                LineDes.Add(lineDesMember);
            }

            //绘制线
            for (int i = 0; i < LineDes.Count;i++ )
            {
                IPoint FormPoint = new PointClass();
                IPoint ToPoint = new PointClass();
                FormPoint = LineDes[i].FromPoint;
                ToPoint = LineDes[i].ToPoint;
                DrawLine(pFLayerDes, strLabel, FormPoint, ToPoint);
            }
        }

        //平移点
        private IPolyline ConstructOffset(IPolyline inPolyline, double offset)
        {
            IPolyline pPolyline = new PolylineClass();
            if (inPolyline == null || inPolyline.IsEmpty)
            {
                return null;
            }
            IPoint FromPoint = new PointClass();
            IPoint ToPoiint = new PointClass();
            FromPoint.Y = inPolyline.FromPoint.Y + offset;
            ToPoiint.Y = inPolyline.ToPoint.Y + offset;
            FromPoint.X = inPolyline.FromPoint.X;
            ToPoiint.X = inPolyline.ToPoint.X;
            pPolyline.FromPoint = FromPoint;
            pPolyline.ToPoint = ToPoiint;
            return pPolyline ;
        }
    }
}
