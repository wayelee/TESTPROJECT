using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using MSWord = Microsoft.Office.Interop.Word;
using DevExpress.XtraCharts;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;
using Point = System.Drawing.Point;
using System.Dynamic;
using System.Collections;

namespace LibCerMap
{
    public partial class FrmIMUCeterlineAlignmentresult : OfficeForm
    {         
        private DataTable sourcetable;
        private string m_ResultType;
        private string TempImagePath;

        //内检测对齐中线
        public DataTable CenterlinePointTable;
        public DataTable InsidePointTable;
        public double InsideCenterlineTolerance;
        public ILayer CenterlineLayer;

        // 两次内检测对齐
        public DataTable BasePointTable;
        public DataTable AlignmentPointTable;
        private List<ToolStripItem> ToolList = new List<ToolStripItem>();


        private Dictionary<DataRow, DataRow> MatchedRowList = new Dictionary<DataRow, DataRow>();
        private List<DataRow> MatchedRowListSelection = new List<DataRow>();
        private ChartZoomScrollHelper chartZoomHelper;

        public FrmIMUCeterlineAlignmentresult(DataTable tb, Dictionary<DataRow, DataRow> matchrow )
        {
            InitializeComponent();
            sourcetable = tb;
            MatchedRowList = matchrow;
            TempImagePath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\TempImage.jpg";
            chartZoomHelper = new ChartZoomScrollHelper(chartControl3);
        }
        public FrmIMUCeterlineAlignmentresult()
        {
            InitializeComponent();         
            TempImagePath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\TempImage.jpg";
        } 
      
        private void ToolButton_Click(object sender, EventArgs e)
        {
            ToolStripButton b = sender as ToolStripButton;
            b.CheckState = CheckState.Checked;
            foreach (ToolStripButton tb in ToolList)
            {
                if (!tb.Equals(b))
                {
                    tb.CheckState = CheckState.Unchecked;
                }
            }
            if (sender.Equals(toolStripButtonManualMatch) )
            {
                chartZoomHelper.CurrentTool = ChartControlToolTypeEnum.ManualMatch;
            }
            if (sender.Equals(toolStripButtonSelect)  )
            {
                chartZoomHelper.CurrentTool = ChartControlToolTypeEnum.SelectPair;
            }
        }

        public static IEnumerable GetData(List<DataRow> RowList)
        {
            foreach (DataRow data in RowList.AsEnumerable())
            {
                yield return GetElement(data, data.Table.Columns);
            }
        }
        static object GetElement(DataRow dataRow, DataColumnCollection columns)
        {
            var element = (IDictionary<string, object>)new ExpandoObject();
            foreach (DataColumn column in columns)
            {
                element.Add(column.ColumnName, dataRow[column.ColumnName]);
            }
            return element;
        }

        private bool PointHitChartData(Point pt, SeriesPoint datapt, Axis2D xAxis, Axis2D yAxis)
        {
            ControlCoordinates cod;
             ControlCoordinates cod2;
            XYDiagram diagram = (XYDiagram)chartControl3.Diagram;
            cod = diagram.DiagramToPoint(datapt.Argument, datapt.Values[0], xAxis, yAxis);
            cod2 = diagram.DiagramToPoint(datapt.Argument, 0, xAxis, yAxis);
            if (Math.Abs(cod.Point.X - pt.X) <=5 && (pt.Y - cod2.Point.Y) * (pt.Y - cod.Point.Y) <=0 )
            {
                return true;
            }
            else
            {
                return false;
            }             
        }

        private DataRow GetHitDataRow(Point FirstPoint, DevExpress.XtraCharts.Series ss, Axis2D xAxis, Axis2D yAxis)
        {
            XYDiagram diagram = (XYDiagram)chartControl3.Diagram; 
            
            if (ss.Points.Any(x => PointHitChartData(FirstPoint, x as SeriesPoint ,xAxis, yAxis)))
            {
                SeriesPoint pt = ss.Points.Where(x => PointHitChartData(FirstPoint, x as SeriesPoint ,xAxis, yAxis)).First() as SeriesPoint;
                //centerline table
               if( ss.Equals(chartControl3.Series[0]))
                {
                    return CenterlinePointTable.Rows[(int)pt.Tag];
                }
               if (ss.Equals(chartControl3.Series[1]))
               {
                   return sourcetable.Rows[(int)pt.Tag];
               }
            }
            return null;
        }

        private bool ValidManuallySelectMatchRow(DataRow IMURow, DataRow Centerlinerow)
        {
            if (MatchedRowList.Count > 0)
            {
                double newIMUM = Convert.ToDouble(IMURow[EvConfig.IMUMoveDistanceField]);
                double newCenterlineM = Convert.ToDouble(Centerlinerow[EvConfig.CenterlineMeasureField]);
                for (int i = 0; i < MatchedRowList.Count; i++)
                {
                    double previewIMUM = Convert.ToDouble(MatchedRowList.Keys.ElementAt(i)[EvConfig.IMUMoveDistanceField]);
                    double previewCentlineM = Convert.ToDouble(MatchedRowList.Values.ElementAt(i)[EvConfig.CenterlineMeasureField]);

                    if ((newIMUM - previewIMUM) * (newCenterlineM - previewCentlineM) <= 0)
                    {
                        MessageBox.Show("特征点不能交叉匹配.");
                        return false;
                    }
                }
            }
            return true;

        }
        private void ChartControl3_MouseDragCompleted(object sender, Point FirstPoint, Point SecondPoint)
        {
           
            if (chartZoomHelper.CurrentTool == ChartControlToolTypeEnum.ManualMatch)
            {
                 XYDiagram diagram = (XYDiagram)chartControl3.Diagram;
                 if (GetHitDataRow(FirstPoint, chartControl3.Series[0], diagram.AxisX, diagram.SecondaryAxesY[0]) != null &&
                     GetHitDataRow(SecondPoint, chartControl3.Series[1], diagram.AxisX, diagram.AxisY) != null)
                 {
                     DataRow centerlinerow = GetHitDataRow(FirstPoint, chartControl3.Series[0], diagram.AxisX, diagram.SecondaryAxesY[0]);
                     DataRow IMURow = GetHitDataRow(SecondPoint, chartControl3.Series[1], diagram.AxisX, diagram.AxisY);
                     if (MatchedRowList.Keys.Contains(IMURow) == false && MatchedRowList.Values.Contains(centerlinerow) == false)
                     {
                         if (ValidManuallySelectMatchRow(IMURow, centerlinerow))
                         {
                             MatchedRowList.Add(IMURow, centerlinerow);
                         }                            
                     }
                     
                 }
                 if (GetHitDataRow(SecondPoint, chartControl3.Series[0], diagram.AxisX, diagram.SecondaryAxesY[0]) != null &&
                      GetHitDataRow(FirstPoint, chartControl3.Series[1], diagram.AxisX, diagram.AxisY) != null)
                 {
                     DataRow centerlinerow = GetHitDataRow(SecondPoint, chartControl3.Series[0], diagram.AxisX, diagram.SecondaryAxesY[0]);
                     DataRow IMURow = GetHitDataRow(FirstPoint, chartControl3.Series[1], diagram.AxisX, diagram.AxisY);
                     if (MatchedRowList.Keys.Contains(IMURow) == false && MatchedRowList.Values.Contains(centerlinerow)== false)
                     {
                         if (ValidManuallySelectMatchRow(IMURow, centerlinerow))
                         {
                             MatchedRowList.Add(IMURow, centerlinerow);
                         }
                            
                     }
                 }
                
            }
            if (chartZoomHelper.CurrentTool == ChartControlToolTypeEnum.SelectPair)
            {
                MatchedRowListSelection.Clear();
                DiagramCoordinates cod;
                XYDiagram diagram = (XYDiagram)chartControl3.Diagram;
                if (FirstPoint != null && SecondPoint != null)
                {
                    // int minX = Math.Min(FirstPoint.Value.X, SecondPoint.Value.X);
                    cod = diagram.PointToDiagram(new Point(FirstPoint.X, FirstPoint.Y));
                    double x1 = cod.NumericalArgument;

                    // int maxX = Math.Max(FirstPoint.Value.X, SecondPoint.Value.X);
                    cod = diagram.PointToDiagram(new Point(SecondPoint.X, SecondPoint.Y));
                    double x2 = cod.NumericalArgument;
                    double maxM = Math.Max(x1, x2);
                    double minM = Math.Min(x1, x2);
                    MatchedRowListSelection.AddRange(sourcetable.AsEnumerable().Where(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]) > minM &&
                      Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]) < maxM));
                }
            }
        }

        private void FrmIMUAlignmentresult_Load(object sender, EventArgs e)
        {
            ToolList.Add(toolStripButtonManualMatch);
            ToolList.Add(toolStripButtonSelect);
            foreach(ToolStripButton b in ToolList)
            { 
                b.Click += ToolButton_Click;
            }

             
            gridControl1.DataSource = sourcetable;
            gridControl1.Refresh();
            gridControlCenterline.DataSource = CenterlinePointTable;
            gridControlCenterline.Refresh();
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridViewCenterline.OptionsView.ColumnAutoWidth = false;

            //gridControlMatch.DataSource = GetData( MatchedRowList.Keys.ToList());
            //gridControlMatch.Refresh();

            chartZoomHelper.MouseDragCompleted += this.ChartControl3_MouseDragCompleted;

            if (m_ResultType == "内检测对齐中线报告")
            {
                CreateInsidPointCenterlineAlignmentStatisticsImage(this.chartControl3);
                this.chartControl3.CustomDrawSeriesPoint += this.chartControl3_CustomDrawSeriesPoint;
                this.gridControlPrecision.DataSource = CreateInsidPointCenterlineAlignmentStatisticsTable();
                this.buttonItemNeijianceZhongxian.Visible = true;

            }
            if (m_ResultType == "两次内检测对齐报告")
            {
                CreateInsideToInsideAlignmentStatisticsImage(this.chartControl3);
                this.chartControl3.CustomDrawSeriesPoint += this.chartControl3_CustomDrawSeriesPoint;
                this.gridControlPrecision.DataSource = CreateInsideToInsideAlignmentStatisticsTable();
                this.buttonItemNeijianceNeijiance.Visible = true;
            }
            if (m_ResultType == "焊缝对齐报告")
            {
                CreateWeldToCenterlineOffsetTableImage(this.chartControl3); 
                this.gridControlPrecision.DataSource = CreateWeldToCenterlineTable();
                //this.buttonItemHanfengDuiqi.Visible = true;
            }
            if (m_ResultType == "外检测对齐中线报告")
            {
                CreateWaijianceToCenterlineOffsetTableImage(this.chartControl3);
                buttonReport.Visible = false;              
                this.bar1.Visible = false;
            }
            if (m_ResultType == "两次外检测对齐报告")
            {                
                buttonReport.Visible = false;
                buttonItemWaijiance.Visible = true;
                this.barChart.Visible = false;
                this.bar1.Visible = false;
            }
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Dim ps As New DevExpress.XtraPrinting.PrintingSystem()
            //    Dim compositeLink As New DevExpress.XtraPrintingLinks.CompositeLink()
            //    compositeLink.PrintingSystem = ps

            //    For Each C1 As Object In devGridList
            //        Dim link As New DevExpress.XtraPrinting.PrintableComponentLink()
            //        link.Component = C1
            //        compositeLink.Links.Add(link)
            //    Next

            //    If String.IsNullOrEmpty(file) Then
            //        compositeLink.ShowPreview()
            //    Else
            //        Select Case mode
            //            Case 1
            //                compositeLink.ExportToPdf(file)
            //            Case 2
            //                compositeLink.ExportToHtml(file)
            //            Case 3
            //                compositeLink.CreateDocument()
            //                compositeLink.CreatePageForEachLink()
            //                Dim ExportOpt As DevExpress.XtraPrinting.XlsxExportOptions = New DevExpress.XtraPrinting.XlsxExportOptions()
            //                ExportOpt.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFilePageByPage
            //                compositeLink.ExportToXlsx(file, ExportOpt)
            //        End Select
            //    End If
            //End If


            try
            {
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                SaveFileDialog1.Filter = "xlsx Files (*.xlsx)|*.xlsx";
                SaveFileDialog1.DefaultExt = "xlsx";
                if (SaveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return; ;
                string filepath = SaveFileDialog1.FileName;

                DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
                compositeLink.PrintingSystem = ps;

                DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink();
                link.Component = gridControl1;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);

                link = new DevExpress.XtraPrinting.PrintableComponentLink();
                link.Component = chartControl3;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);

                link = new DevExpress.XtraPrinting.PrintableComponentLink();
                link.Component = gridControlPrecision;
                link.PrintingSystem = ps;
                compositeLink.Links.Add(link);

                compositeLink.CreateDocument();
                compositeLink.CreatePageForEachLink();
                DevExpress.XtraPrinting.XlsxExportOptions ExportOpt = new DevExpress.XtraPrinting.XlsxExportOptions();
                ExportOpt.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFilePageByPage;

                compositeLink.ExportToXlsx(filepath, ExportOpt);
                MessageBox.Show("导出成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonExportToShapeFIle_Click(object sender, EventArgs e)
        {
            SaveFileDialog di = new SaveFileDialog();
            di.Filter = "shape Files (*.shp)|*.shp";
            di.DefaultExt = "shp";
            if (di.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            DataTable dt = gridControl1.DataSource as DataTable;
            IFeatureClass pfc = CreateShapeFile(di.FileName);
            IFeatureCursor pfCursor = pfc.Insert(true);
            CustomizedControls.StatusForm frm = new CustomizedControls.StatusForm();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();

            try
            {
                int idx = 1;
                foreach (DataRow r in dt.Rows)
                {
                    frm.Status = "生成点" + idx.ToString() + "/" + dt.Rows.Count.ToString();
                    frm.Progress = Convert.ToInt16(Convert.ToDouble(idx) / dt.Rows.Count * 100);
                    Application.DoEvents();
                    IFeatureBuffer pF = pfc.CreateFeatureBuffer();
                    //IFeature pF = pfc.CreateFeature();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "FID")
                        {
                            continue;
                        }
                        if (r[i] == DBNull.Value)
                        {
                            continue;
                        }
                        int filedidx = pF.Fields.FindField(dt.Columns[i].ColumnName);
                        if (filedidx > 0)
                        {
                            try
                            {
                                pF.set_Value(filedidx, r[i]);
                            }
                            catch
                            {
                            }

                        }
                    }
                    IPoint pt = new PointClass();
                    pt.X = Convert.ToDouble(r["X"]);
                    pt.Y = Convert.ToDouble(r["Y"]);
                    IZAware pZ = pt as IZAware;
                    pZ.ZAware = true;
                    IMAware pM = pt as IMAware;
                    pM.MAware = true;
                    pt.Z = Convert.ToDouble(r["Z"]);
                    pt.M = Convert.ToDouble(r["对齐里程"]);
                    pF.Shape = pt;
                    pfCursor.InsertFeature(pF);
                    idx++;
                }

                //IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                //pFeatureLayer.FeatureClass = pfc;
                //pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(txtFileName.Text));
                //m_mapControl.AddLayer(pFeatureLayer as ILayer, 0);
                //m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                MessageBox.Show("生成shape文件成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                frm.Close();
            }

        }


        private IFeatureClass CreateShapeFile(string strFullName)
        {
            IFeatureClass pfc = null;


            string filepath = strFullName;
            string filefolder = System.IO.Path.GetDirectoryName(filepath);
            string shapefilename = System.IO.Path.GetFileName(filepath);
            DataSet ds = new DataSet();
            DataTable dt = gridControl1.DataSource as DataTable;
            if (dt.DataSet != null)
            {
                ds = dt.DataSet;
            }
            else
            {
                ds.Tables.Add(dt);
            }
           

            ClsGDBDataCommon cdc = new ClsGDBDataCommon();
            //cdc.OpenFromShapefile(filefolder);
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

            //设置字段   
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;


            ESRI.ArcGIS.Geometry.ISpatialReferenceFactory spatialReferenceFactory = new ESRI.ArcGIS.Geometry.SpatialReferenceEnvironmentClass();
            //wgs 84
            ISpatialReference pSRF = spatialReferenceFactory.CreateGeographicCoordinateSystem(4326) as IGeographicCoordinateSystem;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialReferenceFactory);

            //创建类型为几何类型的字段   
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;

            pGeoDefEdit.HasM_2 = true;
            pGeoDefEdit.HasZ_2 = true;
            pGeoDefEdit.SpatialReference_2 = pSRF;

            pFieldEdit.Name_2 = "SHAPE";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            pFieldEdit.GeometryDef_2 = pGeoDef;
            //pFieldEdit.IsNullable_2 = true;
            //pFieldEdit.Required_2 = true;
            pFieldsEdit.AddField(pField);

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (ds.Tables[0].Columns[i].ColumnName == "FID")
                {
                    continue;
                }
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = ds.Tables[0].Columns[i].ColumnName;

                if (ds.Tables[0].Columns[i].DataType == Type.GetType("System.Double") ||
                   ds.Tables[0].Columns[i].DataType == Type.GetType("System.Float") ||
                    ds.Tables[0].Columns[i].DataType == Type.GetType("System.Decimal"))
                {
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                }
                else if (ds.Tables[0].Columns[i].DataType == Type.GetType("System.Int16") ||
                   ds.Tables[0].Columns[i].DataType == Type.GetType("System.Int32") ||
                    ds.Tables[0].Columns[i].DataType == Type.GetType("System.Int64"))
                {
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                }
                else
                {
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                }

                //if (ds.Tables[0].Columns[i].ColumnName.Contains("Z_高程"))
                //{
                //    pFieldEdit.Name_2 = EvConfig.CenterlineZField;
                //}
                if (ds.Tables[0].Columns[i].ColumnName.Contains("里程"))
                {
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                }

                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
            }

            ClsGDBDataCommon comm = new ClsGDBDataCommon();
            IWorkspace inmemWor = comm.OpenFromShapefile(filefolder);
            // ifeatureworkspacee
            IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
            pfc = pFeatureWorkspace.CreateFeatureClass(shapefilename, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");

            return pfc;
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (m_ResultType == "内检测对齐中线报告")
            {
                ExportInsideToCenterline();
            }
            if (m_ResultType == "两次内检测对齐报告")
            {
                ExportInsideToInside();
            }
            if (m_ResultType == "焊缝对齐报告")
            {
                ExportWeldToCenterline();
            }
        }
        private void ExportInsideToCenterline()
        {
            string TemplateFileName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\WordTemplate\内检测对齐中线报告.doc";
            SaveFileDialog saveDig = new SaveFileDialog();
            string saveFileName = "";
            saveDig.Filter = "word文档|*.doc";
            saveDig.OverwritePrompt = false;
            if (saveDig.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileName))
                {
                    MessageBox.Show("file already exists!!");
                    return;
                }
                saveFileName = saveDig.FileName;
            }
            else
            {
                return;
            }

            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            //!<根据模板文件生成新文件框架
            File.Copy(TemplateFileName, saveFileName);
            //!<生成documnet对象
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            //!<打开新文挡
            object objFileName = saveFileName;
            object missing = System.Reflection.Missing.Value;
            object isReadOnly = false;
            object isVisible = false;
            doc = app.Documents.Open(ref objFileName, ref missing, ref isReadOnly, ref missing,
            ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref isVisible,
            ref missing, ref missing, ref missing, ref missing);
            doc.Activate();
            try
            {
                DataTable dt = CreateCenterlinePointStatisticsTable();
            string BookMarkName = "tbCenterline";
            ExportDatatableToWordBookMark(dt, BookMarkName, doc);

            BookMarkName = "tbPoint";
            dt = CreateInsidPointStatisticsTable();
            ExportDatatableToWordBookMark(dt, BookMarkName, doc);


            BookMarkName = "管节长度统计表";
            dt = CreateGuanJieChangDuTongjiTable();
            ExportDatatableToWordBookMark(dt, BookMarkName, doc);

            BookMarkName = "管节长度统计描述";
            string discription = CreateGuanjieChangduText(dt);
            ExportTextToWordBookMark(discription, BookMarkName, doc);

            BookMarkName = "tbAlignment";
            dt = CreateInsidPointCenterlineAlignmentStatisticsTable();
            ExportDatatableToWordBookMark(dt, BookMarkName, doc);

            BookMarkName = "tbMeasureDifference";
            FormChart fm = new FormChart();
            CreateInsidPointCenterlineAlignmentDifferenceStatisticsImage(fm.ChartContrl);
           // fm.ShowDialog();
            ExportImageToWordBookMark(BookMarkName, doc, app);

            BookMarkName = "tbMeasureAlignmentStatics";
            CreateInsidPointCenterlineAlignmentStatisticsImage(fm.chartcontrol3);            
            ExportImageToWordBookMark(BookMarkName, doc, app);

            MessageBox.Show("导出成功");
            }
             
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //!<输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                app.Quit(ref missing, ref missing, ref missing);
            }
        }
        private void ExportInsideToInside()
        {
            string TemplateFileName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\WordTemplate\两次内检测对齐报告.doc";
            SaveFileDialog saveDig = new SaveFileDialog();
            string saveFileName = "";
            saveDig.Filter = "word文档|*.doc";
            saveDig.OverwritePrompt = false;
            if (saveDig.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileName))
                {
                    MessageBox.Show("file already exists!!");
                    return;
                }
                saveFileName = saveDig.FileName;
            }
            else
            {
                return;
            }

            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            //!<根据模板文件生成新文件框架
            File.Copy(TemplateFileName, saveFileName);
            //!<生成documnet对象
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            //!<打开新文挡
            object objFileName = saveFileName;
            object missing = System.Reflection.Missing.Value;
            object isReadOnly = false;
            object isVisible = false;
            doc = app.Documents.Open(ref objFileName, ref missing, ref isReadOnly, ref missing,
            ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref isVisible,
            ref missing, ref missing, ref missing, ref missing);
            doc.Activate();
            try
            {
                DataTable dt = CreateInsideToInsiddeBaseStatisticsTable();
                string BookMarkName = "tbBasePointTable";
                ExportDatatableToWordBookMark(dt, BookMarkName, doc);

                BookMarkName = "taAlignmentTable";
                dt = CreateInsideToInsiddeAlignmentTableStatisticsTable();
                ExportDatatableToWordBookMark(dt, BookMarkName, doc);

                BookMarkName = "tbAlignment";
                dt = CreateInsideToInsideAlignmentStatisticsTable();
                ExportDatatableToWordBookMark(dt, BookMarkName, doc);

                BookMarkName = "tbMeasureDifference";
                FormChart fm = new FormChart();
               
                CreateInsideToInsideAlignmentDifferenceStatisticsImage(fm.ChartContrl);
                //fm.Show();
                ExportImageToWordBookMark(BookMarkName, doc, app);

                BookMarkName = "tbtaAnomanyDepth";
                CreateInsideToInsideAnomanyDepthImage(fm.ChartContrl);
                ExportImageToWordBookMark(BookMarkName, doc, app);

                BookMarkName = "tbtaAnomanyDepthChange";
                CreateInsideToInsideMatchedAnomanyDepthChangeImage(fm.ChartContrl);
                ExportImageToWordBookMark(BookMarkName, doc, app);


                BookMarkName = "tbtaAnomanyStatisticsImage";
                CreateInsideToInsideAlignmentStatisticsImage(fm.chartcontrol3);
                ExportImageToWordBookMark(BookMarkName, doc, app);


                MessageBox.Show("报告生成完毕");
               
            }
            catch(SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //!<输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                app.Quit(ref missing, ref missing, ref missing);
            }
            
          
        }
        private void ExportWeldToCenterline()
        {
            string TemplateFileName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\WordTemplate\焊缝对齐报告.doc";
            SaveFileDialog saveDig = new SaveFileDialog();
            string saveFileName = "";
            saveDig.Filter = "word文档|*.doc";
            saveDig.OverwritePrompt = false;
            if (saveDig.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileName))
                {
                    MessageBox.Show("file already exists!!");
                    return;
                }
                saveFileName = saveDig.FileName;
            }
            else
            {
                return;
            }


            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            //!<根据模板文件生成新文件框架
            File.Copy(TemplateFileName, saveFileName);
            //!<生成documnet对象
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
           
                //!<打开新文挡
                object objFileName = saveFileName;
                object missing = System.Reflection.Missing.Value;
                object isReadOnly = false;
                object isVisible = false;
                doc = app.Documents.Open(ref objFileName, ref missing, ref isReadOnly, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref isVisible,
                ref missing, ref missing, ref missing, ref missing);
                doc.Activate();
            try
            {
                DataTable dt = CreateWeldToCenterlineTable();
                string BookMarkName = "tbWeld";
                ExportDatatableToWordBookMark(dt, BookMarkName, doc);

                BookMarkName = "tbWeldOffset";
                FormChart fm = new FormChart();
                CreateWeldToCenterlineOffsetTableImage(fm.ChartContrl);
                ExportImageToWordBookMark(BookMarkName, doc, app);
                MessageBox.Show("报告生成完毕");
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //!<输出完毕后关闭doc对象
                object IsSave = true;
                doc.Close(ref IsSave, ref missing, ref missing);
                app.Quit(ref missing, ref missing, ref missing);
            }


        }
        private void ExportImageToWordBookMark(string bookmarkName, Microsoft.Office.Interop.Word.Document doc, Microsoft.Office.Interop.Word.Application app)
        {
            string imgpath = this.TempImagePath;
            object BookMarkName = bookmarkName;
            object whatimg = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark;
            object missing = System.Reflection.Missing.Value;
            doc.ActiveWindow.Selection.GoTo(ref whatimg, ref missing, ref missing, ref BookMarkName);//!<定位到书签位置
            object Anchor = app.Selection.Range;
            object LinkToFile = false;
            object SaveWithDocument = true;
            Microsoft.Office.Interop.Word.InlineShape shape = doc.InlineShapes.AddPicture(imgpath, ref LinkToFile, ref SaveWithDocument, ref Anchor);

        }
        private void ExportDatatableToWordBookMark(DataTable dt, string bookmarkName, Microsoft.Office.Interop.Word.Document doc)
        {

            object BookMarkName = bookmarkName;
            object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark;
            object missing = System.Reflection.Missing.Value;
            doc.ActiveWindow.Selection.GoTo(ref what, ref missing, ref missing, ref BookMarkName);//!<定位到书签位置

            //doc.ActiveWindow.Selection.TypeText(lineCount[bookIndex - 1].ToString());//!<在对应书签位置插入内容
            //length = lineCount[bookIndex - 1].ToString().Length;
            //app.ActiveDocument.Bookmarks.get_Item(ref BookMarkName).End = app.ActiveDocument.Bookmarks.get_Item(ref BookMarkName).End + length;
            int tableRow = dt.Rows.Count;
            int tableColumn = dt.Columns.Count;
            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(doc.ActiveWindow.Selection.Range, tableRow + 1, tableColumn, ref missing, ref missing);
            table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//表格文本居中
            table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalBottom;//文本垂直贴到底部
            table.Borders.Enable = 1;//这个值可以设置得很大，例如5、13等等
            //doc.Tables[1].Cell(1, 1).Range.Text = "列\n行";
            for (int j = 1; j <= tableColumn; j++)
            {
                table.Cell(1, j).Range.Text = dt.Columns[j - 1].ToString();
            }
            for (int i = 1; i <= tableRow; i++)
            {
                for (int j = 1; j <= tableColumn; j++)
                {
                    table.Cell(i + 1, j).Range.Text = dt.Rows[i - 1][j - 1].ToString();
                }
            }
        }
        private void ExportTextToWordBookMark(string Text, string bookmarkName, Microsoft.Office.Interop.Word.Document doc)
        {
            object BookMarkName = bookmarkName;
            object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark;
            object missing = System.Reflection.Missing.Value;
            doc.ActiveWindow.Selection.GoTo(ref what, ref missing, ref missing, ref BookMarkName);//!<定位到书签位置

            doc.ActiveWindow.Selection.TypeText(Text);//!<在对应书签位置插入内容
           
        }
        public void setResultType(string t)
        {
            m_ResultType = t;
        }

        //中线点统计表
        private DataTable CreateCenterlinePointStatisticsTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("总里程");
            dt.Columns.Add("中线点数");
            dt.Columns.Add("拐点数");
            dt.Columns.Add("管线点数");
            DataRow dr = dt.NewRow();
            dr["总里程"] = CenterlinePointTable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.CenterlineMeasureField]));

            dr["中线点数"] = CenterlinePointTable.Rows.Count;

            dr["拐点数"] = (from DataRow r in CenterlinePointTable.Rows
                         where r[EvConfig.CenterlinePointTypeField] != DBNull.Value && r[EvConfig.CenterlinePointTypeField].ToString().Contains("拐点")
                         select r).Count();
            dr["管线点数"] = (from DataRow r in CenterlinePointTable.Rows
                          where r[EvConfig.CenterlinePointTypeField] != DBNull.Value && r[EvConfig.CenterlinePointTypeField].ToString().Contains("管线点")
                          select r).Count(); ;
            dt.Rows.Add(dr);
            return dt;
        }

        //内检测点统计表
        private DataTable CreateInsidPointStatisticsTable()
        {
            DataTable stable = sourcetable;

            DataTable dt = new DataTable();
            dt.Columns.Add("统计类别");
            dt.Columns.Add("统计数量");
             
            DataRow dr = dt.NewRow();
            dr[0] = "检测长度";
            dr[1] = (stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]))
                - stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]))).ToString() + "m";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "阀门数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("阀门")
                     select r).Count().ToString() + "个"; 
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "三通数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("三通")
                     select r).Count().ToString() + "个"; 
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "开孔（支管）数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value &&
                     (r[EvConfig.IMUPointTypeField].ToString().Contains("开孔") || r[EvConfig.IMUPointTypeField].ToString().Contains("支管"))
                     select r).Count().ToString() + "个"; ;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "绝缘法兰（接头）数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value &&
                     (r[EvConfig.IMUPointTypeField].ToString().Contains("绝缘法兰") || r[EvConfig.IMUPointTypeField].ToString().Contains("接头"))
                     select r).Count().ToString() + "个"; ;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "弯头数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("弯头")
                     select r).Count().ToString() + "个"; 
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "钢管数量";
            dr[1] = ((from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("环向焊缝")
                     select r).Count() -1).ToString() + "根"; 
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "套管数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("套管")
                     select r).Count().ToString() + "个"; ;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "环向焊缝数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("环向焊缝")
                     select r).Count().ToString() + "条"; ;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "金属损失数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("金属损失")
                     select r).Count().ToString() + "个"; ;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "制造缺陷数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("制造缺陷")
                     select r).Count().ToString() + "个"; ; ;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "凹陷数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("凹陷")
                     select r).Count().ToString() + "个"; ; ; ;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "焊缝缺陷数量";
            dr[1] = (from DataRow r in stable.Rows
                     where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("焊缝缺陷")
                     select r).Count().ToString() + "个"; ; ; ;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "IMU 成果";
            dr[1] = "平面：WGS84 "+ "\n 高程：1985国家高程基准";
            dt.Rows.Add(dr); 
            return dt;
        }
        //管节长度统计
        private DataTable CreateGuanJieChangDuTongjiTable()
        {
            DataTable stable = sourcetable;

            DataTable dt = new DataTable();
            dt.Columns.Add("范围");
            dt.Columns.Add("0≤L＜2");
            dt.Columns.Add("2≤L＜5");
            dt.Columns.Add("5≤L＜10");
            dt.Columns.Add("10≤L＜11.5");
            dt.Columns.Add("11.5≤L＜12.3");
            dt.Columns.Add("12.3≤L");

            List<DataRow> hanfenglist = (from DataRow r in stable.AsEnumerable()
                                         where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("环向焊缝")
                                         select r).ToList();
            List<double> lengthlist = new List<double>();
            for (int i = 0; i < hanfenglist.Count - 1; i++)
            {
                lengthlist.Add(Convert.ToDouble(hanfenglist[i + 1][EvConfig.IMUAlignmentMeasureField]) - Convert.ToDouble(hanfenglist[i][EvConfig.IMUAlignmentMeasureField]));
            }

            DataRow row = dt.NewRow();
            row[0] = "数量";
            row[1] = (from double l in lengthlist
                    where l >= 0 && l < 2
                    select l).Count();
            row[2] = (from double l in lengthlist
                    where l >= 2 && l < 5
                    select l).Count();
            row[3] = (from double l in lengthlist
                    where l >= 5 && l < 10
                    select l).Count();
            row[4] = (from double l in lengthlist
                    where l >= 10 && l < 11.5
                    select l).Count();
            row[5] = (from double l in lengthlist
                    where l >= 11.5 && l < 12.3
                    select l).Count();
            row[6] = (from double l in lengthlist
                    where l >= 12.3  
                    select l).Count();
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = "比例";
            row[1] = (from double l in lengthlist
                      where l >= 0 && l < 2
                      select l).Count() * 1.0 / lengthlist.Count;
            row[2] = (from double l in lengthlist
                      where l >= 2 && l < 5
                      select l).Count() * 1.0 / lengthlist.Count;
            row[3] = (from double l in lengthlist
                      where l >= 5 && l < 10
                      select l).Count() * 1.0 / lengthlist.Count;
            row[4] = (from double l in lengthlist
                      where l >= 10 && l < 11.5
                      select l).Count() * 1.0 / lengthlist.Count;
            row[5] = (from double l in lengthlist
                      where l >= 11.5 && l < 12.3
                      select l).Count() * 1.0 / lengthlist.Count;
            row[6] = (from double l in lengthlist
                      where l >= 12.3
                      select l).Count() * 1.0 / lengthlist.Count;
            dt.Rows.Add(row);

             return dt;
        }
        //管径长度描述
        private string CreateGuanjieChangduText(DataTable dt)
        {
           
            double tempv = 0;
            DataRow rowNum = dt.Rows[0];
            DataRow row = dt.Rows[1];
            double maxV = (from string cl in dt.Columns
                          where double.TryParse(row[cl].ToString(), out tempv)
                          select Convert.ToDouble(row[cl])).Max();
            string MaxVcol = (from string cl in dt.Columns
                          where row[cl].ToString() ==  maxV.ToString()
                          select cl).First();
            double duanguanjie = Convert.ToDouble(row["0≤L＜2"]);
            double Jiaoduanduanguanjie = Convert.ToDouble(row["2≤L＜5"]);
            double gongji = duanguanjie + Jiaoduanduanguanjie;
            double chaochangguanjie = Convert.ToDouble(row["12.3≤L"]);
            int rowNumV = Convert.ToInt16(rowNum["12.3≤L"]);

            DataTable stable = sourcetable;
            List<DataRow> hanfenglist = (from DataRow r in stable.AsEnumerable()
                                         where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("环向焊缝")
                                         select r).ToList();

            List<double> lengthlist = new List<double>();
            for (int i = 0; i < hanfenglist.Count - 1; i++)
            {
                lengthlist.Add(Convert.ToDouble(hanfenglist[i + 1][EvConfig.IMUAlignmentMeasureField]) - Convert.ToDouble(hanfenglist[i][EvConfig.IMUAlignmentMeasureField]));
            }

            double maxLength = lengthlist.Max();
            string text = string.Format( @"管道的内检测数据中{0}的管节数最多，
            占比达到{1}%；短管节（0≤L＜2）和较短管节（2≤L＜5）的占比分别为{2}%和{3}%，
            两者占比相对较高，共计{4}%；超长管节（12.3≤L）的比例为{5}%。针对12.3≤L的超长管节，管节数量达到了{6}根，
            其中最大长度为{7}米，如果按照12m的标准管道长度来计算，该管节可能会漏检{8}条环焊缝。", 
            MaxVcol, maxV.ToString(), duanguanjie.ToString(), Jiaoduanduanguanjie.ToString(), gongji.ToString(),chaochangguanjie.ToString(),
            rowNumV.ToString(),maxLength.ToString(), rowNumV.ToString() );               
            return text;
        }
        // 超长管径统计表
        private DataTable CreateExceedPipesegmentTable()
        {
            DataTable stable = sourcetable;

            DataTable dt = new DataTable();
            dt.Columns.Add("范围");
            dt.Columns.Add("0≤L＜2");
            dt.Columns.Add("2≤L＜5");
            dt.Columns.Add("5≤L＜10");
            dt.Columns.Add("10≤L＜11.5");
            dt.Columns.Add("11.5≤L＜12.3");
            dt.Columns.Add("12.3≤L");
            return dt;
        }

        //内检测对齐到中线统计表
        private DataTable CreateInsidPointCenterlineAlignmentStatisticsTable()
        {
            DataTable stable = sourcetable;

            DataTable dt = new DataTable();
            dt.Columns.Add("起点记录距离");
            dt.Columns.Add("终点记录距离");
            dt.Columns.Add("起点对齐里程");
            dt.Columns.Add("终点对齐里程");
            dt.Columns.Add("内检测点数");
            dt.Columns.Add("匹配特征点数");
            dt.Columns.Add("特征点平均里程差");

            DataRow dr = dt.NewRow();
            dr["起点记录距离"] = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            dr["终点记录距离"] = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            dr["内检测点数"] = stable.Rows.Count;
            dr["起点对齐里程"] = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
            dr["终点对齐里程"] = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));

            dr["匹配特征点数"] = (from DataRow r in stable.Rows
                            where r["里程差"] != DBNull.Value
                            select r).Count();
            try
            {
                dr["特征点平均里程差"] = Math.Round((from DataRow r in stable.Rows
                                             where r["里程差"] != DBNull.Value
                                             select Math.Abs(Convert.ToDouble(r["里程差"]))).Average(), 2);
            }
            catch
            {

            }
            
            dt.Rows.Add(dr);
            return dt;
        }
        // 内检测对齐到中线里程差统计图
        private void CreateInsidPointCenterlineAlignmentDifferenceStatisticsImage(ChartControl control)
        {

            try
            {
                DataTable stable = sourcetable;
                ChartControl chartControl1 = control;
                double BegMeasure = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                double endMeasure = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                chartControl1.Series.Clear();
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("特征点里程差", ViewType.Bar);
                series.ShowInLegend = false;
                foreach (DataRow r in stable.Rows)
                {
                    double m; double z;
                    if (r["里程差"] == DBNull.Value)
                        continue;
                    m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                    z = Math.Round(Math.Abs(Convert.ToDouble(r["里程差"])), 2);
                    series.Points.Add(new SeriesPoint(m, z));
                }

                // System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chartControl1.Series.Add(series);
                chartControl1.Titles.Clear();
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);
                if (series.Points.Count > 0)
                {
                    double maxY = (from SeriesPoint pt in series.Points
                                  select pt.Values[0]).Max();
                    ((XYDiagram)chartControl1.Diagram).AxisY.VisualRange.SetMinMaxValues(0, maxY * 1.5);
                }

                XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
                // Customize the appearance of the X-axis title. 
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "对齐里程";
                diagram.AxisX.Title.TextColor = Color.Black;
                diagram.AxisX.Title.EnableAntialiasing =  DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                // Customize the appearance of the Y-axis title. 
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
                diagram.AxisY.Title.Text = "特征点里程差";
                diagram.AxisY.Title.TextColor = Color.Black;
                diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True; 
                diagram.AxisY.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                //((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "对齐里程";
                //((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "特征点里程差";
                Image image = null;
                ImageFormat format = ImageFormat.Jpeg;
                // Create an image of the chart. 
                using (MemoryStream s = new MemoryStream())
                {
                    chartControl1.ExportToImage(s, format);
                    image = Image.FromStream(s);
                }
                control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            // return image;

        }

        //内检测对其到中线对齐情况统计图
        private void CreateInsidPointCenterlineAlignmentStatisticsImage(ChartControl control)
        {
            DataTable stable = InsidePointTable;
            DataTable BaseTable = CenterlinePointTable;
            ChartControl chartControl1 = control;
            double BegMeasure = BaseTable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.CenterlineMeasureField]));
            double endMeasure = BaseTable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.CenterlineMeasureField]));
            //chartControl1.Series.Clear();
          //  DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("中线点", ViewType.Bar);
            DevExpress.XtraCharts.Series seriesNotAligned = chartControl1.Series[1];
            seriesNotAligned.Name = "未对齐内检测点";
            seriesNotAligned.ShowInLegend = true;
            seriesNotAligned.Points.Clear();

            DevExpress.XtraCharts.Series seriesAligned = chartControl1.Series[2];
            seriesAligned.Name = "对齐内检测点";
            seriesAligned.Points.Clear();
            foreach (DataRow r in sourcetable.Rows)
            {
                double m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                double z = 3;
                if (r["里程差"] == DBNull.Value)
                {
                    SeriesPoint spt = new SeriesPoint(m, z);
                    spt.Tag = sourcetable.Rows.IndexOf(r);
                    seriesNotAligned.Points.Add(spt);
                }
                else
                {
                    SeriesPoint spt = new SeriesPoint(m, z);
                    spt.Tag = sourcetable.Rows.IndexOf(r);
                    seriesAligned.Points.Add(spt);
                }
            }


            DevExpress.XtraCharts.Series seriesCenterline = chartControl1.Series[0];
            seriesCenterline.Name = "中线点";
            seriesCenterline.ShowInLegend = true;
            seriesCenterline.Points.Clear();
            foreach (DataRow r in CenterlinePointTable.Rows)
            {
                double m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.CenterlineMeasureField])), 2);
                double z = 3;
                SeriesPoint spt = new SeriesPoint(m, z);
                spt.Tag = CenterlinePointTable.Rows.IndexOf(r);
                seriesCenterline.Points.Add(spt);
            }
            XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
            diagram.SecondaryAxesX[0].WholeRange.MinValue = diagram.AxisX.WholeRange.MinValue;
            diagram.SecondaryAxesX[0].WholeRange.MaxValue = diagram.AxisX.WholeRange.MaxValue;
            diagram.SecondaryAxesY[0].Visibility = DevExpress.Utils.DefaultBoolean.False;
 
            Image image = null;
            ImageFormat format = ImageFormat.Jpeg;
            // Create an image of the chart. 
            using (MemoryStream s = new MemoryStream())
            {
                chartControl1.ExportToImage(s, format);
                image = Image.FromStream(s);
            }
            control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
        }
        

        // 两次内检测base内检测表
        private DataTable CreateInsideToInsiddeBaseStatisticsTable()
        {
            DataTable stable = BasePointTable;
             
            DataTable dt = new DataTable();
            dt.Columns.Add(EvConfig.IMUInspectionYearField);
            dt.Columns.Add("起点记录距离");
            dt.Columns.Add("终点记录距离");
            dt.Columns.Add("内检测点数");
            dt.Columns.Add("环向焊缝数");
            dt.Columns.Add("三通数");
            dt.Columns.Add("弯头数");
            dt.Columns.Add("异常数");
            
            DataRow dr = dt.NewRow();
            dr[EvConfig.IMUInspectionYearField] = stable.Rows[0][EvConfig.IMUInspectionYearField];
            dr["起点记录距离"] = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            dr["终点记录距离"] = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            dr["内检测点数"] = stable.Rows.Count;
            //dr["起点对齐里程"] = InsidePointTable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
            //dr["终点对齐里程"] = InsidePointTable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));

            dr["环向焊缝数"] = (from DataRow r in stable.Rows
                           where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("环向焊缝")
                           select r).Count();
            dr["弯头数"] = (from DataRow r in stable.Rows
                         where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("弯头")
                         select r).Count();
            dr["三通数"] = (from DataRow r in stable.Rows
                         where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("三通")
                         select r).Count();
            dr["异常数"] = (from DataRow r in stable.Rows
                         where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("异常")
                         select r).Count();
            dt.Rows.Add(dr);
            return dt;
        }
        // 两次内检测对齐内检测表
        private DataTable CreateInsideToInsiddeAlignmentTableStatisticsTable()
        {
            DataTable stable = AlignmentPointTable;

            DataTable dt = new DataTable();
            dt.Columns.Add(EvConfig.IMUInspectionYearField);
            dt.Columns.Add("起点记录距离");
            dt.Columns.Add("终点记录距离");
            dt.Columns.Add("内检测点数");
            dt.Columns.Add("环向焊缝数");
            dt.Columns.Add("三通数");
            dt.Columns.Add("弯头数");
            dt.Columns.Add("异常数");

            DataRow dr = dt.NewRow();
            dr[EvConfig.IMUInspectionYearField] = stable.Rows[0][EvConfig.IMUInspectionYearField];
            dr["起点记录距离"] = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            dr["终点记录距离"] = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            dr["内检测点数"] = stable.Rows.Count;
            //dr["起点对齐里程"] = InsidePointTable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
            //dr["终点对齐里程"] = InsidePointTable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));

            dr["环向焊缝数"] = (from DataRow r in stable.Rows
                           where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("环向焊缝")
                           select r).Count();
            dr["弯头数"] = (from DataRow r in stable.Rows
                         where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("弯头")
                         select r).Count();
            dr["三通数"] = (from DataRow r in stable.Rows
                         where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("三通")
                         select r).Count();
            dr["异常数"] = (from DataRow r in stable.Rows
                         where r[EvConfig.IMUPointTypeField] != DBNull.Value && r[EvConfig.IMUPointTypeField].ToString().Contains("异常")
                         select r).Count();
            dt.Rows.Add(dr);
            return dt;
        }
        // 两次内检测对齐统计表
        private DataTable CreateInsideToInsideAlignmentStatisticsTable()
        {
            DataTable stable = sourcetable;

            DataTable dt = new DataTable();
            dt.Columns.Add("起点记录距离");
            dt.Columns.Add("终点记录距离");
            dt.Columns.Add("起点对齐里程");
            dt.Columns.Add("终点对齐里程");
            dt.Columns.Add("粗对齐匹配特征点数");
            dt.Columns.Add("特征点平均里程差");
            dt.Columns.Add("精对齐匹配特征点数");
            dt.Columns.Add("匹配异常数");

            DataRow dr = dt.NewRow();
            dr["起点记录距离"] = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            dr["终点记录距离"] = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            dr["起点对齐里程"] = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
            dr["终点对齐里程"] = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));

            dr["粗对齐匹配特征点数"] = (from DataRow r in stable.Rows
                            where r["里程差"] != DBNull.Value
                            select r).Count();
            dr["精对齐匹配特征点数"] = (from DataRow r in stable.Rows
                               where r["对齐基准点里程"] != DBNull.Value
                               select r).Count();
            dr["特征点平均里程差"] = Math.Round((from DataRow r in stable.Rows
                                         where r["里程差"] != DBNull.Value
                                         select Math.Abs(Convert.ToDouble(r["里程差"]))).Average(), 2);
            //dr["匹配异常数"] =  (from DataRow r in stable.Rows
            //                             where r["壁厚变化"] != DBNull.Value
            //                             select r).ToList().Count;            
            dr["匹配异常数"] =  (from DataRow r in stable.Rows
                                        where r["异常匹配里程差"] != DBNull.Value
                                         select r).ToList().Count;
           
            dt.Rows.Add(dr);
            return dt;
        }
        // 两次内检测对比表
        private DataTable CreateInsideToInsideDifferenceStaticticsTable()
        {           
            DataTable dt = new DataTable();
            dt.Columns.Add("大类");
            dt.Columns.Add("小类");
            string byear = "第一次内检测年份";
            string ayear = "第二次内检测年份";
            try
            {
                  byear = BasePointTable.AsEnumerable().Where(x => x[EvConfig.IMUInspectionYearField] != DBNull.Value).ToList().First()[EvConfig.IMUInspectionYearField].ToString();     
            }
            catch
            {
            }
            try
            {
                  ayear = AlignmentPointTable.AsEnumerable().Where(x => x[EvConfig.IMUInspectionYearField] != DBNull.Value).ToList().First()[EvConfig.IMUInspectionYearField].ToString();        
            }
            catch
            {
            }
            dt.Columns.Add(byear);
            dt.Columns.Add(ayear);
            GenerateDifferenceRow("管道定位", "地面参考点", ref dt);
            GenerateDifferenceRow("管道定位", "环向焊缝", ref dt);

            GenerateDifferenceRow("缺陷", "凹陷", ref dt);
            GenerateDifferenceRow("缺陷", "环向焊缝缺陷", ref dt);
            GenerateDifferenceRow("缺陷", "直焊缝缺陷", ref dt);
            GenerateDifferenceRow("缺陷", "金属腐蚀", ref dt);
            GenerateDifferenceRow("缺陷", "外接金属物", ref dt);
            GenerateDifferenceRow("缺陷", "制造缺陷", ref dt);

            GenerateDifferenceRow("设备", "阀门", ref dt);
            GenerateDifferenceRow("设备", "绝缘接头", ref dt);
            GenerateDifferenceRow("设备", "开孔", ref dt);
            GenerateDifferenceRow("设备", "三通", ref dt);
            GenerateDifferenceRow("设备", "套管", ref dt);
            GenerateDifferenceRow("设备", "弯头", ref dt);
            return dt;            
        }
        private void GenerateDifferenceRow(string c1, string c2, ref DataTable dt)
        {
            DataRow r = dt.NewRow();
            r[0] = c1;
            r[1] = c2;
            r[2] = (BasePointTable.AsEnumerable().Where(x => x[EvConfig.IMUPointTypeField] != null && x[EvConfig.IMUPointTypeField].ToString().Contains(c2))).Count();
            r[3] = (AlignmentPointTable.AsEnumerable().Where(x => x[EvConfig.IMUPointTypeField] != null && x[EvConfig.IMUPointTypeField].ToString().Contains(c2))).Count();
            dt.Rows.Add(r);
        }
        //两次内检测对齐统计图
        private void CreateInsideToInsideAlignmentStatisticsImage(ChartControl control)
        {
            DataTable stable = AlignmentPointTable;
            DataTable BaseTable = BasePointTable;
            ChartControl chartControl1 = control;
            double BegMeasure = BaseTable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            double endMeasure = BaseTable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]));
            //chartControl1.Series.Clear();
            //  DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("中线点", ViewType.Bar);
            DevExpress.XtraCharts.Series seriesNotAligned = chartControl1.Series[1];
            seriesNotAligned.Name = "未对齐内检测点";
            seriesNotAligned.ShowInLegend = true;

            DevExpress.XtraCharts.Series seriesAligned = chartControl1.Series[2];
            seriesAligned.Name = "对齐内检测点";
            foreach (DataRow r in sourcetable.Rows)
            {
                try
                {
                 
                    double m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                   
                    double z = 4;
                    if (r["对齐基准点里程"] == DBNull.Value )
                    {
                        SeriesPoint spt = new SeriesPoint(m, z);
                        spt.Tag = sourcetable.Rows.IndexOf(r);
                        seriesNotAligned.Points.Add(spt);
                    }
                    else if (r["对齐基准点里程"] != DBNull.Value )
                    {
                        SeriesPoint spt = new SeriesPoint(m, z);
                        spt.Tag = sourcetable.Rows.IndexOf(r);
                        seriesAligned.Points.Add(spt);
                    }
                }
                catch
                {
                }
            }


            DevExpress.XtraCharts.Series seriesCenterline = chartControl1.Series[0];
            seriesCenterline.Name = "基础内检测点";
            seriesCenterline.ShowInLegend = true;
            foreach (DataRow r in BasePointTable.Rows)
            {
                double m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                double z = 4;
                seriesCenterline.Points.Add(new SeriesPoint(m, z));
            }
            XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
            double minvalue = Math.Min(Convert.ToDouble(diagram.SecondaryAxesX[0].WholeRange.MinValue),
                Convert.ToDouble(diagram.AxisX.WholeRange.MinValue));

            double maxvalue = Math.Max(Convert.ToDouble(diagram.SecondaryAxesX[0].WholeRange.MaxValue),
                Convert.ToDouble(diagram.AxisX.WholeRange.MaxValue));

            diagram.SecondaryAxesX[0].WholeRange.MinValue = minvalue;
            diagram.SecondaryAxesX[0].WholeRange.MaxValue = maxvalue;

            diagram.AxisX.WholeRange.MinValue = minvalue;
            diagram.AxisX.WholeRange.MaxValue = maxvalue;
            diagram.SecondaryAxesY[0].Visibility = DevExpress.Utils.DefaultBoolean.False;

            Image image = null;
            ImageFormat format = ImageFormat.Jpeg;
            // Create an image of the chart. 
            using (MemoryStream s = new MemoryStream())
            {
                chartControl1.ExportToImage(s, format);
                image = Image.FromStream(s);
            }
            control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
        }


        private void CreateInsideToInsideAlignmentDifferenceStatisticsImage(ChartControl control)
        {

            try
            {
                DataTable stable = sourcetable;
                ChartControl chartControl1 = control;
                double BegMeasure = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                double endMeasure = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                chartControl1.Series.Clear();
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("特征点里程差", ViewType.Bar);
                series.ShowInLegend = false;
                foreach (DataRow r in stable.Rows)
                {
                    double m; double z;
                    if (r["里程差"] == DBNull.Value)
                        continue;
                    m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                    z = Math.Round(Math.Abs(Convert.ToDouble(r["里程差"])), 2);
                    series.Points.Add(new SeriesPoint(m, z));
                }

                // System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chartControl1.Series.Add(series);
                chartControl1.Titles.Clear();
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);
                if (series.Points.Count > 0)
                {
                    double maxY = (from SeriesPoint pt in series.Points
                                   select pt.Values[0]).Max();
                    ((XYDiagram)chartControl1.Diagram).AxisY.VisualRange.SetMinMaxValues(0, maxY * 1.5);
                }

                XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
                // Customize the appearance of the X-axis title. 
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "特征点对齐里程";
                diagram.AxisX.Title.TextColor = Color.Black;
                diagram.AxisX.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                // Customize the appearance of the Y-axis title. 
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
                diagram.AxisY.Title.Text = "特征点里程差";
                diagram.AxisY.Title.TextColor = Color.Black;
                diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                //((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "对齐里程";
                //((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "特征点里程差";
                Image image = null;
                ImageFormat format = ImageFormat.Jpeg;
                // Create an image of the chart. 
                using (MemoryStream s = new MemoryStream())
                {
                    chartControl1.ExportToImage(s, format);
                    image = Image.FromStream(s);
                }
                control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            // return image;

        }

        private void CreateInsideToInsideAnomanyDepthImage(ChartControl control)
        {
            try
            {
                DataTable stable = sourcetable;
                ChartControl chartControl1 = control;
                double BegMeasure = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                double endMeasure = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                chartControl1.Series.Clear();
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("异常", ViewType.Bar);
                series.ShowInLegend = false;
                foreach (DataRow r in stable.Rows)
                {
                    double m; double z;
                    if (r[EvConfig.IMUDepthField] == DBNull.Value || r[EvConfig.IMUPointTypeField] == DBNull.Value || !r[EvConfig.IMUPointTypeField].ToString().Contains("异常"))
                        continue;
                    m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                    z = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUDepthField])), 2);
                    series.Points.Add(new SeriesPoint(m, z));
                }

                // System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chartControl1.Series.Add(series);
                chartControl1.Titles.Clear();
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);
                if (series.Points.Count > 0)
                {
                    double maxY = (from SeriesPoint pt in series.Points
                                   select pt.Values[0]).Max();
                    ((XYDiagram)chartControl1.Diagram).AxisY.VisualRange.SetMinMaxValues(0, maxY * 1.5);
                }

                XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
                // Customize the appearance of the X-axis title. 
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "异常对齐里程";
                diagram.AxisX.Title.TextColor = Color.Black;
                diagram.AxisX.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                // Customize the appearance of the Y-axis title. 
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
                diagram.AxisY.Title.Text = EvConfig.IMUDepthField;
                diagram.AxisY.Title.TextColor = Color.Black;
                diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                //((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "对齐里程";
                //((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "特征点里程差";
                Image image = null;
                ImageFormat format = ImageFormat.Jpeg;
                // Create an image of the chart. 
                using (MemoryStream s = new MemoryStream())
                {
                    chartControl1.ExportToImage(s, format);
                    image = Image.FromStream(s);
                }
                control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateInsideToInsideMatchedAnomanyDepthChangeImage(ChartControl control)
        {
            try
            {
                DataTable stable = sourcetable;
                ChartControl chartControl1 = control;
                double BegMeasure = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                double endMeasure = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                chartControl1.Series.Clear();
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("异常", ViewType.Bar);
                series.ShowInLegend = false;
                foreach (DataRow r in stable.Rows)
                {
                    double m; double z;
                    if (r["壁厚变化"] == DBNull.Value)
                        continue;
                    m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                    z = Math.Round(Math.Abs(Convert.ToDouble(r["壁厚变化"])), 2);
                    series.Points.Add(new SeriesPoint(m, z));
                }

                // System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chartControl1.Series.Add(series);
                chartControl1.Titles.Clear();
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);

                XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
                // Customize the appearance of the X-axis title. 
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "异常对齐里程";
                diagram.AxisX.Title.TextColor = Color.Black;
                diagram.AxisX.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                // Customize the appearance of the Y-axis title. 
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
                diagram.AxisY.Title.Text = "壁厚变化";
                diagram.AxisY.Title.TextColor = Color.Black;
                diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                //((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "对齐里程";
                //((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "特征点里程差";
                Image image = null;
                ImageFormat format = ImageFormat.Jpeg;
                // Create an image of the chart. 
                using (MemoryStream s = new MemoryStream())
                {
                    chartControl1.ExportToImage(s, format);
                    image = Image.FromStream(s);
                }
                control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 焊缝对齐统计
        private DataTable CreateWeldToCenterlineTable()
        {
            DataTable stable = sourcetable;
            DataTable dt = new DataTable();
            dt.Columns.Add("焊缝数");
            dt.Columns.Add("平均偏移距离");
            DataRow dr = dt.NewRow();
            dr["焊缝数"] = stable.Rows.Count;

            dr["平均偏移距离"] = Math.Round((from DataRow r in stable.Rows
                                       where r["距离偏移"] != DBNull.Value
                                       select Math.Abs(Convert.ToDouble(r["距离偏移"]))).Average(), 2);

            dt.Rows.Add(dr);
            return dt;
        }
        private void CreateWeldToCenterlineOffsetTableImage(ChartControl control)
        {
            try
            {
                DataTable stable = sourcetable;
                ChartControl chartControl1 = control;
                
                double BegMeasure = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.WeldAlignmentMeasureField]));
                double endMeasure = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.WeldAlignmentMeasureField]));
                chartControl1.Series.Clear();
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("焊缝", ViewType.Bar);
                series.ShowInLegend = false;
                foreach (DataRow r in stable.Rows)
                {
                    double m; double z;
                    if (r["距离偏移"] == DBNull.Value)
                        continue;
                    m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.WeldAlignmentMeasureField])), 2);
                    z = Math.Round(Math.Abs(Convert.ToDouble(r["距离偏移"])), 2);
                    SeriesPoint spt = new SeriesPoint(m, z);
                    spt.Tag = stable.Rows.IndexOf(r);
                    series.Points.Add(spt);
                }

                // System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chartControl1.Series.Add(series);
                chartControl1.Titles.Clear();
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);

                XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
                // Customize the appearance of the X-axis title. 
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "焊缝里程";
                diagram.AxisX.Title.TextColor = Color.Black;
                diagram.AxisX.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                // Customize the appearance of the Y-axis title.   
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
                diagram.AxisY.Title.Text = "距离偏移";
                diagram.AxisY.Title.TextColor = Color.Black;
                diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True; 
                diagram.AxisY.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                //((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "对齐里程";
                //((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "特征点里程差";
                Image image = null;
                ImageFormat format = ImageFormat.Jpeg;
                // Create an image of the chart. 
                using (MemoryStream s = new MemoryStream())
                {
                    chartControl1.ExportToImage(s, format);
                    image = Image.FromStream(s);
                }
                control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateWaijianceToCenterlineOffsetTableImage(ChartControl control)
        {
            try
            {
                DataTable stable = sourcetable;
                ChartControl chartControl1 = control;

                double BegMeasure = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.WeldAlignmentMeasureField]));
                double endMeasure = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.WeldAlignmentMeasureField]));
                chartControl1.Series.Clear();
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("外检测对齐", ViewType.Bar);
                series.ShowInLegend = false;
                foreach (DataRow r in stable.Rows)
                {
                    double m; double z;
                    if (r["距离偏移"] == DBNull.Value)
                        continue;
                    m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.WeldAlignmentMeasureField])), 2);
                    z = Math.Round(Math.Abs(Convert.ToDouble(r["距离偏移"])), 2);
                    SeriesPoint spt = new SeriesPoint(m, z);
                    spt.Tag = stable.Rows.IndexOf(r);
                    series.Points.Add(spt);                     
                }

                // System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chartControl1.Series.Add(series);
                chartControl1.Titles.Clear();
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);

                XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
                // Customize the appearance of the X-axis title. 
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "外检测数据里程";
                diagram.AxisX.Title.TextColor = Color.Black;
                diagram.AxisX.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                // Customize the appearance of the Y-axis title.   
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
                diagram.AxisY.Title.Text = "距离偏移";
                diagram.AxisY.Title.TextColor = Color.Black;
                diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                //((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "对齐里程";
                //((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "特征点里程差";
                Image image = null;
                ImageFormat format = ImageFormat.Jpeg;
                // Create an image of the chart. 
                using (MemoryStream s = new MemoryStream())
                {
                    chartControl1.ExportToImage(s, format);
                    image = Image.FromStream(s);
                }
                control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateWaijianceToWaijianceMatchedAttrChangeImage(ChartControl control, string attrName)
        {
            try
            {
                string colname = attrName + "变化";
                DataTable stable = sourcetable;
                ChartControl chartControl1 = control;
                double BegMeasure = stable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                double endMeasure = stable.AsEnumerable().Max(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));
                chartControl1.Series.Clear();
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series(attrName, ViewType.Bar);
                series.ShowInLegend = false;
                foreach (DataRow r in stable.Rows)
                {
                    double m; double z;
                    if (r[colname] == DBNull.Value)
                        continue;
                    m = Math.Round(Math.Abs(Convert.ToDouble(r[EvConfig.IMUAlignmentMeasureField])), 2);
                    z = Math.Round(Math.Abs(Convert.ToDouble(r[colname])), 2);
                    series.Points.Add(new SeriesPoint(m, z));
                }

                // System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chartControl1.Series.Add(series);
                chartControl1.Titles.Clear();
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.SetMinMaxValues(BegMeasure, endMeasure);
                if (series.Points.Count > 0)
                {
                    double maxY = (from SeriesPoint pt in series.Points
                                   select pt.Values[0]).Max();
                    ((XYDiagram)chartControl1.Diagram).AxisY.VisualRange.SetMinMaxValues(0, maxY * 1.5);
                }

                XYDiagram diagram = ((XYDiagram)chartControl1.Diagram);
                // Customize the appearance of the X-axis title. 
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "对齐里程";
                diagram.AxisX.Title.TextColor = Color.Black;
                diagram.AxisX.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                // Customize the appearance of the Y-axis title. 
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
                diagram.AxisY.Title.Text = colname;
                diagram.AxisY.Title.TextColor = Color.Black;
                diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Font = new Font("Tahoma", 9, FontStyle.Regular);

                //((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "对齐里程";
                //((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "特征点里程差";
                Image image = null;
                ImageFormat format = ImageFormat.Jpeg;
                // Create an image of the chart. 
                using (MemoryStream s = new MemoryStream())
                {
                    chartControl1.ExportToImage(s, format);
                    image = Image.FromStream(s);
                }
                control.ExportToImage(this.TempImagePath, ImageFormat.Jpeg);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //内检测对齐中线， 中线成果点统计
        private void buttonItemZhongxianTongji_Click(object sender, EventArgs e)
        {
            DataTable dt = CreateCenterlinePointStatisticsTable();
            FrmSingleTable fm = new FrmSingleTable(dt, "中线成果点统计");
            fm.ShowDialog();
        }
        //内检测对齐中线， 内检测点统计
        private void buttonItemNeijianceTongji_Click(object sender, EventArgs e)
        {
            DataTable dt = CreateInsidPointStatisticsTable();
            FrmSingleTable fm = new FrmSingleTable(dt, "内检测点统计");
            fm.ShowDialog();
        }
        //内检测对齐中线， 特征点里程差
        private void buttonItemTezhengdianLichengcha_Click(object sender, EventArgs e)
        {

            FrmSingleChart fm = new FrmSingleChart("特征点里程差统计图");
            CreateInsidPointCenterlineAlignmentDifferenceStatisticsImage(fm.chartControl);
            fm.ShowDialog();
        }

        //两次内检测对齐， 基准内检测统计
        private void buttonItemBaseNeijianceTongji_Click(object sender, EventArgs e)
        {
            DataTable dt = CreateInsideToInsiddeBaseStatisticsTable();
            FrmSingleTable fm = new FrmSingleTable(dt, "基准内检测点统计");
            fm.ShowDialog();
        }
        //两次内检测对齐，对齐内检测统计
        private void buttonItemAlignNeijianceTongji_Click(object sender, EventArgs e)
        {
             DataTable dt  = CreateInsideToInsiddeAlignmentTableStatisticsTable();
             FrmSingleTable fm = new FrmSingleTable(dt, "对齐内检测点统计");
             fm.ShowDialog();
        }
        //两次内检测对齐，特征点里程差
        private void buttonItemTezhendianLicheng_Click(object sender, EventArgs e)
        {
            FrmSingleChart fm = new FrmSingleChart("特征点里程差统计");
            CreateInsideToInsideAlignmentDifferenceStatisticsImage(fm.chartControl);
            fm.ShowDialog();
        }
        //两次内检测对齐，特征点壁厚统计
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            FrmSingleChart fm = new FrmSingleChart("内检测异常壁厚统计");
            CreateInsideToInsideAnomanyDepthImage(fm.chartControl);
            fm.ShowDialog();
        }
        //两次内检测对齐，特征点变化分析
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            FrmYichangToleranceSetting frm = new FrmYichangToleranceSetting(this.sourcetable);
            frm.ShowDialog();
        }
        //两次类检测对齐，数据对比
        private void buttonItemInsideInsideDiff_Click(object sender, EventArgs e)
        {           
            DataTable dt = CreateInsideToInsideDifferenceStaticticsTable();
            FrmSingleTable fm = new FrmSingleTable(dt, "两次内检测数据对比");
            fm.ShowDialog();
        }

        private void buttonItemWaijiance_Click(object sender, EventArgs e)
        {

        }

        private void buttonItemDB_Click(object sender, EventArgs e)
        {
            if (sourcetable.Columns.Contains(EvConfig.CISdb + "变化"))
            {
                FrmSingleChart fm = new FrmSingleChart(EvConfig.CISdb + "变化");
                CreateWaijianceToWaijianceMatchedAttrChangeImage(fm.chartControl, EvConfig.CISdb);
                fm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有" + EvConfig.CISdb + "变化" + "列");
            }

        }

        private void buttonItemGuandaomanshen_Click(object sender, EventArgs e)
        {
            if (sourcetable.Columns.Contains(EvConfig.CISGuanDingMaiShen + "变化"))
            {
                FrmSingleChart fm = new FrmSingleChart(EvConfig.CISGuanDingMaiShen + "变化");
                CreateWaijianceToWaijianceMatchedAttrChangeImage(fm.chartControl, EvConfig.CISGuanDingMaiShen);
                fm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有" + EvConfig.CISGuanDingMaiShen + "变化" + "列");
            }
        }

        private void buttonItemVOn_Click(object sender, EventArgs e)
        {
            if (sourcetable.Columns.Contains(EvConfig.CISVOn + "变化"))
            {
                FrmSingleChart fm = new FrmSingleChart(EvConfig.CISVOn + "变化");
                CreateWaijianceToWaijianceMatchedAttrChangeImage(fm.chartControl, EvConfig.CISVOn);
                fm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有" + EvConfig.CISVOn + "变化" + "列");
            }
        }

        private void buttonItemJiaoliudianya_Click(object sender, EventArgs e)
        {
            if (sourcetable.Columns.Contains(EvConfig.CISJiaoliudianya + "变化"))
            {
                FrmSingleChart fm = new FrmSingleChart(EvConfig.CISJiaoliudianya + "变化");
                CreateWaijianceToWaijianceMatchedAttrChangeImage(fm.chartControl, EvConfig.CISJiaoliudianya);
                fm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有" + EvConfig.CISJiaoliudianya + "变化" + "列");
            }
        }

        private void buttonItemVOff_Click(object sender, EventArgs e)
        {
            if (sourcetable.Columns.Contains(EvConfig.CISVOff + "变化"))
            {
                FrmSingleChart fm = new FrmSingleChart(EvConfig.CISVOff + "变化");
                CreateWaijianceToWaijianceMatchedAttrChangeImage(fm.chartControl, EvConfig.CISVOff);
                fm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有" + EvConfig.CISVOff + "变化" + "列");
            }
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            labelCount.Text = "记录数：" + gridView1.RowCount;
            chartControl3.Invalidate();
        }

        private void chartControl3_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            DevExpress.XtraCharts.BarDrawOptions drawOptions = e.SeriesDrawOptions as DevExpress.XtraCharts.BarDrawOptions;
            if (drawOptions == null)
                return;
            ChartControl ctcontrol = sender as ChartControl;
            //if (ctcontrol.SelectedItems.Contains(e.SeriesPoint))
            //{
            //    drawOptions.Color = Color.FromArgb(0, 255, 255);
            //}
            //else
            //{
            //    if (e.SeriesPoint[0] < (int)((DevExpress.XtraCharts.XYDiagram)ctcontrol.Diagram).Tag)
            //    {
            //        drawOptions.Color = Color.FromArgb(255, 0, 0);
            //    }
            //    else
            //    {
            //        drawOptions.Color = Color.DarkGreen;
            //    }
            //}
            // 第一个series是基准点
            if (e.Series.Equals(ctcontrol.Series[0]))
            {                
                if (e.SeriesPoint.Tag != null)
                {
                    int DataSourceidx = (int)(e.SeriesPoint.Tag);
                    int GridRowHandle = gridViewCenterline.GetRowHandle(DataSourceidx);
                    if (GridRowHandle < 0)
                    {
                        drawOptions.Color = Color.FromArgb(0, 255, 255, 255);
                    }
                    else
                    {
                        return;
                    }
                }
                
            }
               
            if (!e.Series.Equals(ctcontrol.Series[0]))
            {
                if (e.SeriesPoint.Tag != null)
                {
                    int DataSourceidx = (int)(e.SeriesPoint.Tag);
                    int GridRowHandle = gridView1.GetRowHandle(DataSourceidx);
                    if (GridRowHandle < 0)
                    {
                        drawOptions.Color = Color.FromArgb(0, 255, 255, 255);
                    }
                    else
                    {
                        return;
                    }
                }
            }
           
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "对齐结果文件 (*.rst)|*.rst";
            try
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(dlg.FileName))
                    {
                        file.Write(m_ResultType);
                    }
                    string folder = System.IO.Path.GetDirectoryName(dlg.FileName);
                    string filename;
                    //filename = dlg.FileName + ".GV1";
                    //using (MemoryStream stream = new MemoryStream())
                    //{
                    //    stream.Seek(0, SeekOrigin.Begin);
                    //    gridView1.SaveLayoutToStream(stream);
                    //    using (System.IO.FileStream file = new System.IO.FileStream(filename, FileMode.OpenOrCreate))
                    //    {
                    //        //stream.CopyTo(file);
                    //        file.Write(stream.ToArray(), 0, stream.ToArray().Length);
                    //        file.Flush();

                    //    }
                    //}
                    //filename = dlg.FileName + ".ct";
                    //using (MemoryStream stream = new MemoryStream())
                    //{
                    //    stream.Seek(0, SeekOrigin.Begin);
                    //    chartControl3.SaveToStream(stream);
                    //    using (System.IO.FileStream file = new System.IO.FileStream(filename, FileMode.OpenOrCreate))
                    //    {
                    //        //stream.CopyTo(file);
                    //        file.Write(stream.ToArray(), 0, stream.ToArray().Length);
                    //        file.Flush();
                    //    }

                    //}
                    //filename = dlg.FileName + ".gv2";
                    //using (MemoryStream stream = new MemoryStream())
                    //{
                    //    stream.Seek(0, SeekOrigin.Begin);
                    //    gridView1.SaveLayoutToStream(stream);
                    //    using (System.IO.FileStream file = new System.IO.FileStream(filename, FileMode.OpenOrCreate))
                    //    {
                    //        //stream.CopyTo(file);
                    //        file.Write(stream.ToArray(), 0, stream.ToArray().Length);
                    //        file.Flush();
                    //    }
                    //}
                    filename = dlg.FileName + ".stb";
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    DataSet ds  ;
                    if (sourcetable.DataSet != null)
                    {
                        ds = sourcetable.DataSet;
                    }
                    else
                    {
                        ds = new DataSet();
                        ds.Tables.Add(sourcetable);    
                    }
                    if (m_ResultType == "内检测对齐中线报告")
                    {
                        ds.Tables.Add(CenterlinePointTable);                        
                    }
                    if (m_ResultType == "两次内检测对齐报告")
                    {
                        
                      //  ds.Tables.Add(AlignmentPointTable.Copy());
                        ds.Tables.Add(BasePointTable.Copy());
                    }
                    if (m_ResultType == "焊缝对齐报告")
                    {
                        
                    }
                    if (m_ResultType == "外检测对齐中线报告")
                    {
                      //  ds.Tables.Add(AlignmentPointTable.Copy());
                        ds.Tables.Add(BasePointTable.Copy());
                    }
                    if (m_ResultType == "两次外检测对齐报告")
                    {
                        
                    }
                    
                    
                    ds.WriteXml(filename);
                    MessageBox.Show("保存成功！");

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDataforGridView1(string file)
        {
            gridView1.RestoreLayoutFromXml(file);
        }
        private void LoadChartControl(string file)
        {
            chartControl3.LoadFromFile(file);
        }
        private void LoadDataforGridView2(string file)
        {
            gridView2.RestoreLayoutFromXml(file);
        }
        public void LoadMatchResult(string file)
        {
            try
            {
                string filename;
                using(StreamReader sr = new StreamReader(file))
                {
                    string t = sr.ReadLine();
                    m_ResultType = t;
                }
                filename = file + ".stb";
                DataSet ds = new DataSet();
                ds.ReadXml(filename);
                sourcetable = ds.Tables[0];
                if (sourcetable.Columns.Contains("里程差") == false)
                    sourcetable.Columns.Add("里程差");
                //gridControl1.DataSource = sourcetable;
                //filename = file + ".GV1";
                ////LoadDataforGridView1(file);

                //filename = file + ".ct";
                //LoadChartControl(filename);
                //filename = file + ".gv2";
                //LoadDataforGridView2(filename);

                if (m_ResultType == "内检测对齐中线报告")
                {
                    CenterlinePointTable = ds.Tables[1];
                    InsidePointTable = sourcetable;
                    
                    //ds.Tables.Add(CenterlinePointTable);
                }
                if (m_ResultType == "两次内检测对齐报告")
                {
                    //ds.Tables.Add(AlignmentPointTable);
                    //ds.Tables.Add(BasePointTable);
                   // AlignmentPointTable = ds.Tables[1];
                    BasePointTable = ds.Tables[1];
                }
                if (m_ResultType == "焊缝对齐报告")
                {

                }
                if (m_ResultType == "外检测对齐中线报告")
                {
                    //ds.Tables.Add(AlignmentPointTable);
                    //ds.Tables.Add(BasePointTable);
                   // AlignmentPointTable = ds.Tables[1];
                    BasePointTable = ds.Tables[1];
                }
                if (m_ResultType == "两次外检测对齐报告")
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }

        private void chartControl3_CustomPaint(object sender, DevExpress.XtraCharts.CustomPaintEventArgs e)
        {
            DevExpress.XtraCharts.Series centerlineSeries = chartControl3.Series[0];
            DevExpress.XtraCharts.Series AlignedSeries = chartControl3.Series[2];
            DevExpress.XtraCharts.Series UnAlignedSeries = chartControl3.Series[1];
            XYDiagram diagram = (XYDiagram)chartControl3.Diagram;
            ControlCoordinates cod;
            ControlCoordinates cod2;

            var unAlignedSeriesPoints = UnAlignedSeries.Points.Where(x => MatchedRowList.Keys.Contains(sourcetable.Rows[(int)x.Tag])).ToList();

            // manunally selected
            foreach (SeriesPoint pt in unAlignedSeriesPoints)
            {
                float x = Convert.ToSingle(pt.Argument);
                float y = Convert.ToSingle(pt.Values[0]);
                cod = diagram.DiagramToPoint(x, y);

                int rowidx = (int)pt.Tag;
                DataRow r = sourcetable.Rows[rowidx];
                DataRow cRow = MatchedRowList[r];
                int centerlinePointindx = CenterlinePointTable.Rows.IndexOf(cRow);

                SeriesPoint pt2 = centerlineSeries.Points[centerlinePointindx];
                float x1 = Convert.ToSingle(pt2.Argument);
                float y1 = Convert.ToSingle(pt2.Values[0]);
                cod2 = diagram.DiagramToPoint(x1, y1, diagram.SecondaryAxesX[0], diagram.SecondaryAxesY[0]);
                if (MatchedRowListSelection.Contains(r))
                {
                    e.Graphics.DrawLine(Pens.LightSeaGreen, cod.Point.X, cod.Point.Y, cod2.Point.X, cod2.Point.Y);
                }
                else
                {
                    e.Graphics.DrawLine(Pens.Black, cod.Point.X, cod.Point.Y, cod2.Point.X, cod2.Point.Y);
                }

            }

            foreach (SeriesPoint pt in AlignedSeries.Points)
            {
                float x = Convert.ToSingle(pt.Argument);
                float y = Convert.ToSingle(pt.Values[0]);
                cod = diagram.DiagramToPoint(x, y);

                int rowidx = (int)pt.Tag;
                DataRow r = sourcetable.Rows[rowidx];
                if (!MatchedRowList.ContainsKey(r))
                    continue;
                DataRow cRow = MatchedRowList[r];
                int centerlinePointindx = CenterlinePointTable.Rows.IndexOf(cRow);

                SeriesPoint pt2 = centerlineSeries.Points[centerlinePointindx];
                float x1 = Convert.ToSingle(pt2.Argument);
                float y1 = Convert.ToSingle(pt2.Values[0]);
                cod2 = diagram.DiagramToPoint(x1, y1, diagram.SecondaryAxesX[0], diagram.SecondaryAxesY[0]);
                if (MatchedRowListSelection.Contains(r))
                {
                    e.Graphics.DrawLine(Pens.LightSeaGreen, cod.Point.X, cod.Point.Y, cod2.Point.X, cod2.Point.Y);
                }
                else
                {
                    e.Graphics.DrawLine(Pens.Black, cod.Point.X, cod.Point.Y, cod2.Point.X, cod2.Point.Y);
                }
            }

            

            
           
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            for (int i = MatchedRowList.Count - 1; i >= 0; i--)
            {
                DataRow imuR = MatchedRowList.Keys.ElementAt(i);
                if (MatchedRowListSelection.Contains(imuR))
                {
                    MatchedRowList.Remove(imuR);
                    chartControl3.Invalidate();
                }
            }
        }

        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            //DataTable CenterlinePointTable = this.CenterlinePointTable;
            DataTable IMUTable = sourcetable;
            double endM = IMUTable.AsEnumerable().Max(x => Convert.ToDouble( x[EvConfig.IMUAlignmentMeasureField]));
            double beginM = IMUTable.AsEnumerable().Min(x => Convert.ToDouble(x[EvConfig.IMUAlignmentMeasureField]));

            double centerlineLength = endM - beginM;
            if (!IMUTable.Columns.Contains("X"))
                IMUTable.Columns.Add("X", System.Type.GetType("System.Double"));
            if (!IMUTable.Columns.Contains("Y"))
                IMUTable.Columns.Add("Y", System.Type.GetType("System.Double"));
            if (!IMUTable.Columns.Contains("Z"))
                IMUTable.Columns.Add("Z", System.Type.GetType("System.Double"));
            if (!IMUTable.Columns.Contains("里程差"))
                IMUTable.Columns.Add("里程差", System.Type.GetType("System.Double"));
            if (!IMUTable.Columns.Contains("对齐里程"))
                IMUTable.Columns.Add("对齐里程", System.Type.GetType("System.Double"));

            double endIMUM = Convert.ToDouble(IMUTable.Rows[IMUTable.Rows.Count - 1][EvConfig.IMUMoveDistanceField]);
            double beginIMUM = Convert.ToDouble(IMUTable.Rows[0][EvConfig.IMUMoveDistanceField]);
            double IMULength = endIMUM - beginIMUM;

            foreach (DataRow r in IMUTable.Rows)
            {
                if (MatchedRowList.ContainsKey(r))
                {
                    r["对齐里程"] = MatchedRowList[r][EvConfig.CenterlineMeasureField];
                }
                else
                {
                    r["对齐里程"] = DBNull.Value;
                }
               
            }
            //IMUTable.Rows[IMUTable.Rows.Count - 1][EvConfig.IMUAlignmentMeasureField] = endM;
            //IMUTable.Rows[0][EvConfig.IMUAlignmentMeasureField] = beginM;
            

            Dictionary<string, string> TypeMatchDic = new Dictionary<string, string>();
            //key 中线点属性， value 内检测点属性
            TypeMatchDic.Add("阀门", "阀门");
            TypeMatchDic.Add("拐点", "弯头");
            TypeMatchDic.Add("三通", "三通");
            TypeMatchDic.Add("地面标记", "地面标记");
            TypeMatchDic.Add("开挖点", "开挖点");

            //Dictionary<string, string> SpecialMatchDic = new Dictionary<string, string>();
            //SpecialMatchDic.Add("阀门", "阀门");
            //SpecialMatchDic.Add("三通", "三通");
            //SpecialMatchDic.Add("地面标记", "地面标记");
            //SpecialMatchDic.Add("开挖点", "开挖点");
            ////根据特殊控制点强制对齐

            //if (!FrmCenterLineInsideAlignment.PreMatchBySpecialControlPoint(SpecialMatchDic, IMUTable, CenterlinePointTable))
            //{
            //    return;
            //}
            Dictionary<DataRow, DataRow> ManualSelectedRowPayer = new Dictionary<DataRow, DataRow>();
            foreach (DataRow imur in MatchedRowList.Keys)
            {
                ManualSelectedRowPayer.Add(imur, MatchedRowList[imur]);
            }
            Dictionary<DataRow, DataRow> MatchedDataRowPair = MatchedRowList;
            int LastMatchedPointsCount = -1;

            List<DataRow> NeijianceControlPointList = (from DataRow r in IMUTable.Rows
                                                       where (r["类型"] != DBNull.Value && FrmCenterLineInsideAlignment.IsNeiJianCeDianControlPointType(r["类型"].ToString(), TypeMatchDic))
                                                       || ManualSelectedRowPayer.ContainsKey(r)
                                                       select r).ToList();
            //手动选定特征点不用做匹配
            List<DataRow> ZhongXianControlPointList = (from DataRow r in CenterlinePointTable.Rows
                                                       where r["测点属性"] != DBNull.Value && FrmCenterLineInsideAlignment.IsZhongXianDianControlPointType(r["测点属性"].ToString(), TypeMatchDic)
                                                       && ManualSelectedRowPayer.ContainsValue(r) == false
                                                       select r).ToList();


           


          



            while (true)
            {
                int matchedrowCount = IMUTable.AsEnumerable().Where(x => x["对齐里程"] != DBNull.Value).Count();
                MatchedDataRowPair.Clear();
                for (int i = 0; i < NeijianceControlPointList.Count; i++)
                {
                    DataRow IMUr = NeijianceControlPointList[i];

                    double beforeM  ;
                    double beforeD ;
                    double nextM;
                    double nextD;

                    double ActionIMUM = (Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField]) - beginIMUM) * centerlineLength / IMULength + beginM;

                    // 手动匹配的点直接添加
                    if (ManualSelectedRowPayer.ContainsKey(IMUr))
                    {
                        IMUr["里程差"] = 0;
                        MatchedDataRowPair.Add(IMUr, ManualSelectedRowPayer[IMUr]);
                        continue;
                    }

                    if (IMUr["对齐里程"] != DBNull.Value)                    
                    {
                        ActionIMUM = Convert.ToDouble(IMUr["对齐里程"]);
                    }
                    else
                    {
                        // 还没找到对齐点通过整体长度计算里程
                        if (matchedrowCount == 0)
                        {
                            ActionIMUM = (Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField]) - beginIMUM) * centerlineLength / IMULength + beginM;
                        }
                        //找到了对齐点，用最近对齐点计算里程
                        else
                        {
                            DataRow beforeMatchedRow = IMUTable.AsEnumerable().Where(x => x["对齐里程"] != DBNull.Value &&
                                Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]) < Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField])).Last();
                              beforeM = Convert.ToDouble(beforeMatchedRow["对齐里程"]);
                              beforeD = Convert.ToDouble(beforeMatchedRow[EvConfig.IMUMoveDistanceField]);

                             DataRow nextMatchedRow;
                              
                             if (IMUTable.AsEnumerable().Any(x => x["对齐里程"] != DBNull.Value &&
                                 Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]) > Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField])))
                             {
                                 nextMatchedRow = IMUTable.AsEnumerable().Where(x => x["对齐里程"] != DBNull.Value &&
                               Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]) > Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField])).First();
                                 nextM = Convert.ToDouble(nextMatchedRow["对齐里程"]);
                                 nextD = Convert.ToDouble(nextMatchedRow[EvConfig.IMUMoveDistanceField]);
                             }
                             else
                             {
                                 nextM = endM;
                                 nextD = Convert.ToDouble(IMUTable.Rows[IMUTable.Rows.Count - 1][EvConfig.IMUMoveDistanceField]);
                             }

                          
                            

                            double currentD = Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField]);
                            ActionIMUM = (currentD - beforeD) * (nextM - beforeM) / (nextD - beforeD) + beforeM;
                        }
                    }
                    List<DataRow> Featurerow = (from r in ZhongXianControlPointList
                                                where Math.Abs(Convert.ToDouble(r[EvConfig.CenterlineMeasureField]) - ActionIMUM) < InsideCenterlineTolerance &&
                                                 FrmCenterLineInsideAlignment.IsZhongXianNejianceCouldMatch(r["测点属性"].ToString(), IMUr["类型"].ToString(), TypeMatchDic)
                                                select r).OrderBy(x => Math.Abs(Convert.ToDouble(x[EvConfig.CenterlineMeasureField]) - ActionIMUM)).ToList();
                    if (Featurerow.Count > 0)
                    {
                        DataRow NearestR = Featurerow[0];
                        if (MatchedDataRowPair.Values.Contains(NearestR) == false)
                        {
                            IMUr["里程差"] = Convert.ToDouble(NearestR[EvConfig.CenterlineMeasureField]) - ActionIMUM;
                            MatchedDataRowPair.Add(IMUr, NearestR);
                        }
                        else
                        {
                            DataRow mathcedIMUr = (from DataRow k in MatchedDataRowPair.Keys
                                                   where MatchedDataRowPair[k].Equals(NearestR)
                                                   select k).ToList().First();
                            double dis = Math.Abs(Convert.ToDouble(NearestR[EvConfig.CenterlineMeasureField]) - ActionIMUM);
                            double olddis = Math.Abs(Convert.ToDouble(mathcedIMUr["里程差"]));
                            if (dis < olddis)
                            {
                                MatchedDataRowPair.Remove(mathcedIMUr);
                                mathcedIMUr["里程差"] = DBNull.Value;
                                IMUr["里程差"] = Convert.ToDouble(NearestR[EvConfig.CenterlineMeasureField]) - ActionIMUM;
                                MatchedDataRowPair.Add(IMUr, NearestR);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                if (MatchedDataRowPair.Count <= LastMatchedPointsCount)
                {
                    break;
                }
                else
                {
                    LastMatchedPointsCount = MatchedDataRowPair.Count;
                }


                foreach (DataRow r in MatchedDataRowPair.Keys)
                {
                    r["对齐里程"] = MatchedDataRowPair[r][EvConfig.CenterlineMeasureField];
                }
                //未匹配上的点里程设置为null
                foreach (DataRow r in IMUTable.Rows)
                {
                    if (!MatchedDataRowPair.ContainsKey(r))
                    {
                        r["对齐里程"] = DBNull.Value;
                    }
                }
                //更新起始点和终点对齐里程
                if (IMUTable.AsEnumerable().Where(x => x["对齐里程"] != DBNull.Value).Count() > 0)
                {
                    DataRow begrow = IMUTable.Rows[0];
                    DataRow endrow = IMUTable.Rows[IMUTable.Rows.Count - 1];
                    DataRow fistMatchRow = IMUTable.AsEnumerable().Where(x => x["对齐里程"] != DBNull.Value).First();
                    DataRow lastMatchRow = IMUTable.AsEnumerable().Where(x => x["对齐里程"] != DBNull.Value).Last();
                    //根据匹配控制点和 记录距离计算终点和起点的对齐里程
                    if (begrow["对齐里程"] == DBNull.Value)
                    {
                        begrow["对齐里程"] = Convert.ToDouble(fistMatchRow["对齐里程"]) - (Convert.ToDouble(fistMatchRow[EvConfig.IMUMoveDistanceField])
                            - Convert.ToDouble(begrow[EvConfig.IMUMoveDistanceField]));
                    }
                    if (endrow["对齐里程"] == DBNull.Value)
                    {
                        endrow["对齐里程"] = Convert.ToDouble(lastMatchRow["对齐里程"]) + (Convert.ToDouble(endrow[EvConfig.IMUMoveDistanceField])
                           - Convert.ToDouble(lastMatchRow[EvConfig.IMUMoveDistanceField]));
                    }
                }
                 
            }
            FrmCenterLineInsideAlignment.CalculateNullMeasureBasedOnControlpointMeasure(ref IMUTable); 

            IFeatureLayer pLinearlayer = this.CenterlineLayer as IFeatureLayer;
            
            IFeatureCursor pcursor = pLinearlayer.FeatureClass.Search(null, false);
            IFeature pFeature = pcursor.NextFeature();
            IPolyline pline = pFeature.Shape as IPolyline;
            IMSegmentation mSegment = pline as IMSegmentation;
            double maxM = mSegment.MMax;
            double minM = mSegment.MMin;

            //if (beginM > maxM || beginM < minM || endM > maxM || endM < minM)
            //{
            //    MessageBox.Show("输入的起始或终点里程值超出中线里程范围!");
            //    return;
            //}

            for (int i = 0; i < IMUTable.Rows.Count; i++)
            {
                DataRow r = IMUTable.Rows[i];
                double M = Convert.ToDouble(r["对齐里程"]);
                if (M < mSegment.MMin)
                    M = mSegment.MMin;
                if (M > mSegment.MMax)
                    M = mSegment.MMax;

                IGeometryCollection ptcollection = mSegment.GetPointsAtM(M, 0);
                IPoint pt = ptcollection.get_Geometry(0) as IPoint;
                r["X"] = pt.X;
                r["Y"] = pt.Y;
                r["Z"] = pt.Z;

            }

            CreateInsidPointCenterlineAlignmentStatisticsImage(this.chartControl3);

            this.gridControlPrecision.DataSource = CreateInsidPointCenterlineAlignmentStatisticsTable();
            this.gridControlPrecision.MainView.RefreshData();
        }

        
        
    }
    public enum ChartControlToolTypeEnum
    {
        ManualMatch,
        SelectPair,

    }

    public class ChartZoomScrollHelper
    {
        private readonly ChartControl chartControl;
        public ChartControlToolTypeEnum CurrentTool;
        #region Properties
        public ViewType CurrentViewType { get; set; }
        public Point FirstPoint { get; private set; }
        public Point SecondPoint { get; private set; }
        public bool HoldMouse = false;
        #endregion
        public delegate void ChartControlSelectedItemManunalyChanged(object sender, ChartControl chart);
        public event ChartControlSelectedItemManunalyChanged SelectedItemManunalyChanged;

        public delegate void ChartMouseDragCompleted(object sender, Point FirstPoint, Point SencondPoint);
        public event ChartMouseDragCompleted MouseDragCompleted;
        
        public ChartZoomScrollHelper(ChartControl chart)
        {
            chartControl = chart;
            chartControl.CustomPaint += chartControl1_CustomPaint;
            chartControl.MouseDown += chartControl1_MouseDown;
            chartControl.MouseMove += chartControl1_MouseMove;
            chartControl.MouseUp += chartControl1_MouseUp;

        }
        private void chartControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HoldMouse = true;
            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            DiagramCoordinates dcd = diagram.PointToDiagram(e.Location);
            if (dcd.NumericalValue <= 0 || dcd.NumericalValue > ((double)diagram.AxisY.VisualRange.MaxValue))
            {
                return;
            }
            FirstPoint = e.Location;

        }

        private void chartControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HoldMouse = false;

            if (ShouldZoom(e))
            {
                DiagramCoordinates cod;
                XYDiagram diagram = (XYDiagram)chartControl.Diagram;
                if (FirstPoint != null && SecondPoint != null)
                {
                    // int minX = Math.Min(FirstPoint.Value.X, SecondPoint.Value.X);
                    cod = diagram.PointToDiagram(new Point(FirstPoint.X, FirstPoint.Y));
                    double x1 = cod.NumericalArgument;

                    // int maxX = Math.Max(FirstPoint.Value.X, SecondPoint.Value.X);
                    cod = diagram.PointToDiagram(new Point(SecondPoint.X, SecondPoint.Y));
                    double x2 = cod.NumericalArgument;

                    double maxM = Math.Max(x1, x2);
                    double minM = Math.Min(x1, x2);

                     
                    
                }
                
            }
            if (FirstPoint != Point.Empty && SecondPoint != Point.Empty)
            {
                MouseDragCompleted(chartControl, FirstPoint, SecondPoint);
            }
            SecondPoint = Point.Empty;
            FirstPoint = Point.Empty;

        }

        private void CalcArgVisualRange(XYDiagram diagram, out object argMin, out object argMax)
        {
            int minX = Math.Min(FirstPoint.X, SecondPoint.X);
            int maxX = Math.Max(FirstPoint.X, SecondPoint.X);
            argMin = diagram.PointToDiagram(new Point(minX, SecondPoint.Y)).NumericalArgument;
            argMax = diagram.PointToDiagram(new Point(maxX, SecondPoint.Y)).NumericalArgument;
        }

        private void chartControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            SecondPoint = e.Location;

            if (ShouldScroll())
            {
                if (FirstPoint != null && SecondPoint != null)
                {
                    XYDiagram diagram = (XYDiagram)chartControl.Diagram;

                    double argMin = diagram.PointToDiagram(new Point(FirstPoint.X, SecondPoint.Y)).NumericalArgument;
                    double argMax = diagram.PointToDiagram(new Point(SecondPoint.X, SecondPoint.Y)).NumericalArgument;
                    double diff = -(argMax - argMin);
                    double newMin = (double)diagram.AxisX.VisualRange.MinValue + diff;
                    double newMax = (double)diagram.AxisX.VisualRange.MaxValue + diff;
                    if ((newMin >= (double)diagram.AxisX.WholeRange.MinValue) && newMax <= (double)diagram.AxisX.WholeRange.MaxValue)
                        diagram.AxisX.VisualRange.SetMinMaxValues(newMin, newMax);
                }
                FirstPoint = SecondPoint;
            }
        }
        private void chartControl1_CustomPaint(object sender, DevExpress.XtraCharts.CustomPaintEventArgs e)
        {
            if (ShouldZoom())
                DrawZoomBox(e);
            if (ManualMatch())
                DrawConnectLine(e);
                

        }
       

        private bool ManualMatch()
        {
            return CurrentTool == ChartControlToolTypeEnum.ManualMatch && FirstPoint != null && Control.MouseButtons == MouseButtons.Left;
        }
       
        private void DrawConnectLine(DevExpress.XtraCharts.CustomPaintEventArgs e)
        {
            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            if (FirstPoint == Point.Empty || SecondPoint == Point.Empty)
                return;
            ControlCoordinates cod;
            ControlCoordinates cod2;
            if (FirstPoint == null || SecondPoint == null)
                return;             
            cod = diagram.DiagramToPoint(FirstPoint.X, FirstPoint.Y);
           
            cod2 = diagram.DiagramToPoint(SecondPoint.X, SecondPoint.Y);
            int minY = cod.Point.Y;
            e.Graphics.DrawLine(Pens.Purple, FirstPoint.X, FirstPoint.Y, SecondPoint.X, SecondPoint.Y); 
        }
        private void DrawZoomBox(DevExpress.XtraCharts.CustomPaintEventArgs e)
        {
            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            ControlCoordinates cod;
            if (FirstPoint == Point.Empty || SecondPoint == Point.Empty)
                return;

            int minX = Math.Min(FirstPoint.X, SecondPoint.X);
            //int minY = Math.Min(FirstPoint.Value.Y, SecondPoint.Value.Y);
            cod = diagram.DiagramToPoint(0, 0, diagram.AxisX, diagram.AxisY);
            int maxY = cod.Point.Y;
            int maxX = Math.Max(FirstPoint.X, SecondPoint.X);
            //int maxY = Math.Max(FirstPoint.Value.Y, SecondPoint.Value.Y);
            cod = diagram.DiagramToPoint(0, 0, diagram.SecondaryAxesX[0], diagram.SecondaryAxesY[0]);
            int minY = cod.Point.Y;
            e.Graphics.DrawRectangle(Pens.Black, minX, minY, maxX - minX, maxY - minY);
        }
        private bool ShouldScroll()
        {
            return FirstPoint != null && Control.MouseButtons == MouseButtons.Right;
        }
        private bool ShouldZoom()
        {
            return CurrentTool == ChartControlToolTypeEnum.SelectPair && FirstPoint != null && Control.MouseButtons == MouseButtons.Left;
        }
        private bool ShouldZoom(MouseEventArgs e)
        {
            return FirstPoint != null && e.Button == MouseButtons.Left;
        }

       

    }

}
