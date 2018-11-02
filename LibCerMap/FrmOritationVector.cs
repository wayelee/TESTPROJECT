using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmOritationVector : OfficeForm
    {
        private IMapControl3 m_mapControl = null;
        //点图层的投影信息
        ISpatialReference psf = null;
        private class pointSE
        {
            public IPointCollection pPointCollection;
            public List<double> sunvec;
            public List<double> earvec;
            //public pointSE()
            //{
            //    pPointCollection = null;
            //    sunvec = 0;
            //    earvec = 0;
            //}
        }

        public FrmOritationVector(IMapControl3 pMapControl)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_mapControl = pMapControl;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            saveFileDlg.Filter = "*.shp|*.shp";
            //saveFileDlg.FileName = m_pLayer.Name + "Export.shp";
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                txtOutFile.Text = saveFileDlg.FileName;
            }
            
        }

        private void FrmOritationVector_Load(object sender, EventArgs e)
        {
            if (m_mapControl.Map != null)
            {
                for (int i = 0; i < m_mapControl.Map.LayerCount; i++)
                {
                    ILayer pLayer = m_mapControl.Map.get_Layer(i);
                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                        if (pFeatureLayer.FeatureClass != null)
                        {
                            if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                            {
                                //IDataLayer pDatalayer = pLayer as IDataLayer;
                                //IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;
                                //cmbPointLayer.Items.Add(pDname.WorkspaceName.PathName + "\\" + pDname.Name);
                                string layername = pFeatureLayer.Name;
                                cmbPointLayer.Items.Add(layername);

                            }
                        }

                    }
                }
            }
        }

        private void cmbPointLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            IFeatureLayer pLayer = new FeatureLayerClass();
            for (int i = 0; i < m_mapControl.LayerCount; i++)
                {
                    if (m_mapControl.get_Layer(i).Name == cmbPointLayer.SelectedItem.ToString())
                    {
                        IFeatureLayer pflayer=new FeatureLayerClass();
                        pflayer = m_mapControl.get_Layer(i) as IFeatureLayer;
                        pLayer.FeatureClass = pflayer.FeatureClass;
                        break;
                    }
                }

            ITable pTableIn = (ITable)pLayer;
            for (int j = 0; j < pTableIn.Fields.FieldCount; j++)
            {
                cmbSunField.Items.Add(pTableIn.Fields.get_Field(j).Name);
                cmbEarthField.Items.Add(pTableIn.Fields.get_Field(j).Name);
            }
            if (cmbSunField.Items.Count>0)
            {
                cmbSunField.SelectedIndex = 0;
                cmbEarthField.SelectedIndex = 0;
            }
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //从点图层中读取所有点
            pointSE pPointse = new pointSE();
            pPointse = ReadPoint();
            IPointCollection PointCollection = pPointse.pPointCollection;
            List<double> sunv = new List<double>();
            List<double> earv = new List<double>();
            sunv = pPointse.sunvec;
            earv = pPointse.earvec;
            //创建shp文件
            creatshpfile();
            //根据点生成线
            for (int i = 0; i < PointCollection.PointCount; i++)
            {
                IPolyline PolySunline = CreateSunPolyline(PointCollection.Point[i], sunv[i]);

                IPolyline PolyEarline = CreateSunPolyline(PointCollection.Point[i], earv[i]);
 
                AddFeatureVector(PolySunline as IGeometry, "VecValue", sunv[i], "Type","太阳矢量");
                AddFeatureVector(PolyEarline as IGeometry, "VecValue", earv[i], "Type", "地球矢量");

            }
            this.Close();
        }

        //从点图层中收集所有点
        private pointSE ReadPoint()
        {

            string pPointFileName = cmbPointLayer.SelectedItem.ToString();
            ILayer pLayer = null;
            for (int i = 0; i < m_mapControl.LayerCount; i++)
            {
                if (pPointFileName == m_mapControl.get_Layer(i).Name)
                {
                    pLayer = m_mapControl.get_Layer(i);
                    break;
                }
            }

            int sunindex = 0;
            int earindex = 0;
            List<double> sunvect = new List<double>();
            List<double> earvect = new List<double>();
            ITable pTable = pLayer as ITable;
            IFields pPPfields = pTable.Fields;
            for (int i = 0; i < pPPfields.FieldCount; i++)
            {
                if (pPPfields.Field[i].Name == cmbSunField.Text)
                {
                    sunindex = i;
                }
                if (pPPfields.Field[i].Name == cmbEarthField.Text)
                {
                    earindex = i;
                }
            }


            psf = ((IGeoDataset)pLayer).SpatialReference;
            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
            //IFeatureCursor pFeatureCursor = pFeatureLayer.Search(null, false);

            //获取数据库或者单个文件的第一个属性字段
            //IFeature pFeature = null; 
            //IFeature pFeature=pFeatureCursor.NextFeature();
            //IField pField = null;
            //if (pFeatureLayer.FeatureClass.Fields.FindField("FID") != -1)
            //{
            //    pField = pFeatureLayer.FeatureClass.Fields.get_Field(pFeatureLayer.FeatureClass.Fields.FindField("FID"));
            //}
            //else if (pFeatureLayer.FeatureClass.Fields.FindField("OBJECTID") != -1)
            //{
            //    pField = pFeatureLayer.FeatureClass.Fields.get_Field(pFeatureLayer.FeatureClass.Fields.FindField("OBJECTID"));
            //}

            //第一个属性字段名称
            IFeatureClass pFC = pFeatureLayer.FeatureClass;
            IFeatureCursor pFCursor = pFC.Search(null, false);
            IFeature pF = pFCursor.NextFeature();
            IPointCollection pPointCollection = new MultipointClass();
            while (pF != null)
            {
                IGeometry pGeometry = pF.Shape as IGeometry;
                IPoint pPoint = pGeometry as IPoint;

                

                if (pF.get_Value(sunindex).GetType().FullName != "System.DBNull"
                    && pF.get_Value(earindex).GetType().FullName != "System.DBNull")
                {
                    sunvect.Add(Convert.ToDouble(pF.get_Value(sunindex)));
                    earvect.Add(Convert.ToDouble(pF.get_Value(earindex)));
                    pPointCollection.AddPoint(pPoint);
                }
                                                          
                pF = pFCursor.NextFeature();
            }

            //string FirstFieldName = pField.AliasName;
            //IQueryFilter pQueryFilter = new QueryFilterClass();
            //pQueryFilter.WhereClause = FirstFieldName + ">=0";
            //int number = pFeatureLayer.FeatureClass.FeatureCount(pQueryFilter);

            //IPointCollection pPointCollection = new MultipointClass();
            //for (int i = 0; i < number; i++)
            //{
            //    IGeometry pGeometry = pFeature.Shape as IGeometry;
            //    IPoint pPoint = pGeometry as IPoint;

            //    pPointCollection.AddPoint(pPoint);

            //    pFeature = pFeatureCursor.NextFeature();
            //    sunvect.Add(Convert.ToDouble(pFeature.get_Value(sunindex)));
            //    earvect.Add(Convert.ToDouble(pFeature.get_Value(earindex)));
            //}

            pointSE pPointSE = new pointSE();
            pPointSE.pPointCollection = pPointCollection;
            pPointSE.sunvec = sunvect;
            pPointSE.earvec = earvect;

            return pPointSE;
        }

        private void creatshpfile()
        {
            string pLineFile = txtOutFile.Text;
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

                pGeoDefEdit.SpatialReference_2 = psf;

                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldsEdit.AddField(pField);

                //新建字段
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "VecValue";             //代号
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "EarthVec";             //停泊点类型是否改变
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                //pFieldEdit.IsNullable_2 = true;
                //pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Type";             //停泊点类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                //创建shapefile线图层 
                pWS.CreateFeatureClass(pFileName, pFields, null, null, esriFeatureType.esriFTSimple, strShapeFieldName, "");
            }
            else
            {
                MessageBox.Show("文件已存在", "提示", MessageBoxButtons.OK);
            }
        }

        private IPolyline CreateSunPolyline(IPoint pPoint, double sunv)
        {
            double dx = pPoint.X;
            double dy = pPoint.Y;
            IPoint pPointEnd = new PointClass();
            double dAngle = (90 - sunv) / 180 * Math.PI;
            pPointEnd.X = dx + 1 * Math.Cos(dAngle);
            pPointEnd.Y = dy + 1 * Math.Sin(dAngle);
            ILine pline = new LineClass();
            pline.FromPoint = pPoint;
            pline.ToPoint = pPointEnd;
            ISegment pSegment = pline as ISegment;
            ISegmentCollection pSegmentCollection = new PolylineClass();
            pSegmentCollection.AddSegment(pSegment);
            IPolyline pPolyline = new PolylineClass();
            pPolyline = pSegmentCollection as IPolyline;
            return pPolyline;
        }

        private IPolyline CreateEarPolyline(IPoint pPoint, double sunv, double earv)
        {
            double dx = pPoint.X;
            double dy = pPoint.Y;
            IPoint pPointEnd = new PointClass();
            pPointEnd.X = dx + 1 * Math.Sin(earv);
            pPointEnd.Y = dy + 1 * Math.Cos(earv);
            ILine pline = new LineClass();
            pline.FromPoint = pPoint;
            pline.ToPoint = pPointEnd;
            ISegment pSegment = pline as ISegment;
            ISegmentCollection pSegmentCollection = new PolylineClass();
            pSegmentCollection.AddSegment(pSegment);
            IPolyline pPolyline = new PolylineClass();
            pPolyline = pSegmentCollection as IPolyline;
            return pPolyline;
        }

        //将线添加到图层
        private void AddFeatureVector(IGeometry geometry, string strFieldSun,double dSunValue,string strType,string strValue)
        {
            string pLineFile = txtOutFile.Text;
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


            //开始插入新的实体对象
            pFeatureCursor = pFeatureClass.Insert(true);
            pFeatureBuffer.Shape = geometry;
            //写入太阳矢量值
            pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField(strFieldSun), dSunValue);
            pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField(strType), strValue);
            ////写入地球矢量值
            //pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField(strFieldEar), dEarValue);
            //pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField(strType), strFieldEar);

            object featureOID = pFeatureCursor.InsertFeature(pFeatureBuffer);
            //保存实体
            pFeatureCursor.Flush();
            //结束空间编辑
            pWorkspaceEdit.StopEditOperation();
            pWorkspaceEdit.StopEditing(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
        }

        //将线添加到图层
        private void AddFeatureEar(IGeometry geometry, double earv)
        {
            string pLineFile = txtOutFile.Text;
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


            //开始插入新的实体对象
            pFeatureCursor = pFeatureClass.Insert(true);
            pFeatureBuffer.Shape = geometry;
            int index = pFeatureBuffer.Fields.FindField("EarthVector");
            if (index >= 0)
            {
                pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField("EarthVector"), earv);
            }
            index = pFeatureBuffer.Fields.FindField("Type");
            if (index >= 0)
            {
                pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField("Type"), "Ear");
            }

            object featureOID = pFeatureCursor.InsertFeature(pFeatureBuffer);
            //保存实体
            pFeatureCursor.Flush();
            //结束空间编辑
            pWorkspaceEdit.StopEditOperation();
            pWorkspaceEdit.StopEditing(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
