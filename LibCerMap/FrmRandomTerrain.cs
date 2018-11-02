using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibModelGen;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmRandomTerrain : OfficeForm
    {
#region private class member
        private double m_dbResolution;
        private double m_dbUndulation;
        private Point2D[] m_ptRange = new Point2D[2];
        private String m_szOutputFilename;
#endregion

#region public class properties
        public double Resolution
        {
            get
            {
                return m_dbResolution;
            }

            set
            {
                m_dbResolution = value;
            }
        }

        public double Undulation
        {
            get
            {
                return m_dbUndulation;
            }

            set
            {
                m_dbUndulation = value;
            }
        }

        public Point2D[] PtTerrainRange
        {
            get
            {
                return m_ptRange;
            }

            set
            {
                m_ptRange = value;
            }
        }

        public String OutputFilename
        {
            get
            {
                return m_szOutputFilename;
            }

            set
            {
                m_szOutputFilename = value;
            }
        }

        public int CraterNum  ;

        public int StoneNum ;
#endregion
      
        public IMap m_pMap;
        public FrmRandomTerrain()
        {
            InitializeComponent();
            this.EnableGlass = false;
            CraterNum = sldCraterCount.Value;
            StoneNum = sldRockCount.Value;
        }

        private void iniUndulation_ValueChanged(object sender, EventArgs e)
        {
            if (sldUndulation.Value != iniUndulation.Value)
            {
                sldUndulation.Value = iniUndulation.Value;
            }
        }

        private void sldUndulation_ValueChanged(object sender, EventArgs e)
        {
            if (iniUndulation.Value != sldUndulation.Value)
            {
                iniUndulation.Value = sldUndulation.Value;
            }
        }

        private void chkManualInput_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManualInput.CheckState == CheckState.Checked)
            {
                dbiLeftTopX.Enabled = true;
                dbiLeftTopY.Enabled = true;
                dbiRightBottomX.Enabled = true;
                dbiRightBottomY.Enabled = true;
            }
            else
            {
                dbiLeftTopX.Enabled = false;
                dbiLeftTopY.Enabled = false;
                dbiRightBottomX.Enabled = false;
                dbiRightBottomY.Enabled = false;
            }
        }

        private void chkReferToLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReferToLayer.CheckState == CheckState.Checked)
            {
                btnReferToLayer.Enabled = true;
                comboBoxExLayer.Enabled = true;
            }
            else
            {
                btnReferToLayer.Enabled = false;
                comboBoxExLayer.Enabled = false;
                comboBoxExLayer.SelectedIndex = -1;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtOutputFilename.Text))
            {
                MessageBox.Show("请选择文件保存路径！");
                return;
            }

            if (chkReferToLayer.CheckState == CheckState.Checked)
            {
                if (comboBoxExLayer.SelectedIndex ==-1)
                {
                    MessageBox.Show("请选择参考图层！");
                    return;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(dbiLeftTopX.Text) 
                    || String.IsNullOrEmpty(dbiLeftTopY.Text) 
                    || String.IsNullOrEmpty(dbiRightBottomX.Text) 
                    || String.IsNullOrEmpty(dbiRightBottomY.Text))
                {
                    MessageBox.Show("请确定地形范围！");
                    return;
                }
            }

            if (chkManualInput.CheckState == CheckState.Checked)
            {
                m_ptRange[0]=new Point2D(dbiLeftTopX.Value, dbiLeftTopY.Value);
                m_ptRange[1]=new Point2D(dbiRightBottomX.Value, dbiRightBottomY.Value);
            }
            else
            {
                //从图层获取相应的地理坐标范围
            }

            double dbDistanceX=m_ptRange[0].X-m_ptRange[1].X;
            double dbDistanceY=m_ptRange[0].Y-m_ptRange[1].Y;
            double dbDistance=Math.Sqrt(dbDistanceX *dbDistanceX + dbDistanceY *dbDistanceY);
            m_dbUndulation = iniUndulation.Value / 50f;// / 2;
            m_dbResolution = dbiResolution.Value;
            m_szOutputFilename = txtOutputFilename.Text;

            this.DialogResult=DialogResult.OK;
        }

        private void btnOutputFilename_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "保存路径：";
            fileDialog.InitialDirectory = ".";
            fileDialog.Filter = "图像文件(*.TIFF;*.tiff;*.TIF;*.tif)|*.TIFF;*.TIF;*.tiff;*.tif;|所有文件(*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            fileDialog.DefaultExt = "tif";

            //设置对话框属性
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                m_szOutputFilename = fileDialog.FileName;
                txtOutputFilename.Text = m_szOutputFilename;
            }
        }

        private void FrmRandomTerrain_Load(object sender, EventArgs e)
        {
            dbiLeftTopX.Value = 0.0;
            dbiLeftTopY.Value = 0.0;
            dbiRightBottomX.Value = 100.0;
            dbiRightBottomY.Value = 100.0;
            iniUndulation.Value = 50;
            dbiResolution.Value = 0.2;

            //初始化图层列表
            if (m_pMap != null)
            {
                IEnumLayer pEnumLayer = m_pMap.get_Layers(null, true);
                pEnumLayer.Reset();
                ILayer pLayer = null;
                while ((pLayer = pEnumLayer.Next()) != null)
                {
                    comboBoxExLayer.Items.Add(pLayer.Name);
                }
                comboBoxExLayer.Enabled = false;
            }
        }

        private void comboBoxExLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxExLayer.SelectedIndex == -1)
            {
                return;
            }
            ILayer pLayer = m_pMap.get_Layer(comboBoxExLayer.SelectedIndex);

            IGeoDataset pGeoDataset = pLayer as IGeoDataset;

            if (pGeoDataset != null)
            {
                dbiLeftTopX.Value = pGeoDataset.Extent.UpperLeft.X;
                dbiLeftTopY.Value = pGeoDataset.Extent.UpperLeft.Y;
                dbiRightBottomX.Value = pGeoDataset.Extent.LowerRight.X;
                dbiRightBottomY.Value = pGeoDataset.Extent.LowerRight.Y;
            }
            else
            {
                MessageBox.Show("无法获取图层范围！");
            }
        }

        private void txtRockCount_ValueChanged(object sender, EventArgs e)
        {
            StoneNum = txtRockCount.Value;
            sldRockCount.Value = txtRockCount.Value;
        }

        private void txtCraterCount_ValueChanged(object sender, EventArgs e)
        {
            CraterNum = txtCraterCount.Value;
            sldCraterCount.Value = txtCraterCount.Value;
        }

        private void sldRockCount_ValueChanged(object sender, EventArgs e)
        {
            if (txtRockCount.Value != sldRockCount.Value)
            {
                txtRockCount.Value = sldRockCount.Value;
            }
        }

        private void sldCraterCount_ValueChanged(object sender, EventArgs e)
        {
            if (txtCraterCount.Value != sldCraterCount.Value)
            {
                txtCraterCount.Value = sldCraterCount.Value;
            }
        }

        private void dbiResolution_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
