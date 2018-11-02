/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestRasterBlock.h
* @date 2011.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 栅格数据块处理测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CTESTRSTERBLOCK_H
#define CTESTRSTERBLOCK_H

#include <cppunit/extensions/HelperMacros.h>
class CTestRasterBlock: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestRasterBlock);
//    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestInitialImg_ok);
 //   CPPUNIT_TEST(TestInitialImg_ReturnFalse);
//    CPPUNIT_TEST(TestReSet_ok);
//    CPPUNIT_TEST(TestClear_ok);
//    CPPUNIT_TEST(TestGetPtrAt_ok);
 //   CPPUNIT_TEST(TestGetPtrAt_ReturnFalse);
//    CPPUNIT_TEST(TestSetPtrAt_ok);
 //   CPPUNIT_TEST(TestSetPtrAt_ReturnFalse);
//    CPPUNIT_TEST(TestGetAt_ok);
//    CPPUNIT_TEST(TestSetAt_ok);
 //   CPPUNIT_TEST(TestSetAt_ReturnFalse);

//    CPPUNIT_TEST(TestGetDoubleVal_ok);
 //   CPPUNIT_TEST(TestGetDoubleVal_ReturnFalse) ;
//    CPPUNIT_TEST(TestSetDoubleVal_ok);
 //   CPPUNIT_TEST(TestSetDoubleVal_ReturnFalse);
//    CPPUNIT_TEST(TestGetGeoXYZ_ok);
 //   CPPUNIT_TEST(TestGetGeoXYZ_ReturnFalse);
//    CPPUNIT_TEST(TestGetGeoZ_ok);
//    CPPUNIT_TEST(TestGetGeoZ_ReturnFalse);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
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
        CTestRasterBlock();
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
        virtual ~CTestRasterBlock();

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
        * @fn TestInitialImg_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试初始化栅格内存函数功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestInitialImg_ReturnFalse();
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
        void TestInitialImg_ok();
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
        void TestReSet_ok();
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
        void TestClear_ok();
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
        void TestGetPtrAt_ok();
        /**
        * @fn TestGetPtrAt_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试得到某像素数据值的头指针功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetPtrAt_ReturnFalse();
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
        void TestSetPtrAt_ok();
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
        void TestSetPtrAt_ReturnFalse();
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
        void TestGetAt_ok();
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
        void TestSetAt_ok();
         /**
        * @fn TestSetAt_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试设置栅格的某个点数据域第一个字节值功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestSetAt_ReturnFalse();
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
        void TestGetDoubleVal_ok();
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
        void TestGetDoubleVal_ReturnFalse();
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
        void TestSetDoubleVal_ok();
         /**
        * @fn TestSetDoubleVal_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试设置数据块内像素的值功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestSetDoubleVal_ReturnFalse();
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
        void TestGetGeoXYZ_ok();
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
        void TestGetGeoXYZ_ReturnFalse();
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
        void TestGetGeoZ_ok();
        /**
        * @fn TestGetGeoZ_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试得到带坐标信息的数据块内的三维高程值功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetGeoZ_ReturnFalse();
    protected:
    private:
};

#endif // CTESTRSTERBLOCK_H
