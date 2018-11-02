using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.DataSourcesRaster;

namespace LibCerMap
{
    public partial class FrmCreateShapeFile : OfficeForm
    {
        private IMapControl3 m_mapControl = null;
        private IMapControl3 m_mapCtlHide = null;
        private ISpatialReference pSpaReference = null;

        public FrmCreateShapeFile(IMapControl3 mapControl,IMapControl3 mapCtlHide)
        {
            InitializeComponent();
            this.EnableGlass = false;
           m_mapControl = mapControl;
            m_mapCtlHide=mapCtlHide;        
        }

        private void FrmCreateShapeFile_Load(object sender, EventArgs e)
        {
            comboBoxExShapeType.SelectedIndex = 0;
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
                textBoxXPath.Text = dlg.FileName;
            }
        }

        private void buttonXOK_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(textBoxXPath.Text);
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
                /*
                //不定义空间参考会报异常
                //为esriFieldTypeGeometry类型的字段创建几何定义，包括类型和空间参照    
                IGeometryDef pGeoDef = new GeometryDefClass();     //The geometry definition for the field if IsGeometry is TRUE.   
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                pGeoDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                pGeoDefEdit.SpatialReference.SetDomain(-200, 200, -200, 200);//没有这句就抛异常来自HRESULT：0x8004120E。
               // pGeoDefEdit.SpatialReference_2 = pSPF;
                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldsEdit.AddField(pField);
                */



                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                //pGeoDefEdit.AvgNumPoints_2 = 5;
                switch (comboBoxExShapeType.SelectedIndex)
                {
                    case 0:
                        pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                        break;
                    case 1:
                        pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                        break;
                    case 2:
                        pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                        break;
                }
                // pGeoDefEdit.GridCount_2 = 1;              
                pGeoDefEdit.HasM_2 = true;
                pGeoDefEdit.HasZ_2 = true;
                pGeoDefEdit.SpatialReference_2 = pSpaReference;
              //  pGeoDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。      



               // pGeoDefEdit.SpatialReference_2 = CreateProjectedCoordinateSystem();
                
               // pGeoDefEdit.SpatialReference.SetDomain(-8000000, 8000000, -800000, 8000000);//没有这句就抛异常来自HRESULT：0x8004120E。

                pFieldEdit.Name_2 = "SHAPE";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldEdit.IsNullable_2 = true;
                pFieldEdit.Required_2 = true;
                pFieldsEdit.AddField(pField);


                //IFeatureDataset pFdataset = pFeatureWorkspace.CreateFeatureDataset("mem", null);
                //pFeatureClass = pFdataset.CreateFeatureClass("", pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");            
                //  pFeatureClass = pFeatureWorkspace.CreateFeatureClass("", pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");


                ClsGDBDataCommon comm = new ClsGDBDataCommon();
                IWorkspace inmemWor = comm.OpenFromShapefile(filefolder);
                // ifeatureworkspacee
                IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
                IFeatureClass pFeatureClass = pFeatureWorkspace.CreateFeatureClass(di.Name, pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");


                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pFeatureClass;
                pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(di.Name);
                m_mapControl.AddLayer(pFeatureLayer as ILayer, 0);
                m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
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

                              richTextReference.Text = ClsGDBDataCommon.GetReferenceString(pSpatialReference);
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

                              richTextReference.Text = ClsGDBDataCommon.GetReferenceString(pSpatialReference);
                              pSpaReference = pSpatialReference;
                          }
                      }
                  }
              }
          }

          private void btnSelect_Click(object sender, EventArgs e)
          {
              FrmCoordinateSystem frm =new FrmCoordinateSystem(m_mapControl,m_mapCtlHide);
              if (frm.ShowDialog() == DialogResult.OK)
              {
                  pSpaReference = frm.pSpaReference;

                  richTextReference.Text = ClsGDBDataCommon.GetReferenceString(frm.pSpaReference);
              }
  
 
          }
    }
}
