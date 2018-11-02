/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmNeatLine.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 添加轮廓线
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
    public partial class FrmNeatLine : OfficeForm
    {
        IHookHelper m_hookHelper;
        string[] SymbolStyle;
        IActiveView pActiveView;

        ISymbolBackground SymbolBackground;
        ISymbolBorder SymbolBorder;
        ISymbolShadow SymbolShadow;

        
        public FrmNeatLine(string [] symbolstyle,IHookHelper hookHelper)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            m_hookHelper = hookHelper;
            pActiveView = m_hookHelper.ActiveView  as IActiveView;
        }


        private void FrmNeatLine_Load(object sender, EventArgs e)
        {
            IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView.GraphicsContainer;
            IGraphicsContainerSelect pGraphicsContainerSelect = pGraphicsContainer as IGraphicsContainerSelect;
            int SelectElementCount = pGraphicsContainerSelect.ElementSelectionCount;

            if (SelectElementCount != 0)
            {
                this.cBoxSelectElementFrame.Checked = true;
            }
            else
            {
                this.cBoxPageFrame.Checked = true;
            }

            //FrmFrameBorder Frmborder = new FrmFrameBorder(SymbolBorder);
            //if (SymbolBorder != null)
            //{
            //    btBorder.Image = Frmborder.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBorders,SymbolBorder, btBorder.Width - 14, btBorder.Height - 14);
            //}
            //FrmFrameBackground Frmbackground = new FrmFrameBackground(SymbolBackground);
            //if (SymbolBackground != null)
            //{
            //    btBackground.Image = Frmbackground.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass .esriStyleClassBackgrounds,SymbolBackground ,btBackground .Width -14,btBackground .Height -14);
            //}
            //FrmFrameShadow Frmshadow = new FrmFrameShadow(SymbolShadow);
            //if (SymbolShadow != null)
            //{
            //    btShadow.Image = Frmshadow.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass .esriStyleClassShadows,SymbolShadow ,btShadow .Width -14,btShadow.Height -14);
            //}
        }

        //边框、阴影、背景
        private void btBorder_Click(object sender, EventArgs e)
        {
            try
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
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btBackground_Click(object sender, EventArgs e)
        {
            try
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
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btShadow_Click(object sender, EventArgs e)
        {
            try
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
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //撤销边框、阴影、背景
        private void btReturnBorder_Click(object sender, EventArgs e)
        {
            SymbolBorder = null;
            this.btBorder.Image = null;
        }

        private void btReturnBackground_Click(object sender, EventArgs e)
        {
            SymbolBackground = null;
            this.btBackground.Image = null;
        }

        private void btReturnShadow_Click(object sender, EventArgs e)
        {

            SymbolShadow = null;
            this.btShadow.Image = null;
        }

        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cBoxSelectElementFrame.Checked == true)
                {
                    CreateSelectElementFrame();
                }
                else if (this.cBoxPageFrame.Checked == true)
                {
                    CreatePageFrame();
                }
                //else if (this.cBoxAllElementFrame.Checked == true)
                //{
                //    CreateAllElementFrame();
                //}

                m_hookHelper.ActiveView.Refresh();

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

        //创建选择要素的轮廓线
        private void CreateSelectElementFrame()
        {
            IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView.GraphicsContainer;
            IGraphicsContainerSelect pGraphicsContainerSelect = pGraphicsContainer as IGraphicsContainerSelect;
            int SelectElementCount = pGraphicsContainerSelect.ElementSelectionCount;
            IEnumElement pEnumElement = pGraphicsContainerSelect.SelectedElements;
            pEnumElement.Reset();
            IElement pElement = pEnumElement.Next();

            for (int i = 0; i < SelectElementCount; i++)
            {
                IElementProperties pElementProperties = pElement as IElementProperties;
                IFrameElement pFrameElement = pElement as IFrameElement;
                IFrameProperties pFrameProperties = pFrameElement as IFrameProperties;
                //边框的间距和圆角
                if (SymbolBorder != null)
                {
                    if (this.txtBorderGap.Text != null)
                    {
                        SymbolBorder.Gap = double.Parse(this.txtBorderGap.Text);
                    }
                    if (this.txtBorderAngle.Text != null)
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
                //边框
                pFrameProperties.Border = SymbolBorder;
                //背景
                pFrameProperties.Background = SymbolBackground;
                //阴影
                pFrameProperties.Shadow = SymbolShadow;

                pElement = pEnumElement.Next();
            }
        }

        //创建所有要素的轮廓线
        private void CreateAllElementFrame()
        {
            IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView.GraphicsContainer;
         
            IElement pElement = pGraphicsContainer.Next();
            
            //IEnumElement pp = 

            IMapSurroundFrame pMapsurroundFrame = new MapSurroundFrameClass();
            pMapsurroundFrame.MapSurround = pGraphicsContainer as IMapSurround;

            IFrameProperties pFrameProperties = pMapsurroundFrame as IFrameProperties;

            //边框的间距和圆角
            if (SymbolBorder != null)
            {
                if (this.txtBorderGap.Text != null)
                {
                    SymbolBorder.Gap = double.Parse(this.txtBorderGap.Text);
                }
                if (this.txtBorderAngle.Text != null)
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
            //边框
            pFrameProperties.Border = SymbolBorder;

            //背景
            pFrameProperties.Background = SymbolBackground;
            //阴影
            pFrameProperties.Shadow = SymbolShadow;
        }

        //创建页边距内的轮廓线
        private void CreatePageFrame()
        {
            IPageLayout pPageLayout = pActiveView as IPageLayout;
            IPage pPage = pPageLayout.Page as IPage;

            //边框的间距和圆角
            if (SymbolBorder != null)
            {
                if (this.txtBorderGap.Text != null)
                {
                    SymbolBorder.Gap = double.Parse(this.txtBorderGap.Text);
                }
                if (this.txtBorderAngle.Text != null)
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
            IFrameProperties pFrameProperties = pPage as IFrameProperties;
            //边框
            pFrameProperties.Border = SymbolBorder;
            //背景
            pFrameProperties.Background = SymbolBackground;
            //阴影
            pFrameProperties.Shadow = SymbolShadow;
        }
    }
}
