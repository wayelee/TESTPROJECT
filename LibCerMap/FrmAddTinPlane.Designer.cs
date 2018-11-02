namespace LibCerMap
{
    partial class FrmAddTinPlane
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
            this.lblHeightSrc = new DevComponents.DotNetBar.LabelX();
            this.lblHeight = new DevComponents.DotNetBar.LabelX();
            this.comboBoxExHeightSrc = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.doubleInput1 = new DevComponents.Editors.DoubleInput();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInput1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeightSrc
            // 
            // 
            // 
            // 
            this.lblHeightSrc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblHeightSrc.Location = new System.Drawing.Point(26, 23);
            this.lblHeightSrc.Name = "lblHeightSrc";
            this.lblHeightSrc.Size = new System.Drawing.Size(56, 23);
            this.lblHeightSrc.TabIndex = 0;
            this.lblHeightSrc.Text = "高度源:";
            // 
            // lblHeight
            // 
            // 
            // 
            // 
            this.lblHeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblHeight.Location = new System.Drawing.Point(26, 62);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(56, 23);
            this.lblHeight.TabIndex = 0;
            this.lblHeight.Text = "高  度:";
            // 
            // comboBoxExHeightSrc
            // 
            this.comboBoxExHeightSrc.DisplayMember = "Text";
            this.comboBoxExHeightSrc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExHeightSrc.FormattingEnabled = true;
            this.comboBoxExHeightSrc.ItemHeight = 15;
            this.comboBoxExHeightSrc.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.comboBoxExHeightSrc.Location = new System.Drawing.Point(94, 23);
            this.comboBoxExHeightSrc.Name = "comboBoxExHeightSrc";
            this.comboBoxExHeightSrc.Size = new System.Drawing.Size(121, 21);
            this.comboBoxExHeightSrc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExHeightSrc.TabIndex = 1;
            this.comboBoxExHeightSrc.SelectedIndexChanged += new System.EventHandler(this.comboBoxExHeightSrc_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "表面最大值";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "表面最小值";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "指定";
            // 
            // doubleInput1
            // 
            // 
            // 
            // 
            this.doubleInput1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInput1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInput1.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInput1.Increment = 1D;
            this.doubleInput1.Location = new System.Drawing.Point(94, 62);
            this.doubleInput1.Name = "doubleInput1";
            this.doubleInput1.ShowUpDown = true;
            this.doubleInput1.Size = new System.Drawing.Size(121, 21);
            this.doubleInput1.TabIndex = 2;
            this.doubleInput1.ValueChanged += new System.EventHandler(this.doubleInput1_ValueChanged);
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.Location = new System.Drawing.Point(140, 102);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(75, 23);
            this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXOK.TabIndex = 3;
            this.buttonXOK.Text = "确定";
            this.buttonXOK.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // FrmAddTinPlane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 141);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.doubleInput1);
            this.Controls.Add(this.comboBoxExHeightSrc);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblHeightSrc);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddTinPlane";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加TIN平面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAddTinPlane_FormClosing);
            this.Load += new System.EventHandler(this.FrmAddTinPlane_Load);
            ((System.ComponentModel.ISupportInitialize)(this.doubleInput1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblHeightSrc;
        private DevComponents.DotNetBar.LabelX lblHeight;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExHeightSrc;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.DoubleInput doubleInput1;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
    }
}