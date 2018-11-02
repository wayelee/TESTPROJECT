
/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmLegendStyle .cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 图例风格设计
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
    public partial class FrmLegendStyle : OfficeForm
    {
        IStyleGalleryItem pStyleGalleryItem;
        ILegendItem pLegendItem;
        ILegend pLegend;
        int lineindex = 0;
        public FrmLegendStyle(ILegendItem legenditem,ILegend legend,int index)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pLegendItem = legenditem;
            pLegend = legend;
            lineindex = index;
        }

        private void FrmLegendStyle_Load(object sender, EventArgs e)
        {
            //加载图例样式
            string EsriStylePathFram = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyCtlLegendItem.LoadStyleFile(EsriStylePathFram);
            axSymbologyCtlLegendItem.StyleClass = esriSymbologyStyleClass.esriStyleClassLegendItems;
        }

        //选择样式
        private void axSymbologyCtlLegendItem_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //择样式
            pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            ILegendItem pLegendTemp = pStyleGalleryItem.Item as ILegendItem;
            pLegendItem.ShowLayerName = pLegendTemp.ShowLayerName;
            pLegendItem.ShowLabels = pLegendTemp.ShowLabels;
            pLegendItem.ShowHeading = pLegendTemp.ShowHeading;
            pLegendItem.ShowDescriptions = pLegendTemp.ShowDescriptions;
            pStyleGalleryItem.Item = pLegendItem;
            //pLegendItem = pStyleGalleryItem.Item as ILegendItem;
            PreviewImage();
        }

        //预览样式
        private void PreviewImage()
        {
            //Get and set the style class 
            ISymbologyStyleClass symbologyStyleClass = axSymbologyCtlLegendItem.GetStyleClass(axSymbologyCtlLegendItem.StyleClass);

            //Preview an image of the symbol
            stdole.IPictureDisp pPicpture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, ImagePreview.Width, ImagePreview.Height);
            System.Drawing.Image pImage = System.Drawing.Image.FromHbitmap(new System.IntPtr(pPicpture.Handle));

            ImagePreview.Image = pImage;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            //pLegend.Item[lineindex].ShowLayerName = pLegendItem.ShowLayerName;
            //pLegend.Item[lineindex].ShowLabels = pLegendItem.ShowLabels;
            //pLegend.Item[lineindex].ShowHeading = pLegendItem.ShowHeading;
            //pLegend.Item[lineindex].ShowDescriptions = pLegendItem.ShowDescriptions;
            //pLegend.Refresh();
            pLegend.RemoveItem(lineindex);
            pLegend.InsertItem(lineindex, pLegendItem);
        }

        private void btShadowStyle_Click(object sender, EventArgs e)
        {

        }
    }
}
