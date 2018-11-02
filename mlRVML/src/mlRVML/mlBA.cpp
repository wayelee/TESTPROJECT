#include "mlBA.h"
#include <map>
#include "mlMat.h"
#include "mlBase.h"
#include "mlPhgProc.h"
#include "mlFrameImage.h"
#include "mlSparseMat.h"
using namespace std;
////////////////////////////////////////////////////////
//bool getxyFromXYZ( Pt2d &ptxy, Pt3d ptXYZ, Pt3d ptXYZOrg, CmlMat matOPK, DOUBLE fx, DOUBLE fy )
//{
//    DOUBLE dX = ptXYZ.X - ptXYZOrg.X;
//    DOUBLE dY = ptXYZ.Y - ptXYZOrg.Y;
//    DOUBLE dZ = ptXYZ.Z - ptXYZOrg.Z;
//
//    DOUBLE dCoefX = matOPK.GetAt( 0, 0 ) * dX + matOPK.GetAt( 1, 0 ) * dY + matOPK.GetAt( 2, 0 ) * dZ;
//    DOUBLE dCoefY = matOPK.GetAt( 0, 1 ) * dX + matOPK.GetAt( 1, 1 ) * dY + matOPK.GetAt( 2, 1 ) * dZ;
//    DOUBLE dCoefZ = matOPK.GetAt( 0, 2 ) * dX + matOPK.GetAt( 1, 2 ) * dY + matOPK.GetAt( 2, 2 ) * dZ;
//
//    ptxy.X = -fx * dCoefX / dCoefZ;
//    ptxy.Y = -fy * dCoefY / dCoefZ;
//
//    return true;
//}

bool getCoefA_B( Pt2d pt, ExOriPara* pCamEx, DOUBLE fx, DOUBLE fy, Pt3d ptXYZ, DOUBLE* dCoefA_B )
{
	CmlMat matR;
	OPK2RMat( &pCamEx->ori, &matR );
	DOUBLE dR[9];
	memcpy( dR, matR.GetData(), sizeof(DOUBLE)*9 );

	DOUBLE dMinusXYZ[3];
	dMinusXYZ[0] = ptXYZ.X - pCamEx->pos.X;
	dMinusXYZ[1] = ptXYZ.Y - pCamEx->pos.Y;
	dMinusXYZ[2] = ptXYZ.Z - pCamEx->pos.Z;

	DOUBLE dCoefZ = dR[2]*dMinusXYZ[0] + dR[5]*dMinusXYZ[1] + dR[8]*dMinusXYZ[2];
	DOUBLE dCoefX = dR[0]*dMinusXYZ[0] + dR[3]*dMinusXYZ[1] + dR[6]*dMinusXYZ[2];
	DOUBLE dCoefY = dR[1]*dMinusXYZ[0] + dR[4]*dMinusXYZ[1] + dR[7]*dMinusXYZ[2];
	dCoefA_B[0] = -fx / dCoefZ / dCoefZ  *( dR[0]*dCoefZ - dR[2]*dCoefX );
	dCoefA_B[1] = -fx / dCoefZ / dCoefZ  *( dR[3]*dCoefZ - dR[5]*dCoefX );
	dCoefA_B[2] = -fx / dCoefZ / dCoefZ  *( dR[6]*dCoefZ - dR[8]*dCoefX );

	dCoefA_B[3] = -fy / dCoefZ / dCoefZ  *( dR[1]*dCoefZ - dR[2]*dCoefY );
	dCoefA_B[4] = -fy / dCoefZ / dCoefZ  *( dR[4]*dCoefZ - dR[5]*dCoefY );
	dCoefA_B[5] = -fy / dCoefZ / dCoefZ  *( dR[7]*dCoefZ - dR[8]*dCoefY );

	return TRUE;
}
bool getCoefA_B_EpiR( Pt2d pt, ExOriPara* pCamExL, DOUBLE dBaseLength, DOUBLE fx, DOUBLE fy, Pt3d ptXYZ, DOUBLE* dCoefA_B )
{
	CmlMat matR;
	OPK2RMat( &pCamExL->ori, &matR );
	DOUBLE dR[9];
	memcpy( dR, matR.GetData(), sizeof(DOUBLE)*9 );

	DOUBLE dMinusXYZ[3];
	dMinusXYZ[0] = ptXYZ.X - pCamExL->pos.X - dBaseLength*dR[0];
	dMinusXYZ[1] = ptXYZ.Y - pCamExL->pos.Y - dBaseLength*dR[3];
	dMinusXYZ[2] = ptXYZ.Z - pCamExL->pos.Z - dBaseLength*dR[6];

	DOUBLE dCoefZ = dR[2]*dMinusXYZ[0] + dR[5]*dMinusXYZ[1] + dR[8]*dMinusXYZ[2];
	DOUBLE dCoefX = dR[0]*dMinusXYZ[0] + dR[3]*dMinusXYZ[1] + dR[6]*dMinusXYZ[2];
	DOUBLE dCoefY = dR[1]*dMinusXYZ[0] + dR[4]*dMinusXYZ[1] + dR[7]*dMinusXYZ[2];
	dCoefA_B[0] = -fx / dCoefZ / dCoefZ  *( dR[0]*dCoefZ - dR[2]*dCoefX );
	dCoefA_B[1] = -fx / dCoefZ / dCoefZ  *( dR[3]*dCoefZ - dR[5]*dCoefX );
	dCoefA_B[2] = -fx / dCoefZ / dCoefZ  *( dR[6]*dCoefZ - dR[8]*dCoefX );

	dCoefA_B[3] = -fy / dCoefZ / dCoefZ  *( dR[1]*dCoefZ - dR[2]*dCoefY );
	dCoefA_B[4] = -fy / dCoefZ / dCoefZ  *( dR[4]*dCoefZ - dR[5]*dCoefY );
	dCoefA_B[5] = -fy / dCoefZ / dCoefZ  *( dR[7]*dCoefZ - dR[8]*dCoefY );

	return TRUE;
}
bool getCoefA_A( Pt2d pt, ExOriPara* pCamEx, DOUBLE fx, DOUBLE fy, Pt3d ptXYZ, DOUBLE* dCoefA_A )
{
	CmlMat matR;
	OPK2RMat( &pCamEx->ori, &matR );
	DOUBLE dR[9];
	memcpy( dR, matR.GetData(), sizeof(DOUBLE)*9 );

	DOUBLE dXt, dYt, dZt;
	dXt = ptXYZ.X - pCamEx->pos.X;
	dYt = ptXYZ.Y - pCamEx->pos.Y;
	dZt = ptXYZ.Z - pCamEx->pos.Z;
	DOUBLE dXCoef = dR[0]*dXt + dR[3]*dYt + dR[6]*dZt;
	DOUBLE dYCoef = dR[1]*dXt + dR[4]*dYt + dR[7]*dZt;
	DOUBLE dZCoef = dR[2]*dXt + dR[5]*dYt + dR[8]*dZt;
	DOUBLE dZCoefRev = 1.0 / dZCoef;
	DOUBLE dAngle[3];
	dAngle[0] = pCamEx->ori.omg;
	dAngle[1] = pCamEx->ori.phi;
	dAngle[2] = pCamEx->ori.kap;

	dCoefA_A[0] = -fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[2] - dZCoef*dR[0] );

	dCoefA_A[1] = -fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[5] - dZCoef*dR[3] );

	dCoefA_A[2] = -fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[8] - dZCoef*dR[6] );

	dCoefA_A[3] = fx * dZCoefRev * dZCoefRev * ( dXCoef*( -dR[8]*dYt + dR[5]*dZt ) - dZCoef*(-dR[6]*dYt + dR[3]*dZt) );

	dCoefA_A[4] = fx * dZCoefRev * dZCoefRev * ( dXCoef*( cos(dAngle[1])*dXt + sin(dAngle[0])*sin(dAngle[1])*dYt - cos(dAngle[0])*sin(dAngle[1])*dZt) -
		dZCoef*( -sin(dAngle[1])*cos(dAngle[2])*dXt + sin(dAngle[0])*cos(dAngle[1])*cos(dAngle[2])*dYt - cos(dAngle[0])*cos(dAngle[1])*cos(dAngle[2])*dZt));

	dCoefA_A[5] = -fx * dZCoefRev *( dR[1]*dXt + dR[4]*dYt + dR[7]*dZt );

	dCoefA_A[6] = -fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[2] - dZCoef*dR[1] );

	dCoefA_A[7] = -fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[5] - dZCoef*dR[4] );

	dCoefA_A[8] = -fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[8] - dZCoef*dR[7] );

	dCoefA_A[9] = fy * dZCoefRev * dZCoefRev * ( dYCoef*( -dR[8]*dYt + dR[5]*dZt ) - dZCoef*(-dR[7]*dYt + dR[4]*dZt) );

	dCoefA_A[10] = fy * dZCoefRev * dZCoefRev * ( dYCoef*( cos(dAngle[1])*dXt + sin(dAngle[0])*sin(dAngle[1])*dYt - cos(dAngle[0])*sin(dAngle[1])*dZt) -
		dZCoef*( sin(dAngle[1])*sin(dAngle[2])*dXt - sin(dAngle[0])*cos(dAngle[1])*sin(dAngle[2])*dYt + cos(dAngle[0])*cos(dAngle[1])*sin(dAngle[2])*dZt));

	dCoefA_A[11] = fy * dZCoefRev *( dR[0]*dXt + dR[3]*dYt + dR[6]*dZt );

	return true;

}

bool getCoefA_A_EpiR( Pt2d pt, ExOriPara* pCamExL, DOUBLE dBaseLength, DOUBLE fx, DOUBLE fy, Pt3d ptXYZ, DOUBLE* dCoefA_A )
{
	CmlMat matR;
	OPK2RMat( &pCamExL->ori, &matR );
	DOUBLE dR[9];
	memcpy( dR, matR.GetData(), sizeof(DOUBLE)*9 );

	DOUBLE dXt, dYt, dZt;
	Pt3d ptRCamPos;
	ptRCamPos.X = dR[0]*dBaseLength + pCamExL->pos.X;
	ptRCamPos.Y = dR[3]*dBaseLength + pCamExL->pos.Y;
	ptRCamPos.Z = dR[6]*dBaseLength + pCamExL->pos.Z;

	dXt = ptXYZ.X - ptRCamPos.X;
	dYt = ptXYZ.Y - ptRCamPos.Y;
	dZt = ptXYZ.Z - ptRCamPos.Z;

	DOUBLE dXCoef = dR[0]*dXt + dR[3]*dYt + dR[6]*dZt;
	DOUBLE dYCoef = dR[1]*dXt + dR[4]*dYt + dR[7]*dZt;
	DOUBLE dZCoef = dR[2]*dXt + dR[5]*dYt + dR[8]*dZt;
	DOUBLE dZCoefRev = 1.0 / dZCoef;
	DOUBLE dAngle[3];
	dAngle[0] = pCamExL->ori.omg;
	dAngle[1] = pCamExL->ori.phi;
	dAngle[2] = pCamExL->ori.kap;
	DOUBLE dSinOmg = sin(dAngle[0]);
	DOUBLE dSinPhi = sin(dAngle[1]);
	DOUBLE dSinKap = sin(dAngle[2]);

	DOUBLE dCosOmg = cos(dAngle[0]);
	DOUBLE dCosPhi = cos(dAngle[1]);
	DOUBLE dCosKap = cos(dAngle[2]);

	DOUBLE dXtTOmg, dYtTOmg, dZtTOmg, dXtTPhi, dYtTPhi, dZtTPhi, dXtTKap, dYtTKap, dZtTKap;
	DOUBLE da1TOmg = 0;
	DOUBLE db1TOmg = dCosOmg*dSinPhi*dCosKap-dSinOmg*dSinKap;
	DOUBLE dc1TOmg = dSinOmg*dSinPhi*dCosKap+dSinOmg*dSinKap;
	DOUBLE da2TOmg = 0;
	DOUBLE db2TOmg = -dCosOmg*dSinPhi*dSinKap-dSinOmg*dCosKap;
	DOUBLE dc2TOmg = -dSinOmg*dSinPhi*dSinKap+dCosOmg*dCosKap;
	DOUBLE da3TOmg = 0;
	DOUBLE db3TOmg = -dCosOmg*dCosPhi;
	DOUBLE dc3TOmg = -dSinOmg*dCosPhi;
	dXtTOmg = dXt*da1TOmg - 2*dBaseLength*dR[0]*da1TOmg + dYt*(db1TOmg) - 2*dBaseLength*db1TOmg*dR[3] + dZt*dc1TOmg - 2*dBaseLength*dc1TOmg*dR[6];
	dYtTOmg = dXt*da2TOmg - dBaseLength*(da1TOmg*dR[1]+da2TOmg*dR[0])+dYt*db2TOmg - dBaseLength*(db1TOmg*dR[4]+db2TOmg*dR[3]) + dZt*dc2TOmg - dBaseLength*(dc1TOmg*dR[7]+dc2TOmg*dR[6]);
    dZtTOmg = dXt*da3TOmg - dBaseLength*(da1TOmg*dR[2]+da3TOmg*dR[0])+dYt*db3TOmg - dBaseLength*(db1TOmg*dR[5]+db3TOmg*dR[3]) + dZt*dc3TOmg - dBaseLength*(dc1TOmg*dR[8]+dc3TOmg*dR[6]);

    dCoefA_A[3] = -fx * dZCoefRev * dZCoefRev * ( dXtTOmg * dZCoef - dZtTOmg * dXCoef );
    dCoefA_A[9] = -fy * dZCoefRev * dZCoefRev * ( dYtTOmg * dZCoef - dZtTOmg * dYCoef );
    //--------------

    DOUBLE da1TPhi = -dSinPhi*dCosKap;
	DOUBLE db1TPhi = dSinOmg*dCosPhi*dCosKap;
	DOUBLE dc1TPhi = -dCosOmg*dCosPhi*dCosKap;
	DOUBLE da2TPhi = dSinPhi*dSinKap;
	DOUBLE db2TPhi = -dSinOmg*dCosPhi*dSinKap;
	DOUBLE dc2TPhi = dCosOmg*dCosPhi*dSinKap;
	DOUBLE da3TPhi = dCosPhi;
	DOUBLE db3TPhi = dSinOmg*dSinPhi;
	DOUBLE dc3TPhi = -dCosOmg*dSinPhi;
	dXtTPhi = dXt*da1TPhi - 2*dBaseLength*dR[0]*da1TPhi + dYt*(db1TPhi) - 2*dBaseLength*db1TPhi*dR[3] + dZt*dc1TPhi - 2*dBaseLength*dc1TPhi*dR[6];
	dYtTPhi = dXt*da2TPhi - dBaseLength*(da1TPhi*dR[1]+da2TPhi*dR[0])+dYt*db2TPhi - dBaseLength*(db1TPhi*dR[4]+db2TPhi*dR[3]) + dZt*dc2TPhi - dBaseLength*(dc1TPhi*dR[7]+dc2TPhi*dR[6]);
    dZtTPhi = dXt*da3TPhi - dBaseLength*(da1TPhi*dR[2]+da3TPhi*dR[0])+dYt*db3TPhi - dBaseLength*(db1TPhi*dR[5]+db3TPhi*dR[3]) + dZt*dc3TPhi - dBaseLength*(dc1TPhi*dR[8]+dc3TPhi*dR[6]);

    dCoefA_A[4] = -fx * dZCoefRev * dZCoefRev * ( dXtTPhi * dZCoef - dZtTPhi * dXCoef );
    dCoefA_A[10] = -fy * dZCoefRev * dZCoefRev * ( dYtTPhi * dZCoef - dZtTPhi * dYCoef );

    //---------------------------
    DOUBLE da1Tkap = -dCosPhi*dSinKap;
	DOUBLE db1Tkap = -dSinOmg*dSinPhi*dSinKap+dCosOmg*dCosKap;
	DOUBLE dc1Tkap = dCosOmg*dSinPhi*dSinKap+dSinOmg*dCosKap;
	DOUBLE da2Tkap = -dCosPhi*dCosKap;
	DOUBLE db2Tkap = -dSinOmg*dSinPhi*dCosKap-dCosOmg*dSinKap;
	DOUBLE dc2Tkap = dCosOmg*dSinPhi*dCosKap-dSinOmg*dSinKap;
	DOUBLE da3Tkap = 0;
	DOUBLE db3Tkap = 0;
	DOUBLE dc3Tkap = 0;
	dXtTKap = dXt*da1Tkap - 2*dBaseLength*dR[0]*da1Tkap + dYt*(db1Tkap) - 2*dBaseLength*db1Tkap*dR[3] + dZt*dc1Tkap - 2*dBaseLength*dc1Tkap*dR[6];
	dYtTKap = dXt*da2Tkap - dBaseLength*(da1Tkap*dR[1]+da2Tkap*dR[0])+dYt*db2Tkap - dBaseLength*(db1Tkap*dR[4]+db2Tkap*dR[3]) + dZt*dc2Tkap - dBaseLength*(dc1Tkap*dR[7]+dc2Tkap*dR[6]);
    dZtTKap = dXt*da3Tkap - dBaseLength*(da1Tkap*dR[2]+da3Tkap*dR[0])+dYt*db3Tkap - dBaseLength*(db1Tkap*dR[5]+db3Tkap*dR[3]) + dZt*dc3Tkap - dBaseLength*(dc1Tkap*dR[8]+dc3Tkap*dR[6]);

    dCoefA_A[5] = -fx * dZCoefRev * dZCoefRev * ( dXtTKap * dZCoef - dZtTKap * dXCoef );
    dCoefA_A[11] = -fy * dZCoefRev * dZCoefRev * ( dYtTKap * dZCoef - dZtTKap * dYCoef );


    ///////////////////////////


	dCoefA_A[0] = -fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[2] - dZCoef*dR[0] );

	dCoefA_A[1] = -fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[5] - dZCoef*dR[3] );

	dCoefA_A[2] = -fx * dZCoefRev * dZCoefRev * ( dXCoef*dR[8] - dZCoef*dR[6] );

//	dCoefA_A[3] = fx * dZCoefRev * dZCoefRev * ( dXCoef*( -dR[8]*dYt + dR[5]*dZt ) - dZCoef*(-dR[6]*dYt + dR[3]*dZt) );
//
//	dCoefA_A[4] = fx * dZCoefRev * dZCoefRev * ( dXCoef*( cos(dAngle[1])*dXt + sin(dAngle[0])*sin(dAngle[1])*dYt - cos(dAngle[0])*sin(dAngle[1])*dZt) -
//		dZCoef*( -sin(dAngle[1])*cos(dAngle[2])*dXt + sin(dAngle[0])*cos(dAngle[1])*cos(dAngle[2])*dYt - cos(dAngle[0])*cos(dAngle[1])*cos(dAngle[2])*dZt));
//
//	dCoefA_A[5] = -fx * dZCoefRev *( dR[1]*dXt + dR[4]*dYt + dR[7]*dZt );


	dCoefA_A[6] = -fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[2] - dZCoef*dR[1] );


	dCoefA_A[7] = -fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[5] - dZCoef*dR[4] );


	dCoefA_A[8] = -fy * dZCoefRev * dZCoefRev * ( dYCoef*dR[8] - dZCoef*dR[7] );

//	dCoefA_A[9] = fy * dZCoefRev * dZCoefRev * ( dYCoef*( -dR[8]*dYt + dR[5]*dZt ) - dZCoef*(-dR[7]*dYt + dR[4]*dZt) );
//
//
//	dCoefA_A[10] = fy * dZCoefRev * dZCoefRev * ( dYCoef*( cos(dAngle[1])*dXt + sin(dAngle[0])*sin(dAngle[1])*dYt - cos(dAngle[0])*sin(dAngle[1])*dZt) -
//		dZCoef*( sin(dAngle[1])*sin(dAngle[2])*dXt - sin(dAngle[0])*cos(dAngle[1])*sin(dAngle[2])*dYt + cos(dAngle[0])*cos(dAngle[1])*sin(dAngle[2])*dZt));
//
//
//	dCoefA_A[11] = fy * dZCoefRev *( dR[0]*dXt + dR[3]*dYt + dR[6]*dZt );

	return true;

}
////////////////////////////////////////////////////////
CmlBA::CmlBA()
{
    //ctor
}

CmlBA::~CmlBA()
{
    //dtor
}
bool CmlBA::BA_Common( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts, \
                      vector<ExOriPara> &vecImgResErr, vector<Pt3d> &vec3dPtsResErr, Pt2d &ptTotalImgResErr )
{
    if( ( vecbImgIsFixed.size()== 0 )||( vecb3dPtIsFixed.size()== 0 )||( vecImgExOris.size() == 0 )||( vecImgInOris.size() == 0 )||( vecImgPts.size() == 0 ) ||( vec3dPts.size() == 0 ) )
    {
        return false;
    }
    UINT nImgNum = vecImgInOris.size();
    if( ( vec3dPts.size() != vecb3dPtIsFixed.size() )||( nImgNum != vecbImgIsFixed.size() )||( nImgNum != vecImgPts.size() ) || ( nImgNum != vecImgExOris.size() ) )
    {
        return false;
    }
    map<ULONG, UINT> mapIndex3dPts;
    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        mapIndex3dPts.insert( map<ULONG, UINT>::value_type( vec3dPts[i].lID, i ));
    }
    UINT nPt2dNum = 0;
    UINT nPt3dNum = vec3dPts.size();

    for( UINT i = 0; i < vecImgInOris.size(); ++i )
    {
        nPt2dNum += vecImgPts[i].size();
    }
    CmlMat matA, matX, matL;


    UINT nFixed3dPtsCount = 0;
    for( UINT i = 0; i < vecb3dPtIsFixed.size(); ++i )
    {
        if( true == vecb3dPtIsFixed[i] )
        {
            ++nFixed3dPtsCount;
        }
    }
    UINT nFixedImgCount = 0;
    for( UINT i = 0; i < vecbImgIsFixed.size(); ++i )
    {
        if( true == vecbImgIsFixed[i] )
        {
            ++nFixedImgCount;
        }
    }
    UINT nRow = 2*nPt2dNum;
    UINT nCol = 6*(nImgNum-nFixedImgCount)+3*(nPt3dNum-nFixed3dPtsCount);


    CmlSparseMat clsSparMat;
    vector<DOUBLE> vecL, vecX;

    bool bIsUseSparseMat = false;
    //---------------------
    //judge
    UINT nTotal = nRow * nCol;
    if( nTotal >= MAX_DENSE_MAT_ELE_NUM )
    {
        bIsUseSparseMat = true;
    }
    //---------------------
    if( false == bIsUseSparseMat )
    {
        if( ( false == matA.Initial( nRow, nCol ))|| (false == matX.Initial( nCol, 1 ) ) || ( false == matL.Initial( nRow, 1 ) ) )
        {
            return false;
        }
    }
    else
    {
        if( false == clsSparMat.Initial( nRow, nCol ) )
        {
            return false;
        }
    }

    //-----------------------------------------------
    vector<UINT> vecNewImgIndex, vecNew3dPtIndex;
    UINT nTmpIndex = -1;
    for( UINT i = 0; i < vecImgExOris.size(); ++i )
    {
        if( false == vecbImgIsFixed[i])
        {
            nTmpIndex++;
        }
        vecNewImgIndex.push_back( nTmpIndex );
    }
    nTmpIndex = -1;
    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        if( false == vecb3dPtIsFixed[i])
        {
            nTmpIndex++;
        }
        vecNew3dPtIndex.push_back( nTmpIndex );
    }
    //-----------------------------------------------
    UINT nIndexCur = 0;
    UINT nPt3dIndex = 6*(nImgNum-nFixedImgCount);
    UINT nIterator = 0;

    DOUBLE dResErrX, dResErrY, dTotalErr;
    do
    {
        dResErrX = dResErrY = 0;
        nIndexCur = 0;
        dTotalErr = 0;

        if( false == bIsUseSparseMat )
        {
            for( UINT i = 0; i < nRow; ++i )
            {
                matL.SetAt( i, UINT(0), 0 );
                for( UINT j = 0; j < nCol; ++j )
                {
                    matA.SetAt( i, j, 0 );
                }
            }
            for( UINT i = 0; i < nCol; ++i )
            {
                matX.SetAt( i, UINT(0), 0 );
            }
        }
        else
        {
            vecX.clear();
            vecL.clear();
        }

        for( UINT i = 0; i < vecImgInOris.size(); ++i )
        {
            InOriPara inOriCur = vecImgInOris[i];
            ExOriPara exOriCur = vecImgExOris[i];

            vector<Pt2d> &vecPt2dCur = vecImgPts[i];
            for( UINT j = 0; j < vecPt2dCur.size(); ++j )
            {
                Pt2d ptCur = vecPt2dCur[j];
                map<ULONG, UINT>::iterator it = mapIndex3dPts.find( ptCur.lID );
                DOUBLE dCoefA[12], dCoefB[6];

                if( it != mapIndex3dPts.end() )
                {
                    UINT nId = it->second;
                    Pt3d ptTempXYZ = vec3dPts[nId];

                    if( vecbImgIsFixed[i] == false )
                    {
                        getCoefA_A( ptCur, &exOriCur, inOriCur.f, inOriCur.f, ptTempXYZ, dCoefA );
                        for( UINT k = 0; k < 12; ++k )
                        {
                            if( false == bIsUseSparseMat )
                            {
                                matA.SetAt( (2*(nIndexCur+j)+k/6), 6*vecNewImgIndex[i]+(k%6), dCoefA[k] );
                            }
                            else
                            {
                                clsSparMat.SetAt(  (2*(nIndexCur+j)+k/6), 6*vecNewImgIndex[i]+(k%6), dCoefA[k]  );
                            }

                        }
                    }

                    if( vecb3dPtIsFixed[nId] == false )
                    {
                        getCoefA_B( ptCur, &exOriCur, inOriCur.f, inOriCur.f, ptTempXYZ, dCoefB  );
                        for( UINT k = 0; k < 6; ++k )
                        {
                            if( false == bIsUseSparseMat )
                            {
                                 matA.SetAt( 2*(nIndexCur+j)+k/3, nPt3dIndex+3*vecNew3dPtIndex[nId]+(k%3), dCoefB[k]  );
                            }
                            else
                            {
                                clsSparMat.SetAt( 2*(nIndexCur+j)+k/3, nPt3dIndex+3*vecNew3dPtIndex[nId]+(k%3), dCoefB[k] );
                            }

                        }
                    }

                    Pt2d ptTemp;
                    CmlMat matR;
                    OPK2RMat( &exOriCur.ori, &matR );
                    getxyFromXYZ( ptTemp, ptTempXYZ, exOriCur.pos, matR, inOriCur.f, inOriCur.f );

                    if( false == bIsUseSparseMat )
                    {
                        matL.SetAt( 2*(nIndexCur+j), 0, (ptCur.X - ptTemp.X) );
                        matL.SetAt( 2*(nIndexCur+j)+1, 0, (ptCur.Y - ptTemp.Y) );
                    }
                    else
                    {
                        vecL.push_back( (ptCur.X - ptTemp.X) );
                        vecL.push_back( (ptCur.Y - ptTemp.Y) );
                    }


                    dResErrX += (ptCur.X-ptTemp.X)*(ptCur.X-ptTemp.X);
                    dResErrY += (ptCur.Y-ptTemp.Y)*(ptCur.Y-ptTemp.Y);
                }
            }
            nIndexCur += vecPt2dCur.size();

        }
        dTotalErr = dResErrX + dResErrY;

        dResErrX /= nPt2dNum;
        dResErrY /= nPt2dNum;
        dResErrX = sqrt( dResErrX );
        dResErrY = sqrt( dResErrY );


        if( false == bIsUseSparseMat )
        {
            if( false == mlMatSolveSVD( &matA, &matL, &matX ) )
            {
                return false;
            }

            for( UINT h = 0; h < (nImgNum); ++h )
            {
                if( false == vecbImgIsFixed[h])
                {
                    UINT nCurIndex = vecNewImgIndex[h];
                    DOUBLE dX = matX.GetAt( 6*nCurIndex, 0 );
                    DOUBLE dY = matX.GetAt( 6*nCurIndex+1, 0 );
                    DOUBLE dZ = matX.GetAt( 6*nCurIndex+2, 0 );

                    DOUBLE dOmg = matX.GetAt( 6*nCurIndex+3, 0 );
                    DOUBLE dPhi = matX.GetAt( 6*nCurIndex+4, 0 );
                    DOUBLE dKap = matX.GetAt( 6*nCurIndex+5, 0 );

                    ExOriPara* pExOri = &vecImgExOris[h];
                    pExOri->pos.X += matX.GetAt( 6*nCurIndex, 0 );
                    pExOri->pos.Y += matX.GetAt( 6*nCurIndex+1, 0 );
                    pExOri->pos.Z += matX.GetAt( 6*nCurIndex+2, 0 );

                    pExOri->ori.omg += matX.GetAt( 6*nCurIndex+3, 0 );
                    pExOri->ori.phi += matX.GetAt( 6*nCurIndex+4, 0 );
                    pExOri->ori.kap += matX.GetAt( 6*nCurIndex+5, 0 );
                }
            }
            for( UINT h = 0; h < nPt3dNum; ++h )
            {
                if( false == vecb3dPtIsFixed[h])
                {
                    UINT nCurIdx = vecNew3dPtIndex[h];
                    Pt3d* pt3dCur = &vec3dPts[h];
                    pt3dCur->X += matX.GetAt( nPt3dIndex+3*nCurIdx, 0 );
                    pt3dCur->Y += matX.GetAt( nPt3dIndex+3*nCurIdx+1, 0 );
                    pt3dCur->Z += matX.GetAt( nPt3dIndex+3*nCurIdx+2, 0 );
                }

            }
        }
        else
        {
            if( false == clsSparMat.QRSolve( vecL, vecX ) )
            {
                return false;
            }

            for( UINT h = 0; h < (nImgNum); ++h )
            {
                if( false == vecbImgIsFixed[h])
                {
                    UINT nCurIndex = vecNewImgIndex[h];
                    DOUBLE dX = vecX.at( 6*nCurIndex );
                    DOUBLE dY = vecX.at( 6*nCurIndex+1 );
                    DOUBLE dZ = vecX.at( 6*nCurIndex+2 );

                    DOUBLE dOmg = vecX.at( 6*nCurIndex+3 );
                    DOUBLE dPhi = vecX.at( 6*nCurIndex+4 );
                    DOUBLE dKap = vecX.at( 6*nCurIndex+5 );

                    ExOriPara* pExOri = &vecImgExOris[h];
                    pExOri->pos.X += dX;
                    pExOri->pos.Y += dY;
                    pExOri->pos.Z += dZ;

                    pExOri->ori.omg += dOmg;
                    pExOri->ori.phi += dPhi;
                    pExOri->ori.kap += dKap;
                }
            }
            for( UINT h = 0; h < nPt3dNum; ++h )
            {
                if( false == vecb3dPtIsFixed[h])
                {
                    UINT nCurIdx = vecNew3dPtIndex[h];
                    Pt3d* pt3dCur = &vec3dPts[h];
                    pt3dCur->X += vecX.at( nPt3dIndex+3*nCurIdx );
                    pt3dCur->Y += vecX.at( nPt3dIndex+3*nCurIdx+1 );
                    pt3dCur->Z += vecX.at( nPt3dIndex+3*nCurIdx+2 );
                }

            }
        }

        nIterator++;
    }
    while( nIterator < 4 );

//    for( UINT i = 0; i < vecImgExOris.size(); i=i+2 )
//    {
//        ExOriPara exOriL, exOriR, exOriRela;
//        CmlPhgProc clsPhg;
//        exOriL = vecImgExOris[i];
//        exOriR = vecImgExOris[i+1];
//        clsPhg.GetRelaOri( &exOriL, &exOriR, &exOriRela  );
//        int kk = 1;
//    }

    //------------------------------------------------------
    if( false == bIsUseSparseMat )
    {
        CmlMat matQQ;

        mlMatMulTransInv( &matA, &matQQ );//计算(AtA)-1

        if( ( matA.GetH()-matA.GetW() ) > 0 )
        {
            DOUBLE dUnitErr = sqrt( dTotalErr/(matA.GetH()-matA.GetW()) );
            UINT nTmpIdx = 0;
            for( UINT i = 0; i < vecImgExOris.size(); ++i )
            {
                ExOriPara exOriCurErr;
                if( false == vecbImgIsFixed[i] )
                {
                    exOriCurErr.pos.X = sqrt( matQQ.GetAt( 6*nTmpIdx, 6*nTmpIdx ) ) * dUnitErr;
                    exOriCurErr.pos.Y = sqrt( matQQ.GetAt( 6*nTmpIdx+1, 6*nTmpIdx+1 ) ) * dUnitErr;
                    exOriCurErr.pos.Z = sqrt( matQQ.GetAt( 6*nTmpIdx+2, 6*nTmpIdx+2 ) ) * dUnitErr;

                    exOriCurErr.ori.omg = sqrt( matQQ.GetAt( 6*nTmpIdx+3, 6*nTmpIdx+3 ) ) * dUnitErr;
                    exOriCurErr.ori.phi = sqrt( matQQ.GetAt( 6*nTmpIdx+4, 6*nTmpIdx+4 ) ) * dUnitErr;
                    exOriCurErr.ori.kap = sqrt( matQQ.GetAt( 6*nTmpIdx+5, 6*nTmpIdx+5 ) ) * dUnitErr;
                    nTmpIdx++;
                }
                vecImgResErr.push_back( exOriCurErr );
            }
            UINT nTmp3dIdx = 6*nTmpIdx;
            UINT nTmpCurIdx = 0;
            for( UINT i = 0; i < vec3dPts.size(); ++i )
            {
                Pt3d ptCurErr;
                if( false == vecb3dPtIsFixed[i] )
                {
                    ptCurErr.X = sqrt( matQQ.GetAt( nTmp3dIdx+3*nTmpCurIdx, nTmp3dIdx+3*nTmpCurIdx) ) * dUnitErr;
                    ptCurErr.Y = sqrt( matQQ.GetAt( nTmp3dIdx+3*nTmpCurIdx+1, nTmp3dIdx+3*nTmpCurIdx+1) ) * dUnitErr;
                    ptCurErr.Z = sqrt( matQQ.GetAt( nTmp3dIdx+3*nTmpCurIdx+2, nTmp3dIdx+3*nTmpCurIdx+2) ) * dUnitErr;
                    nTmpCurIdx++;
                }
                vec3dPtsResErr.push_back( ptCurErr );
            }
            ptTotalImgResErr.X = dResErrX;
            ptTotalImgResErr.Y = dResErrY;
            return true;
        }
        else
        {
            return false;
        }
    }

}

bool CmlBA::BA_Common(vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts )
{
    vector<ExOriPara> vecImgResErr;
    vector<Pt3d> vec3dPtsResErr;
    Pt2d ptTotalImgResErr;

    return this->BA_Common( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtIsFixed, vecImgExOris, vec3dPts, vecImgResErr, vec3dPtsResErr, ptTotalImgResErr );
}
//bool CmlBA::BA_Common1(vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts)
//{
//    if( ( vecImgExOris.size() == 0 )||( vecImgInOris.size() == 0 )||( vecImgPts.size() == 0 ) ||( vec3dPts.size() == 0 ) )
//    {
//        return false;
//    }
//    UINT nImgNum = vecImgInOris.size();
//    if( ( nImgNum != vecImgPts.size() ) || ( nImgNum != vecImgExOris.size() ) )
//    {
//        return false;
//    }
//    map<ULONG, UINT> mapIndex3dPts;
//    for( UINT i = 0; i < vec3dPts.size(); ++i )
//    {
//        mapIndex3dPts.insert( map<ULONG, UINT>::value_type( vec3dPts[i].lID, i ));
//    }
//    UINT nPt2dNum = 0;
//    UINT nPt3dNum = vec3dPts.size();
//
//    for( UINT i = 0; i < vecImgInOris.size(); ++i )
//    {
//        nPt2dNum += vecImgPts[i].size();
//    }
//
//    nImgNum -= 2;
//
//    CmlMat matA, matX, matL;
//    UINT nRow = 2*nPt2dNum;
//    UINT nCol = 6*(nImgNum)+3*nPt3dNum;
//
//    CmlSparseMat clsSparMat;
//    vector<DOUBLE> vecL, vecX;
//    bool bIsUseSparseMat = false;
//
//    if( false == bIsUseSparseMat )
//    {
//        if( ( false == matA.Initial( nRow, nCol ))|| (false == matX.Initial( nCol, 1 ) ) || ( false == matL.Initial( nRow, 1 ) ) )
//        {
//            return false;
//        }
//    }
//    else
//    {
//        if( false == clsSparMat.Initial( nRow, nCol ) )
//        {
//            return false;
//        }
//    }
//
//
////    for( UINT i = 0; i < vecImgExOris.size(); i=i+2 )
////    {
////        ExOriPara exOriL = vecImgExOris[i];
////        ExOriPara exOriR = vecImgExOris[i+1];
////        ExOriPara exOriRela;
////        CmlPhgProc clsPhg;
////        clsPhg.GetRelaOri( &exOriL, &exOriR, &exOriRela );
////    }
//
//    UINT nIndexCur = 0;
//    UINT nPt3dIndex = 6*(nImgNum);
//    UINT nIterator = 0;
//    CmlFrameImage clsFrm;
//    UINT nLoopIndex = 0;
//    do
//    {
//        nLoopIndex++;
//        DOUBLE dResErrX, dResErrY;
//        dResErrX = dResErrY = 0;
//        nIndexCur = 0;
//
//        if( false == bIsUseSparseMat )
//        {
//            for( UINT i = 0; i < nRow; ++i )
//            {
//                matL.SetAt( i, UINT(0), 0 );
//                for( UINT j = 0; j < nCol; ++j )
//                {
//                    matA.SetAt( i, j, 0 );
//                }
//            }
//            for( UINT i = 0; i < nCol; ++i )
//            {
//                matX.SetAt( i, UINT(0), 0 );
//            }
//        }
//
//
//        for( UINT i = 0; i < vecImgInOris.size(); ++i )
//        {
//            InOriPara inOriCur = vecImgInOris[i];
//            ExOriPara exOriCur = vecImgExOris[i];
//            vector<Pt2d> &vecPt2dCur = vecImgPts[i];
////            if( (nLoopIndex <= 1 )&&( i >= 2 ) )
////            {
////                CmlPhgProc clsPhg;
////          //      clsPhg.mlBackForwardinterSection( vecPt2dCur, vec3dPts, inOriCur.f, &exOriCur, &exOriCur );
////            }
//
//            for( UINT j = 0; j < vecPt2dCur.size(); ++j )
//            {
//                Pt2d ptCur = vecPt2dCur[j];
//                map<ULONG, UINT>::iterator it = mapIndex3dPts.find( ptCur.lID );
//                DOUBLE dCoefA[12], dCoefB[6];
//
//                if( it != mapIndex3dPts.end() )
//                {
//
//                    UINT nId = it->second;
//                    Pt3d ptTempXYZ = vec3dPts[nId];
//                    getCoefA_A( ptCur, &exOriCur, inOriCur.f, inOriCur.f, ptTempXYZ, dCoefA );
//                    if( i >= 2 )
//                    {
//                        for( UINT k = 0; k < 12; ++k )
//                        {
//                            UINT nTRow = (2*(nIndexCur+j)+k/6);
//                            UINT nTCol = 6*(i-2)+(k%6);
//                            if( false == bIsUseSparseMat )
//                            {
//                                matA.SetAt( nTRow, nTCol, dCoefA[k] );
//                            }
//                            else
//                            {
//                                clsSparMat.SetAt(  nTRow, nTCol, dCoefA[k] );
//                            }
//
//                        }
//                    }
//
//                    getCoefA_B( ptCur, &exOriCur, inOriCur.f, inOriCur.f, ptTempXYZ, dCoefB  );
//                    for( UINT k = 0; k < 6; ++k )
//                    {
//                        UINT nTRow = (2*(nIndexCur+j)+k/3);
//                        UINT nTCol = nPt3dIndex+3*nId+(k%3);
//
//                        if( false == bIsUseSparseMat )
//                        {
//                            matA.SetAt( 2*(nIndexCur+j)+k/3, nPt3dIndex+3*nId+(k%3), dCoefB[k]  );
//                        }
//                        else
//                        {
//                            clsSparMat.SetAt( 2*(nIndexCur+j)+k/3, nPt3dIndex+3*nId+(k%3), dCoefB[k] );
//                        }
//                    }
//                    Pt2d ptTemp;
//                    CmlMat matR;
//                    OPK2RMat( &exOriCur.ori, &matR );
//                    getxyFromXYZ( ptTemp, ptTempXYZ, exOriCur.pos, matR, inOriCur.f, inOriCur.f );
//
//                    if( false == bIsUseSparseMat )
//                    {
//                        matL.SetAt( 2*(nIndexCur+j), 0, (ptCur.X - ptTemp.X) );
//                        matL.SetAt( 2*(nIndexCur+j)+1, 0, (ptCur.Y - ptTemp.Y) );
//                    }
//                    else
//                    {
//                        vecL.push_back( (ptCur.X - ptTemp.X) );
//                        vecL.push_back( (ptCur.Y - ptTemp.Y) );
//                    }
//                    dResErrX += (ptCur.X-ptTemp.X)*(ptCur.X-ptTemp.X);
//                    dResErrY += (ptCur.Y-ptTemp.Y)*(ptCur.Y-ptTemp.Y);
//                }
//            }
//            nIndexCur += vecPt2dCur.size();
//
//
//        }
//        dResErrX /= nPt2dNum;
//        dResErrY /= nPt2dNum;
//        dResErrX = sqrt( dResErrX );
//        dResErrY = sqrt( dResErrY );
//
//        if( false == bIsUseSparseMat )
//        {
//            if( false == mlMatSolveSVD( &matA, &matL, &matX ) )
//            {
//                return false;
//            }
//
//            for( UINT h = 0; h < (nImgNum); ++h )
//            {
//                DOUBLE dX = matX.GetAt( 6*h, 0 );
//                DOUBLE dY = matX.GetAt( 6*h+1, 0 );
//                DOUBLE dZ = matX.GetAt( 6*h+2, 0 );
//
//                DOUBLE dOmg = matX.GetAt( 6*h+3, 0 );
//                DOUBLE dPhi = matX.GetAt( 6*h+4, 0 );
//                DOUBLE dKap = matX.GetAt( 6*h+5, 0 );
//
//                ExOriPara* pExOri = &vecImgExOris[h+2];
//                pExOri->pos.X += matX.GetAt( 6*h, 0 );
//                pExOri->pos.Y += matX.GetAt( 6*h+1, 0 );
//                pExOri->pos.Z += matX.GetAt( 6*h+2, 0 );
//
//                pExOri->ori.omg += matX.GetAt( 6*h+3, 0 );
//                pExOri->ori.phi += matX.GetAt( 6*h+4, 0 );
//                pExOri->ori.kap += matX.GetAt( 6*h+5, 0 );
//
//            }
//            for( UINT h = 0; h < nPt3dNum; ++h )
//            {
//                Pt3d* pt3dCur = &vec3dPts[h];
//                pt3dCur->X += matX.GetAt( nPt3dIndex+3*h, 0 );
//                pt3dCur->Y += matX.GetAt( nPt3dIndex+3*h+1, 0 );
//                pt3dCur->Z += matX.GetAt( nPt3dIndex+3*h+2, 0 );
//            }
//        }
//        else
//        {
//            if( false == clsSparMat.QRSolve( vecL, vecX ) )
//            {
//                return false;
//            }
//
//            for( UINT h = 0; h < (nImgNum); ++h )
//            {
//                DOUBLE dX = vecX.at( 6*h );
//                DOUBLE dY = vecX.at( 6*h+1 );
//                DOUBLE dZ = vecX.at( 6*h+2 );
//
//                DOUBLE dOmg = vecX.at( 6*h+3 );
//                DOUBLE dPhi = vecX.at( 6*h+4 );
//                DOUBLE dKap = vecX.at( 6*h+5 );
//
//                ExOriPara* pExOri = &vecImgExOris[h+2];
//                pExOri->pos.X += dX;
//                pExOri->pos.Y += dY;
//                pExOri->pos.Z += dZ;
//
//                pExOri->ori.omg += dOmg;
//                pExOri->ori.phi += dPhi;
//                pExOri->ori.kap += dKap;
//
//            }
//            for( UINT h = 0; h < nPt3dNum; ++h )
//            {
//                Pt3d* pt3dCur = &vec3dPts[h];
//                pt3dCur->X += vecX.at( nPt3dIndex+3*h );
//                pt3dCur->Y += vecX.at( nPt3dIndex+3*h+1 );
//                pt3dCur->Z += vecX.at( nPt3dIndex+3*h+2 );
//            }
//        }
//        nIterator++;
//    }
//    while( nIterator < 8 );
//
//    for( UINT i = 0; i < vecImgExOris.size(); i=i+2 )
//    {
//        ExOriPara exOriL = vecImgExOris[i];
//        ExOriPara exOriR = vecImgExOris[i+1];
//        ExOriPara exOriRela;
//        CmlPhgProc clsPhg;
//        clsPhg.GetRelaOri( &exOriL, &exOriR, &exOriRela );
//    }
//
//    return true;
//}
bool CmlBA::BA_WithEpiImg1N1( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts )
{
    if( ( vecImgExOris.size() == 0 )||( vecImgInOris.size() == 0 )||( vecImgPts.size() == 0 ) ||( vec3dPts.size() == 0 ) )
    {
        return false;
    }
    UINT nImgNum = vecImgInOris.size();
    if( ( nImgNum != vecImgPts.size() ) || ( nImgNum != vecImgExOris.size() ) )
    {
        return false;
    }
    map<ULONG, UINT> mapIndex3dPts;
    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        mapIndex3dPts.insert( map<ULONG, UINT>::value_type( vec3dPts[i].lID, i ));
    }
    UINT nPt2dNum = 0;
    UINT nPt3dNum = vec3dPts.size();

    for( UINT i = 0; i < vecImgInOris.size(); ++i )
    {
        nPt2dNum += vecImgPts[i].size();
    }
    CmlMat matA, matX, matL;
    UINT nRow = 2*nPt2dNum;
    UINT nCol = 6*(nImgNum/2)+3*nPt3dNum;

    bool bIsUseSparseMat = false;
    CmlSparseMat clsSparMat;

    if( false == bIsUseSparseMat )
    {
        if( ( false == matA.Initial( nRow, nCol ))|| (false == matX.Initial( nCol, 1 ) ) || ( false == matL.Initial( nRow, 1 ) ) )
        {
            return false;
        }
        for( UINT i = 0; i < nRow; ++i )
        {
            matL.SetAt( i, UINT(0), 0 );
            for( UINT j = 0; j < nCol; ++j )
            {
                matA.SetAt( i, j, 0 );
            }
        }
        for( UINT i = 0; i < nCol; ++i )
        {
            matX.SetAt( i, UINT(0), 0 );
        }
    }
    else
    {
        if( false == clsSparMat.Initial( nRow, nCol ) )
        {
            return false;
        }
    }



    UINT nIndexCur = 0;
    UINT nPt3dIndex = 6*(nImgNum/2);
    UINT nIterator = 0;


    do
    {
        for( UINT i = 0; i < vecImgInOris.size(); ++i )
        {
            InOriPara inOriCur = vecImgInOris[i];
            ExOriPara exOriCur = vecImgExOris[i];
            DOUBLE dBaseLength = 0;
            ExOriPara exOriPrev;
            if( i%2 != 0 )
            {
                exOriPrev = vecImgExOris[i-1];
                DOUBLE dTmpX = (exOriPrev.pos.X - exOriCur.pos.X);
                DOUBLE dTmpY = (exOriPrev.pos.Y - exOriCur.pos.Y);
                DOUBLE dTmpZ = (exOriPrev.pos.Z - exOriCur.pos.Z);

                dBaseLength = sqrt( dTmpX*dTmpX+dTmpY*dTmpY+dTmpZ*dTmpZ);
            }

            vector<Pt2d> &vecPt2dCur = vecImgPts[i];
            for( UINT j = 0; j < vecPt2dCur.size(); ++j )
            {
                Pt2d ptCur = vecPt2dCur[j];
                map<ULONG, UINT>::iterator it = mapIndex3dPts.find( ptCur.lID );
                DOUBLE dCoefA[12], dCoefB[6];

                if( it != mapIndex3dPts.end() )
                {
                    if( i%2 == 0 )//×óÓ°Ïñ
                    {
                        getCoefA_A( ptCur, &exOriCur, inOriCur.f, inOriCur.f, vec3dPts[it->second], dCoefA );
                        getCoefA_B( ptCur, &exOriCur, inOriCur.f, inOriCur.f, vec3dPts[it->second], dCoefB  );
                    }
                    else
                    {
                        getCoefA_A_EpiR( ptCur, &exOriPrev, dBaseLength, inOriCur.f, inOriCur.f, vec3dPts[it->second], dCoefA );
                        getCoefA_B_EpiR( ptCur, &exOriPrev, dBaseLength, inOriCur.f, inOriCur.f, vec3dPts[it->second], dCoefB );
                    }

                    for( UINT k = 0; k < 12; ++k )
                    {
                        if( false == bIsUseSparseMat )
                        {
                            matA.SetAt( (2*(nIndexCur+j)+k/6), 6*(i/2)+(k%6), dCoefA[k] );
                        }
                        else
                        {
                            clsSparMat.SetAt( (2*(nIndexCur+j)+k/6), 6*(i/2)+(k%6), dCoefA[k] );
                        }

                    }



                    for( UINT k = 0; k < 6; ++k )
                    {
                        if( false == bIsUseSparseMat )
                        {
                            matA.SetAt( 2*(nIndexCur+j)+k/3, nPt3dIndex+3*it->second+(k%3), dCoefB[k]  );
                        }
                        else
                        {
                            clsSparMat.SetAt( 2*(nIndexCur+j)+k/3, nPt3dIndex+3*it->second+(k%3), dCoefB[k] );
                        }
                    }
                    Pt2d ptTemp;
                    CmlMat matR;
                    OPK2RMat( &exOriCur.ori, &matR );
                    if( i%2 == 0 )
                    {
                        getxyFromXYZ( ptTemp, vec3dPts[it->second], exOriCur.pos, matR, inOriCur.f, inOriCur.f );
                    }
                    else
                    {
                        getxyFromXYZ( ptTemp, vec3dPts[it->second], exOriPrev.pos, dBaseLength, matR, inOriCur.f, inOriCur.f );
                    }

                    matL.SetAt( 2*i, 0, ptTemp.X );
                    matL.SetAt( 2*i+1, 0, ptTemp.Y );

                }
            }
            nIndexCur += vecPt2dCur.size();
        }

        if( false == mlMatSolveSVD( &matA, &matL, &matX ) )
        {
            return false;
        }

        for( UINT h = 0; h < (nImgNum/2); ++h )
        {
            ExOriPara* pExOriL = &vecImgExOris[2*h];
            pExOriL->pos.X += matX.GetAt( 6*h, 0 );
            pExOriL->pos.Y += matX.GetAt( 6*h+1, 0 );
            pExOriL->pos.Z += matX.GetAt( 6*h+2, 0 );

            pExOriL->ori.omg += matX.GetAt( 6*h+3, 0 );
            pExOriL->ori.phi += matX.GetAt( 6*h+4, 0 );
            pExOriL->ori.kap += matX.GetAt( 6*h+5, 0 );

            ExOriPara* pExOriR = &vecImgExOris[2*h+1];
            pExOriR->pos.X += matX.GetAt( 6*h, 0 );
            pExOriR->pos.Y += matX.GetAt( 6*h+1, 0 );
            pExOriR->pos.Z += matX.GetAt( 6*h+2, 0 );

        }
        for( UINT h = 0; h < nPt3dNum; ++h )
        {
            Pt3d* pt3dCur = &vec3dPts[h];
            pt3dCur->X += matX.GetAt( nPt3dIndex+3*h, 0 );
            pt3dCur->Y += matX.GetAt( nPt3dIndex+3*h+1, 0 );
            pt3dCur->Z += matX.GetAt( nPt3dIndex+3*h+2, 0 );
        }
//        if(  )
//        {
//
//        }
        nIterator++;
    }
    while( nIterator < 4 );



    return true;
}
bool CmlBA::BA_WithEpiImg1N1( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts, \
                                   vector<ExOriPara> &vecImgResErr, vector<Pt3d> &vec3dPtsResErr, Pt2d &ptTotalImgResErr )
{
    if( ( vecbImgIsFixed.size()== 0 )||( vecb3dPtIsFixed.size()== 0 )||( vecImgExOris.size() == 0 )||( vecImgInOris.size() == 0 )||( vecImgPts.size() == 0 ) ||( vec3dPts.size() == 0 ) )
    {
        return false;
    }
    UINT nImgNum = vecImgInOris.size();
    if( ( vec3dPts.size() != vecb3dPtIsFixed.size() )||( nImgNum != vecbImgIsFixed.size() )||( nImgNum != vecImgPts.size() ) || ( nImgNum != vecImgExOris.size() ) )
    {
        return false;
    }
    map<ULONG, UINT> mapIndex3dPts;
    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        mapIndex3dPts.insert( map<ULONG, UINT>::value_type( vec3dPts[i].lID, i ));
    }
    UINT nPt2dNum = 0;
    UINT nPt3dNum = vec3dPts.size();

    for( UINT i = 0; i < vecImgInOris.size(); ++i )
    {
        nPt2dNum += vecImgPts[i].size();
    }
    CmlMat matA, matX, matL;


    UINT nFixed3dPtsCount = 0;
    for( UINT i = 0; i < vecb3dPtIsFixed.size(); ++i )
    {
        if( true == vecb3dPtIsFixed[i] )
        {
            ++nFixed3dPtsCount;
        }
    }
    UINT nFixedImgCount = 0;
    for( UINT i = 0; i < vecbImgIsFixed.size(); ++i )
    {
        if( true == vecbImgIsFixed[i] )
        {
            ++nFixedImgCount;
        }
    }
    UINT nRow = 2*nPt2dNum;
    UINT nCol = 6*((nImgNum-nFixedImgCount)/2)+3*(nPt3dNum-nFixed3dPtsCount);

    UINT nTotal = nRow * nCol;
    bool bIsUseSparseMat = false;
    if( nTotal >= MAX_DENSE_MAT_ELE_NUM )
    {
        bIsUseSparseMat = true;
    }

    CmlSparseMat clsSparMat;
    vector<DOUBLE> vecL, vecX;

    if( false == bIsUseSparseMat )
    {
        if( ( false == matA.Initial( nRow, nCol ))|| (false == matX.Initial( nCol, 1 ) ) || ( false == matL.Initial( nRow, 1 ) ) )
        {
            return false;
        }
    }
    else
    {
        if( false == clsSparMat.Initial( nRow, nCol ) )
        {
            return false;
        }
    }

    //-----------------------------------------------
    vector<UINT> vecNewImgIndex, vecNew3dPtIndex;
    UINT nTmpIndex = -1;
    for( UINT i = 0; i < vecImgExOris.size(); ++i )
    {
        if( false == vecbImgIsFixed[i])
        {
            nTmpIndex++;
        }
        vecNewImgIndex.push_back( nTmpIndex );
    }
    nTmpIndex = -1;
    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        if( false == vecb3dPtIsFixed[i])
        {
            nTmpIndex++;
        }
        vecNew3dPtIndex.push_back( nTmpIndex );
    }
    //-----------------------------------------------

    UINT nIndexCur = 0;
    UINT nPt3dIndex = 6*( (nImgNum-nFixedImgCount)/2);
    UINT nIterator = 0;

    DOUBLE dResErrX, dResErrY, dTotalErr;
    do
    {
        dResErrX = dResErrY = 0;
        nIndexCur = 0;
        dTotalErr = 0;

        if( false == bIsUseSparseMat )
        {
            for( UINT i = 0; i < nRow; ++i )
            {
                matL.SetAt( i, UINT(0), 0 );
                for( UINT j = 0; j < nCol; ++j )
                {
                    matA.SetAt( i, j, 0 );
                }
            }
            for( UINT i = 0; i < nCol; ++i )
            {
                matX.SetAt( i, UINT(0), 0 );
            }
        }
        else
        {
            vecL.clear();
            vecX.clear();
        }

        vector<DOUBLE > vecdBaseLine;
        for( UINT i = 0; i < vecImgInOris.size(); ++i )
        {
            InOriPara inOriCur = vecImgInOris[i];
            ExOriPara exOriCur = vecImgExOris[i];

            DOUBLE dBaseLength = 0;
            ExOriPara exOriPrev;
            InOriPara inOriPrev;
            if( i%2 != 0 )
            {
                exOriPrev = vecImgExOris[i-1];
                inOriPrev = vecImgInOris[i-1];
                DOUBLE dTmpX = (exOriPrev.pos.X - exOriCur.pos.X);
                DOUBLE dTmpY = (exOriPrev.pos.Y - exOriCur.pos.Y);
                DOUBLE dTmpZ = (exOriPrev.pos.Z - exOriCur.pos.Z);

                dBaseLength = sqrt( dTmpX*dTmpX+dTmpY*dTmpY+dTmpZ*dTmpZ);
                vecdBaseLine.push_back( dBaseLength );
            }

            vector<Pt2d> &vecPt2dCur = vecImgPts[i];
            for( UINT j = 0; j < vecPt2dCur.size(); ++j )
            {
                Pt2d ptCur = vecPt2dCur[j];
                map<ULONG, UINT>::iterator it = mapIndex3dPts.find( ptCur.lID );
                DOUBLE dCoefA[12], dCoefB[6];

                if( it != mapIndex3dPts.end() )
                {
                    UINT nId = it->second;
                    Pt3d ptTempXYZ = vec3dPts[nId];

                    if( vecbImgIsFixed[i] == false )
                    {
                        if( i%2 == 0 )//×óÓ°Ïñ
                        {
                            getCoefA_A( ptCur, &exOriCur, inOriCur.f, inOriCur.f, ptTempXYZ, dCoefA );
                        }
                        else
                        {
                            getCoefA_A_EpiR( ptCur, &exOriPrev, dBaseLength, inOriPrev.f, inOriPrev.f, ptTempXYZ, dCoefA );

                        }
                        for( UINT k = 0; k < 12; ++k )
                        {
                            if( false == bIsUseSparseMat )
                            {
                                matA.SetAt( (2*(nIndexCur+j)+k/6), 6*(vecNewImgIndex[i]/2)+(k%6), dCoefA[k] );
                            }
                            else
                            {
                                clsSparMat.SetAt(  (2*(nIndexCur+j)+k/6), 6*(vecNewImgIndex[i]/2)+(k%6), dCoefA[k] );
                            }
                        }
                    }

                    if( vecb3dPtIsFixed[nId] == false )
                    {
                        if( i%2 ==0 )
                        {
                            getCoefA_B( ptCur, &exOriCur, inOriCur.f, inOriCur.f, ptTempXYZ, dCoefB  );
                        }
                        else
                        {
                            getCoefA_B_EpiR( ptCur, &exOriPrev, dBaseLength, inOriPrev.f, inOriPrev.f, ptTempXYZ, dCoefB );
                        }
                        for( UINT k = 0; k < 6; ++k )
                        {
                            if( false == bIsUseSparseMat )
                            {
                                matA.SetAt( 2*(nIndexCur+j)+k/3, nPt3dIndex+3*vecNew3dPtIndex[nId]+(k%3), dCoefB[k]  );
                            }
                            else
                            {
                                clsSparMat.SetAt( 2*(nIndexCur+j)+k/3, nPt3dIndex+3*vecNew3dPtIndex[nId]+(k%3), dCoefB[k] );
                            }
                        }
                    }

                    Pt2d ptTemp;
                    CmlMat matR;
                    OPK2RMat( &exOriCur.ori, &matR );
                    getxyFromXYZ( ptTemp, ptTempXYZ, exOriCur.pos, matR, inOriCur.f, inOriCur.f );

                    if( false == bIsUseSparseMat )
                    {
                        matL.SetAt( 2*(nIndexCur+j), 0, (ptCur.X - ptTemp.X) );
                        matL.SetAt( 2*(nIndexCur+j)+1, 0, (ptCur.Y - ptTemp.Y) );
                    }
                    else
                    {
                        vecL.push_back( (ptCur.X - ptTemp.X ) );
                        vecL.push_back( (ptCur.Y - ptTemp.Y ) );
                    }


                    dResErrX += (ptCur.X-ptTemp.X)*(ptCur.X-ptTemp.X);
                    dResErrY += (ptCur.Y-ptTemp.Y)*(ptCur.Y-ptTemp.Y);
                }
            }
            nIndexCur += vecPt2dCur.size();

        }
        dTotalErr = dResErrX + dResErrY;

        dResErrX /= nPt2dNum;
        dResErrY /= nPt2dNum;
        dResErrX = sqrt( dResErrX );
        dResErrY = sqrt( dResErrY );


        if( false == bIsUseSparseMat )
        {
            if( false == mlMatSolveSVD( &matA, &matL, &matX ) )
            {
                return false;
            }

            CmlPhgProc clsPhg;

            DOUBLE dX, dY, dZ, dOmg, dPhi, dKap;
            dX = dY = dZ = dOmg = dPhi = dKap = 0;
            for( UINT h = 0; h < (nImgNum); ++h )
            {
                if( false == vecbImgIsFixed[h])
                {
                    if( h%2 ==0 )
                    {
                        UINT nCurIndex = vecNewImgIndex[h];
                        nCurIndex /= 2;
                        dX = matX.GetAt( 6*nCurIndex, 0 );
                        dY = matX.GetAt( 6*nCurIndex+1, 0 );
                        dZ = matX.GetAt( 6*nCurIndex+2, 0 );

                        dOmg = matX.GetAt( 6*nCurIndex+3, 0 );
                        dPhi = matX.GetAt( 6*nCurIndex+4, 0 );
                        dKap = matX.GetAt( 6*nCurIndex+5, 0 );

                        ExOriPara* pExOri = &vecImgExOris[h];
                        pExOri->pos.X += dX;
                        pExOri->pos.Y += dY;
                        pExOri->pos.Z += dZ;

                        pExOri->ori.omg += dOmg;
                        pExOri->ori.phi += dPhi;
                        pExOri->ori.kap += dKap;
                    }
                    else
                    {
                        ExOriPara* pExOriPrev = &vecImgExOris[h-1];
                        ExOriPara* pExOriCur = &vecImgExOris[h];
                        ExOriPara exOriRela;
                        exOriRela.pos.X = vecdBaseLine[h/2];

                        clsPhg.ExOriTrans( pExOriPrev, &exOriRela, pExOriCur );

                    }

                }
            }
            for( UINT h = 0; h < nPt3dNum; ++h )
            {
                if( false == vecb3dPtIsFixed[h])
                {
                    UINT nCurIdx = vecNew3dPtIndex[h];
                    Pt3d* pt3dCur = &vec3dPts[h];
                    pt3dCur->X += matX.GetAt( nPt3dIndex+3*nCurIdx, 0 );
                    pt3dCur->Y += matX.GetAt( nPt3dIndex+3*nCurIdx+1, 0 );
                    pt3dCur->Z += matX.GetAt( nPt3dIndex+3*nCurIdx+2, 0 );
                }

            }
        }
        else
        {
            if( false == clsSparMat.QRSolve( vecL, vecX ) )
            {
                return false;
            }

            CmlPhgProc clsPhg;

            DOUBLE dX, dY, dZ, dOmg, dPhi, dKap;
            dX = dY = dZ = dOmg = dPhi = dKap = 0;
            for( UINT h = 0; h < (nImgNum); ++h )
            {
                if( false == vecbImgIsFixed[h])
                {
                    if( h%2 ==0 )
                    {
                        UINT nCurIndex = vecNewImgIndex[h];
                        nCurIndex /= 2;
                        dX = vecX.at( 6*nCurIndex );
                        dY = vecX.at( 6*nCurIndex+1 );
                        dZ = vecX.at( 6*nCurIndex+2 );

                        dOmg = vecX.at( 6*nCurIndex+3 );
                        dPhi = vecX.at( 6*nCurIndex+4 );
                        dKap = vecX.at( 6*nCurIndex+5 );

                        ExOriPara* pExOri = &vecImgExOris[h];
                        pExOri->pos.X += dX;
                        pExOri->pos.Y += dY;
                        pExOri->pos.Z += dZ;

                        pExOri->ori.omg += dOmg;
                        pExOri->ori.phi += dPhi;
                        pExOri->ori.kap += dKap;
                    }
                    else
                    {
                        ExOriPara* pExOriPrev = &vecImgExOris[h-1];
                        ExOriPara* pExOriCur = &vecImgExOris[h];
                        ExOriPara exOriRela;
                        exOriRela.pos.X = vecdBaseLine[h/2];

                        clsPhg.ExOriTrans( pExOriPrev, &exOriRela, pExOriCur );

                    }

                }
            }
            for( UINT h = 0; h < nPt3dNum; ++h )
            {
                if( false == vecb3dPtIsFixed[h])
                {
                    UINT nCurIdx = vecNew3dPtIndex[h];
                    Pt3d* pt3dCur = &vec3dPts[h];
                    pt3dCur->X += vecX.at( nPt3dIndex+3*nCurIdx );
                    pt3dCur->Y += vecX.at( nPt3dIndex+3*nCurIdx+1 );
                    pt3dCur->Z += vecX.at( nPt3dIndex+3*nCurIdx+2 );
                }

            }
        }


        nIterator++;
    }
    while( nIterator < 5 );


    //----------------------------------------------------
    for( UINT i = 0; i < vecImgExOris.size(); i=i+2 )
    {
        ExOriPara exOriL, exOriR, exOriRela;
        CmlPhgProc clsPhg;
        exOriL = vecImgExOris[i];
        exOriR = vecImgExOris[i+1];
        clsPhg.GetRelaOri( &exOriL, &exOriR, &exOriRela  );
        int kk = 1;
    }

    //------------------------------------------------------
    if( false == bIsUseSparseMat )
    {
        CmlMat matQQ;

        mlMatMulTransInv( &matA, &matQQ );//计算(AtA)-1

        if( ( matA.GetH()-matA.GetW() ) > 0 )
        {
            DOUBLE dUnitErr = sqrt( dTotalErr/(matA.GetH()-matA.GetW()) );
            UINT nTmpIdx = 0;
            for( UINT i = 0; i < vecImgExOris.size(); ++i )
            {
                ExOriPara exOriCurErr;
                if( false == vecbImgIsFixed[i] )
                {
                    UINT nTTIdx = nTmpIdx;
                    nTTIdx = nTmpIdx / 2;

                    exOriCurErr.pos.X = sqrt( matQQ.GetAt( 6*nTTIdx, 6*nTTIdx ) ) * dUnitErr;
                    exOriCurErr.pos.Y = sqrt( matQQ.GetAt( 6*nTTIdx+1, 6*nTTIdx+1 ) ) * dUnitErr;
                    exOriCurErr.pos.Z = sqrt( matQQ.GetAt( 6*nTTIdx+2, 6*nTTIdx+2 ) ) * dUnitErr;

                    exOriCurErr.ori.omg = sqrt( matQQ.GetAt( 6*nTTIdx+3, 6*nTTIdx+3 ) ) * dUnitErr;
                    exOriCurErr.ori.phi = sqrt( matQQ.GetAt( 6*nTTIdx+4, 6*nTTIdx+4 ) ) * dUnitErr;
                    exOriCurErr.ori.kap = sqrt( matQQ.GetAt( 6*nTTIdx+5, 6*nTTIdx+5 ) ) * dUnitErr;

                    nTmpIdx++;
                }
                vecImgResErr.push_back( exOriCurErr );
            }
            UINT nTmp3dIdx = 6*(nTmpIdx/2);
            UINT nTmpCurIdx = 0;
            for( UINT i = 0; i < vec3dPts.size(); ++i )
            {
                Pt3d ptCurErr;
                if( false == vecb3dPtIsFixed[i] )
                {
                    ptCurErr.X = sqrt( matQQ.GetAt( nTmp3dIdx+3*nTmpCurIdx, nTmp3dIdx+3*nTmpCurIdx) ) * dUnitErr;
                    ptCurErr.Y = sqrt( matQQ.GetAt( nTmp3dIdx+3*nTmpCurIdx+1, nTmp3dIdx+3*nTmpCurIdx+1) ) * dUnitErr;
                    ptCurErr.Z = sqrt( matQQ.GetAt( nTmp3dIdx+3*nTmpCurIdx+2, nTmp3dIdx+3*nTmpCurIdx+2) ) * dUnitErr;
                    nTmpCurIdx++;
                }
                vec3dPtsResErr.push_back( ptCurErr );
            }
            ptTotalImgResErr.X = dResErrX;
            ptTotalImgResErr.Y = dResErrY;
            return true;
        }
        else
        {
            return false;
        }

    }

    return true;
}
bool CmlBA::BA_WithEpiImg1N1( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts )
{
     vector<ExOriPara> vecImgResErr;
    vector<Pt3d> vec3dPtsResErr;
    Pt2d ptTotalImgResErr;

    return this->BA_WithEpiImg1N1( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtIsFixed, vecImgExOris, vec3dPts, vecImgResErr, vec3dPtsResErr, ptTotalImgResErr );
}
