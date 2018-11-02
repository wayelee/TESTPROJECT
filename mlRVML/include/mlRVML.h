#ifndef _MLRVML_H_
#define _MLRVML_H_

#include "mlTypes.h"
/**
* @fn mlSingleCamCalib
* @date 2012.2.21
* @author 吴凯 wukai@irsa.ac.cn
* @brief 单相机标定
* @param[in] vecImgPts 标志点像方坐标序列
* @param[in] vecObjPts 标志点物方坐标序列
* @param[in] nW 影像宽度
* @param[in] nH 影像高度
* @param[in] bFlag 判断是进行相机再标定操作还是检查点像方精度检查
* @param[out] inPara 相机内方位参数
* @param[out] exPara 相机外方位参数
* @param[out] vecError 标志点像方残差序列
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlSingleCamCalib(vector<Pt2d>& vecImgPts, const vector<Pt3d>& vecObjPts ,  SINT nW , SINT nH , InOriPara& inPara ,
                                ExOriPara& exPara , vector<Pt3d>& vecError , bool bFlag = 1);
/**
* @fn mlStereoCamCalib
* @date 2012.2.21
* @author 吴凯 wukai@irsa.ac.cn
* @brief 立体相机标定
* @param[in] vecLImgPts 标志点左相机像方坐标序列
* @param[in] vecRImgPts 标志点右相机像方坐标序列
* @param[in] vecObjPts 标志点物方坐标序列
* @param[in] nW 影像宽度
* @param[in] nH 影像高度
* @param[in] bFlag 判断是进行相机再标定操作还是检查点物方精度检查
* @param[out] inLPara 左相机内方位参数
* @param[out] inRPara 右相机内方位参数
* @param[out] exLPara 左相机外方位参数
* @param[out] exRPara 右相机外方位参数
* @param[out] exStereoPara 立体相机相对外方位参数
* @param[out] vecError 标志点物方残差序列
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlStereoCamCalib(const vector<Pt2d>& vecLImgPts , const vector<Pt2d>& vecRImgPts , vector<Pt3d>& vecObjPts ,
                                SINT nW , SINT nH , InOriPara& inLPara , InOriPara& inRPara , ExOriPara& exLPara , ExOriPara& exRPara , ExOriPara& exStereoPara , vector<Pt3d>& vecError , bool bFlag = 1);
/**
* @fn mlMonoSurvey
* @date 2012.2.21
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 单目量测
* @param[in] imgpts 标志点像方坐标
* @param[in] vecObjPts 标志点物方坐标
* @param[in] InOriInput 内方位元素
* @param[out] exOriOut 像片外方位元素
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlMonoSurvey(vector<Pt2d> imgpts, vector<Pt3d> vecObjPts, InOriPara& InOriInput, ExOriPara &exOriOut);
/**
* @fn mlWideBaseAnalysis
* @date 2011.12.1
* @author 彭嫚
* @brief 长基线制图最优基线分析
* @param[in] NavCamPara 导航相机参数
* @param[in] PanCamPara 全景相机参数
* @param[in] AnaPara 全景相机参数
* @param[out] OptiBase 计算的最优基线
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
MLAPI( bool )  mlWideBaseAnalysis(InOriPara NavCamPara, InOriPara PanCamPara, BaseOptions AnaPara,DOUBLE &OptiBase);
/**
* @fn mlWideBaseMapping
* @date 2011.12.1
* @author 彭嫚
* @brief 长基线制图
* @param[in] vecStereoSet 立体像对参数
* @param[in] WidePara 长基线匹配结构体参数
* @param[out] vecFPtSetL 输出的左影像特征点
* @param[out] vecFPtSetR 输出的右影像特征点
* @param[out] vecDPtSetL 输出的左影像密集点
* @param[out] vecDPtSetR 输出的右影像密集点
* @param[out] strDemFile 生成DEM文件路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
MLAPI( bool )  mlWideBaseMapping(vector<StereoSet> vecStereoSet, WideOptions WidePara,vector<ImgPtSet>& vecFPtSetL, vector<ImgPtSet>& vecFPtSetR, vector<ImgPtSet>& vecDPtSetL, vector<ImgPtSet>& vecDPtSetR, SCHAR *strDemFile);
/**
* @fn mlDenseMatch
* @date 2011.12.14
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 影像密集匹配
* @param[in] pStereoSet 立体像对
* @param[in] WidePara 匹配参数结构
* @param[out] vecFPtSetL 特征匹配的左影像点坐标
* @param[out] vecFPtSetR 特征匹配的右影像点坐标
* @param[out] vecDPtSetL 密集匹配的左影像点坐标
* @param[out] vecDPtSetR 密集匹配的右影像点坐标
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDenseMatch(StereoSet* pStereoSet, WideOptions WidePara, ImgPtSet& vecFPtSetL, ImgPtSet& vecFPtSetR, ImgPtSet& vecDPtSetL, ImgPtSet& vecDPtSetR);


/**
* @fn mlDenseMatchRegion
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
MLAPI( bool )  mlDenseMatchRegion(StereoSet* pStereoSet, GaussianFilterOpt GauPara, ExtractFeatureOpt ExtractPara, MatchInRegPara MatchPara, RANSACHomePara RanPara, \
                                  MLRectSearch RectSearch, WideOptions WidePara,  SINT Lx, SINT Ly, SINT ColRange, SINT RowRange, ImgPtSet& vecDPtSetL, ImgPtSet& vecDPtSetR, vector<Pt3d>& vecPtObj, vector<DOUBLE>& vecCorr);

/**
* @fn mlDisparityMap
* @date 2011.11.02
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 根据密集匹配点生成视差图
* @param[in] imgPtL 左影像密集匹配点
* @param[in] imgPtR 右影像密集匹配点
* @param[out] strDisFile 视差图所在路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDisparityMap(ImgPtSet& imgPtL, ImgPtSet& imgPtR, SCHAR *strDisFile);
 /**
 *@fn mlCoordTransResult
 *@date 2012.02
 *@author 张重阳
 *@brief 根据给定的旋转矩阵和平移向量求解转换后的坐标
 *@param[in] pArr 传入坐标数组指针
 *@param[in] strumat 转换关系结构体
 *@param[in] nflag 转换状态参数 默认为0
         对于转换关系 B = R*A + T (R为旋转矩阵，T为平移向量)
         当nflag=0时，表示由A求B；
         当nflag为其他值，表示由B求A
 *@param[out] pTransResult 坐标转换后结果
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：
 *<作者>    <时间>   <版本编号>    <修改原因>\n
 */
MLAPI( bool )  mlCoordTransResult(DOUBLE* pArr,TransMat strumat,DOUBLE* pTransResult,SINT nflag);

/**
* @fn mlSatMatch
* @date 2012.2.21
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像特征点匹配
* @param[in] sLimgPath 左影像路径
* @param[in] sRimgPath 右影像路径
* @param[in] satPara 匹配参数
* @param[in] nMethod 匹配方法
* @param[out] vecRanPts 匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlSatMatch(const string sLimgPath, const string sRimgPath, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, SINT nMethod = 0);

/**
* @fn mlGetLinearImgEop
* @date 2012.2.21
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 获取线阵影像的外方位
* @param[in] vecLineEo  影像线元素
* @param[in] vecAngleEo 影像角元素
* @param[in] vecImg_time 影像每行成像时间
* @param[out] vecEo 每行影像的外方位元素
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlGetLinearImgEop(vector<LineEo> &vecLineEo, vector<AngleEo> &vecAngleEo, vector<DOUBLE> &vecImg_time, vector<ExOriPara> *vecEo);

/**
* @fn mlSatMappingByPts
* @date 2012.2.21
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 由卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM
* @param[in] satproj 卫星影像DEM及DOM生成工程参数
* @param[in] satPara 卫星影像DEM及DOM生成参数
* @param[in] vecRanPts 匹配点
* @param[out] vecDensePts 密集匹配点及物方三维点
* @param[out]  vecPres  物方三维点精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlSatMappingByPts(SatProj &satproj, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, vector<StereoMatchPt> &vecDensePts, vector<Pt3d> &vecPres);
/**
* @fn mlMapByInteBlock
* @date 2012.2.21
* @author 万文辉
* @brief 由单站立体像对生成DEM和DOM
* @param[in] vecStereoSet 单站立体像对信息
* @param[in]  vecImgPtSets 立体像对对应的点信息，如存在数据则跳过匹配
* @param[in] ptLT 待生成范围左上角
* @param[in] ptRB 待生成范围右下角
* @param[in] dResolution DEM、DOM生成的分辨率
* @param[in] extractPtsOpts 特征点提取参数
* @param[in] matchOpts 匹配参数
* @param[in] ransacOpts 粗差剔除参数
* @param[out] strDemPath DEM生成路径
* @param[out] strDomPath Dom生成路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlMapByInteBlock(   vector<StereoSet> &vecStereoSet, vector< ImgPtSet > &vecImgPtSets, Pt2d ptLT, Pt2d ptRB, DOUBLE dResolution,\
                                        ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                                   string strDemPath, string strDomPath );

MLAPI( bool )  mlSiteBA( vector<StereoSet> vecStereoSetIn, vector< ImgPtSet > &vecImgPtSets, \
                            ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                           vector<StereoSet> &vecStereoOut );

/**
* @fn mlPanoMatchPts
* @date 2012.2.21
* @author 梁健
* @brief 生成原始图像间两两匹配点文件
* @param[in] vecParam 全景拼接参数
* @param[in] vecFrmInfo 原始图像图像信息
* @param[in] vecImgPtSets 原始图像对应点信息
* @param[out] strOutPath 输出匹配点文件路径
* @param[out] bNeedAddPts 输出是否人工加点标识
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )   mlPanoMatchPts( vector<char*> vecParam, vector<FrameImgInfo> vecFrmInfo, vector<ImgPtSet> &vecImgPtSets, char* strOutPath, bool &bNeedAddPts );

/**
* @fn mlPanoMosic
* @date 2012.2.21
* @author 梁健
* @brief 全景拼接函数
* @param[in] vecPara 全景拼接参数
* @param[out] vecFrmInfo 原始图像图像信息
* @param[out] vecImgPtSets 原始图像对应点信息
* @param[out] strOutPath 输出全景图像路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )   mlPanoMosic( vector<char*> vecPara, vector<FrameImgInfo> vecFrmInfo, vector< ImgPtSet > &vecImgPtSets, char* strOutPath );

/**
 *@fn mlTinSimply
 *@date 2012.02
 *@author 张重阳
 *@brief 不规则三角网简化
 *@param[in] vecPt3dIn 需要简化的三角网点序列
 *@param[in] simpleIndex 简化系数 取值为0到1之间
 *@param[out] vecPt3dOut 简化后的三角网点序列
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 * @par 修改历史:
 * <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlTinSimply(vector<Pt3d> &vecPt3dIn,vector<Pt3d> &vecPt3dOut,DOUBLE simpleIndex);
/**
 *@fn mlCalcTransMatrixByLatLong
 *@date 2011.11
 *@author 张重阳
 *@brief 本函数根据定位的经纬度实现月固系到局部坐标系转换关系求解
 *@param[in] dLat 定位的纬度  单位为度   范围为-90度～90度  北纬为正 南纬为负
 *@param[in] dLong 定位的经度 单位为度   范围为-180-180度  东经为正 西经为负
 *@param[out] tmat  存储转换的旋转矩阵与平移量
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：
 *<作者>    <时间>   <版本编号>    <修改原因>\n

*/
MLAPI( bool )  mlCalcTransMatrixByLatLong(DOUBLE dLat,DOUBLE dLong,TransMat& tmat);
/**
 *@fn mlCalcTransMatrixByXYZ
 *@date 2011.11
 *@author 张重阳
 *@brief 本函数实现月固系到局部坐标系转换关系求解
 *@param[in] dLocResult_x 着陆点在月固系下得精确定位结果X
 *@param[in] dLocResult_y 着陆点在月固系下得精确定位结果Y
 *@param[in] dLocResult_z 着陆点在月固系下得精确定位结果Z
 *@param[out] tmat  存储转换的旋转矩阵与平移量
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：
 *<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlCalcTransMatrixByXYZ(DOUBLE dLocResult_x,DOUBLE dLocResult_y,DOUBLE dLocResult_z,TransMat& tmat);
/**
 *@fn mlVisualImage
 *@date 2012.02
 *@author 张重阳
 *@brief 根据DEM、DOM生成指定视角下的仿真图像
 *@param[in] strDem DEM路径及文件名，geotiff文件格式，需包含起点坐标
 *@param[in] strDom DOM路径及文件名，geotiff文件格式，需包含起点坐标
 *@param[in] exori 指定视角外方位元素
 *@param[in] fx x方向焦距
 *@param[in] fy y方向焦距
 *@param[in] nImgWid 生成图像的宽
 *@param[in] nImgHei 生成图像的高
 *@param[out] outImg 输出文件名称
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：
 *<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlVisualImage( const SCHAR* strDem,const SCHAR* strDom,const SCHAR* outImg,ExOriPara exori,DOUBLE fx,DOUBLE fy,SINT nImgWid,SINT nImgHei);

/**
 *@fn mlPano2Prespective
 *@date 2012.02
 *@author 梁健
 *@brief 根据全景图像，对已知范围生成透视图像
 *@param[in] cInputPanoFile 输入的全景图像路径
 *@param[in] nOriginX 选取范围的左上角点X坐标
 *@param[in] nOriginY 选取范围的左上角点Y坐标
 *@param[in] nPanoRoiW 选取范围的宽度
 *@param[in] nPanoRoiH 选取范围的高度
 *@param[in] dFocus 焦距
 *@param[out] cOutputImage 输出的透视图像路径
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：
 *<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlPano2Prespective( const SCHAR *cInputPanoFile, const SCHAR * cOutputImage, SINT nOriginX, SINT nOriginY, SINT nPanoRoiW, SINT nPanoRoiH, DOUBLE dFocus);

/**
 *@fn mlDEMMosaic
 *@date 2012.02
 *@author 梁健
 *@brief dem拼接
 *@param[in] vecInputFiles 输入的原始dem
 *@param[in] dXResl 输出文件x方向分辨率
 *@param[in] dYResl 输出文件方向分辨率
 *@param[in] nResampleAlg 重采样算法（默认双线性插值)
 *@param[in] nDisCultLine 拼接线（默认为空）
 *@param[out] cOutputFile 输出文件路径
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：
 *<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDEMMosaic( vector<string> vecInputFiles, const SCHAR* cOutputFile, DOUBLE dXResl, DOUBLE dYResl, SINT nResampleAlg, SINT nDisCultLine);
/**
* @fn mlComputeInsightMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口，根据输入DEM数据和视点坐标，计算通视图
* @param[in] sInputDEM 输入DEM文件路径
* @param[in] nxLocation 视点x坐标
* @param[in] nyLocation 视点y坐标
* @param[in] dViewHight  视点距离地面的高度
* @param[in] InverseHeight 是否将高程反转
* @param[out] sDestDEM   输出文件路径
*@retval TRUE 成功
*@retval FALSE 失败
* @version 1.0
* @par 修改历史：
*@par 修改历史：
*<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeInsightMap( const SCHAR * sInputDEM, SINT nxLocation ,SINT nyLocation, DOUBLE dViewHight, const SCHAR * sDestDEM, bool InverseHeight);

/**
* @fn mlComputeSlopeMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，输出坡度
* @param[in] sInputDEM  输入DEM文件路径
* @param[in] nWindowSize 计算窗口大小
* @param[in] dZfactor 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
* @param[out] sDestDEM 输出文件路径
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeSlopeMap(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor );
/**
* @fn mlComputeSlopeAspectMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，输出坡向
* @param[in] sInputDEM  输入DEM文件路径
* @param[in] nWindowSize 计算窗口大小
* @param[in] dZfactor 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
* @param[out] sDestDEM 输出文件路径
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeSlopeAspectMap(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor );
/**
* @fn mlComputeBarrierMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，障碍图参数计算障碍图
* @param[in] sInputDEM  输入DEM文件路径
* @param[in]  nWindowSize 计算窗口大小
* @param[in]  dZfactor 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
* @param[in]  ObPara 障碍图参数结构体
* @param[out] sDestDEM 输出文件路径
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeBarrierMap(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor,ObstacleMapPara ObPara);
/**
* @fn mlComputeContourMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据和等高距，计算等高线图
* @param[in] dHinterval  等高距
* @param[in] strSrcfilename 输入的DEM文件路径
* @param[in] bCNodata 表示是否自定义Nodata值
* @param[in] dNodata 如果bCNodata设置为true，则dNodata 的值在计算时被当做无效值对待
* @param[in] strAttrib 生成shape文件高程的属性名称，默认为elev
* @param[out] strDstfilename 输出的shape文件路径
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeContourMap(DOUBLE dHinterval, SCHAR* strSrcfilename, SCHAR* strDstfilename ,bool bCNodata = false , DOUBLE dNodata = 0.0 ,SCHAR* strAttrib = "elev" );
/**
 *@fn mlGeoRasterCut
 *@date 2011.11
 *@author 张重阳
 *@brief 根据像素对DEM、DOM进行裁剪
 *@param[in] strFileIn 待裁剪的输入文件
 *@param[in]  pttl_x 裁剪左上角x坐标
 *@param[in]  pttl_y 裁剪左上角y坐标
 *@param[in]  ptbr_x 裁剪右下角x坐标
 *@param[in]  ptbr_y 裁剪右下角y坐标
 *@param[in] nflag 指定裁剪方式 1为按像素裁剪 2为按地理坐标裁剪
 *@param[in] nCutBands 指定裁剪波段 为负数时表示所有波段都裁剪 为正时裁剪特定的波段
 *@param[in] dZoom  采样系数 默认为1
 *@param[out] strFileOut 裁剪后输出文件
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：
 *<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlGeoRasterCut( const SCHAR* strFileIn,const SCHAR* strFileOut,DOUBLE pttl_x,DOUBLE pttl_y,DOUBLE ptbr_x,DOUBLE ptbr_y,SINT nflag, SINT nCutBands, DOUBLE dZoom);
/**
* @fn mlLocalByMatch
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 卫星影像和地面影像间匹配实现定位
* @param[in] strLandDom 地面影像文件路径
* @param[in] strSatDom 卫星影像匹配点坐标
* @param[in] LandImgPtset 地面影像匹配点坐标
* @param[in] SatImgPtset 卫星影像匹配点坐标
* @param[in] localByMOpts 匹配参数
* @param[out] ptLocalRes 定位结果
* @param[out]  dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlLocalByMatch( const SCHAR* strLandDom, const SCHAR* strSatDom,  ImgPtSet &LandImgPtset, ImgPtSet &SatImgPtset, LocalByMatchOpts localByMOpts, Pt2d ptCent, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );
/**
* @fn mlLocalByIntersection
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 多片后方交会实现定位
* @param[in] vecGCPs 控制点坐标
* @param[in] vecImgPtSets 像点坐标
* @param[out] ptLocalRes 定位结果
* @param[out] dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlLocalByIntersection( vector<Pt3d> vecGCPs, vector< ImgPtSet > &vecImgPtSets,  Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );
/**
* @fn mlLocalBySImgIntersection
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
MLAPI( bool )  mlLocalBySImgIntersection( vector<Pt3d> vecGCPs, ImgPtSet imgPts,  ExOriPara &exOriRes, vector<RMS2d> &vecRMSRes, DOUBLE &dTotalRMS  );
/**
* @fn mlLocalIn2Site
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 站点间影像定位
* @param[in] vecFrontSite 前站点所有影像
* @param[in] vecEndSite 后站点所有影像
* @param[in] vecFrontPts 前站点所有像点
* @param[in] vecEndPts 后站点所有像点
* @param[in] localBy2Opt 定位方法参数
* @param[out] ptLocalRes 定位结果
* @param[out] dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlLocalIn2Site( vector<StereoSet> vecFrontSite, vector<StereoSet> vecEndSite, vector<ImgPtSet> &vecFrontPts, vector<ImgPtSet> &vecEndPts, LocalBy2SitesOpts localBy2Opt, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );
/**
* @fn mlLocalBySeqence
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief LocalInSequenceImg 序列影像定位
* @param[in] FrameInfoSet 序列影像路径及信息
* @param[in] strSatDom 卫星影像路径
* @param[in] frmPts 序列影像点集
* @param[in] SatPts 卫星影像点集
* @param[in] stuLocalBySeqOpts 定位方法参数
* @param[out] ptLocalRes 定位结果
* @param[out] dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlLocalBySeqence(  FrameImgInfo FrameInfoSet, const SCHAR* strSatDom, ImgPtSet &frmPts, ImgPtSet &SatPts, LocalBySeqImgOpts stuLocalBySeqOpts, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );
/**
* @fn mlLocalByLander
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 利用着陆器的定位
* @param[in] vecStereoSet 站点所有立体影像
* @param[in] vecGCPs 特征点三维坐标
* @param[in] vecImgPts 匹配结果
* @param[in] stuLocalByLanderOpts 定位参数
* @param[out] vecLocalRes 定位结果
* @param[out] vecLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlLocalByLander(  vector<StereoSet> vecStereoSet, vector<Pt3d> vecGCPs, vector<ImgPtSet> &vecImgPts, LocalByLanderOpts stuLocalByLanderOpts, vector<Pt3d> &vecLocalRes, vector<DOUBLE> &vecLocalAccuracy );
/**
* @fn mlSmoothByGaussian
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像去噪高斯滤波
* @param[in] strFileIn 输入原始影像路径
* @param[in] nTemplateSize 滤波模板大小
* @param[in] dCoef 滤波核参数,一般以0.8为宜
* @param[out] strFileOut 输出滤波后影像路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlSmoothByGaussian(const SCHAR* strFileIn, SINT nTemplateSize, DOUBLE dCoef, const SCHAR* strFileOut );
/**
* @fn mlGetEpipolarImg
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 生成核线影像
* @param[in] pStereoSet 立体像对信息
* @param[out] strFileOutL 左影像核线影像路径
* @param[out] inOriL 左核线影像校正后内方位
* @param[out] exOriL 左核线影像校正后外方位
* @param[out] strFileOutR 右影像核线影像路径
* @param[out] inOriR 左核线影像校正后内方位
* @param[out] exOriR 右核线影像校正后外方位

* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlGetEpipolarImg( StereoSet* pStereoSet,
                                        const SCHAR* strFileOutL,
                                        InOriPara &inOriL,
                                        ExOriPara &exOriL,
                                        const SCHAR* strFileOutR,
                                        InOriPara &inOriR,
                                        ExOriPara &exOriR,
                                        CAMTYPE nLCamType = Nav_Cam,
                                        CAMTYPE nRCamType = Nav_Cam,
                                        DOUBLE dZoomCoef = 1.0  );

/**
* @fn mlSift
* @date 2011.12.16
* @author 彭嫚  pengman@irsa.ac.cn
* @brief sift匹配功能
* @param[in] pStereoSet 立体像对信息
* @param[in] WidePara 匹配参数结构体
* @param[out] vecFLPts 左片匹配点
* @param[out] vecFRPts 右片匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlSift(StereoSet* pStereoSet, SiftMatchPara siftPara, RANSACAffineModPara ransacPara, vector<Pt2d> &vecFLPts, vector<Pt2d> &vecFRPts);

/**
* @fn mlASift
* @date 2011.12.16
* @author 彭嫚  pengman@irsa.ac.cn
* @brief Asift匹配功能
* @param[in] pStereoSet 立体像对信息
* @param[out] strFileOut 匹配点输出路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlASift(StereoSet* pStereoSet, ASiftMatchPara asiftPara, RANSACAffineModPara ransacPara, vector<Pt2d> &vecFLPts, vector<Pt2d> &vecFRPts);

/**
* @fn mlTemplateMatch
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 立体图像间模板匹配
* @param[in] pLeftImg 左影像路径
* @param[in] pRightImg 右影像路径
* @param[in] vecPtL 左影像待匹配点
* @param[in] vecPtR 右影像待匹配点
* @param[in] MatchPara 模板匹配参数
* @param[out] vecMatchPts 输出匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlTemplateMatchImg( const SCHAR* pLeftImg, const SCHAR* pRightImg, vector<Pt2i> &vecPtL, vector<Pt2i> &vecPtR,\
                                       MatchInRegPara MatchPara, vector<StereoMatchPt> &vecFeatMatchPts);
/**
* @fn mlStereoMatch
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 立体图像间匹配并输出相应三维点等
* @param[in] pStereo 立体影像路径
* @param[in] extractOpts 兴趣点提取方法
* @param[in] matchOpts 匹配参数
* @param[in] ransacOpts RANSAC参数
* @param[out] imgLPts 左影像特征匹配点
* @param[out] imgRPts 右影像特征匹配点
* @param[out] vec3dPts 输出相应的三维坐标
* @param MatchPara 模板匹配参数
* @param vecMatchPts 输出匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlStereoMatchInFrmImg( StereoSet* pStereo, ExtractFeatureOpt extractOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, \
                                  ImgPtSet &imgLPts, ImgPtSet &imgRPts, vector<Pt3d> &vec3dPts );
/**
* @fn mlLSMatchImg
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵影像最小二乘匹配，根据左影像上一点取得右影像上同名点
* @param[in] pLeftImg 左影像路径
* @param[in] pRightImg 右影像路径
* @param[in] dLx 左影像匹配点X
* @param[in] dLy 左影像匹配点Y
* @param[in,out] dRx 右影像待匹配点X
* @param[in,out] dRy 右影像待匹配点Y
* @param[in] nTempSize 匹配窗口大小
* @param[in] dCoef 相关系数阈值
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlLSMatchImg(SCHAR* pLeftImg, SCHAR* pRightImg, DOUBLE dLx, DOUBLE dLy, DOUBLE& dRx, DOUBLE& dRy, SINT nTempSize, DOUBLE& dCoef);


///**
//* @fn mlDOMGeneration
//* @date 2011.11.02
//* @author 万文辉 whwan@irsa.ac.cn
//* @brief DOM生成
//* @param[in] vec3dPts  三维匹配点
//* @param[in] strPath DEM路径
//* @param[in] dbDemRect DEM生成范围
//* @param[in] dResolution 生成分辨率
//* @version 1.0
//* @retval TRUE 成功
//* @retval FALSE 失败
//* @par 修改历史：
//* <作者>    <时间>   <版本编号>    <修改原因>\n
//*/
//MLAPI( bool )  mlDEMGeneration( vector<Pt3d> &vec3dPts, const SCHAR* strPath, DbRect dbDemRect, DOUBLE dResolution );

///**
//* @fn mlDOMGeneration
//* @date 2011.11.02
//* @author 万文辉 whwan@irsa.ac.cn
//* @brief DOM生成
//* @param[in] vecStereoSets  待采样的立体影像
//* @param[in] strDEMPath DEM路径
//* @param[in] strDOMPath DOM路径
//
//* @version 1.0
//* @retval TRUE 成功
//* @retval FALSE 失败
//* @par 修改历史：
//* <作者>    <时间>   <版本编号>    <修改原因>\n
//*/
//MLAPI( bool )  mlDOMGeneration( vector<StereoSet> &vecStereoSets, const SCHAR* strDEMPath, const SCHAR* strDOMPath );
/**
* @fn mlRansacPts
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 利用Ransac方法剔除立体匹配点粗差
* @param[in,out] MatchPts 立体匹配点
* @param[in] dThresh 阈值
* @param[in] dConfidence 置信度
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlRansacPts( vector<StereoMatchPt> &MatchPts, DOUBLE dThresh, DOUBLE dConfidence);
/**
* @fn GmlGetNewStereoPtID
* @date 2012.02.10
* @author  万文辉 whwan@irsa.ac.cn
* @brief 获取匹配点的编号
* @param[in] clsLPts 左影像信息
* @param[in] clsRPts 右影像信息
* @param[out] lID 匹配点的编号
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlGetNewStereoPtID( ImgPtSet &clsLPts, ImgPtSet &clsRPts, ULONG &lID );
/**
* @fn mlGetNewSinglePtID
* @date 2012.02.10
* @author  万文辉 whwan@irsa.ac.cn
* @brief 获取单个点的编号
* @param[in] clsImgPts 影像信息
* @param[out] lID 匹配点的编号
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlGetNewSinglePtID( ImgPtSet &clsPts, ULONG &lID );


//////////////////////////////////////////////////////////////////////////////////
/**
* @fn mlSetLogFilePath
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 设置日志文件路径，开启日志模式
* @param[in] strFileName 日志路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlSetLogFilePath(char *strFileName);

/**
* @fn mlCloseLogFile
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 关闭日志文件模式
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlCloseLogFile( );

MLAPI( bool )  mlExOriTrans( ExOriPara* pExOriL, ExOriPara* pExOriRela, ExOriPara* pExOriR );

MLAPI( bool )  mlGetRelaOri( ExOriPara* pExOriL, ExOriPara* pExOriR, ExOriPara* pExOriRela );

MLAPI( bool )  mlGetOPKAngle( DOUBLE *pRMat, DOUBLE *pOPK );

MLAPI( bool )  mlGetRMatByOPK( DOUBLE *pOPK, DOUBLE *pRMat );

MLAPI( bool )  mlSemiAutoMatchInRegion( const SCHAR* strPathL, const SCHAR* strPathR, Pt2i ptL, MatchInRegPara matchPara, Pt2i &ptR, DOUBLE &dCoef );


MLAPI( bool )  mlCalcNewAngle( OriAngle oriA, DOUBLE dRollAngle );
////////////////////////////////////////////////////////////////////////////////////
/**
* @fn mlWallisFilter
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief Wallis滤波方法
* @param[in] strInPath  输入图像路径
* @param[in] strOutPath  输出图像路径
* @param[in] fPara  滤波参数
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlWallisFilter( const SCHAR* strInPath, const SCHAR* strOutPath, WallisFPara fPara );
/**
* @fn mlGuassFilter
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 高斯滤波方法
* @param[in] strInPath  输入图像路径
* @param[in] strOutPath  输出图像路径
* @param[in] nTemplateSize  滤波参数
* @param[in] dCoef  滤波参数
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlGuassFilter(  const SCHAR* strInPath, const SCHAR* strOutPath,  SINT nTemplateSize, DOUBLE dCoef );

/**
* @fn mlHistogramEqualize
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 直方图均衡化
* @param[in] strInPath  输入图像路径
* @param[in] strOutPath  输出图像路径
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlHistogramEqualize(  const SCHAR* strInPath, const SCHAR* strOutPath );
/**
* @fn mlGrayTensile
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 直方图拉伸
* @param[in] strInPath  输入图像路径
* @param[in] strOutPath  输出图像路径
* @param[in] nMin  拉伸后最小灰度
* @param[in] nMax  拉伸后最大灰度
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlGrayTensile(   const SCHAR* strInPath, const SCHAR* strOutPath, UINT nMin = 0, UINT nMax = 255 );
/**
* @fn mlExtractFeatPtByForstner
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像Forstner方法提取特征点
* @param[in] pImg  输入影像路径
* @param[out] m_vecFeaPts 提取的特征点
* @param[in] nGridSize 格网大小
* @param[in] nPtNum 欲提取的点数
* @param[in] dCoef 系数阈值
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlExtractFeatPtByForstner( const SCHAR* pImg, vector<Pt2i>& vecFeaPts, SINT nGridSize, SINT nPtNum = 0, DOUBLE dCoef = 1.0, bool bIsRemoveAbPixel = false );

/**
* @fn mlUnDistortForNavPanCam
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 全景、导航相机畸变改正结果
* @param[in] ptIn  输入二维点
* @param[in] nHeight 图像高度
* @param[in] inOri 内方位元素
* @param[out] ptOutRes 畸变改正结果
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlUnDistort( Pt2d ptIn, UINT nHeight, InOriPara inOri, Pt2d &ptOutRes, CAMTYPE nCamType = Nav_Cam );

MLAPI( bool )  mlUnDistortImg( const SCHAR* strIn, InOriPara inOri, const SCHAR* strOut, CAMTYPE camType = Nav_Cam, DOUBLE dRatio = 1.0 );
///**
//* @fn mlUnDistortForNavPanCam
//* @date 2011.11.02
//* @author 万文辉 whwan@irsa.ac.cn
//* @brief 避障相机畸变改正结果
//* @param[in] ptIn  输入二维点
//* @param[in] nHeight 图像高度
//* @param[in] inOri 内方位元素
//* @param[out] ptOutRes 畸变改正结果
//* @version 1.0
//* @retval TRUE 成功
//* @retval FALSE 失败
//* @par 修改历史：
//* <作者>    <时间>   <版本编号>    <修改原因>\n
//*/
//MLAPI( bool )  mlUnDistortForHazCam( Pt2d ptIn, UINT nHeight, InOriPara inOri, Pt2d &ptOutRes );

MLAPI( bool ) mlCreateImg( const SCHAR* strPath, UINT nW, UINT nH, ImgDotType imgType, void* pData = NULL );

MLAPI( bool ) mlCreateGeoImg( const SCHAR* strPath, UINT nW, UINT nH, Pt2d ptOrig, DOUBLE dXRes, DOUBLE dYRes, ImgDotType imgType, void* pData = NULL );
/**
* @fn mlExtractFeatPtByForstnerWithSRegion
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像Forstner方法提取特征点
* @param[in] pImg  输入影像路径
* @param[out] m_vecFeaPts 提取的特征点
* @param[in] vecDisableRange 失效区域集合
* @param[in] nGridSize 格网大小
* @param[in] nPtNum 欲提取的点数
* @param[in] dCoef 系数阈值
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlExtractFeatPtByForstnerWithSRegion( const SCHAR* pImg, vector<Pt2i>& vecFeaPts, vector<MLRect> vecDisableRange, SINT nGridSize, SINT nPtNum = 0, DOUBLE dCoef = 1.0 );


MLAPI( bool )  mlExtractFeatPtByCanny( const SCHAR* pImg, vector<Pt2i> &vecFeatPts, DOUBLE dThres1, DOUBLE dThres2 );
/**
* @fn mlTemplateMatchInFeatPts
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像Forstner方法提取特征点
* @param[in] pLImg  输入影像路径
* @param[in] pRImg  输入影像路径
* @param[in] vecLPts  左影像特征点
* @param[in] vecRPts  右影像特征点
* @param[out] m_vecFeaPts 提取的特征点
* @param[in] vecDisableRange 失效区域集合
* @param[in] nGridSize 格网大小
* @param[in] nPtNum 欲提取的点数
* @param[in] dCoef 系数阈值
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlTemplateMatchInFeatPts( const SCHAR* pLImg, const SCHAR* pRImg, vector<Pt2i> vecLPts, vector<Pt2i> vecRPts, vector<StereoMatchPt> &vecSMPts, MatchInRegPara matchPara, bool bIsRemoveAbPixel = false );

//特征点左右搜索，并相互验证;方法1表示为左右各自验证的匹配，方法2表示左片对右片的全局搜索匹配，方法三表示右片对左片的全局搜索, 方法四表示精化搜索匹配方法
MLAPI( bool )  mlTemplateMatchInFeatPtsVT( const SCHAR* pLImg, const SCHAR* pRImg, vector<Pt2i> vecLPts, vector<Pt2i> vecRPts, vector<StereoMatchPt> &vecSMPts, MatchInRegPara matchPara, UINT nMethod = 1, bool bIsRemoveAbPixel = false);


/**
* @fn mlTemplateMatchInFeatPts
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像Forstner方法提取特征点
* @param[in] pLImg  输入影像路径
* @param[in] pRImg  输入影像路径
* @param[in] vecLPts  左影像特征点
* @param[in] vecRPts  右影像特征点
* @param[out] m_vecFeaPts 提取的特征点
* @param[in] vecDisableRange 失效区域集合
* @param[in] nGridSize 格网大小
* @param[in] nPtNum 欲提取的点数
* @param[in] dCoef 系数阈值
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlTemplateMatchInFeatPtsWithGivenPts( const SCHAR* pLImg, const SCHAR* pRImg, Pt2d ptL, Pt2d ptR, UINT nRange, UINT nTemplateS, \
                                                     Pt2d &ptRes, DOUBLE &dCoefRes, DOUBLE dCoefThres = -1.0 );



/**
* @fn mlSift
* @date 2011.12.16
* @author 彭嫚  pengman@irsa.ac.cn
* @brief sift匹配功能
* @param[in] strLPath 左影像信息
* @param[in] strRPath 右影像信息
* @param[in] dRatio 匹配阈值系数
* @param[in] nMaxBlockSize 分块最大尺寸
* @param[in] nOverLaySize 块间重叠大小
* @param[out] vecOutRes 匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlSiftMatch( const SCHAR* strLPath, const SCHAR* strRPath, DOUBLE dRatio, UINT nMaxBlockSize, UINT nOverLaySize, vector<StereoMatchPt> &vecOutRes );

MLAPI( double* ) mlSiftMatchVT( const SCHAR* strLPath, const SCHAR* strRPath, DOUBLE dRatio, DOUBLE dbRansacSigma, UINT nMaxBlockSize, UINT nOverLaySize, int &nPtSize );

MLAPI( bool )  mlFreeSiftPts( double *pPts );

MLAPI( bool )  mlSiftMatchWHomo( const SCHAR* strLPath, const SCHAR* strRPath, DOUBLE dRatio, vector<StereoMatchPt> &vecOutRes );
/**
* @fn mlDenseMatchRegion
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
MLAPI( bool )  mlDenseMatchRegion(StereoSet* pStereoSet, GaussianFilterOpt GauPara, ExtractFeatureOpt ExtractPara, MatchInRegPara MatchPara, RANSACHomePara RanPara, \
                                  MLRectSearch RectSearch, WideOptions WidePara,  SINT Lx, SINT Ly, SINT ColRange, SINT RowRange, ImgPtSet& vecDPtSetL, ImgPtSet& vecDPtSetR, vector<Pt3d>& vecPtObj, vector<DOUBLE>& vecCorr);
/**
* @fn mlDenseMatchInRegion
* @date 2011.12.14
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 影像指定区域密集匹配
* @param[in] strInPutLImgPath 左影像
* @param[in] strInPutLImgPath 右影像
* @param[in] vecFeatMPts 特征点点集
* @param[in] nStep 匹配步长
* @param[in] nRadiusX 搜索X方向半径长度
* @param[in] nRadiusY 搜索Y方向半径长度
* @param[in] nXOff X方向搜索位置偏移量
* @param[in] nYOff Y方向搜索位置偏移量
* @param[in] nTemplateSize 模板大小
* @param[in] dCoefThres 相关系数阈值
* @param[in] rect 左影像上待匹配的范围
* @param[out] vecOutMRes 匹配结果
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDenseMatchInRegion( const SCHAR* strInPutLImgPath, const SCHAR* strInPutRImgPath, vector<StereoMatchPt> vecFeatMPts, UINT nStep, UINT nRadiusX, UINT nRadiusY, SINT nXOff, SINT nYOff, \
                                     UINT nTemplateSize, DOUBLE dCoefThres, MLRect rect, vector<StereoMatchPt> &vecOutMRes, bool bIsRemoveAbPixel = false );


MLAPI( bool )  mlDenseMatchInAdaptRegion( const SCHAR* strInPutLImgPath, const SCHAR* strInPutRImgPath, vector<StereoMatchPt> vecFeatMPts, UINT nRadiusX, UINT nRadiusY, SINT nXOff, SINT nYOff, \
                                     UINT nTemplateSize, DOUBLE dCoefThres, stuBlockDeal stuBDData, vector<StereoMatchPt> &vecOutMRes, bool bIsRemoveAbPixel = false );
/**
* @fn mlRansacPtsByHomo
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 采用单应矩阵模型剔除粗差
* @param[in] vecStereoPts  匹配点对结果
* @param[in] dThres  剔除方法阈值
* @param[in] dConfidence  剔除方法阈值
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlRansacPtsByHomo( vector<StereoMatchPt> &vecStereoPts, DOUBLE dThresh = 3, DOUBLE dConfidence = 0.99 );

MLAPI( bool )  mlRansacPtsByHomoVT( vector<StereoMatchPt> vecStereoPts, vector<StereoMatchPt> &vecOutStereoPts, DOUBLE dThresh = 3, DOUBLE dConfidence = 0.99 );

/**
* @fn mlRansacPtsByAffineT
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 采用仿射变化模型剔除粗差
* @param[in] vecStereoPts  匹配点对结果
* @param[in] dSegma  剔除方法阈值
* @param[in] dMaxError  剔除方法阈值
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlRansacPtsByAffineT( vector<StereoMatchPt> vecStereoInPts, DOUBLE dSegma, DOUBLE dMaxError, vector<StereoMatchPt> &vecStereoOutPts );

MLAPI( bool )  mlFilterPtsByMedian( vector<StereoMatchPt> &MatchPts, SINT nTemplateSize = 5, DOUBLE dThresCoef = 0.1, DOUBLE dThres = 5 );
/**
* @fn mlLsMatch
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵影像最小二乘匹配，根据左影像上一点取得右影像上同名点
* @param strPathL 左影像路径
* @param strPathR 右影像路径
* @param ptL 左影像匹配点
* @param nTempSize 匹配窗口大小
* @param ptR 右影像匹配点
* @param dCoef 相关系数阈值
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlLsMatch( const SCHAR* strPathL, const SCHAR* strPathR, Pt2d ptL, UINT nTemplateSize, Pt2d &ptR, DOUBLE &dCoef );

/**
* @fn mlInterSection
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 不含畸变校正功能的通用空间前方交会函数
* @param ptL 左匹配点
* @param ptR 右匹配点
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
MLAPI( bool )  mlIntersection(  Pt2d ptL, Pt2d ptR, Pt3d &ptXYZ, \
                          InOriPara* pInOriL, ExOriPara* pExOriL, InOriPara* pInOriR, ExOriPara* pExOriR, DOUBLE dThres = 0.0001 );

MLAPI( bool )  mlIntersectionWithDis( Pt2d ptL, Pt2d ptR, SINT nHeightL, SINT nHeightR, Pt3d &ptXYZ, InOriPara* pInOriL, ExOriPara* pExOriL, InOriPara* pInOriR, ExOriPara* pExOriR, DOUBLE dThres = 0.0001 );

MLAPI( bool )  mlIntersectionVT(  Pt2d ptL, Pt2d ptR,  ExOriPara exOriL, ExOriPara exOriR, DOUBLE df1, DOUBLE df2, Pt3d &ptXYZ, DOUBLE dCoefConf = 0.0001 );

MLAPI( bool )  mlIntersectionWithAccu(  Pt2d ptL, Pt2d ptR, DOUBLE dCoefConf, DOUBLE df1, DOUBLE df2, ExOriPara exOriL, ExOriPara exOriR, \
                                        Pt3d &ptXYZ, Pt3d &ptOutXYZAccu, DOUBLE &dOutTotalAccu );
MLAPI( bool )  mlResectionNoInitialVal(  vector<Pt3d> vecGCPs, vector<Pt2d> vecImgPts, DOUBLE dFocalLength, ExOriPara &exOriRes );


MLAPI( bool )  mlResection( Pt2d *pImgPt,Pt3d *pGroundPt, double fx,double fy, SINT nPtNum,ExOriPara *pInitExOripara, ExOriPara *pExOripara );

MLAPI( bool )  mlSolvePts( vector<Pt3d> vecOldPts, vector<Pt3d> vecNewPts, UINT nTimes, ExOriPara* pInitialOri );
/**
* @fn mlRelaOriCalcWithOrigPts
* @date 2012.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 相对定向
* @param vecMatchPts 左、右匹配点，含畸变
* @param inOriL 左内参数
* @param inOriR 右内参数
* @param exOriRela 相对定向参数，左影像为标准
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlRelaOriCalcWithOrigPts( vector<StereoMatchPt> vecMatchPts, InOriPara inOriL, UINT nHL, InOriPara inOriR, UINT nHR, ExOriPara &exOriRela  );
/**
* @fn mlSiteFindTiePoints
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief DOM生成
* @param[in] vecStereoImg  待平差影像对信息
* @param[out] vecTiePoints 连接点信息
* @param[in] extractPtsOpt 特征点提取参数
* @param[in] matchOpts 匹配参数
* @param[in] ransacOpts ransac参数
* @param[in] mFilterOpts 中值滤波参数
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlSiteFindTiePoints( vector<StereoSet> vecStereoImg, vector<ImgPtSet> &vecTiePoints, ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts );


/**
* @fn mlBASolve
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief DOM生成
* @param[in] vecStereoIn  输入影像对的初始信息
* @param[in] vecImgPtSets 连接点信息
* @param[out] vecStereoOut 输出平差计算后的信息
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/

MLAPI( bool )  mlBASolve( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<StereoSet> &vecStereoOut, UINT nModel = 2 );

MLAPI( bool )  mlBASolveWithErr( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<StereoSet> &vecStereoOut, Pt2d &ptProjErr, UINT nModel = 2 );

MLAPI( bool )  mlBASolveVT( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<bool> vecbImgIsFixed, vector<StereoSet> &vecStereoOut, UINT nModel = 1 );
/**
* @fn mlDOMGeneration
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief DOM生成
* @param[in] vec3dPts  三维匹配点
* @param[in] strPath DEM路径
* @param[in] dbDemRect DEM生成范围
* @param[in] dResolution 生成分辨率
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDEMGenerationNoRect( vector<Pt3d> &vec3dPts, const SCHAR* strPath, DOUBLE dResolution, ImgDotType imgType, string strTriFile = ""  );

MLAPI( bool )  mlDEMGeneration( vector<Pt3d> &vec3dPts, const SCHAR* strPath, DbRect dbDemRect, DOUBLE dResolution, ImgDotType imgType, string strTriFile = "" );

MLAPI( bool )  mlDEMGenerationInRegion( vector< vector< Pt3d> > &vec3dPts, const SCHAR* strPath, DbRect dbDemRect, DOUBLE dResolution, ImgDotType imgType, string strTriFile = "" );

/**
* @fn mlDOMGeneration
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief DOM生成
* @param[in] vecStereoSets  待采样的立体影像
* @param[in] strDEMPath DEM路径
* @param[in] strDOMPath DOM路径

* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDOMGeneration( vector<StereoSet> &vecStereoSets, const SCHAR* strDEMPath, const SCHAR* strDOMPath, ImgDotType imgType );
/**
* @fn mlPlaneNormal
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面的法向量
* @param[in] vecXYZ  三维点集
* @param[out] pPlaneNormal1 法向量1
* @param[out] pPlaneNormal2 法向量2
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlPlaneNormal( vector<Pt3d> vecXYZ, Pt3d &pPlaneNormal );

/**
* @fn mlCalcLandPtCoord
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面的法向量
* @param[in] ptObImgCentxy 观测图上站点的图像坐标
* @param[in] vecStereoMPts 匹配点信息，左像点为观测图，右像点为底图
* @param[out] ptLandPosInBMap 站心在底图上的图像坐标
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlCalcLandPtCoord( Pt2d ptObImgCentxy, vector<StereoMatchPt> vecStereoMPts, Pt2d &ptLandPosInBMap, DOUBLE &dPrecision );

/**
* @fn mlDealSiteImgSelection
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 选择前后站的视线相同的两队像对
* @param[in] vecSiteOne 第一站左影像外方位集合
* @param[in] vecSiteTwo 第二站左影像外方位集合
* @param[out] nIDOne 第一站影像集合中所选的影像序号
* @param[out] nIDTwo 第二站影像集合中所选的影像序号
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDealSiteImgSelectionV( vector<StereoSet> vecSiteOne, vector<StereoSet> vecSiteTwo, UINT &nIDOne, UINT &nIDTwo );

MLAPI( bool )  mlDealSiteImgSelection( vector<StereoSet> vecSiteOne, vector<StereoSet> vecSiteTwo, StereoSet &sFSiteSet, StereoSet &sSSiteSet );

MLAPI( bool )  mlDealSiteImgSelectionReverse( vector<StereoSet> vecSiteOne, vector<StereoSet> vecSiteTwo, StereoSet &sFSiteSet, StereoSet &sSSiteSet );


MLAPI( bool )  mlSiteSIFTMatch( StereoSet sFirstSet, StereoSet sSecondSet, UINT nNumTilts, vector<Pt2d> &vecPtsInSiteOneLImg, vector<Pt2d> &vecPtsInSiteTwoLImg, DOUBLE dDisIn2SiteThres = 3, DOUBLE dCamHeight = 1.5 );

MLAPI( bool )  mlSiteSIFTMatchReverse( StereoSet sFirstSet, StereoSet sSecondSet, UINT nNumTilts, vector<Pt2d> &vecPtsInSiteOneLImg, vector<Pt2d> &vecPtsInSiteTwoLImg, DOUBLE dDisIn2SiteThres = 3, DOUBLE dCamHeight = 1.5 );

MLAPI( bool )  mlProjMap( StereoSet sFirstSet, StereoSet sSecondSet, DOUBLE dCamHeight, DOUBLE dRes, DOUBLE dRange, const SCHAR* strLProjMap, const SCHAR* strRProjMap );

MLAPI( bool )  mlSiteMatchByProjMap( StereoSet sFirstSet, StereoSet sSecondSet, DOUBLE dCamHeight, DOUBLE dRes, DOUBLE dRange, const SCHAR* strLProjMap, const SCHAR* strRProjMap, \
                                     vector<StereoMatchPt> vecInitSMPts, vector<Pt2d> &vecPtsInSiteOneLImg, vector<Pt2d> &vecPtsInSiteTwoLImg );

MLAPI( bool )  mlAffineSIFT( const SCHAR* strLImgPath, const SCHAR* strRImgPath, UINT nNumTilts, vector<Pt2d> &vecPtsInSiteOneLImg, vector<Pt2d> &vecPtsInSiteTwoLImg );

MLAPI( bool )  mlStereoMatchWithPtSet( const SCHAR* strL, const SCHAR* strR, vector<Pt2d> vecLPts, MatchInRegPara matchPara, vector<Pt2d> &vecRPts, vector<bool> &vecFlags );

MLAPI( bool )  mlGetMatchedPtsInTwoSites(   vector<Pt2d> vecPtsInSiteOneLImg,    //第一站左图像所得到的点坐标，由ASIFT得到
                                            vector<Pt2d> vecPtsInSiteOneRImg,    //第一站右图像所得到的点坐标，由左影像坐标根据相关系数得到
                                            vector<bool> vecValidFlagInSiteOne,  // 由于相关系数不一定成功，故由匹配时输出的匹配情况（见步骤四）在此进行输入，以判断哪些左影像点是存在对应右影像匹配点的

                                            vector<Pt2d> vecPtsInSiteTwoLImg,    //同上，为第二站的情况，均为输入
                                            vector<Pt2d> vecPtsInSiteTwoRImg,
                                            vector<bool> vecValidFlagInSiteTwo,

                                            vector<StereoMatchPt> &vecSMPtsInSiteOne,  //第一站中的匹配点对
                                            vector<StereoMatchPt> &vecSMPtsInSiteTwo  //第二站中的匹配点对，其中，其与第一站中的点对存在对应关系，即 两个数组同一序号 表示 四点 同名
                                        );

MLAPI( bool ) mlDeletePtsByDisConsist( Pt3d ptCentXYZSiteOne, vector<Pt3d> vecPtXYZInSiteOne, Pt3d ptCentXYZSiteTwo, vector<Pt3d> vecPtXYZInSiteTwo, \
                                       DOUBLE dDisThres, DOUBLE dWeightThres, vector<bool> &vecFlags );

MLAPI( bool ) mlCalcSiteOirByStereoMPts( vector<StereoMatchPt> vecMatchedPtsInSiteOne,   //第一站匹配点对
                                        vector<StereoMatchPt> vecMatchedPtsInSiteTwo,   //第二站匹配点对，同vecMatchedPtsInSiteOne是一一对应的同名点关系
                                        StereoSet imgInSiteOne,           //输入第一站影像的内、外方位元素
                                        StereoSet imgInSiteTwo,           //输入第2站影像的内、外方位元素
                                        ExOriPara  exOriSiteTwoOrgin,      //输入第2站中 探测车 初始的 外方位（由于影像外方位不同于车的外方位，故通过前后影像外方位的变化改正 车的外方位）

                                        ExOriPara  &exOriSiteTwoModefied,  //最终所需要的车的外方位结果
                                        Pt3d      &rmsXYZ,                //定位XYZ中误差
                                        DOUBLE    &dTotalRMS,             //总中误差
                                        CAMTYPE  nCamTypeFirst = Nav_Cam,
                                        CAMTYPE  nCamTypeSecond = Nav_Cam
                                        );

MLAPI( bool ) mlCalcSiteOirByStereoMPtsVT( vector<StereoMatchPt> vecMatchedPtsInSiteOne,   //第一站匹配点对
                                        vector<StereoMatchPt> vecMatchedPtsInSiteTwo,   //第二站匹配点对，同vecMatchedPtsInSiteOne是一一对应的同名点关系
                                        StereoSet imgInSiteOne,           //输入第一站影像的内、外方位元素
                                        StereoSet imgInSiteTwo,           //输入第2站影像的内、外方位元素
                                        ExOriPara  exOriSiteTwoOrgin,      //输入第2站中 探测车 初始的 外方位（由于影像外方位不同于车的外方位，故通过前后影像外方位的变化改正 车的外方位）

                                        ExOriPara  &exOriSiteTwoModefied,  //最终所需要的车的外方位结果
                                        Pt3d      &rmsXYZ,                //定位XYZ中误差
                                        DOUBLE    &dTotalRMS,             //总中误差
                                        CAMTYPE  nCamTypeFirst = Nav_Cam,
                                        CAMTYPE  nCamTypeSecond = Nav_Cam
                                        );

MLAPI( bool ) mlMatchByCentPtLSM( const SCHAR* strL, const SCHAR* strR, Pt2d ptL, Pt2d ptGivenR, UINT nRange, UINT nTemplateSize, DOUBLE dCoefThres, \
                                  Pt2d &ptOutRes, DOUBLE &dCoef );

MLAPI( bool ) mlRegionDenseMatch( const SCHAR* strInPutLImgPath, const SCHAR* strInPutRImgPath, vector<StereoMatchPt> vecFeatMPts, UINT nStep, UINT nRadiusX, UINT nRadiusY, SINT nXOff, SINT nYOff, \
                                  UINT nTemplateSize, DOUBLE dCoefThres, DOUBLE dXmid, DOUBLE dYmid, UINT nHeight, UINT nWidth, vector<StereoMatchPt> &vecOutMRes  );
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

MLAPI( bool ) mlCalcCamOriInWorldByGivenInsMat( stuInsMat matBase, DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
                                         stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                         ExOriPara &exOriCamL, ExOriPara &exOriCamR );


MLAPI( bool ) mlCalcHazCamInWorld( stuInsMat matLCamBase2Body, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
                                    ExOriPara &exOriCamL, ExOriPara &exOriCamR );

MLAPI( bool ) mlCalcHazCamInBodyVT( stuInsMat matLCamCap2Body, stuInsMat matLCamCap2RCamCap, ExOriPara &exOriCamL, ExOriPara &exOriCamR );

MLAPI( bool ) mlPt3dTrans( Pt3d ptXYZOrig, ExOriPara exOri, Pt3d &ptXYZRes );

MLAPI( bool ) mlPt3dTransBody2Cam( Pt3d ptXYZOrig, ExOriPara exOri, Pt3d &ptXYZRes );

MLAPI( bool ) mlGeoFileTrans( const SCHAR* pImgIn, const SCHAR* pImgOut, ImgDotType imgType );

/**
* @fn mlDenseMatchPyra
* @date 2011.12.14
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 影像金字塔密集匹配
* @param[in] strInPutLImgPath 左影像
* @param[in] strInPutLImgPath 右影像
* @param[in] vecFeatMPts 特征点点集
* @param[in] nStep 匹配步长
* @param[in] nRadiusX 搜索X方向半径长度
* @param[in] nRadiusY 搜索Y方向半径长度
* @param[in] nXOff X方向搜索位置偏移量
* @param[in] nYOff Y方向搜索位置偏移量
* @param[in] nTemplateSize 模板大小
* @param[in] dCoefThres 相关系数阈值.000000
* @param[in] nlevel 金字塔层数
* @param[out] vecOutMRes 匹配结果
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDenseMatchPyra( const SCHAR* strInPutLImgPath, const SCHAR* strInPutRImgPath, vector<StereoMatchPt> vecFeatMPts, UINT nStep, UINT nRadiusX, UINT nRadiusY, SINT nXOff, SINT nYOff, \
                                        UINT nTemplateSize, DOUBLE dCoefThres, vector<StereoMatchPt> &vecOutMRes, SINT nlevel = 2 );



MLAPI( bool )  mlGeoRasterVerify( const SCHAR* strDemIn,  const SCHAR* strDomIn,  const SCHAR* strDemOut );

MLAPI( bool )  mlGetGeoInfo( const SCHAR* strGeoFile, Pt2d &ptOrig, DOUBLE &dXRes, DOUBLE &dYRes, UINT &nW, UINT &nH, DOUBLE &dNoDataVal );

MLAPI( bool )  mlSetGeoInfo( const SCHAR* strGeoFile, Pt2d ptOrig, DOUBLE dXRes, DOUBLE dYRes, DOUBLE dNoDataVal );

MLAPI( bool )  mlGeoMergeInRegion( const SCHAR* strBaseMap, const SCHAR* strToProceedMap, const SCHAR* strOutRes, vector<Polygon2d> vecPolygon );

MLAPI( bool )  mlGeoMerge( const SCHAR* strBaseMap, const SCHAR* strToProceedMap, const SCHAR* strOutRes, bool bIsDem = true );

MLAPI( bool )  mlConvertGeoFileYRes( const SCHAR* strSrcFile, const SCHAR* strDstFile );

MLAPI( bool )  mlCalcDisparityPerRow( DOUBLE dUnitDis, DOUBLE dCamHeight, UINT nImgH, UINT nMinStep, UINT nMaxStep, UINT nBlockSize, DOUBLE df, ExOriPara exOriL, stuBlockDeal &stuBDRes );

MLAPI( bool )  mlCalcMinXOff( DOUBLE dCamHeight, UINT nImgH, DOUBLE df, DOUBLE dBaseLength, ExOriPara exOriL, SINT &nXOffMin );

MLAPI( bool )  mlGeoFileEnLargeUnit( const SCHAR* strInGeoFile, const SCHAR* strOutGeoFile, DOUBLE dRatio = 1000 );

MLAPI( bool )  mlPt2dInDifImg( Pt2d ptOrig, UINT nOriHeight, InOriPara inOri1, ExOriPara exOri1, CAMTYPE nCamType, UINT nNewHeight, InOriPara inOri2, ExOriPara exOri2, Pt2d &ptRes );

MLAPI( bool )  mlPt3dProjInOrigImg( Pt3d ptXYZ, InOriPara inOri, ExOriPara exOri, UINT nH, CAMTYPE nCamType, Pt2d &ptRes );
//------------------------------------------------------------------------------
MLAPI( bool )  mlFindMatchHoleInImg( vector<Pt2i> vecMatchedPts, UINT nW, UINT nH, UINT nHoleRange, vector<Polygon2d> &vecHolePolys );

//-----------------------------------------------------------------------------
MLAPI( bool )  mlGetGeoFilePtr( const SCHAR* strGeoFile, void* &geoDataPtr );
MLAPI( bool )  mlGetGeoZ( void* geoDataPtr, Pt2d ptXY, DOUBLE &dZ );
MLAPI( bool )  mlGetGeoZByIdx( void* geoDataPtr, UINT nX, UINT nY, Pt3d &ptXYZ );
MLAPI( bool )  mlFreeGeoPtr( void* &geoDataPtr );

MLAPI( bool )  mlJPToGeoTiff( const SCHAR* strJPG, const SCHAR *strOutTiff, Pt2d ptOrig, DOUBLE dXRes, DOUBLE dYRes, DOUBLE dNoDataVal );





















#endif


