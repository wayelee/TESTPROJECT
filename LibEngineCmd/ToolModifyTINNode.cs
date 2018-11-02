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
    /// Summary description for ToolModefyTINNode.
    /// </summary>
    [Guid("1ec4d772-c8f9-4242-a153-d1d30824effa")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolModefyTINNode")]
    public sealed class ToolModifyTINNode : BaseTool
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
         public ITinLayer pTinLayer;
        public IMap pMap;
        //删除的节点
        ITinNode pNode ;
        //显示选中节点feedback
        IMovePointFeedback pMovePtFeedBack = new MovePointFeedbackClass();
        //修改窗口
        FrmModifyTINNode frmModifyNode = new FrmModifyTINNode();
        public ToolModifyTINNode()
        { 
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "修改TIN节点";  //localizable text 
            base.m_message = "修改TIN节点";  //localizable text
            base.m_toolTip = "修改TIN节点";  //localizable text
            base.m_name = "CustomCE.ToolModefyTINNode";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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
            frmModifyNode.Owner =
                   System.Windows.Forms.Form.FromChildHandle(User32API.GetCurrentWindowHandle()) as System.Windows.Forms.Form;

           
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
            // TODO: Add ToolModefyTINNode.OnClick implementation
            // TODO: Add ToolDeleteTINNode.OnClick implementation
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                frmModifyNode.Show();
                pMovePtFeedBack.Display = pMapCtr.ActiveView.ScreenDisplay;
                IPoint pt = new PointClass();
                pMovePtFeedBack.Start(pt, pt);
            }
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolModefyTINNode.OnMouseDown implementation
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            frmModifyNode.m_pTinEdit = pTinEdit;
            frmModifyNode.m_pTinNode = pNode;
            frmModifyNode.setDoubleInputValue(pNode.Z);
            frmModifyNode.setBttonEnable(true);
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            frmModifyNode.m_pMapCtr = pMapCtr;
            pMovePtFeedBack.Stop();
            //IPoint pt = new PointClass();
            //pMovePtFeedBack.Start(pt, pt);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolModefyTINNode.OnMouseMove implementation
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            ITinAdvanced pTinAdvanced = (ITinAdvanced)pTin;
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                ITinNode pTINNode = new TinNodeClass();
                double distance = 0;
                pTinAdvanced.QueryNearestNode(mapPoint, pTINNode, ref distance);
                pNode = pTINNode;
                IPoint pt = new PointClass();
                pt.PutCoords(pNode.X, pNode.Y);
                pMovePtFeedBack.MoveTo(pt);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolModefyTINNode.OnMouseUp implementation
        }
        public override bool Deactivate()
        {
            frmModifyNode.Hide();
            frmModifyNode.setBttonEnable(false);
            pMovePtFeedBack.Stop();
            return true;
        }
        #endregion
    }
}
