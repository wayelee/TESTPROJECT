using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using LibCerMap;
namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdViewLinkTabale.
    /// </summary>
    [Guid("6ae4225c-aa8f-4ed0-8b90-650744cdf6d0")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdViewLinkTabale")]
    public sealed class CmdViewIMUCentelineMappingTable : BaseCommand
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
        public ToolNewIMUToCenterlineMapping m_ToolAddControlPoints;

        public CmdViewIMUCentelineMappingTable()
        {
            //
            // TODO: Define values for the public properties
            //
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "查看控制点";  //localizable text 
            base.m_message = "查看控制点";  //localizable text
            base.m_toolTip = "查看控制点";  //localizable text
            base.m_name = "CustomCE.CmdViewIMUCentelineMappingTable";   //unique id, non-localizable (e.g. "MyCategory_MyTool")

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
            // TODO: Add CmdViewLinkTabale.OnClick implementation
            m_ToolAddControlPoints.FrmVectorLinkTable.Show();
            //m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        public override bool Enabled
        {
            get
            {
                if (m_ToolAddControlPoints != null)
                {
                    if (m_hookHelper.ActiveView is IMap && m_ToolAddControlPoints.Enabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
