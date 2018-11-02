using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmSetSQL : OfficeForm
    {
        public string strSQL;
        public FrmSetSQL()
        {
            InitializeComponent();
        }

        private void FrmSetSQL_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
            lsbFields.Items.Add("FileName");
            lsbFields.Items.Add("FilePath");
            lsbFields.Items.Add("ReceiveTime");
            lsbFields.Items.Add("TaskName");
            lsbFields.Items.Add("ObjName");
            lsbFields.Items.Add("TaskCode");
            lsbFields.Items.Add("ObjCode");
            lsbFields.Items.Add("FileType");
            lsbFields.Items.Add("StandID");
            lsbFields.Items.Add("BJTime");
            lsbFields.Items.Add("TaskTime");
            lsbFields.Items.Add("PrtFileName");
            lsbFields.Items.Add("PrtSeqID");
            lsbFields.Items.Add("ReMark");
        }

        private void InsertRtbText(string strText)
        {
            int nStart = RtbSQL.SelectionStart;
            RtbSQL.Text = RtbSQL.Text.Insert(nStart, strText);
            RtbSQL.SelectionStart = nStart + strText.Length;
            RtbSQL.Focus();
        }

        private void BtnEqual_Click(object sender, EventArgs e)
        {
            InsertRtbText("=");
        }

        private void BtnBig_Click(object sender, EventArgs e)
        {
            InsertRtbText(">");
        }

        private void BtnSmall_Click(object sender, EventArgs e)
        {
            InsertRtbText("<");
        }

        private void BtnNotEqual_Click(object sender, EventArgs e)
        {
            InsertRtbText("<>");
        }

        private void BtnBigEqual_Click(object sender, EventArgs e)
        {
            InsertRtbText(">=");
        }

        private void BtnSmallEqual_Click(object sender, EventArgs e)
        {
            InsertRtbText("<=");
        }

        private void BtnLeftBracket_Click(object sender, EventArgs e)
        {
            InsertRtbText("(");
        }

        private void BtnRightBracket_Click(object sender, EventArgs e)
        {
            InsertRtbText(")");
        }

        private void BtnLike_Click(object sender, EventArgs e)
        {
            InsertRtbText(" LIKE ");
        }

        private void BtnAnd_Click(object sender, EventArgs e)
        {
            InsertRtbText(" AND ");
        }

        private void BtnOr_Click(object sender, EventArgs e)
        {
            InsertRtbText(" OR ");
        }

        private void BtnNot_Click(object sender, EventArgs e)
        {
            InsertRtbText(" NOT ");
        }

        private void BtnIs_Click(object sender, EventArgs e)
        {
            InsertRtbText(" IS ");
        }

        private void BtnUniqeValue_Click(object sender, EventArgs e)
        {
            this.lsbUniqeValue.Items.Clear();
            if (this.lsbFields.SelectedIndex > -1)
            {
                try
                {
                    string strFld = this.lsbFields.SelectedItem.ToString();
                    string sqlstring = "select distinct [" + strFld + "] from FileDB";
                    OleDbSQL cnn = new OleDbSQL();
                    DataSet cds=cnn.dataAdapter(sqlstring, cnn.connection(cnn.connectionPath()));                    
                    DataTable dt = cds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Columns.Count > 0)
                        {
                            this.lsbUniqeValue.Items.Add(dt.Rows[i][0].ToString());
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void lsbFields_DoubleClick(object sender, EventArgs e)
        {
            this.InsertRtbText(lsbFields.SelectedItem.ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            RtbSQL.Clear();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            strSQL = RtbSQL.Text;
            this.Close();
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lsbUniqeValue_DoubleClick(object sender, EventArgs e)
        {
            this.InsertRtbText("'" + lsbUniqeValue.SelectedItem.ToString() + "'");
        }

      
    }
}
