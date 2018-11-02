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
    /// Summary description for CmdOpenSceneDoc.
    /// </summary>
    [Guid("c295f6b7-2a6c-4836-9c4b-71c256cca0bb")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdOpenSceneDoc")]
    public sealed class CmdOpenSceneDoc : BaseCommand
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
        public CmdOpenSceneDoc(ISceneControl scenecontrol)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "打开三维工作空间";  //localizable text
            base.m_message = "打开三维工作空间";  //localizable text 
            base.m_toolTip = "打开三维工作空间";  //localizable text 
            base.m_name = "CustomCE.CmdOpenSceneDoc";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdOpenSceneDoc.OnClick implementation
            //文件名称
                string pOpenFileName = "";
                OpenFileDialog pOpenDoc = new OpenFileDialog();
                pOpenDoc.Title = "打开三维空间";
                pOpenDoc.Filter = "ArcScene Document(*.sxd)|*.sxd";
                if (pOpenDoc.ShowDialog() == DialogResult.OK)
                {
                    pOpenFileName = pOpenDoc.FileName;
                }
                try
                {
                    if (pSceneControl.CheckSxFile(pOpenFileName))
                    {
                        pSceneControl.LoadSxFile(pOpenFileName);
                    }
                    else
                    {
                        IScene pScene = pSceneControl.Scene;
                        IObjectStream pObjectStream = new ObjectStreamClass();

                        IMemoryBlobStream pMemoryBlobStream = new MemoryBlobStreamClass();
                        pMemoryBlobStream.LoadFromFile(pOpenFileName);

                        pObjectStream.Stream = pMemoryBlobStream;

                        IPersistStream pPersistStream = pScene as IPersistStream;

                        pPersistStream.Load(pObjectStream);
                    }
                }
                catch
                {
                    //MessageBox.Show("文件选择为空或者选择格式不正确！");
                }
        }

        #endregion
    }
}
