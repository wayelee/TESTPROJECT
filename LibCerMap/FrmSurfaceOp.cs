using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Controls;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmSurfaceOp : OfficeForm
    {
        private string m_DEMPath = null;
        private string m_ResultPath = null;
        private IMapControl3 m_mapControl = null; 
     
        public FrmSurfaceOp(IMapControl3 pMapControl)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_mapControl = pMapControl;
        }
 
        private void btnInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.tif;*.tiff;|*.tif;*.tiff;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (cmbTargetRasterLayer.Items.Contains(dlg.FileName) == false)
                {
                    cmbTargetRasterLayer.Items.Add(dlg.FileName);
                    cmbTargetRasterLayer.SelectedItem = dlg.FileName;
                }
            }
        }

        private void cmbTargetRasterLayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonX_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSurfaceOp_Load(object sender, EventArgs e)
        {
            cmbOutType.Items.Add("坡度");
            cmbOutType.Items.Add("坡向");
            cmbOutType.Items.Add("山体阴影");
            cmbOutType.Items.Add("等值线");
            cmbOutType.SelectedIndex = 0;
            cmbRenderType.Items.Add("无阴影");
            cmbRenderType.Items.Add("有阴影");
            cmbRenderType.SelectedIndex = 0;
            slider1.Maximum = 90;
            slider1.Value = 45;
            slider1.Minimum = 0;
            slider2.Maximum = 360;
            slider2.Value = 315;
            slider2.Minimum = 0;
            txtContour.Text = "2";
            textBoxX3.Text = "1";
            if (m_mapControl.Map != null)
            {
                //初始化图层列表
                IEnumLayer pEnumLayer = m_mapControl.Map.get_Layers(null, true);
                pEnumLayer.Reset();
                ILayer pLayer = null;
                while ((pLayer = pEnumLayer.Next()) != null)
                {
                    if (pLayer is IRasterLayer)
                    {
                        IDataLayer pDatalayer = pLayer as IDataLayer;
                        IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;
                        cmbTargetRasterLayer.Items.Add(pDname.WorkspaceName.PathName +"\\"+ pDname.Name);
                    }
                }
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdlg = new SaveFileDialog();
            if (cmbOutType.SelectedIndex <3)
            {
                fdlg.Filter ="Tif文件(*.tif)|*.tif";
            }
            else
            {
                fdlg.Filter = "shp文件(*.shp)|*.shp";
            }
            if (fdlg.ShowDialog() == DialogResult.OK && fdlg.FileName != "")
            {
                txtOutLayer.Text = fdlg.FileName;
                m_ResultPath = txtOutLayer.Text;
            }
        }

        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            if (cmbTargetRasterLayer.SelectedItem != null)
            {
                m_DEMPath = cmbTargetRasterLayer.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("请选择原始数据", "警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (m_ResultPath == null)
            {
                MessageBox.Show("请选择输出路径及文件名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RasterLayerClass rasterlayer = new RasterLayerClass();
            rasterlayer.CreateFromFilePath(m_DEMPath);
            IRaster iRaster = rasterlayer.Raster;
            int i = cmbOutType.SelectedIndex;
            IRaster pRaster = new Raster() ;
            IRasterLayer pRasterLayer = new RasterLayer();
            IFeatureLayer pFContourLayer = new FeatureLayerClass();
            switch(i)
            {
                case 0:
                 pRaster=CreateRasterSlope(iRaster);
                 if (pRaster == null)
                     return;
                 pRasterLayer.CreateFromRaster(pRaster);
                 pRasterLayer.Name = "坡度图";
                 m_mapControl.AddLayer(pRasterLayer as ILayer);
                 m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    break;
                case 1:
                   pRaster=CreateRasterAspect(iRaster);
                   if (pRaster == null)
                       return;
                   pRasterLayer.CreateFromRaster(pRaster);
                   pRasterLayer.Name = "坡向图";
                   m_mapControl.AddLayer(pRasterLayer as ILayer);
                   m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                   break;
                case 2:
                   pRaster=CreateRasterHillShade(iRaster);
                   if (pRaster == null)
                       return;
                   pRasterLayer.CreateFromRaster(pRaster);
                   pRasterLayer.Name = "山体阴影图";
                   m_mapControl.AddLayer(pRasterLayer as ILayer);
                   m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                   break;
                 case 3:
                   pFContourLayer = CreateRasterContour(iRaster);
                   if (pFContourLayer == null)
                       return;
                   pFContourLayer.Name = "等高线";
                   m_mapControl.AddLayer(pFContourLayer as ILayer);
                   m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                   break;
               default:
                   break;
            }
            //this.Close();
            return;
        }

        public IRaster CreateRasterSlope(IRaster raster)
        {

            try
            {

                RasterSurfaceOpClass class2 = new RasterSurfaceOpClass();
                IGeoDataset geoDataset = raster as IGeoDataset;

                double zFactor = Convert.ToDouble(textBoxX3.Text);
                if (zFactor <= 0)
                {
                    MessageBox.Show("请指定大于0的正数！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }

                //float zFactor = 1;
                object o = zFactor;
                esriGeoAnalysisSlopeEnum geoType = esriGeoAnalysisSlopeEnum.esriGeoAnalysisSlopeDegrees;
                IGeoDataset pGeoDataset = class2.Slope(geoDataset, geoType, ref o);
                IRaster pRaster = (IRaster)pGeoDataset;
                ISaveAs2 pSaveAs = pRaster as ISaveAs2;
                IDataset pDataset = pSaveAs.SaveAs(m_ResultPath + ".tif", null, "TIFF");
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
                MessageBox.Show("坡度计算完毕！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return pRaster;
            }
            catch (SystemException e)
            {

                MessageBox.Show(e.Message,"提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null ;
            }
        }
        public IRaster CreateRasterAspect(IRaster raster)
        {
            try
            {
                RasterSurfaceOpClass class2 = new RasterSurfaceOpClass();
                IGeoDataset geoDataset = raster as IGeoDataset;
                IGeoDataset pGeoDataset = class2.Aspect(geoDataset);
                IRaster pRaster = (IRaster)pGeoDataset;
                ISaveAs2 pSaveAs = pRaster as ISaveAs2;
                IDataset pDataset = pSaveAs.SaveAs(m_ResultPath + ".tif", null, "TIFF");
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
                MessageBox.Show("坡向计算完毕！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return pRaster;
            }
            catch (SystemException e)
            {

                MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }
        public IRaster CreateRasterHillShade(IRaster raster)
        {
            try
            {
                RasterSurfaceOpClass class2 = new RasterSurfaceOpClass();
                IGeoDataset geoDataset = raster as IGeoDataset;
                double zFactor = Convert.ToDouble(textBoxX3.Text);
                if (zFactor <= 0)
                {
                    MessageBox.Show("请指定大于0的正数！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                object o = zFactor;
                double azimuth = slider2.Value;
                double altitude = slider1.Value;
                bool inModelShadows = false;
                if (cmbRenderType.SelectedIndex == 1)
                {
                    inModelShadows = true;
                }
                IGeoDataset pGeoDataset = class2.HillShade(geoDataset,azimuth,altitude,inModelShadows,ref o);
                IRaster pRaster = (IRaster)pGeoDataset;
                ISaveAs2 pSaveAs = pRaster as ISaveAs2;
                IDataset pDataset = pSaveAs.SaveAs(m_ResultPath + ".tif", null, "TIFF");
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
                MessageBox.Show("山体阴影计算完毕！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return pRaster;
            }
            catch (SystemException e)
            {

                MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }
        public static void OperateConvertToShape(string sFileName, IFeatureClass pInFeatureClass)
        {

            try
            {

                string str = sFileName;

                string str2 = str.Substring(str.LastIndexOf(@"\") + 1);     //文件名

                string str3 = str.Substring(0, (str.Length - str2.Length) - 1);  //路径


                IDataset pInDataset = pInFeatureClass as IDataset;

                IDatasetName pInputDatasetName = (IDatasetName)pInDataset.FullName;

                IFeatureClassName pInputFClassName = (IFeatureClassName)pInputDatasetName;

                ///////////////////////////////////////////// //创建一个输出shp文件的工作空间 
                IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass() as IWorkspaceFactory;

                IWorkspace pWorkspace = pShpWorkspaceFactory.OpenFromFile(str3, 0);

                IWorkspaceName pOutWorkspaceName = new WorkspaceNameClass();

                IDataset pDataset = pWorkspace as IDataset;

                pOutWorkspaceName = (IWorkspaceName)pDataset.FullName;

                //创建一个要素集合

                IFeatureDatasetName pOutFeatureDatasetName = null;

                //创建一个要素类

                IFeatureClassName pOutFeatureClassName = new FeatureClassNameClass();

                IDatasetName pOutDatasetClassName = (IDatasetName)pOutFeatureClassName;

                pOutDatasetClassName.Name = str2;

                //作为输出参数

                pOutDatasetClassName.WorkspaceName = pOutWorkspaceName;

                //////////////////////////////////////////////////////////////// //创建输出文件属性字段

                IFields pOutFields, pInFields;

                IFieldChecker pFieldChecker;

                IField pGeoField;

                IEnumFieldError pEnumFieldError = null;

                pInFields = pInFeatureClass.Fields;

                pFieldChecker = new FieldChecker();

                pFieldChecker.Validate(pInFields, out pEnumFieldError, out pOutFields);

                ///设置输出文件的几何定义

                String shapeFieldName = pInFeatureClass.ShapeFieldName;

                int shapeFieldIndex = pInFeatureClass.FindField(shapeFieldName);

                IField shapeField = pInFeatureClass.Fields.get_Field(shapeFieldIndex);

                IGeometryDef geometryDef = shapeField.GeometryDef;

                IClone geometryDefClone = (IClone)geometryDef;

                IClone outGeometryDefClone = geometryDefClone.Clone();

                IGeometryDef outGeometryDef = (IGeometryDef)outGeometryDefClone;

                IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();

                IEnumInvalidObject enumInvalidObject = featureDataConverter.ConvertFeatureClass(pInputFClassName, null, null, pOutFeatureClassName, outGeometryDef, pOutFields, "", 1000, 0);

             //  IFeatureDataConverter pFeatureDataConverter =  new FeatureDataConverterClass();
               //pFeatureDataConverter.ConvertFeatureClass(sFeatureClassName, null, null, pFeatureClassName, null, null, "", 0x3e8, 0);

                MessageBox.Show("等值线计算完毕！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        public IFeatureLayer CreateRasterContour(IRaster raster)
        {
            try
            {
                ISurfaceOp pSurface = new RasterSurfaceOpClass();
                object BaseHeight = Type.Missing;
                IGeoDataset pGeoDataset = raster as IGeoDataset;
                double interval = Convert.ToDouble(txtContour.Text);
                if (interval<= 0)
                {
                    MessageBox.Show("请指定大于0的正数！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                IGeoDataset pContour = pSurface.Contour(pGeoDataset, interval, ref BaseHeight);
                IFeatureLayer pFContourLayer = new FeatureLayerClass();
                pFContourLayer.FeatureClass = pContour as IFeatureClass;
               // IFeatureDataConverter pFeatureDataConvert = new FeatureDataConverter();
                OperateConvertToShape(m_ResultPath, pFContourLayer.FeatureClass);
                return pFContourLayer;
            }
            catch (SystemException e)
            {

                MessageBox.Show(e.Message);
                return null ;
            }
        }

        private void txtOutLayer_TextChanged(object sender, EventArgs e)
        {
            m_ResultPath = txtOutLayer.Text;
            if (m_ResultPath == null)
            {
                MessageBox.Show("请选择输出路径及文件名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void cmbOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOutType.SelectedIndex == 2)
            {
                labelX4.Visible = false;
                txtContour.Visible = false;
                labelX3.Visible = true;
                cmbRenderType.Visible = true;
                slider1.Visible = true;
                slider2.Visible = true;
                slider1.Value = 45;
                slider2.Value = 315;
                labelX5.Visible = true;
                textBoxX3.Visible = true;
                textBoxX3.Text = "1";
            }
            else if (cmbOutType.SelectedIndex == 3)
            {
                labelX3.Visible = false;
                cmbRenderType.Visible = false;
                slider1.Visible = false;
                slider2.Visible = false;
                labelX4.Visible = true;
                txtContour.Visible = true;
                txtContour.Text = "2";
                labelX5.Visible = false;
                textBoxX3.Visible = false;
            }
            else if (cmbOutType.SelectedIndex == 1)
            {
                labelX5.Visible = false;
                textBoxX3.Visible = false;
                txtContour.Visible = false;
                labelX4.Visible = false;
            }
            else
            {
                labelX3.Visible = false;
                cmbRenderType.Visible = false;
                slider1.Visible = false;
                slider2.Visible = false;
                labelX4.Visible = false;
                txtContour.Visible = false;
                labelX5.Visible = true;
                textBoxX3.Visible = true;
                textBoxX3.Text = "1";
            }
        }

        private void slider2_ValueChanged(object sender, EventArgs e)
        {
            slider2.Text = "光源方位角：" + slider2.Value.ToString()+"°";
        }

        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            slider1.Text = "光源高度角：" + slider1.Value.ToString() + "°";
        }

          private void cmbRenderType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
