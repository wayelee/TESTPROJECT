namespace LibCerMap
{
    partial class FrmIndexTabStyle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIndexTabStyle));
            this.gPanelIndexTab = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.TabWith = new DevComponents.Editors.DoubleInput();
            this.labWith = new DevComponents.DotNetBar.LabelX();
            this.OutlineColor = new CustomColorPicker();
            this.ForegroundColor = new CustomColorPicker();
            this.labOutlineColor = new DevComponents.DotNetBar.LabelX();
            this.labForeground = new DevComponents.DotNetBar.LabelX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.gPanelIndexTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabWith)).BeginInit();
            this.SuspendLayout();
            // 
            // gPanelIndexTab
            // 
            this.gPanelIndexTab.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelIndexTab.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelIndexTab.Controls.Add(this.TabWith);
            this.gPanelIndexTab.Controls.Add(this.labWith);
            this.gPanelIndexTab.Controls.Add(this.OutlineColor);
            this.gPanelIndexTab.Controls.Add(this.ForegroundColor);
            this.gPanelIndexTab.Controls.Add(this.labOutlineColor);
            this.gPanelIndexTab.Controls.Add(this.labForeground);
            this.gPanelIndexTab.Dock = System.Windows.Forms.DockStyle.Top;
            this.gPanelIndexTab.Location = new System.Drawing.Point(0, 0);
            this.gPanelIndexTab.Name = "gPanelIndexTab";
            this.gPanelIndexTab.Size = new System.Drawing.Size(361, 171);
            // 
            // 
            // 
            this.gPanelIndexTab.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelIndexTab.Style.BackColorGradientAngle = 90;
            this.gPanelIndexTab.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelIndexTab.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelIndexTab.Style.BorderBottomWidth = 1;
            this.gPanelIndexTab.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelIndexTab.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelIndexTab.Style.BorderLeftWidth = 1;
            this.gPanelIndexTab.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelIndexTab.Style.BorderRightWidth = 1;
            this.gPanelIndexTab.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelIndexTab.Style.BorderTopWidth = 1;
            this.gPanelIndexTab.Style.CornerDiameter = 4;
            this.gPanelIndexTab.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelIndexTab.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelIndexTab.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelIndexTab.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelIndexTab.TabIndex = 1;
            // 
            // TabWith
            // 
            // 
            // 
            // 
            this.TabWith.BackgroundStyle.Class = "DateTimeInputBackground";
            this.TabWith.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TabWith.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.TabWith.Increment = 1D;
            this.TabWith.Location = new System.Drawing.Point(142, 112);
            this.TabWith.MinValue = 0D;
            this.TabWith.Name = "TabWith";
            this.TabWith.ShowUpDown = true;
            this.TabWith.Size = new System.Drawing.Size(74, 21);
            this.TabWith.TabIndex = 24;
            this.TabWith.Value = 20D;
            // 
            // labWith
            // 
            this.labWith.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labWith.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labWith.Location = new System.Drawing.Point(35, 112);
            this.labWith.Name = "labWith";
            this.labWith.Size = new System.Drawing.Size(84, 23);
            this.labWith.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labWith.TabIndex = 4;
            this.labWith.Text = "选项卡宽度：";
            // 
            // OutlineColor
            // 
            this.OutlineColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OutlineColor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.OutlineColor.Image = ((System.Drawing.Image)(resources.GetObject("OutlineColor.Image")));
            this.OutlineColor.Location = new System.Drawing.Point(142, 64);
            this.OutlineColor.Name = "OutlineColor";
            this.OutlineColor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.OutlineColor.Size = new System.Drawing.Size(74, 23);
            this.OutlineColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OutlineColor.TabIndex = 3;
            // 
            // ForegroundColor
            // 
            this.ForegroundColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ForegroundColor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ForegroundColor.Image = ((System.Drawing.Image)(resources.GetObject("ForegroundColor.Image")));
            this.ForegroundColor.Location = new System.Drawing.Point(142, 16);
            this.ForegroundColor.Name = "ForegroundColor";
            this.ForegroundColor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.ForegroundColor.Size = new System.Drawing.Size(74, 23);
            this.ForegroundColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ForegroundColor.TabIndex = 1;
            // 
            // labOutlineColor
            // 
            this.labOutlineColor.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labOutlineColor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labOutlineColor.Location = new System.Drawing.Point(46, 64);
            this.labOutlineColor.Name = "labOutlineColor";
            this.labOutlineColor.Size = new System.Drawing.Size(73, 23);
            this.labOutlineColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labOutlineColor.TabIndex = 2;
            this.labOutlineColor.Text = "边线颜色：";
            // 
            // labForeground
            // 
            this.labForeground.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labForeground.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labForeground.Location = new System.Drawing.Point(57, 19);
            this.labForeground.Name = "labForeground";
            this.labForeground.Size = new System.Drawing.Size(56, 23);
            this.labForeground.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labForeground.TabIndex = 1;
            this.labForeground.Text = "填充色：";
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(258, 194);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(168, 194);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 3;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // FrmIndexTabStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 237);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gPanelIndexTab);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmIndexTabStyle";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "索引选项卡设计";
            this.Load += new System.EventHandler(this.FrmIndexTabStyle_Load);
            this.gPanelIndexTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabWith)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelIndexTab;
        private DevComponents.Editors.DoubleInput TabWith;
        private DevComponents.DotNetBar.LabelX labWith;
        private CustomColorPicker OutlineColor;
        private CustomColorPicker ForegroundColor;
        private DevComponents.DotNetBar.LabelX labOutlineColor;
        private DevComponents.DotNetBar.LabelX labForeground;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.ButtonX btOK;
    }
}