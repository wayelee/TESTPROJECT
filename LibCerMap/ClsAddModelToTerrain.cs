using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using LibModelGen;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using System.IO;
using ESRI.ArcGIS.Carto;
using DevComponents.DotNetBar.Controls;


namespace LibCerMap
{
    public class Model
    {
        public double x, y;
        public double dbSize;
        public double dbDepth;
        public string szModelType;
        public string szModelID;

        public Model()
        {
            x = double.NaN;
            y = double.NaN;
            dbSize = double.NaN;
            dbDepth = double.NaN;
            szModelType = null;
            szModelID = null;
        }

        public bool compareModelPara(Model second)
        {
            return (dbDepth == second.dbDepth)
                && (dbSize == second.dbSize)
                && (szModelType == second.szModelType);
                //&& (x == second.x)
                //&& (y == second.y);
        }

        public void copyFromModel(Model second)
        {
            x = second.x;
            y = second.y;
            dbSize = second.dbSize;
            dbDepth = second.dbDepth;
            szModelType = second.szModelType;
            szModelID = second.szModelID;
        }
    }

    enum TextureTypeForModel
    {
        ForCrater,
        ForNonCrater
    };

    public class ClsAddModelToTerrain
    {
        public ProgressBarX pProgressbar=null;
        public List<Model> listModelInfo=null;
        
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

        public bool writeModelInfoToXml(string szFilename, List<Model> listModelInfo)
        {
            if (string.IsNullOrEmpty(szFilename))
                return false;

            int nSize = listModelInfo.Count;
            if (nSize <= 0)
                return false;

            XmlDocument doc = new XmlDocument(); // 创建XML对象

            XmlElement root = doc.CreateElement("Models"); // 创建根节点Models
            doc.AppendChild(root);    //  加入到xml document

            XmlElement num = doc.CreateElement("Num"); // 创建节点Num
            XmlText text = doc.CreateTextNode(nSize.ToString());
            num.AppendChild(text);
            root.AppendChild(num);    //  加入到xml document

            //循环添加模型
            for (int i = 0; i < nSize; i++)
            {
                XmlElement tmpModel = doc.CreateElement("Model");

                //添加模型标识
                XmlElement tmpFlag = doc.CreateElement("ModelID");
                XmlText tmpText = doc.CreateTextNode(listModelInfo[i].szModelID);
                tmpFlag.AppendChild(tmpText);
                tmpModel.AppendChild(tmpFlag);

                //添加模型类型
                tmpFlag = doc.CreateElement("ModelType");
                tmpText = doc.CreateTextNode(listModelInfo[i].szModelType);
                tmpFlag.AppendChild(tmpText);
                tmpModel.AppendChild(tmpFlag);

                //添加地理位置信息
                XmlElement tmpCoordinate = doc.CreateElement("Coordinate");
                XmlElement tmpX = doc.CreateElement("GeoX");
                tmpText = doc.CreateTextNode(listModelInfo[i].x.ToString());
                tmpX.AppendChild(tmpText);

                XmlElement tmpY = doc.CreateElement("GeoY");
                tmpText = doc.CreateTextNode(listModelInfo[i].y.ToString());
                tmpY.AppendChild(tmpText);

                tmpCoordinate.AppendChild(tmpX);
                tmpCoordinate.AppendChild(tmpY);
                tmpModel.AppendChild(tmpCoordinate);

                //添加大小信息
                XmlElement tmpSize = doc.CreateElement("Size");
                tmpText = doc.CreateTextNode(listModelInfo[i].dbSize.ToString());
                tmpSize.AppendChild(tmpText);
                tmpModel.AppendChild(tmpSize);

                //添加深度信息
                XmlElement tmpDepth = doc.CreateElement("Depth");
                tmpText = doc.CreateTextNode(listModelInfo[i].dbDepth.ToString());
                tmpDepth.AppendChild(tmpText);
                tmpModel.AppendChild(tmpDepth);

                //往根节点里添加模型信息
                root.AppendChild(tmpModel);
            }

            try
            {
                doc.Save(szFilename);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            
            return true;
        }

        public List<Model> readModelInfoFromXml(string szFilename)
        {
            //文件名空，则直接返回
            if (String.IsNullOrEmpty(szFilename))
                return null;

            List<Model> listResult = new List<Model>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(szFilename);

                //读取模型的个数
                int nModelNum=int.MinValue;
                XmlNode xnModelNum = xmlDoc.SelectSingleNode(@"Models/Num");
                if (xnModelNum != null)
                {
                    nModelNum = int.Parse(xnModelNum.InnerText);
                    if(nModelNum<=0)
                        return null;
                }

                XmlNodeList xnl = xmlDoc.SelectNodes(@"Models/Model");
                foreach (XmlNode xNod in xnl)
                {
                    Model tmpModel = new Model();

                    //模型标识
                    XmlNode xmlnode = xNod.SelectSingleNode(@"ModelID");
                    if (xmlnode != null)
                    {
                        tmpModel.szModelID = xmlnode.InnerText;
                    }

                    //模型类型
                    xmlnode = xNod.SelectSingleNode(@"ModelType");
                    if (xmlnode != null)
                    {
                        tmpModel.szModelType = xmlnode.InnerText;
                    }

                    //地理坐标
                    xmlnode = xNod.SelectSingleNode(@"Coordinate/GeoX");
                    if (xmlnode != null)
                    {
                        tmpModel.x = Double.Parse(xmlnode.InnerText);
                    }

                    xmlnode = xNod.SelectSingleNode(@"Coordinate/GeoY");
                    if (xmlnode != null)
                    {
                        tmpModel.y = Double.Parse(xmlnode.InnerText);
                    }

                    //大小
                    xmlnode = xNod.SelectSingleNode(@"Size");
                    if (xmlnode != null)
                    {
                        tmpModel.dbSize = Double.Parse(xmlnode.InnerText);
                    }

                    //深度
                    xmlnode = xNod.SelectSingleNode(@"Depth");
                    if (xmlnode != null)
                    {
                        tmpModel.dbDepth = Double.Parse(xmlnode.InnerText);
                    }

                    listResult.Add(tmpModel);
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }

            return listResult;
        }

        public bool UpdateMultiPatchFromModel(List<Model> listModelInfo, IRaster pRaster)
        {
            if (listModelInfo.Count <= 0)
                return false;

            //得到当前RASTER的一些属性
            IRaster2 pRaster2 = pRaster as IRaster2;
            IRasterProps pProps = pRaster as IRasterProps;
            int nWidth = pProps.Width;
            int nHeight = pProps.Height;

            //得到RASTER的地理范围
            double xmax = pProps.Extent.XMax;
            double xmin = pProps.Extent.XMin;
            double ymax = pProps.Extent.YMax;
            double ymin = pProps.Extent.YMin;
            double dbGeoRangeX = xmax - xmin;
            double dbGeoRangeY = ymax - ymin;

            //得到MultiPatch并清空
            ClsGDBDataCommon GDC = new ClsGDBDataCommon();
            IWorkspace pWS = GDC.OpenFromFileGDB(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\DefaultFileGDB\MultiPatchFile.gdb");
            IFeatureClass CraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("Crater");
            IFeatureClass NonCraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("NonCrater");
            ITable pTable = CraterFeatureClass as ITable;

            //清空所有对象
            IFeatureCursor pFeatureCursor = CraterFeatureClass.Search(null, false);
            IFeature pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {
                pFeature.Delete();
                pFeature = pFeatureCursor.NextFeature();
            }
            pFeatureCursor = NonCraterFeatureClass.Search(null, false);
            pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {
                pFeature.Delete();
                pFeature = pFeatureCursor.NextFeature();
            }

            //添加模型到MultiPatch
            int nSize = listModelInfo.Count;
            for (int i = 0; i < nSize; i++)
            {
                Model model = listModelInfo[i];

                //得到当前模型指定位置的像素值
                 int nCol, nRow;
                 double dbCurrentPixelValue = double.NaN;
                 pRaster2.MapToPixel(model.x, model.y, out nCol, out nRow);
                 IPoint mapPoint = new PointClass();
                 mapPoint.PutCoords(model.x, model.y);

                 if (nCol >= 0 && nCol < nWidth && nRow >= 0 && nRow < nHeight)
                 {
                     dbCurrentPixelValue = Convert.ToDouble(pRaster2.GetPixelValue(0, nCol, nRow));

                     object pNodata = pProps.NoDataValue;
                     double dbNoData = getNoDataValue(pNodata);

                     //如果为无效值，直接返回
                     if (Math.Abs(dbCurrentPixelValue - dbNoData) <= 1e-7)
                         continue;

                     mapPoint.Z = dbCurrentPixelValue;
                 }
                 else
                     continue;

                //撞击坑
                if (model.szModelType.ToLower().Contains("crater"))
                {
                    //生成撞击坑模型
                    Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                    double dbDiameter = model.dbSize;
                    double dbDepth = model.dbDepth;// dbDiameter * (r.NextDouble() * 0.1 + 0.1); //dbDepth/dbDiameter=[0.05,0.1]
                    TriType triType = TriType.TriForward;
                    LibModelGen.MappingType mappingType = LibModelGen.MappingType.Flat;
                    String szModelOutputFilename = System.IO.Path.GetTempFileName();
                    szModelOutputFilename = szModelOutputFilename.Substring(0, szModelOutputFilename.LastIndexOf('.')) + ".3ds";

                    ModelBase crater = new CraterGen(dbDiameter, dbDepth);
                    crater.OutputFilename = szModelOutputFilename;
                    crater.triype = triType;
                    crater.mappingType = mappingType;

                    //将撞击坑模型添加到相应图层
                    if (crater.generate())
                    {
                        //添加到Crater图层
                        try
                        {
                            //IMultiPatch pMP = new MultiPatchClass();
                            // IFeatureClass pFC = m_CraterLayer.FeatureClass;
                            IFeatureClass pFC = CraterFeatureClass;
                            IFeature pF = pFC.CreateFeature();
                            IImport3DFile pI3D = new Import3DFileClass();
                            pI3D.CreateFromFile(szModelOutputFilename);
                            IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                            ITransform3D pT3D = pMP as ITransform3D;

                            pT3D.Move3D(mapPoint.X, mapPoint.Y, mapPoint.Z);
                            pF.Shape = pMP as IGeometry;
                            pF.Store();
                        }
                        catch (SystemException ee)
                        {
                            MessageBox.Show(ee.Message);
                            continue;
                        }
                        //finally
                        //{
                        //    try
                        //    {
                        //        Directory.Delete(szModelOutputFilename);
                        //    }
                        //    catch (System.Exception ex)
                        //    {
                        //        MessageBox.Show(ex.Message);
                        //    }
                        //}
                    }
                }
                else //石块
                {
                    //获得当前文件路径下的石头模型文件,并随机获得石块模型
                    String szAppPathName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\RockModel";
                    String[] szFileList = Directory.GetFiles(szAppPathName);
                    if (szFileList.Length == 0)
                    {
                        continue;
                    }

                    Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                    int nRandomPos = r.Next(szFileList.Length);
                    String szModelOutputFilename = szFileList[nRandomPos];

                    //将石块模型添加到图层
                    IFeatureClass pFC = NonCraterFeatureClass;
                    IFeature pF = pFC.CreateFeature();
                    IImport3DFile pI3D = new Import3DFileClass();
                    pI3D.CreateFromFile(szModelOutputFilename);
                    IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                    ITransform3D pT3D = pMP as ITransform3D;
                    pT3D.Move3D(mapPoint.X, mapPoint.Y, mapPoint.Z);

                    double dbModelRangeX = pMP.Envelope.Width;
                    double dbModelRangeY = pMP.Envelope.Height;
                    double dbModelRangeZ = pMP.Envelope.ZMax - pMP.Envelope.ZMin;

                    double dbScale = model.dbSize / Math.Max(dbModelRangeX, Math.Max(dbModelRangeY, dbModelRangeZ));
                    pT3D.Scale3D(mapPoint, dbScale, dbScale, dbScale);

                    pF.Shape = pMP as IGeometry;
                    pF.Store();
                }
            }

            return true;
        }

        private IPixelBlock getTextureRaster(/*IPnt ptLeftTop, */IPnt ptBlockSize, TextureTypeForModel textureTypeForModel)
        {
            if (/*ptLeftTop == null || */ptBlockSize == null)
                return null;

            IPixelBlock pixelBlock = null;
            String szAppPathName = null;

            //获得当前文件路径下的模型纹理文件,并随机获得纹理
            if (textureTypeForModel == TextureTypeForModel.ForCrater)
            {
                szAppPathName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\CraterTexture";
            }
            else
            {
                szAppPathName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\RockTexture";
            }

            String[] szFileList = Directory.GetFiles(szAppPathName);
            if (szFileList.Length == 0)
                return null;

            while (true)
            {
                //随机获得纹理
                Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                int nRandomPos = r.Next(szFileList.Length);
                String szModelTextureFilename = szFileList[nRandomPos];

                //构建纹理的RASTER对象
                try
                {
                    IRasterLayer pRasterDataset = new RasterLayerClass();
                    pRasterDataset.CreateFromFilePath(szModelTextureFilename);

                    IPnt ptLeftTop = new DblPntClass();
                    ptLeftTop.SetCoords(0, 0);

                    IRaster pRaster = pRasterDataset.Raster;
                    IRasterProps pProps = pRaster as IRasterProps;
                    int nWidth = pProps.Width;
                    int nHeight = pProps.Height;

                    if (nWidth > ptBlockSize.Y && nHeight > ptBlockSize.X)
                    {
                        pixelBlock = getRasterPixelBlock(ptLeftTop, ref ptBlockSize, pRaster);
                        break;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }

            return pixelBlock;
        }
        
        private IPixelBlock getRasterPixelBlock(IPnt ptLeftTop, ref IPnt ptBlockSize, IRaster pRaster)
        {
            //IPixelBlock pixelBlock = null;
            if (ptLeftTop == null || pRaster == null)
                return null;

            IRaster iRaster = pRaster;
            IGeoDataset pGeoData = iRaster as IGeoDataset;
            IRasterBandCollection pRasBC = iRaster as IRasterBandCollection;
            IRasterBand pRasBand = pRasBC.Item(0);
            IRawPixels pRawPixels = pRasBand as IRawPixels;
            IRasterProps pProps = pRawPixels as IRasterProps;

            //设置为空时，默认读取整个图像
            if (ptBlockSize == null)
            {
                ptBlockSize = new DblPntClass();
                ptBlockSize.X = pProps.Width;
                ptBlockSize.Y = pProps.Height;
            }

            IPixelBlock pPixelBlock = pRawPixels.CreatePixelBlock(ptBlockSize);
            pRawPixels.Read(ptLeftTop, pPixelBlock);

            return pPixelBlock;
        }

        private object getValidType(IRasterProps pProps, double dbValue)
        {
            object oReturnValue=null;
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

        private bool meanFilter(IPixelBlock pixelBlock, IRaster pRaster, int nBorderSize)
        {
            if (pixelBlock == null || nBorderSize <= 0)
                return false;

            if (nBorderSize % 2 == 0)
                nBorderSize++;

            int nWidth = pixelBlock.Width;
            int nHeight = pixelBlock.Height;
            double[,] dbData = new double[nWidth, nHeight];

            System.Array pixels=(System.Array)pixelBlock.get_SafeArray(0);
            for (int i = 0; i < nWidth; i++)
            {
                for (int j = 0; j < nHeight; j++)
                {
                    dbData[i, j] = Convert.ToDouble(pixels.GetValue(i, j));
                }
            }

            if (!avgFilter(ref dbData, nBorderSize))
                return false;

            //if (!avgFilter(ref dbData, nBorderSize))
            //    return false;

            IRasterProps pProps=pRaster as IRasterProps;
            for (int i = 0; i < nWidth; i++)
            {
                for (int j = 0; j < nHeight; j++)
                {
                    object tmpValue=getValidType(pProps, dbData[i,j]);
                    pixels.SetValue(tmpValue,i,j);
                }
            }
                
            return true;
        }

        private double getMeanValue(IPixelBlock pixelBlock)
        {
            if (pixelBlock == null)
                return double.NaN;

            int nWidth = pixelBlock.Width;
            int nHeight = pixelBlock.Height;
            
            double dbValue = 0;
            IPixelBlock3 pPixelBlock3 = pixelBlock as IPixelBlock3;
            System.Array pixels=(System.Array)pPixelBlock3.get_PixelData(0);
            for (int i = 0; i < nHeight; i++)
            {
                for (int j = 0; j < nWidth; j++)
                {
                    dbValue += Convert.ToDouble(pixels.GetValue(j, i));
                }
            }

            dbValue /= (nHeight * nWidth);
            return dbValue;
        }

        //参考自http://blog.csdn.net/likezhaobin/article/details/6835049
        private double[,] gaussianFilter(int nSize, double dbSigma)
        {
            nSize = Math.Abs(nSize);
            dbSigma = Math.Abs(dbSigma);

            if (nSize % 2 == 0)
                nSize++;

            double[,] dbData = new double[nSize, nSize];
            double dbSigma2=dbSigma*dbSigma;
            int nCenterX=nSize/2;
            int nCenterY=nSize/2;
            
            double dbMaxValue=1/(2*Math.PI*dbSigma2);
            double dbMinValue=1/(2*Math.PI*dbSigma2)*Math.Pow(Math.E, -nCenterX*nCenterX/(2*dbSigma2));

            //产生高斯核函数
            for (int i = 0; i < nSize; i++)
            {
                for (int j = 0; j < nSize; j++)
                {
                    int nDeltaX=i-nCenterX;
                    int nDeltaY=j-nCenterY;

                    //超过卷积核大小的一半，就默认为0
                    long lDistance=nDeltaY*nDeltaY+nDeltaX*nDeltaX;
                    if (Math.Sqrt(lDistance) > (nSize / 2))
                    {
                        dbData[i, j] = 0;
                        continue;
                    }

                    double dbComponent=-lDistance/(2*dbSigma2);
                    dbData[i,j]=1/(2*Math.PI*dbSigma2)*Math.Pow(Math.E, dbComponent);

                    //归一化到[0,1]
                    dbData[i,j]=(dbData[i,j]-dbMinValue)/(dbMaxValue-dbMinValue);
                }
            }

            return dbData;
            ////归一化到[0,1]
            //for (int i = 0; i < nSize; i++)
            //{
            //    for (int j = 0; j < nSize; j++)
            //    {
            //        dbData[i,j]=(dbData[i,j]-dbData[nCenterX, 0])/(dbData[nCenterX, nCenterY]-dbData[nCenterX, 0]); //dbData[nCenterX, nCenterY]为核中最大的数
                    
            //        if(dbData[i,j]<0)
            //            dbData[i,j]=0;
            //    }
            //}
        }

        public bool MergeWithRaster(IRasterLayer pRasterLayer, IFeatureClass craterFeatureClass, IFeatureClass nonCraterFeatureClass)
        {
            if (pRasterLayer == null)
                return false;

            IRaster pRaster = pRasterLayer.Raster;
            if (pRaster == null)
                return false;

            //加载DOM到RASTER
            bool bDomRasterFlag = false;
            IRaster pDomRaster = null;
            IRasterProps pDomRasterProps=null;
            IRasterEdit pDomRasterEdit = null;
            IPixelBlock pDomPixelBlock = null;
            IPixelBlock3 domPixelBlock3 = null;
            System.Array domPixels = null;
            int nCraterWidth = int.MinValue;
            int nCraterHeight = int.MinValue;
            System.Array pCraterPixels = null;

            IRasterLayer pDomRasterLayer = new RasterLayerClass();
            try
            {
                //得到RASTER的路径
                string rasterfilepath = pRasterLayer.FilePath;//选中图层的文件路径
                string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
                string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称
                string domFilename = rasterfilename + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
                string domFilePath = rasterfiledir + "\\" + domFilename;

                pDomRasterLayer.CreateFromFilePath(domFilePath);
            }
            catch (System.Exception ex)
            {
                pDomRasterLayer = null;
                pDomRasterEdit = null;
                bDomRasterFlag = false;
                MessageBox.Show("未找到相应的纹理，不会进行纹理的融合操作！");
            }

            if (pDomRasterLayer != null)
            {
                pDomRaster = pDomRasterLayer.Raster;
                pDomRasterEdit = (IRasterEdit)pDomRaster;
                pDomRasterProps=pDomRaster as IRasterProps;
                bDomRasterFlag = true;
            }

            //得到MultiPatch里的图层
            ClsGDBDataCommon GDC = new ClsGDBDataCommon();
            IWorkspace pWS = GDC.OpenFromFileGDB(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\DefaultFileGDB\MultiPatchFile.gdb");
            IFeatureClass CraterFeatureClass = craterFeatureClass;// ((IFeatureWorkspace)pWS).OpenFeatureClass("Crater");
            IFeatureClass NonCraterFeatureClass = nonCraterFeatureClass;// ((IFeatureWorkspace)pWS).OpenFeatureClass("NonCrater");
            
            //得到当前RASTER的一些属性
            IRaster2 pRaster2 = pRaster as IRaster2;
            IRasterProps pRasterProps = pRaster as IRasterProps;
            IRasterEdit rasterEdit = (IRasterEdit)pRaster;
            //IGeoDataset pGeoData = pRaster as IGeoDataset;
            //IEnvelope pExtent = pGeoData.Extent;
            //IRasterBandCollection pRasBC = pRaster as IRasterBandCollection;
            //IRasterBand pRasBand = pRasBC.Item(0);
            //IRawPixels pRawPixels = pRasBand as IRawPixels;
            //IRasterProps pProps = pRawPixels as IRasterProps;
            double dExpandDis = 0.5; // 设置一般情况下向外截取raster的距离
            int nMinExpandSize = 2;  // 设置向外截raster的最小扩展像素量

            int nXExpandCellSize = (int)(dExpandDis / pRasterProps.MeanCellSize().X );
            int nYExpandCellSize = (int)(dExpandDis / pRasterProps.MeanCellSize().Y);
            if (nXExpandCellSize < nMinExpandSize)
            {
                nXExpandCellSize = nMinExpandSize;
            }
            if (nYExpandCellSize < nMinExpandSize)
            {
                nYExpandCellSize = nMinExpandSize;
            }

            int nWidth = pRasterProps.Width;
            int nHeight = pRasterProps.Height;
            double dbMarginWidth = pRasterProps.MeanCellSize().X * nXExpandCellSize;
            double dbMarginHeight = pRasterProps.MeanCellSize().Y * nYExpandCellSize;
            object oNoDataValue = pRasterProps.NoDataValue;

            //只留的石块或撞击坑的位置和块信息
            List<IPixelBlock> listDemPixelBlock = new List<IPixelBlock>();
            List<IPixelBlock> listDomPixelBlock = new List<IPixelBlock>();
            List<IPnt> listPixelLeftTop = new List<IPnt>();
            List<IPnt> listDomLeftTop = new List<IPnt>();
            Random r = new Random(SubDivision.Chaos_GetRandomSeed());
            ITable pntable = NonCraterFeatureClass as ITable;
            ITable pctable = CraterFeatureClass as ITable;
            int addstep = 100 / (pntable.RowCount(null) + pctable.RowCount(null));

            bool bTmpFlag = bDomRasterFlag;
            if (NonCraterFeatureClass != null)
            {
                #region   //融合石块
                IFeatureCursor pCursor = NonCraterFeatureClass.Search(null, false);
                IFeature pFeature = pCursor.NextFeature();
                while (pFeature != null)
                {
                    //指示是否得到DOM的值
                    bDomRasterFlag = bTmpFlag;

                    //1、得到局部区域的RASTER块
                    IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                    IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                    IEnvelope pEnvelope = pProjGeometry.Envelope;

                    //// //得到当前模型指定位置的像素值
                    int nCol, nRow;
                    IPnt ptLeftTop = new DblPntClass();
                    //pRaster2.MapToPixel(pEnvelope.UpperLeft.X, pEnvelope.UpperLeft.Y, out nCol, out nRow);
                   // pRaster2.MapToPixel(pEnvelope.UpperLeft.X - dbMarginWidth, pEnvelope.UpperLeft.Y - dbMarginHeight, out nCol, out nRow);
                    pRaster2.MapToPixel(pEnvelope.UpperLeft.X - dbMarginWidth, pEnvelope.UpperLeft.Y + dbMarginHeight, out nCol, out nRow);
                    ptLeftTop.SetCoords(nCol, nRow);
                  
                    {
                        //2、构TIN
                        IRaster iRaster = pRaster;
                        IGeoDataset pGeoData = iRaster as IGeoDataset;
                        IEnvelope pExtent = pGeoData.Extent;
                        IRasterBandCollection pRasBC = iRaster as IRasterBandCollection;
                        IRasterBand pRasBand = pRasBC.Item(0);
                        IRawPixels pRawPixels = pRasBand as IRawPixels;
                        IRasterProps pProps = pRawPixels as IRasterProps;

                        ITinEdit pTinEdit = new TinClass() as ITinEdit;
                        // pTinEdit.InitNew(pExtent);
                        IEnvelope pEv = new EnvelopeClass();
                        //如果越界就不融合
                        pEnvelope.Expand(dbMarginWidth, dbMarginHeight, false);
                        if (pEnvelope.XMax > pRasterProps.Extent.XMax || pEnvelope.XMin < pRasterProps.Extent.XMin ||
                            pEnvelope.YMax > pRasterProps.Extent.YMax || pEnvelope.YMin < pRasterProps.Extent.YMin)
                        {
                            pFeature = pCursor.NextFeature();
                            continue;
                        } 
                        listPixelLeftTop.Add(ptLeftTop);
                        pTinEdit.InitNew(pEnvelope);
                        ISpatialReference pSpatial = pGeoData.SpatialReference;
                        pExtent.SpatialReference = pSpatial;

                        object nodata = pProps.NoDataValue; //无值标记
                        object vtMissing = Type.Missing;
                        object vPixels = null; //格子
                        double m_zTolerance = 0;

                        double cellsize = 0.0f; //栅格大小
                        IPnt pPnt1 = pProps.MeanCellSize(); //栅格平均大小
                        cellsize = pPnt1.X;

                        IPnt pBlockSize = new DblPntClass();
                        pBlockSize.X = Math.Ceiling(pEnvelope.Width / cellsize + 2*dbMarginWidth);  //左右各往外拓dbmarginwidth的边界
                        pBlockSize.Y = Math.Ceiling(pEnvelope.Height / cellsize + 2*dbMarginHeight);
                        IPnt pOrigin = new DblPntClass();
                        IPnt pPixelBlockOrigin = new DblPntClass();
                        IPixelBlock pPixelBlock = pRawPixels.CreatePixelBlock(pBlockSize);
                        pPixelBlockOrigin.SetCoords(ptLeftTop.X, ptLeftTop.Y);
                        pRawPixels.Read(pPixelBlockOrigin, pPixelBlock);
                        vPixels = pPixelBlock.get_SafeArray(0);
                        System.Array pArrayO = vPixels as System.Array;

                        //得到平均高程
                        double dbMeanValue = getMeanValue(pPixelBlock);
                        dbMeanValue *= 2;

                        //抽析PIXELBLOCK                         
                        int nCoef = 10;
                        nCoef = (int) (pBlockSize.X / 50);
                        if (nCoef <1)
                        {
                            nCoef = 1;
                        }
                        IPnt pDstBlockSize = new DblPntClass();
                        pDstBlockSize.X = Math.Floor(pBlockSize.X / nCoef);// Math.Ceiling(pEnvelope.Width / cellsize + 2 * dbMarginWidth);  //左右各往外拓dbmarginwidth的边界
                        pDstBlockSize.Y =Math.Floor(pBlockSize.Y / nCoef) ;// Math.Ceiling(pEnvelope.Height / cellsize + 2 * dbMarginHeight);
                        IPixelBlock pDstPixelBlock = pRawPixels.CreatePixelBlock(pDstBlockSize);
                        object vDstPixels = pDstPixelBlock.get_SafeArray(0);

                        System.Array pArrayD = vDstPixels as System.Array;
                        for (int i = 0; i < pDstBlockSize.X; i++)
                        {
                            for (int j = 0; j < pDstBlockSize.Y; j++)
                            {
                                double dbTmpValue = dbMeanValue - Convert.ToDouble(pArrayO.GetValue(i * nCoef, j * nCoef));
                                object oTmpValue = getValidType(pProps, dbTmpValue);
                                pArrayD.SetValue(oTmpValue,i,j);
                                //pArrayD.SetValue(pArrayO.GetValue(i*nCoef,j*nCoef),i,j);
                            }
                        }

                        pExtent = pEnvelope;
                        double xMin = pExtent.XMin;
                        double yMax = pExtent.YMax;
                        pOrigin.X = xMin + cellsize / 2;
                        pOrigin.Y = yMax - cellsize / 2;
                        double bX = pOrigin.X;
                        double bY = pOrigin.Y;
                        pTinEdit.AddFromPixelBlock(bX, bY, cellsize*nCoef, cellsize*nCoef, nodata, vDstPixels, m_zTolerance, ref vtMissing, out vtMissing);
                        //pTinEdit.AddFromPixelBlock(bX, bY, cellsize, cellsize, nodata, vPixels, m_zTolerance, ref vtMissing, out vtMissing);
                        //简化tin，加快运算速度
                        //ITinSurface2 pTinSurface = pTinEdit as ITinSurface2;
                        //object nodecount = null;
                        //ITinEdit pSimpleTinEdit2 = new TinClass() as ITinEdit;
                        //pSimpleTinEdit2.InitNew(pEnvelope);
                        //ITin simplepTin = pSimpleTinEdit2 as ITin;
                        //pTinSurface.DecimateNodes(pEnvelope, cellsize, false, ref nodecount, out simplepTin);
                        //pTinEdit = simplepTin as ITinEdit;
                        //3、融合
                        ITinLayer pTinLayer = new TinLayerClass();
                        pTinLayer.Dataset = pTinEdit as ITin;
                        try
                        {
                            vtMissing = true;
                            //pTinEdit = tin as ITinEdit;
                            pTinEdit.SaveAs(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\DefaultFileGDB\TempTin", ref vtMissing);
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

                        FrmMerge merge = new FrmMerge();
                        merge.MergeNonCraterToMemoryTin(pTinLayer, pFeature);

                        //4、TIN转RASTER
                        ITinSurface pSurface = pTinEdit as ITinSurface;
                        IPoint pt = new PointClass();
                        double dbGeoX = double.NaN, dbGeoY = double.NaN;

                        //Populate some pixel values to the pixel block.
                        System.Array pixels;
                        IPixelBlock pixelBlock = pPixelBlock as IPixelBlock;
                        IPixelBlock3 pPixelBlock3 = pixelBlock as IPixelBlock3;
                        pixels = (System.Array)pPixelBlock3.get_PixelData(0);

                        //获得DOM的数组
                        if (bDomRasterFlag)
                        {
                            pDomPixelBlock = getRasterPixelBlock(ptLeftTop, ref pBlockSize, pDomRaster);
                            if (pDomPixelBlock == null)
                                bDomRasterFlag = false;
                            else
                            {
                                domPixelBlock3 = pDomPixelBlock as IPixelBlock3;
                                domPixels = (System.Array)domPixelBlock3.get_PixelData(0);
                            }
                        }

                        ////获得纹理的数组
                        //System.Array texturePixels=null;
                        //IPixelBlock texturePixelBlock = getTextureRaster(pBlockSize, TextureTypeForModel.ForNonCrater);
                        //if (texturePixelBlock == null)
                        //    bDomRasterFlag = false;
                        //else
                        //{
                        //    IPixelBlock3 texturePixelBlock3 = texturePixelBlock as IPixelBlock3;
                        //    texturePixels = (System.Array)texturePixelBlock3.get_PixelData(0);
                        //}                        

                        IPnt ptSize = pBlockSize as IPnt;
                        for (int m = 0; m < Math.Round(ptSize.Y); m++)
                        {
                            for (int n = 0; n < Math.Round(ptSize.X); n++)
                            {
                                pRaster2.PixelToMap(Convert.ToInt32(ptLeftTop.X + n), Convert.ToInt32(ptLeftTop.Y + m), out dbGeoX, out dbGeoY);
                                pt.X = dbGeoX;
                                pt.Y = dbGeoY;

                                double z = pSurface.GetElevation(pt);
                                if (double.IsNaN(z))
                                {
                                    continue;
                                }

                                try
                                {
                                    //object oz = getValidType(pProps, z);
                                    object oz = getValidType(pProps, dbMeanValue - z);
                                    double oOriValue = Convert.ToDouble(pixels.GetValue(n, m));
                                    if (z - oOriValue < 0)
                                    {
                                        continue;
                                    }
                                    pixels.SetValue(oz, n, m);

                                    //改变纹理值
                                    if (bDomRasterFlag)
                                    {
                                        double dbDomOriValue = Convert.ToDouble(domPixels.GetValue(n, m));
                                        double dbDiff = z - oOriValue;

                                        if (Math.Abs(dbDiff) >= (pProps.MeanCellSize().X + pProps.MeanCellSize().Y) / 2)
                                        {
                                            object oDomValue = getValidType(pDomRasterProps, dbDomOriValue + r.NextDouble()*40);
                                            domPixels.SetValue(oDomValue, n, m);
                                        }
                                    }
                                }
                                catch (System.Exception ex)
                                {
                                	continue;
                                }                                                              
                            }
                        }
                        pPixelBlock3.set_PixelData(0, (System.Array)pixels);
                        listDemPixelBlock.Add(pixelBlock);

                        //改变纹理值
                        if (bDomRasterFlag)
                        {
                            domPixelBlock3.set_PixelData(0, (System.Array)domPixels);
                            listDomPixelBlock.Add(pDomPixelBlock);
                            listDomLeftTop.Add(ptLeftTop);
                        } 

                        pFeature = pCursor.NextFeature();
                    }

                    //改变DEM的RASTER值
                    if (listDemPixelBlock.Count != listPixelLeftTop.Count)
                        return false;
                    for (int i = 0; i < listPixelLeftTop.Count; i++)
                    {
                        //Write the pixel block.
                        //写之前中值滤波，缓解边界效应
                        //if (meanFilter(listDemPixelBlock[i], pRaster, 5))
                        //{
                        rasterEdit.Write(listPixelLeftTop[i], listDemPixelBlock[i]);
                        rasterEdit.Refresh();
                        //}
                    }

                    //改变DOM的RASTER值
                    if (listDomPixelBlock.Count != listDomLeftTop.Count)
                        return false;
                    for (int i = 0; i < listDomLeftTop.Count; i++)
                    {
                        //Write the pixel block.
                        pDomRasterEdit.Write(listDomLeftTop[i], listDomPixelBlock[i]);
                        pDomRasterEdit.Refresh();
                    }

                    //进度条
                    if (pProgressbar != null)
                        pProgressbar.Value += addstep;
                }
                #endregion
            }

            listDemPixelBlock.Clear();
            listDomPixelBlock.Clear();
            listPixelLeftTop.Clear();// = new List<IPnt>();
            listDomLeftTop.Clear();// = new List<IPnt>();
            if (CraterFeatureClass != null)
            {
#region 读取撞击坑样本图像
                bool bDomSampleFlag = false;
                if (bDomRasterFlag)
                {
                    IRasterLayer pCraterSampleLayer = new RasterLayerClass();
                    try
                    {
                        string rasterfilepath = pRasterLayer.FilePath;//选中图层的文件路径
                        string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
                        string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称
                        //string domFilename = rasterfilename + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
                        //string szTextureFilaname = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\CraterTexture\DEM-New-01_dom_Clip.tif";
                        //DateTime dt=DateTime.Now;

                        string szTextureFilaname = rasterfiledir + "\\" + rasterfilename + "_dom_clip.tif";// @"\CraterTexture\DEM-New-01_dom_Clip.tif";
                        if (System.IO.File.Exists(szTextureFilaname))
                        {
                            pCraterSampleLayer.CreateFromFilePath(szTextureFilaname);

                            IRaster pCraterRaster = pCraterSampleLayer.Raster;
                            IRasterProps pCraterProps = pCraterRaster as IRasterProps;

                            IPnt ptCraterLeftTop = new DblPntClass();
                            IPnt ptCraterSize = null;
                            ptCraterLeftTop.SetCoords(0, 0);
                            IPixelBlock pCraterPixelBlocks = getRasterPixelBlock(ptCraterLeftTop, ref ptCraterSize, pCraterRaster);
                            nCraterWidth = Convert.ToInt32(ptCraterSize.X);
                            nCraterHeight = Convert.ToInt32(ptCraterSize.Y);

                            //System.Array pCraterPixels = null;
                            if (pCraterPixelBlocks == null)
                                bDomSampleFlag = false;
                            else
                            {
                                IPixelBlock3 pCraterPixelBlock3 = pCraterPixelBlocks as IPixelBlock3;
                                pCraterPixels = (System.Array)pCraterPixelBlock3.get_PixelData(0);
                                bDomSampleFlag = true;
                            }
                        }
                        else
                        {
                            //MessageBox.Show("这幅图还未截取撞击坑纹理样本，不会进行撞击坑纹理的融合！");
                            bDomSampleFlag = false;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    bDomSampleFlag = bDomRasterFlag;//false
#endregion

                #region//融合撞击坑
                IFeatureCursor pCursor = CraterFeatureClass.Search(null, false);
                IFeature pFeature = pCursor.NextFeature();

                while (pFeature != null)
                {
                    //指示是否得到DOM的值
                    bDomRasterFlag = bTmpFlag;

                    //1、得到局部区域的RASTER块
                    IMultiPatch pMultiPatch = pFeature.Shape as IMultiPatch;
                    IGeometry pProjGeometry = pMultiPatch.XYFootprint;
                    IEnvelope pEnvelope = pProjGeometry.Envelope;

                    //// //得到当前模型指定位置的像素值
                    int nCol, nRow;
                    IPnt ptLeftTop = new DblPntClass();
                    //pRaster2.MapToPixel(pEnvelope.UpperLeft.X - dbMarginWidth, pEnvelope.UpperLeft.Y-dbMarginHeight, out nCol, out nRow);
                    pRaster2.MapToPixel(pEnvelope.UpperLeft.X - dbMarginWidth, pEnvelope.UpperLeft.Y + dbMarginHeight, out nCol, out nRow);
                    ptLeftTop.SetCoords(nCol, nRow);
                 

                    //2、构TIN
                    IRaster iRaster = pRaster;
                    IGeoDataset pGeoData = iRaster as IGeoDataset;
                    IEnvelope pExtent = pGeoData.Extent;
                    IRasterBandCollection pRasBC = iRaster as IRasterBandCollection;
                    IRasterBand pRasBand = pRasBC.Item(0);
                    IRawPixels pRawPixels = pRasBand as IRawPixels;
                    IRasterProps pProps = pRawPixels as IRasterProps;

                    ITinEdit pTinEdit = new TinClass() as ITinEdit;
                    // pTinEdit.InitNew(pExtent);
                    //如果越界就不融合
                    pEnvelope.Expand(dbMarginWidth, dbMarginHeight, false);
                    if (pEnvelope.XMax > pRasterProps.Extent.XMax || pEnvelope.XMin < pRasterProps.Extent.XMin ||
                        pEnvelope.YMax > pRasterProps.Extent.YMax || pEnvelope.YMin < pRasterProps.Extent.YMin)
                    {
                        pFeature = pCursor.NextFeature();
                        continue;
                    }   
                    listPixelLeftTop.Add(ptLeftTop);
                    pTinEdit.InitNew(pEnvelope);
                    ISpatialReference pSpatial = pGeoData.SpatialReference;
                    pExtent.SpatialReference = pSpatial;

                    object nodata = pProps.NoDataValue; //无值标记
                    object vtMissing = Type.Missing;
                    object vPixels = null; //格子
                    double m_zTolerance = 0;

                    double cellsize = 0.0f; //栅格大小
                    IPnt pPnt1 = pProps.MeanCellSize(); //栅格平均大小
                    cellsize = pPnt1.X;

                    IPnt pBlockSize = new DblPntClass();
                    pBlockSize.X = Math.Ceiling(pEnvelope.Width / cellsize + 2*dbMarginWidth);
                    pBlockSize.Y = Math.Ceiling(pEnvelope.Height / cellsize + 2*dbMarginHeight);
                    IPnt pOrigin = new DblPntClass();
                    IPnt pPixelBlockOrigin = new DblPntClass();
                    IPixelBlock pPixelBlock = pRawPixels.CreatePixelBlock(pBlockSize);
                    pPixelBlockOrigin.SetCoords(ptLeftTop.X, ptLeftTop.Y);
                    pRawPixels.Read(pPixelBlockOrigin, pPixelBlock);
                    vPixels = pPixelBlock.get_SafeArray(0);
                    System.Array pArrayO = vPixels as System.Array;

                    //得到平均高程
                    double dbMeanValue = getMeanValue(pPixelBlock);
                    dbMeanValue *= 2;

                    int nCoef = 10;
                    nCoef = (int)(pBlockSize.X / 50);
                    if (nCoef < 1)
                    {
                        nCoef = 1;
                    }
                    IPnt pDstBlockSize = new DblPntClass();
                    pDstBlockSize.X = Math.Floor(pBlockSize.X / nCoef);// Math.Ceiling(pEnvelope.Width / cellsize + 2 * dbMarginWidth);  //左右各往外拓dbmarginwidth的边界
                    pDstBlockSize.Y = Math.Floor(pBlockSize.Y / nCoef);// Math.Ceiling(pEnvelope.Height / cellsize + 2 * dbMarginHeight);
                    IPixelBlock pDstPixelBlock = pRawPixels.CreatePixelBlock(pDstBlockSize);
                    object vDstPixels = pDstPixelBlock.get_SafeArray(0);

                    System.Array pArrayD = vDstPixels as System.Array;
                    for (int i = 0; i < pDstBlockSize.X; i++)
                    {
                        for (int j = 0; j < pDstBlockSize.Y; j++)
                        {
                            double dbTmpValue=dbMeanValue-Convert.ToDouble(pArrayO.GetValue(i * nCoef, j * nCoef));
                            object oTmpValue = getValidType(pProps, dbTmpValue);
                            pArrayD.SetValue(oTmpValue, i, j);
                            //pArrayD.SetValue(pArrayO.GetValue(i * nCoef, j * nCoef), i, j);
                        }
                    }


                    pExtent = pEnvelope;
                    double xMin = pExtent.XMin;
                    double yMax = pExtent.YMax;
                    pOrigin.X = xMin + cellsize / 2;
                    pOrigin.Y = yMax - cellsize / 2;
                    double bX = pOrigin.X;
                    double bY = pOrigin.Y;
                    pTinEdit.AddFromPixelBlock(bX, bY, cellsize * nCoef, cellsize * nCoef, nodata, vDstPixels, m_zTolerance, ref vtMissing, out vtMissing);
                    //pTinEdit.AddFromPixelBlock(bX, bY, cellsize, cellsize, nodata, vPixels, m_zTolerance, ref vtMissing, out vtMissing);
                    //简化tin，加快运算速度
                    //ITinSurface2 pTinSurface = pTinEdit as ITinSurface2;
                    //object nodecount = null ; 
                    //ITinEdit pSimpleTinEdit2 = new TinClass() as ITinEdit;   
                    //pSimpleTinEdit2.InitNew(pEnvelope);
                    //ITin simplepTin = pSimpleTinEdit2 as ITin;
                    //pTinSurface.DecimateNodes(pEnvelope, cellsize, false, ref nodecount, out simplepTin);
                    //pTinEdit = simplepTin as ITinEdit;

                    //3、融合
                    ITinLayer pTinLayer = new TinLayerClass();
                    pTinLayer.Dataset = pTinEdit as ITin;
                    try
                    {
                        vtMissing = true;
                        //pTinEdit = tin as ITinEdit;
                        pTinEdit.SaveAs(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\DefaultFileGDB\TempTin", ref vtMissing);
                        pTinEdit.StopEditing(true);
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

                    FrmMerge merge = new FrmMerge();
                    merge.MergeCraterToMemoryTin(pTinLayer, pFeature);

                    //4、TIN转RASTER
                    ITinSurface pSurface = pTinEdit as ITinSurface;
                    IPoint pt = new PointClass();
                    double dbGeoX = double.NaN, dbGeoY = double.NaN;
                    
                    //Populate some pixel values to the pixel block.
                    System.Array pixels;
                    IPixelBlock pixelBlock = pPixelBlock as IPixelBlock;
                    IPixelBlock3 pPixelBlock3 = pixelBlock as IPixelBlock3;
                    pixels = (System.Array)pPixelBlock3.get_PixelData(0);

                    //获得DOM的数组
                    if (bDomRasterFlag)
                    {
                        pDomPixelBlock = getRasterPixelBlock(ptLeftTop, ref pBlockSize, pDomRaster);
                        if (pDomPixelBlock == null)
                            bDomRasterFlag = false;
                        else
                        {
                            domPixelBlock3 = pDomPixelBlock as IPixelBlock3;
                            domPixels = (System.Array)domPixelBlock3.get_PixelData(0);
                        }                        
                    }

                    //获得纹理的数组
                    //System.Array texturePixels=null;
                    //IPixelBlock texturePixelBlock = getTextureRaster(pBlockSize, TextureTypeForModel.ForNonCrater);
                    //if (texturePixelBlock == null)
                    //    bDomRasterFlag = false;
                    //else
                    //{
                    //    IPixelBlock3 texturePixelBlock3 = texturePixelBlock as IPixelBlock3;
                    //    texturePixels = (System.Array)texturePixelBlock3.get_PixelData(0);
                    //}                    

                    IPnt ptSize = pBlockSize as IPnt;

                    //得到高斯卷积核
                    int nGaussianSize=Convert.ToInt32(Math.Max(ptSize.X, ptSize.Y));
                    int nOffsetX=Convert.ToInt32((nGaussianSize-ptSize.X)/2);
                    int nOffsetY=Convert.ToInt32((nGaussianSize-ptSize.Y)/2);
                    double[,] dbGaussianFilter = gaussianFilter(nGaussianSize, nGaussianSize / 2);

                    for (int m = 0; m < Math.Round(ptSize.Y); m++)
                    {
                        for (int n = 0; n < Math.Round(ptSize.X); n++)
                        {
                            pRaster2.PixelToMap(Convert.ToInt32(ptLeftTop.X + n), Convert.ToInt32(ptLeftTop.Y + m), out dbGeoX, out dbGeoY);
                            pt.X = dbGeoX;
                            pt.Y = dbGeoY;

                            try
                            {
                                double z = pSurface.GetElevation(pt);
                                if (double.IsNaN(z))
                                {
                                    continue;
                                }

                                //object oz = getValidType(pProps, z);
                                object oz = getValidType(pProps, dbMeanValue-z);
                                double oOriValue = Convert.ToDouble(pixels.GetValue(n, m));
                                //if (z-oOriValue >0)
                                //{
                                //    continue;
                                //}
                                pixels.SetValue(oz, n, m);

                                //改变纹理值
                                if (bDomRasterFlag && bDomSampleFlag)
                                {
                                    double dbDomOriValue = Convert.ToDouble(domPixels.GetValue(n, m));
                                    double dbDiff = z - oOriValue;


                                    if (Math.Abs(dbDiff) >= (pProps.MeanCellSize().X + pProps.MeanCellSize().Y) / 2)
                                    {
                                        int ptTransX = Convert.ToInt32(Math.Round(n / ptSize.X * nCraterWidth));
                                        int ptTransY = Convert.ToInt32(Math.Round(m / ptSize.Y * nCraterHeight));
                                        double dbCraterValue = Convert.ToDouble(pCraterPixels.GetValue(ptTransX, ptTransY));

                                        //衰减函数
                                        //dbCraterValue = 40;
                                        //double dbWeight = dbGaussianFilter[n + nOffsetX, m + nOffsetY];
                                        //dbCraterValue = dbDomOriValue - dbCraterValue * dbWeight;
                                        double dbWeight = dbGaussianFilter[n + nOffsetX, m + nOffsetY];
                                        dbCraterValue = dbDomOriValue * (1 - dbWeight) + dbCraterValue * dbWeight;

                                        object oDomValue = getValidType(pDomRasterProps, dbCraterValue);
                                        domPixels.SetValue(oDomValue, n, m);
                                    }
                                }
                            }
                            catch (System.Exception ex)
                            {
                            	continue;
                            }
                        }
                    }

                    pPixelBlock3.set_PixelData(0, (System.Array)pixels);
                    listDemPixelBlock.Add(pixelBlock);

                    //改变纹理值
                    if (bDomRasterFlag && bDomSampleFlag)
                    {
                        domPixelBlock3.set_PixelData(0, (System.Array)domPixels);
                        listDomPixelBlock.Add(pDomPixelBlock);
                        listDomLeftTop.Add(ptLeftTop);
                    } 

                    pFeature = pCursor.NextFeature();

                    //进度条
                    if (pProgressbar != null)
                        pProgressbar.Value += addstep;
                }

                //改变DEM的RASTER值
                if (listDemPixelBlock.Count != listPixelLeftTop.Count)
                    return false;
                for (int i = 0; i < listPixelLeftTop.Count; i++)
                {
                    //Write the pixel block.\
                    rasterEdit.Write(listPixelLeftTop[i], listDemPixelBlock[i]);
                    rasterEdit.Refresh();
                }

                //改变DOM的RASTER值
                if (listDomPixelBlock.Count != listDomLeftTop.Count)
                    return false;
                for (int i = 0; i < listDomLeftTop.Count; i++)
                {
                    //Write the pixel block.
                    if (meanFilter(listDomPixelBlock[i], pDomRaster, 7))
                    {
                        pDomRasterEdit.Write(listDomLeftTop[i], listDomPixelBlock[i]);
                        pDomRasterEdit.Refresh();
                    }
                }

                #endregion
            }
          
            return true;
        }

        public bool MergeWithRaster(IRasterLayer pRasterLayer)
        {
            if (pRasterLayer == null)
                return false;

            //得到MultiPatch里的图层
            ClsGDBDataCommon GDC = new ClsGDBDataCommon();
            IWorkspace pWS = GDC.OpenFromFileGDB(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\DefaultFileGDB\MultiPatchFile.gdb");
            IFeatureClass CraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("Crater");
            IFeatureClass NonCraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("NonCrater");

            if (CraterFeatureClass == null || NonCraterFeatureClass == null)
                return false;

            return MergeWithRaster(pRasterLayer, CraterFeatureClass, NonCraterFeatureClass);
        }

        public bool addModelToTerrain(string szXmlFilename, /*string szDomFilename,*/ IRasterLayer pRasterLayer)
        {
            if (string.IsNullOrEmpty(szXmlFilename) || /*string.IsNullOrEmpty(szDomFilename) ||*/ pRasterLayer == null)
                return false;

            //ClsAddModelToTerrain tmpModelInfo=new ClsAddModelToTerrain();

            //读取XML文件 
            //List<Model> listModelInfo = readModelInfoFromXml(szXmlFilename);
            listModelInfo = readModelInfoFromXml(szXmlFilename);
            if (listModelInfo.Count <= 0)
                return false;

            //更新MultiPatch
            IRaster pRaster = pRasterLayer.Raster;
            if (!UpdateMultiPatchFromModel(listModelInfo, pRaster))
                return false;

            //////融合
            //if (!MergeWithRaster(pRasterLayer))
            //    return false;

            return true;
        }

        //static public bool TransAndMerge()
        //{
            
        //    return true;
        //}
    }
}
