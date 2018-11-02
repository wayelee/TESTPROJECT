/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestFileRaster.h
* @date 2011.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 影像文件读写测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CTESTFILERASTER_H
#define CTESTFILERASTER_H

#include <cppunit/extensions/HelperMacros.h>
class CTestFileRaster: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestFileRaster);
//    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestLoadFile_ok);
//    CPPUNIT_TEST(TestCreateFile_ok);
//    CPPUNIT_TEST(TestSaveBlockToFile_ok);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        /**
        * @fn CTestFileRaster
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 空参构造函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        CTestFileRaster();
        /**
        * @fn ~CTestFileRaster
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 析构函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        virtual ~CTestFileRaster();

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
        * @fn TestLoadFile_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试加载栅格影像文件功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestLoadFile_ok();
        /**
        * @fn TestCreateFile_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试创建栅格影像文件功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCreateFile_ok();
        /**
        * @fn TestSaveBlockToFile_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试将栅格影像块存入文件功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestSaveBlockToFile_ok();
    protected:
    private:
};

#endif // CTESTFILERASTER_H
