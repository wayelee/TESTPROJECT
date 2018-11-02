namespace LibCerMap
{
    partial class FrmAddField
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
            this.lblname = new DevComponents.DotNetBar.LabelX();
            this.txtname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbltype = new DevComponents.DotNetBar.LabelX();
            this.cmbtype = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtLength = new DevComponents.Editors.IntegerInput();
            this.labelScale = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLength)).BeginInit();
            this.SuspendLayout();
            // 
            // lblname
            // 
            this.lblname.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblname.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblname.Location = new System.Drawing.Point(17, 14);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(48, 20);
            this.lblname.TabIndex = 0;
            this.lblname.Text = "名称：";
            // 
            // txtname
            // 
            // 
            // 
            // 
            this.txtname.Border.Class = "TextBoxBorder";
            this.txtname.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtname.Location = new System.Drawing.Point(80, 14);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(163, 21);
            this.txtname.TabIndex = 1;
            // 
            // lbltype
            // 
            this.lbltype.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbltype.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbltype.Location = new System.Drawing.Point(17, 61);
            this.lbltype.Name = "lbltype";
            this.lbltype.Size = new System.Drawing.Size(48, 20);
            this.lbltype.TabIndex = 2;
            this.lbltype.Text = "类型：";
            // 
            // cmbtype
            // 
            this.cmbtype.DisplayMember = "Text";
            this.cmbtype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbtype.FormattingEnabled = true;
            this.cmbtype.ItemHeight = 15;
            this.cmbtype.Location = new System.Drawing.Point(80, 60);
            this.cmbtype.Name = "cmbtype";
            this.cmbtype.Size = new System.Drawing.Size(163, 21);
            this.cmbtype.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbtype.TabIndex = 3;
            this.cmbtype.SelectedIndexChanged += new System.EventHandler(this.cmbtype_SelectedIndexChanged);
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnok.Location = new System.Drawing.Point(106, 194);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(78, 23);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 4;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancle.Location = new System.Drawing.Point(208, 194);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(76, 23);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 5;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtLength);
            this.groupPanel1.Controls.Add(this.txtname);
            this.groupPanel1.Controls.Add(this.lblname);
            this.groupPanel1.Controls.Add(this.labelScale);
            this.groupPanel1.Controls.Add(this.lbltype);
            this.groupPanel1.Controls.Add(this.cmbtype);
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(272, 156);
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
            this.groupPanel1.TabIndex = 6;
            // 
            // txtLength
            // 
            // 
            // 
            // 
            this.txtLength.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtLength.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLength.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtLength.Location = new System.Drawing.Point(80, 104);
            this.txtLength.Name = "txtLength";
            this.txtLength.ShowUpDown = true;
            this.txtLength.Size = new System.Drawing.Size(163, 21);
            this.txtLength.TabIndex = 4;
            this.txtLength.Value = 50;
            // 
            // labelScale
            // 
            this.labelScale.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelScale.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelScale.Location = new System.Drawing.Point(17, 104);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(68, 20);
            this.labelScale.TabIndex = 2;
            this.labelScale.Text = "长度：";
            // 
            // FrmAddField
            // 
            this.AcceptButton = this.btnok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(304, 238);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddField";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加字段";
            this.Load += new System.EventHandler(this.FrmAddField_Load);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblname;
        private DevComponents.DotNetBar.Controls.TextBoxX txtname;
        private DevComponents.DotNetBar.LabelX lbltype;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbtype;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelScale;
        private DevComponents.Editors.IntegerInput txtLength;
    }
}