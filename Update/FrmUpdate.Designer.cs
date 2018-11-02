namespace update
{
    partial class update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(update));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.line1 = new System.Windows.Forms.Label();
            this.line2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 43);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(402, 21);
            this.progressBar1.TabIndex = 0;
            // 
            // line1
            // 
            this.line1.AutoSize = true;
            this.line1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.line1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.line1.Location = new System.Drawing.Point(12, 8);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(11, 16);
            this.line1.TabIndex = 3;
            this.line1.Text = " ";
            // 
            // line2
            // 
            this.line2.AutoSize = true;
            this.line2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.line2.Location = new System.Drawing.Point(0, 8);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(0, 16);
            this.line2.TabIndex = 4;
            // 
            // update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(402, 64);
            this.ControlBox = false;
            this.Controls.Add(this.line2);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.progressBar1);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "³ÌÐò¸üÐÂ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label line1;
        private System.Windows.Forms.Label line2;
    }
}

