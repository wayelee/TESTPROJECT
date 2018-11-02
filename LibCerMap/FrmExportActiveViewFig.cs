using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Output;
using System.Drawing.Text;
using ESRI.ArcGIS.Analyst3D;
using stdole;
using System.Runtime.InteropServices;
using DevComponents.DotNetBar;
using System.Xml;


namespace LibCerMap
{
    public partial class FrmExportActiveViewFig : OfficeForm
    {
        /* GDI delegate to GetDeviceCaps function */
        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(int hdc, int nIndex);

        /* User32 delegates to getDC and ReleaseDC */
        [DllImport("User32.dll")]
        public static extern int GetDC(int hWnd);

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(int hWnd, int hDC);

        //[DllImport("user32.dll", SetLastError = true)]
        //static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);


        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);

        /* constants used for user32 calls */
        const uint SPI_GETFONTSMOOTHING = 74;
        const uint SPI_SETFONTSMOOTHING = 75;
        const uint SPIF_UPDATEINIFILE = 0x1;

        int FrmIndex = 0;
        IHookHelper m_HookHelper;
        public FrmExportActiveViewFig(IHookHelper hookhelper)
        {
            InitializeComponent();
            this.EnableGlass = false;
            FrmIndex = 1;
            m_HookHelper = hookhelper;
        }

        ISceneControl pSceneControl;
        public FrmExportActiveViewFig(ISceneControl scenecontrol)
        {
            InitializeComponent();
            pSceneControl = scenecontrol;
            FrmIndex = 2;
        }

        IPageLayoutControl2 m_PagelayoutControl;
        string MxdFilePath;//导入的着陆点Mxd模板图
        Pt_3d LandPointAim;//瞄准落点
        Pt_3d LandPointRes;//实际落点
        ControlsSynchronizer m_pSynchronizer = null;
        public FrmExportActiveViewFig(IPageLayoutControl2 pagelayoutcontrol, string xmlFileFullPath, ControlsSynchronizer synchronizer)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_PagelayoutControl = pagelayoutcontrol;
            m_pSynchronizer = synchronizer;
            FrmIndex = 1;
            AcceptFile(xmlFileFullPath);
        }

        public FrmExportActiveViewFig(IPageLayoutControl2 pagelayoutcontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_PagelayoutControl = pagelayoutcontrol;
 
            FrmIndex = 1;

        }

        private void FrmExportActiveViewFig_Load(object sender, EventArgs e)
        {
            //设置缺少的文件名和路径
            IPageLayoutControl3 pPageLayoutControl = null;
            if (m_HookHelper != null)
            {
                if (m_HookHelper.Hook is IToolbarControl)
                {
                    if (((IToolbarControl)m_HookHelper.Hook).Buddy is IPageLayoutControl3)
                    {
                        pPageLayoutControl = (IPageLayoutControl3)((IToolbarControl)m_HookHelper.Hook).Buddy;
                    }
                }
                else if (m_HookHelper.Hook is IPageLayoutControl3)
                {
                    pPageLayoutControl = m_HookHelper.Hook as IPageLayoutControl3;
                }
            }
            else
            {
                pPageLayoutControl = pSceneControl as IPageLayoutControl3;
            }
            
            if (pPageLayoutControl != null)
            {
                string strDocName = pPageLayoutControl.DocumentFilename;
                if (strDocName == null)
                {
                    this.txtImagePath.Text = System.IO.Path.GetPathRoot(Application.StartupPath) + "CEMapping.jpg";
                }
                else
                {
                    this.txtImagePath.Text = System.IO.Path.GetDirectoryName(strDocName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(strDocName) + ".jpg";
                }

            }
            else
            {
                this.txtImagePath.Text = System.IO.Path.GetPathRoot(Application.StartupPath) + "CEMapping.jpg";
            }

        }
        //加载文件图片路径
        private void btImagePath_Click(object sender, EventArgs e)
        {
            if (FrmIndex == 1)
            {
                SaveFileDialog savfile = new SaveFileDialog();
                savfile.Title = "输出图片路径";
                savfile.Filter = "图像文件(*.JPEG;*.TIFF;*.PDF;*.BMP;*.PNG;*.GIF;*.TGA;*.EXIF)|*.JPEG;*.TIFF;*.PDF;*.BMP;*.PNG;*.GIF;*.TGA;*.EXIF|所有文件(*.*)|*.*";
                if (savfile.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(System.IO.Path.GetExtension(savfile.FileName)))
                    {
                        savfile.FileName += ".jpg";
                    }
                    this.txtImagePath.Text = savfile.FileName;
                }
            }

            if (FrmIndex == 2)
            {
                SaveFileDialog savfile = new SaveFileDialog();
                savfile.Title = "输出图片路径";
                savfile.Filter = ".BMP|*.BMP|.JPEG|*.JPEG";
                if (savfile.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(System.IO.Path.GetExtension(savfile.FileName)))
                    {
                        savfile.FileName += ".jpg";
                    }
                    this.txtImagePath.Text = savfile.FileName;
                }
            }
        }
        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (FrmIndex == 1)
                {
                    long ImageResolution = 0;
                    string ImageType = "";
                    string ImageDirect = "";
                    string ImageFileName = "";
                    //分辨率
                    ImageResolution = long.Parse(double.Parse(cboBoxImageResolution.Text).ToString());
                    //文件路径和名称
                    if (txtImagePath.Text != null)
                    {
                        //路径
                        ImageDirect = System.IO.Path.GetDirectoryName(txtImagePath.Text) + @"\";
                        //名称
                        ImageFileName = System.IO.Path.GetFileNameWithoutExtension(txtImagePath.Text);
                        //文件类型
                        ImageType = System.IO.Path.GetExtension(txtImagePath.Text).Substring(1);

                    }
                    //导出图片
                    ExportActiveViewParameterized(m_HookHelper.ActiveView, ImageResolution, 1, ImageType, ImageDirect, ImageFileName, false);
                }

                if (FrmIndex == 2)
                {
                    CreateSceneViewImage();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void AcceptFile(string xmlFileFullPath)
        {
            //解析电文
            bool readResult = ReadLandPointXML(xmlFileFullPath);

            //修改坐标
            if (readResult)
            {
                LandPointEdit();
            }
            else
            {
                MessageBox.Show("XML文件解析失败", "提示", MessageBoxButtons.OK);
                return;
            }
            //激活到布局
            m_PagelayoutControl.PageLayout.ZoomToWhole();
            m_PagelayoutControl.ActiveView.Refresh();

            //System.DateTime curTime = new System.DateTime();
            //curTime = DateTime.Now;
            //string strFileName = "着陆点图" + curTime.ToShortDateString() + "-" + curTime.ToShortTimeString() + "-" + curTime.Second.ToString();
            //strFileName = strFileName.Replace(@"/", @"-");
            //strFileName = strFileName.Replace(@":", @"-");
            //ExportActiveViewParameterized(m_PagelayoutControl.ActiveView, 400, 1, "JPEG", @"F:\PMRS\", strFileName, false);

            
        }

        public void ExportActiveViewParameterized(IActiveView docActiveView, long iOutputResolution, long lResampleRatio, string ExportType, string sOutputDir, string sOutputFileName, Boolean bClipToGraphicsExtent)
        {
            //IActiveView docActiveView = m_HookHelper.ActiveView;
            IExport docExport;
            long iPrevOutputImageQuality;
            IOutputRasterSettings docOutputRasterSettings;
            IEnvelope PixelBoundsEnv;
            tagRECT exportRECT;
            tagRECT DisplayBounds;
            IDisplayTransformation docDisplayTransformation;
            IPageLayout docPageLayout;
            IEnvelope docMapExtEnv;
            long hdc;
            long tmpDC;
            string sNameRoot;
            long iScreenResolution;
            bool bReenable = false;


            IEnvelope docGraphicsExtentEnv;
            IUnitConverter pUnitConvertor;

            try
            {

                if (GetFontSmoothing())
                {
                    /* font smoothing is on, disable it and set the flag to reenable it later. */
                    bReenable = true;
                    DisableFontSmoothing();
                    if (GetFontSmoothing())
                    {
                        //font smoothing is NOT successfully disabled, error out.
                        return;
                    }
                    //else font smoothing was successfully disabled.
                }


                // The Export*Class() type initializes a new export class of the desired type.

                if (ExportType == "PDF" || ExportType == "pdf")
                {
                    docExport = new ExportPDFClass();
                }
                else if (ExportType == "EPS" || ExportType == "eps")
                {
                    docExport = new ExportPSClass();
                }
                else if (ExportType == "AI" || ExportType == "ai")
                {
                    docExport = new ExportAIClass();
                }
                else if (ExportType == "BMP" || ExportType == "bmp")
                {

                    docExport = new ExportBMPClass();
                }
                else if (ExportType == "TIF" || ExportType == "tif")
                {
                    docExport = new ExportTIFFClass();
                }
                else if (ExportType == "SVG" || ExportType == "svg")
                {
                    docExport = new ExportSVGClass();
                }
                else if (ExportType == "PNG" || ExportType == "png")
                {
                    docExport = new ExportPNGClass();
                }
                else if (ExportType == "GIF" || ExportType == "gif")
                {
                    docExport = new ExportGIFClass();
                }
                else if (ExportType == "EMF" || ExportType == "emf")
                {
                    docExport = new ExportEMFClass();
                }
                else if (ExportType == "JPEG" || ExportType == "jpg")
                {
                    docExport = new ExportJPEGClass();
                }
                else
                {
                    MessageBox.Show("Unsupported export type " + ExportType + ", defaulting to EMF.");
                    ExportType = "EMF";
                    docExport = new ExportEMFClass();
                }


                //  save the previous output image quality, so that when the export is complete it will be set back.
                docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
                iPrevOutputImageQuality = docOutputRasterSettings.ResampleRatio;


                if (docExport is IExportImage)
                {
                    // always set the output quality of the DISPLAY to 1 for image export formats
                    SetOutputQuality(docActiveView, 1);
                }
                else
                {
                    // for vector formats, assign the desired ResampleRatio to control drawing of raster layers at export time   
                    SetOutputQuality(docActiveView, lResampleRatio);
                }

                //set the name root for the export
                // sNameRoot = "ExportActiveViewSampleOutput";
                sNameRoot = sOutputFileName;
                //set the export filename (which is the nameroot + the appropriate file extension)
                docExport.ExportFileName = sOutputDir + sNameRoot + "." + docExport.Filter.Split('.')[1].Split('|')[0].Split(')')[0];


                /* Get the device context of the screen */
                tmpDC = GetDC(0);
                /* Get the screen resolution. */
                iScreenResolution = GetDeviceCaps((int)tmpDC, 88); //88 is the win32 const for Logical pixels/inch in X)
                /* release the DC. */
                ReleaseDC(0, (int)tmpDC);
                docExport.Resolution = iOutputResolution;


                if (docActiveView is IPageLayout)
                {
                    //get the bounds of the "exportframe" of the active view.
                    DisplayBounds = docActiveView.ExportFrame;
                    //set up pGraphicsExtent, used if clipping to graphics extent.
                    docGraphicsExtentEnv = GetGraphicsExtent(docActiveView);
                }
                else
                {
                    //Get the bounds of the deviceframe for the screen.
                    docDisplayTransformation = docActiveView.ScreenDisplay.DisplayTransformation;
                    DisplayBounds = docDisplayTransformation.get_DeviceFrame();
                }

                PixelBoundsEnv = new Envelope() as IEnvelope;

                if (bClipToGraphicsExtent && (docActiveView is IPageLayout))
                {
                    docGraphicsExtentEnv = GetGraphicsExtent(docActiveView);
                    docPageLayout = docActiveView as PageLayout;
                    pUnitConvertor = new UnitConverter();

                    //assign the x and y values representing the clipped area to the PixelBounds envelope
                    PixelBoundsEnv.XMin = 0;
                    PixelBoundsEnv.YMin = 0;
                    PixelBoundsEnv.XMax = pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.XMax, docPageLayout.Page.Units, esriUnits.esriInches) * docExport.Resolution - pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.XMin, docPageLayout.Page.Units, esriUnits.esriInches) * docExport.Resolution;
                    PixelBoundsEnv.YMax = pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.YMax, docPageLayout.Page.Units, esriUnits.esriInches) * docExport.Resolution - pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.YMin, docPageLayout.Page.Units, esriUnits.esriInches) * docExport.Resolution;

                    //'assign the x and y values representing the clipped export extent to the exportRECT
                    exportRECT.bottom = (int)(PixelBoundsEnv.YMax) + 1;
                    exportRECT.left = (int)(PixelBoundsEnv.XMin);
                    exportRECT.top = (int)(PixelBoundsEnv.YMin);
                    exportRECT.right = (int)(PixelBoundsEnv.XMax) + 1;

                    //since we're clipping to graphics extent, set the visible bounds.
                    docMapExtEnv = docGraphicsExtentEnv;
                }
                else
                {
                    double tempratio = iOutputResolution / iScreenResolution;
                    double tempbottom = DisplayBounds.bottom * tempratio;
                    double tempright = DisplayBounds.right * tempratio;
                    //'The values in the exportRECT tagRECT correspond to the width
                    //and height to export, measured in pixels with an origin in the top left corner.
                    exportRECT.bottom = (int)Math.Truncate(tempbottom);
                    exportRECT.left = 0;
                    exportRECT.top = 0;
                    exportRECT.right = (int)Math.Truncate(tempright);


                    //populate the PixelBounds envelope with the values from exportRECT.
                    // We need to do this because the exporter object requires an envelope object
                    // instead of a tagRECT structure.
                    PixelBoundsEnv.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);

                    //since it's a page layout or an unclipped page layout we don't need docMapExtEnv.
                    docMapExtEnv = null;
                }

                // Assign the envelope object to the exporter object's PixelBounds property.  The exporter object
                // will use these dimensions when allocating memory for the export file.
                docExport.PixelBounds = PixelBoundsEnv;

                // call the StartExporting method to tell docExport you're ready to start outputting.
                hdc = docExport.StartExporting();

                // Redraw the active view, rendering it to the exporter object device context instead of the app display.
                // We pass the following values:
                //  * hDC is the device context of the exporter object.
                //  * exportRECT is the tagRECT structure that describes the dimensions of the view that will be rendered.
                // The values in exportRECT should match those held in the exporter object's PixelBounds property.
                //  * docMapExtEnv is an envelope defining the section of the original image to draw into the export object.
                docActiveView.Output((int)hdc, (int)docExport.Resolution, ref exportRECT, docMapExtEnv, null);

                //finishexporting, then cleanup.
                docExport.FinishExporting();
                docExport.Cleanup();


                MessageBox.Show("导出完成 " + sOutputDir + sNameRoot + "." + docExport.Filter.Split('.')[1].Split('|')[0].Split(')')[0] + ".", "Export Active View");

                //set the output quality back to the previous value
                SetOutputQuality(docActiveView, iPrevOutputImageQuality);
                if (bReenable)
                {
                    /* reenable font smoothing if we disabled it before */
                    EnableFontSmoothing();
                    bReenable = false;
                    if (!GetFontSmoothing())
                    {
                        //error: cannot reenable font smoothing.
                        MessageBox.Show("Unable to reenable Font Smoothing", "Font Smoothing error");
                    }
                }


                docMapExtEnv = null;
                PixelBoundsEnv = null;
            }
            catch (System.Exception ex)
            {
                ;
            }
        }

        private void SetOutputQuality(IActiveView docActiveView, long iResampleRatio)
        {
            /* This function sets OutputImageQuality for the active view.  If the active view is a pagelayout, then
             * it must also set the output image quality for EACH of the Maps in the pagelayout.
             */
            IGraphicsContainer oiqGraphicsContainer;
            IElement oiqElement;
            IOutputRasterSettings docOutputRasterSettings;
            IMapFrame docMapFrame;
            IActiveView TmpActiveView;

            if (docActiveView is IMap)
            {
                docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
                docOutputRasterSettings.ResampleRatio = (int)iResampleRatio;
            }
            else if (docActiveView is IPageLayout)
            {
                //assign ResampleRatio for PageLayout
                docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
                docOutputRasterSettings.ResampleRatio = (int)iResampleRatio;
                //and assign ResampleRatio to the Maps in the PageLayout
                oiqGraphicsContainer = docActiveView as IGraphicsContainer;
                oiqGraphicsContainer.Reset();

                oiqElement = oiqGraphicsContainer.Next();
                while (oiqElement != null)
                {
                    if (oiqElement is IMapFrame)
                    {
                        docMapFrame = oiqElement as IMapFrame;
                        TmpActiveView = docMapFrame.Map as IActiveView;
                        docOutputRasterSettings = TmpActiveView.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
                        docOutputRasterSettings.ResampleRatio = (int)iResampleRatio;
                    }
                    oiqElement = oiqGraphicsContainer.Next();
                }

                docMapFrame = null;
                oiqGraphicsContainer = null;
                TmpActiveView = null;
            }
            docOutputRasterSettings = null;

        }

        private IEnvelope GetGraphicsExtent(IActiveView docActiveView)
        {
            /* Gets the combined extent of all the objects in the map. */
            IEnvelope GraphicsBounds;
            IEnvelope GraphicsEnvelope;
            IGraphicsContainer oiqGraphicsContainer;
            IPageLayout docPageLayout;
            IDisplay GraphicsDisplay;
            IElement oiqElement;

            GraphicsBounds = new EnvelopeClass();
            GraphicsEnvelope = new EnvelopeClass();
            docPageLayout = docActiveView as IPageLayout;
            GraphicsDisplay = docActiveView.ScreenDisplay;
            oiqGraphicsContainer = docActiveView as IGraphicsContainer;
            oiqGraphicsContainer.Reset();

            oiqElement = oiqGraphicsContainer.Next();
            while (oiqElement != null)
            {
                oiqElement.QueryBounds(GraphicsDisplay, GraphicsEnvelope);
                GraphicsBounds.Union(GraphicsEnvelope);
                oiqElement = oiqGraphicsContainer.Next();
            }

            return GraphicsBounds;

        }

        private void DisableFontSmoothing()
        {
            bool iResult;
            int pv = 0;

            /* call to systemparametersinfo to set the font smoothing value */
            iResult = SystemParametersInfo(SPI_SETFONTSMOOTHING, 0, ref pv, SPIF_UPDATEINIFILE);
        }

        private void EnableFontSmoothing()
        {
            bool iResult;
            int pv = 0;

            /* call to systemparametersinfo to set the font smoothing value */
            iResult = SystemParametersInfo(SPI_SETFONTSMOOTHING, 1, ref pv, SPIF_UPDATEINIFILE);

        }

        private Boolean GetFontSmoothing()
        {
            bool iResult;
            int pv = 0;

            /* call to systemparametersinfo to get the font smoothing value */
            iResult = SystemParametersInfo(SPI_GETFONTSMOOTHING, 0, ref pv, 0);

            if (pv > 0)
            {
                //pv > 0 means font smoothing is ON.
                return true;
            }
            else
            {
                //pv == 0 means font smoothing is OFF.
                return false;
            }

        }

        private void CreateSceneViewImage()
        {
            try
            {
                string ImageType = "";
                //文件路径和名称
                if (txtImagePath.Text != null)
                {
                    //文件类型
                    ImageType = System.IO.Path.GetExtension(txtImagePath.Text).Substring(1);

                }
                ISceneViewer pSceneView = pSceneControl.SceneViewer;
                if (ImageType == "BMP")
                {
                    pSceneView.GetScreenShot(esri3DOutputImageType.BMP, txtImagePath.Text);
                }
                else if (ImageType == "JPEG")
                {
                    pSceneView.GetScreenShot(esri3DOutputImageType.JPEG, txtImagePath.Text);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ReadLandPointXML(string fileFullPath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileFullPath);
                XmlNode xn = xmlDoc.SelectSingleNode(@"cmlRoot/dAimLog");//(@"cmlRoot/szMacroNum");
                if (xn != null)
                {
                    LandPointAim.X = Convert.ToDouble(xn.InnerText);//经度
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/dAimLat");
                if (xn != null)
                {
                    LandPointAim.Y = Convert.ToDouble(xn.InnerText);//纬度
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/dAimHeight");
                if (xn != null)
                {
                    LandPointAim.Z = Convert.ToDouble(xn.InnerText);//高度
                }

                xn = xmlDoc.SelectSingleNode(@"cmlRoot/dLandLog");
                if (xn != null)
                {
                    LandPointRes.X = Convert.ToDouble(xn.InnerText);//经度
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/dLandLat");
                if (xn != null)
                {
                    LandPointRes.Y = Convert.ToDouble(xn.InnerText);//纬度
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/dLandHeight");
                if (xn != null)
                {
                    LandPointRes.Z = Convert.ToDouble(xn.InnerText);//高度
                }
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private void LandPointEdit()
        {
            try
            {

                ILayer pLayerLand = ClsGDBDataCommon.GetLayerFromName(m_PagelayoutControl.ActiveView.FocusMap, "着陆点");
                ILayer pLayerLandAim = ClsGDBDataCommon.GetLayerFromName(m_PagelayoutControl.ActiveView.FocusMap, "瞄准点");
                if (pLayerLand == null && pLayerLandAim == null) return;

                if (pLayerLand is IFeatureLayer)
                {
                    IFeatureLayer pFLayerRes = pLayerLand as IFeatureLayer;
                    IFeatureCursor pFCursor = null;
                    IFeature pFeature = null;

                    ITable pTable = (ITable)pFLayerRes;
                    IFields pFields = pTable.Fields;
                    int nFieldIndex = pFields.FindField("Type");//获取标示类型的字段
                    int nFieldLog = 0;
                    int nFieldLat = 0;
                    int nFiledHeight = 0;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        if (pFields.get_Field(i).Name == "Log")
                        {
                            nFieldLog = i;
                        }
                        if (pFields.get_Field(i).Name == "Lat")
                        {
                            nFieldLat = i;
                        }
                        if (pFields.get_Field(i).Name == "Height")
                        {
                            nFiledHeight = i;
                        }
                    }

                    pFCursor = pFLayerRes.Search(null, false);

                    pFeature = pFCursor.NextFeature();
                    while (pFeature != null)
                    {
                        string strType = pFeature.get_Value(nFieldIndex).ToString();
                        IGeometry pGeometry = pFeature.Shape;
                        if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
                        {
                            IPoint pPoint = pGeometry as IPoint;
                            if (strType == "实际落点")
                            {
                                pPoint.PutCoords(LandPointRes.X, LandPointRes.Y);
                                pFeature.set_Value(nFieldLog, Math.Abs(LandPointRes.X).ToString("f2") + "°W");
                                pFeature.set_Value(nFieldLat, Math.Abs(LandPointRes.Y).ToString("f2") + "°N");
                                pFeature.set_Value(nFiledHeight, LandPointRes.Z);
                            }
                            pFeature.Store();
                        }
                        pFeature = pFCursor.NextFeature();
                    }
                    //pFCursor.Flush();
                }

                if (pLayerLandAim is IFeatureLayer)
                {

                    IFeatureLayer pFLayerRes = pLayerLandAim as IFeatureLayer;
                    IFeatureCursor pFCursor = null;
                    IFeature pFeature = null;

                    ITable pTable = (ITable)pFLayerRes;
                    IFields pFields = pTable.Fields;
                    int nFieldIndex = pFields.FindField("Type");//获取标示类型的字段
                    int nFieldLog = 0;
                    int nFieldLat = 0;
                    int nFiledHeight = 0;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        if (pFields.get_Field(i).Name == "Log")
                        {
                            nFieldLog = i;
                        }
                        if (pFields.get_Field(i).Name == "Lat")
                        {
                            nFieldLat = i;
                        }
                        if (pFields.get_Field(i).Name == "Height")
                        {
                            nFiledHeight = i;
                        }
                    }

                    pFCursor = pFLayerRes.Search(null, false);
                    pFeature = pFCursor.NextFeature();
                    while (pFeature != null)
                    {
                        string strType = pFeature.get_Value(nFieldIndex).ToString();
                        IGeometry pGeometry = pFeature.Shape;
                        if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
                        {
                            IPoint pPoint = pGeometry as IPoint;
                            if (strType == "瞄准点")
                            {
                                pPoint.PutCoords(LandPointAim.X, LandPointAim.Y);
                                pFeature.set_Value(nFieldLog, Math.Abs(LandPointAim.X).ToString(".##") + "°W");
                                pFeature.set_Value(nFieldLat, Math.Abs(LandPointAim.Y).ToString(".##") + "°N");
                                pFeature.set_Value(nFiledHeight, LandPointAim.Z);
                            }
                            pFeature.Store();
                        }
                        pFeature = pFCursor.NextFeature();
                    }

                    //pFCursor.Flush();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
