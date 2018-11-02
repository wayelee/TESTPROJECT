using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using LibCerMap;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdSetAdjustData.
    /// </summary>
    [Guid("2877dbc4-e83e-4e3c-ae73-ec82ea0987c4")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdSetAdjustData")]
    public sealed class CmdSetAdjustData : BaseCommand
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
        private FrmSetAdjustData m_FrmSetAdjustData = null;
        private ToolNewDisplacement m_NewDisplacement;

        private List<String> m_listLayers;

        public ToolNewDisplacement NewDisplacement
        {
            set
            {
                m_NewDisplacement = value;
            }

            get
            {
                return m_NewDisplacement;
            }
        }

        public List<String> ListLayers
        {
            get
            {
                return m_listLayers;
            }

            set
            {
                m_listLayers = value;
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

        public CmdSetAdjustData()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "设置待校正数据";  //localizable text
            base.m_message = "设置待校正数据";  //localizable text 
            base.m_toolTip = "设置待校正数据";  //localizable text 
            base.m_name = "CustomCE.CmdSetAdjustData";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            if (m_FrmSetAdjustData ==null)
            {
                m_FrmSetAdjustData = new FrmSetAdjustData(pMapCtr);
            }
            //m_FrmSetAdjustData.ListLayers.Clear();
            //for (int i = 0; i < pMapCtr.Map.LayerCount; i++ )
            //{
            //    if (pMapCtr.Map.get_Layer(i) is IFeatureLayer)
            //    {
            //        m_FrmSetAdjustData.ListLayers.Add(pMapCtr.Map.get_Layer(i).Name);
            //    }
            //}

            if (m_FrmSetAdjustData.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.m_listLayers = m_FrmSetAdjustData.ListLayers;

                if (m_NewDisplacement != null)
                {
                    m_NewDisplacement.ListLayers = this.m_listLayers;
                }
            }
        }

        #endregion
    }
}
