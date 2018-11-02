namespace LibCerMap
{
    partial class FrmFullScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFullScreen));
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuMapPopupSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoomin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoomout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolpan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolextent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoominfixed = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoomoutfixed = new System.Windows.Forms.ToolStripMenuItem();
            this.toolpreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolnextview = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(332, 303);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnKeyUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnKeyUpEventHandler(this.axMapControl1_OnKeyUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMapPopupSelect,
            this.toolzoomin,
            this.toolzoomout,
            this.toolpan,
            this.toolextent,
            this.toolzoominfixed,
            this.toolzoomoutfixed,
            this.toolpreview,
            this.toolnextview});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(153, 224);
            // 
            // menuMapPopupSelect
            // 
            this.menuMapPopupSelect.Image = ((System.Drawing.Image)(resources.GetObject("menuMapPopupSelect.Image")));
            this.menuMapPopupSelect.Name = "menuMapPopupSelect";
            this.menuMapPopupSelect.Size = new System.Drawing.Size(152, 22);
            this.menuMapPopupSelect.Text = "选择";
            this.menuMapPopupSelect.Click += new System.EventHandler(this.menuMapPopupSelect_Click);
            // 
            // toolzoomin
            // 
            this.toolzoomin.Image = ((System.Drawing.Image)(resources.GetObject("toolzoomin.Image")));
            this.toolzoomin.Name = "toolzoomin";
            this.toolzoomin.Size = new System.Drawing.Size(152, 22);
            this.toolzoomin.Text = "放大";
            this.toolzoomin.Click += new System.EventHandler(this.toolzoomin_Click);
            // 
            // toolzoomout
            // 
            this.toolzoomout.Image = ((System.Drawing.Image)(resources.GetObject("toolzoomout.Image")));
            this.toolzoomout.Name = "toolzoomout";
            this.toolzoomout.Size = new System.Drawing.Size(152, 22);
            this.toolzoomout.Text = "缩小";
            this.toolzoomout.Click += new System.EventHandler(this.toolzoomout_Click);
            // 
            // toolpan
            // 
            this.toolpan.Image = ((System.Drawing.Image)(resources.GetObject("toolpan.Image")));
            this.toolpan.Name = "toolpan";
            this.toolpan.Size = new System.Drawing.Size(152, 22);
            this.toolpan.Text = "漫游";
            this.toolpan.Click += new System.EventHandler(this.toolpan_Click);
            // 
            // toolextent
            // 
            this.toolextent.Image = ((System.Drawing.Image)(resources.GetObject("toolextent.Image")));
            this.toolextent.Name = "toolextent";
            this.toolextent.Size = new System.Drawing.Size(152, 22);
            this.toolextent.Text = "全图";
            this.toolextent.Click += new System.EventHandler(this.toolextent_Click);
            // 
            // toolzoominfixed
            // 
            this.toolzoominfixed.Image = ((System.Drawing.Image)(resources.GetObject("toolzoominfixed.Image")));
            this.toolzoominfixed.Name = "toolzoominfixed";
            this.toolzoominfixed.Size = new System.Drawing.Size(152, 22);
            this.toolzoominfixed.Text = "中心放大";
            this.toolzoominfixed.Click += new System.EventHandler(this.toolzoominfixed_Click);
            // 
            // toolzoomoutfixed
            // 
            this.toolzoomoutfixed.Image = ((System.Drawing.Image)(resources.GetObject("toolzoomoutfixed.Image")));
            this.toolzoomoutfixed.Name = "toolzoomoutfixed";
            this.toolzoomoutfixed.Size = new System.Drawing.Size(152, 22);
            this.toolzoomoutfixed.Text = "中心缩小";
            this.toolzoomoutfixed.Click += new System.EventHandler(this.toolzoomoutfixed_Click);
            // 
            // toolpreview
            // 
            this.toolpreview.Image = ((System.Drawing.Image)(resources.GetObject("toolpreview.Image")));
            this.toolpreview.Name = "toolpreview";
            this.toolpreview.Size = new System.Drawing.Size(152, 22);
            this.toolpreview.Text = "前一视图";
            this.toolpreview.Click += new System.EventHandler(this.toolpreview_Click);
            // 
            // toolnextview
            // 
            this.toolnextview.Image = ((System.Drawing.Image)(resources.GetObject("toolnextview.Image")));
            this.toolnextview.Name = "toolnextview";
            this.toolnextview.Size = new System.Drawing.Size(152, 22);
            this.toolnextview.Text = "后一视图";
            this.toolnextview.Click += new System.EventHandler(this.toolnextview_Click);
            // 
            // FrmFullScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 303);
            this.Controls.Add(this.axMapControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmFullScreen";
            this.Text = "FrmFullScreen";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmFullScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem menuMapPopupSelect;
        private System.Windows.Forms.ToolStripMenuItem toolzoomin;
        private System.Windows.Forms.ToolStripMenuItem toolzoomout;
        private System.Windows.Forms.ToolStripMenuItem toolpan;
        private System.Windows.Forms.ToolStripMenuItem toolextent;
        private System.Windows.Forms.ToolStripMenuItem toolzoominfixed;
        private System.Windows.Forms.ToolStripMenuItem toolzoomoutfixed;
        private System.Windows.Forms.ToolStripMenuItem toolpreview;
        private System.Windows.Forms.ToolStripMenuItem toolnextview;
    }
}