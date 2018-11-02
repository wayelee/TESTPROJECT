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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmSelByAttribute : OfficeForm
    {
        string m_strSQL = "";
        ILayer m_pLayer = null;
        AxMapControl m_pMapCtl = null;

         public FrmSelByAttribute(AxMapControl mapCtl, ILayer layer)
        {
            InitializeComponent();
            m_pMapCtl = mapCtl;
            m_pLayer = layer;
        }

        private void FrmSelByAttribute_Load(object sender, EventArgs e)
        {
            //初始化图层列表
            if (m_pLayer == null) return;

            ITable pTable = (ITable)m_pLayer;
            IFields pFields = pTable.Fields;

            IField pField;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                pField = pFields.get_Field(i);
                if (pField.Name != "Shape")
                {
                    this.lsbFields.Items.Add(pField.Name);
                }
            }

            this.lblSelect.Text = "SELECT * FROM " + m_pLayer.Name + " WHERE";
        }


        private void BtnEqual_Click(object sender, EventArgs e)
        {
            InsertRtbText("=");
        }

        private void BtnBig_Click(object sender, EventArgs e)
        {
            InsertRtbText(">");
        }

        private void BtnSmall_Click(object sender, EventArgs e)
        {
            InsertRtbText("<");
        }

        private void BtnNotEqual_Click(object sender, EventArgs e)
        {
            InsertRtbText("<>");
        }

        private void BtnBigEqual_Click(object sender, EventArgs e)
        {
            InsertRtbText(">=");
        }

        private void BtnSmallEqual_Click(object sender, EventArgs e)
        {
            InsertRtbText("<=");
        }

        private void BtnLeftBracket_Click(object sender, EventArgs e)
        {
            InsertRtbText("(");
        }

        private void BtnRightBracket_Click(object sender, EventArgs e)
        {
            InsertRtbText(")");
        }

        private void BtnLike_Click(object sender, EventArgs e)
        {
            InsertRtbText(" LIKE ");
        }

        private void BtnAnd_Click(object sender, EventArgs e)
        {
            InsertRtbText(" AND ");
        }

        private void BtnOr_Click(object sender, EventArgs e)
        {
            InsertRtbText(" OR ");
        }

        private void BtnNot_Click(object sender, EventArgs e)
        {
            InsertRtbText(" NOT ");
        }

        private void BtnIs_Click(object sender, EventArgs e)
        {
            InsertRtbText(" IS ");
        }

        private void lsbFields_DoubleClick(object sender, EventArgs e)
        {
            this.InsertRtbText(lsbFields.SelectedItem.ToString());
        }

        private void BtnUniqeValue_Click(object sender, EventArgs e)
        {
            this.lsbUniqeValue.Items.Clear();
            if (this.lsbFields.SelectedIndex > -1)
            {
                    try
                    {                        
                        IFeatureLayer pFL = (IFeatureLayer)m_pLayer;
                        IFeatureClass pFtClass = pFL.FeatureClass;
                        IFields pFlds = pFtClass.Fields;
                        string strFld = this.lsbFields.SelectedItem.ToString();
                        int nIndex = pFlds.FindField(strFld);
                        IField pFld = pFlds.get_Field(nIndex);

                        ICursor pCursor = (ICursor)pFL.Search(null, false);
                        IDataStatistics pData = new DataStatisticsClass();
                        pData.Field = strFld;
                        pData.Cursor = pCursor;

                        System.Collections.IEnumerator pEnumer;
                        object Value = null;
                        pEnumer = (IEnumerator)pData.UniqueValues;
                        int nCount = pData.UniqueValueCount;
                        for (int i = 0; i < nCount; i++)
                        {
                            pEnumer.MoveNext();
                            Value = pEnumer.Current;
                            //再根据字段类型来设置显示
                            switch (pFld.Type)
                            {
                                case esriFieldType.esriFieldTypeString:
                                    this.lsbUniqeValue.Items.Add("'" + Value.ToString() + "'");
                                    break;
                                case esriFieldType.esriFieldTypeBlob:
                                    break;
                                case esriFieldType.esriFieldTypeDate:
                                    break;
                                case esriFieldType.esriFieldTypeGeometry:
                                    break;
                                case esriFieldType.esriFieldTypeRaster:
                                    break;
                                default:
                                    this.lsbUniqeValue.Items.Add(Value.ToString());
                                    break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
               
            }
        }

        private void lsbUniqeValue_DoubleClick(object sender, EventArgs e)
        {
            this.InsertRtbText(lsbUniqeValue.SelectedItem.ToString());
        }

        private void InsertRtbText(string strText)
        {
            int nStart = RtbSQL.SelectionStart;
            RtbSQL.Text = RtbSQL.Text.Insert(nStart, strText);
            RtbSQL.SelectionStart = nStart + strText.Length;
            RtbSQL.Focus();
        }

        private void btnuse_Click(object sender, EventArgs e)
        {

            if (m_pLayer is IFeatureLayer)
            {
                try
                {
                    m_strSQL = RtbSQL.Text;
 
                    m_pMapCtl.Map.ClearSelection();
                    IFeatureSelection pFS = (IFeatureSelection)m_pLayer;
                    IQueryFilter pFilter = new QueryFilterClass();
                    pFilter.WhereClause = m_strSQL;
                    pFS.SelectFeatures(pFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                    pFS.SelectionChanged();
  
                    m_pMapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_pLayer, null);
                    m_pMapCtl.Update();

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            

        }

        private void btnok_Click(object sender, EventArgs e)
        {
            btnuse_Click(null, null);

            this.Close();
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
