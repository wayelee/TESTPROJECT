using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Drawing.Text;
using stdole;
using DevComponents.DotNetBar;
using AOFunctions.GDB;

namespace LibCerMap
{
    public partial class FrmControlPointToCenterLine : OfficeForm
    {
        //点图层的投影信息
        ISpatialReference psf = null;

        IMapControl3 pMapcontrol;

        public FrmControlPointToCenterLine(IMapControl3 mapcontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapcontrol = mapcontrol;
        }

        private void FrmPointToLine_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pMapcontrol.LayerCount; i++)
            {
                ILayer pLayer = null;
                if (pMapcontrol.get_Layer(i) is IFeatureLayer)
                {
                    pLayer = pMapcontrol.get_Layer(i);
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFeatureClass=pFeatureLayer .FeatureClass ;
                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                    {
                        cboBoxPointLayer.Items.Add(pLayer.Name);
                    }
                }
            }
            cboBoxPointLayer.SelectedIndex = 0;
        }
      
        //添加点图层按钮
        private void btAddPoint_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "添加点图层";
            OpenFile.Filter = "(*.shp)|*.shp";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                cboBoxPointLayer.Items.Add(System .IO .Path .GetFileNameWithoutExtension( OpenFile.FileName));

                try
                {
                    //获得文件路径
                    string filePath = System.IO.Path.GetDirectoryName(OpenFile.FileName);
                    //获得文件名称
                    string fileNam = System.IO.Path.GetFileName(OpenFile.FileName);
                    //加载shape文件
                    pMapcontrol.AddShapeFile(filePath, fileNam);
                    pMapcontrol.Refresh();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cboBoxPointLayer.SelectedIndex = 0;
            }
        }
     
        //保存线文件
        private void btAddLine_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Title = "输入保存文件路径及名称";
            SaveFile.Filter = "(*.shp)|*.shp";
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                txtLineFilePath.Text = SaveFile.FileName;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (cboBoxPointLayer.SelectedItem == null)
            {
                MessageBox.Show("请加载点图层！");
                return;
            }
            if (txtLineFilePath.Text == "")
            {
                MessageBox.Show("请输入要输出线图层的路径及名称！");
                return;
            }
            try
            {
                //从点图层中读取所有点
                string pPointFileName = cboBoxPointLayer.SelectedItem.ToString();
                ILayer pLayer = null;
                for (int i = 0; i < pMapcontrol.LayerCount; i++)
                {
                    if (pPointFileName == pMapcontrol.get_Layer(i).Name)
                    {
                        pLayer = pMapcontrol.get_Layer(i);
                    }
                }
                psf = ((IGeoDataset)pLayer).SpatialReference;
                IFeatureLayer pFeatureLayerPoint = pLayer as IFeatureLayer;
                IPointCollection PointCollection = ReadPoint(pFeatureLayerPoint);

                //判断需要生成的线文件是否存在，若不存在则创建文件，若存在则将新添加的线写入文件
                if (File.Exists(txtLineFilePath.Text))
                {
                    //若已经存在则读取结尾点并加入到PointCollection
                    string pLineFile = txtLineFilePath.Text;
                    string pFilePath = System.IO.Path.GetDirectoryName(pLineFile);
                    string pFileName = System.IO.Path.GetFileName(pLineFile);

                    //打开工作空间   
                    IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
                    IFeatureWorkspace pWS = (IFeatureWorkspace)pWSF.OpenFromFile(pFilePath, 0);

                    IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                    pFeatureLayer.FeatureClass = pWS.OpenFeatureClass(pFileName);

                    IField pField = null;
                    if (pFeatureLayer.FeatureClass.Fields.FindField("FID")!=-1)
                    {
                        pField = pFeatureLayer.FeatureClass.Fields.get_Field(pFeatureLayer.FeatureClass.Fields.FindField("FID"));
                    }
                    else if (pFeatureLayer.FeatureClass.Fields.FindField("OBJECTID") != -1)
                    {
                        pField = pFeatureLayer.FeatureClass.Fields.get_Field(pFeatureLayer.FeatureClass.Fields.FindField("OBJECTID"));
                    }
                    //第一个属性字段名称
                    string FirstFieldName = pField.AliasName;
                    IQueryFilter pQueryFilter = new QueryFilterClass();
                    pQueryFilter.WhereClause = FirstFieldName + ">=0";
                    int number = pFeatureLayer.FeatureClass.FeatureCount(pQueryFilter);

                    IFeature pFeature;
                    if (FirstFieldName == "FID")
                    {
                        pFeature = pFeatureLayer.FeatureClass.GetFeature(number - 1);
                    }
                    else
                    {
                        pFeature = pFeatureLayer.FeatureClass.GetFeature(number);
                    }
                    IGeometry pGeometry = pFeature.Shape as IGeometry;
                    IPolyline pPolyline = pGeometry as IPolyline;

                    IPointCollection pPointCollection = new MultipointClass();
                    pPointCollection.AddPoint(pPolyline.ToPoint);

                    PointCollection.InsertPointCollection(0, pPointCollection);
                }
                else
                {
                    //不存在则创建新的线图层
                    CreateNewLineLayer();
                }

                //将所有的点连接成线
                IPolyline Polyline = CreatePolyline(PointCollection);

                //将连接成的线添加到线图层中
                AddFeature(Polyline as IGeometry);

                MessageBox.Show("成功生成中线");

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString ());
            }
        }

        //从点图层中收集所有点
        public IPointCollection ReadPoint(IFeatureLayer pFeatureLayer)
        {
            IFeatureCursor pFeatureCursor = pFeatureLayer.Search(null, false);

            //获取数据库或者单个文件的第一个属性字段
            IFeature pFeature = pFeatureCursor.NextFeature();
            IField pField = null;
            if (pFeatureLayer.FeatureClass.Fields.FindField("FID") != -1)
            {
                pField = pFeatureLayer.FeatureClass.Fields.get_Field(pFeatureLayer.FeatureClass.Fields.FindField("FID"));
            }
            else if (pFeatureLayer.FeatureClass.Fields.FindField("OBJECTID") != -1)
            {
                pField = pFeatureLayer.FeatureClass.Fields.get_Field(pFeatureLayer.FeatureClass.Fields.FindField("OBJECTID"));
            }
            
            //第一个属性字段名称
            string FirstFieldName = pField.AliasName;
          
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = FirstFieldName + ">=0";
            int number = pFeatureLayer.FeatureClass.FeatureCount(pQueryFilter);

            //DataTable dt = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pFeatureLayer.FeatureClass as ITable, "");
            //DataAlignment.DataAlignment.CanlculateDistanceInMiddlePointTable(dt);

            CustomizedControls.StatusForm progressBar = new CustomizedControls.StatusForm();
            progressBar.StartPosition = FormStartPosition.CenterScreen;
            progressBar.Show();
            

               
               

            IPointCollection pPointCollection = new MultipointClass();
            IPoint PrevPT = null;
            for (int i = 0; i < number; i++)
            {
                Application.DoEvents();
                progressBar.Status = "处理点 " + i.ToString() +  "/ " + number.ToString();
                IGeometry pGeometry = pFeature.Shape as IGeometry;
                IPoint pt = pGeometry as IPoint;               
                IPoint pPoint = new PointClass();

                IZAware zpt = pPoint as IZAware;
                zpt.ZAware = true;
                IMAware mpt = pPoint as IMAware;
                mpt.MAware = true;

                pPoint.PutCoords(pt.X, pt.Y);
                pPoint.Z = Convert.ToDouble(pFeature.Value[pFeature.Fields.FindField("Z_高程（米")]);

                if (i == 0)
                {
                    pPoint.M = 0;
                    PrevPT = pPoint;
                }
                else
                {

                    pPoint.M = PrevPT.M + DataAlignment.DataAlignment.CalculateDistanceBetween84TwoPoints(pPoint, PrevPT);

                    PrevPT = pPoint;
                }
                pPointCollection.AddPoint(pPoint);

                pFeature.Value[pFeature.Fields.FindField("里程（m）")] = pPoint.M;
                pFeature.Store();

                progressBar.Progress = Convert.ToInt16(Convert.ToDouble(i) / Convert.ToDouble(number) * 100);

                pFeature = pFeatureCursor.NextFeature();
            }

            progressBar.Close();
            return pPointCollection;
        }
       
        //将收集到的点转换成线
        private IPolyline CreatePolyline(IPointCollection pPointcollection)
        {
            int PointNumber = int.Parse(pPointcollection .PointCount.ToString ());

            object o = Type.Missing;
          
            //线数组
            ISegmentCollection pSegmentCollection = new PolylineClass();
            IZAware z = pSegmentCollection as IZAware;
            IMAware m = pSegmentCollection as IMAware;
            z.ZAware = true;
            m.MAware = true;

            for (int i = 0; i < PointNumber-1; i++)
            {
                ILine pLine = new LineClass();
             
                pLine.PutCoords(pPointcollection .get_Point(i),pPointcollection.get_Point(i+1));

                pSegmentCollection.AddSegment((ISegment)pLine, ref o, ref o);
            }
            IPolyline pPolyline = new PolylineClass();
            pPolyline = pSegmentCollection as IPolyline;

            return pPolyline;
        }

        //创建新图层
        private void CreateNewLineLayer()
        {
            string pLineFile = txtLineFilePath.Text;
            string pFilePath = System.IO.Path.GetDirectoryName(pLineFile);
            string pFileName = System.IO.Path.GetFileName(pLineFile);

            //打开工作空间   
            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pWS = (IFeatureWorkspace)pWSF.OpenFromFile(pFilePath, 0);

            //判断文件是否存在，若不存在则创建文件，若存在则将新添加的线写入文件
            if (!(File.Exists(pLineFile)))
            {
                const string strShapeFieldName = "shape";
                //设置字段集   
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                //设置字段   
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;

                //创建类型为几何类型的字段   
                pFieldEdit.Name_2 = strShapeFieldName;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;

                //为esriFieldTypeGeometry类型的字段创建几何定义，包括类型和空间参照    
                IGeometryDef pGeoDef = new GeometryDefClass();     //The geometry definition for the field if IsGeometry is TRUE.   
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
              
                // pGeoDefEdit.GridCount_2 = 1;              
                pGeoDefEdit.HasM_2 = true;
                pGeoDefEdit.HasZ_2 = true;             

                pGeoDefEdit.SpatialReference_2 = psf;

                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldsEdit.AddField(pField);

                //创建shapefile线图层 
                pWS.CreateFeatureClass(pFileName, pFields, null, null, esriFeatureType.esriFTSimple, strShapeFieldName, "");
            }
        }

        //将线添加到图层
        private void AddFeature(IGeometry geometry)
        {
            string pLineFile = txtLineFilePath.Text;
            string pFilePath = System.IO.Path.GetDirectoryName(pLineFile);
            string pFileName = System.IO.Path.GetFileName(pLineFile);

            //打开工作空间   
            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pWS = (IFeatureWorkspace)pWSF.OpenFromFile(pFilePath, 0);

            //写入实体对象
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = pWS.OpenFeatureClass(pFileName);

            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IDataset pDataset = (IDataset)pFeatureClass;
            IWorkspace pWorkspace = pDataset.Workspace;
            //开始空间编辑
            IWorkspaceEdit pWorkspaceEdit = (IWorkspaceEdit)pWorkspace;
            pWorkspaceEdit.StartEditing(true);
            pWorkspaceEdit.StartEditOperation();
            IFeatureBuffer pFeatureBuffer = pFeatureClass.CreateFeatureBuffer();
            IFeatureCursor pFeatureCursor;
         
            ////清除图层原有实体对象
            //pFeatureCursor = pFeatureClass.Update(null, true);
            //IFeature pFeature;
            //pFeature = pFeatureCursor.NextFeature();
            //while (pFeature != null)
            //{
            //    pFeatureCursor.DeleteFeature();
            //    pFeature = pFeatureCursor.NextFeature();
            //}

            //开始插入新的实体对象
            pFeatureCursor = pFeatureClass.Insert(true);
            pFeatureBuffer.Shape = geometry;
            object featureOID = pFeatureCursor.InsertFeature(pFeatureBuffer);
            //保存实体
            pFeatureCursor.Flush();
            //结束空间编辑
            pWorkspaceEdit.StopEditOperation();
            pWorkspaceEdit.StopEditing(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
