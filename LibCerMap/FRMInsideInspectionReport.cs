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
    public partial class FRMInsideInspectionReport : Form
    {
        public List<DataTable> tablelist;
        public FRMInsideInspectionReport(List<DataTable> tlist)
        {
            tablelist = tlist;
            InitializeComponent();
        }

        private void FRMInsideInspectionReport_Load(object sender, EventArgs e)
        {
            DataTable t1 = tablelist[0];
            DataTable t2 = tablelist[1];
            gridControl1.DataSource = t1;
            gridControl2.DataSource = t2;

            gridView2.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridControl1.Refresh();
            gridControl2.Refresh();
            chartControl1.Series.Clear();
            DevExpress.XtraCharts.Series s = new DevExpress.XtraCharts.Series("",ViewType.Pie);
            DevExpress.XtraCharts.SeriesPoint sp = new DevExpress.XtraCharts.SeriesPoint();
            s.Points.Add(new DevExpress.XtraCharts.SeriesPoint("小于1米", t2.Rows[0][2]));
            s.Points.Add(new DevExpress.XtraCharts.SeriesPoint("＞1米且≤2米", t2.Rows[1][2]));
            s.Points.Add(new DevExpress.XtraCharts.SeriesPoint("大于2米", t2.Rows[2][2]));
            ((DevExpress.XtraCharts.PieSeriesLabel)s.Label).PointOptions.Pattern = "{A}: {V}";
            ((DevExpress.XtraCharts.PieSeriesLabel)s.Label).Position = DevExpress.XtraCharts.PieSeriesLabelPosition.TwoColumns;
            // Detect overlapping of series labels.
            ((DevExpress.XtraCharts.PieSeriesLabel)s.Label).ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.Default;
            PieSeriesView myView = (PieSeriesView)s.View;
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
               DataFilterCondition.GreaterThanOrEqual, 9));
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.NotEqual, "Others"));
            myView.ExplodeMode = PieExplodeMode.UseFilters;
            myView.ExplodedDistancePercentage = 30;
            myView.RuntimeExploding = true;
            myView.HeightToWidthRatio = 0.75;
            chartControl1.Series.Add(s);

            //microChart1.Text = "内检测对齐分析";
            //microChart1.ChartType = DevComponents.DotNetBar.eMicroChartType.Pie;

            //microChart1.DataPoints = new List<double>() {Convert.ToDouble(t2.Rows[0][2]), Convert.ToDouble(t2.Rows[1][2]) ,Convert.ToDouble(t2.Rows[2][2])};
            //microChart1.DataPointTooltips = new List<string>() { "≤1米", "＞1米且≤2米", "大于2米" };

            
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                SaveFileDialog1.Filter = "xlsx Files (*.xlsx)|*.xlsx";
                SaveFileDialog1.DefaultExt = "xlsx";
                if (SaveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return; ;
                string filepath = SaveFileDialog1.FileName;

                DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
                compositeLink.PrintingSystem = ps;
                
                DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink();
                link.Component = gridControl1;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);
                link = new DevExpress.XtraPrinting.PrintableComponentLink();
                link.Component = gridControl2;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);

                link = new DevExpress.XtraPrinting.PrintableComponentLink();
                link.Component = chartControl1;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);
                compositeLink.CreateDocument();
                compositeLink.CreatePageForEachLink();
                DevExpress.XtraPrinting.XlsxExportOptions ExportOpt = new DevExpress.XtraPrinting.XlsxExportOptions();
                ExportOpt.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFilePageByPage;

                compositeLink.ExportToXlsx(filepath, ExportOpt);
                MessageBox.Show("导出成功!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
