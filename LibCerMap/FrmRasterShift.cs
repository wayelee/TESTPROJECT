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
    public partial class FrmRasterShift : OfficeForm
    {
        public FrmRasterShift()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public double X;
        public double Y;
        private void buttonX_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (double.TryParse(textshiftX.Text, out X) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (double.TryParse(textShiftY.Text, out Y) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonX_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRasterShift_Load(object sender, EventArgs e)
        {
            textshiftX.Text = "0.0";
            textShiftY.Text = "0.0";
            X = 0;
            Y = 0;
        }
    }
}
