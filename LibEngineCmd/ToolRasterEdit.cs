using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using LibCerMap;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolRasterEdit.
    /// </summary>
    [Guid("adc779ab-ea64-4df7-95eb-38ab1f413c2b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolRasterEdit")]
    public sealed class ToolRasterEdit : BaseTool
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
        public IRasterLayer m_pRasterLayer = null;

        //选择的区域范围
        private IPolygon m_pClipPolygon = new PolygonClass();

        //无效值设置界面
        FrmSetRasterDataValue m_pFrmSetRasterNoDataValue = null;

        private INewPolygonFeedback m_NewPolygonFeedback = null;
        public ToolRasterEdit()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "栅格编辑";  //localizable text 
            base.m_message = "栅格编辑";  //localizable text
            base.m_toolTip = "栅格编辑";  //localizable text
            base.m_name = "CustomCE.ToolRasterEdit";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add ToolRasterEdit.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolRasterEdit.OnClick implementation
            if (m_pRasterLayer == null)
                m_pFrmSetRasterNoDataValue = new FrmSetRasterDataValue(null);
            else
                m_pFrmSetRasterNoDataValue = new FrmSetRasterDataValue(m_pRasterLayer.Raster);

            m_pFrmSetRasterNoDataValue.Show();
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolRasterEdit.OnMouseDown implementation
            IMapControl3 pMapCtrl = ClsGlobal.GetMapControl(m_hookHelper);
            if (pMapCtrl == null)
                return;
            IPoint pPoint = pMapCtrl.ToMapPoint(X, Y);
            if (Button  ==1 )
            {
                if (m_NewPolygonFeedback == null)
                {
                    m_NewPolygonFeedback = new NewPolygonFeedbackClass();
                    m_NewPolygonFeedback.Display = pMapCtrl.ActiveView.ScreenDisplay;

                    m_NewPolygonFeedback.Start(pPoint);                   
                }
                else
                {
                    try
                    {
                        object Miss = Type.Missing;
                        m_NewPolygonFeedback.AddPoint(pPoint);
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            if (Button ==2)
            {
                if (m_NewPolygonFeedback != null)
                {
                    m_NewPolygonFeedback.Stop();
                }
              
                m_NewPolygonFeedback = null;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolRasterEdit.OnMouseMove implementation
             IMapControl3 pMapCtrl = ClsGlobal.GetMapControl(m_hookHelper);
            if (pMapCtrl == null)
                return;
            IPoint pPoint = pMapCtrl.ToMapPoint(X, Y);
            if (m_NewPolygonFeedback != null)
            {
                m_NewPolygonFeedback.MoveTo(pPoint);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolRasterEdit.OnMouseUp implementation

        }

        public override void OnDblClick()
        {
            IMapControl3 pMapCtrl = ClsGlobal.GetMapControl(m_hookHelper);
            if (pMapCtrl == null)
            {
                if (m_NewPolygonFeedback != null)
                {
                    m_NewPolygonFeedback.Stop();
                    m_NewPolygonFeedback = null;
                    return;
                }
            }
            IGeometry pGeometry = m_NewPolygonFeedback.Stop();
            m_NewPolygonFeedback = null;
            if (pGeometry == null)
                return;

            m_pClipPolygon = pGeometry as IPolygon;
            double dbNoDataValue = m_pFrmSetRasterNoDataValue.NoDataValue;

            //获取到无效值区域、无效值、栅格数据之后，开始设置
            ClsSetRasterRegionToDataValue pSetRasterToNoDataValue = new ClsSetRasterRegionToDataValue(m_pRasterLayer.Raster, m_pClipPolygon, dbNoDataValue);
            if (!pSetRasterToNoDataValue.SetRegionToNoDataValue())
                MessageBox.Show("设置无效值出错！");
            else
            {
                //MessageBox.Show("设置无效值成功！");
                //重新加载栅格数据
                try
                {
                    IRaster2 pSrcRaster2 = m_pRasterLayer.Raster as IRaster2;
                    IRasterDataset pDstRasterDataset = pSrcRaster2.RasterDataset;
                    IRasterPyramid3 pDstRasterPyramid3 = pDstRasterDataset as IRasterPyramid3;
                    if (pDstRasterPyramid3.Present)
                    {
                        IDataLayer2 pDataLayer = m_pRasterLayer as IDataLayer2;
                        pDataLayer.Disconnect();
                        pDstRasterPyramid3.DeletePyramid();
                        pDstRasterPyramid3.Create();
                        m_pRasterLayer.CreateFromDataset(pDstRasterDataset);
                    }

                    IMapControl3 pMapControl3 = ClsGlobal.GetMapControl(m_hookHelper);
                    pMapControl3.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }          
            base.OnDblClick();
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
           
        }

        public override void OnKeyUp(int keyCode, int Shift)
        {
            if (keyCode == 13)
            {
              
            }
            base.OnKeyUp(keyCode, Shift);
        }



        public override bool Deactivate()
        {
            if (m_pFrmSetRasterNoDataValue != null)
                m_pFrmSetRasterNoDataValue.Close();
            return m_deactivate;
        }

        public override bool Enabled
        {
            get
            {
                if ((m_hookHelper.ActiveView is IMap) && m_pRasterLayer != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
