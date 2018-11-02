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
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmDelField : OfficeForm
    {
        ILayer m_pLayer = null;
        public FrmDelField(ILayer layer)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pLayer = layer;
        }

        private void FrmDelField_Load(object sender, EventArgs e)
        {
            if (m_pLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = m_pLayer as IFeatureLayer;
                if (featureLayer.FeatureClass == null) return ;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IFields fields = featureLayer.FeatureClass.Fields;
                IField field = null;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.get_Field(i);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry ||
                        field.Type == esriFieldType.esriFieldTypeGlobalID ||
                        field.Type == esriFieldType.esriFieldTypeGUID ||
                        field.Type == esriFieldType.esriFieldTypeOID)
                    {
                        continue;
                    }
                    else
                    {
                        cmbfields.Items.Add(fields.get_Field(i).Name);
                    }
                }
            }
            if (cmbfields.Items.Count>0)
            {
                cmbfields.SelectedIndex = 0;
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();     
        }

        public bool DeleteField()
        {
            string strFieldName = cmbfields.Text;
            if (m_pLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = m_pLayer as IFeatureLayer;
                if (featureLayer.FeatureClass == null) return false;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IFields fields = featureLayer.FeatureClass.Fields;
                int nIndex = fields.FindField(strFieldName);
                if (nIndex < 0) return false;

                try
                {
                    featureClass.DeleteField(fields.get_Field(nIndex));
                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;
                }

            }
            return false;
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
