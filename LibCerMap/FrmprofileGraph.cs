using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmprofileGraph : OfficeForm
    {
        List<double> ZValue = new List<double>();
        List<double> DistanceValue = new List<double>();
        List<double> ZRValue = new List<double>();
        List<double> ZLValue = new List<double>();
        List<double> RDistanceValue = new List<double>();
        List<double> LDistanceValue = new List<double>();
        IPolyline pNewLine = null;
        double zmin = 99999;
        double zmax = -99999;
        public FrmprofileGraph(List<double> zvalue, List<double> distancevalue, IPolyline line, List<double> ZR, List<double> RD, List<double> ZL, List<double> LD)
        {
            InitializeComponent();
            this.EnableGlass = false;
            ZValue = zvalue;
            DistanceValue = distancevalue;
            pNewLine = line;
            ZRValue = ZR;
            RDistanceValue = RD;
            ZLValue = ZL;
            LDistanceValue = LD;
        }

        private DataTable CreatDataTable(List<double> ZValue, List<double> DistanceValue, List<double> ZR, List<double> RD, List<double> ZL, List<double> LD)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("高程值");
            dt.Columns.Add("距离");
            dt.Columns.Add("右高程值");
            dt.Columns.Add("右距离");
            dt.Columns.Add("左高程值");
            dt.Columns.Add("左距离");
            DataRow drow = dt.NewRow();
            int ncount = DistanceValue.Count;
            if (ncount > RDistanceValue.Count)
            {
                ncount = RDistanceValue.Count;
            }
            if (ncount > LDistanceValue.Count)
            {
                ncount = LDistanceValue.Count;
            }
           
            for (int j = 0; j < ncount; j++)
            {
                DataRow row = dt.NewRow();
                row["高程值"] = ZValue[j];
                row["距离"] = DistanceValue[j];
                row["右高程值"] = ZRValue[j];
                row["右距离"] = RDistanceValue[j];
                row["左高程值"] = ZLValue[j];
                row["左距离"] = LDistanceValue[j];
                dt.Rows.Add(row);
            }
            return dt;
        }

        private void FrmprofileGraph_Load(object sender, EventArgs e)
        {
            DataTable dt = default(DataTable);
          
            dt = CreatDataTable(ZValue, DistanceValue, ZRValue, RDistanceValue, ZLValue, LDistanceValue);
                    
            dataGridViewX1.DataSource = dt;
            chart1.DataSource = dt;
            chart1.Series[0].YValueMembers = "高程值";
            chart1.Series[0].XValueMember = "距离";
            chart1.Series[1].YValueMembers = "左高程值";
            chart1.Series[1].XValueMember = "左距离";
            chart1.Series[2].YValueMembers = "右高程值";
            chart1.Series[2].XValueMember = "右距离";
            chart1.DataBind();


            lbllength.Text = "线长度：" + pNewLine.Length.ToString("0.000")+"米";
            chart1.ChartAreas[0].AxisX.Title = "距离/米";
            chart1.ChartAreas[0].AxisY.Title = "高程值/米";

            for (int i = 0; i < ZValue.Count; i++)
            {
                if (ZValue[i] < zmin)
                {
                    zmin = ZValue[i];
                }
                if(ZValue[i]>zmax)
                {
                    zmax = ZValue[i];
                }
            }
            //int zminval = (int)zmin;
            //int zmaxval=(int)zmax;
            //int zminus=zmaxval-zminval;
            //if (zminus>5)
            //{
            //    sliYMin.Visible = true;
            //    sliYMin.Maximum = zminval;
            //}
            lblZMax.Text = "高程最大值：" + zmax.ToString("0.0000") + "米";
            lblZMin.Text = "高程最小值：" + zmin.ToString("0.0000") + "米"; 
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            chart1.Printing.PrintPreview();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            //保存文件对话框设置
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "BMP文件|*.bmp|JPG文件|*.jpg|PNG文件|*.png|Emf文件|*.Emf|Exif文件|*.Exif|Gif文件|*.Gif|Icon文件|*.Icon|MemoryBmp文件|*.MemoryBmp|Tiff文件|*.Tiff|Wmf文件|*.Wmf";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.Title = "导出";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)    
            {
                if (saveFileDialog1.FilterIndex == 1)    
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                else if (saveFileDialog1.FilterIndex == 2) 
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                else if (saveFileDialog1.FilterIndex == 3) 
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                else if (saveFileDialog1.FilterIndex == 4)  
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Emf);
                else if (saveFileDialog1.FilterIndex == 5)  
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Exif);
                else if (saveFileDialog1.FilterIndex == 6)  
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                else if (saveFileDialog1.FilterIndex == 7)  
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Icon);
                else if (saveFileDialog1.FilterIndex == 8)  
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.MemoryBmp);
                else if (saveFileDialog1.FilterIndex == 9)  
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Tiff);
                else if (saveFileDialog1.FilterIndex == 10)  
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Wmf);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            ExportToExcel exportexcel = new ExportToExcel();
            exportexcel.ExportDataGridViewToExcel(dataGridViewX1);
        }  

        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisY.Minimum = sliYMin.Value;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            txtmax.Enabled = true;
            txtmin.Enabled = true;
            txtmax.Text = chart1.ChartAreas[0].AxisY.Maximum.ToString();
            txtmin.Text = chart1.ChartAreas[0].AxisY.Minimum.ToString();
            txtmin.Focus();
        }

        private void btnminmaxok_Click(object sender, EventArgs e)
        {
            IsNumberic isnum = new IsNumberic();
            bool ismin = isnum.IsNumber(txtmin.Text);
            bool ismax = isnum.IsNumber(txtmax.Text);
            if (ismin&&ismax)
            {
                if (Convert.ToDouble(txtmin.Text) < Convert.ToDouble(txtmax.Text))
                {
                    chart1.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(txtmin.Text);
                    chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(txtmax.Text);
                    txtmax.Enabled = false;
                    txtmin.Enabled = false;
                } 
                else
                {
                    MessageBox.Show("最小值不能比最大值大", "提示", MessageBoxButtons.OK);
                }               
            } 
            else
            {
                MessageBox.Show("请输入数字", "提示", MessageBoxButtons.OK);
            }
           
        }
    }
}
