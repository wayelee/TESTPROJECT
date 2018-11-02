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
    public partial class FrmRasterTransZ : OfficeForm
    {
        public FrmRasterTransZ()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public double a;
        public double b;
        public bool istrue;
        private void FrmRasterTransZ_Load(object sender, EventArgs e)
        {
            a = 1;
            b = 0;
            istrue = false;
            textBoxX1.Text = "1";
            textBoxX2.Text = "0";
        }

        private void buttonX_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (double.TryParse(textBoxX1.Text, out a) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (double.TryParse(textBoxX2.Text, out b) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                istrue = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
}
