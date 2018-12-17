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
            this.gPanelLine.Location = new System.Drawing.Point(12, 116);
            this.gPanelLine.Name = "gPanelLine";
            this.gPanelLine.Size = new System.Drawing.Size(369, 98);
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
            this.comboBoxExIMULayer.Location = new System.Drawing.Point(22, 19);
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
            this.gPanelPoint.Location = new System.Drawing.Point(12, 13);
            this.gPanelPoint.Name = "gPanelPoint";
            this.gPanelPoint.Size = new System.Drawing.Size(369, 97);
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
            this.cboBoxPointLayer.Location = new System.Drawing.Point(22, 17);
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
            // 
            // listBoxDrawBandFields
            // 
            this.listBoxDrawBandFields.FormattingEnabled = true;
            this.listBoxDrawBandFields.Location = new System.Drawing.Point(256, 257);
            this.listBoxDrawBandFields.Name = "listBoxDrawBandFields";
            this.listBoxDrawBandFields.Size = new System.Drawing.Size(155, 251);
            this.listBoxDrawBandFields.TabIndex = 10;
            // 
            // FrmGeneratePDFReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 520);
            this.Controls.Add(this.listBoxDrawBandFields);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonXRemove);
            this.Controls.Add(this.buttonXAdd);
            this.Controls.Add(this.chartControl1);
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
    }
}