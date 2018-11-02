using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdStopEditTinLayer.
    /// </summary>
    [Guid("fa5ac110-f45e-474e-a57e-182aa253fdbd")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdStopEditTinLayer")]
    public sealed class CmdStopEditTinLayer : BaseCommand
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
        //Ä¿±êÍ¼²ã
        public ITinLayer pTinLayer;
        public IMap pMap;
        public CmdStopEditTinLayer()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "Í£Ö¹±à¼­Tin";  //localizable text
            base.m_message = "Í£Ö¹±à¼­Tin";  //localizable text 
            base.m_toolTip = "Í£Ö¹±à¼­";  //localizable text 
            base.m_name = "CustomCE.CmdStopEditTinLayer";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            if (pMapCtr != null)
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
                    return pTinEdit.IsInEditMode;
                }
            }
        }
        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdStopEditTinLayer.OnClick implementation
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            
            if (MessageBox.Show("ÊÇ·ñ±£´æ?", "ÌáÊ¾", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pTinEdit.StopEditing(true);
            }
            else
            {
                pTinEdit.StopEditing(false);
            }
        }

        #endregion
    }
}
