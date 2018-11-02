namespace LibCerMap
{
    partial class FrmFrameAttrSel
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
            this.gPanelSelect = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cBoxFrame = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cBoxGrid = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.gPanelSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // gPanelSelect
            // 
            this.gPanelSelect.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelSelect.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelSelect.Controls.Add(this.cBoxFrame);
            this.gPanelSelect.Controls.Add(this.cBoxGrid);
            this.gPanelSelect.Location = new System.Drawing.Point(0, 6);
            this.gPanelSelect.Name = "gPanelSelect";
            this.gPanelSelect.Size = new System.Drawing.Size(313, 78);
            // 
            // 
            // 
            this.gPanelSelect.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelSelect.Style.BackColorGradientAngle = 90;
            this.gPanelSelect.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelSelect.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelSelect.Style.BorderBottomWidth = 1;
            this.gPanelSelect.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelSelect.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelSelect.Style.BorderLeftWidth = 1;
            this.gPanelSelect.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelSelect.Style.BorderRightWidth = 1;
            this.gPanelSelect.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelSelect.Style.BorderTopWidth = 1;
            this.gPanelSelect.Style.CornerDiameter = 4;
            this.gPanelSelect.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelSelect.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelSelect.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelSelect.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelSelect.TabIndex = 0;
            this.gPanelSelect.Text = "选择要编辑的框架属性";
            // 
            // cBoxFrame
            // 
            this.cBoxFrame.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cBoxFrame.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cBoxFrame.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cBoxFrame.Location = new System.Drawing.Point(163, 16);
            this.cBoxFrame.Name = "cBoxFrame";
            this.cBoxFrame.Size = new System.Drawing.Size(100, 23);
            this.cBoxFrame.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cBoxFrame.TabIndex = 1;
            this.cBoxFrame.Text = "框架属性设计";
            // 
            // cBoxGrid
            // 
            this.cBoxGrid.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cBoxGrid.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cBoxGrid.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cBoxGrid.Location = new System.Drawing.Point(30, 16);
            this.cBoxGrid.Name = "cBoxGrid";
            this.cBoxGrid.Size = new System.Drawing.Size(100, 23);
            this.cBoxGrid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cBoxGrid.TabIndex = 0;
            this.cBoxGrid.Text = "格网属性设计";
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(128, 95);
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
            this.btCancel.Location = new System.Drawing.Point(224, 95);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FrmFrameAttrSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 131);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gPanelSelect);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFrameAttrSel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性类型选择";
            this.Load += new System.EventHandler(this.FrmFrameAttrSel_Load);
            this.gPanelSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelSelect;
        private DevComponents.DotNetBar.Controls.CheckBoxX cBoxFrame;
        private DevComponents.DotNetBar.Controls.CheckBoxX cBoxGrid;
        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
    }
}