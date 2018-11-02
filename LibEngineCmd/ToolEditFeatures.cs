using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using System.Collections.Generic;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using LibCerMap;
using System.Collections;
using ESRI.ArcGIS.Analyst3D;


namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolEditFeatures.
    /// </summary>
    [Guid("a5eb6dbb-ad29-4180-b755-20783a6e6085")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolEditFeatures")]
    public sealed class ToolEditFeatures : BaseTool
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
        public object m_controls;
        public ITinLayer pTinLayer;
        FrmEdit3DFeatures FrmEditFeatures;
        public ToolEditFeatures(object controls)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "编辑物体";  //localizable text 
            base.m_message = "编辑物体";  //localizable text
            base.m_toolTip = "编辑物体";  //localizable text
            base.m_name = "CustomCE.ToolEditFeatures";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            m_controls = controls;
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

            // TODO:  Add ToolEditFeatures.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolEditFeatures.OnClick implementation
            ISceneControl pSceneCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as ISceneControl;
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
           // FrmEdit3DFeatures FrmEditFeatures = new FrmEdit3DFeatures();
            if (FrmEditFeatures == null)
            {
                FrmEditFeatures = new FrmEdit3DFeatures();
            }
            if (FrmEditFeatures.IsDisposed == true)
            {
                FrmEditFeatures = new FrmEdit3DFeatures();
            }
            FrmEditFeatures.psecontrol = ((ArrayList)m_controls)[1];
            FrmEditFeatures.pmapcontrol = ((ArrayList)m_controls)[0];
            FrmEditFeatures.pTinLayer = pTinLayer;
            FrmEditFeatures.m_SceneCtrl = pSceneCtr;
            FrmEditFeatures.m_MapCtrl = pMapCtr;
            FrmEditFeatures.Show();
            FrmEditFeatures.Owner =
                  System.Windows.Forms.Form.FromHandle(User32API.GetCurrentWindowHandle()) as System.Windows.Forms.Form;
        }
        public override bool Enabled
        {
            get
            {
                // TODO: Add CmdNorthArrow.OnClick implementation   
                if ((m_hookHelper.ActiveView) is IMap && pTinLayer != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolEditFeatures.OnMouseDown implementation
            try
            {
                ISceneControl pSceneCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as ISceneControl;
                if (pSceneCtr!= null)
                {
                    IPoint po;
                    object owner, obj;
                    pSceneCtr.SceneGraph.Locate(pSceneCtr.SceneViewer, X, Y, esriScenePickMode.esriScenePickGeography, true, out po, out owner, out obj);//po就是得到的点

                    //方法2
                    IHit3DSet pHit3dSet = null;
                    pSceneCtr.SceneGraph.LocateMultiple(pSceneCtr.SceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickGeography, true, out pHit3dSet);

                    //po = pHit3D.Point;//po就是得到的点    

                    //如果想得到某图层元素，可以看看下面代码

                    //  int index = 2;
                    if (pHit3dSet == null)
                        return;
                    pHit3dSet.OnePerLayer();
                    IHit3D pHit3D = (IHit3D)pHit3dSet.Hits.get_Element(0);
                    IFeature pFeature = (IFeature)pHit3D.Object;
                    if (pFeature == null)
                        return;
                    ILayer pLayer = owner as ILayer;
                    if (pLayer == null)
                        return;
                    if (pLayer.Name != "Crater" && pLayer.Name != "NonCrater")
                    {
                        return;
                    }
                    IFeatureClass pFeatureClass = ((IFeatureLayer)pLayer).FeatureClass;
                    pSceneCtr.Scene.ClearSelection();
                    pSceneCtr.Scene.SelectFeature(pLayer, pFeature);
                    IActiveView pActiveView = pSceneCtr.Scene as IActiveView;
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                    IMultiPatch pMP = pFeature.Shape as IMultiPatch;
                    FrmEditFeatures.setXLabel( (pMP.Envelope.XMax + pMP.Envelope.XMin) / 2);
                    FrmEditFeatures.setYLabel((pMP.Envelope.YMax + pMP.Envelope.YMin) / 2);
                } 
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr != null)
                {
                    IMap pMap = pMapCtr.Map;
                    IPoint po = pMapCtr.ToMapPoint(X, Y);
                    ISelectionEnvironment pSelectionEnv = new SelectionEnvironmentClass();
                    pMap.SelectByShape(po, pSelectionEnv, true);
                    //for (int i = 0; i < pMap.LayerCount; i++ )
                    //{
                    //    ILayer pLayer = pMap.get_Layer(i);
                        
                    //    if (pLayer.Name == "Crater")
                    //    {
                    //        pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                    //    }
                    //    if (pLayer.Name =="NonCrater")
                    //    {
                    //        pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                    //    }
                    //}
                    pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    IEnumFeatureSetup pSelectionsetup = pMap.FeatureSelection as IEnumFeatureSetup;
                    pSelectionsetup.AllFields = true;//这里是关键
                    IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
                    IFeature pF = pFeatureCollection.Next();
                    if (pF !=null&&pF.Shape is IMultiPatch)
                    {
                         IMultiPatch pMP = pF.Shape as IMultiPatch;
                         FrmEditFeatures.setXLabel((pMP.Envelope.XMax + pMP.Envelope.XMin) / 2);
                         FrmEditFeatures.setYLabel((pMP.Envelope.YMax + pMP.Envelope.YMin) / 2);
                    }
                   

                    //ISelection pSelection =pMap.FeatureSelection;
                    ////从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
                    ////这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
                    ////ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
                    ////如果要查看其他数据，必须要进行以下转换才可以：
                    //IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
                    //pSelectionsetup.AllFields = true;//这里是关键
                    //IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
                    //IFeature pF = pFeatureCollection.Next();                    
                    //if (pF != null)
                    //{
                    //    pF.Class
                    //}

                }
            }
            catch (System.Exception ex)
            {     
            }
          

        }

        //public override void OnMouseMove(int Button, int Shift, int X, int Y)
        //{
        //    // TODO:  Add ToolEditFeatures.OnMouseMove implementation
        //}

        //public override void OnMouseUp(int Button, int Shift, int X, int Y)
        //{
        //    // TODO:  Add ToolEditFeatures.OnMouseUp implementation
        //}
        #endregion
    }
}
