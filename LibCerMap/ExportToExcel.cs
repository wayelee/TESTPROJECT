using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public class ExportToExcel
    {
        public void ExportDataGridViewToExcel(DevComponents.DotNetBar.Controls.DataGridViewX dataGridview1)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl 文件(*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel";
            DateTime now = DateTime.Now;
            saveFileDialog.FileName = "";
            //saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
                string str = "";
                string tempStr = string.Empty;
                try
                {
                    //写标题    
                    for (int i = 0; i < dataGridview1.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t";
                        }
                        str += dataGridview1.Columns[i].HeaderText;
                    }
                    sw.WriteLine(str);
                    //写内容 
                    for (int j = 0; j < dataGridview1.Rows.Count - 1; j++)
                    {
                        for (int k = 0; k < dataGridview1.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += dataGridview1.Rows[j].Cells[k].Value.ToString();

                        }
                        sw.WriteLine(tempStr);
                        tempStr = string.Empty;
                    }
                    sw.Close();
                    myStream.Close();
                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
            //else
            //{
            //    MessageBox.Show("导出失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }
    }
}
