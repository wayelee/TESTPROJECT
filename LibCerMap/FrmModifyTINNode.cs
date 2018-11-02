using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmModifyTINNode : OfficeForm
    {
        public double dHeight;
        public ITinEdit m_pTinEdit;
        public ITinNode m_pTinNode;
        public IMapControl2 m_pMapCtr;
        IMovePointFeedback pMovePtFeedBack;
        public FrmModifyTINNode()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        
        private void FrmModifyTINNode_Load(object sender, EventArgs e)
        {

        }
        public void setDoubleInputValue(double val)
        {
            doubleInput1.Value = val;
        }

        public void setBttonEnable(bool val)
        {
            buttonXOK.Enabled = val;
        }

        private void doubleInput1_ValueChanged(object sender, EventArgs e)
        {
            dHeight = doubleInput1.Value;        
        }

        private void buttonXOK_Click(object sender, EventArgs e)
        {
            if (m_pTinEdit != null && m_pTinNode != null)
            {
                m_pTinEdit.SetNodeZ(m_pTinNode.Index,dHeight);
            }
            if (m_pMapCtr != null)
            {
                m_pMapCtr.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, null, null);
            }
            if (pMovePtFeedBack != null)
            {
                IPoint pt= new PointClass();
                pMovePtFeedBack.Start(pt, pt);
            }
            setBttonEnable(false);
        }

        private void FrmModifyTINNode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
            }
            Hide();
        }

    }
}
