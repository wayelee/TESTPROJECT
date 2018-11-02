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
    public partial class FrmParallelLineOffset : OfficeForm
    {
      //  public int SelectedBandIdx = -1;
        public  double  offset;       
        public FrmParallelLineOffset()
        {
            InitializeComponent();            
        }
      
        private void FrmCreateRasterBandLayer_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
            offset = Convert.ToDouble(numOffset.Value);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            offset = Convert.ToDouble(numOffset.Value);
        }
    }
}
