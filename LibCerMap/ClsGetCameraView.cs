using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using LibModelGen;
using System.IO;
using System.Windows.Forms;

namespace LibCerMap
{

    public struct Ex_OriPara
    {
        public Pt_3d pos;//!<外方位线元素
        public Ori_Angle ori;//!<外方位角元素
    }

    public struct Pt_3d
    {
        public double X, Y, Z;
        public ulong lID;
    }

    public struct Ori_Angle
    {
        public double omg;//!<绕X轴转角
        public double phi;//!<绕Y轴转角
        public double kap;//!<绕Z轴转角
    }

    public class Pt2i
    {
        public int X, Y;//!<点X,Y坐标
        public long lID;//!<点序号

        public Pt2i()
        {
            X = Y = 0;
            lID = 0;
        }
    };

    public class Pt2d
    {
        public double X, Y;//!<点X,Y坐标
        public long lID;//!<点序号

        public Pt2d()
        {
            X = Y = 0;
            lID = 0;
        }
    };

    public class Pt3d
    {
        public double X, Y, Z;//!<点X,Y,Z坐标
        public long lID;//!<点序号

        public Pt3d()
        {
            X = Y = Z = 0;
            lID = 0;
        }
    };

    public class OriAngle
    {
        public double omg;//!<绕X轴转角
        public double phi;//!<绕Y轴转角
        public double kap;//!<绕Z轴转角
        public OriAngle()
        {
            omg = phi = kap = 0.0;
        }
    };

    public class ExOriPara
    {
        public Pt3d pos;//!<外方位线元素
        public OriAngle ori;//!<外方位角元素

        public ExOriPara()
        {
            pos=new Pt3d();
            ori=new OriAngle();
        }
    };


    public class InOriPara
    {
        public double dx;//主点x
        public double dy;//主点
        public double df;//焦距
        public double dfx;
        public double dfy;
        public int nW;//影像宽度
        public int nH;//影像高度
        public InOriPara()
        {
            dx = dy = df = dfx = dfy = 0.0;
            nW = nH = 0;            
        }
    }
    public class ClsGetCameraView
    {
        public bool GetDoubleVal(IRaster pRaster, double[,] dbData, Pt2i ptLeftTop, int nH, int nW, ref double dVal)
        {
            IRasterProps pProps = pRaster as IRasterProps;
            int nHeight = pProps.Height;
            int nWidth = pProps.Width;

            dVal = double.NaN;
            if ((nH >= nHeight) || (nW >= nWidth))
            {
                return false;
            }

            //相对坐标
            nW -= ptLeftTop.X;
            nH -= ptLeftTop.Y;

            int nSubWidth = dbData.GetLength(0);
            int nSubHeight = dbData.GetLength(1);
            if (nW < 0 || nW >= nSubWidth || nH < 0 || nH >= nSubHeight)
                return false;

            dVal = dbData[nW, nH];
            //IRasterBandCollection pBandCollection=pRaster as IRasterBandCollection;
            //IRasterBand pBand=pBandCollection.Item(0);
            //IRaster2 pRaster2 = pRaster as IRaster2;
            //dVal = Convert.ToDouble(pRaster2.GetPixelValue(0, nW, nH));
           

            return true;
        }

        //行列号的三维坐标
        public bool GetGeoXYZ(IRaster pRaster, double[,] dbData, Pt2i ptLeftTop, int nH, int nW, ref Pt3d ptXYZ)
        {
            ptXYZ = null;
            if (pRaster == null)
            {
                return false;
            }

            IRasterProps pProps = pRaster as IRasterProps;
            IRaster2 pRaster2 = pRaster as IRaster2;
            //if( ( m_pGdalData == NULL )||( m_pGdalData->m_bIsGeoFile != true) )
            //{
            //    return false;
            //}
            //CmlGeoRaster* pGeoRaster = (CmlGeoRaster*)m_pGdalData;
            ptXYZ = new Pt3d();
            ptXYZ.X = pRaster2.ToMapX(nW);//m_nXOffSet + nW)*pGeoRaster->m_dXResolution + pGeoRaster->m_PtOrigin.X ;
            ptXYZ.Y = pRaster2.ToMapY(nH);// + nH)*pGeoRaster->m_dYResolution + pGeoRaster->m_PtOrigin.Y ;

            //pProps.NoDataValue.GetType();
            double dbNoDataValue = getNoDataValue(pProps.NoDataValue);//Convert.ToDouble(((object[])pProps.NoDataValue)[0]);
            if ((true == GetDoubleVal(pRaster,dbData,ptLeftTop, nH, nW, ref ptXYZ.Z)) && (Math.Abs(ptXYZ.Z - dbNoDataValue) != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //当前地理位置的高程
        public bool GetGeoZ(IRaster pRaster, double[,] dbData, Pt2i ptLeftTop, double dX, double dY, ref double dZ)
        {
            dZ = double.NaN;
            if (pRaster == null)
            {
                return false;
            }

            IRaster2 pRaster2 = pRaster as IRaster2;
            double dXCoord = pRaster2.ToPixelColumn(dX);// dX - pGeoRaster->m_PtOrigin.X ) / pGeoRaster->m_dXResolution ;
            double dYCoord = pRaster2.ToPixelRow(dY);// dY - pGeoRaster->m_PtOrigin.Y ) / pGeoRaster->m_dYResolution ;

            IRasterProps pProps = pRaster as IRasterProps;
            int nWidth = pProps.Width;
            int nHeight = pProps.Height;

            if ((dXCoord < 0) || (dXCoord > (nWidth - 1)) || (dYCoord < 0) || (dYCoord > (nHeight - 1)))
            {
                return false;
            }

            Pt2i ptLT = new Pt2i();
            Pt2i ptRT = new Pt2i();
            Pt2i ptLB = new Pt2i();
            Pt2i ptRB = new Pt2i();
            ptLT.X = (int)Math.Floor(dXCoord);
            ptLT.Y = (int)Math.Floor(dYCoord);
            ptRT.X = ptLT.X + 1;
            ptRT.Y = ptLT.Y;
            ptLB.X = ptLT.X;
            ptLB.Y = ptLT.Y + 1;
            ptRB.X = ptRT.X;
            ptRB.Y = ptLB.Y;

            Pt3d ptZLT = new Pt3d();
            Pt3d ptZRT = new Pt3d();
            Pt3d ptZLB = new Pt3d();
            Pt3d ptZRB = new Pt3d();
            if (GetGeoXYZ(pRaster, dbData, ptLeftTop, ptLT.Y, ptLT.X, ref ptZLT) &&
                GetGeoXYZ(pRaster, dbData, ptLeftTop, ptRT.Y, ptRT.X, ref  ptZRT) &&
                GetGeoXYZ(pRaster, dbData, ptLeftTop, ptLB.Y, ptLB.X, ref  ptZLB) &&
                GetGeoXYZ(pRaster, dbData, ptLeftTop, ptRB.Y, ptRB.X, ref  ptZRB))
            {
                dZ = (ptRB.X - dXCoord) * (ptRB.Y - dYCoord) * ptZLT.Z +
                     (dXCoord - ptLT.X) * (ptRB.Y - dYCoord) * ptZRT.Z +
                     (ptRB.X - dXCoord) * (dYCoord - ptLT.Y) * ptZLB.Z +
                     (dXCoord - ptLT.X) * (dYCoord - ptLT.Y) * ptZRB.Z;

                return true;
            }
            else
            {
                return false;
            }

        }

        public IRasterWorkspace2 OpenRasterWorkspace(string PathName)
        {
            //This function opens a raster workspace.
            try
            {
                IWorkspaceFactory workspaceFact = new RasterWorkspaceFactoryClass();
                return workspaceFact.OpenFromFile(PathName, 0) as IRasterWorkspace2;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        static public bool OPK2RMat(OriAngle pOri, ref Matrix pRMat)
        {
            if (pRMat == null)
            {
                pRMat = new Matrix(3, 3);
            }

            if ((pRMat.row != 3) || (pRMat.col != 3))
            {
                return false;
            }

            double dSOmg = Math.Sin((pOri.omg));
            double dSPhi = Math.Sin((pOri.phi));
            double dSKap = Math.Sin((pOri.kap));

            double dCOmg = Math.Cos((pOri.omg));
            double dCPhi = Math.Cos((pOri.phi));
            double dCKap = Math.Cos((pOri.kap));

            pRMat.SetNum(0, 0, dCPhi * dCKap);
            pRMat.SetNum(0, 1, -dCPhi * dSKap);
            pRMat.SetNum(0, 2, dSPhi);

            pRMat.SetNum(1, 0, (dCOmg * dSKap + dSOmg * dSPhi * dCKap));
            pRMat.SetNum(1, 1, (dCOmg * dCKap - dSOmg * dSPhi * dSKap));
            pRMat.SetNum(1, 2, -dSOmg * dCPhi);

            pRMat.SetNum(2, 0, (dSOmg * dSKap - dCOmg * dSPhi * dCKap));
            pRMat.SetNum(2, 1, (dSOmg * dCKap + dCOmg * dSPhi * dSKap));
            pRMat.SetNum(2, 2, dCOmg * dCPhi);

            return true;

        }

#region 巡视器姿态转换
        public double Deg2Rad(double d)  //!<角度转弧度
        {
            return d / 180.0 * Math.PI;
        }

        public void att321(double x, double y, double z, ref double[,] lbw)
        {
            double sx, cx, sy, cy, sz, cz;
            lbw = new double[3, 3];

            sx = Math.Sin(x);
            cx = Math.Cos(x);
            sy = Math.Sin(y);
            cy = Math.Cos(y);
            sz = Math.Sin(z);
            cz = Math.Cos(z);

            lbw[0, 0] = cy * cz;
            lbw[0, 1] = cy * sz;
            lbw[0, 2] = -sy;
            lbw[1, 0] = sx * sy * cz - cx * sz;
            lbw[1, 1] = sx * sy * sz + cx * cz;
            lbw[1, 2] = sx * cy;
            lbw[2, 0] = cx * sy * cz + sx * sz;
            lbw[2, 1] = cx * sy * sz - sx * cz;
            lbw[2, 2] = cx * cy;
        }

        public void att321_IRSAVer(double x, double y, double z, ref double[,] lbw)
        {
            double sx, cx, sy, cy, sz, cz;
            lbw = new double[3, 3];

            sx = Math.Sin(x);
            cx = Math.Cos(x);
            sy = Math.Sin(y);
            cy = Math.Cos(y);
            sz = Math.Sin(z);
            cz = Math.Cos(z);

            lbw[0, 0] = cy * cz;
            lbw[1, 0] = cy * sz;
            lbw[2, 0] = -sy;
            lbw[0, 1] = sx * sy * cz - cx * sz;
            lbw[1, 1] = sx * sy * sz + cx * cz;
            lbw[2, 1] = sx * cy;
            lbw[0, 2] = cx * sy * cz + sx * sz;
            lbw[1, 2] = cx * sy * sz - sx * cz;
            lbw[2, 2] = cx * cy;
        }
#endregion


        private IRaster CreateRaster(Point2D ptLeftTop, double[] dbResolution, int[] nSize)
        {
            try
            {
                IWorkspaceFactory pworkspaceFactory = new RasterWorkspaceFactory();
                ESRI.ArcGIS.Geodatabase.IWorkspaceName pworkspaceName = pworkspaceFactory.Create(null, "MyWorkspace", null, 0);
                ESRI.ArcGIS.esriSystem.IName pname = (IName)pworkspaceName;
                ESRI.ArcGIS.Geodatabase.IWorkspace inmemWor = (IWorkspace)pname.Open();
                IRasterWorkspace2 rasterWs = (IRasterWorkspace2)inmemWor;

                //Define the spatial reference of the raster dataset.
                ISpatialReference sr = new UnknownCoordinateSystemClass();
                //Define the origin for the raster dataset, which is the lower left corner of the raster.
                IPoint origin = new PointClass();
                origin.PutCoords(ptLeftTop.X, ptLeftTop.Y);
                //Define the dimensions of the raster dataset.
                int width = nSize[0]; //This is the width of the raster dataset.
                int height = nSize[1]; //This is the height of the raster dataset.
                double xCell = dbResolution[0]; //This is the cell size in x direction.
                double yCell = dbResolution[1]; //This is the cell size in y direction.
                int NumBand = 1; // This is the number of bands the raster dataset contains.
                //Create a raster dataset in TIFF format.
                IRasterDataset rasterDataset = rasterWs.CreateRasterDataset("", "MEM",
                    origin, width, height, xCell, yCell, NumBand, rstPixelType.PT_UCHAR, sr,
                    true);

                //If you need to set NoData for some of the pixels, you need to set it on band 
                //to get the raster band.
                IRasterBandCollection rasterBands = (IRasterBandCollection)rasterDataset;
                IRasterBand rasterBand;
                IRasterProps rasterProps;
                rasterBand = rasterBands.Item(0);
                rasterProps = (IRasterProps)rasterBand;
                //Set NoData if necessary. For a multiband image, a NoData value needs to be set for each band.
                //rasterProps.NoDataValue = -9999f;
                //Create a raster from the dataset.
                IRaster raster = rasterDataset.CreateDefaultRaster();

                //Create a pixel block using the weight and height of the raster dataset. 
                //If the raster dataset is large, a smaller pixel block should be used. 
                //Refer to the topic "How to access pixel data using a raster cursor".
                //IPnt blocksize = new PntClass();
                //blocksize.SetCoords(width, height);
                //IPixelBlock3 pixelblock = raster.CreatePixelBlock(blocksize) as IPixelBlock3;

                ////Populate some pixel values to the pixel block.
                //System.Array pixels;
                //pixels = (System.Array)pixelblock.get_PixelData(0);
                //for (int i = 0; i < width; i++)
                //{
                //    for (int j = 0; j < height; j++)
                //    {
                //        pixels.SetValue(dbData[i, j], i, j);
                //    }
                //}
                //pixelblock.set_PixelData(0, (System.Array)pixels);

                ////Define the location that the upper left corner of the pixel block is to write.
                //IPnt upperLeft = new PntClass();
                //upperLeft.SetCoords(ptLeftTop.X, ptLeftTop.Y);

                ////Write the pixel block.
                //IRasterEdit rasterEdit = (IRasterEdit)raster;
                //rasterEdit.Write(upperLeft, (IPixelBlock)pixelblock);
                //rasterEdit.Refresh();

                //Release rasterEdit explicitly.
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterDataset);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterEdit);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterWs);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(sr);
                // GC.Collect();

                return raster;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }

            return null;
        }

        private IRaster createRasterWithoutData(Point2D ptLeftTop, double dbResolution, int[] nSize, string pRasterFile)
        {
            IRaster pRaster = null;

            try
            {
                if (File.Exists(pRasterFile))
                {
                    File.Delete(pRasterFile);
                }

                string Path = System.IO.Path.GetDirectoryName(pRasterFile);
                string FileName = System.IO.Path.GetFileName(pRasterFile);
                IRasterWorkspace2 rasterWs = OpenRasterWorkspace(Path);
                //Define the spatial reference of the raster dataset.
                ISpatialReference sr = new UnknownCoordinateSystemClass();
                //Define the origin for the raster dataset, which is the lower left corner of the raster.
                IPoint origin = new PointClass();
                origin.PutCoords(ptLeftTop.X, ptLeftTop.Y);
                //Define the dimensions of the raster dataset.
                int width = nSize[0]; //This is the width of the raster dataset.
                int height = nSize[1]; //This is the height of the raster dataset.
                double xCell = dbResolution; //This is the cell size in x direction.
                double yCell = dbResolution; //This is the cell size in y direction.
                int NumBand = 1; // This is the number of bands the raster dataset contains.
                //Create a raster dataset in TIFF format.
                IRasterDataset rasterDataset = rasterWs.CreateRasterDataset(FileName, "TIFF",
                    origin, width, height, xCell, yCell, NumBand, rstPixelType.PT_UCHAR, sr,
                    true);

                //Set NoData if necessary. For a multiband image, a NoData value needs to be set for each band.
                //rasterProps.NoDataValue = -9999;
                //Create a raster from the dataset.
                pRaster = rasterDataset.CreateDefaultRaster();

                return pRaster;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return null;
            }

            return pRaster;
        }

        private bool WriteToRaster(IRaster pRaster, byte[,] dbData, Point2D ptLeftTop)
        {
            //Create a pixel block using the weight and height of the raster dataset. 
            //If the raster dataset is large, a smaller pixel block should be used. 
            //Refer to the topic "How to access pixel data using a raster cursor".
            IRasterProps pProps = pRaster as IRasterProps;
            int width = pProps.Width;
            int height = pProps.Height;
            
            IPnt blocksize = new PntClass();
            blocksize.SetCoords(width, height);
            IPixelBlock3 pixelblock = pRaster.CreatePixelBlock(blocksize) as IPixelBlock3;

            //Populate some pixel values to the pixel block.
            System.Array pixels;
            pixels = (System.Array)pixelblock.get_PixelData(0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixels.SetValue(dbData[i, j], i, j);
                }
            }
            pixelblock.set_PixelData(0, (System.Array)pixels);

            //Define the location that the upper left corner of the pixel block is to write.
            IPnt upperLeft = new PntClass();
            upperLeft.SetCoords(ptLeftTop.X, ptLeftTop.Y);

            //Write the pixel block.
            IRasterEdit rasterEdit = (IRasterEdit)pRaster;
            rasterEdit.Write(upperLeft, (IPixelBlock)pixelblock);

            return true;
        }

        private bool writeBlockDataToFile(Point2D ptLeftTop, byte[,] dbData, int[] nSize, IRaster pRaster/*, IRasterEdit rasterEdit*/)
        {
            if (pRaster == null)
            {
                return false;
            }

            try
            {
                //Create a pixel block using the weight and height of the raster dataset. 
                //If the raster dataset is large, a smaller pixel block should be used. 
                //Refer to the topic "How to access pixel data using a raster cursor".
                int nWidth = nSize[0];
                int nHeight = nSize[1];

                IPnt blocksize = new PntClass();
                blocksize.SetCoords(nWidth, nHeight);
                IPixelBlock3 pixelblock = pRaster.CreatePixelBlock(blocksize) as IPixelBlock3;

                //Populate some pixel values to the pixel block.
                System.Array pixels;
                pixels = (System.Array)pixelblock.get_PixelData(0);
                for (int i = 0; i < nWidth; i++)
                {
                    for (int j = 0; j < nHeight; j++)
                    {
                        pixels.SetValue(dbData[i, j], i, j);
                    }
                }

                pixelblock.set_PixelData(0, (System.Array)pixels);

                //Define the location that the upper left corner of the pixel block is to write.
                IPnt upperLeft = new PntClass();
                upperLeft.SetCoords(ptLeftTop.X, ptLeftTop.Y);

                //Write the pixel block.
                IRasterEdit rasterEdit = (IRasterEdit)pRaster;
                rasterEdit.Write(upperLeft, (IPixelBlock)pixelblock);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterEdit);                
                rasterEdit.Refresh();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return false;
            }

            GC.Collect();
            return true;
        }

        public bool readBlockDataToFile(Point2D ptLeftTop, ref double[,] dbData, IRaster pRaster/*, IRasterEdit rasterEdit*/)
        {
            if (pRaster == null)
            {
                return false;
            }

            try
            {
                //Create a pixel block using the weight and height of the raster dataset. 
                //If the raster dataset is large, a smaller pixel block should be used. 
                //Refer to the topic "How to access pixel data using a raster cursor".
                int nWidth = dbData.GetLength(0);
                int nHeight = dbData.GetLength(1);

                IPnt blocksize = new PntClass();
                blocksize.SetCoords(nWidth, nHeight);

                //Define the location that the upper left corner of the pixel block is to write.
                IPnt upperLeft = new PntClass();
                upperLeft.SetCoords(ptLeftTop.X, ptLeftTop.Y);
                IPixelBlock3 pixelblock = pRaster.CreatePixelBlock(blocksize) as IPixelBlock3;
                pRaster.Read(upperLeft, pixelblock as IPixelBlock);

                //Populate some pixel values to the pixel block.
                System.Array pixels;
                pixels = (System.Array)pixelblock.get_PixelData(0);


                for (int i = 0; i < nWidth; i++)
                {
                    for (int j = 0; j < nHeight; j++)
                    {
                        dbData[i, j] = Convert.ToDouble(pixels.GetValue(i, j));//.GetType().;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return false;
            }

            GC.Collect();
            return true;
        }

        public double[,] getSubDem(IRaster pSrcRaster, Pt3d ptCenter, double nGeoRange, ref Pt2i ptImageTopLeft)
        {
            //IRaster pDstRaster = null;
            if (pSrcRaster == null)
                return null;

            IRasterProps pProps = pSrcRaster as IRasterProps;      
            IEnvelope pEnvelop=pProps.Extent;

            Pt2d[] ptRange = new Pt2d[4];
            for (int i = 0; i < 4; i++)
                ptRange[i] = new Pt2d();
            
            ptRange[0].X = ptCenter.X - nGeoRange;  //lefttop
            ptRange[0].X = Math.Max(ptRange[0].X, pEnvelop.UpperLeft.X);
            ptRange[0].Y = ptCenter.Y + nGeoRange;
            ptRange[0].Y = Math.Min(ptRange[0].Y, pEnvelop.UpperLeft.Y);

            ptRange[1].X = ptCenter.X + nGeoRange;  //righttop
            ptRange[1].Y = ptCenter.Y +nGeoRange;
            ptRange[1].X = Math.Min(ptRange[1].X, pEnvelop.UpperRight.X);
            ptRange[1].Y = Math.Min(ptRange[1].Y, pEnvelop.UpperRight.Y);

            ptRange[2].X = ptCenter.X - nGeoRange;  //leftbottom
            ptRange[2].Y = ptCenter.Y - nGeoRange;
            ptRange[2].X = Math.Max(ptRange[2].X, pEnvelop.LowerLeft.X);
            ptRange[2].Y = Math.Max(ptRange[2].Y, pEnvelop.LowerLeft.Y);

            ptRange[3].X = ptCenter.X + nGeoRange;  //rightbottom
            ptRange[3].Y = ptCenter.Y - nGeoRange;
            ptRange[3].X = Math.Min(ptRange[3].X, pEnvelop.LowerRight.X);
            ptRange[3].Y = Math.Max(ptRange[3].Y, pEnvelop.LowerRight.Y);

            Pt2i[] ptImageRange = new Pt2i[4];
            for (int i = 0; i < 4; i++)
                ptImageRange[i] = new Pt2i();

            IRaster2 pRaster2=pSrcRaster as IRaster2;
            ptImageRange[0].X = pRaster2.ToPixelColumn(ptRange[0].X);
            ptImageRange[0].Y = pRaster2.ToPixelRow(ptRange[0].Y);
            ptImageRange[1].X = pRaster2.ToPixelColumn(ptRange[1].X);
            ptImageRange[1].Y = pRaster2.ToPixelRow(ptRange[1].Y);
            ptImageRange[2].X = pRaster2.ToPixelColumn(ptRange[2].X);
            ptImageRange[2].Y = pRaster2.ToPixelRow(ptRange[2].Y);
            ptImageRange[3].X = pRaster2.ToPixelColumn(ptRange[3].X);
            ptImageRange[3].Y = pRaster2.ToPixelRow(ptRange[3].Y);

            ptImageTopLeft.X = Math.Min(Math.Min(Math.Min(ptImageRange[0].X, ptImageRange[1].X), ptImageRange[2].X), ptImageRange[3].X);
            ptImageTopLeft.Y = Math.Min(Math.Min(Math.Min(ptImageRange[0].Y, ptImageRange[1].Y), ptImageRange[2].Y), ptImageRange[3].Y);

            int[] nSize = new int[2];
            nSize[0] = Math.Max(Math.Max(Math.Max(ptImageRange[0].X, ptImageRange[1].X), ptImageRange[2].X), ptImageRange[3].X) - ptImageTopLeft.X;
            nSize[1] = Math.Max(Math.Max(Math.Max(ptImageRange[0].Y, ptImageRange[1].Y), ptImageRange[2].Y), ptImageRange[3].Y) - ptImageTopLeft.Y;

            double[,] dbData = new double[nSize[0], nSize[1]];
            Point2D ptLeftTop = new Point2D();
            ptLeftTop.X = ptImageTopLeft.X;
            ptLeftTop.Y = ptImageTopLeft.Y;
            //double[,] dbData = new double[pProps.Width,pProps.Height];
            //Point2D ptLeftTop = new Point2D();
            //ptImageTopLeft.X = pRaster2.ToPixelColumn(pEnvelop.UpperLeft.X); //(int)pEnvelop.UpperLeft.X;
            //ptImageTopLeft.Y = pRaster2.ToPixelRow(pEnvelop.UpperLeft.Y); //(int)pEnvelop.UpperLeft.X;//(int)pEnvelop.UpperLeft.Y;
            //ptLeftTop.X = ptImageTopLeft.X;// pEnvelop.UpperLeft.X;
            //ptLeftTop.Y = ptImageTopLeft.Y;// pEnvelop.UpperLeft.Y;
            readBlockDataToFile(ptLeftTop, ref dbData, pSrcRaster);

            return dbData;
        }

        static public double getNoDataValue(object pObject)
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

        private object getValidType(IRasterProps pProps,double dbValue)
        {
            object oValue=null;
            if (pProps.PixelType == rstPixelType.PT_DOUBLE)
            {
                double cValue = Convert.ToDouble(dbValue);
                oValue = cValue as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_FLOAT)
            {
                float cValue = Convert.ToSingle(dbValue);
                oValue = cValue as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_LONG)
            {
                long cValue = Convert.ToInt64(dbValue);
                oValue = cValue as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_SHORT)
            {
                short cValue = Convert.ToInt16(dbValue);
                oValue = cValue as object;
            }
            else if (pProps.PixelType == rstPixelType.PT_UCHAR)
            {
                byte cValue = Convert.ToByte(dbValue);
                oValue = cValue as object;
            }
            else
                oValue = null;

            return oValue;
        }

        //计算某个像素点是否可见，只适用于东北天坐标，即Z轴向上
        private bool getPixelIsCoverd(IRaster pRaster,double[,] dbSubDemData, Pt2i ptSubImgLeftTop,
            int posx, int posy,out Point2D ptGeoCovered,ExOriPara exOriPara, double[] dbCoef, double dbFocus, double fx, double fy,double dbInitialStep)
        {
            ptGeoCovered = null;
            if (pRaster == null)
                return false;

            bool bFlag = false;
            
            IRasterProps pProps = pRaster as IRasterProps;
            int nDemHeight = pProps.Height;
            int nDemWidth = pProps.Width;
            
            double dbNoDataValue = getNoDataValue(pProps.NoDataValue);// Convert.ToDouble(((object[])pProps.NoDataValue)[0]);

            double xPix, yPix;
            xPix = posx - fx;// nImgWidth / 2;       //行列号转摄影测量像平面坐标
            yPix = fy - posy;//nImgHeight / 2

            double dVecX = dbCoef[0] * xPix + dbCoef[1] * yPix - dbCoef[2] * dbFocus;
            double dVecY = dbCoef[3] * xPix + dbCoef[4] * yPix - dbCoef[5] * dbFocus;
            double dVecZ = dbCoef[6] * xPix + dbCoef[7] * yPix - dbCoef[8] * dbFocus;
            double dVecDis = Math.Sqrt(dVecX * dVecX + dVecY * dVecY + dVecZ * dVecZ);
            double dRatioX = dVecX / dVecDis;
            double dRatioY = dVecY / dVecDis;
            double dRatioZ = dVecZ / dVecDis;

            //迭代步长
            double dDemZ = 0.0;
            double dXCumulate = exOriPara.pos.X;
            double dYCumulate = exOriPara.pos.Y;
            double dZCumulate = exOriPara.pos.Z;
            double dStep = dbInitialStep;

            int nDemW = 0;
            int nDemH = 0;

            #region
            int nIter = 0;
            while (true)
            {
                nIter++;

                #region 如果迭代次数太多，则取最后一次的结果，避免无限循环
                if (nIter > 10000)
                {
                    if (true == GetGeoZ(pRaster, dbSubDemData, ptSubImgLeftTop, dXCumulate, dYCumulate, ref dDemZ))
                    {
                        ptGeoCovered = new Point2D();
                        ptGeoCovered.X = dXCumulate;
                        ptGeoCovered.Y = dYCumulate;
                        bFlag = true;
                    }

                    break;
                }
                #endregion

                #region 由于存在“回头探测”的现象，因此要判断是否已经过头，此判断仅适用于Z值朝上（东北天坐标）的DEM
                if ((dXCumulate - exOriPara.pos.X) / dRatioX < 0)
                {
                    break;
                }
                #endregion

                //正常迭代
                dXCumulate = dXCumulate + dStep * dRatioX;
                dYCumulate = dYCumulate + dStep * dRatioY;
                dZCumulate = dZCumulate + dStep * dRatioZ;

                IRaster2 pRaster2 = pRaster as IRaster2;
                nDemW = pRaster2.ToPixelColumn(dXCumulate);//dXCumulate - pDem.m_PtOrigin.X) / dXRdem;
                nDemH = pRaster2.ToPixelRow(dYCumulate); // (dYCumulate - pDem.m_PtOrigin.Y) / dYRdem;
                if (nDemH < 0 || nDemH >= nDemHeight || nDemW < 0 || nDemW >= nDemWidth)
                {
                    break;
                }

                if (false == GetGeoZ(pRaster, dbSubDemData, ptSubImgLeftTop, dXCumulate, dYCumulate, ref dDemZ))
                {
                    continue;
                }

                if (Math.Abs(dDemZ - dbNoDataValue) <= 10e-10)
                {
                    //“回头探测”时遇到无效值，认为可以终止
                    if (dStep < 0)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                //if (Math.Abs(dDemZ - dbNoDataValue) < 1)
                //{
                //    continue;
                //}
                
                //double dZMinus = dDemZ - dZCumulate;
                double dZMinus = dZCumulate - dDemZ;
                
                //表示已经到了地形点上
                if (Math.Abs(dZMinus) < 0.01)
                {
                    if (true == GetGeoZ(pRaster, dbSubDemData, ptSubImgLeftTop, dXCumulate, dYCumulate, ref dDemZ))
                    {
                        ptGeoCovered = new Point2D();
                        ptGeoCovered.X = dXCumulate;
                        ptGeoCovered.Y = dYCumulate;
                        bFlag = true;                        
                    }

                    break;
                }

                //表示已经穿过DEM，需要进行“回头探测”
                if (dZMinus < 0)
                {
                    dStep = -Math.Abs(0.1 * dZMinus);
                }
                else
                {
                    dStep = Math.Abs(dStep);
                }

                //if (Math.Abs(dZMinus) < 0.5)
                //{
                //    dStep = 0.3;
                //}

                //if (Math.Abs(dZMinus) < 0.2)
                //{
                //    dStep = 0.1;
                //}
                //if (Math.Abs(dZMinus) < 0.1)
                //{
                //    dStep = 0.03;
                //}

                //if (Math.Abs(dZMinus) < 0.05)
                //{
                //    if (false == GetGeoZ(pRaster, dbSubDemData, ptSubImgLeftTop, dXCumulate, dYCumulate, ref dDemZ))
                //    {
                //        break;
                //    }

                //    ptGeoCovered = new Point2D();
                //    ptGeoCovered.X = dXCumulate;
                //    ptGeoCovered.Y = dYCumulate;
                //    bFlag = true;

                //    break;
                //}
            }
            #endregion

            return bFlag;
        }

        public bool ImageReprojectionRange(IRaster pSrcRaster, out Point2D[,] ptResult, ExOriPara exori, InOriPara pInOri,int nGeoRange)
        {
            double dbFocus = pInOri.df;
            double fx = pInOri.dfx;
            double fy = pInOri.dfy;
            int nImgWidth = pInOri.nW;
            int nImgHeight = pInOri.nH;

            ptResult = null;

            Pt2i ptSubImgLeftTop = new Pt2i();
            double[,] dbSubDemData = getSubDem(pSrcRaster, exori.pos, nGeoRange,ref ptSubImgLeftTop);
            if (dbSubDemData == null)
                return false;

            IRasterProps pProps = pSrcRaster as IRasterProps;
            int nDemHeight = pProps.Height;
            int nDemWidth = pProps.Width;

            IRaster2 pRaster2=pSrcRaster as IRaster2;
            
            double dbNoDataValue=getNoDataValue(pProps.NoDataValue);
            //object pNodata = pProps.NoDataValue;
            //double value=((double[])pNodata)[0];
            //double dbNoDataValue =double.Parse((float[]pNodata)[0].ToString());
            //double dbNoDataValue = Convert.ToDouble(((double[]))[0]);
            //object dbNoDataValue = Convert.ToDouble(pProps.NoDataValue.ToString());
            //object nodatavalue = pProps.NoDataValue;
            //Type type=pProps.NoDataValue.GetType();
            //double dbNoDataValue = type.IsArray ? nodatavalue[0] as double : nodatavalue as double;
            
            //初始步长
            double dbInitialStep = 0;
            int nGridW = nDemWidth / 10;
            int nGridH = nDemHeight / 10;
            int nCount = 0;
            double dbHeightAvg=0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    double dDemZ = double.NaN;
                    double dbGeoX=pRaster2.ToMapX(i*nGridW);
                    double dbGeoY=pRaster2.ToMapY(j*nGridH);
                    if (GetGeoZ(pSrcRaster, dbSubDemData, ptSubImgLeftTop, dbGeoX, dbGeoY, ref dDemZ) && Math.Abs(dDemZ - dbNoDataValue) >= 10e-10)
                    {
                        nCount++;
                        dbHeightAvg += dDemZ;
                    }
                }
            }
            if (nCount != 0)
                dbHeightAvg /= nCount;

            double dbCurCameraZ = 0;
            //IRaster2 pRaster2 = pSrcRaster as IRaster2;
            //int nCameraImgX = pRaster2.ToPixelColumn(exori.pos.X);//dXCumulate - pDem.m_PtOrigin.X) / dXRdem;
            //int nCameraImgY = pRaster2.ToPixelRow(exori.pos.Y); // (dYCumulate - pDem.m_PtOrigin.Y) / dYRdem;

            //if (nCameraImgY >= 0 || nCameraImgY < nDemHeight || nCameraImgX >= 0 || nCameraImgX < nDemWidth)
            //{
            if (GetGeoZ(pSrcRaster, dbSubDemData, ptSubImgLeftTop, exori.pos.X, exori.pos.Y, ref dbCurCameraZ) && Math.Abs(dbCurCameraZ - dbNoDataValue) >= 10e-10)
            {
                dbHeightAvg = dbCurCameraZ;
            }
            //}
            dbInitialStep=Math.Abs(dbHeightAvg-exori.pos.Z)/2;

            
            Point2D ptLeftTop=new Point2D(0,0);

            double[] dbResolution=new double[2];
            dbResolution[0]=pProps.MeanCellSize().X;
            dbResolution[1]=pProps.MeanCellSize().Y;

            int[] nSize=new int[2];
            nSize[0]=nDemWidth;
            nSize[1] = nDemHeight;

            //pDstRaster = CreateRaster(ptLeftTop, dbResolution, nSize);
            //pDstRaster = createRasterWithoutData(ptLeftTop, dbResolution[0], nSize, "testOutput.tif");

           // byte[,] dbData = new byte[nDemWidth, nDemHeight];

            //double demGrayVal=double.NaN;
            //Pt3d demXYZ;
            //byte byteGray;
            //byte bt = (byte)255;

            //Pt2d pt;
            Matrix matA, matB;
            matA = new Matrix(2, 2);
            matB = new Matrix(2, 1);
            Matrix matR;
            matR = new Matrix(3, 3);

            //Matrix matX;

            double[] a = new double[4];
            double[] b = new double[2];

            OPK2RMat(exori.ori, ref matR);    //求解相关系数

            double a1 = matR.getNum(0, 0);
            double a2 = matR.getNum(0, 1);
            double a3 = matR.getNum(0, 2);

            double b1 = matR.getNum(1, 0);
            double b2 = matR.getNum(1, 1);
            double b3 = matR.getNum(1, 2);

            double c1 = matR.getNum(2, 0);
            double c2 = matR.getNum(2, 1);
            double c3 = matR.getNum(2, 2);
            double[] dbCoef = new double[] { a1, a2, a3, b1, b2, b3, c1, c2, c3 };

            //double X, Y, Z, Z1;
           // Z = 0.0;

            //double dthreshold = 0.01;


            //for (int i = 0; i < nDemWidth; i++)
            //{
            //    for (int j = 0; j < nDemHeight; j++)
            //    {
            //        dbData[i, j] = bt;
            //    }
            //}


            ptResult = new Point2D[2, nImgWidth];

            int nSearchStep = 50;
            for (int j = 0; j < nImgWidth + nSearchStep - 1; j += nSearchStep)
            {
                if (j >= nImgWidth)
                    j = nImgWidth - 1;

                //最下面的点
                Point2D ptBottom=null,ptTop = null;
                Point2D ptLastTime = null;
                if (getPixelIsCoverd(pSrcRaster, dbSubDemData, ptSubImgLeftTop, j, nImgHeight - 1, out ptBottom, exori, dbCoef, dbFocus, fx, fy,dbInitialStep))
                {                      
                    int nposy = 0;
                    int nStep = nImgHeight-1;
                    while(nStep>1 && nposy>=0 && nposy<nImgHeight)
                    {
                        nStep /= 2;

                        if (getPixelIsCoverd(pSrcRaster, dbSubDemData, ptSubImgLeftTop, j, nposy, out ptTop, exori, dbCoef, dbFocus, fx, fy,dbInitialStep))
                        {
                            if (ptLastTime == null)
                                ptLastTime = new Point2D();

                            ptLastTime.X = ptTop.X;
                            ptLastTime.Y = ptTop.Y;
                            nposy -= nStep;
                        }
                        else
                        {
                            nposy += nStep;
                        }                       
                    }
                }

                //加入链表
                ptResult[0, j] = ptLastTime;
                ptResult[1, j] = ptBottom;
            }

            //write block data
            //if (!WriteToRaster(pDstRaster, dbData, ptLeftTop))
            //    return false;
            //if (!writeBlockDataToFile(ptLeftTop, dbData, nSize, pDstRaster))
            //    return false;

            return true;
        }

        //逐点算法
        public bool ImageReprojectionRange(IRaster pSrcRaster, out IRaster pDstRaster, ExOriPara exori,
            double dbFocus, double fx, double fy, int nImgWidth, int nImgHeight, int nGeoRange)
        {
            pDstRaster = null;

            Pt2i ptSubImgLeftTop = new Pt2i();
            double[,] dbSubDemData = getSubDem(pSrcRaster, exori.pos, nGeoRange, ref ptSubImgLeftTop);
            if (dbSubDemData == null)
                return false;

            IRasterProps pProps = pSrcRaster as IRasterProps;
            int nDemHeight = pProps.Height;
            int nDemWidth = pProps.Width;

            IRaster2 pRaster2 = pSrcRaster as IRaster2;

            double dbNoDataValue = getNoDataValue(pProps.NoDataValue);
            //object pNodata = pProps.NoDataValue;
            //double value=((double[])pNodata)[0];
            //double dbNoDataValue =double.Parse((float[]pNodata)[0].ToString());
            //double dbNoDataValue = Convert.ToDouble(((double[]))[0]);
            //object dbNoDataValue = Convert.ToDouble(pProps.NoDataValue.ToString());
            //object nodatavalue = pProps.NoDataValue;
            //Type type=pProps.NoDataValue.GetType();
            //double dbNoDataValue = type.IsArray ? nodatavalue[0] as double : nodatavalue as double;

            //初始步长
            double dbInitialStep = 0;
            int nGridW = nDemWidth / 10;
            int nGridH = nDemHeight / 10;
            int nCount = 0;
            double dbHeightAvg = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    double dDemZ = double.NaN;
                    double dbGeoX = pRaster2.ToMapX(i * nGridW);
                    double dbGeoY = pRaster2.ToMapY(j * nGridH);
                    if (GetGeoZ(pSrcRaster, dbSubDemData, ptSubImgLeftTop, dbGeoX, dbGeoY, ref dDemZ) && Math.Abs(dDemZ - dbNoDataValue) >= 10e-10)
                    {
                        nCount++;
                        dbHeightAvg += dDemZ;
                    }
                }
            }
            if (nCount != 0)
                dbHeightAvg /= nCount;
            else
                return false;

            double dbCurCameraZ = 0;
            //IRaster2 pRaster2 = pSrcRaster as IRaster2;
            //int nCameraImgX = pRaster2.ToPixelColumn(exori.pos.X);//dXCumulate - pDem.m_PtOrigin.X) / dXRdem;
            //int nCameraImgY = pRaster2.ToPixelRow(exori.pos.Y); // (dYCumulate - pDem.m_PtOrigin.Y) / dYRdem;

            //if (nCameraImgY >= 0 || nCameraImgY < nDemHeight || nCameraImgX >= 0 || nCameraImgX < nDemWidth)
            //{
            if (GetGeoZ(pSrcRaster, dbSubDemData, ptSubImgLeftTop, exori.pos.X, exori.pos.Y, ref dbCurCameraZ) && Math.Abs(dbCurCameraZ - dbNoDataValue) >= 10e-10)
            {
                dbHeightAvg = dbCurCameraZ;
            }
            //}
            dbInitialStep = Math.Abs(dbHeightAvg - exori.pos.Z) / 2;


            Point2D ptLeftTop = new Point2D(0, 0);

            double[] dbResolution = new double[2];
            dbResolution[0] = pProps.MeanCellSize().X;
            dbResolution[1] = pProps.MeanCellSize().Y;

            int[] nSize = new int[2];
            nSize[0] = nDemWidth;
            nSize[1] = nDemHeight;

            //pDstRaster = CreateRaster(ptLeftTop, dbResolution, nSize);
            //pDstRaster = createRasterWithoutData(ptLeftTop, dbResolution[0], nSize, "testOutput.tif");

            // byte[,] dbData = new byte[nDemWidth, nDemHeight];

            //double demGrayVal=double.NaN;
            //Pt3d demXYZ;
            //byte byteGray;
            //byte bt = (byte)255;

            //Pt2d pt;
            Matrix matA, matB;
            matA = new Matrix(2, 2);
            matB = new Matrix(2, 1);
            Matrix matR;
            matR = new Matrix(3, 3);

            //Matrix matX;

            double[] a = new double[4];
            double[] b = new double[2];

            OPK2RMat(exori.ori, ref matR);    //求解相关系数

            double a1 = matR.getNum(0, 0);
            double a2 = matR.getNum(0, 1);
            double a3 = matR.getNum(0, 2);

            double b1 = matR.getNum(1, 0);
            double b2 = matR.getNum(1, 1);
            double b3 = matR.getNum(1, 2);

            double c1 = matR.getNum(2, 0);
            double c2 = matR.getNum(2, 1);
            double c3 = matR.getNum(2, 2);
            double[] dbCoef = new double[] { a1, a2, a3, b1, b2, b3, c1, c2, c3 };

            //double X, Y, Z, Z1;
            // Z = 0.0;

            //double dthreshold = 0.01;

            //初始化数组
            //int[,] dbData = new int[nDemWidth, nDemHeight];
            //for (int i = 0; i < nDemWidth; i++)
            //{
            //    for (int j = 0; j < nDemHeight; j++)
            //    {
            //        dbData[i, j] = 0;
            //    }
            //}



            try
            {
                //生成RASTER
                Point2D pt2dTopLeft = new Point2D();
                pt2dTopLeft.X = pProps.Extent.UpperLeft.X;
                pt2dTopLeft.Y = pProps.Extent.UpperLeft.Y;
                pDstRaster = CreateRaster(pt2dTopLeft, dbResolution, nSize);
                if (pDstRaster == null)
                    return false;

                //得到数组
                IPnt pBlockSize = new DblPntClass();
                pBlockSize.X = nDemWidth;
                pBlockSize.Y = nDemHeight;

                IPnt pntLeftTop = new DblPntClass();
                pntLeftTop.SetCoords(ptLeftTop.X, ptLeftTop.Y);

                IPixelBlock pPixelBlock = pDstRaster.CreatePixelBlock(pBlockSize);
                IPixelBlock3 pPixelBlock3 = pPixelBlock as IPixelBlock3;
                pDstRaster.Read(pntLeftTop, pPixelBlock);

                System.Array pixels;
                pixels = (System.Array)pPixelBlock3.get_PixelData(0);

                //初始化
                object oValue = getValidType(pDstRaster as IRasterProps, 0);
                for (int i = 0; i < nDemHeight; i++)
                {
                    for (int j = 0; j < nDemWidth; j++)
                    {
                        pixels.SetValue(oValue, j, i);
                    }
                }

                //逐像素判断是否可见
                IRaster2 pSrcRaster2 = pSrcRaster as IRaster2;
                oValue = getValidType(pDstRaster as IRasterProps, 255);
                for (int i = 0; i < nImgHeight; i++)
                {
                    for (int j = 0; j < nImgWidth; j++)
                    {
                        Point2D ptCurrent = null;
                        if (getPixelIsCoverd(pSrcRaster, dbSubDemData, ptSubImgLeftTop, j, i, out ptCurrent, exori, dbCoef, dbFocus, fx, fy, dbInitialStep))
                        {
                            int nCol = int.MaxValue, nRow = int.MaxValue;
                            pSrcRaster2.MapToPixel(ptCurrent.X, ptCurrent.Y, out nCol, out nRow);
                            if (nCol >= nDemWidth || nCol < 0 || nRow < 0 || nRow >= nDemHeight)
                                continue;

                            pixels.SetValue(oValue, nCol, nRow);
                            //pixels[nCol, nRow] = 255;
                        }
                    }
                }
                pPixelBlock3.set_PixelData(0, (System.Array)pixels);

                //修改数据
                IRasterEdit pRasterEdit = pDstRaster as IRasterEdit;
                pRasterEdit.Write(pntLeftTop, pPixelBlock);
                pRasterEdit.Refresh();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
    }
}
