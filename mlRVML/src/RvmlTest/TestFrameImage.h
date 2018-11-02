/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestFrameImage.cpp
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像处理测试源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef CTESTFRAMEIMAGE_H
#define CTESTFRAMEIMAGE_H

#include <cppunit/extensions/HelperMacros.h>
class CTestFrameImage: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestFrameImage);
    //添加测试用例到TestSuite
    //CPPUNIT_TEST(TestLoadFile_ok);
    //CPPUNIT_TEST(TestGetUnDistortImg_ok);
    //CPPUNIT_TEST(TestGetUnDistortImg2_ok);
    //CPPUNIT_TEST(UnDisCorToPlaneFrame_ok);
    //CPPUNIT_TEST(UnDisCorToPicCoord_ok);
    //CPPUNIT_TEST(TestBilinearInterpolation_ok);
    //CPPUNIT_TEST(TestGetDistortionCoordinate_ok);
    //CPPUNIT_TEST(TestGrayInterpolation_ok);
    //CPPUNIT_TEST(TestGetBilinearValue_ok);
    // CPPUNIT_TEST(TestCleanDeadPix_ok);
 //    CPPUNIT_TEST(TestCleanDeadPix_abnormal);
    // CPPUNIT_TEST(TestExtractFeatPtByForstner_ok);
    // CPPUNIT_TEST(TestExtractFeatPtByForstner_abnormal);
    //CPPUNIT_TEST(TestGetBilinearValue_abnormal);
    //CPPUNIT_TEST(TestGrayInterpolation_abnormal);
    //CPPUNIT_TEST(TestBilinearInterpolation_abnormal);
    //CPPUNIT_TEST(TestGetDistortionCoordinate_abnormal);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        CTestFrameImage();
        virtual ~CTestFrameImage();
   public:
        void setUp();//cppunit 系统默认的初始化
        void tearDown();//cppunit 系统默认的销毁函数
        //测试函数

        void TestLoadFile_ok();
        /**
        * @fn TestGetUnDistortImg_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试面阵相机影像畸变校正功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetUnDistortImg_ok();
        /**
        * @fn TestGetUnDistortImg2_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试面阵相机影像畸变校正功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetUnDistortImg2_ok();
        /**
        * @fn UnDisCorToPlaneFrame_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算畸变改正后坐标并转换成像平面坐标系功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void UnDisCorToPlaneFrame_ok();
        /**
        * @fn UnDisCorToPicCoord_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算畸变改正后坐标.转换成图像坐标系功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void UnDisCorToPicCoord_ok();
        /**
        * @fn BilinearInterpolation_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试双线性内插功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestBilinearInterpolation_ok();
        /**
        * @fn TestGetDistortionCoordinate_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试计算畸变改正后点坐标功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetDistortionCoordinate_ok();
        /**
        * @fn GetDistortionCoordinate_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试图像灰度内插功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGrayInterpolation_ok();
        /**
        * @fn GetBilinearValue_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试影像灰度值的双线性内插功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetBilinearValue_ok();
        /**
        * @fn TestCleanDeadPix_ok
        * @date 2011.12.1
        * @author 张重阳 zhangchy@irsa.ac.cn
        * @brief 测试线阵图像去除坏点功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCleanDeadPix_ok();
        /**
        * @fn TestCleanDeadPix_abnormal
        * @date 2011.12.1
        * @author 张重阳 zhangchy@irsa.ac.cn
        * @brief 测试线阵图像去除坏点功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestCleanDeadPix_abnormal();
        /**
        * @fn GetBilinearValue_abnormal
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试mlGetBilinearValue函数输入影像块为空的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetBilinearValue_abnormal();
        /**
        * @fn TestGrayInterpolation_abnormal
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试mlGrayInterpolation函数输入影像为空的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGrayInterpolation_abnormal();
        /**
        * @fn TestBilinearInterpolation_abnormal
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试mlBilinearInterpolation函数输入影像为空的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestBilinearInterpolation_abnormal();
        /**
        * @fn TestGetDistortionCoordinate_abnormal
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试mlGetDistortionCoordinate函数输入影像为空的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestGetDistortionCoordinate_abnormal();
        /**
        * @fn TestExtractFeatPtByForstner_abnormal
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试ExtractFeatPtByForstner函数输入影像块无法转换成IplImg的异常情况
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestExtractFeatPtByForstner_abnormal();
        /**
        * @fn TestExtractFeatPtByForstner_ok
        * @date 2011.12.1
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 测试面阵相机影像Forstner方法提取特征点功能
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        void TestExtractFeatPtByForstner_ok();
    protected:
    private:
};

#endif // CTESTFRAMEIMAGE_H
