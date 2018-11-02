/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlKinging.h
* @date 2012.01
* @author 吴凯 wukai@irsa.ac.cn
* @brief Kringing插值功能模块类头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef MLKRINGING_H_INCLUDED
#define MLKRINGING_H_INCLUDED

#include "mlBase.h"
#include "mlMat.h"
#include "mlTIN.h"

enum KRIGING_MODEL
{
	POLYNOMIANL,
	SPHERICAL,
	EXPONENTIAL,
	SPHERICAL_EXPONENTIAL
};

enum KRIGING_State
{
    KRIGING_OFF,
    KRIGING_ON,
};

/**
* @class CmlKringing
* @date 2011.11
* @author 吴凯 wukai@irsa.ac.cn
* @brief CmlKringing类定义
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CmlKringing
{
public:
public:
    /**
     *@fn CmlKringing
     *@date 2011.11
     *@author 吴凯
     *@brief Kring类构造函数
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    CmlKringing() ;
    /**
     *@fn ~CmlKringing
     *@date 2011.11
     *@author 吴凯
     *@brief Kring类析构函数
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    ~CmlKringing() ;
    /**
     *@fn InitVariogram
     *@date 2012.02
     *@author 吴凯
     *@brief Kring类参数初始化函数
     *@param dRange Kring模型距离参数
     *@param dShort Kring多项式模型短距离参数
     *@param dLong  Kring多项式模型长距离参数
     *@param dDisShort 选择Kring多项式长短距离模型参数的阈值
     *@param dGraceStart Kring球形模型短距离参数
     *@param dGraceEnd Kring球形模型长距离参数
     *@param dSphericalPara Kring球形模型短距离参数
     *@param dExponentialPara Kring球形模型长距离参数
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void InitVariogram(DOUBLE *dShort=NULL,DOUBLE *dLong=NULL , DOUBLE dRange = 50 , DOUBLE dDisShort = 5.0 , DOUBLE dGraceStart = 5.0 ,
                       DOUBLE dGraceEnd = 20.0 , DOUBLE dSphericalPara = 0.00001738 , DOUBLE dExponentialPara= 0.00001738);
    /**
     *@fn GetValueFromTins
     *@date 2012.02
     *@author 吴凯
     *@brief 通过三角网插值高程数据
     *@param vec3DPts 带地理坐标的3维点云
     *@param tri 点云构的三角格网索引
     *@param pNeigh 插值点周围的邻接点
     *@param outNewPt 插值点
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool GetValueFromTins(const vector<Pt3d>& vec3DPts,triangulateio *tri,
                                  SINT *pNeigh , Pt3d& outNewPt) ;

    /**
     *@fn GetValueFromTins
     *@date 2012.02
     *@author 吴凯
     *@brief 通过三角网插值高程数据
     *@param vec3DPts 带地理坐标的3维点云
     *@param tri 点云构的三角格网索引
     *@param pNeigh 插值点周围的邻接点
     *@param outNewPt 插值点
     *@param bFlag 设置克里金插值的两种方式
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
    */

    bool GetValueFromTin(const vector<Pt3d>& vec3DPts,triangulateio *tri,SINT *pNeigh ,
                         Pt3d& outNewPt , KRIGING_State bFlag = KRIGING_ON);
private:
    DOUBLE m_dDisMin ; // 判断是否参与克里金插值解算的阈值
    KRIGING_MODEL m_eKrigingModel;//Kriging模型
	DOUBLE m_dShortPara[4],m_dLongPara[4];//Kring多项式模型长短距离的参数
	DOUBLE m_dDisShort;//选择多项式长短距离模型参数的阈值
	DOUBLE m_dRange ; // 克里金插值距离参数
	DOUBLE m_dSphericalPara ; // 克里金插值球形模型参数
	DOUBLE m_dExponentialPara; // 克里金插值指数模型参数
	DOUBLE m_dGraceStart,m_dGraceEnd; // Kring 球形模型长短距离参数
private:
    /**
     *@fn GetNeighborVertexInMinDis
     *@date 2012.02
     *@author 吴凯
     *@brief  搜索点周围的邻接点
     *@param inPt 目标点
     *@param vec3DPts 三维点云
     *@param tri 点云三角网索引
     *@param pNeigh 目标点周围的邻接点
     *@param nCount 邻接点数目
     *@param vecNeighborPts 参与克里金插值的邻接点
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
    */

    bool GetNeighborVertexInMinDis( Pt3d& inPt,const vector<Pt3d>& vec3DPts ,triangulateio *tri,SINT *pNeigh,SINT nCount, vector<Pt3d>& vecNeighborPts);
    /**
     *@fn VertexNotInTriangle
     *@date 2012.02
     *@author 吴凯
     *@brief 判断点是否在三角形内
     *@param nVt 待判断点索引号
     *@param Vertexs 三角形三个顶点索引号
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool VertexNotInTriangle(SINT nVt,SINT *Vertexs) ;
    /**
     *@fn semivariogram()
     *@date 2012.02
     *@author 吴凯
     *@brief 计算克里金插值两点的相关系数
     *@param *p1 点1的地理坐标
     *@param *p2 点2的地理坐标
     *@param m_eKrigingModel 克里金插值模型
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
    */
    double semivariogram(Pt3d *p1, Pt3d *p2 , KRIGING_MODEL m_eKrigingModel = POLYNOMIANL) ;
};


#endif // MLKRINGING_H_INCLUDED
