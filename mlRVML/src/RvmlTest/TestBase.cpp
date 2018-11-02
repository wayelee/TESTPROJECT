/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestBase.cpp
* @date 2012.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 工程公共函数测试源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "TestBase.h"
#include "../mlRVML/mlBase.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestBase,"alltest" );
/**
* @fn CTestBase
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestBase::CTestBase()
{
    //ctor
}
/**
* @fn ~CTestBase
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestBase::~CTestBase()
{
    //dtor
}

/**
* @fn setup()
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 系统默认的初始化函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestBase::setUp()
{

}
/**
* @fn tearDown()
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 系统默认的销毁函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestBase::tearDown()
{

}
/**
* @fn TestPtInRect_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试判断某点是否在矩形框内功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestPtInRect_ok()
{
    MLRect rect;
    rect.dXMin = 0;
    rect.dYMin = 0;
    rect.dXMax = 10;
    rect.dYMax = 10;
    Pt2d pt;
    pt.X = 5.5;
    pt.Y = 3.5;
    bool result = PtInRect( pt, rect );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestPtInRect_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试判断某点是否在矩形框内功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestPtInRect2_ok()
{
    MLRect rect;
    rect.dXMin = 0;
    rect.dYMin = 0;
    rect.dXMax = 10;
    rect.dYMax = 10;
    Pt2d pt;
    pt.X = 5;
    pt.Y = 3;
    bool result = PtInRect( pt, rect );
    CPPUNIT_ASSERT(result == true);
}
 /**
        * @fn TestPtInRect_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试判断某点是否在矩形框内准确性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
void CTestBase:: TestPtInRect_ReturnFalse()
{
    MLRect rect;
    rect.dXMin = 0;
    rect.dYMin = 0;
    rect.dXMax = 10;
    rect.dYMax = 10;
    Pt2d pt;
    pt.X = -1;
    pt.Y = -1;
    bool result = PtInRect( pt, rect );
    CPPUNIT_ASSERT(result == false);

}
        /**
        * @fn TestPtInRect2_ReturnFalse
        * @date 2011.12.1
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 测试判断某点是否在矩形框内准确性
        * @version 1.0
        * @return 无返回值
        * @par 修改历史
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
void CTestBase:: TestPtInRect2_ReturnFalse()
{
     MLRect rect;
    rect.dXMin = 0;
    rect.dYMin = 0;
    rect.dXMax = 10;
    rect.dYMax = 10;
    Pt2d pt;
    pt.X = -1.5;
    pt.Y = -1.5;
    bool result = PtInRect( pt, rect );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestOPK2RMat_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试旋转角OPK转换到旋转矩阵功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestOPK2RMat_ok()
{
    OriAngle Ori;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;//8.62653	1.09129	-0.854485
    CmlMat RMat;
    bool result = OPK2RMat(  &Ori, &RMat );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestOPK2RMat_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试旋转角OPK转换到旋转矩阵容错性
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestOPK2RMat_ReturnFalse()
{
    OriAngle Ori;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;//8.62653	1.09129	-0.854485
    CmlMat RMat;
    RMat.Initial(4,4);
    bool result = OPK2RMat(  &Ori, &RMat );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestRMat2OPK_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试旋转矩阵转换到旋转角OPK功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestRMat2OPK_ok()
{
    OriAngle Ori,Dst;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;
    CmlMat RMat;
    OPK2RMat(  &Ori, &RMat );
    bool result = RMat2OPK( &RMat, &Dst );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestOpen_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试初始化输出文件的路径，及是否需要打印在屏幕上.同时输入此次日志的头信息功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestOpen_ok()
{
    CmlLogRecord cls;
    const SCHAR*  strFilePath = "../../../UnitTestData/TestBase/log.txt";
    bool result = cls.Open( strFilePath, true );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestOpen_ReturnFalse
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试初始化输出文件的路径，及是否需要打印在屏幕上.同时输入此次日志的头信息功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestOpen_ReturnFalse()
{
    CmlLogRecord cls;
    const SCHAR*  strFilePath = "../../../UnitTestData/TestBase/log.txt";
    cls.Open(strFilePath, true );
    bool result = cls.Open( strFilePath, true );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestClose_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试关闭文件功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestClose_ok()
{
    CmlLogRecord cls;
    bool result = cls.Close();
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestAddSuccessQuitMsg_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试函数运行正常至退出消息函数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestAddSuccessQuitMsg_ok()
{
    CmlLogRecord cls;
    cls.AddSuccessQuitMsg("TestClose_ok" ,"TestBase.cpp", 194 );

}
/**
* @fn TestAddErrorMsg_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试函数运行遭遇预知错误消息函数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestAddErrorMsg_ok()
{
    CmlLogRecord cls;
    cls.AddErrorMsg( "Unknown Error!", "TestClose_ok" ,"TestBase.cpp", 194 );
}
/**
* @fn TestAddExceptionMsg_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试函数运行出现异常消息函数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestAddExceptionMsg_ok()
{
    CmlLogRecord cls;
    cls.AddExceptionMsg( "Unknown Exception!", "TestClose_ok" ,"TestBase.cpp", 194 );
}
/**
* @fn TestAddNoticeMsg_ok
* @date 2011.12.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 测试函数运行出现提示消息函数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBase::TestAddNoticeMsg_ok()
{
    CmlLogRecord cls;
    cls.AddExceptionMsg( "Notice Message!", "TestClose_ok" ,"TestBase.cpp", 194 );
}

