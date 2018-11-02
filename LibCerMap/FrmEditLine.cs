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
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmEditLine : OfficeForm
    {
        IStyleGalleryItem pStyleGalleryItem;
        IStyleGalleryClass pStyleGalleryClass = new LineSymbolStyleGalleryClass();
        List<Image> imagelist = new List<Image>();
     
        string[] SymbolStyle;

        //设计点状线时选择的点样式
        IMarkerSymbol pMarkerSymbol;
        //箭头样式
        IMarkerSymbol pArrowSymbol;

        public FrmEditLine(string [] symbolstyle,IStyleGalleryItem stylegalleryitem)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            pStyleGalleryItem = stylegalleryitem;
        }

        private void FrmEditLine_Load(object sender, EventArgs e)
        {
            IMultiLayerLineSymbol pMultiLayerLineSymbol =pStyleGalleryItem.Item as IMultiLayerLineSymbol;
            colorSimpleLine.SelectedColor = ClsGDBDataCommon.IColorToColor(pMultiLayerLineSymbol.get_Layer (0).Color);
            PreviewImage();
            addListBoxItem();
        }

        //预览
        private void PreviewImage()
        {
            Bitmap bmp = new Bitmap(ImagePreview.Width, ImagePreview.Height);
            Graphics pGraphic = Graphics.FromImage(bmp);
           
            tagRECT pTagrect = new tagRECT();
            pTagrect.right = bmp.Width;
            pTagrect.bottom = bmp.Height;

            IntPtr pIntptr = new IntPtr();
            pIntptr = pGraphic.GetHdc();
            pStyleGalleryClass.Preview(pStyleGalleryItem.Item, pIntptr.ToInt32(), ref pTagrect);
            pGraphic.ReleaseHdc(pIntptr);
            pGraphic.Dispose();
            ImagePreview.Image = bmp;
        }
 
        //添加GLISTBOX项
        private void addListBoxItem()
        {
            Bitmap bmp = new Bitmap(150, 20);
            Graphics pGraphic = Graphics.FromImage(bmp);

            tagRECT pTagrect = new tagRECT();
            pTagrect.right = bmp.Width;
            pTagrect.bottom = bmp.Height;

            IntPtr pIntptr = new IntPtr();
            pIntptr = pGraphic.GetHdc();
            pStyleGalleryClass.Preview(pStyleGalleryItem.Item, pIntptr.ToInt32(), ref pTagrect);
            pGraphic.ReleaseHdc(pIntptr);
            pGraphic.Dispose();

            GridPanel panel = supergrid.PrimaryGrid;
            GridRow row = new GridRow(bmp);
            panel.Rows.Add(row);

            imagelist.Add(bmp);
        }

        //添加样式按钮
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (cboBoxLineType.SelectedIndex == 0)//简单线
            {
                ISimpleLineSymbol pSimpleLineSymbol = CreateSimpleLineSymbol();
                pStyleGalleryItem.Item = pSimpleLineSymbol;
                addListBoxItem();
                PreviewImage();
            }
            else if (cboBoxLineType.SelectedIndex == 5) //点状线
            {
                IMarkerLineSymbol pMarkerLineSymbol = CreateMarkerLineSymbol();
                pStyleGalleryItem.Item = pMarkerLineSymbol;
                addListBoxItem();
                PreviewImage();
            }
        }
        //创建简单线
        private ISimpleLineSymbol CreateSimpleLineSymbol()
        {
            ISimpleLineSymbol pSimpleLS = new SimpleLineSymbolClass();

            //颜色
            if (colorSimpleLine.SelectedColor != Color.Empty)
            {
                pSimpleLS.Color = ClsGDBDataCommon.ColorToIColor(colorSimpleLine.SelectedColor);
            }
            else
            {
                pSimpleLS.Color = ClsGDBDataCommon.ColorToIColor(Color.Black);
                colorSimpleLine.SelectedColor = Color.Black;
            }

            //样式
            switch (cboBoxSimpleLineStyle.SelectedItem.ToString ())
            {
                case "Solid":
                    pSimpleLS.Style = esriSimpleLineStyle.esriSLSSolid;
                    break;
                case "Dashed":
                    pSimpleLS.Style = esriSimpleLineStyle.esriSLSDash;
                    break;
                case "Dotted":
                    pSimpleLS.Style = esriSimpleLineStyle.esriSLSDot;
                    break;
                case "Dash-Dot":
                    pSimpleLS.Style = esriSimpleLineStyle.esriSLSDashDot;
                    break;
                case "Dash-Dot-Dot":
                    pSimpleLS.Style = esriSimpleLineStyle.esriSLSDashDotDot;
                    break;
                case "null":
                    pSimpleLS.Style = esriSimpleLineStyle.esriSLSNull;
                    break;
            }

            //宽度
            pSimpleLS.Width = double.Parse(widthsimpleLine .Text);

            return pSimpleLS;
        }


        //点状线中点的形状
        private void btMarkerSymbol_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();

            FrmSymbol FrmMarker = new FrmSymbol(SymbolStyle,(ISymbol)m_GraphicProperties.MarkerSymbol,esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
            FrmMarker.ShowDialog();
            if (FrmMarker.DialogResult == DialogResult.OK)
            {
                pMarkerSymbol = FrmMarker.GetStyleGalleryItem() as IMarkerSymbol;
            }
        }
        //箭头样式选择
        private void btArrowSymbol_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();

            FrmSymbol FrmMarker = new FrmSymbol(SymbolStyle, (ISymbol)m_GraphicProperties.MarkerSymbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
            FrmMarker.ShowDialog();
            if (FrmMarker.DialogResult == DialogResult.OK)
            {
                pArrowSymbol = FrmMarker.GetStyleGalleryItem() as IMarkerSymbol;
            }
        }

        //创建点状线
        private IMarkerLineSymbol CreateMarkerLineSymbol()
        {
            IMarkerLineSymbol pMarkerLS = new MarkerLineSymbol();
            //样式
            if (pMarkerSymbol != null)
            {
                pMarkerLS.MarkerSymbol = pMarkerSymbol;
            }

            ICartographicLineSymbol pCartographicLS = pMarkerLS as ICartographicLineSymbol;

            //颜色
            if (colorCartographicLine.SelectedColor != Color.Empty)
            {
                pCartographicLS.Color = ClsGDBDataCommon.ColorToIColor(colorCartographicLine.SelectedColor);
            }
            else
            {
                pCartographicLS.Color = ClsGDBDataCommon.ColorToIColor(Color.Black);
                colorCartographicLine.SelectedColor = Color.Black;
            }
            //宽度
            pCartographicLS.Width = double.Parse(widthCartographicLine .Text);

            //端点样式
            if(rbCatButt.Checked==true )
            {
                pCartographicLS.Cap = esriLineCapStyle.esriLCSButt;
            }
            else if(rbCatRoundC.Checked==true)
            {
                pCartographicLS.Cap = esriLineCapStyle.esriLCSRound;
            }
            else if (rbCatSquare.Checked == true)
            {
                pCartographicLS.Cap = esriLineCapStyle.esriLCSSquare;
            }

            //节点样式
            if (rbCatMitre.Checked == true)
            {
                pCartographicLS.Join = esriLineJoinStyle.esriLJSMitre;
            }
            else if (rbCatRoundJ.Checked == true)
            {
                pCartographicLS.Join = esriLineJoinStyle.esriLJSRound;
            }
            else if (rbCatBevel.Checked == true)
            {
                pCartographicLS.Join = esriLineJoinStyle.esriLJSBevel;
            }
          
            //端点箭头样式
            ILineDecorationElement pLineDecorationElement = new SimpleLineDecorationElement();
            
            return pMarkerLS;
        }

    }
}
