/******************************************************************************
 * $Id: gdal_contour.cpp 21191 2010-12-03 20:02:34Z rouault $
 *
 * Project:  Contour Generator
 * Purpose:  Contour Generator mainline.
 * Author:   Frank Warmerdam <warmerdam@pobox.com>
 *
 ******************************************************************************
 * Copyright (c) 2003, Applied Coherent Technology (www.actgate.com).
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 ****************************************************************************/
#include "stdafx.h"
#include "gdal.h"
#include "gdal_alg.h"
#include "cpl_conv.h"
#include "cpl_string.h"
#include "ogr_api.h"
#include "ogr_srs_api.h"

CPL_CVSID("$Id: gdal_contour.cpp 21191 2010-12-03 20:02:34Z rouault $");

/*
 返回值：
 1，正常执行；-1，gdal版本过低；-2，等高距设置有误；-3，输入文件名有误；-4 ，输出文件名有误
 参数1：等高距
 参数2：DEM文件路径
 参数3：输出的shape文件路径
 参数4：是否自定义Nodata
 参数5：如果CNodata设置为true，则nodata 的值在计算时被当做无效值对待
*/
int ComeputeContour(double Hinterval,  char* srcfilename,  char* dstfilename ,bool CNodata = FALSE, double nodata = 0.0 ,char* pszElevAttrib = "elev";)
{
    GDALDatasetH	hSrcDS;
    int b3D = FALSE, bIgnoreNoData = FALSE;
    int bNoDataSet = CNodata;
    int nBandIn = 1;
    double dfNoData = nodata;
    double dfInterval = 0.0, dfOffset = 0.0;
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

    pszElevAttrib = attrib;
    dfInterval = Hinterval;
    if( dfInterval == 0.0 && nFixedLevelCount == 0 )
    {
        // 等高距为零
        return -2;
    }
    pszSrcFilename = srcfilename;
    if (pszSrcFilename == NULL)
    {
        //  源文件输入有误
        return -3;
    }
    pszDstFilename = dstfilename;
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
        // 目的文件输入有误
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



