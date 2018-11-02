namespace LibCerMap
{
    partial class FrmOritationVector
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
            this.cmbSunField = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbPointLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lbltype = new DevComponents.DotNetBar.LabelX();
            this.cmbEarthField = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.txtOutFile = new System.Windows.Forms.TextBox();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.btnSelectFile = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // cmbSunField
            // 
            this.cmbSunField.DisplayMember = "Text";
            this.cmbSunField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSunField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSunField.FormattingEnabled = true;
            this.cmbSunField.ItemHeight = 17;
            this.cmbSunField.Location = new System.Drawing.Point(117, 50);
            this.cmbSunField.Name = "cmbSunField";
            this.cmbSunField.Size = new System.Drawing.Size(263, 23);
            this.cmbSunField.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSunField.TabIndex = 27;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(18, 53);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(93, 20);
            this.labelX4.TabIndex = 28;
            this.labelX4.Text = "太阳矢量字段：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(18, 150);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(70, 20);
            this.labelX2.TabIndex = 25;
            this.labelX2.Text = "输出图层：";
            // 
            // cmbPointLayer
            // 
            this.cmbPointLayer.DisplayMember = "Text";
            this.cmbPointLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPointLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPointLayer.FormattingEnabled = true;
            this.cmbPointLayer.ItemHeight = 17;
            this.cmbPointLayer.Location = new System.Drawing.Point(83, 9);
            this.cmbPointLayer.Name = "cmbPointLayer";
            this.cmbPointLayer.Size = new System.Drawing.Size(297, 23);
            this.cmbPointLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPointLayer.TabIndex = 23;
            this.cmbPointLayer.SelectedIndexChanged += new System.EventHandler(this.cmbPointLayer_SelectedIndexChanged);
            // 
            // lbltype
            // 
            // 
            // 
            // 
            this.lbltype.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbltype.Location = new System.Drawing.Point(18, 12);
            this.lbltype.Name = "lbltype";
            this.lbltype.Size = new System.Drawing.Size(70, 20);
            this.lbltype.TabIndex = 24;
            this.lbltype.Text = "点图层：";
            // 
            // cmbEarthField
            // 
            this.cmbEarthField.DisplayMember = "Text";
            this.cmbEarthField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEarthField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEarthField.FormattingEnabled = true;
            this.cmbEarthField.ItemHeight = 17;
            this.cmbEarthField.Location = new System.Drawing.Point(117, 98);
            this.cmbEarthField.Name = "cmbEarthField";
            this.cmbEarthField.Size = new System.Drawing.Size(263, 23);
            this.cmbEarthField.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbEarthField.TabIndex = 29;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 101);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(93, 20);
            this.labelX1.TabIndex = 30;
            this.labelX1.Text = "地球矢量字段：";
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "openFileDialog1";
            // 
            // txtOutFile
            // 
            this.txtOutFile.Location = new System.Drawing.Point(117, 151);
            this.txtOutFile.Name = "txtOutFile";
            this.txtOutFile.Size = new System.Drawing.Size(191, 21);
            this.txtOutFile.TabIndex = 32;
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(236, 188);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(62, 26);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 33;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.Location = new System.Drawing.Point(318, 188);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(62, 26);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 34;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelectFile.Location = new System.Drawing.Point(318, 151);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(62, 23);
            this.btnSelectFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelectFile.TabIndex = 35;
            this.btnSelectFile.Text = "打开";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // FrmOritationVector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(388, 216);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtOutFile);
            this.Controls.Add(this.cmbEarthField);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cmbSunField);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cmbPointLayer);
            this.Controls.Add(this.lbltype);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOritationVector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "太阳地球矢量生成";
            this.Load += new System.EventHandler(this.FrmOritationVector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSunField;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPointLayer;
        private DevComponents.DotNetBar.LabelX lbltype;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbEarthField;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private System.Windows.Forms.TextBox txtOutFile;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.DotNetBar.ButtonX btnSelectFile;
    }
}