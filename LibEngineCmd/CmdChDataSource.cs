using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SystemUI;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdChDataSource.
    /// </summary>
    [Guid("bf406373-4b13-4731-b022-8d6f9bcabe85")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdChDataSource")]
    public sealed class CmdChDataSource : BaseCommand
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
        //private IMapControl3 m_mapControl;
        private IMapControl2 m_HidenMapCtrl;

        //Record selected map and layer
        ITOCControlDefault m_pTocCtl = null;
        IBasicMap m_pMap = null;
        ILayer m_pLayer = null;

        public CmdChDataSource(IMapControl2 mapctrl,ITOCControlDefault tocCtl)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "更改数据源";  //localizable text
            base.m_message = "更改数据源";  //localizable text 
            base.m_toolTip = "更改数据源";  //localizable text 
            base.m_name = "CustomCE.CmdChDataSource";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            m_HidenMapCtrl = mapctrl;
            m_pTocCtl = tocCtl;
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

            //m_mapControl = (IMapControl3)hook;//不能直接用，不一定是MapControl控件
            // TODO:  Add other initialization code
        }

        public override bool Enabled
        {
            get
            {
                ClsGlobal.GetSelectedMapAndLayer(m_pTocCtl, ref m_pMap, ref m_pLayer);
                if (m_pLayer != null)
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
             try
            {
                ClsGlobal.GetSelectedMapAndLayer(m_pTocCtl, ref m_pMap, ref m_pLayer);
                if (m_pMap ==null || m_pLayer==null)
                {
                    return;
                }
                

                IMap pMapHiden = m_HidenMapCtrl.Map;
                pMapHiden.ClearLayers();

                ICommand pCmd = new ControlsAddDataCommand();
                pCmd.OnCreate(m_HidenMapCtrl);
                pCmd.OnClick();

                ILayer pLayerSrc = null;
                if (pMapHiden.LayerCount > 0)
                {
                    pLayerSrc = pMapHiden.get_Layer(0);
                    if (pLayerSrc != null)
                    {
                        IDataset pDatasetSrc = pLayerSrc as IDataset;
                        IWorkspace pWksSrc = pDatasetSrc.Workspace;
                        LibCerMap.ClsGDBDataCommon cls = new LibCerMap.ClsGDBDataCommon();
                        cls.SetDataSource(m_pLayer, pLayerSrc);
                        cls.RepairDataSource(m_pMap, pWksSrc);
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        m_pTocCtl.Update();
                    }
                }
                pMapHiden.ClearLayers();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        #endregion
        }
    }
}
