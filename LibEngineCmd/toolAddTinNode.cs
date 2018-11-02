using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using LibCerMap;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for toolAddTinNode.
    /// </summary>
    [Guid("5c6f53c2-fbe5-4864-bbaf-ffd8ec77335f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolAddTinNode")]
    public sealed class ToolAddTinNode : BaseTool
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
       
        //添加节点窗口
        FrmAddTINNode frmAddTINNode = new FrmAddTINNode();
        //添加的节点
        IPoint pAddedPoint;
        public ToolAddTinNode()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "添加TIN节点";  //localizable text 
            base.m_message = "添加TIN节点";  //localizable text
            base.m_toolTip = "添加TIN节点";  //localizable text
            base.m_name = "CustomCE.ToolAddTinNode";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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
            frmAddTINNode.Owner =
                   System.Windows.Forms.Form.FromChildHandle(User32API.GetCurrentWindowHandle()) as System.Windows.Forms.Form;
            // TODO:  Add toolAddTinNode.OnCreate implementation
        }

        public override bool Enabled
        {
            get
            {
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr == null)
                {
                    return false;
                }
                // TODO: Add CmdNorthArrow.OnClick implementation   
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
            // TODO: Add toolAddTinNode.OnClick implementation
            frmAddTINNode.Show();
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add toolAddTinNode.OnMouseDown implementation
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.AddPointZ(pAddedPoint, 0);
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            IPageLayoutControl pLayoutCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IPageLayoutControl;
            if (pLayoutCtr != null)
            {
                pLayoutCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            ISurface pSurface = ((ITinAdvanced)pTin).Surface;
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            //在mapctr操作
            if (pMapCtr != null)
            {
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                IZAware za = mapPoint as IZAware;
                za.ZAware = true;
                double zVal;
                if (frmAddTINNode.bFromSurface == true)
                {
                    zVal = pSurface.GetElevation(mapPoint);
                    if (double.IsNaN(zVal))
                    {
                        zVal = 0;
                    }
                    frmAddTINNode.SetdoubleInputHeightValue(zVal);
                }
                else
                {
                    zVal = frmAddTINNode.dHeight;
                    if (double.IsNaN(zVal))
                    {
                        zVal = 0;
                    }
                }
                mapPoint.Z = zVal;
                pAddedPoint = mapPoint;
            }
            //在layout控件操作取不到z值
            else
            {
                IPageLayoutControl pLayoutCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IPageLayoutControl;
                //if (pLayoutCtr.ActiveView.FocusMap.Equals(pMap))

                IPoint mapPoint = pLayoutCtr.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                IZAware za = mapPoint as IZAware;
                za.ZAware = true;
                double zVal;
                if (frmAddTINNode.bFromSurface == true)
                {
                    zVal = pSurface.GetElevation(mapPoint);
                    if (double.IsNaN(zVal))
                    {
                        zVal = 0;
                    }
                    frmAddTINNode.SetdoubleInputHeightValue(zVal);
                }
                else
                {
                    zVal = frmAddTINNode.dHeight;
                    if (double.IsNaN(zVal))
                    {
                        zVal = 0;
                    }
                }
                mapPoint.Z = zVal;
                pAddedPoint = mapPoint;
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add toolAddTinNode.OnMouseUp implementation
        }

        public override bool Deactivate()
        {
            frmAddTINNode.Hide();
            return true;
        }
        #endregion
    }
}
