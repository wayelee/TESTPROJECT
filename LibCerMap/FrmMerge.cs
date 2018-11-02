using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using DevComponents.DotNetBar;


namespace LibCerMap
{
    public partial class FrmMerge : OfficeForm
    {
        public FrmMerge()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public IMap m_Map;
        public IScene m_Scene; 
        private void buttonOK_Click(object sender, EventArgs e)
        {
            ITinLayer pTinlayer= null;
            IFeatureLayer pfeaturelayer = null;
 
            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                ILayer player = m_Map.get_Layer(i);
                if (cmbTINLayer.SelectedItem != null)
                {
                    if (player.Name == cmbTINLayer.SelectedItem.ToString())
                    {
                        pTinlayer = player as ITinLayer;
                    }
                }
                if (cmbCraterLayer.SelectedItem != null)
                {
                    if (player.Name == cmbCraterLayer.SelectedItem.ToString())
                    {
                        pfeaturelayer = player as IFeatureLayer;
                    }
                }
            } 
           // MergeCraterToTin(pTinlayer, pfeaturelayer);
            MergeCraterToTin2(pTinlayer, pfeaturelayer);
            pfeaturelayer = null;
            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                ILayer player = m_Map.get_Layer(i);
                if (cmbNonCraterLayer.SelectedItem != null)
                {
                    if (player.Name == cmbNonCraterLayer.SelectedItem.ToString())
                    {
                        pfeaturelayer = player as IFeatureLayer;
                    }
                }
            }
            MergeNonCraterToTin(pTinlayer, pfeaturelayer);
        }
        

        private void FrmMerge_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                ILayer pLayer = m_Map.get_Layer(i);
                if (pLayer is ITinLayer)
                {
                    cmbTINLayer.Items.Add(pLayer.Name);
                }
                if (pLayer is IFeatureLayer)
                {
                    if (((IFeatureLayer)pLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryMultiPatch)
                    {
                        cmbCraterLayer.Items.Add(pLayer.Name);
                        cmbNonCraterLayer.Items.Add(pLayer.Name);
                    }
                }
            }
            //textBoxXPath.Text = @"C:\Users\Administrator\Desktop\sampleAnalysis\testtin";
        }
        // 将一个撞击坑添加到图层
        public void MergeCraterToTin2(ITinLayer pTinLayer, IFeatureLayer pFeatureLayer)
        {
            if (pTinLayer == null || pFeatureLayer == null)
            {
                return;
            }
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.StartEditing();
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IFeatureCursor pCursor = pFeatureClass.Search(null, false);
            IFeature pFeature = pCursor.NextFeature();
            List<IPolygon> MultiPatchList = new List<IPolygon>();
            ITinSelection pTinSelection = pTin as ITinSelection;
            ITinSurface pSurface = pTin as ITinSurface;
            while (pFeature != null)
            {
                MultiPatchList.Clear();

                IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                IEnvelope pEnvelope = pProjGeometry.Envelope;
                //计算中心点
                IPoint CenterPoint = new PointClass();
                CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                //计算坑缘点
                IPoint EdgePoint = GetTheMaxZValueInMultiPatch(pMultiPatch);
                //计算碗状半径
                double Radii = CalDistaceBetweenTwoPoints(CenterPoint, EdgePoint);

                ITin CraterTin = ConvertMultiPatchToTin(pMultiPatch);
                IFeatureClass MemFeatureClass = CreateMemFeatureClass();
                ITinSurface3 pTinSurface3 = pTin as ITinSurface3;
                IGeometry intersectpolygon = new PolygonClass();
                pTinEdit.StopEditing(false); 
                pTinSurface3.Intersect((ITinSurface)CraterTin, null, MemFeatureClass, "volume", "surfaceArea", "code");
                IFeatureCursor pCursor2 = MemFeatureClass.Search(null, false);
                IFeature pFeature2 = pCursor2.NextFeature();
                while (pFeature2 != null)
                {
                    int findex = pFeature2.Fields.FindFieldByAliasName("code");
                    string code = pFeature2.get_Value(findex).ToString();
                    if (code == "1")
                    {
                        IGeometry pPolygon = pFeature2.Shape;
                        IGeometry pOutputGeometry = new MultiPatchClass();
                        pSurface.InterpolateShape(pPolygon, out pOutputGeometry);
                        //pTinEdit.StartEditing();
                        //pTinEdit.AddShapeZ(pOutputGeometry, esriTinSurfaceType.esriTinHardLine, 0);                        
                        //pTinEdit.StopEditing(true);
                        MultiPatchList.Add(pOutputGeometry as IPolygon);
                    }
                    pFeature2 = pCursor2.NextFeature();
                } //while (pFeature2 != null)


                pTinEdit.StartEditing();
                for (int i = 0; i < MultiPatchList.Count; i++)
                {
                    if (MultiPatchList[i] == null)
                    {
                        continue;
                    }
                    if (MultiPatchList[i].IsClosed == false)
                    {
                        continue;
                    }
                    if ((MultiPatchList[i].Length == 0))
                    {
                        continue;
                    }
                    pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)MultiPatchList[i], true, true, esriTinSelectionType.esriTinSelectionNew);
                    pTinEdit.DeleteSelectedNodes();
                    pTinEdit.AddShapeZ(MultiPatchList[i], esriTinSurfaceType.esriTinHardLine, 0); 
                }

                List<IPoint> PointList = new List<IPoint>();
                PointList.Clear();
                IPointCollection pPointCollection = pMultiPatch as IPointCollection;
                for (int i = 0; i < pPointCollection.PointCount; i++)
                {
                    IPoint pt = pPointCollection.get_Point(i);
                    IZAware za = pt as IZAware;
                    za.ZAware = true;
                    IEnvelope pExtent = pTin.Extent;

                    double zval = pSurface.GetElevation(pt);
                    double rad = CalDistaceBetweenTwoPoints(pt, CenterPoint);
                    if (PointInExtent(pt, pExtent) && (rad - Radii < Radii * 0.1 || Math.Abs(pt.Z - EdgePoint.Z) < 0.1))
                    {
                        if (pt.Z < zval)
                        {
                            //  pTinEdit.AddPointZ(pt, 0);
                            PointList.Add(pt);
                        }
                    }
                }
                // pTinEdit.DeleteSelectedNodes();
                for (int i = 0; i < PointList.Count; i++)
                {
                    pTinEdit.AddPointZ(PointList[i], 0);
                }
                try
                {
                    object vtMissing = true;
                    pTinEdit.SaveAs(textBoxXPath.Text, ref vtMissing);
                    pTinEdit.StopEditing(false);
                }
                catch (SystemException ee)
                {
                    if (ee.Message == "The specified file or dataset already exists.")
                    {
                        pTinEdit.Save();
                        pTinEdit.StopEditing(true);
                    }
                    else
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
                pFeature = pCursor.NextFeature();
            }// while (pFeature != null)

        }
        //将一层的撞击坑添加到tin，如果撞击坑重叠会有问题
        private void MergeCraterToTin(ITinLayer pTinLayer, IFeatureLayer pFeatureLayer)
        {
            if(pTinLayer == null || pFeatureLayer == null)
            {
                return;
            }

            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.StartEditing();
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IFeatureCursor pCursor = pFeatureClass.Search(null,false);
            IFeature pFeature = pCursor.NextFeature();
            List<IPolygon> MultiPatchList = new List<IPolygon>();
            ITinSelection pTinSelection = pTin as ITinSelection;
            ITinSurface pSurface = pTin as ITinSurface;
            while (pFeature != null)
            {
                IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                IEnvelope pEnvelope = pProjGeometry.Envelope;
               //Polygon pPolygon = pProjGeometry as IPolygon;
                //计算中心点
                IPoint CenterPoint = new PointClass();
                CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                //计算坑缘点
                IPoint EdgePoint = GetTheMaxZValueInMultiPatch(pMultiPatch);
                //计算碗状半径
                double Radii = CalDistaceBetweenTwoPoints(CenterPoint, EdgePoint);
                
                //pTinSelection.SelectByEnvelope(esriTinElementType.esriTinNode, pEnvelope, true, true, esriTinSelectionType.esriTinSelectionNew);
                              
               // pTinEdit.DeleteSelectedNodes();
                
                 /*
                IGeometry pOutputGeometry = new MultiPatchClass();
                pSurface.InterpolateShape(pProjGeometry, out pOutputGeometry);
                // IMultiPatch ClipMultiPatch = pOutputGeometry as IMultiPatch;
                IPolygon ClipPolygon = pOutputGeometry as IPolygon;
                IPolyline pPolyline = ClipPolygon as IPolyline;
              //  pTinEdit.AddShapeZ(ClipPolygon, esriTinSurfaceType.esriTinHardLine, 0);
                ITopologicalOperator3 pTO = pMultiPatch as ITopologicalOperator3;
               // IGeometry pIntersectGeometry = pTO.Intersect(ClipPolygon, esriGeometryDimension.esriGeometry2Dimension);

                pFeature = pCursor.NextFeature();
                IMultiPatch patch2 = pFeature.Shape as IMultiPatch;
                //IGeometry pIntersectGeometry = pTO.IntersectMultidimension(patch2);
                //IGeometry pIntersectGeometry = pTO.Intersect(ClipPolygon, esriGeometryDimension.esriGeometry3Dimension);
                //IGeometry pIntersectGeometry = pTO.Intersect(patch2, esriGeometryDimension.esriGeometry0Dimension);
                //IPolyline polylin = new PolylineClass();
                //IPointCollection pPC = pPolyline as IPointCollection;
                //IPointCollection pPCIntersect = pIntersectGeometry as IPointCollection;
                //for(int i = 0; i< pPCIntersect.PointCount;i++)
                //{
                //    pPC.AddPoint(pPCIntersect.get_Point(i));
                //}
                //((ITopologicalOperator)polylin).Simplify();
                //pOutputGeometry = new MultiPatchClass();
                //pSurface.InterpolateShape(polylin, out pOutputGeometry);
                pTinEdit.AddShapeZ(pOutputGeometry, esriTinSurfaceType.esriTinHardLine, 0);
               */
                //pOutputGeometry = new MultiPatchClass();
                //pSurface.InterpolateShape(pIntersectGeometry, out pOutputGeometry);
                //pTinEdit.AddShapeZ(pOutputGeometry, esriTinSurfaceType.esriTinHardLine, 0);


                ITin CraterTin = ConvertMultiPatchToTin(pMultiPatch);
                IFeatureClass MemFeatureClass = CreateMemFeatureClass();
                ITinSurface3 pTinSurface3 = pTin as ITinSurface3;
                IGeometry intersectpolygon = new PolygonClass();
                pTinEdit.StopEditing(false);
                //  pTinSurface3.Intersect((ITinSurface)CraterTin, intersectpolygon, MemFeatureClass, "volume", "surfaceArea", "code");
                pTinSurface3.Intersect((ITinSurface)CraterTin, null, MemFeatureClass, "volume", "surfaceArea", "code");
                IFeatureCursor pCursor2 = MemFeatureClass.Search(null, false);
                IFeature pFeature2 = pCursor2.NextFeature();
                while (pFeature2 != null)
                {
                    int findex = pFeature2.Fields.FindFieldByAliasName("code");
                    string code = pFeature2.get_Value(findex).ToString();
                    if (code == "1")
                    {
                        IGeometry pPolygon = pFeature2.Shape;
                        IGeometry pOutputGeometry = new MultiPatchClass();
                        pSurface.InterpolateShape(pPolygon, out pOutputGeometry);
                        //pTinEdit.StartEditing();
                        //pTinEdit.AddShapeZ(pOutputGeometry, esriTinSurfaceType.esriTinHardLine, 0);                        
                        //pTinEdit.StopEditing(true);
                        MultiPatchList.Add(pOutputGeometry as IPolygon);
                    }
                    pFeature2 = pCursor2.NextFeature();
                } //while (pFeature2 != null)
                pFeature = pCursor.NextFeature();
            }// while (pFeature != null)

            pTinEdit.StartEditing();
            for (int i = 0; i < MultiPatchList.Count; i++)
            {
                if(MultiPatchList[i] == null)
                {
                    continue;
                }
                if(MultiPatchList[i].IsClosed== false)
                {
                    continue;
                }
                if( (MultiPatchList[i].Length == 0))
                {
                    continue;
                }
                pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)MultiPatchList[i], true, true, esriTinSelectionType.esriTinSelectionNew);
                pTinEdit.DeleteSelectedNodes();
                pTinEdit.AddShapeZ(MultiPatchList[i], esriTinSurfaceType.esriTinHardLine,0);
                    //MultiPatchList[i].XYFootprint
            }
            pCursor = pFeatureClass.Search(null, false);            
            pFeature = pCursor.NextFeature();
           
            while (pFeature != null)
            {
                IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                IEnvelope pEnvelope = pProjGeometry.Envelope;
                //Polygon pPolygon = pProjGeometry as IPolygon;
                //计算中心点
                IPoint CenterPoint = new PointClass();
                CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                //计算坑缘点
                IPoint EdgePoint = GetTheMaxZValueInMultiPatch(pMultiPatch);
                //计算碗状半径
                double Radii = CalDistaceBetweenTwoPoints(CenterPoint, EdgePoint);
                //pFeature = pCursor.NextFeature();           
                
                List<IPoint> PointList = new List<IPoint>();
                PointList.Clear();
                IPointCollection pPointCollection = pMultiPatch as IPointCollection;
                for (int i = 0; i < pPointCollection.PointCount; i++)
                {
                    IPoint pt = pPointCollection.get_Point(i);
                    IZAware za = pt as IZAware;
                    za.ZAware = true;
                    IEnvelope pExtent = pTin.Extent;                     

                    double zval = pSurface.GetElevation(pt);
                    double rad = CalDistaceBetweenTwoPoints(pt,CenterPoint);
                    if (PointInExtent(pt, pExtent) && (rad - Radii < Radii * 0.1 || Math.Abs(pt.Z - EdgePoint.Z)<0.1))
                    {
                        if (pt.Z < zval)
                        {
                          //  pTinEdit.AddPointZ(pt, 0);
                            PointList.Add(pt);
                        }
                    }
                }
               // pTinEdit.DeleteSelectedNodes();
                for (int i = 0; i < PointList.Count; i++)
                {
                    pTinEdit.AddPointZ(PointList[i], 0);
                }
                    pFeature = pCursor.NextFeature();
            }
            try
            {
                object vtMissing = true;
                pTinEdit.SaveAs(textBoxXPath.Text, ref vtMissing);
                pTinEdit.StopEditing(false);                 
            }
            catch(SystemException ee)
            {
                if (ee.Message == "The specified file or dataset already exists.")
                {
                    pTinEdit.Save();
                }
                else
                {
                    MessageBox.Show(ee.Message);
                }
            }
     
        }//MergeCraterToTin

        public void MergeNonCraterToTin(ITinLayer pTinLayer, IFeatureLayer pFeatureLayer)
        {
            if (pTinLayer == null || pFeatureLayer == null)
            {
                return;
            }

            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.StartEditing();
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IFeatureCursor pCursor = pFeatureClass.Search(null, false);
            IFeature pFeature = pCursor.NextFeature();
            List<IPolygon> MultiPatchList = new List<IPolygon>();
            ITinSelection pTinSelection = pTin as ITinSelection;
            ITinSurface pSurface = pTin as ITinSurface;
            while (pFeature != null)
            {
                IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                IEnvelope pEnvelope = pProjGeometry.Envelope;
                //Polygon pPolygon = pProjGeometry as IPolygon;
                //计算中心点
                IPoint CenterPoint = new PointClass();
                CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                double CenterZValue = (pMultiPatch.Envelope.ZMax + pMultiPatch.Envelope.ZMin) / 2;

                IPolygon printPolygon = pProjGeometry as IPolygon;
                if (printPolygon != null)
                {
                    pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)pProjGeometry, true, true, esriTinSelectionType.esriTinSelectionNew);
                }

                ITin nonCraterTin = ConvertMultiPatchToTin2(pMultiPatch);
                IFeatureClass MemFeatureClass = CreateMemFeatureClass();
                ITinSurface3 pTinSurface3 = pTin as ITinSurface3;
                IGeometry intersectpolygon = new PolygonClass();
                pTinEdit.StopEditing(false);
                //  pTinSurface3.Intersect((ITinSurface)CraterTin, intersectpolygon, MemFeatureClass, "volume", "surfaceArea", "code");
                pTinSurface3.Intersect((ITinSurface)nonCraterTin, null, MemFeatureClass, "volume", "surfaceArea", "code");
                IFeatureCursor pCursor2 = MemFeatureClass.Search(null, false);
                IFeature pFeature2 = pCursor2.NextFeature();
                 while (pFeature2 != null)
                {
                    int findex = pFeature2.Fields.FindFieldByAliasName("code");
                    string code = pFeature2.get_Value(findex).ToString();
                    if (code == "-1")
                    {
                        IGeometry pPolygon = pFeature2.Shape;
                        IGeometry pOutputGeometry = new MultiPatchClass();
                        pSurface.InterpolateShape(pPolygon, out pOutputGeometry); 
                        MultiPatchList.Add(pOutputGeometry as IPolygon);
                    }
                    pFeature2 = pCursor2.NextFeature();
                } //while (pFeature2 != null)
                pFeature = pCursor.NextFeature();
            }// while (pFeature != null)

            pTinEdit.StartEditing();
            for (int i = 0; i < MultiPatchList.Count; i++)
            {
                if (MultiPatchList[i] == null)
                {
                    continue;
                }
                if (MultiPatchList[i].IsClosed == false)
                {
                    continue;
                }
                if ((MultiPatchList[i].Length == 0))
                {
                    continue;
                }
                pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)MultiPatchList[i], true, true, esriTinSelectionType.esriTinSelectionNew);
                pTinEdit.DeleteSelectedNodes();
                pTinEdit.AddShapeZ(MultiPatchList[i], esriTinSurfaceType.esriTinHardLine, 0);
                //MultiPatchList[i].XYFootprint
            }
            pCursor = pFeatureClass.Search(null, false);
            pFeature = pCursor.NextFeature();

            while (pFeature != null)
            {
                IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                IEnvelope pEnvelope = pProjGeometry.Envelope;
                List<IPoint> PointList = new List<IPoint>();
                PointList.Clear();
                IPointCollection pPointCollection = pMultiPatch as IPointCollection;
                //计算中心点
                IPoint CenterPoint = new PointClass();
                CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                double CenterZValue = (pMultiPatch.Envelope.ZMax + pMultiPatch.Envelope.ZMin) / 2;

                for (int i = 0; i < pPointCollection.PointCount; i++)
                {
                    IPoint pt = pPointCollection.get_Point(i);
                    IZAware za = pt as IZAware;
                    za.ZAware = true;
                    IEnvelope pExtent = pTin.Extent;

                    double zval = pSurface.GetElevation(pt); 
                    if (PointInExtent(pt, pExtent) && pt.Z >= CenterZValue)
                    {
                        if (pt.Z >= zval)
                        {
                            //  pTinEdit.AddPointZ(pt, 0);
                            PointList.Add(pt);
                        }
                    }
                } 
                for (int i = 0; i < PointList.Count; i++)
                {
                    pTinEdit.AddPointZ(PointList[i], 0);
                }
                pFeature = pCursor.NextFeature();
            }
            try
            {
                object vtMissing = true;        
                pTinEdit.SaveAs(textBoxXPath.Text, ref vtMissing);               
                pTinEdit.StopEditing(false);
            }
            catch (SystemException ee)
            {
                if (ee.Message == "The specified file or dataset already exists.")
                {
                    pTinEdit.Save();
                }
                else
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private bool PointInExtent(IPoint pt, IEnvelope Extent)
        {
            if (pt.X >= Extent.XMin && pt.X <= Extent.XMax && pt.Y>= Extent.YMin && pt.Y <= Extent.YMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private double GetMaxZInMultiPatch(IMultiPatch pMultiPatch)
        {
            return pMultiPatch.Envelope.ZMax;
        }
        private IPoint GetTheMaxZValueInMultiPatch(IMultiPatch pMultiPatch)
        {
            IPointCollection pPointCollection = pMultiPatch as IPointCollection;
            IPoint ppoint=null;
            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                IPoint pt = pPointCollection.get_Point(i);
                if (pt.Z == GetMaxZInMultiPatch(pMultiPatch))
                {
                    ppoint = pt;
                    break;
                }
            }
            return ppoint;
        }

        private double CalDistaceBetweenTwoPoints(IPoint pt1, IPoint pt2)
        {
            return Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X) + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y));
        }

      

        //创建内存featureclass
        private IFeatureClass CreateMemFeatureClass()
        {
            IFeatureClass pFeatureClass = null;     
            /*
            ClsGDBDataCommon GDBdataacess = new ClsGDBDataCommon(); 
            IWorkspaceFactory pworkspaceFactory = new InMemoryWorkspaceFactoryClass();
            ESRI.ArcGIS.Geodatabase.IWorkspaceName pworkspaceName = pworkspaceFactory.Create(null, "MyWorkspace", null, 0);
            ESRI.ArcGIS.esriSystem.IName pname = (IName)pworkspaceName;
            ESRI.ArcGIS.Geodatabase.IWorkspace inmemWor = (IWorkspace)pname.Open();
            IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
           // GDBdataacess.CreateFeatureClass((IWorkspace2)inmemWor,
            //IFeatureDataset pFeatureDataset = pFeatureWorkspace.CreateFeatureDataset("Mem", null);
           */
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

            //设置字段   
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
           
            //创建类型为几何类型的字段   
            pFieldEdit.Name_2 = "shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            /*
            //不定义空间参考会报异常
            //为esriFieldTypeGeometry类型的字段创建几何定义，包括类型和空间参照    
            IGeometryDef pGeoDef = new GeometryDefClass();     //The geometry definition for the field if IsGeometry is TRUE.   
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            pGeoDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
            pGeoDefEdit.SpatialReference.SetDomain(-200, 200, -200, 200);//没有这句就抛异常来自HRESULT：0x8004120E。
           // pGeoDefEdit.SpatialReference_2 = pSPF;
            pFieldEdit.GeometryDef_2 = pGeoDef;
            pFieldsEdit.AddField(pField);
            */

            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.AvgNumPoints_2 = 5;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon; ;
            pGeoDefEdit.GridCount_2 = 1;
            pGeoDefEdit.HasM_2 = false;
            pGeoDefEdit.HasZ_2 = false;
            //pGeoDefEdit.SpatialReference_2 = SpatialRef;
            pGeoDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。
            pGeoDefEdit.SpatialReference.SetDomain(-8000000, 8000000, -800000, 8000000);//没有这句就抛异常来自HRESULT：0x8004120E。

            pFieldEdit.Name_2 = "SHAPE";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            pFieldEdit.GeometryDef_2 = pGeoDef;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Required_2 = true;
            pFieldsEdit.AddField(pField);



            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "volume";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;      
            pFieldsEdit.AddField(pField);
           
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "surfaceArea";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldsEdit.AddField(pField);
                
             pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "code";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldsEdit.AddField(pField);

            //IFeatureDataset pFdataset = pFeatureWorkspace.CreateFeatureDataset("mem", null);
            //pFeatureClass = pFdataset.CreateFeatureClass("", pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");            
          //  pFeatureClass = pFeatureWorkspace.CreateFeatureClass("", pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");
            
            
            IAoInitialize aoinitialize = new AoInitializeClass();
            aoinitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);
            ClsGDBDataCommon comm = new ClsGDBDataCommon();
            IWorkspace inmemWor = comm.OpenFromShapefile(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Temp\");
           // ifeatureworkspacee
            IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;           
          //  pFeatureClass = pFeatureWorkspace.CreateFeatureClass("111", pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");
            //System.IO.Path.GetRandomFileName();
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass("tempShapefile.shp");
            ITable pTable = pFeatureClass as ITable;
            pTable.DeleteSearchedRows(null); 
            IField fd = null;
            int idx = pTable.FindField("code");
            if (idx != -1)
            {
                fd = pTable.Fields.get_Field(idx);            
                pTable.DeleteField(fd);
            }
            idx = pTable.FindField("surfaceAre");
            if (idx != -1)
            {
                fd = pTable.Fields.get_Field(idx);            
                pTable.DeleteField(fd);
            }
            idx = pTable.FindField("volume");
            if (idx != -1)
            { 
                fd = pTable.Fields.get_Field(idx);
                pTable.DeleteField(fd);
            }
            return pFeatureClass;
        }
    
        //将multipatch转为tin,该方法用于将撞击坑模型转化为tin，撞击坑模型表面没有z方向的重叠，可以直接转化为tin
        private ITin ConvertMultiPatchToTin(IMultiPatch pMultiPatch)
        {
            IEnvelope pExtent = pMultiPatch.Envelope;
            ITinEdit pTinEdit = new TinClass() as ITinEdit;
            pTinEdit.InitNew(pExtent);
            IPointCollection ptCollection = pMultiPatch as IPointCollection;
            for (int i = 0; i < ptCollection.PointCount; i++)
            {
                IPoint pt = ptCollection.get_Point(i);
                pTinEdit.AddPointZ(pt, 0);
            }
            return pTinEdit as ITin;           
        }

        //将multipatch转为tin,该方法用于将石块模型转化为tin，撞击坑模型表面有z方向的重叠，不可以直接转化为tin
        //在此认为每个石块模型中心以上的表面正好都在z方向没有重叠，石块中心以下的表面都弃之不用
        private ITin ConvertMultiPatchToTin2(IMultiPatch pMultiPatch)
        {
            IEnvelope pExtent = pMultiPatch.Envelope;
            ITinEdit pTinEdit = new TinClass() as ITinEdit;
            double CenterZValue = (pMultiPatch.Envelope.ZMax + pMultiPatch.Envelope.ZMin) / 2;
            pTinEdit.InitNew(pExtent);
            IPointCollection ptCollection = pMultiPatch as IPointCollection;
            for (int i = 0; i < ptCollection.PointCount; i++)
            {
                IPoint pt = ptCollection.get_Point(i);
                if (pt.Z >= CenterZValue)
                {
                    pTinEdit.AddPointZ(pt, 0);
                }
            }
            return pTinEdit as ITin;           
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdlg = new SaveFileDialog();
            if (fdlg.ShowDialog() == DialogResult.OK && fdlg.FileName != "")
            {
                textBoxXPath.Text =  fdlg.FileName;
            }
        }

        public void MergeCraterToMemoryTin(ITinLayer pTinLayer, IFeatureLayer pFeatureLayer)
        {
            if (pTinLayer == null || pFeatureLayer == null)
            {
                return;
            }
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.StartEditing();
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IFeatureCursor pCursor = pFeatureClass.Search(null, false);
            IFeature pFeature = pCursor.NextFeature();
            List<IPolygon> MultiPatchList = new List<IPolygon>();
            ITinSelection pTinSelection = pTin as ITinSelection;
            ITinSurface pSurface = pTin as ITinSurface;
            int modelnum = 0;
            while (pFeature != null)
            {
                MultiPatchList.Clear();
                modelnum++;
                IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                IEnvelope pEnvelope = pProjGeometry.Envelope;
                //计算中心点
                IPoint CenterPoint = new PointClass();
                CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                //计算坑缘点
                IPoint EdgePoint = GetTheMaxZValueInMultiPatch(pMultiPatch);
                //计算碗状半径
                double Radii = CalDistaceBetweenTwoPoints(CenterPoint, EdgePoint);
                try
                {
                    ITin CraterTin = ConvertMultiPatchToTin(pMultiPatch);
                    IFeatureClass MemFeatureClass = CreateMemFeatureClass();
                    ITinSurface3 pTinSurface3 = pTin as ITinSurface3;
                    IGeometry intersectpolygon = new PolygonClass();
                    pTinEdit.StopEditing(false);
                    pTinSurface3.Intersect((ITinSurface)CraterTin, null, MemFeatureClass, "volume", "surfaceArea", "code");
                    IFeatureCursor pCursor2 = MemFeatureClass.Search(null, false);
                    IFeature pFeature2 = pCursor2.NextFeature();
                    while (pFeature2 != null)
                    {
                        int findex = pFeature2.Fields.FindFieldByAliasName("code");
                        string code = pFeature2.get_Value(findex).ToString();
                        if (code == "1")
                        {
                            IGeometry pPolygon = pFeature2.Shape;
                            IGeometry pOutputGeometry = new MultiPatchClass();
                            pSurface.InterpolateShape(pPolygon, out pOutputGeometry);
                            //pTinEdit.StartEditing();
                            //pTinEdit.AddShapeZ(pOutputGeometry, esriTinSurfaceType.esriTinHardLine, 0);                        
                            //pTinEdit.StopEditing(true);
                            MultiPatchList.Add(pOutputGeometry as IPolygon);
                        }
                        pFeature2 = pCursor2.NextFeature();
                    } //while (pFeature2 != null)

                    
                    pTinEdit.StartEditing();
                    for (int i = 0; i < MultiPatchList.Count; i++)
                    {
                        if (MultiPatchList[i] == null)
                        {
                            continue;
                        }
                        if (MultiPatchList[i].IsClosed == false)
                        {
                            continue;
                        }
                        if ((MultiPatchList[i].Length == 0))
                        {
                            continue;
                        }
                        pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)MultiPatchList[i], true, true, esriTinSelectionType.esriTinSelectionNew);
                        pTinEdit.DeleteSelectedNodes();
                        pTinEdit.AddShapeZ(MultiPatchList[i], esriTinSurfaceType.esriTinHardLine, 0);
                    }

                    List<IPoint> PointList = new List<IPoint>();
                    PointList.Clear();
                    IPointCollection pPointCollection = pMultiPatch as IPointCollection;
                    for (int i = 0; i < pPointCollection.PointCount; i++)
                    {
                        IPoint pt = pPointCollection.get_Point(i);
                        IZAware za = pt as IZAware;
                        za.ZAware = true;
                        IEnvelope pExtent = pTin.Extent;

                        double zval = pSurface.GetElevation(pt);
                        double rad = CalDistaceBetweenTwoPoints(pt, CenterPoint);
                        if (PointInExtent(pt, pExtent) && (rad - Radii < Radii * 0.1 || Math.Abs(pt.Z - EdgePoint.Z) < 0.1))
                        {
                            if (pt.Z < zval)
                            {
                                //  pTinEdit.AddPointZ(pt, 0);
                                PointList.Add(pt);
                            }
                        }
                    }
                    // pTinEdit.DeleteSelectedNodes();
                    for (int i = 0; i < PointList.Count; i++)
                    {
                        pTinEdit.AddPointZ(PointList[i], 0);
                    }
                }
                catch (System.Exception ex)
                {
                    pTinEdit.StopEditing(false);
                    pTinEdit.StartEditing();
                }
             
              
                try
                {
                    object vtMissing = true;
                    
                   // pTinEdit.SaveAs(textBoxXPath.Text, ref vtMissing);
                    pTinEdit.Save();
                    pTinEdit.StopEditing(false);
                }
                catch (SystemException ee)
                {
                    if (ee.Message == "The specified file or dataset already exists.")
                    {
                        pTinEdit.Save();
                        pTinEdit.StopEditing(true);
                    }
                    else
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
                pFeature = pCursor.NextFeature();
            }// while (pFeature != null)

        }

        public void MergeNonCraterToMemoryTin(ITinLayer pTinLayer, IFeatureLayer pFeatureLayer)
        {
            if (pTinLayer == null || pFeatureLayer == null)
            {
                return;
            }

            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.StartEditing();
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IFeatureCursor pCursor = pFeatureClass.Search(null, false);
            IFeature pFeature = pCursor.NextFeature();
            List<IPolygon> MultiPatchList = new List<IPolygon>();
            ITinSelection pTinSelection = pTin as ITinSelection;
            ITinSurface pSurface = pTin as ITinSurface;
            while (pFeature != null)
            {
                IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                IEnvelope pEnvelope = pProjGeometry.Envelope;
                //Polygon pPolygon = pProjGeometry as IPolygon;
                
                //计算中心点
                IPoint CenterPoint = new PointClass();
                CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                double CenterZValue = (pMultiPatch.Envelope.ZMax + pMultiPatch.Envelope.ZMin) / 2;

               
                IPolygon printPolygon = pProjGeometry as IPolygon;
                if (printPolygon != null)
                {
                    pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)pProjGeometry, true, true, esriTinSelectionType.esriTinSelectionNew);
                }
                
                ITin nonCraterTin = ConvertMultiPatchToTin2(pMultiPatch);
                IFeatureClass MemFeatureClass = CreateMemFeatureClass();
                ITinSurface3 pTinSurface3 = pTin as ITinSurface3;
                IGeometry intersectpolygon = new PolygonClass();
                pTinEdit.StopEditing(false);
                //  pTinSurface3.Intersect((ITinSurface)CraterTin, intersectpolygon, MemFeatureClass, "volume", "surfaceArea", "code");
                try
                { 
                    pTinSurface3.Intersect((ITinSurface)nonCraterTin, null, MemFeatureClass, "volume", "surfaceArea", "code");
                }
                catch (System.Exception ex)
                {
                    ;
                }                
                IFeatureCursor pCursor2 = MemFeatureClass.Search(null, false);
                IFeature pFeature2 = pCursor2.NextFeature();
                while (pFeature2 != null)
                {
                    int findex = pFeature2.Fields.FindFieldByAliasName("code");
                    string code = pFeature2.get_Value(findex).ToString();
                    if (code == "-1")
                    {
                        IGeometry pPolygon = pFeature2.Shape;
                        IGeometry pOutputGeometry = new MultiPatchClass();
                        pSurface.InterpolateShape(pPolygon, out pOutputGeometry);
                        MultiPatchList.Add(pOutputGeometry as IPolygon);
                    }
                    pFeature2 = pCursor2.NextFeature();
                } //while (pFeature2 != null)
                pFeature = pCursor.NextFeature();
            }// while (pFeature != null)

            pTinEdit.StartEditing();
            for (int i = 0; i < MultiPatchList.Count; i++)
            {
                if (MultiPatchList[i] == null)
                {
                    continue;
                }
                if (MultiPatchList[i].IsClosed == false)
                {
                    continue;
                }
                if ((MultiPatchList[i].Length == 0))
                {
                    continue;
                }
                try
                {
                    pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)MultiPatchList[i], true, true, esriTinSelectionType.esriTinSelectionNew);
                    pTinEdit.DeleteSelectedNodes();
                    pTinEdit.AddShapeZ(MultiPatchList[i], esriTinSurfaceType.esriTinHardLine, 0);
                }
                catch (System.Exception ex)
                {
                    pTinEdit.StopEditing(false);
                    pTinEdit.StartEditing();
                }
              
                //MultiPatchList[i].XYFootprint
            }
            pCursor = pFeatureClass.Search(null, false);
            pFeature = pCursor.NextFeature();

            while (pFeature != null)
            {
                try
                {
                    IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                    IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                    IEnvelope pEnvelope = pProjGeometry.Envelope;
                    List<IPoint> PointList = new List<IPoint>();
                    PointList.Clear();
                    IPointCollection pPointCollection = pMultiPatch as IPointCollection;
                    //计算中心点
                    IPoint CenterPoint = new PointClass();
                    CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                    double CenterZValue = (pMultiPatch.Envelope.ZMax + pMultiPatch.Envelope.ZMin) / 2;

                    for (int i = 0; i < pPointCollection.PointCount; i++)
                    {
                        IPoint pt = pPointCollection.get_Point(i);
                        IZAware za = pt as IZAware;
                        za.ZAware = true;
                        IEnvelope pExtent = pTin.Extent;

                        double zval = pSurface.GetElevation(pt);
                        if (PointInExtent(pt, pExtent) && pt.Z >= CenterZValue)
                        {
                            if (pt.Z >= zval)
                            {
                                //  pTinEdit.AddPointZ(pt, 0);
                                PointList.Add(pt);
                            }
                        }
                    }
                    for (int i = 0; i < PointList.Count; i++)
                    {
                        pTinEdit.AddPointZ(PointList[i], 0);
                    }
                }
                catch (System.Exception ex)
                {
                    ;
                }
              
                pFeature = pCursor.NextFeature();
            }
            try
            {
                object vtMissing = true;
              //  pTinEdit.SaveAs(textBoxXPath.Text, ref vtMissing);
                pTinEdit.Save();
                pTinEdit.StopEditing(false);
            }
            catch (SystemException ee)
            {
                if (ee.Message == "The specified file or dataset already exists.")
                {
                    pTinEdit.Save();
                }
                else
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        public void MergeCraterToMemoryTin(ITinLayer pTinLayer, IFeature pFeature)
        {
            if (pTinLayer == null || pFeature == null)
            {
                return;
            }
            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.StartEditing();
            //IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //IFeatureCursor pCursor = pFeatureClass.Search(null, false);
            //IFeature pFeature = pCursor.NextFeature();
            List<IPolygon> MultiPatchList = new List<IPolygon>();
            ITinSelection pTinSelection = pTin as ITinSelection;
            ITinSurface pSurface = pTin as ITinSurface;
            int modelnum = 0;
           // while (pFeature != null)
            {
                MultiPatchList.Clear();
                modelnum++;
                IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                IEnvelope pEnvelope = pProjGeometry.Envelope;
                //计算中心点
                IPoint CenterPoint = new PointClass();
                CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                //计算坑缘点
                IPoint EdgePoint = GetTheMaxZValueInMultiPatch(pMultiPatch);
                //计算碗状半径
                double Radii = CalDistaceBetweenTwoPoints(CenterPoint, EdgePoint);
                ITin CraterTin = null;
                try
                {
                    CraterTin = ConvertMultiPatchToTin(pMultiPatch);
                    ITinEdit pTinEdit2 = CraterTin as ITinEdit;
                    try
                    {

                        object vtMissing = true;
                        //pTinEdit = tin as ITinEdit;
                        pTinEdit2.SaveAs(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\DefaultFileGDB\TempTin2", ref vtMissing);
                        pTinEdit2.StopEditing(false);
                    }
                    catch (SystemException ee)
                    {
                        if (ee.Message == "The specified file or dataset already exists.")
                        {
                            pTinEdit2.Save();
                            pTinEdit2.StopEditing(true);
                        }
                        else
                        {
                            MessageBox.Show(ee.Message);
                        }
                    }

                    IFeatureClass MemFeatureClass = CreateMemFeatureClass();
                    ITinSurface3 pTinSurface3 = pTin as ITinSurface3;
                    IGeometry intersectpolygon = new PolygonClass();
                    pTinEdit.StopEditing(false);
                    pTinSurface3.Intersect((ITinSurface)CraterTin, null, MemFeatureClass, "volume", "surfaceArea", "code");
                    IFeatureCursor pCursor2 = MemFeatureClass.Search(null, false);
                    IFeature pFeature2 = pCursor2.NextFeature();
                    while (pFeature2 != null)
                    {
                        int findex = pFeature2.Fields.FindFieldByAliasName("code");
                        string code = pFeature2.get_Value(findex).ToString();
                        if (code == "1")
                        {
                            IGeometry pPolygon = pFeature2.Shape;
                            IGeometry pOutputGeometry = new MultiPatchClass();
                            pSurface.InterpolateShape(pPolygon, out pOutputGeometry);
                            //pTinEdit.StartEditing();
                            //pTinEdit.AddShapeZ(pOutputGeometry, esriTinSurfaceType.esriTinHardLine, 0);                        
                            //pTinEdit.StopEditing(true);
                            MultiPatchList.Add(pOutputGeometry as IPolygon);
                        }
                        pFeature2 = pCursor2.NextFeature();
                    } //while (pFeature2 != null)


                    pTinEdit.StartEditing();
                    for (int i = 0; i < MultiPatchList.Count; i++)
                    {
                        if (MultiPatchList[i] == null)
                        {
                            continue;
                        }
                        if (MultiPatchList[i].IsClosed == false)
                        {
                            continue;
                        }
                        if ((MultiPatchList[i].Length == 0))
                        {
                            continue;
                        }
                        pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)MultiPatchList[i], true, true, esriTinSelectionType.esriTinSelectionNew);
                        pTinEdit.DeleteSelectedNodes();
                        pTinEdit.AddShapeZ(MultiPatchList[i], esriTinSurfaceType.esriTinHardLine, 0);
                    }

                    List<IPoint> PointList = new List<IPoint>();
                    PointList.Clear();
                    IPointCollection pPointCollection = pMultiPatch as IPointCollection;
                    for (int i = 0; i < pPointCollection.PointCount; i++)
                    {
                        IPoint pt = pPointCollection.get_Point(i);
                        IZAware za = pt as IZAware;
                        za.ZAware = true;
                        IEnvelope pExtent = pTin.Extent;

                        double zval = pSurface.GetElevation(pt);
                        double rad = CalDistaceBetweenTwoPoints(pt, CenterPoint);
                        if (PointInExtent(pt, pExtent) && (rad - Radii < Radii * 0.1 || Math.Abs(pt.Z - EdgePoint.Z) < 0.1))
                        {
                            if (pt.Z < zval)
                            {
                                //  pTinEdit.AddPointZ(pt, 0);
                                PointList.Add(pt);
                            }
                        }
                    }
                    // pTinEdit.DeleteSelectedNodes();
                    for (int i = 0; i < PointList.Count; i++)
                    {
                        pTinEdit.AddPointZ(PointList[i], 0);
                    }
                }
                catch (System.Exception ex)
                {
                    //出这个异常表示求交操作出错了，不做求交操作
                   // IPoint nodepoint = new PointClass();
                    if (ex.Message == "TIN internal process error.")
                    {
                        ITinSurface pcratersurface = CraterTin as ITinSurface;
                        ITinAdvanced pedittinadvanced = pTinEdit as ITinAdvanced;
                        //tin节点编号从1开始
                        pTinEdit.StartEditing();
                        for (int i = 1; i <= pTin.DataNodeCount; i++ )
                        {
                            ITinNode ptinnode = pedittinadvanced.GetNode(i);
                            double Zv = pcratersurface.get_Z(ptinnode.X, ptinnode.Y);
                            if (Zv < ptinnode.Z)
                            {
                                pTinEdit.SetNodeZ(i, Zv);
                            }
                        }
                    }
                    pTinEdit.StopEditing(true);
                    pTinEdit.StartEditing();
                }
                try
                {
                    object vtMissing = true;

                    // pTinEdit.SaveAs(textBoxXPath.Text, ref vtMissing);
                    pTinEdit.Save();
                    pTinEdit.StopEditing(false);
                }
                catch (SystemException ee)
                {
                    if (ee.Message == "The specified file or dataset already exists.")
                    {
                        pTinEdit.Save();
                        pTinEdit.StopEditing(true);
                    }
                    else
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
               // pFeature = pCursor.NextFeature();
            }// while (pFeature != null)

        }

        public void MergeNonCraterToMemoryTin(ITinLayer pTinLayer, IFeature pFeature)
        {
            if (pTinLayer == null || pFeature == null)
            {
                return;
            }

            ITin pTin = pTinLayer.Dataset;
            ITinEdit pTinEdit = pTin as ITinEdit;
            pTinEdit.StartEditing();
            // IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //  IFeatureCursor pCursor = pFeatureClass.Search(null, false);
            //IFeature pFeature = pCursor.NextFeature();
            List<IPolygon> MultiPatchList = new List<IPolygon>();
            ITinSelection pTinSelection = pTin as ITinSelection;
            ITinSurface pSurface = pTin as ITinSurface;
            ITin nonCraterTin = null;
            try
            {
                //while (pFeature != null)
                {
                    IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                    IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                    IEnvelope pEnvelope = pProjGeometry.Envelope;
                    //Polygon pPolygon = pProjGeometry as IPolygon;

                    //计算中心点
                    IPoint CenterPoint = new PointClass();
                    CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                    double CenterZValue = (pMultiPatch.Envelope.ZMax + pMultiPatch.Envelope.ZMin) / 2;


                    IPolygon printPolygon = pProjGeometry as IPolygon;
                    if (printPolygon != null)
                    {
                        pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)pProjGeometry, true, true, esriTinSelectionType.esriTinSelectionNew);
                    }



                    nonCraterTin = ConvertMultiPatchToTin2(pMultiPatch);
                    // ((ITinEdit)nonCraterTin).StopEditing(false);
                    ITinEdit pTinEdit2 = nonCraterTin as ITinEdit;
                    try
                    {

                        object vtMissing = true;
                        //pTinEdit = tin as ITinEdit;
                        pTinEdit2.SaveAs(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\DefaultFileGDB\TempTin2", ref vtMissing);
                        pTinEdit2.StopEditing(false);
                    }
                    catch (SystemException ee)
                    {
                        if (ee.Message == "The specified file or dataset already exists.")
                        {
                            pTinEdit2.Save();
                            pTinEdit2.StopEditing(true);
                        }
                        else
                        {
                            MessageBox.Show(ee.Message);
                        }
                    }

                    IFeatureClass MemFeatureClass = CreateMemFeatureClass();
                    ITinSurface3 pTinSurface3 = pTin as ITinSurface3;
                    IGeometry intersectpolygon = new PolygonClass();
                    pTinEdit.StopEditing(false);
                    //  pTinSurface3.Intersect((ITinSurface)CraterTin, intersectpolygon, MemFeatureClass, "volume", "surfaceArea", "code");


                    pTinSurface3.Intersect((ITinSurface)nonCraterTin, null, MemFeatureClass, "volume", "surfaceArea", "code");

                    IFeatureCursor pCursor2 = MemFeatureClass.Search(null, false);
                    IFeature pFeature2 = pCursor2.NextFeature();
                    while (pFeature2 != null)
                    {
                        int findex = pFeature2.Fields.FindFieldByAliasName("code");
                        string code = pFeature2.get_Value(findex).ToString();
                        if (code == "-1")
                        {
                            IGeometry pPolygon = pFeature2.Shape;
                            IGeometry pOutputGeometry = new MultiPatchClass();
                            pSurface.InterpolateShape(pPolygon, out pOutputGeometry);
                            MultiPatchList.Add(pOutputGeometry as IPolygon);
                        }
                        pFeature2 = pCursor2.NextFeature();
                    } //while (pFeature2 != null)
                    //  pFeature = pCursor.NextFeature();
                }// while (pFeature != null)

                pTinEdit.StartEditing();
                for (int i = 0; i < MultiPatchList.Count; i++)
                {
                    if (MultiPatchList[i] == null)
                    {
                        continue;
                    }
                    if (MultiPatchList[i].IsClosed == false)
                    {
                        continue;
                    }
                    if ((MultiPatchList[i].Length == 0))
                    {
                        continue;
                    }
                    try
                    {
                        pTinSelection.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)MultiPatchList[i], true, true, esriTinSelectionType.esriTinSelectionNew);
                        pTinEdit.DeleteSelectedNodes();
                        pTinEdit.AddShapeZ(MultiPatchList[i], esriTinSurfaceType.esriTinHardLine, 0);
                    }
                    catch (System.Exception ex)
                    {
                        pTinEdit.StopEditing(false);
                        pTinEdit.StartEditing();
                    }

                    //MultiPatchList[i].XYFootprint
                }
                // pCursor = pFeatureClass.Search(null, false);
                // pFeature = pCursor.NextFeature();

                // while (pFeature != null)
                {
                    //try
                    //{
                    IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                    IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                    IEnvelope pEnvelope = pProjGeometry.Envelope;
                    List<IPoint> PointList = new List<IPoint>();
                    PointList.Clear();
                    IPointCollection pPointCollection = pMultiPatch as IPointCollection;
                    //计算中心点
                    IPoint CenterPoint = new PointClass();
                    CenterPoint.PutCoords((pEnvelope.XMax + pEnvelope.XMin) / 2, (pEnvelope.YMax + pEnvelope.YMin) / 2);
                    double CenterZValue = (pMultiPatch.Envelope.ZMax + pMultiPatch.Envelope.ZMin) / 2;

                    for (int i = 0; i < pPointCollection.PointCount; i++)
                    {
                        IPoint pt = pPointCollection.get_Point(i);
                        IZAware za = pt as IZAware;
                        za.ZAware = true;
                        IEnvelope pExtent = pTin.Extent;

                        double zval = pSurface.GetElevation(pt);
                        if (PointInExtent(pt, pExtent) && pt.Z >= CenterZValue)
                        {
                            if (pt.Z >= zval)
                            {
                                //  pTinEdit.AddPointZ(pt, 0);
                                PointList.Add(pt);
                            }
                        }
                    }
                    for (int i = 0; i < PointList.Count; i++)
                    {
                        pTinEdit.AddPointZ(PointList[i], 0);
                    }
                    //}
                    //catch (System.Exception ex)
                    //{
                    //    ;
                    //}

                    // pFeature = pCursor.NextFeature();
                }
            }
            catch (System.Exception ex)
            {
                //当两个表面求交出错的时候会报这个异常
                if (ex.Message == "TIN internal process error.")
                {
                    ITinSurface pcratersurface = nonCraterTin as ITinSurface;
                    ITinAdvanced pedittinadvanced = pTinEdit as ITinAdvanced;
                    //tin节点编号从1开始
                    pTinEdit.StartEditing();
                    for (int i = 1; i <= pTin.DataNodeCount; i++)
                    {
                        ITinNode ptinnode = pedittinadvanced.GetNode(i);
                        double Zv = pcratersurface.get_Z(ptinnode.X, ptinnode.Y);
                        if (Zv > ptinnode.Z)
                        {
                            pTinEdit.SetNodeZ(i, Zv);
                        }
                    }
                }
                pTinEdit.StopEditing(true);
                pTinEdit.StartEditing();
            }
            try
            {
                object vtMissing = true;
                //  pTinEdit.SaveAs(textBoxXPath.Text, ref vtMissing);
                pTinEdit.Save();
                pTinEdit.StopEditing(false);
            }
            catch (SystemException ee)
            {
                if (ee.Message == "The specified file or dataset already exists.")
                {
                    pTinEdit.Save();
                }
                else
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }
    
    }
}
