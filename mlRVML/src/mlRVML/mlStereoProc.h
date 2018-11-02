/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlStereoProc.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 立体影像处理头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef CMLSTEREOPROC_H
#define CMLSTEREOPROC_H

#include "mlBase.h"
#include "mlGdalDataset.h"

//由三维点结构派生，包含了左影像点，右影像点（即七点结构）

/**
* @class CmlStereoProc
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 立体影像处理类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlStereoProc
{
public:
    /**
    * @fn CmlStereoProc
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlStereoProc类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlStereoProc();
    /**
    * @fn CmlStereoProc
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlStereoProc类有参构造函数
    * @param m_pDataL 左影像
    * @param m_pDataR 右影像
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlStereoProc( CmlGdalDataset* m_pDataL, CmlGdalDataset* m_pDataR );
    /**
    * @fn ~CmlStereoProc
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlStereoProc类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    virtual ~CmlStereoProc();
    /**
    * @fn mlGetRansacPts
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 利用Ransac方法剔除立体匹配点粗差
    * @param MatchPts 输入的立体匹配点
    * @param RanSacPts 输出的立体匹配点
    * @param dThresh 阈值
    * @param dConfidence 置信度
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlGetRansacPts(vector<StereoMatchPt> &MatchPts, vector<StereoMatchPt> &RanSacPts, DOUBLE dThresh = 3, DOUBLE dConfidence = 0.99 );
    /**
    * @fn mlGetRansacPts
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 利用Ransac方法剔除立体匹配点粗差
    * @param MatchPts 立体匹配点
    * @param dThresh 阈值
    * @param dConfidence 置信度
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlGetRansacPts(vector<StereoMatchPt> &MatchPts, DOUBLE dThresh = 3, DOUBLE dConfidence = 0.99 );
    /**
    * @fn mlGetRansacPtsByAffineT
    * @date 2011.11.02
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 利用Ransac方法仿射变换模型剔除立体匹配点粗差
    * @param vxl 左影像匹配点X
    * @param vyl 左影像匹配点y
    * @param vxr 右影像匹配点x
    * @param vyr 右影像匹配点y
    * @param RanSacPts 输出的立体匹配点
    * @param dSigma 噪声阈值
    * @param dConfidence 最小迭代次数
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    SINT mlGetRansacPtsByAffineT(vector<DOUBLE> vxl, vector<DOUBLE> vyl, vector<DOUBLE> vxr, vector<DOUBLE> vyr,\
                                 vector<StereoMatchPt> &RanSacPts, DOUBLE dSigma, UINT nMinItera );

    /**
    * @fn mlGetRansacPts
    * @date 2012.2.10
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 利用Ransac方法剔除立体匹配点粗差
    * @param vxl 左影像匹配点X
    * @param vyl 左影像匹配点y
    * @param vxr 右影像匹配点x
    * @param vyr 右影像匹配点y
    * @param LRanPts 剔除粗差后左影像匹配点
    * @param RRanPts 剔除粗差后右影像匹配点
    * @param dThresh 阈值
    * @param dConfidence 置信度
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlGetRansacPts( vector<DOUBLE> vxl, vector<DOUBLE> vyl, vector<DOUBLE> vxr, vector<DOUBLE> vyr,\
                         vector<StereoMatchPt> &vecRanPts, DOUBLE dThresh, DOUBLE dConfidence );

    /**
    * @fn mlGetRansacPtsByAffineT
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 利用Ransac方法剔除立体匹配点粗差
    * @param MatchPts 立体匹配点
    * @param RanSacPts 去除粗差后点
    * @param nMinLierSetNum 最小内点集合数
    * @param dMaxError 最大误差
    * @return  去除粗差后点的个数
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlGetRansacPtsByAffineT( vector<StereoMatchPt> &MatchPts, vector<StereoMatchPt> &RanSacPts, DOUBLE dSegma, DOUBLE dMaxError );

    bool mlGetRansacPtsByAffineT( vector<StereoMatchPt> &MatchPts, DOUBLE dSegma, DOUBLE dMaxError );
    /**
    * @fn mlFilterPtsByMedian
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 中值滤波
    * @param MatchPts 立体匹配点
    * @param nTemplateSize 模板大小
    * @param dThresCoef 中值不符值的阈值
    * @param dThres 最小视差限差
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlFilterPtsByMedian( vector<StereoMatchPt> &MatchPts, SINT nTemplateSize = 5, DOUBLE dThresCoef = 0.1, DOUBLE dThres = 5 );

    bool mlFilterPtsByMedianByCol( vector<StereoMatchPt> &MatchPts );
    /**
    * @fn mlTemplateMatchInFeatPt
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 图像间特征点模板匹配
    * @param rectSearch 匹配范围
    * @param nTemplateSize 匹配窗口大小
    * @param dCoef 相关系数阈值
    * @param nXOffSet 横向偏移量
    * @param nYOffSet 竖向偏移量
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    //此函数专用于FrameImage等不需分块的情况,结果输入 m_vecFeatMatchPt; 右片的搜索范围等于 左片对应点 + rectSearch + offset;结果输出于m_vecFeatMatchPt；
    bool mlTemplateMatchInFeatPt( MLRect rectSearch, SINT nTemplateSize, DOUBLE dCoef = 0.75, SINT nXOffSet = 0, SINT nYOffSet = 0 );


    /**
    * @fn mlTemplateMatch
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 立体图像间模板匹配
    * @param pLeftImg 左影像Block
    * @param pRightImg 右影像Block
    * @param vecPtL 左影像匹配点
    * @param vecPtR 右影像匹配点
    * @param vecMatchPts 匹配范围
    * @param rectSearch 匹配范围
    * @param nTemplateSize 匹配窗口大小
    * @param dCoef 相关系数阈值
    * @param nXOffSet 横向偏移量
    * @param nYOffSet 竖向偏移量
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlTemplateMatch( CmlRasterBlock* pLeftImg, CmlRasterBlock* pRightImg, vector<Pt2i> &vecPtL, \
                          vector<Pt2i> &vecPtR, vector<StereoMatchPt> &vecMatchPts, MLRect rectSearch, \
                          SINT nTemplateSize, DOUBLE dCoef = 0.75, SINT nXOffSet = 0, SINT nYOffSet = 0, bool bIsRemoveAbPixel = false );

    bool mlTemplateMatchWithAccu( CmlRasterBlock* pLeftImg, CmlRasterBlock* pRightImg, vector<Pt2i> &vecPtL, \
                          vector<Pt2i> &vecPtR, vector<StereoMatchPt> &vecMatchPts, MLRect rectSearch, \
                          SINT nTemplateSize, DOUBLE dCoef = 0.75, SINT nXOffSet = 0, SINT nYOffSet = 0, bool bIsRemoveAbPixel = false );
    /**
    * @fn mlTemplateMatchInRegion
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 根据左影像上一点取得右影像上同名点
    * @param pLeftImg 左影像Block
    * @param pRightImg 右影像Block
    * @param ptLeft 左影像某点
    * @param ptRight 右影像待匹配点
    * @param nXMin 匹配范围左上角X
    * @param nXMax 匹配范围右下角X
    * @param nYMin 匹配范围左上角Y
    * @param nYMax 匹配范围右下角Y
    * @param nTemplateSize 匹配窗口大小
    * @param dCoef 相关系数阈值
    * @param nXOffSet 横向偏移量
    * @param nYOffSet 竖向偏移量
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlTemplateMatchInRegion( CmlRasterBlock* pLeftImg, CmlRasterBlock* pRightImg, Pt2i ptLeft, Pt2i &ptRight, DOUBLE &dCoef,\
                                  SINT nXMin, SINT nXMax, SINT nYMin, SINT nYMax, \
                                  SINT nTemplateSize, DOUBLE dCoefThres = 0.75, SINT nXOffSet = 0, SINT nYOffSet = 0, bool bIsRemoveAbPixel = false  );

    bool mlTemplateMatchInTwoFeatPtsVerify( const SCHAR* pLImg, const SCHAR* pRImg, vector<Pt2i> vecLPts, vector<Pt2i> vecRPts, vector<StereoMatchPt> &vecSMPts, \
                                           MatchInRegPara matchPara, bool bIsRemoveAbPixel = false );

    bool mlTemplateMatchInLFeatPtsToAll( const SCHAR* pLImg, const SCHAR* pRImg, vector<Pt2i> vecLPts, vector<StereoMatchPt> &vecSMPts, \
                                           MatchInRegPara matchPara, bool bIsRemoveAbPixel = false );

    bool mlTemplateMatchInRFeatPtsToAll( const SCHAR* pLImg, const SCHAR* pRImg, vector<Pt2i> vecRPts, vector<StereoMatchPt> &vecSMPts, \
                                           MatchInRegPara matchPara, bool bIsRemoveAbPixel = false );

    bool mlFindCorPtsInRegFeatPts( const char* pImgLPath, const char* pImgRPath, Pt2i ptL, Pt2i &ptR, DOUBLE &dCoef, \
                                   ExtractFeatureOpt extFeatOpts, MatchInRegPara matchPara );


    bool mlFindCorPtsInRegFeatPts( CmlRasterBlock* pBlockL, CmlRasterBlock* pBlockR, vector<Pt2i> vecPtsInRImg, Pt2i ptL, Pt2i &ptR, DOUBLE &dCoef, \
                                   ExtractFeatureOpt extFeatOpts, MatchInRegPara matchPara );



     /**
    * @fn mlLsMatchInFrameImg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵影像最小二乘匹配，得到子像素匹配点
    * @param vecMatchPt 左右影像匹配点坐标
    * @param nTemplateSize 匹配窗口大小
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlLsMatchInFrameImg(  vector<StereoMatchPt> &vecMatchPt, SINT nTempSize );
    /**
    * @fn mlLsMatch
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵影像最小二乘匹配，根据左影像上一点取得右影像上同名点
    * @param pBlockL 左影像Block
    * @param pBlockR 右影像Block
    * @param dLx 左影像匹配点X
    * @param dLy 左影像匹配点Y
    * @param dRx 右影像待匹配点X
    * @param dRy 右影像待匹配点Y
    * @param nTempSize 匹配窗口大小
    * @param dCoef 相关系数阈值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlLsMatch( CmlRasterBlock* pBlockL,  CmlRasterBlock* pBlockR, DOUBLE dLx, DOUBLE dLy, DOUBLE& dRx, DOUBLE& dRy, SINT nTempSize, DOUBLE& dCoef );

    /**
    * @fn mlInterSectionInFrameImg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵影像空间前方交会
    * @param vecMatchPt 匹配点与空间三维点的七点结构体
    * @param dThres 迭代阈值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlInterSectionInFrameImg(  vector<StereoMatchPt> &vecMatchPt, DOUBLE dThres = 0.0001 );

    bool mlInterSectionInFrameImg(  vector<StereoMatchPt> &vecMatchPt, vector<bool> &vecbIsValid, DOUBLE dThres = 0.0001 );


    /**
    * @fn mlInterSectionInFrameImg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵影像空间前方交会
    * @param[in] vecMatchPt 匹配点与空间三维点的七点结构体
    * @param[out] vec3dPts 空间三维点
    * @param dThres 迭代阈值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlInterSectionInFrameImg(  vector<StereoMatchPt> &vecMatchPt, vector<Pt3d> &vec3dPts, DOUBLE dThres = 0.0001 );
    //此函数内不含畸变校正功能
    /**
    * @fn mlInterSection
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 不含畸变校正功能的通用空间前方交会函数
    * @param ptL 左匹配点
    * @param ptR 右匹配点
    * @param nHeightL 左影像高
    * @param nHeightR 右影像高
    * @param ptXYZ 三维点坐标
    * @param pInOriL 左影像内方位
    * @param pExOriL 左影像外方位
    * @param pInOriR 右影像内方位
    * @param pExOriR 右影像外方位
    * @param dThres 迭代阈值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlInterSection(  Pt2d ptL, Pt2d ptR, SINT nHeightL, SINT nHeightR, Pt3d &ptXYZ, \
                          InOriPara* pInOriL, ExOriPara* pExOriL, InOriPara* pInOriR, ExOriPara* pExOriR, DOUBLE dThres = 0.0001 );

    bool mlInterSection(  Pt2d ptL, Pt2d ptR, Pt3d &ptXYZ, \
                          InOriPara* pInOriL, ExOriPara* pExOriL, InOriPara* pInOriR, ExOriPara* pExOriR, DOUBLE dThres = 0.0001 );
    /**
    * @fn mlInterSection
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 不含畸变校正功能的通用空间前方交会函数
    * @param dLx 左匹配点X
    * @param dLy 左匹配点Y
    * @param dRx 右匹配点X
    * @param dRy 右匹配点Y
    * @param pMatL 左影像旋转矩阵
    * @param pMatR 右影像旋转矩阵
    * @param PtXsYsZsL 左影像外方位线元素
    * @param PtXsYsZsR 右影像外方位线元素
    * @param f1 左像主距
    * @param f2 右像主距
    * @param PtXYZ 输出三维点坐标
    * @param dThres 迭代阈值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlInterSection( DOUBLE dLx, DOUBLE dLy, DOUBLE dRx, DOUBLE dRy, CmlMat* pMatL, CmlMat* pMatR, Pt3d PtXsYsZsL, Pt3d PtXsYsZsR, DOUBLE f1, DOUBLE f2, Pt3d &PtXYZ, DOUBLE dThres = 0.0001 );

    bool mlInterSection( DOUBLE dLx, DOUBLE dLy, DOUBLE dRx, DOUBLE dRy, CmlMat* pMatL, CmlMat* pMatR, Pt3d PtXsYsZsL, Pt3d PtXsYsZsR, DOUBLE f1, DOUBLE f2, \
                         Pt3d &ptXYZ, Pt2d &ptLxyAccu, Pt2d &ptRxyAccu, Pt3d &ptXYZAccu, DOUBLE dThres = 0.0001 );
    /**
    * @fn mlGetEpipolarImg
    * @date 2011.11.27
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 将非核线影像重采样成核线影像
    * @param pLOriImg 左影像块
    * @param pROriImg 右影像块
    * @param pInOriL 左影像内方位
    * @param pExOriL 左影像外方位
    * @param pInOriR 右影像内方位
    * @param pExOriR 右影像外方位
    * @param pLEpiImg 左核线影像
    * @param pREpiImg 右核线影像
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlGetEpipolarImg( CmlRasterBlock *pLOriImg, CmlRasterBlock *pROriImg, InOriPara* pInOriL, ExOriPara* pExOriL, \
                           InOriPara* pInOriR, ExOriPara* pExOriR, CmlRasterBlock *pLEpiImg, CmlRasterBlock *pREpiImg );
    /**
    * @fn mlGetEpipolarImg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 核线影像生成入口函数
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlGetEpipolarImg();
    /**
    * @fn mlDenseMatch
    * @date 2011.12.14
    * @author 彭嫚 pengman@irsa.ac.cn
    * @brief 影像密集匹配
    * @param pBlockL 左影像块
    * @param pBlockR 右影像块
    * @param vecMatchPt 影像匹配特征点
    * @param WidePara 匹配参数结构
    * @param vecLPts 密集匹配的左影像点坐标
    * @param vecRPts 密集匹配的右影像点坐标
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlDenseMatch(CmlRasterBlock* pBlockL,  CmlRasterBlock* pBlockR,vector<StereoMatchPt> &vecMatchPt, WideOptions WidePara, \
                      vector<Pt2d> &vecLPts,  vector<Pt2d> &vecRPts);

    /**
    * @fn mlDenseMatch
    * @date 2011.12.14
    * @author 彭嫚 pengman@irsa.ac.cn
    * @brief 影像密集匹配
    * @param pBlockL 左影像块
    * @param pBlockR 右影像块
    * @param vecMatchPt 影像匹配特征点
    * @param WidePara 匹配参数结构
    * @param rectMatch 指定匹配区域
    * @param vecLPts 密集匹配的左影像点坐标
    * @param vecRPts 密集匹配的右影像点坐标
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlDenseMatch(CmlRasterBlock* pBlockL,  CmlRasterBlock* pBlockR,vector<StereoMatchPt> &vecMatchPt, WideOptions WidePara, \
                      MLRect rectMatch, vector<Pt2d> &vecLPts,  vector<Pt2d> &vecRPts, vector<DOUBLE> &vecCorr, bool bIsRemoveAbPixel = false );

    /**
    * @fn mlDisEstimate
    * @date 2011.12.14
    * @author 彭嫚 pengman@irsa.ac.cn
    * @brief 根据正确匹配的特征点生成三角网进行视差范围预测
    * @param vFeaPtL 特征点坐标和视差
    * @param nStep 密集匹配的格网大小
    * @param nRadius 视差估计的搜索半径大小
    * @param vecPt 待匹配点的坐标
    * @param vecDisxy 待匹配点的视差范围
    * @return 进行视差范围预测的匹配点个数
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    SINT mlDisEstimate(vector<Pt3d> &vFeaPtL, UINT nStep,SINT nRadius,vector<Pt2i> &vecPt,vector<Pt2i> &vecDisxy);
    /**
    * @fn mlUniquePt
    * @date 2011.12.20
    * @author 彭嫚 pengman@irsa.ac.cn
    * @brief 去掉特征点匹配中重复的点
    * @param MatchPts 特征匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlUniquePt(vector<StereoMatchPt> &MatchPts);
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 读三维点
//    * @param strPath 文件路径
//    * @param vecPt3d 三维点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool ReadFeatMatchPts( string strPath, vector<Pt3d> &vecPt3d );
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 读立体匹配点
//    * @param strPath 文件路径
//    * @param vecStereoPt 立体匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool ReadFeatMatchPts( string strPath, vector<StereoMatchPt> &vecStereoPt );
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 读立体匹配点
//    * @param strLPath 左文件路径
//    * @param strRPath 右文件路径
//    * @param vecStereoPt 立体匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool ReadFeatMatchPts( string strLPath, string strRPath, vector<Pt3d> &vecStereoPt );
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 读立体匹配点
//    * @param strLPath 左文件路径
//    * @param strRPath 右文件路径
//    * @param vecStereoPt 立体匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool ReadFeatMatchPts( string strLPath, string strRPath, vector<StereoMatchPt> &vecStereoPt );
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 将立体匹配点写入文件
//    * @param strPath 文件路径
//    * @param vecFeatMatchPt 立体匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool WriteFeatMatchPts( string strPath, vector<StereoMatchPt> &vecFeatMatchPt );
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 将立体匹配点写入文件
//    * @param strLPath 左文件路径
//    * @param strRPath 右文件路径
//    * @param pStereoSet 立体像对影像信息
//    * @param vecFeatMatchPt 匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool WriteFeatMatchPts( string strLPath, string strRPath, StereoSet *pStereoSet, vector<StereoMatchPt> &vecFeatMatchPt );
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 读取密集匹配点
//    * @param strLPath 左文件路径
//    * @param strRPath 右文件路径
//    * @param vecStereoPt 密集匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool ReadDenseMatchPts( string strLPath, string strRPath, vector<Pt3d> &vecStereoPt );
//    /**
//    * @fn
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 读取密集匹配点
//    * @param strLPath 左文件路径
//    * @param strRPath 右文件路径
//    * @param vecStereoPt 密集匹配点集
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool ReadDenseMatchPts( string strLPath, string strRPath, vector<StereoMatchPt> &vecStereoPt );
//    /**
//    * @fn WriteDenseMatchPts
//    * @date 2011.11.02
//    * @author 万文辉 whwan@irsa.ac.cn
//    * @brief 读取密集匹配点
//    * @param strLPath 左文件路径
//    * @param strRPath 右文件路径
//    * @param pStereoSet 立体像对影像信息
//    * @param vecFeatMatchPt 密集匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool WriteDenseMatchPts( string strLPath, string strRPath, StereoSet *pStereoSet, vector<StereoMatchPt> &vecFeatMatchPt );

//    /**
//    * @fn ReadDmfPts
//    * @date 2011.11.02
//    * @author 彭嫚 pengman@irsa.ac.cn
//    * @brief 读取密集匹配点
//    * @param strLPath 左文件路径
//    * @param strRPath 右文件路径
//    * @param vecFeatMatchPt 密集匹配点
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史：
//    * <作者>    <时间>   <版本编号>    <修改原因>\n
//    */
//    bool ReadDmfPts( string strLPath, string strRPath, vector<StereoMatchPt> &vecStereoPt );

    /**
    * @fn DisparityMap
    * @date 2011.11.02
    * @author 彭嫚 pengman@irsa.ac.cn
    * @brief 密集匹配点生成视差图
    * @param imgPtL 左文件密集匹配点
    * @param imgPtR 右文件密集匹配点
    * @param DstFile 视差图文件
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool DisparityMap( ImgPtSet& imgPtL, ImgPtSet& imgPtR, char* DstFile);

    CmlGdalDataset* m_pDataL;//!<左影像
    CmlGdalDataset* m_pDataR;//!<右影像

    vector<StereoMatchPt> m_vecFeatMatchPt;//!<特征匹配点
    vector<StereoMatchPt> m_vecDenseMatchPt;//!<密集匹配点

    /**
    * @fn Match2LargeImg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 两张大影像间的匹配(使用SIFT)
    * @param strL 左文件路径
    * @param strR 右文件路径
    * @param vecMatchPts 匹配点结果
    * @param stuSiftPara sift匹配参数
    * @param stuRANSACPara RANSAC参数
    * @param dRatio 辅助系数，即左影像宽高乘以此系数后同右影像匹配，以便更好的实现匹配。但此系数不影响输出结果。
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool Match2LargeImg( const SCHAR* strL, const SCHAR* strR, vector<StereoMatchPt> &vecMatchPts, SiftMatchPara stuSiftPara, RANSACAffineModPara stuRANSACPara, DOUBLE dRatio = 1.0, UINT nMaxBlockSize = 3000, UINT nMaxOverlap = 200, UINT nTilts = 8, bool bIsSIFTNorASIFT = true  );

    bool AffineSIFTMatch( const SCHAR* strL, const SCHAR* strR, UINT nNumTilts, vector<StereoMatchPt> &vecOutMPts );


     /**
    * @fn mlDenseMatchPyra
    * @date 2011.12.14
    * @author 彭嫚 pengman@irsa.ac.cn
    * @brief 建立金字塔影像密集匹配
    * @param pBlockL 左影像块
    * @param pBlockR 右影像块
    * @param vecMatchPt 影像匹配特征点
    * @param WidePara 匹配参数结构
    * @param vecLPts 密集匹配的左影像点坐标
    * @param vecRPts 密集匹配的右影像点坐标
    * @param nSize 建立金字塔的层数
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlDenseMatchPyra(CmlRasterBlock* pBlockL,  CmlRasterBlock* pBlockR,vector<StereoMatchPt> &vecMatchPt, WideOptions WidePara, \
                                 vector<Pt2d> &vecLPts, vector<Pt2d> &vecRPts,SINT nSize=2);
    /**
    * @fn mlMatchPtPyra
    * @date 2011.12.14
    * @author 彭嫚 pengman@irsa.ac.cn
    * @brief 根据原始影像提取特征点计算金字塔影像上特征点坐标
    * @param vecMatchPtIn 输入的原始影像匹配特征点
    * @param vecMatchPtOut 输出的金字塔影像匹配特征点
    * @param nSize 金字塔缩小倍数
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlMatchPtPyra(vector<StereoMatchPt> &vecMatchPtIn, vector<StereoMatchPt> &vecMatchPtOut, SINT nSize, SINT nCase);
protected:
private:

};

#endif // CMLSTEREOPROC_H
