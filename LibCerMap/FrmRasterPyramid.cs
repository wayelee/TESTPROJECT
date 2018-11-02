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
    public partial class FrmRasterPyramid : OfficeForm
    {
        public bool m_bAlwaysSame = false;
        public FrmRasterPyramid()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ChkCreatePyramid_CheckValueChanged(object sender, EventArgs e)
        {
            m_bAlwaysSame = ChkCreatePyramid.Checked;
        }
    }
}
