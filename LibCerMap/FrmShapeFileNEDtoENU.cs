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
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

using DevComponents.DotNetBar;
using ESRI.ArcGIS.esriSystem;

namespace LibCerMap
{
    public partial class FrmShapeFileNEDtoENU : OfficeForm
    {
        IMapControl3 pMapControl;
        IFeatureLayer pFeatureLayer;
        IFeatureClass pTargetFeatureClass;//先复制再修改，复制后的featureclass
        string LayerExpName;//转换后文件存储路径
        public FrmShapeFileNEDtoENU(IMapControl3 mapcontrol)
        {
            InitializeComponent();
            pMapControl = mapcontrol;
        }

        private void FrmNEDtoENU_Load(object sender, EventArgs e)
        {
            this.EnableGlass = false;
            //初始化图层列表
            IEnumLayer pEnumLayer = pMapControl.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = null;
            while ((pLayer = pEnumLayer.Next()) != null)
            {
                if (pLayer is IFeatureLayer)
                {
                    cmbLayerTrans.Items.Add(pLayer.Name);
                }
            }
            if (cmbLayerTrans.Items.Count>0)
            {
                cmbLayerTrans.SelectedIndex = 0;
            }
            
        }

        private void BtnWorkBrowse_Click(object sender, EventArgs e)
        {
           // SaveFileDialog dlgOutputFile = new SaveFileDialog();
           // dlgOutputFile.Title = "选择输出文件路径：";
           // dlgOutputFile.InitialDirectory = ".";
           //// dlgOutputFile.Filter = "shape文件(*.shp)|*.shp|所有文件(*.*)|*.*";
           // dlgOutputFile.Filter = "tin files|*.";
           // dlgOutputFile.RestoreDirectory = true;
           // dlgOutputFile.DefaultExt = "shp";
            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                if (fdlg.SelectedPath != "")
                {
                    txtLayerExp.Text = fdlg.SelectedPath;
                }
               // return;
            }
            
          
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //首先进行坐标系x,y变换
            //string fileExpTran;//进行x,y做表转换后输出的tiff文件存储路径，用这一文件在进行后期的Z转换
            //fileExpTran = System.IO.Path.GetDirectoryName(LayerExpName) +"\\"+ System.IO.Path.GetFileNameWithoutExtension(LayerExpName)+"XY.tif";
            //try
            //{
            //    if (NorthEastToEastNorth(pRasterLayer, LayerExpName))
            //    {
            //        RasterLayerClass rasterlayer = new RasterLayerClass();
            //        rasterlayer.CreateFromFilePath(LayerExpName);
            //        IRaster2 pRaster2 = rasterlayer.Raster as IRaster2;
            //        IRasterDataset2 pRasterDataset = pRaster2.RasterDataset as IRasterDataset2;
            //        ChangeRasterValue(pRasterDataset, -1, 0);
            //        pMapControl.AddLayer(rasterlayer as ILayer);
            //        this.Close();
            //    }             
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            try
            {
                IFeatureClass pFC = pFeatureLayer.FeatureClass;
                IDataset pDS = pFC as IDataset;
                IWorkspace pWS = pDS.Workspace;
                string filedir = pWS.PathName;
                string fdestname = System.IO.Path.GetFileNameWithoutExtension( txtFeatureName.Text);
                ClsGDBDataCommon CGD = new ClsGDBDataCommon();
                IWorkspace pTargetWS = CGD.OpenFromShapefile(txtLayerExp.Text);
                IWorkspace2 workspace2 = pTargetWS as IWorkspace2;

                if (workspace2.get_NameExists(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureClass, fdestname))
                {
                    MessageBox.Show("目标文件已存在！");
                    return;
                }
                //shape文件直接拷贝后再修改
                if (pDS.CanCopy() == true)
                {
                    pDS.Copy(txtFeatureName.Text, pTargetWS);
                    IFeatureWorkspace pFW = pTargetWS as IFeatureWorkspace;
                    pTargetFeatureClass = pFW.OpenFeatureClass(txtFeatureName.Text);
                    TransCoordiante(pFW as IWorkspace, pTargetFeatureClass);
                }
                //表示为gdb的feature，只能往gdb拷贝
                else 
                {
                    // Create workspace name objects.
                    IWorkspaceName sourceWorkspaceName = new WorkspaceNameClass();
                    IWorkspaceName targetWorkspaceName = new WorkspaceNameClass();
                    IName targetName = (IName)targetWorkspaceName;

                    // Set the workspace name properties.
                    sourceWorkspaceName.PathName =pWS.PathName;
                    sourceWorkspaceName.WorkspaceFactoryProgID = 
                        "esriDataSourcesGDB.FileGDBWorkspaceFactory";
                   // targetWorkspaceName.PathName = @"PartialMontgomery.gdb";
                    targetWorkspaceName.PathName = txtLayerExp.Text;
                    targetWorkspaceName.WorkspaceFactoryProgID =
                      "esriDataSourcesGDB.FileGDBWorkspaceFactory";

                    //if (txtLayerExp.Text.Length>4 && txtLayerExp.Text.Substring(txtLayerExp.Text.Length-4,4) == ".gdb")
                    //{
                    //    targetWorkspaceName.WorkspaceFactoryProgID =
                    //      "esriDataSourcesGDB.FileGDBWorkspaceFactory";
                    //}
                    //else
                    //{
                    //    targetWorkspaceName.WorkspaceFactoryProgID =
                    //     "esriDataSourcesFile.ShapefileWorkspaceFactory";
                    //}
                   
                    // Create a name object for the source feature class.
                    IFeatureClassName featureClassName = new FeatureClassNameClass();

                    // Set the featureClassName properties.
                    IDatasetName sourceDatasetName = (IDatasetName)featureClassName;
                    sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                    sourceDatasetName.Name = pDS.BrowseName;
                    IName sourceName = (IName)sourceDatasetName;


                    // Create an enumerator for source datasets.
                    IEnumName sourceEnumName = new NamesEnumeratorClass();
                    IEnumNameEdit sourceEnumNameEdit = (IEnumNameEdit)sourceEnumName;

                    // Add the name object for the source class to the enumerator.
                    sourceEnumNameEdit.Add(sourceName);

                    // Create a GeoDBDataTransfer object and a null name mapping enumerator.
                    IGeoDBDataTransfer geoDBDataTransfer = new GeoDBDataTransferClass();
                    IEnumNameMapping enumNameMapping = null;

                    // Use the data transfer object to create a name mapping enumerator.
                    Boolean conflictsFound = geoDBDataTransfer.GenerateNameMapping(sourceEnumName,
                        targetName, out enumNameMapping);
                    enumNameMapping.Reset();
                    //修改拷贝的文件名
                    INameMapping nameMapping = enumNameMapping.Next(); 
                    if ((nameMapping != null))
                    {
                        nameMapping.TargetName = txtFeatureName.Text;
                    }

                    // Check for conflicts.
                    //if (conflictsFound)
                    //{
                    //    // Iterate through each name mapping.
                    //    INameMapping nameMapping = null;
                    //    while ((nameMapping = enumNameMapping.Next()) != null)
                    //    {
                    //        // Resolve the mapping's conflict (if there is one).
                    //        if (nameMapping.NameConflicts)
                    //        {
                    //            nameMapping.TargetName = nameMapping.GetSuggestedName(targetName);
                    //        }

                    //        // See if the mapping's children have conflicts.
                    //        IEnumNameMapping childEnumNameMapping = nameMapping.Children;
                    //        if (childEnumNameMapping != null)
                    //        {
                    //            childEnumNameMapping.Reset();

                    //            // Iterate through each child mapping.
                    //            INameMapping childNameMapping = null;
                    //            while ((childNameMapping = childEnumNameMapping.Next()) != null)
                    //            {
                    //                if (childNameMapping.NameConflicts)
                    //                {
                    //                    childNameMapping.TargetName = childNameMapping.GetSuggestedName
                    //                        (targetName);
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    // Start the transfer.                   
                    geoDBDataTransfer.Transfer(enumNameMapping, targetName);
                    IFeatureWorkspace pFW =  CGD.OpenFromFileGDB(txtLayerExp.Text) as IFeatureWorkspace;
                    pTargetFeatureClass = pFW.OpenFeatureClass(txtFeatureName.Text);
                    TransCoordiante(pFW as IWorkspace, pTargetFeatureClass);
                    
                }

                if (pTargetFeatureClass !=null)
                {
                    //添加到图层中
                    IFeatureLayer featureLayer = new FeatureLayerClass();
                    featureLayer.FeatureClass = pTargetFeatureClass;
                    featureLayer.Name = fdestname;
                    pMapControl.AddLayer(featureLayer as ILayer);
                    pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TransCoordiante(IWorkspace pWorkSpace, IFeatureClass pFeatureclass)
        {
           // IWorkspaceEdit workspaceEdit = pWorkSpace as IWorkspaceEdit;
            //workspaceEdit.StartEditing(true);
           // workspaceEdit.StartEditOperation();

            IFeatureCursor pOutFCursor =  pFeatureclass.Update(null, true);
            IFeature pFeature =   pOutFCursor.NextFeature();
            while (pFeature!=null)
            {
               // IGeometry pGeometry = pFeature.Shape;
                IPointCollection pPointCollection = pFeature.Shape as IPointCollection;
                if (pPointCollection != null)
                {
                    for (int i = 0; i < pPointCollection.PointCount; i++ )
                    {
                        //多边形最后一个点和第一个点是同一个点，改两次又会改回去了
                        if (i == pPointCollection.PointCount-1)
                        {
                            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                            {
                                continue;
                            }
                            //IPoint firstpoint = pPointCollection.get_Point(0);
                            //IPoint lastpoint = pPointCollection.get_Point(pPointCollection.PointCount-1);                           
                        }
                        double tempvalue;
                        IPoint pt = pPointCollection.get_Point(i);
                        tempvalue = pt.Y;
                        pt.Y = pt.X;
                        pt.X = tempvalue;
                        pt.Z = 0 - pt.Z;
                        pPointCollection.UpdatePoint(i,pt);

                    }
                    //pFeature.Shape = pPointCollection as IGeometry;
                    pFeature.Store();
                }
                //不能转为pointcollection 说明是单点
                if (pPointCollection == null)
                {
                    IPoint pt = pFeature.Shape as IPoint;
                    double tempvalue;                   
                    tempvalue = pt.Y;
                    pt.Y = pt.X;
                    pt.X = tempvalue;
                    pt.Z = 0 - pt.Z;
                    //pFeature.Shape = pt as IGeometry;
                    pFeature.Store();
                }
                pFeature = pOutFCursor.NextFeature();
            }
          //  workspaceEdit.StopEditing(true);
          //  workspaceEdit.StopEditOperation();
           
        }

        static public bool NorthEastToEastNorth(IRasterLayer pRasterLayer, string szFilename)//坐标系x,y变换
        {
            if (pRasterLayer == null || szFilename == null)
                return false;

            IGeoReference pGeoReference = pRasterLayer as IGeoReference;
            IPoint pt = new PointClass();
            pt.X = 0;
            pt.Y = 0;
            pGeoReference.Rotate(pt, -90); //顺时针旋转90

            //水平旋转
            IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
            IRasterProps pProps = pRaster2 as IRasterProps;
            int nWidth = pProps.Width;
            int nHeight = pProps.Height;
            double dbCenterY = (pProps.Extent.UpperLeft.Y + pProps.Extent.LowerRight.Y) / 2;
            double dbDeltaX = 0;
            double dbDeltaY = -dbCenterY * 2;
            pGeoReference.Flip();
            pGeoReference.Shift(dbDeltaX, dbDeltaY);

            pGeoReference.Rectify(szFilename, "TIFF");
            IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);

            return true;
        }

        private void cmbLayerTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < pMapControl.LayerCount;i++ )
            //{
            //   if (pMapControl.get_Layer(i).Name==cmbLayerTrans.Text)
            //   {
                   pFeatureLayer = ClsGDBDataCommon.GetLayerFromName(pMapControl.Map, cmbLayerTrans.Text) as IFeatureLayer;
                   IFeatureClass pFC = pFeatureLayer.FeatureClass;
                   IDataset pDS = pFC as IDataset;
                   IWorkspace pWS = pDS.Workspace;
                   string filedir = pWS.PathName;
                   string fdestname="";
                   if (radioButtonENUtoNED.Checked == true)
                   {
                       fdestname = pDS.BrowseName + "_NED";
                   }
                   if (radioButtonNEDtoENU.Checked == true)
                   {
                       fdestname = pDS.BrowseName + "_ENU";
                   }
                   txtLayerExp.Text = filedir;
                   txtFeatureName.Text = fdestname;
                 

                    //表示为gdb的feature，只能往gdb拷贝
                    if (pDS.CanCopy() == false)
                    {
                        txtLayerExp.Enabled = false;
                        BtnWorkBrowse.Enabled = false;
                    }
                    else
                    {
                        txtLayerExp.Enabled = true;
                        BtnWorkBrowse.Enabled = true;
                    }
 
                   //string rasterfilepath = pFeatureLayer.ToString();//选中图层的文件路径
                   //string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
                   //string rasterfilename=System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称
                   //LayerExpName = rasterfiledir + "\\" + rasterfilename + "Trans.tif";
                   //txtLayerExp.Text = LayerExpName;
                   //break;
            //   }
            //}
        }
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonNEDtoENU_CheckedChanged(object sender, EventArgs e)
        {
            cmbLayerTrans_SelectedIndexChanged(null, null);
        }

        ////只做平面转换，不做Z值变换，临时写的
        //private void buttonX1_Click(object sender, EventArgs e)
        //{
        //    //首先进行坐标系x,y变换
        //    //string fileExpTran;//进行x,y做表转换后输出的tiff文件存储路径，用这一文件在进行后期的Z转换
        //    //fileExpTran = System.IO.Path.GetDirectoryName(LayerExpName) +"\\"+ System.IO.Path.GetFileNameWithoutExtension(LayerExpName)+"XY.tif";
        //    try
        //    {
        //        if (NorthEastToEastNorth(pRasterLayer, LayerExpName))
        //        {
        //            MessageBox.Show("转换完成！");
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
