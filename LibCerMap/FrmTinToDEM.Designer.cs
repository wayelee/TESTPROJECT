namespace LibCerMap
{
    partial class FrmTinToDEM
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
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXTIN = new DevComponents.DotNetBar.ButtonX();
            this.buttonXDEM = new DevComponents.DotNetBar.ButtonX();
            this.textBoxXTIN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxXDEM = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.doubleInputCellSize = new DevComponents.Editors.DoubleInput();
            this.labelXCellSize = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cmbLayers = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputCellSize)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(253, 197);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(75, 23);
            this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXOK.TabIndex = 11;
            this.buttonXOK.Text = "确定";
            this.buttonXOK.Click += new System.EventHandler(this.buttonXOK_Click);
            // 
            // buttonXTIN
            // 
            this.buttonXTIN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXTIN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXTIN.Location = new System.Drawing.Point(379, 16);
            this.buttonXTIN.Name = "buttonXTIN";
            this.buttonXTIN.Size = new System.Drawing.Size(39, 23);
            this.buttonXTIN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXTIN.TabIndex = 9;
            this.buttonXTIN.Text = "...";
            this.buttonXTIN.Click += new System.EventHandler(this.buttonXTIN_Click);
            // 
            // buttonXDEM
            // 
            this.buttonXDEM.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDEM.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDEM.Location = new System.Drawing.Point(379, 94);
            this.buttonXDEM.Name = "buttonXDEM";
            this.buttonXDEM.Size = new System.Drawing.Size(39, 23);
            this.buttonXDEM.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXDEM.TabIndex = 10;
            this.buttonXDEM.Text = "...";
            this.buttonXDEM.Click += new System.EventHandler(this.buttonXDEM_Click);
            // 
            // textBoxXTIN
            // 
            // 
            // 
            // 
            this.textBoxXTIN.Border.Class = "TextBoxBorder";
            this.textBoxXTIN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXTIN.Location = new System.Drawing.Point(88, 18);
            this.textBoxXTIN.Name = "textBoxXTIN";
            this.textBoxXTIN.Size = new System.Drawing.Size(285, 21);
            this.textBoxXTIN.TabIndex = 8;
            this.textBoxXTIN.TextChanged += new System.EventHandler(this.textBoxXTIN_TextChanged);
            // 
            // textBoxXDEM
            // 
            // 
            // 
            // 
            this.textBoxXDEM.Border.Class = "TextBoxBorder";
            this.textBoxXDEM.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXDEM.Location = new System.Drawing.Point(88, 96);
            this.textBoxXDEM.Name = "textBoxXDEM";
            this.textBoxXDEM.Size = new System.Drawing.Size(285, 21);
            this.textBoxXDEM.TabIndex = 7;
            this.textBoxXDEM.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxXDEM_MouseClick);
            this.textBoxXDEM.TextChanged += new System.EventHandler(this.textBoxXDEM_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(18, 18);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(81, 23);
            this.labelX2.TabIndex = 6;
            this.labelX2.Text = "TIN路径：";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(18, 96);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(81, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "DEM路径：";
            // 
            // doubleInputCellSize
            // 
            // 
            // 
            // 
            this.doubleInputCellSize.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputCellSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputCellSize.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputCellSize.Increment = 1D;
            this.doubleInputCellSize.Location = new System.Drawing.Point(88, 128);
            this.doubleInputCellSize.Name = "doubleInputCellSize";
            this.doubleInputCellSize.ShowUpDown = true;
            this.doubleInputCellSize.Size = new System.Drawing.Size(285, 21);
            this.doubleInputCellSize.TabIndex = 12;
            // 
            // labelXCellSize
            // 
            this.labelXCellSize.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXCellSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXCellSize.Location = new System.Drawing.Point(18, 128);
            this.labelXCellSize.Name = "labelXCellSize";
            this.labelXCellSize.Size = new System.Drawing.Size(81, 23);
            this.labelXCellSize.TabIndex = 5;
            this.labelXCellSize.Text = "DEM分辨率：";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(18, 57);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(81, 23);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "从图层添加：";
            // 
            // cmbLayers
            // 
            this.cmbLayers.DisplayMember = "Text";
            this.cmbLayers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLayers.FormattingEnabled = true;
            this.cmbLayers.ItemHeight = 15;
            this.cmbLayers.Location = new System.Drawing.Point(88, 57);
            this.cmbLayers.Name = "cmbLayers";
            this.cmbLayers.Size = new System.Drawing.Size(285, 21);
            this.cmbLayers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLayers.TabIndex = 13;
            this.cmbLayers.SelectedIndexChanged += new System.EventHandler(this.cmbLayers_SelectedIndexChanged);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.cmbLayers);
            this.groupPanel1.Controls.Add(this.doubleInputCellSize);
            this.groupPanel1.Controls.Add(this.textBoxXDEM);
            this.groupPanel1.Controls.Add(this.textBoxXTIN);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.labelXCellSize);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.buttonXTIN);
            this.groupPanel1.Controls.Add(this.buttonXDEM);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(465, 172);
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
            this.groupPanel1.TabIndex = 14;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(346, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmTinToDEM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 248);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.buttonXOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTinToDEM";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TIN转DEM";
            this.Load += new System.EventHandler(this.FrmTinToDEM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputCellSize)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXTIN;
        private DevComponents.DotNetBar.ButtonX buttonXDEM;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXTIN;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXDEM;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.DoubleInput doubleInputCellSize;
        private DevComponents.DotNetBar.LabelX labelXCellSize;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLayers;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}