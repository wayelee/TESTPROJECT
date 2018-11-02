namespace LibCerMap
{
    partial class FrmParallelLineOffset
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numOffset = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(37, 56);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(130, 56);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numOffset
            // 
            this.numOffset.DecimalPlaces = 2;
            this.numOffset.Location = new System.Drawing.Point(37, 12);
            this.numOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numOffset.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numOffset.Name = "numOffset";
            this.numOffset.Size = new System.Drawing.Size(154, 21);
            this.numOffset.TabIndex = 2;
            this.numOffset.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numOffset.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // FrmParallelLineOffset
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 100);
            this.ControlBox = false;
            this.Controls.Add(this.numOffset);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Name = "FrmParallelLineOffset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置偏移距离";
            this.Load += new System.EventHandler(this.FrmCreateRasterBandLayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numOffset;
    }
}