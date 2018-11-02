namespace LibCerMap
{
    partial class FrmAddUValue
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
            this.treeadd = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.lbladd = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.treeadd)).BeginInit();
            this.SuspendLayout();
            // 
            // treeadd
            // 
            this.treeadd.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeadd.AllowDrop = true;
            this.treeadd.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeadd.BackgroundStyle.Class = "TreeBorderKey";
            this.treeadd.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeadd.Location = new System.Drawing.Point(12, 39);
            this.treeadd.Name = "treeadd";
            this.treeadd.NodesConnector = this.nodeConnector1;
            this.treeadd.NodeStyle = this.elementStyle1;
            this.treeadd.PathSeparator = ";";
            this.treeadd.Size = new System.Drawing.Size(166, 191);
            this.treeadd.Styles.Add(this.elementStyle1);
            this.treeadd.TabIndex = 0;
            this.treeadd.Text = "advTree1";
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
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(197, 159);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(71, 27);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 1;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(197, 204);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(71, 27);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 2;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // lbladd
            // 
            // 
            // 
            // 
            this.lbladd.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbladd.Location = new System.Drawing.Point(12, 14);
            this.lbladd.Name = "lbladd";
            this.lbladd.Size = new System.Drawing.Size(148, 19);
            this.lbladd.TabIndex = 3;
            this.lbladd.Text = "请选择要增加的值：";
            // 
            // FrmAddUValue
            // 
            this.AcceptButton = this.btnok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 246);
            this.Controls.Add(this.lbladd);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.treeadd);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddUValue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAddUValue";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAddUValue_FormClosed);
            this.Load += new System.EventHandler(this.FrmAddUValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeadd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree treeadd;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.LabelX lbladd;
    }
}