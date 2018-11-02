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
    public partial class FrmSymboloTin : OfficeForm
    {
        AxMapControl pMapContral = null;
        AxTOCControl pTocContral = null;
        ITinLayer pTLayer = null;
        AxSceneControl pSceneControl = null;
        DevComponents.DotNetBar.Bar bar3 = null;

        List<double> lowbreak=new List<double>();
        List<double> highbreak = new List<double>();
        bool istrue = false;

        ISymbologyStyleClass pSymbolClass = null;

        ITinColorRampRenderer ptinColorRampRenderer = new TinElevationRenderer() as ITinColorRampRenderer;

        public FrmSymboloTin(AxMapControl pmapcontral, AxTOCControl ptoccontral, AxSceneControl scenecontrol,ITinLayer pLayer,DevComponents.DotNetBar.Bar bar)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapContral = pmapcontral;
            pTocContral = ptoccontral;
            pTLayer = pLayer;
            pSceneControl = scenecontrol;
            bar3 = bar;
        }

#region 使用类

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


#endregion

        private void FrmSymboloTin_Load(object sender, EventArgs e)
        {
            string sInstall = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            axSymbologyControl1.LoadStyleFile(sInstall);
            axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;
            pSymbolClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassColorRamps);



            /* *********************************
           * 生成色度带
           * *********************************/

            for (int i = 0; i < pSymbolClass.get_ItemCount(pSymbolClass.StyleCategory); i++)
            {
                stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(pSymbolClass.GetItem(i), cmbcolor.Width, cmbcolor.Height);
                Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                cmbcolor.Items.Add(image);

            }

            if (cmbcolor.Items.Count > 0)
            {
                cmbcolor.SelectedIndex = 0;
            }

            /* *********************************
          * 初始化gridview及颜色控件
          * *********************************/

            if (pTLayer.GetRenderer(0).GetType().Name == "TinFaceRendererClass" || pTLayer.GetRenderer(0).Name=="Faces")
            {
                treeshow.SelectedIndex = 1;
                ITinSingleSymbolRenderer pTinrenderer = pTLayer.GetRenderer(0) as ITinSingleSymbolRenderer;
                ISimpleFillSymbol psymbol = new SimpleFillSymbolClass();
                psymbol = pTinrenderer.Symbol as ISimpleFillSymbol;
                btncolor.SelectedColor = ClsGDBDataCommon.IColorToColor(psymbol.Color);
                istrue = true;
            }
            else if (pTLayer.GetRenderer(0).Name == "Elevation"||pTLayer.GetRenderer(0).Name =="Edge types")
            {
                treeshow.SelectedIndex = 0;
                btncolor.SelectedColor = System.Drawing.Color.Green;
                for (int j = 0; j < pTLayer.RendererCount;j++ )
                {
                    if (pTLayer.GetRenderer(j).Name == "Elevation")
                    {
                        ptinColorRampRenderer = pTLayer.GetRenderer(j) as ITinColorRampRenderer;
                        break;
                    }
                }
                cmbclasses.SelectedIndex = ptinColorRampRenderer.BreakCount - 1;
                initialupdatagrid();
            } 
         
           
        }

        private void treeshow_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
        {
            if (treeshow.SelectedIndex==0)
            {
                lblclass.Visible = true;
                lblcolor.Visible = true;
                cmbcolor.Visible = true;
                cmbclasses.Visible = true;
                labelX1.Visible = false;
                btncolor.Visible = false;
                datagridsymbol.Visible = true;
            } 
            else
            {
                lblclass.Visible = false;
                lblcolor.Visible = false;
                cmbcolor.Visible = false;
                cmbclasses.Visible = false;
                labelX1.Visible = true;
                btncolor.Visible = true;
                datagridsymbol.Visible = false;
            }
        }

        //初始化gridview控件
        private void initialupdatagrid()
        {
            for (int i = 0; i < ptinColorRampRenderer.BreakCount;i++ )
            {
                DataRow row = dataTable2.NewRow();
                row[1] = ptinColorRampRenderer.get_Label(i);
                row[2] = row[1];
                dataTable2.Rows.Add(row);

                ISimpleFillSymbol pSymbol = ptinColorRampRenderer.get_Symbol(i) as ISimpleFillSymbol;
                datagridsymbol.Rows[i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pSymbol.Color);               
            }
            datagridsymbol.CurrentCell = null;
        }

        //更新gridview控件
        private void updatadatagrid()
        {

            int classcount = int.Parse(cmbclasses.SelectedItem.ToString());
            ITinAdvanced pTinAdv = pTLayer.Dataset as ITinAdvanced;
            double dZMin = pTinAdv.Extent.ZMin; //tin的Z轴最高值
            double dZMax = pTinAdv.Extent.ZMax; //tin的Z轴最低值
            double dInterval = (dZMax - dZMin) / classcount;

            double dLowBreak = dZMin;
            double dHighBreak = dLowBreak + dInterval;

            //创建颜色集合
            IColorRamp pColorRamp = null;
            int symbol_index = cmbcolor.SelectedIndex;//获取选择的序号
            IStyleGalleryItem mStyleGalleryItem = pSymbolClass.GetItem(symbol_index);
            // IColorRamp 
            pColorRamp = (IColorRamp)mStyleGalleryItem.Item;//获取选择的符号  
            pColorRamp.Size = classcount;
            bool bOK = false;
            pColorRamp.CreateRamp(out bOK);

            lowbreak.Clear();
            highbreak.Clear();
            dataTable2.Rows.Clear();
            for (int i = 0; i < pColorRamp.Size; i++)
            {
                lowbreak.Add(dLowBreak);
                highbreak.Add(dHighBreak);
                DataRow row = dataTable2.NewRow();
                row[1] = dLowBreak.ToString("#.#") + " - " + dHighBreak.ToString("#.#");
                row[2] = row[1];
                dataTable2.Rows.Add(row);
                dLowBreak = dHighBreak;
                dHighBreak = dHighBreak + dInterval;
                datagridsymbol.Rows[i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pColorRamp.get_Color(i));
               
            }
            datagridsymbol.CurrentCell = null;
        }

        private void datagridsymbol_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                ColorDialog colordig = new ColorDialog();
                if (colordig.ShowDialog() == DialogResult.OK)
                {
                    datagridsymbol.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = colordig.Color;
                    datagridsymbol.CurrentCell = null;
                }
            }
            else
            {
            }
        }

        private void cmbcolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbclasses.SelectedItem!=null)
            {
                ITinAdvanced pTinAdv = pTLayer.Dataset as ITinAdvanced;
                IColorRamp pColorRamp = null;
                int symbol_index = cmbcolor.SelectedIndex;//获取选择的序号
                IStyleGalleryItem mStyleGalleryItem = pSymbolClass.GetItem(symbol_index);
                // IColorRamp 
                pColorRamp = (IColorRamp)mStyleGalleryItem.Item;//获取选择的符号  
                pColorRamp.Size = int.Parse(cmbclasses.SelectedItem.ToString());
                bool bOK = false;
                pColorRamp.CreateRamp(out bOK);
                for (int i = 0; i < pColorRamp.Size; i++)
                {
                    (datagridsymbol.Rows[i].Cells[0]).Style.BackColor = ClsGDBDataCommon.IColorToColor(pColorRamp.get_Color(i));
                }
                datagridsymbol.CurrentCell = null;
            }          
        }

        private void cmbclasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!istrue)
            {
                istrue = true;
            }
            else
            {                             
                updatadatagrid();
            }
        }

        private void btnuse_Click(object sender, EventArgs e)
        {
            if (treeshow.SelectedIndex == 1)
            {
                ITinRenderer pRenderNew = new TinFaceRenderer() as ITinRenderer;
                ITinSingleSymbolRenderer pUVRenderer = pRenderNew as ITinSingleSymbolRenderer;
                ISimpleFillSymbol pSymbol = new SimpleFillSymbolClass();
                pSymbol.Color = ClsGDBDataCommon.ColorToIColor(btncolor.SelectedColor);
                pUVRenderer.Symbol = pSymbol as ISymbol;
                pTLayer.ClearRenderers();
                pTLayer.InsertRenderer(pRenderNew, 0);
                pTocContral.SetBuddyControl(pMapContral);
                pTocContral.Refresh();
                pMapContral.Refresh();
                if ( bar3.SelectedDockTab == 2)
                {
                     IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                     pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pTLayer, null);
                }              
            }
            else
            {
                ITinRenderer pTinRenderer = new TinElevationRenderer() as ITinRenderer;

                //设置样式
                if (pTinRenderer is ITinColorRampRenderer)
                {
                    if (pTinRenderer.Name == "Elevation")
                    {
                        if (lowbreak.Count>0)
                        {
                            int ClassCount = int.Parse(cmbclasses.SelectedItem.ToString());

                            ITinAdvanced pTinAdv = pTLayer.Dataset as ITinAdvanced;
                            ITinColorRampRenderer pTinColorRampRenderer = pTinRenderer as ITinColorRampRenderer;
                            IClassBreaksUIProperties pClassBreaksUIProperties = pTinRenderer as IClassBreaksUIProperties;
                            INumberFormat pNumberFormat = pClassBreaksUIProperties.NumberFormat;
                            pTinColorRampRenderer.MinimumBreak = lowbreak[0];

                            pTinColorRampRenderer.BreakCount = int.Parse(cmbclasses.SelectedItem.ToString());
                            ISimpleFillSymbol pSymbol = null;
                            for (int j = 0; j < pTinColorRampRenderer.BreakCount; j++)
                            {
                                pClassBreaksUIProperties.set_LowBreak(j, lowbreak[j]);
                                pTinColorRampRenderer.set_Break(j, highbreak[j]);

                                //用于图层控制中分级标示显示
                                pTinColorRampRenderer.set_Label(ClassCount - j - 1, datagridsymbol.Rows[j].Cells[2].Value.ToString());
                                pSymbol = new SimpleFillSymbolClass();
                                pSymbol.Color = ClsGDBDataCommon.ColorToIColor(datagridsymbol.Rows[j].Cells[0].Style.BackColor);
                                pTinColorRampRenderer.set_Symbol(ClassCount - j - 1, pSymbol as ISymbol);
                            }
                            pTLayer.ClearRenderers();
                            (pTinColorRampRenderer as ITinRenderer).Visible = true;
                            pTLayer.InsertRenderer(pTinColorRampRenderer as ITinRenderer, 0);//插入一个渲染模型
                            pTocContral.SetBuddyControl(pMapContral);
                            pTocContral.Refresh();
                            pMapContral.Refresh();
                            if (bar3.SelectedDockTab == 2)
                            {
                                IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pTLayer, null);
                            }              
                        }                       
                    }
                }
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (treeshow.SelectedIndex == 1)
            {
                ITinRenderer pRenderNew = new TinFaceRenderer() as ITinRenderer;
                ITinSingleSymbolRenderer pUVRenderer = pRenderNew as ITinSingleSymbolRenderer;
                ISimpleFillSymbol pSymbol = new SimpleFillSymbolClass();
                pSymbol.Color = ClsGDBDataCommon.ColorToIColor(btncolor.SelectedColor);
                pUVRenderer.Symbol = pSymbol as ISymbol;
                pTLayer.ClearRenderers();
                pTLayer.InsertRenderer(pRenderNew, 0);
                pTocContral.SetBuddyControl(pMapContral);
                pTocContral.Refresh();
                pMapContral.Refresh();
                if (bar3.SelectedDockTab == 2)
                {
                    IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pTLayer, null);
                }              
                this.Close();
            }
            else
            {
                ITinRenderer pTinRenderer = new TinElevationRenderer() as ITinRenderer;

                //设置样式
                if (pTinRenderer is ITinColorRampRenderer)
                {
                    if (pTinRenderer.Name == "Elevation")
                    {
                        if (lowbreak.Count > 0)
                        {
                            int ClassCount = int.Parse(cmbclasses.SelectedItem.ToString());

                            ITinAdvanced pTinAdv = pTLayer.Dataset as ITinAdvanced;
                            ITinColorRampRenderer pTinColorRampRenderer = pTinRenderer as ITinColorRampRenderer;
                            IClassBreaksUIProperties pClassBreaksUIProperties = pTinRenderer as IClassBreaksUIProperties;
                            INumberFormat pNumberFormat = pClassBreaksUIProperties.NumberFormat;
                            pTinColorRampRenderer.MinimumBreak = lowbreak[0];

                            pTinColorRampRenderer.BreakCount = int.Parse(cmbclasses.SelectedItem.ToString());
                            ISimpleFillSymbol pSymbol = null;
                            for (int j = 0; j < pTinColorRampRenderer.BreakCount; j++)
                            {
                                pClassBreaksUIProperties.set_LowBreak(j, lowbreak[j]);
                                pTinColorRampRenderer.set_Break(j, highbreak[j]);

                                //用于图层控制中分级标示显示
                                pTinColorRampRenderer.set_Label(ClassCount - j - 1, datagridsymbol.Rows[j].Cells[2].Value.ToString());
                                pSymbol = new SimpleFillSymbolClass();
                                pSymbol.Color = ClsGDBDataCommon.ColorToIColor(datagridsymbol.Rows[j].Cells[0].Style.BackColor);
                                pTinColorRampRenderer.set_Symbol(ClassCount - j - 1, pSymbol as ISymbol);
                            }
                            pTLayer.ClearRenderers();
                            (pTinColorRampRenderer as ITinRenderer).Visible = true;
                            pTLayer.InsertRenderer(pTinColorRampRenderer as ITinRenderer, 0);//插入一个渲染模型 
                            pTocContral.SetBuddyControl(pMapContral);                    
                            pTocContral.Refresh();
                            pMapContral.Refresh();
                            if (bar3.SelectedDockTab == 2)
                            {
                                IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pTLayer, null);
                            }              
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
