/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlLocalization.h
* @date 2011.12.18
* @author 万文辉 whwan@irsa.ac.cn
* @brief  定位功能类头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef CMLLOCALIZATION_H
#define CMLLOCALIZATION_H

#include "mlBase.h"
#include "mlRasterBlock.h"
/**
* @struct tagGCPPt
* @date 2011.12.18
* @author 万文辉
* @brief 控制点信息
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagGCPPt
{
    ULONG nID;//!<控制点的ID
    Pt3d ptXYZ;//!<控制点的坐标
} GCPoint;



class CmlLocalization
{
public:
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
    CmlLocalization();

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
    virtual ~CmlLocalization();

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
    bool LocalInSequenceImg(  FrameImgInfo FrameInfoSet, const SCHAR* strSatDom, ImgPtSet &frmPts, ImgPtSet &SatPts, LocalBySeqImgOpts stuLocalBySeqOpts, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );

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
    bool LocalByBundleResection( vector<Pt3d> vecGCPs, vector< ImgPtSet > &vecImgPtSets,  Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy);


    /**
    * @fn LocalBySImgIntersection
    * @date 2011.12.16
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 单片后方交会实现定位
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
    bool LocalBySImgIntersection( vector<Pt3d> vecGCPs, ImgPtSet imgPts,  ExOriPara &exOriRes, vector<RMS2d> &vecRMSRes, DOUBLE &dTotalRMS  );

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
    bool LocalIn2Dom(  const char* strLandDom, const char* strSatDom, ImgPtSet &landPtSet, ImgPtSet &satPtSet, LocalByMatchOpts localByMOpts, Pt2d ptCent, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );

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
    bool LocalInTwoSite( vector<StereoSet> vecFSite, vector<StereoSet> vecESite, vector<ImgPtSet> &vecFrontPts, vector<ImgPtSet> &vecEndPts, LocalBy2SitesOpts stulocalBy2Opt, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );

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
    bool LocalByLander( const vector<StereoSet> &vecFSite, const SCHAR* strGCPFile, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );

    /**
    * @fn WriteLocalRes
    * @date 2011.12.16
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 写出定位信息
    * @param strPath 输出文件路径
    * @param localRes 定位的姿态信息
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool WriteLocalRes( const SCHAR* strPath, ExOriPara localRes );

    /**
    * @fn WriteLocalRes
    * @date 2011.12.16
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 写出定位信息
    * @param strPath 输出文件路径
    * @param ptlocalRes 定位点信息
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool WriteLocalRes( const SCHAR* strPath, Pt3d ptlocalRes );

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
    bool GetImgIDIn2Site(  const vector<StereoSet> &vecFSite, const vector<StereoSet> &vecESite, UINT &nFID, UINT &nEID );

    bool GetImgIDIn2SiteReverse(  const vector<StereoSet> &vecFSite, const vector<StereoSet> &vecESite, UINT &nFID, UINT &nEID );
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
    bool removePtsByDisConsistant( vector<Pt3d> vecPtsInFrontFrm, Pt3d ptFOrig, vector<Pt3d> vecPtsInEndFrm, Pt3d ptEOrig, DOUBLE dDisThreCoef, DOUBLE dWeightThreCoef, vector<bool> &vecFlags );


    bool getInsterestRegion( StereoSet sFirstSet, StereoSet sSecondSet, DOUBLE dCamHeight, DOUBLE dFirstSiteRange, DOUBLE dSecondSiteRange, vector<Pt2d> &vecL, vector<Pt2d> &vecR, SINT nNumTilts = 8 );

//    bool getInsterestRegionReverse( StereoSet sFirstSet, StereoSet sSecondSet, DOUBLE dCamHeight, DOUBLE dFirstSiteRange, DOUBLE dSecondSiteRange, vector<Pt2d> &vecL, vector<Pt2d> &vecR, SINT nNumTilts = 8 );


    bool CalcTwoSitesByBA( vector<Pt2d> vecPtsOneL, vector<Pt2d> vecPtsOneR, vector<Pt2d> vecPtsTwoL, vector<Pt2d> vecPtsTwoR, vector<Pt3d> vecXYZ, \
                           StereoSet sFirstSet, StereoSet sSecondSet, ExOriPara &exTwoL, ExOriPara &exTwoR );
protected:
private:
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
    bool loadGCPFile( const SCHAR* strGCPFile, vector<GCPoint> &vecGCPs );

    /**
    * @fn loadMarkedPtFile
    * @date 2011.12.16
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 导入标志点文件
    * @param clsImgPtSets 影像及站点信息
    * @param mapPts 投射点
    * @param frameInfo 影像信息
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool loadMarkedPtFile( ImgPtSet clsImgPtSets, map<ULONG, Pt2d> &mapPts, FrameImgInfo &frameInfo );


    bool DealImgSelectionIn2Sites( vector<ExOriPara> vecSiteOne, vector<ExOriPara> vecSiteTwo, UINT nIDOne, UINT nIDTwo );
};

#endif // CMLLOCALIZATION_H
