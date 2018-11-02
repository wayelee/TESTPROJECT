namespace LibCerMap
{
    partial class FrmSaveRasterTransformation
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
            this.doubleInputCellSize = new DevComponents.Editors.DoubleInput();
            this.lblcellsize = new DevComponents.DotNetBar.LabelX();
            this.comboBoxExType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.lblresamplemethod = new DevComponents.DotNetBar.LabelX();
            this.textBoxXPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonXPath = new DevComponents.DotNetBar.ButtonX();
            this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputCellSize)).BeginInit();
            this.SuspendLayout();
            // 
            // doubleInputCellSize
            // 
            // 
            // 
            // 
            this.doubleInputCellSize.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputCellSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputCellSize.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputCellSize.Increment = 1D;
            this.doubleInputCellSize.Location = new System.Drawing.Point(93, 21);
            this.doubleInputCellSize.Name = "doubleInputCellSize";
            this.doubleInputCellSize.ShowUpDown = true;
            this.doubleInputCellSize.Size = new System.Drawing.Size(208, 21);
            this.doubleInputCellSize.TabIndex = 0;
            this.doubleInputCellSize.ValueChanged += new System.EventHandler(this.doubleInputCellSize_ValueChanged);
            // 
            // lblcellsize
            // 
            // 
            // 
            // 
            this.lblcellsize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblcellsize.Location = new System.Drawing.Point(12, 21);
            this.lblcellsize.Name = "lblcellsize";
            this.lblcellsize.Size = new System.Drawing.Size(75, 23);
            this.lblcellsize.TabIndex = 1;
            this.lblcellsize.Text = "分辨率：";
            // 
            // comboBoxExType
            // 
            this.comboBoxExType.DisplayMember = "Text";
            this.comboBoxExType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExType.FormattingEnabled = true;
            this.comboBoxExType.ItemHeight = 15;
            this.comboBoxExType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.comboBoxExType.Location = new System.Drawing.Point(93, 62);
            this.comboBoxExType.Name = "comboBoxExType";
            this.comboBoxExType.Size = new System.Drawing.Size(208, 21);
            this.comboBoxExType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExType.TabIndex = 2;
            this.comboBoxExType.SelectedIndexChanged += new System.EventHandler(this.comboBoxExType_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "最邻近插值";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "双线性插值";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "立方卷积";
            // 
            // lblresamplemethod
            // 
            // 
            // 
            // 
            this.lblresamplemethod.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblresamplemethod.Location = new System.Drawing.Point(12, 62);
            this.lblresamplemethod.Name = "lblresamplemethod";
            this.lblresamplemethod.Size = new System.Drawing.Size(82, 23);
            this.lblresamplemethod.TabIndex = 1;
            this.lblresamplemethod.Text = "重采样方法：";
            // 
            // textBoxXPath
            // 
            // 
            // 
            // 
            this.textBoxXPath.Border.Class = "TextBoxBorder";
            this.textBoxXPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXPath.Location = new System.Drawing.Point(93, 112);
            this.textBoxXPath.Name = "textBoxXPath";
            this.textBoxXPath.Size = new System.Drawing.Size(208, 21);
            this.textBoxXPath.TabIndex = 3;
            this.textBoxXPath.TextChanged += new System.EventHandler(this.textBoxXPath_TextChanged);
            // 
            // buttonXPath
            // 
            this.buttonXPath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXPath.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXPath.Location = new System.Drawing.Point(308, 109);
            this.buttonXPath.Name = "buttonXPath";
            this.buttonXPath.Size = new System.Drawing.Size(35, 23);
            this.buttonXPath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXPath.TabIndex = 4;
            this.buttonXPath.Text = "...";
            this.buttonXPath.Click += new System.EventHandler(this.buttonXPath_Click);
            // 
            // buttonXSave
            // 
            this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXSave.Location = new System.Drawing.Point(268, 158);
            this.buttonXSave.Name = "buttonXSave";
            this.buttonXSave.Size = new System.Drawing.Size(75, 23);
            this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXSave.TabIndex = 5;
            this.buttonXSave.Text = "确定";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 110);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(82, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "输出文件：";
            // 
            // FrmSaveRasterTransformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 194);
            this.Controls.Add(this.buttonXSave);
            this.Controls.Add(this.buttonXPath);
            this.Controls.Add(this.textBoxXPath);
            this.Controls.Add(this.comboBoxExType);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblresamplemethod);
            this.Controls.Add(this.lblcellsize);
            this.Controls.Add(this.doubleInputCellSize);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSaveRasterTransformation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保存";
            this.Load += new System.EventHandler(this.FrmSaveRasterTransformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputCellSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.DoubleInput doubleInputCellSize;
        private DevComponents.DotNetBar.LabelX lblcellsize;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.DotNetBar.LabelX lblresamplemethod;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXPath;
        private DevComponents.DotNetBar.ButtonX buttonXPath;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}