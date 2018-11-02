using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using LibCerMap;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdCreateParrallel.
    /// </summary>
    [Guid("e0266c59-5d70-4338-9eb7-516e6be49e03")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdCreateParrallel")]
    public sealed class CmdCreateParrallel : BaseCommand
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

        public CmdCreateParrallel()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "绘制平行线";  //localizable text
            base.m_message = "绘制平行线";  //localizable text 
            base.m_toolTip = "绘制平行线";  //localizable text 
            base.m_name = "CustomCE.CmdCreateParrallel";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdCreateParrallel.OnClick implementation
            IEngineEditor pEngineEdit = new EngineEditorClass();
            IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
            IFeatureLayer targetLayer = pEEditLayers.TargetLayer;
                       

            IFeatureSelection featureSelection = targetLayer as IFeatureSelection;
            ISelectionSet selectionSet = featureSelection.SelectionSet;
            if (selectionSet.Count > 0)
            {
                ICursor cursor;
                selectionSet.Search(null, true, out cursor);
                IFeatureCursor featureCursor = cursor as IFeatureCursor;
                IFeature feature = null;
                FrmParallelLineOffset frm = new FrmParallelLineOffset();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    while ((feature = featureCursor.NextFeature()) != null)
                    {
                        IPolyline pPolyline = ConstructOffset(feature.Shape as IPolyline, frm.offset);
                        IFeature pFeature = targetLayer.FeatureClass.CreateFeature();
                        pFeature.Shape = pPolyline;
                        pFeature.Store();
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }

                }
            }
        }


        public IPolyline ConstructOffset(IPolyline inPolyline, double offset)
        {
            if (inPolyline == null || inPolyline.IsEmpty)
            {
                return null;
            }
            object Missing = Type.Missing;
            IConstructCurve constructCurve = new PolylineClass();
            constructCurve.ConstructOffset(inPolyline, offset, ref Missing, ref Missing);
            return constructCurve as IPolyline;
        }


        public override bool Enabled
        {
            get
            {
                IMapControl3 mapCtl = ClsGlobal.GetMapControl(m_hookHelper);
                if (mapCtl == null) return false;
                //IMap map = ClsGlobal.GetFocusMap(m_hookHelper);
                //if (map == null) return false;
                IEngineEditor pEngineEdit = new EngineEditorClass();
                IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
                IFeatureLayer targetLayer = pEEditLayers.TargetLayer;

                if (targetLayer == null) return false;
                if (targetLayer.FeatureClass == null) return false;
                if (targetLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline) return false;
                IFeatureSelection featureSelection = targetLayer as IFeatureSelection;
                ISelectionSet selectionSet = featureSelection.SelectionSet;
                if (selectionSet.Count < 1) return false;

                return true;

            }
        }
        #endregion
    }
}
