namespace LibCerMap
{
    partial class FrmRasterResample
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
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmbTargetRasterLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // buttonX_Cancel
            // 
            this.buttonX_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_Cancel.Location = new System.Drawing.Point(151, 99);
            this.buttonX_Cancel.Name = "buttonX_Cancel";
            this.buttonX_Cancel.Size = new System.Drawing.Size(75, 23);
            this.buttonX_Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_Cancel.TabIndex = 17;
            this.buttonX_Cancel.Text = "取消";
            this.buttonX_Cancel.Click += new System.EventHandler(this.buttonX_Cancel_Click);
            // 
            // buttonX_OK
            // 
            this.buttonX_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonX_OK.Location = new System.Drawing.Point(54, 99);
            this.buttonX_OK.Name = "buttonX_OK";
            this.buttonX_OK.Size = new System.Drawing.Size(75, 23);
            this.buttonX_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_OK.TabIndex = 16;
            this.buttonX_OK.Text = "确定";
            this.buttonX_OK.Click += new System.EventHandler(this.buttonX_OK_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 55);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(105, 23);
            this.labelX2.TabIndex = 15;
            this.labelX2.Text = "重采样方式:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(24, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(105, 23);
            this.labelX1.TabIndex = 13;
            this.labelX1.Text = "像元大小:";
            // 
            // cmbTargetRasterLayer
            // 
            this.cmbTargetRasterLayer.DisplayMember = "Text";
            this.cmbTargetRasterLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTargetRasterLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetRasterLayer.FormattingEnabled = true;
            this.cmbTargetRasterLayer.ItemHeight = 17;
            this.cmbTargetRasterLayer.Location = new System.Drawing.Point(86, 53);
            this.cmbTargetRasterLayer.Name = "cmbTargetRasterLayer";
            this.cmbTargetRasterLayer.Size = new System.Drawing.Size(148, 23);
            this.cmbTargetRasterLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTargetRasterLayer.TabIndex = 18;
            this.cmbTargetRasterLayer.SelectedIndexChanged += new System.EventHandler(this.cmbTargetRasterLayer_SelectedIndexChanged);
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(86, 16);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(148, 21);
            this.textBoxX1.TabIndex = 19;
            // 
            // FrmRasterResample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 147);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.cmbTargetRasterLayer);
            this.Controls.Add(this.buttonX_Cancel);
            this.Controls.Add(this.buttonX_OK);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRasterResample";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "栅格图像重采样";
            this.Load += new System.EventHandler(this.FrmRasterResample_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX_Cancel;
        private DevComponents.DotNetBar.ButtonX buttonX_OK;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTargetRasterLayer;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
    }
}