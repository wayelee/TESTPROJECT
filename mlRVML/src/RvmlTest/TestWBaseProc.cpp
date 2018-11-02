#include "TestWBaseProc.h"
#include "../mlRVML/mlWBaseProc.h"
#include "Sitemapproj.h"
//#include "../mlRVML/mlSiteMapping.h"

#include <fstream>
#include <algorithm>

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestWBaseProc,"alltest" );

CTestWBaseProc::CTestWBaseProc()
{
    //ctor
}

CTestWBaseProc::~CTestWBaseProc()
{
    //dtor
}
/** @brief TestmlCoordTrans
  *
  * @todo: document this function
  */

/**
* @fn TestWideBaseAnalysis_ok()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数正常时最优基线计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc::TestWideBaseAnalysis_ok()
{
    CmlWBaseProc cls;
    //读入文件获得相关参数
    char *srcPath = "../../../UnitTestData/TestWBaseProc/wameraPara.txt";
    ifstream stm(srcPath);
    FILE *pIOFile;
    InOriPara mlNav;
    InOriPara mlPan;
    BaseOptions AnaPara;
    DOUBLE dBestBase;
    if((pIOFile = fopen(srcPath,"r")))
    {


        stm >> mlNav.f >> mlNav.x >> mlNav.y >> mlNav.k1 >> mlNav.k2 >> mlNav.k3 >> mlNav.p1 >> mlNav.p2 >> mlNav.alpha >> mlNav.beta ;
        stm >> mlPan.f >> mlPan.x >> mlPan.y >> mlPan.k1 >> mlPan.k2 >> mlPan.k3 >> mlNav.p1 >> mlPan.p2 >> mlPan.alpha >> mlPan.beta ;
        stm >> AnaPara.dFixBase >> AnaPara.dPixel >> AnaPara.dTarget >> AnaPara.nWidth >> AnaPara.dThresHold >> AnaPara.nIterTime;
        stm.close();
    }
    CmlWBaseProc wb;
    bool result;
    result = wb.WideBaseAnalysis(mlNav, mlPan, AnaPara, dBestBase);
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestWideBaseAnalysis_abnormal()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数异常时最优基线计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc::TestWideBaseAnalysis_abnormal()
{
//    CmlWBaseProc cls;
//
//    bool result = cls.WideBaseAnalysis("","");
//    CPPUNIT_ASSERT(result == false);

}

/**
* @fn TestWideBaseMapping_ok()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数正常时最优基线计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc::TestWideBaseMapping_ok()
{
//    WideOptions WidePara;
//    WidePara.bIsUsingFeatPt = false;
//    WidePara.bIsUsingPartion = false;
////    WidePara.dCoef = cd.dCoef;
////    WidePara.nRadiusX = cd.nColRadius;
////    WidePara.nRadiusY = cd.nRowRadius;
////    WidePara.nStep = cd.nGridSize;
////    WidePara.XResolution = cd.dDEMResolution;
//
//    char * srcPath = "../../../UnitTestData/TestWBaseProc/WideMars/SiteMapping.smp";
//    char * dstPath = "../../../UnitTestData/TestWBaseProc/WideMars/WideDem.tif";
//
//    CmlSiteMapProj site;
//    //读入工程文件
//    if( false == site.LoadProj( srcPath ))
//    {
//        return;
//    }
//    int nSiteID, nRollID, nImgID;
//
//
//
//    vector<StereoSet> vecStereoSet;
//    string strProjPath;
//    //读入工程文件中的像对信息
//    site.GetDealSet( 1, 1, -1, vecStereoSet);
//    int nNum = vecStereoSet.size();
//    vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum), vecDPtL(nNum), vecDPtR(nNum);
//    vector<string> vecStrMatchFiles;
//    string strFoldDirectory;
////    site.GetProjPath(strProjPath);
//    int nPos = strProjPath.rfind("/");
//    strFoldDirectory.assign( strProjPath, 0, nPos );
//    strFoldDirectory.append( "/MatchRes/" );
//
//
//    for( int i = 0; i < vecStereoSet.size(); ++i )
//    {
//        StereoSet* pSSet = &vecStereoSet.at(i);
//        int nTempSiteID = pSSet->nSiteID;
//        char cSiteID[20];
//        sprintf( cSiteID, "%d", nTempSiteID );
//        string strTempFDirect = strFoldDirectory;
//        strTempFDirect.append( cSiteID );
//
////        if( access( strTempFDirect.c_str(), 0 ) == -1 )
////        {
////            if( mkdir( strTempFDirect.c_str(), 0777) )
////            {
////                return false;
////            }
////        }
//
//
//        strTempFDirect.append( "/" );
//        string strTLPath = strTempFDirect;
//        string strTRPath = strTempFDirect;
//
//        SINT nTTPos = pSSet->imgLInfo.strName.rfind("/");
//        string strLTempHead;
//        strLTempHead.assign(pSSet->imgLInfo.strName, nTTPos+1,pSSet->imgLInfo.strName.length());
//        SINT nTTBPos = strLTempHead.rfind(".");
//        string strLTempFinal;
//        strLTempFinal.assign(strLTempHead, 0, nTTBPos);
//        strTLPath.append( strLTempFinal);
//        strTLPath.append( ".dmf" );
//        vecStrMatchFiles.push_back( strTLPath );
//
//        nTTPos = pSSet->imgRInfo.strName.rfind("/");
//        strLTempHead;
//        strLTempHead.assign(pSSet->imgRInfo.strName, nTTPos+1,pSSet->imgRInfo.strName.length());
//        nTTBPos = strLTempHead.rfind(".");
//        strLTempFinal;
//        strLTempFinal.assign(strLTempHead, 0, nTTBPos);
//        strTRPath.append( strLTempFinal);
//        strTRPath.append( ".dmf" );
//        vecStrMatchFiles.push_back( strTRPath );
//
//        CmlWBaseProc wb;
//        bool bresult = wb.WideBaseMapping(vecStereoSet,WidePara,vecFPtL, vecFPtR, vecDPtL, vecDPtR, dstPath);
//        CPPUNIT_ASSERT(bresult == true);
//
//    }

}

/**
* @fn TestWideBaseMapping_abnormal()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数异常时最优基线计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc::TestWideBaseMapping_abnormal()
{

}

/**
* @fn TestWideBase3Ds_ok()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数正常时三维点云计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc::TestWideBase3Ds_ok()
{
    WideOptions WidePara;
    WidePara.bIsUsingFeatPt = false;
    WidePara.bIsUsingPartion = false;
//    WidePara.dCoef = cd.dCoef;
//    WidePara.nRadiusX = cd.nColRadius;
//    WidePara.nRadiusY = cd.nRowRadius;
//    WidePara.nStep = cd.nGridSize;
//    WidePara.XResolution = cd.dDEMResolution;

    char * srcPath = "../../../UnitTestData/TestWBaseProc/WideMars/SiteMapping.smp";

    CmlSiteMapProj site;
    CmlWBaseProc wb;
    //读入工程文件
    if( false == site.LoadProj( srcPath ))
    {
        return;
    }
    int nSiteID, nRollID, nImgID;



    vector<StereoSet> vecStereoSet;
    string strProjPath;
    //读入工程文件中的像对信息
    site.GetDealSet( 1, 1, -1, vecStereoSet);
    int nNum = vecStereoSet.size();
    vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum), vecDPtL(nNum), vecDPtR(nNum);
    vector<Pt3d> vecFObjs, vecDObjs, vec3dPts;

    StereoSet* pStereoSet = &vecStereoSet[0];
    CmlFrameImage clsImgL, clsImgR;

    FrameImgInfo ImgLinfo, ImgRinfo;
    ImgLinfo = pStereoSet->imgLInfo;
    ImgRinfo = pStereoSet->imgRInfo;

    clsImgL.LoadFile(ImgLinfo.strImgPath.c_str());
    clsImgR.LoadFile(ImgRinfo.strImgPath.c_str());

    //特征点匹配
    vector<Pt2d> vecFLPts, vecFRPts, vecDLPts, vecDRPts;
    wb.WideFeaMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, vecFLPts, vecFRPts);

    vector<Pt2d> temFPtL, temFPtR;
    temFPtL = vecFLPts;
    temFPtR = vecFRPts;
    wb.WideDenseMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, temFPtL, temFPtR, vecDLPts, vecDRPts);

    // 前方交会生成三维点
    bool bresult = wb.WideBase3Ds(pStereoSet, vecFLPts, vecFRPts, vecFObjs);
    CPPUNIT_ASSERT(bresult == true);


}


/**
* @fn WideDenseMatch_ok()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数正常时三维点云计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc:: TestWideDenseMatch_ok()
{
    WideOptions WidePara;
    WidePara.bIsUsingFeatPt = false;
    WidePara.bIsUsingPartion = false;
//    WidePara.dCoef = cd.dCoef;
//    WidePara.nRadiusX = cd.nColRadius;
//    WidePara.nRadiusY = cd.nRowRadius;
//    WidePara.nStep = cd.nGridSize;
//    WidePara.XResolution = cd.dDEMResolution;

    char * srcPath = "../../../UnitTestData/TestWBaseProc/WideMars/SiteMapping.smp";

    CmlSiteMapProj site;
    CmlWBaseProc wb;
    //读入工程文件
    if( false == site.LoadProj( srcPath ))
    {
        return;
    }
    int nSiteID, nRollID, nImgID;



    vector<StereoSet> vecStereoSet;
    string strProjPath;
    //读入工程文件中的像对信息
    site.GetDealSet( 1, 1, -1, vecStereoSet);
    int nNum = vecStereoSet.size();
    vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum), vecDPtL(nNum), vecDPtR(nNum);
    vector<Pt3d> vecFObjs, vecDObjs, vec3dPts;

    StereoSet* pStereoSet = &vecStereoSet[0];
    CmlFrameImage clsImgL, clsImgR;

    FrameImgInfo ImgLinfo, ImgRinfo;
    ImgLinfo = pStereoSet->imgLInfo;
    ImgRinfo = pStereoSet->imgRInfo;

    clsImgL.LoadFile(ImgLinfo.strImgPath.c_str());
    clsImgR.LoadFile(ImgRinfo.strImgPath.c_str());

    //特征点匹配
    vector<Pt2d> vecFLPts, vecFRPts, vecDLPts, vecDRPts;
    wb.WideFeaMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, vecFLPts, vecFRPts);

    vector<Pt2d> temFPtL, temFPtR;
    temFPtL = vecFLPts;
    temFPtR = vecFRPts;
    bool bresult = wb.WideDenseMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, temFPtL, temFPtR, vecDLPts, vecDRPts);
    CPPUNIT_ASSERT(bresult == true);
}

/**
* @fn WideFeaMatch_ok()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数正常时三维点云计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc:: TestWideFeaMatch_ok()
{
    WideOptions WidePara;
    WidePara.bIsUsingFeatPt = false;
    WidePara.bIsUsingPartion = false;
//    WidePara.dCoef = cd.dCoef;
//    WidePara.nRadiusX = cd.nColRadius;
//    WidePara.nRadiusY = cd.nRowRadius;
//    WidePara.nStep = cd.nGridSize;
//    WidePara.XResolution = cd.dDEMResolution;

    char * srcPath = "../../../UnitTestData/TestWBaseProc/WideMars/SiteMapping.smp";

    CmlSiteMapProj site;
    CmlWBaseProc wb;
    //读入工程文件
    if( false == site.LoadProj( srcPath ))
    {
        return;
    }
    int nSiteID, nRollID, nImgID;



    vector<StereoSet> vecStereoSet;
    string strProjPath;
    //读入工程文件中的像对信息
    site.GetDealSet( 1, 1, -1, vecStereoSet);
    int nNum = vecStereoSet.size();
    vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum), vecDPtL(nNum), vecDPtR(nNum);
    vector<Pt3d> vecFObjs, vecDObjs, vec3dPts;

    StereoSet* pStereoSet = &vecStereoSet[0];
    CmlFrameImage clsImgL, clsImgR;

    FrameImgInfo ImgLinfo, ImgRinfo;
    ImgLinfo = pStereoSet->imgLInfo;
    ImgRinfo = pStereoSet->imgRInfo;

    clsImgL.LoadFile(ImgLinfo.strImgPath.c_str());
    clsImgR.LoadFile(ImgRinfo.strImgPath.c_str());

    //特征点匹配
    vector<Pt2d> vecFLPts, vecFRPts, vecDLPts, vecDRPts;
    bool bresult = wb.WideFeaMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, vecFLPts, vecFRPts);
    CPPUNIT_ASSERT(bresult == true);

}

/**
* @fn WideFeaMatch_ok()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数正常时三维点云计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void  CTestWBaseProc:: TestWidePtsFilter_ok()
{
    WideOptions WidePara;
    WidePara.bIsUsingFeatPt = false;
    WidePara.bIsUsingPartion = false;
//    WidePara.dCoef = cd.dCoef;
//    WidePara.nRadiusX = cd.nColRadius;
//    WidePara.nRadiusY = cd.nRowRadius;
//    WidePara.nStep = cd.nGridSize;
//    WidePara.XResolution = cd.dDEMResolution;

    char * srcPath = "../../../UnitTestData/TestWBaseProc/WideMars/SiteMapping.smp";

    CmlSiteMapProj site;
    CmlWBaseProc wb;
    //读入工程文件
    if( false == site.LoadProj( srcPath ))
    {
        return;
    }
    int nSiteID, nRollID, nImgID;



    vector<StereoSet> vecStereoSet;
    string strProjPath;
    //读入工程文件中的像对信息
    site.GetDealSet( 1, 1, -1, vecStereoSet);
    int nNum = vecStereoSet.size();
    vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum), vecDPtL(nNum), vecDPtR(nNum);
    vector<Pt3d> vecFObjs, vecDObjs, vec3dPts;

    StereoSet* pStereoSet = &vecStereoSet[0];
    CmlFrameImage clsImgL, clsImgR;

    FrameImgInfo ImgLinfo, ImgRinfo;
    ImgLinfo = pStereoSet->imgLInfo;
    ImgRinfo = pStereoSet->imgRInfo;

    clsImgL.LoadFile(ImgLinfo.strImgPath.c_str());
    clsImgR.LoadFile(ImgRinfo.strImgPath.c_str());

    //特征点匹配
    vector<Pt2d> vecFLPts, vecFRPts, vecDLPts, vecDRPts;
    wb.WideFeaMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, vecFLPts, vecFRPts);

    vector<Pt2d> temFPtL, temFPtR;
    temFPtL = vecFLPts;
    temFPtR = vecFRPts;
    wb.WideDenseMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, temFPtL, temFPtR, vecDLPts, vecDRPts);

    wb.WideBase3Ds(pStereoSet, vecFLPts, vecFRPts, vecFObjs);
    wb.WideBase3Ds(pStereoSet, vecDLPts, vecDRPts, vecDObjs);
    bool bresult = wb.WidePtsFilter(vecFObjs, vecDObjs, vec3dPts);

    CPPUNIT_ASSERT(bresult == true);
}

/**
* @fn TestmlBestBase_ok()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数正常时最优基线计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc::TestmlBestBase_ok()
{
    CmlWBaseProc cls;
    InOriPara mlNav;
    InOriPara mlPan;
    mlNav.f=0.01467;
    mlPan.f=0.043;
    DOUBLE *dBestBase;
    DOUBLE dOptiBase;
    DOUBLE dFixBase=0.3;
    DOUBLE dPixel=0.000004;
    DOUBLE dTarget=400;
    DOUBLE dThresHold=0.000001;
    DOUBLE dIterTime=100;
    UINT nWidth=1024;
    bool flag;
    double result = cls.mlBestBase(mlNav,mlPan,dOptiBase,dFixBase,dPixel,dTarget, nWidth,dBestBase,dThresHold, dIterTime);
    if(result>0)
        flag=true;
    else
        flag=false;
    CPPUNIT_ASSERT(flag == true);

}

/**
* @fn TestmlBestBase_abnormal()
* @date 2011.12.1
* @author 彭嫚
* @brief 测试输入参数异常时最优基线计算功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestWBaseProc::TestmlBestBase_abnormal()
{



}

/** @brief TearDown
  *
  * @todo: document this function
  */
void CTestWBaseProc::tearDown()
{

}

/** @brief SetUp
  *
  * @todo: document this function
  */
void CTestWBaseProc::setUp()
{

}

