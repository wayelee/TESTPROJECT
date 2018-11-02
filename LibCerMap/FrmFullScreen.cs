using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmFullScreen : Form
    {
        AxMapControl pMapcontrol = null;
        //IMapControl3 pMapcontrol = null;
        public FrmFullScreen(AxMapControl mapcomtrol)
        {
            InitializeComponent();
            pMapcontrol = mapcomtrol;
        }

        private void FrmFullScreen_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pMapcontrol.LayerCount; i++)
            {
                axMapControl1.Map.AddLayer(pMapcontrol.get_Layer(i));
                axMapControl1.Extent = pMapcontrol.Extent;
            }
        }

        private void axMapControl1_OnKeyUp(object sender, IMapControlEvents2_OnKeyUpEvent e)
        {
            if (e.keyCode==27)
            {
                axMapControl1.Map.ClearLayers();
                axMapControl1.Dispose();
                this.Close();
                
            }
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                System.Drawing.Point p = new System.Drawing.Point();
                p.X = e.x;
                p.Y = e.y;
                contextMenuStrip2.Show(axMapControl1, p);
            }
        }
        private void menuMapPopupSelect_Click(object sender, EventArgs e)//选择
        {
            this.axMapControl1.CurrentTool = null;
            this.Cursor = Cursors.Arrow;
        }

        private void toolzoomin_Click(object sender, EventArgs e)//放大
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapZoomInToolClass();
            pCommand.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;
        }

        private void toolzoomout_Click(object sender, EventArgs e)//缩小
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapZoomOutToolClass();
            pCommand.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;
        }

        private void toolpan_Click(object sender, EventArgs e)//漫游
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapPanToolClass();
            pCommand.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;

        }

        private void toolextent_Click(object sender, EventArgs e)//全图
        {
            ITool TempTool = this.axMapControl1.CurrentTool;
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapFullExtentCommandClass();
            pCommand.OnCreate(this.axMapControl1.Object);
            pCommand.OnClick();
            double size = this.axMapControl1.MapScale;

            this.axMapControl1.CurrentTool = TempTool;
        }

        private void toolzoominfixed_Click(object sender, EventArgs e)//中心放大
        {
            ITool TempTool = this.axMapControl1.CurrentTool;

            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapZoomInFixedCommandClass();
            pCommand.OnCreate(this.axMapControl1.Object);
            pCommand.OnClick();

            this.axMapControl1.CurrentTool = TempTool;
        }

        private void toolzoomoutfixed_Click(object sender, EventArgs e)//中心缩小
        {
            ITool TempTool = this.axMapControl1.CurrentTool;

            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapZoomOutFixedCommandClass();
            pCommand.OnCreate(this.axMapControl1.Object);
            pCommand.OnClick();

            this.axMapControl1.CurrentTool = TempTool;
        }

        private void toolpreview_Click(object sender, EventArgs e)//前一视图
        {
            IExtentStack viewStack = null;
            viewStack = axMapControl1.ActiveView.ExtentStack;
            if (viewStack.CanUndo())
            {
                viewStack.Undo();
                axMapControl1.Refresh();
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                MessageBox.Show("这已是最前视图", "提示", MessageBoxButtons.OK);
            }
        }

        private void toolnextview_Click(object sender, EventArgs e)//后一视图
        {
            IExtentStack viewStack = null;
            viewStack = axMapControl1.ActiveView.ExtentStack;
            if (viewStack.CanRedo())
            {
                viewStack.Redo();
                axMapControl1.Refresh();
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                MessageBox.Show("这已是最后视图", "提示", MessageBoxButtons.OK);
            }
        }
    }
}
