using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using LibCerMap;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolCreateCircle.
    /// </summary>
    [Guid("1f225673-d711-48a3-b6b0-b748fa1b9d1c")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolCreateCircle")]
    public sealed class ToolCreateCircle : BaseTool
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
        private IMapControl3 m_pMapCtl = null;
        private IFeatureLayer m_FLayer;
        private INewCircleFeedback m_NewCircleFeedback = null;
        private IPoint m_CenterPoint;
        public ToolCreateCircle()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "绘制圆";  //localizable text 
            base.m_message = "绘制圆";  //localizable text
            base.m_toolTip = "绘制圆";  //localizable text
            base.m_name = "CustomCE.ToolCreateCircle";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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

            // TODO:  Add ToolCreateCircle.OnCreate implementation

        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolCreateCircle.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolCreateCircle.OnMouseDown implementation
            if ((m_pMapCtl = ClsGlobal.GetMapControl(m_hookHelper)) == null) return;
            IPoint pPoint = m_pMapCtl.ToMapPoint(X, Y);

            if (Button == 1)
            {
                if (m_NewCircleFeedback == null)
                {
                    m_NewCircleFeedback = new NewCircleFeedbackClass();
                    m_NewCircleFeedback.Display = m_pMapCtl.ActiveView.ScreenDisplay;
                    
                    m_NewCircleFeedback.Start(pPoint);
                    m_CenterPoint = pPoint;
                }    
                else
                {
                    try
                    {
                        object Miss = Type.Missing;
                        ICircularArc pArc = m_NewCircleFeedback.Stop();
                        //IGeometry geometry = new PolygonClass();
                        //geometry = m_pMapCtl.TrackCircle();
                        IPolygon pPolygon = new PolygonClass();
                        ISegment pArcC = pArc as ISegment;
                        ISegmentCollection pArcP = pPolygon as ISegmentCollection;
                        pArcP.AddSegment(pArcC, ref Miss, ref Miss);
                        pPolygon.Close();
                        IFeature pFeature = m_FLayer.FeatureClass.CreateFeature();
                        pFeature.Shape = pPolygon;
                        pFeature.Store();
                        m_pMapCtl.Refresh();
                        m_NewCircleFeedback = null;
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            if (Button == 2)
            {
                double radius = Math.Sqrt((pPoint.X - m_CenterPoint.X) * (pPoint.X - m_CenterPoint.X) + (pPoint.Y - m_CenterPoint.Y) * (pPoint.Y - m_CenterPoint.Y));
                FrmDrawCircle frm = new FrmDrawCircle(radius);
                IConstructCircularArc pArcConstruct = null;
                if (m_NewCircleFeedback == null) return;
 
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pArcConstruct =  new CircularArcClass();
                        pArcConstruct.ConstructCircle(m_CenterPoint, frm.m_radius, false);
                        if (pArcConstruct != null)
                        {
                            IPolygon pPolygon = new PolygonClass();
                            ISegment pArcC = pArcConstruct as ISegment;
                            ISegmentCollection pArcP = pPolygon as ISegmentCollection;
                            pArcP.AddSegment(pArcC);
                            pPolygon.Close();
                            IFeature pFeature = m_FLayer.FeatureClass.CreateFeature();
                            pFeature.Shape = pPolygon;
                            pFeature.Store();
                            m_pMapCtl.Refresh();
                            m_NewCircleFeedback.Stop();
                            m_NewCircleFeedback = null;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        if (m_NewCircleFeedback!=null)
                        {
                            m_NewCircleFeedback.Stop();
                        }                       
                        m_NewCircleFeedback = null;
                    }
                   
                }
            }

        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolCreateCircle.OnMouseMove implementation
            if (m_NewCircleFeedback != null)
            {
                IPoint pPoint;
                pPoint = m_pMapCtl.ToMapPoint(X,Y);
                m_NewCircleFeedback.MoveTo(pPoint);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            
            
        }

        public override bool Enabled
        {
            get
            {

                IMapControl3 mapCtl = ClsGlobal.GetMapControl(m_hookHelper);
                if (mapCtl == null) return false;
                //IMap map = ClsGlobal.GetFocusMap(m_hookHelper);
                //if (map == null) return false;
                IEngineEditor pEngineEdit = new EngineEditorClass();
                IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
                IFeatureLayer targetLayer = pEEditLayers.TargetLayer;

                if (targetLayer == null) return false;
                if (targetLayer.FeatureClass == null) return false;
                if (targetLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    if (targetLayer.FeatureClass.FeatureType == esriFeatureType.esriFTSimple)
                    {
                        m_FLayer = targetLayer;
                        return true;
                    }
                }

                
                return false;

#region 原代码

                //IEngineEditor pEngineEdit = new EngineEditorClass();
               
                //IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
                //IMapControl3 axMapCtlMain = ClsGlobal.GetMapControl(m_hookHelper);
                //if (axMapCtlMain==null)
                //{
                //    return false;
                //}


                //for (int i = 0; i < axMapCtlMain.Map.LayerCount; i++)
                //{
                //    ILayer pLayer = axMapCtlMain.Map.get_Layer(i);
                //    if (pLayer is IFeatureLayer)
                //    {
                //        IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                //        IFeatureClass pFClass = pFLayer.FeatureClass;
                //        if (pFClass != null)
                //        {
                //            if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon )
                //            {
                //                if (pEEditLayers.IsEditable(pFLayer) == false)
                //               // if ( pEngineEdit.EditState != esriEngineEditState.esriEngineStateEditing)
                //                {
                //                    continue;
                //                }
                //                else if (pEEditLayers.IsEditable(pFLayer) == true)
                //                //else if (pEngineEdit.EditState == esriEngineEditState.esriEngineStateEditing)
                //                {
                //                    if (pEEditLayers.TargetLayer.Name==pFLayer.Name)
                //                    {
                //                        m_FLayer = pFLayer;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                //if (m_FLayer == null)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
                //return base.Enabled;
#endregion
            }
        }

        public override bool Deactivate()
        {
            m_FLayer = null;
            return base.Deactivate();
        }
        #endregion
    }
}
