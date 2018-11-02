namespace LibCerMap
{
    partial class FrmSelectSet
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
            this.btncreat = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(118, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "选中要素所属图层：";
            // 
            // cmblayer
            // 
            this.cmblayer.DisplayMember = "Text";
            this.cmblayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmblayer.FormattingEnabled = true;
            this.cmblayer.ItemHeight = 15;
            this.cmblayer.Location = new System.Drawing.Point(12, 41);
            this.cmblayer.Name = "cmblayer";
            this.cmblayer.Size = new System.Drawing.Size(213, 21);
            this.cmblayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmblayer.TabIndex = 1;
            // 
            // btncreat
            // 
            this.btncreat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncreat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncreat.Location = new System.Drawing.Point(149, 80);
            this.btncreat.Name = "btncreat";
            this.btncreat.Size = new System.Drawing.Size(76, 23);
            this.btncreat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncreat.TabIndex = 2;
            this.btncreat.Text = "生成图层";
            this.btncreat.Click += new System.EventHandler(this.btncreat_Click);
            // 
            // FrmSelectSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 120);
            this.Controls.Add(this.btncreat);
            this.Controls.Add(this.cmblayer);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectSet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成选择集图层";
            this.Load += new System.EventHandler(this.FrmSelectSet_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmblayer;
        private DevComponents.DotNetBar.ButtonX btncreat;
    }
}