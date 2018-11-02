namespace LibCerMap
{
    partial class FrmFromXML
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
            this.buttonX_cancle = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_ok = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.TextLine = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.textpolygon = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.textlength = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbImpSymbol = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnImpSymbol = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX_cancle
            // 
            this.buttonX_cancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_cancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonX_cancle.Location = new System.Drawing.Point(370, 311);
            this.buttonX_cancle.Name = "buttonX_cancle";
            this.buttonX_cancle.Size = new System.Drawing.Size(89, 23);
            this.buttonX_cancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_cancle.TabIndex = 20;
            this.buttonX_cancle.Text = "取消";
            this.buttonX_cancle.Click += new System.EventHandler(this.buttonX_cancle_Click);
            // 
            // buttonX_ok
            // 
            this.buttonX_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonX_ok.Location = new System.Drawing.Point(266, 311);
            this.buttonX_ok.Name = "buttonX_ok";
            this.buttonX_ok.Size = new System.Drawing.Size(89, 23);
            this.buttonX_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_ok.TabIndex = 19;
            this.buttonX_ok.Text = "确定";
            this.buttonX_ok.Click += new System.EventHandler(this.buttonX_ok_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.BackColor = System.Drawing.Color.Maroon;
            this.buttonX3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(327, 16);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(103, 22);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 18;
            this.buttonX3.Text = "打开";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(85, 17);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(220, 21);
            this.textBoxX1.TabIndex = 17;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(13, 17);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(71, 20);
            this.labelX2.TabIndex = 16;
            this.labelX2.Text = "输入XML：";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(327, 60);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(50, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 23;
            this.buttonX1.Text = "打开GDB";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.Location = new System.Drawing.Point(85, 61);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(220, 21);
            this.textBoxX2.TabIndex = 22;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 61);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(71, 20);
            this.labelX1.TabIndex = 21;
            this.labelX1.Text = "输出gdb：";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(378, 60);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(50, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 34;
            this.buttonX2.Text = "新建GDB";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 105);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(89, 20);
            this.labelX3.TabIndex = 24;
            this.labelX3.Text = "数据集名称：";
            // 
            // textBoxX3
            // 
            // 
            // 
            // 
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX3.Location = new System.Drawing.Point(85, 105);
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.Size = new System.Drawing.Size(139, 21);
            this.textBoxX3.TabIndex = 25;
            this.textBoxX3.TextChanged += new System.EventHandler(this.textBoxX3_TextChanged);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(259, 105);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(73, 20);
            this.labelX4.TabIndex = 26;
            this.labelX4.Text = "航向角：";
            // 
            // textBoxX4
            // 
            // 
            // 
            // 
            this.textBoxX4.Border.Class = "TextBoxBorder";
            this.textBoxX4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX4.Location = new System.Drawing.Point(315, 105);
            this.textBoxX4.Name = "textBoxX4";
            this.textBoxX4.Size = new System.Drawing.Size(115, 21);
            this.textBoxX4.TabIndex = 27;
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(13, 149);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(89, 20);
            this.labelX8.TabIndex = 28;
            this.labelX8.Text = "输出线符号：";
            // 
            // TextLine
            // 
            // 
            // 
            // 
            this.TextLine.Border.Class = "TextBoxBorder";
            this.TextLine.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TextLine.Enabled = false;
            this.TextLine.Location = new System.Drawing.Point(85, 149);
            this.TextLine.Name = "TextLine";
            this.TextLine.Size = new System.Drawing.Size(139, 21);
            this.TextLine.TabIndex = 29;
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(13, 193);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(89, 20);
            this.labelX7.TabIndex = 30;
            this.labelX7.Text = "输出面符号：";
            // 
            // textpolygon
            // 
            // 
            // 
            // 
            this.textpolygon.Border.Class = "TextBoxBorder";
            this.textpolygon.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textpolygon.Enabled = false;
            this.textpolygon.Location = new System.Drawing.Point(85, 193);
            this.textpolygon.Name = "textpolygon";
            this.textpolygon.Size = new System.Drawing.Size(343, 21);
            this.textpolygon.TabIndex = 31;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(257, 149);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(69, 20);
            this.labelX6.TabIndex = 32;
            this.labelX6.Text = "符号长度：";
            // 
            // textlength
            // 
            // 
            // 
            // 
            this.textlength.Border.Class = "TextBoxBorder";
            this.textlength.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textlength.Location = new System.Drawing.Point(315, 149);
            this.textlength.Name = "textlength";
            this.textlength.Size = new System.Drawing.Size(115, 21);
            this.textlength.TabIndex = 33;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.cmbImpSymbol);
            this.groupPanel1.Controls.Add(this.btnImpSymbol);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.textlength);
            this.groupPanel1.Controls.Add(this.textBoxX4);
            this.groupPanel1.Controls.Add(this.textpolygon);
            this.groupPanel1.Controls.Add(this.TextLine);
            this.groupPanel1.Controls.Add(this.textBoxX3);
            this.groupPanel1.Controls.Add(this.textBoxX2);
            this.groupPanel1.Controls.Add(this.textBoxX1);
            this.groupPanel1.Controls.Add(this.buttonX3);
            this.groupPanel1.Controls.Add(this.buttonX2);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.labelX7);
            this.groupPanel1.Controls.Add(this.buttonX1);
            this.groupPanel1.Controls.Add(this.labelX8);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(447, 278);
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
            this.groupPanel1.TabIndex = 35;
            // 
            // cmbImpSymbol
            // 
            this.cmbImpSymbol.DisplayMember = "Text";
            this.cmbImpSymbol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbImpSymbol.FormattingEnabled = true;
            this.cmbImpSymbol.ItemHeight = 15;
            this.cmbImpSymbol.Location = new System.Drawing.Point(85, 231);
            this.cmbImpSymbol.Name = "cmbImpSymbol";
            this.cmbImpSymbol.Size = new System.Drawing.Size(311, 21);
            this.cmbImpSymbol.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbImpSymbol.TabIndex = 51;
            this.cmbImpSymbol.SelectedIndexChanged += new System.EventHandler(this.cmbImpSymbol_SelectedIndexChanged);
            // 
            // btnImpSymbol
            // 
            this.btnImpSymbol.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImpSymbol.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImpSymbol.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.btnImpSymbol.Location = new System.Drawing.Point(402, 231);
            this.btnImpSymbol.Name = "btnImpSymbol";
            this.btnImpSymbol.Size = new System.Drawing.Size(26, 23);
            this.btnImpSymbol.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImpSymbol.TabIndex = 50;
            this.btnImpSymbol.Click += new System.EventHandler(this.btnImpSymbol_Click);
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(13, 233);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(71, 20);
            this.labelX5.TabIndex = 35;
            this.labelX5.Text = "导入符号：";
            // 
            // FrmFromXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 349);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.buttonX_cancle);
            this.Controls.Add(this.buttonX_ok);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFromXML";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建路径图层";
            this.Load += new System.EventHandler(this.FrmFromXML_Load);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX_cancle;
        private DevComponents.DotNetBar.ButtonX buttonX_ok;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX4;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX TextLine;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX textpolygon;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX textlength;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnImpSymbol;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbImpSymbol;

    }
}