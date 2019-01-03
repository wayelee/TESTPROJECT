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

using System.Threading;

namespace LibCerMap
{
    public partial class FrmImportNeijiance : OfficeForm
    {
        AxMapControl m_pMapCtl;
        public string XMLPath;
        public string gdbPath;
        public string symFilePath;
        AxTOCControl m_pTOCCtl;
        string MapTempPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate";
        public FrmImportNeijiance(AxMapControl mcontrol, AxTOCControl mtoccontrol)
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
        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            //add all point 
            List<IPoint> pointList = new List<IPoint>();
            //add point type
            List<string> typeList = new List<string>();
            //read excel file to creat Field
            string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + this.textBoxX1.Text + ";Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(strCon);
            string sql1 = "select * from [Sheet1$]";
            conn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(sql1, strCon);
            DataSet ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();

            ISpatialReference pSpaReference = new UnknownCoordinateSystemClass();
            pSpaReference.SetDomain(-8000000, 8000000, -800000, 8000000);
            IFeatureClass pFt = CreateShapeFile(ds, this.textBoxX2.Text, pSpaReference);

            int xindex = 18;
            int yindex = 19;
            int zindex = 20;
            if (ds.Tables[0].Columns[19].ColumnName.ToString().Contains("东坐标"))
            {
                xindex++;
                yindex++;
                zindex++;
            }

            if (pFt == null)
            {
                return;
            }
            else 
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++ )
                {
                    //get type
                    string typeTemp = ds.Tables[0].Rows[i][3].ToString();
                    typeTemp = typeTemp.Split(new char[1] { ' ' })[0];
                    //根据XY坐标添加点,edit attribute
                    IsNumberic isNum = new IsNumberic();
                    IPoint pPoint = new PointClass();
                    if (isNum.IsNumber(ds.Tables[0].Rows[i][xindex].ToString()) && isNum.IsNumber(ds.Tables[0].Rows[i][yindex].ToString()))
                    {
                        pPoint.X = System.Convert.ToDouble(ds.Tables[0].Rows[i][xindex].ToString());
                        pPoint.Y = System.Convert.ToDouble(ds.Tables[0].Rows[i][yindex].ToString());
                        pPoint.Z = System.Convert.ToDouble(ds.Tables[0].Rows[i][zindex].ToString());
                        IFeature pFeature = pFt.CreateFeature();
                        pFeature.Shape = pPoint;

                        pFeature.Store();
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            //pFeature.set_Value(pFeature.Fields.FindField(ds.Tables[0].Columns[j].ColumnName), ds.Tables[0].Rows[i][j]);
                            pFeature.set_Value(j+2, ds.Tables[0].Rows[i][j].ToString());
                        }
                        pFeature.set_Value(pFeature.Fields.FindField("Type"), typeTemp);
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
            pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(strInputName))+"_point";

            //create line shape file
            IPointCollection PointCollection = ReadPoint(pFeatureLayer);
            string lineName = strInputPath+ "\\" + System.IO.Path.GetFileNameWithoutExtension(strInputName) + "_line.shp";
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

            //export doc file
            string TemplateFileName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\WordTemplate\内检测报告.doc";
            exportDocFile(TemplateFileName, textBoxX4.Text, lineLengthList);

            //#region 读取SHP文件
            //IWorkspace pWorkspace = processDataCommon.OpenFromShapefile(strInputPath);
            //IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            //IFeatureClass pInputFC = pFeatureWorkspace.OpenFeatureClass(strInputName);
            //#endregion

            //IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            //pFeatureLayer.FeatureClass = pInputFC;
            //pFeatureLayer.Name = strInputName;
            //m_pMapCtl.AddLayer(pFeatureLayer as ILayer, 0);
            //m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

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

                bool hasType = false;
 
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = ds.Tables[0].Columns[i].ColumnName;
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    if (ds.Tables[0].Columns[i].ColumnName.ToString().Contains("记录距离"))
                    {
                        pFieldEdit.Name_2 =  EvConfig.IMUMoveDistanceField;
                        pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    }
                    
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);

                    if (ds.Tables[0].Columns[i].ColumnName.ToString()=="Type")
                    {
                        hasType = true;
                    }
                }

                if (hasType)
                {
                }
                else
                {
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "Type";
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
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
                double sLength = lineList[i].Length;
                lineLengthList.Add(sLength);
                pFeatureBuffer.set_Value(pFeatureBuffer.Fields.FindField("length"), sLength);               
                object featureOID = pFeatureCursor.InsertFeature(pFeatureBuffer);
                //保存实体
                pFeatureCursor.Flush();
                //结束空间编辑
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            }        
          
        }

        private void FrmImportNeijiance_Load(object sender, EventArgs e)
        {
            textBoxX3.Text = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate\内检测.mxd";
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

        private void buttonX2_Click(object sender, EventArgs e)
        {
            string saveFileName = "";
            SaveFileDialog saveDig = new SaveFileDialog();
            saveDig.Filter = "word文档|*.doc";
            if (saveDig.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileName))
                {
                    MessageBox.Show("file already exist!!");
                    return;
                }
                saveFileName = saveDig.FileName;
            }
            textBoxX4.Text = saveFileName;
        }

        private void exportDocFile(string TemplateFileName, string saveFileName,List<double>lineLengthList)
        {
            GC.Collect();
            int[] lineCount = new int[6] { 0, 0, 0, 0, 0, 0 };
            double[] linePercent = new double[6] { 0, 0, 0, 0, 0, 0 };
            string[] lineName = new string[6] { "", "", "", "", "", "" };
            double[] linePercentSum = new double[6] { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < lineLengthList.Count;i++ )
            {
                if (lineLengthList[i] >= 0 && lineLengthList[i] < 2)
                {
                    lineCount[0]++;
                    lineName[0] = "[0,2)";
                }
                else if (lineLengthList[i] >= 2 && lineLengthList[i] < 5)
                {
                    lineCount[1]++;
                    lineName[1] = "[2,5)";
                }
                else if (lineLengthList[i] >= 5 && lineLengthList[i] < 10)
                {
                    lineCount[2]++;
                    lineName[2] = "[5,10)";
                }
                else if (lineLengthList[i] >= 10 && lineLengthList[i] < 11.5)
                {
                    lineCount[3]++;
                    lineName[3] = "[10,11.5)";
                }
                else if (lineLengthList[i] >= 11.5 && lineLengthList[i] < 12.3)
                {
                    lineCount[4]++;
                    lineName[4] = "[11.5,12.3)";
                }
                else if (lineLengthList[i] >= 12.3)
                {
                    lineCount[5]++;
                    lineName[5] = ">=12.3";
                }
            }
            for (int i = 0; i < 6; i++)
            {
                linePercent[i] = Math.Round((double)lineCount[i]*100 / (double)lineLengthList.Count,2);
            }

            linePercentSum[0] = linePercent[0]/100;
            for (int i = 1; i < 6; i++)
            {
                linePercentSum[i] += linePercentSum[i - 1] + linePercent[i]/100;
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
            //!<光标转到书签
             int length = 0;
             for (int bookIndex = 1; bookIndex <= 6; bookIndex++)
             {
                 object BookMarkName = "tb" + bookIndex.ToString();
                 object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark;
                 doc.ActiveWindow.Selection.GoTo(ref what, ref missing, ref missing, ref BookMarkName);//!<定位到书签位置

                 doc.ActiveWindow.Selection.TypeText(lineCount[bookIndex - 1].ToString());//!<在对应书签位置插入内容
                 length = lineCount[bookIndex - 1].ToString().Length;
                 app.ActiveDocument.Bookmarks.get_Item(ref BookMarkName).End = app.ActiveDocument.Bookmarks.get_Item(ref BookMarkName).End + length;
             }
             for (int bookIndex = 7; bookIndex <= 12; bookIndex++)
             {
                 object BookMarkName = "tb" + bookIndex.ToString();
                 object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark;
                 doc.ActiveWindow.Selection.GoTo(ref what, ref missing, ref missing, ref BookMarkName);//!<定位到书签位置

                 doc.ActiveWindow.Selection.TypeText(linePercent[bookIndex - 7].ToString()+"%");//!<在对应书签位置插入内容
                 length = linePercent[bookIndex - 7].ToString().Length;
                 app.ActiveDocument.Bookmarks.get_Item(ref BookMarkName).End = app.ActiveDocument.Bookmarks.get_Item(ref BookMarkName).End + length;
             }

             //生成统计图1
             DataTable attributeTable = new DataTable("数量");
             DataColumn pDataColumn = new DataColumn("管节长度");
             pDataColumn.Unique = true;
             pDataColumn.DataType = System.Type.GetType("System.String");
             attributeTable.Columns.Add(pDataColumn);
             pDataColumn = null;
             pDataColumn = new DataColumn("统计值");
             pDataColumn.Unique = false;
             pDataColumn.DataType = System.Type.GetType("System.Double");
             attributeTable.Columns.Add(pDataColumn);
             pDataColumn = null;

             string jpgpath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\neijiance.jpg";
             createStatChart(attributeTable, lineCount, linePercent, lineName, jpgpath);

             object BookMarkNameimg = "tb13";
             object whatimg = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark;
             doc.ActiveWindow.Selection.GoTo(ref whatimg, ref missing, ref missing, ref BookMarkNameimg);//!<定位到书签位置
             object Anchor = app.Selection.Range;
             object LinkToFile = false;
             object SaveWithDocument = true;
             Microsoft.Office.Interop.Word.InlineShape shape = app.ActiveDocument.InlineShapes.AddPicture(jpgpath, ref LinkToFile, ref SaveWithDocument, ref Anchor);
             //shape.Width = 100f;//图片宽度
             //shape.Height = 20f;//图片高度

             //生成统计图2
             DataTable attributeTable2 = new DataTable("累计比例");
             DataColumn pDataColumn2 = new DataColumn("管节长度");
             pDataColumn2.Unique = true;
             pDataColumn2.DataType = System.Type.GetType("System.String");
             attributeTable2.Columns.Add(pDataColumn2);
             pDataColumn2 = null;
             pDataColumn2 = new DataColumn("累计值");
             pDataColumn2.Unique = false;
             pDataColumn2.DataType = System.Type.GetType("System.Double");
             attributeTable2.Columns.Add(pDataColumn2);
             pDataColumn2 = null;

             string jpgpath2 = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\BMP\neijiance2.jpg";
             createStatChart2(attributeTable2, linePercentSum, linePercent, lineName, jpgpath2);

             object BookMarkNameimg2 = "tb14";
             object whatimg2 = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark;
             doc.ActiveWindow.Selection.GoTo(ref whatimg2, ref missing, ref missing, ref BookMarkNameimg2);//!<定位到书签位置
             object Anchor2 = app.Selection.Range;
             object LinkToFile2 = false;
             object SaveWithDocument2 = true;
             Microsoft.Office.Interop.Word.InlineShape shape2 = app.ActiveDocument.InlineShapes.AddPicture(jpgpath2, ref LinkToFile2, ref SaveWithDocument2, ref Anchor2);
             //shape.Width = 100f;//图片宽度
             //shape.Height = 20f;//图片高度

             //!<输出完毕后关闭doc对象
             object IsSave = true;
             doc.Close(ref IsSave, ref missing, ref missing);

             MessageBox.Show("生成“" + saveFileName + "”成功!");

        }

        private void createStatChart(DataTable attributeTable, int[] lineCount, double[] linePercent,string[] lineNames, string jpgpath)
        {
            string DataName = "数量";
            string pDataStats = "管节长度";

             DataRow pDataRow;
             for (int i = 0; i < 6; i++)
             {
                 pDataRow = attributeTable.NewRow();

                 pDataRow[1] = lineCount[i];
                 pDataRow[0] = lineNames[i];

                 attributeTable.Rows.Add(pDataRow);
                 pDataRow = null;
             }

            chart1.ChartAreas["ChartArea1"].AxisX.Title = attributeTable.Columns[0].ColumnName;
            chart1.ChartAreas["ChartArea1"].AxisY.Title = attributeTable.Columns[1].ColumnName;
            Series series = chart1.Series.Add(DataName);

            InitChartStyle(DataName);
            chart1.Series[DataName].Points.DataBind(attributeTable.DefaultView, pDataStats, "统计值", null);

            //create jpg
            chart1.SaveImage(jpgpath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void createStatChart2(DataTable attributeTable, double[] linePercentSum, double[] linePercent, string[] lineNames, string jpgpath)
        {
            string DataName = "累计比例";
            string pDataStats = "管节长度";

            DataRow pDataRow;
            for (int i = 0; i < 6; i++)
            {
                pDataRow = attributeTable.NewRow();

                pDataRow[1] = linePercentSum[i];
                pDataRow[0] = lineNames[i];

                attributeTable.Rows.Add(pDataRow);
                pDataRow = null;
            }

            chart2.ChartAreas["ChartArea1"].AxisX.Title = attributeTable.Columns[0].ColumnName;
            chart2.ChartAreas["ChartArea1"].AxisY.Title = attributeTable.Columns[1].ColumnName;
            Series series = chart2.Series.Add(DataName);

            InitChartStyle2(DataName);
            chart2.Series[DataName].Points.DataBind(attributeTable.DefaultView, pDataStats, "累计值", null);

            //create jpg
            chart2.SaveImage(jpgpath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public void InitChartStyle(string DataName)
        {

            chart1.Series[DataName].BorderWidth = 2;                    //线条宽度
            chart1.Series[DataName].ShadowOffset = 1;                   //阴影宽度
            chart1.Series[DataName].IsVisibleInLegend = true;           //是否显示数据说明
            chart1.Series[DataName].IsValueShownAsLabel = true;
            chart1.Series[DataName].Label = "#PERCENT{P1}";//百分比 

            //作图区的显示属性设置
            chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

            //背景色设置
            chart1.ChartAreas["ChartArea1"].ShadowColor = Color.Transparent;
            chart1.ChartAreas["ChartArea1"].BackColor = Color.Azure;         //该处设置为了由天蓝到白色的逐渐变化
            chart1.ChartAreas["ChartArea1"].BackGradientStyle = GradientStyle.TopBottom;
            chart1.ChartAreas["ChartArea1"].BackSecondaryColor = Color.White;
            //X,Y坐标线颜色和大小
            chart1.ChartAreas["ChartArea1"].AxisX.LineColor = Color.Blue;
            chart1.ChartAreas["ChartArea1"].AxisY.LineColor = Color.Blue;
            chart1.ChartAreas["ChartArea1"].AxisX.LineWidth = 2;
            chart1.ChartAreas["ChartArea1"].AxisY.LineWidth = 2;

            //中间X,Y线条的颜色设置
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Blue;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Blue;

            //坐标轴和数据点标注字体
            System.Drawing.Font font1 = new System.Drawing.Font("微软雅黑", 10, FontStyle.Regular);
            //X Y 轴字体
            chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = font1;
            chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = font1;
            //数据点字体
            chart1.Series[DataName].Font = font1;

            //图表标题字体
            System.Drawing.Font font2 = new System.Drawing.Font("微软雅黑", 20, FontStyle.Regular);
            //添加标题
            //Title title = new Title(DataName, Docking.Top, font2, Color.Black);
            Title title = new Title("内检测统计结果", Docking.Top, font2, Color.Black);
            chart1.Titles.Add(title);
        }

        public void InitChartStyle2(string DataName)
        {

            chart2.Series[DataName].BorderWidth = 2;                    //线条宽度
            chart2.Series[DataName].ShadowOffset = 1;                   //阴影宽度
            chart2.Series[DataName].IsVisibleInLegend = true;           //是否显示数据说明
            chart2.Series[DataName].IsValueShownAsLabel = true;
            chart2.Series[DataName].ChartType = SeriesChartType.FastLine;

            //作图区的显示属性设置
            chart2.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

            //背景色设置
            chart2.ChartAreas["ChartArea1"].ShadowColor = Color.Transparent;
            chart2.ChartAreas["ChartArea1"].BackColor = Color.Azure;         //该处设置为了由天蓝到白色的逐渐变化
            chart2.ChartAreas["ChartArea1"].BackGradientStyle = GradientStyle.TopBottom;
            chart2.ChartAreas["ChartArea1"].BackSecondaryColor = Color.White;
            //X,Y坐标线颜色和大小
            chart2.ChartAreas["ChartArea1"].AxisX.LineColor = Color.Blue;
            chart2.ChartAreas["ChartArea1"].AxisY.LineColor = Color.Blue;
            chart2.ChartAreas["ChartArea1"].AxisX.LineWidth = 2;
            chart2.ChartAreas["ChartArea1"].AxisY.LineWidth = 2;

            //中间X,Y线条的颜色设置
            chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Blue;
            chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Blue;

            //坐标轴和数据点标注字体
            System.Drawing.Font font1 = new System.Drawing.Font("微软雅黑", 10, FontStyle.Regular);
            //X Y 轴字体
            chart2.ChartAreas["ChartArea1"].AxisX.TitleFont = font1;
            chart2.ChartAreas["ChartArea1"].AxisY.TitleFont = font1;
            //数据点字体
            chart2.Series[DataName].Font = font1;

            chart2.ChartAreas["ChartArea1"].AxisY.Maximum = 1.1;

            //图表标题字体
            System.Drawing.Font font2 = new System.Drawing.Font("微软雅黑", 20, FontStyle.Regular);
            //添加标题
            //Title title = new Title(DataName, Docking.Top, font2, Color.Black);
            Title title = new Title("内检测统计管节累计结果", Docking.Top, font2, Color.Black);
            chart2.Titles.Add(title);
        }
    }
}
