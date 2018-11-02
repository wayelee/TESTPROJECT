namespace LibCerMap
{
    partial class FrmTextSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTextSymbol));
            this.gPanelTextSymbol = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.axSymbologyControl = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.gPanelTextPreview = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.toolText = new System.Windows.Forms.ToolStrip();
            this.toolBtnBold = new System.Windows.Forms.ToolStripButton();
            this.toolBtnIntend = new System.Windows.Forms.ToolStripButton();
            this.toolBtnUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolBtnStrikethrough = new System.Windows.Forms.ToolStripButton();
            this.labStyle = new DevComponents.DotNetBar.LabelX();
            this.FontSize = new DevComponents.Editors.DoubleInput();
            this.cmbBoxFontName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.FontColor = new LibCerMap.CustomColorPicker();
            this.labFontSize = new DevComponents.DotNetBar.LabelX();
            this.labFont = new DevComponents.DotNetBar.LabelX();
            this.labFontColor = new DevComponents.DotNetBar.LabelX();
            this.ImagePreview = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.gPanelTextSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl)).BeginInit();
            this.gPanelTextPreview.SuspendLayout();
            this.toolText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // gPanelTextSymbol
            // 
            this.gPanelTextSymbol.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelTextSymbol.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelTextSymbol.Controls.Add(this.axSymbologyControl);
            this.gPanelTextSymbol.Dock = System.Windows.Forms.DockStyle.Left;
            this.gPanelTextSymbol.Location = new System.Drawing.Point(0, 0);
            this.gPanelTextSymbol.Name = "gPanelTextSymbol";
            this.gPanelTextSymbol.Size = new System.Drawing.Size(249, 386);
            // 
            // 
            // 
            this.gPanelTextSymbol.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelTextSymbol.Style.BackColorGradientAngle = 90;
            this.gPanelTextSymbol.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelTextSymbol.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelTextSymbol.Style.BorderBottomWidth = 1;
            this.gPanelTextSymbol.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelTextSymbol.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelTextSymbol.Style.BorderLeftWidth = 1;
            this.gPanelTextSymbol.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelTextSymbol.Style.BorderRightWidth = 1;
            this.gPanelTextSymbol.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelTextSymbol.Style.BorderTopWidth = 1;
            this.gPanelTextSymbol.Style.CornerDiameter = 4;
            this.gPanelTextSymbol.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelTextSymbol.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gPanelTextSymbol.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelTextSymbol.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelTextSymbol.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelTextSymbol.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelTextSymbol.TabIndex = 0;
            // 
            // axSymbologyControl
            // 
            this.axSymbologyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSymbologyControl.Location = new System.Drawing.Point(0, 0);
            this.axSymbologyControl.Name = "axSymbologyControl";
            this.axSymbologyControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl.OcxState")));
            this.axSymbologyControl.Size = new System.Drawing.Size(243, 380);
            this.axSymbologyControl.TabIndex = 0;
            this.axSymbologyControl.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl_OnItemSelected);
            // 
            // gPanelTextPreview
            // 
            this.gPanelTextPreview.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelTextPreview.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelTextPreview.Controls.Add(this.toolText);
            this.gPanelTextPreview.Controls.Add(this.labStyle);
            this.gPanelTextPreview.Controls.Add(this.FontSize);
            this.gPanelTextPreview.Controls.Add(this.cmbBoxFontName);
            this.gPanelTextPreview.Controls.Add(this.FontColor);
            this.gPanelTextPreview.Controls.Add(this.labFontSize);
            this.gPanelTextPreview.Controls.Add(this.labFont);
            this.gPanelTextPreview.Controls.Add(this.labFontColor);
            this.gPanelTextPreview.Controls.Add(this.ImagePreview);
            this.gPanelTextPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.gPanelTextPreview.Location = new System.Drawing.Point(249, 0);
            this.gPanelTextPreview.Name = "gPanelTextPreview";
            this.gPanelTextPreview.Size = new System.Drawing.Size(220, 316);
            // 
            // 
            // 
            this.gPanelTextPreview.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelTextPreview.Style.BackColorGradientAngle = 90;
            this.gPanelTextPreview.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelTextPreview.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelTextPreview.Style.BorderBottomWidth = 1;
            this.gPanelTextPreview.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelTextPreview.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelTextPreview.Style.BorderLeftWidth = 1;
            this.gPanelTextPreview.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelTextPreview.Style.BorderRightWidth = 1;
            this.gPanelTextPreview.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelTextPreview.Style.BorderTopWidth = 1;
            this.gPanelTextPreview.Style.CornerDiameter = 4;
            this.gPanelTextPreview.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelTextPreview.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelTextPreview.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelTextPreview.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelTextPreview.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelTextPreview.TabIndex = 2;
            this.gPanelTextPreview.Text = "当前设置";
            // 
            // toolText
            // 
            this.toolText.BackColor = System.Drawing.Color.Transparent;
            this.toolText.Dock = System.Windows.Forms.DockStyle.None;
            this.toolText.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnBold,
            this.toolBtnIntend,
            this.toolBtnUnderline,
            this.toolBtnStrikethrough});
            this.toolText.Location = new System.Drawing.Point(62, 258);
            this.toolText.Name = "toolText";
            this.toolText.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolText.Size = new System.Drawing.Size(99, 25);
            this.toolText.TabIndex = 22;
            // 
            // toolBtnBold
            // 
            this.toolBtnBold.Checked = true;
            this.toolBtnBold.CheckOnClick = true;
            this.toolBtnBold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnBold.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolBtnBold.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnBold.Image")));
            this.toolBtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBold.Name = "toolBtnBold";
            this.toolBtnBold.Size = new System.Drawing.Size(23, 22);
            this.toolBtnBold.Text = "B";
            // 
            // toolBtnIntend
            // 
            this.toolBtnIntend.CheckOnClick = true;
            this.toolBtnIntend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnIntend.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolBtnIntend.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnIntend.Image")));
            this.toolBtnIntend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnIntend.Name = "toolBtnIntend";
            this.toolBtnIntend.Size = new System.Drawing.Size(23, 22);
            this.toolBtnIntend.Text = "I";
            // 
            // toolBtnUnderline
            // 
            this.toolBtnUnderline.CheckOnClick = true;
            this.toolBtnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnUnderline.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolBtnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnUnderline.Image")));
            this.toolBtnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnUnderline.Name = "toolBtnUnderline";
            this.toolBtnUnderline.Size = new System.Drawing.Size(23, 22);
            this.toolBtnUnderline.Text = "U";
            // 
            // toolBtnStrikethrough
            // 
            this.toolBtnStrikethrough.CheckOnClick = true;
            this.toolBtnStrikethrough.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnStrikethrough.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Strikeout))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolBtnStrikethrough.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnStrikethrough.Image")));
            this.toolBtnStrikethrough.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnStrikethrough.Name = "toolBtnStrikethrough";
            this.toolBtnStrikethrough.Size = new System.Drawing.Size(27, 22);
            this.toolBtnStrikethrough.Text = "ST";
            // 
            // labStyle
            // 
            this.labStyle.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labStyle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labStyle.Location = new System.Drawing.Point(13, 258);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(43, 23);
            this.labStyle.TabIndex = 10;
            this.labStyle.Text = "格式：";
            // 
            // FontSize
            // 
            // 
            // 
            // 
            this.FontSize.BackgroundStyle.Class = "DateTimeInputBackground";
            this.FontSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.FontSize.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.FontSize.Increment = 1D;
            this.FontSize.Location = new System.Drawing.Point(62, 217);
            this.FontSize.MaxValue = 100D;
            this.FontSize.MinValue = 5D;
            this.FontSize.Name = "FontSize";
            this.FontSize.ShowUpDown = true;
            this.FontSize.Size = new System.Drawing.Size(95, 21);
            this.FontSize.TabIndex = 8;
            this.FontSize.Value = 10D;
            this.FontSize.ValueChanged += new System.EventHandler(this.FontSize_ValueChanged);
            // 
            // cmbBoxFontName
            // 
            this.cmbBoxFontName.DisplayMember = "Text";
            this.cmbBoxFontName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBoxFontName.FormattingEnabled = true;
            this.cmbBoxFontName.ItemHeight = 15;
            this.cmbBoxFontName.Location = new System.Drawing.Point(62, 180);
            this.cmbBoxFontName.Name = "cmbBoxFontName";
            this.cmbBoxFontName.Size = new System.Drawing.Size(95, 21);
            this.cmbBoxFontName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbBoxFontName.TabIndex = 7;
            // 
            // FontColor
            // 
            this.FontColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.FontColor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.FontColor.Image = ((System.Drawing.Image)(resources.GetObject("FontColor.Image")));
            this.FontColor.Location = new System.Drawing.Point(62, 139);
            this.FontColor.Name = "FontColor";
            this.FontColor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.FontColor.Size = new System.Drawing.Size(46, 23);
            this.FontColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.FontColor.TabIndex = 6;
            this.FontColor.SelectedColorChanged += new System.EventHandler(this.FontColor_SelectedColorChanged);
            // 
            // labFontSize
            // 
            this.labFontSize.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labFontSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labFontSize.Location = new System.Drawing.Point(13, 215);
            this.labFontSize.Name = "labFontSize";
            this.labFontSize.Size = new System.Drawing.Size(43, 23);
            this.labFontSize.TabIndex = 3;
            this.labFontSize.Text = "大小：";
            // 
            // labFont
            // 
            this.labFont.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labFont.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labFont.Location = new System.Drawing.Point(13, 180);
            this.labFont.Name = "labFont";
            this.labFont.Size = new System.Drawing.Size(43, 23);
            this.labFont.TabIndex = 2;
            this.labFont.Text = "字体：";
            // 
            // labFontColor
            // 
            this.labFontColor.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labFontColor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labFontColor.Location = new System.Drawing.Point(13, 139);
            this.labFontColor.Name = "labFontColor";
            this.labFontColor.Size = new System.Drawing.Size(43, 23);
            this.labFontColor.TabIndex = 1;
            this.labFontColor.Text = "颜色：";
            // 
            // ImagePreview
            // 
            // 
            // 
            // 
            this.ImagePreview.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ImagePreview.BackgroundStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ImagePreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.ImagePreview.Image = ((System.Drawing.Image)(resources.GetObject("ImagePreview.Image")));
            this.ImagePreview.Location = new System.Drawing.Point(0, 0);
            this.ImagePreview.Name = "ImagePreview";
            this.ImagePreview.Size = new System.Drawing.Size(214, 122);
            this.ImagePreview.TabIndex = 0;
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(268, 336);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 3;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(349, 336);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FrmTextSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 386);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gPanelTextPreview);
            this.Controls.Add(this.gPanelTextSymbol);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTextSymbol";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择字体";
            this.Load += new System.EventHandler(this.FrmTextSymbol_Load);
            this.gPanelTextSymbol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl)).EndInit();
            this.gPanelTextPreview.ResumeLayout(false);
            this.gPanelTextPreview.PerformLayout();
            this.toolText.ResumeLayout(false);
            this.toolText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelTextSymbol;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelTextPreview;
        private DevComponents.DotNetBar.Controls.ReflectionImage ImagePreview;
        private DevComponents.DotNetBar.LabelX labFontSize;
        private DevComponents.DotNetBar.LabelX labFont;
        private DevComponents.DotNetBar.LabelX labFontColor;
        private DevComponents.Editors.DoubleInput FontSize;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbBoxFontName;
        private CustomColorPicker FontColor;
        private DevComponents.DotNetBar.LabelX labStyle;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ToolStrip toolText;
        private System.Windows.Forms.ToolStripButton toolBtnBold;
        private System.Windows.Forms.ToolStripButton toolBtnIntend;
        private System.Windows.Forms.ToolStripButton toolBtnUnderline;
        private System.Windows.Forms.ToolStripButton toolBtnStrikethrough;
    }
}