using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.DataSourcesFile;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.esriSystem;

namespace LibCerMap
{
    public partial class FrmSiftMatching : OfficeForm
    {
        public SiftMatchPara m_pSiftMatchPara = new SiftMatchPara();//SIFT匹配参数
        public RasterPara m_pRasterParaLeft = new RasterPara();//左栅格参数
        public RasterPara m_pRasterParaRight = new RasterPara();//右影像参数

        private IMapControl3 m_pMapCtrl = null;
        private IRasterLayer m_pRasterLayerLeft = null;
        private IRasterLayer m_pRasterLayerRight = null;

        public IRaster2 m_pRasterLeft = null;
        public IRaster2 m_pRasterRight = null;
        public FrmSiftMatching(IMapControl3 pMapCtrl, IRasterLayer pRasterLayer)
        {
            InitializeComponent();

            m_pMapCtrl = pMapCtrl;
            m_pRasterLayerLeft = pRasterLayer;
        }

        private void FrmSiftMatching_Load(object sender, EventArgs e)
        {
            if (m_pMapCtrl == null || m_pRasterLayerLeft == null) return;

            //初始化图层列表
            IEnumLayer pEnumLayer = m_pMapCtrl.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = null;
            while ((pLayer = pEnumLayer.Next()) != null)
            {
                if (pLayer is IRasterLayer)
                {
                    //基准图层不能等于待配准图层
                    if ((IRasterLayer)pLayer != m_pRasterLayerLeft)
                    {
                        txtRightImage.Items.Add(pLayer.Name);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region 废弃代码

        //private bool RasterClip(IRasterLayer pRasterLayer, IPolygon clipGeo, string FileName)
        //{
        //    try
        //    {
        //        IRaster pRaster = pRasterLayer.Raster;
        //        IRasterProps pProps = pRaster as IRasterProps;
        //        object cellSizeProvider = pProps.MeanCellSize().X;
        //        IGeoDataset pInputDataset = pRaster as IGeoDataset;
        //        IExtractionOp pExtractionOp = new RasterExtractionOpClass();
        //        IRasterAnalysisEnvironment pRasterAnaEnvir = pExtractionOp as IRasterAnalysisEnvironment;
        //        pRasterAnaEnvir.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cellSizeProvider);
        //        object extentProvider = clipGeo.Envelope;
        //        object snapRasterData = Type.Missing;
        //        pRasterAnaEnvir.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProvider, ref snapRasterData);
        //        IGeoDataset pOutputDataset = pExtractionOp.Polygon(pInputDataset, clipGeo as IPolygon, true);//裁切操作
        //        IRaster clipRaster; //裁切后得到的IRaster
        //        if (pOutputDataset is IRasterLayer)
        //        {
        //            IRasterLayer rasterLayer = pOutputDataset as IRasterLayer;
        //            clipRaster = rasterLayer.Raster;
        //        }
        //        else if (pOutputDataset is IRasterDataset)
        //        {
        //            IRasterDataset rasterDataset = pOutputDataset as IRasterDataset;
        //            clipRaster = rasterDataset.CreateDefaultRaster();
        //        }
        //        else if (pOutputDataset is IRaster)
        //        {
        //            clipRaster = pOutputDataset as IRaster;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //        //保存裁切后得到的clipRaster
        //        //如果直接保存为img影像文件
        //        IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
        //        IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(FileName), 0);
        //        ISaveAs pSaveAs = clipRaster as ISaveAs;
        //        IDataset pDataset = pSaveAs.SaveAs(System.IO.Path.GetFileName(FileName) + ".tif", pWorkspace, "TIFF");//以TIF格式保存
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
        //        return true;
        //        //MessageBox.Show("成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception exp)
        //    {
        //        MessageBox.Show(exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return false;
        //    }
        //}

        //从RASTER中根据SHP截取子图
        //private bool GetRasterSubset(IRasterLayer pRasterLayer, ILayer pLayerRegion, string szOutputRasterFilename,out IPoint ptLeftTopOffset)
        //{
        //    ptLeftTopOffset = null;
        //    if (pLayerRegion ==null)//裁切区域为空时，使用整个栅格
        //    {

        //    }
        //    else
        //    {
        //        //得到范围
        //    }


        //    return true;
        //}

        //private bool GetRasterSubset(IRasterLayer pRasterLayer, string szShpFilename, string szOutputRasterFilename, out IPoint ptLeftTopOffset)
        //{
        //    ptLeftTopOffset = null;
        //    if (string.IsNullOrEmpty(szOutputRasterFilename) || pRasterLayer==null || string.IsNullOrEmpty(szShpFilename))
        //        return false;

        //    try
        //    {
        //        //打开源文件
        //        IRasterLayer rasterlayer = pRasterLayer;

        //        //获得SHP文件
        //        string szShpPath = System.IO.Path.GetDirectoryName(szShpFilename);
        //        string szShpFile = System.IO.Path.GetFileName(szShpFilename);
        //        IWorkspaceFactory PWorkSpaceFactory = new ShapefileWorkspaceFactory();
        //        IFeatureWorkspace pFeatureWorkSpace = (IFeatureWorkspace)PWorkSpaceFactory.OpenFromFile(szShpPath, 0);
        //        IFeatureLayer pFeatureLayer = new FeatureLayerClass();
        //        pFeatureLayer.FeatureClass = pFeatureWorkSpace.OpenFeatureClass(szShpFile);
        //        esriGeometryType pType = pFeatureLayer.FeatureClass.ShapeType;
        //        if (pType != esriGeometryType.esriGeometryPolygon)
        //        {
        //            MessageBox.Show("矢量文件必须是多边形图层！请重新输入！");
        //            return false;
        //        }

        //        //截取子图
        //        IFeatureCursor pFeatureCursor = pFeatureLayer.FeatureClass.Search(null, false);
        //        IFeature pFeature = pFeatureCursor.NextFeature();
        //        IGeometry pGeometry = pFeature.Shape;
        //        IPolygon pPolygon = pGeometry as IPolygon;

        //        IRasterProps pRasterProps = rasterlayer.Raster as IRasterProps;
        //        IPoint pRasterLeftTop = pRasterProps.Extent.UpperLeft;
        //        IPoint pShpLeftTop = pPolygon.Envelope.UpperLeft;

        //        ptLeftTopOffset = new PointClass();
        //        ptLeftTopOffset.X = Math.Abs(pShpLeftTop.X - pRasterLeftTop.X);
        //        ptLeftTopOffset.Y = Math.Abs(pRasterLeftTop.Y - pShpLeftTop.Y);

        //        //保存到文件
        //        bool result = RasterClip(rasterlayer, pPolygon, szOutputRasterFilename);
        //        return result;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}
        #endregion
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRightImage.Text))
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            m_pRasterLayerRight = ClsGDBDataCommon.GetLayerFromName(m_pMapCtrl.Map,txtRightImage.Text) as IRasterLayer;
            //待配准影像完整名
            string strRegisterFile = "";
            //基准图层完整名
            string strBaseFile = "";
            if (m_pRasterLayerRight == null || m_pRasterLayerLeft == null)
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            if (m_pRasterLayerRight.Raster == null ||m_pRasterLayerLeft.Raster == null)
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            IDataLayer pDatalayer = m_pRasterLayerLeft as IDataLayer;
            IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;
            strRegisterFile = pDname.WorkspaceName.PathName + pDname.Name;

            pDatalayer = m_pRasterLayerRight as IDataLayer;
            pDname = (IDatasetName)pDatalayer.DataSourceName;
            strBaseFile = pDname.WorkspaceName.PathName + pDname.Name;

            //得到Raster2
            m_pRasterLeft = m_pRasterLayerLeft.Raster as IRaster2;
            m_pRasterRight = m_pRasterLayerRight.Raster as IRaster2;

            string szLeftGrayImage = System.IO.Path.GetTempFileName();
            string szRightGrayImage = System.IO.Path.GetTempFileName();
            try
            {
                IPoint ptLeftTopOffset = new PointClass();
                ptLeftTopOffset.X = 0;
                ptLeftTopOffset.Y = 0;

                //配置配准参数
                szLeftGrayImage = System.IO.Path.GetDirectoryName(szLeftGrayImage) + "\\" + System.IO.Path.GetFileNameWithoutExtension(szLeftGrayImage) + ".tif";
                szRightGrayImage = System.IO.Path.GetDirectoryName(szRightGrayImage) + "\\" + System.IO.Path.GetFileNameWithoutExtension(szRightGrayImage) + ".tif";
                if(!stretchToGrayImage(m_pRasterLeft as IRaster, szLeftGrayImage)) return;  //拉伸左影像
                if(!stretchToGrayImage(m_pRasterRight as IRaster, szRightGrayImage)) return; //拉伸右影像

                m_pSiftMatchPara.szLeftFilename = szLeftGrayImage;// strRegisterFile;
                m_pSiftMatchPara.szRightFilename = szRightGrayImage;// strBaseFile;
                //m_pSiftMatchPara.szOutputFilename = txtOutputFilename.Text;

                m_pSiftMatchPara.dbLeftTopOffsetX = ptLeftTopOffset.X;
                m_pSiftMatchPara.dbLeftTopOffsetY = ptLeftTopOffset.Y;

                m_pSiftMatchPara.dbRatio = dbiRatio.Value;
                m_pSiftMatchPara.nMaxBlockSize = Convert.ToUInt32(iiMaxBlockSize.Value);
                m_pSiftMatchPara.nOverlaySize = Convert.ToUInt32(iiOverlaySize.Value);
                m_pSiftMatchPara.dbRansacSigma = doubleRansac.Value;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.Cancel;
                return;
            }            
        }

        //拉伸到灰度图像
        private bool stretchToGrayImage(IRaster pSrcRaster, string szOutputFilename, double dbMinThreshold = 0.02, double dbMaxThreshold=0.98)
        {
            try
            {
                if (pSrcRaster == null || szOutputFilename == null)
                    return false;

                //深拷贝
                IClone pSrcClone = pSrcRaster as IClone;
                IClone pDstClone = pSrcClone.Clone();
                IRaster pDstRaster = pDstClone as IRaster;
                IRaster2 pDstRaster2 = pDstRaster as IRaster2;
                IRasterProps pRasterProps = pDstRaster as IRasterProps;
                
                //原始统计信息
                bool bFlag = false;
                IRasterBand pRasterBand = (pDstRaster as IRasterBandCollection).Item(0);
                pRasterBand.HasStatistics(out bFlag);
                if (!bFlag)
                    pRasterBand.ComputeStatsAndHist();

                //得到原始栅格影像的最大最小值和步长
                double dbSrcMaxValue = pRasterBand.Statistics.Maximum;
                double dbSrcMinValue = pRasterBand.Statistics.Minimum;
                double dbSrcStep=(dbSrcMaxValue-dbSrcMinValue)/256;
                double dbStretchMaxValue = pRasterBand.Statistics.Maximum;
                double dbStretchMinValue = pRasterBand.Statistics.Minimum;

                #region 灰度映射，利用重分类来做      
          
                //第一次重分类
                IReclassOp reclassOp = new RasterReclassOpClass();
                INumberRemap numberRemap = new NumberRemapClass();
                for (int i = 0; i < 256;i++ )
                {
                    numberRemap.MapRange(dbSrcMinValue + i * dbSrcStep, dbStretchMinValue + (i + 1) * dbSrcStep, i);
                }
                IRaster pRasterTemp = reclassOp.ReclassByRemap((IGeoDataset)(pDstRaster2.RasterDataset), (IRemap)numberRemap, true) as IRaster;
                IRaster2 pRasterTemp2 = pRasterTemp as IRaster2;
                IRasterProps pRasterTempProps = pRasterTemp as IRasterProps;
                
                //得到属性表
                IRasterDatasetEdit3 pRasterDatasetEdit3 = pRasterTemp2.RasterDataset as IRasterDatasetEdit3;
                pRasterDatasetEdit3.ComputeStatisticsHistogram(1, 1, null, true);
                IRasterBand rasterBand = (pRasterTempProps as IRasterBandCollection).Item(0);
                IRasterHistogram rasterHistogram = rasterBand.Histogram;
                IRasterStatistics rasterStatistics = rasterBand.Statistics;
                
                double[] pHistogramCount=(double[])rasterHistogram.Counts;

                int nCurrentCount = 0;
                int nIndex = 0;
                int nPixelCount = 0;//pRasterProps.Width * pRasterProps.Height;
                //double dSum = 0.0;
                for (int i = 0; i < 256;i++  )
                {
                    nPixelCount += (int)pHistogramCount[i];
                }
                //得到拉伸的最大最小值
                while (Convert.ToDouble(nCurrentCount) / nPixelCount < dbMinThreshold) 
                {
                    nCurrentCount += Convert.ToInt32(pHistogramCount[nIndex++]);
                }
                dbStretchMinValue=nIndex*dbSrcStep+dbSrcMinValue;

                while (Convert.ToDouble(nCurrentCount) / nPixelCount < dbMaxThreshold)
                {
                    nCurrentCount += Convert.ToInt32(pHistogramCount[nIndex++]);
                }
                dbStretchMaxValue = nIndex * dbSrcStep + dbSrcMinValue;

                dbSrcStep = (dbStretchMaxValue - dbStretchMinValue) / 256;
                #endregion

                #region 拉伸
                INumberRemap numberRemapForStretch = new NumberRemapClass();

                numberRemapForStretch.MapRange(dbSrcMinValue, dbStretchMinValue + dbSrcStep,  0);
                for (int i = 1; i < 255; i++)
                {
                    numberRemapForStretch.MapRange(dbStretchMinValue + i * dbSrcStep, dbStretchMinValue + (i + 1) * dbSrcStep, i);
                }
                numberRemapForStretch.MapRange(dbStretchMinValue + 255 * dbSrcStep, dbSrcMaxValue, 255);

                IRaster pRasterStretch = reclassOp.ReclassByRemap((IGeoDataset)((pDstRaster as IRaster2).RasterDataset), (IRemap)numberRemapForStretch, true) as IRaster;
                IRasterProps pRasterStretchProps = pRasterStretch as IRasterProps;

#region 注释代码
                ////拉伸
                //IPixelBlock3 pixelBlock3 = null;
                //IRasterCursor rasterCursor = (pDstRaster as IRaster2).CreateCursorEx(null);//null时为128*128

                //do
                //{
                //    pixelBlock3 = rasterCursor.PixelBlock as IPixelBlock3;
                //    int nWidth = pixelBlock3.Width;
                //    int nHeight = pixelBlock3.Height;

                //    System.Array pixels = (System.Array)pixelBlock3.get_PixelData(0);
                //    for (int m = 0; m < nWidth; m++)
                //    {
                //        for (int n = 0; n < nHeight; n++)
                //        {
                //            double dbSrcValue = Convert.ToDouble(pixels.GetValue(m, n));
                //            if (double.IsNaN(dbSrcValue) || dbSrcValue == dbNoDataValue)
                //                continue;

                //            if (dbSrcValue >= dbStretchMaxValue)
                //                pixels.SetValue(255, m, n);
                //            else if (dbSrcValue <= dbStretchMinValue)
                //                pixels.SetValue(0, m, n);
                //            else
                //            {
                //                byte dbDstValue = Convert.ToByte((dbSrcValue - dbStretchMinValue) / (dbStretchMaxValue - dbStretchMinValue) * 255);
                //                pixels.SetValue(dbDstValue, m, n);
                //            }
                //        }
                //    }
                //    pixelBlock3.set_PixelData(0, (System.Array)pixels);

                //    //修改数据
                //    pRasterEdit.Write(rasterCursor.TopLeft, pixelBlock3 as IPixelBlock);
                //    pRasterEdit.Refresh();
                //} while (rasterCursor.Next() == true);
#endregion
                
                #endregion

                #region 存储数据
                IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactoryClass();
                IWorkspace inmemWor = pWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(szOutputFilename), 0);
                pRasterStretchProps.PixelType = rstPixelType.PT_UCHAR;
                ISaveAs2 pSaveAs = pRasterStretchProps as ISaveAs2;
                if (pSaveAs == null) return false;

                IRasterStorageDef pRSDef = new RasterStorageDefClass();
                IRasterDataset pDataset = pSaveAs.SaveAsRasterDataset(System.IO.Path.GetFileName(szOutputFilename), inmemWor, "TIFF", pRSDef);
                (pDataset as IRasterDataset3).Refresh();
                //IRasterEdit pRasterEdit = pDstRaster as IRasterEdit;

                IRasterEdit pRasterTempEdit = pRasterTemp as IRasterEdit;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterTempEdit);
                IRasterEdit pRasterStretchEdit = pRasterStretch as IRasterEdit;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterStretchEdit);
                #endregion

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}

