using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Collections.Generic;
using LibCerMap;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdSunAltProperty.
    /// </summary>
    [Guid("37adf1ac-e638-45cc-8e30-adc4cd53a3b3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdSunAltProperty")]
    public sealed class CmdSunAltProperty : BaseCommand
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

        public FrmSunAltitude m_frmSunAltitude=new FrmSunAltitude();
        public ToolSunAltCheckState m_toolSunAltCheckState = null;
        
        public override bool Enabled
        {
            get
            {
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if(pMapCtr.CurrentTool is ToolSunAltCheckState)
                    return true;
                else
                    return false;
            }
        }

        public CmdSunAltProperty()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "查看高度角信息";  //localizable text
            base.m_message = "查看高度角信息";  //localizable text 
            base.m_toolTip = "查看高度角信息";  //localizable text 
            base.m_name = "CustomCE.CmdSunAltProperty";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdSunAltProperty.OnClick implementation
            if (m_frmSunAltitude != null)
            {
                m_frmSunAltitude.Show();
                m_frmSunAltitude.updateInfoList();
            }
        }        

        #endregion
    }
}
