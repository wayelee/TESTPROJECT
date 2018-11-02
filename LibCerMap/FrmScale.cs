/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmScale.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 添加比例尺
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
    public partial class FrmScale : OfficeForm
    {
        private IStyleGalleryItem StyleGalleryItem;
        private IHookHelper m_hookHelper;
        esriSymbologyStyleClass StyleClass;
       
        public FrmScale(IHookHelper hookHelper, esriSymbologyStyleClass pStyleClass)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_hookHelper=hookHelper;
            StyleClass = pStyleClass;
          
        }

        private void FrmScaleBar_Load(object sender, EventArgs e)
        {
            try
            {
                //添加比例尺样式
                string EsriStylePath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
                axSymbologyControl.LoadStyleFile(EsriStylePath);

                axSymbologyControl.StyleClass = StyleClass;

                if (StyleClass == esriSymbologyStyleClass.esriStyleClassScaleBars)
                {
                    this.Text = "图形比例尺";
                    SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassScaleBars, 0);
                }
                else if (StyleClass == esriSymbologyStyleClass.esriStyleClassScaleTexts)
                {
                    this.Text = "文字比例尺";
                    SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassScaleTexts, 1);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //设置默认第一个样式为选择样式
        private void SetFeatureClassStyle(esriSymbologyStyleClass symbologyStyleClass,int index)
        {
            this.axSymbologyControl.StyleClass = symbologyStyleClass;
            ISymbologyStyleClass pSymbologyStyleClass = this.axSymbologyControl.GetStyleClass(symbologyStyleClass);
            pSymbologyStyleClass.SelectItem(index);
        }
        //样式选择
        private void axSymbologyControl_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //选择样式
            StyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;

            PreviewImage();
        }
       
        //预览
        private void PreviewImage()
        {
            try
            {
                //Get and set the style class 
                ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);

                //Preview an image of the symbol
                stdole.IPictureDisp pPicpture = symbologyStyleClass.PreviewItem(StyleGalleryItem, ImagePreview.Width, ImagePreview.Height);
                System.Drawing.Image pImage = System.Drawing.Image.FromHbitmap(new System.IntPtr(pPicpture.Handle));

                ImagePreview.Image = pImage;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                CreateScale();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        //取消
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //创建比例尺
        private void CreateScale()
        {
            if (StyleGalleryItem == null)
            {
                return;
            }

            IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView.GraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(m_hookHelper.ActiveView.FocusMap) as IMapFrame;
            if (pMapFrame == null)
            {
                return;
            }

            if (StyleGalleryItem.Item is IScaleBar)
            {
                IScaleBar pScaleBar = (IScaleBar)StyleGalleryItem.Item;
                pScaleBar.Refresh();
                IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
                pMapSurroundFrame.MapFrame = pMapFrame;

                pMapSurroundFrame.MapSurround = (IMapSurround)pScaleBar;
                IElement pElement = (IElement)pMapSurroundFrame;

                //添加和指北针一样大小的窗口
                double pScaleBarSize = 15;
                //显示结果大小
                IActiveView pActiveView = m_hookHelper.ActiveView;
                IPageLayout pPageLayout = (IPageLayout)pActiveView;
                IPage pPage = pPageLayout.Page;

                double pWidth = pPage.PrintableBounds.XMin + 2;
                double pHeigth = pPage.PrintableBounds.YMin + 2;

                IEnvelope pEnv = new EnvelopeClass();
                pEnv.PutCoords(pWidth, pHeigth, pWidth+10,pHeigth+1);
                pElement.Geometry = (IGeometry)pEnv;

                //Add the element to the graphics container
                pGraphicsContainer.AddElement((IElement)pMapSurroundFrame, 0);
                //Refresh
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pMapSurroundFrame, null);
            }
            else if (StyleGalleryItem.Item is IScaleText)
            {
                IScaleText pScaleText = (IScaleText)StyleGalleryItem.Item;
                pScaleText.Refresh();
                IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
                pMapSurroundFrame.MapFrame = pMapFrame;

                pMapSurroundFrame.MapSurround = (IMapSurround)pScaleText;
                IElement pElement = (IElement)pMapSurroundFrame;

                //显示结果大小
                IActiveView pActiveView = m_hookHelper.ActiveView;
                IPageLayout pPageLayout = (IPageLayout)pActiveView;
                IPage pPage = pPageLayout.Page;

                
                double pWidth = pPage .PrintableBounds.XMin+2;
                double pHeigth = pPage .PrintableBounds .YMin+2;

                IEnvelope pEnv = new EnvelopeClass();
                pEnv.PutCoords(pWidth, pHeigth, pWidth+10, pHeigth+1);
                pElement.Geometry = (IGeometry)pEnv;

                //Add the element to the graphics container
                pGraphicsContainer.AddElement((IElement)pMapSurroundFrame, 0);
                //Refresh
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pMapSurroundFrame, null);
            }
        }
     
        //图形比例尺属性设计
        private void btScalebarAttribute_Click(object sender, EventArgs e)
        {
            if (StyleGalleryItem == null)
            {
                MessageBox.Show("请选择图形比例尺样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (axSymbologyControl.StyleClass == esriSymbologyStyleClass.esriStyleClassScaleBars)
            {
                //进入图像比例尺属性设计窗口
                FrmScaleBarAttribute pFrmScaleBarAtt = new FrmScaleBarAttribute(StyleGalleryItem);
                pFrmScaleBarAtt.ShowDialog();
                if (pFrmScaleBarAtt.DialogResult == DialogResult.OK)
                {
                    IStyleGalleryItem pRetrunStyleItem = pFrmScaleBarAtt.GetStyleGalleryItem();
                    if (pRetrunStyleItem != null)
                    {
                        StyleGalleryItem = pRetrunStyleItem;
                    }
                }
                //pFrmScaleBarAtt.Dispose();
            }
            else if (axSymbologyControl.StyleClass ==esriSymbologyStyleClass.esriStyleClassScaleTexts)
            {
                //进入文字比例尺属性窗口
                FrmScaleTextAttribute pFrmScaleTextAtt = new FrmScaleTextAttribute(StyleGalleryItem);
                pFrmScaleTextAtt.ShowDialog();
                if (pFrmScaleTextAtt.DialogResult == DialogResult.OK)
                {
                    IStyleGalleryItem pRetrunStyleItem = pFrmScaleTextAtt.GetStyleGalleryItem();
                    if (pRetrunStyleItem != null)
                    {
                        StyleGalleryItem = pRetrunStyleItem;
                    }
                }
                //pFrmScaleTextAtt.Dispose();
            }

            PreviewImage();          
        }
    }
}
