
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
    public partial class FrmSymbolFillSL : OfficeForm
    {
        private IStyleGalleryItem pStyleGalleryItem;
        esriSymbologyStyleClass pStyleClass;
        FrmSymbolFill frmfill = null;
        string strstyle = "";//用于指示目前进行哪个操作
        //加载样式
        string[] SymbolStyle;
        public FrmSymbolFillSL(esriSymbologyStyleClass StyleClass, FrmSymbolFill frm, string str, string[] SymbolS)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pStyleClass = StyleClass;
            frmfill = frm;
            strstyle = str;
            SymbolStyle = SymbolS;
        }

        private void FrmSymbol_Load(object sender, EventArgs e)
        {
    
            //几个设计小界面显示位置
            this.gPanelPoint.Location = new System.Drawing.Point(256, 151);
            this.gPanelLine.Location= this.gPanelPoint.Location;
            this.gPanelPolygon.Location = this.gPanelPoint.Location;

            //Get the ArcGIS install location
            string EsriStylePath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyControl.LoadStyleFile(EsriStylePath);

            axSymbologyControl.StyleClass = pStyleClass;

            //判读那个设计小界面显示
            if (pStyleClass == esriSymbologyStyleClass.esriStyleClassMarkerSymbols)
            {
                gPanelPoint.Visible = true;
                gPanelLine.Visible = false;
                gPanelPolygon.Visible = false;
            }
            else if (pStyleClass == esriSymbologyStyleClass.esriStyleClassLineSymbols)
            {
                gPanelPoint.Visible = false;
                gPanelLine.Visible = true;
                gPanelPolygon.Visible = false;
            }
            else if (pStyleClass == esriSymbologyStyleClass.esriStyleClassFillSymbols)
            {
                gPanelPoint.Visible = false;
                gPanelLine.Visible = false;
                gPanelPolygon.Visible = true;
            }
        }

        //样式选择
        private void axSymbologyControl_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //选择样式
            pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            //将选择的样式与设计的样式大小、颜色等属性结合
            if (axSymbologyControl.StyleClass == esriSymbologyStyleClass.esriStyleClassMarkerSymbols)
            {
                IMarkerSymbol pMarkerSymbol = pStyleGalleryItem.Item as IMarkerSymbol;
                double pMarkerSize = pMarkerSymbol.Size;
                PointSize.Text = pMarkerSize.ToString();
                double pMarkerAngle = pMarkerSymbol.Angle;
                PointAngle.Text = pMarkerAngle.ToString();
                colorPoint.SelectedColor = ClsGDBDataCommon.IColorToColor(pMarkerSymbol.Color);
            }
            else if (axSymbologyControl.StyleClass == esriSymbologyStyleClass.esriStyleClassLineSymbols)
            {
                ILineSymbol pLineSymbol = pStyleGalleryItem.Item as ILineSymbol;
                double pLineWidth = pLineSymbol.Width;
                LineSize.Text = pLineWidth.ToString();
                colorLine.SelectedColor = ClsGDBDataCommon.IColorToColor(pLineSymbol.Color);
            }
            else if (axSymbologyControl.StyleClass == esriSymbologyStyleClass.esriStyleClassFillSymbols)
            {
                IFillSymbol pFillSymbol = pStyleGalleryItem.Item as IFillSymbol;
                ILineSymbol pLineSymbol = pFillSymbol.Outline;
                double pOutLineWidth = pLineSymbol.Width;
                PolygonSize.Text = pOutLineWidth.ToString();
            }

            PreviewImage();
        }
        
        //预览
        private void PreviewImage()
        {
            //Get and set the style class 
            ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);

            //Preview an image of the symbol
            stdole.IPictureDisp pPicpture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, ImagePreview.Width, ImagePreview.Height);
            System.Drawing.Image pImage = System.Drawing.Image.FromHbitmap(new System.IntPtr(pPicpture.Handle));

            ImagePreview.Image = pImage;
        }
        
        //选择样式后返回图片
        public Image GetImageByGiveSymbolAfterSelectItem(int width, int height)
        {
            ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);

            //Preview an image of the symbol
            stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, width, width);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            return image;
        }

        //点 设计
        private void colorPoint_SelectedColorChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择点样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            IMarkerSymbol pMarkerSymbol= pStyleGalleryItem.Item as IMarkerSymbol;
            pMarkerSymbol.Color = ClsGDBDataCommon.ColorToIColor(colorPoint.SelectedColor);
            PreviewImage();
        }

        private void PointSize_ValueChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择点样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            IMarkerSymbol pMarkerSymbol = pStyleGalleryItem.Item as IMarkerSymbol;
            pMarkerSymbol.Size = double.Parse(PointSize.Text);
            PreviewImage();
        }

        private void PointAngle_ValueChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择点样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            IMarkerSymbol pMarkerSymbol = pStyleGalleryItem.Item as IMarkerSymbol;
            pMarkerSymbol.Angle = double.Parse(PointAngle .Text);
            PreviewImage();
        }

        //线 设计
        private void colorLine_SelectedColorChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择线样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ILineSymbol pLineSymbol = pStyleGalleryItem.Item as ILineSymbol;
            pLineSymbol.Color = ClsGDBDataCommon.ColorToIColor(colorLine.SelectedColor);
            //新添加，用于更改线修饰颜色
            if (pLineSymbol is IMultiLayerLineSymbol)
            {
                IMultiLayerLineSymbol pMultiLyerLine = pLineSymbol as IMultiLayerLineSymbol;
                int nLCount = pMultiLyerLine.LayerCount;
                for (int j = 0; j < nLCount; j++)
                {
                    ILineSymbol Psymbol = pMultiLyerLine.get_Layer(j);
                    ILineProperties pLineProp = Psymbol as ILineProperties;

                    List<ILineDecorationElement> Linedeclist = new List<ILineDecorationElement>();
                    if (pLineProp != null)
                    {
                        ILineDecoration pLineDec = pLineProp.LineDecoration;
                        if (pLineDec != null)
                        {
                            int ncount = pLineDec.ElementCount;
                            for (int i = 0; i < ncount; i++)
                            {
                                ILineDecorationElement pLineDecEle = pLineDec.get_Element(i);
                                Linedeclist.Add(pLineDecEle);

                            }
                            if (Linedeclist.Count > 0)
                            {
                                for (int i = 0; i < ncount; i++)
                                {
                                    ISimpleLineDecorationElement pSLDecEle = Linedeclist[i] as ISimpleLineDecorationElement;
                                    ISimpleLineDecorationElement pnewele = new SimpleLineDecorationElementClass();
                                    if (pSLDecEle != null)
                                    {
                                        IMarkerSymbol pMSymbol = pSLDecEle.MarkerSymbol;

                                        pMSymbol.Color = ClsGDBDataCommon.ColorToIColor(colorLine.SelectedColor); ;
                                        pnewele.AddPosition(pSLDecEle.get_Position(i));
                                        pnewele.MarkerSymbol = pMSymbol;

                                        //IMultiLayerMarkerSymbol pa = pMSymbol as IMultiLayerMarkerSymbol;
                                        //IMultiLayerMarkerSymbol pMLSymbol = new MultiLayerMarkerSymbolClass();
                                        //pMLSymbol = pa;
                                        //pMLSymbol.Color = pRgbColor;
                                        //pLineDec.DeleteElement(i);

                                        //pLineDec.AddElement(pSlinede as ILineDecorationElement);
                                        pLineDec.DeleteElement(i);
                                        pLineDec.AddElement(pnewele);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            PreviewImage();
        }

        private void LineSize_ValueChanged(object sender, EventArgs e)
        {

            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择线样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ILineSymbol pLineSymbol = pStyleGalleryItem.Item as ILineSymbol;
            LineSize.Text = Math.Truncate(double.Parse(LineSize.Text)).ToString ();
            pLineSymbol.Width = Math.Truncate(double.Parse(LineSize.Text));
            PreviewImage();
        }
       
        //面 设计
        private void colorPolygon_SelectedColorChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择面样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            IFillSymbol pFillSymbol = pStyleGalleryItem.Item as IFillSymbol;

            pFillSymbol.Color = ClsGDBDataCommon.ColorToIColor(colorPolygon.SelectedColor); ;
            PreviewImage();
        }

        private void PolygonSize_ValueChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择面样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            IFillSymbol pFillSymbol = pStyleGalleryItem.Item as IFillSymbol;
            ILineSymbol pLineSymbol = pFillSymbol.Outline;
            pLineSymbol.Width = double.Parse(PolygonSize.Text);
            pFillSymbol.Outline = pLineSymbol;
            PreviewImage();
        }

        private void colorOutLine_SelectedColorChanged(object sender, EventArgs e)
        {
            if (pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择面样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            IFillSymbol pFillSymbol = pStyleGalleryItem.Item as IFillSymbol;
            ILineSymbol pLineSymbol = pFillSymbol.Outline;
            pLineSymbol .Color = ClsGDBDataCommon.ColorToIColor(colorOutLine.SelectedColor);
            pFillSymbol.Outline = pLineSymbol;
            PreviewImage();          
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (axSymbologyControl.StyleClass == esriSymbologyStyleClass.esriStyleClassMarkerSymbols)
            {
                if (strstyle == "pointsymbol")
                {
                    frmfill.pMarkersymbol = (IMarkerSymbol)pStyleGalleryItem.Item;
                }              
            }
            else if (axSymbologyControl.StyleClass == esriSymbologyStyleClass.esriStyleClassLineSymbols)
            {
                if (strstyle == "linesymbol")
                {
                    frmfill.pLineSymbol = (ILineSymbol)pStyleGalleryItem.Item;
                }
                else if (strstyle == "outlinesymbol")
                {
                    frmfill.pOutLineSymbol = (ILineSymbol)pStyleGalleryItem.Item;
                }
                else if (strstyle == "outmarkerlinesymbol")
                {
                    frmfill.pMarkOutLineSymbol = (ILineSymbol)pStyleGalleryItem.Item;
                }
            }
           
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btneditpoint_Click(object sender, EventArgs e)
        {
            ISymbologyStyleClass psymbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);
            FrmSymbolPoint frmpoint = new FrmSymbolPoint(psymbologyStyleClass);
            frmpoint.StartPosition = FormStartPosition.CenterScreen;
            frmpoint.ShowDialog();
        }

        private void btneditline_Click(object sender, EventArgs e)
        {
            ISymbologyStyleClass psymbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);
            FrmSymbolLine frmline = new FrmSymbolLine(psymbologyStyleClass);
            frmline.StartPosition = FormStartPosition.CenterScreen;
            frmline.ShowDialog();
        }

        private void btAddStyle_Click(object sender, EventArgs e)
        {
            FrmSymbolStyle frm = new FrmSymbolStyle(SymbolStyle);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                axSymbologyControl.Clear();
                for (int i = 0; i < frm.SelectStylePath.Length; i++)
                {
                    if (frm.SelectStylePath[i] != null)
                    {
                        axSymbologyControl.LoadStyleFile(frm.SelectStylePath[i]);
                    }
                }
                axSymbologyControl.Refresh();
            }
        }

  
    }
}
