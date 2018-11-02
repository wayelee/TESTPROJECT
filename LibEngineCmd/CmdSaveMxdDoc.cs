using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdSaveMxdDoc.
    /// </summary>
    [Guid("428c5bf0-94ad-40dd-b02b-2f5f3b2632e9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdSaveMxdDoc")]
    public sealed class CmdSaveMxdDoc : BaseCommand
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

        public CmdSaveMxdDoc()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "保存地图文档";  //localizable text
            base.m_message = "保存地图文档";  //localizable text 
            base.m_toolTip = "保存地图文档";  //localizable text 
            base.m_name = "CustomCE.CmdSaveMxdDoc";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
                EngineEditor engineEditor = new EngineEditorClass();
                if (engineEditor.EditState == esriEngineEditState.esriEngineStateEditing)
                {
                    return false;
                }
                if (m_hookHelper.ActiveView is IPageLayout || m_hookHelper.ActiveView is IMap)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
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
                string strDocName = pPageLayoutControl.DocumentFilename;
                if (strDocName != null)
                {
                    if (pPageLayoutControl.CheckMxFile(strDocName))
                    {
                        //create a new instance of a MapDocument
                        IMapDocument mapDoc = new MapDocumentClass();
                        mapDoc.Open(strDocName, string.Empty);

                        //Make sure that the MapDocument is not readonly
                        if (mapDoc.get_IsReadOnly(strDocName))
                        {
                            MessageBox.Show("Map document is read only!");
                            mapDoc.Close();
                            return;
                        }

                        //Replace its contents with the current map
                        mapDoc.ReplaceContents((IMxdContents)pPageLayoutControl.PageLayout);

                        //save the MapDocument in order to persist it
                        mapDoc.Save(true, true);
                        
                        //close the MapDocument
                        mapDoc.Close();
                    }
                }
                else
                {
                    IMapDocument mDoc = new MapDocumentClass(); //地图文档对象
                    SaveFileDialog dlgFile = new SaveFileDialog();//以对话框选择文档路径
                    string sFilePath;
                    dlgFile.Title = "另存地图文档";
                    dlgFile.Filter = "Map Documents(*.mxd)|*.mxd";
                    if (dlgFile.ShowDialog() == DialogResult.OK)
                    {
                        sFilePath = dlgFile.FileName;
                        mDoc.New(sFilePath);
                        mDoc.ReplaceContents((IMxdContents)pPageLayoutControl.PageLayout);
                        mDoc.Save(true, true);
                        pPageLayoutControl.DocumentFilename = sFilePath;
                        mDoc.Close();
                        
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
 
        #endregion
    }
}
