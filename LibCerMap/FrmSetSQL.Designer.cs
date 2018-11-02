namespace LibCerMap
{
    partial class FrmSetSQL
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
            this.btncancle = new DevComponents.DotNetBar.ButtonX();
            this.btnok = new DevComponents.DotNetBar.ButtonX();
            this.RtbSQL = new System.Windows.Forms.RichTextBox();
            this.lsbUniqeValue = new System.Windows.Forms.ListBox();
            this.BtnUniqeValue = new DevComponents.DotNetBar.ButtonX();
            this.BtnIs = new DevComponents.DotNetBar.ButtonX();
            this.BtnNot = new DevComponents.DotNetBar.ButtonX();
            this.BtnOr = new DevComponents.DotNetBar.ButtonX();
            this.BtnAnd = new DevComponents.DotNetBar.ButtonX();
            this.BtnLike = new DevComponents.DotNetBar.ButtonX();
            this.BtnRightBracket = new DevComponents.DotNetBar.ButtonX();
            this.BtnLeftBracket = new DevComponents.DotNetBar.ButtonX();
            this.BtnSmallEqual = new DevComponents.DotNetBar.ButtonX();
            this.BtnBigEqual = new DevComponents.DotNetBar.ButtonX();
            this.BtnNotEqual = new DevComponents.DotNetBar.ButtonX();
            this.BtnSmall = new DevComponents.DotNetBar.ButtonX();
            this.BtnBig = new DevComponents.DotNetBar.ButtonX();
            this.BtnEqual = new DevComponents.DotNetBar.ButtonX();
            this.lsbFields = new System.Windows.Forms.ListBox();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // btncancle
            // 
            this.btncancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncancle.Location = new System.Drawing.Point(271, 373);
            this.btncancle.Name = "btncancle";
            this.btncancle.Size = new System.Drawing.Size(70, 25);
            this.btncancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncancle.TabIndex = 42;
            this.btncancle.Text = "取消";
            this.btncancle.Click += new System.EventHandler(this.btncancle_Click);
            // 
            // btnok
            // 
            this.btnok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnok.Location = new System.Drawing.Point(170, 373);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(70, 25);
            this.btnok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnok.TabIndex = 40;
            this.btnok.Text = "确定";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // RtbSQL
            // 
            this.RtbSQL.Location = new System.Drawing.Point(10, 252);
            this.RtbSQL.Name = "RtbSQL";
            this.RtbSQL.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RtbSQL.Size = new System.Drawing.Size(331, 108);
            this.RtbSQL.TabIndex = 39;
            this.RtbSQL.Text = "";
            // 
            // lsbUniqeValue
            // 
            this.lsbUniqeValue.ItemHeight = 12;
            this.lsbUniqeValue.Location = new System.Drawing.Point(170, 123);
            this.lsbUniqeValue.Name = "lsbUniqeValue";
            this.lsbUniqeValue.Size = new System.Drawing.Size(171, 112);
            this.lsbUniqeValue.TabIndex = 38;
            this.lsbUniqeValue.DoubleClick += new System.EventHandler(this.lsbUniqeValue_DoubleClick);
            // 
            // BtnUniqeValue
            // 
            this.BtnUniqeValue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnUniqeValue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnUniqeValue.Location = new System.Drawing.Point(85, 210);
            this.BtnUniqeValue.Name = "BtnUniqeValue";
            this.BtnUniqeValue.Size = new System.Drawing.Size(78, 23);
            this.BtnUniqeValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnUniqeValue.TabIndex = 37;
            this.BtnUniqeValue.Text = "得到独立值";
            this.BtnUniqeValue.Click += new System.EventHandler(this.BtnUniqeValue_Click);
            // 
            // BtnIs
            // 
            this.BtnIs.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnIs.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnIs.Location = new System.Drawing.Point(10, 210);
            this.BtnIs.Name = "BtnIs";
            this.BtnIs.Size = new System.Drawing.Size(34, 23);
            this.BtnIs.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnIs.TabIndex = 36;
            this.BtnIs.Text = "Is";
            this.BtnIs.Click += new System.EventHandler(this.BtnIs_Click);
            // 
            // BtnNot
            // 
            this.BtnNot.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnNot.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnNot.Location = new System.Drawing.Point(130, 181);
            this.BtnNot.Name = "BtnNot";
            this.BtnNot.Size = new System.Drawing.Size(34, 23);
            this.BtnNot.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnNot.TabIndex = 35;
            this.BtnNot.Text = "Not";
            this.BtnNot.Click += new System.EventHandler(this.BtnNot_Click);
            // 
            // BtnOr
            // 
            this.BtnOr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnOr.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnOr.Location = new System.Drawing.Point(90, 181);
            this.BtnOr.Name = "BtnOr";
            this.BtnOr.Size = new System.Drawing.Size(34, 23);
            this.BtnOr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnOr.TabIndex = 34;
            this.BtnOr.Text = "Or";
            this.BtnOr.Click += new System.EventHandler(this.BtnOr_Click);
            // 
            // BtnAnd
            // 
            this.BtnAnd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnAnd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnAnd.Location = new System.Drawing.Point(50, 181);
            this.BtnAnd.Name = "BtnAnd";
            this.BtnAnd.Size = new System.Drawing.Size(34, 23);
            this.BtnAnd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnAnd.TabIndex = 33;
            this.BtnAnd.Text = "And";
            this.BtnAnd.Click += new System.EventHandler(this.BtnAnd_Click);
            // 
            // BtnLike
            // 
            this.BtnLike.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnLike.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnLike.Location = new System.Drawing.Point(10, 181);
            this.BtnLike.Name = "BtnLike";
            this.BtnLike.Size = new System.Drawing.Size(34, 23);
            this.BtnLike.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnLike.TabIndex = 32;
            this.BtnLike.Text = "Like";
            this.BtnLike.Click += new System.EventHandler(this.BtnLike_Click);
            // 
            // BtnRightBracket
            // 
            this.BtnRightBracket.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnRightBracket.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnRightBracket.Location = new System.Drawing.Point(130, 152);
            this.BtnRightBracket.Name = "BtnRightBracket";
            this.BtnRightBracket.Size = new System.Drawing.Size(34, 23);
            this.BtnRightBracket.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnRightBracket.TabIndex = 31;
            this.BtnRightBracket.Text = ")";
            this.BtnRightBracket.Click += new System.EventHandler(this.BtnRightBracket_Click);
            // 
            // BtnLeftBracket
            // 
            this.BtnLeftBracket.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnLeftBracket.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnLeftBracket.Location = new System.Drawing.Point(90, 152);
            this.BtnLeftBracket.Name = "BtnLeftBracket";
            this.BtnLeftBracket.Size = new System.Drawing.Size(34, 23);
            this.BtnLeftBracket.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnLeftBracket.TabIndex = 30;
            this.BtnLeftBracket.Text = "(";
            this.BtnLeftBracket.Click += new System.EventHandler(this.BtnLeftBracket_Click);
            // 
            // BtnSmallEqual
            // 
            this.BtnSmallEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSmallEqual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSmallEqual.Location = new System.Drawing.Point(50, 152);
            this.BtnSmallEqual.Name = "BtnSmallEqual";
            this.BtnSmallEqual.Size = new System.Drawing.Size(34, 23);
            this.BtnSmallEqual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSmallEqual.TabIndex = 29;
            this.BtnSmallEqual.Text = "<=";
            this.BtnSmallEqual.Click += new System.EventHandler(this.BtnSmallEqual_Click);
            // 
            // BtnBigEqual
            // 
            this.BtnBigEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnBigEqual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnBigEqual.Location = new System.Drawing.Point(10, 152);
            this.BtnBigEqual.Name = "BtnBigEqual";
            this.BtnBigEqual.Size = new System.Drawing.Size(34, 23);
            this.BtnBigEqual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnBigEqual.TabIndex = 28;
            this.BtnBigEqual.Text = ">=";
            this.BtnBigEqual.Click += new System.EventHandler(this.BtnBigEqual_Click);
            // 
            // BtnNotEqual
            // 
            this.BtnNotEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnNotEqual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnNotEqual.Location = new System.Drawing.Point(130, 123);
            this.BtnNotEqual.Name = "BtnNotEqual";
            this.BtnNotEqual.Size = new System.Drawing.Size(34, 23);
            this.BtnNotEqual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnNotEqual.TabIndex = 27;
            this.BtnNotEqual.Text = "<>";
            this.BtnNotEqual.Click += new System.EventHandler(this.BtnNotEqual_Click);
            // 
            // BtnSmall
            // 
            this.BtnSmall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSmall.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSmall.Location = new System.Drawing.Point(90, 123);
            this.BtnSmall.Name = "BtnSmall";
            this.BtnSmall.Size = new System.Drawing.Size(34, 23);
            this.BtnSmall.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSmall.TabIndex = 26;
            this.BtnSmall.Text = "<";
            this.BtnSmall.Click += new System.EventHandler(this.BtnSmall_Click);
            // 
            // BtnBig
            // 
            this.BtnBig.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnBig.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnBig.Location = new System.Drawing.Point(50, 123);
            this.BtnBig.Name = "BtnBig";
            this.BtnBig.Size = new System.Drawing.Size(34, 23);
            this.BtnBig.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnBig.TabIndex = 25;
            this.BtnBig.Text = ">";
            this.BtnBig.Click += new System.EventHandler(this.BtnBig_Click);
            // 
            // BtnEqual
            // 
            this.BtnEqual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnEqual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnEqual.Location = new System.Drawing.Point(10, 123);
            this.BtnEqual.Name = "BtnEqual";
            this.BtnEqual.Size = new System.Drawing.Size(34, 23);
            this.BtnEqual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnEqual.TabIndex = 24;
            this.BtnEqual.Text = "=";
            this.BtnEqual.Click += new System.EventHandler(this.BtnEqual_Click);
            // 
            // lsbFields
            // 
            this.lsbFields.ItemHeight = 12;
            this.lsbFields.Location = new System.Drawing.Point(10, 6);
            this.lsbFields.Name = "lsbFields";
            this.lsbFields.Size = new System.Drawing.Size(331, 100);
            this.lsbFields.TabIndex = 23;
            this.lsbFields.DoubleClick += new System.EventHandler(this.lsbFields_DoubleClick);
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClear.Location = new System.Drawing.Point(14, 373);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 25);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 43;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FrmSetSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 410);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btncancle);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetSQL";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置查询条件";
            this.Load += new System.EventHandler(this.FrmSetSQL_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btncancle;
        private DevComponents.DotNetBar.ButtonX btnok;
        private System.Windows.Forms.RichTextBox RtbSQL;
        private System.Windows.Forms.ListBox lsbUniqeValue;
        private DevComponents.DotNetBar.ButtonX BtnUniqeValue;
        private DevComponents.DotNetBar.ButtonX BtnIs;
        private DevComponents.DotNetBar.ButtonX BtnNot;
        private DevComponents.DotNetBar.ButtonX BtnOr;
        private DevComponents.DotNetBar.ButtonX BtnAnd;
        private DevComponents.DotNetBar.ButtonX BtnLike;
        private DevComponents.DotNetBar.ButtonX BtnRightBracket;
        private DevComponents.DotNetBar.ButtonX BtnLeftBracket;
        private DevComponents.DotNetBar.ButtonX BtnSmallEqual;
        private DevComponents.DotNetBar.ButtonX BtnBigEqual;
        private DevComponents.DotNetBar.ButtonX BtnNotEqual;
        private DevComponents.DotNetBar.ButtonX BtnSmall;
        private DevComponents.DotNetBar.ButtonX BtnBig;
        private DevComponents.DotNetBar.ButtonX BtnEqual;
        private System.Windows.Forms.ListBox lsbFields;
        private DevComponents.DotNetBar.ButtonX btnClear;
    }
}