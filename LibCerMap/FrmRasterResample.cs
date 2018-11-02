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
    public partial class FrmRasterResample : OfficeForm
    {
        public FrmRasterResample()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
       public double Cellsize;
       public int resampleType;
        private void FrmRasterResample_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = Cellsize.ToString() ;
            resampleType = 0;
            cmbTargetRasterLayer.Items.Add("最领近像元采样法");
            cmbTargetRasterLayer.Items.Add("双线性插值法");
            cmbTargetRasterLayer.Items.Add("三次卷积插值法");
            cmbTargetRasterLayer.SelectedIndex = 0;
        }

        private void buttonX_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX_OK_Click(object sender, EventArgs e)
        {
          try
            {
                if (double.TryParse(textBoxX1.Text, out Cellsize) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                resampleType = cmbTargetRasterLayer.SelectedIndex;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void cmbTargetRasterLayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
