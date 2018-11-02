/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestSatMapping.h
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像制图头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CTESTSATMAPPING_H
#define CTESTSATMAPPING_H

#include <cppunit/extensions/HelperMacros.h>
//#include <TestSatMapping.h>
//#include "../../include/mlTypes.h"

/**
* @class CTestSatMapping
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像制图类
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
class CTestSatMapping: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestSatMapping);
    //添加测试用例到TestSuite
    //CPPUNIT_TEST(TestSatMatch_ok);
    //CPPUNIT_TEST(TestSatMappingByPtsCE1_ok);
    //CPPUNIT_TEST(TestSatMappingByPtsCE2_ok);
    //CPPUNIT_TEST(TestCE1MappingByPtsLeft_ok);
    //CPPUNIT_TEST(TestCE1MappingByPtsRight_ok);
    //CPPUNIT_TEST(TestGenerateCE2DOM_ok);
    //CPPUNIT_TEST(TestCE2MappingByPtsLeft_ok);
    //CPPUNIT_TEST(TestCE2MappingByPtsRight_ok);
    //CPPUNIT_TEST(TestWriteBLH_ok);
    //CPPUNIT_TEST(TestGenerateDOM_ok);
    //CPPUNIT_TEST(TestInterSection_ok);
    //CPPUNIT_TEST(TestConstractAdjust_ok);
    //CPPUNIT_TEST(TestConstractAdjust2_ok);

    //CPPUNIT_TEST(TestSatMatch_abnormal1);
    //CPPUNIT_TEST(TestSatMatch_abnormal2);
    //CPPUNIT_TEST(TestSatMatch_abnormal3);
    //CPPUNIT_TEST(TestCE1MappingByPts_abnormal);
    //CPPUNIT_TEST(TestCE2MappingByPts_abnormal);
    //CPPUNIT_TEST(TestSatMappingByPts_abnormal1);
    //CPPUNIT_TEST(TestSatMappingByPts_abnormal2);
    //CPPUNIT_TEST(TestSatMappingByPts_abnormal3);
    //CPPUNIT_TEST(TestSatMappingByPts_abnormal4);
    //CPPUNIT_TEST(TestConstractAdjust_abnormal);
    //CPPUNIT_TEST(TestInterSection_abnormal1);
    //CPPUNIT_TEST(TestInterSection_abnormal2);
    //CPPUNIT_TEST(TestGenerateCE2DOM_abnormal1);
    //CPPUNIT_TEST(TestGenerateCE2DOM_abnormal2);
    //CPPUNIT_TEST(TestGenerateCE2DOM_abnormal3);
    //CPPUNIT_TEST(TestGenerateCE2DOM_abnormal4);
    //CPPUNIT_TEST(TestGenerateDOM_abnormal1);
    //CPPUNIT_TEST(TestGenerateDOM_abnormal2);
    //CPPUNIT_TEST(TestGenerateDOM_abnormal3);
    //CPPUNIT_TEST(TestGenerateDOM_abnormal4);
    //CPPUNIT_TEST(TestWriteBLH_abnormal);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
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
    CTestSatMapping();
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
    virtual ~CTestSatMapping();
public:
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
    void setUp();//cppunit 系统默认的初始化
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
    void tearDown();//cppunit 系统默认的销毁函数
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
    void TestSatMatch_ok();
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
    void TestCE1MappingByPtsLeft_ok();
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
    void TestCE1MappingByPtsRight_ok();
    /**
    * @fn TestReadImgScanTime_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试读入卫星影像扫描线时间文件功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestReadImgScanTime_ok();
    /**
    * @fn TestReadEo_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试读入卫星影像原始测控数据文件功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestReadEo_ok();
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
    void TestWriteBLH_ok();
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
    void TestGenerateDOM_ok();
    /**
    * @fn TestGetBlockEoByNum_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试从全部外方位数据中截取某影像块对应的外方位元素功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetBlockEoByNum_ok();
    /**
    * @fn TestGetBlockEoByNum2_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试从全部外方位数据中截取某影像块对应的外方位元素功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetBlockEoByNum2_ok();
    /**
    * @fn TestGetMatchPts_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试SIFT匹配并Ransac剔除粗差功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetMatchPts_ok();
    /**
    * @fn TestGetDensePts_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试初始密集匹配并最小二乘得到子像素精确密集匹配功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetDensePts_ok();
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
    void TestInterSection_ok();
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
    void TestConstractAdjust_ok();
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
    void TestConstractAdjust2_ok();
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
    void TestSatMappingByPtsCE1_ok();
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
    void TestSatMappingByPtsCE2_ok();
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
    void TestGenerateCE2DOM_ok();
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
    void TestCE2MappingByPtsLeft_ok();
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
    void TestCE2MappingByPtsRight_ok();

    /**
    * @fn TestSatMatch_abnormal1
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlSatMatch函数影像不能加载的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestSatMatch_abnormal1();
    /**
    * @fn TestSatMatch_abnormal2
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlSatMatch函数影像不能正常分块的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestSatMatch_abnormal2();
    /**
    * @fn TestSatMatch_abnormal3
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlSatMatch函数影像无匹配点的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestSatMatch_abnormal3();
    /**
    * @fn TestCE1MappingByPts_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlCE1MappingByPts函数无法加载影像的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE1MappingByPts_abnormal();
    /**
    * @fn TestCE2MappingByPts_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlCE1MappingByPts函数无法加载影像的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE2MappingByPts_abnormal();
    /**
    * @fn TestSatMappingByPts_abnormal1
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlSatMappingByPts函数参数输入不正确的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestSatMappingByPts_abnormal1();
    /**
    * @fn TestSatMappingByPts_abnormal2
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlSatMappingByPts函数CE-1内定向参数不正确的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestSatMappingByPts_abnormal2();
    /**
    * @fn TestSatMappingByPts_abnormal3
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlSatMappingByPts函数CE-2内定向参数不正确的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestSatMappingByPts_abnormal3();
    /**
    * @fn TestSatMappingByPts_abnormal4
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlSatMappingByPts函数任务标号不正确的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestSatMappingByPts_abnormal4();
    /**
    * @fn TestConstractAdjust_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试ConstractAdjust函数影像块为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestConstractAdjust_abnormal();
    /**
    * @fn TestInterSection_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试InterSection函数左右点数不一致的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestInterSection_abnormal1();
    /**
    * @fn TestInterSection2_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试InterSection函数输入数据为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestInterSection_abnormal2();
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
    void TestGenerateCE2DOM_abnormal1();
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
    void TestGenerateCE2DOM_abnormal2();
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
    void TestGenerateCE2DOM_abnormal3();
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
    void TestGenerateCE2DOM_abnormal4();
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
    void TestGenerateDOM_abnormal1();
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
    void TestGenerateDOM_abnormal2();
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
    void TestGenerateDOM_abnormal3();
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
    void TestGenerateDOM_abnormal4();
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
    void TestWriteBLH_abnormal();
protected:
private:
};

#endif // CTESTSATMAPPING_H
