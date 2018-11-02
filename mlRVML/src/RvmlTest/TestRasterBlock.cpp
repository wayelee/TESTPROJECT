/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestRasterBlock.cpp
* @date 2012.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 栅格数据块处理测试源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "TestRasterBlock.h"
#include "../mlRVML/mlRasterBlock.h"
#include "../mlRVML/mlGeoRaster.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestRasterBlock,"alltest" );
/**
* @fn CTestRasterBlock
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestRasterBlock::CTestRasterBlock()
{
    //ctor
}
/**
* @fn ~CTestRasterBlock
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestRasterBlock::~CTestRasterBlock()
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
void CTestRasterBlock::setUp()
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
void CTestRasterBlock::tearDown()
{

}

/**
* @fn TestInitialImg_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试初始化栅格内存函数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestInitialImg_ok()
{
    CmlRasterBlock cls;
    bool result = cls.InitialImg( 3,3,8);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestInitialImg_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试初始化栅格内存函数功能容错性
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestInitialImg_ReturnFalse()
{
    CmlRasterBlock cls;
    bool result = cls.InitialImg( 1,1,8);
    result = cls.InitialImg(1,1,8);
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestReSet_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试清空数据，不删除内存功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestReSet_ok()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    cls.SetAt(0,0,250);
    bool result = cls.ReSet();
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestClear_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试重置结构，清空数据，销毁信息功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestClear_ok()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    cls.SetAt(0,0,250);
    bool result = cls.Clear();
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetPtrAt_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到某像素数据值的头指针功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetPtrAt_ok()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    cls.SetAt(0,0,5);
    BYTE* result = cls.GetPtrAt(0,0);
    CPPUNIT_ASSERT(*result == 5);
}
/**
* @fn TestGetPtrAt_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到某像素数据值的头指针功能容错性
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetPtrAt_ReturnFalse()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    cls.SetAt(0,0,5);
    BYTE* result = cls.GetPtrAt(9999,9999);
    CPPUNIT_ASSERT(result == NULL);
}
/**
* @fn TestSetPtrAt_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试设置某像素数据值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestSetPtrAt_ok()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    DOUBLE res = 5;
    bool result = cls.SetPtrAt(0,0,(BYTE*)(&res));
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestSetPtrAt_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试设置某像素数据值功能容错性
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestSetPtrAt_ReturnFalse()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    DOUBLE res = 5;
    bool result = cls.SetPtrAt(9999,9999,(BYTE*)(&res));
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetAt_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到栅格的某个点数据域第一个字节值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetAt_ok()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    cls.SetAt(0,0,250);
    BYTE result = cls.GetAt(0,0);
    CPPUNIT_ASSERT(result == 250);
}
/**
* @fn TestSetAt_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试设置栅格的某个点数据域第一个字节值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestSetAt_ok()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    bool result = cls.SetAt(0,0,250);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestSetAt_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试设置栅格的某个点数据域第一个字节值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestSetAt_ReturnFalse()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    bool result = cls.SetAt(-1,-1,250);
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetDoubleVal_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到数据块内像素的值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetDoubleVal_ok()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    cls.SetAt(0,0,250);
    cls.SetGDTType(GDT_Byte);
    DOUBLE res;
    bool result = cls.GetDoubleVal(0,0,res);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetDoubleVal_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到数据块内像素的值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetDoubleVal_ReturnFalse()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    cls.SetAt(0,0,250);
    cls.SetGDTType(GDT_Byte);
    DOUBLE res;
    bool result = cls.GetDoubleVal(-1,-1,res);
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestSetDoubleVal_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试设置数据块内像素的值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestSetDoubleVal_ok()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    bool result = cls.SetDoubleVal(0,0,250);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestSetDoubleVal_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试设置数据块内像素的值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestSetDoubleVal_ReturnFalse()
{
    CmlRasterBlock cls;
    cls.InitialImg(3,3,8);
    bool result = cls.SetDoubleVal(-1,-1,250);
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetGeoXYZ_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到带坐标信息的数据块内的三维坐标值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetGeoXYZ_ok()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestRasterBlock/test_dem.tif" );
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    Pt3d tmpXYZ;
    bool result = DemBlock.GetGeoXYZ( 100, 100, tmpXYZ );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetGeoXYZ_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到带坐标信息的数据块内的三维坐标值功能容错性
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetGeoXYZ_ReturnFalse()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestRasterBlock/test_dem.tif" );
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    Pt3d tmpXYZ;
    bool result = DemBlock.GetGeoXYZ( -1, 1, tmpXYZ );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetGeoZ_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到带坐标信息的数据块内的三维高程值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetGeoZ_ok()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestRasterBlock/test_dem.tif" );
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    DOUBLE dZ;
    bool result = DemBlock.GetGeoZ( -30.5, 48.5, dZ );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetGeoZ_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试得到带坐标信息的数据块内的三维高程值功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestRasterBlock::TestGetGeoZ_ReturnFalse()
{
    CmlGeoRaster GeoRas;
    GeoRas.LoadGeoFile( "../../../UnitTestData/TestRasterBlock/test_dem.tif" );
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    DOUBLE dZ;
    bool result = DemBlock.GetGeoZ( -99999, -99999, dZ );
    CPPUNIT_ASSERT(result == false);
}
