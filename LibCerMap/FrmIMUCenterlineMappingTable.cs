using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.Geodatabase;

namespace LibCerMap
{
    //public delegate void RefreshEvent();

    public partial class FrmIMUCenterlineMappingTable : OfficeForm
    {
        //连接点原始位置
        private IPointCollection m_OriginPoints;// = new MultipointClass();
        //连接点目标位置
        private IPointCollection m_TargetPoints;// = new MultipointClass();
        //地图控件
        private IMapControl2 m_pMapCtr;// = new IMapControl2();
        private int m_nSeletedIndex;
        //public IGraphicsLayer m_pGraphicsLayer;
        //更新事件
        //public event RefreshEvent refreshLayer=null;//=new RefreshEvent();

        public List<IFeature> IMUFeatureList;
        public List<IFeature> CenterlinePointFeatureList;

        public IFeatureLayer IMULayer;
        public IFeatureLayer CenterlinePointLayer;
        public IFeatureLayer CenterlineLinarLayer;

        public int SeletedIndex
        {
            get
            {
                return m_nSeletedIndex;
            }
        }

        //public IPointCollection OriginPoints
        //{
        //    get
        //    {
        //        return m_OriginPoints;
        //    }

        //    set
        //    {
        //        m_OriginPoints = value;
        //    }
        //}

        //public IPointCollection TargetPoints
        //{
        //    get
        //    {
        //        return m_TargetPoints;
        //    }

        //    set
        //    {
        //        m_TargetPoints = value;
        //    }
        //}

        public IMapControl2 MapCtr
        {
            get
            {
                return m_pMapCtr;
            }

            set
            {
                m_pMapCtr = value;
            }
        }


        public FrmIMUCenterlineMappingTable()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void btnDeleteLink_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rowCollection=this.dataGridViewX1.SelectedRows;
            int nCount=rowCollection.Count;
            int nOriCount= IMUFeatureList.Count;

            List<IFeature> tmpPointCollectionOri = new List<IFeature>();
            List<IFeature> tmpPointCollectionDst = new List<IFeature>();
            for (int i = 0; i < nOriCount; i++)
            {
                bool bFlag = false;
                for (int j = 0; j < nCount; j++)
                {
                    if (i == rowCollection[j].Index)
                    {
                        bFlag = true;
                        break;
                    }
                }

                if (bFlag == false)
                {
                    tmpPointCollectionOri.Add(IMUFeatureList[i]);
                    tmpPointCollectionDst.Add(CenterlinePointFeatureList[i]);
                }
            }
            IMUFeatureList.Clear();
            CenterlinePointFeatureList.Clear();
            //m_OriginPoints.RemovePoints(0, m_OriginPoints.PointCount);
            //m_TargetPoints.RemovePoints(0, m_TargetPoints.PointCount);
            for (int i = 0; i < tmpPointCollectionDst.Count;i++ )
            {
                //m_OriginPoints.AddPoint(tmpPointCollectionOri.get_Point(i));
                //m_TargetPoints.AddPoint(tmpPointCollectionDst.get_Point(i));
                IMUFeatureList.Add(tmpPointCollectionOri[i]);
                CenterlinePointFeatureList.Add(tmpPointCollectionDst[i]);
            }            

            RefreshDataTable();

            //m_pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //m_pMapCtr.RefreshLayer();
            //if (refreshLayer != null)
            //{
            //    refreshLayer();
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_OriginPoints == null)
            {
                return;
            }

            int nRowIndex=e.RowIndex;
            int nColIndex=e.ColumnIndex;
            switch (e.ColumnIndex)
            {
                case 1:
                    m_OriginPoints.get_Point(nRowIndex).X = double.Parse(dataGridViewX1.Rows[nRowIndex].Cells[nColIndex].Value.ToString());
                    break;
                case 2:
                    m_OriginPoints.get_Point(nRowIndex).Y = double.Parse(dataGridViewX1.Rows[nRowIndex].Cells[nColIndex].Value.ToString());
                    break;
                case 3:
                    m_TargetPoints.get_Point(nRowIndex).X = double.Parse(dataGridViewX1.Rows[nRowIndex].Cells[nColIndex].Value.ToString());
                    break;
                case 4:
                    m_TargetPoints.get_Point(nRowIndex).Y = double.Parse(dataGridViewX1.Rows[nRowIndex].Cells[nColIndex].Value.ToString());
                    break;
                default:
                    break;
            }

            RefreshDataTable();
            //m_pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //m_pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //RefreshLayer();
            //if (refreshLayer != null)
            //{
            //    refreshLayer();
            //}
        }

        private void FrmVectorLinkTable_Load(object sender, EventArgs e)
        {
             // m_pGraphicsLayer = m_pMapCtr.Map.BasicGraphicsLayer;
            //  obj2 = m_pGraphicsLayer;
           // m_pGraphicsLayer = ((ICompositeGraphicsLayer)m_pMapCtr.Map.BasicGraphicsLayer).AddLayer("", null); 
            RefreshDataTable();
        }

        public void RefreshDataTable()
        {
            //if (m_OriginPoints == null)
            //{
            //    return;
            //}

            dataTable1.Clear();

            int nCount = IMUFeatureList.Count;
            for (int i = 0; i < nCount; i++)
            {
                DataRow row = dataTable1.NewRow();
                IFeature pCenterlinePointFeature = CenterlinePointFeatureList[i];
                IFeature pIMUFeature = IMUFeatureList[i];
                string fieldname = "";
                row[0] = i + 1;
                fieldname = dataTable1.Columns[1].Caption;
                row[1] = pIMUFeature.Value[pIMUFeature.Fields.FindField(fieldname)];
                fieldname = dataTable1.Columns[2].Caption;
                row[2] = pIMUFeature.Value[pIMUFeature.Fields.FindField(fieldname)];

                fieldname = dataTable1.Columns[3].Caption;
                row[3] = pCenterlinePointFeature.Value[pCenterlinePointFeature.Fields.FindField(fieldname)];

                fieldname = dataTable1.Columns[4].Caption;
                row[4] = pCenterlinePointFeature.Value[pCenterlinePointFeature.Fields.FindField(fieldname)];

                row[5] = pIMUFeature.OID;
                row[6] = pCenterlinePointFeature.OID;
                //row[1] = m_OriginPoints.get_Point(i).X;
                //row[2] = m_OriginPoints.get_Point(i).Y;
                //row[3] = m_TargetPoints.get_Point(i).X;
                //row[4] = m_TargetPoints.get_Point(i).Y;
                dataTable1.Rows.Add(row);
            }

            if (IMUFeatureList.Count == 0)
            {
                this.btnDeleteLink.Enabled = false;
            }
            else
            {
                this.btnDeleteLink.Enabled = true;
            }

            //pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            m_pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewX1.SelectedRows != null && dataGridViewX1.SelectedRows.Count >0)
                    m_nSeletedIndex = dataGridViewX1.SelectedRows[0].Index;
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        //private void dataGridViewX1_DataSourceChanged(object sender, EventArgs e)
        //{

        //}
       
        private void FrmVectorLinkTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
            }
            Hide();
        }

        private void dataGridViewX1_MouseUp(object sender, MouseEventArgs e)
        {
            int nCount = this.dataGridViewX1.SelectedRows.Count;

            for (int i = 0; i < nCount; i++)
            {
                int nIndex = this.dataGridViewX1.SelectedRows[i].Index;

                IPolyline ppl = new PolylineClass();
                ppl.FromPoint = IMUFeatureList[i].Shape as IPoint;
                ppl.ToPoint = CenterlinePointFeatureList[i].Shape as IPoint;

                if (m_pMapCtr != null)
                {
                    m_pMapCtr.FlashShape(ppl, 3, 100);
                }
            }
        }
       
        public void UpdateLayerName()
        {
            if (IMULayer != null)
            {
                labelIMU.Text = IMULayer.Name;
            }
            if (CenterlinePointLayer != null)
            {
                labelCenterlinePoint.Text = CenterlinePointLayer.Name;
            }
            if (CenterlineLinarLayer != null)
            {
                labelCenterline.Text = CenterlineLinarLayer.Name;
            }
        }

        private void buttonXOK_Click(object sender, EventArgs e)
        {
            try
            {
                if(IMUFeatureList.Count < 2)
                {
                    MessageBox.Show("最少需要2个特征点");
                    return;
                }
                
                IQueryFilter pQF = null;
                IFeatureClass pLineFC = CenterlinePointLayer.FeatureClass;
                DataTable CenterlinePointTable = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pLineFC as ITable, pQF);
                IFeatureClass pPointFC = IMULayer.FeatureClass;

                DataTable IMUTable;       
                IMUTable = AOFunctions.GDB.ITableUtil.GetDataTableFromITable(pPointFC as ITable, pQF);

                Dictionary<DataRow, DataRow> MatchedDataRowPair = new Dictionary<DataRow, DataRow>();
                  List<IFeature> sortedIMUList = (from IFeature f in IMUFeatureList
                                    select f).OrderBy(x => x.get_Value(x.Fields.FindField(EvConfig.IMUMoveDistanceField))).ToList();

                 List<IFeature> sortedCenterlinePointList = (from IFeature f in CenterlinePointFeatureList
                                                select f).OrderBy(x => x.get_Value(x.Fields.FindField(EvConfig.CenterlineMeasureField))).ToList();

               
                DataView dv = CenterlinePointTable.DefaultView;
                // dv.Sort = "里程（m） ASC";
                dv.Sort = EvConfig.CenterlineMeasureField + " ASC";
                CenterlinePointTable = dv.ToTable();
                dv = IMUTable.DefaultView;
                // dv.Sort = "记录距离__ ASC";
                dv.Sort = EvConfig.IMUMoveDistanceField + " ASC";
                IMUTable = dv.ToTable();

                
               // double centerlineLength = endM - beginM;
                if (!IMUTable.Columns.Contains("X"))
                    IMUTable.Columns.Add("X", System.Type.GetType("System.Double"));
                if (!IMUTable.Columns.Contains("Y"))
                    IMUTable.Columns.Add("Y", System.Type.GetType("System.Double"));
                if (!IMUTable.Columns.Contains("Z"))
                    IMUTable.Columns.Add("Z", System.Type.GetType("System.Double"));
                if (!IMUTable.Columns.Contains("里程差"))
                    IMUTable.Columns.Add("里程差", System.Type.GetType("System.Double"));
                if (!IMUTable.Columns.Contains("对齐里程"))
                    IMUTable.Columns.Add("对齐里程", System.Type.GetType("System.Double"));

                for (int i = 0; i < IMUFeatureList.Count; i++)
                {
                    int oid1 = sortedIMUList[i].OID;
                    int oid2 = sortedCenterlinePointList[i].OID;
                    DataRow IMURow = IMUTable.AsEnumerable().First(x => Convert.ToInt16(x[0]) == oid1);
                    DataRow CenterlineRow = CenterlinePointTable.AsEnumerable().First(x => Convert.ToInt16(x[0]) == oid2);
                    MatchedDataRowPair.Add(IMURow, CenterlineRow);
                }


                foreach(DataRow r in IMUTable.Rows)
                {
                    r["对齐里程"] = DBNull.Value;
                    r["里程差"] = DBNull.Value;

                }

                double endIMUM = Convert.ToDouble(IMUTable.Rows[IMUTable.Rows.Count - 1][EvConfig.IMUMoveDistanceField]);
                double beginIMUM = Convert.ToDouble(IMUTable.Rows[0][EvConfig.IMUMoveDistanceField]);
                double IMULength = endIMUM - beginIMUM;

                double CenterlineBeginM = Convert.ToDouble( CenterlinePointTable.Rows[0][EvConfig.CenterlineMeasureField]);
                double CenterlineEndM = Convert.ToDouble(CenterlinePointTable.AsEnumerable().Last()[EvConfig.CenterlineMeasureField]);

                foreach (DataRow r in MatchedDataRowPair.Keys)
                {
                    r["对齐里程"] = MatchedDataRowPair[r][EvConfig.CenterlineMeasureField];
                    r["里程差"] = 0;
                }

                //List<DataRow> WantouPointList = (from DataRow r in IMUTable.Rows
                //                                 where r["类型"].ToString().Contains("弯头")
                //                                 select r).ToList();
                //List<DataRow> GuandianPointList = (from DataRow r in CenterlinePointTable.Rows
                //                                   where r["测点属性"].ToString().Contains("拐点")
                //                                   select r).ToList();

                DataTable alignmentPointTable = IMUTable;

                double beginM =0;
                double endM=0;
                 var query = (from r in alignmentPointTable.AsEnumerable()
                                   where r["对齐里程"] != DBNull.Value
                                   select r).ToList();
                    if (query.Count > 0)
                    {
                        DataRow r = query[0];
                        if(IMUTable.Rows[0]["对齐里程"] != DBNull.Value)
                        {
                            beginM = Convert.ToDouble( IMUTable.Rows[0]["对齐里程"]);
                        }
                        else
                        {
                            beginM = Convert.ToDouble(r["对齐里程"]) -
                           (Convert.ToDouble(r[EvConfig.IMUMoveDistanceField]) - Convert.ToDouble(alignmentPointTable.Rows[0][EvConfig.IMUMoveDistanceField]));
                            alignmentPointTable.Rows[0]["对齐里程"] = beginM;
                        }
                       

                        int idx = alignmentPointTable.Rows.Count - 1;
                        r = query[query.Count - 1];

                        if (IMUTable.AsEnumerable().Last()["对齐里程"] != DBNull.Value)
                        {
                            endM = Convert.ToDouble(IMUTable.AsEnumerable().Last()["对齐里程"]);
                        }
                        else
                        {
                            endM = Convert.ToDouble(r["对齐里程"]) +
                          (Convert.ToDouble(alignmentPointTable.Rows[idx][EvConfig.IMUMoveDistanceField]) - Convert.ToDouble(r[EvConfig.IMUMoveDistanceField]));
                            alignmentPointTable.Rows[idx]["对齐里程"] = endM;
                        }
                    }

                if(beginM < CenterlineBeginM)
                {
                    beginM = CenterlineBeginM;
                }
                if(endM > CenterlineEndM)
                {
                    endM = CenterlineEndM;
                }


                    DataRow PrevRowWithM = null;
                    for (int i = 0; i < IMUTable.Rows.Count; i++)
                    {
                        DataRow r = IMUTable.Rows[i];
                        if (r["对齐里程"] != DBNull.Value)
                        {
                            PrevRowWithM = r;
                        }
                        else
                        {
                            DataRow NextRowWithM = null;
                            for (int j = i + 1; j < IMUTable.Rows.Count; j++)
                            {
                                DataRow r2 = IMUTable.Rows[j];
                                if (r2["对齐里程"] != DBNull.Value)
                                {
                                    NextRowWithM = r2;
                                    break;
                                }
                            }
                            if (PrevRowWithM == null || NextRowWithM == null)
                            {
                                break;
                            }
                            double BeginJiluM = Convert.ToDouble(PrevRowWithM[EvConfig.IMUMoveDistanceField]);
                            double endJiluM = Convert.ToDouble(NextRowWithM[EvConfig.IMUMoveDistanceField]);
                            double BeginAM = Convert.ToDouble(PrevRowWithM["对齐里程"]);
                            double endAM = Convert.ToDouble(NextRowWithM["对齐里程"]);
                            double currentJiluM = Convert.ToDouble(r[EvConfig.IMUMoveDistanceField]);
                            r["对齐里程"] = (currentJiluM - BeginJiluM) * (endAM - BeginAM) / (endJiluM - BeginJiluM) + BeginAM;
                        }
                    }

                    IFeatureLayer pLinearlayer = CenterlineLinarLayer;
                     
                    IFeatureCursor pcursor = pLinearlayer.FeatureClass.Search(null, false);
                    IFeature pFeature = pcursor.NextFeature();
                    IPolyline pline = pFeature.Shape as IPolyline;
                    IMSegmentation mSegment = pline as IMSegmentation;
                    double maxM = mSegment.MMax;
                    double minM = mSegment.MMin;

                    //if (beginM > maxM || beginM < minM || endM > maxM || endM < minM)
                    //{
                    //    MessageBox.Show("输入的起始或终点里程值超出中线里程范围!");
                    //    return;
                    //}

                    for (int i = 0; i < IMUTable.Rows.Count; i++)
                    {
                        DataRow r = IMUTable.Rows[i];
                        double M = Convert.ToDouble(r["对齐里程"]);

                        IGeometryCollection ptcollection = mSegment.GetPointsAtM(M, 0);
                        IPoint pt = ptcollection.get_Geometry(0) as IPoint;
                        r["X"] = pt.X;
                        r["Y"] = pt.Y;
                        r["Z"] = pt.Z;
                    }


                    FrmIMUAlignmentresult frm = new FrmIMUAlignmentresult(IMUTable);
                    frm.CenterlinePointTable = CenterlinePointTable;
                    frm.setResultType("内检测对齐中线报告");
                    frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
