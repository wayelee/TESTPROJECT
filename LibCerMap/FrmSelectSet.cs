using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmSelectSet : OfficeForm
    {
        List<string> LayerName = new List<string>();
        AxMapControl pMapControl;
        public FrmSelectSet(List<string> Name, AxMapControl MapControl)
        {
            InitializeComponent();
            this.EnableGlass = false;
            LayerName = Name;
            pMapControl = MapControl;
        }

        private void FrmSelectSet_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < LayerName.Count;i++ )
            {
                cmblayer.Items.Add(LayerName[i]);
                cmblayer.SelectedIndex = 0;
            }
        }

        private void btncreat_Click(object sender, EventArgs e)
        {
            try
            {
                ILayer player = null;
                for (int j = 0; j < pMapControl.Map.LayerCount; j++)
                {
                    if (cmblayer.SelectedItem.ToString() == pMapControl.Map.Layer[j].Name)
                    {
                        player = pMapControl.Map.Layer[j];
                        break;
                    }
                }
                IFeatureLayer pFLayer = (IFeatureLayer)player;
                IFeatureLayerDefinition pFLDef = (IFeatureLayerDefinition)pFLayer;
                IFeatureSelection pFSel = (IFeatureSelection)pFLayer;
                ISelectionSet pSSet = pFSel.SelectionSet;
                if (pSSet.Count > 0)
                {
                    IFeatureLayer pSelFL = pFLDef.CreateSelectionLayer(player.Name + "Selection", true, null, null);
                    pMapControl.Map.AddLayer(pSelFL);
                }
                else
                {
                    MessageBox.Show("没有选中的对象！");
                }
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
    }
}
