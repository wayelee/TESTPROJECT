namespace LibCerMap
{
    partial class FrmGeneratePDFReport
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView3 = new DevExpress.XtraCharts.LineSeriesView();
            this.btOK = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.gPanelLine = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.comboBoxExIMULayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.gPanelPoint = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cboBoxPointLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.buttonXAdd = new DevComponents.DotNetBar.ButtonX();
            this.buttonXRemove = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.listBoxDrawBandFields = new System.Windows.Forms.ListBox();
            this.radioButtonWholeCenterline = new System.Windows.Forms.RadioButton();
            this.radioButtonGivenBeginEndMeasure = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectRangeOnMap = new System.Windows.Forms.RadioButton();
            this.radioButtonMuliple = new System.Windows.Forms.RadioButton();
            this.numericUpDownBeginMeasure1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownEndMeasure1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBeginMeasure2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEndMeasure2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSegmentLength = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownOverlap = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.comboBoxExCenterline = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.gPanelLine.SuspendLayout();
            this.gPanelPoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBeginMeasure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndMeasure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBeginMeasure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndMeasure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSegmentLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOverlap)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOK.Location = new System.Drawing.Point(611, 483);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 25);
            this.btOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOK.TabIndex = 0;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btCancel.Location = new System.Drawing.Point(692, 483);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 25);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gPanelLine
            // 
            this.gPanelLine.BackColor = System.Drawing.Color.Transparent;
            this.gPanelLine.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelLine.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelLine.Controls.Add(this.comboBoxExIMULayer);
            this.gPanelLine.DisabledBackColor = System.Drawing.Color.Empty;
            this.gPanelLine.Location = new System.Drawing.Point(15, 140);
            this.gPanelLine.Name = "gPanelLine";
            this.gPanelLine.Size = new System.Drawing.Size(369, 54);
            // 
            // 
            // 
            this.gPanelLine.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelLine.Style.BackColorGradientAngle = 90;
            this.gPanelLine.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelLine.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderBottomWidth = 1;
            this.gPanelLine.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelLine.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderLeftWidth = 1;
            this.gPanelLine.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderRightWidth = 1;
            this.gPanelLine.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelLine.Style.BorderTopWidth = 1;
            this.gPanelLine.Style.CornerDiameter = 4;
            this.gPanelLine.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelLine.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelLine.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelLine.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelLine.TabIndex = 3;
            this.gPanelLine.Text = "选择内检测点图层";
            // 
            // comboBoxExIMULayer
            // 
            this.comboBoxExIMULayer.DisplayMember = "Text";
            this.comboBoxExIMULayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExIMULayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExIMULayer.FormattingEnabled = true;
            this.comboBoxExIMULayer.ItemHeight = 14;
            this.comboBoxExIMULayer.Location = new System.Drawing.Point(22, 3);
            this.comboBoxExIMULayer.Name = "comboBoxExIMULayer";
            this.comboBoxExIMULayer.Size = new System.Drawing.Size(314, 20);
            this.comboBoxExIMULayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExIMULayer.TabIndex = 1;
            this.comboBoxExIMULayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxExIMULayer_SelectedIndexChanged);
            // 
            // gPanelPoint
            // 
            this.gPanelPoint.BackColor = System.Drawing.Color.Transparent;
            this.gPanelPoint.CanvasColor = System.Drawing.SystemColors.Control;
            this.gPanelPoint.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.gPanelPoint.Controls.Add(this.cboBoxPointLayer);
            this.gPanelPoint.DisabledBackColor = System.Drawing.Color.Empty;
            this.gPanelPoint.Location = new System.Drawing.Point(12, 12);
            this.gPanelPoint.Name = "gPanelPoint";
            this.gPanelPoint.Size = new System.Drawing.Size(369, 54);
            // 
            // 
            // 
            this.gPanelPoint.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gPanelPoint.Style.BackColorGradientAngle = 90;
            this.gPanelPoint.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gPanelPoint.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderBottomWidth = 1;
            this.gPanelPoint.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gPanelPoint.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderLeftWidth = 1;
            this.gPanelPoint.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderRightWidth = 1;
            this.gPanelPoint.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gPanelPoint.Style.BorderTopWidth = 1;
            this.gPanelPoint.Style.CornerDiameter = 4;
            this.gPanelPoint.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gPanelPoint.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.gPanelPoint.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gPanelPoint.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gPanelPoint.TabIndex = 2;
            this.gPanelPoint.Text = "选择中线点图层";
            // 
            // cboBoxPointLayer
            // 
            this.cboBoxPointLayer.DisplayMember = "Text";
            this.cboBoxPointLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBoxPointLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBoxPointLayer.FormattingEnabled = true;
            this.cboBoxPointLayer.ItemHeight = 14;
            this.cboBoxPointLayer.Location = new System.Drawing.Point(22, 3);
            this.cboBoxPointLayer.Name = "cboBoxPointLayer";
            this.cboBoxPointLayer.Size = new System.Drawing.Size(314, 20);
            this.cboBoxPointLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboBoxPointLayer.TabIndex = 1;
            this.cboBoxPointLayer.SelectedIndexChanged += new System.EventHandler(this.cboBoxPointLayer_SelectedIndexChanged);
            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.Margins.Bottom = 0;
            xyDiagram1.Margins.Left = 0;
            xyDiagram1.Margins.Right = 0;
            xyDiagram1.Margins.Top = 0;
            xyDiagram1.PaneDistance = 0;
            secondaryAxisY1.AxisID = 0;
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.VisibleInPanesSerializable = "-1";
            xyDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1});
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(421, 276);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "Series 1";
            series1.View = lineSeriesView1;
            series2.Name = "Series 2";
            series2.View = lineSeriesView2;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.chartControl1.SeriesTemplate.View = lineSeriesView3;
            this.chartControl1.Size = new System.Drawing.Size(369, 179);
            this.chartControl1.TabIndex = 5;
            this.chartControl1.Visible = false;
            // 
            // buttonXAdd
            // 
            this.buttonXAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXAdd.Location = new System.Drawing.Point(176, 313);
            this.buttonXAdd.Name = "buttonXAdd";
            this.buttonXAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonXAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXAdd.TabIndex = 8;
            this.buttonXAdd.Text = "添加>>";
            this.buttonXAdd.Click += new System.EventHandler(this.buttonXAdd_Click);
            // 
            // buttonXRemove
            // 
            this.buttonXRemove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXRemove.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXRemove.Location = new System.Drawing.Point(176, 342);
            this.buttonXRemove.Name = "buttonXRemove";
            this.buttonXRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonXRemove.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXRemove.TabIndex = 8;
            this.buttonXRemove.Text = "移除<<";
            this.buttonXRemove.Click += new System.EventHandler(this.buttonXRemove_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "内检测字段";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "生成报告字段";
            // 
            // listBoxFields
            // 
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.Location = new System.Drawing.Point(15, 250);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(155, 251);
            this.listBoxFields.TabIndex = 10;
            this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDoubleClick);
            // 
            // listBoxDrawBandFields
            // 
            this.listBoxDrawBandFields.FormattingEnabled = true;
            this.listBoxDrawBandFields.Location = new System.Drawing.Point(256, 257);
            this.listBoxDrawBandFields.Name = "listBoxDrawBandFields";
            this.listBoxDrawBandFields.Size = new System.Drawing.Size(155, 251);
            this.listBoxDrawBandFields.TabIndex = 10;
            this.listBoxDrawBandFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxDrawBandFields_MouseDoubleClick);
            // 
            // radioButtonWholeCenterline
            // 
            this.radioButtonWholeCenterline.AutoSize = true;
            this.radioButtonWholeCenterline.Checked = true;
            this.radioButtonWholeCenterline.Location = new System.Drawing.Point(421, 49);
            this.radioButtonWholeCenterline.Name = "radioButtonWholeCenterline";
            this.radioButtonWholeCenterline.Size = new System.Drawing.Size(73, 17);
            this.radioButtonWholeCenterline.TabIndex = 11;
            this.radioButtonWholeCenterline.TabStop = true;
            this.radioButtonWholeCenterline.Text = "整条中线";
            this.radioButtonWholeCenterline.UseVisualStyleBackColor = true;
            // 
            // radioButtonGivenBeginEndMeasure
            // 
            this.radioButtonGivenBeginEndMeasure.AutoSize = true;
            this.radioButtonGivenBeginEndMeasure.Location = new System.Drawing.Point(421, 93);
            this.radioButtonGivenBeginEndMeasure.Name = "radioButtonGivenBeginEndMeasure";
            this.radioButtonGivenBeginEndMeasure.Size = new System.Drawing.Size(109, 17);
            this.radioButtonGivenBeginEndMeasure.TabIndex = 11;
            this.radioButtonGivenBeginEndMeasure.Text = "指定起始结束点";
            this.radioButtonGivenBeginEndMeasure.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelectRangeOnMap
            // 
            this.radioButtonSelectRangeOnMap.AutoSize = true;
            this.radioButtonSelectRangeOnMap.Location = new System.Drawing.Point(421, 140);
            this.radioButtonSelectRangeOnMap.Name = "radioButtonSelectRangeOnMap";
            this.radioButtonSelectRangeOnMap.Size = new System.Drawing.Size(73, 17);
            this.radioButtonSelectRangeOnMap.TabIndex = 11;
            this.radioButtonSelectRangeOnMap.Text = "地图框选";
            this.radioButtonSelectRangeOnMap.UseVisualStyleBackColor = true;
            this.radioButtonSelectRangeOnMap.Visible = false;
            // 
            // radioButtonMuliple
            // 
            this.radioButtonMuliple.AutoSize = true;
            this.radioButtonMuliple.Location = new System.Drawing.Point(421, 194);
            this.radioButtonMuliple.Name = "radioButtonMuliple";
            this.radioButtonMuliple.Size = new System.Drawing.Size(97, 17);
            this.radioButtonMuliple.TabIndex = 11;
            this.radioButtonMuliple.Text = "分段连续输出";
            this.radioButtonMuliple.UseVisualStyleBackColor = true;
            // 
            // numericUpDownBeginMeasure1
            // 
            this.numericUpDownBeginMeasure1.DecimalPlaces = 2;
            this.numericUpDownBeginMeasure1.Location = new System.Drawing.Point(549, 93);
            this.numericUpDownBeginMeasure1.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownBeginMeasure1.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownBeginMeasure1.Name = "numericUpDownBeginMeasure1";
            this.numericUpDownBeginMeasure1.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownBeginMeasure1.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(546, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "起始里程";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(648, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "结束里程";
            // 
            // numericUpDownEndMeasure1
            // 
            this.numericUpDownEndMeasure1.DecimalPlaces = 2;
            this.numericUpDownEndMeasure1.Location = new System.Drawing.Point(651, 93);
            this.numericUpDownEndMeasure1.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownEndMeasure1.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownEndMeasure1.Name = "numericUpDownEndMeasure1";
            this.numericUpDownEndMeasure1.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownEndMeasure1.TabIndex = 12;
            // 
            // numericUpDownBeginMeasure2
            // 
            this.numericUpDownBeginMeasure2.DecimalPlaces = 2;
            this.numericUpDownBeginMeasure2.Location = new System.Drawing.Point(549, 194);
            this.numericUpDownBeginMeasure2.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownBeginMeasure2.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownBeginMeasure2.Name = "numericUpDownBeginMeasure2";
            this.numericUpDownBeginMeasure2.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownBeginMeasure2.TabIndex = 12;
            // 
            // numericUpDownEndMeasure2
            // 
            this.numericUpDownEndMeasure2.DecimalPlaces = 2;
            this.numericUpDownEndMeasure2.Location = new System.Drawing.Point(651, 194);
            this.numericUpDownEndMeasure2.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownEndMeasure2.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.numericUpDownEndMeasure2.Name = "numericUpDownEndMeasure2";
            this.numericUpDownEndMeasure2.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownEndMeasure2.TabIndex = 12;
            // 
            // numericUpDownSegmentLength
            // 
            this.numericUpDownSegmentLength.DecimalPlaces = 2;
            this.numericUpDownSegmentLength.Location = new System.Drawing.Point(549, 250);
            this.numericUpDownSegmentLength.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownSegmentLength.Name = "numericUpDownSegmentLength";
            this.numericUpDownSegmentLength.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownSegmentLength.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(546, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "起始里程";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(648, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "结束里程";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(546, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "分段长度";
            // 
            // numericUpDownOverlap
            // 
            this.numericUpDownOverlap.DecimalPlaces = 2;
            this.numericUpDownOverlap.Location = new System.Drawing.Point(651, 250);
            this.numericUpDownOverlap.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownOverlap.Name = "numericUpDownOverlap";
            this.numericUpDownOverlap.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownOverlap.TabIndex = 12;
            this.numericUpDownOverlap.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(648, 227);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "段间重叠";
            this.label8.Visible = false;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.comboBoxExCenterline);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(15, 77);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(369, 54);
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
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 3;
            this.groupPanel1.Text = "选择中线图层";
            // 
            // comboBoxExCenterline
            // 
            this.comboBoxExCenterline.DisplayMember = "Text";
            this.comboBoxExCenterline.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExCenterline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExCenterline.FormattingEnabled = true;
            this.comboBoxExCenterline.ItemHeight = 14;
            this.comboBoxExCenterline.Location = new System.Drawing.Point(22, 3);
            this.comboBoxExCenterline.Name = "comboBoxExCenterline";
            this.comboBoxExCenterline.Size = new System.Drawing.Size(314, 20);
            this.comboBoxExCenterline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExCenterline.TabIndex = 1;
            this.comboBoxExCenterline.SelectedIndexChanged += new System.EventHandler(this.comboBoxExIMULayer_SelectedIndexChanged);
            // 
            // FrmGeneratePDFReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 520);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownEndMeasure2);
            this.Controls.Add(this.numericUpDownOverlap);
            this.Controls.Add(this.numericUpDownEndMeasure1);
            this.Controls.Add(this.numericUpDownSegmentLength);
            this.Controls.Add(this.numericUpDownBeginMeasure2);
            this.Controls.Add(this.numericUpDownBeginMeasure1);
            this.Controls.Add(this.radioButtonMuliple);
            this.Controls.Add(this.radioButtonSelectRangeOnMap);
            this.Controls.Add(this.radioButtonGivenBeginEndMeasure);
            this.Controls.Add(this.radioButtonWholeCenterline);
            this.Controls.Add(this.listBoxDrawBandFields);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonXRemove);
            this.Controls.Add(this.buttonXAdd);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.gPanelLine);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.gPanelPoint);
            this.Controls.Add(this.btOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGeneratePDFReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层选择";
            this.Load += new System.EventHandler(this.FrmPointToLine_Load);
            this.gPanelLine.ResumeLayout(false);
            this.gPanelPoint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBeginMeasure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndMeasure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBeginMeasure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndMeasure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSegmentLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOverlap)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btOK;
        private DevComponents.DotNetBar.ButtonX btCancel;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelLine;
        private DevComponents.DotNetBar.Controls.GroupPanel gPanelPoint;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboBoxPointLayer;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExIMULayer;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevComponents.DotNetBar.ButtonX buttonXAdd;
        private DevComponents.DotNetBar.ButtonX buttonXRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.ListBox listBoxDrawBandFields;
        private System.Windows.Forms.RadioButton radioButtonWholeCenterline;
        private System.Windows.Forms.RadioButton radioButtonGivenBeginEndMeasure;
        private System.Windows.Forms.RadioButton radioButtonSelectRangeOnMap;
        private System.Windows.Forms.RadioButton radioButtonMuliple;
        private System.Windows.Forms.NumericUpDown numericUpDownBeginMeasure1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownEndMeasure1;
        private System.Windows.Forms.NumericUpDown numericUpDownBeginMeasure2;
        private System.Windows.Forms.NumericUpDown numericUpDownEndMeasure2;
        private System.Windows.Forms.NumericUpDown numericUpDownSegmentLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownOverlap;
        private System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExCenterline;
    }
}