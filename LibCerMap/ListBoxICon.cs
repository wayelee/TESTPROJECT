using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms ;
using System.Drawing ;




namespace LibCerMap
{
    

    // GListBox 类对系统自带的listbox进行扩展，可以显示图片
    //读者可以在此上进行再扩展
    public class GListBox : ListBox
    {
        //显示图象列表
        private ImageList _myImageList;
        public ImageList ImageList
        {
            get { return _myImageList; }
            set { _myImageList = value; }
        }
        public GListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        //重写子项的绘制
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            GListBoxItem item;
            Rectangle bounds = e.Bounds;
            Size imageSize = _myImageList.ImageSize;
            try
            {
                item = (GListBoxItem)Items[e.Index];
                if (item.ImageIndex != -1)
                {
                    ImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                    e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                      bounds.Left + imageSize.Width, bounds.Top);
                    
                }
                else
                {
                    e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                      bounds.Left, bounds.Top);
                }
            }
            catch
            {
                if (e.Index != -1)
                {
                    e.Graphics.DrawString(Items[e.Index].ToString(), e.Font,
                      new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                }
                else
                {
                    e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor),
                      bounds.Left, bounds.Top);
                }
            }
            finally
            {
                base.OnDrawItem(e);
            }
            
        }
    }
    // GListBoxItem 类
    public class GListBoxItem
    {
        private string _myText;
        private int _myImageIndex;
        private CheckBox _myCheck;
        // 属性
        public string Text
        {
            get { return _myText; }
            set { _myText = value; }
        }
        public int ImageIndex
        {
            get { return _myImageIndex; }
            set { _myImageIndex = value; }
        }

        //构造函数
        public GListBoxItem(string text, int index)
        {
            _myText = text;
            _myImageIndex = index;
        }
        public GListBoxItem(string text) : this(text, -1) { }
        public GListBoxItem() : this("") { }
        public GListBoxItem(int index)
        {
            _myCheck = new CheckBox();
            _myImageIndex = index;
        }
        public override string ToString()
        {
            return _myText;
        }
    }
    class ListBoxICon
    {
    }
}
