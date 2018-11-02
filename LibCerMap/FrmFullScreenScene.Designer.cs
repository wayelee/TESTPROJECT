namespace LibCerMap
{
    partial class FrmFullScreenScene
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFullScreenScene));
            this.axSceneControl1 = new ESRI.ArcGIS.Controls.AxSceneControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolnavagation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoomin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoomout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolpan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolextent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolfly = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axSceneControl1
            // 
            this.axSceneControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSceneControl1.Location = new System.Drawing.Point(0, 0);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
            this.axSceneControl1.Size = new System.Drawing.Size(399, 351);
            this.axSceneControl1.TabIndex = 0;
            this.axSceneControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ISceneControlEvents_Ax_OnMouseDownEventHandler(this.axSceneControl1_OnMouseDown);
            this.axSceneControl1.OnKeyUp += new ESRI.ArcGIS.Controls.ISceneControlEvents_Ax_OnKeyUpEventHandler(this.axSceneControl1_OnKeyUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolnavagation,
            this.toolzoomin,
            this.toolzoomout,
            this.toolpan,
            this.toolfly,
            this.toolextent});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 158);
            // 
            // toolnavagation
            // 
            this.toolnavagation.Image = global::LibCerMap.Properties.Resources._3DNavigationTool16;
            this.toolnavagation.Name = "toolnavagation";
            this.toolnavagation.Size = new System.Drawing.Size(152, 22);
            this.toolnavagation.Text = "漫游";
            this.toolnavagation.Click += new System.EventHandler(this.toolnavagation_Click);
            // 
            // toolzoomin
            // 
            this.toolzoomin.Image = global::LibCerMap.Properties.Resources.ZoomInTool_B_16;
            this.toolzoomin.Name = "toolzoomin";
            this.toolzoomin.Size = new System.Drawing.Size(152, 22);
            this.toolzoomin.Text = "放大";
            this.toolzoomin.Click += new System.EventHandler(this.toolzoomin_Click);
            // 
            // toolzoomout
            // 
            this.toolzoomout.Image = global::LibCerMap.Properties.Resources.ZoomOutTool_B_16;
            this.toolzoomout.Name = "toolzoomout";
            this.toolzoomout.Size = new System.Drawing.Size(152, 22);
            this.toolzoomout.Text = "缩小";
            this.toolzoomout.Click += new System.EventHandler(this.toolzoomout_Click);
            // 
            // toolpan
            // 
            this.toolpan.Image = global::LibCerMap.Properties.Resources.PanTool16;
            this.toolpan.Name = "toolpan";
            this.toolpan.Size = new System.Drawing.Size(152, 22);
            this.toolpan.Text = "平移";
            this.toolpan.Click += new System.EventHandler(this.toolpan_Click);
            // 
            // toolextent
            // 
            this.toolextent.Image = global::LibCerMap.Properties.Resources.ZoomFullExtent16;
            this.toolextent.Name = "toolextent";
            this.toolextent.Size = new System.Drawing.Size(152, 22);
            this.toolextent.Text = "全图";
            this.toolextent.Click += new System.EventHandler(this.toolextent_Click);
            // 
            // toolfly
            // 
            this.toolfly.Image = global::LibCerMap.Properties.Resources._3DFlyTool16;
            this.toolfly.Name = "toolfly";
            this.toolfly.Size = new System.Drawing.Size(152, 22);
            this.toolfly.Text = "飞行";
            this.toolfly.Click += new System.EventHandler(this.toolfly_Click);
            // 
            // FrmFullScreenScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 351);
            this.Controls.Add(this.axSceneControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmFullScreenScene";
            this.Text = "FrmFullScreenScene";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmFullScreenScene_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxSceneControl axSceneControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolnavagation;
        private System.Windows.Forms.ToolStripMenuItem toolzoomin;
        private System.Windows.Forms.ToolStripMenuItem toolzoomout;
        private System.Windows.Forms.ToolStripMenuItem toolpan;
        private System.Windows.Forms.ToolStripMenuItem toolextent;
        private System.Windows.Forms.ToolStripMenuItem toolfly;
    }
}