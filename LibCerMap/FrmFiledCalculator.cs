using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Analyst3D;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmFiledCalculator : OfficeForm   
    {
        ILayer pLayer;
        public FrmFiledCalculator(ILayer layer)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pLayer = layer;
        }

        private void FrmFiledCalculator_Load(object sender, EventArgs e)
        {
            //初始化图层列表
            ITable pTable = (ITable)pLayer;
            IFields pFields = pTable.Fields;
            long nRows = pTable.RowCount(null);
            long nFields = pFields.FieldCount;

            IField pField;
            for (int i = 0; i < nFields; i++)
            {
                pField = pFields.get_Field(i);
                this.lsbFields.Items.Add(pField.Name);
                if (pField.Name != "Shape")
                {
                    cmbCalField.Items.Add(pField.Name);
                }
            }
        }

        private void InsertRtbText(string strText)
        {
            int nStart = RtbCal.SelectionStart;
            RtbCal.Text = RtbCal.Text.Insert(nStart, strText);
            RtbCal.SelectionStart = nStart + strText.Length;
            RtbCal.Focus();
        }

        private void lsbFunction_DoubleClick(object sender, EventArgs e)
        {
            this.InsertRtbText(lsbFunction.SelectedItem.ToString());
        }

        private void BtnMultply_Click(object sender, EventArgs e)
        {
            InsertRtbText("+");
        }

        private void btn_Click(object sender, EventArgs e)
        {
            InsertRtbText("-");
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            InsertRtbText("*");
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            InsertRtbText("/");
        }

        private void lsbFields_DoubleClick(object sender, EventArgs e)
        {
            //this.InsertRtbText("[" + lsbFields.SelectedItem.ToString() + "]");
            this.InsertRtbText( lsbFields.SelectedItem.ToString() );
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                //ICursor pCursor;
                //IFeatureSelection pFS = pLayer as IFeatureSelection;
                //ISelectionSet pST = pFS.SelectionSet;
                //pST.Search(null, false, out pCursor);
                //IFeatureCursor pFCursor = pCursor as IFeatureCursor;
                //IFeature pFeature = pFCursor.NextFeature();
                IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                IFeatureClass pFClass = pFLayer.FeatureClass;
                string TableName = pFClass.AliasName;
                IDataset pDSet = pFLayer as IDataset;
                IWorkspace pWorkSpace = pDSet.Workspace;
                IFeatureWorkspace pFWSpace = pWorkSpace as IFeatureWorkspace;
                string strSQL = "update " + TableName + " set " + cmbCalField.Text + "=" + RtbCal.Text;
                pWorkSpace.ExecuteSQL(strSQL);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                if (ex.ToString().Contains("未实现该方法"))
                {
                    try
                    {
                        ITable pTable=pLayer as ITable;
                        IFields pFields=pTable.Fields;
                        IField pFieldDec=null;
                        IField pFieldSor=null;
                        int nFieldDec = 0;
                        int nFieldSor = 0;
                        for (int i = 0; i < pFields.FieldCount;i++ )
                        {
                            if (pFields.get_Field(i).Name == cmbCalField.Text)
                            {
                                pFieldDec = pFields.get_Field(i);
                                nFieldDec = i;
                            }
                            if (pFields.get_Field(i).Name == RtbCal.Text)
                            {
                                pFieldSor = pFields.get_Field(i);
                                nFieldSor = i;
                            }
                        }

                        ICursor pCursor;
                        IFeatureSelection pFS = pLayer as IFeatureSelection;
                        ISelectionSet pST = pFS.SelectionSet;
                        if (pST.Count > 0)
                        {
                            pST.Search(null, false, out pCursor);
                            IFeatureCursor pFCursor = pCursor as IFeatureCursor;
                            IFeature pFeature = pFCursor.NextFeature();
                            while (pFeature != null)
                            {
                                if (pFieldSor.Type == esriFieldType.esriFieldTypeDouble)
                                {
                                    pFeature.set_Value(nFieldDec, Convert.ToSingle(pFeature.get_Value(nFieldSor)));
                                }
                                else
                                {
                                    pFeature.set_Value(nFieldDec, pFeature.get_Value(nFieldSor));
                                }
                                pFeature = pFCursor.NextFeature();
                            }
                        }
                        else
                        {
                            IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                            IFeatureCursor pFCursor = pFLayer.Search(null, false);
                            IFeature pFeature = pFCursor.NextFeature();
                            while (pFeature != null)
                            {
                                if (pFieldSor.Type == esriFieldType.esriFieldTypeDouble)
                                {
                                    pFeature.set_Value(nFieldDec, Convert.ToSingle(pFeature.get_Value(nFieldSor)));
                                }
                                else
                                {
                                    pFeature.set_Value(nFieldDec, pFeature.get_Value(nFieldSor));
                                }
                                pFeature.Store();
                                pFeature = pFCursor.NextFeature();
                            }
                        }
                    }

                    catch (System.Exception ee)
                    {
                        MessageBox.Show(ee.ToString());
                    }
                }
            }
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
