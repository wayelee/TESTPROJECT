using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmRandomModelSetting : OfficeForm
    {
        #region private class member
        private int nRockCount=10;
        private int nCraterCount=10;
        #endregion

        #region public class properties
        public int RockCount
        {
            get
            {
                return nRockCount;
            }

            set
            {
                nRockCount = value;
            }
        }

        public int CraterCount
        {
            set
            {
                nCraterCount = value;
            }

            get
            {
                return nCraterCount;
            }
        }
        #endregion

        public IMap m_pMap;
        public string TargetTinLayerName;
        public FrmRandomModelSetting()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            nRockCount = txtRockCount.Value;
            nCraterCount = txtCraterCount.Value;
            //if (comboBoxExLayers.SelectedIndex == -1)
            //{
            //    MessageBox.Show("需要制定生成随机模型的Tin图层！");
            //    // this.DialogResult = DialogResult.No;
            //}
            //else
            //{
            //    TargetTinLayerName = comboBoxExLayers.SelectedItem.ToString();
            //    this.DialogResult = DialogResult.OK;
            //}
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtCraterCount_ValueChanged(object sender, EventArgs e)
        {
            if (sldCraterCount.Value != txtCraterCount.Value)
            {
                sldCraterCount.Value = txtCraterCount.Value;
            }
        }

        private void txtRockCount_ValueChanged(object sender, EventArgs e)
        {
            if (sldRockCount.Value != txtRockCount.Value)
            {
                sldRockCount.Value = txtRockCount.Value;
            }
        }

        private void sldRockCount_ValueChanged(object sender, EventArgs e)
        {
            if (txtRockCount.Value != sldRockCount.Value)
            {
                txtRockCount.Value = sldRockCount.Value;
            }
        }

        private void sldCraterCount_ValueChanged(object sender, EventArgs e)
        {
            if (txtCraterCount.Value != sldCraterCount.Value)
            {
                txtCraterCount.Value = sldCraterCount.Value;
            }
        }

        private void FrmRandomModelSetting_Load(object sender, EventArgs e)
        {
            
        }
    }
}
