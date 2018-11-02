/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestGeoRaster.h
* @date 2011.11.18
* @author
* @brief
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CTESTGEORASTER_H
#define CTESTGEORASTER_H

#include <cppunit/extensions/HelperMacros.h>

/**
* @class CTestGeoRaster
* @date 2011.11.18
* @author
* @brief
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CTestGeoRaster: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestGeoRaster);
    //添加测试用例到TestSuite
    //CPPUNIT_TEST(TestASCIIDemToGeoTiff_ok);
    //CPPUNIT_TEST(TestGeoTiffToASCIIDem_ok);
//    CPPUNIT_TEST(TestLoadGeoFile_ok);
//    CPPUNIT_TEST(TestCreateGeoFile_ok);
//    CPPUNIT_TEST(TestSaveToGeoFile_ok);
//    CPPUNIT_TEST(TestSaveToGeoFile2_ok);
//    CPPUNIT_TEST(TestSaveToGeoFile3_ok);
//    CPPUNIT_TEST(TestLoadGeoFile_abnormal);
//    CPPUNIT_TEST(TestCreateGeoFile_abnormal);
//    CPPUNIT_TEST(TestSaveToGeoFile_abnormal);
//    CPPUNIT_TEST(TestSaveToGeoFile2_abnormal);
//    CPPUNIT_TEST(TestSaveToGeoFile3_abnormal);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestGeoRaster()
    * @date 2011.12.1
    * @author
    * @brief 空参构造函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestGeoRaster();

    /**
    * @fn ~CTestGeoRaster()
    * @date 2011.12.1
    * @author
    * @brief 析构函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestGeoRaster();

public:
    /**
    * @fn setup()
    * @date 2011.12.1
    * @author
    * @brief 系统默认的初始化函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void setUp();//cppunit 系统默认的初始化

    /**
    * @fn tearDown()
    * @date 2011.12.1
    * @author
    * @brief 系统默认的销毁函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void tearDown();//cppunit 系统默认的销毁函数

//    /**
//    * @fn TestASCIIDemToGeoTiff_ok
//    * @date 2011.12.1
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 测试ASCII格式DEM转为GeoTiff功能
//    * @version 1.0
//    * @return 无返回值
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    void TestASCIIDemToGeoTiff_ok();
//    /**
//    * @fn TestGeoTiffToASCIIDem_ok
//    * @date 2011.12.1
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 测试GeoTiff格式DEM转为ASCII格式功能
//    * @version 1.0
//    * @return 无返回值
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    void TestGeoTiffToASCIIDem_ok();
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
    void TestLoadGeoFile_ok();
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
    void TestCreateGeoFile_ok();
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
    void TestSaveToGeoFile_ok();
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
    void TestSaveToGeoFile2_ok();
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
    void TestSaveToGeoFile3_ok();

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
    void TestLoadGeoFile_abnormal();
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
    void TestCreateGeoFile_abnormal();
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
    void TestSaveToGeoFile_abnormal();
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
    void TestSaveToGeoFile2_abnormal();
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
    void TestSaveToGeoFile3_abnormal();

protected:
private:
};

#endif // CTESTGEORASTER_H
