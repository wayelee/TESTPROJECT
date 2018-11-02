namespace LibCerMap
{
    partial class FrmprofileGraph
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.lbllength = new DevComponents.DotNetBar.LabelX();
            this.lblZMax = new DevComponents.DotNetBar.LabelX();
            this.lblZMin = new DevComponents.DotNetBar.LabelX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.txtmin = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtmax = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnminmaxok = new DevComponents.DotNetBar.ButtonX();
            this.sliYMin = new DevComponents.DotNetBar.Controls.Slider();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chartArea1.AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chartArea1.AxisY.InterlacedColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 1);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "地形剖面线";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "左轮剖面线";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "右轮剖面线";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(737, 451);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(167, 467);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(71, 27);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "打印图片";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(399, 467);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(71, 27);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 2;
            this.buttonX2.Text = "导出excel";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(285, 467);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(71, 27);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 3;
            this.buttonX3.Text = "保存图片";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // dataGridViewX1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.EnableHeadersVisualStyles = false;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(631, 481);
            this.dataGridViewX1.Name = "dataGridViewX1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.Size = new System.Drawing.Size(13, 12);
            this.dataGridViewX1.TabIndex = 4;
            this.dataGridViewX1.Visible = false;
            // 
            // lbllength
            // 
            this.lbllength.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lbllength.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbllength.Location = new System.Drawing.Point(580, 89);
            this.lbllength.Name = "lbllength";
            this.lbllength.Size = new System.Drawing.Size(151, 26);
            this.lbllength.TabIndex = 7;
            this.lbllength.Text = "线长度：";
            // 
            // lblZMax
            // 
            this.lblZMax.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblZMax.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblZMax.Location = new System.Drawing.Point(580, 120);
            this.lblZMax.Name = "lblZMax";
            this.lblZMax.Size = new System.Drawing.Size(157, 23);
            this.lblZMax.TabIndex = 11;
            this.lblZMax.Text = "labelX1";
            // 
            // lblZMin
            // 
            this.lblZMin.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblZMin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblZMin.Location = new System.Drawing.Point(580, 149);
            this.lblZMin.Name = "lblZMin";
            this.lblZMin.Size = new System.Drawing.Size(157, 23);
            this.lblZMin.TabIndex = 12;
            this.lblZMin.Text = "labelX2";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Location = new System.Drawing.Point(584, 270);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(60, 24);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 13;
            this.buttonX4.Text = "设置Y轴";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // txtmin
            // 
            // 
            // 
            // 
            this.txtmin.Border.Class = "TextBoxBorder";
            this.txtmin.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtmin.Enabled = false;
            this.txtmin.Location = new System.Drawing.Point(631, 309);
            this.txtmin.Name = "txtmin";
            this.txtmin.Size = new System.Drawing.Size(100, 21);
            this.txtmin.TabIndex = 14;
            // 
            // txtmax
            // 
            // 
            // 
            // 
            this.txtmax.Border.Class = "TextBoxBorder";
            this.txtmax.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtmax.Enabled = false;
            this.txtmax.Location = new System.Drawing.Point(631, 345);
            this.txtmax.Name = "txtmax";
            this.txtmax.Size = new System.Drawing.Size(100, 21);
            this.txtmax.TabIndex = 15;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(584, 345);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(45, 23);
            this.labelX1.TabIndex = 16;
            this.labelX1.Text = "最大";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(584, 309);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(45, 23);
            this.labelX2.TabIndex = 17;
            this.labelX2.Text = "最小";
            // 
            // btnminmaxok
            // 
            this.btnminmaxok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnminmaxok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnminmaxok.Location = new System.Drawing.Point(665, 270);
            this.btnminmaxok.Name = "btnminmaxok";
            this.btnminmaxok.Size = new System.Drawing.Size(65, 23);
            this.btnminmaxok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnminmaxok.TabIndex = 18;
            this.btnminmaxok.Text = "确认设置";
            this.btnminmaxok.Click += new System.EventHandler(this.btnminmaxok_Click);
            // 
            // sliYMin
            // 
            this.sliYMin.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.sliYMin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliYMin.Location = new System.Drawing.Point(580, 374);
            this.sliYMin.Name = "sliYMin";
            this.sliYMin.Size = new System.Drawing.Size(150, 23);
            this.sliYMin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliYMin.TabIndex = 19;
            this.sliYMin.Text = "Y轴";
            this.sliYMin.Value = 0;
            this.sliYMin.Visible = false;
            // 
            // FrmprofileGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 501);
            this.Controls.Add(this.sliYMin);
            this.Controls.Add(this.btnminmaxok);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtmax);
            this.Controls.Add(this.txtmin);
            this.Controls.Add(this.buttonX4);
            this.Controls.Add(this.lblZMin);
            this.Controls.Add(this.lblZMax);
            this.Controls.Add(this.lbllength);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.chart1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmprofileGraph";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "剖面图";
            this.Load += new System.EventHandler(this.FrmprofileGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX lbllength;
        private DevComponents.DotNetBar.LabelX lblZMax;
        private DevComponents.DotNetBar.LabelX lblZMin;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtmin;
        private DevComponents.DotNetBar.Controls.TextBoxX txtmax;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnminmaxok;
        private DevComponents.DotNetBar.Controls.Slider sliYMin;

    }
}