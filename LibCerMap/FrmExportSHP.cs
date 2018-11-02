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
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesFile;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmExportSHP : OfficeForm
    {
        private ILayer m_pLayer = null;
        private IBasicMap m_pMap = null;

        public FrmExportSHP(IBasicMap map, ILayer layer)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pMap = map;
            m_pLayer = layer;
        }

        private void FrmExportSHP_Load(object sender, EventArgs e)
        {
            cmbData.SelectedIndex = 1;
            rdoLayer.Checked = true;
        }     

        public void ExportDatatoShape()
        {
            ClsGDBDataCommon comm = new ClsGDBDataCommon();
            if (!txtOutData.Text.EndsWith("shp"))
            {
                //timerShow.Start();
                MessageBox.Show("输出文件名不是shp文件!");
                return;
            }
            //
            String strFullName = txtOutData.Text;
            string strPath = System.IO.Path.GetDirectoryName(strFullName);
            string strName = System.IO.Path.GetFileName(strFullName);

            //导出数据
            IFeatureLayer pFtlayer = (IFeatureLayer)m_pLayer;
            IFeatureClass pFtClass = pFtlayer.FeatureClass;
            //IFields pFields = pFtClass.Fields;
            //设置空间参考
            ISpatialReference pSpatialRef;
            if (rdoLayer.Checked)
            {
                IGeoDataset pGeo = (IGeoDataset)pFtlayer;
                pSpatialRef = pGeo.SpatialReference;
            }
            else
            {
                pSpatialRef = m_pMap.SpatialReference;
            }

            //创建fields
            IFields pFields = ClsGDBDataCommon.CopyFeatureField(pFtClass, pSpatialRef);
            if ( pFields==null)
            {
                MessageBox.Show("拷贝图形字段失败！");
                return;
            }

#region 

            //IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

            ////设置字段   
            //IField pField = new FieldClass();
            //IFieldEdit pFieldEdit = (IFieldEdit)pField;
            //pFieldEdit.Name_2 = "shape";
            //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            //IGeometryDef pGeoDef = new GeometryDefClass();
            //IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            //pGeoDefEdit.GeometryType_2 = pFtClass.ShapeType;
            //pGeoDefEdit.SpatialReference_2 = pSpatialRef;
            //pGeoDefEdit.HasM_2 = false;
            //pGeoDefEdit.HasZ_2 = false;
            //pFieldEdit.GeometryDef_2 = pGeoDef;
            //pFieldEdit.IsNullable_2 = true;
            //pFieldEdit.Required_2 = true;
            //pFieldsEdit.AddField(pField);
            //复制pFtClass的字段给fields


            //IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            //for (int i = 0; i < pFtClass.Fields.FieldCount; i++)
            //{
            //    if (pFtClass.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry &&
            //       pFtClass.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeOID)
            //    {
            //        IField pfieldcopy = new FieldClass();
            //        pfieldcopy = pFtClass.Fields.get_Field(i);
            //        pFieldsEdit.AddField(pfieldcopy);
            //    }
            //}
#endregion
            //创建新的FeatureClass
            IFeatureClass pFtClassNew = comm.CreateShapefile(strPath, strName, pFields, pSpatialRef);
            if (pFtClassNew == null)
            {
                MessageBox.Show("创建shp文件失败!");
                return;
            }
            bool bSel =false;
            if (cmbData.SelectedIndex == 0)
            {
                bSel =true;
            }
            if (ClsGDBDataCommon.CopyFeatures(pFtlayer, pFtClassNew, bSel) == false)
            {
                MessageBox.Show("拷贝失败！");
            }
#region 拷贝Feature

            ////
            ////////////////////////////////////////////////////////////////////////////
            //IDataset dataset = (IDataset)pFtClassNew;
            //IWorkspace workspace = dataset.Workspace;

            ////Cast for an IWorkspaceEdit
            //IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;
            //workspaceEdit.StartEditing(true);
            //workspaceEdit.StartEditOperation();

            //IFeatureBuffer featureBuffer;
            //IFeatureCursor featureCursor = null;
            //object featureOID;

            ////////////////////////////////////////////////////////////////////////////

            //IFeature pFeature;
            //long nCount;
            //long nProcess = 0;
            //if (cmbData.SelectedIndex == 0)//选中要素
            //{
            //    IFeatureSelection pFtSel = (IFeatureSelection)pFtlayer;
            //    ISelectionSet pSelSet = pFtSel.SelectionSet;
            //    nCount = pSelSet.Count;
            //    if (nCount < 1)
            //    {
            //        //timerShow.Start();
            //        MessageBox.Show("没有选中对象！");
            //        return;
            //    }
            //    ICursor pCursor;
            //    pSelSet.Search(null, false, out pCursor);
            //    IFeatureCursor pFtCur = (IFeatureCursor)pCursor;
            //    pFeature = pFtCur.NextFeature();

            //    while (pFeature != null)
            //    {
            //        //////////////////////////////////////////////////////////////////////////
            //        featureBuffer = pFtClassNew.CreateFeatureBuffer();
            //        featureCursor = pFtClassNew.Insert(true);
            //        featureBuffer.Shape = pFeature.ShapeCopy;
            //        ClsGDBDataCommon.CopyFeatureFieldValue(pFeature, (IFeature)featureBuffer);
            //        featureOID = featureCursor.InsertFeature(featureBuffer);
            //        //featureCursor.Flush();

            //        nProcess++;
            //        this.Text = string.Format("共有:{0}条数据，已处理:{1}条", nCount, nProcess);
            //        pFeature = pFtCur.NextFeature();
            //    }

            //}
            //else//全部要素
            //{
            //    ITable pTable = (ITable)m_pLayer;
            //    nCount = pTable.RowCount(null);

            //    IFeatureCursor pFtCursor = pFtlayer.Search(null, true);
            //    if (pFtCursor == null) return;
            //    pFeature = pFtCursor.NextFeature();
            //    while (pFeature != null)
            //    {
            //        //////////////////////////////////////////////////////////////////////////
            //        featureBuffer = pFtClassNew.CreateFeatureBuffer();
            //        featureCursor = pFtClassNew.Insert(true);

            //        featureBuffer.Shape = pFeature.ShapeCopy;
            //        ClsGDBDataCommon.CopyFeatureFieldValue(pFeature, (IFeature)featureBuffer);
            //        featureOID = featureCursor.InsertFeature(featureBuffer);

            //        nProcess++;
            //        this.Text = string.Format("共有:{0}条数据，已处理:{1}条", nCount, nProcess);
            //        pFeature = pFtCursor.NextFeature();
            //    }
            //}
            //featureCursor.Flush();
            //workspaceEdit.StopEditOperation();
            //workspaceEdit.StopEditing(true);
#endregion
            //将导出数据添加到地图
            IFeatureLayer pOutFL = new FeatureLayerClass();
            pOutFL.FeatureClass = pFtClassNew;
            pOutFL.Name = pFtClassNew.AliasName;
            m_pMap.AddLayer(pOutFL);
            
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ExportDatatoShape();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnWorkBrowse_Click(object sender, EventArgs e)
        {
            saveFileDlg.Filter = "*.shp|*.shp";
            saveFileDlg.FileName = m_pLayer.Name + "Export.shp";
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                txtOutData.Text = saveFileDlg.FileName;
            }
        }

        private void rdoLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLayer.Checked==false)
            {
                rdoWorkspace.Checked = true;
            } 
            else
            {
                rdoWorkspace.Checked = false;
            }
            
        }

        private void rdoWorkspace_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoWorkspace.Checked==false)
            {
                rdoLayer.Checked = true;
            } 
            else
            {
                rdoLayer.Checked = false;
            }
        }
    }
}
