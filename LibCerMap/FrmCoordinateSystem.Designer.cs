namespace LibCerMap
{
    partial class FrmCoordinateSystem
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
            this.richTextReference = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancle = new DevComponents.DotNetBar.ButtonX();
            this.advTreeCoord = new DevComponents.AdvTree.AdvTree();
            this.NodePredefined = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.btnSelPrjFile = new DevComponents.DotNetBar.ButtonX();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            ((System.ComponentModel.ISupportInitialize)(this.advTreeCoord)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextReference
            // 
            // 
            // 
            // 
            this.richTextReference.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextReference.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextReference.Location = new System.Drawing.Point(0, 0);
            this.richTextReference.Name = "richTextReference";
            this.richTextReference.Size = new System.Drawing.Size(308, 185);
            this.richTextReference.TabIndex = 5;
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImport.Location = new System.Drawing.Point(338, 34);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(60, 23);
            this.btnImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImport.TabIndex = 9;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(230, 464);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 26);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定 ";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(321, 464);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(60, 26);
            this.btnCancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancle.TabIndex = 11;
            this.btnCancle.Text = "取消 ";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // advTreeCoord
            // 
            this.advTreeCoord.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTreeCoord.AllowDrop = true;
            this.advTreeCoord.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTreeCoord.BackgroundStyle.Class = "TreeBorderKey";
            this.advTreeCoord.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advTreeCoord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advTreeCoord.Location = new System.Drawing.Point(0, 0);
            this.advTreeCoord.Name = "advTreeCoord";
            this.advTreeCoord.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.NodePredefined});
            this.advTreeCoord.NodesConnector = this.nodeConnector1;
            this.advTreeCoord.NodeStyle = this.elementStyle1;
            this.advTreeCoord.PathSeparator = ";";
            this.advTreeCoord.Size = new System.Drawing.Size(380, 198);
            this.advTreeCoord.Styles.Add(this.elementStyle1);
            this.advTreeCoord.TabIndex = 12;
            this.advTreeCoord.Text = "advTree1";
            this.advTreeCoord.BeforeExpand += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.advTreeCoord_BeforeExpand);
            this.advTreeCoord.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.advTreeCoord_NodeClick);
            // 
            // NodePredefined
            // 
            this.NodePredefined.Expanded = true;
            this.NodePredefined.Image = global::LibCerMap.Properties.Resources.Folder16;
            this.NodePredefined.Name = "NodePredefined";
            this.NodePredefined.Text = "Predefined";
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
            // btnSelPrjFile
            // 
            this.btnSelPrjFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelPrjFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelPrjFile.Location = new System.Drawing.Point(338, 81);
            this.btnSelPrjFile.Name = "btnSelPrjFile";
            this.btnSelPrjFile.Size = new System.Drawing.Size(60, 23);
            this.btnSelPrjFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelPrjFile.TabIndex = 13;
            this.btnSelPrjFile.Text = "选择";
            this.btnSelPrjFile.Click += new System.EventHandler(this.btnSelPrjFile_Click);
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClear.Location = new System.Drawing.Point(337, 196);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(61, 23);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.richTextReference);
            this.groupPanel1.Location = new System.Drawing.Point(13, 13);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(314, 209);
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
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 15;
            this.groupPanel1.Text = "当前坐标系统";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.advTreeCoord);
            this.groupPanel2.Location = new System.Drawing.Point(12, 228);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(386, 222);
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
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 16;
            this.groupPanel2.Text = "选择一个坐标系统";
            // 
            // FrmCoordinateSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 502);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSelPrjFile);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnImport);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCoordinateSystem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "坐标系统";
            this.Load += new System.EventHandler(this.FrmCoordinateSystem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.advTreeCoord)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextReference;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancle;
        private DevComponents.AdvTree.AdvTree advTreeCoord;
        private DevComponents.AdvTree.Node NodePredefined;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.ButtonX btnSelPrjFile;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
    }
}