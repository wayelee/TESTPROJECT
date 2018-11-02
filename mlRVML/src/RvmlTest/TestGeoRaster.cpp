#include "TestGeoRaster.h"
#include "../mlRVML/mlGeoRaster.h"
#define  DEM_NO_DATA  1.0E+38


CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestGeoRaster,"alltest" );

CTestGeoRaster::CTestGeoRaster()
{
    //ctor
}

CTestGeoRaster::~CTestGeoRaster()
{
    //dtor
}


///**
//* @fn TestASCIIDemToGeoTiff_ok()
//* @date 2011.12.1
//* @author
//* @brief
//* @version 1.1
//* @return 无返回值
//* @par 修改历史：
//* <作者>    <时间>   <版本编号>    <修改原因>\n
//*
//*/
//void CTestGeoRaster::TestASCIIDemToGeoTiff_ok()
//{
//
//
//}


/** @brief TearDown
  *
  * @todo: document this function
  */
void CTestGeoRaster::tearDown()
{

}

/** @brief SetUp
  *
  * @todo: document this function
  */
void CTestGeoRaster::setUp()
{

}

///**
//* @fn TestGeoTiffToASCIIDem_ok
//* @date 2011.12.1
//* @author 万文辉 whwan@irsa.ac.cn
//* @brief 测试GeoTiff格式DEM转为ASCII格式功能
//* @version 1.0
//* @return 无返回值
//* @par 修改历史：
//* <作者>    <时间>   <版本编号>    <修改原因>\n
//*/
//void CTestGeoRaster::TestGeoTiffToASCIIDem_ok()
//{
//
//}
/**
* @fn TestLoadGeoFile_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试载入带地理坐标的栅格数据功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestLoadGeoFile_ok()
{
    CmlGeoRaster GeoRas;
    bool result = GeoRas.LoadGeoFile( "../../../UnitTestData/TestGeoRaster/test_dem.tif" );
    CPPUNIT_ASSERT( result == true );
}
/**
* @fn TestCreateGeoFile_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试创建带地理坐标的栅格数据硬盘文件功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestCreateGeoFile_ok()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestGeoRaster/test_dem.tif" );
    CmlGeoRaster geo;
    bool result = geo.CreateGeoFile( "../../../UnitTestData/TestGeoRaster/create_dem.tif", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Byte, 9999999 );
    CPPUNIT_ASSERT( result == true );
}
/**
* @fn TestSaveToGeoFile_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试将点坐标存入Gdal文件中功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestSaveToGeoFile_ok()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestGeoRaster/test_dem.tif" );
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    CmlGeoRaster geo;
    geo.CreateGeoFile( "../../../UnitTestData/TestGeoRaster/save1_dem.tif", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Float64, DEM_NO_DATA );
    DOUBLE tmp;
    DOUBLE *Zval = new DOUBLE[GeoRas.GetWidth()* GeoRas.GetHeight()];
    for( UINT i = 0; i < GeoRas.GetHeight(); i++ )
    {
        for( UINT j = 0; j < GeoRas.GetWidth(); j++ )
        {

            DemBlock.GetDoubleVal( i, j, tmp );
            Zval[i*GeoRas.GetWidth()+j] = tmp;
        }
    }
    bool result = geo.SaveToGeoFile( Zval );
    CPPUNIT_ASSERT( result == true );
}
/**
* @fn TestSaveToGeoFile2_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试将点坐标存入Gdal文件中功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestSaveToGeoFile2_ok()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestGeoRaster/test_dem.tif" );
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    CmlGeoRaster geo;
    geo.CreateGeoFile( "../../../UnitTestData/TestGeoRaster/save2_dem.tif", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Float64, DEM_NO_DATA );
    DOUBLE tmp;
    vector<DOUBLE> Zval;
    for( UINT i = 0; i < GeoRas.GetHeight(); i++ )
    {
        for( UINT j = 0; j < GeoRas.GetWidth(); j++ )
        {
            DemBlock.GetDoubleVal( i, j, tmp );
            Zval.push_back(tmp);
        }
    }
    bool result = geo.SaveToGeoFile( Zval );
    CPPUNIT_ASSERT( result == true );
}
/**
* @fn TestSaveToGeoFile3_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试将点坐标存入Gdal文件中功能
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestSaveToGeoFile3_ok()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestGeoRaster/test_dem.tif" );
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    CmlGeoRaster geo;
    geo.CreateGeoFile( "../../../UnitTestData/TestGeoRaster/save3_dem.tif", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Float64, DEM_NO_DATA );
    bool result = geo.SaveToGeoFile( (UINT)1, 0, 0, &DemBlock );
    CPPUNIT_ASSERT( result == true );

}

/**
* @fn TestLoadGeoFile_abnormal
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试LoadGeoFile函数输入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestLoadGeoFile_abnormal()
{
    CmlGeoRaster GeoRas;
    bool result = GeoRas.LoadGeoFile( "../" );
    CPPUNIT_ASSERT( result == false );
}
/**
* @fn TestCreateGeoFile_abnormal
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试CreateGeoFile函数输入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestCreateGeoFile_abnormal()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestGeoRaster/test_dem.tif" );
    CmlGeoRaster geo;
    bool result = geo.CreateGeoFile( "../", GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), 1, GDT_Byte, 9999999 );
    CPPUNIT_ASSERT( result == false );
}
/**
* @fn TestSaveToGeoFile_abnormal
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试SaveToGeoFile函数输入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestSaveToGeoFile_abnormal()
{

    CmlGeoRaster geo;
    DOUBLE *Zval=NULL;
    bool result = geo.SaveToGeoFile( Zval );
    CPPUNIT_ASSERT( result == false );

}
/**
* @fn TestSaveToGeoFile2_abnormal
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试SaveToGeoFile函数输入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestSaveToGeoFile2_abnormal()
{
    CmlGeoRaster geo;
    vector<DOUBLE> Zval;
    bool result = geo.SaveToGeoFile( Zval );
    CPPUNIT_ASSERT( result == false );
}
/**
* @fn TestSaveToGeoFile3_abnormal
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试SaveToGeoFile函数输入数据为空的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestGeoRaster::TestSaveToGeoFile3_abnormal()
{
    CmlRasterBlock DemBlock;
    CmlGeoRaster geo;
    bool result = geo.SaveToGeoFile( (UINT)1, 0, 0, &DemBlock );
    CPPUNIT_ASSERT( result == false );
}
