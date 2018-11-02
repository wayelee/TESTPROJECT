namespace LibCerMap
{
    partial class FrmSkylinePara
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
            this.textBoxPicFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxParaFile = new System.Windows.Forms.TextBox();
            this.buttonPicFile = new System.Windows.Forms.Button();
            this.buttonParaFile = new System.Windows.Forms.Button();
            this.buttonCancle = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.btnXYZPHK = new DevComponents.DotNetBar.ButtonX();
            this.txtBeginYaw = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.txtPitchAngle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.txtExpAngle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txtOriKap = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtOriPhi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtOriOmg = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtPosZ = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtPosY = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtPosX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.doubleInputSunAngle = new DevComponents.Editors.DoubleInput();
            this.doubleInputForcusDis = new DevComponents.Editors.DoubleInput();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.doubleInputX0 = new DevComponents.Editors.DoubleInput();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.doubleInputY0 = new DevComponents.Editors.DoubleInput();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputSunAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputForcusDis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputX0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputY0)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPicFile
            // 
            this.textBoxPicFile.Location = new System.Drawing.Point(12, 190);
            this.textBoxPicFile.Name = "textBoxPicFile";
            this.textBoxPicFile.Size = new System.Drawing.Size(471, 21);
            this.textBoxPicFile.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "图像文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "参数文件";
            this.label2.Visible = false;
            // 
            // textBoxParaFile
            // 
            this.textBoxParaFile.Location = new System.Drawing.Point(14, 279);
            this.textBoxParaFile.Name = "textBoxParaFile";
            this.textBoxParaFile.Size = new System.Drawing.Size(396, 21);
            this.textBoxParaFile.TabIndex = 0;
            this.textBoxParaFile.Visible = false;
            // 
            // buttonPicFile
            // 
            this.buttonPicFile.Location = new System.Drawing.Point(489, 188);
            this.buttonPicFile.Name = "buttonPicFile";
            this.buttonPicFile.Size = new System.Drawing.Size(75, 23);
            this.buttonPicFile.TabIndex = 2;
            this.buttonPicFile.Text = "...";
            this.buttonPicFile.UseVisualStyleBackColor = true;
            this.buttonPicFile.Click += new System.EventHandler(this.buttonPicFile_Click);
            // 
            // buttonParaFile
            // 
            this.buttonParaFile.Location = new System.Drawing.Point(428, 277);
            this.buttonParaFile.Name = "buttonParaFile";
            this.buttonParaFile.Size = new System.Drawing.Size(75, 23);
            this.buttonParaFile.TabIndex = 2;
            this.buttonParaFile.UseVisualStyleBackColor = true;
            this.buttonParaFile.Visible = false;
            this.buttonParaFile.Click += new System.EventHandler(this.buttonParaFile_Click);
            // 
            // buttonCancle
            // 
            this.buttonCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancle.Location = new System.Drawing.Point(481, 229);
            this.buttonCancle.Name = "buttonCancle";
            this.buttonCancle.Size = new System.Drawing.Size(82, 23);
            this.buttonCancle.TabIndex = 3;
            this.buttonCancle.Text = "Cancel";
            this.buttonCancle.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(384, 229);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(82, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // btnXYZPHK
            // 
            this.btnXYZPHK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXYZPHK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXYZPHK.Location = new System.Drawing.Point(420, 135);
            this.btnXYZPHK.Name = "btnXYZPHK";
            this.btnXYZPHK.Size = new System.Drawing.Size(171, 22);
            this.btnXYZPHK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXYZPHK.TabIndex = 72;
            this.btnXYZPHK.Text = "从全局段获取位置姿态信息";
            this.btnXYZPHK.Click += new System.EventHandler(this.btnXYZPHK_Click);
            // 
            // txtBeginYaw
            // 
            // 
            // 
            // 
            this.txtBeginYaw.Border.Class = "TextBoxBorder";
            this.txtBeginYaw.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBeginYaw.Location = new System.Drawing.Point(297, 73);
            this.txtBeginYaw.Name = "txtBeginYaw";
            this.txtBeginYaw.Size = new System.Drawing.Size(78, 21);
            this.txtBeginYaw.TabIndex = 67;
            this.txtBeginYaw.Text = "1";
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(201, 73);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(108, 23);
            this.labelX13.TabIndex = 66;
            this.labelX13.Text = "旋转臂偏航角：";
            // 
            // txtPitchAngle
            // 
            // 
            // 
            // 
            this.txtPitchAngle.Border.Class = "TextBoxBorder";
            this.txtPitchAngle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPitchAngle.Location = new System.Drawing.Point(489, 77);
            this.txtPitchAngle.Name = "txtPitchAngle";
            this.txtPitchAngle.Size = new System.Drawing.Size(75, 21);
            this.txtPitchAngle.TabIndex = 65;
            this.txtPitchAngle.Text = "1";
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(393, 77);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(108, 23);
            this.labelX9.TabIndex = 64;
            this.labelX9.Text = "旋转臂俯仰角 ：";
            // 
            // txtExpAngle
            // 
            // 
            // 
            // 
            this.txtExpAngle.Border.Class = "TextBoxBorder";
            this.txtExpAngle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtExpAngle.Location = new System.Drawing.Point(108, 77);
            this.txtExpAngle.Name = "txtExpAngle";
            this.txtExpAngle.Size = new System.Drawing.Size(78, 21);
            this.txtExpAngle.TabIndex = 63;
            this.txtExpAngle.Text = "1";
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(12, 77);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(108, 23);
            this.labelX7.TabIndex = 62;
            this.labelX7.Text = "旋转臂展开角 ：";
            // 
            // txtOriKap
            // 
            // 
            // 
            // 
            this.txtOriKap.Border.Class = "TextBoxBorder";
            this.txtOriKap.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOriKap.Location = new System.Drawing.Point(489, 42);
            this.txtOriKap.Name = "txtOriKap";
            this.txtOriKap.Size = new System.Drawing.Size(75, 21);
            this.txtOriKap.TabIndex = 61;
            this.txtOriKap.Text = "1";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(393, 42);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(108, 23);
            this.labelX6.TabIndex = 60;
            this.labelX6.Text = "巡视器姿态kap ：";
            // 
            // txtOriPhi
            // 
            // 
            // 
            // 
            this.txtOriPhi.Border.Class = "TextBoxBorder";
            this.txtOriPhi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOriPhi.Location = new System.Drawing.Point(299, 44);
            this.txtOriPhi.Name = "txtOriPhi";
            this.txtOriPhi.Size = new System.Drawing.Size(76, 21);
            this.txtOriPhi.TabIndex = 59;
            this.txtOriPhi.Text = "1";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(203, 44);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(108, 23);
            this.labelX5.TabIndex = 58;
            this.labelX5.Text = "巡视器姿态phi ：";
            // 
            // txtOriOmg
            // 
            // 
            // 
            // 
            this.txtOriOmg.Border.Class = "TextBoxBorder";
            this.txtOriOmg.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOriOmg.Location = new System.Drawing.Point(108, 44);
            this.txtOriOmg.Name = "txtOriOmg";
            this.txtOriOmg.Size = new System.Drawing.Size(78, 21);
            this.txtOriOmg.TabIndex = 57;
            this.txtOriOmg.Text = "1";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 44);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(108, 23);
            this.labelX4.TabIndex = 56;
            this.labelX4.Text = "巡视器姿态omg ：";
            // 
            // txtPosZ
            // 
            // 
            // 
            // 
            this.txtPosZ.Border.Class = "TextBoxBorder";
            this.txtPosZ.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPosZ.Location = new System.Drawing.Point(489, 12);
            this.txtPosZ.Name = "txtPosZ";
            this.txtPosZ.Size = new System.Drawing.Size(75, 21);
            this.txtPosZ.TabIndex = 55;
            this.txtPosZ.Text = "1";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(393, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(93, 23);
            this.labelX3.TabIndex = 54;
            this.labelX3.Text = "巡视器位置Z ：";
            // 
            // txtPosY
            // 
            // 
            // 
            // 
            this.txtPosY.Border.Class = "TextBoxBorder";
            this.txtPosY.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPosY.Location = new System.Drawing.Point(299, 12);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(76, 21);
            this.txtPosY.TabIndex = 53;
            this.txtPosY.Text = "1";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(203, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(93, 23);
            this.labelX2.TabIndex = 52;
            this.labelX2.Text = "巡视器位置Y ：";
            // 
            // txtPosX
            // 
            // 
            // 
            // 
            this.txtPosX.Border.Class = "TextBoxBorder";
            this.txtPosX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPosX.Location = new System.Drawing.Point(108, 12);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(78, 21);
            this.txtPosX.TabIndex = 51;
            this.txtPosX.Text = "1";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(93, 23);
            this.labelX1.TabIndex = 50;
            this.labelX1.Text = "巡视器位置X ：";
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(203, 104);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(108, 23);
            this.labelX8.TabIndex = 64;
            this.labelX8.Text = "太阳水平方位 ：";
            // 
            // doubleInputSunAngle
            // 
            // 
            // 
            // 
            this.doubleInputSunAngle.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputSunAngle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputSunAngle.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputSunAngle.Increment = 1D;
            this.doubleInputSunAngle.Location = new System.Drawing.Point(299, 104);
            this.doubleInputSunAngle.MaxValue = 360D;
            this.doubleInputSunAngle.MinValue = 0D;
            this.doubleInputSunAngle.Name = "doubleInputSunAngle";
            this.doubleInputSunAngle.ShowUpDown = true;
            this.doubleInputSunAngle.Size = new System.Drawing.Size(80, 21);
            this.doubleInputSunAngle.TabIndex = 73;
            this.doubleInputSunAngle.Value = 2D;
            // 
            // doubleInputForcusDis
            // 
            // 
            // 
            // 
            this.doubleInputForcusDis.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputForcusDis.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputForcusDis.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputForcusDis.Increment = 1D;
            this.doubleInputForcusDis.Location = new System.Drawing.Point(106, 106);
            this.doubleInputForcusDis.MaxValue = 9999D;
            this.doubleInputForcusDis.MinValue = -9999D;
            this.doubleInputForcusDis.Name = "doubleInputForcusDis";
            this.doubleInputForcusDis.ShowUpDown = true;
            this.doubleInputForcusDis.Size = new System.Drawing.Size(80, 21);
            this.doubleInputForcusDis.TabIndex = 73;
            this.doubleInputForcusDis.Value = 3333.33D;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(10, 106);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(73, 23);
            this.labelX10.TabIndex = 60;
            this.labelX10.Text = "焦距：";
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(10, 136);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(108, 23);
            this.labelX11.TabIndex = 64;
            this.labelX11.Text = "像主点X0：";
            // 
            // doubleInputX0
            // 
            // 
            // 
            // 
            this.doubleInputX0.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputX0.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputX0.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputX0.Increment = 1D;
            this.doubleInputX0.Location = new System.Drawing.Point(106, 136);
            this.doubleInputX0.MaxValue = 9999D;
            this.doubleInputX0.MinValue = -9999D;
            this.doubleInputX0.Name = "doubleInputX0";
            this.doubleInputX0.ShowUpDown = true;
            this.doubleInputX0.Size = new System.Drawing.Size(80, 21);
            this.doubleInputX0.TabIndex = 73;
            this.doubleInputX0.Value = 587.5D;
            // 
            // labelX12
            // 
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Location = new System.Drawing.Point(203, 135);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(108, 23);
            this.labelX12.TabIndex = 64;
            this.labelX12.Text = "像主点Y0：";
            // 
            // doubleInputY0
            // 
            // 
            // 
            // 
            this.doubleInputY0.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputY0.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputY0.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputY0.Increment = 1D;
            this.doubleInputY0.Location = new System.Drawing.Point(299, 135);
            this.doubleInputY0.MaxValue = 9999D;
            this.doubleInputY0.MinValue = -9999D;
            this.doubleInputY0.Name = "doubleInputY0";
            this.doubleInputY0.ShowUpDown = true;
            this.doubleInputY0.Size = new System.Drawing.Size(80, 21);
            this.doubleInputY0.TabIndex = 73;
            this.doubleInputY0.Value = 431.5D;
            // 
            // FrmSkylinePara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 277);
            this.Controls.Add(this.doubleInputForcusDis);
            this.Controls.Add(this.doubleInputY0);
            this.Controls.Add(this.doubleInputX0);
            this.Controls.Add(this.doubleInputSunAngle);
            this.Controls.Add(this.btnXYZPHK);
            this.Controls.Add(this.txtBeginYaw);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.labelX13);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.txtPitchAngle);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.txtExpAngle);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.txtOriKap);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtOriPhi);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtOriOmg);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtPosZ);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtPosY);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtPosX);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancle);
            this.Controls.Add(this.buttonParaFile);
            this.Controls.Add(this.buttonPicFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxParaFile);
            this.Controls.Add(this.textBoxPicFile);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSkylinePara";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.FrmSkylinePara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputSunAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputForcusDis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputX0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputY0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPicFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxParaFile;
        private System.Windows.Forms.Button buttonPicFile;
        private System.Windows.Forms.Button buttonParaFile;
        private System.Windows.Forms.Button buttonCancle;
        private System.Windows.Forms.Button buttonOK;
        private DevComponents.DotNetBar.ButtonX btnXYZPHK;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBeginYaw;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPitchAngle;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExpAngle;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOriKap;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOriPhi;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOriOmg;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPosZ;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPosY;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPosX;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.Editors.DoubleInput doubleInputSunAngle;
        private DevComponents.Editors.DoubleInput doubleInputForcusDis;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.Editors.DoubleInput doubleInputX0;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.Editors.DoubleInput doubleInputY0;
    }
}