/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlRVML.cpp
* @date 2011.2.1
* @author 万文辉 whwan@irsa.ac.cn
* @brief 接口函数源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "../../include/mlRVML.h"

#include "mlGdalDataset.h"
#include "mlWBaseProc.h"
#include "mlCoordTrans.h"
#include "mlCamCalib.h"
#include "mlDemProc.h"
#include "mlFrameImage.h"
#include "mlGeoRaster.h"
#include "mlLinearImage.h"
#include "mlLocalization.h"
#include "mlPhgProc.h"
#include "mlSatMapping.h"
#include "mlSiteMapping.h"
#include "mlStereoProc.h"
#include "mlTIN.h"
#include "mlWBaseProc.h"
#include "mlDemAnalyse.h"
#include "mlRasterMosaic.h"
#include "mlBA.h"

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
ML_EXTERN_C bool mlSingleCamCalib(vector<Pt2d>& vecImgPts , const vector<Pt3d>& vecObjPts ,  SINT nW , SINT nH , InOriPara& inPara ,
                                  ExOriPara& exPara , vector<Pt3d>& vecError , bool bFlag)
{
    CmlCamCalib camCalib ;
    if(!camCalib.mlSingleCamCalib(vecImgPts , vecObjPts , nW , nH , inPara , exPara , vecError , bFlag))
    {
        return false ;
    }
    else
    {
        return true ;
    }
}

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
ML_EXTERN_C bool mlStereoCamCalib(const vector<Pt2d>& vecLImgPts , const vector<Pt2d>& vecRImgPts , vector<Pt3d>& vecObjPts ,
                                  SINT nW , SINT nH , InOriPara& inLPara , InOriPara& inRPara , ExOriPara& exLPara , ExOriPara& exRPara , ExOriPara& exStereoPara , vector<Pt3d>& vecError , bool bFlag)
{
    CmlCamCalib camCalib ;
    if(!camCalib.mlStereoCamCalib(vecLImgPts , vecRImgPts , vecObjPts , nW , nH , inLPara , inRPara , exLPara , exRPara , exStereoPara , vecError , bFlag))
    {
        return false ;
    }
    else
    {
        return true ;
    }
}

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
ML_EXTERN_C bool  mlMonoSurvey(vector<Pt2d> imgpts, vector<Pt3d> vecObjPts, InOriPara& InOriInput, ExOriPara &exOriOut)
{
    CmlCamCalib clsCamCalib;
    bool bflag;
    bflag = clsCamCalib.backProjection(imgpts, vecObjPts, InOriInput, exOriOut);
    return bflag;
}

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
ML_EXTERN_C bool mlWideBaseAnalysis(InOriPara NavCamPara, InOriPara PanCamPara, BaseOptions AnaPara,double &OptiBase)
{
    CmlWBaseProc wb;
    bool bflag;
    bflag = wb.WideBaseAnalysis(NavCamPara, PanCamPara, AnaPara, OptiBase);
    return bflag;

}

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
ML_EXTERN_C bool mlWideBaseMapping(vector<StereoSet> vecStereoSet, WideOptions WidePara,vector<ImgPtSet>& vecFPtSetL, vector<ImgPtSet>& vecFPtSetR, vector<ImgPtSet>& vecDPtSetL, vector<ImgPtSet>& vecDPtSetR, SCHAR *strDemFile)
{
    CmlWBaseProc wb;
    return wb.WideBaseMapping(vecStereoSet,WidePara,vecFPtSetL, vecFPtSetR, vecDPtSetL, vecDPtSetR, strDemFile);
}

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
ML_EXTERN_C bool mlDenseMatch(StereoSet* pStereoSet, WideOptions WidePara, ImgPtSet& vecFPtSetL, ImgPtSet& vecFPtSetR, ImgPtSet& vecDPtSetL, ImgPtSet& vecDPtSetR)
{
    CmlSiteMapping clsSiteMapping;
    return clsSiteMapping.GetStereoDensePts(pStereoSet, WidePara, vecFPtSetL, vecFPtSetR, vecDPtSetL, vecDPtSetR);

}

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
ML_EXTERN_C bool mlDenseMatchRegion(StereoSet* pStereoSet, GaussianFilterOpt GauPara, ExtractFeatureOpt ExtractPara, MatchInRegPara MatchPara, RANSACHomePara RanPara, MLRectSearch RectSearch, WideOptions WidePara, SINT Lx, SINT Ly, SINT ColRange, SINT RowRange, ImgPtSet& vecDPtSetL, ImgPtSet& vecDPtSetR, vector<Pt3d>& vecPtObj, vector<DOUBLE>& vecCorr)
{
    CmlSiteMapping clsSiteMapping;
    return clsSiteMapping.GetRegionDensePts(pStereoSet, GauPara, ExtractPara, MatchPara, RanPara, RectSearch, WidePara, Lx, Ly, ColRange, RowRange, vecDPtSetL, vecDPtSetR, vecPtObj, vecCorr);
}

ML_EXTERN_C bool mlDenseMatchInRegion( const SCHAR* strInPutLImgPath, const SCHAR* strInPutRImgPath, vector<StereoMatchPt> vecFeatMPts, UINT nStep, UINT nRadiusX, UINT nRadiusY, SINT nXOff, SINT nYOff, \
                                       UINT nTemplateSize, DOUBLE dCoefThres, MLRect rect, vector<StereoMatchPt> &vecOutMRes, bool bIsRemoveAbPixel )
{
    CmlStereoProc clsStereoProc;
    CmlFrameImage clsImgL, clsImgR;

    if( ( false == clsImgL.LoadFile( strInPutLImgPath ) )||( false == clsImgR.LoadFile( strInPutRImgPath )) )
    {
        return false;
    }
    WideOptions wOpts;
    wOpts.nStep = nStep;
    wOpts.nRadiusX = nRadiusX;
    wOpts.nRadiusY = nRadiusY;
    wOpts.nXOffSet = nXOff;
    wOpts.nYOffSet = nYOff;
    wOpts.nTemplateSize = nTemplateSize;
    wOpts.dCoef = dCoefThres;

    vector<Pt2d> vecOutLRes, vecOutRRes;
    vector<DOUBLE> vecOutCorr;
    if( false == clsStereoProc.mlDenseMatch( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, vecFeatMPts, wOpts, rect, vecOutLRes, vecOutRRes, vecOutCorr, bIsRemoveAbPixel ) )
    {
        return false;
    }

    for( UINT i = 0; i < vecOutLRes.size(); ++i )
    {
        StereoMatchPt ptCur;
        ptCur.ptLInImg = vecOutLRes[i];
        ptCur.ptRInImg = vecOutRRes[i];
        ptCur.dRelaCoef = vecOutCorr[i];
        vecOutMRes.push_back( ptCur );
    }
    return true;
}

ML_EXTERN_C bool mlDenseMatchInAdaptRegion( const SCHAR* strInPutLImgPath, const SCHAR* strInPutRImgPath, vector<StereoMatchPt> vecFeatMPts, UINT nRadiusX, UINT nRadiusY, SINT nXOff, SINT nYOff, \
                                     UINT nTemplateSize, DOUBLE dCoefThres, stuBlockDeal stuBDData, vector<StereoMatchPt> &vecOutMRes, bool bIsRemoveAbPixel )
{
    CmlStereoProc clsStereoProc;
    CmlFrameImage clsImgL, clsImgR;

    if( ( false == clsImgL.LoadFile( strInPutLImgPath ) )||( false == clsImgR.LoadFile( strInPutRImgPath )) )
    {
        return false;
    }
    WideOptions wOpts;

    wOpts.nRadiusX = nRadiusX;
    wOpts.nRadiusY = nRadiusY;
    wOpts.nXOffSet = nXOff;
    wOpts.nYOffSet = nYOff;
    wOpts.nTemplateSize = nTemplateSize;
    wOpts.dCoef = dCoefThres;

    vector<Pt2d> vecOutLRes, vecOutRRes;
    vector<DOUBLE> vecOutCorr;
    MLRect rect;

    for( UINT i = 0; i < stuBDData.vecnDisp.size(); ++i )
    {
        vecOutLRes.clear();
        vecOutRRes.clear();
        vecOutCorr.clear();

        rect.dXMin = 0;
        rect.dXMax = clsImgL.GetWidth()-1;
        rect.dYMin = i*stuBDData.nBlockH;
        if( i == (stuBDData.vecnDisp.size() - 1) )
        {
            rect.dYMax = clsImgL.GetHeight() - 1;
        }
        if( false == clsStereoProc.mlDenseMatch( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, vecFeatMPts, wOpts, rect, vecOutLRes, vecOutRRes, vecOutCorr, bIsRemoveAbPixel ) )
        {
            continue;
        }
        for( UINT j = 0; j < vecOutLRes.size(); ++j )
        {
            StereoMatchPt ptCur;
            ptCur.ptLInImg = vecOutLRes[j];
            ptCur.ptRInImg = vecOutRRes[j];
            ptCur.dRelaCoef = vecOutCorr[j];
            vecOutMRes.push_back( ptCur );
        }
    }

    return true;
}
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
ML_EXTERN_C bool mlDisparityMap(ImgPtSet& imgPtL, ImgPtSet& imgPtR, SCHAR *strDisFile)
{
    CmlStereoProc clsStereoProc;
    return clsStereoProc.DisparityMap(imgPtL, imgPtR, strDisFile);
}
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
ML_EXTERN_C bool mlSetLogFilePath(SCHAR *strFileName)
{
    return g_clsLog.Open( strFileName, true );
}

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
ML_EXTERN_C bool mlCloseLogFile( )
{
    return g_clsLog.Close();
}

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
//ML_EXTERN_C bool mlCoordTrans(SCHAR *strFileIn,SCHAR *strFileOut)
//{
//    CmlCoordTrans cls;
//    // cls.mlCoordTrans(strFileIn,strFileOut);
//}

//ML_EXTERN_C bool mlCamCalib(const SCHAR *strFileIn,const SCHAR *strFileOut)
//{
//    CmlCamCalib cls;
//    cls.mlCamCalib(strFileIn,strFileOut);
//    return true ;
//}

//ML_EXTERN_C bool mlImgErrorCheck(const SCHAR* m_strCheckPtsFile , const SCHAR *strFileOut , const SCHAR* m_strCamInfoFile)
//{
//    CmlCamCalib cls;
//    cls.mlImgErrorCheck(m_strCheckPtsFile,strFileOut , m_strCamInfoFile);
//    return true ;
//}
//
//MLAPI( bool )mlStereoCalib(const SCHAR *strFileIn, const SCHAR *strFileOut)
//{
//    CmlCamCalib cls;
//    cls.mlStereoCalib(strFileIn,strFileOut);
//    return true ;
//}

//ML_EXTERN_C bool mlObjErrorCheck(const SCHAR* m_strCheckPtsFile , const SCHAR *strFileOut , const SCHAR* m_strCamInfoFile)
//{
//    CmlCamCalib cls;
//    cls.mlObjErrorCheck(m_strCheckPtsFile,strFileOut , m_strCamInfoFile);
//    return true ;
//}

//ML_EXTERN_C bool mlDemProc(DbRect dbDemRect , DOUBLE dDemRes ,SCHAR *strFileOut , bool bBlockFlag)
//{
//    CmlDemProc cls;
//    cls.DemProc(dbDemRect,dDemRes ,strFileOut , bBlockFlag);
//}

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
ML_EXTERN_C bool mlSatMatch(const string sLimgPath, const string sRimgPath, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, SINT nMethod)
{
    CmlSatMapping cls;
    bool H = cls.mlSatMatch(sLimgPath,sRimgPath,satPara,vecRanPts, nMethod );
    return H;
}


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
ML_EXTERN_C bool mlGetLinearImgEop(vector<LineEo> &vecLineEo, vector<AngleEo> &vecAngleEo, vector<DOUBLE> &vecImg_time, vector<ExOriPara> *vecEo)
{
    CmlLinearImage cls;
    bool H = cls.mlGetEop(vecLineEo, vecAngleEo, vecImg_time, vecEo);
    return H;
}
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
ML_EXTERN_C bool mlSatMappingByPts(SatProj &satproj, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, vector<StereoMatchPt> &vecDensePts, vector<Pt3d> &vecPres)
{
    CmlSatMapping cls;
    bool H = cls.mlSatMappingByPts(satproj, satPara,  vecRanPts, vecDensePts, vecPres );
    return H;
}

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
ML_EXTERN_C bool mlPanoMatchPts( vector<char*> vecParam, vector<FrameImgInfo> vecFrmInfo, vector<ImgPtSet> &vecImgPtSets, char* strOutPath, bool &bNeedAddPts)
{
    CmlPanoInterface cls;
    return cls.mlExportMatchPts(vecParam, vecFrmInfo, vecImgPtSets, strOutPath, bNeedAddPts);
    //cls.mlImportMatchPts(vecParam, vecFrmInfo, vecImgPtSets, strOutPath);
}

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
ML_EXTERN_C bool mlPanoMosic( vector<char*> vecParam, vector<FrameImgInfo> vecFrmInfo, vector<ImgPtSet> &vecImgPtSets, char* strOutPath )
{
    CmlPanoInterface cls;
    //cls.mlExportMatchPts(vecParam, vecFrmInfo, vecImgPtSets, strOutPath);
    return cls.mlImportMatchPts(vecParam, vecFrmInfo, vecImgPtSets, strOutPath);
}

//ML_EXTERN_C bool mlSiteImageMapping(SCHAR *strFileIn, SCHAR *strFileOut)
//{
//    CmlSiteMapping cls;
////    cls.mlSiteImageMapping(strFileIn,strFileOut);
//}
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
* @param[out] strDemPath DEM生成路径
* @param[out] strDomPath Dom生成路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
ML_EXTERN_C bool mlMapByInteBlock(   vector<StereoSet> &vecStereoSet, vector< ImgPtSet > &vecImgPtSets, Pt2d ptLT, Pt2d ptRB, DOUBLE dResolution,\
                                     ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                                     string strDemPath, string strDomPath )
{
    CmlSiteMapping cls;
    return cls.MapByInteBlock( vecStereoSet, vecImgPtSets, ptLT, ptRB, dResolution, extractPtsOpts, matchOpts, ransacOpts, mFilterOpts, strDemPath, strDomPath );
}
ML_EXTERN_C bool mlSiteBA( vector<StereoSet> vecStereoSetIn, vector< ImgPtSet > &vecImgPtSets, \
                           ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts, \
                           vector<StereoSet> &vecStereoOut )
{
    CmlSiteMapping cls;
    return cls.BA( vecStereoSetIn, vecImgPtSets, extractPtsOpts, matchOpts, ransacOpts, mFilterOpts, vecStereoOut );
}

ML_EXTERN_C bool mlLoadGdalImage(SCHAR *strFileName)
{
    CmlGdalDataset cls;
    return cls.LoadFile(strFileName,0);
}

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
 *@par 修改历史：
 *<作者>    <时间>   <版本编号>    <修改原因>\n
*/
ML_EXTERN_C bool mlTinSimply(vector<Pt3d> &vecPt3dIn,vector<Pt3d> &vecPt3dOut,DOUBLE simpleIndex)
{
    if(simpleIndex >1 || simpleIndex <= 0)
    {
        cout << "simpleIndex should between 0 and 1!\n";
        return false;
    }
    CmlPhgProc phg;
    phg.mlTinSimply(vecPt3dIn,vecPt3dOut,simpleIndex);
    // return true;



    return true;
//    CmlPhgProc phg;
//    ifstream infile(strFileIn);
//    vector<Pt3d> vecpt;
//    Pt3d pt3d;
//    while(!infile.eof())
//    {
//        infile >> pt3d.X >> pt3d.Y >> pt3d.Z ;
//        //infile >> pt3d.X;
//        vecpt.push_back(pt3d);
//    }
//
//    vector<Pt3d> vecptout;
//    phg.mlTinSimply(vecpt,vecptout,0.2);
//    ofstream outfile(strFileOut);
//    for(UINT i=0; i<vecptout.size(); i++)
//    {
//        outfile << vecptout[i].X << ' ' << vecptout[i].Y <<' '<< vecptout[i].Z<< '\n';
//
//    }
//
//    outfile.close();
//
//
//
//    //以下代码用于Demo显示,不用于正式工程中
//    string strout(strFileOut);
//    SINT nPos = strout.rfind(".");
//    string strDirect;
//    strDirect.assign( strout, 0, nPos );
//    strDirect.append(".smf");
//
//    string strdst = strout+"smf";
//    const char* cdst = strDirect.c_str();
//    char* dst = new char[100];
//    strcpy(dst,cdst);
//    phg.mlTinSimplyDemoFile(vecptout,dst);


}
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

ML_EXTERN_C bool mlCalcTransMatrixByXYZ(double dLocResult_x,double dLocResult_y,double dLocResult_z,TransMat& tmat)

{
    CmlCoordTrans coordtrans;
    CmlMat transMat;
    Pt3d pLocResult;
    pLocResult.X = dLocResult_x;
    pLocResult.Y = dLocResult_y;
    pLocResult.Z = dLocResult_z;
    coordtrans.mlCalcTransMatrixByXYZ(&pLocResult,&transMat,tmat.dVec);
    *tmat.dMat = transMat.GetAt(0,0);
    *(tmat.dMat+1) = transMat.GetAt(0,1);
    *(tmat.dMat+2) = transMat.GetAt(0,2);
    *(tmat.dMat+3) = transMat.GetAt(1,0);
    *(tmat.dMat+4) = transMat.GetAt(1,1);
    *(tmat.dMat+5) = transMat.GetAt(1,2);
    *(tmat.dMat+6) = transMat.GetAt(2,0);
    *(tmat.dMat+7) = transMat.GetAt(2,1);
    *(tmat.dMat+8) = transMat.GetAt(2,2);

//    memcpy(tmat.vec,pTransVec,sizeof(double)*3);
    return TRUE;

}

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
ML_EXTERN_C bool mlCalcTransMatrixByLatLong(double dLat,double dLong,TransMat& tmat)
{
    CmlCoordTrans coordtrans;
    CmlMat transMat;
    coordtrans.mlCalcTransMatrixByLatLong(dLat,dLong,&transMat,tmat.dVec);
    *tmat.dMat = transMat.GetAt(0,0);
    *(tmat.dMat+1) = transMat.GetAt(0,1);
    *(tmat.dMat+2) = transMat.GetAt(0,2);
    *(tmat.dMat+3) = transMat.GetAt(1,0);
    *(tmat.dMat+4) = transMat.GetAt(1,1);
    *(tmat.dMat+5) = transMat.GetAt(1,2);
    *(tmat.dMat+6) = transMat.GetAt(2,0);
    *(tmat.dMat+7) = transMat.GetAt(2,1);
    *(tmat.dMat+8) = transMat.GetAt(2,2);

    return TRUE;


}


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
ML_EXTERN_C bool mlCoordTransResult(DOUBLE* pArr,TransMat strumat,DOUBLE* pTransResult,SINT nflag)
{
    CmlMat rmat;
    rmat.Initial(strumat.nDim,strumat.nDim);
    for(int i=0; i<strumat.nDim; i++)
    {
        for(int j=0; j<strumat.nDim; j++)
        {
            rmat.SetAt(i,j,*(strumat.dMat + i*strumat.nDim + j) );

        }
    }

    CmlCoordTrans cls;
    bool rlt = cls.mlCoordTransResult(pArr,strumat.nDim,&rmat,strumat.dVec,pTransResult,nflag);

    return rlt;
}

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
ML_EXTERN_C bool mlVisualImage( const SCHAR* strDem, const SCHAR* strDom, const SCHAR* outImg,ExOriPara exori, DOUBLE fx,DOUBLE fy,SINT nImgWid,SINT nImgHei)
{
    CmlPhgProc pgh;
    pgh.mlImageReprj(strDem,strDom,outImg,exori,fx,fy,nImgWid,nImgHei);
    return TRUE;
}

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
ML_EXTERN_C bool mlGeoRasterCut( const SCHAR* strFileIn,const SCHAR* strFileOut,DOUBLE pttl_x,DOUBLE pttl_y,DOUBLE ptbr_x,DOUBLE ptbr_y,SINT nflag, SINT nCutBands, DOUBLE dZoom )
{
    CmlPhgProc phg;
    Pt2d pttl,ptbr;
    pttl.X = pttl_x;
    pttl.Y = pttl_y;
    ptbr.X = ptbr_x;
    ptbr.Y = ptbr_y;
    bool result = phg.mlGeoRasterCut(strFileIn,strFileOut,pttl,ptbr,nflag,nCutBands,dZoom);
    return result;

}


ML_EXTERN_C bool mlCleanDeadPix( const char* strImgPathIn, const char* strImgPathOut, vector<Pt2i> vecDeadPix )
{
    CmlFrameImage frmImg;
    bool result = frmImg.mlCleanDeadPix(strImgPathIn,strImgPathOut,vecDeadPix);
    return result;
}

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
ML_EXTERN_C bool mlDEMMosaic(vector<string> vecInputFiles, const SCHAR* cOutputFile, DOUBLE dXResl, DOUBLE dYResl, SINT nResampleAlg, SINT nDisCultLine )
{
    CmlRasterMosaic DemMosaic;
    if(DemMosaic.mlDEMMosaic(vecInputFiles, cOutputFile, dXResl, dYResl, 2, 0))
    {
        return false;
    }
    else
    {
        return true;
    }
}
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
ML_EXTERN_C bool mlPano2Prespective( const SCHAR *cInputPanoFile, const SCHAR * cOutputImage, SINT nOriginX, SINT nOriginY, SINT nPanoRoiW, SINT nPanoRoiH, DOUBLE dFocus)
{
    CmlPano2Prespective Pano2Prespective;
    Pt2i ptOrigin;
    ptOrigin.X = nOriginX;
    ptOrigin.Y = nOriginY;

    if(Pano2Prespective.mlPano2Prespective( cInputPanoFile, cOutputImage, ptOrigin, nPanoRoiW, nPanoRoiH, dFocus))
    {
        return true;
    }
    else
    {
        return false;
    }

}

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
ML_EXTERN_C bool mlComputeSlopeMap (SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor )
{
    CmlDemAnalyse cal;
    if(cal.ComputeSlopeInterface(sInputDEM, sDestDEM, nWindowSize, dZfactor) < 0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

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

ML_EXTERN_C bool mlComputeSlopeAspectMap(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor )
{
    CmlDemAnalyse cal;
    if(cal.ComputeSlopeAspectInterface(sInputDEM, sDestDEM, nWindowSize, dZfactor) < 0)
    {
        return false;
    }
    else
    {
        return true;
    }

}
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
ML_EXTERN_C bool mlComputeBarrierMap(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor,ObstacleMapPara ObPara)
{
    CmlDemAnalyse cal;
    if(cal.ComputeObstacleMapInterface(sInputDEM, sDestDEM, nWindowSize, dZfactor, ObPara) < 0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
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
ML_EXTERN_C bool mlComputeInsightMap( const SCHAR * sInputDEM, SINT nxLocation ,SINT nyLocation, DOUBLE dViewHight, const SCHAR * sDestDEM, bool InversHeight)
{
    CmlDemAnalyse cal;
    if(cal.ComputeViewShedInterface(sInputDEM, nxLocation,nyLocation, dViewHight, sDestDEM,InversHeight)< 0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
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
ML_EXTERN_C bool mlComputeContourMap(DOUBLE dHinterval, SCHAR* strSrcfilename, SCHAR* strDstfilename ,bool bCNodata , DOUBLE dNodata ,SCHAR* strAttrib )
{
    CmlDemAnalyse cal;
    if(cal.ComputeContourInterface(dHinterval, strSrcfilename, strDstfilename, bCNodata, dNodata, strAttrib)<0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
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
ML_EXTERN_C bool mlLocalByMatch( const SCHAR* strLandDom, const SCHAR* strSatDom,  ImgPtSet &LandImgPtset, ImgPtSet &SatImgPtset, LocalByMatchOpts localByMOpts, Pt2d ptCent, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{
    CmlLocalization clsLocal;
    return clsLocal.LocalIn2Dom( strLandDom, strSatDom, LandImgPtset, SatImgPtset, localByMOpts, ptCent, ptLocalRes, dLocalAccuracy );
}
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
ML_EXTERN_C bool mlLocalByIntersection( vector<Pt3d> vecGCPs, vector< ImgPtSet > &vecImgPtSets,  Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{
    CmlLocalization clsLocal;
    return clsLocal.LocalByBundleResection( vecGCPs, vecImgPtSets, ptLocalRes, dLocalAccuracy );
}
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
ML_EXTERN_C bool mlLocalIn2Site( vector<StereoSet> vecFrontSite, vector<StereoSet> vecEndSite, vector<ImgPtSet> &vecFrontPts, vector<ImgPtSet> &vecEndPts, LocalBy2SitesOpts localBy2Opt, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{
    CmlLocalization clsLocal;
    return clsLocal.LocalInTwoSite( vecFrontSite, vecEndSite, vecFrontPts, vecEndPts, localBy2Opt, ptLocalRes, dLocalAccuracy );
}
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
ML_EXTERN_C bool mlLocalBySeqence(  FrameImgInfo FrameInfoSet, const SCHAR* strSatDom, ImgPtSet &frmPts, ImgPtSet &SatPts, LocalBySeqImgOpts stuLocalBySeqOpts, Pt3d &ptLocalRes, DOUBLE &dLocalAccuracy )
{
    CmlLocalization clsLocal;
    return clsLocal.LocalInSequenceImg(  FrameInfoSet, strSatDom, frmPts, SatPts, stuLocalBySeqOpts, ptLocalRes, dLocalAccuracy );
}
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
ML_EXTERN_C bool mlLocalBySImgIntersection( vector<Pt3d> vecGCPs, ImgPtSet imgPts,  ExOriPara &exOriRes, vector<RMS2d> &vecRMSRes, DOUBLE &dTotalRMS  )
{
    CmlLocalization clsLocal;
    return clsLocal.LocalBySImgIntersection( vecGCPs, imgPts, exOriRes, vecRMSRes, dTotalRMS );
}
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
ML_EXTERN_C bool mlGetNewStereoPtID( ImgPtSet &clsLPts, ImgPtSet &clsRPts, ULONG &lID  )
{
    CmlPtsManage clsPtManage;
    return clsPtManage.GetNewStereoPtID( clsLPts, clsRPts, lID );
}
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
ML_EXTERN_C bool mlGetNewSinglePtID(  ImgPtSet &clsImgPts, ULONG &lID  )
{
    CmlPtsManage clsPtManage;
    return clsPtManage.GetNewSinglePtID( clsImgPts, lID );
}

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
ML_EXTERN_C bool mlSmoothByGaussian(const SCHAR* strFileIn, SINT nTemplateSize, DOUBLE dCoef, const SCHAR* strFileOut )
{
    CmlFrameImage clsImg;
    CmlRasterBlock *pImgBlock = &clsImg.m_DataBlock;
    if( ( false == clsImg.LoadFile(strFileIn) ) || ( false == clsImg.SmoothByGuassian(nTemplateSize, dCoef) ) )
    {
        return false;
    }
    CmlGdalDataset clsGdalOut;

    if ( ( false == clsGdalOut.CreateFile(strFileOut, clsImg.GetWidth(),clsImg.GetHeight(), (UINT)1, GDT_Byte,"GTiff") ) ||
            ( false == clsGdalOut.SaveBlockToFile( (UINT)1, 0, 0, pImgBlock,0,0, clsImg.GetWidth(),clsImg.GetHeight(), (UINT)1 ) ) )
    {
        return false;
    }
    return true;
}

/**
* @fn mlGetEpipolarImg
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 生成核线影像
* @param[in] pStereoSet 立体像对信息
* @param[out] strFileOutL 左影像核线影像路径
* @param[out] strFileOutR 右影像核线影像路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
ML_EXTERN_C bool mlGetEpipolarImg(  StereoSet* pStereoSet,
                                    const SCHAR* strFileOutL,
                                    InOriPara &inOriL,
                                    ExOriPara &exOriL,
                                    const SCHAR* strFileOutR,
                                    InOriPara &inOriR,
                                    ExOriPara &exOriR,
                                    CAMTYPE nLCamType,
                                    CAMTYPE nRCamType,
                                    DOUBLE dZoomCoef )
{

    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc clsStereoProc;

    if( false == clsImgL.LoadFile( pStereoSet->imgLInfo.strImgPath.c_str() ) )
    {
        return false;
    }
    clsImgL.m_InOriPara = pStereoSet->imgLInfo.inOri;
    clsImgL.m_ExOriPara = pStereoSet->imgLInfo.exOri;


    if( false == clsImgR.LoadFile( pStereoSet->imgRInfo.strImgPath.c_str() ) )
    {
        return false;
    }
    clsImgR.m_InOriPara = pStereoSet->imgRInfo.inOri;
    clsImgR.m_ExOriPara = pStereoSet->imgRInfo.exOri;


    clsStereoProc.m_pDataL = &clsImgL;
    clsStereoProc.m_pDataR = &clsImgR;

    if( ( false == clsImgL.GetUnDistortImg(nLCamType, dZoomCoef ) ) || ( false == clsImgR.GetUnDistortImg(nRCamType, dZoomCoef) ) )
    {
        return false;
    }

    CmlFrameImage* pFImgL = (CmlFrameImage*)clsStereoProc.m_pDataL;
    CmlFrameImage* pFImgR = (CmlFrameImage*)clsStereoProc.m_pDataR;
    CmlRasterBlock ImgL, ImgR;
    if( ( false == ImgL.InitialImg( pFImgL->GetHeight(), pFImgL->GetWidth() ) ) ||
            ( false == ImgR.InitialImg( pFImgR->GetHeight(), pFImgR->GetWidth() ) ) )
    {
        return false;
    }
    ImgL.SetGDTType( GDT_Byte );
    ImgR.SetGDTType( GDT_Byte );

    clsStereoProc.mlGetEpipolarImg( &pFImgL->m_DataBlock, &pFImgR->m_DataBlock, &clsImgL.m_InOriPara, &clsImgL.m_ExOriPara, \
                                    &clsImgR.m_InOriPara,&clsImgR.m_ExOriPara, &ImgL, &ImgR );
    inOriL = clsImgL.m_InOriPara;
    inOriR = clsImgR.m_InOriPara;
    exOriL = clsImgL.m_ExOriPara;
    exOriR = clsImgR.m_ExOriPara;
    //////////////////////////////////////////
    string strTempLPath(strFileOutL);
    string strTempRPath(strFileOutR);
    SINT nPosL = strTempLPath.rfind(".");
    SINT nPosR = strTempRPath.rfind(".");
    SINT nLStrLength = strTempLPath.length();
    SINT nRStrLength = strTempRPath.length();
    if( ( nPosL >= nLStrLength )||( nPosR >= nRStrLength ) )
    {
        return false;
    }

    const char* cLSuffix = strTempLPath.c_str() + nPosL + 1;
    const char* cRSuffix = strTempRPath.c_str() + nPosR + 1;

    ///////////////////////////////////////////
    if( 0 == strcmp( cLSuffix, "tif" ) )
    {
        cLSuffix = "GTIFF";
    }
    if( 0 == strcmp( cRSuffix, "tif" ) )
    {
        cRSuffix = "GTIFF";
    }

    CmlGdalDataset clsGdalL, clsGdalR;
    if( ( true == clsGdalL.CreateFile(strFileOutL, pFImgL->GetWidth(),pFImgL->GetHeight(), (UINT)1, GDT_Byte,cLSuffix)) &&
            ( true == clsGdalR.CreateFile(strFileOutR, pFImgR->GetWidth(),pFImgR->GetHeight(), (UINT)1, GDT_Byte,cRSuffix) ) )
    {

        return ( ( clsGdalL.SaveBlockToFile( (UINT)1, 0, 0, &ImgL,0,0,pFImgL->GetWidth(),pFImgL->GetHeight(), (UINT)1 ) )&&
                 ( clsGdalR.SaveBlockToFile( (UINT)1, 0, 0, &ImgR,0,0,pFImgR->GetWidth(),pFImgR->GetHeight(), (UINT)1 ) ) );
    }
    else
    {
        return false;
    }
}

ML_EXTERN_C  bool mlSift(StereoSet* pStereoSet, SiftMatchPara siftPara, RANSACAffineModPara ransacPara, vector<Pt2d> &vecFLPts, vector<Pt2d> &vecFRPts)
{
    CmlStereoProc clsStereoProc;
    vector<StereoMatchPt> vecPts;
    if( false == clsStereoProc.Match2LargeImg( pStereoSet->imgLInfo.strImgPath.c_str(), pStereoSet->imgRInfo.strImgPath.c_str(), vecPts, siftPara, ransacPara ) )
    {
        return false;
    }
    for( UINT i = 0; i < vecPts.size(); ++i )
    {
        StereoMatchPt pt = vecPts[i];
        vecFLPts.push_back( pt.ptLInImg );
        vecFRPts.push_back( pt.ptRInImg );
    }
    return true;

}
ML_EXTERN_C  bool mlASift(StereoSet* pStereoSet, ASiftMatchPara asiftPara, RANSACAffineModPara ransacPara, vector<Pt2d> &vecFLPts, vector<Pt2d> &vecFRPts )
{
    CmlStereoProc clsStereoProc;
    vector<StereoMatchPt> vecPts;
    SiftMatchPara siftPara;
    if( false == clsStereoProc.Match2LargeImg( pStereoSet->imgLInfo.strImgPath.c_str(), pStereoSet->imgRInfo.strImgPath.c_str(), vecPts, siftPara, ransacPara, 1.0, (UINT)3000, (UINT)200, asiftPara.nNumTilts, false  ) )
    {
        return false;
    }
    for( UINT i = 0; i < vecPts.size(); ++i )
    {
        StereoMatchPt pt = vecPts[i];
        vecFLPts.push_back( pt.ptLInImg );
        vecFRPts.push_back( pt.ptRInImg );
    }
    return true;
}
ML_EXTERN_C bool  mlTemplateMatchImg( const SCHAR* pLeftImg, const SCHAR* pRightImg, vector<Pt2i> &vecPtL, vector<Pt2i> &vecPtR,\
                                      MatchInRegPara MatchPara, vector<StereoMatchPt> &vecFeatMatchPts)
{

    CmlFrameImage clsImgL, clsImgR;
    CmlStereoProc cls;
    if( ( false == clsImgL.LoadFile( pLeftImg) )||( false == clsImgR.LoadFile( pRightImg ) ) )
    {
        return false;
    }
    clsImgL.SmoothByGuassian(  7, 0.6 );

    clsImgR.SmoothByGuassian(  7, 0.6 );

    cls.m_pDataL = &clsImgL;
    cls.m_pDataR = &clsImgR;

    MLRect rect;
    rect.dXMin = MatchPara.dXMin;
    rect.dYMin = MatchPara.dYMin;
    rect.dXMax = MatchPara.dXMax;
    rect.dYMax = MatchPara.dYMax;

    SINT nTemplateSize = MatchPara.nTempSize;
    DOUBLE dCoef = MatchPara.dCoefThres;
    SINT nXOffSet = (SINT) MatchPara.dXOff;
    SINT nYOffSet = (SINT) MatchPara.dYOff;

    CmlFrameImage* pFImgL = &clsImgL;
    CmlFrameImage* pFImgR = &clsImgR;
    return (cls.mlTemplateMatch( &(pFImgL->m_DataBlock), &(pFImgR->m_DataBlock), vecPtL, vecPtR, vecFeatMatchPts, rect, nTemplateSize, dCoef, nXOffSet, nYOffSet ));
}
ML_EXTERN_C bool mlStereoMatchInFrmImg( StereoSet* pStereo, ExtractFeatureOpt extractOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, \
                                        ImgPtSet &imgLPts, ImgPtSet &imgRPts, vector<Pt3d> &vec3dPts )
{
    CmlFrameImage imgL, imgR;
    if( ( false == imgL.LoadFile( pStereo->imgLInfo.strImgPath.c_str() ))||( false == imgR.LoadFile( pStereo->imgRInfo.strImgPath.c_str() )) )
    {
        return false;
    }
    if( ( false == imgL.ExtractFeatPtByForstner( extractOpts.nGridSize, extractOpts.nPtMaxNum, extractOpts.dThresCoef ) )||
            ( false == imgR.ExtractFeatPtByForstner( extractOpts.nGridSize, extractOpts.nPtMaxNum, extractOpts.dThresCoef ) ) )
    {
        return false;
    }
    vector<StereoMatchPt> vecMPts, vecRanPts;
    if( false == mlTemplateMatchImg( pStereo->imgLInfo.strImgPath.c_str(), pStereo->imgRInfo.strImgPath.c_str(),
                                     imgL.m_vecFeaPtsList, imgR.m_vecFeaPtsList, matchOpts, vecMPts ))
    {
        return false;
    }
    CmlStereoProc clsStereo;
    clsStereo.m_pDataL = &imgL;
    clsStereo.m_pDataR = &imgR;

    if( false == clsStereo.mlGetRansacPts( vecMPts, ransacOpts.dThres, ransacOpts.dConfidence ) )
    {
        return false;
    }

    if( false == clsStereo.mlLsMatchInFrameImg( vecMPts, 7 ) )
    {
        return false;
    }

    vector<bool> vecbIsValid;
    if( false == clsStereo.mlInterSectionInFrameImg( vecMPts, vecbIsValid ))
    {
        return false;
    }

    CmlPtsManage clsPtManage;
    clsPtManage.SplitPairPts( pStereo->imgLInfo, pStereo->imgRInfo, vecMPts, imgLPts, imgRPts );

    for( UINT i = 0; i < vecbIsValid.size(); ++i )
    {
        if( vecbIsValid[i] )
        {
            Pt3d ptCur;
            StereoMatchPt pt = vecMPts[i];
            ptCur.X = pt.X;
            ptCur.Y = pt.Y;
            ptCur.Z = pt.Z;
            ptCur.lID = imgLPts.vecPts.at(i).lID;

            vec3dPts.push_back( ptCur );
        }
    }
    return true;
}
ML_EXTERN_C bool  mlDEMGeneration( vector<Pt3d> &vec3dPts, const SCHAR* strPath, DbRect dbDemRect, DOUBLE dResolution, ImgDotType imgType, string strTriFile )
{
    CmlDemProc clsDemProc;
    return clsDemProc.mlWriteToGeoFile( vec3dPts, dbDemRect, dResolution, strPath, imgType, strTriFile );
}
ML_EXTERN_C bool  mlDEMGenerationNoRect( vector<Pt3d> &vec3dPts, const SCHAR* strPath, DOUBLE dResolution, ImgDotType imgType, string strTriFile )
{
	CmlDemProc clsDemProc;
	return clsDemProc.mlWriteToGeoFile( vec3dPts, dResolution, strPath, imgType, strTriFile );
}
ML_EXTERN_C bool  mlDEMGenerationInRegion( vector< vector< Pt3d> > &vec3dPts, const SCHAR* strPath, DbRect dbDemRect, DOUBLE dResolution, ImgDotType imgType, string strTriFile )
{
    CmlDemProc clsDemProc;
    vector<Pt3d> vecDealPts;
    vector<Polygon3d> vecPolys;
    CmlPhgProc clsPhg;

    for( UINT i = 0; i < vec3dPts.size(); ++i )
    {
        vecDealPts.insert( vecDealPts.end(), vec3dPts[i].begin(), vec3dPts[i].end() );
        Polygon3d poly;
        clsPhg.CalcConvexHull( vec3dPts[i], poly.vecVectex );
        poly.nID = i;
        vecPolys.push_back( poly );
    }

    return clsDemProc.mlWriteRegionToGeoFile(  vecDealPts, dbDemRect, dResolution, vecPolys, strPath, imgType, strTriFile  );

}
ML_EXTERN_C bool  mlDOMGeneration( vector<StereoSet> &vecStereoSets, const SCHAR* strDEMPath, const SCHAR* strDOMPath, ImgDotType imgType )
{
    CmlDomProc clsDomProc;
    string strDem(strDEMPath);
    string strDom(strDOMPath);

    return clsDomProc.createOrthoImage( vecStereoSets, strDem, strDom, imgType );
}
ML_EXTERN_C bool mlExOriTrans( ExOriPara* pExOriL, ExOriPara* pExOriRela, ExOriPara* pExOriR )
{
    CmlPhgProc clsPhgProc;
    return clsPhgProc.ExOriTrans( pExOriL, pExOriRela, pExOriR );
}
ML_EXTERN_C bool mlGetRelaOri( ExOriPara* pExOriL, ExOriPara* pExOriR, ExOriPara* pExOriRela )
{
    CmlPhgProc clsPhgProc;
    return clsPhgProc.GetRelaOri( pExOriL, pExOriR, pExOriRela );
}
ML_EXTERN_C bool mlGetOPKAngle( DOUBLE *pRMat, DOUBLE *pOPK )
{
    CmlPhgProc clsPhgProc;
    return clsPhgProc.GetOPKAngle( pRMat, pOPK );
}
ML_EXTERN_C bool mlGetRMatByOPK( DOUBLE *pOPK, DOUBLE *pRMat )
{
    CmlPhgProc clsPhgProc;
    return clsPhgProc.GetRMatByOPK( pOPK, pRMat );
}
ML_EXTERN_C bool mlSemiAutoMatchInRegion( const SCHAR* strPathL, const SCHAR* strPathR, Pt2i ptL, MatchInRegPara matchPara, Pt2i &ptR, DOUBLE &dCoef )
{
    CmlFrameImage clsImgL, clsImgR;
    if( ( false == clsImgL.LoadFile( strPathL ) )||( false == clsImgR.LoadFile( strPathR ) ) )
    {
        return false;
    }
    CmlStereoProc clsStereoProc;
    if( false == clsStereoProc.mlTemplateMatchInRegion( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, ptL, ptR, dCoef, \
            SINT( matchPara.dXMin+0.1),SINT( matchPara.dXMax+0.1),SINT( matchPara.dYMin+0.1),SINT( matchPara.dYMax+0.1), \
            matchPara.nTempSize, matchPara.dCoefThres, SINT( matchPara.dXOff+0.1), SINT( matchPara.dYOff+0.1) ) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlLsMatch( const SCHAR* strPathL, const SCHAR* strPathR, Pt2d ptL, UINT nTemplateSize, Pt2d &ptR, DOUBLE &dCoef )
{
    CmlFrameImage clsImgL, clsImgR;
    if( ( false == clsImgL.LoadFile( strPathL ) )||( false == clsImgR.LoadFile( strPathR ) ) )
    {
        return false;
    }
    CmlStereoProc clsStereoProc;

    if( false == clsStereoProc.mlLsMatch( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, ptL.X, ptL.Y, ptR.X, ptR.Y, nTemplateSize, dCoef ) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlWallisFilter( const SCHAR* strInPath, const SCHAR* strOutPath, WallisFPara fPara )
{
    CmlFrameImage clsImg;
    if( false == clsImg.LoadFile( strInPath ) )
    {
        return false;
    }
    if( false == clsImg.WallisFilter( fPara.nTemplateSize, fPara.dExpectMean, fPara.dExpectVar, fPara.dCoefA, fPara.dCoefAlpha ) )
    {
        return false;
    }
    if( false == clsImg.SaveFile( strOutPath ) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlGuassFilter(  const SCHAR* strInPath, const SCHAR* strOutPath,  SINT nTemplateSize, DOUBLE dCoef )
{
    CmlFrameImage clsImg;
    if( false == clsImg.LoadFile( strInPath ) )
    {
        return false;
    }
    if( false == clsImg.SmoothByGuassian( nTemplateSize, dCoef ) )
    {
        return false;
    }
    if( false == clsImg.SaveFile( strOutPath) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlHistogramEqualize(  const SCHAR* strInPath, const SCHAR* strOutPath )
{
    CmlFrameImage clsImg;
    if( false == clsImg.LoadFile( strInPath ) )
    {
        return false;
    }
    if( false == clsImg.HistogramEqualize() )
    {
        return false;
    }
    if( false == clsImg.SaveFile( strOutPath) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlGrayTensile( const SCHAR* strInPath, const SCHAR* strOutPath, UINT nMin, UINT nMax )
{
    CmlFrameImage clsImg;
    if( false == clsImg.LoadFile( strInPath ) )
    {
        return false;
    }
    if( false == clsImg.GrayTensile( nMin, nMax) )
    {
        return false;
    }
    if( false == clsImg.SaveFile( strOutPath) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlExtractFeatPtByForstner( const SCHAR* pImg, vector<Pt2i>& vecFeaPts, SINT nGridSize, SINT nPtNum, DOUBLE dCoef, bool bIsRemoveAbPixel )
{
    CmlFrameImage clsImg;
    if( ( false == clsImg.LoadFile( pImg ) )||( false == clsImg.ExtractFeatPtByForstner( nGridSize, nPtNum, dCoef, bIsRemoveAbPixel )) )
    {
        return false;
    }

    vecFeaPts = clsImg.m_vecFeaPtsList;
    return true;
}

bool isInRange( Pt2i pt, MLRect rect )
{
    if( ( pt.X < rect.dXMin )||( pt.X > rect.dXMax )||( pt.Y < rect.dYMin )||( pt.Y > rect.dYMax ) )
    {
        return false;
    }
    else
    {
        return true;
    }
}
MLAPI( bool )  mlExtractFeatPtByForstnerWithSRegion( const SCHAR* pImg, vector<Pt2i>& vecFeaPts, vector<MLRect> vecDisableRange, SINT nGridSize, SINT nPtNum, DOUBLE dCoef )
{
    CmlFrameImage clsImg;
    if( ( false == clsImg.LoadFile( pImg ) )||( false == clsImg.ExtractFeatPtByForstner( nGridSize, nPtNum, dCoef )) )
    {
        return false;
    }

    for( UINT i = 0; i < clsImg.m_vecFeaPtsList.size(); ++i )
    {
        bool bIsInRange = false;
        Pt2i ptCur = clsImg.m_vecFeaPtsList[i];

        for( UINT j = 0; j < vecDisableRange.size(); ++j )
        {
            if( true == isInRange( ptCur, vecDisableRange[j] ) )
            {
                bIsInRange = true;
                break;
            }
        }
        if( false == bIsInRange )
        {
            vecFeaPts.push_back( ptCur );
        }
    }
    return true;
}
ML_EXTERN_C bool mlExtractFeatPtByCanny( const SCHAR* pImg, vector<Pt2i> &vecFeatPts, DOUBLE dThres1, DOUBLE dThres2 )
{
    CmlFrameImage clsFrm;
    return clsFrm.EdgeDetectionByCanny( pImg, vecFeatPts, dThres1, dThres2 );
}
ML_EXTERN_C bool mlUnDistort( Pt2d ptIn, UINT nHeight, InOriPara inOri, Pt2d &ptOutRes, CAMTYPE nCamType )
{
    CmlFrameImage clsImg;
    return clsImg.UnDisCorToPlaneFrame( ptIn, inOri, nHeight, ptOutRes, nCamType );
}
ML_EXTERN_C bool mlUnDistortImg( const SCHAR* strIn, InOriPara inOri, const SCHAR* strOut, CAMTYPE camType, DOUBLE dRatio )
{
	CmlFrameImage clsImg;
	if( false == clsImg.LoadFile( strIn ) )
	{
		return false;
	}
	clsImg.m_InOriPara = inOri;
	if( false == clsImg.GetUnDistortImg( camType, dRatio ) )
	{
		return false;
	}
	if( false == clsImg.SaveFile( strOut ) )
	{
		return false;
	}
	return true;

}
ML_EXTERN_C bool mlRansacPtsByHomo( vector<StereoMatchPt> &vecStereoPts, DOUBLE dThresh, DOUBLE dConfidence)
{
    CmlStereoProc clsStereoProc;
    return clsStereoProc.mlGetRansacPts( vecStereoPts, dThresh, dConfidence );
}
ML_EXTERN_C bool mlRansacPtsByHomoVT( vector<StereoMatchPt> vecStereoPts, vector<StereoMatchPt> &vecOutStereoPts, DOUBLE dThresh, DOUBLE dConfidence )
{
    CmlStereoProc clsStereoProc;
    return clsStereoProc.mlGetRansacPts( vecStereoPts, vecOutStereoPts, dThresh, dConfidence );
}
ML_EXTERN_C bool mlRansacPtsByAffineT( vector<StereoMatchPt> vecStereoInPts, DOUBLE dSegma, DOUBLE dMaxError, vector<StereoMatchPt> &vecStereoOutPts )
{
    CmlStereoProc clsStereoProc;
    return clsStereoProc.mlGetRansacPtsByAffineT( vecStereoInPts, vecStereoOutPts, dSegma, dMaxError );
}
ML_EXTERN_C bool mlTemplateMatchInFeatPts( const SCHAR* pLImg, const SCHAR* pRImg, vector<Pt2i> vecLPts, vector<Pt2i> vecRPts, vector<StereoMatchPt> &vecSMPts, MatchInRegPara matchPara, bool bIsRemoveAbPixel )
{
    CmlFrameImage clsImgL, clsImgR;
    if( ( false == clsImgL.LoadFile( pLImg ) )||( false == clsImgR.LoadFile( pRImg ) ) )
    {
        return false;
    }
    CmlStereoProc clsStereoProc;
    MLRect rect;
    rect.dXMax = matchPara.dXMax;
    rect.dXMin = matchPara.dXMin;
    rect.dYMax = matchPara.dYMax;
    rect.dYMin = matchPara.dYMin;
    if( false == clsStereoProc.mlTemplateMatch( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, vecLPts, vecRPts, vecSMPts, rect, matchPara.nTempSize, matchPara.dCoefThres, matchPara.dXOff, matchPara.dYOff, bIsRemoveAbPixel ) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlTemplateMatchInFeatPtsVT( const SCHAR* pLImg, const SCHAR* pRImg, vector<Pt2i> vecLPts, vector<Pt2i> vecRPts, vector<StereoMatchPt> &vecSMPts, MatchInRegPara matchPara, UINT nMethod, bool bIsRemoveAbPixel )
{
    CmlStereoProc clsStereoProc;
    if( 1 == nMethod )
    {
        return clsStereoProc.mlTemplateMatchInTwoFeatPtsVerify( pLImg, pRImg, vecLPts, vecRPts, vecSMPts, matchPara, bIsRemoveAbPixel );
    }
    if( 2 == nMethod )
    {
        return clsStereoProc.mlTemplateMatchInLFeatPtsToAll( pLImg, pRImg, vecLPts, vecSMPts, matchPara, bIsRemoveAbPixel );
    }
    if( 3 == nMethod )
    {
        return clsStereoProc.mlTemplateMatchInRFeatPtsToAll( pLImg, pRImg, vecRPts, vecSMPts, matchPara, bIsRemoveAbPixel );
    }
    if( 4 == nMethod )
    {
        CmlFrameImage clsImgL, clsImgR;
        if( ( false == clsImgL.LoadFile( pLImg ) )||( false == clsImgR.LoadFile( pRImg ) ) )
        {
            return false;
        }
        MLRect rect;
        rect.dXMax = matchPara.dXMax;
        rect.dXMin = matchPara.dXMin;
        rect.dYMax = matchPara.dYMax;
        rect.dYMin = matchPara.dYMin;
        if( false == clsStereoProc.mlTemplateMatchWithAccu( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, vecLPts, vecRPts, vecSMPts, rect, matchPara.nTempSize, matchPara.dCoefThres, matchPara.dXOff, matchPara.dYOff, bIsRemoveAbPixel ) )
        {
            return false;
        }
        return true;
    }
    return false;

}

ML_EXTERN_C bool mlTemplateMatchInFeatPtsWithGivenPts( const SCHAR* pLImg, const SCHAR* pRImg, Pt2d ptL, Pt2d ptR, UINT nRange, UINT nTemplateS, \
        Pt2d &ptRes, DOUBLE &dCoefRes, DOUBLE dCoefThres )
{
    CmlFrameImage clsImgL, clsImgR;
    if( ( false == clsImgL.LoadFile( pLImg ) )||( false == clsImgR.LoadFile( pRImg ) ) )
    {
        return false;
    }

    CmlStereoProc clsStereoProc;
    Pt2i ptLT, ptRT;
    ptLT.X = SINT( ptL.X + 0.5 );
    ptLT.Y = SINT( ptL.Y + 0.5 );
    ptRT.X = SINT( ptR.X + 0.5 );
    ptRT.Y = SINT( ptR.Y + 0.5 );
    Pt2d ptOut;

    DOUBLE dCoef = -1.0;
    if( true == clsStereoProc.mlTemplateMatchInRegion( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, ptLT, ptRT, dCoef, \
            (-nRange), (nRange), (-nRange), (nRange), \
            nTemplateS, dCoefThres, (ptRT.X-ptLT.X), (ptRT.Y-ptLT.Y) ) )
    {
        ptOut.X = ptRT.X;
        ptOut.Y = ptRT.Y;

        dCoefRes = dCoef;
        DOUBLE dCoefTmp;
        if( true == clsStereoProc.mlLsMatch( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, ptL.X, ptL.Y, ptOut.X, ptOut.Y, nTemplateS, dCoefTmp ))
        {
            dCoefRes = dCoefTmp;
        }

        dCoefRes = dCoef;
        ptRes = ptOut;
        return true;
    }
    else
    {
        return false;
    }

}
ML_EXTERN_C bool mlRegionDenseMatch( const SCHAR* strInPutLImgPath, const SCHAR* strInPutRImgPath, vector<StereoMatchPt> vecFeatMPts, UINT nStep, UINT nRadiusX, UINT nRadiusY, SINT nXOff, SINT nYOff, \
                                  UINT nTemplateSize, DOUBLE dCoefThres, DOUBLE dXmid, DOUBLE dYmid, UINT nHeight, UINT nWidth, vector<StereoMatchPt> &vecOutMRes  )
{
    CmlStereoProc clsStereoProc;
    CmlFrameImage clsImgL, clsImgR;

    if( ( false == clsImgL.LoadFile( strInPutLImgPath ) ) ||( false == clsImgR.LoadFile( strInPutRImgPath ) ) )
    {
        return false;
    }
    WideOptions wOpts;
    wOpts.nStep = nStep;
    wOpts.nRadiusX = nRadiusX;
    wOpts.nRadiusY = nRadiusY;
    wOpts.nXOffSet = nXOff;
    wOpts.nYOffSet = nYOff;
    wOpts.nTemplateSize = nTemplateSize;
    wOpts.dCoef = dCoefThres;

    MLRect rect;
    rect.dXMin = dXmid - (nWidth-1)/2 - ML_ZERO;
    rect.dYMin = dYmid - (nHeight-1)/2 - ML_ZERO;
    rect.dXMax = dXmid + (nWidth-1)/2 + ML_ZERO;
    rect.dYMax = dYmid + (nHeight-1)/2 + ML_ZERO;

    vector<Pt2d> vecOutLRes, vecOutRRes;
    vector<DOUBLE> vecOutCorr;
    if( false == clsStereoProc.mlDenseMatch( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, vecFeatMPts, wOpts, rect, vecOutLRes, vecOutRRes, vecOutCorr ) )
    {
        return false;
    }

    for( UINT i = 0; i < vecOutLRes.size(); ++i )
    {
        StereoMatchPt ptCur;
        ptCur.ptLInImg = vecOutLRes[i];
        ptCur.ptRInImg = vecOutRRes[i];
        ptCur.dRelaCoef = vecOutCorr[i];
        vecOutMRes.push_back( ptCur );
    }
    return true;


}
ML_EXTERN_C bool mlFilterPtsByMedian( vector<StereoMatchPt> &MatchPts, SINT nTemplateSize, DOUBLE dThresCoef, DOUBLE dThres )
{
    CmlStereoProc clsStereoProc;
    clsStereoProc.mlFilterPtsByMedian( MatchPts, nTemplateSize, dThresCoef, dThres );
    return true;
}
ML_EXTERN_C bool mlSiftMatch( const SCHAR* strLPath, const SCHAR* strRPath, DOUBLE dRatio, UINT nMaxBlockSize, UINT nOverLaySize, vector<StereoMatchPt> &vecOutRes )
{
    CmlStereoProc clsStereoProc;
    SiftMatchPara siftPara;
    siftPara.dRatio = dRatio;

    RANSACAffineModPara ransacPara;

    return clsStereoProc.Match2LargeImg( strLPath, strRPath, vecOutRes, siftPara, ransacPara, 1.0, nMaxBlockSize, nOverLaySize, 8, true );
}
ML_EXTERN_C double*  mlSiftMatchVT( const SCHAR* strLPath, const SCHAR* strRPath, DOUBLE dRatio, DOUBLE dbRansacSigma, UINT nMaxBlockSize, UINT nOverLaySize, int &nPtSize )
{
	CmlStereoProc clsStereoProc;
	SiftMatchPara siftPara;
	siftPara.dRatio = dRatio;

	RANSACAffineModPara ransacPara;
	vector<StereoMatchPt> vecOutRes;
		
	ransacPara.dSigma=dbRansacSigma;
	if ( false == clsStereoProc.Match2LargeImg( strLPath, strRPath, vecOutRes, siftPara, ransacPara, 1.0, nMaxBlockSize, nOverLaySize, 8, true ) )
	{
		return 0;	
	}
	double *pPts = new DOUBLE[4*vecOutRes.size()];
	for ( UINT i = 0; i < vecOutRes.size(); ++i )
	{
		StereoMatchPt sPtCur = vecOutRes[i];
		pPts[4*i] = sPtCur.ptLInImg.X;
		pPts[4*i+1] = sPtCur.ptLInImg.Y;
		pPts[4*i+2] = sPtCur.ptRInImg.X;
		pPts[4*i+3] = sPtCur.ptRInImg.Y;
	}
	nPtSize = vecOutRes.size();

	return pPts;
	 
}
ML_EXTERN_C bool mlFreeSiftPts( double *pPts )
{
	if ( pPts != NULL )
	{
		delete[] pPts;
	}
	return true;
}
ML_EXTERN_C bool mlSiftMatchWHomo( const SCHAR* strLPath, const SCHAR* strRPath, DOUBLE dRatio, vector<StereoMatchPt> &vecOutRes )
{
    CmlFrameImage clsFrmL, clsFrmR;
    if( (false == clsFrmL.LoadFile( strLPath ))||( false == clsFrmR.LoadFile( strRPath ) ) )
    {
        return false;
    }
    vector<DOUBLE> vXL, vYL, vXR, vYR;
    SINT nNum = SiftMatchVector( (SCHAR*)clsFrmL.m_DataBlock.GetData(), clsFrmL.GetWidth(), clsFrmL.GetHeight(), 1, (SCHAR*)clsFrmR.m_DataBlock.GetData(), \
                           clsFrmR.GetWidth(), clsFrmR.GetHeight(), 1, vXL, vYL, vXR, vYR, 200, dRatio );
    for( UINT i = 0; i < (UINT)nNum; ++i )
    {
        StereoMatchPt ptCur;
        ptCur.ptLInImg.X = vXL[i];
        ptCur.ptLInImg.Y = vYL[i];
        ptCur.ptRInImg.X = vXR[i];
        ptCur.ptRInImg.Y = vYR[i];
        vecOutRes.push_back( ptCur );
    }
    return mlRansacPtsByHomo( vecOutRes );
}
ML_EXTERN_C bool mlIntersection(  Pt2d ptL, Pt2d ptR, Pt3d &ptXYZ, \
                                  InOriPara* pInOriL, ExOriPara* pExOriL, InOriPara* pInOriR, ExOriPara* pExOriR, DOUBLE dThres )
{
    CmlStereoProc clsStereoProc;
    return clsStereoProc.mlInterSection( ptL, ptR, ptXYZ, pInOriL, pExOriL, pInOriR, pExOriR, dThres );
}
MLAPI( bool )  mlIntersectionWithDis( Pt2d ptL, Pt2d ptR, SINT nHeightL, SINT nHeightR, Pt3d &ptXYZ, InOriPara* pInOriL, ExOriPara* pExOriL, InOriPara* pInOriR, ExOriPara* pExOriR, DOUBLE dThres  )
{
	CmlStereoProc clsStereoProc;
	return clsStereoProc.mlInterSection(ptL,ptR,nHeightL,nHeightR,ptXYZ,pInOriL,pExOriL,pInOriR,pExOriR,dThres);
}




ML_EXTERN_C bool mlIntersectionVT(   Pt2d ptL, Pt2d ptR,  ExOriPara exOriL, ExOriPara exOriR, DOUBLE df1, DOUBLE df2, Pt3d &ptXYZ, DOUBLE dCoefConf )
{
    CmlStereoProc clsStereoProc;
    CmlMat matLRot, matRRot;
    if( ( false == OPK2RMat( &exOriL.ori, &matLRot ) )||( false == OPK2RMat( &exOriR.ori, &matRRot ) ) )
    {
        return false;
    }
    return clsStereoProc.mlInterSection( ptL.X, ptL.Y, ptR.X, ptR.Y, &matLRot, &matRRot, exOriL.pos, exOriR.pos, df1, df2, ptXYZ, dCoefConf );
}
ML_EXTERN_C bool mlRelaOriCalcWithOrigPts( vector<StereoMatchPt> vecMatchPts, InOriPara inOriL, UINT nHL, InOriPara inOriR, UINT nHR, ExOriPara &exOriRela )
{
	CmlPhgProc clsPhg;

	return clsPhg.RelaOriCalcWithOrigPts(vecMatchPts, inOriL, nHL, inOriR, nHR, exOriRela );
}
ML_EXTERN_C bool mlIntersectionWithAccu(  Pt2d ptL, Pt2d ptR, DOUBLE dCoefConf, DOUBLE df1, DOUBLE df2, ExOriPara exOriL, ExOriPara exOriR, \
        Pt3d &ptXYZ, Pt3d &ptOutXYZAccu, DOUBLE &dOutTotalAccu )
{
    CmlStereoProc clsStereoProc;

    CmlMat matLRot, matRRot;
    if( ( false == OPK2RMat( &exOriL.ori, &matLRot))||( false == OPK2RMat( &exOriR.ori, &matRRot )) )
    {
        return false;
    }
    Pt2d ptLxyAccu, ptRxyAccu;
    if( true == clsStereoProc.mlInterSection( ptL.X, ptL.Y, ptR.X, ptR.Y, &matLRot, &matRRot, exOriL.pos, exOriR.pos, df1, df2, ptXYZ, ptLxyAccu, ptRxyAccu, ptOutXYZAccu, dCoefConf ) )
    {
        dOutTotalAccu = sqrt( ptOutXYZAccu.X * ptOutXYZAccu.X + ptOutXYZAccu.Y * ptOutXYZAccu.Y + ptOutXYZAccu.Z * ptOutXYZAccu.Z );
        return true;
    }
    return false;
}
ML_EXTERN_C bool mlResection( Pt2d *pImgPt,Pt3d *pGroundPt, double fx,double fy, SINT nPtNum,ExOriPara *pInitExOripara, ExOriPara *pExOripara )
{
	CmlPhgProc clsPhg;
	return clsPhg.mlBackForwardinterSection(pImgPt, pGroundPt, fx, fy, nPtNum, pInitExOripara, pExOripara );
}
ML_EXTERN_C bool mlResectionNoInitialVal(  vector<Pt3d> vecGCPs, vector<Pt2d> vecImgPts, DOUBLE dFocalLength, ExOriPara &exOriRes )
{
	CmlPhgProc clsPhg;
	return clsPhg.mlResectionNoInitalVal( vecGCPs, vecImgPts, dFocalLength, exOriRes );
}
ML_EXTERN_C bool mlSolvePts( vector<Pt3d> vecOldPts, vector<Pt3d> vecNewPts, UINT nTimes, ExOriPara* pInitialOri )
{
	CmlPhgProc clsPhg;
	return clsPhg.mlSolvePts( vecOldPts, vecNewPts, nTimes, pInitialOri );
}
ML_EXTERN_C bool mlSiteFindTiePoints( vector<StereoSet> vecStereoImg, vector<ImgPtSet> &vecTiePoints, ExtractFeatureOpt extractPtsOpts, MatchInRegPara matchOpts, RANSACHomePara ransacOpts, MedFilterOpts mFilterOpts )
{
    CmlSiteMapping clsSiteMapping;
    return clsSiteMapping.FindTiePoints( vecStereoImg, vecTiePoints, extractPtsOpts, matchOpts, ransacOpts, mFilterOpts );
}
ML_EXTERN_C bool mlBASolve( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<StereoSet> &vecStereoOut, UINT nModel )
{
    CmlSiteMapping clsSiteMapping;
    return clsSiteMapping.BASolve( vecStereoIn, vecImgPtSets, vecStereoOut, nModel );
}
ML_EXTERN_C bool mlBASolveWithErr( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<StereoSet> &vecStereoOut, Pt2d &ptProjErr, UINT nModel )
{
    CmlSiteMapping clsSiteMapping;
    return clsSiteMapping.BASolve( vecStereoIn, vecImgPtSets, vecStereoOut, ptProjErr, nModel );
}
ML_EXTERN_C bool mlBASolveVT( vector<StereoSet> vecStereoIn, vector<ImgPtSet> vecImgPtSets, vector<bool> vecbImgIsFixed, vector<StereoSet> &vecStereoOut, UINT nModel )
{
    CmlSiteMapping clsSiteMapping;
    return clsSiteMapping.BASolve( vecStereoIn, vecImgPtSets, vecbImgIsFixed, vecStereoOut, nModel );
}
ML_EXTERN_C bool mlPlaneNormal( vector<Pt3d> vecXYZ, Pt3d &pPlaneNormal )
{
    CmlPhgProc clsPhgProc;
    return clsPhgProc.GetFitPlaneNormal( vecXYZ, pPlaneNormal, pPlaneNormal) ;
}
ML_EXTERN_C bool mlCalcLandPtCoord( Pt2d ptObImgCentxy, vector<StereoMatchPt> vecStereoMPts, Pt2d &ptLandPosInBMap, DOUBLE &dPrecision )
{
    CmlPhgProc clsPhgProc;
    DOUBLE dA, dB, dC, dD, dXAccu, dYAccu;
    if( ( false == clsPhgProc.GetAffineTCoef( vecStereoMPts, dA, dB, dC, dD, dXAccu, dYAccu ) ) || \
            ( false == clsPhgProc.GetTargetPtPos( ptObImgCentxy, dA, dB, dC, dD, ptLandPosInBMap ) ) )
    {
        return false;
    }
    dPrecision = sqrt( dXAccu*dXAccu + dYAccu*dYAccu );
    return true;
}
ML_EXTERN_C bool mlDealSiteImgSelectionV( vector<StereoSet> vecSiteOne, vector<StereoSet> vecSiteTwo, UINT &nIDOne, UINT &nIDTwo )
{
    if( ( vecSiteOne.size() == 0 )||( vecSiteTwo.size() == 0) )
    {
        return false;
    }
    CmlLocalization clsLocal;
    return clsLocal.GetImgIDIn2Site( vecSiteOne, vecSiteTwo, nIDOne, nIDTwo );
}
ML_EXTERN_C bool mlDealSiteImgSelection( vector<StereoSet> vecSiteOne, vector<StereoSet> vecSiteTwo, StereoSet &sFSiteSet, StereoSet &sSSiteSet )
{
    if( ( vecSiteOne.size() == 0 )||( vecSiteTwo.size() == 0) )
    {
        return false;
    }
    CmlLocalization clsLocal;
    UINT nIDOne, nIDTwo;
    if( false == clsLocal.GetImgIDIn2Site( vecSiteOne, vecSiteTwo, nIDOne, nIDTwo ) )
    {
        return false;
    }
    sFSiteSet = vecSiteOne[nIDOne];
    sSSiteSet = vecSiteTwo[nIDTwo];
    return true;
}
ML_EXTERN_C bool mlDealSiteImgSelectionReverse( vector<StereoSet> vecSiteOne, vector<StereoSet> vecSiteTwo, StereoSet &sFSiteSet, StereoSet &sSSiteSet )
{
    if( ( vecSiteOne.size() == 0 )||( vecSiteTwo.size() == 0) )
    {
        return false;
    }
    CmlLocalization clsLocal;
    UINT nIDOne, nIDTwo;
    if( false == clsLocal.GetImgIDIn2SiteReverse( vecSiteOne, vecSiteTwo, nIDOne, nIDTwo ) )
    {
        return false;
    }
    sFSiteSet = vecSiteOne[nIDOne];
    sSSiteSet = vecSiteTwo[nIDTwo];
    return true;
}
ML_EXTERN_C bool mlSiteSIFTMatch( StereoSet sFirstSet, StereoSet sSecondSet, UINT nNumTilts, vector<Pt2d> &vecPtsInSiteOneLImg, vector<Pt2d> &vecPtsInSiteTwoLImg, DOUBLE dDisIn2SiteThres, DOUBLE dCamHeight )
{
    DOUBLE dLXTemp = sFirstSet.imgLInfo.exOri.pos.X;
    DOUBLE dLYTemp = sFirstSet.imgLInfo.exOri.pos.Y;
    DOUBLE dLZTemp = sFirstSet.imgLInfo.exOri.pos.Z;

    DOUBLE dRXTemp = sSecondSet.imgLInfo.exOri.pos.X;
    DOUBLE dRYTemp = sSecondSet.imgLInfo.exOri.pos.Y;
    DOUBLE dRZTemp = sSecondSet.imgLInfo.exOri.pos.Z;

    DOUBLE dBaseLine = sqrt( (dLXTemp-dRXTemp)*(dLXTemp-dRXTemp) + (dLYTemp-dRYTemp)*(dLYTemp-dRYTemp) + (dLZTemp-dRZTemp)*(dLZTemp-dRZTemp) );
    if( dBaseLine < dDisIn2SiteThres )//使用sift
    {
        vector<StereoMatchPt> vecOutRes;
        mlSiftMatchWHomo( sFirstSet.imgLInfo.strImgPath.c_str(), sSecondSet.imgLInfo.strImgPath.c_str(), 0.65, vecOutRes );
        for( UINT i = 0; i < vecOutRes.size(); ++i )
        {
            StereoMatchPt ptCur = vecOutRes[i];
            vecPtsInSiteOneLImg.push_back( ptCur.ptLInImg );
            vecPtsInSiteTwoLImg.push_back( ptCur.ptRInImg );
        }
    }
    else
    {
        CmlLocalization clsLocal;
        DOUBLE dRange = 0;
        clsLocal.getInsterestRegion( sFirstSet, sSecondSet, dCamHeight, (2+dBaseLine), 2, vecPtsInSiteOneLImg, vecPtsInSiteTwoLImg, nNumTilts );
    }
    return true;
}
ML_EXTERN_C bool mlSiteSIFTMatchReverse( StereoSet sFirstSet, StereoSet sSecondSet, UINT nNumTilts, vector<Pt2d> &vecPtsInSiteOneLImg, vector<Pt2d> &vecPtsInSiteTwoLImg, DOUBLE dDisIn2SiteThres, DOUBLE dCamHeight )
{
    DOUBLE dLXTemp = sFirstSet.imgLInfo.exOri.pos.X;
    DOUBLE dLYTemp = sFirstSet.imgLInfo.exOri.pos.Y;
    DOUBLE dLZTemp = sFirstSet.imgLInfo.exOri.pos.Z;

    DOUBLE dRXTemp = sSecondSet.imgLInfo.exOri.pos.X;
    DOUBLE dRYTemp = sSecondSet.imgLInfo.exOri.pos.Y;
    DOUBLE dRZTemp = sSecondSet.imgLInfo.exOri.pos.Z;

    DOUBLE dBaseLine = sqrt( (dLXTemp-dRXTemp)*(dLXTemp-dRXTemp) + (dLYTemp-dRYTemp)*(dLYTemp-dRYTemp) + (dLZTemp-dRZTemp)*(dLZTemp-dRZTemp) );
    if( dBaseLine < dDisIn2SiteThres )//使用sift
    {
        vector<StereoMatchPt> vecOutRes;
        mlSiftMatchWHomo( sFirstSet.imgLInfo.strImgPath.c_str(), sSecondSet.imgLInfo.strImgPath.c_str(), 0.65, vecOutRes );
        for( UINT i = 0; i < vecOutRes.size(); ++i )
        {
            StereoMatchPt ptCur = vecOutRes[i];
            vecPtsInSiteOneLImg.push_back( ptCur.ptLInImg );
            vecPtsInSiteTwoLImg.push_back( ptCur.ptRInImg );
        }
    }
    else
    {
        CmlLocalization clsLocal;
        DOUBLE dRange = 0;
//        clsLocal.getInsterestRegionReverse( sFirstSet, sSecondSet, dCamHeight, (2+dBaseLine), 2, vecPtsInSiteOneLImg, vecPtsInSiteTwoLImg, nNumTilts );
    }
    return true;
}
ML_EXTERN_C bool mlProjMap( StereoSet sFirstSet, StereoSet sSecondSet, DOUBLE dCamHeight, DOUBLE dRes, DOUBLE dRange, const SCHAR* strLProjMap, const SCHAR* strRProjMap )
{
    string strFirstLPath = sFirstSet.imgLInfo.strImgPath;
    string strSecondLPath = sSecondSet.imgLInfo.strImgPath;
    InOriPara inOriFirstL = sFirstSet.imgLInfo.inOri;
    InOriPara inOriSecondL = sSecondSet.imgLInfo.inOri;
    ExOriPara exOriFirstL = sFirstSet.imgLInfo.exOri;
    ExOriPara exOriSecondL = sSecondSet.imgLInfo.exOri;

    CmlPhgProc clsPhg;
    CmlRasterBlock clsBlockL, clsBlockR;
    Pt2d ptOrigFirstL, ptOrigSecondL;
    if( ( false == clsPhg.PersProjInFlat( strFirstLPath.c_str(), inOriFirstL, exOriFirstL, dRes, dRange, dCamHeight, ptOrigFirstL, clsBlockL ) ) ||\
        ( false == clsPhg.PersProjInFlat( strSecondLPath.c_str(), inOriSecondL, exOriSecondL, dRes, dRange, dCamHeight, ptOrigSecondL, clsBlockR )) )
    {
        return false;
    }
    CmlRasterBlock clsSubBlockL, clsSubBlockR;
    UINT nNewLX, nNewLY, nNewRX, nNewRY;
    clsSubBlockL.GetCentRasterBlock( clsBlockL, nNewLX, nNewLY, clsSubBlockL );
    clsSubBlockR.GetCentRasterBlock( clsBlockR, nNewRX, nNewRY, clsSubBlockR );

    ptOrigFirstL.X += nNewLX * dRes;
    ptOrigFirstL.Y -= nNewLY * dRes;

    ptOrigSecondL.X += nNewRX * dRes;
    ptOrigSecondL.Y -= nNewRY * dRes;

    CmlGeoRaster clsGL, clsGR;
    if( ( false == clsGL.CreateGeoFile( strLProjMap, ptOrigFirstL, dRes, -dRes, clsSubBlockL.GetH(), clsSubBlockL.GetW(), 1, GDT_Byte, 255 )) ||\
        ( false == clsGR.CreateGeoFile( strRProjMap, ptOrigSecondL, dRes, -dRes, clsSubBlockR.GetH(), clsSubBlockR.GetW(), 1, GDT_Byte, 255 )) )
    {
        return false;
    }
    if( ( false == clsGL.SaveToGeoFile( 1, 0, 0, &clsSubBlockL ) ) || \
        ( false == clsGR.SaveToGeoFile( 1, 0, 0, &clsSubBlockR ) ) )
    {
        return false;
    }
    return true;
}

ML_EXTERN_C bool mlAffineSIFT( const SCHAR* strLImgPath, const SCHAR* strRImgPath, UINT nNumTilts, vector<Pt2d> &vecPtsInSiteOneLImg, vector<Pt2d> &vecPtsInSiteTwoLImg )
{
    CmlStereoProc clsStereoProc;
    vector<StereoMatchPt> vecOutMPts;
    if( false == clsStereoProc.AffineSIFTMatch( strLImgPath, strRImgPath, nNumTilts, vecOutMPts ) )
    {
        return false;
    }
    for( UINT i = 0; i < vecOutMPts.size(); ++i )
    {
        StereoMatchPt ptCur = vecOutMPts[i];
        vecPtsInSiteOneLImg.push_back( ptCur.ptLInImg );
        vecPtsInSiteTwoLImg.push_back( ptCur.ptRInImg );
    }
    return true;
}
ML_EXTERN_C bool mlStereoMatchWithPtSet( const SCHAR* strL, const SCHAR* strR, vector<Pt2d> vecLPts, MatchInRegPara matchPara, vector<Pt2d> &vecRPts, vector<bool> &vecFlags )
{
    CmlFrameImage clsImgL, clsImgR;
    if( ( false == clsImgL.LoadFile( strL ) )||(( false == clsImgR.LoadFile( strR ) )) )
    {
        return false;
    }
    CmlStereoProc clsStereoProc;
    for( UINT i = 0; i < vecLPts.size(); ++i )
    {
        Pt2d ptLCur = vecLPts[i];
        Pt2d ptRCur;
        Pt2i ptLT, ptRT;
        DOUBLE dCoef = 0;
        ptLT.X = SINT( ptLCur.X );
        ptLT.Y = SINT( ptLCur.Y );
        if( true == clsStereoProc.mlTemplateMatchInRegion( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, ptLT, ptRT, dCoef, SINT(matchPara.dXMin), SINT(matchPara.dXMax), \
                SINT(matchPara.dYMin), SINT(matchPara.dYMax), matchPara.nTempSize, matchPara.dCoefThres, \
                SINT( matchPara.dXOff), SINT( matchPara.dYOff) ) )
        {
            ptRCur.X = ptRT.X;
            ptRCur.Y = ptRT.Y;
            clsStereoProc.mlLsMatch( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, ptLCur.X, ptLCur.Y, ptRCur.X, ptRCur.Y, matchPara.nTempSize, dCoef );
            vecFlags.push_back( true );
        }
        else
        {
            ptRCur.X = 0;
            ptRCur.Y = 0;
            vecFlags.push_back( false );
        }
        vecRPts.push_back( ptRCur );
    }
    return true;
}
ML_EXTERN_C bool mlGetMatchedPtsInTwoSites( vector<Pt2d> vecPtsInSiteOneLImg,    //第一站左图像所得到的点坐标，由ASIFT得到
        vector<Pt2d> vecPtsInSiteOneRImg ,   //第一站右图像所得到的点坐标，由左影像坐标根据相关系数得到
        vector<bool> vecValidFlagInSiteOne,  // 由于相关系数不一定成功，故由匹配时输出的匹配情况（见步骤四）在此进行输入，以判断哪些左影像点是存在对应右影像匹配点的

        vector<Pt2d> vecPtsInSiteTwoLImg,    //同上，为第二站的情况，均为输入
        vector<Pt2d> vecPtsInSiteTwoRImg,
        vector<bool> vecValidFlagInSiteTwo,

        vector<StereoMatchPt> &vecSMPtsInSiteOne,  //第一站中的匹配点对
        vector<StereoMatchPt> &vecSMPtsInSiteTwo  //第二站中的匹配点对，其中，其与第一站中的点对存在对应关系，即 两个数组同一序号 表示 四点 同名
                                          )
{
    UINT nPtNum = vecPtsInSiteOneLImg.size();
    if( ( nPtNum != vecPtsInSiteOneRImg.size() )||( nPtNum != vecPtsInSiteTwoLImg.size() )||( nPtNum != vecPtsInSiteTwoRImg.size() )|| \
            ( nPtNum != vecValidFlagInSiteOne.size() ) || ( nPtNum != vecValidFlagInSiteTwo.size() ) )
    {
        return false;
    }
    for( UINT i = 0; i < nPtNum; ++i )
    {
        if( ( true == vecValidFlagInSiteOne[i] ) && ( true == vecValidFlagInSiteTwo[i] ) )
        {
            StereoMatchPt ptOne, ptTwo;
            ptOne.ptLInImg = vecPtsInSiteOneLImg[i];
            ptOne.ptRInImg = vecPtsInSiteOneRImg[i];
            ptTwo.ptLInImg = vecPtsInSiteTwoLImg[i];
            ptTwo.ptRInImg = vecPtsInSiteTwoRImg[i];
            vecSMPtsInSiteOne.push_back( ptOne );
            vecSMPtsInSiteTwo.push_back( ptTwo );
        }
    }
    return true;

}
ML_EXTERN_C bool mlDeletePtsByDisConsist( Pt3d ptCentXYZSiteOne, vector<Pt3d> vecPtXYZInSiteOne, Pt3d ptCentXYZSiteTwo, vector<Pt3d> vecPtXYZInSiteTwo, DOUBLE dDisThres, DOUBLE dWeightThres, vector<bool> &vecFlags )
{
    CmlLocalization clsLocal;
    return clsLocal.removePtsByDisConsistant( vecPtXYZInSiteOne, ptCentXYZSiteOne, vecPtXYZInSiteTwo, ptCentXYZSiteTwo, dDisThres, dWeightThres, vecFlags );

}
ML_EXTERN_C bool mlCalcCamOriByGivenInstallMatrix( DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
        stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
        ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    CmlPhgProc clsPhg;
    return clsPhg.CalcCamOriByGivenInstallMatrix( dExpAngle, dYawAngle, dPitchAngle, matMastExp2Body, matMastYaw2Exp, matMastPitch2Yaw, \
            matLCamBase2Pitch, matRCamBase2LCamBase, matLCamCap2Base, matRCamCap2Base, exOriCamL, exOriCamR );
}
ML_EXTERN_C bool mlPt3dTrans( Pt3d ptXYZOrig, ExOriPara exOri, Pt3d &ptXYZRes )
{
    CmlMat matR;
    if( false == OPK2RMat(&exOri.ori, &matR) )
    {
        return false;
    }
    ptXYZRes.X = matR.GetAt( 0, 0 )*ptXYZOrig.X + matR.GetAt( 0, 1 )*ptXYZOrig.Y + matR.GetAt( 0, 2 )*ptXYZOrig.Z + exOri.pos.X;
    ptXYZRes.Y = matR.GetAt( 1, 0 )*ptXYZOrig.X + matR.GetAt( 1, 1 )*ptXYZOrig.Y + matR.GetAt( 1, 2 )*ptXYZOrig.Z + exOri.pos.Y;
    ptXYZRes.Z = matR.GetAt( 2, 0 )*ptXYZOrig.X + matR.GetAt( 2, 1 )*ptXYZOrig.Y + matR.GetAt( 2, 2 )*ptXYZOrig.Z + exOri.pos.Z;
    return true;
}
ML_EXTERN_C bool mlPt3dTransBody2Cam( Pt3d ptXYZOrig, ExOriPara exOri, Pt3d &ptXYZRes )
{
    CmlMat matR;
    if( false == OPK2RMat(&exOri.ori, &matR) )
    {
        return false;
    }
    ptXYZRes.X = matR.GetAt( 0, 0 )*ptXYZOrig.X + matR.GetAt( 1, 0 )*ptXYZOrig.Y + matR.GetAt( 2, 0 )*ptXYZOrig.Z -(  matR.GetAt( 0, 0 )*exOri.pos.X + matR.GetAt( 1, 0 )*exOri.pos.Y + matR.GetAt( 2, 0 )*exOri.pos.Z );
    ptXYZRes.Y = matR.GetAt( 0, 1 )*ptXYZOrig.X + matR.GetAt( 1, 1 )*ptXYZOrig.Y + matR.GetAt( 2, 1 )*ptXYZOrig.Z -(  matR.GetAt( 0, 1 )*exOri.pos.X + matR.GetAt( 1, 1 )*exOri.pos.Y + matR.GetAt( 2, 1 )*exOri.pos.Z );
    ptXYZRes.Z = matR.GetAt( 0, 2 )*ptXYZOrig.X + matR.GetAt( 1, 2 )*ptXYZOrig.Y + matR.GetAt( 2, 2 )*ptXYZOrig.Z -(  matR.GetAt( 0, 2 )*exOri.pos.X + matR.GetAt( 1, 2 )*exOri.pos.Y + matR.GetAt( 2, 2 )*exOri.pos.Z );
    return true;
}
ML_EXTERN_C bool mlCalcCamOriInWorldByGivenInsMat( stuInsMat matBase, DOUBLE dExpAngle, DOUBLE dYawAngle, DOUBLE dPitchAngle, stuInsMat matMastExp2Body, stuInsMat matMastYaw2Exp, stuInsMat matMastPitch2Yaw, \
        stuInsMat matLCamBase2Pitch, stuInsMat matRCamBase2LCamBase, stuInsMat matLCamCap2Base, stuInsMat matRCamCap2Base, \
        ExOriPara &exOriCamL, ExOriPara &exOriCamR )
{
    CmlPhgProc clsPhg;
    return clsPhg.CalcCamOriInWorldByGivenInsMat( matBase, dExpAngle, dYawAngle, dPitchAngle, matMastExp2Body, matMastYaw2Exp, matMastPitch2Yaw, \
            matLCamBase2Pitch, matRCamBase2LCamBase, matLCamCap2Base, matRCamCap2Base, exOriCamL, exOriCamR );
}
ML_EXTERN_C bool mlGeoFileTrans( const SCHAR* pImgIn, const SCHAR* pImgOut, ImgDotType imgType )
{
    CmlGeoRaster geoRaster, geoRasterNew;
    if( false == geoRaster.LoadGeoFile( pImgIn ) )
    {
        return false;
    }


    CmlRasterBlock rasterB, rasterBNew;

    if( false == geoRaster.GetRasterOriginBlock( UINT(1), UINT(0), UINT(0), geoRaster.GetWidth(), geoRaster.GetHeight(), UINT(1), &rasterB ) )

    {
        return false;
    }
    rasterBNew.SetGDTType( GDALDataType(imgType) );
    UINT nBytes = 0;
    switch( imgType )
    {
    case T_Byte:
        nBytes = 1;
        break;
    case T_UInt16:
        nBytes = 2;
        break;
    case T_Int16:
        nBytes = 2;
        break;
    case T_UInt32:
        nBytes = 4;
        break;
    case T_Int32:
        nBytes = 4;
        break;
    case T_Float32:
        nBytes = 4;
        break;
    case T_Float64:
        nBytes = 8;
        break;
    default:
        break;
    }
    if( nBytes == 0 )
    {
        return false;
    }
    rasterBNew.InitialImg( geoRaster.GetHeight(), geoRaster.GetWidth(), nBytes );
    for( UINT i = 0; i < geoRaster.GetHeight(); ++i )
    {
        for( UINT j = 0; j < geoRaster.GetWidth(); ++j )
        {
            DOUBLE dCurVal;
            if( true == rasterB.GetDoubleVal( i, j, dCurVal ) )
            {
//                if( dCurVal < 10000000 )
//
//                {
//                    dCurVal *= -1.0;
//                }
                rasterBNew.SetDoubleVal( i, j, dCurVal);
            }

        }
    }
    DOUBLE dNoTemp = geoRaster.GetNoDataVal();
     switch( imgType )
    {
    case T_Byte:
        dNoTemp = (BYTE)(dNoTemp);
        break;
    case T_UInt16:
        dNoTemp = (USHORT)(dNoTemp);
        break;
    case T_Int16:
        dNoTemp = (SSHORT)(dNoTemp);
        break;
    case T_UInt32:
        dNoTemp = (UINT)(dNoTemp);
        break;
    case T_Int32:
        dNoTemp = (SINT)(dNoTemp);
        break;
    case T_Float32:
       dNoTemp = (FLOAT)(dNoTemp);
        break;
    case T_Float64:
        dNoTemp = (DOUBLE)(dNoTemp);
        break;
    default:
        break;
    }

    if( false == geoRasterNew.CreateGeoFile( pImgOut, geoRaster.m_PtOrigin, geoRaster.m_dXResolution, geoRaster.m_dYResolution, geoRaster.GetHeight(), \
            geoRaster.GetWidth(), 1, GDALDataType(imgType), dNoTemp ) )
    {
        return false;
    }
    geoRasterNew.SaveBlockToFile( UINT(1), UINT(0), UINT(0), &rasterBNew );

    return true;
}
ML_EXTERN_C bool mlCalcSiteOirByStereoMPts( vector<StereoMatchPt> vecMatchedPtsInSiteOne,   //第一站匹配点对
                                        vector<StereoMatchPt> vecMatchedPtsInSiteTwo,   //第二站匹配点对，同vecMatchedPtsInSiteOne是一一对应的同名点关系
                                        StereoSet imgInSiteOne,           //输入第一站影像的内、外方位元素
                                        StereoSet imgInSiteTwo,           //输入第2站影像的内、外方位元素
                                        ExOriPara  exOriSiteTwoOrgin,      //输入第2站中 探测车 初始的 外方位（由于影像外方位不同于车的外方位，故通过前后影像外方位的变化改正 车的外方位）

                                        ExOriPara  &exOriSiteTwoModefied,  //最终所需要的车的外方位结果
                                        Pt3d      &rmsXYZ,                //定位XYZ中误差
                                        DOUBLE    &dTotalRMS,             //总中误差
                                        CAMTYPE   nCamTypeFirst,
                                        CAMTYPE   nCamTypeSecond
                                        )
{
    CmlFrameImage clsFrm;
    vector<bool> vecValids;
    vector<Pt3d> vecPtXYZ;
    for( UINT i = 0; i < vecMatchedPtsInSiteOne.size(); ++i )
    {
        StereoMatchPt pt = vecMatchedPtsInSiteOne[i];
        Pt2d ptL, ptR;
        if( ( false == mlUnDistort( pt.ptLInImg, imgInSiteOne.imgLInfo.nH, imgInSiteOne.imgLInfo.inOri, ptL, nCamTypeFirst ) ) ||
            ( false == mlUnDistort( pt.ptRInImg, imgInSiteOne.imgRInfo.nH, imgInSiteOne.imgRInfo.inOri, ptR, nCamTypeSecond ) ) )
        {
            vecValids.push_back( false );
            continue;
        }
        Pt3d ptXYZ;
        if( false == mlIntersectionVT( ptL, ptR, imgInSiteOne.imgLInfo.exOri, imgInSiteOne.imgRInfo.exOri, imgInSiteOne.imgLInfo.inOri.f, \
                                       imgInSiteOne.imgRInfo.inOri.f, ptXYZ ) )
        {
            vecValids.push_back( false );
            continue;
        }
        vecPtXYZ.push_back( ptXYZ );
        vecValids.push_back( true );
    }
    vector<Pt2d> vecImgPts;
    for( UINT i = 0; i < vecValids.size(); ++i )
    {
        if( true == vecValids[i] )
        {
            StereoMatchPt ptCur = vecMatchedPtsInSiteTwo[i];
            Pt2d ptTmp;
            mlUnDistort( ptCur.ptLInImg, imgInSiteTwo.imgLInfo.nH, imgInSiteTwo.imgLInfo.inOri, ptTmp );
            vecImgPts.push_back( ptTmp );
        }
    }
    CmlPhgProc clsPhg;ExOriPara exOriRes;
    if( false == clsPhg.mlBackForwardinterSection( vecImgPts, vecPtXYZ, imgInSiteTwo.imgLInfo.inOri.f, &imgInSiteTwo.imgLInfo.exOri, &exOriRes ) )
    {
        return false;
    }
    ExOriPara exOriRela;
    mlGetRelaOri( &imgInSiteTwo.imgLInfo.exOri, &exOriSiteTwoOrgin, &exOriRela );
    clsPhg.ExOriTrans( &exOriRes, &exOriRela, &exOriSiteTwoModefied );

    return true;
}
ML_EXTERN_C bool mlCalcSiteOirByStereoMPtsVT( vector<StereoMatchPt> vecMatchedPtsInSiteOne,   //第一站匹配点对
                                        vector<StereoMatchPt> vecMatchedPtsInSiteTwo,   //第二站匹配点对，同vecMatchedPtsInSiteOne是一一对应的同名点关系
                                        StereoSet imgInSiteOne,           //输入第一站影像的内、外方位元素
                                        StereoSet imgInSiteTwo,           //输入第2站影像的内、外方位元素
                                        ExOriPara  exOriSiteTwoOrgin,      //输入第2站中 探测车 初始的 外方位（由于影像外方位不同于车的外方位，故通过前后影像外方位的变化改正 车的外方位）

                                        ExOriPara  &exOriSiteTwoModefied,  //最终所需要的车的外方位结果
                                        Pt3d      &rmsXYZ,                //定位XYZ中误差
                                        DOUBLE    &dTotalRMS,             //总中误差
                                        CAMTYPE   nCamTypeFirst,
                                        CAMTYPE   nCamTypeSecond
                                        )
{
    UINT nImgNum = vecMatchedPtsInSiteOne.size();
    if( nImgNum != vecMatchedPtsInSiteTwo.size() )
    {
        return false;
    }
    vector<InOriPara> vecImgInOris;
    vector< vector<Pt2d> > vecImgPts;
    vector<bool> vecbImgIsFixed;
    vector<bool> vecb3dPtIsFixed;
    vector<ExOriPara> vecImgExOris;
    vector< Pt3d > vec3dPts;

    vector<ExOriPara> vecImgResErr;
    vector<Pt3d> vec3dPtsResErr;
    Pt2d ptTotalImgResErr;

    vector<Pt2d> imgPtsOneL, imgPtsOneR, imgPtsTwoL, imgPtsTwoR;


    vecImgInOris.push_back( imgInSiteOne.imgLInfo.inOri );
    vecImgInOris.push_back( imgInSiteOne.imgRInfo.inOri );
    vecImgInOris.push_back( imgInSiteTwo.imgLInfo.inOri );
    vecImgInOris.push_back( imgInSiteTwo.imgRInfo.inOri );

    vecImgExOris.push_back( imgInSiteOne.imgLInfo.exOri );
    vecImgExOris.push_back( imgInSiteOne.imgRInfo.exOri );
    vecImgExOris.push_back( imgInSiteTwo.imgLInfo.exOri );
    vecImgExOris.push_back( imgInSiteTwo.imgRInfo.exOri );

    vecbImgIsFixed.push_back( true );
    vecbImgIsFixed.push_back( true );
    vecbImgIsFixed.push_back( false );
    vecbImgIsFixed.push_back( false );

    CmlStereoProc clsStereoProc;

    for( UINT i = 0; i < vecMatchedPtsInSiteOne.size(); ++i )
    {

        StereoMatchPt &sPtCurOne = vecMatchedPtsInSiteOne[i];
        StereoMatchPt &sPtCurTwo = vecMatchedPtsInSiteTwo[i];

        mlUnDistort( sPtCurOne.ptLInImg, imgInSiteOne.imgLInfo.nH, imgInSiteOne.imgLInfo.inOri, sPtCurOne.ptLInImg );
        imgPtsOneL.push_back( sPtCurOne.ptLInImg );

        mlUnDistort( sPtCurOne.ptRInImg, imgInSiteOne.imgRInfo.nH, imgInSiteOne.imgRInfo.inOri, sPtCurOne.ptRInImg );
        imgPtsOneR.push_back( sPtCurOne.ptRInImg );

        mlUnDistort( sPtCurTwo.ptLInImg, imgInSiteTwo.imgLInfo.nH, imgInSiteTwo.imgLInfo.inOri, sPtCurTwo.ptLInImg );
        imgPtsTwoL.push_back( sPtCurTwo.ptLInImg );

        mlUnDistort( sPtCurTwo.ptRInImg, imgInSiteTwo.imgRInfo.nH, imgInSiteTwo.imgRInfo.inOri, sPtCurTwo.ptRInImg );
        imgPtsTwoR.push_back( sPtCurTwo.ptRInImg );

        Pt3d ptXYZ;
        clsStereoProc.mlInterSection( sPtCurOne.ptLInImg, sPtCurOne.ptRInImg, ptXYZ, &imgInSiteOne.imgLInfo.inOri, &imgInSiteOne.imgLInfo.exOri, \
                                      &imgInSiteOne.imgRInfo.inOri, &imgInSiteOne.imgRInfo.exOri );
        vec3dPts.push_back( ptXYZ );
        vecb3dPtIsFixed.push_back( false );
    }
    vecImgPts.push_back( imgPtsOneL );
    vecImgPts.push_back( imgPtsOneR );
    vecImgPts.push_back( imgPtsTwoL );
    vecImgPts.push_back( imgPtsTwoR );

    CmlBA clsBA;
    if( false == clsBA.BA_Common( vecImgInOris, vecImgPts, vecbImgIsFixed, vecb3dPtIsFixed, vecImgExOris, vec3dPts, vecImgResErr, vec3dPtsResErr, ptTotalImgResErr ))
    {
        return false;
    }

    CmlPhgProc clsPhg;
    ExOriPara exOriRela;
    mlGetRelaOri( &imgInSiteTwo.imgLInfo.exOri, &exOriSiteTwoOrgin, &exOriRela );
    clsPhg.ExOriTrans( &vecImgExOris[2], &exOriRela, &exOriSiteTwoModefied );

    rmsXYZ = vecImgResErr[2].pos;

    dTotalRMS = sqrt( rmsXYZ.X*rmsXYZ.X+rmsXYZ.Y*rmsXYZ.Y+rmsXYZ.Z*rmsXYZ.Z );

    return true;
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
* @param[in] dCoefThres 相关系数阈值
* @param[out] vecOutMRes 匹配结果
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
ML_EXTERN_C bool  mlDenseMatchPyra( const SCHAR* strInPutLImgPath, const SCHAR* strInPutRImgPath, vector<StereoMatchPt> vecFeatMPts, UINT nStep, UINT nRadiusX, UINT nRadiusY, SINT nXOff, SINT nYOff, \
                                    UINT nTemplateSize, DOUBLE dCoefThres, vector<StereoMatchPt> &vecOutMRes,SINT nlevel )
{
    CmlStereoProc clsStereoProc;
    CmlFrameImage clsImgL, clsImgR;

    if( ( false == clsImgL.LoadFile( strInPutLImgPath ) )||( false == clsImgR.LoadFile( strInPutRImgPath )) )
    {
        return false;
    }
    WideOptions wOpts;
    wOpts.nStep = nStep;
    wOpts.nRadiusX = nRadiusX;
    wOpts.nRadiusY = nRadiusY;
    wOpts.nXOffSet = nXOff;
    wOpts.nYOffSet = nYOff;
    wOpts.nTemplateSize = nTemplateSize;
    wOpts.dCoef = dCoefThres;

    vector<Pt2d> vecOutLRes, vecOutRRes;
    vector<DOUBLE> vecOutCorr;
    if( false == clsStereoProc.mlDenseMatchPyra( &clsImgL.m_DataBlock, &clsImgR.m_DataBlock, vecFeatMPts, wOpts, vecOutLRes, vecOutRRes, nlevel ) )
    {
        return false;
    }

    for( UINT i = 0; i < vecOutLRes.size(); ++i )
    {
        StereoMatchPt ptCur;
        ptCur.ptLInImg = vecOutLRes[i];
        ptCur.ptRInImg = vecOutRRes[i];
        ptCur.dRelaCoef = vecOutCorr[i];
        vecOutMRes.push_back( ptCur );
    }
    return true;
}
ML_EXTERN_C bool mlGeoRasterVerify( const SCHAR* strDemIn,  const SCHAR* strDomIn,  const SCHAR* strDemOut )
{
    CmlGeoRaster demRasterIn, domRasterIn, demRasterOut;
    if( ( false == demRasterIn.LoadGeoFile( strDemIn ) )||( false == domRasterIn.LoadGeoFile( strDomIn ) ) )
    {
        return false;
    }
    CmlRasterBlock demRaster, domRaster;
    if( ( false == demRasterIn.GetRasterOriginBlock( 1, 0, 0, demRasterIn.GetWidth(), demRasterIn.GetHeight(), UINT(1),&demRaster ) )||\
      ( false == domRasterIn.GetRasterOriginBlock( 1, 0, 0, domRasterIn.GetWidth(), domRasterIn.GetHeight(), UINT(1),&domRaster) ) )
    {
      return false;
    }
    DOUBLE dNoT = demRasterIn.GetNoDataVal();
    for( UINT i = 0; i < demRaster.GetH(); ++i )
    {
        for( UINT j = 0; j < demRaster.GetW(); ++j )
        {
            BYTE bCurVal = domRaster.GetAt( i, j );
            if( bCurVal == 255 )
            {
                demRaster.SetDoubleVal( i, j , dNoT );
            }
        }
    }

    demRasterOut.CreateGeoFile( strDemOut, demRasterIn.m_PtOrigin, demRasterIn.m_dXResolution, demRasterIn.m_dYResolution, demRasterIn.GetHeight(), \
                                demRasterIn.GetWidth(), 1, demRasterIn.GetGDTType(), dNoT );
    demRasterOut.SaveToGeoFile( 1, 0, 0, &demRaster );
    return true;
}
ML_EXTERN_C bool mlGetGeoInfo( const SCHAR* strGeoFile, Pt2d &ptOrig, DOUBLE &dXRes, DOUBLE &dYRes, UINT &nW, UINT &nH, DOUBLE &dNoDataVal )
{
    CmlGeoRaster clsGeoR;
    if( false == clsGeoR.LoadGeoFile( strGeoFile ) )
    {
        return false;
    }
    ptOrig = clsGeoR.m_PtOrigin;
    dXRes = clsGeoR.m_dXResolution;
    dYRes = clsGeoR.m_dYResolution;

    nW = clsGeoR.GetWidth();
    nH = clsGeoR.GetHeight();

    dNoDataVal = clsGeoR.GetNoDataVal();
    return true;
}
ML_EXTERN_C bool mlSetGeoInfo( const SCHAR* strGeoFile, Pt2d ptOrig, DOUBLE dXRes, DOUBLE dYRes, DOUBLE dNoDataVal )
{
	return true;
}
ML_EXTERN_C bool mlGeoMergeInRegion( const SCHAR* strBaseMap, const SCHAR* strToProceedMap, const SCHAR* strOutRes, vector<Polygon2d> vecPolygon )
{
    CmlGeoRaster clsGeoProcMap, clsGeoBaseMap;
    if( ( false == clsGeoProcMap.LoadGeoFile( strToProceedMap ) )|| ( false == clsGeoBaseMap.LoadGeoFile( strBaseMap ) ) )
    {
        return false;
    }
    CmlRasterBlock clsRBase, clsRToProcess;
    CmlTIN clsTin;
    if( ( false == clsGeoBaseMap.GetRasterOriginBlock( UINT(1), UINT(0), UINT(0), clsGeoBaseMap.GetWidth(), clsGeoBaseMap.GetHeight(), UINT(1), &clsRBase))||\
        ( false == clsGeoProcMap.GetRasterOriginBlock( UINT(1), UINT(0), UINT(0), clsGeoProcMap.GetWidth(), clsGeoProcMap.GetHeight(), UINT(1), &clsRToProcess)) )
    {
        return false;
    }
    for( UINT i = 0; i < clsRToProcess.GetH(); ++i )
    {
        for( UINT j = 0; j < clsRToProcess.GetW(); ++j )
        {
            DOUBLE dCurVal = 0;
            if( true == clsRToProcess.GetDoubleVal( i, j , dCurVal ) )
            {
                Pt2d ptCur;
                ptCur.X = clsGeoProcMap.m_PtOrigin.X + j * clsGeoProcMap.m_dXResolution;
                ptCur.Y = clsGeoProcMap.m_PtOrigin.Y + i * clsGeoProcMap.m_dYResolution;

                if( fabs( dCurVal - clsGeoProcMap.GetNoDataVal() ) < ML_ZERO )
                {
                    bool bIsIn = false;
                    for( UINT k = 0; k < vecPolygon.size(); ++k )
                    {
                        if( 0 != clsTin.PointInPoly( ptCur.X, ptCur.Y, &(vecPolygon[k].vecVectex )))
                        {
                            bIsIn = true;
                        }
                    }
                    DOUBLE dNewZ = 0;
                    if( ( true == bIsIn )&&( true == clsRBase.GetGeoZ( ptCur.X, ptCur.Y, dNewZ ) ) )
                    {
                        clsRToProcess.SetDoubleVal( i, j , dNewZ );
                    }
                }
            }
        }
    }
    CmlGeoRaster clsGeoOut;
    if( ( false == clsGeoOut.CreateGeoFile( strOutRes, clsGeoProcMap.m_PtOrigin, clsGeoProcMap.m_dXResolution, clsGeoProcMap.m_dYResolution, \
                                          clsGeoProcMap.GetHeight(), clsGeoProcMap.GetWidth(), 1, clsGeoProcMap.GetGDTType(), clsGeoProcMap.GetNoDataVal()))|| \
        ( false == clsGeoOut.SaveToGeoFile( 1, 0, 0, &clsRToProcess ) ) )
    {
        return false;
    }
    return true;
}

ML_EXTERN_C bool mlGeoMerge( const SCHAR* strBaseMap, const SCHAR* strToProceedMap, const SCHAR* strOutRes, bool bIsDem )
{
    CmlGeoRaster clsGeoProcMap, clsGeoBaseMap;
    if( ( false == clsGeoProcMap.LoadGeoFile( strToProceedMap ) )|| ( false == clsGeoBaseMap.LoadGeoFile( strBaseMap ) ) )
    {
        return false;
    }
    CmlRasterBlock clsRBase, clsRToProcess;
    CmlTIN clsTin;
    if( ( false == clsGeoBaseMap.GetRasterOriginBlock( UINT(1), UINT(0), UINT(0), clsGeoBaseMap.GetWidth(), clsGeoBaseMap.GetHeight(), UINT(1), &clsRBase))||\
        ( false == clsGeoProcMap.GetRasterOriginBlock( UINT(1), UINT(0), UINT(0), clsGeoProcMap.GetWidth(), clsGeoProcMap.GetHeight(), UINT(1), &clsRToProcess)) )
    {
        return false;
    }
    DOUBLE dOrigNoVal = clsGeoProcMap.GetNoDataVal();
    if( clsGeoProcMap.GetBytes() == 1 )
    {
        dOrigNoVal = 255;
    }
    for( UINT i = 0; i < clsRToProcess.GetH(); ++i )
    {
        for( UINT j = 0; j < clsRToProcess.GetW(); ++j )
        {
            DOUBLE dCurVal = 0;
            if( true == clsRToProcess.GetDoubleVal( i, j , dCurVal ) )
            {
                Pt2d ptCur;
                ptCur.X = clsGeoProcMap.m_PtOrigin.X + j * clsGeoProcMap.m_dXResolution;
                ptCur.Y = clsGeoProcMap.m_PtOrigin.Y + i * clsGeoProcMap.m_dYResolution;

                if( fabs( dCurVal - dOrigNoVal ) < ML_ZERO )
                {
                    DOUBLE dNewZ = 0;
                    if( true == clsRBase.GetGeoZ( ptCur.X / 1000.0, ptCur.Y / 1000.0, dNewZ ) )
                    {
                        if( bIsDem == true )
                        {
                            dNewZ *= 1000;
                        }
                        clsRToProcess.SetDoubleVal( i, j , dNewZ );
                    }
                }
            }
        }
    }
    CmlGeoRaster clsGeoOut;
    if( ( false == clsGeoOut.CreateGeoFile( strOutRes, clsGeoProcMap.m_PtOrigin, clsGeoProcMap.m_dXResolution, clsGeoProcMap.m_dYResolution, \
                                          clsGeoProcMap.GetHeight(), clsGeoProcMap.GetWidth(), 1, clsGeoProcMap.GetGDTType(), clsGeoProcMap.GetNoDataVal()))|| \
        ( false == clsGeoOut.SaveToGeoFile( 1, 0, 0, &clsRToProcess ) ) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlConvertGeoFileYRes( const SCHAR* strSrcFile, const SCHAR* strDstFile )
{
    CmlGeoRaster geoSrcRData;
    ULONG lGEO_BLOCK_SIZE = 10000000;
    if( false == geoSrcRData.LoadGeoFile( strSrcFile ) )
    {
        return false;
    }
    Pt2d ptSrcOrig = geoSrcRData.m_PtOrigin;
    DOUBLE dSrcXRes = geoSrcRData.m_dXResolution;
    DOUBLE dSrcYRes = geoSrcRData.m_dYResolution;
    UINT nSrcW = geoSrcRData.GetWidth();
    UINT nSrcH = geoSrcRData.GetHeight();

    Pt2d ptDstOrig;
    ptDstOrig.X = ptSrcOrig.X;
    ptDstOrig.Y = ptSrcOrig.Y + dSrcYRes*nSrcH;
    DOUBLE dDstXRes = dSrcXRes;
    DOUBLE dDstYRes = -1.0*dSrcYRes;
    UINT nDstW = nSrcW;
    UINT nDstH = nSrcH;

    ULONG lGeoSize = nSrcH*nSrcW;

    UINT nBlockNum = lGeoSize / lGEO_BLOCK_SIZE;
    if( 0 != lGeoSize % lGEO_BLOCK_SIZE )
    {
        ++nBlockNum;
    }
    UINT nBlockW = nSrcW;
    UINT nBlockH = lGEO_BLOCK_SIZE / nBlockW;

    CmlGeoRaster geoDstRData;
    if( false == geoDstRData.CreateGeoFile( strDstFile, ptDstOrig, dDstXRes, dDstYRes, nDstH, nDstW, geoSrcRData.GetBands(), geoSrcRData.GetGDTType(), geoSrcRData.GetNoDataVal()) )
    {
        return false;
    }
    for( UINT i = 0; i< nBlockNum; ++i )
    {
        CmlRasterBlock clsSrcBlock, clsDstBlock;
        if( i < (nBlockNum - 1) )
        {
            if( false == geoSrcRData.GetRasterOriginBlock( UINT(1), 0, UINT(i*nBlockH), UINT(nBlockW), UINT(nBlockH), UINT(1), &clsSrcBlock ) )
            {
                return false;
            }
            clsDstBlock.InitialImg( nBlockH, nBlockW, geoSrcRData.GetBytes() );
            clsDstBlock.SetGDTType( geoSrcRData.GetGDTType() );
            for( UINT j = 0; j < clsSrcBlock.GetH(); ++j )
            {
                memcpy( ( clsDstBlock.GetData() + (clsSrcBlock.GetH()-1-j)*clsSrcBlock.GetBytes() ), (clsSrcBlock.GetData()+(j*clsSrcBlock.GetW())*clsSrcBlock.GetBytes()), \
                        ( clsSrcBlock.GetW()*clsSrcBlock.GetBytes() ) );
            }
            geoDstRData.SaveToGeoFile( UINT(1), 0, (UINT)(nDstH-(i+1)*nBlockH), &clsDstBlock );
        }
        else
        {
            UINT nCurH = nSrcH - i*nBlockH;
            if( false == geoSrcRData.GetRasterOriginBlock( UINT(1), 0, UINT(i*nBlockH), UINT(nBlockW), nCurH, UINT(1), &clsSrcBlock ) )
            {
                return false;
            }
            clsDstBlock.InitialImg( nCurH, nBlockW, geoSrcRData.GetBytes() );
            clsDstBlock.SetGDTType( geoSrcRData.GetGDTType() );
            for( UINT j = 0; j < clsSrcBlock.GetH(); ++j )
            {
                memcpy( ( clsDstBlock.GetData() + (clsSrcBlock.GetH()-1-j)*clsSrcBlock.GetBytes() ), (clsSrcBlock.GetData()+(j*clsSrcBlock.GetW())*clsSrcBlock.GetBytes()), \
                        ( clsSrcBlock.GetW()*clsSrcBlock.GetBytes() ) );
            }
            geoDstRData.SaveToGeoFile( UINT(1), 0, 0, &clsDstBlock );
        }
    }
    return true;

}
ML_EXTERN_C bool mlCalcDisparityPerRow( DOUBLE dUnitDis, DOUBLE dCamHeight, UINT nImgH, UINT nMinStep, UINT nMaxStep, UINT nBlockSize, DOUBLE df, ExOriPara exOriL, stuBlockDeal &stuBDRes )
{
    UINT nBlockH = 0;
    DOUBLE dBTmp = DOUBLE(nImgH) / DOUBLE(nBlockSize);
    if( 0 != (nImgH % nBlockSize ) )
    {
        nBlockH = UINT(dBTmp+1);
    }
    else
    {
        nBlockH = nImgH / nBlockSize;
    }
    stuBDRes.nBlockH = nBlockH;
    CmlMat matR;
    DOUBLE dAnglePerPixel = (1.0 / df);
    for( UINT i = 0; i < nBlockSize; ++i )
    {
        DOUBLE dCentY = (i + 0.5) * DOUBLE(nBlockH);
        dCentY = (nImgH/2-0.5) - dCentY;
        OPK2RMat( &exOriL.ori, &matR );

        Pt3d ptXYZ;
        ptXYZ.X = matR.GetAt( 0, 1 )*dCentY - matR.GetAt( 0, 2 )* df;
        ptXYZ.Y = matR.GetAt( 1, 1 )*dCentY - matR.GetAt( 1, 2 )* df;
        ptXYZ.Z = matR.GetAt( 2, 1 )*dCentY - matR.GetAt( 2, 2 )* df;

        DOUBLE dAngle = ptXYZ.Z / sqrt( (ptXYZ.X)*(ptXYZ.X) + (ptXYZ.Y)*(ptXYZ.Y) + (ptXYZ.Z)*(ptXYZ.Z) );
        if( dAngle < 0 )
        {
            stuBDRes.vecnDisp.push_back( nMinStep );
        }
        else
        {
            DOUBLE dDis = dCamHeight / dAngle;
            DOUBLE dRes = dDis * dAnglePerPixel;
            SINT nStep = SINT(dUnitDis / dRes);
            if( nStep < nMinStep )
            {
                nStep = nMinStep;
            }
            else if( nStep > nMaxStep )
            {
                nStep = nMaxStep;
            }
            stuBDRes.vecnDisp.push_back( nStep );
        }
    }
    return true;
}
ML_EXTERN_C bool mlCalcMinXOff( DOUBLE dCamHeight, UINT nImgH, DOUBLE df, DOUBLE dBaseLength, ExOriPara exOriL, SINT &nXOffMin )
{
    DOUBLE dCentY = nImgH - 1;
    dCentY = (nImgH/2-0.5) - dCentY;
    CmlMat matR;
    OPK2RMat( &exOriL.ori, &matR );

    Pt3d ptXYZ;
    ptXYZ.X = matR.GetAt( 0, 1 )*dCentY - matR.GetAt( 0, 2 )* df;
    ptXYZ.Y = matR.GetAt( 1, 1 )*dCentY - matR.GetAt( 1, 2 )* df;
    ptXYZ.Z = matR.GetAt( 2, 1 )*dCentY - matR.GetAt( 2, 2 )* df;

    DOUBLE dAngle = ptXYZ.Z / sqrt( (ptXYZ.X)*(ptXYZ.X) + (ptXYZ.Y)*(ptXYZ.Y) + (ptXYZ.Z)*(ptXYZ.Z) );

    DOUBLE dDis = dCamHeight / dAngle;

    nXOffMin = -dBaseLength * df / dDis;
    return true;

}
ML_EXTERN_C bool mlGeoFileEnLargeUnit( const SCHAR* strInGeoFile, const SCHAR* strOutGeoFile, DOUBLE dRatio )
{
    CmlGeoRaster clsGeoIn, clsGeoOut;
    if( false == clsGeoIn.LoadGeoFile( strInGeoFile ) )
    {
        return false;
    }
    Pt2d ptOrig = clsGeoIn.m_PtOrigin;
    Pt2d ptDst;
    ptDst.X = ptOrig.X * dRatio;
    ptDst.Y = ptOrig.Y * dRatio;

    DOUBLE dDstXRes = clsGeoIn.m_dXResolution * dRatio;
    DOUBLE dDstYRes = clsGeoIn.m_dYResolution * dRatio;

    DOUBLE dNoTemp = clsGeoIn.GetNoDataVal();

    switch( clsGeoIn.GetGDTType() )
    {
        case GDT_Byte:
            dNoTemp = (BYTE)(dNoTemp);
            break;
        case GDT_UInt16:
            dNoTemp = (USHORT)(dNoTemp);
            break;
        case GDT_Int16:
            dNoTemp = (SSHORT)(dNoTemp);
            break;
        case GDT_UInt32:
            dNoTemp = (UINT)(dNoTemp);
            break;
        case GDT_Int32:
            dNoTemp = (SINT)(dNoTemp);
            break;
        case GDT_Float32:
            dNoTemp = (FLOAT)(dNoTemp);
            break;
        case GDT_Float64:
            dNoTemp = (DOUBLE)(dNoTemp);
            break;
        default:
            break;
    }
    UINT nDstW = clsGeoIn.GetWidth();
    UINT nDstH = clsGeoIn.GetHeight();


    CmlRasterBlock clsRBSrc, clsRBDst;
    if( false == clsGeoIn.GetRasterOriginBlock( UINT(1), 0, 0, nDstW, nDstH, UINT(1), &clsRBSrc ) )
    {
        return false;
    }
    clsRBDst.InitialImg( clsRBSrc.GetH(), clsRBSrc.GetW(), clsRBSrc.GetBytes() );
    clsRBDst.SetGDTType( clsRBSrc.GetGDTType() );
    if( false == clsGeoOut.CreateGeoFile( strOutGeoFile, ptDst, dDstXRes, dDstYRes, clsGeoIn.GetHeight(), clsGeoIn.GetWidth(), clsGeoIn.GetBands(), \
                                         clsGeoIn.GetGDTType(), dNoTemp ) )
    {
        return false;
    }
    for( UINT  i = 0; i < clsRBDst.GetH(); ++i )
    {
        for( UINT j = 0; j < clsRBDst.GetW(); ++j )
        {
            DOUBLE dCurVal = 0;
            clsRBSrc.GetDoubleVal( i, j , dCurVal );
            if( clsRBSrc.GetGDTType() != GDT_Byte )
            {
                if( fabs( dCurVal - dNoTemp ) < ML_ZERO )
                {
                    continue;
                }
                dCurVal *= dRatio;
            }

            clsRBDst.SetDoubleVal( i, j, dCurVal );
        }
    }
    return clsGeoOut.SaveBlockToFile( UINT(1), UINT(0), UINT(0), &clsRBDst );

}
ML_EXTERN_C bool mlPt2dInDifImg( Pt2d ptOrig, UINT nOriHeight, InOriPara inOri1, ExOriPara exOri1, CAMTYPE nCamType, UINT nNewHeight, InOriPara inOri2, ExOriPara exOri2, Pt2d &ptRes )
{
    Pt2d ptUnDis;
    if( false == mlUnDistort( ptOrig, nOriHeight, inOri1, ptUnDis, nCamType ) )
    {
        return false;
    }
    //---------------------------
    CmlMat matROrig;
    OPK2RMat( &exOri1.ori, &matROrig );
    Pt3d ptXYZ;
    DOUBLE dR[9];
    memcpy( dR, matROrig.GetData(), sizeof(DOUBLE)*9);
    ptXYZ.X = dR[0] * ptUnDis.X + dR[1]*ptUnDis.Y - dR[2]*inOri1.f + exOri1.pos.X;
    ptXYZ.Y = dR[3] * ptUnDis.X + dR[4]*ptUnDis.Y - dR[5]*inOri1.f + exOri1.pos.Y;
    ptXYZ.Z = dR[6] * ptUnDis.X + dR[7]*ptUnDis.Y - dR[8]*inOri1.f + exOri1.pos.Z;

    Pt2d ptResTmp;
    CmlMat matOPK;
    OPK2RMat( &exOri2.ori, &matOPK );
    getxyFromXYZ( ptResTmp, ptXYZ, exOri2.pos, matOPK, inOri2.f, inOri2.f );
    ptRes.X = ptResTmp.X + inOri2.x;
    ptRes.Y = inOri2.y - ptResTmp.Y;


    return true;
}
ML_EXTERN_C bool mlPt3dProjInOrigImg( Pt3d ptXYZ, InOriPara inOri, ExOriPara exOri, UINT nH, CAMTYPE nCamType, Pt2d &ptRes )
{
    Pt2d ptInPers;
    CmlMat matOPK;
    OPK2RMat( &exOri.ori, &matOPK );

    getxyFromXYZ( ptInPers, ptXYZ, exOri.pos, matOPK, inOri.f, inOri.f );
    //-----------
    CmlFrameImage clsFrm;
    Pt2d ptOrigInPers;
    if( false == clsFrm.AddDisCorToPlaneFrame( ptInPers, nH, inOri, exOri, nCamType, ptOrigInPers ) )
    {
        return false;
    }
    ptRes.X = ptOrigInPers.X + inOri.x;
    ptRes.Y = inOri.y - ptOrigInPers.Y;

    return true;
}

//======================================================================================
ML_EXTERN_C bool mlGetGeoFilePtr( const SCHAR* strGeoFile, void* &geoDataPtr )
{
    CmlGeoRaster clsGeoRaster;
    if( false == clsGeoRaster.LoadGeoFile( strGeoFile ) )
    {
        return false;
    }
    CmlRasterBlock *pBlock = new CmlRasterBlock;
    if( false == clsGeoRaster.GetRasterOriginBlock(  UINT(1), UINT(0), UINT(0), clsGeoRaster.GetWidth(), clsGeoRaster.GetHeight(), UINT(1),pBlock ) )
    {
        return false;
    }
    GeoRasBlock* pGeoRBlock = new GeoRasBlock;
    pGeoRBlock->pImgBlockData = pBlock;
    pGeoRBlock->dXRes = clsGeoRaster.m_dXResolution;
    pGeoRBlock->dYRes = clsGeoRaster.m_dYResolution;
    pGeoRBlock->ptOrig = clsGeoRaster.m_PtOrigin;

    geoDataPtr = (void*)(pGeoRBlock);
    return true;;
}
ML_EXTERN_C bool mlGetGeoZ( void* geoDataPtr, Pt2d ptXY, DOUBLE &dZ )
{
    if( geoDataPtr == NULL )
    {
        return false;
    }
    GeoRasBlock* pBlock = (GeoRasBlock*)geoDataPtr;

    if( false == pBlock->pImgBlockData->GetGeoZ( ptXY.X, ptXY.Y, pBlock->ptOrig, pBlock->dXRes, pBlock->dYRes, pBlock->dNoVal, dZ ) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlGetGeoZByIdx( void* geoDataPtr, UINT nX, UINT nY, Pt3d &ptXYZ )
{
    if( geoDataPtr == NULL )
    {
        return false;
    }
    GeoRasBlock* pBlock = (GeoRasBlock*)geoDataPtr;

    if( false == pBlock->pImgBlockData->GetGeoXYZ( nY, nX, pBlock->ptOrig, pBlock->dXRes, pBlock->dYRes, pBlock->dNoVal, ptXYZ ) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlFreeGeoPtr( void* &geoDataPtr )
{
    GeoRasBlock* pBlock = (GeoRasBlock*)geoDataPtr;
    if( pBlock != NULL )
    {
        if( pBlock->pImgBlockData != NULL )
        {
            delete pBlock->pImgBlockData;
        }
        delete pBlock;
    }
    return true;
}
ML_EXTERN_C bool mlJPToGeoTiff( const SCHAR* strJPG, const SCHAR *strOutTiff, Pt2d ptOrig, DOUBLE dXRes, DOUBLE dYRes, DOUBLE dNoDataVal )
{
    CmlGdalDataset clsGdal;
    CmlRasterBlock clsB;
    if( ( false == clsGdal.LoadFile( strJPG ))||( false == clsGdal.GetRasterOriginBlock( UINT(1), UINT(0), UINT(0), clsGdal.GetWidth(), clsGdal.GetHeight(), UINT(1), &clsB ) ) )
    {
        return false;
    }
    CmlGeoRaster clsGeoR;

    if( false == clsGeoR.CreateGeoFile( strOutTiff, ptOrig, dXRes, dYRes, clsB.GetH(), clsB.GetW(), UINT(1), clsGdal.GetGDTType(), dNoDataVal ) )
    {
        return false;
    }
    if( false == clsGeoR.SaveToGeoFile( 1, 0, 0, &clsB ) )
    {
        return false;
    }
    return true;
}
ML_EXTERN_C bool mlFindMatchHoleInImg( vector<Pt2i> vecMatchedPts, UINT nW, UINT nH, UINT nHoleRange, vector<Polygon2d> &vecHolePolys )
{

	return true;
}
ML_EXTERN_C bool mlCalcNewAngle( OriAngle oriA, DOUBLE dRollAngle )
{
	CmlMat matR;
	OPK2RMat( &oriA, &matR );
	DOUBLE dVec[3];
	dVec[0] = -1.0*matR.GetAt( 0, 2 );
	dVec[1] = -1.0*matR.GetAt( 1, 2 );
	dVec[2] = -1.0*matR.GetAt( 2, 2 );

	DOUBLE dAngle1 = atan2( dVec[1], dVec[0] ) - ML_PI / 2.0;
	DOUBLE dAngle2 = atan2( dVec[2], sqrt(dVec[1]*dVec[1]+dVec[0]*dVec[0]));

	OriAngle ori1, ori2, ori3;
	ori1.omg = ML_PI / 2.0;
	ori2.phi = dAngle1;
	ori3.omg = dAngle2;
	
	CmlMat mat1, mat2, mat3;
	OPK2RMat( &ori1, &mat1 );
	OPK2RMat( &ori2, &mat2 );
	OPK2RMat( &ori3, &mat3 );

	CmlMat matRes1, matRes2, matInv;
	mlMatMul( &mat1, &mat2, &matRes1 );
	mlMatMul( &matRes1, &mat3, &matRes2 );
	mlMatInv( &matRes2, &matInv );
	
	CmlMat matRes;
	mlMatMul( &matInv, &matR, &matRes );
	
	OriAngle oriRes;
	RMat2OPK( &matRes, &oriRes );
	return true;
}

ML_EXTERN_C bool mlCreateImg( const SCHAR* strPath, UINT nW, UINT nH, ImgDotType imgType, void* pData )
{
	//----------
	string strTempPath(strPath);
	
	SINT nPos = strTempPath.rfind(".");
	
	SINT nStrLength = strTempPath.length();
	
	if( ( nPos >= nStrLength ) )
	{
		return false;
	}

	const char* cSuffix = strTempPath.c_str() + nPos + 1;
	
	///////////////////////////////////////////
	if( 0 == strcmp( cSuffix, "tif" ) )
	{
		cSuffix = "GTIFF";
	}
	if( 0 == strcmp( cSuffix, "bmp" ) )
	{
		cSuffix = "BMP";
	}
	if( 0 == strcmp( cSuffix, "jpg" ) )
	{
		cSuffix = "JPEG";
	}

	//-----------
	CmlGdalDataset clsGdal;
	if ( false == clsGdal.CreateFile( strPath, nW, nH, UINT(1), (GDALDataType)imgType, cSuffix ) )
	{
		return false;
	}
	CmlRasterBlock clsBlock;

	if ( false == clsBlock.InitialImg( nH, nW, clsGdal.GetBytes() ) )
	{
		return false;
	}
	clsBlock.SetGDTType( clsGdal.GetGDTType() );

	if ( pData != NULL )
	{
		memcpy( clsBlock.GetData(), pData, clsBlock.GetTPixelSize()*clsBlock.GetBytes() );
	}
	else
	{
		BYTE* pTmpData = new BYTE[clsBlock.GetTPixelSize()*clsBlock.GetBytes()];
		memset( pTmpData, 0, clsBlock.GetTPixelSize()*clsBlock.GetBytes() );
		memcpy( clsBlock.GetData(), pTmpData, clsBlock.GetTPixelSize()*clsBlock.GetBytes() );
		delete[] pTmpData;
	}

	if ( false == clsGdal.SaveBlockToFile( UINT(1), UINT(0), UINT(0), &clsBlock ) )
	{
		return false;
	}
	return true;
	
}
ML_EXTERN_C bool mlCreateGeoImg( const SCHAR* strPath, UINT nW, UINT nH, Pt2d ptOrig, DOUBLE dXRes, DOUBLE dYRes, ImgDotType imgType, void* pData )
{
	//----------
	string strTempPath(strPath);

	SINT nPos = strTempPath.rfind(".");

	SINT nStrLength = strTempPath.length();

	if( ( nPos >= nStrLength ) )
	{
		return false;
	}

	const char* cSuffix = strTempPath.c_str() + nPos + 1;

	///////////////////////////////////////////
	if( 0 == strcmp( cSuffix, "tif" ) )
	{
		cSuffix = "GTIFF";
	}
	if( 0 == strcmp( cSuffix, "bmp" ) )
	{
		cSuffix = "BMP";
	}
	if( 0 == strcmp( cSuffix, "jpg" ) )
	{
		cSuffix = "JPEG";
	}

	//-----------
	CmlGeoRaster clsGdal;
	if ( false == clsGdal.CreateGeoFile( strPath, ptOrig, dXRes, dYRes, nH, nW, UINT(1), (GDALDataType)imgType, -99999 ) )
	{
		return false;
	}
	CmlRasterBlock clsBlock;

	if ( false == clsBlock.InitialImg( nH, nW, clsGdal.GetBytes() ) )
	{
		return false;
	}
	clsBlock.SetGDTType( clsGdal.GetGDTType() );

	if ( pData != NULL )
	{
		memcpy( clsBlock.GetData(), pData, clsBlock.GetTPixelSize()*clsBlock.GetBytes() );
	}
	else
	{
		BYTE* pTmpData = new BYTE[clsBlock.GetTPixelSize()*clsBlock.GetBytes()];
		memset( pTmpData, 0, clsBlock.GetTPixelSize()*clsBlock.GetBytes() );
		memcpy( clsBlock.GetData(), pTmpData, clsBlock.GetTPixelSize()*clsBlock.GetBytes() );
		delete[] pTmpData;
	}

	if ( false == clsGdal.SaveToGeoFile( UINT(1), UINT(0), UINT(0), &clsBlock ) )
	{
		return false;
	}
	return true;
}