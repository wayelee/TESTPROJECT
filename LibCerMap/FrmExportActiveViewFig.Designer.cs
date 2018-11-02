namespace LibCerMap
{
    partial class FrmExportActiveViewFig
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
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.gPanelPath = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtImagePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btImagePath = new DevComponents.DotNetBar.ButtonX();
            this.gPanelResolution = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtImageHeight = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtImageWidth = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboBoxImageResolution = new DevComponents.Editors.DoubleInput();
            this.gPanelPath.SuspendLayout();
            this.gPanelResolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboBoxImageResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(344, 182);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(238, 182);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 4;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // gPanelPath
            // 
            this.gPanelPath.BackColor = System.Drawing.Color.Transparent;
            this.gPanelPath.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelPath.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelPath.Controls.Add(this.txtImagePath);
            this.gPanelPath.Controls.Add(this.btImagePath);
            this.gPanelPath.Location = new System.Drawing.Point(12, 83);
            this.gPanelPath.Name = "gPanelPath";
            this.gPanelPath.Size = new System.Drawing.Size(421, 74);
            // 
            // 
            // 
            this.gPanelPath.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelPath.Style.BackColorGradientAngle = 90;
            this.gPanelPath.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelPath.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPath.Style.BorderBottomWidth = 1;
            this.gPanelPath.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelPath.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPath.Style.BorderLeftWidth = 1;
            this.gPanelPath.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPath.Style.BorderRightWidth = 1;
            this.gPanelPath.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPath.Style.BorderTopWidth = 1;
            this.gPanelPath.Style.CornerDiameter = 4;
            this.gPanelPath.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelPath.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelPath.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelPath.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelPath.TabIndex = 3;
            this.gPanelPath.Text = "保存路径";
            // 
            // txtImagePath
            // 
            // 
            // 
            // 
            this.txtImagePath.Border.Class = "TextBoxBorder";
            this.txtImagePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtImagePath.Location = new System.Drawing.Point(20, 13);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(312, 21);
            this.txtImagePath.TabIndex = 4;
            // 
            // btImagePath
            // 
            this.btImagePath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btImagePath.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btImagePath.Location = new System.Drawing.Point(338, 11);
            this.btImagePath.Name = "btImagePath";
            this.btImagePath.Size = new System.Drawing.Size(65, 23);
            this.btImagePath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btImagePath.TabIndex = 4;
            this.btImagePath.Text = "浏览";
            this.btImagePath.Click += new System.EventHandler(this.btImagePath_Click);
            // 
            // gPanelResolution
            // 
            this.gPanelResolution.BackColor = System.Drawing.Color.Transparent;
            this.gPanelResolution.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelResolution.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelResolution.Controls.Add(this.txtImageHeight);
            this.gPanelResolution.Controls.Add(this.txtImageWidth);
            this.gPanelResolution.Controls.Add(this.labelX2);
            this.gPanelResolution.Controls.Add(this.labelX1);
            this.gPanelResolution.Controls.Add(this.cboBoxImageResolution);
            this.gPanelResolution.Location = new System.Drawing.Point(12, 12);
            this.gPanelResolution.Name = "gPanelResolution";
            this.gPanelResolution.Size = new System.Drawing.Size(421, 65);
            // 
            // 
            // 
            this.gPanelResolution.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelResolution.Style.BackColorGradientAngle = 90;
            this.gPanelResolution.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelResolution.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelResolution.Style.BorderBottomWidth = 1;
            this.gPanelResolution.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelResolution.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelResolution.Style.BorderLeftWidth = 1;
            this.gPanelResolution.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelResolution.Style.BorderRightWidth = 1;
            this.gPanelResolution.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelResolution.Style.BorderTopWidth = 1;
            this.gPanelResolution.Style.CornerDiameter = 4;
            this.gPanelResolution.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelResolution.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelResolution.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelResolution.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelResolution.TabIndex = 1;
            this.gPanelResolution.Text = "图片分辨率";
            // 
            // txtImageHeight
            // 
            // 
            // 
            // 
            this.txtImageHeight.Border.Class = "TextBoxBorder";
            this.txtImageHeight.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtImageHeight.Location = new System.Drawing.Point(329, 7);
            this.txtImageHeight.Name = "txtImageHeight";
            this.txtImageHeight.Size = new System.Drawing.Size(74, 21);
            this.txtImageHeight.TabIndex = 6;
            this.txtImageHeight.Visible = false;
            // 
            // txtImageWidth
            // 
            // 
            // 
            // 
            this.txtImageWidth.Border.Class = "TextBoxBorder";
            this.txtImageWidth.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtImageWidth.Location = new System.Drawing.Point(188, 7);
            this.txtImageWidth.Name = "txtImageWidth";
            this.txtImageWidth.Size = new System.Drawing.Size(74, 21);
            this.txtImageWidth.TabIndex = 6;
            this.txtImageWidth.Visible = false;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(278, 6);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(45, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "高度：";
            this.labelX2.Visible = false;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(152, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(44, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "宽度：";
            this.labelX1.Visible = false;
            // 
            // cboBoxImageResolution
            // 
            // 
            // 
            // 
            this.cboBoxImageResolution.BackgroundStyle.Class = "DateTimeInputBackground";
            this.cboBoxImageResolution.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cboBoxImageResolution.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.cboBoxImageResolution.Increment = 1D;
            this.cboBoxImageResolution.Location = new System.Drawing.Point(20, 7);
            this.cboBoxImageResolution.MinValue = 0D;
            this.cboBoxImageResolution.Name = "cboBoxImageResolution";
            this.cboBoxImageResolution.ShowUpDown = true;
            this.cboBoxImageResolution.Size = new System.Drawing.Size(87, 21);
            this.cboBoxImageResolution.TabIndex = 4;
            this.cboBoxImageResolution.Value = 400D;
            // 
            // FrmExportActiveViewFig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 221);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gPanelResolution);
            this.Controls.Add(this.gPanelPath);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportActiveViewFig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出图片";
            this.Load += new System.EventHandler(this.FrmExportActiveViewFig_Load);
            this.gPanelPath.ResumeLayout(false);
            this.gPanelResolution.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboBoxImageResolution)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelPath;
        private DevComponents.DotNetBar.Controls.TextBoxX txtImagePath;
        private DevComponents.DotNetBar.ButtonX btImagePath;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelResolution;
        private DevComponents.Editors.DoubleInput cboBoxImageResolution;
        private DevComponents.DotNetBar.Controls.TextBoxX txtImageHeight;
        private DevComponents.DotNetBar.Controls.TextBoxX txtImageWidth;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}