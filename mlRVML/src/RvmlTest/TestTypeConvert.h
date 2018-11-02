/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestTypeConvert.h
* @date 2011.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 类型转换测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CTESTTYPECONVERT_H
#define CTESTTYPECONVERT_H

#include <cppunit/extensions/HelperMacros.h>
class CTestTypeConvert: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestTypeConvert);
//    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestIplImage2CmlRBlock_ok);
//    CPPUNIT_TEST(TestCmlRBlock2IplImg_ok);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
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
        CTestTypeConvert();
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
        virtual ~CTestTypeConvert();

   public:
        /**
        * @fn setup
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 系统默认的初始化函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void setUp();//cppunit 系统默认的初始化
        /**
        * @fn tearDown
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 系统默认的销毁函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void tearDown();//cppunit 系统默认的销毁函数
        //测试函数
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
        void TestIplImage2CmlRBlock_ok();
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
        void TestCmlRBlock2IplImg_ok();
    protected:
    private:
};

#endif // CTESTTYPECONVERT_H
