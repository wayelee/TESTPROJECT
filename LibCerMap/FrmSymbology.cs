using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;

using DevComponents.DotNetBar;



namespace LibCerMap
{
    public partial class FrmSymbology : Form
    {
        IRasterLayer pRLayer = null;
        ITinLayer pTLayer = null;
        AxMapControl pMapContral = null;
        AxTOCControl pTocContral = null;
        AxPageLayoutControl pPageControl = null;
        AxSceneControl pSceneControl = null;
        IRaster pRaster = null;
        IRaster2 pRaster2 = null;
        IRasterDatasetEdit3 pRasterDatasetEdit = null;
        IRasterBandCollection pRasterBandCollection = null;
        IRasterRenderer pRasterRender = null;
        IRasterRendererColorRamp pRasterRenderColorRamp = null;
        IRasterStretchColorRampRenderer pSCRampRender = null;
        IRasterClassifyColorRampRenderer pRCColorRender = null;
        IRasterUniqueValueRenderer pRUniqueRender = null;
        IRasterUniqueValueRenderer pRUrenderer = new RasterUniqueValueRendererClass();
        ISymbologyStyleClass pSymbolClass = null;
        IColorRamp PColorramp = null;
        IRandomColorRamp pRandomRamp = new RandomColorRampClass();
        IRasterStretch pRasterStretch = new RasterStretchColorRampRendererClass();
        IRasterStretchMinMax pRasterStretchMinMax = null;
        IRasterBand pRasterban = null;

        List<int> uvalue = new List<int>();
        List<long> ucount = new List<long>();

        bool isselect = false;
        bool isstrtrue = false;

        //bool istrue=false;

        public FrmSymbology(AxMapControl pmapcontral, AxTOCControl ptoccontral, IRasterLayer pLayer, AxSceneControl sceneControl, AxPageLayoutControl PageControl)
        {
            InitializeComponent();           
            pRLayer = pLayer;
            pMapContral=pmapcontral;
            pTocContral=ptoccontral;
            pSceneControl = sceneControl;
            pPageControl = PageControl;
        }

        private void FrmSymbology_Load(object sender, EventArgs e)
        {
            pRaster = pRLayer.Raster;
            pRaster2 = pRLayer.Raster as IRaster2;
            pRasterDatasetEdit = pRaster2.RasterDataset as IRasterDatasetEdit3;
            pRasterBandCollection = pRasterDatasetEdit as IRasterBandCollection;
            //计算直方图和统计值
            ComputeStatsAndHist(pRasterBandCollection);

            pRasterRender = pRLayer.Renderer;
            pRasterRenderColorRamp = pRasterRender as IRasterRendererColorRamp;

            pRCColorRender = pRasterRender as IRasterClassifyColorRampRenderer;
            pRUniqueRender = pRasterRender as IRasterUniqueValueRenderer;

            //////////////////////////////////////////////////////////////////////////
            //修改。。。分类模式下无法转换。。。
            //////////////////////////////////////////////////////////////////////////
            pSCRampRender = pRasterRender as IRasterStretchColorRampRenderer;

            //pRasterStretch = pSCRampRender as IRasterStretch;

            string sInstall = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyControl1.LoadStyleFile(sInstall);
            axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;
            pSymbolClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassColorRamps);

            IRasterCursor pRcursor = pRaster2.CreateCursorEx(null);
            IPixelBlock3 pPixelBlock = pRcursor.PixelBlock as IPixelBlock3;
            rstPixelType pixeltype = pPixelBlock.get_PixelType(0);

            if (pixeltype == rstPixelType.PT_UCHAR || pixeltype == rstPixelType.PT_CHAR)
            {
                treeshow.Nodes[2].Visible = true;
            }
            else
            {
                treeshow.Nodes[2].Visible = false;
            }


            if (pRasterRenderColorRamp != null)// 栅格影像为拉伸、分类、独立值渲染
            {
                if (pRaster2.Colormap!=null)
                {
                    treeshow.Nodes[3].Visible = true;
                } 
                else
                {
                    treeshow.Nodes[3].Visible = false;
                }
                singleband_initial();
            } 

            else if(pRaster2.Colormap!=null) //栅格影像含有colormap并使用colormap进行渲染
            {
                colormap_initial();
                colorband_initial();
            }                  
        }

        private void treeshow_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
        {
            if (treeshow.Nodes[0].IsSelected)//分类
            {
                groupfield.Visible = true;
                groupclass.Visible = true;
                datagridsymbol.Visible = true;
                cmbcolor.Visible = true;
                cmbcolorstr.Visible = false;
                groupstr.Visible = false;
                lblstrtype.Visible = false;
                cmbstrsype.Visible = false;
                lblcolor.Visible = false;

                grpanelcolor.Visible = false;
                grPanelfield.Visible = false;
                gridunique.Visible = false;
                gridcolormap.Visible = false;

                chkdisplay.Visible = false;
                backvalue.Visible = false;
                backas.Visible = false;
                colbackg.Visible = false;

                lblDisplayNoData.Visible = false;
                colorNoData.Visible = false;
            } 
            else if(treeshow.Nodes[1].IsSelected)
            {
                groupfield.Visible = false;
                groupclass.Visible = false;
                datagridsymbol.Visible = false;
                cmbcolor.Visible = false;
                cmbcolorstr.Visible = true;
                groupstr.Visible = true;
                lblstrtype.Visible = true;
                cmbstrsype.Visible = true;
                lblcolor.Visible = true;

                grpanelcolor.Visible = false;
                grPanelfield.Visible = false;
                gridunique.Visible = false;
                gridcolormap.Visible = false;

                chkdisplay.Visible = true;
                backvalue.Visible = true;
                backas.Visible = true;
                colbackg.Visible = true;

                lblDisplayNoData.Visible = true;
                colorNoData.Visible = true;
            }

            else if (treeshow.Nodes[2].IsSelected)
            {
                groupfield.Visible = false;
                groupclass.Visible = false;
                datagridsymbol.Visible = false;
                cmbcolor.Visible = false;
                cmbcolorstr.Visible = false;
                groupstr.Visible = false;
                lblstrtype.Visible = false;
                cmbstrsype.Visible = false;
                lblcolor.Visible = true;

                grpanelcolor.Visible = true;
                grPanelfield.Visible = true;
                gridunique.Visible = true;
                gridcolormap.Visible = false;

                chkdisplay.Visible = false;
                backvalue.Visible = false;
                backas.Visible = false;
                colbackg.Visible = false;

                lblDisplayNoData.Visible = false;
                colorNoData.Visible = false;

                initgridunique();
            }
            else
            {
                groupfield.Visible = false;
                groupclass.Visible = false;
                datagridsymbol.Visible = false;
                cmbcolor.Visible = false;
                cmbcolorstr.Visible = false;
                groupstr.Visible = false;
                lblstrtype.Visible = false;
                cmbstrsype.Visible = false;
                lblcolor.Visible = false;

                grpanelcolor.Visible = false;
                grPanelfield.Visible = false;
                gridunique.Visible = false;

                gridcolormap.Visible = true;

                chkdisplay.Visible = false;
                backvalue.Visible = false;
                backas.Visible = false;
                colbackg.Visible = false;

                lblDisplayNoData.Visible = false;
                colorNoData.Visible = false;

                initgridcolormap();
            }
        }

        /// <summary>
        ///  栅格影像为单波段
        /// </summary>
        
        private void singleband_initial()
        {
            /* *********************************
             * 生成色度带
             * *********************************/
            IStyleGalleryItem pStyleGalleryItem = new ServerStyleGalleryItem();
            pStyleGalleryItem.Item = pRasterRenderColorRamp.ColorRamp;
            pSymbolClass.AddItem(pStyleGalleryItem, 0);
            pSymbolClass.SelectItem(0);

            pRandomRamp.MaxSaturation = 100;
            pRandomRamp.MinSaturation = 0;
            pRandomRamp.StartHue = 0;
            pRandomRamp.EndHue = 360;
            pRandomRamp.MaxValue = 100;
            pRandomRamp.MinValue = 0;
            pRandomRamp.Size = 256;
            bool bok;
            pRandomRamp.CreateRamp(out bok);
            if (bok)
            {
                initcmbbox();
            }

            for (int i = 0; i < pSymbolClass.get_ItemCount(pSymbolClass.StyleCategory); i++)
            {
                stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(pSymbolClass.GetItem(i), cmbcolor.Width, cmbcolor.Height);
                Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                cmbcolor.Items.Add(image);
                cmbcolorstr.Items.Add(image);
            }

            if (cmbcolor.Items.Count > 0)
            {
                cmbcolor.SelectedIndex = 0;
                cmbcolorstr.SelectedIndex = 0;
            }

            /***********************************
             * 计算栅格图像的各统计值
             * *********************************/

            IRasterCursor pRcursor = pRaster2.CreateCursorEx(null);
            IPixelBlock3 pPixelBlock = pRcursor.PixelBlock as IPixelBlock3;
            rstPixelType pixeltype = pPixelBlock.get_PixelType(0);

           
            /***********************************
            * 初始化datagridview
            * *********************************/    

            if (pRCColorRender!=null)//采用分类渲染模式的单波段影像
            {

                treeshow.SelectNode(treeshow.Nodes[0], DevComponents.AdvTree.eTreeAction.Code);
                cmbclasses.SelectedIndex = pRCColorRender.ClassCount-1;
                cmbstrsype.SelectedIndex = 0;//将拉伸方式设为无
                datagridsymbol.CurrentCell = null; 
                dataTable1.Rows.Clear();
                dataTable2.Rows.Clear();
                for (int i=0; i < pRCColorRender.ClassCount;i++ )
                {
                    DataRow row = dataTable1.NewRow();
                    row[0] = pRCColorRender.get_Break(i + 1);
                    dataTable1.Rows.Add(row);
                    DataRow row2 = dataTable2.NewRow();
                    row2[1] = pRCColorRender.get_Break(i).ToString() + " - " + pRCColorRender.get_Break(i + 1).ToString();
                    row2[2] = pRCColorRender.get_Label(i);
                    dataTable2.Rows.Add(row2);
                    datagridsymbol.Rows[i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(((IFillSymbol)pRCColorRender.get_Symbol(i)).Color);
                    datagridsymbol.Rows[i].Cells[1].ReadOnly = true;
                }
                datagridsymbol.CurrentCell=null;

                if (pRasterBandCollection.Item(0).Statistics == null)            
                {
                    if (pixeltype.ToString() == "PT_FLOAT" || pixeltype.ToString() == "PT_DOUBLE" || pixeltype.ToString() == "PT_COMPLEX"
                        || pixeltype.ToString() == "PT_DCOMPLEX")
                    {
                        string smin = null;
                        string smax = null;
                        IRasterProps pRasterProp = (IRasterProps)pRaster2;
                        int rheight = pRasterProp.Height;//栅格图像的高度
                        int rwidth = pRasterProp.Width;//栅格图像的宽度
                        double dx = pRasterProp.MeanCellSize().X;//栅格的宽度
                        double dy = pRasterProp.MeanCellSize().Y;//栅格的高度

                        if (pRCColorRender.get_Break(0)>pRCColorRender.get_Break(pRCColorRender.ClassCount-1))
                        {
                            lblmin.Text = pRCColorRender.get_Break(pRCColorRender.ClassCount - 1).ToString();
                            lblmax.Text = pRCColorRender.get_Break(0).ToString();
                        }
                        else 
                        {
                            lblmax.Text = pRCColorRender.get_Break(pRCColorRender.ClassCount - 1).ToString();
                            lblmin.Text = pRCColorRender.get_Break(0).ToString();
                        }

                        lblmaxstr.Text = lblmax.Text;
                        lblminstr.Text = lblmin.Text;

                        lblstr3.Visible = false;
                        lblstr4.Visible = false;
                        lbldev.Visible = false;
                        lblavg.Visible = false;
                    }

                    else
                    {
                        if (MessageBox.Show("该图层没有统计信息，是否计算统计信息？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            this.Close();
                        }

                        if (pRaster2.AttributeTable == null)
                        {
                            pRasterDatasetEdit.BuildAttributeTable();
                        }
                        if (pRasterBandCollection.Item(0).Statistics == null)
                        {
                            pRasterDatasetEdit.DeleteStats();
                            pRasterDatasetEdit.ComputeStatisticsHistogram(1, 1, null, true);
                        }
                        lblmax.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                        lblmin.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                        lblmaxstr.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                        lblminstr.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                        lblavg.Text = pRasterBandCollection.Item(0).Statistics.Mean.ToString();
                        lbldev.Text = pRasterBandCollection.Item(0).Statistics.StandardDeviation.ToString();
                    }
                }
                else
                {
                    lblmax.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                    lblmin.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                    lblmaxstr.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                    lblminstr.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                    lblavg.Text = pRasterBandCollection.Item(0).Statistics.Mean.ToString();
                    lbldev.Text = pRasterBandCollection.Item(0).Statistics.StandardDeviation.ToString();
                }
            }


            else if (pSCRampRender != null)//采用拉伸渲染模式
            {
                bool bFlag = false;
                IRasterBand pRasterBand = (pRaster as IRasterBandCollection).Item(0);
                pRasterBand.HasStatistics(out bFlag);
                if (false==bFlag)
                //if (pRasterBandCollection.Item(0).Statistics == null)
                {
                    if (pixeltype.ToString() == "PT_FLOAT" || pixeltype.ToString() == "PT_DOUBLE" || pixeltype.ToString() == "PT_COMPLEX"
                        || pixeltype.ToString() == "PT_DCOMPLEX")
                    {
                        string smin = null;
                        string smax = null;
                        IRasterProps pRasterProp = (IRasterProps)pRaster2;
                        int rheight = pRasterProp.Height;//栅格图像的高度
                        int rwidth = pRasterProp.Width;//栅格图像的宽度
                        double dx = pRasterProp.MeanCellSize().X;//栅格的宽度
                        double dy = pRasterProp.MeanCellSize().Y;//栅格的高度

                        IRasterStretchColorRampRenderer pStretchColorRasterRenderer1 = (IRasterStretchColorRampRenderer)pRasterRenderColorRamp;
                        string[] sArraylow = pStretchColorRasterRenderer1.LabelLow.Split(char.Parse(":"));
                        string[] sArrayhigh = pStretchColorRasterRenderer1.LabelHigh.Split(char.Parse(":"));

                        lblminstr.Text = sArraylow[1];
                        lblmaxstr.Text = sArrayhigh[1];

                        lblstr3.Visible = false;
                        lblstr4.Visible = false;
                        lbldev.Visible = false;
                        lblavg.Visible = false;
                    }

                    else
                    {
                        if (MessageBox.Show("该图层没有统计信息，是否计算统计信息？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            this.Close();
                        }

                        if (pRaster2.AttributeTable == null)
                        {
                            pRasterDatasetEdit.BuildAttributeTable();
                        }
                        if (pRasterBandCollection.Item(0).Statistics == null)
                        {
                            //pRasterBandCollection.Item(0).ComputeStatsAndHist();
                            pRasterDatasetEdit.DeleteStats();
                            pRasterDatasetEdit.ComputeStatisticsHistogram(1, 1, null, true);
                        }
                        lblmax.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                        lblmin.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                        lblmaxstr.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                        lblminstr.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                        lblavg.Text = pRasterBandCollection.Item(0).Statistics.Mean.ToString();
                        lbldev.Text = pRasterBandCollection.Item(0).Statistics.StandardDeviation.ToString();
                    }
                }
                else
                {
                    lblmax.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                    lblmin.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                    lblmaxstr.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                    lblminstr.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                    lblavg.Text = pRasterBandCollection.Item(0).Statistics.Mean.ToString();
                    lbldev.Text = pRasterBandCollection.Item(0).Statistics.StandardDeviation.ToString();
                }


                treeshow.SelectNode(treeshow.Nodes[1], DevComponents.AdvTree.eTreeAction.Code);
               // istrue = true;
                pRasterStretch = (IRasterStretch)pRasterRender;
                if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_Custom)
                {
                    cmbstrsype.SelectedIndex = 1;
                }
                else if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum)
                {
                    cmbstrsype.SelectedIndex = 2;
                }
                else if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations)
                {
                    cmbstrsype.SelectedIndex = 3;
                }
                else if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize)
                {
                    cmbstrsype.SelectedIndex = 4;
                }
                else
                {
                    cmbstrsype.SelectedIndex = 0;
                }
            
            }

            else if (pRUniqueRender != null)
            {
                treeshow.SelectNode(treeshow.Nodes[2], DevComponents.AdvTree.eTreeAction.Code);
                cmbstrsype.SelectedIndex = 0;//将拉伸方式设为无
                if (pRasterBandCollection.Item(0).Statistics == null)
                {

                    if (MessageBox.Show("该图层没有统计信息，是否计算统计信息？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        this.Close();
                    }

                    if (pRaster2.AttributeTable == null)
                    {
                        pRasterDatasetEdit.BuildAttributeTable();
                    }
                    if (pRasterBandCollection.Item(0).Statistics == null)
                    {
                        //pRasterBandCollection.Item(0).ComputeStatsAndHist();
                        pRasterDatasetEdit.DeleteStats();
                        pRasterDatasetEdit.ComputeStatisticsHistogram(1, 1, null, true);
                    }
                    lblmax.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                    lblmin.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                    lblmaxstr.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                    lblminstr.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                    lblavg.Text = pRasterBandCollection.Item(0).Statistics.Mean.ToString();
                    lbldev.Text = pRasterBandCollection.Item(0).Statistics.StandardDeviation.ToString();
                }
                else
                {
                    lblmax.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                    lblmin.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                    lblmaxstr.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
                    lblminstr.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
                    lblavg.Text = pRasterBandCollection.Item(0).Statistics.Mean.ToString();
                    lbldev.Text = pRasterBandCollection.Item(0).Statistics.StandardDeviation.ToString();
                } 
              
            }
            
     
        }

        /// <summary>
        /// 初始化独立值两个下拉框工具条
        /// </summary>
        private void initcmbbox()
        {
            IRasterCursor pRcursor = pRaster2.CreateCursorEx(null);
            IPixelBlock3 pPixelBlock = pRcursor.PixelBlock as IPixelBlock3;
            rstPixelType pixeltype = pPixelBlock.get_PixelType(0);
            ITable pTable = null;
            ITable table = null;
            if (pixeltype==rstPixelType.PT_UCHAR || pixeltype == rstPixelType.PT_CHAR)
            {
                if (pRaster2.AttributeTable == null)
                {
                    pRasterDatasetEdit.BuildAttributeTable();
                    IRasterBand pRband = null;
                    pRasterBandCollection = pRasterDatasetEdit as IRasterBandCollection;
                    pRband = pRasterBandCollection.Item(0);
                    table = pRband.AttributeTable;
                }

                if (table != null)
                {
                    pTable = table;
                }
                else
                {
                    pTable = pRaster2.AttributeTable;
                }
                if (pTable == null)
                {
                    return;
                }
                IFields pFields = pTable.Fields;


                IStyleGalleryItem pStyleGalleryItem = new ServerStyleGalleryItem();
                pStyleGalleryItem.Item = pRandomRamp;
                stdole.IPictureDisp mPicture = pSymbolClass.PreviewItem(pStyleGalleryItem, cmbcolorunique.Width, cmbcolorunique.Height);
                Image mimage = Image.FromHbitmap(new System.IntPtr(mPicture.Handle));
                cmbcolorunique.Items.Add(mimage);

                for (int i = 53; i < 79; i++)
                {
                    stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(pSymbolClass.GetItem(i), cmbcolorunique.Width, cmbcolorunique.Height);
                    Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                    cmbcolorunique.Items.Add(image);
                }

                if (cmbcolorunique.Items.Count > 0)
                {
                    cmbcolorunique.SelectedIndex = 0;
                }

                for (int i = 1; i < pTable.Fields.FieldCount; i++)
                {
                    if (pFields.Field[i].Name != "Count")
                    {
                        cmbfield.Items.Add(pFields.Field[i].Name);
                    }
                }
                if (pRUniqueRender == null)
                {
                    cmbfield.SelectedIndex = 0;
                }
                else
                {
                    for (int j = 0; j < cmbfield.Items.Count; j++)
                    {
                        if (pRUniqueRender.Field == cmbfield.Items[j].ToString())
                        {
                            cmbfield.SelectedIndex = j;
                        }
                    }
                }

            }
            else
            {

            }
        }

        /// <summary>
        /// 初始化独立值渲染的grid
        /// </summary>
        private void initgridunique()
        {
            if (gridunique.Rows.Count<=1)
            {
                if (pRUniqueRender == null)
                {
                    uvalue.Clear();
                    ucount.Clear();

                    ITable table = null;
                    ITable ptableu = null;                   

                    ///////////////////////
                    //下面的方法能为栅格数据创建数据表并读出数据表
                    ///////////////////////
                    if (pRaster2.AttributeTable == null)
                    {
                        pRasterDatasetEdit.BuildAttributeTable();
                        IRasterBand pRband = null;
                        pRasterBandCollection = pRasterDatasetEdit as IRasterBandCollection;
                        pRband = pRasterBandCollection.Item(0);
                        table = pRband.AttributeTable;
                    }

                    if (table!=null)
                    {
                        ptableu = table;
                    }
                    else 
                    {
                        ptableu = pRaster2.AttributeTable;
                    }                   
                    IFields pfiel = ptableu.Fields;
                    int a = 0;
                    long la = 0;
                    for (int j = 0; j < pfiel.FieldCount; j++)
                    {
                        if (pfiel.Field[j].Name == "Count")
                        {
                            a = j;
                        }
                    }
                    IRow row = null;
                    int tablerows = ptableu.RowCount(null);
                    for (int i = 0; i < tablerows; i++)
                    {
                        row = ptableu.GetRow(i);
                        int value = Convert.ToInt32(row.get_Value(cmbfield.SelectedIndex + 1));
                        la = Convert.ToInt64(row.get_Value(a));
                        uvalue.Add(value);
                        ucount.Add(la);
                    }

                    pRUrenderer.HeadingCount = 1;
                    pRUrenderer.set_Heading(0, "All Data Value");
                    pRUrenderer.set_ClassCount(0, tablerows);
                    pRUrenderer.Field = cmbfield.SelectedItem.ToString();

                    DataRow headrow = dataTable3.NewRow();
                    headrow[1] = cmbfield.SelectedItem.ToString();
                    dataTable3.Rows.Add(headrow);

                    for (int k = 0; k < uvalue.Count; k++)
                    {
                        DataRow pointrow = dataTable3.NewRow();
                        pointrow[1] = uvalue[k];
                        pointrow[2] = ucount[k];
                        dataTable3.Rows.Add(pointrow);
                        gridunique.Rows[1 + k].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pRandomRamp.get_Color(k));
                        pRUrenderer.AddValue(0, k, uvalue[k]);
                        pRUrenderer.set_Label(0, k, uvalue[k].ToString());
                        ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                        fillSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridunique.Rows[1 + k].Cells[0].Style.BackColor);
                        pRUrenderer.set_Symbol(0, k, fillSymbol as ISymbol);
                    }                  
                }
                else
                {
                    uvalue.Clear();
                    ucount.Clear();
                    ITable ptableu = pRaster2.AttributeTable;
                    IFields pfiel = ptableu.Fields;
                    int a = 0;
                    long la = 0;
                    for (int j = 0; j < pfiel.FieldCount; j++)
                    {
                        if (pfiel.Field[j].Name == "Count")
                        {
                            a = j;
                        }
                    }

                    IRow row = null;
                    int tablerows = ptableu.RowCount(null);
                    for (int i = 0; i < pRUniqueRender.get_ClassCount(0); i++)
                    {
                        row = ptableu.GetRow(i);
                        if (row.get_Value(cmbfield.SelectedIndex + 1).ToString() == pRUniqueRender.get_Label(0, i))
                        {
                            la = Convert.ToInt64(row.get_Value(i));
                            ucount.Add(la);
                        }

                    }


                    DataRow headrow = dataTable3.NewRow();
                    headrow[1] = cmbfield.SelectedItem.ToString();
                    dataTable3.Rows.Add(headrow);

                    for (int k = 0; k < pRUniqueRender.get_ClassCount(0); k++)
                    {
                        DataRow pointrow = dataTable3.NewRow();
                        pointrow[1] = pRUniqueRender.get_Label(0, k);
                        pointrow[2] = ucount[k];
                        dataTable3.Rows.Add(pointrow);
                        ISimpleFillSymbol fillSymbol = pRUniqueRender.get_Symbol(0, k) as ISimpleFillSymbol;
                        gridunique.Rows[1 + k].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(fillSymbol.Color);
                    }
                }
            }                      
            gridunique.CurrentCell = null;
        }

        /// <summary>
        /// 栅格影像含有colormap并使用colormap进行渲染
        /// </summary>
        private void colormap_initial()
        {
            treeshow.Nodes[3].Visible = true;
            treeshow.SelectNode(treeshow.Nodes[3], DevComponents.AdvTree.eTreeAction.Code);
        }

        /// <summary>
        /// 初始化colormap的grid
        /// </summary>
        private void initgridcolormap()
        {
            IRasterColormap3 pRColorMap=new RasterColormapClass();
            List<IColor> colormapcolors=new List<IColor>();
            pRColorMap=pRaster2.Colormap as IRasterColormap3;
                   
            for (int k = 0; k < ((int[])pRColorMap.Colors).Length;k++ )
            {
                DataRow pointrow = dataTable4.NewRow();
                Color icolor=ColorTranslator.FromOle(((int[])pRColorMap.Colors)[k]);
                DataRow row = dataTable4.NewRow();
                row[1] = 255 - k;
                dataTable4.Rows.Add(row);
                gridcolormap.Rows[k].Cells[0].Style.BackColor = icolor;
            }
            gridcolormap.CurrentCell = null;
        }

        /// <summary>
        /// 渲染方式为colormap时初始化各控件
        /// </summary>
        private void colorband_initial()
        {
            /* *********************************
            * 生成色度带
            * *********************************/
            IStyleGalleryItem pStyleGalleryItem = new ServerStyleGalleryItem();           

            pRandomRamp.MaxSaturation = 100;
            pRandomRamp.MinSaturation = 0;
            pRandomRamp.StartHue = 0;
            pRandomRamp.EndHue = 360;
            pRandomRamp.MaxValue = 100;
            pRandomRamp.MinValue = 0;
            pRandomRamp.Size = 200;
            bool bok;
            pRandomRamp.CreateRamp(out bok);
            if (bok)
            {
                initcmbbox();
            }

            for (int i = 0; i < pSymbolClass.get_ItemCount(pSymbolClass.StyleCategory); i++)
            {
                stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(pSymbolClass.GetItem(i), cmbcolor.Width, cmbcolor.Height);
                Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                cmbcolor.Items.Add(image);
                cmbcolorstr.Items.Add(image);
            }

            if (cmbcolor.Items.Count > 0)
            {
                cmbcolor.SelectedIndex = 0;
                cmbcolorstr.SelectedIndex = 0;
            }


            if (pRaster2.AttributeTable == null)
            {
                pRasterDatasetEdit.BuildAttributeTable();
            }
            if (pRasterBandCollection.Item(0).Statistics == null)
            {
                //pRasterBandCollection.Item(0).ComputeStatsAndHist();
                pRasterDatasetEdit.DeleteStats();
                pRasterDatasetEdit.ComputeStatisticsHistogram(1, 1, null, true);
            }
            lblmax.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
            lblmin.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
            lblmaxstr.Text = pRasterBandCollection.Item(0).Statistics.Maximum.ToString();
            lblminstr.Text = pRasterBandCollection.Item(0).Statistics.Minimum.ToString();
            lblavg.Text = pRasterBandCollection.Item(0).Statistics.Mean.ToString();
            lbldev.Text = pRasterBandCollection.Item(0).Statistics.StandardDeviation.ToString();

        }

        /// <summary>
        /// 分类数发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbclasses_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (lblmin.Text!="")
                {
                    int classcount = int.Parse(cmbclasses.SelectedItem.ToString());
                    double pixelmax = double.Parse(lblmax.Text);
                    double pixelmin = double.Parse(lblmin.Text);
                    double interval = (pixelmax - pixelmin) / classcount;//分类间隔值
                    dataTable1.Clear();
                    for (int i = 1; i <= classcount; i++)
                    {
                        DataRow row = dataTable1.NewRow();
                        row[0] = (double.Parse(lblmin.Text) + i * interval);
                        dataTable1.Rows.Add(row);
                    }
                    updatadatagrid();
                }
            return;
        }

        /// <summary>
        /// 分类渲染模式选择的颜色条发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbcolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int symbol_index = cmbcolor.SelectedIndex;//获取选择的序号
            IStyleGalleryItem mStyleGalleryItem = pSymbolClass.GetItem(symbol_index);

            // IColorRamp 
            PColorramp = (IColorRamp)mStyleGalleryItem.Item;//获取选择的符号             

            int size = PColorramp.Size;
            int interval;
            if (datagridsymbol.Rows.Count == 2)
            {
                interval = size;
            }
            else
            {
                interval = size / (dataTable2.Rows.Count - 1);
            }
            if (datagridsymbol.Rows.Count > 0)
                (datagridsymbol.Rows[0].Cells[0]).Style.BackColor = ClsGDBDataCommon.IColorToColor(PColorramp.get_Color(0));

            for (int i = 1; i < dataTable2.Rows.Count; i++)
            {
                int cindx = i * interval;
                if (cindx >= size)
                {
                    cindx = size - 1;
                }

                datagridsymbol.Rows[i].Cells[0].Style.BackColor =
                ClsGDBDataCommon.IColorToColor(PColorramp.get_Color(0 + cindx));

            }
            datagridsymbol.CurrentCell = null;
        }

        /// <summary>
        /// 分别设置分类渲染模式下各类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagridsymbol_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                ColorDialog colordig = new ColorDialog();
                if (colordig.ShowDialog()==DialogResult.OK)
                {
                    datagridsymbol.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = colordig.Color;
                    datagridsymbol.CurrentCell = null;
                }
            }
            else
            {
            }
        }

        /// <summary>
        /// 更新datagridview
        /// </summary>
        public void updatadatagrid()
        {
            if (dataTable1.Rows.Count == 0)
                return;
            DataRow row = null;
            dataTable2.Rows.Clear();
            row = dataTable2.NewRow();
            if (lblmin.Text != dataTable1.Rows[0][0].ToString())
            {
                row[1] = lblmin.Text + " - " + dataTable1.Rows[0][0].ToString();
            } 
            else
            {
                row[1] = lblmin.Text;
            }
            
            row[2] = row[1];
            dataTable2.Rows.Add(row);
            for (int i = 0; i < dataTable1.Rows.Count - 1; i++)
            {
                row = dataTable2.NewRow();
                if (dataTable1.Rows[i][0].ToString() != dataTable1.Rows[i + 1][0].ToString())
                {
                    row[1] = dataTable1.Rows[i][0].ToString() + " - " + dataTable1.Rows[i + 1][0].ToString();
                } 
                else
                {
                    row[1] = dataTable1.Rows[i][0].ToString();
                }               
                row[2] = row[1];
                dataTable2.Rows.Add(row);
            }

            int symbolindex = cmbcolor.SelectedIndex;//获取选择颜色的序号
            IStyleGalleryItem pStyleGalleryItem = pSymbolClass.GetItem(symbolindex);
            PColorramp = (IColorRamp)pStyleGalleryItem.Item;

            int size = PColorramp.Size;
            int interval = 0;
            if (datagridsymbol.Rows.Count == 2)
            {
                interval = size;
            }
            else
            {
                interval = size / (dataTable2.Rows.Count - 1);
            }
            for (int j = 0; j < dataTable2.Rows.Count; j++)
            {
                int cindx = j * interval;
                if (cindx >= size)
                {
                    cindx = size - 1;
                }

                datagridsymbol.Rows[j].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(PColorramp.get_Color(0 + cindx));
                datagridsymbol.Rows[j].Cells[1].ReadOnly = true;
            }
            datagridsymbol.CurrentCell = null;
        }

        /// <summary>
        /// 拉伸渲染模式颜色条变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbcolorstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            int symbolindex = cmbcolorstr.SelectedIndex;//获取选择的序号
            IStyleGalleryItem mpStyleGalleryItem = pSymbolClass.GetItem(symbolindex);
            PColorramp = (IColorRamp)mpStyleGalleryItem.Item;//获取选择的符号 
        }

        /// <summary>
        /// 拉伸方式发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbstrsype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbstrsype.SelectedIndex == 0)
            {
                pRasterStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_NONE;

                txtmax.Visible = false;
                txtmin.Visible = false;

                isstrtrue = true;
            }
            if (cmbstrsype.SelectedIndex == 1)
            {
                pRasterStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_Custom;

                txtmax.Visible = false;
                txtmin.Visible = false;

                isstrtrue = true;
            }
            if (cmbstrsype.SelectedIndex == 2)
            {
                if (isstrtrue)
                {
                    pRasterStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;

                    if (lblmin.Text=="")
                    {
                        txtmax.Visible = true;
                        txtmin.Visible = true;
                    }
                    else
                    {
                        txtmax.Visible = true;
                        txtmin.Visible = true;
                        txtmax.Text = lblmax.Text;
                        txtmin.Text = lblmin.Text;
                    }
                    
                    if (pRasterBandCollection.Item(0).Statistics == null)
                    {
                        if (lblmin.Text == "")
                        {
                            MessageBox.Show("该图像没有统计信息，是否计算统计信息？", "提示", MessageBoxButtons.OK);

                            pRasterDatasetEdit.DeleteStats();
                            pRasterDatasetEdit.ComputeStatisticsHistogram(1, 1, null, true);
#region 丢弃

                            //double pixelmax = -999999999;//记录栅格回的最大值
                            //double pixermin = 999999999;//记录栅格灰度最小值
                            /////////////////////////////////////////////////
                            //liuzhaoqin测试成功
                            //object obj = null;
                            //double dPixel = 0.0;

                            //int nWidth = 0;
                            //int nHeight = 0;
                            //System.Array pixels;
                            //IPixelBlock3 pixelBlock3 = null;
                            //IRasterCursor rasterCursor = pRaster2.CreateCursorEx(null);//null时为128*128

                            //do
                            //{
                            //    pixelBlock3 = rasterCursor.PixelBlock as IPixelBlock3;
                            //    nWidth = pixelBlock3.Width;
                            //    nHeight = pixelBlock3.Height;
                            //    pixels = (System.Array)pixelBlock3.get_PixelData(0);
                            //    for (int i = 0; i < nWidth; i++)
                            //    {
                            //        for (int j = 0; j < nHeight; j++)
                            //        {
                            //            obj = pixels.GetValue(i, j);
                            //            double ob = Convert.ToDouble(obj);
                            //            if (ob > -999999 && ob < 999999)
                            //            {
                            //                dPixel = Convert.ToDouble(obj);
                            //                if (dPixel >= pixelmax)
                            //                {
                            //                    pixelmax = dPixel;
                            //                }
                            //                if (dPixel <= pixermin)
                            //                {
                            //                    pixermin = dPixel;
                            //                }
                            //            }
                            //        }
                            //    }

                            //} while (rasterCursor.Next() == true);
#endregion
                            bool bFlag = false;
                            IRasterBand pRasterBand = (pRaster as IRasterBandCollection).Item(0);
                            pRasterBand.HasStatistics(out bFlag);
                            if (!bFlag)
                                pRasterBand.ComputeStatsAndHist();

                            txtmax.Text = pRasterBand.Statistics.Maximum.ToString();
                            txtmin.Text = pRasterBand.Statistics.Minimum.ToString();
                            lblmax.Text = txtmax.Text;
                            lblmin.Text = txtmin.Text;
                        }                     
                    }
                    else
                    {
                        txtmax.Text = lblmaxstr.Text;
                        txtmin.Text = lblminstr.Text;                     
                    }                    
                    txtmin.Focus();
                }
                isstrtrue = true;
            }

            if (cmbstrsype.SelectedIndex == 3)
            {
                pRasterStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations;

                txtmax.Visible = false;
                txtmin.Visible = false;

                isstrtrue = true;
            }
            if (cmbstrsype.SelectedIndex == 4)
            {
                pRasterStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize;

                txtmax.Visible = false;
                txtmin.Visible = false;

                isstrtrue = true;
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

 
        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            btnuse_Click(null, null);
           
            this.Close();
            return;

            if (treeshow.SelectedIndex==0)
            {
                IRaster piRaster = pRLayer.Raster;
                IRaster2 piRaster2 = pRLayer.Raster as IRaster2;
                IRasterDatasetEdit2 pRasterDatasetEdit = pRaster2.RasterDataset as IRasterDatasetEdit2;

                IRasterBandCollection pRasterBandCollection = pRasterDatasetEdit as IRasterBandCollection;

                IRasterClassifyColorRampRenderer pRasterClassifyColorRampRenderer = new RasterClassifyColorRampRendererClass();

                pRasterClassifyColorRampRenderer.ClassCount = datagridsymbol.Rows.Count-1;
                if (datagridsymbol.Rows.Count == 1)
                    return;
                pRasterClassifyColorRampRenderer.set_Break(0, double.Parse(lblmin.Text));

                for (int i = 0; i < pRasterClassifyColorRampRenderer.ClassCount; i++)
                {
                    pRasterClassifyColorRampRenderer.set_Break(i + 1, (double)dataTable1.Rows[i][0]);
                    IFillSymbol pFSymbol = new SimpleFillSymbolClass();
                    pFSymbol.Color = ClsGDBDataCommon.ColorToIColor(datagridsymbol.Rows[i].Cells[0].Style.BackColor);
                    pRasterClassifyColorRampRenderer.set_Symbol(i, (ISymbol)pFSymbol);
                    pRasterClassifyColorRampRenderer.set_Label(i, datagridsymbol.Rows[i].Cells[2].Value.ToString());
                }
                IRasterRenderer pRasterRender = pRasterClassifyColorRampRenderer as IRasterRenderer;
                IRasterRendererColorRamp pcolorramp = pRasterRender as IRasterRendererColorRamp;
                pcolorramp.ColorRamp = PColorramp;
                pRasterRender.Update();
                pRLayer.Renderer = pRasterRender;

                ILayerEffects pLayerEffect = (ILayerEffects)pRLayer;
                ILegendInfo plinfo = (ILegendInfo)pRLayer.Renderer;
                plinfo.get_LegendGroup(0).Visible = false;
                for (int i = 0; i < pRasterClassifyColorRampRenderer.ClassCount; i++)
                {
                    ILegendClass plclass = plinfo.get_LegendGroup(0).get_Class(i);
                    plclass.Label = datagridsymbol.Rows[i].Cells[2].Value.ToString();
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
                this.Close();
            }
            else if (treeshow.SelectedIndex == 1)
            {
                if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum)
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
                        pRasterStretchMinMax = (IRasterStretchMinMax)pRasterStretch;
                        pRasterStretchMinMax.UseCustomStretchMinMax = true;
                        pRasterStretchMinMax.CustomStretchMin = double.Parse(txtmin.Text);
                        pRasterStretchMinMax.CustomStretchMax = double.Parse(txtmax.Text);
                        //pRasterStretch = (IRasterStretch)pRasterStretchMinMax;
                        pRasterStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;
                    }                 
                }
                if (chkdisplay.Checked==true)
                {
                    pRasterStretch.Background = true;
                    pRasterStretch.BackgroundColor = ClsGDBDataCommon.ColorToIColor(colbackg.SelectedColor);
                    pRasterStretch.set_BackgroundValues(Convert.ToDouble(backvalue.Text));
                } 
                else
                {
                    pRasterStretch.Background = false;
                }               
                IRasterStretchColorRampRenderer pStretchColorRasterRenderer = (IRasterStretchColorRampRenderer)pRasterStretch; ;
                IRasterRenderer pRasterRenderer = pStretchColorRasterRenderer as IRasterRenderer;
                pRasterRenderer.Raster = pRLayer.Raster;
                pRasterRenderer.Update();

                pStretchColorRasterRenderer.BandIndex = 0;
                pStretchColorRasterRenderer.ColorRamp = PColorramp as IColorRamp;
                pRLayer.Renderer = pStretchColorRasterRenderer as IRasterRenderer;
                pRasterRenderer.Update();

                if (pTocContral.Buddy == pMapContral.Object)
                {
                    pTocContral.SetBuddyControl(pMapContral);
                    pMapContral.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
                    pTocContral.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pRLayer, null);
                }
                else if (pTocContral.Buddy.Equals(pSceneControl.Object))
                {
                    pTocContral.SetBuddyControl(pSceneControl);
                    IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pRLayer, null);
                    pTocContral.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pRLayer, null);
                    pTocContral.Refresh();
                } 
                this.Close();
            }
            else if (treeshow.SelectedIndex == 2)
            {
                if (pRUniqueRender == null && pRUrenderer != null)
                {
                    pRLayer.Renderer = pRUrenderer as IRasterRenderer;
                }
                else if (pRUniqueRender != null && pRUrenderer == null)
                {
                    pRLayer.Renderer = pRUniqueRender as IRasterRenderer;
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
                this.Close();
            }
        }

        private void btnuse_Click(object sender, EventArgs e)
        {
            if (treeshow.SelectedIndex == 0)//栅格分类渲染
            {
                IRaster piRaster = pRLayer.Raster;
                IRaster2 piRaster2 = pRLayer.Raster as IRaster2;
                IRasterDatasetEdit2 pRasterDatasetEdit = pRaster2.RasterDataset as IRasterDatasetEdit2;

                IRasterBandCollection pRasterBandCollection = pRasterDatasetEdit as IRasterBandCollection;

                IRasterClassifyColorRampRenderer pRasterClassifyColorRampRenderer = new RasterClassifyColorRampRendererClass();

                pRasterClassifyColorRampRenderer.ClassCount = datagridsymbol.Rows.Count - 1;
                if (datagridsymbol.Rows.Count == 1)
                    return;
                pRasterClassifyColorRampRenderer.set_Break(0, double.Parse(lblmin.Text));

                for (int i = 0; i < pRasterClassifyColorRampRenderer.ClassCount; i++)
                {
                    pRasterClassifyColorRampRenderer.set_Break(i + 1, (double)dataTable1.Rows[i][0]);
                    IFillSymbol pFSymbol = new SimpleFillSymbolClass();
                    pFSymbol.Color = ClsGDBDataCommon.ColorToIColor(datagridsymbol.Rows[i].Cells[0].Style.BackColor);
                    pRasterClassifyColorRampRenderer.set_Symbol(i, (ISymbol)pFSymbol);
                    pRasterClassifyColorRampRenderer.set_Label(i, datagridsymbol.Rows[i].Cells[2].Value.ToString());
                }
                IRasterRenderer pRasterRender = pRasterClassifyColorRampRenderer as IRasterRenderer;
                IRasterRendererColorRamp pcolorramp = pRasterRender as IRasterRendererColorRamp;
                pcolorramp.ColorRamp = PColorramp;
                pRLayer.Renderer = pRasterRender;
                pRasterRender.Update();
                

                ILayerEffects pLayerEffect = (ILayerEffects)pRLayer;
                ILegendInfo plinfo = (ILegendInfo)pRLayer.Renderer;
                plinfo.get_LegendGroup(0).Visible = false;
                for (int i = 0; i < pRasterClassifyColorRampRenderer.ClassCount; i++)
                {
                    ILegendClass plclass = plinfo.get_LegendGroup(0).get_Class(i);
                    plclass.Label = datagridsymbol.Rows[i].Cells[2].Value.ToString();
                }
                //pTocContral.SetBuddyControl(pMapContral);
                //pMapContral.Refresh();
                //pTocContral.Refresh();          
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
            else if (treeshow.SelectedIndex == 1)//栅格拉伸渲染
            {
                if (pRasterStretch.StretchType == esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum)
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
                        pRasterStretchMinMax = (IRasterStretchMinMax)pRasterStretch;
                        pRasterStretchMinMax.UseCustomStretchMinMax = true;
                        pRasterStretchMinMax.CustomStretchMin = double.Parse(txtmin.Text);
                        pRasterStretchMinMax.CustomStretchMax = double.Parse(txtmax.Text);
                        //pRasterStretch = (IRasterStretch)pRasterStretchMinMax;
                        pRasterStretch.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;
                    }                 
                }

                //背景色设置
                if (chkdisplay.Checked == true)
                {
                    pRasterStretch.Background = true;
                    pRasterStretch.BackgroundColor = ClsGDBDataCommon.ColorToIColor(colbackg.SelectedColor);
                    pRasterStretch.set_BackgroundValues(Convert.ToDouble(backvalue.Text));
                }
                else
                {
                    pRasterStretch.Background = false;
                }    
                
                //NoData值设置
                IRasterDisplayProps pRasterDisplayProps = new RasterStretchColorRampRendererClass();
                pRasterDisplayProps = pRasterStretch as IRasterDisplayProps;
                pRasterDisplayProps.NoDataColor = ClsGDBDataCommon.ColorToIColor(colorNoData.SelectedColor);
                
                //更改图层渲染方式
                IRasterStretchColorRampRenderer pStretchColorRasterRenderer = (IRasterStretchColorRampRenderer)pRasterStretch;
                IRasterRenderer pRasterRenderer = pStretchColorRasterRenderer as IRasterRenderer;
                pRasterRenderer.Raster = pRLayer.Raster;
                pRasterRenderer.Update();

                pStretchColorRasterRenderer.BandIndex = 0;
                pStretchColorRasterRenderer.ColorRamp = PColorramp as IColorRamp;
                pRLayer.Renderer = pStretchColorRasterRenderer as IRasterRenderer;
                pRasterRenderer.Update();
                pMapContral.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                if (pTocContral.Buddy == pMapContral.Object)
                {
                    pMapContral.Update();
                    pTocContral.SetBuddyControl(pMapContral);
                    pTocContral.Update();
                    pTocContral.ActiveView.Refresh();
                }
                else if (pTocContral.Buddy.Equals(pSceneControl.Object))
                {
                    pTocContral.SetBuddyControl(pSceneControl);
                    IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pRLayer, null);
                    pTocContral.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                } 
            }
            else if (treeshow.SelectedIndex == 2)
            {
                if (pRUniqueRender == null && pRUrenderer != null)
                {
                    pRLayer.Renderer = pRUrenderer as IRasterRenderer;
                }
                else if (pRUniqueRender != null && pRUrenderer == null)
                {
                    pRLayer.Renderer = pRUniqueRender as IRasterRenderer;
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
            else
            {
                //pRLayer.Renderer = pRaster2.Colormap as IRasterRenderer;
                //pTocContral.SetBuddyControl(pMapContral);
                //pMapContral.Refresh();
                //pTocContral.Refresh();
            }
        }

        /// <summary>
        /// 独立值分类颜色条发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbcolorunique_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridunique.Rows.Count>1)
            {
                if (cmbcolorunique.SelectedIndex != 0)
                {
                    IColorRamp PColorramp = null;
                    IStyleGalleryItem mpStyleGalleryItem = pSymbolClass.GetItem(cmbcolorunique.SelectedIndex + 52);
                    PColorramp = (IColorRamp)mpStyleGalleryItem.Item;//获取选择的符号 
                    for (int i = 0; i < gridunique.Rows.Count - 2; i++)
                    {
                        gridunique.Rows[1 + i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(PColorramp.get_Color(i%36));
                    }                      
                }
                else
                {                 
                    for (int i = 0; i < gridunique.Rows.Count-2; i++)
                    {
                        gridunique.Rows[1 + i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pRandomRamp.get_Color(i));
                    }
                }

                for (int i = 0; i < gridunique.Rows.Count - 2; i++)
                {
                     if (pRUniqueRender==null)
                     {
                         ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                         fillSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridunique.Rows[1 + i].Cells[0].Style.BackColor);
                         pRUrenderer.set_Symbol(0, i, fillSymbol as ISymbol);
                     }
                     else if (pRUniqueRender != null )
                     {
                         ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                         fillSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridunique.Rows[1 + i].Cells[0].Style.BackColor);
                         pRUniqueRender.set_Symbol(0, i, fillSymbol as ISymbol);
                     }
                 }

            }
        }

        /// <summary>
        /// 改变独立值分类的类别颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridunique_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridunique.Rows.Count>1)
            {
                if (e.ColumnIndex==0)
                {
                    ColorDialog colordig = new ColorDialog();
                    if (colordig.ShowDialog() == DialogResult.OK)
                    {
                        gridunique.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = colordig.Color;
                        gridunique.CurrentCell = null;
                        if (pRUniqueRender == null)
                        {
                            ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                            fillSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridunique.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                            pRUrenderer.set_Symbol(0, e.RowIndex-1, fillSymbol as ISymbol);
                        }
                        else if (pRUniqueRender != null)
                        {
                            ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                            fillSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridunique.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                            pRUniqueRender.set_Symbol(0, e.RowIndex-1, fillSymbol as ISymbol);
                        }
                    }
                }
            }
        }

        private void btnclass_Click(object sender, EventArgs e)
        {
            if (cmbclasses.SelectedItem!=null)
            {
                if (cmbclasses.SelectedIndex!=0)
                {
                    FrmSymbolBreak frmbreak = new FrmSymbolBreak(this);
                    frmbreak.ShowDialog();
                } 
                else
                {
                    MessageBox.Show("分类数为1时无法设置分隔点", "提示", MessageBoxButtons.OK);
                }                
            } 
            else
            {
                MessageBox.Show("请先选择分类数", "提示", MessageBoxButtons.OK);
            }
           
        }

        private void chkdisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdisplay.Checked==false)
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

        //打开窗体之前首先计算统计值和直方图
        private void ComputeStatsAndHist(IRasterBandCollection pRasterBCollection)
        {
            bool bFlag = false;
            IRasterBand pRBand = pRasterBandCollection.Item(0);
            pRBand.HasStatistics(out bFlag);
            if (false==bFlag)
            {
                if ( MessageBox.Show("该图像没有统计信息，是否计算统计信息？", "提示", MessageBoxButtons.OKCancel)==DialogResult.OK)
                {
                    pRBand.ComputeStatsAndHist();
                }
                else
                {
                    this.Close();
                }
            }
        }

    }//class
}
