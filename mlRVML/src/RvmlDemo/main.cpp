/****************************************************************************
**
** Copyright (C) 2011 Nokia Corporation and/or its subsidiary(-ies).
** All rights reserved.
** Contact: Nokia Corporation (qt-info@nokia.com)
**
** This file is part of the examples of the Qt Toolkit.
**
** $QT_BEGIN_LICENSE:BSD$
** You may use this file under the terms of the BSD license as follows:
**
** "Redistribution and use in source and binary forms, with or without
** modification, are permitted provided that the following conditions are
** met:
**   * Redistributions of source code must retain the above copyright
**     notice, this list of conditions and the following disclaimer.
**   * Redistributions in binary form must reproduce the above copyright
**     notice, this list of conditions and the following disclaimer in
**     the documentation and/or other materials provided with the
**     distribution.
**   * Neither the name of Nokia Corporation and its Subsidiary(-ies) nor
**     the names of its contributors may be used to endorse or promote
**     products derived from this software without specific prior written
**     permission.
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
** "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
** LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
** A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
** OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
** SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
** LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
** DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
** THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
** (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
** OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE."
** $QT_END_LICENSE$
**
****************************************************************************/

#include <QApplication>

#include "imageviewer.h"
//////////////////////////
#include "Sitemapproj.h"
#include "../../include/mlRVML.h"
#include "camCalibIO.h"
/////////////////////////
#include "../../src/mlRVML/mlFrameImage.h"
#include "../../src/mlRVML/mlSparseMat.h"
/////////////////////////
bool CalcLOPKs( DOUBLE dAngleIn1, DOUBLE dAngleIn2, ExOriPara *oriFinalRes )
{
    ExOriPara oriBase;
//    oriBase.ori.omg = Deg2Rad(180);
//    oriBase.ori.kap = Deg2Rad(-90);

    //桅杆相对本体矩阵
    DOUBLE dR1[9], dT1[3];
    dR1[0] = cos(Deg2Rad(0.1704));
    dR1[3] = cos(Deg2Rad(89.8822));
    dR1[6] = cos(Deg2Rad(89.8769));
    dR1[1] = cos(Deg2Rad(90.1175));
    dR1[4] = cos(Deg2Rad(0.1934));
    dR1[7] = cos(Deg2Rad(90.1535));
    dR1[2] = cos(Deg2Rad(90.1234));
    dR1[5] = cos(Deg2Rad(89.8467));
    dR1[8] = cos(Deg2Rad(0.1968));
    dT1[0] = 0.5332004;
    dT1[1] = -0.0015296;
    dT1[2] = -0.5707493;
    ExOriPara ori1;
    DOUBLE dAngle1[3];
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes1;
    mlExOriTrans( &oriBase, &ori1, &oriRes1 );

    //桅杆展开角度
    ori1.ori.omg = 0;
    ori1.ori.phi = Deg2Rad(0);
    ori1.ori.kap = 0;
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ExOriPara oriResV1;
    mlExOriTrans( &oriRes1, &ori1, &oriResV1 );

    //偏航相对桅杆
    dR1[0] = cos(Deg2Rad(0.0));
    dR1[3] = cos(Deg2Rad(90.0));
    dR1[6] = cos(Deg2Rad(90.0));
    dR1[1] = cos(Deg2Rad(90.0));
    dR1[4] = cos(Deg2Rad(0.051));
    dR1[7] = cos(Deg2Rad(89.9490));
    dR1[2] = cos(Deg2Rad(90.0));
    dR1[5] = cos(Deg2Rad(90.0510));
    dR1[8] = cos(Deg2Rad(0.0510));
    dT1[0] = -0.0850554;
    dT1[1] = -0.0479819;
    dT1[2] = 0;
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes2;
    mlExOriTrans( &oriResV1, &ori1, &oriRes2 );

    //偏航角旋转角度
    ori1.ori.omg = 0;
    ori1.ori.phi = 0;
    ori1.ori.kap = Deg2Rad(dAngleIn1);//-59.9670
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ExOriPara oriResV2;
    mlExOriTrans( &oriRes2, &ori1, &oriResV2 );

    //俯仰相对偏航
    dR1[0] = cos(Deg2Rad(0.093));
    dR1[3] = cos(Deg2Rad(90.0930));
    dR1[6] = cos(Deg2Rad(90.0001));
    dR1[1] = cos(Deg2Rad(89.9070));
    dR1[4] = cos(Deg2Rad(0.1554));
    dR1[7] = cos(Deg2Rad(89.8755));
    dR1[2] = cos(Deg2Rad(90.0002));
    dR1[5] = cos(Deg2Rad(90.1245));
    dR1[8] = cos(Deg2Rad(0.1245));
    dT1[0] = 0.0003225;
    dT1[1] = -0.0000005;
    dT1[2] = -0.6493588;
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes3;
    mlExOriTrans( &oriResV2, &ori1, &oriRes3 );

    //俯仰角旋转角度
    ori1.ori.omg = 0;
    ori1.ori.phi = Deg2Rad(dAngleIn2);//
    ori1.ori.kap = 0;
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ExOriPara oriResV3;
    mlExOriTrans( &oriRes3, &ori1, &oriResV3 );

    //基准镜相对俯仰
    dR1[0] = cos(Deg2Rad(90.1032));
    dR1[3] = cos(Deg2Rad(0.2726));
    dR1[6] = cos(Deg2Rad(90.2523));
    dR1[1] = cos(Deg2Rad(90.3158));
    dR1[4] = cos(Deg2Rad(89.7483));
    dR1[7] = cos(Deg2Rad(0.4038));
    dR1[2] = cos(Deg2Rad(0.3357));
    dR1[5] = cos(Deg2Rad(89.8850));
    dR1[8] = cos(Deg2Rad(89.6846));
    dT1[0] = 0.0698878;
    dT1[1] = -0.1090252;
    dT1[2] = -0.1029593;
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes4;
    mlExOriTrans( &oriResV3, &ori1, &oriRes4 );

    //右相机相对左相机基准镜的姿态
    //相对姿态
    //--------------------------------
//    dR1[0] = cos(Deg2Rad(0.2988));
//    dR1[3] = cos(Deg2Rad(90.0116));
//    dR1[6] = cos(Deg2Rad(89.7014));
//    dR1[1] = cos(Deg2Rad(89.9886));
//    dR1[4] = cos(Deg2Rad(0.0312));
//    dR1[7] = cos(Deg2Rad(89.9710));
//    dR1[2] = cos(Deg2Rad(90.2944));
//    dR1[5] = cos(Deg2Rad(90.0289));
//    dR1[8] = cos(Deg2Rad(0.2959));
//    dT1[0] = 0.26991;
//    dT1[1] = -0.00017;
//    dT1[2] = -0.00045;
//    mlGetOPKAngle( dR1, dAngle1 );
//    ori1.ori.omg = dAngle1[0];
//    ori1.ori.phi = dAngle1[1];
//    ori1.ori.kap = dAngle1[2];
//    ori1.pos.X = dT1[0];
//    ori1.pos.Y = dT1[1];
//    ori1.pos.Z = dT1[2];
    //------------------------
    ori1.ori.omg = 0;
    ori1.ori.phi = 0;
    ori1.ori.kap = 0;
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ///////////////////////////

    ExOriPara oriResV4;
    mlExOriTrans( &oriRes4, &ori1, &oriResV4 );

    //相机相对基准镜
    dT1[0] = 0.022863;
    dT1[1] = 0.030159;
    dT1[2] = -0.006573;
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = 0;
    ori1.ori.phi = 0;
    ori1.ori.kap = 0;
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes5;
    mlExOriTrans( &oriResV4, &ori1, &oriRes5 );

    //相机相对像空间坐标系
    ori1.ori.omg = Deg2Rad(180);
    ori1.ori.phi = 0;
    ori1.ori.kap = 0;
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ExOriPara oriRes6;
    mlExOriTrans( &oriRes5, &ori1, &oriRes6 );

    *oriFinalRes = oriRes6;

    return true;
}
bool CalcROPKs( DOUBLE dAngleIn1, DOUBLE dAngleIn2, ExOriPara *oriFinalRes )
{
    ExOriPara oriBase;
//    oriBase.ori.omg = Deg2Rad(180);
//    oriBase.ori.kap = Deg2Rad(-90);

    //桅杆相对本体矩阵


    DOUBLE dR1[9], dT1[3];
    dR1[0] = cos(Deg2Rad(0.1704));
    dR1[3] = cos(Deg2Rad(89.8822));
    dR1[6] = cos(Deg2Rad(89.8769));
    dR1[1] = cos(Deg2Rad(90.1175));
    dR1[4] = cos(Deg2Rad(0.1934));
    dR1[7] = cos(Deg2Rad(90.1535));
    dR1[2] = cos(Deg2Rad(90.1234));
    dR1[5] = cos(Deg2Rad(89.8467));
    dR1[8] = cos(Deg2Rad(0.1968));
    dT1[0] = 0.5332004;
    dT1[1] = -0.0015296;
    dT1[2] = -0.5707493;
    ExOriPara ori1;
    DOUBLE dAngle1[3];
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes1;
    mlExOriTrans( &oriBase, &ori1, &oriRes1 );

    //桅杆展开角度
    ori1.ori.omg = 0;
    ori1.ori.phi = Deg2Rad(-0.044);
    ori1.ori.kap = 0;
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ExOriPara oriResV1;
    mlExOriTrans( &oriRes1, &ori1, &oriResV1 );

    //偏航相对桅杆
    dR1[0] = cos(Deg2Rad(0.0));
    dR1[3] = cos(Deg2Rad(90.0));
    dR1[6] = cos(Deg2Rad(90.0));
    dR1[1] = cos(Deg2Rad(90.0));
    dR1[4] = cos(Deg2Rad(0.051));
    dR1[7] = cos(Deg2Rad(89.9490));
    dR1[2] = cos(Deg2Rad(90.0));
    dR1[5] = cos(Deg2Rad(90.0510));
    dR1[8] = cos(Deg2Rad(0.0510));
    dT1[0] = -0.0850554;
    dT1[1] = -0.0479819;
    dT1[2] = 0;
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes2;
    mlExOriTrans( &oriResV1, &ori1, &oriRes2 );

    //偏航角旋转角度
    ori1.ori.omg = 0;
    ori1.ori.phi = 0;
    ori1.ori.kap = Deg2Rad(dAngleIn1);//-59.9670
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ExOriPara oriResV2;
    mlExOriTrans( &oriRes2, &ori1, &oriResV2 );

    //俯仰相对偏航
    dR1[0] = cos(Deg2Rad(0.093));
    dR1[3] = cos(Deg2Rad(90.0930));
    dR1[6] = cos(Deg2Rad(90.0001));
    dR1[1] = cos(Deg2Rad(89.9070));
    dR1[4] = cos(Deg2Rad(0.1554));
    dR1[7] = cos(Deg2Rad(89.8755));
    dR1[2] = cos(Deg2Rad(90.0002));
    dR1[5] = cos(Deg2Rad(90.1245));
    dR1[8] = cos(Deg2Rad(0.1245));
    dT1[0] = 0.0003225;
    dT1[1] = -0.0000005;
    dT1[2] = -0.6493588;
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes3;
    mlExOriTrans( &oriResV2, &ori1, &oriRes3 );

    //俯仰角旋转角度
    ori1.ori.omg = 0;
    ori1.ori.phi = Deg2Rad(-29.973);//
    ori1.ori.kap = 0;
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ExOriPara oriResV3;
    mlExOriTrans( &oriRes3, &ori1, &oriResV3 );

    //基准镜相对俯仰
    dR1[0] = cos(Deg2Rad(90.1032));
    dR1[3] = cos(Deg2Rad(0.2726));
    dR1[6] = cos(Deg2Rad(90.2523));
    dR1[1] = cos(Deg2Rad(90.3158));
    dR1[4] = cos(Deg2Rad(89.7483));
    dR1[7] = cos(Deg2Rad(0.4038));
    dR1[2] = cos(Deg2Rad(0.3357));
    dR1[5] = cos(Deg2Rad(89.8850));
    dR1[8] = cos(Deg2Rad(89.6846));
    dT1[0] = 0.0698878;
    dT1[1] = -0.1090252;
    dT1[2] = -0.1029593;
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = dAngle1[0];
    ori1.ori.phi = dAngle1[1];
    ori1.ori.kap = dAngle1[2];
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes4;
    mlExOriTrans( &oriResV3, &ori1, &oriRes4 );

    //右相机相对左相机基准镜的姿态
    //相对姿态
    //--------------------------------
//    dR1[0] = cos(Deg2Rad(0.2988));
//    dR1[3] = cos(Deg2Rad(90.0116));
//    dR1[6] = cos(Deg2Rad(89.7014));
//    dR1[1] = cos(Deg2Rad(89.9886));
//    dR1[4] = cos(Deg2Rad(0.0312));
//    dR1[7] = cos(Deg2Rad(89.9710));
//    dR1[2] = cos(Deg2Rad(90.2944));
//    dR1[5] = cos(Deg2Rad(90.0289));
//    dR1[8] = cos(Deg2Rad(0.2959));
//    dT1[0] = 0.26991;
//    dT1[1] = -0.00017;
//    dT1[2] = -0.00045;
//    mlGetOPKAngle( dR1, dAngle1 );
//    ori1.ori.omg = dAngle1[0];
//    ori1.ori.phi = dAngle1[1];
//    ori1.ori.kap = dAngle1[2];
//    ori1.pos.X = dT1[0];
//    ori1.pos.Y = dT1[1];
//    ori1.pos.Z = dT1[2];
    //------------------------
    ori1.ori.omg = 0;
    ori1.ori.phi = 0;
    ori1.ori.kap = 0;
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ///////////////////////////

    ExOriPara oriResV4;
    mlExOriTrans( &oriRes4, &ori1, &oriResV4 );

    //相机相对基准镜
    dT1[0] = 0.022863;
    dT1[1] = -0.030159;
    dT1[2] = -0.006573;
    mlGetOPKAngle( dR1, dAngle1 );
    ori1.ori.omg = 0;
    ori1.ori.phi = 0;
    ori1.ori.kap = 0;
    ori1.pos.X = dT1[0];
    ori1.pos.Y = dT1[1];
    ori1.pos.Z = dT1[2];
    ExOriPara oriRes5;
    mlExOriTrans( &oriResV4, &ori1, &oriRes5 );

    //相机相对像空间坐标系
    ori1.ori.omg = Deg2Rad(180);
    ori1.ori.phi = 0;
    ori1.ori.kap = 0;
    ori1.pos.X = 0;
    ori1.pos.Y = 0;
    ori1.pos.Z = 0;
    ExOriPara oriRes6;
    mlExOriTrans( &oriRes5, &ori1, &oriRes6 );

    *oriFinalRes = oriRes6;

    return true;
}

int main(int argc, char *argv[])
{
//    CmlSparseMat clsSparMat;
//    clsSparMat.Initial( 4, 3 );
//
//    clsSparMat.SetAt( 0, 0,  1);
//    clsSparMat.SetAt( 0, 1,  4);
//    clsSparMat.SetAt( 0, 2,  6);
//
//    clsSparMat.SetAt( 1, 0,  2);
//    clsSparMat.SetAt( 1, 1,  5);
//    clsSparMat.SetAt( 1, 2,  10);
//
//    clsSparMat.SetAt( 2, 0,  4);
//    clsSparMat.SetAt( 2, 1,  2);
//    clsSparMat.SetAt( 2, 2,  9);
//
//    clsSparMat.SetAt( 3, 0,  2);
//    clsSparMat.SetAt( 3, 1,  9);
//    clsSparMat.SetAt( 3, 2,  5);
//
//    vector<DOUBLE> vecB,vecX;
//    vecB.push_back( 23);
//    vecB.push_back(12);
//    vecB.push_back(24);
//    vecB.push_back(66);
//
//    clsSparMat.QRSolve( vecB, vecX);
//    void* pGeo = NULL;
//    mlGetGeoFilePtr( "/home/whwan/trunk/program/TestProj/SiteMapping/Mars/dem.tif", pGeo );
//
//    Pt2d ptXY;
//    ptXY.X = 160;
//    ptXY.Y = 176;
//    DOUBLE dZ = 0 ;
//
//    mlGetGeoZ( pGeo, ptXY, dZ );
//    mlFreeGeoPtr( pGeo );

//    ExOriPara exOriL;
//    SINT nXOffMin = 0;
//    exOriL.ori.omg = Deg2Rad(-165);
//
//    mlCalcMinXOff( 1.5, 1024, 3333, 0.27, exOriL, nXOffMin );
//
//    int kk = 1;

//    ExOriPara exOriL;
//    exOriL.ori.omg = Deg2Rad(45);
//    stuBlockDeal stuBDRes;
//    mlCalcDisparityPerRow( 0.03, 1.5, 1024, 3, 21, 100, 1189, exOriL, stuBDRes );
//
//    for( UINT i = 0; i < stuBDRes.vecnDisp.size(); ++i )
//    {
//        SINT nT = stuBDRes.vecnDisp[i];
//        int k = 1;
//    }

//    vector<Pt2i> vecPtsL, vecPtsR;
//    mlExtractFeatPtByForstner( "/home/whwan/L_2.bmp", vecPtsL, 5 );
//    mlExtractFeatPtByForstner( "/home/whwan/R_2.bmp", vecPtsR, 5 );
//
//    vector<StereoMatchPt> vecSMPts;
//    MatchInRegPara matchPara;
//    mlTemplateMatchInFeatPtsVT( "/home/whwan/L_2.bmp", "/home/whwan/R_2.bmp", vecPtsL, vecPtsR, vecSMPts, matchPara, 4 );
//
//    UINT nLT = vecPtsL.size();
//    UINT nRT = vecPtsR.size();
//    UINT nAll = vecSMPts.size();
//
//    StereoMatchPt ptC = vecSMPts[0];
//
//    printf( "%d", SINT(vecSMPts.size()) );
//    printf( "----\n" );
//    int kk = 1;
//---------------------------------
//    DOUBLE dR[9], dAngle[3];
//    dR[0] = 0.9999773;
//    dR[1] = 0.0039038;
//    dR[2] = -0.0054912;
//
//    dR[3] = -0.0038940;
//    dR[4] = 0.9999908;
//    dR[5] = 0.0017878;
//
//    dR[6] = 0.0054981;
//    dR[7] = -0.0017664;
//    dR[8] = 0.9999833;
//
//
//    mlGetOPKAngle( dR, dAngle );
//
//    dAngle[0] = Rad2Deg( dAngle[0]);
//    dAngle[1] = Rad2Deg( dAngle[1]);
//    dAngle[2] = Rad2Deg( dAngle[2]);

//---------------------------------
//    Pt3d pt1, pt2, pt3, pt4;
//    pt2.X = 1;
//    pt2.Y = 1;
//    pt3.X = 1;
//    pt3.Y = 1;
//    pt3.Z = 1;
//    pt4.X = 0;
//    pt4.Y = 0;
//    pt4.Z = 1;
//    vector<Pt3d> vecPts;
//    vecPts.push_back( pt1 );
//    vecPts.push_back( pt2 );
//    vecPts.push_back( pt3 );
//
//    Pt3d ptNormal;

//    mlPlaneNormal( vecPts, ptNormal );
//
//    int kk = 1;
//    char *c = "sdf";
//    string str;
//    str = c;
//    int kk = 1;
//    mlGeoFileTrans( "/home/whwan/0580-FB04-No6-dem-ty.tif", "/home/whwan/0580-FB04-No6-dem-ty-F32.tif", T_Float32 );
//    CmlFrameImage clsImg;
//    clsImg.LoadFile( "/home/whwan/L2Temp.tif" );
// //   clsImg.GrayTensile(  );
//    clsImg.HistogramEqualize();
////    clsImg.WallisFilter( 43, 128,200, 60, 0.4 );
////    clsImg.ExtractFeatPtByForstner( 10, 0, 0.5 );
////    clsImg.DrawCrossMark( clsImg.m_vecFeaPtsList );
////
//    clsImg.SaveFile( "/home/whwan/L2Out1.tif" );

 //   mlGeoFileTrans( "/home/whwan/0580-FB04-No6-dem-ty.tif", "/home/whwan/0580-FB04-No6-dem-ty_F32.tif", T_Float32 );

//////////////////////////////

//    ExOriPara oriFinalRes;
//    CalcLOPKs( 0, 0, &oriFinalRes );
////    oriFinalRes.ori.omg = Rad2Deg( oriFinalRes.ori.omg );
////    oriFinalRes.ori.phi = Rad2Deg( oriFinalRes.ori.phi );
////    oriFinalRes.ori.kap = Rad2Deg( oriFinalRes.ori.kap );
//
//    DOUBLE dRMat[9];
//    dRMat[0] = 0.999812;
//    dRMat[1] = -0.017943;
//    dRMat[2] = 0.007373;
//
//    dRMat[3] = 0.0017946;
//    dRMat[4] = 0.999839;
//    dRMat[5] = -0.000412;
//
//    dRMat[6] = -0.007364;
//    dRMat[7] = 0.000545;
//    dRMat[8] = 0.999973;
//
//    DOUBLE dPos[3];
//    dPos[0] = 0.2695185;
//    dPos[1] = 0.0037814;
//    dPos[2] = 0.000165;
//
//    ExOriPara oriTest;
//    DOUBLE dOri[3];
//
//    mlGetOPKAngle(  dRMat, dOri );
//    oriTest.pos.X = dPos[0];
//    oriTest.pos.Y = dPos[1];
//    oriTest.pos.Z = dPos[2];
//
////    oriTest.pos.X = 0;
////    oriTest.pos.Y = 0;
////    oriTest.pos.Z = 0;
//
//    oriTest.ori.omg = dOri[0];
//    oriTest.ori.phi = dOri[1];
//    oriTest.ori.kap = dOri[2];
//
////    oriTest.ori.omg = 0;
////    oriTest.ori.phi = 0;
////    oriTest.ori.kap = 0;
//
//    ExOriPara oriF;
//    mlExOriTrans( &oriFinalRes, &oriTest, &oriF );
//
//    oriF.ori.omg = Rad2Deg( oriF.ori.omg );
//    oriF.ori.phi = Rad2Deg( oriF.ori.phi );
//    oriF.ori.kap = Rad2Deg( oriF.ori.kap );

    //---------------------------------
    CmlMat matX, matY, matZ;
    matX.Initial( 3,3 );
    matY.Initial( 3,3 );
    matZ.Initial( 3,3 );
//---------------------------------
    double a = -0.00967897134738;
    matX.SetAt( 0, 0, 1);
    matX.SetAt( 0, 1, 0);
    matX.SetAt( 0, 2, 0);

    matX.SetAt( 1, 0, 0);
    matX.SetAt( 1, 1, cos(a));
    matX.SetAt( 1, 2, -sin(a));

    matX.SetAt( 2, 0, 0);
    matX.SetAt( 2, 1, sin(a));
    matX.SetAt( 2, 2, cos(a));
//---------------------
    double b = -0.00584045936;
    matY.SetAt( 0, 0, cos(b));
    matY.SetAt( 0, 1, 0);
    matY.SetAt( 0, 2, sin(b));

    matY.SetAt( 1, 0, 0);
    matY.SetAt( 1, 1, 1);
    matY.SetAt( 1, 2, 0);

    matY.SetAt( 2, 0, -sin(b));
    matY.SetAt( 2, 1, 0);
    matY.SetAt( 2, 2, cos(b));
//---------------------
    double c = 0.00740040033658;
    matZ.SetAt( 0, 0, cos(c));
    matZ.SetAt( 0, 1, -sin(c));
    matZ.SetAt( 0, 2, 0);

    matZ.SetAt( 1, 0, sin(c));
    matZ.SetAt( 1, 1, cos(c));
    matZ.SetAt( 1, 2, 0);

    matZ.SetAt( 2, 0, 0);
    matZ.SetAt( 2, 1, 0);
    matZ.SetAt( 2, 2, 1);

    CmlMat mat1, mat2, mat3;
    mlMatMul( &matZ, &matY, &mat1 );

    mlMatMul( &mat1, &matX, &mat2 );

    int zz = 1;
//---------------------

//    CmlFrameImage clsImg, clsOut;
//    vector<Pt2i> vecFeatPtsL, vecFeatPtsR;
//    SCHAR *strpathL = "/home/whwan/L2.bmp";
//    SCHAR *strpathR = "/home/whwan/R_5.bmp";
//
//    clsImg.EdgeDetectionByCanny( strpathL, vecFeatPtsL, 40, 10, 40000 );
//    clsImg.EdgeDetectionByCanny( strpathR, vecFeatPtsR, 40, 10, 40000 );
//
//    cout << vecFeatPtsL.size() << vecFeatPtsR.size() << endl;
//    cout << endl;
//
//
//
//    clsOut.LoadFile( strpathL );
//    clsOut.DrawCrossMark( vecFeatPtsL, 1, 1 );
//
//    clsOut.SaveFile( "/home/whwan/L3Out1.bmp" );

    QApplication app(argc, argv);
    ImageViewer imageViewer;

    imageViewer.show();
    return app.exec();

//////////////////////////////////////////////////////////////
//        CmlSiteMapProj site;
//        Pt3d ptCenter;
//        site.LoadProj( "/home/wan/trunk/program/TestProj/SiteMapping/Mars/SiteMapping.smp" );
//        site.GetSiteCenter( ptCenter);
//        DOUBLE dRange = 10;
//        DOUBLE dRes = 0.1;
//        string strDemFile = "/home/wan/trunk/program/TestProj/SiteMapping/Mars/tDem1.tif";
//        string strDomFile = "/home/wan/trunk/program/TestProj/SiteMapping/Mars/tDom1.tif";
//        double dLTx = ptCenter.X - dRange ;
//        double dLTy = ptCenter.Y + dRange ;
//        double dRTx = ptCenter.X + dRange ;
//        double dRTy = ptCenter.Y - dRange ;
//
//        ExtractFeatureOpt extractPtsOpts;
//        MatchInRegPara matchOpts;
//        RANSACHomePara ransacOpts;
//        matchOpts.dXMax = 0;
//        matchOpts.dXMin = -300;
//        matchOpts.dYMax = 2;
//        matchOpts.dYMin = -2;
//        MedFilterOpts mFilterOpts;
//
//        site.CreateDemAndDom( dLTx, dLTy, dRTx, dRTy, dRes, 1, 1, -1, true, false, extractPtsOpts, matchOpts, ransacOpts, mFilterOpts, strDemFile.c_str(), strDomFile.c_str());
////////////////////////////////////////////////////////////////////
//        string strInputPath = "/home/wan/trunk/program/TestProj/camCalib/IFLI_SINGLELEFTCAMPOINTS.txt";
//        string strCamInfoFile = "/home/wan/trunk/program/TestProj/camCalib/caminfo.txt";
//        string strCamAccuracyFile = "/home/wan/trunk/program/TestProj/camCalib/camAcc.txt";
//        CCamCalibIO camIO ;
//        if(camIO.readCamSignPts(strInputPath))
//        {
//            if(camIO.nCamNum == 1)
//            {
//                mlSingleCamCalib(camIO.vecLImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH , camIO.inLCamPara , camIO.exLCamPara , camIO.vecErrorPts) ;
//            }
//            else if(camIO.nCamNum == 2)
//            {
//                mlStereoCamCalib(camIO.vecLImg2DPts , camIO.vecRImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH ,
//                                          camIO.inLCamPara , camIO.inRCamPara , camIO.exLCamPara , camIO.exRCamPara , camIO.exStereoPara ,camIO.vecErrorPts) ;
//            }
//            // 写相机内参数信息
//            camIO.writeCamInfoFile(strCamInfoFile.c_str()) ;
//            camIO.writeAccuracyFile(strCamAccuracyFile.c_str()) ;
//        }
/////////////////////////////////////////////////////////////////////
//        GaussianFilterOpt GauPara;
//        MatchInRegPara MatchPara;
//        RANSACHomePara RanPara;
//        MLRectSearch RectSearch;
//
//        WideOptions WidePara;
//
//        ExtractFeatureOpt ExtractPara;
//        SINT Lx, Ly, nColRange, nRowRange;
//
//        string strParaPath = "/home/wan/trunk/program/TestProj/SiteMapping/regionpara.txt";
//        string strProjPath = "/home/wan/trunk/program/TestProj/SiteMapping/Mars/SiteMapping.smp";
//        //读入参数文件
//        ifstream stm(strParaPath.c_str());
//        FILE *pIOFile;
//
//        bool bGlobal = false;
//
//        if((pIOFile = fopen(strParaPath.c_str(),"r")))
//        {
//
//            stm >> GauPara.nTemplateSize >> GauPara.dCoef ;
//            stm >> ExtractPara.nGridSize >> ExtractPara.nPtMaxNum;
//            stm >> MatchPara.nTempSize >> MatchPara.dCoefThres;
//            stm >> RanPara.dThres >> RanPara.dConfidence;
//            stm >> RectSearch.dXMin >> RectSearch.dYMin >> RectSearch.dXMax >> RectSearch.dYMax;
//            stm >> WidePara.nStep >> WidePara.nRadiusX >> WidePara.nRadiusY >> WidePara.nTemplateSize >> WidePara.dCoef  ;
//            stm >> Lx >> Ly >> nColRange >> nRowRange;
//            stm.close();
//        }
//        else
//        {
//            cout << "The photo number is wrong! \n";
//            return 1;
//        }
//        CmlSiteMapProj site;
//        if( false == site.LoadProj( strProjPath ))
//        {
//            return 1 ;
//        }
//        int nSiteID, nRollID, nImgID;
//        vector<StereoSet> vecStereoSet;
//
//        site.GetDealSet( 1, 1, 8, vecStereoSet);
//
//
//        int nNum = vecStereoSet.size();
//        vector<ImgPtSet> vecDPtL(nNum), vecDPtR(nNum);
//        vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum);
//        vector<Pt3d> vecPt3d;
//        vector<DOUBLE> vecCorr;
//
//
//        if(bGlobal)
//        {
//            site.CreateDmf(vecStereoSet, WidePara, vecFPtL, vecFPtR, vecDPtL, vecDPtR);
//
//        }
//        else
//        {
//            //site.CreateRegionDmf(vecStereoSet, ExtractPara, WidePara, Lx, Ly, nColRange, nRowRange, vecDPtL, vecDPtR, vecPt3d, vecCorr);
//            site.CreateRegionDmf(vecStereoSet, GauPara, ExtractPara, MatchPara, RanPara, RectSearch, WidePara, Lx, Ly, nColRange, nRowRange, vecDPtL, vecDPtR, vecPt3d, vecCorr);
//
//        }
//        return 1;
///////////////////////////////////////////////////////////////
//Test;
    //本体系下外方位
//    ExOriPara oriBase;
//
//    //桅杆相对本体矩阵
//    DOUBLE dR1[9], dT1[3];
//    dR1[0] = cos(Deg2Rad(0.1704));
//    dR1[3] = cos(Deg2Rad(89.8822));
//    dR1[6] = cos(Deg2Rad(89.8769));
//    dR1[1] = cos(Deg2Rad(90.1175));
//    dR1[4] = cos(Deg2Rad(0.1934));
//    dR1[7] = cos(Deg2Rad(90.1535));
//    dR1[2] = cos(Deg2Rad(90.1234));
//    dR1[5] = cos(Deg2Rad(89.8467));
//    dR1[8] = cos(Deg2Rad(0.1968));
//    dT1[0] = 0.5332004;
//    dT1[1] = -0.0015296;
//    dT1[2] = -0.5707493;
//    ExOriPara ori1;
//    DOUBLE dAngle1[3];
//    mlGetOPKAngle( dR1, dAngle1 );
//    ori1.ori.omg = dAngle1[0];
//    ori1.ori.phi = dAngle1[1];
//    ori1.ori.kap = dAngle1[2];
//    ori1.pos.X = dT1[0];
//    ori1.pos.Y = dT1[1];
//    ori1.pos.Z = dT1[2];
//    ExOriPara oriRes1;
//    mlExOriTrans( &oriBase, &ori1, &oriRes1 );
//
//    //桅杆展开角度
//    ori1.ori.omg = 0;
//    ori1.ori.phi = Deg2Rad(-0.044);
//    ori1.ori.kap = 0;
//    ori1.pos.X = 0;
//    ori1.pos.Y = 0;
//    ori1.pos.Z = 0;
//    ExOriPara oriResV1;
//    mlExOriTrans( &oriRes1, &ori1, &oriResV1 );
//
//    //偏航相对桅杆
//    dR1[0] = cos(Deg2Rad(0.0));
//    dR1[3] = cos(Deg2Rad(90.0));
//    dR1[6] = cos(Deg2Rad(90.0));
//    dR1[1] = cos(Deg2Rad(90.0));
//    dR1[4] = cos(Deg2Rad(0.051));
//    dR1[7] = cos(Deg2Rad(89.9490));
//    dR1[2] = cos(Deg2Rad(90.0));
//    dR1[5] = cos(Deg2Rad(90.0510));
//    dR1[8] = cos(Deg2Rad(0.0510));
//    dT1[0] = -0.0850554;
//    dT1[1] = -0.0479819;
//    dT1[2] = 0;
//    mlGetOPKAngle( dR1, dAngle1 );
//    ori1.ori.omg = dAngle1[0];
//    ori1.ori.phi = dAngle1[1];
//    ori1.ori.kap = dAngle1[2];
//    ori1.pos.X = dT1[0];
//    ori1.pos.Y = dT1[1];
//    ori1.pos.Z = dT1[2];
//    ExOriPara oriRes2;
//    mlExOriTrans( &oriResV1, &ori1, &oriRes2 );
//
//    //偏航角旋转角度
//    ori1.ori.omg = 0;
//    ori1.ori.phi = 0;
//    ori1.ori.kap = Deg2Rad(-39.883);//-59.9670
//    ori1.pos.X = 0;
//    ori1.pos.Y = 0;
//    ori1.pos.Z = 0;
//    ExOriPara oriResV2;
//    mlExOriTrans( &oriRes2, &ori1, &oriResV2 );
//
//    //俯仰相对偏航
//    dR1[0] = cos(Deg2Rad(0.093));
//    dR1[3] = cos(Deg2Rad(90.0930));
//    dR1[6] = cos(Deg2Rad(90.0001));
//    dR1[1] = cos(Deg2Rad(89.9070));
//    dR1[4] = cos(Deg2Rad(0.1554));
//    dR1[7] = cos(Deg2Rad(89.8755));
//    dR1[2] = cos(Deg2Rad(90.0002));
//    dR1[5] = cos(Deg2Rad(90.1245));
//    dR1[8] = cos(Deg2Rad(0.1245));
//    dT1[0] = 0.0003225;
//    dT1[1] = -0.0000005;
//    dT1[2] = -0.6493588;
//    mlGetOPKAngle( dR1, dAngle1 );
//    ori1.ori.omg = dAngle1[0];
//    ori1.ori.phi = dAngle1[1];
//    ori1.ori.kap = dAngle1[2];
//    ori1.pos.X = dT1[0];
//    ori1.pos.Y = dT1[1];
//    ori1.pos.Z = dT1[2];
//    ExOriPara oriRes3;
//    mlExOriTrans( &oriResV2, &ori1, &oriRes3 );
//
//    //俯仰角旋转角度
//    ori1.ori.omg = 0;
//    ori1.ori.phi = Deg2Rad(-29.9730);//
//    ori1.ori.kap = 0;
//    ori1.pos.X = 0;
//    ori1.pos.Y = 0;
//    ori1.pos.Z = 0;
//    ExOriPara oriResV3;
//    mlExOriTrans( &oriRes3, &ori1, &oriResV3 );
//
//    //基准镜相对俯仰
//    dR1[0] = cos(Deg2Rad(90.1032));
//    dR1[3] = cos(Deg2Rad(0.2726));
//    dR1[6] = cos(Deg2Rad(90.2523));
//    dR1[1] = cos(Deg2Rad(90.3158));
//    dR1[4] = cos(Deg2Rad(89.7483));
//    dR1[7] = cos(Deg2Rad(0.4038));
//    dR1[2] = cos(Deg2Rad(0.3357));
//    dR1[5] = cos(Deg2Rad(89.8850));
//    dR1[8] = cos(Deg2Rad(89.6846));
//    dT1[0] = 0.0698878;
//    dT1[1] = -0.1090252;
//    dT1[2] = -0.1029593;
//    mlGetOPKAngle( dR1, dAngle1 );
//    ori1.ori.omg = dAngle1[0];
//    ori1.ori.phi = dAngle1[1];
//    ori1.ori.kap = dAngle1[2];
//    ori1.pos.X = dT1[0];
//    ori1.pos.Y = dT1[1];
//    ori1.pos.Z = dT1[2];
//    ExOriPara oriRes4;
//    mlExOriTrans( &oriResV3, &ori1, &oriRes4 );
//
//    //右相机相对左相机基准镜的姿态
//    //相对姿态
//    //--------------------------------
////    dR1[0] = cos(Deg2Rad(0.2988));
////    dR1[3] = cos(Deg2Rad(90.0116));
////    dR1[6] = cos(Deg2Rad(89.7014));
////    dR1[1] = cos(Deg2Rad(89.9886));
////    dR1[4] = cos(Deg2Rad(0.0312));
////    dR1[7] = cos(Deg2Rad(89.9710));
////    dR1[2] = cos(Deg2Rad(90.2944));
////    dR1[5] = cos(Deg2Rad(90.0289));
////    dR1[8] = cos(Deg2Rad(0.2959));
////    dT1[0] = 0.26991;
////    dT1[1] = -0.00017;
////    dT1[2] = -0.00045;
////    mlGetOPKAngle( dR1, dAngle1 );
////    ori1.ori.omg = dAngle1[0];
////    ori1.ori.phi = dAngle1[1];
////    ori1.ori.kap = dAngle1[2];
////    ori1.pos.X = dT1[0];
////    ori1.pos.Y = dT1[1];
////    ori1.pos.Z = dT1[2];
//    //------------------------
//    ori1.ori.omg = 0;
//    ori1.ori.phi = 0;
//    ori1.ori.kap = 0;
//    ori1.pos.X = 0;
//    ori1.pos.Y = 0;
//    ori1.pos.Z = 0;
//    ///////////////////////////
//
//    ExOriPara oriResV4;
//    mlExOriTrans( &oriRes4, &ori1, &oriResV4 );
//
//    //相机相对基准镜
//    dT1[0] = 0.022863;
//    dT1[1] = -0.030159;
//    dT1[2] = -0.006573;
//    mlGetOPKAngle( dR1, dAngle1 );
//    ori1.ori.omg = 0;
//    ori1.ori.phi = 0;
//    ori1.ori.kap = 0;
//    ori1.pos.X = dT1[0];
//    ori1.pos.Y = dT1[1];
//    ori1.pos.Z = dT1[2];
//    ExOriPara oriRes5;
//    mlExOriTrans( &oriResV4, &ori1, &oriRes5 );
//
//    //相机相对像空间坐标系
//    ori1.ori.omg = Deg2Rad(180);
//    ori1.ori.phi = 0;
//    ori1.ori.kap = 0;
//    ori1.pos.X = 0;
//    ori1.pos.Y = 0;
//    ori1.pos.Z = 0;
//    ExOriPara oriRes6;
//    mlExOriTrans( &oriRes5, &ori1, &oriRes6 );

//------------------------------------------
//    ExOriPara outRes, outRes1, outRes2;
//    CalcLOPKs( -59.967, -29.973, &outRes1 );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf  ", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//    CalcROPKs( -59.967, -29.973, &outRes2 );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//
//    ExOriPara outRela;
//    mlGetRelaOri( &outRes1, &outRes2, &outRela );
//    DOUBLE dTempA[3];
//    dTempA[0] = Rad2Deg(outRela.ori.omg);
//    dTempA[1] = Rad2Deg(outRela.ori.phi);
//    dTempA[2] = Rad2Deg(outRela.ori.kap);
//    int kk =1;
//-----------------------------

//    FILE *pF = fopen( "/home/whwan/trunk/program/TestProj/res.txt", "w" );
//
//    ExOriPara outRes;
//    CalcLOPKs( -59.967, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//    CalcROPKs( -59.967, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//
//
//
//    CalcLOPKs( -39.883, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//    CalcROPKs( -39.883, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//
//
//    CalcLOPKs( -19.886, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//    CalcROPKs( -19.886, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//
//
//    CalcLOPKs( 0.088, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//    CalcROPKs( 0.088, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//
//
//    CalcLOPKs( 20.082, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//    CalcROPKs( 20.062, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//
//
//    CalcLOPKs( 40.125, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//    CalcROPKs( 40.125, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//
//
//    CalcLOPKs( 60.099, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//    CalcROPKs( 60.099, -29.973, &outRes );
//    fprintf( pF, "%lf  %lf  %lf  %lf  %lf  %lf\n", outRes.pos.X, outRes.pos.Y, outRes.pos.Z, Rad2Deg(outRes.ori.omg), Rad2Deg(outRes.ori.phi), Rad2Deg(outRes.ori.kap) );
//
//    fclose( pF );
//
//
////    DOUBLE dO = Rad2Deg( oriRes6.ori.omg );
////    DOUBLE dP = Rad2Deg( oriRes6.ori.phi );
////    DOUBLE dK = Rad2Deg( oriRes6.ori.kap );
//
//    int kk = 1;
//    return 1;


}
