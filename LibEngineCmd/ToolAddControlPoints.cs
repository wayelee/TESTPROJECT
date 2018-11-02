using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using LibCerMap;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolAddControlPoints.
    /// </summary>
    [Guid("af6116b3-430e-41fa-bff5-b6ee805b8d7f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolAddControlPoints")]
    public sealed class ToolAddControlPoints : BaseTool
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
        public IRasterLayer pRasterLayer;
        public IMap pMap;
        public IPointCollection OriginPoints = new MultipointClass();
        public IPointCollection TargetPoints = new MultipointClass();
        public IPointCollection TransformedOriginPoints = new MultipointClass();
        private bool FirstPoint = true;
        private INewLineFeedback m_NewLineFeedback = new NewLineFeedbackClass();
        public FrmLinkTableRaster m_FrmLinkTableRaster = new FrmLinkTableRaster();
        public ToolAddControlPoints()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "添加控制点";  //localizable text 
            base.m_message = "添加控制点";  //localizable text
            base.m_toolTip = "添加控制点";  //localizable text
            base.m_name = "CustomCE.ToolAddControlPoints";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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
            FrmLinkTableRaster frm = m_FrmLinkTableRaster;
            frm.pRasterLayer = pRasterLayer;
            frm.TargetPoints = TargetPoints;
            frm.OriginPoints = OriginPoints;
            frm.TransformedOriginPoints = TransformedOriginPoints;
            //frm.pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            //frm.Show();
            frm.Owner =
                   System.Windows.Forms.Form.FromChildHandle(User32API.GetCurrentWindowHandle()) as System.Windows.Forms.Form;

        }

        #region Overridden Class Methods

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
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            
            // TODO:  Add ToolAddControlPoints.OnCreate implementation
            m_FrmLinkTableRaster.pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolAddControlPoints.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolAddControlPoints.OnMouseDown implementation
            object Miss = Type.Missing;
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                IGeoReference pGR = pRasterLayer as IGeoReference;
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                m_NewLineFeedback.Display = pMapCtr.ActiveView.ScreenDisplay;
                if (FirstPoint == true)//起始点
                {                   
                    IPointCollection pPCFrom ;
                    IPointCollection pPCTo = new MultipointClass();
                    pPCTo.AddPoint(mapPoint, ref Miss, ref Miss);
                    pPCFrom = pGR.PointsTransform(pPCTo, false);
                    //for (int i = 0; i < OriginPoints.PointCount; i++)
                    //{
                    //    pPCFrom.AddPoint(OriginPoints.get_Point(i));
                    //}
                    //pPCFrom.AddPoint(mapPoint);

                    m_NewLineFeedback.Start(mapPoint);
                   // OriginPoints.AddPoint(mapPoint);
                    IPoint pt = pPCFrom.get_Point(0);
                    OriginPoints.AddPoint(pt);
                    FirstPoint = false;
                }
                else//目标点
                {
                    TargetPoints.AddPoint(mapPoint);
                    FirstPoint = true;
                    m_NewLineFeedback.Stop();
                   // IPointCollection pPC = pGR.PointsTransform(TargetPoints, false);
                    //IPointCollection pPC = OriginPoints;
                    //IPoint pt1, pt2;
                    //pt1 = pPC.get_Point(0);
                    //pt2 = TargetPoints.get_Point(0);
                    //if (pPC.PointCount == 1)
                    //{
                    //    //IPoint pt1,pt2;
                    //    //pt1 = pPC.get_Point(0);
                    //    //pt2 = TargetPoints.get_Point(0);
                    //    pGR.Shift(pt2.X - pt1.X, pt2.Y - pt1.Y);
                       
                    //}
                    //else
                    //if (pPC.PointCount ==2 )
                    //{
                    //    pGR.TwoPointsAdjust(pPC, TargetPoints);
                    //}
                    //else
                    //{
                    //    pGR.Warp(pPC, TargetPoints, m_FrmLinkTableRaster.WarpType);
                    //}
                    //m_FrmLinkTableRaster.AddControlPoints(OriginPoints.get_Point(OriginPoints.PointCount -1)
                    //    ,TargetPoints.get_Point(TargetPoints.PointCount -1));
                    m_FrmLinkTableRaster.pRasterLayer = pRasterLayer;
                    m_FrmLinkTableRaster.pMapCtr = pMapCtr;
                    m_FrmLinkTableRaster.RefreshControlAllPoints();
                }

                TransformedOriginPoints = pGR.PointsTransform(OriginPoints, true);

               // pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }           
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolAddControlPoints.OnMouseMove implementation
            if (FirstPoint == false)
            {
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr != null)
                {
                    IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                    m_NewLineFeedback.MoveTo(mapPoint);
                }
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolAddControlPoints.OnMouseUp implementation
        }
        public override bool Deactivate()
        {

            return base.Deactivate();
        }

        public void DeleteAllControlPoints()
        {
             IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
             if (pMapCtr != null)
             {
                 IGeoReference pGR = pRasterLayer as IGeoReference;
                 if (pGR != null)
                 {
                     pGR.Reset();
                     TransformedOriginPoints.RemovePoints(0, TransformedOriginPoints.PointCount);
                     OriginPoints.RemovePoints(0,OriginPoints.PointCount);
                     TargetPoints.RemovePoints(0, TargetPoints.PointCount);
                     m_FrmLinkTableRaster.RefreshControlAllPoints();
                     pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                 }
             }
        }

        #endregion
    }
}
