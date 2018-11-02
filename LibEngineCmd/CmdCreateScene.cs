using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using LibCerMap;
using System.Windows.Forms;
namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdCreateScene.
    /// </summary>
    [Guid("bbb5c8e7-d548-45c3-a137-a301cecef395")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdCreateScene")]
    public sealed class CmdCreateScene : BaseCommand
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
        private IMapControl2 pMapCtr;
        public CmdCreateScene(IMapControl2 pMapc)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "生成场景";  //localizable text
            base.m_message = "生成场景";  //localizable text 
            base.m_toolTip = "生成场景";  //localizable text 
            base.m_name = "CustomCE.CmdCreateScene";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            pMapCtr = pMapc;

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
            // TODO: Add CmdCreateScene.OnClick implementation
            FrmGenerateScene frm = new FrmGenerateScene();
            frm.m_pMapCtrl = pMapCtr as IMapControl2;
            frm.m_pSceneCtrl = (((IToolbarControl)m_hookHelper.Hook).Buddy) as ISceneControl;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // bar3.sel;
                try
                {
                    ////barMain.SelectedDockTab = 2;
                    //for (int i = 0; i < barMain.Items.Count; i++)
                    //{
                    //    if (barMain.Items[i].Equals(dock3D))
                    //    {
                    //        barMain.SelectedDockTab = i;
                    //    }
                    //}

                    ControlsSceneFullExtentCommandClass CFC = new ControlsSceneFullExtentCommandClass();
                    CFC.OnCreate(frm.m_pSceneCtrl);
                    CFC.OnClick();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Enabled
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (pMapCtr.Map.LayerCount > 0)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion
    }
}
