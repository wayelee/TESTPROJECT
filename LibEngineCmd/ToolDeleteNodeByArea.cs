using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using LibCerMap;


namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolDeleteNodeByArea.
    /// </summary>
    [Guid("c7229514-a33b-4045-abf8-28ec26141ee7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolDeleteNodeByArea")]
    public sealed class ToolDeleteNodeByArea : BaseTool
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
        public ITinLayer pTinLayer;
        public IMap pMap;
        
        INewPolygonFeedback mNewPolygonFeedback = null;
        public ToolDeleteNodeByArea()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "删除区域TIN节点";  //localizable text 
            base.m_message = "删除区域TIN节点";  //localizable text
            base.m_toolTip = "删除区域TIN节点";  //localizable text
            base.m_name = "CustomCE.ToolDeleteNodeByArea";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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
        public override bool Enabled
        {
            get
            {
                // TODO: Add CmdNorthArrow.OnClick implementation   
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr == null)
                {
                    return false;
                }
                if (pTinLayer == null)
                {
                    return false;
                }
                else
                {
                    ITin pTin = pTinLayer.Dataset;
                    ITinEdit pTinEdit = pTin as ITinEdit;
                    return pTinEdit.IsInEditMode;
                }
            }
        }
        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                pMap = pMapCtr.Map;
            }
            IPageLayoutControl pLayoutCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IPageLayoutControl;
            if (pLayoutCtr != null)
            {
                pMap = pLayoutCtr.ActiveView.FocusMap;
            }

            // TODO:  Add ToolDeleteNodeByArea.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolDeleteNodeByArea.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolDeleteNodeByArea.OnMouseDown implementation
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                if (mNewPolygonFeedback == null)
                {
                    mNewPolygonFeedback = new NewPolygonFeedbackClass();
                    mNewPolygonFeedback.Display = pMapCtr.ActiveView.ScreenDisplay;
                    mNewPolygonFeedback.Start(mapPoint);
                }
                else
                {
                    mapPoint = pMapCtr.ToMapPoint(X, Y);
                    mNewPolygonFeedback.AddPoint(mapPoint);
                }
            }           
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolDeleteNodeByArea.OnMouseMove implementation
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (mNewPolygonFeedback != null && pMapCtr != null)
            {
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                mNewPolygonFeedback.MoveTo(mapPoint);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolDeleteNodeByArea.OnMouseUp implementation          
        }
        public override void OnDblClick()
        {
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            if (mNewPolygonFeedback != null)
            {
                IPolygon polygon = mNewPolygonFeedback.Stop();               
                ITinSelection tinselection = pTinEdit as ITinSelection;
                tinselection.SelectByArea(esriTinElementType.esriTinNode, polygon, true, true, esriTinSelectionType.esriTinSelectionAdd);
                pTinEdit.DeleteSelectedNodes();
                mNewPolygonFeedback = null;
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr != null)
                {
                    pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
            base.OnDblClick();
        }
        public override bool Deactivate()
        {
            mNewPolygonFeedback.Stop();
            mNewPolygonFeedback = null;
            return base.Deactivate();
        }
        #endregion
    }
}
