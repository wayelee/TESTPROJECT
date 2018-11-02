using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
    public partial class FrmTinToDEM : OfficeForm
    {
        public FrmTinToDEM()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public string m_DEMPath;
        public string m_TINPath;
        public IRasterDataset m_pRasterDataset = null;
        public ITin m_pTin;
        public  IMap m_pMap = null;
        private void buttonXTIN_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            if (fdlg.ShowDialog() == DialogResult.OK && fdlg.SelectedPath != "")
            {
                textBoxXTIN.Text = fdlg.SelectedPath;
            }
        }
        private void buttonXDEM_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.tif;*.tiff;)|*.tif;*.tiff;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxXDEM.Text = dlg.FileName;
            }
        }

        private void buttonXOK_Click(object sender, EventArgs e)
        {
            try
            {

                ITinAdvanced ptina = m_pTin as ITinAdvanced;
                // DirectoryInfo dir = Directory.CreateDirectory(m_DEMPath);
                DirectoryInfo dir = new DirectoryInfo(m_DEMPath);
                String sdir = dir.Parent.FullName;
                String name = dir.Name;

                //IRasterDataset rd1 = CreateRasterDataset(@"d:\dem", "aa.tif");
               // IRasterDataset rd =  CreateRasterDataset(sdir, name);
                
               
                //esriRasterizationType eRastConvType = esriRasterizationType.esriElevationAsRaster
              IRasterDataset pRD =   TinToRaster_new(ptina, esriRasterizationType.esriElevationAsRaster, sdir, name, rstPixelType.PT_DOUBLE, doubleInputCellSize.Value, m_pTin.Extent, true);
                // TinToRaster_new(ITinAdvanced pTin, esriRasterizationType eRastConvType, String sDir, String sName, rstPixelType ePixelType, Double cellsize, IEnvelope pExtent, bool bPerm)
              pRD = null;
              GC.Collect();
              MessageBox.Show("转换成功！");
                
            }
            catch(SystemException ee)
            {
                m_DEMPath = "";
                MessageBox.Show(ee.Message);
            }
        }

        private void textBoxXTIN_TextChanged(object sender, EventArgs e)
        {
            m_TINPath = textBoxXTIN.Text;
            try
            {
                DirectoryInfo dir = Directory.CreateDirectory(m_TINPath);
                IWorkspaceFactory pWSFact = new TinWorkspaceFactoryClass();
                IWorkspace pWS = pWSFact.OpenFromFile(dir.Parent.FullName + @"\", 0);
                ITinWorkspace pTinWS = pWS as ITinWorkspace;
                ITin pTin = pTinWS.OpenTin(dir.Name);
                m_pTin = pTin;
                IEnvelope pExtent = pTin.Extent;
                double dmaxextent = pExtent.Height;
                if (pExtent.Width > pExtent.Height)
                {
                    dmaxextent = pExtent.Width;
                }
                double cellsize = dmaxextent / 250;
                doubleInputCellSize.Value = cellsize;
            }
            catch (SystemException ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }

        private void textBoxXDEM_TextChanged(object sender, EventArgs e)
        {
            m_DEMPath = textBoxXDEM.Text;
        }
        public IRasterDataset TinToRaster_new(ITinAdvanced pTin, esriRasterizationType eRastConvType, String sDir, String sName, rstPixelType ePixelType, Double cellsize, IEnvelope pExtent, bool bPerm)
        {
            IPoint pOrigin = pExtent.LowerLeft; 
            //pOrigin.X = pOrigin.X - (cellsize * 0.5);
            //pOrigin.Y = pOrigin.Y - (cellsize * 0.5);
            pOrigin.X = pOrigin.X ;
            pOrigin.Y = pOrigin.Y;
            int nCol, nRow;
            nCol = (int)Math.Round(pExtent.Width / cellsize) ;
            nRow = (int)Math.Round(pExtent.Height / cellsize) ;         
            IGeoDataset pGDS = pTin as IGeoDataset;
            ISpatialReference2 pSR = pGDS.SpatialReference as ISpatialReference2;
            //这个pOrigin为栅格左下角
            IWorkspaceFactory pworkspaceFactory = new RasterWorkspaceFactory();
           IRasterWorkspace2  rasterws = pworkspaceFactory.OpenFromFile(sDir, 0) as IRasterWorkspace2;
            IPoint originpoint = pOrigin;

            //用于计算的 float型的栅格数据
            IRasterDataset demdataset = rasterws.CreateRasterDataset(sName, "TIFF", originpoint, nCol, nRow,
                cellsize, cellsize, 1, rstPixelType.PT_DOUBLE, null, true);
            
            IRasterDataset pRDS = demdataset;

            //IRawPixels pRawPixels = GetRawPixels(pRDS, 0);
            IRaster pRaster = pRDS.CreateDefaultRaster();
            IPnt pBlockSize = new DblPnt();
            //nCol = 50;
            //nRow = 50;
            pBlockSize.X = nCol;
            pBlockSize.Y = nRow; 
            IPixelBlock pPixelBlock = pRaster.CreatePixelBlock(pBlockSize);
            //IPixelBlock pPixelBlock = pRawPixels.CreatePixelBlock(pBlockSize);
            IPixelBlock3 pPixelBlock3 = pPixelBlock as IPixelBlock3;
          
            //object val = pPixelBlock.get_SafeArray(0);
            ITinSurface pTinSurf = pTin as ITinSurface;
           // IRasterProps pRasterProps = pRawPixels as IRasterProps;
            IRasterProps pRasterProps = pRaster as IRasterProps;
            object nodata;
            //pOrigin.X = pOrigin.X + (cellsize * 0.5);
            //pOrigin.Y = pOrigin.Y + (cellsize * nRow) - (cellsize * 0.5);
            pOrigin.X = pOrigin.X  ; 
            pOrigin.Y = pOrigin.Y + (cellsize * nRow) ;
            nodata = pRasterProps.NoDataValue;
            IGeoDatabaseBridge2 pbridge2 = (IGeoDatabaseBridge2)new GeoDatabaseHelperClass();
            //这个pOrigin为栅格左上角
            //pbridge2.QueryPixelBlock(pTinSurf, pOrigin.X, pOrigin.Y, cellsize, cellsize, esriRasterizationType.esriElevationAsRaster, nodata, ref val);
            //if (pTin.ProcessCancelled)
            //    return null;
            //val.GetType();
            CalPixelArray(pTinSurf, pOrigin.X, pOrigin.Y, cellsize, cellsize, ref pPixelBlock3);
            IPnt pOffset = new DblPnt();
            pOffset.X = 0;
            pOffset.Y = 0;
            //pPixelBlock3.set_PixelData(0, val);
            //pRawPixels.Write(pOffset, (IPixelBlock)pPixelBlock3);//写入硬盘
            IRasterEdit prasteredit = pRaster as IRasterEdit;
            prasteredit.Write(pOffset, (IPixelBlock)pPixelBlock3);
            //pRDS = OpenOutputRasterDataset(sDir, sName);
           
            //IPixelBlock pb = pRaster.CreatePixelBlock(pBlockSize);
            //pRaster.Read(pOffset,pb);

           // ISaveAs pSaveas = pRasterProps as ISaveAs2;
           // pSaveas.SaveAs(sDir + "\\" + sName, null, "TIFF");

            prasteredit.Refresh(); 


            return pRDS;
        }
       
        //计算栅格数据块的高程值
        public void CalPixelArray(ITinSurface pTinSurf, double UpLeftX, double UpLeftY, double XCellsize, double YCellsize, ref IPixelBlock3 pixelblock)
        {
            System.Array pArray = pixelblock.get_PixelData(0) as System.Array;
            for(int i = 0 ; i< pixelblock.Width; i++)
            {
                for (int j = 0; j < pixelblock.Height; j++)
                {
                    double dheight = (double)pTinSurf.get_Z(UpLeftX + i * XCellsize, UpLeftY - j * YCellsize);
                    pArray.SetValue(dheight, i, j);
                }
            }
            pixelblock.set_PixelData(0, pArray);
        }

        public IRasterDataset CreateRasterSurf(string sDir, string sName, string sFormat, IPoint pOrigin, int nCol, int nRow, double cellsizeX, double cellsizeY, rstPixelType ePixelType, ISpatialReference2 pSR, bool bPerm)
        {

            //IWorkspaceFactory rWksFac = new RasterWorkspaceFactory();

            //IWorkspace wks = rWksFac.OpenFromFile(sDir, 0);

            //IRasterWorkspace2 rWks = wks as IRasterWorkspace2;

            //int numbands = 1;

            //IRasterDataset pRDS;// = new RasterDatasetClass();
            //pRDS = rWks.CreateRasterDataset(sName, sFormat, pOrigin, nCol, nRow, cellsizeX, cellsizeY, numbands, ePixelType, pSR, bPerm);
            //return pRDS;

            IWorkspaceFactory pworkspaceFactory = new RasterWorkspaceFactory();
            ESRI.ArcGIS.Geodatabase.IWorkspaceName pworkspaceName = pworkspaceFactory.Create(null, "MyWorkspace", null, 0);
            ESRI.ArcGIS.esriSystem.IName pname = (IName)pworkspaceName;
            ESRI.ArcGIS.Geodatabase.IWorkspace inmemWor = (IWorkspace)pname.Open();
            IPoint originpoint = pOrigin;
            IRasterWorkspace2 rasterws = (IRasterWorkspace2)inmemWor;

            //用于计算的 float型的栅格数据
            IRasterDataset demdataset = rasterws.CreateRasterDataset("Dataset", "MEM", originpoint, nCol, nRow,
                (double)cellsizeX, (double)cellsizeY, 1, rstPixelType.PT_DOUBLE, null, true);
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pname);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pworkspaceFactory);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pworkspaceName);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(inmemWor);
            }
            catch { }
            GC.Collect();
            return demdataset;
        }

        public IRawPixels GetRawPixels(IRasterDataset pRDS, int band)
        {
            IRawPixels pRP;
            IRasterBandCollection pBandCollection = pRDS as IRasterBandCollection;

            IRasterBand pRasterBand = pBandCollection.Item(band);

            pRP = pRasterBand as IRawPixels;
            return pRP;

        } 
        private void FrmTinToDEM_Load(object sender, EventArgs e)
        {
            if (m_pMap != null)
            {
                for (int i = 0; i < m_pMap.LayerCount; i++)
                {
                    ILayer pLayer = m_pMap.get_Layer(i);
                    if (pLayer is ITinLayer)
                    {
                        // IRasterLayer prlayer = pLayer as IRasterLayer;
                        cmbLayers.Items.Add(pLayer.Name);
                    }
                }
            }
        }

        private void textBoxXDEM_MouseClick(object sender, MouseEventArgs e)
        {
             
        }
        public IRaster TINToDEM(ITin pTin)
        {
            
            IPoint pOrigin = pTin.Extent.LowerLeft; 
            

             IWorkspaceFactory pworkspaceFactory = new RasterWorkspaceFactory();
            ESRI.ArcGIS.Geodatabase.IWorkspaceName pworkspaceName = pworkspaceFactory.Create(null, "MyWorkspace", null, 0);
            ESRI.ArcGIS.esriSystem.IName pname = (IName)pworkspaceName;
            ESRI.ArcGIS.Geodatabase.IWorkspace inmemWor = (IWorkspace)pname.Open();
            IPoint originpoint = pOrigin;
            IRasterWorkspace2 rasterws = (IRasterWorkspace2)inmemWor;

            int nCol,nRow;
            nCol = 500;
            nRow = 500;
            double cellsizeX,cellsizeY;
            cellsizeX = pTin.Extent.Width / nCol;
            cellsizeY = pTin.Extent.Height / nRow;

            //用于计算的 float型的栅格数据
            IRasterDataset demdataset = rasterws.CreateRasterDataset("Dataset", "MEM", originpoint, nCol, nRow,
                (double)cellsizeX, (double)cellsizeY, 1, rstPixelType.PT_DOUBLE, null, true);
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pname);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pworkspaceFactory);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pworkspaceName);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(inmemWor);
            }
            catch { }

              //IRawPixels pRawPixels = GetRawPixels(pRDS, 0);
            IRaster pRaster = demdataset.CreateDefaultRaster();
            IPnt pBlockSize = new DblPnt();
            //nCol = 50;
            //nRow = 50;
            pBlockSize.X = nCol;
            pBlockSize.Y = nRow; 
            IPixelBlock pPixelBlock = pRaster.CreatePixelBlock(pBlockSize);
            //IPixelBlock pPixelBlock = pRawPixels.CreatePixelBlock(pBlockSize);
            IPixelBlock3 pPixelBlock3 = pPixelBlock as IPixelBlock3;
          
            //object val = pPixelBlock.get_SafeArray(0);
            ITinSurface pTinSurf = pTin as ITinSurface;
           // IRasterProps pRasterProps = pRawPixels as IRasterProps;
            IRasterProps pRasterProps = pRaster as IRasterProps;
            object nodata;
            //pOrigin.X = pOrigin.X + (cellsize * 0.5);
            //pOrigin.Y = pOrigin.Y + (cellsize * nRow) - (cellsize * 0.5);
            pOrigin.X = pOrigin.X  ; 
            pOrigin.Y = pOrigin.Y + (cellsizeY * nRow) ;
            nodata = pRasterProps.NoDataValue;
            IGeoDatabaseBridge2 pbridge2 = (IGeoDatabaseBridge2)new GeoDatabaseHelperClass();
            //这个pOrigin为栅格左上角
            //pbridge2.QueryPixelBlock(pTinSurf, pOrigin.X, pOrigin.Y, cellsize, cellsize, esriRasterizationType.esriElevationAsRaster, nodata, ref val);
            //if (pTin.ProcessCancelled)
            //    return null;
            //val.GetType();
            CalPixelArray(pTinSurf, pOrigin.X, pOrigin.Y, cellsizeX, cellsizeY, ref pPixelBlock3);
            IPnt pOffset = new DblPnt();
            pOffset.X = 0;
            pOffset.Y = 0;
            //pPixelBlock3.set_PixelData(0, val);
            //pRawPixels.Write(pOffset, (IPixelBlock)pPixelBlock3);//写入硬盘
            IRasterEdit prasteredit = pRaster as IRasterEdit;
            prasteredit.Write(pOffset, (IPixelBlock)pPixelBlock3);
            //pRDS = OpenOutputRasterDataset(sDir, sName);
           
            //IPixelBlock pb = pRaster.CreatePixelBlock(pBlockSize);
            //pRaster.Read(pOffset,pb);  
            return pRaster;
        }

        private void cmbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_pMap != null)
            {
                for (int i = 0; i < m_pMap.LayerCount; i++)
                {
                    ILayer pLayer = m_pMap.get_Layer(0);
                    if (pLayer is ITinLayer)
                    {
                        // IRasterLayer prlayer = pLayer as IRasterLayer;
                        if (pLayer.Name == cmbLayers.SelectedItem.ToString())
                        {
                            IDataLayer pDatalayer = pLayer as IDataLayer;
                            IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;
                            textBoxXTIN.Text = pDname.WorkspaceName.PathName + pDname.Name;
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
