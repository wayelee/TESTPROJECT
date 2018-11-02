using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace LibCerMap
{
    public partial class FrmSetAdjustData : OfficeForm
    {
        private List<String> m_listLayers=new List<String>();
        private IMapControl3 m_mapCtl = null;

        public List<String> ListLayers
        {
            get
            {
                return m_listLayers;
            }
        }

        public FrmSetAdjustData(IMapControl3 mapCtrl)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_mapCtl = mapCtrl;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_listLayers.Clear();

            int nCount = this.layersPanel.SelectedItems.Count;
            for (int i = 0; i < nCount; i++)
            {
                m_listLayers.Add(this.layersPanel.SelectedItems[i].Text);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < layersPanel.Items.Count; i++)
            {
                ((CheckBoxItem)(layersPanel.Items[i])).Checked = false;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < layersPanel.Items.Count; i++)
            {
                ((CheckBoxItem)(layersPanel.Items[i])).Checked = true;
            }
        }

        private void FrmSetAdjustData_Load(object sender, EventArgs e)
        {
            layersPanel.Items.Clear();
            for (int i = 0; i < m_mapCtl.Map.LayerCount; i++)
            {
                ILayer layer = m_mapCtl.Map.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    CheckBoxItem chkItem = new CheckBoxItem();
                    chkItem.Text = layer.Name;
                    layersPanel.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] { chkItem });

                    if (m_listLayers == null) continue;
                    for (int j = 0;j< m_listLayers.Count;j++ )
                    {
                        if (layer.Name == m_listLayers[j])
                        {
                            chkItem.Checked = true;
                        }
                    }
                }
            }

        }

    }//Calss
}//Namespace
