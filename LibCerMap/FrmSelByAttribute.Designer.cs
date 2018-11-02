namespace LibCerMap
{
    partial class FrmSelByAttribute
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
            this.lsbFields = new System.Windows.Forms.ListBox();
            this.BtnEqual = new DevComponents.DotNetBar.ButtonX();
            this.BtnBig = new DevComponents.DotNetBar.ButtonX();
            this.BtnSmall = new DevComponents.DotNetBar.ButtonX();
            this.BtnNotEqual = new DevComponents.DotNetBar.ButtonX();
            this.BtnBigEqual = new DevComponents.DotNetBar.ButtonX();
            this.BtnSmallEqual = new DevComponents.DotNetBar.ButtonX();
            this.BtnLeftBracket = new DevComponents.DotNetBar.ButtonX();
            this.BtnRightBracket = new DevComponents.DotNetBar.ButtonX();
            this.BtnLike = new DevComponents.DotNetBar.ButtonX();
            this.BtnAnd = new DevComponents.DotNetBar.ButtonX();
            this.BtnOr = new DevComponents.DotNetBar.ButtonX();
            this.BtnNot = new DevComponents.DotNetBar.ButtonX();
            this.BtnIs = new DevComponents.DotNetBar.ButtonX();
            this.BtnUniqeValue = new DevComponents.DotNetBar.ButtonX();
            this.lsbUniqeValue = new System.Windows.Forms.ListBox();
            this.RtbSQL = new System.Windows.Forms.RichTextBox();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.btnuse = new DevComponents.DotNetBar.ButtonX();
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.lblSelect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lsbFields
            // 
            this.lsbFields.ItemHeight = 12;
            this.lsbFields.Location = new System.Drawing.Point(12, 12);
            this.lsbFields.Name = "lsbFields";
            this.lsbFields.Size = new System.Drawing.Size(331, 100);
            this.lsbFields.TabIndex = 2;
            this.lsbFields.DoubleClick += new System.EventHandler(this.lsbFields_DoubleClick);
            // 
            // BtnEqual
            // 
            this.BtnEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnEqual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnEqual.Location = new System.Drawing.Point(12, 129);
            this.BtnEqual.Name = "BtnEqual";
            this.BtnEqual.Size = new System.Drawing.Size(34, 23);
            this.BtnEqual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnEqual.TabIndex = 3;
            this.BtnEqual.Text = "=";
            this.BtnEqual.Click += new System.EventHandler(this.BtnEqual_Click);
            // 
            // BtnBig
            // 
            this.BtnBig.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnBig.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnBig.Location = new System.Drawing.Point(52, 129);
            this.BtnBig.Name = "BtnBig";
            this.BtnBig.Size = new System.Drawing.Size(34, 23);
            this.BtnBig.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnBig.TabIndex = 4;
            this.BtnBig.Text = ">";
            this.BtnBig.Click += new System.EventHandler(this.BtnBig_Click);
            // 
            // BtnSmall
            // 
            this.BtnSmall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSmall.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSmall.Location = new System.Drawing.Point(92, 129);
            this.BtnSmall.Name = "BtnSmall";
            this.BtnSmall.Size = new System.Drawing.Size(34, 23);
            this.BtnSmall.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSmall.TabIndex = 5;
            this.BtnSmall.Text = "<";
            this.BtnSmall.Click += new System.EventHandler(this.BtnSmall_Click);
            // 
            // BtnNotEqual
            // 
            this.BtnNotEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnNotEqual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnNotEqual.Location = new System.Drawing.Point(132, 129);
            this.BtnNotEqual.Name = "BtnNotEqual";
            this.BtnNotEqual.Size = new System.Drawing.Size(34, 23);
            this.BtnNotEqual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnNotEqual.TabIndex = 6;
            this.BtnNotEqual.Text = "<>";
            this.BtnNotEqual.Click += new System.EventHandler(this.BtnNotEqual_Click);
            // 
            // BtnBigEqual
            // 
            this.BtnBigEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnBigEqual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnBigEqual.Location = new System.Drawing.Point(12, 158);
            this.BtnBigEqual.Name = "BtnBigEqual";
            this.BtnBigEqual.Size = new System.Drawing.Size(34, 23);
            this.BtnBigEqual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnBigEqual.TabIndex = 7;
            this.BtnBigEqual.Text = ">=";
            this.BtnBigEqual.Click += new System.EventHandler(this.BtnBigEqual_Click);
            // 
            // BtnSmallEqual
            // 
            this.BtnSmallEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSmallEqual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSmallEqual.Location = new System.Drawing.Point(52, 158);
            this.BtnSmallEqual.Name = "BtnSmallEqual";
            this.BtnSmallEqual.Size = new System.Drawing.Size(34, 23);
            this.BtnSmallEqual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSmallEqual.TabIndex = 8;
            this.BtnSmallEqual.Text = "<=";
            this.BtnSmallEqual.Click += new System.EventHandler(this.BtnSmallEqual_Click);
            // 
            // BtnLeftBracket
            // 
            this.BtnLeftBracket.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnLeftBracket.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnLeftBracket.Location = new System.Drawing.Point(92, 158);
            this.BtnLeftBracket.Name = "BtnLeftBracket";
            this.BtnLeftBracket.Size = new System.Drawing.Size(34, 23);
            this.BtnLeftBracket.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnLeftBracket.TabIndex = 9;
            this.BtnLeftBracket.Text = "(";
            this.BtnLeftBracket.Click += new System.EventHandler(this.BtnLeftBracket_Click);
            // 
            // BtnRightBracket
            // 
            this.BtnRightBracket.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnRightBracket.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnRightBracket.Location = new System.Drawing.Point(132, 158);
            this.BtnRightBracket.Name = "BtnRightBracket";
            this.BtnRightBracket.Size = new System.Drawing.Size(34, 23);
            this.BtnRightBracket.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnRightBracket.TabIndex = 10;
            this.BtnRightBracket.Text = ")";
            this.BtnRightBracket.Click += new System.EventHandler(this.BtnRightBracket_Click);
            // 
            // BtnLike
            // 
            this.BtnLike.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnLike.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnLike.Location = new System.Drawing.Point(12, 187);
            this.BtnLike.Name = "BtnLike";
            this.BtnLike.Size = new System.Drawing.Size(34, 23);
            this.BtnLike.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnLike.TabIndex = 11;
            this.BtnLike.Text = "Like";
            this.BtnLike.Click += new System.EventHandler(this.BtnLike_Click);
            // 
            // BtnAnd
            // 
            this.BtnAnd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnAnd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnAnd.Location = new System.Drawing.Point(52, 187);
            this.BtnAnd.Name = "BtnAnd";
            this.BtnAnd.Size = new System.Drawing.Size(34, 23);
            this.BtnAnd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnAnd.TabIndex = 12;
            this.BtnAnd.Text = "And";
            this.BtnAnd.Click += new System.EventHandler(this.BtnAnd_Click);
            // 
            // BtnOr
            // 
            this.BtnOr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnOr.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnOr.Location = new System.Drawing.Point(92, 187);
            this.BtnOr.Name = "BtnOr";
            this.BtnOr.Size = new System.Drawing.Size(34, 23);
            this.BtnOr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnOr.TabIndex = 13;
            this.BtnOr.Text = "Or";
            this.BtnOr.Click += new System.EventHandler(this.BtnOr_Click);
            // 
            // BtnNot
            // 
            this.BtnNot.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnNot.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnNot.Location = new System.Drawing.Point(132, 187);
            this.BtnNot.Name = "BtnNot";
            this.BtnNot.Size = new System.Drawing.Size(34, 23);
            this.BtnNot.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnNot.TabIndex = 14;
            this.BtnNot.Text = "Not";
            this.BtnNot.Click += new System.EventHandler(this.BtnNot_Click);
            // 
            // BtnIs
            // 
            this.BtnIs.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnIs.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnIs.Location = new System.Drawing.Point(12, 216);
            this.BtnIs.Name = "BtnIs";
            this.BtnIs.Size = new System.Drawing.Size(34, 23);
            this.BtnIs.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnIs.TabIndex = 15;
            this.BtnIs.Text = "Is";
            this.BtnIs.Click += new System.EventHandler(this.BtnIs_Click);
            // 
            // BtnUniqeValue
            // 
            this.BtnUniqeValue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnUniqeValue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnUniqeValue.Location = new System.Drawing.Point(87, 216);
            this.BtnUniqeValue.Name = "BtnUniqeValue";
            this.BtnUniqeValue.Size = new System.Drawing.Size(78, 23);
            this.BtnUniqeValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnUniqeValue.TabIndex = 16;
            this.BtnUniqeValue.Text = "得到独立值";
            this.BtnUniqeValue.Click += new System.EventHandler(this.BtnUniqeValue_Click);
            // 
            // lsbUniqeValue
            // 
            this.lsbUniqeValue.ItemHeight = 12;
            this.lsbUniqeValue.Location = new System.Drawing.Point(172, 129);
            this.lsbUniqeValue.Name = "lsbUniqeValue";
            this.lsbUniqeValue.Size = new System.Drawing.Size(171, 112);
            this.lsbUniqeValue.TabIndex = 17;
            this.lsbUniqeValue.DoubleClick += new System.EventHandler(this.lsbUniqeValue_DoubleClick);
            // 
            // RtbSQL
            // 
            this.RtbSQL.Location = new System.Drawing.Point(12, 289);
            this.RtbSQL.Name = "RtbSQL";
            this.RtbSQL.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RtbSQL.Size = new System.Drawing.Size(331, 77);
            this.RtbSQL.TabIndex = 18;
            this.RtbSQL.Text = "";
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(96, 372);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(70, 25);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 19;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btnuse
            // 
            this.btnuse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnuse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnuse.Location = new System.Drawing.Point(185, 372);
            this.btnuse.Name = "btnuse";
            this.btnuse.Size = new System.Drawing.Size(70, 25);
            this.btnuse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnuse.TabIndex = 20;
            this.btnuse.Text = "应用";
            this.btnuse.Click += new System.EventHandler(this.btnuse_Click);
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(273, 372);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(70, 25);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 21;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // lblSelect
            // 
            this.lblSelect.ForeColor = System.Drawing.Color.Black;
            this.lblSelect.Location = new System.Drawing.Point(12, 252);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(331, 23);
            this.lblSelect.TabIndex = 22;
            // 
            // FrmSelByAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 416);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.btncancle);
            this.Controls.Add(this.btnuse);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.RtbSQL);
            this.Controls.Add(this.lsbUniqeValue);
            this.Controls.Add(this.BtnUniqeValue);
            this.Controls.Add(this.BtnIs);
            this.Controls.Add(this.BtnNot);
            this.Controls.Add(this.BtnOr);
            this.Controls.Add(this.BtnAnd);
            this.Controls.Add(this.BtnLike);
            this.Controls.Add(this.BtnRightBracket);
            this.Controls.Add(this.BtnLeftBracket);
            this.Controls.Add(this.BtnSmallEqual);
            this.Controls.Add(this.BtnBigEqual);
            this.Controls.Add(this.BtnNotEqual);
            this.Controls.Add(this.BtnSmall);
            this.Controls.Add(this.BtnBig);
            this.Controls.Add(this.BtnEqual);
            this.Controls.Add(this.lsbFields);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelByAttribute";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性查询";
            this.Load += new System.EventHandler(this.FrmSelByAttribute_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsbFields;
        private DevComponents.DotNetBar.ButtonX BtnEqual;
        private DevComponents.DotNetBar.ButtonX BtnBig;
        private DevComponents.DotNetBar.ButtonX BtnSmall;
        private DevComponents.DotNetBar.ButtonX BtnNotEqual;
        private DevComponents.DotNetBar.ButtonX BtnBigEqual;
        private DevComponents.DotNetBar.ButtonX BtnSmallEqual;
        private DevComponents.DotNetBar.ButtonX BtnLeftBracket;
        private DevComponents.DotNetBar.ButtonX BtnRightBracket;
        private DevComponents.DotNetBar.ButtonX BtnLike;
        private DevComponents.DotNetBar.ButtonX BtnAnd;
        private DevComponents.DotNetBar.ButtonX BtnOr;
        private DevComponents.DotNetBar.ButtonX BtnNot;
        private DevComponents.DotNetBar.ButtonX BtnIs;
        private DevComponents.DotNetBar.ButtonX BtnUniqeValue;
        private System.Windows.Forms.ListBox lsbUniqeValue;
        private System.Windows.Forms.RichTextBox RtbSQL;
        private DevComponents.DotNetBar.ButtonX btnok;
        private DevComponents.DotNetBar.ButtonX btnuse;
        private DevComponents.DotNetBar.ButtonX btncancle;
        private System.Windows.Forms.Label lblSelect;
    }
}