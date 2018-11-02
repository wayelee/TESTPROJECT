using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdStartEditTinLayer.
    /// </summary>
    [Guid("c9a6837d-efda-402d-b920-953437d21cbb")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdStartEditTinLayer")]
    public sealed class CmdStartEditTinLayer : BaseCommand
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

        public CmdStartEditTinLayer()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "编辑Tin";  //localizable text
            base.m_message = "开始编辑Tin";  //localizable text 
            base.m_toolTip = "开始编辑";  //localizable text 
            base.m_name = "CustomCE.CmdStartEditTinLayer";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
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
            if(pMapCtr != null)
            {
                pMap = pMapCtr.Map;
            }
            IPageLayoutControl pLayoutCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IPageLayoutControl;
            if (pLayoutCtr != null)
            {
                pMap = pLayoutCtr.ActiveView.FocusMap;
            }       
            // TODO:  Add other initialization code
        }

        public override bool Enabled
        {
            get
            {
                // TODO: Add CmdNorthArrow.OnClick implementation   
                if (pTinLayer == null)
                {
                    return false;
                }
                else
                {
                    ITin pTin = pTinLayer.Dataset;
                    ITinEdit pTinEdit = pTin as ITinEdit;
                    return !pTinEdit.IsInEditMode;
                }       
            }
        }
        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdStartEditTinLayer.OnClick implementation
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.StartEditing();
        }

        #endregion
    }
}
