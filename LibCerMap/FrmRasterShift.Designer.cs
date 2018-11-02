namespace LibCerMap
{
    partial class FrmRasterShift
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
            this.textshiftX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textShiftY = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.buttonX_OK = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_Cancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // textshiftX
            // 
            // 
            // 
            // 
            this.textshiftX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textshiftX.Location = new System.Drawing.Point(107, 12);
            this.textshiftX.Name = "textshiftX";
            this.textshiftX.Size = new System.Drawing.Size(100, 21);
            this.textshiftX.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(105, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "X方向平移距离:";
            // 
            // textShiftY
            // 
            // 
            // 
            // 
            this.textShiftY.Border.Class = "TextBoxBorder";
            this.textShiftY.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textShiftY.Location = new System.Drawing.Point(107, 41);
            this.textShiftY.Name = "textShiftY";
            this.textShiftY.Size = new System.Drawing.Size(100, 21);
            this.textShiftY.TabIndex = 2;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(13, 41);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(105, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "Y方向平移距离:";
            // 
            // buttonX_OK
            // 
            this.buttonX_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonX_OK.Location = new System.Drawing.Point(35, 80);
            this.buttonX_OK.Name = "buttonX_OK";
            this.buttonX_OK.Size = new System.Drawing.Size(75, 23);
            this.buttonX_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_OK.TabIndex = 4;
            this.buttonX_OK.Text = "确定";
            this.buttonX_OK.Click += new System.EventHandler(this.buttonX_OK_Click);
            // 
            // buttonX_Cancel
            // 
            this.buttonX_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_Cancel.Location = new System.Drawing.Point(132, 80);
            this.buttonX_Cancel.Name = "buttonX_Cancel";
            this.buttonX_Cancel.Size = new System.Drawing.Size(75, 23);
            this.buttonX_Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_Cancel.TabIndex = 5;
            this.buttonX_Cancel.Text = "取消";
            this.buttonX_Cancel.Click += new System.EventHandler(this.buttonX_Cancel_Click);
            // 
            // FrmRasterShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 120);
            this.Controls.Add(this.buttonX_Cancel);
            this.Controls.Add(this.buttonX_OK);
            this.Controls.Add(this.textShiftY);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.textshiftX);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRasterShift";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "栅格平移";
            this.Load += new System.EventHandler(this.FrmRasterShift_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX textshiftX;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textShiftY;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX buttonX_OK;
        private DevComponents.DotNetBar.ButtonX buttonX_Cancel;
    }
}