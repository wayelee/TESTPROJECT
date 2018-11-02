/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestMat.cpp
* @date 2012.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩阵基础运算测试源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "TestMat.h"
#include "../mlRVML/mlMat.h"
#include "../mlRVML/mlBase.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestMat,"alltest" );
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
CTestMat::CTestMat()
{
    //ctor
}
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
CTestMat::~CTestMat()
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
void CTestMat::setUp()
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
void CTestMat::tearDown()
{

}
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
void CTestMat::TestmlMat2CvMat_ok()
{
    OriAngle Ori;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;
    CmlMat RMat;
    OPK2RMat(  &Ori, &RMat );
    CvMat* pcvMat = cvCreateMat( 3, 3, CV_64F );
    bool result = mlMat2CvMat( &RMat, pcvMat);
    cvReleaseMat(&pcvMat);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestCvMat2mlMat_ok()
{
    OriAngle Ori;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;
    CmlMat RMat,mlMat;
    OPK2RMat(  &Ori, &RMat );
    CvMat* pcvMat = cvCreateMat( 3, 3, CV_64F );
    mlMat2CvMat( &RMat, pcvMat);
    mlMat.Initial(3,3);
    bool result = CvMat2mlMat(pcvMat,&mlMat);
    cvReleaseMat(&pcvMat);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatTrans_ok()
{
    OriAngle Ori;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;
    CmlMat RMat,TransMat;
    OPK2RMat(  &Ori, &RMat );
    bool result = mlMatTrans(&RMat,&TransMat);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatAdd_ok()
{
    OriAngle Ori1, Ori2;
    Ori1.omg = 0.3;    Ori1.phi = 0.5;    Ori1.kap = 0.8;
    Ori2.omg = 0.4;    Ori2.phi = 0.6;    Ori2.kap = 0.9;
    CmlMat RMat1,RMat2,RMatOut;
    OPK2RMat(  &Ori1, &RMat1 );
    OPK2RMat(  &Ori2, &RMat2 );
    bool result = mlMatAdd(&RMat1,&RMat2,&RMatOut);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatSub_ok()
{
    OriAngle Ori1, Ori2;
    Ori1.omg = 0.3;    Ori1.phi = 0.5;    Ori1.kap = 0.8;
    Ori2.omg = 0.4;    Ori2.phi = 0.6;    Ori2.kap = 0.9;
    CmlMat RMat1,RMat2,RMatOut;
    OPK2RMat(  &Ori1, &RMat1 );
    OPK2RMat(  &Ori2, &RMat2 );
    bool result = mlMatSub(&RMat1,&RMat2,&RMatOut);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatMul_ok()
{
    OriAngle Ori1, Ori2;
    Ori1.omg = 0.3;    Ori1.phi = 0.5;    Ori1.kap = 0.8;
    Ori2.omg = 0.4;    Ori2.phi = 0.6;    Ori2.kap = 0.9;
    CmlMat RMat1,RMat2,RMatOut;
    OPK2RMat(  &Ori1, &RMat1 );
    OPK2RMat(  &Ori2, &RMat2 );
    bool result = mlMatMul(&RMat1,&RMat2,&RMatOut);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatInv_ok()
{
    OriAngle Ori;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;
    CmlMat RMat,InvMat;
    OPK2RMat(  &Ori, &RMat );
    bool result = mlMatInv(&RMat,&InvMat);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatDet_ok()
{
    OriAngle Ori;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;
    CmlMat RMat;
    DOUBLE res;
    OPK2RMat(  &Ori, &RMat );
    bool result = mlMatDet(&RMat,res);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatSolveSVD_ok()
{
    CmlMat RMat1,RMat2,RMatOut;
    OriAngle Ori1;
    Ori1.omg = 0.3;    Ori1.phi = 0.5;    Ori1.kap = 0.8;
    OPK2RMat(  &Ori1, &RMat1 );
    RMat2.Initial(3,1);
    RMat2.SetAt(0,0,4);    RMat2.SetAt(1,0,5);    RMat2.SetAt(2,0,6);
    bool result = mlMatSolveSVD(&RMat1,&RMat2,&RMatOut);
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatSVD_ok()
{
    OriAngle Ori;
    Ori.omg = 0.3;
    Ori.phi = 0.5;
    Ori.kap = 0.8;
    CmlMat RMat,MatU,MatW,MatV;
    DOUBLE res;
    OPK2RMat(  &Ori, &RMat );
    bool result = mlMatSVD( &RMat,&MatU, &MatW, &MatV );
    CPPUNIT_ASSERT(result == true);
}
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
void CTestMat::TestmlMatCross3_ok()
{
    CmlMat RMat1,RMat2,RMatOut;
    RMat1.Initial(3,1);
    RMat1.SetAt(0,0,1);    RMat1.SetAt(1,0,2);    RMat1.SetAt(2,0,3);
    RMat2.Initial(3,1);
    RMat2.SetAt(0,0,4);    RMat2.SetAt(1,0,5);    RMat2.SetAt(2,0,6);
    bool result = mlMatCross3( &RMat1,&RMat2, &RMatOut );
    CPPUNIT_ASSERT(result == true);
}




