/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestRasterMosaic.h
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief DEM影像拼接测试类头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CTESTRASTERMOSAIC_H
#define CTESTRASTERMOSAIC_H

#include <cppunit/extensions/HelperMacros.h>

/**
* @class CTestRasterMosaic
* @date 2011.11.18
* @author
* @brief
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CTestRasterMosaic: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestRasterMosaic);
    //添加测试用例到TestSuite
    CPPUNIT_TEST(TestDEMMosaic_ok);

    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestRasterMosaic()
    * @date 2011.11.18
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 空参构造函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestRasterMosaic();

    /**
    * @fn ~CTestRasterMosaic()
    * @date 2011.11.18
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 析构函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestRasterMosaic();

public:
    /**
    * @fn setup()
    * @date 2011.12.1
    * @author 梁健 liangjian@irsa.ac.cn
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
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 系统默认的销毁函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void tearDown();//cppunit 系统默认的销毁函数

    /**
    * @fn TestDEMMosaic_ok()
    * @date 2011.12.1
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 测试DEM影像拼接正常实现的功能
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void TestDEMMosaic_ok();


protected:
private:
};

#endif // CTESTKRINGING_H
