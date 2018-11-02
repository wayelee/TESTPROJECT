using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdViewLinkTable.
    /// </summary>
    [Guid("a5a919bf-3c1c-4ade-a1cf-d7290a987951")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdViewLinkTable")]
    public sealed class CmdViewLinkTable : BaseCommand
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
        private ToolNewDisplacement m_ToolNewDisplacement;

        public override bool Enabled
        {
            get
            {
                // TODO: Add CmdNorthArrow.OnClick implementation   
                if (m_ToolNewDisplacement == null)
                {
                    return false;
                }
                if (m_ToolNewDisplacement.VectorLinkTable.OriginPoints.PointCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ToolNewDisplacement NewDisplacement
        {
            get
            {
                return m_ToolNewDisplacement;
            }

            set
            {
                m_ToolNewDisplacement = value;
            }
        }

        public CmdViewLinkTable()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "查看控制点";  //localizable text
            base.m_message = "查看控制点";  //localizable text 
            base.m_toolTip = "查看控制点";  //localizable text 
            base.m_name = "CustomCE.CmdViewLinkTable";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdViewLinkTable.OnClick implementation
            if (m_ToolNewDisplacement != null)
            {
                m_ToolNewDisplacement.VectorLinkTable.Show();
            }
        }

        #endregion
    }
}
