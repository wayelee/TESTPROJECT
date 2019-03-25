using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using LibCerMap;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdSetAdjustData.
    /// </summary>
    [Guid("263a926b-9b23-4181-bf62-690dd5a76806")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdSetIMUCentlineData")]
    public sealed class CmdSetIMUCentlineData : BaseCommand
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
        private FrmCenterLineInsideLayerSetting m_FrmSetCenterLineInsideData = null;
        private ToolNewIMUToCenterlineMapping m_NewIMUToCenterlineMapping;
        public IFeatureLayer pIMULayer;
        public IFeatureLayer pCenterlinePointLayer;
        public IFeatureLayer pCenterlineLayer;

        public ToolNewIMUToCenterlineMapping NewIMUToCenterlineMappingTool
        {
            set
            {
                m_NewIMUToCenterlineMapping = value;
            }

            get
            {
                return m_NewIMUToCenterlineMapping;
            }
        }
       

        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.ActiveView is IMap)
                {
                    int nCount = 0;
                    IMapControl3 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;
                    for (int i = 0; i < pMapCtr.Map.LayerCount; i++)
                    {
                        if (pMapCtr.Map.get_Layer(i) is IFeatureLayer)
                        {
                            nCount++;
                        }
                    }

                    if (nCount == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public CmdSetIMUCentlineData()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "设置待对齐数据";  //localizable text
            base.m_message = "设置待对齐数据";  //localizable text 
            base.m_toolTip = "设置待对齐数据";  //localizable text 
            base.m_name = "CustomCE.CmdSetIMUCentlineData";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdSetAdjustData.OnClick implementation
            IMapControl3 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;

            m_FrmSetCenterLineInsideData = new FrmCenterLineInsideLayerSetting(pMapCtr);
            if (m_FrmSetCenterLineInsideData.ShowDialog() == DialogResult.OK)
            {

                if (m_NewIMUToCenterlineMapping.FrmVectorLinkTable.IMUFeatureList != null && m_NewIMUToCenterlineMapping.FrmVectorLinkTable.IMUFeatureList.Count > 0)
                {
                    if (MessageBox.Show("更新图层将清空的已添加特征点，是否继续?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                pIMULayer = m_FrmSetCenterLineInsideData.pIMULayer;
                pCenterlineLayer = m_FrmSetCenterLineInsideData.pCenterlineLayer;
                pCenterlinePointLayer = m_FrmSetCenterLineInsideData.pCenterlinePointLayer;

                if (m_NewIMUToCenterlineMapping != null)
                {
                    m_NewIMUToCenterlineMapping.pIMULayer = this.pIMULayer;
                    m_NewIMUToCenterlineMapping.pCenterlineLayer = this.pCenterlineLayer;
                    m_NewIMUToCenterlineMapping.pCenterlinePointLayer = this.pCenterlinePointLayer;

                    m_NewIMUToCenterlineMapping.FrmVectorLinkTable.IMULayer = this.pIMULayer;
                    m_NewIMUToCenterlineMapping.FrmVectorLinkTable.CenterlinePointLayer = this.pCenterlinePointLayer;
                    m_NewIMUToCenterlineMapping.FrmVectorLinkTable.CenterlineLinarLayer = this.pCenterlineLayer;
                    m_NewIMUToCenterlineMapping.FrmVectorLinkTable.ClearMappingPoints();
                    m_NewIMUToCenterlineMapping.FrmVectorLinkTable.UpdateLayerName();
                }
            }

        }

        #endregion
    }
}
