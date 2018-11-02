namespace LibCerMap
{
    partial class FrmSymbolStyle
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
            this.gPanelSymbolStyle = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.gPanelStyleList = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.itemPanelStyleList = new DevComponents.DotNetBar.ItemPanel();
            this.checkBoxItem11 = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxItem1 = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxItem8 = new DevComponents.DotNetBar.CheckBoxItem();
            this.gPanelSymbolStyle.SuspendLayout();
            this.gPanelStyleList.SuspendLayout();
            this.SuspendLayout();
            // 
            // gPanelSymbolStyle
            // 
            this.gPanelSymbolStyle.BackColor = System.Drawing.Color.Transparent;
            this.gPanelSymbolStyle.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelSymbolStyle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelSymbolStyle.Controls.Add(this.btCancel);
            this.gPanelSymbolStyle.Controls.Add(this.btOK);
            this.gPanelSymbolStyle.Controls.Add(this.gPanelStyleList);
            this.gPanelSymbolStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gPanelSymbolStyle.Location = new System.Drawing.Point(0, 0);
            this.gPanelSymbolStyle.Name = "gPanelSymbolStyle";
            this.gPanelSymbolStyle.Size = new System.Drawing.Size(352, 447);
            // 
            // 
            // 
            this.gPanelSymbolStyle.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelSymbolStyle.Style.BackColorGradientAngle = 90;
            this.gPanelSymbolStyle.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelSymbolStyle.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelSymbolStyle.Style.BorderBottomWidth = 1;
            this.gPanelSymbolStyle.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelSymbolStyle.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelSymbolStyle.Style.BorderLeftWidth = 1;
            this.gPanelSymbolStyle.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelSymbolStyle.Style.BorderRightWidth = 1;
            this.gPanelSymbolStyle.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelSymbolStyle.Style.BorderTopWidth = 1;
            this.gPanelSymbolStyle.Style.CornerDiameter = 4;
            this.gPanelSymbolStyle.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelSymbolStyle.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelSymbolStyle.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelSymbolStyle.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelSymbolStyle.TabIndex = 0;
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(235, 399);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(135, 399);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 2;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // gPanelStyleList
            // 
            this.gPanelStyleList.BackColor = System.Drawing.Color.Transparent;
            this.gPanelStyleList.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelStyleList.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelStyleList.Controls.Add(this.itemPanelStyleList);
            this.gPanelStyleList.Dock = System.Windows.Forms.DockStyle.Top;
            this.gPanelStyleList.Location = new System.Drawing.Point(0, 0);
            this.gPanelStyleList.Name = "gPanelStyleList";
            this.gPanelStyleList.Size = new System.Drawing.Size(346, 381);
            // 
            // 
            // 
            this.gPanelStyleList.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelStyleList.Style.BackColorGradientAngle = 90;
            this.gPanelStyleList.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelStyleList.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelStyleList.Style.BorderBottomWidth = 1;
            this.gPanelStyleList.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelStyleList.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelStyleList.Style.BorderLeftWidth = 1;
            this.gPanelStyleList.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelStyleList.Style.BorderRightWidth = 1;
            this.gPanelStyleList.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelStyleList.Style.BorderTopWidth = 1;
            this.gPanelStyleList.Style.CornerDiameter = 4;
            this.gPanelStyleList.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelStyleList.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelStyleList.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelStyleList.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelStyleList.TabIndex = 1;
            // 
            // itemPanelStyleList
            // 
            this.itemPanelStyleList.AutoScroll = true;
            // 
            // 
            // 
            this.itemPanelStyleList.BackgroundStyle.Class = "ItemPanel";
            this.itemPanelStyleList.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemPanelStyleList.ContainerControlProcessDialogKey = true;
            this.itemPanelStyleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemPanelStyleList.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemPanelStyleList.Location = new System.Drawing.Point(0, 0);
            this.itemPanelStyleList.Name = "itemPanelStyleList";
            this.itemPanelStyleList.Size = new System.Drawing.Size(340, 375);
            this.itemPanelStyleList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.itemPanelStyleList.TabIndex = 2;
            this.itemPanelStyleList.ItemClick += new System.EventHandler(this.itemPanelStyleList_ItemClick);
            // 
            // checkBoxItem11
            // 
            this.checkBoxItem11.Name = "checkBoxItem11";
            this.checkBoxItem11.Text = "checkBoxItem11";
            // 
            // checkBoxItem1
            // 
            this.checkBoxItem1.Name = "checkBoxItem1";
            this.checkBoxItem1.Text = "checkBoxItem1";
            // 
            // checkBoxItem8
            // 
            this.checkBoxItem8.Name = "checkBoxItem8";
            this.checkBoxItem8.Text = "checkBoxItem8";
            // 
            // FrmSymbolStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 447);
            this.Controls.Add(this.gPanelSymbolStyle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSymbolStyle";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "样式选择";
            this.Load += new System.EventHandler(this.FrmSymbolStyle_Load);
            this.gPanelSymbolStyle.ResumeLayout(false);
            this.gPanelStyleList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelSymbolStyle;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelStyleList;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem11;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem1;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem8;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ItemPanel itemPanelStyleList;
    }
}