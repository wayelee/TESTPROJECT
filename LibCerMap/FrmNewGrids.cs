/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmNewGrids.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 新建地图格网
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
using stdole;

namespace LibCerMap
{
    public partial class FrmNewGrids : OfficeForm
    {
        //维护地图格网对象
        public IMapGrid m_pMapGrid = null;

        //当前格网要加入的布局
        //private IPageLayout m_pPageLayout = null;
        private IActiveView m_pActiveView = null;

        //只读属性
        public IMapGrid NewMapGrid
        {
            get
            {
                return m_pMapGrid;
            }
        }

        //样式
        string[] m_pSymbolStyles = null;

        //public FrmNewGrids(IPageLayout pPageLayout, string[] pSymbolStyles)
        //{
        //    InitializeComponent();

        //    m_pPageLayout = pPageLayout;
        //    m_pSymbolStyles = pSymbolStyles;

        //    //this.EnableGlass = false;
        //    //Symbolstyle = symbolstyle;
        //    //ItemPanel = pItemPanel;
        //    //m_hookHelper = hookHelper;
        //    //pActiveView = m_hookHelper.ActiveView as IActiveView;
        //    //pMap = pActiveView.FocusMap;
        //}

        public FrmNewGrids(IActiveView pActiveView, string[] pSymbolStyles)
        {
            InitializeComponent();

            //m_pPageLayout = pPageLayout;
            m_pSymbolStyles = pSymbolStyles;
            m_pActiveView = pActiveView;
            //this.EnableGlass = false;
            //Symbolstyle = symbolstyle;
            //ItemPanel = pItemPanel;
            //m_hookHelper = hookHelper;
            //pActiveView = m_hookHelper.ActiveView as IActiveView;
            //pMap = pActiveView.FocusMap;
        }

        private bool IsThisMapGridNameExist(IMapGrids pMapGrids, string szMapGridName)
        {
            int nCount=pMapGrids.MapGridCount;
            bool bFlag=false;
            for(int i=0;i<nCount;i++)
            {
                IMapGrid pCurrentGrid=pMapGrids.get_MapGrid(i);
                if(pCurrentGrid.Name.Equals(szMapGridName))
                {
                    bFlag=true;
                    break;
                }
            }

            return bFlag;
        }


        private IMapFrame getMapFrame()
        {
            //IActiveView pActiveView = m_pPageLayout as IActiveView;
            IGraphicsContainer pGraphicsContainer = m_pActiveView as IGraphicsContainer;
            IMap pMap = m_pActiveView.FocusMap;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;

            return pMapFrame;
        }

        private IMap getMap()
        {
            IMap pMap = m_pActiveView.FocusMap;
            return pMap;
        }

        //设置默认格网名
        private string getDefaultNewGridName()
        {
            IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            IMapGrids pMapGrids = pMapFrame as IMapGrids;

            //统计当前各种类型的地图格网个数
            int nCount = pMapGrids.MapGridCount;
            int[] nCountIndex = new int[3] { 0, 0, 0 }; //分别存储经纬格网、公里络网和索引格网个数
            for (int i = 0; i < nCount; i++)
            {
                IMapGrid pCurrentGrid = pMapGrids.get_MapGrid(i);
                if (pCurrentGrid is IGraticule)
                    nCountIndex[0]++;
                else if (pCurrentGrid is IMeasuredGrid)
                    nCountIndex[1]++;
                else if (pCurrentGrid is IIndexGrid)
                    nCountIndex[2]++;
                else
                    continue;
            }

            string szBaseName = null;
            int nSuffixNumber = 0;
            if (m_pMapGrid is IGraticule)
            {
                szBaseName = "经纬格网";
                nSuffixNumber = nCountIndex[0];
            }
            else if (m_pMapGrid is IMeasuredGrid)
            {
                szBaseName = "公里格网";
                nSuffixNumber = nCountIndex[1];
            }
            else if (m_pMapGrid is IIndexGrid)
            {
                szBaseName = "索引格网";
                nSuffixNumber = nCountIndex[2];
            }
            else
                return null;

            string szGridName = szBaseName + "_" + nSuffixNumber.ToString();
            while (IsThisMapGridNameExist(pMapGrids, szGridName))
            {
                nSuffixNumber++;
                szGridName = szBaseName + nSuffixNumber.ToString();
            }

            return szGridName;
        }

        //设置第一界面
        private void setFirstFrame()
        {
            //cBGraticule.Checked = true;
            //cBMeasured.Checked = false;
            //cBIndex.Checked = false;
            //m_pMapGrid = new GraticuleClass();
            cBGraticule.Checked = true;
            getDefaultNewGridName();
        }

        //设置第二界面
        private void setSecondFrame()
        {
            //外观
            this.gPanelGraticule.Location = new System.Drawing.Point(0, 0);
            this.gPanelMeasured.Location = new System.Drawing.Point(0, 0);
            this.gPanelIndex.Location = new System.Drawing.Point(0, 0);

            //经纬格网
            if (m_pMapGrid is IGraticule)
            {
                //设置经纬格网标注外观
                cBGLabel.Checked = true;
                cBGTickandLabel.Checked = false;
                cBGGraticuleandLabel.Checked = false;

                //线样式不可选
                btGSymbol.Enabled = false;

                //设置间隔
                try
                {
                    IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;
                    IMap pMap = getMap();
                    //经纬网间隔
                    if (pMap.LayerCount == 0)
                    {
                        this.txtGXIntervalD.Text = "10";
                        this.txtGXIntervalF.Text = "0";
                        this.txtGXIntervalM.Text = "0";
                        this.txtGYIntervalD.Text = "10";
                        this.txtGYIntervalF.Text = "0";
                        this.txtGYIntervalM.Text = "0";
                    }
                    else if (pMap.MapUnits == esriUnits.esriDecimalDegrees)
                    {
                        int pXD;
                        int pXF;
                        int pXM;
                        int pYD;
                        int pYF;
                        int pYM;

                        double pAverageX = pMapFrame.MapBounds.Envelope.Width / 10;
                        pXD = (int)pAverageX;
                        pXF = (int)((pAverageX - pXD) * 6) * 10;
                        pXM = (int)(((pAverageX - pXD) * 60 - (int)((pAverageX - pXD) * 60)) * 6) * 10;

                        this.txtGXIntervalD.Text = pXD.ToString();
                        this.txtGXIntervalF.Text = pXF.ToString();
                        this.txtGXIntervalM.Text = pXM.ToString();

                        double pAverageY = pMapFrame.MapBounds.Envelope.Height / 10;
                        pYD = (int)pAverageY;
                        pYF = (int)((pAverageY - pYD) * 6) * 10;
                        pYM = (int)(((pAverageY - pYD) * 60 - (int)((pAverageY - pYD) * 60)) * 6) * 10;

                        this.txtGYIntervalD.Text = pYD.ToString();
                        this.txtGYIntervalF.Text = pYF.ToString();
                        this.txtGYIntervalM.Text = pYM.ToString();
                    }
                    else
                    {
                        int pXD = 2;
                        int pXF = 0;
                        int pXM = 0;
                        int pYD = 2;
                        int pYF = 0;
                        int pYM = 0;
                        this.txtGXIntervalD.Text = pXD.ToString();
                        this.txtGXIntervalF.Text = pXF.ToString();
                        this.txtGXIntervalM.Text = pXM.ToString();
                        this.txtGYIntervalD.Text = pYD.ToString();
                        this.txtGYIntervalF.Text = pYF.ToString();
                        this.txtGYIntervalM.Text = pYM.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    return;
                }
            }
            else if (m_pMapGrid is IMeasuredGrid) //公里格网
            {
                //设置经纬格网标注外观
                cBGLabel.Checked = true;
                cBGTickandLabel.Checked = false;
                cBMMeasuredandLabel.Checked = false;

                //线样式不可选
                btGSymbol.Enabled = false;

                //设置间隔
                IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;
                IMap pMap = getMap();
                if (pMap.LayerCount == 0)
                {
                    this.txtMXinterval.Text = "200";
                    this.txtMYinterval.Text = "200";
                    this.labMXUnits.Text = pMap.MapUnits.ToString().Remove(0, 4);
                    this.labMYUnits.Text = pMap.MapUnits.ToString().Remove(0, 4);
                }
                else if (pMap.MapUnits == esriUnits.esriDecimalDegrees)
                {
                    string pAverageX = (pMapFrame.MapBounds.Envelope.Width / 5).ToString();
                    this.txtMXinterval.Text = pAverageX.Substring(0, 8);

                    string pAverageY = (pMapFrame.MapBounds.Envelope.Height / 5).ToString();
                    this.txtMYinterval.Text = pAverageY.Substring(0, 8);

                    this.labMXUnits.Text = "十进制度";
                    this.labMYUnits.Text = "十进制度";
                }
                else
                {
                    double px = pMapFrame.MapBounds.Envelope.Width / 5;
                    if (px >= 1)
                    {
                        string pAverageX = ((int)pMapFrame.MapBounds.Envelope.Width / 5).ToString();
                        this.txtMXinterval.Text = (int.Parse(pAverageX.Substring(0, 1)) * Math.Pow(10, pAverageX.Remove(0, 1).Length)).ToString();
                    }
                    else
                    {
                        string pAverageX = (pMapFrame.MapBounds.Envelope.Width / 5).ToString().Substring(0, 3);
                        this.txtMXinterval.Text = pAverageX;
                    }

                    double py = pMapFrame.MapBounds.Envelope.Height / 5;
                    if (py >= 1)
                    {
                        string pAverageY = ((int)pMapFrame.MapBounds.Envelope.Height / 5).ToString();
                        this.txtMYinterval.Text = (int.Parse(pAverageY.Substring(0, 1)) * Math.Pow(10, pAverageY.Remove(0, 1).Length)).ToString();
                    }
                    else
                    {
                        string pAverageY = (pMapFrame.MapBounds.Envelope.Height / 5).ToString().Substring(0, 3);
                        this.txtMYinterval.Text = pAverageY;
                    }

                    this.labMXUnits.Text = pMap.MapUnits.ToString().Remove(0, 4);
                    this.labMYUnits.Text = pMap.MapUnits.ToString().Remove(0, 4);
                }
            }
            else if (m_pMapGrid is IIndexGrid) //索引格网
            {
                //设置经纬格网标注外观
                cBIndexLabel.Checked = true;
                cBIndexGridandLabel.Checked = false;

                //线样式不可选
                btGSymbol.Enabled = false;

                //行列数
                IndexXInterval.Value = 5;
                IndexYInterval.Value = 5;
            }
            else
            {
                return;
            }
        }

        //设置第三界面
        private void setThirdFrame()
        {
            this.gPanelGMAxeAndFont.Location = new System.Drawing.Point(0, 0);
            this.cBoxLongAxe.Checked = true;
            this.btLongLine.Enabled = true;
            this.btShortLine.Enabled = true;
            this.cBoxShortAxe.Checked = true;
            this.LongAxeNumber.Enabled = true;
            this.LongAxeNumber.Value = 0;
            this.cBoxABC123.Checked = true;


            this.gPanelIndexAxeAndFont.Location = new System.Drawing.Point(0, 0);
            cbBoxIndexTab.SelectedIndex = 1;
            IndexColor.SelectedColor = Color.Black;
        }

        //设置第四界面
        private void setFourFrame()
        {
            this.gPanelGBorder.Location = new System.Drawing.Point(0, 0);
            this.cBoxGOutSimpleLine.Checked = true;
            this.btGSimpleLine.Enabled = true;
            this.cBoxGOutGroomLine.Checked = false;
            this.btGGroom.Enabled = false;
            this.cBoxGNeatLine.Checked = false;
            this.btGNeatLine.Enabled = false;
            this.cBoxGAttributeOne.Checked = false;
            this.cBoxGAttributeTwo.Checked = true;

            this.gPanelMBorder.Location = new System.Drawing.Point(0, 0);
            this.cBoxMOutSimpleLine.Checked = false;
            this.btMSimpleLine.Enabled = false;
            this.cBoxMNeatLine.Checked = false;
            this.btMNeatLine.Enabled = false;
            this.cBoxMAttributeOne.Checked = false;
            this.cBoxMAttributeTwo.Checked = true;

            this.gPanelIndexBorder.Location = new System.Drawing.Point(0, 0);
            this.cBoxIndexOutSimpleLine.Checked = false;
            this.btIndexSimpleLine.Enabled = false;
            this.cBoxIndexNeatLine.Checked = false;
            this.btIndexNeatLine.Enabled = false;
            this.cBoxIndexAttributeOne.Checked = false;
            this.cBoxIndexAttributeTwo.Checked = true;
        }

        //打开界面自动加载
        private void FrmNewGrids_Load(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(401, 360);
            //向导大小设置
            this.WizardGrid.Size = this.ClientSize;

            //设置第一界面
            setFirstFrame();

            //设置第二界面
            setSecondFrame();

            //设置第三界面
            setThirdFrame();

            //设置第四界面
            setFourFrame();
        }

        #region //格网类型选择,以及格网名称,第一界面的操作
        //格网类型选择
        private void cBGraticule_CheckedChanged(object sender, EventArgs e)
        {
            //经纬格网
            //cBGraticule.Checked = true;
            //cBMeasured.Checked = false;
            //cBIndex.Checked = false;

            if (cBGraticule.Checked == true)
            {
                //新建经纬格网对象
                m_pMapGrid = new GraticuleClass();

                //第二界面可见性
                gPanelGraticule.Visible = true;
                gPanelMeasured.Visible = false;
                gPanelIndex.Visible = false;
                setSecondFrame();

                //第三界面可见性
                gPanelGMAxeAndFont.Visible = true;
                gPanelIndexAxeAndFont.Visible = false;

                //第四界面可见性
                gPanelGBorder.Visible = true;
                gPanelMBorder.Visible = false;
                gPanelIndexBorder.Visible = false;

                //确定格网名称
                txtGridName.Text = getDefaultNewGridName();
            }
        }

        private void cBMeasured_CheckedChanged(object sender, EventArgs e)
        {
            //地理网格
            //cBMeasured.Checked = true;
            //cBGraticule.Checked = false;
            //cBIndex.Checked = false;

            if (cBMeasured.Checked == true)
            {
                //新建地理网格对象
                m_pMapGrid = new MeasuredGridClass();

                //第二界面可见性
                gPanelGraticule.Visible = false;
                gPanelMeasured.Visible = true;
                gPanelIndex.Visible = false;
                setSecondFrame();

                //第三界面可见性
                gPanelGMAxeAndFont.Visible = true;
                gPanelIndexAxeAndFont.Visible = false;

                //第四界面可见性
                gPanelGBorder.Visible = false;
                gPanelMBorder.Visible = true;
                gPanelIndexBorder.Visible = false;

                //确定格网名称
                txtGridName.Text = getDefaultNewGridName();
            }            
        }

        private void cBIndex_CheckedChanged(object sender, EventArgs e)
        {
            //索引网格
            //cBIndex.Checked = true;
            //cBGraticule.Checked = false;
            //cBMeasured.Checked = false;

            if (cBIndex.Checked == true)
            {
                //新建索引风格对象
                m_pMapGrid = new IndexGridClass();

                //设置默认颜色
                IColor pColor = new RgbColorClass();
                IRgbColor pRgbColor = pColor as IRgbColor;
                pRgbColor.Red = 255;
                pRgbColor.Blue = 0;
                pRgbColor.Green = 0;
                m_pMapGrid.LabelFormat.Color = pColor;

                //第二界面可见性
                gPanelGraticule.Visible = false;
                gPanelMeasured.Visible = false;
                gPanelIndex.Visible = true;
                setSecondFrame();

                //第三界面可见性
                gPanelGMAxeAndFont.Visible = false;
                gPanelIndexAxeAndFont.Visible = true;

                //第四界面可见性
                gPanelGBorder.Visible = false;
                gPanelMBorder.Visible = false;
                gPanelIndexBorder.Visible = true;

                //确定格网名称
                txtGridName.Text = getDefaultNewGridName();
            }
            
        }
        #endregion

        #region 设计格网的显示内容和间隔，第二界面的操作

        //经纬网显示内容

        //显示标注
        private void cBGLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cBGLabel.Checked == true)
            {
                this.btGSymbol.Enabled = false;
                this.btGSymbol.Image = null;
            }
        }

        //显示标注和刻度
        private void cBGTickandLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cBGTickandLabel.Checked == true)
            {
                this.btGSymbol.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                m_pMapGrid.TickMarkSymbol = m_GraphicProperties.MarkerSymbol;

                if (btGSymbol.Image != null)
                    btGSymbol.Image.Dispose();
                this.btGSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.MarkerSymbol, btGSymbol.Width, btGSymbol.Height);
            }
        }

        //显示格网和标注
        private void cBGGraticuleandLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cBGGraticuleandLabel.Checked == true)
            {                
                this.btGSymbol.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                m_pMapGrid.LineSymbol = m_GraphicProperties.LineSymbol;

                if (btGSymbol.Image != null)
                    btGSymbol.Image.Dispose();
                this.btGSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btGSymbol.Width, btGSymbol.Height);
            }
        }

        //公里格网显示内容
        //显示标注
        private void cBMLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cBMLabel.Checked == true)
            {
                this.btMSymbol.Enabled = false;
                this.btMSymbol.Image = null;
            }
        }

        //显示标注和刻度
        private void cBMTickandLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cBMTickandLabel.Checked == true)
            {
                this.btMSymbol.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                m_pMapGrid.TickMarkSymbol = m_GraphicProperties.MarkerSymbol;

                if (btMSymbol.Image != null)
                    btMSymbol.Image.Dispose();
                this.btMSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.MarkerSymbol, btMSymbol.Width, btMSymbol.Height);
            }
        }

        //显示格网和标注
        private void cBMMeasuredandLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cBMMeasuredandLabel.Checked == true)
            {
                this.btMSymbol.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                m_pMapGrid.LineSymbol = m_GraphicProperties.LineSymbol;

                if (btMSymbol.Image != null)
                    btMSymbol.Image.Dispose();
                this.btMSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btMSymbol.Width, btMSymbol.Height);
            }
        }

        //索引格网显示内容
        //显示标注
        private void cBIndexLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cBIndexLabel.Checked == true)
            {
                this.btIndexSymbol.Enabled = false;
                this.btIndexSymbol.Image = null;
            }
        }

        //显示格网和标注
        private void cBIndexGridandLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cBIndexGridandLabel.Checked == true)
            {
                this.btIndexSymbol.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                m_pMapGrid.LineSymbol = m_GraphicProperties.LineSymbol;

                if (btIndexSymbol.Image != null)
                    btIndexSymbol.Image.Dispose();
                this.btIndexSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btIndexSymbol.Width, btIndexSymbol.Height);
            }
        }

        //第二界面中三个内部标记样式的选择
        //经纬度样式选择
        private void btGSymbol_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();

            if (cBGTickandLabel.Checked == true)
            {
                //刻度样式选择
                FrmSymbol TickSymbol = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.MarkerSymbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);

                if (TickSymbol.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem StyleGalleryItem = TickSymbol.GetStyleGalleryItem();
                    if (StyleGalleryItem != null)
                    {
                        m_pMapGrid.TickMarkSymbol = StyleGalleryItem.Item as IMarkerSymbol;
                        btGSymbol.Image = TickSymbol.GetImageByGiveSymbolAfterSelectItem(btGSymbol.Width, btGSymbol.Height);
                    }
                }
            }
            else if (cBGGraticuleandLabel.Checked == true)
            {
                //经纬网样式选择
                FrmSymbol GraticuleSymbol = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                if (GraticuleSymbol.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem StyleGalleryItem = GraticuleSymbol.GetStyleGalleryItem();
                    if (StyleGalleryItem != null)
                    {
                        m_pMapGrid.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                        btGSymbol.Image = GraticuleSymbol.GetImageByGiveSymbolAfterSelectItem(btGSymbol.Width, btGSymbol.Height);
                    }
                }
            }
            else
                return;
        }
        //公里格网样式选择
        private void btMSymbol_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            if (cBMTickandLabel.Checked == true)
            {
                //刻度样式选择
                FrmSymbol TickSymbol = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.MarkerSymbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);

                if (TickSymbol.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem StyleGalleryItem = TickSymbol.GetStyleGalleryItem();
                    if (StyleGalleryItem != null)
                    {
                        m_pMapGrid.TickMarkSymbol = StyleGalleryItem.Item as IMarkerSymbol;
                        btMSymbol.Image = TickSymbol.GetImageByGiveSymbolAfterSelectItem(btMSymbol.Width, btMSymbol.Height);
                    }
                }
            }
            else if (cBMMeasuredandLabel.Checked == true)
            {
                //公里网格样式选择
                FrmSymbol MeasuredSymbol = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                if (MeasuredSymbol.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem StyleGalleryItem = MeasuredSymbol.GetStyleGalleryItem();
                    if (StyleGalleryItem != null)
                    {
                        m_pMapGrid.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                        btMSymbol.Image = MeasuredSymbol.GetImageByGiveSymbolAfterSelectItem(btMSymbol.Width, btMSymbol.Height);
                    }
                }
            }
            else
                return;
        }
        //索引格网样式选择
        private void btIndexSymbol_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            if (cBIndexGridandLabel.Checked == true)
            {
                //索引网格样式选择
                FrmSymbol IndexSymbol = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                if (IndexSymbol.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem StyleGalleryItem = IndexSymbol.GetStyleGalleryItem();
                    if (StyleGalleryItem != null)
                    {
                        m_pMapGrid.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                        btIndexSymbol.Image = IndexSymbol.GetImageByGiveSymbolAfterSelectItem(btIndexSymbol.Width, btIndexSymbol.Height);
                    }
                }
            }
        }

        #endregion

        #region 第三界面中字体样式和经纬网、公里格网的长轴、短轴刻度样式选择

        //长轴显示问题
        private void cBoxLongAxe_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxLongAxe.Checked == true)
            {
                btLongLine.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                m_pMapGrid.TickLineSymbol = m_GraphicProperties.LineSymbol;

                if (btLongLine.Image != null)
                    btLongLine.Image.Dispose();
                this.btLongLine.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btLongLine.Width, btLongLine.Height);
            }
            else
            {
                btLongLine.Enabled = false;
                btLongLine.Image = null;
                //FrmSymbol Frm = new FrmSymbol(Symbolstyle, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                //if (btLongLine.Image != null)
                //    btLongLine.Image.Dispose();
                //this.btLongLine.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, (ISymbol)m_GraphicProperties.LineSymbol, btLongLine.Width, btLongLine.Height);
            }


        }

        //短轴显示问题
        private void cBoxShortAxe_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxShortAxe.Checked == true)
            {
                btShortLine.Enabled = true;
                LongAxeNumber.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                m_pMapGrid.SubTickLineSymbol = m_GraphicProperties.LineSymbol;

                //FrmSymbol Frm = new FrmSymbol(Symbolstyle, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);
                if (this.btShortLine.Image != null)
                    this.btShortLine.Image.Dispose();

                this.btShortLine.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btShortLine.Width, btShortLine.Height);
            }
            else
            {
                btShortLine.Enabled = false;
                LongAxeNumber.Enabled = false;
                btShortLine.Image = null;
                //FrmSymbol Frm = new FrmSymbol(Symbolstyle, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);
                //this.btShortLine.Image = Frm.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, (ISymbol)m_GraphicProperties.LineSymbol, btShortLine.Width, btShortLine.Height);
            }
        }

        //经纬网、公里格网的长轴刻度样式选择
        private void btLongLine_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            FrmSymbol GraticuleAxeSymbol = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

            if (GraticuleAxeSymbol.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem StyleGalleryItem = GraticuleAxeSymbol.GetStyleGalleryItem();
                if (StyleGalleryItem != null)
                {
                    m_pMapGrid.TickLineSymbol = StyleGalleryItem.Item as ILineSymbol;
                    btLongLine.Image = GraticuleAxeSymbol.GetImageByGiveSymbolAfterSelectItem(btLongLine.Width, btLongLine.Height);
                }
            }
        }

        //经纬网、公里格网的短轴刻度样式选择
        private void btShortLine_Click(object sender, EventArgs e)
        {
            //经纬网短轴刻度
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            FrmSymbol GraticuleAxeSymbol = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

            if (GraticuleAxeSymbol.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem StyleGalleryItem = GraticuleAxeSymbol.GetStyleGalleryItem();
                if (StyleGalleryItem != null)
                {
                    m_pMapGrid.SubTickLineSymbol = StyleGalleryItem.Item as ILineSymbol;
                    btShortLine.Image = GraticuleAxeSymbol.GetImageByGiveSymbolAfterSelectItem(btShortLine.Width, btShortLine.Height);
                }
            }
        }

        //经纬网、公里格网的文本样式
        private void btTextSymbol_Click(object sender, EventArgs e)
        {
            FrmTextSymbol FrmGraticuleTextSymbol = new FrmTextSymbol();

            if (FrmGraticuleTextSymbol.ShowDialog() == DialogResult.OK)
            {
                ITextSymbol pTextSymbol = FrmGraticuleTextSymbol.GetTextSymbol();

                IFontDisp pFont = new StdFontClass() as IFontDisp;
                pFont.Bold = pTextSymbol.Font.Bold;
                pFont.Charset = pTextSymbol.Font.Charset;
                pFont.Italic = pTextSymbol.Font.Italic;
                pFont.Name = pTextSymbol.Font.Name;
                pFont.Size = pTextSymbol.Font.Size;
                pFont.Strikethrough = pTextSymbol.Font.Strikethrough;
                pFont.Underline = pTextSymbol.Font.Underline;
                pFont.Weight = pTextSymbol.Font.Weight;

                m_pMapGrid.LabelFormat.Font = pFont;// pTextSymbol.Font;

                IClone pSrcClone=pTextSymbol.Color as IClone;
                IClone pDstClone=pSrcClone.Clone();
                IColor pDstColor=pDstClone as IColor;
                m_pMapGrid.LabelFormat.Color = pDstColor;
            }
        }

        //索引格网的文本样式
        private void btIndexFont_Click(object sender, EventArgs e)
        {
            //公里格网字体样式
            FrmTextSymbol FrmIndexTextSymbol = new FrmTextSymbol();

            if (FrmIndexTextSymbol.ShowDialog() == DialogResult.OK)
            {
                ITextSymbol pTextSymbol = FrmIndexTextSymbol.GetTextSymbol();

                IFontDisp pFont = new StdFontClass() as IFontDisp;
                pFont.Bold = pTextSymbol.Font.Bold;
                pFont.Charset = pTextSymbol.Font.Charset;
                pFont.Italic = pTextSymbol.Font.Italic;
                pFont.Name = pTextSymbol.Font.Name;
                pFont.Size = pTextSymbol.Font.Size;
                pFont.Strikethrough = pTextSymbol.Font.Strikethrough;
                pFont.Underline = pTextSymbol.Font.Underline;
                pFont.Weight = pTextSymbol.Font.Weight;
                m_pMapGrid.LabelFormat.Font = pFont;// pTextSymbol.Font;

                IClone pSrcClone = pTextSymbol.Color as IClone;
                IClone pDstClone = pSrcClone.Clone();
                IColor pDstColor = pDstClone as IColor;

                m_pMapGrid.LabelFormat.Color = pDstColor;
            }
        }

        //索引格网选项卡配置
        private void cBoxABC123_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxABC123.Checked == true)
            {
                cBoxABC123.Checked = true;
                cBox123ABC.Checked = false;
            }
            else
            {
                cBoxABC123.Checked = false;
                cBox123ABC.Checked = true;
            }
        }
        #endregion

        #region 第四界面

        //经纬网属性界面设计
        private void cBoxGOutSimpleLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxGOutSimpleLine.Checked == true)
            {
                btGSimpleLine.Enabled = true;
                //cBoxGOutGroomLine.Checked = false;
                btGGroom.Enabled = false;

                //缺省样式
                //FrmSymbol Frm = new FrmSymbol(Symbolstyle, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                ISimpleMapGridBorder pSimpleBorder = new SimpleMapGridBorderClass();
                pSimpleBorder.LineSymbol = m_GraphicProperties.LineSymbol;
                m_pMapGrid.Border = pSimpleBorder as IMapGridBorder;

                if (this.btGSimpleLine.Image != null)
                    this.btGSimpleLine.Image.Dispose();

                this.btGSimpleLine.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btGSimpleLine.Width, btGSimpleLine.Height);
            }
            
        }

        private void cBoxGNeatLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxGNeatLine.Checked == true)
            {
                btGNeatLine.Enabled = true;

                //缺省样式
                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();

                ISymbolBorder pSymbolBorder = new SymbolBorderClass();
                pSymbolBorder.LineSymbol = m_GraphicProperties.LineSymbol;
                pSymbolBorder.Gap = 23;

                IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;
                pMapFrame.Border = pSymbolBorder as IBorder;

                if (btGNeatLine.Image != null)
                    btGNeatLine.Image.Dispose();
                this.btGNeatLine.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btGNeatLine.Width, btGNeatLine.Height);
            }
            else
            {
                btGNeatLine.Enabled = false;
                btGNeatLine.Image = null;

                IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;
                pMapFrame.Border = null;
            }
        }

        private void cBoxGAttributeOne_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxGAttributeOne.Checked == true)
            {
                cBoxGAttributeOne.Checked = true;
                cBoxGAttributeTwo.Checked = false;
            }
            else
            {
                cBoxGAttributeTwo.Checked = true;
                cBoxGAttributeOne.Checked = false;
            }
        }

        //样式//边框
        private void btGSimpleLine_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            FrmSymbol GraticuleOutSimpleLine = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

            if (GraticuleOutSimpleLine.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem StyleGalleryItem = GraticuleOutSimpleLine.GetStyleGalleryItem();
                if (StyleGalleryItem != null)
                {
                    ISimpleMapGridBorder pSimpleBorder = m_pMapGrid.Border as ISimpleMapGridBorder;
                    pSimpleBorder.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                    btGSimpleLine.Image = GraticuleOutSimpleLine.GetImageByGiveSymbolAfterSelectItem(btGSimpleLine.Width, btGSimpleLine.Height);
                }
            }
        }

        //整饰边框
        private void btGGroom_Click(object sender, EventArgs e)
        {
            FrmCalibGridBorder FrmCali = new FrmCalibGridBorder();
            if (FrmCali.ShowDialog() == DialogResult.OK)
            {
                m_pMapGrid.Border = FrmCali.GetGridBorder() as IMapGridBorder;
            }
        }

        //内图廓线
        private void btGNeatLine_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            FrmSymbol GraticuleNeatLiness = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

            if (GraticuleNeatLiness.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem StyleGalleryItem = GraticuleNeatLiness.GetStyleGalleryItem();
                if (StyleGalleryItem != null)
                {
                    IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;

                    ISymbolBorder pSymbolBorder = new SymbolBorderClass();
                    pSymbolBorder.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                    pMapFrame.Border = pSymbolBorder;

                    btGNeatLine.Image = GraticuleNeatLiness.GetImageByGiveSymbolAfterSelectItem(btGNeatLine.Width, btGNeatLine.Height);
                }
            }
        }

        //公里格网属性界面设计
        private void cBoxMOutSimpleLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxMOutSimpleLine.Checked == true)
            {
                btMSimpleLine.Enabled = true;

                //缺省样式       
                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                ISimpleMapGridBorder pSimpleMapGridBorder = new SimpleMapGridBorderClass();
                pSimpleMapGridBorder.LineSymbol = m_GraphicProperties.LineSymbol;
                m_pMapGrid.Border = pSimpleMapGridBorder as IMapGridBorder;

                if (this.btMSimpleLine.Image != null)
                    this.btMSimpleLine.Image.Dispose();
                this.btMSimpleLine.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btMSimpleLine.Width, btMSimpleLine.Height);
            }
            else
            {
                btMSimpleLine.Enabled = false;
                btMSimpleLine.Image = null;
            }
        }

        private void cBoxMNeatLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxMNeatLine.Checked == true)
            {
                btMNeatLine.Enabled = true;

                //缺省样式
                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;

                ISymbolBorder pSymbolBorder = new SymbolBorderClass();
                pSymbolBorder.LineSymbol = m_GraphicProperties.LineSymbol;
                pSymbolBorder.Gap = 23;
                pMapFrame.Border = pSymbolBorder;

                if (this.btMNeatLine.Image != null)
                    this.btMNeatLine.Image.Dispose();
                this.btMNeatLine.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btMNeatLine.Width, btMNeatLine.Height);
            }
            else
            {
                btMNeatLine.Enabled = false;
                btMNeatLine.Image = null;

                IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;
                pMapFrame.Border = null;
            }
        }

        private void cBoxMAttributeOne_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxMAttributeOne.Checked == true)
            {
                cBoxMAttributeOne.Checked = true;
                cBoxMAttributeTwo.Checked = false;
            }
            else
            {
                cBoxMAttributeTwo.Checked = true;
                cBoxMAttributeOne.Checked = false;
            }
        }

        //边框
        private void btMSimpleLine_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            FrmSymbol MeasuredOutSimpleLine = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

            if (MeasuredOutSimpleLine.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem StyleGalleryItem = MeasuredOutSimpleLine.GetStyleGalleryItem();
                if (StyleGalleryItem != null)
                {
                    ISimpleMapGridBorder pSimpleBorder = m_pMapGrid.Border as ISimpleMapGridBorder;
                    pSimpleBorder.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                    btMSimpleLine.Image = MeasuredOutSimpleLine.GetImageByGiveSymbolAfterSelectItem(btMSimpleLine.Width, btMSimpleLine.Height);
                }
            }
        }

        //内图廓线
        private void btMNeatLine_Click(object sender, EventArgs e)
        {
             IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            FrmSymbol MeasuredNeatLiness = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

            if (MeasuredNeatLiness.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem StyleGalleryItem = MeasuredNeatLiness.GetStyleGalleryItem();
                if (StyleGalleryItem != null)
                {
                    IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;

                    ISymbolBorder pSymbolBorder = new SymbolBorderClass();
                    pSymbolBorder.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                    pMapFrame.Border = pSymbolBorder;
                    btMNeatLine.Image = MeasuredNeatLiness.GetImageByGiveSymbolAfterSelectItem(btMNeatLine.Width, btMNeatLine.Height);
                }
            }
        }

        //索引格网属性界面设计
        private void cBoxIndexOutSimpleLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxIndexOutSimpleLine.Checked == true)
            {
                btIndexSimpleLine.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                ISimpleMapGridBorder pSimpleBorder = new SimpleMapGridBorderClass();
                pSimpleBorder.LineSymbol = m_GraphicProperties.LineSymbol;
                m_pMapGrid.Border = pSimpleBorder as IMapGridBorder;
                this.btIndexSimpleLine.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btIndexSimpleLine.Width, btIndexSimpleLine.Height);
            }
            else
            {
                btIndexSimpleLine.Enabled = false;
                btIndexSimpleLine.Image = null;
            }
        }

        private void cBoxIndexNeatLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxIndexNeatLine.Checked == true)
            {
                btIndexNeatLine.Enabled = true;

                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();

                IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;

                ISymbolBorder pSymbolBorder = new SymbolBorderClass();
                pSymbolBorder.LineSymbol = m_GraphicProperties.LineSymbol;
                pSymbolBorder.Gap = 23;
                pMapFrame.Border = pSymbolBorder;

                if (btIndexNeatLine.Image != null)
                    btIndexNeatLine.Image.Dispose();
                this.btIndexNeatLine.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btIndexNeatLine.Width, btIndexNeatLine.Height);
            }
            else
            {
                btIndexNeatLine.Enabled = false;
                btIndexNeatLine.Image = null;


                IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;
                pMapFrame.Border = null;
            }
        }

        private void cBoxIndexAttributeOne_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxIndexAttributeOne.Checked == true)
            {
                cBoxIndexAttributeOne.Checked = true;
                cBoxIndexAttributeTwo.Checked = false;
            }
            else
            {
                cBoxIndexAttributeOne.Checked = false;
                cBoxIndexAttributeTwo.Checked = true;
            }
        }

        //边框
        private void btIndexSimpleLine_Click(object sender, EventArgs e)
        {
            if (cBoxIndexOutSimpleLine.Checked == true)
            {
                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                FrmSymbol IndexOutSimpleLine = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);
                if (IndexOutSimpleLine.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem StyleGalleryItem = IndexOutSimpleLine.GetStyleGalleryItem();
                    if (StyleGalleryItem != null)
                    {
                        ISimpleMapGridBorder pSimpleBorder = m_pMapGrid.Border as ISimpleMapGridBorder;
                        pSimpleBorder.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                        btIndexSimpleLine.Image = IndexOutSimpleLine.GetImageByGiveSymbolAfterSelectItem(btIndexSimpleLine.Width, btIndexSimpleLine.Height);
                    }
                }
            }
        }

        //内图廓线
        private void btIndexNeatLine_Click(object sender, EventArgs e)
        {
            IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
            FrmSymbol IndexNeatLiness = new FrmSymbol(m_pSymbolStyles, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

            if (IndexNeatLiness.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem StyleGalleryItem = IndexNeatLiness.GetStyleGalleryItem();
                if (StyleGalleryItem != null)
                {

                    IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;

                    ISymbolBorder pSymbolBorder = new SymbolBorderClass();
                    pSymbolBorder.LineSymbol = StyleGalleryItem.Item as ILineSymbol;
                    pMapFrame.Border = pSymbolBorder;
                    btIndexNeatLine.Image = IndexNeatLiness.GetImageByGiveSymbolAfterSelectItem(btIndexNeatLine.Width, btIndexNeatLine.Height);
                }
            }
        }

        #endregion

        //取消按钮
        private void WizardGrid_CancelButtonClick(object sender, CancelEventArgs e)
        {
            m_pMapGrid = null;

            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //完成按钮
        private void WizardGrid_FinishButtonClick(object sender, CancelEventArgs e)
        {
            try
            {
                //创建格网
                if (m_pMapGrid is IGraticule)
                {
                    //创建经纬网
                    CreateGraticuleGrids();
                }
                else if (m_pMapGrid is IMeasuredGrid)
                {
                    //创建地理格网
                    CreateMeasuredGrids();
                }
                else if (m_pMapGrid is IIndexGrid)
                {
                    //创建索引格网
                    CreateIndexGrids();
                }
                else
                    m_pMapGrid = null;

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //创建经纬网
        private void CreateGraticuleGrids()
        {
            try
            {
                

                //建立经纬度网格
                IMeasuredGrid pMeasuredGrid = m_pMapGrid as IMeasuredGrid;
                pMeasuredGrid.FixedOrigin = false;

                pMeasuredGrid.Units = esriUnits.esriDecimalDegrees;
                pMeasuredGrid.XIntervalSize = double.Parse(txtGXIntervalD.Text) + double.Parse(txtGXIntervalF.Text) / 60 + double.Parse(txtGXIntervalM.Text) / 3600;
                pMeasuredGrid.YIntervalSize = double.Parse(txtGYIntervalD.Text) + double.Parse(txtGYIntervalF.Text) / 60 + double.Parse(txtGYIntervalM.Text) / 3600;

                //设定为0时会卡死，如果设定为零，给一个默认值
                if (pMeasuredGrid.XIntervalSize == 0)
                {
                    pMeasuredGrid.XIntervalSize = 1;
                }
                if (pMeasuredGrid.YIntervalSize == 0)
                {
                    pMeasuredGrid.YIntervalSize = 1;
                }

                m_pMapGrid.Name = txtGridName.Text;

                //显示参数选择
                if (cBGLabel.Checked == true)
                {
                    m_pMapGrid.LineSymbol = null;
                    m_pMapGrid.TickMarkSymbol = null;
                }
                else if (cBGTickandLabel.Checked == true)
                {
                    m_pMapGrid.LineSymbol = null;
                }
                else if (cBGGraticuleandLabel.Checked == true)
                {
                    m_pMapGrid.TickMarkSymbol = null;
                }
                else
                {
                    m_pMapGrid.LineSymbol = null;
                    m_pMapGrid.TickMarkSymbol = null;
                }

                //轴线设置
                m_pMapGrid.SubTickCount = Convert.ToInt16(LongAxeNumber.Value);

                //显示标注
                m_pMapGrid.SetLabelVisibility(true, true, true, true);
                m_pMapGrid.SetSubTickVisibility(true, true, true, true);
                m_pMapGrid.SetTickVisibility(true, true, true, true);
                m_pMapGrid.Visible = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        //创建地理格网
        private void CreateMeasuredGrids()
        {

            IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            IMap pMap = getMap();

            IMeasuredGrid pMeasuredGrid = m_pMapGrid as IMeasuredGrid;
            pMeasuredGrid.Units = pMap.MapUnits;

            //格网名称
            m_pMapGrid.Name = txtGridName.Text;

            //格网间隔
            pMeasuredGrid.FixedOrigin = false;
            pMeasuredGrid.XIntervalSize = double.Parse(txtMXinterval.Text);
            pMeasuredGrid.YIntervalSize = double.Parse(txtMYinterval.Text);

            //IProjectedGrid pProjectedGrid = pMeasuredGrid as IProjectedGrid;
            //pProjectedGrid.SpatialReference = pMap.SpatialReference;

            //显示参数选择
            if (cBMLabel.Checked == true)
            {
                m_pMapGrid.TickMarkSymbol = null;
                m_pMapGrid.LineSymbol = null;
            }
            else if (cBMTickandLabel.Checked == true)
            {
                m_pMapGrid.LineSymbol = null;
            }
            else if (cBMMeasuredandLabel.Checked == true)
            {
                m_pMapGrid.TickMarkSymbol = null;
            }
            else
            {
                m_pMapGrid.LineSymbol = null;
                m_pMapGrid.TickMarkSymbol = null;
            }

            //轴线设置
            m_pMapGrid.SubTickCount = Convert.ToInt16(LongAxeNumber.Value);

            //显示标注
            m_pMapGrid.SetLabelVisibility(true, true, true, true);
            m_pMapGrid.LabelFormat.set_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft, false);
            m_pMapGrid.LabelFormat.set_LabelAlignment(esriGridAxisEnum.esriGridAxisRight, false);
            m_pMapGrid.SetSubTickVisibility(true, true, true, true);
            m_pMapGrid.SetTickVisibility(true, true, true, true);

            m_pMapGrid.Visible = true;
        }

        //建立索引格网
        private void CreateIndexGrids()
        {

            IMapFrame pMapFrame = getMapFrame();// pGraphicsContainer.FindFrame(pMap) as IMapFrame;

            //建立索引格网
            IIndexGrid pIndexGrid = m_pMapGrid as IIndexGrid;// new IndexGridClass();
            pIndexGrid.Name = txtGridName.Text;

            pIndexGrid.ColumnCount = Convert.ToInt32(IndexXInterval.Value);// int.Parse(double.Parse().ToString());
            pIndexGrid.RowCount = Convert.ToInt32(IndexYInterval.Value);// int.Parse(double.Parse().ToString());

            //前景颜色
            IClone pSrcClone = m_pMapGrid.LabelFormat.Color as IClone;
            IClone pDstClone = pSrcClone.Clone();
            IColor pDstColor = pDstClone as IColor;
            m_pMapGrid.LabelFormat = getIndexGridLabelFormat();
            m_pMapGrid.LabelFormat.Color = pDstColor;

            //显示参数选择
            if (cBIndexLabel.Checked == true)
            {
                pIndexGrid.TickMarkSymbol = null;
                pIndexGrid.LineSymbol = null;
            }
            else if (cBIndexGridandLabel.Checked == true)
            {
                pIndexGrid.TickMarkSymbol = null;
            }
            else
            {
                pIndexGrid.TickMarkSymbol = null;
                pIndexGrid.LineSymbol = null;
            }

            #region XY轴标注内容
            //XY轴标注内容
            if (cBoxABC123.Checked == true)
            {
                int pLabelX = Convert.ToInt32(IndexXInterval.Value);
                int pLabelY = Convert.ToInt32(IndexYInterval.Value);// double.Parse(IndexYInterval.Text);
                
                for (int i = 0; i < pLabelX; i++)
                {
                    pIndexGrid.set_XLabel(i, CommonProcess.indexToString(i+1));
                }

                for (int i = 0; i < pLabelY; i++)
                {
                    pIndexGrid.set_YLabel(i, (i + 1).ToString());
                }
            }
            else if (cBox123ABC.Checked == true)
            {
                int pLabelX = Convert.ToInt32(IndexXInterval.Value);
                int pLabelY = Convert.ToInt32(IndexYInterval.Value);
               
                for (int i = 0; i < pLabelY; i++)
                {
                    pIndexGrid.set_YLabel(i, CommonProcess.indexToString(i+1));
                }

                for (int i = 0; i < pLabelX; i++)
                {
                    pIndexGrid.set_XLabel(i, (i + 1).ToString());
                }
            }
            #endregion

            //显示标注
            pIndexGrid.SetLabelVisibility(true, true, true, true);
            pIndexGrid.SetSubTickVisibility(true, true, true, true);
            pIndexGrid.SetTickVisibility(true, true, true, true);
            pIndexGrid.Visible = true;
        }

        //////返回建立文件
        //public IMapGrid GetMapGrid()
        //{
        //    return pMapGrid;
        //}

        private void cBoxGOutGroomLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxGOutGroomLine.Checked == true)
            {                
                btGGroom.Enabled = true;
                //cBoxGOutSimpleLine.Checked = false;
                btGSimpleLine.Enabled = false;
                btGSimpleLine.Image = null;

                ICalibratedMapGridBorder pCalibratedBorder = new CalibratedMapGridBorderClass();
                m_pMapGrid.Border = pCalibratedBorder as IMapGridBorder;
            }
            
        }

        private IGridLabel getIndexGridLabelFormat()
        {
            IGridLabel pGridLabel = null;
            if (m_pMapGrid is IIndexGrid)
            {
                //选项卡样式
                if (cbBoxIndexTab.Text == "按钮选项卡")
                {
                    IIndexGridTabStyle pIndexTabButton = new ButtonTabStyleClass();
                    pIndexTabButton.ForegroundColor = ClsGDBDataCommon.ColorToIColor(IndexColor.SelectedColor);
                    pIndexTabButton.Thickness = 20;
                    pGridLabel = pIndexTabButton as IGridLabel;
                }
                else if (cbBoxIndexTab.Text == "连续选项卡")
                {
                    IIndexGridTabStyle pIndexTabContinuou = new ContinuousTabStyleClass();

                    pIndexTabContinuou.ForegroundColor = ClsGDBDataCommon.ColorToIColor(IndexColor.SelectedColor);
                    pIndexTabContinuou.Thickness = 20;
                    pGridLabel = pIndexTabContinuou as IGridLabel;
                }
                else if (cbBoxIndexTab.Text == "填充的背景")
                {
                    IIndexGridTabStyle pIndexTabBackground = new BackgroundTabStyleClass();

                    pIndexTabBackground.ForegroundColor = ClsGDBDataCommon.ColorToIColor(IndexColor.SelectedColor);
                    pIndexTabBackground.Thickness = 20;
                    pGridLabel = pIndexTabBackground as IGridLabel;

                }
                else if (cbBoxIndexTab.Text == "圆角选项卡")
                {
                    IIndexGridTabStyle pIndexTabRounded = new RoundedTabStyleClass();

                    pIndexTabRounded.ForegroundColor = ClsGDBDataCommon.ColorToIColor(IndexColor.SelectedColor);
                    pIndexTabRounded.Thickness = 20;
                    pGridLabel = pIndexTabRounded as IGridLabel;

                }
                else
                    pGridLabel = null;
            }

            return pGridLabel;
        }
    }
}
