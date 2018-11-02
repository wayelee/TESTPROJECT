namespace LibCerMap
{
    partial class FrmSiftMatching
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
            this.lblRightFilename = new System.Windows.Forms.Label();
            this.lblRadio = new System.Windows.Forms.Label();
            this.dbiRatio = new DevComponents.Editors.DoubleInput();
            this.lblMaxBlockSize = new System.Windows.Forms.Label();
            this.lblOverlaySize = new System.Windows.Forms.Label();
            this.iiMaxBlockSize = new DevComponents.Editors.IntegerInput();
            this.iiOverlaySize = new DevComponents.Editors.IntegerInput();
            this.txtRightImage = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.doubleRansac = new DevComponents.Editors.DoubleInput();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dbiRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iiMaxBlockSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iiOverlaySize)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleRansac)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRightFilename
            // 
            this.lblRightFilename.AutoSize = true;
            this.lblRightFilename.BackColor = System.Drawing.Color.Transparent;
            this.lblRightFilename.Location = new System.Drawing.Point(25, 20);
            this.lblRightFilename.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRightFilename.Name = "lblRightFilename";
            this.lblRightFilename.Size = new System.Drawing.Size(82, 15);
            this.lblRightFilename.TabIndex = 0;
            this.lblRightFilename.Text = "基准影像：";
            // 
            // lblRadio
            // 
            this.lblRadio.AutoSize = true;
            this.lblRadio.BackColor = System.Drawing.Color.Transparent;
            this.lblRadio.Location = new System.Drawing.Point(35, 24);
            this.lblRadio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRadio.Name = "lblRadio";
            this.lblRadio.Size = new System.Drawing.Size(82, 15);
            this.lblRadio.TabIndex = 0;
            this.lblRadio.Text = "匹配系数：";
            // 
            // dbiRatio
            // 
            // 
            // 
            // 
            this.dbiRatio.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiRatio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiRatio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiRatio.Increment = 0.05D;
            this.dbiRatio.Location = new System.Drawing.Point(136, 19);
            this.dbiRatio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dbiRatio.MaxValue = 1D;
            this.dbiRatio.MinValue = 0D;
            this.dbiRatio.Name = "dbiRatio";
            this.dbiRatio.ShowUpDown = true;
            this.dbiRatio.Size = new System.Drawing.Size(107, 25);
            this.dbiRatio.TabIndex = 3;
            this.dbiRatio.Value = 0.7D;
            // 
            // lblMaxBlockSize
            // 
            this.lblMaxBlockSize.AutoSize = true;
            this.lblMaxBlockSize.BackColor = System.Drawing.Color.Transparent;
            this.lblMaxBlockSize.Location = new System.Drawing.Point(35, 75);
            this.lblMaxBlockSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxBlockSize.Name = "lblMaxBlockSize";
            this.lblMaxBlockSize.Size = new System.Drawing.Size(82, 15);
            this.lblMaxBlockSize.TabIndex = 0;
            this.lblMaxBlockSize.Text = "分块大小：";
            // 
            // lblOverlaySize
            // 
            this.lblOverlaySize.AutoSize = true;
            this.lblOverlaySize.BackColor = System.Drawing.Color.Transparent;
            this.lblOverlaySize.Location = new System.Drawing.Point(285, 75);
            this.lblOverlaySize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOverlaySize.Name = "lblOverlaySize";
            this.lblOverlaySize.Size = new System.Drawing.Size(97, 15);
            this.lblOverlaySize.TabIndex = 0;
            this.lblOverlaySize.Text = "重叠区大小：";
            // 
            // iiMaxBlockSize
            // 
            // 
            // 
            // 
            this.iiMaxBlockSize.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iiMaxBlockSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iiMaxBlockSize.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iiMaxBlockSize.Location = new System.Drawing.Point(136, 70);
            this.iiMaxBlockSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iiMaxBlockSize.MaxValue = 1280;
            this.iiMaxBlockSize.MinValue = 0;
            this.iiMaxBlockSize.Name = "iiMaxBlockSize";
            this.iiMaxBlockSize.ShowUpDown = true;
            this.iiMaxBlockSize.Size = new System.Drawing.Size(107, 25);
            this.iiMaxBlockSize.TabIndex = 4;
            this.iiMaxBlockSize.Value = 1280;
            // 
            // iiOverlaySize
            // 
            // 
            // 
            // 
            this.iiOverlaySize.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iiOverlaySize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iiOverlaySize.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iiOverlaySize.Location = new System.Drawing.Point(391, 70);
            this.iiOverlaySize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iiOverlaySize.Name = "iiOverlaySize";
            this.iiOverlaySize.ShowUpDown = true;
            this.iiOverlaySize.Size = new System.Drawing.Size(103, 25);
            this.iiOverlaySize.TabIndex = 5;
            this.iiOverlaySize.Value = 100;
            // 
            // txtRightImage
            // 
            this.txtRightImage.DisplayMember = "Text";
            this.txtRightImage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.txtRightImage.FormattingEnabled = true;
            this.txtRightImage.ItemHeight = 15;
            this.txtRightImage.Location = new System.Drawing.Point(121, 15);
            this.txtRightImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRightImage.Name = "txtRightImage";
            this.txtRightImage.Size = new System.Drawing.Size(424, 21);
            this.txtRightImage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtRightImage.TabIndex = 7;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.doubleRansac);
            this.groupPanel1.Controls.Add(this.label1);
            this.groupPanel1.Controls.Add(this.dbiRatio);
            this.groupPanel1.Controls.Add(this.lblRadio);
            this.groupPanel1.Controls.Add(this.iiOverlaySize);
            this.groupPanel1.Controls.Add(this.lblMaxBlockSize);
            this.groupPanel1.Controls.Add(this.iiMaxBlockSize);
            this.groupPanel1.Controls.Add(this.lblOverlaySize);
            this.groupPanel1.Location = new System.Drawing.Point(16, 56);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(531, 152);
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
            this.groupPanel1.TabIndex = 8;
            this.groupPanel1.Text = "配准参数";
            // 
            // doubleRansac
            // 
            // 
            // 
            // 
            this.doubleRansac.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleRansac.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleRansac.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleRansac.Increment = 0.5D;
            this.doubleRansac.Location = new System.Drawing.Point(387, 19);
            this.doubleRansac.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.doubleRansac.MaxValue = 100000D;
            this.doubleRansac.MinValue = 0D;
            this.doubleRansac.Name = "doubleRansac";
            this.doubleRansac.ShowUpDown = true;
            this.doubleRansac.Size = new System.Drawing.Size(107, 25);
            this.doubleRansac.TabIndex = 3;
            this.doubleRansac.Value = 10D;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(285, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ransac阈值：";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(307, 246);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 29);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(441, 246);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 29);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmSiftMatching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 308);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.txtRightImage);
            this.Controls.Add(this.lblRightFilename);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSiftMatching";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动匹配";
            this.Load += new System.EventHandler(this.FrmSiftMatching_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbiRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iiMaxBlockSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iiOverlaySize)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleRansac)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRightFilename;
        private System.Windows.Forms.Label lblRadio;
        private DevComponents.Editors.DoubleInput dbiRatio;
        private System.Windows.Forms.Label lblMaxBlockSize;
        private System.Windows.Forms.Label lblOverlaySize;
        private DevComponents.Editors.IntegerInput iiMaxBlockSize;
        private DevComponents.Editors.IntegerInput iiOverlaySize;
        private DevComponents.DotNetBar.Controls.ComboBoxEx txtRightImage;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.Editors.DoubleInput doubleRansac;
        private System.Windows.Forms.Label label1;
    }
}