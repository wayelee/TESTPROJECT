namespace LibCerMap
{
    partial class FrmFrameShadow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFrameShadow));
            this.gPanelBorder = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.gPanelAttribute = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.colorOutline = new CustomColorPicker();
            this.labOutlineColor = new DevComponents.DotNetBar.LabelX();
            this.btShadowStyle = new DevComponents.DotNetBar.ButtonX();
            this.SizeShadowOutline = new DevComponents.Editors.DoubleInput();
            this.colorFill = new CustomColorPicker();
            this.labFillColor = new DevComponents.DotNetBar.LabelX();
            this.labSize = new DevComponents.DotNetBar.LabelX();
            this.gPanelPreview = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.ImagePreview = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.gPanelSymbolLayout = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.axSymbologyCtlFrame = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.gPanelBorder.SuspendLayout();
            this.gPanelAttribute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeShadowOutline)).BeginInit();
            this.gPanelPreview.SuspendLayout();
            this.gPanelSymbolLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyCtlFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // gPanelBorder
            // 
            this.gPanelBorder.BackColor = System.Drawing.Color.Transparent;
            this.gPanelBorder.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelBorder.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelBorder.Controls.Add(this.btCancel);
            this.gPanelBorder.Controls.Add(this.btOK);
            this.gPanelBorder.Controls.Add(this.gPanelAttribute);
            this.gPanelBorder.Controls.Add(this.gPanelPreview);
            this.gPanelBorder.Controls.Add(this.gPanelSymbolLayout);
            this.gPanelBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gPanelBorder.Location = new System.Drawing.Point(0, 0);
            this.gPanelBorder.Name = "gPanelBorder";
            this.gPanelBorder.Size = new System.Drawing.Size(503, 436);
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
            this.gPanelBorder.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
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
            this.gPanelBorder.TabIndex = 2;
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(389, 386);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 15;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(307, 386);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 14;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // gPanelAttribute
            // 
            this.gPanelAttribute.BackColor = System.Drawing.Color.Transparent;
            this.gPanelAttribute.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelAttribute.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelAttribute.Controls.Add(this.colorOutline);
            this.gPanelAttribute.Controls.Add(this.labOutlineColor);
            this.gPanelAttribute.Controls.Add(this.btShadowStyle);
            this.gPanelAttribute.Controls.Add(this.SizeShadowOutline);
            this.gPanelAttribute.Controls.Add(this.colorFill);
            this.gPanelAttribute.Controls.Add(this.labFillColor);
            this.gPanelAttribute.Controls.Add(this.labSize);
            this.gPanelAttribute.Location = new System.Drawing.Point(310, 148);
            this.gPanelAttribute.Name = "gPanelAttribute";
            this.gPanelAttribute.Size = new System.Drawing.Size(154, 179);
            // 
            // 
            // 
            this.gPanelAttribute.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelAttribute.Style.BackColorGradientAngle = 90;
            this.gPanelAttribute.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelAttribute.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelAttribute.Style.BorderBottomWidth = 1;
            this.gPanelAttribute.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelAttribute.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelAttribute.Style.BorderLeftWidth = 1;
            this.gPanelAttribute.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelAttribute.Style.BorderRightWidth = 1;
            this.gPanelAttribute.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelAttribute.Style.BorderTopWidth = 1;
            this.gPanelAttribute.Style.CornerDiameter = 4;
            this.gPanelAttribute.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelAttribute.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelAttribute.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelAttribute.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelAttribute.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelAttribute.TabIndex = 13;
            this.gPanelAttribute.Text = "属性";
            // 
            // colorOutline
            // 
            this.colorOutline.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.colorOutline.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.colorOutline.Image = ((System.Drawing.Image)(resources.GetObject("colorOutline.Image")));
            this.colorOutline.Location = new System.Drawing.Point(78, 49);
            this.colorOutline.Name = "colorOutline";
            this.colorOutline.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.colorOutline.Size = new System.Drawing.Size(57, 23);
            this.colorOutline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.colorOutline.TabIndex = 9;
            this.colorOutline.SelectedColorChanged += new System.EventHandler(this.colorOutline_SelectedColorChanged);
            // 
            // labOutlineColor
            // 
            this.labOutlineColor.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labOutlineColor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labOutlineColor.Location = new System.Drawing.Point(12, 50);
            this.labOutlineColor.Name = "labOutlineColor";
            this.labOutlineColor.Size = new System.Drawing.Size(67, 22);
            this.labOutlineColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labOutlineColor.TabIndex = 8;
            this.labOutlineColor.Text = "边线颜色：";
            // 
            // btShadowStyle
            // 
            this.btShadowStyle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btShadowStyle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btShadowStyle.Location = new System.Drawing.Point(21, 125);
            this.btShadowStyle.Name = "btShadowStyle";
            this.btShadowStyle.Size = new System.Drawing.Size(107, 23);
            this.btShadowStyle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btShadowStyle.TabIndex = 7;
            this.btShadowStyle.Text = "改变样式";
            this.btShadowStyle.Click += new System.EventHandler(this.btShadowStyle_Click);
            // 
            // SizeShadowOutline
            // 
            // 
            // 
            // 
            this.SizeShadowOutline.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SizeShadowOutline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.SizeShadowOutline.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.SizeShadowOutline.Increment = 1D;
            this.SizeShadowOutline.Location = new System.Drawing.Point(78, 90);
            this.SizeShadowOutline.MinValue = 0D;
            this.SizeShadowOutline.Name = "SizeShadowOutline";
            this.SizeShadowOutline.ShowUpDown = true;
            this.SizeShadowOutline.Size = new System.Drawing.Size(57, 21);
            this.SizeShadowOutline.TabIndex = 6;
            this.SizeShadowOutline.Value = 72D;
            this.SizeShadowOutline.ValueChanged += new System.EventHandler(this.SizeShadowOutline_ValueChanged);
            // 
            // colorFill
            // 
            this.colorFill.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.colorFill.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.colorFill.Image = ((System.Drawing.Image)(resources.GetObject("colorFill.Image")));
            this.colorFill.Location = new System.Drawing.Point(78, 9);
            this.colorFill.Name = "colorFill";
            this.colorFill.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.colorFill.Size = new System.Drawing.Size(57, 23);
            this.colorFill.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.colorFill.TabIndex = 5;
            this.colorFill.SelectedColorChanged += new System.EventHandler(this.colorFill_SelectedColorChanged);
            // 
            // labFillColor
            // 
            this.labFillColor.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labFillColor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labFillColor.Location = new System.Drawing.Point(12, 9);
            this.labFillColor.Name = "labFillColor";
            this.labFillColor.Size = new System.Drawing.Size(67, 22);
            this.labFillColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labFillColor.TabIndex = 3;
            this.labFillColor.Text = "填充颜色：";
            // 
            // labSize
            // 
            this.labSize.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labSize.Location = new System.Drawing.Point(12, 90);
            this.labSize.Name = "labSize";
            this.labSize.Size = new System.Drawing.Size(67, 22);
            this.labSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labSize.TabIndex = 2;
            this.labSize.Text = "边线宽度：";
            // 
            // gPanelPreview
            // 
            this.gPanelPreview.BackColor = System.Drawing.Color.Transparent;
            this.gPanelPreview.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelPreview.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelPreview.Controls.Add(this.ImagePreview);
            this.gPanelPreview.Location = new System.Drawing.Point(310, 9);
            this.gPanelPreview.Name = "gPanelPreview";
            this.gPanelPreview.Size = new System.Drawing.Size(154, 133);
            // 
            // 
            // 
            this.gPanelPreview.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelPreview.Style.BackColorGradientAngle = 90;
            this.gPanelPreview.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelPreview.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPreview.Style.BorderBottomWidth = 1;
            this.gPanelPreview.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelPreview.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPreview.Style.BorderLeftWidth = 1;
            this.gPanelPreview.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPreview.Style.BorderRightWidth = 1;
            this.gPanelPreview.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPreview.Style.BorderTopWidth = 1;
            this.gPanelPreview.Style.CornerDiameter = 4;
            this.gPanelPreview.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelPreview.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelPreview.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelPreview.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelPreview.TabIndex = 3;
            this.gPanelPreview.Text = "预览";
            // 
            // ImagePreview
            // 
            // 
            // 
            // 
            this.ImagePreview.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ImagePreview.BackgroundStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ImagePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagePreview.Image = ((System.Drawing.Image)(resources.GetObject("ImagePreview.Image")));
            this.ImagePreview.Location = new System.Drawing.Point(0, 0);
            this.ImagePreview.Name = "ImagePreview";
            this.ImagePreview.Size = new System.Drawing.Size(148, 109);
            this.ImagePreview.TabIndex = 2;
            // 
            // gPanelSymbolLayout
            // 
            this.gPanelSymbolLayout.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelSymbolLayout.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelSymbolLayout.Controls.Add(this.axSymbologyCtlFrame);
            this.gPanelSymbolLayout.Location = new System.Drawing.Point(6, 6);
            this.gPanelSymbolLayout.Name = "gPanelSymbolLayout";
            this.gPanelSymbolLayout.Size = new System.Drawing.Size(295, 403);
            // 
            // 
            // 
            this.gPanelSymbolLayout.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelSymbolLayout.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelSymbolLayout.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelSymbolLayout.TabIndex = 2;
            // 
            // axSymbologyCtlFrame
            // 
            this.axSymbologyCtlFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSymbologyCtlFrame.Location = new System.Drawing.Point(0, 0);
            this.axSymbologyCtlFrame.Name = "axSymbologyCtlFrame";
            this.axSymbologyCtlFrame.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyCtlFrame.OcxState")));
            this.axSymbologyCtlFrame.Size = new System.Drawing.Size(295, 403);
            this.axSymbologyCtlFrame.TabIndex = 2;
            this.axSymbologyCtlFrame.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.axSymbologyCtlFrame_OnDoubleClick);
            this.axSymbologyCtlFrame.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyCtlFrame_OnItemSelected);
            // 
            // FrmFrameShadow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 436);
            this.Controls.Add(this.gPanelBorder);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFrameShadow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "阴影";
            this.Load += new System.EventHandler(this.FrmFrameShadow_Load);
            this.gPanelBorder.ResumeLayout(false);
            this.gPanelAttribute.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SizeShadowOutline)).EndInit();
            this.gPanelPreview.ResumeLayout(false);
            this.gPanelSymbolLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyCtlFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelBorder;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelAttribute;
        private CustomColorPicker colorOutline;
        private DevComponents.DotNetBar.LabelX labOutlineColor;
        private DevComponents.DotNetBar.ButtonX btShadowStyle;
        private DevComponents.Editors.DoubleInput SizeShadowOutline;
        private CustomColorPicker colorFill;
        private DevComponents.DotNetBar.LabelX labFillColor;
        private DevComponents.DotNetBar.LabelX labSize;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelPreview;
        private DevComponents.DotNetBar.Controls.ReflectionImage ImagePreview;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelSymbolLayout;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyCtlFrame;
    }
}