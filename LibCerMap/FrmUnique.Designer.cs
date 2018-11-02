namespace LibCerMap
{
    partial class FrmUnique
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUnique));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataTable3 = new System.Data.DataTable();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.lblshow = new DevComponents.DotNetBar.LabelX();
            this.treeshow = new DevComponents.AdvTree.AdvTree();
            this.node1 = new DevComponents.AdvTree.Node();
            this.node2 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.grPanelfield = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbfield = new System.Windows.Forms.ComboBox();
            this.gridviewu = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.值DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnaddall = new DevComponents.DotNetBar.ButtonX();
            this.btnremoveall = new DevComponents.DotNetBar.ButtonX();
            this.btnadd = new DevComponents.DotNetBar.ButtonX();
            this.btnremove = new DevComponents.DotNetBar.ButtonX();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.btnuse = new DevComponents.DotNetBar.ButtonX();
            this.grpanelfieldmu = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbfield3 = new System.Windows.Forms.ComboBox();
            this.cmbfield2 = new System.Windows.Forms.ComboBox();
            this.cmbfield1 = new System.Windows.Forms.ComboBox();
            this.grpanelcolor = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbcolor = new LibCerMap.FrmUnique.ComboboxSymbol();
            this.grpanelcolormu = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbcolormu = new LibCerMap.FrmUnique.ComboboxSymbol();
            this.gridviewmu = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.值DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnInlayer = new DevComponents.DotNetBar.ButtonX();
            this.txtInlayer = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeshow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            this.grPanelfield.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewu)).BeginInit();
            this.grpanelfieldmu.SuspendLayout();
            this.grpanelcolor.SuspendLayout();
            this.grpanelcolormu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewmu)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3});
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
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5});
            this.dataTable2.TableName = "Table2";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "颜色";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "值";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Label";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "数量";
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9});
            this.dataTable3.TableName = "Table3";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "颜色";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "值";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "Label";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "数量";
            // 
            // lblshow
            // 
            // 
            // 
            // 
            this.lblshow.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblshow.Location = new System.Drawing.Point(12, 12);
            this.lblshow.Name = "lblshow";
            this.lblshow.Size = new System.Drawing.Size(49, 23);
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
            this.treeshow.Location = new System.Drawing.Point(12, 41);
            this.treeshow.Name = "treeshow";
            this.treeshow.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1,
            this.node2});
            this.treeshow.NodesConnector = this.nodeConnector1;
            this.treeshow.NodeStyle = this.elementStyle1;
            this.treeshow.PathSeparator = ";";
            this.treeshow.Size = new System.Drawing.Size(137, 194);
            this.treeshow.Styles.Add(this.elementStyle1);
            this.treeshow.TabIndex = 1;
            this.treeshow.Text = "advTree1";
            this.treeshow.SelectedIndexChanged += new System.EventHandler(this.treeshow_SelectedIndexChanged);
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = "独立值,单波段";
            // 
            // node2
            // 
            this.node2.Expanded = true;
            this.node2.Name = "node2";
            this.node2.Text = "独立值,多波段";
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
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Location = new System.Drawing.Point(8, 272);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(141, 144);
            this.axSymbologyControl1.TabIndex = 2;
            this.axSymbologyControl1.Visible = false;
            // 
            // grPanelfield
            // 
            this.grPanelfield.CanvasColor = System.Drawing.SystemColors.Control;
            this.grPanelfield.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grPanelfield.Controls.Add(this.cmbfield);
            this.grPanelfield.Location = new System.Drawing.Point(155, 46);
            this.grPanelfield.Name = "grPanelfield";
            this.grPanelfield.Size = new System.Drawing.Size(207, 78);
            // 
            // 
            // 
            this.grPanelfield.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grPanelfield.Style.BackColorGradientAngle = 90;
            this.grPanelfield.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grPanelfield.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grPanelfield.Style.BorderBottomWidth = 1;
            this.grPanelfield.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grPanelfield.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grPanelfield.Style.BorderLeftWidth = 1;
            this.grPanelfield.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grPanelfield.Style.BorderRightWidth = 1;
            this.grPanelfield.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grPanelfield.Style.BorderTopWidth = 1;
            this.grPanelfield.Style.CornerDiameter = 4;
            this.grPanelfield.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grPanelfield.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grPanelfield.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grPanelfield.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grPanelfield.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grPanelfield.TabIndex = 13;
            this.grPanelfield.Text = "字段";
            // 
            // cmbfield
            // 
            this.cmbfield.FormattingEnabled = true;
            this.cmbfield.Location = new System.Drawing.Point(2, 12);
            this.cmbfield.Name = "cmbfield";
            this.cmbfield.Size = new System.Drawing.Size(196, 20);
            this.cmbfield.TabIndex = 0;
            this.cmbfield.SelectedIndexChanged += new System.EventHandler(this.cmbfield_SelectedIndexChanged);
            // 
            // gridviewu
            // 
            this.gridviewu.AutoGenerateColumns = false;
            this.gridviewu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.值DataGridViewTextBoxColumn1,
            this.labelDataGridViewTextBoxColumn1,
            this.数量DataGridViewTextBoxColumn1});
            this.gridviewu.DataMember = "Table2";
            this.gridviewu.DataSource = this.dataSet1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridviewu.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridviewu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.gridviewu.Location = new System.Drawing.Point(156, 130);
            this.gridviewu.Name = "gridviewu";
            this.gridviewu.RowHeadersVisible = false;
            this.gridviewu.RowTemplate.Height = 23;
            this.gridviewu.Size = new System.Drawing.Size(462, 293);
            this.gridviewu.TabIndex = 15;
            this.gridviewu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridviewu_CellClick);
            this.gridviewu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridviewu_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "颜色";
            this.Column1.Name = "Column1";
            this.Column1.Width = 102;
            // 
            // 值DataGridViewTextBoxColumn1
            // 
            this.值DataGridViewTextBoxColumn1.DataPropertyName = "值";
            this.值DataGridViewTextBoxColumn1.HeaderText = "值";
            this.值DataGridViewTextBoxColumn1.Name = "值DataGridViewTextBoxColumn1";
            this.值DataGridViewTextBoxColumn1.Width = 119;
            // 
            // labelDataGridViewTextBoxColumn1
            // 
            this.labelDataGridViewTextBoxColumn1.DataPropertyName = "Label";
            this.labelDataGridViewTextBoxColumn1.HeaderText = "Label";
            this.labelDataGridViewTextBoxColumn1.Name = "labelDataGridViewTextBoxColumn1";
            this.labelDataGridViewTextBoxColumn1.Width = 119;
            // 
            // 数量DataGridViewTextBoxColumn1
            // 
            this.数量DataGridViewTextBoxColumn1.DataPropertyName = "数量";
            this.数量DataGridViewTextBoxColumn1.HeaderText = "数量";
            this.数量DataGridViewTextBoxColumn1.Name = "数量DataGridViewTextBoxColumn1";
            this.数量DataGridViewTextBoxColumn1.Width = 119;
            // 
            // btnaddall
            // 
            this.btnaddall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnaddall.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnaddall.Location = new System.Drawing.Point(180, 429);
            this.btnaddall.Name = "btnaddall";
            this.btnaddall.Size = new System.Drawing.Size(89, 23);
            this.btnaddall.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnaddall.TabIndex = 16;
            this.btnaddall.Text = "添加所有值";
            this.btnaddall.Click += new System.EventHandler(this.btnaddall_Click);
            // 
            // btnremoveall
            // 
            this.btnremoveall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnremoveall.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnremoveall.Location = new System.Drawing.Point(287, 429);
            this.btnremoveall.Name = "btnremoveall";
            this.btnremoveall.Size = new System.Drawing.Size(89, 23);
            this.btnremoveall.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnremoveall.TabIndex = 17;
            this.btnremoveall.Text = "移除所有值";
            this.btnremoveall.Click += new System.EventHandler(this.btnremoveall_Click);
            // 
            // btnadd
            // 
            this.btnadd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnadd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnadd.Enabled = false;
            this.btnadd.Location = new System.Drawing.Point(393, 429);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(89, 23);
            this.btnadd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnadd.TabIndex = 18;
            this.btnadd.Text = "添加值";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnremove
            // 
            this.btnremove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnremove.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnremove.Enabled = false;
            this.btnremove.Location = new System.Drawing.Point(500, 429);
            this.btnremove.Name = "btnremove";
            this.btnremove.Size = new System.Drawing.Size(89, 23);
            this.btnremove.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnremove.TabIndex = 19;
            this.btnremove.Text = "移除值";
            this.btnremove.Click += new System.EventHandler(this.btnremove_Click);
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(287, 478);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(91, 30);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 20;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(408, 478);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(91, 30);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 21;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // btnuse
            // 
            this.btnuse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnuse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnuse.Location = new System.Drawing.Point(527, 478);
            this.btnuse.Name = "btnuse";
            this.btnuse.Size = new System.Drawing.Size(91, 30);
            this.btnuse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnuse.TabIndex = 22;
            this.btnuse.Text = "应用";
            this.btnuse.Click += new System.EventHandler(this.btnuse_Click);
            // 
            // grpanelfieldmu
            // 
            this.grpanelfieldmu.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpanelfieldmu.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grpanelfieldmu.Controls.Add(this.cmbfield3);
            this.grpanelfieldmu.Controls.Add(this.cmbfield2);
            this.grpanelfieldmu.Controls.Add(this.cmbfield1);
            this.grpanelfieldmu.Location = new System.Drawing.Point(155, 48);
            this.grpanelfieldmu.Name = "grpanelfieldmu";
            this.grpanelfieldmu.Size = new System.Drawing.Size(207, 136);
            // 
            // 
            // 
            this.grpanelfieldmu.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpanelfieldmu.Style.BackColorGradientAngle = 90;
            this.grpanelfieldmu.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpanelfieldmu.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelfieldmu.Style.BorderBottomWidth = 1;
            this.grpanelfieldmu.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpanelfieldmu.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelfieldmu.Style.BorderLeftWidth = 1;
            this.grpanelfieldmu.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelfieldmu.Style.BorderRightWidth = 1;
            this.grpanelfieldmu.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelfieldmu.Style.BorderTopWidth = 1;
            this.grpanelfieldmu.Style.CornerDiameter = 4;
            this.grpanelfieldmu.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpanelfieldmu.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpanelfieldmu.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpanelfieldmu.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpanelfieldmu.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpanelfieldmu.TabIndex = 23;
            this.grpanelfieldmu.Text = "字段";
            // 
            // cmbfield3
            // 
            this.cmbfield3.FormattingEnabled = true;
            this.cmbfield3.Location = new System.Drawing.Point(2, 80);
            this.cmbfield3.Name = "cmbfield3";
            this.cmbfield3.Size = new System.Drawing.Size(196, 20);
            this.cmbfield3.TabIndex = 2;
            this.cmbfield3.SelectedIndexChanged += new System.EventHandler(this.cmbfield3_SelectedIndexChanged);
            // 
            // cmbfield2
            // 
            this.cmbfield2.FormattingEnabled = true;
            this.cmbfield2.Location = new System.Drawing.Point(2, 46);
            this.cmbfield2.Name = "cmbfield2";
            this.cmbfield2.Size = new System.Drawing.Size(196, 20);
            this.cmbfield2.TabIndex = 1;
            this.cmbfield2.SelectedIndexChanged += new System.EventHandler(this.cmbfield2_SelectedIndexChanged);
            // 
            // cmbfield1
            // 
            this.cmbfield1.FormattingEnabled = true;
            this.cmbfield1.Location = new System.Drawing.Point(2, 12);
            this.cmbfield1.Name = "cmbfield1";
            this.cmbfield1.Size = new System.Drawing.Size(196, 20);
            this.cmbfield1.TabIndex = 0;
            this.cmbfield1.SelectedIndexChanged += new System.EventHandler(this.cmbfield1_SelectedIndexChanged);
            // 
            // grpanelcolor
            // 
            this.grpanelcolor.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpanelcolor.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grpanelcolor.Controls.Add(this.cmbcolor);
            this.grpanelcolor.Location = new System.Drawing.Point(368, 48);
            this.grpanelcolor.Name = "grpanelcolor";
            this.grpanelcolor.Size = new System.Drawing.Size(250, 76);
            // 
            // 
            // 
            this.grpanelcolor.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpanelcolor.Style.BackColorGradientAngle = 90;
            this.grpanelcolor.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpanelcolor.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolor.Style.BorderBottomWidth = 1;
            this.grpanelcolor.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpanelcolor.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolor.Style.BorderLeftWidth = 1;
            this.grpanelcolor.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolor.Style.BorderRightWidth = 1;
            this.grpanelcolor.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolor.Style.BorderTopWidth = 1;
            this.grpanelcolor.Style.CornerDiameter = 4;
            this.grpanelcolor.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpanelcolor.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpanelcolor.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpanelcolor.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpanelcolor.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpanelcolor.TabIndex = 24;
            this.grpanelcolor.Text = "颜色带";
            // 
            // cmbcolor
            // 
            this.cmbcolor.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbcolor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbcolor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcolor.FormattingEnabled = true;
            this.cmbcolor.Location = new System.Drawing.Point(2, 12);
            this.cmbcolor.Name = "cmbcolor";
            this.cmbcolor.Size = new System.Drawing.Size(239, 22);
            this.cmbcolor.TabIndex = 0;
            this.cmbcolor.SelectedIndexChanged += new System.EventHandler(this.cmbcolor_SelectedIndexChanged);
            // 
            // grpanelcolormu
            // 
            this.grpanelcolormu.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpanelcolormu.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grpanelcolormu.Controls.Add(this.cmbcolormu);
            this.grpanelcolormu.Location = new System.Drawing.Point(368, 48);
            this.grpanelcolormu.Name = "grpanelcolormu";
            this.grpanelcolormu.Size = new System.Drawing.Size(250, 76);
            // 
            // 
            // 
            this.grpanelcolormu.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpanelcolormu.Style.BackColorGradientAngle = 90;
            this.grpanelcolormu.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpanelcolormu.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolormu.Style.BorderBottomWidth = 1;
            this.grpanelcolormu.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpanelcolormu.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolormu.Style.BorderLeftWidth = 1;
            this.grpanelcolormu.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolormu.Style.BorderRightWidth = 1;
            this.grpanelcolormu.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpanelcolormu.Style.BorderTopWidth = 1;
            this.grpanelcolormu.Style.CornerDiameter = 4;
            this.grpanelcolormu.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpanelcolormu.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpanelcolormu.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpanelcolormu.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpanelcolormu.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpanelcolormu.TabIndex = 25;
            this.grpanelcolormu.Text = "颜色带";
            // 
            // cmbcolormu
            // 
            this.cmbcolormu.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbcolormu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbcolormu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcolormu.FormattingEnabled = true;
            this.cmbcolormu.Location = new System.Drawing.Point(2, 12);
            this.cmbcolormu.Name = "cmbcolormu";
            this.cmbcolormu.Size = new System.Drawing.Size(239, 22);
            this.cmbcolormu.TabIndex = 0;
            this.cmbcolormu.SelectedIndexChanged += new System.EventHandler(this.cmbcolormu_SelectedIndexChanged);
            // 
            // gridviewmu
            // 
            this.gridviewmu.AutoGenerateColumns = false;
            this.gridviewmu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewmu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.值DataGridViewTextBoxColumn,
            this.labelDataGridViewTextBoxColumn,
            this.DataGridViewTextBoxColumn});
            this.gridviewmu.DataMember = "Table3";
            this.gridviewmu.DataSource = this.dataSet1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridviewmu.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridviewmu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.gridviewmu.Location = new System.Drawing.Point(155, 190);
            this.gridviewmu.Name = "gridviewmu";
            this.gridviewmu.RowHeadersVisible = false;
            this.gridviewmu.RowTemplate.Height = 23;
            this.gridviewmu.Size = new System.Drawing.Size(462, 233);
            this.gridviewmu.TabIndex = 26;
            this.gridviewmu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridviewmu_CellClick);
            this.gridviewmu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridviewmu_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "颜色";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 102;
            // 
            // 值DataGridViewTextBoxColumn
            // 
            this.值DataGridViewTextBoxColumn.DataPropertyName = "值";
            this.值DataGridViewTextBoxColumn.HeaderText = "值";
            this.值DataGridViewTextBoxColumn.Name = "值DataGridViewTextBoxColumn";
            this.值DataGridViewTextBoxColumn.Width = 119;
            // 
            // labelDataGridViewTextBoxColumn
            // 
            this.labelDataGridViewTextBoxColumn.DataPropertyName = "Label";
            this.labelDataGridViewTextBoxColumn.HeaderText = "Label";
            this.labelDataGridViewTextBoxColumn.Name = "labelDataGridViewTextBoxColumn";
            this.labelDataGridViewTextBoxColumn.Width = 119;
            // 
            // DataGridViewTextBoxColumn
            // 
            this.DataGridViewTextBoxColumn.DataPropertyName = "数量";
            this.DataGridViewTextBoxColumn.HeaderText = "数量";
            this.DataGridViewTextBoxColumn.Name = "DataGridViewTextBoxColumn";
            this.DataGridViewTextBoxColumn.Width = 119;
            // 
            // btnInlayer
            // 
            this.btnInlayer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInlayer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInlayer.Location = new System.Drawing.Point(373, 15);
            this.btnInlayer.Name = "btnInlayer";
            this.btnInlayer.Size = new System.Drawing.Size(96, 20);
            this.btnInlayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInlayer.TabIndex = 32;
            this.btnInlayer.Text = "导入符号图层";
            this.btnInlayer.Click += new System.EventHandler(this.btnInlayer_Click);
            // 
            // txtInlayer
            // 
            this.txtInlayer.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtInlayer.Border.Class = "TextBoxBorder";
            this.txtInlayer.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInlayer.Enabled = false;
            this.txtInlayer.Location = new System.Drawing.Point(160, 15);
            this.txtInlayer.Name = "txtInlayer";
            this.txtInlayer.ReadOnly = true;
            this.txtInlayer.Size = new System.Drawing.Size(196, 21);
            this.txtInlayer.TabIndex = 33;
            // 
            // FrmUnique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(624, 518);
            this.Controls.Add(this.txtInlayer);
            this.Controls.Add(this.btnInlayer);
            this.Controls.Add(this.gridviewmu);
            this.Controls.Add(this.grpanelcolormu);
            this.Controls.Add(this.grpanelcolor);
            this.Controls.Add(this.grPanelfield);
            this.Controls.Add(this.grpanelfieldmu);
            this.Controls.Add(this.btnuse);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.btnremove);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.btnremoveall);
            this.Controls.Add(this.btnaddall);
            this.Controls.Add(this.axSymbologyControl1);
            this.Controls.Add(this.treeshow);
            this.Controls.Add(this.lblshow);
            this.Controls.Add(this.gridviewu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUnique";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "独立值渲染";
            this.Load += new System.EventHandler(this.FrmUnique_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeshow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            this.grPanelfield.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridviewu)).EndInit();
            this.grpanelfieldmu.ResumeLayout(false);
            this.grpanelcolor.ResumeLayout(false);
            this.grpanelcolormu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridviewmu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataTable dataTable2;
        private System.Data.DataTable dataTable3;
        private DevComponents.DotNetBar.LabelX lblshow;
        private DevComponents.AdvTree.AdvTree treeshow;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.Node node2;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private DevComponents.DotNetBar.Controls.GroupPanel grPanelfield;
        private System.Windows.Forms.ComboBox cmbfield;
        private DevComponents.DotNetBar.Controls.GroupPanel grpanelfieldmu;
        private System.Windows.Forms.ComboBox cmbfield1;
        private DevComponents.DotNetBar.Controls.DataGridViewX gridviewu;
        private DevComponents.DotNetBar.ButtonX btnaddall;
        private DevComponents.DotNetBar.ButtonX btnremoveall;
        private DevComponents.DotNetBar.ButtonX btnadd;
        private DevComponents.DotNetBar.ButtonX btnremove;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.ButtonX btnuse;
        private System.Windows.Forms.ComboBox cmbfield3;
        private System.Windows.Forms.ComboBox cmbfield2;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 值DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量DataGridViewTextBoxColumn1;
        private DevComponents.DotNetBar.Controls.GroupPanel grpanelcolor;
        private ComboboxSymbol cmbcolor;
        private DevComponents.DotNetBar.Controls.GroupPanel grpanelcolormu;
        private ComboboxSymbol cmbcolormu;
        private DevComponents.DotNetBar.Controls.DataGridViewX gridviewmu;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 值DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private DevComponents.DotNetBar.ButtonX btnInlayer;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInlayer;
    }
}