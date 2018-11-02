namespace LibCerMap
{
    partial class FrmProjectBackward
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
            this.lblDemFilename = new DevComponents.DotNetBar.LabelX();
            this.lblInputShpFilename = new DevComponents.DotNetBar.LabelX();
            this.txtXmlFilename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblXmlFilename = new DevComponents.DotNetBar.LabelX();
            this.btnXmlFilename = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.cmbPathFeatureLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblOutputPolyline = new DevComponents.DotNetBar.LabelX();
            this.txtOutputPolyline = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnOutputPolyline = new DevComponents.DotNetBar.ButtonX();
            this.lblOutputPoint = new DevComponents.DotNetBar.LabelX();
            this.txtOutputPoint = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnOutputPoint = new DevComponents.DotNetBar.ButtonX();
            this.cmbDemLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // lblDemFilename
            // 
            // 
            // 
            // 
            this.lblDemFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDemFilename.Location = new System.Drawing.Point(15, 52);
            this.lblDemFilename.Name = "lblDemFilename";
            this.lblDemFilename.Size = new System.Drawing.Size(61, 23);
            this.lblDemFilename.TabIndex = 0;
            this.lblDemFilename.Text = "DEM文件：";
            // 
            // lblInputShpFilename
            // 
            // 
            // 
            // 
            this.lblInputShpFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblInputShpFilename.Location = new System.Drawing.Point(15, 10);
            this.lblInputShpFilename.Name = "lblInputShpFilename";
            this.lblInputShpFilename.Size = new System.Drawing.Size(61, 23);
            this.lblInputShpFilename.TabIndex = 2;
            this.lblInputShpFilename.Text = "路径图层:";
            // 
            // txtXmlFilename
            // 
            // 
            // 
            // 
            this.txtXmlFilename.Border.Class = "TextBoxBorder";
            this.txtXmlFilename.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtXmlFilename.Location = new System.Drawing.Point(101, 94);
            this.txtXmlFilename.Name = "txtXmlFilename";
            this.txtXmlFilename.Size = new System.Drawing.Size(297, 21);
            this.txtXmlFilename.TabIndex = 5;
            // 
            // lblXmlFilename
            // 
            // 
            // 
            // 
            this.lblXmlFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblXmlFilename.Location = new System.Drawing.Point(15, 94);
            this.lblXmlFilename.Name = "lblXmlFilename";
            this.lblXmlFilename.Size = new System.Drawing.Size(69, 23);
            this.lblXmlFilename.TabIndex = 4;
            this.lblXmlFilename.Text = "相机姿态：";
            // 
            // btnXmlFilename
            // 
            this.btnXmlFilename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXmlFilename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXmlFilename.Location = new System.Drawing.Point(423, 94);
            this.btnXmlFilename.Name = "btnXmlFilename";
            this.btnXmlFilename.Size = new System.Drawing.Size(50, 23);
            this.btnXmlFilename.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXmlFilename.TabIndex = 6;
            this.btnXmlFilename.Text = "...";
            this.btnXmlFilename.Click += new System.EventHandler(this.btnXmlFilename_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(308, 227);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定 ";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(398, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            // 
            // cmbPathFeatureLayer
            // 
            this.cmbPathFeatureLayer.DisplayMember = "Text";
            this.cmbPathFeatureLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPathFeatureLayer.FormattingEnabled = true;
            this.cmbPathFeatureLayer.ItemHeight = 15;
            this.cmbPathFeatureLayer.Location = new System.Drawing.Point(101, 10);
            this.cmbPathFeatureLayer.Name = "cmbPathFeatureLayer";
            this.cmbPathFeatureLayer.Size = new System.Drawing.Size(372, 21);
            this.cmbPathFeatureLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPathFeatureLayer.TabIndex = 8;
            this.cmbPathFeatureLayer.SelectedIndexChanged += new System.EventHandler(this.cmbPathFeatureLayer_SelectedIndexChanged);
            // 
            // lblOutputPolyline
            // 
            // 
            // 
            // 
            this.lblOutputPolyline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblOutputPolyline.Location = new System.Drawing.Point(15, 136);
            this.lblOutputPolyline.Name = "lblOutputPolyline";
            this.lblOutputPolyline.Size = new System.Drawing.Size(80, 23);
            this.lblOutputPolyline.TabIndex = 4;
            this.lblOutputPolyline.Text = "输出线文件：";
            // 
            // txtOutputPolyline
            // 
            // 
            // 
            // 
            this.txtOutputPolyline.Border.Class = "TextBoxBorder";
            this.txtOutputPolyline.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutputPolyline.Location = new System.Drawing.Point(101, 136);
            this.txtOutputPolyline.Name = "txtOutputPolyline";
            this.txtOutputPolyline.Size = new System.Drawing.Size(297, 21);
            this.txtOutputPolyline.TabIndex = 5;
            // 
            // btnOutputPolyline
            // 
            this.btnOutputPolyline.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutputPolyline.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutputPolyline.Location = new System.Drawing.Point(423, 136);
            this.btnOutputPolyline.Name = "btnOutputPolyline";
            this.btnOutputPolyline.Size = new System.Drawing.Size(50, 23);
            this.btnOutputPolyline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOutputPolyline.TabIndex = 6;
            this.btnOutputPolyline.Text = "...";
            this.btnOutputPolyline.Click += new System.EventHandler(this.btnBtnTxtPolyline_Click);
            // 
            // lblOutputPoint
            // 
            // 
            // 
            // 
            this.lblOutputPoint.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblOutputPoint.Location = new System.Drawing.Point(15, 178);
            this.lblOutputPoint.Name = "lblOutputPoint";
            this.lblOutputPoint.Size = new System.Drawing.Size(80, 23);
            this.lblOutputPoint.TabIndex = 4;
            this.lblOutputPoint.Text = "输出点文件：";
            // 
            // txtOutputPoint
            // 
            // 
            // 
            // 
            this.txtOutputPoint.Border.Class = "TextBoxBorder";
            this.txtOutputPoint.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutputPoint.Location = new System.Drawing.Point(101, 178);
            this.txtOutputPoint.Name = "txtOutputPoint";
            this.txtOutputPoint.Size = new System.Drawing.Size(297, 21);
            this.txtOutputPoint.TabIndex = 5;
            // 
            // btnOutputPoint
            // 
            this.btnOutputPoint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutputPoint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutputPoint.Location = new System.Drawing.Point(423, 178);
            this.btnOutputPoint.Name = "btnOutputPoint";
            this.btnOutputPoint.Size = new System.Drawing.Size(50, 23);
            this.btnOutputPoint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOutputPoint.TabIndex = 6;
            this.btnOutputPoint.Text = "...";
            this.btnOutputPoint.Click += new System.EventHandler(this.btnBtnTxtPoint_Click);
            // 
            // cmbDemLayer
            // 
            this.cmbDemLayer.DisplayMember = "Text";
            this.cmbDemLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDemLayer.FormattingEnabled = true;
            this.cmbDemLayer.ItemHeight = 15;
            this.cmbDemLayer.Location = new System.Drawing.Point(101, 52);
            this.cmbDemLayer.Name = "cmbDemLayer";
            this.cmbDemLayer.Size = new System.Drawing.Size(369, 21);
            this.cmbDemLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDemLayer.TabIndex = 9;
            this.cmbDemLayer.SelectedIndexChanged += new System.EventHandler(this.cmbDemLayer_SelectedIndexChanged);
            // 
            // FrmProjectBackward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 262);
            this.Controls.Add(this.cmbPathFeatureLayer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOutputPoint);
            this.Controls.Add(this.btnOutputPolyline);
            this.Controls.Add(this.btnXmlFilename);
            this.Controls.Add(this.txtOutputPoint);
            this.Controls.Add(this.txtOutputPolyline);
            this.Controls.Add(this.lblOutputPoint);
            this.Controls.Add(this.lblOutputPolyline);
            this.Controls.Add(this.txtXmlFilename);
            this.Controls.Add(this.lblXmlFilename);
            this.Controls.Add(this.lblInputShpFilename);
            this.Controls.Add(this.lblDemFilename);
            this.Controls.Add(this.cmbDemLayer);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProjectBackward";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "行驶路线反投影";
            this.Load += new System.EventHandler(this.FrmProjectBackward_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblDemFilename;
        private DevComponents.DotNetBar.LabelX lblInputShpFilename;
        private DevComponents.DotNetBar.Controls.TextBoxX txtXmlFilename;
        private DevComponents.DotNetBar.LabelX lblXmlFilename;
        private DevComponents.DotNetBar.ButtonX btnXmlFilename;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPathFeatureLayer;
        private DevComponents.DotNetBar.LabelX lblOutputPolyline;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutputPolyline;
        private DevComponents.DotNetBar.ButtonX btnOutputPolyline;
        private DevComponents.DotNetBar.LabelX lblOutputPoint;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutputPoint;
        private DevComponents.DotNetBar.ButtonX btnOutputPoint;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDemLayer;
    }
}