namespace LibCerMap
{
    partial class FrmControlPointToCenterLine
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
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.gPanelLine = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtLineFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btAddLine = new DevComponents.DotNetBar.ButtonX();
            this.gPanelPoint = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cboBoxPointLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btAddPoint = new DevComponents.DotNetBar.ButtonX();
            this.gPanelLine.SuspendLayout();
            this.gPanelPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.Location = new System.Drawing.Point(210, 199);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 0;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.Location = new System.Drawing.Point(302, 199);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gPanelLine
            // 
            this.gPanelLine.BackColor = System.Drawing.Color.Transparent;
            this.gPanelLine.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelLine.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelLine.Controls.Add(this.txtLineFilePath);
            this.gPanelLine.Controls.Add(this.btAddLine);
            this.gPanelLine.Location = new System.Drawing.Point(12, 97);
            this.gPanelLine.Name = "gPanelLine";
            this.gPanelLine.Size = new System.Drawing.Size(369, 81);
            // 
            // 
            // 
            this.gPanelLine.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelLine.Style.BackColorGradientAngle = 90;
            this.gPanelLine.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelLine.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderBottomWidth = 1;
            this.gPanelLine.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelLine.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderLeftWidth = 1;
            this.gPanelLine.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderRightWidth = 1;
            this.gPanelLine.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderTopWidth = 1;
            this.gPanelLine.Style.CornerDiameter = 4;
            this.gPanelLine.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelLine.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelLine.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelLine.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelLine.TabIndex = 3;
            this.gPanelLine.Text = "输出线文件";
            // 
            // txtLineFilePath
            // 
            // 
            // 
            // 
            this.txtLineFilePath.Border.Class = "TextBoxBorder";
            this.txtLineFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLineFilePath.Location = new System.Drawing.Point(22, 16);
            this.txtLineFilePath.Name = "txtLineFilePath";
            this.txtLineFilePath.Size = new System.Drawing.Size(247, 21);
            this.txtLineFilePath.TabIndex = 2;
            // 
            // btAddLine
            // 
            this.btAddLine.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btAddLine.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btAddLine.Location = new System.Drawing.Point(287, 14);
            this.btAddLine.Name = "btAddLine";
            this.btAddLine.Size = new System.Drawing.Size(51, 23);
            this.btAddLine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btAddLine.TabIndex = 1;
            this.btAddLine.Text = "浏览";
            this.btAddLine.Click += new System.EventHandler(this.btAddLine_Click);
            // 
            // gPanelPoint
            // 
            this.gPanelPoint.BackColor = System.Drawing.Color.Transparent;
            this.gPanelPoint.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelPoint.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelPoint.Controls.Add(this.cboBoxPointLayer);
            this.gPanelPoint.Controls.Add(this.btAddPoint);
            this.gPanelPoint.Location = new System.Drawing.Point(12, 12);
            this.gPanelPoint.Name = "gPanelPoint";
            this.gPanelPoint.Size = new System.Drawing.Size(369, 79);
            // 
            // 
            // 
            this.gPanelPoint.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelPoint.Style.BackColorGradientAngle = 90;
            this.gPanelPoint.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelPoint.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderBottomWidth = 1;
            this.gPanelPoint.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelPoint.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderLeftWidth = 1;
            this.gPanelPoint.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderRightWidth = 1;
            this.gPanelPoint.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderTopWidth = 1;
            this.gPanelPoint.Style.CornerDiameter = 4;
            this.gPanelPoint.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelPoint.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelPoint.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelPoint.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelPoint.TabIndex = 2;
            this.gPanelPoint.Text = "输入点文件";
            // 
            // cboBoxPointLayer
            // 
            this.cboBoxPointLayer.DisplayMember = "Text";
            this.cboBoxPointLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBoxPointLayer.FormattingEnabled = true;
            this.cboBoxPointLayer.ItemHeight = 15;
            this.cboBoxPointLayer.Location = new System.Drawing.Point(22, 16);
            this.cboBoxPointLayer.Name = "cboBoxPointLayer";
            this.cboBoxPointLayer.Size = new System.Drawing.Size(247, 21);
            this.cboBoxPointLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboBoxPointLayer.TabIndex = 1;
            // 
            // btAddPoint
            // 
            this.btAddPoint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btAddPoint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btAddPoint.Location = new System.Drawing.Point(287, 14);
            this.btAddPoint.Name = "btAddPoint";
            this.btAddPoint.Size = new System.Drawing.Size(51, 23);
            this.btAddPoint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btAddPoint.TabIndex = 0;
            this.btAddPoint.Text = "浏览";
            this.btAddPoint.Click += new System.EventHandler(this.btAddPoint_Click);
            // 
            // FrmPointToLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 237);
            this.Controls.Add(this.gPanelLine);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.gPanelPoint);
            this.Controls.Add(this.btOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPointToLine";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "点转线";
            this.Load += new System.EventHandler(this.FrmPointToLine_Load);
            this.gPanelLine.ResumeLayout(false);
            this.gPanelPoint.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelLine;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLineFilePath;
        private DevComponents.DotNetBar.ButtonX btAddLine;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelPoint;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboBoxPointLayer;
        private DevComponents.DotNetBar.ButtonX btAddPoint;
    }
}