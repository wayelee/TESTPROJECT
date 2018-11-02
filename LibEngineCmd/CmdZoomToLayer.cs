using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdZoomToLayer.
    /// </summary>
    [Guid("7f4bc04b-d6e2-4934-afdf-8542dcf7e995")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdZoomToLayer")]
    public sealed class CmdZoomToLayer : BaseCommand
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
        ITOCControlDefault m_pTocCtl = null;
        public CmdZoomToLayer(ITOCControlDefault tocCtl)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "LibEngineCmd"; //localizable text
            base.m_caption = "缩放至图层";  //localizable text
            base.m_message = "缩放至图层";  //localizable text 
            base.m_toolTip = "缩放至图层";  //localizable text 
            base.m_name = "CustomCE.CmdZoomToLayer";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            m_pTocCtl = tocCtl;
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
            IBasicMap map = null;
            ILayer layer = null;
            ClsGlobal.GetSelectedMapAndLayer(m_pTocCtl, ref map, ref layer);
            IActiveView activeView = map as IActiveView;
            activeView.Extent = layer.AreaOfInterest;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        #endregion

    }
   
}
