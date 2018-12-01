using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Drawing.Text;
using stdole;
using DevComponents.DotNetBar;
using AOFunctions.GDB;
using ESRI.ArcGIS.Output;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using DevExpress.Pdf;
using DevExpress.Pdf.Drawing;

namespace LibCerMap
{
    public partial class FrmGeneratePDFReport : OfficeForm
    {
        //点图层的投影信息
        ISpatialReference psf = null;

        IMapControl3 pMapcontrol;
        IPageLayoutControl pPageLayoutControl;

        public FrmGeneratePDFReport(IMapControl3 mapcontrol, IPageLayoutControl pagelayoutcontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapcontrol = mapcontrol;
            pPageLayoutControl = pagelayoutcontrol;
        }

        private void FrmPointToLine_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pMapcontrol.LayerCount; i++)
            {
                ILayer pLayer = null;
                if (pMapcontrol.get_Layer(i) is IFeatureLayer)
                {
                    pLayer = pMapcontrol.get_Layer(i);
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFeatureClass=pFeatureLayer .FeatureClass ;
                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                    {
                        cboBoxPointLayer.Items.Add(pLayer.Name);
                    }
                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                    {
                        comboBoxExCenterlineLayer.Items.Add(pLayer.Name);
                    }
                }
            }
            if (cboBoxPointLayer.Items.Count > 0)
            {
                cboBoxPointLayer.SelectedIndex = 0;
            }
            if (comboBoxExCenterlineLayer.Items.Count > 0)
            {
                comboBoxExCenterlineLayer.SelectedIndex = 0;
            }

        }
      
        //添加点图层按钮
        private void btAddPoint_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "添加点图层";
            OpenFile.Filter = "(*.shp)|*.shp";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                cboBoxPointLayer.Items.Add(System .IO .Path .GetFileNameWithoutExtension( OpenFile.FileName));

                try
                {
                    //获得文件路径
                    string filePath = System.IO.Path.GetDirectoryName(OpenFile.FileName);
                    //获得文件名称
                    string fileNam = System.IO.Path.GetFileName(OpenFile.FileName);
                    //加载shape文件
                    pMapcontrol.AddShapeFile(filePath, fileNam);
                    pMapcontrol.Refresh();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cboBoxPointLayer.SelectedIndex = 0;
            }
        }
     
        //保存线文件
        private void btAddLine_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Title = "输入保存文件路径及名称";
            SaveFile.Filter = "(*.shp)|*.shp";
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
            //    txtLineFilePath.Text = SaveFile.FileName;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            
           
            string outFile = "";
            
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF files (*.pdf)|*.pdf";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outFile = dlg.FileName;
            }
            else
            {
                return;
            }

            #region export mapfram

            IPageLayout _PageLayout = this.pPageLayoutControl.PageLayout;
            SizeF m_PageSize = new SizeF();
            double width;
            double hight;



            _PageLayout.Page.QuerySize(out width, out hight);
            m_PageSize.Width = Convert.ToSingle(width);
            m_PageSize.Height = Convert.ToSingle(hight);
            IUnitConverter pUConvert = new UnitConverterClass();
            esriUnits ut = _PageLayout.Page.Units;
            float pageUnitToInchUnitScale =(float)pUConvert.ConvertUnits(1, ut, esriUnits.esriInches);
            


            var layerOutPageSize = m_PageSize;
            var outputDPI = 300;

            var originalActiveView = _PageLayout as IActiveView;
            var activeView = _PageLayout as IActiveView;

            var avEvents = _PageLayout as IActiveViewEvents;

            float centerX = layerOutPageSize.Width / 2;
            float centerY = layerOutPageSize.Height / 2;

            var backGroundEnv = new Envelope() as IEnvelope;
            backGroundEnv.PutCoords(0, 0, layerOutPageSize.Width, layerOutPageSize.Height);

            activeView.Extent = backGroundEnv;
            //System.Threading.Thread.Sleep(2000);

            IExport pExporter = new ExportPDF() as IExport;
            pExporter.ExportFileName = outFile;
            pExporter.Resolution = outputDPI;

            var pExPdf = pExporter as IExportPDF;
            pExPdf.Compressed = true;
            pExPdf.ImageCompression = esriExportImageCompression.esriExportImageCompressionDeflate;

            var screenResolution = GetCurrentScreenResolution();
            var outputResolution = outputDPI;

            ESRI.ArcGIS.esriSystem.tagRECT exportRECT;
            exportRECT.left = 0;
            exportRECT.top = 0;
            exportRECT.right = (int)(activeView.ExportFrame.right * outputResolution / screenResolution) + 1;
            exportRECT.bottom = (int)(activeView.ExportFrame.bottom * outputResolution / screenResolution) + 1;

            var envelope = new Envelope() as IEnvelope;
            envelope.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
            pExporter.PixelBounds = envelope;

            var hDC = pExporter.StartExporting();
            activeView.Output(hDC, outputDPI, exportRECT, backGroundEnv, null);

            pExporter.FinishExporting();
            pExporter.Cleanup();
            MessageBox.Show("Export finished.");
            #endregion


            #region export chartcontrol
            IGraphicsContainer pGC = _PageLayout as IGraphicsContainer;
            pGC.Reset();
            IElement pE = pGC.Next();
            while (pE != null)
            {
                if (pE is IMapFrame)
                {

                    // framgeo related to _PageLayout.Page pagesize, 8.5 x 11, inch
                    IEnvelope framgeo = pE.Geometry.Envelope as IEnvelope;
                    float defaultDPI = PdfGraphics.DefaultDpi;
                    double x = defaultDPI * framgeo.LowerLeft.X * pageUnitToInchUnitScale;
                    double y = defaultDPI * (layerOutPageSize.Height - framgeo.LowerLeft.Y) * pageUnitToInchUnitScale;

                    chartControl1.Size = new System.Drawing.Size((int)(framgeo.Width * pageUnitToInchUnitScale * defaultDPI), (int)(framgeo.Height * pageUnitToInchUnitScale * defaultDPI));
                    // chartControl1.Size = new System.Drawing.Size((int)(framgeo.Width * 300), (int)(framgeo.Height * 300));

                    // PdfGraphics.defaultpageDPI

                    Image img = null;
                    Bitmap bmp = null;
                    // Create an image of the chart.
                    ImageFormat format = ImageFormat.Png;
                    using (MemoryStream s = new MemoryStream())
                    {
                        chartControl1.ExportToImage(s, format);
                        img = Image.FromStream(s);
                        bmp = new Bitmap(img);
                        bmp.SetResolution(300, 300);
                    }

                    //pictureBox1.Image = img;
                    //pictureBox1.Refresh();

                    using (PdfDocumentProcessor processor = new PdfDocumentProcessor())
                    {

                        processor.LoadDocument(outFile);
                        PdfDocument pd = processor.Document;
                        PdfPage page = pd.Pages[0];
                        PdfRectangle pdfrec = page.MediaBox;
                        RectangleF rf = new RectangleF(0, 0, Convert.ToSingle(pdfrec.Width) * 96 / 72, Convert.ToSingle(pdfrec.Height) * 96 / 72);
                        // Create and draw PDF graphics.
                        using (PdfGraphics graph = processor.CreateGraphics())
                        {
                            //  graph.DrawImage(img, new RectangleF((float)x, (float)y, (float)framgeo.Width * 96, (float)framgeo.Height*96));
                            graph.DrawImage(bmp, new RectangleF((float)x, (float)y, (float)framgeo.Width * pageUnitToInchUnitScale * defaultDPI, (float)framgeo.Height * pageUnitToInchUnitScale * defaultDPI));
                         //   graph.DrawImage(bmp, new RectangleF((float)x, 0, (float)500, 500));

                            // graph.DrawImage(img, rf);
                            graph.AddToPageForeground(page, defaultDPI, defaultDPI);
                            // Render a page with graphics.

                            processor.RenderNewPage(PdfPaperSize.Letter, graph);
                            processor.SaveDocument(outFile);
                        }
                    }

                    break;
                }
                pE = pGC.Next();
            }
            #endregion
         
        }

        [DllImport("GDI32.dll", EntryPoint = "GetDeviceCaps", ExactSpelling = true, SetLastError = true)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        private int GetCurrentScreenResolution()
        {
            try
            {
                var g = Graphics.FromHwnd(IntPtr.Zero);
                var desktop = g.GetHdc();
                int dpi = GetDeviceCaps(desktop, 88);  //90 Y 
                return dpi;
            }
            catch (Exception ex)
            {
                return 96;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {

        }

    }
}
