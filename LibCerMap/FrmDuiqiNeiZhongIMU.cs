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

using System.Threading;

namespace LibCerMap
{
    public partial class FrmDuiqiNeiZhongIMU : OfficeForm
    {
        AxMapControl m_pMapCtl;
        public string XMLPath;
        public string gdbPath;
        public string symFilePath;
        AxTOCControl m_pTOCCtl;
        string MapTempPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\MapTemplate";
        public FrmDuiqiNeiZhongIMU(AxMapControl mcontrol, AxTOCControl mtoccontrol)
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
            if (Convert.ToInt32(textBoxX4.Text) > 5)
            {
                MessageBox.Show("拐点距离阈值大于5米，精度低于预期，不能用来进行对齐");
                return;
            }
            if (Convert.ToInt32(textBoxX5.Text) < 15)
            {
                MessageBox.Show("拐点角度阈值低于15度，精度低于预期，不能用来进行对齐");
                return;
            }
            Thread.Sleep(4000);
            ClsGDBDataCommon processDataCommon = new ClsGDBDataCommon();
            string strInputPath = System.IO.Path.GetDirectoryName(textBoxX2.Text);
            string strInputName = System.IO.Path.GetFileName(textBoxX2.Text);

            #region 读取SHP文件
            IWorkspace pWorkspace = processDataCommon.OpenFromShapefile(strInputPath);
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            IFeatureClass pInputFC = pFeatureWorkspace.OpenFeatureClass(strInputName);
            #endregion

            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = pInputFC;
            pFeatureLayer.Name = strInputName;
            m_pMapCtl.AddLayer(pFeatureLayer as ILayer, 0);
            m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

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
                textBoxX2.Text = "C:\\Users\\analyse\\Desktop\\zq\\data\\内检测、中线示例数据-对齐\\对齐\\对齐结果.shp";
            }
        }        
    }
}
