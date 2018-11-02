using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibModelGen;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmRandomTexture : OfficeForm
    {
        #region private class member
        private String[] szTextureFilenames = new String[3];
        private int[] nThresholdValues = new int[3] { 85, 170, 255 };
        private Bitmap m_bmpPreview;
        private IRaster m_pRaster;
        private String szOutputFilename;
        #endregion

        #region public class properties
        public String OutputFilename
        {
            get
            {
                return szOutputFilename;
            }
        }
        public IRaster pRaster
        {
            get
            {
                return m_pRaster;
            }

            set
            {
                m_pRaster = value;
            }
        }

        public String[] TextureNames
        {
            get
            {
                return szTextureFilenames;
            }

            set
            {
                szTextureFilenames = value;
            }
        }

        public IMap m_pMap;

        public int[] ThresholdValues
        {
            get
            {
                return nThresholdValues;
            }

            set
            {
                nThresholdValues = value;
            }
        }

        public Bitmap BmpPreview
        {
            get
            {
                return m_bmpPreview;
            }
        }
        #endregion
        public FrmRandomTexture()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void prepiewTexture()
        {
            //preview
            if ((!String.IsNullOrEmpty(szTextureFilenames[0]))
                && (!String.IsNullOrEmpty(szTextureFilenames[1]))
                && (!String.IsNullOrEmpty(szTextureFilenames[2]))
                && (m_pRaster != null))
            {
                double[,] dbData = TextureGen.raster2double(m_pRaster);

                int[] nTempValues = (int[])nThresholdValues.Clone();
                picPreview.Image = TextureGen.textureGen(dbData, szTextureFilenames, nTempValues);
                picPreview.Refresh();
            }
        }

        private void picFirstTexture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "打开纹理贴图路径：";
            fileDialog.InitialDirectory = ".";
            fileDialog.Filter = "图像文件(*.BMP;*.PCX;*.TIFF;*.GIF;*.JPG;*.TGA;*.EXIF)|*.BMP;*.PCX;*.TIFF;*.GIF;*.JPG;*.TGA;*.EXIF|所有文件(*.*)|*.*";
            fileDialog.RestoreDirectory = true;

            //设置对话框属性
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                szTextureFilenames[0] = fileDialog.FileName;
                picFirstTexture.Image = new Bitmap(szTextureFilenames[0]);
            }

            //preview
            prepiewTexture();
        }

        private void picSecondTexture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "打开纹理贴图路径：";
            fileDialog.InitialDirectory = ".";
            fileDialog.Filter = "图像文件(*.BMP;*.PCX;*.TIFF;*.GIF;*.JPG;*.TGA;*.EXIF)|*.BMP;*.PCX;*.TIFF;*.GIF;*.JPG;*.TGA;*.EXIF|所有文件(*.*)|*.*";
            fileDialog.RestoreDirectory = true;

            //设置对话框属性
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                szTextureFilenames[1] = fileDialog.FileName;
                picSecondTexture.Image = new Bitmap(szTextureFilenames[1]);
            }

            //preview
            prepiewTexture();
        }

        private void picThirdTexture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "打开纹理贴图路径：";
            fileDialog.InitialDirectory = ".";
            fileDialog.Filter = "图像文件(*.BMP;*.PCX;*.TIFF;*.GIF;*.JPG;*.TGA;*.EXIF)|*.BMP;*.PCX;*.TIFF;*.GIF;*.JPG;*.TGA;*.EXIF|所有文件(*.*)|*.*";
            fileDialog.RestoreDirectory = true;

            //设置对话框属性
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                szTextureFilenames[2] = fileDialog.FileName;
                picThirdTexture.Image = new Bitmap(szTextureFilenames[2]);
            }

            //preview
            prepiewTexture();
        }

        private void btnOutputFilename_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "打开纹理贴图路径：";
            fileDialog.InitialDirectory = ".";
            fileDialog.Filter = "图像文件(*.TIFF) | *.tif |所有文件(*.*)|*.*";
            fileDialog.RestoreDirectory = true;

            //设置对话框属性
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                szOutputFilename = fileDialog.FileName;
                txtOutputFilename.Text = szOutputFilename;
            }
        }

        private void sldFirstValue_ValueChanged(object sender, EventArgs e)
        {
            int nValue = sldFirstValue.Value;
            if (nThresholdValues[0] == nValue)
            {
                return;
            }

            if (nValue <= nThresholdValues[1])
            {
                nThresholdValues[0] = nValue;
                txtFirstValue.Text = nValue.ToString();
            }
            else
            {
                sldFirstValue.Value = nThresholdValues[0];
            }
        }

        private void sldSecondValue_ValueChanged(object sender, EventArgs e)
        {
            int nValue = sldSecondValue.Value;
            if (nThresholdValues[1] == nValue)
            {
                return;
            }

            if (nValue >= nThresholdValues[0] && nValue <= nThresholdValues[2])
            {
                nThresholdValues[1] = nValue;
                txtSecondValue.Text = nValue.ToString();
            }
            else
            {
                sldSecondValue.Value = nThresholdValues[1];
            }
        }

        private void sldThirdValue_ValueChanged(object sender, EventArgs e)
        {
            int nValue = sldThirdValue.Value;
            if (nThresholdValues[2] == sldThirdValue.Value)
            {
                return;
            }

            if (nValue >= nThresholdValues[1])
            {
                nThresholdValues[2] = nValue;
                txtThirdValue.Text = nValue.ToString();
            }
            else
            {
                sldThirdValue.Value = nThresholdValues[2];
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (String.IsNullOrEmpty(szTextureFilenames[i]))
                {
                    String szTmp = String.Format("请指定第{0}层纹理图!", i + 1);
                    MessageBox.Show(szTmp);
                    return;
                }
            }

            if (String.IsNullOrEmpty(szOutputFilename))
            {
                MessageBox.Show("请指定输出路径！");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void FrmRandomTexture_Load(object sender, EventArgs e)
        {
            IEnumLayer pEnumLayer = m_pMap.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = null;
            while ((pLayer = pEnumLayer.Next()) != null)
            {
                if (pLayer is IRasterLayer || pLayer is ITinLayer)
                {
                    cmbLayers.Items.Add(pLayer.Name);
                }
            }

            if (cmbLayers.Items.Count > 0)
            {
                cmbLayers.SelectedIndex = 0;
            }

            sldFirstValue.Value = nThresholdValues[0];
            sldSecondValue.Value = nThresholdValues[1];
            sldThirdValue.Value = nThresholdValues[2];

            txtFirstValue.Text = nThresholdValues[0].ToString();
            txtSecondValue.Text = nThresholdValues[1].ToString();
            txtThirdValue.Text = nThresholdValues[2].ToString();

            txtOutputFilename.Text = String.Empty;
            for (int i = 0; i < szTextureFilenames.Length; i++)
            {
                szTextureFilenames[i] = String.Empty;
            }
        }

        private void txtFirstValue_TextChanged(object sender, EventArgs e)
        {
            int nValue = int.Parse(txtFirstValue.Text);
            if (nValue == nThresholdValues[0])
            {
                return;
            }

            if (nValue <= nThresholdValues[1])
            {
                nThresholdValues[0] = nValue;
                sldFirstValue.Value = nValue;
            }
            else
            {
                txtFirstValue.Text = nThresholdValues[0].ToString();
            }
        }

        private void txtSecondValue_TextChanged(object sender, EventArgs e)
        {
            int nValue = int.Parse(txtSecondValue.Text);
            if (nValue == nThresholdValues[1])
            {
                return;
            }

            if (nValue >= nThresholdValues[0] && nValue <= nThresholdValues[2])
            {
                nThresholdValues[1] = nValue;
                sldSecondValue.Value = nValue;
            }
            else
            {
                txtSecondValue.Text = nThresholdValues[1].ToString();
            }
        }

        private void txtThirdValue_TextChanged(object sender, EventArgs e)
        {
            int nValue = int.Parse(txtThirdValue.Text);
            if (nValue == nThresholdValues[2])
            {
                return;
            }

            if (nValue >= nThresholdValues[1])
            {
                nThresholdValues[2] = nValue;
                sldThirdValue.Value = nValue;
            }
            else
            {
                txtThirdValue.Text = nThresholdValues[2].ToString();
            }
        }

        private void cmbLayers_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbLayers.SelectedIndex == -1)
            {
                return;
            }
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(m_pMap, cmbLayers.Text);
            if (pLayer is IRasterLayer)
            {
                m_pRaster = ((IRasterLayer)pLayer).Raster;
            }
            else if (pLayer is ITinLayer)
            {
                ITinLayer pTinlayer = null;
                pTinlayer = pLayer as ITinLayer;
                if (pTinlayer != null)
                {
                    //FrmDEMToTin frm = new FrmDEMToTin();
                    ////ITin tin = frm.DEMToTIN(raster);
                    //ITinLayer tinlayer = new TinLayerClass();
                    //tinlayer.Dataset = tin;
                    //tinlayer.Name = "Mem_RandomTinLayer";
                    //// tinlayer.TipText = "RandomTinLayer";                
                    //ITinEdit pTEdit = tin as ITinEdit;
                    FrmTinToDEM frm = new FrmTinToDEM();
                    ITin ptin = pTinlayer.Dataset;
                    IRaster praster = frm.TINToDEM(ptin);
                    m_pRaster = praster;
                }
            }
            else
            {
                m_pRaster = null;
            }

        }

        private void sldThirdValue_MouseUp(object sender, MouseEventArgs e)
        {
            //preview
            prepiewTexture();
        }

        private void sldSecondValue_MouseUp(object sender, MouseEventArgs e)
        {
            //preview
            prepiewTexture();
        }

        private void sldFirstValue_MouseUp(object sender, MouseEventArgs e)
        {
            //preview
            prepiewTexture();
        }
    }
}
