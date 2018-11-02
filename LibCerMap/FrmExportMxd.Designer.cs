namespace LibCerMap
{
    partial class FrmExportMxd
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
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.radioFrame = new System.Windows.Forms.RadioButton();
            this.radioLayer = new System.Windows.Forms.RadioButton();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelExport = new DevComponents.DotNetBar.LabelX();
            this.txtFolder = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnFolder = new DevComponents.DotNetBar.ButtonX();
            this.chkExportVisible = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.radioFrame);
            this.groupPanel1.Controls.Add(this.radioLayer);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(362, 74);
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
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "空间参考系统选择";
            // 
            // radioFrame
            // 
            this.radioFrame.AutoSize = true;
            this.radioFrame.BackColor = System.Drawing.Color.Transparent;
            this.radioFrame.Location = new System.Drawing.Point(222, 12);
            this.radioFrame.Name = "radioFrame";
            this.radioFrame.Size = new System.Drawing.Size(71, 16);
            this.radioFrame.TabIndex = 0;
            this.radioFrame.Text = "地图框架";
            this.radioFrame.UseVisualStyleBackColor = false;
            // 
            // radioLayer
            // 
            this.radioLayer.AutoSize = true;
            this.radioLayer.BackColor = System.Drawing.Color.Transparent;
            this.radioLayer.Checked = true;
            this.radioLayer.Location = new System.Drawing.Point(43, 12);
            this.radioLayer.Name = "radioLayer";
            this.radioLayer.Size = new System.Drawing.Size(83, 16);
            this.radioLayer.TabIndex = 0;
            this.radioLayer.TabStop = true;
            this.radioLayer.Text = "保持原图层";
            this.radioLayer.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(174, 213);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 31);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(265, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 31);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.chkExportVisible);
            this.groupPanel2.Controls.Add(this.labelExport);
            this.groupPanel2.Controls.Add(this.txtFolder);
            this.groupPanel2.Controls.Add(this.btnFolder);
            this.groupPanel2.Location = new System.Drawing.Point(0, 82);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(362, 117);
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
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "导出目录";
            // 
            // labelExport
            // 
            this.labelExport.BackColor = System.Drawing.SystemColors.Highlight;
            // 
            // 
            // 
            this.labelExport.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelExport.Location = new System.Drawing.Point(125, 58);
            this.labelExport.Name = "labelExport";
            this.labelExport.Size = new System.Drawing.Size(228, 23);
            this.labelExport.TabIndex = 2;
            // 
            // txtFolder
            // 
            // 
            // 
            // 
            this.txtFolder.Border.Class = "TextBoxBorder";
            this.txtFolder.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFolder.Location = new System.Drawing.Point(9, 17);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(284, 21);
            this.txtFolder.TabIndex = 1;
            // 
            // btnFolder
            // 
            this.btnFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFolder.Location = new System.Drawing.Point(299, 15);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(48, 23);
            this.btnFolder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFolder.TabIndex = 0;
            this.btnFolder.Text = "浏览";
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // chkExportVisible
            // 
            this.chkExportVisible.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkExportVisible.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkExportVisible.Location = new System.Drawing.Point(12, 58);
            this.chkExportVisible.Name = "chkExportVisible";
            this.chkExportVisible.Size = new System.Drawing.Size(114, 23);
            this.chkExportVisible.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkExportVisible.TabIndex = 3;
            this.chkExportVisible.Text = "只导出可见图层";
            // 
            // FrmExportMxd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 253);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportMxd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出工作空间";
            this.Load += new System.EventHandler(this.FrmExportMxd_Load);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.groupPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.RadioButton radioFrame;
        private System.Windows.Forms.RadioButton radioLayer;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFolder;
        private DevComponents.DotNetBar.ButtonX btnFolder;
        private DevComponents.DotNetBar.LabelX labelExport;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkExportVisible;
    }
}