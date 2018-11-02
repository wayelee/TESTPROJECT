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
    public partial class FrmRasterRotate : OfficeForm
    {
        public FrmRasterRotate()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public double X;
        public double Y;
        public double angle;
        public bool isture;
        private void FrmRasterRotate_Load(object sender, EventArgs e)
        {
            //slider2.Maximum = 360;
            //slider2.Minimum = -360;
           // slider2.Value = 0;
            angle = 0;
            textRotateX.Text = X.ToString();
            textRotateY.Text = Y.ToString();
            textRotate.Text = "0.0";
            isture = false;
        }

        private void buttonX_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (double.TryParse(textRotateX.Text, out X) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (double.TryParse(textRotateY.Text, out Y) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (double.TryParse(textRotate.Text, out angle) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (angle < -360 || angle > 360)
                {
                    MessageBox.Show("超出阈值，请输入-360°到360°范围内的角度", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    //angle = slider2.Value;
                    isture = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void textRotateX_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
