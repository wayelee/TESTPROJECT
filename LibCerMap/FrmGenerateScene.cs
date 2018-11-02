using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmGenerateScene : OfficeForm
    {
        public FrmGenerateScene()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public IMapControl2 m_pMapCtrl;
        public ISceneControl m_pSceneCtrl;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //高程底面
            ILayer pBaseLayer = null;
            ISurface pSurface = null;
            if (m_pSceneCtrl != null)
            {
                try
                {
                    // m_pSceneCtrl.LoadSxFile(ClsGDBDataCommon.GetParentPathofExe() +@"Resource\DefaultData\Default.sxd");
                }
                catch
                {
                }
                //IScene pScene = new SceneClass();
                //pScene.Name = "Scene";
                //m_pSceneCtrl.Scene = pScene;
                
                IScene pScene = m_pSceneCtrl.Scene;
                pScene.Name = "Scene";
                //ILayer pglayer = new GraphicsLayer3DClass();
                //pScene.AddLayer(pglayer);
                //pScene.ActiveGraphicsLayer = new CompositeGraphicsLayerClass();
               
                pScene.ExaggerationFactor = Convert.ToDouble(doubleInputExaFactor.Value.ToString());
                while (pScene.LayerCount > 0)
                {
                    pScene.DeleteLayer(pScene.get_Layer(0));
                }
               // pScene.ClearLayers(); 
                pScene.AddLayer(pScene.ActiveGraphicsLayer);
                GC.Collect();
                for (int i = 0; i < m_pMapCtrl.Map.LayerCount; i++)
                {
                    ILayer pMapLayer = m_pMapCtrl.Map.get_Layer(i);
                    ILayer pLayer = pMapLayer;
                   
                    if (comboBoxExBaseHeightLayer.Items.Count > 0)
                    {
                        if (pLayer.Name == comboBoxExBaseHeightLayer.SelectedItem.ToString())
                        {
                            pBaseLayer = pLayer;
                        }
                    }                 
                }

                if (pBaseLayer is IRasterLayer)
                {
                    IRaster pRaster = ((IRasterLayer)pBaseLayer).Raster;
                    IRasterBand pBand = ((IRasterBandCollection)pRaster).Item(0);
                    IRasterSurface rsurface = new RasterSurface();
                    rsurface.RasterBand = pBand;
                    //将dem的数据指定为surface
                    pSurface = rsurface as ISurface;
                }
                if (pBaseLayer is ITinLayer)
                {
                    ITin pTin = ((ITinLayer)pBaseLayer).Dataset;
                    ITinSurface pTinSurface = pTin as ITinSurface;
                    pSurface = pTinSurface as ISurface;
                }


                for (int i = 0; i < m_pMapCtrl.Map.LayerCount; i++)
                {
                    ILayer pMapLayer = m_pMapCtrl.Map.get_Layer(i);
                    
                    ILayer pLayer = pMapLayer;
                    string filepath = "";
                    string filename = "";
                    string sourcepath = GetDataLayerPath(pMapLayer as IDataLayer,ref filepath,ref filename);

                    //if (pMapLayer is IRasterLayer)
                    //{
                    //    IRasterLayer prl = new RasterLayerClass();
                    //     prl.CreateFromRaster(((IRasterLayer)pMapLayer).Raster);
                    //     pLayer = prl as ILayer;
                    //}
                    if (pMapLayer is IFeatureLayer )
                    {
                       // if (((IFeatureLayer)pMapLayer).FeatureClass.ShapeType != ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryMultiPatch)
                      //  {

                            IFeatureLayer pfl = new FeatureLayerClass();
                            IFeatureClass pFc = ((IFeatureLayer)pMapLayer).FeatureClass;
                            if (pFc != null)
                            {

                                pfl.FeatureClass = ((IFeatureLayer)pMapLayer).FeatureClass;
                                pLayer = pfl as ILayer;
                                pLayer.Name = pMapLayer.Name;

                                /////////////////////////////////////////////////////////////////////
                                //后期添加，用于带进涂层渲染
                                /////////////////////////////////////////////////////////////////////
                                IGeoFeatureLayer pGeoFLayer = pfl as IGeoFeatureLayer;
                                IGeoFeatureLayer pGFMapLayer=pMapLayer as IGeoFeatureLayer;
                                if (pGFMapLayer!=null)//注记图层为空
                                {
                                    pGeoFLayer.Renderer = pGFMapLayer.Renderer;
                                    ILegendInfo pLegend3D = (ILegendInfo)pGeoFLayer ;
                                    for (int k = 0; k < pLegend3D.LegendGroupCount;k++ )
                                    {
                                        ILegendGroup pLgroup = pLegend3D.LegendGroup[k];
                                        for (int p = 0; p < pLgroup.ClassCount;p++ )
                                        {
                                            ILegendClass pLClass = pLgroup.Class[p];
                                            if (pLClass.Symbol is IMarkerSymbol)
                                            {
                                                IMarkerSymbol pSMSymbol = pLClass.Symbol as IMarkerSymbol;
                                                pSMSymbol.Size /= 20;
                                                
                                            }
                                        }
                                    }
                                    if (pFc.ShapeType == esriGeometryType.esriGeometryMultipoint||pFc.ShapeType == esriGeometryType.esriGeometryPoint)
                                    {
                                        if (pGFMapLayer.Renderer is IUniqueValueRenderer)
                                        {
                                        }
                                    }
                                    
                                }                               
                            }
                      //  }
                    }

                    #region 这段代码家的有点重复，如果不加有时候会弹出设置数据源的对话框
                    if (pLayer is IRasterLayer)
                    {
                        I3DProperties properties = null;

                        properties = new Raster3DPropertiesClass();
                        ILayerExtensions layerextensions = pLayer as ILayerExtensions;

                        object p3d;
                        for (int j = 0; j < layerextensions.ExtensionCount; j++)
                        {
                            p3d = layerextensions.get_Extension(j);
                            if (p3d != null)
                            {
                                properties = p3d as I3DProperties;
                                if (properties != null)
                                    break;
                            }
                        }
                        properties.BaseOption = esriBaseOption.esriBaseSurface;
                        properties.BaseSurface = pSurface;
                        //I、更改分辨率后，如何让分辨率设置发挥作用。主要是刷新的问题（使用IActiveView刷新、而不能使用Iscene及IScenegraph刷新）
                        //II、分辨率数值在（ 262144-268435456）范围之内，越大越清晰。使用3DProperties.MaxTextureMemory 方法
                        properties.MaxTextureMemory = 268435456;
                    }
                    #endregion
                    pScene.AddLayer(pLayer);
                    //if (((CheckBoxItem)itemPanelSetVisible.Items[i]).Checked == false)
                    //{
                    //    pLayer.Visible = false;
                    //}
                    //else
                    //{
                    //    pLayer.Visible = true;
                    //}
                    if (comboBoxExBaseHeightLayer.Items.Count > 0)
                    {
                        if (pLayer.Name == comboBoxExBaseHeightLayer.SelectedItem.ToString())
                        {
                            pBaseLayer = pLayer;
                        }
                    }
                }
            }
           
            if (pSurface != null)
            {
                for (int i = 0; i < m_pSceneCtrl.Scene.LayerCount; i++)
                {
                    ILayer pLayer = m_pSceneCtrl.Scene.get_Layer(i);

                    ILayerExtensions layerextensions = m_pSceneCtrl.Scene.get_Layer(i) as ILayerExtensions;
                    I3DProperties properties = null;
                    if (pLayer is IRasterLayer)
                    {
                        properties = new Raster3DPropertiesClass();
                    }
                    if (pLayer is IFeatureLayer)
                    {
                        properties = new Feature3DPropertiesClass();
                    }
                    if (pLayer is ITinLayer)
                    {
                        properties = new Tin3DPropertiesClass();
                    }
                    object p3d;
                    for (int j = 0; j < layerextensions.ExtensionCount; j++)
                    {
                        p3d = layerextensions.get_Extension(j);
                        if (p3d != null)
                        {
                            properties =  p3d as I3DProperties;
                            if(properties != null)
                            break;
                        }
                    }
                    if (!(pLayer is IFeatureLayer))
                    {
                        properties.BaseOption = esriBaseOption.esriBaseSurface;
                        properties.BaseSurface = pSurface;
                        //I、更改分辨率后，如何让分辨率设置发挥作用。主要是刷新的问题（使用IActiveView刷新、而不能使用Iscene及IScenegraph刷新）
                        //II、分辨率数值在（ 262144-268435456）范围之内，越大越清晰。使用3DProperties.MaxTextureMemory 方法
                        properties.MaxTextureMemory = 268435456;
                    }
                    else
                    {
                        IFeatureLayer pFlayer = pLayer as IFeatureLayer;
                        //multipatch的本身有高度信息
                        if (pFlayer.FeatureClass !=null && pFlayer.FeatureClass.ShapeType != ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryMultiPatch)
                        {
                            bool needConstantHeight = false;
                            /*
                            try
                            {
                                //把导航点转向角，视场角等信息都设置成不贴在表面而需要根据生产该信息的文件设置高度
                                IFeatureClass fc = pFlayer.FeatureClass;
                                IFeatureCursor pFcursor = fc.Search(null, false);
                                int OriginalFeatureidx = fc.FindField("OriginalFeatureClass");
                                int OriginalFeatureOIDidx = fc.FindField("OID");
                                IFeature pF = pFcursor.NextFeature();
                                IFeatureLayer pOFeatureLayer = null;
                                if (OriginalFeatureidx == -1|| OriginalFeatureOIDidx == -1)
                                {
                                    pF = null;
                                }
                                else
                                {
                                    if (pF != null)
                                    {
                                        //获取路径点图层名称
                                        string orignalname = pF.get_Value(OriginalFeatureidx).ToString();
                                        IMap pMap = m_pMapCtrl.Map;
                                        for (int k = 0; k < pMap.LayerCount; k++)
                                        {
                                            if (pMap.get_Layer(k).Name.Equals(orignalname))
                                            {
                                                pOFeatureLayer = pMap.get_Layer(k) as IFeatureLayer;
                                                break;
                                            }
                                        }
                                        if (pOFeatureLayer == null)
                                        {
                                            pF = null;
                                        }
                                    }
                                }
                                
                                while (pF != null)
                                {
                                    //IFeatureClass pFClass = pOFeatureLayer.FeatureClass;
                                    //pFClass.FindField(OriginalFeatureOIDidx);
                                    

                                    pF = pFcursor.NextFeature();
                                }
                            }
                            catch
                            {
                                ;
                            }
                            */
                            if (needConstantHeight == false)
                            {
                                properties.BaseOption = esriBaseOption.esriBaseSurface;
                                //I3DProperties2 pr = properties as I3DProperties2;
                                //if (pr != null)
                                //{
                                //    pr.OffsetExpressionString = "0.005";
                                //}
                                //  properties.BaseOption = esriBaseOption.esriBaseExpression;
                                properties.BaseSurface = pSurface;
                                //I、更改分辨率后，如何让分辨率设置发挥作用。主要是刷新的问题（使用IActiveView刷新、而不能使用Iscene及IScenegraph刷新）
                                //II、分辨率数值在（ 262144-268435456）范围之内，越大越清晰。使用3DProperties.MaxTextureMemory 方法
                                properties.MaxTextureMemory = 268435456;
                            }
                        }
                    }
                    //  properties.Apply3DProperties(domlayer);
                    // ps.SceneGraph.RefreshViewers();

                      IActiveView iv = m_pSceneCtrl.Scene as IActiveView;
                     iv.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                }
            }
        }

        private void FrmGenerateScene_Load(object sender, EventArgs e)
        {
           // itemPanelSetVisible.Items.Clear();
            doubleInputExaFactor.Value = Convert.ToDecimal( m_pSceneCtrl.Scene.ExaggerationFactor.ToString());
            comboBoxExBaseHeightLayer.Items.Clear();
            if (m_pMapCtrl != null)
            {
                for (int i = 0; i < m_pMapCtrl.Map.LayerCount; i++)
                {
                    DevComponents.DotNetBar.CheckBoxItem CBItem =  new DevComponents.DotNetBar.CheckBoxItem();
                    CBItem.Checked = true;
                    ILayer pLayer = m_pMapCtrl.Map.get_Layer(i);
                    CBItem.Text = pLayer.Name;
                 //   itemPanelSetVisible.Items.Add(CBItem);
                    if (pLayer is IRasterLayer )
                    {
                        IRasterBandCollection pRasterCollection = ((IRasterLayer)pLayer).Raster as IRasterBandCollection;
                        if (pRasterCollection.Count == 1)
                        {
                            comboBoxExBaseHeightLayer.Items.Add(pLayer.Name);
                        }
                    }
                    if(pLayer is ITinLayer)
                    { 
                        comboBoxExBaseHeightLayer.Items.Add(pLayer.Name);
                    }
                }
            }
            if (comboBoxExBaseHeightLayer.Items.Count > 0)
            {
                comboBoxExBaseHeightLayer.SelectedIndex = 0;
            }
        }


        private string GetDataLayerPath(IDataLayer pDataLayer, ref string pFilePath ,ref string pFileName)
        {
            try
            {
                IDatasetName pDatasetName;
                IWorkspaceName pWSName;
                pDatasetName = pDataLayer.DataSourceName as IDatasetName;
                pWSName = pDatasetName.WorkspaceName;
                pFilePath = pWSName.PathName;
                pFileName = pDatasetName.Name;
                return pFilePath + "\\" + pFileName;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("获取高程图层路径失败");
                return null;
            }
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }    
    }
}
