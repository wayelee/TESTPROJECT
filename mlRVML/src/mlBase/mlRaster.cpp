/************************************************************************
Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
文件名称:   mlRaster.cpp
创建日期:   2011.11.02
作    者:   万文辉
描    述:   ml工程中栅格数据结构
版本编号:   1.0
修改历史:    <作者>    <时间>   <版本编号>    <描述>


************************************************************************/
#include "../../include/mlBase.h"

template <typename T>
CRaster<T>::CRaster()
{
    m_bIsNULL = true;
    m_pData = NULL;
    m_nTSize = 0;
};

template <typename T>
CRaster<T>::~CRaster()
{
    this->Destory();
};

template <typename T>
bool CRaster<T>::Initial( int nW, int nH, T val )
{
    if( true != this->m_bIsNull )
    {
        return false;
    }

    if( NULL == m_pData )
    {
        int nTSize = nW * nH;
        try
        {
            m_pData = new T[nTSize];
        }
        catch( const bad_alloc& e)
        {
            m_pData = NULL;
            return false;
        }
        m_nW = nW;
        m_nH = nH;
        m_nTSize = nTSize;
        memset( m_pData, m_nTSize, val );
        m_bIsNULL = false;
        return true;
    }
    else
    {
        return false;
    }
}

template <typename T>
int CRaster<T>::GetH()
{
    return m_nH;
}

template <typename T>
int CRaster<T>::GetW()
{
    return m_nW;
}

template <typename T>
int CRaster<T>::GetTSize()
{
    return m_nTSize;
}

template <typename T>
T* CRaster<T>::GetData()
{
    return m_pData;
}

template <typename T>
T CRaster<T>::GetAt( int nRow, int nCol )
{
    return m_pData[nRow * m_nW + nCol];
}

template <typename T>
bool CRaster<T>::SetAt( int nRow, int nCol, T val )
{
    if( (nRow >= m_nH) || ( nCol >= m_nW ) )
    {
        return false;
    }

    T* pTmp = m_pData( nRow * m_nW + nCol );
    *pTmp = val;
    return true;
}

template <typename T>
bool CRaster<T>::Fill( T val )
{
    memset( m_pData, m_nTSize, val );
    return true;
}

template <typename T>
void CRaster<T>::Destory()
{
    if( NULL == m_pData )
    {
        return;
    }
    delete[] m_pData;
    m_nW = 0;
    m_nH = 0;
    m_nTSize = 0;
    m_bIsNULL = true;
}

template <typename T>
CRaster<T>& CRaster<T>::operator=( CRaster<T>& tmp )
{
    if(m_bIsNULL == true)
    {
        T s;
        this->Initial( tmp.GetW(), tmp.GetH(), s );
        memcpy( this->GetData(), tmp.GetData(), tmp.GetTSize() );
        return *this;
    }
}


