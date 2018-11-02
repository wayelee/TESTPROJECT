/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestLinearImage.cpp
* @date 2011.2.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 线阵影像处理测试源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "TestLinearImage.h"
#include "../mlRVML/mlLinearImage.h"
#include "../mlRVML/mlSatMapping.h"
#include "SatMapProj.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestLinearImage,"alltest" );

/**
* @fn CTestLinearImage
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestLinearImage::CTestLinearImage()
{
    //ctor
}
/**
* @fn ~CTestLinearImage
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestLinearImage::~CTestLinearImage()
{
    //dtor
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
void CTestLinearImage::tearDown()
{

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
void CTestLinearImage::setUp()
{

}
/**
* @fn TestGetEop_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试轨道测控数据多项式内插外方位元素功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetEop_ok()
{
    CmlSatMapProj sat;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    vector<ExOriPara> vecEo;
    sat.ReadImgScanTime("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1000_ImgTime.txt",LImgTime);
    sat.ReadLineEo( "../../../UnitTestData/TestSatMappingData/CE-1/data/0562-LineEo.txt",  LLineEo );
    sat.ReadAngleEo( "../../../UnitTestData/TestSatMappingData/CE-1/data/0562-AngleEo.txt",  LAngleEo );
    CmlLinearImage cls;
    bool result = cls.mlGetEop( LLineEo, LAngleEo, LImgTime, &vecEo );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestBLH2XYZ_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试将物方三维点由月球大地坐标系转成月固坐标系下的空间直角坐标功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestBLH2XYZ_ok()
{
    Pt3d blhPts,xyzPts;
    blhPts.X = -28.85433;
    blhPts.Y = 47.55422;
    blhPts.Z = -4180.12351;
    CmlLinearImage cls;
    bool result = cls.mlBLH2XYZ( blhPts,xyzPts );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestXYZ2BLH_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试将物方三维点由月固坐标系下的空间直角坐标转成月球大地坐标系功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestXYZ2BLH_ok()
{
    Pt3d blhPts,xyzPts;
    xyzPts.X = 1024513.18126680;
    xyzPts.Y = -564496.63770046;
    xyzPts.Z = 1278971.23802258;
    CmlLinearImage cls;
    bool result = cls.mlXYZ2BLH( xyzPts, blhPts );
    CPPUNIT_ASSERT(result == true);

}
/**
* @fn TestGetCE1DOMCoordinate_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试生成CE-1卫星影像DOM格网在原图像上的x,y坐标功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE1DOMCoordinate_ok()
{
    CmlCE1LinearImage LCE1img;
    CmlSatMapProj sat;
    CmlRasterBlock DemBlock,domBlock, ImgBlock;
    sat.ReadCE1InOri("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001_InOriPara.txt",LCE1img.m_CE1IOPara);
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    sat.ReadImgScanTime("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1000_ImgTime.txt",LImgTime);
    sat.ReadLineEo( "../../../UnitTestData/TestSatMappingData/CE-1/data/0562-LineEo.txt",  LLineEo );
    sat.ReadAngleEo( "../../../UnitTestData/TestSatMappingData/CE-1/data/0562-AngleEo.txt",  LAngleEo );
    LCE1img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE1img.m_vecImgEo );
    LCE1img.mlCE1OPK2RMat(LCE1img.m_vecImgEo,LCE1img.m_vecPosition, LCE1img.m_vecMatrix);
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),(UINT)0, (UINT)0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &ImgBlock);
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestSatMappingData/CE-1/data/test_dem.tif");
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    domBlock.InitialImg(DemBlock.GetH(),DemBlock.GetW());
    domBlock.SetGDTType( GDT_Byte );
    vector<Pt3d> vecPtXYZ;
    Pt3d tmpXYZ;
    for( SINT nY = 0; nY < DemBlock.GetH(); nY++ )
    {
        for( SINT nX = 0; nX < DemBlock.GetW(); nX++ )
        {
            DemBlock.GetGeoXYZ( nY, nX, tmpXYZ );
            vecPtXYZ.push_back( tmpXYZ );
        }
    }
    CRasterPt2D ImgSL;
    ImgSL.Initial(domBlock.GetH(),domBlock.GetW());
    DOUBLE trueline = ( LCE1img.m_CE1IOPara.l0 - LCE1img.m_CE1IOPara.nCCD_line ) * LCE1img.m_CE1IOPara.pixsize-LCE1img.m_CE1IOPara.x0;
    /**二分法计算像点坐标,默认是上行**/
    CmlCE1LinearImage ce;
    bool result = ce.mlGetCE1DOMCoordinate( ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, LCE1img.m_CE1IOPara.f, vecPtXYZ, domBlock.GetW(), domBlock.GetH(), ImgSL, trueline, 5, 0.000002 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetCE1DOMCoordinate_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试生成CE-2卫星影像DOM格网在原图像上的x,y坐标功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE2DOMCoordinate_ok()
{
    CmlCE2LinearImage LCE2img;
    CmlSatMapProj sat;
    CmlRasterBlock DemBlock,domBlock, ImgBlock;
    sat.ReadCE2InOri("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6_InOriPara.txt",LCE2img.m_CE2IOPara);
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    sat.ReadImgScanTime("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-ImgTime-00.txt",LImgTime);
    sat.ReadLineEo( "../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-LineEo.txt",  LLineEo );
    sat.ReadAngleEo( "../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-AngleEo.txt",  LAngleEo );
    LCE2img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE2img.m_vecImgEo );
    LCE2img.mlCE2OPK2RMat(LCE2img.m_vecImgEo,LCE2img.m_vecPosition, LCE2img.m_vecMatrix);
    LCE2img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-00.bmp");
    LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),(UINT)0, (UINT)0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &ImgBlock);
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestSatMappingData/CE-2/data/test_dem.tif");
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    domBlock.InitialImg(DemBlock.GetH(),DemBlock.GetW());
    domBlock.SetGDTType( GDT_Byte );
    vector<Pt3d> vecPtXYZ;
    Pt3d tmpXYZ;
    for( SINT nY = 0; nY < DemBlock.GetH(); nY++ )
    {
        for( SINT nX = 0; nX < DemBlock.GetW(); nX++ )
        {
            DemBlock.GetGeoXYZ( nY, nX, tmpXYZ );
            vecPtXYZ.push_back( tmpXYZ );
        }
    }
    CRasterPt2D ImgSL;
    ImgSL.Initial(domBlock.GetH(),domBlock.GetW());
    DOUBLE trueline = LCE2img.m_CE2IOPara.x0 - tan( LCE2img.m_CE2IOPara.AngleDeg * ML_PI / 180.0 ) * LCE2img.m_CE2IOPara.f;
    /**二分法计算像点坐标,默认是上行**/
    CmlCE2LinearImage ce;
    bool result = ce.mlGetCE2DOMCoordinate( ImgBlock, LCE2img.m_vecMatrix, LCE2img.m_vecPosition, LCE2img.m_CE2IOPara.f, vecPtXYZ, domBlock.GetW(), domBlock.GetH(), ImgSL, trueline, 5, 0.000002 );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE1OPK2RMat_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试CE-1卫星影像外方位角元素转旋转矩阵功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE1OPK2RMat_ok()
{
    CmlMat mat;
    ExOriPara tmp;
    CmlCE1LinearImage cls;
    tmp.ori.phi = -41.75254318354704; tmp.ori.omg =  21.15869935498243; tmp.ori.kap = 159.4600431300858;
    bool result = cls.mlCE1OPK2RMat( tmp.ori.phi, tmp.ori.omg, tmp.ori.kap, &mat );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE1OPK2RMatvec_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试将CE-1卫星影像六类外方位角元素转成线元素及旋转矩阵的形式功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE1OPK2RMatvec_ok()
{
    vector<ExOriPara> vecEo;
    vector<Pt3d> vecXsYsZs;
    vector<MatrixR> vecR;
    ExOriPara tmp;
    tmp.pos.X = 1197983.339518462;  tmp.pos.Y = -695156.4382548895; tmp.pos.Z = 1342190.313397624;
    tmp.ori.phi = -41.75254318354704; tmp.ori.omg =  21.15869935498243; tmp.ori.kap = 159.4600431300858;
    vecEo.push_back(tmp);
    tmp.pos.X = 1197899.70993768;  tmp.pos.Y = -695112.2561749869;  tmp.pos.Z = 1342287.621579567;
    tmp.ori.phi = -41.74849152450207; tmp.ori.omg = 21.15729401702465; tmp.ori.kap = 159.4585694455475;
    vecEo.push_back(tmp);

    CmlCE1LinearImage cls;
    bool result = cls.mlCE1OPK2RMat( vecEo, vecXsYsZs, vecR );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE1InOrietaion_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试CE-1卫星影像内定向功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE1InOrietaion_ok()
{
    CmlCE1LinearImage LCE1img;
    CmlSatMapProj sat;
    sat.ReadCE1InOri("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001_InOriPara.txt",LCE1img.m_CE1IOPara);
    vector<Pt2d> vecPtsList, vecXY;
    Pt2d tmp;
    tmp.X = 229;    tmp.Y = 509;
    vecPtsList.push_back(tmp);
    tmp.X = 486;    tmp.Y = 572;
    vecPtsList.push_back(tmp);
    bool result = LCE1img.mlCE1InOrietaion( vecPtsList, &LCE1img.m_CE1IOPara, vecXY );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetCE1DOMup_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试生成CE-1卫星上行影像DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE1DOMup_ok()
{
    CmlCE1LinearImage LCE1img;
    CmlSatMapProj sat;
    CmlRasterBlock DemBlock,domBlock, ImgBlock;
    sat.ReadCE1InOri("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001_InOriPara.txt",LCE1img.m_CE1IOPara);
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    sat.ReadImgScanTime("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1000_ImgTime.txt",LImgTime);
    sat.ReadLineEo( "../../../UnitTestData/TestSatMappingData/CE-1/data/0562-LineEo.txt",  LLineEo );
    sat.ReadAngleEo( "../../../UnitTestData/TestSatMappingData/CE-1/data/0562-AngleEo.txt",  LAngleEo );
    LCE1img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE1img.m_vecImgEo );
    LCE1img.mlCE1OPK2RMat(LCE1img.m_vecImgEo,LCE1img.m_vecPosition, LCE1img.m_vecMatrix);
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),(UINT)0, (UINT)0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &ImgBlock);
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestSatMappingData/CE-1/data/test_dem.tif");
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    domBlock.InitialImg(DemBlock.GetH(),DemBlock.GetW());
    domBlock.SetGDTType( GDT_Byte );
    vector<Pt3d> vecPtXYZ;
    Pt3d tmpXYZ;
    for( SINT nY = 0; nY < DemBlock.GetH(); nY++ )
    {
        for( SINT nX = 0; nX < DemBlock.GetW(); nX++ )
        {
            DemBlock.GetGeoXYZ( nY, nX, tmpXYZ );
            vecPtXYZ.push_back( tmpXYZ );
        }
    }
    bool result = LCE1img.mlGetCE1DOM(ImgBlock,LCE1img.m_vecMatrix,LCE1img.m_vecPosition,&LCE1img.m_CE1IOPara, vecPtXYZ, domBlock);
    CmlGeoRaster geo;
    geo.CreateGeoFile( "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Byte, 999999999 );
    geo.SaveToGeoFile( 1,0,0,&domBlock );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetCE1DOMdown_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试生成CE-1卫星下行影像DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE1DOMdown_ok()
{
    CmlCE1LinearImage LCE1img;
    CmlSatMapProj sat;
    CmlRasterBlock DemBlock,domBlock, ImgBlock;
    sat.ReadCE1InOri("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001_InOriPara.txt",LCE1img.m_CE1IOPara);
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    sat.ReadImgScanTime("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1000_ImgTime.txt",LImgTime);
    sat.ReadLineEo( "../../../UnitTestData/TestSatMappingData/CE-1/data/0562-LineEo.txt",  LLineEo );
    sat.ReadAngleEo( "../../../UnitTestData/TestSatMappingData/CE-1/data/0562-AngleEo.txt",  LAngleEo );
    LCE1img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE1img.m_vecImgEo );
    LCE1img.mlCE1OPK2RMat(LCE1img.m_vecImgEo,LCE1img.m_vecPosition, LCE1img.m_vecMatrix);
    LCE1img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-1/data/0562_F_1001.bmp");
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),(UINT)0, (UINT)0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &ImgBlock);
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestSatMappingData/CE-1/data/test_dem.tif");
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    domBlock.InitialImg(DemBlock.GetH(),DemBlock.GetW());
    domBlock.SetGDTType( GDT_Byte );
    vector<Pt3d> vecPtXYZ;
    Pt3d tmpXYZ;
    for( SINT nY = 0; nY < DemBlock.GetH(); nY++ )
    {
        for( SINT nX = 0; nX < DemBlock.GetW(); nX++ )
        {
            DemBlock.GetGeoXYZ( nY, nX, tmpXYZ );
            vecPtXYZ.push_back( tmpXYZ );
        }
    }
    LCE1img.m_CE1IOPara.upflag = false;
    bool result = LCE1img.mlGetCE1DOM(ImgBlock,LCE1img.m_vecMatrix,LCE1img.m_vecPosition,&LCE1img.m_CE1IOPara, vecPtXYZ, domBlock);
    CmlGeoRaster geo;
    geo.CreateGeoFile( "../../../UnitTestData/TestSatMappingData/CE-1/result/test_dom.tif", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Byte, 999999999 );
    geo.SaveToGeoFile( 1,0,0,&domBlock );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE2OPK2RMat_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试CE-2卫星影像外方位角元素转旋转矩阵功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE2OPK2RMat_ok()
{
    CmlMat mat;
    ExOriPara tmp;
    CmlCE2LinearImage cls;
    tmp.ori.phi = 20.9823220150; tmp.ori.omg = 141.8078314468; tmp.ori.kap =  -20.7951698540;
    bool result = cls.mlCE2OPK2RMat( tmp.ori.phi, tmp.ori.omg, tmp.ori.kap, &mat );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE2OPK2RMatvec_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试将CE-2卫星影像六类外方位角元素转成线元素及旋转矩阵的形式功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE2OPK2RMatvec_ok()
{
    vector<ExOriPara> vecEo;
    vector<Pt3d> vecXsYsZs;
    vector<MatrixR> vecR;
    ExOriPara tmp;
    tmp.pos.X = 1061989.2024705999;  tmp.pos.Y = -644558.0445896300; tmp.pos.Z = 1350918.9641566400;
    tmp.ori.phi = 20.9823220150; tmp.ori.omg = 141.8078314468; tmp.ori.kap =  -20.7951698540;
    vecEo.push_back(tmp);
    tmp.pos.X = 1061984.2089012300;  tmp.pos.Y = -644555.5720735500;  tmp.pos.Z = 1350924.0240697600;
    tmp.ori.phi = 20.9822397222; tmp.ori.omg = 141.8080668915; tmp.ori.kap =  -20.7952529796;
    vecEo.push_back(tmp);

    CmlCE1LinearImage cls;
    bool result = cls.mlCE1OPK2RMat( vecEo, vecXsYsZs, vecR );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCE2InOrietaion_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试CE-2卫星影像内定向功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE2InOrietaion_ok()
{
    CmlCE2LinearImage LCE2img;
    CmlSatMapProj sat;
    sat.ReadCE2InOri("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6_InOriPara.txt",LCE2img.m_CE2IOPara);
    vector<Pt2d> vecPtsList, vecXY;
    Pt2d tmp;
    tmp.X = 513;    tmp.Y = 1131;
    vecPtsList.push_back(tmp);
    tmp.X = 79;    tmp.Y = 968;
    vecPtsList.push_back(tmp);
    bool result = LCE2img.mlCE2InOrietaion( vecPtsList, &LCE2img.m_CE2IOPara, vecXY );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetCE2DOMup_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试生成CE-2卫星上行影像DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE2DOMup_ok()
{
    CmlCE2LinearImage LCE2img;
    CmlSatMapProj sat;
    CmlRasterBlock DemBlock,domBlock, ImgBlock;
    sat.ReadCE2InOri("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6_InOriPara.txt",LCE2img.m_CE2IOPara);
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    sat.ReadImgScanTime("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-ImgTime-00.txt",LImgTime);
    sat.ReadLineEo( "../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-LineEo.txt",  LLineEo );
    sat.ReadAngleEo( "../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-AngleEo.txt",  LAngleEo );
    LCE2img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE2img.m_vecImgEo );
    LCE2img.mlCE2OPK2RMat(LCE2img.m_vecImgEo,LCE2img.m_vecPosition, LCE2img.m_vecMatrix);
    LCE2img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-00.bmp");
    LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),(UINT)0, (UINT)0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &ImgBlock);
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestSatMappingData/CE-2/data/test_dem.tif");
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    domBlock.InitialImg(DemBlock.GetH(),DemBlock.GetW());
    domBlock.SetGDTType( GDT_Byte );
    vector<Pt3d> vecPtXYZ;
    Pt3d tmpXYZ;
    for( SINT nY = 0; nY < DemBlock.GetH(); nY++ )
    {
        for( SINT nX = 0; nX < DemBlock.GetW(); nX++ )
        {
            DemBlock.GetGeoXYZ( nY, nX, tmpXYZ );
            vecPtXYZ.push_back( tmpXYZ );
        }
    }
    bool result = LCE2img.mlGetCE2DOM(ImgBlock,LCE2img.m_vecMatrix,LCE2img.m_vecPosition,&LCE2img.m_CE2IOPara, vecPtXYZ, domBlock);
    CmlGeoRaster geo;
    geo.CreateGeoFile( "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dom.tif", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Byte, 999999999 );
    geo.SaveToGeoFile( 1,0,0,&domBlock );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetCE2DOMdown_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试生成CE-2卫星下行影像DOM功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE2DOMdown_ok()
{
    CmlCE2LinearImage LCE2img;
    CmlSatMapProj sat;
    CmlRasterBlock DemBlock,domBlock, ImgBlock;
    sat.ReadCE2InOri("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6_InOriPara.txt",LCE2img.m_CE2IOPara);
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    sat.ReadImgScanTime("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-ImgTime-00.txt",LImgTime);
    sat.ReadLineEo( "../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-LineEo.txt",  LLineEo );
    sat.ReadAngleEo( "../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-AngleEo.txt",  LAngleEo );
    LCE2img.mlGetEop( LLineEo, LAngleEo, LImgTime, &LCE2img.m_vecImgEo );
    LCE2img.mlCE2OPK2RMat(LCE2img.m_vecImgEo,LCE2img.m_vecPosition, LCE2img.m_vecMatrix);
    LCE2img.LoadFile("../../../UnitTestData/TestSatMappingData/CE-2/data/0581-F04-No6-00.bmp");
    LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),(UINT)0, (UINT)0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &ImgBlock);
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestSatMappingData/CE-2/data/test_dem.tif");
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    domBlock.InitialImg(DemBlock.GetH(),DemBlock.GetW());
    domBlock.SetGDTType( GDT_Byte );
    vector<Pt3d> vecPtXYZ;
    Pt3d tmpXYZ;
    for( SINT nY = 0; nY < DemBlock.GetH(); nY++ )
    {
        for( SINT nX = 0; nX < DemBlock.GetW(); nX++ )
        {
            DemBlock.GetGeoXYZ( nY, nX, tmpXYZ );
            vecPtXYZ.push_back( tmpXYZ );
        }
    }
    LCE2img.m_CE2IOPara.upflag = false;
    bool result = LCE2img.mlGetCE2DOM(ImgBlock,LCE2img.m_vecMatrix,LCE2img.m_vecPosition,&LCE2img.m_CE2IOPara, vecPtXYZ, domBlock);
    CmlGeoRaster geo;
    geo.CreateGeoFile( "../../../UnitTestData/TestSatMappingData/CE-2/result/test_dom.tif", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Byte, 999999999 );
    geo.SaveToGeoFile( 1,0,0,&domBlock );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetEop_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGetEop函数中读入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetEop_abnormal()
{
    CmlSatMapProj sat;
    vector<LineEo>  LLineEo;
    vector<AngleEo> LAngleEo;
    vector<double> LImgTime;
    vector<ExOriPara> vecEo;
    CmlLinearImage cls;
    bool result = cls.mlGetEop( LLineEo, LAngleEo, LImgTime, &vecEo );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestCE1OPK2RMat_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlCE1OPK2RMat函数中读入矩阵大小不正确的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE1OPK2RMat_abnormal()
{
    CmlMat mat;
    mat.Initial(3,1);
    ExOriPara tmp;
    CmlCE1LinearImage cls;
    tmp.ori.phi = -41.75254318354704; tmp.ori.omg =  21.15869935498243; tmp.ori.kap = 159.4600431300858;
    bool result = cls.mlCE1OPK2RMat( tmp.ori.phi, tmp.ori.omg, tmp.ori.kap, &mat );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestCE1OPK2RMatvec_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlCE1OPK2RMat函数中读入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE1OPK2RMatvec_abnormal()
{
    vector<ExOriPara> vecEo;
    vector<Pt3d> vecXsYsZs;
    vector<MatrixR> vecR;
    CmlCE1LinearImage cls;
    bool result = cls.mlCE1OPK2RMat( vecEo, vecXsYsZs, vecR );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestCE2OPK2RMat_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlCE2OPK2RMat函数中读入矩阵大小不正确的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE2OPK2RMat_abnormal()
{
    CmlMat mat;
    mat.Initial(3,1);
    ExOriPara tmp;
    CmlCE2LinearImage cls;
    tmp.ori.phi = -41.75254318354704; tmp.ori.omg =  21.15869935498243; tmp.ori.kap = 159.4600431300858;
    bool result = cls.mlCE2OPK2RMat( tmp.ori.phi, tmp.ori.omg, tmp.ori.kap, &mat );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestCE2OPK2RMatvec_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlCE2OPK2RMat函数中读入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestCE2OPK2RMatvec_abnormal()
{
    vector<ExOriPara> vecEo;
    vector<Pt3d> vecXsYsZs;
    vector<MatrixR> vecR;
    CmlCE2LinearImage cls;
    bool result = cls.mlCE2OPK2RMat( vecEo, vecXsYsZs, vecR );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetCE1DOMCoordinate_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGetCE1DOMCoordinate函数中读入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE1DOMCoordinate_abnormal()
{
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock domBlock, ImgBlock;
    vector<Pt3d> vecPtXYZ;
    CRasterPt2D ImgSL;
    DOUBLE trueline;
    /**二分法计算像点坐标,默认是上行**/
    CmlCE1LinearImage ce;
    bool result = ce.mlGetCE1DOMCoordinate( ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, LCE1img.m_CE1IOPara.f, vecPtXYZ, domBlock.GetW(), domBlock.GetH(), ImgSL, trueline, 5, 0.000002 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetCE2DOMCoordinate_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGetCE2DOMCoordinate函数中读入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE2DOMCoordinate_abnormal()
{
    CmlCE2LinearImage LCE2img;
    CmlRasterBlock domBlock, ImgBlock;
    vector<Pt3d> vecPtXYZ;
    CRasterPt2D ImgSL;
    DOUBLE trueline;
    /**二分法计算像点坐标,默认是上行**/
    CmlCE2LinearImage ce;
    bool result = ce.mlGetCE2DOMCoordinate( ImgBlock, LCE2img.m_vecMatrix, LCE2img.m_vecPosition, LCE2img.m_CE2IOPara.f, vecPtXYZ, domBlock.GetW(), domBlock.GetH(), ImgSL, trueline, 5, 0.000002 );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetCE1DOM_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGetCE1DOM函数中读入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE1DOM_abnormal()
{
    CmlCE1LinearImage LCE1img;
    CmlRasterBlock domBlock, ImgBlock;
    vector<Pt3d> vecPtXYZ;
    bool result = LCE1img.mlGetCE1DOM(ImgBlock,LCE1img.m_vecMatrix,LCE1img.m_vecPosition,&LCE1img.m_CE1IOPara, vecPtXYZ, domBlock);
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetCE2DOM_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试mlGetCE2DOM函数中读入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestLinearImage::TestGetCE2DOM_abnormal()
{
    CmlCE2LinearImage LCE2img;
    CmlRasterBlock domBlock, ImgBlock;
    vector<Pt3d> vecPtXYZ;
    bool result = LCE2img.mlGetCE2DOM(ImgBlock,LCE2img.m_vecMatrix,LCE2img.m_vecPosition,&LCE2img.m_CE2IOPara, vecPtXYZ, domBlock);
    CPPUNIT_ASSERT(result == false);
}

