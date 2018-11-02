/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestBlockCalculation.cpp
* @date 2012.2.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 影像分块测试源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "TestBlockCalculation.h"
#include "../mlRVML/mlBlockCalculation.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestBlockCalculation,"alltest" );
/**
* @fn CTestBlockCalculation
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestBlockCalculation::CTestBlockCalculation()
{
    //ctor
}
/**
* @fn ~CTestBlockCalculation
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CTestBlockCalculation::~CTestBlockCalculation()
{
    //dtor
}

/**
* @fn setup()
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 系统默认的初始化函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestBlockCalculation::setUp()
{

}
/**
* @fn tearDown()
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 系统默认的销毁函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestBlockCalculation::tearDown()
{

}

/**
* @fn TestCalBlockCol_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像沿列（X）方向分块的大小的块数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestCalBlockCol_ok()
{
    CmlBlockCalculation cls;
    bool result = cls.CalBlockCol(5000,2000);
    CPPUNIT_ASSERT(result == true);

}
/**
* @fn TestCalBlockRow_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像沿行（Y）方向分块的大小的块数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestCalBlockRow_ok()
{
    CmlBlockCalculation cls;
    bool result = cls.CalBlockRow(5000,2000);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetStartCol_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像按列（X）方向某一分块的起始列号功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestGetStartCol_ok()
{
    CmlBlockCalculation cls;
    cls.CalBlockCol(5000,2000);
    SINT result = cls.GetStartCol(1);
    CPPUNIT_ASSERT(result == 1600);
}
/**
* @fn TestGetStartRow_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像按行（Y）方向某一分块的起始行号功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestGetStartRow_ok()
{
    CmlBlockCalculation cls;
    cls.CalBlockRow(5000,2000);
    SINT result = cls.GetStartRow(1);
    CPPUNIT_ASSERT(result == 1600);
}
/**
* @fn TestGetBlockPos_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算某一影像块的起始位置和大小功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestGetBlockPos_ok()
{
    CmlBlockCalculation BlockData(3000,3000,200,200);
    BlockData.CalBlockCol( 1000, 512 );
    BlockData.CalBlockRow( 2000, 512 );
    Pt2i ptOrg;
    UINT nW,nH;
    bool result = BlockData.GetBlockPos( 0, 0, ptOrg, nW, nH );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestGetBlockPosLast_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算某一影像块最后一块的起始位置和大小功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestGetBlockPosLast_ok()
{
    CmlBlockCalculation BlockData(3000,3000,200,200);
    BlockData.CalBlockCol( 1000, 512 );
    BlockData.CalBlockRow( 1000, 512 );
    Pt2i ptOrg;
    UINT nW,nH;
    bool result = BlockData.GetBlockPos( 2, 2, ptOrg, nW, nH );
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCalBlock_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像沿行（列）方向分块的大小的块数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestCalBlock_ok()
{
    UINT nBlockL;
    UINT nCount;
    UINT nLastBlockL;
    CmlBlockCalculation cls;
    bool result = cls.CalBlock(5000,2000,3000,200,nBlockL,nCount,nLastBlockL);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCalBlock2_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像沿行（列）方向分块的大小的块数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestCalBlock2_ok()
{
    UINT nBlockL;
    UINT nCount;
    UINT nLastBlockL;
    CmlBlockCalculation cls;
    bool result = cls.CalBlock(4001,2000,3000,0,nBlockL,nCount,nLastBlockL);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCalBlock3_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像沿行（列）方向分块的大小的块数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestCalBlock3_ok()
{
    UINT nBlockL;
    UINT nCount;
    UINT nLastBlockL;
    CmlBlockCalculation cls;
    bool result = cls.CalBlock(500,2000,3000,0,nBlockL,nCount,nLastBlockL);
    CPPUNIT_ASSERT(result == true);
}
/**
* @fn TestCalBlock4_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像沿行（列）方向分块的大小的块数功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestCalBlock4_ok()
{
    UINT nBlockL;
    UINT nCount;
    UINT nLastBlockL;
    CmlBlockCalculation cls;
    bool result = cls.CalBlock(4001,2000,2000,0,nBlockL,nCount,nLastBlockL);
    CPPUNIT_ASSERT(result == true);
}

/**
* @fn TestGetBlock_ok
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试计算影像沿行（列）方向某一块的起始位置功能
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestGetBlock_ok()
{
    CmlBlockCalculation cls;
    SINT result = cls.GetBlock(0,2000,4,200);
    CPPUNIT_ASSERT(result == 0);
}
/**
* @fn TestGetBlockPos_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试GetBlockPos函数输入参数超限的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestGetBlockPos_abnormal()
{
    CmlBlockCalculation BlockData(3000,3000,200,200);
    BlockData.CalBlockCol( 1000, 512 );
    BlockData.CalBlockRow( 2000, 512 );
    Pt2i ptOrg;
    UINT nW,nH;
    bool result = BlockData.GetBlockPos( 7, 7, ptOrg, nW, nH );
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestCalBlock_abnormal1
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试CalBlock函数输入参数为零的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestCalBlock_abnormal1()
{
    UINT nBlockL;
    UINT nCount;
    UINT nLastBlockL;
    CmlBlockCalculation cls;
    bool result = cls.CalBlock(0,0,0,0,nBlockL,nCount,nLastBlockL);
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestCalBlock_abnormal2
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试CalBlock函数输入参数超限的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestCalBlock_abnormal2()
{
    UINT nBlockL;
    UINT nCount;
    UINT nLastBlockL;
    CmlBlockCalculation cls;
    bool result = cls.CalBlock(100,200,100,500,nBlockL,nCount,nLastBlockL);
    CPPUNIT_ASSERT(result == false);
}
/**
* @fn TestGetBlock_abnormal
* @date 2011.12.1
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 测试GetBlock函数输入参数超限的异常情况
* @version 1.0
* @return 无返回值
* @par 修改历史
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
void CTestBlockCalculation::TestGetBlock_abnormal()
{
    CmlBlockCalculation cls;
    SINT result = cls.GetBlock(7,2000,4,200);
    CPPUNIT_ASSERT(result == -1);
}

