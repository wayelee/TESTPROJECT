/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestLocalization.h
* @date 2011.11.18
* @author 彭嫚   pengman@irsa.ac.cn
* @brief 定位功能测试类头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CTESTLOCALIZATION_H
#define CTESTLOCALIZATION_H

#include <cppunit/extensions/HelperMacros.h>

/**
* @class CTestLocalization
* @date 2011.11.18
* @author
* @brief
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CTestLocalization: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestLocalization);
    //添加测试用例到TestSuite
    //CPPUNIT_TEST(TestLocalInSequenceImg_ok);
//    CPPUNIT_TEST(TestLocalInTwoSite_ok);
//    CPPUNIT_TEST(TestLocalByBundleResection_ok);
//    CPPUNIT_TEST(TestLocalIn2Dom_ok);

    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestLocalization()
    * @date 2011.11.18
    * @author 彭嫚   pengman@irsa.ac.cn
    * @brief 空参构造函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestLocalization();

    /**
    * @fn ~CTestLocalization()
    * @date 2011.11.18
    * @author 彭嫚   pengman@irsa.ac.cn
    * @brief 析构函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestLocalization();

public:
    /**
    * @fn setup()
    * @date 2011.12.1
    * @author 彭嫚   pengman@irsa.ac.cn
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
    * @author 彭嫚   pengman@irsa.ac.cnn
    * @brief 系统默认的销毁函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void tearDown();//cppunit 系统默认的销毁函数

    /**
    * @fn TestLocalInSequenceImg_ok()
    * @date 2011.12.1
    * @author 彭嫚   pengman@irsa.ac.cn
    * @brief 测试序列影像定位正常实现的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalInSequenceImg_ok();

    /**
    * @fn TestLocalInTwoSite_ok()
    * @date 2011.12.1
    * @author 彭嫚   pengman@irsa.ac.cn
    * @brief 测试两站点定位正常实现的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalInTwoSite_ok();


    /**
    * @fn TestLocalIn2Dom_ok()
    * @date 2011.12.1
    * @author 彭嫚   pengman@irsa.ac.cn
    * @brief 测试利用DOM定位正常实现的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalIn2Dom_ok();

    /**
    * @fn TestLocalByBundleResection_ok()
    * @date 2011.12.1
    * @author 彭嫚   pengman@irsa.ac.cn
    * @brief 测试通过后方交会定位正常实现的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalByBundleResection_ok();

protected:
private:
};

#endif // CTESTKRINGING_H
