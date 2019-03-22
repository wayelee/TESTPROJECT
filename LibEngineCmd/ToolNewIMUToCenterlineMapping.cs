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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.TrackingAnalyst;
using ESRI.ArcGIS.Geodatabase;
namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolNewDisplacement.
    /// </summary>
    [Guid("7e55e1b0-7404-4c3f-a9b1-a84163f24d4a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolNewIMUToCenterlineMapping")]
    //手动添加内检测点到中线点的匹配
    public sealed class ToolNewIMUToCenterlineMapping : BaseTool
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
        public List<IFeature> m_IMUFeatureList = new List<IFeature>();
        public List<IFeature> m_CenterlinePointFeatureList = new List<IFeature>();
        private IFeature IMUFeature;
        private IFeature CenterlinePointFeature;

        //橡皮线对象
        private INewLineFeedback m_NewLineFeedback = new NewLineFeedbackClass();

        private ISnappingFeedback m_snappingFeed;

        ISnappingEnvironment m_SnappingEnvironment;
        private EngineEditor m_EngineEditor = new EngineEditorClass();

        ISet SnappingtExcludedLayers = new ObjectList();

        //指示是否为线的起点
        private bool bFlag = true;
        //连接点表
        public FrmIMUCenterlineMappingTable m_FrmVectorLinkTable = new FrmIMUCenterlineMappingTable();
        private IGraphicsLayer m_pIGraphicsLayer = null;

        

        private FrmCenterLineInsideLayerSetting LayerSettingForm;

        public IFeatureLayer pIMULayer;
        public IFeatureLayer pCenterlinePointLayer;
        public IFeatureLayer pCenterlineLayer;

        public ToolNewIMUToCenterlineMapping()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "设置控制点";  //localizable text 
            base.m_message = "设置控制点";  //localizable text
            base.m_toolTip = "设置控制点";  //localizable text
            base.m_name = "CustomCE.ToolNewIMUToCenterlineMapping";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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

        public FrmIMUCenterlineMappingTable FrmVectorLinkTable
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

        public FrmIMUCenterlineMappingTable VectorLinkTable
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
                if (m_hookHelper.ActiveView is IMap )
                {
                    return true;
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
            //m_FrmVectorLinkTable.OriginPoints = OriginPoints;
            //m_FrmVectorLinkTable.TargetPoints = TargetPoints;
            m_FrmVectorLinkTable.IMUFeatureList = m_IMUFeatureList;
            m_FrmVectorLinkTable.CenterlinePointFeatureList = m_CenterlinePointFeatureList;
            m_FrmVectorLinkTable.MapCtr = pMapCtr;
            //m_FrmVectorLinkTable.refreshLayer += new RefreshEvent(RefreshLayer);
           
            if (m_FrmVectorLinkTable!=null)
            {
                m_FrmVectorLinkTable.Owner =
                   System.Windows.Forms.Form.FromChildHandle(User32API.GetCurrentWindowHandle()) as System.Windows.Forms.Form;
            }

            IExtensionManager extensionManager = ((IHookHelper2)m_hookHelper).ExtensionManager;
            UID guid = new UIDClass();
            guid.Value = "{E07B4C52-C894-4558-B8D4-D4050018D1DA}"; //Snapping extension.
            IExtension extension = extensionManager.FindExtension(guid);
            m_SnappingEnvironment = extension as ISnappingEnvironment;

            m_snappingFeed = new SnappingFeedbackClass();
            m_snappingFeed.Initialize(hook, m_SnappingEnvironment, true);
        }
        private IPoint TransformToMapPoint(int X, int Y)
        {

            IActiveView pActiveView = m_hookHelper.ActiveView;
            IDisplayTransformation pDisplayTrans = pActiveView.ScreenDisplay.DisplayTransformation;

            IPoint point = pDisplayTrans.ToMapPoint(X, Y);
            return point;
        }
        private bool DataReady()
        {
            return !(pIMULayer == null || pCenterlineLayer == null || pCenterlinePointLayer == null);             
        }
        
        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolNewDisplacement.OnClick implementation
            //IMapControl3 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;
            //LayerSettingForm = new FrmCenterLineInsideLayerSetting(pMapCtr);
            //if(LayerSettingForm.ShowDialog() == DialogResult.OK)
                
            {
                //pIMULayer = LayerSettingForm.pIMULayer;
                //pCenterlineLayer = LayerSettingForm.pCenterlineLayer;
                //pCenterlinePointLayer = LayerSettingForm.pCenterlinePointLayer;
                if (!DataReady())
                {
                    MessageBox.Show("内检测、中线图层设置不正确");
                }
            }
        }
       

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolNewDisplacement.OnMouseDown implementation
            if (!DataReady())
            {
                MessageBox.Show("内检测、中线图层设置不正确");
                return;
            }

            object Miss = Type.Missing;
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
           IMapControl3 m_MapControl = pMapCtr as IMapControl3;

            if (pMapCtr != null)
            {
                //IGeoReference pGR = pRasterLayer as IGeoReference;
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                m_NewLineFeedback.Display = pMapCtr.ActiveView.ScreenDisplay;
                if (bFlag == true)//起始点
                {
                    IPoint point = TransformToMapPoint(X, Y);   
                    IPointSnapper ptSnapper = m_SnappingEnvironment.PointSnapper;
                    ISnappingResult snapresult = ptSnapper.Snap(point); 
                    if (snapresult != null)
                    {
                        m_snappingFeed.UnInitialize();
                        m_snappingFeed.Initialize(m_MapControl.Object, m_SnappingEnvironment, true);
                        m_snappingFeed.Refresh(m_MapControl.ActiveView.ScreenDisplay.hDC);
                        m_SnappingEnvironment.SnappingType = esriSnappingType.esriSnappingTypePoint;
                        ISpatialFilter pSF = new SpatialFilterClass();
                        IPoint ProjPt = snapresult.Location;
                        ProjPt.Project(((IGeoDataset)(pIMULayer.FeatureClass)).SpatialReference);
                        pSF.Geometry = ProjPt;
                        pSF.GeometryField = pIMULayer.FeatureClass.ShapeFieldName;
                        pSF.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        IFeatureCursor pCursor = pIMULayer.FeatureClass.Search(pSF, false);
                        IFeature pFeature = pCursor.NextFeature();
                        if (pFeature != null)
                        {
                            IMUFeature = pFeature; 
                            m_NewLineFeedback.Start(mapPoint); 

                            bFlag = false;
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pSF);

                      
                    }

                }
                else//目标点
                {
                     IPoint point = TransformToMapPoint(X, Y);   
                    IPointSnapper ptSnapper = m_SnappingEnvironment.PointSnapper;
                    ISnappingResult snapresult = ptSnapper.Snap(point);
                    if (snapresult != null)
                    {
                        //m_snappingFeed.Refresh(m_MapControl.ActiveView.ScreenDisplay.hDC);
                        //m_snappingFeed.UnInitialize();
                        //m_snappingFeed.Refresh(m_MapControl.ActiveView.ScreenDisplay.hDC);
                        //m_snappingFeed.Initialize(m_MapControl.Object, m_SnappingEnvironment, true);
                       
                        m_SnappingEnvironment.SnappingType = esriSnappingType.esriSnappingTypePoint;
                        ISpatialFilter pSF = new SpatialFilterClass();
                        IPoint ProjPt = snapresult.Location;
                        ProjPt.Project(((IGeoDataset)(pCenterlinePointLayer.FeatureClass)).SpatialReference);
                        pSF.Geometry = ProjPt;
                        pSF.GeometryField = pCenterlinePointLayer.FeatureClass.ShapeFieldName;
                        pSF.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        IFeatureCursor pCursor = pCenterlinePointLayer.FeatureClass.Search(pSF, false);
                        IFeature pFeature = pCursor.NextFeature();
                        if (pFeature != null)
                        {
                            CenterlinePointFeature = pFeature;

                          

                            if (m_CenterlinePointFeatureList.Count > 0)
                            {
                                double newIMUM = Convert.ToDouble(IMUFeature.Value[m_IMUFeatureList[0].Fields.FindField("记录距离")]);
                                double newCenterlineM = Convert.ToDouble(CenterlinePointFeature.Value[m_CenterlinePointFeatureList[0].Fields.FindField("里程")]);
                                for(int i = 0; i< m_CenterlinePointFeatureList.Count; i++)
                                {
                                    double previewIMUM = Convert.ToDouble(m_IMUFeatureList[i].Value[m_IMUFeatureList[0].Fields.FindField("记录距离")]);
                                    double previewCentlineM = Convert.ToDouble(m_CenterlinePointFeatureList[i].Value[m_CenterlinePointFeatureList[0].Fields.FindField("里程")]);
                                     
                                    if((newIMUM - previewIMUM) * (newCenterlineM - previewCentlineM) <= 0)
                                    {
                                        MessageBox.Show("特征点不能交叉匹配.");
                                        return;
                                    }  
                                }
                            }
                            
                            m_IMUFeatureList.Add(IMUFeature);
                            m_CenterlinePointFeatureList.Add(CenterlinePointFeature);
                            bFlag = true;
                            IPolyline pPline = m_NewLineFeedback.Stop();
                            m_FrmVectorLinkTable.RefreshDataTable();
                                                       
                        }
                    }
                    m_MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
                   //m_snappingFeed.UnInitialize();
                    
                   //m_snappingFeed.Initialize(m_MapControl.Object, m_SnappingEnvironment, true);
                   // m_snappingFeed.Refresh(m_MapControl.ActiveView.ScreenDisplay.hDC);
                }
            }      
        }
       

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolNewDisplacement.OnMouseMove implementation

           
            IMapControl3 m_MapControl = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;
            m_snappingFeed.Refresh(m_MapControl.ActiveView.ScreenDisplay.hDC);
            m_snappingFeed.UnInitialize();
            m_snappingFeed.Initialize(m_MapControl.Object, m_SnappingEnvironment, true);
            m_snappingFeed.Refresh(m_MapControl.ActiveView.ScreenDisplay.hDC);
            m_SnappingEnvironment.SnappingType = esriSnappingType.esriSnappingTypePoint;
            

            //起始点
            if (bFlag == true)
            {
                IPoint point = TransformToMapPoint(X, Y);   
                 IPointSnapper ptSnapper = m_SnappingEnvironment.PointSnapper;
                ISnappingResult snapresult = ptSnapper.Snap(point);
                if (snapresult != null)
                {
                 //   IEngineEditProperties2 ep2 = env as IEngineEditProperties2;
                    //if (ep2.SnapTips == true)
                    {
                        if (m_snappingFeed != null)
                        {
                            if (snapresult.Description == pIMULayer.Name + ": Point")
                            {
                                m_snappingFeed.Update(snapresult, m_MapControl.ActiveView.ScreenDisplay.hDC);  
                            }             
                        }

                    }
                }                
                else
                {
                    m_snappingFeed.Update(null, m_MapControl.ActiveView.ScreenDisplay.hDC);
                }
            }
            // 结束点
            if (bFlag == false)
            {
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr != null)
                {
                    IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                    m_NewLineFeedback.MoveTo(mapPoint);

                    IPoint point = TransformToMapPoint(X, Y);
                    IPointSnapper ptSnapper = m_SnappingEnvironment.PointSnapper;
                    ISnappingResult snapresult = ptSnapper.Snap(point);
                    if (snapresult != null)
                    {
                        //   IEngineEditProperties2 ep2 = env as IEngineEditProperties2;
                        //if (ep2.SnapTips == true)
                        {
                            if (m_snappingFeed != null)
                            {
                                if (snapresult.Description == pCenterlinePointLayer.Name + ": Point")
                                {
                                    m_snappingFeed.Update(snapresult, m_MapControl.ActiveView.ScreenDisplay.hDC);
                                }
                            }
                        }
                    }
                    else
                    {
                        m_snappingFeed.Update(null, m_MapControl.ActiveView.ScreenDisplay.hDC);
                    }

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
                int nCount = m_IMUFeatureList.Count;

                pGC.DeleteAllElements();
                for (int i = 0; i < nCount; i++)
                {
                    IPolyline ppl = new PolylineClass();
                    ppl.FromPoint = m_IMUFeatureList[i].Shape as IPoint;
                    ppl.ToPoint = m_CenterlinePointFeatureList[i].Shape as IPoint;

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
                    if (m_IMUFeatureList.Count == 0)
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
