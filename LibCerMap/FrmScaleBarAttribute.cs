/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmScaleBarAttribute.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 图形比例尺属性窗口
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
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmScaleBarAttribute : OfficeForm
    {
        //传进来的样式
        IStyleGalleryItem StyleGalleryItem;
        //转成图形比例尺
        IScaleBar ScaleBar;
        //单位标志字体
        ITextSymbol UnitsLabelSymbols;
        //频率标志字体
        ITextSymbol FrequencyLabelSymbols;

        //设置调用构造器
        int FrmIndex = 0;

        
        public FrmScaleBarAttribute(IStyleGalleryItem pstyleGalleryItem)
        {
            InitializeComponent();
            this.EnableGlass = false;
            StyleGalleryItem = pstyleGalleryItem;
            ScaleBar = StyleGalleryItem.Item as IScaleBar;
            ScaleBar.Subdivisions = 2;
            ScaleBar.Divisions = 2;
            ScaleBar.UnitLabel = "Meters";

            FrmIndex = 1;
        }

        string[] SymbolStyle;
        IElement pElement;
        IGraphicsContainer pGraphicsContainer;
        IHookHelper m_hookHelper;
        IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();

        ISymbolBorder SymbolBorder;
        ISymbolBackground SymbolBackground;
        ISymbolShadow SymbolShadow;
        //记录当前比例尺属性，用于取消时返回
        //double pOriDivision = 0;
        //double pOriSubDivision = 0;
        //double pOriUnit = 0;
        //double pOriUnitPos = 0;
        //ITextSymbol pOriUnitSymbol = null;
        //double pOriUnitGap = 0;
        //double pOriFre = 0;
        //double pOriFrePos = 0;
        //ITextSymbol pOriFreSymbol = null;
        //double pOriFreGap = 0;
        //double pOriBarSize = 0;
        //IRgbColor pOriBarcolor = null;
        //属性调用构造器
        public FrmScaleBarAttribute(string[] symbolstyle,IElement element,IGraphicsContainer graphicscontainer,IHookHelper hookhelper)
        {
            InitializeComponent();
            SymbolStyle = symbolstyle;
            pElement = element;
            pGraphicsContainer = graphicscontainer;
            m_hookHelper = hookhelper;

            pMapSurroundFrame = (IMapSurroundFrame)pElement;
            ScaleBar = pMapSurroundFrame.MapSurround as IScaleBar;
            
            FrmIndex = 2;
        }

        private void FrmScaleBarAttribute_Load(object sender, EventArgs e)
        {
            if (FrmIndex == 2)
            {
                this.tabItemFrame.Visible = true;
            }
            else
            {
                this.tabItemFrame.Visible = false;
            }
            //根据加载进来的比例尺设计界面的初始值
            //格数默认值
            DivisionNum.Value = ScaleBar.Divisions;
            SubDivisionNum.Value = ScaleBar.Subdivisions;
            //单位默认值:单位、单位位置、标签、间隔
            //单位
            if (ScaleBar.UnitLabel == "厘米")
            {
                coboBoxUnits.SelectedIndex = 0;
            }
            else if (ScaleBar.UnitLabel == "度")
            {
                coboBoxUnits.SelectedIndex = 1;
            }
            else if (ScaleBar.UnitLabel == "分米")
            {
                coboBoxUnits.SelectedIndex = 2;
            }
            else if (ScaleBar.UnitLabel == "英尺")
            {
                coboBoxUnits.SelectedIndex = 3;
            }
            else if (ScaleBar.UnitLabel == "英寸")
            {
                coboBoxUnits.SelectedIndex = 4;
            }
            else if (ScaleBar.UnitLabel == "千米" || ScaleBar.UnitLabel == "Kilometers")
            {
                coboBoxUnits.SelectedIndex = 5;
            }
            else if (ScaleBar.UnitLabel == "米" || ScaleBar.UnitLabel == "Meters")
            {
                coboBoxUnits.SelectedIndex = 6;
            }
            else if (ScaleBar.UnitLabel == "英里")
            {
                coboBoxUnits.SelectedIndex = 7;
            }
            else if (ScaleBar.UnitLabel == "毫米")
            {
                coboBoxUnits.SelectedIndex = 8;
            }
            else if (ScaleBar.UnitLabel == "海里")
            {
                coboBoxUnits.SelectedIndex = 9;
            }
            else if (ScaleBar.UnitLabel == "点")
            {
                coboBoxUnits.SelectedIndex = 10;
            }
            else if (ScaleBar.UnitLabel == "UnitsLast")
            {
                coboBoxUnits.SelectedIndex = 11;
            }
            else if (ScaleBar.UnitLabel == "UnknownUnits")
            {
                coboBoxUnits.SelectedIndex = 12;
            }
            else if (ScaleBar.UnitLabel == "码")
            {
                coboBoxUnits.SelectedIndex = 13;
            }
            else
                coboBoxUnits.SelectedIndex = 12;
            //coboBoxUnits.SelectedIndex = 6;

            //位置
            if (ScaleBar.UnitLabelPosition == esriScaleBarPos.esriScaleBarAfterBar)
            {
                coboBoxPos.SelectedIndex = 0;
            }
            else if (ScaleBar.UnitLabelPosition == esriScaleBarPos.esriScaleBarAbove)
            {
                coboBoxPos.SelectedIndex = 1;
            }
            else if (ScaleBar.UnitLabelPosition == esriScaleBarPos.esriScaleBarAfterLabels)
            {
                coboBoxPos.SelectedIndex = 2;
            }
            else if (ScaleBar.UnitLabelPosition == esriScaleBarPos.esriScaleBarBeforeBar)
            {
                coboBoxPos.SelectedIndex = 3;
            }
            else if (ScaleBar.UnitLabelPosition == esriScaleBarPos.esriScaleBarBeforeLabels)
            {
                coboBoxPos.SelectedIndex = 4;
            }
            else if (ScaleBar.UnitLabelPosition == esriScaleBarPos.esriScaleBarBelow)
            {
                coboBoxPos.SelectedIndex = 5;
            }
           //间隔
            UnitsGap.Value = ScaleBar.UnitLabelGap;

            //频率、频率位置、频率间隔 默认值
            if (ScaleBar.LabelFrequency == esriScaleBarFrequency.esriScaleBarDivisionsAndFirstMidpoint)
            {
                cbBoxFrequency.SelectedIndex = 0;
            }
            else if (ScaleBar.LabelFrequency == esriScaleBarFrequency.esriScaleBarDivisions)
            {
                cbBoxFrequency.SelectedIndex = 1;
            }
            else if (ScaleBar.LabelFrequency == esriScaleBarFrequency.esriScaleBarDivisionsAndFirstSubdivisions)
            {
                cbBoxFrequency.SelectedIndex = 2;
            }
            else if (ScaleBar.LabelFrequency == esriScaleBarFrequency.esriScaleBarDivisionsAndSubdivisions)
            {
                cbBoxFrequency.SelectedIndex = 3;
            }
            else if (ScaleBar.LabelFrequency == esriScaleBarFrequency.esriScaleBarMajorDivisions)
            {
                cbBoxFrequency.SelectedIndex = 4;
            }
            else if (ScaleBar.LabelFrequency == esriScaleBarFrequency.esriScaleBarNone)
            {
                cbBoxFrequency.SelectedIndex = 5;
            }
            else if (ScaleBar.LabelFrequency == esriScaleBarFrequency.esriScaleBarOne)
            {
                cbBoxFrequency.SelectedIndex = 6;
            }

            //频率位置
            if (ScaleBar.LabelPosition == esriVertPosEnum.esriAbove)
            {
                cbBoxPostion.SelectedIndex = 0;
            }
            else if (ScaleBar.LabelPosition == esriVertPosEnum.esriTop)
            {
                cbBoxPostion.SelectedIndex = 1;
            }
            else if (ScaleBar.LabelPosition == esriVertPosEnum.esriOn)
            {
                cbBoxPostion.SelectedIndex = 2;
            }
            else if (ScaleBar.LabelPosition == esriVertPosEnum.esriBottom)
            {
                cbBoxPostion.SelectedIndex = 3;
            }
            else if (ScaleBar.LabelPosition == esriVertPosEnum.esriBelow)
            {
                cbBoxPostion.SelectedIndex = 4;
            }
            //频率间距
            FrequencyGap.Value = ScaleBar.LabelGap;

            //比例尺的宽度
            BarsSize.Value = ScaleBar.BarHeight;

            IColor pScaleBarColor = ScaleBar.BarColor;
            Color pColor = ColorTranslator.FromOle(pScaleBarColor.RGB);
            colorBar.SelectedColor = pColor;

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
        
        //比例尺单位
        private void coboBoxUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coboBoxUnits.SelectedItem.ToString() == "Centimeters")
            {
                txtUnitsLabel.Text = "厘米";
                //txtUnitsLabel.Text = "Centimeters";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "DecimalDegrees")
            {
                txtUnitsLabel.Text = "度";
                //txtUnitsLabel.Text = "DecimalDegrees";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Decimeters")
            {
                txtUnitsLabel.Text = "分米";
                //txtUnitsLabel.Text = "Decimeters";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Feet")
            {
                txtUnitsLabel.Text = "英尺";
                //txtUnitsLabel.Text = "Feet";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Inches")
            {
                txtUnitsLabel.Text = "英寸";
                //txtUnitsLabel.Text = "Inches";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Kilometers")
            {
                txtUnitsLabel.Text = "千米";
                //txtUnitsLabel.Text = "Kilometers";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Meters")
            {
                txtUnitsLabel.Text = "米";
                //txtUnitsLabel.Text = "Meters";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Miles")
            {
                txtUnitsLabel.Text = "英里";
                //txtUnitsLabel.Text = "Miles";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Millimeters")
            {
                txtUnitsLabel.Text = "毫米";
                //txtUnitsLabel.Text = "Millimeters";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "NauticalMiles")
            {
                txtUnitsLabel.Text = "海里";
                //txtUnitsLabel.Text = "NauticalMiles";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Points")
            {
                txtUnitsLabel.Text = "点";
                //txtUnitsLabel.Text = "Points";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "UnitsLast")
            {
                txtUnitsLabel.Text = "UnitsLast";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "UnknownUnits")
            {
                txtUnitsLabel.Text = "UnknownUnits";
            }
            else if (coboBoxUnits.SelectedItem.ToString() == "Yards")
            {
                txtUnitsLabel.Text = "码";
                //txtUnitsLabel.Text = "Yards";
            }
            else
                txtUnitsLabel.Text = string.Empty;
        }

        //设置单位标志字体样式
        private void btSymbolUnit_Click(object sender, EventArgs e)
        {
            FrmTextSymbol pFrmTextSymbol = new FrmTextSymbol();
            pFrmTextSymbol.ShowDialog();
            if (pFrmTextSymbol.DialogResult == DialogResult.OK)
            {
                UnitsLabelSymbols = pFrmTextSymbol.GetTextSymbol();
            }
            pFrmTextSymbol.Dispose();
        }

        //频率标志字体样式
        private void btSymbolFre_Click(object sender, EventArgs e)
        {
            FrmTextSymbol pFrmTextSymbol = new FrmTextSymbol();
            pFrmTextSymbol.ShowDialog();
            if (pFrmTextSymbol.DialogResult == DialogResult.OK)
            {
                FrequencyLabelSymbols = pFrmTextSymbol.GetTextSymbol();
            }
            pFrmTextSymbol.Dispose();
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
        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                CreateScaleBar();
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
      
        //设计比例尺属性
        private void CreateScaleBar()
        {
            //格数设计
            string pOriDiv = double.Parse(DivisionNum.Text).ToString();
            ScaleBar.Divisions = short.Parse(pOriDiv);
            string pOriSubDiv = double.Parse(SubDivisionNum.Text).ToString();
            ScaleBar.Subdivisions = short.Parse(pOriSubDiv);

            //单位设计
            if (coboBoxUnits.SelectedItem != null)
            {
                if (coboBoxUnits.SelectedItem.ToString() == "Centimeters")
                {
                    ScaleBar.Units = esriUnits.esriCentimeters;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "DecimalDegrees")
                {
                    ScaleBar.Units = esriUnits.esriDecimalDegrees;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Decimeters")
                {
                    ScaleBar.Units = esriUnits.esriDecimeters;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Feet")
                {
                    ScaleBar.Units = esriUnits.esriFeet;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Inches")
                {
                    ScaleBar.Units = esriUnits.esriInches;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Kilometers")
                {
                    ScaleBar.Units = esriUnits.esriKilometers;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Meters")
                {
                    ScaleBar.Units = esriUnits.esriMeters;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Miles")
                {
                    ScaleBar.Units = esriUnits.esriMiles;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Millimeters")
                {
                    ScaleBar.Units = esriUnits.esriMillimeters;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "NauticalMiles")
                {
                    ScaleBar.Units = esriUnits.esriNauticalMiles;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Points")
                {
                    ScaleBar.Units = esriUnits.esriPoints;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "UnitsLast")
                {
                    ScaleBar.Units = esriUnits.esriUnitsLast;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "UnknownUnits")
                {
                    ScaleBar.Units = esriUnits.esriUnknownUnits;
                }
                else if (coboBoxUnits.SelectedItem.ToString() == "Yards")
                {
                    ScaleBar.Units = esriUnits.esriYards;
                }

            }
            else
            {
                ScaleBar.Units = esriUnits.esriUnknownUnits;
            }
            
            //比例尺单位标签
            ScaleBar.UnitLabel = txtUnitsLabel.Text;

            //单位标签样式
            if (UnitsLabelSymbols != null)
            {
                ScaleBar.UnitLabelSymbol=UnitsLabelSymbols;
            }
            //单位标签位置
            if (coboBoxPos.SelectedItem.ToString() == "After Bar")
            {
                ScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarAfterBar;
            }
            else if (coboBoxPos.SelectedItem.ToString() == "Above Bar")
            {
                ScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarAbove;
            }
            else if (coboBoxPos.SelectedItem.ToString() == "After Labels")
            {
                ScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarAfterLabels;
            }
            else if (coboBoxPos.SelectedItem.ToString() == "Before Bar")
            {
                ScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarBeforeBar;
            }
            else if (coboBoxPos.SelectedItem.ToString() == "Before Labels")
            {
                ScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarBeforeLabels;
            }
            else if (coboBoxPos.SelectedItem.ToString() == "Below Bar")
            {
                ScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarBelow;
            }
            //单位标签间隔
            ScaleBar.UnitLabelGap = double.Parse(UnitsGap.Text);

            //频率设置
            if (cbBoxFrequency.SelectedItem.ToString() == "Divisions And First Mid point")
            {
                ScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndFirstMidpoint;
            }
            else if (cbBoxFrequency.SelectedItem.ToString() == "Divisions")
            {
                ScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisions;
            }
            else if (cbBoxFrequency.SelectedItem.ToString() == "Divisions And First Subdivisions")
            {
                ScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndFirstSubdivisions;
            }
            else if (cbBoxFrequency.SelectedItem.ToString() == "Divisions And Subdivisions")
            {
                ScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndSubdivisions;
            }
            else if (cbBoxFrequency.SelectedItem.ToString() == "ends(and zero)")
            {
                ScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarMajorDivisions;
            }
            else if (cbBoxFrequency.SelectedItem.ToString() == "no labels")
            {
                ScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarNone;
            }
            else if (cbBoxFrequency.SelectedItem.ToString() == "single labels")
            {
                ScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarOne;
            }
            //位置设计
            if (cbBoxPostion.SelectedItem.ToString() == "Above Bar")
            {
                ScaleBar.LabelPosition = esriVertPosEnum.esriAbove;
            }
            else if (cbBoxPostion.SelectedItem.ToString() == "Below bar")
            {
                ScaleBar.LabelPosition = esriVertPosEnum.esriBelow;
            }
            else if (cbBoxPostion.SelectedItem.ToString() == "Align to bottom of bar")
            {
                ScaleBar.LabelPosition = esriVertPosEnum.esriBottom;
            }
            else if (cbBoxPostion.SelectedItem.ToString() == "Center on bar")
            {
                ScaleBar.LabelPosition = esriVertPosEnum.esriOn;
            }
            else if (cbBoxPostion.SelectedItem.ToString() == "Align to top of bar")
            {
                ScaleBar.LabelPosition = esriVertPosEnum.esriTop;
            }
            //间隔设计
            ScaleBar.LabelGap = double.Parse(FrequencyGap.Text);
            
            //标注样式
            if (FrequencyLabelSymbols != null)
            {
                ScaleBar.LabelSymbol = FrequencyLabelSymbols;
            }

            //比例尺Bar大小、颜色设计
            //大小
            ScaleBar.BarHeight = double.Parse(BarsSize.Text);
            //颜色
            if (colorBar.SelectedColor != null)
            {
                ScaleBar.BarColor = ClsGDBDataCommon.ColorToIColor(colorBar.SelectedColor);
            }
            else
            {
                return;
            }

            //设置边框、阴影、背景的距离和圆角
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

        private void DivisionNum_ValueChanged(object sender, EventArgs e)
        {

        }
     
    }
}
