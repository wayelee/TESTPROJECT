using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using LibCerMap;


namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdRasterRotate.
    /// </summary>
    [Guid("e26f3121-5fd0-4e3b-b4bb-bb37b2c9d8d0")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdRasterRotate")]
    public sealed class CmdRasterRotate : BaseCommand
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
        //Ä¿±êÍ¼²ã
        public IRasterLayer pRasterLayer;
        public FrmRasterRotate m_frmRasterRotate = null;

        public CmdRasterRotate()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "Õ¤¸ñÍ¼ÏñÐý×ª";  //localizable text
            base.m_message = "Õ¤¸ñÍ¼ÏñÐý×ª";  //localizable text 
            base.m_toolTip = "Õ¤¸ñÍ¼ÏñÐý×ª";  //localizable text 
            base.m_name = "CustomCE.CmdRasterRotate";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdRasterRotate.OnClick implementation
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                //IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                m_frmRasterRotate = new FrmRasterRotate();
                IRaster pRaster = pRasterLayer.Raster;
                IRasterProps pRasterProps = pRaster as IRasterProps;
                IEnvelope pEnvelope = pRasterProps.Extent;
                double X = (pEnvelope.XMax + pEnvelope.XMin) / 2;
                double Y = (pEnvelope.YMax + pEnvelope.YMin) / 2;
                m_frmRasterRotate.X = X;
                m_frmRasterRotate.Y = Y;
                if (m_frmRasterRotate.ShowDialog() == DialogResult.OK)
                {
                    if (m_frmRasterRotate.isture == true)
                    {
                        IGeoReference pGR = pRasterLayer as IGeoReference;
                        IPoint pPoint = new PointClass();
                        pPoint.X = m_frmRasterRotate.X;
                        pPoint.Y = m_frmRasterRotate.Y;
                        pGR.Rotate(pPoint, m_frmRasterRotate.angle);
                        pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
                    }
                }
            }
        }

        #endregion
    }
}
