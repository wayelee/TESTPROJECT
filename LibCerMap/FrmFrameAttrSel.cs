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
    public partial class FrmFrameAttrSel : OfficeForm
    {
        public int FrmIndex=0;
        public FrmFrameAttrSel()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void FrmFrameAttrSel_Load(object sender, EventArgs e)
        {
            cBoxGrid.Checked = true;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (cBoxGrid.Checked == true)
            {
                FrmIndex = 1;
            }
            else if (cBoxFrame.Checked == true)
            {
                FrmIndex = 2;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
