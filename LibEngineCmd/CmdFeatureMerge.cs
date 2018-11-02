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
using System.Windows.Forms;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdFeatureMerge.
    /// </summary>
    [Guid("65f18dc1-10bc-414d-b733-5a586831e0d6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdFeatureMerge")]
    public sealed class CmdFeatureMerge : BaseCommand
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

        public CmdFeatureMerge()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "合并要素";  //localizable text
            base.m_message = "合并要素";  //localizable text 
            base.m_toolTip = "合并要素";  //localizable text 
            base.m_name = "CustomCE.CmdFeatureMerge";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            FrmFeatureMerge frm = new FrmFeatureMerge();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (MergeFeatures(frm.m_nFeatureID))
                {
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }

        public override bool Enabled
        {
            get
            {
                IMapControl3 mapCtl = ClsGlobal.GetMapControl(m_hookHelper);
                if (mapCtl == null) return false;

                IEngineEditor pEngineEdit = new EngineEditorClass();
                IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
                IFeatureLayer targetLayer = pEEditLayers.TargetLayer;

                if (targetLayer == null) return false;
                if (targetLayer.FeatureClass == null) return false;

                IFeatureSelection featureSelection = targetLayer as IFeatureSelection;
                ISelectionSet selectionSet = featureSelection.SelectionSet;
                if (selectionSet.Count < 2) return false;

                return true;

            }
        }
        #endregion
        private bool MergeFeatures(int nFeatureID)
        {
            IEngineEditor pEngineEdit = new EngineEditorClass();
            IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
            IFeatureLayer targetLayer = pEEditLayers.TargetLayer;
            IFeatureClass featureClass = targetLayer.FeatureClass;

            IFeatureSelection featureSelection = targetLayer as IFeatureSelection;
            ISelectionSet selectionSet = featureSelection.SelectionSet;

            if (selectionSet.Count < 2) return false;
            pEngineEdit.StartOperation();
            try
            {
                object miss = Type.Missing;
                if (targetLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint) return false;

                IFeature featureTarget = featureClass.GetFeature(nFeatureID);
                if (featureTarget == null) return false;

                ITopologicalOperator top = featureTarget.ShapeCopy as ITopologicalOperator;

                int nID = -1;
                IEnumIDs enumIDs = selectionSet.IDs;
                enumIDs.Reset();
                while ((nID = enumIDs.Next()) >= 0)
                {
                    if (nID == nFeatureID) continue;
                    IFeature feature = featureClass.GetFeature(nID);
                    top = top.Union(feature.Shape) as ITopologicalOperator;
                }
                featureTarget.Shape = top as IGeometry;
                featureTarget.Store();

                //删除其余要素
                enumIDs.Reset();
                while ((nID = enumIDs.Next()) >= 0)
                {
                    if (nID == nFeatureID) continue;
                    IFeature feature = featureClass.GetFeature(nID);
                    feature.Delete();
                }

                pEngineEdit.StopOperation("Merge features");
                return true;
            }
            catch (System.Exception ex)
            {
                pEngineEdit.AbortOperation();

            }

            return false;
        }
    }
 
}
