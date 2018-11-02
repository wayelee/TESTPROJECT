using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdTurnOnAllLayer.
    /// </summary>
    [Guid("4c26e77c-8dfc-4fa1-9299-77851cff4bf4")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdTurnOnAllLayer")]
    public sealed class CmdTurnOnAllLayer : BaseCommand
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
        private IMap m_Map = null;

        public CmdTurnOnAllLayer()
        {
            //
            // TODO: Define values for the public properties
            //m_MapCtl = mapCtl;
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "打开所有图层";  //localizable text
            base.m_message = "打开所有图层";  //localizable text 
            base.m_toolTip = "打开所有图层";  //localizable text 
            base.m_name = "CustomCE.CmdTurnOnAllLayer";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            if (m_hookHelper.ActiveView is IMap)
            {
                if (((IMapControl3)m_hookHelper.Hook).CustomProperty is IMap)
                {
                    m_Map = ((IMapControl3)m_hookHelper.Hook).CustomProperty as IMap;
                }
            }
            else if (m_hookHelper.ActiveView is IPageLayout)
            {
                if (((IPageLayoutControl3)m_hookHelper.Hook).CustomProperty is IMap)
                {
                    m_Map = ((IPageLayoutControl3)m_hookHelper.Hook).CustomProperty as IMap;
                }
            }
            
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdTurnOnAllLayer.OnClick implementation
           if (m_hookHelper.ActiveView is IMap)
           {
               if (((IMapControl3)m_hookHelper.Hook) .CustomProperty is IMap)
               {
                   m_Map = ((IMapControl3)m_hookHelper.Hook).CustomProperty as IMap ;
               }
           }
           else if (m_hookHelper.ActiveView is IPageLayout)
           {
               if (((IPageLayoutControl3)m_hookHelper.Hook).CustomProperty is IMap)
               {
                   m_Map = ((IPageLayoutControl3)m_hookHelper.Hook).CustomProperty as IMap;
               }
           }

            if (m_Map == null)
            {
                return;
            }
            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                
                m_Map.get_Layer(i).Visible = true;
            }
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.Hook is IMapControl3 || m_hookHelper.Hook is IPageLayoutControl3)
                {
                    
                    if (m_Map == null)
                    {
                        return false;
                    }

                    if (m_Map.LayerCount > 0)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
