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
using System.Data.OleDb;

namespace LibCerMap
{
    public partial class FrmCenterLineInsideAlignment : OfficeForm
    {
        //点图层的投影信息
        ISpatialReference psf = null;

        IMapControl3 pMapcontrol;

        public FrmCenterLineInsideAlignment(IMapControl3 mapcontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapcontrol = mapcontrol;
        }

        private void FrmPointToLine_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < pMapcontrol.LayerCount; i++)
                {
                    ILayer pLayer = null;
                    if (pMapcontrol.get_Layer(i) is IFeatureLayer)
                    {
                        pLayer = pMapcontrol.get_Layer(i);
                        IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                        IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                        {
                            cboBoxPointLayer.Items.Add(pLayer.Name);
                        }
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                        {
                            comboBoxExCenterlineLayer.Items.Add(pLayer.Name);
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
                if (comboBoxExCenterlineLayer.Items.Count > 0)
                {
                    comboBoxExCenterlineLayer.SelectedIndex = 0;
                }
            }
            catch(SystemException ex)
            {
                
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
                string pCenterlinName = comboBoxExCenterlineLayer.SelectedItem.ToString();
                IFeatureLayer pIMUPointLayer = null;
                IFeatureLayer pCenterlinePointLayer = null;
                for (int i = 0; i < pMapcontrol.LayerCount; i++)
                {
                    if (pPointFileName == pMapcontrol.get_Layer(i).Name)
                    {
                        pIMUPointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                    if (pCenterlinName == pMapcontrol.get_Layer(i).Name)
                    {
                        pCenterlinePointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                }
                IQueryFilter pQF = null;
                IFeatureClass pLineFC = pCenterlinePointLayer.FeatureClass;
                DataTable CenterlinePointTable = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pLineFC as ITable, pQF);
                IFeatureClass pPointFC = pIMUPointLayer.FeatureClass;

                DataTable IMUTable;
                if (radioButtonFromMap.Checked)
                {
                    IMUTable = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pPointFC as ITable, pQF);
                }
                else
                {
                    string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + this.textBoxFile.Text + ";Extended Properties=Excel 8.0";
                    OleDbConnection conn = new OleDbConnection(strCon);
                    string sql1 = "select * from [Sheet1$]";
                    conn.Open();
                    OleDbDataAdapter myCommand = new OleDbDataAdapter(sql1, strCon);
                    DataSet ds = new DataSet();
                    myCommand.Fill(ds);
                    conn.Close();
                    IMUTable = ds.Tables[0];
                    IMUTable.Columns[EvConfig.IMUMoveDistanceField].DataType = System.Type.GetType("System.Double");
                }




                double beginM = (double)numericUpDown1.Value;
                double endM = (double)numericUpDown2.Value;



                DataView dv = CenterlinePointTable.DefaultView;
                // dv.Sort = "里程（m） ASC";
                dv.Sort = EvConfig.CenterlineMeasureField + " ASC";
                CenterlinePointTable = dv.ToTable();
                dv = IMUTable.DefaultView;
                // dv.Sort = "记录距离__ ASC";
                dv.Sort = EvConfig.IMUMoveDistanceField + " ASC";
                IMUTable = dv.ToTable();
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

                foreach (DataRow r in IMUTable.Rows)
                {
                    r["对齐里程"] = DBNull.Value;
                }

                double endIMUM = Convert.ToDouble(IMUTable.Rows[IMUTable.Rows.Count - 1][EvConfig.IMUMoveDistanceField]);
                double beginIMUM = Convert.ToDouble(IMUTable.Rows[0][EvConfig.IMUMoveDistanceField]);
                double IMULength = endIMUM - beginIMUM;

                Dictionary<string, string> TypeMatchDic = new Dictionary<string, string>();
                //key 中线点属性， value 内检测点属性
                TypeMatchDic.Add("阀门", "阀门");
                TypeMatchDic.Add("拐点", "弯头");
                TypeMatchDic.Add("三通", "三通");
                TypeMatchDic.Add("地面标记", "地面标记");
                TypeMatchDic.Add("开挖点", "开挖点");

                Dictionary<string, string> SpecialMatchDic = new Dictionary<string, string>();
                SpecialMatchDic.Add("阀门", "阀门");
                SpecialMatchDic.Add("三通", "三通");
                SpecialMatchDic.Add("地面标记", "地面标记");
                SpecialMatchDic.Add("开挖点", "开挖点");
                //根据特殊控制点强制对齐
                if (!PreMatchBySpecialControlPoint(SpecialMatchDic, IMUTable, CenterlinePointTable)) 
                {
                    return;
                }

                List<DataRow> NeijianceControlPointList = (from DataRow r in IMUTable.Rows
                                                           where r["类型"] != DBNull.Value && IsNeiJianCeDianControlPointType(r["类型"].ToString(), TypeMatchDic)
                                                           select r).ToList();

                List<DataRow> ZhongXianControlPointList = (from DataRow r in CenterlinePointTable.Rows
                                                           where r["测点属性"] != DBNull.Value && IsZhongXianDianControlPointType(r["测点属性"].ToString(), TypeMatchDic)
                                                           select r).ToList();


                Dictionary<DataRow, DataRow> MatchedDataRowPair = new Dictionary<DataRow, DataRow>();
                int LastMatchedPointsCount = -1;

                

                while (true)
                {
                    int matchedrowCount = IMUTable.AsEnumerable().Where(x => x["对齐里程"] != DBNull.Value).Count();
                    MatchedDataRowPair.Clear();
                    for (int i = 0; i < NeijianceControlPointList.Count; i++)
                    {
                        DataRow IMUr = NeijianceControlPointList[i];
                                                
                        double ActionIMUM = (Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField]) - beginIMUM) * centerlineLength / IMULength + beginM;
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
                                double beforeM = Convert.ToDouble( beforeMatchedRow["对齐里程"]);
                                double beforeD =  Convert.ToDouble( beforeMatchedRow[EvConfig.IMUMoveDistanceField]);

                                DataRow nextMatchedRow = IMUTable.AsEnumerable().Where(x => x["对齐里程"] != DBNull.Value &&
                                    Convert.ToDouble(x[EvConfig.IMUMoveDistanceField]) > Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField])).First(); 
                                double nextM = Convert.ToDouble( nextMatchedRow["对齐里程"]);
                                double nextD = Convert.ToDouble( nextMatchedRow[EvConfig.IMUMoveDistanceField]);

                                double currentD = Convert.ToDouble(IMUr[EvConfig.IMUMoveDistanceField]);
                                ActionIMUM = (currentD - beforeD) * (nextM - beforeM) / (nextD - beforeD) + beforeM;
                            }
                        }
                        List<DataRow> Featurerow = (from r in ZhongXianControlPointList
                                                    where Math.Abs(Convert.ToDouble(r[EvConfig.CenterlineMeasureField]) - ActionIMUM) < Convert.ToDouble(numericUpDown3.Value) &&
                                                     IsZhongXianNejianceCouldMatch(r["测点属性"].ToString(), IMUr["类型"].ToString(), TypeMatchDic)
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

                    //IMUTable.Rows[0]["对齐里程"] = beginM;
                    //IMUTable.Rows[IMUTable.Rows.Count - 1]["对齐里程"] = endM;
                }
                    CalculateNullMeasureBasedOnControlpointMeasure(ref IMUTable);

                    //DataRow PrevRowWithM = null;
                    //for (int i = 0; i < IMUTable.Rows.Count; i++)
                    //{
                    //    DataRow r = IMUTable.Rows[i];
                    //    if (r["对齐里程"] != DBNull.Value)
                    //    {
                    //        PrevRowWithM = r;
                    //    }
                    //    else
                    //    {
                    //        DataRow NextRowWithM = null;
                    //        for (int j = i + 1; j < IMUTable.Rows.Count; j++)
                    //        {
                    //            DataRow r2 = IMUTable.Rows[j];
                    //            if (r2["对齐里程"] != DBNull.Value)
                    //            {
                    //                NextRowWithM = r2;
                    //                break;
                    //            }
                    //        }
                    //        if (PrevRowWithM == null || NextRowWithM == null)
                    //        {
                    //            break;
                    //        }
                    //        double BeginJiluM = Convert.ToDouble(PrevRowWithM[EvConfig.IMUMoveDistanceField]);
                    //        double endJiluM = Convert.ToDouble(NextRowWithM[EvConfig.IMUMoveDistanceField]);
                    //        double BeginAM = Convert.ToDouble(PrevRowWithM["对齐里程"]);
                    //        double endAM = Convert.ToDouble(NextRowWithM["对齐里程"]);
                    //        double currentJiluM = Convert.ToDouble(r[EvConfig.IMUMoveDistanceField]);
                    //        r["对齐里程"] = (currentJiluM - BeginJiluM) * (endAM - BeginAM) / (endJiluM - BeginJiluM) + BeginAM;
                    //    }
                    //}

              




                IFeatureLayer pLinearlayer = null;
                string pCenterlineLinearName = comboBoxExCenterlineLinearLayer.SelectedItem.ToString();
                for (int i = 0; i < pMapcontrol.LayerCount; i++)
                {
                    if (pCenterlineLinearName == pMapcontrol.get_Layer(i).Name)
                    {
                        pLinearlayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                }
                IFeatureCursor pcursor = pLinearlayer.FeatureClass.Search(null, false);
                IFeature pFeature = pcursor.NextFeature();
                IPolyline pline = pFeature.Shape as IPolyline;
                IMSegmentation mSegment = pline as IMSegmentation;
                double maxM = mSegment.MMax;
                double minM = mSegment.MMin;

                if (beginM > maxM || beginM < minM || endM > maxM || endM < minM)
                {
                    MessageBox.Show("输入的起始或终点里程值超出中线里程范围!");
                    return;
                }

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


                FrmIMUAlignmentresult frm = new FrmIMUAlignmentresult(IMUTable);
                frm.CenterlinePointTable = CenterlinePointTable;
                frm.setResultType("内检测对齐中线报告");
                frm.ShowDialog();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }
        // 根据特殊标记点强制对齐，特殊对齐点的顺序，数量，默认完全对应，所以直接按照顺序对齐
        private bool PreMatchBySpecialControlPoint(Dictionary<string, string> SpecialMatchDic, DataTable IMUTable, DataTable CenterlinePointTable)
        {
            Dictionary<DataRow, DataRow> MatchedDataRowPair = new Dictionary<DataRow, DataRow>();
            foreach (string k in SpecialMatchDic.Keys)
            {
                string IMUType = SpecialMatchDic[k];
                string CenterlineType = k;
                int IMUTypeCount = IMUTable.AsEnumerable().Where(x => x["类型"] != DBNull.Value && x["类型"].ToString().Contains(IMUType)).Count();
                int centerlineTypeCount = CenterlinePointTable.AsEnumerable().Where(x => x["测点属性"] != DBNull.Value && x["测点属性"].ToString().Contains(CenterlineType)).Count();
                if (IMUTypeCount != centerlineTypeCount)
                {
                    MessageBox.Show("内检测点“" + IMUType +"”与中线点“" + CenterlineType +"”的数量不匹配！");
                    return false;
                }
                List<DataRow> NeijianceControlPointList = (from DataRow r in IMUTable.Rows
                                                           where r["类型"] != DBNull.Value && r["类型"].ToString().Contains(IMUType)
                                                           select r).ToList();
                List<DataRow> ZhongXianControlPointList = (from DataRow r in CenterlinePointTable.Rows
                                                           where r["测点属性"] != DBNull.Value && r["测点属性"].ToString().Contains(CenterlineType)
                                                           select r).ToList();
                for (int i = 0; i < NeijianceControlPointList.Count; i++ )
                {
                    DataRow IMURow = NeijianceControlPointList[i];
                    DataRow centerlineRow = ZhongXianControlPointList[i];
                    IMURow["对齐里程"] = centerlineRow[EvConfig.CenterlineMeasureField];
                }
            }
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
            CalculateNullMeasureBasedOnControlpointMeasure(ref IMUTable);
            return true;
        }
        //根据特征点匹配对齐里程计算其他店对齐里程
        private void CalculateNullMeasureBasedOnControlpointMeasure(ref DataTable IMUTable)
        {
            DataRow PrevRowWithM = null;
            for (int i = 0; i < IMUTable.Rows.Count; i++)
            {
                DataRow r = IMUTable.Rows[i];
                if (r["对齐里程"] != DBNull.Value)
                {
                    PrevRowWithM = r;
                }
                else
                {
                    DataRow NextRowWithM = null;
                    for (int j = i + 1; j < IMUTable.Rows.Count; j++)
                    {
                        DataRow r2 = IMUTable.Rows[j];
                        if (r2["对齐里程"] != DBNull.Value)
                        {
                            NextRowWithM = r2;
                            break;
                        }
                    }
                    if (PrevRowWithM == null || NextRowWithM == null)
                    {
                        break;
                    }
                    double BeginJiluM = Convert.ToDouble(PrevRowWithM[EvConfig.IMUMoveDistanceField]);
                    double endJiluM = Convert.ToDouble(NextRowWithM[EvConfig.IMUMoveDistanceField]);
                    double BeginAM = Convert.ToDouble(PrevRowWithM["对齐里程"]);
                    double endAM = Convert.ToDouble(NextRowWithM["对齐里程"]);
                    double currentJiluM = Convert.ToDouble(r[EvConfig.IMUMoveDistanceField]);
                    r["对齐里程"] = (currentJiluM - BeginJiluM) * (endAM - BeginAM) / (endJiluM - BeginJiluM) + BeginAM;
                }
            }
        }

        private bool IsZhongXianDianControlPointType(string pointtype, Dictionary<string, string> TypeMatchDic)
        {
            foreach (string controlpointtype in TypeMatchDic.Keys)
            {
                if (pointtype.Contains(controlpointtype))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsNeiJianCeDianControlPointType(string pointtype, Dictionary<string, string> TypeMatchDic)
        {
            foreach (string controlpointtype in TypeMatchDic.Values)
            {
                if (pointtype.Contains(controlpointtype))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsZhongXianNejianceCouldMatch(string zhongxiandianType, string neijiancedianType, Dictionary<string, string> TypeMatchDic)
        {
            foreach (string TypeKey in TypeMatchDic.Keys)
            {
                if (zhongxiandianType.Contains(TypeKey) && neijiancedianType.Contains(TypeMatchDic[TypeKey]))
                {
                    return true;
                }
            }
            return false;
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

        private void buttonXDIR_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "Excel文件(*.xls) | *.xls";

            OpenFileDialog1.FilterIndex = 2;
            OpenFileDialog1.RestoreDirectory = true;          
            if( OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                textBoxFile.Text = OpenFileDialog1.FileName;
            }

        }
    }
}
