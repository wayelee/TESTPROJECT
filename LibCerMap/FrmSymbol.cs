/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmSymbol.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 面、点、线设计窗口
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
    public partial class FrmSymbol : OfficeForm
    {
        private IStyleGalleryItem pStyleGalleryItem =null;// new ServerStyleGalleryItem();
        private esriSymbologyStyleClass pStyleClass;
        private ISymbol pSymbol;

        //加载样式
        string[] SymbolStyle;

        //构造器选择
        //int FrmIndex = 0;

        public FrmSymbol(string[] symbolstyle, ISymbol symbol, esriSymbologyStyleClass styleClass)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            pStyleClass = styleClass;
            pSymbol = symbol;
            
            //Add by Liuzhaoqin,2013.11.16
            axSymbologyControl.StyleClass = styleClass;
            ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(styleClass);
            if (symbol != null)
            {
                pStyleGalleryItem = new ServerStyleGalleryItem();
                pStyleGalleryItem.Item = symbol;
                pStyleGalleryItem.Name = "Current";

                symbologyStyleClass.AddItem(pStyleGalleryItem, 0);
                symbologyStyleClass.SelectItem(0);
            }
        }

        private void FrmSymbol_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < SymbolStyle.Length; i++)
            {
                if (SymbolStyle[i] != null)
                {
                    axSymbologyControl.LoadStyleFile(SymbolStyle[i]);
                }
            }

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

        private void axSymbologyControl_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
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

                PolygonSize.Text = pFillSymbol.Outline.Width.ToString();      
                colorPolygon.SelectedColor = ClsGDBDataCommon.IColorToColor(pFillSymbol.Color);
                colorOutLine.SelectedColor = ClsGDBDataCommon.IColorToColor(pFillSymbol.Outline.Color);
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

        //选择样式前返回图片
        public Image GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass styleClass, ISymbol symbol, int width, int height)
        {
            try
            {
                axSymbologyControl.StyleClass = styleClass;
                ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(styleClass);

                IStyleGalleryItem styleGalleryItem = new ServerStyleGalleryItem();

                styleGalleryItem.Item = symbol;
                //// styleGalleryItem.Name = "DefaultSymbol";
                symbologyStyleClass.AddItem(styleGalleryItem, 0);
                symbologyStyleClass.SelectItem(0);
                stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem(pStyleGalleryItem, width, height);
                System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
                return image;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return null;
            }            
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

        //调用该窗口后返回样式
        public IStyleGalleryItem GetStyleGalleryItem()
        {
            return pStyleGalleryItem;
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
            if (!(pMarkerSymbol is IPictureMarkerSymbol))
            {
                pMarkerSymbol.Color = ClsGDBDataCommon.ColorToIColor(colorPoint.SelectedColor);     
            }
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
            pMarkerSymbol.Size = PointSize.Value;
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
            pMarkerSymbol.Angle = PointAngle .Value;
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
            if(pLineSymbol ==null)
            {
                return;
             }

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

                                        pMSymbol.Color = ClsGDBDataCommon.ColorToIColor(colorLine.SelectedColor);
                                        pnewele.AddPosition(pSLDecEle.get_Position(i));
                                        pnewele.MarkerSymbol = pMSymbol;
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
            LineSize.Value = LineSize.Value;
            pLineSymbol.Width = LineSize.Value;
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
            if (!(pFillSymbol is IPictureFillSymbol))
            {
                pFillSymbol.Color = ClsGDBDataCommon.ColorToIColor(colorPolygon.SelectedColor);
            }
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
            pLineSymbol.Width = PolygonSize.Value;
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
            if (!(pFillSymbol is IPictureFillSymbol))
            {
                ILineSymbol pLineSymbol = pFillSymbol.Outline;
                pLineSymbol.Color = ClsGDBDataCommon.ColorToIColor(colorOutLine.SelectedColor);
                pFillSymbol.Outline = pLineSymbol;
            }
            PreviewImage();          
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
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

        //编辑点
        private void btEditPoint_Click(object sender, EventArgs e)
        {
            ISymbologyStyleClass psymbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);
            FrmSymbolPoint frmpoint = new FrmSymbolPoint(psymbologyStyleClass);
            frmpoint.StartPosition = FormStartPosition.CenterScreen;
            frmpoint.ShowDialog();
        }

        //编辑线
        private void btEditLine_Click(object sender, EventArgs e)
        {
            FrmEditLine frm = new FrmEditLine(SymbolStyle, pStyleGalleryItem);
            frm.ShowDialog();          
        }

        private void btEditPolygon_Click(object sender, EventArgs e)
        {
            ISymbologyStyleClass psymbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);
            FrmSymbolFill frmfill = new FrmSymbolFill(psymbologyStyleClass, SymbolStyle);
            frmfill.StartPosition = FormStartPosition.CenterScreen;
            frmfill.ShowDialog();
        }
        

      
    }
}
