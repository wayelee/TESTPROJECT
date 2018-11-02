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
    public partial class FrmAddField : DevComponents.DotNetBar.OfficeForm
    {
        ILayer m_pLayer = null;
        public string m_strFieldName = string.Empty;
        public FrmAddField(ILayer layer)
        {
            InitializeComponent();
            m_pLayer = layer;
        }

        private void FrmAddField_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
            InitialFieldType();
            if (cmbtype.Items.Count >0)
            {
                cmbtype.SelectedIndex = 0;
            }
        }

        private void InitialFieldType()
        {
            for (int i = 0; i < 6;i++ )
            {
                string strType = ((esriFieldType)i).ToString().Replace("esriFieldType","");
                cmbtype.Items.Add(strType);

            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            m_strFieldName = txtname.Text;
            this.Close();
        }

        public int AddField(string strFieldName, esriFieldType fieldType, int nPrecision = 0, int nLength = 255)
        {
            try
            {
                if (m_pLayer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = m_pLayer as IFeatureLayer;
                    if (featureLayer.FeatureClass == null) return -1;
                    IFeatureClass featureClass = featureLayer.FeatureClass;

                    IField field = new FieldClass();
                    IFieldEdit fieldEdit = (IFieldEdit)field;
                    fieldEdit.Name_2 = strFieldName;

                    fieldEdit.Type_2 = fieldType;
                    if (fieldType == esriFieldType.esriFieldTypeSmallInteger ||
                        fieldType == esriFieldType.esriFieldTypeInteger)
                    {
                        fieldEdit.Precision_2 = nPrecision;
                    }
                    else if(fieldType == esriFieldType.esriFieldTypeSingle ||
                        fieldType == esriFieldType.esriFieldTypeDouble)
                    {
                        fieldEdit.Precision_2 = nPrecision;
                        fieldEdit.Scale_2 = nPrecision;
                    }
                    else if (fieldType == esriFieldType.esriFieldTypeString)
                    {
                        fieldEdit.Length_2 = nLength;
                    }

                    fieldEdit.IsNullable_2 = true;
                    fieldEdit.DefaultValue_2 = 0;
                    fieldEdit.Editable_2 = true;
                    featureClass.AddField(field);

                    return featureClass.Fields.FindField(strFieldName);
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int AddField(string strFieldName)
        {
            try
            {
                if (m_pLayer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = m_pLayer as IFeatureLayer;
                    if (featureLayer.FeatureClass == null) return -1; 
                    IFeatureClass featureClass = featureLayer.FeatureClass;

                    IField field = new FieldClass();
                    IFieldEdit fieldEdit = (IFieldEdit)field;
                    fieldEdit.Name_2 = strFieldName;
                    
                    esriFieldType fieldType = (esriFieldType)cmbtype.SelectedIndex;
                    fieldEdit.Type_2 = fieldType;

                    if (fieldType == esriFieldType.esriFieldTypeSmallInteger ||
                        fieldType == esriFieldType.esriFieldTypeInteger)
                    {
                        fieldEdit.Precision_2 = 0;
                    }
                    else if(fieldType == esriFieldType.esriFieldTypeSingle ||
                        fieldType == esriFieldType.esriFieldTypeDouble)
                    {
                        fieldEdit.Precision_2 = 0;
                        fieldEdit.Scale_2 = 0;
                    }
                    else if (fieldType == esriFieldType.esriFieldTypeString)
                    {
                        fieldEdit.Length_2 = int.Parse(txtLength.Text);
                    }

                    fieldEdit.IsNullable_2 = true;
                    fieldEdit.DefaultValue_2 = 0;
                    fieldEdit.Editable_2 = true;
                    featureClass.AddField(field);

                    return featureClass.Fields.FindField(strFieldName);
                }
                return -1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return -1;
            }

            return -1;

        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((esriFieldType)cmbtype.SelectedIndex == esriFieldType.esriFieldTypeString)
            {
                  txtLength.Enabled = true;
            }
             else
            {
                txtLength.Enabled = false;
            }
        }



    }
}
