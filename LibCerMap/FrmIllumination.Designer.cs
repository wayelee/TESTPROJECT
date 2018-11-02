namespace LibCerMap
{
    partial class FrmIllumination
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
            DevComponents.Instrumentation.KnobColorTable knobColorTable1 = new DevComponents.Instrumentation.KnobColorTable();
            DevComponents.Instrumentation.Primitives.LinearGradientColorTable linearGradientColorTable1 = new DevComponents.Instrumentation.Primitives.LinearGradientColorTable();
            DevComponents.Instrumentation.KnobColorTable knobColorTable2 = new DevComponents.Instrumentation.KnobColorTable();
            DevComponents.Instrumentation.Primitives.LinearGradientColorTable linearGradientColorTable2 = new DevComponents.Instrumentation.Primitives.LinearGradientColorTable();
            DevComponents.Instrumentation.Primitives.LinearGradientColorTable linearGradientColorTable3 = new DevComponents.Instrumentation.Primitives.LinearGradientColorTable();
            this.gPanelAzimuth = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lblAziumth = new System.Windows.Forms.Label();
            this.ksAzimuth = new DevComponents.Instrumentation.KnobControl();
            this.gpanelaltitude = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lblaltitude = new System.Windows.Forms.Label();
            this.ksAltitude = new DevComponents.Instrumentation.KnobControl();
            this.gPanelConstract = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lblcontrast = new System.Windows.Forms.Label();
            this.trackcontrast = new System.Windows.Forms.TrackBar();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.gPanelAzimuth.SuspendLayout();
            this.gpanelaltitude.SuspendLayout();
            this.gPanelConstract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackcontrast)).BeginInit();
            this.SuspendLayout();
            // 
            // gPanelAzimuth
            // 
            this.gPanelAzimuth.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelAzimuth.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelAzimuth.Controls.Add(this.lblAziumth);
            this.gPanelAzimuth.Controls.Add(this.ksAzimuth);
            this.gPanelAzimuth.Location = new System.Drawing.Point(12, 12);
            this.gPanelAzimuth.Name = "gPanelAzimuth";
            this.gPanelAzimuth.Size = new System.Drawing.Size(177, 221);
            // 
            // 
            // 
            this.gPanelAzimuth.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelAzimuth.Style.BackColorGradientAngle = 90;
            this.gPanelAzimuth.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelAzimuth.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelAzimuth.Style.BorderBottomWidth = 1;
            this.gPanelAzimuth.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelAzimuth.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelAzimuth.Style.BorderLeftWidth = 1;
            this.gPanelAzimuth.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelAzimuth.Style.BorderRightWidth = 1;
            this.gPanelAzimuth.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelAzimuth.Style.BorderTopWidth = 1;
            this.gPanelAzimuth.Style.CornerDiameter = 4;
            this.gPanelAzimuth.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelAzimuth.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelAzimuth.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelAzimuth.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelAzimuth.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelAzimuth.TabIndex = 16;
            this.gPanelAzimuth.Text = "太阳方位角";
            // 
            // lblAziumth
            // 
            this.lblAziumth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAziumth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAziumth.Location = new System.Drawing.Point(3, 170);
            this.lblAziumth.Name = "lblAziumth";
            this.lblAziumth.Size = new System.Drawing.Size(71, 22);
            this.lblAziumth.TabIndex = 48;
            this.lblAziumth.Text = "label1";
            this.lblAziumth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ksAzimuth
            // 
            this.ksAzimuth.BackColor = System.Drawing.Color.Transparent;
            this.ksAzimuth.ForeColor = System.Drawing.Color.Transparent;
            linearGradientColorTable1.End = System.Drawing.Color.DarkTurquoise;
            linearGradientColorTable1.GradientAngle = 40;
            linearGradientColorTable1.Start = System.Drawing.Color.DarkSlateGray;
            knobColorTable1.KnobIndicatorColor = linearGradientColorTable1;
            knobColorTable1.KnobIndicatorPointerColor = System.Drawing.Color.DarkSlateGray;
            knobColorTable1.MinorTickColor = System.Drawing.Color.Transparent;
            this.ksAzimuth.KnobColor = knobColorTable1;
            this.ksAzimuth.Location = new System.Drawing.Point(-24, -27);
            this.ksAzimuth.MajorTickAmount = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ksAzimuth.MaxValue = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.ksAzimuth.MinorTickAmount = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ksAzimuth.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ksAzimuth.Name = "ksAzimuth";
            this.ksAzimuth.SelectionDecimals = 3;
            this.ksAzimuth.Size = new System.Drawing.Size(276, 221);
            this.ksAzimuth.StartAngle = 270;
            this.ksAzimuth.SweepAngle = 360;
            this.ksAzimuth.TabIndex = 47;
            this.ksAzimuth.Text = "knobControl11";
            this.ksAzimuth.ValueChanged += new System.EventHandler<DevComponents.Instrumentation.ValueChangedEventArgs>(this.ksAzimuth_ValueChanged);
            // 
            // gpanelaltitude
            // 
            this.gpanelaltitude.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpanelaltitude.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gpanelaltitude.Controls.Add(this.lblaltitude);
            this.gpanelaltitude.Controls.Add(this.ksAltitude);
            this.gpanelaltitude.Location = new System.Drawing.Point(214, 12);
            this.gpanelaltitude.Name = "gpanelaltitude";
            this.gpanelaltitude.Size = new System.Drawing.Size(177, 221);
            // 
            // 
            // 
            this.gpanelaltitude.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpanelaltitude.Style.BackColorGradientAngle = 90;
            this.gpanelaltitude.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpanelaltitude.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpanelaltitude.Style.BorderBottomWidth = 1;
            this.gpanelaltitude.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpanelaltitude.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpanelaltitude.Style.BorderLeftWidth = 1;
            this.gpanelaltitude.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpanelaltitude.Style.BorderRightWidth = 1;
            this.gpanelaltitude.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpanelaltitude.Style.BorderTopWidth = 1;
            this.gpanelaltitude.Style.CornerDiameter = 4;
            this.gpanelaltitude.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpanelaltitude.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpanelaltitude.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpanelaltitude.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpanelaltitude.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpanelaltitude.TabIndex = 46;
            this.gpanelaltitude.Text = "太阳高度角";
            // 
            // lblaltitude
            // 
            this.lblaltitude.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblaltitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblaltitude.Location = new System.Drawing.Point(3, 170);
            this.lblaltitude.Name = "lblaltitude";
            this.lblaltitude.Size = new System.Drawing.Size(71, 22);
            this.lblaltitude.TabIndex = 56;
            this.lblaltitude.Text = "label1";
            this.lblaltitude.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ksAltitude
            // 
            this.ksAltitude.BackColor = System.Drawing.Color.Transparent;
            this.ksAltitude.ForeColor = System.Drawing.Color.Transparent;
            linearGradientColorTable2.End = System.Drawing.Color.Transparent;
            linearGradientColorTable2.GradientAngle = 0;
            linearGradientColorTable2.Start = System.Drawing.Color.Transparent;
            knobColorTable2.KnobFaceColor = linearGradientColorTable2;
            linearGradientColorTable3.End = System.Drawing.Color.Transparent;
            knobColorTable2.KnobIndicatorColor = linearGradientColorTable3;
            knobColorTable2.MajorTickColor = System.Drawing.Color.Transparent;
            knobColorTable2.MinorTickColor = System.Drawing.Color.Transparent;
            knobColorTable2.ZoneIndicatorColor = System.Drawing.Color.DarkCyan;
            this.ksAltitude.KnobColor = knobColorTable2;
            this.ksAltitude.KnobStyle = DevComponents.Instrumentation.eKnobStyle.Style4;
            this.ksAltitude.Location = new System.Drawing.Point(-160, -7);
            this.ksAltitude.MajorTickAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ksAltitude.MaxValue = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.ksAltitude.MinorTickAmount = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ksAltitude.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ksAltitude.Name = "ksAltitude";
            this.ksAltitude.Size = new System.Drawing.Size(341, 378);
            this.ksAltitude.StartAngle = 0;
            this.ksAltitude.SweepAngle = -90;
            this.ksAltitude.TabIndex = 55;
            this.ksAltitude.Text = "knobControl8";
            this.ksAltitude.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.ksAltitude.ValueChanged += new System.EventHandler<DevComponents.Instrumentation.ValueChangedEventArgs>(this.ksAltitude_ValueChanged);
            // 
            // gPanelConstract
            // 
            this.gPanelConstract.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelConstract.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelConstract.Controls.Add(this.lblcontrast);
            this.gPanelConstract.Controls.Add(this.trackcontrast);
            this.gPanelConstract.Location = new System.Drawing.Point(12, 263);
            this.gPanelConstract.Name = "gPanelConstract";
            this.gPanelConstract.Size = new System.Drawing.Size(298, 85);
            // 
            // 
            // 
            this.gPanelConstract.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelConstract.Style.BackColorGradientAngle = 90;
            this.gPanelConstract.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelConstract.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelConstract.Style.BorderBottomWidth = 1;
            this.gPanelConstract.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelConstract.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelConstract.Style.BorderLeftWidth = 1;
            this.gPanelConstract.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelConstract.Style.BorderRightWidth = 1;
            this.gPanelConstract.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelConstract.Style.BorderTopWidth = 1;
            this.gPanelConstract.Style.CornerDiameter = 4;
            this.gPanelConstract.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelConstract.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelConstract.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelConstract.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelConstract.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelConstract.TabIndex = 47;
            this.gPanelConstract.Text = "标注字段";
            // 
            // lblcontrast
            // 
            this.lblcontrast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblcontrast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcontrast.Location = new System.Drawing.Point(216, 33);
            this.lblcontrast.Name = "lblcontrast";
            this.lblcontrast.Size = new System.Drawing.Size(71, 22);
            this.lblcontrast.TabIndex = 57;
            this.lblcontrast.Text = "label1";
            this.lblcontrast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackcontrast
            // 
            this.trackcontrast.Location = new System.Drawing.Point(3, 10);
            this.trackcontrast.Maximum = 100;
            this.trackcontrast.Name = "trackcontrast";
            this.trackcontrast.Size = new System.Drawing.Size(202, 45);
            this.trackcontrast.TabIndex = 0;
            this.trackcontrast.ValueChanged += new System.EventHandler(this.trackcontrast_ValueChanged);
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(214, 380);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(68, 26);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 48;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(322, 380);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(69, 26);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 49;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(322, 294);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(69, 26);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 51;
            this.buttonX1.Text = "还原值";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // FrmIllumination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 418);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.gPanelConstract);
            this.Controls.Add(this.gpanelaltitude);
            this.Controls.Add(this.gPanelAzimuth);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmIllumination";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "光照设置";
            this.Load += new System.EventHandler(this.FrmIllumination_Load);
            this.gPanelAzimuth.ResumeLayout(false);
            this.gpanelaltitude.ResumeLayout(false);
            this.gPanelConstract.ResumeLayout(false);
            this.gPanelConstract.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackcontrast)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelAzimuth;
        private DevComponents.Instrumentation.KnobControl ksAzimuth;
        private System.Windows.Forms.Label lblAziumth;
        private DevComponents.DotNetBar.Controls.GroupPanel gpanelaltitude;
        private System.Windows.Forms.Label lblaltitude;
        private DevComponents.Instrumentation.KnobControl ksAltitude;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelConstract;
        private System.Windows.Forms.Label lblcontrast;
        private System.Windows.Forms.TrackBar trackcontrast;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}