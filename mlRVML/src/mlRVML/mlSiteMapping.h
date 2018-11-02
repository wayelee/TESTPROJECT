/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlSiteMapping.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 站点地形建立头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef CMLSITEMAPPING_H
#define CMLSITEMAPPING_H

#include "mlBase.h"
#include "mlGeoRaster.h"
#include "mlDemProc.h"
#include "mlDomProc.h"
/**
* @class CmlSiteMapping
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 单站地形建立类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlSiteMapping
{
public :
    DbRect m_dbDemRect ;  //!< 指定DEM范围
    DOUBLE m_dDemRes ; //!<DEM分辨率
    UINT m_nW ;//!<DEM宽
    UINT m_nH ;//!<DEM高
//    CmlDemProc demProc ;
public:
    /**
        * @fn CmlSiteMapping
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief CmlSiteMapping类空参构造函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
    CmlSiteMapping();
    /**
        * @fn ~CmlSiteMapping
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief CmlSiteMapping类析构函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
    virtual ~CmlSiteMapping();
//    /**
//    * @fn mlSiteParaInit
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 单站地形建立参数初始化
//    * @param dbRect 文件路径
//    * @param dRes 分辨率
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool mlSiteParaInit(DbRect dbRect , DOUBLE dRes) ;
    /**
    * @fn MapByMultiBlock
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 影像块分别制图合成
    * @param vecStereoSet 立体影像数据信息
    * @param vecStrMatchFile 匹配点文件
    * @param ptLT 制图区域左上角坐标
    * @param ptRB 制图区域右下角坐标
    * @param dResolution 分辨率
    * @param strDemPath Dem生成路径
    * @param strDomPath Dom生成路径
    * @param bIsUsingFeatPt 判断是否使用匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

    bool MapByMultiBlock(  vector<StereoSet> &vecStereoSet, vector<string> &vecStrMatchFile, Pt2d ptLT, Pt2d ptRB, DOUBLE dResolution,
                           string strDemPath, string strDomPath, bool bIsUsingFeatPt = true );
    /**
    * @fn MapByInteBlock
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 单站一体化制图
    * @param vecStereoSet 立体影像数据信息
    * @param vecImgPtSets 匹配点
    * @param ptLT 制图区域左上角坐标
    * @param ptRB 制图区域右下角坐标
    * @param dResolution 分辨率
    * @param strDemPath Dem生成路径
    * @param strDomPath Dom生成路径
    * @param bIsUsingFeatPt 判断是否使用匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool MapByInteBlock(   vector<StereoSet> &vecStereoSet, vector< ImgPtSet > &vecImgPtSets, Pt2d ptLT, Pt2d ptRB, DOUBLE dResolution,\
                            ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                           string strDemPath, string strDomPath, bool bIsUsingFeatPt = true );
//     /**
//    * @fn DisMapByMultiBlock
//    * @date 2011.11.02
//    * @author 彭嫚 pengman@irsa.ac.cn
//    * @brief 为生成视差图进行密集匹配
//    * @param vecStereoSet 立体影像数据信息
//    * @param vecStrMatchFile 匹配点文件
//    * @param nGrid 视差图生成格网大小
//    * @param nRowRadius 行方向搜索半径
//    * @param nColRadius 列方向搜索半径
//    * @param nTempleSize 模板大小
//    * @param strDisPath 视差图生成路径
//    * @param bIsUsingFeatPt 判断是否使用匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
    //bool DisMapByBlock( vector<StereoSet> &vecStereoSet, vector<string> &vecStrMatchFile);
    /**
    * @fn MapByOneSite
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 单站地形制图
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool MapByOneSite( );
    /**
    * @fn GetStereoFeatPts
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 立体影像对特征点匹配，计算三维点云，存入文件
    * @param pStereoSet 立体影像数据信息
    * @param clsImgPtL 左影像匹配点
    * @param clsImgPtR 右影像匹配点
    * @param vecPt3ds 三维点云
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool GetStereoFeatPts( StereoSet* pStereoSet, \
                           ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                          ImgPtSet &clsImgPtL, ImgPtSet &clsImgPtR, vector<Pt3d> &vecPt3ds, Pt3d &ptMinDis );
//    /**
//    * @fn GetStereoDensePts
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 立体影像对特征点匹配，密集匹配，计算三维点云，存入文件
//    * @param pStereoSet 立体影像数据信息
//    * @param strLPtPath 左影像匹配点文件路径
//    * @param strRPtPath 右影像匹配点文件路径
//    * @param vecPt3ds 三维点云
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
    bool GetStereoDensePts( StereoSet* pStereoSet, string strLPtPath, string strRPtPath, vector<Pt3d> &vecPt3ds, UINT nGrid, UINT nRowRadius,UINT nColRadius,UINT nTempleSize,DOUBLE dCoef );
    /**
    * @fn GetStereoDensePts
    * @date 2011.11.02
    * @author 彭嫚
    * @brief 立体影像对特征点匹配，密集匹配，计算三维点云，存入文件
    * @param pStereoSet 立体影像数据信息
    * @param WidePara 立体影像匹配参数
    * @param ptSetL 左影像密集匹配点
    * @param ptSetR 右影像密集匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool GetStereoDensePts( StereoSet* pStereoSet,  WideOptions WidePara, ImgPtSet& ptSetL, ImgPtSet& ptSetR );

    /**
    * @fn GetStereoDensePts
    * @date 2011.11.02
    * @author 彭嫚
    * @brief 立体影像对特征点匹配，密集匹配，计算三维点云，存入文件
    * @param pStereoSet 立体影像数据信息
    * @param WidePara 立体影像匹配参数
    * @param ptfSetL 左影像特征匹配点
    * @param ptfSetR 右影像特征匹配点
    * @param ptSetL 左影像密集匹配点
    * @param ptSetR 右影像密集匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool GetStereoDensePts( StereoSet* pStereoSet,  WideOptions WidePara, ImgPtSet& ptfSetL, ImgPtSet& ptfSetR ,ImgPtSet& ptSetL, ImgPtSet& ptSetR );
/**
* @fn GetRegionDensePts
* @date 2011.12.14
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 影像指定区域密集匹配
* @param[in] pStereoSet 立体像对
* @param[in] GauPara 高斯滤波参数
* @param[in] ExtractPara 特征点提取参数
* @param[in] MatchPara 特征点匹配参数
* @param[in] RanPara RANSAC去粗差参数
* @param[in] RectSearch 模板匹配搜索范围参数
* @param[in] WidePara 密集匹配参数
* @param[in] Lx 待匹配左影像指定矩形范围的左上角x坐标
* @param[in] Ly 待匹配左影像指定矩形范围的左上角y坐标
* @param[in] ColRange 待匹配左影像指定矩形范围的宽
* @param[in] RowRange 待匹配左影像指定矩形范围的高
* @param[out] vecDPtSetL 密集匹配的左影像点坐标
* @param[out] vecDPtSetR 密集匹配的右影像点坐标
* @param[out] vecPtObj 密集匹配的点的三维坐标
* @param[out] vecCorr 密集匹配的相关系数
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool GetRegionDensePts( StereoSet* pStereoSet, GaussianFilterOpt GauPara, ExtractFeatureOpt ExtractPara, MatchInRegPara MatchPara, RANSACHomePara RanPara, MLRectSearch RectSearch, WideOptions WidePara, SINT Lx, SINT Ly, SINT ColRange, SINT RowRange,
                                   ImgPtSet& ptSetL, ImgPtSet& ptSetR, vector<Pt3d>& vecPtObj, vector<DOUBLE>& vecCorr);
    bool WriteDisMap(vector<Pt3d> vecPt3ds, char* DisPath);

    bool GetRegionDensePts( StereoSet* pStereoSet, vector<StereoMatchPt> vecFeatMatchPt, WideOptions WidePara, SINT Lx, SINT Ly, SINT ColRange, SINT RowRange,
                                  ImgPtSet& ptSetL, ImgPtSet& ptSetR, vector<Pt3d>& vecPtObj, vector<DOUBLE>& vecCorr);

    bool BA( vector<StereoSet> vecStereoSetIn, vector< ImgPtSet > &vecImgPtSets, \
                            ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                           vector<StereoSet> &vecStereoOut );

    bool FindTiePoints( vector<StereoSet> vecStereoSetIn, vector< ImgPtSet > &vecImgPtSets, \
                            ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts );

    bool BASolve( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<StereoSet> &vecStereoOut, UINT nModel = 2 );

    bool BASolve( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<StereoSet> &vecStereoOut, Pt2d &ptProjErr, UINT nModel = 2 );

    bool BASolve( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<bool> vecbImgIsFixed, vector<StereoSet> &vecStereoOut, UINT nModel = 1 );
public:
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 吴凯 wukai@irsa.ac.cn
//    * @brief 三维点云生成指定范围DEM，并存入geotiff文件
//    * @param vec3DPts 三维点云
//    * @param strGeoFilePath 文件路径
//    * @param dbDemRect DEM生成范围
//    * @param dRes DEM分辨率
//    * @param bType 是否剔除粗差点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool mlWriteToGeoFile(vector<Pt3d>& vec3DPts , SCHAR* strGeoFilePath , DbRect dbDemRect , DOUBLE dRes = 1.0 , delErrorType bType = delOff) ;
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 吴凯 wukai@irsa.ac.cn
//    * @brief 三维点云全部生成DEM，并存入geotiff文件
//    * @param vec3DPts 三维点云
//    * @param strGeoFilePath 文件路径
//    * @param dRes DEM分辨率
//    * @param bType 是否剔除粗差点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool mlWriteToGeoFile(vector<Pt3d>& vec3DPts , SCHAR* strGeoFilePath , DOUBLE dRes = 1.0 , delErrorType bType = delOff) ;
    //分块写入到geoTiff文件
//    bool mlWriteToGeoFile(vector<Pt3d>& vec3DPts , SINT nXOrigin , SINT nYOrigin , SINT nBlockWidth , SINT nBlockHeight , SCHAR* strGeoFilePath) ;
protected:
private:
//      /**
//    * @fn getRangeFromPts
//    * @date 2011.11.02
//    * @author 吴凯 wukai@irsa.ac.cn
//    * @brief 计算三维点云范围
//    * @param vec3DPts 三维点云
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//  bool getRangeFromPts(vector<Pt3d>& vec3DPts) ;
    /**
        * @fn paraInit
        * @date 2011.11.02
        * @author 吴凯 wukai@irsa.ac.cn
        * @brief 参数初始化
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
    bool paraInit() ;

    bool judgeIsOverlap( FrameImgInfo *pSet1, FrameImgInfo *pSet2, DOUBLE &dXOff, DOUBLE &dYOff  );
};

#endif // CMLSITEMAPPING_H
