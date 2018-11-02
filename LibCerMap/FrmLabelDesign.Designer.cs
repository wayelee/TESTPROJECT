namespace LibCerMap
{
    partial class FrmLabelDesign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLabelDesign));
            this.chklabel = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.gPanelfield = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbfields = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnexpression = new DevComponents.DotNetBar.ButtonX();
            this.labPoint = new DevComponents.DotNetBar.LabelX();
            this.gPanelsymbol = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.toolText = new System.Windows.Forms.ToolStrip();
            this.toolBtnBold = new System.Windows.Forms.ToolStripButton();
            this.toolBtnIntend = new System.Windows.Forms.ToolStripButton();
            this.toolBtnUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolBtnStrikethrough = new System.Windows.Forms.ToolStripButton();
            this.btnsymbol = new DevComponents.DotNetBar.ButtonX();
            this.lblsymbol = new DevComponents.DotNetBar.LabelX();
            this.cmbsize = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.comboItem13 = new DevComponents.Editors.ComboItem();
            this.comboItem14 = new DevComponents.Editors.ComboItem();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.comboItem19 = new DevComponents.Editors.ComboItem();
            this.comboItem20 = new DevComponents.Editors.ComboItem();
            this.comboItem21 = new DevComponents.Editors.ComboItem();
            this.comboItem22 = new DevComponents.Editors.ComboItem();
            this.lblsize = new DevComponents.DotNetBar.LabelX();
            this.cmbfont = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labFont = new DevComponents.DotNetBar.LabelX();
            this.symbolcolor = new CustomColorPicker();
            this.lblcolor = new DevComponents.DotNetBar.LabelX();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.btnuse = new DevComponents.DotNetBar.ButtonX();
            this.btnConvetAnno = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkOverLap = new System.Windows.Forms.CheckBox();
            this.radioBelowRight = new System.Windows.Forms.RadioButton();
            this.radioBelowCenter = new System.Windows.Forms.RadioButton();
            this.radioBelowLeft = new System.Windows.Forms.RadioButton();
            this.radioCenterRight = new System.Windows.Forms.RadioButton();
            this.radioCenterTop = new System.Windows.Forms.RadioButton();
            this.radioCenterLeft = new System.Windows.Forms.RadioButton();
            this.radioAboveRight = new System.Windows.Forms.RadioButton();
            this.radioAboveCenter = new System.Windows.Forms.RadioButton();
            this.radioAboveLeft = new System.Windows.Forms.RadioButton();
            this.radioBtnHorizontal = new System.Windows.Forms.RadioButton();
            this.radioBtnPerpendicular = new System.Windows.Forms.RadioButton();
            this.radioBtnParallel = new System.Windows.Forms.RadioButton();
            this.gPanelfield.SuspendLayout();
            this.gPanelsymbol.SuspendLayout();
            this.toolText.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chklabel
            // 
            // 
            // 
            // 
            this.chklabel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chklabel.Location = new System.Drawing.Point(12, 12);
            this.chklabel.Name = "chklabel";
            this.chklabel.Size = new System.Drawing.Size(100, 23);
            this.chklabel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chklabel.TabIndex = 0;
            this.chklabel.Text = "标注本图层";
            this.chklabel.CheckedChanged += new System.EventHandler(this.chklabel_CheckedChanged);
            // 
            // gPanelfield
            // 
            this.gPanelfield.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelfield.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelfield.Controls.Add(this.cmbfields);
            this.gPanelfield.Controls.Add(this.btnexpression);
            this.gPanelfield.Controls.Add(this.labPoint);
            this.gPanelfield.Location = new System.Drawing.Point(12, 41);
            this.gPanelfield.Name = "gPanelfield";
            this.gPanelfield.Size = new System.Drawing.Size(357, 72);
            // 
            // 
            // 
            this.gPanelfield.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelfield.Style.BackColorGradientAngle = 90;
            this.gPanelfield.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelfield.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelfield.Style.BorderBottomWidth = 1;
            this.gPanelfield.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelfield.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelfield.Style.BorderLeftWidth = 1;
            this.gPanelfield.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelfield.Style.BorderRightWidth = 1;
            this.gPanelfield.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelfield.Style.BorderTopWidth = 1;
            this.gPanelfield.Style.CornerDiameter = 4;
            this.gPanelfield.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelfield.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelfield.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelfield.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelfield.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelfield.TabIndex = 15;
            this.gPanelfield.Text = "标注字段";
            // 
            // cmbfields
            // 
            this.cmbfields.DisplayMember = "Text";
            this.cmbfields.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbfields.FormattingEnabled = true;
            this.cmbfields.ItemHeight = 15;
            this.cmbfields.Location = new System.Drawing.Point(62, 10);
            this.cmbfields.Name = "cmbfields";
            this.cmbfields.Size = new System.Drawing.Size(177, 21);
            this.cmbfields.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbfields.TabIndex = 20;
            // 
            // btnexpression
            // 
            this.btnexpression.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnexpression.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnexpression.Location = new System.Drawing.Point(264, 10);
            this.btnexpression.Name = "btnexpression";
            this.btnexpression.Size = new System.Drawing.Size(68, 21);
            this.btnexpression.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnexpression.TabIndex = 19;
            this.btnexpression.Text = "组合标注";
            this.btnexpression.Click += new System.EventHandler(this.btnexpression_Click);
            // 
            // labPoint
            // 
            this.labPoint.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labPoint.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labPoint.Location = new System.Drawing.Point(12, 10);
            this.labPoint.Name = "labPoint";
            this.labPoint.Size = new System.Drawing.Size(44, 23);
            this.labPoint.TabIndex = 2;
            this.labPoint.Text = "字段：";
            // 
            // gPanelsymbol
            // 
            this.gPanelsymbol.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelsymbol.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelsymbol.Controls.Add(this.radioBtnParallel);
            this.gPanelsymbol.Controls.Add(this.radioBtnPerpendicular);
            this.gPanelsymbol.Controls.Add(this.radioBtnHorizontal);
            this.gPanelsymbol.Controls.Add(this.toolText);
            this.gPanelsymbol.Controls.Add(this.btnsymbol);
            this.gPanelsymbol.Controls.Add(this.lblsymbol);
            this.gPanelsymbol.Controls.Add(this.cmbsize);
            this.gPanelsymbol.Controls.Add(this.lblsize);
            this.gPanelsymbol.Controls.Add(this.cmbfont);
            this.gPanelsymbol.Controls.Add(this.labFont);
            this.gPanelsymbol.Controls.Add(this.symbolcolor);
            this.gPanelsymbol.Controls.Add(this.lblcolor);
            this.gPanelsymbol.Location = new System.Drawing.Point(12, 119);
            this.gPanelsymbol.Name = "gPanelsymbol";
            this.gPanelsymbol.Size = new System.Drawing.Size(357, 157);
            // 
            // 
            // 
            this.gPanelsymbol.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelsymbol.Style.BackColorGradientAngle = 90;
            this.gPanelsymbol.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelsymbol.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelsymbol.Style.BorderBottomWidth = 1;
            this.gPanelsymbol.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelsymbol.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelsymbol.Style.BorderLeftWidth = 1;
            this.gPanelsymbol.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelsymbol.Style.BorderRightWidth = 1;
            this.gPanelsymbol.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelsymbol.Style.BorderTopWidth = 1;
            this.gPanelsymbol.Style.CornerDiameter = 4;
            this.gPanelsymbol.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelsymbol.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelsymbol.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelsymbol.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelsymbol.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelsymbol.TabIndex = 16;
            this.gPanelsymbol.Text = "文本符号";
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
            this.toolText.Location = new System.Drawing.Point(209, 56);
            this.toolText.Name = "toolText";
            this.toolText.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolText.Size = new System.Drawing.Size(99, 25);
            this.toolText.TabIndex = 30;
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
            // btnsymbol
            // 
            this.btnsymbol.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnsymbol.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnsymbol.Location = new System.Drawing.Point(264, 16);
            this.btnsymbol.Name = "btnsymbol";
            this.btnsymbol.Size = new System.Drawing.Size(68, 21);
            this.btnsymbol.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnsymbol.TabIndex = 29;
            this.btnsymbol.Text = "符号";
            this.btnsymbol.Click += new System.EventHandler(this.btnsymbol_Click);
            // 
            // lblsymbol
            // 
            this.lblsymbol.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblsymbol.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblsymbol.Location = new System.Drawing.Point(173, 59);
            this.lblsymbol.Name = "lblsymbol";
            this.lblsymbol.Size = new System.Drawing.Size(44, 23);
            this.lblsymbol.TabIndex = 24;
            this.lblsymbol.Text = "样式：";
            // 
            // cmbsize
            // 
            this.cmbsize.DisplayMember = "Text";
            this.cmbsize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbsize.FormattingEnabled = true;
            this.cmbsize.ItemHeight = 15;
            this.cmbsize.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10,
            this.comboItem11,
            this.comboItem12,
            this.comboItem13,
            this.comboItem14,
            this.comboItem15,
            this.comboItem16,
            this.comboItem17,
            this.comboItem18,
            this.comboItem19,
            this.comboItem20,
            this.comboItem21,
            this.comboItem22});
            this.cmbsize.Location = new System.Drawing.Point(62, 60);
            this.cmbsize.Name = "cmbsize";
            this.cmbsize.Size = new System.Drawing.Size(95, 21);
            this.cmbsize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbsize.TabIndex = 23;
            this.cmbsize.SelectedIndexChanged += new System.EventHandler(this.cmbsize_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "5";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "6";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "7";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "8";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "9";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "10";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "11";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "12";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "14";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "16";
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "18";
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "20";
            // 
            // comboItem13
            // 
            this.comboItem13.Text = "22";
            // 
            // comboItem14
            // 
            this.comboItem14.Text = "24";
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "26";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "28";
            // 
            // comboItem17
            // 
            this.comboItem17.Text = "30";
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "32";
            // 
            // comboItem19
            // 
            this.comboItem19.Text = "34";
            // 
            // comboItem20
            // 
            this.comboItem20.Text = "36";
            // 
            // comboItem21
            // 
            this.comboItem21.Text = "48";
            // 
            // comboItem22
            // 
            this.comboItem22.Text = "72";
            // 
            // lblsize
            // 
            this.lblsize.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblsize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblsize.Location = new System.Drawing.Point(12, 59);
            this.lblsize.Name = "lblsize";
            this.lblsize.Size = new System.Drawing.Size(53, 23);
            this.lblsize.TabIndex = 22;
            this.lblsize.Text = "字号：";
            this.lblsize.Click += new System.EventHandler(this.lblsize_Click);
            // 
            // cmbfont
            // 
            this.cmbfont.DisplayMember = "Text";
            this.cmbfont.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbfont.FormattingEnabled = true;
            this.cmbfont.ItemHeight = 15;
            this.cmbfont.Location = new System.Drawing.Point(62, 16);
            this.cmbfont.Name = "cmbfont";
            this.cmbfont.Size = new System.Drawing.Size(95, 21);
            this.cmbfont.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbfont.TabIndex = 21;
            // 
            // labFont
            // 
            this.labFont.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labFont.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labFont.Location = new System.Drawing.Point(13, 15);
            this.labFont.Name = "labFont";
            this.labFont.Size = new System.Drawing.Size(43, 23);
            this.labFont.TabIndex = 20;
            this.labFont.Text = "字体：";
            // 
            // symbolcolor
            // 
            this.symbolcolor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.symbolcolor.BackColor = System.Drawing.SystemColors.Control;
            this.symbolcolor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.symbolcolor.Image = ((System.Drawing.Image)(resources.GetObject("symbolcolor.Image")));
            this.symbolcolor.Location = new System.Drawing.Point(209, 16);
            this.symbolcolor.Name = "symbolcolor";
            this.symbolcolor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.symbolcolor.Size = new System.Drawing.Size(37, 21);
            this.symbolcolor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.symbolcolor.TabIndex = 3;
            // 
            // lblcolor
            // 
            this.lblcolor.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblcolor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblcolor.Location = new System.Drawing.Point(173, 15);
            this.lblcolor.Name = "lblcolor";
            this.lblcolor.Size = new System.Drawing.Size(44, 23);
            this.lblcolor.TabIndex = 2;
            this.lblcolor.Text = "颜色：";
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(149, 404);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(68, 25);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 17;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(224, 404);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(68, 25);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 18;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // btnuse
            // 
            this.btnuse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnuse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnuse.Location = new System.Drawing.Point(302, 404);
            this.btnuse.Name = "btnuse";
            this.btnuse.Size = new System.Drawing.Size(68, 25);
            this.btnuse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnuse.TabIndex = 19;
            this.btnuse.Text = "应用";
            this.btnuse.Click += new System.EventHandler(this.btnuse_Click);
            // 
            // btnConvetAnno
            // 
            this.btnConvetAnno.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConvetAnno.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConvetAnno.Location = new System.Drawing.Point(261, 12);
            this.btnConvetAnno.Name = "btnConvetAnno";
            this.btnConvetAnno.Size = new System.Drawing.Size(108, 23);
            this.btnConvetAnno.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConvetAnno.TabIndex = 20;
            this.btnConvetAnno.Text = "创建注记图层";
            this.btnConvetAnno.Click += new System.EventHandler(this.btnConvetAnno_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.chkOverLap);
            this.groupPanel1.Controls.Add(this.radioBelowRight);
            this.groupPanel1.Controls.Add(this.radioBelowCenter);
            this.groupPanel1.Controls.Add(this.radioBelowLeft);
            this.groupPanel1.Controls.Add(this.radioCenterRight);
            this.groupPanel1.Controls.Add(this.radioCenterTop);
            this.groupPanel1.Controls.Add(this.radioCenterLeft);
            this.groupPanel1.Controls.Add(this.radioAboveRight);
            this.groupPanel1.Controls.Add(this.radioAboveCenter);
            this.groupPanel1.Controls.Add(this.radioAboveLeft);
            this.groupPanel1.Location = new System.Drawing.Point(13, 282);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(356, 103);
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
            this.groupPanel1.TabIndex = 21;
            this.groupPanel1.Text = "标注位置";
            // 
            // chkOverLap
            // 
            this.chkOverLap.AutoSize = true;
            this.chkOverLap.Checked = true;
            this.chkOverLap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverLap.Location = new System.Drawing.Point(208, 47);
            this.chkOverLap.Name = "chkOverLap";
            this.chkOverLap.Size = new System.Drawing.Size(72, 16);
            this.chkOverLap.TabIndex = 1;
            this.chkOverLap.Text = "重叠放置";
            this.chkOverLap.UseVisualStyleBackColor = true;
            // 
            // radioBelowRight
            // 
            this.radioBelowRight.AutoSize = true;
            this.radioBelowRight.BackColor = System.Drawing.Color.Transparent;
            this.radioBelowRight.Location = new System.Drawing.Point(144, 47);
            this.radioBelowRight.Name = "radioBelowRight";
            this.radioBelowRight.Size = new System.Drawing.Size(47, 16);
            this.radioBelowRight.TabIndex = 0;
            this.radioBelowRight.Text = "右下";
            this.radioBelowRight.UseVisualStyleBackColor = false;
            // 
            // radioBelowCenter
            // 
            this.radioBelowCenter.AutoSize = true;
            this.radioBelowCenter.BackColor = System.Drawing.Color.Transparent;
            this.radioBelowCenter.Checked = true;
            this.radioBelowCenter.Location = new System.Drawing.Point(87, 47);
            this.radioBelowCenter.Name = "radioBelowCenter";
            this.radioBelowCenter.Size = new System.Drawing.Size(47, 16);
            this.radioBelowCenter.TabIndex = 0;
            this.radioBelowCenter.TabStop = true;
            this.radioBelowCenter.Text = "下中";
            this.radioBelowCenter.UseVisualStyleBackColor = false;
            // 
            // radioBelowLeft
            // 
            this.radioBelowLeft.AutoSize = true;
            this.radioBelowLeft.BackColor = System.Drawing.Color.Transparent;
            this.radioBelowLeft.Location = new System.Drawing.Point(34, 47);
            this.radioBelowLeft.Name = "radioBelowLeft";
            this.radioBelowLeft.Size = new System.Drawing.Size(47, 16);
            this.radioBelowLeft.TabIndex = 0;
            this.radioBelowLeft.Text = "左下";
            this.radioBelowLeft.UseVisualStyleBackColor = false;
            // 
            // radioCenterRight
            // 
            this.radioCenterRight.AutoSize = true;
            this.radioCenterRight.BackColor = System.Drawing.Color.Transparent;
            this.radioCenterRight.Location = new System.Drawing.Point(144, 25);
            this.radioCenterRight.Name = "radioCenterRight";
            this.radioCenterRight.Size = new System.Drawing.Size(47, 16);
            this.radioCenterRight.TabIndex = 0;
            this.radioCenterRight.Text = "右中";
            this.radioCenterRight.UseVisualStyleBackColor = false;
            // 
            // radioCenterTop
            // 
            this.radioCenterTop.AutoSize = true;
            this.radioCenterTop.BackColor = System.Drawing.Color.Transparent;
            this.radioCenterTop.Location = new System.Drawing.Point(88, 25);
            this.radioCenterTop.Name = "radioCenterTop";
            this.radioCenterTop.Size = new System.Drawing.Size(47, 16);
            this.radioCenterTop.TabIndex = 0;
            this.radioCenterTop.Text = "中心";
            this.radioCenterTop.UseVisualStyleBackColor = false;
            // 
            // radioCenterLeft
            // 
            this.radioCenterLeft.AutoSize = true;
            this.radioCenterLeft.BackColor = System.Drawing.Color.Transparent;
            this.radioCenterLeft.Location = new System.Drawing.Point(34, 25);
            this.radioCenterLeft.Name = "radioCenterLeft";
            this.radioCenterLeft.Size = new System.Drawing.Size(47, 16);
            this.radioCenterLeft.TabIndex = 0;
            this.radioCenterLeft.Text = "左中";
            this.radioCenterLeft.UseVisualStyleBackColor = false;
            // 
            // radioAboveRight
            // 
            this.radioAboveRight.AutoSize = true;
            this.radioAboveRight.BackColor = System.Drawing.Color.Transparent;
            this.radioAboveRight.Location = new System.Drawing.Point(144, 3);
            this.radioAboveRight.Name = "radioAboveRight";
            this.radioAboveRight.Size = new System.Drawing.Size(47, 16);
            this.radioAboveRight.TabIndex = 0;
            this.radioAboveRight.Text = "右上";
            this.radioAboveRight.UseVisualStyleBackColor = false;
            // 
            // radioAboveCenter
            // 
            this.radioAboveCenter.AutoSize = true;
            this.radioAboveCenter.BackColor = System.Drawing.Color.Transparent;
            this.radioAboveCenter.Location = new System.Drawing.Point(88, 3);
            this.radioAboveCenter.Name = "radioAboveCenter";
            this.radioAboveCenter.Size = new System.Drawing.Size(47, 16);
            this.radioAboveCenter.TabIndex = 0;
            this.radioAboveCenter.Text = "上中";
            this.radioAboveCenter.UseVisualStyleBackColor = false;
            // 
            // radioAboveLeft
            // 
            this.radioAboveLeft.AutoSize = true;
            this.radioAboveLeft.BackColor = System.Drawing.Color.Transparent;
            this.radioAboveLeft.Location = new System.Drawing.Point(34, 3);
            this.radioAboveLeft.Name = "radioAboveLeft";
            this.radioAboveLeft.Size = new System.Drawing.Size(47, 16);
            this.radioAboveLeft.TabIndex = 0;
            this.radioAboveLeft.Text = "左上";
            this.radioAboveLeft.UseVisualStyleBackColor = false;
            // 
            // radioBtnHorizontal
            // 
            this.radioBtnHorizontal.AutoSize = true;
            this.radioBtnHorizontal.Location = new System.Drawing.Point(13, 102);
            this.radioBtnHorizontal.Name = "radioBtnHorizontal";
            this.radioBtnHorizontal.Size = new System.Drawing.Size(47, 16);
            this.radioBtnHorizontal.TabIndex = 31;
            this.radioBtnHorizontal.TabStop = true;
            this.radioBtnHorizontal.Text = "水平";
            this.radioBtnHorizontal.UseVisualStyleBackColor = true;
            // 
            // radioBtnPerpendicular
            // 
            this.radioBtnPerpendicular.AutoSize = true;
            this.radioBtnPerpendicular.Location = new System.Drawing.Point(133, 102);
            this.radioBtnPerpendicular.Name = "radioBtnPerpendicular";
            this.radioBtnPerpendicular.Size = new System.Drawing.Size(47, 16);
            this.radioBtnPerpendicular.TabIndex = 31;
            this.radioBtnPerpendicular.TabStop = true;
            this.radioBtnPerpendicular.Text = "垂直";
            this.radioBtnPerpendicular.UseVisualStyleBackColor = true;
            // 
            // radioBtnParallel
            // 
            this.radioBtnParallel.AutoSize = true;
            this.radioBtnParallel.Checked = true;
            this.radioBtnParallel.Location = new System.Drawing.Point(253, 102);
            this.radioBtnParallel.Name = "radioBtnParallel";
            this.radioBtnParallel.Size = new System.Drawing.Size(47, 16);
            this.radioBtnParallel.TabIndex = 31;
            this.radioBtnParallel.TabStop = true;
            this.radioBtnParallel.Text = "平行";
            this.radioBtnParallel.UseVisualStyleBackColor = true;
            // 
            // FrmLabelDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 446);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnConvetAnno);
            this.Controls.Add(this.btnuse);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.gPanelsymbol);
            this.Controls.Add(this.gPanelfield);
            this.Controls.Add(this.chklabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLabelDesign";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层注记";
            this.Load += new System.EventHandler(this.FrmLabelDesign_Load);
            this.gPanelfield.ResumeLayout(false);
            this.gPanelsymbol.ResumeLayout(false);
            this.gPanelsymbol.PerformLayout();
            this.toolText.ResumeLayout(false);
            this.toolText.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CheckBoxX chklabel;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelfield;
        private DevComponents.DotNetBar.ButtonX btnexpression;
        private DevComponents.DotNetBar.LabelX labPoint;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelsymbol;
        private CustomColorPicker symbolcolor;
        private DevComponents.DotNetBar.LabelX lblcolor;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbfields;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbfont;
        private DevComponents.DotNetBar.LabelX labFont;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbsize;
        private DevComponents.DotNetBar.LabelX lblsize;
        private DevComponents.DotNetBar.ButtonX btnsymbol;
        private DevComponents.DotNetBar.LabelX lblsymbol;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.ButtonX btnuse;
        private System.Windows.Forms.ToolStrip toolText;
        private System.Windows.Forms.ToolStripButton toolBtnBold;
        private System.Windows.Forms.ToolStripButton toolBtnIntend;
        private System.Windows.Forms.ToolStripButton toolBtnUnderline;
        private System.Windows.Forms.ToolStripButton toolBtnStrikethrough;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.ComboItem comboItem13;
        private DevComponents.Editors.ComboItem comboItem14;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.Editors.ComboItem comboItem18;
        private DevComponents.Editors.ComboItem comboItem19;
        private DevComponents.Editors.ComboItem comboItem20;
        private DevComponents.Editors.ComboItem comboItem21;
        private DevComponents.Editors.ComboItem comboItem22;
        private DevComponents.DotNetBar.ButtonX btnConvetAnno;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.RadioButton radioBelowRight;
        private System.Windows.Forms.RadioButton radioBelowCenter;
        private System.Windows.Forms.RadioButton radioBelowLeft;
        private System.Windows.Forms.RadioButton radioCenterRight;
        private System.Windows.Forms.RadioButton radioCenterTop;
        private System.Windows.Forms.RadioButton radioCenterLeft;
        private System.Windows.Forms.RadioButton radioAboveRight;
        private System.Windows.Forms.RadioButton radioAboveCenter;
        private System.Windows.Forms.RadioButton radioAboveLeft;
        private System.Windows.Forms.CheckBox chkOverLap;
        private System.Windows.Forms.RadioButton radioBtnParallel;
        private System.Windows.Forms.RadioButton radioBtnPerpendicular;
        private System.Windows.Forms.RadioButton radioBtnHorizontal;
    }
}