using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
using System.Collections;
using ESRI.ArcGIS.DataSourcesRaster;

namespace LibCerMap
{

    public class ClsProjectBackward
    {
        #region 常量定义区
        //定义坐标范围最大最小值
        public const int MAX_INT = 2147483647;
        public const int MIN_INT = -2147483648;
        #endregion

        public ClsCameraPara ParseXmlFileToGetPara(string szFilename)
        {
            try
            {
                ClsCameraPara result = new ClsCameraPara();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(szFilename);

                XmlNode cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dX");
                if (cs != null)
                {
                    result.dX = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dY");
                if (cs != null)
                {
                    result.dY = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dZ");
                if (cs != null)
                {
                    result.dZ = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dPhi");
                if (cs != null)
                {
                    result.dPhi = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dOmg");
                if (cs != null)
                {
                    result.dOmg = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dKappa");
                if (cs != null)
                {
                    result.dKappa = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dFocus");
                if (cs != null)
                {
                    result.dFocus = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dPx");
                if (cs != null)
                {
                    result.dPx = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dPy");
                if (cs != null)
                {
                    result.dPy = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/nW");
                if (cs != null)
                {
                    result.nW = int.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/nH");
                if (cs != null)
                {
                    result.nH = int.Parse(cs.InnerText);
                }

                //cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/szImageName");
                //if (cs != null)
                //{
                //    result.szImageName = cs.InnerText;
                //}

                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

//         private void CopyFeatureField(IFeature pFt, IFeatureBuffer pFtNew)
//         {
//             object o = Type.Missing;
//             IFields pFlds = pFt.Fields;
//             IFields pFldsNew = pFtNew.Fields;
//             int nCount = pFlds.FieldCount;
//             for (int i = 0; i < nCount; i++)
//             {
//                 if (pFlds.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry &&
//                    pFlds.get_Field(i).Type != esriFieldType.esriFieldTypeOID)
//                 {
//                     object ob = pFt.get_Value(i);
//                     if(!pFlds.get_Field(i).Editable)
//                         continue;
// 
//                     string szOb = ob as string;
//                     if (szOb == null || string.IsNullOrEmpty(szOb))
//                     {
//                         if (pFldsNew.get_Field(i).IsNullable)
//                         {
//                             pFtNew.set_Value(i, ob);
//                         }
//                         else
//                             pFtNew.set_Value(i, o);
//                     }
//                     else
//                     {
//                         //if (pFldsNew.get_Field(i).IsNullable)
//                         //{
//                         //    pFtNew.set_Value(i, ob);
//                         //}
// 
//                         pFtNew.set_Value(i, ob);
//                     }
//                 }
//             }
//         }

        //public bool projectBackward(string szInputShpFilename, string szDemFilename, string szXmlFilename, string szOutShpFilename)
        //{
        //    if (string.IsNullOrEmpty(szInputShpFilename) || string.IsNullOrEmpty(szDemFilename) || string.IsNullOrEmpty(szXmlFilename) || string.IsNullOrEmpty(szOutShpFilename))
        //        return false;

        //    ClsGDBDataCommon processDataCommon = new ClsGDBDataCommon();
        //    string strInputPath = System.IO.Path.GetDirectoryName(szInputShpFilename);
        //    string strInputName = System.IO.Path.GetFileName(szInputShpFilename);

        //    #region 读取SHP文件
        //    IWorkspace pWorkspace = processDataCommon.OpenFromShapefile(strInputPath);
        //    IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
        //    IFeatureClass pInputFC = pFeatureWorkspace.OpenFeatureClass(strInputName);
        //    #endregion

        //    #region 读取DEM文件
        //    IRasterLayer pRasterLayer = new RasterLayerClass();
        //    pRasterLayer.CreateFromFilePath(szDemFilename);
        //    IRaster pDemRaster = pRasterLayer.Raster;
        //    IRasterProps pDemRasterProps=pDemRaster as IRasterProps;
        //    #endregion

        //    #region 得到节点信息
        //    List<List<Pt3d>> ptFeaturePoints = new List<List<Pt3d>>();
        //    List<IFeature> pFeatureList=new List<IFeature>();
        //    IFeatureCursor pFCursor = pInputFC.Search(null, false);
        //    IFeature pFeature = null;// pFCursor.NextFeature();
        //    while ((pFeature = pFCursor.NextFeature()) != null)
        //    {
        //        IPolyline pPolyline = pFeature.Shape as IPolyline;
        //        if (pPolyline == null)
        //        {
        //            //pFeature = pFCursor.NextFeature();
        //            continue;
        //        }

        //        IPointCollection pPtCol = pPolyline as IPointCollection;
        //        List<Pt3d> ptSingleFeaturePoints = new List<Pt3d>();

        //        int nGeoRange = Convert.ToInt32(pDemRasterProps.MeanCellSize().X * 10);
        //        Pt2i ptLeftTop=new Pt2i();
        //        ClsGetCameraView getCameraView = new ClsGetCameraView();
        //        for (int i = 0; i < pPtCol.PointCount; i++)
        //        {
        //            IPoint pPt = pPtCol.get_Point(i);
        //            Pt3d ptFeaturePoint = new Pt3d();
        //            ptFeaturePoint.X = pPt.X;
        //            ptFeaturePoint.Y = pPt.Y;

        //            //从DEM上得到相应高程
        //            double[,] dbSubData=getCameraView.getSubDem(pDemRaster, ptFeaturePoint, nGeoRange, ref ptLeftTop);
        //            if (dbSubData == null)
        //                continue;

        //            if (getCameraView.GetGeoZ(pDemRaster, dbSubData, ptLeftTop, pPt.X, pPt.Y, ref ptFeaturePoint.Z))
        //            {
        //                ptSingleFeaturePoints.Add(ptFeaturePoint);
        //            }
        //        }

        //        IFeature pTmpFeature=pFeature;
        //        pFeatureList.Add(pTmpFeature);
        //        ptFeaturePoints.Add(ptSingleFeaturePoints);
        //        //pFeature = pFCursor.NextFeature();
        //    }
        //    #endregion

        //    #region 读取相机姿态参数
        //    ClsCameraPara cameraPara = ParseXmlFileToGetPara(szXmlFilename);
        //    if (cameraPara == null)
        //        return false;

        //    //根据姿态参数获得旋转矩阵
        //    OriAngle oriAngle = new OriAngle();
        //    Matrix pRotateMatrix = new Matrix(3, 3);

        //    oriAngle.kap = cameraPara.dKappa;
        //    oriAngle.omg = cameraPara.dOmg;
        //    oriAngle.phi = cameraPara.dPhi;
        //    if (!ClsGetCameraView.OPK2RMat(oriAngle, ref pRotateMatrix))
        //        return false;
        //    #endregion

        //    #region 创建输出SHP文件
        //    string strOutputPath = System.IO.Path.GetDirectoryName(szOutShpFilename);
        //    string strOutputName = System.IO.Path.GetFileName(szOutShpFilename);

        //    IFields pFields = pInputFC.Fields;
        //    //设置空间参考
        //    ISpatialReference pSpatialRef = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。
        //    pSpatialRef.SetDomain(-8000000, 8000000, -8000000, 8000000);//没有这句就抛异常来自HRESULT：0x8004120E。
        //    pSpatialRef.SetZDomain(-8000000, 8000000);
        //    IFeatureClass pOutputFC = processDataCommon.CreateShapefile(strOutputPath, strOutputName, pFields, pSpatialRef);           
        //    if (pOutputFC == null)
        //    {
        //        return false;
        //    }
        //    #endregion

        //    #region 节点位置反投影
        //    List<List<Pt2d>> ptImageFeaturePoints = new List<List<Pt2d>>();
        //    for (int i = 0; i < ptFeaturePoints.Count; i++)
        //    {
        //        int nCount = ptFeaturePoints[i].Count;

        //        List<Pt2d> ptSingleImageFeaturePoints = new List<Pt2d>();
        //        for (int j = 0; j < nCount; j++)
        //        {
        //            Pt3d ptCurrentFeaturePoint = ptFeaturePoints[i][j];
        //            double dbX, dbY, dbZ;
        //            dbX = pRotateMatrix.getNum(0, 0) * (ptCurrentFeaturePoint.X - cameraPara.dX) + pRotateMatrix.getNum(1, 0) * (ptCurrentFeaturePoint.Y - cameraPara.dY)
        //                + pRotateMatrix.getNum(2, 0) * (ptCurrentFeaturePoint.Z - cameraPara.dZ);
        //            dbY = pRotateMatrix.getNum(0, 1) * (ptCurrentFeaturePoint.X - cameraPara.dX) + pRotateMatrix.getNum(1, 1) * (ptCurrentFeaturePoint.Y - cameraPara.dY)
        //                + pRotateMatrix.getNum(2, 1) * (ptCurrentFeaturePoint.Z - cameraPara.dZ);
        //            dbZ = pRotateMatrix.getNum(0, 2) * (ptCurrentFeaturePoint.X - cameraPara.dX) + pRotateMatrix.getNum(1, 2) * (ptCurrentFeaturePoint.Y - cameraPara.dY)
        //                + pRotateMatrix.getNum(2, 2) * (ptCurrentFeaturePoint.Z - cameraPara.dZ);

        //            Pt2d ptImageTmp = new Pt2d();
        //            ptImageTmp.X = (int)Math.Round(-cameraPara.dFocus * dbX / dbZ);
        //            ptImageTmp.Y = (int)Math.Round(-cameraPara.dFocus * dbY / dbZ);

        //            //ptImageTmp.Y *= -1;
        //            ptImageTmp.X += 512;
        //            ptImageTmp.Y += 512;
        //            ptImageTmp.Y -= 1024;



        //            ptSingleImageFeaturePoints.Add(ptImageTmp);
        //        }
        //        ptImageFeaturePoints.Add(ptSingleImageFeaturePoints);
        //    }
        //    #endregion

        //    #region 写入节点信息
        //    IDataset dataset = (IDataset)pOutputFC;
        //    IWorkspace workspace = dataset.Workspace;

        //    //Cast for an IWorkspaceEdit
        //    IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;
        //    workspaceEdit.StartEditing(true);
        //    workspaceEdit.StartEditOperation();
        //    IFeatureCursor pOutFCursor = null;
        //    //////////////////////////////////////////////////////////////////////////
        //    for (int i = 0; i < ptImageFeaturePoints.Count; i++)
        //    {
        //        int nPtsCount = ptImageFeaturePoints[i].Count;
        //        IFeatureBuffer pFeatureBuffer = pOutputFC.CreateFeatureBuffer();
        //         pOutFCursor = pOutputFC.Insert(true);
        //        IPolyline pPolyline = new PolylineClass();
        //        IPointCollection pPointCollection = pPolyline as IPointCollection;
        //        for (int j = 0; j < nPtsCount; j++)
        //        {
        //            IPoint pt = new PointClass();
        //            pt.PutCoords(ptImageFeaturePoints[i][j].X, ptImageFeaturePoints[i][j].Y);

        //            pPointCollection.AddPoint(pt);
        //        }
        //        CopyFeatureField(pFeatureList[i],pFeatureBuffer);
        //        pFeatureBuffer.Shape = pPolyline as IGeometry;
        //        object featureOID = pOutFCursor.InsertFeature(pFeatureBuffer);
        //    }
        //    pOutFCursor.Flush();
        //    //featureCursor.Flush();
        //    workspaceEdit.StopEditOperation();
        //    workspaceEdit.StopEditing(true);
        //    #endregion

        //    return true;
        //}

        private bool IsInCameraFrontArea(Pt3d ptObj, Matrix pRotateMatrix, ClsCameraPara cameraPara)
        {            
            ////判断当前路径点是否在相机的可视范围内  视场角范围(-90, 90)，即在相平面的前方
            double dbCameraX = cameraPara.dX;
            double dbCameraY = cameraPara.dY;
            double dbCameraZ=cameraPara.dZ;
            double dbDeltaY = ptObj.Y - dbCameraY;
            double dbDeltaX = ptObj.X - dbCameraX;
            double dbDeltaZ = ptObj.Z - dbCameraZ;  

            //物方向量
            Matrix matrixObjectToCamera = new Matrix(3,1);
            matrixObjectToCamera.SetNum(0, 0, dbDeltaX);
            matrixObjectToCamera.SetNum(1, 0, dbDeltaY);
            matrixObjectToCamera.SetNum(2, 0, dbDeltaZ);

            //主点向量转到物方
            Matrix matrixFocus = new Matrix(3,1);
            matrixFocus.SetNum(0, 0, 0);
            matrixFocus.SetNum(1, 0, 0);
            matrixFocus.SetNum(2, 0, -1.0*cameraPara.dFocus);
            Matrix matrixFocusToObject = pRotateMatrix.Mutiply(pRotateMatrix, matrixFocus);

            //点乘
            double dbValue = matrixObjectToCamera.getNum(0, 0) * matrixFocusToObject.getNum(0, 0) +
                matrixObjectToCamera.getNum(1, 0) * matrixFocusToObject.getNum(1, 0) +
                matrixObjectToCamera.getNum(2, 0) * matrixFocusToObject.getNum(2, 0);

            return (dbValue > 0);
            //try
            //{
            //    //计算以东朝向为准的方位角 [0, 2*PI]

            //    // 返回结果:
            //    //     角度 θ，以弧度为单位，满足 -π≤θ≤π，且 tan(θ) = y / x，其中 (x, y) 是笛卡尔平面中的一个点。请看下面：如果 (x,
            //    //     y) 在第 1 象限，则 0 < θ < π/2。如果 (x, y) 在第 2 象限，则 π/2 < θ≤π。如果 (x, y) 在第 3 象限，则
            //    //     -π < θ < -π/2。如果 (x, y) 在第 4 象限，则 -π/2 < θ < 0。如果点在象限的边界上，则返回值如下：如果 y 为 0
            //    //     并且 x 不为负值，则 θ = 0。如果 y 为 0 并且 x 为负值，则 θ = π。如果 y 为正值并且 x 为 0，则 θ = π/2。如果
            //    //     y 负值并且 x 为 0，则 θ = -π/2。
            //    double dbAzimuth = Math.Atan2(dbDeltaY, dbDeltaX);
            //    if (dbDeltaY < 0)  //第三第四象限
            //        dbAzimuth += (2 * Math.PI);

            //    //KAPPA+PI/2的原因：相机方位以北为0度，逆时针增加；而方位角以东为0度，逆时针增加，两者恒差90
            //    double dbDeltaAngle = Math.Abs(dbAzimuth - (cameraPara.dKappa + Math.PI / 2));
            //    if (dbDeltaAngle > (Math.PI / 2))
            //        return null;
            //}
            //catch (System.Exception ex)
            //{
            //    return null;
            //}            
        }

        private Pt2d getProjectBackwardPoint(IPoint pGeoCoords, IRaster pDemRaster, ClsGetCameraView pGetCameraView,
            Matrix pRotateMatrix, ClsCameraPara cameraPara)
        {
            Pt3d ptFeaturePoint = new Pt3d();
            ptFeaturePoint.X = pGeoCoords.X;
            ptFeaturePoint.Y = pGeoCoords.Y;
            
            //从DEM上得到相应高程
            IRasterProps pDemRasterProps = pDemRaster as IRasterProps;
            double nGeoRange = pDemRasterProps.MeanCellSize().X * 10;
            Pt2i ptLeftTop = new Pt2i();
            double[,] dbSubData = pGetCameraView.getSubDem(pDemRaster, ptFeaturePoint, nGeoRange, ref ptLeftTop);
            if (dbSubData == null)
                return null;

            if (pGetCameraView.GetGeoZ(pDemRaster, dbSubData, ptLeftTop, ptFeaturePoint.X, ptFeaturePoint.Y, ref ptFeaturePoint.Z))
            {
                Pt2d pt2d = new Pt2d();

                double dbX, dbY, dbZ;
                Pt3d ptCurrentFeaturePoint = ptFeaturePoint;

                //判断是否在视场前方
                if (!IsInCameraFrontArea(ptCurrentFeaturePoint, pRotateMatrix, cameraPara))
                    return null;

                dbX = pRotateMatrix.getNum(0, 0) * (ptCurrentFeaturePoint.X - cameraPara.dX) + pRotateMatrix.getNum(1, 0) * (ptCurrentFeaturePoint.Y - cameraPara.dY)
                    + pRotateMatrix.getNum(2, 0) * (ptCurrentFeaturePoint.Z - cameraPara.dZ);
                dbY = pRotateMatrix.getNum(0, 1) * (ptCurrentFeaturePoint.X - cameraPara.dX) + pRotateMatrix.getNum(1, 1) * (ptCurrentFeaturePoint.Y - cameraPara.dY)
                    + pRotateMatrix.getNum(2, 1) * (ptCurrentFeaturePoint.Z - cameraPara.dZ);
                dbZ = pRotateMatrix.getNum(0, 2) * (ptCurrentFeaturePoint.X - cameraPara.dX) + pRotateMatrix.getNum(1, 2) * (ptCurrentFeaturePoint.Y - cameraPara.dY)
                    + pRotateMatrix.getNum(2, 2) * (ptCurrentFeaturePoint.Z - cameraPara.dZ);

                Pt2d ptImageTmp = new Pt2d();
                ptImageTmp.X = (int)Math.Round(-cameraPara.dFocus * dbX / dbZ);
                ptImageTmp.Y = (int)Math.Round(-cameraPara.dFocus * dbY / dbZ);

                //ptImageTmp.Y *= -1;
                //像主点坐标系===>笛卡尔坐标系（以东为X，以北为Y）
                ptImageTmp.X += cameraPara.dPx;
                ptImageTmp.Y += (-1 * cameraPara.dPy);
                //ptImageTmp.Y *= -1;
                

                //arcgis显示时把左上角放于原点
                //ptImageTmp.Y -= cameraPara.nH;

                return ptImageTmp;
            }

            return null;
        }

        public bool projectBackward(string szInputShpFilename, IRasterLayer pDemLayer, string szXmlFilename, string szOutShpFilename)
        {
            if (string.IsNullOrEmpty(szInputShpFilename) || pDemLayer==null || string.IsNullOrEmpty(szXmlFilename) || string.IsNullOrEmpty(szOutShpFilename))
                return false;

            ClsGDBDataCommon processDataCommon = new ClsGDBDataCommon();
            string strInputPath = System.IO.Path.GetDirectoryName(szInputShpFilename);
            string strInputName = System.IO.Path.GetFileName(szInputShpFilename);

            #region 读取SHP文件
            IWorkspace pWorkspace = processDataCommon.OpenFromShapefile(strInputPath);
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            IFeatureClass pInputFC = pFeatureWorkspace.OpenFeatureClass(strInputName);
            #endregion

            #region 读取DEM文件
            IRasterLayer pRasterLayer = pDemLayer;// new RasterLayerClass();
            //pRasterLayer.CreateFromFilePath(szDemFilename);
            IRaster pDemRaster = pRasterLayer.Raster;
            IRasterProps pDemRasterProps = pDemRaster as IRasterProps;
            #endregion

            #region 得到曲线信息
            List<ISegmentCollection> pSegmentCollection = new List<ISegmentCollection>();
            List<IFeature> pFeatureList = new List<IFeature>();
            IFeatureCursor pFCursor = pInputFC.Search(null, false);
            IFeature pFeature = null;// pFCursor.NextFeature();
            while ((pFeature = pFCursor.NextFeature()) != null)
            {
                IPolyline pPolyline = pFeature.Shape as IPolyline;
                if (pPolyline == null)
                {
                    //pFeature = pFCursor.NextFeature();
                    continue;
                }

                IFeature pTmpFeature = pFeature;
                pFeatureList.Add(pTmpFeature);

                ISegmentCollection pCurrentSegmentCollection = pPolyline as ISegmentCollection;
                pSegmentCollection.Add(pCurrentSegmentCollection);
            }
            #endregion

            #region 读取相机姿态参数
            ClsCameraPara cameraPara = ParseXmlFileToGetPara(szXmlFilename);
            if (cameraPara == null)
                return false;

            //根据姿态参数获得旋转矩阵
            OriAngle oriAngle = new OriAngle();
            Matrix pRotateMatrix = new Matrix(3, 3);

            oriAngle.kap = cameraPara.dKappa;
            oriAngle.omg = cameraPara.dOmg;
            oriAngle.phi = cameraPara.dPhi;
            if (!ClsGetCameraView.OPK2RMat(oriAngle, ref pRotateMatrix))
                return false;
            #endregion

            #region 创建输出SHP文件
            string strOutputPath = System.IO.Path.GetDirectoryName(szOutShpFilename);
            string strOutputName = System.IO.Path.GetFileName(szOutShpFilename);
            string strPointOutputName = System.IO.Path.GetFileNameWithoutExtension(szOutShpFilename) + "_point" + System.IO.Path.GetExtension(szOutShpFilename);

            //设置空间参考
            ISpatialReference pSpatialRef = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。
            pSpatialRef.SetDomain(MIN_INT, MAX_INT, MIN_INT, MAX_INT);//没有这句就抛异常来自HRESULT：0x8004120E。
            pSpatialRef.SetZDomain(MIN_INT, MAX_INT);

            //创建POLYLINE文件的字段
            IFields pFields = pInputFC.Fields;
            
            //创建点SHP文件的字段
            IFields pPointFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pPointFields;

            //设置字段   
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;

            //创建类型为几何类型的字段   
            pFieldEdit.Name_2 = "shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            pGeoDefEdit.HasM_2 = false;
            pGeoDefEdit.HasZ_2 = false;
            pGeoDefEdit.SpatialReference_2 = pSpatialRef;
            //pFieldEdit.Name_2 = "SHAPE";
            //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            pFieldEdit.GeometryDef_2 = pGeoDef;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Required_2 = true;
            pFieldsEdit.AddField(pField);

            IFeatureClass pOutputFC = processDataCommon.CreateShapefile(strOutputPath, strOutputName, pFields, pSpatialRef);
            IFeatureClass pOutputPointFC = processDataCommon.CreateShapefile(strOutputPath, strPointOutputName, pPointFields, pSpatialRef);
            if (pOutputFC == null || pOutputPointFC==null)
            {
                return false;
            }
            #endregion

            #region 节点位置反投影
            List<List<Pt2d>> ptImageFeaturePoints = new List<List<Pt2d>>();
            List<List<Pt2d>> ptImageFeaturePointsForPointShp = new List<List<Pt2d>>();
            ClsGetCameraView pGetCameraView = new ClsGetCameraView();

            for (int i = 0; i < pSegmentCollection.Count; i++)
            {
                List<Pt2d> ptSingleImageFeaturePoints = new List<Pt2d>();
                List<Pt2d> ptSingleImageFeaturePointsForPointShp = new List<Pt2d>();
                for (int j = 0; j < pSegmentCollection[i].SegmentCount; j++)
                { 
                    ISegment pCurrentSegment = pSegmentCollection[i].get_Segment(j);
                    if (pCurrentSegment == null)
                        continue;

                    //直线
                    if (pCurrentSegment.GeometryType == esriGeometryType.esriGeometryLine)
                    {
                        ILine pLine = pCurrentSegment as ILine;

                        //起始点
                        IPoint ptFrom = pLine.FromPoint;
                        Pt2d ptFrom2d = getProjectBackwardPoint(ptFrom, pDemRaster, pGetCameraView, pRotateMatrix, cameraPara);
                        if (ptFrom2d != null)
                        {
                            ptSingleImageFeaturePoints.Add(ptFrom2d);
                            ptSingleImageFeaturePointsForPointShp.Add(ptFrom2d);
                        }

                        //终止点
                        IPoint ptTo = pLine.ToPoint;
                        Pt2d ptTo2d = getProjectBackwardPoint(ptTo, pDemRaster, pGetCameraView, pRotateMatrix, cameraPara);
                        if (ptTo2d != null)
                        {
                            ptSingleImageFeaturePoints.Add(ptTo2d);
                            if (j == pSegmentCollection[i].SegmentCount - 1)   //最后一条直线段
                                ptSingleImageFeaturePointsForPointShp.Add(ptTo2d);
                        }
                    }
                    else if (pCurrentSegment.GeometryType == esriGeometryType.esriGeometryCircularArc) //圆弧
                    {
                        ICircularArc pCircularArc = pCurrentSegment as ICircularArc;
                        double dbCurveLength = pCircularArc.Length;
                        int nCount = 15;
                        double dbOffset = dbCurveLength / nCount;
                        for (int nCurrent = 0; nCurrent < nCount; nCurrent++)
                        {
                            IPoint ptCurrent = new Point();
                            pCircularArc.QueryPoint(esriSegmentExtension.esriNoExtension, nCurrent * dbOffset, false, ptCurrent);
                            Pt2d ptCurrentImagePoint = getProjectBackwardPoint(ptCurrent, pDemRaster, pGetCameraView, pRotateMatrix, cameraPara);
                            if (ptCurrentImagePoint != null)
                            {
                                ptSingleImageFeaturePoints.Add(ptCurrentImagePoint);

                                //只保留端点的信息，用于创建点SHP图层
                                if (nCurrent == 0 || nCurrent == nCount - 1)
                                    ptSingleImageFeaturePointsForPointShp.Add(ptCurrentImagePoint);
                            }
                        }
                    }
                    else
                        continue;
                }

                ptImageFeaturePoints.Add(ptSingleImageFeaturePoints);
                ptImageFeaturePointsForPointShp.Add(ptSingleImageFeaturePointsForPointShp);
            }
            #endregion

            #region 写入POLYLINE节点信息
            IDataset dataset = (IDataset)pOutputFC;
            IWorkspace workspace = dataset.Workspace;

            //Cast for an IWorkspaceEdit
            IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();

            //////////////////////////////////////////////////////////////////////////
            //整条路线的添加
            IFeatureCursor pOutFCursor = null;            
            for (int i = 0; i < ptImageFeaturePoints.Count; i++)
            {
                int nPtsCount = ptImageFeaturePoints[i].Count;
                IFeatureBuffer pFeatureBuffer = pOutputFC.CreateFeatureBuffer();
                pOutFCursor = pOutputFC.Insert(true);

                IPolyline pPolyline = new PolylineClass();
                IPointCollection pPointCollection = pPolyline as IPointCollection;

                for (int j = 0; j < nPtsCount; j++)
                {
                    IPoint pt = new PointClass();
                    pt.PutCoords(ptImageFeaturePoints[i][j].X, ptImageFeaturePoints[i][j].Y);
                    pPointCollection.AddPoint(pt);
                }

                if (!ClsGDBDataCommon.CopyFeatureFieldValue(pFeatureList[i], pFeatureBuffer as IFeature))
                    continue;

                pFeatureBuffer.Shape = pPolyline as IGeometry;
                object featureOID = pOutFCursor.InsertFeature(pFeatureBuffer);
            }
            pOutFCursor.Flush();

            //分段路线的添加
            for (int i = 0; i < ptImageFeaturePoints.Count; i++)
            {
                int nPtsCount = ptImageFeaturePoints[i].Count;
                for (int j = 0; j < nPtsCount-1; j++)
                {
                    IFeatureBuffer pFeatureBuffer = pOutputFC.CreateFeatureBuffer();
                    pOutFCursor = pOutputFC.Insert(true);
                    IPolyline pPolyline = new PolylineClass();
                    IPointCollection pPointCollection = pPolyline as IPointCollection;

                    IPoint pt = new PointClass();
                    pt.PutCoords(ptImageFeaturePoints[i][j].X, ptImageFeaturePoints[i][j].Y);
                    pPointCollection.AddPoint(pt);
                    pt.PutCoords(ptImageFeaturePoints[i][j+1].X, ptImageFeaturePoints[i][j+1].Y);
                    pPointCollection.AddPoint(pt);

                    if (!ClsGDBDataCommon.CopyFeatureFieldValue(pFeatureList[i], pFeatureBuffer as IFeature))
                        continue;
                    pFeatureBuffer.Shape = pPolyline as IGeometry;
                    object featureOID = pOutFCursor.InsertFeature(pFeatureBuffer);
                }
            }
            pOutFCursor.Flush();
            //featureCursor.Flush();
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);
            #endregion

#region 点SHP文件的添加
            IDataset pPointDataset = (IDataset)pOutputPointFC;
            IWorkspace pPointWorkspace = pPointDataset.Workspace;

            //Cast for an IWorkspaceEdit
            IWorkspaceEdit pPointWorkspaceEdit = pPointWorkspace as IWorkspaceEdit;
            pPointWorkspaceEdit.StartEditing(true);
            pPointWorkspaceEdit.StartEditOperation();

            IFeatureCursor pOutPointFCursor = null;
            for (int i = 0; i < ptImageFeaturePointsForPointShp.Count; i++)
            {
                //int nSegmentCount = pSegmentCollection[i].SegmentCount;

                //IMultipoint pMultiPoint = new MultipointClass();
                for (int j = 0; j < ptImageFeaturePointsForPointShp[i].Count; j++)
                {
                    IFeatureBuffer pFeatureBuffer = pOutputPointFC.CreateFeatureBuffer();
                    pOutPointFCursor = pOutputPointFC.Insert(true);
                    IPoint pt = new PointClass();
                    Pt2d pt2d = ptImageFeaturePointsForPointShp[i][j];
                    pt.PutCoords(pt2d.X, pt2d.Y);

                    //CopyFeatureField(pFeatureList[i], pFeatureBuffer);
                    pFeatureBuffer.Shape = pt as IGeometry;
                    object featureOID = pOutPointFCursor.InsertFeature(pFeatureBuffer);
                }
            }
            pOutPointFCursor.Flush();

            //featureCursor.Flush();
            pPointWorkspaceEdit.StopEditOperation();
            pPointWorkspaceEdit.StopEditing(true);
#endregion

            return true;
        }

        public bool projectBackward(IFeatureClass pInputFeatureClass, IRasterLayer pDemLayer, string szXmlFilename,
            ref IFeatureClass pOutputPolylineFeatureClass, ref IFeatureClass pOutputPointFeatureClass)
        {
            if (pInputFeatureClass == null || pDemLayer==null || string.IsNullOrEmpty(szXmlFilename) || pOutputPointFeatureClass==null || pOutputPolylineFeatureClass==null)
                return false;

            #region 读取DEM文件
            IRasterLayer pRasterLayer = pDemLayer;// new RasterLayerClass();
            //pRasterLayer.CreateFromFilePath(szDemFilename);
            IRaster pDemRaster = pRasterLayer.Raster;
            IRasterProps pDemRasterProps = pDemRaster as IRasterProps;
            #endregion

            #region 读取相机姿态参数
            ClsCameraPara cameraPara = ParseXmlFileToGetPara(szXmlFilename);
            if (cameraPara == null)
                return false;

            //根据姿态参数获得旋转矩阵
            OriAngle oriAngle = new OriAngle();
            Matrix pRotateMatrix = new Matrix(3, 3);

            oriAngle.kap = cameraPara.dKappa;
            oriAngle.omg = cameraPara.dOmg;
            oriAngle.phi = cameraPara.dPhi;
            if (!ClsGetCameraView.OPK2RMat(oriAngle, ref pRotateMatrix))
                return false;
            #endregion

            #region 得到曲线信息
            List<ISegmentCollection> pSegmentCollection = new List<ISegmentCollection>();
            List<IFeature> pFeatureList = new List<IFeature>();
            IFeatureCursor pFCursor = pInputFeatureClass.Search(null, false);
            IFeature pFeature = null;// pFCursor.NextFeature();
            while ((pFeature = pFCursor.NextFeature()) != null)
            {
                IPolyline pPolyline = pFeature.Shape as IPolyline;
                if (pPolyline == null)
                {
                    //pFeature = pFCursor.NextFeature();
                    continue;
                }

                IFeature pTmpFeature = pFeature;
                pFeatureList.Add(pTmpFeature);

                ISegmentCollection pCurrentSegmentCollection = pPolyline as ISegmentCollection;
                pSegmentCollection.Add(pCurrentSegmentCollection);
            }
            #endregion

            #region 节点位置反投影
            List<List<Pt2d>> ptImageFeaturePoints = new List<List<Pt2d>>();
            List<List<Pt2d>> ptImageFeaturePointsForPointShp = new List<List<Pt2d>>();
            ClsGetCameraView pGetCameraView = new ClsGetCameraView();

            for (int i = 0; i < pSegmentCollection.Count; i++)
            {
                List<Pt2d> ptSingleImageFeaturePoints = new List<Pt2d>();
                List<Pt2d> ptSingleImageFeaturePointsForPointShp = new List<Pt2d>();
                for (int j = 0; j < pSegmentCollection[i].SegmentCount; j++)
                {
                    ISegment pCurrentSegment = pSegmentCollection[i].get_Segment(j);
                    if (pCurrentSegment == null)
                        continue;

                    //直线
                    if (pCurrentSegment.GeometryType == esriGeometryType.esriGeometryLine)
                    {
                        ILine pLine = pCurrentSegment as ILine;

                        //起始点
                        IPoint ptFrom = pLine.FromPoint;
                        Pt2d ptFrom2d = getProjectBackwardPoint(ptFrom, pDemRaster, pGetCameraView, pRotateMatrix, cameraPara);
                        if (ptFrom2d != null)
                        {
                            ptSingleImageFeaturePoints.Add(ptFrom2d);
                            ptSingleImageFeaturePointsForPointShp.Add(ptFrom2d);
                        }

                        //终止点
                        IPoint ptTo = pLine.ToPoint;
                        Pt2d ptTo2d = getProjectBackwardPoint(ptTo, pDemRaster, pGetCameraView, pRotateMatrix, cameraPara);
                        if (ptTo2d != null)
                        {
                            if (j == pSegmentCollection[i].SegmentCount - 1)   //最后一条直线段
                            {
                                ptSingleImageFeaturePointsForPointShp.Add(ptTo2d);
                                ptSingleImageFeaturePoints.Add(ptTo2d);
                            }
                        }
                    }
                    else if (pCurrentSegment.GeometryType == esriGeometryType.esriGeometryCircularArc) //圆弧
                    {
                        ICircularArc pCircularArc = pCurrentSegment as ICircularArc;
                        double dbCurveLength = pCircularArc.Length;
                        int nCount = 15;
                        double dbOffset = dbCurveLength / nCount;
                        for (int nCurrent = 0; nCurrent <= nCount; nCurrent++)
                        {
                            IPoint ptCurrent = new Point();
                            pCircularArc.QueryPoint(esriSegmentExtension.esriNoExtension, nCurrent * dbOffset, false, ptCurrent);
                            Pt2d ptCurrentImagePoint = getProjectBackwardPoint(ptCurrent, pDemRaster, pGetCameraView, pRotateMatrix, cameraPara);
                            if (ptCurrentImagePoint != null)
                            {
                                ptSingleImageFeaturePoints.Add(ptCurrentImagePoint);

                                //只保留端点的信息，用于创建点SHP图层
                                if (nCurrent == 0 || nCurrent == nCount)
                                    ptSingleImageFeaturePointsForPointShp.Add(ptCurrentImagePoint);
                            }
                        }
                    }
                    else
                        continue;
                }

                ptImageFeaturePoints.Add(ptSingleImageFeaturePoints);
                ptImageFeaturePointsForPointShp.Add(ptSingleImageFeaturePointsForPointShp);
            }
            #endregion

            #region 把节点位置信息写入
            IDataset dataset = (IDataset)pOutputPolylineFeatureClass;
            IWorkspace workspace = dataset.Workspace;

            //Cast for an IWorkspaceEdit
            IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();

            //////////////////////////////////////////////////////////////////////////
            //整条路线的添加
            IFeatureCursor pOutFCursor = null;
            for (int i = 0; i < ptImageFeaturePoints.Count; i++)
            {
                int nPtsCount = ptImageFeaturePoints[i].Count;
                IFeatureBuffer pFeatureBuffer = pOutputPolylineFeatureClass.CreateFeatureBuffer();
                pOutFCursor = pOutputPolylineFeatureClass.Insert(true);

                IPolyline pPolyline = new PolylineClass();
                IPointCollection pPointCollection = pPolyline as IPointCollection;

                for (int j = 0; j < nPtsCount; j++)
                {
                    IPoint pt = new PointClass();
                    pt.PutCoords(ptImageFeaturePoints[i][j].X, ptImageFeaturePoints[i][j].Y);
                    pPointCollection.AddPoint(pt);
                }

                if (!ClsGDBDataCommon.CopyFeatureFieldValue(pFeatureList[i], pFeatureBuffer as IFeature))
                    continue;
                pFeatureBuffer.Shape = pPolyline as IGeometry;
                object featureOID = pOutFCursor.InsertFeature(pFeatureBuffer);
            }

            if(pOutFCursor!=null)
                pOutFCursor.Flush();

            //分段路线的添加
            for (int i = 0; i < ptImageFeaturePoints.Count; i++)
            {
                int nPtsCount = ptImageFeaturePoints[i].Count;
                for (int j = 0; j < nPtsCount - 1; j++)
                {
                    IFeatureBuffer pFeatureBuffer = pOutputPolylineFeatureClass.CreateFeatureBuffer();
                    pOutFCursor = pOutputPolylineFeatureClass.Insert(true);
                    IPolyline pPolyline = new PolylineClass();
                    IPointCollection pPointCollection = pPolyline as IPointCollection;

                    IPoint pt = new PointClass();
                    pt.PutCoords(ptImageFeaturePoints[i][j].X, ptImageFeaturePoints[i][j].Y);
                    pPointCollection.AddPoint(pt);
                    pt.PutCoords(ptImageFeaturePoints[i][j + 1].X, ptImageFeaturePoints[i][j + 1].Y);
                    pPointCollection.AddPoint(pt);

                    if (!ClsGDBDataCommon.CopyFeatureFieldValue(pFeatureList[i], pFeatureBuffer as IFeature))
                        continue;
                    pFeatureBuffer.Shape = pPolyline as IGeometry;
                    object featureOID = pOutFCursor.InsertFeature(pFeatureBuffer);
                }
            }
            if(pOutFCursor!=null)
                pOutFCursor.Flush();

            //featureCursor.Flush();
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);
            #endregion

            #region 点SHP文件的添加
            IDataset pPointDataset = (IDataset)pOutputPointFeatureClass;
            IWorkspace pPointWorkspace = pPointDataset.Workspace;

            //Cast for an IWorkspaceEdit
            IWorkspaceEdit pPointWorkspaceEdit = pPointWorkspace as IWorkspaceEdit;
            pPointWorkspaceEdit.StartEditing(true);
            pPointWorkspaceEdit.StartEditOperation();

            IFeatureCursor pOutPointFCursor = null;
            for (int i = 0; i < ptImageFeaturePointsForPointShp.Count; i++)
            {
                //int nSegmentCount = pSegmentCollection[i].SegmentCount;

                //IMultipoint pMultiPoint = new MultipointClass();
                for (int j = 0; j < ptImageFeaturePointsForPointShp[i].Count; j++)
                {
                    IFeatureBuffer pFeatureBuffer = pOutputPointFeatureClass.CreateFeatureBuffer();
                    pOutPointFCursor = pOutputPointFeatureClass.Insert(true);
                    IPoint pt = new PointClass();
                    Pt2d pt2d = ptImageFeaturePointsForPointShp[i][j];
                    pt.PutCoords(pt2d.X, pt2d.Y);

                    //CopyFeatureField(pFeatureList[i], pFeatureBuffer);
                    pFeatureBuffer.Shape = pt as IGeometry;
                    object featureOID = pOutPointFCursor.InsertFeature(pFeatureBuffer);
                }
            }

            if (pOutPointFCursor != null)
                pOutPointFCursor.Flush();

            //featureCursor.Flush();
            pPointWorkspaceEdit.StopEditOperation();
            pPointWorkspaceEdit.StopEditing(true);
            #endregion

            return true;
        }
    }
}
