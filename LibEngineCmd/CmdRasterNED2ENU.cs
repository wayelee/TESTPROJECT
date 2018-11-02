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
    /// Summary description for CmdRasterNED2ENU.
    /// </summary>
    [Guid("1ae5f1d6-7582-44e1-9e1d-b3c7e023c377")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdRasterNED2ENU")]
    public sealed class CmdRasterNED2ENU : BaseCommand
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
        private ClsRasterOp pRasterOp=new ClsRasterOp();

        //目标图层
        public IRasterLayer pRasterLayer;
        public CmdRasterNED2ENU()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "坐标转换";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "坐标转换";  //localizable text 
            base.m_name = "CustomCE.CmdRasterNED2ENU";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
                if ((m_hookHelper.ActiveView is IMap) && pRasterLayer != null)
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

#region 废弃代码
                //SaveFileDialog dlgOutputFile = new SaveFileDialog();
                //dlgOutputFile.Title = "选择输出文件路径：";
                //dlgOutputFile.InitialDirectory = ".";
                //dlgOutputFile.Filter = "raster文件(*.tif)|*.tif|所有文件(*.*)|*.*";
                //dlgOutputFile.RestoreDirectory = true;
                //dlgOutputFile.DefaultExt = "tif";

                //if (dlgOutputFile.ShowDialog() == DialogResult.OK)
                //{
                //    string szDstFilename = dlgOutputFile.FileName;
                //    //pRasterOp.NorthEastToEastNorth3(pRasterLayer);
                //    pRasterOp.NorthEastToEastNorth2(pRasterLayer, szDstFilename);
                //    //pRasterOp.NorthEastToEastNorth(pRasterLayer, szDstFilename);
                //    //添加LAYER
                //    IRasterLayer pDstRasterLayer = new RasterLayerClass();
                //    pDstRasterLayer.CreateFromFilePath(szDstFilename);


                //    IMapControl3 pMapControl = null;
                //    if (m_hookHelper.Hook is IToolbarControl)
                //    {
                //        if (((IToolbarControl)m_hookHelper.Hook).Buddy is IMapControl3)
                //        {
                //            pMapControl = (IMapControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
                //        }

                //    }
                //    //In case the container is MapControl
                //    else if (m_hookHelper.Hook is IMapControl3)
                //    {
                //        pMapControl = (IMapControl3)m_hookHelper.Hook;
                //    }
                //    else
                //    {
                //        pMapControl = null;
                //    }
                //    if (pMapControl != null)
                //    {
                //        pMapControl.AddLayer(pDstRasterLayer as ILayer);
                //        pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                //    }
                //}
#endregion
                pRasterOp.NorthEastToEastNorth3(pRasterLayer);

                //刷新
                IMapControl3 pMapControl = null;
                if (m_hookHelper.Hook is IToolbarControl)
                {
                    if (((IToolbarControl)m_hookHelper.Hook).Buddy is IMapControl3)
                    {
                        pMapControl = (IMapControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
                    }

                }
                //In case the container is MapControl
                else if (m_hookHelper.Hook is IMapControl3)
                {
                    pMapControl = (IMapControl3)m_hookHelper.Hook;
                }
                else
                {
                    pMapControl = null;
                }
                if (pMapControl != null)
                {
                    pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
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
