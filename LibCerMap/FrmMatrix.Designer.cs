namespace LibCerMap
{
    partial class FrmMatrix
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
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnExportXml = new DevComponents.DotNetBar.ButtonX();
            this.btnImportXml = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textOutData = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.dataGridMatrix = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.a = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnok.Location = new System.Drawing.Point(373, 296);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(71, 25);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 36;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancle.Location = new System.Drawing.Point(373, 252);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(71, 25);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 37;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // btnExportXml
            // 
            this.btnExportXml.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportXml.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportXml.Location = new System.Drawing.Point(373, 121);
            this.btnExportXml.Name = "btnExportXml";
            this.btnExportXml.Size = new System.Drawing.Size(71, 23);
            this.btnExportXml.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExportXml.TabIndex = 51;
            this.btnExportXml.Text = "导出XML";
            this.btnExportXml.Click += new System.EventHandler(this.btnExportXml_Click);
            // 
            // btnImportXml
            // 
            this.btnImportXml.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImportXml.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImportXml.Location = new System.Drawing.Point(373, 92);
            this.btnImportXml.Name = "btnImportXml";
            this.btnImportXml.Size = new System.Drawing.Size(71, 23);
            this.btnImportXml.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImportXml.TabIndex = 52;
            this.btnImportXml.Text = "导入XML";
            this.btnImportXml.Click += new System.EventHandler(this.btnImportXml_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(19, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(47, 23);
            this.labelX2.TabIndex = 44;
            this.labelX2.Text = "输入：";
            // 
            // cmbLayer
            // 
            this.cmbLayer.DisplayMember = "Text";
            this.cmbLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayer.FormattingEnabled = true;
            this.cmbLayer.ItemHeight = 17;
            this.cmbLayer.Location = new System.Drawing.Point(72, 12);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Size = new System.Drawing.Size(287, 23);
            this.cmbLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLayer.TabIndex = 50;
            this.cmbLayer.TextChanged += new System.EventHandler(this.cmbLayer_TextChanged);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(19, 91);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(47, 23);
            this.labelX3.TabIndex = 45;
            this.labelX3.Text = "关系：";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(19, 48);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 23);
            this.labelX1.TabIndex = 46;
            this.labelX1.Text = "输出：";
            // 
            // textOutData
            // 
            // 
            // 
            // 
            this.textOutData.Border.Class = "TextBoxBorder";
            this.textOutData.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textOutData.Location = new System.Drawing.Point(72, 50);
            this.textOutData.Name = "textOutData";
            this.textOutData.Size = new System.Drawing.Size(287, 21);
            this.textOutData.TabIndex = 47;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::LibCerMap.Properties.Resources.Matrix;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(72, 201);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(287, 120);
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Image = global::LibCerMap.Properties.Resources.GenericOpen16;
            this.btnExport.Location = new System.Drawing.Point(373, 50);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(71, 23);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 49;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dataGridMatrix
            // 
            this.dataGridMatrix.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMatrix.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMatrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.a,
            this.b,
            this.c,
            this.d});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridMatrix.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridMatrix.EnableHeadersVisualStyles = false;
            this.dataGridMatrix.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridMatrix.Location = new System.Drawing.Point(72, 91);
            this.dataGridMatrix.Name = "dataGridMatrix";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMatrix.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridMatrix.RowTemplate.Height = 23;
            this.dataGridMatrix.Size = new System.Drawing.Size(287, 104);
            this.dataGridMatrix.TabIndex = 55;
            this.dataGridMatrix.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridMatrix_CellValueChanged);
            // 
            // a
            // 
            this.a.HeaderText = "a";
            this.a.Name = "a";
            this.a.Width = 61;
            // 
            // b
            // 
            this.b.HeaderText = "b";
            this.b.Name = "b";
            this.b.Width = 61;
            // 
            // c
            // 
            this.c.HeaderText = "c";
            this.c.Name = "c";
            this.c.Width = 61;
            // 
            // d
            // 
            this.d.HeaderText = "d";
            this.d.Name = "d";
            this.d.Width = 61;
            // 
            // FrmMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(480, 347);
            this.Controls.Add(this.dataGridMatrix);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnExportXml);
            this.Controls.Add(this.btnImportXml);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cmbLayer);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.textOutData);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMatrix";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "矢量栅格数据坐标转换";
            this.Load += new System.EventHandler(this.FrmMatrix_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMatrix)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.ButtonX btnExportXml;
        private DevComponents.DotNetBar.ButtonX btnImportXml;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLayer;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textOutData;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridMatrix;
        private System.Windows.Forms.DataGridViewTextBoxColumn a;
        private System.Windows.Forms.DataGridViewTextBoxColumn b;
        private System.Windows.Forms.DataGridViewTextBoxColumn c;
        private System.Windows.Forms.DataGridViewTextBoxColumn d;
    }
}