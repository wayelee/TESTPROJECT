using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdPrintPreview.
    /// </summary>
    [Guid("b9145b24-9af2-46c4-9736-1516f15205c4")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdPrintPreview")]
    public sealed class CmdPrintPreview : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper;
        IPageLayoutControl3 pPageLayoutControl = null;

        //declare the dialogs for print preview, print dialog, page setup
        internal PrintPreviewDialog printPreviewDialog1;
        internal PrintDialog printDialog1;
        internal PageSetupDialog pageSetupDialog1;
        //declare a PrintDocument object named document, to be displayed in the print preview


        private System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();
        //cancel tracker which is passed to the output function when printing to the print preview
        private ITrackCancel m_TrackCancel = new CancelTrackerClass();

        private short m_CurrentPrintPage;


        public CmdPrintPreview(System.Drawing.Printing.PrintDocument m_document)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE";  //localizable text
            base.m_caption = "打印预览";  //localizable text
            base.m_message = "打印预览";  //localizable text 
            base.m_toolTip = "打印预览"; //localizable text 
            base.m_name = "CustomCE.CmdPrintPreview";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }

            document = m_document;
            //打印预览设置
            InitializePrintPreviewDialog(); //initialize the print preview dialog
            printDialog1 = new PrintDialog(); //create a print dialog object
            InitializePageSetupDialog(); //initialize the page setup dialog   
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add other initialization code

        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdPrintPreview.OnClick implementation

            if (m_hookHelper.Hook is IToolbarControl)
            {
                pPageLayoutControl = (IPageLayoutControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
            }
            //In case the container is MapControl
            else if (m_hookHelper.Hook is IPageLayoutControl3)
            {
                pPageLayoutControl = (IPageLayoutControl3)m_hookHelper.Hook;
            }
            else
            {
                MessageBox.Show("当前界面必须是PageLayoutControl界面!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //pPageLayoutControl.ActiveView.Refresh();

            ////打印预览设置
            //InitializePrintPreviewDialog(); //initialize the print preview dialog
            //printDialog1 = new PrintDialog(); //create a print dialog object
            //InitializePageSetupDialog(); //initialize the page setup dialog   

            //initialize the currently printed page number
            m_CurrentPrintPage = 0;

            //check if a document is loaded into PageLayout	control
            //if (pPageLayoutControl.DocumentFilename == null) return;
            //set the name of the print preview document to the name of the mxd doc
            document.DocumentName = pPageLayoutControl.DocumentFilename;

            //set the PrintPreviewDialog.Document property to the PrintDocument object selected by the user
            printPreviewDialog1.Document = document;

            //show the dialog - this triggers the document's PrintPage event
            printPreviewDialog1.ShowDialog();

        }

        private void InitializePrintPreviewDialog()
        {
            // create a new PrintPreviewDialog using constructor
            printPreviewDialog1 = new PrintPreviewDialog();
            //set the size, location, name and the minimum size the dialog can be resized to
            printPreviewDialog1.ClientSize = new System.Drawing.Size(800, 600);
            printPreviewDialog1.Location = new System.Drawing.Point(29, 29);
            printPreviewDialog1.Name = "PrintPreviewDialog1";
            printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);
            //set UseAntiAlias to true to allow the operating system to smooth fonts
            printPreviewDialog1.UseAntiAlias = true;

            //associate the event-handling method with the document's PrintPage event
            this.document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(document_PrintPage);
        }

        private void InitializePageSetupDialog()
        {
            //create a new PageSetupDialog using constructor
            pageSetupDialog1 = new PageSetupDialog();
            //initialize the dialog's PrinterSettings property to hold user defined printer settings
            pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();
            //initialize dialog's PrinterSettings property to hold user set printer settings
            pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            //do not show the network in the printer dialog
            pageSetupDialog1.ShowNetwork = false;
        }

        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                pPageLayoutControl.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;

                //get the resolution of the graphics device used by the print preview (including the graphics device)
                short dpi = (short)e.Graphics.DpiX;
                //envelope for the device boundaries
                IEnvelope devBounds = new EnvelopeClass();
                //get page

                IPage page = pPageLayoutControl.Page;

                //the number of printer pages the page will be printed on
                short printPageCount;
                printPageCount = pPageLayoutControl.get_PrinterPageCount(0);
                m_CurrentPrintPage++;

                //the currently selected printer
                IPrinter printer = pPageLayoutControl.Printer;
                //get the device bounds of the currently selected printer
                page.GetDeviceBounds(printer, m_CurrentPrintPage, 0, dpi, devBounds);

                //structure for the device boundaries
                tagRECT deviceRect;
                //Returns the coordinates of lower, left and upper, right corners
                double xmin, ymin, xmax, ymax;
                devBounds.QueryCoords(out xmin, out ymin, out xmax, out ymax);
                //initialize the structure for the device boundaries
                deviceRect.bottom = (int)ymax;
                deviceRect.left = (int)xmin;
                deviceRect.top = (int)ymin;
                deviceRect.right = (int)xmax;

                //determine the visible bounds of the currently printed page
                IEnvelope visBounds = new EnvelopeClass();
                page.GetPageBounds(printer, m_CurrentPrintPage, 0, visBounds);

                //get a handle to the graphics device that the print preview will be drawn to
                IntPtr hdc = e.Graphics.GetHdc();

                //print the page to the graphics device using the specified boundaries 
                pPageLayoutControl.ActiveView.Output(hdc.ToInt32(), dpi, ref deviceRect, visBounds, m_TrackCancel);

                //release the graphics device handle
                e.Graphics.ReleaseHdc(hdc);

                //check if further pages have to be printed
                if (m_CurrentPrintPage < printPageCount)
                    e.HasMorePages = true; //document_PrintPage event will be called again
                else
                    e.HasMorePages = false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.ActiveView is IPageLayout)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
