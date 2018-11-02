namespace LibCerMap
{
    partial class FrmCamOriInWorld
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
            this.txtPosX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPosY = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtPosZ = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtOriOmg = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtOriPhi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtOriKap = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtExpAngle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txtPitchAngle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.txtCamFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.cmbDEMlayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.txtBeginYaw = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.txtLastYaw = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.txtPerYaw = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX15 = new DevComponents.DotNetBar.LabelX();
            this.txtPolygonDataset = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnXYZPHK = new DevComponents.DotNetBar.ButtonX();
            this.btnPolygonDataset = new DevComponents.DotNetBar.ButtonX();
            this.BtnWorkBrowse = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(9, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(61, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "北(X)：";
            // 
            // txtPosX
            // 
            // 
            // 
            // 
            this.txtPosX.Border.Class = "TextBoxBorder";
            this.txtPosX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPosX.Location = new System.Drawing.Point(62, 13);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(78, 21);
            this.txtPosX.TabIndex = 1;
            this.txtPosX.TextChanged += new System.EventHandler(this.txtPosX_TextChanged);
            // 
            // txtPosY
            // 
            // 
            // 
            // 
            this.txtPosY.Border.Class = "TextBoxBorder";
            this.txtPosY.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPosY.Location = new System.Drawing.Point(228, 13);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(78, 21);
            this.txtPosY.TabIndex = 3;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(173, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(61, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "东(Y)：";
            // 
            // txtPosZ
            // 
            // 
            // 
            // 
            this.txtPosZ.Border.Class = "TextBoxBorder";
            this.txtPosZ.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPosZ.Location = new System.Drawing.Point(399, 13);
            this.txtPosZ.Name = "txtPosZ";
            this.txtPosZ.Size = new System.Drawing.Size(78, 21);
            this.txtPosZ.TabIndex = 5;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(340, 13);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(61, 23);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "地(Z)：";
            // 
            // txtOriOmg
            // 
            // 
            // 
            // 
            this.txtOriOmg.Border.Class = "TextBoxBorder";
            this.txtOriOmg.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOriOmg.Location = new System.Drawing.Point(62, 45);
            this.txtOriOmg.Name = "txtOriOmg";
            this.txtOriOmg.Size = new System.Drawing.Size(78, 21);
            this.txtOriOmg.TabIndex = 7;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(9, 45);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(61, 23);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "翻滚角：";
            // 
            // txtOriPhi
            // 
            // 
            // 
            // 
            this.txtOriPhi.Border.Class = "TextBoxBorder";
            this.txtOriPhi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOriPhi.Location = new System.Drawing.Point(228, 45);
            this.txtOriPhi.Name = "txtOriPhi";
            this.txtOriPhi.Size = new System.Drawing.Size(78, 21);
            this.txtOriPhi.TabIndex = 9;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(173, 45);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(61, 23);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "俯仰角：";
            // 
            // txtOriKap
            // 
            // 
            // 
            // 
            this.txtOriKap.Border.Class = "TextBoxBorder";
            this.txtOriKap.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOriKap.Location = new System.Drawing.Point(399, 43);
            this.txtOriKap.Name = "txtOriKap";
            this.txtOriKap.Size = new System.Drawing.Size(78, 21);
            this.txtOriKap.TabIndex = 11;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(340, 43);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(61, 23);
            this.labelX6.TabIndex = 10;
            this.labelX6.Text = "偏航角 ：";
            // 
            // txtExpAngle
            // 
            // 
            // 
            // 
            this.txtExpAngle.Border.Class = "TextBoxBorder";
            this.txtExpAngle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtExpAngle.Location = new System.Drawing.Point(62, 3);
            this.txtExpAngle.Name = "txtExpAngle";
            this.txtExpAngle.Size = new System.Drawing.Size(78, 21);
            this.txtExpAngle.TabIndex = 13;
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(10, 3);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(60, 23);
            this.labelX7.TabIndex = 12;
            this.labelX7.Text = "展开角 ：";
            // 
            // txtPitchAngle
            // 
            // 
            // 
            // 
            this.txtPitchAngle.Border.Class = "TextBoxBorder";
            this.txtPitchAngle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPitchAngle.Location = new System.Drawing.Point(228, 3);
            this.txtPitchAngle.Name = "txtPitchAngle";
            this.txtPitchAngle.Size = new System.Drawing.Size(75, 21);
            this.txtPitchAngle.TabIndex = 17;
            // 
            // labelX9
            // 
            this.labelX9.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(173, 3);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(60, 23);
            this.labelX9.TabIndex = 16;
            this.labelX9.Text = "俯仰角 ：";
            // 
            // txtCamFilePath
            // 
            // 
            // 
            // 
            this.txtCamFilePath.Border.Class = "TextBoxBorder";
            this.txtCamFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCamFilePath.Location = new System.Drawing.Point(90, 461);
            this.txtCamFilePath.Name = "txtCamFilePath";
            this.txtCamFilePath.Size = new System.Drawing.Size(309, 21);
            this.txtCamFilePath.TabIndex = 32;
            this.txtCamFilePath.Text = "1";
            // 
            // labelX10
            // 
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(12, 461);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(75, 23);
            this.labelX10.TabIndex = 31;
            this.labelX10.Text = "配置文件 ：";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonX1.Location = new System.Drawing.Point(343, 398);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 34;
            this.buttonX1.Text = "确定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(439, 398);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 35;
            this.buttonX2.Text = "取消";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // cmbDEMlayer
            // 
            this.cmbDEMlayer.DisplayMember = "Text";
            this.cmbDEMlayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDEMlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDEMlayer.FormattingEnabled = true;
            this.cmbDEMlayer.ItemHeight = 17;
            this.cmbDEMlayer.Location = new System.Drawing.Point(102, 12);
            this.cmbDEMlayer.Name = "cmbDEMlayer";
            this.cmbDEMlayer.Size = new System.Drawing.Size(412, 23);
            this.cmbDEMlayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDEMlayer.TabIndex = 36;
            this.cmbDEMlayer.SelectedIndexChanged += new System.EventHandler(this.cmbDEMlayer_SelectedIndexChanged);
            // 
            // labelX11
            // 
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(14, 12);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(104, 20);
            this.labelX11.TabIndex = 37;
            this.labelX11.Text = "原始DEM图层：";
            // 
            // txtBeginYaw
            // 
            // 
            // 
            // 
            this.txtBeginYaw.Border.Class = "TextBoxBorder";
            this.txtBeginYaw.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBeginYaw.Location = new System.Drawing.Point(62, 32);
            this.txtBeginYaw.Name = "txtBeginYaw";
            this.txtBeginYaw.Size = new System.Drawing.Size(78, 21);
            this.txtBeginYaw.TabIndex = 41;
            // 
            // labelX13
            // 
            this.labelX13.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(10, 32);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(60, 23);
            this.labelX13.TabIndex = 40;
            this.labelX13.Text = "起始角 ：";
            // 
            // txtLastYaw
            // 
            // 
            // 
            // 
            this.txtLastYaw.Border.Class = "TextBoxBorder";
            this.txtLastYaw.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLastYaw.Location = new System.Drawing.Point(228, 32);
            this.txtLastYaw.Name = "txtLastYaw";
            this.txtLastYaw.Size = new System.Drawing.Size(76, 21);
            this.txtLastYaw.TabIndex = 43;
            // 
            // labelX14
            // 
            this.labelX14.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Location = new System.Drawing.Point(173, 32);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(60, 23);
            this.labelX14.TabIndex = 42;
            this.labelX14.Text = "终止角 ：";
            // 
            // txtPerYaw
            // 
            // 
            // 
            // 
            this.txtPerYaw.Border.Class = "TextBoxBorder";
            this.txtPerYaw.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPerYaw.Location = new System.Drawing.Point(399, 32);
            this.txtPerYaw.Name = "txtPerYaw";
            this.txtPerYaw.Size = new System.Drawing.Size(75, 21);
            this.txtPerYaw.TabIndex = 45;
            // 
            // labelX15
            // 
            this.labelX15.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX15.Location = new System.Drawing.Point(340, 32);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new System.Drawing.Size(60, 23);
            this.labelX15.TabIndex = 44;
            this.labelX15.Text = "角间隔 ：";
            // 
            // txtPolygonDataset
            // 
            // 
            // 
            // 
            this.txtPolygonDataset.Border.Class = "TextBoxBorder";
            this.txtPolygonDataset.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPolygonDataset.Location = new System.Drawing.Point(10, 11);
            this.txtPolygonDataset.Name = "txtPolygonDataset";
            this.txtPolygonDataset.Size = new System.Drawing.Size(428, 21);
            this.txtPolygonDataset.TabIndex = 47;
            // 
            // btnXYZPHK
            // 
            this.btnXYZPHK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXYZPHK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXYZPHK.Location = new System.Drawing.Point(391, 82);
            this.btnXYZPHK.Name = "btnXYZPHK";
            this.btnXYZPHK.Size = new System.Drawing.Size(86, 22);
            this.btnXYZPHK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXYZPHK.TabIndex = 49;
            this.btnXYZPHK.Text = "全局段获取";
            this.btnXYZPHK.Click += new System.EventHandler(this.btnXYZPHK_Click);
            // 
            // btnPolygonDataset
            // 
            this.btnPolygonDataset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPolygonDataset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPolygonDataset.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.btnPolygonDataset.Location = new System.Drawing.Point(444, 11);
            this.btnPolygonDataset.Name = "btnPolygonDataset";
            this.btnPolygonDataset.Size = new System.Drawing.Size(30, 23);
            this.btnPolygonDataset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPolygonDataset.TabIndex = 48;
            this.btnPolygonDataset.Click += new System.EventHandler(this.btnPolygonDataset_Click);
            // 
            // BtnWorkBrowse
            // 
            this.BtnWorkBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnWorkBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnWorkBrowse.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.BtnWorkBrowse.Location = new System.Drawing.Point(405, 461);
            this.BtnWorkBrowse.Name = "BtnWorkBrowse";
            this.BtnWorkBrowse.Size = new System.Drawing.Size(30, 23);
            this.BtnWorkBrowse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnWorkBrowse.TabIndex = 33;
            this.BtnWorkBrowse.Click += new System.EventHandler(this.BtnWorkBrowse_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtPosX);
            this.groupPanel1.Controls.Add(this.txtPosY);
            this.groupPanel1.Controls.Add(this.txtPosZ);
            this.groupPanel1.Controls.Add(this.txtOriOmg);
            this.groupPanel1.Controls.Add(this.txtOriPhi);
            this.groupPanel1.Controls.Add(this.txtOriKap);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.btnXYZPHK);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Location = new System.Drawing.Point(14, 41);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(502, 131);
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
            this.groupPanel1.TabIndex = 50;
            this.groupPanel1.Text = "巡视器位置姿态";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.txtLastYaw);
            this.groupPanel2.Controls.Add(this.txtExpAngle);
            this.groupPanel2.Controls.Add(this.txtBeginYaw);
            this.groupPanel2.Controls.Add(this.txtPitchAngle);
            this.groupPanel2.Controls.Add(this.labelX7);
            this.groupPanel2.Controls.Add(this.labelX9);
            this.groupPanel2.Controls.Add(this.labelX13);
            this.groupPanel2.Controls.Add(this.txtPerYaw);
            this.groupPanel2.Controls.Add(this.labelX14);
            this.groupPanel2.Controls.Add(this.labelX15);
            this.groupPanel2.Location = new System.Drawing.Point(14, 182);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(502, 104);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 51;
            this.groupPanel2.Text = "桅杆机构";
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.txtPolygonDataset);
            this.groupPanel3.Controls.Add(this.btnPolygonDataset);
            this.groupPanel3.Location = new System.Drawing.Point(14, 293);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(502, 81);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 52;
            this.groupPanel3.Text = "输出结果:";
            // 
            // FrmCamOriInWorld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(529, 433);
            this.Controls.Add(this.groupPanel3);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.cmbDEMlayer);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.BtnWorkBrowse);
            this.Controls.Add(this.txtCamFilePath);
            this.Controls.Add(this.labelX10);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCamOriInWorld";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "环拍可视区域分析";
            this.Load += new System.EventHandler(this.FrmCamOriInWorld_Load);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPosX;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPosY;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPosZ;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOriOmg;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOriPhi;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOriKap;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExpAngle;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPitchAngle;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.ButtonX BtnWorkBrowse;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCamFilePath;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDEMlayer;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBeginYaw;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLastYaw;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPerYaw;
        private DevComponents.DotNetBar.LabelX labelX15;
        private DevComponents.DotNetBar.ButtonX btnPolygonDataset;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPolygonDataset;
        private DevComponents.DotNetBar.ButtonX btnXYZPHK;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
    }
}