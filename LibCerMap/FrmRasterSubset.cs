using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Controls;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmRasterSubset : OfficeForm
    {

         private IMapControl3 m_mapControl = null;
 
        public FrmRasterSubset(IMapControl3 pMapControl)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_mapControl = pMapControl;
        }
 
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbSource.Text) || string.IsNullOrEmpty(cmbRegion.Text) || string.IsNullOrEmpty(txtTarget.Text)) return;
            try
            {

                //得到图层
                
                ILayer pSourceLayer = ClsGDBDataCommon.GetLayerFromName(m_mapControl.Map, cmbSource.Text);
                ILayer pRegionLayer = ClsGDBDataCommon.GetLayerFromName(m_mapControl.Map, cmbRegion.Text);
                if (pSourceLayer == null || pRegionLayer == null) return;
                if (!(pSourceLayer is IRasterLayer)) return;

                IRaster raster = ((IRasterLayer)pSourceLayer).Raster;
                IRaster2 raster2 = raster as IRaster2;
                IRasterDataset rasterDatasetSource = raster2.RasterDataset;

                //得到裁切范围
                IGeometry pGeometryRegion = null;
                if (pRegionLayer is IRasterLayer)
                {
                    IRaster rasterRegion = ((IRasterLayer)pRegionLayer).Raster;
                    IGeoDataset geoDatasetRegion = rasterRegion as IGeoDataset;
                    IEnvelope pEnvelope = geoDatasetRegion.Extent;
                    pGeometryRegion = pEnvelope as IGeometry;
                }
                else if (pRegionLayer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = pRegionLayer as IFeatureLayer;
                    if (featureLayer.FeatureClass != null)
                    {
                        if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            IFeatureCursor pFeatureCursor = featureLayer.FeatureClass.Search(null, false);
                            IFeature pFeature = pFeatureCursor.NextFeature();
                            //只裁切出第一个多边形
                            if (pFeature != null)
                            {
                                pGeometryRegion = pFeature.Shape;
                            }
                        }
                    }
                }
                //裁切栅格
                ClsGDBDataCommon cls = new ClsGDBDataCommon();
                cls.RasterSubsetByPolygon(rasterDatasetSource, pGeometryRegion, txtTarget.Text);
                //加到地图中

                IRasterLayer rasterLayerNew = new RasterLayerClass();
                rasterLayerNew.CreateFromFilePath(txtTarget.Text);
                m_mapControl.AddLayer(rasterLayerNew);
                m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
             catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
#region 原代码

            //if (cmbSource.SelectedItem != null)
            //{
            //    string itemName = cmbSource.SelectedItem.ToString();
            //    for (int i=0;i<m_mapControl.Map.LayerCount;i++)
            //    {
            //        ILayer pLayer = m_mapControl.Map.get_Layer(i);
            //        if (pLayer.Name==itemName)
            //        {                      
            //            if(pLayer is IRasterLayer)
            //            {
            //                IDataLayer pDatalayer = pLayer as IDataLayer;
            //                IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;

            //                string layername = pDname.WorkspaceName.PathName + "\\" + pDname.Name;
            //                if (!layername.Contains("\\\\"))
            //                {
            //                    m_RasterPath=pDname.WorkspaceName.PathName + "\\" + pDname.Name;
            //                }
            //                else
            //                {
            //                    layername = pDname.WorkspaceName.PathName + pDname.Name;
            //                    m_RasterPath=layername;
            //                }
            //                break;
            //            }                      
            //        }
            //    }
            //}

            //else
            //{
            //    MessageBox.Show("请选择原始栅格数据", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (m_ResultPath == null)
            //{
            //    MessageBox.Show("请选择输出路径及文件名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (cmbRegion.SelectedItem != null)
            //{
            //    //m_ShpPath = comboBoxEx1.SelectedItem.ToString();
            //    string itemName = cmbRegion.SelectedItem.ToString();
            //    for (int i = 0; i < m_mapControl.Map.LayerCount; i++)
            //    {
            //        ILayer pLayer = m_mapControl.Map.get_Layer(i);
            //        if (pLayer.Name == itemName)
            //        {
            //           if(pLayer is IFeatureLayer)
            //           {
            //               IDataLayer pDatalayer = pLayer as IDataLayer;
            //               IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;
            //               m_ShpPath=pDname.WorkspaceName.PathName + "\\" + pDname.Name + ".shp";
            //                break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请选择矢量文件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //RasterLayerClass rasterlayer = new RasterLayerClass();
            //rasterlayer.CreateFromFilePath(m_RasterPath);
            //string str = m_ShpPath;
            //string str2 = str.Substring(str.LastIndexOf(@"\") + 1);     //文件名
            //string str3 = str.Substring(0, (str.Length - str2.Length) - 1);  //路径
            //IWorkspaceFactory PWorkSpaceFactory = new ShapefileWorkspaceFactory();
            //IFeatureWorkspace pFeatureWorkSpace = (IFeatureWorkspace)PWorkSpaceFactory.OpenFromFile(str3, 0) ;
            //IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            //pFeatureLayer.FeatureClass = pFeatureWorkSpace.OpenFeatureClass(str2);
            //esriGeometryType pType = pFeatureLayer.FeatureClass.ShapeType;
            //if (pType != esriGeometryType.esriGeometryPolygon)
            //{
            //    MessageBox.Show("矢量文件必须是多边形图层！请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //IFeatureCursor pFeatureCursor = pFeatureLayer.FeatureClass.Search(null, false);
            //IFeature pFeature = pFeatureCursor.NextFeature();
            //IGeometry pGeometry = pFeature.Shape;
            //IPolygon pPolygon = pGeometry as IPolygon;
            //bool result = RasterClip(rasterlayer, pPolygon, m_ResultPath);
            //if (result == true)
            //{
            //    RasterLayerClass prasterlayer = new RasterLayerClass();
            //    rasterlayer.CreateFromFilePath(m_ResultPath + ".tif");
            //    m_mapControl.AddLayer(rasterlayer as ILayer);
            //    m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //}
            //else
            //{
            //    return;
            //}

#endregion
        }

        private void FrmRasterSubset_Load(object sender, EventArgs e)
        {
            if (m_mapControl.Map != null)
            {
                 //初始化图层列表
                IEnumLayer pEnumLayer = m_mapControl.Map.get_Layers(null, true);
                pEnumLayer.Reset();
                ILayer pLayer = null;
                while ((pLayer = pEnumLayer.Next()) != null)
                {
                    if (pLayer is IRasterLayer)
                    {
                        cmbSource.Items.Add(pLayer.Name);
                        cmbRegion.Items.Add(pLayer.Name);
                    }
                    else if (pLayer is IFeatureLayer)
                    {
                         IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                        if (pFeatureLayer.FeatureClass !=null)
                        {
                            if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                            {
                                cmbRegion.Items.Add(pLayer.Name);
                            }
                        }                  
                    }
                    
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.tif;*.tiff;|*.tif;*.tiff;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (cmbSource.Items.Contains(dlg.FileName) == false)
                {
                    cmbSource.Items.Add(dlg.FileName);
                    cmbSource.SelectedItem = dlg.FileName;
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.shp;|*.shp;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (cmbRegion.Items.Contains(dlg.FileName) == false)
                {
                    cmbRegion.Items.Add(dlg.FileName);
                    cmbRegion.SelectedItem = dlg.FileName;
                }
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Title = "保存栅格文件";
            fdlg.Filter = "tif文件(*.tif)|*.tif|img文件(*.img)|*.img||";
            if (fdlg.ShowDialog() == DialogResult.OK && fdlg.FileName != "")
            {
                txtTarget.Text = fdlg.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool RasterClip(IRasterLayer pRasterLayer, IPolygon clipGeo, string FileName)
        {
            try
            {
                IRaster pRaster = pRasterLayer.Raster;
                IRasterProps pProps = pRaster as IRasterProps;
                object cellSizeProvider = pProps.MeanCellSize().X;
                IGeoDataset pInputDataset = pRaster as IGeoDataset;
                IExtractionOp pExtractionOp = new RasterExtractionOpClass();
                IRasterAnalysisEnvironment pRasterAnaEnvir = pExtractionOp as IRasterAnalysisEnvironment;
                pRasterAnaEnvir.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cellSizeProvider);
                object extentProvider = clipGeo.Envelope;
                object snapRasterData = Type.Missing;
                pRasterAnaEnvir.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProvider, ref snapRasterData);
                IGeoDataset pOutputDataset = pExtractionOp.Polygon(pInputDataset, clipGeo as IPolygon, true);//裁切操作
                IRaster clipRaster; //裁切后得到的IRaster
                if (pOutputDataset is IRasterLayer)
                {
                    IRasterLayer rasterLayer = pOutputDataset as IRasterLayer;
                    clipRaster = rasterLayer.Raster;
                }
                else if (pOutputDataset is IRasterDataset)
                {
                    IRasterDataset rasterDataset = pOutputDataset as IRasterDataset;
                    clipRaster = rasterDataset.CreateDefaultRaster();
                }
                else if (pOutputDataset is IRaster)
                {
                    clipRaster = pOutputDataset as IRaster;
                }
                else
                {
                    return false;
                }
                //保存裁切后得到的clipRaster
                //如果直接保存为img影像文件
                IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(FileName), 0);
                ISaveAs pSaveAs = clipRaster as ISaveAs;
                IDataset pDataset = pSaveAs.SaveAs(System.IO.Path.GetFileName(FileName)+".tif", pWorkspace, "TIFF");//以TIF格式保存
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
                return true;
                //MessageBox.Show("成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void cmbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRasterLayer pRasterLayer = ClsGDBDataCommon.GetLayerFromName(m_mapControl.Map, cmbSource.Text) as IRasterLayer;
            if (pRasterLayer == null) return;

            IDataLayer pDatalayer = pRasterLayer as IDataLayer;
            IDatasetName pDname = pDatalayer.DataSourceName as IDatasetName;

            txtTarget.Text = pDname.WorkspaceName.PathName + System.IO.Path.GetFileNameWithoutExtension(pDname.Name) + "_Sub.tif"; ;
        }

        
    }
}
