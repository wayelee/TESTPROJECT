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
    /// Summary description for CmdCreateSelectionLayer.
    /// </summary>
    [Guid("4677ccfc-f9d5-46e3-b62a-84374a3ba35d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdCreateSelectionLayer")]
    public sealed class CmdCreateSelectionLayer : BaseCommand
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
        private ITOCControlDefault m_pTocCtl = null;
        public CmdCreateSelectionLayer(ITOCControlDefault tocCtl)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "创建选择集图层";  //localizable text
            base.m_message = "创建选择集图层";  //localizable text 
            base.m_toolTip = "创建选择集图层";  //localizable text 
            base.m_name = "CustomCE.CmdCreateSelectionLayer";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            m_pTocCtl = tocCtl;
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

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            try
            {
                //ILayer player = ClsGlobal.GetSelectedLayer(m_hookHelper);
                IBasicMap map = null;
                ILayer layer = null;
                ClsGlobal.GetSelectedMapAndLayer(m_pTocCtl, ref map, ref layer);
                if (map ==null || layer ==null)
                {
                    return;
                }

                IFeatureLayer pFLayer = (IFeatureLayer)layer;
                IFeatureLayerDefinition pFLDef = (IFeatureLayerDefinition)pFLayer;
                IFeatureSelection pFSel = (IFeatureSelection)pFLayer;
                ISelectionSet pSSet = pFSel.SelectionSet;
                if (pSSet.Count > 0)
                {
                    IFeatureLayer pSelFL = pFLDef.CreateSelectionLayer(layer.Name + "Selection", true, null, null); 
                    map.AddLayer(pSelFL);
                    IActiveView activiView = map as IActiveView;
                    activiView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    m_pTocCtl.Update();
                }
            }
            catch (Exception ex)
            {
                ;
            }
   
        }

        public override bool Enabled
        {
            get
            {
                ILayer layer = ClsGlobal.GetSelectedLayer(m_hookHelper);
                if (layer != null)
                {
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLaeyr = layer as IFeatureLayer;
                        if (featureLaeyr.FeatureClass != null)
                        {
                            IFeatureSelection featSelection = featureLaeyr as IFeatureSelection;
                            if (featSelection.SelectionSet.Count > 0) return true;
                        }
                    }
                }

                return false;
            }
        }
        #endregion

     }
}
