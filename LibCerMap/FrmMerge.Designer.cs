namespace LibCerMap
{
    partial class FrmMerge
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
            this.cmbTINLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbCraterLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbNonCraterLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblTIN = new DevComponents.DotNetBar.LabelX();
            this.lblCrater = new DevComponents.DotNetBar.LabelX();
            this.lblNonCrater = new DevComponents.DotNetBar.LabelX();
            this.textBoxXPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonPath = new DevComponents.DotNetBar.ButtonX();
            this.lblPath = new DevComponents.DotNetBar.LabelX();
            this.buttonOK = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // cmbTINLayer
            // 
            this.cmbTINLayer.DisplayMember = "Text";
            this.cmbTINLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTINLayer.FormattingEnabled = true;
            this.cmbTINLayer.ItemHeight = 15;
            this.cmbTINLayer.Location = new System.Drawing.Point(136, 12);
            this.cmbTINLayer.Name = "cmbTINLayer";
            this.cmbTINLayer.Size = new System.Drawing.Size(121, 21);
            this.cmbTINLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTINLayer.TabIndex = 0;
            // 
            // cmbCraterLayer
            // 
            this.cmbCraterLayer.DisplayMember = "Text";
            this.cmbCraterLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCraterLayer.FormattingEnabled = true;
            this.cmbCraterLayer.ItemHeight = 15;
            this.cmbCraterLayer.Location = new System.Drawing.Point(136, 68);
            this.cmbCraterLayer.Name = "cmbCraterLayer";
            this.cmbCraterLayer.Size = new System.Drawing.Size(121, 21);
            this.cmbCraterLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbCraterLayer.TabIndex = 0;
            // 
            // cmbNonCraterLayer
            // 
            this.cmbNonCraterLayer.DisplayMember = "Text";
            this.cmbNonCraterLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNonCraterLayer.FormattingEnabled = true;
            this.cmbNonCraterLayer.ItemHeight = 15;
            this.cmbNonCraterLayer.Location = new System.Drawing.Point(363, 66);
            this.cmbNonCraterLayer.Name = "cmbNonCraterLayer";
            this.cmbNonCraterLayer.Size = new System.Drawing.Size(129, 21);
            this.cmbNonCraterLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbNonCraterLayer.TabIndex = 0;
            // 
            // lblTIN
            // 
            // 
            // 
            // 
            this.lblTIN.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTIN.Location = new System.Drawing.Point(44, 12);
            this.lblTIN.Name = "lblTIN";
            this.lblTIN.Size = new System.Drawing.Size(75, 23);
            this.lblTIN.TabIndex = 1;
            this.lblTIN.Text = "TIN图层";
            // 
            // lblCrater
            // 
            // 
            // 
            // 
            this.lblCrater.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCrater.Location = new System.Drawing.Point(44, 66);
            this.lblCrater.Name = "lblCrater";
            this.lblCrater.Size = new System.Drawing.Size(75, 23);
            this.lblCrater.TabIndex = 1;
            this.lblCrater.Text = "撞击坑图层";
            // 
            // lblNonCrater
            // 
            // 
            // 
            // 
            this.lblNonCrater.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblNonCrater.Location = new System.Drawing.Point(263, 66);
            this.lblNonCrater.Name = "lblNonCrater";
            this.lblNonCrater.Size = new System.Drawing.Size(92, 23);
            this.lblNonCrater.TabIndex = 1;
            this.lblNonCrater.Text = "非撞击坑图层";
            // 
            // textBoxXPath
            // 
            // 
            // 
            // 
            this.textBoxXPath.Border.Class = "TextBoxBorder";
            this.textBoxXPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXPath.Location = new System.Drawing.Point(44, 145);
            this.textBoxXPath.Name = "textBoxXPath";
            this.textBoxXPath.Size = new System.Drawing.Size(378, 21);
            this.textBoxXPath.TabIndex = 2;
            // 
            // buttonPath
            // 
            this.buttonPath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonPath.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonPath.Location = new System.Drawing.Point(453, 143);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(30, 23);
            this.buttonPath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonPath.TabIndex = 3;
            this.buttonPath.Text = "...";
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            // 
            // lblPath
            // 
            // 
            // 
            // 
            this.lblPath.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPath.Location = new System.Drawing.Point(44, 116);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(75, 23);
            this.lblPath.TabIndex = 1;
            this.lblPath.Text = "新地形路径";
            // 
            // buttonOK
            // 
            this.buttonOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(430, 189);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(62, 23);
            this.buttonOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "确定";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FrmMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 224);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonPath);
            this.Controls.Add(this.textBoxXPath);
            this.Controls.Add(this.lblNonCrater);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.lblCrater);
            this.Controls.Add(this.lblTIN);
            this.Controls.Add(this.cmbNonCraterLayer);
            this.Controls.Add(this.cmbCraterLayer);
            this.Controls.Add(this.cmbTINLayer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMerge";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地形融合";
            this.Load += new System.EventHandler(this.FrmMerge_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTINLayer;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbCraterLayer;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbNonCraterLayer;
        private DevComponents.DotNetBar.LabelX lblTIN;
        private DevComponents.DotNetBar.LabelX lblCrater;
        private DevComponents.DotNetBar.LabelX lblNonCrater;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXPath;
        private DevComponents.DotNetBar.ButtonX buttonPath;
        private DevComponents.DotNetBar.LabelX lblPath;
        private DevComponents.DotNetBar.ButtonX buttonOK;
    }
}