/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmNorthArrwoAttribute .cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 指北针属性设计
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
using ESRI.ArcGIS.esriSystem;
using System.Drawing.Text;
using stdole;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmNorthArrwoAttribute : OfficeForm
    {
         IGraphicsContainer m_pGraphicsContainer;
        IHookHelper m_hookHelper;
 
        //Add by Liuzhaoqin
        //指北针实际上由一个MarkerNorthArrow和MapSurroundFrame组成，内部做一个临时变量存储原有的对象
        INorthArrow m_pNorthArrowOrg =null;
        IMapSurroundFrame m_pMapFrameOrg = null;
        INorthArrow m_pNorthArrow = null;
        IMapSurroundFrame m_pMapFrame = null;
        IElement m_pElement = null;
        
        string[] m_strSymbolStyle;

        public FrmNorthArrwoAttribute(string[] symbolstyle,IElement element,IGraphicsContainer GraphicsContainer, IHookHelper hookHelper)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pElement = element;

            //SymbolStyle = symbolstyle;
            //pElement = Element;
            m_pGraphicsContainer = GraphicsContainer;
            m_hookHelper = hookHelper;
            //pOrielement = Element;

            m_pNorthArrowOrg = new MarkerNorthArrowClass();
            m_pMapFrameOrg = new MapSurroundFrameClass();
            m_pMapFrame = new MapSurroundFrameClass();
            //得到当前对象
            m_pMapFrame = m_pElement as IMapSurroundFrame;
             m_pNorthArrow = m_pMapFrame.MapSurround as INorthArrow;            
            //拷贝个对象
            CopyMapSurroundFrame(m_pMapFrame, m_pMapFrameOrg);
            CopyNorthArrow(m_pNorthArrow, m_pNorthArrowOrg);
        }

        //更新界面值，false-界面值更新至对象，true-对象值更新至界面
        private void UpdateUI(bool bValue)
        {
            
            if (bValue)
            {
                #region 更新界面              
                //批北针属性
                this.NorthArrowAngle.Value = m_pNorthArrow.CalibrationAngle;
                this.NorthArrowSize.Value = m_pNorthArrow.Size;
                IColor pNorthArrowColor = m_pNorthArrow.Color;
                Color pColor = ColorTranslator.FromOle(pNorthArrowColor.RGB);
                colorNorthArrow.SelectedColor = pColor;

                //Frame属性
                IFrameProperties pFrameProperties = m_pMapFrame as IFrameProperties;
                if (pFrameProperties.Border != null)
                {
                    FrmFrameBorder Frm = new FrmFrameBorder(m_strSymbolStyle, (ISymbolBorder)pFrameProperties.Border);
                    btBorder.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBorders, (ISymbolBorder)pFrameProperties.Border, btBorder.Width - 14, btBorder.Height - 14);
                    //pOriNAborder = pFrameProperties.Border as ISymbolBorder;
                    //重新加载
                    ISymbolBorder SymbolBorder = pFrameProperties.Border as ISymbolBorder;
                    this.txtBorderAngle.Text = SymbolBorder.CornerRounding.ToString();
                    this.txtBorderGap.Text = SymbolBorder.Gap.ToString();

                }

                if (pFrameProperties.Background != null)
                {
                    FrmFrameBackground Frm = new FrmFrameBackground(m_strSymbolStyle, (ISymbolBackground)pFrameProperties.Background);
                    btBackground.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBackgrounds, (ISymbolBackground)pFrameProperties.Background, btBackground.Width - 14, btBackground.Height - 14);
                    //pOriNAbackground = pFrameProperties.Background as ISymbolBackground;
                    //重新加载
                    ISymbolBackground SymbolBackground = pFrameProperties.Background as ISymbolBackground;
                    this.txtBackgroundAngle.Text = SymbolBackground.CornerRounding.ToString();
                    this.txtBackgroundGap.Text = SymbolBackground.Gap.ToString();
                }
                if (pFrameProperties.Shadow != null)
                {
                    FrmFrameShadow Frm = new FrmFrameShadow(m_strSymbolStyle, (ISymbolShadow)pFrameProperties.Shadow);
                    btShadow.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassShadows, (ISymbolShadow)pFrameProperties.Shadow, btShadow.Width - 14, btShadow.Height - 14);
                    //pOriNAshadow = pFrameProperties.Shadow as ISymbolShadow;

                    //重新加载
                    ISymbolShadow SymbolShadow = pFrameProperties.Shadow as ISymbolShadow;
                    this.txtShadowAngle.Text = SymbolShadow.CornerRounding.ToString();
                    this.txtShadowX.Text = SymbolShadow.HorizontalSpacing.ToString();
                    this.txtShadowY.Text = SymbolShadow.VerticalSpacing.ToString();
                }
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                #endregion
            }
            else
            {
                #region 更新对象属性值

                #endregion
            }
        }

        private void FrmNorthArrwoAttribute_Load(object sender, EventArgs e)
        {
            UpdateUI(true);
#region 废弃

            ////得到当前指北针
            ////pMapSurroundFrame = new MapSurroundFrameClass();
            //pMapSurroundFrame = (IMapSurroundFrame)pElement;
            //INorthArrow  pNorthArrow = pMapSurroundFrame.MapSurround as INorthArrow;
            //NorthArrow = pNorthArrow;
           
            ////设置界面属性值
            //this.NorthArrowAngle.Value = NorthArrow.CalibrationAngle;        
            //this.NorthArrowSize.Value = NorthArrow.Size;
            //IColor pNorthArrowColor = NorthArrow.Color;
            //Color pColor = ColorTranslator.FromOle(pNorthArrowColor.RGB);
            //colorNorthArrow.SelectedColor = pColor;

            
            //pOriNA = pNorthArrow;
            //pOriNAangle = NorthArrow.CalibrationAngle;
            //pOriNAsize = NorthArrow.Size;
            //pOriNAcolor = NorthArrow.Color as IRgbColor ;

            ////设置默认边框、背景、阴影
            //IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            //if (pFrameProperties.Border != null)
            //{
            //    FrmFrameBorder Frm = new FrmFrameBorder(SymbolStyle,(ISymbolBorder)pFrameProperties.Border);
            //    btBorder.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBorders, (ISymbolBorder)pFrameProperties.Border, btBorder.Width - 14, btBorder.Height - 14);
            //    pOriNAborder = pFrameProperties.Border as ISymbolBorder;
            //    //重新加载
            //    SymbolBorder = pFrameProperties.Border as ISymbolBorder;
            //    this.txtBorderAngle.Text = SymbolBorder.CornerRounding.ToString();
            //    this.txtBorderGap.Text = SymbolBorder.Gap.ToString();
                
            //}

            //if (pFrameProperties.Background  != null)
            //{
            //    FrmFrameBackground Frm = new FrmFrameBackground(SymbolStyle, (ISymbolBackground)pFrameProperties.Background);
            //    btBackground.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBackgrounds, (ISymbolBackground)pFrameProperties.Background, btBackground.Width - 14, btBackground.Height - 14);
            //    pOriNAbackground = pFrameProperties.Background as ISymbolBackground;
            //    //重新加载
            //    SymbolBackground = pFrameProperties.Background as ISymbolBackground;
            //    this.txtBackgroundAngle.Text = SymbolBackground.CornerRounding.ToString();
            //    this.txtBackgroundGap.Text = SymbolBackground.Gap.ToString();
            //}
            //if (pFrameProperties.Shadow != null)
            //{
            //    FrmFrameShadow Frm = new FrmFrameShadow(SymbolStyle, (ISymbolShadow)pFrameProperties.Shadow);
            //    btShadow.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassShadows, (ISymbolShadow)pFrameProperties.Shadow, btShadow.Width - 14, btShadow.Height - 14);
            //    pOriNAshadow = pFrameProperties.Shadow as ISymbolShadow;

            //    //重新加载
            //    SymbolShadow = pFrameProperties.Shadow as ISymbolShadow;
            //    this.txtShadowAngle.Text = SymbolShadow.CornerRounding.ToString();
            //    this.txtShadowX.Text = SymbolShadow.HorizontalSpacing.ToString();
            //    this.txtShadowY.Text = SymbolShadow.VerticalSpacing.ToString();
            //}
#endregion
        }
      
        //复制指北针
        private void CopyNorthArrow(INorthArrow srcArrow,INorthArrow destArrow)
        {
            IClone pSrc = srcArrow as IClone;
            IClone pDest = pSrc.Clone();
            destArrow = pDest as INorthArrow;
        }
        //复制MapFrame
        private void CopyMapSurroundFrame(IMapSurroundFrame srcArrow, IMapSurroundFrame destArrow)
        {
            IClone pSrc = srcArrow as IClone;
            IClone pDest = pSrc.Clone();
            destArrow = pDest as IMapSurroundFrame;
        }

        //角度
        private void NorthArrowAngle_ValueChanged(object sender, EventArgs e)
        {
            if (m_pNorthArrow != null && !string.IsNullOrEmpty(NorthArrowAngle.Text))
            {
                m_pNorthArrow.CalibrationAngle = double.Parse(this.NorthArrowAngle.Text); //这个角度
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            
        }
     
        //颜色
        private void colorNorthArrow_SelectedColorChanged(object sender, EventArgs e)
        {
            if (m_pNorthArrow != null)
            {
                m_pNorthArrow.Color = ClsGDBDataCommon.ColorToIColor(colorNorthArrow.SelectedColor);
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }
     
        //大小
        private void NorthArrowSize_ValueChanged(object sender, EventArgs e)
        {
            if (m_pNorthArrow != null && !string.IsNullOrEmpty(NorthArrowSize.Text))
            {
                m_pNorthArrow.Size = double.Parse(this.NorthArrowSize.Text);
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
         }
      
        //边框
        private void btBorder_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = m_pMapFrame as IFrameProperties;
            //pFrameProperties.Border = SymbolBorder;
            FrmFrameBorder Frm = new FrmFrameBorder(m_strSymbolStyle, (ISymbolBorder)pFrameProperties.Border);
            
            if (Frm.ShowDialog() == DialogResult.OK)
            {
                ISymbolBorder SymbolBorder = Frm.GetSymbolBorder();
                if (SymbolBorder != null)
                {
                    btBorder.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btBorder.Width - 14, btBorder.Height - 14);
                    pFrameProperties.Border = SymbolBorder;
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }
     
        //背景
        private void btBackground_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = m_pMapFrame as IFrameProperties;
            FrmFrameBackground Frm = new FrmFrameBackground(m_strSymbolStyle, (ISymbolBackground)pFrameProperties.Background);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                ISymbolBackground SymbolBackground = Frm.GetSymbolBackground();
                if (SymbolBackground != null)
                {
                    btBackground.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btBackground.Width - 14, btBackground.Height - 14);
                    pFrameProperties.Background = SymbolBackground;
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }
      
        //阴影
        private void btShadow_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = m_pMapFrame as IFrameProperties;
            FrmFrameShadow Frm = new FrmFrameShadow(m_strSymbolStyle, (ISymbolShadow)pFrameProperties.Shadow);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                ISymbolShadow SymbolShadow = Frm.GetSymbolShadow();
                if (SymbolShadow != null)
                {
                    btShadow.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btShadow.Width - 14, btShadow.Height - 14);
                    pFrameProperties.Shadow = SymbolShadow;
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }

        //撤销边框、背景、阴影
        private void btReturnBorder_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = m_pMapFrame as IFrameProperties;
            pFrameProperties.Border = null;
            btBorder.Image = null;
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void btReturnBackground_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = m_pMapFrame as IFrameProperties;
            pFrameProperties.Background = null;
            btBackground.Image = null;
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void btReturnShadow_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = m_pMapFrame as IFrameProperties;
            pFrameProperties.Shadow = null;
            btShadow.Image = null;
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
       
        //调用指北针样式
        private void btNAStyle_Click(object sender, EventArgs e)
        {
              //记录原位置 
            IEnvelope pEnv =new EnvelopeClass();
            pEnv = m_pElement.Geometry.Envelope;
           
             //删除原有对象    
            m_pGraphicsContainer.DeleteElement(m_pElement);
            //Create the form with the SymbologyControl
            FrmNorthArrow symbolForm = new FrmNorthArrow(m_pNorthArrow);
            //Get the IStyleGalleryItem
            if (symbolForm.ShowDialog() != DialogResult.OK) return;

            IStyleGalleryItem styleGalleryItem = symbolForm.GetItem();
            //Release the form
            symbolForm.Dispose();
            if (styleGalleryItem == null) return;

            IMapFrame pMapFrame = m_pGraphicsContainer.FindFrame(m_hookHelper.ActiveView.FocusMap) as IMapFrame;
            if (pMapFrame == null) return;

            //Create a map surround frame
            IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
            //Set its map frame and map surround
            pMapSurroundFrame.MapFrame = pMapFrame;
            pMapSurroundFrame.MapSurround = (IMapSurround)styleGalleryItem.Item;

            m_pElement = (IElement)pMapSurroundFrame;
            m_pElement.Geometry = (IGeometry)pEnv;

            //Add the element to the graphics container
            m_pGraphicsContainer.AddElement(m_pElement, 0);
            m_pMapFrame = m_pElement as IMapSurroundFrame;
            m_pNorthArrow = m_pMapFrame.MapSurround as INorthArrow;
            UpdateUI(true);
            //Refresh
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pMapSurroundFrame, null);
        }


        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
             this.Close();
        }
       
        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            IFrameProperties pFrameProperties = m_pMapFrame as IFrameProperties;
            IFrameProperties pFramePropertiesOrg = m_pMapFrameOrg as IFrameProperties;
            pFrameProperties.Border = pFramePropertiesOrg.Border;
            pFrameProperties.Background = pFramePropertiesOrg.Background;
            pFrameProperties.Shadow = pFramePropertiesOrg.Shadow;

            m_pNorthArrow.Size = m_pNorthArrowOrg.Size;
            m_pNorthArrow.Color = m_pNorthArrowOrg.Color;
            m_pNorthArrow.CalibrationAngle = m_pNorthArrowOrg.CalibrationAngle;

          
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            this.Close();
        }
    }
}
