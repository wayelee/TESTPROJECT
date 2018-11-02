namespace LibCerMap
{
    partial class FrmLabelExpression
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
            this.gPanelfield = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lsbFields = new System.Windows.Forms.ListBox();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.RtbSQL = new System.Windows.Forms.RichTextBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.gPanelfield.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gPanelfield
            // 
            this.gPanelfield.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelfield.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelfield.Controls.Add(this.lsbFields);
            this.gPanelfield.Location = new System.Drawing.Point(4, 5);
            this.gPanelfield.Name = "gPanelfield";
            this.gPanelfield.Size = new System.Drawing.Size(370, 152);
            // 
            // 
            // 
            this.gPanelfield.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelfield.Style.BackColorGradientAngle = 90;
            this.gPanelfield.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelfield.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelfield.Style.BorderBottomWidth = 1;
            this.gPanelfield.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelfield.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelfield.Style.BorderLeftWidth = 1;
            this.gPanelfield.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelfield.Style.BorderRightWidth = 1;
            this.gPanelfield.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelfield.Style.BorderTopWidth = 1;
            this.gPanelfield.Style.CornerDiameter = 4;
            this.gPanelfield.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelfield.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gPanelfield.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gPanelfield.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelfield.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelfield.TabIndex = 16;
            this.gPanelfield.Text = "标注字段";
            // 
            // lsbFields
            // 
            this.lsbFields.ItemHeight = 12;
            this.lsbFields.Location = new System.Drawing.Point(3, 3);
            this.lsbFields.Name = "lsbFields";
            this.lsbFields.Size = new System.Drawing.Size(358, 124);
            this.lsbFields.TabIndex = 3;
            this.lsbFields.DoubleClick += new System.EventHandler(this.lsbFields_DoubleClick);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.RtbSQL);
            this.groupPanel1.Location = new System.Drawing.Point(4, 177);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(370, 152);
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
            this.groupPanel1.TabIndex = 17;
            this.groupPanel1.Text = "表达式";
            // 
            // RtbSQL
            // 
            this.RtbSQL.Location = new System.Drawing.Point(3, 3);
            this.RtbSQL.Name = "RtbSQL";
            this.RtbSQL.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RtbSQL.Size = new System.Drawing.Size(358, 122);
            this.RtbSQL.TabIndex = 19;
            this.RtbSQL.Text = "";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(207, 378);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(69, 27);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 18;
            this.buttonX1.Text = "确定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(292, 378);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(69, 27);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 19;
            this.buttonX2.Text = "取消";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(10, 339);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(357, 17);
            this.labelX1.TabIndex = 20;
            this.labelX1.Text = "提示：可在表达式中字段间的双引号（“”）中间添加分隔符";
            // 
            // FrmLabelExpression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 417);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.gPanelfield);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLabelExpression";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组合标注";
            this.Load += new System.EventHandler(this.FrmLabelExpression_Load);
            this.gPanelfield.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gPanelfield;
        private System.Windows.Forms.ListBox lsbFields;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.RichTextBox RtbSQL;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}