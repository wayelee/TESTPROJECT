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
    /// Summary description for CmdPrintMap.
    /// </summary>
    [Guid("059d7edd-d145-4afc-95bc-7615bbbc4e4c")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdPrintMap")]
    public sealed class CmdPrintMap : BaseCommand
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

        internal PrintPreviewDialog pPrintPreviewDialog;
        internal PrintDialog pPrintDialog;

        //declare a PrintDocument object named document, to be displayed in the print preview
        private System.Drawing.Printing.PrintDocument pPrintDocument = new System.Drawing.Printing.PrintDocument();
        //cancel tracker which is passed to the output function when printing to the print preview
        private ITrackCancel m_TrackCancel = new CancelTrackerClass();

        //the page that is currently printed to the print preview
        private short m_CurrentPrintPage;

        public CmdPrintMap()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "打印";  //localizable text
            base.m_message = "打印设置";  //localizable text 
            base.m_toolTip = "打印";  //localizable text 
            base.m_name = "CustomCE.CmdPrintMap";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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

            InitializePrintPreviewDialog(); //initialize the print preview dialog
            pPrintDialog = new PrintDialog(); //create a print dialog object
        }

        #region Overridden Class Methods

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
            // TODO: Add CmdPrintMap.OnClick implementation

            if (m_hookHelper.Hook is IToolbarControl)
            {
                pPageLayoutControl =((IToolbarControl)m_hookHelper.Hook).Buddy as IPageLayoutControl3;
                if (pPageLayoutControl == null)
                {
                    MessageBox.Show("当前界面必须是PageLayoutControl界面!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
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

            try
            {
                //打印开始
                //allow the user to choose the page range to be printed
                pPrintDialog.AllowSomePages = true;
                //show the help button.
                pPrintDialog.ShowHelp = true;

                //set the Document property to the PrintDocument for which the PrintPage Event 
                //has been handled. To display the dialog, either this property or the 
                //PrinterSettings property must be set 
                pPrintDialog.Document = pPrintDocument;

                //show the print dialog and wait for user input
                DialogResult result = pPrintDialog.ShowDialog();

                // If the result is OK then print the document.
                if (result == DialogResult.OK) pPrintDocument.Print();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitializePrintPreviewDialog()
        {
            // create a new PrintPreviewDialog using constructor
            pPrintPreviewDialog = new PrintPreviewDialog();
            //set the size, location, name and the minimum size the dialog can be resized to
            pPrintPreviewDialog.ClientSize = new System.Drawing.Size(800, 600);
            pPrintPreviewDialog.Location = new System.Drawing.Point(29, 29);
            pPrintPreviewDialog.Name = "PrintPreviewDialog";
            pPrintPreviewDialog.MinimumSize = new System.Drawing.Size(375, 250);
            //set UseAntiAlias to true to allow the operating system to smooth fonts
            pPrintPreviewDialog.UseAntiAlias = true;

            //associate the event-handling method with the document's PrintPage event
            this.pPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Document_PrintPage);
        }
        private void Document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //string sPageToPrinterMapping = (string)this.comboBox1.SelectedItem;
            string sPageToPrinterMapping = "esriPageMappingTile";
            if (sPageToPrinterMapping == null)
                pPageLayoutControl.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
            else if (sPageToPrinterMapping.Equals("esriPageMappingTile"))
                pPageLayoutControl.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
            else if (sPageToPrinterMapping.Equals("esriPageMappingCrop"))
                pPageLayoutControl.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingCrop;
            else if (sPageToPrinterMapping.Equals("esriPageMappingScale"))
                pPageLayoutControl.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale;
            else
                pPageLayoutControl.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;

            try
            {
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
        #endregion
    }
}
