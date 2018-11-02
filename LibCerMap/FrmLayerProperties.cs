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
using ESRI.ArcGIS.Geodatabase;
using System.IO;
using DevComponents.DotNetBar.SuperGrid;
using ESRI.ArcGIS.DataSourcesRaster;
using DevComponents.AdvTree;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace LibCerMap
{
    public partial class FrmLayerProperties : OfficeForm
    {
        private ILayer m_pLayer = null;
        public FrmLayerProperties(ILayer layer)
        {
            InitializeComponent();
            m_pLayer = layer;
        }

        private void FrmLayerProperties_Load(object sender, EventArgs e)
        {
            //获取数据源
            txtBoxDataSource.Text = GetLayerDataSource(m_pLayer);
            
            ReadLayerProperties(m_pLayer, advTreeProps);
            ReadLayerTabelField(m_pLayer, this.superGridFields);
            txtCoordinateSystem.Text = ReadLayerCoordinateSystem(m_pLayer);
            txtCoordinateSystem.Update();
        }

        //读取图层坐标系统
        private string ReadLayerCoordinateSystem(ILayer layer)
        {
            string strCoord = string.Empty;
            if (layer is IGeoDataset)
            {
                IGeoDataset geoDataset = layer as IGeoDataset;
                if (geoDataset == null) return strCoord;
                ISpatialReference reference = geoDataset.SpatialReference;

                strCoord = ClsGDBDataCommon.GetReferenceString(reference);
            }
            return strCoord;
        }
        //读取图层属性表
        private void ReadLayerTabelField(ILayer layer,SuperGridControl superGrid)
        {
            superGridFields.PrimaryGrid.ClearAll();
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer.FeatureClass == null) return;
                IFields fields = featureLayer.FeatureClass.Fields;
                
                for (int i = 0; i < fields.FieldCount;i++ )
                {
                    IField field = fields.get_Field(i);

                    GridRow row = new GridRow(field.Name, field.Type.ToString().Replace("esriFieldType", ""), field.Length.ToString());
                    superGrid.PrimaryGrid.Rows.Add(row);
                }
            }
            else if (layer is IRasterLayer)
            {
                ITable table = ClsGDBDataCommon.GetTableofLayer(m_pLayer);
                if (table !=null)
                {
                    IFields fields = table.Fields;
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        IField field = fields.get_Field(i);

                        GridRow row = new GridRow(field.Name, field.Type.ToString().Replace("esriFieldType", ""), field.Length.ToString());
                        superGrid.PrimaryGrid.Rows.Add(row);
                    }
                }
            }
        }
        //获取数据源属性
        private string GetLayerDataSource(ILayer layer)
        {
            string strDataSource = string.Empty;

            if (layer is IDataLayer)
            {
                IDataLayer dataLayer = layer as IDataLayer;
                IDatasetName dataSetName = dataLayer.DataSourceName as IDatasetName;
                if (dataSetName != null)
                {
                    IWorkspaceName workspaceName = dataSetName.WorkspaceName;
                    if (workspaceName != null)
                    {
                        strDataSource += @"数据源类型    ：" + workspaceName.Category + "\r\n\r\n";
                        strDataSource += @"工作空间      ：" + workspaceName.PathName + "\r\n\r\n";
                        strDataSource += @"数据源名称    ：" + dataSetName.Name + "\r\n\r\n";
                    }
                }
            }

            return strDataSource;
        }

        //获取图层属性
        private void ReadLayerProperties(ILayer layer, AdvTree tree)
        {
            tree.BeginUpdate();
            tree.Nodes.Clear();
            if (layer is IFeatureLayer)
            {
                ILayerGeneralProperties layerProperties = layer as ILayerGeneralProperties;
                AddChildNode(tree.Nodes, "LastMaximumScale",layerProperties.LastMaximumScale.ToString());
                AddChildNode(tree.Nodes, "LastMinimumScale", layerProperties.LastMinimumScale.ToString());
                AddChildNode(tree.Nodes, "LayerDescription ", layerProperties.LayerDescription);
            }
            else if (layer is IRasterLayer)
            {
                IRasterLayer rasterLayer = layer as IRasterLayer;
                if (rasterLayer.Raster !=null)
                {
                    IRaster raster = rasterLayer.Raster;
                    IRaster2 raster2 = raster as IRaster2;
                    IRasterBandCollection coll = raster2 as IRasterBandCollection;
                    IRasterProps rasterProps = raster2 as IRasterProps;
                    //基本信息
                    Node node = AddChildNode(tree.Nodes, "基本信息", null); 
                    AddChildNode(node.Nodes, "行数，列数", rasterProps.Height.ToString() + "," + rasterProps.Width.ToString());     
                    AddChildNode(node.Nodes, "波段数", coll.Count.ToString());
                    AddChildNode(node.Nodes, "像元大小（x,y)",rasterProps.MeanCellSize().X.ToString() + "," + rasterProps.MeanCellSize().Y.ToString());
                    AddChildNode(node.Nodes, "波段数", coll.Count.ToString());
                    AddChildNode(node.Nodes, "像素类型", rasterProps.PixelType.ToString());
                    AddChildNode(node.Nodes, "无效值", ClsGetCameraView.getNoDataValue(rasterProps.NoDataValue).ToString());

                    node = AddChildNode(tree.Nodes, "坐标范围", null);
                    AddChildNode(node.Nodes, "上", rasterProps.Extent.YMax.ToString());
                    AddChildNode(node.Nodes, "左", rasterProps.Extent.XMin.ToString());
                    AddChildNode(node.Nodes, "右", rasterProps.Extent.XMax.ToString());
                    AddChildNode(node.Nodes, "下", rasterProps.Extent.YMin.ToString());

                    node = AddChildNode(tree.Nodes, "统计信息", null);
                    ClsGDBDataCommon cls = new ClsGDBDataCommon();
                    for (int i = 0; i < coll.Count;i++ )
                    {
                        double[] dValue = cls.GetRasterStatistics(m_pLayer,i);
                        Node nodeBand = AddChildNode(node.Nodes, "Band_" + (i+1).ToString(),null);
                        AddChildNode(nodeBand.Nodes,"最大值",dValue[0].ToString());
                        AddChildNode(nodeBand.Nodes,"最小值",dValue[1].ToString());
                        AddChildNode(nodeBand.Nodes,"平均值",dValue[2].ToString());
                        AddChildNode(nodeBand.Nodes,"标准差",dValue[3].ToString());
                    }
                    

                }
            }
            tree.EndUpdate(true);
            tree.ExpandAll();
        }

        private void BuildRasterAttributeTable(IRasterDataset rasterDataset, ITable table)
        {
            //Cast to IRasterDatasetEdit2 to build a raster attribute table.
            IRasterDatasetEdit2 rasterDatasetEdit = (IRasterDatasetEdit2)rasterDataset;

            //Build a default raster attribute table with VALUE and COUNT fields.
            if (table == null)
            {
                rasterDatasetEdit.BuildAttributeTable();
            }
            else
            {
                //Assign the given table as the raster attribute table.
                rasterDatasetEdit.AlterAttributeTable(table);
            }
        }

        private Node AddChildNode(NodeCollection parent, string strNode, string strCell)
        {
            Node node = new Node();
            node.Text = strNode;

            if (!string.IsNullOrEmpty(strCell))
            {
                Cell cell = new Cell(strCell);
                node.Cells.Add(cell);
            }           

            parent.Add(node);

            return node;
        }

        //直接更改图层坐标系统，并不重投影数据值
        private void btnChangeCoordinate_Click(object sender, EventArgs e)
        {
            ISpatialReference pSpatialReference = null;
            FrmCoordinateSystem frm = new FrmCoordinateSystem(m_pLayer);
            if (frm.ShowDialog()==DialogResult.OK)
            {
                pSpatialReference = frm.pSpaReference;           

                if (m_pLayer is IFeatureLayer)
                {
                
                    IFeatureLayer pFLayer = m_pLayer as IFeatureLayer;
                    IFeatureClass pFClass = pFLayer.FeatureClass;
                    if (pFClass == null) return;
                    IFeatureDataset pFDataset = pFClass.FeatureDataset;
                    if (pFDataset == null)
                    {
                        IGeoDatasetSchemaEdit geoSchemaEdit = pFClass as IGeoDatasetSchemaEdit;
                        if (geoSchemaEdit == null) return;

                        if (geoSchemaEdit.CanAlterSpatialReference == true)
                        {
                            geoSchemaEdit.AlterSpatialReference(pSpatialReference);
                        }
                    }
                    else
                    {
                        IGeoDatasetSchemaEdit geoSchemaEdit = pFDataset as IGeoDatasetSchemaEdit;
                        if (geoSchemaEdit == null) return;

                        if (geoSchemaEdit.CanAlterSpatialReference == true)
                        {
                            geoSchemaEdit.AlterSpatialReference(pSpatialReference);
                        }
                    }
                
                }
                else if (m_pLayer is IRasterLayer)
                {
                    IRasterLayer pRLayer = m_pLayer as IRasterLayer;                   
                    IRaster pRaster = pRLayer.Raster;
                    IRaster2 pRaster2 = pRaster as IRaster2;
                    IRasterDataset pRDataset = pRaster2.RasterDataset;
                    IGeoDatasetSchemaEdit geoSchemaEdit = pRDataset as IGeoDatasetSchemaEdit;
                    if (geoSchemaEdit == null) return;

                    if (geoSchemaEdit.CanAlterSpatialReference == true)
                    {
                         geoSchemaEdit.AlterSpatialReference(pSpatialReference);
                         pRLayer.CreateFromDataset(pRDataset);
                    }
                }


                string strCoord = string.Empty;
                if (m_pLayer is IGeoDataset)
                {
                    IGeoDataset geoDataset = m_pLayer as IGeoDataset;
                    if (geoDataset==null)
                    {
                        this.txtCoordinateSystem.Text = string.Empty;
                    }
                    else
                    {
                        ISpatialReference reference = geoDataset.SpatialReference;
                        
                        strCoord = ClsGDBDataCommon.GetReferenceString(reference);
                        txtCoordinateSystem.Text = strCoord;
                        txtCoordinateSystem.Update();
                    }
                }
            }
        }

    }//Class
}
