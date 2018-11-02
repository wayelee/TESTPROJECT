using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;


namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdJoinAttribute.
    /// </summary>
    [Guid("ffa7f81a-938b-4ef0-9988-376ebcb8f438")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdJoinAttribute")]
    public sealed class CmdJoinAttribute : BaseCommand
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
        //public ILayer m_pLayer = null;

        public AxMapControl pMapControl;
        public DevComponents.DotNetBar.Bar m_barTable;
        public System.Data.DataTable m_AttributeTable;
        public DevComponents.DotNetBar.Controls.DataGridViewX m_gridfield;
        public DevComponents.DotNetBar.DockContainerItem m_docktable;

        public CmdJoinAttribute()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "joinattribute"; //localizable text
            base.m_caption = "连接属性表";  //localizable text
            base.m_message = "连接属性表";  //localizable text 
            base.m_toolTip = "连接属性表";  //localizable text 
            base.m_name = "CustomCE.CmdJoinAttribute";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdJoinAttribute.OnClick implementation

            ILayer layer = ClsGlobal.GetSelectedLayer(m_hookHelper);
            if (layer is IFeatureLayer)
            {
                LibCerMap.FrmJoinAttribute frm = new LibCerMap.FrmJoinAttribute(pMapControl, layer, m_barTable, m_AttributeTable, m_gridfield, m_docktable);
                frm.ShowDialog();
            }
            else 
            {
                MessageBox.Show( "只有矢量图层才能进行数据表连接", "提示",MessageBoxButtons.OK);
            }
        }

        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.Hook is IMapControl3 || m_hookHelper.Hook is IPageLayoutControl3)
                {
                    ILayer layer = ClsGlobal.GetSelectedLayer(m_hookHelper);
                    if (layer == null) { return false; }

                    if (layer is IFeatureLayer)
                    {
                        if (((IFeatureLayer)layer).FeatureClass != null)
                        {
                            return true;
                        }
                    }
                        return false;
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
