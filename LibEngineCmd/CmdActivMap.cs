using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using System.Collections;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdActivMap.
    /// </summary>
    [Guid("caf03a00-6583-46b0-ae2e-d6a471d66d1f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdActivMap")]
    public sealed class CmdActivMap : BaseCommand
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

        public CmdActivMap()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "¼¤»î";  //localizable text 
            base.m_message = "¼¤»î";  //localizable text
            base.m_toolTip = "¼¤»î";  //localizable text
            base.m_name = "CustomCE.CmdActivMap";   //unique id, non-localizable (e.g. "MyCategory_MyTool")

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

            // TODO:  Add other initialization code
        }

        public override bool Enabled
        {
            get
            {
                // TODO: Add CmdNorthArrow.OnClick implementation   
                IPageLayoutControl2 pLayeoutctr = (m_hookHelper.Hook) as IPageLayoutControl2;
                if (pLayeoutctr == null)
                {
                    return false;
                }                 
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdActivMap.OnClick implementation
           IPageLayoutControl2 pLayeoutctr = (m_hookHelper.Hook) as IPageLayoutControl2;
           //if (pLayeoutctr != null && pLayeoutctr.CustomProperty != null)
           //{
           //    ArrayList al = pLayeoutctr.CustomProperty as ArrayList;
           //    IMap pMap = al[1] as IMap;
           //    IMapControl2 pmapctr = al[0] as IMapControl2;
           //    while (!pLayeoutctr.ActiveView.FocusMap.Equals(pMap))
           //    {
           //        pLayeoutctr.PageLayout.FocusNextMapFrame();
           //    }
           //    pmapctr.Map = pMap;
               
           //}
        }

        #endregion
    }
}
