using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using DevComponents.DotNetBar;


namespace LibCerMap
{
    public partial class FrmDEMToTin : OfficeForm
    {
        public FrmDEMToTin()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        public string m_DEMPath;
        public string m_TINPath;
        public ITin m_pTin = null;
        public IMap m_pMap;
        private void FrmDEMToTin_Load(object sender, EventArgs e)
        {
            if (m_pMap != null)
            {
                for (int i = 0; i < m_pMap.LayerCount; i++)
                {
                    ILayer pLayer = m_pMap.get_Layer(i);
                    if (pLayer is IRasterLayer)
                    {
                       // IRasterLayer prlayer = pLayer as IRasterLayer;
                        cmbLayers.Items.Add(pLayer.Name);
                    }
                }
            }
        }

        private void buttonXDEM_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.tif;*.tiff;|*.tif;*.tiff;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxXDEM.Text = dlg.FileName;
            }

        }
        private void buttonXTIN_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdlg = new SaveFileDialog();
            if (fdlg.ShowDialog() == DialogResult.OK && fdlg.FileName != "")
            {
                textBoxXTIN.Text = fdlg.FileName;
            }
        }
        private void textBoxXDEM_TextChanged(object sender, EventArgs e)
        {
            m_DEMPath = textBoxXDEM.Text;
        }

        private void textBoxXTIN_TextChanged(object sender, EventArgs e)
        {
            m_TINPath = textBoxXTIN.Text;
        }
        public void DEMToTIN(IRaster iRaster, string tinFileName)
        {
            try
            {
                //***************生成TIN模型*********************************************
                IGeoDataset pGeoData = iRaster as IGeoDataset;
                IEnvelope pExtent = pGeoData.Extent;
                IRasterBandCollection pRasBC = iRaster as IRasterBandCollection;
                IRasterBand pRasBand = pRasBC.Item(0);
                IRawPixels pRawPixels = pRasBand as IRawPixels;
                IRasterProps pProps = pRawPixels as IRasterProps;
                int iWid = pProps.Width;
                int iHei = pProps.Height;
                double w = iWid / 1000.0f;
                double h = iHei / 1000.0f;
                IPnt pBlockSize = new DblPntClass();
                bool IterationFlag;

                if (w < 1 && h < 1) //横纵都小于1000个像素
                {
                    pBlockSize.X = iWid;
                    pBlockSize.Y = iHei;
                    IterationFlag = false;
                }
                else
                {
                    pBlockSize.X = 1001.0f;
                    pBlockSize.Y = 1001.0f;
                    IterationFlag = true;
                }
                double cellsize = 0.0f; //栅格大小

                IPnt pPnt1 = pProps.MeanCellSize(); //栅格平均大小
                cellsize = pPnt1.X;
                ITinEdit pTinEdit = new TinClass() as ITinEdit;
                pTinEdit.InitNew(pExtent);
                ISpatialReference pSpatial = pGeoData.SpatialReference;
                pExtent.SpatialReference = pSpatial;
                IPnt pOrigin = new DblPntClass();
                IPnt pPixelBlockOrigin = new DblPntClass();
                //栅格左上角像素中心坐标
                double bX = pBlockSize.X;
                double bY = pBlockSize.Y;
                pBlockSize.SetCoords(bX, bY);
                IPixelBlock pPixelBlock = pRawPixels.CreatePixelBlock(pBlockSize);
                object nodata = pProps.NoDataValue; //无值标记
                ITinAdvanced2 pTinNodeCount = pTinEdit as ITinAdvanced2;
                int nodeCount = pTinNodeCount.NodeCount;
                object vtMissing = Type.Missing;
                object vPixels = null; //格子
                double m_zTolerance = 0;
                if (!IterationFlag) //当为一个处理单元格子时;(w < 1 && h < 1) //横纵都小于1000个像素
                {
                    pPixelBlockOrigin.SetCoords(0.0f, 0.0f);
                    pRawPixels.Read(pPixelBlockOrigin, pPixelBlock);
                    vPixels = pPixelBlock.get_SafeArray(0);
                    double xMin = pExtent.XMin;
                    double yMax = pExtent.YMax;
                    pOrigin.X = xMin + cellsize / 2;
                    pOrigin.Y = yMax - cellsize / 2;
                    bX = pOrigin.X;
                    bY = pOrigin.Y;
                    pTinEdit.AddFromPixelBlock(bX, bY, cellsize, cellsize, nodata, vPixels, m_zTolerance, ref vtMissing, out vtMissing);
                }
                else //当有多个处理单元格时，依次循环处理每个单元格
                {
                    int i = 0, j = 0, count = 0;
                    int FirstGoNodeCount = 0;
                    
                    //while (nodeCount != FirstGoNodeCount)
                    //{
                        count++;
                        nodeCount = pTinNodeCount.NodeCount;
                        int bwidth = 0;int  bheight = 0;
                        //依次循环处理
                        for (i = 0; i < (int)h + 1; i++)
                        {
                            if (i < (int)h)
                            {
                                bheight = 1000;
                            }
                            else
                            {
                                bheight = iHei - 1000 * i;
                            }
                            for (j = 0; j < (int)w + 1; j++)
                            {
                                if (j < (int)w)
                                {
                                    bwidth = 1000;
                                }
                                else
                                {
                                    bwidth = iWid - 1000 * j;
                                }
                                pBlockSize.SetCoords(bwidth, bheight);
                                pPixelBlock = pRawPixels.CreatePixelBlock(pBlockSize);
                                
                                
                                double bX1, bY1, xMin1, yMax1;
                                bX1 = pBlockSize.X;
                                bY1 = pBlockSize.Y;
                                pPixelBlockOrigin.SetCoords(j * 1000, i * 1000);
                                
                                pRawPixels.Read(pPixelBlockOrigin, pPixelBlock);
                                vPixels = pPixelBlock.get_SafeArray(0);
                                xMin1 = pExtent.XMin;
                                yMax1 = pExtent.YMax;
                                //bX1 = pBlockSize.X;
                                //bY1 = pBlockSize.Y;
                                pOrigin.X = xMin1 + j * 1000 * cellsize + cellsize / 2.0f;
                                pOrigin.Y = yMax1 - i * 1000 * cellsize - cellsize / 2.0f;
                                bX1 = pOrigin.X;
                                bY1 = pOrigin.Y;
                                pTinEdit.AddFromPixelBlock(bX1, bY1, cellsize, cellsize, nodata, vPixels, m_zTolerance, ref vtMissing, out vtMissing);
                                FirstGoNodeCount = pTinNodeCount.NodeCount;
                                 
                            }
                        }
                    //}
                    
                }
                //保存TIN文件
                pTinEdit.SaveAs(tinFileName, ref vtMissing);
                pTinEdit.StopEditing(true);
                m_pTin = pTinEdit as ITin;
                MessageBox.Show("转换成功！");
            }
            catch (SystemException e)
            {
                
                MessageBox.Show(e.Message);
            }
        }

        private void buttonXOK_Click(object sender, EventArgs e)
        {
            try
            {
                string dempath = m_DEMPath;
                IRaster raster = new RasterClass();
                RasterLayerClass rasterlayer = new RasterLayerClass();
                rasterlayer.CreateFromFilePath(dempath);
                IRaster iRaster = rasterlayer.Raster;
                string tinFileName = m_TINPath;
                DEMToTIN(iRaster, tinFileName);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public ITin DEMToTIN(IRaster iRaster)
        {
            try
            {
                //***************生成TIN模型*********************************************
                IGeoDataset pGeoData = iRaster as IGeoDataset;
                IEnvelope pExtent = pGeoData.Extent;
                IRasterBandCollection pRasBC = iRaster as IRasterBandCollection;
                IRasterBand pRasBand = pRasBC.Item(0);
                IRawPixels pRawPixels = pRasBand as IRawPixels;
                IRasterProps pProps = pRawPixels as IRasterProps;
                int iWid = pProps.Width;
                int iHei = pProps.Height;
                double w = iWid / 1000.0f;
                double h = iHei / 1000.0f;
                IPnt pBlockSize = new DblPntClass();
                bool IterationFlag;

                if (w < 1 && h < 1) //横纵都小于1000个像素
                {
                    pBlockSize.X = iWid;
                    pBlockSize.Y = iHei;
                    IterationFlag = false;
                }
                else
                {
                    pBlockSize.X = 1001.0f;
                    pBlockSize.Y = 1001.0f;
                    IterationFlag = true;
                }
                double cellsize = 0.0f; //栅格大小

                IPnt pPnt1 = pProps.MeanCellSize(); //栅格平均大小
                cellsize = pPnt1.X;
                ITinEdit pTinEdit = new TinClass() as ITinEdit;
                pTinEdit.InitNew(pExtent);
                ISpatialReference pSpatial = pGeoData.SpatialReference;
                pExtent.SpatialReference = pSpatial;
                IPnt pOrigin = new DblPntClass();
                IPnt pPixelBlockOrigin = new DblPntClass();
                //栅格左上角像素中心坐标
                double bX = pBlockSize.X;
                double bY = pBlockSize.Y;
                pBlockSize.SetCoords(bX, bY);
                IPixelBlock pPixelBlock = pRawPixels.CreatePixelBlock(pBlockSize);
                object nodata = pProps.NoDataValue; //无值标记
                ITinAdvanced2 pTinNodeCount = pTinEdit as ITinAdvanced2;
                int nodeCount = pTinNodeCount.NodeCount;
                object vtMissing = Type.Missing;
                object vPixels = null; //格子
                double m_zTolerance = 0;
                if (IterationFlag) //当为一个处理单元格子时
                {
                    pPixelBlockOrigin.SetCoords(0.0f, 0.0f);
                    pRawPixels.Read(pPixelBlockOrigin, pPixelBlock);
                    vPixels = pPixelBlock.get_SafeArray(0);
                    double xMin = pExtent.XMin;
                    double yMax = pExtent.YMax;
                    pOrigin.X = xMin + cellsize / 2;
                    pOrigin.Y = yMax - cellsize / 2;
                    bX = pOrigin.X;
                    bY = pOrigin.Y;
                    pTinEdit.AddFromPixelBlock(bX, bY, cellsize, cellsize, nodata, vPixels, m_zTolerance, ref vtMissing, out vtMissing);
                }
                else //当有多个处理单元格时，依次循环处理每个单元格
                {
                    int i = 0, j = 0, count = 0;
                    int FirstGoNodeCount = 0;
                    while (nodeCount != FirstGoNodeCount)
                    {
                        count++;
                        nodeCount = pTinNodeCount.NodeCount;
                        //依次循环处理
                        for (i = 0; i < (int)h + 1; i++)
                        {
                            for (j = 0; j < (int)w + 1; j++)
                            {
                                double bX1, bY1, xMin1, yMax1;
                                bX1 = pBlockSize.X;
                                bY1 = pBlockSize.Y;
                                pPixelBlockOrigin.SetCoords(j * bX1, i * bY1);
                                pRawPixels.Read(pPixelBlockOrigin, pPixelBlock);
                                vPixels = pPixelBlock.get_SafeArray(0);
                                xMin1 = pExtent.XMin;
                                yMax1 = pExtent.YMax;
                                bX1 = pBlockSize.X;
                                bY1 = pBlockSize.Y;
                                pOrigin.X = xMin1 + j * bX1 * cellsize + cellsize / 2.0f;
                                pOrigin.Y = yMax1 + i * bY1 * cellsize - cellsize / 2.0f;
                                bX1 = pOrigin.X;
                                bY1 = pOrigin.Y;
                                pTinEdit.AddFromPixelBlock(bX1, bY1, cellsize, cellsize, nodata, vPixels, m_zTolerance, ref vtMissing, out vtMissing);
                                FirstGoNodeCount = pTinNodeCount.NodeCount;
                            }
                        }
                    }
                }
                // pTinEdit.StopEditing(true);
                return pTinEdit as ITin;
            }
            catch (SystemException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private void cmbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_pMap != null)
            {
                for (int i = 0; i < m_pMap.LayerCount; i++)
                {
                    ILayer pLayer = m_pMap.get_Layer(i);
                    if (pLayer is IRasterLayer)
                    {
                        // IRasterLayer prlayer = pLayer as IRasterLayer;
                        if (pLayer.Name == cmbLayers.SelectedItem.ToString())
                        {
                            IDataLayer pDatalayer = pLayer as IDataLayer;
                            IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;
                            textBoxXDEM.Text = pDname.WorkspaceName.PathName + pDname.Name;
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
