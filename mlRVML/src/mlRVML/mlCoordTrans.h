/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlCoordTrans.h
* @date 2011.11.18
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 坐标转换算法类头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CMLCOORDTRANS_H
#define CMLCOORDTRANS_H

#include "mlBase.h"

/**
* @class CmlCoordTrans
* @date 2011.11
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 坐标转换算法类定义
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CmlCoordTrans
{
public:
    /**
     *@fn CmlCoordTrans()
     *@date 2011.11
     *@author 张重阳
     *@brief 坐标转换类析构函数
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
    */
    CmlCoordTrans();

    /**
     *@fn CmlCoordTrans()
     *@date 2011.11
     *@author 张重阳
     *@brief 坐标转换类析构函数
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    virtual ~CmlCoordTrans();

    /**
     *@fn mlCoordTransResult
     *@date 2012.02
     *@author 张重阳
     *@brief 根据给定的旋转矩阵和平移向量求解转换后的坐标
     *@param pArr 传入坐标数组指针
     *@param nDim 坐标维数
     *@param pRotateMat 旋转矩阵指针
     *@param pTransVec 平移向量指针
     *@param fx，fy 焦距
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
    bool mlCoordTransResult(DOUBLE* pArr, SINT nDim, CmlMat* pRotateMat, DOUBLE* pTransVec, DOUBLE* pTransResult,SINT nflag);

    /**
     *@fn mlCalcTransMatrixByXYZ
     *@date 2011.11
     *@author 张重阳
     *@brief 本函数实现月固系到局部坐标系转换关系求解
     *@param pLocResult 着陆点在月固系下得精确定位结果
     *@param pTransMat  存储转换的旋转矩阵
     *@param pTransVec  存储平移向量
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

     */
    bool mlCalcTransMatrixByXYZ(Pt3d* pLocResult,CmlMat* pTransMat, DOUBLE* pTransVec);
    /**
     *@fn mlCalcTransMatrixByLatLong
     *@date 2011.11
     *@author 张重阳
     *@brief 本函数根据定位的经纬度实现月固系到局部坐标系转换关系求解
     *@param dLat 定位的纬度  单位为度   范围为-90度～90度  北纬为正 南纬为负
     *@param dLong 定位的经度 单位为度   范围为-180-180度  东经为正 西经为负
     *@param pTransMat  存储转换的旋转矩阵
     *@param pTransVec  存储平移向量
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

    */
    bool mlCalcTransMatrixByLatLong(double dLat,double dLong,CmlMat* pTransMat, DOUBLE* pTransVec);

protected:
private:
};

#endif // CMLCOORDTRANS_H
