namespace LibCerMap
{
    partial class FrmSelectColorRamp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectColorRamp));
            this.cmbColorRamp = new LibCerMap.FrmSymbology.ComboboxSymbol();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbColorRamp
            // 
            this.cmbColorRamp.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbColorRamp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbColorRamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorRamp.FormattingEnabled = true;
            this.cmbColorRamp.Location = new System.Drawing.Point(8, 22);
            this.cmbColorRamp.Name = "cmbColorRamp";
            this.cmbColorRamp.Size = new System.Drawing.Size(240, 22);
            this.cmbColorRamp.TabIndex = 16;
            this.cmbColorRamp.SelectedIndexChanged += new System.EventHandler(this.cmbColorRamp_SelectedIndexChanged);
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Location = new System.Drawing.Point(8, 50);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(240, 26);
            this.axSymbologyControl1.TabIndex = 17;
            this.axSymbologyControl1.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(158, 93);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 31);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "确定";
            // 
            // FrmSelectColorRamp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 134);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.axSymbologyControl1);
            this.Controls.Add(this.cmbColorRamp);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectColorRamp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择渲染颜色";
            this.Load += new System.EventHandler(this.FrmSelectColorRamp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FrmSymbology.ComboboxSymbol cmbColorRamp;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private DevComponents.DotNetBar.ButtonX btnOK;
    }
}