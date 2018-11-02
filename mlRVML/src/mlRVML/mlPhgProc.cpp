/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlPhgProc.cpp
* @date 2012.01
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 摄影测量算法类实现文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#include "mlPhgProc.h"
#include "mlBase.h"
#include "mlMat.h"
#include "mlGeoRaster.h"
#include "mlRasterBlock.h"
#include "mlTIN.h"
#include "mlTinSimply.h"
#include "mlGpc.h"
#include "mlFrameImage.h"
#include "mlTin.h"
//#include "gsl.h"
#include <complex>
//#include "mlTinSimply.h"
/*********************************************
摄影测量处理类
**********************************************/
DOUBLE DisIn2Pts( Pt3d pt1, Pt3d pt2 )
{
    DOUBLE dx = pt1.X - pt2.X;
    DOUBLE dy = pt1.Y - pt2.Y;
    DOUBLE dz = pt1.Z - pt2.Z;
    return (sqrt( ( dx )*( dx) + ( dy )*(dy) + (dz)*(dz) ));
}
DOUBLE DisIn2Pts( Pt2d pt1, Pt2d pt2 )
{
    DOUBLE dx = pt1.X - pt2.X;
    DOUBLE dy = pt1.Y - pt2.Y;
    return (sqrt( ( dx )*( dx) + ( dy )*(dy)));
}
/**
* @fn getxyFromXYZ
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 根据三维点及外方位元素求像点
* @param x 像点x
* @param y 像点y
* @param X 三维点X
* @param Y 三维点Y
* @param Z 三维点Z
* @param XsYsZs 外方位线元素
* @param R 旋转矩阵
* @param fx x方向焦距
* @param fy y方向焦距
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool getxyFromXYZ( DOUBLE& x, DOUBLE& y, DOUBLE X, DOUBLE Y, DOUBLE Z, DOUBLE *XsYsZs, DOUBLE *R, DOUBLE fx, DOUBLE fy )
{
    DOUBLE dX = X - XsYsZs[0];
    DOUBLE dY = Y - XsYsZs[1];
    DOUBLE dZ = Z - XsYsZs[2];

    DOUBLE dCoefX = R[0] * dX + R[3] * dY + R[6] * dZ;
    DOUBLE dCoefY = R[1] * dX + R[4] * dY + R[7] * dZ;
    DOUBLE dCoefZ = R[2] * dX + R[5] * dY + R[8] * dZ;

    x = -fx * dCoefX / dCoefZ;
    y = -fy * dCoefY / dCoefZ;

    return true;
}
bool getxyFromXYZ( Pt2d &ptxy, Pt3d ptXYZ, Pt3d ptXYZOrg, CmlMat matOPK, DOUBLE fx, DOUBLE fy )
{
    DOUBLE dX = ptXYZ.X - ptXYZOrg.X;
    DOUBLE dY = ptXYZ.Y - ptXYZOrg.Y;
    DOUBLE dZ = ptXYZ.Z - ptXYZOrg.Z;

    DOUBLE dCoefX = matOPK.GetAt( 0, 0 ) * dX + matOPK.GetAt( 1, 0 ) * dY + matOPK.GetAt( 2, 0 ) * dZ;
    DOUBLE dCoefY = matOPK.GetAt( 0, 1 ) * dX + matOPK.GetAt( 1, 1 ) * dY + matOPK.GetAt( 2, 1 ) * dZ;
    DOUBLE dCoefZ = matOPK.GetAt( 0, 2 ) * dX + matOPK.GetAt( 1, 2 ) * dY + matOPK.GetAt( 2, 2 ) * dZ;

    ptxy.X = -fx * dCoefX / dCoefZ;
    ptxy.Y = -fy * dCoefY / dCoefZ;

    return true;
}
bool getxyFromXYZ( Pt2d &ptxy, Pt3d ptXYZ, Pt3d ptXYZOrgL, DOUBLE dBaseLength, CmlMat matOPK, DOUBLE fx, DOUBLE fy )
{
    Pt3d ptNewOrig;
    ptNewOrig.X = ptXYZOrgL.X + matOPK.GetAt( 0, 0 )*dBaseLength;
    ptNewOrig.Y = ptXYZOrgL.Y + matOPK.GetAt( 1, 0 )*dBaseLength;
    ptNewOrig.Z = ptXYZOrgL.Z + matOPK.GetAt( 2, 0 )*dBaseLength;

    return getxyFromXYZ( ptxy, ptXYZ, ptNewOrig, matOPK, fx, fy );

}
/**
 *@fn CmlPhgProc()
 *@date 2011.11
 *@author 张重阳
 *@brief 摄影测量算法类构造函数
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
*/
CmlPhgProc::CmlPhgProc()
{
    //ctor
}


/**
 *@fn CmlPhgProc()
 *@date 2011.11
 *@author 张重阳
 *@brief 摄影测量算法类析构函数
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
*/
CmlPhgProc::~CmlPhgProc()
{
    //dtor
}

/**
*@fn mlReproject(Pt2d* pImgpt, Pt3d* pGroundPt,ExOriPara* pExOri, DOUBLE fx, DOUBLE fy,CmlMat* pRotateMat = NULL)
*@date 2011.11
*@author 张重阳
*@brief 根据共线方程，将物方坐标点反投到像平面上
*@param pImgpt 求解的像方坐标点指针
*@param pGroundPt 物方三位点坐标指针
*@param pExOri 外方位元素指针
*@param fx   焦距
*@param fy   焦距
*@param pRotateMat 旋转矩阵指针，默认为空
*@retval TRUE 成功
*@retval FALSE 失败
*@version 1.0
*@par 修改历史：
*<作者>    <时间>   <版本编号>    <修改原因>\n
*/

bool CmlPhgProc::mlReproject(Pt2d* pImgpt, Pt3d* pGroundPt,ExOriPara* pExOri, DOUBLE fx, DOUBLE fy,CmlMat* pRotateMat)
{
    DOUBLE dX = pGroundPt->X - pExOri->pos.X;
    DOUBLE dY = pGroundPt->Y - pExOri->pos.Y;
    DOUBLE dZ = pGroundPt->Z - pExOri->pos.Z;
    if(pRotateMat == NULL)           //如果旋转矩阵为空，则首先计算旋转矩阵
    {
        CmlMat* pRotateMat;

        pRotateMat = new CmlMat;
        OPK2RMat(&(pExOri->ori), pRotateMat);
        DOUBLE a1 = pRotateMat->GetAt(0,0);   //共线方程各系数赋值
        DOUBLE a2 = pRotateMat->GetAt(0,1);
        DOUBLE a3 = pRotateMat->GetAt(0,2);
        DOUBLE b1 = pRotateMat->GetAt(1,0);
        DOUBLE b2 = pRotateMat->GetAt(1,1);
        DOUBLE b3 = pRotateMat->GetAt(1,2);
        DOUBLE c1 = pRotateMat->GetAt(2,0);
        DOUBLE c2 = pRotateMat->GetAt(2,1);
        DOUBLE c3 = pRotateMat->GetAt(2,2);

        DOUBLE dCoefX = a1 * dX + b1 * dY + c1 * dZ;
        DOUBLE dCoefY = a2 * dX + b2 * dY + c2 * dZ;
        DOUBLE dCoefZ = a3 * dX + b3 * dY + c3 * dZ;

        pImgpt->X = -fx * dCoefX / dCoefZ;          //根据共线方程计算
        pImgpt->Y = -fy * dCoefY / dCoefZ;

        delete pRotateMat;

        // LOGAddSuccessQuitMsg();
        return true;

    }

    else                  //若旋转矩阵已知，则直接赋值
    {
        DOUBLE a1 = pRotateMat->GetAt(0,0);
        DOUBLE a2 = pRotateMat->GetAt(0,1);
        DOUBLE a3 = pRotateMat->GetAt(0,2);
        DOUBLE b1 = pRotateMat->GetAt(1,0);
        DOUBLE b2 = pRotateMat->GetAt(1,1);
        DOUBLE b3 = pRotateMat->GetAt(1,2);
        DOUBLE c1 = pRotateMat->GetAt(2,0);
        DOUBLE c2 = pRotateMat->GetAt(2,1);
        DOUBLE c3 = pRotateMat->GetAt(2,2);

        DOUBLE dCoefX = a1 * dX + b1 * dY + c1 * dZ;
        DOUBLE dCoefY = a2 * dX + b2 * dY + c2 * dZ;
        DOUBLE dCoefZ = a3 * dX + b3 * dY + c3 * dZ;

        pImgpt->X = -fx * dCoefX / dCoefZ;   //根据共线方程计算
        pImgpt->Y = -fy * dCoefY / dCoefZ;

        //   LOGAddSuccessQuitMsg();
        return true;
    }

}



/**
 *@fn mlBackForwardinterSection
 *@date 2011.11
 *@author 张重阳
 *@brief 由控制点坐标后方教会求解影像外方外元素
 *@param pImgPt 像方坐标点集  结构体数组
 *@param pGroundPt 物方三位点坐标点集  结构体数组
 *@param fx  焦距
 *@param fy  焦距
 *@param pInitExOripara 初始外方位元素指针
 *@param pExOripara 外方为元素指针
 *@param nPtNum 总点数
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n

*/

bool CmlPhgProc::mlBackForwardinterSection( Pt2d *pImgPt,Pt3d *pGroundPt, DOUBLE fx,DOUBLE fy, SINT nPtNum,ExOriPara *pInitExOripara, ExOriPara *pExOripara )
{
    if( nPtNum < 3)                //已知点数小于3，无法求解，返回fasle
    {
        SCHAR strMsg[] = "Less than 3 input points to calculate!\n";
        LOGAddErrorMsg(strMsg);
        return false;

    }

    DOUBLE dDeltaXs, dDeltaYs, dDeltaZs;
    DOUBLE dDeltaOmiga, dDeltaPhi, dDeltaKappa;

    dDeltaXs = dDeltaYs = dDeltaZs = 0.0;
    dDeltaPhi = dDeltaOmiga = dDeltaKappa = 100;
    CmlMat matR;
    matR.Initial(3,3);
    *pExOripara = *pInitExOripara;

    DOUBLE dThreshold = 0.1 / 60;  // 角元素改正数迭代阈值
    SINT iIterTimesThresh = 15; // 迭代最高次数
    SINT nTimes = 0; //迭代次数

    while( (abs(dDeltaOmiga) > dThreshold )  || ( abs(dDeltaPhi)> dThreshold ) || ( abs(dDeltaKappa) > dThreshold ) )       //当改正值大于限差，继续迭代
    {
        OPK2RMat(&(pExOripara->ori), &matR);

        DOUBLE a1 = matR.GetAt(0,0);
        DOUBLE a2 = matR.GetAt(0,1);
        DOUBLE a3 = matR.GetAt(0,2);

        DOUBLE b1 = matR.GetAt(1,0);
        DOUBLE b2 = matR.GetAt(1,1);
        DOUBLE b3 = matR.GetAt(1,2);

        DOUBLE c1 = matR.GetAt(2,0);
        DOUBLE c2 = matR.GetAt(2,1);
        DOUBLE c3 = matR.GetAt(2,2);

        CmlMat CmatA, CmatX, CmatL;
        CmatA.Initial(2*nPtNum, 6);      //初始化矩阵A
        CmatL.Initial(2*nPtNum,1);       //初始化矩阵L

        for(SINT i = 0; i < nPtNum; i++)
        {

            //控制点三维坐标
            DOUBLE dX = pGroundPt[i].X;
            DOUBLE dY = pGroundPt[i].Y;
            DOUBLE dZ = pGroundPt[i].Z;
            //原始像点坐标
            DOUBLE dx = pImgPt[i].X;
            DOUBLE dy = pImgPt[i].Y;
            //共线方程反算像点坐标
            Pt2d PtImgAppro;
            mlReproject(&PtImgAppro,&(pGroundPt[i]),pExOripara,fx,fy);

            DOUBLE dxAppro = PtImgAppro.X;
            DOUBLE dyAppro = PtImgAppro.Y;

            CmatL.SetAt(2 * i, 0,(dx - dxAppro));
            CmatL.SetAt(2 * i + 1, 0, (dy - dyAppro));

            DOUBLE dXt = dX - pExOripara->pos.X;
            DOUBLE dYt = dY - pExOripara->pos.Y;
            DOUBLE dZt = dZ - pExOripara->pos.Z;

            DOUBLE dXCoef = a1 * dXt + b1 * dYt + c1 * dZt;
            DOUBLE dYCoef = a2 * dXt + b2 * dYt + c2 * dZt;
            DOUBLE dZCoef = a3 * dXt + b3 * dYt + c3 * dZt;
            DOUBLE dZCoefRev = 1.0 / dZCoef;

            DOUBLE dAngle[3] = {(pExOripara->ori.omg), (pExOripara->ori.phi), (pExOripara->ori.kap)};
            DOUBLE dR[9] = {a1,a2,a3,b1,b2,b3,c1,c2,c3};

            DOUBLE dTempCoef = 0;

            dTempCoef = fx * dZCoefRev * dZCoefRev * ( dXCoef*( -dR[8]*dYt + dR[5]*dZt ) - dZCoef*(-dR[6]*dYt + dR[3]*dZt) );
            CmatA.SetAt(2*i,0,dTempCoef);

            dTempCoef = fx * dZCoefRev * dZCoefRev * ( dXCoef*( cos(dAngle[1])*dXt + sin(dAngle[0])*sin(dAngle[1])*dYt - cos(dAngle[0])*sin(dAngle[1])*dZt) -
                        dZCoef*( -sin(dAngle[1])*cos(dAngle[2])*dXt + sin(dAngle[0])*cos(dAngle[1])*cos(dAngle[2])*dYt - cos(dAngle[0])*cos(dAngle[1])*cos(dAngle[2])*dZt));
            CmatA.SetAt(2*i, 1, dTempCoef);

            dTempCoef = -fx * dZCoefRev *( dR[1]*dXt + dR[4]*dYt + dR[7]*dZt );
            CmatA.SetAt(2*i, 2,dTempCoef);

            dTempCoef = fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[2] - dZCoef*dR[0] );
            dTempCoef *= -1.0;
            CmatA.SetAt(2*i,3,dTempCoef);

            dTempCoef = fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[5] - dZCoef*dR[3] );
            dTempCoef *= -1.0;
            CmatA.SetAt(2*i,4,dTempCoef);

            dTempCoef = fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[8] - dZCoef*dR[6] );
            dTempCoef *= -1.0;
            CmatA.SetAt(2*i,5,dTempCoef);

            dTempCoef = fy * dZCoefRev * dZCoefRev * ( dYCoef*( -dR[8]*dYt + dR[5]*dZt ) - dZCoef*(-dR[7]*dYt + dR[4]*dZt) );
            CmatA.SetAt(2 * i + 1,0, dTempCoef);


            dTempCoef = fy * dZCoefRev * dZCoefRev * ( dYCoef*( cos(dAngle[1])*dXt + sin(dAngle[0])*sin(dAngle[1])*dYt - cos(dAngle[0])*sin(dAngle[1])*dZt) -
                        dZCoef*( sin(dAngle[1])*sin(dAngle[2])*dXt - sin(dAngle[0])*cos(dAngle[1])*sin(dAngle[2])*dYt + cos(dAngle[0])*cos(dAngle[1])*sin(dAngle[2])*dZt));
            CmatA.SetAt(2 * i + 1, 1, dTempCoef);

            dTempCoef = fy * dZCoefRev *( dR[0]*dXt + dR[3]*dYt + dR[6]*dZt );
            CmatA.SetAt(2*i+1,2,dTempCoef);

            dTempCoef = fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[2] - dZCoef*dR[1] );
            dTempCoef *= -1.0;
            CmatA.SetAt(2*i+1, 3, dTempCoef);

            dTempCoef = fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[5] - dZCoef*dR[4] );
            dTempCoef *= -1.0;
            CmatA.SetAt(2*i + 1,4, dTempCoef);

            dTempCoef = fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[8] - dZCoef*dR[7] );
            dTempCoef *= -1.0;
            CmatA.SetAt(2*i+1,5,dTempCoef);

        }

        mlMatSolveSVD(&CmatA, &CmatL, &CmatX);         //求解方程

        pExOripara->ori.omg += (CmatX.GetAt(0,0));
        pExOripara->ori.phi += (CmatX.GetAt(1,0));
        pExOripara->ori.kap += (CmatX.GetAt(2,0));
        pExOripara->pos.X += CmatX.GetAt(3,0);
        pExOripara->pos.Y += CmatX.GetAt(4,0);
        pExOripara->pos.Z += CmatX.GetAt(5,0);

        dDeltaOmiga = CmatX.GetAt(0,0);
        dDeltaPhi = CmatX.GetAt(1,0);
        dDeltaKappa = CmatX.GetAt(2,0);
        dDeltaXs = CmatX.GetAt(3,0);
        dDeltaYs = CmatX.GetAt(4,0);
        dDeltaZs = CmatX.GetAt(5,0);

        if(++nTimes > iIterTimesThresh )          //如果迭代次数大于阈值，函数退出，返回false
        {
            SCHAR strMsg[] = "iterate not converge!\n";
            LOGAddErrorMsg(strMsg);
            return false;
        }
    }

    //LOGAddSuccessQuitMsg();
    return true;
}
bool CmlPhgProc::mlBackForwardinterSection( vector<Pt2d> vecImgPts,vector<Pt3d> vec3ds, double dF, ExOriPara *pInitExOripara, ExOriPara *pExOripara )
{
    UINT nNum = vecImgPts.size();
    if( nNum != vec3ds.size() )
    {
        return false;
    }
    Pt2d* pImgPt = new Pt2d[nNum];
    Pt3d* p3dPt = new Pt3d[nNum];
    for( UINT i = 0; i < nNum; ++i )
    {
        pImgPt[i] = vecImgPts[i];
        p3dPt[i] = vec3ds[i];
    }
    bool bFlag = this->mlBackForwardinterSection( pImgPt, p3dPt, dF, dF, nNum, pInitExOripara, pExOripara );
    delete[] pImgPt;
    delete[] p3dPt;
    return bFlag;
}
bool CmlPhgProc::mlCalcResidualError( Pt3d ptXYZ, Pt2d ptOrig2d, ExOriPara exOri, DOUBLE fx, DOUBLE fy, RMS2d &rmsError )
{
    Pt2d ptImg;
    if( true == mlReproject(&ptImg, &ptXYZ, &exOri, fx, fy) )
    {
        rmsError.lID = ptOrig2d.lID;
        rmsError.rmsX = ptOrig2d.X - ptImg.X;
        rmsError.rmsY = ptOrig2d.Y - ptImg.Y;
        rmsError.rmsAll = sqrt( rmsError.rmsX * rmsError.rmsX  + rmsError.rmsY * rmsError.rmsY );
        return true;
    }
    return false;
}

/**
 *@fn mlResectionNoInitalVal
 *@date 2011.11
 *@author 万文辉
 *@brief 无需初始值的后方交会直接解(4点)
 * @param[in] vecGCPs 控制点坐标
 * @param[in] vecImgPts 像点坐标(像平面坐标系)
 * @param[out] exOriRes 定位后的外方位元素
 * @param[out] vecRMS 点误差
 * @param[out] dTotalRMS 总误差
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n

*/
bool CmlPhgProc::mlResectionNoInitalVal( vector<Pt3d> vecGCPs, vector<Pt2d> vecImgPts, DOUBLE dFocalLength, ExOriPara &exOriRes )
{
    if( ( vecGCPs.size() != 4 )||( vecGCPs.size() != vecImgPts.size() ) )
    {
        return false;
    }
    vector<Pt3d> vecGCP1;
    vector<Pt2d> vecImgPts1;
    vector<Pt3d> vecGCP2;
    vector<Pt2d> vecImgPts2;

    for( UINT i = 0; i < 4; ++i )
    {
        Pt3d ptCurGcp = vecGCPs[i];
        Pt2d ptCurImgPt = vecImgPts[i];

        if( i != 3 )
        {
            vecGCP1.push_back( ptCurGcp );
            vecImgPts1.push_back( ptCurImgPt );
        }
        if( i != 2 )
        {
            vecGCP2.push_back( ptCurGcp);
            vecImgPts2.push_back(ptCurImgPt);
        }
    }
    vector<Pt3d> vecRes1, vecRes2;
    Pt3d dBDis1, dBDis2, dAngle1, dAngle2;
    this->mlGetDisBy3Pts( vecGCP1, vecImgPts1, dFocalLength, vecRes1, dBDis1, dAngle1 );
    this->mlGetDisBy3Pts( vecGCP2, vecImgPts2, dFocalLength, vecRes2, dBDis2, dAngle2 );

    Pt3d dDisFinal;
    for( UINT i = 0; i < vecRes1.size(); ++i )
    {
        Pt3d pt1 = vecRes1[i];
        for( UINT j = 0; j < vecRes2.size(); ++j )
        {
            Pt3d pt2 = vecRes2[j];
            if( ( abs(pt1.X - pt2.X) > 10e-2 ) || ( abs(pt1.Y - pt2.Y) > 10e-2 )  )
            {
                continue;
            }
            else
            {
                dDisFinal = pt1;
                break;
            }
        }
    }
    if( abs( dDisFinal.X ) < 10e-2 )
    {
        return false;
    }
    /////////////////////////////////////////////////////////////////////////
    Pt3d ptXYZ;
    OriAngle oriA;

    mlGetXYZCoordBy3DisVal(  vecGCP1, dDisFinal, dAngle1, ptXYZ );

    mlGetRotateMatByXYZ(  vecGCP1,  vecImgPts1, ptXYZ, dFocalLength, oriA );

    exOriRes.ori = oriA;
    exOriRes.pos = ptXYZ;

    return true;
}

/**
 *@fn mlGeoRasterCut
 *@date 2011.11
 *@author 张重阳
 *@brief 根据像素对DEM、DOM进行裁剪
 *@param strFileIn 待裁剪的输入文件
 *@param strFileOut 裁剪后输出文件
 *@param pttl 裁剪左上像素点位置X Y
 *@param ptbr 裁剪右下角像素点坐标
 *@param nflag 指定裁剪方式 1为按像素裁剪 2为按地理坐标裁剪
 *@param nCutBands 指定裁剪波段 为负数时表示所有波段都裁剪 为正时制定裁剪特定的波段
 *@param dZoom 采样系数 默认为1
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/
bool CmlPhgProc::mlGeoRasterCut( const SCHAR* strFileIn, const SCHAR* strFileOut,Pt2d pttl,Pt2d ptbr,SINT nflag,SINT nCutBands,DOUBLE dZoom )
{
    CmlGeoRaster geo;
    geo.LoadGeoFile(strFileIn);   //加载待裁剪图像
    DOUBLE dXResolution = geo.m_dXResolution;   //获取图像分辨率
    DOUBLE dYResolution = geo.m_dYResolution;
    if(nflag ==1)     //根据像素坐标剪裁
    {
        SINT nWidth = SINT(ptbr.X- pttl.X + ML_ZERO);
        SINT nHeight = SINT(abs(ptbr.Y - pttl.Y) + ML_ZERO );
        SINT nBands = geo.GetBands();
        CmlRasterBlock rsblk;
        if(nCutBands < 0)   //裁剪所有波段
        {
            for(SINT i=1; i<nBands+1; i++)
            {
                geo.GetRasterOriginBlock(i,pttl.X,pttl.Y,nWidth,nHeight,dZoom,&rsblk);
            }

        }
        else      //裁剪指定波段
        {
            geo.GetRasterOriginBlock(nCutBands,pttl.X,pttl.Y,nWidth,nHeight,dZoom,&rsblk);
        }


        GDALDataType GDTType = rsblk.GetGDTType();

        DOUBLE dNoData = geo.GetNoDataVal();


        Pt2d m_pt;
        m_pt.X = geo.m_PtOrigin.X + pttl.X * dXResolution;
        m_pt.Y = geo.m_PtOrigin.Y + pttl.Y * dYResolution;        //计算新图像起点坐标
        CmlGeoRaster geowrite;   //将新图像写入文件
        geowrite.CreateGeoFile(strFileOut,m_pt,dXResolution/dZoom,dYResolution/dZoom,nHeight*dZoom,nWidth*dZoom,nBands,GDTType,dNoData);
        geowrite.SaveBlockToFile(nBands,0,0,&rsblk);

    }
    else if(nflag == 2)  //根据地理坐标裁剪
    {
        UINT nBands = geo.GetBands();
        CmlRasterBlock rsblk;
//        UINT nXOffset = (pttl.X - geo.m_PtOrigin.X)/dXResolution;
//        UINT nYOffset = (pttl.Y - geo.m_PtOrigin.Y)/dYResolution;
        UINT nWidth = (ptbr.X- pttl.X)/dXResolution;
        UINT nHeight = abs( (ptbr.Y - pttl.Y) / dYResolution);
        if(nCutBands < 0)   //裁剪所有波段
        {
            for(UINT i=1; i<nBands+1; i++)
            {
                geo.GetRasterOriginBlock(i,pttl.X,pttl.Y,nWidth,nHeight,(UINT)1,&rsblk);
            }

        }
        else        //裁剪指定波段
        {
            geo.GetRasterOriginBlock(nCutBands,pttl.X,pttl.Y,nWidth,nHeight,dZoom,&rsblk);
        }
        GDALDataType GDTType = rsblk.GetGDTType();
        DOUBLE dNoData = geo.GetNoDataVal();
        CmlGeoRaster geowrite;   //写入文件
        geowrite.CreateGeoFile(strFileOut,pttl,dXResolution/dZoom,dYResolution/dZoom,nHeight*dZoom,nWidth*dZoom,nBands,GDTType,dNoData);
        geowrite.SaveBlockToFile(nBands,0,0,&rsblk);

    }

    else
    {
        //cout << "nflag should either be 1 or 2!\n";
        SCHAR strMsg[] = "nflag should either be 1 or 2!\n";
        LOGAddErrorMsg(strMsg);

        return false;
    }

    LOGAddSuccessQuitMsg();
    return true;

}


/**
 *@fn mlImageReprj
 *@date 2012.02
 *@author 张重阳
 *@brief 根据DEM、DOM生成指定视角下的仿真图像
 *@param strDem DEM路径及文件名，geotiff文件格式，需包含起点坐标
 *@param strDom DOM路径及文件名，geotiff文件格式，需包含起点坐标
 *@param outImg 输出文件名称
 *@param exori 指定视角外方位元素
 *@param fx 焦距
 *@param fy 焦距
 *@param nImgWid 生成图像的宽
 *@param nImgHei 生成图像的高
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/
bool CmlPhgProc::mlImageReprj( const SCHAR* strDem, const SCHAR* strDom, const SCHAR* outImg,ExOriPara exori, DOUBLE fx,DOUBLE fy, SINT nImgWid, SINT nImgHei )
{
    CmlGeoRaster geodem,geodom;
    geodem.LoadGeoFile(strDem);    //读入DEM
    geodom.LoadGeoFile(strDom);    //读入DOM

    DOUBLE dXRdem = geodem.m_dXResolution;
    DOUBLE dYRdem = geodem.m_dYResolution;
    DOUBLE dNodata = geodem.GetNoDataVal();

    geodom.m_PtOrigin = geodem.m_PtOrigin;

    CmlRasterBlock demblk,domblk,imgblk;

    UINT nWiddem = geodem.GetWidth();
    UINT nHeidem = geodem.GetHeight();

    UINT nWiddom = geodom.GetWidth();
    UINT nHeidom = geodom.GetHeight();

    geodem.GetRasterOriginBlock((UINT)1,(UINT)0,(UINT)0,nWiddem,nHeidem,(UINT)1,&demblk);
    geodom.GetRasterOriginBlock((UINT)1,(UINT)0,(UINT)0,nWiddom,nHeidom,(UINT)1,&domblk);

    imgblk.InitialImg(nImgHei,nImgWid,1);   //初始化新图像矩阵块
    imgblk.SetGDTType(GDT_Byte);
    DOUBLE domGrayVal;
    Pt3d demXYZ;
    BYTE byteGray;
    BYTE bt = (BYTE)0;

    SINT xPix;
    SINT yPix;

    Pt2d pt;
    CmlMat matA, matB;
    matA.Initial(2,2);
    matB.Initial(2,1);
    CmlMat matR;
    matR.Initial(3,3);

    CmlMat matX;

//    DOUBLE a[4],b[2];

    OPK2RMat(& exori.ori, &matR);    //求解相关系数



    DOUBLE a1 = matR.GetAt(0,0);
    DOUBLE a2 = matR.GetAt(0,1);
    DOUBLE a3 = matR.GetAt(0,2);

    DOUBLE b1 = matR.GetAt(1,0);
    DOUBLE b2 = matR.GetAt(1,1);
    DOUBLE b3 = matR.GetAt(1,2);

    DOUBLE c1 = matR.GetAt(2,0);
    DOUBLE c2 = matR.GetAt(2,1);
    DOUBLE c3 = matR.GetAt(2,2);



//    DOUBLE X,Y,Z,Z1;
//    DOUBLE Z = 0.0;

    UINT nDemW;
    UINT nDemH;

//    DOUBLE dDemW;
//    DOUBLE dDemH;

//    DOUBLE dthreshold = 0.01;

    for(SINT i=0; i<nImgHei; i++)
    {
        for(SINT j=0; j<nImgWid; j++)
        {
            imgblk.SetPtrAt(i,j,&bt);
        }
    }

    for(SINT i=0; i<nImgHei; i++)          //新图像每一行
    {

        for(SINT j=0; j<nImgWid; j++)       //新图像每一列
        {

            xPix = j - nImgWid/2;       //行列号转摄影测量像平面坐标
            yPix = nImgHei/2 - i;

//            SINT nCount = 0;
//            SINT nCount2 = 0;

            DOUBLE dVecX = a1*xPix + a2*yPix - a3*fx;
            DOUBLE dVecY = b1*xPix + b2*yPix - b3*fx;
            DOUBLE dVecZ = c1*xPix + c2*yPix - c3*fx;
            DOUBLE dVecDis = sqrt(dVecX*dVecX + dVecY*dVecY + dVecZ*dVecZ);
            DOUBLE dRatioX = dVecX / dVecDis;
            DOUBLE dRatioY = dVecY / dVecDis;
            DOUBLE dRatioZ = dVecZ / dVecDis;

            DOUBLE dStep = 0.3;        //迭代步长
            DOUBLE dStepCumulate = 0;

            DOUBLE dXCumulate = exori.pos.X;
            DOUBLE dYCumulate = exori.pos.Y;
            DOUBLE dZCumulate = exori.pos.Z;

            DOUBLE dDemZ = 0.0;

//            DOUBLE dZMinus1 = 1;
//            DOUBLE dZMinus2 = 1;

            while(1)
            {

                //dZMinus2 = dZMinus1;
                dStepCumulate = dStepCumulate + dStep;
                dXCumulate = dXCumulate + dStep*dRatioX;
                dYCumulate = dYCumulate + dStep*dRatioY;
                dZCumulate = dZCumulate + dStep*dRatioZ;

                nDemW = (dXCumulate-geodem.m_PtOrigin.X)/dXRdem;
                nDemH = (dYCumulate - geodem.m_PtOrigin.Y)/dYRdem;

                if(nDemH<0 || nDemH>=nHeidem || nDemW <0 || nDemW >= nWiddem)
                {

                    byteGray = 255;
                    imgblk.SetPtrAt(i,j,&byteGray);
                    break;
                }


                demblk.GetGeoZ(dXCumulate,dYCumulate,dDemZ);
                if(abs(dDemZ - dNodata) < 1 )
                {
                    continue;
                }
                DOUBLE dZMinus = dZCumulate - dDemZ;
                if(abs(dZMinus) < 0.2)   //限差小于0.2之后步长改为0.03
                {
                    dStep = 0.03;
                }
                if(abs(dZMinus) < 0.03)
                {
                    if(false == domblk.GetGeoZ(dXCumulate,dYCumulate,domGrayVal))
                    {
                        cout << "fail to getGeoZ " << '\n';
                    }
                    byteGray = (BYTE)domGrayVal;
                    imgblk.SetPtrAt(i,j,&byteGray);
                    cout << "process" << i << ' '<< j << ' '<< domGrayVal <<  '\n';

                    break;
                }
            }
        }
    }

    CmlGeoRaster geoOutImg;       //将生成的图像写成文件输出
    geoOutImg.CreateGeoFile(outImg,pt,dXRdem,dYRdem,nImgHei,nImgWid,1,GDT_Byte,255);
    geoOutImg.SaveBlockToFile(1,0,0,&imgblk);

    LOGAddSuccessQuitMsg();
    return true;
}


/**
 *@fn mlTinSimply
 *@date 2012.02
 *@author 张重阳
 *@brief 不规则三角网简化
 *@param vecPt3dIn 需要简化的三角网点序列
 *@param vecPt3dOut 简化后的三角网点序列
 *@param simpleIndex 简化系数 取值为0到1之间
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/
bool CmlPhgProc::mlTinSimply(vector<Pt3d> &vecPt3dIn,vector<Pt3d> &vecPt3dOut,DOUBLE simpleIndex)
{
    CmlTIN tin;
    tin.Build2By3DPt(vecPt3dIn);         //根据点云构建TIN
    UINT nfaces = tin.GetNumOfTriangles();
    SINT *triList = new SINT[nfaces*3];
    UINT simfaces = nfaces * simpleIndex;   //简化够TIN大概的平面数
    memcpy(triList,tin.GetTriList(),nfaces*3*sizeof(SINT));
    ofstream infile("temp.smf");
    if(!infile)
    {
        cout << "can't read file!\n";
    }
    infile << "#$SMF 1.0" << '\n';
    infile << "#$vertices "<< vecPt3dIn.size() << '\n';
    infile << "#$faces " << nfaces << '\n';
    infile << "#"<< '\n';
    infile << "#---the temp model----" << '\n';
    infile << "#" << '\n';
    infile << "#" << '\n';
    infile << "#" << '\n';


    for(UINT i=0; i < vecPt3dIn.size(); i++)
    {
        infile << "v " << vecPt3dIn[i].X << ' '<< vecPt3dIn[i].Y <<' '<< vecPt3dIn[i].Z << '\n';

    }

    for(UINT j=0; j<nfaces; j++)
    {
        infile << "f "<<triList[3*j]+1 << ' ' << triList[3*j+1]+1<< ' ' << triList[3*j+2]+1 << '\n';
    }

    delete[] triList;

    infile.close();

    SINT argc = 4;
    SCHAR* argv[6];
    argv[0] = "qslim";
    argv[1] = "-t";
    argv[2] = new SCHAR[100];
    sprintf(argv[2], "%d", simfaces);
    argv[3] = "-o";
    argv[4] = "tempout.smf";
    argv[5] = "temp.smf";
#if defined _WIN32 || defined WIN32 || defined _WIN64 || defined WIN64
    
#else
	qslim_main(6,argv);
#endif
	cout << "finished!\n";

    ifstream result("tempout.smf");
    Pt3d pt3dout;
    string str;
    SCHAR ch;
    result >> str;
    while(!result.eof())
    {
        result >> ch;
        if(ch == 'v')
        {
            result >> pt3dout.X >> pt3dout.Y >> pt3dout.Z ;
            vecPt3dOut.push_back(pt3dout);
        }


    }

//    remove("tempout.smf");
//    remove("temp.smf");
    delete[] argv[2];

    LOGAddSuccessQuitMsg();
    return true;
}


//该函数只用于Demo显示 不在实际工程中显示
bool CmlPhgProc::mlTinSimplyDemoFile(vector<Pt3d> &vecPt3dIn,SCHAR* dst)
{
    CmlTIN tin;
    tin.Build2By3DPt(vecPt3dIn);
    UINT nfaces = tin.GetNumOfTriangles();
    SINT *triList = new SINT[nfaces*3];
    memcpy(triList,tin.GetTriList(),nfaces*3*sizeof(SINT));
    ofstream infile(dst);
    if(!infile)
    {
        cout << "can't read file!\n";
    }
    infile << "#$SMF 1.0" << '\n';
    infile << "#$vertices "<< vecPt3dIn.size() << '\n';
    infile << "#$faces " << nfaces << '\n';
    infile << "#"<< '\n';
    infile << "#---the temp model----" << '\n';
    infile << "#" << '\n';
    infile << "#" << '\n';
    infile << "#" << '\n';


    for(UINT i=0; i < vecPt3dIn.size(); i++)
    {
        infile << "v " << vecPt3dIn[i].X << ' '<< vecPt3dIn[i].Y <<' '<< vecPt3dIn[i].Z << '\n';

    }

    for(UINT j=0; j<nfaces; j++)
    {
        infile << "f "<<triList[3*j]+1 << ' ' << triList[3*j+1]+1<< ' ' << triList[3*j+2]+1 << '\n';
    }

    delete[] triList;

    infile.close();

    return true;




}

void  quadratic_equation(double a4,double b4,double c4,complex<double> &z1,complex<double> &z2)
{

    double delta;
    complex<double> temp1,temp2;

    delta=b4*b4-4*a4*c4;


    complex<double> temp(delta,0);

    temp1 = (-b4)/(2*a4);
    temp2 = sqrt(temp)/(2*a4);

    z1=temp1+temp2;
    z2=temp1-temp2;

}





// 2. 四次方程中所调用的三次方程；
double  cubic_equation(double a3,double b3,double c3,double d3)
{

    double p,q,delta;
    double M,N;
    double y0;

    complex<double> temp1,temp2;
    complex<double> x1,x2,x3;
    complex<double> y1,y2;



    if(a3==0)
    {
        quadratic_equation(b3,c3,d3,y1,y2);

        x1 = y1;
        x2 = y2;
        x3 = 0;
    }
    else
    {

    p = -1.0/3*pow((b3*1.0/a3),2.0)+c3*1.0/a3;
    q = 2.0/27*pow((b3*1.0/a3),3.0)-1.0/3*b3*c3/(a3*a3)+d3*1.0/a3;

    // p = -1/3*(b3/a3)^2+c3/a3;
    // q = 2/27*(b3/a3)^3-1/3*b3*c3/(a3*a3)+d3/a3;
    //化成 y^3+p*y+q=0 形式；


    delta = pow((q/2.0),2.0)+pow((p/3.0),3.0);

    //判别式 delta = (q/2)^2+(p/3)^3;

    //cout<<" delta>0,有一个实根和两个复根；delta=0,有三个实根；delta<0,有三个不等的实根"<<endl;
    //cout<<" please output the discriminant  delta="<<delta<<endl;
    //delta>0,有一个实根和两个复根；
    //delta=0,有三个实根；
    //delta<0,有三个不等的实根。


    complex<double>  omega1(-1.0/2, sqrt(3.0)/2.0);
    complex<double>  omega2(-1.0/2, -sqrt(3.0)/2.0);

    complex<double>  yy(b3/(3.0*a3),0.0);


    M = -q/2.0;

    if(delta<0)
    {

         N = sqrt(fabs(delta));
         complex<double>  s1(M,N);
         complex<double>  s2(M,-N);

         x1 = (pow(s1,(1.0/3))+pow(s2,(1.0/3)))-yy;
         x2 = (pow(s1,(1.0/3))*omega1+pow(s2,(1.0/3))*omega2)-yy;
         x3 = (pow(s1,(1.0/3))*omega2+pow(s2,(1.0/3))*omega1)-yy;

    }
    else
    {

        N = sqrt(delta);

        complex<double>  f1(M+N,0);
        complex<double>  f2(M-N,0);

         if(M+N >= 0)
             temp1 = pow((f1),1.0/3);
         else
             temp1 = -norm(pow(sqrt(f1),1.0/3));


         if(M-N >= 0)
             temp2 = pow((f2),1.0/3);
         else
             temp2 = -norm(pow(sqrt(f2),1.0/3));


         x1 = temp1+temp2-yy;
         x2 = omega1*temp1+omega2*temp2-yy;
         x3 = omega2*temp1+omega1*temp2-yy;

    }

    }

       y0 = real(x1);
    return y0;

    // y0 为所调用的三次方程的返回值；

}

// 3.main函数所调用的四次方程；
void quartic_equation(
                      double a,double b,double c,double d,double e,
                      complex<double> &x1,complex<double> &x2,
                      complex<double> &x3,complex<double> &x4
                      )

{

    double a2,b2,c2;
    double a4,b4,c4;
    double a3,b3,c3,d3;
    double y;
    complex<double>  y1,y2,y3,y4;


    //cout<<"输入一元四次方程 a*x^4+b*x^3+c*x^2+d*x+e=0 的系数 a, b, c, d, e : ";
    //cin>>a>>b>>c>>d>>e;

    //输入一元四次方程的系数，得方程为a*x^4+b*x^3+c*x^2+d*x+e=0；

    if(b==0 && c==0 && d==0 && e==0)
    {
        x1 = 0; x2 = 0; x3 = 0; x4 = 0;
    }
    else if (b==0 && d==0 && e==0)
    {
        b3 = a; c3 = 0; d3 = c;
        quadratic_equation(b3,c3,d3,y1,y2);
        x1 = y1; x2 = y2;  x3 = 0; x4 = 0;
    }
    else
    {
//把任意系数的四次方程化为首项系数为1的四次方程；
     b= b/a;  c= c/a;  d= d/a;  e= e/a; a= a/a;

//所调用的三次方程的系数;
    a3= 8.0;
    b3= -4.0*c;
    c3= 2.0*b*d-8.0*e;
    d3= e*(4.0*c-b*b)-d*d;


    y = cubic_equation(a3,b3,c3,d3);
    //把三次方程的返回值赋给 y ；



 //第一次调用的二次方程的系数；
    a2= 1.0;
    b2= b/2.0-sqrt(8.0*y+b*b-4*c)/2.0;
    c2= y-(b*y-d)/sqrt(8.0*y+b*b-4*c);
    quadratic_equation(a2,b2,c2,y1,y2);

    x1 = y1;
    x2 = y2;


//第二次调用的二次方程的系数；
    a4= 1.0;
    b4= b/2.0+sqrt(8.0*y+b*b-4.0*c)/2.0;
    c4= y+(b*y-d)/sqrt(8.0*y+b*b-4.0*c);
    quadratic_equation(a4,b4,c4,y3,y4);

    x3 = y3;
    x4 = y4;

    }

    //程序结果为任意一元四次方程 a*x^4+b*x^3+c*x^2+d*x+e=0
    //的四个解。
}

bool CmlPhgProc::ml4OnceQuationSolve( DOUBLE da, DOUBLE db, DOUBLE dc, DOUBLE dd, DOUBLE de, vector<DOUBLE> &vecRes  )
{
//-----------------------------------------------
    CvMat *pMatCoef, *pMatRoots;
    pMatCoef = cvCreateMat( 5, 1, CV_64F );



    cvSolvePoly( pMatCoef, pMatRoots );


//--------------------------------
//gsl 版
////    complex<DOUBLE> x1, x2, x3, x4;
////    quartic_equation(  da, db, dc, dd, de, x1, x2, x3, x4);
////    if( abs( x1.imag() ) < 10e-7 )
////    {
////        vecRes.push_back( x1.real() );
////    }
////    if( abs( x2.imag() ) < 10e-7 )
////    {
////        vecRes.push_back( x2.real() );
////    }
////    if( abs( x3.imag() ) < 10e-7 )
////    {
////        vecRes.push_back( x3.real() );
////    }
////    if( abs( x4.imag() ) < 10e-7 )
////    {
////        vecRes.push_back( x4.real() );
////    }
//    DOUBLE dCoef[5] = { de, dd, dc, db, da};
//    DOUBLE dZ[8];
//
////    gsl_poly_complex_workspace *w = gsl_poly_complex_workspace_alloc(5);
////
////    gsl_poly_complex_solve( dCoef, 5, w, dZ );
////
////    gsl_poly_complex_workspace_free(w);
//
//    for( UINT i = 0; i < 4; ++i )
//    {
//        if( abs( dZ[2*i+1] ) < 10e-7 )
//        {
//            vecRes.push_back( dZ[2*i] );
//        }
//    }
//
//    if( vecRes.size() != 0 )
//    {
//        return true;
//    }
//    else
//    {
//        return false;
//    }
//---------------------------------------
	return true;
}
bool CmlPhgProc::mlGetDisBy3Pts( vector<Pt3d> vecGCPs, vector<Pt2d> vecImgPts, DOUBLE dFocalLength, vector<Pt3d> &vecDisXYZ, Pt3d &dDisBLine, Pt3d &dAngle )
{
    if( ( vecGCPs.size() != 3 ) || ( vecGCPs.size() != vecImgPts.size() ) )
    {
        return false;
    }
//    UINT nPtNum = 3;
    Pt2d ptImg1, ptImg2, ptImg3;
    ptImg1.X = vecImgPts[0].X;
    ptImg1.Y = vecImgPts[0].Y;
    ptImg2.X = vecImgPts[1].X;
    ptImg2.Y = vecImgPts[1].Y;
    ptImg3.X = vecImgPts[2].X;
    ptImg3.Y = vecImgPts[2].Y;
    DOUBLE dCos1 = (ptImg2.X*ptImg3.X + ptImg2.Y*ptImg3.Y + dFocalLength*dFocalLength) / sqrt((ptImg2.X*ptImg2.X+ptImg2.Y*ptImg2.Y+dFocalLength*dFocalLength)*(ptImg3.X*ptImg3.X+ptImg3.Y*ptImg3.Y+dFocalLength*dFocalLength));
    DOUBLE dCos2 = (ptImg1.X*ptImg3.X + ptImg1.Y*ptImg3.Y + dFocalLength*dFocalLength) / sqrt((ptImg1.X*ptImg1.X+ptImg1.Y*ptImg1.Y+dFocalLength*dFocalLength)*(ptImg3.X*ptImg3.X+ptImg3.Y*ptImg3.Y+dFocalLength*dFocalLength));
    DOUBLE dCos3 = (ptImg2.X*ptImg1.X + ptImg2.Y*ptImg1.Y + dFocalLength*dFocalLength) / sqrt((ptImg2.X*ptImg2.X+ptImg2.Y*ptImg2.Y+dFocalLength*dFocalLength)*(ptImg1.X*ptImg1.X+ptImg1.Y*ptImg1.Y+dFocalLength*dFocalLength));

    DOUBLE dL1, dL2, dL3;
    Pt3d pt1, pt2, pt3;
    pt1 = vecGCPs[0];
    pt2 = vecGCPs[1];
    pt3 = vecGCPs[2];
    dL1 = sqrt((pt2.X-pt3.X)*(pt2.X-pt3.X)+(pt2.Y-pt3.Y)*(pt2.Y-pt3.Y)+(pt2.Z-pt3.Z)*(pt2.Z-pt3.Z));
    dL2 = sqrt((pt1.X-pt3.X)*(pt1.X-pt3.X)+(pt1.Y-pt3.Y)*(pt1.Y-pt3.Y)+(pt1.Z-pt3.Z)*(pt1.Z-pt3.Z));
    dL3 = sqrt((pt1.X-pt2.X)*(pt1.X-pt2.X)+(pt1.Y-pt2.Y)*(pt1.Y-pt2.Y)+(pt1.Z-pt2.Z)*(pt1.Z-pt2.Z));

    DOUBLE da, db, dc, dd, de;
    da = (pow((dL2*dL2+dL3*dL3-dL1*dL1), 2) - 4*dL2*dL2*dL3*dL3*dCos1*dCos1);
    DOUBLE dK = 2*(dL2*dL2+dL3*dL3-dL1*dL1)*(dL1*dL1+dL3*dL3-dL2*dL2)*dCos3 + 4*dL3*dL3*(dL1*dL1+dL2*dL2-dL3*dL3)*dCos1*dCos2;
    db = dK - 2*da*dCos3;
    de = pow((dL1*dL1+dL3*dL3-dL2*dL2), 2 ) - 4*dL1*dL1*dL3*dL3*dCos2*dCos2;
    dd = dK - 2*de*dCos3;
    dc = da + de -2*dK*dCos3 + 4*pow(dL3,4)*(dCos1*dCos1+dCos2*dCos2+dCos3*dCos3-2*dCos1*dCos2*dCos3 - 1);

    vector<DOUBLE> vecRes;
    if( false == ml4OnceQuationSolve(  da,  db,  dc,  dd,  de, vecRes  ) )
    {
        return false;
    }

    for( UINT i = 0; i < vecRes.size(); ++i )
    {
        DOUBLE dm1 = vecRes[i];
        DOUBLE du = dL3 / sqrt(1-2*dm1*dCos3+dm1*dm1);
        DOUBLE dm2 = ( (dL1*dL1+dL3*dL3-dL2*dL2)+2*(dL2*dL2-dL1*dL1)*dm1*dCos3+(dL1*dL1-dL2*dL2-dL3*dL3)*dm1*dm1 ) / (2*dL3*dL3*(dCos2-dm1*dCos1) );
        DOUBLE dv = dm1*du;
        DOUBLE dw = dm2*du;
        Pt3d ptXYZ;
        ptXYZ.X = du;
        ptXYZ.Y = dv;
        ptXYZ.Z = dw;
        vecDisXYZ.push_back(ptXYZ);
    }
    dDisBLine.X = dL3;
    dDisBLine.Y = dL1;
    dDisBLine.Z = dL2;

    dAngle.X = acos(dCos1);
    dAngle.Y = acos(dCos2);
    dAngle.Z = acos(dCos3);
    return true;
    ///////////////////////////////////




}
bool CmlPhgProc::mlGetXYZCoordBy3DisVal( vector<Pt3d> vecGCPs, Pt3d dDis, Pt3d dAngle, Pt3d &ptXYZ )
{
    if( vecGCPs.size() != 3 )
    {
        return false;
    }
    Pt3d pt1, pt2, pt3;
    pt1 = vecGCPs[0];
    pt2 = vecGCPs[1];
    pt3 = vecGCPs[2];
    DOUBLE du, dv, dw;
    du = dDis.X;
    dv = dDis.Y;
    dw = dDis.Z;
    DOUBLE dCOS1, dCOS2, dCOS3;
    dCOS1 = cos(dAngle.X);
    dCOS2 = cos(dAngle.Y);
    dCOS3 = cos(dAngle.Z);

    CmlMat matA, matL;
    matA.Initial(3, 3);
    matL.Initial(3, 1);

    matA.SetAt( 0, 0, (2*(pt1.X-pt2.X)));
    matA.SetAt( 0, 1, (2*(pt1.Y-pt2.Y)));
    matA.SetAt( 0, 2, (2*(pt1.Z-pt2.Z)));

    DOUBLE dTempL =  dv*dv - du*du + pt1.X*pt1.X + pt1.Y*pt1.Y + pt1.Z*pt1.Z - pt2.X*pt2.X- pt2.Y*pt2.Y- pt2.Z*pt2.Z;
    matL.SetAt( 0, 0, dTempL );

    matA.SetAt( 1, 0, (2*(pt1.X-pt3.X)));
    matA.SetAt( 1, 1, (2*(pt1.Y-pt3.Y)));
    matA.SetAt( 1, 2, (2*(pt1.Z-pt3.Z)));
    dTempL = dw*dw - du*du + pt1.X*pt1.X + pt1.Y*pt1.Y + pt1.Z*pt1.Z - pt3.X*pt3.X- pt3.Y*pt3.Y- pt3.Z*pt3.Z;
    matL.SetAt( 1, 0, dTempL );

    DOUBLE dDeta1, dDeta2, dDeta3;
    dDeta1 = pt1.Y*(pt2.Z-pt3.Z) + pt2.Y*(pt3.Z-pt1.Z) + pt3.Y*(pt1.Z-pt2.Z);
    dDeta2 = pt1.Z*(pt2.X-pt3.X) + pt2.Z*(pt3.X-pt1.X) + pt3.Z*(pt1.X-pt2.X);
    dDeta3 = pt1.X*(pt2.Y-pt3.Y) + pt2.X*(pt3.Y-pt1.Y) + pt3.X*(pt1.Y-pt2.Y);

    matA.SetAt( 2, 0, dDeta1 );
    matA.SetAt( 2, 1, dDeta2 );
    matA.SetAt( 2, 2, dDeta3 );

    dTempL = du*dv*dw*(sqrt(1+2*dCOS1*dCOS2*dCOS3-dCOS1*dCOS1 -dCOS2*dCOS2-dCOS3*dCOS3 ) ) + dDeta1*pt1.X + dDeta2*pt1.Y + dDeta3*pt1.Z;
    matL.SetAt( 2, 0, dTempL );

    CmlMat matX;
    mlMatSolveSVD( &matA, &matL, &matX );
    ptXYZ.X = matX.GetAt( 0, 0 );
    ptXYZ.Y = matX.GetAt( 1, 0 );
    ptXYZ.Z = matX.GetAt( 2, 0 );

    return true;
}
bool CmlPhgProc::mlGetRotateMatByXYZ(  vector<Pt3d> vecGCPs, vector<Pt2d> vecImgPts, Pt3d ptXYZ, DOUBLE dF, OriAngle &oriA )
{
    if( vecGCPs.size() != 3 )
    {
        return false;
    }

    CmlMat matA, matB, matBTemp, matX;

    matA.Initial( 3, 3 );
    matB.Initial( 3, 3 );
    for( UINT i = 0; i < 3; ++i )
    {
        Pt2d ptImgCur = vecImgPts[i];
        Pt3d pt3dCur = vecGCPs[i];
        DOUBLE dPixelDis = sqrt( ptImgCur.X*ptImgCur.X + ptImgCur.Y*ptImgCur.Y+dF*dF );
        DOUBLE d3dDis = sqrt( (pt3dCur.X-ptXYZ.X)*(pt3dCur.X-ptXYZ.X) +(pt3dCur.Y-ptXYZ.Y)*(pt3dCur.Y-ptXYZ.Y) +(pt3dCur.Z-ptXYZ.Z)*(pt3dCur.Z-ptXYZ.Z) );

        matA.SetAt( 0, i, (pt3dCur.X-ptXYZ.X)/d3dDis );
        matA.SetAt( 1, i, (pt3dCur.Y-ptXYZ.Y)/d3dDis );
        matA.SetAt( 2, i, (pt3dCur.Z-ptXYZ.Z)/d3dDis );

        matB.SetAt( 0, i, ptImgCur.X / dPixelDis );
        matB.SetAt( 1, i, ptImgCur.Y / dPixelDis );
        matB.SetAt( 2, i, -dF / dPixelDis );
    }
    mlMatInv( &matB, &matBTemp );
    mlMatMul( &matA, &matBTemp, &matX );

    RMat2OPK(  &matX, &oriA );
    return true;
}
////////////////////////////////////////////////////////////////////////////////////////////


//判断两个点(或向量)是否相等
bool operator==(const Pt2d &pt1, const Pt2d &pt2)
{
    return ( (abs(pt1.X - pt2.X) < 10e-7 ) && (abs(pt1.Y - pt2.Y) < 10e-7 ) );
}
bool operator==(const Pt3d &pt1, const Pt3d &pt2)
{
    return ( (abs(pt1.X - pt2.X) < 10e-7 ) && (abs(pt1.Y - pt2.Y) < 10e-7 ) );
}
// 比较向量中哪个与x轴向量(1, 0)的夹角更大
bool CompareVector(const Pt2d &pt1, const Pt2d &pt2)
{
    //求向量的模
    DOUBLE m1 = sqrt((pt1.X * pt1.X + pt1.Y * pt1.Y));
    DOUBLE m2 = sqrt((pt2.X * pt2.X + pt2.Y * pt2.Y));
    //两个向量分别与(1, 0)求内积
    DOUBLE v1 = pt1.X / m1;
    DOUBLE v2 = pt2.X / m2;
    //如果向量夹角相等，则返回离基点较近的一个，保证有序
    return ( (v1 > v2 )|| ((v1 == v2) && ( m1 < m2 ) ));
}
bool CompareVector3D(const Pt3d &pt1, const Pt3d &pt2)
{
    //求向量的模
    DOUBLE m1 = sqrt((pt1.X * pt1.X + pt1.Y * pt1.Y));
    DOUBLE m2 = sqrt((pt2.X * pt2.X + pt2.Y * pt2.Y));
    //两个向量分别与(1, 0)求内积
    DOUBLE v1 = pt1.X / m1;
    DOUBLE v2 = pt2.X / m2;
    //如果向量夹角相等，则返回离基点较近的一个，保证有序
    return ( (v1 > v2 )|| ((v1 == v2) && ( m1 < m2 ) ));
}
//计算凸包
bool CmlPhgProc::CalcConvexHull(vector<Pt3d> vecSrc, vector<Pt3d> &vecDst)
{
    if( vecSrc.size() < 3 )
    {
        return false;
    }
    SINT nPtCount = vecSrc.size();
    CvPoint* pPt = new CvPoint[nPtCount];
    SINT *pIdx = new SINT[nPtCount];
    SINT nIdxNum = 0;
    for( UINT i = 0; i < nPtCount; ++i )
    {
        Pt3d ptCur = vecSrc[i];
        pPt[i].x = ptCur.X;
        pPt[i].y = ptCur.Y;
    }
    cvConvexHull( pPt, nPtCount, NULL, CV_COUNTER_CLOCKWISE, pIdx, &nIdxNum );
    for( UINT i = 0; i < nIdxNum; ++i )
    {
        Pt3d ptCur;
        ptCur.X = pPt[pIdx[i]].x;
        ptCur.Y = pPt[pIdx[i]].y;
        vecDst.push_back( ptCur );
    }
    delete[] pPt;
    delete[] pIdx;
//=================================================
//opensrc
//    //点集中至少应有3个点，才能构成多边形
//    vecDst = vecSrc;
//    if (vecDst.size() < 3)
//    {
//        return false;
//    }
//    //查找基点
//    Pt3d ptBase = vecDst.front(); //将第1个点预设为最小点
//    for (PTARRAY_V::iterator i = vecDst.begin() + 1; i != vecDst.end(); ++i)
//    {
//        //如果当前点的y值小于最小点，或y值相等，x值较小
//        if (( i->Y < ptBase.Y )|| ((i->Y == ptBase.Y) && (i->X > ptBase.X)))
//        {
//            //将当前点作为最小点
//            ptBase = *i;
//        }
//    }
//    //计算出各点与基点构成的向量
//    for (PTARRAY_V::iterator i = vecDst.begin(); i != vecDst.end();)
//    {
//        //排除与基点相同的点，避免后面的排序计算中出现除0错误
//        if (*i == ptBase)
//        {
//            i = vecDst.erase(i);
//        }
//        else
//        {
//            //方向由基点到目标点
//            i->X -= ptBase.X, i->Y -= ptBase.Y;
//            ++i;
//        }
//    }
//    //按各向量与横坐标之间的夹角排序
//    sort(vecDst.begin(), vecDst.end(), &CompareVector3D);
//    //删除相同的向量
//    vecDst.erase(unique(vecDst.begin(), vecDst.end()), vecDst.end());
//    //计算得到首尾依次相联的向量
//    if( vecDst.size() <= 1 )
//    {
//        vecDst.clear();
//        return false;
//    }
//    for (PTARRAY_V::reverse_iterator ri = vecDst.rbegin(); ri != vecDst.rend() - 1; ++ri)
//    {
//        PTARRAY_V::reverse_iterator riNext = ri + 1;
//        //向量三角形计算公式
//        ri->X -= riNext->X, ri->Y -= riNext->Y;
//    }
//    //依次删除不在凸包上的向量
//    for (PTARRAY_V::iterator i = vecDst.begin() + 1; i != vecDst.end(); ++i)
//    {
//        //回溯删除旋转方向相反的向量，使用外积判断旋转方向
//        for (PTARRAY_V::iterator iLast = i - 1; iLast != vecDst.begin();)
//        {
//            DOUBLE v1 = i->X * iLast->Y, v2 = i->Y * iLast->X;
//            //如果叉积小于0，则无没有逆向旋转
//            //如果叉积等于0，还需判断方向是否相逆
//            if (v1 < v2 || (v1 == v2 && i->X * iLast->X > 0 &&
//                i->Y * iLast->Y > 0))
//            {
//                break;
//            }
//            //删除前一个向量后，需更新当前向量，与前面的向量首尾相连
//            //向量三角形计算公式
//            i->X += iLast->X, i->Y += iLast->Y;
//            iLast = (i = vecDst.erase(iLast)) - 1;
//        }
//    }
//    //将所有首尾相连的向量依次累加，换算成坐标
//    vecDst.front().X += ptBase.X, vecDst.front().Y += ptBase.Y;
//    for (PTARRAY_V::iterator i = vecDst.begin() + 1; i != vecDst.end(); ++i)
//    {
//        i->X += (i - 1)->X, i->Y += (i - 1)->Y;
//    }
//    //添加基点，全部的凸包计算完成
//    vecDst.push_back(ptBase);
    return true;
}

bool CmlPhgProc::CalcConvexHull(vector<Pt2d> vecSrc, vector<Pt2d> &vecDst)
{
    if( vecSrc.size() < 3 )
    {
        return false;
    }
    SINT nPtCount = vecSrc.size();
    CvPoint* pPt = new CvPoint[nPtCount];
    SINT *pIdx = new SINT[nPtCount];
    SINT nIdxNum = 0;
    for( UINT i = 0; i < nPtCount; ++i )
    {
        Pt2d ptCur = vecSrc[i];
        pPt[i].x = ptCur.X;
        pPt[i].y = ptCur.Y;
    }
    cvConvexHull( pPt, nPtCount, NULL, CV_COUNTER_CLOCKWISE, pIdx, &nIdxNum );
    for( UINT i = 0; i < nIdxNum; ++i )
    {
        Pt2d ptCur;
        ptCur.X = pPt[pIdx[i]].x;
        ptCur.Y = pPt[pIdx[i]].y;
        vecDst.push_back( ptCur );
    }
    delete[] pPt;
    delete[] pIdx;
//=================================================
//opensrc
    //点集中至少应有3个点，才能构成多边形
//    vecDst = vecSrc;
//    if (vecDst.size() < 3)
//    {
//        return false;
//    }
//    //查找基点
//    Pt2d ptBase = vecDst.front(); //将第1个点预设为最小点
//    for (PTARRAY::iterator i = vecDst.begin() + 1; i != vecDst.end(); ++i)
//    {
//        //如果当前点的y值小于最小点，或y值相等，x值较小
//        if (( i->Y < ptBase.Y )|| ((i->Y == ptBase.Y) && (i->X > ptBase.X)))
//        {
//            //将当前点作为最小点
//            ptBase = *i;
//        }
//    }
//    //计算出各点与基点构成的向量
//    for (PTARRAY::iterator i = vecDst.begin(); i != vecDst.end();)
//    {
//        //排除与基点相同的点，避免后面的排序计算中出现除0错误
//        if (*i == ptBase)
//        {
//            i = vecDst.erase(i);
//        }
//        else
//        {
//            //方向由基点到目标点
//            i->X -= ptBase.X, i->Y -= ptBase.Y;
//            ++i;
//        }
//    }
//    //按各向量与横坐标之间的夹角排序
//    sort(vecDst.begin(), vecDst.end(), &CompareVector);
//    //删除相同的向量
//    vecDst.erase(unique(vecDst.begin(), vecDst.end()), vecDst.end());
//    //计算得到首尾依次相联的向量
//    if( vecDst.size() <= 1 )
//    {
//        vecDst.clear();
//        return false;
//    }
//    for (PTARRAY::reverse_iterator ri = vecDst.rbegin(); ri != vecDst.rend() - 1; ++ri)
//    {
//        PTARRAY::reverse_iterator riNext = ri + 1;
//        //向量三角形计算公式
//        ri->X -= riNext->X, ri->Y -= riNext->Y;
//    }
//    //依次删除不在凸包上的向量
//    for (PTARRAY::iterator i = vecDst.begin() + 1; i != vecDst.end(); ++i)
//    {
//        //回溯删除旋转方向相反的向量，使用外积判断旋转方向
//        for (PTARRAY::iterator iLast = i - 1; iLast != vecDst.begin();)
//        {
//            DOUBLE v1 = i->X * iLast->Y, v2 = i->Y * iLast->X;
//            //如果叉积小于0，则无没有逆向旋转
//            //如果叉积等于0，还需判断方向是否相逆
//            if (v1 < v2 || (v1 == v2 && i->X * iLast->X > 0 &&
//                i->Y * iLast->Y > 0))
//            {
//                break;
//            }
//            //删除前一个向量后，需更新当前向量，与前面的向量首尾相连
//            //向量三角形计算公式
//            i->X += iLast->X, i->Y += iLast->Y;
//            iLast = (i = vecDst.erase(iLast)) - 1;
//        }
//    }
//    //将所有首尾相连的向量依次累加，换算成坐标
//    vecDst.front().X += ptBase.X, vecDst.front().Y += ptBase.Y;
//    for (PTARRAY::iterator i = vecDst.begin() + 1; i != vecDst.end(); ++i)
//    {
//        i->X += (i - 1)->X, i->Y += (i - 1)->Y;
//    }
//    //添加基点，全部的凸包计算完成
//    vecDst.push_back(ptBase);
    return true;
}
////////////////////////////////////////////////////////////////////////////////////////////////

bool CmlPhgProc::ExOriTrans( ExOriPara* pExOriL, ExOriPara* pExOriRela, ExOriPara* pExOriR )
{
    if( ( pExOriL == NULL )||( pExOriRela == NULL )||( pExOriR == NULL ) )
    {
        return false;
    }
    CmlMat matL, matRela, matR;
    if( ( true == OPK2RMat( &pExOriRela->ori, &matRela ) ) &&
        ( true == OPK2RMat( &pExOriL->ori, &matL )) &&
        ( true == mlMatMul( &matL, &matRela, &matR )) &&
        ( true == RMat2OPK( &matR, &pExOriR->ori)) )
    {
        CmlMat matPosRela, matPosL, matTemp, matPosR;
        if( false == matPosL.Initial( 3, 1 ))
        {
            return false;
        }
        matPosL.SetAt( 0, 0, pExOriL->pos.X );
        matPosL.SetAt( 1, 0, pExOriL->pos.Y );
        matPosL.SetAt( 2, 0, pExOriL->pos.Z );

        if( false == matPosRela.Initial( 3, 1 ) )
        {
            return false;
        }
        matPosRela.SetAt( 0, 0, pExOriRela->pos.X );
        matPosRela.SetAt( 1, 0, pExOriRela->pos.Y );
        matPosRela.SetAt( 2, 0, pExOriRela->pos.Z );

        if( ( true == mlMatMul( &matL, &matPosRela, &matTemp ) ) &&
            ( true == mlMatAdd( &matTemp, &matPosL, &matPosR) ) )
        {
            pExOriR->pos.X = matPosR.GetAt( 0, 0);
            pExOriR->pos.Y = matPosR.GetAt( 1, 0);
            pExOriR->pos.Z = matPosR.GetAt( 2, 0);
            return true;
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }
}

bool CmlPhgProc::GetRelaOri( ExOriPara* pExOriL, ExOriPara* pExOriR, ExOriPara* pExOriRela )
{
    if( ( pExOriL == NULL )||( pExOriRela == NULL )||( pExOriR == NULL ) )
    {
        return false;
    }
    CmlMat matL, matR, matRela;
    CmlMat matTTemp, matTRela;
    CmlMat matLInv;

    if( ( true == OPK2RMat( &pExOriL->ori, &matL ) )&&
        ( true == OPK2RMat( &pExOriR->ori, &matR ) )&&
        ( true == mlMatInv( &matL, &matLInv ) )&&
        ( true == mlMatMul( &matLInv, &matR, &matRela ) )&&
        ( true == RMat2OPK( &matRela, &pExOriRela->ori )) )
    {
        if( true == matTTemp.Initial( 3, 1 ) )
        {
            matTTemp.SetAt( 0, 0, (pExOriR->pos.X - pExOriL->pos.X ) );
            matTTemp.SetAt( 1, 0, (pExOriR->pos.Y - pExOriL->pos.Y ) );
            matTTemp.SetAt( 2, 0, (pExOriR->pos.Z - pExOriL->pos.Z ) );
        }
        else
        {
            return false;
        }

        if( true == mlMatMul( &matLInv, &matTTemp, &matTRela ) )
        {
            pExOriRela->pos.X = matTRela.GetAt( 0, 0 );
            pExOriRela->pos.Y = matTRela.GetAt( 1, 0 );
            pExOriRela->pos.Z = matTRela.GetAt( 2, 0 );
            return true;
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }

}
bool CmlPhgProc::mlSolvePts( vector<Pt3d> vecOldPts, vector<Pt3d> vecNewPts, UINT nTimes, ExOriPara* pInitialOri )
{
    if( ( vecOldPts.size() < 3 )||( vecNewPts.size() < 3)||( vecOldPts.size() != vecNewPts.size() ) )
    {
        return false;
    }
    UINT nPts = vecOldPts.size();
    if( pInitialOri == NULL )
    {
        return false;
    }
    CmlMat matA, matL, matX, matR;

    if( ( false == matA.Initial( nPts*3, 6 ) )|| ( false == matL.Initial( nPts*3, 1 ) )|| ( false == matX.Initial( 6, 1 ) ))
    {
        return false;
    }


    for ( UINT k = 0; k < nTimes; ++k )
	{
		for ( UINT i = 0; i < nPts; ++i )
		{
		    Pt3d ptCurOld = vecOldPts[i];
			DOUBLE x = ptCurOld.X;
			DOUBLE y = ptCurOld.Y;
			DOUBLE z = ptCurOld.Z;

			DOUBLE dData = 0;

			DOUBLE dO = pInitialOri->ori.omg;
			DOUBLE dF = pInitialOri->ori.phi;
			DOUBLE dK = pInitialOri->ori.kap;
            if( false == OPK2RMat( &pInitialOri->ori, &matR ) )
            {
                return false;
            }

			dData = 0;
			matA.SetAt( 3*i, 0, dData );

			dData = -sin(dF)*cos(dK)*x + sin(dF)*sin(dK)*y + cos(dF)*z;
			matA.SetAt( 3*i, 1, dData );

			dData = -cos(dF)*sin(dK)*x - cos(dF)*cos(dK)*y;
			matA.SetAt( 3*i, 2, dData );

            matA.SetAt( 3*i, 3, 1 );
            matA.SetAt( 3*i, 4, 0 );
            matA.SetAt( 3*i, 5, 0 );

			dData = ( -sin(dO)*sin(dK) + cos(dO)*sin(dF)*cos(dK) )*x + (-sin(dO)*cos(dK) - cos(dO)*sin(dF)*sin(dK) )*y + ( -cos(dO)*cos(dF))*z;
			matA.SetAt( 3*i+1, 0, dData );

			dData = ( sin(dO)*cos(dF)*cos(dK) )*x + (-sin(dO)*cos(dF)*sin(dK) )*y + (sin(dO)*sin(dF))*z;
            matA.SetAt( 3*i+1, 1, dData );

			dData = ( cos(dO)*cos(dK) - sin(dO)*sin(dF)*sin(dK) )*x + ( -cos(dO)*sin(dK) - sin(dO)*sin(dF)*cos(dK))*y;
			matA.SetAt( 3*i+1, 2, dData );

			matA.SetAt( 3*i+1, 3, 0 );
            matA.SetAt( 3*i+1, 4, 1 );
            matA.SetAt( 3*i+1, 5, 0 );

			dData = ( cos(dO)*sin(dK) + sin(dO)*sin(dF)*cos(dK) )*x + ( cos(dO)*cos(dK) - sin(dO)*sin(dF)*sin(dK) )*y + ( -sin(dO)*cos(dF))*z;
			matA.SetAt( 3*i+2, 0, dData );

			dData = ( -cos(dO)*cos(dF)*cos(dK) )*x + ( cos(dO)*cos(dF)*sin(dK) )*y + ( -cos(dO)*sin(dF) )*z;
			matA.SetAt( 3*i+2, 1, dData );

			dData = ( sin(dO)*cos(dK) + cos(dO)*sin(dF)*sin(dK) )*x + ( -sin(dO)+sin(dK) + cos(dO)*sin(dF)*cos(dK) )*y;
			matA.SetAt( 3*i+2, 2, dData );

            matA.SetAt( 3*i+2, 3, 0 );
            matA.SetAt( 3*i+2, 4, 0 );
            matA.SetAt( 3*i+2, 5, 1 );

			//==========================================
			Pt3d ptCurNew = vecNewPts[i];
			dData = ptCurNew.X - matR.GetAt( 0, 0 )*x - matR.GetAt( 0, 1 )*y - matR.GetAt( 0, 2 )*z - pInitialOri->pos.X;
			matL.SetAt( 3*i, 0, dData );

			dData = ptCurNew.Y - matR.GetAt( 1, 0 )*x - matR.GetAt( 1, 1 )*y - matR.GetAt( 1, 2 )*z - pInitialOri->pos.Y;
            matL.SetAt( 3*i+1, 0, dData );

			dData = ptCurNew.Z - matR.GetAt( 2, 0 )*x - matR.GetAt( 2, 1 )*y - matR.GetAt( 2, 2 )*z - pInitialOri->pos.Z;
            matL.SetAt( 3*i+2, 0, dData );
		}

		mlMatSolveSVD( &matA, &matL, &matX );

        pInitialOri->ori.omg += matX.GetAt( 0, 0 );
        pInitialOri->ori.phi += matX.GetAt( 1, 0 );
        pInitialOri->ori.kap += matX.GetAt( 2, 0 );

        pInitialOri->pos.X += matX.GetAt( 3, 0 );
        pInitialOri->pos.Y += matX.GetAt( 4, 0 );
        pInitialOri->pos.Z += matX.GetAt( 5, 0 );

    }
	return 1;
}
bool CmlPhgProc::GetFitPlaneNormal( vector<Pt3d> vecXYZ, Pt3d &pPlaneNormal1, Pt3d &pPlaneNormal2 )
{
    if( vecXYZ.size() < 3 )
    {
        return false;
    }
    CmlMat matA, matL, matX;
    if( ( false == matA.Initial( 3, 3 ) )||(( false == matL.Initial( 3, 1 )) ) )
    {
        return false;
    }
    DOUBLE dR[9], dL[3];
    for( UINT i = 0; i < 9; ++i )
    {
        dR[i] = 0;
    }
    for( UINT i = 0; i < 3; ++i )
    {
        dL[i] = 0;
    }
    for( UINT i = 0; i < vecXYZ.size(); ++i )
    {
        Pt3d ptCur = vecXYZ[i];

        dR[0] += ptCur.X * ptCur.X;
        dR[1] += ptCur.X * ptCur.Y;
        dR[2] += ptCur.X * ptCur.Z;

        dR[3] += ptCur.X * ptCur.Y;
        dR[4] += ptCur.Y * ptCur.Y;
        dR[5] += ptCur.Y * ptCur.Z;

        dR[6] += ptCur.X * ptCur.Z;
        dR[7] += ptCur.Y * ptCur.Z;
        dR[8] += ptCur.Z * ptCur.Z;

        dL[0] += -ptCur.X;
        dL[1] += -ptCur.Y;
        dL[2] += -ptCur.Z;
    }
    //dR[8] = vecXYZ.size();

    memcpy( matA.GetData(), dR, sizeof(DOUBLE)*9 );
    memcpy( matL.GetData(), dL, sizeof(DOUBLE)*3 );

    if( false == mlMatSolveSVD( &matA, &matL, &matX ) )
    {
        return false;
    }
    DOUBLE dX = matX.GetAt( 0, 0 );
    DOUBLE dY = matX.GetAt( 1, 0 );
    DOUBLE dZ = matX.GetAt( 2, 0 );

    DOUBLE dTCoef = sqrt( dX * dX + dY * dY + dZ * dZ );
    dX = dX / dTCoef;
    dY = dY / dTCoef;
    dZ = dZ / dTCoef;

    pPlaneNormal1.X = dX;
    pPlaneNormal1.Y = dY;
    pPlaneNormal1.Z = dZ;

    pPlaneNormal2.X = -1.0*dX;
    pPlaneNormal2.Y = -1.0*dY;
    pPlaneNormal2.Z = -1.0*dZ;

    return true;
}
bool CmlPhgProc::GetAffineTCoef( vector<StereoMatchPt> vecMPts, DOUBLE &dA, DOUBLE &dB, DOUBLE &dC, DOUBLE &dD, DOUBLE &dXAccu, DOUBLE &dYAccu  )
{
    UINT nPtNum = vecMPts.size();
    if( nPtNum < 2 )
    {
        return false;
    }
    CmlMat matA, matL, matX;
    if( ( false == matA.Initial( nPtNum*2, 4 ) )||( false == matL.Initial( nPtNum*2, 1 ) ) )
    {
        return false;
    }

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

    if( false == mlMatSolveSVD( &matA, &matL, &matX ) )
    {
        return false;
    }
    dA = matX.GetAt( 0, 0 );
    dB = matX.GetAt( 1, 0 );
    dC = matX.GetAt( 2, 0 );
    dD = matX.GetAt( 3, 0 );

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
    dXAccu = sqrt( dXALL / nPtNum );
    dYAccu = sqrt( dYALL / nPtNum );

    return true;

}
bool CmlPhgProc::GetTargetPtPos( Pt2d ptCent, DOUBLE dA, DOUBLE dB, DOUBLE dC, DOUBLE dD, Pt2d &ptOutRes )
{
    ptOutRes.X = ptCent.X * dA - ptCent.Y * dB + dC;
    ptOutRes.Y = ptCent.X * dB + ptCent.Y * dA + dD;
    return true;
}

////////////////////////////////////////////////////////////////
bool calcOriByMat( ExOriPara oriBase, stuInsMat mat, ExOriPara &oriNew )
{
    CmlPhgProc clsPhg;
    ExOriPara ori1;
    DOUBLE dR1[9], dT1[3];
    dR1[0] = cos(Deg2Rad(mat.dOriMatrix[0]));
    dR1[3] = cos(Deg2Rad(mat.dOriMatrix[1]));
    dR1[6] = cos(Deg2Rad(mat.dOriMatrix[2]));
    dR1[1] = cos(Deg2Rad(mat.dOriMatrix[3]));
    dR1[4] = cos(Deg2Rad(mat.dOriMatrix[4]));
    dR1[7] = cos(Deg2Rad(mat.dOriMatrix[5]));
    dR1[2] = cos(Deg2Rad(mat.dOriMatrix[6]));
    dR1[5] = cos(Deg2Rad(mat.dOriMatrix[7]));
    dR1[8] = cos(Deg2Rad(mat.dOriMatrix[8]));
    DOUBLE dAngle1[3];
    clsPhg.GetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];

    ori1.pos.X = mat.dPosMatrix[0] / 1000.0;
    ori1.pos.Y = mat.dPosMatrix[1] / 1000.0;
    ori1.pos.Z = mat.dPosMatrix[2] / 1000.0;

    return clsPhg.ExOriTrans( &oriBase, &ori1, &oriNew );
}

//计算本体系中相机的姿态,本体系为北东地坐标系
bool CmlPhgProc::CalcCamOriByGivenInstallMatrix( DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                                 stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                                 ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    stuInsMat matBase;
    matBase.dOriMatrix[0] = 1;
    matBase.dOriMatrix[1] = 0;
    matBase.dOriMatrix[2] = 0;
    matBase.dOriMatrix[3] = 0;
    matBase.dOriMatrix[4] = 1;
    matBase.dOriMatrix[5] = 0;
    matBase.dOriMatrix[6] = 0;
    matBase.dOriMatrix[7] = 0;
    matBase.dOriMatrix[8] = 1;

    matBase.dPosMatrix[0] = 0;
    matBase.dPosMatrix[1] = 0;
    matBase.dPosMatrix[2] = 0;
    return this->CalcCamOriInWorldByGivenInsMat( matBase, dExpAngle, dYawAngle, dPitchAngle, matMastExp2Body, matMastYaw2Exp, matMastPitch2Yaw, \
                                                   matLCamBase2Pitch, matRCamBase2LCamBase, matLCamCap2Base, matRCamCap2Base, exOriCamL, exOriCamR );
}
bool CmlPhgProc::CalcCamOriByGivenInstallMatrixVT( DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matLCamBase2LCamCap, stuInsMat matRCamCap2LCamCap, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    stuInsMat matBase;
    matBase.dOriMatrix[0] = 1;
    matBase.dOriMatrix[1] = 0;
    matBase.dOriMatrix[2] = 0;
    matBase.dOriMatrix[3] = 0;
    matBase.dOriMatrix[4] = 1;
    matBase.dOriMatrix[5] = 0;
    matBase.dOriMatrix[6] = 0;
    matBase.dOriMatrix[7] = 0;
    matBase.dOriMatrix[8] = 1;

    matBase.dPosMatrix[0] = 0;
    matBase.dPosMatrix[1] = 0;
    matBase.dPosMatrix[2] = 0;
    return this->CalcCamOriInWorldByGivenInsMatVT( matBase, dExpAngle, dYawAngle, dPitchAngle, matMastExp2Body, matMastYaw2Exp, matMastPitch2Yaw, \
                                                   matLCamBase2Pitch, matLCamBase2LCamCap, matRCamCap2LCamCap, exOriCamL, exOriCamR );

}
bool CmlPhgProc::CalcCamOriInWorldByGivenInsMat( stuInsMat matBase, DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    CmlPhgProc clsPhg;
    //先由本体系转至东北天坐标系，也可不需要
    ExOriPara oriBase, oriExp;

    CmlMat matR;
    if( false == matR.Initial( 3, 3 ) )
    {
        return false;
    }
    memcpy( matR.GetData(), matBase.dOriMatrix, sizeof(DOUBLE)*9 );
    if( false == RMat2OPK( &matR, &oriBase.ori ) )
    {
        return false;
    }
    oriBase.pos.X = matBase.dPosMatrix[0];
    oriBase.pos.Y = matBase.dPosMatrix[1];
    oriBase.pos.Z = matBase.dPosMatrix[2];

    //桅杆相对本体矩阵
    calcOriByMat( oriBase, matMastExp2Body, oriExp );

    //桅杆展开角度
    ExOriPara oriExpSet, oriExpRes;
    oriExpSet.ori.phi = Deg2Rad(dExpAngle);

    clsPhg.ExOriTrans( &oriExp, &oriExpSet, &oriExpRes );

    //偏航相对桅杆
    ExOriPara oriYaw;
    calcOriByMat( oriExpRes, matMastYaw2Exp, oriYaw );

    //偏航角旋转角度
    ExOriPara oriYawSet, oriYawRes;
    oriYawSet.ori.kap = Deg2Rad(dYawAngle);//-59.9670
    clsPhg.ExOriTrans( &oriYaw, &oriYawSet, &oriYawRes );

    //俯仰相对偏航
    ExOriPara oriPitch;
    calcOriByMat( oriYawRes, matMastPitch2Yaw, oriPitch );

    //俯仰角旋转角度
    ExOriPara oriPitchSet, oriPitchRes;
    oriPitchSet.ori.phi = Deg2Rad(dPitchAngle);//
    clsPhg.ExOriTrans( &oriPitch, &oriPitchSet, &oriPitchRes );

    //基准镜相对俯仰
    ExOriPara oriLCamBase;
    calcOriByMat( oriPitchRes, matLCamBase2Pitch, oriLCamBase );

    //右相机相对左相机基准镜的姿态
    //相对姿态
    //--------------------------------
    ExOriPara oriRCamBase;
    calcOriByMat( oriLCamBase, matRCamBase2LCamBase, oriRCamBase );

    //相机相对基准镜
    ExOriPara oriLCamCap, oriRCamCap;
    calcOriByMat( oriLCamBase, matLCamCap2Base, oriLCamCap );
    calcOriByMat( oriRCamBase, matRCamCap2Base, oriRCamCap );

    ExOriPara oriCamBody2CamCap;
    oriCamBody2CamCap.ori.omg = Deg2Rad(180);

    clsPhg.ExOriTrans( &oriLCamCap, &oriCamBody2CamCap, &exOriCamL );
    clsPhg.ExOriTrans( &oriRCamCap, &oriCamBody2CamCap, &exOriCamR );

    return true;
}
bool CmlPhgProc::CalcCamOriInWorldByGivenInsMatVT( stuInsMat matBase, DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matLCamBase2LCamCap, stuInsMat matRCamCap2LCamCap, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR  )
{
   CmlPhgProc clsPhg;
    //先由本体系转至东北天坐标系，也可不需要
    ExOriPara oriBase, oriExp;

    CmlMat matR;
    if( false == matR.Initial( 3, 3 ) )
    {
        return false;
    }
    memcpy( matR.GetData(), matBase.dOriMatrix, sizeof(DOUBLE)*9 );
    if( false == RMat2OPK( &matR, &oriBase.ori ) )
    {
        return false;
    }
    oriBase.pos.X = matBase.dPosMatrix[0];
    oriBase.pos.Y = matBase.dPosMatrix[1];
    oriBase.pos.Z = matBase.dPosMatrix[2];

    //桅杆相对本体矩阵
    calcOriByMat( oriBase, matMastExp2Body, oriExp );

    //桅杆展开角度
    ExOriPara oriExpSet, oriExpRes;
    oriExpSet.ori.phi = Deg2Rad(dExpAngle);

    clsPhg.ExOriTrans( &oriExp, &oriExpSet, &oriExpRes );

    //偏航相对桅杆
    ExOriPara oriYaw;
    calcOriByMat( oriExpRes, matMastYaw2Exp, oriYaw );

    //偏航角旋转角度
    ExOriPara oriYawSet, oriYawRes;
    oriYawSet.ori.kap = Deg2Rad(dYawAngle);//-59.9670
    clsPhg.ExOriTrans( &oriYaw, &oriYawSet, &oriYawRes );

    //俯仰相对偏航
    ExOriPara oriPitch;
    calcOriByMat( oriYawRes, matMastPitch2Yaw, oriPitch );

    //俯仰角旋转角度
    ExOriPara oriPitchSet, oriPitchRes;
    oriPitchSet.ori.phi = Deg2Rad(dPitchAngle);//
    clsPhg.ExOriTrans( &oriPitch, &oriPitchSet, &oriPitchRes );

    //基准镜相对俯仰
    ExOriPara oriLCamBase;
    calcOriByMat( oriPitchRes, matLCamBase2Pitch, oriLCamBase );

    //右相机相对左相机基准镜的姿态
    //相对姿态
    //--------------------------------
    DOUBLE dMatLCamCapToLCamBase[9];
    dMatLCamCapToLCamBase[0] = matLCamBase2LCamCap.dOriMatrix[0];
    dMatLCamCapToLCamBase[3] = matLCamBase2LCamCap.dOriMatrix[1];
    dMatLCamCapToLCamBase[6] = matLCamBase2LCamCap.dOriMatrix[2];

    dMatLCamCapToLCamBase[1] = matLCamBase2LCamCap.dOriMatrix[3];
    dMatLCamCapToLCamBase[4] = matLCamBase2LCamCap.dOriMatrix[4];
    dMatLCamCapToLCamBase[7] = matLCamBase2LCamCap.dOriMatrix[5];

    dMatLCamCapToLCamBase[2] = matLCamBase2LCamCap.dOriMatrix[6];
    dMatLCamCapToLCamBase[5] = matLCamBase2LCamCap.dOriMatrix[7];
    dMatLCamCapToLCamBase[8] = matLCamBase2LCamCap.dOriMatrix[8];

    DOUBLE dOPKAngleLCamCap2LBase[3];
    clsPhg.GetOPKAngle( dMatLCamCapToLCamBase, dOPKAngleLCamCap2LBase );
    ExOriPara exOriLCamCap2LBase;
    exOriLCamCap2LBase.ori.omg = dOPKAngleLCamCap2LBase[0];
    exOriLCamCap2LBase.ori.phi = dOPKAngleLCamCap2LBase[1];
    exOriLCamCap2LBase.ori.kap = dOPKAngleLCamCap2LBase[2];

    exOriLCamCap2LBase.pos.X = matLCamBase2LCamCap.dPosMatrix[0] / 1000.0;
    exOriLCamCap2LBase.pos.Y = matLCamBase2LCamCap.dPosMatrix[1] / 1000.0;
    exOriLCamCap2LBase.pos.Z = matLCamBase2LCamCap.dPosMatrix[2] / 1000.0;

    ExOriPara oriLCamCap;
    clsPhg.ExOriTrans( &oriLCamBase, &exOriLCamCap2LBase, &oriLCamCap );

    //-----------------------------------------
    DOUBLE dMatRCamCap2LCamCap[9];
    dMatRCamCap2LCamCap[0] = matRCamCap2LCamCap.dOriMatrix[0];
    dMatRCamCap2LCamCap[1] = matRCamCap2LCamCap.dOriMatrix[1];
    dMatRCamCap2LCamCap[2] = matRCamCap2LCamCap.dOriMatrix[2];

    dMatRCamCap2LCamCap[3] = matRCamCap2LCamCap.dOriMatrix[3];
    dMatRCamCap2LCamCap[4] = matRCamCap2LCamCap.dOriMatrix[4];
    dMatRCamCap2LCamCap[5] = matRCamCap2LCamCap.dOriMatrix[5];

    dMatRCamCap2LCamCap[6] = matRCamCap2LCamCap.dOriMatrix[6];
    dMatRCamCap2LCamCap[7] = matRCamCap2LCamCap.dOriMatrix[7];
    dMatRCamCap2LCamCap[8] = matRCamCap2LCamCap.dOriMatrix[8];

    DOUBLE dOPKAngleRCamCap2LCamCap[3];
    clsPhg.GetOPKAngle( dMatRCamCap2LCamCap, dOPKAngleRCamCap2LCamCap );
    ExOriPara exOriRCamCap2LCamCap;
    exOriRCamCap2LCamCap.ori.omg = dOPKAngleRCamCap2LCamCap[0];
    exOriRCamCap2LCamCap.ori.phi = dOPKAngleRCamCap2LCamCap[1];
    exOriRCamCap2LCamCap.ori.kap = dOPKAngleRCamCap2LCamCap[2];

    exOriRCamCap2LCamCap.pos.X = matRCamCap2LCamCap.dPosMatrix[0] / 1000.0;
    exOriRCamCap2LCamCap.pos.Y = matRCamCap2LCamCap.dPosMatrix[1] / 1000.0;
    exOriRCamCap2LCamCap.pos.Z = matRCamCap2LCamCap.dPosMatrix[2] / 1000.0;

    ExOriPara oriRCamCap;
    clsPhg.ExOriTrans( &oriLCamCap, &exOriRCamCap2LCamCap, &oriRCamCap );
    ExOriPara oriCamBody2CamCap;
    oriCamBody2CamCap.ori.omg = Deg2Rad( 180.0 );

    clsPhg.ExOriTrans( &oriLCamCap, &oriCamBody2CamCap, &exOriCamL );
    clsPhg.ExOriTrans( &oriRCamCap, &oriCamBody2CamCap, &exOriCamR );

    return true;
}
bool CmlPhgProc::mlCalcHazCamInWorld( stuInsMat matLCamBase2Body, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                    ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    CmlPhgProc clsPhg;

    ExOriPara oriBase, oriLCamBase;

    //左相机基准镜相对本体矩阵
    calcOriByMat( oriBase, matLCamBase2Body, oriLCamBase );

    //右相机基准镜到左相机基准镜
    ExOriPara oriRCamBase;

    calcOriByMat( oriLCamBase, matRCamBase2LCamBase, oriRCamBase );

     //相机相对基准镜
    ExOriPara oriLCamCap, oriRCamCap;
    calcOriByMat( oriLCamBase, matLCamCap2Base, oriLCamCap );
    calcOriByMat( oriRCamBase, matRCamCap2Base, oriRCamCap );

    ExOriPara oriCamBody2CamCap;
    oriCamBody2CamCap.ori.omg = Deg2Rad(180);

    clsPhg.ExOriTrans( &oriLCamCap, &oriCamBody2CamCap, &exOriCamL );
    clsPhg.ExOriTrans( &oriRCamCap, &oriCamBody2CamCap, &exOriCamR );

    return true;
}
bool CmlPhgProc::mlCalcHazCamInWorldVT( stuInsMat matBase, stuInsMat matLCamCap2Body, stuInsMat matLCamCap2RCamCap, ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    CmlPhgProc clsPhg;
    ExOriPara oriBase, oriLCamCap, oriRCamCap;

    CmlMat matR;
    if( false == matR.Initial( 3, 3 ) )
    {
        return false;
    }
    memcpy( matR.GetData(), matBase.dOriMatrix, sizeof(DOUBLE)*9 );
    if( false == RMat2OPK( &matR, &oriBase.ori ) )
    {
        return false;
    }
    oriBase.pos.X = matBase.dPosMatrix[0];
    oriBase.pos.Y = matBase.dPosMatrix[1];
    oriBase.pos.Z = matBase.dPosMatrix[2];

    //----------------------------------------
    DOUBLE dRMatLCamCap2Body[9];

    dRMatLCamCap2Body[0] = matLCamCap2Body.dOriMatrix[0];
    dRMatLCamCap2Body[1] = matLCamCap2Body.dOriMatrix[1];
    dRMatLCamCap2Body[2] = matLCamCap2Body.dOriMatrix[2];

    dRMatLCamCap2Body[3] = matLCamCap2Body.dOriMatrix[3];
    dRMatLCamCap2Body[4] = matLCamCap2Body.dOriMatrix[4];
    dRMatLCamCap2Body[5] = matLCamCap2Body.dOriMatrix[5];

    dRMatLCamCap2Body[6] = matLCamCap2Body.dOriMatrix[6];
    dRMatLCamCap2Body[7] = matLCamCap2Body.dOriMatrix[7];
    dRMatLCamCap2Body[8] = matLCamCap2Body.dOriMatrix[8];

    DOUBLE dOPKLCamCapInBody[3];
    clsPhg.GetOPKAngle( dRMatLCamCap2Body, dOPKLCamCapInBody );
    ExOriPara exOriLCamCap2Body;
    exOriLCamCap2Body.ori.omg = dOPKLCamCapInBody[0];
    exOriLCamCap2Body.ori.phi = dOPKLCamCapInBody[1];
    exOriLCamCap2Body.ori.kap = dOPKLCamCapInBody[2];

    exOriLCamCap2Body.pos.X = matLCamCap2Body.dPosMatrix[0] / 1000;
    exOriLCamCap2Body.pos.Y = matLCamCap2Body.dPosMatrix[1] / 1000;
    exOriLCamCap2Body.pos.Z = matLCamCap2Body.dPosMatrix[2] / 1000;

    clsPhg.ExOriTrans( &oriBase, &exOriLCamCap2Body, &oriLCamCap );

    //-------------------------------------
    DOUBLE dRMatRCamCap2LCamCap[9];
    dRMatRCamCap2LCamCap[0] = matLCamCap2RCamCap.dOriMatrix[0];
    dRMatRCamCap2LCamCap[1] = matLCamCap2RCamCap.dOriMatrix[1];
    dRMatRCamCap2LCamCap[2] = matLCamCap2RCamCap.dOriMatrix[2];

    dRMatRCamCap2LCamCap[3] = matLCamCap2RCamCap.dOriMatrix[3];
    dRMatRCamCap2LCamCap[4] = matLCamCap2RCamCap.dOriMatrix[4];
    dRMatRCamCap2LCamCap[5] = matLCamCap2RCamCap.dOriMatrix[5];

    dRMatRCamCap2LCamCap[6] = matLCamCap2RCamCap.dOriMatrix[6];
    dRMatRCamCap2LCamCap[7] = matLCamCap2RCamCap.dOriMatrix[7];
    dRMatRCamCap2LCamCap[8] = matLCamCap2RCamCap.dOriMatrix[8];

    DOUBLE dOPKRCam2LCamCap[3];
    clsPhg.GetOPKAngle( dRMatRCamCap2LCamCap, dOPKRCam2LCamCap );
    ExOriPara exOriRCamCap2LCamCap;
    exOriRCamCap2LCamCap.ori.omg = dOPKRCam2LCamCap[0];
    exOriRCamCap2LCamCap.ori.phi = dOPKRCam2LCamCap[1];
    exOriRCamCap2LCamCap.ori.kap = dOPKRCam2LCamCap[2];

    clsPhg.ExOriTrans( &oriLCamCap, &exOriRCamCap2LCamCap, &oriRCamCap );
    //--------------------
    ExOriPara oriCamBody2CamCap;
    oriCamBody2CamCap.ori.omg = Deg2Rad(180.0);
    clsPhg.ExOriTrans( &oriLCamCap, &oriCamBody2CamCap, &exOriCamL );
    clsPhg.ExOriTrans( &oriRCamCap, &oriCamBody2CamCap, &exOriCamR );
    return true;
}
bool CmlPhgProc::mlCalcHazCamInBodyVT( stuInsMat matLCamCap2Body, stuInsMat matLCamCap2RCamCap, ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    stuInsMat matBase;
    matBase.dOriMatrix[0] = 1;
    matBase.dOriMatrix[1] = 0;
    matBase.dOriMatrix[2] = 0;
    matBase.dOriMatrix[3] = 0;
    matBase.dOriMatrix[4] = 1;
    matBase.dOriMatrix[5] = 0;
    matBase.dOriMatrix[6] = 0;
    matBase.dOriMatrix[7] = 0;
    matBase.dOriMatrix[8] = 1;

    matBase.dPosMatrix[0] = 0;
    matBase.dPosMatrix[1] = 0;
    matBase.dPosMatrix[2] = 0;
    return this->mlCalcHazCamInWorldVT( matBase, matLCamCap2Body, matLCamCap2RCamCap, exOriCamL, exOriCamR );
}
bool CmlPhgProc::GetOPKAngle( DOUBLE *pRMat, DOUBLE *pOPK )
{
    if( ( pRMat == NULL )||( pOPK == NULL ) )
    {
        return false;
    }
    CmlMat matR;
    if( true == matR.Initial( 3,3 ) )
    {
        memcpy( matR.GetData(), pRMat, sizeof(DOUBLE)*9 );
        OriAngle oriA;
        if( true == RMat2OPK( &matR, &oriA ) )
        {
            pOPK[0] = oriA.omg;
            pOPK[1] = oriA.phi;
            pOPK[2] = oriA.kap;
            return true;
        }
    }
    return false;
}
bool CmlPhgProc::GetRMatByOPK( DOUBLE *pOPK, DOUBLE *pRMat )
{
    if( ( pRMat == NULL )||( pOPK == NULL ) )
    {
        return false;
    }
    CmlMat matR;
    OriAngle oriA;
    if( true == matR.Initial( 3,3 ) )
    {
        oriA.omg = pOPK[0];
        oriA.phi = pOPK[1];
        oriA.kap = pOPK[2];
        OPK2RMat( &oriA, &matR );
        memcpy( pRMat, matR.GetData(), sizeof(DOUBLE)*9 );
        return true;
    }
    return false;
}
bool Polygon2d2GPC_Poly( Polygon2d poly, gpc_polygon )
{
    return false;
}
bool CmlPhgProc::GetPolygonUnion2D( Polygon2d poly1, Polygon2d poly2, Polygon2d &polyNew )
{
	return false;
}

bool CmlPhgProc::GetPolygonUnion3D( Polygon3d poly1, Polygon3d poly2, Polygon3d &polyNew )
{
	return false;
}
bool CmlPhgProc::PersProjInFlat( const SCHAR* strImg, InOriPara inOri, ExOriPara exOri, DOUBLE dRes, DOUBLE dRange, DOUBLE dCamH, const SCHAR* strPers )
{
    Pt3d ptCent = exOri.pos;
    Pt3d ptOrig;
    ptOrig.X = ptCent.X - dRange;
    ptOrig.Y = ptCent.Y - dRange;
    UINT nW = 2*dRange / dRes;
    UINT nH = 2*dRange / dRes;
    CmlGeoRaster clsG;
    Pt2d ptLL;
    ptLL.X = ptOrig.X;
    ptLL.Y = ptOrig.Y + (nH - 1)*dRes;

    if( false == clsG.CreateGeoFile( strPers, ptLL, dRes, -dRes, nH, nW, 1, GDT_Byte, 255 ) )
    {
        return false;
    }
    CmlRasterBlock clsRB;
    if( false == this->PersProjInFlat( strImg, inOri, exOri, dRes, dRange, dCamH, ptLL, clsRB ) )
    {
        return false;
    }
    if( false == clsG.SaveToGeoFile( 1, 0, 0, &clsRB) )
    {
        return false;
    }
    return true;
}
bool CmlPhgProc::PersProjInFlat( const SCHAR* strImg, InOriPara inOri, ExOriPara exOri, DOUBLE dRes, DOUBLE dRange, DOUBLE dCamH, Pt2d &ptOrig, CmlRasterBlock &clsRB )
{
    Pt3d ptCent = exOri.pos;

    ptOrig.X = ptCent.X - dRange;
    ptOrig.Y = ptCent.Y - dRange;
    UINT nW = 2*dRange / dRes;
    UINT nH = 2*dRange / dRes;
    CmlGeoRaster clsG;
    Pt2d ptLL;
    ptOrig.X = ptOrig.X;
    ptOrig.Y = ptOrig.Y + (nH-1)*dRes;

    CRasterPt2D clsMat2d;
    clsMat2d.Initial( nH, nW );
    for( UINT i = 0; i < nH; ++i )
    {
        for( UINT j = 0; j < nW; ++j )
        {
            Pt3d ptCurXYZ;
            ptCurXYZ.X = ptOrig.X + j *dRes;
            ptCurXYZ.Y = ptOrig.X - j *dRes;
            ptCurXYZ.Z = ptCent.Z + dCamH;

            Pt2d ptCurxy;
            CmlMat matOPK;
            OPK2RMat( &exOri.ori, &matOPK );
            getxyFromXYZ( ptCurxy, ptCurXYZ, ptCent, matOPK, inOri.f, inOri.f );
            ptCurxy.X += inOri.x;
            ptCurxy.Y = inOri.y - ptCurxy.Y;
            clsMat2d.SetAt( i, j, ptCurxy );
        }
    }
    CmlFrameImage clsSrcImg, clsDstImg;
    if( false == clsSrcImg.LoadFile( strImg ) )
    {
        return false;
    }
    clsRB.SetGDTType( GDT_Byte );
    return clsSrcImg.mlGrayInterpolation( &clsSrcImg.m_DataBlock, &clsMat2d, &clsRB, 0 );

}
//输入像平面坐标，输出五个系数，分别为左右角元素该正值
bool funcKernel( Pt2d ptL, Pt2d ptR, DOUBLE df1, DOUBLE df2, DOUBLE* pdCoef, DOUBLE* pdACoef, DOUBLE* pdLCoef )
{
	OriAngle oriTempL, oriTempR;
	oriTempL.omg = 0;
	oriTempL.phi = pdCoef[0];
	oriTempL.kap = pdCoef[1];
	oriTempR.omg = pdCoef[2];
	oriTempR.phi = pdCoef[3];
	oriTempR.kap = pdCoef[4];
	CmlMat matRL, matRR;
	OPK2RMat( &oriTempL, &matRL );
	OPK2RMat( &oriTempR, &matRR );
	
	Pt3d pt1, pt2;
	pt1.X = matRL.GetAt( 0, 0 )*ptL.X + matRL.GetAt( 0, 1 )*ptL.Y - matRL.GetAt( 0, 2 )*df1;
	pt1.Y = matRL.GetAt( 1, 0 )*ptL.X + matRL.GetAt( 1, 1 )*ptL.Y - matRL.GetAt( 1, 2 )*df1;
	pt1.Z = matRL.GetAt( 2, 0 )*ptL.X + matRL.GetAt( 2, 1 )*ptL.Y - matRL.GetAt( 2, 2 )*df1;

	pt2.X = matRR.GetAt( 0, 0 )*ptR.X + matRR.GetAt( 0, 1 )*ptR.Y - matRR.GetAt( 0, 2 )*df2;
	pt2.Y = matRR.GetAt( 1, 0 )*ptR.X + matRR.GetAt( 1, 1 )*ptR.Y - matRR.GetAt( 1, 2 )*df2;
	pt2.Z = matRR.GetAt( 2, 0 )*ptR.X + matRR.GetAt( 2, 1 )*ptR.Y - matRR.GetAt( 2, 2 )*df2;

	DOUBLE dY1ToPhi1 = ptL.X*sin(oriTempL.omg)*cos(oriTempL.phi)*cos(oriTempL.kap) - ptL.Y*sin(oriTempL.omg)*cos(oriTempL.phi)*sin(oriTempL.kap) - df1*sin(oriTempL.omg)*sin(oriTempL.phi);
	DOUBLE dZ1ToPhi1 = -ptL.X*cos(oriTempL.omg)*cos(oriTempL.phi)*cos(oriTempL.kap) + ptL.Y*cos(oriTempL.omg)*cos(oriTempL.phi)*sin(oriTempL.kap) + df1*cos(oriTempL.omg)*sin(oriTempL.phi);
	DOUBLE dY1ToKap1 = ptL.X*(cos(oriTempL.omg)*cos(oriTempL.kap)-sin(oriTempL.omg)*cos(oriTempL.phi)*sin(oriTempL.kap))+ptL.Y*(-cos(oriTempL.omg)*sin(oriTempL.kap)-sin(oriTempL.omg)*sin(oriTempL.phi)*cos(oriTempL.kap));
	DOUBLE dZ1ToKap1 = ptL.X*(sin(oriTempL.omg)*cos(oriTempL.kap)+cos(oriTempL.omg)*sin(oriTempL.phi)*sin(oriTempL.kap))+ptL.Y*(-sin(oriTempL.omg)*sin(oriTempL.kap)+cos(oriTempL.omg)*sin(oriTempL.phi)*cos(oriTempL.kap));
	
	DOUBLE dY2ToOmg2 = ptR.X*( -sin(oriTempR.omg)*sin(oriTempR.kap)+cos(oriTempR.omg)*sin(oriTempR.phi)*cos(oriTempR.kap))+ptR.Y*(-sin(oriTempR.omg)*cos(oriTempR.kap)-cos(oriTempR.omg)*sin(oriTempR.phi)*sin(oriTempR.kap))+df2*cos(oriTempR.omg)*cos(oriTempR.phi);
	DOUBLE dZ2ToOmg2 = ptR.X*(cos(oriTempR.omg)*sin(oriTempR.kap)+sin(oriTempR.omg)*sin(oriTempR.phi)*cos(oriTempR.kap))+ptR.Y*(cos(oriTempR.omg)*cos(oriTempR.kap)-sin(oriTempR.omg)*sin(oriTempR.phi)*sin(oriTempR.kap))+df2*sin(oriTempR.omg)*cos(oriTempR.phi);

	DOUBLE dY2ToPhi2 = ptR.X*sin(oriTempR.omg)*cos(oriTempR.phi)*cos(oriTempR.kap)-ptR.Y*sin(oriTempR.omg)*cos(oriTempR.phi)*sin(oriTempR.kap)-df2*sin(oriTempR.omg)*sin(oriTempR.phi);
	DOUBLE dZ2ToPhi2 = -ptR.X*cos(oriTempR.omg)*cos(oriTempR.phi)*cos(oriTempR.kap) + ptR.Y*cos(oriTempR.omg)*cos(oriTempR.phi)*sin(oriTempR.kap) + df2*cos(oriTempR.omg)*sin(oriTempR.phi);

	DOUBLE dY2ToKap2 = ptR.X*(cos(oriTempR.omg)*cos(oriTempR.kap)-sin(oriTempR.omg)*cos(oriTempR.phi)*sin(oriTempR.kap))+ptR.Y*(-cos(oriTempR.omg)*sin(oriTempR.kap)-sin(oriTempR.omg)*sin(oriTempR.phi)*cos(oriTempR.kap));
	DOUBLE dZ2ToKap2 = ptR.X*(sin(oriTempL.omg)*cos(oriTempR.kap)+cos(oriTempR.omg)*sin(oriTempR.phi)*sin(oriTempR.kap))+ptR.Y*(-sin(oriTempR.omg)*sin(oriTempR.kap)+cos(oriTempR.omg)*sin(oriTempR.phi)*cos(oriTempR.kap));

	pdACoef[0] = pt2.Z * dY1ToPhi1 - pt2.Y * dZ1ToPhi1;
	pdACoef[1] = pt2.Z * dY1ToKap1 - pt2.Y * dZ1ToKap1;

	pdACoef[2] = pt1.Y * dZ2ToOmg2 - pt1.Z * dY2ToOmg2;
	pdACoef[3] = pt1.Y * dZ2ToPhi2 - pt1.Z * dY2ToPhi2;
	pdACoef[4] = pt1.Y * dZ2ToKap2 - pt1.Z * dY2ToKap2;

	pdLCoef[0] = (df1*(pt1.Y*pt2.Z-pt2.Y*pt1.Z)) / (pt1.Z*pt2.Z);
	//---------------------------------------------
	pdACoef[0] *= df1/(pt1.Z*pt2.Z);
	pdACoef[1] *= df1/(pt1.Z*pt2.Z);
	pdACoef[2] *= df1/(pt1.Z*pt2.Z);
	pdACoef[3] *= df1/(pt1.Z*pt2.Z);
	pdACoef[4] *= df1/(pt1.Z*pt2.Z);

	pdLCoef[0] *= -1.0;

	return true;
}
bool CmlPhgProc::RelaOriCalcWithOrigPts( vector<StereoMatchPt> vecMatchPts, InOriPara inOriL, UINT nHL, InOriPara inOriR, UINT nHR, ExOriPara &exOriRela )
{
	CmlFrameImage clsFrmL, clsFrmR;
	
	vector<StereoMatchPt> vecUnDisPts;
	Pt2d ptResL, ptResR;
	for( UINT i = 0; i < vecMatchPts.size(); ++i )
	{
		StereoMatchPt ptCur = vecMatchPts[i];
		if( ( false == clsFrmL.UnDisCorToPlaneFrame( ptCur.ptLInImg, inOriL, nHL, ptResL ))||\
		    ( false == clsFrmR.UnDisCorToPlaneFrame( ptCur.ptRInImg, inOriR, nHR, ptResR ))) 
		{
			continue;
		}
		ptCur.ptLInImg = ptResL;
		ptCur.ptRInImg = ptResR;
		vecUnDisPts.push_back( ptCur );
	}
	//////////////////////////////////////////////////////////////////////////
	CmlMat matA, matL, matX;
	if( ( false == matA.Initial( vecUnDisPts.size(), 5 ) )||( false == matL.Initial( vecUnDisPts.size(), 1 ) )||( false == matX.Initial( 5, 1 ) ))
	{
		return false;
	}
	DOUBLE dParaCoef[5];
	dParaCoef[0] = dParaCoef[1] = dParaCoef[2] = dParaCoef[3] = dParaCoef[4] = dParaCoef[5] = 0;
	SINT nItera = 0;
	while ( nItera++ < 25 )
	{	
		for( UINT i = 0; i < matA.GetH(); ++i )
		{
			for( UINT j = 0; j < matA.GetW(); ++j )
			{
				matA.SetAt( i, j, 0 );
			}
			matL.SetAt( i, 0, 0 );
		}
		for ( UINT i = 0; i < matX.GetH(); ++i )
		{
			matX.SetAt( i, 0, 0 );
		}
		

		DOUBLE dACoef[5], dLCoef;
		for( UINT i = 0; i < vecUnDisPts.size(); ++i )
		{
			StereoMatchPt ptCur = vecUnDisPts[i];
			if ( false == funcKernel( ptCur.ptLInImg, ptCur.ptRInImg, inOriL.f, inOriR.f, dParaCoef, dACoef, &dLCoef ) )
			{
				continue;
			}
			for( UINT j = 0; j < 5; ++j )
			{
				matA.SetAt( i, j, dACoef[j] );
			}
			matL.SetAt( i, 0, dLCoef );			
		}
		if( false == mlMatSolveSVD( &matA, &matL, &matX ) )
		{
			return false;
		}
		dParaCoef[0] += matX.GetAt( 0, 0 );
		dParaCoef[1] += matX.GetAt( 1, 0 );
		dParaCoef[2] += matX.GetAt( 2, 0 );
		dParaCoef[3] += matX.GetAt( 3, 0 );
		dParaCoef[4] += matX.GetAt( 4, 0 );
		
		
	}
	ExOriPara exL, exR;
	exL.ori.omg = 0;
	exL.ori.phi = dParaCoef[0];
	exL.ori.kap = dParaCoef[1];
	
	exR.ori.omg = dParaCoef[2];
	exR.ori.phi = dParaCoef[3];
	exR.ori.kap = dParaCoef[4];
	exR.pos.X = 0.498041;

	this->GetRelaOri( &exL, &exR, &exOriRela );

	
	return true;
}

bool CmlPhgProc::FindMatchHoleInImg( vector<Pt2i> vecMatchedPts, UINT nW, UINT nH, UINT nHoleRange, vector<Polygon2d> &vecHolePolys )
{
	if( vecMatchedPts.size() == 0 )
	{
		return false;
	}
	vector<Pt2d> vecMatchedPtsTmp;
	for( UINT i = 0; i < vecMatchedPts.size(); ++i )
	{
		Pt2i ptCur = vecMatchedPts[i];
		Pt2d ptTmp;
		ptTmp.X = ptCur.X;
		ptTmp.Y = ptCur.Y;
		vecMatchedPtsTmp.push_back( ptTmp );
	}
	vector<Pt2d> vecConvex;
	//计算点凸包
	if( false == CalcConvexHull( vecMatchedPtsTmp, vecConvex ))
	{
		return false;
	}
	bool *pMarkImg = NULL;
	try
	{
		pMarkImg = new bool[nW*nH];
		if( pMarkImg == NULL )
		{
			return false;
		}
	}
	catch (bad_alloc &memExp)
	{
		return false;
	}
	//----------------------------
	//图像赋初值
	CmlTIN clsTin;
	for( UINT i = 0; i < (nW*nH); ++i )
	{
		if( 0 == clsTin.PointInPoly( (i%nW), (i/nW), &vecConvex ) )
		{
			pMarkImg[i] = true;
		}
		else
		{
			pMarkImg[i] = false;
		}		
	}
	//----------------------------
	
	for( UINT i = 0; i < vecMatchedPts.size(); ++i )
	{
		Pt2i ptCur = vecMatchedPts[i];
		pMarkImg[ptCur.Y*nH+ptCur.X] = true;
	}
	UINT nCurRange = nHoleRange;
	
	vector<Pt2i> vecValidPts;

	for( UINT i = 0; i < (nW*nH); ++i )
	{
		//如果中心点有效，则直接进入下一循环
		UINT nCurY = (i/nW);
		UINT nCurX = (i%nW);
		if ( pMarkImg[nCurY*nW+nCurX] == true )
		{
			continue;
		}
		//--------------------------
		//模板处理
		bool bFindIsOk = true;
		for( SINT nTH = nCurY-nCurRange; nTH < (nCurY+nCurRange); ++nTH )
		{
			if ( ( nTH < 0 )||( nTH >= nH ) )
			{
				continue;
			}			
			for( SINT nTW = nCurX-nCurRange; nTW < (nCurX+nCurRange); ++nTW )
			{
				if ( ( nTW < 0 )||( nTW >= nW ) )
				{
					continue;
				}
				if ( pMarkImg[nTH*nW+nTW] == true )
				{
					bFindIsOk = false;
				}
			}
			if ( bFindIsOk == false )
			{
				break;
			}
			
		}
		if ( bFindIsOk == true )
		{
			Pt2i ptCur;
			ptCur.X = nCurX;
			ptCur.Y = nCurY;
			vecValidPts.push_back( ptCur );
		}
	}
	return true;	
}
//设定DEM为北东地坐标系，
bool CmlPhgProc::Find3DCoordBy2DPos( InOriPara inOri, ExOriPara exOri, Pt2d ptPos, GeoRasBlock *pclsGeoRasterB, Pt3d &ptRes )
{
	
	return true;
}
bool CmlPhgProc::getCoef( Pt2d ptXY, Pt3d ptObjXYZ, DOUBLE df, ExOriPara exOri, ExOriPara exOriRela, DOUBLE *pCoef )
{
	ExOriPara exOriCur;
	ExOriTrans( &exOri, &exOriRela, &exOriCur );
	CmlMat matOrig, matCur, matRela;

	OPK2RMat( &exOri.ori, &matOrig );
	OPK2RMat( &exOriCur.ori, &matCur );
	OPK2RMat( &exOriRela.ori, &matRela );

	DOUBLE dOmgOrig = exOri.ori.omg;
	DOUBLE dPhiOrig = exOri.ori.phi;
	DOUBLE dKapOrig = exOri.ori.kap;

	DOUBLE dXt = matCur.GetAt( 0, 0 )*( ptObjXYZ.X - exOriCur.pos.X ) + matCur.GetAt( 1, 0 )*( ptObjXYZ.Y - exOriCur.pos.Y ) + matCur.GetAt( 2, 0 )*( ptObjXYZ.Z - exOriCur.pos.Z );
	DOUBLE dYt = matCur.GetAt( 0, 1 )*( ptObjXYZ.X - exOriCur.pos.X ) + matCur.GetAt( 1, 1 )*( ptObjXYZ.Y - exOriCur.pos.Y ) + matCur.GetAt( 2, 1 )*( ptObjXYZ.Z - exOriCur.pos.Z );
	DOUBLE dZt = matCur.GetAt( 0, 2 )*( ptObjXYZ.X - exOriCur.pos.X ) + matCur.GetAt( 1, 2 )*( ptObjXYZ.Y - exOriCur.pos.Y ) + matCur.GetAt( 2, 2 )*( ptObjXYZ.Z - exOriCur.pos.Z );

	//ToOmg
	DOUBLE da1OrigToOmg = 0;
	DOUBLE da2OrigToOmg = 0;
	DOUBLE da3OrigToOmg = 0;
	DOUBLE db1OrigToOmg = -sin(dOmgOrig)*sin(dKapOrig) + cos(dOmgOrig)*sin(dPhiOrig)*cos(dKapOrig);
	DOUBLE db2OrigToOmg = -sin(dOmgOrig)*cos(dKapOrig) - cos(dOmgOrig)*sin(dPhiOrig)*sin(dKapOrig);
	DOUBLE db3OrigToOmg = -cos(dOmgOrig)*cos(dPhiOrig);
	DOUBLE dc1OrigToOmg = cos(dOmgOrig)*sin(dKapOrig) + sin(dOmgOrig)*sin(dPhiOrig)*cos(dKapOrig);
	DOUBLE dc2OrigToOmg = cos(dOmgOrig)*cos(dKapOrig) - sin(dOmgOrig)*sin(dPhiOrig)*sin(dKapOrig);
	DOUBLE dc3OrigToOmg = -sin(dOmgOrig)*cos(dPhiOrig);

	DOUBLE da1CurToOmg = da1OrigToOmg*matRela.GetAt(0,0) + da2OrigToOmg*matRela.GetAt(1,0) + da3OrigToOmg*matRela.GetAt(2,0);
	DOUBLE da2CurToOmg = da1OrigToOmg*matRela.GetAt(0,1) + da2OrigToOmg*matRela.GetAt(1,1) + da3OrigToOmg*matRela.GetAt(2,1);
	DOUBLE da3CurToOmg = da1OrigToOmg*matRela.GetAt(0,2) + da2OrigToOmg*matRela.GetAt(1,2) + da3OrigToOmg*matRela.GetAt(2,2);
	DOUBLE db1CurToOmg = db1OrigToOmg*matRela.GetAt(0,0) + db2OrigToOmg*matRela.GetAt(1,0) + db3OrigToOmg*matRela.GetAt(2,0);
	DOUBLE db2CurToOmg = db1OrigToOmg*matRela.GetAt(0,1) + db2OrigToOmg*matRela.GetAt(1,1) + db3OrigToOmg*matRela.GetAt(2,1);
	DOUBLE db3CurToOmg = db1OrigToOmg*matRela.GetAt(0,2) + db2OrigToOmg*matRela.GetAt(1,2) + db3OrigToOmg*matRela.GetAt(2,2);
	DOUBLE dc1CurToOmg = dc1OrigToOmg*matRela.GetAt(0,0) + dc2OrigToOmg*matRela.GetAt(1,0) + dc3OrigToOmg*matRela.GetAt(2,0);
	DOUBLE dc2CurToOmg = dc1OrigToOmg*matRela.GetAt(0,1) + dc2OrigToOmg*matRela.GetAt(1,1) + dc3OrigToOmg*matRela.GetAt(2,1);
	DOUBLE dc3CurToOmg = dc1OrigToOmg*matRela.GetAt(0,2) + dc2OrigToOmg*matRela.GetAt(1,2) + dc3OrigToOmg*matRela.GetAt(2,2);

	DOUBLE dXsToOmg  = da1OrigToOmg*exOriRela.pos.X + da2OrigToOmg*exOriRela.pos.Y + da3OrigToOmg*exOriRela.pos.Z;
	DOUBLE dYsToOmg  = db1OrigToOmg*exOriRela.pos.X + db2OrigToOmg*exOriRela.pos.Y + db3OrigToOmg*exOriRela.pos.Z;
	DOUBLE dZsToOmg  = dc1OrigToOmg*exOriRela.pos.X + dc2OrigToOmg*exOriRela.pos.Y + dc3OrigToOmg*exOriRela.pos.Z;

	DOUBLE dXtToOmg = ptObjXYZ.X * da1CurToOmg - da1CurToOmg*exOriCur.pos.X - matCur.GetAt(0,0)*dXsToOmg + 
		ptObjXYZ.Y * db1CurToOmg - db1CurToOmg*exOriCur.pos.Y - matCur.GetAt(1,0)*dYsToOmg +
		ptObjXYZ.Z * dc1CurToOmg - dc1CurToOmg*exOriCur.pos.Z - matCur.GetAt(2,0)*dZsToOmg;

	DOUBLE dYtToOmg = ptObjXYZ.X * da2CurToOmg - da2CurToOmg*exOriCur.pos.X - matCur.GetAt(0,1)*dXsToOmg + 
		ptObjXYZ.Y * db2CurToOmg - db2CurToOmg*exOriCur.pos.Y - matCur.GetAt(1,1)*dYsToOmg +
		ptObjXYZ.Z * dc2CurToOmg - dc2CurToOmg*exOriCur.pos.Z - matCur.GetAt(2,1)*dZsToOmg;

	DOUBLE dZtToOmg = ptObjXYZ.X * da3CurToOmg - da3CurToOmg*exOriCur.pos.X - matCur.GetAt(0,2)*dXsToOmg + 
		ptObjXYZ.Y * db3CurToOmg - db3CurToOmg*exOriCur.pos.Y - matCur.GetAt(1,2)*dYsToOmg +
		ptObjXYZ.Z * dc3CurToOmg - dc3CurToOmg*exOriCur.pos.Z - matCur.GetAt(2,2)*dZsToOmg;

	DOUBLE dCoefZ = dZt*dZt;
	pCoef[0] = -df*(dXtToOmg*dZt - dXt*dZtToOmg) / dCoefZ;
	pCoef[6] = -df*(dYtToOmg*dZt - dYt*dZtToOmg) / dCoefZ;

	//ToPhi
	DOUBLE da1OrigToPhi = -sin(dPhiOrig)*cos(dKapOrig);
	DOUBLE da2OrigToPhi = sin(dPhiOrig)*sin(dKapOrig);
	DOUBLE da3OrigToPhi = cos(dPhiOrig);
	DOUBLE db1OrigToPhi = sin(dOmgOrig)*cos(dPhiOrig)*cos(dKapOrig);
	DOUBLE db2OrigToPhi = -sin(dOmgOrig)*cos(dPhiOrig)*sin(dKapOrig);
	DOUBLE db3OrigToPhi = sin(dOmgOrig)*sin(dPhiOrig);
	DOUBLE dc1OrigToPhi = -cos(dOmgOrig)*cos(dKapOrig)*cos(dKapOrig);
	DOUBLE dc2OrigToPhi = cos(dOmgOrig)*cos(dKapOrig)*sin(dKapOrig);
	DOUBLE dc3OrigToPhi = -cos(dOmgOrig)*sin(dPhiOrig);

	DOUBLE da1CurToPhi = da1OrigToPhi*matRela.GetAt(0,0) + da2OrigToPhi*matRela.GetAt(1,0) + da3OrigToPhi*matRela.GetAt(2,0);
	DOUBLE da2CurToPhi = da1OrigToPhi*matRela.GetAt(0,1) + da2OrigToPhi*matRela.GetAt(1,1) + da3OrigToPhi*matRela.GetAt(2,1);
	DOUBLE da3CurToPhi = da1OrigToPhi*matRela.GetAt(0,2) + da2OrigToPhi*matRela.GetAt(1,2) + da3OrigToPhi*matRela.GetAt(2,2);
	DOUBLE db1CurToPhi = db1OrigToPhi*matRela.GetAt(0,0) + db2OrigToPhi*matRela.GetAt(1,0) + db3OrigToPhi*matRela.GetAt(2,0);
	DOUBLE db2CurToPhi = db1OrigToPhi*matRela.GetAt(0,1) + db2OrigToPhi*matRela.GetAt(1,1) + db3OrigToPhi*matRela.GetAt(2,1);
	DOUBLE db3CurToPhi = db1OrigToPhi*matRela.GetAt(0,2) + db2OrigToPhi*matRela.GetAt(1,2) + db3OrigToPhi*matRela.GetAt(2,2);
	DOUBLE dc1CurToPhi = dc1OrigToPhi*matRela.GetAt(0,0) + dc2OrigToPhi*matRela.GetAt(1,0) + dc3OrigToPhi*matRela.GetAt(2,0);
	DOUBLE dc2CurToPhi = dc1OrigToPhi*matRela.GetAt(0,1) + dc2OrigToPhi*matRela.GetAt(1,1) + dc3OrigToPhi*matRela.GetAt(2,1);
	DOUBLE dc3CurToPhi = dc1OrigToPhi*matRela.GetAt(0,2) + dc2OrigToPhi*matRela.GetAt(1,2) + dc3OrigToPhi*matRela.GetAt(2,2);

	DOUBLE dXsToPhi  = da1OrigToPhi*exOriRela.pos.X + da2OrigToPhi*exOriRela.pos.Y + da3OrigToPhi*exOriRela.pos.Z;
	DOUBLE dYsToPhi  = db1OrigToPhi*exOriRela.pos.X + db2OrigToPhi*exOriRela.pos.Y + db3OrigToPhi*exOriRela.pos.Z;
	DOUBLE dZsToPhi  = dc1OrigToPhi*exOriRela.pos.X + dc2OrigToPhi*exOriRela.pos.Y + dc3OrigToPhi*exOriRela.pos.Z;

	DOUBLE dXtToPhi = ptObjXYZ.X * da1CurToPhi - da1CurToPhi*exOriCur.pos.X - matCur.GetAt(0,0)*dXsToPhi + 
		ptObjXYZ.Y * db1CurToPhi - db1CurToPhi*exOriCur.pos.Y - matCur.GetAt(1,0)*dYsToPhi +
		ptObjXYZ.Z * dc1CurToPhi - dc1CurToPhi*exOriCur.pos.Z - matCur.GetAt(2,0)*dZsToPhi;

	DOUBLE dYtToPhi = ptObjXYZ.X * da2CurToPhi - da2CurToPhi*exOriCur.pos.X - matCur.GetAt(0,1)*dXsToPhi + 
		ptObjXYZ.Y * db2CurToPhi - db2CurToPhi*exOriCur.pos.Y - matCur.GetAt(1,1)*dYsToPhi +
		ptObjXYZ.Z * dc2CurToPhi - dc2CurToPhi*exOriCur.pos.Z - matCur.GetAt(2,1)*dZsToPhi;

	DOUBLE dZtToPhi = ptObjXYZ.X * da3CurToPhi - da3CurToPhi*exOriCur.pos.X - matCur.GetAt(0,2)*dXsToPhi + 
		ptObjXYZ.Y * db3CurToPhi - db3CurToPhi*exOriCur.pos.Y - matCur.GetAt(1,2)*dYsToPhi +
		ptObjXYZ.Z * dc3CurToPhi - dc3CurToPhi*exOriCur.pos.Z - matCur.GetAt(2,2)*dZsToPhi;

	pCoef[1] = -df*(dXtToPhi*dZt - dXt*dZtToPhi) / dCoefZ;
	pCoef[7] = -df*(dYtToPhi*dZt - dYt*dZtToPhi) / dCoefZ;

	//ToKap
	DOUBLE da1OrigToKap = -cos(dPhiOrig)*sin(dKapOrig);
	DOUBLE da2OrigToKap = -cos(dPhiOrig)*cos(dKapOrig);
	DOUBLE da3OrigToKap = 0;
	DOUBLE db1OrigToKap = cos(dOmgOrig)*cos(dKapOrig) - sin(dOmgOrig)*sin(dPhiOrig)*sin(dKapOrig);
	DOUBLE db2OrigToKap = -cos(dOmgOrig)*sin(dKapOrig) - sin(dOmgOrig)*sin(dPhiOrig)*cos(dKapOrig);
	DOUBLE db3OrigToKap = 0;
	DOUBLE dc1OrigToKap = sin(dOmgOrig)*cos(dKapOrig) + cos(dOmgOrig)*sin(dKapOrig)*sin(dKapOrig);
	DOUBLE dc2OrigToKap = -sin(dOmgOrig)*sin(dKapOrig) + cos(dOmgOrig)*sin(dKapOrig)*cos(dKapOrig);
	DOUBLE dc3OrigToKap = 0;

	DOUBLE da1CurToKap = da1OrigToKap*matRela.GetAt(0,0) + da2OrigToKap*matRela.GetAt(1,0) + da3OrigToKap*matRela.GetAt(2,0);
	DOUBLE da2CurToKap = da1OrigToKap*matRela.GetAt(0,1) + da2OrigToKap*matRela.GetAt(1,1) + da3OrigToKap*matRela.GetAt(2,1);
	DOUBLE da3CurToKap = da1OrigToKap*matRela.GetAt(0,2) + da2OrigToKap*matRela.GetAt(1,2) + da3OrigToKap*matRela.GetAt(2,2);
	DOUBLE db1CurToKap = db1OrigToKap*matRela.GetAt(0,0) + db2OrigToKap*matRela.GetAt(1,0) + db3OrigToKap*matRela.GetAt(2,0);
	DOUBLE db2CurToKap = db1OrigToKap*matRela.GetAt(0,1) + db2OrigToKap*matRela.GetAt(1,1) + db3OrigToKap*matRela.GetAt(2,1);
	DOUBLE db3CurToKap = db1OrigToKap*matRela.GetAt(0,2) + db2OrigToKap*matRela.GetAt(1,2) + db3OrigToKap*matRela.GetAt(2,2);
	DOUBLE dc1CurToKap = dc1OrigToKap*matRela.GetAt(0,0) + dc2OrigToKap*matRela.GetAt(1,0) + dc3OrigToKap*matRela.GetAt(2,0);
	DOUBLE dc2CurToKap = dc1OrigToKap*matRela.GetAt(0,1) + dc2OrigToKap*matRela.GetAt(1,1) + dc3OrigToKap*matRela.GetAt(2,1);
	DOUBLE dc3CurToKap = dc1OrigToKap*matRela.GetAt(0,2) + dc2OrigToKap*matRela.GetAt(1,2) + dc3OrigToKap*matRela.GetAt(2,2);

	DOUBLE dXsToKap  = da1OrigToKap*exOriRela.pos.X + da2OrigToKap*exOriRela.pos.Y + da3OrigToKap*exOriRela.pos.Z;
	DOUBLE dYsToKap  = db1OrigToKap*exOriRela.pos.X + db2OrigToKap*exOriRela.pos.Y + db3OrigToKap*exOriRela.pos.Z;
	DOUBLE dZsToKap  = dc1OrigToKap*exOriRela.pos.X + dc2OrigToKap*exOriRela.pos.Y + dc3OrigToKap*exOriRela.pos.Z;

	DOUBLE dXtToKap = ptObjXYZ.X * da1CurToKap - da1CurToKap*exOriCur.pos.X - matCur.GetAt(0,0)*dXsToKap + 
		ptObjXYZ.Y * db1CurToKap - db1CurToKap*exOriCur.pos.Y - matCur.GetAt(1,0)*dYsToKap +
		ptObjXYZ.Z * dc1CurToKap - dc1CurToKap*exOriCur.pos.Z - matCur.GetAt(2,0)*dZsToKap;

	DOUBLE dYtToKap = ptObjXYZ.X * da2CurToKap - da2CurToKap*exOriCur.pos.X - matCur.GetAt(0,1)*dXsToKap + 
		ptObjXYZ.Y * db2CurToKap - db2CurToKap*exOriCur.pos.Y - matCur.GetAt(1,1)*dYsToKap +
		ptObjXYZ.Z * dc2CurToKap - dc2CurToKap*exOriCur.pos.Z - matCur.GetAt(2,1)*dZsToKap;

	DOUBLE dZtToKap = ptObjXYZ.X * da3CurToKap - da3CurToKap*exOriCur.pos.X - matCur.GetAt(0,2)*dXsToKap + 
		ptObjXYZ.Y * db3CurToKap - db3CurToKap*exOriCur.pos.Y - matCur.GetAt(1,2)*dYsToKap +
		ptObjXYZ.Z * dc3CurToKap - dc3CurToKap*exOriCur.pos.Z - matCur.GetAt(2,2)*dZsToKap;

	pCoef[2] = -df*(dXtToKap*dZt - dXt*dZtToKap) / dCoefZ;
	pCoef[8] = -df*(dYtToKap*dZt - dYt*dZtToKap) / dCoefZ;

	//ToXs
	DOUBLE dXtToXs = -matCur.GetAt(0,0);
	DOUBLE dYtToXs = -matCur.GetAt(0,1);
	DOUBLE dZtToXs = -matCur.GetAt(0,2);

	pCoef[3] = -df*(dXtToXs*dZt - dXt*dZtToXs) / dCoefZ;
	pCoef[9] = -df*(dYtToXs*dZt - dYt*dZtToXs) / dCoefZ;

	//ToYs
	DOUBLE dXtToYs = -matCur.GetAt(1,0);
	DOUBLE dYtToYs = -matCur.GetAt(1,1);
	DOUBLE dZtToYs = -matCur.GetAt(1,2);

	pCoef[4] = -df*(dXtToYs*dZt - dXt*dZtToYs) / dCoefZ;
	pCoef[10] = -df*(dYtToYs*dZt - dYt*dZtToYs) / dCoefZ;

	//ToZs
	DOUBLE dXtToZs = -matCur.GetAt(2,0);
	DOUBLE dYtToZs = -matCur.GetAt(2,1);
	DOUBLE dZtToZs = -matCur.GetAt(2,2);

	pCoef[5] = -df*(dXtToZs*dZt - dXt*dZtToZs) / dCoefZ;
	pCoef[11] = -df*(dYtToZs*dZt - dYt*dZtToZs) / dCoefZ;

	return true;
}
bool CmlPhgProc::ResectionInMultiImg( vector<Pt3d> vecGCPs, vector<vector<Pt2d>> vecMultiImgPts, vector<DOUBLE> vecFocals, vector<ExOriPara> vecExInOri, vector<ExOriPara> &vecImgOutExOris )
{	
	UINT nImgNo = vecMultiImgPts.size();

	if( ( nImgNo == 0 )||( nImgNo != vecFocals.size() )||( nImgNo != vecExInOri.size()) )
	{
		return false;
	}
	UINT nPtNum = vecGCPs.size();

	UINT nCount = 0;

	for ( UINT i = 0; i < vecMultiImgPts.size(); ++i )
	{
		vector<Pt2d> &vecPts = vecMultiImgPts[i];
		for ( UINT j = 0; j < vecPts.size(); ++j )
		{
			nCount++;
		}
	}
	if ( (nPtNum == 0)||(nCount!= nPtNum) )
	{
		return false;
	}
	//-------------------------------
	vector<ExOriPara> vecRelaOri;
	for ( UINT i = 0; i < nImgNo; ++i )
	{
		ExOriPara exOri;
		GetRelaOri( &vecExInOri[0], &vecExInOri[i], &exOri );
		vecRelaOri.push_back( exOri );
	}
	//////////////////////////////////////////////////////////////////////////
	CmlMat matA, matX, matL;

	if ( (false == matA.Initial(2*nPtNum, 6))||(false == matX.Initial( 6, 1 ))||(false== matL.Initial(2*nPtNum, 1 ) ) )
	{
		return false;
	}
	ExOriPara exOriFirst = vecExInOri[0];

	UINT nItera = 0;

	DOUBLE dOmgThres, dPhiThres, dKapThres, dXRes, dYRes, dZRes;
	dOmgThres = dPhiThres = dKapThres = dXRes = dYRes = dZRes = 0;

	DOUBLE dAngleThres = Deg2Rad(0.1);
	DOUBLE dPosThres = 0.05;

	do 
	{
		for ( UINT i = 0; i < 2*nPtNum; ++i )
		{
			for ( UINT j = 0; j < 6; ++j )
			{
				matA.SetAt( i, j, 0 );
			}
		}
		for ( UINT i = 0; i < 6; ++i )
		{
			matX.SetAt( i, 0, 0 );
		}
		for ( UINT i = 0; i < 2*nPtNum; ++i )
		{
			matL.SetAt( i, 0, 0 );
		}
		//////////////////////////////////////////////////////////////////////////

		UINT nCnt = 0;
		for ( UINT i = 0; i < nImgNo; ++i )
		{
			CmlMat matCurR;
			ExOriPara exOriCur;
			ExOriTrans( &exOriFirst, &vecRelaOri[i], &exOriCur );
			OPK2RMat( &exOriCur.ori, &matCurR );

			vector<Pt2d> &vecPts = vecMultiImgPts[i];

			for ( UINT j = 0; j < vecPts.size(); ++j )
			{
				DOUBLE dCoef[12];

				if ( false == this->getCoef( vecPts[j], vecGCPs[nCnt], vecFocals[i], exOriFirst, vecRelaOri[i], dCoef ) )
				{
					return false;
				}
				for ( UINT k = 0; k < 6; ++k )
				{
					matA.SetAt( nCnt*2, k, dCoef[k] );
					matA.SetAt( nCnt*2+1, k, dCoef[6+k] );
				}
				Pt2d ptEstimate;
				getxyFromXYZ( ptEstimate, vecGCPs[nCnt], exOriCur.pos, matCurR, vecFocals[i], vecFocals[i] );

				Pt2d ptTmp = vecPts[j];
				DOUBLE dXResid = ptTmp.X - ptEstimate.X;
				DOUBLE dYResid = ptTmp.Y - ptEstimate.Y;

				matL.SetAt( nCnt*2, 0, dXResid );
				matL.SetAt( nCnt*2+1, 0, dYResid );
				nCnt++;
			}
		}
		if ( false == mlMatSolveSVD( &matA, &matL, &matX ) )
		{
			return false;
		}

		dOmgThres = matX.GetAt( 0, 0 );
		dPhiThres = matX.GetAt( 1, 0 );
		dKapThres = matX.GetAt( 2, 0 );

		dXRes = matX.GetAt( 3, 0 );
		dYRes = matX.GetAt( 4, 0 );
		dZRes = matX.GetAt( 5, 0 );

		exOriFirst.ori.omg += dOmgThres;
		exOriFirst.ori.phi += dPhiThres;
		exOriFirst.ori.kap += dKapThres;

		if ( nItera > 15 )
		{
			return false;
		}
		if ( (fabs(dOmgThres) < dAngleThres )||(fabs(dPhiThres) < dAngleThres )||(fabs(dKapThres) < dAngleThres )|| \
			(fabs(dXRes) < dPosThres )||(fabs(dYRes) < dPosThres )||(fabs(dZRes) < dPosThres ) )
		{
			break;
		}

	} while ( true );

	for ( UINT i = 0; i < nImgNo; ++i )
	{
		ExOriPara exOriRes;
		ExOriTrans( &exOriFirst, &vecRelaOri[i], &exOriRes );
		vecImgOutExOris.push_back( exOriRes );
	}
	return true;
	
}