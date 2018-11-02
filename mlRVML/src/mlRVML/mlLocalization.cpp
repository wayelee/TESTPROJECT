/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlLocalization.cpp
* @date 2011.12.18
* @author 万文辉 whwan@irsa.ac.cn
* @brief  定位功能类源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#include "mlLocalization.h"
#include "mlGeoRaster.h"
#include "SiftMatch.h"
#include "ASift.h"
#include "mlStereoProc.h"
#include "mlFrameImage.h"
#include "mlPtsManage.h"
#include "mlSatMapping.h"
#include "mlCamCalib.h"
#include "mlPhgProc.h"
//#include "mlSatImgProc.h"
#include "mlBA.h"
typedef struct tagPtList
{
    Pt3d CirInfo;
    Pt3d pt1;
    Pt3d pt2;
} PtList;

/**
* @fn CmlLocalization
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlLocalization类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlLocalization::CmlLocalization()
{
    //ctor
}

/**
* @fn ~CmlLocalization
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlLocalization类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlLocalization::~CmlLocalization()
{
    //dtor
}

/**
* @fn LocalInSequenceImg
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief LocalInSequenceImg 序列影像定位
* @param strSatPath 影像路径
* @param vecFrameInfo 影像参数
* @param ptLocalRes 定位结果
* @param dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::LocalInSequenceImg(  FrameImgInfo FrameInfoSet, const SCHAR* strSatDom, ImgPtSet &frmPts, ImgPtSet &SatPts, LocalBySeqImgOpts stuLocalBySeqOpts, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{
    vector<StereoMatchPt> vecMPts;

    CmlPtsManage clsPtManage;
    clsPtManage.GetPairPts( frmPts, SatPts, vecMPts );

    CmlGeoRaster clsImgRover, clsImgSat;
    bool bIsLoadSat = clsImgSat.LoadGeoFile( strSatDom );
    bool bIsLoadLand = clsImgRover.LoadGeoFile( FrameInfoSet.strImgPath.c_str() );

    if( vecMPts.size() < 3 )
    {
        if( ( true == bIsLoadLand )&&( true == bIsLoadSat ) )
        {

            CmlStereoProc clsStereoProc;
            clsStereoProc.Match2LargeImg( FrameInfoSet.strImgPath.c_str(), strSatDom, vecMPts, stuLocalBySeqOpts.stuSiftPara, stuLocalBySeqOpts.stuRANSACPara, stuLocalBySeqOpts.dZoomCoef );

            if( frmPts.imgInfo.nImgIndex == 0 )
            {
                frmPts.imgInfo.nImgIndex = 1001;
            }
            if( SatPts.imgInfo.nImgIndex == 0 )
            {
                SatPts.imgInfo.nImgIndex = 1002;
            }
            CmlPtsManage clsPtManage;
            clsPtManage.SplitPairPts( frmPts.imgInfo, SatPts.imgInfo, vecMPts, frmPts, SatPts );

        }
        else
        {
            return false;
        }
    }
    UINT nRanPtNum = vecMPts.size();

    CmlMat matA, matL, matX;
    matA.Initial( nRanPtNum*2, 4 );
    matL.Initial( nRanPtNum*2, 1 );

    for( UINT i = 0; i < vecMPts.size(); ++i )
    {
        StereoMatchPt pt = vecMPts[i];
        matA.SetAt( 2*i, 0, pt.ptLInImg.X );
        matA.SetAt( 2*i, 1, -1.0*pt.ptLInImg.Y );
        matA.SetAt( 2*i, 2, 1 );
        matA.SetAt( 2*i, 3, 0 );

        matA.SetAt( 2*i+1, 0, pt.ptLInImg.Y );
        matA.SetAt( 2*i+1, 1, pt.ptLInImg.X );
        matA.SetAt( 2*i+1, 2, 0 );
        matA.SetAt( 2*i+1, 3, 1 );

        matL.SetAt( 2*i, 0, pt.ptRInImg.X );
        matL.SetAt( 2*i+1, 0, pt.ptRInImg.Y );

    }

    mlMatSolveSVD( &matA, &matL, &matX );

//    UINT nRoverImgW = clsImgRover.GetWidth();
//    UINT nRoverImgH = clsImgRover.GetHeight();

    DOUBLE dx = FrameInfoSet.inOri.x;
    DOUBLE dy = FrameInfoSet.inOri.y;

    Pt3d ptRes;

    ptRes.X = matX.GetAt( 0,0 )*dx - matX.GetAt( 1, 0 )*dy + matX.GetAt( 2, 0 );
    ptRes.Y = matX.GetAt( 1,0 )*dx + matX.GetAt( 0, 0 )*dy + matX.GetAt( 3, 0 );

    ptRes.X = clsImgSat.m_PtOrigin.X + clsImgSat.m_dXResolution * ptRes.X;
    ptRes.Y = clsImgSat.m_PtOrigin.Y + clsImgSat.m_dYResolution * ptRes.Y;

    ptLocalRes.X = ptRes.X;
    ptLocalRes.Y = ptRes.Y;

    ///////////////////////////////////////////////////////////////
    DOUBLE dXALL, dYALL;
    dXALL = dYALL = 0.0;

    for( UINT i = 0; i < vecMPts.size(); ++i )
    {
        StereoMatchPt pt = vecMPts[i];
        DOUBLE dTempX = pt.ptLInImg.X * matX.GetAt( 0, 0 ) - pt.ptLInImg.Y * matX.GetAt( 1, 0 ) + matX.GetAt( 2, 0 );
        DOUBLE dTempY = pt.ptLInImg.Y * matX.GetAt( 1, 0 ) + pt.ptLInImg.Y * matX.GetAt( 0, 0 ) + matX.GetAt( 3, 0 );

        dXALL += ( dTempX - pt.ptRInImg.X )*( dTempX - pt.ptRInImg.X );
        dYALL += ( dTempY - pt.ptRInImg.Y )*( dTempY - pt.ptRInImg.Y );
    }
    dLocalAccuracy = sqrt( ( dXALL + dYALL ) / vecMPts.size() );
    ///////////////////////////////////////////////////////////////
    return true;

}

/**
* @fn CalInterPts
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算两个圆的交点
* @param cir1 圆心坐标和半径
* @param cir2 圆心坐标和半径
* @param pt1 交点1的坐标
* @param pt1 交点2的坐标
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CalInterPts(Pt3d &cir1, Pt3d &cir2, Pt2d &pt1,Pt2d &pt2)
{
    if(((cir1.X - cir2.X)*(cir1.X - cir2.X)+(cir1.Y - cir2.Y)*(cir1.Y - cir2.Y)) > (cir1.Z + cir2.Z)*(cir1.Z + cir2.Z))
    {
        printf("The two circles have no intersection points!\n");
        return false;
    }
    else if( ( fabs( cir1.X - cir2.X) ) < ML_ZERO )
    {
        pt1.Y=((cir1.Z*cir1.Z-cir2.Z*cir2.Z)/(cir2.Y-cir1.Y)+cir1.Y+cir2.Y)/2;
        pt2.Y=pt1.Y;
        pt1.X=sqrt(cir1.Z*cir1.Z-(pt1.Y-cir1.Y)*(pt1.Y-cir1.Y))+cir1.X;
        pt2.X=-sqrt(cir1.Z*cir1.Z-(pt1.Y-cir1.Y)*(pt1.Y-cir1.Y))+cir1.X;
    }
    else if( fabs( cir1.Y - cir2.Y) < ML_ZERO )
    {
        pt1.X= ((cir1.Z*cir1.Z-cir2.Z*cir2.Z)/(cir2.X-cir1.X)+cir1.X+cir2.X)/2;
        pt2.X=pt1.X;
        pt1.Y=sqrt(cir1.Z*cir1.Z-(pt1.X-cir1.X)*(pt1.X-cir1.X))+cir1.Y;
        pt2.Y=-sqrt(cir1.Z*cir1.Z-(pt1.X-cir1.X)*(pt1.X-cir1.X))+cir1.Y;
    }
    else if(fabs( ((cir1.X - cir2.X)*(cir1.X - cir2.X)+(cir1.Y - cir2.Y)*(cir1.Y - cir2.Y)) - (cir1.Z - cir2.Z) ) < ML_ZERO )
    {
        pt1.X=(cir1.X+cir2.X*cir1.Z/cir2.Z)/(1+cir1.Z/cir2.Z);
        pt1.Y=(cir1.Y+cir2.Y*cir1.Z/cir2.Z)/(1+cir1.Z/cir2.Z);
        pt2.X=pt1.X;
        pt2.Y=pt1.Y;
    }
    else
    {
        DOUBLE L=sqrt((cir1.X-cir2.X)*(cir1.X-cir2.X)+(cir1.Y-cir2.Y)*(cir1.Y-cir2.Y));
        DOUBLE K1=(cir1.Y-cir2.Y)/(cir1.X-cir2.X) ;
        DOUBLE K2=-1/K1 ;
        DOUBLE X0=cir2.X+(cir1.X-cir2.X)*(cir2.Z*cir2.Z-cir1.Z*cir1.Z+L*L)/(2*L*L) ;
        DOUBLE Y0=cir2.Y+K1*(X0-cir2.X);
        DOUBLE Z0=cir2.Z*cir2.Z-(X0-cir2.X)*(X0-cir2.X)-(Y0-cir2.Y)*(Y0-cir2.Y) ;

        pt1.X=X0-sqrt(Z0/(1+K2*K2)) ;
        pt1.Y=Y0+K2*(pt1.X-X0) ;
        pt2.X=X0+sqrt(Z0/(1+K2*K2)) ;
        pt2.Y=Y0+K2*(pt2.X-X0);
    }
    return true;
}

/**
* @fn LocalByBundleResection
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 多片后方交会实现定位
* @param vecGCPs 控制点坐标
* @param vecImgPtSets 像点坐标
* @param ptLocalRes 定位结果
* @param dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::LocalByBundleResection( vector<Pt3d> vecGCPs, vector< ImgPtSet > &vecImgPtSets,  Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{
    vector<DOUBLE> vecAngle;
    vector<Pt3d> vecGcppt;
    if( 0 != vecGCPs.size() )
    {
        for( UINT i = 0; i < vecImgPtSets.size(); ++i )
        {
            map< ULONG, Pt2d > mapPt;
            FrameImgInfo frameInfo = vecImgPtSets[i].imgInfo;
            if( true == this->loadMarkedPtFile( vecImgPtSets[i], mapPt, frameInfo ) )
            {
                for( UINT i = 0; i < vecGCPs.size(); ++i )
                {
                    Pt3d pt = vecGCPs.at(i);
                    map<ULONG, Pt2d>::iterator it =  mapPt.find( pt.lID );
                    if( it != mapPt.end() )
                    {
                        Pt2d ptUnDis;
                        ptUnDis.X = (it->second).X - frameInfo.inOri.x;
                        ptUnDis.Y = -1.0* ((it->second).Y - frameInfo.inOri.y);
                        CmlMat Rmat;
                        OPK2RMat( &frameInfo.exOri.ori, &Rmat );
                        Pt2d ptNew;
                        ptNew.X = ptUnDis.X * Rmat.GetAt(0,0) + ptUnDis.Y * Rmat.GetAt(0,1) - frameInfo.inOri.f * Rmat.GetAt(0,2);
                        ptNew.Y = ptUnDis.X * Rmat.GetAt(1,0) + ptUnDis.Y * Rmat.GetAt(1,1) - frameInfo.inOri.f * Rmat.GetAt(1,2);
                        DOUBLE dAngle = atan2( ptNew.Y,  ptNew.X );
                        dAngle += 4*ML_PI;
                        SINT nT = SINT( dAngle / (2*ML_PI) );
                        dAngle = dAngle - nT*( 2*ML_PI);

                        vecAngle.push_back( dAngle );
                        vecGcppt.push_back( pt );
                    }
                }
            }
        }
        if( vecAngle. size() < 3 )
        {
            return false;
        }
        else
        {
            vector< PtList > vecCir;
            for( UINT i = 0; i < ( vecAngle.size() - 1 ); ++i )
            {
                for( UINT j = i+1; j < vecAngle.size(); ++j )
                {
                    if( i == j )
                    {
                        continue;
                    }
                    else
                    {
                        Pt3d pt1, pt2;
                        pt1 = vecGCPs[i];
                        pt2 = vecGCPs[j];

                        DOUBLE dAngleT = vecAngle[j] - vecAngle[i];
                        if( ( abs( dAngleT - SINT( (dAngleT + 10e-6)/ ML_PI ) * ML_PI ) < ML_ZERO )||
                                ( abs( dAngleT - SINT( (dAngleT - 10e-6) / ML_PI ) * ML_PI ) < ML_ZERO ))
                        {
                            continue;
                        }

                        bool bFlag = true;
                        if( ( dAngleT < 0 ) )
                        {
                            bFlag = !(bFlag);
                        }
                        if( SINT( abs(dAngleT) / (0.5*ML_PI) ) % 2  == 1 )
                        {
                            bFlag = !(bFlag);
                        }

                        DOUBLE dXT = pt2.X - pt1.X;
                        DOUBLE dYT = pt2.Y - pt1.Y;

                        DOUBLE dTA = atan2( dYT, dXT );
                        dTA += ML_PI / 2.0;
                        Pt2d ptcent;
                        Pt3d ptcent1;
                        ptcent.X = ( pt2.X + pt1.X ) / 2.0;
                        ptcent.Y = ( pt2.Y + pt1.Y ) / 2.0;
                        DOUBLE dXRatio = cos(dTA);
                        DOUBLE dYRatio = sin(dTA);
                        if( bFlag == false )
                        {
                            dXRatio *= -1.0;
                            dYRatio *= -1.0;
                        }

                        DOUBLE dHalfL = 0.5*sqrt( dXT*dXT + dYT*dYT );
                        ptcent1.Z = abs( dHalfL / sin(abs(dAngleT)) );

                        DOUBLE dTDis = sqrt( ptcent1.Z * ptcent1.Z - dHalfL * dHalfL );
                        ptcent1.X = dTDis * dXRatio + ptcent.X;
                        ptcent1.Y = dTDis * dYRatio + ptcent.Y;

                        PtList ptListTemp;
                        ptListTemp.CirInfo = ptcent1;
                        ptListTemp.pt1 = pt1;
                        ptListTemp.pt2 = pt2;
                        vecCir.push_back( ptListTemp );
                    }
                }
            }
            vector<Pt2d> vecCents;
            for( UINT i = 0; i < ( vecCir.size() - 1 ); ++i )
            {
                for( UINT j = i + 1; j < vecCir.size(); ++j )
                {
                    if( ( ( abs( vecCir[i].pt1.X - vecCir[j].pt1.X) < ML_ZERO )&&
                            (  abs( vecCir[i].pt1.Y - vecCir[j].pt1.Y) < ML_ZERO ) ) ||
                            ( ( abs( vecCir[i].pt1.X - vecCir[j].pt2.X) < ML_ZERO )&&
                              (  abs( vecCir[i].pt1.Y - vecCir[j].pt2.Y) < ML_ZERO ) ) ||
                            ( ( abs( vecCir[i].pt2.X - vecCir[j].pt1.X) < ML_ZERO )&&
                              (  abs( vecCir[i].pt2.Y - vecCir[j].pt1.Y) < ML_ZERO ) ) ||
                            ( ( abs( vecCir[i].pt2.X - vecCir[j].pt2.X) < ML_ZERO )&&
                              (  abs( vecCir[i].pt2.Y - vecCir[j].pt2.Y) < ML_ZERO ) ) )
                    {
                        Pt2d pt1, pt2;
                        if( true == CalInterPts( vecCir[i].CirInfo, vecCir[j].CirInfo, pt1, pt2 ) )
                        {
                            bool bTempFlag = true;
                            for( UINT k = 0; k < vecGcppt.size(); ++k )
                            {
                                DOUBLE dTempDistance = sqrt( (pt1.X - vecGcppt[k].X)*( pt1.X - vecGcppt[k].X) + (pt1.Y - vecGcppt[k].Y)*( pt1.Y - vecGcppt[k].Y) );
                                if( dTempDistance < 10e-1 )
                                {
                                    bTempFlag = false;
                                }
                            }
                            if( bTempFlag == true )
                            {
                                vecCents.push_back( pt1 );
                            }
                            else
                            {
                                vecCents.push_back( pt2 );
                            }

                        }
                    }

                }
            }
            if( vecCents.size() == 0 )
            {
                return false;
            }
            DOUBLE dXAll, dYAll;
            dXAll = dYAll = 0.0;
            for( UINT i = 0; i < vecCents.size(); ++i )
            {
                dXAll += vecCents[i].X;
                dYAll += vecCents[i].Y;
            }
            dXAll /= vecCents.size();
            dYAll /= vecCents.size();

            ptLocalRes.X = dXAll;
            ptLocalRes.Y = dYAll;
            ptLocalRes.Z = 0.0;

            DOUBLE dResidual = 0.0;
            for( UINT i = 0; i < vecCents.size(); ++i )
            {
                DOUBLE dTempResX = vecCents[i].X - ptLocalRes.X;
                DOUBLE dTempResY = vecCents[i].Y - ptLocalRes.Y;

                dResidual += dTempResX * dTempResX + dTempResY * dTempResY;
            }
            dLocalAccuracy = sqrt( dResidual / vecCents.size() );

            return true;
        }
    }
    else
    {
        return false;
    }

}
/**
* @fn LocalBySImgIntersection
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 单片后方交会实现定位(至少4点)
* @param[in] vecGCPs 控制点坐标
* @param[in] vecImgPtSets 像点坐标(像平面坐标系)
* @param[out] exOriRes 定位后的外方位元素
* @param[out] vecRMSRes 点误差
* @param[out] dTotalRMS 总误差
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::LocalBySImgIntersection( vector<Pt3d> vecGCPs, ImgPtSet imgPts,  ExOriPara &exOriRes, vector<RMS2d> &vecRMSRes, DOUBLE &dTotalRMS  )
{
    vector<Pt2d> vecReal2d;
    vector<Pt3d> vecReal3d;

    for( UINT i = 0; i < imgPts.vecPts.size(); ++i )
    {
        Pt2d ptCur = imgPts.vecPts.at(i);
        for( UINT j = 0; j < vecGCPs.size(); ++j )
        {
            Pt3d ptCurGcp = vecGCPs[j];
            if( ptCur.lID == ptCurGcp.lID )
            {
                vecReal2d.push_back( ptCur );
                vecReal3d.push_back( ptCurGcp );
                break;
            }
        }
    }
    for( UINT i = 0; i < imgPts.vecAddPts.size(); ++i )
    {
        Pt2d ptCur = imgPts.vecAddPts.at(i);
        for( UINT j = 0; j < vecGCPs.size(); ++j )
        {
            Pt3d ptCurGcp = vecGCPs[j];
            if( ptCur.lID == ptCurGcp.lID )
            {
                vecReal2d.push_back( ptCur );
                vecReal3d.push_back( ptCurGcp );
                break;
            }
        }
    }
    //////////////////////////////////////////////////////////////
    CmlPhgProc clsPhProc;

    if( vecReal2d.size() < 4 )
    {
        return false;
    }

    vector<Pt2d> vecInital2d;
    vector<Pt3d> vecInital3d;

//    UINT nOff = 0;
//    for( UINT i = 0; i < vecReal2d.size(); ++i )
//    {
//        if( nOff < 4 )
//        {
//            vecInital2d.push_back( vecReal2d[i]);
//            vecInital3d.push_back( vecReal3d[i]);
//            ++nOff;
//        }
//    }
    vector<Pt2d> vecConvexPts;
    clsPhProc.CalcConvexHull( vecReal2d, vecConvexPts );
    if( vecConvexPts.size() ==3 )
    {
        vecInital2d = vecConvexPts;
        for( UINT i = 0; i < vecReal2d.size(); ++i )
        {
            bool bFlag = false;
            for( UINT j = 0; j < vecConvexPts.size(); ++j )
            {
                if( vecReal2d[i].lID == vecConvexPts[j].lID )
                {
                    bFlag = true;
                }
            }
            if( false == bFlag )
            {
                vecInital2d.push_back( vecReal2d[i]);
                break;
            }
        }
    }
    else
    {
        DOUBLE dT = DOUBLE(vecConvexPts.size() )/ 4.0;
        dT += 0.0001;
        vecInital2d.push_back( vecConvexPts[0]);
        vecInital2d.push_back( vecConvexPts[SINT(dT)] );
        vecInital2d.push_back( vecConvexPts[2*SINT(dT)]);
        vecInital2d.push_back( vecConvexPts[3*SINT(dT)]);
    }
    for( UINT i = 0; i < vecInital2d.size(); ++i )
    {
        Pt2d ptImgCur = vecInital2d[i];
        for( UINT j = 0; j < vecReal3d.size(); ++j )
        {
            Pt3d pt3dCur = vecReal3d[j];
            if( ptImgCur.lID == pt3dCur.lID )
            {
                vecInital3d.push_back( pt3dCur );
                break;
            }
        }
    }

    /////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////

    DOUBLE dFocalLength = imgPts.imgInfo.inOri.f;

    ExOriPara exOriInit;
    if( false == clsPhProc.mlResectionNoInitalVal( vecInital3d, vecInital2d, dFocalLength, exOriInit ) )
    {
       return false;
    }

    if( false == clsPhProc.mlBackForwardinterSection( vecReal2d,vecReal3d, dFocalLength, &exOriInit, &exOriRes ) )
    {
        return false;
    }

    DOUBLE dAll = 0;
    for( UINT i = 0; i < vecReal2d.size(); ++i )
    {
        Pt3d pt3dCur = vecReal3d.at(i);
        Pt2d pt2dCur = vecReal2d.at(i);

        RMS2d rmsError;
        clsPhProc.mlCalcResidualError( pt3dCur, pt2dCur, exOriRes, dFocalLength, dFocalLength, rmsError );
        vecRMSRes.push_back( rmsError );
        dAll += rmsError.rmsAll * rmsError.rmsAll;
    }
    dAll /= vecReal2d.size();
    dTotalRMS = sqrt( dAll );

    return true;
}

/**
* @fn LocalByLander
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 利用地标的定位
* @param vecFSite 站点所有影像
* @param strGCPFile 地标的坐标
* @param ptLocalRes 定位结果
* @param dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::LocalByLander( const vector<StereoSet> &vecFSite, const SCHAR* strGCPFile, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{
    return false;






}
/**
* @fn GetImgIDIn2Site
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 获得影像的ID号
* @param vecFSite 前站影像
* @param vecESite 后站影像
* @param nFID 前站影像的ID号
* @param nEID 后站影像的ID号
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::GetImgIDIn2Site(  const vector<StereoSet> &vecFSite, const vector<StereoSet> &vecESite, UINT &nFID, UINT &nEID )
{
    DOUBLE dMinAngle = -99999;
    SINT nF = -1;
    SINT nE = -1;

    DOUBLE dCamH = 1.56;

    DOUBLE dXFSite, dYFSite, dZFSite, dXESite, dYESite, dZESite;
    dXFSite = dYFSite = dXESite = dYESite = dZFSite = dZESite = 0.0;
    for( UINT i = 0; i < vecFSite.size(); ++i )
    {
        StereoSet tmpSet = vecFSite[i];
        StereoSet* pSet = &tmpSet;
        dXFSite += pSet->imgLInfo.exOri.pos.X + pSet->imgRInfo.exOri.pos.X;
        dYFSite += pSet->imgLInfo.exOri.pos.Y + pSet->imgRInfo.exOri.pos.Y;
        dZFSite += pSet->imgLInfo.exOri.pos.Z + pSet->imgRInfo.exOri.pos.Z;
    }
    dXFSite /= ( vecFSite.size() * 2.0 );
    dYFSite /= ( vecFSite.size() * 2.0 );
    dZFSite /= ( vecFSite.size() * 2.0 );

    for( UINT i = 0; i < vecESite.size(); ++i )
    {
        StereoSet tmpSet = vecESite[i];
        StereoSet* pSet = &tmpSet;
        dXESite += pSet->imgLInfo.exOri.pos.X + pSet->imgRInfo.exOri.pos.X;
        dYESite += pSet->imgLInfo.exOri.pos.Y + pSet->imgRInfo.exOri.pos.Y;
        dZESite += pSet->imgLInfo.exOri.pos.Z + pSet->imgRInfo.exOri.pos.Z;
    }
    dXESite /= ( vecESite.size() * 2.0 );
    dYESite /= ( vecESite.size() * 2.0 );
    dZESite /= ( vecESite.size() * 2.0 );
    DOUBLE dXVec, dYVec, dZVec;
    dXVec = dYVec = dZVec = 0.0;

    dXVec = dXESite - dXFSite;
    dYVec = dYESite - dYFSite;
    dZVec = dZESite - dZFSite + dCamH;

    DOUBLE dXYTempAll = sqrt( dXVec * dXVec + dYVec * dYVec + dZVec * dZVec );

    DOUBLE dThres = 1;

    if( fabs( dXYTempAll ) < dThres )
    {
        for( UINT i = 0; i < vecFSite.size(); ++i )
        {
            ExOriPara ExOri = vecFSite.at(i).imgLInfo.exOri;
            CmlMat MatRF;
            OPK2RMat( &ExOri.ori, &MatRF );
            Pt3d ptXYZF;
            ptXYZF.X = -1.0 * MatRF.GetAt( 0, 2 );
            ptXYZF.Y = -1.0 * MatRF.GetAt( 1, 2 );
            ptXYZF.Z = -1.0 * MatRF.GetAt( 2, 2 );

            for( UINT j = 0; j < vecESite.size(); ++j )
            {
                ExOriPara ExOriE = vecESite.at(j).imgLInfo.exOri;
                CmlMat matRE;
                OPK2RMat( &ExOriE.ori, &matRE );
                Pt3d ptXYZE;
                ptXYZE.X = -1.0 * matRE.GetAt( 0, 2 );
                ptXYZE.Y = -1.0 * matRE.GetAt( 1, 2 );
                ptXYZE.Z = -1.0 * matRE.GetAt( 2, 2 );

                DOUBLE dAngleTemp = ptXYZF.X * ptXYZE.X + ptXYZF.Y * ptXYZE.Y + ptXYZF.Z * ptXYZE.Z;

                if( dAngleTemp > dMinAngle )
                {
                    nF = i;
                    nE = j;
                    dMinAngle = dAngleTemp;
                }
            }

        }
        nFID = nF;
        nEID = nE;
        return true;

    }
    else
    {
        dXVec /= dXYTempAll;
        dYVec /= dXYTempAll;
        dZVec /= dXYTempAll;

        Pt3d dFVec;

        for( UINT i = 0; i < vecFSite.size(); ++i )
        {
            ExOriPara ExOri = vecFSite.at(i).imgLInfo.exOri;
            CmlMat MatR;
            OPK2RMat( &ExOri.ori, &MatR );
            Pt3d ptXYZ;
            ptXYZ.X = -1.0 * MatR.GetAt( 0, 2 );
            ptXYZ.Y = -1.0 * MatR.GetAt( 1, 2 );
            ptXYZ.Z = -1.0 * MatR.GetAt( 2, 2 );

            DOUBLE dAngleTemp = ptXYZ.X * dXVec + ptXYZ.Y * dYVec + ptXYZ.Z * dZVec ;
            if( dAngleTemp > dMinAngle )
            {
                nF = i;
                dMinAngle = dAngleTemp;
                dFVec = ptXYZ;
            }
        }
        //----------------------------------------------
        ExOriPara exOriTmp = vecFSite[nF].imgLInfo.exOri;
        CmlMat MatR;
        OPK2RMat( &exOriTmp.ori, &MatR );
        Pt3d ptXYZ;
        ptXYZ.X = -1.0*MatR.GetAt( 0, 2 );
        ptXYZ.Y = -1.0*MatR.GetAt( 1, 2 );
        ptXYZ.Z = -1.0*MatR.GetAt( 2, 2 );

        DOUBLE dDis = dMinAngle * dXYTempAll;
        dDis += 3.5;
        Pt3d ptXYZTmp;
        ptXYZTmp.X = ptXYZ.X * dDis + dXFSite;
        ptXYZTmp.Y = ptXYZ.Y * dDis + dYFSite;
        ptXYZTmp.Z = dCamH + dZFSite;

        Pt3d ptVecXYZNew;
        ptVecXYZNew.X = ptXYZTmp.X - dXESite;
        ptVecXYZNew.Y = ptXYZTmp.Y - dYESite;
        ptVecXYZNew.Z = ptXYZTmp.Z - dZESite;


        //----------------------------------------------

        dMinAngle = -99999;
        for( UINT i = 0; i < vecESite.size(); ++i )
        {
            ExOriPara ExOri = vecESite.at(i).imgLInfo.exOri;
            CmlMat MatR;
            OPK2RMat( &ExOri.ori, &MatR );
            Pt3d ptXYZ;
            ptXYZ.X = -1.0 * MatR.GetAt( 0, 2 );
            ptXYZ.Y = -1.0 * MatR.GetAt( 1, 2 );
            ptXYZ.Z = -1.0 * MatR.GetAt( 2, 2 );

            DOUBLE dAngleTemp = ptXYZ.X * dFVec.X + ptXYZ.Y * dFVec.Y + ptXYZ.Z * dFVec.Z ;
            if( dAngleTemp > dMinAngle )
            {
                nE = i;
                dMinAngle = dAngleTemp;
            }
        }

        if( ( nF == -1 )||( nE == -1 ) )
        {
            return false;
        }
        nEID = nE;
        nFID = nF;
        return true;
    }


}

//----------------------------------------
bool CmlLocalization::GetImgIDIn2SiteReverse(  const vector<StereoSet> &vecFSite, const vector<StereoSet> &vecESite, UINT &nFID, UINT &nEID )
{
    DOUBLE dMaxAngle = 99999;
    SINT nF = -1;
    SINT nE = -1;

    DOUBLE dCamH = 1.56;

    DOUBLE dXFSite, dYFSite, dZFSite, dXESite, dYESite, dZESite;
    dXFSite = dYFSite = dXESite = dYESite = dZFSite = dZESite = 0.0;
    for( UINT i = 0; i < vecFSite.size(); ++i )
    {
        StereoSet tmpSet = vecFSite[i];
        StereoSet* pSet = &tmpSet;
        dXFSite += pSet->imgLInfo.exOri.pos.X + pSet->imgRInfo.exOri.pos.X;
        dYFSite += pSet->imgLInfo.exOri.pos.Y + pSet->imgRInfo.exOri.pos.Y;
        dZFSite += pSet->imgLInfo.exOri.pos.Z + pSet->imgRInfo.exOri.pos.Z;
    }
    dXFSite /= ( vecFSite.size() * 2.0 );
    dYFSite /= ( vecFSite.size() * 2.0 );
    dZFSite /= ( vecFSite.size() * 2.0 );

    for( UINT i = 0; i < vecESite.size(); ++i )
    {
        StereoSet tmpSet = vecESite[i];
        StereoSet* pSet = &tmpSet;
        dXESite += pSet->imgLInfo.exOri.pos.X + pSet->imgRInfo.exOri.pos.X;
        dYESite += pSet->imgLInfo.exOri.pos.Y + pSet->imgRInfo.exOri.pos.Y;
        dZESite += pSet->imgLInfo.exOri.pos.Z + pSet->imgRInfo.exOri.pos.Z;
    }
    dXESite /= ( vecESite.size() * 2.0 );
    dYESite /= ( vecESite.size() * 2.0 );
    dZESite /= ( vecESite.size() * 2.0 );
    DOUBLE dXVec, dYVec, dZVec;
    dXVec = dYVec = dZVec = 0.0;

    dXVec = dXESite - dXFSite;
    dYVec = dYESite - dYFSite;
    dZVec = dZESite - dZFSite + dCamH;

    DOUBLE dXYTempAll = sqrt( dXVec * dXVec + dYVec * dYVec + dZVec * dZVec );

    DOUBLE dThres = 3;

    if( fabs( dXYTempAll ) < dThres )
    {
        for( UINT i = 0; i < vecFSite.size(); ++i )
        {
            ExOriPara ExOri = vecFSite.at(i).imgLInfo.exOri;
            CmlMat MatRF;
            OPK2RMat( &ExOri.ori, &MatRF );
            Pt3d ptXYZF;
            ptXYZF.X = -1.0 * MatRF.GetAt( 0, 2 );
            ptXYZF.Y = -1.0 * MatRF.GetAt( 1, 2 );
            ptXYZF.Z = -1.0 * MatRF.GetAt( 2, 2 );

            for( UINT j = 0; j < vecESite.size(); ++j )
            {
                ExOriPara ExOriE = vecESite.at(j).imgLInfo.exOri;
                CmlMat matRE;
                OPK2RMat( &ExOriE.ori, &matRE );
                Pt3d ptXYZE;
                ptXYZE.X = -1.0 * matRE.GetAt( 0, 2 );
                ptXYZE.Y = -1.0 * matRE.GetAt( 1, 2 );
                ptXYZE.Z = -1.0 * matRE.GetAt( 2, 2 );

                DOUBLE dAngleTemp = ptXYZF.X * ptXYZE.X + ptXYZF.Y * ptXYZE.Y + ptXYZF.Z * ptXYZE.Z;

                if( dAngleTemp < dMaxAngle )
                {
                    nF = i;
                    nE = j;
                    dMaxAngle = dAngleTemp;
                }
            }

        }
        nFID = nF;
        nEID = nE;
        return true;

    }
    else
    {
        DOUBLE dMinAngle = -99999;
        dXVec /= dXYTempAll;
        dYVec /= dXYTempAll;
        dZVec /= dXYTempAll;

        Pt3d dFVec;

        for( UINT i = 0; i < vecFSite.size(); ++i )
        {
            ExOriPara ExOri = vecFSite.at(i).imgLInfo.exOri;
            CmlMat MatR;
            OPK2RMat( &ExOri.ori, &MatR );
            Pt3d ptXYZ;
            ptXYZ.X = -1.0 * MatR.GetAt( 0, 2 );
            ptXYZ.Y = -1.0 * MatR.GetAt( 1, 2 );
            ptXYZ.Z = -1.0 * MatR.GetAt( 2, 2 );

            DOUBLE dAngleTemp = ptXYZ.X * dXVec + ptXYZ.Y * dYVec + ptXYZ.Z * dZVec ;
            if( dAngleTemp > dMinAngle )
            {
                nF = i;
                dMinAngle = dAngleTemp;
                dFVec = ptXYZ;
            }
        }
        //----------------------------------------------
        ExOriPara exOriTmp = vecFSite[nF].imgLInfo.exOri;
        CmlMat MatR;
        OPK2RMat( &exOriTmp.ori, &MatR );
        Pt3d ptXYZ;
        ptXYZ.X = -1.0*MatR.GetAt( 0, 2 );
        ptXYZ.Y = -1.0*MatR.GetAt( 1, 2 );
        ptXYZ.Z = -1.0*MatR.GetAt( 2, 2 );

        DOUBLE dDis = dMinAngle * dXYTempAll;
 //       dDis += 3.5;
        Pt3d ptXYZTmp;
        ptXYZTmp.X = ptXYZ.X * dDis + dXFSite;
        ptXYZTmp.Y = ptXYZ.Y * dDis + dYFSite;
        ptXYZTmp.Z = dCamH + dZFSite;

        Pt3d ptVecXYZNew;
        ptVecXYZNew.X = ptXYZTmp.X - dXESite;
        ptVecXYZNew.Y = ptXYZTmp.Y - dYESite;
        ptVecXYZNew.Z = ptXYZTmp.Z - dZESite;


        //----------------------------------------------

        dMinAngle = -99999;
        for( UINT i = 0; i < vecESite.size(); ++i )
        {
            ExOriPara ExOri = vecESite.at(i).imgLInfo.exOri;
            CmlMat MatR;
            OPK2RMat( &ExOri.ori, &MatR );
            Pt3d ptXYZ;
            ptXYZ.X = -1.0 * MatR.GetAt( 0, 2 );
            ptXYZ.Y = -1.0 * MatR.GetAt( 1, 2 );
            ptXYZ.Z = -1.0 * MatR.GetAt( 2, 2 );

            DOUBLE dAngleTemp = ptXYZ.X * dFVec.X + ptXYZ.Y * dFVec.Y + ptXYZ.Z * dFVec.Z ;
            if( dAngleTemp > dMinAngle )
            {
                nE = i;
                dMinAngle = dAngleTemp;
            }
        }

        if( ( nF == -1 )||( nE == -1 ) )
        {
            return false;
        }
        nEID = nE;
        nFID = nF;
        return true;
    }


}
/**
* @fn removePtsByDisConsistant
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 通过视差不一致性去掉错误匹配点
* @param vecPtsInFrontFrm 前帧影像获取的三维点坐标
* @param ptFOrig 起始帧三维坐标
* @param vecPtsInEndFrm 最后帧影像获取的三维点坐标
* @param ptEOrig 结束帧三维坐标
* @param dThreCoef 相关系数阈值
* @param vecFlags 标识
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::removePtsByDisConsistant( vector<Pt3d> vecPtsInFrontFrm, Pt3d ptFOrig, vector<Pt3d> vecPtsInEndFrm, Pt3d ptEOrig, DOUBLE dDisThreCoef, DOUBLE dWeightThreCoef, vector<bool> &vecFlags )
{
    vecFlags.clear();
    vector< UINT > vecWVal;

    for( UINT i = 0; i < vecPtsInFrontFrm.size(); ++i )
    {
        vecWVal.push_back( UINT(0) );
    }

    for( UINT i = 0; i < vecPtsInFrontFrm.size(); ++i )
    {
        vector<DOUBLE > vecFTmp, vecETmp;
        for( UINT j = 0; j < vecPtsInFrontFrm.size(); ++j )
        {
            if( j != i )
            {
                DOUBLE dDisInF =  DisIn2Pts( vecPtsInFrontFrm[i], vecPtsInFrontFrm[j] );
                DOUBLE dDisInE =  DisIn2Pts( vecPtsInEndFrm[i], vecPtsInEndFrm[j] );

                DOUBLE dResidual = abs( dDisInE - dDisInF );

                DOUBLE dThres = dDisThreCoef * (  DisIn2Pts( ptFOrig, vecPtsInFrontFrm[i] ) + DisIn2Pts( ptFOrig, vecPtsInFrontFrm[j] ) + DisIn2Pts( ptEOrig, vecPtsInEndFrm[i] ) + DisIn2Pts( ptEOrig, vecPtsInEndFrm[j] )  );
                dThres /= 4.0;

                if( dResidual < dThres )
                {
                    UINT* pUWValA = &( vecWVal.at(i) );
                    UINT* pUWValB = &( vecWVal.at(j) );
                    *pUWValA = ++( vecWVal.at(i));
                    *pUWValB = ++( vecWVal.at(j));
                }
            }
        }
    }
    DOUBLE dMaxVal = -1;
    for( UINT i = 0; i < vecWVal.size(); ++i )
    {
        dMaxVal = ( vecWVal[i] > dMaxVal ) ? vecWVal[i] : dMaxVal;
    }


    for( UINT i = 0; i < vecWVal.size(); ++i )
    {
        if( vecWVal[i] < dMaxVal*dWeightThreCoef )
        {
            vecFlags.push_back( false );
        }
        else
        {
            vecFlags.push_back( true );
        }
    }
    return true;

}
bool CmlLocalization::getInsterestRegion( StereoSet sFirstSet, StereoSet sSecondSet, DOUBLE dCamHeight, DOUBLE dFirstSiteRange, DOUBLE dSecondSiteRange, vector<Pt2d> &vecL, vector<Pt2d> &vecR, SINT nNumTilts )
{
    DOUBLE dBaseLength = dFirstSiteRange - dSecondSiteRange;
    DOUBLE dZoomRatioFirst = 3;
    DOUBLE dZoomRatioSecond = 1;
    CmlGdalDataset clsGdalSetF;
    //---------------------------------
    CmlMat matRTmp;
    DOUBLE dfF = sFirstSet.imgLInfo.inOri.f;
    OriAngle oriF = sFirstSet.imgLInfo.exOri.ori;
    Pt3d ptPosL = sFirstSet.imgLInfo.exOri.pos;
    OPK2RMat( &oriF, &matRTmp );

    DOUBLE dVecFTmp[3];
    dVecFTmp[0] = -1.0*matRTmp.GetAt( 0, 2 );
    dVecFTmp[1] = -1.0*matRTmp.GetAt( 1, 2 );
    dVecFTmp[2] = -1.0*matRTmp.GetAt( 2, 2 );

    Pt3d ptXYZTmp, ptXYZTmp2;
    ptXYZTmp.X = sSecondSet.imgLInfo.exOri.pos.X - sFirstSet.imgLInfo.exOri.pos.X;
    ptXYZTmp.Y = sSecondSet.imgLInfo.exOri.pos.Y - sFirstSet.imgLInfo.exOri.pos.Y;
    ptXYZTmp.Z = sSecondSet.imgLInfo.exOri.pos.Z - sFirstSet.imgLInfo.exOri.pos.Z;

    DOUBLE dNewDis = ptXYZTmp.X * dVecFTmp[0] + ptXYZTmp.Y * dVecFTmp[1] + ptXYZTmp.Z * dVecFTmp[2];

    DOUBLE dSecondDis = 4.0;
    if( dNewDis > 0 )
    {
        DOUBLE dZoomTmp = ( dNewDis + dSecondDis ) / dSecondDis;
        if( dZoomTmp > dZoomRatioFirst )
        {
            dZoomTmp = dZoomRatioFirst;
        }
        else if( dZoomTmp < 1 )
        {
            dZoomTmp = 1;
        }
        dZoomRatioFirst = dZoomTmp;
    }
    //---------------------------------
    if( false == clsGdalSetF.LoadFile( sFirstSet.imgLInfo.strImgPath.c_str() ) )
    {
        return false;
    }

    CmlMat matRF;
    dfF = sFirstSet.imgLInfo.inOri.f;
    oriF = sFirstSet.imgLInfo.exOri.ori;
    OPK2RMat( &oriF, &matRF );

    DOUBLE dVecF[3];
    dVecF[0] = -1.0*matRF.GetAt( 0, 2 ) ;
    dVecF[1] = -1.0*matRF.GetAt( 1, 2 ) ;
    dVecF[2] = -1.0*matRF.GetAt( 2, 2 ) ;

    CmlMat matRE;
    DOUBLE dfE = sSecondSet.imgLInfo.inOri.f;
    OriAngle oriE = sSecondSet.imgLInfo.exOri.ori;
    Pt3d ptPosR = sSecondSet.imgLInfo.exOri.pos;
    OPK2RMat( &oriE, &matRE );

    DOUBLE dVecE[3];
    dVecE[0] = -1.0 * matRE.GetAt( 0, 2 );
    dVecE[1] = -1.0 * matRE.GetAt( 1, 2 );
    dVecE[2] = -1.0 * matRE.GetAt( 2, 2 );

    //------------

    DOUBLE dImgyMin = 0.0;
    DOUBLE dAnglePitch = asin(dVecF[2] );

    dImgyMin = dfF* tan(dAnglePitch);

    DOUBLE dYImgMINF = sFirstSet.imgLInfo.inOri.y - min( dImgyMin, sFirstSet.imgLInfo.inOri.y );

    DOUBLE dYImgMAXF = clsGdalSetF.GetHeight() - 1;

    DOUBLE dYImgMAXFTmp = sFirstSet.imgLInfo.inOri.y + ( sFirstSet.imgLInfo.inOri.f * tan( ML_PI*0.5 - dAnglePitch - atan(dFirstSiteRange / dCamHeight ) ) ) ;

    dYImgMAXF = min( dYImgMAXF, dYImgMAXFTmp );

    UINT nHTmpF = UINT(dYImgMAXF-dYImgMINF);

    //----------------------------
    CmlGdalDataset clsGdalSetE;
    if( false == clsGdalSetE.LoadFile( sSecondSet.imgLInfo.strImgPath.c_str() ) )
    {
        return false;
    }
    dImgyMin = 0.0;

    dAnglePitch = asin(dVecE[2]);
    dImgyMin = dfE * tan(dAnglePitch);

    DOUBLE dYImgMINE = sSecondSet.imgLInfo.inOri.y - min( dImgyMin, sSecondSet.imgLInfo.inOri.y );

    DOUBLE dYImgMAXE = clsGdalSetE.GetHeight() - 1;

    DOUBLE dYImgMAXETmp = sSecondSet.imgLInfo.inOri.y + ( sSecondSet.imgLInfo.inOri.f * tan( ML_PI * 0.5 - dAnglePitch - atan(dSecondSiteRange / dCamHeight ) ) ) ;

    dYImgMAXE = min(dYImgMAXE, dYImgMAXETmp );

    UINT nHTmpE = UINT(dYImgMAXE-dYImgMINE);

    //======================================
    DOUBLE dXYZLE[3], dXYZRE[3];
    DOUBLE dxLE = -1.0 * sSecondSet.imgLInfo.inOri.x;
    DOUBLE dxRE = dxLE + clsGdalSetE.GetWidth();

    dXYZLE[0] = matRE.GetAt( 0, 0 )*dxLE - matRE.GetAt( 0, 2 )*dfE;
    dXYZLE[1] = matRE.GetAt( 1, 0 )*dxLE - matRE.GetAt( 1, 2 )*dfE;
    dXYZLE[2] = matRE.GetAt( 2, 0 )*dxLE - matRE.GetAt( 2, 2 )*dfE;

    DOUBLE dTmpDisLE = sqrt( dXYZLE[0]*dXYZLE[0] + dXYZLE[1]*dXYZLE[1] + dXYZLE[2]*dXYZLE[2] );
    dXYZLE[0] /= dTmpDisLE;
    dXYZLE[1] /= dTmpDisLE;
    dXYZLE[2] /= dTmpDisLE;

    dXYZRE[0] = matRE.GetAt( 0, 0 )*dxRE - matRE.GetAt( 0, 2 )*dfE;
    dXYZRE[1] = matRE.GetAt( 1, 0 )*dxRE - matRE.GetAt( 1, 2 )*dfE;
    dXYZRE[2] = matRE.GetAt( 2, 0 )*dxRE - matRE.GetAt( 2, 2 )*dfE;

    DOUBLE dTmpDisRE = sqrt( dXYZRE[0]*dXYZRE[0] + dXYZRE[1]*dXYZRE[1] + dXYZRE[2]*dXYZRE[2] );
    dXYZRE[0] /= dTmpDisRE;
    dXYZRE[1] /= dTmpDisRE;
    dXYZRE[2] /= dTmpDisRE;

    DOUBLE dNearDis = 2;
    DOUBLE dFarDis = 2 + 8;

    Pt3d ptLENear, ptLEFar, ptRENear, ptREFar;
    ptLENear.X = dNearDis * dXYZLE[0] + ptPosR.X;
    ptLENear.Y = dNearDis * dXYZLE[1] + ptPosR.Y;
    ptLENear.Z = dNearDis * dXYZLE[2] + ptPosR.Z;

    ptLEFar.X = dFarDis * dXYZLE[0] + ptPosR.X;
    ptLEFar.Y = dFarDis * dXYZLE[1] + ptPosR.Y;
    ptLEFar.Z = dFarDis * dXYZLE[2] + ptPosR.Z;

    ptRENear.X = dNearDis*dXYZRE[0] + ptPosR.X;
    ptRENear.X = dNearDis*dXYZRE[1] + ptPosR.Y;
    ptRENear.X = dNearDis*dXYZRE[2] + ptPosR.Z;

    ptREFar.X = dFarDis*dXYZRE[0] + ptPosR.X;
    ptREFar.X = dFarDis*dXYZRE[1] + ptPosR.Y;
    ptREFar.X = dFarDis*dXYZRE[2] + ptPosR.Z;

    CmlPhgProc clsPhg;
    Pt2d ptLFNear, ptLFFar, ptRFNear, ptRFFar;
    clsPhg.mlReproject( &ptLFNear, &ptLENear, &sFirstSet.imgLInfo.exOri, dfF, dfF, &matRF );
    clsPhg.mlReproject( &ptLFFar, &ptLEFar, &sFirstSet.imgLInfo.exOri, dfF, dfF, &matRF );
    clsPhg.mlReproject( &ptRFNear, &ptRENear, &sFirstSet.imgLInfo.exOri, dfF, dfF, &matRF );
    clsPhg.mlReproject( &ptRFFar, &ptREFar, &sFirstSet.imgLInfo.exOri, dfF, dfF, &matRF );

    ptLFNear.X += sFirstSet.imgLInfo.inOri.x;
    ptLFFar.X += sFirstSet.imgLInfo.inOri.x;
    ptRFNear.X += sFirstSet.imgLInfo.inOri.x;
    ptRFFar.X += sFirstSet.imgLInfo.inOri.x;

    DOUBLE dLX = min( ptLFNear.X, ptLFFar.X );
    DOUBLE dRX = max( ptRFNear.X, ptRFFar.X );

    UINT nFStartX = 0;
    UINT nFEndX = clsGdalSetF.GetWidth() - 1;
    if( dLX < dRX )
    {
        if( dLX > 0 )
        {
            nFStartX = UINT(dLX);
        }
        if( (dLX > 0 )&&( dRX < nFEndX ))
        {
            nFEndX = UINT(dRX);
        }
    }

    //======================================

    //----------------------------
    CmlRasterBlock clsBlocksL, clsBlocksR;
    clsGdalSetF.GetRasterGrayBlock( UINT(1), UINT(0), UINT(dYImgMINF), (nFEndX-nFStartX), nHTmpF, dFirstSiteRange, &clsBlocksL );
    clsBlocksL.SetGDTType( GDT_Byte );
    CmlSatMapping clsSatProc;
    clsSatProc.ConstractAdjust( clsBlocksL );

    //------------------------------------------------------------------


    clsGdalSetE.GetRasterGrayBlock( UINT(1), UINT(0), UINT(dYImgMINE), clsGdalSetE.GetWidth(), nHTmpE, dSecondSiteRange, &clsBlocksR );
    clsBlocksR.SetGDTType( GDT_Byte );

    clsSatProc.ConstractAdjust( clsBlocksR );

    //========================================================
    DOUBLE *pLx, *pRx, *pLy, *pRy;
    pLx = pRx = pLy = pRy = NULL;

    SINT nNum = ASiftMatch( clsBlocksL.GetData(), clsBlocksL.GetW(), clsBlocksL.GetH(), clsBlocksR.GetData(), clsBlocksR.GetW(), clsBlocksR.GetH(),
                            pLx, pLy, pRx, pRy, nNumTilts );

    for( UINT i = 0; i < UINT(nNum); ++i )
    {
        Pt2d ptF, ptE;
        ptF.X = pLx[i] / dZoomRatioFirst + nFStartX;
        ptF.Y = pLy[i] / dZoomRatioFirst + dYImgMINF;
        ptE.X = pRx[i] / dZoomRatioSecond;
        ptE.Y = pRy[i] / dZoomRatioSecond + dYImgMINE;
        vecL.push_back( ptF );
        vecR.push_back( ptE );
    }
    freeASiftData( pLx );
    freeASiftData( pLy );
    freeASiftData( pRx );
    freeASiftData( pRy );
    return true;
}

/**
* @fn LocalInTwoSite
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 站点间影像定位
* @param vecFSite 前站点所有影像
* @param vecESite 后站点所有影像
* @param vecFrontPts 前站点所有匹配点
* @param vecEndPts 后站点所有匹配点
* @param stulocalBy2Opt 定位参数
* @param ptLocalRes 定位结果
* @param dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::LocalInTwoSite(  vector<StereoSet> vecFSite, vector<StereoSet> vecESite, vector<ImgPtSet> &vecFrontPts, vector<ImgPtSet> &vecEndPts, LocalBy2SitesOpts stulocalBy2Opt, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{

    UINT nF = -1;
    UINT nE = -1;
    if( false == GetImgIDIn2Site( vecFSite, vecESite, nF, nE ) )
    {
        return false;
    }
    CmlSatMapping clsSatProc;

    StereoSet stereoFS = vecFSite[nF];
    StereoSet stereoES = vecESite[nE];

    CmlMat matRE;
    DOUBLE dfE = stereoES.imgLInfo.inOri.f;
    OriAngle oriE = stereoES.imgLInfo.exOri.ori;
    OPK2RMat( &oriE, &matRE );

    DOUBLE dVecE[3];
    dVecE[0] = -1.0*matRE.GetAt( 0, 2 ) ;
    dVecE[1] = -1.0*matRE.GetAt( 1, 2 ) ;
    dVecE[2] = -1.0*matRE.GetAt( 2, 2 ) ;

    DOUBLE dImgyMin = 0.0;
    if( abs( matRE.GetAt( 2, 1 ) ) > ML_ZERO  )
    {
        dImgyMin =  dfE * matRE.GetAt( 2, 2 ) / matRE.GetAt( 2, 1 );
    }
    DOUBLE dYImgMINE = stereoES.imgLInfo.inOri.y - dImgyMin;

    DOUBLE dYImgMAXE = stereoES.imgLInfo.nH - 1;


    DOUBLE dAngle = acos(  dVecE[2] );
    dYImgMAXE = stereoES.imgLInfo.inOri.y + ( stereoES.imgLInfo.inOri.f * tan( dAngle - atan(stulocalBy2Opt.dESiteDis / stulocalBy2Opt.dCamH ) ) ) ;

    if( dYImgMAXE < dYImgMINE )
    {
        return false;
    }

    CmlRasterBlock clsRasterBE;
    CmlGdalDataset clsGdalSetE, clsGdalSetEOut;
    if( false == clsGdalSetE.LoadFile( stereoES.imgLInfo.strImgPath.c_str() ) )
    {
        return false;
    }
    UINT nHTmp = UINT(dYImgMAXE-dYImgMINE);

    clsGdalSetE.GetRasterGrayBlock( UINT(1), UINT(0), UINT(dYImgMINE), clsGdalSetE.GetWidth(), nHTmp, UINT(1), &clsRasterBE );
    clsRasterBE.SetGDTType( GDT_Byte );
    clsSatProc.ConstractAdjust( clsRasterBE );

//    clsGdalSetEOut.CreateFile( "/home/wan/tttL.bmp", clsRasterBE.GetW(), clsRasterBE.GetH(), UINT(1), GDT_Byte, "BMP" );
//    clsGdalSetEOut.SaveBlockToFile( UINT(1), UINT(0), UINT(0), &clsRasterBE );

    /////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////
    CmlMat matRF;
    DOUBLE dfF = stereoFS.imgLInfo.inOri.f;
    OriAngle oriF = stereoFS.imgLInfo.exOri.ori;
    OPK2RMat( &oriF, &matRF );

    DOUBLE dVecF[3];
    dVecF[0] = -1.0*matRF.GetAt( 0, 2 ) ;
    dVecF[1] = -1.0*matRF.GetAt( 1, 2 ) ;
    dVecF[2] = -1.0*matRF.GetAt( 2, 2 ) ;

    dImgyMin = 0.0;
    if( abs( matRF.GetAt( 2, 1 ) ) > ML_ZERO  )
    {
        dImgyMin =  dfF * matRF.GetAt( 2, 2 ) / matRF.GetAt( 2, 1 );
    }
    DOUBLE dYImgMINF = stereoFS.imgLInfo.inOri.y - dImgyMin;

    DOUBLE dYImgMAXF = stereoFS.imgLInfo.nH - 1;

    dAngle = acos(  dVecF[2] );
    dYImgMAXF = stereoFS.imgLInfo.inOri.y + ( stereoFS.imgLInfo.inOri.f * tan( dAngle - atan( 12.0 / 1.5 ) ) ) ;



    if( dYImgMAXF < dYImgMINF )
    {
        return false;
    }

    CmlRasterBlock clsRasterBF;
    CmlGdalDataset clsGdalSetF, clsGdalSetFOut;
    if( false == clsGdalSetF.LoadFile( stereoFS.imgLInfo.strImgPath.c_str() ) )
    {
        return false;
    }
    nHTmp = UINT(dYImgMAXF-dYImgMINF);

    clsGdalSetF.GetRasterGrayBlock( UINT(1), UINT(0), UINT(dYImgMINF), clsGdalSetF.GetWidth(), nHTmp, UINT(1), &clsRasterBF );

    //-----------------------------------
    clsRasterBF.SetGDTType( GDT_Byte );
    clsSatProc.ConstractAdjust( clsRasterBF );

//    clsGdalSetFOut.CreateFile( "/home/wan/tttR.bmp", clsRasterBF.GetW(), clsRasterBF.GetH(), UINT(1), GDT_Byte, "BMP" );
//    clsGdalSetFOut.SaveBlockToFile( UINT(1), UINT(0), UINT(0), &clsRasterBF );

    DOUBLE *pLx, *pRx, *pLy, *pRy;
    pLx = pRx = pLy = pRy = NULL;

    SINT nNum = ASiftMatch( clsRasterBE.GetData(), clsRasterBE.GetW(), clsRasterBE.GetH(), clsRasterBF.GetData(), clsRasterBF.GetW(), clsRasterBF.GetH(),
                            pLx, pLy, pRx, pRy, 8 );


//    FILE* pTempp = fopen( "/home/wan/pdfDoc/temp.txt", "w");
//    for( int i = 0; i < nNum; ++i )
//    {
//        fprintf( pTempp, "%lf  %lf  %lf  %lf\n", pLx[i], pLy[i], pRx[i], pRy[i] );
//    }
//    fclose( pTempp );


    vector<Pt2d> vecMPtsFL, vecMPtsEL;
    for( UINT i = 0; i < UINT(nNum); ++i )
    {
        Pt2d ptF, ptE;
        ptE.X = pLx[i];
        ptE.Y = pLy[i] + dYImgMINE;
        ptF.X = pRx[i];
        ptF.Y = pRy[i] + dYImgMINF;
        vecMPtsEL.push_back( ptE );
        vecMPtsFL.push_back( ptF );
    }
    freeASiftData( pLx );
    freeASiftData( pLy );
    freeASiftData( pRx );
    freeASiftData( pRy );

    CmlStereoProc clsStereoProc;

    //---------------------------------------------------------
    CmlFrameImage clsFrmImgFL, clsFrmImgFR;
    clsFrmImgFL.LoadFile( stereoFS.imgLInfo.strImgPath.c_str() );
    clsFrmImgFR.LoadFile( stereoFS.imgRInfo.strImgPath.c_str() );

    vector<StereoMatchPt> vecSMPtF;
    for( UINT i = 0; i < UINT(nNum); ++i )
    {
        StereoMatchPt ptSPt;
        Pt2i ptL, ptR;
        ptR.X = -1;
        ptR.Y = -1;
        ptSPt.ptLInImg.X = vecMPtsFL[i].X;
        ptSPt.ptLInImg.Y = vecMPtsFL[i].Y;
        ptL.X = SINT( ptSPt.ptLInImg.X);
        ptL.Y = SINT( ptSPt.ptLInImg.Y);

        DOUBLE dCoef = 0.0;
        if( true == clsStereoProc.mlTemplateMatchInRegion(  &clsFrmImgFL.m_DataBlock, &clsFrmImgFR.m_DataBlock, ptL, ptR, dCoef, -100, 0, -2, 2, 15, 0.0 ) )
        {
            ptSPt.ptRInImg.X = ptR.X;
            ptSPt.ptRInImg.Y = ptR.Y;

            clsStereoProc.mlLsMatch( &clsFrmImgFL.m_DataBlock, &clsFrmImgFR.m_DataBlock, ptSPt.ptLInImg.X, ptSPt.ptLInImg.Y, ptSPt.ptRInImg.X, ptSPt.ptRInImg.Y, 7, dCoef );
        }
        vecSMPtF.push_back( ptSPt );
    }

    //-------------------------------------------------------------------------
    CmlFrameImage clsFrmImgEL, clsFrmImgER;
    clsFrmImgEL.LoadFile( stereoES.imgLInfo.strImgPath.c_str() );
    clsFrmImgER.LoadFile( stereoES.imgRInfo.strImgPath.c_str() );

    vector<StereoMatchPt> vecSMPtE;
    for( UINT i = 0; i < UINT(nNum); ++i )
    {
        StereoMatchPt ptSPt;
        Pt2i ptL, ptR;
        ptR.X = -1;
        ptR.Y = -1;
        ptSPt.ptLInImg.X = vecMPtsEL[i].X;
        ptSPt.ptLInImg.Y = vecMPtsEL[i].Y;
        ptL.X = SINT( ptSPt.ptLInImg.X);
        ptL.Y = SINT( ptSPt.ptLInImg.Y);
        DOUBLE dCoef = 0.0;
        if( true == clsStereoProc.mlTemplateMatchInRegion(  &clsFrmImgEL.m_DataBlock, &clsFrmImgER.m_DataBlock, ptL, ptR, dCoef, -100, 0, -2, 2, 15, 0.0 ) )
        {
            ptSPt.ptRInImg.X = ptR.X;
            ptSPt.ptRInImg.Y = ptR.Y;

            clsStereoProc.mlLsMatch( &clsFrmImgEL.m_DataBlock, &clsFrmImgEL.m_DataBlock, ptSPt.ptLInImg.X, ptSPt.ptLInImg.Y, ptSPt.ptRInImg.X, ptSPt.ptRInImg.Y, 7, dCoef );
        }
        vecSMPtE.push_back( ptSPt );
    }
    ////////////////////////////////////////////////////////////////////////////////
    vector<Pt3d> vec3dPtsF, vec3dPtsE;


    for( UINT i = 0; i < vecSMPtF.size(); ++i )
    {
        StereoMatchPt ptCurF = vecSMPtF[i];
        StereoMatchPt ptCurE = vecSMPtE[i];
        Pt3d ptXYZF, ptXYZE;
        clsStereoProc.mlInterSection( ptCurF.ptLInImg, ptCurF.ptRInImg, clsFrmImgFL.GetHeight(), clsFrmImgFR.GetHeight(), ptXYZF,
                                      &stereoFS.imgLInfo.inOri, &stereoFS.imgLInfo.exOri, &stereoFS.imgRInfo.inOri, &stereoFS.imgRInfo.exOri ) ;

        clsStereoProc.mlInterSection( ptCurE.ptLInImg, ptCurE.ptRInImg, clsFrmImgEL.GetHeight(), clsFrmImgER.GetHeight(), ptXYZE,
                                      &stereoES.imgLInfo.inOri, &stereoES.imgLInfo.exOri, &stereoES.imgRInfo.inOri, &stereoES.imgRInfo.exOri ) ;

        vec3dPtsF.push_back( ptXYZF );
        vec3dPtsE.push_back( ptXYZE );
    }


    Pt3d ptFImgOrig, ptEImgOrig;
    ptFImgOrig.X = ( stereoFS.imgLInfo.exOri.pos.X + stereoFS.imgRInfo.exOri.pos.X ) / 2.0;
    ptFImgOrig.Y = ( stereoFS.imgLInfo.exOri.pos.Y + stereoFS.imgRInfo.exOri.pos.Y ) / 2.0;
    ptFImgOrig.Z = ( stereoFS.imgLInfo.exOri.pos.Z + stereoFS.imgRInfo.exOri.pos.Z ) / 2.0;

    ptEImgOrig.X = ( stereoES.imgLInfo.exOri.pos.X + stereoES.imgRInfo.exOri.pos.X ) / 2.0;
    ptEImgOrig.Y = ( stereoES.imgLInfo.exOri.pos.Y + stereoES.imgRInfo.exOri.pos.Y ) / 2.0;
    ptEImgOrig.Z = ( stereoES.imgLInfo.exOri.pos.Z + stereoES.imgRInfo.exOri.pos.Z ) / 2.0;


    vector<bool> vecFlags;
    this->removePtsByDisConsistant( vec3dPtsF, ptFImgOrig, vec3dPtsE, ptEImgOrig, 0.003, 0.4, vecFlags );

    vector<StereoMatchPt> vecSMPtsF, vecSMPtsE;
    vector<Pt3d> vec3dAccPtsF, vec3dAccPtsE;
    vector<Pt2d> vecImgPts;
    vector<DOUBLE> vecTDis;
    for( UINT i = 0; i < vecFlags.size(); ++i )
    {
        if( true == vecFlags[i] )
        {
            vecSMPtsF.push_back( vecSMPtF[i] );
            vecSMPtsE.push_back( vecSMPtE[i] );

            vecImgPts.push_back( vecSMPtE[i].ptLInImg);

            vec3dAccPtsF.push_back( vec3dPtsF[i] );
            vec3dAccPtsE.push_back( vec3dPtsE[i] );

            vecTDis.push_back( DisIn2Pts( vec3dPtsF[i], vec3dPtsE[i] ) );
        }
    }
    UINT nImgPts = vec3dAccPtsF.size();
    Pt2d *ptImgPts = new Pt2d[nImgPts];
    Pt3d *ptObjPts = new Pt3d[nImgPts];
    ExOriPara exOri = stereoES.imgLInfo.exOri;
    InOriPara inOri = stereoES.imgLInfo.inOri;

    for( UINT i = 0; i < nImgPts; ++i )
    {
        Pt2d* ptCurImgPt = (ptImgPts + i);
        ptCurImgPt->X = vecImgPts[i].X - inOri.x;
        ptCurImgPt->Y = inOri.y - vecImgPts[i].Y;
        ptObjPts[i] = vec3dAccPtsF[i];
    }

    ExOriPara exOriRes;


    CmlPhgProc clsPhgProc;
    clsPhgProc.mlBackForwardinterSection( ptImgPts, ptObjPts, inOri.f, inOri.f, nImgPts, &exOri, &exOriRes );

    //////////////////////////////////////
    CmlMat matRelat, matLeft, matLeftInv, matRight, matTR, matTL, matTRelat, matTTemp;
    OPK2RMat( &stereoES.imgLInfo.exOri.ori, &matLeft );
    OPK2RMat( &stereoES.imgRInfo.exOri.ori, &matRight );
    mlMatInv( &matLeft, &matLeftInv );
    mlMatMul( &matLeftInv, &matRight, &matRelat );

    matTL.Initial( 3, 1 );
    matTR.Initial( 3, 1 );
    matTL.SetAt( 0, 0, stereoES.imgLInfo.exOri.pos.X );
    matTL.SetAt( 1, 0, stereoES.imgLInfo.exOri.pos.Y );
    matTL.SetAt( 2, 0, stereoES.imgLInfo.exOri.pos.Z );

    matTR.SetAt( 0, 0, stereoES.imgRInfo.exOri.pos.X );
    matTR.SetAt( 1, 0, stereoES.imgRInfo.exOri.pos.Y );
    matTR.SetAt( 2, 0, stereoES.imgRInfo.exOri.pos.Z );

    mlMatSub( &matTR, &matTL, &matTTemp );
    mlMatMul( &matLeftInv, &matTTemp, &matTRelat );

    ExOriPara exOriRelat;
    RMat2OPK( &matRelat, &exOriRelat.ori );
    exOriRelat.pos.X = matTRelat.GetAt( 0, 0 );
    exOriRelat.pos.Y = matTRelat.GetAt( 1, 0 );
    exOriRelat.pos.Z = matTRelat.GetAt( 2, 0 );

    //---------------------------------------------------
    ExOriPara exOriLeft, exOriRight;
    exOriLeft = exOriRes;

    CmlMat matTempLR, matTempRR, matTempTR;
    OPK2RMat( &exOriLeft.ori, &matTempLR );
    mlMatMul( &matTempLR, &matRelat, &matTempRR );
    RMat2OPK( &matTempRR, &exOriRight.ori );

    mlMatMul( &matTempLR, &matTRelat, &matTempTR );
    exOriRight.pos.X = matTempTR.GetAt( 0, 0 ) + matTL.GetAt( 0, 0 );
    exOriRight.pos.Y = matTempTR.GetAt( 1, 0 ) + matTL.GetAt( 1, 0 );
    exOriRight.pos.Z = matTempTR.GetAt( 2, 0 ) + matTL.GetAt( 2, 0 );


    ptLocalRes.X = ( exOriLeft.pos.X + exOriRight.pos.X ) / 2.0;
    ptLocalRes.Y = ( exOriLeft.pos.Y + exOriRight.pos.Y ) / 2.0;
    ptLocalRes.Z = ( exOriLeft.pos.Z + exOriRight.pos.Z ) / 2.0;




    /////////////////////////////////////////////////////////////////////////////////
//    FILE* pFF = fopen( "/home/wan/testF.txt", "w");
//    for( UINT i = 0; i < nNum; ++i )
//    {
//        if( vecFlags[i] == false )
//        {
//           continue;
//        }
//        fprintf( pFF, "%lf  %lf  %lf  %lf\n  ", vecSMPtF[i].ptLInImg.X, vecSMPtF[i].ptLInImg.Y, vecSMPtF[i].ptRInImg.X, vecSMPtF[i].ptRInImg.Y );
//    }
//    fclose( pFF );
//
//    FILE* pFE = fopen( "/home/wan/testE.txt", "w");
//    for( UINT i = 0; i < nNum; ++i )
//    {
//        if( vecFlags[i] == false )
//        {
//            continue;
//        }
//        fprintf( pFE, "%lf  %lf  %lf  %lf\n  ", vecSMPtE[i].ptLInImg.X, vecSMPtE[i].ptLInImg.Y, vecSMPtE[i].ptRInImg.X, vecSMPtE[i].ptRInImg.Y );
//    }
//    fclose( pFE );
//    SINT nkkkkkkkkkk = 1;

    return true;

}

/**
* @fn LocalIn2Dom
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 卫星影像和地面影像间匹配实现定位
* @param strLandDom 地面影像文件路径
* @param strSatDom 卫星影像匹配点坐标
* @param landPtSet 地面影像匹配点坐标
* @param satPtSet 卫星影像匹配点坐标
* @param localByMOpts 定位参数
* @param ptLocalRes 定位结果
* @param dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::LocalIn2Dom(  const char* strLandDom, const char* strSatDom, ImgPtSet &landPtSet, ImgPtSet &satPtSet, LocalByMatchOpts localByMOpts, Pt2d ptCent, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{
    vector<StereoMatchPt> vecRanMPt;

    CmlPtsManage clsPtManage;
    clsPtManage.GetPairPts( landPtSet, satPtSet, vecRanMPt );

    CmlGeoRaster clsImgRover, clsImgSat;
    bool bIsLoadSat = clsImgSat.LoadGeoFile( strSatDom );
    bool bIsLoadLand = clsImgRover.LoadGeoFile( strLandDom );

    if( vecRanMPt.size() < 3 )
    {
        if( ( true == bIsLoadLand )&&( true == bIsLoadSat ) )
        {
            double dRoverRes = clsImgRover.m_dXResolution;
            double dSatRes = clsImgSat.m_dXResolution;

            double dRatio = dRoverRes/ dSatRes;

            CmlStereoProc clsStereoProc;
            clsStereoProc.Match2LargeImg( strLandDom, strSatDom, vecRanMPt, localByMOpts.stuSiftPara, localByMOpts.stuRANSACPara, dRatio );

            if( landPtSet.imgInfo.nImgIndex == 0 )
            {
                landPtSet.imgInfo.nImgIndex = 1001;
            }
            if( satPtSet.imgInfo.nImgIndex == 0 )
            {
                satPtSet.imgInfo.nImgIndex = 1002;
            }
            CmlPtsManage clsPtManage;
            clsPtManage.SplitPairPts( landPtSet.imgInfo, satPtSet.imgInfo, vecRanMPt, landPtSet, satPtSet );

        }
        else
        {
            return false;
        }
    }
    UINT nRanPtNum = vecRanMPt.size();

    CmlMat matA, matL, matX;
    matA.Initial( nRanPtNum*2, 4 );
    matL.Initial( nRanPtNum*2, 1 );

    for( UINT i = 0; i < vecRanMPt.size(); ++i )
    {
        StereoMatchPt pt = vecRanMPt[i];
        matA.SetAt( 2*i, 0, pt.ptLInImg.X );
        matA.SetAt( 2*i, 1, -1.0*pt.ptLInImg.Y );
        matA.SetAt( 2*i, 2, 1 );
        matA.SetAt( 2*i, 3, 0 );

        matA.SetAt( 2*i+1, 0, pt.ptLInImg.Y );
        matA.SetAt( 2*i+1, 1, pt.ptLInImg.X );
        matA.SetAt( 2*i+1, 2, 0 );
        matA.SetAt( 2*i+1, 3, 1 );

        matL.SetAt( 2*i, 0, pt.ptRInImg.X );
        matL.SetAt( 2*i+1, 0, pt.ptRInImg.Y );

    }

    mlMatSolveSVD( &matA, &matL, &matX );

//    SINT nRoverImgW = clsImgRover.GetWidth();
//    SINT nRoverImgH = clsImgRover.GetHeight();

    DOUBLE dx = ptCent.X;
    DOUBLE dy = ptCent.Y;

    Pt3d ptRes;

    ptRes.X = matX.GetAt( 0,0 )*dx - matX.GetAt( 1, 0 )*dy + matX.GetAt( 2, 0 );
    ptRes.Y = matX.GetAt( 1,0 )*dx + matX.GetAt( 0, 0 )*dy + matX.GetAt( 3, 0 );

    ptRes.X = clsImgSat.m_PtOrigin.X + clsImgSat.m_dXResolution * ptRes.X;
    ptRes.Y = clsImgSat.m_PtOrigin.Y + clsImgSat.m_dYResolution * ptRes.Y;

    ptLocalRes.X = ptRes.X;
    ptLocalRes.Y = ptRes.Y;

    ///////////////////////////////////////////////////////////////
    DOUBLE dXALL, dYALL;
    dXALL = dYALL = 0.0;

    for( UINT i = 0; i < vecRanMPt.size(); ++i )
    {
        StereoMatchPt pt = vecRanMPt[i];
        DOUBLE dTempX = pt.ptLInImg.X * matX.GetAt( 0, 0 ) - pt.ptLInImg.Y * matX.GetAt( 1, 0 ) + matX.GetAt( 2, 0 );
        DOUBLE dTempY = pt.ptLInImg.Y * matX.GetAt( 1, 0 ) + pt.ptLInImg.Y * matX.GetAt( 0, 0 ) + matX.GetAt( 3, 0 );

        dXALL += ( dTempX - pt.ptRInImg.X )*( dTempX - pt.ptRInImg.X );
        dYALL += ( dTempY - pt.ptRInImg.Y )*( dTempY - pt.ptRInImg.Y );
    }
    dLocalAccuracy = sqrt( ( dXALL + dYALL ) / vecRanMPt.size() );
    ///////////////////////////////////////////////////////////////
    return true;

}
bool CmlLocalization::WriteLocalRes( const char* strPath, ExOriPara localRes )
{
    return false;
}
bool CmlLocalization::WriteLocalRes( const char* strPath, Pt3d ptlocalRes )
{
    string strInPath( strPath );
    SINT nStrPos = strInPath.rfind(".");

    string sTempPath;
    sTempPath.assign(strInPath, nStrPos, strInPath.length() );

    if ( 0 != strcmp( sTempPath.c_str(), ".lrf" ))
    {
        return false;
    }

    FILE* pF = NULL;
    pF = fopen( strPath, "w" );
    if( pF != NULL )
    {
        fprintf( pF, "%s\n", "ML_LOCALIZATION_RESULT_FILE" );

        double dVersion = 1.0;
        fprintf( pF, "%lf\n", dVersion );

        fprintf( pF, "%lf %lf %lf\n", ptlocalRes.X, ptlocalRes.Y, ptlocalRes.Z );
        fclose( pF );
        return true;
    }
    else
    {
        return false;
    }
}

/**
* @fn loadGCPFile
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 导入控制点文件
* @param strGCPFile 控制点文件路径
* @param vecGCPs 控制点信息
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::loadGCPFile( const SCHAR* strGCPFile, vector<GCPoint> &vecGCPs )
{
//    string strInPath( strGCPFile );
//    SINT nStrPos = strInPath.rfind(".");
//
//    string sTempPath;
//    sTempPath.assign( strInPath, nStrPos, strInPath.length() );
//
//    if ( 0 != strcmp( sTempPath.c_str(), ".gcp" ))
//    {
//        return false;
//    }
//
//    FILE* pF = NULL;
//    pF = fopen( strGCPFile, "r" );
//    if( pF != NULL )
//    {
//        char cHead[128];
//        fscanf( pF, "%s\n", cHead );
//        if( 0 != strcmp( cHead, "ML_GCP_POINT_FILE" ) )
//        {
//            return false;
//        }
//        double dVersion = 1.0;
//        fscanf( pF, "%lf\n", &dVersion );
//
//        SINT nNum;
//        fscanf( pF, "%d\n", &nNum );
//
//        for( int i = 0; i < nNum; ++i )
//        {
//            GCPoint pt;
//            fscanf( pF, "%llu %lf %lf %lf\n", &pt.nID, &pt.ptXYZ.X, &pt.ptXYZ.Y, &pt.ptXYZ.Z );
//            vecGCPs.push_back( pt );
//        }
//
//        fclose( pF );
        return true;
//    }
//    else
//    {
//        return false;
//    }
}

/**
* @fn loadMarkedPtFile
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 导入标志点文件
* @param clsImgPtSets 影像及站点信息
* @param mapPts
* @param frameInfo 影像信息
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlLocalization::loadMarkedPtFile( ImgPtSet clsImgPtSets, map<ULONG, Pt2d> &mapPts, FrameImgInfo &frameInfo  )
{
    UINT nPtNum = clsImgPtSets.vecPts.size();
    for( UINT i = 0; i < nPtNum; ++i )
    {
        Pt2d ptTemp = clsImgPtSets.vecPts[i];
        mapPts.insert( map<ULONG, Pt2d> :: value_type( ptTemp.lID, ptTemp ));
    }
    UINT nAddPtNum = clsImgPtSets.vecAddPts.size();
    for( UINT i = 0; i < nAddPtNum; ++i )
    {
        Pt2d ptTemp = clsImgPtSets.vecAddPts[i];
        mapPts.insert( map<ULONG, Pt2d> :: value_type( ptTemp.lID, ptTemp ));
    }

    if( ( nPtNum == 0 )&&( nAddPtNum == 0 ))
    {
        return false;
    }
    else
    {
        return true;
    }

}
bool CmlLocalization::DealImgSelectionIn2Sites( vector<ExOriPara> vecSiteOne, vector<ExOriPara> vecSiteTwo, UINT nIDOne, UINT nIDTwo )
{
    return true;
}
bool CmlLocalization::CalcTwoSitesByBA( vector<Pt2d> vecPtsOneL, vector<Pt2d> vecPtsOneR, vector<Pt2d> vecPtsTwoL, vector<Pt2d> vecPtsTwoR, vector<Pt3d> vecXYZ, \
                                        StereoSet sFirstSet, StereoSet sSecondSet, ExOriPara &exTwoL, ExOriPara &exTwoR )
{
    CmlBA clsBA;



//    UINT nPtNum = vecPtsOneL.size();
//    if( ( nPtNum == 0 )||( nPtNum != vecPtsOneR.size() )||( nPtNum != vecPtsTwoL.size() )||( nPtNum != vecPtsTwoR.size() )||( nPtNum != vecXYZ.size() ) )
//    {
//        return false;
//    }
//    CmlMat matA, matL, matX;
//    if( ( false == matA.Initial( (UINT)8*nPtNum, (UINT)(12+3*nPtNum) ) ) || ( false == matL.Initial( (UINT)8*nPtNum, UINT(1) ) ) || (false == matX.Initial( UINT(12+3*nPtNum), UINT(1) ) ) )
//    {
//        return false;
//    }
//    for( UINT i = 0; i < 8*nPtNum; ++i )
//    {
//        matL.SetAt( i, 0, 0 );
//        for( UINT j = 0; j < (12+3*nPtNum); ++j )
//        {
//            matA.SetAt( i, j, 0 );
//            if( i == 0 )
//            {
//                matX.SetAt( j, 0, 0 );
//            }
//        }
//    }
//    ExOriPara oriExOriFirstL = sFirstSet.imgLInfo.exOri;
//    ExOriPara oriExOriFirstR = sFirstSet.imgRInfo.exOri;
//    ExOriPara oriExOriSecondL = sSecondSet.imgLInfo.exOri;
//    ExOriPara oriExOriSecondR = sSecondSet.imgRInfo.exOri;
//    DOUBLE dFirstLf = sFirstSet.imgLInfo.inOri.f;
//    DOUBLE dFirstRf = sFirstSet.imgRInfo.inOri.f;
//    DOUBLE dSecondLf = sSecondSet.imgLInfo.inOri.f;
//    DOUBLE dSecondRf = sSecondSet.imgRInfo.inOri.f;
//    for( UINT i = 0; i < nPtNum; ++i )
//    {
//        Pt2d pt1L = vecPtsOneL[i];
//        Pt2d pt1R = vecPtsOneR[i];
//        Pt2d pt2L = vecPtsTwoL[i];
//        Pt2d pt2R = vecPtsTwoR[i];
//        Pt3d ptXYZ = vecXYZ[i];
//        DOUBLE dCoefA2L[12], dCoefA2R[12], dCoefB1L[6], dCoefB1R[6], dCoefB2L[6], dCoefB2R[6];
//
//
//        getCoefA_B( pt1L, &oriExOriFirstL, dFirstLf, dFirstLf, ptXYZ, dCoefB1L );
//        getCoefA_B( pt1R, &oriExOriFirstR, dFirstRf, dFirstRf, ptXYZ, dCoefB1R );
//        getCoefA_B( pt2L, &oriExOriSecondL, dSecondLf, dSecondLf, ptXYZ, dCoefB2L );
//        getCoefA_B( pt2R, &oriExOriSecondR, dSecondRf, dSecondRf, ptXYZ, dCoefB2R );
//
//        for( UINT j = 0; j < 3; ++j )
//        {
//            matA.SetAt( 8*i,   (12+3*i+j), dCoefB1L[j] );
//            matA.SetAt( 8*i+1, (12+3*i+j), dCoefB1L[3+j] );
//            matA.SetAt( 8*i+2, (12+3*i+j), dCoefB1R[j] );
//            matA.SetAt( 8*i+3, (12+3*i+j), dCoefB1R[3+j] );
//
//            matA.SetAt( 8*i+4, (12+3*i+j), dCoefB2L[j] );
//            matA.SetAt( 8*i+5, (12+3*i+j), dCoefB2L[3+j] );
//            matA.SetAt( 8*i+6, (12+3*i+j), dCoefB2R[j] );
//            matA.SetAt( 8*i+7, (12+3*i+j), dCoefB2R[3+j] );
//        }
//
//        getCoefA_B( pt2L, &oriExOriSecondL, dSecondLf, dSecondLf, ptXYZ, dCoefA2L );
//        getCoefA_B( pt2R, &oriExOriSecondR, dSecondRf, dSecondRf, ptXYZ, dCoefA2R );
//        for( UINT j = 0; j < 6; ++j )
//        {
//            matA.SetAt( 8*i+4, (j), dCoefA2L[j] );
//            matA.SetAt( 8*i+5, (j), dCoefA2L[6+j] );
//            matA.SetAt( 8*i+6, (6+j), dCoefA2R[j] );
//            matA.SetAt( 8*i+7, (6+j), dCoefA2R[6+j] );
//        }
//
//
//
//
//
//
//    }	
	return true;
}
