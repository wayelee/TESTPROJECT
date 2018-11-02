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

namespace LibCerMap
{
    public partial class FrmAddModelToTerrain : DevComponents.DotNetBar.OfficeForm
    {
        public IMap m_pMap;
        public IRasterLayer m_pRasterLayer;
        public string m_pXmlFilename;

        public FrmAddModelToTerrain(IMap pMap)
        {
            InitializeComponent();
            m_pMap = pMap;
        }

        private void btnXmlFilename_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgXmlFile = new OpenFileDialog();
            dlgXmlFile.Title = "打开配置文件路径：";
            dlgXmlFile.InitialDirectory = ".";
            //dlgTextureFile.Filter="3DS文件(*.3DS;*.3ds)|*.3DS;*.3ds|所有文件(*.*)|*.*";
            dlgXmlFile.Filter = "图像文件(*.XML;*.xml;|*.XML;*.xml|所有文件(*.*)|*.*";
            dlgXmlFile.RestoreDirectory = true;

            if (dlgXmlFile.ShowDialog() == DialogResult.OK)
            {
                txtXmlFilename.Text = dlgXmlFile.FileName;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //IRasterLayer pRasterLayer = null;
            m_pRasterLayer = ClsGDBDataCommon.GetLayerFromName(m_pMap, cmbRaster.Text) as IRasterLayer;
            //for (int i = 0; i < m_pMap.LayerCount; i++)
            //{
            //    ILayer pLayer = m_pMap.get_Layer(i);
            //    if (pLayer is IRasterLayer)
            //    {
            //        // IRasterLayer prlayer = pLayer as IRasterLayer;
            //        if (pLayer.Name == cmbRaster.SelectedItem.ToString())
            //        {
            //            m_pRasterLayer = pLayer as IRasterLayer;
            //            //pRaster = pRasterLayer.Raster;
            //            break;
            //        }
            //    }
            //}

            m_pXmlFilename = txtXmlFilename.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddModelToTerrain_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
            if (m_pMap != null)
            {
                for (int i = 0; i < m_pMap.LayerCount; i++)
                {
                    ILayer pLayer = m_pMap.get_Layer(i);
                    if (pLayer is IRasterLayer)
                    {
                        // IRasterLayer prlayer = pLayer as IRasterLayer;
                        cmbRaster.Items.Add(pLayer.Name);
                    }
                }
            }
        }

        private void cmbRaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
