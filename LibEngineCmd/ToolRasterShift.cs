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

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolRasterShift.
    /// </summary>
    [Guid("398531c6-d78a-47cb-b07b-1900efb3bc13")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolRasterShift")]
    public sealed class ToolRasterShift : BaseTool
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
        public IRasterLayer pRasterLayer;
        public IMap pMap;


        public IPoint DownPoint;
        public IMoveEnvelopeFeedback pMoveEnvelopeFeedback;
        public ToolRasterShift()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "平移";  //localizable text 
            base.m_message = "平移";  //localizable text
            base.m_toolTip = "平移";  //localizable text
            base.m_name = "CustomCE.ToolRasterShift";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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

            // TODO:  Add ToolRasterShift.OnCreate implementation
        }

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
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolRasterShift.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolRasterShift.OnMouseDown implementation
           // DownPoint = new PointClass();
            IMapControl3 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;
            if (pMapCtr != null)
            {
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);

                pMoveEnvelopeFeedback = new MoveEnvelopeFeedbackClass();
                pMoveEnvelopeFeedback.Display = pMapCtr.ActiveView.ScreenDisplay;
                pMoveEnvelopeFeedback.Start(pRasterLayer.VisibleExtent, mapPoint);
                DownPoint = mapPoint;
            }           
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolRasterShift.OnMouseMove implementation
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
            if (pMapCtr != null)
            {
                IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                if (pMoveEnvelopeFeedback != null)
                {
                    pMoveEnvelopeFeedback.MoveTo(mapPoint);
                }
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolRasterShift.OnMouseUp implementation
            pMoveEnvelopeFeedback.Stop();
             IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
             if (pMapCtr != null)
             {
                 IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                 IGeoReference pGR = pRasterLayer as IGeoReference;
                 pGR.Shift(mapPoint.X - DownPoint.X, mapPoint.Y - DownPoint.Y);
                 pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
             }
        }
        #endregion
    }
}
