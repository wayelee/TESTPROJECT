using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;

using DevComponents.DotNetBar;
using DevComponents.AdvTree;

namespace LibCerMap
{
    public partial class FrmCoordinateSystem : OfficeForm
    {
        public IMapControl3 pMapControl;
        public IMapControl3 pMapControlSpacial;//用于记录导入图层的空间参考，传入的时主窗体中axMapControlHide
        public ILayer pLayer;//从图层属性位置打开本窗体时记录该图层
        public ISpatialReference pSpaReference = null;
        private string startPosition;

        public FrmCoordinateSystem(IMapControl3 mapCtl,IMapControl3 mapCtlHide)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapControl = mapCtl;
            pMapControlSpacial = mapCtlHide;
            startPosition = "startFromMap";
        }

        public FrmCoordinateSystem(ILayer layer)//从图层属性位置打开这个窗体
        {
            InitializeComponent();
            this.EnableGlass = false;
            pLayer = layer;
            startPosition = "startFromLayer";
        }

        private void FrmCoordinateSystem_Load(object sender, EventArgs e)
        {
            switch (startPosition)
            {
                case "startFromMap":
                    DirectoryInfo CoordDirInfo = new DirectoryInfo(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Coordinate Systems");
                    LoadDirectories(advTreeCoord.Nodes[0], CoordDirInfo);

                    richTextReference.Text = ClsGDBDataCommon.GetReferenceString(pMapControl.SpatialReference);
                    IEngineEditor pEngineEdit = new EngineEditorClass();
                    if (pEngineEdit.Map == pMapControl.Map)
                    {
                        advTreeCoord.Enabled = false;
                        MessageBox.Show("该图层处在编辑状态不能修改坐标系统", "提示", MessageBoxButtons.YesNo);
                    }
                    break;
                case "startFromLayer":
                    DirectoryInfo CoordDirInfoLayer = new DirectoryInfo(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Coordinate Systems");
                    LoadDirectories(advTreeCoord.Nodes[0], CoordDirInfoLayer);
                    IGeoDataset geoDataset = pLayer as IGeoDataset;
                    ISpatialReference reference = geoDataset.SpatialReference;

                    richTextReference.Text = ClsGDBDataCommon.GetReferenceString(reference);
                    break;
            }
        }

        private void LoadDirectories(Node parent, DirectoryInfo directoryInfo)
        {
            
            DirectoryInfo[] directories = directoryInfo.GetDirectories();

            foreach (DirectoryInfo dir in directories)
            {
                Node node = new Node();
                if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;                
                node.Tag = dir;
                node.Text = dir.Name;
                node.Image = global::LibCerMap.Properties.Resources.Folder16;
                node.ImageExpanded = global::LibCerMap.Properties.Resources.FolderOpenState16;
                node.Cells.Add(new Cell());
                node.ExpandVisibility = eNodeExpandVisibility.Visible;
                parent.Nodes.Add(node);

               // LoadDirectories(node, dir);

            }

            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                Node node = new Node();
                node.Text = file.Name;
                node.Image = global::LibCerMap.Properties.Resources.CoordinateSystem16;
                //Cell cell = new Cell(file.Length.ToString("N0"));
                //cell.StyleNormal = _RightAlignFileSizeStyle;
                //node.Cells.Add(cell);
                parent.Nodes.Add(node);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.SystemUI.ICommand command = new ControlsAddDataCommandClass();
            command.OnCreate(pMapControlSpacial);
            command.OnClick();
            //在此添加空间参考的详细信息
            if (pMapControlSpacial.Map!=null)
            {
                for (int i = 0; i < pMapControlSpacial.Map.LayerCount; i++)
                {
                    ILayer pLayer = pMapControlSpacial.Map.get_Layer(i);

                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                        IGeoDataset pGeoDataset = pFeatureLayer as IGeoDataset;
                        ISpatialReference pSpatialReference = pGeoDataset.SpatialReference;

                        richTextReference.Text = ClsGDBDataCommon.GetReferenceString(pSpatialReference);
                        pSpaReference = pSpatialReference;
                    }
                    if (pLayer is IRasterLayer)
                    {
                        IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                        IRaster pRaster = pRasterLayer.Raster;
                        IRasterProps pRasterProps = pRaster as IRasterProps;
                        ISpatialReference pSpatialReference = pRasterProps.SpatialReference;

                        richTextReference.Text = ClsGDBDataCommon.GetReferenceString(pSpatialReference);
                        pSpaReference = pSpatialReference;
                    }
                }
                pMapControlSpacial.Map.ClearLayers();
            } 
        }

        private void advTreeCoord_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (e.Node.Text.Contains(".prj")==true)
            {
                string fileFullPath = "";
                string fileFullName = e.Node.Text;
                int depthIndex = e.Node.Level;
                Node curnode = new Node();
                curnode = e.Node;
                for (int i = depthIndex; i > 1; i--)
                {
                    fileFullName = curnode.Parent.Text + "\\" + fileFullName;
                    curnode = curnode.Parent;
                }
                fileFullPath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Coordinate Systems\" + fileFullName;

                ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                try
                {
                    ISpatialReference pSpatialReference = pSpatialReferenceFactory.CreateESRISpatialReferenceFromPRJFile(fileFullPath);
                    if (pSpatialReference == null)
                        return;
                    pSpaReference = pSpatialReference;

                    richTextReference.Text = ClsGDBDataCommon.GetReferenceString(pSpatialReference);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (startPosition)
            {
                case "startFromMap":
                    pMapControl.SpatialReference = pSpaReference;
                    pMapControl.Refresh();
                    break;
                case "startFromLayer":
                    
                    break;
            }
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void advTreeCoord_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
        {
            Node parent = e.Node;
            if (parent.Nodes.Count > 0) return;

            if (parent.Tag is DriveInfo)
            {
                advTreeCoord.BeginUpdate();
                DriveInfo driveInfo = (DriveInfo)parent.Tag;
                LoadDirectories(parent, driveInfo.RootDirectory);
                parent.ExpandVisibility = eNodeExpandVisibility.Auto;
                advTreeCoord.EndUpdate(true);
            }
            else if (parent.Tag is DirectoryInfo)
            {
                LoadDirectories(parent, (DirectoryInfo)parent.Tag);
            }
        }

        private void btnSelPrjFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PRJ文件(*.prj)|*.prj";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                try
                {
                    ISpatialReference pSpatialReference = pSpatialReferenceFactory.CreateESRISpatialReferenceFromPRJFile(dlg.FileName);
                    if (pSpatialReference == null)
                        return;
                    pSpaReference = pSpatialReference;

                    richTextReference.Text = ClsGDBDataCommon.GetReferenceString(pSpaReference);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ISpatialReference SpaReference = new UnknownCoordinateSystemClass();
            pSpaReference = SpaReference;
            richTextReference.Text = ClsGDBDataCommon.GetReferenceString(SpaReference);
        }

    }
}
