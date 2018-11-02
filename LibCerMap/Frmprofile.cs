using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class Frmprofile : OfficeForm
    {
        IFeature pFeature = null;
        IMapControl3 pMapControl = null;
        public Frmprofile(IFeature feature,IMapControl3 mapcontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pFeature = feature;
            pMapControl = mapcontrol;
        }

        private void Frmprofile_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pMapControl.LayerCount; i++)
            {
                ILayer player = pMapControl.get_Layer(i);
                if (player is ITinLayer)
                {
                    cmblayer.Items.Add(player.Name);
                }
                else if (player is IRasterLayer)
                {
                    IRasterLayer pRLayer = (IRasterLayer)player;
                    if (pRLayer.BandCount == 1)
                    {
                        cmblayer.Items.Add(pRLayer.Name);
                    }
                }
            }
            cmblayer.SelectedIndex = 0;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                ILayer pLayer = null;
                IRasterLayer pRlayer = null;
                ITinLayer pTinlayer = null;
                ISurface pSurface=null;

                for (int i = 0; i < pMapControl.LayerCount;i++ )
                {
                    if (pMapControl.get_Layer(i).Name==cmblayer.SelectedItem.ToString())
                    {
                        pLayer = pMapControl.get_Layer(i) ;
                    }
                }
                if (pLayer is IRasterLayer)
                {
                    pRlayer = pLayer as IRasterLayer;
                    IRasterSurface rasterSurf = new RasterSurfaceClass();
                    rasterSurf.PutRaster(pRlayer.Raster, 0);
                    pSurface = rasterSurf as ISurface;
                } 
                else if (pLayer is ITinLayer)
                {
                    pTinlayer = pLayer as ITinLayer;
                    ITin ptin = pTinlayer.Dataset;
                    pSurface = (ISurface)ptin;
                }               
                IPolyline pPolyline = pFeature.ShapeCopy as IPolyline;
                IGeometry pProfLine;
                IPoint m_StartPoint = pPolyline.FromPoint;
                IPoint m_EndPoint = pPolyline.ToPoint;
                object size = new object();

                pSurface.GetProfile(pPolyline , out pProfLine , ref size);

                IPolyline pNewLine = pProfLine as IPolyline;
                double iii = pPolyline.Length;
                List<double> ZValue = new List<double>();
                List<double> DistanceValue = new List<double>();
                DistanceValue.Add(0);
                ZValue.Add(pNewLine.FromPoint.Z);

                for (int i = 1; i < (int)pNewLine.Length;i++ )
                {
                    DistanceValue.Add(i);
                    ICurve pCurve;
                    pNewLine.GetSubcurve(0, DistanceValue[i], false, out pCurve);
                    ZValue.Add(pCurve.ToPoint.Z);
                }
                DistanceValue.Add(pNewLine.Length);
                ZValue.Add(pNewLine.ToPoint.Z);

                double roffset = inoffset.Value / 2;//右轮偏移
                double loffset = -inoffset.Value / 2;//左轮偏移
                IPolyline pRLine = ConstructOffset(pPolyline, roffset);
                IPolyline pLLine = ConstructOffset(pPolyline, loffset);
                IGeometry pLProfLine;
                IGeometry pRProfLine;
                pSurface.GetProfile(pRLine, out pRProfLine, ref size);
                pSurface.GetProfile(pLLine, out pLProfLine, ref size);
                IPolyline pRNewline = pRProfLine as IPolyline;
                IPolyline pLNewline = pLProfLine as IPolyline;
                List<double> ZRValue = new List<double>();
                List<double> ZLValue = new List<double>();
                List<double> RDistanceValue = new List<double>();
                List<double> LDistanceValue = new List<double>();
                RDistanceValue.Add(0);
                LDistanceValue.Add(0);
                ZRValue.Add(pRNewline.FromPoint.Z);
                ZLValue.Add(pLNewline.FromPoint.Z);
                for (int j = 1; j < (int)pRNewline.Length;j++ )
                {
                    RDistanceValue.Add(j);
                    ICurve pCurve;
                    pRNewline.GetSubcurve(0, RDistanceValue[j], false, out pCurve);
                    ZRValue.Add(pCurve.ToPoint.Z);
                }
                RDistanceValue.Add(pRNewline.Length);
                ZRValue.Add(pRNewline.ToPoint.Z);
                for (int k = 1; k < (int)pLNewline.Length;k++ )
                {
                    LDistanceValue.Add(k);
                    ICurve pCurve;
                    pLNewline.GetSubcurve(0, LDistanceValue[k], false, out pCurve);
                    ZLValue.Add(pCurve.ToPoint.Z);
                }
                LDistanceValue.Add(pLNewline.Length);
                ZLValue.Add(pLNewline.ToPoint.Z);

                FrmprofileGraph frmgraph = new FrmprofileGraph(ZValue,DistanceValue,pNewLine,ZRValue,RDistanceValue,ZLValue,LDistanceValue);
                frmgraph.ShowDialog();
                frmgraph.ShowInTaskbar = false;
                frmgraph.StartPosition = FormStartPosition.CenterScreen;
#region ///

                ////用ISurface的 InterpolateShape方法，得到这条直线做剖面后的Geometry，方法说明详见帮助       

                //IGeometry OutShape;

                //object size = new object();
                //pSurface.InterpolateShape(pPolyline, out OutShape, ref size);

                ////将结果QI为 PointCollection，QI成功显示结果是直线上单一的几个离散点，InterpolateShape会自动将分析的结果直线上选取六个等分的离散点，以这六个点的值来作曲线图
                //IPointCollection pointCollection = OutShape as IPointCollection;
                //IMAware maware = pointCollection as IMAware;
                //maware.MAware = true;
                //IZAware zaware = pointCollection as IZAware;
                //zaware.ZAware = true;
                //IMSegmentation mseg = pointCollection as IMSegmentation;
                ////设置M的值为距离
                //mseg.SetMsAsDistance(false);

                //IPointCollection newPointColl = new PolylineClass();
                //int count = pointCollection.PointCount;
                //for (int i = 0; i < count; i++)
                //{
                //    IPoint point = pointCollection.get_Point(i);
                //    IPoint newPoint = new PointClass();
                //    newPoint.X = point.M;
                //    newPoint.Y = point.Z;
                //    object missing = Type.Missing;
                //    newPointColl.AddPoint(newPoint, ref missing, ref missing);
                //}

                //#region

                ////IWorkspaceFactory wsFactory = new ShapefileWorkspaceFactoryClass();

                ////IWorkspaceName workspaceName = wsFactory.Create("F:", "temp", null, 0);

                //IWorkspaceFactory workspaceFactory = new InMemoryWorkspaceFactoryClass();
                //IWorkspaceName workspaceName = workspaceFactory.Create("", "MyWorkspace", null, 0);
                //IName name = workspaceName as IName;
                //IWorkspace workspace = name.Open() as IWorkspace;

                //IFields fields = new FieldsClass();
                //IFieldsEdit fieldsEdit = fields as IFieldsEdit;
                //IField field = new FieldClass();
                //IFieldEdit fieldEdit = field as IFieldEdit;
                //fieldEdit.Name_2 = "OID";
                //fieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                //fieldsEdit.AddField(field);

                //IGeometryDef geometryDef = new GeometryDefClass();
                //IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
                //geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                //ISpatialReference spatialRef = new UnknownCoordinateSystemClass();
                //geometryDefEdit.SpatialReference_2 = (pRlayer as IGeoDataset).SpatialReference;


                //field = new FieldClass();
                //fieldEdit = field as IFieldEdit;
                //fieldEdit.Name_2 = "Shape";
                //fieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //fieldEdit.GeometryDef_2 = geometryDef;
                //fieldsEdit.AddField(field);

                //IFieldChecker fieldChecker = new FieldCheckerClass();
                //IEnumFieldError enumFieldError = null;
                //IFields validatedFields = null;
                //fieldChecker.ValidateWorkspace = workspace;
                //fieldChecker.Validate(fields, out enumFieldError, out validatedFields);


                //IFeatureClass featureClass = (workspace as IFeatureWorkspace).CreateFeatureClass("test",
                //    validatedFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

                //IFeatureCursor featureCursor = featureClass.Insert(true);
                //IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
                //featureBuffer.Shape = pFeature as IGeometry;
                //featureCursor.InsertFeature(featureBuffer);
                //featureCursor.Flush();

                //#endregion

                //IFeatureLayer featureLayer = new FeatureLayerClass();
                //featureLayer.FeatureClass = featureClass;
                //featureLayer.Name = featureClass.AliasName;

                //pMapControl.AddLayer(featureLayer as ILayer);
                //pMapControl.Refresh();



  ////建立featureClass，为后面曲线图提供数据

                //IWorkspaceFactory workspaceFactory = new InMemoryWorkspaceFactoryClass();
                //IWorkspaceName workspaceName = workspaceFactory.Create("", "MyWorkspace", null, 0);
                //IName name = (IName)workspaceName;
                //IWorkspace inmemWor = (IWorkspace)name.Open();

                //IFields fields = new FieldsClass();
                //IFieldsEdit fieldsEdit = fields as IFieldsEdit;
                //IField oidField = new FieldClass();
                //IFieldEdit oidFieldEdit = oidField as IFieldEdit;
                //oidFieldEdit.Name_2 = "OID";
                //oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                //fieldsEdit.AddField(oidField);

                //IGeometryDef geometryDef = new GeometryDefClass();
                //IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
                //geometryDefEdit.GeometryType_2 = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint;
                //geometryDefEdit.HasM_2 = true;
                //geometryDefEdit.HasZ_2 = true;
                //ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                //ISpatialReference spatialReference = pMapControl.SpatialReference;//spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                //ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference; spatialReferenceResolution.ConstructFromHorizon();
                //ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference; spatialReferenceTolerance.SetDefaultXYTolerance();
                //geometryDefEdit.SpatialReference_2 = spatialReference;

                //IField geometryField = new FieldClass();
                //IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
                //geometryFieldEdit.Name_2 = "Shape";
                //geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //geometryFieldEdit.GeometryDef_2 = geometryDef;
                //fieldsEdit.AddField(geometryField);

                //IField mField = new FieldClass();
                //IFieldEdit mFieldEdit = (IFieldEdit)mField;
                //mFieldEdit.Name_2 = "M";
                //mFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                //mFieldEdit.Length_2 = 8;
                //fieldsEdit.AddField(mField);

                //IField zField = new FieldClass();
                //IFieldEdit zFieldEdit = (IFieldEdit)zField;
                //zFieldEdit.Name_2 = "Z";
                //zFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                //zFieldEdit.Length_2 = 8;
                //fieldsEdit.AddField(zField);

                //IFieldChecker fieldChecker = new FieldCheckerClass();
                //IEnumFieldError enumFieldError = null;
                //IFields validatedFields = null;
                //fieldChecker.ValidateWorkspace = inmemWor;
                //fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

                //IFeatureClass featureClass = (inmemWor as IFeatureWorkspace).CreateFeatureClass("test", validatedFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

                //IFeatureCursor cursor = featureClass.Insert(true);
                //IFeatureBuffer buffer = featureClass.CreateFeatureBuffer();
                //int count = pointCollection.PointCount;
                //for (int i = 0; i < count; ++i)
                //{
                //    IPoint p = pointCollection.get_Point(i);
                //    buffer.Shape = p as IGeometry;
                //    buffer.set_Value(buffer.Fields.FindField("M"), p.M);
                //    buffer.set_Value(buffer.Fields.FindField("Z"), p.Z);
                //    cursor.InsertFeature(buffer);
                //}

                //cursor.Flush();

                ////开始做曲线图

#endregion

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("该图像不能做剖面分析", "提示", MessageBoxButtons.OK);
            }
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 求出车轮行走线，右轮offset为正
        /// </summary>
        /// <param name="inPolyline"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IPolyline ConstructOffset(IPolyline inPolyline, double offset)
        {
            if (inPolyline == null || inPolyline.IsEmpty)
            {
                return null;
            }
            object Missing = Type.Missing;
            IConstructCurve constructCurve = new PolylineClass();
            constructCurve.ConstructOffset(inPolyline, offset, ref Missing, ref Missing);
            return constructCurve as IPolyline;
        }

    }
}
