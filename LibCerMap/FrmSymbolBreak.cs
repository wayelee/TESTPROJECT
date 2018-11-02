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
    public partial class FrmSymbolBreak : OfficeForm
    {
        string fvalue = "";//用于设置修改之前的值
        FrmSymbology frmsym = null;
        List<double> fbreak = new List<double>();
        public FrmSymbolBreak(FrmSymbology frm)
        {
            InitializeComponent();
            this.EnableGlass = false;
            frmsym = frm;
        }

        private void FrmSymbolBreak_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < frmsym.dataTable1.Rows.Count;i++ )
            {
                DevComponents.AdvTree.Node node=new DevComponents.AdvTree.Node();
                node.Expanded = true;
                node.Name = frmsym.dataTable1.Rows[i][0].ToString();
                node.Text = frmsym.dataTable1.Rows[i][0].ToString(); 
                advTree1.Nodes.Add(node);

                double fb = (double)frmsym.dataTable1.Rows[i][0];
                fbreak.Add(fb);
            }
        }


        /// <summary>
        /// 用于设置修改之前的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advTree1_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            for (int i = 0; i < advTree1.Nodes.Count; i++)
            {
                if (advTree1.Nodes[i].Name == e.Node.Name)
                {
                    fvalue = advTree1.Nodes[i].Text;
                    break;
                }
            }
        }

        private void advTree1_AfterCellEditComplete(object sender, DevComponents.AdvTree.CellEditEventArgs e)
        {
            int cindex = 0;
            for (int i = 0; i < advTree1.Nodes.Count; i++)
            {
                if (advTree1.Nodes[i].Text == e.Cell.Text)
                {
                    cindex = i;
                    break;
                }
            }
            if (cindex == 0)
            {
                if (Convert.ToDouble(advTree1.Nodes[cindex].Text) >= Convert.ToDouble(frmsym.lblmin.Text))
                {
                    if (Convert.ToDouble(advTree1.Nodes[cindex].Text) < Convert.ToDouble(advTree1.Nodes[cindex + 1].Text))
                    {
                        frmsym.dataTable1.Rows[cindex][0] = advTree1.Nodes[cindex].Text;
                    }
                    else
                    {
                        MessageBox.Show("前面的间隔点值只能小于后面的间隔点值", "提示", MessageBoxButtons.OK);
                        advTree1.Nodes[cindex].Text = fvalue;
                    }
                }
                else
                {
                    MessageBox.Show("第一个间隔点值不能小于图小灰度最小值", "提示", MessageBoxButtons.OK);
                    advTree1.Nodes[cindex].Text = fvalue;
                }
                
            }
            else if (cindex==advTree1.Nodes.Count-1)
            {
                if (Convert.ToDouble(advTree1.Nodes[cindex].Text) <= Convert.ToDouble(fvalue))
                {
                    if (Convert.ToDouble(advTree1.Nodes[cindex].Text) > Convert.ToDouble(advTree1.Nodes[cindex - 1].Text))
                    {
                        frmsym.dataTable1.Rows[cindex][0] = advTree1.Nodes[cindex].Text;
                    }
                    else
                    {
                        MessageBox.Show("前面的间隔点值只能小于后面的间隔点值", "提示", MessageBoxButtons.OK);
                        advTree1.Nodes[cindex].Text = fvalue;
                    }
                }
                else
                {
                    MessageBox.Show("最后一个间隔点值不能大于图像灰度最大值", "提示", MessageBoxButtons.OK);
                    advTree1.Nodes[cindex].Text = fvalue;
                }
               
            } 
            else
            {
                if (Convert.ToDouble(advTree1.Nodes[cindex].Text) < Convert.ToDouble(advTree1.Nodes[cindex + 1].Text) 
                     && Convert.ToDouble(advTree1.Nodes[cindex].Text) > Convert.ToDouble(advTree1.Nodes[cindex - 1].Text))
                {
                    frmsym.dataTable1.Rows[cindex][0] = advTree1.Nodes[cindex].Text;
                }
                else
                {
                    MessageBox.Show("前面的间隔点值只能小于后面的间隔点值", "提示", MessageBoxButtons.OK);
                    advTree1.Nodes[cindex].Text = fvalue;
                }
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            frmsym.updatadatagrid();
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < fbreak.Count;i++ )
            {
                frmsym.dataTable1.Rows[i][0] = fbreak[i];
            }
            this.Close();
        }

    }
}
