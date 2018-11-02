/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestStereoProc.cpp
* @date 2012.2.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 立体影像处理测试源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#include "TestStereoProc.h"
#include "../mlRVML/mlStereoProc.h"
#include "../mlRVML/mlFrameImage.h"
#include "../mlRVML/mlSatMapping.h"
#include "PtFilesRW.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestStereoProc,"alltest" );

CTestStereoProc::CTestStereoProc()
{
    //ctor
}

CTestStereoProc::~CTestStereoProc()
{
    //dtor
}
/** @brief TearDown
  *
  * @todo: document this function
  */
void CTestStereoProc::tearDown()
{

}

/** @brief SetUp
  *
  * @todo: document this function
  */
void CTestStereoProc::setUp()
{

}

/**
* @fn TestGetEpipolarImg_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试利用Ransac方法剔除立体匹配点粗差功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestGetEpipolarImg_ok()
{
    CmlFrameImage LImg,RImg;
    LImg.LoadFile("../../../UnitTestData/TestStereoProc/L_1.bmp");
    RImg.LoadFile("../../../UnitTestData/TestStereoProc/R_1.bmp");
    CmlStereoProc cls;
    //左片
    LImg.m_InOriPara.x = 684.565648;
    LImg.m_InOriPara.y = LImg.GetHeight() - 1 - 513.464857;// Img.GetH()-1-
    LImg.m_InOriPara.f = 1864.761157;
    //右片
    RImg.m_InOriPara.x = 732.252679;
    RImg.m_InOriPara.y = RImg.GetHeight() - 1 - 522.395535;//
    RImg.m_InOriPara.f = 1864.674388;
    RImg.m_ExOriPara.pos.X = 0.302785;
    RImg.m_ExOriPara.pos.Y = 0.002498;
    RImg.m_ExOriPara.pos.Z = 0.006235;
    RImg.m_ExOriPara.ori.omg = -0.001026;
    RImg.m_ExOriPara.ori.phi = 0.038904;
    RImg.m_ExOriPara.ori.kap = 0.000070;
    cls.m_pDataL = &LImg;
    cls.m_pDataR = &RImg;
    LImg.GetUnDistortImg();
    RImg.GetUnDistortImg();
    bool result =cls.mlGetEpipolarImg( );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetEpipolarImg2_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试将非核线影像重采样成核线影像功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestGetEpipolarImg2_ok()
{
    CmlFrameImage LImg,RImg;
    LImg.LoadFile("../../../UnitTestData/TestStereoProc/L_1.bmp");
    RImg.LoadFile("../../../UnitTestData/TestStereoProc/R_1.bmp");
    CmlStereoProc cls;
    //左片
    LImg.m_InOriPara.x = 684.565648;
    LImg.m_InOriPara.y = LImg.GetHeight() - 1 - 513.464857;// Img.GetH()-1-
    LImg.m_InOriPara.f = 1864.761157;
    //右片
    RImg.m_InOriPara.x = 732.252679;
    RImg.m_InOriPara.y = RImg.GetHeight() - 1 - 522.395535;//
    RImg.m_InOriPara.f = 1864.674388;
    RImg.m_ExOriPara.pos.X = 0.302785;
    RImg.m_ExOriPara.pos.Y = 0.002498;
    RImg.m_ExOriPara.pos.Z = 0.006235;
    RImg.m_ExOriPara.ori.omg = -0.001026;
    RImg.m_ExOriPara.ori.phi = 0.038904;
    RImg.m_ExOriPara.ori.kap = 0.000070;
    cls.m_pDataL = &LImg;
    cls.m_pDataR = &RImg;
    LImg.GetUnDistortImg();
    RImg.GetUnDistortImg();
    CmlFrameImage* pFImgL = (CmlFrameImage*)cls.m_pDataL;
    CmlFrameImage* pFImgR = (CmlFrameImage*)cls.m_pDataR;
    CmlRasterBlock ImgL, ImgR;
    ImgL.InitialImg( pFImgL->GetHeight(), pFImgL->GetWidth() );
    ImgR.InitialImg( pFImgR->GetHeight(), pFImgR->GetWidth() );
    bool result = cls.mlGetEpipolarImg( &pFImgL->m_DataBlock, &pFImgR->m_DataBlock, &pFImgL->m_InOriPara, &pFImgL->m_ExOriPara, \
                                        &pFImgR->m_InOriPara, &pFImgR->m_ExOriPara, &ImgL, &ImgR );
    CPPUNIT_ASSERT(result == true);

}
/**
* @fn TestGetRansacPts_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试Ransac去除粗差功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestGetRansacPts_ok()
{
    CmlCE1LinearImage LCE1img,RCE1img;
    CmlSatMapping cls;
    CmlRasterBlock LImgBlock,RImgBlock;
    vector<StereoMatchPt>  MatchPts;
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    RCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_B_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &LImgBlock);
    RCE1img.GetRasterGrayBlock(RCE1img.GetBands(),0, 0, RCE1img.GetWidth(), RCE1img.GetHeight(), (UINT)1, &RImgBlock);
    cls.ConstractAdjust(LImgBlock);
    vector<DOUBLE> vxl, vyl, vxr, vyr;
    UINT nPtNum = SiftMatch((SCHAR*)LImgBlock.GetData(),LImgBlock.GetW(),LImgBlock.GetH(),8,(SCHAR*)RImgBlock.GetData(),RImgBlock.GetW(),RImgBlock.GetH(),8,vxl,vyl,vxr,vyr,200,0.5);
    StereoMatchPt tmpMatch;
    for ( UINT k = 0; k < nPtNum; k++)
    {
        tmpMatch.ptLInImg.X = vxl.at(k);
        tmpMatch.ptLInImg.Y = vyl.at(k);
        tmpMatch.ptRInImg.X = vxr.at(k);
        tmpMatch.ptRInImg.Y = vyr.at(k);
        MatchPts.push_back(tmpMatch);
    }
    CmlStereoProc stp;
    bool result = stp.mlGetRansacPts( MatchPts, 3, 0.98 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetRansacPts2_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试Ransac去除粗差功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestGetRansacPts2_ok()
{
    CmlCE1LinearImage LCE1img,RCE1img;
    CmlSatMapping cls;
    CmlRasterBlock LImgBlock,RImgBlock;
    vector<StereoMatchPt>  MatchPts;
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    RCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_B_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &LImgBlock);
    RCE1img.GetRasterGrayBlock(RCE1img.GetBands(),0, 0, RCE1img.GetWidth(), RCE1img.GetHeight(), (UINT)1, &RImgBlock);
    cls.ConstractAdjust(LImgBlock);
    //cls.GetMatchPts(MatchPts,LImgBlock,8,RImgBlock,8,200,0.5,3,0.98);
    vector<DOUBLE> vxl, vyl, vxr, vyr;
    UINT nPtNum = SiftMatch((SCHAR*)LImgBlock.GetData(),LImgBlock.GetW(),LImgBlock.GetH(),8,(SCHAR*)RImgBlock.GetData(),RImgBlock.GetW(),RImgBlock.GetH(),8,vxl,vyl,vxr,vyr,200,0.5);
    StereoMatchPt tmpMatch;
    for ( UINT k = 0; k < nPtNum; k++)
    {
        tmpMatch.ptLInImg.X = vxl.at(k);
        tmpMatch.ptLInImg.Y = vyl.at(k);
        tmpMatch.ptRInImg.X = vxr.at(k);
        tmpMatch.ptRInImg.Y = vyr.at(k);
        MatchPts.push_back(tmpMatch);
    }
    CmlStereoProc stp;
    vector<StereoMatchPt> ranPts;
    bool result = stp.mlGetRansacPts( MatchPts,ranPts, 3, 0.98 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetRansacPts3_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试Ransac去除粗差功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestGetRansacPts3_ok()
{
    CmlCE1LinearImage LCE1img,RCE1img;
    CmlSatMapping cls;
    CmlRasterBlock LImgBlock,RImgBlock;
    vector<StereoMatchPt>  MatchPts;
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    RCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_B_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &LImgBlock);
    RCE1img.GetRasterGrayBlock(RCE1img.GetBands(),0, 0, RCE1img.GetWidth(), RCE1img.GetHeight(), (UINT)1, &RImgBlock);
    cls.ConstractAdjust(LImgBlock);
    vector<DOUBLE> vxl, vyl, vxr, vyr;
    UINT nPtNum = SiftMatch((SCHAR*)LImgBlock.GetData(),LImgBlock.GetW(),LImgBlock.GetH(),8,(SCHAR*)RImgBlock.GetData(),RImgBlock.GetW(),RImgBlock.GetH(),8,vxl,vyl,vxr,vyr,200,0.5);
    CmlStereoProc stp;
    vector<StereoMatchPt> vecRanPts;
    bool result = stp.mlGetRansacPts( vxl, vyl, vxr, vyr, vecRanPts, 3, 0.98 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestFilterPtsByMedian_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试中值滤波功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestFilterPtsByMedian_ok()
{
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    vector<StereoMatchPt> vecRanPts;
    StereoMatchPt  tmpMatPts;
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestStereoProc/L_1_img.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestStereoProc/R_1_img.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    CmlStereoProc cls;
    bool result = cls.mlFilterPtsByMedian(vecRanPts,5,0.1,5);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestTemplateMatchInFeatPt_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试图像间特征点模板匹配功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestTemplateMatchInFeatPt_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgL.SmoothByGuassian(  7, 0.6 );
    clsImgL.ExtractFeatPtByForstner( 5 );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_1.tif"  );
    clsImgR.SmoothByGuassian(  7, 0.6 );
    clsImgR.ExtractFeatPtByForstner( 5 );

    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;

    MLRect rectSearch;
    rectSearch.dYMax = 2;
    rectSearch.dYMin = -2;
    rectSearch.dXMax = 5;
    rectSearch.dXMin = 170;
    bool result = cls.mlTemplateMatchInFeatPt( rectSearch, 17 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestTemplateMatch_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试立体图像间模板匹配功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestTemplateMatch_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgL.SmoothByGuassian(  7, 0.6 );
    clsImgL.ExtractFeatPtByForstner( 5 );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_1.tif"  );
    clsImgR.SmoothByGuassian(  7, 0.6 );
    clsImgR.ExtractFeatPtByForstner( 5 );

    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;

    MLRect rectSearch;
    rectSearch.dYMax = 2;
    rectSearch.dYMin = -2;
    rectSearch.dXMax = 5;
    rectSearch.dXMin = 170;
    CmlFrameImage* pFImgL = (CmlFrameImage*)cls.m_pDataL;
    CmlFrameImage* pFImgR = (CmlFrameImage*)cls.m_pDataR;
    bool result = cls.mlTemplateMatch( &(pFImgL->m_DataBlock), &(pFImgR->m_DataBlock), pFImgL->m_vecFeaPtsList, pFImgR->m_vecFeaPtsList, cls.m_vecFeatMatchPt, \
                                       rectSearch, 7 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestTemplateMatchInRegion_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试根据左影像上一点取得右影像上同名点功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestTemplateMatchInRegion_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif"  );
    Pt2i ptLeft,ptRight;
    ptLeft.X = 553;
    ptLeft.Y = 318;
    bool result = cls.mlTemplateMatchInRegion( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, ptLeft, ptRight,-5, 5, -5, 5, 7 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestLsMatchInFrameImg_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试面阵影像最小二乘匹配，得到子像素匹配点功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestLsMatchInFrameImg_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif"  );
    vector<StereoMatchPt> vecMatchPt;
    StereoMatchPt tmp;
    tmp.ptLInImg.X = tmp.ptRInImg.X = 553;
    tmp.ptRInImg.X = tmp.ptRInImg.Y = 318;
    vecMatchPt.push_back(tmp);
    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;
    bool result = cls.mlLsMatchInFrameImg( vecMatchPt , 7 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestLsMatch_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试面阵影像最小二乘匹配，根据左影像上一点取得右影像上同名点功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestLsMatch_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif"  );
    DOUBLE dRx,dRy;
    dRx = 553;
    dRy = 318;
    DOUBLE dCoef;
    bool result = cls.mlLsMatch( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, 553, 318, dRx, dRy, 7, dCoef );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestInterSectionInFrameImg_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试面阵影像空间前方交会功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestInterSectionInFrameImg_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_1.tif"  );
    clsImgL.m_InOriPara.f = 1500;
    clsImgR.m_InOriPara.f = 1500;
    clsImgR.m_ExOriPara.pos.Z = 0.3;
    vector<StereoMatchPt> vecMatchPt;
    vector<Pt3d> vecPts;
    StereoMatchPt tmp;
    tmp.ptLInImg.X =  553;
    tmp.ptRInImg.X =  318;
    tmp.ptRInImg.X = 529;
    tmp.ptRInImg.Y = 316;
    vecMatchPt.push_back(tmp);
    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;
    bool result = cls.mlInterSectionInFrameImg(  vecMatchPt,vecPts, 0.0001 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestInterSectionInFrameImg2_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试面阵影像空间前方交会功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestInterSectionInFrameImg2_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_1.tif"  );
    clsImgL.m_InOriPara.f = 1500;
    clsImgR.m_InOriPara.f = 1500;
    clsImgR.m_ExOriPara.pos.Z = 0.3;
    vector<StereoMatchPt> vecMatchPt;
    StereoMatchPt tmp;
    tmp.ptLInImg.X =  553;
    tmp.ptRInImg.X =  318;
    tmp.ptRInImg.X = 529;
    tmp.ptRInImg.Y = 316;
    vecMatchPt.push_back(tmp);
    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;
    bool result = cls.mlInterSectionInFrameImg(  vecMatchPt, 0.0001 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestInterSectionPt2d_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试不含畸变校正功能的通用空间前方交会函数功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestInterSectionPt2d_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_1.tif"  );
    clsImgL.m_InOriPara.f = 1500;
    clsImgR.m_InOriPara.f = 1500;
    clsImgR.m_ExOriPara.pos.Z = 0.3;
    vector<StereoMatchPt> vecMatchPt;
    StereoMatchPt tmp;
    tmp.ptLInImg.X =  553;
    tmp.ptRInImg.X =  318;
    tmp.ptRInImg.X = 529;
    tmp.ptRInImg.Y = 316;
    vecMatchPt.push_back(tmp);
    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;
    SINT nWL,nHL,nWR,nHR;
    nWL = cls.m_pDataL->GetWidth();
    nHL = cls.m_pDataL->GetHeight();
    nWR = cls.m_pDataR->GetWidth();
    nHR = cls.m_pDataR->GetHeight();
    CmlFrameImage* pImgL = (CmlFrameImage*)(cls.m_pDataL);
    CmlFrameImage* pImgR = (CmlFrameImage*)(cls.m_pDataR);
    Pt3d tmpXYZ;
    bool result = cls.mlInterSection( tmp.ptLInImg, tmp.ptRInImg, nHL, nHR, tmpXYZ, &pImgL->m_InOriPara, &pImgL->m_ExOriPara, &pImgR->m_InOriPara, &pImgR->m_ExOriPara, 0.0001 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestInterSection_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试不含畸变校正功能的通用空间前方交会函数功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestInterSection_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_1.tif"  );
    clsImgL.m_InOriPara.f = 1500;
    clsImgR.m_InOriPara.f = 1500;
    clsImgR.m_ExOriPara.pos.Z = 0.3;
    vector<StereoMatchPt> vecMatchPt;
    StereoMatchPt tmp;
    tmp.ptLInImg.X =  553;
    tmp.ptRInImg.X =  318;
    tmp.ptRInImg.X = 529;
    tmp.ptRInImg.Y = 316;
    vecMatchPt.push_back(tmp);
    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;
    SINT nWL,nHL,nWR,nHR;
    nWL = cls.m_pDataL->GetWidth();
    nHL = cls.m_pDataL->GetHeight();
    nWR = cls.m_pDataR->GetWidth();
    nHR = cls.m_pDataR->GetHeight();
    CmlFrameImage* pImgL = (CmlFrameImage*)(cls.m_pDataL);
    CmlFrameImage* pImgR = (CmlFrameImage*)(cls.m_pDataR);
    Pt3d tmpXYZ;

    CmlMat matRotaL, matRotaR, matA;
    OPK2RMat( &pImgL->m_ExOriPara.ori, &matRotaL );
    OPK2RMat( &pImgR->m_ExOriPara.ori, &matRotaR );

    DOUBLE x1, y1, x2, y2;
    x1 = tmp.ptLInImg.X - pImgL->m_InOriPara.x;
    y1 = ( nHL - 1 - tmp.ptLInImg.Y ) - pImgL->m_InOriPara.y;
    x2 = tmp.ptRInImg.X - pImgR->m_InOriPara.x;
    y2 = ( nHR - 1 - tmp.ptRInImg.Y ) -pImgR->m_InOriPara.y;
    bool result = cls.mlInterSection( x1, y1, x2, y2, &matRotaL, &matRotaR, pImgL->m_ExOriPara.pos, pImgR->m_ExOriPara.pos, pImgL->m_InOriPara.f, pImgR->m_InOriPara.f, tmpXYZ, 0.0001 );
    CPPUNIT_ASSERT(result == true);

}
/**
* @fn TestDenseMatch_ok
* @date 2012.2.10
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试影像密集匹配功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestDenseMatch_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgL.SmoothByGuassian(  7, 0.6 );
    clsImgL.ExtractFeatPtByForstner( 5 );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_1.tif"  );
    clsImgR.SmoothByGuassian(  7, 0.6 );
    clsImgR.ExtractFeatPtByForstner( 5 );

    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;

    MLRect rectSearch;
    rectSearch.dYMax = 2;
    rectSearch.dYMin = -2;
    rectSearch.dXMax = 5;
    rectSearch.dXMin = 170;
    CmlFrameImage* pFImgL = (CmlFrameImage*)cls.m_pDataL;
    CmlFrameImage* pFImgR = (CmlFrameImage*)cls.m_pDataR;
    cls.mlTemplateMatch( &(pFImgL->m_DataBlock), &(pFImgR->m_DataBlock), pFImgL->m_vecFeaPtsList, pFImgR->m_vecFeaPtsList, cls.m_vecFeatMatchPt, \
                         rectSearch, 7 );
    if(cls.m_vecFeatMatchPt.size()== 0 )
    {
        return ;
    }
    WideOptions WidePara;
    vector<Pt2d> vecDPtsL, vecDPtsR;

    bool result = cls.mlDenseMatch( &(pFImgL->m_DataBlock), &(pFImgR->m_DataBlock), cls.m_vecFeatMatchPt, WidePara, \
                                    vecDPtsL, vecDPtsR );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestDisEstimate_ok
* @date 2012.2.10
* @author 彭嫚
* @brief 测试根据正确匹配的特征点生成三角网进行视差范围预测功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestDisEstimate_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_1.tif" );
    clsImgL.SmoothByGuassian(  7, 0.6 );
    clsImgL.ExtractFeatPtByForstner( 5 );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_1.tif"  );
    clsImgR.SmoothByGuassian(  7, 0.6 );
    clsImgR.ExtractFeatPtByForstner( 5 );

    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;

    MLRect rectSearch;
    rectSearch.dYMax = 2;
    rectSearch.dYMin = -2;
    rectSearch.dXMax = 5;
    rectSearch.dXMin = 170;
    CmlFrameImage* pFImgL = (CmlFrameImage*)cls.m_pDataL;
    CmlFrameImage* pFImgR = (CmlFrameImage*)cls.m_pDataR;
    cls.mlTemplateMatch( &(pFImgL->m_DataBlock), &(pFImgR->m_DataBlock), pFImgL->m_vecFeaPtsList, pFImgR->m_vecFeaPtsList, cls.m_vecFeatMatchPt, \
                         rectSearch, 7 );
    if(cls.m_vecFeatMatchPt.size()== 0 )
    {
        return ;
    }
    vector<Pt2i> vecPt,  vecDisx, vecDisy;
    vector<Pt3d> vFeaPtLx, vFeaPtLy;
    WideOptions WidePara;
    SINT nTemplateSize = WidePara.nTemplateSize;
    DOUBLE dCoef = WidePara.dCoef;
    UINT nXOffSet = WidePara.nXOffSet;
    UINT nYOffSet = WidePara.nYOffSet;
    UINT nStep = WidePara.nStep;
    UINT nRadiusX = WidePara.nRadiusX;
    UINT nRadiusY = WidePara.nRadiusY;

    for(UINT i=0; i < cls.m_vecFeatMatchPt.size(); i++)
    {
        StereoMatchPt pt1 = cls.m_vecFeatMatchPt[i];

        Pt3d tempPt1,tempPt2;
        tempPt1.X = SINT (pt1.ptLInImg.X);
        tempPt1.Y = SINT (pt1.ptLInImg.Y);
        tempPt1.Z = SINT (-pt1.ptLInImg.X+pt1.ptRInImg.X);
        vFeaPtLx.push_back(tempPt1);

        tempPt2.X = SINT (pt1.ptLInImg.X);
        tempPt2.Y = SINT (pt1.ptLInImg.Y);
        tempPt2.Z = SINT (-pt1.ptLInImg.Y+pt1.ptRInImg.Y);
        vFeaPtLy.push_back(tempPt2);
    }


    //根据输入的特征点坐标根据(x,y,列视差)构成三角网生成列视差范围vecDisx
    SINT nSize1;
    nSize1 = cls.mlDisEstimate(vFeaPtLx, nStep, nRadiusX,vecPt,vecDisx);
    if(nSize1>0)
        CPPUNIT_ASSERT(1);
    else
        CPPUNIT_ASSERT(0);

}
/**
* @fn TestUniquePt_ok
* @date 2012.2.10
* @author 彭嫚
* @brief 测试去掉特征点匹配中重复的点功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestUniquePt_ok()
{
    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    clsImgL.LoadFile( "../../../UnitTestData/TestStereoProc/L_2.tif" );
    clsImgL.SmoothByGuassian(  7, 0.6 );
    clsImgL.ExtractFeatPtByForstner( 5 );
    clsImgR.LoadFile( "../../../UnitTestData/TestStereoProc/R_2.tif"  );
    clsImgR.SmoothByGuassian(  7, 0.6 );
    clsImgR.ExtractFeatPtByForstner( 5 );

    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;

    MLRect rectSearch;
    rectSearch.dYMax = 2;
    rectSearch.dYMin = -2;
    rectSearch.dXMax = 5;
    rectSearch.dXMin = 170;
    CmlFrameImage* pFImgL = (CmlFrameImage*)cls.m_pDataL;
    CmlFrameImage* pFImgR = (CmlFrameImage*)cls.m_pDataR;

    //sift匹配
    vector<double> vxl;
    vector<double> vyl;
    vector<double> vxr;
    vector<double> vyr;

    WideOptions WidePara;
    UINT nMaxCheck = WidePara.nMaxCheck;
    DOUBLE dRatio = WidePara.dRatio;

    UINT nPtNum = SiftMatch((char*)pFImgL->m_DataBlock.GetData( ),pFImgL->m_DataBlock.GetW(),pFImgL->m_DataBlock.GetH(),8,(char*)pFImgR->m_DataBlock.GetData( ),pFImgR->m_DataBlock.GetW(),pFImgR->m_DataBlock.GetH(),8,vxl,vyl,vxr,vyr,nMaxCheck,dRatio);

    vector<StereoMatchPt> MatchPts;
    SINT nCount;
    StereoMatchPt tempPts;
    for(int i=0; i<nPtNum; i++)
    {
        tempPts.ptLInImg.X = vxl[i];
        tempPts.ptLInImg.Y = vyl[i];
        tempPts.ptRInImg.X = vxr[i];
        tempPts.ptRInImg.Y = vyr[i];
        MatchPts.push_back(tempPts);
    }

    bool bresult = cls.mlUniquePt(MatchPts);

    CPPUNIT_ASSERT(bresult == true);

}

/**
* @fn TestDisparityMap_ok
* @date 2012.2.10
* @author 彭嫚 ylliu@irsa.ac.cn
* @brief 测试根据密集匹配点生成视差图的功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestStereoProc::TestDisparityMap_ok()
{
    char* srcPathL = "../../../UnitTestData/TestStereoProc/L_8.dmf";
    char* srcPathR = "../../../UnitTestData/TestStereoProc/R_8.dmf";
    char* dstPath = "../../../UnitTestData/TestStereoProc/Dis.tif";
    CPtFilesRW clsFileRW;
    ImgPtSet imgPtL, imgPtR;
    clsFileRW.LoadImgPtSet(srcPathL, imgPtL);
    clsFileRW.LoadImgPtSet(srcPathR, imgPtR);

    CmlStereoProc cls;
    bool bresult = cls.DisparityMap(imgPtL, imgPtR, dstPath);

    CPPUNIT_ASSERT(bresult == true);

}


