/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestTypeConvert.cpp
* @date 2012.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 类型转换测试源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "TestTypeConvert.h"
#include "../mlRVML/mlTypeConvert.h"
#include "../mlRVML/mlGeoRaster.h"
CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestTypeConvert,"alltest" );
/**
* @fn CTestTypeConvert
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestTypeConvert::CTestTypeConvert()
{
    //ctor
}
/**
* @fn ~CTestTypeConvert
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestTypeConvert::~CTestTypeConvert()
{
    //dtor
}

/**
* @fn setup()
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 系统默认的初始化函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestTypeConvert::setUp()
{

}
/**
* @fn tearDown()
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 系统默认的销毁函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestTypeConvert::tearDown()
{

}
/**
* @fn TestIplImage2CmlRBlock_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试将IplImage型变量转化成CmlSBlock型变量功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestTypeConvert::TestIplImage2CmlRBlock_ok()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestTypeConvert/test_dem.tif" );
    CmlRasterBlock DemBlock,Block;
    Block.InitialImg(GeoRas.GetHeight(),GeoRas.GetWidth(), 1);
    IplImage* pIplImg = NULL;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    CmlRBlock2IplImg(&DemBlock,pIplImg);
    bool result = IplImage2CmlRBlock(pIplImg,&Block);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCmlRBlock2IplImg_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试将CmlSBlock型变量转化成IplImage型变量功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestTypeConvert::TestCmlRBlock2IplImg_ok()
{

    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestTypeConvert/test_dem.tif" );
    CmlRasterBlock DemBlock;
    IplImage* pIplImg = NULL;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    bool result = CmlRBlock2IplImg(&DemBlock,pIplImg);
    CPPUNIT_ASSERT(result == true);

}
