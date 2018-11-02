/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlPhgProc.cpp
* @date 2012.01
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 摄影测量算法类实现头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CMLPHGPROC_H
#define CMLPHGPROC_H
#include "mlBase.h"
#include "mlMat.h"
#include "mlRasterBlock.h"
DOUBLE DisIn2Pts( Pt3d pt1, Pt3d pt2 );
DOUBLE DisIn2Pts( Pt2d pt1, Pt2d pt2 );

bool getxyFromXYZ( DOUBLE& x, DOUBLE& y, DOUBLE X, DOUBLE Y, DOUBLE Z, DOUBLE *XsYsZs, DOUBLE *R, DOUBLE fx, DOUBLE fy );
bool getxyFromXYZ( Pt2d &ptxy, Pt3d ptXYZ, Pt3d ptXYZOrg, CmlMat matOPK, DOUBLE fx, DOUBLE fy );
bool getxyFromXYZ( Pt2d &ptxy, Pt3d ptXYZ, Pt3d ptXYZOrgL, DOUBLE dBaseLength, CmlMat matOPK, DOUBLE fx, DOUBLE fy );

typedef vector<Pt2d> PTARRAY;
typedef vector<Pt3d> PTARRAY_V;
/**
* @class CmlPhgProc
* @date 2011.11
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 摄影测量算法类定义
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/
class CmlPhgProc
{
public :

public:
    /**
    *@fn CmlPhgProc()
    *@date 2011.11
    *@author 张重阳
    *@brief 摄影测量算法类构造函数
    *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
    */
    CmlPhgProc();


    /**
    *@fn ~CmlPhgProc()
    *@date 2011.11
    *@author 张重阳
    *@brief 摄影测量算法析构函数
    *@version 1.0
    *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
    */
    virtual ~CmlPhgProc();


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
    //bool mlReproject(Pt2d* pImgpt, Pt3d* pGroundPt,ExOriPara* pExOri, double fx, double fy);
    bool mlReproject(Pt2d* pImgpt, Pt3d* pGroundPt,ExOriPara* pExOri, DOUBLE fx, DOUBLE fy,CmlMat* pRotateMat = NULL);

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
    bool mlBackForwardinterSection( Pt2d *pImgPt,Pt3d *pGroundPt, double fx,double fy, SINT nPtNum,ExOriPara *pInitExOripara, ExOriPara *pExOripara );

    bool mlBackForwardinterSection( vector<Pt2d> vecImgPts,vector<Pt3d> vec3ds, double dF, ExOriPara *pInitExOripara, ExOriPara *pExOripara );


    /**
     *@fn mlResectionNoInitalVal
     *@date 2011.11
     *@author 万文辉
     *@brief 无需初始值的后方交会
     * @param[in] vecGCPs 控制点坐标
     * @param[in] vecImgPts 像点坐标(像平面坐标系)
     * @param[in] dFocalLength 相机主距(像素)
     * @param[out] exOriRes 定位后的外方位元素
     * @param[out] vecRMS 点误差
     * @param[out] dTotalRMS 总误差
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n

    */
    bool mlResectionNoInitalVal( vector<Pt3d> vecGCPs, vector<Pt2d> vecImgPts, DOUBLE dFocalLength, ExOriPara &exOriRes );

    bool mlCalcResidualError( Pt3d ptXYZ, Pt2d ptOrig2d, ExOriPara exOri, DOUBLE fx, DOUBLE fy, RMS2d &rmsError );
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
    bool mlGeoRasterCut( const SCHAR* strFileIn, const SCHAR* strFileOut,Pt2d pttl,Pt2d ptbr,SINT nflag,SINT nCutBands,DOUBLE dZoom);

//        /**
//         *@fn mlGeoRasterCutByGeopos
//         *@date 2011.11
//         *@author 张重阳
//         *@brief 根据地理坐标对DEM、DOM进行裁剪
//         *@param strFileIn 待裁剪的输入文件
//         *@param strFileOut 裁剪后输出文件
//         *@param pttl 裁剪起始像点地理坐标
//         *@param ptbr 裁剪右下角点地理坐标
//         *@retval TRUE 成功
//         *@retval FALSE 失败
//         *@version 1.0
//         *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
//
//        */
    //  bool mlGeoRasterCutByGeopos(char* strFileIn,char* strFileOut,Pt2d pttl, Pt2d ptbr);



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
    bool mlImageReprj( const SCHAR* strDem, const SCHAR* strDom, const SCHAR* outImg,ExOriPara exori, DOUBLE fx,DOUBLE fy, SINT nImgWid, SINT nImgHei);

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
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

    */
    bool mlTinSimply(vector<Pt3d> &vecPt3dIn,vector<Pt3d> &vecPt3dOut,double simpleIndex);

    bool mlTinSimplyDemoFile(vector<Pt3d> &vecPt3dIn,char* dst);


    bool ml4OnceQuationSolve( DOUBLE da, DOUBLE db, DOUBLE dc, DOUBLE dd, DOUBLE de, vector<DOUBLE> &vecRes  );

    bool mlGetDisBy3Pts(  vector<Pt3d> vecGCPs, vector<Pt2d> vecImgPts, DOUBLE dFocalLength, vector<Pt3d> &vecDisXYZ, Pt3d &dDisBLine, Pt3d &dAngle );

    bool mlGetXYZCoordBy3DisVal( vector<Pt3d> vecGCPs, Pt3d dDis, Pt3d dAngle, Pt3d &ptXYZ );

    bool mlGetRotateMatByXYZ(  vector<Pt3d> vecGCPs, vector<Pt2d> vecImgPts, Pt3d ptXYZ, DOUBLE dF, OriAngle &oriA );

    bool mlSolvePts( vector<Pt3d> vecOldPts, vector<Pt3d> vecNewPts, UINT nTimes, ExOriPara* pInitialOri );


    bool CalcConvexHull(vector<Pt2d> vecSrc, vector<Pt2d> &vecDst);

    bool CalcConvexHull(vector<Pt3d> vecSrc, vector<Pt3d> &vecDst);

    bool GetPolygonUnion2D( Polygon2d poly1, Polygon2d poly2, Polygon2d &polyNew );

    bool GetPolygonUnion3D( Polygon3d poly1, Polygon3d poly2, Polygon3d &polyNew );

    bool ExOriTrans( ExOriPara* pExOriL, ExOriPara* pExOriRela, ExOriPara* pExOriR );

    bool GetRelaOri( ExOriPara* pExOriL, ExOriPara* pExOriR, ExOriPara* pExOriRela );

    bool GetFitPlaneNormal( vector<Pt3d> vecXYZ, Pt3d &pPlaneNormal1, Pt3d &pPlaneNormal2 );

    bool GetAffineTCoef( vector<StereoMatchPt> vecMPts, DOUBLE &dA, DOUBLE &dB, DOUBLE &dC, DOUBLE &dD, DOUBLE &dXAccu, DOUBLE &dYAccu );

    bool GetTargetPtPos( Pt2d ptCent, DOUBLE dA, DOUBLE dB, DOUBLE dC, DOUBLE dD, Pt2d &ptOutRes);

    bool CalcCamOriByGivenInstallMatrix( DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR );

    bool CalcCamOriByGivenInstallMatrixVT( DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matLCamBase2LCamCap, stuInsMat matRCamCap2LCamCap, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR );


    bool CalcCamOriInWorldByGivenInsMat( stuInsMat matBase, DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR );

    bool CalcCamOriInWorldByGivenInsMatVT( stuInsMat matBase, DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matLCamBase2LCamCap, stuInsMat matRCamCap2LCamCap, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR  );

    bool mlCalcHazCamInWorld( stuInsMat matLCamBase2Body, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                    ExOriPara &exOriCamL, ExOriPara &exOriCamR );

    bool mlCalcHazCamInWorldVT( stuInsMat matBase, stuInsMat matLCamCap2Body, stuInsMat matLCamCap2RCamCap, ExOriPara &exOriCamL, ExOriPara &exOriCamR );

    bool mlCalcHazCamInBodyVT( stuInsMat matLCamCap2Body, stuInsMat matLCamCap2RCamCap, ExOriPara &exOriCamL, ExOriPara &exOriCamR );

    bool GetOPKAngle( DOUBLE *pRMat, DOUBLE *pOPK );

    bool GetRMatByOPK( DOUBLE *pOPK, DOUBLE *pRMat );

    bool Polygon3dUnion( Polygon3d poly1, Polygon3d poly2, Polygon3d polyRes );

    bool PersProjInFlat( const SCHAR* strImg, InOriPara inOri, ExOriPara exOri, DOUBLE dRes, DOUBLE dRange, DOUBLE dCamH, const SCHAR* strPers );

    bool PersProjInFlat( const SCHAR* strImg, InOriPara inOri, ExOriPara exOri, DOUBLE dRes, DOUBLE dRange, DOUBLE dCamH, Pt2d &ptOrig, CmlRasterBlock &clsRB );

	bool RelaOriCalcWithOrigPts( vector<StereoMatchPt> vecMatchPts, InOriPara inOriL, UINT nHL, InOriPara inOriR, UINT nHR, ExOriPara &exOriRela );

	bool FindMatchHoleInImg( vector<Pt2i> vecMatchedPts, UINT nW, UINT nH, UINT nHoleRange, vector<Polygon2d> &vecHolePolys );

	bool Find3DCoordBy2DPos( InOriPara inOri, ExOriPara exOri, Pt2d ptPos, GeoRasBlock *pclsGeoRasterB, Pt3d &ptRes );

	bool ResectionInMultiImg( vector<Pt3d> vecGCPs, vector<vector<Pt2d>> vecMultiImgPts, vector<DOUBLE> vecFocals, vector<ExOriPara> vecExInOri, vector<ExOriPara> &vecImgOutExOris );
protected:
private:
	bool getCoef( Pt2d ptXY, Pt3d ptObjXYZ, DOUBLE df, ExOriPara exOri, ExOriPara exOriRela, DOUBLE *pCoef );
};

#endif // CMLPHGPROC_H
