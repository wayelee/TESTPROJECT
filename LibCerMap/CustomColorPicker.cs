using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevComponents.DotNetBar;
using System.Drawing;

namespace LibCerMap
{
    //自定义颜色控件
    class CustomColorPicker : DevComponents.DotNetBar.ColorPickerButton
    {
        private DevComponents.DotNetBar.ButtonItem buttonItemNoColor;
        public CustomColorPicker()
        {
            buttonItemNoColor = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemNoColor.GlobalItem = false;
            this.buttonItemNoColor.Name = "buttonItemNoColor";
            this.buttonItemNoColor.Text = "NoColor";

            buttonItemNoColor.Click += new EventHandler(buttonItemNoColor_Click);
            base.PopupOpen += new EventHandler(CustomColorPicker_PopupOpen);
            base.PopupClose += new EventHandler(CustomColorPicker_PopupClose);

        }
        public void CustomColorPicker_PopupOpen(object sender, EventArgs e)
        {
            SubItems.Add(buttonItemNoColor);
        }
        public void buttonItemNoColor_Click(object sender, EventArgs e)
        {
            SelectedColor = Color.FromArgb(0, 0, 0, 0);
            
            Symbol = "";
        }
        public void CustomColorPicker_PopupClose(object sender, EventArgs e)
        {
            if (SelectedColor.A == 0 /*&& SelectedColor.R == 255*/)
            {
                Symbol = "";
            }
            else
            {
                Symbol = "";
            }
        }

       
    }
}
