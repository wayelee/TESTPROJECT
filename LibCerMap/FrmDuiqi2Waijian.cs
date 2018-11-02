using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using stdole;

using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;

using DevComponents.DotNetBar;
using System.IO;

using Microsoft.Office;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;

using System.Threading;

namespace LibCerMap
{
    public partial class FrmDuiqi2Waijian : OfficeForm
    {
        AxMapControl m_pMapCtl;
        public string XMLPath;
        public string gdbPath;
        public string symFilePath;
        AxTOCControl m_pTOCCtl;
        string MapTempPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate";
        public FrmDuiqi2Waijian(AxMapControl mcontrol, AxTOCControl mtoccontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pMapCtl = mcontrol;
            m_pTOCCtl = mtoccontrol;
        }

        private void btnOpenXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "shp文件(*.shp)|*.shp|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLPath = dlg.FileName;
                textBoxX1.Text = XMLPath;
            }
        }

        private void buttonX_ok_Click(object sender, EventArgs e)
        {
            string strInputPath1 = System.IO.Path.GetDirectoryName(textBoxX1.Text);
            string strInputName1 = System.IO.Path.GetFileName(textBoxX2.Text);
            string strInputPath2 = System.IO.Path.GetDirectoryName(textBoxX3.Text);
            string strInputName2 = System.IO.Path.GetFileName(textBoxX3.Text);
            if(checkBoxX1.Checked)
            {
                MessageBox.Show("做矫正对齐，同时生成数据报告");

            }
            else
            {
                MessageBox.Show("不做矫正对齐只生成数据报告");

                ClsGDBDataCommon processDataCommon = new ClsGDBDataCommon();

                #region 读取SHP文件
                IWorkspace pWorkspace = processDataCommon.OpenFromShapefile(strInputPath1);
                IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
                IFeatureClass pInputFC = pFeatureWorkspace.OpenFeatureClass(strInputName1);
                #endregion

                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pInputFC;
                pFeatureLayer.Name = strInputName1;
                m_pMapCtl.AddLayer(pFeatureLayer as ILayer, 0);
                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                #region 读取SHP文件
                IWorkspace pWorkspace2 = processDataCommon.OpenFromShapefile(strInputPath2);
                IFeatureWorkspace pFeatureWorkspace2 = pWorkspace2 as IFeatureWorkspace;
                IFeatureClass pInputFC2 = pFeatureWorkspace2.OpenFeatureClass(strInputName2);
                #endregion

                IFeatureLayer pFeatureLayer2 = new FeatureLayerClass();
                pFeatureLayer2.FeatureClass = pInputFC2;
                pFeatureLayer2.Name = strInputName2;
                m_pMapCtl.AddLayer(pFeatureLayer2 as ILayer, 0);
                m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);               
            }
            

            

            this.Close();            
        }

        private void btnImpSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Title = "Save file";
                fbd.InitialDirectory = System.IO.Path.GetDirectoryName(XMLPath);
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(fbd.FileName + ".shp") == true)
                    {
                        MessageBox.Show("shp已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspaceName workspaceName = workspaceFactory.Create(System.IO.Path.GetDirectoryName(fbd.FileName), System.IO.Path.GetFileName(fbd.FileName), null, 0);
                    gdbPath = fbd.FileName + ".gdb";
                    textBoxX2.Text = fbd.FileName + ".gdb";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonX_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "shp文件(*.shp)|*.shp|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLPath = dlg.FileName;
                textBoxX3.Text = XMLPath;
                string filename = System.IO.Path.GetDirectoryName(XMLPath) + "\\shp\\" + System.IO.Path.GetFileNameWithoutExtension(XMLPath) + ".shp";
                textBoxX2.Text = "C:\\Users\\analyse\\Desktop\\zq\\data\\内检测、中线示例数据-对齐\\对齐\\三桩偏移_外防腐层缺陷点_外检测对齐.shp";
            }
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxX2.Enabled = !checkBoxX1.Checked;
            btnImpSymbol.Enabled = !checkBoxX1.Checked;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            SaveFileDialog fbd = new SaveFileDialog();
            fbd.Title = "Save file";
            fbd.Filter = "word文件(*.doc)|*.doc";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.Directory.Exists(fbd.FileName + ".doc") == true)
                {
                    MessageBox.Show("word已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                textBoxX4.Text = fbd.FileName;
            }
        }        
    }
}
