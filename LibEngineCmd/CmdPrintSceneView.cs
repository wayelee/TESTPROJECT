using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Analyst3D;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdPrintSceneView.
    /// </summary>
    [Guid("4533b3d8-f8fa-48f6-8a7e-fb825852c8fe")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdPrintSceneView")]
    public sealed class CmdPrintSceneView : BaseCommand
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
        ISceneControl pSceneControl=null;
        public CmdPrintSceneView(ISceneControl scenecontrol)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "导出图片";  //localizable text
            base.m_message = "导出三维场景图片";  //localizable text 
            base.m_toolTip = "导出图片";  //localizable text 
            base.m_name = "CustomCE.CmdPrintSceneView";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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

            pSceneControl = scenecontrol;
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
            // TODO: Add CmdPrintSceneView.OnClick implementation
          
            LibCerMap.FrmExportActiveViewFig frm = new LibCerMap.FrmExportActiveViewFig(pSceneControl);
            frm.ShowDialog();
        }

        /// <summary>
        /// Enabled
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.Hook is IToolbarControl)
                {
                    object o = ((IToolbarControl)m_hookHelper.Hook).Buddy;
                    if (o is ISceneControl)
                    {
                        ISceneControl scene = o as ISceneControl;
                        if (scene.Scene != null && scene.Scene.LayerCount > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }



        #endregion
    }
}
