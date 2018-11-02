using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using System.IO;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmLinkTableRaster : OfficeForm
    {
        public IRasterLayer pRasterLayer;
        public IMap pMap;
        public IPointCollection OriginPoints  ;
        public IPointCollection TargetPoints ;
        public IPointCollection TransformedOriginPoints  ;
        public IMapControl2 pMapCtr;
        public int WarpType = 0;
        public int selectedidx = -1;
        public FrmLinkTableRaster()
        {
            InitializeComponent();
            this.EnableGlass = false;
            dataTable1.RowChanged += new DataRowChangeEventHandler(dataTable1_RowChanged);
            dataTable1.TableClearing +=new DataTableClearEventHandler(dataTable1_TableClearing);
            comboBoxExType.Items.Clear();
            comboBoxExType.Items.Add("一次多项式");
            comboBoxExType.SelectedIndex = 0;
        }
        
        private void FrmLinkTableRaster_Load(object sender, EventArgs e)
        {
            RefreshControlAllPoints();
        }

        private void dataTable1_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            int a = 0;
        }
        private void dataTable1_TableClearing(object sender, DataTableClearEventArgs e)
        {
            ;
        }

        public void AddControlPoints(int idx,IPoint pto, IPoint ptt)
        {
            if (pto == null)
                return;
            if (ptt == null)
                return;
            //得到转换后的坐标 
            double dRms = 0.0;
            
            if(TransformedOriginPoints.PointCount >idx)
            {
                IPoint ptTrans = TransformedOriginPoints.get_Point(idx);
                dRms = ComputeRMS(ptTrans, ptt);
            }          

            DataRow row = dataTable1.NewRow();
            row[0] = idx;
            row[1] = pto.X;
            row[2] = pto.Y;
            row[3] = ptt.X;
            row[4] = ptt.Y;
            row[5] = dRms;
            dataTable1.Rows.Add(row);
        }

        public double ComputeRMS(IPoint opt,IPoint tpt)
        {
            double dRms = 0.0;
            if (pMapCtr != null && opt !=null && tpt !=null)
            {
                //int ox = 0; int oy = 0; int tx = 0; int ty = 0;
                //pMapCtr.FromMapPoint(opt, ref ox, ref oy);
                //pMapCtr.FromMapPoint(tpt, ref tx, ref ty);
                double ox = opt.X;
                double oy = opt.Y;
                double tx = tpt.X;
                double ty = tpt.Y;
                dRms = Math.Sqrt((tx - ox) * (tx - ox) + (ty - oy) * (ty - oy));
            }

            return dRms;
        }
            
        //public void AddControlPoints( IPoint pto, IPoint ptt)
        //{
        //    if (pto == null)
        //        return;
        //    if (ptt == null)
        //        return;
        //    DataRow row = dataTable1.NewRow();
        //    row[1] = pto.X;
        //    row[2] = pto.Y;
        //    row[3] = ptt.X;
        //    row[4] = ptt.Y;
        //    dataTable1.Rows.Add(row);
        //}

        private void FrmLinkTableRaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
            }
            Hide();
        }

        //#region 测试绘制
        //private void DrawControlPoint()
        //{
        //    try
        //    {
        //       // if (m_ToolAddControlPoints == null) return;

        //        int linelength = 8;
        //        IGraphicsContainer pGraphiccsContainer = pMapCtr.Map as IGraphicsContainer;
        //        if (pGraphiccsContainer == null)
        //            return;

        //        for (int i = 0; i < TargetPoints.PointCount; i++)
        //        {
        //            IPoint opt = TransformedOriginPoints.get_Point(i);
        //            IPoint tpt = TargetPoints.get_Point(i);
        //            IMapControl3 mapctrl = pMapCtr  as IMapControl3;

        //            //判断点是否在视图范围内，如果在即绘制，不在则不绘制
        //            IEnvelope pEnvelope = mapctrl.ActiveView.Extent;

        //            //int ox = 0; int oy = 0; int tx = 0; int ty = 0;
        //            //mapctrl.FromMapPoint(opt, ref ox, ref oy);
        //            //mapctrl.FromMapPoint(tpt, ref tx, ref ty);

        //            #region 连接线
        //            ILineElement pConnectLineElement = new LineElementClass();
        //            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
        //            pLineSymbol.Width = 2;
        //            pConnectLineElement.Symbol = pLineSymbol;
        //            IElement pConnectElement = pConnectLineElement as IElement;

        //            //设置GEOMETRY
        //            IPolyline pLine = new PolylineClass();
        //            //IPoint ptFromPoint = opt;// new PointClass();
        //            //ptFromPoint.X = ox;
        //            //ptFromPoint.Y = oy;
        //            pLine.FromPoint = opt;// ptFromPoint;

        //            //IPoint ptToPoint = new PointClass();
        //            //ptToPoint.X = ox;
        //            //ptToPoint.Y = oy;
        //            pLine.ToPoint = tpt;
        //            pConnectElement.Geometry = pLine;
        //            pGraphiccsContainer.AddElement(pConnectElement, 0);

        //            //设置颜色
        //            //if (pMapCtr.CurrentTool ==m_FrmLinkTableRaster.selectedidx)
        //            //{
        //            //    
        //            //}
        //            #endregion

        //            #region 源点十字丝
        //            //            gcs.DrawLine(penred, ox - linelength, oy, ox + linelength, oy);
        //            //            gcs.DrawLine(penred, ox, oy - linelength, ox, oy + linelength);
        //            //            gcs.DrawLine(pengreen, tx - linelength, ty, tx + linelength, ty);
        //            //            gcs.DrawLine(pengreen, tx, ty - linelength, tx, ty + linelength);
        //            //            gcs.DrawString(i.ToString(), strFont, strBrush, ox+4, oy+4);
        //            //ILineElement pSrcLineElement = new LineElementClass();
        //            //IElement pSrcElement = pSrcLineElement as IElement;

        //            //ILine pSrcLine = new LineClass();
        //            //IPoint pSrcFromPoint = new PointClass();
        //            //pSrcFromPoint.X=ox - linelength;
        //            //pSrcFromPoint.Y = oy;

        //            //IPoint pSrcFromPoint = new PointClass();
        //            //pSrcFromPoint.X = ox - linelength;
        //            //pSrcFromPoint.Y = oy;
        //            #endregion

        //            #region 目标点十字丝
        //            #endregion

        //        }

        //        pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        //    }
        //    catch (System.Exception ex)
        //    {

        //    }
        //}
        //#endregion

        //删除选中点
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count == 0)
            {
                return;
            }

            int nCount = dataGridViewX1.SelectedRows.Count;
            List<int> nIndexList = new List<int>();
            for (int i = 0; i < nCount; i++)
            {
                DataGridViewRow row = dataGridViewX1.SelectedRows[i];
                nIndexList.Add(row.Index);
            }

            //升序排列
            nIndexList.Sort();
            nCount=-1;
            while ((nCount = nIndexList.Count) != 0)
            {
                OriginPoints.RemovePoints(nIndexList[nCount - 1], 1);
                TargetPoints.RemovePoints(nIndexList[nCount - 1], 1);
                nIndexList.RemoveAt(nCount - 1);
            }
            //for (int i = 0; i < nCount; i++)
            //{
            //    int idx = nIndexArray[i];
            //    OriginPoints.RemovePoints(idx, 1);
            //    TargetPoints.RemovePoints(idx, 1);
            //}
                        
            RefreshControlAllPoints();
            //if(dataGridViewX1)
            //if (idx - 1 >= 0)
            //{
            //    dataGridViewX1.Rows[idx - 1].Selected = true;
            //}
            //if (dataTable1.Rows.Count > 0)
            //{
            //    dataGridViewX1.Rows[idx-1].Selected = true;
            //}

        }
        public void RefreshControlAllPoints()
        {
            //先对图作校正，再更新表格数据，方便计算中误差

            //1.校正
            WarpLayer();

            //2.更新表格数据
            dataTable1.Rows.Clear();
            for (int i = 0; i < OriginPoints.PointCount; i++)
            {
                IPoint pto = OriginPoints.get_Point(i);
                IPoint ptt = null;
                if (i < TargetPoints.PointCount)
                {
                    ptt = TargetPoints.get_Point(i);
                }
                AddControlPoints(i,pto, ptt);
            }
        }

        private void dataGridViewX1_Validated(object sender, EventArgs e)
        {
          //  RefreshControlAllPoints();
        }

        public void WarpLayer()
        {
            IGeoReference pGR = pRasterLayer as IGeoReference; 
            if (pGR != null)
            {
                pGR.Reset();
                if (OriginPoints == null || TargetPoints == null || OriginPoints.PointCount<1 || TargetPoints.PointCount<1)
                {
                    return;
                }
                if (OriginPoints.PointCount == 1)
                {
                    IPoint pt1, pt2;
                    pt1 = OriginPoints.get_Point(0);
                    pt2 = TargetPoints.get_Point(0);
                    pGR.Shift(pt2.X - pt1.X, pt2.Y - pt1.Y);

                }
                else
                {
                    if (OriginPoints.PointCount == 2)
                    {
                        pGR.TwoPointsAdjust(OriginPoints, TargetPoints);
                    }
                    else
                    {
                        pGR.Warp(OriginPoints, TargetPoints, WarpType);
                    }
                    TransformedOriginPoints = pGR.PointsTransform(OriginPoints, true);
 
                    //RefreshWarpType();
                }
            }          
        }
        //private void RefreshWarpType()
        //{
        //    comboBoxExType.Items.Clear();
        //    if (TargetPoints.PointCount <1)
        //    {
        //        return;
        //    }
        //    comboBoxExType.Items.Add("一次多项式");
        //    if (TargetPoints != null)
        //    {
        //        if (TargetPoints.PointCount >= 6)
        //        {
        //            comboBoxExType.Items.Add("二次多项式");
        //        }
        //        if (TargetPoints.PointCount >= 10)
        //        {
        //            comboBoxExType.Items.Add("三次多项式");
        //        }
        //    }

        //    if (WarpType > comboBoxExType.Items.Count - 1)
        //    {
        //        comboBoxExType.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        comboBoxExType.SelectedIndex = WarpType;
        //    }
        //}
        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row = dataGridViewX1.Rows[e.RowIndex];
                int idx = int.Parse(row.Cells[0].Value.ToString());
                double val = double.Parse(dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                switch (e.ColumnIndex)
                {
                    case 0:
                        break;
                    case 1:
                        OriginPoints.get_Point(idx).X = val;
                        RefreshControlAllPoints();
                        break;
                    case 2:
                        OriginPoints.get_Point(idx).Y = val;
                        RefreshControlAllPoints();
                        break;
                    case 3:
                        TargetPoints.get_Point(idx).X = val;
                        RefreshControlAllPoints();
                        break;
                    case 4:
                        TargetPoints.get_Point(idx).Y = val;
                        RefreshControlAllPoints();
                        break;
                }
            }
            catch
            {
                ;
            }
        }

        private void comboBoxExType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //WarpType = comboBoxExType.SelectedIndex;
            //IGeoReference pGR = pRasterLayer as IGeoReference;
            //if (pGR != null)
            //{
            //    pGR.Reset();
            //    if (OriginPoints == null || TargetPoints == null)
            //    {
            //        return;
            //    }
            //    if (OriginPoints.PointCount == 1)
            //    {
            //        IPoint pt1, pt2;
            //        pt1 = OriginPoints.get_Point(0);
            //        pt2 = TargetPoints.get_Point(0);
            //        pGR.Shift(pt2.X - pt1.X, pt2.Y - pt1.Y);

            //    }
            //    else
            //        if (OriginPoints.PointCount == 2)
            //        {
            //            pGR.TwoPointsAdjust(OriginPoints, TargetPoints);
            //        }
            //        else
            //        {
            //            pGR.Warp(OriginPoints, TargetPoints, WarpType);
            //        }
            //    TransformedOriginPoints = pGR.PointsTransform(OriginPoints, true);
            //    //重新计算中误差

            //    if (pMapCtr != null)
            //        pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //}  
           
          
     
        
        }

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0)
            {
               // selectedidx = dataGridViewX1.SelectedRows[0].Index;
                selectedidx = int.Parse( dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString());
            }
            if (pMapCtr != null)
            {
                pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }

        }

        private void buttonXSaveRaster_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.tif)|*.tif";
            dlg.OverwritePrompt = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                IGeoReference pGR = pRasterLayer as IGeoReference;
                if (pGR != null)
                {
                    try
                    {
                    pGR.Rectify(dlg.FileName, "TIFF");
                    MessageBox.Show("\""+dlg.FileName + "\"保存成功！");
                    }
                    catch(SystemException ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
            }
           
            /*
            if (pRasterLayer == null)
            {
                return;
            }
            FrmSaveRasterTransformation dlg = new FrmSaveRasterTransformation();
           
            double dxcellsize = ((IRasterProps)pRasterLayer.Raster).MeanCellSize().X;
            dlg.SetCellSize(dxcellsize);
            double y = ((IRasterProps)pRasterLayer.Raster).MeanCellSize().Y;
            dlg.SetCellSize(((IRasterProps)pRasterLayer.Raster).Extent.Height / ((IRasterProps)pRasterLayer.Raster).Height);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
               
                try
                {
                    //IGeoReference pGR = pRasterLayer as IGeoReference;
                    //if (pGR != null)
                    //{
                    //    pGR.Rectify(dlg.FileName, "TIFF");
                    //}
                   ITransformationOp TO = new RasterTransformationOpClass();
                    ESRI.ArcGIS.Geodatabase.IGeoDataset ds= TO.Warp(pRasterLayer.Raster as ESRI.ArcGIS.Geodatabase.IGeoDataset,
                        OriginPoints, TargetPoints,
                        (ESRI.ArcGIS.DataSourcesRaster.esriGeoTransTypeEnum)(WarpType + 1),
                        (esriGeoAnalysisResampleEnum)dlg.InterationType);
                    ClsGDBDataCommon cgc = new ClsGDBDataCommon();
                    DirectoryInfo di = new DirectoryInfo(dlg.savefilepath);
                    string strName = di.Name;
                    string parentpath = di.Parent.FullName;
                    IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass ();
                    ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySetClass();
                    propertySet.SetProperty("DATABASE", parentpath); 
                    IWorkspace inmemWor = workspaceFactory.OpenFromFile(parentpath, 0);

                    IRasterLayer player = new RasterLayerClass();
                    player.CreateFromDataset(ds as IRasterDataset);
                    pMapCtr.Map.AddLayer(player as ILayer);
                    pMapCtr.Refresh();

                    //ISaveAs2 sa = ds as ISaveAs2;
                    ISaveAs2 sa = (ds) as ISaveAs2;
                    if (sa != null)
                    {
                       sa.SaveAs(strName,   inmemWor, "TIFF");
                       ((IRasterDataset3)ds).Refresh();
                    }

                    MessageBox.Show("保存\"" + dlg.savefilepath + "\"成功！");
                }
                catch (SystemException ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            */
        }


        //导出控制点
        private void btnExportControlPoints_Click(object sender, EventArgs e)
        {
            if (OriginPoints.PointCount<=0)
                return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "文本文件(*.txt;*.TXT)|*.txt;*.TXT|所有文件(*.*)|*.*";
            if(dlg.ShowDialog()== DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(dlg.FileName);

                    for (int i = 0; i < OriginPoints.PointCount; i++)
                    {
                        sw.Write("{0:f6}\t{1:f6}\t{2:f6}\t{3:f6}", OriginPoints.get_Point(i).X, OriginPoints.get_Point(i).Y, TargetPoints.get_Point(i).X, TargetPoints.get_Point(i).Y);
                        sw.WriteLine();
                    }

                    sw.Close();
                    MessageBox.Show("导出成功！");
                }
                catch (System.Exception ex)
                {
                	MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnImportControlPoints_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "文本文件(*.txt;*.TXT)|*.txt;*.TXT|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OriginPoints.RemovePoints(0, OriginPoints.PointCount);
                    TargetPoints.RemovePoints(0, TargetPoints.PointCount);
                    TransformedOriginPoints.RemovePoints(0, TransformedOriginPoints.PointCount);
                    
                    StreamReader sr = new StreamReader(dlg.FileName);
                    while (!sr.EndOfStream)
                    {
                        string szCurrent=sr.ReadLine();
                        string[] szStringList = szCurrent.Split('\t');
                        if (szStringList.Length != 4)
                            continue;
                        else
                        {
                            IPoint pSrcPoint=new PointClass();
                            pSrcPoint.X = Convert.ToDouble(szStringList[0]);
                            pSrcPoint.Y = Convert.ToDouble(szStringList[1]);
                            OriginPoints.AddPoint(pSrcPoint);

                            IPoint pDstPoint = new PointClass();
                            pDstPoint.X = Convert.ToDouble(szStringList[2]);
                            pDstPoint.Y = Convert.ToDouble(szStringList[3]);
                            TargetPoints.AddPoint(pDstPoint);
                        }
                    }

                    sr.Close();

                    RefreshControlAllPoints();
                    if (OriginPoints.PointCount >0)
                        dataGridViewX1.Rows[0].Selected = true;

                    //DrawControlPoint();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            DelAllPoints();
        }

        public void DelAllPoints()
        {
            OriginPoints.RemovePoints(0, OriginPoints.PointCount);
            TargetPoints.RemovePoints(0, TargetPoints.PointCount);
            TransformedOriginPoints.RemovePoints(0, TransformedOriginPoints.PointCount);
            RefreshControlAllPoints();
        }

        private void dataGridViewX1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //双击时居中对应点
            try
            {       
            if (dataGridViewX1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewX1.SelectedRows[0];
                selectedidx = int.Parse(row.Cells[0].Value.ToString());
                if (selectedidx >=0)
                {
                    IPoint point = new PointClass();
                    point.X = (double.Parse(row.Cells[1].Value.ToString()) + double.Parse(row.Cells[3].Value.ToString()))/2;
                    point.Y = (double.Parse(row.Cells[2].Value.ToString() )+ double.Parse(row.Cells[4].Value.ToString()))/2;
                    pMapCtr.CenterAt(point);
                    pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void FrmLinkTableRaster_VisibleChanged(object sender, EventArgs e)
        {
            if (pMapCtr != null)
                pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null); 
        }
    }
}
