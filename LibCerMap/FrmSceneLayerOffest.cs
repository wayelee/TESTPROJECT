using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmSceneLayerOffest : OfficeForm
    {
        public double offsetvalue;
        public ISceneControl m_pSceneCtrl = null;
        public ILayer pLayer = null;
        I3DProperties properties = null;
        public FrmSceneLayerOffest()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void doubleInputOffset_ValueChanged(object sender, EventArgs e)
        {
           // offsetvalue = doubleInputOffset.Value;
        }
        private void SetdoubleOffsetValue(double val)
        {
            numericUpDownOffset.Value = Convert.ToDecimal(val);
        }

        private void buttonXOK_Click(object sender, EventArgs e)
        {
            //高程底面 
            if (m_pSceneCtrl != null)
            {

                //IScene pScene = m_pSceneCtrl.Scene;
                //ILayerExtensions layerextensions = pLayer as ILayerExtensions;
                ////I3DProperties properties = null;
                //if (pLayer is IRasterLayer)
                //{
                //    properties = new Raster3DPropertiesClass();
                //}
                //if (pLayer is IFeatureLayer)
                //{
                //    properties = new Feature3DPropertiesClass();
                //}
                //if (pLayer is ITinLayer)
                //{
                //    properties = new Tin3DPropertiesClass();
                //}
                //object p3d;
                //for (int j = 0; j < layerextensions.ExtensionCount; j++)
                //{
                //    p3d = layerextensions.get_Extension(j);
                //    if (p3d != null)
                //    {
                //        properties = p3d as I3DProperties;
                //        if (properties != null)
                //            break;
                //    }
                //}
                properties.OffsetExpressionString = numericUpDownOffset.Value.ToString();

                IActiveView iv = m_pSceneCtrl.Scene as IActiveView;
                iv.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
            }
        }

        private void FrmSceneLayerOffest_Load(object sender, EventArgs e)
        {
            // 读取偏移高程
            if (m_pSceneCtrl != null)
            {

                IScene pScene = m_pSceneCtrl.Scene;
                ILayerExtensions layerextensions = pLayer as ILayerExtensions;
                //I3DProperties properties = null;
                if (pLayer is IRasterLayer)
                {
                    properties = new Raster3DPropertiesClass();
                }
                if (pLayer is IFeatureLayer)
                {
                    properties = new Feature3DPropertiesClass();
                }
                if (pLayer is ITinLayer)
                {
                    properties = new Tin3DPropertiesClass();
                }
                object p3d;
                for (int j = 0; j < layerextensions.ExtensionCount; j++)
                {
                    p3d = layerextensions.get_Extension(j);
                    if (p3d != null)
                    {
                        properties = p3d as I3DProperties;
                        if (properties != null)
                            break;
                    }
                }
                try
                {
                    numericUpDownOffset.Value = Convert.ToDecimal(properties.OffsetExpressionString);
                }
                catch
                {
                }
               // properties.OffsetExpressionString = doubleInputOffset.Value.ToString();

                //IActiveView iv = m_pSceneCtrl.Scene as IActiveView;
                //iv.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
            }
        }

        private void numericUpDownOffset_ValueChanged(object sender, EventArgs e)
        {
            offsetvalue = Convert.ToDouble( numericUpDownOffset.Value);
        }
        
    }
}
