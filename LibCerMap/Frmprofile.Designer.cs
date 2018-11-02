namespace LibCerMap
{
    partial class Frmprofile
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmblayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.inoffset = new DevComponents.Editors.DoubleInput();
            ((System.ComponentModel.ISupportInitialize)(this.inoffset)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(35, 32);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(86, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "地      形 ：";
            // 
            // cmblayer
            // 
            this.cmblayer.DisplayMember = "Text";
            this.cmblayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmblayer.FormattingEnabled = true;
            this.cmblayer.ItemHeight = 15;
            this.cmblayer.Location = new System.Drawing.Point(127, 32);
            this.cmblayer.Name = "cmblayer";
            this.cmblayer.Size = new System.Drawing.Size(144, 21);
            this.cmblayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmblayer.TabIndex = 1;
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(85, 139);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(76, 24);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 2;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(193, 139);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(76, 24);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 3;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(35, 83);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(86, 21);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "巡视器轮距 ：";
            // 
            // inoffset
            // 
            // 
            // 
            // 
            this.inoffset.BackgroundStyle.Class = "DateTimeInputBackground";
            this.inoffset.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.inoffset.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.inoffset.Increment = 0.1D;
            this.inoffset.Location = new System.Drawing.Point(127, 83);
            this.inoffset.MaxValue = 3D;
            this.inoffset.MinValue = 0D;
            this.inoffset.Name = "inoffset";
            this.inoffset.ShowUpDown = true;
            this.inoffset.Size = new System.Drawing.Size(144, 21);
            this.inoffset.TabIndex = 20;
            this.inoffset.Value = 1.5D;
            // 
            // Frmprofile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 192);
            this.Controls.Add(this.inoffset);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.cmblayer);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frmprofile";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地形剖面图";
            this.Load += new System.EventHandler(this.Frmprofile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inoffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmblayer;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DoubleInput inoffset;
    }
}