using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using stdole;

using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;

using DevComponents.DotNetBar;
using System.IO;

namespace LibCerMap
{
    public partial class FrmCreatParkPoint : OfficeForm
    {
        AxMapControl m_pMapCtl;
        public string XMLPath;
        public string gdbPath;
        public string symFilePath;//用于记录符号文件路径
        AxTOCControl m_pTOCCtl;
        string MapTempPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate";
        public FrmCreatParkPoint(AxMapControl mcontrol,AxTOCControl mtoccontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pMapCtl = mcontrol;
            m_pTOCCtl = mtoccontrol;
        }

        private void btnOpenXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML文件(*.xml)|*.xml;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLPath = dlg.FileName;
                textBoxX1.Text = XMLPath;
               // textBoxX3.Text = System.IO.Path.GetFileNameWithoutExtension(XMLPath);     //文件名
                textBoxX3.Text = "zhouqi";
                try
                {
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspace pWorkspace = workspaceFactory.OpenFromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(XMLPath), textBoxX3.Text + ".gdb"), 0);
                    textBoxX2.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(XMLPath), textBoxX3.Text + ".gdb");
                    gdbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(XMLPath), textBoxX3.Text + ".gdb");

                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (System.IO.Directory.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(XMLPath), textBoxX3.Text + ".gdb")) == true)
                    {
                        MessageBox.Show("GDB同名文件夹已存在，请指定GDB", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspaceName workspaceName = workspaceFactory.Create(System.IO.Path.GetDirectoryName(XMLPath), textBoxX3.Text, null, 0);
                    gdbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(XMLPath), textBoxX3.Text + ".gdb");
                    textBoxX2.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(XMLPath), textBoxX3.Text + ".gdb");
                    return;
                }

            }
        }

        private void btnOpenGDB_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                gdbPath = fbd.SelectedPath;
                textBoxX2.Text = gdbPath;
            }
        }

        private void btnNewGDB_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Title = "新建File Geodatabase";
                fbd.InitialDirectory = System.IO.Path.GetDirectoryName(XMLPath);
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(fbd.FileName + ".gdb") == true)
                    {
                        MessageBox.Show("GDB已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspaceName workspaceName = workspaceFactory.Create(System.IO.Path.GetDirectoryName(fbd.FileName), System.IO.Path.GetFileName(fbd.FileName), null, 0);
                    gdbPath = fbd.FileName + ".gdb";
                    textBoxX2.Text = fbd.FileName + ".gdb";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            
            CParkPoint xml = new CParkPoint();
            bool result = xml.ReadParkXML(XMLPath);
            Regex regNum = new Regex("^[0-9]");
            if (regNum.IsMatch(textBoxX3.Text) == true)
            {
                MessageBox.Show("数据集名称不能以数字开头命名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (result == true)
            {
                try
                {
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    gdbPath = textBoxX2.Text;
                    IFeatureWorkspace pFeatureWorkspace = workspaceFactory.OpenFromFile(gdbPath, 0) as IFeatureWorkspace;
                    ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();

                    //ISpatialReference pSpatialReference = new UnknownCoordinateSystemClass();
                    ISpatialReference pSpatialReference = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
                    pSpatialReference.SetDomain(-8000000, 8000000, -800000, 8000000);
                    IFeatureDataset pFeatureDataset = pFeatureWorkspace.CreateFeatureDataset(textBoxX3.Text, pSpatialReference);
                    IFeatureClass pFeatureclass = CreatePointFeatureClass(pFeatureDataset, xml);
                    IFeatureClass pFCLine = CreatLineFC(pFeatureDataset, pFeatureclass);
                    if (pFeatureclass == null || pFCLine == null)
                    {
                        MessageBox.Show("数据生成失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    IFeatureLayer pFPointLayer = new FeatureLayerClass();
                    pFPointLayer.FeatureClass = pFeatureclass;
                    pFPointLayer.Name = textBoxX3.Text + "_parkpoint";

                    IFeatureLayer pFLintLayer = new FeatureLayerClass();
                    pFLintLayer.FeatureClass = pFCLine;
                    pFLintLayer.Name = textBoxX3.Text + "_parkline";

                    m_pMapCtl.AddLayer(pFLintLayer as ILayer, 0);
                    m_pMapCtl.AddLayer(pFPointLayer as ILayer, 0);
                    m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                    if (cmbImpSymbol.Text != null && symFilePath != null)
                    {
                        ImpSymbolFromFile(symFilePath, pFPointLayer, pFLintLayer);
                    }
                    
                    //MessageBox.Show("数据已经生成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                MessageBox.Show("XML文件解析失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        public IFeatureClass CreatePointFeatureClass(IFeatureDataset pFeatureDataset, CParkPoint xml)
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
                pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint; //点、线、面
                pGeometryDefEdit.SpatialReference_2 = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);

                //新建字段
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "SzID";             //代号
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Changed";             //停泊点类型是否改变
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Symbol";             //停泊点类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "BegineTime";             //到达停泊点时间
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "LeaveTime";             //离开停泊点时间
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "太阳方位角";             //太阳方位角
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "地球方位角";             //地球方位角
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "WorkModeNum";             //离开停泊点时间
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                for (int i = 0; i <= 7;i++ )
                {
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "Mode_" + i.ToString()+"_symbol";             //第i工作模式名称
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);

                    //pField = new FieldClass();
                    //pFieldEdit = (IFieldEdit)pField;
                    //pFieldEdit.Name_2 = "Mode_" + i.ToString() + "_Name";             //第i工作模式名称
                    //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    //pFieldEdit.IsNullable_2 = true;
                    //pFieldsEdit.AddField(pField);

                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "Mode_" + i.ToString() + "_Slice";             //第i工作模式固有时长
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);

                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "Mode_" + i.ToString() + "_AssSlice";             //第i工作模式分配时长
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);

                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "Mode_" + i.ToString() + "_BeginTime";             //第i工作模式开始时刻
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);

                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "Mode_" + i.ToString() + "_EndTime";             //第i工作模式结束时刻
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);

                    //pField = new FieldClass();
                    //pFieldEdit = (IFieldEdit)pField;
                    //pFieldEdit.Name_2 = "Mode_" + i.ToString() + "_Energy";             //第i工作模式结束能量
                    //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    //pFieldEdit.IsNullable_2 = true;
                    //pFieldsEdit.AddField(pField);
                }

                IFeatureClass pFeatureClass = pFeatureDataset.CreateFeatureClass(textBoxX3.Text + "_parkpoint", pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                for (int j = 0; j < xml.uiParkNum;j++ )
                {
                    IPoint pPoint = new PointClass();
                    pPoint.X = xml.ParkSList[j].stCoord.dx;
                    pPoint.Y = xml.ParkSList[j].stCoord.dy;
                    IFeature pFeature = pFeatureClass.CreateFeature();
                    pFeature.Shape = pPoint;
                    pFeature.Store();
                    AddAttributeTable(pFeature, xml.ParkSList[j],j);
                }
                return pFeatureClass;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        //创建线图层
        public IFeatureClass CreatLineFC(IFeatureDataset pFeatureDataset, IFeatureClass pPointFClass)
        {
            try
            {
                //建立shape字段
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "SHAPE";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //设置Geometry definition
                IGeometryDef pGeometryDef = new GeometryDefClass();
                IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pGeometryDef;
                pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline; //点、线、面
                pGeometryDefEdit.SpatialReference_2 = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);

                //新建字段
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Length";             //线类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                IFeatureClass pFeatureClass = pFeatureDataset.CreateFeatureClass(textBoxX3.Text + "_GmpLine", pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");

                FrmPointToLine frmP2L = new FrmPointToLine(m_pMapCtl.Object as IMapControl3);
                IFeatureLayer PFLayer = new FeatureLayerClass();
                PFLayer.FeatureClass = pPointFClass;
                IPointCollection pPointColl = frmP2L.ReadPoint(PFLayer);
                List<IPolyline> LineList = CreatePolyline(pPointColl);

                for (int i = 0; i < LineList.Count; i++)
                {
                    if (LineList[i].Length > 0)
                    {
                        IFeature pFeature = pFeatureClass.CreateFeature();
                        pFeature.Shape = LineList[i];
                        string sLength = ((int)(LineList[i].Length * 100.0) / 100.0).ToString() + "m";
                        pFeature.set_Value(pFeature.Fields.FindField("Length"), sLength);
                        pFeature.Store();
                    }
                }
                return pFeatureClass;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private List<IPolyline> CreatePolyline(IPointCollection pPointcollection)
        {
            List<IPolyline> lineList = new List<IPolyline>();
            int PointNumber = int.Parse(pPointcollection.PointCount.ToString());

            object o = Type.Missing;

            IPolyline pPolyline = new PolylineClass();
            for (int i = 0; i < PointNumber - 1; i++)
            {
                ILine pLine = new LineClass();
                pLine.PutCoords(pPointcollection.get_Point(i), pPointcollection.get_Point(i + 1));
                ISegmentCollection pSC = new PolylineClass();
                pSC.AddSegment(pLine as ISegment, ref o, ref o);
                pPolyline = pSC as IPolyline;
                lineList.Add(pPolyline);
            }
            return lineList;
        }

        public void AddAttributeTable(IFeature pFeature, ParkSite pPathPoint,int j)
        {
            try
            {
                pFeature.set_Value(pFeature.Fields.FindField("SzID"), pPathPoint.szID);
                pFeature.set_Value(pFeature.Fields.FindField("Changed"), pPathPoint.szSymbolChanged);
                pFeature.set_Value(pFeature.Fields.FindField("Symbol"), pPathPoint.szSymbol);
                pFeature.set_Value(pFeature.Fields.FindField("BegineTime"), "到达时间:" + pPathPoint.szBeginTime);
                pFeature.set_Value(pFeature.Fields.FindField("LeaveTime"), "离开时间:" + pPathPoint.szLeaveTime);
                pFeature.set_Value(pFeature.Fields.FindField("WorkModeNum"), pPathPoint.uiWorkModeNum);
                for (int i = 0; i <= 7; i++)
                {
                    
                    pFeature.set_Value(pFeature.Fields.FindField("Mode_" + i.ToString() + "_symbol"), pPathPoint.ST_WorkModes[i].szSymbol);
                    pFeature.set_Value(pFeature.Fields.FindField("Mode_" + i.ToString() + "_Slice"), pPathPoint.ST_WorkModes[i].dSlice);
                    pFeature.set_Value(pFeature.Fields.FindField("Mode_" + i.ToString() + "_AssSlice"), pPathPoint.ST_WorkModes[i].dAssignSlice);
                    pFeature.set_Value(pFeature.Fields.FindField("Mode_" + i.ToString() + "_BeginTime"), pPathPoint.ST_WorkModes[i].szBeginTime);
                    pFeature.set_Value(pFeature.Fields.FindField("Mode_" + i.ToString() + "_EndTime"), pPathPoint.ST_WorkModes[i].szEndTime);
                }
                pFeature.Store();
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

        private void btnImpSymbol_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgMapSymbol = new OpenFileDialog();
            dlgMapSymbol.Title = "选择周期规划模板地图";
            dlgMapSymbol.InitialDirectory = ".";
            dlgMapSymbol.Filter = "mxd文件(*.mxd)|*.mxd";
            dlgMapSymbol.RestoreDirectory = true;
            if (dlgMapSymbol.ShowDialog() == DialogResult.OK)
            {
                symFilePath = dlgMapSymbol.FileName;
                cmbImpSymbol.Items.Add(dlgMapSymbol.FileName);
                cmbImpSymbol.Text = dlgMapSymbol.FileName;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 对生成的周期规划各图层进行符号、标注设置
        /// </summary>
        /// <param name="impfilepath">导入符号mxd文件路径</param>
        /// <param name="passpoint">生成的周期规划点图层</param>
        /// <param name="passline">生成的周期规划线图层</param>
        public void ImpSymbolFromFile(string impfilepath, ILayer passpoint, ILayer passline)
        {
            IMapDocument pMapDocument = new MapDocumentClass();
            pMapDocument.Open(impfilepath, null);
            IMap pMap = pMapDocument.Map[0];

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                ILayer pLayerSymbol = pMap.get_Layer(i);
                if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_parkpoint"))
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
                else if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_parkline"))
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

        private void FrmCreatParkPoint_Load(object sender, EventArgs e)
        {
            LoadMxd(this.cmbImpSymbol, MapTempPath);
        }

        public void LoadMxd(DevComponents.DotNetBar.Controls.ComboBoxEx cmbbox, string FileDir)
        {
            DirectoryInfo dri = new DirectoryInfo(FileDir);
            foreach (FileInfo NextFile in dri.GetFiles())
            {
                if (NextFile.Extension.ToUpper().Equals(".MXD"))
                {
                    cmbbox.Items.Add(NextFile.Name);
                }
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(cmbImpSymbol.Text))
            {
                symFilePath = cmbImpSymbol.Text;
            }
            else
            {
                symFilePath = MapTempPath + "\\" + cmbImpSymbol.Text;
            }
        }
    }
}
