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
    public partial class FrmSaveRasterTransformation : OfficeForm
    {
        public string savefilepath;
        public int InterationType;
        public double cellsize;
        public FrmSaveRasterTransformation()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void buttonXPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.tif)|*.tif";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxXPath.Text = dlg.FileName;
            }
        }

        private void textBoxXPath_TextChanged(object sender, EventArgs e)
        {
            savefilepath = textBoxXPath.Text;
        }

        private void FrmSaveRasterTransformation_Load(object sender, EventArgs e)
        {
            comboBoxExType.SelectedIndex = 0;
        }

        private void comboBoxExType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InterationType = comboBoxExType.SelectedIndex;
        }

        private void doubleInputCellSize_ValueChanged(object sender, EventArgs e)
        {
            cellsize = doubleInputCellSize.Value;
            
        }
        public void SetCellSize(double val)
        {
            try
            {
                doubleInputCellSize.Value = val;
            }
            catch
            {
            }
        }
        
    }
}
