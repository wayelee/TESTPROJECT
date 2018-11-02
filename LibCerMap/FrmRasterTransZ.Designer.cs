namespace LibCerMap
{
    partial class FrmRasterTransZ
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
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX_Cancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_OK = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(79, 23);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(47, 21);
            this.textBoxX1.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(54, 23);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(23, 23);
            this.labelX1.TabIndex = 8;
            this.labelX1.Text = "Z\'=";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(126, 23);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(32, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = " Z + ";
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.Location = new System.Drawing.Point(159, 24);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(47, 21);
            this.textBoxX2.TabIndex = 10;
            // 
            // buttonX_Cancel
            // 
            this.buttonX_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_Cancel.Location = new System.Drawing.Point(151, 176);
            this.buttonX_Cancel.Name = "buttonX_Cancel";
            this.buttonX_Cancel.Size = new System.Drawing.Size(75, 23);
            this.buttonX_Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_Cancel.TabIndex = 13;
            this.buttonX_Cancel.Text = "取消";
            this.buttonX_Cancel.Click += new System.EventHandler(this.buttonX_Cancel_Click);
            // 
            // buttonX_OK
            // 
            this.buttonX_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonX_OK.Location = new System.Drawing.Point(54, 176);
            this.buttonX_OK.Name = "buttonX_OK";
            this.buttonX_OK.Size = new System.Drawing.Size(75, 23);
            this.buttonX_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_OK.TabIndex = 12;
            this.buttonX_OK.Text = "确定";
            this.buttonX_OK.Click += new System.EventHandler(this.buttonX_OK_Click);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(54, 75);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(152, 23);
            this.labelX3.TabIndex = 14;
            this.labelX3.Text = "Z值不变：Z\' = 1 * Z + 0";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(54, 104);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(162, 23);
            this.labelX4.TabIndex = 15;
            this.labelX4.Text = "Z值翻转：Z\' = -1 * Z + 0";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(54, 132);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(152, 23);
            this.labelX5.TabIndex = 16;
            this.labelX5.Text = "Z值变换：Z\' = a * Z + b";
            // 
            // FrmRasterTransZ
            // 
            this.AcceptButton = this.buttonX_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 227);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.buttonX_Cancel);
            this.Controls.Add(this.buttonX_OK);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRasterTransZ";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "栅格值变换";
            this.Load += new System.EventHandler(this.FrmRasterTransZ_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.ButtonX buttonX_Cancel;
        private DevComponents.DotNetBar.ButtonX buttonX_OK;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}