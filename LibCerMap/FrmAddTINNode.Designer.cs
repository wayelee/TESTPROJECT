namespace LibCerMap
{
    partial class FrmAddTINNode
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
            this.lblHeithtSrc = new DevComponents.DotNetBar.LabelX();
            this.lblHeight = new DevComponents.DotNetBar.LabelX();
            this.cmbHeightSrc = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.doubleInputHeight = new DevComponents.Editors.DoubleInput();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeithtSrc
            // 
            // 
            // 
            // 
            this.lblHeithtSrc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblHeithtSrc.Location = new System.Drawing.Point(29, 13);
            this.lblHeithtSrc.Name = "lblHeithtSrc";
            this.lblHeithtSrc.Size = new System.Drawing.Size(55, 23);
            this.lblHeithtSrc.TabIndex = 0;
            this.lblHeithtSrc.Text = "高度源:";
            // 
            // lblHeight
            // 
            // 
            // 
            // 
            this.lblHeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblHeight.Location = new System.Drawing.Point(28, 54);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(55, 23);
            this.lblHeight.TabIndex = 0;
            this.lblHeight.Text = "高  度:";
            // 
            // cmbHeightSrc
            // 
            this.cmbHeightSrc.DisplayMember = "Text";
            this.cmbHeightSrc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbHeightSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHeightSrc.FormattingEnabled = true;
            this.cmbHeightSrc.ItemHeight = 15;
            this.cmbHeightSrc.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cmbHeightSrc.Location = new System.Drawing.Point(94, 13);
            this.cmbHeightSrc.Name = "cmbHeightSrc";
            this.cmbHeightSrc.Size = new System.Drawing.Size(141, 21);
            this.cmbHeightSrc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbHeightSrc.TabIndex = 1;
            this.cmbHeightSrc.SelectedIndexChanged += new System.EventHandler(this.cmbHeightSrc_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "自表面";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "指定";
            // 
            // doubleInputHeight
            // 
            // 
            // 
            // 
            this.doubleInputHeight.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInputHeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInputHeight.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInputHeight.Increment = 1D;
            this.doubleInputHeight.Location = new System.Drawing.Point(94, 54);
            this.doubleInputHeight.Name = "doubleInputHeight";
            this.doubleInputHeight.ShowUpDown = true;
            this.doubleInputHeight.Size = new System.Drawing.Size(141, 21);
            this.doubleInputHeight.TabIndex = 2;
            this.doubleInputHeight.ValueChanged += new System.EventHandler(this.doubleInputHeight_ValueChanged);
            // 
            // FrmAddTINNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 101);
            this.Controls.Add(this.doubleInputHeight);
            this.Controls.Add(this.cmbHeightSrc);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblHeithtSrc);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddTINNode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加TIN节点";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAddTINNode_FormClosing);
            this.Load += new System.EventHandler(this.FrmAddTINNode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblHeithtSrc;
        private DevComponents.DotNetBar.LabelX lblHeight;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbHeightSrc;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.DoubleInput doubleInputHeight;
    }
}