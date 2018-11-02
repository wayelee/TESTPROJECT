namespace LibCerMap
{
    partial class FrmSymbolRGB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSymbolRGB));
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.btnuse = new DevComponents.DotNetBar.ButtonX();
            this.gridsymbol = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cmbred = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbgreen = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbblue = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbalpha = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.treeshow = new DevComponents.AdvTree.AdvTree();
            this.NodeStreach = new DevComponents.AdvTree.Node();
            this.NodeRGB = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.lblband = new DevComponents.DotNetBar.LabelX();
            this.cmbBand = new System.Windows.Forms.ComboBox();
            this.groupstr = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtmax = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtmin = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblmaxstr = new DevComponents.DotNetBar.LabelX();
            this.lblstr2 = new DevComponents.DotNetBar.LabelX();
            this.lblminstr = new DevComponents.DotNetBar.LabelX();
            this.lblstr1 = new DevComponents.DotNetBar.LabelX();
            this.lblColorRamp = new DevComponents.DotNetBar.LabelX();
            this.cmbColorRamp = new LibCerMap.FrmSymbolRGB.ComboboxSymbol();
            this.lblstreach = new DevComponents.DotNetBar.LabelX();
            this.cmbStreach = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.colbackg = new CustomColorPicker();
            this.backas = new DevComponents.DotNetBar.LabelX();
            this.backvalue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkdisplay = new DevComponents.DotNetBar.Controls.CheckBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.gridsymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeshow)).BeginInit();
            this.groupstr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(158, 262);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(69, 24);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 8;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(261, 262);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(71, 24);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 9;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // btnuse
            // 
            this.btnuse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnuse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnuse.Location = new System.Drawing.Point(360, 262);
            this.btnuse.Name = "btnuse";
            this.btnuse.Size = new System.Drawing.Size(69, 24);
            this.btnuse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnuse.TabIndex = 10;
            this.btnuse.Text = "应用";
            this.btnuse.Click += new System.EventHandler(this.btnuse_Click);
            // 
            // gridsymbol
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridsymbol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridsymbol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridsymbol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridsymbol.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridsymbol.EnableHeadersVisualStyles = false;
            this.gridsymbol.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.gridsymbol.Location = new System.Drawing.Point(136, 12);
            this.gridsymbol.Name = "gridsymbol";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridsymbol.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridsymbol.RowHeadersVisible = false;
            this.gridsymbol.RowTemplate.Height = 23;
            this.gridsymbol.ScrollBarAppearance = DevComponents.DotNetBar.eScrollBarAppearance.Default;
            this.gridsymbol.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gridsymbol.Size = new System.Drawing.Size(318, 109);
            this.gridsymbol.TabIndex = 14;
            this.gridsymbol.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridsymbol_DataError);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "选择";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.Width = 36;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "颜色通道";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 88;
            // 
            // Column2
            // 
            this.Column2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Column2.HeaderText = "波段";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column2.Width = 190;
            // 
            // cmbred
            // 
            this.cmbred.DisplayMember = "Text";
            this.cmbred.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbred.FormattingEnabled = true;
            this.cmbred.ItemHeight = 15;
            this.cmbred.Location = new System.Drawing.Point(261, 31);
            this.cmbred.Name = "cmbred";
            this.cmbred.Size = new System.Drawing.Size(192, 21);
            this.cmbred.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbred.TabIndex = 15;
            // 
            // cmbgreen
            // 
            this.cmbgreen.DisplayMember = "Text";
            this.cmbgreen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbgreen.FormattingEnabled = true;
            this.cmbgreen.ItemHeight = 15;
            this.cmbgreen.Location = new System.Drawing.Point(261, 54);
            this.cmbgreen.Name = "cmbgreen";
            this.cmbgreen.Size = new System.Drawing.Size(192, 21);
            this.cmbgreen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbgreen.TabIndex = 16;
            // 
            // cmbblue
            // 
            this.cmbblue.DisplayMember = "Text";
            this.cmbblue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbblue.FormattingEnabled = true;
            this.cmbblue.ItemHeight = 15;
            this.cmbblue.Location = new System.Drawing.Point(261, 77);
            this.cmbblue.Name = "cmbblue";
            this.cmbblue.Size = new System.Drawing.Size(192, 21);
            this.cmbblue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbblue.TabIndex = 17;
            // 
            // cmbalpha
            // 
            this.cmbalpha.DisplayMember = "Text";
            this.cmbalpha.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbalpha.FormattingEnabled = true;
            this.cmbalpha.ItemHeight = 15;
            this.cmbalpha.Location = new System.Drawing.Point(261, 100);
            this.cmbalpha.Name = "cmbalpha";
            this.cmbalpha.Size = new System.Drawing.Size(192, 21);
            this.cmbalpha.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbalpha.TabIndex = 18;
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
            this.treeshow.Location = new System.Drawing.Point(2, 12);
            this.treeshow.Name = "treeshow";
            this.treeshow.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.NodeStreach,
            this.NodeRGB});
            this.treeshow.NodesConnector = this.nodeConnector1;
            this.treeshow.NodeStyle = this.elementStyle1;
            this.treeshow.PathSeparator = ";";
            this.treeshow.Size = new System.Drawing.Size(111, 172);
            this.treeshow.Styles.Add(this.elementStyle1);
            this.treeshow.TabIndex = 19;
            this.treeshow.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.treeshow_AfterNodeSelect);
            // 
            // NodeStreach
            // 
            this.NodeStreach.Expanded = true;
            this.NodeStreach.Name = "NodeStreach";
            this.NodeStreach.Text = "拉伸";
            // 
            // NodeRGB
            // 
            this.NodeRGB.Expanded = true;
            this.NodeRGB.Name = "NodeRGB";
            this.NodeRGB.Text = "RGB组合";
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
            // lblband
            // 
            // 
            // 
            // 
            this.lblband.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblband.Location = new System.Drawing.Point(136, 12);
            this.lblband.Name = "lblband";
            this.lblband.Size = new System.Drawing.Size(46, 23);
            this.lblband.TabIndex = 20;
            this.lblband.Text = "波段 :";
            // 
            // cmbBand
            // 
            this.cmbBand.FormattingEnabled = true;
            this.cmbBand.Location = new System.Drawing.Point(214, 12);
            this.cmbBand.Name = "cmbBand";
            this.cmbBand.Size = new System.Drawing.Size(172, 20);
            this.cmbBand.TabIndex = 21;
            this.cmbBand.SelectedIndexChanged += new System.EventHandler(this.cmbBand_SelectedIndexChanged);
            // 
            // groupstr
            // 
            this.groupstr.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupstr.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupstr.Controls.Add(this.txtmax);
            this.groupstr.Controls.Add(this.txtmin);
            this.groupstr.Controls.Add(this.lblmaxstr);
            this.groupstr.Controls.Add(this.lblstr2);
            this.groupstr.Controls.Add(this.lblminstr);
            this.groupstr.Controls.Add(this.lblstr1);
            this.groupstr.Location = new System.Drawing.Point(136, 42);
            this.groupstr.Name = "groupstr";
            this.groupstr.Size = new System.Drawing.Size(317, 102);
            // 
            // 
            // 
            this.groupstr.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupstr.Style.BackColorGradientAngle = 90;
            this.groupstr.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupstr.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupstr.Style.BorderBottomWidth = 1;
            this.groupstr.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupstr.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupstr.Style.BorderLeftWidth = 1;
            this.groupstr.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupstr.Style.BorderRightWidth = 1;
            this.groupstr.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupstr.Style.BorderTopWidth = 1;
            this.groupstr.Style.CornerDiameter = 4;
            this.groupstr.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupstr.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupstr.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupstr.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupstr.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupstr.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupstr.TabIndex = 22;
            this.groupstr.Text = "统计值";
            this.groupstr.Visible = false;
            // 
            // txtmax
            // 
            // 
            // 
            // 
            this.txtmax.Border.Class = "TextBoxBorder";
            this.txtmax.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtmax.Location = new System.Drawing.Point(87, 46);
            this.txtmax.Name = "txtmax";
            this.txtmax.Size = new System.Drawing.Size(116, 21);
            this.txtmax.TabIndex = 9;
            this.txtmax.Visible = false;
            // 
            // txtmin
            // 
            // 
            // 
            // 
            this.txtmin.Border.Class = "TextBoxBorder";
            this.txtmin.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtmin.Location = new System.Drawing.Point(87, 6);
            this.txtmin.Name = "txtmin";
            this.txtmin.Size = new System.Drawing.Size(116, 21);
            this.txtmin.TabIndex = 8;
            this.txtmin.Visible = false;
            // 
            // lblmaxstr
            // 
            this.lblmaxstr.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblmaxstr.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblmaxstr.Location = new System.Drawing.Point(87, 46);
            this.lblmaxstr.Name = "lblmaxstr";
            this.lblmaxstr.Size = new System.Drawing.Size(116, 21);
            this.lblmaxstr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lblmaxstr.TabIndex = 3;
            // 
            // lblstr2
            // 
            this.lblstr2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblstr2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblstr2.Location = new System.Drawing.Point(25, 46);
            this.lblstr2.Name = "lblstr2";
            this.lblstr2.Size = new System.Drawing.Size(48, 26);
            this.lblstr2.TabIndex = 2;
            this.lblstr2.Text = "最大值";
            // 
            // lblminstr
            // 
            this.lblminstr.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lblminstr.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblminstr.Location = new System.Drawing.Point(87, 6);
            this.lblminstr.Name = "lblminstr";
            this.lblminstr.Size = new System.Drawing.Size(116, 21);
            this.lblminstr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lblminstr.TabIndex = 1;
            // 
            // lblstr1
            // 
            this.lblstr1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblstr1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblstr1.Location = new System.Drawing.Point(25, 3);
            this.lblstr1.Name = "lblstr1";
            this.lblstr1.Size = new System.Drawing.Size(48, 26);
            this.lblstr1.TabIndex = 0;
            this.lblstr1.Text = "最小值";
            // 
            // lblColorRamp
            // 
            // 
            // 
            // 
            this.lblColorRamp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblColorRamp.Location = new System.Drawing.Point(136, 158);
            this.lblColorRamp.Name = "lblColorRamp";
            this.lblColorRamp.Size = new System.Drawing.Size(55, 23);
            this.lblColorRamp.TabIndex = 23;
            this.lblColorRamp.Text = "颜色带：";
            // 
            // cmbColorRamp
            // 
            this.cmbColorRamp.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbColorRamp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbColorRamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorRamp.FormattingEnabled = true;
            this.cmbColorRamp.Location = new System.Drawing.Point(214, 158);
            this.cmbColorRamp.Name = "cmbColorRamp";
            this.cmbColorRamp.Size = new System.Drawing.Size(239, 22);
            this.cmbColorRamp.TabIndex = 24;
            this.cmbColorRamp.SelectedIndexChanged += new System.EventHandler(this.cmbColorRamp_SelectedIndexChanged);
            // 
            // lblstreach
            // 
            // 
            // 
            // 
            this.lblstreach.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblstreach.Location = new System.Drawing.Point(136, 196);
            this.lblstreach.Name = "lblstreach";
            this.lblstreach.Size = new System.Drawing.Size(63, 23);
            this.lblstreach.TabIndex = 25;
            this.lblstreach.Text = "拉伸方式:";
            // 
            // cmbStreach
            // 
            this.cmbStreach.DisplayMember = "Text";
            this.cmbStreach.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStreach.FormattingEnabled = true;
            this.cmbStreach.ItemHeight = 15;
            this.cmbStreach.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5});
            this.cmbStreach.Location = new System.Drawing.Point(214, 196);
            this.cmbStreach.Name = "cmbStreach";
            this.cmbStreach.Size = new System.Drawing.Size(171, 21);
            this.cmbStreach.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbStreach.TabIndex = 26;
            this.cmbStreach.SelectedIndexChanged += new System.EventHandler(this.cmbStreach_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "无";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "通用";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "最大值-最小值";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "标准差";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "直方图均衡化";
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Location = new System.Drawing.Point(2, 196);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(111, 60);
            this.axSymbologyControl1.TabIndex = 27;
            this.axSymbologyControl1.Visible = false;
            // 
            // colbackg
            // 
            this.colbackg.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.colbackg.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.colbackg.Enabled = false;
            this.colbackg.Image = ((System.Drawing.Image)(resources.GetObject("colbackg.Image")));
            this.colbackg.Location = new System.Drawing.Point(396, 221);
            this.colbackg.Name = "colbackg";
            this.colbackg.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.colbackg.Size = new System.Drawing.Size(37, 23);
            this.colbackg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.colbackg.TabIndex = 36;
            // 
            // backas
            // 
            // 
            // 
            // 
            this.backas.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.backas.Location = new System.Drawing.Point(368, 223);
            this.backas.Name = "backas";
            this.backas.Size = new System.Drawing.Size(22, 21);
            this.backas.TabIndex = 35;
            this.backas.Text = "as";
            // 
            // backvalue
            // 
            // 
            // 
            // 
            this.backvalue.Border.Class = "TextBoxBorder";
            this.backvalue.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.backvalue.Enabled = false;
            this.backvalue.Location = new System.Drawing.Point(248, 223);
            this.backvalue.Name = "backvalue";
            this.backvalue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.backvalue.Size = new System.Drawing.Size(86, 21);
            this.backvalue.TabIndex = 34;
            this.backvalue.Text = "255";
            // 
            // chkdisplay
            // 
            // 
            // 
            // 
            this.chkdisplay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkdisplay.Location = new System.Drawing.Point(136, 221);
            this.chkdisplay.Name = "chkdisplay";
            this.chkdisplay.Size = new System.Drawing.Size(91, 23);
            this.chkdisplay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkdisplay.TabIndex = 33;
            this.chkdisplay.Text = "显示背景值";
            this.chkdisplay.CheckedChanged += new System.EventHandler(this.chkdisplay_CheckedChanged);
            // 
            // FrmSymbolRGB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(460, 294);
            this.Controls.Add(this.colbackg);
            this.Controls.Add(this.backas);
            this.Controls.Add(this.backvalue);
            this.Controls.Add(this.chkdisplay);
            this.Controls.Add(this.axSymbologyControl1);
            this.Controls.Add(this.cmbStreach);
            this.Controls.Add(this.lblstreach);
            this.Controls.Add(this.cmbColorRamp);
            this.Controls.Add(this.lblColorRamp);
            this.Controls.Add(this.groupstr);
            this.Controls.Add(this.cmbBand);
            this.Controls.Add(this.lblband);
            this.Controls.Add(this.treeshow);
            this.Controls.Add(this.cmbalpha);
            this.Controls.Add(this.cmbblue);
            this.Controls.Add(this.cmbgreen);
            this.Controls.Add(this.cmbred);
            this.Controls.Add(this.gridsymbol);
            this.Controls.Add(this.btnuse);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSymbolRGB";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RGB色彩合成";
            this.Load += new System.EventHandler(this.FrmSymbolRGB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridsymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeshow)).EndInit();
            this.groupstr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.ButtonX btnuse;
        private DevComponents.DotNetBar.Controls.DataGridViewX gridsymbol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbred;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbgreen;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbblue;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbalpha;
        private DevComponents.AdvTree.AdvTree treeshow;
        private DevComponents.AdvTree.Node NodeStreach;
        private DevComponents.AdvTree.Node NodeRGB;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.LabelX lblband;
        private System.Windows.Forms.ComboBox cmbBand;
        private DevComponents.DotNetBar.LabelX lblstreach;
        private DevComponents.DotNetBar.Controls.GroupPanel groupstr;
        private DevComponents.DotNetBar.Controls.TextBoxX txtmax;
        private DevComponents.DotNetBar.Controls.TextBoxX txtmin;
        private DevComponents.DotNetBar.LabelX lblmaxstr;
        private DevComponents.DotNetBar.LabelX lblstr2;
        private DevComponents.DotNetBar.LabelX lblminstr;
        private DevComponents.DotNetBar.LabelX lblstr1;
        private DevComponents.DotNetBar.LabelX lblColorRamp;
        private FrmSymbolRGB.ComboboxSymbol cmbColorRamp;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbStreach;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private CustomColorPicker colbackg;
        private DevComponents.DotNetBar.LabelX backas;
        private DevComponents.DotNetBar.Controls.TextBoxX backvalue;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkdisplay;
    }
}