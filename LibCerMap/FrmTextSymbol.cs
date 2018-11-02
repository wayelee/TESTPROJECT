/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmTextSymbol.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 文字属性设计窗口
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*张三  2013.03.03  1.0  
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using stdole;
using System.Drawing.Text;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmTextSymbol : OfficeForm
    {
        private IStyleGalleryItem pStyleGalleryItem;

        private IFontDisp pFont = new StdFontClass() as IFontDisp;
        public IGraphicProperties pGraphicProperties = new CommandsEnvironmentClass();
        private ITextSymbol pTextSymbol=new TextSymbolClass();

        public FrmTextSymbol()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void FrmTextSymbol_Load(object sender, EventArgs e)
        {
            //加载样式
            string EsriStylePath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyControl.LoadStyleFile(EsriStylePath);
            axSymbologyControl.StyleClass = esriSymbologyStyleClass.esriStyleClassTextSymbols;
            
           //选择一个样式为默认样式
            SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassTextSymbols);
            //加载字体名称
            InstalledFontCollection pFontCollection = new InstalledFontCollection();
            FontFamily[] pFontFamily = pFontCollection.Families;
            for (int i = 0; i < pFontFamily.Length; i++)
            {
                string pFontName = pFontFamily[i].Name;
                this.cmbBoxFontName.Items.Add(pFontName);
            }
            this.cmbBoxFontName.Text = "宋体";
            
        }

        private void SetFeatureClassStyle(esriSymbologyStyleClass symbologyStyleClass)
        {
            this.axSymbologyControl.StyleClass = symbologyStyleClass;
            ISymbologyStyleClass pSymbologyStyleClass = this.axSymbologyControl.GetStyleClass(symbologyStyleClass);
            pSymbologyStyleClass.SelectItem(0);
        }

        //返回样式图片
        public void GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass styleClass, ISymbol symbol)
        {
            axSymbologyControl.StyleClass = styleClass;
            ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(styleClass);

            IStyleGalleryItem styleGalleryItem = new ServerStyleGalleryItem();
            styleGalleryItem.Item = symbol;

            stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem(styleGalleryItem, ImagePreview.Width, ImagePreview.Height);
            System.Drawing.Image pImage = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            ImagePreview.Image = pImage;
        }
      
        //选择样式
        private void axSymbologyControl_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //选择样式
            pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            if (pStyleGalleryItem != null)
            {
                pTextSymbol = pStyleGalleryItem.Item as ITextSymbol;
                IColor pTextColor = pTextSymbol.Color;
                Color pColor = ColorTranslator.FromOle(pTextColor.RGB);
                FontColor.SelectedColor = pColor;

                cmbBoxFontName.Text = pTextSymbol.Font.Name;
                FontSize.Text = pTextSymbol.Font.Size.ToString ();
            }
            PreviewImage();
        }
    
        //预览
        private void PreviewImage()
        {
            //Get and set the style class 
            ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);

            //Preview an image of the symbol
            stdole.IPictureDisp pPicpture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, ImagePreview.Width, ImagePreview.Height);
            System.Drawing.Image pImage = System.Drawing.Image.FromHbitmap(new System.IntPtr(pPicpture.Handle));

            ImagePreview.Image = pImage;
        }
    
        //设置颜色变化
        private void FontColor_SelectedColorChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择字体样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            pTextSymbol = pStyleGalleryItem.Item as ITextSymbol;
            pTextSymbol.Color = ClsGDBDataCommon.ColorToIColor(FontColor.SelectedColor); ;
            PreviewImage();
        }
   
        //大小设计
        private void FontSize_ValueChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择字体样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            pTextSymbol = pStyleGalleryItem.Item as ITextSymbol;
            string pFontString = double.Parse(FontSize.Text).ToString();
            pFont.Size = decimal.Parse(pFontString);
            pTextSymbol.Font = pFont;
            PreviewImage();
        }
     
        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            CreateTextSymbol();
            this.Close();
        }
        
        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            pTextSymbol = new TextSymbolClass();
            this.Close();
        }

        //创建字体
        private void CreateTextSymbol()
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择指北针样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
      
            pTextSymbol = pStyleGalleryItem.Item as ITextSymbol;

            //颜色
            if (FontColor.SelectedColor != null)
            {
                pTextSymbol.Color = ClsGDBDataCommon.ColorToIColor(FontColor.SelectedColor); ;
            }

            //大小
            string pFontString = double.Parse(FontSize.Text).ToString();
            pFont.Size = decimal.Parse(pFontString);

            //字体、
            pFont.Name = this.cmbBoxFontName.Text;

            //风格
            if (toolBtnBold.Checked == true)
            {
                pFont.Bold = true;
            }
            else
            {
                pFont.Bold = false;
            }
            if (toolBtnIntend.Checked == true)
            {
                pFont.Italic = true;
            }
            else
            {
                pFont.Italic = false;
            }
            if (toolBtnUnderline.Checked == true)
            {
                pFont.Underline = true;
            }
            else
            {
                pFont.Underline = false;
            }
            if (toolBtnStrikethrough.Checked == true)
            {
                pFont.Strikethrough = true;
            }
            else
            {
                pFont.Strikethrough = false;
            }

            pTextSymbol.Font = pFont;
        }
        public ITextSymbol GetTextSymbol()
        {
            return pTextSymbol;
        }
    }
}
