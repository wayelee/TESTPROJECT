namespace LibCerMap
{
    partial class FrmRandomModelSetting
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
            this.lblRockCount = new DevComponents.DotNetBar.LabelX();
            this.txtRockCount = new DevComponents.Editors.IntegerInput();
            this.sldRockCount = new DevComponents.DotNetBar.Controls.Slider();
            this.lblCraterCount = new DevComponents.DotNetBar.LabelX();
            this.txtCraterCount = new DevComponents.Editors.IntegerInput();
            this.sldCraterCount = new DevComponents.DotNetBar.Controls.Slider();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            ((System.ComponentModel.ISupportInitialize)(this.txtRockCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCraterCount)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRockCount
            // 
            this.lblRockCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblRockCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRockCount.Location = new System.Drawing.Point(19, 24);
            this.lblRockCount.Name = "lblRockCount";
            this.lblRockCount.Size = new System.Drawing.Size(82, 23);
            this.lblRockCount.TabIndex = 0;
            this.lblRockCount.Text = "石块数量：";
            // 
            // txtRockCount
            // 
            // 
            // 
            // 
            this.txtRockCount.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtRockCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRockCount.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtRockCount.Location = new System.Drawing.Point(99, 24);
            this.txtRockCount.Name = "txtRockCount";
            this.txtRockCount.ShowUpDown = true;
            this.txtRockCount.Size = new System.Drawing.Size(80, 21);
            this.txtRockCount.TabIndex = 1;
            this.txtRockCount.Value = 10;
            this.txtRockCount.ValueChanged += new System.EventHandler(this.txtRockCount_ValueChanged);
            // 
            // sldRockCount
            // 
            this.sldRockCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.sldRockCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sldRockCount.LabelVisible = false;
            this.sldRockCount.Location = new System.Drawing.Point(202, 21);
            this.sldRockCount.Name = "sldRockCount";
            this.sldRockCount.Size = new System.Drawing.Size(188, 23);
            this.sldRockCount.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sldRockCount.TabIndex = 3;
            this.sldRockCount.TabStop = false;
            this.sldRockCount.Value = 10;
            this.sldRockCount.ValueChanged += new System.EventHandler(this.sldRockCount_ValueChanged);
            // 
            // lblCraterCount
            // 
            this.lblCraterCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCraterCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCraterCount.Location = new System.Drawing.Point(19, 75);
            this.lblCraterCount.Name = "lblCraterCount";
            this.lblCraterCount.Size = new System.Drawing.Size(82, 23);
            this.lblCraterCount.TabIndex = 0;
            this.lblCraterCount.Text = "撞击坑数量：";
            // 
            // txtCraterCount
            // 
            // 
            // 
            // 
            this.txtCraterCount.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtCraterCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCraterCount.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtCraterCount.Location = new System.Drawing.Point(99, 75);
            this.txtCraterCount.Name = "txtCraterCount";
            this.txtCraterCount.ShowUpDown = true;
            this.txtCraterCount.Size = new System.Drawing.Size(80, 21);
            this.txtCraterCount.TabIndex = 2;
            this.txtCraterCount.Value = 10;
            this.txtCraterCount.ValueChanged += new System.EventHandler(this.txtCraterCount_ValueChanged);
            // 
            // sldCraterCount
            // 
            this.sldCraterCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.sldCraterCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sldCraterCount.LabelVisible = false;
            this.sldCraterCount.Location = new System.Drawing.Point(202, 75);
            this.sldCraterCount.Name = "sldCraterCount";
            this.sldCraterCount.Size = new System.Drawing.Size(188, 23);
            this.sldCraterCount.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sldCraterCount.TabIndex = 4;
            this.sldCraterCount.TabStop = false;
            this.sldCraterCount.Value = 10;
            this.sldCraterCount.ValueChanged += new System.EventHandler(this.sldCraterCount_ValueChanged);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(221, 159);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(314, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtRockCount);
            this.groupPanel1.Controls.Add(this.sldCraterCount);
            this.groupPanel1.Controls.Add(this.txtCraterCount);
            this.groupPanel1.Controls.Add(this.sldRockCount);
            this.groupPanel1.Controls.Add(this.lblRockCount);
            this.groupPanel1.Controls.Add(this.lblCraterCount);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(417, 136);
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
            this.groupPanel1.TabIndex = 5;
            // 
            // FrmRandomModelSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 205);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRandomModelSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "随机模型参数";
            this.Load += new System.EventHandler(this.FrmRandomModelSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtRockCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCraterCount)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblRockCount;
        private DevComponents.Editors.IntegerInput txtRockCount;
        private DevComponents.DotNetBar.Controls.Slider sldRockCount;
        private DevComponents.DotNetBar.LabelX lblCraterCount;
        private DevComponents.Editors.IntegerInput txtCraterCount;
        private DevComponents.DotNetBar.Controls.Slider sldCraterCount;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
    }
}