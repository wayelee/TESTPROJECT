/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlSiteMapping.cpp
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 站点地形建立源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#include "mlSiteMapping.h"
#include "mlFrameImage.h"
#include "mlStereoProc.h"
#include "mlDemProc.h"
#include "mlPtsManage.h"
#include "mlPhgProc.h"
#include "mlSatMapping.h"
#include "SiftMatch.h"
#include "mlBA.h"

/**
* @fn getPtRect
* @date 2011.11.02
* @author 吴凯 wukai@irsa.ac.cn
* @brief 计算三维点云外包矩形框
* @param vecPts 三维点云
* @param rectMax 设定最大矩形框
* @return 三维点云外包矩形框
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
DbRect getPtRect( vector<Pt3d> &vecPts, DbRect rectMax )
{
    DbRect rectCur;
    rectCur.right = -9999999;
    rectCur.left = 9999999;
    rectCur.top = -9999999;
    rectCur.bottom = 9999999;

    for( UINT i = 0; i < vecPts.size(); ++i )
    {
        Pt3d* ptCur = &vecPts.at(i);
        if( ( ptCur->X > rectMax.right ) || ( ptCur->X < rectMax.left ) || ( ptCur->Y > rectMax.top ) || ( ptCur->Y < rectMax.bottom ) )
        {
            continue;
        }
        else
        {
            rectCur.right = ( rectCur.right < ptCur->X ) ? ptCur->X : rectCur.right;
            rectCur.left = ( rectCur.left > ptCur->X ) ? ptCur->X : rectCur.left;
            rectCur.top = ( rectCur.top < ptCur->Y ) ? ptCur->Y : rectCur.top;
            rectCur.bottom = ( rectCur.bottom > ptCur->Y ) ? ptCur->Y : rectCur.bottom;
        }
    }
    return rectCur;
}
/**
* @fn CmlSiteMapping
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlSiteMapping类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlSiteMapping::CmlSiteMapping()
{
    //ctor
    m_dDemRes = 1.0 ;
    m_dbDemRect.left = MAX_DEM ;
    m_dbDemRect.right = MIN_DEM ;
    m_dbDemRect.bottom = MAX_DEM ;
    m_dbDemRect.top = MIN_DEM ;
}
/**
* @fn paraInit
* @date 2011.11.02
* @author 吴凯 wukai@irsa.ac.cn
* @brief 参数初始化
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlSiteMapping::paraInit()
{
    m_dbDemRect.left = MAX_DEM ;
    m_dbDemRect.right = MIN_DEM ;
    m_dbDemRect.bottom = MAX_DEM ;
    m_dbDemRect.top = MIN_DEM ;
    return true;
}
/**
* @fn ~CmlSiteMapping
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlSiteMapping类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlSiteMapping::~CmlSiteMapping()
{
    //dtor
}
/**
* @fn GetStereoFeatPts
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 立体影像对特征点匹配，计算三维点云，存入文件
* @param pStereoSet 立体影像数据信息
* @param strLPtPath 左影像匹配点文件路径
* @param strRPtPath 右影像匹配点文件路径
* @param vecPt3ds 三维点云
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlSiteMapping::GetStereoFeatPts( StereoSet* pStereoSet, \
                                       ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                                       ImgPtSet &clsImgPtL, ImgPtSet &clsImgPtR, vector<Pt3d> &vecPt3ds, Pt3d &ptMinDis )
{
    if( pStereoSet == NULL )
    {
        return false;
    }
    vector<StereoMatchPt> stereoMPt;
    CmlPtsManage clsPtManage;

    CmlStereoProc clsStereo;

    vector<Pt3d> vecTemp3ds;
    Pt3d ptCent;
    ptCent.X = ( pStereoSet->imgLInfo.exOri.pos.X + pStereoSet->imgRInfo.exOri.pos.X  ) / 2.0;
    ptCent.Y = ( pStereoSet->imgLInfo.exOri.pos.Y + pStereoSet->imgRInfo.exOri.pos.Y  ) / 2.0;
    ptCent.Z = ( pStereoSet->imgLInfo.exOri.pos.Z + pStereoSet->imgRInfo.exOri.pos.Z  ) / 2.0;
    DOUBLE dMinDis = 9999999999;

    if( true == clsPtManage.GetPairPts( clsImgPtL, clsImgPtR, stereoMPt ) )
    {
        for( UINT i = 0; i < stereoMPt.size(); ++i )
        {
            StereoMatchPt &pt = stereoMPt[i];
            Pt3d ptXYZ;
            FrameImgInfo frmLInfo = clsImgPtL.imgInfo;
            FrameImgInfo frmRInfo = clsImgPtR.imgInfo;
            if( true == clsStereo.mlInterSection( pt.ptLInImg, pt.ptRInImg, (SINT)frmLInfo.nH, (SINT)frmRInfo.nH, ptXYZ, &frmLInfo.inOri, &frmLInfo.exOri, &frmRInfo.inOri, &frmRInfo.exOri ) )
            {
                vecTemp3ds.push_back( ptXYZ );
                DOUBLE dTempDis = DisIn2Pts( ptCent, ptXYZ );
                if( dTempDis < dMinDis )
                {
                    ptMinDis = ptXYZ;
                    dMinDis = dTempDis;
                }
            }
        }
    }
    else
    {
        CmlFrameImage clsImgL, clsImgR;
        CmlStereoProc clsStereoProc;
        CmlSatMapping clsSatProc;

        if( false == clsImgL.LoadFile( pStereoSet->imgLInfo.strImgPath.c_str() ) )
        {
            return false;
        }
        clsImgL.m_InOriPara = pStereoSet->imgLInfo.inOri;
        clsImgL.m_ExOriPara = pStereoSet->imgLInfo.exOri;
        clsImgL.GrayTensile( );
        clsImgL.SmoothByGuassian(  7, 0.6 );
        clsImgL.ExtractFeatPtByForstner( extractPtsOpts.nGridSize, extractPtsOpts.nPtMaxNum, extractPtsOpts.dThresCoef );

        if( false == clsImgR.LoadFile( pStereoSet->imgRInfo.strImgPath.c_str() ) )
        {
            return false;
        }
        clsImgR.m_InOriPara = pStereoSet->imgRInfo.inOri;
        clsImgR.m_ExOriPara = pStereoSet->imgRInfo.exOri;
        clsImgR.GrayTensile( );
        clsImgR.SmoothByGuassian(  7, 0.6 );
        clsImgR.ExtractFeatPtByForstner( extractPtsOpts.nGridSize, extractPtsOpts.nPtMaxNum, extractPtsOpts.dThresCoef );

        clsStereoProc.m_pDataL = &clsImgL;
        clsStereoProc.m_pDataR = &clsImgR;

        Pt3d pPosL = pStereoSet->imgLInfo.exOri.pos;
        Pt3d pPosR = pStereoSet->imgRInfo.exOri.pos;
//        DOUBLE dBaseWidth = sqrt( (pPosL.X - pPosR.X)*(pPosL.X - pPosR.X)+(pPosL.Y - pPosR.Y)*(pPosL.Y - pPosR.Y)+(pPosL.Z - pPosR.Z)*(pPosL.Z - pPosR.Z) );

        if( pStereoSet->nStereoLevel ==  2)//为核线影像
        {
            MLRect rect;
            rect.dXMax = matchOpts.dXMax;
            rect.dYMax = matchOpts.dYMax;
            rect.dXMin = matchOpts.dXMin;
            rect.dYMin = matchOpts.dYMin;
            clsStereoProc.mlTemplateMatchInFeatPt( rect, matchOpts.nTempSize, matchOpts.dCoefThres, matchOpts.dXOff, matchOpts.dYOff );

            clsStereoProc.mlGetRansacPts( clsStereoProc.m_vecFeatMatchPt, ransacOpts.dThres, ransacOpts.dConfidence );

            clsStereoProc.mlFilterPtsByMedian( clsStereoProc.m_vecFeatMatchPt, mFilterOpts.nFilterSize, mFilterOpts.dThresCoef, mFilterOpts.nThres );

            clsStereoProc.mlLsMatchInFrameImg( clsStereoProc.m_vecFeatMatchPt, 9 );

            clsStereoProc.mlInterSectionInFrameImg( clsStereoProc.m_vecFeatMatchPt );

            clsPtManage.SplitPairPts( pStereoSet->imgLInfo, pStereoSet->imgRInfo, clsStereoProc.m_vecFeatMatchPt, clsImgPtL, clsImgPtR );

            for( UINT i = 0; i < clsStereoProc.m_vecFeatMatchPt.size(); ++i )
            {
                Pt3d ptTemp = clsStereoProc.m_vecFeatMatchPt[i] ;
                vecTemp3ds.push_back( ptTemp );

                DOUBLE dTempDis = DisIn2Pts( ptCent, ptTemp );
                if( dTempDis < dMinDis )
                {
                    ptMinDis = ptTemp;
                    dMinDis = dTempDis;
                }
            }
        }
        else
        {
            ////////////////////
            //不为核线影像
            return false;
            /////////////////
        }
    }
    vecPt3ds.insert( vecPt3ds.end(), vecTemp3ds.begin(), vecTemp3ds.end() );
    return true;
}
/**
* @fn GetStereoDensePts
* @date 2011.11.02
* @author 彭嫚
* @brief 立体影像对特征点匹配，密集匹配，计算三维点云，存入文件
* @param pStereoSet 立体影像数据信息
* @param WidePara 立体影像匹配参数
* @param ptSetL 左影像密集匹配点
* @param ptSetR 右影像密集匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlSiteMapping::GetStereoDensePts( StereoSet* pStereoSet,  WideOptions WidePara, ImgPtSet& ptSetL, ImgPtSet& ptSetR )
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc clsStereoProc;

    if(clsImgL.LoadFile( pStereoSet->imgLInfo.strImgPath.c_str() )==false)
    {
        return false;
    }
    clsImgL.m_InOriPara = pStereoSet->imgLInfo.inOri;
    clsImgL.m_ExOriPara = pStereoSet->imgLInfo.exOri;
    clsImgL.SmoothByGuassian(  7, 0.6 );
    clsImgL.ExtractFeatPtByForstner( 5 );

    if(clsImgR.LoadFile( pStereoSet->imgRInfo.strImgPath.c_str() )==false)
    {
        return false;
    }
    clsImgR.m_InOriPara = pStereoSet->imgRInfo.inOri;
    clsImgR.m_ExOriPara = pStereoSet->imgRInfo.exOri;
    clsImgR.SmoothByGuassian(  7, 0.6 );
    clsImgR.ExtractFeatPtByForstner( 5 );

    clsStereoProc.m_pDataL = &clsImgL;
    clsStereoProc.m_pDataR = &clsImgR;

    Pt3d pPosL = pStereoSet->imgLInfo.exOri.pos;
    Pt3d pPosR = pStereoSet->imgRInfo.exOri.pos;
    double dBaseWidth = sqrt( (pPosL.X - pPosR.X)*(pPosL.X - pPosR.X)+(pPosL.Y - pPosR.Y)*(pPosL.Y - pPosR.Y)+(pPosL.Z - pPosR.Z)*(pPosL.Z - pPosR.Z) );

    if( pStereoSet->nStereoLevel ==  2)//为核线影像
    {
        MLRect rectSearch;
        rectSearch.dYMax = 2;
        rectSearch.dYMin = -2;
        rectSearch.dXMax = 5;
        rectSearch.dXMin = -1.0 * pStereoSet->imgLInfo.inOri.f *dBaseWidth / 1.5;


        clsStereoProc.mlTemplateMatchInFeatPt( rectSearch, 17 );

        clsStereoProc.mlGetRansacPts( clsStereoProc.m_vecFeatMatchPt );

        clsStereoProc.mlFilterPtsByMedian( clsStereoProc.m_vecFeatMatchPt );

        vector<Pt2d> vecDPtsL, vecDPtsR;

        clsStereoProc.mlDenseMatch(&clsImgL.m_DataBlock, &clsImgR.m_DataBlock, clsStereoProc.m_vecFeatMatchPt , WidePara, vecDPtsL, vecDPtsR);
        int nNum = vecDPtsL.size();
        ptSetL.imgInfo = pStereoSet->imgLInfo;
        ptSetR.imgInfo = pStereoSet->imgRInfo;

        Pt2d tempLPt, tempRPt;
        for(int k=0; k<nNum; k++)
        {
            tempLPt.X = vecDPtsL[k].X;
            tempLPt.Y = vecDPtsL[k].Y;
            tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
            tempLPt.byType = 1;
            tempRPt.X = vecDPtsR[k].X;
            tempRPt.Y = vecDPtsR[k].Y;
            tempRPt.lID = tempLPt.lID;
            tempRPt.byType = 1;
            ptSetL.vecPts.push_back(tempLPt);
            ptSetR.vecPts.push_back(tempRPt);
        }

        return true;
    }
    else
    {
        ////////////////////
        //不为核线影像
        return false;
        /////////////////
    }

}
/**
* @fn GetRegionDensePts
* @date 2011.12.14
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 影像指定区域密集匹配
* @param[in] pStereoSet 立体像对
* @param[in] GauPara 高斯滤波参数
* @param[in] ExtractPara 特征点提取参数
* @param[in] MatchPara 特征点匹配参数
* @param[in] RanPara RANSAC去粗差参数
* @param[in] RectSearch 模板匹配搜索范围参数
* @param[in] WidePara 密集匹配参数
* @param[in] Lx 待匹配左影像指定矩形范围的左上角x坐标
* @param[in] Ly 待匹配左影像指定矩形范围的左上角y坐标
* @param[in] ColRange 待匹配左影像指定矩形范围的宽
* @param[in] RowRange 待匹配左影像指定矩形范围的高
* @param[out] vecDPtSetL 密集匹配的左影像点坐标
* @param[out] vecDPtSetR 密集匹配的右影像点坐标
* @param[out] vecPtObj 密集匹配的点的三维坐标
* @param[out] vecCorr 密集匹配的相关系数
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlSiteMapping:: GetRegionDensePts( StereoSet* pStereoSet, GaussianFilterOpt GauPara, ExtractFeatureOpt ExtractPara, MatchInRegPara MatchPara, RANSACHomePara RanPara, MLRectSearch RectSearch, WideOptions WidePara,SINT Lx, SINT Ly, SINT ColRange, SINT RowRange, ImgPtSet& ptSetL, ImgPtSet& ptSetR, vector<Pt3d>& vecPtObj, vector<DOUBLE>& vecCorr)
{
    if( pStereoSet == NULL )
    {
        return false;
    }
    if(vecPtObj.size() > 0)
    {
        vecPtObj.clear();
    }

    if(vecCorr.size() > 0)
    {
        vecCorr.clear();
    }
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc clsStereoProc;

    if( ( false == clsImgL.LoadFile( pStereoSet->imgLInfo.strImgPath.c_str() ) ) || ( false == clsImgR.LoadFile( pStereoSet->imgRInfo.strImgPath.c_str() )) )
    {
        return false;
    }
    clsImgL.m_InOriPara = pStereoSet->imgLInfo.inOri;
    clsImgL.m_ExOriPara = pStereoSet->imgLInfo.exOri;
    clsImgL.SmoothByGuassian(  GauPara.nTemplateSize, GauPara.dCoef );
    clsImgL.ExtractFeatPtByForstner(ExtractPara.nGridSize, ExtractPara.nPtMaxNum);


    clsImgR.m_InOriPara = pStereoSet->imgRInfo.inOri;
    clsImgR.m_ExOriPara = pStereoSet->imgRInfo.exOri;
    clsImgR.SmoothByGuassian(  GauPara.nTemplateSize, GauPara.dCoef);
    clsImgR.ExtractFeatPtByForstner(ExtractPara.nGridSize, ExtractPara.nPtMaxNum);

    clsStereoProc.m_pDataL = &clsImgL;
    clsStereoProc.m_pDataR = &clsImgR;

    MLRect rectMatch;
    rectMatch.dXMin = Lx;
    rectMatch.dYMin = Ly;
    rectMatch.dXMax = Lx + ColRange;
    rectMatch.dYMax = Ly + RowRange;
    vector<DOUBLE> veccorrdense;
    if( pStereoSet->nStereoLevel ==  2)//为核线影像
    {

        Pt3d pPosL = pStereoSet->imgLInfo.exOri.pos;
        Pt3d pPosR = pStereoSet->imgRInfo.exOri.pos;

        MLRect rectSearch;
        rectSearch.dYMax = MatchPara.dYMax;
        rectSearch.dYMin = MatchPara.dYMin;
        rectSearch.dXMax = MatchPara.dXMax;
        rectSearch.dXMin = MatchPara.dXMin;

        if( false == clsStereoProc.mlTemplateMatchInFeatPt( rectSearch, MatchPara.nTempSize, MatchPara.dCoefThres ) )
        {
            return false;
        }

        clsStereoProc.mlGetRansacPts( clsStereoProc.m_vecFeatMatchPt,  RanPara.dThres,RanPara.dConfidence );

        clsStereoProc.mlFilterPtsByMedian( clsStereoProc.m_vecFeatMatchPt );

        vector<Pt2d> vecDPtsL, vecDPtsR;

        if( false == clsStereoProc.mlDenseMatch(&clsImgL.m_DataBlock, &clsImgR.m_DataBlock, clsStereoProc.m_vecFeatMatchPt , WidePara, rectMatch, vecDPtsL, vecDPtsR, veccorrdense) )
        {
            return false;
        }
        int nNum = vecDPtsL.size();
        ptSetL.imgInfo = pStereoSet->imgLInfo;
        ptSetR.imgInfo = pStereoSet->imgRInfo;

        Pt3d ptXYZ;
        Pt2d tempLPt, tempRPt;
        DOUBLE tempCorr;
        for(int k=0; k<nNum; k++)
        {
            tempLPt.X = vecDPtsL[k].X;
            tempLPt.Y = vecDPtsL[k].Y;

            if(PtInRect(tempLPt, rectMatch )==true)
            {
                tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
                tempLPt.byType = 1;
                tempRPt.X = vecDPtsR[k].X;
                tempRPt.Y = vecDPtsR[k].Y;
                tempRPt.lID = tempLPt.lID;
                tempRPt.byType = 1;
                ptSetL.vecPts.push_back(tempLPt);
                ptSetR.vecPts.push_back(tempRPt);

                //输出物方点坐标
                clsStereoProc.mlInterSection(tempLPt, tempRPt, (SINT)ptSetL.imgInfo.nH, (SINT)ptSetR.imgInfo.nH, ptXYZ, &ptSetL.imgInfo.inOri, &ptSetL.imgInfo.exOri, &ptSetR.imgInfo.inOri, &ptSetR.imgInfo.exOri );
                vecPtObj.push_back(ptXYZ);

                tempCorr = veccorrdense[k];
                vecCorr.push_back(tempCorr);

            }

        }

//        SINT nPt = ptSetR.vecPts.size();

        return true;
    }
    else
    {
        //不为核线影像
        MLRect rectSearch;
        rectSearch.dYMax = RectSearch.dYMax;
        rectSearch.dYMin = RectSearch.dYMin;
        rectSearch.dXMax = RectSearch.dXMax;
        rectSearch.dXMin = RectSearch.dXMin;
        clsStereoProc.mlTemplateMatchInFeatPt( rectSearch, MatchPara.nTempSize, MatchPara.dCoefThres );

        clsStereoProc.mlGetRansacPts( clsStereoProc.m_vecFeatMatchPt,  RanPara.dThres,RanPara.dConfidence );

        clsStereoProc.mlFilterPtsByMedian( clsStereoProc.m_vecFeatMatchPt );

        vector<Pt2d> vecDPtsL, vecDPtsR;


        clsStereoProc.mlDenseMatch(&clsImgL.m_DataBlock, &clsImgR.m_DataBlock, clsStereoProc.m_vecFeatMatchPt , WidePara, rectMatch, vecDPtsL, vecDPtsR, veccorrdense);
        int nNum = vecDPtsL.size();
        ptSetL.imgInfo = pStereoSet->imgLInfo;
        ptSetR.imgInfo = pStereoSet->imgRInfo;

        Pt3d ptXYZ;
        Pt2d tempLPt, tempRPt;
        DOUBLE tempCorr;
        for(int k=0; k<nNum; k++)
        {
            tempLPt.X = vecDPtsL[k].X;
            tempLPt.Y = vecDPtsL[k].Y;

            if(PtInRect(tempLPt, rectMatch )==true)
            {
                tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
                tempLPt.byType = 1;
                tempRPt.X = vecDPtsR[k].X;
                tempRPt.Y = vecDPtsR[k].Y;
                tempRPt.lID = tempLPt.lID;
                tempRPt.byType = 1;
                ptSetL.vecPts.push_back(tempLPt);
                ptSetR.vecPts.push_back(tempRPt);

                //输出物方点坐标
                clsStereoProc.mlInterSection(tempLPt, tempRPt, (SINT)ptSetL.imgInfo.nH, (SINT)ptSetR.imgInfo.nH, ptXYZ, &ptSetL.imgInfo.inOri, &ptSetL.imgInfo.exOri, &ptSetR.imgInfo.inOri, &ptSetR.imgInfo.exOri );
                vecPtObj.push_back(ptXYZ);

                tempCorr = veccorrdense[k];
                vecCorr.push_back(tempCorr);

            }

        }
        return true;
    }

}
bool CmlSiteMapping::GetRegionDensePts( StereoSet* pStereoSet, vector<StereoMatchPt> vecFeatMatchPt, WideOptions WidePara, SINT Lx, SINT Ly, SINT ColRange, SINT RowRange,
                                        ImgPtSet& ptSetL, ImgPtSet& ptSetR, vector<Pt3d>& vecPtObj, vector<DOUBLE>& vecCorr)
{
    if( pStereoSet == NULL )
    {
        return false;
    }
    if(vecFeatMatchPt.size()==0)
    {
        return false;
    }
    if(vecPtObj.size() > 0)
    {
        vecPtObj.clear();
    }

    if(vecCorr.size() > 0)
    {
        vecCorr.clear();
    }
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc clsStereoProc;

    if( ( false == clsImgL.LoadFile( pStereoSet->imgLInfo.strImgPath.c_str() ) ) || clsImgR.LoadFile( pStereoSet->imgRInfo.strImgPath.c_str() ) )
    {
        return false;
    }
    clsImgL.m_InOriPara = pStereoSet->imgLInfo.inOri;
    clsImgL.m_ExOriPara = pStereoSet->imgLInfo.exOri;

    clsImgR.m_InOriPara = pStereoSet->imgRInfo.inOri;
    clsImgR.m_ExOriPara = pStereoSet->imgRInfo.exOri;

    clsStereoProc.m_pDataL = &clsImgL;
    clsStereoProc.m_pDataR = &clsImgR;

    MLRect rectMatch;
    rectMatch.dXMin = Lx;
    rectMatch.dYMin = Ly;
    rectMatch.dXMax = Lx + ColRange;
    rectMatch.dYMax = Ly + RowRange;
    vector<DOUBLE> veccorrdense;

    vector<Pt2d> vecDPtsL, vecDPtsR;

    if( false == clsStereoProc.mlDenseMatch(&clsImgL.m_DataBlock, &clsImgR.m_DataBlock, clsStereoProc.m_vecFeatMatchPt , WidePara, rectMatch, vecDPtsL, vecDPtsR, veccorrdense) )
    {
        return false;
    }
    int nNum = vecDPtsL.size();
    ptSetL.imgInfo = pStereoSet->imgLInfo;
    ptSetR.imgInfo = pStereoSet->imgRInfo;

    Pt3d ptXYZ;
    Pt2d tempLPt, tempRPt;
    DOUBLE tempCorr;
    for(int k=0; k<nNum; k++)
    {
        tempLPt.X = vecDPtsL[k].X;
        tempLPt.Y = vecDPtsL[k].Y;

        if(PtInRect(tempLPt, rectMatch )==true)
        {
            tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
            tempLPt.byType = 1;
            tempRPt.X = vecDPtsR[k].X;
            tempRPt.Y = vecDPtsR[k].Y;
            tempRPt.lID = tempLPt.lID;
            tempRPt.byType = 1;
            ptSetL.vecPts.push_back(tempLPt);
            ptSetR.vecPts.push_back(tempRPt);

            //输出物方点坐标
            if( false == clsStereoProc.mlInterSection(tempLPt, tempRPt, (SINT)ptSetL.imgInfo.nH, (SINT)ptSetR.imgInfo.nH, ptXYZ, &ptSetL.imgInfo.inOri, &ptSetL.imgInfo.exOri, &ptSetR.imgInfo.inOri, &ptSetR.imgInfo.exOri ) )
            {
                continue;
            }
            vecPtObj.push_back(ptXYZ);

            tempCorr = veccorrdense[k];
            vecCorr.push_back(tempCorr);

        }

    }

//    SINT nPt = ptSetR.vecPts.size();

    return true;

}
/**
* @fn MapByMultiBlock
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 影像块分别制图合成
* @param vecStereoSet 立体影像数据信息
* @param vecStrMatchFile 匹配点文件
* @param ptLT 制图区域左上角坐标
* @param ptRB 制图区域右下角坐标
* @param dResolution 分辨率
* @param strDemPath Dem生成路径
* @param strDomPath Dom生成路径
* @param bIsUsingFeatPt 判断是否使用匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlSiteMapping::MapByMultiBlock(   vector<StereoSet> &vecStereoSet, vector<string> &vecStrMatchFile, Pt2d ptLT, Pt2d ptRB, DOUBLE dResolution,
                                        string strDemPath, string strDomPath, bool bIsUsingFeatPt )
{
//    if( bIsUsingFeatPt == true )
//    {
//
//        for( UINT i = 0; i < vecStereoSet.size(); ++i )
//        {
//            vector<Pt3d> vecSitePts;
//            StereoSet* pStereoSet = &vecStereoSet.at(i);
//
//            string strLCurPath = vecStrMatchFile[2*i];
//            string strRCurPath = vecStrMatchFile[2*i+1];
//
//            SINT nTempCPos = strLCurPath.rfind( "/" );
//            string strCurDemPath, strCurDomPath;
//            strCurDemPath.assign( strLCurPath, 0, nTempCPos );
//            strCurDemPath.append( "/" );
//            strCurDemPath.append( pStereoSet->imgLInfo.strName );
//            strCurDemPath.append( "-" );
//            strCurDemPath.append( pStereoSet->imgRInfo.strName );
//            strCurDomPath = strCurDemPath;
//            strCurDemPath.append( "-Dem.tif" );
//            strCurDomPath.append( "-Dom.tif" );
//            this->GetStereoFeatPts( pStereoSet, strLCurPath, strRCurPath, vecSitePts );
//
//            SINT kk;
//            kk = vecSitePts.size();
//
//            DbRect dbDemRect;
//            dbDemRect.left = ptLT.X;
//            dbDemRect.right = ptRB.X;
//            dbDemRect.top = ptLT.Y;
//            dbDemRect.bottom = ptRB.Y;
//
//            DbRect rectMin = getPtRect( vecSitePts, dbDemRect );
//
//            char cTemp[128];
//            strcpy( cTemp, strCurDemPath.c_str() );
//
////            this->mlWriteToGeoFile( vecSitePts, cTemp, rectMin , dResolution, delOff );
////            this->mlWriteToGeoFile( vecSitePts, cTemp, dResolution, delOff );
//            this->paraInit() ;
////
////            this->mlWriteToGeoFile( vecSitePts, strDemPath, );
////            (vector<Pt3d>& vec3DPts , SCHAR* strGeoFilePath , DbRect dbDemRect , DOUBLE dRes = 1.0 , delErrorType bType = delOn
//
//        }
//
//    }
//    else
//    {
//
//    }

    return true;
}
/**
* @fn MapByInteBlock
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 单站一体化制图
* @param vecStereoSet 立体影像数据信息
* @param vecStrMatchFile 匹配点文件
* @param ptLT 制图区域左上角坐标
* @param ptRB 制图区域右下角坐标
* @param dResolution 分辨率
* @param strDemPath Dem生成路径
* @param strDomPath Dom生成路径
* @param bIsUsingFeatPt 判断是否使用匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlSiteMapping::MapByInteBlock(   vector<StereoSet> &vecStereoSet, vector< ImgPtSet > &vecImgPtSets, Pt2d ptLT, Pt2d ptRB, DOUBLE dResolution,\
                                       ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                                       string strDemPath, string strDomPath, bool bIsUsingFeatPt )
{
////////////////////////////////////////////////////
    if( bIsUsingFeatPt == true )
    {

        CmlPhgProc clsPhgProc;

        vector<Pt3d> vecSitePts, vecTempHoles;vector<Polygon3d> vecRegions;
        for( UINT i = 0; i < vecStereoSet.size(); ++i )
        {
            vector<Pt3d> vecTmpPts;
            StereoSet* pStereoSet = &vecStereoSet[i];
            Pt3d ptMinDis;
            this->GetStereoFeatPts( pStereoSet, extractPtsOpts, matchOpts, ransacOpts, mFilterOpts, vecImgPtSets[2*i],  vecImgPtSets[2*i+1], vecTmpPts, ptMinDis );
            vecTempHoles.push_back( ptMinDis );

            vecSitePts.insert( vecSitePts.end(), vecTmpPts.begin(), vecTmpPts.end() );
            Polygon3d poly;
            clsPhgProc.CalcConvexHull( vecTmpPts, poly.vecVectex );

            poly.nID = i;

           vecRegions.push_back( poly );

        }

        vector<Pt2d> vec2dTemp, vecHoles;

        for( UINT i = 0; i < vecTempHoles.size(); ++i )
        {
            Pt2d ptTCur;
            ptTCur.X = vecTempHoles[i].X;
            ptTCur.Y = vecTempHoles[i].Y;
            vec2dTemp.push_back( ptTCur );
        }

        clsPhgProc.CalcConvexHull(vec2dTemp, vecHoles );

        DbRect dbDemRect;
        dbDemRect.left = ptLT.X;
        dbDemRect.right = ptRB.X;
        dbDemRect.top = ptLT.Y;
        dbDemRect.bottom = ptRB.Y;
        char cTemp[128];
        strcpy( cTemp, strDemPath.c_str() );
        // 生产 DEM
        CmlDemProc demDemo ;
//       demDemo.m_vecHoles = vecHoles;
//        if( false == demDemo.mlWriteToGeoFile(vecSitePts , dbDemRect , dResolution , cTemp, T_Float32 ) )
        if( false == demDemo.mlWriteRegionToGeoFile( vecSitePts, dbDemRect, dResolution, vecRegions, cTemp, T_Float32 ) )
        {
            return false;
        }
        // 生成dom
        // 如果生成DEM成功， 则读取Geotiff格式的DEM文件， 得到DOM文件
        CmlDomProc domDemo ;
        if( false == domDemo.createOrthoImage(vecStereoSet, strDemPath , strDomPath, T_Byte ) )
        {
            return false;
        }
    }
    return true;
}

/**
* @fn DisMapByMultiBlock
* @date 2011.11.02
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 单对影像生成视差图
* @param vecStereoSet 立体影像数据信息
* @param vecStrMatchFile 匹配点文件
* @param nGrid 视差图生成格网大小
* @param nRowRadius 行方向搜索半径
* @param nColRadius 列方向搜索半径
* @param nTempleSize 模板大小
* @param strDisPath 视差图生成路径
* @param bIsUsingFeatPt 判断是否使用匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
//bool CmlSiteMapping::DisMapByBlock( vector<StereoSet> &vecStereoSet, vector<string> &vecStrMatchFile, UINT nGrid, UINT nRowRadius,UINT nColRadius,UINT nTempleSize,DOUBLE dCoef, bool bIsUsingFeatPt )
//{
//
//
//    //读取文件，如果没有密集文件就密集匹配，然后进行写文件
//    if( bIsUsingFeatPt == false )
//    {
//
//        for( int i = 0; i < vecStereoSet.size(); ++i )
//        {
//            vector<Pt3d> vecSitePts;
//            StereoSet* pStereoSet = &vecStereoSet.at(i);
//
//            string strLCurPath = vecStrMatchFile[2*i];
//            string strRCurPath = vecStrMatchFile[2*i+1];
//
//            int nTempCPos = strLCurPath.rfind( "/" );
//            this->GetStereoDensePts(pStereoSet, strLCurPath, strRCurPath, vecSitePts, nGrid, nRowRadius, nColRadius, nTempleSize,dCoef);
//
////            int kk = vecSitePts.size();
////
////            DbRect dbDemRect;
////            dbDemRect.left = ptLT.X;
////            dbDemRect.right = ptRB.X;
////            dbDemRect.top = ptLT.Y;
////            dbDemRect.bottom = ptRB.Y;
////
////            DbRect rectMin = getPtRect( vecSitePts, dbDemRect );
////
////            char cTemp[128];
////            strcpy( cTemp, strCurDemPath.c_str() );
////
//////            this->mlWriteToGeoFile( vecSitePts, cTemp, rectMin , dResolution, delOff );
////            this->mlWriteToGeoFile( vecSitePts, cTemp, dResolution, delOff );
////            this->paraInit() ;
////
////            this->mlWriteToGeoFile( vecSitePts, strDemPath, );
////            (vector<Pt3d>& vec3DPts , SCHAR* strGeoFilePath , DbRect dbDemRect , DOUBLE dRes = 1.0 , delErrorType bType = delOn
//
//        }
//
//    }
//    else
//    {
//
//    }
//
//}

//bool CmlSiteMapping::mlMapByOneRoll(   vector<string> &vecStrL, vector<string> &vecStrR, vector<bool> &vecbIsEpiImg, vector<InOriPara> &vecInOriL, vector<InOriPara> &vecInOriR, \
//                                    vector<ExOriPara> &vecExOriL, vector<ExOriPara> &vecExOriR, bool bIsUsingDenseMatch  )
//{
//    vector<CmlFrameImage> vecImgL;
//    vector<CmlFrameImage> vecImgR;
//    vector<Pt3d> vecSite3dPts;
//
//    for( SINT i = 0; i < vecStrL.size(); ++i )
//    {
//        CmlFrameImage imgTempL, imgTempR;
//        char cL[200], cR[200];
//        strcpy( cL, vecStrL[i].c_str() );
//        strcpy( cR, vecStrR[i].c_str() );
//        imgTempL.LoadFile( cL );
//        imgTempL.m_InOriPara = vecInOriL[i];
//        imgTempL.m_ExOriPara = vecExOriL[i];
//
//        imgTempR.LoadFile( cR );
//        imgTempL.m_InOriPara = vecInOriL[i];
//        imgTempL.m_ExOriPara = vecExOriL[i];
//
//        imgTempL.SmoothByGuassian( 9, 0.8 );
//        imgTempR.SmoothByGuassian( 9, 0.8 );
//
//
//        CmlStereoProc stereoProc;
//        stereoProc.m_pDataL = &imgTempL;
//        stereoProc.m_pDataR = &imgTempR;
//        if( vecbIsEpiImg[i] == false )
//        {
//            imgTempL.GetUnDistortImg();
//            imgTempR.GetUnDistortImg();
//            stereoProc.mlGetEpipolarImg( );
//        }
//        SINT nPtSize = imgTempL.GetHeight() * imgTempL.GetWidth() / 4000;
//        imgTempL.ExtractFeatPtByForstner( 5, nPtSize );
//        imgTempR.ExtractFeatPtByForstner( 5, nPtSize );
//
//        MLRect rectResearch;
//        rectResearch.dXMax = 5;
//        rectResearch.dXMin = -imgTempL.m_InOriPara.f*0.2;
//        rectResearch.dYMax = 2;
//        rectResearch.dYMin = -2;
//        if( bIsUsingDenseMatch == false )
//        {
//
//        }
//        else
//        {
//            vector<Pt3d> vec3dPts;
//            stereoProc.mlTemplateMatchInFeatPt( rectResearch, 15 );
//            stereoProc.mlGetRansacPts( stereoProc.m_vecFeatMatchPt );
//            stereoProc.mlInterSectionInFrameImg( stereoProc.m_vecFeatMatchPt, vec3dPts );
//
//            for( SINT i = 0; i < vec3dPts.size(); ++i )
//            {
//                vecSite3dPts.push_back( vec3dPts[i]);
//            }
//        }
//
//
//
//    }
//
//}
/**
* @fn mlSiteParaInit
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 单站地形建立参数初始化
* @param dbRect 文件路径
* @param dRes 分辨率
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
//bool CmlSiteMapping::mlSiteParaInit(DbRect dbRect , DOUBLE dRes)
//{
//    // 判断 制定dbRect 的坐标方位关系
//    if((dbRect.left >= dbRect.right) || (dbRect.bottom >= dbRect.top))
//    {
//        SCHAR strErr[] = "mlSiteParaInit error : not correct dem coordinate type\n" ;
//        LOGAddErrorMsg(strErr) ;
//        return false ;
//    }
//    else
//    {
//        m_dbDemRect.left = MAX(m_dbDemRect.left , dbRect.left) ;
//        m_dbDemRect.top = MIN(m_dbDemRect.top , dbRect.top) ;
//        m_dbDemRect.right = MIN(m_dbDemRect.right , dbRect.right) ;
//        m_dbDemRect.bottom = MAX(m_dbDemRect.bottom , dbRect.bottom) ;
//        // 判断矩形是否符合要求
//        if((m_dbDemRect.left > m_dbDemRect.right) ||(m_dbDemRect.bottom > m_dbDemRect.top))
//        {
//            SCHAR strErr[] = "mlSiteParaInit error : not correct dem coordinate type\n" ;
//            LOGAddErrorMsg(strErr) ;
//            return false ;
//        }
//        m_nW = (UINT)((m_dbDemRect.right - m_dbDemRect.left) / m_dDemRes) ;
//        m_nH = (UINT)((m_dbDemRect.top - m_dbDemRect.bottom) / m_dDemRes) ;
//        m_dbDemRect.right = m_dbDemRect.left +  m_nW * m_dDemRes ;
//        m_dbDemRect.bottom = m_dbDemRect.top - m_nH * m_dDemRes ;
//        m_dDemRes = dRes ;
//        return true ;
//    }
//}
///**
//* @fn
//* @date 2011.11.02
//* @author 吴凯 wukai@irsa.ac.cn
//* @brief 三维点云生成指定范围DEM，并存入geotiff文件
//* @param vec3DPts 三维点云
//* @param strGeoFilePath 文件路径
//* @param dbDemRect DEM生成范围
//* @param dRes DEM分辨率
//* @param bType 是否剔除粗差点
//* @retval TRUE 成功
//* @retval FALSE 失败
//* @version 1.0
//* @par 修改历史：
//* <作者>    <时间>   <版本编号>    <修改原因>\n
//*/
//bool CmlSiteMapping::mlWriteToGeoFile(vector<Pt3d>& vec3DPts , SCHAR* strGeoFilePath , DbRect dbDemRect , DOUBLE dRes , delErrorType bType)
//{
//    if(dRes < 0)
//    {
//        dRes = -dRes ;
//    }
//    CmlDemProc demProc ;
//    UINT nSize = vec3DPts.size();
//    if( nSize )
//    {
//        demProc.m_vec3DPts = vec3DPts;
//        getRangeFromPts(vec3DPts) ; // 判断所有点云的范围
//        m_dDemRes = dRes ;
//        if(!mlSiteParaInit(dbDemRect , dRes)) // 指定矩形范围与点云范围求交来确定dem的范围
//        {
//            SCHAR strErr[] = "mlWriteToGeoFile error : No Intersection area\n" ;
//            LOGAddErrorMsg(strErr) ;
//            return false ;
//        }
//        // 对于欲求的地形范围增加缓冲区
//        DbRect dbBufferRect ;
//        SINT nBufferNum = (SINT)(BUFFER_RADIUS / m_dDemRes) ;
////        dbBufferRect.left = m_dbDemRect.left - nBufferNum * m_dDemRes ;
////        dbBufferRect.right = m_dbDemRect.right + nBufferNum * m_dDemRes ;
////        dbBufferRect.top = m_dbDemRect.top + nBufferNum * m_dDemRes;
////        dbBufferRect.bottom = m_dbDemRect.bottom - nBufferNum * m_dDemRes ;
//        Pt2d ptBufferLeft ;
//        ptBufferLeft.X = m_dbDemRect.left - nBufferNum * m_dDemRes ;
//        ptBufferLeft.Y = m_dbDemRect.top + nBufferNum * m_dDemRes;
//        demProc.mlDemParaInit(ptBufferLeft ,m_nW , m_nH , nBufferNum , m_dDemRes) ;
//        if(!demProc.mlRawPts2DemPts(bType)||!demProc.mlCreateDemFrom3DPts()) // 处理点云
//        {
//            SCHAR strErr[] = "mlWriteToGeoFile error : Failed to process 3D points\n" ;
//            LOGAddErrorMsg(strErr) ;
//            return false ;
//        }
//        Pt2d ptOrigin ;
//        ptOrigin.X = m_dbDemRect.left ;
//        ptOrigin.Y = m_dbDemRect.top ;
//        DOUBLE* m_pDemValTemp = NULL;
//        DOUBLE* m_pDemVal = NULL;
//        if(!(m_pDemValTemp = new DOUBLE[m_nW * m_nH] ))
//        {
//            SCHAR strErr[] = "mlWriteToGeoFile error : memory allocation failed \n" ;
//            LOGAddErrorMsg(strErr) ;
//            return false ;
//        }
//        else
//        {
//            for(UINT i = 0 ; i < m_nH ; i++)
//            {
//                for(UINT j = 0 ; j < m_nW ; j++)
//                {
//                    m_pDemValTemp[i * m_nW + j] = DEM_NO_DATA ;
//                }
//            }
//            // 裁剪缓冲区
//            for(UINT i = 0 ; i < m_nH ; i++)
//            {
//                for(UINT j = 0 ; j < m_nW ; j++)
//                {
//                    m_pDemValTemp[i * m_nW + j] = demProc.m_pDem[(i+nBufferNum)*demProc.m_nW + j + nBufferNum] ;
//                }
//            }
//        }
//        if(!(m_pDemVal = new DOUBLE[m_nW * m_nH] ))
//        {
//            SCHAR strErr[] = "mlWriteToGeoFile error : memory allocation failed \n" ;
//            LOGAddErrorMsg(strErr) ;
//            return false ;
//        }
//        else
//        {
//            for(UINT i = 0 ; i < m_nH ; i++)
//            {
//                for(UINT j = 0 ; j < m_nW ; j++)
//                {
//                    m_pDemVal[i * m_nW + j] = m_pDemValTemp[(m_nH -1 -i) * m_nW + j] ;
//                }
//            }
//        }
//        CmlGeoRaster geoRaster ;
//        if(!geoRaster.CreateGeoFile(strGeoFilePath , ptOrigin , m_dDemRes , -m_dDemRes , m_nH , m_nW , 1 , GDT_Float64 , DEM_NO_DATA)
//                || !geoRaster.SaveToGeoFile(m_pDemVal))
//        {
//            SCHAR strErr[] = "mlWriteToGeoFile error : Failed to save geoTiff file\n" ;
//            LOGAddErrorMsg(strErr) ;
//            return false ;
//        }
//        if(m_pDemVal)
//        {
//            delete [] m_pDemVal ;
//        }
//        if(m_pDemValTemp)
//        {
//            delete [] m_pDemValTemp ;
//        }
//        LOGAddSuccessQuitMsg() ;
//        return true ;
//    }
//    else
//    {
//        SCHAR strErr[] = "mlWriteToGeoFile error : Null points \n" ;
//        LOGAddErrorMsg(strErr) ;
//        return false ;
//    }
//}
///**
//* @fn
//* @date 2011.11.02
//* @author 吴凯 wukai@irsa.ac.cn
//* @brief 三维点云全部生成DEM，并存入geotiff文件
//* @param vec3DPts 三维点云
//* @param strGeoFilePath 文件路径
//* @param dRes DEM分辨率
//* @param bType 是否剔除粗差点
//* @retval TRUE 成功
//* @retval FALSE 失败
//* @version 1.0
//* @par 修改历史：
//* <作者>    <时间>   <版本编号>    <修改原因>\n
//*/
//bool CmlSiteMapping::mlWriteToGeoFile(vector<Pt3d>& vec3DPts , SCHAR* strGeoFilePath , DOUBLE dRes , delErrorType bType)
//{
//    if(dRes < 0)
//    {
//        dRes = -dRes ;
//    }
//    CmlDemProc demProc ;
//    UINT nSize = vec3DPts.size();
//    if( nSize )
//    {
////        if(demProc.m_vec3DPts.size() != vec3DPts.size()) //  方便连续调用处理地形，清除上一站数据缓存
////        {
//        demProc.m_vec3DPts.clear() ;
//        for (UINT i = 0 ; i < nSize ; i++)
//        {
//            Pt3d ptTemp = vec3DPts[i] ;
//            demProc.m_vec3DPts.push_back(ptTemp)  ;
//        }
////        }
//        getRangeFromPts(vec3DPts) ;
//        m_dDemRes = dRes ;
//        demProc.mlDemParaInit(m_dbDemRect , m_dDemRes) ;
//        if(!demProc.mlRawPts2DemPts(bType)||!demProc.mlCreateDemFrom3DPts()) // 处理点云
//        {
//            SCHAR strErr[] = "mlWriteToGeoFile error : Failed to process 3D points\n" ;
//            LOGAddErrorMsg(strErr) ;
//        }
//        Pt2d ptOrigin ;
//        ptOrigin.X = m_dbDemRect.left ;
//        ptOrigin.Y = m_dbDemRect.top ;
//        m_nH = demProc.m_nH ;
//        m_nW = demProc.m_nW ;
//        DOUBLE* m_pDemVal = NULL;
//        if(!(m_pDemVal = new DOUBLE[m_nW * m_nH] ))
//        {
//            SCHAR strErr[] = "mlWriteToGeoFile error : memory allocation failed \n" ;
//            LOGAddErrorMsg(strErr) ;
//            return false ;
//        }
//        else
//        {
//            for(UINT i = 0 ; i < m_nH ; i++)
//            {
//                for(UINT j = 0 ; j < m_nW ; j++)
//                {
//                    m_pDemVal[i * m_nW + j] = demProc.m_pDem[(m_nH -1 -i) * m_nW + j] ;
//                }
//            }
//        }
//        CmlGeoRaster geoRaster ;
//        if(!geoRaster.CreateGeoFile(strGeoFilePath , ptOrigin , m_dDemRes , -m_dDemRes , demProc.m_nH , demProc.m_nW , 1 , GDT_Float64 , DEM_NO_DATA)
//                || !geoRaster.SaveToGeoFile(m_pDemVal))
//        {
//            SCHAR strErr[] = "mlWriteToGeoFile error : Failed to save geoTiff file\n" ;
//            LOGAddErrorMsg(strErr) ;
//            return false ;
//        }
//        if(m_pDemVal)
//        {
//            delete [] m_pDemVal ;
//        }
//        LOGAddSuccessQuitMsg() ;
//        return true ;
//    }
//    else
//    {
//        SCHAR strErr[] = "mlWriteToGeoFile error : Null points \n" ;
//        LOGAddErrorMsg(strErr) ;
//        return false ;
//    }
//}
///**
//* @fn getRangeFromPts
//* @date 2011.11.02
//* @author 吴凯 wukai@irsa.ac.cn
//* @brief 计算三维点云范围
//* @param vec3DPts 三维点云
//* @retval TRUE 成功
//* @retval FALSE 失败
//* @version 1.0
//* @par 修改历史：
//* <作者>    <时间>   <版本编号>    <修改原因>\n
//*/
//bool CmlSiteMapping::getRangeFromPts(vector<Pt3d>& vec3DPts)
//{
//    SINT nNum ;
//    if(!(nNum = vec3DPts.size()))
//    {
//        SCHAR strErr[] = "getRangeFromPts error : 3d points clouds is Null" ;
//        LOGAddErrorMsg(strErr) ;
//        return false ;
//    }
//    Pt3d ptTemp ;
//    for(SINT i = 0 ; i < nNum ; i++)
//    {
//        ptTemp = vec3DPts[i] ;
//        m_dbDemRect.left = MIN(ptTemp.X , m_dbDemRect.left) ;
//        m_dbDemRect.right = MAX(ptTemp.X , m_dbDemRect.right) ;
//        m_dbDemRect.bottom = MIN(ptTemp.Y , m_dbDemRect.bottom) ;
//        m_dbDemRect.top = MAX(ptTemp.Y , m_dbDemRect.top) ;
//    }
//    return true ;
//}
//bool CmlSiteMapping::WriteToGeoFile(vector<Pt3d>& vec3DPts , SINT nXOrigin , SINT nYOrigin , SINT nBlockWidth , SINT nBlockHeight , SCHAR* strGeoFilePath)
//{
//    return true ;
//}

bool CmlSiteMapping::WriteDisMap(vector<Pt3d> vecPt3ds, char* DisPath)
{
    return false;
//       this->mlWriteToGeoFile( vecPt3ds, DisPath, 1, delOff);
}
/**
* @fn GetStereoDensePts
* @date 2011.11.02
* @author 彭嫚
* @brief 立体影像对特征点匹配，密集匹配，计算三维点云，存入文件
* @param pStereoSet 立体影像数据信息
* @param WidePara 立体影像匹配参数
* @param ptfSetL 左影像特征匹配点
* @param ptfSetR 右影像特征匹配点
* @param ptSetL 左影像密集匹配点
* @param ptSetR 右影像密集匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlSiteMapping::GetStereoDensePts( StereoSet* pStereoSet,  WideOptions WidePara, ImgPtSet& ptfSetL, ImgPtSet& ptfSetR ,ImgPtSet& ptSetL, ImgPtSet& ptSetR )
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc clsStereoProc;

    if(clsImgL.LoadFile( pStereoSet->imgLInfo.strImgPath.c_str() )==false)
    {
        return false;
    }
    clsImgL.m_InOriPara = pStereoSet->imgLInfo.inOri;
    clsImgL.m_ExOriPara = pStereoSet->imgLInfo.exOri;
    clsImgL.SmoothByGuassian(  7, 0.6 );
    clsImgL.ExtractFeatPtByForstner( 5 );

    if(clsImgR.LoadFile( pStereoSet->imgRInfo.strImgPath.c_str() )==false)
    {
        return false;
    }
    clsImgR.m_InOriPara = pStereoSet->imgRInfo.inOri;
    clsImgR.m_ExOriPara = pStereoSet->imgRInfo.exOri;
    clsImgR.SmoothByGuassian(  7, 0.6 );
    clsImgR.ExtractFeatPtByForstner( 5 );

    clsStereoProc.m_pDataL = &clsImgL;
    clsStereoProc.m_pDataR = &clsImgR;

    Pt3d pPosL = pStereoSet->imgLInfo.exOri.pos;
    Pt3d pPosR = pStereoSet->imgRInfo.exOri.pos;
    double dBaseWidth = sqrt( (pPosL.X - pPosR.X)*(pPosL.X - pPosR.X)+(pPosL.Y - pPosR.Y)*(pPosL.Y - pPosR.Y)+(pPosL.Z - pPosR.Z)*(pPosL.Z - pPosR.Z) );

    if( pStereoSet->nStereoLevel ==  2)//为核线影像
    {
        MLRect rectSearch;
        rectSearch.dYMax = 2;
        rectSearch.dYMin = -2;
        rectSearch.dXMax = 5;
        rectSearch.dXMin = -1.0 * pStereoSet->imgLInfo.inOri.f *dBaseWidth / 2.0;
//实验图像无外方位无参数
//        rectSearch.dXMin = -500;
//        rectSearch.dXMax = 500;
//        rectSearch.dYMin = -30;
//        rectSearch.dYMax =30;

        clsStereoProc.mlTemplateMatchInFeatPt( rectSearch, 17 );

        clsStereoProc.mlGetRansacPts( clsStereoProc.m_vecFeatMatchPt );

        clsStereoProc.mlFilterPtsByMedian( clsStereoProc.m_vecFeatMatchPt );

        vector<Pt2d> vecFPtsL, vecFPtsR;
        Pt2d tempfLPt, tempfRPt;
        if(clsStereoProc.m_vecFeatMatchPt.size()>0)
        {
            ptfSetL.imgInfo = pStereoSet->imgLInfo;
            ptfSetR.imgInfo = pStereoSet->imgRInfo;
            for(UINT kk=0; kk< clsStereoProc.m_vecFeatMatchPt.size(); kk++)
            {
                tempfLPt.X = clsStereoProc.m_vecFeatMatchPt[kk].ptLInImg.X;
                tempfLPt.Y = clsStereoProc.m_vecFeatMatchPt[kk].ptLInImg.Y;
                tempfLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + kk + 1;
                tempfLPt.byType = 1;
                ptfSetL.vecPts.push_back(tempfLPt);
                tempfRPt.X = clsStereoProc.m_vecFeatMatchPt[kk].ptRInImg.X;
                tempfRPt.Y = clsStereoProc.m_vecFeatMatchPt[kk].ptRInImg.Y;
                tempfRPt.lID = tempfLPt.lID;
                tempfRPt.byType = 1;
                ptfSetR.vecPts.push_back(tempfRPt);
            }

        }
        else
        {
            return false;
        }

        vector<Pt2d> vecDPtsL, vecDPtsR;

        clsStereoProc.mlDenseMatch(&clsImgL.m_DataBlock, &clsImgR.m_DataBlock, clsStereoProc.m_vecFeatMatchPt , WidePara, vecDPtsL, vecDPtsR);
        int nNum = vecDPtsL.size();
        ptSetL.imgInfo = pStereoSet->imgLInfo;
        ptSetR.imgInfo = pStereoSet->imgRInfo;

        Pt2d tempLPt, tempRPt;
        for(int k=0; k<nNum; k++)
        {
            tempLPt.X = vecDPtsL[k].X;
            tempLPt.Y = vecDPtsL[k].Y;
            tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
            tempLPt.byType = 1;
            tempRPt.X = vecDPtsR[k].X;
            tempRPt.Y = vecDPtsR[k].Y;
            tempRPt.lID = tempLPt.lID;
            tempRPt.byType = 1;
            ptSetL.vecPts.push_back(tempLPt);
            ptSetR.vecPts.push_back(tempRPt);
        }

        return true;
    }
    else
    {

        //实验图像无外方位无参数
        MLRect rectSearch;
        rectSearch.dXMin = -500;
        rectSearch.dXMax = 500;
        rectSearch.dYMin = -30;
        rectSearch.dYMax =30;

        clsStereoProc.mlTemplateMatchInFeatPt( rectSearch, 17 );

        clsStereoProc.mlGetRansacPts( clsStereoProc.m_vecFeatMatchPt );

        clsStereoProc.mlFilterPtsByMedian( clsStereoProc.m_vecFeatMatchPt );

        vector<Pt2d> vecFPtsL, vecFPtsR;
        Pt2d tempfLPt, tempfRPt;
        if(clsStereoProc.m_vecFeatMatchPt.size()>0)
        {
            ptfSetL.imgInfo = pStereoSet->imgLInfo;
            ptfSetR.imgInfo = pStereoSet->imgRInfo;
            for(UINT kk=0; kk< clsStereoProc.m_vecFeatMatchPt.size(); kk++)
            {
                tempfLPt.X = clsStereoProc.m_vecFeatMatchPt[kk].ptLInImg.X;
                tempfLPt.Y = clsStereoProc.m_vecFeatMatchPt[kk].ptLInImg.Y;
                tempfLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + kk + 1;
                tempfLPt.byType = 1;
                ptfSetL.vecPts.push_back(tempfLPt);
                tempfRPt.X = clsStereoProc.m_vecFeatMatchPt[kk].ptRInImg.X;
                tempfRPt.Y = clsStereoProc.m_vecFeatMatchPt[kk].ptRInImg.Y;
                tempfRPt.lID = tempfLPt.lID;
                tempfRPt.byType = 1;
                ptfSetR.vecPts.push_back(tempfRPt);
            }

        }
        else
        {
            return false;
        }

        vector<Pt2d> vecDPtsL, vecDPtsR;

        clsStereoProc.mlDenseMatch(&clsImgL.m_DataBlock, &clsImgR.m_DataBlock, clsStereoProc.m_vecFeatMatchPt , WidePara, vecDPtsL, vecDPtsR);
        int nNum = vecDPtsL.size();
        ptSetL.imgInfo = pStereoSet->imgLInfo;
        ptSetR.imgInfo = pStereoSet->imgRInfo;

        Pt2d tempLPt, tempRPt;
        for(int k=0; k<nNum; k++)
        {
            tempLPt.X = vecDPtsL[k].X;
            tempLPt.Y = vecDPtsL[k].Y;
            tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
            tempLPt.byType = 1;
            tempRPt.X = vecDPtsR[k].X;
            tempRPt.Y = vecDPtsR[k].Y;
            tempRPt.lID = tempLPt.lID;
            tempRPt.byType = 1;
            ptSetL.vecPts.push_back(tempLPt);
            ptSetR.vecPts.push_back(tempRPt);
        }

        return true;
        /////////////////
    }

}
bool CmlSiteMapping::BA( vector<StereoSet> vecStereoSetIn, vector< ImgPtSet > &vecImgPtSets, \
                         ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                         vector<StereoSet> &vecStereoOut )
{
    //////////////////////

//    if( false == FindTiePoints( vecStereoSetIn, vecImgPtSets, extractPtsOpts, matchOpts, ransacOpts, mFilterOpts ) )
//    {
//        return false;
//    }
    if( false == BASolve( vecStereoSetIn, vecImgPtSets, vecStereoOut, 2 ) )
    {
        return false;
    }
//    vector< vector<Pt3d> > vecVecPts;
//    for( UINT i = 0; i < vecImgPtSets.size(); i=i+2 )
//    {
//        ImgPtSet imgCurLSet = vecImgPtSets[i];
//        ImgPtSet imgCurRSet = vecImgPtSets[i+1];
//        CmlFrameImage clsFrmImgL, clsFrmImgR;
//        if( false == clsFrmImgL.LoadFile( imgCurLSet.imgInfo.strImgPath.c_str() ) )
//        {
//            continue;
//        }
//        clsFrmImgR.LoadFile( imgCurRSet.imgInfo.strImgPath.c_str() );
//        vector<Pt3d> vecPts;
//        for( UINT j = 0; j < imgCurLSet.vecPts.size(); ++j )
//        {
//            Pt2d ptRes;
//            Pt2d ptCur = imgCurLSet.vecPts[j];
//            Pt2i ptTemp;
//            ptTemp.X = int( ptCur.X+0.1);
//            ptTemp.Y = int( ptCur.Y+0.1);
//            CmlStereoProc clsStereoProc;
//            Pt2i ptResTemp;DOUBLE dCoef;
//            if( false == clsStereoProc.mlTemplateMatchInRegion( &clsFrmImgL.m_DataBlock, &clsFrmImgR.m_DataBlock, ptTemp, ptResTemp, dCoef, -200, 0, -2, 2, 17 ) )
//            {
//                continue;
//            }
//            ptRes.X = ptResTemp.X;
//            ptRes.Y = ptResTemp.Y;
//
//            clsStereoProc.mlLsMatch( &clsFrmImgL.m_DataBlock, &clsFrmImgR.m_DataBlock, ptTemp.X, ptTemp.Y, ptRes.X, ptRes.Y, 9, dCoef);
//            Pt3d ptXYZ;
//            if( true == clsStereoProc.mlInterSection( ptCur, ptRes, 1024, 1024, ptXYZ, &imgCurLSet.imgInfo.inOri, &imgCurLSet.imgInfo.exOri, &imgCurRSet.imgInfo.inOri, &imgCurRSet.imgInfo.exOri)  )
//            {
//                ptXYZ.lID = ptCur.lID;
//                vecPts.push_back( ptXYZ );
//            }
//
//
//        }
//        vecVecPts.push_back( vecPts );
//    }
//    for( UINT i = 0; i < (vecVecPts.size()-1); ++i )
//    {
//        vector<Pt3d> vecOldPts, vecNewPts;
//        ExOriPara exInit;
//        for( UINT j = 0; j < vecVecPts[i].size(); ++j )
//        {
//            Pt3d pt1 = vecVecPts[i].at(j);
//            for( UINT k = 0; k < vecVecPts[i+1].size(); ++k )
//            {
//                Pt3d pt2 = vecVecPts[i+1].at(k);
//                if( pt1.lID == pt2.lID )
//                {
//                    vecOldPts.push_back( pt1 );
//                    vecNewPts.push_back( pt2 );
//                }
//            }
//        }
//        CmlPhgProc clsPhg;
//        clsPhg.mlSolvePts( vecOldPts, vecNewPts, (UINT)(10), &exInit );
//
//    }



    return true;
}
bool CmlSiteMapping::FindTiePoints( vector<StereoSet> vecStereoSetIn, vector< ImgPtSet > &vecImgPtSets, \
                                    ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts )
{
    if( vecStereoSetIn.size() <= 1 )
    {
        return false;
    }
    if( vecImgPtSets.size() != (2*vecStereoSetIn.size()))
    {
        return false;
    }

    for( UINT i = 0; i < (vecStereoSetIn.size() - 1); ++i )
    {
        StereoSet stereoSI = vecStereoSetIn[i];
        for( UINT j = i+1; j < vecStereoSetIn.size(); ++j )
        {
            StereoSet stereoSJ = vecStereoSetIn[j];
            DOUBLE dXOff, dYOff;
            dXOff = dYOff = 0.0;
            if( true == judgeIsOverlap( &stereoSI.imgLInfo, &stereoSJ.imgLInfo, dXOff, dYOff ) )
            {
                CmlPtsManage clsPtManage;
                vector<StereoMatchPt> stereoPts;
                if( false == clsPtManage.GetPairPts( vecImgPtSets[2*i], vecImgPtSets[2*j], stereoPts) )
                {
                    CmlStereoProc clsStereoProc;
                    CmlFrameImage imgL, imgR;
                    if( ( false == imgL.LoadFile( stereoSI.imgLInfo.strImgPath.c_str() ) ) || ( false == imgR.LoadFile( stereoSJ.imgLInfo.strImgPath.c_str() ) )  )
                    {
                        continue;
                    }
                    imgL.ExtractFeatPtByForstner( extractPtsOpts.nGridSize, extractPtsOpts.nPtMaxNum, extractPtsOpts.dThresCoef );
                    imgR.ExtractFeatPtByForstner( extractPtsOpts.nGridSize, extractPtsOpts.nPtMaxNum, extractPtsOpts.dThresCoef );

                    imgL.SmoothByGuassian( 7, 0.8 );
                    imgR.SmoothByGuassian( 7, 0.8 );
                    clsStereoProc.m_pDataL = &imgL;
                    clsStereoProc.m_pDataR = &imgR;

                    MLRect rectSearch;
                    rectSearch.dXMax = matchOpts.dXMax;
                    rectSearch.dXMin = matchOpts.dXMin;
                    rectSearch.dYMax = matchOpts.dYMax;
                    rectSearch.dYMin = matchOpts.dYMin;
                    dXOff += matchOpts.dXOff;
                    dYOff += matchOpts.dYOff;

                    if( false == clsStereoProc.mlTemplateMatchInFeatPt( rectSearch, matchOpts.nTempSize, matchOpts.dCoefThres, dXOff, dYOff ) )
                    {
                        continue;
                    }
                    clsStereoProc.mlGetRansacPts( clsStereoProc.m_vecFeatMatchPt, ransacOpts.dThres, ransacOpts.dConfidence );

                    clsStereoProc.mlFilterPtsByMedian( clsStereoProc.m_vecFeatMatchPt, mFilterOpts.nFilterSize, mFilterOpts.dThresCoef, mFilterOpts.nThres );

//                    getRanSacPts( )

                    stereoPts = clsStereoProc.m_vecFeatMatchPt;

                    ImgPtSet imgTempLL, imgTempRL;
                    if( false == clsPtManage.SplitPairPts( stereoSI.imgLInfo, stereoSJ.imgLInfo, stereoPts, imgTempLL, imgTempRL ))//vecImgPtSets[2*i], vecImgPtSets[2*j]
                    {
                        continue;
                    }
                    /////////////////////////////////////////////////////////
                    MatchInRegPara matchPara;
                    CmlFrameImage clsTempImgL, clsTempImgR, clsTempImgLT, clsTempImgRT;
                    if( ( false == (clsTempImgL.LoadFile( stereoSI.imgLInfo.strImgPath.c_str()))) || \
                            ( false == (clsTempImgR.LoadFile( stereoSI.imgRInfo.strImgPath.c_str()))) )
                    {
                        continue;
                    }
                    if( ( false == (clsTempImgLT.LoadFile( stereoSJ.imgLInfo.strImgPath.c_str()))) || \
                            ( false == (clsTempImgRT.LoadFile( stereoSJ.imgRInfo.strImgPath.c_str()))) )
                    {
                        continue;
                    }
                    if( false == clsTempImgR.ExtractFeatPtByForstner( extractPtsOpts.nGridSize, extractPtsOpts.nPtMaxNum, extractPtsOpts.dThresCoef ) )
                    {
                        continue;
                    }
                    if( false == clsTempImgRT.ExtractFeatPtByForstner( extractPtsOpts.nGridSize, extractPtsOpts.nPtMaxNum, extractPtsOpts.dThresCoef ) )
                    {
                        continue;
                    }
                    ImgPtSet* pTempImgPtsetLL = &vecImgPtSets[2*i];
                    ImgPtSet* pTempImgPtsetLR = &vecImgPtSets[2*i+1];
                    ImgPtSet* pTempImgPtsetRL = &vecImgPtSets[2*j];
                    ImgPtSet* pTempImgPtsetRR = &vecImgPtSets[2*j+1];

                    vector<StereoMatchPt> vecTempMatchedPts;
                    vector<Pt2d> vecLLPts;

                    for( UINT k = 0; k < imgTempLL.vecPts.size(); ++k )
                    {
                        Pt2d ptCurTLL = imgTempLL.vecPts[k];
                        Pt2i ptTempLL, ptTempResLL;
                        DOUBLE dCoefResL = 0;
                        ptTempLL.X = SINT( ptCurTLL.X + ML_ZERO);
                        ptTempLL.Y = SINT( ptCurTLL.Y + ML_ZERO);

                        Pt2d ptCurTRL = imgTempRL.vecPts[k];
                        Pt2i ptTempRL, ptTempResRL;
                        DOUBLE dCoefResR = 0;
                        ptTempRL.X = SINT( ptCurTRL.X + ML_ZERO);
                        ptTempRL.Y = SINT( ptCurTRL.Y + ML_ZERO);

                        if( ( false == clsStereoProc.mlFindCorPtsInRegFeatPts( &clsTempImgL.m_DataBlock, &clsTempImgR.m_DataBlock, clsTempImgR.m_vecFeaPtsList, \
                                ptTempLL, ptTempResLL, dCoefResL, extractPtsOpts, matchPara ) )||
                                ( false == clsStereoProc.mlFindCorPtsInRegFeatPts( &clsTempImgLT.m_DataBlock, &clsTempImgRT.m_DataBlock, clsTempImgRT.m_vecFeaPtsList, \
                                        ptTempRL, ptTempResRL, dCoefResR, extractPtsOpts, matchPara )))
                        {
                            continue;
                        }
                        Pt2d ptTLR;
                        ptTLR.X = ptTempResLL.X;
                        ptTLR.Y = ptTempResLL.Y;
                        ptTLR.lID = ptCurTLL.lID;
                        ptTLR.byType = 1;

                        Pt2d ptTRR;
                        ptTRR.X = ptTempResRL.X;
                        ptTRR.Y = ptTempResRL.Y;
                        ptTRR.lID = ptCurTRL.lID;
                        ptTRR.byType = 1;

                        StereoMatchPt ptStereoL, ptStereoR;
                        ptStereoL.ptLInImg = ptCurTLL;
                        ptStereoL.ptRInImg = ptTLR;
                        ptStereoR.ptLInImg = ptCurTRL;
                        ptStereoR.ptRInImg = ptTRR;

                        vecTempMatchedPts.push_back( ptStereoL );
                        vecTempMatchedPts.push_back( ptStereoR );

                        vecLLPts.push_back( ptCurTLL );
                    }
                    //-----------------------------------------------
                    //格网化
                    MLRect rectTemp;
                    rectTemp.dXMax = DOUBLE_MIN;
                    rectTemp.dXMin = DOUBLE_MAX;
                    rectTemp.dYMax = DOUBLE_MIN;
                    rectTemp.dYMin = DOUBLE_MAX;
                    for( UINT k = 0; k < vecLLPts.size(); ++k )
                    {
                        Pt2d ptCur = vecLLPts[k];
                        if( rectTemp.dXMax < ptCur.X )
                        {
                            rectTemp.dXMax = ptCur.X;
                        }
                        if( rectTemp.dXMin > ptCur.X )
                        {
                            rectTemp.dXMin = ptCur.X;
                        }
                        if( rectTemp.dYMax < ptCur.Y )
                        {
                            rectTemp.dYMax = ptCur.Y;
                        }
                        if( rectTemp.dYMin > ptCur.Y )
                        {
                            rectTemp.dYMin = ptCur.Y;
                        }
                    }
                    UINT nGridH = UINT(( rectTemp.dYMax - rectTemp.dYMin ) / 4 );
                    UINT nGridW = UINT(( rectTemp.dXMax - rectTemp.dXMin ) / 4 );

                    vector<bool> vecFlags;
                    if( true == clsTempImgLT.PtSelectionByGrid( vecLLPts, clsTempImgLT.GetWidth(), clsTempImgLT.GetHeight(), nGridW, nGridH, vecFlags ) )
                    {
                        for( UINT k = 0; k < vecFlags.size(); ++k )
                        {
                            if( false == vecFlags[k] )
                            {
                                continue;
                            }
                            pTempImgPtsetLL->vecPts.push_back( vecTempMatchedPts[2*k].ptLInImg );
                            pTempImgPtsetLR->vecPts.push_back( vecTempMatchedPts[2*k].ptRInImg );

                            pTempImgPtsetRL->vecPts.push_back( vecTempMatchedPts[2*k+1].ptLInImg );
                            pTempImgPtsetRR->vecPts.push_back( vecTempMatchedPts[2*k+1].ptRInImg );
                        }
                    }
                }
                /////////////////////////////////////////////////////
                break;
            }
        }
    }
    return true;

}

bool CmlSiteMapping::judgeIsOverlap( FrameImgInfo *pSet1, FrameImgInfo *pSet2, DOUBLE &dXOff, DOUBLE &dYOff )
{
    if( ( pSet1 == NULL )||( pSet2 == NULL ) )
    {
        return false;
    }
    ExOriPara exOriL1 = pSet1->exOri;
    ExOriPara exOriL2 = pSet2->exOri;

    CmlMat mat1, mat2;
    OPK2RMat( &exOriL1.ori, &mat1 );
    OPK2RMat( &exOriL2.ori, &mat2 );
    DOUBLE dVec1[3], dVec2[3];
    dVec1[0] = -1.0*mat1.GetAt( 0, 2 );
    dVec1[1] = -1.0*mat1.GetAt( 1, 2 );
    dVec1[2] = -1.0*mat1.GetAt( 2, 2 );

    dVec2[0] = -1.0*mat2.GetAt( 0, 2 );
    dVec2[1] = -1.0*mat2.GetAt( 1, 2 );
    dVec2[2] = -1.0*mat2.GetAt( 2, 2 );

    DOUBLE dAngle = dVec1[0]*dVec2[0]+dVec1[1]*dVec2[1]+dVec1[2]*dVec2[2];
    dAngle /= sqrt( dVec1[0]*dVec1[0] + dVec1[1]*dVec1[1] + dVec1[2]*dVec1[2] );
    dAngle /= sqrt( dVec2[0]*dVec2[0] + dVec2[1]*dVec2[1] + dVec2[2]*dVec2[2] );
    dAngle = acos( dAngle );

    DOUBLE dVOFH1 = atan( pSet1->nW / pSet1->inOri.f );
    DOUBLE dVOFV1 = atan( pSet1->nH / pSet1->inOri.f );

    DOUBLE dVOFH2 = atan( pSet2->nW / pSet2->inOri.f );
    DOUBLE dVOFV2 = atan( pSet2->nH / pSet2->inOri.f );

    DOUBLE dMax1 = MAX( dVOFH1, dVOFV1 );
    DOUBLE dMax2 = MAX( dVOFH2, dVOFV2 );

    CmlPhgProc clsPhgProc;
    if( dAngle < ( MAX( dMax1,  dMax2 ) ) )
    {
        ExOriPara exOriRela;
        if( true == clsPhgProc.GetRelaOri( &pSet1->exOri, &pSet2->exOri, &exOriRela ) )
        {
            DOUBLE dYAngle = exOriRela.ori.omg;
            DOUBLE dXAngle = exOriRela.ori.phi;
            dYOff = (tan(dYAngle)*pSet2->inOri.f );
            dXOff = (tan(dXAngle)*pSet2->inOri.f );
        }
        return true;
    }
    else
    {
        return false;
    }
}


//    vector<mlSBA::SBAImgInfo> vecSBAImgInfos;
//    vector<mlSBA::SBAPtList> vecSBAPtList;
//
//    //////////////////////////////////////////
//    vector<ImgPtSet> vecImgDealSets;
//    for( UINT i = 0; i < 4; i = i + 2 )//vecImgPtSets.size()
//    {
//        ImgPtSet imgTempCurSet = vecImgPtSets[i];
//        CmlFrameImage clsFrmImg;
//        ImgPtSet imgTempOutSet;
//        imgTempOutSet.imgInfo = imgTempCurSet.imgInfo;
//        clsFrmImg.UnDisCorToPlaneFrame( imgTempCurSet.vecPts, imgTempCurSet.imgInfo.inOri, imgTempCurSet.imgInfo.nH, imgTempOutSet.vecPts );
//        clsFrmImg.UnDisCorToPlaneFrame( imgTempCurSet.vecAddPts, imgTempCurSet.imgInfo.inOri, imgTempCurSet.imgInfo.nH, imgTempOutSet.vecAddPts );
//        vecImgDealSets.push_back( imgTempOutSet );
//    }
//
//    //////////////////////////////////////////
//    vector< map<ULONG, Pt2d> > vecMapImgPts;
//    vector< UINT > vecImgIndex;
//    //都采用左影像
//    for( UINT i = 0; i < vecImgDealSets.size(); ++i )
//    {
//        map<ULONG, Pt2d> mapCur;
//        ImgPtSet imgSetCur = vecImgDealSets[i];
//        UINT nIndex = imgSetCur.imgInfo.nImgIndex;
//        vecImgIndex.push_back( nIndex );
//
//        for( UINT j = 0; j < imgSetCur.vecPts.size(); ++j )
//        {
//            Pt2d ptCur = imgSetCur.vecPts[j];
//            mapCur.insert( map<ULONG, Pt2d>::value_type( ptCur.lID, ptCur ));
//        }
//        for( UINT j = 0; j < imgSetCur.vecAddPts.size(); ++j )
//        {
//            Pt2d ptCur = imgSetCur.vecAddPts[j];
//            mapCur.insert( map<ULONG, Pt2d>::value_type( ptCur.lID, ptCur ));
//        }
//        vecMapImgPts.push_back( mapCur );
//
//        FrameImgInfo frmInfo = imgSetCur.imgInfo;
//        mlSBA::SBAImgInfo imgInfo;
//
//
//        imgInfo.exOri.dOmg = frmInfo.exOri.ori.omg;
//        imgInfo.exOri.dFai = frmInfo.exOri.ori.phi;
//        imgInfo.exOri.dKap = frmInfo.exOri.ori.kap;
//        imgInfo.exOri.dX   = frmInfo.exOri.pos.X;
//        imgInfo.exOri.dY   = frmInfo.exOri.pos.Y;
//        imgInfo.exOri.dZ   = frmInfo.exOri.pos.Z;
//        imgInfo.inOri.fx   = frmInfo.inOri.f;
//        imgInfo.inOri.fy   = frmInfo.inOri.f;
//
//        if( i == 0 )
//        {
//            imgInfo.exOri.dOmg += 0.05;
//        }
//
//        imgInfo.lImgID = frmInfo.nImgIndex;
//        vecSBAImgInfos.push_back( imgInfo );
//    }
//
//    ////////////////////////////////
//    CmlStereoProc clsStereoProc;
//
//    map<ULONG, ULONG > mapIndex;
//    for( UINT i = 0; i < vecImgDealSets.size(); ++i )
//    {
//        ImgPtSet imgSetCur = vecImgDealSets[i];
//        UINT nTempIndex = imgSetCur.imgInfo.nImgIndex;
//
//        for( UINT j = 0; j < imgSetCur.vecPts.size(); ++j )
//        {
//            mlSBA::SBAPtList sbaCurList;
//            Pt2d ptCur = imgSetCur.vecPts[j];
//            map<ULONG, ULONG>::iterator it = mapIndex.find( ptCur.lID);
//            if( it != mapIndex.end() )
//            {
//                continue;
//            }
//            mapIndex.insert( map<ULONG, ULONG>::value_type( ptCur.lID, ptCur.lID ));
//
//            mlSBA::SBAImgPt sbaCurImgPt;
//            sbaCurImgPt.x = ptCur.X;
//            sbaCurImgPt.y = ptCur.Y;
//            sbaCurImgPt.lID = ptCur.lID;
//            sbaCurImgPt.lImgID = nTempIndex;
//
//            sbaCurList.ptXYZ.lID = sbaCurImgPt.lID;
//            sbaCurList.vecImgPtList.push_back( sbaCurImgPt );
//            vector<FrameImgInfo> tempFrmInfo;
//            tempFrmInfo.push_back( imgSetCur.imgInfo );
//
//            for( UINT k = 0; k < vecMapImgPts.size(); ++k )
//            {
//                if( i == k )
//                {
//                    continue;
//                }
//                map<ULONG, Pt2d> mapCur = vecMapImgPts[k];
//                map<ULONG, Pt2d>::iterator itCur = mapCur.find(ptCur.lID);
//                if( itCur != mapCur.end() )
//                {
//                    Pt2d ptCurTemp = itCur->second;
//                    sbaCurImgPt.x = ptCurTemp.X;
//                    sbaCurImgPt.y = ptCurTemp.Y;
//                    sbaCurImgPt.lID = ptCurTemp.lID;
//                    sbaCurImgPt.lImgID = vecImgIndex[k];
//                    sbaCurList.vecImgPtList.push_back( sbaCurImgPt );
//                    tempFrmInfo.push_back( vecImgDealSets[k].imgInfo );
//                }
//            }
//            if( sbaCurList.vecImgPtList.size() >= 2 )
//            {
//                FrameImgInfo frmLInfo = tempFrmInfo[0];
//                FrameImgInfo frmRInfo = tempFrmInfo[1];
//                Pt2d ptL, ptR;
//                Pt3d ptXYZ;
//                ptL.X = sbaCurList.vecImgPtList[0].x;
//                ptL.Y = sbaCurList.vecImgPtList[0].y;
//                ptR.X = sbaCurList.vecImgPtList[1].x;
//                ptR.Y = sbaCurList.vecImgPtList[1].y;
//                if( true == clsStereoProc.mlInterSection( ptL, ptR, ptXYZ, &frmLInfo.inOri, &frmLInfo.exOri, &frmRInfo.inOri, &frmRInfo.exOri ))
//                {
//                    sbaCurList.ptXYZ.X = ptXYZ.X;
//                    sbaCurList.ptXYZ.Y = ptXYZ.Y;
//                    sbaCurList.ptXYZ.Z = ptXYZ.Z;
//
////                    if( vecSBAPtList.size() < 4*(i+1) )
////                    {
//                        vecSBAPtList.push_back( sbaCurList );
////                    }
//
//                }
//            }
//        }
//         for( UINT j = 0; j < imgSetCur.vecAddPts.size(); ++j )
//        {
//            mlSBA::SBAPtList sbaCurList;
//            Pt2d ptCur = imgSetCur.vecAddPts[j];
//            map<ULONG, ULONG>::iterator it = mapIndex.find( ptCur.lID);
//            if( it != mapIndex.end() )
//            {
//                continue;
//            }
//            mapIndex.insert( map<ULONG, ULONG>::value_type( ptCur.lID, ptCur.lID ));
//
//            mlSBA::SBAImgPt sbaCurImgPt;
//            sbaCurImgPt.x = ptCur.X;
//            sbaCurImgPt.y = ptCur.Y;
//            sbaCurImgPt.lID = ptCur.lID;
//            sbaCurImgPt.lImgID = nTempIndex;
//
//            sbaCurList.ptXYZ.lID = sbaCurImgPt.lID;
//            sbaCurList.vecImgPtList.push_back( sbaCurImgPt );
//            vector<FrameImgInfo> tempFrmInfo;
//            tempFrmInfo.push_back( imgSetCur.imgInfo );
//
//            for( UINT k = 0; k < vecMapImgPts.size(); ++k )
//            {
//                if( i == k )
//                {
//                    continue;
//                }
//                map<ULONG, Pt2d> mapCur = vecMapImgPts[k];
//                map<ULONG, Pt2d>::iterator itCur = mapCur.find(ptCur.lID);
//                if( itCur != mapCur.end() )
//                {
//                    Pt2d ptCurTemp = itCur->second;
//                    sbaCurImgPt.x = ptCurTemp.X;
//                    sbaCurImgPt.y = ptCurTemp.Y;
//                    sbaCurImgPt.lID = ptCurTemp.lID;
//                    sbaCurImgPt.lImgID = vecImgIndex[k];
//                    sbaCurList.vecImgPtList.push_back( sbaCurImgPt );
//                    tempFrmInfo.push_back( vecImgDealSets[k].imgInfo );
//                }
//            }
//            if( sbaCurList.vecImgPtList.size() >= 2 )
//            {
//                FrameImgInfo frmLInfo = tempFrmInfo[0];
//                FrameImgInfo frmRInfo = tempFrmInfo[1];
//                Pt2d ptL, ptR;
//                Pt3d ptXYZ;
//                ptL.X = sbaCurList.vecImgPtList[0].x;
//                ptL.Y = sbaCurList.vecImgPtList[0].y;
//                ptR.X = sbaCurList.vecImgPtList[1].x;
//                ptR.Y = sbaCurList.vecImgPtList[1].y;
//                if( true == clsStereoProc.mlInterSection( ptL, ptR, ptXYZ, &frmLInfo.inOri, &frmLInfo.exOri, &frmRInfo.inOri, &frmRInfo.exOri ) )
//                {
//                    sbaCurList.ptXYZ.X = ptXYZ.X;
//                    sbaCurList.ptXYZ.Y = ptXYZ.Y;
//                    sbaCurList.ptXYZ.Z = ptXYZ.Z;
//                    if( ptXYZ.Z < 0 )
//                    {
//                        if( vecSBAPtList.size() < 4*i )
//                        {
//
//                        vecSBAPtList.push_back( sbaCurList );                        }
//
//                    }
//
//
//                }
//            }
//        }
//    }
//
//    ////////////////////////////////////
//    mlSBA::SBAPara sbaPara;
//
//    mlSBA::SBAImgInfo* pImgInfoOut = NULL;
//    mlSBA::SBAPt3d* pPt3dArray = NULL;
//    if( -1 != mlSBA_BasicModel( vecSBAPtList, vecSBAImgInfos, sbaPara, pImgInfoOut, pPt3dArray ) )
//    {
//
//    }


bool CmlSiteMapping::BASolve( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<StereoSet> &vecStereoOut, UINT nModel )
{
    if( vecStereoIn.size() < 2 )
    {
        return false;
    }
    if( vecImgPtSets.size() != 2*vecStereoIn.size() )
    {
        return false;
    }
    vector<InOriPara> vecImgInOris;
    vector< vector<Pt2d> > vecImgPts;
    vector<ExOriPara> vecImgExOris;
    vector< Pt3d > vec3dPts;
    vector<ULONG> vecImgIDs;
    vector<UINT> vecHeight;

    for( UINT i = 0; i < vecStereoIn.size(); ++i )
    {
        StereoSet sSet = vecStereoIn[i];
        vecImgInOris.push_back( sSet.imgLInfo.inOri );
        vecImgInOris.push_back( sSet.imgRInfo.inOri );

        vecImgExOris.push_back( sSet.imgLInfo.exOri );
        vecImgExOris.push_back( sSet.imgRInfo.exOri );

        vecImgIDs.push_back( sSet.imgLInfo.nImgIndex );
        vecImgIDs.push_back( sSet.imgRInfo.nImgIndex );

        vector<Pt2d> vecPts1 = vecImgPtSets.at(2*i).vecPts;
        vecPts1.insert( vecPts1.end(), vecImgPtSets.at(2*i).vecAddPts.begin(), vecImgPtSets.at(2*i).vecAddPts.end() );
        vecImgPts.push_back( vecPts1 );

        vector<Pt2d> vecPts2 = vecImgPtSets.at(2*i+1).vecPts;
        vecPts2.insert( vecPts2.end(), vecImgPtSets.at(2*i+1).vecAddPts.begin(), vecImgPtSets.at(2*i+1).vecAddPts.end() );
        vecImgPts.push_back( vecPts2 );

        vecHeight.push_back( sSet.imgLInfo.nH );
        vecHeight.push_back( sSet.imgRInfo.nH );
    }
    vector< map<ULONG, UINT> > vecMaps;
    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        map<ULONG, UINT> mapTemp;
        vector<Pt2d> &vecPtsTemp = vecImgPts[i];
        for( UINT j = 0; j < vecPtsTemp.size(); ++j )
        {
            mapTemp.insert( map<ULONG, UINT>::value_type( vecPtsTemp[j].lID, j ) );
        }
        vecMaps.push_back( mapTemp );
    }
    map<ULONG, ULONG> mapIndex;
    CmlFrameImage clsFrm;
    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        vector<Pt2d> &vecPtsTemp = vecImgPts[i];
        for( UINT j = 0; j < vecPtsTemp.size(); ++j )
        {
            Pt2d &ptCur = vecPtsTemp[j];
            Pt2d ptRes;
            clsFrm.UnDisCorToPlaneFrame( ptCur, vecImgInOris[i], vecHeight[i], ptRes );
            ptCur = ptRes;
        }
    }

    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        vector<Pt2d> &vecTempPts = vecImgPts[i];
        for( UINT j = 0; j < vecTempPts.size(); ++j )
        {
            Pt2d &ptCur = vecTempPts[j];
            if( mapIndex.find(ptCur.lID) != mapIndex.end() )
            {
                continue;
            }

            for( UINT k = 0; k < vecImgPts.size(); ++k )
            {
                if( k == i )
                {
                    continue;
                }
                map<ULONG, UINT> &mapCur = vecMaps[k];
                map<ULONG, UINT>::iterator it = mapCur.find( ptCur.lID );
                if( it != mapCur.end() )
                {
                    Pt2d &ptR = vecImgPts[k].at( it->second );

                    CmlStereoProc clsStereoProc;
                    Pt3d ptXYZ;
                    if( true == clsStereoProc.mlInterSection(  ptCur, ptR, ptXYZ, &vecImgInOris[i], &vecImgExOris[i], \
                            &vecImgInOris[k], &vecImgExOris[k] ) )
                    {
                        ptXYZ.lID = ptCur.lID;
                        vec3dPts.push_back( ptXYZ );
                        mapIndex.insert( map<ULONG, ULONG>::value_type( ptCur.lID, ptCur.lID ) );
                        break;
                    }
                }
            }
        }
    }
    //////////////////////////////////////////
    CmlBA clsBA;
    vector<bool> vecbImgIsFixed, vecb3dPtsIsFixed;
    for( UINT i = 0; i < vecImgExOris.size(); ++i )
    {
        if( ( i == (vecImgExOris.size() / 2   ) )||(( i == (vecImgExOris.size() / 2  + 1)) ))
        {
            vecbImgIsFixed.push_back( true );
        }
        else
        {
            vecbImgIsFixed.push_back( false );
        }
    }
    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        vecb3dPtsIsFixed.push_back( false );
    }
    if( nModel == 1 )
    {
        if( false == clsBA.BA_Common( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtsIsFixed, vecImgExOris, vec3dPts ) )
        {
            return false;
        }
    }
    else
    {
        if( false == clsBA.BA_WithEpiImg1N1( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtsIsFixed, vecImgExOris, vec3dPts ) )
        {
            return false;
        }
    }

    for( UINT i = 0; i < vecStereoIn.size(); ++i )
    {
        StereoSet sSet = vecStereoIn[i];
        sSet.imgLInfo.exOri = vecImgExOris[2*i];
        sSet.imgRInfo.exOri = vecImgExOris[2*i+1];
        vecStereoOut.push_back(sSet);
    }
    return true;
}
bool CmlSiteMapping::BASolve( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<StereoSet> &vecStereoOut, Pt2d &ptProjErr, UINT nModel )
{
    if( vecStereoIn.size() < 2 )
    {
        return false;
    }
    if( vecImgPtSets.size() != 2*vecStereoIn.size() )
    {
        return false;
    }
    vector<InOriPara> vecImgInOris;
    vector< vector<Pt2d> > vecImgPts;
    vector<ExOriPara> vecImgExOris;
    vector< Pt3d > vec3dPts;
    vector<ULONG> vecImgIDs;
    vector<UINT> vecHeight;

    for( UINT i = 0; i < vecStereoIn.size(); ++i )
    {
        StereoSet sSet = vecStereoIn[i];
        vecImgInOris.push_back( sSet.imgLInfo.inOri );
        vecImgInOris.push_back( sSet.imgRInfo.inOri );

        vecImgExOris.push_back( sSet.imgLInfo.exOri );
        vecImgExOris.push_back( sSet.imgRInfo.exOri );

        vecImgIDs.push_back( sSet.imgLInfo.nImgIndex );
        vecImgIDs.push_back( sSet.imgRInfo.nImgIndex );

        vector<Pt2d> vecPts1 = vecImgPtSets.at(2*i).vecPts;
        vecPts1.insert( vecPts1.end(), vecImgPtSets.at(2*i).vecAddPts.begin(), vecImgPtSets.at(2*i).vecAddPts.end() );
        vecImgPts.push_back( vecPts1 );

        vector<Pt2d> vecPts2 = vecImgPtSets.at(2*i+1).vecPts;
        vecPts2.insert( vecPts2.end(), vecImgPtSets.at(2*i+1).vecAddPts.begin(), vecImgPtSets.at(2*i+1).vecAddPts.end() );
        vecImgPts.push_back( vecPts2 );

        vecHeight.push_back( sSet.imgLInfo.nH );
        vecHeight.push_back( sSet.imgRInfo.nH );
    }
    vector< map<ULONG, UINT> > vecMaps;
    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        map<ULONG, UINT> mapTemp;
        vector<Pt2d> &vecPtsTemp = vecImgPts[i];
        for( UINT j = 0; j < vecPtsTemp.size(); ++j )
        {
            mapTemp.insert( map<ULONG, UINT>::value_type( vecPtsTemp[j].lID, j ) );
        }
        vecMaps.push_back( mapTemp );
    }
    map<ULONG, ULONG> mapIndex;
    CmlFrameImage clsFrm;
    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        vector<Pt2d> &vecPtsTemp = vecImgPts[i];
        for( UINT j = 0; j < vecPtsTemp.size(); ++j )
        {
            Pt2d &ptCur = vecPtsTemp[j];
            Pt2d ptRes;
            clsFrm.UnDisCorToPlaneFrame( ptCur, vecImgInOris[i], vecHeight[i], ptRes );
            ptCur = ptRes;
        }
    }

    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        vector<Pt2d> &vecTempPts = vecImgPts[i];
        for( UINT j = 0; j < vecTempPts.size(); ++j )
        {
            Pt2d &ptCur = vecTempPts[j];
            if( mapIndex.find(ptCur.lID) != mapIndex.end() )
            {
                continue;
            }

            for( UINT k = 0; k < vecImgPts.size(); ++k )
            {
                if( k == i )
                {
                    continue;
                }
                map<ULONG, UINT> &mapCur = vecMaps[k];
                map<ULONG, UINT>::iterator it = mapCur.find( ptCur.lID );
                if( it != mapCur.end() )
                {
                    Pt2d &ptR = vecImgPts[k].at( it->second );

                    CmlStereoProc clsStereoProc;
                    Pt3d ptXYZ;
                    if( true == clsStereoProc.mlInterSection(  ptCur, ptR, ptXYZ, &vecImgInOris[i], &vecImgExOris[i], \
                            &vecImgInOris[k], &vecImgExOris[k] ) )
                    {
                        ptXYZ.lID = ptCur.lID;
                        vec3dPts.push_back( ptXYZ );
                        mapIndex.insert( map<ULONG, ULONG>::value_type( ptCur.lID, ptCur.lID ) );
                        break;
                    }
                }
            }
        }
    }
    //////////////////////////////////////////
    CmlBA clsBA;
    vector<bool> vecbImgIsFixed, vecb3dPtsIsFixed;
    for( UINT i = 0; i < vecImgExOris.size(); ++i )
    {
        if( ( i == (vecImgExOris.size() / 2   ) )||(( i == (vecImgExOris.size() / 2  + 1)) ))
        {
            vecbImgIsFixed.push_back( true );
        }
        else
        {
            vecbImgIsFixed.push_back( false );
        }
    }
    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        vecb3dPtsIsFixed.push_back( false );
    }
    vector<ExOriPara> vecImgResErr;
    vector<Pt3d> vec3dPtsResErr;


    if( nModel == 1 )
    {
        if( false == clsBA.BA_Common( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtsIsFixed, vecImgExOris, vec3dPts, vecImgResErr, vec3dPtsResErr, ptProjErr ) )
        {
            return false;
        }
    }
    else
    {
        if( false == clsBA.BA_WithEpiImg1N1( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtsIsFixed, vecImgExOris, vec3dPts, vecImgResErr, vec3dPtsResErr, ptProjErr  ) )
        {
            return false;
        }
    }

    for( UINT i = 0; i < vecStereoIn.size(); ++i )
    {
        StereoSet sSet = vecStereoIn[i];
        sSet.imgLInfo.exOri = vecImgExOris[2*i];
        sSet.imgRInfo.exOri = vecImgExOris[2*i+1];
        vecStereoOut.push_back(sSet);
    }
    return true;
}
bool CmlSiteMapping::BASolve( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<bool> vecbImgIsFixed, vector<StereoSet> &vecStereoOut, UINT nModel )
{
    if( vecStereoIn.size() < 2 )
    {
        return false;
    }
    if( vecImgPtSets.size() != 2*vecStereoIn.size() )
    {
        return false;
    }
    vector<InOriPara> vecImgInOris;
    vector< vector<Pt2d> > vecImgPts;
    vector<ExOriPara> vecImgExOris;
    vector< Pt3d > vec3dPts;
    vector<ULONG> vecImgIDs;
    vector<UINT> vecHeight;

    for( UINT i = 0; i < vecStereoIn.size(); ++i )
    {
        StereoSet sSet = vecStereoIn[i];
        vecImgInOris.push_back( sSet.imgLInfo.inOri );
        vecImgInOris.push_back( sSet.imgRInfo.inOri );

        vecImgExOris.push_back( sSet.imgLInfo.exOri );
        vecImgExOris.push_back( sSet.imgRInfo.exOri );

        vecImgIDs.push_back( sSet.imgLInfo.nImgIndex );
        vecImgIDs.push_back( sSet.imgRInfo.nImgIndex );

        vector<Pt2d> vecPts1 = vecImgPtSets.at(2*i).vecPts;
        vecPts1.insert( vecPts1.end(), vecImgPtSets.at(2*i).vecAddPts.begin(), vecImgPtSets.at(2*i).vecAddPts.end() );
        vecImgPts.push_back( vecPts1 );

        vector<Pt2d> vecPts2 = vecImgPtSets.at(2*i+1).vecPts;
        vecPts2.insert( vecPts2.end(), vecImgPtSets.at(2*i+1).vecAddPts.begin(), vecImgPtSets.at(2*i+1).vecAddPts.end() );
        vecImgPts.push_back( vecPts2 );

        vecHeight.push_back( sSet.imgLInfo.nH );
        vecHeight.push_back( sSet.imgRInfo.nH );
    }
    vector< map<ULONG, UINT> > vecMaps;
    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        map<ULONG, UINT> mapTemp;
        vector<Pt2d> &vecPtsTemp = vecImgPts[i];
        for( UINT j = 0; j < vecPtsTemp.size(); ++j )
        {
            mapTemp.insert( map<ULONG, UINT>::value_type( vecPtsTemp[j].lID, j ) );
        }
        vecMaps.push_back( mapTemp );
    }
    map<ULONG, ULONG> mapIndex;
    CmlFrameImage clsFrm;
    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        vector<Pt2d> &vecPtsTemp = vecImgPts[i];
        for( UINT j = 0; j < vecPtsTemp.size(); ++j )
        {
            Pt2d &ptCur = vecPtsTemp[j];
            Pt2d ptRes;
            clsFrm.UnDisCorToPlaneFrame( ptCur, vecImgInOris[i], vecHeight[i], ptRes );
            ptCur = ptRes;
        }
    }

    for( UINT i = 0; i < vecImgPts.size(); ++i )
    {
        vector<Pt2d> &vecTempPts = vecImgPts[i];
        for( UINT j = 0; j < vecTempPts.size(); ++j )
        {
            Pt2d &ptCur = vecTempPts[j];
            if( mapIndex.find(ptCur.lID) != mapIndex.end() )
            {
                continue;
            }

            for( UINT k = 0; k < vecImgPts.size(); ++k )
            {
                if( k == i )
                {
                    continue;
                }
                map<ULONG, UINT> &mapCur = vecMaps[k];
                map<ULONG, UINT>::iterator it = mapCur.find( ptCur.lID );
                if( it != mapCur.end() )
                {
                    Pt2d &ptR = vecImgPts[k].at( it->second );

                    CmlStereoProc clsStereoProc;
                    Pt3d ptXYZ;
                    if( true == clsStereoProc.mlInterSection(  ptCur, ptR, ptXYZ, &vecImgInOris[i], &vecImgExOris[i], \
                            &vecImgInOris[k], &vecImgExOris[k] ) )
                    {
                        ptXYZ.lID = ptCur.lID;
                        vec3dPts.push_back( ptXYZ );
                        mapIndex.insert( map<ULONG, ULONG>::value_type( ptCur.lID, ptCur.lID ) );
                        break;
                    }
                }
            }
        }
    }
    //////////////////////////////////////////
    CmlBA clsBA;
    vector<bool> vecb3dPtsIsFixed;

    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        vecb3dPtsIsFixed.push_back( false );
    }

    if( nModel == 1 )
    {
        if( false == clsBA.BA_Common( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtsIsFixed, vecImgExOris, vec3dPts ) )
        {
            return false;
        }
    }
    else
    {
        if( false == clsBA.BA_WithEpiImg1N1( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtsIsFixed, vecImgExOris, vec3dPts ) )
        {
            return false;
        }
    }
    for( UINT i = 0; i < vecStereoIn.size(); ++i )
    {
        StereoSet sSet = vecStereoIn[i];
        sSet.imgLInfo.exOri = vecImgExOris[2*i];
        sSet.imgRInfo.exOri = vecImgExOris[2*i+1];
        vecStereoOut.push_back(sSet);
    }
    return true;
}
