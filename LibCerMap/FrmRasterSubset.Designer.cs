namespace LibCerMap
{
    partial class FrmRasterSubset
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
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.lbltype = new DevComponents.DotNetBar.LabelX();
            this.cmbSource = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmbRegion = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnOutput = new DevComponents.DotNetBar.ButtonX();
            this.txtTarget = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(523, 21);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(31, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 7;
            this.buttonX1.Text = "....";
            this.buttonX1.Visible = false;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // lbltype
            // 
            this.lbltype.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbltype.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbltype.Location = new System.Drawing.Point(10, 24);
            this.lbltype.Name = "lbltype";
            this.lbltype.Size = new System.Drawing.Size(71, 20);
            this.lbltype.TabIndex = 6;
            this.lbltype.Text = "栅格图层：";
            // 
            // cmbSource
            // 
            this.cmbSource.DisplayMember = "Text";
            this.cmbSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.ItemHeight = 17;
            this.cmbSource.Location = new System.Drawing.Point(87, 21);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(429, 23);
            this.cmbSource.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSource.TabIndex = 5;
            this.cmbSource.SelectedIndexChanged += new System.EventHandler(this.cmbSource_SelectedIndexChanged);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(523, 65);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(31, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 10;
            this.buttonX2.Text = "....";
            this.buttonX2.Visible = false;
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(10, 68);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(71, 20);
            this.labelX1.TabIndex = 9;
            this.labelX1.Text = "范围图层：";
            // 
            // cmbRegion
            // 
            this.cmbRegion.DisplayMember = "Text";
            this.cmbRegion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegion.FormattingEnabled = true;
            this.cmbRegion.ItemHeight = 17;
            this.cmbRegion.Location = new System.Drawing.Point(87, 65);
            this.cmbRegion.Name = "cmbRegion";
            this.cmbRegion.Size = new System.Drawing.Size(429, 23);
            this.cmbRegion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbRegion.TabIndex = 8;
            // 
            // btnOutput
            // 
            this.btnOutput.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutput.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutput.Location = new System.Drawing.Point(485, 111);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(31, 23);
            this.btnOutput.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOutput.TabIndex = 13;
            this.btnOutput.Text = "....";
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // txtTarget
            // 
            // 
            // 
            // 
            this.txtTarget.Border.Class = "TextBoxBorder";
            this.txtTarget.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTarget.Location = new System.Drawing.Point(87, 114);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(383, 21);
            this.txtTarget.TabIndex = 12;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(10, 114);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(71, 20);
            this.labelX2.TabIndex = 11;
            this.labelX2.Text = "输出图层：";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(410, 193);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 34);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Location = new System.Drawing.Point(278, 193);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 34);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "应用";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.lbltype);
            this.groupPanel1.Controls.Add(this.cmbSource);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.buttonX1);
            this.groupPanel1.Controls.Add(this.txtTarget);
            this.groupPanel1.Controls.Add(this.buttonX2);
            this.groupPanel1.Controls.Add(this.btnOutput);
            this.groupPanel1.Controls.Add(this.cmbRegion);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(541, 173);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 16;
            // 
            // FrmRasterSubset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 252);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRasterSubset";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "栅格裁切";
            this.Load += new System.EventHandler(this.FrmRasterSubset_Load);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX lbltype;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSource;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbRegion;
        private DevComponents.DotNetBar.ButtonX btnOutput;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTarget;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;

    }
}