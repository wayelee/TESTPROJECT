namespace LibCerMap
{
    partial class FrmMapUnits
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
            this.cmbMapUnits = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.comboItem13 = new DevComponents.Editors.ComboItem();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.cmbDisplayUnits = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem14 = new DevComponents.Editors.ComboItem();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.comboItem19 = new DevComponents.Editors.ComboItem();
            this.comboItem20 = new DevComponents.Editors.ComboItem();
            this.comboItem21 = new DevComponents.Editors.ComboItem();
            this.comboItem22 = new DevComponents.Editors.ComboItem();
            this.comboItem23 = new DevComponents.Editors.ComboItem();
            this.comboItem24 = new DevComponents.Editors.ComboItem();
            this.comboItem25 = new DevComponents.Editors.ComboItem();
            this.comboItem27 = new DevComponents.Editors.ComboItem();
            this.comboItem26 = new DevComponents.Editors.ComboItem();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(28, 23);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "单位 :";
            // 
            // cmbMapUnits
            // 
            this.cmbMapUnits.DisplayMember = "Text";
            this.cmbMapUnits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMapUnits.FormattingEnabled = true;
            this.cmbMapUnits.ItemHeight = 15;
            this.cmbMapUnits.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10,
            this.comboItem11,
            this.comboItem12,
            this.comboItem13});
            this.cmbMapUnits.Location = new System.Drawing.Point(92, 23);
            this.cmbMapUnits.Name = "cmbMapUnits";
            this.cmbMapUnits.Size = new System.Drawing.Size(121, 21);
            this.cmbMapUnits.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbMapUnits.TabIndex = 1;
            this.cmbMapUnits.SelectedIndexChanged += new System.EventHandler(this.cmbunits_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "UnKnownUnits";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "Inches";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "Points";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "Feet";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "Yards";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "Miles";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "NauticalMiles";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "Millimeters";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "Centimeters";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "Meters";
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "Kilometers";
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "DecimalDegrees";
            // 
            // comboItem13
            // 
            this.comboItem13.Text = "Decimeters";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(52, 106);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 24);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(145, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 24);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbDisplayUnits
            // 
            this.cmbDisplayUnits.DisplayMember = "Text";
            this.cmbDisplayUnits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDisplayUnits.FormattingEnabled = true;
            this.cmbDisplayUnits.ItemHeight = 15;
            this.cmbDisplayUnits.Items.AddRange(new object[] {
            this.comboItem14,
            this.comboItem15,
            this.comboItem16,
            this.comboItem17,
            this.comboItem18,
            this.comboItem19,
            this.comboItem20,
            this.comboItem21,
            this.comboItem22,
            this.comboItem23,
            this.comboItem24,
            this.comboItem25,
            this.comboItem27,
            this.comboItem26});
            this.cmbDisplayUnits.Location = new System.Drawing.Point(92, 56);
            this.cmbDisplayUnits.Name = "cmbDisplayUnits";
            this.cmbDisplayUnits.Size = new System.Drawing.Size(121, 21);
            this.cmbDisplayUnits.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDisplayUnits.TabIndex = 7;
            // 
            // comboItem14
            // 
            this.comboItem14.Text = "UnKnownUnits";
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "Inches";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "Points";
            // 
            // comboItem17
            // 
            this.comboItem17.Text = "Feet";
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "Yards";
            // 
            // comboItem19
            // 
            this.comboItem19.Text = "Miles";
            // 
            // comboItem20
            // 
            this.comboItem20.Text = "NauticalMiles";
            // 
            // comboItem21
            // 
            this.comboItem21.Text = "Millimeters";
            // 
            // comboItem22
            // 
            this.comboItem22.Text = "Centimeters";
            // 
            // comboItem23
            // 
            this.comboItem23.Text = "Meters";
            // 
            // comboItem24
            // 
            this.comboItem24.Text = "Kilometers";
            // 
            // comboItem25
            // 
            this.comboItem25.Text = "DecimalDegrees";
            // 
            // comboItem27
            // 
            this.comboItem27.Text = "DegreeaMinutesSecond";
            // 
            // comboItem26
            // 
            this.comboItem26.Text = "Decimeters";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(28, 56);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(47, 23);
            this.labelX2.TabIndex = 6;
            this.labelX2.Text = "显示 :";
            // 
            // FrmMapUnits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 148);
            this.Controls.Add(this.cmbDisplayUnits);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbMapUnits);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMapUnits";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地图单位";
            this.Load += new System.EventHandler(this.FrmMapUnits_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbMapUnits;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.ComboItem comboItem13;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDisplayUnits;
        private DevComponents.Editors.ComboItem comboItem14;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.Editors.ComboItem comboItem18;
        private DevComponents.Editors.ComboItem comboItem19;
        private DevComponents.Editors.ComboItem comboItem20;
        private DevComponents.Editors.ComboItem comboItem21;
        private DevComponents.Editors.ComboItem comboItem22;
        private DevComponents.Editors.ComboItem comboItem23;
        private DevComponents.Editors.ComboItem comboItem24;
        private DevComponents.Editors.ComboItem comboItem25;
        private DevComponents.Editors.ComboItem comboItem27;
        private DevComponents.Editors.ComboItem comboItem26;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}