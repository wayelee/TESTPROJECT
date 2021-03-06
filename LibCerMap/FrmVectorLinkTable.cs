﻿using System;
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

namespace LibCerMap
{
    public delegate void RefreshEvent();

    public partial class FrmVectorLinkTable : OfficeForm
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
        
        public int SeletedIndex
        {
            get
            {
                return m_nSeletedIndex;
            }
        }

        public IPointCollection OriginPoints
        {
            get
            {
                return m_OriginPoints;
            }

            set
            {
                m_OriginPoints = value;
            }
        }

        public IPointCollection TargetPoints
        {
            get
            {
                return m_TargetPoints;
            }

            set
            {
                m_TargetPoints = value;
            }
        }

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

        
        public FrmVectorLinkTable()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void btnDeleteLink_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rowCollection=this.dataGridViewX1.SelectedRows;
            int nCount=rowCollection.Count;
            int nOriCount=m_OriginPoints.PointCount;

            IPointCollection tmpPointCollectionOri = new MultipointClass();
            IPointCollection tmpPointCollectionDst = new MultipointClass();
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
                    tmpPointCollectionOri.AddPoint(m_OriginPoints.get_Point(i));
                    tmpPointCollectionDst.AddPoint(m_TargetPoints.get_Point(i));
                }
            }

            m_OriginPoints.RemovePoints(0, m_OriginPoints.PointCount);
            m_TargetPoints.RemovePoints(0, m_TargetPoints.PointCount);
            for (int i = 0; i < tmpPointCollectionDst.PointCount;i++ )
            {
                m_OriginPoints.AddPoint(tmpPointCollectionOri.get_Point(i));
                m_TargetPoints.AddPoint(tmpPointCollectionDst.get_Point(i));
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
            if (m_OriginPoints == null)
            {
                return;
            }

            dataTable1.Clear();

            int nCount = m_OriginPoints.PointCount;
            for (int i = 0; i < nCount; i++)
            {
                DataRow row = dataTable1.NewRow();
                row[0] = i + 1;
                row[1] = m_OriginPoints.get_Point(i).X;
                row[2] = m_OriginPoints.get_Point(i).Y;
                row[3] = m_TargetPoints.get_Point(i).X;
                row[4] = m_TargetPoints.get_Point(i).Y;
                dataTable1.Rows.Add(row);
            }

            if (m_OriginPoints.PointCount == 0)
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
                if (dataGridViewX1.SelectedRows != null)
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
                ppl.FromPoint = m_OriginPoints.get_Point(nIndex);
                ppl.ToPoint = m_TargetPoints.get_Point(nIndex);

                if (m_pMapCtr != null)
                {
                    m_pMapCtr.FlashShape(ppl, 3, 100);
                }
            }
        }
    }
}
