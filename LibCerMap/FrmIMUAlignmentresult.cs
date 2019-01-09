using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
namespace LibCerMap
{
    public partial class FrmIMUAlignmentresult : OfficeForm
    {
        private DataTable sourcetable;
        public FrmIMUAlignmentresult(DataTable tb)
        {
            InitializeComponent();
            sourcetable = tb;
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
            catch(Exception ex)
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
                return ;
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
    }
}
