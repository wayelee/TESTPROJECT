/************************************************************
  Copyright (C), 2011-2012, PMRS Lab, IRSA, CAS
  文件名称: GISTool.cpp
  创建日期: 2011.11.6
  作    者: 李巍
  描    述: 地形分析计算
  版本编号：1.0
  修改历史:   <作者>   <时间>   <版本编号>   <描述>

***********************************************************/

#include "GISTool.h"

CPL_CVSID("$Id: gdal_contour.cpp 21191 2010-12-03 20:02:34Z rouault $");

/*************************************************
  函数名称:    mlComeputeContour
  作    者:   李巍
  功能描述：   等高线生成
  输    入：
  参数1：等高距
  参数2：DEM文件路径
  参数3：输出的shape文件路径
  参数4：是否自定义Nodata
  参数5：如果bCNodata设置为true，则dNodata 的值在计算时被当做无效值对待
  参数6：生成shape文件高程的属性名称，默认为elev
  输    出： 返回值：1，正常执行；-1，gdal版本过低；-2，等高距设置有误；-3，输入文件名有误；-4 ，输出文件名有误
  版本编号：   1.0
  修改历史：   <作者>   <时间>   <版本号>   <描述>
  *************************************************/
int mlComeputeContour(double dHinterval, char* strSrcfilename, char* strDstfilename ,bool bCNodata , double dNodata ,char* strAttrib )
{
    GDALDatasetH	hSrcDS;
    int b3D = FALSE, bIgnoreNoData = FALSE;
    int bNoDataSet = bCNodata;
    int nBandIn = 1;
    double dfNoData = dNodata;
    double dfInterval = 0.0,dfOffset = 0.0;
    const char *pszSrcFilename = NULL;
    const char *pszDstFilename = NULL;
    const char *pszElevAttrib = NULL;
    const char *pszFormat = "ESRI Shapefile";
    double adfFixedLevels[1000];
    int    nFixedLevelCount = 0;
    const char *pszNewLayerName = "contour";
    int bQuiet = FALSE;
    GDALProgressFunc pfnProgress = NULL;

    /* Check that we are running against at least GDAL 1.4 */
    /* Note to developers : if we use newer API, please change the requirement */
    if (atoi(GDALVersionInfo("VERSION_NUM")) < 1400)
    {
        // gdal 版本过低
        return -1;
    }

    GDALAllRegister();
    OGRRegisterAll();

    pszElevAttrib = strAttrib;
    dfInterval = dHinterval;
    if( dfInterval == 0.0 && nFixedLevelCount == 0 )
    {
        // 等高距为零
       return -2;
    }
    pszSrcFilename = strSrcfilename;
    if (pszSrcFilename == NULL)
    {
        //  源文件输入有误
        return -3;
    }
    pszDstFilename = strDstfilename;
    if (pszDstFilename == NULL)
    {
        // 目的文件输入有误
        return -4;
    }

    if (!bQuiet)
    {
        pfnProgress = GDALTermProgress;
    }


/* -------------------------------------------------------------------- */
/*      Open source raster file.                                        */
/* -------------------------------------------------------------------- */
    GDALRasterBandH hBand;
    hSrcDS = GDALOpen( pszSrcFilename, GA_ReadOnly );
    if( hSrcDS == NULL )
    {
        // 源文件输入有误
        return -3;
    }
    nBandIn = 1;
    hBand = GDALGetRasterBand( hSrcDS, nBandIn );
    if( hBand == NULL )
    {
        CPLError( CE_Failure, CPLE_AppDefined,
                  "Band %d does not exist on dataset.",
                  nBandIn );
                // 源文件输入有误
        return -3;
    }
    if( !bNoDataSet && !bIgnoreNoData )
    {
        dfNoData = GDALGetRasterNoDataValue( hBand, &bNoDataSet );
    }

/* -------------------------------------------------------------------- */
/*      Try to get a coordinate system from the raster.                 */
/* -------------------------------------------------------------------- */
    OGRSpatialReferenceH hSRS = NULL;

    const char *pszWKT = GDALGetProjectionRef( hSrcDS );

    if( pszWKT != NULL && strlen(pszWKT) != 0 )
    {
        hSRS = OSRNewSpatialReference( pszWKT );
    }
/* -------------------------------------------------------------------- */
/*      Create the outputfile.                                          */
/* -------------------------------------------------------------------- */
    OGRDataSourceH hDS;
    OGRSFDriverH hDriver = OGRGetDriverByName( pszFormat );
    OGRFieldDefnH hFld;
    OGRLayerH hLayer;

    hDS = OGR_Dr_CreateDataSource( hDriver, pszDstFilename, NULL );
    if( hDS == NULL )
    {
        // 目的文件输入有误
        return -4;
    }


    hLayer = OGR_DS_CreateLayer( hDS, pszNewLayerName, hSRS,
                                 b3D ? wkbLineString25D : wkbLineString,
                                 NULL );
    if( hLayer == NULL )
    {
        // 目的文件输有误
        return -4;
    }

    hFld = OGR_Fld_Create( "ID", OFTInteger );
    OGR_Fld_SetWidth( hFld, 8 );
    OGR_L_CreateField( hLayer, hFld, FALSE );
    OGR_Fld_Destroy( hFld );

    if( pszElevAttrib )
    {
        hFld = OGR_Fld_Create( pszElevAttrib, OFTReal );
        OGR_Fld_SetWidth( hFld, 12 );
        OGR_Fld_SetPrecision( hFld, 3 );
        OGR_L_CreateField( hLayer, hFld, FALSE );
        OGR_Fld_Destroy( hFld );
    }

/* -------------------------------------------------------------------- */
/*      Invoke.                                                         */
/* -------------------------------------------------------------------- */
    CPLErr eErr;

    eErr = GDALContourGenerate( hBand, dfInterval, dfOffset,
                         nFixedLevelCount, adfFixedLevels,
                         bNoDataSet, dfNoData, hLayer,
                         OGR_FD_GetFieldIndex( OGR_L_GetLayerDefn( hLayer ),
                                               "ID" ),
                         (pszElevAttrib == NULL) ? -1 :
                                 OGR_FD_GetFieldIndex( OGR_L_GetLayerDefn( hLayer ),
                                                       pszElevAttrib ),
                         pfnProgress, NULL );

    OGR_DS_Destroy( hDS );
    GDALClose( hSrcDS );

    if (hSRS)
    {
        OSRDestroySpatialReference( hSRS );
    }
    GDALDestroyDriverManager();
    OGRCleanupAll();
    return 1;
}



