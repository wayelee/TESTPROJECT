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
using LibCerMap;


namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdPageSetup.
    /// </summary>
    /// 
    //页面设置
    [Guid("3755f8d7-64ca-4341-9aaf-57bda11a1568")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdPageSetup")]
    public sealed class CmdPageSetup : BaseCommand
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
 
        internal PageSetupDialog pPageSetupDialog;
        //declare a PrintDocument object named document, to be displayed in the print preview
        public System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();

        public CmdPageSetup(System.Drawing.Printing.PrintDocument M_document)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "页面设置";  //localizable text
            base.m_message = "页面设置";  //localizable text 
            base.m_toolTip = "页面设置";  //localizable text 
            base.m_name = "CustomCE.CmdPageSetup";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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

            document = M_document;
            //页面设置初始化
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
            // TODO: Add CmdPageSetup.OnClick implementation
            
            IPageLayoutControl3 pPageLayoutControl = null;
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

            FrmPageSetup frmPage = new FrmPageSetup(pPageLayoutControl);
            if (frmPage.ShowDialog() == DialogResult.OK)
            {
                return;
            }

#region 

            //try
            //{
            //    //开始页面设置调用
            //    DialogResult result = pPageSetupDialog.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        //set the printer settings of the preview document to the selected printer settings
            //        document.PrinterSettings = pPageSetupDialog.PrinterSettings;
            //        //set the page settings of the preview document to the selected page settings
            //        document.DefaultPageSettings = pPageSetupDialog.PageSettings;

            //        //due to a bug in PageSetupDialog the PaperSize has to be set explicitly by iterating through the
            //        //available PaperSizes in the PageSetupDialog finding the selected PaperSize
            //        int i;
            //        IEnumerator paperSizes = pPageSetupDialog.PrinterSettings.PaperSizes.GetEnumerator();
            //        paperSizes.Reset();
            //        for (i = 0; i < pPageSetupDialog.PrinterSettings.PaperSizes.Count; ++i)
            //        {
            //            paperSizes.MoveNext();
            //            if (((PaperSize)paperSizes.Current).Kind == document.DefaultPageSettings.PaperSize.Kind)
            //                if (((PaperSize)paperSizes.Current).PaperName == document.DefaultPageSettings.PaperSize.PaperName)
            //            {
            //                document.DefaultPageSettings.PaperSize = ((PaperSize)paperSizes.Current);
            //            }
            //        }

            //        IPaper paper = new PaperClass(); //create a paper object
            //        IPrinter printer = new EmfPrinterClass(); //create a printer object
            //        paper.Attach(pPageSetupDialog.PrinterSettings.GetHdevmode(pPageSetupDialog.PageSettings).ToInt32(), pPageSetupDialog.PrinterSettings.GetHdevnames().ToInt32());

            //        //pass the paper to the emf printer
            //        printer.Paper = paper;
            //        //set the page layout control's printer to the currently selected printer
            //        pPageLayoutControl.Printer = printer;
            //        //pPageLayoutControl.PageLayout.Page
            //        pPageLayoutControl.ActiveView.Refresh();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
#endregion
        }

        private void InitializePageSetupDialog()
        {
            //create a new PageSetupDialog using constructor
            pPageSetupDialog = new PageSetupDialog();
            //initialize the dialog's PrinterSettings property to hold user defined printer settings
            pPageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();
            //initialize dialog's PrinterSettings property to hold user set printer settings
            pPageSetupDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            //do not show the network in the printer dialog
            pPageSetupDialog.ShowNetwork = false;
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
