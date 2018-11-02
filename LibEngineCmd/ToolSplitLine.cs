using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolSplitLine.
    /// </summary>
    [Guid("4b1e1c72-758a-47a4-8247-5888d51a8e55")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolSplitLine")]
    public sealed class ToolSplitLine : BaseTool
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
      
        private IEngineEditor m_engineEditor;
        private IEngineEditLayers m_editLayer;

        IEngineSnapEnvironment snapEnvironment;
        IEngineFeatureSnapAgent featureSnapAgent = null;
        IPoint pt;
        IMovePointFeedback pMPfeedback;
        ITool oldtool;
        private System.Windows.Forms.Cursor m_InsertVertexCursor;
        private System.Windows.Forms.Cursor m_DeleteVertexCursor;
        public ToolSplitLine()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "打断线";  //localizable text 
            base.m_message = "打断线";  //localizable text
            base.m_toolTip = "打断线";  //localizable text
            base.m_name = "CustomCE.ToolSplitLine";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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
            try
            {
                //m_InsertVertexCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("VertexCommands_CS.InsertVertexCursor.cur"));
                //m_DeleteVertexCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("VertexCommands_CS.DeleteVertexCursor.cur"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Cursor");
            }           
        }

        #region Overridden Class Methods


        /// <summary>
        /// Return the cursor to be used by the tool
        /// </summary>
        //public override int Cursor
        //{
        //    get
        //    {
        //        int iHandle = 0;

        //        iHandle = m_InsertVertexCursor.Handle.ToInt32();
        //        //switch (m_lSubType)
        //        //{
        //        //    case 1:
        //        //        iHandle = m_InsertVertexCursor.Handle.ToInt32();
        //        //        break;

        //        //    case 2:
        //        //        iHandle = m_DeleteVertexCursor.Handle.ToInt32();
        //        //        break;
        //        //}

        //        return (iHandle);
        //    }
        //}

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                m_engineEditor = new EngineEditorClass(); //this class is a singleton
                m_editLayer = m_engineEditor as IEngineEditLayers;

            }
            catch
            {
                m_hookHelper = null;
            }

            // TODO:  Add ToolSplitLine.OnCreate implementation
        }


        /// <summary>
        /// Perform checks so that the tool is enabled appropriately
        /// </summary>
        public override bool Enabled
        {
            get
            {
                //check whether Editing 
                if (m_engineEditor.EditState == esriEngineEditState.esriEngineStateNotEditing)
                {
                    return false;
                }

                //check for appropriate geometry types
                esriGeometryType geomType = m_editLayer.TargetLayer.FeatureClass.ShapeType;
                if ((geomType != esriGeometryType.esriGeometryPolygon) & (geomType != esriGeometryType.esriGeometryPolyline))
                {
                    return false;
                }

                //check that only one feature is currently selected
                IFeatureSelection featureSelection = m_editLayer.TargetLayer as IFeatureSelection;
                ISelectionSet selectionSet = featureSelection.SelectionSet;
                if (selectionSet.Count != 1)
                {
                    return false;
                }            
                return true;
            }
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolSplitLine.OnClick implementation
            //Find the Modify Feature task and set it as the current task
            IEngineEditTask editTask = m_engineEditor.GetTaskByUniqueName("ControlToolsEditing_ModifyFeatureTask");
            m_engineEditor.CurrentTask = editTask;

            IEngineEditLayers editLayers = m_editLayer;
            snapEnvironment = m_engineEditor as IEngineSnapEnvironment;
            pt = new PointClass();
            //Check the user is editing; otherwise, there will be no snap agent loaded.
            if (editLayers.TargetLayer == null)
            {
                System.Windows.Forms.MessageBox.Show("Please start an edit session");
                return;
            }

            ////Create a feature snap agent.
            IEngineFeatureSnapAgent featureSnapAgent = new EngineFeatureSnap();
            IFeatureClass layerFeatureClass = editLayers.TargetLayer.FeatureClass;
            featureSnapAgent.FeatureClass = layerFeatureClass;
            featureSnapAgent.HitType = esriGeometryHitPartType.esriGeometryPartBoundary;

            //Activate only the snap agent for the target layer.
            snapEnvironment.AddSnapAgent(featureSnapAgent);
            pMPfeedback = new MovePointFeedbackClass();
            pMPfeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;
            pMPfeedback.Start(pt, pt);

            IToolbarBuddy toolbarbuddy = (IToolbarBuddy)((IToolbarControl)m_hookHelper.Hook).Buddy;
            oldtool = toolbarbuddy.CurrentTool;
           
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            try
            {
                pt = ((m_hookHelper.ActiveView.ScreenDisplay).DisplayTransformation).ToMapPoint(X, Y);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            bool ddd = snapEnvironment.SnapPoint(pt);
            if (pMPfeedback !=null  )
            {
                pMPfeedback.Stop();
            }            
            pMPfeedback = null;

            bool hasCut = false;
            ISet newFeaturesSet = null;
            try
            {
                IFeatureSelection featureSelection = m_editLayer.TargetLayer as IFeatureSelection;
                ISelectionSet selectionSet = featureSelection.SelectionSet;
                IEnumIDs enumIDs = selectionSet.IDs;
                int iD = enumIDs.Next();
                while (iD != -1) //-1 is reutned after the last valid ID has been reached      
                {
                    m_engineEditor.StartOperation();
                    IFeatureClass featureClass = m_editLayer.TargetLayer.FeatureClass;
                    IFeature feature = featureClass.GetFeature(iD);
                    // 判断点是否在线上，不是则跳过
                    ITopologicalOperator pto = feature.Shape as ITopologicalOperator;
                    IGeometry pgeometry = pto.Intersect(pt, esriGeometryDimension.esriGeometry0Dimension);
                    if (pgeometry.IsEmpty == true)
                    {
                        iD = enumIDs.Next();
                        continue;
                    }
                    IFeatureEdit featureedit = feature as IFeatureEdit;
                    /*ISet*/
                    newFeaturesSet = featureedit.Split(pgeometry);
                    if (newFeaturesSet != null)
                    {
                        newFeaturesSet.Reset();
                        hasCut = true;
                        break;
                    }
                    iD = enumIDs.Next();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (hasCut)
            {
                //如果操作成功，选中切割的两条线
                //IFeatureSelection featureSelection = m_editLayer.TargetLayer as IFeatureSelection;
                //ISelectionSet selectionSet = featureSelection.SelectionSet;
                //for (int i = 0; i < newFeaturesSet.Count; i++)
                //{
                //    selectionSet.Add(((IFeature)newFeaturesSet.Next()).OID);
                //}
                //selectionSet.Refresh();

                //Refresh the display including modified layer and any previously selected component. 
                IActiveView activeView = m_engineEditor.Map as IActiveView;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);
                activeView.Refresh();
                //Complete the edit operation.
                m_engineEditor.StopOperation("Split a line");
                // 将当前工具设置为选择工具
                IToolbarControl toolbarctl = m_hookHelper.Hook as IToolbarControl;
                //ICommand com = new ControlsEditingEditToolClass();
                //com.OnCreate(m_mapControl.Object);
                //this.m_mapControl.CurrentTool = com as ITool; 
                for (int i = 0; i < toolbarctl.Count; i++)
                {
                    IToolbarItem tbi = toolbarctl.GetItem(i);
                    if (tbi.Command!=null && tbi.Command.Name.Equals("ControlToolsEditing_Edit"))
                    {
                        tbi.Command.OnClick();// = true;
                        toolbarctl.CurrentTool = tbi.Command as ITool;
                        IToolbarBuddy toolbarbuddy = (IToolbarBuddy)((IToolbarControl)m_hookHelper.Hook).Buddy;
                        break;
                    }

                }
                //  操作成功后将当前工具置为以前的工具
                //IToolbarBuddy toolbarbuddy = (IToolbarBuddy)((IToolbarControl)m_hookHelper.Hook).Buddy;
                //toolbarbuddy.CurrentTool = oldtool;
                //((IToolbarControl)m_hookHelper.Hook).SetBuddyControl(toolbarbuddy);
                this.Deactivate();

            }
            else
            {
                m_engineEditor.AbortOperation();
                MessageBox.Show("切割点不在线上，未能成功切割选择的线段");
                //重新开始选点
                this.OnClick();
            }
            base.OnMouseDown(Button, Shift, X, Y);
        }
        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            //IDisplayTransformation 
            try
            {
                pt = ((m_hookHelper.ActiveView.ScreenDisplay).DisplayTransformation).ToMapPoint(X, Y);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            bool ddd = snapEnvironment.SnapPoint(pt);
            if (pMPfeedback != null)
            {
                pMPfeedback.MoveTo(pt);
            }
            if (ddd)
            {
                // MessageBox.Show("catched!");
            }
        }

        public override bool Deactivate()
        {

            for (int i = 0; i < snapEnvironment.SnapAgentCount; i++)
            {
                if (snapEnvironment.get_SnapAgent(i).Equals(featureSnapAgent))
                {
                    snapEnvironment.RemoveSnapAgent(i);
                }
            }
            return base.Deactivate();
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolSplitLine.OnMouseUp implementation
        }
        #endregion
    }
}
