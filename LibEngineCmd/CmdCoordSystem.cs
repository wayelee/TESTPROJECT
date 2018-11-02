using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdCoordSystem.
    /// </summary>
    [Guid("bd95776d-5ed8-4b3a-a482-7f93f1983a01")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdCoordSystem")]
    public sealed class CmdCoordSystem : BaseCommand
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
        public IMapControl3 m_pMapControl;
        public IMapControl3 m_pMapCtlHide;//用于记录导入图层的空间参考，传入的时主窗体中axMapControlHide

        public CmdCoordSystem(IMapControl3 mapCtl,IMapControl3 mapCtlHide)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "地图坐标系统";  //localizable text
            base.m_message = "地图坐标系统";  //localizable text 
            base.m_toolTip = "地图坐标系统";  //localizable text 
            base.m_name = "CustomCE.CmdCoordSystem";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            m_pMapControl = mapCtl;
            m_pMapCtlHide = mapCtlHide;
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

        public override bool Enabled
        {
            get
            {
                // TODO: Add CmdNorthArrow.OnClick implementation   
                IMapControl3 mapcontrol3 = ClsGlobal.GetMapControl(m_hookHelper);
                if (mapcontrol3 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdCoordSystem.OnClick implementation
            LibCerMap.FrmCoordinateSystem frmCoordSystem = new LibCerMap.FrmCoordinateSystem(m_pMapControl, m_pMapCtlHide);
            if (frmCoordSystem.ShowDialog() ==DialogResult.OK )
            {
                m_pMapControl.SpatialReference = frmCoordSystem.pSpaReference;
            }
        }

        #endregion
    }
}
