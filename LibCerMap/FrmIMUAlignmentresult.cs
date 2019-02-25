﻿using System;
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

namespace LibCerMap
{
    public partial class FrmIMUAlignmentresult : OfficeForm
    {
        private DataTable sourcetable;
        private string m_ResultType;
        private string TempImagePath;
        //内检测对齐中线
        public DataTable CenterlinePointTable;
        public DataTable InsidePointTable;

        // 两次内检测对齐
        public DataTable BasePointTable;
        public DataTable AlignmentPointTable;

        public FrmIMUAlignmentresult(DataTable tb)
        {
            InitializeComponent();
            sourcetable = tb;
            TempImagePath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\TempImage.jpg";
        }

        private void FrmIMUAlignmentresult_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = sourcetable;
            gridControl1.Refresh();
            gridView1.OptionsView.ColumnAutoWidth = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                    IFeature pF = pfc.CreateFeature();
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
                    pF.Store();
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
            ds.Tables.Add(dt);

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

            DataTable dt = CreateCenterlinePointStatisticsTable();
            string BookMarkName = "tbCenterline";
            ExportDatatableToWordBookMark(dt, BookMarkName, doc);

            BookMarkName = "tbPoint";
            dt = CreateInsidPointStatisticsTable();
            ExportDatatableToWordBookMark(dt, BookMarkName, doc);

            BookMarkName = "tbAlignment";
            dt = CreateInsidPointCenterlineAlignmentStatisticsTable();
            ExportDatatableToWordBookMark(dt, BookMarkName, doc);

            BookMarkName = "tbMeasureDifference";
            FormChart fm = new FormChart();
            CreateInsidPointCenterlineAlignmentDifferenceStatisticsImage(fm.ChartContrl);
            fm.ShowDialog();
            ExportImageToWordBookMark(BookMarkName, doc, app);

            //!<输出完毕后关闭doc对象
            object IsSave = true;
            doc.Close(ref IsSave, ref missing, ref missing);
            app.Quit(ref missing, ref missing, ref missing);
            MessageBox.Show("报告生成完毕");
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
            ExportImageToWordBookMark(BookMarkName, doc, app);

            BookMarkName = "tbtaAnomanyDepth";             
            CreateInsideToInsideAnomanyDepthImage(fm.ChartContrl);
            ExportImageToWordBookMark(BookMarkName, doc, app);

            BookMarkName = "tbtaAnomanyDepthChange";
            CreateInsideToInsideMatchedAnomanyDepthChangeImage(fm.ChartContrl);
            ExportImageToWordBookMark(BookMarkName, doc, app);
             
            //!<输出完毕后关闭doc对象
            object IsSave = true;
            doc.Close(ref IsSave, ref missing, ref missing);
            app.Quit(ref missing, ref missing, ref missing);
            MessageBox.Show("报告生成完毕");
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
            dt.Columns.Add("起点记录距离");
            dt.Columns.Add("终点记录距离");
            dt.Columns.Add("内检测点数");
            dt.Columns.Add("环向焊缝数");
            dt.Columns.Add("三通数");
            dt.Columns.Add("弯头数");
            dt.Columns.Add("异常数");
            DataRow dr = dt.NewRow();
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
             
            dr["特征点平均里程差"] = Math.Round((from DataRow r in stable.Rows
                                         where r["里程差"] != DBNull.Value
                                         select Math.Abs(Convert.ToDouble(r["里程差"]))).Average(), 2);
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
            dr[EvConfig.IMUInspectionYearField] = BasePointTable.Rows[0][EvConfig.IMUInspectionYearField];
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
            dr[EvConfig.IMUInspectionYearField] = BasePointTable.Rows[0][EvConfig.IMUInspectionYearField];
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
            dr["匹配异常数"] =  (from DataRow r in stable.Rows
                                         where r["壁厚变化"] != DBNull.Value
                                         select r).ToList().Count;
            dt.Rows.Add(dr);
            return dt;
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
    }

}
