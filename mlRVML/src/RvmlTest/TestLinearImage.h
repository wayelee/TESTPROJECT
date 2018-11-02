/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestLinearImage.h
* @date 2011.2.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 线阵影像处理测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CTESTLINEARIMAGE_H
#define CTESTLINEARIMAGE_H

#include <cppunit/extensions/HelperMacros.h>

/**
* @class CTestLinearImage
* @date 2011.11.18
* @author
* @brief
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CTestLinearImage: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestLinearImage);
    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestGetEop_ok);
//    CPPUNIT_TEST(TestBLH2XYZ_ok);
//    CPPUNIT_TEST(TestXYZ2BLH_ok);

//    CPPUNIT_TEST(TestCE1OPK2RMat_ok);
//    CPPUNIT_TEST(TestCE1OPK2RMatvec_ok);
//    CPPUNIT_TEST(TestCE1InOrietation_ok);
//    CPPUNIT_TEST(TestGetCE1DOMup_ok);
//    CPPUNIT_TEST(TestGetCE1DOMdown_ok);
//    CPPUNIT_TEST(TestGetCE1DOMCoordinate_ok);

//    CPPUNIT_TEST(TestCE2OPK2RMat_ok);
//    CPPUNIT_TEST(TestCE2OPK2RMatvec_ok);
//    CPPUNIT_TEST(TestCE2InOrietation_ok);
//    CPPUNIT_TEST(TestGetCE2DOMup_ok);
//    CPPUNIT_TEST(TestGetCE2DOMdown_ok);
//    CPPUNIT_TEST(TestGetCE2DOMCoordinate_ok);

//    CPPUNIT_TEST(TestGetEop_abnormal);
//    CPPUNIT_TEST(TestCE1OPK2RMat_abnormal);
//    CPPUNIT_TEST(TestCE1OPK2RMatvec_abnormal);
//    CPPUNIT_TEST(TestCE2OPK2RMat_abnormal);
//    CPPUNIT_TEST(TestCE2OPK2RMatvec_abnormal);
//    CPPUNIT_TEST(TestGetCE1DOMCoordinate_abnormal);
//    CPPUNIT_TEST(TestGetCE2DOMCoordinate_abnormal);
//    CPPUNIT_TEST(TestGetCE1DOM_abnormal);
//    CPPUNIT_TEST(TestGetCE2DOM_abnormal);

    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestLinearImage
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestLinearImage();
    /**
    * @fn ~CTestLinearImage
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestLinearImage();

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
    */
    void tearDown();//cppunit 系统默认的销毁函数
    /**
    * @fn TestGetEop_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试轨道测控数据多项式内插外方位元素功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetEop_ok();
    /**
    * @fn TestBLH2XYZ_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试将物方三维点由月球大地坐标系转成月固坐标系下的空间直角坐标功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestBLH2XYZ_ok();
    /**
    * @fn TestXYZ2BLH_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试将物方三维点由月固坐标系下的空间直角坐标转成月球大地坐标系功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestXYZ2BLH_ok();
    /**
    * @fn TestGetCE1DOMCoordinate_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试生成CE-1卫星影像DOM格网在原图像上的x,y坐标功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE1DOMCoordinate_ok();
    /**
    * @fn TestGetCE2DOMCoordinate_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试生成CE-2卫星影像DOM格网在原图像上的x,y坐标功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE2DOMCoordinate_ok();
    /**
    * @fn TestCE1OPK2RMat_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试CE-1卫星影像外方位角元素转旋转矩阵功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE1OPK2RMat_ok();
    /**
    * @fn TestCE1OPK2RMatvec_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试将CE-1卫星影像六类外方位角元素转成线元素及旋转矩阵的形式功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE1OPK2RMatvec_ok();
    /**
    * @fn TestCE1InOrietation_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试CE-1卫星影像内定向功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE1InOrietation_ok();
    /**
    * @fn TestGetCE1DOMup_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试生成CE-1卫星上行影像DOM功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE1DOMup_ok();
    /**
    * @fn TestGetCE1DOMdown_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试生成CE-1卫星下行影像DOM功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE1DOMdown_ok();
    /**
    * @fn TestCE2OPK2RMat_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试CE-2卫星影像外方位角元素转旋转矩阵功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE2OPK2RMat_ok();
    /**
    * @fn TestCE2OPK2RMatvec_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试将CE-2卫星影像六类外方位角元素转成线元素及旋转矩阵的形式功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE2OPK2RMatvec_ok();
    /**
    * @fn TestCE2InOrietation_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试CE-2卫星影像内定向功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE2InOrietation_ok();
    /**
    * @fn TestGetCE2DOMup_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试生成CE-2卫星上行影像DOM功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE2DOMup_ok();
    /**
    * @fn TestGetCE2DOMdown_ok
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试生成CE-2卫星下行影像DOM功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE2DOMdown_ok();
    /**
    * @fn TestGetEop_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlGetEop函数中读入数据为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetEop_abnormal();
    /**
    * @fn TestCE1OPK2RMat_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlCE1OPK2RMat函数中读入矩阵大小不正确的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE1OPK2RMat_abnormal();
    /**
    * @fn TestCE1OPK2RMatvec_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlCE1OPK2RMat函数中读入数据为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE1OPK2RMatvec_abnormal();
    /**
    * @fn TestCE2OPK2RMat_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlCE1OPK2RMat函数中读入矩阵大小不正确的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE2OPK2RMat_abnormal();
    /**
    * @fn TestCE2OPK2RMatvec_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlCE2OPK2RMat函数中读入数据为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCE2OPK2RMatvec_abnormal();
    /**
    * @fn TestGetCE1DOMCoordinate_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlGetCE1DOMCoordinate函数中读入数据为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE1DOMCoordinate_abnormal();
    /**
    * @fn TestGetCE2DOMCoordinate_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlGetCE2DOMCoordinate函数中读入数据为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE2DOMCoordinate_abnormal();
    /**
    * @fn TestGetCE1DOM_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlGetCE1DOM函数中读入数据为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE1DOM_abnormal();
    /**
    * @fn TestGetCE2DOM_abnormal
    * @date 2011.12.1
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 测试mlGetCE2DOM函数中读入数据为空的异常情况
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestGetCE2DOM_abnormal();

protected:
private:
};

#endif // CTESTLINEARIMAGE_H
