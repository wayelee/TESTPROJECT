using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;

namespace LibCerMap
{
    public class ClsSiftMatching
    {
        //private IRaster2 m_pRasterLeft = null;
        //private IRaster2 m_pRasterRight = null;
        public ClsSiftMatching()
        {

        }

        //MLAPI( double* ) mlSiftMatchVT( const SCHAR* strLPath, const SCHAR* strRPath, DOUBLE dRatio, UINT nMaxBlockSize, UINT nOverLaySize, int &nPtSize );
        [DllImport("mlRVML.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr mlSiftMatchVT(string szLeftFilename, string szRightFilename, double dbRatio, double dbRansacSigma, uint nMaxBlockSize, uint nOverlaySize, ref int nCount);

        [DllImport("mlRVML.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool mlFreeSiftPts(IntPtr pData);

        [DllImport("mlRVML.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool mlHistogramEqualize(string szLeft, string szRight);

#region 注释代码
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

        ////从RASTER中根据SHP截取子图
        //private bool GetRasterSubset(string szSrcRasterFilename, string szShpFilename, string szOutputRasterFilename, out IPoint ptLeftTopOffset)
        //{
        //    ptLeftTopOffset = null;
        //    if (string.IsNullOrEmpty(szOutputRasterFilename) || string.IsNullOrEmpty(szSrcRasterFilename) || string.IsNullOrEmpty(szShpFilename))
        //        return false;

        //    try
        //    {
        //        //打开源文件
        //        RasterLayerClass rasterlayer = new RasterLayerClass();
        //        rasterlayer.CreateFromFilePath(szSrcRasterFilename);

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
        

        public bool siftMatching(/*IRaster2 rasterLeft,IRaster2 rasterRight,*/SiftMatchPara pSiftMatchPara, out double[] dbMatchPts,out int pCount)
        {
            if (pSiftMatchPara == null /*|| rasterLeft ==null || rasterRight ==null*/)
            {
                pCount = 0;
                dbMatchPts = null;
                return false;
            }
            //m_pRasterLeft =rasterLeft;
            //m_pRasterRight =rasterRight;

            string szLeftFilename = pSiftMatchPara.szLeftFilename;  //底图
            string szRightFilename = pSiftMatchPara.szRightFilename;  //待匹配的图
            //string szOutputFilename = pSiftMatchPara.szOutputFilename; //匹配点输出路径 
            if (string.IsNullOrEmpty(szLeftFilename) || string.IsNullOrEmpty(szRightFilename) )
            {
                pCount = 0;
                dbMatchPts = null;
                return false;
            }

            double dbRatio = pSiftMatchPara.dbRatio;
            uint nMaxBlockSize = pSiftMatchPara.nMaxBlockSize;
            uint nOverlaySize = pSiftMatchPara.nOverlaySize;
            double dbRansacSigma = pSiftMatchPara.dbRansacSigma;
            double ptLeftTopOffsetX = pSiftMatchPara.dbLeftTopOffsetX;
            double ptLeftTopOffsetY = pSiftMatchPara.dbLeftTopOffsetY;

            try
            {
                int nCount = 0;
                IntPtr pMatchPoint = mlSiftMatchVT(szLeftFilename, szRightFilename, dbRatio, dbRansacSigma, nMaxBlockSize, nOverlaySize, ref nCount);
                pCount = nCount;
                if (pMatchPoint == null || pCount <= 0)
                {
                    dbMatchPts = null;
                    return false;
                }
                dbMatchPts = new double[nCount*4];
                Marshal.Copy(pMatchPoint, dbMatchPts, 0, nCount*4);

                //修正左上角偏移量
                for (int i = 0; i < nCount; i++)
                {
                    dbMatchPts[4 * i + 2] += ptLeftTopOffsetX;
                    dbMatchPts[4 * i + 3] += ptLeftTopOffsetY;
                }
                //outputMatchPointsToFile("d:\\a.txt", dbMatchPts, nCount);

                ////转换到地理坐标
                //int x = 0;
                //int y = 0;
                ////int nLeftHeight = (m_pRasterLeft as IRasterProps).Height;
                ////int nRightHeight = (m_pRasterRight as IRasterProps).Height;
                //for (int i = 0; i < nCount; i++)
                //{
                //    x = (int)dbMatchPts[4 * i];
                //    y = (int)(dbMatchPts[4 * i + 1]);
                //    //y = (int)(-1*dbMatchPts[4 * i + 1]);
                //    m_pRasterLeft.PixelToMap(x, y, out dbMatchPts[4 * i], out dbMatchPts[4 * i + 1]);

                //    x = (int)dbMatchPts[4 * i + 2];
                //    y = (int)(dbMatchPts[4 * i + 3]);
                //    //y = (int)(-1*dbMatchPts[4 * i + 3]);
                //    m_pRasterRight.PixelToMap(x, y, out dbMatchPts[4 * i + 2], out dbMatchPts[4 * i + 3]);
                //}
#region 转换到地理坐标
                ////转换到地理坐标
                //int x = 0;
                //int y = 0;

                //IRasterProps pLeftProps = m_pRasterLeft as IRasterProps;
                //IPoint pLeftLowerLeft=pLeftProps.Extent.LowerLeft;
                //IRasterProps pRightProps = m_pRasterRight as IRasterProps;
                //IPoint pRightLowerLeft = pRightProps.Extent.LowerLeft;

                //for (int i = 0; i < nCount; i++)
                //{
                //    x = (int)dbMatchPts[4 * i];
                //    y = (int)(dbMatchPts[4 * i + 1]);
                //    dbMatchPts[4*i] = x * pLeftProps.MeanCellSize().X + pLeftLowerLeft.X;
                //    dbMatchPts[4*i+1] = y * pLeftProps.MeanCellSize().Y + pLeftLowerLeft.Y;
                    
                //    //y = (int)(-1*dbMatchPts[4 * i + 1]);
                //    //m_pRasterLeft.PixelToMap(x, y, out dbMatchPts[4 * i], out dbMatchPts[4 * i + 1]);

                //    x = (int)dbMatchPts[4 * i + 2];
                //    y = (int)(dbMatchPts[4 * i + 3]);
                //    dbMatchPts[4 * i+2] = x * pRightProps.MeanCellSize().X + pRightLowerLeft.X;
                //    dbMatchPts[4 * i + 3] = y * pRightProps.MeanCellSize().Y + pRightLowerLeft.Y;
                //    //y = (int)(-1*dbMatchPts[4 * i + 3]);
                //    //m_pRasterRight.PixelToMap(x, y, out dbMatchPts[4 * i + 2], out dbMatchPts[4 * i + 3]);
                //}
#endregion
                //保存匹配点
                //outputMatchPointsToFile("d:\\a.txt", dbMatchPts, nCount);
                return mlFreeSiftPts(pMatchPoint);
                //return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                pCount = 0;
                dbMatchPts = null;
                return false;
            }
        }

        public bool outputMatchPointsToFile(string szFilename, double[]dbMatchPts, int nCount)
        {
            if (szFilename == null || dbMatchPts==null || nCount<=0)
                return false;

            try
            {
                StreamWriter sw = new StreamWriter(szFilename);
                for (int i = 0; i < nCount; i++)
                {
                    sw.Write("{0:f6}\t{1:f6}\t{2:f6}\t{3:f6}", dbMatchPts[4 * i + 0], dbMatchPts[4 * i + 1], dbMatchPts[4 * i + 2], dbMatchPts[4 * i + 3]);
                    sw.WriteLine();
                }

                sw.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;	
            }
        }
    }

    //SIFT匹配参数类
    public class SiftMatchPara
    {
        public string szLeftFilename = null;
        public string szRightFilename = null;
        //public string szOutputFilename = null;
        //public string szRegionFilename = null;

        public double dbLeftTopOffsetX = 0;
        public double dbLeftTopOffsetY = 0;
        public double dbRatio = 0.7;
        public uint nMaxBlockSize = 1024;
        public uint nOverlaySize = 300;
        public double dbRansacSigma = 1.0;

        public SiftMatchPara()
        {

        }
    }

    public class RasterPara
    {
        //栅格左上角地理坐标
        public double m_dLTX = 0.0;
        public double m_dLTY = 0.0;
        //栅格大小
        public double m_dPixelX = 1.0;
        public double m_dPixelY = 1.0;

        public RasterPara()
        {

        }

    }

}