namespace LibCerMap
{
    partial class FrmXmlEditor
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
            this.listModelList = new System.Windows.Forms.ListBox();
            this.lblGeoX = new DevComponents.DotNetBar.LabelX();
            this.lblGeoY = new DevComponents.DotNetBar.LabelX();
            this.lblSize = new DevComponents.DotNetBar.LabelX();
            this.lblDepth = new DevComponents.DotNetBar.LabelX();
            this.lblModelType = new DevComponents.DotNetBar.LabelX();
            this.cmbModelType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmiCrater = new DevComponents.Editors.ComboItem();
            this.cmiRock = new DevComponents.Editors.ComboItem();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.lblXmlFilename = new DevComponents.DotNetBar.LabelX();
            this.txtXmlFilename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnXmlFilename = new DevComponents.DotNetBar.ButtonX();
            this.btnSaveAs = new DevComponents.DotNetBar.ButtonX();
            this.dbiGeoX = new DevComponents.Editors.DoubleInput();
            this.dbiGeoY = new DevComponents.Editors.DoubleInput();
            this.dbiSize = new DevComponents.Editors.DoubleInput();
            this.dbiDepth = new DevComponents.Editors.DoubleInput();
            this.lblModelID = new DevComponents.DotNetBar.LabelX();
            this.txtModelID = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dbiGeoX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiGeoY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // listModelList
            // 
            this.listModelList.FormattingEnabled = true;
            this.listModelList.ItemHeight = 12;
            this.listModelList.Location = new System.Drawing.Point(61, 91);
            this.listModelList.Name = "listModelList";
            this.listModelList.Size = new System.Drawing.Size(127, 364);
            this.listModelList.TabIndex = 0;
            this.listModelList.SelectedIndexChanged += new System.EventHandler(this.listModelList_SelectedIndexChanged);
            // 
            // lblGeoX
            // 
            // 
            // 
            // 
            this.lblGeoX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblGeoX.Location = new System.Drawing.Point(236, 169);
            this.lblGeoX.Name = "lblGeoX";
            this.lblGeoX.Size = new System.Drawing.Size(75, 23);
            this.lblGeoX.TabIndex = 1;
            this.lblGeoX.Text = "X坐标:";
            // 
            // lblGeoY
            // 
            // 
            // 
            // 
            this.lblGeoY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblGeoY.Location = new System.Drawing.Point(236, 210);
            this.lblGeoY.Name = "lblGeoY";
            this.lblGeoY.Size = new System.Drawing.Size(75, 23);
            this.lblGeoY.TabIndex = 1;
            this.lblGeoY.Text = "Y坐标:";
            // 
            // lblSize
            // 
            // 
            // 
            // 
            this.lblSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSize.Location = new System.Drawing.Point(236, 252);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(75, 23);
            this.lblSize.TabIndex = 1;
            this.lblSize.Text = "尺寸：";
            // 
            // lblDepth
            // 
            // 
            // 
            // 
            this.lblDepth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDepth.Location = new System.Drawing.Point(236, 297);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(75, 23);
            this.lblDepth.TabIndex = 1;
            this.lblDepth.Text = "深度：";
            // 
            // lblModelType
            // 
            // 
            // 
            // 
            this.lblModelType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblModelType.Location = new System.Drawing.Point(236, 91);
            this.lblModelType.Name = "lblModelType";
            this.lblModelType.Size = new System.Drawing.Size(75, 23);
            this.lblModelType.TabIndex = 1;
            this.lblModelType.Text = "模型类型：";
            // 
            // cmbModelType
            // 
            this.cmbModelType.DisplayMember = "Text";
            this.cmbModelType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbModelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModelType.FormattingEnabled = true;
            this.cmbModelType.ItemHeight = 15;
            this.cmbModelType.Items.AddRange(new object[] {
            this.cmiCrater,
            this.cmiRock});
            this.cmbModelType.Location = new System.Drawing.Point(308, 91);
            this.cmbModelType.Name = "cmbModelType";
            this.cmbModelType.Size = new System.Drawing.Size(121, 21);
            this.cmbModelType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbModelType.TabIndex = 3;
            this.cmbModelType.SelectedIndexChanged += new System.EventHandler(this.cmbModelType_SelectedIndexChanged);
            // 
            // cmiCrater
            // 
            this.cmiCrater.Text = "撞击坑";
            // 
            // cmiRock
            // 
            this.cmiRock.Text = "石块";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(236, 432);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(360, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(236, 369);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Location = new System.Drawing.Point(355, 369);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "增加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnModify.Location = new System.Drawing.Point(471, 369);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnModify.TabIndex = 5;
            this.btnModify.Text = "修改";
            this.btnModify.Visible = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // lblXmlFilename
            // 
            // 
            // 
            // 
            this.lblXmlFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblXmlFilename.Location = new System.Drawing.Point(61, 36);
            this.lblXmlFilename.Name = "lblXmlFilename";
            this.lblXmlFilename.Size = new System.Drawing.Size(75, 23);
            this.lblXmlFilename.TabIndex = 1;
            this.lblXmlFilename.Text = "XML文件：";
            // 
            // txtXmlFilename
            // 
            // 
            // 
            // 
            this.txtXmlFilename.Border.Class = "TextBoxBorder";
            this.txtXmlFilename.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtXmlFilename.Location = new System.Drawing.Point(143, 36);
            this.txtXmlFilename.Name = "txtXmlFilename";
            this.txtXmlFilename.Size = new System.Drawing.Size(244, 21);
            this.txtXmlFilename.TabIndex = 6;
            // 
            // btnXmlFilename
            // 
            this.btnXmlFilename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXmlFilename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXmlFilename.Location = new System.Drawing.Point(402, 36);
            this.btnXmlFilename.Name = "btnXmlFilename";
            this.btnXmlFilename.Size = new System.Drawing.Size(75, 23);
            this.btnXmlFilename.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXmlFilename.TabIndex = 5;
            this.btnXmlFilename.Text = "...";
            this.btnXmlFilename.Click += new System.EventHandler(this.btnXmlFilename_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveAs.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveAs.Location = new System.Drawing.Point(471, 432);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAs.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveAs.TabIndex = 5;
            this.btnSaveAs.Text = "另存为";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
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
            this.dbiGeoX.Location = new System.Drawing.Point(308, 169);
            this.dbiGeoX.Name = "dbiGeoX";
            this.dbiGeoX.ShowUpDown = true;
            this.dbiGeoX.Size = new System.Drawing.Size(112, 21);
            this.dbiGeoX.TabIndex = 7;
            this.dbiGeoX.ValueChanged += new System.EventHandler(this.dbiGeoX_ValueChanged);
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
            this.dbiGeoY.Location = new System.Drawing.Point(308, 212);
            this.dbiGeoY.Name = "dbiGeoY";
            this.dbiGeoY.ShowUpDown = true;
            this.dbiGeoY.Size = new System.Drawing.Size(112, 21);
            this.dbiGeoY.TabIndex = 7;
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
            this.dbiSize.Location = new System.Drawing.Point(308, 254);
            this.dbiSize.Name = "dbiSize";
            this.dbiSize.ShowUpDown = true;
            this.dbiSize.Size = new System.Drawing.Size(112, 21);
            this.dbiSize.TabIndex = 7;
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
            this.dbiDepth.Location = new System.Drawing.Point(308, 297);
            this.dbiDepth.Name = "dbiDepth";
            this.dbiDepth.ShowUpDown = true;
            this.dbiDepth.Size = new System.Drawing.Size(112, 21);
            this.dbiDepth.TabIndex = 7;
            // 
            // lblModelID
            // 
            // 
            // 
            // 
            this.lblModelID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblModelID.Location = new System.Drawing.Point(236, 127);
            this.lblModelID.Name = "lblModelID";
            this.lblModelID.Size = new System.Drawing.Size(75, 23);
            this.lblModelID.TabIndex = 1;
            this.lblModelID.Text = "模型ID：";
            // 
            // txtModelID
            // 
            // 
            // 
            // 
            this.txtModelID.Border.Class = "TextBoxBorder";
            this.txtModelID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtModelID.Location = new System.Drawing.Point(308, 127);
            this.txtModelID.Name = "txtModelID";
            this.txtModelID.Size = new System.Drawing.Size(121, 21);
            this.txtModelID.TabIndex = 8;
            // 
            // FrmXmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 577);
            this.Controls.Add(this.txtModelID);
            this.Controls.Add(this.dbiDepth);
            this.Controls.Add(this.dbiSize);
            this.Controls.Add(this.dbiGeoY);
            this.Controls.Add(this.dbiGeoX);
            this.Controls.Add(this.txtXmlFilename);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnXmlFilename);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbModelType);
            this.Controls.Add(this.lblDepth);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblGeoY);
            this.Controls.Add(this.lblXmlFilename);
            this.Controls.Add(this.lblModelID);
            this.Controls.Add(this.lblModelType);
            this.Controls.Add(this.lblGeoX);
            this.Controls.Add(this.listModelList);
            this.Name = "FrmXmlEditor";
            this.Text = "FrmXmlEditor";
            this.Load += new System.EventHandler(this.FrmXmlEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbiGeoX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiGeoY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbiDepth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listModelList;
        private DevComponents.DotNetBar.LabelX lblGeoX;
        private DevComponents.DotNetBar.LabelX lblGeoY;
        private DevComponents.DotNetBar.LabelX lblSize;
        private DevComponents.DotNetBar.LabelX lblDepth;
        private DevComponents.DotNetBar.LabelX lblModelType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbModelType;
        private DevComponents.Editors.ComboItem cmiCrater;
        private DevComponents.Editors.ComboItem cmiRock;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.LabelX lblXmlFilename;
        private DevComponents.DotNetBar.Controls.TextBoxX txtXmlFilename;
        private DevComponents.DotNetBar.ButtonX btnXmlFilename;
        private DevComponents.DotNetBar.ButtonX btnSaveAs;
        private DevComponents.Editors.DoubleInput dbiGeoX;
        private DevComponents.Editors.DoubleInput dbiGeoY;
        private DevComponents.Editors.DoubleInput dbiSize;
        private DevComponents.Editors.DoubleInput dbiDepth;
        private DevComponents.DotNetBar.LabelX lblModelID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtModelID;

    }
}