/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmScaleTextAttribute.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 文字比例尺属性窗口
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
using System.Drawing.Text;
using stdole;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmScaleTextAttribute : OfficeForm
    {
        IStyleGalleryItem StyleGalleryItem;
        IScaleText2 ScaleText;
        IStyleGalleryItem CancelStyleGItem;
        string SeparatorType;
        ITextSymbol TextSymbol;
       
        //设置调用构造器
        int FrmIndex = 0;
        public FrmScaleTextAttribute(IStyleGalleryItem pstyleGalleryItem)
        {
            InitializeComponent();
            this.EnableGlass = false;
            StyleGalleryItem = pstyleGalleryItem;
            CancelStyleGItem = pstyleGalleryItem;
            ScaleText = StyleGalleryItem.Item as IScaleText2;
            SeparatorType = ScaleText.Separator;
            FrmIndex = 1;
        }

        IElement pElement;
        string[] SymbolStyle;
        IGraphicsContainer pGraphicsContainer;
        IHookHelper m_hookHelper;
        IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
       
        ISymbolBorder SymbolBorder;
        ISymbolBackground SymbolBackground;
        ISymbolShadow SymbolShadow;
        //属性调用构造器
        public FrmScaleTextAttribute(string[] symbolstyle,IElement element, IGraphicsContainer graphicscontainer, IHookHelper hookhelper)
        {
            InitializeComponent();
            SymbolStyle = symbolstyle;
            pElement = element;
            pGraphicsContainer = graphicscontainer;
            m_hookHelper = hookhelper;

            pMapSurroundFrame = (IMapSurroundFrame)pElement;

            ScaleText = pMapSurroundFrame.MapSurround as IScaleText2;

            FrmIndex = 2;
        }

        private void FrmScaleTextAttribute_Load(object sender, EventArgs e)
        {
            if (FrmIndex == 2)
            {
                this.tabItemFrame.Visible = true;
            }
            else
            {
                this.tabItemFrame.Visible = false;
            }
            //将选择的文字比例尺的一般属性赋值给属性表
            if(ScaleText.Separator == " = ")
            {
                ckBoxAsolute.Checked = false;
                ckBoxRelative.Checked = true;
                txtSeparator.Text = " = ";
            }
            else if (ScaleText.Separator == ":")
            {
                ckBoxRelative.Checked = false;
                ckBoxAsolute.Checked = true;
                txtSeparator.Text = ":";
                cmbBoxMapUnits.Enabled = false;
                cmbBoxPageUnits.Enabled = false;
                txtPageLabel.Enabled = false;
                txtMapLabel.Enabled = false;
            }
            else if (ScaleText.Separator == " equals ")
            {
                ckBoxAsolute.Checked = false;
                ckBoxRelative.Checked = true;
                txtSeparator.Text = " equals ";
            }
            //布局
            if (ScaleText.PageUnitLabel == "in" || ScaleText.PageUnitLabel == "inch")
            {
                cmbBoxPageUnits.SelectedIndex = 1;
            }
            else if (ScaleText.PageUnitLabel == "cm" || ScaleText.PageUnitLabel == "centimeter" || ScaleText.PageUnitLabel == "page unit")
            {
                cmbBoxPageUnits.SelectedIndex = 0;
            }
           //地图
            if (ScaleText.MapUnitLabel == "feet" || ScaleText.MapUnitLabel == "ft")
            {
                cmbBoxMapUnits.SelectedIndex = 3;
            }
            else if (ScaleText.MapUnitLabel == "miles")
            {
                cmbBoxMapUnits.SelectedIndex = 7;
            }
            else if (ScaleText.MapUnitLabel == "km")
            {
                cmbBoxMapUnits.SelectedIndex = 5;
            }
            else if (ScaleText.MapUnitLabel == "meters")
            {
                cmbBoxMapUnits.SelectedIndex = 6;
            }
            else if (ScaleText.MapUnitLabel == "yards")
            {
                cmbBoxMapUnits.SelectedIndex = 12;
            }
            else if (ScaleText.MapUnitLabel == "map units")
            {
                cmbBoxMapUnits.SelectedIndex = 11;
            }

            //字体名称加载
            //加载字体名称
            InstalledFontCollection pFontCollection = new InstalledFontCollection();
            FontFamily[] pFontFamily = pFontCollection.Families;
            for (int i = 0; i < pFontFamily.Length; i++)
            {
                string pFontName = pFontFamily[i].Name;
                this.cboBoxFontName.Items.Add(pFontName);
            }
            this.cboBoxFontName.Text = "宋体";
            //加载字体大小
            for (int i = 3; i <= 100; i++)
            {
                this.cboBoxFontSize.Items.Add(i.ToString());
            }
            this.cboBoxFontSize.Text = "15";
            //颜色
            FontColor.SelectedColor = Color.Black;
            //设置默认边框、背景、阴影
            IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            if (pFrameProperties.Border != null)
            {
                FrmFrameBorder Frm = new FrmFrameBorder(SymbolStyle,(ISymbolBorder)pFrameProperties.Border);
                btBorder.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBorders, (ISymbolBorder)pFrameProperties.Border, btBorder.Width - 14, btBorder.Height - 14);
                //重新加载
                SymbolBorder = pFrameProperties.Border as ISymbolBorder;
                this.txtBorderAngle.Text = SymbolBorder.CornerRounding.ToString();
                this.txtBorderGap.Text = SymbolBorder.Gap.ToString();

            }
            if (pFrameProperties.Background != null)
            {
                FrmFrameBackground Frm = new FrmFrameBackground(SymbolStyle, (ISymbolBackground)pFrameProperties.Background);
                btBackground.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBackgrounds, (ISymbolBackground)pFrameProperties.Background, btBackground.Width - 14, btBackground.Height - 14);
                //重新加载
                SymbolBackground = pFrameProperties.Background as ISymbolBackground;
                this.txtBackgroundAngle.Text = SymbolBackground.CornerRounding.ToString();
                this.txtBackgroundGap.Text = SymbolBackground.Gap.ToString();
            }
            if (pFrameProperties.Shadow != null)
            {
                FrmFrameShadow Frm = new FrmFrameShadow(SymbolStyle, (ISymbolShadow)pFrameProperties.Shadow);
                btShadow.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassShadows, (ISymbolShadow)pFrameProperties.Shadow, btShadow.Width - 14, btShadow.Height - 14);
                //重新加载
                SymbolShadow = pFrameProperties.Shadow as ISymbolShadow;
                this.txtShadowAngle.Text = SymbolShadow.CornerRounding.ToString();
                this.txtShadowX.Text = SymbolShadow.HorizontalSpacing.ToString();
                this.txtShadowY.Text = SymbolShadow.VerticalSpacing.ToString();
            }
        }

        //文字比例尺样式：绝对类型
        private void ckBoxAsolute_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBoxAsolute.Checked)
            {
                ckBoxRelative.Checked = false;
                txtSeparator.Text = ":";
                cmbBoxMapUnits.Enabled = false;
                cmbBoxPageUnits.Enabled = false;
                txtPageLabel.Enabled = false;
                txtMapLabel.Enabled = false;
            }
        }

        //文字比例尺样式：相对类型
        private void ckBoxRelative_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBoxRelative.Checked)
            {
                ckBoxAsolute.Checked = false;
                if (SeparatorType == ":" || SeparatorType == " equals ")
                {
                    txtSeparator.Text = " equals ";
                }
                else 
                {
                    txtSeparator.Text = " = ";
                }
                cmbBoxMapUnits.Enabled = true;
                cmbBoxPageUnits.Enabled = true;
                txtPageLabel.Enabled = true;
                txtMapLabel.Enabled = true;

            }
        }

        //布局单位
        private void cmbBoxPageUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxPageUnits.SelectedItem.ToString() == "Centimeters")
            {
                txtPageLabel.Text= "Centimeter";
            }
            else if (cmbBoxPageUnits.SelectedItem.ToString() == "Inches")
            {
                txtPageLabel.Text = "Inch";
            }
            else if (cmbBoxPageUnits.SelectedItem.ToString() == "Points")
            {
                txtPageLabel.Text = "Point";
            }
        }

        //地图单位
        private void cmbBoxMapUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxMapUnits.SelectedItem.ToString() == "Centimeters")
            {
                txtMapLabel.Text = "Centimeters";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "DecimalDegrees")
            {
                txtMapLabel.Text = "Decimal Degrees";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Decimeters")
            {
                txtMapLabel.Text = "Decimeters";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Feet")
            {
                txtMapLabel.Text = "Feet";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Inches")
            {
                txtMapLabel.Text = "Inches";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Kilometers")
            {
                txtMapLabel.Text = "Kilometers";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Meters")
            {
                txtMapLabel.Text = "Meters";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Miles")
            {
                txtMapLabel.Text = "Miles";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Millimeters")
            {
                txtMapLabel.Text = "Millimeters";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "NauticalMiles")
            {
                txtMapLabel.Text = "Nautical Miles";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Points")
            {
                txtMapLabel.Text = "Points";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "UnknownUnits")
            {
                txtMapLabel.Text = "Unknown Units";
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Yards")
            {
                txtMapLabel.Text = "Yards";
            }   
        }
       
        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                CreateScaleText();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //属性设计完成后返回结果
        public IStyleGalleryItem GetStyleGalleryItem()
        {
            return StyleGalleryItem;
        }

        //字体样式按钮
        private void btTextSymbol_Click(object sender, EventArgs e)
        {
            FrmTextSymbol pFrmTextSymbol = new FrmTextSymbol();
            pFrmTextSymbol.ShowDialog();
            if (pFrmTextSymbol.DialogResult == DialogResult.OK)
            {
                TextSymbol = pFrmTextSymbol.GetTextSymbol();
            }
            pFrmTextSymbol.Dispose();
        }

        //创建文字比例尺
        private void CreateScaleText()
        {
            //文字比例尺样式：绝对类型和相对类型
            if (ckBoxAsolute.Checked)
            {
                ScaleText.Style = esriScaleTextStyleEnum.esriScaleTextAbsolute;
                ScaleText.Separator = txtSeparator.Text;
            }
            else if (ckBoxRelative.Checked)
            {
                ScaleText.Style = esriScaleTextStyleEnum.esriScaleTextRelative;
                ScaleText.Separator = txtSeparator.Text;
            }
            //布局单位
            if (cmbBoxPageUnits.SelectedItem.ToString() == "Centimeters")
            {
                ScaleText.PageUnits = esriUnits.esriCentimeters;
                ScaleText.PageUnitLabel = txtPageLabel.Text;
            }
            else if (cmbBoxPageUnits.SelectedItem.ToString() == "Inches")
            {
                ScaleText.PageUnits = esriUnits.esriInches;
                ScaleText.PageUnitLabel = txtPageLabel.Text;
            }
            else if (cmbBoxPageUnits.SelectedItem.ToString() == "Points")
            {
                ScaleText.PageUnits = esriUnits.esriPoints;
                ScaleText.PageUnitLabel = txtPageLabel.Text;
            }
            //地图单位
            if (cmbBoxMapUnits.SelectedItem.ToString() == "Centimeters")
            {
                ScaleText.MapUnits = esriUnits.esriCentimeters;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Decimal Degrees")
            {
                ScaleText.MapUnits = esriUnits.esriDecimalDegrees;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Decimeters")
            {
                ScaleText.MapUnits = esriUnits.esriDecimeters;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Feet")
            {
                ScaleText.MapUnits = esriUnits.esriFeet;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Inches")
            {
                ScaleText.MapUnits = esriUnits.esriInches;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Kilometers")
            {
                ScaleText.MapUnits = esriUnits.esriKilometers;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Meters")
            {
                ScaleText.MapUnits = esriUnits.esriMeters;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Miles")
            {
                ScaleText.MapUnits = esriUnits.esriMiles;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Millimeters")
            {
                ScaleText.MapUnits = esriUnits.esriMillimeters;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Nautical Miles")
            {
                ScaleText.MapUnits = esriUnits.esriNauticalMiles;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Points")
            {
                ScaleText.MapUnits = esriUnits.esriPoints;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Unknown Units")
            {
                ScaleText.MapUnits = esriUnits.esriUnknownUnits;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }
            else if (cmbBoxMapUnits.SelectedItem.ToString() == "Yards")
            {
                ScaleText.MapUnits = esriUnits.esriYards;
                ScaleText.MapUnitLabel = txtMapLabel.Text;
            }

            if (TextSymbol == null)
            {
                //字体样式
                ITextSymbol pTextSymbol = new TextSymbolClass();
                IFontDisp pFont = new StdFontClass() as IFontDisp;

                pFont.Name = this.cboBoxFontName.Text;
                pFont.Size = decimal.Parse(this.cboBoxFontSize.Text);

                if (FontColor.SelectedColor != null)
                {
                    pTextSymbol.Color = ClsGDBDataCommon.ColorToIColor(FontColor.SelectedColor);
                }
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
                ScaleText.Symbol = pTextSymbol;
            }
            else if (TextSymbol != null)
            {
                ScaleText.Symbol = TextSymbol;
            }

            //设置边框、阴影、背景的距离和圆角
            //边框、背景、阴影距离、角度
            if (SymbolBorder != null)
            {
                if (this.txtBorderGap.Text != null)
                {
                    SymbolBorder.Gap = double.Parse(this.txtBorderGap.Text);
                }
                if (txtBorderAngle.Text != null)
                {
                    SymbolBorder.CornerRounding = short.Parse(this.txtBorderAngle.Text);
                }
            }
            if (SymbolBackground != null)
            {
                if (txtBackgroundGap.Text != null)
                {
                    SymbolBackground.Gap = double.Parse(this.txtBackgroundGap.Text);
                }
                if (txtBackgroundAngle.Text != null)
                {
                    SymbolBackground.CornerRounding = short.Parse(this.txtBackgroundAngle.Text);
                }
            }
            if (SymbolShadow != null)
            {
                if (txtShadowX.Text != null)
                {
                    SymbolShadow.HorizontalSpacing = double.Parse(this.txtShadowX.Text);
                }
                if (txtShadowY.Text != null)
                {
                    SymbolShadow.VerticalSpacing = double.Parse(this.txtShadowY.Text);
                }
                if (txtShadowAngle.Text != null)
                {
                    SymbolShadow.CornerRounding = short.Parse(this.txtShadowAngle.Text);
                }
            }
            //添加边框、背景、阴影
            IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            if (SymbolBorder != null)
            {
                pFrameProperties.Border = SymbolBorder;
            }
            if (SymbolBackground != null)
            {
                pFrameProperties.Background = SymbolBackground;
            }
            if (SymbolShadow != null)
            {
                pFrameProperties.Shadow = SymbolShadow;
            }
           
        }

        //边框、背景、阴影
        private void btBorder_Click(object sender, EventArgs e)
        {
            FrmFrameBorder Frm = new FrmFrameBorder(SymbolStyle, SymbolBorder);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                SymbolBorder = Frm.GetSymbolBorder();
                if (SymbolBorder != null)
                {
                    btBorder.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btBorder.Width - 14, btBorder.Height - 14);
                }
            }
        }

        private void btBackground_Click(object sender, EventArgs e)
        {
            FrmFrameBackground Frm = new FrmFrameBackground(SymbolStyle, SymbolBackground);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                SymbolBackground = Frm.GetSymbolBackground();
                if (SymbolBackground != null)
                {
                    btBackground.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btBackground.Width - 14, btBackground.Height - 14);
                }
            }
        }

        private void btShadow_Click(object sender, EventArgs e)
        {
            FrmFrameShadow Frm = new FrmFrameShadow(SymbolStyle, SymbolShadow);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                SymbolShadow = Frm.GetSymbolShadow();
                if (SymbolShadow != null)
                {
                    btShadow.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btShadow.Width - 14, btShadow.Height - 14);
                }
            }
        }

        //撤销边框、背景、阴影
        private void btReturnBorder_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            pFrameProperties.Border = null;
            btBorder.Image = null;
            m_hookHelper.ActiveView.Refresh();
        }

        private void btReturnBackground_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            pFrameProperties.Background = null;
            btBackground.Image = null;
            m_hookHelper.ActiveView.Refresh();
        }

        private void btReturnShadow_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            pFrameProperties.Shadow = null;
            btShadow.Image = null;
            m_hookHelper.ActiveView.Refresh();
        }

    }
}
