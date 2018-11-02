#include "TestRvml.h"
#include "../../include/mlRVML.h"
#include "camCalibIO.h"
#include "PtFilesRW.h"
#include "Sitemapproj.h"
#include "SatMapProj.h"
#include "../mlRVML/mlCamCalib.h"
#include "../mlRVML/mlWBaseProc.h"
#include "../mlRVML/mlSiteMapping.h"
#include "../mlRVML/mlCoordTrans.h"
#include "../mlRVML/mlRasterMosaic.h"
#include "../mlRVML/mlPhgProc.h"
#include "../mlRVML/mlDemAnalyse.h"
#include "../mlRVML/mlLocalization.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestRvml,"alltest" );

CTestRvml::CTestRvml()
{
    //ctor
}

CTestRvml::~CTestRvml()
{
    //dtor
}


/**
* @fn TestSingleCamCalib_ok()
* @date 2011.12.1
* @author
* @brief
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestCameraCalibration_ok1()
{
    SCHAR* srcPath = "../../../ComTestData/TestCamCalib/IFLI_SINGLELEFTCAMPOINTS.txt";
    SCHAR* destPath1 = "../../../ComTestData/TestCamCalib/CamInfo.txt";
    SCHAR* destPath2 = "../../../ComTestData/TestCamCalib/Accuracy.txt";

    string strInputPath(srcPath) ;
    string strCamInfoFile(destPath1) ;
    string strCamAccuracyFile(destPath2);

    CCamCalibIO camIO ;
    if(camIO.readCamSignPts(strInputPath))
    {
        CmlCamCalib camCalib ;
        camCalib.mlSingleCamCalib(camIO.vecLImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH , camIO.inLCamPara , camIO.exLCamPara , camIO.vecErrorPts);
    }
    bool result_CamFile = camIO.writeCamInfoFile(strCamInfoFile.c_str()) ;
    bool result_AccFile = camIO.writeAccuracyFile(strCamAccuracyFile.c_str()) ;
    bool result = (result_CamFile && result_AccFile);
    CPPUNIT_ASSERT(result == TRUE);

}

/**
* @fn TestStereoCamCalib_ok()
* @date 2012.3.18
* @author
* @brief 测试双相机标定函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestCameraCalibration_ok2()
{
    SCHAR* srcPath = "../../../ComTestData/TestCamCalib/IFLI_STEREOCAMPOINTS.txt";
    SCHAR* destPath1 = "../../../ComTestData/TestCamCalib/CamInfo.txt";
    SCHAR* destPath2 = "../../../ComTestData/TestCamCalib/Accuracy.txt";

    string strInputPath(srcPath) ;
    string strCamInfoFile(destPath1) ;
    string strCamAccuracyFile(destPath2);

    CCamCalibIO camIO ;
    if(camIO.readCamSignPts(strInputPath))
    {
        CmlCamCalib camCalib ;
        camCalib.mlStereoCamCalib(camIO.vecLImg2DPts , camIO.vecRImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH ,
                                  camIO.inLCamPara , camIO.inRCamPara , camIO.exLCamPara , camIO.exRCamPara , camIO.exStereoPara ,camIO.vecErrorPts);
    }
    bool result_CamFile = camIO.writeCamInfoFile(strCamInfoFile.c_str()) ;
    bool result_AccFile = camIO.writeAccuracyFile(strCamAccuracyFile.c_str()) ;
    bool result = (result_CamFile && result_AccFile);
    CPPUNIT_ASSERT(result == TRUE);

}

/**
* @fn TestMonoSurvey_ok()
* @date 2012.3.18
* @author
* @brief 测试单目量测函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestMonoSurvey_ok()
{
    char * FeaPath  = "../../../ComTestData/TestMonoSurvey/FEATURE.txt";
    char * ElePath  = "../../../ComTestData/TestMonoSurvey/Interior.txt";
    char * dstPath = "../../../ComTestData/TestMonoSurvey/result.txt";
    ifstream stmele(ElePath);
    CCamCalibIO camIO ;
    // 读取标志点信息文件
    bool result ;
    string strInputPath(FeaPath) ;
    if(camIO.readCamSignPts(strInputPath))
    {
        if(stmele.is_open())
        {
            stmele >> camIO.inLCamPara.f >> camIO.inLCamPara.x >> camIO.inLCamPara.y;
        }
        stmele.close();
        CmlCamCalib clsCamCalib;
        result = clsCamCalib.backProjection(camIO.vecLImg2DPts, camIO.vecObj3DPts, camIO.inLCamPara , camIO.exLCamPara);

    }

    ofstream monoresult(dstPath);
    if(monoresult.is_open())
    {
        monoresult << camIO.exLCamPara.pos.X <<" "<<camIO.exLCamPara.pos.Y<<" "<<camIO.exLCamPara.pos.Z<<" "<<camIO.exLCamPara.ori.omg<<" "<<camIO.exLCamPara.ori.phi<<" "<<camIO.exLCamPara.ori.kap;
    }
    monoresult.close();
    CPPUNIT_ASSERT(result == TRUE);

}

/**
* @fn TestWideBaseAnalysis_ok()
* @date 2012.3.18
* @author
* @brief 测试最优基线计算函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestWideBaseAnalysis_ok()
{
    char * srcPath = "../../../ComTestData/TestWBaseProc/CameraPara.txt";
    char * dstPath = "../../../ComTestData/TestWBaseProc/OptiBase.txt";

    ifstream stm(srcPath);
    FILE *pIOFile;
    InOriPara mlNav;
    InOriPara mlPan;
    BaseOptions AnaPara;
    DOUBLE dBestBase;
    if((pIOFile = fopen(srcPath,"r")))
    {


        stm >> mlNav.f >> mlNav.x >> mlNav.y >> mlNav.k1 >> mlNav.k2 >> mlNav.k3 >> mlNav.p1 >> mlNav.p2 >> mlNav.alpha >> mlNav.beta ;
        stm >> mlPan.f >> mlPan.x >> mlPan.y >> mlPan.k1 >> mlPan.k2 >> mlPan.k3 >> mlNav.p1 >> mlPan.p2 >> mlPan.alpha >> mlPan.beta ;
        stm >> AnaPara.dFixBase >> AnaPara.dPixel >> AnaPara.dTarget >> AnaPara.nWidth >> AnaPara.dThresHold >> AnaPara.nIterTime;
        stm.close();
    }

    bool result;
    result = mlWideBaseAnalysis(mlNav, mlPan, AnaPara,dBestBase);

    ofstream fbase(dstPath);
    fbase << dBestBase;
    fbase.close();
    CPPUNIT_ASSERT(result == TRUE);
}

/**
* @fn TestWideBaseMapping_ok()
* @date 2012.3.18
* @author
* @brief 测试长基线测图函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestWideBaseMapping_ok()
{
    WideOptions WidePara;
    WidePara.bIsUsingFeatPt = false;
    WidePara.bIsUsingPartion = true;

    char *srcPath="../../../ComTestData/TestWBaseProc/WideMars/SiteMapping.smp";
    char *dstPath="../../../ComTestData/TestWBaseProc/WideMars/WideDEM.tif";
    CmlSiteMapProj site;
    string strProjPath;
    vector<StereoSet> vecStereoImg;
    //读入工程文件
    if( false == site.LoadProj( srcPath ))
    {
        return;
    }
    int nSiteID, nRollID, nImgID;

    site.GetProjPath(strProjPath);
    site.GetImgSet(vecStereoImg);

    vector<StereoSet> vecStereoSet;
    //读入工程文件中的像对信息
    site.GetDealSet( 1, 1, -1, vecStereoSet);
    int nNum = vecStereoSet.size();
    vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum), vecDPtL(nNum), vecDPtR(nNum);

    bool result = site.CreateWideDem(vecStereoSet, WidePara, vecFPtL, vecFPtR, vecDPtL, vecDPtR, dstPath);
    CPPUNIT_ASSERT(result == TRUE);

}

/**
* @fn TestDenseMatch_ok()
* @date 2012.3.18
* @author
* @brief 测试密集匹配函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestDenseMatch_ok()
{
    WideOptions WidePara;
    WidePara.nStep = 3;
    WidePara.nRadiusX = 10;
    WidePara.nRadiusY = 5;
    WidePara.nTemplateSize = 13;
    WidePara.dCoef = 0.75;

    char * srcPath = "../../../ComTestData/TestSiteMapping/Mars/SiteMapping.smp";
    CmlSiteMapProj site;
    string strProjPath;
    vector<StereoSet> vecStereoImg;

    site.GetProjPath(strProjPath);
    site.GetImgSet(vecStereoImg);

    if( false == site.LoadProj( srcPath ))
    {
        return;
    }
    int nSiteID, nRollID, nImgID;
    vector<StereoSet> vecStereoSet;


    site.GetDealSet( 1, 1, 10, vecStereoSet);
    int nNum = vecStereoSet.size();
    vector<ImgPtSet> vecDPtL(nNum), vecDPtR(nNum);

    bool result;

    if(true)
    {
        result =  site.CreateDmf(vecStereoSet, WidePara, vecDPtL, vecDPtR);
    }

    CPPUNIT_ASSERT(result == TRUE);


}

/**
* @fn TestDisparityMap_ok()
* @date 2012.3.18
* @author
* @brief 测试视差图生成函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestDisparityMap_ok()
{


}

/**
* @fn TestCoordTransLat_ok()
* @date 2012.3.18
* @author
* @brief 测试坐标转换函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestCoordTransLat_ok()
{
    DOUBLE dLat = 45;
    DOUBLE dLong = 60;
    CmlMat transMat;
    DOUBLE transVec[3];
    CmlCoordTrans cls;
    bool result = cls.mlCalcTransMatrixByLatLong(dLat,dLong,&transMat,transVec);
    CPPUNIT_ASSERT(result == true);

}

void CTestRvml::TestCoordTransXYZ_ok()
{
    CmlCoordTrans cls;
    Pt3d LocResult;
    LocResult.X = 1737.4;
    LocResult.Y = 0;
    LocResult.Z = 0;
    CmlMat transMat;
    DOUBLE transVec[3];

    bool result = cls.mlCalcTransMatrixByXYZ(&LocResult,&transMat,transVec);
    CPPUNIT_ASSERT(result == true);

}


/**
* @fn TestSatMapping_ok()
* @date 2012.3.18
* @author
* @brief 测试嫦娥卫星影像测图函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestSatMapping_ok()
{
    DOUBLE dRes = 150;
    bool bBaseLeft = true;
    bool bUsePts = false;
    CmlSatMapProj mapproj;

    char * srcPath = "../../../ComTestData/TestSatMappingData/CE-1/CE1Proj.txt";
    char * demPath = "../../../ComTestData/TestSatMappingData/CE-1/result/CE1dem.tif";
    char * domPath = "../../../ComTestData/TestSatMappingData/CE-1/result/CE1dem.tif";
    bool result = mapproj.SatMapping( srcPath, demPath, domPath, dRes, bBaseLeft,bUsePts );

    CPPUNIT_ASSERT(result == true);


}

/**
* @fn TestMapByInteBlock_ok()
* @date 2012.3.18
* @author
* @brief 测试单站点测图函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestMapByInteBlock_ok()
{
    SCHAR* srcPath = "../../../ComTestData/TestSiteMapping/Mars/SiteMapping.smp";
    SCHAR* demPath = "../../../ComTestData/TestSiteMapping/Mars/SiteDEM.tif";
    SCHAR* domPath = "../../../ComTestData/TestSiteMapping/Mars/SiteDOM.tif";

    double dRange = 10;
    double dRes = 0.05;

    string strInputPath(srcPath);
    string strDemFile(demPath);
    string strDomFile(domPath);

    CmlSiteMapProj site;
    Pt3d ptCenter;
    site.LoadProj( strInputPath );
    site.GetSiteCenter( ptCenter);

    double dLTx = ptCenter.X - dRange ;
    double dLTy = ptCenter.Y + dRange ;
    double dRTx = ptCenter.X + dRange ;
    double dRTy = ptCenter.Y - dRange ;
    bool result  = site.CreateDemAndDom( dLTx, dLTy, dRTx, dRTy, dRes, 1, 1, -1, true, false, strDemFile.c_str(), strDomFile.c_str());
    CPPUNIT_ASSERT(result == true);

}



/**
* @fn TestPanoMosic_ok()
* @date 2012.3.18
* @author
* @brief 测试全景拼接函数,待改正
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestPanoMosic_ok()
{
    char *cProjectFile = "../../../ComTestData/TestPanoMosaic/site2/project.smp";
    char *cOutputFile = "../../../ComTestData/TestPanoMosaic/site2/PanoImg.tif";

    CmlSiteMapProj clsSMapProj;
    clsSMapProj.LoadProj(cProjectFile);

    vector<char*> cParamList;

    cParamList.push_back("opencv_stitching");
    cParamList.push_back("--warp");
    cParamList.push_back("cylindrical");
    cParamList.push_back("--match_conf");
    cParamList.push_back("0.65");
    bool result = clsSMapProj.Mosaic( cParamList, cOutputFile );
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestTinSimply_ok()
* @date 2012.3.18
* @author
* @brief 测试三角网简化函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestTinSimply_ok()
{
    char* srcPath = "../../../ComTestData/TestTinSimply/TinSimply.txt";
    char* dstPath = "../../../ComTestData/TestTinSimply/result.txt";
    double simpleIndex = 0.5;

    vector<Pt3d> vecin;
    vector<Pt3d> vecout;
    Pt3d pt3d;
    ifstream infile(srcPath);
    ofstream outfile(dstPath);
    while(!infile.eof())
    {
        infile >> pt3d.X >> pt3d.Y >> pt3d.Z;
        vecin.push_back(pt3d);

    }
    infile.close();


    bool result = mlTinSimply(vecin,vecout,simpleIndex);

    for(int i=0; i<vecout.size(); i++)
    {
        outfile << vecout.at(i).X << ' ' << vecout.at(i).Y << ' '<< vecout.at(i).Z << '\n';

    }
    outfile.close();

    CPPUNIT_ASSERT(result == true);

}



/**
* @fn TestVisualImage_ok()
* @date 2012.3.18
* @author
* @brief 测试仿真图像生成函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestVisualImage_ok()
{
    char* srcParaPath = "../../../ComTestData/TestVisualImg/para.txt";
    char* srcDemPath = "../../../ComTestData/TestVisualImg/DemReverse.tif";
    char* srcDomPath = "../../../ComTestData/TestVisualImg/DomReverse.tif";
    char* srcImgPath = "../../../ComTestData/TestVisualImg/visual.tif";
    ifstream infile(srcParaPath);
    ExOriPara exori;
    double fx,fy;

    infile >> exori.ori.omg >> exori.ori.phi >> exori.ori.kap >> exori.pos.X >> exori.pos.Y
    >> exori.pos.Z >> fx >> fy;

    bool result = mlVisualImage(srcDemPath,srcDomPath,srcImgPath,exori,fx,fy,1024,1024);

    CPPUNIT_ASSERT(result == true);
}

/**
* @fn TestPano2Prespective_ok()
* @date 2012.3.18
* @author
* @brief 测试全景图像生成透视图像函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestPano2Prespective_ok()
{
    char *srcFile = "../../../ComTestData/TestPerImg/pano.tif";
    char *dstFile = "../../../ComTestData/TestPerImg/subimg.tif";
    SINT nLTX= 500;
    SINT nLTY = 500;
    SINT nWidth = 1000;
    SINT nHeight = 1000;
    DOUBLE dFocus = 900;


    Pt2i ptOrigin;
    ptOrigin.X = nLTX;
    ptOrigin.Y = nLTY;

    bool result = mlPano2Prespective(srcFile, dstFile, ptOrigin.X, ptOrigin.Y, nWidth, nHeight, dFocus);

    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestDemMosaic_ok()
* @date 2012.3.18
* @author
* @brief 测试DEM拼接函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestDemMosaic_ok()
{
    char *cProjectFile = "../../../ComTestData/TestDemMosaic/proj.dat";
    char *cOutputFile = "../../../ComTestData/TestDemMosaic/out.tif";

    double dXResl = 0.05;
    double dYResl = 0.05;

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

    string strProjPath( cProjectFile );
    int nPos = strProjPath.rfind( "/"  );
    string strMainProj;
    strMainProj.assign( strProjPath, 0, nPos );
    strMainProj.append( "/");

    for( int i = 0; i < vecDem.size(); ++i )
    {
        string strCurMain = strMainProj;
        string strCurPath = vecDem[i];
        string *pCur = &vecDem.at(i);
        strCurMain.append( strCurPath );
        *pCur = strCurMain;
    }

    bool result =  mlDEMMosaic(vecDem, cOutputFile, dXResl, dYResl, 2, 0);
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestComputeInsightMap_ok()
* @date 2012.3.18
* @author
* @brief 测试通视图计算函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestComputeInsightMap_ok()
{
    char* srcfilename = "../../../ComTestData/TestInsightMap/SiteDem.tif";
    char* dstfilename = "../../../ComTestData/TestInsightMap/Insight.tif";
    int x = 0;
    int y = 0;
    double h = 2;
    CmlDemAnalyse cda;
    int result = cda.ComputeViewShedInterface(srcfilename,x,y,h,dstfilename,true);
    CPPUNIT_ASSERT(result == 1);

}

/**
* @fn TestComputeSlopeAspectMap_ok()
* @date 2012.3.18
* @author
* @brief 测试坡向图生成函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestComputeSlopeAspectMap_ok()
{
    char* srcfilename = "../../../ComTestData/TestInsightMap/SiteDem.tif";
    char* dstfilename = "../../../ComTestData/TestInsightMap/Slope.tif";
    int nWindowSize = 3;
    double dzfactor = 1;
    CmlDemAnalyse cda;
    int result = cda.ComputeSlopeInterface(srcfilename,dstfilename,nWindowSize,dzfactor);
    CPPUNIT_ASSERT(result == 1);

}

/**
* @fn TestComputeBarrierMap_ok()
* @date 2012.3.18
* @author
* @brief 测试障碍图生成函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestComputeBarrierMap_ok()
{
    char* srcfilename = "../../../ComTestData/TestInsightMap/SiteDem.tif";
    char* dstfilename = "../../../ComTestData/TestInsightMap/Obstacle.tif";
    int nWindowSize = 3;
    double dZfactor = 1;
    CmlDemAnalyse cda;
    ObstacleMapPara obpara;
    obpara.dMaxObstacleValue = 2;
    obpara.dMaxRoughness = 0.1;
    obpara.dMaxSlope = 25;
    obpara.dMaxStep = 0.1;
    obpara.dRoughnessCoef = 100;
    obpara.dSlopeCoef = 100;
    obpara.dStepCoef = 100;
    int result = cda.ComputeObstacleMapInterface(srcfilename,dstfilename,nWindowSize,dZfactor,obpara);
    CPPUNIT_ASSERT(result == 1);

}

/**
* @fn TestComputeContourMap_ok()
* @date 2012.3.18
* @author
* @brief 测试等高线图生成函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestComputeContourMap_ok()
{
    char* srcfilename = "../../../ComTestData/TestInsightMap/SiteDem.tif";
    char* dstfilename = "../../../ComTestData/TestInsightMap/Contour.tif";
    int nWindowSize = 3;
    double dzfactor = 1;
    double dhinterval = 0.4;
    CmlDemAnalyse cda;
    int result = cda.ComputeContourInterface(dhinterval,srcfilename,dstfilename);
    CPPUNIT_ASSERT(result == 1);

}

/**
* @fn TestGeoRasterCut_GeoCoo_ok()
* @date 2012.3.18
* @author
* @brief 测试DEM、DOM裁切函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestGeoRasterCut_GeoCoo_ok()
{
    char* srcfile= "../../../ComTestData/TestDemProcess/DOMReverse.tif";
    char* dstfile = "../../../ComTestData/TestDemProcess/DOMReverse_cut_Geo.tif";
    Pt2d pttl,ptbr;
    pttl.X = 153.54;
    pttl.Y = 174.5;
    ptbr.X = 168.94;
    ptbr.Y = 165.62;

    CmlPhgProc phg;
    bool result = phg.mlGeoRasterCut(srcfile,dstfile,pttl,ptbr,2,-1,1);
    CPPUNIT_ASSERT(result == true);


}

/**
* @fn TestGeoRasterCut_Pixel_ok()
* @date 2012.3.18
* @author
* @brief 测试DEM、DOM裁切函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestGeoRasterCut_Pixel_ok()
{
    char* srcfile = "../../../ComTestData/TestDemProcess/DOMReverse.tif";
    char* dstfile = "../../../ComTestData/TestDemProcess/DOMReverse_cut_pixel.tif";
    Pt2d pttl,ptbr;
    pttl.X = 10;
    pttl.Y = 10;
    ptbr.X = 100;
    ptbr.Y = 100;

    CmlPhgProc phg;
    bool result = phg.mlGeoRasterCut(srcfile,dstfile,pttl,ptbr,1,-1,1);
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestLocalByMatch_ok()
* @date 2012.3.18
* @author
* @brief 测试卫星影像和地面影像间匹配实现定位函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestLocalByMatch_ok()
{
    char* cLandDom = "../../../ComTestData/TestLocalByMatch/LandDom.tif";
    char* cSatDom = "../../../ComTestData/TestLocalByMatch/SatDom.tif";
    char* cOutPath = "../../../ComTestData/TestLocalByMatch/LocaResult.lrf";

    Pt3d ptLocalRes;
    DOUBLE dLocalAccuracy = 0;

    ImgPtSet satPtSet;
    ImgPtSet LandPtSet;

    LocalByMatchOpts stuLocalByMOpts;


    CmlLocalization clsLocal;
    bool result1 = clsLocal.LocalIn2Dom( cLandDom, cSatDom, LandPtSet, satPtSet, stuLocalByMOpts, ptLocalRes, dLocalAccuracy );

    string strSatDom(cSatDom);
    string strLandDom(cLandDom);
    int nSatPos = strSatDom.rfind( "." );
    int nLandPos = strLandDom.rfind( "." );
    CPtFilesRW clsPtRW;
    if( ( nSatPos > 0 )&&( nLandPos > 0 ) )
    {
        string strTempSat, strTempLand;
        strTempLand.assign( strLandDom, 0,  nLandPos );
        strTempSat.assign( strSatDom, 0,  nSatPos );
        strTempLand.append( ".fmf" );
        strTempSat.append( ".fmf" );
        clsPtRW.SaveImgPtSet( strTempLand, LandPtSet );
        clsPtRW.SaveImgPtSet( strTempSat, satPtSet );
    }

    bool result2 =  clsPtRW.SaveLocalRes( cOutPath, ptLocalRes, dLocalAccuracy );

    bool result = (result1 &&result2);
    CPPUNIT_ASSERT(result == true);
}

/**
* @fn TestLocalByIntersection_ok()
* @date 2012.3.18
* @author
* @brief 测试后方交会定位函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestLocalByIntersection_ok()
{
    char* cProjFile = "../../../ComTestData/TestLocalByInterSec/Mars1/Local.smp";
    char* cGCPFile = "../../../ComTestData/TestLocalByInterSec/test.gcp";
    char* cOutFile = "../../../ComTestData/TestLocalByInterSec/res.lrf";

    CmlSiteMapProj siteProj;
    string strProjPath;
    vector<StereoSet> vecStereoImg;

    bool result;
    if( true == siteProj.LoadProj( cProjFile ) )
    {
        result = siteProj.LocalByBundleResection( cGCPFile, cOutFile );
    }

    CPPUNIT_ASSERT(result == true);
}


/**
* @fn TestLocalIn2Site_ok()
* @date 2012.3.18
* @author
* @brief 测试站点间定位函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestLocalIn2Site_ok()
{
    char* cProjFile = "../../../ComTestData/TestLocalInTwoSite/Mars1/Local.smp";
    char* cOutFile = "../../../ComTestData/TestLocalInTwoSite/res.lrf";

    SINT nFrontSiteNum = 1;
    SINT nBackSiteNum = 2;
    CmlSiteMapProj* pSiteProj = new CmlSiteMapProj;
    bool result;
    if( true == pSiteProj->LoadProj( cProjFile ) )
    {
        result = pSiteProj->LocalInTwoSite( nFrontSiteNum, nBackSiteNum, cOutFile );

    }
    CPPUNIT_ASSERT(result == true);
    delete pSiteProj;

}

/**
* @fn TestLocalBySeqence_ok()
* @date 2012.3.18
* @author
* @brief 测试序列影像定位函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml:: TestLocalBySeqence_ok()
{
    char* cProjPath = "../../../ComTestData/TestLocalBySequence/Lunar/Local.smp";
    char* cSatPath = "../../../ComTestData/TestLocalBySequence/Lunar/satDom.smp";
    char* cOutPath = "../../../ComTestData/TestLocalBySequence/Lunar/local.lrf";

    CmlSiteMapProj clsSiteProj;
    bool result;
    if( true == clsSiteProj.LoadProj( cProjPath ) )
    {
        result = clsSiteProj.LocalBySeqImg( cSatPath, cOutPath );
    }
    CPPUNIT_ASSERT(result == true);

}

/**
* @fn TestLocalByLander_ok()
* @date 2012.3.18
* @author
* @brief 测试着陆器定位函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
void CTestRvml::TestLocalByLander_ok()
{

}

/** @brief TearDown
  *
  * @todo: document this function
  */
void CTestRvml::tearDown()
{

}

/** @brief SetUp
  *
  * @todo: document this function
  */
void CTestRvml::setUp()
{

}

