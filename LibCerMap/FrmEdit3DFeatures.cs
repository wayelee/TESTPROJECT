using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry; 
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmEdit3DFeatures : OfficeForm
    {
        public ISceneControl m_SceneCtrl;
        public IMapControl2 m_MapCtrl;
       public object psecontrol = null;
       public object pmapcontrol = null;
       public ITinLayer pTinLayer = null;
        public FrmEdit3DFeatures()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public IFeature m_Feature;
        private void FrmEdit3DFeatures_Load(object sender, EventArgs e)
        {
           // ISelection pSelection = m_MapCtrl.Map.FeatureSelection;
           // m_MapCtrl = ((AxMapControl)pmapcontrol).Object as IMapControl2;
            if (m_SceneCtrl == null)
            {
                m_SceneCtrl = ((AxSceneControl)psecontrol).Object as ISceneControl;
            }
        }

        private void buttonXXDecrease_Click(object sender, EventArgs e)
        {
            ISelection pSelection = null;
            // ISelection pSelection 
            if (m_SceneCtrl != null)
            {
                pSelection = m_SceneCtrl.Scene.FeatureSelection;
            }           
            if (m_MapCtrl != null)
            {  
                pSelection = m_MapCtrl.Map.FeatureSelection;
            }
           
            //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
            //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
            //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
            //如果要查看其他数据，必须要进行以下转换才可以：
            IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup; 
            pSelectionsetup.AllFields = true;//这里是关键
            IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature; 
            IFeature pF = pFeatureCollection.Next();
            while (pF != null)
            {
                if ((pF.Shape is IMultiPatch))
                {
                    int OID = pF.OID;
                    IFeatureClass pFC = pF.Table as IFeatureClass;
                    IQueryFilter pQF = new QueryFilterClass();
                    pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString() ; 
                    IFeatureCursor pFCursor = pFC.Update(pQF,false);
                    IFeature pFeature = pFCursor.NextFeature();      

                //    StartEditing(pFeature);
                    IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                    ITransform3D pTf3D = pMpatch as ITransform3D;        

                    pTf3D.Move3D(doubleInputXIncrement.Value *-1, 0, 0);

                    try
                    {
                        if (!double.IsNaN(Convert.ToDouble(labelXX.Text)))
                        {
                            setXLabel(Convert.ToDouble(labelXX.Text) - doubleInputXIncrement.Value);
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }


                    pFeature.Shape = pMpatch as IGeometry; 
                    pFeature.Store(); 
                  //  StopEditing(pFeature);              
                    AxMapControl ms = pmapcontrol as AxMapControl;                     
                   // ms.Refresh();
                    ms.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
                    //刷新三维视图
                     for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                     {
                         ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                         if (player is IFeatureLayer)
                         {
                             IFeatureLayer pFlayer = player as IFeatureLayer;
                             if (pFlayer.FeatureClass.Equals(pFC))  
                             {
                                 IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                 pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                 //ps.Refresh();
                             }
                         }
                     }
                }               
                pF = pFeatureCollection.Next();
            }                      
        }

        private void buttonXXIncrease_Click(object sender, EventArgs e)
        {
            ISelection pSelection = null;
            // ISelection pSelection 
            if (m_SceneCtrl != null)
            {
                pSelection = m_SceneCtrl.Scene.FeatureSelection;
            }
            if (m_MapCtrl != null)
            {
                pSelection = m_MapCtrl.Map.FeatureSelection;
            }
            //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
            //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
            //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
            //如果要查看其他数据，必须要进行以下转换才可以：
            IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
            pSelectionsetup.AllFields = true;//这里是关键
            IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
            IFeature pF = pFeatureCollection.Next();
            while (pF != null)
            {
                if ((pF.Shape is IMultiPatch))
                {
                    int OID = pF.OID;
                    IFeatureClass pFC = pF.Table as IFeatureClass;
                    IQueryFilter pQF = new QueryFilterClass();
                    pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString();
                    IFeatureCursor pFCursor = pFC.Update(pQF, false);
                    IFeature pFeature = pFCursor.NextFeature();
                    try
                    {
                        //    StartEditing(pFeature);
                        IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                        ITransform3D pTf3D = pMpatch as ITransform3D;

                        pTf3D.Move3D(doubleInputXIncrement.Value, 0, 0);

                        try
                        { 
                            if (!double.IsNaN(Convert.ToDouble(labelXX.Text)))
                            {
                                setXLabel(Convert.ToDouble(labelXX.Text) + doubleInputXIncrement.Value);
                            }
                        }
                        catch (System.Exception ex)
                        {
                        	
                        }
                       
                        

                        pFeature.Shape = pMpatch as IGeometry;
                        pFeature.Store();
                        //  StopEditing(pFeature);              
                        AxMapControl ms = pmapcontrol as AxMapControl;
                        ms.Refresh();
                        //刷新三维视图
                        for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                        {
                            ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                            if (player is IFeatureLayer)
                            {
                                IFeatureLayer pFlayer = player as IFeatureLayer;
                                if (pFlayer.FeatureClass.Equals(pFC))
                                {
                                    IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                    //ps.Refresh();
                                }
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Message == "The spatial index grid size is invalid.")
                        {
                            IFeatureClassLoad pFCL = pFC as IFeatureClassLoad;
                            pFCL.LoadOnlyMode = true;
                            pFeature.Store();
                            pFCL.LoadOnlyMode = false;
                        }
                        else
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
               
                }
                pF = pFeatureCollection.Next();
            }                      
        }

        private void buttonXYDecrease_Click(object sender, EventArgs e)
        {
            ISelection pSelection = null;
            // ISelection pSelection 
            if (m_SceneCtrl != null)
            {
                pSelection = m_SceneCtrl.Scene.FeatureSelection;
            }
            if (m_MapCtrl != null)
            {
                pSelection = m_MapCtrl.Map.FeatureSelection;
            }
            //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
            //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
            //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
            //如果要查看其他数据，必须要进行以下转换才可以：
            IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
            pSelectionsetup.AllFields = true;//这里是关键
            IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
            IFeature pF = pFeatureCollection.Next();
            while (pF != null)
            {
                if ((pF.Shape is IMultiPatch))
                {
                    int OID = pF.OID;
                    IFeatureClass pFC = pF.Table as IFeatureClass;
                    IQueryFilter pQF = new QueryFilterClass();
                    pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString();
                    IFeatureCursor pFCursor = pFC.Update(pQF, false);
                    IFeature pFeature = pFCursor.NextFeature();

                    //    StartEditing(pFeature);
                    IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                    ITransform3D pTf3D = pMpatch as ITransform3D;

                    pTf3D.Move3D(0,doubleInputYIncrement.Value * -1,  0);
                    try
                    {
                        if (!double.IsNaN(Convert.ToDouble(labelXY.Text)))
                        {
                            setYLabel(Convert.ToDouble(labelXY.Text) - doubleInputYIncrement.Value);
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }

                    pFeature.Shape = pMpatch as IGeometry;
                    pFeature.Store();
                    //  StopEditing(pFeature);              
                    AxMapControl ms = pmapcontrol as AxMapControl;
                    ms.Refresh();
                    //刷新三维视图
                    for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                    {
                        ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                        if (player is IFeatureLayer)
                        {
                            IFeatureLayer pFlayer = player as IFeatureLayer;
                            if (pFlayer.FeatureClass.Equals(pFC))
                            {
                                IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                //ps.Refresh();
                            }
                        }
                    }
                }
                pF = pFeatureCollection.Next();
            }                      
        }

        private void buttonXYIncrease_Click(object sender, EventArgs e)
        {
            ISelection pSelection = null;
            // ISelection pSelection 
            if (m_SceneCtrl != null)
            {
                pSelection = m_SceneCtrl.Scene.FeatureSelection;
            }
            if (m_MapCtrl != null)
            {
                pSelection = m_MapCtrl.Map.FeatureSelection;
            }
            //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
            //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
            //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
            //如果要查看其他数据，必须要进行以下转换才可以：
            IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
            pSelectionsetup.AllFields = true;//这里是关键
            IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
            IFeature pF = pFeatureCollection.Next();
            while (pF != null)
            {
                if ((pF.Shape is IMultiPatch))
                {
                    int OID = pF.OID;
                    IFeatureClass pFC = pF.Table as IFeatureClass;
                    IQueryFilter pQF = new QueryFilterClass();
                    pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString();
                    IFeatureCursor pFCursor = pFC.Update(pQF, false);
                    IFeature pFeature = pFCursor.NextFeature();

                    //    StartEditing(pFeature);
                    IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                    ITransform3D pTf3D = pMpatch as ITransform3D;

                    pTf3D.Move3D( 0,doubleInputYIncrement.Value , 0);

                    try
                    {
                        if (!double.IsNaN(Convert.ToDouble(labelXY.Text)))
                        {
                            setYLabel(Convert.ToDouble(labelXY.Text) + doubleInputYIncrement.Value);
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }

                    pFeature.Shape = pMpatch as IGeometry;
                    pFeature.Store();
                    //  StopEditing(pFeature);              
                    AxMapControl ms = pmapcontrol as AxMapControl;
                    ms.Refresh();
                    //刷新三维视图
                    for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                    {
                        ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                        if (player is IFeatureLayer)
                        {
                            IFeatureLayer pFlayer = player as IFeatureLayer;
                            if (pFlayer.FeatureClass.Equals(pFC))
                            {
                                IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                //ps.Refresh();
                            }
                        }
                    }
                }
                pF = pFeatureCollection.Next();
            }                      
        }
      
        private void buttonXZDecrease_Click(object sender, EventArgs e)
        {
            ISelection pSelection = null;
            // ISelection pSelection 
            if (m_SceneCtrl != null)
            {
                pSelection = m_SceneCtrl.Scene.FeatureSelection;
            }
            if (m_MapCtrl != null)
            {
                pSelection = m_MapCtrl.Map.FeatureSelection;
            }
            //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
            //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
            //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
            //如果要查看其他数据，必须要进行以下转换才可以：
            IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
            pSelectionsetup.AllFields = true;//这里是关键
            IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
            IFeature pF = pFeatureCollection.Next();
            while (pF != null)
            {
                if ((pF.Shape is IMultiPatch))
                {
                    int OID = pF.OID;
                    IFeatureClass pFC = pF.Table as IFeatureClass;
                    IQueryFilter pQF = new QueryFilterClass();
                    pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString();
                    IFeatureCursor pFCursor = pFC.Update(pQF, false);
                    IFeature pFeature = pFCursor.NextFeature();

                    //    StartEditing(pFeature);
                    IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                    ITransform3D pTf3D = pMpatch as ITransform3D;

                    pTf3D.Move3D(0,0,doubleInputZIncrement.Value * -1  );

                    pFeature.Shape = pMpatch as IGeometry;
                    pFeature.Store();
                    //  StopEditing(pFeature);              
                    AxMapControl ms = pmapcontrol as AxMapControl;
                    ms.Refresh();
                    //刷新三维视图
                    for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                    {
                        ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                        if (player is IFeatureLayer)
                        {
                            IFeatureLayer pFlayer = player as IFeatureLayer;
                            if (pFlayer.FeatureClass.Equals(pFC))
                            {
                                IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                //ps.Refresh();
                            }
                        }
                    }
                }
                pF = pFeatureCollection.Next();
            }                      
        }

        private void buttonXZIncrease_Click(object sender, EventArgs e)
        {
            ISelection pSelection = null;
            // ISelection pSelection 
            if (m_SceneCtrl != null)
            {
                pSelection = m_SceneCtrl.Scene.FeatureSelection;
            }
            if (m_MapCtrl != null)
            {
                pSelection = m_MapCtrl.Map.FeatureSelection;
            }
            //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
            //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
            //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
            //如果要查看其他数据，必须要进行以下转换才可以：
            IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
            pSelectionsetup.AllFields = true;//这里是关键
            IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
            IFeature pF = pFeatureCollection.Next();
            while (pF != null)
            {
                if ((pF.Shape is IMultiPatch))
                {
                    int OID = pF.OID;
                    IFeatureClass pFC = pF.Table as IFeatureClass;
                    IQueryFilter pQF = new QueryFilterClass();
                    pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString();
                    IFeatureCursor pFCursor = pFC.Update(pQF, false);
                    IFeature pFeature = pFCursor.NextFeature();

                    //    StartEditing(pFeature);
                    IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                    ITransform3D pTf3D = pMpatch as ITransform3D;

                    pTf3D.Move3D(0, 0, doubleInputZIncrement.Value );

                    pFeature.Shape = pMpatch as IGeometry;
                    pFeature.Store();
                    //  StopEditing(pFeature);              
                    AxMapControl ms = pmapcontrol as AxMapControl;
                    ms.Refresh();
                    //刷新三维视图
                    for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                    {
                        ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                        if (player is IFeatureLayer)
                        {
                            IFeatureLayer pFlayer = player as IFeatureLayer;
                            if (pFlayer.FeatureClass.Equals(pFC))
                            {
                                IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                //ps.Refresh();
                            }
                        }
                    }
                }
                pF = pFeatureCollection.Next();
            }                      
        }
    
        
        //将feature的工作空间编辑状态打开
        public void StartEditing(IFeature pFeature)
        {
            try
            {
                IFeatureClass pFC = pFeature.Table as IFeatureClass; 
                IDataset pDataset = pFC as IDataset;               
                if (pDataset == null) return;
                // 开始编辑,并设置Undo/Redo 为可用
                IWorkspaceEdit pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
                if (!pWorkspaceEdit.IsBeingEdited())
                {
                    pWorkspaceEdit.StartEditing(true);
                    pWorkspaceEdit.EnableUndoRedo();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
       // 关闭feature的工作空间编辑状态
        public void StopEditing(IFeature pFeature)
        {
            try
            {
                IFeatureClass pFC = pFeature.Table as IFeatureClass;
                IDataset pDataset = pFC as IDataset;
                if (pDataset == null) return;

                //如果数据已被修改，则提示用户是否保存
                IWorkspaceEdit pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
                if (pWorkspaceEdit.IsBeingEdited())
                {
                    pWorkspaceEdit.StopEditing(true);
                } 
                IActiveView pActiveView = m_SceneCtrl.Scene  as IActiveView;
                pActiveView.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        private void buttonXSave_Click(object sender, EventArgs e)
        {
            
        }
        public IPoint GetMultiPatchCenter(IMultiPatch pMultiPatch)
        {
            IPoint pt = new PointClass();
            pt.PutCoords((pMultiPatch.Envelope.XMax + pMultiPatch.Envelope.XMin) / 2,
               (pMultiPatch.Envelope.YMax + pMultiPatch.Envelope.YMin) / 2);
            IZAware za = pt as IZAware;
            za.ZAware = true;
            pt.Z = (pMultiPatch.Envelope.ZMax + pMultiPatch.Envelope.ZMin) / 2;
            return pt;
        }

        private void buttonXSmaller_Click(object sender, EventArgs e)
        {
            ISelection pSelection = null;
            // ISelection pSelection 
            if (m_SceneCtrl != null)
            {
                pSelection = m_SceneCtrl.Scene.FeatureSelection;
            }
            if (m_MapCtrl != null)
            {
                pSelection = m_MapCtrl.Map.FeatureSelection;
            }
                //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
                //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
                //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
                //如果要查看其他数据，必须要进行以下转换才可以：
                IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
                pSelectionsetup.AllFields = true;//这里是关键
                IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
                IFeature pF = pFeatureCollection.Next();
                while (pF != null)
                {
                    if ((pF.Shape is IMultiPatch))
                    {
                        int OID = pF.OID;
                        IFeatureClass pFC = pF.Table as IFeatureClass;
                        IQueryFilter pQF = new QueryFilterClass();
                        pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString();
                        IFeatureCursor pFCursor = pFC.Update(pQF, false);
                        IFeature pFeature = pFCursor.NextFeature();
                        try
                        {
                            StartEditing(pFeature);

                            IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                            ITransform3D pTf3D = pMpatch as ITransform3D;

                            IPoint originPoint = GetMultiPatchCenter(pMpatch);
                            MakeZAware(originPoint as IGeometry);

                            double dbScaleCoef = dbiScaleCoef.Value;
                            pTf3D.Scale3D(originPoint, dbScaleCoef, dbScaleCoef, dbScaleCoef);
                            //pTf3D.Scale3D(originPoint, 0.5, 0.5, 0.5);
                            //  pTf3D.Move3D(0, 0, doubleInputZIncrement.Value);

                            pFeature.Shape = pMpatch as IGeometry;
                            pFeature.Store();
                            //pFCursor.UpdateFeature(pFeature);
                            StopEditing(pFeature);
                            AxMapControl ms = pmapcontrol as AxMapControl;
                            ms.Refresh();
                            
                            //刷新三维视图
                            for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                            {
                                ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                                if (player is IFeatureLayer)
                                {
                                    IFeatureLayer pFlayer = player as IFeatureLayer;
                                    if (pFlayer.FeatureClass.Equals(pFC))
                                    {
                                        IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                        //ps.Refresh();
                                    }
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            if (ex.Message == "The spatial index grid size is invalid.")
                            {
                                IFeatureClassLoad pFCL = pFC as IFeatureClassLoad;
                                pFCL.LoadOnlyMode = true;
                                pFeature.Store();
                                pFCL.LoadOnlyMode = false;
                            }
                            else
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }                        
                    }
                    pF = pFeatureCollection.Next();
                }
        }

        private void buttonXBigger_Click(object sender, EventArgs e)
        {
            ISelection pSelection = null;
            // ISelection pSelection 
            if (m_SceneCtrl != null)
            {
                pSelection = m_SceneCtrl.Scene.FeatureSelection;
            }
            if (m_MapCtrl != null)
            {
                pSelection = m_MapCtrl.Map.FeatureSelection;
            }
            //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
            //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
            //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
            //如果要查看其他数据，必须要进行以下转换才可以：
            IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
            pSelectionsetup.AllFields = true;//这里是关键
            IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
            IFeature pF = pFeatureCollection.Next();
            while (pF != null)
            {
                if ((pF.Shape is IMultiPatch))
                {
                    int OID = pF.OID;
                    IFeatureClass pFC = pF.Table as IFeatureClass;
                    IQueryFilter pQF = new QueryFilterClass();
                    pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString();
                    IFeatureCursor pFCursor = pFC.Update(pQF, false);
                    IFeature pFeature = pFCursor.NextFeature();

                    //    StartEditing(pFeature);
                    try
                    {
                        IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                        ITransform3D pTf3D = pMpatch as ITransform3D;

                        IPoint originPoint = GetMultiPatchCenter(pMpatch);
                        MakeZAware(originPoint as IGeometry);

                        double dbScaleCoef = dbiScaleCoef.Value;
                        pTf3D.Scale3D(originPoint, dbScaleCoef, dbScaleCoef, dbScaleCoef);
                        //pTf3D.Scale3D(originPoint, 2, 2, 2);
                        //  pTf3D.Move3D(0, 0, doubleInputZIncrement.Value);

                        pFeature.Shape = pMpatch as IGeometry;
                        pFeature.Store();
                        //  StopEditing(pFeature);              
                        AxMapControl ms = pmapcontrol as AxMapControl;
                        ms.Refresh();
                        //刷新三维视图
                        for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                        {
                            ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                            if (player is IFeatureLayer)
                            {
                                IFeatureLayer pFlayer = player as IFeatureLayer;
                                if (pFlayer.FeatureClass.Equals(pFC))
                                {
                                    IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                    //ps.Refresh();
                                }
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                    	 if (ex.Message == "The spatial index grid size is invalid.")
                            {
                                IFeatureClassLoad pFCL = pFC as IFeatureClassLoad;
                                //pFCL.LoadOnlyMode = true;
                                //pFeature.Store();
                                //pFCL.LoadOnlyMode = false;
                                MessageBox.Show(ex.Message + " 无法再放大！");
                            }
                            else
                            {
                                MessageBox.Show(ex.Message);
                            }
                    }
                  
                }
                pF = pFeatureCollection.Next();
            }                      
        }

        private static IPoint ConstructPoint3D(double x, double y, double z)
        {
           IPoint point = ConstructPoint2D(x, y);
            point.Z = z;
 
            MakeZAware(point as IGeometry);
 
           return point;
        }

        private static IPoint ConstructPoint2D(double x, double y)
        {
            IPoint point = new PointClass();
            point.PutCoords(x, y);

            return point;
        }
        
        private static void MakeZAware(IGeometry geometry)
        {
           IZAware zAware = geometry as IZAware;
            zAware.ZAware = true;
        }

        private void buttonXDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ISelection pSelection = null;
                // ISelection pSelection 
                if (m_SceneCtrl != null)
                {
                    pSelection = m_SceneCtrl.Scene.FeatureSelection;
                }
                if (m_MapCtrl != null)
                {
                    pSelection = m_MapCtrl.Map.FeatureSelection;
                }
                //从Map.FeatureSelection获得ISelection不能读到Feature的其他属性，
                //这是因为从axMapControl1.Map.FeatureSelection QI到IEnumFeature 时，
                //ArcGIS中FeatureSelection默认的时候只存入Feature 的Shape，而不是整个Feature的字段数据。
                //如果要查看其他数据，必须要进行以下转换才可以：
                IEnumFeatureSetup pSelectionsetup = pSelection as IEnumFeatureSetup;
                pSelectionsetup.AllFields = true;//这里是关键
                IEnumFeature pFeatureCollection = pSelectionsetup as IEnumFeature;
                IFeature pF = pFeatureCollection.Next();
                while (pF != null)
                {
                    if ((pF.Shape is IMultiPatch))
                    {
                        int OID = pF.OID;
                        IFeatureClass pFC = pF.Table as IFeatureClass;
                        IQueryFilter pQF = new QueryFilterClass();
                        pQF.WhereClause = "\"OBJECTID\" = " + OID.ToString();
                        IFeatureCursor pFCursor = pFC.Update(pQF, false);
                        IFeature pFeature = pFCursor.NextFeature();

                        StartEditing(pFeature);
                        //IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
                        //ITransform3D pTf3D = pMpatch as ITransform3D;

                        //IPoint originPoint = GetMultiPatchCenter(pMpatch);
                        //MakeZAware(originPoint as IGeometry);

                        //pTf3D.Scale3D(originPoint, 0.5, 0.5, 0.5);
                        ////  pTf3D.Move3D(0, 0, doubleInputZIncrement.Value);

                        //pFeature.Shape = pMpatch as IGeometry;
                        pFeature.Delete();
                        //pFeature.Store();
                        //pFCursor.UpdateFeature(pFeature);
                        StopEditing(pFeature);
                        AxMapControl ms = pmapcontrol as AxMapControl;
                        ms.Refresh();
                        //刷新三维视图
                        for (int i = 0; i < m_SceneCtrl.Scene.LayerCount; i++)
                        {
                            ILayer player = m_SceneCtrl.Scene.get_Layer(i);
                            if (player is IFeatureLayer)
                            {
                                IFeatureLayer pFlayer = player as IFeatureLayer;
                                if (pFlayer.FeatureClass.Equals(pFC))
                                {
                                    IActiveView pActiveView = m_SceneCtrl.Scene as IActiveView;
                                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFlayer, null);
                                    //ps.Refresh();
                                }
                            }
                        }
                    }
                    pF = pFeatureCollection.Next();
                }
            }
            catch (System.AccessViolationException ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public void setXLabel(double v)
        {
            labelXX.Text = v.ToString();
        }
        public void setYLabel(double v)
        {
            labelXY.Text = v.ToString();
        }
        public void setZLabel(double v)
        {
            labelXZ.Text = v.ToString();
        }
    }
}
