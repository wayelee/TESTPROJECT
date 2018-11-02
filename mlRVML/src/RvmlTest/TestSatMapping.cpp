/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestSatMapping.cpp
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像制图源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "TestSatMapping.h"
#include "../mlRVML/mlSatMapping.h"
#include "SatMapProj.h"
#include "../mlRVML/mlBlockCalculation.h"
#include "PtFilesRW.h"


CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestSatMapping,"alltest" );
/**
* @fn CTestSatMapping()
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestSatMapping::CTestSatMapping()
{
    //ctor
}
/**
* @fn ~CTestSatMapping()
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestSatMapping::~CTestSatMapping()
{
    //dtor
}
/**
* @fn setup()
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 系统默认的初始化函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestSatMapping::setUp()
{

}
/**
* @fn tearDown()
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 系统默认的销毁函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestSatMapping::tearDown()
{

}
/**
* @fn TestSatMatch_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试卫星影像特征点匹配功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMatch_ok()
{
    SCHAR *sLimgPath = "../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp";
    SCHAR *sRimgPath = "../../../UnitTestData/TestSatMappingData/CE-1/data/0562_B_1001.bmp";
    vector<StereoMatchPt> vecRanPts;
    SatOptions sat;
    CmlSatMapping cls;
    bool result = cls.mlSatMatch( sLimgPath, sRimgPath, sat, vecRanPts );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE1MappingByPtsLeft_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试由CE-1卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和以左片为基准生成DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestCE1MappingByPtsLeft_ok()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-1/CE1Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,150, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_F_1001.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_B_1001.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    bool result = cls.mlCE1MappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE1MappingByPtsRight_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试由CE-1卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和以右片为基准生成DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestCE1MappingByPtsRight_ok()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-1/CE1Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,150, false);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_F_1001.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_B_1001.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    pro.sDemPath = "../ ";
    bool result = cls.mlCE1MappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == true);
}

/**
* @fn TestWriteBLH_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试将三维点云数据生成规则格网DEM，并以GeoTiff格式存储功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestWriteBLH_ok()
{
    CmlSatMapping cls;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/result/dem.tif";
    SCHAR * sblhPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/FNB-BLH-1000-0562-lagrange.txt";
    std::ifstream stmRead(sblhPath);
    SINT ncount = 0;
    stmRead>>ncount;
    Pt3d tmpblh;
    vector<Pt3d> vecblh;
    for( SINT i = 0; i< ncount; i++ )
    {
        stmRead>>tmpblh.Y>>tmpblh.X>>tmpblh.Z;
        vecblh.push_back(tmpblh);
    }
    bool result = cls.WriteBLH(sdemPath,vecblh,150,150);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGenerateDOM_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试生成DOM并以GeoTiff格式存储功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateDOM_ok()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock domImg, ImgBlock;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/test_dem.tif";
    SCHAR * sdomPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/result/create_dom.tif";
    SCHAR * srcPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001_InOriPara.txt";
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    SCHAR * slinePath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562-LineEo.txt";
    SCHAR * sanglePath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562-AngleEo.txt";
    SCHAR * simgtimePath =(SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562_B_1000_ImgTime.txt";
    cls.ReadImgScanTime(simgtimePath,LImgTime);
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &ImgBlock);
    cls.ReadCE1InOri(srcPath, LCE1img.m_CE1IOPara );
    cls.ReadLineEo(slinePath,LLineEo);
    cls.ReadAngleEo(sanglePath,LAngleEo);
    LCE1img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE1img.m_vecImgEo );
    LCE1img.mlCE1OPK2RMat(LCE1img.m_vecImgEo,LCE1img.m_vecPosition, LCE1img.m_vecMatrix);
    bool result = sat.GenerateDOM( sdemPath, sdomPath, LCE1img.m_CE1IOPara, ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, domImg, 35, 1, 999999999 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestInterSection_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试前方交会生成三维物方点功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestInterSection_ok()
{
    CmlSatMapping cls;
    SatProj satproj;
    SatOptions satPara;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-1/testCE1Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( satproj, satPara, projPath, demPath, domPath,150, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_F_1001.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_B_1001.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    CmlCE1LinearImage LCE1img, RCE1img;
    bool H1,H2;
    //读入左右影像
    if(LCE1img.LoadFile(satproj.sLimgPath.c_str())&& RCE1img.LoadFile(satproj.sRimgPath.c_str()))
    {
        printf("Images have been loaded!\n");
    }
    else
    {
        printf("Can not load the images,please check the image files!\n");
    }
    //影像内参数赋值
    LCE1img.m_CE1IOPara = satproj.CE1LimgIO;
    RCE1img.m_CE1IOPara = satproj.CE1RimgIO;
    LCE1img.m_CE1IOPara.nSample = LCE1img.GetWidth();
    LCE1img.m_CE1IOPara.nLine = LCE1img.GetHeight();
    RCE1img.m_CE1IOPara.nSample = RCE1img.GetWidth();
    RCE1img.m_CE1IOPara.nLine = RCE1img.GetHeight();
    //外方位元素赋值
    LCE1img.m_vecImgEo = satproj.LimgEo;
    RCE1img.m_vecImgEo = satproj.RimgEo;
    //将外方位转成旋转矩阵
    H1 = LCE1img.mlCE1OPK2RMat(LCE1img.m_vecImgEo,LCE1img.m_vecPosition, LCE1img.m_vecMatrix);
    H2 = RCE1img.mlCE1OPK2RMat(RCE1img.m_vecImgEo,RCE1img.m_vecPosition, RCE1img.m_vecMatrix);
    if(!(H1&&H2))
    {
        printf("Can not convert exterior orientation parameters to ratation matrixs!\n");
    }
    //获得对应影像块
    CmlRasterBlock LimgBlock, RimgBlock;
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &LimgBlock);
    RCE1img.GetRasterGrayBlock(RCE1img.GetBands(),0, 0, RCE1img.GetWidth(), RCE1img.GetHeight(), (UINT)1, &RimgBlock);
    //影像自动对比度
    if(!(cls.ConstractAdjust(LimgBlock)&&cls.ConstractAdjust(RimgBlock)))
    {
        printf("Can not constract adjust the image blocks!\n");
    }
    //密集匹配
    vector<Pt2d> ptSubDenseL,ptSubDenseR;
    WideOptions wid;//密集匹配相关参数设置
    wid.nStep = satPara.nStep;
    wid.nRadiusX = satPara.nRadiusX;
    wid.nRadiusY = satPara.nRadiusY;
    wid.nTemplateSize = satPara.nTemplateSize;
    wid.dCoef = satPara.dCoef;
    wid.nXOffSet = satPara.nXOffSet;
    wid.nYOffSet = satPara.nYOffSet;
    CmlStereoProc stp;
    H1 = stp.mlDenseMatch( &LimgBlock, &RimgBlock, vecRanPts, wid, ptSubDenseL, ptSubDenseR );
    if( !H1 )
    {
        printf("Dense matching has problems!\n");
    }
    //内定向
    vector<Pt2d> vecXYL, vecXYR;
    H1 = LCE1img.mlCE1InOrietation( ptSubDenseL, &LCE1img.m_CE1IOPara, vecXYL );
    H2 = RCE1img.mlCE1InOrietation( ptSubDenseR, &RCE1img.m_CE1IOPara, vecXYR );
    if( !(H1 && H2) )
    {
        printf("Interior orientation has problems!\n");
    }
    //前方交会
    vector<Pt3d> vecBLH;
    bool result = cls.InterSection(ptSubDenseL, ptSubDenseR, vecXYL, vecXYR, LCE1img.m_vecPosition, RCE1img.m_vecPosition, LCE1img.m_vecMatrix, RCE1img.m_vecMatrix, LCE1img.m_CE1IOPara.f, RCE1img.m_CE1IOPara.f, vecBLH );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestConstractAdjust_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试影像自动对比度增强功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestConstractAdjust_ok()
{

    CmlSatMapping cls;
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock LImgBlock;
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),(UINT)0, (UINT)0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &LImgBlock);

    bool result  = cls.ConstractAdjust(LImgBlock);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestConstractAdjust2_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试影像自动对比度增强功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestConstractAdjust2_ok()
{
    CmlSatMapping cls;
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock LImgBlock;
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/other/white.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),(UINT)0, (UINT)0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &LImgBlock);

    bool result  = cls.ConstractAdjust(LImgBlock);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestSatMappingByPtsCE1_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试由CE-1卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMappingByPtsCE1_ok()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-1/testCE1Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,150, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_F_1001.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_B_1001.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    bool result = cls.mlSatMappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestSatMappingByPtsCE2_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试由CE-2卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMappingByPtsCE2_ok()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-2/testCE2Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,50, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-F04-No6-00.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-B04-No6-00.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    bool result = cls.mlSatMappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE2MappingByPtsLeft_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试由CE-2卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和以左片为基准生成DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestCE2MappingByPtsLeft_ok()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-2/testCE2Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,50, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-F04-No6-00.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-B04-No6-00.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    bool result = cls.mlCE2MappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE2MappingByPtsRight_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试由CE-2卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和以右片为基准生成DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestCE2MappingByPtsRight_ok()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-2/testCE2Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,50, false);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-F04-No6-00.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-B04-No6-00.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    bool result = cls.mlCE2MappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGenerateCE2DOM_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试由CE-2卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateCE2DOM_ok()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE2LinearImage LCE2img;
    CmlRasterBlock domImg, ImgBlock;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/test_dem.tif";
    SCHAR * sdomPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/result/create_dom.tif";
    SCHAR * srcPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6_InOriPara.txt";
    LCE2img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6.bmp");
    SCHAR * slinePath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-LineEo.txt";
    SCHAR * sanglePath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-AngleEo.txt";
    SCHAR * simgtimePath =(SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-ImgTime.txt";
    cls.ReadImgScanTime(simgtimePath,LImgTime);
    LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),0, 0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &ImgBlock);
    cls.ReadCE2InOri(srcPath, LCE2img.m_CE2IOPara );
    cls.ReadLineEo(slinePath,LLineEo);
    cls.ReadAngleEo(sanglePath,LAngleEo);
    LCE2img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE2img.m_vecImgEo );
    LCE2img.mlCE2OPK2RMat(LCE2img.m_vecImgEo,LCE2img.m_vecPosition, LCE2img.m_vecMatrix);
    bool result = sat.GenerateCE2DOM( sdemPath, sdomPath, LCE2img.m_CE2IOPara, ImgBlock, LCE2img.m_vecMatrix, LCE2img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == true);
}
/////////////////////////////////////////////////
/**
* @fn TestSatMatch_abnormal1
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试影像不能加载的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMatch_abnormal1()
{
    SCHAR *sLimgPath = " 1.bmp ";
    SCHAR *sRimgPath = " 2.bmp ";
    vector<StereoMatchPt> vecRanPts;
    SatOptions sat;
    CmlSatMapping cls;
    bool result = cls.mlSatMatch( sLimgPath, sRimgPath, sat, vecRanPts );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestSatMatch_abnormal2
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试影像不能正常分块的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMatch_abnormal2()
{
    SCHAR *sLimgPath = "../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp";
    SCHAR *sRimgPath = "../../../UnitTestData/TestSatMappingData/CE-1/data/0562_B_1001.bmp";
    vector<StereoMatchPt> vecRanPts;
    SatOptions sat;
    CmlSatMapping cls;
    sat.BlockOps.nOverlayH = -10;
    bool result = cls.mlSatMatch( sLimgPath, sRimgPath, sat, vecRanPts );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestSatMatch_abnormal3
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试影像无匹配点的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMatch_abnormal3()
{
    SCHAR *sLimgPath = "../../../UnitTestData/TestSatMappingData/other/black.bmp";
    SCHAR *sRimgPath = "../../../UnitTestData/TestSatMappingData/other/white.bmp";
    vector<StereoMatchPt> vecRanPts;
    SatOptions sat;
    CmlSatMapping cls;
    bool result = cls.mlSatMatch( sLimgPath, sRimgPath, sat, vecRanPts );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestCE1MappingByPts_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试无法加载影像的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestCE1MappingByPts_abnormal()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-1/testCE1Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,150, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_F_1001.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_B_1001.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    pro.sLimgPath = " ";
    bool result = cls.mlCE1MappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestCE2MappingByPts_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试无法加载影像的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestCE2MappingByPts_abnormal()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-2/testCE2Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,50, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-F04-No6-00.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-B04-No6-00.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    pro.sLimgPath = " ";
    bool result = cls.mlCE2MappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == false);

}
/**
* @fn TestSatMappingByPts_abnormal1
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试参数输入不正确的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMappingByPts_abnormal1()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-1/testCE1Proj.txt";
    char * demPath = " ";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,150, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_F_1001.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_B_1001.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    bool result = cls.mlSatMappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestSatMappingByPts_abnormal2
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试CE-1内定向参数不正确的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMappingByPts_abnormal2()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-1/testCE1Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,150, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_F_1001.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-1/result/0562_B_1001.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    pro.CE1LimgIO.nCCD_line = 17;
    bool result = cls.mlSatMappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == false);

}
/**
* @fn TestSatMappingByPts_abnormal3
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试CE-2内定向参数不正确的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMappingByPts_abnormal3()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-2/testCE2Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,150, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-F04-No6-00.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-B04-No6-00.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    pro.CE2LimgIO.AngleDeg = 10;
    bool result = cls.mlSatMappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestSatMappingByPts_abnormal4
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试任务标号不正确的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestSatMappingByPts_abnormal4()
{
    CmlSatMapping cls;
    SatProj pro;
    SatOptions ops;
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    ImgPtSet LimgPSet, RimgPSet;
    StereoMatchPt tmpMatPts;
    char * projPath = "../../../UnitTestData/TestSatMappingData/CE-2/testCE2Proj.txt";
    char * demPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dem.tif";
    char * domPath = "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dom.tif";
    CmlSatMapProj proj;
    proj.LoadProj( pro, ops, projPath, demPath, domPath,150, true);
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-F04-No6-00.fmf", LimgPSet );
    clsPtRW.LoadImgPtSet( "../../../UnitTestData/TestSatMappingData/CE-2/result/0581-B04-No6-00.fmf", RimgPSet );
    for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
    {
        tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
        tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
        vecRanPts.push_back(tmpMatPts);
    }
    ops.sMissionName = "CE-N";
    bool result = cls.mlSatMappingByPts( pro, ops, vecRanPts, vecDensePts,vecPres );
    CPPUNIT_ASSERT(result == false);

}
/**
* @fn TestConstractAdjust_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试影像块为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestConstractAdjust_abnormal()
{
    CmlSatMapping cls;
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock LImgBlock;
    bool result  = cls.ConstractAdjust(LImgBlock);
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestInterSection_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试左右点数不一致的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestInterSection_abnormal1()
{
    CmlSatMapping cls;
    CmlCE1LinearImage LCE1img, RCE1img;
    vector<Pt2d> ptSubDenseL,ptSubDenseR;
    Pt2d tmp;
    tmp.X = 1;tmp.Y = 2;
    ptSubDenseL.push_back(tmp);
    vector<Pt2d> vecXYL, vecXYR;
    vector<Pt3d> vecBLH;
    bool result = cls.InterSection(ptSubDenseL, ptSubDenseR, vecXYL, vecXYR, LCE1img.m_vecPosition, RCE1img.m_vecPosition, LCE1img.m_vecMatrix, RCE1img.m_vecMatrix, LCE1img.m_CE1IOPara.f, RCE1img.m_CE1IOPara.f, vecBLH );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestInterSection2_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试输入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestInterSection_abnormal2()
{
    CmlSatMapping cls;
    CmlCE1LinearImage LCE1img, RCE1img;
    vector<Pt2d> ptSubDenseL,ptSubDenseR;
    Pt2d tmp;
    tmp.X = 1;tmp.Y = 2;
    ptSubDenseL.push_back(tmp);
    vector<Pt2d> vecXYL, vecXYR;
    vector<Pt3d> vecBLH;
    bool result = cls.InterSection(ptSubDenseL, ptSubDenseR, vecXYL, vecXYR, LCE1img.m_vecPosition, RCE1img.m_vecPosition, LCE1img.m_vecMatrix, RCE1img.m_vecMatrix, LCE1img.m_CE1IOPara.f, RCE1img.m_CE1IOPara.f, vecBLH );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGenerateCE2DOM_abnormal1
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGenerateCE2DOM函数输入影像块为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateCE2DOM_abnormal1()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE2LinearImage LCE2img;
    CmlRasterBlock domImg, ImgBlock;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/test_dem.tif";
    SCHAR * sdomPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/result/create_dom.tif";
    bool result = sat.GenerateCE2DOM( sdemPath, sdomPath, LCE2img.m_CE2IOPara, ImgBlock, LCE2img.m_vecMatrix, LCE2img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGenerateCE2DOM_abnormal2
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGenerateCE2DOM函数无法打开DEM文件的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateCE2DOM_abnormal2()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE2LinearImage LCE2img;
    CmlRasterBlock domImg, ImgBlock;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    SCHAR * sdemPath = (SCHAR*)" ";
    SCHAR * sdomPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/result/create_dom.tif";
    SCHAR * srcPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6_InOriPara.txt";
    LCE2img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6.bmp");
    LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),0, 0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &ImgBlock);
    bool result = sat.GenerateCE2DOM( sdemPath, sdomPath, LCE2img.m_CE2IOPara, ImgBlock, LCE2img.m_vecMatrix, LCE2img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGenerateCE2DOM_abnormal3
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGenerateCE2DOM函数无法生成DOM的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateCE2DOM_abnormal3()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE2LinearImage LCE2img;
    CmlRasterBlock domImg, ImgBlock;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/test_dem.tif";
    SCHAR * sdomPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/result/create_dom.tif";
    SCHAR * srcPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6_InOriPara.txt";
    LCE2img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6.bmp");
    LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),0, 0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &ImgBlock);
    bool result = sat.GenerateCE2DOM( sdemPath, sdomPath, LCE2img.m_CE2IOPara, ImgBlock, LCE2img.m_vecMatrix, LCE2img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGenerateCE2DOM_abnormal4
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGenerateCE2DOM函数无法将DOM写入文件的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateCE2DOM_abnormal4()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE2LinearImage LCE2img;
    CmlRasterBlock domImg, ImgBlock;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/test_dem.tif";
    SCHAR * sdomPath = (SCHAR*)"../";
    SCHAR * srcPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6_InOriPara.txt";
    LCE2img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6.bmp");
    SCHAR * slinePath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-LineEo.txt";
    SCHAR * sanglePath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-AngleEo.txt";
    SCHAR * simgtimePath =(SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-ImgTime.txt";
    cls.ReadImgScanTime(simgtimePath,LImgTime);
    LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),0, 0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &ImgBlock);
    cls.ReadCE2InOri(srcPath, LCE2img.m_CE2IOPara );
    cls.ReadLineEo(slinePath,LLineEo);
    cls.ReadAngleEo(sanglePath,LAngleEo);
    LCE2img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE2img.m_vecImgEo );
    LCE2img.mlCE2OPK2RMat(LCE2img.m_vecImgEo,LCE2img.m_vecPosition, LCE2img.m_vecMatrix);
    bool result = sat.GenerateCE2DOM( sdemPath, sdomPath, LCE2img.m_CE2IOPara, ImgBlock, LCE2img.m_vecMatrix, LCE2img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == false);

}
/**
* @fn TestGenerateDOM_abnormal1
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGenerateDOM函数输入影像块为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateDOM_abnormal1()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock domImg, ImgBlock;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/test_dem.tif";
    SCHAR * sdomPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/result/create_dom.tif";
    bool result = sat.GenerateDOM( sdemPath, sdomPath, LCE1img.m_CE1IOPara, ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGenerateDOM_abnormal2
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGenerateDOM函数无法打开DEM文件的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateDOM_abnormal2()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock domImg, ImgBlock;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    SCHAR * sdemPath = (SCHAR*)" ";
    SCHAR * sdomPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/result/create_dom.tif";
    SCHAR * srcPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001_InOriPara.txt";
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &ImgBlock);
    bool result = sat.GenerateDOM( sdemPath, sdomPath, LCE1img.m_CE1IOPara, ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGenerateDOM_abnormal3
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGenerateDOM函数无法生成DOM的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateDOM_abnormal3()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock domImg, ImgBlock;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/test_dem.tif";
    SCHAR * sdomPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/result/create_dom.tif";
    SCHAR * srcPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001_InOriPara.txt";
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &ImgBlock);
    bool result = sat.GenerateDOM( sdemPath, sdomPath, LCE1img.m_CE1IOPara, ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGenerateDOM_abnormal4
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGenerateDOM函数无法将DOM写入文件的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestGenerateDOM_abnormal4()
{
    CmlSatMapProj cls;
    CmlSatMapping sat;
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock domImg, ImgBlock;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    SCHAR * sdemPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/test_dem.tif";
    SCHAR * sdomPath = (SCHAR*)"../";
    SCHAR * srcPath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001_InOriPara.txt";
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    SCHAR * slinePath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562-LineEo.txt";
    SCHAR * sanglePath = (SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562-AngleEo.txt";
    SCHAR * simgtimePath =(SCHAR*)"../../../UnitTestData/TestSatMappingData/CE-1/data/0562_B_1000_ImgTime.txt";
    cls.ReadImgScanTime(simgtimePath,LImgTime);
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &ImgBlock);
    cls.ReadCE1InOri(srcPath, LCE1img.m_CE1IOPara );
    cls.ReadLineEo(slinePath,LLineEo);
    cls.ReadAngleEo(sanglePath,LAngleEo);
    LCE1img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE1img.m_vecImgEo );
    LCE1img.mlCE1OPK2RMat(LCE1img.m_vecImgEo,LCE1img.m_vecPosition, LCE1img.m_vecMatrix);
    bool result = sat.GenerateDOM( sdemPath, sdomPath, LCE1img.m_CE1IOPara, ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, domImg,35, 1, 999999999 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestWriteBLH_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlWriteBLH函数无法打开文件的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestSatMapping::TestWriteBLH_abnormal()
{
    CmlSatMapping cls;
    SCHAR * sdemPath = (SCHAR*)"../ ";
    vector<Pt3d> vecblh;
    bool result = cls.WriteBLH(sdemPath,vecblh,150,150);
    CPPUNIT_ASSERT(result == false);
}


