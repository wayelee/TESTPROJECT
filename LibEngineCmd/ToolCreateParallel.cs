using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using LibCerMap;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolCreateParallel.
    /// </summary>
    [Guid("630cfefc-ee79-4735-961c-4b28c5dc423a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolCreateParallel")]
    public sealed class ToolCreateParallel : BaseTool
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
       // FrmParallelLineOffset m_FrmParallelLineOffset = null;
        private IMapControl3 m_pMapCtl = null;
        private ESRI.ArcGIS.Carto.IFeatureLayer m_FLayer;

        public ToolCreateParallel()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "绘制平行线";  //localizable text 
            base.m_message = "绘制平行线";  //localizable text
            base.m_toolTip = "绘制平行线";  //localizable text
            base.m_name = "CustomCE.ToolCreateParallel";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            
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

            // TODO:  Add ToolCreateParallel.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolCreateParallel.OnClick implementation
            //if (m_FrmParallelLineOffset == null)
            //{
            //    m_FrmParallelLineOffset = new FrmParallelLineOffset();
            //}
            //if (m_FrmParallelLineOffset.IsDisposed == true)
            //{
            //    m_FrmParallelLineOffset = new FrmParallelLineOffset();
            //}
            //m_FrmParallelLineOffset.Show();
            //m_FrmParallelLineOffset.Owner =
            //      System.Windows.Forms.Form.FromHandle(User32API.GetCurrentWindowHandle()) as System.Windows.Forms.Form;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolCreateParallel.OnMouseDown implementation
            if (Button == 1 && (m_pMapCtl = ClsGlobal.GetMapControl(m_hookHelper)) != null)
            {
                IMap pMap = m_pMapCtl.Map;
                IPoint po = m_pMapCtl.ToMapPoint(X, Y);
                ISelectionEnvironment pSelectionEnv = new SelectionEnvironmentClass();

                IGeometry geometry = new PolygonClass();
                geometry = m_pMapCtl.TrackRectangle();
                pMap.SelectByShape(geometry, pSelectionEnv, true);
                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                IEnumFeatureSetup pSelectionsetup = pMap.FeatureSelection as IEnumFeatureSetup;
                pSelectionsetup.AllFields = true;//这里是关键
                IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
                IFeature pF = pFeatureCollection.Next();
                if (pF != null && pF.Shape is IPolyline)
                {
                    FrmParallelLineOffset frm = new FrmParallelLineOffset();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        IPolyline pPolyline = ConstructOffset(pF.Shape as IPolyline, frm.offset);
                        IFeature pFeature = m_FLayer.FeatureClass.CreateFeature();
                        pFeature.Shape = pPolyline;
                        pFeature.Store();
                        m_pMapCtl.Refresh();
                    }
                }
            }
        }
        public IPolyline ConstructOffset(IPolyline inPolyline, double offset)
        {
            if (inPolyline == null || inPolyline.IsEmpty)
            {
                return null;
            }
            object Missing = Type.Missing;
            IConstructCurve constructCurve = new PolylineClass();
            constructCurve.ConstructOffset(inPolyline, offset, ref Missing, ref Missing);
            return constructCurve as IPolyline;
        }

        public override bool Enabled
        {
            get
            {
                IEngineEditor pEngineEdit = new EngineEditorClass();

                IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
                IMapControl3 axMapCtlMain = ClsGlobal.GetMapControl(m_hookHelper);
                if (axMapCtlMain == null)
                {
                    return false;
                }

                for (int i = 0; i < axMapCtlMain.Map.LayerCount; i++)
                {
                    ILayer pLayer = axMapCtlMain.Map.get_Layer(i);
                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                        IFeatureClass pFClass = pFLayer.FeatureClass;
                        if (pFClass != null)
                        {
                            if (pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                            {
                                if (pEEditLayers.IsEditable(pFLayer) == false)
                                // if ( pEngineEdit.EditState != esriEngineEditState.esriEngineStateEditing)
                                {
                                    continue;
                                }
                                else if (pEEditLayers.IsEditable(pFLayer) == true)
                                //else if (pEngineEdit.EditState == esriEngineEditState.esriEngineStateEditing)
                                {
                                    if (pEEditLayers.TargetLayer.Name == pFLayer.Name)
                                    {
                                        m_FLayer = pFLayer;
                                    }
                                }
                            }
                        }
                    }
                }
                if (m_FLayer == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                //return base.Enabled;
            }
        }
        
        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolCreateParallel.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolCreateParallel.OnMouseUp implementation
        }
        #endregion
    }
}
