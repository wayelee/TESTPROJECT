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
    public partial class FrmIMUAlignmentresult : OfficeForm
    {
        private DataTable sourcetable;
        public FrmIMUAlignmentresult(DataTable tb)
        {
            InitializeComponent();
            sourcetable = tb;
        }

        private void FrmIMUAlignmentresult_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = sourcetable;
            gridControl1.Refresh();
            gridView1.OptionsView.ColumnAutoWidth = false;
        }

        private void button1_Click(object sender, EventArgs e)
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
