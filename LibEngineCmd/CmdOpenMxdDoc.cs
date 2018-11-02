using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using LibCerMap;
using ESRI.ArcGIS.SystemUI;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdOpenMxdDoc.
    /// </summary>
    [Guid("e914face-de43-4c3b-94cc-fc407dd00230")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdOpenMxdDoc")]
    public sealed class CmdOpenMxdDoc : BaseCommand
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

        public DevComponents.DotNetBar.Bar m_barTable;
        public System.Data.DataTable m_AttributeRemove;
        public AxMapControl pMapControl;

        private LibCerMap.ControlsSynchronizer m_controlsSynchronizer = null;
        private string m_strDocName = string.Empty;

        public CmdOpenMxdDoc(LibCerMap.ControlsSynchronizer controlsSynchronizer,DevComponents.DotNetBar.Bar barTable, System.Data.DataTable AttributeRemove,string docName)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "打开地图文档";  //localizable text
            base.m_message = "打开地图文档";  //localizable text 
            base.m_toolTip = "打开地图文档";  //localizable text 
            base.m_name = "CustomCE.CmdOpenMxdDoc";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);

                m_controlsSynchronizer = controlsSynchronizer;
                m_barTable = barTable;
                m_AttributeRemove = AttributeRemove;
                m_strDocName = docName;
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
            IPageLayoutControl3 pPageLayoutControl = null;        
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                if (string.IsNullOrEmpty(m_strDocName))
                {
                    OpenFileDialog dlgFile = new OpenFileDialog();//以对话框选择文档路径 
                    dlgFile.Title = "打开一个地图文档";
                    dlgFile.Filter = "Map Documents(*.mxd)|*.mxd";
                    if (dlgFile.ShowDialog() == DialogResult.OK)
                    {
                        m_strDocName = dlgFile.FileName;
                    }
                }
                IMapDocument mapDoc = new MapDocumentClass(); //地图文档对象 
                IMap map = null;
                if (mapDoc.get_IsPresent(m_strDocName) && !mapDoc.get_IsPasswordProtected(m_strDocName))
                {
                    mapDoc.Open(m_strDocName, string.Empty);

                     IMaps maps = new Maps();
                     for (int i = 0; i < mapDoc.MapCount;i++ )
                     {
                         maps.Add(mapDoc.get_Map(i));
                     }
                    // set the first map as the active view
                    map = mapDoc.get_Map(0);
                    mapDoc.SetActiveView((IActiveView)map);

                    m_controlsSynchronizer.PageLayoutControl.PageLayout = mapDoc.PageLayout;
                    m_controlsSynchronizer.PageLayoutControl.Printer = mapDoc.Printer;
                    m_controlsSynchronizer.PageLayoutControl.DocumentFilename = m_strDocName;
                    //m_controlsSynchronizer.ReplaceMap(map);
                    m_controlsSynchronizer.AddNewMaps(maps);
                    
                    mapDoc.Close();
                   
                    m_strDocName = string.Empty;
                }
               
                /////////////////////////////
                //关闭属性表
                if (m_barTable.Visible == true)
                {
                    if (m_AttributeRemove!=null)
                    {
                        m_AttributeRemove.Clear();
                        m_AttributeRemove.Columns.Clear();
                        m_barTable.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打开地图文档失败");
            }
        }

        #endregion
    }
}
