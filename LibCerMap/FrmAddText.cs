/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmAddText.cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 添加文本，以及文本属性编辑
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
    public partial class FrmAddText : OfficeForm
    {
        //类内部维护的实例对象
        public ITextElement pTextElement = new TextElementClass();
        //原来的对象
        public ITextElement pSrcTextElement = null;
        //用于刷新
        IHookHelper m_hookHelper = null;

        private void deepCopyToTextElement(ITextElement dstTextElement)
        {
            if (dstTextElement == null)
                return;

            IClone pSrcClone = pTextElement as IClone;
            IClone pDstClone = pSrcClone.Clone();
            ITextElement pTmpElement = pDstClone as ITextElement;

            dstTextElement.Symbol = pTmpElement.Symbol;
            try
            { 
                dstTextElement.ScaleText = pTmpElement.ScaleText;
            }
            catch (System.Exception ex)
            {
                int a = 0;
                a++;
            }
           
            dstTextElement.Text = pTmpElement.Text;

        }
        private void deepCopyFromTextElement(ITextElement srcTextElement)
        {
            if (srcTextElement == null)
                return;

            IClone pSrcClone = srcTextElement as IClone;
            IClone pDstClone = pSrcClone.Clone();
            pTextElement = pDstClone as ITextElement;
        }

        //构造器
        public FrmAddText(ITextElement element, IHookHelper hookHelper)
        {
            InitializeComponent();

            pSrcTextElement = element;
            deepCopyFromTextElement(element);

            m_hookHelper = hookHelper;
        }

        private void updateUI()
        {
            //加载字体名称
            InstalledFontCollection pFontCollection = new InstalledFontCollection();
            FontFamily[] pFontFamily = pFontCollection.Families;
            for (int i = 0; i < pFontFamily.Length; i++)
            {
                string pFontName = pFontFamily[i].Name;
                this.cmbBoxFontName.Items.Add(pFontName);
            }

            //大小、角度、字体名
            ITextSymbol pTextSymbol = pTextElement.Symbol;
            this.txtAddText.Text = pTextSymbol.Text;
            this.cmbBoxFontName.Text = pTextSymbol.Font.Name;
            this.TextAngle.Value = pTextSymbol.Angle;
            this.FontSize.Value = Convert.ToDouble(pTextSymbol.Font.Size);

            //字符间距、行间距
            IFormattedTextSymbol pFormattedTextSymbol = pTextSymbol as IFormattedTextSymbol;
            double dbCharSpacing = pFormattedTextSymbol.CharacterSpacing;
            double dbLineLeading = pFormattedTextSymbol.Leading;
            dbiCharSpacing.Value = dbCharSpacing;
            dbiLineLeading.Value = dbLineLeading;

            //颜色
            IColor pTextColor = pTextSymbol.Color;
            Color pColor = ColorTranslator.FromOle(pTextColor.RGB);
            FontColor.SelectedColor = pColor;

            //样式
            IFontDisp pFont = pTextSymbol.Font;
            this.toolBtnBold.Checked = pFont.Bold;
            this.toolBtnIntend.Checked = pFont.Italic;
            this.toolBtnUnderline.Checked = pFont.Underline;
            this.toolBtnStrikethrough.Checked = pFont.Strikethrough;

            //文字对齐方式
            esriTextHorizontalAlignment pHorAlign = pTextSymbol.HorizontalAlignment;
            if (pHorAlign == esriTextHorizontalAlignment.esriTHACenter)
            {
                this.toolBtnLeft.Checked = false;
                this.toolBtnRight.Checked = false;
                this.toolBtnCenter.Checked = true;
                this.toolBtnBoth.Checked = false;
            }
            else if (pHorAlign == esriTextHorizontalAlignment.esriTHAFull)
            {
                this.toolBtnLeft.Checked = false;
                this.toolBtnRight.Checked = false;
                this.toolBtnCenter.Checked = false;
                this.toolBtnBoth.Checked = true;
            }
            else if (pHorAlign == esriTextHorizontalAlignment.esriTHALeft)
            {
                this.toolBtnLeft.Checked = true;
                this.toolBtnRight.Checked = false;
                this.toolBtnCenter.Checked = false;
                this.toolBtnBoth.Checked = false;
            }
            else if (pHorAlign == esriTextHorizontalAlignment.esriTHARight)
            {
                this.toolBtnLeft.Checked = false;
                this.toolBtnRight.Checked = true;
                this.toolBtnCenter.Checked = false;
                this.toolBtnBoth.Checked = false;
            }
            else
                ;
        }

        private void updateFromUI()
        {
            ITextSymbol pTmpTextSymbol = new TextSymbolClass();

            //字体
            IFontDisp pFont = new StdFontClass() as IFontDisp;
            pFont.Name = this.cmbBoxFontName.Text;
            pFont.Size = decimal.Parse(this.FontSize.Text);
            pFont.Bold = this.toolBtnBold.Checked;
            pFont.Italic = this.toolBtnIntend.Checked;
            pFont.Underline = this.toolBtnUnderline.Checked;
            pFont.Strikethrough = this.toolBtnStrikethrough.Checked;
            pTmpTextSymbol.Font = pFont;

            //颜色
            if (this.FontColor.SelectedColor != null)
            {
                pTmpTextSymbol.Color = ClsGDBDataCommon.ColorToIColor(FontColor.SelectedColor);
            }

            //角度
            pTmpTextSymbol.Angle = double.Parse(this.TextAngle.Text);

            //字体对齐方式
            if (this.toolBtnLeft.Checked == true)
            {
                pTmpTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            }
            else if (this.toolBtnCenter.Checked == true)
            {
                pTmpTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            }
            else if (this.toolBtnRight.Checked == true)
            {
                pTmpTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
            }
            else if (this.toolBtnBoth.Checked == true)
            {
                pTmpTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull;
            }

            //字符间距、行间距
            IFormattedTextSymbol pFormattedTextSymbol = pTmpTextSymbol as IFormattedTextSymbol;
            pFormattedTextSymbol.CharacterSpacing = dbiCharSpacing.Value;
            pFormattedTextSymbol.Leading = dbiLineLeading.Value;

            pTextElement.Symbol = pTmpTextSymbol;
            pTextElement.Text = this.txtAddText.Text;
        }

        private void FrmAddText_Load(object sender, EventArgs e)
        {
            if (pTextElement != null)
                updateUI();
        }

        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            updateFromUI();
            deepCopyToTextElement(pSrcTextElement);
            if (m_hookHelper != null)
                m_hookHelper.ActiveView.Refresh();
        }

         private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //应用按钮
        private void btApply_Click(object sender, EventArgs e)
        {
            updateFromUI();
            deepCopyToTextElement(pSrcTextElement);
            //CreateText();
            if (m_hookHelper != null)
                m_hookHelper.ActiveView.Refresh();
        }
        //字体对齐方式按钮：左对齐、居中、右对齐、两端对齐

        private void toolBtnRight_Click(object sender, EventArgs e)
        {

            this.toolBtnLeft.Checked = false;
            this.toolBtnRight.Checked = true;
            this.toolBtnCenter.Checked = false;
            this.toolBtnBoth.Checked = false;

        }

        private void toolBtnLeft_Click(object sender, EventArgs e)
        {

            this.toolBtnLeft.Checked = true;
            this.toolBtnRight.Checked = false;
            this.toolBtnCenter.Checked = false;
            this.toolBtnBoth.Checked = false;

        }

        private void toolBtnCenter_Click(object sender, EventArgs e)
        {

            this.toolBtnLeft.Checked = false;
            this.toolBtnRight.Checked = false;
            this.toolBtnCenter.Checked = true;
            this.toolBtnBoth.Checked = false;

        }

        private void toolBtnBoth_Click(object sender, EventArgs e)
        {

            this.toolBtnLeft.Checked = false;
            this.toolBtnRight.Checked = false;
            this.toolBtnCenter.Checked = false;
            this.toolBtnBoth.Checked = true;
        }
    }
}
