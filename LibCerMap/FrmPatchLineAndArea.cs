/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmPatchLineAndArea .cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 图例图面设计
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
    public partial class FrmPatchLineAndArea : OfficeForm
    {
        IStyleGalleryItem pStyleGalleryItem;
        esriSymbologyStyleClass SymbolStyle;
        public FrmPatchLineAndArea(esriSymbologyStyleClass symbolstyle)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
        }

        private void FrmPatchLineAndArea_Load(object sender, EventArgs e)
        {
            string EsriStylePathFram = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyCtlFrame.LoadStyleFile(EsriStylePathFram);
            axSymbologyCtlFrame.StyleClass = SymbolStyle;
            if (SymbolStyle == esriSymbologyStyleClass.esriStyleClassLinePatches)
            {
                this.Text = "线";
            }
            else
            {
                this.Text = "面积";
            }
        }

        //样式选择
        private void axSymbologyCtlFrame_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //选择样式
            pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            PreviewImage();
        }
       
        //预览样式
        private void PreviewImage()
        {
            //Get and set the style class 
            ISymbologyStyleClass symbologyStyleClass = axSymbologyCtlFrame.GetStyleClass(axSymbologyCtlFrame.StyleClass);

            //Preview an image of the symbol
            stdole.IPictureDisp pPicpture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, ImagePreview.Width, ImagePreview.Height);
            System.Drawing.Image pImage = System.Drawing.Image.FromHbitmap(new System.IntPtr(pPicpture.Handle));

            ImagePreview.Image = pImage;
        }
        
        //选择样式前返回线图片
        public Image GetImageByGiveLineSymbolBeforeSelectItem(esriSymbologyStyleClass styleClass, ILinePatch symbol, int width, int height)
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
       
        //选择样式前返回面图片
        public Image GetImageByGiveAreaSymbolBeforeSelectItem(esriSymbologyStyleClass styleClass, IAreaPatch symbol, int width, int height)
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
       
        //选择样式后返回图片
        public Image GetImageByGiveSymbolAfterSelectItem(int width, int height)
        {
            ISymbologyStyleClass symbologyStyleClass = axSymbologyCtlFrame.GetStyleClass(axSymbologyCtlFrame.StyleClass);

            //Preview an image of the symbol
            stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, width, height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            return image;
        }

        //调用该窗口后返回线样式
        public ILinePatch GetLinePatch()
        {
            ILinePatch pLinePatch=pStyleGalleryItem .Item as ILinePatch ;
            
            return pLinePatch;
        }
      
        //调用该窗口后返回面样式
        public IAreaPatch GetAreaPatch()
        {
            IAreaPatch pAreaPatch = pStyleGalleryItem.Item as IAreaPatch;

            return pAreaPatch;
        }

        private void axSymbologyCtlFrame_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
