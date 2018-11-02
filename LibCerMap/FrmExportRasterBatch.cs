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

namespace LibCerMap
{
    public partial class FrmExportRasterBatch : OfficeForm
    {
        private IMapControl3 m_pMapControl;

        public FrmExportRasterBatch(IMapControl3 pMapControl)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pMapControl = pMapControl;
        }

        private void rdoLayer_CheckedChanged(object sender, EventArgs e)
        {
            rdoWorkspace.Checked = !rdoLayer.Checked;
        }

        private void rdoWorkspace_CheckedChanged(object sender, EventArgs e)
        {
            rdoLayer.Checked = !rdoWorkspace.Checked;
        }

        private void BtnWorkBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            fdlg.SelectedPath = @"D:\CE3Map";
            if (fdlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (fdlg.SelectedPath != "")
            {
                //DirectoryInfo dir = Directory.CreateDirectory(fdlg.SelectedPath);
                txtOutData.Text = fdlg.SelectedPath;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ExportToTif();
        }

        private void ExportToTif()
        {
            string strFloder = txtOutData.Text;
            for (int i = 0; i < m_pMapControl.Map.LayerCount; i++)
            {
                if (m_pMapControl.Map.get_Layer(i) is IRasterLayer)
                {
                    //导入数据
                    IRasterLayer pRLayer = m_pMapControl.Map.get_Layer(i) as IRasterLayer;
                    IRaster2 pRaster = pRLayer.Raster as IRaster2;
                    IRawBlocks pRawBlocks = pRaster as IRawBlocks;
                    IRasterInfo pRInfo = pRawBlocks.RasterInfo;
                    IPnt pPnt = pRInfo.CellSize;
                    double dcellSize = pPnt.X;
                    double dcellsizeY = pPnt.Y;
                    string sCellSize = Convert.ToInt32(dcellSize * 10).ToString();
                    string strFileName = "R" + sCellSize + "_" + pRLayer.Name;
                    string strFullName = txtOutData.Text + "\\" + strFileName;
                    //设置空间参考
                    ISpatialReference pSpatialRef;
                    if (rdoLayer.Checked)//与原图相同
                    {
                        IGeoDataset pGeo = (IGeoDataset)pRLayer;
                        pSpatialRef = pGeo.SpatialReference;
                    }
                    else//与工作空间相同
                    {
                        pSpatialRef = m_pMapControl.Map.SpatialReference;
                    }

                    try
                    {
                        //IRasterLayerExport pRLayerExport = new RasterLayerExportClass();
                        //pRLayerExport.RasterLayer = pRLayer;
                        //pRLayerExport.SpatialReference = pSpatialRef;
                        //pRLayerExport.SetSize(pRLayer.ColumnCount, pRLayer.RowCount);
                        IWorkspaceFactory pWSF = new RasterWorkspaceFactoryClass();
                        IWorkspace pWS = pWSF.OpenFromFile(txtOutData.Text, 0);
                        //IRasterDataset pRDset = pRLayerExport.Export(pWS, strFileName, "TIFF");
                        //System.Runtime.InteropServices.Marshal.ReleaseComObject(pRDset);

                        IRasterProps pRasterProps = pRaster as IRasterProps;
                        //pRasterProps.PixelType = pRInfo.PixelType;
                        //投影发生变化，栅格的分辨率会发生变化
                        pRasterProps.SpatialReference = pSpatialRef;
                        IRawBlocks pRBlocks = pRasterProps as IRawBlocks;
                        IRasterInfo pRaInfo = pRBlocks.RasterInfo;
                        //计算出重投影之后对应的栅格的行数和列数，行列数改变之后，它对应的栅格的分辨率也会变化为原始栅格的分辨率
                        //如果没有下面的计算行列的步骤，重投影之后的分辨率和原始影响的分辨率不同，行列数相同
                        pRasterProps.Width = Convert.ToInt32(pRasterProps.MeanCellSize().X * pRLayer.ColumnCount / dcellSize);
                        pRasterProps.Height = Convert.ToInt32(pRasterProps.MeanCellSize().Y * pRLayer.RowCount / dcellsizeY);
                        IEnvelope pEnvelope = new EnvelopeClass();
                        pEnvelope.XMin = pRasterProps.Extent.UpperLeft.X;
                        pEnvelope.YMax = pRasterProps.Extent.UpperLeft.Y;
                        pEnvelope.XMax= pRasterProps.Extent.UpperLeft.X + pRasterProps.Width * dcellSize;
                        pEnvelope.YMin = pRasterProps.Extent.UpperLeft.Y - pRasterProps.Height * dcellsizeY;
                        pRasterProps.Extent = pEnvelope;

                        ISaveAs2 pSaveAs = pRasterProps as ISaveAs2;
                        IRasterStorageDef pRSDef = new RasterStorageDefClass();
                        //将存储栅格的分辨率设置为与原始图像相同，其实经过上面行列计算之后已经是相同的
                        IPnt pPntdec = new PntClass();
                        pPntdec.X = dcellSize;
                        pPntdec.Y = dcellsizeY;
                        pRSDef.CellSize = pPntdec;

                        IRasterDataset pDataset = pSaveAs.SaveAsRasterDataset(strFileName, pWS, "TIFF", pRSDef);
                        pDataset.PrecalculateStats(0);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
                        
                    }
                    catch (Exception exc)
                    {
                        //MessageBox.Show(exc.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            this.Close();
        }
        
    }
}
