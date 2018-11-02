#ifndef CTESTCOORDTRANS_H
#define CTESTCOORDTRANS_H

#include <cppunit/extensions/HelperMacros.h>

class CTestCoordTrans: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestCoordTrans);
    //添加测试用例到TestSuite
    //CPPUNIT_TEST(TestmlCoordTrans);
//    CPPUNIT_TEST(TestmlCoordTrans_OK);
//    CPPUNIT_TEST(TestmlCoordTransResult1_OK);
//    CPPUNIT_TEST(TestmlCoordTransResult2_OK);
//    CPPUNIT_TEST(TestmlCalcTransMatrix_case2);
//    CPPUNIT_TEST(TestmlCalcTransMatrix_case3);
//    CPPUNIT_TEST(TestmlCalcTransMatrix_case4);
//    CPPUNIT_TEST(TestmlCoordTransResult1_abnormal1);
//
//    CPPUNIT_TEST(TestmlCalcTransMatrixByLatLong);
       //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        CTestCoordTrans();
        virtual ~CTestCoordTrans();

    public:
        void setUp();//cppunit 系统默认的初始化
        void tearDown();//cppunit 系统默认的销毁函数
        //测试函数
       /**
        * @fn TestmlCoordTrans_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时月固系到局部坐标系转换功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestmlCoordTrans_OK();

       /**
        * @fn TestmlCoordTransResult1_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时坐标转换功能（正算）
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */

        void TestmlCalcTransMatrix_case2();
        void TestmlCalcTransMatrix_case3();
        void TestmlCalcTransMatrix_case4();

        void TestmlCoordTransResult1_OK();



        /**
        * @fn TestmlCoordTransResult1_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时坐标转换功能（正算）
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestmlCoordTransResult2_OK();

        void TestmlCoordTransResult1_abnormal1();
        //旋转矩阵维数和坐标维数不相等

        void TestmlCalcTransMatrixByLatLong();

    protected:
    private:
};

#endif // CTESTCOORDTRANS_H
