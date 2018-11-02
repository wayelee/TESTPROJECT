namespace LibCerMap
{
    partial class FrmRandomTerrain
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
            this.sldUndulation = new DevComponents.DotNetBar.Controls.Slider();
            this.lblTerrainUndulation = new DevComponents.DotNetBar.LabelX();
            this.lblTerrainResolution = new DevComponents.DotNetBar.LabelX();
            this.grpTerrainRange = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.comboBoxExLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnReferToLayer = new DevComponents.DotNetBar.ButtonX();
            this.lblRightBottom = new DevComponents.DotNetBar.LabelX();
            this.lblY = new DevComponents.DotNetBar.LabelX();
            this.chkReferToLayer = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblX = new DevComponents.DotNetBar.LabelX();
            this.chkManualInput = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblLeftTop = new DevComponents.DotNetBar.LabelX();
            this.dbiRightBottomY = new DevComponents.Editors.DoubleInput();
            this.dbiLeftTopY = new DevComponents.Editors.DoubleInput();
            this.dbiRightBottomX = new DevComponents.Editors.DoubleInput();
            this.dbiLeftTopX = new DevComponents.Editors.DoubleInput();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.dbiResolution = new DevComponents.Editors.DoubleInput();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.iniUndulation = new DevComponents.Editors.IntegerInput();
            this.lblOutputFilename = new DevComponents.DotNetBar.LabelX();
            this.txtOutputFilename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnOutputFilename = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.checkBoxXAddModel = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtRockCount = new DevComponents.Editors.IntegerInput();
            this.sldCraterCount = new DevComponents.DotNetBar.Controls.Slider();
            this.txtCraterCount = new DevComponents.Editors.IntegerInput();
            this.sldRockCount = new DevComponents.DotNetBar.Controls.Slider();
            this.lblRockCount = new DevComponents.DotNetBar.LabelX();
            this.lblCraterCount = new DevComponents.DotNetBar.LabelX();
            this.grpTerrainRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbiRightBottomY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiLeftTopY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiRightBottomX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiLeftTopX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iniUndulation)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRockCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCraterCount)).BeginInit();
            this.SuspendLayout();
            // 
            // sldUndulation
            // 
            // 
            // 
            // 
            this.sldUndulation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sldUndulation.LabelVisible = false;
            this.sldUndulation.Location = new System.Drawing.Point(219, 252);
            this.sldUndulation.Maximum = 500;
            this.sldUndulation.Name = "sldUndulation";
            this.sldUndulation.Size = new System.Drawing.Size(165, 23);
            this.sldUndulation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sldUndulation.TabIndex = 0;
            this.sldUndulation.Value = 50;
            this.sldUndulation.ValueChanged += new System.EventHandler(this.sldUndulation_ValueChanged);
            // 
            // lblTerrainUndulation
            // 
            this.lblTerrainUndulation.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTerrainUndulation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTerrainUndulation.Location = new System.Drawing.Point(21, 252);
            this.lblTerrainUndulation.Name = "lblTerrainUndulation";
            this.lblTerrainUndulation.Size = new System.Drawing.Size(75, 23);
            this.lblTerrainUndulation.TabIndex = 1;
            this.lblTerrainUndulation.Text = "起伏程度:";
            // 
            // lblTerrainResolution
            // 
            this.lblTerrainResolution.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTerrainResolution.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTerrainResolution.Location = new System.Drawing.Point(21, 207);
            this.lblTerrainResolution.Name = "lblTerrainResolution";
            this.lblTerrainResolution.Size = new System.Drawing.Size(75, 23);
            this.lblTerrainResolution.TabIndex = 1;
            this.lblTerrainResolution.Text = "分辨率(m):";
            // 
            // grpTerrainRange
            // 
            this.grpTerrainRange.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpTerrainRange.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grpTerrainRange.Controls.Add(this.comboBoxExLayer);
            this.grpTerrainRange.Controls.Add(this.btnReferToLayer);
            this.grpTerrainRange.Controls.Add(this.lblRightBottom);
            this.grpTerrainRange.Controls.Add(this.lblY);
            this.grpTerrainRange.Controls.Add(this.chkReferToLayer);
            this.grpTerrainRange.Controls.Add(this.lblX);
            this.grpTerrainRange.Controls.Add(this.chkManualInput);
            this.grpTerrainRange.Controls.Add(this.lblLeftTop);
            this.grpTerrainRange.Controls.Add(this.dbiRightBottomY);
            this.grpTerrainRange.Controls.Add(this.dbiLeftTopY);
            this.grpTerrainRange.Controls.Add(this.dbiRightBottomX);
            this.grpTerrainRange.Controls.Add(this.dbiLeftTopX);
            this.grpTerrainRange.Location = new System.Drawing.Point(12, 12);
            this.grpTerrainRange.Name = "grpTerrainRange";
            this.grpTerrainRange.Size = new System.Drawing.Size(494, 176);
            // 
            // 
            // 
            this.grpTerrainRange.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpTerrainRange.Style.BackColorGradientAngle = 90;
            this.grpTerrainRange.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpTerrainRange.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpTerrainRange.Style.BorderBottomWidth = 1;
            this.grpTerrainRange.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpTerrainRange.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpTerrainRange.Style.BorderLeftWidth = 1;
            this.grpTerrainRange.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpTerrainRange.Style.BorderRightWidth = 1;
            this.grpTerrainRange.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpTerrainRange.Style.BorderTopWidth = 1;
            this.grpTerrainRange.Style.CornerDiameter = 4;
            this.grpTerrainRange.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpTerrainRange.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpTerrainRange.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpTerrainRange.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpTerrainRange.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpTerrainRange.TabIndex = 2;
            this.grpTerrainRange.Text = "地理范围";
            // 
            // comboBoxExLayer
            // 
            this.comboBoxExLayer.DisplayMember = "Text";
            this.comboBoxExLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExLayer.FormattingEnabled = true;
            this.comboBoxExLayer.ItemHeight = 15;
            this.comboBoxExLayer.Location = new System.Drawing.Point(173, 109);
            this.comboBoxExLayer.Name = "comboBoxExLayer";
            this.comboBoxExLayer.Size = new System.Drawing.Size(121, 21);
            this.comboBoxExLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExLayer.TabIndex = 5;
            this.comboBoxExLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxExLayer_SelectedIndexChanged);
            // 
            // btnReferToLayer
            // 
            this.btnReferToLayer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReferToLayer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReferToLayer.Location = new System.Drawing.Point(386, 107);
            this.btnReferToLayer.Name = "btnReferToLayer";
            this.btnReferToLayer.Size = new System.Drawing.Size(75, 23);
            this.btnReferToLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReferToLayer.TabIndex = 3;
            this.btnReferToLayer.Text = "...";
            this.btnReferToLayer.Visible = false;
            // 
            // lblRightBottom
            // 
            this.lblRightBottom.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblRightBottom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRightBottom.Location = new System.Drawing.Point(173, 64);
            this.lblRightBottom.Name = "lblRightBottom";
            this.lblRightBottom.Size = new System.Drawing.Size(75, 25);
            this.lblRightBottom.TabIndex = 4;
            this.lblRightBottom.Text = "右下角：";
            // 
            // lblY
            // 
            this.lblY.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblY.Location = new System.Drawing.Point(381, 6);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(75, 25);
            this.lblY.TabIndex = 4;
            this.lblY.Text = "Y(m):";
            // 
            // chkReferToLayer
            // 
            this.chkReferToLayer.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkReferToLayer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkReferToLayer.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkReferToLayer.Location = new System.Drawing.Point(50, 107);
            this.chkReferToLayer.Name = "chkReferToLayer";
            this.chkReferToLayer.Size = new System.Drawing.Size(100, 23);
            this.chkReferToLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkReferToLayer.TabIndex = 4;
            this.chkReferToLayer.Text = "从图层获取:";
            this.chkReferToLayer.CheckedChanged += new System.EventHandler(this.chkReferToLayer_CheckedChanged);
            // 
            // lblX
            // 
            this.lblX.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblX.Location = new System.Drawing.Point(280, 6);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(75, 25);
            this.lblX.TabIndex = 4;
            this.lblX.Text = "X(m):";
            // 
            // chkManualInput
            // 
            this.chkManualInput.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkManualInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkManualInput.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkManualInput.Checked = true;
            this.chkManualInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkManualInput.CheckValue = "Y";
            this.chkManualInput.Location = new System.Drawing.Point(50, 49);
            this.chkManualInput.Name = "chkManualInput";
            this.chkManualInput.Size = new System.Drawing.Size(100, 23);
            this.chkManualInput.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkManualInput.TabIndex = 4;
            this.chkManualInput.Text = "手动指定:";
            this.chkManualInput.CheckedChanged += new System.EventHandler(this.chkManualInput_CheckedChanged);
            // 
            // lblLeftTop
            // 
            this.lblLeftTop.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblLeftTop.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblLeftTop.Location = new System.Drawing.Point(173, 33);
            this.lblLeftTop.Name = "lblLeftTop";
            this.lblLeftTop.Size = new System.Drawing.Size(75, 25);
            this.lblLeftTop.TabIndex = 4;
            this.lblLeftTop.Text = "左上角：";
            // 
            // dbiRightBottomY
            // 
            // 
            // 
            // 
            this.dbiRightBottomY.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiRightBottomY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiRightBottomY.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiRightBottomY.Increment = 1D;
            this.dbiRightBottomY.Location = new System.Drawing.Point(381, 64);
            this.dbiRightBottomY.Name = "dbiRightBottomY";
            this.dbiRightBottomY.ShowUpDown = true;
            this.dbiRightBottomY.Size = new System.Drawing.Size(80, 21);
            this.dbiRightBottomY.TabIndex = 4;
            this.dbiRightBottomY.Value = 100D;
            // 
            // dbiLeftTopY
            // 
            // 
            // 
            // 
            this.dbiLeftTopY.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiLeftTopY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiLeftTopY.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiLeftTopY.Increment = 1D;
            this.dbiLeftTopY.Location = new System.Drawing.Point(381, 38);
            this.dbiLeftTopY.Name = "dbiLeftTopY";
            this.dbiLeftTopY.ShowUpDown = true;
            this.dbiLeftTopY.Size = new System.Drawing.Size(80, 21);
            this.dbiLeftTopY.TabIndex = 2;
            // 
            // dbiRightBottomX
            // 
            // 
            // 
            // 
            this.dbiRightBottomX.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiRightBottomX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiRightBottomX.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiRightBottomX.Increment = 1D;
            this.dbiRightBottomX.Location = new System.Drawing.Point(280, 64);
            this.dbiRightBottomX.Name = "dbiRightBottomX";
            this.dbiRightBottomX.ShowUpDown = true;
            this.dbiRightBottomX.Size = new System.Drawing.Size(80, 21);
            this.dbiRightBottomX.TabIndex = 3;
            this.dbiRightBottomX.Value = 100D;
            // 
            // dbiLeftTopX
            // 
            // 
            // 
            // 
            this.dbiLeftTopX.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiLeftTopX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiLeftTopX.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiLeftTopX.Increment = 1D;
            this.dbiLeftTopX.Location = new System.Drawing.Point(280, 37);
            this.dbiLeftTopX.Name = "dbiLeftTopX";
            this.dbiLeftTopX.ShowUpDown = true;
            this.dbiLeftTopX.Size = new System.Drawing.Size(80, 21);
            this.dbiLeftTopX.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(331, 494);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dbiResolution
            // 
            // 
            // 
            // 
            this.dbiResolution.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiResolution.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiResolution.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiResolution.Increment = 1D;
            this.dbiResolution.Location = new System.Drawing.Point(102, 207);
            this.dbiResolution.MinValue = 0D;
            this.dbiResolution.Name = "dbiResolution";
            this.dbiResolution.ShowUpDown = true;
            this.dbiResolution.Size = new System.Drawing.Size(80, 21);
            this.dbiResolution.TabIndex = 5;
            this.dbiResolution.Value = 0.2D;
            this.dbiResolution.ValueChanged += new System.EventHandler(this.dbiResolution_ValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(421, 494);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            // 
            // iniUndulation
            // 
            // 
            // 
            // 
            this.iniUndulation.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iniUndulation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iniUndulation.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iniUndulation.Location = new System.Drawing.Point(102, 252);
            this.iniUndulation.MaxValue = 500;
            this.iniUndulation.MinValue = 0;
            this.iniUndulation.Name = "iniUndulation";
            this.iniUndulation.ShowUpDown = true;
            this.iniUndulation.Size = new System.Drawing.Size(80, 21);
            this.iniUndulation.TabIndex = 6;
            this.iniUndulation.Value = 50;
            this.iniUndulation.ValueChanged += new System.EventHandler(this.iniUndulation_ValueChanged);
            // 
            // lblOutputFilename
            // 
            this.lblOutputFilename.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblOutputFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblOutputFilename.Location = new System.Drawing.Point(12, 443);
            this.lblOutputFilename.Name = "lblOutputFilename";
            this.lblOutputFilename.Size = new System.Drawing.Size(75, 23);
            this.lblOutputFilename.TabIndex = 6;
            this.lblOutputFilename.Text = "输出路径：";
            // 
            // txtOutputFilename
            // 
            // 
            // 
            // 
            this.txtOutputFilename.Border.Class = "TextBoxBorder";
            this.txtOutputFilename.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutputFilename.Location = new System.Drawing.Point(112, 443);
            this.txtOutputFilename.Name = "txtOutputFilename";
            this.txtOutputFilename.Size = new System.Drawing.Size(351, 21);
            this.txtOutputFilename.TabIndex = 7;
            // 
            // btnOutputFilename
            // 
            this.btnOutputFilename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutputFilename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutputFilename.Location = new System.Drawing.Point(469, 443);
            this.btnOutputFilename.Name = "btnOutputFilename";
            this.btnOutputFilename.Size = new System.Drawing.Size(44, 23);
            this.btnOutputFilename.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOutputFilename.TabIndex = 8;
            this.btnOutputFilename.Text = "...";
            this.btnOutputFilename.Click += new System.EventHandler(this.btnOutputFilename_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.sldUndulation);
            this.groupPanel1.Controls.Add(this.dbiResolution);
            this.groupPanel1.Controls.Add(this.lblTerrainUndulation);
            this.groupPanel1.Controls.Add(this.lblTerrainResolution);
            this.groupPanel1.Controls.Add(this.iniUndulation);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(529, 292);
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
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
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
            this.groupPanel1.TabIndex = 10;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.checkBoxXAddModel);
            this.groupPanel2.Controls.Add(this.txtRockCount);
            this.groupPanel2.Controls.Add(this.sldCraterCount);
            this.groupPanel2.Controls.Add(this.txtCraterCount);
            this.groupPanel2.Controls.Add(this.sldRockCount);
            this.groupPanel2.Controls.Add(this.lblRockCount);
            this.groupPanel2.Controls.Add(this.lblCraterCount);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel2.Location = new System.Drawing.Point(0, 292);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(529, 136);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 11;
            // 
            // checkBoxXAddModel
            // 
            // 
            // 
            // 
            this.checkBoxXAddModel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxXAddModel.Checked = true;
            this.checkBoxXAddModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxXAddModel.CheckValue = "Y";
            this.checkBoxXAddModel.Location = new System.Drawing.Point(19, 4);
            this.checkBoxXAddModel.Name = "checkBoxXAddModel";
            this.checkBoxXAddModel.Size = new System.Drawing.Size(160, 23);
            this.checkBoxXAddModel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxXAddModel.TabIndex = 5;
            this.checkBoxXAddModel.Text = "随机添加撞击坑和石块";
            // 
            // txtRockCount
            // 
            // 
            // 
            // 
            this.txtRockCount.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtRockCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRockCount.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtRockCount.Location = new System.Drawing.Point(99, 44);
            this.txtRockCount.Name = "txtRockCount";
            this.txtRockCount.ShowUpDown = true;
            this.txtRockCount.Size = new System.Drawing.Size(80, 21);
            this.txtRockCount.TabIndex = 1;
            this.txtRockCount.Value = 5;
            this.txtRockCount.ValueChanged += new System.EventHandler(this.txtRockCount_ValueChanged);
            // 
            // sldCraterCount
            // 
            this.sldCraterCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.sldCraterCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sldCraterCount.LabelVisible = false;
            this.sldCraterCount.Location = new System.Drawing.Point(202, 95);
            this.sldCraterCount.Name = "sldCraterCount";
            this.sldCraterCount.Size = new System.Drawing.Size(188, 23);
            this.sldCraterCount.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sldCraterCount.TabIndex = 4;
            this.sldCraterCount.TabStop = false;
            this.sldCraterCount.Value = 5;
            this.sldCraterCount.ValueChanged += new System.EventHandler(this.sldCraterCount_ValueChanged);
            // 
            // txtCraterCount
            // 
            // 
            // 
            // 
            this.txtCraterCount.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtCraterCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCraterCount.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtCraterCount.Location = new System.Drawing.Point(99, 95);
            this.txtCraterCount.Name = "txtCraterCount";
            this.txtCraterCount.ShowUpDown = true;
            this.txtCraterCount.Size = new System.Drawing.Size(80, 21);
            this.txtCraterCount.TabIndex = 2;
            this.txtCraterCount.Value = 5;
            this.txtCraterCount.ValueChanged += new System.EventHandler(this.txtCraterCount_ValueChanged);
            // 
            // sldRockCount
            // 
            this.sldRockCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.sldRockCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sldRockCount.LabelVisible = false;
            this.sldRockCount.Location = new System.Drawing.Point(202, 41);
            this.sldRockCount.Name = "sldRockCount";
            this.sldRockCount.Size = new System.Drawing.Size(188, 23);
            this.sldRockCount.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sldRockCount.TabIndex = 3;
            this.sldRockCount.TabStop = false;
            this.sldRockCount.Value = 5;
            this.sldRockCount.ValueChanged += new System.EventHandler(this.sldRockCount_ValueChanged);
            // 
            // lblRockCount
            // 
            this.lblRockCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblRockCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRockCount.Location = new System.Drawing.Point(19, 44);
            this.lblRockCount.Name = "lblRockCount";
            this.lblRockCount.Size = new System.Drawing.Size(82, 23);
            this.lblRockCount.TabIndex = 0;
            this.lblRockCount.Text = "石块数量：";
            // 
            // lblCraterCount
            // 
            this.lblCraterCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCraterCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCraterCount.Location = new System.Drawing.Point(19, 95);
            this.lblCraterCount.Name = "lblCraterCount";
            this.lblCraterCount.Size = new System.Drawing.Size(82, 23);
            this.lblCraterCount.TabIndex = 0;
            this.lblCraterCount.Text = "撞击坑数量：";
            // 
            // FrmRandomTerrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 537);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.btnOutputFilename);
            this.Controls.Add(this.txtOutputFilename);
            this.Controls.Add(this.lblOutputFilename);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpTerrainRange);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRandomTerrain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地形信息";
            this.Load += new System.EventHandler(this.FrmRandomTerrain_Load);
            this.grpTerrainRange.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbiRightBottomY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiLeftTopY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiRightBottomX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiLeftTopX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiResolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iniUndulation)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRockCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCraterCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Slider sldUndulation;
        private DevComponents.DotNetBar.LabelX lblTerrainUndulation;
        private DevComponents.DotNetBar.LabelX lblTerrainResolution;
        private DevComponents.DotNetBar.Controls.GroupPanel grpTerrainRange;
        private DevComponents.Editors.DoubleInput dbiRightBottomY;
        private DevComponents.Editors.DoubleInput dbiLeftTopY;
        private DevComponents.Editors.DoubleInput dbiRightBottomX;
        private DevComponents.Editors.DoubleInput dbiLeftTopX;
        private DevComponents.Editors.DoubleInput dbiResolution;
        private DevComponents.DotNetBar.ButtonX btnReferToLayer;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkManualInput;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkReferToLayer;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.Editors.IntegerInput iniUndulation;
        private DevComponents.DotNetBar.LabelX lblLeftTop;
        private DevComponents.DotNetBar.LabelX lblRightBottom;
        private DevComponents.DotNetBar.LabelX lblY;
        private DevComponents.DotNetBar.LabelX lblX;
        private DevComponents.DotNetBar.LabelX lblOutputFilename;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutputFilename;
        private DevComponents.DotNetBar.ButtonX btnOutputFilename;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExLayer;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxXAddModel;
        private DevComponents.Editors.IntegerInput txtRockCount;
        private DevComponents.DotNetBar.Controls.Slider sldCraterCount;
        private DevComponents.Editors.IntegerInput txtCraterCount;
        private DevComponents.DotNetBar.Controls.Slider sldRockCount;
        private DevComponents.DotNetBar.LabelX lblRockCount;
        private DevComponents.DotNetBar.LabelX lblCraterCount;
    }
}