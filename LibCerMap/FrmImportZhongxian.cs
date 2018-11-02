using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using stdole;

using System.Windows.Forms.DataVisualization.Charting;

using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

using DevComponents.DotNetBar;
using System.IO;

using System.Data.OleDb;
using System.Runtime.InteropServices;

using System.Threading;

namespace LibCerMap
{
    public partial class FrmImportZhongxian : OfficeForm
    {
        AxMapControl m_pMapCtl;
        public string XMLPath;
        public string gdbPath;
        public string symFilePath;
        AxTOCControl m_pTOCCtl;
        string MapTempPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate";
        public FrmImportZhongxian(AxMapControl mcontrol, AxTOCControl mtoccontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pMapCtl = mcontrol;
            m_pTOCCtl = mtoccontrol;
        }

        private void btnOpenXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel文件(*.xls)|*.xls;*.xlsx|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLPath = dlg.FileName;
                textBoxX1.Text = XMLPath;
                //string filename = System.IO.Path.GetDirectoryName(XMLPath)+"\\shp\\" + System.IO.Path.GetFileNameWithoutExtension(XMLPath) + ".shp";
                //textBoxX2.Text = filename;
            }
            if (XMLPath.Contains("中线成果"))
            {
                textBoxX3.Text = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate\中线成果点.mxd";
            }
            else if (XMLPath.Contains("桩"))
            {
                textBoxX3.Text = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate\桩.mxd";
            }
        }

        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            //add all point 
            List<IPoint> pointList = new List<IPoint>();

            Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application(); 
            object missing = System.Reflection.Missing.Value;
            myExcel.Application.Workbooks.Open(textBoxX1.Text, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing); //this.txtFile.Text为Excel文件的全路径
            Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks[1];

            //获取第一个Sheet
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Sheets[1];
            string sheetName = sheet.Name; //Sheet名
            myBook.Close(Type.Missing, Type.Missing, Type.Missing);
            myExcel.Quit();

            //read excel file to creat Field
            string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + this.textBoxX1.Text + ";Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(strCon);
            string sql1 = string.Format("select * from [{0}$]", sheetName);
            conn.Open();

            OleDbDataAdapter myCommand = new OleDbDataAdapter(sql1, strCon);
            DataSet ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();

            int xIndex = 0;
            int yIndex = 0;
            int zIndex = 0;
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (ds.Tables[0].Columns[i].ColumnName == "X_经度")
                {
                    xIndex = i;
                }
                if (ds.Tables[0].Columns[i].ColumnName == "Y_纬度")
                {
                    yIndex = i;
                }
                if (ds.Tables[0].Columns[i].ColumnName.Contains("Z_高程"))
                {
                    zIndex = i;
                }
            }

            ISpatialReference pSpaReference = new UnknownCoordinateSystemClass();
            pSpaReference.SetDomain(-8000000, 8000000, -800000, 8000000);
            IFeatureClass pFt = CreateShapeFile(ds, this.textBoxX2.Text, pSpaReference);

            if (pFt == null)
            {
                return;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //根据XY坐标添加点,edit attribute
                    IsNumberic isNum = new IsNumberic();
                    ESRI.ArcGIS.Geometry.IPoint pPoint = new ESRI.ArcGIS.Geometry.PointClass();
                    if (ds.Tables[0].Rows[i][xIndex].ToString() == "" || ds.Tables[0].Rows[i][xIndex].ToString() == " ")
                    {
                        break;
                    }
                    if (isNum.IsNumber(ds.Tables[0].Rows[i][xIndex].ToString()) && isNum.IsNumber(ds.Tables[0].Rows[i][yIndex].ToString()))
                    {
                        pPoint.X = System.Convert.ToSingle(ds.Tables[0].Rows[i][xIndex].ToString());
                        pPoint.Y = System.Convert.ToSingle(ds.Tables[0].Rows[i][yIndex].ToString());
                        pPoint.Z = System.Convert.ToSingle(ds.Tables[0].Rows[i][zIndex].ToString());
                        IFeature pFeature = pFt.CreateFeature();
                        pFeature.Shape = pPoint;

                        pFeature.Store();
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            if (ds.Tables[0].Columns[j].ColumnName.Contains("里程"))
                            {
                                continue;
                            }
                            //pFeature.set_Value(pFeature.Fields.FindField(ds.Tables[0].Columns[j].ColumnName), ds.Tables[0].Rows[i][j]);
                            pFeature.set_Value(j + 2, ds.Tables[0].Rows[i][j].ToString());
                        }
                        pFeature.Store();

                        pointList.Add(pPoint);
                    }
                    else
                    {
                        MessageBox.Show("the" + i + "rows x and y value is unvalid!");
                    }
                }
            }

            ClsGDBDataCommon processDataCommon = new ClsGDBDataCommon();
            string strInputPath = System.IO.Path.GetDirectoryName(textBoxX2.Text);
            string strInputName = System.IO.Path.GetFileName(textBoxX2.Text);

            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = pFt;
            pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(strInputName));


            //create line shape file
            IPointCollection PointCollection = ReadPoint(pFeatureLayer);
            string lineName = strInputPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(strInputName) + "_line.shp";
            CreateLineShpFile(lineName, pSpaReference);
            //将所有的点连接成线
            List<IPolyline> Polyline = CreatePolyline(PointCollection);
            List<double> lineLengthList = new List<double>();
            //将连接成的线添加到线图层中
            string pLineFile = lineName;
            string pFilePath = System.IO.Path.GetDirectoryName(pLineFile);
            string pFileName = System.IO.Path.GetFileName(pLineFile);
            //打开工作空间   
            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pWS = (IFeatureWorkspace)pWSF.OpenFromFile(pFilePath, 0);
            //写入实体对象
            IFeatureLayer plineLayer = new FeatureLayerClass();
            plineLayer.FeatureClass = pWS.OpenFeatureClass(pFileName);
            AddFeature(plineLayer, Polyline, pointList, lineLengthList);
            plineLayer.Name = pFeatureLayer.Name + "_line";

            m_pMapCtl.AddLayer(plineLayer as ILayer, 0);
            m_pMapCtl.AddLayer(pFeatureLayer as ILayer, 0);
            m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            ImpSymbolFromFile(textBoxX3.Text, pFeatureLayer, plineLayer);

            this.Close();           
        }

        private void btnImpSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Title = "Save file";
                fbd.Filter = "shape文件(*.shp)|*.shp";
                fbd.InitialDirectory = System.IO.Path.GetDirectoryName(XMLPath);
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(fbd.FileName + ".shp") == true)
                    {
                        MessageBox.Show("shp已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    //IWorkspaceName workspaceName = workspaceFactory.Create(System.IO.Path.GetDirectoryName(fbd.FileName), System.IO.Path.GetFileName(fbd.FileName), null, 0);
                    //gdbPath = fbd.FileName + ".gdb";
                    textBoxX2.Text = fbd.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonX_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public IFeatureClass CreateShapeFile(DataSet ds, string strOutShpName, ISpatialReference pSRF)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(strOutShpName);
                string filefolder = di.Parent.FullName;
                ClsGDBDataCommon cdc = new ClsGDBDataCommon();
                //cdc.OpenFromShapefile(filefolder);
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                //设置字段   
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;

                //创建类型为几何类型的字段   
                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;

                pGeoDefEdit.HasM_2 = false;
                pGeoDefEdit.HasZ_2 = false;
                pGeoDefEdit.SpatialReference_2 = pSRF;

                pFieldEdit.Name_2 = "SHAPE";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                pFieldEdit.GeometryDef_2 = pGeoDef;
                //pFieldEdit.IsNullable_2 = true;
                //pFieldEdit.Required_2 = true;
                pFieldsEdit.AddField(pField);

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = ds.Tables[0].Columns[i].ColumnName;
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    if (ds.Tables[0].Columns[i].ColumnName.Contains("Z_高程"))
                    {
                        pFieldEdit.Name_2 = "Z_高程（米";
                    }
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
                IFeatureClass pFeatureClass = pFeatureWorkspace.CreateFeatureClass(di.Name, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");

                return pFeatureClass;
                //IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                //pFeatureLayer.FeatureClass = pFeatureClass;
                //pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(di.Name);
                //m_mapControl.AddLayer(pFeatureLayer as ILayer, 0);
                //m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
                return null;
            }

        }

        public void CreateLineShpFile(string fileName, ISpatialReference pSRF)
        {
            //判断需要生成的线文件是否存在，若不存在则创建文件，若存在则将新添加的线写入文件
            if (File.Exists(fileName))
            {
                MessageBox.Show("file already exist, please change file name!");
                return;
            }
            else
            {
                string pLineFile = fileName;
                string pFilePath = System.IO.Path.GetDirectoryName(pLineFile);
                string pFileName = System.IO.Path.GetFileName(pLineFile);
                //打开工作空间   
                IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
                IFeatureWorkspace pWS = (IFeatureWorkspace)pWSF.OpenFromFile(pFilePath, 0);

                //判断文件是否存在，若不存在则创建文件
                if (!(File.Exists(fileName)))
                {
                    const string strShapeFieldName = "shape";
                    //设置字段集   
                    IFields pFields = new FieldsClass();
                    IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                    //设置字段   
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = (IFieldEdit)pField;

                    //创建类型为几何类型的字段   
                    pFieldEdit.Name_2 = strShapeFieldName;
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    //为esriFieldTypeGeometry类型的字段创建几何定义，包括类型和空间参照    
                    IGeometryDef pGeoDef = new GeometryDefClass();     //The geometry definition for the field if IsGeometry is TRUE.   
                    IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                    pGeoDefEdit.SpatialReference_2 = pSRF;
                    pFieldEdit.GeometryDef_2 = pGeoDef;
                    pFieldsEdit.AddField(pField);

                    //add length field
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "length";
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);

                    //add length field
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "质量";
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);

                    //创建shapefile线图层 
                    pWS.CreateFeatureClass(pFileName, pFields, null, null, esriFeatureType.esriFTSimple, strShapeFieldName, "");
                }
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

            IPointCollection pPointCollection = new MultipointClass();
            for (int i = 0; i < number; i++)
            {
                IGeometry pGeometry = pFeature.Shape as IGeometry;
                IPoint pPoint = pGeometry as IPoint;

                pPointCollection.AddPoint(pPoint);

                pFeature = pFeatureCursor.NextFeature();
            }

            return pPointCollection;
        }

        //将收集到的点转换成线
        private List<IPolyline> CreatePolyline(IPointCollection pPointcollection)
        {
            List<IPolyline> lineList = new List<IPolyline>();
            int PointNumber = int.Parse(pPointcollection.PointCount.ToString());

            object o = Type.Missing;

            for (int i = 0; i < PointNumber - 1; i++)
            {
                //线数组
                ISegmentCollection pSegmentCollection = new PolylineClass();
                ILine pLine = new LineClass();

                pLine.PutCoords(pPointcollection.get_Point(i), pPointcollection.get_Point(i + 1));

                pSegmentCollection.AddSegment((ISegment)pLine, ref o, ref o);

                IPolyline pPolyline = new PolylineClass();
                pPolyline = pSegmentCollection as IPolyline;
                lineList.Add(pPolyline);
            }

            return lineList;
        }

        //将线添加到图层
        private void AddFeature(IFeatureLayer pFeatureLayer, List<IPolyline> lineList, List<IPoint> ptsList, List<double> lineLengthList)
        {
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IDataset pDataset = (IDataset)pFeatureClass;
            IWorkspace pWorkspace = pDataset.Workspace;

            //for (int i = 0; i < lineList.Count; i++)
            //{
            //    if (lineList[i].Length > 0)
            //    {
            //        IFeature pFeature = pFeatureClass.CreateFeature();
            //        pFeature.Shape = lineList[i];
            //        double sLength = Math.Round((ptsList[i + 1].X - ptsList[i].X) * (ptsList[i + 1].X - ptsList[i].X) + (ptsList[i + 1].Y - ptsList[i].Y) * (ptsList[i + 1].Y - ptsList[i].Y) + (ptsList[i + 1].Z - ptsList[i].Z) * (ptsList[i + 1].Z - ptsList[i].Z),4);
            //        pFeature.set_Value(pFeature.Fields.FindField("length"), sLength);
            //        pFeature.Store();
            //    }
            //}
            //开始空间编辑
            IWorkspaceEdit pWorkspaceEdit = (IWorkspaceEdit)pWorkspace;
            pWorkspaceEdit.StartEditing(true);
            pWorkspaceEdit.StartEditOperation();
            IFeatureBuffer pFeatureBuffer = pFeatureClass.CreateFeatureBuffer();
            IFeatureCursor pFeatureCursor;

            //开始插入新的实体对象
            for (int i = 0; i < lineList.Count; i++)
            {
                IGeometry geometry = lineList[i];
                pFeatureCursor = pFeatureClass.Insert(true);
                pFeatureBuffer.Shape = geometry;
                double sLength = Math.Round((ptsList[i + 1].X - ptsList[i].X) * (ptsList[i + 1].X - ptsList[i].X) + (ptsList[i + 1].Y - ptsList[i].Y) * (ptsList[i + 1].Y - ptsList[i].Y) + (ptsList[i + 1].Z - ptsList[i].Z) * (ptsList[i + 1].Z - ptsList[i].Z), 4);
                lineLengthList.Add(sLength);
                pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField("length"), sLength);
                if (sLength > System.Convert.ToSingle(textBoxX4.Text.ToString()))
                {
                    pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField("质量"), "坏点");
                }
                else
                {
                    pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField("质量"), "好点");
                }
                object featureOID = pFeatureCursor.InsertFeature(pFeatureBuffer);
                //保存实体
                pFeatureCursor.Flush();
                //结束空间编辑
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgMapSymbol = new OpenFileDialog();
            dlgMapSymbol.Title = "选择模板";
            dlgMapSymbol.InitialDirectory = ".";
            dlgMapSymbol.Filter = "mxd文件(*.mxd)|*.mxd";
            dlgMapSymbol.RestoreDirectory = true;
            if (dlgMapSymbol.ShowDialog() == DialogResult.OK)
            {
                symFilePath = dlgMapSymbol.FileName;
                textBoxX3.Text = dlgMapSymbol.FileName;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 对生成的各图层进行符号、标注设置
        /// </summary>
        /// <param name="impfilepath">导入符号mxd文件路径</param>
        /// <param name="passpoint">生成的点图层</param>
        /// <param name="passline">生成的线图层</param>
        public void ImpSymbolFromFile(string impfilepath, ILayer passpoint, ILayer passline)
        {
            IMapDocument pMapDocument = new MapDocumentClass();
            pMapDocument.Open(impfilepath, null);
            IMap pMap = pMapDocument.Map[0];

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                ILayer pLayerSymbol = pMap.get_Layer(i);
                if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("point"))
                {
                    IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                    IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                    IFeatureLayer pFPasspointWait = passpoint as IFeatureLayer;
                    IGeoFeatureLayer pGPasspointWait = pFPasspointWait as IGeoFeatureLayer;
                    if (pGFLayerSymbol != null)
                    {
                        pGPasspointWait.Renderer = pGFLayerSymbol.Renderer;

                        IAnnotateLayerProperties pAnnoLayerP;// = new LabelEngineLayerPropertiesClass();//渲染图层的符号图层标注
                        IElementCollection PELECOLL;//queryitem的参数，用不到
                        IElementCollection pelecoll;//同上
                        pGFLayerSymbol.AnnotationProperties.QueryItem(0, out pAnnoLayerP, out  PELECOLL, out pelecoll);
                        ILabelEngineLayerProperties pLabelEenLayPro = pAnnoLayerP as ILabelEngineLayerProperties;


                        IFontDisp pFont = new StdFontClass() as IFontDisp;
                        ITextSymbol pTextSymbol = new TextSymbolClass();
                        pTextSymbol.Color = pLabelEenLayPro.Symbol.Color;
                        pFont.Bold = pLabelEenLayPro.Symbol.Font.Bold;
                        pFont.Italic = pLabelEenLayPro.Symbol.Font.Italic;
                        pFont.Strikethrough = pLabelEenLayPro.Symbol.Font.Strikethrough;
                        pFont.Underline = pLabelEenLayPro.Symbol.Font.Underline;
                        pFont.Name = pLabelEenLayPro.Symbol.Font.Name;
                        pFont.Size = pLabelEenLayPro.Symbol.Font.Size;
                        pTextSymbol.Font = pFont;

                        pGPasspointWait.AnnotationProperties.Clear();
                        ILabelEngineLayerProperties pLabelWaitLayPro = new LabelEngineLayerPropertiesClass();//设置待渲染图层的标注
                        pLabelWaitLayPro.Expression = pLabelEenLayPro.Expression;
                        pLabelWaitLayPro.Symbol = pTextSymbol;

                        IAnnotateLayerProperties pAnnLayProWait = pLabelWaitLayPro as IAnnotateLayerProperties;//新生成的符号图层设置标注
                        pAnnLayProWait.DisplayAnnotation = true;
                        pAnnLayProWait.FeatureLayer = pGPasspointWait;
                        pAnnLayProWait.LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures;
                        pAnnLayProWait.WhereClause = "";
                        pGPasspointWait.AnnotationProperties.Add(pAnnLayProWait);
                        pGPasspointWait.DisplayAnnotation = true;
                    }
                }
                else if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("line"))
                {
                    IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                    IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                    IFeatureLayer pFPasspointWait = passline as IFeatureLayer;
                    IGeoFeatureLayer pGPasspointWait = pFPasspointWait as IGeoFeatureLayer;
                    if (pGFLayerSymbol != null)
                    {
                        pGPasspointWait.Renderer = pGFLayerSymbol.Renderer;

                        IAnnotateLayerProperties pAnnoLayerP = new LabelEngineLayerPropertiesClass();//符号图层标注
                        IElementCollection PELECOLL;//queryitem的参数，用不到
                        IElementCollection pelecoll;//同上
                        pGFLayerSymbol.AnnotationProperties.QueryItem(0, out pAnnoLayerP, out  PELECOLL, out pelecoll);
                        ILabelEngineLayerProperties pLabelEenLayPro = pAnnoLayerP as ILabelEngineLayerProperties;

                        IFontDisp pFont = new StdFontClass() as IFontDisp;
                        ITextSymbol pTextSymbol = new TextSymbolClass();
                        pTextSymbol.Color = pLabelEenLayPro.Symbol.Color;
                        pFont.Bold = pLabelEenLayPro.Symbol.Font.Bold;
                        pFont.Italic = pLabelEenLayPro.Symbol.Font.Italic;
                        pFont.Strikethrough = pLabelEenLayPro.Symbol.Font.Strikethrough;
                        pFont.Underline = pLabelEenLayPro.Symbol.Font.Underline;
                        pFont.Name = pLabelEenLayPro.Symbol.Font.Name;
                        pFont.Size = pLabelEenLayPro.Symbol.Font.Size;
                        pTextSymbol.Font = pFont;

                        pGPasspointWait.AnnotationProperties.Clear();
                        ILabelEngineLayerProperties pLabelWaitLayPro = new LabelEngineLayerPropertiesClass();//设置待渲染图层的标注
                        pLabelWaitLayPro.Expression = pLabelEenLayPro.Expression;
                        pLabelWaitLayPro.Symbol = pTextSymbol;

                        IAnnotateLayerProperties pAnnLayProWait = pLabelWaitLayPro as IAnnotateLayerProperties;//新生成的符号图层设置标注
                        pAnnLayProWait.DisplayAnnotation = true;
                        pAnnLayProWait.FeatureLayer = pGPasspointWait;
                        pAnnLayProWait.LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures;
                        pAnnLayProWait.WhereClause = "";
                        pGPasspointWait.AnnotationProperties.Add(pAnnLayProWait);
                        pGPasspointWait.DisplayAnnotation = true;
                    }
                }
            }
            m_pTOCCtl.SetBuddyControl(m_pMapCtl);
            m_pTOCCtl.ActiveView.Refresh();
            m_pMapCtl.ActiveView.Refresh();
        }

        private void FrmImportZhongxian_Load(object sender, EventArgs e)
        {
            textBoxX3.Text = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate\中线成果点.mxd";
        }

    }
}
