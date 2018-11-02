#define TEST_TEXTURE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Random;

namespace LibModelGen
{
    //声明公有委托，用于通知外部内部处理进度
    public delegate void InformHandler(int nValue);

    public class TextureGen
    {
        private static Bitmap blend(Bitmap a, Bitmap b)
        {
            Graphics g4 = Graphics.FromImage(a);
            g4.DrawImage(b, new System.Drawing.Point(0, 0));
            return a;
        }

        private static bool writeGeoInfo(IRaster pRaster, String szFilename)
        {
            String szPath = System.IO.Path.GetDirectoryName(szFilename);
            String szOutputFilename = szPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(szFilename) + ".tfw";

            IRaster2 pRaster2 = (IRaster2)pRaster;
            IRasterProps rasterProperties = (IRasterProps)pRaster2;

            //X方向分辨率
            double dbResolutionX = rasterProperties.MeanCellSize().X;
            //Y方向分辨率
            double dbResolutionY = -1 * rasterProperties.MeanCellSize().Y;
            //平移量
            double dbB = 0, dbD = 0;
            //左上角 像素中心地理坐标
            Point2D ptLeftTop = new Point2D();
            ptLeftTop.X = rasterProperties.Extent.XMin + dbResolutionX / 2;
            ptLeftTop.Y = rasterProperties.Extent.YMax + dbResolutionY / 2;

            //写文件
            try
            {
                StreamWriter sw = new StreamWriter(szOutputFilename);
                sw.WriteLine("{0:f8}", dbResolutionX);
                sw.WriteLine("{0:f8}", dbD);
                sw.WriteLine("{0:f8}", dbB);
                sw.WriteLine("{0:f8}", dbResolutionY);
                sw.WriteLine("{0:f8}", ptLeftTop.X);
                sw.WriteLine("{0:f8}", ptLeftTop.Y);
                sw.Close();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        private static bool substract(ref double[,] dbData, int nSize)
        {
            if (dbData == null)
            {
                return false;
            }

            int nWidth = dbData.GetLength(0);
            int nHeight = dbData.GetLength(1);
            double[,] dbTmpData = (double[,])dbData.Clone();

            //nSize = (nSize % 2 == 0) ? nSize + 1 : nSize;
            int nHalfSize = nSize / 2;
            for (int i = nHalfSize; i < nWidth - nHalfSize; i++)
            {
                for (int j = nHalfSize; j < nHeight - nHalfSize; j++)
                {
                    double dbTempResult = 0;
                    double dbCurrentValue = dbData[i, j];

                    for (int m = -nHalfSize; m < nHalfSize; m++)
                    {
                        for (int n = -nHalfSize; n < nHalfSize; n++)
                        {
                            dbTempResult += Math.Abs(dbCurrentValue - dbData[i + m, j + n]);
                        }
                    }

                    dbTmpData[i, j] = dbTempResult;
                }
            }

            dbData = dbTmpData;

            return true;
        }

        //private static bool substract(ref Bitmap dbData, int nSize)
        //{
        //    if (dbData == null)
        //    {
        //        return false;
        //    }

        //    int nWidth = dbData.Width;
        //    int nHeight = dbData.Height;

        //    Bitmap dbTmpData = (Bitmap)dbData.Clone();

        //    //nSize = (nSize % 2 == 0) ? nSize + 1 : nSize;
        //    int nHalfSize = nSize / 2;
        //    for (int i = nHalfSize; i < nWidth - nHalfSize; i++)
        //    {
        //        for (int j = nHalfSize; j < nHeight - nHalfSize; j++)
        //        {
        //            double dbTempResult = 0;

        //            Color color = dbData.GetPixel(i, j);
        //            double dbCurrentValue = (color.R + color.G + color.B) / 3;// dbData[i, j];

        //            for (int m = -nHalfSize; m < nHalfSize; m++)
        //            {
        //                for (int n = -nHalfSize; n < nHalfSize; n++)
        //                {
        //                    Color colorTmp = dbData.GetPixel(i + m, j + n);
        //                    double dbTmpCurrentValue = (colorTmp.R + colorTmp.G + colorTmp.B) / 3;// dbData[i, j];
        //                    dbTempResult += (dbCurrentValue - dbTmpCurrentValue);
        //                }
        //            }

        //            dbTempResult= (-0)/(255f)
        //            dbTmpData.SetPixel(i,j, Color.FromArgb(dbT));
        //        }
        //    }

        //    dbData = dbTmpData;

        //    return true;
        //}

        public static bool textureGen(IRaster pRaster, String[] szTexutreFilenames, int[] nValues, String szOutputFilename)
        {
            #region 检查输入参数是否正确
            if (String.IsNullOrEmpty(szOutputFilename))
            {
                return false;
            }

            if (szTexutreFilenames.Length != 3)
            {
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                if (String.IsNullOrEmpty(szTexutreFilenames[0]))
                {
                    return false;
                }
            }

            if (nValues.Length != 3)
            {
                return false;
            }

            if (pRaster == null)
            {
                return false;
            }
            #endregion

            double[,] dbData = TextureGen.raster2double(pRaster);
            int nWidth = dbData.GetLength(0);
            int nHeight = dbData.GetLength(1);

            Bitmap texture = new Bitmap(szTexutreFilenames[0]);
            Bitmap firstTexture = new Bitmap(texture, nWidth, nHeight);  //重采样成131*131
            Bitmap secondTexture = new Bitmap(szTexutreFilenames[1]);  //保留原尺寸
            texture = new Bitmap(szTexutreFilenames[2]);
            Bitmap thirdTexture = new Bitmap(texture, nWidth, nHeight);    //重采样成307*307
            //Bitmap firstTexture = new Bitmap(szTexutreFilenames[0]);
            //Bitmap secondTexture = new Bitmap(szTexutreFilenames[1]);
            //Bitmap thirdTexture = new Bitmap(szTexutreFilenames[2]);

            Bitmap firstLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap secondLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap thirdLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap finalTexture = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //first step:平铺
            Graphics firstG = Graphics.FromImage(firstLevel);
            Graphics secondG = Graphics.FromImage(secondLevel);
            Graphics thirdG = Graphics.FromImage(thirdLevel);

            int nSubWidth = firstTexture.Width;
            int nSubHeight = firstTexture.Height;
            int nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            int nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    firstG.DrawImage(firstTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = secondTexture.Width;
            nSubHeight = secondTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    secondG.DrawImage(secondTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = thirdTexture.Width;
            nSubHeight = thirdTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    thirdG.DrawImage(thirdTexture, nSubWidth * i, nSubHeight * j);
                }
            }


#if (TEST_TEXTURE)
            if (!substract(ref dbData, 5))
            {
                return false;
            }
#endif

            #region 拉伸阈值
            double dbMax = double.MinValue, dbMin = double.MaxValue;
            for (int i = 0; i < nHeight; i++)
            {
                for (int j = 0; j < nWidth; j++)
                {
                    double dbAvg = dbData[i, j];
                    if (dbMax < dbAvg)
                    {
                        dbMax = dbAvg;
                    }

                    if (dbMin > dbAvg)
                    {
                        dbMin = dbAvg;
                    }
                }
            }
            #endregion

            //second step:合成
            for (int i = 0; i < nWidth; i++)
            {
                for (int j = 0; j < nHeight; j++)
                {
                    //Color color = heightMap.GetPixel(i, j);
                    double nAvg = (dbData[i, j] - dbMin) / (dbMax - dbMin) * 255;// (color.G + color.R + color.B) / 3;

                    if (nAvg < nValues[0] - 30)
                    {
                        //Color levelColor = secondLevel.GetPixel(i, j);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[0] - 30 && nAvg < nValues[0] + 30)
                    {
                        Color levelColor = secondLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[0] - 30)) * 4.25);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }

                    if (nAvg < nValues[1] - 30)
                    {
                        //Color levelColor = thirdLevel.GetPixel(i, j);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[1] - 30 && nAvg < nValues[1] + 30)
                    {
                        Color levelColor = thirdLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[1] - 30)) * 4.25);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }
                }
            }

            //blend
            finalTexture = blend(blend(firstLevel, secondLevel), thirdLevel);

            //save
            finalTexture.Save(szOutputFilename);
            return writeGeoInfo(pRaster, szOutputFilename);
        }

        public static bool textureGen(double[,] dbData, String[] szTexutreFilenames, int[] nValues, String szOutputFilename)
        {
            #region 检查输入参数是否正确
            if (String.IsNullOrEmpty(szOutputFilename))
            {
                return false;
            }

            if (szTexutreFilenames.Length != 3)
            {
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                if (String.IsNullOrEmpty(szTexutreFilenames[0]))
                {
                    return false;
                }
            }

            if (nValues.Length != 3)
            {
                return false;
            }

            if (dbData == null)
            {
                return false;
            }
            #endregion

            Bitmap firstTexture = new Bitmap(szTexutreFilenames[0]);
            Bitmap secondTexture = new Bitmap(szTexutreFilenames[1]);
            Bitmap thirdTexture = new Bitmap(szTexutreFilenames[2]);

            int nWidth = dbData.GetLength(0);
            int nHeight = dbData.GetLength(1);
            Bitmap firstLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap secondLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap thirdLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap finalTexture = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //first step:平铺
            Graphics firstG = Graphics.FromImage(firstLevel);
            Graphics secondG = Graphics.FromImage(secondLevel);
            Graphics thirdG = Graphics.FromImage(thirdLevel);

            int nSubWidth = firstTexture.Width;
            int nSubHeight = firstTexture.Height;
            int nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            int nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    firstG.DrawImage(firstTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = secondTexture.Width;
            nSubHeight = secondTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    secondG.DrawImage(secondTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = thirdTexture.Width;
            nSubHeight = thirdTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    thirdG.DrawImage(thirdTexture, nSubWidth * i, nSubHeight * j);
                }
            }


#if (TEST_TEXTURE)
            if (!substract(ref dbData, 3))
            {
                return false;
            }
#endif

            #region 拉伸阈值
            double dbMax = double.MinValue, dbMin = double.MaxValue;
            for (int i = 0; i < nHeight; i++)
            {
                for (int j = 0; j < nWidth; j++)
                {
                    double dbAvg = dbData[i, j];
                    if (dbMax < dbAvg)
                    {
                        dbMax = dbAvg;
                    }

                    if (dbMin > dbAvg)
                    {
                        dbMin = dbAvg;
                    }
                }
            }
            #endregion

            //second step:合成
            for (int i = 0; i < nWidth; i++)
            {
                for (int j = 0; j < nHeight; j++)
                {
                    //Color color = heightMap.GetPixel(i, j);
                    double nAvg = (dbData[i, j] - dbMin) / (dbMax - dbMin) * 255;// (color.G + color.R + color.B) / 3;

                    if (nAvg < nValues[0] - 30)
                    {
                        //Color levelColor = secondLevel.GetPixel(i, j);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[0] - 30 && nAvg < nValues[0] + 30)
                    {
                        Color levelColor = secondLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[0] - 30)) * 4.25);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }

                    if (nAvg < nValues[1] - 30)
                    {
                        //Color levelColor = thirdLevel.GetPixel(i, j);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[1] - 30 && nAvg < nValues[1] + 30)
                    {
                        Color levelColor = thirdLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[1] - 30)) * 4.25);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }
                }
            }

            //blend
            finalTexture = blend(blend(firstLevel, secondLevel), thirdLevel);

            //save
            finalTexture.Save(szOutputFilename);

            return true;
        }

        public static Bitmap textureGen(double[,] dbData, String[] szTexutreFilenames, int[] nValues)
        {
            #region 检查输入参数是否正确
            if (szTexutreFilenames.Length != 3)
            {
                return null;
            }

            for (int i = 0; i < 3; i++)
            {
                if (String.IsNullOrEmpty(szTexutreFilenames[0]))
                {
                    return null;
                }
            }

            if (nValues.Length != 3)
            {
                return null;
            }

            if (dbData == null)
            {
                return null;
            }
            #endregion
            Bitmap firstTexture = new Bitmap(szTexutreFilenames[0]);
            Bitmap secondTexture = new Bitmap(szTexutreFilenames[1]);
            Bitmap thirdTexture = new Bitmap(szTexutreFilenames[2]);

            int nWidth = dbData.GetLength(0);
            int nHeight = dbData.GetLength(1);
            Bitmap firstLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap secondLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap thirdLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap finalTexture = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //first step:平铺
            Graphics firstG = Graphics.FromImage(firstLevel);
            Graphics secondG = Graphics.FromImage(secondLevel);
            Graphics thirdG = Graphics.FromImage(thirdLevel);

            int nSubWidth = firstTexture.Width;
            int nSubHeight = firstTexture.Height;
            int nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            int nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    firstG.DrawImage(firstTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = secondTexture.Width;
            nSubHeight = secondTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    secondG.DrawImage(secondTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = thirdTexture.Width;
            nSubHeight = thirdTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    thirdG.DrawImage(thirdTexture, nSubWidth * i, nSubHeight * j);
                }
            }

#if (TEST_TEXTURE)
            if (!substract(ref dbData, 3))
            {
                return null;
            }
#endif

            #region 拉伸阈值
            double dbMax = double.MinValue, dbMin = double.MaxValue;
            for (int i = 0; i < nHeight; i++)
            {
                for (int j = 0; j < nWidth; j++)
                {
                    double dbAvg = dbData[i, j];
                    if (dbMax < dbAvg)
                    {
                        dbMax = dbAvg;
                    }

                    if (dbMin > dbAvg)
                    {
                        dbMin = dbAvg;
                    }
                }
            }
            #endregion

            //second step:合成
            for (int i = 0; i < nWidth; i++)
            {
                for (int j = 0; j < nHeight; j++)
                {
                    //Color color = heightMap.GetPixel(i, j);
                    double nAvg = (dbData[i, j] - dbMin) / (dbMax - dbMin) * 255;// (color.G + color.R + color.B) / 3;

                    if (nAvg < nValues[0] - 30)
                    {
                        //Color levelColor = secondLevel.GetPixel(i, j);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[0] - 30 && nAvg < nValues[0] + 30)
                    {
                        Color levelColor = secondLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[0] - 30)) * 4.25);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }

                    if (nAvg < nValues[1] - 30)
                    {
                        //Color levelColor = thirdLevel.GetPixel(i, j);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[1] - 30 && nAvg < nValues[1] + 30)
                    {
                        Color levelColor = thirdLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[1] - 30)) * 4.25);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }
                }
            }

            //blend
            finalTexture = blend(blend(firstLevel, secondLevel), thirdLevel);
            return finalTexture;
        }

        public static Bitmap textureGen(String szHeightMapFilename, String[] szTexutreFilenames, int[] nValues)
        {
            //检查输入参数是否正确
            if (String.IsNullOrEmpty(szHeightMapFilename))
            {
                return null;
            }

            if (szTexutreFilenames.Length != 3)
            {
                return null;
            }

            for (int i = 0; i < 3; i++)
            {
                if (String.IsNullOrEmpty(szTexutreFilenames[0]))
                {
                    return null;
                }
            }

            if (nValues.Length != 3)
            {
                return null;
            }

            Bitmap heightMap = new Bitmap(szHeightMapFilename);
            Bitmap firstTexture = new Bitmap(szTexutreFilenames[0]);
            Bitmap secondTexture = new Bitmap(szTexutreFilenames[1]);
            Bitmap thirdTexture = new Bitmap(szTexutreFilenames[2]);

            int nWidth = heightMap.Width;
            int nHeight = heightMap.Height;
            Bitmap firstLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap secondLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap thirdLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap finalTexture = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //first step:平铺
            Graphics firstG = Graphics.FromImage(firstLevel);
            Graphics secondG = Graphics.FromImage(secondLevel);
            Graphics thirdG = Graphics.FromImage(thirdLevel);

            int nSubWidth = firstTexture.Width;
            int nSubHeight = firstTexture.Height;
            int nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            int nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    firstG.DrawImage(firstTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = secondTexture.Width;
            nSubHeight = secondTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    secondG.DrawImage(secondTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = thirdTexture.Width;
            nSubHeight = thirdTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    thirdG.DrawImage(thirdTexture, nSubWidth * i, nSubHeight * j);
                }
            }


            //#if (TEST_TEXTURE)
            //            if (!substract(ref heightMap, 3))
            //            {
            //                return null;
            //            }
            //#endif

            #region 拉伸阈值
            double dbMax = double.MinValue, dbMin = double.MaxValue;
            for (int i = 0; i < nHeight; i++)
            {
                for (int j = 0; j < nWidth; j++)
                {
                    Color color = heightMap.GetPixel(j, i);
                    double dbAvg = (color.R + color.G + color.B) / 3;
                    if (dbMax < dbAvg)
                    {
                        dbMax = dbAvg;
                    }

                    if (dbMin > dbAvg)
                    {
                        dbMin = dbAvg;
                    }
                }
            }
            #endregion

            //second step:合成
            for (int i = 0; i < heightMap.Width; i++)
            {
                for (int j = 0; j < heightMap.Height; j++)
                {
                    Color color = heightMap.GetPixel(i, j);
                    int nAvg = (color.G + color.R + color.B) / 3;
                    nAvg = (int)((nAvg - dbMin) / (dbMax - dbMin) * 255);// (color.G + color.R + color.B) / 3;

                    if (nAvg < nValues[0] - 30)
                    {
                        //Color levelColor = secondLevel.GetPixel(i, j);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[0] - 30 && nAvg < nValues[0] + 30)
                    {
                        Color levelColor = secondLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[0] - 30)) * 4.25);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }

                    if (nAvg < nValues[1] - 30)
                    {
                        //Color levelColor = thirdLevel.GetPixel(i, j);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[1] - 30 && nAvg < nValues[1] + 30)
                    {
                        Color levelColor = thirdLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[1] - 30)) * 4.25);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }
                }
            }

            //blend
            finalTexture = blend(blend(firstLevel, secondLevel), thirdLevel);
            return finalTexture;
        }

        public static bool textureGen(String szHeightMapFilename, String[] szTexutreFilenames, int[] nValues, String szOutputFilename)
        {
            //检查输入参数是否正确
            if (String.IsNullOrEmpty(szOutputFilename))
            {
                return false;
            }

            if (String.IsNullOrEmpty(szHeightMapFilename))
            {
                return false;
            }

            if (szTexutreFilenames.Length != 3)
            {
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                if (String.IsNullOrEmpty(szTexutreFilenames[0]))
                {
                    return false;
                }
            }

            if (nValues.Length != 3)
            {
                return false;
            }

            Bitmap heightMap = new Bitmap(szHeightMapFilename);
            Bitmap firstTexture = new Bitmap(szTexutreFilenames[0]);
            Bitmap secondTexture = new Bitmap(szTexutreFilenames[1]);
            Bitmap thirdTexture = new Bitmap(szTexutreFilenames[2]);

            //resize
            int nSize = 128;
            firstTexture = new Bitmap(firstTexture, nSize, nSize);
            secondTexture = new Bitmap(secondTexture, nSize, nSize);
            thirdTexture = new Bitmap(thirdTexture, nSize, nSize);

            int nWidth = heightMap.Width;
            int nHeight = heightMap.Height;
            Bitmap firstLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap secondLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap thirdLevel = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap finalTexture = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            #region first step:平铺
            Graphics firstG = Graphics.FromImage(firstLevel);
            Graphics secondG = Graphics.FromImage(secondLevel);
            Graphics thirdG = Graphics.FromImage(thirdLevel);

            int nSubWidth = firstTexture.Width;
            int nSubHeight = firstTexture.Height;
            int nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            int nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    firstG.DrawImage(firstTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = secondTexture.Width;
            nSubHeight = secondTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    secondG.DrawImage(secondTexture, nSubWidth * i, nSubHeight * j);
                }
            }

            nSubWidth = thirdTexture.Width;
            nSubHeight = thirdTexture.Height;
            nCountX = (int)Math.Ceiling((double)nWidth / nSubWidth);
            nCountY = (int)Math.Ceiling((double)nHeight / nSubHeight);
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    thirdG.DrawImage(thirdTexture, nSubWidth * i, nSubHeight * j);
                }
            }
            #endregion

            //#if (TEST_TEXTURE)
            //            if (!substract(ref heightMap, 3))
            //            {
            //                return false;
            //            }
            //#endif

            #region 拉伸阈值
            double dbMax = double.MinValue, dbMin = double.MaxValue;
            for (int i = 0; i < nHeight; i++)
            {
                for (int j = 0; j < nWidth; j++)
                {
                    Color color = heightMap.GetPixel(j, i);
                    double dbAvg = (color.R + color.G + color.B) / 3;
                    if (dbMax < dbAvg)
                    {
                        dbMax = dbAvg;
                    }

                    if (dbMin > dbAvg)
                    {
                        dbMin = dbAvg;
                    }
                }
            }
            #endregion

            #region second step:合成
            for (int i = 0; i < heightMap.Width; i++)
            {
                for (int j = 0; j < heightMap.Height; j++)
                {
                    Color color = heightMap.GetPixel(i, j);
                    int nAvg = (color.G + color.R + color.B) / 3;
                    nAvg = (int)((nAvg - dbMin) / (dbMax - dbMin) * 255);// (color.G + color.R + color.B) / 3;

                    if (nAvg < nValues[0] - 30)
                    {
                        //Color levelColor = secondLevel.GetPixel(i, j);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[0] - 30 && nAvg < nValues[0] + 30)
                    {
                        Color levelColor = secondLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[0] - 30)) * 4.25);
                        secondLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }

                    if (nAvg < nValues[1] - 30)
                    {
                        //Color levelColor = thirdLevel.GetPixel(i, j);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }

                    if (nAvg >= nValues[1] - 30 && nAvg < nValues[1] + 30)
                    {
                        Color levelColor = thirdLevel.GetPixel(i, j);
                        int nAlpha = (int)((nAvg - (nValues[1] - 30)) * 4.25);
                        thirdLevel.SetPixel(i, j, System.Drawing.Color.FromArgb(nAlpha, levelColor.R, levelColor.G, levelColor.B));
                    }
                }
            }
            #endregion

            //blend
            finalTexture = blend(blend(firstLevel, secondLevel), thirdLevel);

            //save
            finalTexture.Save(szOutputFilename);

            return true;
        }

        public static double[,] raster2double(IRaster pRaster)
        {
            IRaster2 pRaster2 = (IRaster2)pRaster;
            IRasterProps rasterProperties = (IRasterProps)pRaster2;
            int nWidth = rasterProperties.Width;
            int nHeight = rasterProperties.Height;

            //Create a pixel block using the weight and height of the raster dataset. 
            //If the raster dataset is large, a smaller pixel block should be used. 
            //Refer to the topic "How to access pixel data using a raster cursor".
            //IRasterDataset3 pRasterSet = pRaster2.RasterDataset as IRasterDataset3;
            //int nWidth = pRasterSet.BlockWidth;
            //int nHeight = pRasterSet.BlockHeight;

            IPnt blocksize = new PntClass();
            blocksize.SetCoords(nWidth, nHeight);
            //IPixelBlock3 pixelblock = pRaster.CreatePixelBlock(blocksize) as IPixelBlock3;

            double[,] dbData = new double[nWidth, nHeight];
            IRasterCursor rasterCursor = pRaster2.CreateCursorEx(blocksize);//null时为128*128
            IPixelBlock3 pixelBlock3 = null;
            System.Array pixels = null;
            System.Object obj = null;

            do
            {
                pixelBlock3 = rasterCursor.PixelBlock as IPixelBlock3;
                //nWidth = pixelBlock3.Width;
                //nHeight = pixelBlock3.Height;
                pixels = (System.Array)pixelBlock3.get_PixelData(0);
                for (int i = 0; i < nWidth; i++)
                {
                    for (int j = 0; j < nHeight; j++)
                    {
                        obj = pixels.GetValue(i, j);
                        if (obj != null)
                        {
                            dbData[i, j] = Convert.ToDouble(obj);
                        }
                    }
                }

            } while (rasterCursor.Next() == true);

            //IPnt ptLeftTop = new PntClass();
            //ptLeftTop.SetCoords(rasterProperties.Extent.UpperLeft.X, rasterProperties.Extent.UpperLeft.Y);
            //pRaster.Read(ptLeftTop, (IPixelBlock)pixelblock);

            //Populate some pixel values to the pixel block.
            //System.Array pixels = (System.Array)pixelblock.get_PixelData(0);
            //double[,] dbData = new double[nWidth, nHeight];
            //for (int i = 0; i < nWidth; i++)
            //{
            //    for (int j = 0; j < nHeight; j++)
            //    {
            //        double dbTmpValue=Convert.ToDouble(pixels.GetValue(i, j));
            //        if (double.IsNaN(dbTmpValue) )
            //        {
            //            if (i > 0 || j > 0)
            //            {
            //                int ddd = 0;
            //            }
            //            dbData[i, j] = double.NaN;
            //            continue;
            //        }
            //        if (dbTmpValue == Convert.ToDouble(((double[])rasterProperties.NoDataValue)[0]))
            //        {
            //             if(i>0 && j>0)
            //             {
            //                int ddd = 0;
            //             }
            //            dbData[i, j] = double.NaN;
            //            continue;
            //        }
            //        dbData[i, j] = dbTmpValue;
            //    }
            //}

            //TerrainFilter.writeData(dbData, "tmpTrans.txt");

            return dbData;
        }
    }

    public class TerrainGen
    {
        //声明事件，通知外部内部处理进度
        public event InformHandler m_infoProgress;

        private int nSubBlockSize = 1024;

        public int SubBlockSize
        {
            get { return nSubBlockSize; }
            set { nSubBlockSize = value; }
        }

        public static int Chaos_GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        //生成高斯分布随机数
        static private double gaussRandom()
        {
            Random rand = new Random(Chaos_GetRandomSeed());

            // 参考http://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
            double u1, u2, v1 = 0, v2 = 0, s = 0, dbResult = 0;
            while (s > 1 || s == 0)
            {
                u1 = rand.NextDouble();
                u2 = rand.NextDouble();
                v1 = 2 * u1 - 1;
                v2 = 2 * u2 - 1;
                s = v1 * v1 + v2 * v2;
            }

            dbResult = Math.Sqrt(-2 * Math.Log(s) / s) * v1;
            //Console.WriteLine(dbResult);
            return dbResult;
        }

        public bool interpolateLargeTerrain(double[,] dbSrcData, int[] nSizes, IRaster pRaster)
        {
            if (dbSrcData == null || pRaster == null)
            {
                return false;
            }

            int nDstWidth = nSizes[0];
            int nDstHeight = nSizes[1];
            int nSrcWidth = dbSrcData.GetLength(0);
            int nSrcHeight = dbSrcData.GetLength(1);

            int nSubHeight = 1024;
            int nSubCount = (int)Math.Ceiling((double)nDstHeight / nSubHeight);

            for (int i = 0; i < nSubCount; i++)
            {
                int nOffset = i * nSubHeight;
                if (nOffset + nSubHeight > nDstHeight)
                {
                    nSubHeight = nDstHeight - nOffset;
                }

                //内插 
                double[,] dbTmpData = new double[nDstWidth, nSubHeight];
                for (int m = 0; m < nDstWidth; m++)
                {
                    for (int n = 0; n < nSubHeight; n++)
                    {
                        double dbPosX = (double)m / nDstWidth * (nSrcWidth - 1);
                        double dbPosY = (double)(n + nOffset) / nDstHeight * (nSrcHeight - 1);
                        int nPosX = (int)Math.Floor(dbPosX);
                        int nPosY = (int)Math.Floor(dbPosY);
                        double dbPortionX = dbPosX - nPosX;
                        double dbPortionY = dbPosY - nPosY;

                        //f（x,y)=f(0,0)(1-x)(1-y)+f(1,0)x(1-y)+f(0,1)(1-x)y+f(1,1)xy
                        dbTmpData[m, n] = (1 - dbPortionX) * (1 - dbPortionY) * dbSrcData[nPosX, nPosY]
                                                    + dbPortionX * (1 - dbPortionY) * dbSrcData[nPosX + 1, nPosY]
                                                    + (1 - dbPortionX) * dbPortionY * dbSrcData[nPosX, nPosY + 1]
                                                    + dbPortionX * dbPortionY * dbSrcData[nPosX + 1, nPosY + 1];
                    }
                }

                ////terrain filter
                //int nFilterSize = 30;
                //TerrainFilter filter = new TerrainFilter();
                //filter.terrainFilter(ref dbTmpData, nFilterSize);                

                //写回
                Point2D ptLeftTop = new Point2D(0, nOffset);
                int[] nSubSize = new int[2] { nDstWidth, nSubHeight };
                writeBlockDataToFile(ptLeftTop, dbTmpData, nSubSize, pRaster);
                dbTmpData = null;
            }

            return true;
        }

        public bool interpolateLargeTerrain(Point2D[] dbSeeds, double dbResolution, double dbMax, String szFilename)
        {
            //计算地形范围以网格数据
            double dbRangeX = Math.Abs(dbSeeds[0].X - dbSeeds[1].X);
            double dbRangeY = Math.Abs(dbSeeds[0].Y - dbSeeds[1].Y);

            int nSizeX = (int)(dbRangeX / dbResolution);
            int nSizeY = (int)(dbRangeY / dbResolution);

            nSubBlockSize = Math.Min(nSizeX, nSizeY);
            nSubBlockSize = (int)Math.Pow(2, Math.Ceiling(Math.Log(nSubBlockSize, 2)));
            if (nSubBlockSize > 4096)
            {
                nSubBlockSize = 4096;
            }
            nSubBlockSize += 1;

            double dbBaseHeight = 0;
            double[] dbSeedHeights = new double[4] { dbBaseHeight, dbBaseHeight, dbBaseHeight, dbBaseHeight };
            double[,] dbData = generateUnitBlock(nSubBlockSize, dbMax, dbSeedHeights);

            Point2D ptLeftTop = new Point2D(0, 0);
            int[] nSizes = new int[2] { nSizeX, nSizeY };
            IRaster pRaster = createRasterWithoutData(ptLeftTop, dbResolution, nSizes, szFilename);
            if (pRaster == null)
            {
                return false;
            }

            if (!interpolateLargeTerrain(dbData, nSizes, pRaster))
            {
                return false;
            }

            IRasterEdit pRasterEdit = pRaster as IRasterEdit;
            pRasterEdit.Refresh();

            return true;
        }

        public bool generateLargeTerrain(Point2D[] dbSeeds, double dbResolution, double dbMax, String szFilename)
        {
            //计算地形范围以网格数据
            double dbRangeX = Math.Abs(dbSeeds[0].X - dbSeeds[1].X);
            double dbRangeY = Math.Abs(dbSeeds[0].Y - dbSeeds[1].Y);

            int nSizeX = (int)(dbRangeX / dbResolution);
            int nSizeY = (int)(dbRangeY / dbResolution);
            int nMinSize = Math.Min(nSizeX, nSizeY);
            nSubBlockSize = (int)Math.Pow(2, Math.Ceiling(Math.Log(nMinSize, 2)));
            if (nSubBlockSize > 2048)
            {
                nSubBlockSize = 2048;
            }

            int nCountX = (int)Math.Ceiling((double)nSizeX / nSubBlockSize);
            int nCountY = (int)Math.Ceiling((double)nSizeY / nSubBlockSize);

            //随机生成种子点高程
            Random r = new Random();

            double dbBaseHeight = 0;
            double dbFirstSeedHeight = dbBaseHeight;
            double dbSecondSeedHeight = dbBaseHeight;
            double dbThirdSeedHeight = dbBaseHeight;
            double dbFourthSeedHeight = dbBaseHeight;
            double[,] dbSeedHeights = new double[2, 2] { { dbFirstSeedHeight, dbSecondSeedHeight }, { dbThirdSeedHeight, dbFourthSeedHeight } };

            //用于保留边界值
            int nBorderValueSizeX = nCountX * nSubBlockSize + 1;
            int nBorderValueSizeY = nCountY * nSubBlockSize + 1;
            double[,] dbBorderValuesX = new double[nCountY + 1, nBorderValueSizeX];
            double[,] dbBorderValuesY = new double[nCountX + 1, nBorderValueSizeY];

            //生成边界值
            double[] dbTmpSeeds = new double[2];
            double[] dbTmpResult = null;

            #region 产生最外围四个边界的值
            dbTmpSeeds[0] = dbFirstSeedHeight;
            dbTmpSeeds[1] = dbSecondSeedHeight;
            //dbMax /= 10f;
            if (randomCurve(dbTmpSeeds, nBorderValueSizeX, dbMax, out dbTmpResult))
            {
                for (int i = 0; i < nBorderValueSizeX; i++)
                {
                    dbBorderValuesX[0, i] = dbTmpResult[i];
                }
            }

            dbTmpSeeds[0] = dbThirdSeedHeight;
            dbTmpSeeds[1] = dbFourthSeedHeight;
            if (randomCurve(dbTmpSeeds, nBorderValueSizeX, dbMax, out dbTmpResult))
            {
                for (int i = 0; i < nBorderValueSizeX; i++)
                {
                    dbBorderValuesX[nCountY, i] = dbTmpResult[i];
                }
            }

            dbTmpSeeds[0] = dbFirstSeedHeight;
            dbTmpSeeds[1] = dbThirdSeedHeight;
            if (randomCurve(dbTmpSeeds, nBorderValueSizeY, dbMax, out dbTmpResult))
            {
                for (int i = 0; i < nBorderValueSizeY; i++)
                {
                    dbBorderValuesY[0, i] = dbTmpResult[i];
                }
            }

            dbTmpSeeds[0] = dbSecondSeedHeight;
            dbTmpSeeds[1] = dbFourthSeedHeight;
            if (randomCurve(dbTmpSeeds, nBorderValueSizeY, dbMax, out dbTmpResult))
            {
                for (int i = 0; i < nBorderValueSizeY; i++)
                {
                    dbBorderValuesY[nCountX, i] = dbTmpResult[i];
                }
            }
            #endregion

            #region 产生中间子块边界的值
            //X方向边界值
            for (int i = 1; i < nCountY; i++)
            {
                dbTmpSeeds[0] = dbBorderValuesY[0, i * nSubBlockSize];
                dbTmpSeeds[1] = dbBorderValuesY[nCountX, i * nSubBlockSize];
                if (randomCurve(dbTmpSeeds, nBorderValueSizeX, dbMax, out dbTmpResult))
                {
                    for (int j = 0; j < nBorderValueSizeX; j++)
                    {
                        dbBorderValuesX[i, j] = dbTmpResult[j];
                    }
                }
            }

            //校正X方向各节点坐标
            //TerrainFilter borderFilter = new TerrainFilter();
            //borderFilter.terrainFilter(ref dbBorderValuesX, dbBorderValuesX.GetLength(1));
            avgFilter(ref dbBorderValuesX, 3);

            //Y方向边界值
            for (int i = 1; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    dbTmpSeeds[0] = dbBorderValuesX[j, i * nSubBlockSize];
                    dbTmpSeeds[1] = dbBorderValuesX[j + 1, i * nSubBlockSize];
                    if (randomCurve(dbTmpSeeds, nSubBlockSize + 1, dbMax, out dbTmpResult))
                    {
                        for (int m = 0; m < nSubBlockSize; m++)
                        {
                            dbBorderValuesY[i, j * nSubBlockSize + m] = dbTmpResult[m];
                        }
                    }
                }

                //for (int j = 0; j < nBorderValueSizeY; j++)
                //{
                //    dbBorderValuesY[i, j] = dbTmpResult[j];
                //}                
            }
            #endregion

            #region 平滑滤波尺寸自适应
            int nFilterSize = 10;
            int nSize = Math.Max(nSizeX, nSizeY);
            if (nSize > 500 && nSize <= 1000)
            {
                nFilterSize = 20;
            }
            else if (nSize > 1000)
            {
                nFilterSize = 30;
            }
            else
            {
                nFilterSize = 10;
            }
            #endregion

            #region 生成数据文件
            int[] nSizes = new int[2] { nSizeX, nSizeY };
            Point2D ptLeftTop = new Point2D(0, 0);
            IRaster pRaster = createRasterWithoutData(ptLeftTop, dbResolution, nSizes, szFilename);
            if (pRaster == null)
            {
                return false;
            }
            //IRasterEdit rasterEdit = (IRasterEdit)pRaster;
            #endregion

            #region 分块生成数据并平滑滤波
            for (int i = 0; i < nCountX; i++)
            {
                for (int j = 0; j < nCountY; j++)
                {
                    int nOffsetX = i * nSubBlockSize;
                    int nOffsetY = j * nSubBlockSize;

                    int[] nSubSizes = new int[2] { nSubBlockSize + 1, nSubBlockSize + 1 };
                    if (nOffsetX + nSubBlockSize + 1 >= nSizeX)
                    {
                        nSubSizes[0] = nSizeX - nOffsetX;
                    }

                    if (nOffsetY + nSubBlockSize + 1 >= nSizeY)
                    {
                        nSubSizes[1] = nSizeY - nOffsetY;
                    }

                    #region 产生地形数据
                    double[,] dbData = null;

                    //准备边界数据并初始化
                    double[,] dbTmpBorderValues = new double[4, nSubBlockSize + 1];
                    for (int m = 0; m <= nSubBlockSize; m++)
                    {
                        dbTmpBorderValues[0, m] = dbBorderValuesX[j, nOffsetX + m];
                        dbTmpBorderValues[1, m] = dbBorderValuesX[j + 1, nOffsetX + m];
                        dbTmpBorderValues[2, m] = dbBorderValuesY[i, nOffsetY + m];
                        dbTmpBorderValues[3, m] = dbBorderValuesY[i + 1, nOffsetY + m];
                    }

                    //产生数据
                    dbData = generateUnitBlock(nSubBlockSize + 1, dbMax, dbTmpBorderValues);

                    if (dbData == null)
                    {
                        return false;
                    }
                    #endregion

                    #region 平滑滤波
                    TerrainFilter filter = new TerrainFilter();
                    if (!filter.terrainFilter(ref dbData, nFilterSize))
                    {
                        return false;
                    }
                    #endregion

                    #region 将数据写出到文件
                    Point2D ptTmpLeftTop = new Point2D(nOffsetX, nOffsetY);

                    //TerrainFilter filter = new TerrainFilter();
                    //String szTmpFilename = String.Format("C:\\Users\\syw\\Desktop\\testResult{0}_{1}.txt", i, j);
                    //filter.writeData(dbData, szTmpFilename);
                    //dbData = null;
                    //String szTmpFilename = String.Format("C:\\Users\\syw\\Desktop\\testResult{0}_{1}.txt", i, j);
                    //filter.readData(out dbData, szTmpFilename);
                    if (!writeBlockDataToFile(ptTmpLeftTop, dbData, nSubSizes, pRaster))
                    {
                        return false;
                    }

                    //gen.CreateRasterDataset(new Point2D(0, 0), 0.1, nSize, dbData, "C:\\Users\\syw\\Desktop\\SecondTest.tif");
                    //CreateRasterDataset(ptLeftTop, .1, nSubSizes, dbData, szFilename);
                    #endregion

                    #region  显示进度条信息
                    if (m_infoProgress != null)
                    {
                        int nTempValue = (int)((double)(j + i * nCountY) / (nCountX * nCountY) * 80);
                        m_infoProgress(nTempValue);
                    }
                    #endregion
                }
            }
            #endregion

            #region 均值滤波
            //nFilterSize = 71;
            int nMultiply = 3;
            int nTmpWidth = nSizeX;
            int nTmpHeight = nMultiply * nFilterSize;
            double[,] dbTmpData = new double[nTmpWidth, nTmpHeight];

            TerrainFilter tmpFilter = new TerrainFilter();
            for (int i = 1; i < nCountY; i++)
            {
                int nOffset = i * nSubBlockSize - nTmpHeight / 2;
                Point2D ptTmpLeftTop = new Point2D(0, nOffset);

                //read
                if (readBlockDataToFile(ptTmpLeftTop, ref dbTmpData, pRaster))
                {
                    //filter
                    if (tmpFilter.terrainFilter(ref dbTmpData, nMultiply * nFilterSize))
                    {
                        //write
                        int[] nTmpSize = new int[2] { nTmpWidth, nTmpHeight };
                        writeBlockDataToFile(ptTmpLeftTop, dbTmpData, nTmpSize, pRaster);
                    }
                }
            }

            #region  显示进度条信息
            if (m_infoProgress != null)
            {
                m_infoProgress(90);
            }
            #endregion

            nTmpWidth = nMultiply * nFilterSize;
            nTmpHeight = nSizeY;
            dbTmpData = null;
            dbTmpData = new double[nTmpWidth, nTmpHeight];
            for (int j = 1; j < nCountX; j++)
            {
                int nOffset = j * nSubBlockSize - nTmpWidth / 2;
                Point2D ptTmpLeftTop = new Point2D(nOffset, 0);

                //read
                if (readBlockDataToFile(ptTmpLeftTop, ref dbTmpData, pRaster))
                {
                    //filter
                    if (tmpFilter.terrainFilter(ref dbTmpData, nMultiply * nFilterSize))
                    {
                        //write
                        int[] nTmpSize = new int[2] { nTmpWidth, nTmpHeight };
                        writeBlockDataToFile(ptTmpLeftTop, dbTmpData, nTmpSize, pRaster);
                    }
                }
            }
            #endregion

            IRasterEdit pRasterEdit = pRaster as IRasterEdit;
            pRasterEdit.Refresh();

            #region  显示进度条信息
            if (m_infoProgress != null)
            {
                m_infoProgress(100);
            }
            #endregion

            //System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterEdit);
            return true;
        }

        public IRaster generateTerrain(Point2D[] dbSeeds, double dbResolution, double dbMax)
        {
            //计算地形范围以网格数据
            double dbRangeX = Math.Abs(dbSeeds[0].X - dbSeeds[1].X);
            double dbRangeY = Math.Abs(dbSeeds[0].Y - dbSeeds[1].Y);

            int nSizeX = (int)(dbRangeX / dbResolution);
            int nSizeY = (int)(dbRangeY / dbResolution);
            int nSize = Math.Max(nSizeX, nSizeY);
            nSize = (int)Math.Pow(2, Math.Ceiling(Math.Log(nSize, 2))) + 1;

            //随机生成种子点高程
            Random r = new Random();

            double dbBaseHeight = 0;
            double[] dbSeedHeights = new double[4] { dbBaseHeight, dbBaseHeight, dbBaseHeight, dbBaseHeight };
            double[,] dbData = generateUnitBlock(nSize, dbMax, dbSeedHeights);

            //median filter
            int nFilterSize = 10;

            if (nSize > 500 && nSize <= 1000)
            {
                nFilterSize = 20;
            }
            else if (nSize > 1000)
            {
                nFilterSize = 30;
            }
            else
            {
                nFilterSize = 10;
            }

            TerrainFilter filter = new TerrainFilter();
            if (!filter.terrainFilter(ref dbData, nFilterSize))
            {
                return null;
            }

            //通知消息
            if (m_infoProgress != null)
            {
                m_infoProgress(85);
            }

            medianFilter(ref dbData, 5);

            //通知消息
            if (m_infoProgress != null)
            {
                m_infoProgress(100);
            }

            //写成TIF文件
            int[] nSizes = new int[2] { nSizeX, nSizeY };
            IRaster raster = CreateRaster(dbSeeds[0], dbResolution, nSizes, dbData);

            return raster;
        }

        public bool generateTerrain(Point2D[] dbSeeds, double dbResolution, double dbMax, String szFilename)
        {
            //计算地形范围以网格数据
            double dbRangeX = Math.Abs(dbSeeds[0].X - dbSeeds[1].X);
            double dbRangeY = Math.Abs(dbSeeds[0].Y - dbSeeds[1].Y);

            int nSizeX = (int)(dbRangeX / dbResolution);
            int nSizeY = (int)(dbRangeY / dbResolution);
            int nSize = Math.Max(nSizeX, nSizeY);
            nSize = (int)Math.Pow(2, Math.Ceiling(Math.Log(nSize, 2))) + 1;

            //随机生成种子点高程
            Random r = new Random();

            double dbBaseHeight = 0;
            //double dbMaxElevationDiff = 5;
            //double dbFirstSeedHeight = (r.NextDouble() - 0.5) * dbMaxElevationDiff + dbBaseHeight;
            //double dbSecondSeedHeight = (r.NextDouble() - 0.5) * dbMaxElevationDiff + dbBaseHeight;
            //double dbThirdSeedHeight = (r.NextDouble() - 0.5) * dbMaxElevationDiff + dbBaseHeight;
            //double dbFourthSeedHeight = (r.NextDouble() - 0.5) * dbMaxElevationDiff + dbBaseHeight;
            double[] dbSeedHeights = new double[4] { dbBaseHeight, dbBaseHeight, dbBaseHeight, dbBaseHeight };
            double[,] dbData = generateUnitBlock(nSize, dbMax, dbSeedHeights);

            //median filter
            int nFilterSize = 10;

            if (nSize > 500 && nSize <= 1000)
            {
                nFilterSize = 20;
            }
            else if (nSize > 1000)
            {
                nFilterSize = 30;
            }
            else
            {
                nFilterSize = 10;
            }

            TerrainFilter filter = new TerrainFilter();
            if (!filter.terrainFilter(ref dbData, nFilterSize))
            {
                return false;
            }

            //通知消息
            if (m_infoProgress != null)
            {
                m_infoProgress(85);
            }

            medianFilter(ref dbData, 5);

            //通知消息
            if (m_infoProgress != null)
            {
                m_infoProgress(100);
            }

            //写成TIF文件
            int[] nSizes = new int[2] { nSizeX, nSizeY };
            CreateRasterDataset(dbSeeds[0], dbResolution, nSizes, dbData, szFilename);

            return true;
        }

        public bool medianFilter(ref double[,] dbData, int nBorderSize)
        {
            if (dbData == null)
            {
                return false;
            }

            int nWidth = dbData.GetLength(0);
            int nHeight = dbData.GetLength(1);

            double[,] newData = new double[nWidth, nHeight];
            newData = (double[,])dbData.Clone();

            int nSize = nBorderSize / 2;
            int nCount = nBorderSize * nBorderSize;

            List<double> list = new List<double>();
            for (int i = nSize; i < nHeight - nSize; i++)
            {
                for (int j = nSize; j < nWidth - nSize; j++)
                {
                    list.Clear();
                    for (int m = -nSize; m <= nSize; m++)
                    {
                        for (int n = -nSize; n <= nSize; n++)
                        {
                            list.Add(dbData[j + n, i + m]);
                        }
                    }

                    list.Sort();
                    newData[j, i] = list[nCount / 2];
                }
            }

            dbData = newData;

            return true;
        }

        private bool avgFilter(ref double[,] dbData, int nBorderSize)
        {
            if (dbData == null)
            {
                return false;
            }

            int nWidth = dbData.GetLength(0);
            int nHeight = dbData.GetLength(1);

            double[,] newData = new double[nWidth, nHeight];
            newData = (double[,])dbData.Clone();

            int nSize = nBorderSize / 2;
            int nCount = nBorderSize * nBorderSize;

            double dbTmp = 0;
            for (int i = nSize; i < nHeight - nSize; i++)
            {
                for (int j = nSize; j < nWidth - nSize; j++)
                {
                    dbTmp = 0;
                    for (int m = -nSize; m <= nSize; m++)
                    {
                        for (int n = -nSize; n <= nSize; n++)
                        {
                            dbTmp += dbData[j + n, i + m];
                        }
                    }

                    newData[j, i] = dbTmp / nCount;
                }
            }

            dbData = newData;

            return true;
        }

        private bool avgFilter(ref double[] dbData, int nBorderSize)
        {
            if (dbData == null)
            {
                return false;
            }

            int nSize = dbData.Length;

            nBorderSize = (nBorderSize % 2 == 0) ? nBorderSize + 1 : nBorderSize;
            if (nBorderSize >= nSize)
            {
                return false;
            }

            double[] dbTmpData = (double[])dbData.Clone();

            double dbAvg = 0;

            //第一个点的计算
            for (int j = 0; j < nBorderSize; j++)
            {
                dbAvg += dbData[j];
            }
            dbAvg /= nBorderSize;
            dbTmpData[nBorderSize / 2] = dbAvg;

            for (int i = nBorderSize / 2 + 1; i < nSize - nBorderSize / 2; i++)
            {
                dbAvg = (dbAvg * nBorderSize - dbData[i - 1 - nBorderSize / 2] + dbData[i + nBorderSize / 2]) / nBorderSize;
                dbTmpData[i] = dbAvg;
            }

            dbData = dbTmpData;
            return true;
        }

        public bool randomCurve(double[] dbSeeds, int nCount, double dbMax, out double[] dbResult)
        {
            dbResult = null;
            if (dbSeeds.Length != 2)
            {
                return false;
            }

            dbResult = new double[nCount];

            int nTempCount = (int)Math.Pow(2, Math.Ceiling(Math.Log(nCount, 2))) + 1;
            double[] dbTmpResult = new double[nTempCount];
            dbTmpResult[0] = dbSeeds[0];
            dbTmpResult[nTempCount - 1] = dbSeeds[1];

            int nStep = nTempCount / 2;
            SystemCryptoRandomNumberGenerator sr = new SystemCryptoRandomNumberGenerator();

            while (nStep >= 1)
            {
                int nCurrent = nStep;
                double dbTmpMax = dbMax * 0.5 * nStep / (nCount - 1);

                while (nCurrent < nTempCount)
                {
                    int nSign = Math.Sign(sr.NextDouble() - 0.5);
                    double dbDisplacement = nSign * dbTmpMax * Math.Sqrt(0.5 * nCount + 0.02 * nCount * sr.NextDouble());

                    dbTmpResult[nCurrent] = (dbTmpResult[nCurrent - nStep] + dbTmpResult[nCurrent + nStep]) / 2 + dbDisplacement;
                    nCurrent = nCurrent + nStep * 2;
                }

                nStep /= 2;
            }

            double dbStep = (double)nTempCount / nCount;
            for (int i = 0; i < nCount; i++)
            {
                dbResult[i] = dbTmpResult[(int)Math.Floor(dbStep * i)];
            }

            int nBorderSize = 11;
            avgFilter(ref dbResult, nBorderSize);

            return true;
        }

        private double[,] generateUnitBlock(int nSize, double dbMax, double[] dbSeeds)
        {
            if (dbSeeds.Length != 4)
                return null;

            double[,] dbResult = new double[nSize, nSize];

            //初始化地形
            for (int i = 0; i < nSize; i++)
            {
                for (int j = 0; j < nSize; j++)
                {
                    dbResult[i, j] = Double.NaN;
                }
            }

            dbResult[0, 0] = dbSeeds[0];//[0, 0];
            dbResult[nSize - 1, 0] = dbSeeds[1];//[1, 0];
            dbResult[0, nSize - 1] = dbSeeds[2];//[0, 1];
            dbResult[nSize - 1, nSize - 1] = dbSeeds[3];//[1, 1];

            //基于Diamond-Square正方形细分算法生成三维地形
            int nStep = nSize - 1;
            double dbGridStep = (nSize - 1);

            while (nStep > 1)
            {
                double dbTmpMax = dbMax * 0.5 * nStep / (nSize - 1);

                //diamond step:
                int nRowValue = nStep / 2;
                while (nRowValue < nSize - 1)
                {
                    int nColValue = nStep / 2;
                    while (nColValue < nSize - 1)
                    {
                        int nSign = Math.Sign(gaussRandom());
                        double dbDisplacement = nSign * dbTmpMax * Math.Sqrt(0.5 * dbGridStep + 0.02 * dbGridStep * gaussRandom());
                        //Random r = new Random(Chaos_GetRandomSeed());
                        //double dbDisplacement = dbTmpMax * Math.Sqrt(0.5 * dbGridStep + 0.02 * dbGridStep * r.NextDouble()) * (r.NextDouble() - 0.5);
                        dbResult[nColValue, nRowValue] =
                            0.25 * (dbResult[nColValue - nStep / 2, nRowValue - nStep / 2]
                                    + dbResult[nColValue - nStep / 2, nRowValue + nStep / 2]
                                    + dbResult[nColValue + nStep / 2, nRowValue - nStep / 2]
                                    + dbResult[nColValue + nStep / 2, nRowValue + nStep / 2]) + dbDisplacement;
                        nColValue += nStep;
                    }
                    nRowValue += nStep;
                }

                //Square step:
                nRowValue = 0;
                while (nRowValue < nSize)
                {
                    int nColValue = 0;
                    while (nColValue < nSize)
                    {
                        if (Double.IsNaN(dbResult[nColValue, nRowValue]))
                        {
                            int nSign = Math.Sign(gaussRandom());
                            double dbDisplacement = nSign * dbTmpMax * Math.Sqrt(0.5 * dbGridStep + 0.02 * dbGridStep * gaussRandom());

                            if (nRowValue == 0)
                            {
                                dbResult[nColValue, nRowValue] = (dbResult[nColValue - nStep / 2, nRowValue]
                                    + dbResult[nColValue + nStep / 2, nRowValue]
                                    + dbResult[nColValue, nRowValue + nStep / 2]
                                    + dbResult[nColValue, nRowValue + nStep / 2]) / 4 + dbDisplacement;
                            }
                            else if (nRowValue == nSize - 1)
                            {
                                dbResult[nColValue, nRowValue] = (dbResult[nColValue - nStep / 2, nRowValue]
                                    + dbResult[nColValue + nStep / 2, nRowValue]
                                    + dbResult[nColValue, nRowValue - nStep / 2]
                                    + dbResult[nColValue, nRowValue - nStep / 2]) / 4 + dbDisplacement;
                            }
                            else if (nColValue == 0)
                            {
                                dbResult[nColValue, nRowValue] = (dbResult[nColValue, nRowValue - nStep / 2]
                                    + dbResult[nColValue, nRowValue + nStep / 2]
                                    + dbResult[nColValue + nStep / 2, nRowValue]
                                    + dbResult[nColValue + nStep / 2, nRowValue]) / 4 + dbDisplacement;
                            }
                            else if (nColValue == nSize - 1)
                            {
                                dbResult[nColValue, nRowValue] = (dbResult[nColValue, nRowValue - nStep / 2]
                                    + dbResult[nColValue, nRowValue + nStep / 2]
                                    + dbResult[nColValue - nStep / 2, nRowValue]
                                    + dbResult[nColValue - nStep / 2, nRowValue]) / 4 + dbDisplacement;
                            }
                            else
                            {
                                dbResult[nColValue, nRowValue] = (dbResult[nColValue, nRowValue - nStep / 2]
                                    + dbResult[nColValue, nRowValue + nStep / 2]
                                    + dbResult[nColValue - nStep / 2, nRowValue]
                                    + dbResult[nColValue + nStep / 2, nRowValue]) / 4 + dbDisplacement;
                            }

                            nColValue += nStep;
                        }
                        else
                        {
                            nColValue += nStep / 2;
                        }
                    }
                    nRowValue += nStep / 2;
                }

                nStep /= 2;

                ////发布消息
                //if (m_infoProgress != null)
                //{
                //    m_infoProgress((int)(((double)(nSize - 1) / nStep) / (nSize - 1) * 70));
                //}
            }

            return dbResult;
        }

        private double[,] generateUnitBlock(int nSize, double dbMax, double[,] dbBoundsValue)
        {
            if (dbBoundsValue.GetLength(0) != 4 || dbBoundsValue.GetLength(1) != nSize)
                return null;

            double[,] dbResult = new double[nSize, nSize];

            //初始化地形
            for (int i = 0; i < nSize; i++)
            {
                for (int j = 0; j < nSize; j++)
                {
                    dbResult[i, j] = Double.NaN;
                }
            }

            dbResult[0, 0] = dbBoundsValue[0, 0];//dbSeeds[0, 0];
            dbResult[nSize - 1, 0] = dbBoundsValue[0, nSize - 1];//dbSeeds[1, 0];
            dbResult[0, nSize - 1] = dbBoundsValue[1, 0];//dbSeeds[0, 1];
            dbResult[nSize - 1, nSize - 1] = dbBoundsValue[1, nSize - 1];// dbSeeds[1, 1];

            //基于Diamond-Square正方形细分算法生成三维地形
            int nStep = nSize - 1;
            double dbGridStep = (nSize - 1);

            while (nStep > 1)
            {
                double dbTmpMax = dbMax * 0.5 * nStep / (nSize - 1);

                //diamond step:
                int nRowValue = nStep / 2;
                while (nRowValue < nSize - 1)
                {
                    int nColValue = nStep / 2;
                    while (nColValue < nSize - 1)
                    {
                        int nSign = Math.Sign(gaussRandom());
                        double dbDisplacement = nSign * dbTmpMax * Math.Sqrt(0.5 * dbGridStep + 0.02 * dbGridStep * gaussRandom());
                        //Random r = new Random(Chaos_GetRandomSeed());
                        //double dbDisplacement = dbTmpMax * Math.Sqrt(0.5 * dbGridStep + 0.02 * dbGridStep * r.NextDouble()) * (r.NextDouble() - 0.5);
                        dbResult[nColValue, nRowValue] =
                            0.25 * (dbResult[nColValue - nStep / 2, nRowValue - nStep / 2]
                                    + dbResult[nColValue - nStep / 2, nRowValue + nStep / 2]
                                    + dbResult[nColValue + nStep / 2, nRowValue - nStep / 2]
                                    + dbResult[nColValue + nStep / 2, nRowValue + nStep / 2]) + dbDisplacement;
                        nColValue += nStep;
                    }
                    nRowValue += nStep;
                }

                //Square step:
                nRowValue = 0;
                while (nRowValue < nSize)
                {
                    int nColValue = 0;
                    while (nColValue < nSize)
                    {
                        if (Double.IsNaN(dbResult[nColValue, nRowValue]))
                        {
                            int nSign = Math.Sign(gaussRandom());
                            double dbDisplacement = nSign * dbTmpMax * Math.Sqrt(0.5 * dbGridStep + 0.02 * dbGridStep * gaussRandom());

                            if (nRowValue == 0)
                            {
                                dbResult[nColValue, nRowValue] = dbBoundsValue[0, nColValue];
                            }
                            else if (nRowValue == nSize - 1)
                            {
                                dbResult[nColValue, nRowValue] = dbBoundsValue[1, nColValue];
                            }
                            else if (nColValue == 0)
                            {
                                dbResult[nColValue, nRowValue] = dbBoundsValue[2, nRowValue];
                            }
                            else if (nColValue == nSize - 1)
                            {

                                dbResult[nColValue, nRowValue] = dbBoundsValue[3, nRowValue];
                            }
                            else
                            {
                                dbResult[nColValue, nRowValue] = (dbResult[nColValue, nRowValue - nStep / 2]
                                    + dbResult[nColValue, nRowValue + nStep / 2]
                                    + dbResult[nColValue - nStep / 2, nRowValue]
                                    + dbResult[nColValue + nStep / 2, nRowValue]) / 4 + dbDisplacement;
                            }

                            nColValue += nStep;
                        }
                        else
                        {
                            nColValue += nStep / 2;
                        }
                    }
                    nRowValue += nStep / 2;
                }

                nStep /= 2;

                ////发布消息
                //if (m_infoProgress != null)
                //{
                //    m_infoProgress((int)(((double)(nSize - 1) / nStep) / (nSize - 1) * 70));
                //}
            }

            return dbResult;
        }

        private IRaster CreateRaster(Point2D ptLeftTop, double dbResolution, int[] nSize, double[,] dbData)
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
                double xCell = dbResolution; //This is the cell size in x direction.
                double yCell = dbResolution; //This is the cell size in y direction.
                int NumBand = 1; // This is the number of bands the raster dataset contains.
                //Create a raster dataset in TIFF format.
                IRasterDataset rasterDataset = rasterWs.CreateRasterDataset("", "MEM",
                    origin, width, height, xCell, yCell, NumBand, rstPixelType.PT_DOUBLE, sr,
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
                IPnt blocksize = new PntClass();
                blocksize.SetCoords(width, height);
                IPixelBlock3 pixelblock = raster.CreatePixelBlock(blocksize) as IPixelBlock3;

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
                IRasterEdit rasterEdit = (IRasterEdit)raster;
                rasterEdit.Write(upperLeft, (IPixelBlock)pixelblock);
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
                    origin, width, height, xCell, yCell, NumBand, rstPixelType.PT_DOUBLE, sr,
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

        private bool writeBlockDataToFile(Point2D ptLeftTop, double[,] dbData, int[] nSize, IRaster pRaster/*, IRasterEdit rasterEdit*/)
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

        private bool readBlockDataToFile(Point2D ptLeftTop, ref double[,] dbData, IRaster pRaster/*, IRasterEdit rasterEdit*/)
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
                        dbData[i, j] = (double)pixels.GetValue(i, j);
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

        public void CreateRasterDataset(Point2D ptLeftTop, double dbResolution, int[] nSize, double[,] dbData, string pRasterFile)
        {
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
                    origin, width, height, xCell, yCell, NumBand, rstPixelType.PT_DOUBLE, sr,
                    true);

                //If you need to set NoData for some of the pixels, you need to set it on band 
                //to get the raster band.
                IRasterBandCollection rasterBands = (IRasterBandCollection)rasterDataset;
                IRasterBand rasterBand;
                IRasterProps rasterProps;
                rasterBand = rasterBands.Item(0);
                rasterProps = (IRasterProps)rasterBand;
                //Set NoData if necessary. For a multiband image, a NoData value needs to be set for each band.
                //rasterProps.NoDataValue = -9999;
                //Create a raster from the dataset.
                IRaster raster = rasterDataset.CreateDefaultRaster();

                //Create a pixel block using the weight and height of the raster dataset. 
                //If the raster dataset is large, a smaller pixel block should be used. 
                //Refer to the topic "How to access pixel data using a raster cursor".
                IPnt blocksize = new PntClass();
                blocksize.SetCoords(width, height);
                IPixelBlock3 pixelblock = raster.CreatePixelBlock(blocksize) as IPixelBlock3;

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
                IRasterEdit rasterEdit = (IRasterEdit)raster;
                rasterEdit.Write(upperLeft, (IPixelBlock)pixelblock);
                rasterEdit.Refresh();

                pixelblock = raster.CreatePixelBlock(blocksize) as IPixelBlock3;
                raster.Read(upperLeft, pixelblock as IPixelBlock);
                pixels = (System.Array)pixelblock.get_PixelData(0);
                //Release rasterEdit explicitly.
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterDataset);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterEdit);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterWs);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(sr);
                //GC.Collect();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private IRasterWorkspace2 OpenRasterWorkspace(string PathName)
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
    }

    public class TerrainFilter
    {
        public bool writeData(double[,] dbData, String szFilename)
        {
            StreamWriter sr = new StreamWriter(szFilename);

            int nRow = dbData.GetLength(0);
            int nCol = dbData.GetLength(1);
            sr.WriteLine("{0} {1} ", nRow, nCol);
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    sr.Write("{0} ", dbData[i, j]);
                }
                sr.WriteLine();
            }

            sr.Close();
            return true;
        }

        public bool readData(out double[,] dbData, String szFilename)
        {
            dbData = null;

            StreamReader sr = new StreamReader(szFilename);
            String[] szSizes = sr.ReadLine().Trim().Split(' ');
            if (szSizes.Length != 2)
            {
                return false;
            }

            int nRow = int.Parse(szSizes[0]);
            int nCol = int.Parse(szSizes[1]);

            dbData = new double[nRow, nCol];
            for (int i = 0; i < nRow; i++)
            {
                String[] szDatas = sr.ReadLine().Trim().Split(' ');
                if (szDatas.Length != nCol)
                {
                    continue;
                }

                for (int j = 0; j < nCol; j++)
                {
                    dbData[i, j] = double.Parse(szDatas[j]);
                }
            }

            sr.Close();
            return true;
        }

        public bool terrainFilter(ref double[,] dbData, int nStep)
        {
            if (dbData == null || nStep < 3)
            {
                return false;
            }

            int nWidth = dbData.GetLength(0);
            int nHeight = dbData.GetLength(1);
            DenseMatrix oriTerrain = new DenseMatrix(dbData);

            int nBlockX = (int)Math.Ceiling((double)nWidth / nStep);
            int nBlockY = (int)Math.Ceiling((double)nHeight / nStep);
            for (int i = 0; i < nBlockX; i++)
            {
                for (int j = 0; j < nBlockY; j++)
                {
                    //对于每个小块，模拟一个二维曲面，并除掉相应的毛刺点
                    int nCountX = nStep, nCountY = nStep;
                    if ((i + 1) * nStep > nWidth)
                    {
                        nCountX = nWidth - i * nStep;
                    }
                    if ((j + 1) * nStep > nHeight)
                    {
                        nCountY = nHeight - j * nStep;
                    }

                    //点数
                    int nCount = nCountX * nCountY;
                    if (nCount < 6)
                    {
                        continue;
                    }

                    DenseMatrix matrixB = new DenseMatrix(nCount, 6);
                    //DenseMatrix matrixP = DenseMatrix.Identity(nCount);
                    DenseMatrix matrixL = new DenseMatrix(nCount, 1);

                    //z=1+ax+by+cxy+dx^2+ey^2
                    for (int m = 0; m < nCountY; m++)
                    {
                        for (int n = 0; n < nCountX; n++)
                        {
                            int X = n + i * nStep;
                            int Y = m + j * nStep;

                            matrixB[m * nCountX + n, 0] = 1;
                            matrixB[m * nCountX + n, 1] = X;
                            matrixB[m * nCountX + n, 2] = Y;
                            matrixB[m * nCountX + n, 3] = X * Y;
                            matrixB[m * nCountX + n, 4] = X * X;
                            matrixB[m * nCountX + n, 5] = Y * Y;

                            matrixL[m * nCountX + n, 0] = oriTerrain[X, Y];
                        }
                    }

                    //solve
                    var BTPB = matrixB.Transpose() * matrixB;  //BTPB
                    var BTPL = matrixB.Transpose() * matrixL;   //BTPL
                    var result = BTPB.Inverse() * BTPL;                           //x=inv(BTPB)*BTPL
                    var matrixV = matrixB * result - matrixL;                     //v=BX-L
                    var VTPV = matrixV.Transpose() * matrixV;        //VTPV
                    double dbStdErr = Math.Sqrt((double)VTPV[0, 0] / (nCount - 6));  //sigma0

                    //filter
                    for (int m = 0; m < nCountY; m++)
                    {
                        for (int n = 0; n < nCountX; n++)
                        {
                            int X = n + i * nStep;
                            int Y = m + j * nStep;

                            if (Math.Abs(matrixV[m * nCountX + n, 0]) > 2 * dbStdErr)
                            {
                                //1+ax+by+cxy+dx^2+ey^2
                                var dbCorrectedValue = matrixB.SubMatrix(m * nCountX + n, 1, 0, 6) * result;
                                dbData[X, Y] = dbCorrectedValue[0, 0];
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
