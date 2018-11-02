namespace LibCerMap
{
    partial class FrmDelField
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
            this.cmbfields = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblfield = new DevComponents.DotNetBar.LabelX();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // cmbfields
            // 
            this.cmbfields.DisplayMember = "Text";
            this.cmbfields.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbfields.FormattingEnabled = true;
            this.cmbfields.ItemHeight = 15;
            this.cmbfields.Location = new System.Drawing.Point(114, 30);
            this.cmbfields.Name = "cmbfields";
            this.cmbfields.Size = new System.Drawing.Size(121, 21);
            this.cmbfields.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbfields.TabIndex = 0;
            // 
            // lblfield
            // 
            // 
            // 
            // 
            this.lblfield.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblfield.Location = new System.Drawing.Point(25, 29);
            this.lblfield.Name = "lblfield";
            this.lblfield.Size = new System.Drawing.Size(83, 22);
            this.lblfield.TabIndex = 1;
            this.lblfield.Text = "选择字段 ：";
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnok.Location = new System.Drawing.Point(45, 109);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(73, 28);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 2;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancel
            // 
            this.btncancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancel.Location = new System.Drawing.Point(162, 109);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(73, 28);
            this.btncancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancel.TabIndex = 3;
            this.btncancel.Text = "取消";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // FrmDelField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 170);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.lblfield);
            this.Controls.Add(this.cmbfields);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDelField";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "删除字段";
            this.Load += new System.EventHandler(this.FrmDelField_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbfields;
        private DevComponents.DotNetBar.LabelX lblfield;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancel;
    }
}