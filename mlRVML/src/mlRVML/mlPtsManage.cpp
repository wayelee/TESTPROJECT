/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlPtsManage.cpp
* @date 2012.02.10
* @author 万文辉 whwan@irsa.ac.cn
* @brief 匹配点管理源文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#include "mlPtsManage.h"

/**
* @fn CmlPtsManage
* @date 2012.02.10
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlPtsManage类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlPtsManage::CmlPtsManage()
{
    //ctor
}

/**
* @fn ~CmlPtsManage
* @date 2012.02.10
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlPtsManage类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlPtsManage::~CmlPtsManage()
{
    //dtor
}

/**
* @fn GetPairPts
* @date 2012.02.10
* @author  万文辉 whwan@irsa.ac.cn
* @brief 将左影像匹配点和右影像匹配点合并
* @param clsImgLPts 左影像匹配点
* @param clsImgRPts 右影像匹配点
* @param vecMatchPts 左右匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlPtsManage::GetPairPts( ImgPtSet &clsImgLPts, ImgPtSet &clsImgRPts, vector<StereoMatchPt> &vecMatchPts )
{
    map< ULONG, Pt2d > mapRPts;
    for( UINT i = 0; i < clsImgRPts.vecPts.size(); ++i )
    {
        Pt2d ptCur = clsImgRPts.vecPts[i];
        mapRPts.insert( map<ULONG, Pt2d>::value_type( ptCur.lID, ptCur ) );
    }


    for( UINT i = 0; i < clsImgLPts.vecPts.size(); ++i )
    {
        Pt2d ptCur = clsImgLPts.vecPts[i];
        map<ULONG, Pt2d>::iterator it = mapRPts.find( ptCur.lID );
        if( it != mapRPts.end() )
        {
            StereoMatchPt ptMatch;
            ptMatch.ptLInImg = ptCur;
            ptMatch.ptRInImg = it->second;
            ptMatch.lID = ptCur.lID;
            vecMatchPts.push_back( ptMatch );
        }
    }

    bool bFlag = true;
    if( vecMatchPts.size() == 0 )
    {
        bFlag = false;
    }

    for( UINT i = 0; i < clsImgLPts.vecAddPts.size(); ++i )
    {
        Pt2d ptLCur = clsImgLPts.vecAddPts[i];
        for( UINT j = 0; j < clsImgRPts.vecAddPts.size(); ++j )
        {
            Pt2d ptRCur = clsImgRPts.vecAddPts[j];
            if( ptLCur.lID == ptRCur.lID )
            {
                StereoMatchPt ptMatch;
                ptMatch.ptLInImg = ptLCur;
                ptMatch.ptRInImg = ptRCur;
                ptMatch.lID = ptLCur.lID;
                vecMatchPts.push_back( ptMatch );
            }
        }
    }
    return bFlag;
}

/**
* @fn SplitPairPts
* @date 2012.02.10
* @author  万文辉 whwan@irsa.ac.cn
* @brief 将成对匹配点分开
* @param frmInfoL 左影像信息
* @param frmInfoR 右影像信息
* @param vecAutoMatchPts 左右同名匹配点
* @param clsImgLPts 左影像匹配点
* @param clsImgRPts 右影像匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlPtsManage::SplitPairPts( FrameImgInfo frmInfoL, FrameImgInfo frmInfoR, vector<StereoMatchPt> &vecAutoMatchPts, ImgPtSet &clsImgLPts, ImgPtSet &clsImgRPts )
{

    UINT nLIndex = frmInfoL.nImgIndex;
    UINT nRIndex = frmInfoR.nImgIndex;

    if( clsImgLPts.imgInfo.nCamID == 0 )
    {
        clsImgLPts.imgInfo = frmInfoL;
    }
    if( clsImgRPts.imgInfo.nCamID == 0 )
    {
        clsImgRPts.imgInfo = frmInfoR;
    }


    for( UINT i = 0; i < vecAutoMatchPts.size(); ++i )
    {
        StereoMatchPt* pt = &vecAutoMatchPts[i];
        ULONG nTmpID = nLIndex * 10e11 + nRIndex * 10e7 + i + 1;;
        Pt2d ptL, ptR;
        ptL = pt->ptLInImg;
        ptL.byType = 1;
        ptL.lID = nTmpID;

        ptR = pt->ptRInImg;
        ptR.byType = 1;
        ptR.lID = nTmpID;

        clsImgLPts.vecPts.push_back( ptL );
        clsImgRPts.vecPts.push_back( ptR );
    }
    return true;
}

bool getFindRes( vector<Pt2d> &vecPts, ULONG lID )
{
    for( UINT i = 0; i < vecPts.size(); ++i )
    {
        ULONG lCurID = vecPts[i].lID;
        if( lCurID == lID )
        {
            return true;
        }
    }
    return false;
}

/**
* @fn GetNewStereoPtID
* @date 2012.02.10
* @author  万文辉 whwan@irsa.ac.cn
* @brief 获取匹配点的编号
* @param clsLPts 左影像信息
* @param clsRPts 右影像信息
* @param lID 匹配点的编号
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlPtsManage::GetNewStereoPtID(  ImgPtSet &clsLPts, ImgPtSet &clsRPts, ULONG &lID  )
{
    if( ( clsLPts.imgInfo.nImgIndex == 0 )||( clsRPts.imgInfo.nImgIndex == 0 ) )
    {
        return false;
    }
    ULONG lHeadID = clsLPts.imgInfo.nImgIndex * 10e11 + clsRPts.imgInfo.nImgIndex * 10e7 +  9 * 10e6 + 1;
    for( UINT i = 0; i < 10e5; ++i )
    {
        ULONG lCurID = lHeadID + i;

        if( ( false == getFindRes( clsLPts.vecAddPts, lCurID ) )&&( false == getFindRes( clsLPts.vecAddPts, lCurID ) ) )
        {
            lID = lCurID;
            return true;
        }
    }
    return false;
}

/**
* @fn GetNewSinglePtID
* @date 2012.02.10
* @author  万文辉 whwan@irsa.ac.cn
* @brief 获取单个点的编号
* @param clsImgPts 影像信息
* @param lID 匹配点的编号
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlPtsManage::GetNewSinglePtID(  ImgPtSet &clsImgPts, ULONG &lID  )
{
    if( ( clsImgPts.imgInfo.nImgIndex == 0 ) )
    {
        return false;
    }
    ULONG lHeadID = clsImgPts.imgInfo.nImgIndex * 10e11 + clsImgPts.imgInfo.nImgIndex * 10e7 +  9 * 10e6 + 1;
    for( UINT i = 0; i < 10e5; ++i )
    {
        ULONG lCurID = lHeadID + i;

        if( false == getFindRes( clsImgPts.vecAddPts, lCurID ) )
        {
            lID = lCurID;
            return true;
        }
    }
    return false;
}


