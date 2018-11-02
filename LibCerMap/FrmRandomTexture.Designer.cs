namespace LibCerMap
{
    partial class FrmRandomTexture
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
            this.grpTexturesInfo = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.cmbLayers = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.picThirdTexture = new System.Windows.Forms.PictureBox();
            this.picSecondTexture = new System.Windows.Forms.PictureBox();
            this.picFirstTexture = new System.Windows.Forms.PictureBox();
            this.txtThirdValue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSecondValue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtFirstValue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.sldThirdValue = new DevComponents.DotNetBar.Controls.Slider();
            this.sldSecondValue = new DevComponents.DotNetBar.Controls.Slider();
            this.sldFirstValue = new DevComponents.DotNetBar.Controls.Slider();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOutputFilename = new DevComponents.DotNetBar.ButtonX();
            this.txtOutputFilename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblOutputFilename = new DevComponents.DotNetBar.LabelX();
            this.grpTexturesInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThirdTexture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSecondTexture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFirstTexture)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTexturesInfo
            // 
            this.grpTexturesInfo.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpTexturesInfo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grpTexturesInfo.Controls.Add(this.picPreview);
            this.grpTexturesInfo.Controls.Add(this.cmbLayers);
            this.grpTexturesInfo.Controls.Add(this.picThirdTexture);
            this.grpTexturesInfo.Controls.Add(this.lblOutputFilename);
            this.grpTexturesInfo.Controls.Add(this.picSecondTexture);
            this.grpTexturesInfo.Controls.Add(this.txtOutputFilename);
            this.grpTexturesInfo.Controls.Add(this.btnOutputFilename);
            this.grpTexturesInfo.Controls.Add(this.picFirstTexture);
            this.grpTexturesInfo.Controls.Add(this.txtThirdValue);
            this.grpTexturesInfo.Controls.Add(this.txtSecondValue);
            this.grpTexturesInfo.Controls.Add(this.txtFirstValue);
            this.grpTexturesInfo.Controls.Add(this.sldThirdValue);
            this.grpTexturesInfo.Controls.Add(this.sldSecondValue);
            this.grpTexturesInfo.Controls.Add(this.sldFirstValue);
            this.grpTexturesInfo.Location = new System.Drawing.Point(12, 12);
            this.grpTexturesInfo.Name = "grpTexturesInfo";
            this.grpTexturesInfo.Size = new System.Drawing.Size(870, 419);
            // 
            // 
            // 
            this.grpTexturesInfo.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpTexturesInfo.Style.BackColorGradientAngle = 90;
            this.grpTexturesInfo.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpTexturesInfo.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpTexturesInfo.Style.BorderBottomWidth = 1;
            this.grpTexturesInfo.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpTexturesInfo.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpTexturesInfo.Style.BorderLeftWidth = 1;
            this.grpTexturesInfo.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpTexturesInfo.Style.BorderRightWidth = 1;
            this.grpTexturesInfo.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpTexturesInfo.Style.BorderTopWidth = 1;
            this.grpTexturesInfo.Style.CornerDiameter = 4;
            this.grpTexturesInfo.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpTexturesInfo.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpTexturesInfo.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpTexturesInfo.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpTexturesInfo.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpTexturesInfo.TabIndex = 1;
            this.grpTexturesInfo.Text = "纹理信息";
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(3, 10);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(394, 334);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 11;
            this.picPreview.TabStop = false;
            // 
            // cmbLayers
            // 
            this.cmbLayers.DisplayMember = "Text";
            this.cmbLayers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLayers.FormattingEnabled = true;
            this.cmbLayers.ItemHeight = 15;
            this.cmbLayers.Location = new System.Drawing.Point(403, 27);
            this.cmbLayers.Name = "cmbLayers";
            this.cmbLayers.Size = new System.Drawing.Size(133, 21);
            this.cmbLayers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLayers.TabIndex = 1;
            this.cmbLayers.SelectedValueChanged += new System.EventHandler(this.cmbLayers_SelectedValueChanged);
            // 
            // picThirdTexture
            // 
            this.picThirdTexture.Location = new System.Drawing.Point(403, 271);
            this.picThirdTexture.Name = "picThirdTexture";
            this.picThirdTexture.Size = new System.Drawing.Size(133, 48);
            this.picThirdTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picThirdTexture.TabIndex = 9;
            this.picThirdTexture.TabStop = false;
            this.picThirdTexture.Click += new System.EventHandler(this.picThirdTexture_Click);
            // 
            // picSecondTexture
            // 
            this.picSecondTexture.Location = new System.Drawing.Point(403, 178);
            this.picSecondTexture.Name = "picSecondTexture";
            this.picSecondTexture.Size = new System.Drawing.Size(121, 62);
            this.picSecondTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSecondTexture.TabIndex = 8;
            this.picSecondTexture.TabStop = false;
            this.picSecondTexture.Click += new System.EventHandler(this.picSecondTexture_Click);
            // 
            // picFirstTexture
            // 
            this.picFirstTexture.Location = new System.Drawing.Point(403, 75);
            this.picFirstTexture.Name = "picFirstTexture";
            this.picFirstTexture.Size = new System.Drawing.Size(121, 65);
            this.picFirstTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFirstTexture.TabIndex = 7;
            this.picFirstTexture.TabStop = false;
            this.picFirstTexture.Click += new System.EventHandler(this.picFirstTexture_Click);
            // 
            // txtThirdValue
            // 
            // 
            // 
            // 
            this.txtThirdValue.Border.Class = "TextBoxBorder";
            this.txtThirdValue.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtThirdValue.Location = new System.Drawing.Point(756, 285);
            this.txtThirdValue.Name = "txtThirdValue";
            this.txtThirdValue.Size = new System.Drawing.Size(100, 21);
            this.txtThirdValue.TabIndex = 4;
            this.txtThirdValue.TextChanged += new System.EventHandler(this.txtThirdValue_TextChanged);
            // 
            // txtSecondValue
            // 
            // 
            // 
            // 
            this.txtSecondValue.Border.Class = "TextBoxBorder";
            this.txtSecondValue.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSecondValue.Location = new System.Drawing.Point(756, 200);
            this.txtSecondValue.Name = "txtSecondValue";
            this.txtSecondValue.Size = new System.Drawing.Size(100, 21);
            this.txtSecondValue.TabIndex = 3;
            this.txtSecondValue.TextChanged += new System.EventHandler(this.txtSecondValue_TextChanged);
            // 
            // txtFirstValue
            // 
            // 
            // 
            // 
            this.txtFirstValue.Border.Class = "TextBoxBorder";
            this.txtFirstValue.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFirstValue.Location = new System.Drawing.Point(756, 117);
            this.txtFirstValue.Name = "txtFirstValue";
            this.txtFirstValue.Size = new System.Drawing.Size(100, 21);
            this.txtFirstValue.TabIndex = 2;
            this.txtFirstValue.TextChanged += new System.EventHandler(this.txtFirstValue_TextChanged);
            // 
            // sldThirdValue
            // 
            // 
            // 
            // 
            this.sldThirdValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sldThirdValue.LabelVisible = false;
            this.sldThirdValue.Location = new System.Drawing.Point(553, 283);
            this.sldThirdValue.Maximum = 255;
            this.sldThirdValue.Name = "sldThirdValue";
            this.sldThirdValue.Size = new System.Drawing.Size(184, 23);
            this.sldThirdValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sldThirdValue.TabIndex = 4;
            this.sldThirdValue.TabStop = false;
            this.sldThirdValue.Text = "slider1";
            this.sldThirdValue.Value = 255;
            this.sldThirdValue.ValueChanged += new System.EventHandler(this.sldThirdValue_ValueChanged);
            this.sldThirdValue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sldThirdValue_MouseUp);
            // 
            // sldSecondValue
            // 
            // 
            // 
            // 
            this.sldSecondValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sldSecondValue.LabelVisible = false;
            this.sldSecondValue.Location = new System.Drawing.Point(553, 200);
            this.sldSecondValue.Maximum = 255;
            this.sldSecondValue.Name = "sldSecondValue";
            this.sldSecondValue.Size = new System.Drawing.Size(184, 23);
            this.sldSecondValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sldSecondValue.TabIndex = 3;
            this.sldSecondValue.TabStop = false;
            this.sldSecondValue.Text = "slider1";
            this.sldSecondValue.Value = 170;
            this.sldSecondValue.ValueChanged += new System.EventHandler(this.sldSecondValue_ValueChanged);
            this.sldSecondValue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sldSecondValue_MouseUp);
            // 
            // sldFirstValue
            // 
            // 
            // 
            // 
            this.sldFirstValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sldFirstValue.LabelVisible = false;
            this.sldFirstValue.Location = new System.Drawing.Point(553, 117);
            this.sldFirstValue.Maximum = 255;
            this.sldFirstValue.Name = "sldFirstValue";
            this.sldFirstValue.Size = new System.Drawing.Size(184, 23);
            this.sldFirstValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sldFirstValue.TabIndex = 2;
            this.sldFirstValue.TabStop = false;
            this.sldFirstValue.Text = "slider1";
            this.sldFirstValue.Value = 85;
            this.sldFirstValue.ValueChanged += new System.EventHandler(this.sldFirstValue_ValueChanged);
            this.sldFirstValue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sldFirstValue_MouseUp);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(702, 454);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(796, 454);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            // 
            // btnOutputFilename
            // 
            this.btnOutputFilename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutputFilename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutputFilename.Location = new System.Drawing.Point(781, 356);
            this.btnOutputFilename.Name = "btnOutputFilename";
            this.btnOutputFilename.Size = new System.Drawing.Size(75, 23);
            this.btnOutputFilename.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOutputFilename.TabIndex = 5;
            this.btnOutputFilename.Text = "...";
            this.btnOutputFilename.Click += new System.EventHandler(this.btnOutputFilename_Click);
            // 
            // txtOutputFilename
            // 
            // 
            // 
            // 
            this.txtOutputFilename.Border.Class = "TextBoxBorder";
            this.txtOutputFilename.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutputFilename.Location = new System.Drawing.Point(484, 358);
            this.txtOutputFilename.Name = "txtOutputFilename";
            this.txtOutputFilename.Size = new System.Drawing.Size(283, 21);
            this.txtOutputFilename.TabIndex = 5;
            // 
            // lblOutputFilename
            // 
            this.lblOutputFilename.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblOutputFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblOutputFilename.Location = new System.Drawing.Point(403, 354);
            this.lblOutputFilename.Name = "lblOutputFilename";
            this.lblOutputFilename.Size = new System.Drawing.Size(75, 23);
            this.lblOutputFilename.TabIndex = 3;
            this.lblOutputFilename.Text = "输出路径：";
            // 
            // FrmRandomTexture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 489);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpTexturesInfo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRandomTexture";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "随机纹理";
            this.Load += new System.EventHandler(this.FrmRandomTexture_Load);
            this.grpTexturesInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThirdTexture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSecondTexture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFirstTexture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel grpTexturesInfo;
        private DevComponents.DotNetBar.Controls.Slider sldThirdValue;
        private DevComponents.DotNetBar.Controls.Slider sldSecondValue;
        private DevComponents.DotNetBar.Controls.Slider sldFirstValue;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.TextBoxX txtThirdValue;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSecondValue;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFirstValue;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLayers;
        private System.Windows.Forms.PictureBox picThirdTexture;
        private System.Windows.Forms.PictureBox picSecondTexture;
        private System.Windows.Forms.PictureBox picFirstTexture;
        private DevComponents.DotNetBar.ButtonX btnOutputFilename;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutputFilename;
        private DevComponents.DotNetBar.LabelX lblOutputFilename;
        private System.Windows.Forms.PictureBox picPreview;

    }
}