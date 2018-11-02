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
    /// Summary description for CmdExportSHP.
    /// </summary>
    [Guid("bb8627c2-f4bd-4792-a9c4-966166e79b5b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdExportSHP")]
    public sealed class CmdExportSHP : BaseCommand
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
        private ITOCControlDefault m_pTocCtl = null;
        public CmdExportSHP(ITOCControlDefault tocCtl)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "导出数据";  //localizable text
            base.m_message = "导出数据";  //localizable text 
            base.m_toolTip = "导出数据";  //localizable text 
            base.m_name = "CustomCE.CmdExportSHP";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            m_pTocCtl  = tocCtl;
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
            // TODO: Add CmdExportSHP.OnClick implementation
            IBasicMap map = null;
            ILayer layer = null;
            ClsGlobal.GetSelectedMapAndLayer(m_pTocCtl, ref map, ref layer);
            if (layer is IFeatureLayer)
            {
                LibCerMap.FrmExportSHP dlg = new LibCerMap.FrmExportSHP(map,layer);
                dlg.StartPosition = FormStartPosition.CenterScreen;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    IActiveView activiView = map as IActiveView;
                    activiView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    m_pTocCtl.Update();
                }
            }
            else if (layer is IRasterLayer)
            {
                LibCerMap.FrmExportRaster frm = new LibCerMap.FrmExportRaster(map,layer);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    IActiveView activiView = map as IActiveView;
                    activiView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    m_pTocCtl.Update();
                }
            }           
        }

       #endregion
    }
}
