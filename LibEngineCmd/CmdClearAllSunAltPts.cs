using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using LibCerMap;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdClearAllSunAltPts.
    /// </summary>
    [Guid("fe7c485b-e656-4789-aac1-4baa8fe8aa0f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdClearAllSunAltPts")]
    public sealed class CmdClearAllSunAltPts : BaseCommand
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

        public List<ClsPointInfo> m_sunAltPts = null;
        public FrmSunAltitude m_frmAltitude = null;

        public override bool Enabled
        {
            get
            {
                if (m_sunAltPts == null)
                    return false;

                return (m_sunAltPts.Count != 0);
            }
        }

        public CmdClearAllSunAltPts()
        {
            //
            // TODO: Define values for the public properties
            //
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "Çå¿Õ";  //localizable text
            base.m_message = "Çå¿Õ";  //localizable text 
            base.m_toolTip = "Çå¿Õ";  //localizable text 
            base.m_name = "CustomCE.CmdClearAllSunAltPts";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdClearAllSunAltPts.OnClick implementation
            if (m_sunAltPts != null)
            {
                m_sunAltPts.Clear();
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }

            if (m_frmAltitude != null)
            {
                m_frmAltitude.updateInfoList();
            }
        }

        #endregion
    }
}
