/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlSatMapping.cpp
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 线阵卫星影像制图源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#include "mlSatMapping.h"
#include <iostream>
#define PI 3.141592653
#define Moon_Radius 1737400
using namespace std;
/**
* @fn CmlSatMapping
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlSatMapping类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlSatMapping::CmlSatMapping()
{

}
/**
* @fn ~CmlSatMapping
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlSatMapping类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlSatMapping::~CmlSatMapping()
{

}
/**
* @fn WriteBLH
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 将三维点云数据生成规则格网DEM，并以GeoTiff格式存储
* @param path 文件路径
* @param vecBLH 三维点云数据
* @param XResolution X方向分辨率
* @param YResolution Y方向分辨率
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::WriteBLH( const SCHAR *path, vector<Pt3d> &vecBLH, DOUBLE XReslution, DOUBLE YReslution )
{
    CmlDemProc dem;
    std::ofstream stmBLH(path) ;
    if(!stmBLH.is_open())
    {
        SCHAR strErr[] = "Fail to create DEM file!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false ;
    }
    else
    {
        SCHAR *demppath = (SCHAR*) path;
        stmBLH.close() ;
        dem.mlWriteToGeoFile( vecBLH, XReslution , demppath );
        return true;
    }
}
/**
* @fn GenerateDOM
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 生成CE-1卫星影像DOM并以GeoTiff格式存储
* @param dempath DEM路径
* @param path DOM存储路径
* @param CE1IO 嫦娥一号内方位参数
* @param ImgBlock 原始影像
* @param vecR 旋转矩阵
* @param vecXsYsZs 外方位线元素
* @param domBlock 生成的DOM
* @param nBands 影像波段数
* @param nodata 无数据区值
* @param B0 墨卡托投影割角
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::GenerateDOM( const SCHAR *dempath, const SCHAR *path, CE1IOPara &CE1IO, CmlRasterBlock &ImgBlock, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, CmlRasterBlock &domBlock, DOUBLE B0, UINT nBands, DOUBLE nodata )
{
    if( !ImgBlock.IsValid() )
    {
        return false;
    }
    //读取DEM文件
    CmlGeoRaster GeoRas;
    if(!GeoRas.LoadGeoFile( (SCHAR*)dempath ))
    {
        return false;
    }
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    //定义DOM影像块
    domBlock.InitialImg(DemBlock.GetH(),DemBlock.GetW());
    domBlock.SetGDTType( GDT_Byte );
    vector<Pt3d> vecPtXYZ;
    CmlCE1LinearImage img;
    Pt3d tmpBLH,tmpXYZ;
    DOUBLE scale = Moon_Radius * cos(Deg2Rad(B0));
    DOUBLE tmp;
    for( UINT nY = 0; nY < DemBlock.GetH(); nY++ )
    {
        for( UINT nX = 0; nX < DemBlock.GetW(); nX++ )
        {
            //获得三维坐标
            DemBlock.GetGeoXYZ( nY, nX, tmpBLH );
            //转投影
            tmpBLH.X = Rad2Deg(tmpBLH.X) / scale;
            tmp = atan(exp(tmpBLH.Y / scale)) - PI/4.0;
            tmpBLH.Y = Rad2Deg(tmp) * 2.0;
            //月心大地坐标系转月心直角坐标系
            img.mlBLH2XYZ(tmpBLH,tmpXYZ);
            vecPtXYZ.push_back( tmpXYZ );
        }
    }
    //DOM生成
    if(!img.mlGetCE1DOM(ImgBlock,vecR,vecXsYsZs,&CE1IO, vecPtXYZ, domBlock))
    {
        return false;
    }
    CmlGeoRaster geo;

    std::ofstream stmDOM(path) ;
    if(!stmDOM.is_open())
    {
        SCHAR strErr[] = "Fail to create DOM file!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false ;
    }
    else
    {
        geo.CreateGeoFile( (SCHAR*)path, GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), nBands, GDT_Byte, nodata );
        geo.SaveToGeoFile( nBands,0,0,&domBlock );
    }
    return true;
}

/**
* @fn GenerateCE2DOM
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 生成DOM并以GeoTiff格式存储
* @param dempath DEM路径
* @param path DOM存储路径
* @param CE2IO 嫦娥一号内方位参数
* @param ImgBlock 原始影像
* @param vecR 旋转矩阵
* @param vecXsYsZs 外方位线元素
* @param domBlock 生成的DOM
* @param nBands 影像波段数
* @param nodata 无数据区值
* @param B0 墨卡托投影割角
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::GenerateCE2DOM( const SCHAR *dempath, const SCHAR *path, CE2IOPara &CE2IO, CmlRasterBlock &ImgBlock, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, CmlRasterBlock &domBlock, DOUBLE B0, UINT nBands, DOUBLE nodata )
{
    if( !ImgBlock.IsValid() )
    {
        return false;
    }
    //读取DEM文件
    CmlGeoRaster GeoRas;
    if(!GeoRas.LoadGeoFile( (SCHAR*)dempath ))
    {
        return false;
    }
    CmlRasterBlock DemBlock;
    GeoRas.GetRasterOriginBlock( GeoRas.GetBands(), (UINT)0, (UINT)0, GeoRas.GetWidth(), GeoRas.GetHeight(),(UINT)1, &DemBlock);
    //定义DOM影像块
    domBlock.InitialImg(DemBlock.GetH(),DemBlock.GetW());
    domBlock.SetGDTType( GDT_Byte );
    vector<Pt3d> vecPtXYZ;
    CmlCE2LinearImage img;
    Pt3d tmpBLH,tmpXYZ;
    DOUBLE scale = Moon_Radius * cos(Deg2Rad(B0));
    DOUBLE tmp;
    for( UINT nY = 0; nY < DemBlock.GetH(); nY++ )
    {
        for( UINT nX = 0; nX < DemBlock.GetW(); nX++ )
        {
            //获得三维坐标
            DemBlock.GetGeoXYZ( nY, nX, tmpBLH );
            //转投影
            tmpBLH.X = Rad2Deg(tmpBLH.X) / scale;
            tmp = atan(exp(tmpBLH.Y / scale)) - PI/4.0;
            tmpBLH.Y = Rad2Deg(tmp) * 2.0;
            //月心大地坐标系转月心直角坐标系
            img.mlBLH2XYZ(tmpBLH,tmpXYZ);
            vecPtXYZ.push_back( tmpXYZ );
        }
    }
    //DOM生成
    if(!img.mlGetCE2DOM(ImgBlock,vecR,vecXsYsZs,&CE2IO, vecPtXYZ, domBlock))
    {
        return false;
    }
    CmlGeoRaster geo;
    std::ofstream stmDOM(path) ;
    if(!stmDOM.is_open())
    {
        SCHAR strErr[] = "Fail to create DEM file!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false ;
    }
    else
    {
        geo.CreateGeoFile( (SCHAR*)path, GeoRas.m_PtOrigin, GeoRas.m_dXResolution, -GeoRas.m_dXResolution, GeoRas.GetHeight(), GeoRas.GetWidth(), nBands, GDT_Byte, nodata );
        geo.SaveToGeoFile( nBands,0,0,&domBlock );
    }
    return true;
}
/**
* @fn InterSection
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 前方交会生成三维物方点
* @param ptSubDenseL 左影像块子像素密集匹配点
* @param ptSubDenseR 右影像块子像素密集匹配点
* @param vecXYL 左影像块焦平面坐标
* @param vecXYR 右影像块焦平面坐标
* @param vecLXsYsZs 左影像块外方位线元素
* @param vecRXsYsZs 右影像块外方位线元素
* @param vecRL 左影像块旋转矩阵
* @param vecRR 右影像块旋转矩阵
* @param lf 左影像焦距
* @param rf 右影像焦距
* @param vecBLH 三维物方点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::InterSection(vector<Pt2d> &ptSubDenseL, vector<Pt2d> &ptSubDenseR, vector<Pt2d> &vecXYL, vector<Pt2d> &vecXYR, \
                                 vector<Pt3d> &vecLXsYsZs, vector<Pt3d> &vecRXsYsZs,vector<MatrixR> &vecRL, vector<MatrixR> &vecRR, DOUBLE lf, DOUBLE rf,vector<Pt3d> &vecXYZ )
{
    if( (ptSubDenseL.size()!=ptSubDenseL.size()) || (ptSubDenseL.size()!=vecXYL.size()) || (ptSubDenseL.size()!=vecXYR.size()) )
    {
        return false;
    }
    if( (ptSubDenseL.size() == 0)||(vecLXsYsZs.size() == 0)||(vecRXsYsZs.size() == 0)||(vecRL.size() == 0)||(vecRR.size() == 0) )
    {
        return false;
    }
    DOUBLE dLX, dLY, dRX, dRY;
    Pt3d tmpPt3d,tmpPt3dd;
    //vector<Pt3d> vecXYZ;
    UINT inxL,inxR;
    CmlStereoProc stp;
    for ( UINT k = 0; k < vecXYL.size(); k++ )
    {
        inxL = UINT( ptSubDenseL[k].Y );
        inxR = UINT( ptSubDenseR[k].Y );
        dLX = vecXYL[k].X;
        dLY = vecXYL[k].Y;
        dRX = vecXYR[k].X;
        dRY = vecXYR[k].Y;
        CmlMat tmp;
        Pt3d tmppt3d;
        tmp = vecRL[inxL].matR;
        tmppt3d = vecLXsYsZs[inxL];
        //空间交会得到月固空间直角坐标系坐标
        stp.mlInterSection(dLX, dLY, dRX, dRY, &vecRL[inxL].matR, &vecRR[inxR].matR, vecLXsYsZs[inxL], vecRXsYsZs[inxR],lf,rf, tmpPt3d);

        vecXYZ.push_back(tmpPt3d);
    }
    return true;
}
/**
* @fn ConstractAdjust
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 影像自动对比度增强
* @param Img 影像块
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::ConstractAdjust(CmlRasterBlock &Img)
{

    //判断异常
    if( !Img.IsValid() )
    {
        return false;
    }

    BYTE max_val = 0;
    BYTE min_val = 255;
    BYTE tmp_val;
    //查找影像最大灰度值与最小灰度值
    for( UINT i = 0; i < Img.GetH(); i++ )
    {
        for( UINT j = 0; j < Img.GetW(); j++ )
        {
            tmp_val = Img.GetAt(i,j);
            if( tmp_val > max_val )
            {
                max_val = tmp_val;
            }
            if( tmp_val < min_val )
            {
                min_val = tmp_val;
            }
        }
    }
    //若数据块只有一个灰度值的情况
    if( max_val == min_val )
    {
        return true;
    }
    else
    {
        //进行灰度拉伸
        for( UINT i = 0; i < Img.GetH(); i++ )
        {
            for( UINT j = 0; j < Img.GetW(); j++ )
            {
                tmp_val = Img.GetAt( i, j );
                tmp_val = ( tmp_val - min_val ) * 255 / ( max_val - min_val );
                if( tmp_val > 255 )
                {
                    tmp_val = 255;
                }
                else if( tmp_val < 0 )
                {
                    tmp_val = 0;
                }
                Img.SetAt( i, j, tmp_val );
            }
        }
    }
    return true;
}

SINT MatchMethod1( CmlRasterBlock &LimgBlock, CmlRasterBlock &RimgBlock, vector<StereoMatchPt> &vecPts,UINT nMaxCheck, DOUBLE dRatio, DOUBLE dThresh, DOUBLE dConfidence )
{
    SINT nPtNum = 0;
    SINT nRanPts = 0;
    vector<DOUBLE> vxl, vyl, vxr, vyr;

    nPtNum = SiftMatchVector((SCHAR*)LimgBlock.GetData(),LimgBlock.GetW(),LimgBlock.GetH(),8,(SCHAR*)RimgBlock.GetData(),RimgBlock.GetW(),RimgBlock.GetH(),8,vxl,vyl,vxr,vyr,nMaxCheck,dRatio);
    if( nPtNum <= 0 )
    {
        SCHAR strErr[] = "Sift matching is failed!\n" ;
        LOGAddErrorMsg(strErr) ;
        return 0;
    }
    else
    {
        //Ransac剔除粗差
        vecPts.clear();
        CmlStereoProc stp;
        nRanPts = stp.mlGetRansacPts( vxl, vyl, vxr, vyr, vecPts, dThresh, dConfidence );
        printf("Ransac: %d /%d Points!\n",nRanPts,nPtNum);
    }
    return nRanPts;
}
SINT MatchMethod2( CmlRasterBlock &LimgBlock, CmlRasterBlock &RimgBlock, vector<StereoMatchPt> &vecPts,UINT nMaxCheck, DOUBLE dRatio, DOUBLE dSigma, UINT nMinItera )
{
    SINT nPtNum = 0;
    SINT nRanPts = 0;
    vector<DOUBLE> vxl, vyl, vxr, vyr;
    nPtNum = SiftMatchVector((SCHAR*)LimgBlock.GetData(),LimgBlock.GetW(),LimgBlock.GetH(),8,(SCHAR*)RimgBlock.GetData(),RimgBlock.GetW(),RimgBlock.GetH(),8,vxl,vyl,vxr,vyr,nMaxCheck,dRatio);
    if( nPtNum <= 0 )
    {
        SCHAR strErr[] = "Sift matching is failed!\n" ;
        LOGAddErrorMsg(strErr) ;
        return 0;
    }
    else
    {
        //Ransac剔除粗差
        vecPts.clear();
        CmlStereoProc stp;
        nRanPts = stp.mlGetRansacPtsByAffineT(vxl, vyl, vxr, vyr, vecPts, dSigma, nMinItera );
        printf("Ransac: %d /%d Points!\n",nRanPts,nPtNum);
    }
    return nRanPts;
}
SINT MatchMethod3(CmlRasterBlock &LimgBlock, CmlRasterBlock &RimgBlock, vector<StereoMatchPt> &vecPts)
{
    DOUBLE *pLx, *pRx, *pLy, *pRy;
    pLx = pRx = pLy = pRy = NULL;
    SINT nPtNum = ASiftMatch( LimgBlock.GetData(), LimgBlock.GetW(), LimgBlock.GetH(), RimgBlock.GetData(), RimgBlock.GetW(), RimgBlock.GetH(), pLx, pLy, pRx, pRy, 8 );
    if( nPtNum <= 0 )
    {
        SCHAR strErr[] = "ASift matching is failed!\n" ;
        LOGAddErrorMsg(strErr) ;
        return 0;
    }
    else
    {
        vecPts.clear();
        StereoMatchPt tmpPts;
        for( UINT i = 0; i < UINT(nPtNum); ++i )
        {
            tmpPts.ptLInImg.X = pLx[i];
            tmpPts.ptLInImg.Y = pLy[i];
            tmpPts.ptRInImg.X = pRx[i];
            tmpPts.ptRInImg.Y = pRy[i];
            vecPts.push_back( tmpPts );
        }
        printf("ASift: %d Points!\n",nPtNum);
    }
    return nPtNum;
}
/**
* @fn mlSatMatch
* @date 2012.2.21
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像特征点匹配
* @param sLimgPath 左影像路径
* @param sRimgPath 右影像路径
* @param satPara 匹配参数
* @param vecRanPts 匹配点
* @param method 匹配方法选择
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::mlSatMatch( const string sLimgPath, const string sRimgPath, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, SINT nMethod )
{
    CmlGdalDataset Limg,Rimg;
    if(Limg.LoadFile(sLimgPath.c_str())&& Rimg.LoadFile(sRimgPath.c_str()))
    {
        SCHAR strErr[] = "Images have been loaded!\n" ;
        LOGAddNoticeMsg(strErr) ;
    }
    else
    {
        SCHAR strErr[] = "Can not load the images,please check the image files!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //影像分块
    CmlBlockCalculation BlockData(satPara.BlockOps.nColThres,satPara.BlockOps.nRowThres,satPara.BlockOps.nOverlayW,satPara.BlockOps.nOverlayH);
    bool H1, H2;
    H1 = BlockData.CalBlockCol( Limg.GetWidth(), satPara.BlockOps.nBlockWidth );
    H2 = BlockData.CalBlockRow( Limg.GetHeight(), satPara.BlockOps.nBlockHeight );
    UINT rowCount = BlockData.GetRowCount();
    UINT colCount = BlockData.GetColCount();

    if(!(H1&&H2))
    {
        SCHAR strErr[] = "Can not get image block,please redefine block parameters!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    Pt2i ptOrg;
    UINT nW,nH;
    for( UINT j = 0; j < rowCount; j++ )
    {
        for ( UINT i = 0; i < colCount; i++ )
        {
            //获得对应影像块
            CmlRasterBlock LimgBlock, RimgBlock;
            if( BlockData.GetBlockPos( i, j, ptOrg, nW, nH ) )
            {
                Limg.GetRasterGrayBlock(Limg.GetBands(),ptOrg.X, ptOrg.Y, nW, nH, (UINT)1, &LimgBlock);
                Rimg.GetRasterGrayBlock(Rimg.GetBands(),ptOrg.X, ptOrg.Y, nW, nH, (UINT)1, &RimgBlock);
            }
            else
            {
                SCHAR strErr[] = "Can not get image block!\n" ;
                LOGAddErrorMsg(strErr) ;
                return false;
            }
            if(!(ConstractAdjust(LimgBlock)&&ConstractAdjust(RimgBlock)))
            {
                SCHAR strErr[] = "Can not constract adjust the image blocks!\n" ;
                LOGAddErrorMsg(strErr) ;
                return false;
            }
            //Sift匹配及Ransac
            vector<StereoMatchPt> vecPts;
            SINT nPts = 0;
            if( nMethod == 0 )
            {
                nPts = MatchMethod1( LimgBlock, RimgBlock, vecPts, satPara.nMaxCheck, satPara.dRatio, satPara.dThresh, satPara.dConfidence );

            }
            else if(nMethod == 1)
            {
                nPts = MatchMethod2( LimgBlock, RimgBlock, vecPts, satPara.nMaxCheck, satPara.dRatio, satPara.dSigma, satPara.nMinItera );
            }
            else if(nMethod == 2)
            {
                nPts=MatchMethod3(LimgBlock, RimgBlock, vecPts);
            }
            else
            {
                SCHAR strErr[] = "Matching method not found!\n";
                LOGAddErrorMsg(strErr) ;
                return false;
            }

            if( nPts <= 0 )
            {
                SCHAR strErr[] = "Ransac is failed!\n";
                LOGAddErrorMsg(strErr) ;
                return false;
            }
            else
            {
                //写入七点结构
                CmlStereoProc stp;
//                DOUBLE dcoef;
//                DOUBLE lx,ly,rx,ry;
//                SINT count = 0;
                for(SINT k = 0; k < nPts; k++)
                {
                    StereoMatchPt tmp;
                    tmp = vecPts.at(k);
                    tmp.ptLInImg.X += BlockData.GetStartCol(i);
                    tmp.ptLInImg.Y += BlockData.GetStartRow(j);
                    tmp.ptRInImg.X += BlockData.GetStartCol(i);
                    tmp.ptRInImg.Y += BlockData.GetStartRow(j);
                    vecRanPts.push_back(tmp);
//                      lx=tmp.ptLInImg.X;ly=tmp.ptLInImg.Y;rx=tmp.ptRInImg.X;ry=tmp.ptRInImg.Y;
//                      stp.mlLsMatch(&LimgBlock, &RimgBlock,lx,ly,rx,ry,satPara.nTemplateSize, dcoef );
//                      if(dcoef>0.85)
//                      {
//                            tmp.ptLInImg.X = lx + BlockData.GetStartCol(i);
//                            tmp.ptLInImg.Y = ly + BlockData.GetStartRow(j);
//                            tmp.ptRInImg.X = rx + BlockData.GetStartCol(i);
//                            tmp.ptRInImg.Y = ry + BlockData.GetStartRow(j);
//                            vecRanPts.push_back(tmp);
//                            count++;
//                        }
                }
//                printf("LSMatch: %d /%d Points!\n",nPts,count);
            }
    }
}
LOGAddSuccessQuitMsg( ) ;
//printf("Satellite Images have been matched by Sift Algrithom!\n Completely 100%!\n");
return true;
}
/**
* @fn mlCE1MappingByPts
* @date 2012.2.21
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 由CE-1卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM
* @param satproj 卫星影像DEM及DOM生成工程参数
* @param satPara 卫星影像DEM及DOM生成参数
* @param vecRanPts 匹配点
* @param vecDensePts 密集匹配点及物方三维点
* @param vecPres  物方三维点精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::mlCE1MappingByPts( SatProj &satproj, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, vector<StereoMatchPt> &vecDensePts, vector<Pt3d> &vecPres )
{
    CmlCE1LinearImage LCE1img, RCE1img;
    bool H1,H2;
    //读入左右影像
    if(LCE1img.LoadFile(satproj.sLimgPath.c_str())&& RCE1img.LoadFile(satproj.sRimgPath.c_str()))
    {
        SCHAR strErr[] = "Images have been loaded!\n" ;
        LOGAddNoticeMsg(strErr) ;
    }
    else
    {
        SCHAR strErr[] = "Can not load the images,please check the image files!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //影像内参数赋值
    LCE1img.m_CE1IOPara = satproj.CE1LimgIO;
    RCE1img.m_CE1IOPara = satproj.CE1RimgIO;
    LCE1img.m_CE1IOPara.nSample = LCE1img.GetWidth();
    LCE1img.m_CE1IOPara.nLine = LCE1img.GetHeight();
    RCE1img.m_CE1IOPara.nSample = RCE1img.GetWidth();
    RCE1img.m_CE1IOPara.nLine = RCE1img.GetHeight();

    //外方位元素赋值
    LCE1img.m_vecImgEo = satproj.LimgEo;
    RCE1img.m_vecImgEo = satproj.RimgEo;
    //将外方位转成旋转矩阵
    H1 = LCE1img.mlCE1OPK2RMat(LCE1img.m_vecImgEo,LCE1img.m_vecPosition, LCE1img.m_vecMatrix);
    H2 = RCE1img.mlCE1OPK2RMat(RCE1img.m_vecImgEo,RCE1img.m_vecPosition, RCE1img.m_vecMatrix);
    if(!(H1&&H2))
    {
        SCHAR strErr[] = "Can not convert exterior orientation parameters to ratation matrixs!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //获得对应影像块
    CmlRasterBlock LimgBlock, RimgBlock;
    LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),0, 0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &LimgBlock);
    RCE1img.GetRasterGrayBlock(RCE1img.GetBands(),0, 0, RCE1img.GetWidth(), RCE1img.GetHeight(), (UINT)1, &RimgBlock);
    //影像自动对比度
    if(!(ConstractAdjust(LimgBlock)&&ConstractAdjust(RimgBlock)))
    {
        SCHAR strErr[] = "Can not constract adjust the image blocks!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //密集匹配
    vector<Pt2d> ptSubDenseL,ptSubDenseR;
    WideOptions wid;//密集匹配相关参数设置
    wid.nStep = satPara.nStep;
    wid.nRadiusX = satPara.nRadiusX;
    wid.nRadiusY = satPara.nRadiusY;
    wid.nTemplateSize = satPara.nTemplateSize;
    wid.dCoef = satPara.dCoef;
    wid.nXOffSet = satPara.nXOffSet;
    wid.nYOffSet = satPara.nYOffSet;
    CmlStereoProc stp;
    H1 = stp.mlDenseMatch( &LimgBlock, &RimgBlock, vecRanPts, wid, ptSubDenseL, ptSubDenseR );
    if( !H1 )
    {
        SCHAR strErr[] = "Dense matching has problems!\n";
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //内定向
    vector<Pt2d> vecXYL, vecXYR;
    H1 = LCE1img.mlCE1InOrietation( ptSubDenseL, &LCE1img.m_CE1IOPara, vecXYL );
    H2 = RCE1img.mlCE1InOrietation( ptSubDenseR, &RCE1img.m_CE1IOPara, vecXYR );
    if( !(H1 && H2) )
    {
        SCHAR strErr[] = "Interior orientation has problems!\n";
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //前方交会
    vector<Pt3d> vecXYZ,vecBLH;
    H1 = InterSection(ptSubDenseL, ptSubDenseR, vecXYL, vecXYR, LCE1img.m_vecPosition, RCE1img.m_vecPosition, LCE1img.m_vecMatrix, RCE1img.m_vecMatrix, LCE1img.m_CE1IOPara.f, RCE1img.m_CE1IOPara.f, vecXYZ );
    vecDensePts.clear();
    //写入七点结构
    CmlCE1LinearImage cls;
    StereoMatchPt tmppts;
    Pt3d tmpxyz,tmpblh;
    //处理投影割角超过纬度范围的情况
    satPara.B0 = abs(satPara.B0) > 90 ? 90 : abs(satPara.B0);
    for( UINT i = 0; i < vecXYZ.size(); i++ )
    {
        tmppts.ptLInImg = ptSubDenseL[i];
        tmppts.ptRInImg = ptSubDenseR[i];
        tmppts.X = tmpxyz.X = vecXYZ[i].X;
        tmppts.Y = tmpxyz.Y = vecXYZ[i].Y;
        tmppts.Z = tmpxyz.Z = vecXYZ[i].Z;
        //月心直角坐标系转月心大地坐标系
        cls.mlXYZ2BLH( tmpxyz, tmpblh );
        vecDensePts.push_back(tmppts);
        //月心大地坐标系转正轴等角割B0度墨卡托投影
        tmpblh.X=Moon_Radius*cos(Deg2Rad(satPara.B0))*Deg2Rad(tmpblh.X);//Longitude
        tmpblh.Y=Moon_Radius*cos(Deg2Rad(satPara.B0))*log(tan(PI/4.0+Deg2Rad(tmpblh.Y/2.0)));//Latitude
        vecBLH.push_back(tmpblh);
    }
    if( !(H1 && H2) )
    {
        SCHAR strErr[] = "Space intersection has problems!\n";
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //DEM及DOM生成
    //将三维点写成geotiff格式DEM
    H1 = WriteBLH( satproj.sDemPath.c_str(), vecBLH, satPara.XResolution, satPara.YResolution );
    if( H1 )
    {
        SCHAR strErr[] = "DEM has generated successfully!\n";
        LOGAddNoticeMsg(strErr);
    }
    else
    {
        SCHAR strErr[] = "DEM generation if failed!\n";
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    CmlRasterBlock domImg, ImgBlock;
    if( satPara.bLeftBaseFlag )//以左片为基准做DOM
    {
        LCE1img.GetRasterGrayBlock(LCE1img.GetBands(),(UINT)0, (UINT)0, LCE1img.GetWidth(), LCE1img.GetHeight(), (UINT)1, &ImgBlock);
        H1 = GenerateDOM( satproj.sDemPath.c_str(), satproj.sDomPath.c_str(), LCE1img.m_CE1IOPara, ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, domImg, satPara.B0, satPara.nBands, satPara.nodata );
        //H1 = GenerateDOM1( satproj.sDemPath.c_str(), satproj.sDomPath.c_str(), LCE1img.m_CE1IOPara, ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, domImg, &TransMat,TransVec, satPara.nBands, satPara.nodata );
    }
    else//以右片为基准做DOM
    {
        RCE1img.GetRasterGrayBlock(RCE1img.GetBands(),(UINT)0, (UINT)0, RCE1img.GetWidth(), RCE1img.GetHeight(), (UINT)1, &ImgBlock);
        H1 = GenerateDOM( satproj.sDemPath.c_str(), satproj.sDomPath.c_str(), LCE1img.m_CE1IOPara, ImgBlock, LCE1img.m_vecMatrix, LCE1img.m_vecPosition, domImg,satPara.B0, satPara.nBands, satPara.nodata );
        //H1 = GenerateDOM1( satproj.sDemPath.c_str(), satproj.sDomPath.c_str(),  RCE1img.m_CE1IOPara, ImgBlock, RCE1img.m_vecMatrix, RCE1img.m_vecPosition, domImg, &TransMat,TransVec, satPara.nBands, satPara.nodata );
    }
    if(H1)
    {
        SCHAR strErr[] = "DOM has generated successfully!\n";
        LOGAddNoticeMsg(strErr);
    }
    else
    {
        SCHAR strErr[] = "DOM generation is failed!\nDone!\n";
        LOGAddErrorMsg(strErr);
        return false;
    }
    LOGAddSuccessQuitMsg();
    printf("DONE!\n");
    return true;
}
/**
* @fn mlCE2MappingByPts
* @date 2012.2.21
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 由CE-2卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM
* @param satproj 卫星影像DEM及DOM生成工程参数
* @param satPara 卫星影像DEM及DOM生成参数
* @param vecRanPts 匹配点
* @param vecDensePts 密集匹配点及物方三维点
* @param vecPres  物方三维点精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::mlCE2MappingByPts( SatProj &satproj, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, vector<StereoMatchPt> &vecDensePts, vector<Pt3d> &vecPres )
{
    CmlCE2LinearImage LCE2img, RCE2img;
    bool H1,H2;
    //读入左右影像
    if(LCE2img.LoadFile(satproj.sLimgPath.c_str())&& RCE2img.LoadFile(satproj.sRimgPath.c_str()))
    {
        SCHAR strErr[] = "Images have been loaded!\n" ;
        LOGAddNoticeMsg(strErr) ;
    }
    else
    {
        SCHAR strErr[] = "Can not load the images,please check the image files!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //影像内参数赋值
    LCE2img.m_CE2IOPara = satproj.CE2LimgIO;
    RCE2img.m_CE2IOPara = satproj.CE2RimgIO;
    LCE2img.m_CE2IOPara.nSample = LCE2img.GetWidth();
    RCE2img.m_CE2IOPara.nSample = RCE2img.GetWidth();
    LCE2img.m_CE2IOPara.nLine = LCE2img.GetHeight();
    RCE2img.m_CE2IOPara.nLine = RCE2img.GetHeight();

    //外方位元素赋值
    LCE2img.m_vecImgEo = satproj.LimgEo;
    RCE2img.m_vecImgEo = satproj.RimgEo;
    //将外方位转成旋转矩阵
    H1 = LCE2img.mlCE2OPK2RMat(LCE2img.m_vecImgEo,LCE2img.m_vecPosition, LCE2img.m_vecMatrix);
    H2 = RCE2img.mlCE2OPK2RMat(RCE2img.m_vecImgEo,RCE2img.m_vecPosition, RCE2img.m_vecMatrix);
    if(!(H1&&H2))
    {
        SCHAR strErr[] = "Can not convert exterior orientation parameters to ratation matrixs!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //获得对应影像块
    CmlRasterBlock LimgBlock, RimgBlock;
    LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),0, 0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &LimgBlock);
    RCE2img.GetRasterGrayBlock(RCE2img.GetBands(),0, 0, RCE2img.GetWidth(), RCE2img.GetHeight(), (UINT)1, &RimgBlock);
    //影像自动对比度
    if(!(ConstractAdjust(LimgBlock)&&ConstractAdjust(RimgBlock)))
    {
        SCHAR strErr[] = "Can not constract adjust the image blocks!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //密集匹配
    vector<Pt2d> ptSubDenseL,ptSubDenseR;
    WideOptions wid;//密集匹配相关参数设置
    wid.nStep = satPara.nStep;
    wid.nRadiusX = satPara.nRadiusX;
    wid.nRadiusY = satPara.nRadiusY;
    wid.nTemplateSize = satPara.nTemplateSize;
    wid.dCoef = satPara.dCoef;
    wid.nXOffSet = satPara.nXOffSet;
    wid.nYOffSet = satPara.nYOffSet;
    CmlStereoProc stp;
    H1 = stp.mlDenseMatch( &LimgBlock, &RimgBlock, vecRanPts, wid, ptSubDenseL, ptSubDenseR );
    if( !H1 )
    {
        SCHAR strErr[] = "Dense matching has problems!\n";
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //内定向
    vector<Pt2d> vecXYL, vecXYR;
    H1 = LCE2img.mlCE2InOrietation( ptSubDenseL, &LCE2img.m_CE2IOPara, vecXYL );
    H2 = RCE2img.mlCE2InOrietation( ptSubDenseR, &RCE2img.m_CE2IOPara, vecXYR );
    if( !(H1 && H2) )
    {
        SCHAR strErr[] = "Interior orientation has problems!\n";
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //前方交会
    SCHAR strErr[] = "Doing Intersection...!\n";
    LOGAddNoticeMsg(strErr) ;
    vector<Pt3d> vecXYZ,vecBLH;
    H1 = InterSection(ptSubDenseL, ptSubDenseR, vecXYL, vecXYR, LCE2img.m_vecPosition, RCE2img.m_vecPosition, LCE2img.m_vecMatrix, RCE2img.m_vecMatrix, LCE2img.m_CE2IOPara.f, RCE2img.m_CE2IOPara.f, vecXYZ );
    vecDensePts.clear();
    //写入七点结构
    CmlCE1LinearImage cls;
    StereoMatchPt tmppts;
    Pt3d tmpxyz,tmpblh;
    //处理投影割角超过纬度范围的情况
    satPara.B0 = abs(satPara.B0) > 90 ? 90 : abs(satPara.B0);
    for( UINT i = 0; i < vecXYZ.size(); i++ )
    {
        tmppts.ptLInImg = ptSubDenseL[i];
        tmppts.ptRInImg = ptSubDenseR[i];
        tmppts.X = tmpxyz.X = vecXYZ[i].X;
        tmppts.Y = tmpxyz.Y = vecXYZ[i].Y;
        tmppts.Z = tmpxyz.Z = vecXYZ[i].Z;
        //月心直角坐标系转月心大地坐标系
        cls.mlXYZ2BLH( tmpxyz, tmpblh );
        vecDensePts.push_back(tmppts);
        //月心大地坐标系转正轴等角割B0度墨卡托投影
        tmpblh.X=Moon_Radius*cos(Deg2Rad(satPara.B0))*Deg2Rad(tmpblh.X);//Longitude
        tmpblh.Y=Moon_Radius*cos(Deg2Rad(satPara.B0))*log(tan(PI/4.0+Deg2Rad(tmpblh.Y/2.0)));//Latitude
        vecBLH.push_back(tmpblh);
    }
    if( !(H1 && H2) )
    {
        SCHAR strErr[] = "Space intersection has problems!\n";
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //DEM及DOM生成
    //将三维点写成geotiff格式DEM
    SCHAR strErr1[] = "Writing BLH to File...!\n";
    LOGAddNoticeMsg(strErr1) ;

    H1 = WriteBLH( satproj.sDemPath.c_str(), vecBLH, satPara.XResolution, satPara.YResolution );
    if( H1 )
    {
        SCHAR strErr[] = "DEM has generated successfully!\n";
        LOGAddNoticeMsg(strErr) ;
    }
    else
    {
        SCHAR strErr[] = "DEM generation if failed!\n";
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    CmlRasterBlock domImg, ImgBlock;
    if( satPara.bLeftBaseFlag )//以左片为基准做DOM
    {
        LCE2img.GetRasterGrayBlock(LCE2img.GetBands(),(UINT)0, (UINT)0, LCE2img.GetWidth(), LCE2img.GetHeight(), (UINT)1, &ImgBlock);
        H1 = GenerateCE2DOM( satproj.sDemPath.c_str(), satproj.sDomPath.c_str(), LCE2img.m_CE2IOPara, ImgBlock, LCE2img.m_vecMatrix, LCE2img.m_vecPosition, domImg, satPara.B0, satPara.nBands, satPara.nodata );
    }
    else//以右片为基准做DOM
    {
        RCE2img.GetRasterGrayBlock(RCE2img.GetBands(),(UINT)0, (UINT)0, RCE2img.GetWidth(), RCE2img.GetHeight(), (UINT)1, &ImgBlock);
        H1 = GenerateCE2DOM( satproj.sDemPath.c_str(), satproj.sDomPath.c_str(),  RCE2img.m_CE2IOPara, ImgBlock, RCE2img.m_vecMatrix, RCE2img.m_vecPosition, domImg, satPara.B0, satPara.nBands, satPara.nodata );
    }
    if(H1)
    {
        SCHAR strErr[] = "DOM has generated successfully!\n";
        LOGAddNoticeMsg(strErr);
    }
    else
    {
        SCHAR strErr[] = "DOM generation is failed!\nDone!\n";
        LOGAddErrorMsg(strErr);
        return false;
    }
    LOGAddSuccessQuitMsg();
    return true;
}
/**
* @fn mlSatMappingByPts
* @date 2012.2.21
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 由卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM
* @param satproj 卫星影像DEM及DOM生成工程参数
* @param satPara 卫星影像DEM及DOM生成参数
* @param vecRanPts 匹配点
* @param vecDensePts 密集匹配点及物方三维点
* @param vecPres  物方三维点精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlSatMapping::mlSatMappingByPts( SatProj &satproj, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, vector<StereoMatchPt> &vecDensePts, vector<Pt3d> &vecPres )
{
    if((satproj.LimgEo.size() == 0)||(satproj.RimgEo.size() == 0)||(satproj.sDemPath == " ")||(satproj.sDomPath == " ")||(satproj.sLimgPath == " ")||(satproj.sRimgPath == " ") )
    {
        SCHAR strErr[] = "Parameters set in this project is incorrect!";
        LOGAddErrorMsg(strErr);
        return false;
    }
    if( satPara.sMissionName == "CE-1" )
    {
        if((satproj.CE1LimgIO.nCCD_line != 11)&&(satproj.CE1LimgIO.nCCD_line != 512)&&(satproj.CE1LimgIO.nCCD_line != 1013))
        {
            SCHAR strErr[] = "Interior Orientation Parameters of CE-1 is set incorrectly!";
            LOGAddErrorMsg(strErr);
            return false;
        }
        if( (satPara.XResolution <120 ) || (satPara.YResolution < 120) )
        {
            SCHAR strErr[] = "The original resolution of CE-1 is 120m, DEM resolution exceeding the value can not reach higher precision and it maybe take a lot of time!\n";
            LOGAddNoticeMsg(strErr);
        }
        return mlCE1MappingByPts( satproj, satPara, vecRanPts, vecDensePts, vecPres );
    }
    else if( satPara.sMissionName == "CE-2" )
    {

        if((satproj.CE2LimgIO.AngleDeg != 7.98)&&(satproj.CE2LimgIO.AngleDeg != -17.24))
        {
            SCHAR strErr[] = "Interior Orientation Parameters of CE-2 is set incorrectly!";
            LOGAddErrorMsg(strErr);
            return false;
        }
        if( (satPara.XResolution <1.5 ) || (satPara.YResolution < 1.5) )
        {
            SCHAR strErr[] = "The original resolutions of CE-2 are 1.5m and 7m, DEM resolution exceeding these values can not reach higher precision and it maybe take a lot of time!\n";
            LOGAddNoticeMsg(strErr);
        }
        return mlCE2MappingByPts( satproj, satPara, vecRanPts, vecDensePts, vecPres );
    }
    else
    {
        SCHAR strErr[] = "Mission is not defined!";
        LOGAddErrorMsg(strErr);
        return false;
    }
    LOGAddSuccessQuitMsg();
    return true;

}


