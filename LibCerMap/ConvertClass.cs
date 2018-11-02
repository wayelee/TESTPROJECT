using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ESRI.ArcGIS.Display;

namespace LibCerMap
{
    public class ConvertClass
    {
        //通过指定的高度和宽度生成图象
        public  System.Drawing.Bitmap StyleGalleryItemToBmp(
            int iWidth,
            int iHeight,
            IStyleGalleryClass styleGalleryClass,
            IStyleGalleryItem styleGalleryItem)
        {
            Bitmap bmp = new Bitmap(iWidth, iHeight);
            Graphics gImage = Graphics.FromImage(bmp);


            ESRI.ArcGIS.esriSystem.tagRECT rect = new ESRI.ArcGIS.esriSystem.tagRECT();
            rect.right = bmp.Width;
            rect.bottom = bmp.Height;
            //生成预览
            System.IntPtr hdc = new IntPtr();
            hdc = gImage.GetHdc();
            styleGalleryClass.Preview(styleGalleryItem.Item, hdc.ToInt32(), ref rect);
            gImage.ReleaseHdc(hdc);
            gImage.Dispose();
            return bmp;
        }
    }

}
