/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file TestRasterMosaic.h
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief DEM影像拼接测试类源文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#include "TestRasterMosaic.h"
#include "../mlRVML/mlRasterMosaic.h"



CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestRasterMosaic,"alltest" );

CTestRasterMosaic::CTestRasterMosaic()
{
    //ctor
}

CTestRasterMosaic::~CTestRasterMosaic()
{
    //dtor
}


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
void CTestRasterMosaic::TestDEMMosaic_ok()
{
    char *cProjectFile = "../../../UnitTestData/TestRasterMosaic/TestDemMosaic/proj.dat";

    char *cOutputFile = "../../../UnitTestData/TestRasterMosaic/TestDemMosaic/DemMosaic.tif";

    vector<string> vecDem;
    string sGetLine;


    ifstream ifProjFile(cProjectFile);
    if(ifProjFile.good())
    {
        while(!ifProjFile.eof())
        {
            getline(ifProjFile, sGetLine);
            char* cPch;
            char* cLineParam[10];

            char* cTemp = const_cast<char*>(sGetLine.c_str());
            if(sGetLine[0] == 'd')
            {
                int nCount = 1;
                cPch = strtok (cTemp," ");
                cLineParam[0] = cPch;
                while(cPch != NULL)
                {
                    cPch = strtok (NULL," ");
                    cLineParam[nCount] = cPch;
                    nCount++;
                }
                vecDem.push_back(cLineParam[2]);

            }

            else
            {
                continue;
            }
        }
    }

    DOUBLE dXResolution = 0.05;
    DOUBLE dYResolution = 0.05;
    CmlRasterMosaic clsRasterMosaic;
    int result =clsRasterMosaic.mlDEMMosaic(vecDem, cOutputFile, dXResolution, dYResolution, 2, 0);
    CPPUNIT_ASSERT(result == 0);

}


/** @brief TearDown
  *
  * @todo: document this function
  */
void CTestRasterMosaic::tearDown()
{

}

/** @brief SetUp
  *
  * @todo: document this function
  */
void CTestRasterMosaic::setUp()
{

}

