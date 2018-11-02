namespace LibCerMap
{
    partial class FrmCreateShpFromFile
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmbReference = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.richTextFile = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.btnSelect = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.buttonXPath = new DevComponents.DotNetBar.ButtonX();
            this.txtFileName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(6, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(67, 23);
            this.labelX1.TabIndex = 8;
            this.labelX1.Text = "文 件 名：";
            // 
            // cmbReference
            // 
            this.cmbReference.DisplayMember = "Text";
            this.cmbReference.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbReference.FormattingEnabled = true;
            this.cmbReference.ItemHeight = 15;
            this.cmbReference.Location = new System.Drawing.Point(79, 244);
            this.cmbReference.Name = "cmbReference";
            this.cmbReference.Size = new System.Drawing.Size(367, 21);
            this.cmbReference.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbReference.TabIndex = 14;
            this.cmbReference.SelectedIndexChanged += new System.EventHandler(this.cmbReference_SelectedIndexChanged);
            // 
            // richTextFile
            // 
            // 
            // 
            // 
            this.richTextFile.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextFile.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextFile.Location = new System.Drawing.Point(79, 53);
            this.richTextFile.Name = "richTextFile";
            this.richTextFile.Size = new System.Drawing.Size(367, 172);
            this.richTextFile.TabIndex = 13;
            // 
            // btnSelect
            // 
            this.btnSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelect.Location = new System.Drawing.Point(452, 53);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(60, 23);
            this.btnSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelect.TabIndex = 12;
            this.btnSelect.Text = "打开";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 243);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(67, 23);
            this.labelX2.TabIndex = 6;
            this.labelX2.Tag = "";
            this.labelX2.Text = "空间参考：";
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImport.Location = new System.Drawing.Point(452, 242);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(60, 23);
            this.btnImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImport.TabIndex = 11;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(6, 53);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(67, 23);
            this.labelX3.TabIndex = 7;
            this.labelX3.Tag = "";
            this.labelX3.Text = "文件内容：";
            // 
            // buttonXPath
            // 
            this.buttonXPath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXPath.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXPath.Location = new System.Drawing.Point(452, 11);
            this.buttonXPath.Name = "buttonXPath";
            this.buttonXPath.Size = new System.Drawing.Size(60, 23);
            this.buttonXPath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXPath.TabIndex = 10;
            this.buttonXPath.Text = "新建";
            this.buttonXPath.Click += new System.EventHandler(this.buttonXPath_Click);
            // 
            // txtFileName
            // 
            // 
            // 
            // 
            this.txtFileName.Border.Class = "TextBoxBorder";
            this.txtFileName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFileName.Location = new System.Drawing.Point(79, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(367, 21);
            this.txtFileName.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(371, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(265, 297);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmCreateShpFromFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 339);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cmbReference);
            this.Controls.Add(this.richTextFile);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.buttonXPath);
            this.Controls.Add(this.txtFileName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCreateShpFromFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "从文本文件创建点文件";
            this.Load += new System.EventHandler(this.FrmCreateShpFromFile_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbReference;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextFile;
        private DevComponents.DotNetBar.ButtonX btnSelect;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX buttonXPath;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFileName;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
    }
}