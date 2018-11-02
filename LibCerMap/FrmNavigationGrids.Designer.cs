namespace LibCerMap
{
    partial class FrmNavigationGrids
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
            this.gPanelGrid = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btDeleteGrids = new DevComponents.DotNetBar.ButtonX();
            this.btGridsAttribute = new DevComponents.DotNetBar.ButtonX();
            this.btAddNewGrids = new DevComponents.DotNetBar.ButtonX();
            this.gPanelGridsList = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.itemPanelGrids = new DevComponents.DotNetBar.ItemPanel();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.btApply = new DevComponents.DotNetBar.ButtonX();
            this.gPanelGrid.SuspendLayout();
            this.gPanelGridsList.SuspendLayout();
            this.SuspendLayout();
            // 
            // gPanelGrid
            // 
            this.gPanelGrid.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelGrid.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gPanelGrid.Controls.Add(this.btDeleteGrids);
            this.gPanelGrid.Controls.Add(this.btGridsAttribute);
            this.gPanelGrid.Controls.Add(this.btAddNewGrids);
            this.gPanelGrid.Controls.Add(this.gPanelGridsList);
            this.gPanelGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.gPanelGrid.Location = new System.Drawing.Point(0, 0);
            this.gPanelGrid.Name = "gPanelGrid";
            this.gPanelGrid.Size = new System.Drawing.Size(389, 267);
            // 
            // 
            // 
            this.gPanelGrid.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelGrid.Style.BackColorGradientAngle = 90;
            this.gPanelGrid.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelGrid.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelGrid.Style.BorderBottomWidth = 1;
            this.gPanelGrid.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelGrid.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelGrid.Style.BorderLeftWidth = 1;
            this.gPanelGrid.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelGrid.Style.BorderRightWidth = 1;
            this.gPanelGrid.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelGrid.Style.BorderTopWidth = 1;
            this.gPanelGrid.Style.CornerDiameter = 4;
            this.gPanelGrid.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelGrid.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gPanelGrid.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelGrid.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelGrid.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelGrid.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelGrid.TabIndex = 0;
            // 
            // btDeleteGrids
            // 
            this.btDeleteGrids.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btDeleteGrids.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btDeleteGrids.Location = new System.Drawing.Point(257, 105);
            this.btDeleteGrids.Name = "btDeleteGrids";
            this.btDeleteGrids.Size = new System.Drawing.Size(84, 23);
            this.btDeleteGrids.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btDeleteGrids.TabIndex = 3;
            this.btDeleteGrids.Text = "删除格网";
            this.btDeleteGrids.Click += new System.EventHandler(this.btDeleteGrids_Click);
            // 
            // btGridsAttribute
            // 
            this.btGridsAttribute.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btGridsAttribute.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btGridsAttribute.Location = new System.Drawing.Point(257, 58);
            this.btGridsAttribute.Name = "btGridsAttribute";
            this.btGridsAttribute.Size = new System.Drawing.Size(84, 23);
            this.btGridsAttribute.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btGridsAttribute.TabIndex = 2;
            this.btGridsAttribute.Text = "属性";
            this.btGridsAttribute.Click += new System.EventHandler(this.btGridsAttribute_Click);
            // 
            // btAddNewGrids
            // 
            this.btAddNewGrids.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btAddNewGrids.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btAddNewGrids.Location = new System.Drawing.Point(257, 13);
            this.btAddNewGrids.Name = "btAddNewGrids";
            this.btAddNewGrids.Size = new System.Drawing.Size(84, 23);
            this.btAddNewGrids.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btAddNewGrids.TabIndex = 1;
            this.btAddNewGrids.Text = "新建格网";
            this.btAddNewGrids.Click += new System.EventHandler(this.btAddNewGrids_Click);
            // 
            // gPanelGridsList
            // 
            this.gPanelGridsList.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelGridsList.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelGridsList.Controls.Add(this.itemPanelGrids);
            this.gPanelGridsList.Dock = System.Windows.Forms.DockStyle.Left;
            this.gPanelGridsList.Location = new System.Drawing.Point(0, 0);
            this.gPanelGridsList.Name = "gPanelGridsList";
            this.gPanelGridsList.Size = new System.Drawing.Size(226, 261);
            // 
            // 
            // 
            this.gPanelGridsList.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelGridsList.Style.BackColorGradientAngle = 90;
            this.gPanelGridsList.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelGridsList.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelGridsList.Style.BorderBottomWidth = 1;
            this.gPanelGridsList.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelGridsList.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelGridsList.Style.BorderLeftWidth = 1;
            this.gPanelGridsList.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelGridsList.Style.BorderRightWidth = 1;
            this.gPanelGridsList.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelGridsList.Style.BorderTopWidth = 1;
            this.gPanelGridsList.Style.CornerDiameter = 4;
            this.gPanelGridsList.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelGridsList.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gPanelGridsList.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelGridsList.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelGridsList.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelGridsList.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelGridsList.TabIndex = 0;
            // 
            // itemPanelGrids
            // 
            // 
            // 
            // 
            this.itemPanelGrids.BackgroundStyle.Class = "ItemPanel";
            this.itemPanelGrids.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemPanelGrids.ContainerControlProcessDialogKey = true;
            this.itemPanelGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemPanelGrids.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemPanelGrids.Location = new System.Drawing.Point(0, 0);
            this.itemPanelGrids.Name = "itemPanelGrids";
            this.itemPanelGrids.Size = new System.Drawing.Size(220, 255);
            this.itemPanelGrids.TabIndex = 0;
            this.itemPanelGrids.Text = "itemPanel1";
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.Location = new System.Drawing.Point(95, 294);
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
            this.btCancel.Location = new System.Drawing.Point(181, 294);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btApply
            // 
            this.btApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btApply.Location = new System.Drawing.Point(264, 294);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 23);
            this.btApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btApply.TabIndex = 3;
            this.btApply.Text = "应用";
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // FrmNavigationGrids
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 351);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gPanelGrid);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNavigationGrids";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "格网";
            this.Load += new System.EventHandler(this.FrmGrids_Load);
            this.gPanelGrid.ResumeLayout(false);
            this.gPanelGridsList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelGrid;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelGridsList;
        private DevComponents.DotNetBar.ButtonX btDeleteGrids;
        private DevComponents.DotNetBar.ButtonX btGridsAttribute;
        private DevComponents.DotNetBar.ButtonX btAddNewGrids;
        private DevComponents.DotNetBar.ItemPanel itemPanelGrids;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.ButtonX btApply;
    }
}