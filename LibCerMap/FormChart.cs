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
    public partial class FormChart : Form
    {
        public DevExpress.XtraCharts.ChartControl ChartContrl;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartcontrol2;
        public FormChart()
        {
            InitializeComponent();
            ChartContrl = this.chartControl1;
            chartcontrol2 = this.chart1;
        }
        public void ChartControlToImage()
        {
            //chartControl1.Series.Clear();
            //Series series = new Series("特征点里程差", ViewType.Bar);  

            //foreach (DataRow r in centerlinePointTable.Rows)
            //{
            //    double m; double z; double underz;
            //    m = Convert.ToDouble(r[EvConfig.CenterlineMeasureField]);
            //    if (m < BegMeasure || m > endMeasure)
            //        continue;
            //    if (r[EvConfig.CenterlineMeasureField] != DBNull.Value && r[EvConfig.CenterlineZField] != DBNull.Value)
            //    {
            //        m = Convert.ToDouble(r[EvConfig.CenterlineMeasureField]);
            //        z = Convert.ToDouble(r[EvConfig.CenterlineZField]);
            //        series.Points.Add(new SeriesPoint(m, z));
            //    }
            //    if (r[EvConfig.CenterlineMeasureField] != DBNull.Value && r["管道埋深（"] != DBNull.Value)
            //    {
            //        m = Convert.ToDouble(r[EvConfig.CenterlineMeasureField]);
            //        z = Convert.ToDouble(r["管道埋深（"]);
            //        series2.Points.Add(new SeriesPoint(m, z));
            //    }
            //}

            //chartControl1.Series.Add(series);             
            //((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
             
            //((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);
        }
    }
}
