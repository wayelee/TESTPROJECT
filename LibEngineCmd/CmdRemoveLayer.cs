using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using DevComponents.DotNetBar;
using System.Data;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdRemoveLayer.
    /// </summary>
    [Guid("4dec4827-4a52-47be-9d4f-08b67e56e423")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("LibEngineCmd.CmdRemoveLayer")]
    public sealed class CmdRemoveLayer : BaseCommand
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
        private Bar m_pBar = null;
        private DataTable m_pDataTable = null;
        //Record selected map and layer
        ITOCControlDefault m_pTocCtl = null;
        IBasicMap m_pMap = null;
        ILayer m_pLayer = null;
        public CmdRemoveLayer(ITOCControlDefault tocCtl,Bar bar, DataTable table)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "ÒÆ³ýÍ¼²ã";  //localizable text
            base.m_message = "ÒÆ³ýÍ¼²ã";  //localizable text 
            base.m_toolTip = "ÒÆ³ýÍ¼²ã";  //localizable text 
            base.m_name = "CustomCE.CmdRemoveLayer";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            m_pTocCtl = tocCtl;
            m_pBar = bar;
            m_pDataTable = table;
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
            ClsGlobal.GetSelectedMapAndLayer(m_pTocCtl, ref m_pMap, ref m_pLayer);

            if (m_pMap != null && m_pLayer != null)
            {
                m_pMap.DeleteLayer(m_pLayer);
               
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                m_pTocCtl.Update();
                if (m_pBar.Visible == true && m_pBar.Text == m_pLayer.Name)
                {
                    m_pDataTable.Clear();
                    m_pDataTable.Columns.Clear();
                    m_pBar.Visible = false;
                }
            }
        }
        /// <summary>
        /// Occurs when this command is update
        /// </summary>
        public override bool Enabled
        {
            get
            {
                ClsGlobal.GetSelectedMapAndLayer(m_pTocCtl, ref m_pMap, ref m_pLayer);
                if (m_pLayer != null)
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
