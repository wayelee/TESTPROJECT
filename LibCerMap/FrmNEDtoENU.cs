using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

using DevComponents.DotNetBar;
using ESRI.ArcGIS.GeoAnalyst;

namespace LibCerMap
{
    public partial class FrmNEDtoENU : OfficeForm
    {

        IMapControl3 pMapControl;
        public string szDstName;//转换后文件存储路径
   
        public string szSrcName=string.Empty;//
  
        public FrmNEDtoENU(IMapControl3 mapcontrol, string filefullPath)//从数据接收位置打开
        {
            InitializeComponent();
            pMapControl = mapcontrol;
            szSrcName = filefullPath;
 
        }

        private void FrmNEDtoENU_Load(object sender, EventArgs e)
        {
             if (!string.IsNullOrEmpty(szSrcName))
            {
                cmbLayerTrans.Text = szSrcName;

                string szSrcPath = System.IO.Path.GetDirectoryName(szSrcName);
                string szSrcFilenameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(szSrcName);
                string szDstFilename = szSrcPath + "\\" + szSrcFilenameWithoutExtension + "_Trans" + System.IO.Path.GetExtension(szSrcName);
                txtLayerExp.Text = szDstFilename;
            }
            else
            {
                //初始化图层列表
                IEnumLayer pEnumLayer = pMapControl.Map.get_Layers(null, true);
                pEnumLayer.Reset();
                ILayer pLayer = null;
                while ((pLayer = pEnumLayer.Next()) != null)
                {
                    if (pLayer is IRasterLayer)
                    {
                        cmbLayerTrans.Items.Add(pLayer.Name);
                    }
                }

                if(cmbLayerTrans.Items.Count>0)
                {
                    cmbLayerTrans.SelectedIndex = 0;
                }                
            }

        }

        private void BtnWorkBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgOutputFile = new SaveFileDialog();
            dlgOutputFile.Title = "选择输出文件路径：";
            dlgOutputFile.InitialDirectory = ".";
            dlgOutputFile.Filter = "raster文件(*.tif)|*.tif|所有文件(*.*)|*.*";
            dlgOutputFile.RestoreDirectory = true;
            dlgOutputFile.DefaultExt = "tif";
            if (dlgOutputFile.ShowDialog()==DialogResult.OK)
            {
                txtLayerExp.Text = dlgOutputFile.FileName;
            }          
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            szDstName = txtLayerExp.Text;
            szSrcName = cmbLayerTrans.Text;

            //for (int i = 0; i < pMapControl.LayerCount; i++)
            //{
            //    if (pMapControl.get_Layer(i).Name == cmbLayerTrans.Text)
            //    {
            //        pRasterLayer = pMapControl.get_Layer(i) as IRasterLayer;
            //        break;
            //    }
            //}


            //首先进行坐标系x,y变换
            //string fileExpTran;//进行x,y做表转换后输出的tiff文件存储路径，用这一文件在进行后期的Z转换
            //fileExpTran = System.IO.Path.GetDirectoryName(LayerExpName) +"\\"+ System.IO.Path.GetFileNameWithoutExtension(LayerExpName)+"XY.tif";
            //try
            //{
            //    LayerExpName = txtLayerExp.Text;
            //    if (WorkMode==0)
            //    {
            //        if (pRasterOp.NorthEastToEastNorth(pRasterLayer, LayerExpName))
            //        {
            //            RasterLayerClass rasterlayer = new RasterLayerClass();
            //            rasterlayer.CreateFromFilePath(LayerExpName);
            //            //IRaster2 pRaster2 = rasterlayer.Raster as IRaster2;
            //            //IRasterDataset2 pRasterDataset = pRaster2.RasterDataset as IRasterDataset2;
            //            //ChangeRasterValue(pRasterDataset, -1, 0);
            //            pMapControl.AddLayer(rasterlayer as ILayer);
            //            this.Close();
            //        }             
            //    } 
            //    else
            //    {
            //        if (pRasterOp.NorthEastToEastNorth(pRasterLayerMode1, LayerExpName))
            //        {
            //            RasterLayerClass rasterlayer = new RasterLayerClass();
            //            rasterlayer.CreateFromFilePath(LayerExpName);
            //            //IRaster2 pRaster2 = rasterlayer.Raster as IRaster2;
            //            //IRasterDataset2 pRasterDataset = pRaster2.RasterDataset as IRasterDataset2;
            //            //ChangeRasterValue(pRasterDataset, -1, 0);
            //            pMapControl.AddLayer(rasterlayer as ILayer);
            //            this.Close();
            //        }            
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        ////采用另一种方法实现互换XY，不会改变影像的分辨率
        //static public bool NorthEastToEastNorth(IRasterLayer pRasterLayer, string szFilename)//坐标系x,y变换
        //{
        //    if (pRasterLayer == null || szFilename == null)
        //        return false;

        //    try
        //    {
        //        ITransformationOp dstTrans = new RasterTransformationOpClass();
        //        IRasterAnalysisEnvironment pDstAnalysisEnvironment = dstTrans as IRasterAnalysisEnvironment;
        //        if (pDstAnalysisEnvironment == null)
        //            return false;

        //        //设置分辨率
        //        IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
        //        IRasterProps pProps = pRaster2 as IRasterProps;
        //        double dbCellSizeX = pProps.MeanCellSize().X;
        //        double dbCellSizeY = pProps.MeanCellSize().Y;
        //        double dbCellSizeMean = (dbCellSizeX + dbCellSizeY) / 2;
        //        //pDstAnalysisEnvironment.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, dbCellSizeMean);

        //        //定义原始数据集和目标数据集
        //        IGeoDataset pSrcGeoDataset = pRasterLayer.Raster as IGeoDataset;
        //        IGeoDataset pDstGeoDataset = null;
        //        IGeoDataset pTmpGeoDataset = null;

        //        //顺时针旋转90度
        //        IPoint pt = new PointClass();
        //        pt.X = 0;
        //        pt.Y = 0;
        //        pTmpGeoDataset = dstTrans.Rotate(pSrcGeoDataset, esriGeoAnalysisResampleEnum.esriGeoAnalysisResampleCubic, 90, pt);
        //        //pDstGeoDataset = pTmpGeoDataset;

        //        //水平旋转            
        //        pSrcGeoDataset = dstTrans.Flip(pTmpGeoDataset);

        //        //由于翻转是以图像中心为界，所以要加上偏移量
        //        IEnvelope pExtent = pSrcGeoDataset.Extent;
        //        double dbCenterY = (pExtent.UpperLeft.Y + pExtent.LowerRight.Y) / 2;
        //        double dbDeltaX = 0;
        //        double dbDeltaY = -dbCenterY * 2;
        //        //pDstGeoDataset = dstTrans.Flip(pTmpGeoDataset);                
        //        pDstGeoDataset = dstTrans.Shift(pSrcGeoDataset, dbDeltaX, dbDeltaY, null);
        //        if (pDstGeoDataset == null)
        //            return false;

        //        IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
        //        IWorkspace inmemWor = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(szFilename), 0);
        //        ISaveAs2 sa = pDstGeoDataset as ISaveAs2;

        //        if (sa != null)
        //        {
        //            sa.SaveAs(System.IO.Path.GetFileName(szFilename), inmemWor, "TIFF");
        //            (pDstGeoDataset as IRasterDataset3).Refresh();
        //        }

        //        IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        ////这个版本虽然XY互换了，但会改变影像的分辨率
        ////static public bool NorthEastToEastNorth(IRasterLayer pRasterLayer, string szFilename)//坐标系x,y变换
        ////{
        ////    if (pRasterLayer == null || szFilename == null)
        ////        return false;

        ////    IGeoReference pGeoReference = pRasterLayer as IGeoReference;
        ////    IPoint pt = new PointClass();
        ////    pt.X = 0;
        ////    pt.Y = 0;
        ////    pGeoReference.Rotate(pt, -90); //顺时针旋转90

        ////    //水平旋转
        ////    IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
        ////    IRasterProps pProps = pRaster2 as IRasterProps;
        ////    int nWidth = pProps.Width;
        ////    int nHeight = pProps.Height;
        ////    double dbCenterY = (pProps.Extent.UpperLeft.Y + pProps.Extent.LowerRight.Y) / 2;
        ////    double dbDeltaX = 0;
        ////    double dbDeltaY = -dbCenterY * 2;
        ////    pGeoReference.Flip();
        ////    pGeoReference.Shift(dbDeltaX, dbDeltaY);

        ////    pGeoReference.Rectify(szFilename, "TIFF");
        ////    pGeoReference.Reset();
        ////    IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;
        ////    System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);

        ////    return true;
        ////}

        private void cmbLayerTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRasterLayer pRasterLayer = null;
            if (string.IsNullOrEmpty(szSrcName))
            {
                for (int i = 0; i < pMapControl.LayerCount; i++)
                {
                    if (pMapControl.get_Layer(i).Name == cmbLayerTrans.Text)
                    {
                        pRasterLayer = pMapControl.get_Layer(i) as IRasterLayer;
                        string rasterfilepath = pRasterLayer.FilePath;//选中图层的文件路径
                        string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
                        string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称

                        string szTmpName = rasterfiledir + "\\" + rasterfilename + "_ENU.tif";
                        txtLayerExp.Text = szTmpName;
                        break;
                    }
                }
            }
            else
            {
                //pRasterLayerMode1.CreateFromFilePath(fileFullPath);
                string rasterfiledir = System.IO.Path.GetDirectoryName(szSrcName);//文件位置
                string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(szSrcName);//文件名称
                string szTmpName = rasterfiledir + "\\" + rasterfilename + "_ENU.tif";
                txtLayerExp.Text = szTmpName;
            }
            

            //if (WorkMode==0)
            //{
            //    for (int i = 0; i < pMapControl.LayerCount; i++)
            //    {
            //        if (pMapControl.get_Layer(i).Name == cmbLayerTrans.Text)
            //        {
            //            pRasterLayer = pMapControl.get_Layer(i) as IRasterLayer;
            //            string rasterfilepath = pRasterLayer.FilePath;//选中图层的文件路径
            //            string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
            //            string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称
            //            LayerExpName = rasterfiledir + "\\" + rasterfilename + "_ENU.tif";
            //            txtLayerExp.Text = LayerExpName;
            //            break;
            //        }
            //    }
            //} 
            //else if(WorkMode==1)
            //{
            //    pRasterLayerMode1.CreateFromFilePath(fileFullPath);
            //    string rasterfiledir = System.IO.Path.GetDirectoryName(fileFullPath);//文件位置
            //    string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(fileFullPath);//文件名称
            //    LayerExpName = rasterfiledir + "\\" + rasterfilename + "_ENU.tif";
            //    txtLayerExp.Text = LayerExpName;
            //}           
        }

        //public void ChangeRasterValue(IRasterDataset2 pRasterDatset, double dbScale, double dbOffset)
        //{
        //    IRaster2 pRaster2 = pRasterDatset.CreateFullRaster() as IRaster2;

        //    IPnt pPntBlock = new PntClass();

        //    pPntBlock.X = 128;
        //    pPntBlock.Y = 128;

        //    IRasterCursor pRasterCursor = pRaster2.CreateCursorEx(pPntBlock);
        //    IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;

        //    if (pRasterEdit.CanEdit())
        //    {
        //        IRasterBandCollection pBands = pRasterDatset as IRasterBandCollection;
        //        IPixelBlock3 pPixelblock3 = null;
        //        int pBlockwidth = 0;
        //        int pBlockheight = 0;
        //        System.Array pixels;
        //        IPnt pPnt = null;
        //        object pValue;
        //        long pBandCount = pBands.Count;

        //        //获取Nodata
        //        IRasterProps pRasterPro = pRaster2 as IRasterProps;
        //        object pNodata = pRasterPro.NoDataValue;
        //        //double dbNoData = Convert.ToDouble(((double[])pNodata)[0]);
        //        double dbNoData = getNoDataValue(pNodata);

        //        do
        //        {
        //            pPixelblock3 = pRasterCursor.PixelBlock as IPixelBlock3;
        //            pBlockwidth = pPixelblock3.Width;
        //            pBlockheight = pPixelblock3.Height;

        //            for (int k = 0; k < pBandCount; k++)
        //            {
        //                pixels = (System.Array)pPixelblock3.get_PixelData(k);
        //                for (int i = 0; i < pBlockwidth; i++)
        //                {
        //                    for (int j = 0; j < pBlockheight; j++)
        //                    {
        //                        pValue = pixels.GetValue(i, j);
        //                        double ob = Convert.ToDouble(pValue);
        //                        if (ob != dbNoData)
        //                        {
        //                            ob *= dbScale;  //翻转
        //                            ob += dbOffset; //Z方向偏移                                
        //                        }

        //                        IRasterProps pRP = pRaster2 as IRasterProps;
        //                        if (pRP.PixelType == rstPixelType.PT_CHAR)
        //                            pixels.SetValue(Convert.ToChar(ob), i, j);
        //                        else if (pRP.PixelType == rstPixelType.PT_UCHAR)
        //                            pixels.SetValue(Convert.ToByte(ob), i, j);
        //                        else if (pRP.PixelType == rstPixelType.PT_FLOAT)
        //                            pixels.SetValue(Convert.ToSingle(ob), i, j);
        //                        else if (pRP.PixelType == rstPixelType.PT_DOUBLE)
        //                            pixels.SetValue(Convert.ToDouble(ob), i, j);
        //                        else if (pRP.PixelType == rstPixelType.PT_ULONG)
        //                            pixels.SetValue(Convert.ToInt32(ob), i, j);
        //                        else
        //                            ;
        //                    }
        //                }
        //                pPixelblock3.set_PixelData(k, pixels);

        //                System.Array textPixel = null;
        //                textPixel = (System.Array)pPixelblock3.get_PixelData(k);
        //            }

        //            pPnt = pRasterCursor.TopLeft;
        //            pRasterEdit.Write(pPnt, (IPixelBlock)pPixelblock3);
        //        }
        //        while (pRasterCursor.Next());

        //        //改变了Z值，重新统计下直方图
        //        //IRasterDataset2 prd = pRaster2 as IRasterDataset2;
        //        IRasterDatasetEdit3 pRedtit = pRasterDatset as IRasterDatasetEdit3;
        //        pRedtit.DeleteStats();
        //        pRedtit.ComputeStatisticsHistogram(1, 1, null, true);

        //        pRasterEdit.Refresh();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);
        //    }
        //}

        //static public double getNoDataValue(object pObject)
        //{
        //    double dbNoData = double.NaN;

        //    if (pObject is double[])
        //        dbNoData = Convert.ToDouble(((double[])pObject)[0]);
        //    else if (pObject is float[])
        //        dbNoData = Convert.ToDouble(((float[])pObject)[0]);
        //    else if (pObject is Int64[])
        //        dbNoData = Convert.ToDouble(((Int64[])pObject)[0]);
        //    else if (pObject is Int32[])
        //        dbNoData = Convert.ToDouble(((Int32[])pObject)[0]);
        //    else if (pObject is Int16[])
        //        dbNoData = Convert.ToDouble(((Int16[])pObject)[0]);
        //    else if (pObject is byte[])
        //        dbNoData = Convert.ToDouble(((byte[])pObject)[0]);
        //    else if (pObject is char[])
        //        dbNoData = Convert.ToDouble(((char[])pObject)[0]);
        //    else
        //        ;

        //    return dbNoData;
        //}

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
