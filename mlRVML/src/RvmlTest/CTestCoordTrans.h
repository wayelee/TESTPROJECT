#ifndef CTESTCOORDTRANS_H
#define CTESTCOORDTRANS_H

#include <cppunit/extensions/HelperMacros.h>

class CTestCoordTrans: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestCoordTrans);
    //添加测试用例到TestSuite
    CPPUNIT_TEST(TestmlCoordTrans);

    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        CTestCoordTrans();
        virtual ~CTestCoordTrans();

    public:
        void setUp();//cppunit 系统默认的初始化
        void tearDown();//cppunit 系统默认的销毁函数
        void TestmlCoordTrans();

    protected:
    private:
};

#endif // CTESTCOORDTRANS_H
