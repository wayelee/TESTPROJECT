using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;

namespace LibCerMap
{
    public partial class FrmProjectBackward : DevComponents.DotNetBar.OfficeForm
    {
        public IMapControl3 m_pMapControl = null;

        public FrmProjectBackward(IMapControl3 pMapControl)
        {
            InitializeComponent();
            m_pMapControl = pMapControl;
        }

        private void btnDemFilename_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "地形文件(*.tif)|*.tif|所有文件|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                cmbDemLayer.Text = openfile.FileName;

                string szPath = ClsGDBDataCommon.GetDirectoryNameWithSeperator(cmbDemLayer.Text);
                string szOutputPolyline = szPath + "Polyline.shp";
                string szOutputPoint = szPath +  "Point.shp";

                txtOutputPoint.Text = szOutputPoint;
                txtOutputPolyline.Text = szOutputPolyline;
            }
        }

        //private void btnInputShpFilename_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog openfile = new OpenFileDialog();
        //    openfile.Filter = "路径文件(*.shp)|*.shp|所有文件|*.*";
        //    if (openfile.ShowDialog() == DialogResult.OK)
        //    {
        //        txtInputShpFilename.Text = openfile.FileName;
        //    }
        //}

        private void btnXmlFilename_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "相机姿态文件(*.txt;*.xml)|*.txt;*.xml|所有文件|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                txtXmlFilename.Text = openfile.FileName;
            }
        }

//         private IProjectedCoordinateSystem CreateProjectedCoordinateSystem()
//         {
// 
//             // Set up the SpatialReferenceEnvironment.
//             // SpatialReferenceEnvironment is a singleton object and needs to use the Activator class.
//             Type factoryType = Type.GetTypeFromProgID(
//                 "esriGeometry.SpatialReferenceEnvironment");
//             System.Object obj = Activator.CreateInstance(factoryType);
//             ISpatialReferenceFactory3 spatialReferenceFactory = obj as
//                 ISpatialReferenceFactory3;
// 
//             // Create a projection, GeographicCoordinateSystem, and unit using the factory.
//             IProjectionGEN projection = spatialReferenceFactory.CreateProjection((int)
//                 esriSRProjectionType.esriSRProjection_Sinusoidal) as IProjectionGEN;
//             IGeographicCoordinateSystem geographicCoordinateSystem =
//                 spatialReferenceFactory.CreateGeographicCoordinateSystem((int)
//                 esriSRGeoCS3Type.esriSRGeoCS_TheMoon);
//             ILinearUnit unit = spatialReferenceFactory.CreateUnit((int)
//                 esriSRUnitType.esriSRUnit_Meter) as ILinearUnit;
// 
//             // Get the default parameters from the projection.
//             IParameter[] parameters = projection.GetDefaultParameters();
// 
//             // Create a PCS using the Define method.
//             IProjectedCoordinateSystemEdit projectedCoordinateSystemEdit = new
//                 ProjectedCoordinateSystemClass();
//             object name = "Coustom";
//             object alias = "Coustom";
//             object abbreviation = "Coustom";
//             object remarks = "Coustom";
//             object usage = "Coustom";
//             object geographicCoordinateSystemObject = geographicCoordinateSystem as object;
//             object unitObject = unit as object;
//             object projectionObject = projection as object;
//             object parametersObject = parameters as object;
// 
// 
//             projectedCoordinateSystemEdit.Define(ref name, ref alias, ref abbreviation, ref
//         remarks, ref usage, ref geographicCoordinateSystemObject, ref unitObject,
//                 ref projectionObject, ref parametersObject);
//             ;
//             IProjectedCoordinateSystem userDefinedProjectCoordinateSystem =
//             projectedCoordinateSystemEdit as IProjectedCoordinateSystem;
//             return userDefinedProjectCoordinateSystem;
//         }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //string szInputFilename = ;
            string szDemFilename = cmbDemLayer.Text;
            IRasterLayer pDemLayer = ClsGDBDataCommon.GetLayerFromName(m_pMapControl.Map, szDemFilename) as IRasterLayer;
            if (pDemLayer == null)
                return;

            string szXmlFilename = txtXmlFilename.Text;
            
            //得到FeatureLayer
            IFeatureLayer pInputFeatureLayer = ClsGDBDataCommon.GetLayerFromName(m_pMapControl.Map, cmbPathFeatureLayer.Text) as IFeatureLayer;
            if (pInputFeatureLayer == null)
            {
                return;
            }

            try
            {
                //获得inputFeatureClass
                IFeatureClass pInputFeatureClass = pInputFeatureLayer.FeatureClass;

#region 注释代码
                //IFeatureDataset pInputDataset = pInputFeatureClass.FeatureDataset;
                //IWorkspace pInputWorkspace = pInputDataset.Workspace;
                //IWorkspace2 pInputWorkspace2 = pInputWorkspace as IWorkspace2;

                //设置空间参考
                //ISpatialReference pSpatialRef = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。
                //pSpatialRef.SetDomain(-8000000, 8000000, -8000000, 8000000);//没有这句就抛异常来自HRESULT：0x8004120E。
                //pSpatialRef.SetZDomain(-8000000, 8000000);
#endregion
                

                #region 创建点SHP文件的字段
                IFields pPointFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pPointFields;

                //创建类型为几何类型的字段   
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;

                pFieldEdit.Name_2 = "shape";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                pGeoDefEdit.HasM_2 = false;
                pGeoDefEdit.HasZ_2 = false;
                //pGeoDefEdit.SpatialReference_2 = pSpatialRef;
                //pFieldEdit.Name_2 = "SHAPE";
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = true;
                pFieldsEdit.AddField(pField);

                //添加字段
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Name";             //名字
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Type";             //类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldsEdit.AddField(pField);
                #endregion

                #region 创建polyline字段
                IFields pPolylineFields = new FieldsClass(); //pInputFeatureClass.Fields;
                pFieldsEdit = (IFieldsEdit)pPolylineFields;

                //创建类型为几何类型的字段   
                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;

                //pFieldEdit.Name_2 = "shape";
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //pGeoDef = new GeometryDefClass();
                //pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                //pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                //pGeoDefEdit.HasM_2 = false;
                //pGeoDefEdit.HasZ_2 = false;
                //pGeoDefEdit.SpatialReference_2 = pSpatialRef;
                ////pFieldEdit.Name_2 = "SHAPE";
                ////pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //pFieldEdit.GeometryDef_2 = pGeoDef;
                //pFieldEdit.IsNullable_2 = true;
                //pFieldEdit.Required_2 = true;
                //pFieldsEdit.AddField(pField);

                for (int i = 0; i < pInputFeatureClass.Fields.FieldCount; i++)
                {
                    //pField = new FieldClass();
                    //pFieldEdit = (IFieldEdit)pField;

                    IField pTmpField = pInputFeatureClass.Fields.get_Field(i);
                    IClone pSrcClone = pTmpField as IClone;
                    IClone pDstClone = pSrcClone.Clone();
                    IField pDstField = pDstClone as IField;
                    //IFieldEdit pTmpFieldEdit = pTmpField as IFieldEdit;

                    //pFieldEdit.Name_2 = pTmpFieldEdit.Name;
                    //pFieldEdit.Type_2 = pTmpFieldEdit.Type;
                    pFieldsEdit.AddField(pDstField);
                }

                ////添加字段
                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "Name";             //名字
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                //pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "Type";             //类型
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                //pFieldsEdit.AddField(pField);
                #endregion
                                
                //创建outputPolylineFeatureClass, outputPointFeatureClass
                IFeatureClass pOutputPolylineFeatureClass = null, pOutputPointFeatureClass = null;
                ClsGDBDataCommon pDataCommon = new ClsGDBDataCommon();


                string szPolylineFilename = txtOutputPolyline.Text;// "D:\\test\\testPolyline.shp";
                string szPointFilename = txtOutputPoint.Text;// "D:\\test\\testPoint.shp";
                //IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                //IWorkspace pWorkspace = pDataCommon.OpenFromShapefile(System.IO.Path.GetDirectoryName(szPolylineFilename));
                //IFeatureWorkspace pFW = pWorkspace as IFeatureWorkspace;

                string szPath = System.IO.Path.GetDirectoryName(szPointFilename);
                pOutputPointFeatureClass = pDataCommon.CreateShapefile(szPath, System.IO.Path.GetFileName(szPointFilename), pPointFields, null);
                pOutputPolylineFeatureClass = pDataCommon.CreateShapefile(szPath, System.IO.Path.GetFileName(szPolylineFilename),pPolylineFields,null);

                //判断类型是否一致
                if (pOutputPointFeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    MessageBox.Show("输出点文件已存在，但类型不一致，请更改或删除！");
                    return;
                }

                if (pOutputPolylineFeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                {
                    MessageBox.Show("输出路径文件已存在，但类型不一致，请更改或删除！");
                    return;
                }
                //pOutputPolylineFeatureClass = pDataCommon.CreateFeatureClass(pInputWorkspace as IWorkspace2, pInputDataset, "Polyline", pPolylineFields, null, null, "");
                //pOutputPointFeatureClass = pDataCommon.CreateFeatureClass(pInputWorkspace as IWorkspace2, pInputDataset, "Point", pPointFields, null, null, "");
                if (pOutputPointFeatureClass == null || pOutputPolylineFeatureClass == null)
                    return;                

                //先清空所有对象                
                IFeatureCursor pFeatureCursor = pOutputPolylineFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    pFeature.Delete();
                    pFeature = pFeatureCursor.NextFeature();
                }
                pFeatureCursor = pOutputPointFeatureClass.Search(null, false);
                pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    pFeature.Delete();
                    pFeature = pFeatureCursor.NextFeature();
                }
                
                //反投影
                ClsProjectBackward pProjectBackward = new ClsProjectBackward();
                if (pProjectBackward.projectBackward(pInputFeatureClass, pDemLayer, szXmlFilename, 
                    ref pOutputPolylineFeatureClass, ref pOutputPointFeatureClass))
                    MessageBox.Show("反投影操作已经完成！");
                else
                    MessageBox.Show("反投影操作失败！");

                //写入GDB

            }
            catch (System.Exception ex)
            {
            	MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void FrmProjectBackward_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
             //初始化图层列表
            IEnumLayer pEnumLayer = m_pMapControl.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = null;
            while ((pLayer = pEnumLayer.Next()) != null)
            {
                if (pLayer is IFeatureLayer)
                {
                    cmbPathFeatureLayer.Items.Add(pLayer.Name);
                }

                if (pLayer is IRasterLayer)
                {
                    cmbDemLayer.Items.Add(pLayer.Name);
                }                
            }
            
            if (cmbPathFeatureLayer.Items.Count>0)
                cmbPathFeatureLayer.SelectedIndex = 0;
            if (cmbDemLayer.Items.Count > 0)
                cmbDemLayer.SelectedIndex = 0;            
        }

        private void cmbPathFeatureLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnBtnTxtPolyline_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Shp文件(*.shp)|*.shp|所有文件|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                txtOutputPolyline.Text = openfile.FileName;

                string szPath = ClsGDBDataCommon.GetDirectoryNameWithSeperator(txtOutputPolyline.Text);
                string szOutputPoint = szPath + "Point.shp";
                txtOutputPoint.Text = szOutputPoint;
            }
        }

        private void btnBtnTxtPoint_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Shp文件(*.shp)|*.shp|所有文件|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                txtOutputPoint.Text = openfile.FileName;
            }
        }

        private void cmbDemLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRasterLayer pRasterLayer = ClsGDBDataCommon.GetLayerFromName(m_pMapControl.Map, cmbDemLayer.Text) as IRasterLayer;
            if (pRasterLayer == null) return;

            IDataLayer pDatalayer = pRasterLayer as IDataLayer;
            IDatasetName pDname = pDatalayer.DataSourceName as IDatasetName;

            string szPath=pDname.WorkspaceName.PathName;
            string szOutputPolyline = szPath + "Polyline.shp";
            string szOutputPoint = szPath + "Point.shp";

            txtOutputPoint.Text = szOutputPoint;
            txtOutputPolyline.Text = szOutputPolyline;
        }
    }
}
