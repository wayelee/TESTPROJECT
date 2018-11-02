namespace LibCerMap
{
    partial class FrmCreateLabel
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
            this.buttonX_cancle = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_ok = new DevComponents.DotNetBar.ButtonX();
            this.lbltype = new DevComponents.DotNetBar.LabelX();
            this.cmbTargetRasterLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // buttonX_cancle
            // 
            this.buttonX_cancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_cancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_cancle.Location = new System.Drawing.Point(335, 227);
            this.buttonX_cancle.Name = "buttonX_cancle";
            this.buttonX_cancle.Size = new System.Drawing.Size(75, 23);
            this.buttonX_cancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_cancle.TabIndex = 14;
            this.buttonX_cancle.Text = "取消";
            this.buttonX_cancle.Click += new System.EventHandler(this.buttonX_cancle_Click);
            // 
            // buttonX_ok
            // 
            this.buttonX_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_ok.Location = new System.Drawing.Point(223, 227);
            this.buttonX_ok.Name = "buttonX_ok";
            this.buttonX_ok.Size = new System.Drawing.Size(75, 23);
            this.buttonX_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_ok.TabIndex = 13;
            this.buttonX_ok.Text = "确定";
            this.buttonX_ok.Click += new System.EventHandler(this.buttonX_ok_Click);
            // 
            // lbltype
            // 
            // 
            // 
            // 
            this.lbltype.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbltype.Location = new System.Drawing.Point(25, 25);
            this.lbltype.Name = "lbltype";
            this.lbltype.Size = new System.Drawing.Size(89, 20);
            this.lbltype.TabIndex = 12;
            this.lbltype.Text = "原始点图层：";
            // 
            // cmbTargetRasterLayer
            // 
            this.cmbTargetRasterLayer.DisplayMember = "Text";
            this.cmbTargetRasterLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTargetRasterLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetRasterLayer.FormattingEnabled = true;
            this.cmbTargetRasterLayer.ItemHeight = 17;
            this.cmbTargetRasterLayer.Location = new System.Drawing.Point(113, 25);
            this.cmbTargetRasterLayer.Name = "cmbTargetRasterLayer";
            this.cmbTargetRasterLayer.Size = new System.Drawing.Size(297, 23);
            this.cmbTargetRasterLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTargetRasterLayer.TabIndex = 11;
            this.cmbTargetRasterLayer.SelectedIndexChanged += new System.EventHandler(this.cmbTargetRasterLayer_SelectedIndexChanged);
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Enabled = false;
            this.textBoxX1.Location = new System.Drawing.Point(113, 146);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(136, 21);
            this.textBoxX1.TabIndex = 16;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(25, 145);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(89, 20);
            this.labelX1.TabIndex = 15;
            this.labelX1.Text = "输出线符号：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(25, 185);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(89, 20);
            this.labelX2.TabIndex = 17;
            this.labelX2.Text = "输出面符号：";
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.Enabled = false;
            this.textBoxX2.Location = new System.Drawing.Point(113, 185);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(297, 21);
            this.textBoxX2.TabIndex = 18;
            // 
            // textBoxX3
            // 
            // 
            // 
            // 
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX3.Location = new System.Drawing.Point(328, 146);
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.Size = new System.Drawing.Size(82, 21);
            this.textBoxX3.TabIndex = 20;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(255, 147);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(89, 20);
            this.labelX3.TabIndex = 19;
            this.labelX3.Text = "符号线长度：";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 17;
            this.comboBoxEx1.Location = new System.Drawing.Point(113, 66);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(297, 23);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 21;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(25, 66);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(104, 20);
            this.labelX4.TabIndex = 22;
            this.labelX4.Text = "原始DEM图层：";
            // 
            // textBoxX4
            // 
            // 
            // 
            // 
            this.textBoxX4.Border.Class = "TextBoxBorder";
            this.textBoxX4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX4.Location = new System.Drawing.Point(113, 107);
            this.textBoxX4.Name = "textBoxX4";
            this.textBoxX4.Size = new System.Drawing.Size(229, 21);
            this.textBoxX4.TabIndex = 24;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(25, 105);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(89, 20);
            this.labelX5.TabIndex = 23;
            this.labelX5.Text = "输入EXCEL表：";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(348, 107);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(62, 21);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 25;
            this.buttonX1.Text = "....";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // FrmCreateLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 270);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.textBoxX4);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.comboBoxEx1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.textBoxX3);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cmbTargetRasterLayer);
            this.Controls.Add(this.buttonX_cancle);
            this.Controls.Add(this.buttonX_ok);
            this.Controls.Add(this.lbltype);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCreateLabel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新标注图层";
            this.Load += new System.EventHandler(this.FrmCreateLabel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX_cancle;
        private DevComponents.DotNetBar.ButtonX buttonX_ok;
        private DevComponents.DotNetBar.LabelX lbltype;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTargetRasterLayer;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}