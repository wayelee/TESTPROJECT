/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TesRvml.h
* @date 2012.3.18
* @author
* @brief
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CTESTRVML_H
#define CTESTRVML_H

#include <cppunit/extensions/HelperMacros.h>

/**
* @class CTestRvml
* @date 2012.3.18
* @author
* @brief
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CTestRvml: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestRvml);
    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestCameraCalibration_ok1);
//    CPPUNIT_TEST(TestCameraCalibration_ok2);
//    CPPUNIT_TEST(TestMonoSurvey_ok);
//    CPPUNIT_TEST(TestWideBaseAnalysis_ok);
//    CPPUNIT_TEST(TestWideBaseMapping_ok);
//    CPPUNIT_TEST(TestDenseMatch_ok);
//    CPPUNIT_TEST(TestDisparityMap_ok);
//    CPPUNIT_TEST(TestCoordTrans_ok);
//    CPPUNIT_TEST(TestCoordTransXYZ_ok);

//    CPPUNIT_TEST(TestSatMapping_ok);
      CPPUNIT_TEST(TestMapByInteBlock_ok);
//      CPPUNIT_TEST(TestPanoMosic_ok);
//     CPPUNIT_TEST(TestTinSimply_ok);
//     CPPUNIT_TEST(TestVisualImage_ok);
//
//    CPPUNIT_TEST(TestPano2Prespective_ok);
//    CPPUNIT_TEST(TestDemMosaic_ok);
//    CPPUNIT_TEST(TestComputeInsightMap_ok);
//    CPPUNIT_TEST(TestComputeSlopeAspectMap_ok);
//    CPPUNIT_TEST(TestComputeBarrierMap_ok);
//    CPPUNIT_TEST(TestComputeContourMap_ok);
//    CPPUNIT_TEST(TestGeoRasterCut_GeoCoo_ok);
//    CPPUNIT_TEST(TestGeoRasterCut_Pixel_ok);
//    CPPUNIT_TEST(TestLocalByMatch_ok);
//    CPPUNIT_TEST(TestLocalByIntersection_ok);
//
//    CPPUNIT_TEST(TestLocalIn2Site_ok);
//    CPPUNIT_TEST(TestLocalBySeqence_ok);
//    CPPUNIT_TEST(TestLocalByLander_ok);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestRvml()
    * @date 2012.3.18
    * @author
    * @brief 空参构造函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestRvml();

    /**
    * @fn ~CTestRvml()
    * @date 2012.3.18
    * @author
    * @brief 析构函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestRvml();

public:
    /**
    * @fn setup()
    * @date 2012.3.18
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
    * @date 2012.3.18
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
    * @fn TestSingleCamCalib_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试单相机标定函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestCameraCalibration_ok1();

    /**
    * @fn TestStereoCamCalib_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试双相机标定函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestCameraCalibration_ok2();

    /**
    * @fn TestMonoSurvey_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试单目量测函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestMonoSurvey_ok();

    /**
    * @fn TestWideBaseAnalysis_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试最优基线计算函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestWideBaseAnalysis_ok();

    /**
    * @fn TestWideBaseMapping_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试长基线测图函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestWideBaseMapping_ok();

    /**
    * @fn TestDenseMatch_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试密集匹配函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestDenseMatch_ok();

    /**
    * @fn TestDisparityMap_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试视差图生成函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestDisparityMap_ok();

    /**
    * @fn TestCoordTransLat_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试坐标转换函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestCoordTransLat_ok();

    /**
    * @fn TestCoordTransXYZ_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试坐标转换函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestCoordTransXYZ_ok();

    /**
    * @fn TestSatMapping_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试嫦娥卫星影像测图函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestSatMapping_ok();

    /**
    * @fn TestMapByInteBlock_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试单站点测图函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestMapByInteBlock_ok();



    /**
    * @fn TestPanoMosic_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试全景拼接函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestPanoMosic_ok();

    /**
    * @fn TestTinSimply_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试三角网简化函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestTinSimply_ok();



    /**
    * @fn TestVisualImage_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试仿真图像生成函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestVisualImage_ok();

    /**
    * @fn TestPano2Prespective_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试全景图像生成透视图像函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestPano2Prespective_ok();

    /**
    * @fn TestDemMosaic_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试DEM拼接函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestDemMosaic_ok();

    /**
    * @fn TestDemMosaic_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试通视图计算函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestComputeInsightMap_ok();

    /**
    * @fn TestComputeSlopeAspectMap_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试坡向图生成函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestComputeSlopeAspectMap_ok();

    /**
    * @fn TestComputeBarrierMap_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试障碍图生成函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestComputeBarrierMap_ok();

    /**
    * @fn TestComputeContourMap_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试等高线图生成函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestComputeContourMap_ok();

    /**
    * @fn TestGeoRasterCut_GeoCoo_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试DEM、DOM裁切函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestGeoRasterCut_GeoCoo_ok();

    /**
    * @fn TestGeoRasterCut_Pixel_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试DEM、DOM裁切函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestGeoRasterCut_Pixel_ok();

    /**
    * @fn TestLocalByMatch_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试卫星影像和地面影像间匹配实现定位函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalByMatch_ok();

    /**
    * @fn TestLocalByIntersection_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试后方交会定位函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalByIntersection_ok();

    /**
    * @fn TestLocalIn2Site_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试站点间定位函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalIn2Site_ok();

    /**
    * @fn TestLocalBySeqence_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试序列影像定位函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalBySeqence_ok();

    /**
    * @fn TestLocalByLander_ok()
    * @date 2012.3.18
    * @author
    * @brief 测试着陆器定位函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestLocalByLander_ok();




protected:
private:
};

#endif // CTESTKRINGING_H
