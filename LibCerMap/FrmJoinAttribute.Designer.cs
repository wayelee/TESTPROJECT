namespace LibCerMap
{
    partial class FrmJoinAttribute
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
            this.lbllayertable = new DevComponents.DotNetBar.LabelX();
            this.cmbtable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.gPanelouttable = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnInputShp = new DevComponents.DotNetBar.ButtonX();
            this.cmbjointable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmblayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkmatch = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkall = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.gPanelouttable.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbllayertable
            // 
            // 
            // 
            // 
            this.lbllayertable.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbllayertable.Location = new System.Drawing.Point(18, 26);
            this.lbllayertable.Name = "lbllayertable";
            this.lbllayertable.Size = new System.Drawing.Size(72, 23);
            this.lbllayertable.TabIndex = 0;
            this.lbllayertable.Text = "连接字段 ：";
            // 
            // cmbtable
            // 
            this.cmbtable.DisplayMember = "Text";
            this.cmbtable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbtable.FormattingEnabled = true;
            this.cmbtable.ItemHeight = 15;
            this.cmbtable.Location = new System.Drawing.Point(96, 26);
            this.cmbtable.Name = "cmbtable";
            this.cmbtable.Size = new System.Drawing.Size(254, 21);
            this.cmbtable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbtable.TabIndex = 1;
            // 
            // gPanelouttable
            // 
            this.gPanelouttable.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelouttable.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelouttable.Controls.Add(this.btnInputShp);
            this.gPanelouttable.Controls.Add(this.cmbjointable);
            this.gPanelouttable.Controls.Add(this.cmblayer);
            this.gPanelouttable.Controls.Add(this.labelX2);
            this.gPanelouttable.Controls.Add(this.labelX1);
            this.gPanelouttable.Location = new System.Drawing.Point(12, 64);
            this.gPanelouttable.Name = "gPanelouttable";
            this.gPanelouttable.Size = new System.Drawing.Size(344, 119);
            // 
            // 
            // 
            this.gPanelouttable.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelouttable.Style.BackColorGradientAngle = 90;
            this.gPanelouttable.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelouttable.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelouttable.Style.BorderBottomWidth = 1;
            this.gPanelouttable.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelouttable.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelouttable.Style.BorderLeftWidth = 1;
            this.gPanelouttable.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelouttable.Style.BorderRightWidth = 1;
            this.gPanelouttable.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelouttable.Style.BorderTopWidth = 1;
            this.gPanelouttable.Style.CornerDiameter = 4;
            this.gPanelouttable.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelouttable.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelouttable.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelouttable.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelouttable.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelouttable.TabIndex = 13;
            this.gPanelouttable.Text = "连接表信息";
            // 
            // btnInputShp
            // 
            this.btnInputShp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInputShp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInputShp.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.btnInputShp.Location = new System.Drawing.Point(305, 12);
            this.btnInputShp.Name = "btnInputShp";
            this.btnInputShp.Size = new System.Drawing.Size(30, 23);
            this.btnInputShp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInputShp.TabIndex = 49;
            this.btnInputShp.Click += new System.EventHandler(this.btnInputShp_Click);
            // 
            // cmbjointable
            // 
            this.cmbjointable.DisplayMember = "Text";
            this.cmbjointable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbjointable.FormattingEnabled = true;
            this.cmbjointable.ItemHeight = 15;
            this.cmbjointable.Location = new System.Drawing.Point(81, 56);
            this.cmbjointable.Name = "cmbjointable";
            this.cmbjointable.Size = new System.Drawing.Size(254, 21);
            this.cmbjointable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbjointable.TabIndex = 3;
            // 
            // cmblayer
            // 
            this.cmblayer.DisplayMember = "Text";
            this.cmblayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmblayer.FormattingEnabled = true;
            this.cmblayer.ItemHeight = 15;
            this.cmblayer.Location = new System.Drawing.Point(81, 12);
            this.cmblayer.Name = "cmblayer";
            this.cmblayer.Size = new System.Drawing.Size(211, 21);
            this.cmblayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmblayer.TabIndex = 2;
            this.cmblayer.TextChanged += new System.EventHandler(this.cmblayer_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(3, 56);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(72, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "连接字段 ：";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "连接图层 ：";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.chkmatch);
            this.groupPanel1.Controls.Add(this.chkall);
            this.groupPanel1.Location = new System.Drawing.Point(12, 203);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(344, 72);
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
            this.groupPanel1.Text = "连接方式";
            // 
            // chkmatch
            // 
            this.chkmatch.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkmatch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkmatch.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkmatch.Location = new System.Drawing.Point(192, 12);
            this.chkmatch.Name = "chkmatch";
            this.chkmatch.Size = new System.Drawing.Size(100, 23);
            this.chkmatch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkmatch.TabIndex = 1;
            this.chkmatch.Text = "保留匹配记录";
            this.chkmatch.CheckedChanged += new System.EventHandler(this.chkmatch_CheckedChanged);
            // 
            // chkall
            // 
            this.chkall.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkall.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkall.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkall.Checked = true;
            this.chkall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkall.CheckValue = "Y";
            this.chkall.Location = new System.Drawing.Point(36, 12);
            this.chkall.Name = "chkall";
            this.chkall.Size = new System.Drawing.Size(100, 23);
            this.chkall.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkall.TabIndex = 0;
            this.chkall.Text = "保留所有记录";
            this.chkall.CheckedChanged += new System.EventHandler(this.chkall_CheckedChanged);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(170, 303);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 26);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 15;
            this.buttonX1.Text = "确定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(281, 303);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 26);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 16;
            this.buttonX2.Text = "取消";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // FrmJoinAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(375, 350);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.gPanelouttable);
            this.Controls.Add(this.cmbtable);
            this.Controls.Add(this.lbllayertable);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmJoinAttribute";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接属性表";
            this.Load += new System.EventHandler(this.FrmJoinAttribute_Load);
            this.gPanelouttable.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lbllayertable;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbtable;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelouttable;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbjointable;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmblayer;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkmatch;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkall;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX btnInputShp;
    }
}