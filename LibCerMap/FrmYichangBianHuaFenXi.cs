using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCerMap
{
    public partial class FrmYichangBianHuaFenXi : Form
    {
        public DataTable sourcetable;
        public DevExpress.XtraCharts.ChartControl LengthChart;
        public DevExpress.XtraCharts.ChartControl WidthChart;
        public DevExpress.XtraCharts.ChartControl DepthChart;

        public FrmYichangBianHuaFenXi(DataTable sdt)
        {
            InitializeComponent();
            sourcetable = sdt;
            gridControl1.DataSource = sourcetable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridControl1.Refresh();
            LengthChart = chartControl1;
            WidthChart = chartControl3;
            DepthChart = chartControl2;
            
        }

        private void buttonExport_Click(object sender, EventArgs e)
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
                link.Component = DepthChart;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);

                link = new DevExpress.XtraPrinting.PrintableComponentLink();
                link.Component = LengthChart;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);

                link = new DevExpress.XtraPrinting.PrintableComponentLink();
                link.Component = WidthChart;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);

                compositeLink.CreateDocument();
                compositeLink.CreatePageForEachLink();
                DevExpress.XtraPrinting.XlsxExportOptions ExportOpt = new DevExpress.XtraPrinting.XlsxExportOptions();
                ExportOpt.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFilePageByPage;

                compositeLink.ExportToXlsx(filepath, ExportOpt);
                MessageBox.Show("导出成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
