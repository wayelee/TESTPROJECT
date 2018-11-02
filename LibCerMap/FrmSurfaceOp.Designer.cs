namespace LibCerMap
{
    partial class FrmSurfaceOp
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
            this.cmbTargetRasterLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblTargetRasterLayer = new DevComponents.DotNetBar.LabelItem();
            this.lbltype = new DevComponents.DotNetBar.LabelX();
            this.btnInput = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtOutLayer = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnOutput = new DevComponents.DotNetBar.ButtonX();
            this.cmbOutType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonX_ok = new DevComponents.DotNetBar.ButtonX();
            this.buttonX_cancle = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.slider1 = new DevComponents.DotNetBar.Controls.Slider();
            this.slider2 = new DevComponents.DotNetBar.Controls.Slider();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cmbRenderType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtContour = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbTargetRasterLayer
            // 
            this.cmbTargetRasterLayer.DisplayMember = "Text";
            this.cmbTargetRasterLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTargetRasterLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetRasterLayer.FormattingEnabled = true;
            this.cmbTargetRasterLayer.ItemHeight = 17;
            this.cmbTargetRasterLayer.Location = new System.Drawing.Point(73, 16);
            this.cmbTargetRasterLayer.Name = "cmbTargetRasterLayer";
            this.cmbTargetRasterLayer.Size = new System.Drawing.Size(371, 23);
            this.cmbTargetRasterLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTargetRasterLayer.TabIndex = 2;
            // 
            // lblTargetRasterLayer
            // 
            this.lblTargetRasterLayer.Name = "lblTargetRasterLayer";
            this.lblTargetRasterLayer.Text = "栅格图层";
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
            this.lbltype.Text = "原始图层：";
            // 
            // btnInput
            // 
            this.btnInput.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInput.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInput.Location = new System.Drawing.Point(459, 16);
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
            this.labelX1.Location = new System.Drawing.Point(9, 59);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(71, 20);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "输出图层：";
            // 
            // txtOutLayer
            // 
            // 
            // 
            // 
            this.txtOutLayer.Border.Class = "TextBoxBorder";
            this.txtOutLayer.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutLayer.Location = new System.Drawing.Point(73, 59);
            this.txtOutLayer.Name = "txtOutLayer";
            this.txtOutLayer.Size = new System.Drawing.Size(371, 21);
            this.txtOutLayer.TabIndex = 6;
            this.txtOutLayer.TextChanged += new System.EventHandler(this.txtOutLayer_TextChanged);
            // 
            // btnOutput
            // 
            this.btnOutput.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutput.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutput.Location = new System.Drawing.Point(459, 58);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(33, 23);
            this.btnOutput.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOutput.TabIndex = 7;
            this.btnOutput.Text = "....";
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // cmbOutType
            // 
            this.cmbOutType.DisplayMember = "Text";
            this.cmbOutType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutType.FormattingEnabled = true;
            this.cmbOutType.ItemHeight = 17;
            this.cmbOutType.Location = new System.Drawing.Point(73, 100);
            this.cmbOutType.Name = "cmbOutType";
            this.cmbOutType.Size = new System.Drawing.Size(175, 23);
            this.cmbOutType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbOutType.TabIndex = 8;
            this.cmbOutType.SelectedIndexChanged += new System.EventHandler(this.cmbOutType_SelectedIndexChanged);
            // 
            // buttonX_ok
            // 
            this.buttonX_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_ok.Location = new System.Drawing.Point(329, 251);
            this.buttonX_ok.Name = "buttonX_ok";
            this.buttonX_ok.Size = new System.Drawing.Size(75, 23);
            this.buttonX_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_ok.TabIndex = 9;
            this.buttonX_ok.Text = "应用";
            this.buttonX_ok.Click += new System.EventHandler(this.buttonX_ok_Click);
            // 
            // buttonX_cancle
            // 
            this.buttonX_cancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX_cancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX_cancle.Location = new System.Drawing.Point(421, 251);
            this.buttonX_cancle.Name = "buttonX_cancle";
            this.buttonX_cancle.Size = new System.Drawing.Size(75, 23);
            this.buttonX_cancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX_cancle.TabIndex = 10;
            this.buttonX_cancle.Text = "关闭";
            this.buttonX_cancle.Click += new System.EventHandler(this.buttonX_cancle_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(9, 101);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(71, 20);
            this.labelX2.TabIndex = 11;
            this.labelX2.Text = "分析类型：";
            // 
            // slider1
            // 
            this.slider1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.slider1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider1.LabelPosition = DevComponents.DotNetBar.eSliderLabelPosition.Top;
            this.slider1.LabelWidth = 70;
            this.slider1.Location = new System.Drawing.Point(229, 173);
            this.slider1.Name = "slider1";
            this.slider1.Size = new System.Drawing.Size(215, 43);
            this.slider1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider1.TabIndex = 12;
            this.slider1.Text = "光源方位角";
            this.slider1.Value = 100;
            this.slider1.Visible = false;
            this.slider1.ValueChanged += new System.EventHandler(this.slider1_ValueChanged);
            // 
            // slider2
            // 
            this.slider2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.slider2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider2.LabelPosition = DevComponents.DotNetBar.eSliderLabelPosition.Top;
            this.slider2.LabelWidth = 70;
            this.slider2.Location = new System.Drawing.Point(9, 173);
            this.slider2.Name = "slider2";
            this.slider2.Size = new System.Drawing.Size(214, 43);
            this.slider2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider2.TabIndex = 13;
            this.slider2.Text = "光源高度角";
            this.slider2.Value = 45;
            this.slider2.Visible = false;
            this.slider2.ValueChanged += new System.EventHandler(this.slider2_ValueChanged);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(9, 144);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(71, 21);
            this.labelX3.TabIndex = 15;
            this.labelX3.Text = "晕渲类型：";
            this.labelX3.Visible = false;
            // 
            // cmbRenderType
            // 
            this.cmbRenderType.DisplayMember = "Text";
            this.cmbRenderType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRenderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRenderType.FormattingEnabled = true;
            this.cmbRenderType.ItemHeight = 17;
            this.cmbRenderType.Location = new System.Drawing.Point(73, 142);
            this.cmbRenderType.Name = "cmbRenderType";
            this.cmbRenderType.Size = new System.Drawing.Size(371, 23);
            this.cmbRenderType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbRenderType.TabIndex = 14;
            this.cmbRenderType.Visible = false;
            this.cmbRenderType.SelectedIndexChanged += new System.EventHandler(this.cmbRenderType_SelectedIndexChanged);
            // 
            // txtContour
            // 
            // 
            // 
            // 
            this.txtContour.Border.Class = "TextBoxBorder";
            this.txtContour.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtContour.Location = new System.Drawing.Point(73, 144);
            this.txtContour.Name = "txtContour";
            this.txtContour.Size = new System.Drawing.Size(371, 21);
            this.txtContour.TabIndex = 16;
            this.txtContour.Visible = false;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(9, 144);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(86, 21);
            this.labelX4.TabIndex = 17;
            this.labelX4.Text = "等值线间距：";
            this.labelX4.Visible = false;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(276, 101);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(86, 20);
            this.labelX5.TabIndex = 19;
            this.labelX5.Text = "Z_Factor:";
            // 
            // textBoxX3
            // 
            // 
            // 
            // 
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX3.Location = new System.Drawing.Point(337, 101);
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.Size = new System.Drawing.Size(107, 21);
            this.textBoxX3.TabIndex = 18;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtContour);
            this.groupPanel1.Controls.Add(this.cmbTargetRasterLayer);
            this.groupPanel1.Controls.Add(this.txtOutLayer);
            this.groupPanel1.Controls.Add(this.cmbOutType);
            this.groupPanel1.Controls.Add(this.textBoxX3);
            this.groupPanel1.Controls.Add(this.lbltype);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.btnInput);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.btnOutput);
            this.groupPanel1.Controls.Add(this.slider2);
            this.groupPanel1.Controls.Add(this.slider1);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.cmbRenderType);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(551, 234);
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
            this.groupPanel1.TabIndex = 20;
            // 
            // FrmSurfaceOp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 302);
            this.Controls.Add(this.buttonX_cancle);
            this.Controls.Add(this.buttonX_ok);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSurfaceOp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "表面分析";
            this.Load += new System.EventHandler(this.FrmSurfaceOp_Load);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTargetRasterLayer;
        private DevComponents.DotNetBar.LabelItem lblTargetRasterLayer;
        private DevComponents.DotNetBar.LabelX lbltype;
        private DevComponents.DotNetBar.ButtonX btnInput;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutLayer;
        private DevComponents.DotNetBar.ButtonX btnOutput;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbOutType;
        private DevComponents.DotNetBar.ButtonX buttonX_ok;
        private DevComponents.DotNetBar.ButtonX buttonX_cancle;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.Slider slider1;
        private DevComponents.DotNetBar.Controls.Slider slider2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbRenderType;
        private DevComponents.DotNetBar.Controls.TextBoxX txtContour;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
    }
}