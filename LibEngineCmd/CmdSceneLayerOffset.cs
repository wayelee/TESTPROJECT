using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;
using LibCerMap;
using ESRI.ArcGIS.Analyst3D;
namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdTinSaveAs.
    /// </summary>
    [Guid("f2ffac7c-baa3-45df-969f-e93e9d385bcc")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdSceneLayerOffset")]
    public sealed class CmdSceneLayerOffset : BaseCommand
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
        //目标图层
        public ILayer pLayer;
        public ISceneControl pSceneCtr = null;
       // public IScene Iscene;

        public CmdSceneLayerOffset()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "图层高度偏移";  //localizable text 
            base.m_message = "图层高度偏移";  //localizable text
            base.m_toolTip = "图层高度偏移";  //localizable text
            base.m_name = "CustomCE.CmdSceneLayerOffset";   //unique id, non-localizable (e.g. "MyCategory_MyTool")

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
              //  ISceneControl pSceneCtr = null;
                //ISceneControl pSceneCtr = (((IToolbarMenu)m_hookHelper.Hook).Hook) as ISceneControl;
                if (pSceneCtr == null)
                {
                    return false;
                }
                //if (pSceneCtr != null)
                //{
                //    pLayer = pSceneCtr.CustomProperty as ILayer;
                //    if (pLayer == null)
                //    {
                //        return false;
                //    }
                //}
                if (pLayer == null)
                {
                    return false;
                }
                return true;
                 
            }
        }
        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdTinSaveAs.OnClick implementation
            //SaveFileDialog dlg = new SaveFileDialog();
           // dlg.Filter = "";
            FrmSceneLayerOffest frm = new FrmSceneLayerOffest();
           // ISceneControl pSceneCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as ISceneControl;
            frm.m_pSceneCtrl = pSceneCtr;
            frm.pLayer = pLayer;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //ITin pTin = pTinLayer.Dataset;
                //ITinEdit pTinEdit = pTin as ITinEdit;
                //try
                //{
                //    object ob = true;
                //    pTin.SaveAs(dlg.FileName, ref ob);
                //    MessageBox.Show("保存"+dlg.FileName+"成功！");
                //}
                //catch (SystemException ee)
                //{
                //    MessageBox.Show(ee.Message);
                //}
            }
        }

        #endregion
    }
}
