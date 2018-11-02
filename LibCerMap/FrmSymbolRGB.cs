using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

using DevComponents.DotNetBar;


namespace LibCerMap
{
    public partial class FrmSymbolRGB : Form
    {
        IRasterLayer pRLayer = null;
        AxMapControl pMapContral = null;
        AxTOCControl pTocContral = null;
        AxSceneControl pSceneControl = null;
        IRaster pRaster = null;
        IRasterBandCollection pRBCollection=null;//将用于数据集得到的band集合，判断是否为3波段以上的栅格
        IRasterBandCollection pRbandCollection = null;
        IRasterRGBRenderer2 pRRGBRender = null;
        IRasterBand pRBand = null;
        IRasterRenderer pRrender = null;

        //streach相关
        ISymbologyStyleClass pSymbolClass = null;
        IRasterRenderer pRasterRender = null;
        IRasterStretch pRasterStretch = null;
        IRasterStretch pRStretch = new RasterStretchColorRampRendererClass();
        IRasterRendererColorRamp pRasterRenderColorRamp = null;
        IRasterStretchColorRampRenderer pSCRampRender = null;
        IRasterStretchMinMax pRasterStretchMinMax = null;
        IColorRamp PColorramp = null;
        bool isminmax = false;//用于判定最大最小值选择次数


        public FrmSymbolRGB(AxMapControl pmapcontral, AxTOCControl ptoccontral, IRasterLayer pLayer, AxSceneControl sceneControl)
        {
            InitializeComponent();
            //this.EnableGlass = false;          
            pMapContral = pmapcontral;
            pTocContral = ptoccontral;
            pRLayer = pLayer;
            pSceneControl = sceneControl;
        }

        private void FrmSymbolRGB_Load(object sender, EventArgs e)
        {
            treeshow.SelectNode(treeshow.Nodes[1], DevComponents.AdvTree.eTreeAction.Code);
            //treeshow.SelectedIndex = 0; 
            string filepath = "";//记录栅格图的path
            filepath = pRLayer.FilePath;
            string pathinfo = System.IO.Path.GetDirectoryName(filepath);
            string filename = System.IO.Path.GetFileName(filepath);

            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            IWorkspace workspace;
            workspace = workspaceFactory.OpenFromFile(pathinfo,0);//栅格存储路径生成工作空间
            IRasterWorkspace rasterWork = (IRasterWorkspace)workspace;
            IRasterDataset rasterDatst;
            rasterDatst = rasterWork.OpenRasterDataset(filename);//栅格文件名生成数据集

            pRBCollection = rasterDatst as IRasterBandCollection;
            int pbandcount = pRBCollection.Count;
            pRrender = pRLayer.Renderer;
            if (pRrender is IRasterRGBRenderer2)
            {
                pRRGBRender = (IRasterRGBRenderer2)pRrender;
            }
            if (pbandcount <= 3)
            {
                pRaster = pRLayer.Raster;
                pRbandCollection = (IRasterBandCollection)pRaster;
                              
                int icount = pRbandCollection.Count;
                string[] BNameArray = new string[icount];

                for (int i = 0; i < 3; i++)
                {
                    gridsymbol.Rows.Add();
                }

                for (int j = 0; j < icount; j++)
                {
                    pRBand = pRbandCollection.Item(j);
                    BNameArray[j] = pRBand.Bandname;
                    //((DataGridViewComboBoxCell)gridsymbol.Rows[i].Cells[2]).Items.Add(BNameArray[j]);
                    cmbred.Items.Add(BNameArray[j]);
                    cmbblue.Items.Add(BNameArray[j]);
                    cmbgreen.Items.Add(BNameArray[j]);
                    cmbalpha.Items.Add(BNameArray[j]);

                    cmbBand.Items.Add(BNameArray[j]);//拉伸波段添加
                }
            } 
            else//多波段添加
            {
                //pRaster=pRLayer.Raster;
                //IRasterBandCollection pBandsCNew = pRaster as IRasterBandCollection;
                //IRasterBand pBand = null;
                //for (int i = 3; i < pbandcount;i++ )
                //{
                //    pBand = pRBCollection.Item(i);
                //    pBandsCNew.AppendBand(pBand);
                //}
                for (int i = 0; i < 3; i++)
                {
                    gridsymbol.Rows.Add();
                }

                for (int j = 0; j < pbandcount; j++)
                {
                    pRBand = pRBCollection.Item(j);

                    cmbred.Items.Add(pRBCollection.Item(j).Bandname);
                    cmbblue.Items.Add(pRBCollection.Item(j).Bandname);
                    cmbgreen.Items.Add(pRBCollection.Item(j).Bandname);
                    cmbalpha.Items.Add(pRBCollection.Item(j).Bandname);

                    cmbBand.Items.Add(pRBCollection.Item(j).Bandname);//拉伸波段添加
                }                
            }

            if (pRrender is IRasterRGBRenderer2)
            {
                cmbred.SelectedIndex = pRRGBRender.RedBandIndex;
                cmbgreen.SelectedIndex = pRRGBRender.GreenBandIndex;
                cmbblue.SelectedIndex = pRRGBRender.BlueBandIndex;
                cmbalpha.SelectedIndex = pRRGBRender.AlphaBandIndex;
            }

            gridsymbol.Rows[0].Cells[0].Value = true;
            gridsymbol.Rows[1].Cells[0].Value = true;
            gridsymbol.Rows[2].Cells[0].Value = true;
            gridsymbol.Rows[3].Cells[0].Value = false;
            gridsymbol.Rows[0].Cells[1].Value = "Red";
            gridsymbol.Rows[1].Cells[1].Value = "Green";
            gridsymbol.Rows[2].Cells[1].Value = "Blue";
            gridsymbol.Rows[3].Cells[1].Value = "Alpha";

            //((DataGridViewComboBoxCell)gridsymbol.Rows[0].Cells[2]).Value = pRRGBRender.RedBandIndex;
            //((DataGridViewComboBoxCell)gridsymbol.Rows[1].Cells[2]).Value = pRRGBRender.GreenBandIndex;
            //((DataGridViewComboBoxCell)gridsymbol.Rows[2].Cells[2]).Value = pRRGBRender.BlueBandIndex;  

            initstreach();//初始化与拉伸相关控件
        }
      
        /// <summary>
        /// 将DataGridViewComboBoxCell转化成ComBoBox类型
        /// </summary>
        /// <param name="datacmb"></param>
        /// <returns></returns>
        private ComboBox comDataCmbboxtoCmbbox(DataGridViewComboBoxCell datacmb)
        {
            ComboBox pcmbbox = new ComboBox();
            int pcount = datacmb.Items.Count;
            for (int i = 0; i < pcount;i++ )
            {
                pcmbbox.Items.Add(datacmb.Items[i]);
            }
            return pcmbbox;
        }

        private void gridsymbol_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnok_Click(object sender, EventArgs e)
        {
            btnuse_Click(sender, e);
            this.Close();
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnuse_Click(object sender, EventArgs e)
        {
            if (treeshow.SelectedIndex == 1)
            {
                IRasterRGBRenderer2 pRRGBRenderUse = new RasterRGBRendererClass();
                if ((bool)gridsymbol.Rows[0].Cells[0].Value == true)
                {
                    pRRGBRenderUse.UseRedBand = true;
                }
                else
                {
                    pRRGBRenderUse.UseRedBand = false;
                }

                if ((bool)gridsymbol.Rows[1].Cells[0].Value == true)
                {
                    pRRGBRenderUse.UseGreenBand = true;
                }
                else
                {
                    pRRGBRenderUse.UseGreenBand = false;
                }
                if ((bool)gridsymbol.Rows[2].Cells[0].Value == true)
                {
                    pRRGBRenderUse.UseBlueBand = true;
                }
                else
                {
                    pRRGBRenderUse.UseBlueBand = false;
                }
                if ((bool)gridsymbol.Rows[3].Cells[0].Value == true)
                {
                    pRRGBRenderUse.UseAlphaBand = true;
                }
                else
                {
                    pRRGBRenderUse.UseAlphaBand = false;
                }

                pRRGBRenderUse.RedBandIndex = cmbred.SelectedIndex;
                pRRGBRenderUse.GreenBandIndex = cmbgreen.SelectedIndex;
                pRRGBRenderUse.BlueBandIndex = cmbblue.SelectedIndex;
                pRRGBRenderUse.AlphaBandIndex = cmbalpha.SelectedIndex;

                pRLayer.Renderer = (IRasterRenderer)pRRGBRenderUse;
            }
            else
            {
                if (pRStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum)
                {
                    if (txtmin.Text == "" && txtmax.Text != "")
                    {
                        MessageBox.Show("请设置拉伸的最小值", "提示", MessageBoxButtons.OK);
                    }
                    else if (txtmax.Text == "" && txtmin.Text != "")
                    {
                        MessageBox.Show("请设置拉伸的最大值", "提示", MessageBoxButtons.OK);
                    }
                    else if (txtmax.Text == "" && txtmin.Text == "")
                    {
                        MessageBox.Show("请设置拉伸的最大值以及最小值", "提示", MessageBoxButtons.OK);
                    }
                    else
                    {
                        pRasterStretchMinMax = (IRasterStretchMinMax)pRStretch;
                        pRasterStretchMinMax.UseCustomStretchMinMax = true;
                        pRasterStretchMinMax.CustomStretchMin = double.Parse(txtmin.Text);
                        pRasterStretchMinMax.CustomStretchMax = double.Parse(txtmax.Text);
                        pRStretch = (IRasterStretch)pRasterStretchMinMax;
                        pRStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;
                    }
                }

                if (chkdisplay.Checked == true)
                {
                    pRStretch.Background = true;
                    pRStretch.BackgroundColor = ClsGDBDataCommon.ColorToIColor(colbackg.SelectedColor);
                    pRStretch.set_BackgroundValues(Convert.ToDouble(backvalue.Text));
                }
                else
                {
                    pRStretch.Background = false;
                }               

                IRasterStretchColorRampRenderer pStretchColorRasterRenderer = (IRasterStretchColorRampRenderer)pRStretch; ;
                IRasterRenderer pRasterRenderer = pStretchColorRasterRenderer as IRasterRenderer;
                pRasterRenderer.Raster = pRLayer.Raster;
                pRasterRenderer.Update();

                pStretchColorRasterRenderer.BandIndex = cmbBand.SelectedIndex;
                pStretchColorRasterRenderer.ColorRamp = PColorramp as IColorRamp;
                pRasterRenderer.Update();
                pRLayer.Renderer = pStretchColorRasterRenderer as IRasterRenderer;
            }


            if (pTocContral.Buddy == pMapContral.Object)
            {
                pTocContral.SetBuddyControl(pMapContral);
                pMapContral.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                pTocContral.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            else if (pTocContral.Buddy.Equals(pSceneControl.Object))
            {
                pTocContral.SetBuddyControl(pSceneControl);
                IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pRLayer, null);
                pTocContral.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            } 
        }

        private void treeshow_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
        {
            if (treeshow.SelectedIndex==1)
            {
                gridsymbol.Visible = true;
                cmbred.Visible = true;
                cmbgreen.Visible = true;
                cmbblue.Visible = true;
                cmbalpha.Visible = true;

                lblband.Visible = false;
                cmbBand.Visible = false;
                groupstr.Visible = false;
                lblColorRamp.Visible = false;
                cmbColorRamp.Visible = false;
                lblstreach.Visible = false;
                cmbStreach.Visible = false;
                chkdisplay.Visible = false;
                backvalue.Visible = false;
                backas.Visible = false;
                colbackg.Visible = false ;

            } 
            else
            {
                gridsymbol.Visible = false;
                cmbred.Visible = false;
                cmbgreen.Visible = false;
                cmbblue.Visible = false;
                cmbalpha.Visible = false;

                lblband.Visible = true;
                cmbBand.Visible = true;
                groupstr.Visible = true;
                lblColorRamp.Visible = true;
                cmbColorRamp.Visible = true;
                lblstreach.Visible = true;
                cmbStreach.Visible = true;
                chkdisplay.Visible = true;
                backvalue.Visible = true;
                backas.Visible = true;
                colbackg.Visible = true;
            }
        }

        /// <summary>
        /// 初始化与拉伸相关控件
        /// </summary>
        private void initstreach()
        {
            string sInstall = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyControl1.LoadStyleFile(sInstall);
            axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;
            pSymbolClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassColorRamps);
            pRasterRender = pRLayer.Renderer;
            pRasterRenderColorRamp = pRasterRender as IRasterRendererColorRamp;
            //生成色度带
            if (pRasterRender is IRasterStretchColorRampRenderer)
            {
                IStyleGalleryItem pStyleGalleryItem = new ServerStyleGalleryItem();
                pStyleGalleryItem.Item = pRasterRenderColorRamp.ColorRamp;
                pSymbolClass.AddItem(pStyleGalleryItem, 0);
                pSymbolClass.SelectItem(0);
            }
            for (int i = 0; i < pSymbolClass.get_ItemCount(pSymbolClass.StyleCategory); i++)
            {
                stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(pSymbolClass.GetItem(i), cmbColorRamp.Width, cmbColorRamp.Height);
                Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                cmbColorRamp.Items.Add(image);
            }

            if (cmbColorRamp.Items.Count > 0)
            {
                cmbColorRamp.SelectedIndex = 0;
            }
            //判断拉伸方式和拉伸波段
            if (pRasterRender is IRasterStretchColorRampRenderer)
            {
                pSCRampRender = pRasterRender as IRasterStretchColorRampRenderer;
                pRasterStretch = pRasterRender as IRasterStretch;
                int bandindex = pSCRampRender.BandIndex;
                cmbBand.SelectedIndex = bandindex;
                if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_Custom)
                {
                    cmbStreach.SelectedIndex = 1;
                }
                else if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum)
                {
                    cmbStreach.SelectedIndex = 2;
                }
                else if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations)
                {
                    cmbStreach.SelectedIndex = 3;
                }
                else if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize)
                {
                    cmbStreach.SelectedIndex = 4;
                }
                else
                {
                    cmbStreach.SelectedIndex = 0;
                }

                lblminstr.Text = pSCRampRender.LabelLow;
                lblmaxstr.Text = pSCRampRender.LabelHigh;
                txtmin.Text = pSCRampRender.LabelLow;
                txtmax.Text = pSCRampRender.LabelHigh;
                if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum)
                {
                    txtmax.Visible = true;
                    txtmin.Visible = false;
                }
                
            }
            else
            {
                cmbBand.SelectedIndex = 0;
                cmbStreach.SelectedIndex = 0;
            }
            isminmax = true;
        }      

        private void cmbStreach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStreach.SelectedIndex == 0)
            {
                pRStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_NONE;

                txtmax.Visible = false;
                txtmin.Visible = false;
            }
            if (cmbStreach.SelectedIndex == 1)
            {
                pRStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_Custom;

                txtmax.Visible = false;
                txtmin.Visible = false;
            }
            if (cmbStreach.SelectedIndex == 2)
            {
                if (isminmax)
                {
                    pRStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;

                    if (pRBCollection.Item(cmbBand.SelectedIndex).Statistics == null)
                    {
                        pRBCollection.Item(cmbBand.SelectedIndex).ComputeStatsAndHist();
                    }
                    txtmax.Visible = true;
                    txtmin.Visible = true;
                    lblminstr.Text = pRBCollection.Item(cmbBand.SelectedIndex).Statistics.Minimum.ToString();
                    lblmaxstr.Text = pRBCollection.Item(cmbBand.SelectedIndex).Statistics.Maximum.ToString();
                    txtmin.Text = lblminstr.Text;
                    txtmax.Text = lblmaxstr.Text;
                    txtmin.Focus();

                }
            }

            if (cmbStreach.SelectedIndex == 3)
            {
                pRStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations;

                txtmax.Visible = false;
                txtmin.Visible = false;
            }
            if (cmbStreach.SelectedIndex == 4)
            {
                pRStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize;

                txtmax.Visible = false;
                txtmin.Visible = false;
            }
        }

        private void cmbBand_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRasterBand pRABand = pRBCollection.Item(cmbBand.SelectedIndex);
            if (pRABand.Statistics == null)
            {
                pRABand.ComputeStatsAndHist();
            }

            lblminstr.Text = pRABand.Statistics.Minimum.ToString();
            lblmaxstr.Text = pRABand.Statistics.Maximum.ToString();
            txtmin.Text = lblminstr.Text;
            txtmax.Text = lblmaxstr.Text;
        }

        private void cmbColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int symbolindex = cmbColorRamp.SelectedIndex;//获取选择的序号
            IStyleGalleryItem mpStyleGalleryItem = pSymbolClass.GetItem(symbolindex);
            PColorramp = (IColorRamp)mpStyleGalleryItem.Item;//获取选择的符号 
        }

        private void chkdisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdisplay.Checked == false)
            {
                backvalue.Enabled = false;
                colbackg.Enabled = false;
            }
            else
            {
                backvalue.Enabled = true;
                colbackg.Enabled = true;
            }
        }


        /****重写combox的类，使得combox能够显示图片****/
        public partial class ComboboxSymbol : ComboBox
        {
            public ComboboxSymbol()
            {
                DrawMode = DrawMode.OwnerDrawFixed;
                DropDownStyle = ComboBoxStyle.DropDownList;
                Cursor = System.Windows.Forms.Cursors.Arrow;
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                try
                {
                    //显示图片
                    Image image = (Image)Items[e.Index];
                    System.Drawing.Rectangle rect = e.Bounds;
                    e.Graphics.DrawImage(image, rect);
                }

                catch
                {
                }

                finally
                {
                    base.OnDrawItem(e);
                }
            }
        }

    }
}
