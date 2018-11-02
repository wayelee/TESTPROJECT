namespace LibCerMap
{
    partial class FrmCreateShapeFile
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
            this.comboBoxExShapeType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.labelXShapeType = new DevComponents.DotNetBar.LabelX();
            this.textBoxXPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXPath = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.btnSelect = new DevComponents.DotNetBar.ButtonX();
            this.richTextReference = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.cmbReference = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxExShapeType
            // 
            this.comboBoxExShapeType.DisplayMember = "Text";
            this.comboBoxExShapeType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExShapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExShapeType.FormattingEnabled = true;
            this.comboBoxExShapeType.ItemHeight = 15;
            this.comboBoxExShapeType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.comboBoxExShapeType.Location = new System.Drawing.Point(84, 47);
            this.comboBoxExShapeType.Name = "comboBoxExShapeType";
            this.comboBoxExShapeType.Size = new System.Drawing.Size(367, 21);
            this.comboBoxExShapeType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExShapeType.TabIndex = 0;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "点";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "线";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "面";
            // 
            // labelXShapeType
            // 
            this.labelXShapeType.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXShapeType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXShapeType.Location = new System.Drawing.Point(11, 46);
            this.labelXShapeType.Name = "labelXShapeType";
            this.labelXShapeType.Size = new System.Drawing.Size(67, 23);
            this.labelXShapeType.TabIndex = 1;
            this.labelXShapeType.Text = "文件类型：";
            // 
            // textBoxXPath
            // 
            // 
            // 
            // 
            this.textBoxXPath.Border.Class = "TextBoxBorder";
            this.textBoxXPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXPath.Location = new System.Drawing.Point(84, 11);
            this.textBoxXPath.Name = "textBoxXPath";
            this.textBoxXPath.Size = new System.Drawing.Size(367, 21);
            this.textBoxXPath.TabIndex = 2;
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(346, 316);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(71, 23);
            this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXOK.TabIndex = 3;
            this.buttonXOK.Text = "确定";
            this.buttonXOK.Click += new System.EventHandler(this.buttonXOK_Click);
            // 
            // buttonXPath
            // 
            this.buttonXPath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXPath.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXPath.Location = new System.Drawing.Point(457, 10);
            this.buttonXPath.Name = "buttonXPath";
            this.buttonXPath.Size = new System.Drawing.Size(60, 23);
            this.buttonXPath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXPath.TabIndex = 3;
            this.buttonXPath.Text = "新建";
            this.buttonXPath.Click += new System.EventHandler(this.buttonXPath_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(11, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(67, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "文 件 名：";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Location = new System.Drawing.Point(449, 316);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(11, 82);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(67, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Tag = "";
            this.labelX2.Text = "空间参考：";
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImport.Location = new System.Drawing.Point(457, 81);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(60, 23);
            this.btnImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelect.Location = new System.Drawing.Point(457, 119);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(60, 23);
            this.btnSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "选择";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // richTextReference
            // 
            // 
            // 
            // 
            this.richTextReference.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextReference.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextReference.Location = new System.Drawing.Point(84, 119);
            this.richTextReference.Name = "richTextReference";
            this.richTextReference.Size = new System.Drawing.Size(367, 172);
            this.richTextReference.TabIndex = 4;
            // 
            // cmbReference
            // 
            this.cmbReference.DisplayMember = "Text";
            this.cmbReference.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbReference.FormattingEnabled = true;
            this.cmbReference.ItemHeight = 15;
            this.cmbReference.Location = new System.Drawing.Point(84, 83);
            this.cmbReference.Name = "cmbReference";
            this.cmbReference.Size = new System.Drawing.Size(367, 21);
            this.cmbReference.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbReference.TabIndex = 5;
            this.cmbReference.SelectedIndexChanged += new System.EventHandler(this.cmbReference_SelectedIndexChanged);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(11, 119);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(67, 23);
            this.labelX3.TabIndex = 1;
            this.labelX3.Tag = "";
            this.labelX3.Text = "详细信息：";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.cmbReference);
            this.groupPanel1.Controls.Add(this.comboBoxExShapeType);
            this.groupPanel1.Controls.Add(this.richTextReference);
            this.groupPanel1.Controls.Add(this.labelXShapeType);
            this.groupPanel1.Controls.Add(this.btnSelect);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.btnImport);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.buttonXPath);
            this.groupPanel1.Controls.Add(this.textBoxXPath);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(587, 302);
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
            // FrmCreateShapeFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 370);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.buttonXOK);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCreateShapeFile";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建Shape文件";
            this.Load += new System.EventHandler(this.FrmCreateShapeFile_Load);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExShapeType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.DotNetBar.LabelX labelXShapeType;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXPath;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXPath;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.ButtonX btnSelect;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextReference;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbReference;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
    }
}