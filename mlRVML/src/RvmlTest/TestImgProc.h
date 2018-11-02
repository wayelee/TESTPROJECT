/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestImgProc.h
* @date 2011.11.18
* @author 
* @brief 
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CTESTIMGPROC_H
#define CTESTIMGPROC_H

#include <cppunit/extensions/HelperMacros.h>

/**
* @class CTestImgProc
* @date 2011.11.18
* @author 
* @brief 
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CTestImgProc: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestImgProc);
    //添加测试用例到TestSuite
    //CPPUNIT_TEST(TestASCIIDemToGeoTiff_ok);

    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

public:
    /**
    * @fn CTestImgProc()
    * @date 2011.12.1
    * @author 
    * @brief 空参构造函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CTestImgProc();

    /**
    * @fn ~CTestImgProc()
    * @date 2011.12.1
    * @author 
    * @brief 析构函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CTestImgProc();

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
    * @fn TestCalBlockCol_ok()
    * @date 2011.12.1
    * @author 
    * @brief 
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    //void TestASCIIDemToGeoTiff_ok();


protected:
private:
};

#endif // CTESTIMGPROC_H
