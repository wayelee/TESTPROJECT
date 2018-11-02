namespace LibCerMap
{
    partial class FrmVectorLinkTable
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
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.column1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.btnDeleteLink = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.lblRMSError = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1DataGridViewTextBoxColumn,
            this.column2DataGridViewTextBoxColumn,
            this.column3DataGridViewTextBoxColumn,
            this.column4DataGridViewTextBoxColumn,
            this.column5DataGridViewTextBoxColumn});
            this.dataGridViewX1.DataMember = "Table1";
            this.dataGridViewX1.DataSource = this.dataSet1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewX1.EnableHeadersVisualStyles = false;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX1.MultiSelect = false;
            this.dataGridViewX1.Name = "dataGridViewX1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(543, 234);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellValueChanged);
            this.dataGridViewX1.SelectionChanged += new System.EventHandler(this.dataGridViewX1_SelectionChanged);
            this.dataGridViewX1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewX1_MouseUp);
            // 
            // column1DataGridViewTextBoxColumn
            // 
            this.column1DataGridViewTextBoxColumn.DataPropertyName = "Column1";
            this.column1DataGridViewTextBoxColumn.HeaderText = "ID";
            this.column1DataGridViewTextBoxColumn.Name = "column1DataGridViewTextBoxColumn";
            this.column1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // column2DataGridViewTextBoxColumn
            // 
            this.column2DataGridViewTextBoxColumn.DataPropertyName = "Column2";
            this.column2DataGridViewTextBoxColumn.HeaderText = "SrcX";
            this.column2DataGridViewTextBoxColumn.Name = "column2DataGridViewTextBoxColumn";
            // 
            // column3DataGridViewTextBoxColumn
            // 
            this.column3DataGridViewTextBoxColumn.DataPropertyName = "Column3";
            this.column3DataGridViewTextBoxColumn.HeaderText = "SrcY";
            this.column3DataGridViewTextBoxColumn.Name = "column3DataGridViewTextBoxColumn";
            // 
            // column4DataGridViewTextBoxColumn
            // 
            this.column4DataGridViewTextBoxColumn.DataPropertyName = "Column4";
            this.column4DataGridViewTextBoxColumn.HeaderText = "DstX";
            this.column4DataGridViewTextBoxColumn.Name = "column4DataGridViewTextBoxColumn";
            // 
            // column5DataGridViewTextBoxColumn
            // 
            this.column5DataGridViewTextBoxColumn.DataPropertyName = "Column5";
            this.column5DataGridViewTextBoxColumn.HeaderText = "DstY";
            this.column5DataGridViewTextBoxColumn.Name = "column5DataGridViewTextBoxColumn";
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
            this.dataColumn5});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "ID";
            this.dataColumn1.ColumnName = "Column1";
            this.dataColumn1.ReadOnly = true;
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "SrcX";
            this.dataColumn2.ColumnName = "Column2";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "SrcY";
            this.dataColumn3.ColumnName = "Column3";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "DstX";
            this.dataColumn4.ColumnName = "Column4";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "DstY";
            this.dataColumn5.ColumnName = "Column5";
            // 
            // btnDeleteLink
            // 
            this.btnDeleteLink.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeleteLink.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDeleteLink.Location = new System.Drawing.Point(12, 251);
            this.btnDeleteLink.Name = "btnDeleteLink";
            this.btnDeleteLink.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteLink.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDeleteLink.TabIndex = 1;
            this.btnDeleteLink.Text = "删除连接";
            this.btnDeleteLink.Click += new System.EventHandler(this.btnDeleteLink_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(456, 251);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRMSError
            // 
            // 
            // 
            // 
            this.lblRMSError.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRMSError.Location = new System.Drawing.Point(93, 251);
            this.lblRMSError.Name = "lblRMSError";
            this.lblRMSError.Size = new System.Drawing.Size(168, 23);
            this.lblRMSError.TabIndex = 3;
            this.lblRMSError.Text = "RMSError: 0.0000";
            this.lblRMSError.Visible = false;
            // 
            // FrmVectorLinkTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 286);
            this.Controls.Add(this.lblRMSError);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDeleteLink);
            this.Controls.Add(this.dataGridViewX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVectorLinkTable";
            this.ShowInTaskbar = false;
            this.Text = "FrmVectorLinkTable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVectorLinkTable_FormClosing);
            this.Load += new System.EventHandler(this.FrmVectorLinkTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.ButtonX btnDeleteLink;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.LabelX lblRMSError;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn column2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn column3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn column4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn column5DataGridViewTextBoxColumn;
    }
}