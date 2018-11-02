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
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DisplayUI;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmVertor : OfficeForm
    {
        IPageLayoutControl3 pPageLayoutControl;
        ISceneControl pSceneControl;

        public FrmVertor(IPageLayoutControl3 pagelayoutcontrol, ISceneControl scenecontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pPageLayoutControl = pagelayoutcontrol;
            pSceneControl = scenecontrol;

        }

        private void FrmVertor_Load(object sender, EventArgs e)
        {
            SunAzimuth.Value = 0;
            SunIncli.Value = 0;
            EarAzimuth.Value = 0;
            EarIncli.Value = 0;
        }

        private void btn2D_Click(object sender, EventArgs e)
        {
            try
            {
                IGraphicsContainer pGraphicsContainer = pPageLayoutControl.ActiveView.GraphicsContainer as IGraphicsContainer;
                IMarkerElement pMarkerElement = new MarkerElementClass();

                //////正北方向
                //IPictureMarkerSymbol pPMarkerNorth = new PictureMarkerSymbolClass();
                //string MarkerNorthPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\NorthArrow.emf";
                //pPMarkerNorth.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureEMF, MarkerNorthPath);
                //pPMarkerNorth.Angle = 0;
                //pPMarkerNorth.Size = 100;

                ////太阳方向
                IPictureMarkerSymbol pPMarkerSun = new PictureMarkerSymbolClass();
                string MarkerSunPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\SunArrow.emf";
                pPMarkerSun.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureEMF, MarkerSunPath);
                pPMarkerSun.Angle = (-1) * SunAzimuth.Value;
                pPMarkerSun.Size = 100;

                ////地球方向
                IPictureMarkerSymbol pPMarkerEarth = new PictureMarkerSymbolClass();
                string MarkerEarthPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\EarthArrow.emf";
                pPMarkerEarth.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureEMF, MarkerEarthPath);
                pPMarkerEarth.Angle = (-1) * EarAzimuth.Value;
                pPMarkerEarth.Size = 100;


                IMultiLayerMarkerSymbol pMultiLayerMarkerSymbol = new MultiLayerMarkerSymbolClass();
                //pMultiLayerMarkerSymbol.AddLayer(pPMarkerNorth);
                pMultiLayerMarkerSymbol.AddLayer(pPMarkerSun);
                pMultiLayerMarkerSymbol.AddLayer(pPMarkerEarth);

                pMarkerElement.Symbol = pMultiLayerMarkerSymbol;
                IElement pElement = pMarkerElement as IElement;
                IActiveView pActiveView = pPageLayoutControl.ActiveView;
                IPageLayout pPageLayout = (IPageLayout)pActiveView;
                IPage pPage = pPageLayout.Page;
                double pWidth = pPage.PrintableBounds.XMin + 3;
                double pHeigth = pPage.PrintableBounds.YMax - 5;
                IPoint pPoint = new PointClass();
                pPoint.PutCoords(pWidth, pHeigth);
                pElement.Geometry = (IGeometry)pPoint;
                pGraphicsContainer.AddElement(pElement, 0);

                pPageLayoutControl.ActiveView.Refresh();
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }

            this.Close();
        }

        private void btn3D_Click(object sender, EventArgs e)
        {
            try
            {
                IGraphicsContainer3D graphicsContainer3D = pSceneControl.Scene.ActiveGraphicsLayer as IGraphicsContainer3D;
                graphicsContainer3D.DeleteAllElements();
                pSceneControl.Scene.SceneGraph.RefreshViewers();
                //屏幕大小
                double PageX = pSceneControl.SceneGraph.Extent.Envelope.XMax;
                double PageY = pSceneControl.SceneGraph.Extent.Envelope.YMax;
                double PageZ = pSceneControl.SceneGraph.Extent.Envelope.ZMax;
                double PageXmin = pSceneControl.SceneGraph.Extent.Envelope.XMin;

                double pMarkerSize = (PageX - PageXmin) / 10;
            
                IMarkerElement pMarkerElement = new MarkerElementClass();
                //正北方向
                IMarker3DSymbol pMarker3DSymbolNorth = new Marker3DSymbolClass();
                string NorthPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\NorthArrow.3DS";
                pMarker3DSymbolNorth.CreateFromFile(NorthPath);
                IMarker3DPlacement pMarker3DPlacement = pMarker3DSymbolNorth as IMarker3DPlacement;

                pMarker3DPlacement.Size = pMarkerSize;
                pMarker3DPlacement.SetRotationAngles(-90, 0, 0);

                //IRgbColor pRgbColorNorth = new RgbColorClass();
                //pRgbColorNorth.Red = Color.Black.R;
                //pRgbColorNorth.Green = Color.Black.G;
                //pRgbColorNorth.Blue = Color.Black.B;
                pMarker3DPlacement.Color = ClsGDBDataCommon.ColorToIColor(Color.Black); 

                //太阳方向
                IMarker3DSymbol pMarker3DSymbolSun = new Marker3DSymbolClass();
                string SunPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\SunArrow.3DS";
                pMarker3DSymbolSun.CreateFromFile(SunPath);
                IMarker3DPlacement pMarker3DPlacement1 = pMarker3DSymbolSun as IMarker3DPlacement;
                pMarker3DPlacement1.Size = pMarkerSize;
                double SunAngleX = SunIncli.Value - 90;
                double SunAngleY = 0;
                double SunAngleZ = 0 - SunAzimuth.Value;
                pMarker3DPlacement1.SetRotationAngles(SunAngleX, SunAngleY, SunAngleZ);

                //IRgbColor pRgbColorSun = new RgbColorClass();
                //pRgbColorSun.Red = Color.Red.R;
                //pRgbColorSun.Green = Color.Red.G;
                //pRgbColorSun.Blue = Color.Red.B;
                pMarker3DPlacement1.Color = ClsGDBDataCommon.ColorToIColor(Color.Red);

                //地球方向
                IMarker3DSymbol pMarker3DSymbolEarth = new Marker3DSymbolClass();
                string EarthPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\EarthArrow.3DS";
                pMarker3DSymbolEarth.CreateFromFile(EarthPath);
                IMarker3DPlacement pMarker3DPlacement2 = pMarker3DSymbolEarth as IMarker3DPlacement;
                pMarker3DPlacement2.Size = pMarkerSize;

                double EarthAngleX = EarIncli.Value - 90;
                double EarthAngleY = 0;
                double EarthAngleZ = 0 - EarAzimuth.Value;
                pMarker3DPlacement2.SetRotationAngles(EarthAngleX, EarthAngleY, EarthAngleZ);

                //IRgbColor pRgbColorEarth = new RgbColorClass();
                //pRgbColorEarth.Red = Color.BlueViolet.R;
                //pRgbColorEarth.Green = Color.BlueViolet.G;
                //pRgbColorEarth.Blue = Color.BlueViolet.B;
                pMarker3DPlacement2.Color = ClsGDBDataCommon.ColorToIColor(Color.BlueViolet);

                //合并成一个整体
                IMultiLayerMarkerSymbol pMultiLayerMarkerSymbol = new MultiLayerMarkerSymbolClass();
                pMultiLayerMarkerSymbol.AddLayer(pMarker3DSymbolNorth as IMarkerSymbol);
                pMultiLayerMarkerSymbol.AddLayer(pMarker3DSymbolSun as IMarkerSymbol);
                pMultiLayerMarkerSymbol.AddLayer(pMarker3DSymbolEarth as IMarkerSymbol);

                pMarkerElement.Symbol = pMultiLayerMarkerSymbol;
                IElement pElement = pMarkerElement as IElement;

                //确定添加位置
                IPoint pPoint = new PointClass();
                pPoint.X = PageX;
                pPoint.Y = PageY;
                pPoint.Z = PageZ+PageZ/10;

                IZAware zAware = pPoint as IZAware;
                zAware.ZAware = true;
                IGeometry pointGeometry = pPoint as IGeometry;
                pointGeometry.SpatialReference = pSceneControl.Scene.SpatialReference;
                pElement.Geometry = pointGeometry;

                graphicsContainer3D.AddElement(pElement);
                pSceneControl.Scene.SceneGraph.RefreshViewers();
            }
            catch
            {
                MessageBox.Show(e.ToString ());
            }

            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
