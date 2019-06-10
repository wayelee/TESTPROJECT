namespace LibCerMap
{
    partial class FrmInsideToInsideAlignment
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
            this.radioButtonFile = new System.Windows.Forms.RadioButton();
            this.radioButtonLayer = new System.Windows.Forms.RadioButton();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxExCenterlineLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxMeasureField = new System.Windows.Forms.ComboBox();
            this.buttonXSelect = new DevComponents.DotNetBar.ButtonX();
            this.gPanelPoint = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxBaseMeasureField = new System.Windows.Forms.ComboBox();
            this.cboBoxPointLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxFixInputValue = new System.Windows.Forms.CheckBox();
            this.gPanelLine.SuspendLayout();
            this.gPanelPoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.Location = new System.Drawing.Point(225, 393);
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
            this.btCancel.Location = new System.Drawing.Point(306, 393);
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
            this.gPanelLine.Controls.Add(this.textBoxFile);
            this.gPanelLine.Controls.Add(this.label5);
            this.gPanelLine.Controls.Add(this.comboBoxExCenterlineLayer);
            this.gPanelLine.Controls.Add(this.comboBoxMeasureField);
            this.gPanelLine.Controls.Add(this.buttonXSelect);
            this.gPanelLine.DisabledBackColor = System.Drawing.Color.Empty;
            this.gPanelLine.Location = new System.Drawing.Point(12, 116);
            this.gPanelLine.Name = "gPanelLine";
            this.gPanelLine.Size = new System.Drawing.Size(350, 158);
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
            this.gPanelLine.Text = "选择对齐内检测点";
            // 
            // radioButtonFile
            // 
            this.radioButtonFile.AutoSize = true;
            this.radioButtonFile.Location = new System.Drawing.Point(368, 201);
            this.radioButtonFile.Name = "radioButtonFile";
            this.radioButtonFile.Size = new System.Drawing.Size(85, 17);
            this.radioButtonFile.TabIndex = 10;
            this.radioButtonFile.Text = "从文件获取";
            this.radioButtonFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonLayer
            // 
            this.radioButtonLayer.AutoSize = true;
            this.radioButtonLayer.Checked = true;
            this.radioButtonLayer.Location = new System.Drawing.Point(368, 154);
            this.radioButtonLayer.Name = "radioButtonLayer";
            this.radioButtonLayer.Size = new System.Drawing.Size(85, 17);
            this.radioButtonLayer.TabIndex = 10;
            this.radioButtonLayer.TabStop = true;
            this.radioButtonLayer.Text = "从图层获取";
            this.radioButtonLayer.UseVisualStyleBackColor = true;
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(22, 66);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(273, 20);
            this.textBoxFile.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "对齐内检测点里程字段";
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
            this.comboBoxExCenterlineLayer.Size = new System.Drawing.Size(273, 20);
            this.comboBoxExCenterlineLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExCenterlineLayer.TabIndex = 1;
            this.comboBoxExCenterlineLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxExCenterlineLayer_SelectedIndexChanged);
            // 
            // comboBoxMeasureField
            // 
            this.comboBoxMeasureField.Enabled = false;
            this.comboBoxMeasureField.FormattingEnabled = true;
            this.comboBoxMeasureField.Items.AddRange(new object[] {
            "记录距离"});
            this.comboBoxMeasureField.Location = new System.Drawing.Point(152, 96);
            this.comboBoxMeasureField.Name = "comboBoxMeasureField";
            this.comboBoxMeasureField.Size = new System.Drawing.Size(119, 21);
            this.comboBoxMeasureField.TabIndex = 7;
            // 
            // buttonXSelect
            // 
            this.buttonXSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSelect.Location = new System.Drawing.Point(301, 66);
            this.buttonXSelect.Name = "buttonXSelect";
            this.buttonXSelect.Size = new System.Drawing.Size(34, 25);
            this.buttonXSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXSelect.TabIndex = 0;
            this.buttonXSelect.Text = "...";
            this.buttonXSelect.Click += new System.EventHandler(this.buttonXSelect_Click);
            // 
            // gPanelPoint
            // 
            this.gPanelPoint.BackColor = System.Drawing.Color.Transparent;
            this.gPanelPoint.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelPoint.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelPoint.Controls.Add(this.label4);
            this.gPanelPoint.Controls.Add(this.comboBoxBaseMeasureField);
            this.gPanelPoint.Controls.Add(this.cboBoxPointLayer);
            this.gPanelPoint.DisabledBackColor = System.Drawing.Color.Empty;
            this.gPanelPoint.Location = new System.Drawing.Point(12, 13);
            this.gPanelPoint.Name = "gPanelPoint";
            this.gPanelPoint.Size = new System.Drawing.Size(350, 97);
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
            this.gPanelPoint.Text = "选择基准内检测点图层";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "基准内检测点里程字段";
            // 
            // comboBoxBaseMeasureField
            // 
            this.comboBoxBaseMeasureField.FormattingEnabled = true;
            this.comboBoxBaseMeasureField.Location = new System.Drawing.Point(161, 48);
            this.comboBoxBaseMeasureField.Name = "comboBoxBaseMeasureField";
            this.comboBoxBaseMeasureField.Size = new System.Drawing.Size(134, 21);
            this.comboBoxBaseMeasureField.TabIndex = 7;
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
            this.cboBoxPointLayer.Size = new System.Drawing.Size(273, 20);
            this.cboBoxPointLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboBoxPointLayer.TabIndex = 1;
            this.cboBoxPointLayer.SelectedIndexChanged += new System.EventHandler(this.cboBoxPointLayer_SelectedIndexChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(15, 314);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "开始点里程";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(148, 314);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 298);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "结束点里程";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(15, 354);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown3.TabIndex = 5;
            this.numericUpDown3.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "粗对齐误差容限";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(148, 354);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown4.TabIndex = 5;
            this.numericUpDown4.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 337);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "精对齐误差容限";
            // 
            // checkBoxFixInputValue
            // 
            this.checkBoxFixInputValue.AutoSize = true;
            this.checkBoxFixInputValue.Location = new System.Drawing.Point(271, 315);
            this.checkBoxFixInputValue.Name = "checkBoxFixInputValue";
            this.checkBoxFixInputValue.Size = new System.Drawing.Size(110, 17);
            this.checkBoxFixInputValue.TabIndex = 7;
            this.checkBoxFixInputValue.Text = "固定输入里程值";
            this.checkBoxFixInputValue.UseVisualStyleBackColor = true;
            // 
            // FrmInsideToInsideAlignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 436);
            this.Controls.Add(this.radioButtonLayer);
            this.Controls.Add(this.radioButtonFile);
            this.Controls.Add(this.checkBoxFixInputValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.gPanelLine);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.gPanelPoint);
            this.Controls.Add(this.btOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInsideToInsideAlignment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "两次内检测对齐图层选择";
            this.Load += new System.EventHandler(this.FrmPointToLine_Load);
            this.gPanelLine.ResumeLayout(false);
            this.gPanelLine.PerformLayout();
            this.gPanelPoint.ResumeLayout(false);
            this.gPanelPoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
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
        private System.Windows.Forms.ComboBox comboBoxBaseMeasureField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMeasureField;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxFixInputValue;
        private System.Windows.Forms.RadioButton radioButtonFile;
        private System.Windows.Forms.RadioButton radioButtonLayer;
        private System.Windows.Forms.TextBox textBoxFile;
        private DevComponents.DotNetBar.ButtonX buttonXSelect;
    }
}