using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using LibCerMap;
using System.Collections.Generic;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolNewDisplacement.
    /// </summary>
    [Guid("85b59f3f-ac7d-4ea5-9ca4-baea7525f13d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolNewDisplacement")]
    public sealed class ToolNewDisplacement : BaseTool
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
        //连接点原始位置
        private IPointCollection m_OriginPoints = new MultipointClass();
        //连接点目标位置
        private IPointCollection m_TargetPoints = new MultipointClass();
        //橡皮线对象
        private INewLineFeedback m_NewLineFeedback = new NewLineFeedbackClass();
        //指示是否为线的起点
        private bool bFlag = true;
        //连接点表
        private FrmVectorLinkTable m_FrmVectorLinkTable=new FrmVectorLinkTable();
        private IGraphicsLayer m_pIGraphicsLayer = null;

        private List<String> m_listLayers;

        public ToolNewDisplacement()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "添加新连接";  //localizable text 
            base.m_message = "添加新连接";  //localizable text
            base.m_toolTip = "添加新连接";  //localizable text
            base.m_name = "CustomCE.ToolNewDisplacement";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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

        public FrmVectorLinkTable FrmVectorLinkTable
        {
            get
            {
                return m_FrmVectorLinkTable;
            }

            set
            {
                m_FrmVectorLinkTable = value;
            }
        }

        public int ControlPointsCount
        {
            get
            {
                return OriginPoints.PointCount;
            }
        }

        public IPointCollection OriginPoints
        {
            get
            {
                return m_OriginPoints;
            }
        }

        public IPointCollection TargetPoints
        {
            get
            {
                return m_TargetPoints;
            }
        }

        public FrmVectorLinkTable VectorLinkTable
        {
            get
            {
                return m_FrmVectorLinkTable;
            }
        }

        public IGraphicsLayer pGraphicsLayer
        {
            get
            {
                return m_pIGraphicsLayer;
            }

            set
            {
                m_pIGraphicsLayer = value;
            }
        }

       

        #region Overridden Class Methods

        public override bool Enabled
        {
            get
            {
                // TODO: Add CmdNorthArrow.OnClick implementation
                if (m_hookHelper.ActiveView is IMap && m_listLayers!=null)
                {
                    if (m_listLayers.Count >0)
                    {
                        return true;
                    }
 
                }
                return false;
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

            // TODO:  Add ToolNewDisplacement.OnCreate implementation
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            //m_NewLineFeedback.Display = pMapCtr.ActiveView.ScreenDisplay;
            m_FrmVectorLinkTable.OriginPoints = OriginPoints;
            m_FrmVectorLinkTable.TargetPoints = TargetPoints;
            m_FrmVectorLinkTable.MapCtr = pMapCtr;
            //m_FrmVectorLinkTable.refreshLayer += new RefreshEvent(RefreshLayer);
           
            if (m_FrmVectorLinkTable!=null)
            {
                m_FrmVectorLinkTable.Owner =
                   System.Windows.Forms.Form.FromChildHandle(User32API.GetCurrentWindowHandle()) as System.Windows.Forms.Form;
            }
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolNewDisplacement.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolNewDisplacement.OnMouseDown implementation
            object Miss = Type.Missing;
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                //IGeoReference pGR = pRasterLayer as IGeoReference;
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                m_NewLineFeedback.Display = pMapCtr.ActiveView.ScreenDisplay;
                if (bFlag == true)//起始点
                {
                    //IPointCollection pPCFrom;
                    //IPointCollection pPCTo = new MultipointClass();
                    //pPCTo.AddPoint(mapPoint, ref Miss, ref Miss);
                    //pPCFrom = pGR.PointsTransform(pPCTo, false);
                    
                    
                    // OriginPoints.AddPoint(mapPoint);
                    //IPoint pt = pPCFrom.get_Point(0);
                    m_NewLineFeedback.Start(mapPoint);
                    OriginPoints.AddPoint(mapPoint);
                    bFlag = false;
                }
                else//目标点
                {
                    TargetPoints.AddPoint(mapPoint);
                    bFlag = true;
                    IPolyline pPline = m_NewLineFeedback.Stop();

                    m_FrmVectorLinkTable.RefreshDataTable();
                    //RefreshLayer();
                }
            }      
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolNewDisplacement.OnMouseMove implementation
            if (bFlag == false)
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
            // TODO:  Add ToolNewDisplacement.OnMouseUp implementation
        }

        public void RefreshLayer()
        {
            

            if (pGraphicsLayer != null)
            {
                IGraphicsContainer pGC = pGraphicsLayer as IGraphicsContainer;
                int nCount = OriginPoints.PointCount;

                pGC.DeleteAllElements();
                for (int i = 0; i < nCount; i++)
                {
                    IPolyline ppl = new PolylineClass();
                    ppl.FromPoint = OriginPoints.get_Point(i);
                    ppl.ToPoint = TargetPoints.get_Point(i);

                    IRgbColor pColor = new RgbColorClass();


                    ICartographicLineSymbol pCartoLineSymbol = new CartographicLineSymbolClass();
                    pCartoLineSymbol.Cap = esriLineCapStyle.esriLCSRound;

                    ILineProperties pLineProp = pCartoLineSymbol as ILineProperties;
                    pLineProp.DecorationOnTop = true;

                    ILineDecoration pLineDecoration = new LineDecorationClass();
                    ISimpleLineDecorationElement pSimpleLineDecoElem = new SimpleLineDecorationElementClass();
                    pSimpleLineDecoElem.AddPosition(1);
                    IArrowMarkerSymbol pArrowMarkerSym = new ArrowMarkerSymbolClass();
                    pArrowMarkerSym.Size = 8;
                    pArrowMarkerSym.Color = pColor;
                    pSimpleLineDecoElem.MarkerSymbol = pArrowMarkerSym as IMarkerSymbol;
                    pLineDecoration.AddElement(pSimpleLineDecoElem as ILineDecorationElement);
                    pLineProp.LineDecoration = pLineDecoration;

                    ILineSymbol pLineSymbol = pCartoLineSymbol as ILineSymbol;

                    pLineSymbol.Color = pColor;
                    pLineSymbol.Width = 1;

                    ILineElement pLineElem = new LineElementClass();
                    pLineElem.Symbol = pLineSymbol;
                    IElement pElem = pLineElem as IElement;
                    pElem.Geometry = ppl;

                    pGC.AddElement(pElem, 0);

                    // IGraphicsContainerSelect pGCS = pGC as IGraphicsContainerSelect;
                    // pGCS.SelectAllElements();
                    //bool bbb =   pGCS.ElementSelected(pElement);
                    //pGC.UpdateElement(pElement);

                    // IGraphicsContainerSelect pGCS = pGC as IGraphicsContainerSelect;
                    // pGCS.SelectAllElements();
                    //bool bbb =   pGCS.ElementSelected(pElement);
                    //pGC.UpdateElement(pElement);

                    //pElement = new MarkerElementClass();
                    //pElement.Geometry = m_OriginPoints.get_Point(i);
                    //ISimpleMarkerSymbol sms = new SimpleMarkerSymbolClass();
                    //sms.Style = esriSimpleMarkerStyle.esriSMSSquare;
                    //sms.Size = 9;
                    //IMarkerElement im = pElement as IMarkerElement;
                    //im.Symbol = sms ;
                    //    IGraphicsContainer pGraphicsContainer = m_pMapCtr.Map.BasicGraphicsLayer as IGraphicsContainer;
                    //  pGraphicsContainer.AddElement(pElement, 0);
                    //pGC.AddElement(pElement,0);
                    //pGC.UpdateElement(pElement);



                    //IMarkerElement pMarkerEle = new MarkerElementClass();
                    //IPictureMarkerSymbol pPictureMarkerSymbol = new PictureMarkerSymbol();
                    ////pPictureMarkerSymbol.Color = sitecolor as IColor;
                    //pPictureMarkerSymbol.Size = 10;
                    //pPictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap,
                    //    GetParentPathofExe() + @"Resource\Globe.bmp");
                    //IElement pEle = pMarkerEle as IElement;
                    //pEle.Geometry = ppl.FromPoint;
                    //pMarkerEle.Symbol = pPictureMarkerSymbol;
                    //IGraphicsContainer pGraphicsContainer = m_pMapCtr.Map.BasicGraphicsLayer as IGraphicsContainer;
                    ////site.pEle = pEle;
                    //pGraphicsContainer.AddElement(pEle, 0);
                }

                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr != null)
                {
                    if (OriginPoints.PointCount == 0)
                    {
                        pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
                    }
                    else
                    {
                        pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    }
                }
            }
        }
        #endregion
    }
}
