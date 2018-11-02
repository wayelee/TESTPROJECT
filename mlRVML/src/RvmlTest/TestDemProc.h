#ifndef CTESTDEMPROC_H
#define CTESTDEMPROC_H

#include <cppunit/extensions/HelperMacros.h>
class CTestDemProc: public CppUnit::TestFixture
{
    //声明一个TestSuite(即将此类名加入)
    CPPUNIT_TEST_SUITE(CTestDemProc);
    //添加测试用例到TestSuite
   // CPPUNIT_TEST(TestmlCoordTrans);
//    CPPUNIT_TEST(TestDemProc_OK_ByPix);
//    CPPUNIT_TEST(TestDemProc_OK_ByGeoPos);
//    CPPUNIT_TEST(TestImgReprj_OK);
//    CPPUNIT_TEST(TestmlTinSimply_OK);
//    CPPUNIT_TEST(TestBackfordinterSection_OK);
//    CPPUNIT_TEST(TestBuild2By2DPt_OK);
//    CPPUNIT_TEST(TestBackfordinterSection_abnormal);
//    CPPUNIT_TEST(TestBuild2By2DPt_OK);
//    CPPUNIT_TEST(TestBuild2By3DPt_OK);
//    CPPUNIT_TEST(TestBuild2Byvec3dPt_OK);
//    CPPUNIT_TEST(TestmlReproject_OK2);
    CPPUNIT_TEST(TestmlWriteToGeoFile_case1);
    CPPUNIT_TEST(TestmlWriteToGeoFile_case1);
//    CPPUNIT_TEST(TestGetValueFromTin);
//    CPPUNIT_TEST(TestGetCenterTriIndex_OK);
//    CPPUNIT_TEST(TestGetGridTriIndex_OK);
//    CPPUNIT_TEST(TestmlInitVariogram);


    //TestSuite声明完成
    CPPUNIT_TEST_SUITE_END();

    public:
        CTestDemProc();
        virtual ~CTestDemProc();
   public:
        void setUp();//cppunit 系统默认的初始化
        void tearDown();//cppunit 系统默认的销毁函数
        //测试函数

        /**
        * @fn TestDemProc_OK_ByPix()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时DEMDOM根据像素裁剪功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestDemProc_OK_ByPix();


        /**
        * @fn TestDemProc_OK_ByGeoPos()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时DEMDOM根据地理坐标裁剪功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestDemProc_OK_ByGeoPos();


        /**
        * @fn TestImgReprj_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时指定视角图像生成功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestImgReprj_OK();


        /**
        * @fn TestmlTinSimply_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时TIN简化功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestmlTinSimply_OK();

        /**
        * @fn TestBackfordinterSection_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时后方交会功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestBackfordinterSection_OK();

        /**
        * @fn TestBackfordinterSection_abnormal()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数已知点小于3时后方交会功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestBackfordinterSection_abnormal();

        /**
        * @fn TestImgReprj_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试输入参数正确时指定视角图像生成功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */

        void TestmlReproject_OK();

        void TestmlReproject_OK2();


        /**
        * @fn TestBuild2By2DPt_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试由二维点序列构TIN功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */

        void TestBuild2By2DPt_OK();

       /**
        * @fn TestBuild2By3DPt_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试由三维点序列构TIN功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestBuild2By3DPt_OK();

        /**
        * @fn TestBuild2Byvec3dPt_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试由vector三维点构TIN功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestBuild2Byvec3dPt_OK();


        /**
        * @fn TestmlWriteToGeoFile_case1()
        * @date 2011.12.1
        * @author 吴凯
        * @brief 测试写入geo文件功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestmlWriteToGeoFile_case1();
        /**
        * @fn TestmlWriteToGeoFile_case2()
        * @date 2011.12.1
        * @author 吴凯
        * @brief 测试写入geo文件功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestmlWriteToGeoFile_case2();
        /**
        * @fn TestGetValueFromTin()
        * @date 2011.12.1
        * @author 吴凯
        * @brief 测试TIN内插功能
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestGetValueFromTin();

        /**
        * @fn TestGetCenterTriIndex_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试获取中心三角形索引
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestGetCenterTriIndex_OK();


        /**
        * @fn TestmlInitVariogram()
        * @date 2011.12.1
        * @author 吴凯
        * @brief 测试初始化variogram
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestmlInitVariogram();


        /**
        * @fn TestGetGridTriIndex_OK()
        * @date 2011.12.1
        * @author 张重阳
        * @brief 测试获取格网三角形索引
        * @version 1.1
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        *
        */
        void TestGetGridTriIndex_OK();



    protected:
    private:
};

#endif // CTESTDEMPROC_H
