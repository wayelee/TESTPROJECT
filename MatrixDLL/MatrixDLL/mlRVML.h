#ifndef _MLRVML_H_
#define _MLRVML_H_

#include "mlTypes.h"

//输入的矩阵中分别为度数与位置（毫米），输出弧度与米
/**
* @fn mlDealSiteImgSelection
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 根据展开角、偏航角、俯仰角和标定的安置矩阵求得相机在本体坐标系下的姿态与位置，输入单位为度与毫米，输出弧度与米
* @param[in] dExpAngle 展开角
* @param[in] dYawAngle 偏航角
* @param[in] dPitchAngle 俯仰角
* @param[in] matMastExp2Body 展开相对于本体的安装矩阵
* @param[in] matMastYaw2Exp 偏航相对于展开的安装矩阵
* @param[in] matMastPitch2Yaw 俯仰相对于偏航的安装矩阵
* @param[in] matLCamBase2Pitch 左相机基准相对于俯仰的安装矩阵
* @param[in] matRCamBase2LCamBase 右相机基准相对于左相机基准的安装矩阵
* @param[in] matLCamCap2Base 左相机成像相对于基准的安装矩阵
* @param[in] matRCamCap2Base 右相机成像相对于基准的安装矩阵
* @param[out] exOriCamL 左相机外方位
* @param[out] exOriCamR 右相机外方位

* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool ) mlCalcCamOriByGivenInstallMatrix( DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR );

MLAPI( bool ) mlCalcCamOriInWorldByGivenInsMatPosX( stuInsMat matBase, DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
	                                                  stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
	                                                  ExOriPara *pExOriCamL, ExOriPara *pEOriCamR);



MLAPI( bool ) mlCalcHazCamInWorld( stuInsMat matLCamBase2Body, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                    ExOriPara &exOriCamL, ExOriPara &exOriCamR);

MLAPI( bool ) mlCalcHazCamInBodyVT( stuInsMat matLCamCap2Body, stuInsMat matLCamCap2RCamCap, ExOriPara &exOriCamL, ExOriPara &exOriCamR );


MLAPI( bool ) mlCalcMonitorPicXY( DOUBLE dAngle1, DOUBLE dAngle2, DOUBLE dAngle3, DOUBLE dPitchAngle, DOUBLE dCamX, DOUBLE dCamY, DOUBLE dCamZ, DOUBLE dPlaneGround,\
	                                DOUBLE dFocal, DOUBLE dObjX, DOUBLE  dObjY, DOUBLE dObjZ, DOUBLE *pPicX, DOUBLE *pPicY );

MLAPI( bool ) mlCalcObjXY( DOUBLE dAngle1, DOUBLE dAngle2, DOUBLE dAngle3, DOUBLE dPitchAngle, DOUBLE dCamX, DOUBLE dCamY, DOUBLE dCamZ, DOUBLE dPlaneGround,\
									DOUBLE dFocal, DOUBLE dPicX, DOUBLE dPicY ,  DOUBLE *pObjX, DOUBLE *pObjY, DOUBLE *pObjZ);

MLAPI( bool ) mlTest();














#endif


