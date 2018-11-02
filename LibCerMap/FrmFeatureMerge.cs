using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using DevComponents.DotNetBar.SuperGrid;
using ESRI.ArcGIS.Geometry;
using System.Collections;

namespace LibCerMap
{
    public partial class FrmFeatureMerge : OfficeForm
    {
        public int m_nFeatureID = -1;
        private int m_nIDFieldIndex = -1;

        public FrmFeatureMerge()
        {
            InitializeComponent();
        }

        private void FrmFeatureMerge_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
            InitialGridTable();
        }

        private void InitialGridTable()
        {
            superGridCtl.PrimaryGrid.ClearAll();

            IEngineEditor pEngineEdit = new EngineEditorClass();
            IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
            IFeatureLayer targetLayer = pEEditLayers.TargetLayer;

            IFeatureSelection featureSelection = targetLayer as IFeatureSelection;
            ISelectionSet selectionSet = featureSelection.SelectionSet;

            if (selectionSet.Count > 0)
            {             
                IFields fields = targetLayer.FeatureClass.Fields;
                IField field = null;
                //填充表格开头
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.get_Field(i);
                    GridColumn col = new GridColumn(field.Name);
                    col.Width = 100;
                    superGridCtl.PrimaryGrid.Columns.Add(col);

                    //记录OID字段索引
                    if (field.Type == esriFieldType.esriFieldTypeOID)
                    {
                        m_nIDFieldIndex = i;
                    }
                }
                //读取要素属性
                ICursor cursor;
                selectionSet.Search(null, true, out cursor);
                IFeatureCursor featureCursor = cursor as IFeatureCursor;
                IFeature feature = null;
                while ((feature = featureCursor.NextFeature()) != null)
                {
                    object[] obj = new object[fields.FieldCount];                
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        field = fields.get_Field(i);
                        if (field.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            obj[i] = field.Type.ToString();
                        }
                        else
                        {
                            obj[i] = feature.get_Value(i);
                        }
                    }
                    GridRow row = new GridRow(obj);
                    superGridCtl.PrimaryGrid.Rows.Add(row);
                }

                //if (superGridCtl.PrimaryGrid.Rows.Count >0)
                //{
                //    GridRow row = (GridRow)superGridCtl.PrimaryGrid.GetRowFromIndex(0);
                //    superGridCtl.PrimaryGrid.SetSelected(row, true);
                //    superGridCtl.Refresh();
                //}
            }


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //从列表得到FeatureID
            SelectedElementCollection items = superGridCtl.PrimaryGrid.GetSelectedRows();
            if (items.Count >0)
            {
                GridRow row = (GridRow)items[0];
                m_nFeatureID = Convert.ToInt32(row.Cells[m_nIDFieldIndex].Value); 
            }
            if (m_nFeatureID < 0)
            {
                MessageBox.Show("请选择一条记录！");
                this.DialogResult = DialogResult.None;
                return;
            }
        }
    }
}
