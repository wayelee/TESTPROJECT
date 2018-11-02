using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.IO;
using System.Xml;
using LibCerMap;
using ESRI.ArcGIS.DataSourcesRaster;
using LibModelGen;

namespace LibCerMap
{
    public partial class FrmCreateImagePath : OfficeForm
    {
        public FrmCreateImagePath()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public ESRI.ArcGIS.Controls.AxMapControl m_pMapCtl;
        public IFeatureClass pPointFeatureclass;
        public IRaster pRaster;
        private void CreatePolyline()
        {
            try
            {
                //建立shape字段
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Shape";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //设置Geometry definition
                IGeometryDef pGeometryDef = new GeometryDefClass();
                IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pGeometryDef;
                pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline; //点、线、面
                pGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                pGeometryDefEdit.SpatialReference.SetDomain(-8000000, 8000000, -800000, 8000000);
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);
                IFeatureClass pFeatureClass = null;
                DirectoryInfo di = new DirectoryInfo(textBoxX1.Text);
                string filefolder = di.Parent.FullName;
                ClsGDBDataCommon comm = new ClsGDBDataCommon();
                IWorkspace inmemWor = comm.OpenFromShapefile(filefolder);
                // ifeatureworkspacee
                IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
                try
                {
                    pFeatureClass = pFeatureWorkspace.CreateFeatureClass(di.Name, pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");
                }
                catch (SystemException eee)
                {
                    if (eee.Message == "The table already exists.")
                    {
                        pFeatureClass = pFeatureWorkspace.OpenFeatureClass(textBoxX1.Text);
                        ITable ptable = pFeatureClass as ITable;
                        ptable.DeleteSearchedRows(null);
                    }
                    //MessageBox.Show(eee.Message);
                }
                List<Pt2d> ImagePoint = new List<Pt2d>();
                IQueryFilter pQF = new QueryFilterClass();
                IFeatureCursor pFC = pPointFeatureclass.Search(null,false); 
                IFeature pF = pFC.NextFeature();
                while (pF != null)
                {
                    IPoint pPoint = pF.Shape as IPoint;
                    Pt2d imgpoint = new Pt2d();
                    imgpoint.X = pPoint.X;
                    imgpoint.Y = pPoint.Y;
                    ImagePoint.Add(imgpoint);
                    pF = pFC.NextFeature();
                }
                Pt2d[] originPt = new Pt2d[ImagePoint.Count];
                for (int i = 0; i < ImagePoint.Count;i++ )
                {
                    Pt2d pttemp = new Pt2d();
                    pttemp.X = ImagePoint[i].X;
                    pttemp.Y = ImagePoint[i].Y;
                    originPt[i] = pttemp;
                }
               // Pt2d[] ptImagePos = new Pt2d[ImagePoint.Count];
                Pt2d[] ptImagePos = null;
                calImagePos(pRaster, originPt, textBoxX2.Text, out ptImagePos);
                IPointCollection pPointC = new PolylineClass();
                for(int i = 0;i<ptImagePos.Length;i++)
                {
                    if (ptImagePos[i] == null)
                    {
                        continue;
                    }
                   IPoint pPoint = new PointClass();
                   pPoint.X = ptImagePos[i].X;
                   pPoint.Y = ptImagePos[i].Y;
                   pPointC.AddPoint(pPoint);
                }
                ISegmentCollection pSegC = new PolylineClass();
                pSegC.AddSegment(pPointC as ISegment);
                IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                pFeatureTemp.Shape = pPointC as IGeometry;
                pFeatureTemp.Store();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            try
            {
                CreatePolyline();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonX_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCreateImagePath_Load(object sender, EventArgs e)
        {
            //初始化图层列表
            IEnumLayer pEnumLayer = m_pMapCtl.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = null;
            while ((pLayer = pEnumLayer.Next()) != null)
            {
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pfL = pLayer as IFeatureLayer;
                    esriGeometryType pType = pfL.FeatureClass.ShapeType;
                    if (pType == esriGeometryType.esriGeometryPoint)
                    {
                        comboBoxEx1.Items.Add(pLayer.Name);
                    }
                }
                if (pLayer is IRasterLayer)
                {
                    IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                    if (pRasterLayer.BandCount == 1)
                    {
                        comboBoxEx2.Items.Add(pLayer.Name);
                    }
                }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML文件(*.xml)|*.xml;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxX2.Text = dlg.FileName;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "shp文件(*.shp)|*.shp;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxX1.Text = dlg.FileName;
            }
        }

        //private void buttonX2_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.Filter = "shp文件(*.shp)|*.shp;|All files (*.*)|*.*";
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        if (comboBoxEx1.Items.Contains(dlg.FileName) == false)
        //        {
        //            //if(dlg.FileName)
        //            comboBoxEx1.Items.Add(dlg.FileName);
        //        }
        //    }
        //}
        public ClsCameraPara ParseXmlFileToGetPara(string szFilename)
        {
            try
            {
                ClsCameraPara result = new ClsCameraPara();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(szFilename);

                XmlNode cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dX");
                if (cs != null)
                {
                    result.dX = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dY");
                if (cs != null)
                {
                    result.dY = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dZ");
                if (cs != null)
                {
                    result.dZ = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dPhi");
                if (cs != null)
                {
                    result.dPhi = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dOmg");
                if (cs != null)
                {
                    result.dOmg = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dKappa");
                if (cs != null)
                {
                    result.dKappa = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dFocus");
                if (cs != null)
                {
                    result.dFocus = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dPx");
                if (cs != null)
                {
                    result.dPx = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dPy");
                if (cs != null)
                {
                    result.dPy = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/nW");
                if (cs != null)
                {
                    result.nW = int.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/nH");
                if (cs != null)
                {
                    result.nH = int.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/szImageName");
                if (cs != null)
                {
                    result.szImageName = cs.InnerText;
                }

                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public void calImagePos(IRaster pRaster, Pt2d[] ptGeoPos, string szXmlFile, out Pt2d[] ptImagePos)
        {
            ptImagePos = null;
            if (pRaster == null)
                return;

            try
            {
                ClsCameraPara cameraPara = ParseXmlFileToGetPara(szXmlFile);
                if (cameraPara == null)
                    return;

                ClsGetCameraView getCameraView = new ClsGetCameraView();
                Pt2i ptLeftTop = new Pt2i();

                IRasterProps pProps=pRaster as IRasterProps;
                int nWidth=pProps.Width;
                int nHeight=pProps.Height;

                double[,] dbData =new double[nWidth,nHeight];
                Point2D ptTmpLeftTop=new Point2D();
                ptTmpLeftTop.X=0;
                ptTmpLeftTop.Y=0;
                if (!getCameraView.readBlockDataToFile(ptTmpLeftTop, ref dbData, pRaster))
                    return;
                //double[,] dbData = getCameraView.getSubDem(pRaster, new Pt3d(), 0, ref ptLeftTop);

                int nCount = ptGeoPos.Length;
                ptImagePos = new Pt2d[nCount];
                for (int i = 0; i < nCount; i++)
                {
                    Pt2d pt2d = new Pt2d();
                    OriAngle oriAngle = new OriAngle();
                    oriAngle.kap = cameraPara.dKappa;
                    oriAngle.omg = cameraPara.dOmg;
                    oriAngle.phi = cameraPara.dPhi;

                    Matrix pRMat = new Matrix(3, 3);
                    ClsGetCameraView.OPK2RMat(oriAngle, ref pRMat);

                    double dbTmpZ = double.NaN;
                    getCameraView.GetGeoZ(pRaster, dbData, ptLeftTop, ptGeoPos[i].X, ptGeoPos[i].Y, ref dbTmpZ);
                    if (dbTmpZ == double.NaN)
                    {
                        ptImagePos[i] = null;
                        continue;
                    }
                    double dbX, dbY, dbZ;
                    dbX = pRMat.getNum(0, 0) * (ptGeoPos[i].X - cameraPara.dX) + pRMat.getNum(1, 0) * (ptGeoPos[i].Y - cameraPara.dY)
                        + pRMat.getNum(2, 0) * (dbTmpZ - cameraPara.dZ);
                    dbY = pRMat.getNum(0, 1) * (ptGeoPos[i].X - cameraPara.dX) + pRMat.getNum(1, 1) * (ptGeoPos[i].Y - cameraPara.dY)
                        + pRMat.getNum(2, 1) * (dbTmpZ - cameraPara.dZ);
                    dbZ = pRMat.getNum(0, 2) * (ptGeoPos[i].X - cameraPara.dX) + pRMat.getNum(1, 2) * (ptGeoPos[i].Y - cameraPara.dY)
                        + pRMat.getNum(2, 2) * (dbTmpZ - cameraPara.dZ);

                    Pt2d ptImageTmp = new Pt2d();
                    ptImageTmp.X = (int)Math.Round(-cameraPara.dFocus * dbX / dbZ);
                    ptImageTmp.Y = (int)Math.Round(-cameraPara.dFocus * dbY / dbZ);

                    ptImagePos[i] = ptImageTmp;

                }
                return;
            }
            catch (System.Exception ee)
            {
                MessageBox.Show(ee.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(m_pMapCtl.Map, comboBoxEx1.Text);
            if (pLayer is IFeatureLayer)
            {
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                pPointFeatureclass = pFeatureLayer.FeatureClass;
            }
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(m_pMapCtl.Map, comboBoxEx2.Text);
            if (pLayer is IRasterLayer)
            {
                IRasterLayer pFeatureLayer = pLayer as IRasterLayer;
                pRaster = pFeatureLayer.Raster;
            }
        }
     
    }
}
