using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;

namespace LibCerMap
{
    public class ClsRasterMeaAttribute
    {
        public void RasterMeaAtt(IRaster pRaster,IGeometry pGeoMetry,ref double pixelmax,ref double pixelmin)
        {
            IPolygon clipGeo = pGeoMetry as IPolygon;
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
            IRaster clipRaster=null; //裁切后得到的IRaster
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
                //return false;
            }

            CalRasterAtt(clipRaster as IRaster2,ref pixelmax,ref pixelmin);
        }

        public void CalRasterAtt(IRaster2 pRaster2, ref double dmax, ref double dmin)
        {
            double pixelmax = -999999999;//记录栅格回的最大值
            double pixermin = 999999999;//记录栅格灰度最小值

            object obj = null;
            double dPixel = 0.0;

            int nWidth = 0;
            int nHeight = 0;
            System.Array pixels;
            IPixelBlock3 pixelBlock3 = null;
            IRasterCursor rasterCursor = pRaster2.CreateCursorEx(null);//null时为128*128

            do
            {
                pixelBlock3 = rasterCursor.PixelBlock as IPixelBlock3;
                nWidth = pixelBlock3.Width;
                nHeight = pixelBlock3.Height;
                pixels = (System.Array)pixelBlock3.get_PixelData(0);
                for (int i = 0; i < nWidth; i++)
                {
                    for (int j = 0; j < nHeight; j++)
                    {
                        obj = pixels.GetValue(i, j);
                        double ob = Convert.ToDouble(obj);
                        if (ob > -99999 && ob < 99999)
                        {
                            dPixel = Convert.ToDouble(obj);
                            if (dPixel >= pixelmax)
                            {
                                pixelmax = dPixel;
                            }
                            if (dPixel <= pixermin)
                            {
                                pixermin = dPixel;
                            }
                        }
                    }
                }

            } while (rasterCursor.Next() == true);

            dmax = pixelmax;
            dmin = pixermin;
           
        }
    }
}
