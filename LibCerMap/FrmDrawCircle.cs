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
    public partial class FrmDrawCircle : OfficeForm
    {
        public double m_radius=0.0;//半径
        public double m_CircleX;//圆心
        public double m_circleY;
        public bool m_isFrmDrawCircleShow = false;//指示窗体状态
        public bool m_isMouseClickDraw = false;
        public IFeatureLayer m_FLayrDraw;
        public FrmDrawCircle(double radius)
        {
            InitializeComponent();
            m_radius = radius;
        }

        private void FrmDrawCircle_Load(object sender, EventArgs e)
        {
            m_isFrmDrawCircleShow = true;
            this.txtRadio.Text = m_radius.ToString();
        }

        private void cheMouseClick_CheckedChanged(object sender, EventArgs e)
        {
            m_isMouseClickDraw = cheMouseClick.Checked;
            btnDraw.Enabled = !cheMouseClick.Checked;
            txtCircleX.Enabled = !cheMouseClick.Checked;
            txtCircleY.Enabled = !cheMouseClick.Checked;
            txtRadio.Enabled = !cheMouseClick.Checked;
        }

        private void txtCircleX_TextChanged(object sender, EventArgs e)
        {
            IsNumberic isNum = new IsNumberic();
            if (isNum.IsNumber(txtCircleX.Text))
            {
                m_CircleX = Convert.ToDouble(txtCircleX.Text);
            }
            else
            {
                MessageBox.Show("输入的不是数字", "提示", MessageBoxButtons.OK);
            }
        }

        private void txtCircleY_TextChanged(object sender, EventArgs e)
        {
            IsNumberic isNum = new IsNumberic();
            if (isNum.IsNumber(txtCircleY.Text))
            {
                m_circleY = Convert.ToDouble(txtCircleY.Text);
            }
            else
            {
                MessageBox.Show("输入的不是数字", "提示", MessageBoxButtons.OK);
            }
        }

        private void txtRadio_TextChanged(object sender, EventArgs e)
        {
            //IsNumberic isNum = new IsNumberic();
            //if (isNum.IsNumber(txtRadio.Text))
            //{
            //    m_radius = Convert.ToDouble(txtRadio.Text);
            //}
            //else
            //{
            //    MessageBox.Show("输入的不是数字", "提示", MessageBoxButtons.OK);
            //}
        }

        private void FrmDrawCircle_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_isFrmDrawCircleShow = false;
            m_isMouseClickDraw = false;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            IsNumberic isNum = new IsNumberic();
            if (isNum.IsNumber(txtRadio.Text))
            {
                m_radius = Convert.ToDouble(txtRadio.Text);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("输入的不是数字", "提示", MessageBoxButtons.OK);
               
            }
          
        }
    }
}
