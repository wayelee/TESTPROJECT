namespace LibCheckUpdate
{
    partial class UpdateMe
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateMe));
            this.Update_bttn = new System.Windows.Forms.Button();
            this.downloadsurl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.versionfilename = new System.Windows.Forms.TextBox();
            this.checkForUpdate_bttn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.thisversion = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.updateResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Update_bttn
            // 
            this.Update_bttn.BackColor = System.Drawing.Color.Gold;
            this.Update_bttn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Update_bttn.Location = new System.Drawing.Point(235, 167);
            this.Update_bttn.Name = "Update_bttn";
            this.Update_bttn.Size = new System.Drawing.Size(250, 33);
            this.Update_bttn.TabIndex = 4;
            this.Update_bttn.Text = "执行更新";
            this.Update_bttn.UseVisualStyleBackColor = false;
            this.Update_bttn.Click += new System.EventHandler(this.update_bttn_Click);
            // 
            // downloadsurl
            // 
            this.downloadsurl.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadsurl.Location = new System.Drawing.Point(98, 46);
            this.downloadsurl.Name = "downloadsurl";
            this.downloadsurl.Size = new System.Drawing.Size(387, 23);
            this.downloadsurl.TabIndex = 0;
            this.downloadsurl.Text = "\\\\11.10.70.43\\CERMapping_Update\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "更新地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "版本文件：";
            // 
            // versionfilename
            // 
            this.versionfilename.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionfilename.Location = new System.Drawing.Point(98, 82);
            this.versionfilename.Name = "versionfilename";
            this.versionfilename.Size = new System.Drawing.Size(387, 23);
            this.versionfilename.TabIndex = 1;
            this.versionfilename.Text = "Update.txt";
            // 
            // checkForUpdate_bttn
            // 
            this.checkForUpdate_bttn.BackColor = System.Drawing.Color.Cornsilk;
            this.checkForUpdate_bttn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkForUpdate_bttn.Location = new System.Drawing.Point(98, 167);
            this.checkForUpdate_bttn.Name = "checkForUpdate_bttn";
            this.checkForUpdate_bttn.Size = new System.Drawing.Size(134, 33);
            this.checkForUpdate_bttn.TabIndex = 3;
            this.checkForUpdate_bttn.Text = "更新检查";
            this.checkForUpdate_bttn.UseVisualStyleBackColor = false;
            this.checkForUpdate_bttn.Click += new System.EventHandler(this.checkForUpdate_bttn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "当前版本";
            // 
            // thisversion
            // 
            this.thisversion.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thisversion.Location = new System.Drawing.Point(98, 117);
            this.thisversion.Name = "thisversion";
            this.thisversion.Size = new System.Drawing.Size(387, 23);
            this.thisversion.TabIndex = 2;
            this.thisversion.Text = "1.001";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 20000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 20;
            this.toolTip1.ToolTipTitle = "Tip:";
            // 
            // updateResult
            // 
            this.updateResult.AutoSize = true;
            this.updateResult.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateResult.ForeColor = System.Drawing.Color.Lime;
            this.updateResult.Location = new System.Drawing.Point(95, 15);
            this.updateResult.Name = "updateResult";
            this.updateResult.Size = new System.Drawing.Size(127, 18);
            this.updateResult.TabIndex = 11;
            this.updateResult.Text = "更新成功完成！";
            // 
            // UpdateMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(497, 227);
            this.Controls.Add(this.updateResult);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.thisversion);
            this.Controls.Add(this.checkForUpdate_bttn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.versionfilename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.downloadsurl);
            this.Controls.Add(this.Update_bttn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(549, 304);
            this.MinimizeBox = false;
            this.Name = "UpdateMe";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "程序更新";
            this.Load += new System.EventHandler(this.UpdateMe_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Update_bttn;
        private System.Windows.Forms.TextBox downloadsurl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox versionfilename;
        private System.Windows.Forms.Button checkForUpdate_bttn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox thisversion;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label updateResult;
    }
}

