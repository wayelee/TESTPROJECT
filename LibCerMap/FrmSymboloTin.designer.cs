namespace LibCerMap
{
    partial class FrmSymboloTin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSymboloTin));
            this.lblshow = new DevComponents.DotNetBar.LabelX();
            this.treeshow = new DevComponents.AdvTree.AdvTree();
            this.nodeclassified = new DevComponents.AdvTree.Node();
            this.node1 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.cmbclasses = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.comboItem13 = new DevComponents.Editors.ComboItem();
            this.comboItem14 = new DevComponents.Editors.ComboItem();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.comboItem19 = new DevComponents.Editors.ComboItem();
            this.comboItem20 = new DevComponents.Editors.ComboItem();
            this.comboItem21 = new DevComponents.Editors.ComboItem();
            this.comboItem22 = new DevComponents.Editors.ComboItem();
            this.comboItem23 = new DevComponents.Editors.ComboItem();
            this.comboItem24 = new DevComponents.Editors.ComboItem();
            this.comboItem25 = new DevComponents.Editors.ComboItem();
            this.comboItem26 = new DevComponents.Editors.ComboItem();
            this.comboItem27 = new DevComponents.Editors.ComboItem();
            this.comboItem28 = new DevComponents.Editors.ComboItem();
            this.comboItem29 = new DevComponents.Editors.ComboItem();
            this.comboItem30 = new DevComponents.Editors.ComboItem();
            this.comboItem31 = new DevComponents.Editors.ComboItem();
            this.comboItem32 = new DevComponents.Editors.ComboItem();
            this.lblclass = new DevComponents.DotNetBar.LabelX();
            this.lblcolor = new DevComponents.DotNetBar.LabelX();
            this.datagridsymbol = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.范围DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancel = new DevComponents.DotNetBar.ButtonX();
            this.btnuse = new DevComponents.DotNetBar.ButtonX();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.cmbcolor = new LibCerMap.FrmSymboloTin.ComboboxSymbol();
            this.colordlg = new System.Windows.Forms.ColorDialog();
            this.btncolor = new CustomColorPicker();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.treeshow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridsymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblshow
            // 
            // 
            // 
            // 
            this.lblshow.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblshow.Location = new System.Drawing.Point(12, 12);
            this.lblshow.Name = "lblshow";
            this.lblshow.Size = new System.Drawing.Size(51, 22);
            this.lblshow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lblshow.TabIndex = 0;
            this.lblshow.Text = "显示：";
            // 
            // treeshow
            // 
            this.treeshow.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeshow.AllowDrop = true;
            this.treeshow.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeshow.BackgroundStyle.Class = "TreeBorderKey";
            this.treeshow.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeshow.Location = new System.Drawing.Point(12, 40);
            this.treeshow.Name = "treeshow";
            this.treeshow.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.nodeclassified,
            this.node1});
            this.treeshow.NodesConnector = this.nodeConnector1;
            this.treeshow.NodeStyle = this.elementStyle1;
            this.treeshow.PathSeparator = ";";
            this.treeshow.Size = new System.Drawing.Size(144, 148);
            this.treeshow.Styles.Add(this.elementStyle1);
            this.treeshow.TabIndex = 1;
            this.treeshow.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.treeshow_AfterNodeSelect);
            // 
            // nodeclassified
            // 
            this.nodeclassified.Expanded = true;
            this.nodeclassified.Name = "nodeclassified";
            this.nodeclassified.Text = "高程渲染";
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = "单色面渲染";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // cmbclasses
            // 
            this.cmbclasses.DisplayMember = "Text";
            this.cmbclasses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbclasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbclasses.FormattingEnabled = true;
            this.cmbclasses.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbclasses.ItemHeight = 15;
            this.cmbclasses.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10,
            this.comboItem11,
            this.comboItem12,
            this.comboItem13,
            this.comboItem14,
            this.comboItem15,
            this.comboItem16,
            this.comboItem17,
            this.comboItem18,
            this.comboItem19,
            this.comboItem20,
            this.comboItem21,
            this.comboItem22,
            this.comboItem23,
            this.comboItem24,
            this.comboItem25,
            this.comboItem26,
            this.comboItem27,
            this.comboItem28,
            this.comboItem29,
            this.comboItem30,
            this.comboItem31,
            this.comboItem32});
            this.cmbclasses.Location = new System.Drawing.Point(238, 40);
            this.cmbclasses.Name = "cmbclasses";
            this.cmbclasses.Size = new System.Drawing.Size(69, 21);
            this.cmbclasses.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbclasses.TabIndex = 1;
            this.cmbclasses.Visible = false;
            this.cmbclasses.SelectedIndexChanged += new System.EventHandler(this.cmbclasses_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "2";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "3";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "4";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "5";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "6";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "7";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "8";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "9";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "10";
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "11";
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "12";
            // 
            // comboItem13
            // 
            this.comboItem13.Text = "13";
            // 
            // comboItem14
            // 
            this.comboItem14.Text = "14";
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "15";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "16";
            // 
            // comboItem17
            // 
            this.comboItem17.Text = "17";
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "18";
            // 
            // comboItem19
            // 
            this.comboItem19.Text = "19";
            // 
            // comboItem20
            // 
            this.comboItem20.Text = "20";
            // 
            // comboItem21
            // 
            this.comboItem21.Text = "21";
            // 
            // comboItem22
            // 
            this.comboItem22.Text = "22";
            // 
            // comboItem23
            // 
            this.comboItem23.Text = "23";
            // 
            // comboItem24
            // 
            this.comboItem24.Text = "24";
            // 
            // comboItem25
            // 
            this.comboItem25.Text = "25";
            // 
            // comboItem26
            // 
            this.comboItem26.Text = "26";
            // 
            // comboItem27
            // 
            this.comboItem27.Text = "27";
            // 
            // comboItem28
            // 
            this.comboItem28.Text = "28";
            // 
            // comboItem29
            // 
            this.comboItem29.Text = "29";
            // 
            // comboItem30
            // 
            this.comboItem30.Text = "30";
            // 
            // comboItem31
            // 
            this.comboItem31.Text = "31";
            // 
            // comboItem32
            // 
            this.comboItem32.Text = "32";
            // 
            // lblclass
            // 
            this.lblclass.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblclass.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblclass.Location = new System.Drawing.Point(172, 40);
            this.lblclass.Name = "lblclass";
            this.lblclass.Size = new System.Drawing.Size(43, 21);
            this.lblclass.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lblclass.TabIndex = 0;
            this.lblclass.Text = "分类数";
            this.lblclass.Visible = false;
            // 
            // lblcolor
            // 
            this.lblcolor.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblcolor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblcolor.Location = new System.Drawing.Point(345, 40);
            this.lblcolor.Name = "lblcolor";
            this.lblcolor.Size = new System.Drawing.Size(44, 22);
            this.lblcolor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lblcolor.TabIndex = 4;
            this.lblcolor.Text = "颜色条";
            this.lblcolor.UseWaitCursor = true;
            this.lblcolor.Visible = false;
            // 
            // datagridsymbol
            // 
            this.datagridsymbol.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridsymbol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.datagridsymbol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridsymbol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.范围DataGridViewTextBoxColumn,
            this.labelDataGridViewTextBoxColumn});
            this.datagridsymbol.DataMember = "Table2";
            this.datagridsymbol.DataSource = this.dataSet1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridsymbol.DefaultCellStyle = dataGridViewCellStyle2;
            this.datagridsymbol.EnableHeadersVisualStyles = false;
            this.datagridsymbol.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.datagridsymbol.Location = new System.Drawing.Point(162, 84);
            this.datagridsymbol.Name = "datagridsymbol";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridsymbol.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.datagridsymbol.RowHeadersVisible = false;
            this.datagridsymbol.RowTemplate.Height = 23;
            this.datagridsymbol.Size = new System.Drawing.Size(444, 187);
            this.datagridsymbol.TabIndex = 6;
            this.datagridsymbol.Visible = false;
            this.datagridsymbol.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridsymbol_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "颜色";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // 范围DataGridViewTextBoxColumn
            // 
            this.范围DataGridViewTextBoxColumn.DataPropertyName = "范围";
            this.范围DataGridViewTextBoxColumn.HeaderText = "范围";
            this.范围DataGridViewTextBoxColumn.Name = "范围DataGridViewTextBoxColumn";
            this.范围DataGridViewTextBoxColumn.Width = 185;
            // 
            // labelDataGridViewTextBoxColumn
            // 
            this.labelDataGridViewTextBoxColumn.DataPropertyName = "Label";
            this.labelDataGridViewTextBoxColumn.HeaderText = "Label";
            this.labelDataGridViewTextBoxColumn.Name = "labelDataGridViewTextBoxColumn";
            this.labelDataGridViewTextBoxColumn.Width = 185;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Column1";
            this.dataColumn1.DataType = typeof(double);
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4});
            this.dataTable2.TableName = "Table2";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "颜色";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "范围";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Label";
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(287, 297);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(77, 24);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 7;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancel
            // 
            this.btncancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancel.Location = new System.Drawing.Point(406, 297);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(77, 24);
            this.btncancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancel.TabIndex = 8;
            this.btncancel.Text = "取消";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnuse
            // 
            this.btnuse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnuse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnuse.Location = new System.Drawing.Point(526, 297);
            this.btnuse.Name = "btnuse";
            this.btnuse.Size = new System.Drawing.Size(77, 24);
            this.btnuse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnuse.TabIndex = 9;
            this.btnuse.Text = "应用";
            this.btnuse.Click += new System.EventHandler(this.btnuse_Click);
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Location = new System.Drawing.Point(12, 201);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(144, 120);
            this.axSymbologyControl1.TabIndex = 10;
            this.axSymbologyControl1.Visible = false;
            // 
            // cmbcolor
            // 
            this.cmbcolor.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbcolor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbcolor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcolor.FormattingEnabled = true;
            this.cmbcolor.Location = new System.Drawing.Point(406, 39);
            this.cmbcolor.Name = "cmbcolor";
            this.cmbcolor.Size = new System.Drawing.Size(200, 22);
            this.cmbcolor.TabIndex = 13;
            this.cmbcolor.Visible = false;
            this.cmbcolor.SelectedIndexChanged += new System.EventHandler(this.cmbcolor_SelectedIndexChanged);
            // 
            // btncolor
            // 
            this.btncolor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncolor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncolor.Image = ((System.Drawing.Image)(resources.GetObject("btncolor.Image")));
            this.btncolor.Location = new System.Drawing.Point(313, 40);
            this.btncolor.Name = "btncolor";
            this.btncolor.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.btncolor.Size = new System.Drawing.Size(37, 23);
            this.btncolor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncolor.TabIndex = 14;
            this.btncolor.Visible = false;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(238, 40);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(43, 21);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX1.TabIndex = 15;
            this.labelX1.Text = "颜色";
            this.labelX1.Visible = false;
            // 
            // FrmSymboloTin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 337);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btncolor);
            this.Controls.Add(this.lblclass);
            this.Controls.Add(this.cmbclasses);
            this.Controls.Add(this.cmbcolor);
            this.Controls.Add(this.axSymbologyControl1);
            this.Controls.Add(this.btnuse);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.datagridsymbol);
            this.Controls.Add(this.lblcolor);
            this.Controls.Add(this.treeshow);
            this.Controls.Add(this.lblshow);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmSymboloTin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TIN渲染";
            this.Load += new System.EventHandler(this.FrmSymboloTin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeshow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridsymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblshow;
        private DevComponents.AdvTree.AdvTree treeshow;
        private DevComponents.AdvTree.Node nodeclassified;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.LabelX lblclass;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbclasses;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.ComboItem comboItem13;
        private DevComponents.Editors.ComboItem comboItem14;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.Editors.ComboItem comboItem18;
        private DevComponents.Editors.ComboItem comboItem19;
        private DevComponents.Editors.ComboItem comboItem20;
        private DevComponents.Editors.ComboItem comboItem21;
        private DevComponents.Editors.ComboItem comboItem22;
        private DevComponents.Editors.ComboItem comboItem23;
        private DevComponents.Editors.ComboItem comboItem24;
        private DevComponents.Editors.ComboItem comboItem25;
        private DevComponents.Editors.ComboItem comboItem26;
        private DevComponents.Editors.ComboItem comboItem27;
        private DevComponents.Editors.ComboItem comboItem28;
        private DevComponents.Editors.ComboItem comboItem29;
        private DevComponents.Editors.ComboItem comboItem30;
        private DevComponents.Editors.ComboItem comboItem31;
        private DevComponents.Editors.ComboItem comboItem32;
        private DevComponents.DotNetBar.LabelX lblcolor;
        private DevComponents.DotNetBar.Controls.DataGridViewX datagridsymbol;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancel;
        private DevComponents.DotNetBar.ButtonX btnuse;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private ComboboxSymbol cmbcolor;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Windows.Forms.ColorDialog colordlg;
        private DevComponents.AdvTree.Node node1;
        private CustomColorPicker btncolor;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 范围DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn;
    }
}