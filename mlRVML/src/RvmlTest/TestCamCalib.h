#ifndef CTESTCAMCALIB_H
#define CTESTCAMCALIB_H

#include <cppunit/extensions/HelperMacros.h>

class CTestCamCalib: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestCamCalib);
    //添加测试用例到TestSuite
    //CPPUNIT_TEST(TestmlCoordTrans);

    //TestSuite声明完成
//    CPPUNIT_TEST(TestmlSingleCamCalib_OK);
//    CPPUNIT_TEST(TestmlStereoCamCalib_OK);
//    CPPUNIT_TEST(TestbackProjection);
//    CPPUNIT_TEST(TestmlSingleCamCalibcase2);
//    CPPUNIT_TEST(TestmlStereoCamCalibcase2);
    CPPUNIT_TEST_SUITE_END();

    public:
        CTestCamCalib();
        virtual ~CTestCamCalib();
   public:
        void setUp();//cppunit 系统默认的初始化
        void tearDown();//cppunit 系统默认的销毁函数
        void TestmlSingleCamCalib_OK();
        void TestmlStereoCamCalib_OK();
        void TestbackProjection();
        void TestmlSingleCamCalibcase2();
        void TestmlStereoCamCalibcase2();
        //测试函数

    protected:
    private:
};

#endif // CTESTCAMCALIB_H
