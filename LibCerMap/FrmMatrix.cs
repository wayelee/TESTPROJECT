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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesFile;

using DevComponents.DotNetBar;
using System.IO;

namespace LibCerMap
{
    public partial class FrmMatrix : DevComponents.DotNetBar.OfficeForm
    {

#region 测试用旋转和平移矩阵

        //double[] rotateMat = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //double[] rotateMat = { 1,0,0,0,1,0,0,0,1};
        //double[] tranVec = { 1, 1, 0 };

#endregion

        IMapControl3 pMapControl = null;
        double[] rotateMat = { 1, 0, 0, 0, 1, 0, 0, 0, 1 };
        double[] tranVec = new double[3];
        string nvalue = "";
        bool isshp = true;
        bool isgo = false;
        public FrmMatrix(IMapControl3 mapControl)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapControl = mapControl;
        }

        private void FrmMatrix_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < pMapControl.Map.LayerCount;i++ )
            {
                ILayer pLayer = pMapControl.Map.Layer[i];
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pFLayer=pLayer as IFeatureLayer;
                    IFeatureClass pFClass = pFLayer.FeatureClass;
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint || pFClass.ShapeType == esriGeometryType.esriGeometryLine 
                        || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline || pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        cmbLayer.Items.Add(pLayer.Name);
                    }
                }
                else if(pLayer is IRasterLayer)
                {
                    cmbLayer.Items.Add(pLayer.Name);
                }
            }
            if (cmbLayer.Items.Count > 0)
            {
                cmbLayer.SelectedIndex = 0;
            }

            dataGridMatrix.Rows.Add();
            dataGridMatrix.Rows.Add();
            dataGridMatrix.Rows.Add();
            dataGridMatrix.Rows[0].HeaderCell.Value = "x";
            dataGridMatrix.Rows[1].HeaderCell.Value = "y";
            dataGridMatrix.Rows[2].HeaderCell.Value = "z";
            dataGridMatrix.Rows[0].Cells[0].Value = "1.0";
            dataGridMatrix.Rows[1].Cells[1].Value = "1.0";
            dataGridMatrix.Rows[2].Cells[2].Value = "1.0";
            dataGridMatrix.Rows[0].Cells[2].Value = "0.0";
            dataGridMatrix.Rows[0].Cells[2].ReadOnly = true;
            dataGridMatrix.Rows[1].Cells[2].Value = "0.0";
            dataGridMatrix.Rows[1].Cells[2].ReadOnly = true;
            dataGridMatrix.Rows[2].Cells[0].Value = "0.0";
            dataGridMatrix.Rows[2].Cells[0].ReadOnly = true;
            dataGridMatrix.Rows[2].Cells[1].Value = "0.0";
            dataGridMatrix.Rows[2].Cells[1].ReadOnly = true;
            //dataGridMatrix.Rows[1].Cells[0].ReadOnly = true;
            dataGridMatrix.Rows[1].Cells[0].Value = "1.0";
            dataGridMatrix.Rows[1].Cells[1].ReadOnly = true;
            dataGridMatrix.CurrentCell = null;
            isgo = true;
        }

        //根据名字查找图层
        private List<ILayer> GetLayersByName(string LayerName)
        {
            List<ILayer> LayerList = new List<ILayer>();
            for (int i = 0; i < pMapControl.Map.LayerCount; i++)
            {
                ILayer player = pMapControl.Map.get_Layer(i);
                if (player.Name == LayerName)
                {
                    LayerList.Add(player);
                }
            }
            return LayerList;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(isshp)
                saveFileDialog1.Filter = "*.shp|*.shp";
            else
                saveFileDialog1.Filter = "*.tif|*.tif";

            string szLayerName = cmbLayer.SelectedItem.ToString();
            string szExportName = System.IO.Path.GetFileNameWithoutExtension(szLayerName) + "_Export" + System.IO.Path.GetExtension(szLayerName);
            saveFileDialog1.FileName =  szExportName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textOutData.Text = saveFileDialog1.FileName;
            }

            //if (isshp)
            //{
            //    saveFileDialog1.Filter = "*.shp|*.shp";
            //    saveFileDialog1.FileName = cmbLayer.SelectedItem.ToString() + "Export.shp";
            //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        textOutData.Text = saveFileDialog1.FileName;
            //    }
            //}
            //else
            //{
            //    saveFileDialog1.Filter = "*.tif|*.tif";
            //    saveFileDialog1.FileName = cmbLayer.SelectedItem.ToString() + "Export.tif";
            //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        textOutData.Text = saveFileDialog1.FileName;
            //    }
            //}
           

        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                if (isshp)
                {
                    exprotMatrix();
                }
                else
                {
                    ExportTifMat();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }           
        }
        /// <summary>
        /// 栅格转换函数
        /// </summary>
        private void ExportTifMat()
        {
            if (!textOutData.Text.EndsWith("tif"))
            {
                //timerShow.Start();
                MessageBox.Show("输出文件名不是tif文件!");
                return;
            }
            ILayer player = null;
            for (int i = 0; i < pMapControl.Map.LayerCount; i++)
            {
                if (pMapControl.Map.Layer[i].Name == cmbLayer.Text)
                {
                    player = pMapControl.Map.Layer[i];
                    break;
                }
            }

            IRasterLayer pRLayer = player as IRasterLayer;

            //SaveFileDialog sfDialog = new SaveFileDialog();
            //sfDialog.Filter = "TIF文件(*.tif;*.TIF)|*.tif;*.TIF|所有文件(*.*)|*.*";
            //if (DialogResult.OK == sfDialog.ShowDialog())
            //{
            //    String szFilename = sfDialog.FileName;
            if (rasterTransfer(pRLayer, textOutData.Text, rotateMat, tranVec))
                MessageBox.Show("转换完成！");
            else
                MessageBox.Show("转换出错！");
            //}

            this.Close();
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
            //else
            //    ;

            return dbNoData;
        }

        //AE+C#修改栅格数据像素值
        //private void ChangeRasterValue(IRasterDataset2 pRasterDatset, double dbScale, double dbOffset)
        //{
        //    IRaster2 pRaster2 = pRasterDatset.CreateFullRaster() as IRaster2;

        //    IPnt pPntBlock = new PntClass();

        //    pPntBlock.X = 128;
        //    pPntBlock.Y = 128;

        //    IRasterCursor pRasterCursor = pRaster2.CreateCursorEx(pPntBlock);
        //    IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;

        //    if (pRasterEdit.CanEdit())
        //    {
        //        IRasterBandCollection pBands = pRasterDatset as IRasterBandCollection;
        //        IPixelBlock3 pPixelblock3 = null;
        //        int pBlockwidth = 0;
        //        int pBlockheight = 0;
        //        System.Array pixels;
        //        IPnt pPnt = null;
        //        object pValue;
        //        long pBandCount = pBands.Count;

        //        //获取Nodata
        //        IRasterProps pRasterPro = pRaster2 as IRasterProps;
        //        object pNodata = pRasterPro.NoDataValue;
        //        //double dbNoData = Convert.ToDouble(((double[])pNodata)[0]);
        //        double dbNoData = getNoDataValue(pNodata);
        //        if (double.IsNaN(dbNoData))
        //            return;

        //        do
        //        {
        //            pPixelblock3 = pRasterCursor.PixelBlock as IPixelBlock3;
        //            pBlockwidth = pPixelblock3.Width;
        //            pBlockheight = pPixelblock3.Height;

        //            for (int k = 0; k < pBandCount; k++)
        //            {
        //                pixels = (System.Array)pPixelblock3.get_PixelData(k);
        //                for (int i = 0; i < pBlockwidth; i++)
        //                {
        //                    for (int j = 0; j < pBlockheight; j++)
        //                    {
        //                        pValue = pixels.GetValue(i, j);
        //                        double ob = Convert.ToDouble(pValue);
        //                        if (ob != dbNoData)
        //                        {
        //                            ob *= dbScale;  //翻转
        //                            ob += dbOffset; //Z方向偏移                                
        //                        }

        //                        //pixels.SetValue(ob, i, j);
        //                        IRasterProps pRP = pRaster2 as IRasterProps;
        //                        if (pRP.PixelType == rstPixelType.PT_CHAR)
        //                            pixels.SetValue(Convert.ToChar(ob), i, j);
        //                        else if (pRP.PixelType == rstPixelType.PT_UCHAR)
        //                            pixels.SetValue(Convert.ToByte(ob), i, j);
        //                        else if (pRP.PixelType == rstPixelType.PT_FLOAT)
        //                            pixels.SetValue(Convert.ToSingle(ob), i, j);
        //                        else if (pRP.PixelType == rstPixelType.PT_DOUBLE)
        //                            pixels.SetValue(Convert.ToDouble(ob), i, j);
        //                        else if (pRP.PixelType == rstPixelType.PT_ULONG)
        //                            pixels.SetValue(Convert.ToInt32(ob), i, j);
        //                        else
        //                            ;
        //                    }
        //                }
        //                pPixelblock3.set_PixelData(k, pixels);

        //                System.Array textPixel = null;
        //                textPixel = (System.Array)pPixelblock3.get_PixelData(k);
        //            }

        //            pPnt = pRasterCursor.TopLeft;
        //            pRasterEdit.Write(pPnt, (IPixelBlock)pPixelblock3);
        //        }
        //        while (pRasterCursor.Next());

        //        pRasterEdit.Refresh();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);
        //    }
        //}

        private bool rasterTransfer(IRasterLayer pRasterLayer, string szFilename, double[] dbRotateMatrix, double[] dbOffsetMatrix)
        {
            if (pRasterLayer == null || dbRotateMatrix == null || dbOffsetMatrix == null || dbRotateMatrix.Length != 9 || dbOffsetMatrix.Length != 3)
            {
                return false;
            }

            //IRasterLayer pResultLayer = new RasterLayerClass();
            //pResultLayer.CreateFromRaster(pRasterLayer.Raster);
            //pResult = new RasterLayer();
            //pResult.co

            IGeoReference pGeoReference = pRasterLayer as IGeoReference;

            //XY平面平移和旋转
            double dbScale = Math.Sqrt(dbRotateMatrix[0] * dbRotateMatrix[0] + dbRotateMatrix[1] * dbRotateMatrix[1]);
            double dbTheta = Math.Acos(dbRotateMatrix[0] / dbScale);

            IPoint pt = new PointClass();
            pt.X = 0; pt.Y = 0;
            pGeoReference.Rotate(pt, -dbTheta / Math.PI * 180);                                   //旋转
            pGeoReference.Shift(-dbOffsetMatrix[0], -dbOffsetMatrix[1]);  //平移            
            //pGeoReference.Rotate(pt, dbTheta / Math.PI * 180);                                   //旋转
            pGeoReference.ReScale(1/dbScale, 1/dbScale);                      //拉伸

            try
            {
                if (!File.Exists(szFilename))
                {
                    pGeoReference.Rectify(szFilename, "TIFF");

                    IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
                    IRasterEdit pRasterEdit = pRaster2 as IRasterEdit;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterEdit);
                }
                //MessageBox.Show("转换完成！");
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("转换出错！");
                return false;
            }
            finally
            {
                pGeoReference.Reset();
            }


            //Open a raster file workspace.
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            IRasterWorkspace rasterWorkspace = (IRasterWorkspace)
                workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(szFilename), 0);

            //Open a file raster dataset. 
            IRasterDataset rasterDataset = rasterWorkspace.OpenRasterDataset(System.IO.Path.GetFileName(szFilename));
            IRasterDataset2 rasterDataset2 = rasterDataset as IRasterDataset2;

            ClsRasterOp pRasterOp = new ClsRasterOp();
            pRasterOp.ChangeRasterValue(rasterDataset2, dbRotateMatrix[8], dbOffsetMatrix[2]);
            
            ////Z方向变化

            //System.Array pixels, dstPixels;
            //IPixelBlock3 pixelBlock3 = null;
            //IRaster2 pRaster2 = rasterDataset.Raster as IRaster2;
            //IRaster pRaster = pDstLayer.Raster;
            //IRasterEdit pEdit = pRaster as IRasterEdit;
            //IRasterCursor rasterCursor = pRaster2.CreateCursorEx(null);//null时为128*128

            //do
            //{
            //    IPnt nSize = new PntClass();
            //    nSize.X = rasterCursor.PixelBlock.Width;
            //    nSize.Y = rasterCursor.PixelBlock.Height;
            //    pixelblock4 = rasterCursor.PixelBlock as IPixelBlock3;
            //    pixelBlock3 = pRaster.CreatePixelBlock(nSize) as IPixelBlock3;

            //    int nWidth = pixelBlock3.Width;
            //    int nHeight = pixelBlock3.Height;
            //    pixels = (System.Array)pixelBlock3.get_PixelData(0);
            //    dstPixels = (System.Array)pixelblock4.get_PixelData(0);
            //    for (int i = 0; i < nWidth; i++)
            //    {
            //        for (int j = 0; j < nHeight; j++)
            //        {
            //            object obj = pixels.GetValue(i, j);
            //            double ob = Convert.ToDouble(obj);

            //            ob *= dbRotateMatrix[8];  //翻转
            //            ob += dbOffsetMatrix[2]; //Z方向偏移

            //            dstPixels.SetValue(ob, i, j);
            //        }
            //    }

            //    //写回数据
            //    pEdit.Write(rasterCursor.TopLeft, pixelblock4 as IPixelBlock);
            //} while (rasterCursor.Next() == true);

            return true;
        }
        //}

        /// <summary>
        /// 矢量转换函数
        /// </summary>
        private void exprotMatrix()
        {
            ClsGDBDataCommon comm = new ClsGDBDataCommon();
            if (!textOutData.Text.EndsWith("shp"))
            {
                //timerShow.Start();
                MessageBox.Show("输出文件名不是shp文件!");
                return;
            }
            object featureOID;

            String strFullName = textOutData.Text;
            string strPath = System.IO.Path.GetDirectoryName(strFullName);
            string strName = System.IO.Path.GetFileName(strFullName);

            ILayer player=null;
            for (int i=0;i<pMapControl.Map.LayerCount;i++)
            {
                if (pMapControl.Map.Layer[i].Name==cmbLayer.Text)
                {
                    player = pMapControl.Map.Layer[i];
                    break;
                }
            }
            IFeatureLayer pFlayer = player as IFeatureLayer;
            IFeatureClass pFclass = pFlayer.FeatureClass;
            IFields pFields = pFclass.Fields;
            //设置空间参考
            ISpatialReference pSpatialRef;
            IGeoDataset pGeo = (IGeoDataset)pFlayer;
            pSpatialRef = pGeo.SpatialReference;

            IFeatureClass pFtClassNew = comm.CreateShapefile(strPath, strName, pFields, pSpatialRef);
            //for (int i = 0; i < pFtClassNew.Fields.FieldCount;i++ )
            //{
            //    if (pFtClassNew.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry &&
            //      pFtClassNew.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeOID)
            //    {
            //        pFtClassNew.Fields.get_Field(i).IsNullable = true;
            //    }
            //}

           
            IFeatureCursor pFC = pFlayer.FeatureClass.Search(null, false);
            IFeature pF = pFC.NextFeature();
            while (pF!=null)
            {
                if (pFclass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFclass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    double[]oldarray=new double[3];
                    double[] newarray = new double[3];
                    Matrix matrix = new Matrix();

                    IPoint pPointOld = pF.Shape as IPoint;
                    IPoint pPointNew = new PointClass();
                    oldarray[0] = pPointOld.X;
                    oldarray[1] = pPointOld.Y;
                    if (pPointOld.Z.ToString() == "NaN" || pPointOld.Z.ToString() == "非数字")
                    {
                        oldarray[2] = 0;//pPointOld.Z;
                    } 
                    else
                    {
                        oldarray[2] = pPointOld.Z;
                    }
                    matrix.coord_Trans(oldarray, rotateMat, tranVec, newarray);

                    pPointNew.X = newarray[0];
                    pPointNew.Y = newarray[1];
                    if (pPointOld.Z.ToString() == "NaN" || pPointOld.Z.ToString() == "非数字")
                    {
                        pPointNew.Z = 0;
                    }
                    else
                    {
                         pPointNew.Z = newarray[2];
                    }
                    

                    IFeature pFeatureTemp = pFtClassNew.CreateFeature();
                    pFeatureTemp.Shape = pPointNew as IGeometry;
                    ClsGDBDataCommon.CopyFeatureFieldValue(pF, pFeatureTemp);
                    pFeatureTemp.Store();                    
                }
                else if (pFclass.ShapeType == esriGeometryType.esriGeometryLine || pFclass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    double[] oldarrayS = new double[3];
                    double[] oldarrayT = new double[3];
                    double[] newarrayS = new double[3];
                    double[] newarrayT = new double[3];
                    Matrix matrix = new Matrix();

                    ISegmentCollection pSegCOld = pF.Shape as ISegmentCollection;
                    ISegmentCollection pSegCNew = new PolylineClass();
                   // ISegment pSegmentNEW = new LineClass();
                    //ILine pLine = new LineClass();
                    IPoint pPointF = new PointClass();
                    IPoint pPointT = new PointClass();

                    for (int i = 0; i < pSegCOld.SegmentCount;i++ )
                    {
                        ISegment pSegmentNEW = new LineClass();
                        ISegment pseg = pSegCOld.Segment[i];
                        oldarrayS[0] = pseg.FromPoint.X;
                        oldarrayS[1] = pseg.FromPoint.Y;
                        if (pseg.FromPoint.Z.ToString() == "NaN" || pseg.FromPoint.Z.ToString() == "非数字")
                        {
                           oldarrayS[2] = 0; 
                        }
                        else
                        {
                            oldarrayS[2] = pseg.FromPoint.Z;
                        }
                        matrix.coord_Trans(oldarrayS, rotateMat, tranVec, newarrayS);
                        pPointF.X = newarrayS[0];
                        pPointF.Y = newarrayS[1];
                        if (pseg.FromPoint.Z.ToString() == "NaN" || pseg.FromPoint.Z.ToString() == "非数字")
                        {
                            pPointF.Z = 0;
                        }
                        else
                        {
                            pPointF.Z = newarrayS[2];
                        }
                        pSegmentNEW.FromPoint = pPointF;
                        oldarrayT[0] = pseg.ToPoint.X;
                        oldarrayT[1] = pseg.ToPoint.Y;
                        if (pseg.ToPoint.Z.ToString() == "NaN" || pseg.ToPoint.Z.ToString() == "非数字")
                        {
                            oldarrayT[2] = 0;
                        }
                        else
                        {
                            oldarrayT[2] = pseg.ToPoint.Z;
                        }
                        matrix.coord_Trans(oldarrayT, rotateMat, tranVec, newarrayT);
                        pPointT.X = newarrayT[0];
                        pPointT.Y = newarrayT[1];
                        if (pseg.ToPoint.Z.ToString() == "NaN" || pseg.ToPoint.Z.ToString() == "非数字")
                        {
                            pPointT.Z = 0;
                        }
                        else
                        {
                            pPointT.Z = newarrayT[2];
                        }
                        pSegmentNEW.ToPoint = pPointT;
                        
                        pSegCNew.AddSegment(pSegmentNEW);
                    }
                    IFeature pFeatureTemp = pFtClassNew.CreateFeature();
                    pFeatureTemp.Shape = pSegCNew as IGeometry;
                    ClsGDBDataCommon.CopyFeatureFieldValue(pF, pFeatureTemp);
                    pFeatureTemp.Store();

                }

                else if (pFclass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    double[] oldarrayS = new double[3];
                    double[] oldarrayT = new double[3];
                    double[] newarrayS = new double[3];
                    double[] newarrayT = new double[3];
                    Matrix matrix = new Matrix();

                    IGeometryCollection pGCollectionOld = pF.Shape as IGeometryCollection;
                    IGeometryCollection pGCollectionNew = new PolygonClass();
                    ISegmentCollection psegCOld = new RingClass();
                   
                   
                    //ILine pLine = new LineClass();
                    IPoint pPointF = new PointClass();
                    IPoint pPointT = new PointClass();

                    //IPointCollection pPointCOld = new PolygonClass();
                    //IPointCollection pPointCNew = new PolygonClass();
                    //pPointCOld=pF.Shape as IPointCollection;
                    //for (int i = 0; i < pPointCOld.PointCount;i++ )
                    //{
                    //    pPointF = pPointCOld.Point[i];
                    //    oldarrayS[0] = pPointF.X;
                    //    oldarrayS[0] = pPointF.Y;
                    //    if (pPointF.Z.ToString() == "NaN" || pPointF.Z.ToString() == "非数字")
                    //    {
                    //        oldarrayS[2] = 0;//pPointOld.Z;
                    //    }
                    //    else
                    //    {
                    //        oldarrayS[2] = pPointF.Z;
                    //    }
                    //    matrix.coord_Trans(oldarrayS, rotateMat, tranVec, newarrayT);
                    //    pPointT.X = newarrayT[0];
                    //    pPointT.Y = newarrayT[1];
                    //    if (pPointF.Z.ToString() == "NaN" || pPointF.Z.ToString() == "非数字")
                    //    {
                    //        pPointT.Z = 0;
                    //    }
                    //    else
                    //    {
                    //        pPointT.Z = newarrayT[2];
                    //    }
                    //    pPointCNew.AddPoint(pPointT);
                    //}
                    //IPolygon pPolygon = pPointCNew as IPolygon;
                    //pPolygon.Close();
                    //IFeature pFeatureTemp = pFtClassNew.CreateFeature();
                    //pFeatureTemp.Shape = pPolygon as IGeometry;
                    //CopyFeatureField(pF, pFeatureTemp);
                    //pFeatureTemp.Store();

                    ///方法一
                    //IPolygon pPolygon=pF.Shape as IPolygon;
                    //IRing pExterRingOld = new RingClass();
                    //IRing pExterRingNew = new RingClass();

                    //for (int i = 0; i<pPolygon.ExteriorRingCount;i++ )
                    //{
                    //    pExterRingOld = pPolygon.FindExteriorRing(null);
                    //}


                    //方法二
                    for (int i = 0; i < pGCollectionOld.GeometryCount;i++ )
                    {                      
                        psegCOld = pGCollectionOld.Geometry[i] as ISegmentCollection;
                        ISegmentCollection pSegCNew = new RingClass();
                        for (int j = 0; j < psegCOld.SegmentCount -1; j++)
                        {
                            ISegment pSegmentNEW = new LineClass();
                            ISegment pseg = psegCOld.Segment[j];
                            oldarrayS[0] = pseg.FromPoint.X;
                            oldarrayS[1] = pseg.FromPoint.Y;
                            if (pseg.FromPoint.Z.ToString() == "NaN" || pseg.FromPoint.Z.ToString() == "非数字")
                            {
                                oldarrayS[2] = 0;
                            }
                            else
                            {
                                oldarrayS[2] = pseg.FromPoint.Z;
                            }
                            matrix.coord_Trans(oldarrayS, rotateMat, tranVec, newarrayS);
                            pPointF.X = newarrayS[0];
                            pPointF.Y = newarrayS[1];
                            if (pseg.FromPoint.Z.ToString() == "NaN" || pseg.FromPoint.Z.ToString() == "非数字")
                            {
                                pPointF.Z = 0;
                            }
                            else
                            {
                                pPointF.Z = newarrayS[2];
                            }
                            pSegmentNEW.FromPoint = pPointF;
                            oldarrayT[0] = pseg.ToPoint.X;
                            oldarrayT[1] = pseg.ToPoint.Y;
                            if (pseg.ToPoint.Z.ToString() == "NaN" || pseg.ToPoint.Z.ToString() == "非数字")
                            {
                                oldarrayT[2] = 0;
                            }
                            else
                            {
                                oldarrayT[2] = pseg.ToPoint.Z;
                            }
                            matrix.coord_Trans(oldarrayT, rotateMat, tranVec, newarrayT);
                            pPointT.X = newarrayT[0];
                            pPointT.Y = newarrayT[1];
                            if (pseg.ToPoint.Z.ToString() == "NaN" || pseg.ToPoint.Z.ToString() == "非数字")
                            {
                                pPointT.Z = 0;
                            }
                            else
                            {
                                pPointT.Z = newarrayT[2];
                            }
                            pSegmentNEW.ToPoint = pPointT;

                            pSegCNew.AddSegment(pSegmentNEW);
                        }
                        IRing pRing = pSegCNew as IRing;
                        pRing.Close();
                        object ob = Type.Missing;
                        pGCollectionNew.AddGeometry(pRing as IGeometry, ref ob, ref ob);

                    }
                    IFeature pFeatureTemp = pFtClassNew.CreateFeature();
                    pFeatureTemp.Shape = pGCollectionNew as IGeometry;
                    ClsGDBDataCommon.CopyFeatureFieldValue(pF, pFeatureTemp);
                    pFeatureTemp.Store();
                }


                pF = pFC.NextFeature();
            }

            IFeatureLayer ppFeatureLayer = new FeatureLayerClass();
            ppFeatureLayer.FeatureClass = pFtClassNew;
            ppFeatureLayer.Name = strName;
            pMapControl.AddLayer(ppFeatureLayer as ILayer);
            //pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            this.Close();
        }

        ///// <summary>
        ///// 添加字段值
        ///// </summary>
        ///// <param name="pFt"></param>
        ///// <param name="pFtNew"></param>
        //private void CopyFeatureField(IFeature pFt, IFeature pFtNew)
        //{
        //    IFields pFlds = pFt.Fields;
        //    IFields pFldsNew = pFtNew.Fields;
        //    int nCount = pFlds.FieldCount;
        //    for (int i = 0; i < nCount; i++)
        //    {
        //        if (pFldsNew.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry &&
        //           pFldsNew.get_Field(i).Type != esriFieldType.esriFieldTypeOID)
        //        {
        //            for (int j = 0; j < pFlds.FieldCount;j++ )
        //            {
        //                if (pFlds.Field[j].Name==pFldsNew.Field[i].Name)
        //                {
        //                    object ob = pFt.get_Value(j);
        //                    if (ob.ToString()=="")
        //                    {
        //                        pFtNew.set_Value(i, -9999);
        //                    }
        //                    else
        //                    {
        //                        pFtNew.set_Value(i, (object)pFt.get_Value(j));
        //                    }
                           
        //                    //pFtNew.set_Value(i, (object)pFlds.Field[j].ToString());
        //                    break;
        //                }                        
        //            }                  
        //        }
        //    }

        //}

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 设置矩阵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridMatrix_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 3 && e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                int nindex = e.RowIndex * 3 + e.ColumnIndex;               
                IsNumberic isnum = new IsNumberic();
                if (dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (isnum.IsNumber(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                    {
                        if (e.ColumnIndex==0&&e.RowIndex==0)//编辑第一行第一列
                        {
                            rotateMat[nindex] = Convert.ToDouble(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                            rotateMat[4]=rotateMat[0];
                            if (isgo)
                            {
                                if (dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                                {
                                    dataGridMatrix.Rows[1].Cells[1].Value = Convert.ToDouble(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                                }
                                else
                                {
                                    dataGridMatrix.Rows[1].Cells[1].Value = 0.0;
                                }
                            }                            
                            //dataGridMatrix.Rows[1].Cells[5].Value = Convert.ToDouble(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        }
                        else if (e.ColumnIndex == 1&&e.RowIndex==0)//编辑第一行第二列
                        {
                            rotateMat[nindex] = Convert.ToDouble(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                            rotateMat[3] = -rotateMat[1];
                            if (isgo)
                            {
                                if (dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                                {
                                    //dataGridMatrix.Rows[1].Cells[0].Value = -Convert.ToDouble(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                                }
                                else
                                {
                                    dataGridMatrix.Rows[1].Cells[0].Value = 0.0;
                                }

                            }                            
                            //dataGridMatrix.Rows[1].Cells[4].Value = -Convert.ToDouble(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        }
                        else
                        {
                            rotateMat[nindex] = Convert.ToDouble(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("请输入数字", "提示", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    rotateMat[nindex] = 0.0;
                }
                
            }
            else if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                if (dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    IsNumberic isnum = new IsNumberic();
                    if (isnum.IsNumber(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                    {
                        tranVec[e.RowIndex] = Convert.ToDouble(dataGridMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("请输入数字", "提示", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    tranVec[e.RowIndex] = 0.0;
                }
               
            }
        }

        private void btnImportXml_Click(object sender, EventArgs e)
        {

        }

        private void btnExportXml_Click(object sender, EventArgs e)
        {

        }

        private void cmbLayer_TextChanged(object sender, EventArgs e)
        {
            ILayer Player = null;
            for (int i = 0; i < pMapControl.LayerCount; i++)
            {
                if (pMapControl.Layer[i].Name == cmbLayer.SelectedItem.ToString())
                {
                    Player = pMapControl.Layer[i];
                    break;
                }
            }
            if (Player is IFeatureLayer)
            {
                isshp = true;
            }
            else if (Player is IRasterLayer)
            {
                isshp = false;
            }
        }


   }
}
