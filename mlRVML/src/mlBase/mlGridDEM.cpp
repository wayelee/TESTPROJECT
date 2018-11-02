/************************************************************************
Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
文件名称:   mlMat.cpp
创建日期:   2011.11.02
作    者:   万文辉
描    述:   ml工程中矩阵运算实现
版本编号:   1.0
修改历史:    <作者>    <时间>   <版本编号>    <描述>


************************************************************************/
#include "../../include/mlBase.h"


//初始化DEM
bool CmlGridDEM::InitialDEM( int nX, int nY, double dCellSize, Pt2d ptLL )
{
    if( true == Initial( nX, nY ) )
    {
        m_dCellSize = dCellSize;
        m_pt2LL = ptLL;
        return true;
    }
    else
    {
        return false;
    }
}

double CmlGridDEM::GetXRange()
{
    return ( m_nW * m_dCellSize );
}

double CmlGridDEM::GetYRange()
{
    return ( m_nH * m_dCellSize );
}

int CmlGridDEM::GetXSize()
{
    return m_nW;
}
int CmlGridDEM::GetYSize()
{
    return m_nH;
}
int CmlGridDEM::GetTSize()
{
    return m_nTSize;
}
double CmlGridDEM::GetZByIndex( int nX, int nY )
{
    return GetAt( nX, nY );
}
//得到格网坐标
bool CmlGridDEM::GetGridXY( int nX, int nY, double& dX, double& dY )
{
    if(( nX >= m_nW )||( nY >= m_nH ) )
    {
        return false;
    }
    dX = m_pt2LL.X + nX * m_dCellSize;
    dY = m_pt2LL.Y + nY * m_dCellSize;
    return true;
}
 //得到格网位置
bool CmlGridDEM::GetIndexByXY( double dX, double dY, int& nX, int& nY )
{
    int nTmpX = (int)( ( dX - m_pt2LL.X + ML_DOUBLEMIN ) / m_dCellSize );
    int nTmpY = (int)( ( dY - m_pt2LL.Y + ML_DOUBLEMIN ) / m_dCellSize );
    if( ( (nTmpX >= 0)&&( nTmpX < m_nW ) ) && ( (nTmpY >= 0)&&( nTmpY < m_nH ) ) )
    {
        nX = nTmpX;
        nY = nTmpY;
        return true;
    }
    else
    {
        return true;
    }
}

//得到dem左下角点坐标
Pt2d CmlGridDEM::GetStartPt()
{
    return m_pt2LL;
};

//得到DEM分辨率
double CmlGridDEM::GetCellSize()
{
    return m_dCellSize;
};

double CmlGridDEM::GetZFromInterXY( double X, double Y )
{
    assert(0);
    return 0;
}
bool CmlGridDEM::SetZAt( int nX, int nY, double dZ )
{
    return SetAt( nX, nY, dZ );
}



