using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using System.Collections.Generic;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using LibCerMap;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.SpatialAnalyst;


namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolExtractCraterSample.
    /// </summary>
    [Guid("33c0fa43-5278-4f24-9fa4-6dc8cca2189b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolExtractCraterSample")]
    public sealed class ToolExtractCraterSample : BaseTool
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
        //撞击坑图层
        public IFeatureLayer pFeatureLayer;
        public IRasterLayer pRasterLayer;
        public IMap pMap;
        private INewRectangleFeedback  pNewRectangleFeedback = new NewRectangleFeedbackClass();
        IPoint pMouseDownPoint ;
        IPoint pMouseSecondPoint;
        public IPolygon pPolygon;
        public string clipdompath;
        bool IsSquare = false;
        public ToolExtractCraterSample()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "选择撞击坑样本";  //localizable text 
            base.m_message = "选择撞击坑样本";  //localizable text
            base.m_toolTip = "选择撞击坑样本";  //localizable text
            base.m_name = "CustomCE.ToolExtractCraterSample";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }

        }

        #region Overridden Class Methods

        public override bool Enabled
        {
            get
            {
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr == null)
                {
                    return false;
                }
                // TODO: Add CmdNorthArrow.OnClick implementation   
                if (pRasterLayer == null)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add ToolExtractCraterSample.OnCreate implementation
        }

        public override bool Deactivate()
        {
            pNewRectangleFeedback.Stop(null);
            return base.Deactivate();
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolExtractCraterSample.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolExtractCraterSample.OnMouseDown implementation
            if (Button != 1)
            {
                return;
            }
            IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
          
            pNewRectangleFeedback.Angle = 0;
            pNewRectangleFeedback.Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsSquare;
            pNewRectangleFeedback.Display = pMapCtr.ActiveView.ScreenDisplay;
           // ((Control)pMapCtr).Focus();
            pNewRectangleFeedback.Stop(null);
            pMouseSecondPoint = null;
            if (pMapCtr != null)
            {
                pMouseDownPoint = pMapCtr.ToMapPoint(X, Y);
                pNewRectangleFeedback.Start(pMouseDownPoint);
            }

        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolExtractCraterSample.OnMouseMove implementation
            if (pMouseDownPoint != null)
            {
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr != null)
                {  
                    IPoint pt =  pMapCtr.ToMapPoint(X, Y);
                    double dx =  pt.X - pMouseDownPoint.X;
                    double dy =  pt.Y- pMouseDownPoint.Y;
                    IPoint pt2 = new PointClass();
                    if (IsSquare == true)
                    { 
                        if ( Math.Abs(dy) > Math.Abs(dx))
                        {
                            pt.Y = pMouseDownPoint.Y + Math.Abs(dx) * Math.Abs(dy) / dy;
                        }
                        else
                        {
                            pt.X = pMouseDownPoint.X + Math.Abs(dy) * Math.Abs(dx) / dx;
                        }
                    }
                   
                  //  pNewRectangleFeedback.SetPoint(pt2);
                    pNewRectangleFeedback.IsEnvelope = true;                  
                    pNewRectangleFeedback.MoveTo(pt);
                    pMouseSecondPoint = pt;
                   // pNewRectangleFeedback.Refresh(pMapCtr.ActiveView.ScreenDisplay.hDC);
                }
             
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolExtractCraterSample.OnMouseUp implementation
            pMouseDownPoint = null;
        }

        public override void OnDblClick()
        {
           
            base.OnDblClick();
        }
        
        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (Shift == 1)
            {
                IsSquare = true;
            }
            base.OnKeyDown(keyCode, Shift);
        }
        public override void OnKeyUp(int keyCode, int Shift)
        {
            IsSquare = false;
            if (keyCode == 13)
            {
                if (pMouseSecondPoint != null)
                {
                    IPolygon pPolygong = pNewRectangleFeedback.Stop(pMouseSecondPoint) as IPolygon;
                    pPolygon = pPolygong;
                    string rasterfilepath = pRasterLayer.FilePath;//选中图层的文件路径
                    string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
                    string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称
                    //clipdompath = GetParentPathofExe() + @"Resource\CraterTexture\" + rasterfilename + "_clip" + System.IO.Path.GetExtension(rasterfilepath);
                    //clipdompath = GetParentPathofExe() + @"Resource\CraterTexture\DEM-New-01_dom_Clip.tif";
                    clipdompath = rasterfiledir + "\\" + rasterfilename + "_clip.tif";// GetParentPathofExe() + @"Resource\CraterTexture\DEM-New-01_dom_Clip.tif";
                    if (File.Exists(clipdompath) == true)
                    {
                        try
                        {
                            File.Delete(clipdompath);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                    if (pPolygon == null)
                    {
                        return;
                    }
                    //RasterClip(pRasterLayer, pPolygong, clipdompath);
                    IRaster2 pr = pRasterLayer.Raster as IRaster2;

                    RasterSubsetByPolygon(pr.RasterDataset , pPolygong, clipdompath);
                }
            }
            base.OnKeyUp(keyCode, Shift);
        }

        public bool RasterSubsetByPolygon(IRasterDataset pRasterDataset, IPolygon clipGeo, string FileName)
        {
            try
            {
                IRasterBandCollection rasterBands = pRasterDataset as IRasterBandCollection;
                int nBandCount = rasterBands.Count;
                //设置输出栅格参数
                IRasterLayer rasterLayer = new RasterLayerClass();
                rasterLayer.CreateFromDataset(pRasterDataset);
                IRaster pRaster = rasterLayer.Raster;//此处只得到前3个波段              
                IRasterBandCollection bandsNew = pRaster as IRasterBandCollection;
                IRasterBand pBand = null;

                for (int i = 3; i < nBandCount; i++)
                {
                    pBand = rasterBands.Item(i);
                    bandsNew.AppendBand(pBand);
                }

                IRasterProps pProps = pRaster as IRasterProps;
                object cellSizeProvider = pProps.MeanCellSize().X;
                IGeoDataset pInputDataset = pRaster as IGeoDataset;

                //IGeoDataset pInputDataset = pRasterDataset as IGeoDataset;//此种方式也只是得到前3个波段

                //设置格格处理环境
                IExtractionOp2 pExtractionOp = new RasterExtractionOpClass();
                IRasterAnalysisEnvironment pRasterAnaEnvir = pExtractionOp as IRasterAnalysisEnvironment;
                pRasterAnaEnvir.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cellSizeProvider);

                object extentProvider = clipGeo.Envelope;
                object snapRasterData = Type.Missing;
                pRasterAnaEnvir.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProvider, ref snapRasterData);

                IGeoDataset pOutputDataset = pExtractionOp.Polygon(pInputDataset, clipGeo as IPolygon, true);//裁切操作

                IRaster clipRaster; //裁切后得到的IRaster
                if (pOutputDataset is IRasterLayer)
                {
                    IRasterLayer rasterLayer2 = pOutputDataset as IRasterLayer;
                    clipRaster = rasterLayer.Raster;
                }
                else if (pOutputDataset is IRasterDataset)
                {
                    IRasterDataset rasterDataset = pOutputDataset as IRasterDataset;
                    clipRaster = rasterDataset.CreateDefaultRaster();
                }
                else if (pOutputDataset is IRaster)
                {
                    clipRaster = pOutputDataset as IRaster;
                    IRasterProps temRastPro = clipRaster as IRasterProps;
                    temRastPro.NoDataValue = pProps.NoDataValue;
                    temRastPro.PixelType = rstPixelType.PT_UCHAR;
                }
                else
                {
                    return false;
                }
                //保存裁切后得到的clipRaster
                //如果直接保存为img影像文件
                //判断保存类型
                string strFileType = System.IO.Path.GetExtension(FileName);
                switch (strFileType.ToUpper())
                {
                    case "TIF":
                        strFileType = "TIFF";
                        break;
                    case "IMG":
                        strFileType = "IMAGINE Image";
                        break;
                    default:
                        strFileType = "TIFF";
                        break;
                }
                IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(FileName), 0);
                ISaveAs pSaveAs = clipRaster as ISaveAs;
                IDataset pDataset = pSaveAs.SaveAs(FileName, pWorkspace, strFileType);//以TIF格式保存
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
                MessageBox.Show("\"" + FileName + "\"成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
                //MessageBox.Show("成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public  void RasterClip(IRasterLayer pRasterLayer, IPolygon clipGeo, string FileName)
        {
            try
            {

                //IRaster pRaster = pRasterLayer.Raster;

                //IRasterProps pProps = pRaster as IRasterProps;

                //object cellSizeProvider = pProps.MeanCellSize().X;

                //IGeoDataset pInputDataset = pRaster as IGeoDataset;

                //IExtractionOp2 pExtractionOp = new ESRI.ArcGIS.SpatialAnalyst.RasterExtractionOpClass();

                //IRasterAnalysisEnvironment pRasterAnaEnvir = pExtractionOp as IRasterAnalysisEnvironment;

                //pRasterAnaEnvir.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cellSizeProvider);

                //object extentProvider = clipGeo.Envelope;

                //object snapRasterData = Type.Missing;

                //pRasterAnaEnvir.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProvider, ref snapRasterData);


                IRasterProps pProps =pRasterLayer.Raster  as IRasterProps;
                object cellSizeProvider = pProps.MeanCellSize().X;
                IGeoDataset pInputDataset = pRasterLayer.Raster as IGeoDataset;

                //IGeoDataset pInputDataset = pRasterDataset as IGeoDataset;//此种方式也只是得到前3个波段

                //设置格格处理环境
                IExtractionOp2 pExtractionOp = new RasterExtractionOpClass();
                IRasterAnalysisEnvironment pRasterAnaEnvir = pExtractionOp as IRasterAnalysisEnvironment;
                pRasterAnaEnvir.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cellSizeProvider);

                object extentProvider = clipGeo.Envelope;
                object snapRasterData = Type.Missing;
                pRasterAnaEnvir.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProvider, ref snapRasterData);



                IGeoDataset pOutputDataset = pExtractionOp.Polygon(pInputDataset, clipGeo as IPolygon, true);//裁切操作
              //  IGeoDataset pOutputDataset = pExtractionOp.Rectangle(pInputDataset, clipGeo.Envelope, true);

                IRaster clipRaster; //裁切后得到的IRasterzzz

                if (pOutputDataset is IRasterLayer)
                {

                    IRasterLayer rasterLayer = pOutputDataset as IRasterLayer;

                    clipRaster = rasterLayer.Raster;

                }

                else if (pOutputDataset is IRasterDataset)
                {

                    IRasterDataset rasterDataset = pOutputDataset as IRasterDataset;

                    clipRaster = rasterDataset.CreateDefaultRaster();

                }

                else if (pOutputDataset is IRaster)
                {

                    clipRaster = pOutputDataset as IRaster;
                    //增加阈值设定
                    IRasterProps temRastPro = clipRaster as IRasterProps;
                    temRastPro.NoDataValue = pProps.NoDataValue;
                  //  temRastPro.PixelType = pProps.PixelType;

                }

                else
                {

                    return;

                }

                //保存裁切后得到的clipRaster 

                //如果直接保存为img影像文件

                IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();

                IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(FileName), 0);

                ISaveAs pSaveAs = clipRaster as ISaveAs;

                //bool cansave = pSaveAs.CanSaveAs("TIFF");
                bool cansave = pSaveAs.CanSaveAs("BMP");
                bool zzz = cansave;

                IRasterDataset pRD = pSaveAs.SaveAs(System.IO.Path.GetFileName(FileName), pWorkspace, "TIFF") as IRasterDataset;//以img格式保存

                //Note, SaveAs will return a RasterDataset, to prevent from ISaveAs holding the output,
                //.NET ReleaseCOMObject needs to be called to release the referene to the output raster dataset:
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(outRasterDS);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pRD);


                MessageBox.Show("\"" +FileName +"\"成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exp)
            {
                 
                 MessageBox.Show("截取样本失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        
        #endregion
    }
}
