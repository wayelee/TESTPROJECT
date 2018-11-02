/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlSatMapping.h
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 线阵卫星影像制图头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/

#ifndef CMLSATMAPPING_H
#define CMLSATMAPPING_H
#include "mlMat.h"
#include "mlBase.h"
#include "mlTypes.h"
#include "mlGdalDataset.h"
#include "mlRasterBlock.h"
#include "mlBlockCalculation.h"
#include "mlLinearImage.h"
#include "mlStereoProc.h"
#include "mlSiteMapping.h"
#include "mlCoordTrans.h"
#include "SiftMatch.h"
#include "ASift.h"

/**
* @class CmlSatMapping
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 线阵卫星影像制图类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlSatMapping
{
public:
    /**
    * @fn CmlSatMapping
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CmlSatMapping类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlSatMapping();
    /**
    * @fn ~CmlSatMapping
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CmlSatMapping类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    virtual ~CmlSatMapping();
public:
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
    bool mlSatMatch( const string sLimgPath, const string sRimgPath, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, SINT nMethod = 0);
    /**
    * @fn mlSatMappingByPts
    * @date 2012.2.21
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 由卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM
    * @param satproj 卫星影像DEM及DOM生成工程参数
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
    bool mlSatMappingByPts( SatProj &satproj, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, vector<StereoMatchPt> &vecDensePts, vector<Pt3d> &vecPres );
    /**
    * @fn mlCE1MappingByPts
    * @date 2012.2.21
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 由CE-1卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM
    * @param satproj 卫星影像DEM及DOM生成工程参数
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
    bool mlCE1MappingByPts( SatProj &satproj, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, vector<StereoMatchPt> &vecDensePts, vector<Pt3d> &vecPres );
    /**
    * @fn mlCE2MappingByPts
    * @date 2012.2.21
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 由CE-2卫星影像匹配点生成密集匹配点及物方三维点，生成DEM和DOM
    * @param satproj 卫星影像DEM及DOM生成工程参数
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
    bool mlCE2MappingByPts( SatProj &satproj, SatOptions &satPara, vector<StereoMatchPt> &vecRanPts, vector<StereoMatchPt> &vecDensePts, vector<Pt3d> &vecPres );
    /**
    * @fn WriteBLH
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 将三维点云数据生成规则格网DEM，并以GeoTiff格式存储
    * @param path 文件路径
    * @param vecBLH 三维点云数据
    * @param XResolution X方向分辨率
    * @param YResolution Y方向分辨率
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool WriteBLH( const SCHAR *path, vector<Pt3d> &vecBLH, DOUBLE XResolution, DOUBLE YResolution );
    /**
    * @fn GenerateDOM
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 生成CE-1卫星影像DOM并以GeoTiff格式存储
    * @param dempath DEM路径
    * @param path DOM存储路径
    * @param CE1IO 嫦娥一号内方位参数
    * @param ImgBlock 原始影像
    * @param vecR 旋转矩阵
    * @param vecXsYsZs 外方位线元素
    * @param domBlock 生成的DOM
    * @param nBands 影像波段数
    * @param nodata 无数据区值
    * @param B0 墨卡托投影割角
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool GenerateDOM( const SCHAR *dempath, const SCHAR *path, CE1IOPara &CE1IO, CmlRasterBlock &ImgBlock, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, CmlRasterBlock &domBlock, DOUBLE B0, UINT nBands, DOUBLE nodata );
    /**
    * @fn GenerateCE2DOM
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 生成DOM并以GeoTiff格式存储
    * @param dempath DEM路径
    * @param path DOM存储路径
    * @param CE2IO 嫦娥一号内方位参数
    * @param ImgBlock 原始影像
    * @param vecR 旋转矩阵
    * @param vecXsYsZs 外方位线元素
    * @param domBlock 生成的DOM
    * @param nBands 影像波段数
    * @param nodata 无数据区值
    * @param B0 墨卡托投影割角
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool GenerateCE2DOM( const SCHAR *dempath, const SCHAR *path, CE2IOPara &CE2IO, CmlRasterBlock &ImgBlock, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, CmlRasterBlock &domBlock, DOUBLE B0, UINT nBands, DOUBLE nodata );
    /**
    * @fn InterSection
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 前方交会生成三维物方点
    * @param ptSubDenseL 左影像块子像素密集匹配点
    * @param ptSubDenseR 右影像块子像素密集匹配点
    * @param vecXYL 左影像块焦平面坐标
    * @param vecXYR 右影像块焦平面坐标
    * @param vecLXsYsZs 左影像块外方位线元素
    * @param vecRXsYsZs 右影像块外方位线元素
    * @param vecRL 左影像块旋转矩阵
    * @param vecRR 右影像块旋转矩阵
    * @param lf 左影像焦距
    * @param rf 右影像焦距
    * @param vecBLH 三维物方点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool InterSection(vector<Pt2d> &ptSubDenseL, vector<Pt2d> &ptSubDenseR, vector<Pt2d> &vecXYL, vector<Pt2d> &vecXYR, \
                                 vector<Pt3d> &vecLXsYsZs, vector<Pt3d> &vecRXsYsZs,vector<MatrixR> &vecRL, vector<MatrixR> &vecRR, DOUBLE lf, DOUBLE rf,vector<Pt3d> &vecBLH );
    /**
    * @fn ConstractAdjust
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 影像自动对比度增强
    * @param Img 影像块
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool ConstractAdjust(CmlRasterBlock &Img);
protected:
private:
};

#endif // CMLSATMAPPING_H
