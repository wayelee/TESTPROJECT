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
    public partial class FrmInsideToInsideAlignment : OfficeForm
    {
        //点图层的投影信息
        ISpatialReference psf = null;

        IMapControl3 pMapcontrol;

        public FrmInsideToInsideAlignment(IMapControl3 mapcontrol)
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
                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                    {
                        comboBoxExCenterlineLayer.Items.Add(pLayer.Name);
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
            comboBoxMeasureField.SelectedIndex = 0;
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
                string pBasePointFileName = cboBoxPointLayer.SelectedItem.ToString();
                string pAlignmentPointnName = comboBoxExCenterlineLayer.SelectedItem.ToString();
                IFeatureLayer pBasePointLayer = null;
                IFeatureLayer pCenterlinePointLayer = null;
                for (int i = 0; i < pMapcontrol.LayerCount; i++)
                {
                    if (pBasePointFileName == pMapcontrol.get_Layer(i).Name)
                    {
                        pBasePointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                    if (pAlignmentPointnName == pMapcontrol.get_Layer(i).Name)
                    {
                        pCenterlinePointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                    }
                }
                IFeatureClass pAlignmentPointFC = pCenterlinePointLayer.FeatureClass;
                IFeatureClass pPointFC = pBasePointLayer.FeatureClass;
                IQueryFilter pQF = null;
                DataTable alignmentPointTable ;
                    
                    
                if(radioButtonLayer.Checked)
                {
                    alignmentPointTable = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pAlignmentPointFC as ITable, pQF);
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
                    alignmentPointTable = ds.Tables[0];
                   if(!alignmentPointTable.Columns.Contains("记录距离"))
                    {
                        MessageBox.Show("'记录距离' 不存在!");
                        return;
                    }
                    alignmentPointTable.Columns[EvConfig.IMUMoveDistanceField].DataType = System.Type.GetType("System.Double");
                }                   
                DataTable baseTable = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pPointFC as ITable, pQF);



                //IMUTable => basetable, Centerlinetable => alignmenttable
                double beginM = (double)numericUpDown1.Value;
                double endM = (double)numericUpDown2.Value;
                

                string baseMeasureColumn = comboBoxBaseMeasureField.SelectedItem.ToString();
                string AlingMeasureColumn = comboBoxMeasureField.SelectedItem.ToString();

                DataView dv = baseTable.DefaultView;
                dv.Sort = baseMeasureColumn + " ASC";
                baseTable = dv.ToTable();
                dv = alignmentPointTable.DefaultView;
                dv.Sort = AlingMeasureColumn + " ASC";
                alignmentPointTable = dv.ToTable();

                double centerlineLength = endM - beginM;
                if (alignmentPointTable.Columns.Contains("里程差"))
                    alignmentPointTable.Columns.Remove("里程差");
                alignmentPointTable.Columns.Add("里程差", System.Type.GetType("System.Double"));
                if (alignmentPointTable.Columns.Contains("对齐里程"))
                    alignmentPointTable.Columns.Remove("对齐里程");
                alignmentPointTable.Columns.Add("对齐里程", System.Type.GetType("System.Double"));

                double endIMUM = Convert.ToDouble(alignmentPointTable.Rows[alignmentPointTable.Rows.Count - 1][AlingMeasureColumn]);
                double beginIMUM = Convert.ToDouble(alignmentPointTable.Rows[0][AlingMeasureColumn]);
                double IMULength = endIMUM - beginIMUM;

                List<DataRow> WantouPointList = (from DataRow r in alignmentPointTable.Rows
                                                 where r["类型"].ToString().Contains("弯头")
                                                 select r).ToList();
                List<DataRow> GuandianPointList = (from DataRow r in baseTable.Rows
                                                   where r["类型"].ToString().Contains("弯头")
                                                   select r).ToList();

                #region //匹配弯头作为特征点
                Dictionary<DataRow, DataRow> MatchedDataRowPair = new Dictionary<DataRow, DataRow>();
                for (int i = 0; i < WantouPointList.Count; i++)
                {
                    DataRow IMUr = WantouPointList[i];
                    double ActionIMUM = (Convert.ToDouble(IMUr[AlingMeasureColumn]) - beginIMUM) * centerlineLength / IMULength + beginM;
                    List<DataRow> Featurerow = (from r in GuandianPointList
                                                where Math.Abs(Convert.ToDouble(r[baseMeasureColumn]) - ActionIMUM) < Convert.ToDouble(numericUpDown3.Value)
                                                select r).OrderBy(x => Math.Abs(Convert.ToDouble(x[baseMeasureColumn]) - ActionIMUM)).ToList();
                    if (Featurerow.Count > 0)
                    {
                        DataRow NearestR = Featurerow[0];
                        //已经匹配
                        if (MatchedDataRowPair.Values.Contains(NearestR))
                        {
                            continue;
                        }
                        if (MatchedDataRowPair.Values.Contains(NearestR) == false)
                        {
                            IMUr["里程差"] = Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM;
                            MatchedDataRowPair.Add(IMUr, NearestR);
                        }
                        else
                        {
                           DataRow mathcedIMUr = (from DataRow k in MatchedDataRowPair.Keys
                                                 where MatchedDataRowPair[k].Equals(NearestR)
                                                 select k).ToList().First();
                           double dis = Math.Abs(Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM);
                           double olddis = Math.Abs(Convert.ToDouble(mathcedIMUr["里程差"]));
                           if (dis < olddis)
                           {
                               MatchedDataRowPair.Remove(mathcedIMUr);
                               IMUr["里程差"] = Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM;
                               MatchedDataRowPair.Add(IMUr, NearestR);
                           }
                           else
                           {
                               continue;
                           }
                        }
                    }
                }
                #endregion

                #region //计算未匹配的点的对齐里程
                foreach (DataRow r in MatchedDataRowPair.Keys)
                {
                    r["对齐里程"] = MatchedDataRowPair[r][baseMeasureColumn];
                }
                //将输入的里程当做最终里程
                if (checkBoxFixInputValue.Checked == true)
                {
                    alignmentPointTable.Rows[0]["对齐里程"] = beginM;
                    alignmentPointTable.Rows[alignmentPointTable.Rows.Count - 1]["对齐里程"] = endM;
                }
                //根据最近的特征点以及距离重算起始点里程
                else
                {
                    var query = (from r in alignmentPointTable.AsEnumerable()
                                   where r["对齐里程"] != DBNull.Value
                                   select r).ToList();
                    if (query.Count > 0)
                    {
                        DataRow r = query[0];
                        beginM = Convert.ToDouble(r["对齐里程"]) -
                            (Convert.ToDouble(r[AlingMeasureColumn]) - Convert.ToDouble(alignmentPointTable.Rows[0][AlingMeasureColumn]));
                        alignmentPointTable.Rows[0]["对齐里程"] = beginM;

                        int idx = alignmentPointTable.Rows.Count - 1;
                        r = query[query.Count - 1];

                        endM = Convert.ToDouble(r["对齐里程"]) +
                            (Convert.ToDouble(alignmentPointTable.Rows[idx][AlingMeasureColumn]) - Convert.ToDouble(r[AlingMeasureColumn]));
                        alignmentPointTable.Rows[idx]["对齐里程"] = endM;
                    }
                 }               

                DataRow PrevRowWithM = null;
                for (int i = 0; i < alignmentPointTable.Rows.Count; i++)
                {
                    DataRow r = alignmentPointTable.Rows[i];
                    if (r["对齐里程"] != DBNull.Value)
                    {
                        PrevRowWithM = r;
                    }
                    else
                    {
                        DataRow NextRowWithM = null;
                        for (int j = i + 1; j < alignmentPointTable.Rows.Count; j++)
                        {
                            DataRow r2 = alignmentPointTable.Rows[j];
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
                        double BeginJiluM = Convert.ToDouble(PrevRowWithM[AlingMeasureColumn]);
                        double endJiluM = Convert.ToDouble(NextRowWithM[AlingMeasureColumn]);
                        double BeginAM = Convert.ToDouble(PrevRowWithM["对齐里程"]);
                        double endAM = Convert.ToDouble(NextRowWithM["对齐里程"]);
                        double currentJiluM = Convert.ToDouble(r[AlingMeasureColumn]);
                        r["对齐里程"] = (currentJiluM - BeginJiluM) * (endAM - BeginAM) / (endJiluM - BeginJiluM) + BeginAM;
                    }
                }
                #endregion


              

                #region //对齐里程后，将焊缝加入特征点继续匹配
              
                alignmentPointTable.Columns.Add("对齐基准点里程", System.Type.GetType("System.Double"));
                alignmentPointTable.Columns.Add("对齐基准点里程差", System.Type.GetType("System.Double"));
                alignmentPointTable.Columns.Add("对齐基准点类型");

                foreach (DataRow IMUr in alignmentPointTable.Rows)
                {
                    IMUr["对齐基准点里程"] = DBNull.Value;
                    IMUr["对齐基准点里程差"]= DBNull.Value;
                    IMUr["对齐基准点类型"] = "";
                }

                int LastMatchedPointsCount = -1;
              //  int CurrentMatchedPointsCount = 0;

                while (true)
                {
                    MatchedDataRowPair.Clear();
                    foreach (DataRow IMUr in alignmentPointTable.Rows)
                    {
                        if (!IMUr["类型"].ToString().Contains("弯头") && !IMUr["类型"].ToString().Contains("环向焊缝") )
                        {
                            continue;
                        }
                        if (IMUr["对齐里程"] == DBNull.Value)
                            continue;
                        double ActionIMUM = Convert.ToDouble(IMUr["对齐里程"]);

                        //List<DataRow> Featurerow = (from DataRow r in baseTable.Rows
                        //                            where Math.Abs(Convert.ToDouble(r[baseMeasureColumn]) - ActionIMUM) < Convert.ToDouble(numericUpDown3.Value) &&
                        //                           ( ( r["类型"].ToString().Contains("弯头") && IMUr["类型"].ToString().Contains("弯头") )  ||
                        //                          ( r["类型"].ToString().Contains("异常") && IMUr["类型"].ToString().Contains("异常") ))
                        //                            select r).OrderBy(x => Math.Abs(Convert.ToDouble(x[baseMeasureColumn]) - ActionIMUM)).ToList();
                        List<DataRow> Featurerow = (from DataRow r in baseTable.Rows
                                                    where Math.Abs(Convert.ToDouble(r[baseMeasureColumn]) - ActionIMUM) < Convert.ToDouble(numericUpDown4.Value) &&
                                                   ((r["类型"].ToString().Contains("弯头") && IMUr["类型"].ToString().Contains("弯头")) ||
                                                  (r["类型"].ToString().Contains("环向焊缝") && IMUr["类型"].ToString().Contains("环向焊缝"))
                                                  )
                                                    select r).OrderBy(x => Math.Abs(Convert.ToDouble(x[baseMeasureColumn]) - ActionIMUM)).ToList();
                        if (Featurerow.Count > 0)
                        {
                            DataRow NearestR = Featurerow[0];
                            if (MatchedDataRowPair.Values.Contains(NearestR))
                            {
                                continue;
                            }
                            if (MatchedDataRowPair.Values.Contains(NearestR) == false)
                            {
                                IMUr["对齐基准点里程差"] = Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM;
                                IMUr["对齐基准点里程"] = NearestR[baseMeasureColumn];
                                IMUr["对齐基准点类型"] = NearestR["类型"];
                                MatchedDataRowPair.Add(IMUr, NearestR);
                            }
                            else
                            {
                                DataRow mathcedIMUr = (from DataRow k in MatchedDataRowPair.Keys
                                                       where MatchedDataRowPair[k].Equals(NearestR)
                                                       select k).ToList().First();
                                double dis = Math.Abs(Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM);
                                double olddis = Math.Abs(Convert.ToDouble(mathcedIMUr["对齐基准点里程差"]));
                                if (dis < olddis)
                                {
                                    MatchedDataRowPair.Remove(mathcedIMUr);
                                    IMUr["对齐基准点里程差"] = Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM;
                                    IMUr["对齐基准点里程"] = NearestR[baseMeasureColumn];
                                    IMUr["对齐基准点类型"] = NearestR["类型"];
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
                #endregion


                    #region // 根据匹配的特征点重算

                    foreach (DataRow r in MatchedDataRowPair.Keys)
                    {
                        r["对齐里程"] = MatchedDataRowPair[r][baseMeasureColumn];
                    }

                    //将输入的里程当做最终里程
                    if (checkBoxFixInputValue.Checked == true)
                    {
                        alignmentPointTable.Rows[0]["对齐里程"] = beginM;
                        alignmentPointTable.Rows[alignmentPointTable.Rows.Count - 1]["对齐里程"] = endM;
                    }
                    //根据最近的特征点以及距离重算起始点里程
                    else
                    {
                        var query = (from r in alignmentPointTable.AsEnumerable()
                                     where r["对齐里程"] != DBNull.Value
                                     select r).ToList();
                        if (query.Count > 0)
                        {
                            DataRow r = query[0];
                            beginM = Convert.ToDouble(r["对齐里程"]) -
                                (Convert.ToDouble(r[AlingMeasureColumn]) - Convert.ToDouble(alignmentPointTable.Rows[0][AlingMeasureColumn]));
                            alignmentPointTable.Rows[0]["对齐里程"] = beginM;

                            int idx = alignmentPointTable.Rows.Count - 1;
                            r = query[query.Count - 1];

                            endM = Convert.ToDouble(r["对齐里程"]) +
                                (Convert.ToDouble(alignmentPointTable.Rows[idx][AlingMeasureColumn]) - Convert.ToDouble(r[AlingMeasureColumn]));
                            alignmentPointTable.Rows[idx]["对齐里程"] = endM;
                        }
                    }

                    //未匹配上的点里程设置为null
                    for (int i = 0; i < alignmentPointTable.Rows.Count; i++)
                    {
                        DataRow r = alignmentPointTable.Rows[i];
                        if (!MatchedDataRowPair.Keys.Contains(r))
                        {
                            r["对齐里程"] = DBNull.Value;
                        }
                    }

                    //将输入的里程当做最终里程
                    if (checkBoxFixInputValue.Checked == true)
                    {
                        alignmentPointTable.Rows[0]["对齐里程"] = beginM;
                        alignmentPointTable.Rows[alignmentPointTable.Rows.Count - 1]["对齐里程"] = endM;
                    }
                    //根据最近的特征点以及距离重算起始点里程
                    else
                    {
                        var query = (from r in alignmentPointTable.AsEnumerable()
                                     where r["对齐里程"] != DBNull.Value
                                     select r).ToList();
                        if (query.Count > 0)
                        {
                            DataRow r = query[0];
                            beginM = Convert.ToDouble(r["对齐里程"]) -
                                (Convert.ToDouble(r[AlingMeasureColumn]) - Convert.ToDouble(alignmentPointTable.Rows[0][AlingMeasureColumn]));
                            alignmentPointTable.Rows[0]["对齐里程"] = beginM;

                            int idx = alignmentPointTable.Rows.Count - 1;
                            r = query[query.Count - 1];

                            endM = Convert.ToDouble(r["对齐里程"]) +
                                (Convert.ToDouble(alignmentPointTable.Rows[idx][AlingMeasureColumn]) - Convert.ToDouble(r[AlingMeasureColumn]));
                            alignmentPointTable.Rows[idx]["对齐里程"] = endM;
                        }
                    }

                    PrevRowWithM = null;
                    for (int i = 0; i < alignmentPointTable.Rows.Count; i++)
                    {
                        DataRow r = alignmentPointTable.Rows[i];
                        if (r["对齐里程"] != DBNull.Value)
                        {
                            PrevRowWithM = r;
                        }
                        else
                        {
                            DataRow NextRowWithM = null;
                            for (int j = i + 1; j < alignmentPointTable.Rows.Count; j++)
                            {
                                DataRow r2 = alignmentPointTable.Rows[j];
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
                            double BeginJiluM = Convert.ToDouble(PrevRowWithM[AlingMeasureColumn]);
                            double endJiluM = Convert.ToDouble(NextRowWithM[AlingMeasureColumn]);
                            double BeginAM = Convert.ToDouble(PrevRowWithM["对齐里程"]);
                            double endAM = Convert.ToDouble(NextRowWithM["对齐里程"]);
                            double currentJiluM = Convert.ToDouble(r[AlingMeasureColumn]);
                            r["对齐里程"] = (currentJiluM - BeginJiluM) * (endAM - BeginAM) / (endJiluM - BeginJiluM) + BeginAM;
                        }
                    }
                  
                }
                //alignmentPointTable.Columns.Remove("对齐基准点里程差");
                #endregion



                #region //匹配异常

                List<DataRow> AlimAnomany = (from DataRow r in alignmentPointTable.Rows
                                                 where r["类型"].ToString().Contains("异常")
                                                 select r).ToList();
                List<DataRow> BaseAnomany = (from DataRow r in baseTable.Rows
                                                   where r["类型"].ToString().Contains("异常")
                                                   select r).ToList();

                alignmentPointTable.Columns.Add("异常匹配里程差", System.Type.GetType("System.Double"));
                Dictionary<DataRow, DataRow> anomalyMatchedDataRowPair = new Dictionary<DataRow, DataRow>();
                //MatchedDataRowPair.Clear();
                for (int i = 0; i < AlimAnomany.Count; i++)
                {
                    DataRow IMUr = AlimAnomany[i];
                    if (IMUr["对齐里程"] == DBNull.Value)
                        continue;
                    double ActionIMUM = Convert.ToDouble(IMUr["对齐里程"]);
                    if (IMUr[EvConfig.IMUAngleField] == DBNull.Value)
                    {
                        continue;
                    }

                    //double AlimomanyAngle = Convert.ToDouble(IMUr[EvConfig.IMUAngleField]);
                    //double AlimomanyAngle = ShizhongFangweiToMinutes(IMUr[EvConfig.IMUAngleField].ToString());
                    //List<DataRow> Featurerow = (from r in BaseAnomany
                    //                            where Math.Abs(Convert.ToDouble(r[baseMeasureColumn]) - ActionIMUM) < Convert.ToDouble(numericUpDown4.Value)
                    //                            && r[EvConfig.IMUAngleField] != DBNull.Value
                    //                            && (Math.Abs(ShizhongFangweiToMinutes(r[EvConfig.IMUAngleField].ToString()) - AlimomanyAngle) < 10 ||
                    //                            720 - Math.Abs(ShizhongFangweiToMinutes(r[EvConfig.IMUAngleField].ToString()) - AlimomanyAngle) < 10)
                    //                            select r).OrderBy(x => Math.Abs(Convert.ToDouble(x[baseMeasureColumn]) - ActionIMUM)).ToList();


                    List<DataRow> Featurerow = (from r in BaseAnomany
                                                where Math.Abs(Convert.ToDouble(r[baseMeasureColumn]) - ActionIMUM) < Convert.ToDouble(numericUpDown4.Value)                                               
                                                select r).OrderBy(x => Math.Abs(Convert.ToDouble(x[baseMeasureColumn]) - ActionIMUM)).ToList();

                    if (Featurerow.Count > 0)
                    {
                        DataRow NearestR = Featurerow[0];
                        if (anomalyMatchedDataRowPair.Values.Contains(NearestR) == false)
                        {
                            IMUr["异常匹配里程差"] = Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM;
                            anomalyMatchedDataRowPair.Add(IMUr, NearestR);
                        }
                        else
                        {
                            DataRow mathcedIMUr = (from DataRow k in anomalyMatchedDataRowPair.Keys
                                                   where anomalyMatchedDataRowPair[k].Equals(NearestR)
                                                   select k).ToList().First();
                            double dis = Math.Abs(Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM);
                            double olddis = Math.Abs(Convert.ToDouble(mathcedIMUr["异常匹配里程差"]));
                            if (dis < olddis)
                            {
                                anomalyMatchedDataRowPair.Remove(mathcedIMUr);
                                IMUr["异常匹配里程差"] = Convert.ToDouble(NearestR[baseMeasureColumn]) - ActionIMUM;
                                anomalyMatchedDataRowPair.Add(IMUr, NearestR);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
                #endregion
                string addcolumn = "壁厚变化";
                if (alignmentPointTable.Columns.Contains(addcolumn))
                {
                    alignmentPointTable.Columns.Remove(addcolumn);
                }
                addcolumn = "长度变化";
                if (alignmentPointTable.Columns.Contains(addcolumn))
                {
                    alignmentPointTable.Columns.Remove(addcolumn);
                }
                addcolumn = "宽度变化";
                if (alignmentPointTable.Columns.Contains(addcolumn))
                {
                    alignmentPointTable.Columns.Remove(addcolumn);
                }
                addcolumn = "角度变化";
                if (alignmentPointTable.Columns.Contains(addcolumn))
                {
                    alignmentPointTable.Columns.Remove(addcolumn);
                }
                addcolumn = "壁厚变化率";
                if (alignmentPointTable.Columns.Contains(addcolumn))
                {
                    alignmentPointTable.Columns.Remove(addcolumn);
                }
                addcolumn = "长度变化率";
                if (alignmentPointTable.Columns.Contains(addcolumn))
                {
                    alignmentPointTable.Columns.Remove(addcolumn);
                }
                addcolumn = "宽度变化率";
                if (alignmentPointTable.Columns.Contains(addcolumn))
                {
                    alignmentPointTable.Columns.Remove(addcolumn);
                }
                int baseYear = 0;
                int alignYear = 0;
                try
                {
                    if (baseTable.Columns.Contains(EvConfig.IMUInspectionYearField))
                    {
                        baseYear = Convert.ToInt16( baseTable.Rows[0][EvConfig.IMUInspectionYearField]);
                    }
                    if (alignmentPointTable.Columns.Contains(EvConfig.IMUInspectionYearField))
                    {
                        alignYear = Convert.ToInt16(alignmentPointTable.Rows[0][EvConfig.IMUInspectionYearField]);
                    }
                }
                catch
                {

                }
               



                alignmentPointTable.Columns.Add("壁厚变化", System.Type.GetType("System.Double"));
                alignmentPointTable.Columns.Add("长度变化", System.Type.GetType("System.Double"));
                alignmentPointTable.Columns.Add("宽度变化", System.Type.GetType("System.Double"));
                alignmentPointTable.Columns.Add("壁厚变化率", System.Type.GetType("System.Double"));
                alignmentPointTable.Columns.Add("长度变化率", System.Type.GetType("System.Double"));
                alignmentPointTable.Columns.Add("宽度变化率", System.Type.GetType("System.Double")); 
                alignmentPointTable.Columns.Add("角度变化", System.Type.GetType("System.Double"));
                #region // 异常增长计算
                for (int i = 0; i < anomalyMatchedDataRowPair.Count; i++)
                {
                    DataRow IMUr = anomalyMatchedDataRowPair.Keys.ElementAt(i);
                    DataRow BaseRow = anomalyMatchedDataRowPair.Values.ElementAt(i);
                    try
                    {
                        if (IMUr[EvConfig.IMUDepthField] != DBNull.Value && BaseRow[EvConfig.IMUDepthField] != DBNull.Value)
                            IMUr["壁厚变化"] = Convert.ToDouble(IMUr[EvConfig.IMUDepthField]) - Convert.ToDouble(BaseRow[EvConfig.IMUDepthField]);
                       
                        if (IMUr[EvConfig.IMULengthField] != DBNull.Value && BaseRow[EvConfig.IMULengthField] != DBNull.Value)
                            IMUr["长度变化"] = Convert.ToDouble(IMUr[EvConfig.IMULengthField]) - Convert.ToDouble(BaseRow[EvConfig.IMULengthField]);

                        if (IMUr[EvConfig.IMUWidththField] != DBNull.Value && BaseRow[EvConfig.IMUWidththField] != DBNull.Value)
                            IMUr["宽度变化"] = Convert.ToDouble(IMUr[EvConfig.IMUWidththField]) - Convert.ToDouble(BaseRow[EvConfig.IMUWidththField]);

                        if (IMUr[EvConfig.IMUAngleField] != DBNull.Value && BaseRow[EvConfig.IMUAngleField] != DBNull.Value)
                        {
                            double AlimomanyAngle = ShizhongFangweiToMinutes(IMUr[EvConfig.IMUAngleField].ToString());
                            IMUr["角度变化"] = Math.Min(Math.Abs(ShizhongFangweiToMinutes(BaseRow[EvConfig.IMUAngleField].ToString()) - AlimomanyAngle),
                            Math.Abs(720 - Math.Abs(ShizhongFangweiToMinutes(BaseRow[EvConfig.IMUAngleField].ToString()) - AlimomanyAngle)));
                        }
                        if (baseYear != 0 && alignYear != 0 && baseYear - alignYear != 0)
                        {
                            IMUr["壁厚变化率"] = Convert.ToDouble(IMUr["壁厚变化"]) / (alignYear - baseYear);
                            IMUr["长度变化率"] = Convert.ToDouble(IMUr["长度变化"]) / (alignYear - baseYear);
                            IMUr["宽度变化率"] = Convert.ToDouble(IMUr["宽度变化"]) / (alignYear - baseYear);
                        }

                    }
                    catch
                    {
                    }
                }



                #endregion
                FrmIMUToIMUAlignmentresult frm = new FrmIMUToIMUAlignmentresult(alignmentPointTable, MatchedDataRowPair);
                //FrmIMUAlignmentresult frm = new FrmIMUAlignmentresult(alignmentPointTable);
                frm.setResultType("两次内检测对齐报告");
                frm.BasePointTable = baseTable;
                frm.AlignmentPointTable = alignmentPointTable;
                frm.InsideToInsideTolerance = Convert.ToDouble(numericUpDown4.Value);
                frm.CenterlinePointTable = baseTable; 
                frm.ShowDialog();

                //frm.InsideCenterlineTolerance = Convert.ToDouble(numericUpDown4.Value);
                //frm.CenterlineLayer = pLinearlayer;
                //frm.CenterlinePointTable = CenterlinePointTable;
                //frm.setResultType("内检测对齐中线报告");
                //frm.ShowDialog();

                   
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void buttonXSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "Excel文件(*.xls) | *.xls";

            OpenFileDialog1.FilterIndex = 2;
            OpenFileDialog1.RestoreDirectory = true;
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFile.Text = OpenFileDialog1.FileName;
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

        private void cboBoxPointLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxBaseMeasureField.Items.Clear();
            
            string pPointFileName = cboBoxPointLayer.SelectedItem.ToString();
            IFeatureLayer pIMUPointLayer = null;           
            for (int i = 0; i < pMapcontrol.LayerCount; i++)
            {
                if (pPointFileName == pMapcontrol.get_Layer(i).Name)
                {
                    pIMUPointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                }
            }            
            IFeatureClass pPointFC = pIMUPointLayer.FeatureClass;
            for (int i = 0; i < pPointFC.Fields.FieldCount; i++ )
            {
                IField fd = pPointFC.Fields.get_Field(i);
               // if (fd.Type == esriFieldType.esriFieldTypeDouble)
                {
                    comboBoxBaseMeasureField.Items.Add(fd.Name);
                }
            }
          
        }
        // format 12:00,  to minute. 0 ~ 720
        private double ShizhongFangweiToMinutes(string timestring)
        {
            DateTime tm ;
            if (DateTime.TryParse(timestring, out tm))
            {
                return tm.Hour * 60.0 + tm.Minute;
            }
            else
            {
                return 0;
            }
            
        }
        private void comboBoxExCenterlineLayer_SelectedIndexChanged(object sender, EventArgs e)
        {

            //comboBoxMeasureField.Items.Clear();

            string pPointFileName = comboBoxExCenterlineLayer.SelectedItem.ToString();
            IFeatureLayer pIMUPointLayer = null;
            for (int i = 0; i < pMapcontrol.LayerCount; i++)
            {
                if (pPointFileName == pMapcontrol.get_Layer(i).Name)
                {
                    pIMUPointLayer = pMapcontrol.get_Layer(i) as IFeatureLayer;
                }
            }
            IFeatureClass pPointFC = pIMUPointLayer.FeatureClass;
            for (int i = 0; i < pPointFC.Fields.FieldCount; i++)
            {
                IField fd = pPointFC.Fields.get_Field(i);
                //    if (fd.Type == esriFieldType.esriFieldTypeDouble)
                {
               //     comboBoxMeasureField.Items.Add(fd.Name);
                }
            }
          
        }

    }
}
