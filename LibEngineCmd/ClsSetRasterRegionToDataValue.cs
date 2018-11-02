using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using LibCerMap;

namespace LibEngineCmd
{
    public class ClsSetRasterRegionToDataValue
    {
        private IRaster m_pSrcRaster = null;
        private IPolygon m_pClipPolygon = null;
        private double m_dbNoDataValue = double.NaN;

        private IPolygon intersectWithRasterEnvelope()
        {
            try
            {
                IRasterProps pRasterProps = m_pSrcRaster as IRasterProps;
                IEnvelope pSrcEnvelope = pRasterProps.Extent;

                ITopologicalOperator pTopologicalOp = m_pClipPolygon as ITopologicalOperator;
                IGeometry pDstGeometry=pTopologicalOp.Intersect(pSrcEnvelope as IGeometry,  esriGeometryDimension.esriGeometry2Dimension);
                IPolygon pResultPolygon = pDstGeometry as IPolygon;

                return pResultPolygon;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ClsSetRasterRegionToDataValue(IRaster pSrcRaster, IPolygon pClipPolygon, double dbNoDataValue)
        {
            m_pSrcRaster = pSrcRaster;
            m_pClipPolygon = pClipPolygon;
            m_dbNoDataValue = dbNoDataValue;

            //将传入的区域和栅格范围求交，得到交集范围
            //m_pClipPolygon=intersectWithRasterEnvelope();
        }

        private object getValidType(IRasterProps pProps, double dbValue)
        {
            object oReturnValue = null;
            if (pProps.PixelType == rstPixelType.PT_CHAR)
            {
                oReturnValue = Convert.ToChar(dbValue) as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_DOUBLE)
            {
                oReturnValue = Convert.ToDouble(dbValue) as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_FLOAT)
            {
                oReturnValue = Convert.ToSingle(dbValue) as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_LONG)
            {
                oReturnValue = Convert.ToInt32(dbValue) as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_SHORT)
            {
                oReturnValue = Convert.ToInt16(dbValue) as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_ULONG)
            {
                oReturnValue = Convert.ToUInt64(dbValue) as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_USHORT)
            {
                oReturnValue = Convert.ToUInt16(dbValue) as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_UCHAR)
            {
                oReturnValue = Convert.ToByte(dbValue) as object;
            }
            else
            {
                oReturnValue = null;
            }

            return oReturnValue;
        }

        public bool SetRegionToNoDataValue()
        {
            try
            {
                if (m_pSrcRaster == null || m_pClipPolygon == null || double.IsNaN(m_dbNoDataValue))
                    return false;

                IGeoDataset pSrcGeoDataset = m_pSrcRaster as IGeoDataset;
                IExtractionOp pRasterExtractionOp = new RasterExtractionOpClass();
                IRasterProps pSrcRasterProps = m_pSrcRaster as IRasterProps;
                double dbCellSize = (pSrcRasterProps.MeanCellSize().X + pSrcRasterProps.MeanCellSize().Y) / 2;

                //设置范围和分辨率
                IRasterAnalysisEnvironment pRasterAnalysisEnv = pRasterExtractionOp as IRasterAnalysisEnvironment;
                pRasterAnalysisEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, dbCellSize);
                pRasterAnalysisEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, m_pClipPolygon.Envelope, Type.Missing);
                //pRasterAnalysisEnv.OutSpatialReference = (m_pSrcRaster as IRasterProps).SpatialReference;

                //保留区域外的值，区域内的设置为原始栅格的无效值
                IGeoDataset pDstGeoDataset = pRasterExtractionOp.Rectangle(pSrcGeoDataset, m_pClipPolygon.Envelope, true);

                //逐点判断像素是否在区域内，在区域内则改变为设置值，否则不变
                IRelationalOperator pRelationalOp = m_pClipPolygon as IRelationalOperator;
                if (pDstGeoDataset is IRaster)
                {
                    //得到原始栅格的对象，用于修改
                    IRaster2 pSrcRaster2 = m_pSrcRaster as IRaster2;
                    IRasterDataset2 pSrcRasterDataset2 = pSrcRaster2.RasterDataset as IRasterDataset2;
                    IRaster pTmpRaster = pSrcRasterDataset2.CreateFullRaster();
                    IRasterEdit pSrcEdit = pTmpRaster as IRasterEdit;
                    //得到图层NoDataValue
                    IRasterProps rasterProps = pSrcRaster2 as IRasterProps;
                    double noData = ClsGDBDataCommon.getNoDataValue(rasterProps.NoDataValue);

                    //得到输出的栅格
                    IRaster2 pDstRaster2 = pDstGeoDataset as IRaster2;
                    IRasterProps pDstRasterProps = pDstRaster2 as IRasterProps;
                    IRasterCursor pDstRasterCursor = pDstRaster2.CreateCursorEx(null);
                    //pDstRasterCursor.Reset();

                    do
                    {
                        //得到当前处理的块
                        IPixelBlock3 pixelBlock3 = pDstRasterCursor.PixelBlock as IPixelBlock3;
                        int nWidth = pixelBlock3.Width;
                        int nHeight = pixelBlock3.Height;
                        IPnt ptLeftTop = pDstRasterCursor.TopLeft;

                        //block值转数组时，NoData转换时有时为NoData，有时为栅格中的最小值
                        System.Array array = pixelBlock3.get_PixelData(0) as System.Array;
                        
                        //逐点判断: 判断像素是否在区域内，在区域内则改变为设置值，否则不变
                        for (int i = 0; i < nWidth; i++)
                        {
                            for (int j = 0; j < nHeight; j++)
                            {
                                double dbX = double.NaN, dbY = double.NaN;

                                //得到当前像素点的地图坐标
                                int nCurrentX = Convert.ToInt32(ptLeftTop.X + i);
                                int nCurrentY = Convert.ToInt32(ptLeftTop.Y + j);
                                pDstRaster2.PixelToMap(nCurrentX, nCurrentY, out dbX, out dbY);                             
                                IPoint ptInMap = new PointClass();
                                ptInMap.X = dbX;
                                ptInMap.Y = dbY;

                                //判断是否在区域内
                                bool bFlag = pRelationalOp.Contains(ptInMap as IGeometry);
                                if (bFlag) //在当前区域内
                                {
                                    object oValidValue = getValidType(pDstRasterProps, m_dbNoDataValue);
                                    array.SetValue(oValidValue, i, j);
                                }
                                else
                                {
                                    double v = Convert.ToDouble(array.GetValue(i, j));
                                 
                                    if (v == 0  || v< -3.4e15|| v > 3.4e15 )
                                    //if (v == 0 || Math.Abs(v -noData) <1e18)
                                    {
                                        int col, row;                          
                                       pSrcRaster2.MapToPixel(dbX,dbY,out col, out row);
                                        //表示getpixelvalue为null表示nodata
                                        object obj = pSrcRaster2.GetPixelValue(0, col, row);
                                       if (obj == null)
                                        {
                                            object oValidValue = getValidType(pDstRasterProps, m_dbNoDataValue);
                                            array.SetValue(oValidValue, i, j);
                                        }
                                       else
                                       {
                                           array.SetValue(obj,i,j);
                                       }
                                     
                                    }

                                }
                            }
                        }
                        pixelBlock3.set_PixelData(0, array);

                        //得到当前区域块在原图中的左上角像素坐标, 直接修改原栅格的数据
                        int nPixelLeftX = -1, nPixelLeftY = -1;
                        double dbMapLeftTopX = double.NaN, dbMapLeftTopY = double.NaN;
                        pDstRaster2.PixelToMap(Convert.ToInt32(ptLeftTop.X), Convert.ToInt32(ptLeftTop.Y), out dbMapLeftTopX, out dbMapLeftTopY); //得到当前块左上角的地理坐标
                        pSrcRaster2.MapToPixel(dbMapLeftTopX, dbMapLeftTopY, out nPixelLeftX, out nPixelLeftY);

                        IPnt ptPixelLeftTop = new PntClass();
                        ptPixelLeftTop.SetCoords(nPixelLeftX, nPixelLeftY);
                        if (pSrcEdit.CanEdit())
                        {
                            pSrcEdit.Write(ptPixelLeftTop, pixelBlock3 as IPixelBlock);
                            //pSrcEdit.Refresh();
                        }
                        else
                            return false;
                    } while (pDstRasterCursor.Next() == true);

                    //更新
                    pSrcEdit.Refresh();
                }
                else
                    return false;

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
