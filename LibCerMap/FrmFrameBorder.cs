/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file  FrmFrameBorder.cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 选择边框
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Drawing.Text;
using stdole;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmFrameBorder : OfficeForm
    {
        static public IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
      
        private IStyleGalleryItem pStyleGalleryItem = new ServerStyleGalleryItem();
        ILineSymbol pLineSymbol;
        ISymbolBorder SymbolBorder;

        string[] SymbolStyle;
        public FrmFrameBorder(string[] symbolstyle,ISymbolBorder symbolborder)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            SymbolBorder = symbolborder;
        }

        private void FrmFrameBorder_Load(object sender, EventArgs e)
        {
            //加载边框样式
            string EsriStylePathFram = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyCtlFrame.LoadStyleFile(EsriStylePathFram);
            axSymbologyCtlFrame.StyleClass = esriSymbologyStyleClass.esriStyleClassBorders;
         
            if (SymbolBorder != null)
            {
                pStyleGalleryItem.Item = SymbolBorder;

                ILineSymbol PLS = SymbolBorder.LineSymbol;
                this.SizeBorder.Text = PLS.Width.ToString();
                IColor pLineColor = PLS.Color;
                Color pColor = ColorTranslator.FromOle(pLineColor.RGB);
                colorBorder.SelectedColor = pColor;
                pLineSymbol = null;

                PreviewImage();
            }
            else
            {
                SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassBorders);
            }
        }

        //选择边框样式
        private void axSymbologyCtlFram_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //选择样式
            pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;

            if (pStyleGalleryItem != null)
            {
                SymbolBorder = (ISymbolBorder)pStyleGalleryItem.Item;
                ILineSymbol PLS=SymbolBorder .LineSymbol ;
                this.SizeBorder.Text = PLS.Width.ToString ();
                IColor pLineColor = PLS.Color;
                Color pColor = ColorTranslator.FromOle(pLineColor .RGB );
                colorBorder.SelectedColor = pColor;
                pLineSymbol = null;
                
            }
            PreviewImage();
        }

        //预览
        private void PreviewImage()
        {
            //Get and set the style class 
            ISymbologyStyleClass symbologyStyleClass = axSymbologyCtlFrame.GetStyleClass(axSymbologyCtlFrame.StyleClass);

            //Preview an image of the symbol
            stdole.IPictureDisp pPicpture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, ImagePreview.Width, ImagePreview.Height);
            System.Drawing.Image pImage = System.Drawing.Image.FromHbitmap(new System.IntPtr(pPicpture.Handle));

            ImagePreview.Image = pImage;
        }
        /// <summary>
        /// 初始化SymbologyControl的StyleClass,图层如果已有符号,则把符号添加到SymbologyControl中的第一个符号,并选中  
        /// </summary>
        /// <param name="symbologyStyleClass"></param>
        private void SetFeatureClassStyle(esriSymbologyStyleClass symbologyStyleClass)
        {
            this.axSymbologyCtlFrame.StyleClass = symbologyStyleClass;
            ISymbologyStyleClass pSymbologyStyleClass = this.axSymbologyCtlFrame.GetStyleClass(symbologyStyleClass);
            pSymbologyStyleClass.SelectItem(0);
        }
        /**
* @fn GetImageByGiveSymbolBeforeSelectItem
* @date 2013.03.16
* @author Ge Xizhi
* @brief 调用边框样式前返回缺省时的图片
* @param styleClass 样式的类型
* @param symbol 样式
* @param width 图片宽度
* @param height 图片高度
* @retval TRUE 成功
* @retval FALSE 失败
*/      
        //选择样式前返回图片
        public Image GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass styleClass,ISymbolBorder symbol, int width, int height)
        {
            axSymbologyCtlFrame.StyleClass = styleClass;
            ISymbologyStyleClass symbologyStyleClass = axSymbologyCtlFrame.GetStyleClass(styleClass);

            IStyleGalleryItem styleGalleryItem = new ServerStyleGalleryItem();
            styleGalleryItem.Item = symbol;
            symbologyStyleClass.AddItem(styleGalleryItem, 0);
            symbologyStyleClass.SelectItem(0);
            stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, width, height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            return image;
        }
    
        /**
      * @fn GetImageByGiveSymbolAfterSelectItem
      * @date 2013.03.16
      * @author Ge Xizhi
      * @brief 调用边框样式前返回选择样式的图片
      * @param width 图片宽度
      * @param height 图片高度
      * @retval TRUE 成功
      * @retval FALSE 失败
      */
        //选择样式后返回图片
        public Image GetImageByGiveSymbolAfterSelectItem(int width, int height)
        {
            ISymbologyStyleClass symbologyStyleClass = axSymbologyCtlFrame.GetStyleClass(axSymbologyCtlFrame.StyleClass);
            stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, width,height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            return image;
        }

        //边框大小
        private void SizeBorder_ValueChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem != null)
            {
                SymbolBorder = (ISymbolBorder)pStyleGalleryItem.Item;
                ILineSymbol pLS = SymbolBorder.LineSymbol;
                pLS.Width = double.Parse(this.SizeBorder .Text);
                SymbolBorder.LineSymbol = pLS;
                PreviewImage();
            }
        }

        //边框颜色
        private void colorBorder_SelectedColorChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem != null)
            {
                SymbolBorder = (ISymbolBorder)pStyleGalleryItem.Item;
                ILineSymbol pLS = SymbolBorder.LineSymbol;         

                pLS.Color = ClsGDBDataCommon.ColorToIColor(colorBorder.SelectedColor);
                SymbolBorder.LineSymbol = pLS;
                PreviewImage();
            }
        }

        //边框样式选择
        private void btBorderStyle_Click(object sender, EventArgs e)
        {
            if (pStyleGalleryItem != null)
            {
                ISymbolBorder pSBorder = (ISymbolBorder)pStyleGalleryItem.Item;
                ILineSymbol pLS = pSBorder .LineSymbol;
                FrmSymbol Frm = new FrmSymbol(SymbolStyle,(ISymbol)pLS,esriSymbologyStyleClass.esriStyleClassLineSymbols);
                Frm.ShowDialog();
                if (Frm.DialogResult == DialogResult.OK)
                {
                    pLineSymbol = Frm.GetStyleGalleryItem().Item as ILineSymbol;
                    this.SizeBorder.Text = pLineSymbol.Width.ToString();
                    IColor pLineColor = pLineSymbol.Color;
                    Color pColor = ColorTranslator.FromOle(pLineColor.RGB);
                    colorBorder.SelectedColor = pColor;
                    SymbolBorder = (ISymbolBorder)pStyleGalleryItem.Item;
                    SymbolBorder.LineSymbol = pLineSymbol;

                    PreviewImage();
                }
            }
            else
            {
                MessageBox.Show("请选择一种样式!");
            }
        }

        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择边框样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                CreateSymbolBorder();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //创建边框（不用了）
        private void CreateSymbolBorder()
        {
            //SymbolBorder = (ISymbolBorder)pStyleGalleryItem.Item;
            //if (pLineSymbol != null)
            //{
            //    pLineSymbol.Width = double.Parse(this.SizeBorder .Text);
            //    if (this.colorBorder.SelectedColor != null)
            //    {
            //        IRgbColor pRgbColor = new RgbColorClass();
            //        pRgbColor.Red = colorBorder.SelectedColor.R;
            //        pRgbColor.Green = colorBorder.SelectedColor.G;
            //        pRgbColor.Blue = colorBorder.SelectedColor.B;
            //        pLineSymbol.Color = pRgbColor;
            //    }
            //    SymbolBorder.LineSymbol = pLineSymbol; 
            //}
            //else if (pLineSymbol == null)
            //{
            //    ILineSymbol pLS = SymbolBorder.LineSymbol;
            //    pLS.Width = double.Parse(this.SizeBorder.Text);
            //    if (this.colorBorder.SelectedColor != null)
            //    {
            //        IRgbColor pRgbColor = new RgbColorClass();
            //        pRgbColor.Red = colorBorder.SelectedColor.R;
            //        pRgbColor.Green = colorBorder.SelectedColor.G;
            //        pRgbColor.Blue = colorBorder.SelectedColor.B;
            //        pLS.Color = pRgbColor;
            //    }
            //    SymbolBorder.LineSymbol = pLS;
            //}
        }

        //调用该窗口时返回边框样式
        public ISymbolBorder GetSymbolBorder()
        {
            return SymbolBorder;
        }

        private void axSymbologyCtlFrame_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            btOK_Click(null, null);
        }
    }
}
