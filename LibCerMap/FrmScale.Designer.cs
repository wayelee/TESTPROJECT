namespace LibCerMap
{
    partial class FrmScale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScale));
            this.gPanelScaleBar = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.axSymbologyControl = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.gPanelScaleBarPreview = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.ImagePreview = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.btScalebarAttribute = new DevComponents.DotNetBar.ButtonX();
            this.gPanelScaleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl)).BeginInit();
            this.gPanelScaleBarPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // gPanelScaleBar
            // 
            this.gPanelScaleBar.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelScaleBar.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelScaleBar.Controls.Add(this.axSymbologyControl);
            this.gPanelScaleBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.gPanelScaleBar.Location = new System.Drawing.Point(0, 0);
            this.gPanelScaleBar.Name = "gPanelScaleBar";
            this.gPanelScaleBar.Size = new System.Drawing.Size(249, 450);
            // 
            // 
            // 
            this.gPanelScaleBar.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelScaleBar.Style.BackColorGradientAngle = 90;
            this.gPanelScaleBar.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelScaleBar.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelScaleBar.Style.BorderBottomWidth = 1;
            this.gPanelScaleBar.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelScaleBar.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelScaleBar.Style.BorderLeftWidth = 1;
            this.gPanelScaleBar.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelScaleBar.Style.BorderRightWidth = 1;
            this.gPanelScaleBar.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelScaleBar.Style.BorderTopWidth = 1;
            this.gPanelScaleBar.Style.CornerDiameter = 4;
            this.gPanelScaleBar.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelScaleBar.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gPanelScaleBar.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelScaleBar.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelScaleBar.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelScaleBar.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelScaleBar.TabIndex = 0;
            // 
            // axSymbologyControl
            // 
            this.axSymbologyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSymbologyControl.Location = new System.Drawing.Point(0, 0);
            this.axSymbologyControl.Name = "axSymbologyControl";
            this.axSymbologyControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl.OcxState")));
            this.axSymbologyControl.Size = new System.Drawing.Size(243, 444);
            this.axSymbologyControl.TabIndex = 0;
            this.axSymbologyControl.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl_OnItemSelected);
            // 
            // gPanelScaleBarPreview
            // 
            this.gPanelScaleBarPreview.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelScaleBarPreview.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelScaleBarPreview.Controls.Add(this.ImagePreview);
            this.gPanelScaleBarPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.gPanelScaleBarPreview.Location = new System.Drawing.Point(249, 0);
            this.gPanelScaleBarPreview.Name = "gPanelScaleBarPreview";
            this.gPanelScaleBarPreview.Size = new System.Drawing.Size(236, 146);
            // 
            // 
            // 
            this.gPanelScaleBarPreview.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelScaleBarPreview.Style.BackColorGradientAngle = 90;
            this.gPanelScaleBarPreview.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelScaleBarPreview.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelScaleBarPreview.Style.BorderBottomWidth = 1;
            this.gPanelScaleBarPreview.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelScaleBarPreview.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelScaleBarPreview.Style.BorderLeftWidth = 1;
            this.gPanelScaleBarPreview.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelScaleBarPreview.Style.BorderRightWidth = 1;
            this.gPanelScaleBarPreview.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelScaleBarPreview.Style.BorderTopWidth = 1;
            this.gPanelScaleBarPreview.Style.CornerDiameter = 4;
            this.gPanelScaleBarPreview.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelScaleBarPreview.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelScaleBarPreview.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelScaleBarPreview.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelScaleBarPreview.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelScaleBarPreview.TabIndex = 1;
            this.gPanelScaleBarPreview.Text = "预览";
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
            this.ImagePreview.Size = new System.Drawing.Size(230, 122);
            this.ImagePreview.TabIndex = 0;
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(258, 383);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 2;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(340, 383);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btScalebarAttribute
            // 
            this.btScalebarAttribute.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btScalebarAttribute.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btScalebarAttribute.Location = new System.Drawing.Point(271, 323);
            this.btScalebarAttribute.Name = "btScalebarAttribute";
            this.btScalebarAttribute.Size = new System.Drawing.Size(125, 23);
            this.btScalebarAttribute.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btScalebarAttribute.TabIndex = 4;
            this.btScalebarAttribute.Text = "属性";
            this.btScalebarAttribute.Click += new System.EventHandler(this.btScalebarAttribute_Click);
            // 
            // FrmScale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 450);
            this.Controls.Add(this.btScalebarAttribute);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gPanelScaleBarPreview);
            this.Controls.Add(this.gPanelScaleBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmScale";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图形比例尺";
            this.Load += new System.EventHandler(this.FrmScaleBar_Load);
            this.gPanelScaleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl)).EndInit();
            this.gPanelScaleBarPreview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelScaleBar;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelScaleBarPreview;
        private DevComponents.DotNetBar.Controls.ReflectionImage ImagePreview;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.ButtonX btScalebarAttribute;
    }
}