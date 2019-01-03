using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Drawing.Text;
using stdole;
using DevComponents.DotNetBar;
using AOFunctions.GDB;

namespace LibCerMap
{
    public partial class FrmCenterLineInsideReport : OfficeForm
    {
        //点图层的投影信息
        ISpatialReference psf = null;

        IMapControl3 pMapcontrol;

        public FrmCenterLineInsideReport(IMapControl3 mapcontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapcontrol = mapcontrol;
        }

        private void FrmPointToLine_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pMapcontrol.LayerCount; i++)
            {
                ILayer pLayer = null;
                if (pMapcontrol.get_Layer(i) is IFeatureLayer)
                {
                    pLayer = pMapcontrol.get_Layer(i);
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFeatureClass=pFeatureLayer .FeatureClass ;
                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                    {
                        cboBoxPointLayer.Items.Add(pLayer.Name);
                    }
                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        comboBoxExCenterlineLayer.Items.Add(pLayer.Name);
                    }
                }
            }
            if (cboBoxPointLayer.Items.Count > 0)
            {
                cboBoxPointLayer.SelectedIndex = 0;
            }
            if (comboBoxExCenterlineLayer.Items.Count > 0)
            {
                comboBoxExCenterlineLayer.SelectedIndex = 0;
            }

        }
      
        //添加点图层按钮
        private void btAddPoint_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "添加点图层";
            OpenFile.Filter = "(*.shp)|*.shp";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                cboBoxPointLayer.Items.Add(System .IO .Path .GetFileNameWithoutExtension( OpenFile.FileName));

                try
                {
                    //获得文件路径
                    string filePath = System.IO.Path.GetDirectoryName(OpenFile.FileName);
                    //获得文件名称
                    string fileNam = System.IO.Path.GetFileName(OpenFile.FileName);
                    //加载shape文件
                    pMapcontrol.AddShapeFile(filePath, fileNam);
                    pMapcontrol.Refresh();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cboBoxPointLayer.SelectedIndex = 0;
            }
        }
     
        //保存线文件
        private void btAddLine_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Title = "输入保存文件路径及名称";
            SaveFile.Filter = "(*.shp)|*.shp";
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
            //    txtLineFilePath.Text = SaveFile.FileName;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            string pPointFileName = cboBoxPointLayer.SelectedItem.ToString();
            string pCenterlinName = comboBoxExCenterlineLayer.SelectedItem.ToString();
            IFeatureLayer pPointLayer = null;
            IFeatureLayer pCenterlineLayer = null;
            for (int i = 0; i < pMapcontrol.LayerCount; i++)
            {
                if (pPointFileName == pMapcontrol.get_Layer(i).Name)
                {
                    pPointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                }
                if (pCenterlinName == pMapcontrol.get_Layer(i).Name)
                {
                    pCenterlineLayer = pMapcontrol.get_Layer(i)as IFeatureLayer;
                }
            }
            IFeatureClass pLineFC = pCenterlineLayer.FeatureClass;
            IFeatureClass pPointFC = pPointLayer.FeatureClass;
            IQueryFilter pQF = null;
            IFeatureCursor pLineCursor = pLineFC.Search(null, false);
            IFeatureCursor pPointCursor = pPointFC.Search(null, false);
            IFeature pLineFeature =  pLineCursor.NextFeature();
            DataTable ptTable = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pPointFC as ITable, pQF);
            ptTable.Columns.Add("长度");
            ptTable.Columns.Add("距离");
            ptTable.Columns.Add("中间点坐标"); 
            if (pLineFeature != null)
            {
                 ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                //wgs 84
                IGeographicCoordinateSystem wgs84 = spatialReferenceFactory.CreateGeographicCoordinateSystem(4326) as IGeographicCoordinateSystem;
                IUnit meterUnit = spatialReferenceFactory.CreateUnit((int)ESRI.ArcGIS.Geometry.esriSRUnitType.esriSRUnit_Meter);
                IPolyline pline = pLineFeature.Shape as IPolyline;
                pline.SpatialReference = wgs84;

                IProximityOperator pPO = pline as IProximityOperator;
                IFeature ptFeature = pPointCursor.NextFeature();
                int idx = 0;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialReferenceFactory);
                while (ptFeature != null)
                {
                    DataRow row = ptTable.Rows[idx];
                    IPoint pt = ptFeature.Shape as IPoint;
                    pt.SpatialReference = wgs84;
                    IPoint nearestpt =  pPO.ReturnNearestPoint(pt, esriSegmentExtension.esriNoExtension);
                   double distance  = DataAlignment.DataAlignment.CalculateDistanceBetween84TwoPoints(pt, nearestpt);
                  // = pPO.ReturnDistance(pt);

                    row["距离"] = distance;
                    if (idx == 0)
                    {
                        row["长度"] = 0;
                    }
                    else
                    {
                        DataRow preRow = ptTable.Rows[idx - 1];
                        row["长度"] = Convert.ToDouble(row[EvConfig.IMUMoveDistanceField]) - Convert.ToDouble(preRow[EvConfig.IMUMoveDistanceField]);
                    }
                    ptFeature = pPointCursor.NextFeature();
                    idx++;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialReferenceFactory);
            }
           

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("名称");
            dt2.Columns.Add("长度");
            dt2.Columns.Add("比例");

          //  List<DataRow> rowlist = (dt2.Rows.Where(e => Convert.ToDouble(e["距离"]) <= 2)).ToList();

            List<DataRow> less1 = (from DataRow r in ptTable.Rows
                      where Convert.ToDouble(r["距离"]) <= 1
                      select r).ToList();
            double length1 = less1.Sum(x => Convert.ToDouble(x["长度"]));
            int sum1 = less1.Count;

            List<DataRow> Great2 = (from DataRow r in ptTable.Rows
                                   where Convert.ToDouble(r["距离"]) >2 
                                   select r).ToList();
            double length2 = Great2.Sum(x => Convert.ToDouble(x["长度"]));
            int sum2 = Great2.Count;

            List<DataRow> Medium2 = (from DataRow r in ptTable.Rows
                                     where Convert.ToDouble(r["距离"]) > 1 && Convert.ToDouble(r["距离"]) <=2
                                    select r).ToList();
            double length3 = Medium2.Sum(x => Convert.ToDouble(x["长度"]));
            int sum3 = Medium2.Count;

            DataRow r1;
            r1 = dt2.Rows.Add();
            r1[0] = "≤1米";
            r1[1] = length1;
            r1[2] = 1.0 * sum1 / (sum1 + sum2 + sum3);
          
            r1 = dt2.Rows.Add();
            r1[0] = "＞1米且≤2米";
            r1[1] = length3;
            r1[2] = 1.0 *  sum3 / (sum1 + sum2 + sum3);
            

            r1 = dt2.Rows.Add();
            r1[0] = "大于2米";
            r1[1] = length2;
            r1[2] = 1.0 *  sum2 / (sum1 + sum2 + sum3);
             

            List<DataTable> tablelist = new List<DataTable>();
            tablelist.Add(ptTable);
            tablelist.Add(dt2);

            FRMInsideInspectionReport frm = new FRMInsideInspectionReport(tablelist);
            frm.ShowDialog();
        }

        //从点图层中收集所有点
        public IPointCollection ReadPoint(IFeatureLayer pFeatureLayer)
        {
            IFeatureCursor pFeatureCursor = pFeatureLayer.Search(null, false);

            //获取数据库或者单个文件的第一个属性字段
            IFeature pFeature = pFeatureCursor.NextFeature();
            IField pField = null;
            if (pFeatureLayer.FeatureClass.Fields.FindField("FID") != -1)
            {
                pField = pFeatureLayer.FeatureClass.Fields.get_Field(pFeatureLayer.FeatureClass.Fields.FindField("FID"));
            }
            else if (pFeatureLayer.FeatureClass.Fields.FindField("OBJECTID") != -1)
            {
                pField = pFeatureLayer.FeatureClass.Fields.get_Field(pFeatureLayer.FeatureClass.Fields.FindField("OBJECTID"));
            }
            
            //第一个属性字段名称
            string FirstFieldName = pField.AliasName;
          
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = FirstFieldName + ">=0";
            int number = pFeatureLayer.FeatureClass.FeatureCount(pQueryFilter);

            //DataTable dt = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pFeatureLayer.FeatureClass as ITable, "");
            //DataAlignment.DataAlignment.CanlculateDistanceInMiddlePointTable(dt);

            IPointCollection pPointCollection = new MultipointClass();
            IPoint PrevPT = null;
            for (int i = 0; i < number; i++)
            {
                IGeometry pGeometry = pFeature.Shape as IGeometry;
                IPoint pt = pGeometry as IPoint;               
                IPoint pPoint = new PointClass();

                IZAware zpt = pPoint as IZAware;
                zpt.ZAware = true;
                IMAware mpt = pPoint as IMAware;
                mpt.MAware = true;

                pPoint.PutCoords(pt.X, pt.Y);
                pPoint.Z = Convert.ToDouble(pFeature.Value[pFeature.Fields.FindField(EvConfig.CenterlineZField)]);

                if (i == 0)
                {
                    pPoint.M = 0;
                    PrevPT = pPoint;
                }
                else
                {

                    pPoint.M = PrevPT.M + DataAlignment.DataAlignment.CalculateDistanceBetween84TwoPoints(pPoint, PrevPT);

                    PrevPT = pPoint;
                }
                pPointCollection.AddPoint(pPoint);

                pFeature = pFeatureCursor.NextFeature();
            }

            return pPointCollection;
        }
       
        //将收集到的点转换成线
        private IPolyline CreatePolyline(IPointCollection pPointcollection)
        {
            int PointNumber = int.Parse(pPointcollection .PointCount.ToString ());

            object o = Type.Missing;
          
            //线数组
            ISegmentCollection pSegmentCollection = new PolylineClass();
            IZAware z = pSegmentCollection as IZAware;
            IMAware m = pSegmentCollection as IMAware;
            z.ZAware = true;
            m.MAware = true;

            for (int i = 0; i < PointNumber-1; i++)
            {
                ILine pLine = new LineClass();
             
                pLine.PutCoords(pPointcollection .get_Point(i),pPointcollection.get_Point(i+1));

                pSegmentCollection.AddSegment((ISegment)pLine, ref o, ref o);
            }
            IPolyline pPolyline = new PolylineClass();
            pPolyline = pSegmentCollection as IPolyline;

            return pPolyline;
        }

        //创建新图层
        private void CreateNewLineLayer()
        {
         

           
        }

        //将线添加到图层
        private void AddFeature(IGeometry geometry)
        {
           
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
