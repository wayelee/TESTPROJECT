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

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolDeleteTINNode.
    /// </summary>
    [Guid("7db020e9-7cf3-4a78-a027-0733fb950da3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolDeleteTINNode")]
    public sealed class ToolDeleteTINNode : BaseTool
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
        //删除的节点
        ITinNode pNode;
        //显示选中节点feedback
        IMovePointFeedback pMovePtFeedBack = new MovePointFeedbackClass();
       
        public ToolDeleteTINNode()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "删除TIN节点";  //localizable text 
            base.m_message = "删除TIN节点";  //localizable text
            base.m_toolTip = "删除TIN节点";  //localizable text
            base.m_name = "CustomCE.ToolDeleteTINNode";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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

            // TODO:  Add toolAddTinNode.OnCreate implementation
        }

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
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolDeleteTINNode.OnClick implementation
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                pMovePtFeedBack.Display = pMapCtr.ActiveView.ScreenDisplay;
                IPoint pt = new PointClass();
                pMovePtFeedBack.Start(pt, pt);
            }

        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolDeleteTINNode.OnMouseDown implementation
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.DeleteNode(pNode.Index);
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            pMovePtFeedBack.Stop();
            IPoint pt = new PointClass();
            pMovePtFeedBack.Start(pt, pt);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolDeleteTINNode.OnMouseMove implementation
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            ITinAdvanced pTinAdvanced = (ITinAdvanced)pTin;
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                ITinNode pTINNode = new TinNodeClass() ;
                double distance = 0;
                pTinAdvanced.QueryNearestNode(mapPoint, pTINNode, ref distance);
                pNode = pTINNode;
                IPoint pt = new PointClass();
                pt.PutCoords(pNode.X,pNode.Y);
                pMovePtFeedBack.MoveTo(pt);                
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolDeleteTINNode.OnMouseUp implementation
        }
        public override bool Deactivate()
        {           
            pMovePtFeedBack.Stop();
            return true;
        }
        #endregion
    }
}
