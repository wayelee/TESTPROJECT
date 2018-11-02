#include "SatMapProj.h"
#include <fstream>
#include <algorithm>
#include "../../src/mlRVML/mlSatMapping.h"
#include "../../include/mlTypes.h"
#include "../../include/mlRVML.h"
#include "PtFilesRW.h"

CmlSatMapProj::CmlSatMapProj()
{

}

CmlSatMapProj::CmlSatMapProj( string strPath )
{

}
CmlSatMapProj:: ~CmlSatMapProj()
{

}
bool CmlSatMapProj::LoadProj( SatProj &pro,SatOptions &ops, string projPath, string demPath, string domPath, DOUBLE dRes, bool bBaseFlag )
{

    ops.bLeftBaseFlag = bBaseFlag;
    ops.XResolution = dRes;
    ops.YResolution = dRes;
    pro.sDemPath = demPath;
    pro.sDomPath = domPath;
    fstream stm;
    stm.open( projPath.c_str() );
    SINT nStrPos = projPath.rfind("/");
    string sTempPath;
    sTempPath.assign(projPath, 0, nStrPos+1 );
    sTempPath += "data/";
    bool H1, H2, H3, H4, H5, H6, H7, H8;
    string sFileName,sTempName;
    string strPath;
    vector<DOUBLE> LImgTime,RImgTime;//!<左右影像扫描线时间
    vector<LineEo> LLineEo,RLineEo;//!<左右影像轨道星历
    vector<AngleEo> LAngleEo,RAngleEo;//!<左右影像轨道欧拉角
    if( stm )
    {
        string cHead;
        stm >> cHead;
        stm >> cHead;
        if( cHead == "CE-1" )
        {
            ops.sMissionName = "CE-1";
        }
        else if( cHead == "CE-2" )
        {
            ops.sMissionName = "CE-2";
        }
        else
        {
            printf("Mission is not supported!\n") ;
            return false;
        }
        stm>>strPath;
        if( strPath == "****Required**Fields****" )
        {
            stm>>sFileName;
            while( sFileName != "****Required**Fields**End****" )
            {
                if( sFileName == "LeftImagePath" )
                {
                    stm>>sFileName;
                    pro.sLimgPath = sTempPath + sFileName;
                }
                else if( sFileName == "RightImagePath" )
                {
                    stm>>sFileName;
                    pro.sRimgPath = sTempPath + sFileName;
                }
                else if( sFileName == "LeftInteriorOrientationPath" )
                {
                    stm>>sFileName;
                    string sLInOriPath = sTempPath + sFileName;
                    if(ops.sMissionName == "CE-1")
                    {
                        H1 = ReadCE1InOri( sLInOriPath.c_str(),  pro.CE1LimgIO );
                    }
                    else if(ops.sMissionName == "CE-2")
                    {
                        H1 = ReadCE2InOri( sLInOriPath.c_str(),  pro.CE2LimgIO );
                    }
                }
                else if( sFileName == "RightInteriorOrientationPath" )
                {
                    stm>>sFileName;
                    string sRInOriPath = sTempPath + sFileName;
                    if(ops.sMissionName == "CE-1")
                    {
                        H2 = ReadCE1InOri( sRInOriPath.c_str(), pro.CE1RimgIO );
                    }
                    else if(ops.sMissionName == "CE-2")
                    {
                        H2 = ReadCE2InOri( sRInOriPath.c_str(), pro.CE2RimgIO );
                    }
                }
                else if( sFileName == "LeftImageTimePath" )
                {
                    stm>>sFileName;
                    string sLImgTimePath = sTempPath + sFileName;
                    H3 = ReadImgScanTime(sLImgTimePath.c_str(),LImgTime);
                }
                else if( sFileName == "RightImageTimePath" )
                {
                    stm>>sFileName;
                    string sRImgTimePath = sTempPath + sFileName;
                    H4 = ReadImgScanTime(sRImgTimePath.c_str(),RImgTime);
                }
                else if( sFileName == "LeftLineEoPath" )
                {
                    stm>>sFileName;
                    string sLLineEoPath = sTempPath + sFileName;
                    H5 = ReadLineEo(sLLineEoPath.c_str(),LLineEo);
                }
                else if( sFileName == "RightLineEoPath" )
                {
                    stm>>sFileName;
                    string sRLineEoPath = sTempPath + sFileName;
                    H6 = ReadLineEo(sRLineEoPath.c_str(),RLineEo);
                }
                else if( sFileName == "LeftAngleEoPath" )
                {
                    stm>>sFileName;
                    string sLAngleEoPath = sTempPath + sFileName;
                    H7 = ReadAngleEo(sLAngleEoPath.c_str(),LAngleEo);
                }
                else if( sFileName == "RightAngleEoPath" )
                {
                    stm>>sFileName;
                    string sRAngleEoPath = sTempPath + sFileName;
                    H8 = ReadAngleEo(sRAngleEoPath.c_str(),RAngleEo);
                }
                else
                {
                    printf("Feilds not correct! \n") ;
                    return false;
                }
                stm>>sFileName;
            }
        }
        else
        {
            printf("Project File Format is Wrong!\n") ;
            return false;
        }
        stm>>strPath;
        if( strPath == "****Optional**Fields****" )
        {
            stm>>sFileName;
            while( sFileName != "****Optional**Fields**End****" )
            {
                if( sFileName == "MaxCheck" )
                {
                    stm>>ops.nMaxCheck;
                }
                else if( sFileName == "Ratio" )
                {
                    stm>>ops.dRatio;
                }
                else if( sFileName == "Sigma" )
                {
                    stm>>ops.dSigma;
                }
                else if( sFileName == "MinInterTime" )
                {
                    stm>>ops.nMinItera;
                }
                else if( sFileName == "Step" )
                {
                    stm>>ops.nStep;
                }
                else if( sFileName == "RadiusX" )
                {
                    stm>>ops.nRadiusX;
                }
                else if( sFileName == "RadiusY" )
                {
                    stm>>ops.nRadiusY;
                }
                else if( sFileName == "TemplateSize" )
                {
                    stm>>ops.nTemplateSize;
                }
                else if( sFileName == "Coef" )
                {
                    stm>>ops.dCoef;
                }
                else if( sFileName == "XOffSet" )
                {
                    stm>>ops.nXOffSet;
                }
                else if( sFileName == "YOffSet" )
                {
                    stm>>ops.nYOffSet;
                }
                else if( sFileName == "Bands" )
                {
                    stm>>ops.nBands;
                }
                else if( sFileName == "nodata" )
                {
                    stm>>ops.nodata;
                }
                else if( sFileName == "ColThres" )
                {
                    stm>>ops.BlockOps.nColThres;
                }
                else if( sFileName == "RowThres" )
                {
                    stm>>ops.BlockOps.nRowThres;
                }
                else if( sFileName == "OverlayW" )
                {
                    stm>>ops.BlockOps.nOverlayW;
                }
                else if( sFileName == "OverlayH" )
                {
                    stm>>ops.BlockOps.nOverlayH;
                }
                else if( sFileName == "BlockWidth" )
                {
                    stm>>ops.BlockOps.nBlockWidth;
                }
                else if( sFileName == "BlockHeight" )
                {
                    stm>>ops.BlockOps.nBlockHeight;
                }
                else if( sFileName == "ProjectAngle" )
                {
                    stm>>ops.B0;
                }
                else
                {
                    printf("Feilds not correct! \n") ;
                    return false;
                }
                stm>>sFileName;
            }
        }
        else
        {
            printf("Warning:Optional parameters not set! \n Enable the default setting...\n") ;
        }
    }
    else
    {
        printf("Project File can not opened!\n") ;
        return false ;
    }
    if(!(H1&&H2&&H3&&H4&&H5&&H6&&H7&&H8))
    {
        printf("Fail to load files!\n");
        return false;
    }
    else
    {
        bool H1 = mlGetLinearImgEop(LLineEo, LAngleEo, LImgTime, &pro.LimgEo);
        bool H2 = mlGetLinearImgEop(RLineEo, RAngleEo, RImgTime, &pro.RimgEo);
        if(!H1||!H2)
        {
            printf("Exterior orientation parameters can not be interpolated!\n");
            return false;
        }
        else
        {
            printf("Exterior orientation parameters have been interpolated!\n");
        }
    }
    return true;
}
bool CmlSatMapProj::SatMapping( string projPath, string demPath, string domPath, DOUBLE dRes, bool bBaseFlag, bool bUsePts )
{
    //mlSetLogFilePath("/home/lyl612/RabbitVCS/rvml/DL/CODE/trunk/program/UnitTestData/TestSatMappingData/log.txt");
    SatProj pro;
    SatOptions ops;
    LoadProj( pro, ops, projPath, demPath, domPath, dRes, bBaseFlag);
    vector<StereoMatchPt> vecRanPts, vecDensePts;
    vector<Pt3d> vecPres;
    CPtFilesRW clsPtRW;
    SINT nSize = pro.sLimgPath.size();
    SINT nLEnd = pro.sLimgPath.rfind(".");
    SINT nREnd = pro.sRimgPath.rfind(".");
    SINT nLPos = pro.sLimgPath.rfind("/");
    SINT nRPos = pro.sRimgPath.rfind("/");
    string sTempPath,sLPtsPath,sRPtsPath,sLDPtsPath,sRDPtsPath,sLimgName,sRimgName;
     SINT nPos = projPath.rfind("/");
    sTempPath.assign( projPath, 0, nPos+1 );
    sLimgName.assign(pro.sLimgPath,(nRPos+1), nLEnd-nLPos );
    sRimgName.assign(pro.sRimgPath, (nRPos+1), nLEnd-nLPos );
    sLPtsPath = sTempPath + "result/" + sLimgName + "fmf";
    sRPtsPath = sTempPath + "result/" + sRimgName + "fmf";
    sLDPtsPath = sTempPath + "result/" + sLimgName + "dmf";
    sRDPtsPath = sTempPath + "result/" + sRimgName + "dmf";
    ImgPtSet LimgPSet, RimgPSet;
    Pt2d tmpPts;
    StereoMatchPt tmpMatPts;
    if(bUsePts)
    {
        clsPtRW.LoadImgPtSet( sLPtsPath, LimgPSet );
        clsPtRW.LoadImgPtSet( sRPtsPath, RimgPSet );
        UINT LptsSize = LimgPSet.vecPts.size();
        UINT RptsSize = RimgPSet.vecPts.size();

        if( (LptsSize!= RptsSize)||(RptsSize == 0)||(LptsSize == 0) )
        {
            printf("Error: The number of left image feature points is not equal to The number of right image feature points!\n");
            return false;
        }
        else
        {
            int cc;
            cc = LimgPSet.vecPts.size();
            for( UINT i = 0; i < LimgPSet.vecPts.size(); i++ )
            {
                tmpMatPts.ptLInImg = LimgPSet.vecPts[i];
                tmpMatPts.ptRInImg = RimgPSet.vecPts[i];
                vecRanPts.push_back(tmpMatPts);
            }
        }
    }
    else
    {
        ULONG nTmpID = 1001 * 10e11 + 1002 * 10e7 + 1;
        mlSatMatch( pro.sLimgPath, pro.sRimgPath, ops, vecRanPts);
        for( UINT i = 0; i < vecRanPts.size(); i++ )
        {
            tmpMatPts = vecRanPts[i];
            //nTmpID = nLIndex * 10e11 + nRIndex * 10e7 + i + 1;
            tmpMatPts.ptLInImg.lID = nTmpID + i;
            tmpMatPts.ptRInImg.lID = nTmpID + i;
            tmpMatPts.ptLInImg.byType = 1;
            tmpMatPts.ptRInImg.byType = 1;
            LimgPSet.vecPts.push_back(tmpMatPts.ptLInImg);
            RimgPSet.vecPts.push_back(tmpMatPts.ptRInImg);
        }
        clsPtRW.SaveImgPtSet( sLPtsPath, LimgPSet );
        clsPtRW.SaveImgPtSet( sRPtsPath, RimgPSet );

    }
    if(vecRanPts.size() == 0)
    {
        printf("Error: Can not read the match points!\n");
        return false;
    }
    //卫星影像制图
    mlSatMappingByPts( pro, ops, vecRanPts, vecDensePts, vecPres );
    //将密集点和精度存储到文件里
    LimgPSet.vecPts.clear();
    RimgPSet.vecPts.clear();
    ULONG nTmpID = 1001 * 10e11 + 1002 * 10e7 + 1;
    for( UINT i = 0; i < vecDensePts.size(); i++ )
    {
        tmpMatPts = vecDensePts[i];
        //nTmpID = nLIndex * 10e11 + nRIndex * 10e7 + i + 1;
        tmpMatPts.ptLInImg.lID = nTmpID + i;
        tmpMatPts.ptRInImg.lID = nTmpID + i;
        tmpMatPts.ptLInImg.byType = 1;
        tmpMatPts.ptRInImg.byType = 1;
        LimgPSet.vecPts.push_back(tmpMatPts.ptLInImg);
        RimgPSet.vecPts.push_back(tmpMatPts.ptRInImg);
    }
    clsPtRW.SaveImgPtSet( sLDPtsPath, LimgPSet );
    clsPtRW.SaveImgPtSet( sRDPtsPath, RimgPSet );

    return true;
}
/**
* @fn ReadCE1InOri
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 读入CE-1卫星影像内定向参数文件
* @param path 文件路径
* @param CE1img CE-1卫星影像
* @param CE1imgIO CE-1卫星影像内定向参数
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapProj::ReadCE1InOri( const SCHAR *path, CE1IOPara &CE1imgIO )
{
    FILE *pIOFile;
    if(!(pIOFile = fopen(path,"r")))
    {
        printf("Fail to open interior orientation file!\n");
        return false ;
    }
    fclose(pIOFile);
    std::ifstream stmIo(path);
    if(stmIo.is_open())
    {
        stmIo>>CE1imgIO.f>>CE1imgIO.l0>>CE1imgIO.s0>>CE1imgIO.pixsize>>CE1imgIO.x0>>CE1imgIO.y0>>CE1imgIO.nCCD_line>>CE1imgIO.upflag;
    }
    return true;
}
/**
* @fn ReadCE2InOri
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 读入CE-2卫星影像内定向参数文件
* @param path 文件路径
* @param CE2img CE-2卫星影像
* @param CE2imgIO CE-2卫星影像内定向参数
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapProj::ReadCE2InOri( const SCHAR *path, CE2IOPara &CE2imgIO )
{
    FILE *pIOFile;
    if(!(pIOFile = fopen(path,"r")))
    {
        printf("Fail to open interior orientation file!\n");
        return false ;
    }
    fclose(pIOFile);
    std::ifstream stmIo(path);
    if(stmIo.is_open())
    {
        stmIo>>CE2imgIO.f>>CE2imgIO.s0>>CE2imgIO.pixsize>>CE2imgIO.x0>>CE2imgIO.y0>>CE2imgIO.AngleDeg>>CE2imgIO.upflag;
    }
    return true;
}
/**
* @fn ReadImgScanTime
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 读入卫星影像扫描线时间文件
* @param path 文件路径
* @param pImgTime 卫星影像扫描线时间
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapProj::ReadImgScanTime( const SCHAR *path, vector<DOUBLE> &vecImgTime )
{
    DOUBLE tmp;
    FILE *pImgTimeFile;
    if( !( pImgTimeFile = fopen( path , "r" ) ) )
    {
        printf( "Fail to open image scantime file!\n" ) ;
        return false ;
    }
    fclose( pImgTimeFile );
    std::ifstream stmImgTime( path );
    while( !stmImgTime.eof() )
    {
        stmImgTime>>tmp;
        vecImgTime.push_back(tmp);
    }
    vecImgTime.pop_back();
    stmImgTime.close();
    return true;
}
/**
* @fn ReadLineEo
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 读入卫星影像原始测控数据文件外方位线元素
* @param path 文件路径
* @param pEo 外方位线元素
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapProj::ReadLineEo( const SCHAR *path, vector<LineEo> &vecLineEo )
{
    LineEo tmp;
    FILE *pEoFile;
    if(!( pEoFile = fopen( path, "r" ) ) )
    {
        printf( "Fail to open exterior orientation file!\n" ) ;
        return false ;
    }
    fclose( pEoFile ) ;
    std::ifstream stmEo( path );
    while( !stmEo.eof() )/////计算密集匹配点的个数
    {
        stmEo>>tmp.dEoTime>>tmp.pos.X>>tmp.pos.Y>>tmp.pos.Z;
        vecLineEo.push_back(tmp);
    }
    vecLineEo.pop_back();
    stmEo.close();
    return true;
}
/**
* @fn ReadAngleEo
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 读入卫星影像原始测控数据文件外方位角元素
* @param path 文件路径
* @param pEo 卫星影像原始测控数据外方位角元素
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapProj::ReadAngleEo( const SCHAR *path, vector<AngleEo> &vecAngleEo )
{
    AngleEo tmp;
    FILE *pEoFile;
    if(!( pEoFile = fopen( path, "r" ) ) )
    {
        printf( "Fail to open exterior orientation file!\n" ) ;
        return false ;
    }
    fclose( pEoFile ) ;
    std::ifstream stmEo( path );
    while( !stmEo.eof() )/////计算密集匹配点的个数
    {
        stmEo>>tmp.dEoTime>>tmp.ori.phi>>tmp.ori.omg>>tmp.ori.kap;
        vecAngleEo.push_back(tmp);
    }
    vecAngleEo.pop_back();
    stmEo.close();
    return true;
}
