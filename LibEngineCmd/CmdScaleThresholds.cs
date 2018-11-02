using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdScaleThresholds.
    /// </summary>
    [Guid("0fe355e3-a30f-4f1c-971e-a42d785ef598")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdScaleThresholds")]
    public sealed class CmdScaleThresholds : BaseCommand, ICommandSubType
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
        private IMapControl3 m_mapControl;
        private long m_subType;

        public CmdScaleThresholds()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "";  //localizable text
            base.m_message = "设置显示阈值";  //localizable text 
            base.m_toolTip = "设置显示阈值";  //localizable text 
            base.m_name = "CustomCE.CmdScaleThresholds";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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

        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            ILayer layer = ClsGlobal.GetSelectedLayer(m_hookHelper);
            if (m_subType == 1) layer.MaximumScale = m_mapControl.MapScale;
            if (m_subType == 2) layer.MinimumScale = m_mapControl.MapScale;
            if (m_subType == 3)
            {
                layer.MaximumScale = 0;
                layer.MinimumScale = 0;
            }
            m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
        public int GetCount()
        {
            return 3;
        }

        public void SetSubType(int SubType)
        {
            m_subType = SubType;
        }

        public override string Caption
        {
            get
            {
                if (m_subType == 1) return "设置为最大比例尺";
                else if (m_subType == 2) return "设置为最小比例尺";
                else return "清除比例尺限制";
            }
        }

        public override bool Enabled
        {
            get
            {
                 bool enabled = true;
                ILayer layer = ClsGlobal.GetSelectedLayer(m_hookHelper);

                if (m_subType == 3)
                {
                    try
                    {
                        if ((layer.MaximumScale == 0) & (layer.MinimumScale == 0)) enabled = false;
                    }
                    catch
                    {
                        return false;
                    }
                }
                return enabled;
            }
        }
        #endregion
    }
}
