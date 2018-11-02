using System;
using System.Drawing;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using LibCerMap;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdSaveAsMxdDoc.
    /// </summary>
    [Guid("73a9d5ec-1719-4db1-814a-7a686fa7dbbf")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdSaveAsMxdDoc")]
    public sealed class CmdSaveAsMxdDoc : BaseCommand
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
        public IMapDocument m_pDoc = null;

        private IHookHelper m_hookHelper;

        public CmdSaveAsMxdDoc()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "另存地图文档";  //localizable text
            base.m_message = "另存地图文档";  //localizable text 
            base.m_toolTip = "另存地图文档";  //localizable text 
            base.m_name = "CustomCE.CmdSaveAsMxdDoc";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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

            SaveFileDialog dlgFile = new SaveFileDialog();//以对话框选择文档路径
            dlgFile.Title = "另存地图文档";
            dlgFile.Filter = "Map Documents(*.mxd)|*.mxd";
            string sFilePath = string.Empty;
            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                sFilePath = dlgFile.FileName;
                ClsGDBDataCommon cls = new ClsGDBDataCommon();
                m_pDoc =cls.SaveAsDocument(pPageLayoutControl, sFilePath);
            }
            
        }

        //public IMapDocument SaveAsDocument(IPageLayoutControl3 pageCtl,string sFilePath)
        //{
        //    try
        //    {
        //        m_pDoc = new MapDocumentClass(); //地图文档对象
        //        m_pDoc.New(sFilePath);
        //        m_pDoc.ReplaceContents((IMxdContents)pageCtl.PageLayout);

        //        m_pDoc.Save(true, true);
        //        pageCtl.DocumentFilename = sFilePath;
        //        //m_pDoc.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("另存地图文档失败", ex.Message);
        //        return null;
        //    }

        //    return m_pDoc;
        //}

        #endregion
    }
}
