using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;

using LibModelGen;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmCamOriInWorld : Form
    {
        public double dPosX;        //巡视器位置X
        public double dPosY;        //巡视器位置Y
        public double dPosZ;        //巡视器位置Z
        public double dOriOmg;      //巡视器姿态omg
        public double dOriPhi;      //巡视器姿态phi
        public double dOriKap;      //巡视器姿态Kap
        public double dExpAngle;    //旋转臂展开角
        //public double dYawAngle;    //旋转臂偏航角
        public List<double> dYawAngle = new List<double>();
        public double dPitchAngle;  //旋转臂俯仰角
        public string CamFilePath;  //配置文件路径
        public string datasetName;  //数据集名称 
        public IRaster pRasterDem;  //环拍DEM
        public IFeatureDataset pFeatureDataset;
        public IFeatureClass pFeatureClass ;
        AxMapControl pMapControl;
        IsNumberic isNum = new IsNumberic();
        OpenFileDialog ODialog = new OpenFileDialog();

        public double[] zgVecValue = new double[3]{-1,-1,-1};

        public bool isResultOK=false;

        //zg库定义委托
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void VecCallBack(double value1, double value2, double value3);
        VecCallBack zg_m_call;
        [DllImport("LibZmsgDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Register_CallBack_Vec([MarshalAs(UnmanagedType.FunctionPtr)]VecCallBack call);

        [DllImport("LibZmsgDLL.dll", EntryPoint = "zg_vec", CallingConvention = CallingConvention.Cdecl)]
        static extern int zg_vec(int uiTaskCode, int uiObjCode, int vecindex);
        void OutLouVec(double Value1, double Value2, double Value3)
        {
            zgVecValue[0] = Value1;
            zgVecValue[1] = Value2;
            zgVecValue[2] = Value3;
        }


        public FrmCamOriInWorld(AxMapControl mapcontrol)
        {
            InitializeComponent();
            pMapControl = mapcontrol;
        }

        private void BtnWorkBrowse_Click(object sender, EventArgs e)
        {
            ODialog.Title = "选择相机参数配置文件";
            ODialog.Filter = "文本文件(*.txt)|*.txt";
            ODialog.RestoreDirectory = true;
            if (ODialog.ShowDialog()==DialogResult.OK)
            {
                txtCamFilePath.Text = ODialog.FileName;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtCamFilePath.Text != null && txtPosX.Text != null && txtPosY != null && txtPosZ != null && txtOriOmg != null
                && txtOriPhi != null && txtOriKap != null && txtExpAngle != null &&  txtPitchAngle != null
                &&txtBeginYaw != null && txtLastYaw != null && txtPerYaw != null)
            {
                if (isNum.IsNumber(txtPosX.Text) && isNum.IsNumber(txtPosY.Text) && isNum.IsNumber(txtPosZ.Text)
                    && isNum.IsNumber(txtOriOmg.Text) && isNum.IsNumber(txtOriPhi.Text) && isNum.IsNumber(txtOriKap.Text)
                    && isNum.IsNumber(txtExpAngle.Text)  && isNum.IsNumber(txtPitchAngle.Text)
                    && isNum.IsNumber(txtBeginYaw.Text) && isNum.IsNumber(txtLastYaw.Text) && isNum.IsNumber(txtPerYaw.Text))
                {
                    dPosX = Convert.ToDouble(txtPosX.Text);
                    dPosY = Convert.ToDouble(txtPosY.Text);
                    dPosZ = Convert.ToDouble(txtPosZ.Text);
                    dOriOmg = Convert.ToDouble(txtOriOmg.Text);
                    dOriPhi = Convert.ToDouble(txtOriPhi.Text);
                    dOriKap = Convert.ToDouble(txtOriKap.Text);
                    dExpAngle = Convert.ToDouble(txtExpAngle.Text);
                    //dYawAngle = Convert.ToDouble(txtYawAngle.Text);
                    dPitchAngle = Convert.ToDouble(txtPitchAngle.Text);
                    //datasetName = txtdataset.Text;
                    CamFilePath = ODialog.FileName;
                    int YawNum = (int)(Math.Abs(Convert.ToDouble(txtLastYaw.Text.ToString()) - Convert.ToDouble(txtBeginYaw.Text.ToString())) / Convert.ToDouble(txtPerYaw.Text.ToString()));
                    if (Convert.ToDouble(txtLastYaw.Text.ToString()) >= Convert.ToDouble(txtBeginYaw.Text.ToString()))//初始偏航角小于终止偏航角
                    {
                        for (int i = 0; i <= YawNum; i++)
                        {
                            dYawAngle.Add(Convert.ToDouble(txtBeginYaw.Text.ToString()) + Convert.ToDouble(txtPerYaw.Text.ToString()) * i);
                        }
                    }
                    isResultOK = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("相机参数值必须为数字,请检查并重新输入", "输入有误", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("参数值不能为空", "提示", MessageBoxButtons.OK);
            }
        }

        private void FrmCamOriInWorld_Load(object sender, EventArgs e)
        {
            //初始化图层列表
            IEnumLayer pEnumLayer = pMapControl.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = null;
            while ((pLayer = pEnumLayer.Next()) != null)
            {
                if (pLayer is IRasterLayer)
                {
                    cmbDEMlayer.Items.Add(pLayer.Name);
                }
            }
            //初始化zg全局段回调函数
            zg_m_call = new VecCallBack(OutLouVec);
            Register_CallBack_Vec(zg_m_call);
        }

        private void cmbDEMlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //初始化图层列表
            IEnumLayer pEnumLayer = pMapControl.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = null;
            while ((pLayer = pEnumLayer.Next()) != null)
            {
                if (pLayer is IRasterLayer)
                {
                    if (pLayer.Name == cmbDEMlayer.SelectedItem.ToString())
                    {
                        IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                        pRasterDem = pRasterLayer.Raster;
                    }
                }
            }
        }

        private void btnPolygonDataset_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Title = "新建File Geodatabase";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(fbd.FileName + ".gdb") == true)
                    {
                        MessageBox.Show("GDB已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    //IWorkspaceName workspaceName = workspaceFactory.Create(System.IO.Path.GetDirectoryName(fbd.FileName), System.IO.Path.GetFileName(fbd.FileName), null, 0);
                    ClsGDBDataCommon com = new ClsGDBDataCommon();
                    com.Create_FileGDB(System.IO.Path.GetDirectoryName(fbd.FileName), System.IO.Path.GetFileName(fbd.FileName)+".gdb");
                    IWorkspace pws = com.OpenFromFileGDB(fbd.FileName + ".gdb");
                    pFeatureDataset = creatdataset(pws, System.IO.Path.GetFileName(fbd.FileName), pMapControl.SpatialReference);
                    creatfeatureclass(System.IO.Path.GetFileName(fbd.FileName));
                    txtPolygonDataset.Text = fbd.FileName + ".gdb";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        public IFeatureDataset creatdataset(IWorkspace workspace, string fdsName, ISpatialReference fdsSR)
        {
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            return featureWorkspace.CreateFeatureDataset(fdsName, fdsSR);
        }

        public void creatfeatureclass(string strLayerName)
        {
            //建立shape字段
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            //设置Geometry definition
            IGeometryDef pGeometryDef = new GeometryDefClass();
            IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon; //点、线、面
            pGeometryDefEdit.SpatialReference_2 = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
            //pGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
            pFieldEdit.GeometryDef_2 = pGeometryDef;
            pFieldsEdit.AddField(pField);
            //新建字段
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "PointID";             //点位PointID
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "X"; //X
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Y";             //Y
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Z";             //Z
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "omg";             //omg
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "phi";             //phi
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "kap";             //kap
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "width";             //width
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "height";             //height
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "fx";             //fx
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "fy";             //fy
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "f";             //f
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.AddField(pField);

            try
            {
                pFeatureClass = pFeatureDataset.CreateFeatureClass(strLayerName, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
            }
            catch (SystemException eee)
            {
                if (eee.Message == "The table already exists.")
                {
                    ClsGDBDataCommon cls = new ClsGDBDataCommon();
                    IWorkspace ws = pFeatureDataset.Workspace;
                    IFeatureWorkspace fws = ws as IFeatureWorkspace;
                    pFeatureClass = fws.OpenFeatureClass(txtLastYaw.Text);
                    ITable ptable = pFeatureClass as ITable;
                    ptable.DeleteSearchedRows(null);
                    
                }
            }
        }


        public bool GetCamOri(Ex_OriPara exOriCamL, IFeatureClass pFeatureClass,int nID)
        {
            try
            {
                ExOriPara pExOriPara = new ExOriPara();
                InOriPara pInOriPara = new InOriPara();

                pExOriPara.ori.omg = exOriCamL.ori.omg;
                pExOriPara.ori.phi = exOriCamL.ori.phi;
                pExOriPara.ori.kap = exOriCamL.ori.kap;
                pExOriPara.pos.X = exOriCamL.pos.X;
                pExOriPara.pos.Y = exOriCamL.pos.Y;
                pExOriPara.pos.Z = exOriCamL.pos.Z;

                pInOriPara.df = 1226.23;
                pInOriPara.dfx = 512;
                pInOriPara.dfy = 512;
                pInOriPara.nW = 1024;
                pInOriPara.nH = 1024;
                //pInOriPara.dfx = 50;
                //pInOriPara.dfy = 50;
                //pInOriPara.nW = 100;
                //pInOriPara.nH = 100;

                //pInOriPara.df = CView.PointInformationList[i].f;
                //pInOriPara.dfx = CView.PointInformationList[i].fx;
                //pInOriPara.dfy = CView.PointInformationList[i].fy;
                //pInOriPara.nW = CView.PointInformationList[i].width;
                //pInOriPara.nH = CView.PointInformationList[i].height;

                ///////////////////////////////////////////////////////////
                //非逐点计算
                //DrawPolygon(pRasterDem, pExOriPara, pInOriPara, pFeatureClass, nID);

                ////////////////////////////////////////////////////////////////
                //逐点计算
                drawRaster(pRasterDem, pExOriPara, pInOriPara);
                
            }
            catch (SystemException e)
            {
                return false;
            }

            return true;
        }

        private void drawRaster(IRaster pRaster, ExOriPara pExOriPara, InOriPara pInOriPara)
        {
            IRaster pRasterResult;
            IRasterLayer pRasterLayer = new RasterLayerClass();
            ClsGetCameraView CGetCameraView = new ClsGetCameraView();
            if (CGetCameraView.ImageReprojectionRange(pRaster, out pRasterResult, pExOriPara, pInOriPara.df, pInOriPara.dx, pInOriPara.dy,
                pInOriPara.nW, pInOriPara.nH, 50))
            {
                pRasterLayer.CreateFromRaster(pRasterResult);
                pMapControl.AddLayer(pRasterLayer as ILayer);
                pMapControl.Refresh();
            }
        }

        public bool DrawPolygon(IRaster pRaster, ExOriPara pExOriPara, InOriPara pInOriPara, IFeatureClass pFeatureClass, int nID)
        {
            try
            {
                Point2D[,] ptResult = null;
                ClsGetCameraView CGetCameraView = new ClsGetCameraView();
                if (CGetCameraView.ImageReprojectionRange(pRaster, out ptResult, pExOriPara, pInOriPara, 50))
                {
                    IPointCollection pPointCollection = new PolygonClass();
                    for (int j = 0; j < ptResult.GetLength(1); j++)
                    {
                        if (ptResult[0, j] == null)
                        {
                            continue;
                        }
                        IPoint pPoint1 = new PointClass();
                        pPoint1.X = ptResult[0, j].X;
                        pPoint1.Y = ptResult[0, j].Y;
                        pPointCollection.AddPoint(pPoint1);
                    }
                    for (int k = ptResult.GetLength(1) - 1; k >= 0; k--)
                    {
                        if (ptResult[1, k] == null)
                        {
                            continue;
                        }

                        IPoint pPoint2 = new PointClass();
                        pPoint2.X = ptResult[1, k].X;
                        pPoint2.Y = ptResult[1, k].Y;
                        pPointCollection.AddPoint(pPoint2);
                    }
                    IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                    IPolygon pPolygon = new PolygonClass();
                    pPolygon = pPointCollection as IPolygon;
                    pPolygon.Close();
                    pFeatureTemp.Shape = pPolygon as IGeometry;
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("PointID"), nID);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("X"), pExOriPara.pos.X);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Y"), pExOriPara.pos.Y);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Z"), pExOriPara.pos.Z);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("omg"), pExOriPara.ori.omg);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("phi"), pExOriPara.ori.phi);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("kap"), pExOriPara.ori.kap);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("width"), pInOriPara.nW);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("height"), pInOriPara.nH);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("fx"), pInOriPara.dfx);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("fy"), pInOriPara.dfy);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("f"), pInOriPara.df);
                    pFeatureTemp.Store();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void btnXYZPHK_Click(object sender, EventArgs e)
        {
            int nErrorCode = -1;
            nErrorCode=zg_vec(3, 5115, 7);
            if (nErrorCode >= 0)  //正确
            {
                txtPosX.Text = zgVecValue[0].ToString();
                txtPosY.Text = zgVecValue[1].ToString();
                txtPosZ.Text = zgVecValue[2].ToString();
            }
            
            nErrorCode=zg_vec(3, 5115, 6);
            if (nErrorCode >= 0)
            {
                txtOriOmg.Text = zgVecValue[0].ToString();
                txtOriPhi.Text = zgVecValue[1].ToString();
                txtOriKap.Text = zgVecValue[2].ToString();
            }            
        }

        private void txtPosX_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
