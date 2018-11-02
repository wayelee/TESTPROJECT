namespace LibCerMap
{
    partial class FrmManualSetModelPara
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
            this.dbiDepth = new DevComponents.Editors.DoubleInput();
            this.dbiSize = new DevComponents.Editors.DoubleInput();
            this.dbiGeoY = new DevComponents.Editors.DoubleInput();
            this.dbiGeoX = new DevComponents.Editors.DoubleInput();
            this.lblDepth = new DevComponents.DotNetBar.LabelX();
            this.lblSize = new DevComponents.DotNetBar.LabelX();
            this.lblGeoY = new DevComponents.DotNetBar.LabelX();
            this.lblGeoX = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.lblModelType = new DevComponents.DotNetBar.LabelX();
            this.cmiRock = new DevComponents.Editors.ComboItem();
            this.cmiCrater = new DevComponents.Editors.ComboItem();
            this.cmbModelType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtModelID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblModelID = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dbiDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiGeoY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiGeoX)).BeginInit();
            this.SuspendLayout();
            // 
            // dbiDepth
            // 
            // 
            // 
            // 
            this.dbiDepth.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiDepth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiDepth.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiDepth.Increment = 1D;
            this.dbiDepth.Location = new System.Drawing.Point(94, 159);
            this.dbiDepth.Name = "dbiDepth";
            this.dbiDepth.ShowUpDown = true;
            this.dbiDepth.Size = new System.Drawing.Size(152, 21);
            this.dbiDepth.TabIndex = 15;
            // 
            // dbiSize
            // 
            // 
            // 
            // 
            this.dbiSize.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiSize.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiSize.Increment = 1D;
            this.dbiSize.Location = new System.Drawing.Point(94, 130);
            this.dbiSize.Name = "dbiSize";
            this.dbiSize.ShowUpDown = true;
            this.dbiSize.Size = new System.Drawing.Size(152, 21);
            this.dbiSize.TabIndex = 14;
            // 
            // dbiGeoY
            // 
            // 
            // 
            // 
            this.dbiGeoY.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiGeoY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiGeoY.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiGeoY.Increment = 1D;
            this.dbiGeoY.Location = new System.Drawing.Point(94, 101);
            this.dbiGeoY.Name = "dbiGeoY";
            this.dbiGeoY.ShowUpDown = true;
            this.dbiGeoY.Size = new System.Drawing.Size(152, 21);
            this.dbiGeoY.TabIndex = 17;
            // 
            // dbiGeoX
            // 
            // 
            // 
            // 
            this.dbiGeoX.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiGeoX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiGeoX.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiGeoX.Increment = 1D;
            this.dbiGeoX.Location = new System.Drawing.Point(94, 72);
            this.dbiGeoX.Name = "dbiGeoX";
            this.dbiGeoX.ShowUpDown = true;
            this.dbiGeoX.Size = new System.Drawing.Size(152, 21);
            this.dbiGeoX.TabIndex = 16;
            // 
            // lblDepth
            // 
            // 
            // 
            // 
            this.lblDepth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDepth.Location = new System.Drawing.Point(22, 157);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(66, 23);
            this.lblDepth.TabIndex = 9;
            this.lblDepth.Text = "深度：";
            // 
            // lblSize
            // 
            // 
            // 
            // 
            this.lblSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSize.Location = new System.Drawing.Point(22, 128);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(66, 23);
            this.lblSize.TabIndex = 8;
            this.lblSize.Text = "尺寸：";
            // 
            // lblGeoY
            // 
            // 
            // 
            // 
            this.lblGeoY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblGeoY.Location = new System.Drawing.Point(22, 99);
            this.lblGeoY.Name = "lblGeoY";
            this.lblGeoY.Size = new System.Drawing.Size(66, 23);
            this.lblGeoY.TabIndex = 10;
            this.lblGeoY.Text = "Y坐标:";
            // 
            // lblGeoX
            // 
            // 
            // 
            // 
            this.lblGeoX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblGeoX.Location = new System.Drawing.Point(22, 70);
            this.lblGeoX.Name = "lblGeoX";
            this.lblGeoX.Size = new System.Drawing.Size(66, 23);
            this.lblGeoX.TabIndex = 11;
            this.lblGeoX.Text = "X坐标:";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(171, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "关闭";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(73, 205);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblModelType
            // 
            // 
            // 
            // 
            this.lblModelType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblModelType.Location = new System.Drawing.Point(22, 12);
            this.lblModelType.Name = "lblModelType";
            this.lblModelType.Size = new System.Drawing.Size(66, 23);
            this.lblModelType.TabIndex = 12;
            this.lblModelType.Text = "模型类型：";
            // 
            // cmiRock
            // 
            this.cmiRock.Text = "石块";
            // 
            // cmiCrater
            // 
            this.cmiCrater.Text = "撞击坑";
            // 
            // cmbModelType
            // 
            this.cmbModelType.DisplayMember = "Text";
            this.cmbModelType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbModelType.Enabled = false;
            this.cmbModelType.FormattingEnabled = true;
            this.cmbModelType.ItemHeight = 15;
            this.cmbModelType.Items.AddRange(new object[] {
            this.cmiCrater,
            this.cmiRock});
            this.cmbModelType.Location = new System.Drawing.Point(94, 14);
            this.cmbModelType.Name = "cmbModelType";
            this.cmbModelType.Size = new System.Drawing.Size(152, 21);
            this.cmbModelType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbModelType.TabIndex = 13;
            this.cmbModelType.SelectedIndexChanged += new System.EventHandler(this.cmbModelType_SelectedIndexChanged);
            // 
            // txtModelID
            // 
            // 
            // 
            // 
            this.txtModelID.Border.Class = "TextBoxBorder";
            this.txtModelID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtModelID.Location = new System.Drawing.Point(94, 43);
            this.txtModelID.Name = "txtModelID";
            this.txtModelID.Size = new System.Drawing.Size(136, 21);
            this.txtModelID.TabIndex = 21;
            // 
            // lblModelID
            // 
            // 
            // 
            // 
            this.lblModelID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblModelID.Location = new System.Drawing.Point(22, 41);
            this.lblModelID.Name = "lblModelID";
            this.lblModelID.Size = new System.Drawing.Size(66, 23);
            this.lblModelID.TabIndex = 20;
            this.lblModelID.Text = "模型ID：";
            // 
            // FrmManualSetModelPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 242);
            this.Controls.Add(this.txtModelID);
            this.Controls.Add(this.lblModelID);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dbiDepth);
            this.Controls.Add(this.dbiSize);
            this.Controls.Add(this.dbiGeoY);
            this.Controls.Add(this.dbiGeoX);
            this.Controls.Add(this.cmbModelType);
            this.Controls.Add(this.lblDepth);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblGeoY);
            this.Controls.Add(this.lblModelType);
            this.Controls.Add(this.lblGeoX);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManualSetModelPara";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManualSetModelPara";
            this.Load += new System.EventHandler(this.FrmManualSetModelPara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbiDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiGeoY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiGeoX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.DoubleInput dbiDepth;
        private DevComponents.Editors.DoubleInput dbiSize;
        private DevComponents.Editors.DoubleInput dbiGeoY;
        private DevComponents.Editors.DoubleInput dbiGeoX;
        private DevComponents.DotNetBar.LabelX lblDepth;
        private DevComponents.DotNetBar.LabelX lblSize;
        private DevComponents.DotNetBar.LabelX lblGeoY;
        private DevComponents.DotNetBar.LabelX lblGeoX;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.LabelX lblModelType;
        private DevComponents.Editors.ComboItem cmiRock;
        private DevComponents.Editors.ComboItem cmiCrater;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbModelType;
        private DevComponents.DotNetBar.Controls.TextBoxX txtModelID;
        private DevComponents.DotNetBar.LabelX lblModelID;
    }
}