namespace LibCerMap
{
    partial class FrmLayerProperties
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
            this.components = new System.ComponentModel.Container();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.tabCtlAttribute = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.txtCoordinateSystem = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnChangeCoordinate = new DevComponents.DotNetBar.ButtonX();
            this.tab = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.advTreeProps = new DevComponents.AdvTree.AdvTree();
            this.ColProps = new DevComponents.AdvTree.ColumnHeader();
            this.ColPropsValue = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtBoxDataSource = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tabItemDataSource = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.superGridFields = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.tabFields = new DevComponents.DotNetBar.TabItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabCtlAttribute)).BeginInit();
            this.tabCtlAttribute.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advTreeProps)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtlAttribute
            // 
            this.tabCtlAttribute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabCtlAttribute.CanReorderTabs = true;
            this.tabCtlAttribute.Controls.Add(this.tabControlPanel1);
            this.tabCtlAttribute.Controls.Add(this.tabControlPanel3);
            this.tabCtlAttribute.Controls.Add(this.tabControlPanel2);
            this.tabCtlAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlAttribute.Location = new System.Drawing.Point(0, 0);
            this.tabCtlAttribute.Name = "tabCtlAttribute";
            this.tabCtlAttribute.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabCtlAttribute.SelectedTabIndex = 0;
            this.tabCtlAttribute.Size = new System.Drawing.Size(414, 450);
            this.tabCtlAttribute.TabIndex = 0;
            this.tabCtlAttribute.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabCtlAttribute.Tabs.Add(this.tabItemDataSource);
            this.tabCtlAttribute.Tabs.Add(this.tab);
            this.tabCtlAttribute.Tabs.Add(this.tabFields);
            this.tabCtlAttribute.Text = "tabControl1";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.txtCoordinateSystem);
            this.tabControlPanel2.Controls.Add(this.panelEx1);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(414, 424);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tab;
            // 
            // txtCoordinateSystem
            // 
            // 
            // 
            // 
            this.txtCoordinateSystem.Border.Class = "TextBoxBorder";
            this.txtCoordinateSystem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCoordinateSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCoordinateSystem.Location = new System.Drawing.Point(1, 1);
            this.txtCoordinateSystem.Multiline = true;
            this.txtCoordinateSystem.Name = "txtCoordinateSystem";
            this.txtCoordinateSystem.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtCoordinateSystem.Size = new System.Drawing.Size(412, 350);
            this.txtCoordinateSystem.TabIndex = 2;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnChangeCoordinate);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx1.Location = new System.Drawing.Point(1, 351);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(412, 72);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnChangeCoordinate
            // 
            this.btnChangeCoordinate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChangeCoordinate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChangeCoordinate.Location = new System.Drawing.Point(276, 23);
            this.btnChangeCoordinate.Name = "btnChangeCoordinate";
            this.btnChangeCoordinate.Size = new System.Drawing.Size(125, 38);
            this.btnChangeCoordinate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChangeCoordinate.TabIndex = 0;
            this.btnChangeCoordinate.Text = "更改";
            this.btnChangeCoordinate.Click += new System.EventHandler(this.btnChangeCoordinate_Click);
            // 
            // tab
            // 
            this.tab.AttachedControl = this.tabControlPanel2;
            this.tab.Name = "tab";
            this.tab.Text = "坐标系统";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.groupPanel2);
            this.tabControlPanel1.Controls.Add(this.groupPanel1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(414, 424);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItemDataSource;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.advTreeProps);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Location = new System.Drawing.Point(1, 144);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(412, 279);
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
            this.groupPanel2.TabIndex = 1;
            // 
            // advTreeProps
            // 
            this.advTreeProps.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTreeProps.AllowDrop = true;
            this.advTreeProps.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTreeProps.BackgroundStyle.Class = "TreeBorderKey";
            this.advTreeProps.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advTreeProps.Columns.Add(this.ColProps);
            this.advTreeProps.Columns.Add(this.ColPropsValue);
            this.advTreeProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advTreeProps.GridLinesColor = System.Drawing.Color.LightGray;
            this.advTreeProps.GridRowLines = true;
            this.advTreeProps.Location = new System.Drawing.Point(0, 0);
            this.advTreeProps.Name = "advTreeProps";
            this.advTreeProps.NodesConnector = this.nodeConnector1;
            this.advTreeProps.NodeStyle = this.elementStyle1;
            this.advTreeProps.PathSeparator = ";";
            this.advTreeProps.Size = new System.Drawing.Size(406, 273);
            this.advTreeProps.Styles.Add(this.elementStyle1);
            this.advTreeProps.TabIndex = 0;
            this.advTreeProps.Text = "advTree1";
            // 
            // ColProps
            // 
            this.ColProps.Name = "ColProps";
            this.ColProps.Text = "属性";
            this.ColProps.Width.Absolute = 150;
            // 
            // ColPropsValue
            // 
            this.ColPropsValue.Name = "ColPropsValue";
            this.ColPropsValue.Text = "值";
            this.ColPropsValue.Width.Absolute = 230;
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
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtBoxDataSource);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(1, 1);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(412, 143);
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
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            // 
            // txtBoxDataSource
            // 
            // 
            // 
            // 
            this.txtBoxDataSource.Border.Class = "TextBoxBorder";
            this.txtBoxDataSource.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBoxDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxDataSource.Location = new System.Drawing.Point(0, 0);
            this.txtBoxDataSource.Multiline = true;
            this.txtBoxDataSource.Name = "txtBoxDataSource";
            this.txtBoxDataSource.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBoxDataSource.Size = new System.Drawing.Size(406, 137);
            this.txtBoxDataSource.TabIndex = 0;
            // 
            // tabItemDataSource
            // 
            this.tabItemDataSource.AttachedControl = this.tabControlPanel1;
            this.tabItemDataSource.Name = "tabItemDataSource";
            this.tabItemDataSource.Text = "数据源";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this.superGridFields);
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(414, 424);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 3;
            this.tabControlPanel3.TabItem = this.tabFields;
            // 
            // superGridFields
            // 
            this.superGridFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridFields.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridFields.Location = new System.Drawing.Point(1, 1);
            this.superGridFields.Name = "superGridFields";
            gridColumn1.Name = "字段名";
            gridColumn1.Width = 150;
            gridColumn2.Name = "类型";
            gridColumn3.Name = "长度";
            this.superGridFields.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridFields.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridFields.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridFields.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridFields.Size = new System.Drawing.Size(412, 422);
            this.superGridFields.TabIndex = 3;
            // 
            // tabFields
            // 
            this.tabFields.AttachedControl = this.tabControlPanel3;
            this.tabFields.Name = "tabFields";
            this.tabFields.Text = "字段";
            // 
            // FrmLayerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 450);
            this.Controls.Add(this.tabCtlAttribute);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLayerProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "图层属性";
            this.Load += new System.EventHandler(this.FrmLayerProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabCtlAttribute)).EndInit();
            this.tabCtlAttribute.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advTreeProps)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.tabControlPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabCtlAttribute;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tabFields;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tab;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItemDataSource;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBoxDataSource;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridFields;
        private DevComponents.AdvTree.AdvTree advTreeProps;
        private DevComponents.AdvTree.ColumnHeader ColProps;
        private DevComponents.AdvTree.ColumnHeader ColPropsValue;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCoordinateSystem;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnChangeCoordinate;


    }
}