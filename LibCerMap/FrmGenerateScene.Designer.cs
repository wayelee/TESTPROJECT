namespace LibCerMap
{
    partial class FrmGenerateScene
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
            this.comboBoxExBaseHeightLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblBaseHeightLayer = new DevComponents.DotNetBar.LabelX();
            this.buttonOK = new DevComponents.DotNetBar.ButtonX();
            this.labelXExaFactor = new DevComponents.DotNetBar.LabelX();
            this.doubleInputExaFactor = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputExaFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxExBaseHeightLayer
            // 
            this.comboBoxExBaseHeightLayer.DisplayMember = "Text";
            this.comboBoxExBaseHeightLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExBaseHeightLayer.FormattingEnabled = true;
            this.comboBoxExBaseHeightLayer.ItemHeight = 15;
            this.comboBoxExBaseHeightLayer.Location = new System.Drawing.Point(120, 22);
            this.comboBoxExBaseHeightLayer.Name = "comboBoxExBaseHeightLayer";
            this.comboBoxExBaseHeightLayer.Size = new System.Drawing.Size(209, 21);
            this.comboBoxExBaseHeightLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExBaseHeightLayer.TabIndex = 0;
            // 
            // lblBaseHeightLayer
            // 
            // 
            // 
            // 
            this.lblBaseHeightLayer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBaseHeightLayer.Location = new System.Drawing.Point(12, 22);
            this.lblBaseHeightLayer.Name = "lblBaseHeightLayer";
            this.lblBaseHeightLayer.Size = new System.Drawing.Size(102, 23);
            this.lblBaseHeightLayer.TabIndex = 1;
            this.lblBaseHeightLayer.Text = "高度基准图层：";
            // 
            // buttonOK
            // 
            this.buttonOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(173, 117);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "确定";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelXExaFactor
            // 
            // 
            // 
            // 
            this.labelXExaFactor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXExaFactor.Location = new System.Drawing.Point(12, 63);
            this.labelXExaFactor.Name = "labelXExaFactor";
            this.labelXExaFactor.Size = new System.Drawing.Size(122, 23);
            this.labelXExaFactor.TabIndex = 1;
            this.labelXExaFactor.Text = "场景高度基准：";
            // 
            // doubleInputExaFactor
            // 
            this.doubleInputExaFactor.DecimalPlaces = 4;
            this.doubleInputExaFactor.Location = new System.Drawing.Point(120, 63);
            this.doubleInputExaFactor.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.doubleInputExaFactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.doubleInputExaFactor.Name = "doubleInputExaFactor";
            this.doubleInputExaFactor.Size = new System.Drawing.Size(80, 21);
            this.doubleInputExaFactor.TabIndex = 4;
            this.doubleInputExaFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(254, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmGenerateScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 152);
            this.Controls.Add(this.doubleInputExaFactor);
            this.Controls.Add(this.comboBoxExBaseHeightLayer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelXExaFactor);
            this.Controls.Add(this.lblBaseHeightLayer);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGenerateScene";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成3D场景";
            this.Load += new System.EventHandler(this.FrmGenerateScene_Load);
            ((System.ComponentModel.ISupportInitialize)(this.doubleInputExaFactor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExBaseHeightLayer;
        private DevComponents.DotNetBar.LabelX lblBaseHeightLayer;
        private DevComponents.DotNetBar.ButtonX buttonOK;
        private DevComponents.DotNetBar.LabelX labelXExaFactor;
        private System.Windows.Forms.NumericUpDown doubleInputExaFactor;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}