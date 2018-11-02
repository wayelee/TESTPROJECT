namespace LibCerMap
{
    partial class FrmJSCameraLabel
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmbCamImage = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.BtnOpenImage = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtOri1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtOri2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtOri3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSaveLabel = new DevComponents.DotNetBar.ButtonX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtSaveLabel = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.txtExpAngleSun = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtYawAngleSun = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtExpAngleEarth = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtYawAngleEarth = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 22);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(91, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "监视相机图像：";
            // 
            // cmbCamImage
            // 
            this.cmbCamImage.DisplayMember = "Text";
            this.cmbCamImage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCamImage.FormattingEnabled = true;
            this.cmbCamImage.ItemHeight = 15;
            this.cmbCamImage.Location = new System.Drawing.Point(100, 22);
            this.cmbCamImage.Name = "cmbCamImage";
            this.cmbCamImage.Size = new System.Drawing.Size(342, 21);
            this.cmbCamImage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbCamImage.TabIndex = 1;
            this.cmbCamImage.SelectedIndexChanged += new System.EventHandler(this.cmbCamImage_SelectedIndexChanged);
            // 
            // BtnOpenImage
            // 
            this.BtnOpenImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnOpenImage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnOpenImage.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.BtnOpenImage.Location = new System.Drawing.Point(449, 22);
            this.BtnOpenImage.Name = "BtnOpenImage";
            this.BtnOpenImage.Size = new System.Drawing.Size(30, 23);
            this.BtnOpenImage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnOpenImage.TabIndex = 30;
            this.BtnOpenImage.Click += new System.EventHandler(this.BtnOpenImage_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(237, 59);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(91, 23);
            this.labelX2.TabIndex = 31;
            this.labelX2.Text = "着陆器姿态θ：";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 59);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(91, 23);
            this.labelX3.TabIndex = 32;
            this.labelX3.Text = "着陆器姿态φ：";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 92);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(91, 23);
            this.labelX4.TabIndex = 33;
            this.labelX4.Text = "着陆器姿态Ψ：";
            // 
            // txtOri1
            // 
            // 
            // 
            // 
            this.txtOri1.Border.Class = "TextBoxBorder";
            this.txtOri1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOri1.Location = new System.Drawing.Point(100, 59);
            this.txtOri1.Name = "txtOri1";
            this.txtOri1.Size = new System.Drawing.Size(115, 21);
            this.txtOri1.TabIndex = 34;
            // 
            // txtOri2
            // 
            // 
            // 
            // 
            this.txtOri2.Border.Class = "TextBoxBorder";
            this.txtOri2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOri2.Location = new System.Drawing.Point(327, 59);
            this.txtOri2.Name = "txtOri2";
            this.txtOri2.Size = new System.Drawing.Size(115, 21);
            this.txtOri2.TabIndex = 35;
            // 
            // txtOri3
            // 
            // 
            // 
            // 
            this.txtOri3.Border.Class = "TextBoxBorder";
            this.txtOri3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOri3.Location = new System.Drawing.Point(100, 92);
            this.txtOri3.Name = "txtOri3";
            this.txtOri3.Size = new System.Drawing.Size(115, 21);
            this.txtOri3.TabIndex = 36;
            // 
            // btnSaveLabel
            // 
            this.btnSaveLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveLabel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveLabel.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.btnSaveLabel.Location = new System.Drawing.Point(449, 204);
            this.btnSaveLabel.Name = "btnSaveLabel";
            this.btnSaveLabel.Size = new System.Drawing.Size(30, 23);
            this.btnSaveLabel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveLabel.TabIndex = 43;
            this.btnSaveLabel.Click += new System.EventHandler(this.btnSaveLabel_Click);
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(11, 204);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(91, 23);
            this.labelX6.TabIndex = 41;
            this.labelX6.Text = "标注图层位置：";
            // 
            // txtSaveLabel
            // 
            // 
            // 
            // 
            this.txtSaveLabel.Border.Class = "TextBoxBorder";
            this.txtSaveLabel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSaveLabel.Location = new System.Drawing.Point(100, 204);
            this.txtSaveLabel.Name = "txtSaveLabel";
            this.txtSaveLabel.Size = new System.Drawing.Size(342, 21);
            this.txtSaveLabel.TabIndex = 45;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Location = new System.Drawing.Point(303, 254);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 46;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.Location = new System.Drawing.Point(410, 254);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(69, 23);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 47;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // txtExpAngleSun
            // 
            // 
            // 
            // 
            this.txtExpAngleSun.Border.Class = "TextBoxBorder";
            this.txtExpAngleSun.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtExpAngleSun.Location = new System.Drawing.Point(327, 131);
            this.txtExpAngleSun.Name = "txtExpAngleSun";
            this.txtExpAngleSun.Size = new System.Drawing.Size(115, 21);
            this.txtExpAngleSun.TabIndex = 51;
            // 
            // txtYawAngleSun
            // 
            // 
            // 
            // 
            this.txtYawAngleSun.Border.Class = "TextBoxBorder";
            this.txtYawAngleSun.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtYawAngleSun.Location = new System.Drawing.Point(100, 131);
            this.txtYawAngleSun.Name = "txtYawAngleSun";
            this.txtYawAngleSun.Size = new System.Drawing.Size(115, 21);
            this.txtYawAngleSun.TabIndex = 50;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(12, 131);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(91, 23);
            this.labelX7.TabIndex = 49;
            this.labelX7.Text = "太阳高度角：";
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(237, 131);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(91, 23);
            this.labelX8.TabIndex = 48;
            this.labelX8.Text = "太阳方位角：";
            // 
            // txtExpAngleEarth
            // 
            // 
            // 
            // 
            this.txtExpAngleEarth.Border.Class = "TextBoxBorder";
            this.txtExpAngleEarth.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtExpAngleEarth.Location = new System.Drawing.Point(327, 164);
            this.txtExpAngleEarth.Name = "txtExpAngleEarth";
            this.txtExpAngleEarth.Size = new System.Drawing.Size(115, 21);
            this.txtExpAngleEarth.TabIndex = 55;
            // 
            // txtYawAngleEarth
            // 
            // 
            // 
            // 
            this.txtYawAngleEarth.Border.Class = "TextBoxBorder";
            this.txtYawAngleEarth.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtYawAngleEarth.Location = new System.Drawing.Point(100, 164);
            this.txtYawAngleEarth.Name = "txtYawAngleEarth";
            this.txtYawAngleEarth.Size = new System.Drawing.Size(115, 21);
            this.txtYawAngleEarth.TabIndex = 54;
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(12, 164);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(91, 23);
            this.labelX9.TabIndex = 53;
            this.labelX9.Text = "地球高度角：";
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(237, 164);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(91, 23);
            this.labelX10.TabIndex = 52;
            this.labelX10.Text = "地球方位角：";
            // 
            // FrmJSCameraLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 299);
            this.Controls.Add(this.txtExpAngleEarth);
            this.Controls.Add(this.txtYawAngleEarth);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.txtExpAngleSun);
            this.Controls.Add(this.txtYawAngleSun);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSaveLabel);
            this.Controls.Add(this.btnSaveLabel);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtOri3);
            this.Controls.Add(this.txtOri2);
            this.Controls.Add(this.txtOri1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.BtnOpenImage);
            this.Controls.Add(this.cmbCamImage);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmJSCameraLabel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监视相机标注";
            this.Load += new System.EventHandler(this.FrmJSCameraLabel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbCamImage;
        private DevComponents.DotNetBar.ButtonX BtnOpenImage;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOri1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOri2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOri3;
        private DevComponents.DotNetBar.ButtonX btnSaveLabel;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSaveLabel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExpAngleSun;
        private DevComponents.DotNetBar.Controls.TextBoxX txtYawAngleSun;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExpAngleEarth;
        private DevComponents.DotNetBar.Controls.TextBoxX txtYawAngleEarth;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX10;
    }
}