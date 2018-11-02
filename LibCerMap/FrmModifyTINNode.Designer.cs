namespace LibCerMap
{
    partial class FrmModifyTINNode
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
            this.doubleInput1 = new DevComponents.Editors.DoubleInput();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInput1)).BeginInit();
            this.SuspendLayout();
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
            this.doubleInput1.Location = new System.Drawing.Point(12, 21);
            this.doubleInput1.Name = "doubleInput1";
            this.doubleInput1.ShowUpDown = true;
            this.doubleInput1.Size = new System.Drawing.Size(80, 21);
            this.doubleInput1.TabIndex = 0;
            this.doubleInput1.ValueChanged += new System.EventHandler(this.doubleInput1_ValueChanged);
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.Enabled = false;
            this.buttonXOK.Location = new System.Drawing.Point(121, 21);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(75, 23);
            this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXOK.TabIndex = 1;
            this.buttonXOK.Text = "确定";
            this.buttonXOK.Click += new System.EventHandler(this.buttonXOK_Click);
            // 
            // FrmModifyTINNode
            // 
            this.AcceptButton = this.buttonXOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 65);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.doubleInput1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmModifyTINNode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改TIN节点";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmModifyTINNode_FormClosing);
            this.Load += new System.EventHandler(this.FrmModifyTINNode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.doubleInput1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.DoubleInput doubleInput1;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
    }
}