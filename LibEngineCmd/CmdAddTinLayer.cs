using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using System.Windows.Forms;
using System.IO;
namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdAddTinLayer.
    /// </summary>
    [Guid("dba75fff-0e60-42ac-abc7-8a3931f3eecf")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdAddTinLayer")]
    public sealed class CmdAddTinLayer : BaseCommand
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

        public CmdAddTinLayer()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "Ìí¼ÓTINÍ¼²ã";  //localizable text 
            base.m_message = "Ìí¼ÓTINÍ¼²ã";  //localizable text
            base.m_toolTip = "Ìí¼ÓTINÍ¼²ã";  //localizable text
            base.m_name = "CustomCE.CmdAddTinLayer";   //unique id, non-localizable (e.g. "MyCategory_MyTool")

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

        public override bool Enabled
        {
            get
            {
                // TODO: Add CmdNorthArrow.OnClick implementation   
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr == null)
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
            // TODO: Add CmdAddTinLayer.OnClick implementation
            FolderBrowserDialog fdlg = new FolderBrowserDialog();            
            //fdlg.SelectedPath = @"C:\Users\Administrator\Desktop\sampleAnalysis\TIN\";
            try
            {
                if (fdlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                //FrmFolderDialog dlg = new FrmFolderDialog();
                //dlg.DisplayDialog();
                if (fdlg.SelectedPath != "")
                {
                    // DirectoryInfo dir = Directory.CreateDirectory(dlg.Path);
                    DirectoryInfo dir = Directory.CreateDirectory(fdlg.SelectedPath);
                    IWorkspaceFactory pWSFact = new TinWorkspaceFactoryClass();
                    IWorkspace pWS = pWSFact.OpenFromFile(dir.Parent.FullName + @"\", 0);
                    ITinWorkspace pTinWS = pWS as ITinWorkspace;
                    ITin pTin = pTinWS.OpenTin(dir.Name);
                    //½«TIN±äÎªTINÍ¼²ã
                    ITinLayer pTinLayer = new TinLayerClass();
                    pTinLayer.Dataset = pTin;
                    pTinLayer.Name = dir.Name;
                    IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                    pMapCtr.Map.AddLayer((ILayer)pTinLayer);
                }
            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        #endregion
    }
}
