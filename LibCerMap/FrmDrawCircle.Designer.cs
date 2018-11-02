namespace LibCerMap
{
    partial class FrmDrawCircle
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
            this.txtCircleY = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtCircleX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtRadio = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cheMouseClick = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnDraw = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // txtCircleY
            // 
            // 
            // 
            // 
            this.txtCircleY.Border.Class = "TextBoxBorder";
            this.txtCircleY.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCircleY.Location = new System.Drawing.Point(252, -5);
            this.txtCircleY.Name = "txtCircleY";
            this.txtCircleY.Size = new System.Drawing.Size(116, 21);
            this.txtCircleY.TabIndex = 0;
            this.txtCircleY.Visible = false;
            this.txtCircleY.TextChanged += new System.EventHandler(this.txtCircleY_TextChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(198, -7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(48, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "圆心Y：";
            this.labelX1.Visible = false;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(16, -9);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(48, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "圆心X：";
            this.labelX2.Visible = false;
            // 
            // txtCircleX
            // 
            // 
            // 
            // 
            this.txtCircleX.Border.Class = "TextBoxBorder";
            this.txtCircleX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCircleX.Location = new System.Drawing.Point(70, -7);
            this.txtCircleX.Name = "txtCircleX";
            this.txtCircleX.Size = new System.Drawing.Size(113, 21);
            this.txtCircleX.TabIndex = 2;
            this.txtCircleX.Visible = false;
            this.txtCircleX.TextChanged += new System.EventHandler(this.txtCircleX_TextChanged);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(16, 29);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(48, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "半径：";
            // 
            // txtRadio
            // 
            // 
            // 
            // 
            this.txtRadio.Border.Class = "TextBoxBorder";
            this.txtRadio.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRadio.Location = new System.Drawing.Point(70, 31);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.Size = new System.Drawing.Size(113, 21);
            this.txtRadio.TabIndex = 4;
            this.txtRadio.TextChanged += new System.EventHandler(this.txtRadio_TextChanged);
            // 
            // cheMouseClick
            // 
            // 
            // 
            // 
            this.cheMouseClick.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cheMouseClick.Location = new System.Drawing.Point(189, 12);
            this.cheMouseClick.Name = "cheMouseClick";
            this.cheMouseClick.Size = new System.Drawing.Size(88, 23);
            this.cheMouseClick.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cheMouseClick.TabIndex = 6;
            this.cheMouseClick.Text = "鼠标单击绘制圆";
            this.cheMouseClick.Visible = false;
            this.cheMouseClick.CheckedChanged += new System.EventHandler(this.cheMouseClick_CheckedChanged);
            // 
            // btnDraw
            // 
            this.btnDraw.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDraw.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDraw.Location = new System.Drawing.Point(228, 31);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(49, 23);
            this.btnDraw.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDraw.TabIndex = 7;
            this.btnDraw.Text = "绘圆";
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // FrmDrawCircle
            // 
            this.AcceptButton = this.btnDraw;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 79);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.cheMouseClick);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtRadio);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtCircleX);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtCircleY);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDrawCircle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "绘制圆";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDrawCircle_FormClosed);
            this.Load += new System.EventHandler(this.FrmDrawCircle_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtCircleY;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCircleX;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRadio;
        private DevComponents.DotNetBar.Controls.CheckBoxX cheMouseClick;
        private DevComponents.DotNetBar.ButtonX btnDraw;
    }
}