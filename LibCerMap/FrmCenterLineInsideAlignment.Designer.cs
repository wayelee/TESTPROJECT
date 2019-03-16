namespace LibCerMap
{
    partial class FrmCenterLineInsideAlignment
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
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.gPanelLine = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.comboBoxExCenterlineLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.gPanelPoint = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cboBoxPointLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.comboBoxExCenterlineLinearLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.radioButtonFromMap = new System.Windows.Forms.RadioButton();
            this.radioButtonFromExcelFile = new System.Windows.Forms.RadioButton();
            this.buttonXDIR = new DevComponents.DotNetBar.ButtonX();
            this.gPanelLine.SuspendLayout();
            this.gPanelPoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.Location = new System.Drawing.Point(206, 463);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 25);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 0;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.Location = new System.Drawing.Point(303, 463);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 25);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gPanelLine
            // 
            this.gPanelLine.BackColor = System.Drawing.Color.Transparent;
            this.gPanelLine.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelLine.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelLine.Controls.Add(this.comboBoxExCenterlineLayer);
            this.gPanelLine.DisabledBackColor = System.Drawing.Color.Empty;
            this.gPanelLine.Location = new System.Drawing.Point(12, 196);
            this.gPanelLine.Name = "gPanelLine";
            this.gPanelLine.Size = new System.Drawing.Size(369, 88);
            // 
            // 
            // 
            this.gPanelLine.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelLine.Style.BackColorGradientAngle = 90;
            this.gPanelLine.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelLine.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderBottomWidth = 1;
            this.gPanelLine.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelLine.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderLeftWidth = 1;
            this.gPanelLine.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderRightWidth = 1;
            this.gPanelLine.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderTopWidth = 1;
            this.gPanelLine.Style.CornerDiameter = 4;
            this.gPanelLine.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelLine.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelLine.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelLine.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelLine.TabIndex = 3;
            this.gPanelLine.Text = "选择中线点图层";
            // 
            // comboBoxExCenterlineLayer
            // 
            this.comboBoxExCenterlineLayer.DisplayMember = "Text";
            this.comboBoxExCenterlineLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExCenterlineLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExCenterlineLayer.FormattingEnabled = true;
            this.comboBoxExCenterlineLayer.ItemHeight = 14;
            this.comboBoxExCenterlineLayer.Location = new System.Drawing.Point(22, 19);
            this.comboBoxExCenterlineLayer.Name = "comboBoxExCenterlineLayer";
            this.comboBoxExCenterlineLayer.Size = new System.Drawing.Size(314, 20);
            this.comboBoxExCenterlineLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExCenterlineLayer.TabIndex = 1;
            // 
            // gPanelPoint
            // 
            this.gPanelPoint.BackColor = System.Drawing.Color.Transparent;
            this.gPanelPoint.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelPoint.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelPoint.Controls.Add(this.cboBoxPointLayer);
            this.gPanelPoint.DisabledBackColor = System.Drawing.Color.Empty;
            this.gPanelPoint.Location = new System.Drawing.Point(12, 13);
            this.gPanelPoint.Name = "gPanelPoint";
            this.gPanelPoint.Size = new System.Drawing.Size(369, 86);
            // 
            // 
            // 
            this.gPanelPoint.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelPoint.Style.BackColorGradientAngle = 90;
            this.gPanelPoint.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelPoint.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderBottomWidth = 1;
            this.gPanelPoint.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelPoint.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderLeftWidth = 1;
            this.gPanelPoint.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderRightWidth = 1;
            this.gPanelPoint.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderTopWidth = 1;
            this.gPanelPoint.Style.CornerDiameter = 4;
            this.gPanelPoint.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelPoint.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelPoint.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelPoint.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelPoint.TabIndex = 2;
            this.gPanelPoint.Text = "选择内检测点图层";
            // 
            // cboBoxPointLayer
            // 
            this.cboBoxPointLayer.DisplayMember = "Text";
            this.cboBoxPointLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBoxPointLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBoxPointLayer.FormattingEnabled = true;
            this.cboBoxPointLayer.ItemHeight = 14;
            this.cboBoxPointLayer.Location = new System.Drawing.Point(22, 17);
            this.cboBoxPointLayer.Name = "cboBoxPointLayer";
            this.cboBoxPointLayer.Size = new System.Drawing.Size(314, 20);
            this.cboBoxPointLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboBoxPointLayer.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(34, 423);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 404);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "开始点里程";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(239, 423);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 404);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "结束点里程";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(34, 463);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 446);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "误差容限";
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.comboBoxExCenterlineLinearLayer);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(12, 303);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(369, 88);
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
            this.groupPanel1.TabIndex = 7;
            this.groupPanel1.Text = "选择中线图层";
            // 
            // comboBoxExCenterlineLinearLayer
            // 
            this.comboBoxExCenterlineLinearLayer.DisplayMember = "Text";
            this.comboBoxExCenterlineLinearLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExCenterlineLinearLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExCenterlineLinearLayer.FormattingEnabled = true;
            this.comboBoxExCenterlineLinearLayer.ItemHeight = 14;
            this.comboBoxExCenterlineLinearLayer.Location = new System.Drawing.Point(22, 19);
            this.comboBoxExCenterlineLinearLayer.Name = "comboBoxExCenterlineLinearLayer";
            this.comboBoxExCenterlineLinearLayer.Size = new System.Drawing.Size(314, 20);
            this.comboBoxExCenterlineLinearLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExCenterlineLinearLayer.TabIndex = 1;
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel2.Controls.Add(this.textBoxFile);
            this.groupPanel2.Controls.Add(this.buttonXDIR);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Location = new System.Drawing.Point(12, 105);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(369, 86);
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
            this.groupPanel2.TabIndex = 2;
            this.groupPanel2.Text = "选择内检测点文件";
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(19, 19);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(297, 20);
            this.textBoxFile.TabIndex = 0;
            // 
            // radioButtonFromMap
            // 
            this.radioButtonFromMap.AutoSize = true;
            this.radioButtonFromMap.Checked = true;
            this.radioButtonFromMap.Location = new System.Drawing.Point(387, 52);
            this.radioButtonFromMap.Name = "radioButtonFromMap";
            this.radioButtonFromMap.Size = new System.Drawing.Size(85, 17);
            this.radioButtonFromMap.TabIndex = 8;
            this.radioButtonFromMap.TabStop = true;
            this.radioButtonFromMap.Text = "从图层获取";
            this.radioButtonFromMap.UseVisualStyleBackColor = true;
            // 
            // radioButtonFromExcelFile
            // 
            this.radioButtonFromExcelFile.AutoSize = true;
            this.radioButtonFromExcelFile.Location = new System.Drawing.Point(387, 144);
            this.radioButtonFromExcelFile.Name = "radioButtonFromExcelFile";
            this.radioButtonFromExcelFile.Size = new System.Drawing.Size(85, 17);
            this.radioButtonFromExcelFile.TabIndex = 8;
            this.radioButtonFromExcelFile.Text = "从文件获取";
            this.radioButtonFromExcelFile.UseVisualStyleBackColor = true;
            // 
            // buttonXDIR
            // 
            this.buttonXDIR.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDIR.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDIR.Location = new System.Drawing.Point(322, 17);
            this.buttonXDIR.Name = "buttonXDIR";
            this.buttonXDIR.Size = new System.Drawing.Size(28, 25);
            this.buttonXDIR.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXDIR.TabIndex = 0;
            this.buttonXDIR.Text = "...";
            this.buttonXDIR.Click += new System.EventHandler(this.buttonXDIR_Click);
            // 
            // FrmCenterLineInsideAlignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 514);
            this.Controls.Add(this.radioButtonFromExcelFile);
            this.Controls.Add(this.radioButtonFromMap);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.gPanelLine);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.gPanelPoint);
            this.Controls.Add(this.btOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCenterLineInsideAlignment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中线-内检测对齐图层选择";
            this.Load += new System.EventHandler(this.FrmPointToLine_Load);
            this.gPanelLine.ResumeLayout(false);
            this.gPanelPoint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelLine;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelPoint;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboBoxPointLayer;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExCenterlineLayer;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExCenterlineLinearLayer;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.TextBox textBoxFile;
        private DevComponents.DotNetBar.ButtonX buttonXDIR;
        private System.Windows.Forms.RadioButton radioButtonFromMap;
        private System.Windows.Forms.RadioButton radioButtonFromExcelFile;
    }
}