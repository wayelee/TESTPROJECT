using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace LibCerMap
{
    public class OleDbSQL
    {
        string reportPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Database\FileDB.mdb";
        /// <summary>
        /// 设置连接路径
        /// </summary>
        /// <returns></returns>
        public string connectionPath()
        {
            string Path = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + reportPath;

            return Path;
        }
        /// <summary>
        /// 设置连接并打开
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public OleDbConnection connection(string str)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(str);
                conn.Open();
                return conn;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 设置command执行非查询操作，如包括增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public OleDbCommand command(string sql, OleDbConnection con)
        {
            OleDbCommand cmd = new OleDbCommand(sql, con);
            cmd.ExecuteNonQuery(); 
            con.Close();
            return cmd;
        }
        /// <summary>
        /// 设置commandReader查询操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public OleDbDataReader commandRead(string sql, OleDbConnection con)
        {
            OleDbDataReader dr;
            OleDbCommand cmd = new OleDbCommand(sql, con);
            dr = cmd.ExecuteReader();          
            return dr;
        }
        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public DataSet dataAdapter(string sql, OleDbConnection con)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();////
                return ds;
            }
            catch (System.Exception ex)
            {
                if (ex.ToString().Contains("SQL语句") || ex.ToString().Contains("语法错误"))
                {
                    MessageBox.Show("SQL语句错误","提示",MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
                return null;
            }
        }
    }
}
