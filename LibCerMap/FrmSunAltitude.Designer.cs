namespace LibCerMap
{
    partial class FrmSunAltitude
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
            this.dataGridMatrix = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.nPtIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.angleHorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.angleVerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imgXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imgYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cameraXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cameraYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cameraZDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cameraPhiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cameraOmgDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cameraKalwaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.szImageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnDelRow = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMatrix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridMatrix
            // 
            this.dataGridMatrix.AutoGenerateColumns = false;
            this.dataGridMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMatrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nPtIDDataGridViewTextBoxColumn,
            this.angleHorDataGridViewTextBoxColumn,
            this.angleVerDataGridViewTextBoxColumn,
            this.imgXDataGridViewTextBoxColumn,
            this.imgYDataGridViewTextBoxColumn,
            this.cameraXDataGridViewTextBoxColumn,
            this.cameraYDataGridViewTextBoxColumn,
            this.cameraZDataGridViewTextBoxColumn,
            this.cameraPhiDataGridViewTextBoxColumn,
            this.cameraOmgDataGridViewTextBoxColumn,
            this.cameraKalwaDataGridViewTextBoxColumn,
            this.szImageName});
            this.dataGridMatrix.DataMember = "Table1";
            this.dataGridMatrix.DataSource = this.dataSet1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridMatrix.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridMatrix.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridMatrix.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridMatrix.Location = new System.Drawing.Point(0, 0);
            this.dataGridMatrix.Name = "dataGridMatrix";
            this.dataGridMatrix.RowTemplate.Height = 23;
            this.dataGridMatrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridMatrix.Size = new System.Drawing.Size(719, 296);
            this.dataGridMatrix.TabIndex = 56;
            // 
            // nPtIDDataGridViewTextBoxColumn
            // 
            this.nPtIDDataGridViewTextBoxColumn.DataPropertyName = "nPtID";
            this.nPtIDDataGridViewTextBoxColumn.HeaderText = "序号";
            this.nPtIDDataGridViewTextBoxColumn.Name = "nPtIDDataGridViewTextBoxColumn";
            // 
            // angleHorDataGridViewTextBoxColumn
            // 
            this.angleHorDataGridViewTextBoxColumn.DataPropertyName = "AngleHor";
            this.angleHorDataGridViewTextBoxColumn.HeaderText = "方位角度";
            this.angleHorDataGridViewTextBoxColumn.Name = "angleHorDataGridViewTextBoxColumn";
            // 
            // angleVerDataGridViewTextBoxColumn
            // 
            this.angleVerDataGridViewTextBoxColumn.DataPropertyName = "AngleVer";
            this.angleVerDataGridViewTextBoxColumn.HeaderText = "垂直角度";
            this.angleVerDataGridViewTextBoxColumn.Name = "angleVerDataGridViewTextBoxColumn";
            // 
            // imgXDataGridViewTextBoxColumn
            // 
            this.imgXDataGridViewTextBoxColumn.DataPropertyName = "ImgX";
            this.imgXDataGridViewTextBoxColumn.HeaderText = "图像坐标X";
            this.imgXDataGridViewTextBoxColumn.Name = "imgXDataGridViewTextBoxColumn";
            // 
            // imgYDataGridViewTextBoxColumn
            // 
            this.imgYDataGridViewTextBoxColumn.DataPropertyName = "ImgY";
            this.imgYDataGridViewTextBoxColumn.HeaderText = "图像坐标Y";
            this.imgYDataGridViewTextBoxColumn.Name = "imgYDataGridViewTextBoxColumn";
            // 
            // cameraXDataGridViewTextBoxColumn
            // 
            this.cameraXDataGridViewTextBoxColumn.DataPropertyName = "CameraX";
            this.cameraXDataGridViewTextBoxColumn.HeaderText = "相机坐标X";
            this.cameraXDataGridViewTextBoxColumn.Name = "cameraXDataGridViewTextBoxColumn";
            // 
            // cameraYDataGridViewTextBoxColumn
            // 
            this.cameraYDataGridViewTextBoxColumn.DataPropertyName = "CameraY";
            this.cameraYDataGridViewTextBoxColumn.HeaderText = "相机坐标Y";
            this.cameraYDataGridViewTextBoxColumn.Name = "cameraYDataGridViewTextBoxColumn";
            // 
            // cameraZDataGridViewTextBoxColumn
            // 
            this.cameraZDataGridViewTextBoxColumn.DataPropertyName = "CameraZ";
            this.cameraZDataGridViewTextBoxColumn.HeaderText = "相机坐标Z";
            this.cameraZDataGridViewTextBoxColumn.Name = "cameraZDataGridViewTextBoxColumn";
            // 
            // cameraPhiDataGridViewTextBoxColumn
            // 
            this.cameraPhiDataGridViewTextBoxColumn.DataPropertyName = "CameraPhi";
            this.cameraPhiDataGridViewTextBoxColumn.HeaderText = "相机姿态Phi";
            this.cameraPhiDataGridViewTextBoxColumn.Name = "cameraPhiDataGridViewTextBoxColumn";
            // 
            // cameraOmgDataGridViewTextBoxColumn
            // 
            this.cameraOmgDataGridViewTextBoxColumn.DataPropertyName = "CameraOmg";
            this.cameraOmgDataGridViewTextBoxColumn.HeaderText = "相机姿态Omg";
            this.cameraOmgDataGridViewTextBoxColumn.Name = "cameraOmgDataGridViewTextBoxColumn";
            // 
            // cameraKalwaDataGridViewTextBoxColumn
            // 
            this.cameraKalwaDataGridViewTextBoxColumn.DataPropertyName = "CameraKalwa";
            this.cameraKalwaDataGridViewTextBoxColumn.HeaderText = "相机姿态Kalwa";
            this.cameraKalwaDataGridViewTextBoxColumn.Name = "cameraKalwaDataGridViewTextBoxColumn";
            // 
            // szImageName
            // 
            this.szImageName.DataPropertyName = "szImageName";
            this.szImageName.HeaderText = "影像名称";
            this.szImageName.Name = "szImageName";
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
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "序号";
            this.dataColumn1.ColumnName = "nPtID";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "方位角";
            this.dataColumn2.ColumnName = "AngleHor";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "垂直角度";
            this.dataColumn3.ColumnName = "AngleVer";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "图像坐标X";
            this.dataColumn4.ColumnName = "ImgX";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "图像坐标Y";
            this.dataColumn5.ColumnName = "ImgY";
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "相机坐标X";
            this.dataColumn6.ColumnName = "CameraX";
            // 
            // dataColumn7
            // 
            this.dataColumn7.Caption = "相机坐标Y";
            this.dataColumn7.ColumnName = "CameraY";
            // 
            // dataColumn8
            // 
            this.dataColumn8.Caption = "相机坐标Z";
            this.dataColumn8.ColumnName = "CameraZ";
            // 
            // dataColumn9
            // 
            this.dataColumn9.Caption = "相机姿态Phi";
            this.dataColumn9.ColumnName = "CameraPhi";
            // 
            // dataColumn10
            // 
            this.dataColumn10.Caption = "相机姿态Omg";
            this.dataColumn10.ColumnName = "CameraOmg";
            // 
            // dataColumn11
            // 
            this.dataColumn11.Caption = "相机姿态Kalwa";
            this.dataColumn11.ColumnName = "CameraKalwa";
            // 
            // dataColumn12
            // 
            this.dataColumn12.Caption = "影像名称";
            this.dataColumn12.ColumnName = "szImageName";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(612, 325);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 57;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(123, 325);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 58;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnDelRow
            // 
            this.btnDelRow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelRow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelRow.Location = new System.Drawing.Point(10, 325);
            this.btnDelRow.Name = "btnDelRow";
            this.btnDelRow.Size = new System.Drawing.Size(75, 23);
            this.btnDelRow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelRow.TabIndex = 59;
            this.btnDelRow.Text = "删除行";
            this.btnDelRow.Click += new System.EventHandler(this.btnDelRow_Click);
            // 
            // FrmSunAltitude
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(719, 364);
            this.Controls.Add(this.btnDelRow);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridMatrix);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSunAltitude";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地平线高度角测量";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSunAltitude_FormClosing);
            this.Load += new System.EventHandler(this.FrmSunAltitude_Load);
            this.Shown += new System.EventHandler(this.FrmSunAltitude_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMatrix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridMatrix;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnDelRow;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataColumn dataColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn nPtIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn angleHorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn angleVerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imgXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imgYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cameraXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cameraYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cameraZDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cameraPhiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cameraOmgDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cameraKalwaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn szImageName;
    }
}