#ifndef _MLRVML_H_
#define _MLRVML_H_
#include "mlBase.h"
#include "mlTypes.h"
/**
* @fn mlSingleCamCalib
* @date 2012.2.21
* @author 吴凯 wukai@irsa.ac.cn
* @brief 单相机标定
* @param vecImgPts 标志点像方坐标序列
* @param vecObjPts 标志点物方坐标序列
* @param nW 影像宽度
* @param nH 影像高度
* @param inPara 相机内方位参数
* @param exPara 相机外方位参数
* @param vecError 标志点像方残差序列
* @param bFlag 判断是进行相机再标定操作还是检查点像方精度检查
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
* @param vecLImgPts 标志点左相机像方坐标序列
* @param vecRImgPts 标志点右相机像方坐标序列
* @param vecObjPts 标志点左相机物方坐标序列
* @param vecObjPts 标志点右相机物方坐标序列
* @param nW 影像宽度
* @param nH 影像高度
* @param inLPara 左相机内方位参数
* @param inRPara 右相机内方位参数
* @param exLPara 左相机外方位参数
* @param exRPara 右相机外方位参数
* @param exStereoPara 立体相机相对外方位参数
* @param vecError 标志点物方残差序列
* @param bFlag 判断是进行相机再标定操作还是检查点物方精度检查
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
* @param imgpts 标志点像方坐标
* @param InOriInput 内方位元素
* @param exOriOut 像片外方位元素
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
* @param NavCamPara,导航相机参数
* @param PanCamPara,全景相机参数
* @param AnaPara,全景相机参数
* @param OptiBase,计算的最优基线
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
* @param vecStereoSet,立体像对
* @param WidePara，长基线匹配结构体参数
* @param vecFPtSetL，输出的左影像特征点
* @param vecFPtSetR，输出的右影像特征点
* @param vecDPtSetL，输出的左影像密集点
* @param vecDPtSetR，输出的右影像密集点
* @param strDemFile，生成DEM文件路径
* @param strDomFile，生成DOM文件路径
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
MLAPI( bool )  mlDenseMatch(StereoSet* pStereoSet, WideOptions WidePara, ImgPtSet& vecDPtSetL, ImgPtSet& vecDPtSetR);
/**
* @fn mlDisparityMap
* @date 2011.11.02
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 根据密集匹配点生成视差图
* @param imgPtL 左影像密集匹配点
* @param imgPtR 右影像密集匹配点
* @param strDisFile 视差图所在路径
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
 *@param pArr 传入坐标数组指针
 *@param strumat 转换关系结构体
 *@param pTransResult 坐标转换后结果
 *@param nflag 转换状态参数 默认为0
         对于转换关系 B = R*A + T (R为旋转矩阵，T为平移向量)
         当nflag=0时，表示由A求B；
         当nflag为其他值，表示由B求A
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/
MLAPI( bool )  mlCoordTransResult(DOUBLE* pArr,TransMat strumat,DOUBLE* pTransResult,SINT nflag);

/**
* @fn mlSatMatch
* @date 2012.2.21
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像特征点匹配
* @param sLimgPath 左影像路径
* @param sRimgPath 右影像路径
* @param satPara 匹配参数
* @param vecRanPts 匹配点
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
* @param vecLineEo  影像线元素
* @param vecAngleEo 影像角元素
* @param vecImg_time 影像每行成像时间
* @param vecE 每行影像的外方位元素
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
* @param satProj 卫星影像DEM及DOM生成工程参数
* @param satPara 卫星影像DEM及DOM生成参数
* @param vecRanPts 匹配点
* @param vecDensePts 密集匹配点及物方三维点
* @param vecPres  物方三维点精度
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
* @param vecStereoSet 单站立体像对信息
* @param vecImgPtSets 立体像对对应的点信息，如存在数据则跳过匹配
* @param ptLT 待生成范围左上角
* @param ptRB 待生成范围右下角
* @param dResolution DEM、DOM生成的分辨率
* @param strDemPath DEM生成路径
* @param strDomPath Dom生成路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
MLAPI( bool )  mlMapByInteBlock(   vector<StereoSet> &vecStereoSet, vector< ImgPtSet > &vecImgPtSets, Pt2d ptLT, Pt2d ptRB, DOUBLE dResolution,
                                   string strDemPath, string strDomPath );

/**
* @fn mlPanoMatchPts
* @date 2012.2.21
* @author 梁健
* @brief 生成原始图像间两两匹配点文件
* @param vecParam 全景拼接参数
* @param vecFrmInfo 原始图像图像信息
* @param vecImgPtSets 原始图像对应点信息
* @param strOutPath 输出匹配点文件路径
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
* @param vecParam 全景拼接参数
* @param vecFrmInfo 原始图像图像信息
* @param vecImgPtSets 原始图像对应点信息
* @param strOutPath 输出全景图像路径
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
 *@param vecPt3dIn 需要简化的三角网点序列
 *@param vecPt3dOut 简化后的三角网点序列
 *@param simpleIndex 简化系数 取值为0到1之间
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlTinSimply(vector<Pt3d> &vecPt3dIn,vector<Pt3d> &vecPt3dOut,DOUBLE simpleIndex);
/**
 *@fn mlCalcTransMatrixByLatLong
 *@date 2011.11
 *@author 张重阳
 *@brief 本函数根据定位的经纬度实现月固系到局部坐标系转换关系求解
 *@param dLat 定位的纬度  单位为度   范围为-90度～90度  北纬为正 南纬为负
 *@param dLong 定位的经度 单位为度   范围为-180-180度  东经为正 西经为负
 *@param tmat  存储转换的旋转矩阵与平移量
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/
MLAPI( bool )  mlCalcTransMatrixByLatLong(DOUBLE dLat,DOUBLE dLong,TransMat& tmat);
/**
 *@fn mlCalcTransMatrixByXYZ
 *@date 2011.11
 *@author 张重阳
 *@brief 本函数实现月固系到局部坐标系转换关系求解
 *@param dLocResult_x 着陆点在月固系下得精确定位结果X
 *@param dLocResult_y 着陆点在月固系下得精确定位结果Y
 *@param dLocResult_z 着陆点在月固系下得精确定位结果Z
 *@param tmat  存储转换的旋转矩阵与平移量
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/
MLAPI( bool )  mlCalcTransMatrixByXYZ(DOUBLE dLocResult_x,DOUBLE dLocResult_y,DOUBLE dLocResult_z,TransMat& tmat);
/**
 *@fn mlVisualImage
 *@date 2012.02
 *@author 张重阳
 *@brief 根据DEM、DOM生成指定视角下的仿真图像
 *@param strDem DEM路径及文件名，geotiff文件格式，需包含起点坐标
 *@param strDom DOM路径及文件名，geotiff文件格式，需包含起点坐标
 *@param outImg 输出文件名称
 *@param ExOriPara 指定视角外方位元素
 *@param fx，fy 焦距
 *@param nImgWid nImgHei 生成图像的宽、高
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlVisualImage( const SCHAR* strDem,const SCHAR* strDom,const SCHAR* outImg,ExOriPara exori,DOUBLE fx,DOUBLE fy,SINT nImgWid,SINT nImgHei);

/**
 *@fn mlPano2Prespective
 *@date 2012.02
 *@author 梁健
 *@brief 根据全景图像，对已知范围生成透视图像
 *@param cInputPanoFile 输入的全景图像路径
 *@param cOutputImage 输出的透视图像路径
 *@param nOriginX 选取范围的左上角点X坐标
 *@param nOriginY 选取范围的左上角点Y坐标
 *@param nPanoRoiW 选取范围的宽度
 *@param nPanoRoiH 选取范围的高度
 *@param dFocus 焦距
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlPano2Prespective( const SCHAR *cInputPanoFile, const SCHAR * cOutputImage, SINT nOriginX, SINT nOriginY, SINT nPanoRoiW, SINT nPanoRoiH, DOUBLE dFocus);

/**
 *@fn mlDEMMosaic
 *@date 2012.02
 *@author 梁健
 *@brief dem拼接
 *@param vecInputFiles 输入的原始dem
 *@param cOutputFile 输出文件路径, DOUBLE , DOUBLE , SINT , SINT
 *@param dXResl 输出文件x方向分辨率
 *@param dYResl 输出文件方向分辨率
 *@param nResampleAlg 重采样算法（默认双线性插值)
 *@param nDisCultLine 拼接线（默认为空）
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlDEMMosaic( vector<string> vecInputFiles, const SCHAR* cOutputFile, DOUBLE dXResl, DOUBLE dYResl, SINT nResampleAlg, SINT nDisCultLine);
/**
* @fn mlComputeInsightMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口，根据输入DEM数据和视点坐标，计算通视图
* @param sInputDEM,  输入DEM文件路径
* @param nxLocation, 视点x坐标
* @param nyLocation, 视点y坐标
* @param ViewHight，  视点距离地面的高度
* @param sDestDEM，   输出文件路径
* @param InverseHeight， 是否将高程反转
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeInsightMap( const SCHAR * sInputDEM, SINT nxLocation ,SINT nyLocation, DOUBLE dViewHight, const SCHAR * sDestDEM, bool InverseHeight);

/**
* @fn mlComputeSlopeMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，输出坡度
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
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
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
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
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
* @param ObPara: 障碍图参数结构体
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
* @param dHinterval,  等高距
* @param strSrcfilename, 输入的DEM文件路径
* @param strDstfilename, 输出的shape文件路径
* @param bCNodata， 表示是否自定义Nodata值
* @param dNodata， 如果bCNodata设置为true，则dNodata 的值在计算时被当做无效值对待
* @param cAttrib，生成shape文件高程的属性名称，默认为elev
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
 *@param strFileIn 待裁剪的输入文件
 *@param strFileOut 裁剪后输出文件
 *@param pttl 裁剪左上角点
 *@param ptbr 裁剪右下角点
 *@param nWidth nHeight 裁剪宽 高
 *@param nflag 指定裁剪方式 1为按像素裁剪 2为按地理坐标裁剪
 *@param nCutBands 指定裁剪波段 为负数时表示所有波段都裁剪 为正时裁剪特定的波段
 *@param dZoom采样系数 默认为1
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlGeoRasterCut( const SCHAR* strFileIn,const SCHAR* strFileOut,DOUBLE pttl_x,DOUBLE pttl_y,DOUBLE ptbr_x,DOUBLE ptbr_y,SINT nflag, SINT nCutBands, DOUBLE dZoom);
/**
* @fn mlLocalByMatch
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 卫星影像和地面影像间匹配实现定位
* @param strLandDom 地面影像文件路径
* @param strSatDom 卫星影像匹配点坐标
* @param LandImgPtset 地面影像匹配点坐标
* @param SatImgPtset 卫星影像匹配点坐标
* @param localByMOpts 匹配参数
* @param ptLocalRes 定位结果
* @param dLocalAccuracy 定位精度
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlLocalByMatch( const SCHAR* strLandDom, const SCHAR* strSatDom,  ImgPtSet &LandImgPtset, ImgPtSet &SatImgPtset, LocalByMatchOpts localByMOpts, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );
/**
* @fn mlLocalByIntersection
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
MLAPI( bool )  mlLocalByIntersection( vector<Pt3d> vecGCPs, vector< ImgPtSet > &vecImgPtSets,  Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy );
/**
* @fn mlLocalIn2Site
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 站点间影像定位
* @param vecFrontSite 前站点所有影像
* @param vecEndSite 后站点所有影像
* @param vecFrontPts 前站点所有像点
* @param vecEndPts 后站点所有像点
* @param localBy2Opt 定位方法参数
* @param ptLocalRes 定位结果
* @param dLocalAccuracy 定位精度
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
* @param FrameInfoSet 序列影像路径及信息
* @param strSatDom 卫星影像路径
* @param frmPts 序列影像点集
* @param SatPts 卫星影像点集
* @param stuLocalBySeqOpts 定位方法参数
* @param ptLocalRes 定位结果
* @param dLocalAccuracy 定位精度
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
* @param vecStereoSet 站点所有立体影像
* @param vecGCPs 特征点三维坐标
* @param vecImgPts 匹配结果
* @param stuLocalByLanderOpts 定位参数
* @param vecLocalRes 定位结果
* @param vecLocalAccuracy 定位精度
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
* @param nTemplateSize 滤波模板大小
* @param dCoef 滤波核参数,一般以0.8为宜
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlSmoothByGaussian(const SCHAR* strFileIn, SINT nTemplateSize, DOUBLE dCoef );
/**
* @fn mlGetEpipolarImg
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 生成核线影像
* @param pStereoSet 立体像对信息
* @param strFileOutL 左影像核线影像路径
* @param strFileOutR 右影像核线影像路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlGetEpipolarImg(StereoSet* pStereoSet, const SCHAR* strFileOutL, const SCHAR* strFileOutR);

/**
* @fn mlSift
* @date 2011.12.16
* @author 彭嫚  pengman@irsa.ac.cn
* @brief sift匹配功能
* @param pStereoSet 立体像对信息
* @param WidePara 匹配参数结构体
* @param strFileOutL 匹配点输出路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlSift(StereoSet* pStereoSet, WideOptions WidePara,const SCHAR* strFileOut);

/**
* @fn mlASift
* @date 2011.12.16
* @author 彭嫚  pengman@irsa.ac.cn
* @brief Asift匹配功能
* @param pStereoSet 立体像对信息
* @param strFileOutL 匹配点输出路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlASift(StereoSet* pStereoSet, const SCHAR* strFileOut);

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
MLAPI( bool )  mlTemplateMatchImg(SCHAR* pLeftImg, SCHAR* pRightImg, vector<Pt2i> &vecPtL, \
                                  vector<Pt2i> &vecPtR, vector<StereoMatchPt> &vecFeatMatchPts, MLRect rectSearch, SINT nTemplateSize, \
                                  DOUBLE dCoef, SINT nXOffSet, SINT nYOffSet);

/**
* @fn mlLSMatchImg
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵影像最小二乘匹配，根据左影像上一点取得右影像上同名点
* @param pLeftImg 左影像路径
* @param pRightImg 右影像路径
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
MLAPI( bool )  mlLSMatchImg(SCHAR* pLeftImg, SCHAR* pRightImg, DOUBLE dLx, DOUBLE dLy, DOUBLE& dRx, DOUBLE& dRy, SINT nTempSize, DOUBLE& dCoef);
/**
* @fn mlExtractFeatPtByForstner
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像Forstner方法提取特征点
* @param pImg  输入影像路径
* @param m_vecFeaPts 提取的特征点
* @param nGridSize 格网大小
* @param nPtNum 欲提取的点数
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlExtractFeatPtByForstner( SCHAR* pImg, vector<Pt2i> m_vecFeaPts, SINT nGridSize, SINT nPtNum = 0);

/**
* @fn mlRansacPts
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 利用Ransac方法剔除立体匹配点粗差
* @param MatchPts 立体匹配点
* @param dThresh 阈值
* @param dConfidence 置信度
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlRansacPts( vector<StereoMatchPt> &MatchPts, DOUBLE dThresh, DOUBLE dConfidence);
/*
* @fn GmlGetNewStereoPtID
* @date 2012.02.10
* @author  万文辉 whwan@irsa.ac.cn
* @brief 获取匹配点的编号
* @param clsLPts 左影像信息
* @param clsRPts 右影像信息
* @param lID 匹配点的编号
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
* @param clsImgPts 影像信息
* @param lID 匹配点的编号
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
* @param strFileName 日志路径
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

#endif


