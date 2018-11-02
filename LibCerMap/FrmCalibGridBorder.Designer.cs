namespace LibCerMap
{
    partial class FrmCalibGridBorder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCalibGridBorder));
            this.gPanelBorder = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.ckBoxAlternating = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.BorderInterval = new DevComponents.Editors.DoubleInput();
            this.BorderWith = new DevComponents.Editors.DoubleInput();
            this.labInterval = new DevComponents.DotNetBar.LabelX();
            this.labWith = new DevComponents.DotNetBar.LabelX();
            this.ForegroundColor = new CustomColorPicker();
            this.BackgroundColor = new CustomColorPicker();
            this.labForeground = new DevComponents.DotNetBar.LabelX();
            this.labBackground = new DevComponents.DotNetBar.LabelX();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.gPanelBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BorderInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BorderWith)).BeginInit();
            this.SuspendLayout();
            // 
            // gPanelBorder
            // 
            this.gPanelBorder.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelBorder.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelBorder.Controls.Add(this.ckBoxAlternating);
            this.gPanelBorder.Controls.Add(this.BorderInterval);
            this.gPanelBorder.Controls.Add(this.BorderWith);
            this.gPanelBorder.Controls.Add(this.labInterval);
            this.gPanelBorder.Controls.Add(this.labWith);
            this.gPanelBorder.Controls.Add(this.ForegroundColor);
            this.gPanelBorder.Controls.Add(this.BackgroundColor);
            this.gPanelBorder.Controls.Add(this.labForeground);
            this.gPanelBorder.Controls.Add(this.labBackground);
            this.gPanelBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.gPanelBorder.Location = new System.Drawing.Point(0, 0);
            this.gPanelBorder.Name = "gPanelBorder";
            this.gPanelBorder.Size = new System.Drawing.Size(283, 241);
            // 
            // 
            // 
            this.gPanelBorder.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelBorder.Style.BackColorGradientAngle = 90;
            this.gPanelBorder.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelBorder.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelBorder.Style.BorderBottomWidth = 1;
            this.gPanelBorder.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelBorder.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelBorder.Style.BorderLeftWidth = 1;
            this.gPanelBorder.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelBorder.Style.BorderRightWidth = 1;
            this.gPanelBorder.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelBorder.Style.BorderTopWidth = 1;
            this.gPanelBorder.Style.CornerDiameter = 4;
            this.gPanelBorder.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelBorder.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelBorder.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelBorder.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelBorder.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelBorder.TabIndex = 0;
            // 
            // ckBoxAlternating
            // 
            this.ckBoxAlternating.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ckBoxAlternating.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckBoxAlternating.Location = new System.Drawing.Point(78, 195);
            this.ckBoxAlternating.Name = "ckBoxAlternating";
            this.ckBoxAlternating.Size = new System.Drawing.Size(124, 23);
            this.ckBoxAlternating.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckBoxAlternating.TabIndex = 26;
            this.ckBoxAlternating.Text = "是否使用交互边框";
            // 
            // BorderInterval
            // 
            // 
            // 
            // 
            this.BorderInterval.BackgroundStyle.Class = "DateTimeInputBackground";
            this.BorderInterval.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.BorderInterval.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.BorderInterval.Increment = 1D;
            this.BorderInterval.Location = new System.Drawing.Point(128, 154);
            this.BorderInterval.MinValue = 0D;
            this.BorderInterval.Name = "BorderInterval";
            this.BorderInterval.ShowUpDown = true;
            this.BorderInterval.Size = new System.Drawing.Size(74, 21);
            this.BorderInterval.TabIndex = 25;
            this.BorderInterval.Value = 72D;
            // 
            // BorderWith
            // 
            // 
            // 
            // 
            this.BorderWith.BackgroundStyle.Class = "DateTimeInputBackground";
            this.BorderWith.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.BorderWith.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.BorderWith.Increment = 1D;
            this.BorderWith.Location = new System.Drawing.Point(128, 117);
            this.BorderWith.MinValue = 0D;
            this.BorderWith.Name = "BorderWith";
            this.BorderWith.ShowUpDown = true;
            this.BorderWith.Size = new System.Drawing.Size(74, 21);
            this.BorderWith.TabIndex = 24;
            this.BorderWith.Value = 6D;
            // 
            // labInterval
            // 
            this.labInterval.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labInterval.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labInterval.Location = new System.Drawing.Point(66, 154);
            this.labInterval.Name = "labInterval";
            this.labInterval.Size = new System.Drawing.Size(45, 23);
            this.labInterval.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labInterval.TabIndex = 5;
            this.labInterval.Text = "间隔：";
            // 
            // labWith
            // 
            this.labWith.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labWith.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labWith.Location = new System.Drawing.Point(66, 115);
            this.labWith.Name = "labWith";
            this.labWith.Size = new System.Drawing.Size(45, 23);
            this.labWith.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labWith.TabIndex = 4;
            this.labWith.Text = "宽度：";
            // 
            // ForegroundColor
            // 
            this.ForegroundColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ForegroundColor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ForegroundColor.Image = ((System.Drawing.Image)(resources.GetObject("ForegroundColor.Image")));
            this.ForegroundColor.Location = new System.Drawing.Point(128, 67);
            this.ForegroundColor.Name = "ForegroundColor";
            this.ForegroundColor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.ForegroundColor.Size = new System.Drawing.Size(74, 23);
            this.ForegroundColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ForegroundColor.TabIndex = 3;
            // 
            // BackgroundColor
            // 
            this.BackgroundColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BackgroundColor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BackgroundColor.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundColor.Image")));
            this.BackgroundColor.Location = new System.Drawing.Point(128, 19);
            this.BackgroundColor.Name = "BackgroundColor";
            this.BackgroundColor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.BackgroundColor.Size = new System.Drawing.Size(74, 23);
            this.BackgroundColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BackgroundColor.TabIndex = 1;
            // 
            // labForeground
            // 
            this.labForeground.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labForeground.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labForeground.Location = new System.Drawing.Point(66, 67);
            this.labForeground.Name = "labForeground";
            this.labForeground.Size = new System.Drawing.Size(63, 23);
            this.labForeground.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labForeground.TabIndex = 2;
            this.labForeground.Text = "空颜色：";
            // 
            // labBackground
            // 
            this.labBackground.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labBackground.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labBackground.Location = new System.Drawing.Point(66, 19);
            this.labBackground.Name = "labBackground";
            this.labBackground.Size = new System.Drawing.Size(63, 23);
            this.labBackground.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labBackground.TabIndex = 1;
            this.labBackground.Text = "背景色：";
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(75, 257);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 1;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(172, 257);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FrmCalibGridBorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 296);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gPanelBorder);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCalibGridBorder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "格网整饰边框";
            this.Load += new System.EventHandler(this.FrmCalibGridBorder_Load);
            this.gPanelBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BorderInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BorderWith)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelBorder;
        private DevComponents.DotNetBar.LabelX labWith;
        private CustomColorPicker ForegroundColor;
        private CustomColorPicker BackgroundColor;
        private DevComponents.DotNetBar.LabelX labForeground;
        private DevComponents.DotNetBar.LabelX labBackground;
        private DevComponents.Editors.DoubleInput BorderWith;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckBoxAlternating;
        private DevComponents.Editors.DoubleInput BorderInterval;
        private DevComponents.DotNetBar.LabelX labInterval;

    }
}