namespace LibCerMap
{
    partial class FrmExportRaster
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
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.txtOutData = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.grpanelcolormu = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rdoWorkspace = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.rdoLayer = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.BtnWorkBrowse = new DevComponents.DotNetBar.ButtonX();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.comboBoxExBands = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbPixelType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbResample = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.textNoData = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtOutRows = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtOutColumns = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCellSizeY = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCellSizeX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkSocCellSize = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkRasterSize = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkCellSize = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.grpanelcolormu.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.Location = new System.Drawing.Point(329, 411);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(85, 26);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 37;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Location = new System.Drawing.Point(223, 411);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 26);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 36;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtOutData
            // 
            // 
            // 
            // 
            this.txtOutData.Border.Class = "TextBoxBorder";
            this.txtOutData.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutData.Location = new System.Drawing.Point(3, 17);
            this.txtOutData.Name = "txtOutData";
            this.txtOutData.Size = new System.Drawing.Size(346, 21);
            this.txtOutData.TabIndex = 34;
            // 
            // grpanelcolormu
            // 
            this.grpanelcolormu.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpanelcolormu.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grpanelcolormu.Controls.Add(this.rdoWorkspace);
            this.grpanelcolormu.Controls.Add(this.rdoLayer);
            this.grpanelcolormu.Location = new System.Drawing.Point(12, 12);
            this.grpanelcolormu.Name = "grpanelcolormu";
            this.grpanelcolormu.Size = new System.Drawing.Size(405, 73);
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
            this.grpanelcolormu.TabIndex = 32;
            this.grpanelcolormu.Text = "坐标系统";
            // 
            // rdoWorkspace
            // 
            this.rdoWorkspace.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.rdoWorkspace.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rdoWorkspace.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rdoWorkspace.Location = new System.Drawing.Point(197, 14);
            this.rdoWorkspace.Name = "rdoWorkspace";
            this.rdoWorkspace.Size = new System.Drawing.Size(123, 23);
            this.rdoWorkspace.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rdoWorkspace.TabIndex = 1;
            this.rdoWorkspace.Text = "与工作空间相同";
            this.rdoWorkspace.CheckedChanged += new System.EventHandler(this.rdoWorkspace_CheckedChanged);
            this.rdoWorkspace.Click += new System.EventHandler(this.rdoWorkspace_Click);
            // 
            // rdoLayer
            // 
            this.rdoLayer.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.rdoLayer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rdoLayer.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rdoLayer.Location = new System.Drawing.Point(25, 14);
            this.rdoLayer.Name = "rdoLayer";
            this.rdoLayer.Size = new System.Drawing.Size(100, 23);
            this.rdoLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rdoLayer.TabIndex = 0;
            this.rdoLayer.Text = "与原图层相同";
            this.rdoLayer.CheckedChanged += new System.EventHandler(this.rdoLayer_CheckedChanged);
            this.rdoLayer.Click += new System.EventHandler(this.rdoLayer_Click);
            // 
            // BtnWorkBrowse
            // 
            this.BtnWorkBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnWorkBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnWorkBrowse.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.BtnWorkBrowse.Location = new System.Drawing.Point(355, 15);
            this.BtnWorkBrowse.Name = "BtnWorkBrowse";
            this.BtnWorkBrowse.Size = new System.Drawing.Size(30, 23);
            this.BtnWorkBrowse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnWorkBrowse.TabIndex = 35;
            this.BtnWorkBrowse.Click += new System.EventHandler(this.BtnWorkBrowse_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtOutData);
            this.groupPanel1.Controls.Add(this.BtnWorkBrowse);
            this.groupPanel1.Location = new System.Drawing.Point(12, 317);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(405, 77);
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
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 38;
            this.groupPanel1.Text = "输出tif文件 ";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.comboBoxExBands);
            this.groupPanel2.Controls.Add(this.cmbPixelType);
            this.groupPanel2.Controls.Add(this.cmbResample);
            this.groupPanel2.Controls.Add(this.textNoData);
            this.groupPanel2.Controls.Add(this.txtOutRows);
            this.groupPanel2.Controls.Add(this.txtOutColumns);
            this.groupPanel2.Controls.Add(this.txtCellSizeY);
            this.groupPanel2.Controls.Add(this.txtCellSizeX);
            this.groupPanel2.Controls.Add(this.chkSocCellSize);
            this.groupPanel2.Controls.Add(this.chkRasterSize);
            this.groupPanel2.Controls.Add(this.chkCellSize);
            this.groupPanel2.Controls.Add(this.labelX4);
            this.groupPanel2.Controls.Add(this.labelX6);
            this.groupPanel2.Controls.Add(this.labelX5);
            this.groupPanel2.Controls.Add(this.labelX2);
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.labelX3);
            this.groupPanel2.Location = new System.Drawing.Point(12, 100);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(405, 199);
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
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 39;
            this.groupPanel2.Text = "参数设置";
            // 
            // comboBoxExBands
            // 
            this.comboBoxExBands.DisplayMember = "Text";
            this.comboBoxExBands.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExBands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExBands.FormattingEnabled = true;
            this.comboBoxExBands.ItemHeight = 15;
            this.comboBoxExBands.Location = new System.Drawing.Point(89, 136);
            this.comboBoxExBands.Name = "comboBoxExBands";
            this.comboBoxExBands.Size = new System.Drawing.Size(110, 21);
            this.comboBoxExBands.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExBands.TabIndex = 45;
            // 
            // cmbPixelType
            // 
            this.cmbPixelType.DisplayMember = "Text";
            this.cmbPixelType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPixelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPixelType.FormattingEnabled = true;
            this.cmbPixelType.ItemHeight = 15;
            this.cmbPixelType.Location = new System.Drawing.Point(89, 101);
            this.cmbPixelType.Name = "cmbPixelType";
            this.cmbPixelType.Size = new System.Drawing.Size(110, 21);
            this.cmbPixelType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPixelType.TabIndex = 45;
            // 
            // cmbResample
            // 
            this.cmbResample.DisplayMember = "Text";
            this.cmbResample.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbResample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResample.FormattingEnabled = true;
            this.cmbResample.ItemHeight = 15;
            this.cmbResample.Location = new System.Drawing.Point(271, 101);
            this.cmbResample.Name = "cmbResample";
            this.cmbResample.Size = new System.Drawing.Size(110, 21);
            this.cmbResample.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbResample.TabIndex = 45;
            // 
            // textNoData
            // 
            // 
            // 
            // 
            this.textNoData.Border.Class = "TextBoxBorder";
            this.textNoData.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textNoData.Location = new System.Drawing.Point(271, 136);
            this.textNoData.Name = "textNoData";
            this.textNoData.Size = new System.Drawing.Size(110, 21);
            this.textNoData.TabIndex = 41;
            // 
            // txtOutRows
            // 
            // 
            // 
            // 
            this.txtOutRows.Border.Class = "TextBoxBorder";
            this.txtOutRows.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutRows.Enabled = false;
            this.txtOutRows.Location = new System.Drawing.Point(271, 65);
            this.txtOutRows.Name = "txtOutRows";
            this.txtOutRows.Size = new System.Drawing.Size(110, 21);
            this.txtOutRows.TabIndex = 39;
            this.txtOutRows.TextChanged += new System.EventHandler(this.txtOutRows_TextChanged);
            // 
            // txtOutColumns
            // 
            // 
            // 
            // 
            this.txtOutColumns.Border.Class = "TextBoxBorder";
            this.txtOutColumns.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutColumns.Enabled = false;
            this.txtOutColumns.Location = new System.Drawing.Point(89, 63);
            this.txtOutColumns.Name = "txtOutColumns";
            this.txtOutColumns.Size = new System.Drawing.Size(110, 21);
            this.txtOutColumns.TabIndex = 38;
            this.txtOutColumns.TextChanged += new System.EventHandler(this.txtOutColumns_TextChanged);
            // 
            // txtCellSizeY
            // 
            // 
            // 
            // 
            this.txtCellSizeY.Border.Class = "TextBoxBorder";
            this.txtCellSizeY.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCellSizeY.Location = new System.Drawing.Point(271, 32);
            this.txtCellSizeY.Name = "txtCellSizeY";
            this.txtCellSizeY.Size = new System.Drawing.Size(110, 21);
            this.txtCellSizeY.TabIndex = 36;
            this.txtCellSizeY.TextChanged += new System.EventHandler(this.txtCellSizeY_TextChanged);
            // 
            // txtCellSizeX
            // 
            // 
            // 
            // 
            this.txtCellSizeX.Border.Class = "TextBoxBorder";
            this.txtCellSizeX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCellSizeX.Location = new System.Drawing.Point(89, 32);
            this.txtCellSizeX.Name = "txtCellSizeX";
            this.txtCellSizeX.Size = new System.Drawing.Size(110, 21);
            this.txtCellSizeX.TabIndex = 35;
            this.txtCellSizeX.TextChanged += new System.EventHandler(this.txtCellSizeX_TextChanged);
            // 
            // chkSocCellSize
            // 
            this.chkSocCellSize.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkSocCellSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkSocCellSize.Location = new System.Drawing.Point(257, 1);
            this.chkSocCellSize.Name = "chkSocCellSize";
            this.chkSocCellSize.Size = new System.Drawing.Size(113, 23);
            this.chkSocCellSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkSocCellSize.TabIndex = 44;
            this.chkSocCellSize.Text = "保持原图分辨率";
            this.chkSocCellSize.CheckedChanged += new System.EventHandler(this.chkSocCellSize_CheckedChanged);
            // 
            // chkRasterSize
            // 
            this.chkRasterSize.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkRasterSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkRasterSize.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkRasterSize.Location = new System.Drawing.Point(12, 65);
            this.chkRasterSize.Name = "chkRasterSize";
            this.chkRasterSize.Size = new System.Drawing.Size(86, 23);
            this.chkRasterSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkRasterSize.TabIndex = 43;
            this.chkRasterSize.Text = "行列 (列)";
            this.chkRasterSize.CheckedChanged += new System.EventHandler(this.chkRasterSize_CheckedChanged);
            // 
            // chkCellSize
            // 
            this.chkCellSize.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkCellSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkCellSize.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkCellSize.Checked = true;
            this.chkCellSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCellSize.CheckValue = "Y";
            this.chkCellSize.Location = new System.Drawing.Point(12, 30);
            this.chkCellSize.Name = "chkCellSize";
            this.chkCellSize.Size = new System.Drawing.Size(86, 23);
            this.chkCellSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkCellSize.TabIndex = 42;
            this.chkCellSize.Text = "分辨率(x)";
            this.chkCellSize.CheckedChanged += new System.EventHandler(this.chkCellSize_CheckedChanged);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(29, 135);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(69, 23);
            this.labelX4.TabIndex = 40;
            this.labelX4.Text = "选择波段:";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(235, 30);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(32, 23);
            this.labelX6.TabIndex = 40;
            this.labelX6.Text = "(y):";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(235, 65);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(40, 23);
            this.labelX5.TabIndex = 40;
            this.labelX5.Text = "(行):";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(211, 101);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(64, 23);
            this.labelX2.TabIndex = 40;
            this.labelX2.Text = "采样方式:";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(29, 100);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(69, 23);
            this.labelX1.TabIndex = 40;
            this.labelX1.Text = "存储位数:";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(211, 134);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(64, 23);
            this.labelX3.TabIndex = 40;
            this.labelX3.Text = "无 效 值:";
            // 
            // FrmExportRaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 464);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpanelcolormu);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportRaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出栅格数据";
            this.Load += new System.EventHandler(this.FrmExportRaster_Load);
            this.grpanelcolormu.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX BtnWorkBrowse;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutData;
        private DevComponents.DotNetBar.Controls.GroupPanel grpanelcolormu;
        private DevComponents.DotNetBar.Controls.CheckBoxX rdoWorkspace;
        private DevComponents.DotNetBar.Controls.CheckBoxX rdoLayer;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkRasterSize;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkCellSize;
        private DevComponents.DotNetBar.Controls.TextBoxX textNoData;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutRows;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutColumns;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCellSizeY;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCellSizeX;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSocCellSize;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPixelType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbResample;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExBands;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}