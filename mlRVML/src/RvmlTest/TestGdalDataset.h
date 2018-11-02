/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestGdalDataset.h
* @date 2011.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief GDAL图像基础操作测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CTESTGDALDATASET_H
#define CTESTGDALDATASET_H

#include <cppunit/extensions/HelperMacros.h>
class CTestGdalDataset: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestGdalDataset);
//    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestLoadFile_ok);
 //   CPPUNIT_TEST(TestLoadFile_ReturnFalse);
//    CPPUNIT_TEST(TestCreateFile_ok);
   // CPPUNIT_TEST(TestCreateFile_ReturnFalse);
     // CPPUNIT_TEST(TestSaveBlockToFile_ok);
    //  CPPUNIT_TEST(TestSaveBlockToFile2_ok);
   // CPPUNIT_TEST(TestSaveBlockToFile_ReturnFalse);
  //  CPPUNIT_TEST(TestSaveBlockToFile2_ReturnFalse);
//    CPPUNIT_TEST(TestGetRasterGrayBlock_ok);
//    CPPUNIT_TEST(TestGetRasterGrayBlock2_ok);
//    CPPUNIT_TEST(TestGetRasterGrayBlock3_ok);
 //   CPPUNIT_TEST(TestGetRasterGrayBlock_ReturnFalse);
 //   CPPUNIT_TEST(TestGetRasterGrayBlock2_ReturnFalse);
 //   CPPUNIT_TEST(TestGetRasterGrayBlock3_ReturnFalse);
//    CPPUNIT_TEST(TestGetRasterOriginBlock_ok);
//    CPPUNIT_TEST(TestGetRasterOriginBlock2_ok);
//    CPPUNIT_TEST(TestGetRasterOriginBlock_ReturnFalse);
//    CPPUNIT_TEST(TestGetRasterOriginBlock2_ReturnFalse);
//    CPPUNIT_TEST(TestSetNoDataVal_ok);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        /**
        * @fn CTestGdalDataset
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 空参构造函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        CTestGdalDataset();
        /**
        * @fn ~CTestGdalDataset
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 析构函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        virtual ~CTestGdalDataset();
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
        * @fn TestLoadFile_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试加载栅格影像文件功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestLoadFile_ReturnFalse();
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
        * @fn TestCreateFile_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试创建栅格影像文件功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCreateFile_ReturnFalse();
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
        void TestSaveBlockToFile2_ok();
        /**
        * @fn TestSaveBlockToFile_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试将栅格影像块存入文件功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestSaveBlockToFile_ReturnFalse();
        /**
        * @fn TestSaveBlockToFile2_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试将栅格影像块存入文件功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestSaveBlockToFile2_ReturnFalse();
        /**
        * @fn TestGetRasterGrayBlock_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到某个波段某个位置下类型强制GDT_BYTE类型的影像块功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterGrayBlock_ok();
        /**
        * @fn TestGetRasterGrayBlock2_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到某个波段某个位置下类型强制GDT_BYTE类型的影像块功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterGrayBlock2_ok();
        /**
        * @fn TestGetRasterGrayBlock3_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到某三个波段（强制GDT_BYTE），将其当成R、G、B波段后合成block功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterGrayBlock3_ok();
        /**
        * @fn TestGetRasterGrayBlock_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到某个波段某个位置下类型强制GDT_BYTE类型的影像块功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterGrayBlock_ReturnFalse();
        /**
        * @fn TestGetRasterGrayBlock2_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到某个波段某个位置下类型强制GDT_BYTE类型的影像块功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterGrayBlock2_ReturnFalse();
        /**
        * @fn TestGetRasterGrayBlock3_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到某三个波段（强制GDT_BYTE），将其当成R、G、B波段后合成block功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterGrayBlock3_ReturnFalse();
        /**
        * @fn TestGetRasterOriginBlock_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到个波段，将其原始数据导入block中功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterOriginBlock_ok();
        /**
        * @fn TestGetRasterOriginBlock2_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到个波段，将其原始数据导入block中功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterOriginBlock_ReturnFalse();
         /**
        * @fn TestGetRasterOriginBlock_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到个波段，将其原始数据导入block中功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterOriginBlock2_ReturnFalse();
        /**
        * @fn TestGetRasterOriginBlock2_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试在GdalDataSet中得到个波段，将其原始数据导入block中功能容错性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetRasterOriginBlock2_ok();
        /**
        * @fn TestSetNoDataVal_ok
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试设置DataSet中空值的代表值功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestSetNoDataVal_ok();
    protected:
    private:
};

#endif // CTESTGDALDATASET_H
