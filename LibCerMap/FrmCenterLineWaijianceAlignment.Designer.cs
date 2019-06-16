namespace LibCerMap
{
    partial class FrmCenterLineWaijianceAlignment
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
            this.gPanelPoint = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.cboBoxPointLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonXSelect = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.comboBoxExCenterlineLinearLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.radioButtonLayer = new System.Windows.Forms.RadioButton();
            this.radioButtonFile = new System.Windows.Forms.RadioButton();
            this.gPanelPoint.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.Location = new System.Drawing.Point(211, 245);
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
            this.btCancel.Location = new System.Drawing.Point(308, 245);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 25);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gPanelPoint
            // 
            this.gPanelPoint.BackColor = System.Drawing.Color.Transparent;
            this.gPanelPoint.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelPoint.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelPoint.Controls.Add(this.textBoxFile);
            this.gPanelPoint.Controls.Add(this.cboBoxPointLayer);
            this.gPanelPoint.Controls.Add(this.buttonXSelect);
            this.gPanelPoint.DisabledBackColor = System.Drawing.Color.Empty;
            this.gPanelPoint.Location = new System.Drawing.Point(12, 13);
            this.gPanelPoint.Name = "gPanelPoint";
            this.gPanelPoint.Size = new System.Drawing.Size(369, 120);
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
            this.gPanelPoint.Text = "选择外检测点图层";
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(22, 55);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(273, 20);
            this.textBoxFile.TabIndex = 11;
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
            // buttonXSelect
            // 
            this.buttonXSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSelect.Location = new System.Drawing.Point(301, 55);
            this.buttonXSelect.Name = "buttonXSelect";
            this.buttonXSelect.Size = new System.Drawing.Size(34, 25);
            this.buttonXSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXSelect.TabIndex = 10;
            this.buttonXSelect.Text = "...";
            this.buttonXSelect.Click += new System.EventHandler(this.buttonXSelect_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.comboBoxExCenterlineLinearLayer);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(12, 139);
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
            // radioButtonLayer
            // 
            this.radioButtonLayer.AutoSize = true;
            this.radioButtonLayer.Checked = true;
            this.radioButtonLayer.Location = new System.Drawing.Point(387, 49);
            this.radioButtonLayer.Name = "radioButtonLayer";
            this.radioButtonLayer.Size = new System.Drawing.Size(85, 17);
            this.radioButtonLayer.TabIndex = 12;
            this.radioButtonLayer.TabStop = true;
            this.radioButtonLayer.Text = "从图层获取";
            this.radioButtonLayer.UseVisualStyleBackColor = true;
            // 
            // radioButtonFile
            // 
            this.radioButtonFile.AutoSize = true;
            this.radioButtonFile.Location = new System.Drawing.Point(387, 90);
            this.radioButtonFile.Name = "radioButtonFile";
            this.radioButtonFile.Size = new System.Drawing.Size(85, 17);
            this.radioButtonFile.TabIndex = 11;
            this.radioButtonFile.Text = "从文件获取";
            this.radioButtonFile.UseVisualStyleBackColor = true;
            // 
            // FrmCenterLineWaijianceAlignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 300);
            this.Controls.Add(this.radioButtonLayer);
            this.Controls.Add(this.radioButtonFile);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.gPanelPoint);
            this.Controls.Add(this.btOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCenterLineWaijianceAlignment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中线-外检测对齐图层选择";
            this.Load += new System.EventHandler(this.FrmPointToLine_Load);
            this.gPanelPoint.ResumeLayout(false);
            this.gPanelPoint.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelPoint;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboBoxPointLayer;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExCenterlineLinearLayer;
        private System.Windows.Forms.TextBox textBoxFile;
        private DevComponents.DotNetBar.ButtonX buttonXSelect;
        private System.Windows.Forms.RadioButton radioButtonLayer;
        private System.Windows.Forms.RadioButton radioButtonFile;
    }
}