using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Analyst3D;

using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;

namespace LibCerMap
{
    public partial class FrmSymbolPoint : OfficeForm
    {
        ISymbologyStyleClass psymbologyStyleClass = null;
        //符号库文件名
        public string fileName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
        string picfilename = "";//图片路径
        string filename3D = "";//模型路径
        bool isuse3d = true;//是否应用纹理

        List<Image> imagelist = new List<Image>();
        List<ISymbol> Symbollist = new List<ISymbol>();
        IMultiLayerMarkerSymbol pMultiLineSymbol = new MultiLayerMarkerSymbolClass();

        ////保存符号的偏移量
        //List<double> arrayXOffset = new List<double>();
        //List<double> arrayYOffset = new List<double>();
        ////保存符号的大小
        //List<double> arraySize = new List<double>();
        ////保存符号的颜色
        //List<Color> arrayColor = new List<Color>();
        ////保存符号的角度
        //List<double> arrayAngle = new List<double>();

        //缩放比例

        float Radio = 1;

        public FrmSymbolPoint(ISymbologyStyleClass symbologyStyleClass)
        {
            InitializeComponent();
            this.EnableGlass = false;
            psymbologyStyleClass = symbologyStyleClass;
        }

        private void FrmSymbolPoint_Load(object sender, EventArgs e)
        {
            colorsimple.SelectedColor = Color.Black;
            colorarrow.SelectedColor = Color.Black;
            backcolorpic.SelectedColor = Color.Transparent;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (superTabControl1.SelectedTabIndex == 0)
            {
                esriSimpleMarkerStyle style = new esriSimpleMarkerStyle();
                if (cbSimpleStyle.SelectedIndex == 0)
                {
                    style = esriSimpleMarkerStyle.esriSMSCircle;
                }
                else if (cbSimpleStyle.SelectedIndex == 1)
                {
                    style = esriSimpleMarkerStyle.esriSMSSquare;
                }
                else if (cbSimpleStyle.SelectedIndex == 2)
                {
                    style = esriSimpleMarkerStyle.esriSMSCross;
                }
                else if (cbSimpleStyle.SelectedIndex == 3)
                {
                    style = esriSimpleMarkerStyle.esriSMSX;
                }
                else
                {
                    style = esriSimpleMarkerStyle.esriSMSDiamond;
                }


                if (superTabControl1.SelectedTabIndex == 0)
                {
                    if (checkBoxX1.Checked == true)
                    {
                        ISimpleMarkerSymbol pSimMarSymbol = creatsimpoint(colorsimple.SelectedColor, style, sizesimple.Value, anglesimple.Value, xsimoffset.Value,
                                                                           ysimoffset.Value, outcolor.SelectedColor, outsize.Value);
                        addListBoxItem(pSimMarSymbol as ISymbol);

                    }
                    else
                    {
                        ISimpleMarkerSymbol pSimMarSymbol = creatsimpoint(colorsimple.SelectedColor, style, sizesimple.Value, anglesimple.Value, xsimoffset.Value,
                                                                           ysimoffset.Value, Color.Empty, 0);
                        addListBoxItem(pSimMarSymbol as ISymbol);
                    }
                }
            }

            else if(superTabControl1.SelectedTabIndex==1)
            {
                if (picfilename=="")
                {
                    MessageBox.Show("请先选择图片", "提示", MessageBoxButtons.OK);
                } 
                else
                {
                    IPictureMarkerSymbol pPicMarSymbol=creatpicpoint(sizepicture.Value, xpicoffset.Value, ypicoffset.Value, anglepicture.Value, backcolorpic.SelectedColor, picfilename);
                    addListBoxItem(pPicMarSymbol as ISymbol);
                }
            }

            else if (superTabControl1.SelectedTabIndex == 2)
            {
                IArrowMarkerSymbol pArrMarPoint= creatarrowpoint(colorarrow.SelectedColor, lengtharrow.Value, widtharrow.Value, anglearrow.Value, xarroffset.Value, yarroffset.Value);
                addListBoxItem(pArrMarPoint as ISymbol);
            }

            else if (superTabControl1.SelectedTabIndex == 3)
            {
                if (filename3D=="")
                {
                    MessageBox.Show("请先选择模型", "提示", MessageBoxButtons.OK);
                } 
                else
                {
                    IMarker3DSymbol p3DMarSymbol = creat3dmarkerpoint(isuse3d, filename3D);
                    addListBoxItem(p3DMarSymbol as ISymbol);
                }
            }

            RePainPictureBox(Radio);
        }


#region 移除、移动操作

        private void btnremove_Click(object sender, EventArgs e)
        {
            if (supergrid.PrimaryGrid.Rows.Count > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow rurow = null;
                foreach (GridRow item in items)
                {
                    rurow = item;
                    int n = rurow.Index;
                    supergrid.PrimaryGrid.Rows.Remove(item);
                    imagelist.Remove(imagelist[n]);
                    Symbollist.Remove(Symbollist[n]);

                    RePainPictureBox(Radio);
                }
                
            }
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                if (supergrid.PrimaryGrid.Rows.Count > 1)
                {
                    SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                    GridRow rerow = null;
                    foreach (GridRow item in items)
                    {
                        rerow = item;
                        int n = rerow.Index;
                        if (n != 0)
                        {
                            supergrid.PrimaryGrid.Rows.Remove(item);
                            supergrid.PrimaryGrid.Rows.Insert(n - 1, rerow);
                            Image imagetemp = null;
                            imagetemp = imagelist[n];
                            imagelist[n] = imagelist[n - 1];
                            imagelist[n - 1] = imagetemp;
                            ISymbol symboltemp = Symbollist[n];
                            Symbollist[n] = Symbollist[n - 1];
                            Symbollist[n - 1] = symboltemp;
                        }

                        RePainPictureBox(Radio);
                    }
                }
            }
        }

        private void btndown_Click(object sender, EventArgs e)
        {
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                if (supergrid.PrimaryGrid.Rows.Count > 1)
                {
                    SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                    GridRow rerow = null;
                    foreach (GridRow item in items)
                    {
                        rerow = item;
                        int n = rerow.Index;
                        if (n != supergrid.PrimaryGrid.Rows.Count - 1)
                        {
                            supergrid.PrimaryGrid.Rows.Remove(item);
                            supergrid.PrimaryGrid.Rows.Insert(n + 1, rerow);
                            Image imagetemp = null;
                            imagetemp = imagelist[n];
                            imagelist[n] = imagelist[n + 1];
                            imagelist[n + 1] = imagetemp;
                            ISymbol symboltemp = Symbollist[n];
                            Symbollist[n] = Symbollist[n + 1];
                            Symbollist[n + 1] = symboltemp;
                        }

                        RePainPictureBox(Radio);
                    }
                }
            }
        }

  
#endregion
 
#region 生成简单点

        private ISimpleMarkerSymbol creatsimpoint(Color color, esriSimpleMarkerStyle style, double size, double angle, double xoffset, double yoffset, Color outcolor, double outsize)
        {
            ISimpleMarkerSymbol pSimSymbol = new SimpleMarkerSymbolClass();
            pSimSymbol.Color = ClsGDBDataCommon.ColorToIColor(color);
            pSimSymbol.Style = style;
            pSimSymbol.Size = size;
            pSimSymbol.Angle = angle;
            pSimSymbol.XOffset = xoffset;
            pSimSymbol.YOffset = yoffset;

            if (checkBoxX1.Checked == true)
            {
                pSimSymbol.Outline = true;
                pSimSymbol.OutlineColor = ClsGDBDataCommon.ColorToIColor(outcolor);
                pSimSymbol.OutlineSize = outsize;
            }
            else
            {
                pSimSymbol.Outline = false;
            }

            return pSimSymbol;
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked == true)
            {
                lbloutcolor.Enabled = true;
                outcolor.Enabled = true;
                lbloutsize.Enabled = true;
                outsize.Enabled = true;
                outcolor.SelectedColor = Color.Red;
            }
            else
            {
                lbloutcolor.Enabled = false;
                outcolor.Enabled = false;
                lbloutsize.Enabled = false;
                outsize.Enabled = false;
            }
        }

#endregion

#region 生成图片点

        private void btnpicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "图片（bmp）|*.bmp|所有文件|*.*";
            openfile.FilterIndex = 0;
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                picfilename = openfile.FileName;
                lblpicture.Text = picfilename;
            }
        }

        private IPictureMarkerSymbol creatpicpoint(double size, double xoffset, double yoffset, double angle, Color backcolor, string picfile)
        {
            IPictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbolClass();
            pictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, picfile);
            pictureMarkerSymbol.Size = size;
            pictureMarkerSymbol.Angle = angle;
            pictureMarkerSymbol.XOffset = xoffset;
            pictureMarkerSymbol.YOffset = yoffset;
            pictureMarkerSymbol.BackgroundColor = ClsGDBDataCommon.ColorToIColor(backcolor);
            return pictureMarkerSymbol;
        }


#endregion

#region 生成箭头点

        private IArrowMarkerSymbol creatarrowpoint(Color color,double length,double width,double angle,double xoffset,double yoffset)
        {
            IArrowMarkerSymbol arrowMarkerSymbol = new ArrowMarkerSymbolClass();
            arrowMarkerSymbol.Color = ClsGDBDataCommon.ColorToIColor(color);
            arrowMarkerSymbol.Length = length;
            arrowMarkerSymbol.Width = width;
            arrowMarkerSymbol.Angle = angle;
            arrowMarkerSymbol.XOffset = xoffset;
            arrowMarkerSymbol.YOffset = yoffset;


            return arrowMarkerSymbol;
        }

#endregion

#region 生成三维点

        private void btn3d_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "3d 模型（3ds）|*.3ds";
            openfile.FilterIndex = 0;
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                filename3D = openfile.FileName;
                lbl3d.Text = filename3D;
            }
        }

        private void chk3dmaterial_CheckedChanged(object sender, EventArgs e)
        {
            if (chk3dmaterial.Checked==true)
            {
                isuse3d = true;
            } 
            else
            {
                isuse3d = false;
            }
        }

        private IMarker3DSymbol creat3dmarkerpoint(bool isuse,string filename)
        {
            IMarker3DSymbol pMar3DSymbol = new Marker3DSymbolClass();
            pMar3DSymbol.CreateFromFile(filename3D);
            pMar3DSymbol.UseMaterialDraping = isuse;
            return pMar3DSymbol;
        }

#endregion
    
#region 预览图像操作

        private void RePainPictureBox(float ratio)
        {
            //在pictureBox 中预浏览图象
            Bitmap bitmap = new Bitmap(picturepre.Width, picturepre.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(0, 0, 0);

            System.Drawing.Pen pen = new Pen(new SolidBrush(Color.Gray), 1);

            pen.DashStyle = DashStyle.DashDot;
            graphics.DrawLine(pen, new PointF(0, picturepre.Height / 2), new PointF(picturepre.Width, picturepre.Height / 2));
            graphics.DrawLine(pen, new PointF(picturepre.Width / 2, 0), new PointF(picturepre.Width / 2, picturepre.Height));

            Bitmap image;
            int startX;
            int startY;
            for (int i = 0; i < supergrid.PrimaryGrid.Rows.Count; i++)
            {
                image = (Bitmap)imagelist[i];
                startX = picturepre.Width / 2 - (int)(imagelist[i].Size.Width * ratio) / 2;
                startY = picturepre.Height / 2 - (int)(imagelist[i].Size.Height * ratio) / 2;

                image.MakeTransparent(Color.Transparent);
                System.Drawing.Rectangle rectangle = new Rectangle(startX, startY, (int)(imagelist[i].Size.Width * ratio), (int)(imagelist[i].Size.Height * ratio));
                graphics.DrawImage(imagelist[i],rectangle);
                graphics.Save();
            }
            graphics.Dispose();
            picturepre.Image = bitmap;
        }

        //选择缩放比率
        private void cmbPercent_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string strRadio = this.cmbPercent.Text;
            float fradio = float.Parse(strRadio.Substring(0, strRadio.Length - 1));
            Radio = fradio / 100;
            RePainPictureBox(Radio);
        }
      
#endregion

#region 使用类

 
  
#endregion


        //添加GLISTBOX项
        private void addListBoxItem(ISymbol symbol)
        {

            IStyleGallery styleGallery;
            IStyleGalleryItem styleGalleryItem;
            IStyleGalleryStorage styleGalleryStorge;

            styleGalleryItem = new ServerStyleGalleryItemClass();
            styleGalleryItem.Name = this.txtSymbolName.Text;
            styleGalleryItem.Category = "default";
            object objSymbol = symbol;
            styleGalleryItem.Item = objSymbol;

            styleGallery = new ServerStyleGalleryClass();
            styleGalleryStorge = styleGallery as IStyleGalleryStorage;
            styleGalleryStorge.TargetFile = fileName;

            ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
            pSymbolClass.AddItem(styleGalleryItem, 0);
            pSymbolClass.SelectItem(0);
            Image image=null;
            if (symbol is IMarker3DSymbol)
            {
                IMarker3DSymbol mar3dsym=symbol as IMarker3DSymbol;
                stdole.IPicture pic = mar3dsym.Thumbnail ;
                image = Image.FromHbitmap(new System.IntPtr(pic.Handle));
            } 
            else
            {
                stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
            }
            
            GridPanel panel = supergrid.PrimaryGrid;
            GridRow row = new GridRow(image);
            panel.Rows.Add(row);
            psymbologyStyleClass.RemoveItem(0);
            imagelist.Add(image);
            Symbollist.Add(symbol);

        }

        private void btnok_Click(object sender, EventArgs e)
        {
            ////构建图片存储临时目录
            //FileInfo fileInfo = new FileInfo(fileName);
            //string path = fileInfo.DirectoryName;
            //string bitmapFileName = path + @"\" + this.txtSymbolName.Text + ".bmp";
            ////创建新的画图，并将底色清为白色
            ////Bitmap bitmap = new Bitmap(100, 100, PixelFormat.Format24bppRgb);
            //Bitmap bitmap = new Bitmap(listBoxImage.ImageSize.Width - 100, listBoxImage.ImageSize.Height);
            //Graphics graphics = Graphics.FromImage(bitmap);
            //System.Drawing.Color color = System.Drawing.Color.FromArgb(0, 0, 0);
            //graphics.Clear(Color.White);
            ////将各个子样式在画图上绘制
            //Bitmap image;
            //int startX;
            //int startY;
            //for (int i = 0; i < supergrid.PrimaryGrid.Rows.Count; i++)
            //{
            //    image = (Bitmap)imagelist[i];
            //    startX = picturepre.Width / 2 - (int)(imagelist[i].Size.Width * Radio) / 2;
            //    startY = picturepre.Height / 2 - (int)(imagelist[i].Size.Height * Radio) / 2;

            //    image.MakeTransparent(Color.Transparent);
            //    System.Drawing.Rectangle rectangle = new Rectangle(startX-50, startY-50, (int)(imagelist[i].Size.Width * Radio), (int)(imagelist[i].Size.Height * Radio));
            //    graphics.DrawImage(imagelist[i], rectangle);
            //    graphics.Save();
            //}
            //graphics.SmoothingMode = SmoothingMode.HighQuality;
            //graphics.Dispose();
            ////将图片保存在临时目录
            //bitmap.Save(bitmapFileName, System.Drawing.Imaging.ImageFormat.Bmp);
            ////创建图片样式
            //IPictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbolClass();
            //pictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, bitmapFileName);
            //pictureMarkerSymbol.Angle = 0;

            //pictureMarkerSymbol.Size = 30;
            //pictureMarkerSymbol.XOffset = 0;
            //pictureMarkerSymbol.YOffset = 0;
            for (int i=0;i<Symbollist.Count;i++)
            {
                pMultiLineSymbol.AddLayer(Symbollist[i] as IMarkerSymbol);
            }
            

            IStyleGallery styleGallery;
            IStyleGalleryItem styleGalleryItem;
            IStyleGalleryStorage styleGalleryStorge;
            //创建新的样式
            styleGalleryItem = new ServerStyleGalleryItemClass();
            styleGalleryItem.Name = this.txtSymbolName.Text;
            styleGalleryItem.Category = "default";
            object objSymbol = pMultiLineSymbol;
            styleGalleryItem.Item = objSymbol;

            styleGallery = new ServerStyleGalleryClass();
            styleGalleryStorge = styleGallery as IStyleGalleryStorage;
            styleGalleryStorge.TargetFile = fileName;
            //添加新样式
            //styleGallery.AddItem(styleGalleryItem);

            ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
            pSymbolClass.AddItem(styleGalleryItem, 0);
            pSymbolClass.SelectItem(0);

            //保存新样式
            //styleGallery.SaveStyle(fileName, fileInfo.Name, "marker Symbols");

            this.Close();
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       



    }
}
