namespace LibCerMap
{
    partial class FrmChangeDefaultSymbol
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
            this.labPoint = new System.Windows.Forms.Label();
            this.labPolyline = new System.Windows.Forms.Label();
            this.labPolygon = new System.Windows.Forms.Label();
            this.gpPanelDefault = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btPoint = new DevComponents.DotNetBar.ButtonX();
            this.btPolyline = new DevComponents.DotNetBar.ButtonX();
            this.btPolygon = new DevComponents.DotNetBar.ButtonX();
            this.buttonOK = new DevComponents.DotNetBar.ButtonX();
            this.btClose = new DevComponents.DotNetBar.ButtonX();
            this.gpPanelDefault.SuspendLayout();
            this.SuspendLayout();
            // 
            // labPoint
            // 
            this.labPoint.AutoSize = true;
            this.labPoint.BackColor = System.Drawing.Color.Transparent;
            this.labPoint.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPoint.Location = new System.Drawing.Point(71, 95);
            this.labPoint.Name = "labPoint";
            this.labPoint.Size = new System.Drawing.Size(29, 19);
            this.labPoint.TabIndex = 1;
            this.labPoint.Text = "点";
            // 
            // labPolyline
            // 
            this.labPolyline.AutoSize = true;
            this.labPolyline.BackColor = System.Drawing.Color.Transparent;
            this.labPolyline.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPolyline.Location = new System.Drawing.Point(203, 95);
            this.labPolyline.Name = "labPolyline";
            this.labPolyline.Size = new System.Drawing.Size(29, 19);
            this.labPolyline.TabIndex = 1;
            this.labPolyline.Text = "线";
            // 
            // labPolygon
            // 
            this.labPolygon.AutoSize = true;
            this.labPolygon.BackColor = System.Drawing.Color.Transparent;
            this.labPolygon.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPolygon.Location = new System.Drawing.Point(335, 95);
            this.labPolygon.Name = "labPolygon";
            this.labPolygon.Size = new System.Drawing.Size(29, 19);
            this.labPolygon.TabIndex = 1;
            this.labPolygon.Text = "面";
            // 
            // gpPanelDefault
            // 
            this.gpPanelDefault.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpPanelDefault.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gpPanelDefault.Controls.Add(this.btPolygon);
            this.gpPanelDefault.Controls.Add(this.btPolyline);
            this.gpPanelDefault.Controls.Add(this.btPoint);
            this.gpPanelDefault.Controls.Add(this.labPolygon);
            this.gpPanelDefault.Controls.Add(this.labPolyline);
            this.gpPanelDefault.Controls.Add(this.labPoint);
            this.gpPanelDefault.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpPanelDefault.Location = new System.Drawing.Point(0, 0);
            this.gpPanelDefault.Name = "gpPanelDefault";
            this.gpPanelDefault.Size = new System.Drawing.Size(441, 133);
            // 
            // 
            // 
            this.gpPanelDefault.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.gpPanelDefault.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.gpPanelDefault.Style.BackColorGradientAngle = 90;
            this.gpPanelDefault.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpPanelDefault.Style.BorderBottomWidth = 1;
            this.gpPanelDefault.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpPanelDefault.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpPanelDefault.Style.BorderLeftWidth = 1;
            this.gpPanelDefault.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpPanelDefault.Style.BorderRightWidth = 1;
            this.gpPanelDefault.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpPanelDefault.Style.BorderTopWidth = 1;
            this.gpPanelDefault.Style.CornerDiameter = 4;
            this.gpPanelDefault.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpPanelDefault.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpPanelDefault.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpPanelDefault.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpPanelDefault.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpPanelDefault.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpPanelDefault.TabIndex = 3;
            // 
            // btPoint
            // 
            this.btPoint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btPoint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btPoint.Location = new System.Drawing.Point(38, 21);
            this.btPoint.Name = "btPoint";
            this.btPoint.Size = new System.Drawing.Size(94, 50);
            this.btPoint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btPoint.TabIndex = 2;
            this.btPoint.Click += new System.EventHandler(this.btPoint_Click);
            // 
            // btPolyline
            // 
            this.btPolyline.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btPolyline.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btPolyline.Location = new System.Drawing.Point(170, 21);
            this.btPolyline.Name = "btPolyline";
            this.btPolyline.Size = new System.Drawing.Size(94, 50);
            this.btPolyline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btPolyline.TabIndex = 3;
            this.btPolyline.Click += new System.EventHandler(this.btPolyline_Click);
            // 
            // btPolygon
            // 
            this.btPolygon.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btPolygon.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btPolygon.Location = new System.Drawing.Point(302, 21);
            this.btPolygon.Name = "btPolygon";
            this.btPolygon.Size = new System.Drawing.Size(94, 50);
            this.btPolygon.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btPolygon.TabIndex = 4;
            this.btPolygon.Click += new System.EventHandler(this.btPolygon_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOK.Location = new System.Drawing.Point(219, 155);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(82, 34);
            this.buttonOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "确定";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // btClose
            // 
            this.btClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btClose.Location = new System.Drawing.Point(315, 155);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(82, 34);
            this.btClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btClose.TabIndex = 6;
            this.btClose.Text = "取消";
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // FrmChangeDefaultSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 211);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.gpPanelDefault);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangeDefaultSymbol";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "缺省样式";
            this.Load += new System.EventHandler(this.ChangeDefaultSymbol_Load);
            this.gpPanelDefault.ResumeLayout(false);
            this.gpPanelDefault.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labPoint;
        private System.Windows.Forms.Label labPolyline;
        private System.Windows.Forms.Label labPolygon;
        private DevComponents.DotNetBar.Controls.GroupPanel gpPanelDefault;
        private DevComponents.DotNetBar.ButtonX btPoint;
        private DevComponents.DotNetBar.ButtonX btPolygon;
        private DevComponents.DotNetBar.ButtonX btPolyline;
        private DevComponents.DotNetBar.ButtonX buttonOK;
        private DevComponents.DotNetBar.ButtonX btClose;
    }
}