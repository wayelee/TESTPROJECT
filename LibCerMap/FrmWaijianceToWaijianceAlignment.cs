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
    public partial class FrmWaijianceToWaijianceAlignment : OfficeForm
    {
        //点图层的投影信息
        ISpatialReference psf = null;

        IMapControl3 pMapcontrol;

        public FrmWaijianceToWaijianceAlignment(IMapControl3 mapcontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapcontrol = mapcontrol;
        }

        private void FrmPointToLine_Load(object sender, EventArgs e)
        {
            
        }
      
        //添加点图层按钮
        private void btAddPoint_Click(object sender, EventArgs e)
        {
            
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
            string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + this.textBoxFile.Text + ";Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(strCon);
            string sql1 = "select * from [Sheet1$]";
            conn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(sql1, strCon);
            DataSet ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();
             DataTable alignmentPointTable = ds.Tables[0];
            if (!alignmentPointTable.Columns.Contains("对齐里程"))
            {
                MessageBox.Show("对齐外检测表的'对齐里程' 不存在!");
                return;
            }
            alignmentPointTable.Columns["对齐里程"].DataType = System.Type.GetType("System.Double");


            strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + this.textBoxFileBase.Text + ";Extended Properties=Excel 8.0";
            conn = new OleDbConnection(strCon);
            sql1 = "select * from [Sheet1$]";
            conn.Open();
            myCommand = new OleDbDataAdapter(sql1, strCon);
            ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();
            DataTable basetable = ds.Tables[0];

            double tolerance = Convert.ToDouble(numericUpDown1.Value);
            string measurevcol = "对齐里程";
            MatchWaijianceTable(basetable, ref alignmentPointTable, measurevcol, tolerance);

            FrmIMUAlignmentresult frm = new FrmIMUAlignmentresult(alignmentPointTable);
            frm.setResultType("两次外检测对齐报告");
            frm.BasePointTable = basetable;
            frm.AlignmentPointTable = alignmentPointTable;
            frm.ShowDialog();
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
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "Excel文件(*.xls) | *.xls";            
            OpenFileDialog1.FilterIndex = 2;
            OpenFileDialog1.RestoreDirectory = true;
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFileBase.Text = OpenFileDialog1.FileName;
            }
        }

        private void MatchWaijianceTable(DataTable basetable, ref DataTable inputtable, string MeasurecolumnName, double tolorance = 0.1)
        {
            basetable.DefaultView.Sort = MeasurecolumnName + " asc";
            basetable = basetable.DefaultView.ToTable();
            inputtable.DefaultView.Sort = MeasurecolumnName + " asc";
            inputtable = inputtable.DefaultView.ToTable();

            string matchflagncolumnName = "MatchFlag";
            if (!basetable.Columns.Contains(matchflagncolumnName))
            {
                basetable.Columns.Add(matchflagncolumnName);
            }

            if (!inputtable.Columns.Contains(matchflagncolumnName))
            {
                inputtable.Columns.Add(matchflagncolumnName);
            }

            if (!inputtable.Columns.Contains("匹配里程"))
            {
                inputtable.Columns.Add("匹配里程");
            }
            // for avoid crossing match
            DataRow lastmatchedBaserow = basetable.Rows[0];
            foreach (DataRow inputr in inputtable.Rows)
            {
                List<DataRow> CandidateBaseRows = (from DataRow r in basetable.AsEnumerable()
                                                   where Math.Abs(Convert.ToDouble(r[MeasurecolumnName]) - Convert.ToDouble(inputr[MeasurecolumnName])) < tolorance &&
                                                         r[matchflagncolumnName] == DBNull.Value &&
                                                       Convert.ToDouble(r[MeasurecolumnName]) >= Convert.ToDouble(lastmatchedBaserow[MeasurecolumnName])
                                                   select r).OrderBy(x => Math.Abs(Convert.ToDouble(x[MeasurecolumnName]) - Convert.ToDouble(inputr[MeasurecolumnName]))).ToList();

                foreach (DataRow baserow in CandidateBaseRows)
                {
                    List<DataRow> matchedinputRows = (from DataRow r in inputtable.AsEnumerable()
                                                      where Math.Abs(Convert.ToDouble(r[MeasurecolumnName]) - Convert.ToDouble(baserow[MeasurecolumnName])) < tolorance &&
                                                            r[matchflagncolumnName] == DBNull.Value
                                                      select r).OrderBy(x => Math.Abs(Convert.ToDouble(x[MeasurecolumnName]) - Convert.ToDouble(baserow[MeasurecolumnName]))).ToList();

                    DataRow closestrow = matchedinputRows[0];
                    if (Math.Abs(Convert.ToDouble(closestrow[MeasurecolumnName]) - Convert.ToDouble(baserow[MeasurecolumnName])) <
                        Math.Abs(Convert.ToDouble(baserow[MeasurecolumnName]) - Convert.ToDouble(inputr[MeasurecolumnName])))
                    {
                        continue;
                    }
                    else
                    {
                        inputr[matchflagncolumnName] = "Y";
                        inputr["匹配里程"] = baserow[MeasurecolumnName];
                        baserow[matchflagncolumnName] = "Y";
                        lastmatchedBaserow = baserow;

                        if (inputtable.Columns.Contains(EvConfig.CISGuanDingMaiShen) && inputtable.Columns.Contains(EvConfig.CISGuanDingMaiShen))
                        {
                            string newcolmen = EvConfig.CISGuanDingMaiShen + "变化";
                            if (inputtable.Columns.Contains(newcolmen) == false)
                            {
                                inputtable.Columns.Add(newcolmen);
                            }
                                try
                                {
                                    inputr[newcolmen] = Convert.ToDouble( inputr[EvConfig.CISGuanDingMaiShen]) - Convert.ToDouble( baserow[EvConfig.CISGuanDingMaiShen]);
                                }
                                catch
                                {
                                }
                            
                        }

                        if (inputtable.Columns.Contains(EvConfig.CISVOn) && inputtable.Columns.Contains(EvConfig.CISVOn))
                        {
                            string newcolmen = EvConfig.CISVOn + "变化";
                            if (inputtable.Columns.Contains(newcolmen) == false)
                            {
                                inputtable.Columns.Add(newcolmen);
                            }
                                try
                                {
                                    inputr[newcolmen] = Convert.ToDouble(inputr[EvConfig.CISVOn]) - Convert.ToDouble(baserow[EvConfig.CISVOn]);
                                }
                                catch
                                {
                                }
                           
                        }
                        if (inputtable.Columns.Contains(EvConfig.CISVOff) && inputtable.Columns.Contains(EvConfig.CISVOff))
                        {
                            string newcolmen = EvConfig.CISVOff + "变化";
                            if (inputtable.Columns.Contains(newcolmen) == false)
                            {
                                inputtable.Columns.Add(newcolmen);
                            }
                                try
                                {
                                    inputr[newcolmen] = Convert.ToDouble(inputr[EvConfig.CISVOff]) - Convert.ToDouble(baserow[EvConfig.CISVOff]);
                                }
                                catch
                                {
                                }
                            
                        }
                        if (inputtable.Columns.Contains(EvConfig.CISdb) && inputtable.Columns.Contains(EvConfig.CISdb))
                        {
                            string newcolmen = EvConfig.CISdb + "变化";
                            if (inputtable.Columns.Contains(newcolmen) == false)
                            {
                                inputtable.Columns.Add(newcolmen);
                            }
                                try
                                {
                                    inputr[newcolmen] = Convert.ToDouble(inputr[EvConfig.CISdb]) - Convert.ToDouble(baserow[EvConfig.CISdb]);
                                }
                                catch
                                {
                                }
                           
                        }
                        if (inputtable.Columns.Contains(EvConfig.CISJiaoliudianya) && inputtable.Columns.Contains(EvConfig.CISJiaoliudianya))
                        {
                            string newcolmen = EvConfig.CISJiaoliudianya + "变化";
                            if (inputtable.Columns.Contains(newcolmen) == false)
                            {
                                inputtable.Columns.Add(newcolmen);
                            }
                                try
                                {
                                    inputr[newcolmen] = Convert.ToDouble(inputr[EvConfig.CISJiaoliudianya]) - Convert.ToDouble(baserow[EvConfig.CISJiaoliudianya]);
                                }
                                catch
                                {
                                }
                            
                        }

                        break;
                    }
                }
            }
            basetable.Columns.Remove("MatchFlag");
            inputtable.Columns.Remove("MatchFlag");

           

        }

    }
}
