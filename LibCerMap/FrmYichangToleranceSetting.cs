using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
namespace LibCerMap
{
    public partial class FrmYichangToleranceSetting : Form
    {
        DataTable InputSourceTable;
        DataTable ResultTable;
        public FrmYichangToleranceSetting(DataTable dtable)
        {
            InitializeComponent();
            InputSourceTable = dtable;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(numericUpDown1.Value);
            double length = Convert.ToDouble(numericUpDown2.Value);
            double width = Convert.ToDouble(numericUpDown3.Value);
            double depth = Convert.ToDouble(numericUpDown4.Value);
            List<DataRow> YichangRowList = (from DataRow r in InputSourceTable.AsEnumerable()
                                            where r[EvConfig.IMUPointTypeField] != DBNull.Value &&
                                            r[EvConfig.IMUPointTypeField].ToString().Contains("异常") &&
                                            r["壁厚变化"] != DBNull.Value && Math.Abs(Convert.ToDouble(r["壁厚变化"])) <= depth &&
                                            r["长度变化"] != DBNull.Value && Math.Abs(Convert.ToDouble(r["长度变化"])) <= length &&
                                            r["宽度变化"] != DBNull.Value && Math.Abs(Convert.ToDouble(r["宽度变化"])) <= width &&
                                            r["角度变化"] != DBNull.Value && Math.Abs(Convert.ToDouble(r["角度变化"])) <= angle 
                                            select r) .ToList();
            ResultTable = new DataTable();
            ResultTable = InputSourceTable.Clone();
            foreach(DataRow r in YichangRowList)
            {
                ResultTable.ImportRow(r);
            }
            FrmYichangBianHuaFenXi frm = new FrmYichangBianHuaFenXi(ResultTable);
            CreateDifferenceChart(frm.LengthChart, "长度变化");
            CreateDifferenceChart(frm.WidthChart, "宽度变化");
            CreateDifferenceChart(frm.DepthChart, "壁厚变化");
            frm.ShowDialog();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CreateDifferenceChart(DevExpress.XtraCharts.ChartControl control, string ColumnName)
        {
            try
            {
                DataTable stable = ResultTable;
                ChartControl chartControl1 = control;
                double BegMeasure = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                double endMeasure = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                chartControl1.Series.Clear();
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series(ColumnName, ViewType.Bar);
                series.ShowInLegend = false;
                foreach (DataRow r in stable.Rows)
                {
                    double m; double z;
                    if (r[ColumnName] == DBNull.Value)
                        continue;
                    m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                    z = Math.Round(Math.Abs(Convert.ToDouble(r[ColumnName])), 2);
                    series.Points.Add(new SeriesPoint(m, z));
                }

                // System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chartControl1.Series.Add(series);
                chartControl1.Titles.Clear();
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);

                XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
                // Customize the appearance of the X-axis title. 
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "匹配异常里程";
                diagram.AxisX.Title.TextColor = Color.Black;
                diagram.AxisX.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                // Customize the appearance of the Y-axis title. 
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
                diagram.AxisY.Title.Text = ColumnName;
                diagram.AxisY.Title.TextColor = Color.Black;
                diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                //((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "对齐里程";
                //((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "特征点里程差";
                 
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            // return image;
        }
         
    }
}
