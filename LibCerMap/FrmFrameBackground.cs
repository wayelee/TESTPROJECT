/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmFrameBackground.cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 选择背景
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
using DevComponents.DotNetBar;
using stdole;
namespace LibCerMap
{
    public partial class FrmFrameBackground : OfficeForm
    {
        static public IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();

        private IStyleGalleryItem pStyleGalleryItem = new ServerStyleGalleryItem();
        IFillSymbol pFillSymbol;
        ISymbolBackground SymbolBackground;
        string[] SymbolStyle;
        public FrmFrameBackground(string[] symbolstyle,ISymbolBackground symbolBackground)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            SymbolBackground = symbolBackground;
        }

        private void FrmFrameBackground_Load(object sender, EventArgs e)
        {
            //加载背景样式
            string EsriStylePathFram = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyCtlFrame.LoadStyleFile(EsriStylePathFram);
            axSymbologyCtlFrame.StyleClass = esriSymbologyStyleClass.esriStyleClassBackgrounds;
            if (SymbolBackground != null)
            {
                pStyleGalleryItem.Item = SymbolBackground;
          
                IFillSymbol pFS = SymbolBackground.FillSymbol;
                this.SizeBackgroundOutline.Text = pFS.Outline.Width.ToString();

                IColor pFillColor = pFS.Color;
                Color pColorFill = ColorTranslator.FromOle(pFillColor.RGB);
                colorFill.SelectedColor = pColorFill;

                IColor pOutlineColor = pFS.Outline.Color;
                Color pColorOutline = ColorTranslator.FromOle(pOutlineColor.RGB);
                colorOutline.SelectedColor = pColorOutline;
            }
            else
            {
                SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassBackgrounds);
            }
        }

        //样式选择
        private void axSymbologyCtlFrame_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //选择样式
            pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            if (pStyleGalleryItem != null)
            {
                SymbolBackground = pStyleGalleryItem.Item as ISymbolBackground;
                IFillSymbol pFS = SymbolBackground.FillSymbol;
                this.SizeBackgroundOutline.Text = pFS.Outline.Width.ToString ();

                IColor pFillColor = pFS.Color;
                Color pColorFill = ColorTranslator.FromOle(pFillColor .RGB);
                colorFill.SelectedColor = pColorFill;

                IColor pOutlineColor = pFS.Outline.Color;
                Color pColorOutline = ColorTranslator.FromOle(pOutlineColor .RGB );
                colorOutline.SelectedColor = pColorOutline;
                
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
            axSymbologyCtlFrame.StyleClass = symbologyStyleClass;
            ISymbologyStyleClass pSymbologyStyleClass = axSymbologyCtlFrame.GetStyleClass(symbologyStyleClass);
            pSymbologyStyleClass.SelectItem(0);
        }

        /**
* @fn GetImageByGiveSymbolBeforeSelectItem
* @date 2013.03.16
* @author Ge Xizhi
* @brief 调用背景样式前返回缺省时的图片
* @param styleClass 样式的类型
* @param symbol 样式
* @param width 图片宽度
* @param height 图片高度
* @retval TRUE 成功
* @retval FALSE 失败
*/      

        //选择样式前返回图片
        public Image GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass styleClass, ISymbolBackground symbol, int width, int height)
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
        * @brief 调用背景样式前返回选择样式的图片
        * @param width 图片宽度
        * @param height 图片高度
        * @retval TRUE 成功
        * @retval FALSE 失败
        */

        //选择样式后返回图片
        public Image GetImageByGiveSymbolAfterSelectItem(int width, int height)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择背景式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            ISymbologyStyleClass symbologyStyleClass = axSymbologyCtlFrame.GetStyleClass(axSymbologyCtlFrame.StyleClass);
            //Preview an image of the symbol
            stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, width, height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            return image;
        }

        //填充颜色设置
        private void colorFill_SelectedColorChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem != null)
            {
                SymbolBackground = pStyleGalleryItem.Item as ISymbolBackground;             
                IFillSymbol pFS = SymbolBackground.FillSymbol;
                pFS.Color = ClsGDBDataCommon.ColorToIColor(colorFill.SelectedColor);

                SymbolBackground.FillSymbol = pFS;
                PreviewImage();
            }
        }

        //边线颜色设置
        private void colorOutline_SelectedColorChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem != null)
            {
                SymbolBackground = pStyleGalleryItem.Item as ISymbolBackground;
                IFillSymbol pFS = SymbolBackground.FillSymbol as IFillSymbol;
                ILineSymbol pLS = pFS.Outline as ILineSymbol;

                pLS.Color = ClsGDBDataCommon.ColorToIColor(colorOutline.SelectedColor);     
                pFS.Outline = pLS;
                SymbolBackground.FillSymbol = pFS;
                PreviewImage();
            }
        }

        //边线大小设置
        private void SizeBackgroundOutline_ValueChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem != null)
            {
                SymbolBackground = pStyleGalleryItem.Item as ISymbolBackground;
                IFillSymbol pFS = SymbolBackground.FillSymbol;
                ILineSymbol pLS = pFS.Outline;
              
                pLS.Width = double.Parse(this.SizeBackgroundOutline .Text);

                pFS.Outline = pLS;
                SymbolBackground.FillSymbol = pFS;

                PreviewImage();
            }
        }

        //背景样式选择，调用FrmSy窗口
        private void btBackgroundStyle_Click(object sender, EventArgs e)
        {
            if (pStyleGalleryItem != null)
            {
                ISymbolBackground pSBackground = pStyleGalleryItem.Item as ISymbolBackground;
                IFillSymbol pFS = pSBackground.FillSymbol;
                FrmSymbol Frm = new FrmSymbol(SymbolStyle,(ISymbol)pFS,esriSymbologyStyleClass.esriStyleClassFillSymbols);
                Frm.ShowDialog();
                if (Frm.DialogResult == DialogResult.OK)
                {
                    pFillSymbol = Frm.GetStyleGalleryItem().Item as IFillSymbol;
                    this.SizeBackgroundOutline.Text = pFillSymbol.Outline.Width.ToString();

                    IColor pFillColor = pFillSymbol.Color;
                    Color pColorFill = ColorTranslator.FromOle(pFillColor.RGB);
                    colorFill.SelectedColor = pColorFill;

                    IColor pOutlineColor = pFillSymbol.Outline.Color;
                    Color pColorOutline = ColorTranslator.FromOle(pOutlineColor.RGB);
                    colorOutline.SelectedColor = pColorOutline;

                    SymbolBackground = pStyleGalleryItem.Item as ISymbolBackground;
                    SymbolBackground.FillSymbol = pFillSymbol;

                    PreviewImage();
                }
            }
            else
            {
                MessageBox.Show("请选择一种样式！");
            }
        }

        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择背景样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                CreateBackground();
                this.DialogResult = DialogResult.OK;
                this.Close();
            } 
        }

        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //创建背景（不用了）
        private void CreateBackground()
        {
            //SymbolBackground = pStyleGalleryItem.Item as ISymbolBackground;
            //if (pFillSymbol != null)
            //{
            //    pFillSymbol.Outline.Width = double.Parse(this.SizeBackgroundOutline.Text);
            //    if (colorFill.SelectedColor != null && colorFill .DialogResult ==DialogResult .OK)
            //    {
            //        IRgbColor pRgbColor = new RgbColorClass();
            //        pRgbColor.Red = colorFill.SelectedColor.R;
            //        pRgbColor.Green = colorFill.SelectedColor.G;
            //        pRgbColor.Blue = colorFill.SelectedColor.B;
            //        pFillSymbol.Color = pRgbColor;
            //    }
            //    if (colorOutline.SelectedColor != null && colorOutline.DialogResult ==DialogResult .OK)
            //    {
            //        IRgbColor pRgbColor = new RgbColorClass();
            //        pRgbColor.Red = colorOutline.SelectedColor.R;
            //        pRgbColor.Green = colorOutline.SelectedColor.G;
            //        pRgbColor.Blue = colorOutline.SelectedColor.B;
            //        pFillSymbol.Outline.Color = pRgbColor;
            //    }
            //    SymbolBackground.FillSymbol = pFillSymbol;
            //}
            //else if (pFillSymbol == null)
            //{
            //    IFillSymbol pFL = SymbolBackground.FillSymbol;
            //    pFL.Outline.Width = double.Parse(this.SizeBackgroundOutline.Text);
            //    if (colorFill.SelectedColor != null && colorFill.DialogResult == DialogResult.OK)
            //    {
            //        IRgbColor pRgbColor = new RgbColorClass();
            //        pRgbColor.Red = colorFill.SelectedColor.R;
            //        pRgbColor.Green = colorFill.SelectedColor.G;
            //        pRgbColor.Blue = colorFill.SelectedColor.B;
            //        pFL.Color = pRgbColor;
            //    }
            //    if (colorOutline.SelectedColor != null && colorOutline.DialogResult == DialogResult.OK)
            //    {
            //        IRgbColor pRgbColor = new RgbColorClass();
            //        pRgbColor.Red = colorOutline.SelectedColor.R;
            //        pRgbColor.Green = colorOutline.SelectedColor.G;
            //        pRgbColor.Blue = colorOutline.SelectedColor.B;
            //        pFL.Outline.Color = pRgbColor;
            //    }
            //    SymbolBackground.FillSymbol = pFL;
            //}
        }

        //调用该窗口后返回背景样式
        public ISymbolBackground GetSymbolBackground()
        {
            return SymbolBackground;
        }

        //双击样式选择
        private void axSymbologyCtlFrame_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            btOK_Click(null, null);
        }
    }
}
