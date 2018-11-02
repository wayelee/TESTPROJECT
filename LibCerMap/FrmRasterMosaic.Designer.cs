namespace LibCerMap
{
    partial class FrmRasterMosaic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRasterMosaic));
            this.textBoxX_output = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX_ok = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.comboBox_colormap = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cmbox_operatortype = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonX_down = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_up = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_sub = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX_input = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbltype = new DevComponents.DotNetBar.LabelX();
            this.btnInput = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnOutput = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_cancle = new DevComponents.DotNetBar.ButtonX();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxX_output
            // 
            // 
            // 
            // 
            this.textBoxX_output.Border.Class = "TextBoxBorder";
            this.textBoxX_output.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX_output.Location = new System.Drawing.Point(84, 224);
            this.textBoxX_output.Name = "textBoxX_output";
            this.textBoxX_output.Size = new System.Drawing.Size(496, 21);
            this.textBoxX_output.TabIndex = 6;
            // 
            // buttonX_ok
            // 
            this.buttonX_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_ok.Location = new System.Drawing.Point(455, 350);
            this.buttonX_ok.Name = "buttonX_ok";
            this.buttonX_ok.Size = new System.Drawing.Size(75, 23);
            this.buttonX_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_ok.TabIndex = 21;
            this.buttonX_ok.Text = "确定";
            this.buttonX_ok.Click += new System.EventHandler(this.buttonX_ok_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.comboBox_colormap);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.cmbox_operatortype);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.listBox1);
            this.groupPanel1.Controls.Add(this.buttonX_down);
            this.groupPanel1.Controls.Add(this.buttonX_up);
            this.groupPanel1.Controls.Add(this.buttonX_sub);
            this.groupPanel1.Controls.Add(this.textBoxX_input);
            this.groupPanel1.Controls.Add(this.textBoxX_output);
            this.groupPanel1.Controls.Add(this.lbltype);
            this.groupPanel1.Controls.Add(this.btnInput);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.btnOutput);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(665, 327);
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
            this.groupPanel1.TabIndex = 23;
            // 
            // comboBox_colormap
            // 
            this.comboBox_colormap.DisplayMember = "Text";
            this.comboBox_colormap.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox_colormap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_colormap.FormattingEnabled = true;
            this.comboBox_colormap.ItemHeight = 17;
            this.comboBox_colormap.Location = new System.Drawing.Point(84, 280);
            this.comboBox_colormap.Name = "comboBox_colormap";
            this.comboBox_colormap.Size = new System.Drawing.Size(496, 23);
            this.comboBox_colormap.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBox_colormap.TabIndex = 17;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(8, 280);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(80, 20);
            this.labelX3.TabIndex = 18;
            this.labelX3.Text = "色彩映射表：";
            // 
            // cmbox_operatortype
            // 
            this.cmbox_operatortype.DisplayMember = "Text";
            this.cmbox_operatortype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbox_operatortype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbox_operatortype.FormattingEnabled = true;
            this.cmbox_operatortype.ItemHeight = 17;
            this.cmbox_operatortype.Location = new System.Drawing.Point(84, 251);
            this.cmbox_operatortype.Name = "cmbox_operatortype";
            this.cmbox_operatortype.Size = new System.Drawing.Size(496, 23);
            this.cmbox_operatortype.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbox_operatortype.TabIndex = 15;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(8, 252);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(71, 20);
            this.labelX2.TabIndex = 16;
            this.labelX2.Text = "镶嵌操作：";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(84, 45);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(496, 172);
            this.listBox1.TabIndex = 14;
            // 
            // buttonX_down
            // 
            this.buttonX_down.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX_down.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_down.Image = ((System.Drawing.Image)(resources.GetObject("buttonX_down.Image")));
            this.buttonX_down.Location = new System.Drawing.Point(586, 118);
            this.buttonX_down.Name = "buttonX_down";
            this.buttonX_down.Size = new System.Drawing.Size(33, 23);
            this.buttonX_down.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_down.TabIndex = 13;
            this.buttonX_down.Click += new System.EventHandler(this.buttonX_down_Click);
            // 
            // buttonX_up
            // 
            this.buttonX_up.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX_up.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_up.Image = ((System.Drawing.Image)(resources.GetObject("buttonX_up.Image")));
            this.buttonX_up.Location = new System.Drawing.Point(586, 82);
            this.buttonX_up.Name = "buttonX_up";
            this.buttonX_up.Size = new System.Drawing.Size(33, 23);
            this.buttonX_up.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_up.TabIndex = 12;
            this.buttonX_up.Click += new System.EventHandler(this.buttonX_up_Click);
            // 
            // buttonX_sub
            // 
            this.buttonX_sub.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_sub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX_sub.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_sub.Image = ((System.Drawing.Image)(resources.GetObject("buttonX_sub.Image")));
            this.buttonX_sub.Location = new System.Drawing.Point(586, 46);
            this.buttonX_sub.Name = "buttonX_sub";
            this.buttonX_sub.Size = new System.Drawing.Size(33, 23);
            this.buttonX_sub.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_sub.TabIndex = 11;
            this.buttonX_sub.Click += new System.EventHandler(this.buttonX_sub_Click);
            // 
            // textBoxX_input
            // 
            // 
            // 
            // 
            this.textBoxX_input.Border.Class = "TextBoxBorder";
            this.textBoxX_input.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX_input.Location = new System.Drawing.Point(84, 17);
            this.textBoxX_input.Name = "textBoxX_input";
            this.textBoxX_input.Size = new System.Drawing.Size(496, 21);
            this.textBoxX_input.TabIndex = 9;
            // 
            // lbltype
            // 
            this.lbltype.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbltype.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbltype.Location = new System.Drawing.Point(9, 17);
            this.lbltype.Name = "lbltype";
            this.lbltype.Size = new System.Drawing.Size(71, 20);
            this.lbltype.TabIndex = 3;
            this.lbltype.Text = "输入栅格：";
            // 
            // btnInput
            // 
            this.btnInput.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInput.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInput.Location = new System.Drawing.Point(586, 16);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(33, 23);
            this.btnInput.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInput.TabIndex = 4;
            this.btnInput.Text = "....";
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(9, 224);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(71, 20);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "输出栅格：";
            // 
            // btnOutput
            // 
            this.btnOutput.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutput.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutput.Location = new System.Drawing.Point(586, 224);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(33, 23);
            this.btnOutput.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOutput.TabIndex = 7;
            this.btnOutput.Text = "....";
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // buttonX_cancle
            // 
            this.buttonX_cancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_cancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_cancle.Location = new System.Drawing.Point(547, 350);
            this.buttonX_cancle.Name = "buttonX_cancle";
            this.buttonX_cancle.Size = new System.Drawing.Size(75, 23);
            this.buttonX_cancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_cancle.TabIndex = 22;
            this.buttonX_cancle.Text = "取消";
            this.buttonX_cancle.Click += new System.EventHandler(this.buttonX_cancle_Click);
            // 
            // labelItem1
            // 
            this.labelItem1.GlobalItem = false;
            this.labelItem1.Name = "labelItem1";
            // 
            // FrmRasterMosaic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 399);
            this.Controls.Add(this.buttonX_ok);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.buttonX_cancle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRasterMosaic";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "栅格镶嵌";
            this.Load += new System.EventHandler(this.FrmRasterMosaic_Load);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX_output;
        private DevComponents.DotNetBar.ButtonX buttonX_ok;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX lbltype;
        private DevComponents.DotNetBar.ButtonX btnInput;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnOutput;
        private DevComponents.DotNetBar.ButtonX buttonX_cancle;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX_input;
        private DevComponents.DotNetBar.ButtonX buttonX_down;
        private DevComponents.DotNetBar.ButtonX buttonX_up;
        private DevComponents.DotNetBar.ButtonX buttonX_sub;
        private System.Windows.Forms.ListBox listBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbox_operatortype;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBox_colormap;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}