/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestWBaseProc.h
* @date 2011.11.18
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 长基线制图测试类头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
* 彭嫚  2011.11.29  1.0  转化为c++框架
*/
#ifndef CTESTWBASEPROC_H
#define CTESTWBASEPROC_H

#include <cppunit/extensions/HelperMacros.h>

/**
* @class CTestWBaseProc
* @date 2011.11.18
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 长基线制图测试类定义
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CTestWBaseProc: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestWBaseProc);
    //添加测试用例到TestSuite
//
//    CPPUNIT_TEST(TestWideBaseAnalysis_ok);
//    CPPUNIT_TEST(TestWideBaseAnalysis_abnormal);
//    CPPUNIT_TEST(TestWideBaseMapping_ok);
//    CPPUNIT_TEST(TestWideBaseMapping_abnormal);
//    CPPUNIT_TEST(TestWideBase3Ds_ok);
//    CPPUNIT_TEST(TestWideBase3Ds_abnormal);
//    CPPUNIT_TEST(TestWideDenseMatch_ok);
//    CPPUNIT_TEST(TestWideDenseMatch_abnormal);
//      CPPUNIT_TEST(TestWideFeaMatch_ok);
//    CPPUNIT_TEST(TestWideFeaMatch_abnormal);
//    CPPUNIT_TEST(TestWidePtsFilter_ok);
//    CPPUNIT_TEST(TestWidePtsFilter_abnormal);
//    CPPUNIT_TEST(TestmlBestBase_ok);
//    CPPUNIT_TEST(TestmlBestBase_abnormal);

    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestWBaseProc()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 空参构造函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestWBaseProc();

    /**
    * @fn ~CTestWBaseProc()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 析构函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestWBaseProc();

public:
    /**
    * @fn setup()
    * @date 2011.12.1
    * @author 彭嫚
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
    * @author 彭嫚
    * @brief 系统默认的销毁函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void tearDown();//cppunit 系统默认的销毁函数

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
    void TestWideBaseAnalysis_ok();

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
    void TestWideBaseAnalysis_abnormal();

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
    void TestWideBaseMapping_ok();

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
    void TestWideBaseMapping_abnormal();

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
    void TestWideBase3Ds_ok();

    /**
    * @fn TestWideBase3Ds_abnormal()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 测试输入参数异常时三维点云计算功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestWideBase3Ds_abnormal();

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
    void TestWideDenseMatch_ok();

    /**
    * @fn WideDenseMatch_abnormal()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 测试输入参数异常时三维点云计算功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestWideDenseMatch_abnormal();

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
    void TestWideFeaMatch_ok();

    /**
    * @fn WideDenseMatch_abnormal()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 测试输入参数异常时三维点云计算功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestWideFeaMatch_abnormal();

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
    void TestWidePtsFilter_ok();

    /**
    * @fn WideDenseMatch_abnormal()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 测试输入参数异常时三维点云计算功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestWidePtsFilter_abnormal();


    /**
    * @fn TestmlBestBase_ok()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 测试输入参数正确时最优基线计算功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestmlBestBase_ok();

    /**
    * @fn TestmlBestBase_abnormal()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 测试输入参数正确时最优基线计算功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestmlBestBase_abnormal();

protected:
private:
};

#endif // CTESTWBASEPROC_H
