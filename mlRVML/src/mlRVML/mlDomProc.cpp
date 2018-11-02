/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlDomProc.cpp
* @date 2011.12.18
* @author 吴凯  wukai@irsa.ac.cn
* @brief  单站地形dom生成功能模块类源文件(包括多视角下地形生成)
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#include "mlDomProc.h"
#include "mlPhgProc.h"
/**
 *@fn CmlDomProc()
 *@date 2011.11
 *@author 吴凯
 *@brief DOM处理类构造函数
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
CmlDomProc::CmlDomProc()
{
    m_nBlockNum = 0 ;
}

/**
 *@fn ~CmlDomProc()
 *@date 2011.11
 *@author 吴凯
 *@brief DOM处理类析构函数
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
CmlDomProc::~CmlDomProc()
{

}

/**
 *@fn createOrthoImage()
 *@date 2011.11
 *@author 吴凯
 *@brief   序列影像生成正射影像
 *@param vecStereoImgInfo 序列影像信息
 *@param strDemFilePath dem 文件路径
 *@param strDomFilePath dom 文件路径
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
bool CmlDomProc::createOrthoImage(vector<StereoSet>& vecStereoImgInfo , string strDemFilePath , string strDomFilePath, ImgDotType imgType)
{
    // dem栅格数据
    CmlGeoRaster geoDemRaster ;
    if(!geoDemRaster.LoadGeoFile(strDemFilePath.c_str()))
    {
        SCHAR strErr[] = "MapByInteBlock error : Fail to open geotiff file\n" ;
        LOGAddErrorMsg(strErr) ;
        return false ;
    }
    else
    {
        // 将dem的左上角点赋值为dom左上角点
        ptOrigin = geoDemRaster.m_PtOrigin ;
        m_nHeight = geoDemRaster.GetHeight();
        m_nWidth = geoDemRaster.GetWidth();
        dRes = fabs(geoDemRaster.m_dXResolution) ;
        // 判断分块数
        SINT nBlockWidth ;
        SINT nBlockHeight ;
        ULONG lBlockSize ;
        nBlockWidth = m_nWidth ;
        ULONG lDemSize = m_nHeight * nBlockWidth ;
        if(lDemSize < DEM_BLOCK_SIZE)
        {
            m_nBlockNum = 1 ;
            nBlockHeight = m_nHeight ;  // 分块的高度
            lBlockSize = nBlockWidth * nBlockHeight ; // 分块的数据量
        }
        else
        {
            nBlockHeight = (SINT)(DEM_BLOCK_SIZE / nBlockWidth) ; // 分块的高度
            lBlockSize = nBlockWidth * nBlockHeight ; // 分块的数据量
            m_nBlockNum = (SINT)(lDemSize / lBlockSize) ;
            ULONG lDelta = lDemSize % lBlockSize ;
            if(lDelta != 0)
            {
                m_nBlockNum += 1 ;
            }
        }
        // 得到外方位线元素序列
        SINT nImgPair = vecStereoImgInfo.size() ;
        for(SINT i = 0 ; i < nImgPair; i++)
        {
            Pt3d ptTemp ;
            ptTemp.X = vecStereoImgInfo[i].imgLInfo.exOri.pos.X ;
            ptTemp.Y = vecStereoImgInfo[i].imgLInfo.exOri.pos.Y ;
            ptTemp.Z = vecStereoImgInfo[i].imgLInfo.exOri.pos.Z ;
            vecImgPos.push_back(ptTemp) ;
        }
        // 生成geotiff 格式的dom
        CmlGeoRaster geoDomRaster ;
        GDALDataType gType = (GDALDataType)imgType;
        SCHAR* pImgPath = const_cast<SCHAR*>(strDomFilePath.c_str()) ;
        if(!geoDomRaster.CreateGeoFile(pImgPath , ptOrigin , dRes , -dRes , m_nHeight, m_nWidth , 1 , gType , DOM_NODATA ))
        {
            SCHAR strErr[] = "createOrthoImage error : Failed to create geoTiff file\n" ;
            LOGAddErrorMsg(strErr) ;
            return false ;
        }
        UINT nBytes = 0;
        switch( imgType )
        {
            case T_Byte:
            nBytes = 1;
            break;
            case T_UInt16:
            nBytes = 2;
            break;
            case T_Int16:
            nBytes = 2;
            break;
            case T_UInt32:
            nBytes = 4;
            break;
            case T_Int32:
            nBytes = 4;
            break;
            case T_Float32:
            nBytes = 4;
            break;
            case T_Float64:
            nBytes = 8;
            break;
            default:
            break;
        }

        // 分块赋值dom灰度
        for(SINT i = 0 ; i < m_nBlockNum ; i++)
        {
            CmlRasterBlock imgDemRaster ;
            CmlRasterBlock imgDomRaster;
            imgDomRaster.SetGDTType(gType) ;
            imgDemRaster.SetGDTType(geoDemRaster.GetGDTType()) ;
            if(i< (m_nBlockNum -1 ))
            {
                // 得到与dom地理坐标范围对应的dem栅格数据块
                if(!geoDemRaster.GetRasterOriginBlock((UINT)1 , 0 , (UINT)(i*nBlockHeight) , (UINT)nBlockWidth, (UINT)nBlockHeight , (UINT)1 , &imgDemRaster ))
                {
                    SCHAR strErr[] = "MapByInteBlock error : Fail to read raster block\n" ;
                    LOGAddErrorMsg(strErr) ;
                    return false ;
                }
                DOUBLE *pDem = new DOUBLE[nBlockHeight*nBlockWidth];
                for( UINT nTi = 0; nTi < (UINT)nBlockHeight; ++nTi )
                {
                    for( UINT nTj = 0; nTj < (UINT)nBlockWidth; ++nTj )
                    {
                         imgDemRaster.GetDoubleVal( nTi, nTj, pDem[nTi*nBlockWidth+nTj] );
                    }
                }
                DOUBLE *pDom = new DOUBLE[nBlockHeight*nBlockWidth];
                imgDomRaster.InitialImg(nBlockHeight , nBlockWidth , nBytes ) ;

                // 赋值dom栅格数据块
                if(!fillDomBlock(vecStereoImgInfo , pDem , 0 , i*nBlockHeight , nBlockWidth , nBlockHeight, pDom))
                {
                    delete[] pDem;
                    delete[] pDom;
                    SCHAR strErr[] = "createOrthoImage error : Fail to write to raster block\n" ;
                    LOGAddErrorMsg(strErr) ;
                    return false ;
                }
                else
                {
                    for( UINT ni = 0; ni < (UINT)nBlockHeight; ++ni )
                    {
                        for( UINT nj = 0; nj < (UINT)nBlockWidth; ++nj )
                        {
                            imgDomRaster.SetDoubleVal( ni, nj, pDom[ni*nBlockWidth+nj] );
                        }
                    }
                    delete[] pDem;
                    delete[] pDom;
                }
            }
            else
            {
                // 得到与dom地理坐标范围对应的dem栅格数据块
                if(!geoDemRaster.GetRasterOriginBlock((UINT)1 , 0 , (UINT)(i*nBlockHeight) , (UINT)nBlockWidth, (UINT)(m_nHeight - i*nBlockHeight) , (UINT)1 , &imgDemRaster ))
                {
                    SCHAR strErr[] = "MapByInteBlock error : Fail to read raster block\n" ;
                    LOGAddErrorMsg(strErr) ;
                    return false ;
                }
                UINT nTH = m_nHeight - i*nBlockHeight;

                DOUBLE *pDem = new DOUBLE[nTH*nBlockWidth];
                for( UINT nTi = 0; nTi < (UINT)nTH; ++nTi )
                {
                    for( UINT nTj = 0; nTj < (UINT)nBlockWidth; ++nTj )
                    {
                         imgDemRaster.GetDoubleVal( nTi, nTj, pDem[nTi*nBlockWidth+nTj] );
                    }
                }
                DOUBLE *pDom = new DOUBLE[nTH*nBlockWidth];
                imgDomRaster.InitialImg(nTH , nBlockWidth , nBytes ) ;
                // 赋值dom栅格数据块
                if(!fillDomBlock(vecStereoImgInfo , pDem , 0 , i*nBlockHeight , nBlockWidth , nTH , pDom))
                {
                    delete[] pDem;
                    delete[] pDom;
                    SCHAR strErr[] = "createOrthoImage error : Fail to write to raster block\n" ;
                    LOGAddErrorMsg(strErr) ;
                    return false ;
                }
                else
                {
                    for( UINT ni = 0; ni < nTH; ++ni )
                    {
                        for( UINT nj = 0; nj < (UINT)nBlockWidth; ++nj )
                        {
                            imgDomRaster.SetDoubleVal( ni, nj, pDom[ni*nBlockWidth+nj] );
                        }
                    }
                    delete[] pDem;
                    delete[] pDom;
                }

            }
            // 存储dom栅格数据块
            geoDomRaster.SaveBlockToFile((UINT)1 , 0 , (UINT)(i * nBlockHeight ) , &imgDomRaster) ;
        }
    }
    return true ;
}
/**
 *@fn fillDomBlock()
 *@date 2011.11
 *@author 吴凯
 *@brief  对dom 分块数据赋值
 *@param vecStereoImgInfo  序列影像信息
 *@param *pDem  分块dem栅格数据
 *@param vecStereoImgInfo  序列影像信息
 *@param nXOffset  dom 沿x方向偏移量
 *@param nYOffset  dom 沿y方向偏移量
 *@param nWidth  dem 分块宽度
 *@param nHeight  dom 分块高度
 *@param *pDem  对应于分块dem的dom灰度值数据
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
bool CmlDomProc::fillDomBlock(vector<StereoSet>& vecStereoImgInfo , DOUBLE* pDem , SINT nXOffset , SINT nYOffset , SINT nWidth , SINT nHeight , DOUBLE* pDom)
{
    // 得到序列影像对数
    SINT nImgPair ;
    if(!(nImgPair = vecStereoImgInfo.size()))
    {
        SCHAR strErr[] = "createOrthoMap error : image info fails to load properly" ;
        LOGAddErrorMsg(strErr) ;
        return false ;
    }
    // dom灰度值结构体分配空间
    structDomUnit* pDomBlock = NULL ;
    if(!(pDomBlock = new structDomUnit[nHeight * nWidth]))
    {
        SCHAR strErr[] = "createOrthoMap error : memory allocation failed" ;
        LOGAddErrorMsg(strErr) ;
        return false ;
    }
    //dom灰度值结构体初始化
    else
    {
        for( SINT i = 0 ; i < nHeight ; i++)
        {
            for( SINT j = 0 ; j < nWidth ; j++)
            {
                pDomBlock[i * nWidth+ j].bFlag = false ;
                pDomBlock[i * nWidth + j].b_Value = 255 ;
                pDomBlock[i * nWidth + j].dMinDis = 1024 ;
                pDomBlock[i * nWidth + j].nImg = 0 ;
            }
        }
    }
    // 地理坐标反投影到影像插值，得到dom栅格数据块的灰度
    for( SINT i = 0 ; i < nImgPair ; i++)
    {
        projOrthoMapVal(vecStereoImgInfo[i] , i , nXOffset , nYOffset , nWidth , nHeight , pDem , pDomBlock) ;
    }
    // test
//    std::ofstream stmSave("dom.txt");
    for( SINT i = 0 ; i < nHeight ; i++)
    {
        for( SINT j = 0 ; j < nWidth ; j++)
        {
            pDom[i * nWidth+ j] = pDomBlock[i * nWidth + j].b_Value ;
//            stmSave << pDomBlock[i * nWidth + j].b_Value << "\t" ;
        }
//        stmSave << "\n" ;
    }
//    stmSave.close() ;
    if(pDomBlock)
    {
        delete [] pDomBlock ;
    }
    return true ;
}

/**
*@fn projOrthoMapVal()
*@date 2011.11
*@author 吴凯
*@brief  反投影到正射影像， 取得相应范围的影像值
*@param structCamInfo 单影像信息
*@param nImgNo  影像序号
*@param nXOffset  dom 沿x方向偏移量
*@param nYOffset  dom 沿y方向偏移量
*@param nW  dem 分块宽度
*@param nH  dom 分块高度
*@param *pDem  对应于分块dem的dom灰度值数据
*@param *pUnitBlock   dom 灰度值结构体单元
*@retval TRUE 成功
*@retval FALSE 失败
*@version 1.0
*@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
*/
bool CmlDomProc::projOrthoMapVal(StereoSet& structCamInfo , SINT nImgNo , SINT nXOffset , SINT nYOffset ,  SINT  nW , SINT nH , DOUBLE*  pDem , structDomUnit* pUnitBlock)
{
    // 得到影像栅格数据块
    CmlGdalDataset CImgRaster ;
    SCHAR* pImgPath = const_cast<SCHAR*>(structCamInfo.imgLInfo.strImgPath.c_str()) ;
    if(!CImgRaster.LoadFile(pImgPath))
    {
        SCHAR strErr[] = "projOrthoMapVal error : failed to load image file " ;
        LOGAddErrorMsg(strErr) ;
        return false ;
    }
    // 获取影像宽度及高度
    m_nImgHeight = structCamInfo.imgLInfo.nH ;
    m_nImgWidth = structCamInfo.imgLInfo.nW ;
    // 对DEM范围内的覆盖下的区域进行赋值
    return fillProjPolyVal(structCamInfo.imgLInfo.exOri , nImgNo , structCamInfo.imgLInfo.inOri , nXOffset , nYOffset , nW , nH , pDem , &CImgRaster , pUnitBlock) ;

}
/**
 *@fn fillProjPolyVal()
 *@date 2011.11
 *@author 吴凯
 *@brief  对DEM范围内的覆盖下的区域进行赋值
 *@param exPara 影像外方位元素
 *@param nImgNo 影像序号
 *@param inPara 影像内方位元素
 *@param nH dom 分块高度
 *@param nW dom 分块宽度
 *@param *pDem 对应于分块dem的dom灰度值数据
 *@param pRasterProc 影像栅格数据信息
 *@param pUnitBlock dom 灰度值结构体单元
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
bool CmlDomProc::fillProjPolyVal(ExOriPara exPara , SINT nImgNo , InOriPara inPara  , SINT nXOffset , SINT nYOffset , SINT  nW ,  SINT nH , DOUBLE*  pDem ,
                                 CmlGdalDataset* pRasterProc , structDomUnit* pUnitBlock)
{
    CmlMat matZBuff;
    CmlMat RMat ;
    DOUBLE R[9] ;
    OPK2RMat(&exPara.ori , &RMat) ;
    for(SINT i = 0 ; i < 3 ; i++)
    {
        for(SINT j = 0 ; j < 3 ; j++)
        {
            R[i*3 + j] = RMat.GetAt(i , j) ;
        }
    }
    Pt3d ptS = exPara.pos ;
    SINT nImgPair = vecImgPos.size() ;
    Pt3d ptOpp = vecImgPos[(nImgNo+nImgPair/2 + 1)%nImgPair] ;
//    DOUBLE dFocal = inPara.f ;
    // 判断外围矩形与DEM范围的关系 ,  注意地理坐标系的方向 , 转变为图像坐标系
    SINT nLeft , nRight , nTop , nBottom ;
    nLeft = nXOffset ;
    nRight = nXOffset + nW  ;
    nTop = nYOffset ;
    nBottom = nYOffset + nH  ;
    CmlFrameImage CFrameImg ;
    CmlRasterBlock CRasterData ;
    // 得到图像栅格数据
//    std::ofstream stmSave("index.txt");
    pRasterProc->GetRasterOriginBlock((UINT)1 , (UINT)0 , (UINT)0 , m_nImgWidth , m_nImgHeight , (UINT)1 , &CRasterData) ;
    if((nRight < nXOffset) || (nLeft > (nXOffset + nW-1)) || (nTop > (nYOffset + nH - 1)) || (nBottom < nYOffset)) // 外围矩形在分块DOM范围之外，直接返回
    {
        return false ;
    }
    else
    {
//        UINT nZoom = 5;
//        if( true == matZBuff.Initial( pRasterProc->GetHeight()*nZoom, pRasterProc->GetWidth()*nZoom ) )
//        {
//            for( UINT i = 0; i < matZBuff.GetH(); ++i )
//            {
//                for( UINT j = 0; j < matZBuff.GetW(); ++j )
//                {
//                    matZBuff.SetAt( i, j, -99999 );
//                }
//            }
//        }
        //---------------------------------
//        for(SINT i = nTop ; i < nBottom ; i++)
//        {
//            if(i < nYOffset )
//            {
//                continue ;
//            }
//            else if(i > (nYOffset + nH-1))
//            {
//                break ;
//            }
//            for(SINT j = nLeft ; j < nRight ; j++)
//            {
//                if(j < nXOffset)
//                {
//                    continue ;
//                }
//                else if( j > (nXOffset + nW-1))
//                {
//                    break ;
//                }
//                Pt3d ptGeo ;
//                Pt2d ptImg ;
//                ptGeo.X = ptOrigin.X + j * dRes ;
//                ptGeo.Y = ptOrigin.Y - i * dRes ;
//                ptGeo.Z = pDem[(i - nYOffset)* nW + j - nXOffset] ;  // 分块读取dem
//                if( fabs( ptGeo.Z - DEM_NO_DATA) < ML_ZERO )
//                {
//                    continue ;
//                }
//
//                // 增加判断位置方位关系
//
//                if( false == projObjPt2ImgPt(ptS , ptGeo , R , inPara , ptImg) )
//                {
//                    continue;
//                }
////                DOUBLE dZBuf = DisIn2Pts( ptS, ptGeo );
////
////                UINT nTx = UINT( ptImg.X * nZoom + 0.5 );
////                UINT nTy = UINT( ptImg.Y * nZoom + 0.5 );
////                if( ( nTx < matZBuff.GetW() )&&( nTy < matZBuff.GetH() ) )
////                {
////                    DOUBLE dCurZ = matZBuff.GetAt( nTy, nTx );
////                    if( ( dCurZ < 0 )||( dZBuf < dCurZ ) )
////                    {
////                        matZBuff.SetAt( nTy, nTx, dZBuf );
////                    }
////
////                }
//            }
//        }



        //---------------------------------
        for(SINT i = nTop ; i < nBottom ; i++)
        {
            if(i < nYOffset )
            {
                continue ;
            }
            else if(i > (nYOffset + nH-1))
            {
                break ;
            }
            for(SINT j = nLeft ; j < nRight ; j++)
            {
                if(j < nXOffset)
                {
                    continue ;
                }
                else if( j > (nXOffset + nW-1))
                {
                    break ;
                }
                Pt3d ptGeo ;
                Pt2d ptImg ;
                ptGeo.X = ptOrigin.X + j * dRes ;
                ptGeo.Y = ptOrigin.Y - i * dRes ;
                ptGeo.Z = pDem[(i - nYOffset)* nW + j - nXOffset] ;  // 分块读取dem
                if( fabs( ptGeo.Z - DEM_NO_DATA) < ML_ZERO )
                {
                    continue ;
                }

                // 增加判断位置方位关系

                if( false == projObjPt2ImgPt(ptS , ptGeo , R , inPara , ptImg) )
                {
                    continue;
                }

                //--------------------------------
 //               DOUBLE dZBuf = DisIn2Pts( ptS, ptGeo );

                //--------------------------------
//                double dVal ;
                // 比较与dom数据区域值的位置关系
//                if((ptImg.X < ClipBuffer) || (ptImg.X > (m_nImgWidth -1 - ClipBuffer)) || (ptImg.Y < ClipBuffer) || (ptImg.Y > (m_nImgHeight -1 - ClipBuffer))
//                        || ((dVal = calCrossVal(ptGeo , ptOpp , exPara)) >= 0))
//                {
//                    continue ;
//                }
//                if((ptImg.X < ClipBuffer) || (ptImg.X > (m_nImgWidth -1 - ClipBuffer)) || (ptImg.Y < ClipBuffer) || (ptImg.Y > (m_nImgHeight -1 - ClipBuffer))
//                   || ((dVal = calCrossVal(ptGeo , exPara)) >= 0))
                if((ptImg.X < ClipBuffer) || (ptImg.X > (m_nImgWidth -1 - ClipBuffer)) || (ptImg.Y < ClipBuffer) || (ptImg.Y > (m_nImgHeight -1 - ClipBuffer)))
                {
                    continue ;
                }
                // 有值传下来
                if(!pUnitBlock[(i - nYOffset)* nW + j - nXOffset].bFlag) // 还未对dom赋值
                {
                    pUnitBlock[(i - nYOffset)* nW + j - nXOffset].b_Value = CFrameImg.mlGetBilinearValue(&CRasterData , ptImg) ;
////                    BYTE bTemp = pUnitBlock[(i - nYOffset)* nW + j - nXOffset].b_Value ;
////                    if(pUnitBlock[(i - nYOffset)* nW + j - nXOffset].b_Value == 255)
////                    {
////                        continue ;
////                    }
////                    else
////                    {
////                        UINT nTx = UINT( ptImg.X * nZoom + 0.5 );
////                        UINT nTy = UINT( ptImg.Y * nZoom + 0.5 );
////                        if( ( nTx < matZBuff.GetW() )&&( nTy < matZBuff.GetH() ) )
////                        {
//                            DOUBLE dCurZBuf = matZBuff.GetAt( nTy, nTx );
//                            if( ( dCurZBuf > 0 ) && ( dCurZBuf < dZBuf ) )
//                            {
//                                pUnitBlock[(i - nYOffset)* nW + j - nXOffset].b_Value = 0;
//                            }
////                        }
////                    }
//                    stmSave << i <<"\t"<<j<<"\t"<< (double)bTemp << "\n" ;
                    pUnitBlock[(i - nYOffset)* nW + j - nXOffset].dMinDis = MIN(ptImg.X , m_nImgWidth-1-ptImg.X) ;
                    pUnitBlock[(i - nYOffset)* nW + j - nXOffset].bFlag = true ;
                    pUnitBlock[(i - nYOffset)* nW + j - nXOffset].nImg++ ;
                }
                else if(pUnitBlock[(i - nYOffset)* nW + j - nXOffset].nImg == 1 )
                {

                    BYTE bGray1 , bGray2 , bGray;
                    DOUBLE dGray = 0;
                    DOUBLE dRatio1 , dRatio2 , dRatioSum;
                    DOUBLE dDis = MIN(ptImg.X , m_nImgWidth-1-ptImg.X)  ;
                    DOUBLE dMinDis = pUnitBlock[(i - nYOffset)* nW + j - nXOffset].dMinDis ;
                    // 接缝地方线性加权平滑
                    bGray1 =  pUnitBlock[(i - nYOffset)* nW + j - nXOffset].b_Value ;
                    bGray2 = CFrameImg.mlGetBilinearValue(&CRasterData , ptImg) ;

                    //---------------------------
//                    if(bGray2 != 255)
//                    {
//                        UINT nTx = UINT( ptImg.X * nZoom + 0.5 );
//                        UINT nTy = UINT( ptImg.Y * nZoom + 0.5 );
//                        if( ( nTx < matZBuff.GetW() )&&( nTy < matZBuff.GetH() ) )
//                        {
//                            DOUBLE dCurZBuf = matZBuff.GetAt( nTy, nTx );
//                            if( ( dCurZBuf > 0 ) && ( dCurZBuf < dZBuf ) )
//                            {
//                                bGray2 = 0;
//                            }
//                        }
//                    }
                    //---------------------------
                    dRatio1 = dMinDis ;
                    dRatio2 = dDis ;
                    dRatioSum = dRatio1 + dRatio2 ;
                    dGray = (bGray1 * dRatio1/dRatioSum + bGray2 * dRatio2/dRatioSum) ;
//                    if( bGray2 == 0 )
//                    {
//                        dGray = 0;
//                    }
//                    else
//                    {
                        if( ( bGray1 == 255 )&&( bGray2 == 255 ) )
                        {
                            dGray = 255;
                        }
                        else if( ( bGray1 == 255 )&&( bGray2 != 255 ) )
                        {
                            dGray = bGray2;
                        }
                        else if( ( bGray1 != 255 )&&( bGray2 == 255 ) )
                        {
                            dGray = bGray1;
                        }
//                    }


                    pUnitBlock[(i - nYOffset)* nW + j - nXOffset].b_Value = BYTE(dGray+0.5) ;
                    pUnitBlock[(i - nYOffset)* nW + j - nXOffset].nImg++ ;
                }
            }
        }
    }

    return true ;
}

///**
// *@fn calCrossVal()
// *@date 2011.11
// *@author 吴凯
// *@brief  对DEM范围内的覆盖下的区域进行赋值
// *@param ptObj  物方点地理坐标
// *@param ptBack 影像相对的外方位线元素
// *@param exPara 影像外方位元素
// *@retval 向量相乘值，据此判断向量交叉角度
// *@version 1.0
// *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
// */
////DOUBLE CmlDomProc::calCrossVal(Pt3d ptObj , Pt3d ptBack , ExOriPara exPara)
////{
////    return ((ptObj.X - exPara.pos.X)*(ptBack.X - exPara.pos.X) + (ptObj.Y - exPara.pos.Y)*(ptBack.Y - exPara.pos.Y)) ;
////}
////
////DOUBLE CmlDomProc::calCrossVal(Pt3d ptObj , ExOriPara exPara)
////{
////    CmlMat RMat ;
////    OPK2RMat(&exPara.ori , &RMat) ;
////    return ((ptObj.X - exPara.pos.X)*RMat.GetAt(0,2) + (ptObj.Y - exPara.pos.Y)*RMat.GetAt(1,2) + (ptObj.Z - exPara.pos.Z)*RMat.GetAt(2,2));
////}

//double CmlDomProc::calCrossVal(Pt3d ptObj , ExOriPara exPara)
//{
//    CmlMat RMat ;
//    OPK2RMat(&exPara.ori , &RMat) ;
//    return ((ptObj.X - exPara.pos.X)*RMat.GetAt(0,2) + (ptObj.Y - exPara.pos.Y)*RMat.GetAt(1,2) + (ptObj.Z - exPara.pos.Z)*RMat.GetAt(2,2));
//}

/**
 *@fn projObjPt2ImgPt()
 *@date 2011.11
 *@author 吴凯
 *@brief  反投影地面点，取得相应的影像点
 *@param ptXs 影像线元素
 *@param ptObjX 物方点的物方点坐标
 *@param R 旋转矩阵
 *@param inPara 影像内方位元素
 *@param ptImgX 物方点对应的像方坐标
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
bool CmlDomProc::projObjPt2ImgPt(Pt3d ptXs , Pt3d ptObjX , DOUBLE* R , InOriPara& inPara , Pt2d& ptImgX)
{
    DOUBLE dTX = ptObjX.X - ptXs.X;
    DOUBLE dTY = ptObjX.Y - ptXs.Y;
    DOUBLE dTZ = ptObjX.Z - ptXs.Z;
    DOUBLE dTmpZ = dTX*R[2] + dTY*R[5] + dTZ*R[8];
    if( (dTmpZ) > 0 )
    {
        return false;
    }

    DOUBLE x , y ;
    x = -inPara.f * (R[0]*(dTX) + R[3]*(dTY) + R[6]*(dTZ))/
        (dTmpZ) ;
    y = -inPara.f * (R[1]*(dTX) + R[4]*(dTY) + R[7]*(dTZ)) /
        (dTmpZ) ;
    // 坐标系转换 ，以图像左上角点为原点
    ptImgX.X = x + inPara.x ;
    ptImgX.Y = -y + inPara.y ;
    return true ;
}
