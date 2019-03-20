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
using System.Data.OleDb;

namespace LibCerMap
{
    public partial class FrmCenterLineInsideLayerSetting : OfficeForm
    {
        //点图层的投影信息
        ISpatialReference psf = null;

        IMapControl3 pMapcontrol;
        public IFeatureLayer pIMULayer;
        public IFeatureLayer pCenterlinePointLayer;
        public IFeatureLayer pCenterlineLayer;

        public FrmCenterLineInsideLayerSetting(IMapControl3 mapcontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapcontrol = mapcontrol;
        }

        private void FrmPointToLine_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < pMapcontrol.LayerCount; i++)
                {
                    ILayer pLayer = null;
                    if (pMapcontrol.get_Layer(i) is IFeatureLayer)
                    {
                        pLayer = pMapcontrol.get_Layer(i);
                        IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                        IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                        {
                            cboBoxPointLayer.Items.Add(pLayer.Name);
                        }
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                        {
                            comboBoxExCenterlineLayer.Items.Add(pLayer.Name);
                        }
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            comboBoxExCenterlineLinearLayer.Items.Add(pLayer.Name);
                        }
                    }
                }
                if (cboBoxPointLayer.Items.Count > 0)
                {
                    cboBoxPointLayer.SelectedIndex = 0;
                }
                if (comboBoxExCenterlineLayer.Items.Count > 0)
                {
                    comboBoxExCenterlineLayer.SelectedIndex = 0;
                }
                if (comboBoxExCenterlineLinearLayer.Items.Count > 0)
                {
                    comboBoxExCenterlineLinearLayer.SelectedIndex = 0;
                }
            }
            catch(SystemException ex)
            {
                
            }

        }
      
    

        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                string pPointFileName = cboBoxPointLayer.SelectedItem.ToString();
                string pCenterlinName = comboBoxExCenterlineLayer.SelectedItem.ToString();
                string pCenterlineLinearName = comboBoxExCenterlineLinearLayer.SelectedItem.ToString();
                IFeatureLayer IMUPointLayer = null;
                IFeatureLayer CenterlinePointLayer = null;
                IFeatureLayer CenterlinelinearLayer = null;
                for (int i = 0; i < pMapcontrol.LayerCount; i++)
                {
                    if (pPointFileName == pMapcontrol.get_Layer(i).Name)
                    {
                        IMUPointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                    if (pCenterlinName == pMapcontrol.get_Layer(i).Name)
                    {
                        CenterlinePointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                    if (pCenterlineLinearName == pMapcontrol.get_Layer(i).Name)
                    {
                        CenterlinelinearLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                }

                pIMULayer = IMUPointLayer;
                pCenterlinePointLayer = CenterlinePointLayer;
                pCenterlineLayer = CenterlinelinearLayer;
                if(IMUPointLayer != null && pCenterlinePointLayer != null && pCenterlineLayer != null)
                {
                    this.DialogResult = DialogResult.OK;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

       

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
