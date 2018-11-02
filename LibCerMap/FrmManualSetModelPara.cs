using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCerMap
{
    public partial class FrmManualSetModelPara : DevComponents.DotNetBar.OfficeForm
    {
        public Model m_pModel=new Model();
        //public bool m_bModifiedFlag = false;  //标识模型参数是否已经被修改，是否需要重新生成模型文件

        public FrmManualSetModelPara(Pt2d nCurrentPos, string szModelType)
        {
            InitializeComponent();
            //m_CurrentPos = nCurrentPos;
            m_pModel.x = nCurrentPos.X;
            m_pModel.y = nCurrentPos.Y;
            if (szModelType.ToLower().Contains("crater"))
                m_pModel.szModelType = "Crater";
            else
                m_pModel.szModelType = "Rock";
            m_pModel.dbSize = 5;
            m_pModel.dbDepth = 1;
        }

        public FrmManualSetModelPara(Model pModel)
        {
            InitializeComponent();
            //m_CurrentPos = nCurrentPos;
            m_pModel.x = pModel.x;// nCurrentPos.X;
            m_pModel.y = pModel.y;// nCurrentPos.Y;
            m_pModel.szModelType = pModel.szModelType;
            m_pModel.dbSize = pModel.dbSize;
            m_pModel.dbDepth = pModel.dbDepth;
        }

        private void cmbModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModelType.SelectedItem == cmiRock)
                dbiDepth.Enabled = false;
            else
                dbiDepth.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (m_pModel.dbDepth != dbiDepth.Value)
            //{
            //    m_pModel.dbDepth = dbiDepth.Value;
            //    m_bModifiedFlag = true;
            //}

            //if (m_pModel.dbSize != dbiSize.Value)
            //{
            //    m_pModel.dbSize = dbiSize.Value;
            //    m_bModifiedFlag = true;
            //}

            //if (m_pModel.x != dbiGeoX.Value)
            //{
            //    m_pModel.x = dbiGeoX.Value;
            //    m_bModifiedFlag = true;
            //}

            //if (m_pModel.y != dbiGeoY.Value)
            //{
            //    m_pModel.y = dbiGeoY.Value;
            //    m_bModifiedFlag = true;
            //}

            m_pModel.dbDepth = dbiDepth.Value;
            m_pModel.dbSize = dbiSize.Value;
            m_pModel.x = dbiGeoX.Value;
            m_pModel.y = dbiGeoY.Value;
            m_pModel.szModelID = txtModelID.Text;
            if (cmbModelType.SelectedItem == cmiCrater)
                m_pModel.szModelType = "Crater";
            else
                m_pModel.szModelType = "Rock";
        }

        private void FrmManualSetModelPara_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
            dbiGeoX.Value = m_pModel.x;
            dbiGeoY.Value = m_pModel.y;
            dbiSize.Value = m_pModel.dbSize;
            dbiDepth.Value = m_pModel.dbDepth;               

            if (m_pModel.szModelType.ToLower().Contains("crater"))
            {
                cmbModelType.SelectedItem = cmiCrater;
                txtModelID.Text = "Crater";
            }
            else
            {
                cmbModelType.SelectedItem = cmiRock;
                txtModelID.Text = "Rock";
            }
        }
    }
}
