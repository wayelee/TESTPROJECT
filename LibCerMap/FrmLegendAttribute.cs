/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmLegendAttribute.cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 图例属性窗口
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
    public partial class FrmLegendAttribute : OfficeForm
    {
        string[] SymbolStyle;
        IElement pElement;
        IGraphicsContainer pGraphicsContainer;
        IHookHelper m_hookHelper;
        IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
        ILegend Legend;
        IActiveView pActiView;
        IMap pMap;
        List<int> pSelectedidx = null;

        //标题样式
        ITextSymbol pTitleSymbol;
        //图面样式
        ILinePatch pLinePatch;
        IAreaPatch pAreaPatch;
        //图例中文本样式
        ITextSymbol pWholeItemSymbol; //所有文本的样式
        ITextSymbol pLayerNameSymbol; //图层名样式
        ITextSymbol pHeadingSymbol;//标题样式
        ITextSymbol pLabelSymbol;//标注样式
        ITextSymbol pDescriptionSymbol;//描述样式
        //图例样式
        
        //边框、背景、阴影
        ISymbolBorder pSymbolBorder;
        ISymbolBackground pSymbolBackground;
        ISymbolShadow pSymbolShadow;

        //记录legend中所有的元素
        List<ILegendItem> legendlist = new List<ILegendItem>();
        int lineindex = 0;//记录目前被选中的legend的item的索引

        public FrmLegendAttribute(string[] symbolstyle,IElement element, IGraphicsContainer graphicscontainer, IHookHelper hookhelper)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            pElement = element;
            pGraphicsContainer = graphicscontainer;
            m_hookHelper = hookhelper;
            pActiView = m_hookHelper.PageLayout as IActiveView;
            pMap = pActiView.FocusMap;
            pSelectedidx = new List<int>();

            pMapSurroundFrame = (IMapSurroundFrame)pElement;
            Legend = pMapSurroundFrame.MapSurround as ILegend;
            pMap = Legend.Map;
        }

        private void FrmLegendAttribute_Load(object sender, EventArgs e)
        {
            //图例界面要素信息加载
         
            ILegendFormat pLegendFormat = Legend.Format;
            //标题
            this.txtLegendTitle.Text = Legend.Title;
            if (pLegendFormat.ShowTitle == true)
            {
                this.cBoxShowTitle.Checked = true;
                this.txtLegendTitle.Enabled = true;
            }
            else
            {
                this.cBoxShowTitle.Checked = false;
                this.txtLegendTitle.Enabled = false;
            }
            pTitleSymbol = pLegendFormat.TitleSymbol;
            //图面
            this.txtPatchWith.Text = pLegendFormat.DefaultPatchWidth.ToString ();
            this.txtPatchHeight.Text = pLegendFormat.DefaultPatchHeight.ToString();

            if (pLegendFormat.DefaultLinePatch != null)
            {
                FrmPatchLineAndArea frmPatchLine = new FrmPatchLineAndArea(esriSymbologyStyleClass.esriStyleClassLinePatches);
                this.btLineShape.Image = frmPatchLine.GetImageByGiveLineSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLinePatches, (ILinePatch)pLegendFormat.DefaultLinePatch, btLineShape.Width, btLineShape.Height);
            }
            if (pLegendFormat.DefaultAreaPatch != null)
            {
                FrmPatchLineAndArea frmPatchArea = new FrmPatchLineAndArea(esriSymbologyStyleClass.esriStyleClassAreaPatches);
                this.btAreaShape.Image = frmPatchArea.GetImageByGiveAreaSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassAreaPatches, (IAreaPatch)pLegendFormat.DefaultAreaPatch, btAreaShape.Width, btAreaShape.Height);
            }

            //间隔
            this.txtTitleAndLegend.Text = pLegendFormat.TitleGap.ToString ();
            this.txtLegendItems.Text = pLegendFormat.VerticalItemGap.ToString ();
            this.txtColumns.Text = pLegendFormat.HorizontalItemGap.ToString ();
            this.txtLabelsAndDescription.Text = pLegendFormat.TextGap.ToString ();
            this.txtHeadings.Text = pLegendFormat.HeadingGap.ToString();
            this.txtPatchesV.Text = pLegendFormat.VerticalPatchGap.ToString();
            this.txtPatchAndLabels.Text = pLegendFormat.HorizontalPatchGap.ToString();     
    
            //图层加载
            if (pMap.LayerCount > 0)
            {
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    //将所有数据名称加载到ListView中
                    this.LViewMapLayer.Items.Add(pMap.get_Layer(i).Name);

                    for (int j = 0; j < Legend.ItemCount; j++)
                    {
                        ILegendItem pLegendItem = Legend.Item[j];
                        if (pMap.get_Layer(i).Name == pLegendItem.Layer.Name)
                        {
                            this.LViewLegendLayer.Items.Add(pLegendItem .Layer .Name);
                            pSelectedidx.Add(i);
                        }
                    }
                }
            }

            //各类文本样式
            ILegendItem pLegendItem0 = Legend.Item[0];
            pLayerNameSymbol = pLegendItem0.LayerNameSymbol;
            pHeadingSymbol = pLegendItem0.HeadingSymbol;
            ILegendClassFormat pLegendClassFormat = pLegendItem0.LegendClassFormat;
            pLabelSymbol = pLegendClassFormat.LabelSymbol;
            pDescriptionSymbol = pLegendClassFormat.DescriptionSymbol;

            ////图例列数
            //ILegendItem pLegendItemff = new HorizontalLegendItemClass();
            //this.LegendColumn.Text = pLegendItemff.Columns.ToString ();
            //文本样式下拉列表
            this.cboBoxTextSymbol.SelectedIndex = 0;

            cBoxAddVisibleLayer.Checked = Legend.AutoVisibility;//只显示显示的图层
            cBoxAddNewLayer.Checked = Legend.AutoAdd;//自动添加显示的图层
            cBoxNewOrder.Checked = Legend.AutoReorder;//按照图层顺序排列图例
 
            //地图框架
            IFrameProperties pFrameProperties=pMapSurroundFrame as IFrameProperties;
            if (pMapSurroundFrame.Border != null)
            {
                FrmFrameBorder frm = new FrmFrameBorder(SymbolStyle,(ISymbolBorder)pMapSurroundFrame.Border);

                if (btBorder.Image != null)
                    btBorder.Image.Dispose();
                this.btBorder.Image = frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBorders,(ISymbolBorder)pMapSurroundFrame.Border,btBorder.Width,btBorder.Height);
                pSymbolBorder = pFrameProperties.Border as ISymbolBorder;
                txtBorderGap.Text = pFrameProperties.Border.Gap.ToString();
                txtBorderAngle.Text = pSymbolBorder.CornerRounding.ToString();
            }
            if (pMapSurroundFrame.Background != null)
            {
                FrmFrameBackground frm = new FrmFrameBackground(SymbolStyle, (ISymbolBackground)pMapSurroundFrame.Background);

                if (btBackground.Image != null)
                    btBackground.Image.Dispose();
                this.btBackground.Image = frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassBackgrounds,(ISymbolBackground)pMapSurroundFrame.Background,btBackground.Width ,btBackground.Height);

                pSymbolBackground = pFrameProperties.Background as ISymbolBackground;
                txtBackgroundGap.Text = pSymbolBackground.Gap.ToString();
                txtBackgroundAngle.Text = pSymbolBackground.CornerRounding.ToString();
            }
            if (pFrameProperties.Shadow != null)
            {
                FrmFrameShadow frm = new FrmFrameShadow(SymbolStyle, (ISymbolShadow)pFrameProperties.Shadow);

                if (btShadow.Image != null)
                    btShadow.Image.Dispose();
                this.btShadow.Image = frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassShadows,(ISymbolShadow)pFrameProperties .Shadow ,btShadow .Width ,btShadow .Height);

                pSymbolShadow = pFrameProperties.Shadow as ISymbolShadow;
                txtShadowX.Text = pSymbolShadow.HorizontalSpacing.ToString();
                txtShadowY.Text = pSymbolShadow.VerticalSpacing.ToString();
                txtShadowAngle.Text = pSymbolShadow.CornerRounding.ToString();
            }


        }
      
        //标题样式
        private void btSymbol_Click(object sender, EventArgs e)
        {
            FrmTextSymbol frm = new FrmTextSymbol();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                pTitleSymbol = frm.GetTextSymbol();
            }
        }

        //图面样式选择
        private void btLineShape_Click(object sender, EventArgs e)
        {
            FrmPatchLineAndArea frm = new FrmPatchLineAndArea(esriSymbologyStyleClass .esriStyleClassLinePatches);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                pLinePatch = frm.GetLinePatch();
                if (pLinePatch != null)
                {
                    this.btLineShape.Image = frm.GetImageByGiveSymbolAfterSelectItem(btLineShape .Width ,btLineShape .Height);
                }
            }
        }

        private void btAreaShape_Click(object sender, EventArgs e)
        {
            FrmPatchLineAndArea frm = new FrmPatchLineAndArea(esriSymbologyStyleClass .esriStyleClassAreaPatches);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                pAreaPatch = frm.GetAreaPatch();
                if (pAreaPatch != null)
                {
                    this.btAreaShape.Image = frm.GetImageByGiveSymbolAfterSelectItem(btAreaShape .Width ,btAreaShape .Height);
                }
            }
        }

        //左右移动图层名称
        private void btToRight_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LViewMapLayer.SelectedIndices.Count; i++)
            {
                int pIndex = LViewMapLayer.SelectedIndices[i];
                if (pSelectedidx.Contains(pIndex) == false)
                {
                    this.LViewLegendLayer.Items.Add(pMap.get_Layer(pIndex).Name);
                    pSelectedidx.Add(pIndex);
                }
            }
        }

        private void btAllToRight_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LViewMapLayer.Items.Count; i++)
            {
                if (pSelectedidx.Contains(i) == false)
                {
                    this.LViewLegendLayer.Items.Add(pMap.get_Layer(i).Name);
                    pSelectedidx.Add(i);
                }
            }
        }

        private void btToLeft_Click(object sender, EventArgs e)
        {
            for (int i = LViewLegendLayer.SelectedIndices.Count - 1; i >= 0; i--)
            {

                pSelectedidx.RemoveAt(LViewLegendLayer.SelectedIndices[i]);
                this.LViewLegendLayer.Items.RemoveAt(LViewLegendLayer.SelectedIndices[i]);
                
            }

        }

        private void btAllToLeft_Click(object sender, EventArgs e)
        {
            this.LViewLegendLayer.Items.Clear();
            pSelectedidx.Clear();
        }

        //上下移动图层名称
        private void btUpTop_Click(object sender, EventArgs e)
        {
            //移动到顶端
            int pSelectIndex = LViewLegendLayer.SelectedItems[0].Index;
            if (pSelectIndex != 0)
            {
                string pSelectItem = LViewLegendLayer.Items[pSelectIndex].SubItems[0].Text;
                for (int i = pSelectIndex; i > 0; i--)
                {
                    LViewLegendLayer.Items[i].SubItems[0].Text = LViewLegendLayer.Items[i - 1].SubItems[0].Text;
                    pSelectedidx[i] = i - 1;
                }
                LViewLegendLayer.Items[0].SubItems[0].Text = pSelectItem;
                pSelectedidx[0] = pSelectIndex;
            }
            else
            {
                return;
            }
        }

        private void btUpOne_Click(object sender, EventArgs e)
        {
            int pSelectIndex = LViewLegendLayer.SelectedItems[0].Index;

            if (pSelectIndex != 0)
            {
                string pSelectItem = LViewLegendLayer.Items[pSelectIndex].SubItems[0].Text;

                LViewLegendLayer.Items[pSelectIndex].SubItems[0].Text = LViewLegendLayer.Items[pSelectIndex - 1].SubItems[0].Text;
                LViewLegendLayer.Items[pSelectIndex - 1].SubItems[0].Text = pSelectItem;
                LViewLegendLayer.Items[pSelectIndex - 1].Selected = true;

                int temp = pSelectedidx[pSelectIndex];
                pSelectedidx[pSelectIndex] = pSelectedidx[pSelectIndex - 1];
                pSelectedidx[pSelectIndex - 1] = temp;
            }
            else
            {
                return;
            }      
        }

        private void btDownOne_Click(object sender, EventArgs e)
        {
            //向下移动一个
            int pSelectIndex = LViewLegendLayer.SelectedItems[0].Index;

            if (pSelectIndex != (LViewLegendLayer.Items.Count - 1))
            {
                string pSelectItem = LViewLegendLayer.Items[pSelectIndex].SubItems[0].Text;
                LViewLegendLayer.Items[pSelectIndex].SubItems[0].Text = LViewLegendLayer.Items[pSelectIndex + 1].SubItems[0].Text;
                LViewLegendLayer.Items[pSelectIndex + 1].SubItems[0].Text = pSelectItem;
                LViewLegendLayer.Items[pSelectIndex + 1].Selected = true;

                int temp = pSelectedidx[pSelectIndex];
                pSelectedidx[pSelectIndex] = pSelectedidx[pSelectIndex + 1];
                pSelectedidx[pSelectIndex + 1] = temp;
            }
            else
            {
                return;
            }

        }

        private void btDownBottom_Click(object sender, EventArgs e)
        {
            //移到最底部
            int pSelectIndex = LViewLegendLayer.SelectedItems[0].Index;
            if (pSelectIndex != (LViewLegendLayer.Items.Count - 1))
            {
                string pSelectItem = LViewLegendLayer.Items[pSelectIndex].SubItems[0].Text;

                for (int i = pSelectIndex; i < LViewLegendLayer.Items.Count - 1; i++)
                {
                    LViewLegendLayer.Items[i].SubItems[0].Text = LViewLegendLayer.Items[i + 1].SubItems[0].Text;
                    pSelectedidx[i] = i + 1;
                }
                LViewLegendLayer.Items[LViewLegendLayer.Items.Count - 1].SubItems[0].Text = pSelectItem;
                pSelectedidx[LViewLegendLayer.Items.Count - 1] = pSelectIndex;
            }
        }

        private void LViewLegendLayer_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (LViewLegendLayer.SelectedItems.Count > 0)
                {
                    lineindex = LViewLegendLayer.SelectedItems[0].Index;
                }
            }
            catch (System.Exception ex)
            {

            }
           
        }


        //图例样式选择 （没有完成）
        private void btLegendSymbol_Click(object sender, EventArgs e)
        {
            if (lineindex<Legend.ItemCount)
            {
                FrmLegendStyle frm = new FrmLegendStyle(Legend.Item[lineindex],Legend,lineindex);
                frm.ShowDialog();
            } 
            else
            {
            }
            
            
        }
    
        //图例中类型文本样式
        private void btTextSymbol_Click(object sender, EventArgs e)
        {
            if (this.cboBoxTextSymbol.SelectedIndex == 0)
            {
                //所有文本的样式
                FrmTextSymbol frm = new FrmTextSymbol();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                   pWholeItemSymbol = frm.GetTextSymbol();
                }
            }
            else if (this.cboBoxTextSymbol.SelectedIndex == 1)
            {
                //图层名样式
                FrmTextSymbol frm = new FrmTextSymbol();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    pLayerNameSymbol = frm.GetTextSymbol();
                }
            }
            else if (this.cboBoxTextSymbol.SelectedIndex ==2)
            {
                //标题样式
                FrmTextSymbol frm = new FrmTextSymbol();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    pHeadingSymbol  = frm.GetTextSymbol();
                }
            }
            else if (this.cboBoxTextSymbol.SelectedIndex == 3)
            {
                //标注样式
                FrmTextSymbol frm = new FrmTextSymbol();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                   pLabelSymbol = frm.GetTextSymbol();
                }
            }
            else if (this.cboBoxTextSymbol.SelectedIndex == 4)
            {
                //描述样式
                FrmTextSymbol frm = new FrmTextSymbol();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                   pDescriptionSymbol= frm.GetTextSymbol();
                }
            }
        }

        //边框、背景、阴影
        private void btBorder_Click(object sender, EventArgs e)
        {
            FrmFrameBorder Frm = new FrmFrameBorder(SymbolStyle, pSymbolBorder);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                pSymbolBorder = Frm.GetSymbolBorder();
                if (pSymbolBorder != null)
                {
                    btBorder.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btBorder.Width - 14, btBorder.Height - 14);
                }
            }
        }

        private void btBackground_Click(object sender, EventArgs e)
        {
            FrmFrameBackground Frm = new FrmFrameBackground(SymbolStyle, pSymbolBackground);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                pSymbolBackground = Frm.GetSymbolBackground();
                if (pSymbolBackground != null)
                {
                    btBackground.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btBackground.Width - 14, btBackground.Height - 14);
                }
            }
        }

        private void btShadow_Click(object sender, EventArgs e)
        {
            FrmFrameShadow Frm = new FrmFrameShadow(SymbolStyle, pSymbolShadow);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                pSymbolShadow = Frm.GetSymbolShadow();
                if (pSymbolShadow != null)
                {
                    btShadow.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btShadow.Width - 14, btShadow.Height - 14);
                }
            }
        }

        //撤销边框、背景、阴影
        private void btReturnBorder_Click(object sender, EventArgs e)
        {
            pSymbolBorder = null;
            this.btBorder.Image = null;
        }

        private void btReturnBackground_Click(object sender, EventArgs e)
        {
            pSymbolBackground = null;
            this.btBackground.Image = null;
        }

        private void btReturnShadow_Click(object sender, EventArgs e)
        {
            pSymbolShadow = null;
            this.btShadow.Image = null;
        }

        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            UpdateLegend();
            this.Close();
        }
        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //更新图例
        private void UpdateLegend()
        {
            //边框、背景、阴影的间距
            if (txtBorderGap.Text != null && pSymbolBorder != null)
            {
                pSymbolBorder.Gap = double.Parse(this.txtBorderGap.Text);
            }
            if (txtBackgroundGap.Text != null && pSymbolBackground != null)
            {
                pSymbolBackground.Gap = double.Parse(this.txtBackgroundGap.Text);
            }
            if (txtShadowX.Text != null && pSymbolShadow != null)
            {
                pSymbolShadow.VerticalSpacing = double.Parse(this.txtShadowX.Text);
            }
            if (txtShadowY.Text != null && pSymbolShadow != null)
            {
                pSymbolShadow.HorizontalSpacing = double.Parse(this.txtShadowY.Text);
            }
            //边框、背景、阴影的角度
            if (txtBorderAngle.Text != null && pSymbolBorder != null)
            {
                pSymbolBorder.CornerRounding = short.Parse(txtBorderAngle.Text);
            }
            if (txtBackgroundAngle.Text != null && pSymbolBackground != null)
            {
                pSymbolBackground.CornerRounding = short.Parse(txtBackgroundAngle.Text);
            }
            if (txtShadowAngle.Text != null && pSymbolShadow != null)
            {
                pSymbolShadow.CornerRounding = short.Parse(txtShadowAngle.Text);
            }
            //边框、背景、阴影
            IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            if (pSymbolBorder != null)
            {
                pFrameProperties.Border = pSymbolBorder;
            }
            if (pSymbolBackground != null)
            {
                pFrameProperties.Background = pSymbolBackground;
            }
            if (pSymbolShadow != null)
            {
                pFrameProperties.Shadow = pSymbolShadow;
            }

            ILegendFormat pLegendFormat = new LegendFormatClass();

            //标题设置  显示
            if (this.cBoxShowTitle.Checked == true)
            {
                pLegendFormat.ShowTitle = true;
            }
            else
            {
                pLegendFormat.ShowTitle = false;
            }
            //标题样式

            if (pTitleSymbol != null)
            {
                pLegendFormat.TitleSymbol = pTitleSymbol;
            }
            
            //图面样式以及高度和样式设计
            if (pLinePatch != null)
            {
                pLegendFormat.DefaultLinePatch = pLinePatch;
            }
            if (pAreaPatch != null)
            {
                pLegendFormat.DefaultAreaPatch = pAreaPatch;
            }
            if (txtPatchWith.Text != null)
            {
                pLegendFormat.DefaultPatchWidth = double.Parse(txtPatchWith .Text);
            }
            if (txtPatchHeight.Text != null)
            {
                pLegendFormat.DefaultPatchHeight = double.Parse(txtPatchHeight.Text);
            }

            //第四界面从上到下图例各部分之间的距离
            pLegendFormat.TitleGap = double.Parse(txtTitleAndLegend.Text);
            pLegendFormat.VerticalItemGap = double.Parse(txtLegendItems.Text);
            pLegendFormat.HorizontalItemGap = double.Parse(txtColumns.Text);
            pLegendFormat.HeadingGap = double.Parse(txtHeadings.Text);
            pLegendFormat.TextGap = double.Parse(txtLabelsAndDescription.Text);
            pLegendFormat.VerticalPatchGap = double.Parse(txtPatchesV.Text);
            pLegendFormat.HorizontalPatchGap = double.Parse(txtPatchAndLabels.Text);

            //样式添加到图例
            Legend.Title = this.txtLegendTitle.Text;
            Legend.Format = pLegendFormat;
            //地图链接
            
            Legend.AutoVisibility = cBoxAddVisibleLayer.Checked;//只显示显示的图层
            Legend.AutoAdd = cBoxAddNewLayer.Checked;//自动添加显示的图层
            Legend.AutoReorder = cBoxNewOrder.Checked;//按照图层顺序排列图例

            //Legend.ClearItems();
            //Legend.Refresh();

            //图例列数
            double pLegendColumn = double.Parse(this.LegendColumn.Text);
            //图例中要包含的图层个数为  pSelectedidx.Count
            double pLegendItemCount = pSelectedidx.Count;
            //计算图例分裂的位置
            int ColumnPos = (int)(pLegendItemCount / pLegendColumn);
            double remainder = (pLegendItemCount % pLegendColumn) / pLegendColumn;
            if (remainder >= 0.5)
            {
                ColumnPos = ColumnPos + 1;
            }

            for (int i = 0; i < pSelectedidx.Count; i++)
            {
                ILegendItem pLegendItem = new HorizontalLegendItemClass();
                pLegendItem.Layer = pMap.get_Layer(pSelectedidx[i]);
                int lengendinex = 0;//记录同名图层在legend中的位置
                for (int j = 0; j < Legend.ItemCount;j++ )
                {
                    if (Legend.Item[j].Layer==pLegendItem.Layer)
                    {
                        lengendinex = j;
                        break;
                    }
                }
                pLegendItem.ShowDescriptions = Legend.Item[lengendinex].ShowDescriptions;
                pLegendItem.ShowHeading = Legend.Item[lengendinex].ShowHeading;
                pLegendItem.ShowLabels = Legend.Item[lengendinex].ShowLabels;
                pLegendItem.ShowLayerName = Legend.Item[lengendinex].ShowLayerName;
                pLegendItem.HeadingSymbol = Legend.Item[lengendinex].HeadingSymbol;
                pLegendItem.LayerNameSymbol = Legend.Item[lengendinex].LayerNameSymbol;
                pLegendItem.LegendClassFormat = Legend.Item[lengendinex].LegendClassFormat;

                //列数设置
                //列数设置
                if (ColumnPos <= 1)
                {
                    pLegendItem.NewColumn = true;
                }
                else if (ColumnPos > 1 && i > 0)
                {
                    int columnI = i % ColumnPos;
                    if (columnI == 0)
                    {
                        pLegendItem.NewColumn = true;
                    }
                }
                legendlist.Add(pLegendItem);

            }
            Legend.ClearItems();
            Legend.Refresh();
            for (int i = 0; i < legendlist.Count;i++ )
            {
                Legend.AddItem(legendlist[i]);
            }
            ILegendLayout pLegendLayout = pLegendFormat as ILegendLayout;
            pLegendLayout.ScaleGraphicsOnResize = false;
            m_hookHelper.ActiveView.Refresh();
        }

        private void gPanelShadow_Click(object sender, EventArgs e)
        {

        }

        private void gPanelBackground_Click(object sender, EventArgs e)
        {

        }

        private void gPanelBorder_Click(object sender, EventArgs e)
        {

        }

        private void txtBorderAngle_TextChanged(object sender, EventArgs e)
        {

        }

        private void labFramAngle_Click(object sender, EventArgs e)
        {

        }

        private void txtBorderGap_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
