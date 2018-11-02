namespace LibCerMap
{
    partial class FrmFiledCalculator
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
            this.lsbFields = new System.Windows.Forms.ListBox();
            this.lsbFunction = new System.Windows.Forms.ListBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.BtnMultply = new DevComponents.DotNetBar.ButtonX();
            this.btn = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cmbCalField = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.RtbCal = new System.Windows.Forms.RichTextBox();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(129, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "选择用于计算的字段：";
            // 
            // lsbFields
            // 
            this.lsbFields.ItemHeight = 12;
            this.lsbFields.Location = new System.Drawing.Point(12, 42);
            this.lsbFields.Name = "lsbFields";
            this.lsbFields.Size = new System.Drawing.Size(170, 196);
            this.lsbFields.TabIndex = 3;
            this.lsbFields.DoubleClick += new System.EventHandler(this.lsbFields_DoubleClick);
            // 
            // lsbFunction
            // 
            this.lsbFunction.ItemHeight = 12;
            this.lsbFunction.Items.AddRange(new object[] {
            "Abs( )",
            "Atn( )",
            "Cos( )",
            "Exp( )",
            "Fix( )",
            "Int( )",
            "Log( )",
            "Sin( )",
            "Sqr( )",
            "Tan( )"});
            this.lsbFunction.Location = new System.Drawing.Point(204, 42);
            this.lsbFunction.Name = "lsbFunction";
            this.lsbFunction.Size = new System.Drawing.Size(170, 160);
            this.lsbFunction.TabIndex = 4;
            this.lsbFunction.DoubleClick += new System.EventHandler(this.lsbFunction_DoubleClick);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(204, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(129, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "计算函数：";
            // 
            // BtnMultply
            // 
            this.BtnMultply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnMultply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnMultply.Location = new System.Drawing.Point(204, 215);
            this.BtnMultply.Name = "BtnMultply";
            this.BtnMultply.Size = new System.Drawing.Size(34, 23);
            this.BtnMultply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnMultply.TabIndex = 6;
            this.BtnMultply.Text = "+";
            this.BtnMultply.Click += new System.EventHandler(this.BtnMultply_Click);
            // 
            // btn
            // 
            this.btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn.Location = new System.Drawing.Point(249, 215);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(34, 23);
            this.btn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn.TabIndex = 7;
            this.btn.Text = "-";
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(295, 215);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(34, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 8;
            this.buttonX2.Text = "*";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(340, 215);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(34, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 9;
            this.buttonX3.Text = "/";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 244);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(129, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "选择计算的目标字段：";
            // 
            // cmbCalField
            // 
            this.cmbCalField.DisplayMember = "Text";
            this.cmbCalField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCalField.FormattingEnabled = true;
            this.cmbCalField.ItemHeight = 15;
            this.cmbCalField.Location = new System.Drawing.Point(13, 263);
            this.cmbCalField.Name = "cmbCalField";
            this.cmbCalField.Size = new System.Drawing.Size(129, 21);
            this.cmbCalField.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbCalField.TabIndex = 11;
            // 
            // RtbCal
            // 
            this.RtbCal.Location = new System.Drawing.Point(13, 290);
            this.RtbCal.Name = "RtbCal";
            this.RtbCal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RtbCal.Size = new System.Drawing.Size(361, 112);
            this.RtbCal.TabIndex = 19;
            this.RtbCal.Text = "";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(148, 261);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(18, 23);
            this.labelX4.TabIndex = 20;
            this.labelX4.Text = "=";
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(304, 419);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(70, 25);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 24;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(189, 419);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(70, 25);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 22;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // FrmFiledCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 456);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.RtbCal);
            this.Controls.Add(this.cmbCalField);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.BtnMultply);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.lsbFunction);
            this.Controls.Add(this.lsbFields);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFiledCalculator";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "字段计算";
            this.Load += new System.EventHandler(this.FrmFiledCalculator_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.ListBox lsbFields;
        private System.Windows.Forms.ListBox lsbFunction;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX BtnMultply;
        private DevComponents.DotNetBar.ButtonX btn;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbCalField;
        private System.Windows.Forms.RichTextBox RtbCal;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.ButtonX btnok;
    }
}