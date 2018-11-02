using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

using LibCerMap;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdLabelDesign.
    /// </summary>
    [Guid("eed838b9-4c7a-4869-8011-8752635cdf05")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdLabelDesign")]
    public sealed class CmdLabelDesign : BaseCommand
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

        public AxMapControl m_Mapcontrol;
        public AxTOCControl m_Toccontrol;
        public AxSceneControl m_scenecontrol;
        public ILayer m_Layer;

        public CmdLabelDesign()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "Í¼²ã±ê×¢";  //localizable text
            base.m_message = "Í¼²ã±ê×¢";  //localizable text 
            base.m_toolTip = "Í¼²ã±ê×¢";  //localizable text 
            base.m_name = "CustomCE.CmdLabelDesign";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdLabelDesign.OnClick implementation
            LibCerMap.FrmLabelDesign frmlaebel = new LibCerMap.FrmLabelDesign(m_Mapcontrol,m_Toccontrol,m_Layer,m_scenecontrol);
            frmlaebel.Show();
            frmlaebel.StartPosition = FormStartPosition.CenterScreen;
            frmlaebel.ShowInTaskbar = false;
        }
        public override bool Enabled
        {
            get
            {

                bool enable = false;
                m_Layer = ClsGlobal.GetSelectedLayer(m_hookHelper);
                if (m_Layer is IFeatureLayer)
                {
                    enable = true;
                }
                else if (m_Layer is IRasterLayer)
                {
                    enable = false;
                }
                return enable;
            }
        }

       
        #endregion
    }
}
