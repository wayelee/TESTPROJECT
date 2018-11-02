namespace LibCerMap
{
    partial class FrmEdit3DFeatures
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
            this.lblY = new DevComponents.DotNetBar.LabelX();
            this.lblX = new DevComponents.DotNetBar.LabelX();
            this.buttonXXDecrease = new DevComponents.DotNetBar.ButtonX();
            this.buttonXXIncrease = new DevComponents.DotNetBar.ButtonX();
            this.buttonXYIncrease = new DevComponents.DotNetBar.ButtonX();
            this.buttonXYDecrease = new DevComponents.DotNetBar.ButtonX();
            this.doubleInputXIncrement = new DevComponents.Editors.DoubleInput();
            this.doubleInputYIncrement = new DevComponents.Editors.DoubleInput();
            this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.lblZ = new DevComponents.DotNetBar.LabelX();
            this.doubleInputZIncrement = new DevComponents.Editors.DoubleInput();
            this.buttonXZDecrease = new DevComponents.DotNetBar.ButtonX();
            this.buttonXZIncrease = new DevComponents.DotNetBar.ButtonX();
            this.groupPanelLocation = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelXZ = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelXY = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelXX = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupPanelSize = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lblScaleCoef = new DevComponents.DotNetBar.LabelX();
            this.dbiScaleCoef = new DevComponents.Editors.DoubleInput();
            this.buttonXBigger = new DevComponents.DotNetBar.ButtonX();
            this.buttonXSmaller = new DevComponents.DotNetBar.ButtonX();
            this.buttonXDelete = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputXIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputYIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputZIncrement)).BeginInit();
            this.groupPanelLocation.SuspendLayout();
            this.groupPanelSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbiScaleCoef)).BeginInit();
            this.SuspendLayout();
            // 
            // lblY
            // 
            this.lblY.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblY.Location = new System.Drawing.Point(30, 56);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(49, 23);
            this.lblY.TabIndex = 0;
            this.lblY.Text = "Y方向：";
            // 
            // lblX
            // 
            this.lblX.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblX.Location = new System.Drawing.Point(30, 13);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(49, 23);
            this.lblX.TabIndex = 0;
            this.lblX.Text = "X方向：";
            // 
            // buttonXXDecrease
            // 
            this.buttonXXDecrease.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXXDecrease.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXXDecrease.Location = new System.Drawing.Point(262, 13);
            this.buttonXXDecrease.Name = "buttonXXDecrease";
            this.buttonXXDecrease.Size = new System.Drawing.Size(75, 23);
            this.buttonXXDecrease.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXXDecrease.TabIndex = 1;
            this.buttonXXDecrease.Text = "X方向减少";
            this.buttonXXDecrease.Click += new System.EventHandler(this.buttonXXDecrease_Click);
            // 
            // buttonXXIncrease
            // 
            this.buttonXXIncrease.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXXIncrease.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXXIncrease.Location = new System.Drawing.Point(170, 13);
            this.buttonXXIncrease.Name = "buttonXXIncrease";
            this.buttonXXIncrease.Size = new System.Drawing.Size(75, 23);
            this.buttonXXIncrease.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXXIncrease.TabIndex = 1;
            this.buttonXXIncrease.Text = "X方向增加";
            this.buttonXXIncrease.Click += new System.EventHandler(this.buttonXXIncrease_Click);
            // 
            // buttonXYIncrease
            // 
            this.buttonXYIncrease.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXYIncrease.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXYIncrease.Location = new System.Drawing.Point(170, 56);
            this.buttonXYIncrease.Name = "buttonXYIncrease";
            this.buttonXYIncrease.Size = new System.Drawing.Size(75, 23);
            this.buttonXYIncrease.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXYIncrease.TabIndex = 1;
            this.buttonXYIncrease.Text = "Y方向增加";
            this.buttonXYIncrease.Click += new System.EventHandler(this.buttonXYIncrease_Click);
            // 
            // buttonXYDecrease
            // 
            this.buttonXYDecrease.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXYDecrease.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXYDecrease.Location = new System.Drawing.Point(262, 56);
            this.buttonXYDecrease.Name = "buttonXYDecrease";
            this.buttonXYDecrease.Size = new System.Drawing.Size(75, 23);
            this.buttonXYDecrease.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXYDecrease.TabIndex = 1;
            this.buttonXYDecrease.Text = "Y方向减少";
            this.buttonXYDecrease.Click += new System.EventHandler(this.buttonXYDecrease_Click);
            // 
            // doubleInputXIncrement
            // 
            // 
            // 
            // 
            this.doubleInputXIncrement.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputXIncrement.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputXIncrement.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputXIncrement.Increment = 1D;
            this.doubleInputXIncrement.Location = new System.Drawing.Point(75, 13);
            this.doubleInputXIncrement.Name = "doubleInputXIncrement";
            this.doubleInputXIncrement.ShowUpDown = true;
            this.doubleInputXIncrement.Size = new System.Drawing.Size(80, 21);
            this.doubleInputXIncrement.TabIndex = 2;
            this.doubleInputXIncrement.Value = 100D;
            // 
            // doubleInputYIncrement
            // 
            // 
            // 
            // 
            this.doubleInputYIncrement.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputYIncrement.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputYIncrement.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputYIncrement.Increment = 1D;
            this.doubleInputYIncrement.Location = new System.Drawing.Point(75, 56);
            this.doubleInputYIncrement.Name = "doubleInputYIncrement";
            this.doubleInputYIncrement.ShowUpDown = true;
            this.doubleInputYIncrement.Size = new System.Drawing.Size(80, 21);
            this.doubleInputYIncrement.TabIndex = 2;
            this.doubleInputYIncrement.Value = 100D;
            // 
            // buttonXSave
            // 
            this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSave.Location = new System.Drawing.Point(188, 349);
            this.buttonXSave.Name = "buttonXSave";
            this.buttonXSave.Size = new System.Drawing.Size(75, 23);
            this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXSave.TabIndex = 3;
            this.buttonXSave.Text = "保存";
            this.buttonXSave.Visible = false;
            this.buttonXSave.Click += new System.EventHandler(this.buttonXSave_Click);
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.Location = new System.Drawing.Point(290, 349);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXCancel.TabIndex = 3;
            this.buttonXCancel.Text = "取消";
            this.buttonXCancel.Visible = false;
            // 
            // lblZ
            // 
            this.lblZ.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblZ.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblZ.Location = new System.Drawing.Point(25, 97);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(49, 23);
            this.lblZ.TabIndex = 0;
            this.lblZ.Text = "Z方向：";
            // 
            // doubleInputZIncrement
            // 
            // 
            // 
            // 
            this.doubleInputZIncrement.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputZIncrement.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputZIncrement.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputZIncrement.Increment = 1D;
            this.doubleInputZIncrement.Location = new System.Drawing.Point(75, 97);
            this.doubleInputZIncrement.Name = "doubleInputZIncrement";
            this.doubleInputZIncrement.ShowUpDown = true;
            this.doubleInputZIncrement.Size = new System.Drawing.Size(80, 21);
            this.doubleInputZIncrement.TabIndex = 2;
            this.doubleInputZIncrement.Value = 1D;
            // 
            // buttonXZDecrease
            // 
            this.buttonXZDecrease.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXZDecrease.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXZDecrease.Location = new System.Drawing.Point(262, 97);
            this.buttonXZDecrease.Name = "buttonXZDecrease";
            this.buttonXZDecrease.Size = new System.Drawing.Size(75, 23);
            this.buttonXZDecrease.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXZDecrease.TabIndex = 4;
            this.buttonXZDecrease.Text = "Z方向减少";
            this.buttonXZDecrease.Click += new System.EventHandler(this.buttonXZDecrease_Click);
            // 
            // buttonXZIncrease
            // 
            this.buttonXZIncrease.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXZIncrease.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXZIncrease.Location = new System.Drawing.Point(170, 97);
            this.buttonXZIncrease.Name = "buttonXZIncrease";
            this.buttonXZIncrease.Size = new System.Drawing.Size(75, 23);
            this.buttonXZIncrease.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXZIncrease.TabIndex = 4;
            this.buttonXZIncrease.Text = "Z方向增加";
            this.buttonXZIncrease.Click += new System.EventHandler(this.buttonXZIncrease_Click);
            // 
            // groupPanelLocation
            // 
            this.groupPanelLocation.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanelLocation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanelLocation.Controls.Add(this.labelXZ);
            this.groupPanelLocation.Controls.Add(this.labelX3);
            this.groupPanelLocation.Controls.Add(this.labelXY);
            this.groupPanelLocation.Controls.Add(this.labelX2);
            this.groupPanelLocation.Controls.Add(this.labelXX);
            this.groupPanelLocation.Controls.Add(this.labelX1);
            this.groupPanelLocation.Controls.Add(this.lblX);
            this.groupPanelLocation.Controls.Add(this.buttonXZIncrease);
            this.groupPanelLocation.Controls.Add(this.lblY);
            this.groupPanelLocation.Controls.Add(this.buttonXZDecrease);
            this.groupPanelLocation.Controls.Add(this.lblZ);
            this.groupPanelLocation.Controls.Add(this.buttonXXDecrease);
            this.groupPanelLocation.Controls.Add(this.buttonXYIncrease);
            this.groupPanelLocation.Controls.Add(this.doubleInputZIncrement);
            this.groupPanelLocation.Controls.Add(this.buttonXXIncrease);
            this.groupPanelLocation.Controls.Add(this.doubleInputYIncrement);
            this.groupPanelLocation.Controls.Add(this.buttonXYDecrease);
            this.groupPanelLocation.Controls.Add(this.doubleInputXIncrement);
            this.groupPanelLocation.Location = new System.Drawing.Point(12, 12);
            this.groupPanelLocation.Name = "groupPanelLocation";
            this.groupPanelLocation.Size = new System.Drawing.Size(360, 225);
            // 
            // 
            // 
            this.groupPanelLocation.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanelLocation.Style.BackColorGradientAngle = 90;
            this.groupPanelLocation.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanelLocation.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanelLocation.Style.BorderBottomWidth = 1;
            this.groupPanelLocation.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanelLocation.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanelLocation.Style.BorderLeftWidth = 1;
            this.groupPanelLocation.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanelLocation.Style.BorderRightWidth = 1;
            this.groupPanelLocation.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanelLocation.Style.BorderTopWidth = 1;
            this.groupPanelLocation.Style.CornerDiameter = 4;
            this.groupPanelLocation.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanelLocation.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanelLocation.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanelLocation.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanelLocation.TabIndex = 5;
            this.groupPanelLocation.Text = "位置";
            this.groupPanelLocation.Visible = false;
            // 
            // labelXZ
            // 
            this.labelXZ.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXZ.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXZ.Location = new System.Drawing.Point(80, 175);
            this.labelXZ.Name = "labelXZ";
            this.labelXZ.Size = new System.Drawing.Size(165, 23);
            this.labelXZ.TabIndex = 0;
            this.labelXZ.Text = ".";
            this.labelXZ.Visible = false;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(25, 175);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(49, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "Z：";
            this.labelX3.Visible = false;
            // 
            // labelXY
            // 
            this.labelXY.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXY.Location = new System.Drawing.Point(80, 152);
            this.labelXY.Name = "labelXY";
            this.labelXY.Size = new System.Drawing.Size(165, 23);
            this.labelXY.TabIndex = 0;
            this.labelXY.Text = ".";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(26, 152);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(49, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "Y：";
            // 
            // labelXX
            // 
            this.labelXX.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelXX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXX.Location = new System.Drawing.Point(81, 132);
            this.labelXX.Name = "labelXX";
            this.labelXX.Size = new System.Drawing.Size(167, 23);
            this.labelXX.TabIndex = 0;
            this.labelXX.Text = ".";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(26, 132);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(49, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "X：";
            // 
            // groupPanelSize
            // 
            this.groupPanelSize.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanelSize.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanelSize.Controls.Add(this.lblScaleCoef);
            this.groupPanelSize.Controls.Add(this.dbiScaleCoef);
            this.groupPanelSize.Controls.Add(this.buttonXBigger);
            this.groupPanelSize.Controls.Add(this.buttonXSmaller);
            this.groupPanelSize.Location = new System.Drawing.Point(7, 243);
            this.groupPanelSize.Name = "groupPanelSize";
            this.groupPanelSize.Size = new System.Drawing.Size(358, 78);
            // 
            // 
            // 
            this.groupPanelSize.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanelSize.Style.BackColorGradientAngle = 90;
            this.groupPanelSize.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanelSize.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanelSize.Style.BorderBottomWidth = 1;
            this.groupPanelSize.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanelSize.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanelSize.Style.BorderLeftWidth = 1;
            this.groupPanelSize.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanelSize.Style.BorderRightWidth = 1;
            this.groupPanelSize.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanelSize.Style.BorderTopWidth = 1;
            this.groupPanelSize.Style.CornerDiameter = 4;
            this.groupPanelSize.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanelSize.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanelSize.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanelSize.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanelSize.TabIndex = 6;
            this.groupPanelSize.Text = "大小";
            // 
            // lblScaleCoef
            // 
            this.lblScaleCoef.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblScaleCoef.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblScaleCoef.Location = new System.Drawing.Point(10, 16);
            this.lblScaleCoef.Name = "lblScaleCoef";
            this.lblScaleCoef.Size = new System.Drawing.Size(64, 23);
            this.lblScaleCoef.TabIndex = 2;
            this.lblScaleCoef.Text = "缩放系数:";
            // 
            // dbiScaleCoef
            // 
            // 
            // 
            // 
            this.dbiScaleCoef.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiScaleCoef.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiScaleCoef.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiScaleCoef.Increment = 0.1D;
            this.dbiScaleCoef.Location = new System.Drawing.Point(80, 16);
            this.dbiScaleCoef.MaxValue = 10D;
            this.dbiScaleCoef.MinValue = 0.1D;
            this.dbiScaleCoef.Name = "dbiScaleCoef";
            this.dbiScaleCoef.ShowUpDown = true;
            this.dbiScaleCoef.Size = new System.Drawing.Size(104, 21);
            this.dbiScaleCoef.TabIndex = 1;
            this.dbiScaleCoef.Value = 1D;
            // 
            // buttonXBigger
            // 
            this.buttonXBigger.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXBigger.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXBigger.Location = new System.Drawing.Point(261, 16);
            this.buttonXBigger.Name = "buttonXBigger";
            this.buttonXBigger.Size = new System.Drawing.Size(75, 23);
            this.buttonXBigger.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXBigger.TabIndex = 0;
            this.buttonXBigger.Text = "缩放";
            this.buttonXBigger.Click += new System.EventHandler(this.buttonXBigger_Click);
            // 
            // buttonXSmaller
            // 
            this.buttonXSmaller.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSmaller.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSmaller.Location = new System.Drawing.Point(173, 16);
            this.buttonXSmaller.Name = "buttonXSmaller";
            this.buttonXSmaller.Size = new System.Drawing.Size(75, 23);
            this.buttonXSmaller.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXSmaller.TabIndex = 0;
            this.buttonXSmaller.Text = "缩小";
            this.buttonXSmaller.Visible = false;
            this.buttonXSmaller.Click += new System.EventHandler(this.buttonXSmaller_Click);
            // 
            // buttonXDelete
            // 
            this.buttonXDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDelete.Location = new System.Drawing.Point(20, 349);
            this.buttonXDelete.Name = "buttonXDelete";
            this.buttonXDelete.Size = new System.Drawing.Size(89, 23);
            this.buttonXDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXDelete.TabIndex = 7;
            this.buttonXDelete.Text = "删除所选物体";
            this.buttonXDelete.Click += new System.EventHandler(this.buttonXDelete_Click);
            // 
            // FrmEdit3DFeatures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 377);
            this.Controls.Add(this.buttonXDelete);
            this.Controls.Add(this.groupPanelSize);
            this.Controls.Add(this.groupPanelLocation);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXSave);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEdit3DFeatures";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑三维物体";
            this.Load += new System.EventHandler(this.FrmEdit3DFeatures_Load);
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputXIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputYIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputZIncrement)).EndInit();
            this.groupPanelLocation.ResumeLayout(false);
            this.groupPanelSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbiScaleCoef)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblY;
        private DevComponents.DotNetBar.LabelX lblX;
        private DevComponents.DotNetBar.ButtonX buttonXXDecrease;
        private DevComponents.DotNetBar.ButtonX buttonXXIncrease;
        private DevComponents.DotNetBar.ButtonX buttonXYIncrease;
        private DevComponents.DotNetBar.ButtonX buttonXYDecrease;
        private DevComponents.Editors.DoubleInput doubleInputXIncrement;
        private DevComponents.Editors.DoubleInput doubleInputYIncrement;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.LabelX lblZ;
        private DevComponents.Editors.DoubleInput doubleInputZIncrement;
        private DevComponents.DotNetBar.ButtonX buttonXZDecrease;
        private DevComponents.DotNetBar.ButtonX buttonXZIncrease;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanelLocation;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanelSize;
        private DevComponents.DotNetBar.ButtonX buttonXBigger;
        private DevComponents.DotNetBar.ButtonX buttonXSmaller;
        private DevComponents.DotNetBar.ButtonX buttonXDelete;
        private DevComponents.DotNetBar.LabelX labelXZ;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelXY;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelXX;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblScaleCoef;
        private DevComponents.Editors.DoubleInput dbiScaleCoef;
    }
}