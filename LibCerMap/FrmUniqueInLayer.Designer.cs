namespace LibCerMap
{
    partial class FrmUniqueInLayer
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
            this.cmbInlayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.grpanelfield = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbfield3 = new System.Windows.Forms.ComboBox();
            this.cmbfield2 = new System.Windows.Forms.ComboBox();
            this.cmbfield1 = new System.Windows.Forms.ComboBox();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.grpanelfield.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbInlayer
            // 
            this.cmbInlayer.DisplayMember = "Text";
            this.cmbInlayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbInlayer.FormattingEnabled = true;
            this.cmbInlayer.ItemHeight = 15;
            this.cmbInlayer.Location = new System.Drawing.Point(103, 25);
            this.cmbInlayer.Name = "cmbInlayer";
            this.cmbInlayer.Size = new System.Drawing.Size(142, 21);
            this.cmbInlayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbInlayer.TabIndex = 0;
            this.cmbInlayer.SelectedIndexChanged += new System.EventHandler(this.cmbInlayer_SelectedIndexChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(28, 25);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 21);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "符号图层 ：";
            // 
            // grpanelfield
            // 
            this.grpanelfield.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpanelfield.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grpanelfield.Controls.Add(this.cmbfield3);
            this.grpanelfield.Controls.Add(this.cmbfield2);
            this.grpanelfield.Controls.Add(this.cmbfield1);
            this.grpanelfield.Location = new System.Drawing.Point(28, 72);
            this.grpanelfield.Name = "grpanelfield";
            this.grpanelfield.Size = new System.Drawing.Size(223, 136);
            // 
            // 
            // 
            this.grpanelfield.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpanelfield.Style.BackColorGradientAngle = 90;
            this.grpanelfield.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpanelfield.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelfield.Style.BorderBottomWidth = 1;
            this.grpanelfield.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpanelfield.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelfield.Style.BorderLeftWidth = 1;
            this.grpanelfield.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelfield.Style.BorderRightWidth = 1;
            this.grpanelfield.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelfield.Style.BorderTopWidth = 1;
            this.grpanelfield.Style.CornerDiameter = 4;
            this.grpanelfield.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpanelfield.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpanelfield.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpanelfield.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpanelfield.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpanelfield.TabIndex = 24;
            this.grpanelfield.Text = "字段";
            // 
            // cmbfield3
            // 
            this.cmbfield3.FormattingEnabled = true;
            this.cmbfield3.Location = new System.Drawing.Point(2, 80);
            this.cmbfield3.Name = "cmbfield3";
            this.cmbfield3.Size = new System.Drawing.Size(212, 20);
            this.cmbfield3.TabIndex = 2;
            // 
            // cmbfield2
            // 
            this.cmbfield2.FormattingEnabled = true;
            this.cmbfield2.Location = new System.Drawing.Point(2, 46);
            this.cmbfield2.Name = "cmbfield2";
            this.cmbfield2.Size = new System.Drawing.Size(212, 20);
            this.cmbfield2.TabIndex = 1;
            // 
            // cmbfield1
            // 
            this.cmbfield1.FormattingEnabled = true;
            this.cmbfield1.Location = new System.Drawing.Point(2, 12);
            this.cmbfield1.Name = "cmbfield1";
            this.cmbfield1.Size = new System.Drawing.Size(212, 20);
            this.cmbfield1.TabIndex = 0;
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(50, 233);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(68, 24);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 25;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancle.Location = new System.Drawing.Point(157, 233);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(68, 24);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 26;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // FrmUniqueInLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 275);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.grpanelfield);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cmbInlayer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUniqueInLayer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入图层符号";
            this.Load += new System.EventHandler(this.FrmUniqueInLayer_Load);
            this.grpanelfield.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbInlayer;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.GroupPanel grpanelfield;
        private System.Windows.Forms.ComboBox cmbfield3;
        private System.Windows.Forms.ComboBox cmbfield2;
        private System.Windows.Forms.ComboBox cmbfield1;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
    }
}