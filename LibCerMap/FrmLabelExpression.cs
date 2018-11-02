using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmLabelExpression : OfficeForm
    {
        DevComponents.DotNetBar.Controls.ComboBoxEx cmbboxexp=null;
        ILayer pLayer = null;
        public FrmLabelExpression(DevComponents.DotNetBar.Controls.ComboBoxEx cmbbox,ILayer layer)
        {
            InitializeComponent();
            this.EnableGlass = false;
            cmbboxexp = cmbbox;
            pLayer = layer;
        }

        private void FrmLabelExpression_Load(object sender, EventArgs e)
        {
            //初始化图层列表
            ITable pTable = (ITable)pLayer;
            IFields pFields = pTable.Fields;
            long nRows = pTable.RowCount(null);
            long nFields = pFields.FieldCount;

            IField pField;
            for (int i = 0; i < nFields; i++)
            {
                pField = pFields.get_Field(i);
                if (pField.Name != "Shape")
                {
                    this.lsbFields.Items.Add(pField.Name);
                }
            }

            if (cmbboxexp.Text.Contains("[") && cmbboxexp.Text.Contains("]"))
            {
                this.InsertRtbText(cmbboxexp.Text);
            }
            else
            {
                this.InsertRtbText("[" + cmbboxexp.Text + "]") ;
            }
        }

        private void lsbFields_DoubleClick(object sender, EventArgs e)
        {
            if (RtbSQL.Text=="")
            {
                this.InsertRtbText("[" + lsbFields.SelectedItem.ToString() + "]");
            }
            else
            {
                this.InsertRtbText("&\"\"&"+"[" + lsbFields.SelectedItem.ToString() + "]");
            }

        }

        private void InsertRtbText(string strText)
        {
            int nStart = RtbSQL.SelectionStart;
            RtbSQL.Text = RtbSQL.Text.Insert(nStart, strText);
            RtbSQL.SelectionStart = nStart +strText.Length;
            RtbSQL.Focus();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            cmbboxexp.Text =RtbSQL.Text;
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
