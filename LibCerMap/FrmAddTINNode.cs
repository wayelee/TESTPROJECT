using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmAddTINNode : OfficeForm
    {
        public FrmAddTINNode()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public double dHeight;
        public bool bFromSurface;
        private void FrmAddTINNode_Load(object sender, EventArgs e)
        {
            cmbHeightSrc.SelectedIndex = 0;
        }

        public void SetdoubleInputHeightValue(double val)
        {
            doubleInputHeight.Value = val;
        }

        private void doubleInputHeight_ValueChanged(object sender, EventArgs e)
        {
            dHeight = doubleInputHeight.Value;
        }

        private void cmbHeightSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //自表面
            if (cmbHeightSrc.SelectedIndex == 0)
            {
                doubleInputHeight.Enabled = false;
                bFromSurface = true;
            }
                //指定
            else
            {
                doubleInputHeight.Enabled = true;
                bFromSurface = false;
            }
        }

        private void FrmAddTINNode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
            }
            Hide();
        }
    }
}
