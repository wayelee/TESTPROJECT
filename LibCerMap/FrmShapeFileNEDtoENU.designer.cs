namespace LibCerMap
{
    partial class FrmShapeFileNEDtoENU
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
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbLayerTrans = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtLayerExp = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.BtnWorkBrowse = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.txtFeatureName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.radioButtonENUtoNED = new System.Windows.Forms.RadioButton();
            this.radioButtonNEDtoENU = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 41);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "转换矢量 ：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 121);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "输出空间 ：";
            // 
            // cmbLayerTrans
            // 
            this.cmbLayerTrans.DisplayMember = "Text";
            this.cmbLayerTrans.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLayerTrans.FormattingEnabled = true;
            this.cmbLayerTrans.ItemHeight = 15;
            this.cmbLayerTrans.Location = new System.Drawing.Point(90, 41);
            this.cmbLayerTrans.Name = "cmbLayerTrans";
            this.cmbLayerTrans.Size = new System.Drawing.Size(338, 21);
            this.cmbLayerTrans.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLayerTrans.TabIndex = 2;
            this.cmbLayerTrans.SelectedIndexChanged += new System.EventHandler(this.cmbLayerTrans_SelectedIndexChanged);
            // 
            // txtLayerExp
            // 
            // 
            // 
            // 
            this.txtLayerExp.Border.Class = "TextBoxBorder";
            this.txtLayerExp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLayerExp.Location = new System.Drawing.Point(90, 121);
            this.txtLayerExp.Name = "txtLayerExp";
            this.txtLayerExp.Size = new System.Drawing.Size(295, 21);
            this.txtLayerExp.TabIndex = 3;
            // 
            // BtnWorkBrowse
            // 
            this.BtnWorkBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnWorkBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnWorkBrowse.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.BtnWorkBrowse.Location = new System.Drawing.Point(398, 121);
            this.BtnWorkBrowse.Name = "BtnWorkBrowse";
            this.BtnWorkBrowse.Size = new System.Drawing.Size(30, 23);
            this.BtnWorkBrowse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnWorkBrowse.TabIndex = 30;
            this.BtnWorkBrowse.Click += new System.EventHandler(this.BtnWorkBrowse_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Location = new System.Drawing.Point(205, 214);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 31;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(324, 214);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 32;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // txtFeatureName
            // 
            // 
            // 
            // 
            this.txtFeatureName.Border.Class = "TextBoxBorder";
            this.txtFeatureName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFeatureName.Location = new System.Drawing.Point(90, 171);
            this.txtFeatureName.Name = "txtFeatureName";
            this.txtFeatureName.Size = new System.Drawing.Size(153, 21);
            this.txtFeatureName.TabIndex = 3;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(35, 171);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(52, 23);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "名称 ：";
            // 
            // radioButtonENUtoNED
            // 
            this.radioButtonENUtoNED.AutoSize = true;
            this.radioButtonENUtoNED.Location = new System.Drawing.Point(265, 87);
            this.radioButtonENUtoNED.Name = "radioButtonENUtoNED";
            this.radioButtonENUtoNED.Size = new System.Drawing.Size(113, 16);
            this.radioButtonENUtoNED.TabIndex = 35;
            this.radioButtonENUtoNED.Text = "东北天 - 北东地";
            this.radioButtonENUtoNED.UseVisualStyleBackColor = true;
            // 
            // radioButtonNEDtoENU
            // 
            this.radioButtonNEDtoENU.AutoSize = true;
            this.radioButtonNEDtoENU.Checked = true;
            this.radioButtonNEDtoENU.Location = new System.Drawing.Point(90, 87);
            this.radioButtonNEDtoENU.Name = "radioButtonNEDtoENU";
            this.radioButtonNEDtoENU.Size = new System.Drawing.Size(113, 16);
            this.radioButtonNEDtoENU.TabIndex = 34;
            this.radioButtonNEDtoENU.TabStop = true;
            this.radioButtonNEDtoENU.Text = "北东地 - 东北天";
            this.radioButtonNEDtoENU.UseVisualStyleBackColor = true;
            this.radioButtonNEDtoENU.CheckedChanged += new System.EventHandler(this.radioButtonNEDtoENU_CheckedChanged);
            // 
            // FrmShapeFileNEDtoENU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 255);
            this.Controls.Add(this.radioButtonENUtoNED);
            this.Controls.Add(this.radioButtonNEDtoENU);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.BtnWorkBrowse);
            this.Controls.Add(this.txtFeatureName);
            this.Controls.Add(this.txtLayerExp);
            this.Controls.Add(this.cmbLayerTrans);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmShapeFileNEDtoENU";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "东北天—北东地";
            this.Load += new System.EventHandler(this.FrmNEDtoENU_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLayerTrans;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLayerExp;
        private DevComponents.DotNetBar.ButtonX BtnWorkBrowse;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFeatureName;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.RadioButton radioButtonENUtoNED;
        private System.Windows.Forms.RadioButton radioButtonNEDtoENU;
    }
}