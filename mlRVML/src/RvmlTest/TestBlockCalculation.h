/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestBlockCalculation.h
* @date 2011.2.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 影像分块测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CTESTBLOCKCALCULATION_H
#define CTESTBLOCKCALCULATION_H

#include <cppunit/extensions/HelperMacros.h>
class CTestBlockCalculation: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestBlockCalculation);
//    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestCalBlockCol_ok);
//    CPPUNIT_TEST(TestCalBlockRow_ok);
//    CPPUNIT_TEST(TestGetStartCol_ok);
//    CPPUNIT_TEST(TestGetStartRow_ok);
//    CPPUNIT_TEST(TestGetBlockPos_ok);
//    CPPUNIT_TEST(TestGetBlockPosLast_ok);
//      CPPUNIT_TEST(TestCalBlock_ok);
//      CPPUNIT_TEST(TestCalBlock2_ok);
//      CPPUNIT_TEST(TestCalBlock3_ok);
      CPPUNIT_TEST(TestCalBlock4_ok);
//    CPPUNIT_TEST(TestGetBlock_ok);

//    CPPUNIT_TEST(TestGetBlockPos_abnormal);
//    CPPUNIT_TEST(TestCalBlock_abnormal1);
//    CPPUNIT_TEST(TestCalBlock_abnormal2);
//    CPPUNIT_TEST(TestGetBlock_abnormal);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        /**
        * @fn CTestBlockCalculation
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 空参构造函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        CTestBlockCalculation();
        /**
        * @fn ~CTestBlockCalculation
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 析构函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        virtual ~CTestBlockCalculation();

   public:
        /**
        * @fn setup
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
        * @fn tearDown
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
        //测试函数
        /**
        * @fn TestCalBlockCol_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像沿列（X）方向分块的大小的块数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCalBlockCol_ok();
        /**
        * @fn TestCalBlockRow_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像沿行（Y）方向分块的大小的块数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCalBlockRow_ok();
        /**
        * @fn TestGetStartCol_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像按列（X）方向某一分块的起始列号功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetStartCol_ok();
        /**
        * @fn TestGetStartRow_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像按行（Y）方向某一分块的起始行号功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetStartRow_ok();
        /**
        * @fn TestGetBlockPos_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算某一影像块的起始位置和大小功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetBlockPos_ok();
        /**
        * @fn TestGetBlockPosLast_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算某一影像块最后一块的起始位置和大小功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetBlockPosLast_ok();
        /**
        * @fn TestCalBlock_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像沿行（列）方向分块的大小的块数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCalBlock_ok();
        /**
        * @fn TestCalBlock2_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像沿行（列）方向分块的大小的块数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCalBlock2_ok();
        /**
        * @fn TestCalBlock3_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像沿行（列）方向分块的大小的块数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCalBlock3_ok();
        /**
        * @fn TestCalBlock4_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像沿行（列）方向分块的大小的块数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCalBlock4_ok();
        /**
        * @fn TestGetBlock_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算影像沿行（列）方向某一块的起始位置功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetBlock_ok();
        /**
        * @fn TestGetBlockPos_abnormal
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试GetBlockPos函数输入参数超限的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetBlockPos_abnormal();
        /**
        * @fn TestCalBlock_abnormal1
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试CalBlock函数输入参数为零的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCalBlock_abnormal1();
        /**
        * @fn TestCalBlock_abnormal2
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试CalBlock函数输入参数超限的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCalBlock_abnormal2();
        /**
        * @fn TestGetBlock_abnormal
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试GetBlock函数输入参数超限的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetBlock_abnormal();
    protected:
    private:
};

#endif // CTESTBLOCKCALCULATION_H
