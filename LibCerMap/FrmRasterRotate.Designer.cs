namespace LibCerMap
{
    partial class FrmRasterRotate
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
            this.buttonX_Cancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_OK = new DevComponents.DotNetBar.ButtonX();
            this.textRotateY = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.textRotateX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textRotate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // buttonX_Cancel
            // 
            this.buttonX_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_Cancel.Location = new System.Drawing.Point(174, 167);
            this.buttonX_Cancel.Name = "buttonX_Cancel";
            this.buttonX_Cancel.Size = new System.Drawing.Size(75, 23);
            this.buttonX_Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_Cancel.TabIndex = 11;
            this.buttonX_Cancel.Text = "取消";
            this.buttonX_Cancel.Click += new System.EventHandler(this.buttonX_Cancel_Click);
            // 
            // buttonX_OK
            // 
            this.buttonX_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonX_OK.Location = new System.Drawing.Point(58, 167);
            this.buttonX_OK.Name = "buttonX_OK";
            this.buttonX_OK.Size = new System.Drawing.Size(75, 23);
            this.buttonX_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_OK.TabIndex = 10;
            this.buttonX_OK.Text = "确定";
            this.buttonX_OK.Click += new System.EventHandler(this.buttonX_OK_Click);
            // 
            // textRotateY
            // 
            // 
            // 
            // 
            this.textRotateY.Border.Class = "TextBoxBorder";
            this.textRotateY.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textRotateY.Location = new System.Drawing.Point(119, 62);
            this.textRotateY.Name = "textRotateY";
            this.textRotateY.Size = new System.Drawing.Size(140, 21);
            this.textRotateY.TabIndex = 8;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(25, 61);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(105, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "旋转中心Y坐标:";
            // 
            // textRotateX
            // 
            // 
            // 
            // 
            this.textRotateX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textRotateX.Location = new System.Drawing.Point(119, 25);
            this.textRotateX.Name = "textRotateX";
            this.textRotateX.Size = new System.Drawing.Size(140, 15);
            this.textRotateX.TabIndex = 6;
            this.textRotateX.TextChanged += new System.EventHandler(this.textRotateX_TextChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(25, 25);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(105, 23);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "旋转中心X坐标:";
            // 
            // textRotate
            // 
            // 
            // 
            // 
            this.textRotate.Border.Class = "TextBoxBorder";
            this.textRotate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textRotate.Location = new System.Drawing.Point(119, 99);
            this.textRotate.Name = "textRotate";
            this.textRotate.Size = new System.Drawing.Size(140, 21);
            this.textRotate.TabIndex = 15;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(57, 97);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(105, 23);
            this.labelX3.TabIndex = 16;
            this.labelX3.Text = "旋转角度:";
            // 
            // FrmRasterRotate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 215);
            this.Controls.Add(this.textRotate);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.buttonX_Cancel);
            this.Controls.Add(this.buttonX_OK);
            this.Controls.Add(this.textRotateY);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.textRotateX);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRasterRotate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "栅格旋转";
            this.Load += new System.EventHandler(this.FrmRasterRotate_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX_Cancel;
        private DevComponents.DotNetBar.ButtonX buttonX_OK;
        private DevComponents.DotNetBar.Controls.TextBoxX textRotateY;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX textRotateX;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textRotate;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}