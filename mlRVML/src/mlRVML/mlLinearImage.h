/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlLinearImage.h
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 线阵卫星影像处理头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef MLLINEARIMAGE_H
#define MLLINEARIMAGE_H

#include "mlGdalDataset.h"
#include "mlMat.h"
#include "mlDemProc.h"
#include "mlPhgProc.h"
#include "mlFrameImage.h"
#include "mlRasterBlock.h"

/**
* @struct tagMatrixR
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 旋转矩阵结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagMatrixR
{
    CmlMat matR;//!<旋转矩阵
    tagMatrixR()
    {
        matR.Initial( 3, 3 );
    }
} MatrixR;
/**
* @class CmlLinearImage
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 线阵卫星影像类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlLinearImage : public CmlGdalDataset
{
public:
    /**
    * @fn CmlLinearImage
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CmlLinearImage类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlLinearImage();
    /**
    * @fn ~CmlLinearImage
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CmlLinearImage类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    virtual ~CmlLinearImage();

public:
    //SatImgInfo m_SatImgInfo;//!<影像头信息
    vector<ExOriPara> m_vecImgEo;//!<左影像外方位元素
    vector<MatrixR> m_vecMatrix;//!<左影像外方位旋转矩阵
    vector<Pt3d> m_vecPosition;//!<左影像外方位线元素
    /**
    * @fn mlGetEop
    * @date 2011.11.22
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 轨道测控数据多项式内插外方位元素
    * @param vecLineEo 原始测控数据中线元素
    * @param vecAngleEo 原始测控数据中角元素
    * @param vecImg_time 卫星影像扫描行获取时间
    * @param vecEo 内插后外方位
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlGetEop( vector<LineEo> &vecLineEo, vector<AngleEo> &vecAngleEo, vector<DOUBLE> &vecImg_time, vector<ExOriPara> *vecEo );
    /**
    * @fn mlBLH2XYZ
    * @date 2011.11.29
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 将物方三维点由月球大地坐标系转成月固坐标系下的空间直角坐标
    * @param blhPts 月球大地坐标系坐标
    * @param xyzPts 月固坐标系下的空间直角坐标
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlBLH2XYZ(Pt3d& blhPts, Pt3d& xyzPts);
    /**
    * @fn mlXYZ2BLH
    * @date 2011.11.29
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 将物方三维点由月固坐标系下的空间直角坐标转成月球大地坐标系
    * @param xyzPts 月固坐标系下的XYZ坐标
    * @param blhPts 月球大地坐标系（Longitude,Latitude,Height）坐标
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlXYZ2BLH(Pt3d &xyzPts, Pt3d &blhPts);
protected:
private:
};
/**
* @class CmlCE1LinearImage
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CE-1线阵卫星影像类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlCE1LinearImage : public CmlLinearImage
{
public:
    /**
    * @fn CmlCE1LinearImage
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CmlCE1LinearImage类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlCE1LinearImage();
    /**
    * @fn ~CmlCE1LinearImage
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CmlCE1LinearImage类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    virtual ~CmlCE1LinearImage();
    /**
    * @fn mlGetCE1DOMCoordinate
    * @date 2011.12.07
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 生成CE-1卫星影像DOM格网在原图像上的x,y坐标
    * @param OriSatImg 原始影像
    * @param vecR 影像外方位旋转矩阵
    * @param vecXsYsZs 影像外方位线元素
    * @param f 相机焦距
    * @param vecPtXYZ 物方三维坐标
    * @param pCE1IOP 内方位元素
    * @param nWidth 卫星影像宽度
    * @param nHeight 影像高度
    * @param thresh 计算出的x坐标与理论值之差的阈值
    * @param ImgSL 卫星影像DOM格网在原图像上的x,y坐标
    * @param trueline 影像坐标真实行
    * @param range 搜索范围
    * @param thresh 阈值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlGetCE1DOMCoordinate(CmlRasterBlock &OriSatImg, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, DOUBLE f, vector<Pt3d> &vecPtXYZ, UINT nWidth, UINT nHeight, CRasterPt2D &ImgSL, DOUBLE trueline, UINT range , DOUBLE thresh );
public:

    CE1IOPara m_CE1IOPara ;//!<CE-1卫星影像内定向参数
    /**
    * @fn mlCE1OPK2RMat
    * @date 2011.11.30
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CE-1卫星影像外方位角元素转旋转矩阵
    * @param pitch 俯仰角
    * @param roll 翻滚角
    * @param yaw 航偏角
    * @param pRMat 旋转矩阵
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlCE1OPK2RMat( DOUBLE &pitch, DOUBLE &roll, DOUBLE &yaw, CmlMat* pRMat );
    /**
    * @fn mlCE1OPK2RMat
    * @date 2011.11.30
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 将CE-1卫星影像六类外方位角元素转成线元素及旋转矩阵的形式
    * @param vecEo 外方位元素
    * @param vecXsYsZs 线元素
    * @param vecR 旋转矩阵
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlCE1OPK2RMat( vector<ExOriPara> &vecEo, vector<Pt3d> & vecXsYsZs, vector<MatrixR> &vecR );
    /**
    * @fn mlCE1InOrietation
    * @date 2011.11.20
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CE-1卫星影像内定向
    * @param vecPtsList 像点坐标
    * @param pSatSio 内定向参数结构体
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlCE1InOrietation( vector<Pt2d> &vecPtsList, CE1IOPara *pSatSio, vector<Pt2d> &vecXY );
    /**
    * @fn mlGetCE1DOM
    * @date 2011.12.07
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 生成CE-1卫星影像DOM
    * @param OriSatImg 原始CE-1卫星影像
    * @param vecR 影像外方位旋转矩阵
    * @param vecXsYsZs 影像外方位线元素
    * @param pCE1IOP 内方位元素
    * @param vecPtXYZ 三维物方点
    * @param range 外方位搜索范围
    * @param thresh 计算出的x坐标与理论值之差的阈值
    * @param SatDom CE-1卫星影像DOM
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlGetCE1DOM( CmlRasterBlock &OriSatImg, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, CE1IOPara *pCE1IOP, vector<Pt3d> &vecPtXYZ, CmlRasterBlock &SatDom, UINT range = 5, DOUBLE thresh = 0.00002 );
};

/**
* @class CmlCE2LinearImage
* @date 2011.11.18
* @author  刘一良 ylliu@irsa.ac.cn
* @brief CE-2线阵卫星影像类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlCE2LinearImage : public CmlLinearImage
{
public:
    /**
    * @fn CmlCE2LinearImage
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CmlCE2LinearImage类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlCE2LinearImage();
    /**
    * @fn ~CmlCE2LinearImage
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CmlCE2LinearImage类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    virtual ~CmlCE2LinearImage();
    /**
    * @fn mlGetCE2DOMCoordinate
    * @date 2011.12.07
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 生成CE-2卫星影像DOM格网在原图像上的x,y坐标
    * @param OriSatImg 原始影像
    * @param vecR 影像外方位旋转矩阵
    * @param vecXsYsZs 影像外方位线元素
    * @param f 相机焦距
    * @param vecPtXYZ 物方三维坐标
    * @param pCE1IOP 内方位元素
    * @param nWidth 卫星影像宽度
    * @param nHeight 影像高度
    * @param thresh 计算出的x坐标与理论值之差的阈值
    * @param ImgSL 卫星影像DOM格网在原图像上的x,y坐标
    * @param trueline 影像坐标真实行
    * @param range 搜索范围
    * @param thresh 阈值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlGetCE2DOMCoordinate(CmlRasterBlock &OriSatImg, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, DOUBLE f, vector<Pt3d> &vecPtXYZ, UINT nWidth, UINT nHeight, CRasterPt2D &ImgSL, DOUBLE trueline, UINT range , DOUBLE thresh );
public:
    //CE2IOPara m_CE2IOPara ;//!<CE-2卫星影像内定向参数
    CE2IOPara m_CE2IOPara ;//!<CE-2卫星影像内定向参数
    /**
    * @fn mlCE2InOrietation
    * @date 2011.11.20
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CE-2卫星影像内定向，将卫星影像匹配同名点行列号转换为像平面坐标
    * @param vecPtsList 输入时为像点行列号坐标，输出时为焦平面坐标
    * @param pSatSio 卫星影像内定向参数结构体
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlCE2InOrietation( vector<Pt2d> &vecPtsList, CE2IOPara *pSatSio, vector<Pt2d> &vecXY );
    /**
    * @fn mlCE2OPK2RMat
    * @date 2012.3.3
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief CE-2卫星影像外方位角元素转旋转矩阵
    * @param angX 绕X轴夹角
    * @param angY 绕Y轴夹角
    * @param angZ 绕Z轴夹角
    * @param pRMat 旋转矩阵
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlCE2OPK2RMat( DOUBLE &angX, DOUBLE &angY, DOUBLE &angZ, CmlMat* pRMat );
    /**
    * @fn mlCE2OPK2RMat
    * @date 2011.11.30
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 将CE-2卫星影像六类外方位角元素转成线元素及旋转矩阵的形式
    * @param vecEo 外方位元素
    * @param vecXsYsZs 线元素
    * @param vecR 旋转矩阵
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlCE2OPK2RMat( vector<ExOriPara> &vecEo, vector<Pt3d> & vecXsYsZs, vector<MatrixR> &vecR );
    /**
    * @fn mlGetCE2DOM
    * @date 2011.12.07
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 生成CE-2卫星影像DOM
    * @param OriSatImg 原始CE-2卫星影像
    * @param vecR 影像外方位旋转矩阵
    * @param vecXsYsZs 影像外方位线元素
    * @param pCE1IOP 内方位元素
    * @param pDem  卫星影像DEM高程值
    * @param PLL DEM左上角坐标X,Y
    * @param res DEM的X和Y方向的分辨率
    * @param range 外方位搜索范围
    * @param thresh 计算出的x坐标与理论值之差的阈值
    * @param SatDom CE-2卫星影像DOM
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlGetCE2DOM( CmlRasterBlock &OriSatImg, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, CE2IOPara *pCE2IOP, vector<Pt3d> &vecPtXYZ, CmlRasterBlock &SatDom, UINT range = 5, DOUBLE thresh = 0.00002 );
};


#endif // MLLINEARIMAGE_H
