/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlWBaseProc.h
* @date 2011.11.18
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 长基线制图类头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
* 彭嫚  2011.11.29  1.0  转化为c++框架
*/
#ifndef MLWBASEPROC_H
#define MLWBASEPROC_H

#include "mlWBaseProc.h"
#include "mlBase.h"
#include "mlMat.h"
#include "mlFrameImage.h"
#include "mlTypeConvert.h"
#include "mlPtsManage.h"

/**
* @class CmlWBaseProc
* @date 2011.11.18
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 长基线制图功能模块类定义
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/
class CmlWBaseProc
{
public:
    /**
    * @fn CmlWBaseProc()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 空参构造函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    CmlWBaseProc();

    /**
    * @fn ~CmlWBaseProc()
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 析构函数
    * @version 1.1
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    virtual ~CmlWBaseProc();

    /**
    * @fn WideBaseAnalysis
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
    bool WideBaseAnalysis(InOriPara NavCamPara, InOriPara PanCamPara, BaseOptions AnaPara,double &OptiBase);

    /**
    * @fn WideBaseMapping
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
   // bool WideBaseMapping(vector<StereoSet> &vecStereoSet, vector<string> &vecStrMatchFile, int nColRadius,int nRowRadius,int nTemplateSize,int nGridSize,DOUBLE dRes,DOUBLE dCoef, char * strDemPath, bool bIsUsingFeatPt, bool bIsUsingPartion );

    bool WideBaseMapping(vector<StereoSet> vecStereoSet, WideOptions WidePara,vector<ImgPtSet>& vecFPtSetL, vector<ImgPtSet>& vecFPtSetR, vector<ImgPtSet>& vecDPtSetL, vector<ImgPtSet>& vecDPtSetR,SCHAR *strDemFile);


    /**
    * @fn WideBase3Ds
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 长基线点云生成
    * @param vecStereoSet,立体像对
    * @param vecLPts，左影像匹配点
    * @param vecRPts，右影像匹配点
    * @param vec3ds，生成点云文件
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    bool WideBase3Ds(StereoSet* pStereoSet, vector<Pt2d>& vecLPts, vector<Pt2d>& vecRPts, vector<Pt3d>& vec3ds);

    //bool WideBase3Ds(StereoSet* pStereoSetf, StereoSet* pStereoSets, vector<Pt2d>& vecLPts, vector<Pt2d>& vecRPts, vector<Pt3d>& vec3ds);

    /**
    * @fn WideDenseMatch
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 密集匹配
    * @param LOriImg,待匹配左影像
    * @param ROriImg，待匹配右影像
    * @param WidePara，长基线匹配相关参数
    * @param vecFPtL，左影像特征点
    * @param vecFPtR，右影像特征点
    * @param vecMPtL，左影像密集点
    * @param vecMPtR，右影像密集点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    bool WideDenseMatch(CmlRasterBlock LOriImg,CmlRasterBlock ROriImg, WideOptions WidePara, vector<Pt2d> &vecFPtL, vector<Pt2d> &vecFPtR, vector<Pt2d> &vecMPtL, vector<Pt2d> &vecMPtR);

    /**
    * @fn WideFeaMatch
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 特征匹配
    * @param LOriImg,待匹配左影像
    * @param ROriImg，待匹配右影像
    * @param WidePara，长基线匹配相关参数
    * @param vecPtL，左影像特征点
    * @param vecPtR，右影像特征点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    bool WideFeaMatch(CmlRasterBlock LOriImg,CmlRasterBlock ROriImg, WideOptions WidePara, vector<Pt2d> &vecPtL, vector<Pt2d> &vecPtR);

    /**
    * @fn WidePtsFilter
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 对长基线密集点的三维点云滤波去除粗差
    * @param vecFPts,特征点三维点云数据
    * @param vecDPts,密集点三维点云数据
    * @param vec3dPts, 总的三维点云数据
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    bool WidePtsFilter(vector<Pt3d>& vecFPts, vector<Pt3d>& vecDPts, vector<Pt3d>& vec3dPts);
    /**
    * @fn mlBestBase
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 计算最优基线长
    * @param mlNav,导航相机参数
    * @param mlPan, 全景相机参数
    * @param dOptiBase,最优基线长
    * @param dFixBase,导航相机固定基线长
    * @param dPixel,像素匹配误差
    * @param dTarget,目标距离
    * @param nWidth,影像宽度
    * @param dBase,基线长函数变量
    * @param dThreshold,阈值大小
    * @param dIterTime,迭代次数
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    DOUBLE mlBestBase(InOriPara mlNav, InOriPara mlPan,DOUBLE dOptiBase,DOUBLE dFixBase, DOUBLE dPixel, DOUBLE dTarget, UINT nWidth,DOUBLE * dBase,DOUBLE dThresHold, DOUBLE dIterTime);
protected:

//private:
public:
    /**
    * @fn mlFunAcc
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 测图精度函数的一阶导数和二阶导数
    * @param dFixBase,导航相机固定基线长
    * @param dBase,基线长函数变量
    * @param dFunAcc,一阶导数和二阶导数
    * @param mlNav,导航相机参数
    * @param mlPan,全景相机参数
    * @param dPixel,像素匹配误差
    * @param dTarget,目标距离
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    void mlFunAcc(DOUBLE dFixBase,DOUBLE dBase,DOUBLE dFunAcc[2],InOriPara mlNav, InOriPara mlPan,DOUBLE dPixel, DOUBLE dTarget);

    /**
    * @fn mlNewTon
    * @date 2011.11.22
    * @author 彭嫚
    * @brief 牛顿迭代法求解函数值
    * @param dFixBase,导航相机固定基线长
    * @param dBase,基线长函数变量
    * @param dThreshold,阈值大小
    * @param dIterTime,迭代次数
    * @param mlNav,导航相机参数
    * @param mlPan,全景相机参数
    * @param dPixel,像素匹配误差
    * @param dTarget,目标距离
    * @retval TRUE 成功
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    DOUBLE mlNewTon(DOUBLE dFixBase,DOUBLE * dBase,DOUBLE dThresHold, DOUBLE dIterTime,InOriPara mlNav, InOriPara mlPan,DOUBLE dPixel, DOUBLE dTarget);

    /**
    * @fn mlOriPtToEpi
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 将原始影像的匹配点转到核线影像上的点
    * @param vecOPtsL，原始左影像匹配点
    * @param vecOPtsR，原始右影像匹配点`
    * @param LeftHomo，左影像透视矩阵
    * @param RightHomo，右影像透视矩阵
    * @param vecEPtsL,左核线影像匹配点
    * @param vecEPtsR,右核线影像匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    bool mlOriPtToEpi(const vector<Pt2d>& vecOPtsL, const vector<Pt2d>& vecOPtsR,CmlMat& LeftHomo, CmlMat& RightHomo, vector<Pt2d>& vecEPtsL, vector<Pt2d>& vecEPtsR);

    /**
    * @fn mlEpiPtToOri
    * @date 2011.12.1
    * @author 彭嫚
    * @brief 将核线影像的匹配点转到原始影像上的点
    * @param vecEPtsL,左核线影像匹配点
    * @param vecEPtsR,右核线影像匹配点
    * @param LeftHomo，左影像透视矩阵
    * @param RightHomo，右影像透视矩阵
    * @param vecOPtsL，原始左影像匹配点
    * @param vecOPtsR，原始右影像匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    bool mlEpiPtToOri(const vector<Pt2d>& vecEPtsL, const vector<Pt2d>& vecEPtsR,CmlMat& LeftHomo, CmlMat& RightHomo, vector<Pt2d>& vecOPtsL, vector<Pt2d>& vecOPtsR);

    /**
    * @fn mlWideEpiImg
    * @date 2011.12.9
    * @author 彭嫚
    * @brief 根据正确匹配的点计算基础矩阵生成长基线影像的核线影像
    * @param vecEPtsL,左影像匹配像点
    * @param vecEPtsR,右影像匹配像点
    * @param pLOriImg,原始左影像
    * @param pROriImg,原始右影像
    * @param pLEpiImg,左影像核线影像
    * @param pREpiImg,右影像核线影像
    * @param LeftHomo,左影像单应矩阵
    * @param RightHomo,右影像单应矩阵
    * @retval TRUE 成功
    * @version 1.1
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    *
    */
    bool mlWideEpiImg(const vector<Pt2d>& vecEPtsL, const vector<Pt2d>& vecEPtsR, CmlRasterBlock *pLOriImg, CmlRasterBlock *pROriImg, CmlRasterBlock *pLEpiImg, CmlRasterBlock *pREpiImg, CmlMat &LeftHomo, CmlMat &RightHomo);

};

#endif // MLWBASEPROC_H
