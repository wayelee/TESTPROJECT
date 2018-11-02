using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;

namespace LibCerMap
{
    public class ClsRasterOp
    {
        public ClsRasterOp()
        {

        }

        public void ChangeRasterValue(IRasterDataset2 pRasterDatset, double dbScale, double dbOffset)
        {
            try
            {
                IRaster2 pRaster2 = pRasterDatset.CreateFullRaster() as IRaster2;

                IPnt pPntBlock = new PntClass();

                pPntBlock.X = 128;
                pPntBlock.Y = 128;

                IRasterCursor pRasterCursor = pRaster2.CreateCursorEx(pPntBlock);
                IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;

                if (pRasterEdit.CanEdit())
                {
                    IRasterBandCollection pBands = pRasterDatset as IRasterBandCollection;
                    IPixelBlock3 pPixelblock3 = null;
                    int pBlockwidth = 0;
                    int pBlockheight = 0;
                    System.Array pixels;
                    IPnt pPnt = null;
                    object pValue;
                    long pBandCount = pBands.Count;

                    //获取Nodata
                    IRasterProps pRasterPro = pRaster2 as IRasterProps;
                    object pNodata = pRasterPro.NoDataValue;
                    //double dbNoData = Convert.ToDouble(((double[])pNodata)[0]);
                    double dbNoData = getNoDataValue(pNodata);

                    do
                    {
                        pPixelblock3 = pRasterCursor.PixelBlock as IPixelBlock3;
                        pBlockwidth = pPixelblock3.Width;
                        pBlockheight = pPixelblock3.Height;

                        for (int k = 0; k < pBandCount; k++)
                        {
                            pixels = (System.Array)pPixelblock3.get_PixelData(k);
                            for (int i = 0; i < pBlockwidth; i++)
                            {
                                for (int j = 0; j < pBlockheight; j++)
                                {
                                    pValue = pixels.GetValue(i, j);
                                    double ob = Convert.ToDouble(pValue);
                                    if (ob != dbNoData)
                                    {
                                        ob *= dbScale;  //翻转
                                        ob += dbOffset; //Z方向偏移                                
                                    }

                                    IRasterProps pRP = pRaster2 as IRasterProps;
                                    if (pRP.PixelType == rstPixelType.PT_CHAR)
                                        pixels.SetValue(Convert.ToChar(ob), i, j);
                                    else if (pRP.PixelType == rstPixelType.PT_UCHAR)
                                        pixels.SetValue(Convert.ToByte(ob), i, j);
                                    else if (pRP.PixelType == rstPixelType.PT_FLOAT)
                                        pixels.SetValue(Convert.ToSingle(ob), i, j);
                                    else if (pRP.PixelType == rstPixelType.PT_DOUBLE)
                                        pixels.SetValue(Convert.ToDouble(ob), i, j);
                                    else if (pRP.PixelType == rstPixelType.PT_ULONG)
                                        pixels.SetValue(Convert.ToInt32(ob), i, j);
                                    else
                                        ;
                                }
                            }
                            pPixelblock3.set_PixelData(k, pixels);

                            System.Array textPixel = null;
                            textPixel = (System.Array)pPixelblock3.get_PixelData(k);
                        }

                        pPnt = pRasterCursor.TopLeft;
                        pRasterEdit.Write(pPnt, (IPixelBlock)pPixelblock3);
                    }
                    while (pRasterCursor.Next());

                    //改变了Z值，重新统计下直方图
                    //IRasterDataset2 prd = pRaster2 as IRasterDataset2;
                    IRasterDatasetEdit3 pRedtit = pRasterDatset as IRasterDatasetEdit3;
                    pRedtit.DeleteStats();//This method is avaliable only on raster datasets in File and ArcSDE geodatabases.
                    pRedtit.ComputeStatisticsHistogram(1, 1, null, true);

                    pRasterEdit.Refresh();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public double getNoDataValue(object pObject)
        {
            double dbNoData = double.NaN;

            if (pObject is double[])
                dbNoData = Convert.ToDouble(((double[])pObject)[0]);
            else if (pObject is float[])
                dbNoData = Convert.ToDouble(((float[])pObject)[0]);
            else if (pObject is Int64[])
                dbNoData = Convert.ToDouble(((Int64[])pObject)[0]);
            else if (pObject is Int32[])
                dbNoData = Convert.ToDouble(((Int32[])pObject)[0]);
            else if (pObject is Int16[])
                dbNoData = Convert.ToDouble(((Int16[])pObject)[0]);
            else if (pObject is byte[])
                dbNoData = Convert.ToDouble(((byte[])pObject)[0]);
            else if (pObject is char[])
                dbNoData = Convert.ToDouble(((char[])pObject)[0]);
            else
                ;

            return dbNoData;
        }

        //采用另一种方法实现互换XY，不会改变影像的分辨率
        public bool NorthEastToEastNorth(IRasterLayer pRasterLayer, string szFilename)//坐标系x,y变换
        {
            if (pRasterLayer == null || szFilename == null)
                return false;

            try
            {
                ITransformationOp dstTrans = new RasterTransformationOpClass();
                IRasterAnalysisEnvironment pDstAnalysisEnvironment = dstTrans as IRasterAnalysisEnvironment;
                if (pDstAnalysisEnvironment == null)
                    return false;

                //设置分辨率
                IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
                IRasterProps pProps = pRaster2 as IRasterProps;
                double dbCellSizeX = pProps.MeanCellSize().X;
                double dbCellSizeY = pProps.MeanCellSize().Y;
                double dbCellSizeMean = (dbCellSizeX + dbCellSizeY) / 2;
                //pDstAnalysisEnvironment.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, dbCellSizeMean);

                //定义原始数据集和目标数据集
                IGeoDataset pSrcGeoDataset = pRasterLayer.Raster as IGeoDataset;
                IGeoDataset pDstGeoDataset = null;
                IGeoDataset pTmpGeoDataset = null;

                //顺时针旋转90度
                IPoint pt = new PointClass();
                pt.X = 0;
                pt.Y = 0;
                pTmpGeoDataset = dstTrans.Rotate(pSrcGeoDataset, esriGeoAnalysisResampleEnum.esriGeoAnalysisResampleCubic, 90, pt);
                //pDstGeoDataset = pTmpGeoDataset;

                //水平旋转            
                pSrcGeoDataset = dstTrans.Flip(pTmpGeoDataset);

                //由于翻转是以图像中心为界，所以要加上偏移量
                IEnvelope pExtent = pSrcGeoDataset.Extent;
                double dbCenterY = (pExtent.UpperLeft.Y + pExtent.LowerRight.Y) / 2;
                double dbDeltaX = 0;
                double dbDeltaY = -dbCenterY * 2;
                //pDstGeoDataset = dstTrans.Flip(pTmpGeoDataset);                
                pDstGeoDataset = dstTrans.Shift(pSrcGeoDataset, dbDeltaX, dbDeltaY, null);
                if (pDstGeoDataset == null)
                    return false;

                IRasterLayer pLayer = new RasterLayerClass();
                pLayer.CreateFromDataset(pDstGeoDataset as IRasterDataset);
                

                IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
                IWorkspace inmemWor = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(szFilename), 0);
                ISaveAs2 sa = pDstGeoDataset as ISaveAs2;
                //IRaster2 pR2 = ((IRasterDataset3)pDstGeoDataset).CreateDefaultRaster() as IRaster2;
                IRaster2 pR2 =pLayer.Raster as IRaster2;
                IRasterEdit pRe = pR2 as IRasterEdit;
                
                //pRe.Refresh();
               // sa = ((IRasterDataset3)pDstGeoDataset).CreateDefaultRaster() as ISaveAs2;
                sa = pRe as ISaveAs2;
                if (sa != null)
                {
                    sa.SaveAs(System.IO.Path.GetFileName(szFilename), inmemWor, "TIFF");
                    //sa.SaveAsRasterDataset()
                    pRe.Refresh();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pRe);
                    (pDstGeoDataset as IRasterDataset3).Refresh();
                    
                }

                IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //采用另一种方法实现互换XY，不会改变影像的分辨率
        public bool NorthEastToEastNorth(string szSrcFilename, string szFilename)//坐标系x,y变换
        {
            if (szSrcFilename == null || szFilename == null)
                return false;

            try
            {
                IRasterLayer pRasterLayer = new RasterLayerClass();
                pRasterLayer.CreateFromFilePath(szSrcFilename);
                return NorthEastToEastNorth(pRasterLayer, szFilename);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
            //try
            //{
            //    //由文件名创建RASTERLAYER
                
            //    ITransformationOp dstTrans = new RasterTransformationOpClass();
            //    IRasterAnalysisEnvironment pDstAnalysisEnvironment = dstTrans as IRasterAnalysisEnvironment;
            //    if (pDstAnalysisEnvironment == null)
            //        return false;

            //    //设置分辨率
            //    IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
            //    IRasterProps pProps = pRaster2 as IRasterProps;
            //    double dbCellSizeX = pProps.MeanCellSize().X;
            //    double dbCellSizeY = pProps.MeanCellSize().Y;
            //    double dbCellSizeMean = (dbCellSizeX + dbCellSizeY) / 2;
            //    //pDstAnalysisEnvironment.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, dbCellSizeMean);

            //    //定义原始数据集和目标数据集
            //    IGeoDataset pSrcGeoDataset = pRasterLayer.Raster as IGeoDataset;
            //    IGeoDataset pDstGeoDataset = null;
            //    IGeoDataset pTmpGeoDataset = null;

            //    //顺时针旋转90度
            //    IPoint pt = new PointClass();
            //    pt.X = 0;
            //    pt.Y = 0;
            //    pTmpGeoDataset = dstTrans.Rotate(pSrcGeoDataset, esriGeoAnalysisResampleEnum.esriGeoAnalysisResampleCubic, 90, pt);
            //    //pDstGeoDataset = pTmpGeoDataset;

            //    //水平旋转            
            //    pSrcGeoDataset = dstTrans.Flip(pTmpGeoDataset);

            //    //由于翻转是以图像中心为界，所以要加上偏移量
            //    IEnvelope pExtent = pSrcGeoDataset.Extent;
            //    double dbCenterY = (pExtent.UpperLeft.Y + pExtent.LowerRight.Y) / 2;
            //    double dbDeltaX = 0;
            //    double dbDeltaY = -dbCenterY * 2;
            //    //pDstGeoDataset = dstTrans.Flip(pTmpGeoDataset);                
            //    pDstGeoDataset = dstTrans.Shift(pSrcGeoDataset, dbDeltaX, dbDeltaY, null);
            //    if (pDstGeoDataset == null)
            //        return false;

            //    IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            //    IWorkspace inmemWor = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(szFilename), 0);
            //    ISaveAs2 sa = pDstGeoDataset as ISaveAs2;

            //    if (sa != null)
            //    {
            //        sa.SaveAs(System.IO.Path.GetFileName(szFilename), inmemWor, "TIFF");
            //        (pDstGeoDataset as IRasterDataset3).Refresh();
            //    }

            //    IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;
            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);
            //    return true;
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return false;
            //}

        }

        //这个版本虽然XY互换了，但会改变影像的分辨率
         public bool NorthEastToEastNorth2(IRasterLayer pRasterLayer, string szFilename)//坐标系x,y变换
        {
            if (pRasterLayer == null || szFilename == null)
                return false;

            IGeoReference pGeoReference = pRasterLayer as IGeoReference;
            IPoint pt = new PointClass();
            pt.X = 0;
            pt.Y = 0;
            pGeoReference.Rotate(pt, -90); //顺时针旋转90

            //水平旋转
            IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
            IRasterProps pProps = pRaster2 as IRasterProps;
            int nWidth = pProps.Width;
            int nHeight = pProps.Height;
            double dbCenterY = (pProps.Extent.UpperLeft.Y + pProps.Extent.LowerRight.Y) / 2;
            double dbDeltaX = 0;
            double dbDeltaY = -dbCenterY * 2;
            pGeoReference.Flip();
            pGeoReference.Shift(dbDeltaX, dbDeltaY);

            //pGeoReference.Rectify(szFilename, "TIFF");
            //pGeoReference.Reset();
            IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);

            return true;
        }

         //这个版本虽然XY互换了，但会改变影像的分辨率
         public bool NorthEastToEastNorth3(IRasterLayer pRasterLayer)//坐标系x,y变换
         {
             if (pRasterLayer == null)
                 return false;

             IGeoReference pGeoReference = pRasterLayer as IGeoReference;
             IPoint pt = new PointClass();
             pt.X = 0;
             pt.Y = 0;
             pGeoReference.Rotate(pt, -90); //顺时针旋转90

             //水平旋转
             IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
             IRasterProps pProps = pRaster2 as IRasterProps;
             int nWidth = pProps.Width;
             int nHeight = pProps.Height;
             double dbCenterY = (pProps.Extent.UpperLeft.Y + pProps.Extent.LowerRight.Y) / 2;
             double dbDeltaX = 0;
             double dbDeltaY = -dbCenterY * 2;
             pGeoReference.Flip();
             pGeoReference.Shift(dbDeltaX, dbDeltaY);

             //IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;
             //System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);

             return true;
         }
    }
}
