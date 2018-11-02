using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using stdole;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Text.RegularExpressions;
using DevComponents.DotNetBar;
using System.IO;

namespace LibCerMap
{
    public partial class FrmFromXML : OfficeForm
    {
        string MapTempPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate";
        public FrmFromXML()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public ESRI.ArcGIS.Controls.AxMapControl m_pMapCtl;
        public ESRI.ArcGIS.Controls.AxTOCControl m_pTOCCtl;
        public string XMLPath;
        public string gdbPath;
        public double angle;
        public double angleOrign;
        public double distance;
        public string symFilePath;//用于记录符号文件路径
        private void buttonX3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML文件(*.xml)|*.xml;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLPath = dlg.FileName;
                textBoxX1.Text = XMLPath;
               // textBoxX3.Text = System.IO.Path.GetFileNameWithoutExtension(XMLPath);     //文件名
                textBoxX3.Text = "danyuan";
                //TextLine.Text = System.IO.Path.GetFileNameWithoutExtension(XMLPath) + "_polylinelabel";
                TextLine.Text = "danyuan" + "_polylinelabel";
                //textpolygon.Text = System.IO.Path.GetFileNameWithoutExtension(XMLPath) + "_polygonlabel";
                textpolygon.Text = "danyuan" + "_polygonlabel";
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                gdbPath = fbd.SelectedPath;
                textBoxX2.Text = gdbPath;
            }
        }

        private void buttonX_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void AddAttributeTable(IFeature pFeature,PathPoint pPathPoint,double angle)
        {
            try
            {
                pFeature.set_Value(pFeature.Fields.FindField("Angle"), angle);
                pFeature.set_Value(pFeature.Fields.FindField("szWorkMode"), pPathPoint.szWorkMode);
                pFeature.set_Value(pFeature.Fields.FindField("szMove"), pPathPoint.szMove);
                pFeature.set_Value(pFeature.Fields.FindField("dCurv"), pPathPoint.dCurv);
                pFeature.set_Value(pFeature.Fields.FindField("dMileage"), pPathPoint.dMileage);
                pFeature.set_Value(pFeature.Fields.FindField("dAngle"), pPathPoint.dAngle);
                pFeature.set_Value(pFeature.Fields.FindField("szSign"), pPathPoint.szSign);
                pFeature.set_Value(pFeature.Fields.FindField("uiPathPointNum"), pPathPoint.uiPathPointNum);
                for (int i = 0; i < 13; i++)
                {
                    pFeature.set_Value(pFeature.Fields.FindField("dx_" + i.ToString()), pPathPoint.stPathPoints[i].dx);
                    pFeature.set_Value(pFeature.Fields.FindField("dy_" + i.ToString()), pPathPoint.stPathPoints[i].dy);
                }
                pFeature.Store();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        public bool CreatePolylineArrow(double angle, IPoint pPoint, double length, double theta, ref ISegmentCollection pSegmentCollection)
        {
            // CreatePolylineArrow(angle, pToPoint, 0.2 * distance, theta, ref pSegmentCollection);  //创建小箭头
            //左边箭头小短线终点旋转后坐标
            double X1 = -1 * length * Math.Sin(theta); //该点在旋转后坐标系中的X坐标
            double Y1 = -1 * length * Math.Cos(theta); //该点在旋转后坐标系中的Y坐标
            //右边箭头小短线终点旋转后坐标
            double X2 = length * Math.Sin(theta); //该点在旋转后坐标系中的X坐标
            double Y2 = -1 * length * Math.Cos(theta); //该点在旋转后坐标系中的Y坐标
            //旋转坐标系原点在原始坐标系中的坐标
            double a = pPoint.X; //旋转后坐标系原点在原坐标系的位置X
            double b = pPoint.Y; //旋转后坐标系原点在原坐标系的位置Y
            //左边箭头小短线终点原始坐标
            angle = -1 * angle;
            double X11 = X1 * Math.Cos(angle) - Y1 * Math.Sin(angle) + a; //该点在原始坐标系中的X坐标
            double Y11 = X1 * Math.Sin(angle) + Y1 * Math.Cos(angle) + b; //该点在原始坐标系中的Y坐标
            //左边箭头小短线
            ILine plline = new LineClass();
            IPoint plPoint = new PointClass();
            plPoint.X = X11;
            plPoint.Y = Y11;
            plline.FromPoint = pPoint;
            plline.ToPoint = plPoint;
            ILine mline = new LineClass();
            mline.FromPoint = plPoint;
            mline.ToPoint = pPoint;
            //右边箭头小短线终点原始坐标
            double X22 = X2 * Math.Cos(angle) - Y2 * Math.Sin(angle) + a; //该点在原始坐标系中的X坐标
            double Y22 = X2 * Math.Sin(angle) + Y2 * Math.Cos(angle) + b; //该点在原始坐标系中的Y坐标
            //右边箭头小短线
            ILine prline = new LineClass();
            prline.FromPoint = pPoint;
            IPoint prPoint = new PointClass();
            prPoint.X = X22;
            prPoint.Y = Y22;
            prline.ToPoint = prPoint;
            pSegmentCollection.AddSegment(plline as ISegment);
            pSegmentCollection.AddSegment(mline as ISegment);
            pSegmentCollection.AddSegment(prline as ISegment);
            return true;

        }

        //绘制航向，输入：点文件，初始航向
        public ILayer CreatePolylineLabelFeatureClass(IFeatureClass pFeatureclass,double dAngleOrg, double dLength)
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
                pGeometryDefEdit.SpatialReference_2 = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
                //pGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);
                //新建字段
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "OID";             //原始OBJECTID
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Name";             //类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "OriginalFeatureClass"; //原始图层名称
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Angle";             //航向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dCurv";             //曲率
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "szWorkMode";             //行走控制模式
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "szMove";    //前进标示
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dAngle";             //原地转向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Type";             //备用字段
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                IFeatureClass pFeatureClass = null;
                try
                {
                    pFeatureClass = pFeatureclass.FeatureDataset.CreateFeatureClass(TextLine.Text, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                }
                catch (SystemException eee)
                {
                    if (eee.Message == "The table already exists.")
                    {
                        ClsGDBDataCommon cls = new ClsGDBDataCommon();
                        IWorkspace ws = pFeatureclass.FeatureDataset.Workspace;
                        IFeatureWorkspace fws = ws as IFeatureWorkspace;
                        pFeatureClass = fws.OpenFeatureClass(TextLine.Text);
                        ITable ptable = pFeatureClass as ITable;
                        ptable.DeleteSearchedRows(null);
                    }
                    //MessageBox.Show(eee.Message);
                }
                if (double.TryParse(textlength.Text, out distance) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }

               
  
                IQueryFilter pQF = new QueryFilterClass(); 
                IFeatureCursor pFC = pFeatureclass.Search(null, false);
                IFeature pF = pFC.NextFeature();

                //////////////////////////////////////////////////////////////////////
                //首先绘制起始航向
                IPoint pointStart = pF.Shape as IPoint;
                IPoint pointEnd = new PointClass();
                pointEnd.X = pointStart.X + distance * Math.Sin(dAngleOrg) * 1.2;
                pointEnd.Y = pointStart.Y + distance * Math.Cos(dAngleOrg) * 1.2;
                ILine pL = new LineClass();
                pL.FromPoint = pointStart;
                pL.ToPoint = pointEnd;
                ISegment pSeg = pL as ISegment;
                ISegmentCollection pSC = new PolylineClass();
                pSC.AddSegment(pSeg);
                CreatePolylineArrow(dAngleOrg, pointEnd, 0.2 * distance, Math.PI / 6, ref pSC);  //创建小箭头
                IFeature pFTemp = pFeatureClass.CreateFeature();
                pFTemp.Shape = pSC as IGeometry;

                pFTemp.set_Value(pFTemp.Fields.FindField("OriginalFeatureClass"), textBoxX3.Text + "_passpoint");
                pFTemp.Store();

                ///////////////////////////////////////////////////

                while (pF != null)
                {
                    double angle1 = 0.0;
                    string szWorkMode;
                    
                    int OID = 0;
                    OID = int.Parse(pF.get_Value(pF.Fields.FindField("OBJECTID")).ToString());
                    if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("Angle")).ToString()) == false)
                    {
                        angle1 = double.Parse(pF.get_Value(pF.Fields.FindField("Angle")).ToString());
                        
                        if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("szWorkMode")).ToString()) == false)
                        {
                            szWorkMode = pF.get_Value(pF.Fields.FindField("szWorkMode")).ToString();
                            if (szWorkMode == "MZY" || szWorkMode == "YDY")//盲走模式，原地转向时首先也要画出起始航向
                            {
                                IPoint pPoint = pF.Shape as IPoint;
                                IPoint pToPoint = new PointClass();
                                pToPoint.X = pPoint.X + distance * Math.Sin(angle1) * 1.2;
                                pToPoint.Y = pPoint.Y + distance * Math.Cos(angle1) * 1.2;
                                ILine pline = new LineClass();
                                pline.FromPoint = pPoint;
                                pline.ToPoint = pToPoint;
                                ISegment pSegment = pline as ISegment;
                                ISegmentCollection pSegmentCollection = new PolylineClass();
                                pSegmentCollection.AddSegment(pSegment);
                                CreatePolylineArrow(angle1, pToPoint, 0.2 * distance, Math.PI / 6, ref pSegmentCollection);  //创建小箭头
                                IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                                pFeatureTemp.Shape = pSegmentCollection as IGeometry;
                                string szMove = "";
                                if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("szMove")).ToString()) == false)
                                {
                                    szMove = pF.get_Value(pF.Fields.FindField("szMove")).ToString();
                                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szMove"), szMove);
                                }
                                if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("dCurv")).ToString()) == false)
                                {
                                    double dCurv = double.Parse(pF.get_Value(pF.Fields.FindField("dCurv")).ToString());
                                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("dCurv"), dCurv );
                                }

                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OID"), OID);                             
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), (angle1 * 180/Math.PI)%360);                                
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), "MZY");
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), textBoxX3.Text + "_passpoint");
                                pFeatureTemp.Store();
                            }
                            if (szWorkMode == "YDY")//原地转向，绘制转身角度
                            {
                                IPoint pPoint = pF.Shape as IPoint;
                                IPoint pToPoint = new PointClass();
                                pToPoint.X = pPoint.X + distance * Math.Sin(angle1);
                                pToPoint.Y = pPoint.Y + distance * Math.Cos(angle1);
                                double dAngle = 0;
                                string szSign = "11H";
                                bool isCCW = true;
                                double toAngle = 0; //旋转终点航向
                                if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("dAngle")).ToString()) == false)
                                {
                                    dAngle = double.Parse(pF.get_Value(pF.Fields.FindField("dAngle")).ToString());
                                }
                                if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("szSign")).ToString()) == false)
                                {
                                    szSign = pF.get_Value(pF.Fields.FindField("szSign")).ToString();
                                }
                                if (szSign == "00H")
                                {
                                    isCCW = true;
                                    toAngle = angle1 - dAngle * Math.PI / 180 - Math.PI / 2;
                                }
                                else if (szSign == "11H")
                                {
                                    isCCW = false;
                                    toAngle = angle1 + dAngle * Math.PI / 180 + Math.PI / 2;
                                }
                                IConstructCircularArc constructCircularArc = new CircularArcClass();
                                ICircularArc circularArc = constructCircularArc as ICircularArc;
                                constructCircularArc.ConstructArcDistance(pPoint, pToPoint, isCCW, distance * dAngle * Math.PI / 180);
                                ISegment pSegment = circularArc as ISegment;
                                ISegmentCollection pSegmentCollection = new PolylineClass();
                                pSegmentCollection.AddSegment(pSegment);
  
                                CreatePolylineArrow(toAngle, circularArc.ToPoint, 0.2 * distance, Math.PI / 6, ref pSegmentCollection);  //创建小箭头
                                IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                                pFeatureTemp.Shape = pSegmentCollection as IGeometry;
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OID"), OID);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), dAngle%360);//将转动角度记入字段方便标注
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("dAngle"), dAngle);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), textBoxX3.Text + "_passpoint");
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), "YDY");
                                
                                pFeatureTemp.Store();
                            }
                            else if (szWorkMode == "JTY" || szWorkMode == "ZAY"
                            || szWorkMode == "ZBY" || szWorkMode == "ZCY")
                            {
                                int num = int.Parse(pF.get_Value(pF.Fields.FindField("uiPathPointNum")).ToString());
                                IPoint pPoint = pF.Shape as IPoint;
                                IPoint pToPoint = new PointClass();
                                double[,] XY = new double[num, 2];

                                for (int i = 0; i < num; i++)
                                {
                                    XY[i, 0] = double.Parse(pF.get_Value(pF.Fields.FindField("dx_" + i.ToString())).ToString());
                                    XY[i, 1] = double.Parse(pF.get_Value(pF.Fields.FindField("dy_" + i.ToString())).ToString());
                                    double angleTemp = Math.Atan((XY[i, 1] - pPoint.Y) / (XY[i, 0] - pPoint.X));
                                    pToPoint.X = pPoint.X + 1.2 * distance * Math.Cos(angleTemp); ;
                                    pToPoint.Y = pPoint.Y + 1.2 * distance * Math.Sin(angleTemp);
                                    ILine pline = new LineClass();
                                    pline.FromPoint = pPoint;
                                    pline.ToPoint = pToPoint;
                                    ISegment pSegment = pline as ISegment;
                                    ISegmentCollection pSegmentCollection = new PolylineClass();
                                    pSegmentCollection.AddSegment(pSegment);
                                    CreatePolylineArrow(Math.PI / 2 - angleTemp, pToPoint, 0.2 * distance, Math.PI / 6, ref pSegmentCollection);  //创建小箭头
                                    IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                                    pFeatureTemp.Shape = pSegmentCollection as IGeometry;
                                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OID"), OID + i);
                                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), szWorkMode);
                                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), textBoxX3.Text + "_passpoint");
                                    pFeatureTemp.Store();
                                    pPoint.X = XY[i, 0];
                                    pPoint.Y = XY[i, 1];
                                }
                            }
                         }                            
                         else//行走模式为空，只画航向
                        {
                            IPoint pPoint = pF.Shape as IPoint;
                            IPoint pToPoint = new PointClass();
                            pToPoint.X = pPoint.X + distance * Math.Sin(angle1) * 1.2;
                            pToPoint.Y = pPoint.Y + distance * Math.Cos(angle1) * 1.2;
                            ILine pline = new LineClass();
                            pline.FromPoint = pPoint;
                            pline.ToPoint = pToPoint;
                            ISegment pSegment = pline as ISegment;
                            ISegmentCollection pSegmentCollection = new PolylineClass();
                            pSegmentCollection.AddSegment(pSegment);
                            CreatePolylineArrow(angle1, pToPoint, 0.2 * distance, Math.PI / 6, ref pSegmentCollection);  //创建小箭头
                            IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                            pFeatureTemp.Shape = pSegmentCollection as IGeometry;
 
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), (angle *180 / Math.PI)%360);
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), textBoxX3.Text + "_passpoint");
                            pFeatureTemp.Store();
                        }                       
                    }
                    pF = pFC.NextFeature();
                }
                IFeatureLayer ppFeatureLayer = new FeatureLayerClass();
                ppFeatureLayer.FeatureClass = pFeatureClass;
                ppFeatureLayer.Name = TextLine.Text;
                m_pMapCtl.AddLayer(ppFeatureLayer as ILayer,2);
                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                ILayer pPasspointLayer = ppFeatureLayer as ILayer;
                return pPasspointLayer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }
        public void CreatePolygonLabelFeatureClass(IFeatureClass pFeatureclass)
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
                pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon; //点、线、面
                pGeometryDefEdit.SpatialReference_2 = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
                //pGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);
                //新建字段
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "PointID";             //点位PointID
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "X"; //X
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Y";             //Y
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Z";             //Z
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "omg";             //omg
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "phi";             //phi
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "kap";             //kap
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "width";             //width
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "height";             //height
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "fx";             //fx
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "fy";             //fy
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "f";             //f
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
                IFeatureClass pFeatureClass = null;
                try
                {
                    pFeatureClass = pFeatureclass.FeatureDataset.CreateFeatureClass(textpolygon.Text, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                }
                catch (SystemException eee)
                {
                    if (eee.Message == "The table already exists.")
                    {
                        ClsGDBDataCommon cls = new ClsGDBDataCommon();
                        IWorkspace ws = pFeatureclass.FeatureDataset.Workspace;
                        IFeatureWorkspace fws = ws as IFeatureWorkspace;
                        pFeatureClass = fws.OpenFeatureClass(textpolygon.Text);
                        ITable ptable = pFeatureClass as ITable;
                        ptable.DeleteSearchedRows(null);
                    }
                }
                IFeatureLayer ppFeatureLayer = new FeatureLayerClass();
                ppFeatureLayer.FeatureClass = pFeatureClass;
                ppFeatureLayer.Name = textpolygon.Text;
                m_pMapCtl.AddLayer(ppFeatureLayer as ILayer, 3);
                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return ;
            }
 
        }
        public IFeatureClass CreatePointFeatureClass(IFeatureDataset pFeatureDataset, CPathPoint xml, double dAngleOrg/*初始航向*/,ref IFeatureLayer pFeaturelayerOut )
        {
            try
            {
                double angleTemp = angle;
                //建立shape字段
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Shape";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "OBJECTID";
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                //pFieldsEdit.AddField(pField);  
                //设置Geometry definition
                IGeometryDef pGeometryDef = new GeometryDefClass();
                IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pGeometryDef;
                pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint; //点、线、面
                pGeometryDefEdit.SpatialReference_2 = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
                //pGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);
                //新建字段
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Name";             //类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Type";             //类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "StartAngle";      //初始航向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dAngle";             //原地转向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "szSign";             //原地转向角度符号
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Angle";             //航向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                 pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "szWorkMode";             //行走控制模式类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "szMove";             //前进标示
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dCurv";             //路径曲率
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dMileage";             //路径里程
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "ViewAngle";             //视场角
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "ViewOrientation";             //视场方位角
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);
               
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "uiPathPointNum";             //路径点个数
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                for (int i = 0; i < 13; i++)
                {
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "dx_" + i.ToString();             //原地转向角度符号
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "dy_" + i.ToString();             //路径点个数
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.AddField(pField);
                }
                IFeatureClass pFeatureClass = pFeatureDataset.CreateFeatureClass(textBoxX3.Text + "_passpoint", pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                //记录起始点
                IPoint pPoint = new PointClass();
                pPoint.X = xml.stStartPointPos.dx;
                pPoint.Y = xml.stStartPointPos.dy;
                IFeature pFeature = pFeatureClass.CreateFeature();
                pFeature.Shape = pPoint;
                AddAttributeTable(pFeature, xml.PathPointList[0],angleTemp);

                //路径点
                int uiPathPhaseNum = xml.uiPathPhaseNum;
                //创建临时坐标变量
                double X = xml.stStartPointPos.dx;
                double Y = xml.stStartPointPos.dy;
                for (int i = 0; i < uiPathPhaseNum; i++)
                {
                    if (xml.PathPointList[i].szWorkMode == "MZY")
                    {
                        IConstructCircularArc constructCircularArc = new CircularArcClass();
                        ICircularArc circularArc = constructCircularArc as ICircularArc;
                        bool isCCW = true;
                        if (xml.PathPointList[i].szMove == "11H") //前进
                        {
                            if (xml.PathPointList[i].dCurv > 0)
                            {
                                isCCW = false;
                            }
                            else if (xml.PathPointList[i].dCurv < 0)
                            {
                                isCCW = true;
                            }
                        }
                        else if (xml.PathPointList[i].szMove == "00H") //后退
                        {
                            if (xml.PathPointList[i].dCurv < 0)
                            {
                                isCCW = false;
                                angle = angle + Math.PI;
                            }
                            else if (xml.PathPointList[i].dCurv > 0)
                            {
                                isCCW = true;
                                angle = angle + Math.PI;
                            }
                        }
                        IPoint fromPoint = new PointClass();
                        IPoint toPoint = new PointClass();
                        fromPoint.PutCoords(X - 10 * Math.Sin(angle), Y - 10 * Math.Cos(angle));//切线角度 
                        if (xml.PathPointList[i].szMove == "00H")
                        {
                            angle = angle - Math.PI;
                        }
                        toPoint.PutCoords(X, Y);
                        ILine line = new LineClass();
                        line.PutCoords(fromPoint, toPoint);
                        if (xml.PathPointList[i].dCurv!=0)//当盲走曲率不为0时
                        {                                                                                                             
                            constructCircularArc.ConstructTangentRadiusArc(line as ISegment, false, isCCW, Math.Abs(1 / xml.PathPointList[i].dCurv), xml.PathPointList[i].dMileage);
                            X = circularArc.ToPoint.X;
                            Y = circularArc.ToPoint.Y;
                        }
                        else//当盲走曲率为0时
                        {
                            X = X + xml.PathPointList[i].dMileage * Math.Cos(Math.PI / 2 - angle);
                            Y = Y + xml.PathPointList[i].dMileage * Math.Sin(Math.PI / 2 - angle);
                        }

                        if (xml.PathPointList[i].szMove == "11H") //前进
                        {
                            if (xml.PathPointList[i].dCurv > 0)
                            {
                                angle = angle + xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                                angleTemp = angle;
                            }
                            else if (xml.PathPointList[i].dCurv < 0)
                            {
                                angleTemp = angleTemp - xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                                angle = angle - xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                            }
                        }
                        else if (xml.PathPointList[i].szMove == "00H") //后退
                        {
                            if (xml.PathPointList[i].dCurv < 0)
                            {
                                angleTemp = angleTemp + xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                                angle = angle  + xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                            }
                            else if (xml.PathPointList[i].dCurv > 0)
                            {
                                angleTemp = angleTemp - xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                                angle = angle - xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                            }
                        }
                        IPoint pPointTemp = new PointClass();
                        pPointTemp.X = X;
                        pPointTemp.Y = Y;
                        IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                        pFeatureTemp.Shape = pPointTemp;
                        if (i + 1 > uiPathPhaseNum - 1)
                        {
                             pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), angleTemp%360);//最后一点的终点航向记录
                             pFeatureTemp.Store();
                        }
                        else
                        {
                            AddAttributeTable(pFeatureTemp, xml.PathPointList[i + 1],angleTemp);
                        }
                    }
                    if (xml.PathPointList[i].szWorkMode == "JTY" || xml.PathPointList[i].szWorkMode == "ZAY"
                        || xml.PathPointList[i].szWorkMode == "ZBY" || xml.PathPointList[i].szWorkMode == "ZCY")
                    {
                        for (int j = 0; j < xml.PathPointList[i].uiPathPointNum; j++)
                        {
                            IPoint pPointTemp = new PointClass();
                            pPointTemp.X = xml.PathPointList[i].stPathPoints[j].dx;
                            pPointTemp.Y = xml.PathPointList[i].stPathPoints[j].dy;
                            IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                            pFeatureTemp.Shape = pPointTemp;
                            pFeatureTemp.Store();
                        }
                    }
                    if (xml.PathPointList[i].szWorkMode == "YDY")
                    {
                        if(xml.PathPointList[i].szSign == "11H")
                        {
                            angle = angle + xml.PathPointList[i].dAngle*Math.PI/180;
                            angleTemp = angleTemp + xml.PathPointList[i].dAngle*Math.PI/180;
                        }
                        else if(xml.PathPointList[i].szSign == "00H")
                        {
                            angleTemp = angleTemp - xml.PathPointList[i].dAngle* Math.PI / 180 ;
                            angle = angle - xml.PathPointList[i].dAngle*Math.PI / 180;
                        }
                        IPoint pPointt = new PointClass();
                        pPointt.X = X;
                        pPointt.Y = Y;
                        IFeature pF = pFeatureClass.CreateFeature();
                        pF.Shape = pPointt;
                        
                        if (i + 1 > uiPathPhaseNum - 1)
                        {
                            pF.set_Value(pF.Fields.FindField("Angle"), angleTemp);//最后一点的终点航向记录
                            pF.Store();
                        }
                        else
                        {
                            AddAttributeTable(pF, xml.PathPointList[i + 1], angleTemp);
                        }
                    }
                }
                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pFeatureClass;
                pFeatureLayer.Name = textBoxX3.Text + "_passpoint";
                pFeaturelayerOut = pFeatureLayer;//返回本图层，用于导入符号
                m_pMapCtl.AddLayer(pFeatureLayer as ILayer,0);
                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                return pFeatureClass;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        //创建行驶路径文件，输入：线图层，XML，初始航向
        public ILayer CreatePolyLineFeatureClass(IFeatureDataset pFeatureDataset, CPathPoint xml,double dAngleOrg)
        {
            try
            {
                angle = dAngleOrg;//初始航向 //+++++++++++++++++++++++++++++++++++++++++++++传进参数+++++++++++++++++++++++++++++++++++++++
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
                pGeometryDefEdit.SpatialReference_2 = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
                //pGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);

                //增加一个备用字段，用于表达路径类型
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Name";             //类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.IsNullable_2 = true;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Type";             //行走控制模式类型
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dCurv";             //路径曲率
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);               //新建字段

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dAngle";             //原地转向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Angle";             //原地转向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);               
                
                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "szWorkMode";             //行走控制模式类型
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                //pFieldsEdit.AddField(pField);
                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "szMove";             //前进标示
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                //pFieldsEdit.AddField(pField);
                
                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "dMileage";             //路径里程
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                //pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "szSign";             //原地转向角度符号
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                //pFieldsEdit.AddField(pField);
                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = "uiPathPointNum";             //路径点个数
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                //pFieldsEdit.AddField(pField);
                /*for (int i = 0; i < 13; i++)
                {
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "dx_" + i.ToString();             //原地转向角度符号
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFieldsEdit.AddField(pField);
                    pField = new FieldClass();
                    pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "dy_" + i.ToString();             //路径点个数
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFieldsEdit.AddField(pField);
                }*/
                IFeatureClass pFeatureClass = pFeatureDataset.CreateFeatureClass(textBoxX3.Text + "_passline", pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");

                int uiPathPhaseNum = xml.uiPathPhaseNum;
                ISegmentCollection pSegC = new PolylineClass();
                //创建临时坐标变量，记录起点位置坐标
                double X = xml.stStartPointPos.dx;
                double Y = xml.stStartPointPos.dy;

                ////////////////////////////////////////////

                for (int i = 0; i < uiPathPhaseNum; i++)
                {
                    if (xml.PathPointList[i].szWorkMode == "MZY")
                    {
                        IConstructCircularArc constructCircularArc = new CircularArcClass();
                        ICircularArc circularArc = constructCircularArc as ICircularArc;
                        bool isCCW = true;
                        if (xml.PathPointList[i].szMove == "11H") //前进
                        {
                            if (xml.PathPointList[i].dCurv > 0)
                            {
                                isCCW = false;
                            }
                            else if (xml.PathPointList[i].dCurv < 0)
                            {
                                isCCW = true;
                            }
                        }
                        else if (xml.PathPointList[i].szMove == "00H") //后退
                        {
                            if (xml.PathPointList[i].dCurv < 0)
                            {
                                isCCW = false;
                                angle = angle + Math.PI;
                            }
                            else if (xml.PathPointList[i].dCurv > 0)
                            {
                                isCCW = true;
                                angle = angle + Math.PI;
                            }
                        }
                        IPoint fromPoint = new PointClass();
                        IPoint toPoint = new PointClass();
                        fromPoint.PutCoords(X - 10 * Math.Sin(angle), Y - 10 * Math.Cos(angle));//切线角度 
                        if (xml.PathPointList[i].szMove == "00H")
                        {
                                angle = angle - Math.PI;
                        }
                        toPoint.PutCoords(X, Y);
                        ILine line = new LineClass();
                        line.PutCoords(fromPoint, toPoint);
                        if (xml.PathPointList[i].dCurv != 0)//当盲走曲率不为0时
                        {
                            constructCircularArc.ConstructTangentRadiusArc(line as ISegment, false, isCCW, Math.Abs(1 / xml.PathPointList[i].dCurv), xml.PathPointList[i].dMileage);
                            X = circularArc.ToPoint.X;
                            Y = circularArc.ToPoint.Y;
                            if (xml.PathPointList[i].szMove == "11H") //前进
                            {
                                if (xml.PathPointList[i].dCurv > 0)
                                {
                                    angle = angle + xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                                }
                                else if (xml.PathPointList[i].dCurv < 0)
                                {
                                    angle = angle - xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                                }
                            }
                            else if (xml.PathPointList[i].szMove == "00H") //后退
                            {
                                if (xml.PathPointList[i].dCurv < 0)
                                {
                                    angle = angle + xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                                }
                                else if (xml.PathPointList[i].dCurv > 0)
                                {
                                    angle = angle - xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                                }
                            }
                            ISegment pSegment = circularArc as ISegment;
                            ISegmentCollection pSegmentCollection = new PolylineClass();
                            pSegmentCollection.AddSegment(pSegment);
                            pSegC.AddSegment(pSegment);
                            IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                            pFeatureTemp.Shape = pSegmentCollection as IGeometry;
                            //写入字段值
                            //pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("dAngle"), xml.PathPointList[i].dAngle);
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("dCurv"), xml.PathPointList[i].dCurv);

                            pFeatureTemp.Store();
                        }
                        else//当盲走曲率为0时
                        {
                            IPoint pFromPoint=new PointClass();
                            IPoint pEndPoint=new PointClass();
                            pFromPoint.X=X;
                            pFromPoint.Y=Y;
                            X = X + xml.PathPointList[i].dMileage * Math.Cos(Math.PI / 2 - angle);
                            Y = Y + xml.PathPointList[i].dMileage * Math.Sin(Math.PI / 2 - angle);
                            pEndPoint.X=X;
                            pEndPoint.Y=Y;
                            angle = angle + xml.PathPointList[i].dMileage * Math.Abs(xml.PathPointList[i].dCurv);
                            ILine pLinec = new LineClass();
                            pLinec.PutCoords(pFromPoint,pEndPoint);
                            ISegment psegment = pLinec as ISegment;
                            ISegmentCollection pSegmentCollection = new PolylineClass();
                            pSegmentCollection.AddSegment(psegment);
                            pSegC.AddSegment(psegment);
                            IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                            pFeatureTemp.Shape = pSegmentCollection as IGeometry;
                            //写入字段值
                            //pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("dAngle"), xml.PathPointList[i].dAngle);
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("dCurv"), xml.PathPointList[i].dCurv);

                            pFeatureTemp.Store();
                        }
                        
                    }
                    if (xml.PathPointList[i].szWorkMode == "JTY" || xml.PathPointList[i].szWorkMode == "ZAY"
                        || xml.PathPointList[i].szWorkMode == "ZBY" || xml.PathPointList[i].szWorkMode == "ZCY")
                    {
                        for (int j = 0; j < xml.PathPointList[i].uiPathPointNum; j++)
                        {
                            IPointCollection pPointC = new PolylineClass();
                            ILine pline = new LineClass();
                            
                            IPoint pPoint = new PointClass();
                            IPoint pPointTemp = new PointClass();
                            pPoint.X = X;
                            pPoint.Y = Y;
                            pPointC.AddPoint(pPoint);
                            pPointTemp.X = xml.PathPointList[i].stPathPoints[j].dx;
                            pPointTemp.Y = xml.PathPointList[i].stPathPoints[j].dy;
                            pPointC.AddPoint(pPointTemp);
                            pSegC.AddSegment(pPointC as ISegment);
                            X = pPointTemp.X;
                            Y = pPointTemp.Y;
                            IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                            pFeatureTemp.Shape = pPointC as IGeometry;
                            pFeatureTemp.Store();
                            pline.FromPoint = pPoint;
                            pline.ToPoint = pPointTemp;
                            ISegment ps = pline as ISegment;
                            pSegC.AddSegment(ps);

                        }
                    }
                    if (xml.PathPointList[i].szWorkMode == "YDY")
                    {
                        if (xml.PathPointList[i].szSign == "11H")
                        {
                            angle = angle + xml.PathPointList[i].dAngle*Math.PI/180;
                        }
                        else if (xml.PathPointList[i].szSign == "00H")
                        {
                            angle = angle - xml.PathPointList[i].dAngle * Math.PI / 180;
                        }
                    }
                }

                //连接成一条路径
                //IFeature pF = pFeatureClass.CreateFeature();
                //pF.Shape = pSegC as IGeometry;
                //pF.Store();

                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pFeatureClass;
                pFeatureLayer.Name = textBoxX3.Text + "_passline";
                m_pMapCtl.AddLayer(pFeatureLayer as ILayer,1);
                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                ILayer pPasslineLayer=pFeatureLayer as ILayer;
                return pPasslineLayer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        //根据起点和角度及绘制长度绘制方向线,北方向为正，顺时针角度为正，逆时针角度为负
        private IPolyline CreateNavigationline(IPoint pPoint, double dAngle,double dLength)
        {
            double dx = pPoint.X;
            double dy = pPoint.Y;
            IPoint pPointEnd = new PointClass();
            double dRand = (90 - dAngle) / 180 * Math.PI;
            pPointEnd.X = dx + dLength * Math.Cos(dRand);
            pPointEnd.Y = dy + dLength * Math.Sin(dRand);
            ILine pline = new LineClass();
            pline.FromPoint = pPoint;
            pline.ToPoint = pPointEnd;
            ISegment pSegment = pline as ISegment;
            ISegmentCollection pSegmentCollection = new PolylineClass();
            pSegmentCollection.AddSegment(pSegment);
            IPolyline pPolyline = new PolylineClass();
            pPolyline = pSegmentCollection as IPolyline;
            return pPolyline;
        }

        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            CPathPoint xml = new CPathPoint();
            bool result = xml.ReadXML(XMLPath);
            Regex regNum = new Regex("^[0-9]");
            if (regNum.IsMatch(textBoxX3.Text) == true)
            {
                MessageBox.Show("数据集名称不能以数字开头命名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (regNum.IsMatch(TextLine.Text) == true)
            {
                MessageBox.Show("数据集名称不能以数字开头命名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (regNum.IsMatch(textpolygon.Text) == true)
            {
                MessageBox.Show("数据集名称不能以数字开头命名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (result == true)
            {
                try
                {
                    angle = double.Parse(textBoxX4.Text);
                    angle = angle * Math.PI / 180;
                    angleOrign = angle;
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    gdbPath = textBoxX2.Text;
                    IFeatureWorkspace pFeatureWorkspace = workspaceFactory.OpenFromFile(gdbPath, 0) as IFeatureWorkspace;
                    ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();

                    //ISpatialReference pSpatialReference = new UnknownCoordinateSystemClass();
                    ISpatialReference pSpatialReference = ClsGDBDataCommon.CreateProjectedCoordinateSystem();
                    pSpatialReference.SetDomain(-8000000, 8000000, -800000, 8000000);
                    IFeatureDataset pFeatureDataset = pFeatureWorkspace.CreateFeatureDataset(textBoxX3.Text, pSpatialReference);
                    //解析XML文件为点文件
                    IFeatureLayer pFLayerPassPoint = new FeatureLayerClass();
                    IFeatureClass pFeatureclass = CreatePointFeatureClass(pFeatureDataset, xml, angleOrign, ref pFLayerPassPoint);
                    if (pFeatureclass == null)
                    {
                        MessageBox.Show("数据生成失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ILayer pPassLineLayer = CreatePolyLineFeatureClass(pFeatureDataset, xml, angleOrign);
                    ILayer pLineLabelLayer = CreatePolylineLabelFeatureClass(pFeatureclass, angleOrign, 1);
                    CreatePolygonLabelFeatureClass(pFeatureclass);
                    //MessageBox.Show("数据已经生成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    if (cmbImpSymbol.Text != null && symFilePath!=null)
                    {
                        IFeatureLayer pFeaturePassPointLayer = new FeatureLayerClass();
                        pFeaturePassPointLayer.FeatureClass = pFeatureclass;
                        ILayer pPassPointLayer = pFeaturePassPointLayer as ILayer;
                        ImpSymbolFromFile(symFilePath, pFLayerPassPoint, pPassLineLayer, pLineLabelLayer);
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return ;
                }
            }
            else 
            {
                MessageBox.Show("XML文件解析失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void FrmFromXML_Load(object sender, EventArgs e)
        {
            angle = 0;
            textBoxX4.Text ="0";
            textlength.Text = "2.0";
            distance = 2.0;

            LoadMxd(this.cmbImpSymbol, MapTempPath);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Title = "新建File Geodatabase";
                fbd.InitialDirectory = System.IO.Path.GetDirectoryName(XMLPath);
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(fbd.FileName+".gdb") == true)
                    {
                        MessageBox.Show("GDB已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspaceName workspaceName = workspaceFactory.Create(System.IO.Path.GetDirectoryName(fbd.FileName) , System.IO.Path.GetFileName(fbd.FileName), null, 0);
                    gdbPath = fbd.FileName + ".gdb";
                    textBoxX2.Text = fbd.FileName + ".gdb";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            TextLine.Text = textBoxX3.Text + "_polylinelabel";
            textpolygon.Text = textBoxX3.Text + "_polygonlabel";
        }

        private void btnImpSymbol_Click(object sender, EventArgs e)//选择符号渲染图层
        {
            OpenFileDialog dlgMapSymbol = new OpenFileDialog();
            dlgMapSymbol.Title = "选择单元规划模板地图";
            dlgMapSymbol.InitialDirectory = ".";
            dlgMapSymbol.Filter = "mxd文件(*.mxd)|*.mxd";
            dlgMapSymbol.RestoreDirectory = true;
            if (dlgMapSymbol.ShowDialog()==DialogResult.OK)
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
        /// 对生成的单元规划各图层进行符号、标注设置
        /// </summary>
        /// <param name="impfilepath">导入符号mxd文件路径</param>
        /// <param name="passpoint">生成的单元规划点图层</param>
        /// <param name="passline">生成的单元规划行驶路径图层</param>
        /// <param name="polylinelabel">生成的单元规划航向线图层</param>
        private void ImpSymbolFromFile(string impfilepath, ILayer passpoint, ILayer passline, ILayer polylinelabel)
        {
            IMapDocument pMapDocument = new MapDocumentClass();
            pMapDocument.Open(impfilepath, null);
            IMap pMap = pMapDocument.Map[0];

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                ILayer pLayerSymbol = pMap.get_Layer(i);
                if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_passpoint"))
                {
                    IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                    IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                    IFeatureLayer pFPasspointWait = passpoint as IFeatureLayer;
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
                else if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_passline"))
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
                else if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_polylinelabel"))
                {
                    IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                    IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                    IFeatureLayer pFPasspointWait = polylinelabel as IFeatureLayer;
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

        public void LoadMxd(DevComponents.DotNetBar.Controls.ComboBoxEx cmbbox,string FileDir)
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

        private void cmbImpSymbol_SelectedIndexChanged(object sender, EventArgs e)
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
