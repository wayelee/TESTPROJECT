namespace LibCerMap
{
    partial class FrmSceneLayerOffest
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
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.labelXOffset = new DevComponents.DotNetBar.LabelX();
            this.numericUpDownOffset = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(171, 96);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(75, 23);
            this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXOK.TabIndex = 0;
            this.buttonXOK.Text = "确定";
            this.buttonXOK.Click += new System.EventHandler(this.buttonXOK_Click);
            // 
            // labelXOffset
            // 
            // 
            // 
            // 
            this.labelXOffset.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXOffset.Location = new System.Drawing.Point(23, 36);
            this.labelXOffset.Name = "labelXOffset";
            this.labelXOffset.Size = new System.Drawing.Size(86, 23);
            this.labelXOffset.TabIndex = 2;
            this.labelXOffset.Text = "图层高程偏移";
            // 
            // numericUpDownOffset
            // 
            this.numericUpDownOffset.DecimalPlaces = 8;
            this.numericUpDownOffset.Location = new System.Drawing.Point(147, 40);
            this.numericUpDownOffset.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownOffset.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.numericUpDownOffset.Name = "numericUpDownOffset";
            this.numericUpDownOffset.Size = new System.Drawing.Size(99, 21);
            this.numericUpDownOffset.TabIndex = 3;
            this.numericUpDownOffset.ValueChanged += new System.EventHandler(this.numericUpDownOffset_ValueChanged);
            // 
            // FrmSceneLayerOffest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 140);
            this.Controls.Add(this.numericUpDownOffset);
            this.Controls.Add(this.labelXOffset);
            this.Controls.Add(this.buttonXOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSceneLayerOffest";
            this.ShowInTaskbar = false;
            this.Text = "设置图层高度偏移";
            this.Load += new System.EventHandler(this.FrmSceneLayerOffest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.LabelX labelXOffset;
        private System.Windows.Forms.NumericUpDown numericUpDownOffset;
    }
}