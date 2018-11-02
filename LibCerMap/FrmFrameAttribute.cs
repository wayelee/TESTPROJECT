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
    public partial class FrmFrameAttribute : OfficeForm
    {
        IElement Element;
        IGraphicsContainer GraphicsContainer;
        IHookHelper HookHelper;
        IMapSurroundFrame MapSurroundFrame;

        //记录加载时边框、背景、阴影样式
        ISymbolBackground pSymbolBackground;
        ISymbolBorder pSymbolBorder;
        ISymbolShadow pSymbolShadow;

        //用于设计生成新的边框、背景、阴影样式
        ISymbolBackground SymbolBackground;
        ISymbolBorder SymbolBorder;
        ISymbolShadow SymbolShadow;
        string[] SymbolStyle;
        public FrmFrameAttribute(string [] symbolstyle,IElement element,IGraphicsContainer graphicsContainer, IHookHelper hookHelper)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            Element = element;
            GraphicsContainer = graphicsContainer;
            HookHelper = hookHelper;
        }

        private void FrmFrameAttribute_Load(object sender, EventArgs e)
        {
            IFrameElement FrameElement = Element as IFrameElement;
            IFrameProperties FrameProperties = FrameElement as IFrameProperties;
            //设置默认边框、背景、阴影

            if (FrameProperties.Border != null)
            {
                FrmFrameBorder Frm = new FrmFrameBorder(SymbolStyle,(ISymbolBorder)FrameProperties.Border);

                if (btBorder.Image != null)
                    btBorder.Image.Dispose();
                btBorder.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBorders, (ISymbolBorder)FrameProperties.Border, btBorder.Width - 14, btBorder.Height - 14);

                //记录
                pSymbolBorder = FrameProperties.Border as ISymbolBorder;
                //重新加载
                SymbolBorder = FrameProperties.Border as ISymbolBorder;
                this.txtBorderAngle.Text = SymbolBorder.CornerRounding.ToString();
                this.txtBorderGap.Text = SymbolBorder.Gap.ToString();

            }
            if (FrameProperties.Background != null)
            {
                FrmFrameBackground Frm = new FrmFrameBackground(SymbolStyle,(ISymbolBackground)FrameProperties.Background);
                if (btBackground.Image != null)
                    btBackground.Image.Dispose();
                btBackground.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBackgrounds, (ISymbolBackground)FrameProperties.Background, btBackground.Width - 14, btBackground.Height - 14);
               
                //记录
                pSymbolBackground = FrameProperties.Background as ISymbolBackground;
                //重新加载
                SymbolBackground = FrameProperties.Background as ISymbolBackground;
                this.txtBackgroundAngle.Text = SymbolBackground.CornerRounding.ToString();
                this.txtBackgroundGap.Text = SymbolBackground.Gap.ToString();
            }
            if (FrameProperties.Shadow != null)
            {
                FrmFrameShadow Frm = new FrmFrameShadow(SymbolStyle,(ISymbolShadow)FrameProperties.Shadow);
                if (btShadow.Image != null)
                    btShadow.Image.Dispose();
                btShadow.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassShadows, (ISymbolShadow)FrameProperties.Shadow, btShadow.Width - 14, btShadow.Height - 14);
               
                //记录
                pSymbolShadow = FrameProperties.Shadow as ISymbolShadow;
                //重新加载
                SymbolShadow = FrameProperties.Shadow as ISymbolShadow;
                this.txtShadowAngle.Text = SymbolShadow.CornerRounding.ToString();
                this.txtShadowX.Text = SymbolShadow.HorizontalSpacing.ToString();
                this.txtShadowY.Text = SymbolShadow.VerticalSpacing.ToString();
            }
        }

        //选择边框、背景、阴影按钮
        private void btBorder_Click(object sender, EventArgs e)
        {
            FrmFrameBorder Frm = new FrmFrameBorder(SymbolStyle,SymbolBorder);
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
            FrmFrameBackground Frm = new FrmFrameBackground(SymbolStyle,SymbolBackground);
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
            FrmFrameShadow Frm = new FrmFrameShadow(SymbolStyle,SymbolShadow);
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

        //撤销边框、背景、阴影按钮
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
            CreateSelectElementFrame();
            this.Close();
        }
        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            IFrameElement pFrameElement = Element as IFrameElement;
            IFrameProperties pFrameProperties = pFrameElement as IFrameProperties;
            //边框
            pFrameProperties.Border = pSymbolBorder;
            //背景
            pFrameProperties.Background = pSymbolBackground;
            //阴影
            pFrameProperties.Shadow = pSymbolShadow;

            this.Close();
        }

        //应用按钮
        private void btApply_Click(object sender, EventArgs e)
        {
            CreateSelectElementFrame();
            HookHelper.ActiveView.Refresh();
        }
        //创建要素的轮廓线
        private void CreateSelectElementFrame()
        {
            IFrameElement pFrameElement = Element as IFrameElement;
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
        }
    }
}
