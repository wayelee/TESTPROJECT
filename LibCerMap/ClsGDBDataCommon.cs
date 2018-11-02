using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;

using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;
using System.Windows.Forms;
using System.Drawing;

namespace LibCerMap
{
    [Guid("d28419ef-2eb5-4432-84a4-4968873232d2")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AeClsLib.ClsLzqCommon")]
    public class ClsGDBDataCommon
    {
        public ClsGDBDataCommon()
        {

        }

        static public double getNoDataValue(object pObject)
        {
            double dbNoData = double.NaN;

            if (pObject is double[])
                dbNoData = Convert.ToDouble(((double[])pObject)[0]);
            else if (pObject is float[])
                dbNoData = Convert.ToDouble(((float[])pObject)[0]);
            else if (pObject is Int64[])
                dbNoData = Convert.ToDouble(((Int64[])pObject)[0]);
            else if (pObject is Int32[])
                dbNoData = Convert.ToDouble(((Int32[])pObject)[0]);
            else if (pObject is Int16[])
                dbNoData = Convert.ToDouble(((Int16[])pObject)[0]);
            else if (pObject is byte[])
                dbNoData = Convert.ToDouble(((byte[])pObject)[0]);
            else if (pObject is char[])
                dbNoData = Convert.ToDouble(((char[])pObject)[0]);
            else
                ;

            return dbNoData;
        }
        /****************************************
         * 将Icolor类型转为Color类型
         * **************************************/
        public static  Color IColorToColor(IColor PColor)
        {
            Color ccc;
            if (PColor != null)
            {
                ccc = ColorTranslator.FromOle(PColor.RGB);
                ccc = Color.FromArgb(PColor.Transparency, ccc.R, ccc.G, ccc.B);
            }
            else
            {
                ccc = Color.Empty;
            }
            return ccc;

        }
        /****************************************
        * Color类型转为IColor类型
        * **************************************/
        public static IColor ColorToIColor(Color cl)
        {
            IRgbColor rgb = new ESRI.ArcGIS.Display.RgbColorClass();
            rgb.Red = cl.R;
            rgb.Blue = cl.B;
            rgb.Green = cl.G;
            rgb.Transparency = cl.A;
            return rgb as IColor;
        }

        public static string GetDirectoryNameWithSeperator(string szFilename)
        {
            try
            {
                if (string.IsNullOrEmpty(szFilename))
                    return null;

                string szDirectoryWithSeperator = System.IO.Path.GetDirectoryName(szFilename);
                if (szDirectoryWithSeperator[szDirectoryWithSeperator.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    szDirectoryWithSeperator += System.IO.Path.DirectorySeparatorChar;
                }

                return szDirectoryWithSeperator;
            }
            catch (System.Exception ex)
            {
                return null;	
            }            
        }

        //得到执行程序的上一级目录
        public static string GetParentPathofExe()
        {
            DirectoryInfo path = new DirectoryInfo(Application.StartupPath);
            string strPath = path.ToString();
            strPath = strPath.Remove(strPath.Length - path.Name.Length);
            return strPath;
        }
        //根据图名称得到图层
        public static ILayer GetLayerFromName(IMap map, string strName)
        {      
            if (map == null) return null;

            ILayer pLayer = null;
            IEnumLayer pEnumLayer = map.get_Layers(null, true);
            pEnumLayer.Reset();
            pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                if (pLayer.Name == strName)
                {
                    break;
                }
                pLayer = pEnumLayer.Next();
            }

            return pLayer;
        }

        public static List<ILayer> GetLayersByName(IMap map, string LayerName)
        {
            if (map == null || string.IsNullOrEmpty(LayerName)) return null;

            List<ILayer> LayerList = new List<ILayer>();

            IEnumLayer pEnumLayer = map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer layer = pEnumLayer.Next();
            while (layer != null)
            {
                //Console.WriteLine(layer.Name);
                if (layer.Name == LayerName)
                {
                    LayerList.Add(layer);
                }
                layer = pEnumLayer.Next();
            }

            return LayerList;
        }

        //得到图层的属性表
        public static ITable GetTableofLayer(ILayer layer)
        {
            ITable table = null;
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                table = featureLayer as ITable;
            }
            else if (layer is IRasterLayer)
            {
                IRasterLayer rasterlayer = layer as IRasterLayer;
                IRaster2 raster = rasterlayer.Raster as IRaster2;
                IRasterProps rasterProps = raster as IRasterProps;

                if (rasterProps.PixelType == rstPixelType.PT_CHAR ||
                    rasterProps.PixelType == rstPixelType.PT_CLONG ||
                    rasterProps.PixelType == rstPixelType.PT_UCHAR ||
                    rasterProps.PixelType == rstPixelType.PT_SHORT ||
                    rasterProps.PixelType == rstPixelType.PT_ULONG ||
                    rasterProps.PixelType == rstPixelType.PT_USHORT)
                {
                    table = raster.AttributeTable;
                    if (table == null)
                    {
                        IRasterDatasetEdit2 edit2 = raster.RasterDataset as IRasterDatasetEdit2;
                        edit2.BuildAttributeTable();
                        IRasterBandCollection pBandCol = (IRasterBandCollection)raster;
                        IRasterBand pBand = pBandCol.Item(0);
                        //pBand.ComputeStatsAndHist();
                        table = pBand.AttributeTable;
                    }
                }

            }
            return table;
        }

        public double[] GetRasterStatistics(ILayer layer, int nBand)
        {
            double[] dValue = new double[4];
            ITable pTable = ClsGDBDataCommon.GetTableofLayer(layer);
            if (pTable != null)
            {
                IStatisticsResults pRst = GetDataStatistics(pTable, "Value");
                dValue[0] = pRst.Maximum;
                dValue[1] = pRst.Minimum;
                dValue[2] = pRst.Mean;
                dValue[3] = pRst.StandardDeviation;
            }
            else
            {
                IRasterLayer pRasterLayer = layer as IRasterLayer;
                IRaster pRasterInput = pRasterLayer.Raster;
                IRasterBandCollection pBandCol = (IRasterBandCollection)pRasterInput;
                IRasterBand pBand;
                if (pBandCol.Count > nBand)
                {
                    pBand = pBandCol.Item(nBand);
                    bool bHas;
                    pBand.HasStatistics(out bHas);
                    if (!bHas) pBand.ComputeStatsAndHist();
                    IRasterStatistics pRst = pBand.Statistics;
                    dValue[0] = pRst.Maximum;
                    dValue[1] = pRst.Minimum;
                    dValue[2] = pRst.Mean;
                    dValue[3] = pRst.StandardDeviation; ;
                }
            }
            return dValue;
        }

        //统计指定字段的统计值
        private IStatisticsResults GetDataStatistics(ITable table, string strField)
        {
            IQueryFilter pQuerFilter = new QueryFilterClass();
            pQuerFilter.SubFields = strField;
            ICursor pCursor = table.Search(pQuerFilter, false);

            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = strField;
            pDataStatistics.Cursor = pCursor;
            IStatisticsResults pResult = pDataStatistics.Statistics;

            return pResult;
        }

        //拷贝字段
        public static IFields CopyFeatureField(IFeatureClass pFtClass, ISpatialReference spatialRef)
        {
            IFields fields = new FieldsClass();
            try
            {             
                IFieldsEdit pFieldsEdit = (IFieldsEdit)fields;
                //设置字段   
                IField field = new FieldClass();
                IFieldEdit fieldEdit = (IFieldEdit)field;
                fieldEdit.Name_2 = "shape";
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = pFtClass.ShapeType;
                pGeoDefEdit.SpatialReference_2 = spatialRef;
                pGeoDefEdit.HasM_2 = false;
                pGeoDefEdit.HasZ_2 = false;
                fieldEdit.GeometryDef_2 = pGeoDef;
                fieldEdit.IsNullable_2 = true;
                fieldEdit.Required_2 = true;
                pFieldsEdit.AddField(field);

                for (int i = 0; i < pFtClass.Fields.FieldCount; i++)
                {
                    if (pFtClass.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry &&
                       pFtClass.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeOID)
                    {
                        IField pfieldcopy = new FieldClass();
                        pfieldcopy = pFtClass.Fields.get_Field(i);
                        pFieldsEdit.AddField(pfieldcopy);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
            return fields;
        }

        //实现字段值的拷贝
        public static bool CopyFeatureFieldValue(IFeature featureSrc, IFeature featureNew)
        {
            IFields fieldsSrc = featureSrc.Fields;
            IFields fieldsNew = featureNew.Fields;
            //注意字段拷贝时两个字段顺序可能会不一至
            IField fieldSrc = null;
            for (int i = 0; i < fieldsSrc.FieldCount; i++)
            {
                fieldSrc = fieldsSrc.get_Field(i);
                //几何字段和FID字段不拷贝
                if (fieldSrc.Type != esriFieldType.esriFieldTypeGeometry &&
                    fieldSrc.Type != esriFieldType.esriFieldTypeOID)
                {
                    //找到字段所在的索引
                    int nIndexNew = fieldsNew.FindField(fieldSrc.Name);
                    if (nIndexNew < 0) continue;
                    try
                    {
                        if (string.IsNullOrEmpty(featureSrc.get_Value(i).ToString()) &&
                            featureNew.Fields.get_Field(nIndexNew).IsNullable == false)
                        {
                            featureNew.set_Value(nIndexNew, string.Empty);
                        }
                        else
                        {
                            featureNew.set_Value(nIndexNew, (object)featureSrc.get_Value(i));
                        }
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }
                }
            }
            return true;
        } 

        //拷贝Feature
        public static bool CopyFeatures(IFeatureLayer ftSrc,IFeatureClass fcDst, bool bSelection)
        {
            IDataset dataset = (IDataset)fcDst;
            IWorkspace workspace = dataset.Workspace;

            //Cast for an IWorkspaceEdit
            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();

            IFeatureBuffer featureBuffer;
            IFeatureCursor featureCursor = null;
            object featureOID;

            //////////////////////////////////////////////////////////////////////////

            IFeature pFeature;
            long nCount;
            long nProcess = 0;
            if (bSelection)//只导出选中要素
            {
                IFeatureSelection pFtSel = (IFeatureSelection)ftSrc;
                ISelectionSet pSelSet = pFtSel.SelectionSet;
                nCount = pSelSet.Count;
                if (nCount < 1)
                {
                    //timerShow.Start();
                    MessageBox.Show("没有选中对象！");
                    return false;
                }
                ICursor pCursor;
                pSelSet.Search(null, false, out pCursor);
                IFeatureCursor pFtCur = (IFeatureCursor)pCursor;
                pFeature = null;// pFtCur.NextFeature();

                while ((pFeature=pFtCur.NextFeature()) != null)
                {
                    featureBuffer = fcDst.CreateFeatureBuffer();
                    featureCursor = fcDst.Insert(true);
                    featureBuffer.Shape = pFeature.ShapeCopy;
                    ClsGDBDataCommon.CopyFeatureFieldValue(pFeature, (IFeature)featureBuffer);
                    featureOID = featureCursor.InsertFeature(featureBuffer);
                    if (fcDst.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        IFeature featureTemp = fcDst.GetFeature(Convert.ToInt32(featureOID));
                        IAnnotationFeature2 annoFeature = pFeature as IAnnotationFeature2;
                        IAnnotationFeature2 annoFeatureBuffer = featureTemp as IAnnotationFeature2;
                        annoFeatureBuffer.Annotation = annoFeature.Annotation;
                        annoFeatureBuffer.AnnotationClassID = annoFeature.AnnotationClassID;
                        annoFeatureBuffer.LinkedFeatureID = annoFeature.LinkedFeatureID;
                    }
                }

            }
            else//全部要素
            {
                ITable pTable = (ITable)ftSrc;
                nCount = pTable.RowCount(null);
                if (nCount < 1) return true;

                IFeatureCursor pFtCursor = ftSrc.Search(null, true);
                if (pFtCursor == null) return false;
                pFeature = null;

                while ((pFeature = pFtCursor.NextFeature()) != null)
                {
                    featureBuffer = fcDst.CreateFeatureBuffer();
                    featureCursor = fcDst.Insert(true);

                    featureBuffer.Shape = pFeature.ShapeCopy;
                    ClsGDBDataCommon.CopyFeatureFieldValue(pFeature, (IFeature)featureBuffer);

                    featureOID = featureCursor.InsertFeature(featureBuffer);
                    if (fcDst.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        IFeature featureTemp = fcDst.GetFeature(Convert.ToInt32(featureOID));
                        IAnnotationFeature2 annoFeature = pFeature as IAnnotationFeature2;
                        IAnnotationFeature2 annoFeatureBuffer = featureTemp as IAnnotationFeature2;
                        annoFeatureBuffer.Annotation = annoFeature.Annotation;
                        annoFeatureBuffer.AnnotationClassID = annoFeature.AnnotationClassID;
                        annoFeatureBuffer.LinkedFeatureID = annoFeature.LinkedFeatureID;
                    }
                }

            }
            featureCursor.Flush();
             //判断是否为注记图层
        
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);
            return true;
        }

        //设置注记图层
        public void SetAnnotationFeatureProps()
        {

        }
        //导出栅格图像为TIF
        public IRasterDataset ExportRasterToWorkspace(IRaster rasterSrc, IWorkspace wksDst, string strRasterName, ISpatialReference spatialRef)
        {
            IRasterDataset rasterDatasetNew = null;
            try
            {
                //导出时要保持分辨率和行列数
                IRaster2 raster2 = rasterSrc as IRaster2;
                IRasterProps rasterProps = raster2 as IRasterProps;

                IRasterBandCollection bandsNew = raster2 as IRasterBandCollection;
                IRasterBand pBand = null;
                IRasterBandCollection rasterBands = raster2.RasterDataset as IRasterBandCollection;

                for (int i = 3; i < rasterBands.Count; i++)
                {
                    pBand = rasterBands.Item(i);
                    bandsNew.AppendBand(pBand);
                }
                //设置空间参考
                rasterProps.SpatialReference = spatialRef;

                ISaveAs2 pSaveAs = rasterProps as ISaveAs2;               
                IRasterStorageDef pRSDef = new RasterStorageDefClass();
                IRasterStorageDef2 pRsDef2 = pRSDef as IRasterStorageDef2;
                //判断工作空间中是否存在重名栅格
                IWorkspace2 wks2 = wksDst as IWorkspace2;
               
                int nTmp = 0;
                
              
                string strNameTemp = System.IO.Path.GetFileNameWithoutExtension(strRasterName);
                string strFullName = wksDst.PathName + strNameTemp + ".tif";
                while (true)
                {
                    bool bFlag = false;
                    if (wks2 != null)
                    {
                        bFlag = wks2.get_NameExists(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTRasterDataset, System.IO.Path.GetFileNameWithoutExtension(strFullName));
                    }
                    else
                    {
                        bFlag = System.IO.File.Exists(strFullName);
                    }
                    
                    if (bFlag) //feature class with that name already exists 
                    {
                        strFullName = wksDst.PathName + strNameTemp + ((nTmp++).ToString()) +".tif";
                    }
                    else
                        break;
                }

                pRsDef2.PyramidResampleType = rstResamplingTypes.RSP_BilinearInterpolation;
                rasterDatasetNew = pSaveAs.SaveAsRasterDataset(strFullName, wksDst, "TIFF", pRSDef);
                IRasterDatasetEdit3 rasterEdit3 = rasterDatasetNew as IRasterDatasetEdit3;
                //rasterEdit3.DeleteStats();//This method is avaliable only on raster datasets in File and ArcSDE geodatabases.
                rasterEdit3.ComputeStatisticsHistogram(1, 1, null, true);

                return rasterDatasetNew;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

        }
        //
        //set the layer to new data source
        public void SetDataSource(ILayer layerOld, ILayer layerSrc)
        {
            if (layerSrc ==null)
            {
                return;
            }
            if (layerOld is IFeatureLayer)
            {
                IFeatureLayer pFLOld = layerOld as IFeatureLayer;
                pFLOld.FeatureClass = ((IFeatureLayer)layerSrc).FeatureClass;
                //mapCtl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            else if (layerOld is IRasterLayer)
            {
                IRasterLayer pRLOld = layerOld as IRasterLayer;
                pRLOld.CreateFromRaster(((IRasterLayer)layerSrc).Raster);
            }
        }

        public bool SetdataSourceOfFeatureLayer(ILayer layerOld, IFeatureClass featureClass)
        {
            if (featureClass == null) return false;
            //if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation) return false;

            if (layerOld is IFeatureLayer)
            {
                IFeatureLayer pFLOld = layerOld as IFeatureLayer;
                pFLOld.FeatureClass = featureClass;
                layerOld.Name = featureClass.AliasName;
            }
            return true;
        }
        //Repair null data source of mapcontrol
        public void RepairDataSource(IBasicMap map, IWorkspace wksSrc)
        {
            ILayer layerOld = null;

            IEnumLayer pEnumLayer = map.get_Layers(null, true);
            pEnumLayer.Reset();
            layerOld = pEnumLayer.Next();
            while (layerOld != null)
            {
                //Console.WriteLine(layerOld.Name);
                RepairData(layerOld, wksSrc);
                layerOld = pEnumLayer.Next();
            }

        }

        public void RepairData(ILayer layerOld, IWorkspace wksSrc)
        {
            IEnumDataset pEnumDatasetSrc = null;
            if (layerOld is IFeatureLayer)
            {
                IFeatureLayer pFlOld = layerOld as IFeatureLayer;
                if (pFlOld.FeatureClass == null)
                {
                    IDataset dtOld = pFlOld as IDataset;
                    pEnumDatasetSrc = wksSrc.get_Datasets(dtOld.Type);
                    IDataset dtSrc = pEnumDatasetSrc.Next();

                    while (dtSrc != null)
                    {
                        if (dtSrc.BrowseName == dtOld.BrowseName)
                        {
                            pFlOld.FeatureClass = dtSrc as IFeatureClass;
                            break;
                        }
                        dtSrc = pEnumDatasetSrc.Next();
                    }
                }
            }
            else if (layerOld is IRasterLayer)
            {
                IRasterLayer pRlOld = layerOld as IRasterLayer;
                if (pRlOld.Raster == null)
                {
                    IDataset dtOld = pRlOld as IDataset;
                    if (dtOld == null) return;
  
                    pEnumDatasetSrc = wksSrc.get_Datasets(dtOld.Type);
                    IDataset dtSrc = pEnumDatasetSrc.Next();
                    IRasterDataset rasterDt = dtSrc as IRasterDataset;

                    while (rasterDt != null)
                    {
                        if (dtSrc.BrowseName == null || dtOld.BrowseName == null) continue;
  
                        if (dtSrc.BrowseName == dtOld.BrowseName)
                        {
                            string szOldName = dtOld.Name;
                            pRlOld.CreateFromDataset(rasterDt);

                            //把显示名字改成原来的
                            pRlOld.Name = szOldName;// dtSrc.Name;
                            //dtOld = pRlOld as IDataset;   
                            //dtOld.BrowseName = dtSrc.BrowseName;
                            break;
                        }
                        dtSrc = pEnumDatasetSrc.Next();
                        rasterDt = dtSrc as IRasterDataset;
                    }
                }
            }
        }
        public void CreateArcinfoWork(string strPath,string strName)
        {
            ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySetClass();
            propertySet.SetProperty("DATABASE", strPath);
            IWorkspaceFactory workspaceFactory = new ArcInfoWorkspaceFactoryClass();
            IWorkspaceName workspace = workspaceFactory.Create(strPath, strName, propertySet, 0);
        }

        public IWorkspace OpenArcinfoWork(String strPath)
        {
            try
            {
                ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySetClass();
                propertySet.SetProperty("DATABASE", strPath);
                IWorkspaceFactory workspaceFactory = new ArcInfoWorkspaceFactoryClass();
                return workspaceFactory.Open(propertySet, 0);
            }
            catch
            {
                return null;
            }
        }

        public IMapDocument SaveAsDocument(IPageLayoutControl3 pageCtl, string sFilePath)
        {
            IMapDocument m_pDoc = null;
            try
            {
                m_pDoc = new MapDocumentClass(); //地图文档对象
                m_pDoc.New(sFilePath);
                m_pDoc.ReplaceContents((IMxdContents)pageCtl.PageLayout);

                m_pDoc.Save(true, true);
                pageCtl.DocumentFilename = sFilePath;
                //m_pDoc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("另存地图文档失败", ex.Message);
                return null;
            }

            return m_pDoc;
        }
        public bool Create_FileGDB(String strPath,String strName)
        {       
            try
            {
                // create a new FileGDB workspace factory        
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                // Create a workspacename with the workspace factory        
                IWorkspaceName workspaceName = workspaceFactory.Create(strPath + "\\", strName, null, 0);
                // Cast for IName        
                //ESRI.ArcGIS.esriSystem.IName name = (IName)workspaceName;
                //Open a reference to the FileGDB workspace through the name object        
                //IWorkspace fileGDB_workspace = (IWorkspace)name.Open();
                //Console.WriteLine("Current path of the {0} is {1}", fileGDB_workspace.Type, fileGDB_workspace.PathName);
            }
            catch
            {
                return false;
            }
            return true;
        } 
        //e.g., nameOfFile = "E:\\data\\english\\access\\canada\\canada.mdb"    
        public IWorkspace OpenFromFileAccess(string nameOfFile)    
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactoryClass();
                return workspaceFactory.OpenFromFile(nameOfFile, 0);
            }
            catch (Exception ex) { return null; }
        }

        //e.g., nameOfFile = "E:\\data\\english\\FileGDB\\canada\\canada.gdb"
        public IWorkspace OpenFromFileGDB(string nameOfFile) 
        { 
            try
            {
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                return workspaceFactory.OpenFromFile(nameOfFile, 0); 
            }
            catch (Exception ex){return null;}

        }

        //e.g., nameOfFile = "D:\\data\\redarrow.sde"    
        public IWorkspace OpenFromFileArcSDE(string nameOfFile)    
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new SdeWorkspaceFactoryClass();
                return workspaceFactory.OpenFromFile(nameOfFile, 0);
            }
            catch (Exception ex) { return null; }
        }

        //The connection string for a shapefile should be the full path     
        //to the Shapefile's folder (e.g., "C:\\temp")    
        // Note: location cannot be relative path ".\\temp" it must be an explicit pathname    
        public IWorkspace OpenFromShapefile(string nameOfFile)    
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
                return workspaceFactory.OpenFromFile(nameOfFile, 0);    
            }
            catch (Exception ex){return null;}
        }

        public IFeatureClass CreateShapefile(String strPath, String strName, IFields pFields, ISpatialReference pSpatialRef)
        {
            IFeatureClass pFtClass=null;
            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IWorkspace2 pWorkspace2 = pWSF.OpenFromFile(strPath, 0) as IWorkspace2;
            IFeatureWorkspace pFWS = pWorkspace2 as IFeatureWorkspace;

            //已经存在，直接打开返回
            if (pWorkspace2.get_NameExists(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureClass, strName)) //feature class with that name already exists 
            {
                pFtClass = pFWS.OpenFeatureClass(strName);
                return pFtClass;
            }

            //创建新的要素类
            pFtClass = pFWS.CreateFeatureClass(strName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");            
            return pFtClass;
        }
        public static ISpatialReference CreateProjectedCoordinateSystem()
        {
            ISpatialReference pSpatialReference = new UnknownCoordinateSystemClass();
            return pSpatialReference;
        }

        #region 创建坐标系统 Sinusoidal

        //public static IProjectedCoordinateSystem CreateProjectedCoordinateSystem()
        //{
        //    // Set up the SpatialReferenceEnvironment.
        //    // SpatialReferenceEnvironment is a singleton object and needs to use the Activator class.
        //    Type factoryType = Type.GetTypeFromProgID("esriGeometry.SpatialReferenceEnvironment");
        //    System.Object obj = Activator.CreateInstance(factoryType);
        //    ISpatialReferenceFactory3 spatialReferenceFactory = obj as ISpatialReferenceFactory3;

        //    // Create a projection, GeographicCoordinateSystem, and unit using the factory.
        //    IProjectionGEN projection = spatialReferenceFactory.CreateProjection((int)esriSRProjectionType.esriSRProjection_Sinusoidal) as IProjectionGEN;
        //    IGeographicCoordinateSystem geographicCoordinateSystem =
        //        spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCS3Type.esriSRGeoCS_TheMoon);
        //    ILinearUnit unit = spatialReferenceFactory.CreateUnit((int)esriSRUnitType.esriSRUnit_Meter) as ILinearUnit;

        //    // Get the default parameters from the projection.
        //    IParameter[] parameters = projection.GetDefaultParameters();

        //    // Create a PCS using the Define method.
        //    IProjectedCoordinateSystemEdit projectedCoordinateSystemEdit = new ProjectedCoordinateSystemClass();
        //    object name = "Coustom";
        //    object alias = "Coustom";
        //    object abbreviation = "Coustom";
        //    object remarks = "Coustom";
        //    object usage = "Coustom";
        //    object geographicCoordinateSystemObject = geographicCoordinateSystem as object;
        //    object unitObject = unit as object;
        //    object projectionObject = projection as object;
        //    object parametersObject = parameters as object;


        //    projectedCoordinateSystemEdit.Define(ref name, ref alias, ref abbreviation, ref remarks, ref usage, ref geographicCoordinateSystemObject,
        //                                                                       ref unitObject, ref projectionObject, ref parametersObject);

        //    IProjectedCoordinateSystem userDefinedProjectCoordinateSystem =projectedCoordinateSystemEdit as IProjectedCoordinateSystem;

        //    return userDefinedProjectCoordinateSystem;

        //}
#endregion

        public void DelSystemTempFiles()
        {
            try{
                string strTempPath = System.IO.Path.GetTempPath();
                //删除内部文件
                DirectoryInfo dirInfo = new DirectoryInfo(strTempPath);
                FileInfo[] fileInfos = dirInfo.GetFiles();
                if (fileInfos != null && fileInfos.Length > 0)
                {
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        //DateTime.Compare( fileInfo.LastWriteTime,DateTime.Now);
                        try{
                            File.Delete(fileInfo.FullName); //删除文件
                        }
                        catch (Exception ex) { }
                    }
                }

                DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
                if (dirInfos != null && dirInfos.Length > 0)
                {
                    foreach (DirectoryInfo childDirInfo in dirInfos)
                    {
                        try{
                            this.DeleteInDir(childDirInfo.FullName.ToString()); //递归
                        }
                        catch (Exception ex) { }

                    }
                }

            }
            catch(Exception ex){}
        }

        //删除目录及内部所有文件
        public void DeleteInDir(string szDirPath)
        {
            if (szDirPath.Trim() == "" || !Directory.Exists(szDirPath))
                return;
            DirectoryInfo dirInfo = new DirectoryInfo(szDirPath);

            FileInfo[] fileInfos = dirInfo.GetFiles();
            if (fileInfos != null && fileInfos.Length > 0)
            {
                foreach (FileInfo fileInfo in fileInfos)
                {
                    //DateTime.Compare( fileInfo.LastWriteTime,DateTime.Now);
                    try
                    {
                        File.Delete(fileInfo.FullName); //删除文件
                    }
                    catch (Exception ex) { }
                }
            }

            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
            if (dirInfos != null && dirInfos.Length > 0)
            {
                foreach (DirectoryInfo childDirInfo in dirInfos)
                {
                    try
                    {
                        this.DeleteInDir(childDirInfo.FullName.ToString()); //递归
                    }
                    catch (Exception ex) { }
                }
            }
            try{
                Directory.Delete(dirInfo.FullName, true); //删除目录
            }
            catch (Exception ex) { }

        }


        //删除文件夹内所有内容
        public void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件 
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir); //删除已空文件夹 
                //Response.Write(dir + " 文件夹删除成功");
            }
        }


        /// 删除文件
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <param name="delname">待删除文件或文件夹名称</param>
        public void DeleteFolder(string dir, string delname)
        {
            if (Directory.Exists(dir))
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    string tmpd = d.Substring(0, d.LastIndexOf("\\")) + "\\" + delname;
                    if (Directory.Exists(d))
                    {
                        if (d == tmpd)
                            Directory.Delete(d, true);
                        else
                            DeleteFolder(d, delname);//递归删除子文件夹   
                    }
                    else if (File.Exists(d))
                    {
                        if (d == tmpd)
                            File.Delete(d);
                    }
                }
            }
        }

        //private string getFilenameIfExist(string szSrcFilename, bool bIfCreateNewWhenExist)
        //{
        //    string szResult = string.Empty;
        //    if (bIfCreateNewWhenExist) //如果存在是否创建新的文件名, true代表是
        //    {
        //        int nCount = 0;
        //        while (true)
        //        {
        //            bool bFlag=
        //        }
        //    }
        //    else
        //        szResult = szSrcFilename;  //直接返回原来的文件名

        //    return szResult;
        //}
      
        
        ///<summary>Simple helper to create a featureclass in a geodatabase.</summary>
        /// 
        ///<param name="workspace">An IWorkspace2 interface</param>
        ///<param name="featureDataset">An IFeatureDataset interface or Nothing</param>
        ///<param name="featureClassName">A System.String that contains the name of the feature class to open or create. Example: "states"</param>
        ///<param name="fields">An IFields interface</param>
        ///<param name="CLSID">A UID value or Nothing. Example "esriGeoDatabase.Feature" or Nothing</param>
        ///<param name="CLSEXT">A UID value or Nothing (this is the class extension if you want to reference a class extension when creating the feature class).</param>
        ///<param name="strConfigKeyword">An empty System.String or RDBMS table string for ArcSDE. Example: "myTable" or ""</param>
        ///  
        ///<returns>An IFeatureClass interface or a Nothing</returns>
        ///  
        ///<remarks>
        ///  (1) If a 'featureClassName' already exists in the workspace a reference to that feature class 
        ///      object will be returned.
        ///  (2) If an IFeatureDataset is passed in for the 'featureDataset' argument the feature class
        ///      will be created in the dataset. If a Nothing is passed in for the 'featureDataset'
        ///      argument the feature class will be created in the workspace.
        ///  (3) When creating a feature class in a dataset the spatial reference is inherited 
        ///      from the dataset object.
        ///  (4) If an IFields interface is supplied for the 'fields' collection it will be used to create the
        ///      table. If a Nothing value is supplied for the 'fields' collection, a table will be created using 
        ///      default values in the method.
        ///  (5) The 'strConfigurationKeyword' parameter allows the application to control the physical layout 
        ///      for this table in the underlying RDBMS—for example, in the case of an Oracle database, the 
        ///      configuration keyword controls the tablespace in which the table is created, the initial and 
        ///     next extents, and other properties. The 'strConfigurationKeywords' for an ArcSDE instance are 
        ///      set up by the ArcSDE data administrator, the list of available keywords supported by a workspace 
        ///      may be obtained using the IWorkspaceConfiguration interface. For more information on configuration 
        ///      keywords, refer to the ArcSDE documentation. When not using an ArcSDE table use an empty 
        ///      string (ex: "").
        ///</remarks>

        public IFeatureClass CreateFeatureClass(IWorkspace2 workspace, IFeatureDataset featureDataset, String featureClassName,IFields fields,UID CLSID,UID CLSEXT,String strConfigKeyword,bool bNewIfExist,esriFeatureType featureType)
        {
            if (featureClassName == "") return null; // name was not passed in 

            ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass;
            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)workspace; // Explicit Cast


            if (bNewIfExist)//如果存在重名需要创建新的
            {
                int nTmp = 0;
                string strNameTemp = featureClassName;
                while (true)
                {
                    bool bFlag = workspace.get_NameExists(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureClass, featureClassName);
                    if (bFlag) //feature class with that name already exists 
                    {
                        //strRasterName += "1";
                        featureClassName = strNameTemp + ((nTmp++).ToString());
                    }
                    else
                        break;
                }               
            }
            else
            {
                featureClass = featureWorkspace.OpenFeatureClass(featureClassName);
                return featureClass;
            }

            // assign the class id value if not assigned
            if (CLSID == null)
            {
                CLSID = new ESRI.ArcGIS.esriSystem.UIDClass();
                CLSID.Value = "esriGeoDatabase.Feature";
            }

            ESRI.ArcGIS.Geodatabase.IObjectClassDescription objectClassDescription = new ESRI.ArcGIS.Geodatabase.FeatureClassDescriptionClass();

            // if a fields collection is not passed in then supply our own
            if (fields == null)
            {
                // create the fields using the required fields method
                fields = objectClassDescription.RequiredFields;
                ESRI.ArcGIS.Geodatabase.IFieldsEdit fieldsEdit = (ESRI.ArcGIS.Geodatabase.IFieldsEdit)fields; // Explicit Cast
                ESRI.ArcGIS.Geodatabase.IField field = new ESRI.ArcGIS.Geodatabase.FieldClass();

                // create a user defined text field
                ESRI.ArcGIS.Geodatabase.IFieldEdit fieldEdit = (ESRI.ArcGIS.Geodatabase.IFieldEdit)field; // Explicit Cast

                // setup field properties
                fieldEdit.Name_2 = "Type";
                fieldEdit.Type_2 = ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeString;
                fieldEdit.IsNullable_2 = true;
                fieldEdit.AliasName_2 = "Sample Field Column";
                fieldEdit.DefaultValue_2 = "NonCrater";
                fieldEdit.Editable_2 = true;
                fieldEdit.Length_2 = 10;

                // add field to field collection
                fieldsEdit.AddField(field);
                fields = (ESRI.ArcGIS.Geodatabase.IFields)fieldsEdit; // Explicit Cast
            }

            System.String strShapeField = "";

            // locate the shape field
            for (int j = 0; j < fields.FieldCount; j++)
            {
                if (fields.get_Field(j).Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeGeometry)
                {
                    strShapeField = fields.get_Field(j).Name;
                }
            }

            // Use IFieldChecker to create a validated fields collection.
            ESRI.ArcGIS.Geodatabase.IFieldChecker fieldChecker = new ESRI.ArcGIS.Geodatabase.FieldCheckerClass();
            ESRI.ArcGIS.Geodatabase.IEnumFieldError enumFieldError = null;
            ESRI.ArcGIS.Geodatabase.IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (ESRI.ArcGIS.Geodatabase.IWorkspace)workspace;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

            // The enumFieldError enumerator can be inspected at this point to determine 
            // which fields were modified during validation.


            // finally create and return the feature class
            if (featureDataset == null)// if no feature dataset passed in, create at the workspace level
            {
                featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, CLSID, CLSEXT, featureType, strShapeField, strConfigKeyword);
            }
            else
            {
                featureClass = featureDataset.CreateFeatureClass(featureClassName, validatedFields, CLSID, CLSEXT, featureType, strShapeField, strConfigKeyword);
            }
            return featureClass;
        }

        //创建注记要素类，不同于一般的要素类
        public IFeatureClass CreateAnnotationClass(IFeatureClass fcSrc, string strLayerName,IWorkspace2 wksDest,ISpatialReference spatialRef)
        {
            if (fcSrc == null) return null;
            if (fcSrc.FeatureType != esriFeatureType.esriFTAnnotation) return null;
            IFeatureWorkspace featureWorkspace = wksDest as IFeatureWorkspace;
            if(featureWorkspace == null) return null;
            //Get source featureclass attribute
            IFeatureClassExtension fcExtensionSrc = fcSrc.Extension as IFeatureClassExtension;
            if (fcExtensionSrc == null) return null;
            IAnnoClass annoClassSrc = fcExtensionSrc as IAnnoClass;
            if (annoClassSrc == null) return null;
            IAnnotateLayerPropertiesCollection2 annoPropsColl = annoClassSrc.AnnoProperties as IAnnotateLayerPropertiesCollection2;

            //Add the annotation class to a collection
            IAnnotateLayerPropertiesCollection annotateLayerPropsClollectionDst = new AnnotateLayerPropertiesCollectionClass();            
            ILabelEngineLayerProperties labelEngineLayerPropertiesDst = new LabelEngineLayerPropertiesClass();

           //Set annotationSymbol
            ISymbolCollection symbolCollectionDst = annoClassSrc.SymbolCollection;
            ISymbolCollection2 symbolCollection2Dst =symbolCollectionDst as ISymbolCollection2;
            ISymbolIdentifier2 symbolIdentifier2 = null;
            symbolCollection2Dst.GetSymbolIdentifier(0,out symbolIdentifier2);
            labelEngineLayerPropertiesDst.SymbolID = symbolIdentifier2.ID;

            IAnnotateLayerProperties annotateLayerPropertiesDst = labelEngineLayerPropertiesDst as IAnnotateLayerProperties;
            for (int i = 0; i < annoPropsColl.Count;i++ )
            {
                IAnnotateLayerProperties annoProps = annoPropsColl.get_Properties(i);
                annotateLayerPropertiesDst.Class = annoProps.Class;
                annotateLayerPropsClollectionDst.Add(annotateLayerPropertiesDst);
            }
            //Create a graphics layer scale object
            IGraphicsLayerScale graphicsLayerScale = new GraphicsLayerScaleClass();
            graphicsLayerScale.ReferenceScale = annoClassSrc.ReferenceScale;
            graphicsLayerScale.Units = annoClassSrc.ReferenceScaleUnits;

            IAnnotationClassExtension annoClassExtension = annoClassSrc as IAnnotationClassExtension;
            //Create the overposter properties for the standard label engine
            IOverposterProperties overposterProperties = annoClassExtension.OverposterProperties;//new BasicOverposterPropertiesClass();
             //Instantiate a class description object
            IObjectClassDescription ocDescription = new AnnotationFeatureClassDescriptionClass();
            IFeatureClassDescription fcDescription = ocDescription as IFeatureClassDescription;

            //Get the shape field 
            IFields fields = ocDescription.RequiredFields;
            int shapeFieldIndex = fields.FindField(fcDescription.ShapeFieldName);
            IField shapeField = fields.get_Field(shapeFieldIndex);
            IGeometryDef geometryDef = shapeField.GeometryDef;
            IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
            geometryDefEdit.SpatialReference_2 = spatialRef;

            //判断数据库是否已存在同名注记层
            int nTmp = 1;
            string strNameTemp = strLayerName;
            while (true)
            {
                bool bFlag = wksDest.get_NameExists(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureClass, strLayerName);
                if (bFlag)
                {
                    strLayerName = strNameTemp + ((nTmp++).ToString());
                }
                else
                    break;
            }
            string configKeyword = "Default";

            IAnnotationLayerFactory annotationLayerFactory = new FDOGraphicsLayerFactoryClass();
            IAnnotationLayer annotationLayer = annotationLayerFactory.CreateAnnotationLayer(featureWorkspace, null, strLayerName, geometryDef, null,
                annotateLayerPropsClollectionDst,graphicsLayerScale,symbolCollectionDst,false,false,false,true,overposterProperties,configKeyword);

            IFeatureLayer featureLayer = annotationLayer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;

            return featureClass;
        }
        public IFeatureClass CreateAnnotationClass(IFeatureWorkspace featureWordspace, IFeatureDataset featureDataset, 
            string layerName, ISpatialReference spatialReference, int referenceScale, esriUnits referenceScaleUnits, string configKeyword)
        {
            ILabelEngineLayerProperties labelEngineLayerProperties = new LabelEngineLayerPropertiesClass();
            IAnnotateLayerProperties annotateLayerProperties =labelEngineLayerProperties as IAnnotateLayerProperties;
            annotateLayerProperties.Class = "Annotation Class 1";

            ITextSymbol annotationTextSymbol = labelEngineLayerProperties.Symbol;
            ISymbol annotationSymbol = annotationTextSymbol as ISymbol;

            ISymbolCollection symbolCollection = new SymbolCollectionClass();
            ISymbolCollection2 symbolCollection2 = symbolCollection as ISymbolCollection2;
            ISymbolIdentifier2 symbolIdentifier2 = null;
            symbolCollection2.AddSymbol(annotationSymbol, "Annotation Class 1", out symbolIdentifier2);
            labelEngineLayerProperties.SymbolID = symbolIdentifier2.ID;

            //Add the annotation class to a collection
            IAnnotateLayerPropertiesCollection annotateLayerPropsClollection = new AnnotateLayerPropertiesCollectionClass();
            annotateLayerPropsClollection.Add(annotateLayerProperties);

            //Create a graphics layer scale object
            IGraphicsLayerScale graphicsLayerScale = new GraphicsLayerScaleClass();
            graphicsLayerScale.ReferenceScale = referenceScale;
            graphicsLayerScale.Units = referenceScaleUnits;

            //Create the overposter properties for the standard label engine
            IOverposterProperties overposterProperties = new BasicOverposterPropertiesClass();

            //Instantiate a class description object
            IObjectClassDescription ocDescription = new AnnotationFeatureClassDescriptionClass();
            IFeatureClassDescription fcDescription = ocDescription as IFeatureClassDescription;

            //Get the shape field 
            IFields fields = ocDescription.RequiredFields;
            int shapeFieldIndex = fields.FindField(fcDescription.ShapeFieldName);
            IField shapeField = fields.get_Field(shapeFieldIndex);
            IGeometryDef geometryDef = shapeField.GeometryDef;
            IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
            geometryDefEdit.SpatialReference_2 = spatialReference;

            IAnnotationLayerFactory annotationLayerFactory = new FDOGraphicsLayerFactoryClass();

            IAnnotationLayer annotationLayer = annotationLayerFactory.CreateAnnotationLayer(featureWordspace, featureDataset, layerName, geometryDef, null,
                annotateLayerPropsClollection,graphicsLayerScale,symbolCollection,false,false,false,true,overposterProperties,configKeyword);

            IFeatureLayer featureLayer = annotationLayer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;

            return featureClass;
        }
        //多边形裁切栅格影像
        public bool RasterSubsetByPolygon(IRasterDataset pRasterDataset, IGeometry clipGeo, string FileName)
        {
            try
            {
                IRasterBandCollection rasterBands = pRasterDataset as IRasterBandCollection;
                int nBandCount = rasterBands.Count;
                //设置输出栅格参数
                IRasterLayer rasterLayer = new RasterLayerClass();
                rasterLayer.CreateFromDataset(pRasterDataset);
                IRaster pRaster = rasterLayer.Raster;//此处只得到前3个波段              
                IRasterBandCollection bandsNew = pRaster as IRasterBandCollection;
                IRasterBand pBand = null;

                for (int i = 3; i < nBandCount; i++)
                {
                    pBand = rasterBands.Item(i);
                    bandsNew.AppendBand(pBand);
                }

                IRasterProps pProps = pRaster as IRasterProps;
                object cellSizeProvider = pProps.MeanCellSize().X;
                IGeoDataset pInputDataset = pRaster as IGeoDataset;

                //IGeoDataset pInputDataset = pRasterDataset as IGeoDataset;//此种方式也只是得到前3个波段

                //设置格格处理环境
                IExtractionOp2 pExtractionOp = new RasterExtractionOpClass();
                IRasterAnalysisEnvironment pRasterAnaEnvir = pExtractionOp as IRasterAnalysisEnvironment;
                pRasterAnaEnvir.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cellSizeProvider);

                object extentProvider = clipGeo.Envelope;
                object snapRasterData = Type.Missing;
                pRasterAnaEnvir.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProvider, ref snapRasterData);

                IGeoDataset pOutputDataset = null;
                if (clipGeo.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    pOutputDataset = pExtractionOp.Polygon(pInputDataset, clipGeo as IPolygon, true);//裁切操作
                }
                else if (clipGeo.GeometryType == esriGeometryType.esriGeometryEnvelope)
                {
                    pOutputDataset = pExtractionOp.Rectangle(pInputDataset, clipGeo as IEnvelope, true);//裁切操作
                }
                else
                {
                    return false;
                }


                IRaster clipRaster; //裁切后得到的IRaster
                if (pOutputDataset is IRasterLayer)
                {
                    IRasterLayer rasterLayer2 = pOutputDataset as IRasterLayer;
                    clipRaster = rasterLayer.Raster;
                }
                else if (pOutputDataset is IRasterDataset)
                {
                    IRasterDataset rasterDataset = pOutputDataset as IRasterDataset;
                    clipRaster = rasterDataset.CreateDefaultRaster();
                }
                else if (pOutputDataset is IRaster)
                {
                    clipRaster = pOutputDataset as IRaster;
                }
                else
                {
                    return false;
                }
                //保存裁切后得到的clipRaster
                //如果直接保存为img影像文件
                //判断保存类型
                string strFileType = System.IO.Path.GetExtension(FileName);
                switch (strFileType.ToUpper())
                {
                    case "TIF":
                        strFileType = "TIFF";
                        break;
                    case "IMG":
                        strFileType = "IMAGINE Image";
                        break;
                    default:
                        strFileType = "TIFF";
                        break;
                }
                IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(FileName), 0);
                ISaveAs pSaveAs = clipRaster as ISaveAs;
                IDataset pDataset = pSaveAs.SaveAs(FileName, pWorkspace, strFileType);//以TIF格式保存
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataset);
                return true;
                //MessageBox.Show("成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public static string GetReferenceString(ISpatialReference pSpatialReference)
        {
            string strRef = string.Empty;
            if (pSpatialReference is IProjectedCoordinateSystem)
            {
                IProjectedCoordinateSystem pProjectedCoordinateSystem = new ProjectedCoordinateSystemClass();
                pProjectedCoordinateSystem = pSpatialReference as IProjectedCoordinateSystem;

                strRef += "Name:" + pProjectedCoordinateSystem.Name + Environment.NewLine; //名称
                strRef += "Projection:" + pProjectedCoordinateSystem.Projection.Name + Environment.NewLine;
                strRef += "false_easting:" + pProjectedCoordinateSystem.FalseEasting.ToString() + Environment.NewLine;
                strRef += "false_northing:" + pProjectedCoordinateSystem.FalseNorthing.ToString() + Environment.NewLine;
                // strRef +="central_meridian:" + pProjectedCoordinateSystem.get_CentralMeridian(true).ToString() + Environment.NewLine; //中央子午线
                // strRef +="scale_factor:" + pProjectedCoordinateSystem.ScaleFactor.ToString() + Environment.NewLine;
                //strRef +="Linear Unit:" + pProjectedCoordinateSystem.CoordinateUnit.Name + "(" + pProjectedCoordinateSystem.CoordinateUnit.MetersPerUnit.ToString() + ")");
                strRef += "" + Environment.NewLine;
                strRef += "Geographic Coordinate System:" + pProjectedCoordinateSystem.GeographicCoordinateSystem.Name + Environment.NewLine;
                strRef += "Angular Unit:" + pProjectedCoordinateSystem.GeographicCoordinateSystem.CoordinateUnit.Name + "(" + pProjectedCoordinateSystem.GeographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString() + ")" + Environment.NewLine;
                strRef += "Prime Meridian:" + pProjectedCoordinateSystem.GeographicCoordinateSystem.PrimeMeridian.Name + "(" + pProjectedCoordinateSystem.GeographicCoordinateSystem.PrimeMeridian.Longitude.ToString() + ")" + Environment.NewLine;
                strRef += "Datum:" + pProjectedCoordinateSystem.GeographicCoordinateSystem.Datum.Name + Environment.NewLine;
                strRef += "Spheroid:" + pProjectedCoordinateSystem.GeographicCoordinateSystem.Datum.Spheroid.Name + Environment.NewLine;
                strRef += "Semimajor Axis:" + pProjectedCoordinateSystem.GeographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString() + Environment.NewLine;
                strRef += "Semiminor Axis:" + pProjectedCoordinateSystem.GeographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString() + Environment.NewLine;
                strRef += "Flattening:" + pProjectedCoordinateSystem.GeographicCoordinateSystem.Datum.Spheroid.Flattening.ToString() + Environment.NewLine;
            }
            else if (pSpatialReference is IGeographicCoordinateSystem)
            {
                IGeographicCoordinateSystem pGeographicCoordinateSystem = new GeographicCoordinateSystemClass();
                pGeographicCoordinateSystem = pSpatialReference as IGeographicCoordinateSystem;
                strRef += "Geographic Coordinate System:" + pGeographicCoordinateSystem.Name + Environment.NewLine;
                strRef += "Angular Unit:" + pGeographicCoordinateSystem.CoordinateUnit.Name + "(" + pGeographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString() + ")" + Environment.NewLine;
                strRef += "Prime Meridian:" + pGeographicCoordinateSystem.PrimeMeridian.Name + "(" + pGeographicCoordinateSystem.PrimeMeridian.Longitude.ToString() + ")" + Environment.NewLine;
                strRef += "Datum:" + pGeographicCoordinateSystem.Datum.Name + Environment.NewLine;
                strRef += "Spheroid:" + pGeographicCoordinateSystem.Datum.Spheroid.Name + Environment.NewLine;
                strRef += "Semimajor Axis:" + pGeographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString() + Environment.NewLine;
                strRef += "Semiminor Axis:" + pGeographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString() + Environment.NewLine;
                strRef += "Flattening:" + pGeographicCoordinateSystem.Datum.Spheroid.Flattening.ToString() + Environment.NewLine;
            }
            else if (pSpatialReference is IUnknownCoordinateSystem)
            {
                strRef = "Unknown";
            }

            return strRef;
        }

    }//class



    public class ReadData
    {
        public static List<ILayer> ReadShapLayer(List<string> filePathList)
        {
            List<ILayer> layerList = new List<ILayer>();

            if (filePathList.Count == 0) return null;
            else
            {
                foreach (string path in filePathList)
                {
                    IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
                    IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(path), 0);
                    IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;

                    IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(path));
                    IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                    pFeatureLayer.FeatureClass = pFeatureClass;
                    pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(path);
                    layerList.Add(pFeatureLayer as ILayer);
                }
                return layerList;
            }
        }

        public static List<ILayer> ReadLayerFromAccess(List<string> filePathList)
        {
            List<ILayer> layerList = new List<ILayer>();

            if (filePathList.Count == 0) return null;
            else
            {
                foreach (string path in filePathList)
                {
                    IWorkspaceFactory pWorkspaceFactory = new AccessWorkspaceFactoryClass();
                    IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(path, 0);
                    IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;

                    IEnumDataset pEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass) as IEnumDataset;
                    pEnumDataset.Reset();
                    IDataset pDataset = pEnumDataset.Next();

                    while (pDataset is IFeatureClass)
                    {
                        IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                        pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset.Name);
                        pFeatureLayer.Name = pDataset.Name;
                        ILayer pLayer = pFeatureLayer as ILayer;
                        layerList.Add(pFeatureLayer as ILayer);
                        pDataset = pEnumDataset.Next();
                    }
                }
                return layerList;
            }
        }

        public static List<ILayer> ReadLayerFromGDB(List<string> filePathList)
        {
            List<ILayer> layerList = new List<ILayer>();

            if (filePathList.Count == 0) return null;
            else
            {
                foreach (string path in filePathList)
                {
                    IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(path, 0);
                    IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;

                    IEnumDataset pEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass) as IEnumDataset;
                    pEnumDataset.Reset();
                    IDataset pDataset = pEnumDataset.Next();

                    while (pDataset is IFeatureClass)
                    {
                        IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                        pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset.Name);
                        pFeatureLayer.Name = pDataset.Name;
                        ILayer pLayer = pFeatureLayer as ILayer;
                        layerList.Add(pFeatureLayer as ILayer);
                        pDataset = pEnumDataset.Next();
                    }
                }
                return layerList;
            }
        }

        public static List<ILayer> ReadRasterLayer(List<string> filePathList)
        {
            List<ILayer> layerList = new List<ILayer>();

            if (filePathList.Count == 0) return null;
            else
            {
                foreach (string path in filePathList)
                {
                    IRasterLayer pRasterLayer = new RasterLayerClass();
                    pRasterLayer.CreateFromFilePath(path);
                    layerList.Add(pRasterLayer as ILayer);
                }

                return layerList;
            }
        }

        public static List<ILayer> ReadCADLayer(List<string> filePathList)
        {
            List<ILayer> layerList = new List<ILayer>();

            if (filePathList.Count == 0) return null;
            else
            {
                foreach (string path in filePathList)
                {
                    IWorkspaceFactory pWorkspaceFactory = new CadWorkspaceFactoryClass();
                    IFeatureWorkspace pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(path), 0) as IFeatureWorkspace;
                    IFeatureDataset pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(System.IO.Path.GetFileName(path));
                    IFeatureClassContainer pFeatClassContainer = pFeatureDataset as IFeatureClassContainer;

                    for (int i = 0; i < pFeatClassContainer.ClassCount - 1; i++)
                    {
                        IFeatureLayer pFeatureLayer;
                        IFeatureClass pFeatClass = pFeatClassContainer.get_Class(i);
                        if (pFeatClass.FeatureType == esriFeatureType.esriFTCoverageAnnotation) pFeatureLayer = new CadAnnotationLayerClass();
                        else pFeatureLayer = new FeatureLayerClass();

                        pFeatureLayer.Name = pFeatClass.AliasName;
                        pFeatureLayer.FeatureClass = pFeatClass;
                        layerList.Add(pFeatureLayer as ILayer);
                    }
                }
                return layerList;
            }
        }

        

        public static List<ILayer> ReadXYZfile(List<string> filePathLis)
        {
            //稍后贴出
            return null;
        }

       
    }


}
