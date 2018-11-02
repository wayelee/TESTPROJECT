namespace LibCerMap
{
    partial class FrmVertor
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
            this.btn2D = new DevComponents.DotNetBar.ButtonX();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.SunIncli = new DevComponents.Editors.DoubleInput();
            this.SunAzimuth = new DevComponents.Editors.DoubleInput();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.grpanelcolormu = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.EarIncli = new DevComponents.Editors.DoubleInput();
            this.EarAzimuth = new DevComponents.Editors.DoubleInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btn3D = new DevComponents.DotNetBar.ButtonX();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SunIncli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SunAzimuth)).BeginInit();
            this.grpanelcolormu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EarIncli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EarAzimuth)).BeginInit();
            this.SuspendLayout();
            // 
            // btn2D
            // 
            this.btn2D.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn2D.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn2D.Location = new System.Drawing.Point(155, 209);
            this.btn2D.Name = "btn2D";
            this.btn2D.Size = new System.Drawing.Size(72, 27);
            this.btn2D.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn2D.TabIndex = 4;
            this.btn2D.Text = "2D";
            this.btn2D.Click += new System.EventHandler(this.btn2D_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.Location = new System.Drawing.Point(374, 209);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(72, 27);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 5;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.SunIncli);
            this.groupPanel1.Controls.Add(this.SunAzimuth);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Location = new System.Drawing.Point(4, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(450, 76);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 29;
            this.groupPanel1.Text = "太阳矢量参数";
            // 
            // SunIncli
            // 
            // 
            // 
            // 
            this.SunIncli.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SunIncli.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.SunIncli.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.SunIncli.Increment = 1D;
            this.SunIncli.Location = new System.Drawing.Point(282, 14);
            this.SunIncli.MaxValue = 90D;
            this.SunIncli.MinValue = -90D;
            this.SunIncli.Name = "SunIncli";
            this.SunIncli.ShowUpDown = true;
            this.SunIncli.Size = new System.Drawing.Size(143, 21);
            this.SunIncli.TabIndex = 29;
            // 
            // SunAzimuth
            // 
            // 
            // 
            // 
            this.SunAzimuth.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SunAzimuth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.SunAzimuth.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.SunAzimuth.Increment = 1D;
            this.SunAzimuth.Location = new System.Drawing.Point(63, 14);
            this.SunAzimuth.MaxValue = 360D;
            this.SunAzimuth.MinValue = -360D;
            this.SunAzimuth.Name = "SunAzimuth";
            this.SunAzimuth.ShowUpDown = true;
            this.SunAzimuth.Size = new System.Drawing.Size(143, 21);
            this.SunAzimuth.TabIndex = 28;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(230, 14);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(59, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "高度角 :";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 14);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(57, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "方位角 :";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(57, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "方位角 :";
            // 
            // grpanelcolormu
            // 
            this.grpanelcolormu.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpanelcolormu.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grpanelcolormu.Controls.Add(this.EarIncli);
            this.grpanelcolormu.Controls.Add(this.EarAzimuth);
            this.grpanelcolormu.Controls.Add(this.labelX2);
            this.grpanelcolormu.Controls.Add(this.labelX1);
            this.grpanelcolormu.Location = new System.Drawing.Point(4, 111);
            this.grpanelcolormu.Name = "grpanelcolormu";
            this.grpanelcolormu.Size = new System.Drawing.Size(450, 76);
            // 
            // 
            // 
            this.grpanelcolormu.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpanelcolormu.Style.BackColorGradientAngle = 90;
            this.grpanelcolormu.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpanelcolormu.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolormu.Style.BorderBottomWidth = 1;
            this.grpanelcolormu.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpanelcolormu.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolormu.Style.BorderLeftWidth = 1;
            this.grpanelcolormu.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolormu.Style.BorderRightWidth = 1;
            this.grpanelcolormu.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolormu.Style.BorderTopWidth = 1;
            this.grpanelcolormu.Style.CornerDiameter = 4;
            this.grpanelcolormu.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpanelcolormu.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpanelcolormu.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpanelcolormu.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpanelcolormu.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpanelcolormu.TabIndex = 28;
            this.grpanelcolormu.Text = "地球矢量参数";
            // 
            // EarIncli
            // 
            // 
            // 
            // 
            this.EarIncli.BackgroundStyle.Class = "DateTimeInputBackground";
            this.EarIncli.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.EarIncli.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.EarIncli.Increment = 1D;
            this.EarIncli.Location = new System.Drawing.Point(282, 15);
            this.EarIncli.MaxValue = 90D;
            this.EarIncli.MinValue = -90D;
            this.EarIncli.Name = "EarIncli";
            this.EarIncli.ShowUpDown = true;
            this.EarIncli.Size = new System.Drawing.Size(143, 21);
            this.EarIncli.TabIndex = 30;
            // 
            // EarAzimuth
            // 
            // 
            // 
            // 
            this.EarAzimuth.BackgroundStyle.Class = "DateTimeInputBackground";
            this.EarAzimuth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.EarAzimuth.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.EarAzimuth.Increment = 1D;
            this.EarAzimuth.Location = new System.Drawing.Point(63, 13);
            this.EarAzimuth.MaxValue = 360D;
            this.EarAzimuth.MinValue = -360D;
            this.EarAzimuth.Name = "EarAzimuth";
            this.EarAzimuth.ShowUpDown = true;
            this.EarAzimuth.Size = new System.Drawing.Size(143, 21);
            this.EarAzimuth.TabIndex = 29;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(230, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(56, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "高度角 :";
            // 
            // btn3D
            // 
            this.btn3D.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn3D.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn3D.Location = new System.Drawing.Point(259, 209);
            this.btn3D.Name = "btn3D";
            this.btn3D.Size = new System.Drawing.Size(72, 27);
            this.btn3D.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn3D.TabIndex = 30;
            this.btn3D.Text = "3D";
            this.btn3D.Click += new System.EventHandler(this.btn3D_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // FrmVertor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 251);
            this.Controls.Add(this.btn3D);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.grpanelcolormu);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btn2D);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVertor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地球矢量";
            this.Load += new System.EventHandler(this.FrmVertor_Load);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SunIncli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SunAzimuth)).EndInit();
            this.grpanelcolormu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EarIncli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EarAzimuth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btn2D;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.Editors.DoubleInput SunIncli;
        private DevComponents.Editors.DoubleInput SunAzimuth;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.GroupPanel grpanelcolormu;
        private DevComponents.Editors.DoubleInput EarIncli;
        private DevComponents.Editors.DoubleInput EarAzimuth;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btn3D;
        private DevComponents.DotNetBar.StyleManager styleManager1;
    }
}