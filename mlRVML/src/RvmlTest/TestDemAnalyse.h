#ifndef CTESTDEMANALYSE_H
#define CTESTDEMANALYSE_H

#include <cppunit/extensions/HelperMacros.h>
class CTestDemAnalyse: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestDemAnalyse);
    //添加测试用例到TestSuite
    //CPPUNIT_TEST(TestmlCoordTrans);
//    CPPUNIT_TEST(ComputeViewShedInterface_OK);
//    // 返回-1的情形需要大图像
//    // 返回-3的情形时创建block时出现异常，应该在图片过大时出现
//    CPPUNIT_TEST(ComputeViewShedInterface_ReturnM2);
//    CPPUNIT_TEST(ComputeViewShedInterface_ReturnM4);
//
//    CPPUNIT_TEST(ComputeSlopeInterface_OK);
//    CPPUNIT_TEST(ComputeSlopeInterface_ReturnM1);
//    CPPUNIT_TEST(ComputeSlopeInterface_ReturnM2);
//
//    CPPUNIT_TEST(ComputeSlopeAspectInterface_OK);
//    CPPUNIT_TEST(ComputeSlopeAspectInterface_ReturnM1);
//    CPPUNIT_TEST(ComputeSlopeAspectInterface_ReturnM2);
//
//    CPPUNIT_TEST(ComputeObstacleMapInterface_OK);
//    CPPUNIT_TEST(ComputeObstacleMapInterface_ReturnM1);
//    CPPUNIT_TEST(ComputeObstacleMapInterface_ReturnM2);
//
//
//    CPPUNIT_TEST(ComputeContourInterface_OK);
//    // 返回-1时是gdal版本过低
//    CPPUNIT_TEST(ComputeContourInterface_ReturnM2);
//    CPPUNIT_TEST(ComputeContourInterface_ReturnM3);
//    CPPUNIT_TEST(ComputeContourInterface_ReturnM4);


    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        CTestDemAnalyse();
        virtual ~CTestDemAnalyse();
   public:
        void setUp();//cppunit 系统默认的初始化
        void tearDown();//cppunit 系统默认的销毁函数
        //测试函数
    /**
    * @fn ComputeViewShedInterface_OK()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入参数正常时生成通视图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeViewShedInterface_OK();

    /**
    * @fn ComputeViewShedInterface_ReturnM2()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入DEM源文件参数异常时生成通视图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeViewShedInterface_ReturnM2();

    /**
    * @fn ComputeViewShedInterface_ReturnM4()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入视点位置参数异常时生成通视图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeViewShedInterface_ReturnM4();



    /**
    * @fn ComputeSlopeInterface_OK()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入参数正常时生成坡度图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeSlopeInterface_OK();

    /**
    * @fn ComputeSlopeInterface_ReturnM1()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入文件路径错误时生成坡度图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeSlopeInterface_ReturnM1();

    /**
    * @fn ComputeSlopeInterface_ReturnM2()
    * @date 2012.2.8
    * @author 李巍
    * @brief 计算坡度失败时生成坡度图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeSlopeInterface_ReturnM2();

    /**
    * @fn ComputeSlopeAspectInterface_OK()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入参数正常时生成坡向图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeSlopeAspectInterface_OK();

    /**
    * @fn ComputeSlopeAspectInterface_ReturnM1()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入文件路径错误时生成坡向图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeSlopeAspectInterface_ReturnM1();

    /**
    * @fn ComputeSlopeInterface_ReturnM2()
    * @date 2012.2.8
    * @author 李巍
    * @brief 计算坡向失败时生成坡向图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeSlopeAspectInterface_ReturnM2();

    /**
    * @fn ComputeObstacleMapInterface_OK()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入参数正常时生成障碍图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeObstacleMapInterface_OK();

    /**
    * @fn ComputeObstacleMapInterface_ReturmM1()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入文件路径错误时生成障碍图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeObstacleMapInterface_ReturnM1();

    /**
    * @fn ComputeObstacleMapInterface_ReturmM2()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试生成障碍错误时生成障碍图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeObstacleMapInterface_ReturnM2();

     /**
    * @fn ComputeContourInterface_OK()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入参数正常时生成等高线图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeContourInterface_OK();

    /**
    * @fn ComputeContourInterface_ReturnM2()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入参数等高距为0时生成等高线图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeContourInterface_ReturnM2();

    /**
    * @fn ComputeContourInterface_ReturnM3()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输入文件名有误时生成等高线图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeContourInterface_ReturnM3();

    /**
    * @fn ComputeContourInterface_ReturnM4()
    * @date 2012.2.8
    * @author 李巍
    * @brief 测试输出文件名有误时生成等高线图的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void ComputeContourInterface_ReturnM4();

    protected:
    private:
};

#endif // CTESTDEMANALYSE_H
