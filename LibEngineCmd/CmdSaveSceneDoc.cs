using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.SystemUI;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdSaveSceneDoc.
    /// </summary>
    [Guid("d5018a56-295a-43fb-99f2-1195d0bca99f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdSaveSceneDoc")]
    public sealed class CmdSaveSceneDoc : BaseCommand
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
        ISceneControl pSceneControl;
        public CmdSaveSceneDoc(ISceneControl scenecontrol)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "保存三维工作空间";  //localizable text
            base.m_message = "保存三维工作空间";  //localizable text 
            base.m_toolTip = "保存三维工作空间";  //localizable text 
            base.m_name = "CustomCE.CmdSaveSceneDoc";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdSaveSceneDoc.OnClick implementation

            for (int i = 0; i < pSceneControl.Scene.LayerCount; i++)
            {
                 ILayer pLayer = pSceneControl.Scene.get_Layer(i);
                 if (pLayer == pSceneControl.Scene.ActiveGraphicsLayer)
                 {
                     pSceneControl.Scene.DeleteLayer(pLayer);
                     pSceneControl.Scene.SceneGraph.RefreshViewers();
                 }
            }
            try
            {
                //文件名称
                string pSaveFileName = "";
                SaveFileDialog pSaveDoc = new SaveFileDialog();
                pSaveDoc.Title = "保存工作空间";
                pSaveDoc.Filter = "ArcScene Document(*.sxd)|*.sxd";
                if (pSaveDoc.ShowDialog() == DialogResult.OK)
                {
                    pSaveFileName = pSaveDoc.FileName;
                }

                IMemoryBlobStream pMemoryBlobStream = new MemoryBlobStreamClass();
                IObjectStream pObjectStream = new ObjectStreamClass();
                pObjectStream.Stream = pMemoryBlobStream;

                IPersistStream pPersistStream = pSceneControl.Scene as IPersistStream;

                pPersistStream.Save(pObjectStream, 1);

                pMemoryBlobStream.SaveToFile(pSaveFileName);
            }
            catch
            {
                
            }
           
        }
        #endregion
    }
}
