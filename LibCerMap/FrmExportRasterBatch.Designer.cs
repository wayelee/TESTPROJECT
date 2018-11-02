namespace LibCerMap
{
    partial class FrmExportRasterBatch
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
            this.grpanelcolormu.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.Location = new System.Drawing.Point(311, 224);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(70, 26);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 37;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Location = new System.Drawing.Point(205, 224);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 26);
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
            this.txtOutData.Size = new System.Drawing.Size(302, 21);
            this.txtOutData.TabIndex = 34;
            // 
            // grpanelcolormu
            // 
            this.grpanelcolormu.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpanelcolormu.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grpanelcolormu.Controls.Add(this.rdoWorkspace);
            this.grpanelcolormu.Controls.Add(this.rdoLayer);
            this.grpanelcolormu.Location = new System.Drawing.Point(18, 12);
            this.grpanelcolormu.Name = "grpanelcolormu";
            this.grpanelcolormu.Size = new System.Drawing.Size(363, 83);
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
            // 
            // rdoLayer
            // 
            this.rdoLayer.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.rdoLayer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rdoLayer.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rdoLayer.Checked = true;
            this.rdoLayer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rdoLayer.CheckValue = "Y";
            this.rdoLayer.Location = new System.Drawing.Point(37, 14);
            this.rdoLayer.Name = "rdoLayer";
            this.rdoLayer.Size = new System.Drawing.Size(100, 23);
            this.rdoLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rdoLayer.TabIndex = 0;
            this.rdoLayer.Text = "与原图层相同";
            this.rdoLayer.CheckedChanged += new System.EventHandler(this.rdoLayer_CheckedChanged);
            // 
            // BtnWorkBrowse
            // 
            this.BtnWorkBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnWorkBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnWorkBrowse.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.BtnWorkBrowse.Location = new System.Drawing.Point(311, 15);
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
            this.groupPanel1.Location = new System.Drawing.Point(18, 118);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(363, 83);
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
            this.groupPanel1.Text = "输出文件目录";
            // 
            // FrmExportRasterBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 281);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpanelcolormu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportRasterBatch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量导出栅格数据";
            this.grpanelcolormu.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
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
    }
}