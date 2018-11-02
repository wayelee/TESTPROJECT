namespace LibCerMap
{
    partial class FrmAddModelToTerrain
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
            this.lblXmlFilename = new DevComponents.DotNetBar.LabelX();
            this.lblRaster = new DevComponents.DotNetBar.LabelX();
            this.txtXmlFilename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnXmlFilename = new DevComponents.DotNetBar.ButtonX();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.cmbRaster = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // lblXmlFilename
            // 
            // 
            // 
            // 
            this.lblXmlFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblXmlFilename.Location = new System.Drawing.Point(37, 26);
            this.lblXmlFilename.Name = "lblXmlFilename";
            this.lblXmlFilename.Size = new System.Drawing.Size(75, 23);
            this.lblXmlFilename.TabIndex = 0;
            this.lblXmlFilename.Text = "Xml文件：";
            // 
            // lblRaster
            // 
            // 
            // 
            // 
            this.lblRaster.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRaster.Location = new System.Drawing.Point(37, 70);
            this.lblRaster.Name = "lblRaster";
            this.lblRaster.Size = new System.Drawing.Size(75, 23);
            this.lblRaster.TabIndex = 1;
            this.lblRaster.Text = "添加到图层:";
            // 
            // txtXmlFilename
            // 
            // 
            // 
            // 
            this.txtXmlFilename.Border.Class = "TextBoxBorder";
            this.txtXmlFilename.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtXmlFilename.Location = new System.Drawing.Point(119, 26);
            this.txtXmlFilename.Name = "txtXmlFilename";
            this.txtXmlFilename.Size = new System.Drawing.Size(204, 21);
            this.txtXmlFilename.TabIndex = 2;
            // 
            // btnXmlFilename
            // 
            this.btnXmlFilename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXmlFilename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXmlFilename.Location = new System.Drawing.Point(345, 24);
            this.btnXmlFilename.Name = "btnXmlFilename";
            this.btnXmlFilename.Size = new System.Drawing.Size(75, 23);
            this.btnXmlFilename.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXmlFilename.TabIndex = 4;
            this.btnXmlFilename.Text = "...";
            this.btnXmlFilename.Click += new System.EventHandler(this.btnXmlFilename_Click);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(244, 125);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确认";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbRaster
            // 
            this.cmbRaster.DisplayMember = "Text";
            this.cmbRaster.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRaster.FormattingEnabled = true;
            this.cmbRaster.ItemHeight = 15;
            this.cmbRaster.Location = new System.Drawing.Point(119, 72);
            this.cmbRaster.Name = "cmbRaster";
            this.cmbRaster.Size = new System.Drawing.Size(301, 21);
            this.cmbRaster.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbRaster.TabIndex = 6;
            this.cmbRaster.SelectedIndexChanged += new System.EventHandler(this.cmbRaster_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(345, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmAddModelToTerrain
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 176);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbRaster);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnXmlFilename);
            this.Controls.Add(this.txtXmlFilename);
            this.Controls.Add(this.lblRaster);
            this.Controls.Add(this.lblXmlFilename);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddModelToTerrain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加模型";
            this.Load += new System.EventHandler(this.FrmAddModelToTerrain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblXmlFilename;
        private DevComponents.DotNetBar.LabelX lblRaster;
        private DevComponents.DotNetBar.Controls.TextBoxX txtXmlFilename;
        private DevComponents.DotNetBar.ButtonX btnXmlFilename;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbRaster;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}