/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlKinging.cpp
* @date 2012.01
* @author 吴凯 wukai@irsa.ac.cn
* @brief Kringing插值功能模块类源文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#include "mlKringing.h"
/**
 *@fn CmlKringing
 *@date 2012.02
 *@author 吴凯
 *@brief CmlKringing构造函数
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlKringing::CmlKringing()
{

}

/**
 *@fn ~CmlKringing
 *@date 2012.02
 *@author 吴凯
 *@brief CmlKringing析构函数
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlKringing::~CmlKringing()
{

}
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
void CmlKringing::InitVariogram(DOUBLE *dShort,DOUBLE *dLong, DOUBLE dRange , DOUBLE dDisShort , DOUBLE dGraceStart , DOUBLE dGraceEnd ,
                                DOUBLE dSphericalPara , DOUBLE dExponentialPara)
{
    // 克里金插值模型参数赋值
    if (dShort==NULL)
    {
        DOUBLE dShortNew[] = {0.00001738, 0.00005993, 0.00053967, 0.00001876};
        memcpy(m_dShortPara,dShortNew,sizeof(DOUBLE)*4);
        DOUBLE dLongNew[] = {-0.00000189, 0.00027033, -0.00109061, 0.00526723};
        memcpy(m_dLongPara,dLongNew,sizeof(DOUBLE)*4);
    }
    else
    {
        memcpy(m_dShortPara,dShort,sizeof(DOUBLE)*4);
        memcpy(m_dLongPara,dLong,sizeof(DOUBLE)*4);
    }
    m_dRange = dRange ;
    m_dDisShort = dDisShort ;
    m_dGraceStart =  dGraceStart ;
    m_dGraceEnd = dGraceEnd ;
    m_dSphericalPara = dSphericalPara ;
    m_dExponentialPara = dExponentialPara ;
}

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

bool CmlKringing::GetValueFromTins(const vector<Pt3d>& vec3DPts,triangulateio *tri,
                                   SINT *pNeigh , Pt3d& outNewPt)
{
    // 得到插值点附近的参与克里金插值的点
    vector<Pt3d> vecNeighborPts;
    GetNeighborVertexInMinDis(outNewPt , vec3DPts , tri , pNeigh , 4 ,vecNeighborPts) ;
    SINT nVertex = vecNeighborPts.size() ;
    // 参与插值的点不足，退出
    if (nVertex < 4)
    {
        return false;
    }
    CmlMat A, B ,W ;
    A.Initial(nVertex+1 , nVertex+1) ;
    B.Initial(nVertex+1 , 1) ;
    // 插值点权因数计算
    for (SINT i=0; i<nVertex; i++)
    {
        A.SetAt(i, i , 0) ;
        A.SetAt(i, nVertex , 1) ;
        A.SetAt(nVertex, i, 1) ;
        B.SetAt(i , 0 , semivariogram(&(vecNeighborPts[i]), &outNewPt)) ;
    }

    A.SetAt(nVertex, nVertex , 0) ;
    B.SetAt(nVertex, 0 ,1) ;
    // 协相关权因数矩阵计算
    for(SINT i=0 ; i<nVertex-1 ; i++)
    {
        for(SINT j=i+1 ; j<nVertex ; j++)
        {
            A.SetAt(i , j , semivariogram(&(vecNeighborPts[i]), &(vecNeighborPts[j]))) ;
            A.SetAt(j , i , A.GetAt(i ,j)) ;
        }
    }
    // 插值点权计算
    CmlMat ATran , ATranA , ATranAInv , ATranB ;
    if(!mlMatTrans(&A , &ATran) || !mlMatMul(&ATran , &A , &ATranA) || !mlMatInv(&ATranA , &ATranAInv)
            ||!mlMatMul(&ATran , &B , &ATranB) || !mlMatMul(&ATranAInv , &ATranB , &W) )
    {
        SCHAR strErr[] = "GetValueFromTin error : correlation plus method fails" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    double dWeightSum = 0 ;
    double dEstimateZ= 0;
    double dWeightMax = -1000;
    // 计算最终高程值
    for(SINT i=0 ; i<nVertex ; i++)
    {
        dWeightSum += W.GetAt(i,0);
        dWeightMax = max(dWeightMax, fabs(W.GetAt(i,0)));
    }
    if(dWeightMax == 0)
    {
        return false ;
    }
    for(SINT i=0 ; i<nVertex ; i++)
    {
        dEstimateZ += (W.GetAt(i,0) * vecNeighborPts[i].Z)/dWeightSum ;
    }
    vecNeighborPts.clear() ;
    outNewPt.Z = dEstimateZ;
    LOGAddSuccessQuitMsg() ;
    return true ;
}


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

bool CmlKringing::GetValueFromTin(const vector<Pt3d>& vec3DPts,triangulateio *tri,
                                  SINT *pNeigh , Pt3d& outNewPt , KRIGING_State bFlag)
{
    // 得到插值点附近的参与克里金插值的点
    vector<Pt3d> vecNeighborPts;
    GetNeighborVertexInMinDis(outNewPt , vec3DPts , tri , pNeigh , 4 ,vecNeighborPts) ;
    SINT nVertex = vecNeighborPts.size() ;
    // 判断克里金插值的方式
    if(bFlag)
    {
        // 参与插值的点不足，退出
        if (nVertex < 4)
        {
            SCHAR strErr[] = "GetValueFromTin error : not enough neighboring points" ;
            LOGAddErrorMsg(strErr) ;
            return false;
        }
        CmlMat A, B ,W ;
        A.Initial(nVertex+1 , nVertex+1) ;
        B.Initial(nVertex+1 , 1) ;
        // 插值点权因数计算
        for (SINT i=0; i<nVertex; i++)
        {
            A.SetAt(i, i , 0) ;
            A.SetAt(i, nVertex , 1) ;
            A.SetAt(nVertex, i, 1) ;
            B.SetAt(i , 0 , semivariogram(&(vecNeighborPts[i]), &outNewPt)) ;
        }

        A.SetAt(nVertex, nVertex , 0) ;
        B.SetAt(nVertex, 0 ,1) ;
        // 协相关权因数矩阵计算
        for(SINT i=0 ; i<nVertex-1 ; i++)
        {
            for(SINT j=i+1 ; j<nVertex ; j++)
            {
                A.SetAt(i , j , semivariogram(&(vecNeighborPts[i]), &(vecNeighborPts[j]))) ;
                A.SetAt(j , i , A.GetAt(i ,j)) ;
            }
        }
        // 插值点权计算
        CmlMat ATran , ATranA , ATranAInv , ATranB ;
        if(!mlMatTrans(&A , &ATran) || !mlMatMul(&ATran , &A , &ATranA) || !mlMatInv(&ATranA , &ATranAInv)
                ||!mlMatMul(&ATran , &B , &ATranB) || !mlMatMul(&ATranAInv , &ATranB , &W) )
        {
            SCHAR strErr[] = "GetValueFromTin error : correlation plus method fails" ;
            LOGAddErrorMsg(strErr) ;
            return false;
        }
        double dWeightSum = 0 ;
        double dEstimateZ= 0;
        double dWeightMax = -1000;
        // 计算最终高程值
        for(SINT i=0 ; i<nVertex ; i++)
        {
            dWeightSum += W.GetAt(i,0);
            dWeightMax = max(dWeightMax, fabs(W.GetAt(i,0)));
        }
        if(dWeightMax == 0)
        {
            return false ;
        }
        for(SINT i=0 ; i<nVertex ; i++)
        {
            dEstimateZ += (W.GetAt(i,0) * vecNeighborPts[i].Z)/dWeightSum ;
        }
        vecNeighborPts.clear() ;
        outNewPt.Z = dEstimateZ;
//        LOGAddSuccessQuitMsg() ;
        return true ;
    }
    // 失效时使用下面替代
    else
    {
        CmlMat A, B ,W ;
        A.Initial(3,3) ;
        B.Initial(3,1) ;
        SINT vt[3];//三角形顶点编号
        Pt3d dbPoint;
        // 协相关权因数矩阵计算
        for (SINT i=0; i<3; i++)
        {
            vt[i]=tri->triList[3*pNeigh[0]+i];
            dbPoint=vec3DPts[vt[i]];
            A.SetAt(i , 0 , dbPoint.X) ;
            A.SetAt(i , 1 , dbPoint.Y) ;
            A.SetAt(i , 2 , dbPoint.Z) ;
            B.SetAt(i , 0 , 1.0) ;
        }
        // 插值点权计算
        CmlMat ATran , ATranA , ATranAInv , ATranB ;
        if(!mlMatTrans(&A , &ATran) || !mlMatMul(&ATran , &A , &ATranA) || !mlMatInv(&ATranA , &ATranAInv)
                ||!mlMatMul(&ATran , &B , &ATranB) || !mlMatMul(&ATranAInv , &ATranB , &W) )
        {
            SCHAR strErr[] = "GetValueFromTin error : correlation method fails" ;
            LOGAddErrorMsg(strErr) ;
            return false;
        }
        if (W.GetAt(2,0) == 0)
        {
            return false;
        }
        // 计算最终高程值
        double dEstimateZ= 0;
        dEstimateZ = (1 - W.GetAt(0,0) * outNewPt.X - W.GetAt(1,0) * outNewPt.Y )/ W.GetAt(2,0);
        vecNeighborPts.clear() ;
        outNewPt.Z = dEstimateZ;
//        LOGAddSuccessQuitMsg() ;
        return true ;
    }
}

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

bool CmlKringing::GetNeighborVertexInMinDis( Pt3d& inPt,const vector<Pt3d>& vec3DPts ,triangulateio *tri,
        SINT *pNeigh,SINT nCount,vector<Pt3d>& vecNeighborPts)
{
//    DOUBLE dDis ;
    DOUBLE x , y ;
    SINT vt[4][3];//三角形顶点编号,存储三角形及和其相邻的三个三角形的顶点
    x = inPt.X ;
    y = inPt.Y ;
    //得到中心三角形的三个顶点
    for (SINT j=0; j<3; j++)
    {
        vt[0][j] = tri->triList[3*pNeigh[0]+j];
        vecNeighborPts.push_back(vec3DPts[vt[0][j]]) ;
    }
//    //得到相邻三角形的顶点
    for (SINT i=1; i<nCount; i++)
    {
        if (pNeigh[i]<0)
        {
            continue;
        }
        for (SINT j=0; j<3; j++)
        {
            vt[i][j] = tri->triList[3*pNeigh[i]+j];
            if (VertexNotInTriangle(vt[i][j],vt[0]))
            {
                vecNeighborPts.push_back(vec3DPts[vt[i][j]]) ;
            }
        }
    }
    return true ;
}

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

bool CmlKringing::VertexNotInTriangle(SINT nVt,SINT *Vertexs)
{
    bool bIn=true;
    for (SINT i=0; i<3; i++)
    {
        if (Vertexs[i]==nVt)
        {
            bIn=false;
            break;
        }
    }
    return bIn;
}


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
double CmlKringing::semivariogram(Pt3d *p1, Pt3d *p2 , KRIGING_MODEL m_eKrigingModel)
{
    // 根据采用的多项式模型计算权因子
    double dSemiVariogram = 0.0;
    double dDistance = sqrt((p1->X - p2->X) * (p1->X - p2->X) + (p1->Y - p2->Y) * (p1->Y - p2->Y) );
    // 多项式模型
    if(m_eKrigingModel == POLYNOMIANL)
    {
        if(dDistance < m_dDisShort)
        {
            dSemiVariogram = ((m_dShortPara[0] * dDistance + m_dShortPara[1]) * dDistance + m_dShortPara[2]) * dDistance + m_dShortPara[3];
        }
        else
        {
            dSemiVariogram = ((m_dLongPara[0] * dDistance + m_dLongPara[1]) * dDistance + m_dLongPara[2]) * dDistance + m_dLongPara[3];
        }
    }
    // 克里金球型模型
    else if(m_eKrigingModel == SPHERICAL)
    {
        dSemiVariogram = m_dSphericalPara * (3 * dDistance / 2 / m_dRange - pow(dDistance, 3) / 2 / pow(m_dRange, 3));
    }
    // 克里金球型指数模型
    else if(m_eKrigingModel == SPHERICAL_EXPONENTIAL)
    {
        double	dSemiVariogramSpherical = m_dSphericalPara * (3 * dDistance / 2 / m_dRange - pow(dDistance, 3) / 2 / pow(m_dRange, 3)),
                                          dSemiVariogramExponential = (1 - exp(dDistance/m_dRange)) * m_dExponentialPara ;
        double dRatio;
        if(dDistance < m_dGraceStart)
            dSemiVariogram = dSemiVariogramSpherical;
        else if(dDistance > m_dGraceEnd)
            dSemiVariogram = dSemiVariogramExponential;
        else
        {
            dRatio = (dDistance - m_dGraceStart) / (m_dGraceEnd - m_dGraceStart);
            dSemiVariogram = (1 - dRatio) * dSemiVariogramSpherical + dRatio * dSemiVariogramExponential;
        }
    }
    return dSemiVariogram;
}



