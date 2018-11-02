using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoAnalyst;
using LibCerMap;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdProfileAnalysis.
    /// </summary>
    [Guid("85500f8e-2161-4329-b7f8-dd659963a5c1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdProfileAnalysis")]
    public sealed class CmdProfileAnalysis : BaseCommand
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
        public IRasterLayer pRasterLayer;

        public CmdProfileAnalysis()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "栅格剖面分析";  //localizable text
            base.m_message = "栅格剖面分析";  //localizable text 
            base.m_toolTip = "栅格剖面分析";  //localizable text 
            base.m_name = "CustomCE.CmdProfileAnalysis";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
        /// Enabled
        /// </summary>
        public override bool Enabled
        {
            get
            {
                IMapControl3 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;
                if (pMapCtr == null)
                {
                    return false;
                }

                if (pMapCtr.Map.SelectionCount > 0)
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
            IMapControl3 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;
            ISelection pSelection = pMapCtr.Map.FeatureSelection;

            int i = 0;
            IEnumFeature pEnumFeature = pSelection as IEnumFeature;
            IEnumFeatureSetup m_EnumFeatureSetup = pEnumFeature as IEnumFeatureSetup;
            m_EnumFeatureSetup.AllFields = true;
            pEnumFeature.Reset();
            IFeature pFeature = pEnumFeature.Next();
            if (pFeature != null)
            {
                while (pFeature != null)
                {
                    i++;
                    pFeature = pEnumFeature.Next();
                }
                if (i == 1)
                {
                    pEnumFeature.Reset();
                    IFeature mFeature = pEnumFeature.Next();
                    Frmprofile frmprofile = new Frmprofile(mFeature, pMapCtr);
                    frmprofile.ShowInTaskbar = false;
                    frmprofile.StartPosition = FormStartPosition.CenterScreen;
                    frmprofile.ShowDialog();
                }
                else
                {
                    MessageBox.Show("只能选中一条线路进行剖面分析", "提示", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("请先选中一条路线", "提示", MessageBoxButtons.OK);
            }
        }

        #endregion
    }
}
