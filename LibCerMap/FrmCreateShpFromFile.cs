using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;

namespace LibCerMap
{
    public partial class FrmCreateShpFromFile :OfficeForm
    {
        private IMapControl3 m_mapControl = null;
        private ISpatialReference pSpaReference = null;

        public FrmCreateShpFromFile(IMapControl3 mapControl)
        {
            m_mapControl = mapControl;
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void FrmCreateShpFromFile_Load(object sender, EventArgs e)
        {
            InitLayerReference(m_mapControl, cmbReference);
        }

        private void InitLayerReference(IMapControl3 mapControl, DevComponents.DotNetBar.Controls.ComboBoxEx cmb)
        {
            cmb.Items.Clear();
            for (int i = 0; i < mapControl.Map.LayerCount; i++)
            {
                ILayer pLayer = mapControl.Map.Layer[i];
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFClass = pFLayer.FeatureClass;
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint || pFClass.ShapeType == esriGeometryType.esriGeometryLine
                        || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline || pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        cmb.Items.Add(pLayer.Name);
                    }
                }
                else if (pLayer is IRasterLayer)
                {
                    cmb.Items.Add(pLayer.Name);
                }
            }
            if (cmb.Items.Count > 0)
            {
                cmb.SelectedIndex = 0;
            }
        }

        private void buttonXPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "|*.shp";
            dlg.OverwritePrompt = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = dlg.FileName;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PRJ文件(*.txt)|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //读取文件
                try
                {
                    //string strFileName = dlg.FileName;
                    StreamReader sr = new StreamReader(dlg.FileName, System.Text.Encoding.Default);
                    string strTemp = "";
                    while (sr.Peek() >= 0)
                    {
                        strTemp = sr.ReadLine();
                        richTextFile.Text = richTextFile.Text  + strTemp + "\n";
                    }
                    sr.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //创建shp文件
                string strFullName = txtFileName.Text;
                IFeatureClass pFt = CreateShapeFile(strFullName, pSpaReference);

                if (pFt == null)
                {
                    return;
                }
                //读取richtext文本
                string[] strSplitLine;
                string[] strSplitXY;
                IsNumberic isNum = new IsNumberic();
                strSplitLine = System.Text.RegularExpressions.Regex.Split(richTextFile.Text.Trim(), @"\n");
                for (int i = 0; i < strSplitLine.Length; i++)
                {
                    strSplitXY = System.Text.RegularExpressions.Regex.Split(strSplitLine[i], @",");
                    //根据XY坐标添加点
                    IPoint pPoint = new PointClass();
                    if (strSplitXY.Length < 2)
                    {
                        continue;
                    }
                    if (isNum.IsNumber(strSplitXY[0]) && isNum.IsNumber(strSplitXY[1]))
                    {
                        pPoint.X = System.Convert.ToDouble(strSplitXY[0]);
                        pPoint.Y = System.Convert.ToDouble(strSplitXY[1]);
                        IFeature pFeature = pFt.CreateFeature();
                        pFeature.Shape = pPoint;
                        pFeature.Store();
                    }
                }

                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pFt;
                pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(txtFileName.Text));
                m_mapControl.AddLayer(pFeatureLayer as ILayer, 0);
                m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public IFeatureClass CreateShapeFile(string strFileName, ISpatialReference pSRF)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(txtFileName.Text);
                string filefolder = di.Parent.FullName;
                ClsGDBDataCommon cdc = new ClsGDBDataCommon();
                //cdc.OpenFromShapefile(filefolder);
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                //设置字段   
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;

                //创建类型为几何类型的字段   
                pFieldEdit.Name_2 = "shape";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
 
                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                
                pGeoDefEdit.HasM_2 = false;
                pGeoDefEdit.HasZ_2 = false;
                pGeoDefEdit.SpatialReference_2 = pSRF;

                pFieldEdit.Name_2 = "SHAPE";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = true;
                pFieldsEdit.AddField(pField);


                ClsGDBDataCommon comm = new ClsGDBDataCommon();
                IWorkspace inmemWor = comm.OpenFromShapefile(filefolder);
                // ifeatureworkspacee
                IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
                IFeatureClass pFeatureClass = pFeatureWorkspace.CreateFeatureClass(di.Name, pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");

                return pFeatureClass;
                //IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                //pFeatureLayer.FeatureClass = pFeatureClass;
                //pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(di.Name);
                //m_mapControl.AddLayer(pFeatureLayer as ILayer, 0);
                //m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
                return null ;          
            }

        }

        private void cmbReference_SelectedIndexChanged(object sender, EventArgs e)
        {
            //在此添加空间参考的详细信息
            if (m_mapControl.Map != null)
            {
                for (int i = 0; i < m_mapControl.Map.LayerCount; i++)
                {
                    ILayer pLayer = m_mapControl.Map.get_Layer(i);
                    if (pLayer is IFeatureLayer)
                    {
                        if (pLayer.Name == cmbReference.SelectedItem.ToString())
                        {
                            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                            IGeoDataset pGeoDataset = pFeatureLayer as IGeoDataset;
                            ISpatialReference pSpatialReference = pGeoDataset.SpatialReference;

                            richTextFile.Text = ClsGDBDataCommon.GetReferenceString(pSpatialReference);
                            pSpaReference = pSpatialReference;
                        }
                    }
                    if (pLayer is IRasterLayer)
                    {
                        if (pLayer.Name == cmbReference.SelectedItem.ToString())
                        {
                            IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                            IRaster pRaster = pRasterLayer.Raster;
                            IRasterProps pRasterProps = pRaster as IRasterProps;
                            ISpatialReference pSpatialReference = pRasterProps.SpatialReference;

                            richTextFile.Text = ClsGDBDataCommon.GetReferenceString(pSpatialReference);
                            pSpaReference = pSpatialReference;
                        }
                    }
                }
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //
            ESRI.ArcGIS.SystemUI.ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(m_mapControl);
            command.OnClick();
            //更新图层列表
            InitLayerReference(m_mapControl, cmbReference);
        }

  
    }//Class
}
