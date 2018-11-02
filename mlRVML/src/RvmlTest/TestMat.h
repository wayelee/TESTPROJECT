/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestMat.h
* @date 2011.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩阵基础运算测试头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CTESTMAT_H
#define CTESTMAT_H

#include <cppunit/extensions/HelperMacros.h>
class CTestMat: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestMat);
//    //添加测试用例到TestSuite
//    CPPUNIT_TEST(TestmlMat2CvMat_ok);
//    CPPUNIT_TEST(TestCvMat2mlMat_ok);
//    CPPUNIT_TEST(TestmlMatTrans_ok);
//    CPPUNIT_TEST(TestmlMatAdd_ok);
//    CPPUNIT_TEST(TestmlMatSub_ok);
//    CPPUNIT_TEST(TestmlMatMul_ok);
//    CPPUNIT_TEST(TestmlMatInv_ok);
//    CPPUNIT_TEST(TestmlMatDet_ok);
//    CPPUNIT_TEST(TestmlMatSolveSVD_ok);
//    CPPUNIT_TEST(TestmlMatSVD_ok);
//    CPPUNIT_TEST(TestmlMatCross3_ok);
    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestMat
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestMat();
    /**
    * @fn ~CTestMat
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestMat();

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
    @fn TestmlMat2CvMat_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试将CmlMat转化成CvMat功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMat2CvMat_ok();
    /**
    * @fn TestCvMat2mlMat_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试将CvMat转化成CmlMat功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestCvMat2mlMat_ok();
    /**
    * @fn TestmlMatTrans_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试矩阵转置功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatTrans_ok();
    /**
    * @fn TestmlMatAdd_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试矩阵加法功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatAdd_ok();
    /**
    * @fn TestmlMatSub_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试矩阵减法功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatSub_ok();
    /**
    * @fn TestmlMatMul_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试矩阵乘法功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatMul_ok();
    /**
    * @fn TestmlMatInv_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试矩阵求逆功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatInv_ok();
    /**
    * @fn TestmlMatDet_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试求矩阵行列式值功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatDet_ok();
    /**
    * @fn TestmlMatSolveSVD_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试解方程A*x=B功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatSolveSVD_ok();
    /**
    * @fn TestmlMatSVD_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试SVD分解功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatSVD_ok();
    /**
    * @fn TestmlMatCross3_ok
    * @date 2011.12.1
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 测试三维向量叉乘功能
    * @version 1.0
    * @return 无返回值
    * @par 修改历史
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void TestmlMatCross3_ok();
protected:
private:
};

#endif // CTESTMAT_H
