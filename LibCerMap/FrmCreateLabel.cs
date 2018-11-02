using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Text.RegularExpressions;
using LibModelGen;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmCreateLabel : OfficeForm
    {
        public FrmCreateLabel()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public ESRI.ArcGIS.Controls.AxMapControl m_pMapCtl;
        public double distance;
        public double ViewAngle = -9999;
        public IFeatureDataset pFeatureDataset;
        public IFeatureLayer pFeatureLayer;
        public IRaster pRaster;
        public bool CreatePolylineArrow(double angle,IPoint pPoint, double length, double theta,ref ISegmentCollection pSegmentCollection)
        {
            // CreatePolylineArrow(angle, pToPoint, 0.2 * distance, theta, ref pSegmentCollection);  //创建小箭头
            //左边箭头小短线终点旋转后坐标
            double X1 = -1*length*Math.Sin(theta); //该点在旋转后坐标系中的X坐标
            double Y1 = -1*length*Math.Cos(theta); //该点在旋转后坐标系中的Y坐标
            //右边箭头小短线终点旋转后坐标
            double X2 = length*Math.Sin(theta); //该点在旋转后坐标系中的X坐标
            double Y2 = -1*length*Math.Cos(theta); //该点在旋转后坐标系中的Y坐标
            //旋转坐标系原点在原始坐标系中的坐标
            double a = pPoint.X; //旋转后坐标系原点在原坐标系的位置X
            double b = pPoint.Y; //旋转后坐标系原点在原坐标系的位置Y
            //左边箭头小短线终点原始坐标
            angle = -1 * angle;
            double X11 = X1 * Math.Cos(angle) - Y1 * Math.Sin(angle) +a; //该点在原始坐标系中的X坐标
            double Y11 = X1 * Math.Sin(angle) + Y1 * Math.Cos(angle) +b; //该点在原始坐标系中的Y坐标
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
            double X22 = X2 * Math.Cos(angle) - Y2 * Math.Sin(angle) +a; //该点在原始坐标系中的X坐标
            double Y22 = X2 * Math.Sin(angle) + Y2 * Math.Cos(angle) +b; //该点在原始坐标系中的Y坐标
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
        public bool CreatePolylineFeatureClass()
        {
            /* if (double.TryParse(textBoxX3.Text, out distance) == false)
             {
                 MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 return;
             }*/
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
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "OriginalFeatureClass"; //原始图层名称
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Angle";             //航向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "szWorkMode";             //行走控制模式
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "szMove";    //前进标示
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString; 
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dAngle";             //原地转向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);
                IFeatureClass pFeatureClass = null;
                try
                {
                    pFeatureClass = pFeatureDataset.CreateFeatureClass(textBoxX1.Text, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                }
                catch (SystemException eee)
                {
                    if (eee.Message == "The table already exists.")
                    {
                        ClsGDBDataCommon cls = new ClsGDBDataCommon();
                        IWorkspace ws = pFeatureDataset.Workspace;
                        IFeatureWorkspace fws = ws as IFeatureWorkspace;
                        pFeatureClass = fws.OpenFeatureClass(textBoxX1.Text);
                        ITable ptable = pFeatureClass as ITable;
                        ptable.DeleteSearchedRows(null);
                    }
                    //MessageBox.Show(eee.Message);
                }
                if (double.TryParse(textBoxX3.Text, out distance) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                IQueryFilter pQF = new QueryFilterClass();
                IFeatureCursor pFC = pFeatureLayer.FeatureClass.Search(null, false);
                IFeature pF = pFC.NextFeature();

                //////////////////////////////////////////////////////////////////////
                //首先绘制起始航向
                double dAngleOrg = System.Convert.ToDouble(pF.get_Value(pF.Fields.FindField("Angle")));
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

                pFTemp.set_Value(pFTemp.Fields.FindField("OriginalFeatureClass"), pFeatureLayer.Name);
                pFTemp.Store();

                ///////////////////////////////////////////////////

                while (pF != null)
                {
                    double angle = 0;
                    string szWorkMode;
                    int OID = 0;
                    OID = int.Parse(pF.get_Value(pF.Fields.FindField("OBJECTID")).ToString());
                    if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("Angle")).ToString()) == false)
                    {
                        angle = double.Parse(pF.get_Value(pF.Fields.FindField("Angle")).ToString());

                        if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("szWorkMode")).ToString()) == false)
                        {
                            szWorkMode = pF.get_Value(pF.Fields.FindField("szWorkMode")).ToString();
                            if (szWorkMode == "MZY")
                            {
                                IPoint pPoint = pF.Shape as IPoint;
                                IPoint pToPoint = new PointClass();
                                pToPoint.X = pPoint.X + distance * Math.Sin(angle) * 1.2;
                                pToPoint.Y = pPoint.Y + distance * Math.Cos(angle) * 1.2;
                                ILine pline = new LineClass();
                                pline.FromPoint = pPoint;
                                pline.ToPoint = pToPoint;
                                ISegment pSegment = pline as ISegment;
                                ISegmentCollection pSegmentCollection = new PolylineClass();
                                pSegmentCollection.AddSegment(pSegment);
                                CreatePolylineArrow(angle, pToPoint, 0.2 * distance, Math.PI / 6, ref pSegmentCollection);  //创建小箭头
                                IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                                pFeatureTemp.Shape = pSegmentCollection as IGeometry;
                                string szMove = "";
                                if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("szMove")).ToString()) == false)
                                {
                                    szMove = pF.get_Value(pF.Fields.FindField("szMove")).ToString();
                                }
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OID"), OID);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szMove"), szMove);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), angle);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), "MZY");
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), pFeatureLayer.Name);
                                pFeatureTemp.Store();
                            }
                            else if (szWorkMode == "YDY")
                            {
                                IPoint pPoint = pF.Shape as IPoint;
                                IPoint pToPoint = new PointClass();
                                pToPoint.X = pPoint.X + distance * Math.Sin(angle);
                                pToPoint.Y = pPoint.Y + distance * Math.Cos(angle);
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
                                    toAngle = angle - dAngle * Math.PI / 180 - Math.PI / 2;
                                }
                                else if (szSign == "11H")
                                {
                                    isCCW = false;
                                    toAngle = angle + dAngle * Math.PI / 180 + Math.PI / 2;
                                }
                                IConstructCircularArc constructCircularArc = new CircularArcClass();
                                ICircularArc circularArc = constructCircularArc as ICircularArc;
                                constructCircularArc.ConstructArcDistance(pPoint, pToPoint, isCCW, distance * dAngle * Math.PI / 180);
                                ISegment pSegment = circularArc as ISegment;
                                ISegmentCollection pSegmentCollection = new PolylineClass();
                                pSegmentCollection.AddSegment(pSegment);
                                //ILine pline = new LineClass();
                                //pline.FromPoint = pToPoint;
                                //pline.ToPoint = pPoint;
                                //pSegmentCollection.AddSegment(pline as ISegment);
                                CreatePolylineArrow(toAngle, circularArc.ToPoint, 0.2 * distance, Math.PI / 6, ref pSegmentCollection);  //创建小箭头
                                IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                                pFeatureTemp.Shape = pSegmentCollection as IGeometry;
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OID"), OID);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), angle);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), pFeatureLayer.Name);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), "YDY");
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("dAngle"), dAngle);
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
                                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), pFeatureLayer.Name);
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
                            pToPoint.X = pPoint.X + distance * Math.Sin(angle) * 1.2;
                            pToPoint.Y = pPoint.Y + distance * Math.Cos(angle) * 1.2;
                            ILine pline = new LineClass();
                            pline.FromPoint = pPoint;
                            pline.ToPoint = pToPoint;
                            ISegment pSegment = pline as ISegment;
                            ISegmentCollection pSegmentCollection = new PolylineClass();
                            pSegmentCollection.AddSegment(pSegment);
                            CreatePolylineArrow(angle, pToPoint, 0.2 * distance, Math.PI / 6, ref pSegmentCollection);  //创建小箭头
                            IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                            pFeatureTemp.Shape = pSegmentCollection as IGeometry;

                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), angle);
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), "MZY");
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), pFeatureLayer.Name);
                            pFeatureTemp.Store();
                        }
                    }                    
                    pF = pFC.NextFeature();
                }
                bool exist = false;
                for (int i = 0; i < m_pMapCtl.Map.LayerCount; i++)
                {
                    ILayer pLayer = m_pMapCtl.Map.get_Layer(i);
                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer pfL = pLayer as IFeatureLayer;
                        esriGeometryType pType = pfL.FeatureClass.ShapeType;
                        if (pType == esriGeometryType.esriGeometryPolyline)
                        {
                            if (pLayer.Name == textBoxX1.Text)
                            {
                                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                                exist = true;
                            }
                        }
                    }
                }
                if (exist == false)
                {
                    IFeatureLayer ppFeatureLayer = new FeatureLayerClass();
                    ppFeatureLayer.FeatureClass = pFeatureClass;
                    ppFeatureLayer.Name = textBoxX1.Text;
                    m_pMapCtl.AddLayer(ppFeatureLayer as ILayer);
                    m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        public bool CreatePolygonFeatureClass()
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
                pFieldEdit.Name_2 = "OID";             //原始OBJECTID
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "OriginalFeatureClass"; //原始图层名称
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Angle";             //航向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "ViewAngle";             //ViewAngle
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "ViewOrientation";             //ViewOrientation
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "szWorkMode";             //行走控制模式
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldsEdit.AddField(pField);
                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "dAngle";             //原地转向角度
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.AddField(pField);
                IFeatureClass pFeatureClass = null;
                try
                {
                    pFeatureClass = pFeatureDataset.CreateFeatureClass(textBoxX2.Text, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                }
                catch (SystemException eee)
                {
                    if (eee.Message == "The table already exists.")
                    {
                        ClsGDBDataCommon cls = new ClsGDBDataCommon();
                        IWorkspace ws = pFeatureDataset.Workspace;
                        IFeatureWorkspace fws = ws as IFeatureWorkspace;
                        pFeatureClass = fws.OpenFeatureClass(textBoxX2.Text);
                        ITable ptable = pFeatureClass as ITable;
                        ptable.DeleteSearchedRows(null);
                    }
                    //MessageBox.Show(eee.Message);
                }
                if (double.TryParse(textBoxX3.Text, out distance) == false)
                {
                    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                //if (double.TryParse(textBoxX4.Text, out ViewAngle) == false)
                //{
                //    MessageBox.Show("不合法的字符，请重新输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return false;
                //}
                //else
                //{
                //    ViewAngle = ViewAngle * Math.PI / 180;
                //}
                IQueryFilter pQF = new QueryFilterClass();
                IFeatureCursor pFC = pFeatureLayer.FeatureClass.Search(null, false);
                IFeature pF = pFC.NextFeature();
                while (pF != null)
                {
                    double angle = 0;
                    string szWorkMode;
                    int OID = 0;
                    OID = int.Parse(pF.get_Value(pF.Fields.FindField("OBJECTID")).ToString());
                    if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("Angle")).ToString()) == false)
                    {
                        angle = double.Parse(pF.get_Value(pF.Fields.FindField("Angle")).ToString());
                    }
                    if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("ViewAngle")).ToString()) == false)
                    {
                        ViewAngle = double.Parse(pF.get_Value(pF.Fields.FindField("ViewAngle")).ToString());
                        ViewAngle = ViewAngle * Math.PI / 180;
                        if (ViewAngle <= 0)
                        {
                            pF = pFC.NextFeature();
                            continue;
                        }
                        if (ViewAngle > 2*Math.PI)
                        {
                            ViewAngle = 2 * Math.PI;
                        }
                    }
                    else 
                    {
                        pF = pFC.NextFeature();
                        continue;
                    }
                    if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("szWorkMode")).ToString()) == false)
                    {
                        szWorkMode = pF.get_Value(pF.Fields.FindField("szWorkMode")).ToString();
                        if (szWorkMode == "MZY")
                        {
                            IPoint pPoint = pF.Shape as IPoint;
                            IPoint pToPoint = new PointClass();
                            ISegmentCollection pSegmentCollection = new RingClass();
                            pToPoint.X = pPoint.X + distance * Math.Sin(angle + ViewAngle / 2);
                            pToPoint.Y = pPoint.Y + distance * Math.Cos(angle + ViewAngle / 2);
                            ILine pline = new LineClass();
                            pline.FromPoint = pPoint;
                            pline.ToPoint = pToPoint;
                            pSegmentCollection.AddSegment(pline as ISegment);
                            IConstructCircularArc constructCircularArc = new CircularArcClass();
                            ICircularArc circularArc = constructCircularArc as ICircularArc;
                            constructCircularArc.ConstructArcDistance(pPoint, pToPoint, true, distance * ViewAngle);
                            pSegmentCollection.AddSegment(circularArc as ISegment);
                            IRing pRing = pSegmentCollection as IRing;
                            pRing.Close();
                            IGeometryCollection pGeometryCollection = new PolygonClass();
                            object ob = Type.Missing;
                            pGeometryCollection.AddGeometry(pRing, ref ob, ref ob);
                            ESRI.ArcGIS.Geometry.IPolygon polygon = (IPolygon)pGeometryCollection;
                            IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                            pFeatureTemp.Shape = polygon;
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OID"), OID);
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), angle);
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), pFeatureLayer.Name);
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), "MZY");
                            pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("ViewAngle"), ViewAngle);
                            pFeatureTemp.Store();
                        }
                        else if (szWorkMode == "YDY")
                        {
                            /* IPoint pPoint = pF.Shape as IPoint;
                             IPoint pToPoint = new PointClass();
                             pToPoint.X = pPoint.X + distance * Math.Sin(angle);
                             pToPoint.Y = pPoint.Y + distance * Math.Cos(angle);
                             double dAngle = 0;
                             string szSign = "11H";
                             bool isCCW = true;
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
                             }
                             else if (szSign == "11H")
                             {
                                 isCCW = false;
                             }
                             IConstructCircularArc constructCircularArc = new CircularArcClass();
                             ICircularArc circularArc = constructCircularArc as ICircularArc;
                             constructCircularArc.ConstructArcDistance(pPoint, pToPoint, isCCW, distance * dAngle);
                             ISegment pSegment = circularArc as ISegment;
                             ISegmentCollection pSegmentCollection = new PolylineClass();
                             pSegmentCollection.AddSegment(pSegment);
                             IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                             pFeatureTemp.Shape = pSegmentCollection as IGeometry;
                             pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OID"), OID);
                             pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Angle"), angle);
                             pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), pFeatureLayer.Name);
                             pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), "YDY");
                             pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("dAngle"), dAngle);
                             pFeatureTemp.Store();*/
                        }
                        else if (szWorkMode == "JTY" || szWorkMode == "ZAY"
                        || szWorkMode == "ZBY" || szWorkMode == "ZCY")
                        {
                            int num = 0;
                            if (String.IsNullOrEmpty(pF.get_Value(pF.Fields.FindField("uiPathPointNum")).ToString()) == false)
                            {
                                num = int.Parse(pF.get_Value(pF.Fields.FindField("uiPathPointNum")).ToString());
                            }
                            else
                            {
                                MessageBox.Show("避障模式属性表信息缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return false;
                            }
                            IPoint pPoint = pF.Shape as IPoint;
                            IPoint pToPoint = new PointClass();
                            double[,] XY = new double[num, 2];
                            IFeatureCursor pFeatureCursor = pFC;
                            for (int i = 0; i < num; i++)
                            {
                                if (ViewAngle == -9999)
                                {
                                    continue;
                                }
                                XY[i, 0] = double.Parse(pF.get_Value(pF.Fields.FindField("dx_" + i.ToString())).ToString());
                                XY[i, 1] = double.Parse(pF.get_Value(pF.Fields.FindField("dy_" + i.ToString())).ToString());
                                double angleTemp = Math.Atan((XY[i, 1] - pPoint.Y) / (XY[i, 0] - pPoint.X));
                                pToPoint.X = pPoint.X + distance * Math.Cos(angleTemp - ViewAngle / 2); ;
                                pToPoint.Y = pPoint.Y + distance * Math.Sin(angleTemp - ViewAngle / 2);
                                ILine pline = new LineClass();
                                pline.FromPoint = pPoint;
                                pline.ToPoint = pToPoint;
                                ISegment pSegment = pline as ISegment;
                                ISegmentCollection pSegmentCollection = new RingClass();
                                pSegmentCollection.AddSegment(pSegment);
                                IConstructCircularArc constructCircularArc = new CircularArcClass();
                                ICircularArc circularArc = constructCircularArc as ICircularArc;
                                constructCircularArc.ConstructArcDistance(pPoint, pToPoint, true, distance * ViewAngle);
                                pSegmentCollection.AddSegment(circularArc as ISegment);
                                IRing pRing = pSegmentCollection as IRing;
                                pRing.Close();
                                IGeometryCollection pGeometryCollection = new PolygonClass();
                                object ob = Type.Missing;
                                pGeometryCollection.AddGeometry(pRing, ref ob, ref ob);
                                ESRI.ArcGIS.Geometry.IPolygon polygon = (IPolygon)pGeometryCollection;
                                IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                                pFeatureTemp.Shape = polygon;
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OID"), OID + i);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("szWorkMode"), szWorkMode);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("OriginalFeatureClass"), pFeatureLayer.Name);
                                pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("ViewAngle"), ViewAngle);
                                pFeatureTemp.Store();
                                pPoint.X = XY[i, 0];
                                pPoint.Y = XY[i, 1];
                                IFeature pFtemp = pFeatureCursor.NextFeature();
                                if (pFtemp != null)
                                {
                                    if (String.IsNullOrEmpty(pFtemp.get_Value(pFtemp.Fields.FindField("ViewAngle")).ToString()) == false)
                                    {
                                        ViewAngle = double.Parse(pFtemp.get_Value(pFtemp.Fields.FindField("ViewAngle")).ToString());
                                        ViewAngle = ViewAngle * Math.PI / 180;
                                        if (ViewAngle <= 0)
                                        {
                                            ViewAngle = -9999;
                                            continue;
                                        }
                                        if (ViewAngle > 2 * Math.PI)
                                        {
                                            ViewAngle = 2 * Math.PI;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        ViewAngle = -9999;
                                        continue;
                                        //ViewAngle = double.Parse(textBoxX4.Text) * Math.PI / 180;
                                    }
                                }
                            }

                        }
                    }
                    pF = pFC.NextFeature();
                }

                bool exist = false;
                for (int i = 0; i < m_pMapCtl.Map.LayerCount; i++)
                {
                    ILayer pLayer = m_pMapCtl.Map.get_Layer(i);
                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer pfL = pLayer as IFeatureLayer;
                        esriGeometryType pType = pfL.FeatureClass.ShapeType;
                        if (pType == esriGeometryType.esriGeometryPolygon)
                        {
                            if (pLayer.Name == textBoxX2.Text)
                            {
                                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                                exist = true;
                            }
                        }
                    }
                }
                if (exist == false)
                {
                    IFeatureLayer ppFeatureLayer = new FeatureLayerClass();
                    ppFeatureLayer.FeatureClass = pFeatureClass;
                    ppFeatureLayer.Name = textBoxX2.Text;
                    m_pMapCtl.AddLayer(ppFeatureLayer as ILayer);
                    m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public bool CreatePolygon()
        {
            IFeatureClass pFC = InitialLayer(textBoxX2.Text);
            if (pFC == null)
            {
                return false;
            }

            if (!GetCamOri(textBoxX4.Text,pFC))
            {
                return false;
            }

            return true;
        }

        public IFeatureClass InitialLayer(string strLayerName)
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
                    pFeatureClass = pFeatureDataset.CreateFeatureClass(strLayerName, pFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                }
                catch (SystemException eee)
                {
                    if (eee.Message == "The table already exists.")
                    {
                        ClsGDBDataCommon cls = new ClsGDBDataCommon();
                        IWorkspace ws = pFeatureDataset.Workspace;
                        IFeatureWorkspace fws = ws as IFeatureWorkspace;
                        pFeatureClass = fws.OpenFeatureClass(textBoxX2.Text);
                        ITable ptable = pFeatureClass as ITable;
                        ptable.DeleteSearchedRows(null);
                        return pFeatureClass;
                    }
                }
                return pFeatureClass;
            }
            catch (SystemException e)
            {
                return null;
            }
        }


        public bool GetCamOri(string strFileOri, IFeatureClass pFeatureClass)
        {
            try
            {
                CViewRange CView = new CViewRange();
                //bool istrue = CView.ReadExcel(strFileOri);
                bool istrue = CView.ReadTextFile(strFileOri);
                if (istrue == false)
                {
                    return false;
                }
                ClsGetCameraView CGetCameraView = new ClsGetCameraView();
                ExOriPara pExOriPara = new ExOriPara();
                InOriPara pInOriPara = new InOriPara();
                
                for(int i = 0;i<CView.count;i++)
                {
                    Point2D[,] ptResult = null;
                    pExOriPara.ori.omg = CView.PointInformationList[i].omg;
                    pExOriPara.ori.phi = CView.PointInformationList[i].phi;
                    pExOriPara.ori.kap = CView.PointInformationList[i].kap;
                    pExOriPara.pos.X = CView.PointInformationList[i].X;
                    pExOriPara.pos.Y = CView.PointInformationList[i].Y;
                    pExOriPara.pos.Z = CView.PointInformationList[i].Z;

                    pInOriPara.df = CView.PointInformationList[i].f;
                    pInOriPara.dfx = CView.PointInformationList[i].fx;
                    pInOriPara.dfy = CView.PointInformationList[i].fy;
                    pInOriPara.nW = CView.PointInformationList[i].width;
                    pInOriPara.nH = CView.PointInformationList[i].height;

                    DrawPolygon(pRaster, pExOriPara, pInOriPara, pFeatureClass, CView.PointInformationList[i].PointID);
                }
            }
            catch (SystemException e)
            {
                return false;
            }

            return true;
        }

        public bool DrawPolygon(IRaster pPaster, ExOriPara pExOriPara, InOriPara pInOriPara,IFeatureClass pFeatureClass,int nID)
        {
            try
            {
                Point2D[,] ptResult = null;
                ClsGetCameraView CGetCameraView = new ClsGetCameraView();
                if (CGetCameraView.ImageReprojectionRange(pRaster, out ptResult, pExOriPara, pInOriPara,50))
                {
                    //continue;
                    //MessageBox.Show("dONE!");
                    //ISegmentCollection pSegmentCollection = new RingClass();
                    IPointCollection pPointCollection = new PolygonClass();
                    for (int j = 0; j < ptResult.GetLength(1); j++)
                    {
                        if (ptResult[0, j] == null)
                        {
                            continue;
                        }
                        IPoint pPoint1 = new PointClass();
                        pPoint1.X = ptResult[0, j].X;
                        pPoint1.Y = ptResult[0, j].Y;
                        pPointCollection.AddPoint(pPoint1);
                    }
                    for (int k = ptResult.GetLength(1) - 1; k >= 0; k--)
                    {
                        if (ptResult[1, k] == null)
                        {
                            continue;
                        }

                        IPoint pPoint2 = new PointClass();
                        pPoint2.X = ptResult[1, k].X;
                        pPoint2.Y = ptResult[1, k].Y;
                        pPointCollection.AddPoint(pPoint2);
                    }
                    IFeature pFeatureTemp = pFeatureClass.CreateFeature();
                    IPolygon pPolygon = new PolygonClass();
                    pPolygon = pPointCollection as IPolygon;
                    pPolygon.Close();
                    pFeatureTemp.Shape = pPolygon as IGeometry;
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("PointID"),nID);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("X"), pExOriPara.pos.X);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Y"), pExOriPara.pos.Y);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("Z"), pExOriPara.pos.Z);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("omg"), pExOriPara.ori.omg);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("phi"), pExOriPara.ori.phi);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("kap"), pExOriPara.ori.kap);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("width"), pInOriPara.nW);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("height"), pInOriPara.nH);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("fx"), pInOriPara.dfx);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("fy"), pInOriPara.dfy);
                    pFeatureTemp.set_Value(pFeatureTemp.Fields.FindField("f"), pInOriPara.df);
                    pFeatureTemp.Store();
                }

    
                bool exist = false;
                for (int i = 0; i < m_pMapCtl.Map.LayerCount; i++)
                {
                    ILayer pLayer = m_pMapCtl.Map.get_Layer(i);
                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer pfL = pLayer as IFeatureLayer;
                        esriGeometryType pType = pfL.FeatureClass.ShapeType;
                        if (pType == esriGeometryType.esriGeometryPolygon)
                        {
                            if (pLayer.Name == textBoxX2.Text)
                            {
                                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                                exist = true;
                            }
                        }
                    }
                }
                if (exist == false)
                {
                    IFeatureLayer ppFeatureLayer = new FeatureLayerClass();
                    ppFeatureLayer.FeatureClass = pFeatureClass;
                    ppFeatureLayer.Name = textBoxX2.Text;
                    m_pMapCtl.AddLayer(ppFeatureLayer as ILayer);
                    m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            bool istruePolyline = CreatePolylineFeatureClass();
            if (istruePolyline == false)
            {
                return;
            }
           /* bool istruePolygon = CreatePolygonFeatureClass();
            if (istruePolygon == false)
            {
                return;
            }*/
            bool istruePolygon = CreatePolygon();
            if (istruePolygon == false)
            {
                return;
            }
            this.Close();
        }

        private void buttonX_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCreateLabel_Load(object sender, EventArgs e)
        {
            textBoxX3.Text = "2.0";
           // textBoxX4.Text = "30";
            distance = 2.0;
            ViewAngle = 30 * Math.PI / 180;
            if (m_pMapCtl.Map != null)
            {
                for (int i = 0; i < m_pMapCtl.Map.LayerCount; i++)
                {
                    ILayer pLayer = m_pMapCtl.Map.get_Layer(i);
                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer pfL = pLayer as IFeatureLayer;
                        esriGeometryType pType = pfL.FeatureClass.ShapeType;
                        if (pType == esriGeometryType.esriGeometryPoint)
                        {
                            cmbTargetRasterLayer.Items.Add(pLayer.Name);
                        }
                        /*IFeatureClass pfc = pfL.FeatureClass;
                        pfc.FeatureDataset.PropertySet*/
                        // IDataLayer pDatalayer = pLayer as IDataLayer;
                        //IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;
                        //cmbTargetRasterLayer.Items.Add(pDname.WorkspaceName.PathName + pDname.Name);

                    }
                    if (pLayer is IRasterLayer)
                    {
                       /* IDataLayer pDatalayer = pLayer as IDataLayer;
                        IDatasetName pDname = (IDatasetName)pDatalayer.DataSourceName;
                        comboBoxEx1.Items.Add(pDname.WorkspaceName.PathName + pDname.Name);*/
                        IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                        if (pRasterLayer.BandCount == 1)
                        {
                            comboBoxEx1.Items.Add(pLayer.Name);
                        }
                    }
                }
                if (cmbTargetRasterLayer.Items.Count > 0)
                {
                    cmbTargetRasterLayer.SelectedIndex = 0;
                }
                if (comboBoxEx1.Items.Count > 0)
                {
                    comboBoxEx1.SelectedIndex = 0;
                }
            }
        }

        private void cmbTargetRasterLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_pMapCtl.Map != null)
            {
                for (int i = 0; i < m_pMapCtl.Map.LayerCount; i++)
                {
                    ILayer pLayer = m_pMapCtl.Map.get_Layer(i);
                    if (pLayer is IFeatureLayer)
                    {
                        if (pLayer.Name == cmbTargetRasterLayer.SelectedItem.ToString())
                        {
                            pFeatureLayer = pLayer as IFeatureLayer;
                            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                            pFeatureDataset = pFeatureClass.FeatureDataset;
                            textBoxX1.Text = pFeatureDataset.Name + "_polylinelabel";
                            textBoxX2.Text = pFeatureDataset.Name + "_polygonlabel";
                        }
                    }
                }
            }
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "EXCEL文件(*.txt)|*.txt;|(*.xls)|*.xls;|(*.xlsx)|*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxX4.Text = dlg.FileName;
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_pMapCtl.Map != null)
            {
                for (int i = 0; i < m_pMapCtl.Map.LayerCount; i++)
                {
                    ILayer pLayer = m_pMapCtl.Map.get_Layer(i);
                    if (pLayer is IRasterLayer)
                    {
                        if (pLayer.Name == comboBoxEx1.SelectedItem.ToString())
                        {
                            IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                            pRaster = pRasterLayer.Raster;
                        }
                    }
                }
            }
        }
    }
}
