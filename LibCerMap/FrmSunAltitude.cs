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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmSunAltitude : OfficeForm
    {
        public IMapControl3 m_pMapControl = null;
        //public ClsCameraPara m_clsCameraPara = null;//new ClsCameraPara();

        //public IPointCollection pSunAltPoints = null;
        public List<ClsPointInfo> m_sunAltInfoList =null;// new List<ClsCameraPara>();

        public FrmSunAltitude(IMapControl3 mapControl)
        {
            m_pMapControl = mapControl;
            InitializeComponent();
            this.EnableGlass = false;
        }

        public void updateInfoList()
        {
            dataTable1.Clear();
            if (m_sunAltInfoList != null && m_sunAltInfoList.Count != 0)
            {
                for (int i = 0; i < m_sunAltInfoList.Count; i++)
                {
                    ClsPointInfo ptInfo = m_sunAltInfoList[i];// new ClsPointInfo();
                    //m_sunAltInfoList.Add(cameraPara);
                    //this.dataGridMatrix.tab
                    DataRow row = dataTable1.NewRow();
                    row[0] = ptInfo.nPointID;
                    row[1] = ptInfo.dbAngleHor;
                    row[2] = ptInfo.dbAngleVer;// m_OriginPoints.get_Point(i).Y;
                    row[3] = ptInfo.nImageX;// m_TargetPoints.get_Point(i).X;
                    row[4] = ptInfo.nImageY;// m_TargetPoints.get_Point(i).Y;

                    if (ptInfo.cameraPara != null)
                    {
                        row[5] = ptInfo.cameraPara.dX;
                        row[6] = ptInfo.cameraPara.dY;
                        row[7] = ptInfo.cameraPara.dZ;
                        row[8] = ptInfo.cameraPara.dPhi;
                        row[9] = ptInfo.cameraPara.dOmg;
                        row[10] = ptInfo.cameraPara.dKappa;
                        row[11] = ptInfo.cameraPara.szImageName;
                    }

                    dataTable1.Rows.Add(row);
                }
            }
        }

        public FrmSunAltitude()
        {
            InitializeComponent();
        }

        private void FrmSunAltitude_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < m_pMapControl.Map.LayerCount; i++)
            //{
            //    ILayer pLayer = m_pMapControl.Map.Layer[i];
            //    if (pLayer is IRasterLayer)
            //    {
            //        cmbLayer.Items.Add(pLayer.Name);
            //    }
            //}
            //if (cmbLayer.Items.Count > 0)
            //{
            //    cmbLayer.SelectedIndex = 0;
            //}

            updateInfoList();
        }

        private void cmbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //此处要根据选择的图层自动读取同文件夹下的同名图像的XML文件，XML中存有相关相机参数
            //if (!GetCameraParaFromXml("", m_clsCameraPara))
            //{
            //    MessageBox.Show("图像参数读取失败！");
            //}
        }

        private bool GetCameraParaFromXml(string strFile, ClsCameraPara para)
        {
            //从文件中读取数据

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel exportexcel = new ExportToExcel();
            exportexcel.ExportDataGridViewToExcel(this.dataGridMatrix);
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            //删除表中数据
            if (m_sunAltInfoList ==null)
            {
                return;
            }

            DataGridViewSelectedRowCollection rowCollection = this.dataGridMatrix.SelectedRows;
            int nCount = rowCollection.Count;

            for (int i = 0; i < nCount; i++)
            {
                int nPtID = Convert.ToInt32(rowCollection[i].Cells[0].Value);
                for (int j = 0; j < m_sunAltInfoList.Count; j++)
                {
                    if (m_sunAltInfoList[j].nPointID == nPtID)
                    {
                        m_sunAltInfoList.RemoveAt(j);
                        break;
                    }
                }
            }

            updateInfoList();

            //refresh table view
            if(m_pMapControl!=null)
                m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void FrmSunAltitude_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
            }

            Hide();
        }

        private void FrmSunAltitude_Shown(object sender, EventArgs e)
        {
            updateInfoList();
        }
    }

    public class ClsPointInfo
    {
        public int nPointID=-1;
        public double dbAngleHor=0;
        public double dbAngleVer=0;
        public int nImageX=-1;
        public int nImageY=-1;

        public ClsCameraPara cameraPara=null;

        public ClsPointInfo()
        {

        }
    }

    public class ClsCameraPara
    {
        //外方位
        public double dX = 0.0;
        public double dY = 0.0;
        public double dZ = 0.0;
        public double dPhi = 0.0;
        public double dOmg = 0.0;
        public double dKappa = 0.0;
        //内方位
        public double dFocus = 0.0;//焦距
        public double dPx = 0.0;//主点
        public double dPy = 0.0;//主点
        public int nW = 0;//影像宽度
        public int nH = 0;//影像高度
        public string szImageName = null;

        public ClsCameraPara()
        {
            
        }
    }
}
