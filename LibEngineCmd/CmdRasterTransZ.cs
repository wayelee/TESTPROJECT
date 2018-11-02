using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using LibCerMap;
using ESRI.ArcGIS.Display;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdRasterTransZ.
    /// </summary>
    [Guid("b90b719f-f7f3-4211-8ab4-41d978b06ff6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdRasterTransZ")]
    public sealed class CmdRasterTransZ : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper;
        private ITOCControl m_axTocControl = null;
        
        //目标图层
        public IRasterLayer pRasterLayer;
           
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
        //        double dbNoData = Convert.ToDouble(((float[])pNodata)[0]);

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

        //                        pixels.SetValue(ob, i, j);
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

        //        pRasterEdit.Refresh();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);
        //    }
        //}


        public CmdRasterTransZ(ITOCControl tocControl)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "栅格图Z值变换";  //localizable text
            base.m_message = "栅格图Z值变换";  //localizable text 
            base.m_toolTip = "栅格图Z值变换";  //localizable text 
            base.m_name = "CustomCE.CmdRasterTransZ";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            m_axTocControl = tocControl;
            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add other initialization code

        }

        /// <summary>
        /// Enabled
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if ((m_hookHelper.ActiveView is IMap) && pRasterLayer != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

     


        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdRasterTransZ.OnClick implementation
            IMapControl3 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;
            if (pMapCtr != null)
            {
                FrmRasterTransZ m_frmRasterTranZ = new FrmRasterTransZ();
                if (m_frmRasterTranZ.ShowDialog() == DialogResult.OK)
                {
                    if (m_frmRasterTranZ.istrue == true)
                    {
                        ClsRasterOp pRasterOp = new ClsRasterOp();
                        IRaster2 pRaster2  = pRasterLayer.Raster as IRaster2;
                        IRasterDataset2 pRasterDataset = pRaster2.RasterDataset as IRasterDataset2;
                        pRasterOp.ChangeRasterValue(pRasterDataset, m_frmRasterTranZ.a, m_frmRasterTranZ.b);   
                        //此处要判断图层是否存在金字塔，如果存在则需要重新创建
                        IRasterPyramid3 pDstRasterPyramid3 = pRasterDataset as IRasterPyramid3;
                        if (pDstRasterPyramid3.Present)
                        {
                            IDataLayer2 pDataLayer = pRasterLayer as IDataLayer2;
                            pDataLayer.Disconnect();

                            pDstRasterPyramid3.DeletePyramid();
                            pDstRasterPyramid3.Create();

                            pRasterLayer.CreateFromDataset(pRasterDataset);
                        }
                        //更新图层渲染方式
                        ChangeStrechRender(pRasterLayer);
                        pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        if (m_axTocControl != null)
                        {
                            m_axTocControl.SetBuddyControl(pMapCtr);
                            m_axTocControl.Update();
                            m_axTocControl.ActiveView.Refresh();
                        }
                    }
                }
            }
        }
        #endregion

        private void ChangeStrechRender(IRasterLayer rasterLayer/*, double dbMaxValue, double dbMinValue*/)
        {
            IRaster pRaster = rasterLayer.Raster;
            IRasterBandCollection pRasterBandCollection = pRaster as IRasterBandCollection;
            IRasterBand pRasterBand = pRasterBandCollection.Item(0);
            if (pRasterBand == null)
                return;

            bool bFlag = false;
            pRasterBand.HasStatistics(out bFlag);
            if (!bFlag)
            {
                pRasterBand.ComputeStatsAndHist();
            }

            double dbMaxValue = double.NaN;
            double dbMinValue = double.NaN;

            IRasterStatistics pStaticts = pRasterBand.Statistics;
            dbMaxValue = pStaticts.Maximum;
            dbMinValue = pStaticts.Minimum;

            //设置最大最小渲染方式
            IRasterStretch pRasterStretch = new RasterStretchColorRampRendererClass();
            IRasterStretchMinMax pRasterStretchMinMax = pRasterStretch as IRasterStretchMinMax;
            pRasterStretchMinMax.UseCustomStretchMinMax = true;
            pRasterStretchMinMax.CustomStretchMin = dbMinValue;
            pRasterStretchMinMax.CustomStretchMax = dbMaxValue;
            pRasterStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;

            //update
            IRasterStretchColorRampRenderer pStretchColorRasterRenderer = (IRasterStretchColorRampRenderer)pRasterStretch; ;
            IRasterRenderer pRasterRenderer = pStretchColorRasterRenderer as IRasterRenderer;
            pRasterRenderer.Raster = rasterLayer.Raster;
            pRasterLayer.Renderer = pStretchColorRasterRenderer as IRasterRenderer;
            pRasterRenderer.Update();
        }
    }
}
