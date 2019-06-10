using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCerMap
{
    public partial class FrmYichangBianHuaFenXi : Form
    {
        public DataTable sourcetable;
        public DevExpress.XtraCharts.ChartControl LengthChart;
        public DevExpress.XtraCharts.ChartControl WidthChart;
        public DevExpress.XtraCharts.ChartControl DepthChart;

        public FrmYichangBianHuaFenXi(DataTable sdt)
        {
            InitializeComponent();
            sourcetable = sdt;
            gridControl1.DataSource = sourcetable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridControl1.Refresh();
            LengthChart = chartControl1;
            WidthChart = chartControl3;
            DepthChart = chartControl2;
            
        }
    }
}
