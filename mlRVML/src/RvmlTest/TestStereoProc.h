/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestStereoProc.h
* @date 2012.2.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 立体影像处理测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CTESTSTEREOPROC_H
#define CTESTSTEREOPROC_H

#include <cppunit/extensions/HelperMacros.h>

/**
* @class CTestStereoProc
* @date 2011.11.18
* @author
* @brief
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CTestStereoProc: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestStereoProc);
    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestGetRansacPts_ok);
//    CPPUNIT_TEST(TestGetRansacPts2_ok);
//    CPPUNIT_TEST(TestGetRansacPts3_ok);


//    CPPUNIT_TEST(TestFilterPtsByMedian_ok);
//    CPPUNIT_TEST(TestTemplateMatchInFeatPt_ok);
//    CPPUNIT_TEST(TestTemplateMatch_ok);
//    CPPUNIT_TEST(TestTemplateMatchInRegion_ok);
//    CPPUNIT_TEST(TestLsMatchInFrameImg_ok);
//    CPPUNIT_TEST(TestLsMatch_ok);
//    CPPUNIT_TEST(TestInterSectionInFrameImg_ok);
//    CPPUNIT_TEST(TestInterSectionInFrameImg2_ok);
//    CPPUNIT_TEST(TestInterSectionPt2d_ok);
//    CPPUNIT_TEST(TestInterSection_ok);

//    CPPUNIT_TEST(TestGetEpipolarImg_ok);
//    CPPUNIT_TEST(TestGetEpipolarImg2_ok);

//    CPPUNIT_TEST(TestGetRansacPtsByAffineT_ok);
//    CPPUNIT_TEST(TestGetRansacPtsByAffineT2_ok);
//    CPPUNIT_TEST(TestMatch2LargeImg_ok);

//    CPPUNIT_TEST(TestDenseMatch_ok);
//    CPPUNIT_TEST(TestDisEstimate_ok);
//    CPPUNIT_TEST(TestUniquePt_ok);

    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestStereoProc
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestStereoProc();

    /**
    * @fn ~CTestStereoProc
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 析构函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestStereoProc();

public:
    /**
    * @fn setup()
    * @date 2011.12.1
    * @author
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
    * @author
    * @brief 系统默认的销毁函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void tearDown();//cppunit 系统默认的销毁函数
    /**
    * @fn TestGetEpipolarImg_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试核线影像生成入口函数功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetEpipolarImg_ok();
    /**
    * @fn TestGetEpipolarImg2_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试将非核线影像重采样成核线影像功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetEpipolarImg2_ok();
    /**
    * @fn TestGetRansacPts_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试Ransac去除粗差功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetRansacPts_ok();
    /**
    * @fn TestGetRansacPts2_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试Ransac去除粗差功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetRansacPts2_ok();
    /**
    * @fn TestGetRansacPts3_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试Ransac去除粗差功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetRansacPts3_ok();
    /**
    * @fn TestFilterPtsByMedian_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试中值滤波功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestFilterPtsByMedian_ok();
    /**
    * @fn TestTemplateMatchInFeatPt_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试图像间特征点模板匹配功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestTemplateMatchInFeatPt_ok();
    /**
    * @fn TestTemplateMatch_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试立体图像间模板匹配功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestTemplateMatch_ok();
    /**
    * @fn TestTemplateMatchInRegion_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试根据左影像上一点取得右影像上同名点功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestTemplateMatchInRegion_ok();
    /**
    * @fn TestLsMatchInFrameImg_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试面阵影像最小二乘匹配，得到子像素匹配点功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestLsMatchInFrameImg_ok();
    /**
    * @fn TestLsMatch_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试面阵影像最小二乘匹配，根据左影像上一点取得右影像上同名点功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestLsMatch_ok();
    /**
    * @fn TestInterSectionInFrameImg_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试面阵影像空间前方交会功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestInterSectionInFrameImg_ok();
    /**
    * @fn TestInterSectionInFrameImg2_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试面阵影像空间前方交会功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestInterSectionInFrameImg2_ok();
    /**
    * @fn TestInterSectionPt2d_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试不含畸变校正功能的通用空间前方交会函数功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestInterSectionPt2d_ok();
    /**
    * @fn TestInterSection_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试不含畸变校正功能的通用空间前方交会函数功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestInterSection_ok();
    /**
    * @fn TestDenseMatch_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试影像密集匹配功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestDenseMatch_ok();
    /**
    * @fn TestDisEstimate_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试根据正确匹配的特征点生成三角网进行视差范围预测功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestDisEstimate_ok();
    /**
    * @fn TestUniquePt_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试去掉特征点匹配中重复的点功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestUniquePt_ok();
    /**
    * @fn TestGetRansacPtsByAffineT_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试利用Ransac方法剔除立体匹配点粗差功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetRansacPtsByAffineT2_ok();
    /**
    * @fn TestGetRansacPtsByAffineT_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试利用Ransac方法放射变换模型剔除立体匹配点粗差功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetRansacPtsByAffineT_ok();
    /**
    * @fn TestGetRansacPtsByAffineT_ok
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试两张大影像间的匹配功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestMatch2LargeImg_ok();

    /**
    * @fn TestDisparityMap_ok
    * @date 2012.2.10
    * @author 彭嫚 ylliu@irsa.ac.cn
    * @brief 测试根据密集匹配点生成视差图的功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestDisparityMap_ok();

protected:
private:
};

#endif // CTESTSTEREOPROC_H
