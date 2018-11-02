namespace LibCerMap
{
    partial class FrmSetRasterDataValue
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
            this.lblSetNoDataValue = new DevComponents.DotNetBar.LabelX();
            this.dbiNoDataValue = new DevComponents.Editors.DoubleInput();
            this.btnGetFromLayer = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dbiNoDataValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSetNoDataValue
            // 
            // 
            // 
            // 
            this.lblSetNoDataValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSetNoDataValue.Location = new System.Drawing.Point(24, 21);
            this.lblSetNoDataValue.Name = "lblSetNoDataValue";
            this.lblSetNoDataValue.Size = new System.Drawing.Size(51, 23);
            this.lblSetNoDataValue.TabIndex = 0;
            this.lblSetNoDataValue.Text = "新值:";
            // 
            // dbiNoDataValue
            // 
            // 
            // 
            // 
            this.dbiNoDataValue.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbiNoDataValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbiNoDataValue.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbiNoDataValue.Increment = 1D;
            this.dbiNoDataValue.Location = new System.Drawing.Point(81, 21);
            this.dbiNoDataValue.Name = "dbiNoDataValue";
            this.dbiNoDataValue.Size = new System.Drawing.Size(113, 21);
            this.dbiNoDataValue.TabIndex = 1;
            this.dbiNoDataValue.ValueChanged += new System.EventHandler(this.dbiNoDataValue_ValueChanged);
            // 
            // btnGetFromLayer
            // 
            this.btnGetFromLayer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetFromLayer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGetFromLayer.Location = new System.Drawing.Point(81, 62);
            this.btnGetFromLayer.Name = "btnGetFromLayer";
            this.btnGetFromLayer.Size = new System.Drawing.Size(113, 36);
            this.btnGetFromLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGetFromLayer.TabIndex = 2;
            this.btnGetFromLayer.Text = "自动获取NODATA";
            this.btnGetFromLayer.Click += new System.EventHandler(this.btnGetFromLayer_Click);
            // 
            // FrmSetRasterNoDataValue
            // 
            this.ClientSize = new System.Drawing.Size(209, 112);
            this.ControlBox = false;
            this.Controls.Add(this.btnGetFromLayer);
            this.Controls.Add(this.dbiNoDataValue);
            this.Controls.Add(this.lblSetNoDataValue);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetRasterNoDataValue";
            this.Text = "修改栅格值";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSetRasterNoDataValue_FormClosed);
            this.Load += new System.EventHandler(this.FrmSetRasterNoDataValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbiNoDataValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblSetNoDataValue;
        private DevComponents.Editors.DoubleInput dbiNoDataValue;
        private DevComponents.DotNetBar.ButtonX btnGetFromLayer;
    }
}