/**
* @copyright Copyright(C), 2012-2013, PMRS Lab, IRSA, CAS
* @file FrmLegend.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 添加图例窗体
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*Ge Xizhi  2013.03.15  1.0  修改了边框、背景、阴影的设计 
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
    public partial class FrmLegend : OfficeForm
    {
        private IHookHelper m_hookHelper;
        string[] SymbolStyle;
        List<int> pSelectedidx = null;
        IActiveView pActiveView;
        IMap pMap;

        //边框、背景、阴影
        ISymbolBorder pSymbolBorder;
        ISymbolBackground pSymbolBackground;
        ISymbolShadow pSymbolShadow;
        //图面样式
        ILinePatch pLinePatch;
        IAreaPatch pAreaPatch;

        public FrmLegend(string [] symbolstyle,IHookHelper hookHelper)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_hookHelper = hookHelper;
            SymbolStyle = symbolstyle;
            pActiveView = m_hookHelper.PageLayout as IActiveView;
            pMap = pActiveView.FocusMap;

            pSelectedidx = new List<int>();
        }

        //开始加载
        private void FrmLegend_Load(object sender, EventArgs e)
        {
            //第一界面需要加载内容
            if (pMap.LayerCount > 0)
            {
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    //将所有数据名称加载到第一个ListView中
                    this.LViewMapLayer.Items.Add(pMap .get_Layer(i).Name);
                    //将所有显示的数据名称加载到第二个ListView中
                    if(pMap .get_Layer (i).Visible ==true )
                    {
                        //第一界面中需要添加到图例的图层列表
                        this.LViewLegendLayer.Items .Add (pMap .get_Layer (i).Name);
                        pSelectedidx.Add(i);
                    }
                }
            }
            //第二界面需要加载内容
            
            //加载字体名称
             InstalledFontCollection pFontCollection = new InstalledFontCollection();
             FontFamily[] pFontFamily = pFontCollection.Families;
             for (int i = 0; i < pFontFamily.Length; i++)
             {
                 string pFontName = pFontFamily[i].Name;
                 this.cboBoxTitleFontName.Items.Add(pFontName);
             }
             this.cboBoxTitleFontName.Text = "宋体";
            //加载字体大小
             for (int i = 3; i <= 100; i++)
             {
                 this.cboBoxTitleFontSize.Items.Add(i.ToString ());
             }
             this.cboBoxTitleFontSize.Text = "10";
            //颜色
             btTitleColor.SelectedColor = Color.Black;
            //字体风格和位置
             this.toolTitlePostion.Visible = true;
             this.toolTitleStyle.Visible = true;

            //第三界面需要加载内容
             ILegendFormat pLegendFormat = new LegendFormatClass();
             if (pLegendFormat.DefaultLinePatch != null)
             {             
                 FrmPatchLineAndArea frm = new FrmPatchLineAndArea(esriSymbologyStyleClass.esriStyleClassLinePatches);
                 this.btLineShape.Image = frm.GetImageByGiveLineSymbolBeforeSelectItem(esriSymbologyStyleClass .esriStyleClassLinePatches ,(ILinePatch)pLegendFormat .DefaultLinePatch ,btLineShape .Width-14 ,btLineShape .Height-14 );
             }
             if (pLegendFormat.DefaultAreaPatch != null)
             {
                 FrmPatchLineAndArea frm = new FrmPatchLineAndArea(esriSymbologyStyleClass .esriStyleClassAreaPatches);
                 this.btAreaShape.Image = frm.GetImageByGiveAreaSymbolBeforeSelectItem(esriSymbologyStyleClass .esriStyleClassAreaPatches ,(IAreaPatch)pLegendFormat .DefaultAreaPatch,btAreaShape .Width-14 ,btAreaShape .Height-14);
             }
            //第四界面需要加载内容
             this.LegendDistanceImage.Image  = global::LibCerMap.Properties.Resources._1;
        }

        #region 第一界面内容设计

        //左右移动图层名称
        private void btToRight_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LViewMapLayer.SelectedIndices.Count;i++ )
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
            for (int i = 0; i < LViewMapLayer.Items .Count ; i++)
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
                this.LViewLegendLayer.Items.RemoveAt(LViewLegendLayer.SelectedIndices [i]);
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
                LViewLegendLayer.Items[0].Selected = true;
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

                int temp=pSelectedidx [pSelectIndex];
                pSelectedidx[pSelectIndex]=pSelectedidx[pSelectIndex-1];
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
                //LViewLegendLayer.SelectedItems[]

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
                LViewLegendLayer.Items[LViewLegendLayer.Items.Count - 1].Selected = true;
                pSelectedidx[LViewLegendLayer.Items.Count - 1] = pSelectIndex;
            }
        }

        #endregion

        #region 第二界面内容设计

/**
* @fn CreateTitleSymbol
* @date 2013.03.04
* @author Ge Xizhi
* @brief 图例标题样式
* @param 返回 ITextSymbol
* @param 
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
        //图例标题字体设计
        private ITextSymbol CreateTitleSymbol()
        {
            ITextSymbol pTitleSymbol = new TextSymbolClass();
            IFontDisp pFont = new StdFontClass() as IFontDisp;
 
            pTitleSymbol.Color = ClsGDBDataCommon.ColorToIColor(btTitleColor.SelectedColor);

            //字体大小
            pFont.Size = decimal.Parse(this.cboBoxTitleFontSize.Text);

            //字体名
            pFont.Name = this.cboBoxTitleFontName.Text;     
 
            //字体加粗
            if (this.toolBtnBold.Checked == true)
            {
                pFont.Bold = true;
            }
            //倾斜
            if (this.toolBtnIntend.Checked == true)
            {
                pFont.Italic = true;
            }
            else
            {
                pFont.Italic = false;
            }
            //下划线
            if (this.toolBtnUnderline.Checked == true)
            {
                pFont.Underline = true;
            }
            else
            {
                pFont.Underline = false;
            }
            //中间线
            if (this.toolBtnStrikethrough.Checked == true)
            {
                pFont.Strikethrough = true;
            }
            else
            {
                pFont.Strikethrough = false;
            }           
            //将字体赋值给pTitleSymbol
            pTitleSymbol.Font = pFont;

            //字体在水平方向的位置
            if (this.toolBtnLeft.Checked == true)
            {
                pTitleSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            }
            else if (this.toolBtnCenter.Checked == true)
            {
                pTitleSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            }
            else if (this.toolBtnRight.Checked == true)
            {
                pTitleSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
            }
            else if (this.toolBtnBoth.Checked == true)
            { 
                pTitleSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull;
            }

            //返回样式
            return pTitleSymbol;
        }
        //图例标题位置
        private void toolBtnLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (this.toolBtnLeft.Checked == true)
            {
                this.toolBtnLeft.Checked = true;
                this.toolBtnCenter.Checked = false;
                this.toolBtnRight.Checked = false;
                this.toolBtnBoth.Checked = false;
            }
        }

        private void toolBtnCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.toolBtnCenter.Checked == true)
            {
                this.toolBtnCenter.Checked = true;
                this.toolBtnRight.Checked = false;
                this.toolBtnBoth.Checked = false;
                this.toolBtnLeft.Checked = false;
            }
        }

        private void toolBtnRight_CheckedChanged(object sender, EventArgs e)
        {
            if (this.toolBtnRight.Checked == true)
            {
                this.toolBtnCenter.Checked = false;
                this.toolBtnRight.Checked = true;
                this.toolBtnBoth.Checked = false;
                this.toolBtnLeft.Checked = false;
            }
        }

        private void toolBtnBoth_CheckedChanged(object sender, EventArgs e)
        {
            if (this.toolBtnBoth.Checked == true)
            {
                this.toolBtnCenter.Checked = false;
                this.toolBtnRight.Checked = false;
                this.toolBtnBoth.Checked = true;
                this.toolBtnLeft.Checked = false;
            }
        }

        #endregion

        #region 第三界面内容设计
        //框架、阴影、背景样式
        private void btBorder_Click(object sender, EventArgs e)
        {
            //IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            FrmFrameBorder Frm = new FrmFrameBorder(SymbolStyle,pSymbolBorder);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                pSymbolBorder = Frm.GetSymbolBorder();
                if (pSymbolBorder != null)
                {
                    btBorder.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btBorder.Width - 14, btBorder.Height - 14);
                    //pSymbolBorder.Gap = double.Parse(this.txtLegendFrameGap.Text);
                    //pSymbolBorder.CornerRounding = short.Parse(txtLegendFrameAngle.Text);
                    //pFrameProperties.Border = (IBorder)pSymbolBorder;
                }
            }

        }

        private void btBackground_Click(object sender, EventArgs e)
        {
            //IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            FrmFrameBackground Frm = new FrmFrameBackground(SymbolStyle,pSymbolBackground);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                pSymbolBackground = Frm.GetSymbolBackground();
                if (pSymbolBackground != null)
                {
                    btBackground.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btBackground.Width - 14, btBackground.Height - 14);
                    //pSymbolBackground.CornerRounding = short.Parse(txtLegendFrameAngle.Text);
                    //pSymbolBackground.Gap = double.Parse(this.txtLegendFrameGap.Text);
                    //pFrameProperties.Background = (IBackground)pSymbolBackground;
                }
            }
        }

        private void btShadow_Click(object sender, EventArgs e)
        {
            //IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            FrmFrameShadow Frm = new FrmFrameShadow(SymbolStyle, pSymbolShadow);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                pSymbolShadow = Frm.GetSymbolShadow();
                if (pSymbolShadow != null)
                {
                    btShadow.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btShadow.Width - 14, btShadow.Height - 14);                  
                    //pSymbolShadow.CornerRounding = short.Parse(txtLegendFrameAngle.Text);
                    //pSymbolShadow.HorizontalSpacing = double.Parse(this.txtLegendFrameGap.Text);
                    //pSymbolShadow.VerticalSpacing = double.Parse(this.txtLegendFrameGap.Text); ;                 
                    //pFrameProperties.Shadow = (IShadow)pSymbolShadow;
                }
            }
        }
       
        private void btLineShape_Click(object sender, EventArgs e)
        {
            FrmPatchLineAndArea Frm = new FrmPatchLineAndArea(esriSymbologyStyleClass.esriStyleClassLinePatches);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                pLinePatch=Frm .GetLinePatch ();
                if (pLinePatch != null)
                {
                    //pLegendFormat.DefaultLinePatch = pLinePatch;
                    btLineShape.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btLineShape .Width-14 ,btLineShape .Height-14);
                }
            }
        }

        private void btAreaShape_Click(object sender, EventArgs e)
        {
            FrmPatchLineAndArea Frm = new FrmPatchLineAndArea(esriSymbologyStyleClass.esriStyleClassAreaPatches);
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                pAreaPatch = Frm.GetAreaPatch();
                if (pAreaPatch != null)
                {
                    //pLegendFormat.DefaultAreaPatch = pAreaPatch;
                    btAreaShape.Image = Frm.GetImageByGiveSymbolAfterSelectItem(btAreaShape.Width-14, btAreaShape.Height-14);
                }
            }
        }
        //撤销按钮
        private void btReturnBorder_Click(object sender, EventArgs e)
        {
            //IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            //pFrameProperties.Border = null;
            pSymbolBorder = null;
            btBorder.Image = null;
        }

        private void btReturnBackground_Click(object sender, EventArgs e)
        {
            //IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            //pFrameProperties.Background = null;
            pSymbolBackground = null;
            btBackground.Image = null;
        }

        private void btReturnShadow_Click(object sender, EventArgs e)
        {
            //IFrameProperties pFrameProperties = pMapSurroundFrame as IFrameProperties;
            //pFrameProperties.Shadow = null;
            pSymbolShadow = null;
            btShadow.Image = null;
        }

        #endregion

        #region 第四界面内容设计

        //当鼠标点击各部分距离文本框，添加对应的图片 LegendDistanceImage
        private void txtTitleAndLegend_MouseClick(object sender, MouseEventArgs e)
        {
            this.LegendDistanceImage.Image = global::LibCerMap.Properties.Resources._1;
        }

        private void txtLegendItems_MouseClick(object sender, MouseEventArgs e)
        {
            this.LegendDistanceImage.Image = global::LibCerMap.Properties.Resources._2;
        }

        private void txtColumns_MouseClick(object sender, MouseEventArgs e)
        {
            this.LegendDistanceImage.Image = global::LibCerMap.Properties.Resources._3;
        }

        private void txtHeadings_MouseClick(object sender, MouseEventArgs e)
        {
            this.LegendDistanceImage.Image = global::LibCerMap.Properties.Resources._4;
        }

        private void txtLabelsAndDescription_MouseClick(object sender, MouseEventArgs e)
        {
            this.LegendDistanceImage.Image = global::LibCerMap.Properties.Resources._5;
        }

        private void txtPatchesV_MouseClick(object sender, MouseEventArgs e)
        {
            this.LegendDistanceImage.Image = global::LibCerMap.Properties.Resources._6;
        }

        private void txtPatchAndLabels_MouseClick(object sender, MouseEventArgs e)
        {
            this.LegendDistanceImage.Image = global::LibCerMap.Properties.Resources._7;
        }

        #endregion

        //点击完成按钮
        private void WizardLegend_FinishButtonClick(object sender, CancelEventArgs e)
        {
            try
            {
                //创建图例
                CreateLegend();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        //点击取消按钮
        private void WizardLegend_CancelButtonClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        //创建图例
        private void CreateLegend()
        {
            ILegendFormat pLegendFormat = new LegendFormatClass();
            IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
            pActiveView = m_hookHelper.ActiveView as IActiveView;
            pMap = pActiveView.FocusMap;

            //边框、背景、阴影的间距
            if (txtLegendFrameGap.Text != null && pSymbolBorder != null)
            {
                pSymbolBorder.Gap = double.Parse(this.txtLegendFrameGap.Text);
            }
            if (txtLegendFrameGap.Text != null && pSymbolBackground != null)
            {
                pSymbolBackground.Gap = double.Parse(this.txtLegendFrameGap.Text);
            }
            if (txtLegendFrameGap.Text != null && pSymbolShadow != null)
            {
                pSymbolShadow.VerticalSpacing = double.Parse(this.txtLegendFrameGap.Text)+10;
                pSymbolShadow.HorizontalSpacing = double.Parse(this.txtLegendFrameGap.Text)+10;
            }
            //边框、背景、阴影的角度
            if (txtLegendFrameAngle.Text != null && pSymbolBorder != null)
            {
                pSymbolBorder.CornerRounding = short.Parse(txtLegendFrameAngle.Text);
            }
            if (txtLegendFrameAngle.Text != null && pSymbolBackground != null)
            {
                pSymbolBackground.CornerRounding = short.Parse(txtLegendFrameAngle.Text);
            }
            if (txtLegendFrameAngle.Text != null && pSymbolShadow != null)
            {
                pSymbolShadow.CornerRounding = short.Parse(txtLegendFrameAngle.Text);
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
      
            //显示标题
            pLegendFormat.ShowTitle = true;
            //标题位置
            pLegendFormat.TitlePosition = esriRectanglePosition.esriTopSide;
            //标题样式
            pLegendFormat.TitleSymbol = CreateTitleSymbol();
            

            //第四界面从上到下图例各部分之间的距离
            pLegendFormat.TitleGap = double.Parse(txtTitleAndLegend.Text);
            pLegendFormat.VerticalItemGap = double.Parse(txtLegendItems.Text);
            pLegendFormat.HorizontalItemGap = double.Parse(txtColumns.Text);
            pLegendFormat.HeadingGap = double.Parse(txtHeadings.Text);
            pLegendFormat.TextGap = double.Parse(txtLabelsAndDescription .Text);
            pLegendFormat.VerticalPatchGap = double.Parse(txtPatchesV.Text);
            pLegendFormat.HorizontalPatchGap = double.Parse(txtPatchAndLabels.Text);
           
            pLegendFormat.LayerNameGap = 2;

            //图面的样式以及高度和宽度
            if (pLinePatch != null)
            {
                pLegendFormat.DefaultLinePatch = pLinePatch;
            }
            if (pAreaPatch != null)
            {
                pLegendFormat.DefaultAreaPatch = pAreaPatch;
            }
            pLegendFormat.DefaultPatchHeight = double.Parse(this.txtPatchHeight.Text);
            pLegendFormat.DefaultPatchWidth = double.Parse(this.txtPatchWith.Text);

            
            //创建图例
            ILegend pLegend = new LegendClass_2();
            //图例名称
            //pLegend.Name = this.txtLegendTitle.Text;
            pLegend.AutoVisibility = false;//显示显示的图层            
            pLegend.AutoAdd = false;  //自动添加新数据到图例

            pLegend.AutoReorder = true;//根据图层顺序自动排列
            pLegend.Title = this.txtLegendTitle.Text;
            pLegend.Format = pLegendFormat;
            
            pLegend.Map = pMap;
            pLegend.ClearItems();
            //pLegend.Refresh();
           
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
            //设置添加到图例的图层
            for (int i = 0; i < pSelectedidx.Count; i++)
            {
                ILegendItem pLegendItem = new HorizontalLegendItemClass();
                
                ILayer pLayer=(ILayer)(pMap.get_Layer(pSelectedidx[i]));
                if(!pLayer.Valid)continue;
                pLegendItem.Layer = pLayer;
                
                //显示标注和描述
                pLegendItem.ShowDescriptions = true;
                pLegendItem.ShowLabels = true;

                //标注和描述字体样式
                ITextSymbol pLDTextSymbol = new TextSymbolClass();
                IFontDisp pLDFont = new StdFontClass() as IFontDisp;
                IRgbColor pLDColor = new RgbColorClass();
                pLDColor.Red = 0;
                pLDColor.Green = 0;
                pLDColor.Blue = 0;
                pLDTextSymbol.Color = pLDColor;

                pLDFont.Size = (decimal)8;
                pLDFont.Name = "宋体";
                pLDTextSymbol.Font = pLDFont;
                ILegendClassFormat pLegendClassFormat = new LegendClassFormatClass();
                pLegendClassFormat.LabelSymbol = pLDTextSymbol;
                pLegendClassFormat.DescriptionSymbol = pLDTextSymbol;

                pLegendItem.LegendClassFormat = pLegendClassFormat;
                //图层名称是否显示
                if (pLegendItem.Layer is IFeatureLayer)
                {
                    if (((IGeoFeatureLayer)pLegendItem.Layer).Renderer is ISimpleRenderer)
                    {
                        pLegendItem.ShowHeading = false;
                        pLegendItem.ShowLayerName = false;
                    }
                    else
                    {
                        pLegendItem.ShowLayerName = true;
                        pLegendItem.ShowHeading = true;
                        //图层名称和头文件的样式
                        ITextSymbol pLHTextSymbol = new TextSymbolClass();
                        IFontDisp pLHFont = new StdFontClass() as IFontDisp;
                        IRgbColor pLHColor = new RgbColorClass();
                        pLDColor.Red = 0;
                        pLDColor.Green = 0;
                        pLDColor.Blue = 0;

                        pLHTextSymbol.Color = pLHColor;
                        pLHFont.Name = "宋体";
                        pLHFont.Size = 10;
                        pLHFont.Bold = true;
                        pLHTextSymbol.Font = pLHFont;
                        pLHTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;

                        pLegendItem.HeadingSymbol = pLHTextSymbol;
                        pLegendItem.LayerNameSymbol = pLHTextSymbol;
                    }
                }
                else if (pLegendItem.Layer is IRasterLayer)
                {
                    pLegendItem.ShowLayerName = true;
                    pLegendItem.ShowHeading = true;
                    //图层名称和头文件的样式
                    ITextSymbol pLHTextSymbol = new TextSymbolClass();
                    IFontDisp pLHFont = new StdFontClass() as IFontDisp;
                    IRgbColor pLHColor = new RgbColorClass();
                    pLDColor.Red = 0;
                    pLDColor.Green = 0;
                    pLDColor.Blue = 0;

                    pLHTextSymbol.Color = pLHColor;
                    pLHFont.Name = "宋体";
                    pLHFont.Size = 10;
                    pLHFont.Bold = true;
                    pLHTextSymbol.Font = pLHFont;
                    pLHTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;

                    pLegendItem.HeadingSymbol = pLHTextSymbol;
                    pLegendItem.LayerNameSymbol = pLHTextSymbol;
                }

                //列数设置
                if (ColumnPos <=1)
                {
                    pLegendItem.NewColumn = true;
                }
                else if (ColumnPos > 1&& i>0)
                {
                    int columnI = i%ColumnPos;
                    if (columnI==0)
                    {
                        pLegendItem.NewColumn = true;
                    }
                }

                ILegendLayout pLegendLayout = pLegendFormat as ILegendLayout;
                pLegendLayout.ScaleGraphicsOnResize = false;
                

                //将图层及样式设计加入图例中
                pLegend.AddItem(pLegendItem);
                pLegend.Refresh();
            }


     
           
            //将图例加载到PageLayout上
            IGraphicsContainer pGraphicsContainer = pActiveView.GraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            if (pMapFrame == null)
            {
                return;
            }
            
            pMapSurroundFrame.MapFrame = pMapFrame;
            pMapSurroundFrame.MapSurround = (IMapSurround)pLegend;
            IElement pElement = (IElement)pMapSurroundFrame;

           
            IPageLayout pPageLayout = (IPageLayout)pActiveView;
            IPage pPage = pPageLayout.Page;
            double pWidth = pPage.PrintableBounds.XMax/10.0;
            double pHeigth = pPage.PrintableBounds.YMin/10.0;

            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.PutCoords(pWidth, pHeigth, pWidth+6, pHeigth+6);
            pElement.Geometry = (IGeometry)pEnvelope;
            
            pGraphicsContainer.AddElement((IElement)pMapSurroundFrame, 0);

            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pMapSurroundFrame, null);
        }
    }
}
