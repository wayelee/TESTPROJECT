using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Carto;
using stdole;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdMapGrid.
    /// </summary>
    [Guid("f7bc9e82-b113-4e11-8259-c78b78a8f74d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdMapGrid")]
    public sealed class CmdMapGrid : BaseCommand
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
        string[] SymbolStyle;
        public CmdMapGrid(string[] symbolstyle)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "地图格网";  //localizable text
            base.m_message = "地图格网";  //localizable text 
            base.m_toolTip = "地图格网";  //localizable text 
            base.m_name = "CustomCE.CmdMapGrid";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            SymbolStyle = symbolstyle;
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
            // TODO: Add CmdGrids.OnClick implementation
            IPageLayoutControl3 pPageLayoutControl = null;

            if (m_hookHelper.Hook is IToolbarControl)
            {
                pPageLayoutControl = (IPageLayoutControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
            }
            //In case the container is MapControl
            else if (m_hookHelper.Hook is IPageLayoutControl3)
            {
                pPageLayoutControl = (IPageLayoutControl3)m_hookHelper.Hook;
            }
            else
            {
                MessageBox.Show("当前界面必须是PageLayoutControl界面!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                LibCerMap.FrmNavigationGrids pFrmGrids = new LibCerMap.FrmNavigationGrids(SymbolStyle, m_hookHelper);
                pFrmGrids.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                GC.Collect();
            }
            
        }

        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.ActiveView is IPageLayout)
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
