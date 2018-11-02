using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

using DevComponents.DotNetBar;
using ESRI.ArcGIS.esriSystem;

namespace LibCerMap
{
    public partial class FrmExportRaster : OfficeForm
    {
        private ILayer m_pLayer;
        private IBasicMap m_pMap;

        //记录原始图像属性
        //double m_dCellSizeX;//原始图像的X分辨率
        //double m_dCellsizeY;//原始图像的Y分辨率
        int m_nCols;//原始图像的列数
        int m_nRows;//原始图像的行数

        IRasterLayer m_pRasterLayer;
        IRaster2 m_pRaster;
        //IRawBlocks pRawBlocks;
        //IRasterInfo pRInfo;
        //IPnt pPnt;
        IEnvelope m_Envelope = new EnvelopeClass();

        //设置空间参考
        ISpatialReference m_pSpatialRef;//
        IRasterProps m_pRasterProps;//导出数据属性

        public FrmExportRaster(IBasicMap map, ILayer pLayer)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pLayer = pLayer;
            m_pMap = map;
        }

        private void FrmExportRaster_Load(object sender, EventArgs e)
        {
            m_pRasterLayer = m_pLayer as IRasterLayer;
            IRaster2 pRasterOrg = m_pRasterLayer.Raster as IRaster2;

            IClone pClone = pRasterOrg as IClone;
            IClone cloneDest = pClone.Clone();
            m_pRaster = cloneDest as IRaster2;
            m_pSpatialRef = ((IGeoDataset)m_pRasterLayer).SpatialReference;
            m_pRasterProps = m_pRaster as IRasterProps;

            //更新图层范围
            SetLayerExtent(m_pRasterProps.MeanCellSize().X, m_pRasterProps.MeanCellSize().Y);

            m_Envelope = m_pRasterProps.Extent;
            m_nRows = m_pRasterProps.Height;
            m_nCols = m_pRasterProps.Width;

            InitialData(m_pRasterProps);

            rdoLayer.Checked = true;
            if (m_pMap.SpatialReference == null)
            {
                rdoWorkspace.Enabled = false;
            }

            IRasterBandCollection pRB = m_pRaster.RasterDataset as IRasterBandCollection;
            comboBoxExBands.Items.Add("所有波段");
            for (int i = 0; i < pRB.Count; i++)
            {
                comboBoxExBands.Items.Add("Band" + (i + 1).ToString());
            }
            comboBoxExBands.SelectedIndex = 0;
            //SetMeanCellSize();
            chkSocCellSize.Checked = true;
            //设置缺少目录

        }

        private void InitialData(IRasterProps rasterProps)
        {
            //int nCurrentType = -1;
            for (int i = 0; i < 15; i++)
            {
                string strPixelType = ((rstPixelType)i).ToString();
                cmbPixelType.Items.Add(strPixelType);
            }
            cmbPixelType.Text = rasterProps.PixelType.ToString();

            //初始化采样方式
            for (int i = 0; i < 4; i++)
            {
                string strResampleType = ((rstResamplingTypes)i).ToString().Replace("RSP_", "");
                cmbResample.Items.Add(strResampleType);
            }
            cmbResample.SelectedIndex = 1;
            
            rdoLayer_Click(null, null);
        }

        private void rdoLayer_Click(object sender, EventArgs e)
        {
            rdoLayer.Checked = true;

            m_pRasterProps.SpatialReference = m_pSpatialRef;
            SetMeanCellSize();

        }

        private void rdoLayer_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoWorkspace_Click(object sender, EventArgs e)
        {
            rdoWorkspace.Checked = true;
            m_pRasterProps.SpatialReference = m_pMap.SpatialReference;
            SetMeanCellSize();
        }

        private void rdoWorkspace_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnWorkBrowse_Click(object sender, EventArgs e)
        {
            saveFileDlg.Filter = "*.tif|*.tif";
            saveFileDlg.FileName = m_pLayer.Name;
            saveFileDlg.OverwritePrompt = false;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDlg.FileName))
                {
                    if (MessageBox.Show("同名文件已存在，是否覆盖？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        try
                        {
                            File.Delete(saveFileDlg.FileName);
                            txtOutData.Text = saveFileDlg.FileName;
                        }
                        catch
                        {
                            MessageBox.Show("文件覆盖失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    txtOutData.Text = saveFileDlg.FileName;
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                m_pRasterProps.SpatialReference = m_pSpatialRef;
                m_pRasterProps.Extent = m_Envelope;
            }
            catch (System.Exception ex)
            {

            }

            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ExportToTif())
            {
                this.Close();
            }
        }

        #region 得到存储位数和重采样方式
        //private rstPixelType GetRasterPixelType(string strType)
        //{
        //    if (strType == rstPixelType.PT_CHAR.ToString())
        //    {
        //        return rstPixelType.PT_CHAR;
        //    }
        //    else if (strType == rstPixelType.PT_UCHAR.ToString())
        //    {
        //        return rstPixelType.PT_UCHAR;
        //    }
        //    else if (strType == rstPixelType.PT_SHORT.ToString())
        //    {
        //        return rstPixelType.PT_SHORT;
        //    }
        //    else if (strType == rstPixelType.PT_USHORT.ToString())
        //    {
        //        return rstPixelType.PT_USHORT;
        //    }
        //    else if (strType == rstPixelType.PT_LONG.ToString())
        //    {
        //        return rstPixelType.PT_LONG;
        //    }
        //    else if (strType == rstPixelType.PT_ULONG.ToString())
        //    {
        //        return rstPixelType.PT_ULONG;
        //    }
        //    else if (strType == rstPixelType.PT_CSHORT.ToString())
        //    {
        //        return rstPixelType.PT_CSHORT;
        //    }
        //    else if (strType == rstPixelType.PT_CLONG.ToString())
        //    {
        //        return rstPixelType.PT_CLONG;
        //    }
        //    else if (strType == rstPixelType.PT_FLOAT.ToString())
        //    {
        //        return rstPixelType.PT_FLOAT;
        //    }
        //    else if (strType == rstPixelType.PT_DOUBLE.ToString())
        //    {
        //        return rstPixelType.PT_DOUBLE;
        //    }

        //    return rstPixelType.PT_CHAR;
        //}
        //private rstResamplingTypes GetRasterResample(string strType)
        //{
        //    if (strType == "NearestNeighbor")
        //    {
        //        return rstResamplingTypes.RSP_NearestNeighbor;
        //    }
        //    else if (strType == "BilinearInterpolation")
        //    {
        //        return rstResamplingTypes.RSP_BilinearInterpolation;
        //    }
        //    else if (strType == "CubicConvolution")
        //    {
        //        return rstResamplingTypes.RSP_CubicConvolution;
        //    }
        //    else if (strType == "Majority")
        //    {
        //        return rstResamplingTypes.RSP_Majority;
        //    }
        //    return rstResamplingTypes.RSP_NearestNeighbor;
        //}

        #endregion

        private bool ExportToTif()
        {
            if (!txtOutData.Text.EndsWith("tif"))
            {
                MessageBox.Show("输出文件名不是tif文件!");
                return false;
            }

            String strFullName = txtOutData.Text;
            string strPath = System.IO.Path.GetDirectoryName(strFullName);//导出文件路径
            string strName = System.IO.Path.GetFileName(strFullName);//导出文件名

            try
            {
                IRasterBandCollection bandsOut = m_pRaster as IRasterBandCollection;
                IRasterBandCollection rasterBands = m_pRaster.RasterDataset as IRasterBandCollection;
                double[] dNodata;
                int nBandOut = 1;
                //IRaster pRasterOut = null;
                IRasterBand pBand = null;

                if (comboBoxExBands.SelectedIndex == 0)//所有波段
                {
                    //添加其他波段
                    for (int i = 3; i < rasterBands.Count; i++)
                    {
                        pBand = rasterBands.Item(i);
                        bandsOut.AppendBand(pBand);
                    }
                    nBandOut = rasterBands.Count;
                }
                else
                {
                    #region 原代码
                    //pRasterOut = new RasterClass();
                    //IRasterBandCollection pRB2 = pRasterOut as IRasterBandCollection;
                    //pRB2.AppendBand(rasterBands.Item(comboBoxExBands.SelectedIndex - 1));
                    //m_pRasterProps = pRasterOut as IRasterProps;
                    #endregion
                    //导出单波段时，不能用Clear()，会清除图层的几何校正属性
                    int nOut = bandsOut.Count;
                    for (int i = 0; i < nOut; i++)
                    {
                        bandsOut.Remove(i);
                    }
                    pBand = rasterBands.Item(comboBoxExBands.SelectedIndex - 1);
                    bandsOut.AppendBand(pBand);
                }
                //重新设置NoData
                dNodata = new double[nBandOut];
                if (!string.IsNullOrEmpty(textNoData.Text))
                {
                    for (int i = 0; i < nBandOut; i++)
                    {
                        dNodata[i] = Convert.ToDouble(textNoData.Text);
                    }
                    m_pRasterProps.NoDataValue = dNodata;
                }
                //else
                //{
                //    for (int i = 0; i < nBandOut; i++)
                //    {
                //        dNodata[i] = ClsGDBDataCommon.getNoDataValue((rasterBands.Item(i) as IRasterProps).NoDataValue);// Convert.ToDouble((rasterBands.Item(i) as IRasterProps).NoDataValue);
                //    }
                //    m_pRasterProps.NoDataValue = dNodata;
                //}
                

                IWorkspaceFactory pWSF = new RasterWorkspaceFactoryClass();
                IWorkspace pWS = pWSF.OpenFromFile(System.IO.Path.GetDirectoryName(txtOutData.Text), 0);

                //导出时要保持分辨率和行列数
                m_pRasterProps.Width = Convert.ToInt32(txtOutColumns.Text);
                m_pRasterProps.Height = Convert.ToInt32(txtOutRows.Text);
                double dcellSizeX = double.Parse(txtCellSizeX.Text);
                double dcellSizeY = double.Parse(txtCellSizeY.Text);

                IEnvelope pEnvelope = new EnvelopeClass();
                pEnvelope.XMin = m_pRasterProps.Extent.UpperLeft.X;
                pEnvelope.YMax = m_pRasterProps.Extent.UpperLeft.Y;
                pEnvelope.XMax = m_pRasterProps.Extent.UpperLeft.X + m_pRasterProps.Width * dcellSizeX;
                pEnvelope.YMin = m_pRasterProps.Extent.UpperLeft.Y - m_pRasterProps.Height * dcellSizeY;
                m_pRasterProps.Extent = pEnvelope;
                //设置存储位数
                m_pRasterProps.PixelType = (rstPixelType)cmbPixelType.SelectedIndex;

                ISaveAs2 pSaveAs = m_pRasterProps as ISaveAs2;

                IRasterStorageDef pRSDef = new RasterStorageDefClass();
                IRasterStorageDef2 pRsDef2 = pRSDef as IRasterStorageDef2;
                //将存储栅格的分辨率设置为与原始图像相同，其实经过上面行列计算之后已经是相同的
                IPnt pPntdec = new PntClass();
                pPntdec.X = dcellSizeX;
                pPntdec.Y = dcellSizeY;
                pRsDef2.CellSize = pPntdec;
                pRsDef2.PyramidResampleType = (rstResamplingTypes)cmbResample.SelectedIndex;
                IRasterDataset pDataset = pSaveAs.SaveAsRasterDataset(System.IO.Path.GetFileName(txtOutData.Text), pWS, "TIFF", pRSDef);
                IRasterDatasetEdit3 rasterEdit3 = pDataset as IRasterDatasetEdit3;
                //rasterEdit3.DeleteStats();//This method is avaliable only on raster datasets in File and ArcSDE geodatabases.
                rasterEdit3.ComputeStatisticsHistogram(1, 1, null, true);


                //导出数据之后要恢复图像的原始属性
                m_pRasterProps.SpatialReference = m_pSpatialRef;
                m_pRasterProps.Extent = m_Envelope;
                m_pRasterProps.Width = m_nCols;
                m_pRasterProps.Height = m_nRows;
                //加到当前地图中
                IRasterLayer layerNew = new RasterLayerClass();
                layerNew.CreateFromDataset(pDataset);
                m_pMap.AddLayer(layerNew);
                IActiveView activeView = m_pMap as IActiveView;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }

        private void chkCellSize_CheckedChanged(object sender, EventArgs e)
        {
            chkRasterSize.Checked = !chkCellSize.Checked;
            txtCellSizeX.Enabled = chkCellSize.Checked;
            txtCellSizeY.Enabled = chkCellSize.Checked;
            txtOutColumns.Enabled = !chkCellSize.Checked;
            txtOutRows.Enabled = !chkCellSize.Checked;
        }

        private void chkRasterSize_CheckedChanged(object sender, EventArgs e)
        {
            chkCellSize.Checked = !chkRasterSize.Checked;
        }

        private void txtCellSizeX_TextChanged(object sender, EventArgs e)
        {
            IsNumberic isnum = new IsNumberic();
            if (isnum.IsNumber(txtCellSizeX.Text) && chkCellSize.Checked)
            {
                double cx = Convert.ToDouble(txtCellSizeX.Text);
                //double ColumnOut = m_pRasterProps.Extent.Width / cx;
                txtOutColumns.Text = (Convert.ToInt32(m_pRasterProps.Extent.Width / cx)).ToString();
            }
        }

        private void txtCellSizeY_TextChanged(object sender, EventArgs e)
        {
            IsNumberic isnum = new IsNumberic();
            if (isnum.IsNumber(txtCellSizeY.Text) && chkCellSize.Checked)
            {
                double cy = Convert.ToDouble(txtCellSizeY.Text);
                //double RowOut = m_pRasterProps.Extent.Height / cy;
                txtOutRows.Text = (Convert.ToInt32(m_pRasterProps.Extent.Height / cy)).ToString();
            }
        }

        private void txtOutColumns_TextChanged(object sender, EventArgs e)
        {
            IsNumberic isnum = new IsNumberic();
            if (isnum.IsNumber(txtOutColumns.Text) && chkRasterSize.Checked)
            {
                double columnCount = Convert.ToDouble(txtOutColumns.Text);
                double cx = m_pRasterProps.Extent.Width / columnCount;
                txtCellSizeX.Text = cx.ToString();
            }
        }

        private void txtOutRows_TextChanged(object sender, EventArgs e)
        {
            IsNumberic isnum = new IsNumberic();
            if (isnum.IsNumber(txtOutRows.Text) && chkRasterSize.Checked)
            {
                double RowsCount = Convert.ToDouble(txtOutRows.Text);
                double cy = m_pRasterProps.Extent.Height / RowsCount;
                txtCellSizeY.Text = cy.ToString();
            }
        }

        private void chkSocCellSize_CheckedChanged(object sender, EventArgs e)
        {
            SetMeanCellSize();
        }

        private void SetMeanCellSize()
        {
            if (chkSocCellSize.Checked)
            {
                IRasterBandCollection rasterBands = m_pRaster.RasterDataset as IRasterBandCollection;
                if (rasterBands.Count < 1) return;
                IRasterProps props = rasterBands.Item(0) as IRasterProps;

                //更改分辨率首先要修改图层范围
                if (SetLayerExtent(props.MeanCellSize().X, props.MeanCellSize().Y))
                {
                    txtCellSizeX.Text = props.MeanCellSize().X.ToString();
                    txtCellSizeY.Text = props.MeanCellSize().Y.ToString();
                }
            }
            else
            {
                if (SetLayerExtent(m_pRasterProps.MeanCellSize().X, m_pRasterProps.MeanCellSize().Y))
                {
                    txtCellSizeX.Text = m_pRasterProps.MeanCellSize().X.ToString();
                    txtCellSizeY.Text = m_pRasterProps.MeanCellSize().Y.ToString();
                }
            }

        }

        private bool SetLayerExtent(double dcellSizeX, double dcellSizeY)
        {
            if (rdoLayer.Checked)
            {
                m_pRasterProps.SpatialReference = m_pSpatialRef;
            }
            if (rdoWorkspace.Checked)
            {
                m_pRasterProps.SpatialReference = m_pMap.SpatialReference;
            }
            //首先根据当前的分辨率设置图层的行列数
            int nW = Convert.ToInt32(m_pRasterProps.Extent.Width / dcellSizeX);
            int nH = Convert.ToInt32(m_pRasterProps.Extent.Height / dcellSizeY);
            if (nW > 0 && nH > 0)
            {
                m_pRasterProps.Width = nW;
                m_pRasterProps.Height = nH;

                IEnvelope pEnvelope = new EnvelopeClass();
                pEnvelope.XMin = m_pRasterProps.Extent.UpperLeft.X;
                pEnvelope.YMax = m_pRasterProps.Extent.UpperLeft.Y;
                pEnvelope.XMax = m_pRasterProps.Extent.UpperLeft.X + m_pRasterProps.Width * dcellSizeX;
                pEnvelope.YMin = m_pRasterProps.Extent.UpperLeft.Y - m_pRasterProps.Height * dcellSizeY;
                m_pRasterProps.Extent = pEnvelope;
                return true;
            }
            return false;
        }
    }

 
}
