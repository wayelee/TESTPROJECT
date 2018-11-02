/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestBase.h
* @date 2011.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 工程公共函数测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CTESTBASE_H
#define CTESTBASE_H

#include <cppunit/extensions/HelperMacros.h>
class CTestBase: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestBase);
//    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestPtInRect_ok);
//    CPPUNIT_TEST(TestPtInRect2_ok);
 //   CPPUNIT_TEST(TestPtInRect_ReturnFalse);
 //   CPPUNIT_TEST(TestPtInRect2_ReturnFalse);
//     CPPUNIT_TEST(TestOPK2RMat_ok);
 //   CPPUNIT_TEST(TestOPK2RMat_ReturnFalse);
//     CPPUNIT_TEST(TestRMat2OPK_ok);
//    CPPUNIT_TEST(TestOpen_ok);
 //  CPPUNIT_TEST(TestOpen_ReturnFalse);
//    CPPUNIT_TEST(TestClose_ok);
 //   CPPUNIT_TEST(TestAddSuccessQuitMsg_ok);
//   CPPUNIT_TEST(TestAddErrorMsg_ok);
//    CPPUNIT_TEST(TestAddExceptionMsg_ok);
//    CPPUNIT_TEST(TestAddNoticeMsg_ok);
   //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        /**
        * @fn CTestBase
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 空参构造函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        CTestBase();
        /**
        * @fn ~CTestBase
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 析构函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        virtual ~CTestBase();

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
        * @fn TestPtInRect_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试判断某点是否在矩形框内功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestPtInRect_ok();
        /**
        * @fn TestPtInRect2_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试判断某点是否在矩形框内功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestPtInRect2_ok();
        /**
        * @fn TestPtInRect_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试判断某点是否在矩形框内准确性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
         void TestPtInRect_ReturnFalse();
        /**
        * @fn TestPtInRect2_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试判断某点是否在矩形框内准确性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestPtInRect2_ReturnFalse();
        /**
        * @fn TestOPK2RMat_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试旋转角OPK转换到旋转矩阵功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestOPK2RMat_ok();
        /**
        * @fn TestOPK2RMat_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试旋转角OPK转换到旋转矩阵容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestOPK2RMat_ReturnFalse();

            /**
        * @fn TestRMat2OPK_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试旋转矩阵转换到旋转角OPK功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestRMat2OPK_ok();
        /**
        * @fn TestOpen_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试初始化输出文件的路径，及是否需要打印在屏幕上.同时输入此次日志的头信息功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestOpen_ok();
        /**
        * @fn TestOpen_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试初始化输出文件的路径，及是否需要打印在屏幕上.同时输入此次日志的头信息功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestOpen_ReturnFalse();
        /**
        * @fn TestClose_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试关闭文件功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestClose_ok();
        /**
        * @fn TestAddSuccessQuitMsg_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试函数运行正常至退出消息函数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestAddSuccessQuitMsg_ok();
        /**
        * @fn TestAddErrorMsg_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试函数运行遭遇预知错误消息函数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestAddErrorMsg_ok();
        /**
        * @fn TestAddExceptionMsg_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试函数运行出现异常消息函数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestAddExceptionMsg_ok();
        /**
        * @fn TestAddNoticeMsg_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试函数运行出现提示消息函数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestAddNoticeMsg_ok();

    protected:
    private:
};

#endif // CTESTBASE_H
