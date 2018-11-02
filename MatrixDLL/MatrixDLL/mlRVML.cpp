/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlRVML.cpp
* @date 2011.2.1
* @author ���Ļ� whwan@irsa.ac.cn
* @brief �ӿں���Դ�ļ�
* @version 1.0
* @par �޸���ʷ��
* <����>  <�޸�����>  <�汾��>  <��ϸ����>\n
*/

#include "mlRVML.h"

//#include "mlGdalDataset.h"
//#include "mlWBaseProc.h"
//#include "mlCoordTrans.h"
//#include "mlCamCalib.h"
//#include "mlDemProc.h"
//#include "mlFrameImage.h"
//#include "mlGeoRaster.h"
//#include "mlLinearImage.h"
//#include "mlLocalization.h"
#include "mlPhgProc.h"
//#include "mlSatMapping.h"
//#include "mlSiteMapping.h"
//#include "mlStereoProc.h"
//#include "mlTIN.h"
//#include "mlWBaseProc.h"
//#include "mlDemAnalyse.h"
//#include "mlRasterMosaic.h"
//#include "mlBA.h"


ML_EXTERN_C bool mlCalcCamOriByGivenInstallMatrix( DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
        stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
        ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    CmlPhgProc clsPhg;
    return clsPhg.CalcCamOriByGivenInstallMatrix( dExpAngle, dYawAngle, dPitchAngle, matMastExp2Body, matMastYaw2Exp, matMastPitch2Yaw, \
            matLCamBase2Pitch, matRCamBase2LCamBase, matLCamCap2Base, matRCamCap2Base, exOriCamL, exOriCamR );
}

ML_EXTERN_C bool mlCalcCamOriInWorldByGivenInsMatPosX( stuInsMat matBase, DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
	stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
	ExOriPara *pExOriCamL, ExOriPara *pEOriCamR  )
{

	CmlPhgProc clsPhg;
	ExOriPara exLRes;
	ExOriPara exRRes;
	//-------------
	//CmlMat matOrig;
	//ExOriPara angle;
	//angle.ori.omg = ML_PI;
	//angle.ori.kap = -ML_PI / 2.0;

	//ExOriPara exOriBase;
	//exOriBase.pos.X = matBase.dPosMatrix[0];
	//exOriBase.pos.Y = matBase.dPosMatrix[1];
	//exOriBase.pos.Z = matBase.dPosMatrix[2];

	//CmlMat matB;
	//matB.Initial( 3, 3);
	//memcpy( matB.GetData(), matBase.dOriMatrix, sizeof(DOUBLE)*9);

	//RMat2OPK( &matB, &exOriBase.ori );

	//ExOriPara exRes;
	//clsPhg.ExOriTrans( &angle, &exOriBase, &exRes );

	//CmlMat matResTmp;
	//OPK2RMat( &exRes.ori, &matResTmp );
	//matBase.dPosMatrix[0] = exRes.pos.X;
	//matBase.dPosMatrix[1] = exRes.pos.Y;
	//matBase.dPosMatrix[2] = exRes.pos.Z;

	//memcpy( matBase.dOriMatrix, matResTmp.GetData(), sizeof(DOUBLE)*9);
	//-------------

	return clsPhg.CalcCamOriInWorldByGivenInsMat( matBase, dExpAngle, dYawAngle, dPitchAngle, matMastExp2Body, matMastYaw2Exp, matMastPitch2Yaw, \
		matLCamBase2Pitch, matRCamBase2LCamBase, matLCamCap2Base, matRCamCap2Base/*, exLRes,exRRes*/,*pExOriCamL, *pEOriCamR);
	//return exLRes.pos.X;	
}


ML_EXTERN_C bool mlCalcHazCamInWorld( stuInsMat matLCamBase2Body, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                    ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    CmlPhgProc clsPhg;
    return clsPhg.mlCalcHazCamInWorld( matLCamBase2Body, matRCamBase2LCamBase, matLCamCap2Base, matRCamCap2Base, exOriCamL, exOriCamR );
}
ML_EXTERN_C bool mlCalcHazCamInBodyVT( stuInsMat matLCamCap2Body, stuInsMat matLCamCap2RCamCap, ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    CmlPhgProc clsPhg;
    return clsPhg.mlCalcHazCamInBodyVT( matLCamCap2Body, matLCamCap2RCamCap, exOriCamL, exOriCamR );
}

ML_EXTERN_C bool mlTest()
{
	return true;
}

ML_EXTERN_C bool mlCalcMonitorPicXY( DOUBLE dAngle1, DOUBLE dAngle2, DOUBLE dAngle3, DOUBLE dPitchAngle, DOUBLE dCamX, DOUBLE dCamY, DOUBLE dCamZ,DOUBLE dPlaneGround,\
	DOUBLE dFocal, DOUBLE dObjX, DOUBLE  dObjY, DOUBLE dObjZ, DOUBLE *pPicX, DOUBLE *pPicY )
{
	CmlMat matL2B;
	if ( false == matL2B.Initial( 3, 3 ) )
	{
		return false;
	}
	dAngle1 = Deg2Rad( dAngle1 );
	dAngle2 = Deg2Rad( dAngle2 );
	dAngle3 = Deg2Rad( dAngle3 );

	matL2B.SetAt( 0, 0, ( cos(dAngle2)*cos(dAngle3)-sin(dAngle2)*sin(dAngle1)*sin(dAngle3) ));
	matL2B.SetAt( 0, 1, ( cos(dAngle2)*sin(dAngle3)+sin(dAngle2)*sin(dAngle1)*cos(dAngle3) ));
	matL2B.SetAt( 0, 2, ( -sin(dAngle2)*cos(dAngle1) ));

	matL2B.SetAt( 1, 0, ( -cos(dAngle1)*sin(dAngle3) ));
	matL2B.SetAt( 1, 1, ( cos(dAngle1)*cos(dAngle3) ));
	matL2B.SetAt( 1, 2, ( sin(dAngle1) ) );

	matL2B.SetAt( 2, 0, ( sin(dAngle2)*cos(dAngle3)+cos(dAngle2)*sin(dAngle1)*sin(dAngle3) ));
	matL2B.SetAt( 2, 1, ( sin(dAngle2)*sin(dAngle3)-cos(dAngle2)*sin(dAngle1)*cos(dAngle3) ));
	matL2B.SetAt( 2, 2, ( cos(dAngle2)*cos(dAngle1) ));

	CmlMat matB2Cam, matCamPitch;

	OriAngle oriB2Cam;
	oriB2Cam.omg = Deg2Rad(180);
	oriB2Cam.phi = Deg2Rad(0);
	oriB2Cam.kap = Deg2Rad(-90);
	OPK2RMat( &oriB2Cam, &matB2Cam );

	OriAngle oriCamPitch;
	oriCamPitch.omg = Deg2Rad(-15);

	OPK2RMat( &oriCamPitch, &matCamPitch );

	CmlMat matL2BInv;
	if ( false == mlMatTrans( &matL2B, &matL2BInv ) )
	{
		return false;
	}

	CmlMat matTmp, matRes;
	if ( false == mlMatMul( &matL2BInv, &matB2Cam, &matTmp ))
	{
		return false;
	}
	if ( false == mlMatMul( &matTmp, &matCamPitch, &matRes ))
	{
		return false;
	}
	ExOriPara exCam;
	RMat2OPK( &matRes, &exCam.ori );
	exCam.pos.X = dCamX;
	exCam.pos.Y = dCamY;
	exCam.pos.Z = dCamZ;

	//=======================
	Pt2d ptxy;
	Pt3d ptXYZ;
	ptXYZ.X = dObjX;
	ptXYZ.Y = dObjY;
	ptXYZ.Z = dObjZ;
	Pt3d ptXYZOrg;
	ptXYZOrg.X = dCamX;
	ptXYZOrg.Y = dCamY;
	ptXYZOrg.Z = dCamZ;
 
	getxyFromXYZ( ptxy, ptXYZ, ptXYZOrg, matRes, dFocal, dFocal );

	*pPicX = ptxy.X;
	*pPicY = ptxy.Y;

	return true;
}

ML_EXTERN_C bool mlCalcObjXY( DOUBLE dAngle1, DOUBLE dAngle2, DOUBLE dAngle3, DOUBLE dPitchAngle, DOUBLE dCamX, DOUBLE dCamY, DOUBLE dCamZ, DOUBLE dPlaneGround,\
	DOUBLE dFocal, DOUBLE dPicX, DOUBLE dPicY , DOUBLE *pObjX, DOUBLE *pObjY, DOUBLE *pObjZ)
{
	CmlMat matL2B;
	if ( false == matL2B.Initial( 3, 3 ) )
	{
		return false;
	}
	dAngle1 = Deg2Rad( dAngle1 );
	dAngle2 = Deg2Rad( dAngle2 );
	dAngle3 = Deg2Rad( dAngle3 );
	
	matL2B.SetAt( 0, 0, ( cos(dAngle2)*cos(dAngle3)-sin(dAngle2)*sin(dAngle1)*sin(dAngle3) ));
	matL2B.SetAt( 0, 1, ( cos(dAngle2)*sin(dAngle3)+sin(dAngle2)*sin(dAngle1)*cos(dAngle3) ));
	matL2B.SetAt( 0, 2, ( -sin(dAngle2)*cos(dAngle1) ));

	matL2B.SetAt( 1, 0, ( -cos(dAngle1)*sin(dAngle3) ));
	matL2B.SetAt( 1, 1, ( cos(dAngle1)*cos(dAngle3) ));
	matL2B.SetAt( 1, 2, ( sin(dAngle1) ) );

	matL2B.SetAt( 2, 0, ( sin(dAngle2)*cos(dAngle3)+cos(dAngle2)*sin(dAngle1)*sin(dAngle3) ));
	matL2B.SetAt( 2, 1, ( sin(dAngle2)*sin(dAngle3)-cos(dAngle2)*sin(dAngle1)*cos(dAngle3) ));
	matL2B.SetAt( 2, 2, ( cos(dAngle2)*cos(dAngle1) ));

	CmlMat matB2Cam, matCamPitch;

	OriAngle oriB2Cam;
	oriB2Cam.omg = Deg2Rad(180);
	oriB2Cam.phi = Deg2Rad(0);
	oriB2Cam.kap = Deg2Rad(-90);
	OPK2RMat( &oriB2Cam, &matB2Cam );

	OriAngle oriCamPitch;
	oriCamPitch.omg = Deg2Rad(-15);

	OPK2RMat( &oriCamPitch, &matCamPitch );

	CmlMat matL2BInv;
	if ( false == mlMatTrans( &matL2B, &matL2BInv ) )
	{
		return false;
	}

	CmlMat matTmp, matRes;
	if ( false == mlMatMul( &matL2BInv, &matB2Cam, &matTmp ))
	{
		return false;
	}
	if ( false == mlMatMul( &matTmp, &matCamPitch, &matRes ))
	{
		return false;
	}
	ExOriPara exCam;
	RMat2OPK( &matRes, &exCam.ori );
	exCam.pos.X = dCamX;
	exCam.pos.Y = dCamY;
	exCam.pos.Z = dCamZ;
	
	//=======================
	CmlMat matPic, matPicRes;
	if ( false == matPic.Initial( 3, 1 ))
	{
		return false;
	}
	
	matPic.SetAt( 0, 0, dPicX );
	matPic.SetAt( 1, 0, dPicY );
	matPic.SetAt( 2, 0, -dFocal );

	if ( false == mlMatMul( &matRes, &matPic, &matPicRes ))
	{
		return false;
	}
	if ( matPicRes.GetAt( 0, 0 ) > 0 )
	{
		return false;
	}
	


	DOUBLE dRatio = ( (dPlaneGround - dCamX) / matPicRes.GetAt( 0, 0 ) );
	
	*pObjX = dRatio*matPicRes.GetAt( 0, 0 ) + dCamX;
	*pObjY = dRatio*matPicRes.GetAt( 1, 0 ) + dCamY;
	*pObjZ = dRatio*matPicRes.GetAt( 2, 0 ) + dCamZ;

	return true;
	
}