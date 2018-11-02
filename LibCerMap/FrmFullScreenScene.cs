using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;


namespace LibCerMap
{
    public partial class FrmFullScreenScene : Form
    {
        AxSceneControl pSecenControl = null;
        public FrmFullScreenScene(AxSceneControl SecenControl)
        {
            InitializeComponent();
            pSecenControl = SecenControl;
        }

        private void axSceneControl1_OnKeyUp(object sender, ESRI.ArcGIS.Controls.ISceneControlEvents_OnKeyUpEvent e)
        {
            if (e.keyCode == 27)
            {
                axSceneControl1.Scene.ClearLayers();
                axSceneControl1.Dispose();
                pSecenControl.Refresh();
                pSecenControl.Update();
                this.Close();
            }
        }

        private void FrmFullScreenScene_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pSecenControl.Scene.LayerCount; i++)
            {
                axSceneControl1.Scene.AddLayer(pSecenControl.Scene.get_Layer(i));
                //axSceneControl1.Scene.Extent=pSecenControl.Scene.Extent;
            }            
        }

        private void axSceneControl1_OnMouseDown(object sender, ISceneControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                System.Drawing.Point p = new System.Drawing.Point();
                p.X = e.x;
                p.Y = e.y;
                contextMenuStrip1.Show(axSceneControl1, p);
            }
        }

        private void toolnavagation_Click(object sender, EventArgs e)//导航
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsSceneNavigateToolClass();
            pCommand.OnCreate(this.axSceneControl1.Object);
            this.axSceneControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;
        }

        private void toolzoomin_Click(object sender, EventArgs e)//放大
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsSceneZoomInToolClass();
            pCommand.OnCreate(this.axSceneControl1.Object);
            this.axSceneControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;
        }

        private void toolzoomout_Click(object sender, EventArgs e)//缩小
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsSceneZoomOutToolClass();
            pCommand.OnCreate(this.axSceneControl1.Object);
            this.axSceneControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;
        }

        private void toolpan_Click(object sender, EventArgs e)//平移
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsScenePanToolClass();
            pCommand.OnCreate(this.axSceneControl1.Object);
            this.axSceneControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;
        }

        private void toolextent_Click(object sender, EventArgs e)//全图
        {
            ITool TempTool = this.axSceneControl1.CurrentTool;

            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsSceneFullExtentCommandClass();
            pCommand.OnCreate(this.axSceneControl1.Object);
            this.axSceneControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;
            pCommand.OnClick();

            this.axSceneControl1.CurrentTool = TempTool;
        }

        private void toolfly_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsSceneFlyToolClass();
            pCommand.OnCreate(this.axSceneControl1.Object);
            this.axSceneControl1.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;
        }

    }
}
