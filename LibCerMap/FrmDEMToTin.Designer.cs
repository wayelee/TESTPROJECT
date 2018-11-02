namespace LibCerMap
{
    partial class FrmDEMToTin
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
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.textBoxXDEM = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxXTIN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonXDEM = new DevComponents.DotNetBar.ButtonX();
            this.buttonXTIN = new DevComponents.DotNetBar.ButtonX();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.cmbLayers = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(24, 27);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "DEM路径：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(24, 107);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "TIN路径：";
            // 
            // textBoxXDEM
            // 
            // 
            // 
            // 
            this.textBoxXDEM.Border.Class = "TextBoxBorder";
            this.textBoxXDEM.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXDEM.Location = new System.Drawing.Point(105, 27);
            this.textBoxXDEM.Name = "textBoxXDEM";
            this.textBoxXDEM.Size = new System.Drawing.Size(274, 21);
            this.textBoxXDEM.TabIndex = 2;
            this.textBoxXDEM.TextChanged += new System.EventHandler(this.textBoxXDEM_TextChanged);
            // 
            // textBoxXTIN
            // 
            // 
            // 
            // 
            this.textBoxXTIN.Border.Class = "TextBoxBorder";
            this.textBoxXTIN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXTIN.Location = new System.Drawing.Point(105, 107);
            this.textBoxXTIN.Name = "textBoxXTIN";
            this.textBoxXTIN.Size = new System.Drawing.Size(274, 21);
            this.textBoxXTIN.TabIndex = 2;
            this.textBoxXTIN.TextChanged += new System.EventHandler(this.textBoxXTIN_TextChanged);
            // 
            // buttonXDEM
            // 
            this.buttonXDEM.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDEM.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDEM.Location = new System.Drawing.Point(404, 24);
            this.buttonXDEM.Name = "buttonXDEM";
            this.buttonXDEM.Size = new System.Drawing.Size(39, 23);
            this.buttonXDEM.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXDEM.TabIndex = 3;
            this.buttonXDEM.Text = "...";
            this.buttonXDEM.Click += new System.EventHandler(this.buttonXDEM_Click);
            // 
            // buttonXTIN
            // 
            this.buttonXTIN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXTIN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXTIN.Location = new System.Drawing.Point(404, 105);
            this.buttonXTIN.Name = "buttonXTIN";
            this.buttonXTIN.Size = new System.Drawing.Size(39, 23);
            this.buttonXTIN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXTIN.TabIndex = 3;
            this.buttonXTIN.Text = "...";
            this.buttonXTIN.Click += new System.EventHandler(this.buttonXTIN_Click);
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(259, 157);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(75, 23);
            this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXOK.TabIndex = 4;
            this.buttonXOK.Text = "确定";
            this.buttonXOK.Click += new System.EventHandler(this.buttonXOK_Click);
            // 
            // cmbLayers
            // 
            this.cmbLayers.DisplayMember = "Text";
            this.cmbLayers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayers.FormattingEnabled = true;
            this.cmbLayers.ItemHeight = 15;
            this.cmbLayers.Location = new System.Drawing.Point(105, 69);
            this.cmbLayers.Name = "cmbLayers";
            this.cmbLayers.Size = new System.Drawing.Size(274, 21);
            this.cmbLayers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLayers.TabIndex = 5;
            this.cmbLayers.SelectedIndexChanged += new System.EventHandler(this.cmbLayers_SelectedIndexChanged);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(24, 69);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(87, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "从图层添加：";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(365, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmDEMToTin
            // 
            this.AcceptButton = this.buttonXOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 198);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbLayers);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.buttonXTIN);
            this.Controls.Add(this.buttonXDEM);
            this.Controls.Add(this.textBoxXTIN);
            this.Controls.Add(this.textBoxXDEM);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDEMToTin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DEM转TIN";
            this.Load += new System.EventHandler(this.FrmDEMToTin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXDEM;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXTIN;
        private DevComponents.DotNetBar.ButtonX buttonXDEM;
        private DevComponents.DotNetBar.ButtonX buttonXTIN;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLayers;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}