using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace LibCerMap
{
    
    public partial class FrmSelectColorRamp : OfficeForm
    {
        private IRasterRenderer m_pRasterRenderer = null;
        IStyleGalleryItem m_pStyleGalleryItem = null;
        ISymbologyStyleClass pSymbolClass = null;
        public FrmSelectColorRamp(IRasterRenderer  rasterRenderer,esriSymbologyStyleClass styleClass)
        {
            InitializeComponent();
            m_pRasterRenderer= rasterRenderer;
            InitialColorRamp(styleClass);
        }

        private void FrmSelectColorRamp_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;      
        }

        private void InitialColorRamp(esriSymbologyStyleClass styleClass)
        {
            if (m_pRasterRenderer is IRasterStretchColorRampRenderer)
            {
                //得到当前栅格拉伸渲染样式
                IRasterStretchColorRampRenderer rasterStrecthcRenderer = m_pRasterRenderer as IRasterStretchColorRampRenderer;
    
                //从文件打开渲染库
                string sInstall = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
                axSymbologyControl1.LoadStyleFile(sInstall);
                axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;
                pSymbolClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassColorRamps);

                IStyleGalleryItem pStyleGalleryItem = new ServerStyleGalleryItem();
                pStyleGalleryItem.Item = rasterStrecthcRenderer.ColorRamp;
                pSymbolClass.AddItem(pStyleGalleryItem, 0);
                
                //将渲染库中所有渲染方式添加到列表中
                for (int i = 0; i < pSymbolClass.ItemCount; i++)
                {
                    stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(pSymbolClass.GetItem(i), cmbColorRamp.Width, cmbColorRamp.Height);
                    Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                    cmbColorRamp.Items.Add(image);
                }
                if (pSymbolClass.ItemCount >0)
                {
                    pSymbolClass.SelectItem(0);
                }
                if (cmbColorRamp.Items.Count>0)
                {
                    cmbColorRamp.SelectedIndex = 0;
                }
                
            }
        }

        //调用该窗口后返回样式
        public IStyleGalleryItem GetStyleGalleryItem()
        {
            return m_pStyleGalleryItem;
        }

        private void cmbColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_pStyleGalleryItem = pSymbolClass.GetItem(cmbColorRamp.SelectedIndex);
        }
    }
}
