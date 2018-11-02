/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlFrameImage.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像处理头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef CMLFRAMEIMAGE_H
#define CMLFRAMEIMAGE_H

#include "mlGdalDataset.h"
#include "mlBase.h"

/**
* @class CmlFrameImage
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlFrameImage : public CmlGdalDataset
{
public:
    /**
    * @fn CmlFrameImage
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlFrameImage类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlFrameImage();
    /**
    * @fn ~CmlFrameImage
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlFrameImage类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    virtual ~CmlFrameImage();
    /**
    * @fn LoadFile
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 读取图像并直接存入对应的block中
    * @param FileName 文件路径
    * @param nType 文件类型
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool LoadFile( const SCHAR *FileName, SINT nType=0);
    /**
    * @fn
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵相机影像畸变校正
    * @param pInImg 输入图像
    * @param pOutImg 输出图像
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool GetUnDistortImg( CmlRasterBlock* pInImg,  CmlRasterBlock* pOutImg, CAMTYPE nCamType = Nav_Cam, DOUBLE dZoomCoef = 1.0 );
    /**
    * @fn
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵相机影像畸变校正
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool GetUnDistortImg( CAMTYPE nCamType = Nav_Cam, DOUBLE dZoomCoef = 1.0 );

    /**
    * @fn SmoothByGuassian
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵相机影像去噪高斯滤波
    * @param nTemplateSize 滤波模板大小
    * @param dCoef 滤波核参数,一般以0.8为宜
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool SmoothByGuassian( SINT nTemplateSize, DOUBLE dCoef );

    /**
    * @fn SmoothByGuassian
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵相机影像去噪高斯滤波
    * @param nTemplateSize 滤波模板大小
    * @param dCoef 滤波核参数,一般以0.8为宜
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool SmoothByGuassian( CmlRasterBlock &clsBlock, SINT nTemplateSize, DOUBLE dCoef );
    /**
    * @fn ExtractFeatPtByForstner
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 面阵相机影像Forstner方法提取特征点
    * @param nGridSize 格网大小
    * @param nPtNum 欲提取的点数
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool ExtractFeatPtByForstner( SINT nGridSize, SINT nPtNum = 0, DOUBLE dThresCoef = 1.0, bool bIsRemoveAbPixel = false );
    /**
    * @fn UnDisCorToPlaneFrame
    * @date 2011.11.20
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 计算畸变改正后坐标并转换成像平面坐标系
    * @param imgInList 图像点坐标（左上角坐标系）
    * @param inPara 内定向参数
    * @param imgOutList 像平面坐标x,y
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool UnDisCorToPlaneFrame(vector<Pt2d>& imgInList, InOriPara& inPara, vector<Pt2d>& imgOutList, CAMTYPE nCamType = Nav_Cam );

    bool UnDisCorToPlaneFrame( Pt2d pt, InOriPara inPara, Pt2d &ptRes, CAMTYPE nCamType = Nav_Cam );

    bool UnDisCorToPlaneFrame(vector<Pt2d>& imgInList, InOriPara& inPara, UINT nHeight, vector<Pt2d>& imgOutList, CAMTYPE nCamType = Nav_Cam );

    bool UnDisCorToPlaneFrame( Pt2d pt, InOriPara inPara, UINT nHeight, Pt2d &ptRes, CAMTYPE nCamType = Nav_Cam );

//    bool CalcObjPtsInFrame( Pt3d ptXYZ, UINT nHeight, InOriPara inPara, ExOriPara exPara, CAMTYPE nCamType, Pt2d &ptRes );

    bool AddDisCorToPlaneFrame( Pt2d ptxyOrig, UINT nHeight, InOriPara inPara, ExOriPara exPara, CAMTYPE nCamType, Pt2d &ptRes );
    /**
    * @fn UnDisCorToPicCoord
    * @date 2011.11.20
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 计算畸变改正后坐标.转换成图像坐标系
    * @param imgInList 图像点坐标（左上角坐标系）
    * @param inPara 内定向参数
    * @param imgOutList 畸变矫正后图像点坐标（左上角坐标系）
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool UnDisCorToPicCoord(vector<Pt2d>& imgPtsList, InOriPara& inPara, vector<Pt2d>& imgOutList, CAMTYPE nCamType = Nav_Cam );



    CmlRasterBlock m_DataBlock;//!<数据块
    vector<Pt2i> m_vecFeaPtsList ;//!<存放Forstner特征点

    InOriPara m_InOriPara;//!<内定向参数
    ExOriPara m_ExOriPara;//!<外定向参数
    /**
    * @fn mlGetBilinearValue
    * @date 2012.01.08
    * @author 吴凯 wukai@irsa.ac.cn
    * @brief 进行影像灰度值的双线性内插.（左上角坐标系）
    * @param pImg 原始影像
    * @param tempXY 像点坐标
    * @version 1.0
    * @return 灰度内插后对应的影像灰度值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    BYTE mlGetBilinearValue(CmlRasterBlock *pImg , Pt2d tempXY) ;
    /**
    * @fn mlGetNearValue
    * @date 2012.01.08
    * @author 吴凯 wukai@irsa.ac.cn
    * @brief 计算某点在影像上的最临近像素点的值
    * @param pImg 原始影像
    * @param tempXY 像点坐标
    * @version 1.0
    * @return 最临近像素点的值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    BYTE mlGetNearValue(CmlRasterBlock *pImg , Pt2d tempXY) ;
    /**
    * @fn mlBilinearInterpolation
    * @date 2011.11.20
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 双线性内插
    * @param pImg 原始影像
    * @param pCoordinate 内插前非整型坐标点
    * @param pDisImg 内插后影像
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlBilinearInterpolation( CmlRasterBlock *pImg , CRasterPt2D *pCoordinate, CmlRasterBlock *pDisImg );
    /**
    * @fn mlGetDistortionCoordinate
    * @date 2011.11.20
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 计算畸变改正后点坐标
    * @param pDisImg 原始畸变影像
    * @param pCoordinate 畸变矫正后坐标点
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlGetDistortionCoordinate( CmlRasterBlock *pDisImg , CRasterPt2D * pCoordinate, CAMTYPE nCamType = Nav_Cam, DOUBLE dZoomCoef = 1.0 );
    /**
    * @fn mlGrayInterpolation
    * @date 2011.11.20
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 图像灰度内插
    * @param pImg 原始影像
    * @param pCoordinate 内插前非整形坐标点
    * @param pDisImg 内插后影像
    * @param nOptions 灰度内插类型
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlGrayInterpolation( CmlRasterBlock *pImg , CRasterPt2D *pCoordinate, CmlRasterBlock *pDisImg, int nOptions );

    /**
    * @fn mlCleanDeadPixel
    * @date 2012.02
    * @author 张重阳 zhangchy@irsa.ac.cn
    * @brief 线阵影像坏点去除
    * @param strImgPathIn 输入图像路径
    * @param strImgPathOut 输出图像路径
    * @param vecDeadPix 坏点位置
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlCleanDeadPix( const char* strImgPathIn, const char* strImgPathOut, vector<Pt2i> vecDeadPix );

    bool ExtractFeatPtByForstner(CmlRasterBlock &InputImg, vector<Pt2i> &vecFeaPts, SINT nGridSize,  SINT nPtNum = 0, DOUBLE dThresCoef = 1.0, bool bIsRemoveAbPixel = false );

    bool GrayTensile( CmlRasterBlock* pBlock, UINT nMin = 0, UINT nMax = 255 );

    bool GrayTensile( UINT nMin = 0, UINT nMax = 255 );

    bool SaveFile( const char* strOutPath );

    bool HistogramEqualize();
	bool RasterGrayStrench(CmlRasterBlock *rasterBlock,double dThresh =0.0015);//栅格影像拉伸，用于匹配提高匹配点数

    bool DrawCrossMark( vector<Pt2i> vecPts, UINT nLineLength = 10, UINT nLineWidth = 2 );

    bool WallisFilter( UINT nTemplateSize, DOUBLE dExpectMean, DOUBLE dExpectVar, DOUBLE dCoefA, DOUBLE dCoefAlpha );

    bool PtSelectionByGrid( vector<Pt2d> vecPts, UINT nImgW, UINT nImgH, UINT nGridW, UINT nGridH, vector<Pt2d> &vecOutRes );
    bool PtSelectionByGrid( vector<Pt2d> vecPts, UINT nImgW, UINT nImgH, UINT nGridW, UINT nGridH, vector<bool> &vecFlags );

    bool EdgeDetectionByCanny( void* pImg, vector<Pt2i> &vecPts, DOUBLE dThresHold1, DOUBLE dThresHold2, UINT nMaxPts = 10000 );

    bool EdgeDetectionByCanny( const SCHAR* strInPath, vector<Pt2i> &vecPts, DOUBLE dThresHold1, DOUBLE dThresHold2, UINT nMaxPts = 10000 );
    /**
    * @fn imgpyramid
    * @date 2012.02
    * @author pengman@irsa.ac.cn
    * @brief 影像金字塔生成
    * @param InputImg 原始影像
    * @param OutputImg 输出图像
    * @param nSize 金字塔影像的缩放系数
    * @version 1.0
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool imgpyramid( CmlRasterBlock *InputImg, vector<CmlRasterBlock> &OutputImg,SINT nSize);


protected:
private:


};

#endif // CMLFRAMEIMAGE_H
