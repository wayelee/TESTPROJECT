/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlRasterMosaic.cpp
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 块拼接源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#include "mlRasterMosaic.h"
#include "mlGeoRaster.h"
#include "mlGdalDataset.h"
#include "mlTypeConvert.h"
#include <vector>
//#include "opencv.hpp"
#include "Panorama.h"

///int CmlRasterMosaic::mlDEMMosaic(vector<char*> vecInputFiles, char* cOutputFile, double dXResl, double dYResl, int nResampleAlg, int nDisCultLine)

    /**
    * @fn mlDEMMosaic
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief DEM拼接函数，利用输入文件的地理信息
    * @param vecInputFiles 输入DEM
    * @param cOutputFile 输出DEM
    * @param dXResl X方向分辨率
    * @param dYResl Y方向分辨率
    * @param nResampleAlg  采样方法对应的值
    * @param nDisCultLine  裁切线值
    * @retval 1 成功
    * @retval 0 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
int CmlRasterMosaic::mlDEMMosaic(vector<string> vecInputFiles, const SCHAR* cOutputFile, DOUBLE dXResl, DOUBLE dYResl, SINT nResampleAlg, SINT nDisCultLine)
{

    m_dMinX=0.0;
    m_dMinY=0.0;
    m_dMaxX=0.0;
    m_dMaxY=0.0;
    m_dXRes=dXResl;
    m_dYRes=dYResl;
    bTargetAlignedPixels = FALSE;
    nForcePixels=0;
    nForceLines=0;
    bQuiet = FALSE;
    bEnableDstAlpha = FALSE;
    bEnableSrcAlpha = FALSE;
    bVRT = FALSE;


    cFormat = "GTiff";
//    cInputFiles = NULL;
//    cOutputFile = NULL;
    bCreateOutput = FALSE;
///    bCreateOutput = TRUE;
    hTransformArg = NULL, hGenImgProjArg=NULL, hApproxArg=NULL;
    cWarpOptions = NULL;
    dErrorThreshold = 0.125;
    dWarpMemoryLimit = 0.0;
    pfnTransformer = NULL;
    cCreateOptions = NULL;
    eOutputType = GDT_Unknown;
    eWorkingType = GDT_Unknown;
    eResampleAlg = (GDALResampleAlg)nResampleAlg;
    cSrcNodata = NULL;
    cDstNodata = NULL;
    bMulti = FALSE;
    cTO = NULL;
    pszCutlineDSName = NULL;
    cCLayer = NULL;
    pszCWHERE = NULL;
    pszCSQL = NULL;
    hCutline = NULL;
    bHasGotErr = FALSE;
    bCropToCutline = FALSE;
    bOverwrite = FALSE;

    char** cInputFiles = new char*[vecInputFiles.size()];
    vector<GeoTransformStruct> vecGeoTranStruct;
    vector<OverlapStruct> vecOverlap;

    vector<int> vecBlendPixelX;
    vector<int> vecBlendPixelY;

    for(UINT nInFileNum = 0; nInFileNum<vecInputFiles.size(); nInFileNum++)
    {
        cInputFiles[nInFileNum] = const_cast<char*>(vecInputFiles[nInFileNum].c_str());
    }

    GDALAllRegister();

    CPLPushErrorHandler( CPLQuietErrorHandler );
    hDstDS = GDALOpen( cOutputFile, GA_Update );
    CPLPopErrorHandler();

    if( hDstDS != NULL && bOverwrite )
    {
        GDALClose(hDstDS);
        hDstDS = NULL;
    }

    if( hDstDS != NULL && bCreateOutput )
    {
        cout<<"Error!";
        return -1;
    }


    if ( hDstDS == NULL && !bOverwrite )
    {
        CPLPushErrorHandler( CPLQuietErrorHandler );
        hDstDS = GDALOpen( cOutputFile, GA_ReadOnly );
        CPLPopErrorHandler();

        if (hDstDS)
        {
            cout<<"Dst file Error!";
            GDALClose(hDstDS);
            return -1;
        }
    }


    if( pszCutlineDSName != NULL )
    {
        LoadCutline( pszCutlineDSName, cCLayer, pszCWHERE, pszCSQL, &hCutline );
    }

#ifdef OGR_ENABLED
    if ( bCropToCutline && hCutline != NULL )
    {
        OGRGeometryH hCutlineGeom = OGR_G_Clone( (OGRGeometryH) hCutline );
        OGRSpatialReferenceH hCutlineSRS = OGR_G_GetSpatialReference( hCutlineGeom );
        const char *cThisTargetSRS = CSLFetchNameValue( papszTO, "DST_SRS" );
        OGRCoordinateTransformationH hCT = NULL;
        if (hCutlineSRS == NULL)
        {
            /* We suppose it is in target coordinates */
        }
        else if (cThisTargetSRS != NULL)
        {
            OGRSpatialReferenceH hTargetSRS = OSRNewSpatialReference(NULL);
            if( OSRImportFromWkt( hTargetSRS, (char **)&cThisTargetSRS ) != CE_None )
            {
                fprintf(stderr, "Cannot compute bounding box of cutline.\n");
                return -1;
            }

            hCT = OCTNewCoordinateTransformation(hCutlineSRS, hTargetSRS);

            OSRDestroySpatialReference(hTargetSRS);
        }
        else if (cThisTargetSRS == NULL)
        {
            if (cInputFiles[0] != NULL)
            {
                GDALDatasetH hSrcDS = GDALOpen(cInputFiles[0], GA_ReadOnly);
                if (hSrcDS == NULL)
                {
                    fprintf(stderr, "Cannot compute bounding box of cutline.\n");
                    return -1;
                }

                OGRSpatialReferenceH  hRasterSRS = NULL;
                const char *pszProjection = NULL;

                if( GDALGetProjectionRef( hSrcDS ) != NULL && strlen(GDALGetProjectionRef( hSrcDS )) > 0 )
                {
                    pszProjection = GDALGetProjectionRef( hSrcDS );
                }

                else if( GDALGetGCPProjection( hSrcDS ) != NULL )
                {
                    pszProjection = GDALGetGCPProjection( hSrcDS );
                }


                if( pszProjection == NULL )
                {
                    fprintf(stderr, "Cannot compute bounding box of cutline.\n");
                    return -1;
                }

                hRasterSRS = OSRNewSpatialReference(NULL);
                if( OSRImportFromWkt( hRasterSRS, (char **)&pszProjection ) != CE_None )
                {
                    fprintf(stderr, "Cannot compute bounding box of cutline.\n");
                    return -1;
                }

                hCT = OCTNewCoordinateTransformation(hCutlineSRS, hRasterSRS);

                OSRDestroySpatialReference(hRasterSRS);

                GDALClose(hSrcDS);
            }
            else
            {
                fprintf(stderr, "Cannot compute bounding box of cutline.\n");
                return -1;
            }
        }

        if (hCT)
        {
            OGR_G_Transform( hCutlineGeom, hCT );

            OCTDestroyCoordinateTransformation(hCT);
        }

        OGREnvelope sEnvelope;
        OGR_G_GetEnvelope(hCutlineGeom, &sEnvelope);

        dfMinX = sEnvelope.MinX;
        dfMinY = sEnvelope.MinY;
        dfMaxX = sEnvelope.MaxX;
        dfMaxY = sEnvelope.MaxY;

        OGR_G_DestroyGeometry(hCutlineGeom);
    }
#endif


    int   bInitDestSetForFirst = FALSE;

    if( hDstDS == NULL )
    {
        hDstDS = mlCreateOutputFile( cInputFiles, cOutputFile, cFormat, cTO, &cCreateOptions, eOutputType, vecInputFiles.size());
        bCreateOutput = TRUE;

        if( CSLFetchNameValue( cWarpOptions, "INIT_DEST" ) == NULL && cDstNodata == NULL )
        {
            cWarpOptions = CSLSetNameValue(cWarpOptions, "INIT_DEST", "0");
            bInitDestSetForFirst = TRUE;
        }
        else if( CSLFetchNameValue( cWarpOptions, "INIT_DEST" ) == NULL )
        {
            cWarpOptions = CSLSetNameValue(cWarpOptions, "INIT_DEST", "NO_DATA" );
            bInitDestSetForFirst = TRUE;
        }

        CSLDestroy(cCreateOptions);
        cCreateOptions = NULL;
    }

    if( hDstDS == NULL )
        return -1;

    UINT nSrc;
    OverlapStruct strOverlap;
    //for( nSrc = 0; cInputFiles[nSrc] != NULL; nSrc++ )
    for( nSrc = 0; nSrc<vecInputFiles.size(); nSrc++ )
    {
        GDALDatasetH hSrcDS;
        GeoTransformStruct GeoTranStruct;

        hSrcDS = GDALOpen( cInputFiles[nSrc], GA_ReadOnly );


        if( hSrcDS == NULL )
            return -1;


        if ( GDALGetRasterCount(hSrcDS) == 0 )
        {
            cout<<"输入数据错误"<<endl;
            return -1;

        }

        if( !bQuiet )
            printf( "Processing input file %s.\n", cInputFiles[nSrc] );



        if ( eResampleAlg != GRA_NearestNeighbour && GDALGetRasterColorTable(GDALGetRasterBand(hSrcDS, 1)) != NULL)
        {
            if( !bQuiet )
                fprintf( stderr, "Warning: Input file %s has a color table, which will likely lead to "
                         "bad results when using a resampling method other than "
                         "nearest neighbour. Converting the dataset prior to 24/32 bit "
                         "is advised.\n", cInputFiles[nSrc] );
        }

        //alpha通道
        if( GDALGetRasterColorInterpretation(GDALGetRasterBand(hSrcDS,GDALGetRasterCount(hSrcDS))) == GCI_AlphaBand && !bEnableSrcAlpha )
        {
            bEnableSrcAlpha = TRUE;
            if( !bQuiet )
                printf( "Using band %d of source image as alpha.\n",
                        GDALGetRasterCount(hSrcDS) );
        }

        //坐标转换
        hTransformArg = hGenImgProjArg = GDALCreateGenImgProjTransformer2( hSrcDS, hDstDS, cTO );

        if( hTransformArg == NULL )
            return -1;


        pfnTransformer = GDALGenImgProjTransform;

        if( dErrorThreshold != 0.0 )
        {
            hTransformArg = hApproxArg = GDALCreateApproxTransformer( GDALGenImgProjTransform, hGenImgProjArg, dErrorThreshold);
            pfnTransformer = GDALApproxTransform;
        }

        if( bInitDestSetForFirst && nSrc == 1 )
        {
            cWarpOptions = CSLSetNameValue( cWarpOptions, "INIT_DEST", NULL );
        }


        GDALWarpOptions *pWarpOpt = GDALCreateWarpOptions();

        pWarpOpt->papszWarpOptions = CSLDuplicate(cWarpOptions);
        pWarpOpt->eWorkingDataType = eWorkingType;
        pWarpOpt->eResampleAlg = eResampleAlg;

        pWarpOpt->hSrcDS = hSrcDS;
        pWarpOpt->hDstDS = hDstDS;

        pWarpOpt->pfnTransformer = pfnTransformer;
        pWarpOpt->pTransformerArg = hTransformArg;

        if(!bQuiet)
            pWarpOpt->pfnProgress = GDALTermProgress;

        if(dWarpMemoryLimit != 0.0)
            pWarpOpt->dfWarpMemoryLimit = dWarpMemoryLimit;


        if(bEnableSrcAlpha)
            pWarpOpt->nBandCount = GDALGetRasterCount(hSrcDS) - 1;
        else
            pWarpOpt->nBandCount = GDALGetRasterCount(hSrcDS);

        pWarpOpt->panSrcBands = (int *) CPLMalloc(pWarpOpt->nBandCount*sizeof(int));
        pWarpOpt->panDstBands = (int *) CPLMalloc(pWarpOpt->nBandCount*sizeof(int));

        for( i = 0; i < pWarpOpt->nBandCount; i++ )
        {
            pWarpOpt->panSrcBands[i] = i+1;
            pWarpOpt->panDstBands[i] = i+1;
        }


        if( bEnableSrcAlpha )
            pWarpOpt->nSrcAlphaBand = GDALGetRasterCount(hSrcDS);

        if( !bEnableDstAlpha
                && GDALGetRasterCount(hDstDS) == pWarpOpt->nBandCount+1
                && GDALGetRasterColorInterpretation(
                    GDALGetRasterBand(hDstDS,GDALGetRasterCount(hDstDS)))
                == GCI_AlphaBand )
        {
            if( !bQuiet )
                printf( "Using band %d of destination image as alpha.\n",
                        GDALGetRasterCount(hDstDS) );

            bEnableDstAlpha = TRUE;
        }

        if( bEnableDstAlpha )
            pWarpOpt->nDstAlphaBand = GDALGetRasterCount(hDstDS);


        if( cSrcNodata != NULL && !EQUALN(cSrcNodata,"n",1) )
        {
            char **papszTokens = CSLTokenizeString( cSrcNodata );
            int  nTokenCount = CSLCount(papszTokens);

            pWarpOpt->padfSrcNoDataReal = (double *)CPLMalloc(pWarpOpt->nBandCount*sizeof(double));
            pWarpOpt->padfSrcNoDataImag = (double *)CPLMalloc(pWarpOpt->nBandCount*sizeof(double));

            for( i = 0; i < pWarpOpt->nBandCount; i++ )
            {
                if( i < nTokenCount )
                {
                    CPLStringToComplex( papszTokens[i], pWarpOpt->padfSrcNoDataReal + i, pWarpOpt->padfSrcNoDataImag + i );
                }
                else
                {
                    pWarpOpt->padfSrcNoDataReal[i] = pWarpOpt->padfSrcNoDataReal[i-1];
                    pWarpOpt->padfSrcNoDataImag[i] = pWarpOpt->padfSrcNoDataImag[i-1];
                }
            }

            CSLDestroy( papszTokens );

            pWarpOpt->papszWarpOptions = CSLSetNameValue(pWarpOpt->papszWarpOptions,
                                         "UNIFIED_SRC_NODATA", "YES" );
        }


        if( cSrcNodata == NULL )
        {
            int bHaveNodata = FALSE;
            double dfReal = 0.0;

            for( i = 0; !bHaveNodata && i < pWarpOpt->nBandCount; i++ )
            {
                GDALRasterBandH hBand = GDALGetRasterBand( hSrcDS, i+1 );
                dfReal = GDALGetRasterNoDataValue( hBand, &bHaveNodata );
            }

            if( bHaveNodata )
            {
                if( !bQuiet )
                {
                    if (CPLIsNan(dfReal))
                        printf( "Using internal nodata values (eg. nan) for image %s.\n",
                                cInputFiles[nSrc] );
                    else
                        printf( "Using internal nodata values (eg. %g) for image %s.\n",
                                dfReal, cInputFiles[nSrc] );
                }
                pWarpOpt->padfSrcNoDataReal = (double *)CPLMalloc(pWarpOpt->nBandCount*sizeof(double));
                pWarpOpt->padfSrcNoDataImag = (double *)CPLMalloc(pWarpOpt->nBandCount*sizeof(double));

                for( i = 0; i < pWarpOpt->nBandCount; i++ )
                {
                    GDALRasterBandH hBand = GDALGetRasterBand( hSrcDS, i+1 );

                    dfReal = GDALGetRasterNoDataValue( hBand, &bHaveNodata );

                    if( bHaveNodata )
                    {
                        pWarpOpt->padfSrcNoDataReal[i] = dfReal;
                        pWarpOpt->padfSrcNoDataImag[i] = 0.0;
                    }
                    else
                    {
                        pWarpOpt->padfSrcNoDataReal[i] = -123456.789;
                        pWarpOpt->padfSrcNoDataImag[i] = 0.0;
                    }
                }
            }
        }


        if( cDstNodata != NULL )
        {
            char **papszTokens = CSLTokenizeString(cDstNodata);
            int  nTokenCount = CSLCount(papszTokens);

            pWarpOpt->padfDstNoDataReal = (double *)CPLMalloc(pWarpOpt->nBandCount*sizeof(double));
            pWarpOpt->padfDstNoDataImag = (double *)CPLMalloc(pWarpOpt->nBandCount*sizeof(double));

            for( i = 0; i < pWarpOpt->nBandCount; i++ )
            {
                if( i < nTokenCount )
                {
                    CPLStringToComplex( papszTokens[i], pWarpOpt->padfDstNoDataReal + i, pWarpOpt->padfDstNoDataImag + i );
                }
                else
                {
                    pWarpOpt->padfDstNoDataReal[i] = pWarpOpt->padfDstNoDataReal[i-1];
                    pWarpOpt->padfDstNoDataImag[i] = pWarpOpt->padfDstNoDataImag[i-1];
                }

                GDALRasterBandH hBand = GDALGetRasterBand( hDstDS, i+1 );
                int bClamped = FALSE, bRounded = FALSE;

#define CLAMP(val,type,minval,maxval) \
    do { if (val < minval) { bClamped = TRUE; val = minval; } \
    else if (val > maxval) { bClamped = TRUE; val = maxval; } \
    else if (val != (type)val) { bRounded = TRUE; val = (type)(val + 0.5); } } \
    while(0)

                switch(GDALGetRasterDataType(hBand))
                {
                case GDT_Byte:
                    CLAMP(pWarpOpt->padfDstNoDataReal[i], GByte,
                          0.0, 255.0);
                    break;
                case GDT_Int16:
                    CLAMP(pWarpOpt->padfDstNoDataReal[i], GInt16,
                          -32768.0, 32767.0);
                    break;
                case GDT_UInt16:
                    CLAMP(pWarpOpt->padfDstNoDataReal[i], GUInt16,
                          0.0, 65535.0);
                    break;
                case GDT_Int32:
                    CLAMP(pWarpOpt->padfDstNoDataReal[i], GInt32,
                          -2147483648.0, 2147483647.0);
                    break;
                case GDT_UInt32:
                    CLAMP(pWarpOpt->padfDstNoDataReal[i], GUInt32,
                          0.0, 4294967295.0);
                    break;
                default:
                    break;
                }

                if (bClamped)
                {
                    printf( "for band %d, destination nodata value has been clamped "
                            "to %.0f, the original value being out of range.\n",
                            i + 1, pWarpOpt->padfDstNoDataReal[i]);
                }
                else if(bRounded)
                {
                    printf("for band %d, destination nodata value has been rounded "
                           "to %.0f, %s being an integer datatype.\n",
                           i + 1, pWarpOpt->padfDstNoDataReal[i],
                           GDALGetDataTypeName(GDALGetRasterDataType(hBand)));
                }

                if( bCreateOutput )
                {
                    GDALSetRasterNoDataValue( GDALGetRasterBand( hDstDS, pWarpOpt->panDstBands[i] ), pWarpOpt->padfDstNoDataReal[i] );
                }
            }

            CSLDestroy( papszTokens );
        }


        if( hCutline != NULL )
        {
            TransformCutlineToSource( hSrcDS, hCutline, &(pWarpOpt->papszWarpOptions), cTO );
        }


        if( bVRT )
        {
            if( GDALInitializeWarpedVRT( hDstDS, pWarpOpt ) != CE_None )
                return -1;

            GDALClose( hDstDS );
            GDALClose( hSrcDS );


            if (pfnTransformer == GDALApproxTransform)
            {
                if( hGenImgProjArg != NULL )
                    GDALDestroyGenImgProjTransformer( hGenImgProjArg );
            }

            GDALDestroyWarpOptions( pWarpOpt );

//            CPLFree( cOutputFile );
////////            CSLDestroy( argv );
            CSLDestroy( cInputFiles );
            CSLDestroy( cWarpOptions );
            CSLDestroy( cTO );

            GDALDumpOpenDatasets( stderr );

            GDALDestroyDriverManager();

            return 0;
        }


        GDALWarpOperation WarpOper;

        if( WarpOper.Initialize( pWarpOpt ) == CE_None )
        {
            CPLErr eErr;
            if( bMulti )
                eErr = WarpOper.ChunkAndWarpMulti( 0, 0, GDALGetRasterXSize( hDstDS ), GDALGetRasterYSize( hDstDS ) );
            else
                eErr = WarpOper.ChunkAndWarpImage( 0, 0, GDALGetRasterXSize( hDstDS ), GDALGetRasterYSize( hDstDS ) );
            if (eErr != CE_None)
                bHasGotErr = TRUE;
        }



        double dInputFileGeoTransform[6];
        GDALGetGeoTransform(hSrcDS, dInputFileGeoTransform);

        GeoTranStruct.cFileName = cInputFiles[nSrc];
        GeoTranStruct.nID = nSrc;
        GeoTranStruct.dTopLeftX = dInputFileGeoTransform[0];
        GeoTranStruct.dTopLeftY = dInputFileGeoTransform[3];
        GeoTranStruct.dXRes = dInputFileGeoTransform[1];
        GeoTranStruct.dYRes = dInputFileGeoTransform[5];
        GeoTranStruct.nXSize = GDALGetRasterXSize(hSrcDS);
        GeoTranStruct.nYSize = GDALGetRasterYSize(hSrcDS);
        GeoTranStruct.dLowRightX = GeoTranStruct.dTopLeftX + GeoTranStruct.nXSize*GeoTranStruct.dXRes;
        GeoTranStruct.dLowRightY = GeoTranStruct.dTopLeftY+ GeoTranStruct.nYSize*GeoTranStruct.dYRes;

        vecGeoTranStruct.push_back(GeoTranStruct);



        /* -------------------------------------------------------------------- */
        if( hApproxArg != NULL )
            GDALDestroyApproxTransformer( hApproxArg );

        if( hGenImgProjArg != NULL )
            GDALDestroyGenImgProjTransformer( hGenImgProjArg );

        GDALDestroyWarpOptions( pWarpOpt );

        GDALClose( hSrcDS );
    }


    /* -------------------------------------------------------------------- */

//////////    for(int nInputFileNo = 0; nInputFileNo<vecGeoTranStruct.size(); nInputFileNo++)
//////////    {
//////////        for(int nInputFileNoN = 0; nInputFileNoN<vecGeoTranStruct.size(); nInputFileNoN++)
//////////        {
//////////            if(nInputFileNoN>nInputFileNo)
//////////            {
//////////                if(vecGeoTranStruct[nInputFileNoN].dTopLeftX<vecGeoTranStruct[nInputFileNo].dLowRightX && vecGeoTranStruct[nInputFileNo].dLowRightX<vecGeoTranStruct[nInputFileNoN].dLowRightX
//////////                        && abs(vecGeoTranStruct[nInputFileNoN].dTopLeftY)<abs(vecGeoTranStruct[nInputFileNo].dLowRightY) && abs(vecGeoTranStruct[nInputFileNo].dLowRightY)<abs(vecGeoTranStruct[nInputFileNoN].dLowRightY))
//////////                {
//////////                    strOverlap.dTopLeftX = vecGeoTranStruct[nInputFileNoN].dTopLeftX;
//////////                    strOverlap.dTopLeftY = vecGeoTranStruct[nInputFileNoN].dTopLeftY;
//////////                    strOverlap.dLowRightX = vecGeoTranStruct[nInputFileNo].dLowRightX;
//////////                    strOverlap.dLowRightY = vecGeoTranStruct[nInputFileNo].dLowRightY;
//////////                    strOverlap.pirID.first = nInputFileNo;
//////////                    strOverlap.pirID.second = nInputFileNoN;
//////////
//////////                }
//////////                else if(vecGeoTranStruct[nInputFileNoN].dTopLeftX<vecGeoTranStruct[nInputFileNo].dTopLeftX&&vecGeoTranStruct[nInputFileNo].dTopLeftX<vecGeoTranStruct[nInputFileNoN].dLowRightX
//////////                        && abs(vecGeoTranStruct[nInputFileNoN].dTopLeftY)<abs(vecGeoTranStruct[nInputFileNo].dLowRightY)&&abs(vecGeoTranStruct[nInputFileNo].dLowRightY)<abs(vecGeoTranStruct[nInputFileNoN].dLowRightY))
//////////                {
//////////                    strOverlap.dTopLeftX = vecGeoTranStruct[nInputFileNo].dTopLeftX;
//////////                    strOverlap.dTopLeftY = vecGeoTranStruct[nInputFileNoN].dTopLeftY;
//////////                    strOverlap.dLowRightX = vecGeoTranStruct[nInputFileNoN].dLowRightX;
//////////                    strOverlap.dLowRightY = vecGeoTranStruct[nInputFileNo].dLowRightY;
//////////                    strOverlap.pirID.first = nInputFileNo;
//////////                    strOverlap.pirID.second = nInputFileNoN;
//////////
//////////                }
//////////                else if(vecGeoTranStruct[nInputFileNoN].dTopLeftX<vecGeoTranStruct[nInputFileNo].dTopLeftX && vecGeoTranStruct[nInputFileNo].dLowRightX<vecGeoTranStruct[nInputFileNoN].dLowRightX
//////////                        && abs(vecGeoTranStruct[nInputFileNoN].dTopLeftY)<abs(vecGeoTranStruct[nInputFileNo].dLowRightY)&&abs(vecGeoTranStruct[nInputFileNo].dLowRightY)<abs(vecGeoTranStruct[nInputFileNoN].dLowRightY))
//////////                {
//////////                    strOverlap.dTopLeftX = vecGeoTranStruct[nInputFileNo].dTopLeftX;
//////////                    strOverlap.dTopLeftY = vecGeoTranStruct[nInputFileNoN].dTopLeftY;
//////////                    strOverlap.dLowRightX = vecGeoTranStruct[nInputFileNo].dLowRightX;
//////////                    strOverlap.dLowRightY = vecGeoTranStruct[nInputFileNo].dLowRightY;
//////////                    strOverlap.pirID.first = nInputFileNo;
//////////                    strOverlap.pirID.second = nInputFileNoN;
//////////                }
//////////                else if(vecGeoTranStruct[nInputFileNoN].dTopLeftX>vecGeoTranStruct[nInputFileNo].dTopLeftX && vecGeoTranStruct[nInputFileNo].dLowRightX>vecGeoTranStruct[nInputFileNoN].dLowRightX
//////////                        && abs(vecGeoTranStruct[nInputFileNoN].dTopLeftY)<abs(vecGeoTranStruct[nInputFileNo].dLowRightY) && abs(vecGeoTranStruct[nInputFileNo].dLowRightY)<abs(vecGeoTranStruct[nInputFileNoN].dLowRightY))
//////////                {
//////////                    strOverlap.dTopLeftX = vecGeoTranStruct[nInputFileNoN].dTopLeftX;
//////////                    strOverlap.dTopLeftY = vecGeoTranStruct[nInputFileNoN].dTopLeftY;
//////////                    strOverlap.dLowRightX = vecGeoTranStruct[nInputFileNoN].dLowRightX;
//////////                    strOverlap.dLowRightY = vecGeoTranStruct[nInputFileNo].dLowRightY;
//////////                    strOverlap.pirID.first = nInputFileNo;
//////////                    strOverlap.pirID.second = nInputFileNoN;
//////////                }
//////////                else
//////////                {
//////////                    return -1;
//////////                }
//////////                vecOverlap.push_back(strOverlap);
//////////            }
//////////
//////////        }
//////////    }
//////////
/////////////////////////////////////////////////
//////////    double dOverlapTopLeftX;
//////////    double dOverlapTopLeftY;
//////////    double dOverlapLowRightX;
//////////    double dOverlapLowRightY;
//////////    for(int nOpNum = 0; nOpNum < vecOverlap.size()-1; nOpNum++)
//////////    {
//////////        dOverlapTopLeftX = MIN(vecOverlap[nOpNum].dTopLeftX, vecOverlap[nOpNum+1].dTopLeftX);
//////////        dOverlapTopLeftY = MIN(vecOverlap[nOpNum].dTopLeftY, vecOverlap[nOpNum+1].dTopLeftY);
//////////        dOverlapLowRightX = MAX(vecOverlap[nOpNum].dLowRightX, vecOverlap[nOpNum+1].dLowRightX);
//////////        dOverlapLowRightY = MAX(vecOverlap[nOpNum].dLowRightY, vecOverlap[nOpNum+1].dLowRightY);
//////////    }
/////////////////////////////////////////////////
//////////
//////////    int nOutXSize = GDALGetRasterXSize(hDstDS);
//////////    int nOutYSize = GDALGetRasterYSize(hDstDS);
//////////    double dOutFileGeoTransform[6];
//////////
//////////    GDALGetGeoTransform(hDstDS, dOutFileGeoTransform);
//////////    double dOutXOrigin = dOutFileGeoTransform[0];
//////////    double dOutYOrigin = dOutFileGeoTransform[3];
//////////    double dOutXRes = dOutFileGeoTransform[1];
//////////    double dOutYRes = dOutFileGeoTransform[5];
//////////    double dOutXGeoCoord;
//////////    double dOutYGeoCoord;
//////////
//////////    GDALRasterBandH hBand;
//////////    double *pafScanline;
//////////
//////////    hBand = GDALGetRasterBand(hDstDS, 1);
//////////    //int nXSize = GDALGetRasterBandXSize( hBand );
//////////
//////////    pafScanline = (double *)CPLMalloc(sizeof(double)*nOutXSize);
//////////
//////////
//////////    for(int nYS = 0; nYS < GDALGetRasterYSize(hDstDS); nYS++)
//////////    {
//////////        GDALRasterIO(hBand, GF_Read, 0, nYS, nOutXSize, 1, pafScanline, nOutXSize, 1, GDT_Float64, 0, 0);
//////////        dOutYGeoCoord = dOutYOrigin + nYS*dOutYRes;
//////////        for(int nXS = 0; nXS < GDALGetRasterXSize(hDstDS); nXS++)
//////////        {
//////////            dOutXGeoCoord = dOutXOrigin + nXS*dOutXRes;
//////////
//////////        }
//////////    }
//////////    double dBlendPixelX;
//////////    double dBlendPixelY;
//////////    int nBlendPixelX;
//////////    int nBlendPixelY;
//////////    float *dSrcPiexlA;
//////////    float *dSrcPiexlB;
//////////    dSrcPiexlA = (float *)CPLMalloc(sizeof(float)*1);
//////////    dSrcPiexlB = (float *)CPLMalloc(sizeof(float)*1);
//////////    //vector<int> vecBlendPixelX;
//////////    //vector<int> vecBlendPixelY;
//////////    for(int nOutFileX = 0; nOutFileX < GDALGetRasterXSize(hDstDS); nOutFileX++)
//////////    {
//////////        dOutXGeoCoord = dOutXOrigin + nOutFileX*dOutXRes;
//////////        for(int i = 0; i < vecInputFiles.size(); i++)
//////////        {
//////////            if(dOutXGeoCoord >=vecOverlap[i].dTopLeftX && dOutXGeoCoord <=vecOverlap[i].dLowRightX)
//////////            {
//////////                int nOverlapA = vecOverlap[i].pirID.first;
//////////                int nOverlapB = vecOverlap[i].pirID.second;
//////////                for(int nOutFileY = 0; nOutFileY < GDALGetRasterYSize(hDstDS); nOutFileY++)
//////////                {
//////////                    dOutYGeoCoord = dOutYOrigin + nOutFileY*dOutYRes;
//////////                    if(abs(dOutYGeoCoord) >= abs(vecOverlap[i].dTopLeftY) && abs(dOutYGeoCoord) <= abs(vecOverlap[i].dLowRightY))
//////////                    {
//////////                        dBlendPixelX = (dOutXGeoCoord - vecGeoTranStruct[i].dTopLeftX) / vecGeoTranStruct[i].dXRes;
//////////                        dBlendPixelY = (dOutYGeoCoord - vecGeoTranStruct[i].dTopLeftY) / vecGeoTranStruct[i].dYRes;
//////////                        nBlendPixelX = floor(dBlendPixelX);
//////////                        nBlendPixelY = floor(dBlendPixelY);
//////////
//////////                        GDALDatasetH hSrcA = GDALOpen(vecInputFiles[nOverlapA], GA_ReadOnly);
//////////                        GDALDatasetH hSrcB = GDALOpen(vecInputFiles[nOverlapB], GA_ReadOnly);
//////////                        GDALRasterBandH hSrcBandA;
//////////                        GDALRasterBandH hSrcBandB;
//////////                        hSrcBandA = GDALGetRasterBand(hSrcA, 1);
//////////                        hSrcBandB = GDALGetRasterBand(hSrcB, 1);
//////////
//////////////                        double *dSrcPiexlA;
//////////////                        double *dSrcPiexlB;
//////////
//////////////                        dSrcPiexlA = (double *)CPLMalloc(sizeof(double)*1);
//////////////                        dSrcPiexlB = (double *)CPLMalloc(sizeof(double)*1);
//////////                        GDALRasterIO( hSrcBandA, GF_Read, nBlendPixelX, nBlendPixelY, 1, 1, dSrcPiexlA, 1, 1, GDT_Float32, 0, 0 );
//////////                        GDALRasterIO( hSrcBandB, GF_Read, nBlendPixelX, nBlendPixelY, 1, 1, dSrcPiexlB, 1, 1, GDT_Float32, 0, 0 );
//////////                        float dBlendPixel = ((*dSrcPiexlA) + (*dSrcPiexlB))/2;
//////////
//////////                        if(dOutXGeoCoord!=0)
//////////                        {
//////////
//////////                        }
//////////
//////////                        //vecBlendPixelX.push_back(nBlendPixelX);
//////////
//////////                        //vecBlendPixelY.push_back(nBlendPixelY);
//////////                        //dOutXGeoCoord,dOutYGeoCoord
//////////                    }
//////////
//////////
//////////
//////////                }
//////////            }
//////////        }
//////////        //if(dOutXGeoCoord <=vecOverlap[] )
//////////
//////////    }
//////////
//////////    for(int i = 0; i < vecInputFiles.size(); i++)
//////////    {
//////////
//////////    }



    CPLErrorReset();
    GDALFlushCache( hDstDS );
    if( CPLGetLastErrorType() != CE_None )
        bHasGotErr = TRUE;
    GDALClose( hDstDS );


    CSLDestroy( cWarpOptions );
    CSLDestroy( cTO );

    GDALDumpOpenDatasets( stderr );

    GDALDestroyDriverManager();

#ifdef OGR_ENABLED
    if( hCutline != NULL )
        OGR_G_DestroyGeometry( (OGRGeometryH) hCutline );
    OGRCleanupAll();
#endif

    return (bHasGotErr) ? 1 : 0;
}

/**
* @fn mlInitCoordSys
* @date 2011.11.22
* @author 梁健 liangjian@irsa.ac.cn
* @brief 初始化输入DEM坐标
* @param cInputCoordSys 输入坐标系统
* @return 坐标系统标识
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
char* CmlRasterMosaic::mlInitCoordSys(const char *cInputCoordSys )
{
    OGRSpatialReferenceH hSRS;
    char *pszResult = NULL;

    CPLErrorReset();

    hSRS = OSRNewSpatialReference( NULL );
    if( OSRSetFromUserInput( hSRS, cInputCoordSys ) == OGRERR_NONE )
        OSRExportToWkt( hSRS, &pszResult );
    else
    {
        CPLError( CE_Failure, CPLE_AppDefined,
                  "Translating source or target SRS failed:\n%s",
                  cInputCoordSys );
        return NULL;
    }

    OSRDestroySpatialReference( hSRS );

    return pszResult;
}

/**
* @fn mlCreateOutputFile
* @date 2011.11.22
* @author 梁健 liangjian@irsa.ac.cn
* @brief 创建输出文件
* @param cSrcFiles 输入源文件
* @param cFilename 输出文件
* @param cFormat 输出图像格式
* @param papszTO 输出文件坐标系
* @param cCreateOptions 创建图像选项
* @param eDT 波段每个像素的字节
* @param nInputFileNum 输入文件数量
* @retval GDALDatasetH 返回
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
GDALDatasetH CmlRasterMosaic::mlCreateOutputFile(char **cInputFiles, const char *cOutputFile, const char *cOutputFileFormat, char **papszTO, char ***ppapszCreateOptions, GDALDataType eDT, int nInputFileNum)
{
    GDALDriverH hDriver;
    GDALDatasetH hDstDS;
    void *hTransformArg;
    GDALColorTableH hCT = NULL;
    double dfWrkMinX=0, dfWrkMaxX=0, dfWrkMinY=0, dfWrkMaxY=0;
    double dfWrkResX=0, dfWrkResY=0;
    int nDstBandCount = 0;

    hDriver = GDALGetDriverByName( cOutputFileFormat );
    if( hDriver == NULL || GDALGetMetadataItem(hDriver, GDAL_DCAP_CREATE, NULL) == NULL )
    {
        int	iDr;

        printf( "Out put Failed!/n", cOutputFileFormat );

        for( iDr = 0; iDr < GDALGetDriverCount(); iDr++ )
        {
            GDALDriverH hDriver = GDALGetDriver(iDr);

            if( GDALGetMetadataItem( hDriver, GDAL_DCAP_CREATE, NULL) != NULL )
            {
                printf( "  %s: %s\n", GDALGetDriverShortName(hDriver), GDALGetDriverLongName(hDriver));
            }
        }
        printf( "\n" );
        return NULL;
    }


    if( bVRT )
    {
        *ppapszCreateOptions = CSLSetNameValue( *ppapszCreateOptions, "SUBCLASS", "VRTWarpedDataset" );
    }


    int nSrc;
    char *cThisTargetSRS = (char*)CSLFetchNameValue( papszTO, "DST_SRS" );
    if( cThisTargetSRS != NULL )
        cThisTargetSRS = CPLStrdup( cThisTargetSRS );


    for( nSrc = 0; nSrc<nInputFileNum; nSrc++ )
    {
        GDALDatasetH hSrcDS;
        const char *pszThisSourceSRS = CSLFetchNameValue(papszTO,"SRC_SRS");

        hSrcDS = GDALOpen( cInputFiles[nSrc], GA_ReadOnly );
        if( hSrcDS == NULL )
            return NULL;

        /*!
            检查Raster数据正确性
        */
        if ( GDALGetRasterCount(hSrcDS) == 0 )
        {
            fprintf(stderr, "Input file %s has no raster bands.\n", cInputFiles[nSrc] );
            return NULL;
        }

        if( eDT == GDT_Unknown )
            eDT = GDALGetRasterDataType(GDALGetRasterBand(hSrcDS,1));

        /*!
            采用第一个输入文件的color table作为全局的color table
        */
        if( nSrc == 0 )
        {
            nDstBandCount = GDALGetRasterCount(hSrcDS);
            hCT = GDALGetRasterColorTable( GDALGetRasterBand(hSrcDS,1) );
            if( hCT != NULL )
            {
                hCT = GDALCloneColorTable( hCT );
                if( !bQuiet )
                    printf( "Copying color table from %s to new file.\n",
                            cInputFiles[nSrc] );
            }
        }
		delete[] cInputFiles;
        /*!
            获取输入文件的坐标信息
        */
        if( pszThisSourceSRS == NULL )
        {
            const char *pszMethod = CSLFetchNameValue( papszTO, "METHOD" );

            if( GDALGetProjectionRef( hSrcDS ) != NULL
                    && strlen(GDALGetProjectionRef( hSrcDS )) > 0
                    && (pszMethod == NULL || EQUAL(pszMethod,"GEOTRANSFORM")) )
                pszThisSourceSRS = GDALGetProjectionRef( hSrcDS );

            else if( GDALGetGCPProjection( hSrcDS ) != NULL
                     && strlen(GDALGetGCPProjection(hSrcDS)) > 0
                     && GDALGetGCPCount( hSrcDS ) > 1
                     && (pszMethod == NULL || EQUALN(pszMethod,"GCP_",4)) )
                pszThisSourceSRS = GDALGetGCPProjection( hSrcDS );
            else if( pszMethod != NULL && EQUAL(pszMethod,"RPC") )
                pszThisSourceSRS = SRS_WKT_WGS84;
            else
                pszThisSourceSRS = "";
        }

        if( cThisTargetSRS == NULL )
            cThisTargetSRS = CPLStrdup( pszThisSourceSRS );

        /*!
            将输入文件的坐标系统转换为输出文件的坐标系统
        */
        hTransformArg = GDALCreateGenImgProjTransformer2( hSrcDS, NULL, papszTO );

        if( hTransformArg == NULL )
        {
            CPLFree( cThisTargetSRS );
            GDALClose( hSrcDS );
            return NULL;
        }

        /*!
            设置输出文件参数
        */

        double adfThisGeoTransform[6];
        double adfExtent[4];
        int    nThisPixels, nThisLines;

        if( GDALSuggestedWarpOutput2( hSrcDS,
                                      GDALGenImgProjTransform, hTransformArg,
                                      adfThisGeoTransform,
                                      &nThisPixels, &nThisLines,
                                      adfExtent, 0 ) != CE_None )
        {
            CPLFree( cThisTargetSRS );
            GDALClose( hSrcDS );
            return NULL;
        }

        if (CPLGetConfigOption( "CHECK_WITH_INVERT_PROJ", NULL ) == NULL)
        {
            double MinX = adfExtent[0];
            double MaxX = adfExtent[2];
            double MaxY = adfExtent[3];
            double MinY = adfExtent[1];
            int bSuccess = TRUE;



            int nSteps = 20;
            int i,j;
            for(i=0; i<=nSteps && bSuccess; i++)
            {
                for(j=0; j<=nSteps && bSuccess; j++)
                {
                    double dfRatioI = i * 1.0 / nSteps;
                    double dfRatioJ = j * 1.0 / nSteps;
                    double expected_x = (1 - dfRatioI) * MinX + dfRatioI * MaxX;
                    double expected_y = (1 - dfRatioJ) * MinY + dfRatioJ * MaxY;
                    double x = expected_x;
                    double y = expected_y;
                    double z = 0;

                    if (!GDALGenImgProjTransform(hTransformArg, TRUE, 1, &x, &y, &z, &bSuccess) || !bSuccess)
                    {
                        bSuccess = FALSE;
                    }


                    if (!GDALGenImgProjTransform(hTransformArg, FALSE, 1, &x, &y, &z, &bSuccess) || !bSuccess)
                    {
                        bSuccess = FALSE;
                    }

                    if (fabs(x - expected_x) > (MaxX - MinX) / nThisPixels || fabs(y - expected_y) > (MaxY - MinY) / nThisLines)
                    {
                        bSuccess = FALSE;
                    }

                }
            }


            if (!bSuccess)
            {
                CPLSetConfigOption( "CHECK_WITH_INVERT_PROJ", "TRUE" );
                CPLDebug("WARP", "Recompute out extent with CHECK_WITH_INVERT_PROJ=TRUE");
                GDALDestroyGenImgProjTransformer(hTransformArg);
                hTransformArg =
                    GDALCreateGenImgProjTransformer2( hSrcDS, NULL, papszTO );

                if( GDALSuggestedWarpOutput2( hSrcDS,
                                              GDALGenImgProjTransform, hTransformArg,
                                              adfThisGeoTransform,
                                              &nThisPixels, &nThisLines,
                                              adfExtent, 0 ) != CE_None )
                {
                    CPLFree( cThisTargetSRS );
                    GDALClose( hSrcDS );
                    return NULL;
                }
            }
        }


        if( dfWrkMaxX == 0.0 && dfWrkMinX == 0.0 )
        {
            dfWrkMinX = adfExtent[0];
            dfWrkMaxX = adfExtent[2];
            dfWrkMaxY = adfExtent[3];
            dfWrkMinY = adfExtent[1];
            dfWrkResX = adfThisGeoTransform[1];
            dfWrkResY = ABS(adfThisGeoTransform[5]);
        }
        else
        {
            dfWrkMinX = MIN(dfWrkMinX,adfExtent[0]);
            dfWrkMaxX = MAX(dfWrkMaxX,adfExtent[2]);
            dfWrkMaxY = MAX(dfWrkMaxY,adfExtent[3]);
            dfWrkMinY = MIN(dfWrkMinY,adfExtent[1]);
            dfWrkResX = MIN(dfWrkResX,adfThisGeoTransform[1]);
            dfWrkResY = MIN(dfWrkResY,ABS(adfThisGeoTransform[5]));
        }

        GDALDestroyGenImgProjTransformer( hTransformArg );

        GDALClose( hSrcDS );
    }


    if( nDstBandCount == 0 )
    {
        CPLError( CE_Failure, CPLE_AppDefined,
                  "No usable source images." );
        CPLFree( cThisTargetSRS );
        return NULL;
    }


    double adfDstGeoTransform[6];
    int nPixels, nLines;

    adfDstGeoTransform[0] = dfWrkMinX;
    adfDstGeoTransform[1] = dfWrkResX;
    adfDstGeoTransform[2] = 0.0;
    adfDstGeoTransform[3] = dfWrkMaxY;
    adfDstGeoTransform[4] = 0.0;
    adfDstGeoTransform[5] = -1 * dfWrkResY;

    nPixels = (int) ((dfWrkMaxX - dfWrkMinX) / dfWrkResX + 0.5);
    nLines = (int) ((dfWrkMaxY - dfWrkMinY) / dfWrkResY + 0.5);


    if( m_dXRes != 0.0 && m_dYRes != 0.0 )
    {
        if( m_dMinX == 0.0 && m_dMinY == 0.0 && m_dMaxX == 0.0 && m_dMaxY == 0.0 )
        {
            m_dMinX = adfDstGeoTransform[0];
            m_dMaxX = adfDstGeoTransform[0] + adfDstGeoTransform[1] * nPixels;
            m_dMaxY = adfDstGeoTransform[3];
            m_dMinY = adfDstGeoTransform[3] + adfDstGeoTransform[5] * nLines;
        }

        if ( bTargetAlignedPixels )
        {
            m_dMinX = floor(m_dMinX / m_dXRes) * m_dXRes;
            m_dMaxX = ceil(m_dMaxX / m_dXRes) * m_dXRes;
            m_dMinY = floor(m_dMinY / m_dYRes) * m_dYRes;
            m_dMaxY = ceil(m_dMaxY / m_dYRes) * m_dYRes;
        }

        nPixels = (int) ((m_dMaxX - m_dMinX + (m_dXRes/2.0)) / m_dXRes);
        nLines = (int) ((m_dMaxY - m_dMinY + (m_dYRes/2.0)) / m_dYRes);
        adfDstGeoTransform[0] = m_dMinX;
        adfDstGeoTransform[3] = m_dMaxY;
        adfDstGeoTransform[1] = m_dXRes;
        adfDstGeoTransform[5] = -m_dYRes;
    }

    else if( nForcePixels != 0 && nForceLines != 0 )
    {
        if( m_dMinX == 0.0 && m_dMinY == 0.0 && m_dMaxX == 0.0 && m_dMaxY == 0.0 )
        {
            m_dMinX = dfWrkMinX;
            m_dMaxX = dfWrkMaxX;
            m_dMaxY = dfWrkMaxY;
            m_dMinY = dfWrkMinY;
        }

        m_dXRes = (m_dMaxX - m_dMinX) / nForcePixels;
        m_dYRes = (m_dMaxY - m_dMinY) / nForceLines;

        adfDstGeoTransform[0] = m_dMinX;
        adfDstGeoTransform[3] = m_dMaxY;
        adfDstGeoTransform[1] = m_dXRes;
        adfDstGeoTransform[5] = -m_dYRes;

        nPixels = nForcePixels;
        nLines = nForceLines;
    }

    else if( nForcePixels != 0 )
    {
        if( m_dMinX == 0.0 && m_dMinY == 0.0 && m_dMaxX == 0.0 && m_dMaxY == 0.0 )
        {
            m_dMinX = dfWrkMinX;
            m_dMaxX = dfWrkMaxX;
            m_dMaxY = dfWrkMaxY;
            m_dMinY = dfWrkMinY;
        }

        m_dXRes = (m_dMaxX - m_dMinX) / nForcePixels;
        m_dYRes = m_dXRes;

        adfDstGeoTransform[0] = m_dMinX;
        adfDstGeoTransform[3] = m_dMaxY;
        adfDstGeoTransform[1] = m_dXRes;
        adfDstGeoTransform[5] = -m_dYRes;

        nPixels = nForcePixels;
        nLines = (int) ((m_dMaxY - m_dMinY + (m_dYRes/2.0)) / m_dYRes);
    }

    else if( nForceLines != 0 )
    {
        if( m_dMinX == 0.0 && m_dMinY == 0.0 && m_dMaxX == 0.0 && m_dMaxY == 0.0 )
        {
            m_dMinX = dfWrkMinX;
            m_dMaxX = dfWrkMaxX;
            m_dMaxY = dfWrkMaxY;
            m_dMinY = dfWrkMinY;
        }

        m_dYRes = (m_dMaxY - m_dMinY) / nForceLines;
        m_dXRes = m_dYRes;

        adfDstGeoTransform[0] = m_dMinX;
        adfDstGeoTransform[3] = m_dMaxY;
        adfDstGeoTransform[1] = m_dXRes;
        adfDstGeoTransform[5] = -m_dYRes;

        nPixels = (int) ((m_dMaxX - m_dMinX + (m_dXRes/2.0)) / m_dXRes);
        nLines = nForceLines;
    }

    else if( m_dMinX != 0.0 || m_dMinY != 0.0 || m_dMaxX != 0.0 || m_dMaxY != 0.0 )
    {
        m_dXRes = adfDstGeoTransform[1];
        m_dYRes = fabs(adfDstGeoTransform[5]);

        nPixels = (int) ((m_dMaxX - m_dMinX + (m_dXRes/2.0)) / m_dXRes);
        nLines = (int) ((m_dMaxY - m_dMinY + (m_dYRes/2.0)) / m_dYRes);

        m_dXRes = (m_dMaxX - m_dMinX) / nPixels;
        m_dYRes = (m_dMaxY - m_dMinY) / nLines;

        adfDstGeoTransform[0] = m_dMinX;
        adfDstGeoTransform[3] = m_dMaxY;
        adfDstGeoTransform[1] = m_dXRes;
        adfDstGeoTransform[5] = -m_dYRes;
    }


    if( bEnableSrcAlpha )
        nDstBandCount--;

    if( bEnableDstAlpha )
        nDstBandCount++;

    printf( "Creating output file that is %dP x %dL.\n", nPixels, nLines );
    hDstDS = GDALCreate( hDriver, cOutputFile, nPixels, nLines, nDstBandCount, eDT, *ppapszCreateOptions );

    if( hDstDS == NULL )
    {
        CPLFree( cThisTargetSRS );
        return NULL;
    }

    GDALSetProjection( hDstDS, cThisTargetSRS );
    GDALSetGeoTransform( hDstDS, adfDstGeoTransform );


    if( hCT != NULL )
    {
        GDALSetRasterColorTable( GDALGetRasterBand(hDstDS,1), hCT );
        GDALDestroyColorTable( hCT );
    }

    CPLFree( cThisTargetSRS );
    return hDstDS;

}


void CmlRasterMosaic::LoadCutline( const char *pszCutlineDSName, const char *pszCLayer, const char *pszCWHERE, const char *pszCSQL, void **phCutlineRet )
{
#ifndef OGR_ENABLED
    CPLError( CE_Failure, CPLE_AppDefined,
              "Request to load a cutline failed, this build does not support OGR features.\n" );

#else // def OGR_ENABLED
    OGRRegisterAll();


    OGRDataSourceH hSrcDS;

    hSrcDS = OGROpen( pszCutlineDSName, FALSE, NULL );
    if( hSrcDS == NULL )
        return -1;


    OGRLayerH hLayer = NULL;

    if( pszCSQL != NULL )
        hLayer = OGR_DS_ExecuteSQL( hSrcDS, pszCSQL, NULL, NULL );
    else if( pszCLayer != NULL )
        hLayer = OGR_DS_GetLayerByName( hSrcDS, pszCLayer );
    else
        hLayer = OGR_DS_GetLayer( hSrcDS, 0 );

    if( hLayer == NULL )
    {
        fprintf( stderr, "Failed to identify source layer from datasource.\n" );
        return -1;
    }


    if( pszCWHERE != NULL )
        OGR_L_SetAttributeFilter( hLayer, pszCWHERE );


    OGRFeatureH hFeat;
    OGRGeometryH hMultiPolygon = OGR_G_CreateGeometry( wkbMultiPolygon );

    OGR_L_ResetReading( hLayer );

    while( (hFeat = OGR_L_GetNextFeature( hLayer )) != NULL )
    {
        OGRGeometryH hGeom = OGR_F_GetGeometryRef(hFeat);

        if( hGeom == NULL )
        {
            fprintf( stderr, "ERROR: Cutline feature without a geometry.\n" );
            return -1;
        }

        OGRwkbGeometryType eType = wkbFlatten(OGR_G_GetGeometryType( hGeom ));

        if( eType == wkbPolygon )
            OGR_G_AddGeometry( hMultiPolygon, hGeom );
        else if( eType == wkbMultiPolygon )
        {
            int iGeom;

            for( iGeom = 0; iGeom < OGR_G_GetGeometryCount( hGeom ); iGeom++ )
            {
                OGR_G_AddGeometry( hMultiPolygon,
                                   OGR_G_GetGeometryRef(hGeom,iGeom) );
            }
        }
        else
        {
            fprintf( stderr, "ERROR: Cutline not of polygon type.\n" );
            return -1;
        }

        OGR_F_Destroy( hFeat );
    }

    if( OGR_G_GetGeometryCount( hMultiPolygon ) == 0 )
    {
        fprintf( stderr, "ERROR: Did not get any cutline features.\n" );
        return -1;
    }


    OGR_G_AssignSpatialReference(
        hMultiPolygon, OGR_L_GetSpatialRef(hLayer) );

    *phCutlineRet = (void *) hMultiPolygon;


    if( pszCSQL != NULL )
        OGR_DS_ReleaseResultSet( hSrcDS, hLayer );

    OGR_DS_Destroy( hSrcDS );
#endif
}


void CmlRasterMosaic::TransformCutlineToSource( GDALDatasetH hSrcDS, void *hCutline, char ***ppapszWarpOptions, char **papszTO_In )
{
#ifdef OGR_ENABLED
    OGRGeometryH hMultiPolygon = OGR_G_Clone( (OGRGeometryH) hCutline );
    char **papszTO = CSLDuplicate( papszTO_In );


    OGRSpatialReferenceH  hRasterSRS = NULL;
    const char *pszProjection = NULL;

    if( GDALGetProjectionRef( hSrcDS ) != NULL
            && strlen(GDALGetProjectionRef( hSrcDS )) > 0 )
        pszProjection = GDALGetProjectionRef( hSrcDS );
    else if( GDALGetGCPProjection( hSrcDS ) != NULL )
        pszProjection = GDALGetGCPProjection( hSrcDS );

    if( pszProjection != NULL )
    {
        hRasterSRS = OSRNewSpatialReference(NULL);
        if( OSRImportFromWkt( hRasterSRS, (char **)&pszProjection ) != CE_None )
        {
            OSRDestroySpatialReference(hRasterSRS);
            hRasterSRS = NULL;
        }
    }

    OGRSpatialReferenceH hCutlineSRS = OGR_G_GetSpatialReference( hMultiPolygon );
    if( hRasterSRS != NULL && hCutlineSRS != NULL )
    {
        /* ok, we will reproject */
    }
    else if( hRasterSRS != NULL && hCutlineSRS == NULL )
    {
        fprintf(stderr,
                "Warning : the source raster dataset has a SRS, but the cutline features\n"
                "not.  We assume that the cutline coordinates are expressed in the destination SRS.\n"
                "If not, cutline results may be incorrect.\n");
    }
    else if( hRasterSRS == NULL && hCutlineSRS != NULL )
    {
        fprintf(stderr, "Warning : the input vector layer has a SRS, but the source raster dataset does not.\n"
                "Cutline results may be incorrect.\n");
    }

    if( hRasterSRS != NULL )
        OSRDestroySpatialReference(hRasterSRS);


    if( hCutlineSRS != NULL )
    {
        char *pszCutlineSRS_WKT = NULL;

        OSRExportToWkt( hCutlineSRS, &pszCutlineSRS_WKT );
        papszTO = CSLSetNameValue( papszTO, "DST_SRS", pszCutlineSRS_WKT );
        CPLFree( pszCutlineSRS_WKT );
    }


    CutlineTransformer oTransformer;


    oTransformer.hSrcImageTransformer = GDALCreateGenImgProjTransformer2( hSrcDS, NULL, papszTO );

    CSLDestroy( papszTO );

    if( oTransformer.hSrcImageTransformer == NULL )
        return -1;
    ///exit( 1 );

    OGR_G_Transform( hMultiPolygon, (OGRCoordinateTransformationH) &oTransformer );

    GDALDestroyGenImgProjTransformer( oTransformer.hSrcImageTransformer );

    char *pszWKT = NULL;

    OGR_G_ExportToWkt(hMultiPolygon, &pszWKT);
    OGR_G_DestroyGeometry(hMultiPolygon);

    *ppapszWarpOptions = CSLSetNameValue(*ppapszWarpOptions, "CUTLINE", pszWKT);
    CPLFree(pszWKT);
#endif
}

/**
* @fn mlPano2Prespective
* @date 2011.11.22
* @author 梁健 liangjian@irsa.ac.cn
* @brief 正向圆柱投影
* @param cInputPanoFile 输入全景图像
* @param cOutputImage 输出透视图像
* @param ptOriginal 全景图像中待生成透视图像的左上角坐标
* @param nPanoW 全景图像的宽度
* @param nPanoH 全景图像的高度
* @param dFocus 相机焦距
* @retval 无
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlPano2Prespective::mlPano2Prespective( const SCHAR *cInputPanoFile, const SCHAR * cOutputImage, Pt2i ptOrigin, int nPanoRoiW, int nPanoRoiH, double dFocus)
{
    CmlGdalDataset PanoDS;
//    double dRotmatElem;
//    int nOriWidthSum = 0;
//    int nOriPtInNo;
//    double dPanoWScale;
//    CvMat *mRotmat = cvCreateMat(3, 3, CV_64FC1);
//    CvMat *mInvRotmat = cvCreateMat(3, 3, CV_64FC1);

    IplImage *iplPano = cvLoadImage(cInputPanoFile, 0);
    IplImage *iplPanoRoi = cvCreateImage(cvSize(nPanoRoiW, nPanoRoiH), iplPano->depth, iplPano->nChannels);
    cvSetImageROI(iplPano, cvRect(ptOrigin.X, ptOrigin.Y, nPanoRoiW, nPanoRoiH));
    cvCopy(iplPano, iplPanoRoi);
    cvResetImageROI(iplPano);

//    IplImage *iplInvRoi = cvCreateImage(cvSize(iplPanoRoi->width, iplPanoRoi->height), iplPanoRoi->depth, iplPanoRoi->nChannels);

    cv::Mat mPanoRoi(iplPanoRoi);
    cv::Mat mPresDst;
    cv::Mat mAffineMat;

    cv::Point2f ptSrcPano[4];
    cv::Point2f ptDstProj[4];

    ptSrcPano[0].x = 0;
    ptSrcPano[0].y = 0;

    ptSrcPano[1].x = 0;
    ptSrcPano[1].y = nPanoRoiH;

    ptSrcPano[2].x = nPanoRoiW;
    ptSrcPano[2].y = 0;

    ptSrcPano[3].x = nPanoRoiW;
    ptSrcPano[3].y = nPanoRoiH;


    double f = dFocus;

    for(int i = 0; i < 4; i++)
    {
        ptDstProj[i].x = -f*tan((ptSrcPano[i].x - f*atanf(nPanoRoiW/(2*f)))/f) + nPanoRoiW/2;
        ptDstProj[i].y = -(ptSrcPano[i].y - nPanoRoiH/2)/cos((ptSrcPano[i].x - f*atanf(nPanoRoiW/(2*f)))/f) + nPanoRoiH/2;
    }

    float fOffsetXMin = 99999;
    float fOffsetYMin = 99999;
    float fOffsetXMax = -99999;
    float fOffsetYMax = -99999;

    for(int i = 0; i < 4; i++)
    {
        if(ptDstProj[i].x < fOffsetXMin)
        {
            fOffsetXMin = ptDstProj[i].x;
        }
        if(ptDstProj[i].x > fOffsetXMax)
        {
            fOffsetXMax = ptDstProj[i].x;
        }


        if(ptDstProj[i].y < fOffsetXMin)
        {
            fOffsetYMin = ptDstProj[i].y;
        }
        if(ptDstProj[i].y > fOffsetYMax)
        {
            fOffsetYMax = ptDstProj[i].y;
        }

    }

    int nProjW = (int)(fOffsetXMax - fOffsetXMin);
    int nProjH = (int)(fOffsetYMax - fOffsetYMin);

    cv::Mat mPerspectiveMat;
    mPerspectiveMat = getPerspectiveTransform(ptSrcPano, ptDstProj);
    warpPerspective(mPanoRoi, mPresDst, mPerspectiveMat, cv::Size(nProjW, nProjH));

    double dRotAngDeg = 180;
    cv::Point2f ptCenter;

    ptCenter.x = nPanoRoiW/2;
    ptCenter.y = nPanoRoiH/2;

    int nOffWidth = (nProjW - nPanoRoiW)/2;
    int nOffHeigh = (nProjH - nPanoRoiH)/2;

    cv::Mat mFinalDstImg;
    mAffineMat = getRotationMatrix2D(ptCenter, dRotAngDeg, 1);
    mAffineMat.at<double>(0, 2) += nOffWidth;
    mAffineMat.at<double>(1, 2) += nOffHeigh;

    warpAffine(mPresDst, mFinalDstImg, mAffineMat, cv::Size(nProjW, nProjH));
    imwrite(cOutputImage, mFinalDstImg);
    return TRUE;
}

/**
* @fn mlExportMatchPts
* @date 2011.11.22
* @author 梁健 liangjian@irsa.ac.cn
* @brief 输出相邻图像间的匹配点
* @param vecParam 生成全景图像的参数
* @param vecFrmInfo 原始图像信息
* @param vecImgPtSets 输出点信息
* @param strOutPath 输出点文件路径
* @retval 无
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlPanoInterface::mlExportMatchPts(vector<char*> vecParam, vector<FrameImgInfo> vecFrmInfo, vector<ImgPtSet> &vecImgPtSets, char* strOutPath, bool &bNeedAddPts)
{
    bool bVecImgPtSetEmpty = false;
    int nSumPtSize = 0;
    vector<string> vecImagePath;
    vector<double> dExtParam;
    vector<PanoMatchInfo> vecPanoMatchInfo;
    PanoMatchInfo pmInfo;
    for(UINT i = 0; i < vecImgPtSets.size(); i++)
    {
        nSumPtSize = vecImgPtSets[i].vecPts.size() + vecImgPtSets[i+1].vecPts.size();
        vecImgPtSets[i].imgInfo = vecFrmInfo[i];
    }

    if(0 == nSumPtSize)
    {
        bVecImgPtSetEmpty = true;
    }
    else
    {
        bVecImgPtSetEmpty = false;
    }

    for(UINT i = 0; i < vecFrmInfo.size(); i++)
    {
        vecImagePath.push_back(vecFrmInfo[i].strImgPath);

        dExtParam.push_back(vecFrmInfo[i].exOri.pos.X);
        dExtParam.push_back(vecFrmInfo[i].exOri.pos.Y);
        dExtParam.push_back(vecFrmInfo[i].exOri.pos.Z);
        dExtParam.push_back(vecFrmInfo[i].exOri.ori.omg);
        dExtParam.push_back(vecFrmInfo[i].exOri.ori.phi);
        dExtParam.push_back(vecFrmInfo[i].exOri.ori.kap);

        pmInfo.nSrcImgIdx = vecFrmInfo[i].nImgIndex;
        pmInfo.sImgPathSrc = vecFrmInfo[i].strImgPath;
        vecPanoMatchInfo.push_back(pmInfo);//此时vecPanoMatchInfo.size()的大小已经确定，等于原始图像的数量
    }

    PanoMatchPoint(vecParam, vecImagePath, dExtParam, strOutPath, vecPanoMatchInfo, bNeedAddPts);

    for(UINT i = 0; i < vecPanoMatchInfo.size(); i++)
    {
        Pt2d ptExp2d;
        for(UINT m = 0; m < vecImgPtSets.size(); m++)
        {
            if(vecPanoMatchInfo[i].nSrcImgIdx == vecImgPtSets[m].imgInfo.nImgIndex)
            {
                for(UINT j = 0; j < vecPanoMatchInfo[i].vecMatchPtsSrc.size(); j++)
                {
                    ptExp2d.X = vecPanoMatchInfo[i].vecMatchPtsSrc[j].x;
                    ptExp2d.Y = vecPanoMatchInfo[i].vecMatchPtsSrc[j].y;
                    ptExp2d.byType = 1;
                    ptExp2d.lID = vecPanoMatchInfo[i].vecMatchPtsSrc[j].ptIdx;
                    vecImgPtSets[m].vecPts.push_back(ptExp2d);
                }
            }
        }
    }
    vector<int> vecSizeOut;
    int n= 0;
    for(UINT i=0; i < vecImgPtSets.size(); i++)
    {
        n = vecImgPtSets[i].vecPts.size();
        vecSizeOut.push_back(n);
    }

    return true;
}

/**
* @fn mlImportMatchPts
* @date 2011.11.22
* @author 梁健 liangjian@irsa.ac.cn
* @brief 导入相邻图像间的匹配点
* @param vecParam 生成全景图像的参数
* @param vecFrmInfo 原始图像信息
* @param vecImgPtSets 输出点信息
* @param strOutPath 生成全景图像路径
* @retval 无
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlPanoInterface::mlImportMatchPts(vector<char*> vecParam, vector<FrameImgInfo> vecFrmInfo, vector<ImgPtSet> &vecImgPtSets, char* strOutPath)
{
//只处理添加的点，将人工添加的点转为vector<PanoMatchInfo>
//    for(int i = 0; i < vecImgPtSets.size(); i++)
//    {
//        vecImgPtSets[i].vecPts.clear();
//    }

//    bool bVecAddPtEmpty = false;
//    int nSumPtSize = 0;
    vector<string> vecImagePath;
    vector<double> dExtParam;
    vector<PanoMatchInfo> vecPanoMatchPts;
    PanoMatchInfo pmImportInfo;
    MatchPt AddPts;//人工添加点（标识为2）
    MatchPt AutoPts;//自动匹配点（标识为1）
    for(UINT i = 0; i < vecFrmInfo.size(); i++)
    {
        vecImagePath.push_back(vecFrmInfo[i].strImgPath);
        dExtParam.push_back(vecFrmInfo[i].exOri.pos.X);
        dExtParam.push_back(vecFrmInfo[i].exOri.pos.Y);
        dExtParam.push_back(vecFrmInfo[i].exOri.pos.Z);
        dExtParam.push_back(vecFrmInfo[i].exOri.ori.omg);
        dExtParam.push_back(vecFrmInfo[i].exOri.ori.phi);
        dExtParam.push_back(vecFrmInfo[i].exOri.ori.kap);

        pmImportInfo.nSrcImgIdx = 0;//vecFrmInfo[i].nImgIndex;
        pmImportInfo.sImgPathSrc = vecFrmInfo[i].strImgPath;

        //判断是否有自动匹配点，若有则读取原有的自动匹配点（标识为1的点）
        if(vecImgPtSets[i].vecPts.size() != 0)
        {
//            int n1 = vecImgPtSets[i].vecPts.size();
            for(UINT m = 0; m < vecImgPtSets[i].vecPts.size(); m++)
            {
                AutoPts.x = vecImgPtSets[i].vecPts[m].X;
                AutoPts.y = vecImgPtSets[i].vecPts[m].Y;
                AutoPts.ptIdx = vecImgPtSets[i].vecPts[m].lID;
                pmImportInfo.vecMatchPtsSrc.push_back(AutoPts);
                ///pmImportInfo.vecFeatPtsAuto.push_back(AutoPts);
            }
        }

        //判断该图像是否有人工添加的点（标识为），若有则读取
        if(vecImgPtSets[i].vecAddPts.size() != 0)
        {
//            int n2 = vecImgPtSets[i].vecAddPts.size();
            for(UINT j = 0; j < vecImgPtSets[i].vecAddPts.size(); j++)
            {
                AddPts.x = vecImgPtSets[i].vecAddPts[j].X;
                AddPts.y = vecImgPtSets[i].vecAddPts[j].Y;
                AddPts.ptIdx = vecImgPtSets[i].vecAddPts[j].lID;
                pmImportInfo.vecMatchPtsSrc.push_back(AddPts);
                ///pmImportInfo.vecFeatPtsManual.push_back(AutoPts);
            }
        }

//        int nn = pmImportInfo.vecMatchPtsSrc.size();
        vecPanoMatchPts.push_back(pmImportInfo);
        pmImportInfo.vecMatchPtsSrc.clear();
    }

    bool bResult = PanoMosaic(vecParam, vecImagePath, dExtParam, vecPanoMatchPts, strOutPath);

    return bResult;

}
