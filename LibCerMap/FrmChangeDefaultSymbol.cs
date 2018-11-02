using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using DevComponents.DotNetBar;
 
 
namespace LibCerMap
{
    public partial class FrmChangeDefaultSymbol : OfficeForm
    {
        static public IGraphicProperties m_graphicProperties = new CommandsEnvironmentClass();
        static FrmChangeDefaultSymbol CFORM = null;
        public string[] SymbolStyle;

        public FrmChangeDefaultSymbol()
        {
            InitializeComponent();
            this.EnableGlass = false;
            
        }
            
       static public  FrmChangeDefaultSymbol GetChangeDefaultSymbol()
        {
          
            if (CFORM == null)
            {
                CFORM = new FrmChangeDefaultSymbol();
                return CFORM;
            }
           else
            {
                return CFORM;
            }

        }
        
       List<object> symbollist = new List<object>();
        private void ChangeDefaultSymbol_Load(object sender, EventArgs e)
        {
            FrmSymbol fsb = new FrmSymbol(SymbolStyle, (ISymbol)m_graphicProperties.MarkerSymbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
            btPoint.Image = fsb.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassMarkerSymbols,(ISymbol)m_graphicProperties.MarkerSymbol, btPoint.Width, btPoint.Height);

            fsb = new FrmSymbol(SymbolStyle,(ISymbol)m_graphicProperties.LineSymbol,esriSymbologyStyleClass.esriStyleClassLineSymbols);
            btPolyline.Image = fsb.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLineSymbols,(ISymbol)m_graphicProperties.LineSymbol, btPolyline.Width, btPolyline.Height);

            fsb = new FrmSymbol(SymbolStyle,(ISymbol)m_graphicProperties.FillSymbol, esriSymbologyStyleClass.esriStyleClassFillSymbols);
            btPolygon.Image = fsb.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassFillSymbols, (ISymbol)m_graphicProperties.FillSymbol, btPolygon.Width, btPolygon.Width);
            symbollist.Clear();
            symbollist.Add(m_graphicProperties.MarkerSymbol);
            symbollist.Add(m_graphicProperties.LineSymbol);
            symbollist.Add(m_graphicProperties.FillSymbol);
        }

        private void btPoint_Click(object sender, EventArgs e)
        {
            FrmSymbol frmSymbol = new FrmSymbol(SymbolStyle,(ISymbol)m_graphicProperties.MarkerSymbol,esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
            IStyleGalleryItem styleGalleryItem = null;
            frmSymbol.ShowDialog();
            if (frmSymbol.DialogResult == DialogResult.OK)
            {
                styleGalleryItem = frmSymbol.GetStyleGalleryItem();

                if (styleGalleryItem == null)
                {
                    return;
                }
                // m_graphicProperties.MarkerSymbol = (IMarkerSymbol)styleGalleryItem.Item; 
                symbollist[0] = styleGalleryItem.Item;
                btPoint.Image = frmSymbol.GetImageByGiveSymbolAfterSelectItem(btPoint.Width, btPoint.Height);
            }
        }

        private void btPolyline_Click(object sender, EventArgs e)
        {
            FrmSymbol frmSymbol = new FrmSymbol(SymbolStyle,(ISymbol)m_graphicProperties.LineSymbol,esriSymbologyStyleClass.esriStyleClassLineSymbols);
            IStyleGalleryItem styleGalleryItem = null;
            frmSymbol.ShowDialog();
            if (frmSymbol.DialogResult == DialogResult.OK)
            {
                styleGalleryItem = frmSymbol.GetStyleGalleryItem();
                if (styleGalleryItem == null)
                {
                    return;
                }
                // m_graphicProperties.LineSymbol = (ILineSymbol)styleGalleryItem.Item;
                symbollist[1] = styleGalleryItem.Item;
                btPolyline.Image = frmSymbol.GetImageByGiveSymbolAfterSelectItem(btPolyline.Width, btPolyline.Height);
            }
        }

        private void btPolygon_Click(object sender, EventArgs e)
        {
            FrmSymbol frmSymbol = new FrmSymbol(SymbolStyle,(ISymbol)m_graphicProperties.FillSymbol, esriSymbologyStyleClass.esriStyleClassFillSymbols);
            IStyleGalleryItem styleGalleryItem = null;
            frmSymbol.ShowDialog();
            if (frmSymbol.DialogResult == DialogResult.OK)
            {
                styleGalleryItem = frmSymbol.GetStyleGalleryItem();
                if (styleGalleryItem == null)
                {
                    return;
                }
                //  m_graphicProperties.FillSymbol = (IFillSymbol)styleGalleryItem.Item;
                symbollist[2] = styleGalleryItem.Item;
                btPolygon.Image = frmSymbol.GetImageByGiveSymbolAfterSelectItem(btPolygon.Width, btPolygon.Height);
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            symbollist.Clear();
            symbollist.Add(m_graphicProperties.MarkerSymbol);
            symbollist.Add(m_graphicProperties.LineSymbol);
            symbollist.Add(m_graphicProperties.FillSymbol);           
            this.Hide();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            m_graphicProperties.MarkerSymbol = (IMarkerSymbol)symbollist[0];
            m_graphicProperties.LineSymbol = (ILineSymbol)symbollist[1];
            m_graphicProperties.FillSymbol = (IFillSymbol)symbollist[2];
            DialogResult = DialogResult.OK;
        }
    }
}
