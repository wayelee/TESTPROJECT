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
using System.IO;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;

namespace LibCerMap
{
    public partial class FrmExportMxd : OfficeForm
    {
        IPageLayoutControl3 m_pPageLayoutCtl = null;
        private IMapDocument m_pMapDoc = null;
        public string m_strDocNameNew = string.Empty;
        public FrmExportMxd(IPageLayoutControl3 pageCtl)
        {
            InitializeComponent();
            m_pPageLayoutCtl = pageCtl;
        }

        private void FrmExportMxd_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder.Text = dlg.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_pPageLayoutCtl == null) return;

           string strFullpath = txtFolder.Text;
           if (strFullpath[strFullpath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
           {
               strFullpath += System.IO.Path.DirectorySeparatorChar;
           }

           string strDocName = CreateMxdFolderAndGDB(strFullpath, m_pPageLayoutCtl.DocumentFilename);

           string strFullGdbName = strFullpath + strDocName + ".gdb";
           string strFullDocName = strFullpath + strDocName + ".mxd";
           //另存地图文档
           ClsGDBDataCommon cls = new ClsGDBDataCommon();
           m_pMapDoc = cls.SaveAsDocument(m_pPageLayoutCtl, strFullDocName);
            //n
           if (ExportLayers(m_pMapDoc, strFullGdbName, radioLayer.Checked))
           {
               m_pMapDoc.Save(true, false);
               m_strDocNameNew = m_pMapDoc.DocumentFilename;
               m_pMapDoc.Close();

               MessageBox.Show("导出成功！");
           }
        }

        private bool ExportLayers(IMapDocument mapDoc, string strGdbName,bool bLayerSpatial)
        {
            //打开GDB工作空间
            ClsGDBDataCommon cls = new ClsGDBDataCommon();
            //IWorkspace wks = (IWorkspace)cls.OpenFromFileGDB(strGdbName);
            IWorkspace2 wksFeature = (IWorkspace2)cls.OpenFromFileGDB(strGdbName);
            if (wksFeature == null) return false;

            IWorkspaceFactory pWSF = new RasterWorkspaceFactoryClass();
            IWorkspace wksRaster =pWSF.OpenFromFile(System.IO.Path.GetDirectoryName(mapDoc.DocumentFilename), 0);
            if (wksRaster == null) return false;

            for (int i = 0; i < mapDoc.MapCount; i++)
            {
                IMap map = mapDoc.get_Map(i);
                if (map == null) continue;
                ILayer layer = null;
                IEnumLayer pEnumLayer = map.get_Layers(null, true);
                pEnumLayer.Reset();
                while ((layer = pEnumLayer.Next()) != null)
                {
                    //只导出可见图层
                    if (chkExportVisible.Checked ==true)
                    {
                        if (layer.Visible == false)
                        {
                            map.DeleteLayer(layer);
                            continue;
                        }
                    }

                    this.labelExport.Text = "导出：" + layer.Name;
                    this.labelExport.Refresh();
                    if (layer is IFeatureLayer)
                    {
                        IFeatureClass featureClassNew = CopyFeatureClassToWorkspace(map, layer, wksFeature, bLayerSpatial);
                        if (featureClassNew != null)
                        {
                            //拷贝完成重设数据源
                            cls.SetdataSourceOfFeatureLayer(layer, featureClassNew);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (layer is IRasterLayer)
                    {
                        IRasterDataset rasterDatasetNew = CopyRasterToWorkspace(map, layer, wksRaster, bLayerSpatial);
                        if (rasterDatasetNew != null)
                        {
                            //拷贝完成重设数据源
                            IRasterLayer rasterLayer = layer as IRasterLayer;
                            rasterLayer.CreateFromDataset(rasterDatasetNew);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            this.labelExport.Text = "导出完成!";
            return true;
        }

        private IFeatureClass CopyFeatureClassToWorkspace(IMap map, ILayer layer, IWorkspace2 wksDst, bool bLayerSpatial)
        {
            IFeatureClass pFtClassNew = null;
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer.FeatureClass == null) return null;
               
                IFeatureClass featureClass = featureLayer.FeatureClass;
                //注记图层
                //if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation) return null;

                //设置空间参考
                ISpatialReference spatialRef;
                if (bLayerSpatial)
                {
                    IGeoDataset pGeo = (IGeoDataset)featureLayer;
                    spatialRef = pGeo.SpatialReference;
                }
                else
                {
                    spatialRef = map.SpatialReference;
                }
                //打开GDB的WORKSPACE
               
                ClsGDBDataCommon cls = new ClsGDBDataCommon();
                //字段拷贝
                //创建fields
                IFields pFields = ClsGDBDataCommon.CopyFeatureField(featureClass, spatialRef);
                if (pFields == null)
                {
                    MessageBox.Show("拷贝图形字段失败！");
                    return null;
                }
                //创建FeatureClass
                //创建新的FeatureClass
                //注记图层
                if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    pFtClassNew = cls.CreateAnnotationClass(featureClass, layer.Name,  wksDst, spatialRef);
                }
                else
                {
                    pFtClassNew = cls.CreateFeatureClass(wksDst, null, layer.Name, pFields, null, null, null, true, featureClass.FeatureType);
                }

                if (pFtClassNew == null)
                {
                    MessageBox.Show("创建shp文件失败!");
                    return null;
                }
                //拷贝Feature
                if (ClsGDBDataCommon.CopyFeatures(featureLayer, pFtClassNew, false) == false)
                {
                    MessageBox.Show("拷贝失败！");
                }

            }
            return pFtClassNew ;
        }
        private IRasterDataset CopyRasterToWorkspace(IMap map, ILayer layer, IWorkspace wksDst,bool bLayerSpatial)
        {
            if (layer is IRasterLayer)
            {
                IRasterLayer rasterLayer = layer as IRasterLayer;
                if (rasterLayer.Raster == null) return null;

                IRaster rasterSrc = rasterLayer.Raster;
                //设置空间参考
                ISpatialReference spatialRef;
                if (bLayerSpatial)
                {
                    IGeoDataset pGeo = (IGeoDataset)rasterLayer;
                    spatialRef = pGeo.SpatialReference;
                }
                else
                {
                    spatialRef = map.SpatialReference;
                }

                ClsGDBDataCommon cls = new ClsGDBDataCommon();
                return cls.ExportRasterToWorkspace(rasterSrc, wksDst, layer.Name, spatialRef);
            }
            return null;
        }

        private string CreateMxdFolderAndGDB(string fulPath,string docName)
        {
            //在指定文件夹中创建与地图文档同名的GDB，如果地图文档名称为空，创建与文件夹同名的GDB
            DirectoryInfo path = new DirectoryInfo(fulPath);
            if (path == null) return string.Empty;
            if(!path.Exists) path.Create();
            
            if (string.IsNullOrEmpty(docName)) 
            {
                docName = path.Name;
            }
            else
            {
                docName = System.IO.Path.GetFileNameWithoutExtension(docName);
            }
            
            //创建GDB
            ClsGDBDataCommon cls = new ClsGDBDataCommon();
            if (!cls.Create_FileGDB(path.FullName, docName))
            {
                MessageBox.Show("创建数据库失败！");
                return string.Empty ;
            }
           
            return docName;
        }
    }
}
