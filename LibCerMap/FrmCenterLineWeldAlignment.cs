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
    public partial class FrmCenterLineWeldAlignment : OfficeForm
    {
        //点图层的投影信息
        ISpatialReference psf = null;

        IMapControl3 pMapcontrol;

        public FrmCenterLineWeldAlignment(IMapControl3 mapcontrol)
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
                        comboBoxExCenterlineLinearLayer.Items.Add(pLayer.Name);
                    }
                }
            }
            if (cboBoxPointLayer.Items.Count > 0)
            {
                cboBoxPointLayer.SelectedIndex = 0;
            }
            if (comboBoxExCenterlineLinearLayer.Items.Count > 0)
            {
                comboBoxExCenterlineLinearLayer.SelectedIndex = 0;
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
            try
            {
                string pPointFileName = cboBoxPointLayer.SelectedItem.ToString();
                string pCenterlinName = comboBoxExCenterlineLinearLayer.SelectedItem.ToString();
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
                        pCenterlineLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                }
                IFeatureClass pLineFC = pCenterlineLayer.FeatureClass;
                IFeatureClass pPointFC = pPointLayer.FeatureClass;
                IFeatureCursor pLineCursor = pLineFC.Search(null, false);                
                IFeature pLineFeature = pLineCursor.NextFeature();
                IQueryFilter qf1 = null;
                DataTable ptable = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pPointFC as ITable, qf1);
                if (!ptable.Columns.Contains("X"))
                    ptable.Columns.Add("X", System.Type.GetType("System.Double"));
                if (!ptable.Columns.Contains("Y"))
                    ptable.Columns.Add("Y", System.Type.GetType("System.Double"));
                 if (!ptable.Columns.Contains("Z"))
                    ptable.Columns.Add("Z", System.Type.GetType("System.Double"));
                if (!ptable.Columns.Contains("对齐里程"))
                    ptable.Columns.Add("对齐里程", System.Type.GetType("System.Double"));
                if (!ptable.Columns.Contains("距离偏移"))
                    ptable.Columns.Add("距离偏移", System.Type.GetType("System.Double"));
                ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                //wgs 84
                IGeographicCoordinateSystem wgs84 = spatialReferenceFactory.CreateGeographicCoordinateSystem(4326) as IGeographicCoordinateSystem;
                IUnit meterUnit = spatialReferenceFactory.CreateUnit((int)ESRI.ArcGIS.Geometry.esriSRUnitType.esriSRUnit_Meter);
              
                IPolyline pline = pLineFeature.Shape as IPolyline;      
                pline.SpatialReference = wgs84;

                IProximityOperator pPO = pline as IProximityOperator;
                
                int idx = 0;
                for (int i = 0; i < ptable.Rows.Count; i++)
                {
                    DataRow r = ptable.Rows[i];
                    IPoint pt = new PointClass();
                    pt.X = Convert.ToDouble(r[EvConfig.WeldXField]);
                    pt.Y = Convert.ToDouble(r[EvConfig.WeldYField]);
                    pt.SpatialReference = wgs84;
                    IPoint ptInLine = pPO.ReturnNearestPoint(pt, esriSegmentExtension.esriNoExtension);
                    r["X"] = ptInLine.X;
                    r["Y"] = ptInLine.Y;
                    r["对齐里程"] = ptInLine.M;
                    r["Z"] = ptInLine.Z;
                    
                    r["距离偏移"] = Math.Round( DataAlignment.DataAlignment.CalculateDistanceBetween84TwoPoints(pt, ptInLine),2);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialReferenceFactory);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pLineCursor);


                FrmIMUAlignmentresult frm = new FrmIMUAlignmentresult(ptable);
                frm.setResultType("焊缝对齐报告");
                frm.ShowDialog();
                   
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
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
