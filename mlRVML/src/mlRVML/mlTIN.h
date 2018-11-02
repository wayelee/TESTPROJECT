/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlTIN.cpp
* @date 2012.01
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 不规则三角网类实现头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#ifndef CMLTIN_H
#define CMLTIN_H

#include "mlBase.h"
#include "Triangle.h"


/**
* @class CmlTIN
* @date 2011.11
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 不规则三角网类定义
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/
#define  MAX_DEM  1.0E+38
#define  MIN_DEM -1.0E+38
#define  EPSILON  0.0001
#define  MAX_TRI  65535
#define  ZERO 0.000001
#define  NULL_INDEX -10000

enum  POINT_TRIANGLE
{
	INNER =0,
	OUTER =1,
	EDGE =2,
	VERTEX =3
};

class CmlTIN
{
public:
    /**
     *@fn ~CmlTIN()
     *@date 2011.11
     *@author 张重阳
     *@brief TIN类构造函数
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    CmlTIN();


    /**
     *@fn ~CmlTIN()
     *@date 2011.11
     *@author 张重阳
     *@brief TIN类析构函数
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    ~CmlTIN();
public:
    //2维三角网
 //   bool Build2By2DPt( Pt2d* ptList, int64 lPtNum );
    /**
     *@fn Build2By2DPt()
     *@date 2011.11
     *@author 张重阳
     *@brief 根据二维坐标（x，y）构造三角网 构造后的三角网储存在TIN私有成员变量m_tri中
     *@param ptList 点序列指针
     *@param lPtNum 点数
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool Build2By2DPt( DOUBLE* ptList, SLONG lPtNum); //2维点序列指针，按XY存储

 //   bool Build2By3DPt( Pt3d* ptList, int64 lPtNum );

    /**
     *@fn Build2By3DPt()
     *@date 2011.11
     *@author 张重阳
     *@brief 根据三维点构TIN 构造后的三角网储存在TIN私有成员变量m_tri中
     *@param ptList 点序列指针,每个点坐标按照x、y、z顺序排列
     *@param lPtNum 点数
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool Build2By3DPt( DOUBLE* ptList, SLONG lPtNum ); //3维点序列指针，按XYZ存储

    /**
     *@fn Build2By3DPt()
     *@date 2011.11
     *@author 张重阳
     *@brief 根据三维点构TIN 构造后的三角网储存在TIN私有成员变量m_tri中
     *@param vector<Pt3d> 三维点向量
     *@param lPtNum 点数
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool Build2By3DPt( vector<Pt3d> &vecPt ); //3维点序列指针，按XYZ存储

   /**
     *@fn GetNumOfCorners()
     *@date 2011.11
     *@author 张重阳
     *@brief 获取角点数目
     *@retval 角点数目
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    UINT GetNumOfCorners();

    /**
     *@fn GetNumOfTriangles()
     *@date 2011.11
     *@author 张重阳
     *@brief 获取三角形数目
     *@retval 三角形数目
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    UINT GetNumOfTriangles();

    /**
     *@fn GetCornersByTriIndex()
     *@date 2011.11
     *@author 张重阳
     *@brief 根据三角形索引号得到三个角点的三位坐标值
     *@param tri_index 三角形索引号
     *@param pPt3ds 三角形三个顶点坐标
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool GetCornersByTriIndex(UINT tri_index,Pt3d* pPt3ds);

    /**
     *@fn GetTriList()
     *@date 2011.11
     *@author 张重阳
     *@brief 得到三角网三角形序列
     *@retval 三角形序列
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    UINT* GetTriList();

    /**
     *@fn GetPt2List()
     *@date 2011.11
     *@author 张重阳
     *@brief 得到三角网三角形2维点序列
     *@retval 2维点序列
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    DOUBLE* GetPt2List();

    /**
     *@fn GetPt2List()
     *@date 2011.11
     *@author 张重阳
     *@brief 得到三角网三角形Z值序列
     *@retval Z值序列
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    DOUBLE* GetPtZList();

    triangulateio* GetTri(){return &m_tri;};

    triangulateio m_tri;

    /**
     *@fn GetGridTriIndex()
     *@date 2011.11
     *@author 张重阳
     *@brief 创建三角形索引
     *@param *pIndex 索引图像地址
     *@param nW DEM宽度
     *@param nH DEM高度
     *@param tri 三角网指针
     *@param rect DEM范围
     *@param dResolution 分辨率
     *@param *pVecPtHole 洞
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool GetGridTriIndex(SINT *pIndex, UINT nW,UINT nH, DbRect *rect,DOUBLE dResolution,vector<Pt2d> *pVecPtHole = NULL );

    bool GetGridTriIndex(SINT *pIndex, UINT nW,UINT nH, DbRect *rect,DOUBLE dResolution,vector<Polygon3d> *pVecPoly = NULL, vector<Pt2d> *pVecPtHole = NULL );

//    bool GetGridTriIndex(SINT *pIndex, DOUBLE *pDisIndex , UINT nW , UINT nH, DbRect *rect,DOUBLE dResolution,vector<Pt2d> *pVecPtHole ) ;

        /**
     *@fn GetGridTriIndex()
     *@date 2011.11
     *@author 吴凯
     *@brief 创建三角形索引，获取三角形中心
     *@param *pIndex 索引图像地址
     *@param &vecAddPts 三角网加密点点集
     *@param *rect DEM范围
     *@param dResolution 分辨率
     *@param *pVecPtHole 洞
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool GetCenterTriIndex(SINT *pIndex, vector<Pt3d> &vecAddPts , DbRect *rect,DOUBLE dResolution,vector<Pt2d> *pVecPtHole) ;

    bool GetCenterTriIndex(SINT *pIndex, vector<Pt3d> &vecAddPts , DbRect *rect,DOUBLE dResolution,vector<Polygon3d> *pVecPoly = NULL, vector<Pt2d> *pVecPtHole = NULL ) ;

    /**
     *@fn PointInPoly（）
     *@date 2011.11
     *@author 张重阳
     *@brief 判断点是否在多边形内
     *@param *vecPoly 多边形各顶点坐标
     *@param x,y,点的x、y坐标
     *@retval VERTEX 点是三角形的一个顶点
     *@retval 0 点不在多边形内
     *@retval 非0 点在多边形内
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    UINT PointInPoly(DOUBLE x,DOUBLE y,vector<Pt2d> *vecPoly);

    UINT PointInRegion(DOUBLE x,DOUBLE y,vector<Polygon3d> *pVecPoly);

    bool WriteToFiles( const SCHAR* strOutPath, DOUBLE dScale = 1.0 );
private:

    /**
     *@fn MIN3()
     *@date 2011.11
     *@author 张重阳
     *@brief 求解三个数最小值
     *@param x1,x2,x3 输入的三个变量
     *@retval 返回其最小值
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    DOUBLE MIN3(DOUBLE x1,DOUBLE x2,DOUBLE x3);

    /**
     *@fn MAX3()
     *@date 2011.11
     *@author 张重阳
     *@brief 求解三个数最大值
     *@param x1,x2,x3 输入的三个变量
     *@retval 返回其最小值
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    DOUBLE MAX3(DOUBLE x1,DOUBLE x2,DOUBLE x3);
    /**
     *@fn ComputeTriangleRect()
     *@date 2011.11
     *@author 张重阳
     *@brief 创建三角形的外接矩形 注：此矩形的left top right bottom坐标以左下角为原点
     *@param DOUBLE *x，DOUBLE *y 三角形三个顶点的x、y坐标
     *@retval *rect 外接矩形
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    void ComputeTriangleRect(DOUBLE *x, DOUBLE *y,DbRect *rect);

    /**
     *@fn PointInTriangle
     *@date 2011.11
     *@author 张重阳
     *@brief 判断点是否在三角形内
     *@param DOUBLE *x，DOUBLE *y 三角形三个顶点的x、y坐标
     *@param x0,y0,点的x、y坐标
     *@retval VERTEX 点是三角形的一个顶点
     *@retval EDGE 点在三角形的边上
     *@retval OUTER 点在三角形外
     *@retval INNER 点在三角形内
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    POINT_TRIANGLE PointInTriangle(DOUBLE x0,DOUBLE y0,DOUBLE *x,DOUBLE *y);



    /**
     *@fn VertexNotInTriangle()
     *@date 2011.11
     *@author 张重阳
     *@brief 判断点是否是三角形的顶点
     *@param nVt 待判断点索引号
     *@param Vertexs 三角形三个顶点索引号
     *@retval TRUE 点是三角形的顶点
     *@retval FALSE 点不是三角形顶点
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool VertexNotInTriangle(SINT nVt,SINT *Vertexs);
    /**
     *@fn MinVertexDis
     *@date 2011.11
     *@author 吴凯
     *@brief 解求点到三角形的最小距离
     *@param DOUBLE *x，DOUBLE *y 三角形三个顶点的x、y坐标
     *@param x0,y0,点的x、y坐标
     *@retval dMinDis 点到三角形的最小距离
     *@version 1.1
     *@par 修改历史：
     *<作者>    <时间>   <版本编号>    <修改原因>\n
     */
    double MinVertexDis(DOUBLE x0,DOUBLE y0,DOUBLE *x,DOUBLE *y) ;
private:

    DOUBLE *m_pt2List;
    DOUBLE *m_ptZList;
    SLONG m_nPtNum;

};
#endif // CMLTIN_H
