namespace LibCerMap
{
    partial class FrmLinkTableRaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.linkDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xSourceDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ySourceDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xMapDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yMapDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dRms = new System.Data.DataColumn();
            this.comboBoxExType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonXSaveRaster = new DevComponents.DotNetBar.ButtonX();
            this.btnExportControlPoints = new DevComponents.DotNetBar.ButtonX();
            this.btnImportControlPoints = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnHide = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnDelAll = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(3, 9);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(69, 31);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "删除选中点";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.linkDataGridViewTextBoxColumn1,
            this.xSourceDataGridViewTextBoxColumn1,
            this.ySourceDataGridViewTextBoxColumn1,
            this.xMapDataGridViewTextBoxColumn1,
            this.yMapDataGridViewTextBoxColumn1,
            this.RMS});
            this.dataGridViewX1.DataMember = "Table1";
            this.dataGridViewX1.DataSource = this.dataSet1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.EnableHeadersVisualStyles = false;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.HighlightSelectedColumnHeaders = false;
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 0);
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
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(645, 421);
            this.dataGridViewX1.TabIndex = 2;
            this.dataGridViewX1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellValueChanged);
            this.dataGridViewX1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_RowHeaderMouseDoubleClick);
            this.dataGridViewX1.SelectionChanged += new System.EventHandler(this.dataGridViewX1_SelectionChanged);
            this.dataGridViewX1.Validated += new System.EventHandler(this.dataGridViewX1_Validated);
            // 
            // linkDataGridViewTextBoxColumn1
            // 
            this.linkDataGridViewTextBoxColumn1.DataPropertyName = "Link";
            this.linkDataGridViewTextBoxColumn1.HeaderText = "Link";
            this.linkDataGridViewTextBoxColumn1.Name = "linkDataGridViewTextBoxColumn1";
            this.linkDataGridViewTextBoxColumn1.ReadOnly = true;
            this.linkDataGridViewTextBoxColumn1.Width = 60;
            // 
            // xSourceDataGridViewTextBoxColumn1
            // 
            this.xSourceDataGridViewTextBoxColumn1.DataPropertyName = "XSource";
            this.xSourceDataGridViewTextBoxColumn1.HeaderText = "XSource";
            this.xSourceDataGridViewTextBoxColumn1.Name = "xSourceDataGridViewTextBoxColumn1";
            // 
            // ySourceDataGridViewTextBoxColumn1
            // 
            this.ySourceDataGridViewTextBoxColumn1.DataPropertyName = "YSource";
            this.ySourceDataGridViewTextBoxColumn1.HeaderText = "YSource";
            this.ySourceDataGridViewTextBoxColumn1.Name = "ySourceDataGridViewTextBoxColumn1";
            // 
            // xMapDataGridViewTextBoxColumn1
            // 
            this.xMapDataGridViewTextBoxColumn1.DataPropertyName = "XMap";
            this.xMapDataGridViewTextBoxColumn1.HeaderText = "XMap";
            this.xMapDataGridViewTextBoxColumn1.Name = "xMapDataGridViewTextBoxColumn1";
            // 
            // yMapDataGridViewTextBoxColumn1
            // 
            this.yMapDataGridViewTextBoxColumn1.DataPropertyName = "YMap";
            this.yMapDataGridViewTextBoxColumn1.HeaderText = "YMap";
            this.yMapDataGridViewTextBoxColumn1.Name = "yMapDataGridViewTextBoxColumn1";
            // 
            // RMS
            // 
            this.RMS.DataPropertyName = "RMS";
            this.RMS.HeaderText = "RMS";
            this.RMS.Name = "RMS";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dRms});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "Link";
            this.dataColumn1.ColumnName = "Link";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "XSource";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "YSource";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "XMap";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "YMap";
            // 
            // dRms
            // 
            this.dRms.ColumnName = "RMS";
            // 
            // comboBoxExType
            // 
            this.comboBoxExType.DisplayMember = "Text";
            this.comboBoxExType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExType.FormattingEnabled = true;
            this.comboBoxExType.ItemHeight = 15;
            this.comboBoxExType.Location = new System.Drawing.Point(126, 15);
            this.comboBoxExType.Name = "comboBoxExType";
            this.comboBoxExType.Size = new System.Drawing.Size(194, 21);
            this.comboBoxExType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExType.TabIndex = 3;
            this.comboBoxExType.Visible = false;
            this.comboBoxExType.SelectedIndexChanged += new System.EventHandler(this.comboBoxExType_SelectedIndexChanged);
            // 
            // buttonXSaveRaster
            // 
            this.buttonXSaveRaster.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSaveRaster.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSaveRaster.Location = new System.Drawing.Point(9, 15);
            this.buttonXSaveRaster.Name = "buttonXSaveRaster";
            this.buttonXSaveRaster.Size = new System.Drawing.Size(111, 31);
            this.buttonXSaveRaster.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXSaveRaster.TabIndex = 4;
            this.buttonXSaveRaster.Text = "导出栅格数据";
            this.buttonXSaveRaster.Click += new System.EventHandler(this.buttonXSaveRaster_Click);
            // 
            // btnExportControlPoints
            // 
            this.btnExportControlPoints.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportControlPoints.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportControlPoints.Location = new System.Drawing.Point(3, 111);
            this.btnExportControlPoints.Name = "btnExportControlPoints";
            this.btnExportControlPoints.Size = new System.Drawing.Size(69, 31);
            this.btnExportControlPoints.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExportControlPoints.TabIndex = 1;
            this.btnExportControlPoints.Text = "导出控制点";
            this.btnExportControlPoints.Click += new System.EventHandler(this.btnExportControlPoints_Click);
            // 
            // btnImportControlPoints
            // 
            this.btnImportControlPoints.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImportControlPoints.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImportControlPoints.Location = new System.Drawing.Point(3, 162);
            this.btnImportControlPoints.Name = "btnImportControlPoints";
            this.btnImportControlPoints.Size = new System.Drawing.Size(69, 31);
            this.btnImportControlPoints.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImportControlPoints.TabIndex = 1;
            this.btnImportControlPoints.Text = "导入控制点";
            this.btnImportControlPoints.Click += new System.EventHandler(this.btnImportControlPoints_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnHide);
            this.groupPanel1.Controls.Add(this.buttonXSaveRaster);
            this.groupPanel1.Controls.Add(this.comboBoxExType);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupPanel1.Location = new System.Drawing.Point(0, 427);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(733, 61);
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
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
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
            this.groupPanel1.TabIndex = 5;
            // 
            // btnHide
            // 
            this.btnHide.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnHide.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnHide.Location = new System.Drawing.Point(610, 15);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(111, 31);
            this.btnHide.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnHide.TabIndex = 4;
            this.btnHide.Text = "关闭";
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.btnDelAll);
            this.groupPanel2.Controls.Add(this.buttonX1);
            this.groupPanel2.Controls.Add(this.btnExportControlPoints);
            this.groupPanel2.Controls.Add(this.btnImportControlPoints);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupPanel2.Location = new System.Drawing.Point(651, 0);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(82, 427);
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
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
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
            this.groupPanel2.TabIndex = 6;
            // 
            // btnDelAll
            // 
            this.btnDelAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelAll.Location = new System.Drawing.Point(3, 60);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new System.Drawing.Size(69, 31);
            this.btnDelAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelAll.TabIndex = 1;
            this.btnDelAll.Text = "删除所有点";
            this.btnDelAll.Click += new System.EventHandler(this.btnDelAll_Click);
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.dataGridViewX1);
            this.groupPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel3.Location = new System.Drawing.Point(0, 0);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(651, 427);
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
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
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
            this.groupPanel3.TabIndex = 7;
            // 
            // FrmLinkTableRaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 488);
            this.Controls.Add(this.groupPanel3);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.Name = "FrmLinkTableRaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "控制点表";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLinkTableRaster_FormClosing);
            this.Load += new System.EventHandler(this.FrmLinkTableRaster_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmLinkTableRaster_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExType;
        private DevComponents.DotNetBar.ButtonX buttonXSaveRaster;
        private DevComponents.DotNetBar.ButtonX btnExportControlPoints;
        private DevComponents.DotNetBar.ButtonX btnImportControlPoints;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.ButtonX btnHide;
        private DevComponents.DotNetBar.ButtonX btnDelAll;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private System.Data.DataColumn dRms;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn xSourceDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ySourceDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn xMapDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn yMapDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RMS;
    }
}