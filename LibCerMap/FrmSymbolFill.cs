using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;

namespace LibCerMap
{
    public partial class FrmSymbolFill : OfficeForm
    {
        ISymbologyStyleClass psymbologyStyleClass = null;
        AxSymbologyControl pSymbologyControl = null;
        public ILineSymbol pLineSymbol = null;
        public ILineSymbol pOutLineSymbol = null;
        public ILineSymbol pMarkOutLineSymbol = null;
        public IMarkerSymbol pMarkersymbol = null;
        string strstyle = "";//用于指示目前进行哪个操作
        List<Image> imagelist = new List<Image>();
        //缩放比例
        float Radio = 1;
        //符号库文件名
        public string fileName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
        ISymbol pSymbolOK = null;

        //加载样式
        string[] SymbolStyle;

        public FrmSymbolFill(ISymbologyStyleClass symbologyStyleClass, string[] S)
        {
            InitializeComponent();
            this.EnableGlass = false;
            psymbologyStyleClass = symbologyStyleClass;
            SymbolStyle = S;
        }

        private void FrmSymbolFill_Load(object sender, EventArgs e)
        {           
            colorline.SelectedColor = Color.Black;
            colorpoint.SelectedColor = Color.Black;
            angleline.Value = 0.0;
            lineoffset.Value = 0.0;
            separline.Value = 5.0;
        }

#region 颜色类

 
 
#endregion

#region 添加删除操作

        private void btnremove_Click(object sender, EventArgs e)
        {
            if (supergrid.PrimaryGrid.Rows.Count > 0)
            {
                supergrid.PrimaryGrid.Rows.RemoveAt(0);
                RePainPictureBox(Radio);
            }

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (supergrid.PrimaryGrid.Rows.Count > 0)
            {
                supergrid.PrimaryGrid.Rows.RemoveAt(0);
                RePainPictureBox(Radio);
            }
            if (superTabControl1.SelectedTabIndex == 0)
            {
                if (pLineSymbol == null)
                {
                    MessageBox.Show("请选择填充线的类型", "提示", MessageBoxButtons.OK);
                }
                else if (pOutLineSymbol == null)
                {
                    MessageBox.Show("请选择外框线的类型", "提示", MessageBoxButtons.OK);
                }
                else
                {
                    ILineFillSymbol pLineFillsymbol = CreatLineFillSymbol(pLineSymbol, pOutLineSymbol, ClsGDBDataCommon.ColorToIColor(colorline.SelectedColor), angleline.Value, lineoffset.Value, separline.Value);
                    pSymbolOK = (ISymbol)pLineFillsymbol;
                    addListBoxItem((ISymbol)pLineFillsymbol);
                }
            }
            else if (superTabControl1.SelectedTabIndex == 1)
            {
                if (pMarkersymbol == null)
                {
                    MessageBox.Show("请选择填充点的类型", "提示", MessageBoxButtons.OK);
                }
                else if (pMarkOutLineSymbol == null)
                {
                    MessageBox.Show("请选择外框线的类型", "提示", MessageBoxButtons.OK);
                }
                else
                {
                    IMarkerFillSymbol pMarkerFillSymbol = CreatMarkerFillSymbol(pMarkersymbol, pMarkOutLineSymbol, ClsGDBDataCommon.ColorToIColor(colorpoint.SelectedColor),xoffsetpoint.Value,yoffsetpoint.Value,xsparpoint.Value,ysparpoint.Value);
                    pSymbolOK = (ISymbol)pMarkerFillSymbol;
                    addListBoxItem((ISymbol)pMarkerFillSymbol);
                }
            }

            RePainPictureBox(Radio);
        }

#endregion
     
#region 创建线填充

        private ILineFillSymbol CreatLineFillSymbol(ILineSymbol fillsymbol, ILineSymbol outlinesymbol, IColor pcolor, double angle, double offset, double septer)
        {
            ILineFillSymbol pLineFillSymbol = new LineFillSymbolClass();
            pLineFillSymbol.LineSymbol = fillsymbol;
            pLineFillSymbol.Outline = outlinesymbol;
            pLineFillSymbol.Angle = angle;
            pLineFillSymbol.Color = pcolor;
            pLineFillSymbol.Offset = offset;
            pLineFillSymbol.Separation = septer;
            return pLineFillSymbol;
        }

        private void btnline_Click(object sender, EventArgs e)
        {
            strstyle = "linesymbol";
            FrmSymbolFillSL frmSl = new FrmSymbolFillSL(esriSymbologyStyleClass.esriStyleClassLineSymbols, this, strstyle, SymbolStyle);
            frmSl.StartPosition = FormStartPosition.CenterScreen;
            frmSl.ShowDialog();
        }

        private void btnoutline_Click(object sender, EventArgs e)
        {
            strstyle = "outlinesymbol";
            FrmSymbolFillSL frmSl = new FrmSymbolFillSL(esriSymbologyStyleClass.esriStyleClassLineSymbols, this, strstyle, SymbolStyle);
            frmSl.StartPosition = FormStartPosition.CenterScreen;
            frmSl.ShowDialog();
        }

#endregion

#region 创建点填充

        private IMarkerFillSymbol CreatMarkerFillSymbol(IMarkerSymbol pMarkerSymbol, ILineSymbol pLineSymbol, IColor pColor, double xoffset, double yoffset, double xsept, double ysept)
        {
            IMarkerFillSymbol pMFillSymbol = new MarkerFillSymbolClass();
            IFillProperties pFillProp = new MarkerFillSymbolClass();
            pFillProp.XOffset = xoffset;
            pFillProp.YOffset = yoffset;
            pFillProp.XSeparation = xsept;
            pFillProp.YSeparation = ysept;
            pMFillSymbol = pFillProp as IMarkerFillSymbol;
            pMFillSymbol.MarkerSymbol = pMarkerSymbol;
            pMFillSymbol.Outline = pLineSymbol;
            pMFillSymbol.Color = pColor;
            pMFillSymbol.Style = esriMarkerFillStyle.esriMFSGrid;
            return pMFillSymbol;
        }

        private void btnpoint_Click(object sender, EventArgs e)
        {
            strstyle = "pointsymbol";
            FrmSymbolFillSL frmSl = new FrmSymbolFillSL(esriSymbologyStyleClass.esriStyleClassMarkerSymbols, this, strstyle, SymbolStyle);
            frmSl.StartPosition = FormStartPosition.CenterScreen;
            frmSl.ShowDialog();
        }

        private void btnpointline_Click(object sender, EventArgs e)
        {
            strstyle = "outmarkerlinesymbol";
            FrmSymbolFillSL frmSl = new FrmSymbolFillSL(esriSymbologyStyleClass.esriStyleClassLineSymbols, this, strstyle, SymbolStyle);
            frmSl.StartPosition = FormStartPosition.CenterScreen;
            frmSl.ShowDialog();
        }

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

            //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
            ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
            pSymbolClass.AddItem(styleGalleryItem, 0);
            pSymbolClass.SelectItem(0);

            stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 20);
            Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
            GridPanel panel = supergrid.PrimaryGrid;
            GridRow row = new GridRow(image);
            panel.Rows.Add(row);
            psymbologyStyleClass.RemoveItem(0);
            imagelist.Add(image);

        }

#region 预览图像操作

        private void RePainPictureBox(float ratio)
        {
            //在pictureBox 中预浏览图象
            Bitmap bitmap = new Bitmap(picturepre.Width, picturepre.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(0, 0, 0);

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
                graphics.DrawImage(imagelist[i], rectangle);
                graphics.Save();
            }
            graphics.Dispose();
            picturepre.Image = bitmap;
        }

        //选择缩放比率
        private void cmbPercent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRadio = this.cmbPercent.Text;
            float fradio = float.Parse(strRadio.Substring(0, strRadio.Length - 1));
            Radio = fradio / 100;
            RePainPictureBox(Radio);
        }

#endregion

        private void btnok_Click(object sender, EventArgs e)
        {
            if (pSymbolOK!=null)
            {
                IStyleGalleryItem currentStyleGalleryItem = new ServerStyleGalleryItem();
                currentStyleGalleryItem.Item = pSymbolOK;
                psymbologyStyleClass.AddItem(currentStyleGalleryItem, 0);
                psymbologyStyleClass.SelectItem(0);
                this.Close();    
            } 
            else
            {
            }                
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
