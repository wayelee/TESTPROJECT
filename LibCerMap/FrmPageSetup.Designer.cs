namespace LibCerMap
{
    partial class FrmPageSetup
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
            this.picCustomPage = new System.Windows.Forms.PictureBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboPageSize = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panelCustom = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.radioOritationLandscape = new System.Windows.Forms.RadioButton();
            this.radioOritationPortrait = new System.Windows.Forms.RadioButton();
            this.cmbPageUnit = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtPageHeight = new DevComponents.Editors.DoubleInput();
            this.txtPageWidth = new DevComponents.Editors.DoubleInput();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.btnPrinterPage = new DevComponents.DotNetBar.ButtonX();
            this.cmbPrinters = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chkUsePrinterPage = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelExWH = new DevComponents.DotNetBar.PanelEx();
            ((System.ComponentModel.ISupportInitialize)(this.picCustomPage)).BeginInit();
            this.panelCustom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageWidth)).BeginInit();
            this.panelExWH.SuspendLayout();
            this.SuspendLayout();
            // 
            // picCustomPage
            // 
            this.picCustomPage.Image = global::LibCerMap.Properties.Resources.printsetup;
            this.picCustomPage.Location = new System.Drawing.Point(359, 3);
            this.picCustomPage.Name = "picCustomPage";
            this.picCustomPage.Size = new System.Drawing.Size(85, 120);
            this.picCustomPage.TabIndex = 0;
            this.picCustomPage.TabStop = false;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(23, 22);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "打印机:";
            // 
            // cboPageSize
            // 
            this.cboPageSize.DisplayMember = "Text";
            this.cboPageSize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPageSize.FormattingEnabled = true;
            this.cboPageSize.ItemHeight = 15;
            this.cboPageSize.Location = new System.Drawing.Point(81, 14);
            this.cboPageSize.Name = "cboPageSize";
            this.cboPageSize.Size = new System.Drawing.Size(268, 21);
            this.cboPageSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboPageSize.TabIndex = 2;
            this.cboPageSize.SelectedIndexChanged += new System.EventHandler(this.cboPageSize_SelectedIndexChanged);
            // 
            // panelCustom
            // 
            this.panelCustom.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelCustom.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelCustom.Controls.Add(this.panelExWH);
            this.panelCustom.Controls.Add(this.radioOritationLandscape);
            this.panelCustom.Controls.Add(this.radioOritationPortrait);
            this.panelCustom.Controls.Add(this.picCustomPage);
            this.panelCustom.Controls.Add(this.cboPageSize);
            this.panelCustom.Controls.Add(this.labelX5);
            this.panelCustom.Controls.Add(this.labelX4);
            this.panelCustom.Location = new System.Drawing.Point(12, 101);
            this.panelCustom.Name = "panelCustom";
            this.panelCustom.Size = new System.Drawing.Size(453, 198);
            // 
            // 
            // 
            this.panelCustom.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelCustom.Style.BackColorGradientAngle = 90;
            this.panelCustom.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelCustom.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.panelCustom.Style.BorderBottomWidth = 1;
            this.panelCustom.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelCustom.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.panelCustom.Style.BorderLeftWidth = 1;
            this.panelCustom.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.panelCustom.Style.BorderRightWidth = 1;
            this.panelCustom.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.panelCustom.Style.BorderTopWidth = 1;
            this.panelCustom.Style.CornerDiameter = 4;
            this.panelCustom.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.panelCustom.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelCustom.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.panelCustom.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelCustom.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.panelCustom.TabIndex = 4;
            this.panelCustom.Text = "自定义页面";
            // 
            // radioOritationLandscape
            // 
            this.radioOritationLandscape.AutoSize = true;
            this.radioOritationLandscape.BackColor = System.Drawing.Color.Transparent;
            this.radioOritationLandscape.Location = new System.Drawing.Point(166, 143);
            this.radioOritationLandscape.Name = "radioOritationLandscape";
            this.radioOritationLandscape.Size = new System.Drawing.Size(47, 16);
            this.radioOritationLandscape.TabIndex = 4;
            this.radioOritationLandscape.Text = "横向";
            this.radioOritationLandscape.UseVisualStyleBackColor = false;
            this.radioOritationLandscape.Click += new System.EventHandler(this.radioOritationLandscape_Click);
            // 
            // radioOritationPortrait
            // 
            this.radioOritationPortrait.AutoSize = true;
            this.radioOritationPortrait.BackColor = System.Drawing.Color.Transparent;
            this.radioOritationPortrait.Checked = true;
            this.radioOritationPortrait.Location = new System.Drawing.Point(81, 143);
            this.radioOritationPortrait.Name = "radioOritationPortrait";
            this.radioOritationPortrait.Size = new System.Drawing.Size(47, 16);
            this.radioOritationPortrait.TabIndex = 4;
            this.radioOritationPortrait.TabStop = true;
            this.radioOritationPortrait.Text = "纵向";
            this.radioOritationPortrait.UseVisualStyleBackColor = false;
            this.radioOritationPortrait.Click += new System.EventHandler(this.radioOritationPortrait_Click);
            // 
            // cmbPageUnit
            // 
            this.cmbPageUnit.DisplayMember = "Text";
            this.cmbPageUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPageUnit.Enabled = false;
            this.cmbPageUnit.FormattingEnabled = true;
            this.cmbPageUnit.ItemHeight = 15;
            this.cmbPageUnit.Location = new System.Drawing.Point(212, 14);
            this.cmbPageUnit.Name = "cmbPageUnit";
            this.cmbPageUnit.Size = new System.Drawing.Size(121, 21);
            this.cmbPageUnit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPageUnit.TabIndex = 3;
            // 
            // txtPageHeight
            // 
            // 
            // 
            // 
            this.txtPageHeight.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtPageHeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPageHeight.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtPageHeight.Increment = 1D;
            this.txtPageHeight.Location = new System.Drawing.Point(73, 48);
            this.txtPageHeight.Name = "txtPageHeight";
            this.txtPageHeight.ShowUpDown = true;
            this.txtPageHeight.Size = new System.Drawing.Size(114, 21);
            this.txtPageHeight.TabIndex = 2;
            this.txtPageHeight.Value = 50D;
            // 
            // txtPageWidth
            // 
            // 
            // 
            // 
            this.txtPageWidth.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtPageWidth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPageWidth.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtPageWidth.Increment = 1D;
            this.txtPageWidth.Location = new System.Drawing.Point(73, 12);
            this.txtPageWidth.Name = "txtPageWidth";
            this.txtPageWidth.ShowUpDown = true;
            this.txtPageWidth.Size = new System.Drawing.Size(114, 21);
            this.txtPageWidth.TabIndex = 2;
            this.txtPageWidth.Value = 50D;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(26, 12);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(42, 23);
            this.labelX5.TabIndex = 1;
            this.labelX5.Text = "纸张:";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(26, 140);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(75, 23);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "方向:";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(18, 48);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(42, 23);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "高度:";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(18, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(42, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "宽度:";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(289, 325);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 33);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(384, 325);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(81, 33);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "取消";
            // 
            // btnPrinterPage
            // 
            this.btnPrinterPage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrinterPage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrinterPage.Location = new System.Drawing.Point(149, 59);
            this.btnPrinterPage.Name = "btnPrinterPage";
            this.btnPrinterPage.Size = new System.Drawing.Size(107, 31);
            this.btnPrinterPage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrinterPage.TabIndex = 8;
            this.btnPrinterPage.Text = "打印机页面设置";
            this.btnPrinterPage.Click += new System.EventHandler(this.btnPrinterPage_Click);
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.DisplayMember = "Text";
            this.cmbPrinters.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.ItemHeight = 15;
            this.cmbPrinters.Location = new System.Drawing.Point(79, 23);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(386, 21);
            this.cmbPrinters.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPrinters.TabIndex = 10;
            // 
            // chkUsePrinterPage
            // 
            this.chkUsePrinterPage.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkUsePrinterPage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkUsePrinterPage.Location = new System.Drawing.Point(23, 63);
            this.chkUsePrinterPage.Name = "chkUsePrinterPage";
            this.chkUsePrinterPage.Size = new System.Drawing.Size(120, 23);
            this.chkUsePrinterPage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkUsePrinterPage.TabIndex = 11;
            this.chkUsePrinterPage.Text = "使用打印机设置";
            this.chkUsePrinterPage.CheckedChanged += new System.EventHandler(this.chkUsePrinterPage_CheckedChanged);
            // 
            // panelExWH
            // 
            this.panelExWH.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExWH.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelExWH.Controls.Add(this.txtPageWidth);
            this.panelExWH.Controls.Add(this.labelX2);
            this.panelExWH.Controls.Add(this.labelX3);
            this.panelExWH.Controls.Add(this.cmbPageUnit);
            this.panelExWH.Controls.Add(this.txtPageHeight);
            this.panelExWH.Location = new System.Drawing.Point(8, 44);
            this.panelExWH.Name = "panelExWH";
            this.panelExWH.Size = new System.Drawing.Size(341, 79);
            this.panelExWH.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExWH.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelExWH.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExWH.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExWH.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelExWH.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExWH.Style.GradientAngle = 90;
            this.panelExWH.TabIndex = 5;
            // 
            // FrmPageSetup
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(480, 378);
            this.Controls.Add(this.chkUsePrinterPage);
            this.Controls.Add(this.cmbPrinters);
            this.Controls.Add(this.btnPrinterPage);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelCustom);
            this.Controls.Add(this.labelX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPageSetup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "页面设置";
            this.Load += new System.EventHandler(this.FrmPageSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCustomPage)).EndInit();
            this.panelCustom.ResumeLayout(false);
            this.panelCustom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageWidth)).EndInit();
            this.panelExWH.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picCustomPage;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboPageSize;
        private DevComponents.DotNetBar.Controls.GroupPanel panelCustom;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.ButtonX btnPrinterPage;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPrinters;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPageUnit;
        private DevComponents.Editors.DoubleInput txtPageHeight;
        private DevComponents.Editors.DoubleInput txtPageWidth;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkUsePrinterPage;
        private System.Windows.Forms.RadioButton radioOritationLandscape;
        private System.Windows.Forms.RadioButton radioOritationPortrait;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.PanelEx panelExWH;
    }
}