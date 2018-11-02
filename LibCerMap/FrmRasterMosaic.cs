using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;


namespace LibCerMap
{
    public partial class FrmRasterMosaic : Office2007Form
    {
        public FrmRasterMosaic()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "(*.tif;*.tiff;|*.tif;*.tiff;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dlg.FileNames.Length; i++)
                {
                    if (listBox1.Items.Contains(dlg.FileNames[i]) == false)
                    {
                        listBox1.Items.Add(dlg.FileNames[i]);
                    }
                }
            }
        }

        private void FrmRasterMosaic_Load(object sender, EventArgs e)
        {
            cmbox_operatortype.Items.Add("叠置区域的输出像元值为镶嵌到该位置的第一个栅格数据集中的值");
            cmbox_operatortype.Items.Add("叠置区域的输出像元值为镶嵌到该位置的最后一个栅格数据集中的值");
            cmbox_operatortype.Items.Add("重叠区域的输出像元值为叠置像元的最小值");
            cmbox_operatortype.Items.Add("重叠区域的输出像元值为叠置像元的最大值");
            cmbox_operatortype.Items.Add("重叠区域的输出像元值为叠置像元的平均值");
            cmbox_operatortype.Items.Add("叠置区域的输出像元值为叠置区域中各像元值的水平加权计算结果");
            cmbox_operatortype.SelectedIndex = 1;
            comboBox_colormap.Items.Add("列表中第一个栅格数据集中的色彩映射表将应用于输出栅格镶嵌");
            comboBox_colormap.Items.Add("列表中最后一个栅格数据集中的色彩映射表将应用于输出栅格镶嵌");
            comboBox_colormap.Items.Add("镶嵌时会考虑所有色彩映射表。尝试与具有最接近的可用色彩的值进行匹配");
            comboBox_colormap.Items.Add("仅对那些不包含关联色彩映射表的栅格数据集进行镶嵌");
            comboBox_colormap.SelectedIndex = 0;

        }

        private void buttonX_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX_sub_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection sic = listBox1.SelectedIndices;//得到选择的Item的下标
            if (sic.Count == 0)
            {
                return;
            }
            else
            {
                List<int> list = new List<int>();
                for (int i = 0; i < sic.Count; i++)
                {
                    list.Add(sic[i]);
                }

                list.Sort();
                while (list.Count != 0)
                {
                    listBox1.Items.RemoveAt(list[list.Count - 1]);
                    list.RemoveAt(list.Count - 1);
                }
            }
        }


        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            try
            {
                IRasterCollection pRasterCollection = new MosaicRasterClass();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    IRasterLayer prasterlayer = new RasterLayerClass();
                    prasterlayer.CreateFromFilePath(listBox1.Items[i].ToString());
                    pRasterCollection.Append(prasterlayer.Raster);
                }
                IMosaicRaster pMosaicRaster = pRasterCollection as IMosaicRaster;
                switch(cmbox_operatortype.SelectedIndex)
                {
                    case 0:
                         pMosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_FIRST;
                        break;
                    case 1:
                         pMosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_LAST;
                        break;
                    case 2:
                         pMosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_MIN;
                        break;
                    case 3:
                         pMosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_MAX;
                        break;
                   case 4:
                         pMosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_MEAN;
                        break;
                    case 5:
                         pMosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_BLEND;
                        break;
                    default:
                        pMosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_LAST;
                        break;
                }
                switch (comboBox_colormap.SelectedIndex)
                {
                    case 0:
                        pMosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_FIRST;
                        break;
                    case 1:
                        pMosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_LAST;
                        break;
                    case 2:
                        pMosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_MATCH;
                        break;
                    case 3:
                        pMosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_REJECT;
                        break;
                    default:
                        pMosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_FIRST;
                        break;
                }
                if (string.IsNullOrEmpty(textBoxX_output.Text) == true)
                    return;
                //如果直接保存为img影像文件
                IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(textBoxX_output.Text), 0);
                ISaveAs pSaveAs = pMosaicRaster as ISaveAs;
                IDataset pDataset = pSaveAs.SaveAs(System.IO.Path.GetFileNameWithoutExtension(textBoxX_output.Text) + ".tif", pWorkspace, "TIFF");//以TIF格式保存
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdlg = new SaveFileDialog();
            if (fdlg.ShowDialog() == DialogResult.OK && fdlg.FileName != "")
            {
                textBoxX_output.Text = fdlg.FileName;
            }
        }

        private void buttonX_up_Click(object sender, EventArgs e)
        {
            try
            {
                ListBox.SelectedIndexCollection sic = listBox1.SelectedIndices;//得到选择的Item的下标
                if (sic.Count == 0)
                {
                    return;
                }
                else
                {
                    List<int> list = new List<int>();
                    for (int i = 0; i < sic.Count; i++)
                    {
                        list.Add(sic[i]);
                    }
                    list.Sort();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] > 0)
                        {
                            string temp = listBox1.Items[list[i] - 1].ToString();
                            listBox1.Items[list[i]-1] = listBox1.Items[list[i]].ToString();
                            listBox1.Items[list[i]] = temp;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonX_down_Click(object sender, EventArgs e)
        {
            try
            {
                ListBox.SelectedIndexCollection sic = listBox1.SelectedIndices;//得到选择的Item的下标
                if (sic.Count == 0)
                {
                    return;
                }
                else
                {
                    List<int> list = new List<int>();
                    for (int i = 0; i < sic.Count; i++)
                    {
                        list.Add(sic[i]);
                    }
                    list.Sort();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] >= 0 && list[i] != listBox1.Items.Count - 1)
                        {
                            string temp = listBox1.Items[list[i] + 1].ToString();
                            listBox1.Items[list[i]+1] = listBox1.Items[list[i]].ToString();
                            listBox1.Items[list[i]] = temp;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
