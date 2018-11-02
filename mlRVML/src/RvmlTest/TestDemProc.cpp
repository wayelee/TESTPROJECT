#include "TestDemProc.h"
#include "../mlRVML/mlBase.h"
#include "../mlRVML/mlTIN.h"
#include "../mlRVML/mlPhgProc.h"
#include "../mlRVML/mlDemProc.h"
#include "../mlRVML/mlKringing.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestDemProc,"alltest" );

CTestDemProc::CTestDemProc()
{
    //ctor
}

CTestDemProc::~CTestDemProc()
{
    //dtor
}

/** @brief tearDown
  *
  * @todo: document this function
  */
void CTestDemProc::tearDown()
{

}

/** @brief setUp
  *
  * @todo: document this function
  */
void CTestDemProc::setUp()
{

}

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
void CTestDemProc::TestDemProc_OK_ByPix()
{
    //bool CmlPhgProc::mlGeoRasterCut(char* strFileIn,char* strFileOut,Pt2d pttl,Pt2d ptbr,int nflag,int nCutBands=-1,double dZoom=1)
    char* strFileIn = "../../../UnitTestData/TestDemProc/DOMReverse.tif";
    char* strFileOut = "../../../UnitTestData/TestDemProc/DOMReverse_Cut.tif";
    Pt2d pttl,ptbr;
    pttl.X = 10;
    pttl.Y = 10;
    ptbr.X = 100;
    ptbr.Y = 100;

    CmlPhgProc phg;
    bool result = phg.mlGeoRasterCut(strFileIn,strFileOut,pttl,ptbr,1,-1,1);
    CPPUNIT_ASSERT(result == true);

    return;
}

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

void CTestDemProc::TestDemProc_OK_ByGeoPos()
{
    char* strFileIn = "../../../UnitTestData/TestDemProc/DOMReverse.tif";
    char* strFileOut = "../../../UnitTestData/TestDemProc/DOMReverse_Cut.tif";
    Pt2d pttl,ptbr;
    pttl.X = 153.54;
    pttl.Y = 174.5;
    ptbr.X = 168.94;
    ptbr.Y = 165.62;

    CmlPhgProc phg;
    bool result = phg.mlGeoRasterCut(strFileIn,strFileOut,pttl,ptbr,2,-1,1);
    CPPUNIT_ASSERT(result == true);
    return;

}

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

void CTestDemProc::TestImgReprj_OK()
{
    //bool CmlPhgProc::mlImageReprj(char* strDem,char* strDom,char* outImg,ExOriPara exori,double fx,double fy,int nImgWid,int nImgHei)
    char* strDem = "../../../UnitTestData/TestDemProc/DemReverse.tif";
    char* strDom = "../../../UnitTestData/TestDemProc/DOMReverse.tif";
    char* outImg = "../../../UnitTestData/TestDemProc/reprjImg.tif";
    ExOriPara exori;
    exori.ori.omg =  -1.888407414;
    exori.ori.phi = 0.4303669265;
    exori.ori.kap =  0.0885459891;
    exori.pos.X = 161.30174;
    exori.pos.Y = 168.962353;
    exori.pos.Z = -1.356028;
    double fx,fy;
    fx = fy = 1226.63;
    CmlPhgProc phg;
    bool result;
    result = phg.mlImageReprj(strDem,strDom,outImg,exori,fx,fy,1024,1024);
    CPPUNIT_ASSERT(result == true);

}


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
void CTestDemProc::TestmlTinSimply_OK()
{

    CmlPhgProc phg;
    char* strFileIn = "../../../UnitTestData/TestDemProc/TinSimply.txt";
    ifstream infile(strFileIn);
    vector<Pt3d> vecpt;
    Pt3d pt3d;
    while(!infile.eof())
    {
       infile >> pt3d.X >> pt3d.Y >> pt3d.Z ;
        //infile >> pt3d.X;
        vecpt.push_back(pt3d);
    }

    vector<Pt3d> vecptout;
    double simpleIndex = 0.5;
    bool result = phg.mlTinSimply(vecpt,vecptout,simpleIndex);
    CPPUNIT_ASSERT(result == true);

}

//bool CmlPhgProc::mlBackForwardinterSection( Pt2d *pImgPt,Pt3d *pGroundPt, DOUBLE fx,DOUBLE fy, SINT nPtNum,ExOriPara *pInitExOripara, ExOriPara *pExOripara )
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
void CTestDemProc::TestBackfordinterSection_OK()
{
    char* strFileIn = "../../../UnitTestData/TestDemProc/pt.txt";
    char* strFileExori = "../../../UnitTestData/TestDemProc/initialExori.txt";
    double fx,fy;
    fx = fy = 1226.63;
    SINT nPtNum = 9;
    Pt2d ptImg[nPtNum];
    Pt3d ptGround[nPtNum];
    ifstream filept(strFileIn);
    ifstream fileExori(strFileExori);
    ExOriPara exoriInit;
    ExOriPara exori;
    for(int i=0; i<nPtNum; i++)
    {
        filept >> ptImg[i].X >> ptImg[i].Y >> ptGround[i].X >> ptGround[i].Y >> ptGround[i].Z;
        fileExori >> exoriInit.ori.omg >> exoriInit.ori.phi>>exoriInit.ori.kap>>exoriInit.pos.X>>exoriInit.pos.Y>>exoriInit.pos.Z;

    }

    exoriInit.ori.phi = Deg2Rad(exoriInit.ori.phi);
    exoriInit.ori.omg = Deg2Rad(exoriInit.ori.omg);
    exoriInit.ori.kap = Deg2Rad(exoriInit.ori.kap);

    CmlPhgProc phg;
    bool result = phg.mlBackForwardinterSection(ptImg,ptGround,fx,fy,nPtNum,&exoriInit,&exori);
    CPPUNIT_ASSERT(result == true);

}



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
void CTestDemProc::TestBackfordinterSection_abnormal()  //已知点数小于3
{
    char* strFileIn = "../../../UnitTestData/TestDemProc/pt.txt";
    char* strFileExori = "../../../UnitTestData/TestDemProc/initialExori.txt";
    double fx,fy;
    fx = fy = 1226.63;
    SINT nPtNum = 2;
    Pt2d ptImg[nPtNum];
    Pt3d ptGround[nPtNum];
    ifstream filept(strFileIn);
    ifstream fileExori(strFileExori);
    ExOriPara exoriInit;
    ExOriPara exori;
    for(int i=0; i<nPtNum; i++)
    {
        filept >> ptImg[i].X >> ptImg[i].Y >> ptGround[i].X >> ptGround[i].Y >> ptGround[i].Z;
        fileExori >> exoriInit.ori.omg >> exoriInit.ori.phi>>exoriInit.ori.kap>>exoriInit.pos.X>>exoriInit.pos.Y>>exoriInit.pos.Z;

    }

    exoriInit.ori.phi = Deg2Rad(exoriInit.ori.phi);
    exoriInit.ori.omg = Deg2Rad(exoriInit.ori.omg);
    exoriInit.ori.kap = Deg2Rad(exoriInit.ori.kap);

    CmlPhgProc phg;
    bool result = phg.mlBackForwardinterSection(ptImg,ptGround,fx,fy,nPtNum,&exoriInit,&exori);
    CPPUNIT_ASSERT(result == false);


}

/**
* @fn TestmlReproject_OK()
* @date 2011.12.1
* @author 张重阳
* @brief 测试根据共线函数反投函数功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestDemProc::TestmlReproject_OK()
{
    Pt2d imgPt;
    Pt3d grdPt;
    grdPt.X = 125.5;
    grdPt.Y = 256.3;
    grdPt.Z = 1.2;
    ExOriPara exori;
    exori.ori.omg = 2.734665703;
    exori.ori.phi = 1.229205691;
    exori.ori.kap = 1.933945632;
    exori.pos.X = 161.351759;
    exori.pos.Y = 169.062072;
    exori.pos.Z = -1.35644;
    double fx,fy;
    fx = fy = 1226.63;
    CmlPhgProc phg;
    bool result = phg.mlReproject(&imgPt,&grdPt,&exori,fx,fy);
    CPPUNIT_ASSERT(result == true);



}

void CTestDemProc::TestmlReproject_OK2()
{
    Pt2d imgPt;
    Pt3d grdPt;
    grdPt.X = 125.5;
    grdPt.Y = 256.3;
    grdPt.Z = 1.2;
    ExOriPara exori;
    exori.ori.omg = 2.734665703;
    exori.ori.phi = 1.229205691;
    exori.ori.kap = 1.933945632;
    exori.pos.X = 161.351759;
    exori.pos.Y = 169.062072;
    exori.pos.Z = -1.35644;
    double fx,fy;
    fx = fy = 1226.63;
    CmlMat matR;
    OPK2RMat(&exori.ori,&matR);
    CmlPhgProc phg;
    bool result = phg.mlReproject(&imgPt,&grdPt,&exori,fx,fy,&matR);
    CPPUNIT_ASSERT(result == true);


}



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
void CTestDemProc::TestBuild2By2DPt_OK()
//bool Build2By2DPt( DOUBLE* ptList, SLONG lPtNum); /
{
    CmlTIN tin;
    ifstream infile("../../../UnitTestData/TestDemProc/buildtin2d.txt");
    SLONG lPtnum;
    double* ptList = new double[lPtnum*2];
    for (int i=0; i<lPtnum*2; i++)
    {
        infile >> ptList[i];
    }

    bool result =  tin.Build2By2DPt(ptList,lPtnum);
    CPPUNIT_ASSERT(result == true);

}

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
void CTestDemProc::TestBuild2By3DPt_OK()
{
    CmlTIN tin;
    ifstream infile("../../../UnitTestData/TestDemProc/buildtin3d.txt");
    SLONG lPtnum;
    double* ptList = new double[lPtnum*3];
    for (int i=0; i<lPtnum*3; i++)
    {
        infile >> ptList[i];
    }

    bool result =  tin.Build2By3DPt(ptList,lPtnum);
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestBuild2By2DPt_OK()
* @date 2011.12.1
* @author 张重阳
* @brief 测试由vector三维点构TIN功能
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestDemProc::TestBuild2Byvec3dPt_OK()
{
    CmlTIN tin;
    ifstream infile("../../../UnitTestData/TestDemProc/buildtin3d.txt");
    vector<Pt3d> vecpt3;
    for(int i=0; i<vecpt3.size(); i++)
    {
        infile >> vecpt3[i].X >> vecpt3[i].Y >> vecpt3[i].Z;
    }

    bool result = tin.Build2By3DPt(vecpt3);
    CPPUNIT_ASSERT(result == true);
}


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
void CTestDemProc::TestGetGridTriIndex_OK()
{
    CmlTIN tin;
    ifstream infile("../../../UnitTestData/TestDemProc/TinSimply.txt");
    vector<Pt3d> vecpt3;
    Pt3d pt3d;
    while(!infile.eof())
    {
        infile >> pt3d.X >> pt3d.Y>>pt3d.Z;
        vecpt3.push_back(pt3d);

    }
    tin.Build2By3DPt(vecpt3);
    UINT tri_num = tin.GetNumOfTriangles();
    SINT *pIndex = new SINT[tri_num];
    UINT nW = 3;
    UINT nH = 3;
    DbRect rect;
    rect.bottom = 50;
    rect.left = 0;
    rect.top = 0;
    rect.right = 50;
    DOUBLE dRes = 0.05;
    bool result = tin.GetGridTriIndex(pIndex,nW,nH,&rect,dRes);
    CPPUNIT_ASSERT(result == true);


}



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
void CTestDemProc::TestGetCenterTriIndex_OK()
{
    CmlTIN tin;
    ifstream infile("../../../UnitTestData/TestDemProc/buildtin3d.txt");
    vector<Pt3d> vecpt3;
    for(int i=0; i<vecpt3.size(); i++)
    {
        infile >> vecpt3[i].X >> vecpt3[i].Y >> vecpt3[i].Z;
    }
    tin.Build2By3DPt(vecpt3);
    UINT tri_num = tin.GetNumOfTriangles();
    SINT *pIndex = new SINT[tri_num];
    vector<Pt3d> vecAddPts;
    DbRect rect;
    rect.bottom = 50;
    rect.left = 0;
    rect.top = 0;
    rect.right = 50;
    DOUBLE dRes = 0.05;
    bool result = tin.GetCenterTriIndex(pIndex,vecAddPts,&rect,dRes);
    CPPUNIT_ASSERT(result == true);







}


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
void CTestDemProc::TestmlWriteToGeoFile_case1()
{
    CmlDemProc demproc;
    ifstream infile("../../../UnitTestData/TestDemProc/buildtin3d.txt");
    SCHAR* strGeoFilePath = "../../../UnitTestData/TestDemProc/geoDem1.tiff";
    vector<Pt3d> vecpt3;
    DbRect rect;
    rect.bottom = 50;
    rect.left = 0;
    rect.top = 0;
    rect.right = 50;
    DOUBLE dRes = 0.05;
    Pt3d pt3d;
    while(!infile.eof())
    {
        infile >> pt3d.X >> pt3d.Y>>pt3d.Z;
        vecpt3.push_back(pt3d);

    }
//    for(int i=0; i<vecpt3.size(); i++)
//    {
//        infile >> vecpt3[i].X >> vecpt3[i].Y >> vecpt3[i].Z;
//    }
    bool result = demproc.mlWriteToGeoFile(vecpt3,rect,dRes,strGeoFilePath);
    CPPUNIT_ASSERT(result == true);


}
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
void CTestDemProc::TestmlWriteToGeoFile_case2()
{
    CmlDemProc demproc;
    ifstream infile("../../../UnitTestData/TestDemProc/buildtin3d.txt");
    SCHAR* strGeoFilePath = "../../../UnitTestData/TestDemProc/geoDem2.tiff";
    vector<Pt3d> vecpt3;
    DOUBLE dRes = 0.05;
    for(int i=0; i<vecpt3.size(); i++)
    {
        infile >> vecpt3[i].X >> vecpt3[i].Y >> vecpt3[i].Z;
    }
    bool result = demproc.mlWriteToGeoFile(vecpt3,dRes,strGeoFilePath);
    CPPUNIT_ASSERT(result == true);


}

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

void CTestDemProc::TestGetValueFromTin()
{
    CmlKringing krig;
    ifstream infile("../../../UnitTestData/TestDemProc/buildtin3d.txt");
    vector<Pt3d> vecpt3;
    for(int i=0; i<vecpt3.size(); i++)
    {
        infile >> vecpt3[i].X >> vecpt3[i].Y >> vecpt3[i].Z;
    }
    CmlTIN tin;
    tin.Build2By3DPt(vecpt3);
    triangulateio tri = tin.m_tri;
    SINT neigh[4] = {1,2,3,4};
    Pt3d outNewPt;
    KRIGING_State bFlag = KRIGING_ON;
    bool result = krig.GetValueFromTin(vecpt3,&tri,neigh,outNewPt,bFlag);
    CPPUNIT_ASSERT(result == true);

}

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
void CTestDemProc::TestmlInitVariogram()
{
    DOUBLE *dShort = NULL;
    DOUBLE *dLong = NULL;
    DOUBLE dDisShort = -1.0;
    DOUBLE dVertex = -1.0;
    CmlKringing krig;
    krig.InitVariogram(dShort,dLong,dDisShort,dVertex);
    bool result = true;
    CPPUNIT_ASSERT(result == true);



}




