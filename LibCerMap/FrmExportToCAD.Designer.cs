namespace LibCerMap
{
    partial class FrmExportToCAD
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
            this.buttonXExport = new DevComponents.DotNetBar.ButtonX();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonXadd = new DevComponents.DotNetBar.ButtonX();
            this.buttonXRemove = new DevComponents.DotNetBar.ButtonX();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonXSelect = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // buttonXExport
            // 
            this.buttonXExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXExport.Location = new System.Drawing.Point(534, 370);
            this.buttonXExport.Name = "buttonXExport";
            this.buttonXExport.Size = new System.Drawing.Size(75, 23);
            this.buttonXExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXExport.TabIndex = 0;
            this.buttonXExport.Text = " 导出";
            this.buttonXExport.Click += new System.EventHandler(this.buttonXExport_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(273, 251);
            this.listBox1.TabIndex = 1;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(344, 41);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(303, 251);
            this.listBox2.TabIndex = 1;
            this.listBox2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "输出图层";
            // 
            // buttonXadd
            // 
            this.buttonXadd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXadd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXadd.Location = new System.Drawing.Point(291, 83);
            this.buttonXadd.Name = "buttonXadd";
            this.buttonXadd.Size = new System.Drawing.Size(47, 23);
            this.buttonXadd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXadd.TabIndex = 0;
            this.buttonXadd.Text = ">>";
            this.buttonXadd.Click += new System.EventHandler(this.buttonXadd_Click);
            // 
            // buttonXRemove
            // 
            this.buttonXRemove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXRemove.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXRemove.Location = new System.Drawing.Point(291, 152);
            this.buttonXRemove.Name = "buttonXRemove";
            this.buttonXRemove.Size = new System.Drawing.Size(47, 23);
            this.buttonXRemove.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXRemove.TabIndex = 0;
            this.buttonXRemove.Text = "<<";
            this.buttonXRemove.Click += new System.EventHandler(this.buttonXRemove_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "DGN_V8",
            "DWG_R14",
            "DWG_R2000",
            "DWG_R2004",
            "DWG_R2005",
            "DWG_R2007",
            "DWG_R2010",
            "DXF_R14",
            "DXF_R2000",
            "DXF_R2004",
            "DXF_R2005",
            "DXF_R2007",
            "DXF_R2010"});
            this.comboBox1.Location = new System.Drawing.Point(15, 314);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "地图图层";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 341);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(594, 20);
            this.textBox1.TabIndex = 4;
            // 
            // buttonXSelect
            // 
            this.buttonXSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSelect.Location = new System.Drawing.Point(615, 341);
            this.buttonXSelect.Name = "buttonXSelect";
            this.buttonXSelect.Size = new System.Drawing.Size(29, 23);
            this.buttonXSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXSelect.TabIndex = 5;
            this.buttonXSelect.Text = "...";
            this.buttonXSelect.Click += new System.EventHandler(this.buttonXSelect_Click);
            // 
            // FrmExportToCAD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 398);
            this.Controls.Add(this.buttonXSelect);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonXadd);
            this.Controls.Add(this.buttonXRemove);
            this.Controls.Add(this.buttonXExport);
            this.DoubleBuffered = true;
            this.Name = "FrmExportToCAD";
            this.Text = "导出 CAD";
            this.Load += new System.EventHandler(this.FrmExportToCAD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXExport;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX buttonXadd;
        private DevComponents.DotNetBar.ButtonX buttonXRemove;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private DevComponents.DotNetBar.ButtonX buttonXSelect;
    }
}