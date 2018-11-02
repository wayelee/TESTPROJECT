#include "TestCamCalib.h"
#include "../../../src/mlRVML/mlCamCalib.h"
#include "../../../src/RvmlDemo/camCalibIO.h"


CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestCamCalib,"alltest" );

CTestCamCalib::CTestCamCalib()
{
    //ctor
}

CTestCamCalib::~CTestCamCalib()
{
    //dtor
}

/** @brief tearDown
  *
  * @todo: document this function
  */
void CTestCamCalib::tearDown()
{

}

/** @brief setUp
  *
  * @todo: document this function
  */
void CTestCamCalib::setUp()
{

}

/**
* @fn TestmlSingleCamCalib_OK()
* @date 2012.02
* @author 吴凯
* @brief 测试单相机标定函数功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestCamCalib::TestmlSingleCamCalib_OK()
{
    CCamCalibIO camIO;
    char* strInputPath = "../../../UnitTestData/TestCamCalib/IFLI_SINGLELEFTCAMPOINTS.txt" ; //加入相关文件路径
    camIO.readCamSignPts(strInputPath);
    CmlCamCalib cal;
    bool result = cal.mlSingleCamCalib(camIO.vecLImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH , camIO.inLCamPara , camIO.exLCamPara , camIO.vecErrorPts) ;
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestmlSingleCamCalibcase2()
* @date 2012.02
* @author 吴凯
* @brief 测试单相机标定函数功能 flag=0
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/

void CTestCamCalib::TestmlSingleCamCalibcase2()  //flag = 0
{
    CCamCalibIO camIO;
    char* strInputPath = "../../../UnitTestData/TestCamCalib/IFLI_SINGLELEFTCAMPOINTS.txt" ; //加入相关文件路径
    camIO.readCamSignPts(strInputPath);
    CmlCamCalib cal;
    bool result = cal.mlSingleCamCalib(camIO.vecLImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH , camIO.inLCamPara , camIO.exLCamPara , camIO.vecErrorPts,0) ;
    CPPUNIT_ASSERT(result == true);

}


/**
* @fn TestmlStereoCamCalib_OK()
* @date 2012.02
* @author 吴凯
* @brief 测试双相机标定函数功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestCamCalib::TestmlStereoCamCalib_OK()
{
    CCamCalibIO camIO;
    char* strInputPath = "../../../UnitTestData/TestCamCalib/IFLI_STEREOCAMPOINTS.txt" ; //加入相关文件路径
    camIO.readCamSignPts(strInputPath);
    CmlCamCalib cal;
    bool result = cal.mlStereoCamCalib(camIO.vecLImg2DPts , camIO.vecRImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH ,
                                          camIO.inLCamPara , camIO.inRCamPara , camIO.exLCamPara , camIO.exRCamPara , camIO.exStereoPara ,camIO.vecErrorPts);
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestmlStereoCamCalibcase2()
* @date 2012.02
* @author 吴凯
* @brief 测试双相机标定函数功能 flag=0
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestCamCalib::TestmlStereoCamCalibcase2()  //flag = 0
{
    CCamCalibIO camIO;
    char* strInputPath = "../../../UnitTestData/TestCamCalib/IFLI_STEREOCAMPOINTS.txt" ; //加入相关文件路径
    camIO.readCamSignPts(strInputPath);
    CmlCamCalib cal;
    bool result = cal.mlStereoCamCalib(camIO.vecLImg2DPts , camIO.vecRImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH ,
                                          camIO.inLCamPara , camIO.inRCamPara , camIO.exLCamPara , camIO.exRCamPara , camIO.exStereoPara ,camIO.vecErrorPts,0);
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestmlStereoCamCalibcase2()
* @date 2012.02
* @author 吴凯
* @brief 测试后方交会函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestCamCalib::TestbackProjection()
{
    CCamCalibIO camIO;
    char* strInputPath = "../../../UnitTestData/TestCamCalib/IFLI_SINGLELEFTCAMPOINTS.txt";
    camIO.readCamSignPts(strInputPath);
    CmlCamCalib cal;
    bool result = cal.backProjection(camIO.vecLImg2DPts,camIO.vecObj3DPts,camIO.inLCamPara,camIO.exLCamPara);
    CPPUNIT_ASSERT(result == true);

}
