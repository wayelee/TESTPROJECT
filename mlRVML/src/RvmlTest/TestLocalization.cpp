/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestLocalization.h
* @date 2011.11.18
* @author 彭嫚  pengman@irsa.ac.cn
* @brief DEM影像拼接测试类源文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "PtsFileManage.h"
#include "PtFilesRW.h"
#include "TestLocalization.h"
#include "Sitemapproj.h"
#include "../mlRVML/mlLocalization.h"



CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestLocalization,"alltest" );

CTestLocalization::CTestLocalization()
{
    //ctor
}

CTestLocalization::~CTestLocalization()
{
    //dtor
}


/**
* @fn TestLocalInSequenceImg_ok()
* @date 2011.12.1
* @author 彭嫚  pengman@irsa.ac.cn
* @brief 测试序列影像定位的功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestLocalization::TestLocalInSequenceImg_ok()
{
//    char* cProjPath = "../../../UnitTestData/TestLocalInBySequenceImg/Lunar/Local.smp";
//    char* cSatPath  = "../../../UnitTestData/TestLocalInBySequenceImg/Lunar/satDom.tif";
//    char* cOutPath  = "../../../UnitTestData/TestLocalInBySequenceImg/Lunar/localSequence.txt";
//
//    CmlSiteMapProj clsSiteProj;
//    vector<StereoSet> vecStereoImg;
//
//    if( true == clsSiteProj.LoadProj( cProjPath ) )
//    {
//        clsSiteProj.GetImgSet(vecStereoImg);
//        vector<FrameImgInfo> vecFrameInfo;
//        for( int i = 0; i < vecStereoImg.size(); ++i )
//        {
//            StereoSet* pSet = &vecStereoImg.at(i);
//
//            vecFrameInfo.push_back( pSet->imgLInfo );
//        }
//        Pt3d ptLocalRes;
//        DOUBLE dAccuracy = 0;
//
//        CmlLocalization clsLocal;
//        bool bresult = clsLocal.LocalInSequenceImg( cSatPath, vecFrameInfo, ptLocalRes, dAccuracy );
//        CPPUNIT_ASSERT(bresult == true);
//
//    }


}

/**
* @fn TestLocalIn2Dom_ok()
* @date 2011.12.1
* @author 彭嫚   pengman@irsa.ac.cn
* @brief 测试序列影像定位正常实现的功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestLocalization::TestLocalIn2Dom_ok()
{
//    char* cLandDom = "../../../UnitTestData/TestLocalIn2Dom/LandDom.tif";
//    char* cSatDom = "../../../UnitTestData/TestLocalIn2Dom/SatDom.tif";
//
//    Pt3d ptLocalRes;
//    DOUBLE dLocalAccuracy = 0;
//
//    ImgPtSet satPtSet;
//    ImgPtSet LandPtSet;
//
//    CmlLocalization clsLocal;
//    bool bresult = clsLocal.LocalIn2Dom( cLandDom, cSatDom, LandPtSet, satPtSet, ptLocalRes, dLocalAccuracy );
//    CPPUNIT_ASSERT(bresult == true);
}

/**
* @fn TestLocalInTwoSite_ok()
* @date 2011.12.1
* @author 彭嫚   pengman@irsa.ac.cn
* @brief 测试序列影像定位正常实现的功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestLocalization::TestLocalInTwoSite_ok()
{
//    char* cSiteProjFile =  "../../../UnitTestData/TestLocalInTwoSite/Mars1/Local.smp";
//    char* cOutPath = "../../../UnitTestData/TestLocalInTwoSite/Mars1/Local.rtf";
//
//    CmlSiteMapProj clsSiteProj;
//    UINT FrontSiteNum = 2;
//    UINT SecondSiteNum = 5;
//    if( true == clsSiteProj.LoadProj( cSiteProjFile ) )
//    {
//        vector<StereoSet> vecFrontStereoSet;
//        if( false == clsSiteProj.GetDealSet( FrontSiteNum, -1, -1, vecFrontStereoSet ) )
//        {
//            return;
//        }
//        vector<StereoSet> vecEndStereoSet;
//        if( false == clsSiteProj.GetDealSet( SecondSiteNum, -1, -1, vecEndStereoSet ) )
//        {
//            return;
//        }
//        Pt3d ptLocalRes;
//        DOUBLE dAccuracy = 0;
//        CmlLocalization clsLocal;
//        bool bresult = clsLocal.LocalInTwoSite(vecFrontStereoSet, vecEndStereoSet, ptLocalRes, dAccuracy);
//        CPPUNIT_ASSERT(bresult == true);
//
//    }


}

/**
* @fn TestLocalByBundleResection_ok()
* @date 2011.12.1
* @author 彭嫚   pengman@irsa.ac.cn
* @brief 测试通过后方交会定位正常实现的功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestLocalization::TestLocalByBundleResection_ok()
{
//    char* cProjFile = "../../../UnitTestData/TestLocalByIntersec/Mars1/Local.smp";
//    char* cGCPFile = "../../../UnitTestData/TestLocalByIntersec/test.gcp";
//
//
//    CmlSiteMapProj siteProj;
//    string strProjPath;
//    vector<StereoSet> vecStereoImg;
//
//    if( true == siteProj.LoadProj( cProjFile ) )
//    {
//
//        vector<Pt3d> vecGcps;
//        CPtFilesRW clsPtRW;
//        clsPtRW.LoadGCPPts( cGCPFile,vecGcps );
//
//        vector<ImgPtSet> vecImgPts;
//
//        string strFoldDirectory;
//
//        siteProj.GetProjPath(strProjPath);
//        siteProj.GetImgSet(vecStereoImg);
//
//        int nPos = strProjPath.rfind("/");
//        strFoldDirectory.assign( strProjPath, 0, nPos );
//        strFoldDirectory.append( "/MatchRes/" );
//
//        vector<string> vecMarkedfiles;
//        for( SINT i = 0; i < vecStereoImg.size(); ++i )
//        {
//            StereoSet* pSSet = &vecStereoImg.at(i);
//            int nTempSiteID = pSSet->nSiteID;
//            char cSiteID[20];
//            sprintf( cSiteID, "%d", nTempSiteID );
//            string strTempFDirect = strFoldDirectory;
//            strTempFDirect.append( cSiteID );
//
////        if( access( strTempFDirect.c_str(), 0 ) == -1 )
////        {
////            if( mkdir( strTempFDirect.c_str(), 0777) )
////            {
////                return ;
////            }
////        }
//
//            strTempFDirect.append( "/" );
//            string strTLPath = strTempFDirect;
//            string strTRPath = strTempFDirect;
//
//            SINT nTTPos = pSSet->imgLInfo.strName.rfind( "/");
//            string strLTempHead;
//            strLTempHead.assign( pSSet->imgLInfo.strName, nTTPos+1, pSSet->imgLInfo.strName.length() );
//            SINT nTTBPos = strLTempHead.rfind( "." );
//            string strLTempFinal;
//            strLTempFinal.assign( strLTempHead, 0, nTTBPos );
//            strTLPath.append( strLTempFinal );
//            strTLPath.append( ".fmf" );
//            ImgPtSet imgSetL, imgSetR;
//            clsPtRW.LoadImgPtSet( strTLPath, imgSetL );
//
//            vecImgPts.push_back( imgSetL );
//
//            nTTPos = pSSet->imgRInfo.strName.rfind( "/");
//            string strRTempHead;
//            strRTempHead.assign( pSSet->imgRInfo.strName, nTTPos+1, pSSet->imgRInfo.strName.length() );
//            nTTBPos = strRTempHead.rfind( "." );
//            string strRTempFinal;
//            strRTempFinal.assign( strRTempHead, 0, nTTBPos );
//
//            strTRPath.append( strRTempFinal );
//            strTRPath.append( ".fmf" );
//
//            clsPtRW.LoadImgPtSet( strTRPath, imgSetR );
//
//            vecImgPts.push_back( imgSetR);
//
//            ///////////////////////////////////////////////////////////////////
//
//        }
//
//        Pt3d ptLocalRes;
//        DOUBLE dAccuracy;
//
//        CmlLocalization clsLocal;
//        bool bresult = clsLocal.LocalByBundleResection( vecGcps, vecImgPts, ptLocalRes, dAccuracy );
//        CPPUNIT_ASSERT(bresult == true);
//
//    }

}

/** @brief TearDown
  *
  * @todo: document this function
  */
void CTestLocalization::tearDown()
{

}

/** @brief SetUp
  *
  * @todo: document this function
  */
void CTestLocalization::setUp()
{

}

