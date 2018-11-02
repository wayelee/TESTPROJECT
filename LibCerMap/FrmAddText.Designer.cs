namespace LibCerMap
{
    partial class FrmAddText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddText));
            this.btApply = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.gPanelEditText = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dbiLineLeading = new DevComponents.Editors.DoubleInput();
            this.dbiCharSpacing = new DevComponents.Editors.DoubleInput();
            this.TextAngle = new DevComponents.Editors.DoubleInput();
            this.labAngle = new DevComponents.DotNetBar.LabelX();
            this.labPostion = new DevComponents.DotNetBar.LabelX();
            this.toolTitlePostion = new System.Windows.Forms.ToolStrip();
            this.toolBtnLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnBoth = new System.Windows.Forms.ToolStripButton();
            this.toolText = new System.Windows.Forms.ToolStrip();
            this.toolBtnBold = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnIntend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnStrikethrough = new System.Windows.Forms.ToolStripButton();
            this.labStyle = new DevComponents.DotNetBar.LabelX();
            this.FontSize = new DevComponents.Editors.DoubleInput();
            this.cmbBoxFontName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.FontColor = new CustomColorPicker();
            this.labFontSize = new DevComponents.DotNetBar.LabelX();
            this.labelLineLeading = new DevComponents.DotNetBar.LabelX();
            this.labFont = new DevComponents.DotNetBar.LabelX();
            this.labelCharSpacing = new DevComponents.DotNetBar.LabelX();
            this.labFontColor = new DevComponents.DotNetBar.LabelX();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.gPanelText = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtAddText = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.gPanelEditText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbiLineLeading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiCharSpacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextAngle)).BeginInit();
            this.toolTitlePostion.SuspendLayout();
            this.toolText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).BeginInit();
            this.gPanelText.SuspendLayout();
            this.SuspendLayout();
            // 
            // btApply
            // 
            this.btApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btApply.Location = new System.Drawing.Point(285, 356);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 23);
            this.btApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btApply.TabIndex = 8;
            this.btApply.Text = "应用";
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(196, 356);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "取消";
            // 
            // gPanelEditText
            // 
            this.gPanelEditText.BackColor = System.Drawing.Color.Transparent;
            this.gPanelEditText.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelEditText.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelEditText.Controls.Add(this.dbiLineLeading);
            this.gPanelEditText.Controls.Add(this.dbiCharSpacing);
            this.gPanelEditText.Controls.Add(this.TextAngle);
            this.gPanelEditText.Controls.Add(this.labAngle);
            this.gPanelEditText.Controls.Add(this.labPostion);
            this.gPanelEditText.Controls.Add(this.toolTitlePostion);
            this.gPanelEditText.Controls.Add(this.toolText);
            this.gPanelEditText.Controls.Add(this.labStyle);
            this.gPanelEditText.Controls.Add(this.FontSize);
            this.gPanelEditText.Controls.Add(this.cmbBoxFontName);
            this.gPanelEditText.Controls.Add(this.FontColor);
            this.gPanelEditText.Controls.Add(this.labFontSize);
            this.gPanelEditText.Controls.Add(this.labelLineLeading);
            this.gPanelEditText.Controls.Add(this.labFont);
            this.gPanelEditText.Controls.Add(this.labelCharSpacing);
            this.gPanelEditText.Controls.Add(this.labFontColor);
            this.gPanelEditText.Location = new System.Drawing.Point(17, 159);
            this.gPanelEditText.Name = "gPanelEditText";
            this.gPanelEditText.Size = new System.Drawing.Size(342, 175);
            // 
            // 
            // 
            this.gPanelEditText.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelEditText.Style.BackColorGradientAngle = 90;
            this.gPanelEditText.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelEditText.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelEditText.Style.BorderBottomWidth = 1;
            this.gPanelEditText.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelEditText.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelEditText.Style.BorderLeftWidth = 1;
            this.gPanelEditText.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelEditText.Style.BorderRightWidth = 1;
            this.gPanelEditText.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelEditText.Style.BorderTopWidth = 1;
            this.gPanelEditText.Style.CornerDiameter = 4;
            this.gPanelEditText.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelEditText.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelEditText.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelEditText.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelEditText.TabIndex = 6;
            // 
            // dbiLineLeading
            // 
            // 
            // 
            // 
            this.dbiLineLeading.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiLineLeading.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiLineLeading.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiLineLeading.Increment = 1D;
            this.dbiLineLeading.Location = new System.Drawing.Point(226, 95);
            this.dbiLineLeading.MinValue = 0D;
            this.dbiLineLeading.Name = "dbiLineLeading";
            this.dbiLineLeading.ShowUpDown = true;
            this.dbiLineLeading.Size = new System.Drawing.Size(79, 21);
            this.dbiLineLeading.TabIndex = 34;
            // 
            // dbiCharSpacing
            // 
            // 
            // 
            // 
            this.dbiCharSpacing.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiCharSpacing.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiCharSpacing.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiCharSpacing.Increment = 1D;
            this.dbiCharSpacing.Location = new System.Drawing.Point(65, 136);
            this.dbiCharSpacing.MinValue = 0D;
            this.dbiCharSpacing.Name = "dbiCharSpacing";
            this.dbiCharSpacing.ShowUpDown = true;
            this.dbiCharSpacing.Size = new System.Drawing.Size(79, 21);
            this.dbiCharSpacing.TabIndex = 34;
            // 
            // TextAngle
            // 
            // 
            // 
            // 
            this.TextAngle.BackgroundStyle.Class = "DateTimeInputBackground";
            this.TextAngle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TextAngle.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.TextAngle.Increment = 1D;
            this.TextAngle.Location = new System.Drawing.Point(50, 93);
            this.TextAngle.MaxValue = 360D;
            this.TextAngle.MinValue = 0D;
            this.TextAngle.Name = "TextAngle";
            this.TextAngle.ShowUpDown = true;
            this.TextAngle.Size = new System.Drawing.Size(95, 21);
            this.TextAngle.TabIndex = 34;
            // 
            // labAngle
            // 
            this.labAngle.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labAngle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labAngle.Location = new System.Drawing.Point(6, 93);
            this.labAngle.Name = "labAngle";
            this.labAngle.Size = new System.Drawing.Size(43, 23);
            this.labAngle.TabIndex = 33;
            this.labAngle.Text = "角度：";
            // 
            // labPostion
            // 
            this.labPostion.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labPostion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labPostion.Location = new System.Drawing.Point(167, 54);
            this.labPostion.Name = "labPostion";
            this.labPostion.Size = new System.Drawing.Size(43, 23);
            this.labPostion.TabIndex = 32;
            this.labPostion.Text = "对齐：";
            // 
            // toolTitlePostion
            // 
            this.toolTitlePostion.BackColor = System.Drawing.Color.Transparent;
            this.toolTitlePostion.Dock = System.Windows.Forms.DockStyle.None;
            this.toolTitlePostion.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolTitlePostion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnLeft,
            this.toolStripSeparator1,
            this.toolBtnCenter,
            this.toolStripSeparator2,
            this.toolBtnRight,
            this.toolStripSeparator3,
            this.toolBtnBoth});
            this.toolTitlePostion.Location = new System.Drawing.Point(211, 51);
            this.toolTitlePostion.Name = "toolTitlePostion";
            this.toolTitlePostion.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolTitlePostion.Size = new System.Drawing.Size(113, 26);
            this.toolTitlePostion.TabIndex = 31;
            // 
            // toolBtnLeft
            // 
            this.toolBtnLeft.AutoSize = false;
            this.toolBtnLeft.Checked = true;
            this.toolBtnLeft.CheckOnClick = true;
            this.toolBtnLeft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBtnLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnLeft.Image = global::LibCerMap.Properties.Resources.Left;
            this.toolBtnLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnLeft.Name = "toolBtnLeft";
            this.toolBtnLeft.Size = new System.Drawing.Size(23, 23);
            this.toolBtnLeft.Click += new System.EventHandler(this.toolBtnLeft_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 0);
            // 
            // toolBtnCenter
            // 
            this.toolBtnCenter.AutoSize = false;
            this.toolBtnCenter.CheckOnClick = true;
            this.toolBtnCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnCenter.Image = global::LibCerMap.Properties.Resources.Center;
            this.toolBtnCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnCenter.Name = "toolBtnCenter";
            this.toolBtnCenter.Size = new System.Drawing.Size(23, 23);
            this.toolBtnCenter.Click += new System.EventHandler(this.toolBtnCenter_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 0);
            // 
            // toolBtnRight
            // 
            this.toolBtnRight.AutoSize = false;
            this.toolBtnRight.CheckOnClick = true;
            this.toolBtnRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnRight.Image = global::LibCerMap.Properties.Resources.Right;
            this.toolBtnRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnRight.Name = "toolBtnRight";
            this.toolBtnRight.Size = new System.Drawing.Size(23, 23);
            this.toolBtnRight.Click += new System.EventHandler(this.toolBtnRight_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 0);
            // 
            // toolBtnBoth
            // 
            this.toolBtnBoth.AutoSize = false;
            this.toolBtnBoth.CheckOnClick = true;
            this.toolBtnBoth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnBoth.Image = global::LibCerMap.Properties.Resources.Both;
            this.toolBtnBoth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBoth.Name = "toolBtnBoth";
            this.toolBtnBoth.Size = new System.Drawing.Size(23, 23);
            this.toolBtnBoth.Click += new System.EventHandler(this.toolBtnBoth_Click);
            // 
            // toolText
            // 
            this.toolText.BackColor = System.Drawing.Color.Transparent;
            this.toolText.Dock = System.Windows.Forms.DockStyle.None;
            this.toolText.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnBold,
            this.toolStripSeparator4,
            this.toolBtnIntend,
            this.toolStripSeparator5,
            this.toolBtnUnderline,
            this.toolStripSeparator6,
            this.toolBtnStrikethrough});
            this.toolText.Location = new System.Drawing.Point(211, 10);
            this.toolText.Name = "toolText";
            this.toolText.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolText.Size = new System.Drawing.Size(117, 25);
            this.toolText.TabIndex = 30;
            // 
            // toolBtnBold
            // 
            this.toolBtnBold.CheckOnClick = true;
            this.toolBtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnBold.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolBtnBold.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnBold.Image")));
            this.toolBtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBold.Name = "toolBtnBold";
            this.toolBtnBold.Size = new System.Drawing.Size(23, 22);
            this.toolBtnBold.Text = "B";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 0);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 0);
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AutoSize = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 0);
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
            this.labStyle.Location = new System.Drawing.Point(167, 12);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(43, 23);
            this.labStyle.TabIndex = 29;
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
            this.FontSize.Location = new System.Drawing.Point(49, 55);
            this.FontSize.MaxValue = 100D;
            this.FontSize.MinValue = 5D;
            this.FontSize.Name = "FontSize";
            this.FontSize.ShowUpDown = true;
            this.FontSize.Size = new System.Drawing.Size(95, 21);
            this.FontSize.TabIndex = 28;
            this.FontSize.Value = 10D;
            // 
            // cmbBoxFontName
            // 
            this.cmbBoxFontName.DisplayMember = "Text";
            this.cmbBoxFontName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBoxFontName.FormattingEnabled = true;
            this.cmbBoxFontName.ItemHeight = 15;
            this.cmbBoxFontName.Location = new System.Drawing.Point(49, 12);
            this.cmbBoxFontName.Name = "cmbBoxFontName";
            this.cmbBoxFontName.Size = new System.Drawing.Size(95, 21);
            this.cmbBoxFontName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbBoxFontName.TabIndex = 27;
            // 
            // FontColor
            // 
            this.FontColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.FontColor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.FontColor.Image = ((System.Drawing.Image)(resources.GetObject("FontColor.Image")));
            this.FontColor.Location = new System.Drawing.Point(216, 134);
            this.FontColor.Name = "FontColor";
            this.FontColor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.FontColor.Size = new System.Drawing.Size(46, 23);
            this.FontColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.FontColor.TabIndex = 26;
            // 
            // labFontSize
            // 
            this.labFontSize.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labFontSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labFontSize.Location = new System.Drawing.Point(6, 53);
            this.labFontSize.Name = "labFontSize";
            this.labFontSize.Size = new System.Drawing.Size(43, 23);
            this.labFontSize.TabIndex = 25;
            this.labFontSize.Text = "大小：";
            // 
            // labelLineLeading
            // 
            this.labelLineLeading.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelLineLeading.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelLineLeading.Location = new System.Drawing.Point(167, 93);
            this.labelLineLeading.Name = "labelLineLeading";
            this.labelLineLeading.Size = new System.Drawing.Size(53, 23);
            this.labelLineLeading.TabIndex = 23;
            this.labelLineLeading.Text = "行间距:";
            // 
            // labFont
            // 
            this.labFont.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labFont.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labFont.Location = new System.Drawing.Point(6, 12);
            this.labFont.Name = "labFont";
            this.labFont.Size = new System.Drawing.Size(43, 23);
            this.labFont.TabIndex = 24;
            this.labFont.Text = "字体：";
            // 
            // labelCharSpacing
            // 
            this.labelCharSpacing.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelCharSpacing.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelCharSpacing.Location = new System.Drawing.Point(6, 134);
            this.labelCharSpacing.Name = "labelCharSpacing";
            this.labelCharSpacing.Size = new System.Drawing.Size(62, 23);
            this.labelCharSpacing.TabIndex = 23;
            this.labelCharSpacing.Text = "字符间距:";
            // 
            // labFontColor
            // 
            this.labFontColor.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labFontColor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labFontColor.Location = new System.Drawing.Point(167, 134);
            this.labFontColor.Name = "labFontColor";
            this.labFontColor.Size = new System.Drawing.Size(43, 23);
            this.labFontColor.TabIndex = 23;
            this.labFontColor.Text = "颜色：";
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(107, 356);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 4;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // gPanelText
            // 
            this.gPanelText.BackColor = System.Drawing.Color.Transparent;
            this.gPanelText.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelText.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelText.Controls.Add(this.txtAddText);
            this.gPanelText.Location = new System.Drawing.Point(17, 5);
            this.gPanelText.Name = "gPanelText";
            this.gPanelText.Size = new System.Drawing.Size(342, 142);
            // 
            // 
            // 
            this.gPanelText.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelText.Style.BackColorGradientAngle = 90;
            this.gPanelText.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelText.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelText.Style.BorderBottomWidth = 1;
            this.gPanelText.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelText.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelText.Style.BorderLeftWidth = 1;
            this.gPanelText.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelText.Style.BorderRightWidth = 1;
            this.gPanelText.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelText.Style.BorderTopWidth = 1;
            this.gPanelText.Style.CornerDiameter = 4;
            this.gPanelText.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelText.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelText.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelText.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelText.TabIndex = 5;
            // 
            // txtAddText
            // 
            // 
            // 
            // 
            this.txtAddText.BackgroundStyle.Class = "RichTextBoxBorder";
            this.txtAddText.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAddText.Location = new System.Drawing.Point(3, 3);
            this.txtAddText.Name = "txtAddText";
            this.txtAddText.Size = new System.Drawing.Size(330, 130);
            this.txtAddText.TabIndex = 0;
            this.txtAddText.Text = "Text";
            // 
            // FrmAddText
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 406);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.gPanelEditText);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gPanelText);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddText";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加文本";
            this.Load += new System.EventHandler(this.FrmAddText_Load);
            this.gPanelEditText.ResumeLayout(false);
            this.gPanelEditText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbiLineLeading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiCharSpacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextAngle)).EndInit();
            this.toolTitlePostion.ResumeLayout(false);
            this.toolTitlePostion.PerformLayout();
            this.toolText.ResumeLayout(false);
            this.toolText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).EndInit();
            this.gPanelText.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btApply;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelEditText;
        private DevComponents.Editors.DoubleInput TextAngle;
        private DevComponents.DotNetBar.LabelX labAngle;
        private DevComponents.DotNetBar.LabelX labPostion;
        private System.Windows.Forms.ToolStrip toolTitlePostion;
        private System.Windows.Forms.ToolStripButton toolBtnLeft;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolBtnCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolBtnRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolBtnBoth;
        private System.Windows.Forms.ToolStrip toolText;
        private System.Windows.Forms.ToolStripButton toolBtnBold;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolBtnIntend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolBtnUnderline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolBtnStrikethrough;
        private DevComponents.DotNetBar.LabelX labStyle;
        private DevComponents.Editors.DoubleInput FontSize;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbBoxFontName;
        private CustomColorPicker FontColor;
        private DevComponents.DotNetBar.LabelX labFontSize;
        private DevComponents.DotNetBar.LabelX labFont;
        private DevComponents.DotNetBar.LabelX labFontColor;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelText;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx txtAddText;
        private DevComponents.Editors.DoubleInput dbiLineLeading;
        private DevComponents.Editors.DoubleInput dbiCharSpacing;
        private DevComponents.DotNetBar.LabelX labelLineLeading;
        private DevComponents.DotNetBar.LabelX labelCharSpacing;

    }
}