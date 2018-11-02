/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlMat.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩阵基础运算头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef MLMAT_H_INCLUDED
#define MLMAT_H_INCLUDED

#include "../../3rdParty/opencv2.3/opencv.h"
#include "../../include/mlTypes.h"



/**
* @class CmlTemplateMat
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 栅格模板类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
//此栅格结构可以当成矩阵看待，按行存储
template <typename T>
class CmlTemplateMat
{
public:
    /**
    * @fn CmlTemplateMat
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlTemplateMat类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlTemplateMat();
    /**
    * @fn CmlTemplateMat
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlTemplateMat类有参构造函数
    * @param Mat 初始化矩阵
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlTemplateMat( const CmlTemplateMat<T> &Mat );
    /**
    * @fn ~CmlTemplateMat
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlTemplateMat类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    ~CmlTemplateMat();
public:
    /**
    * @fn Initial
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 初始化栅格的大小
    * @param nH 栅格高
    * @param nW 栅格宽
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool Initial( const UINT nH, const UINT nW ) ;   //初始化栅格的大小
    UINT GetW() const;                         //!<得到栅格的宽
    UINT GetH() const;                         //!<得到栅格的高
    UINT GetTSize() const;                     //!<得到栅格的总数
    T*  GetData() const;                      //!<得到栅格数据的头指针
    T   GetAt( const UINT nRow, const UINT nCol ) const ;    //!<得到栅格的某个点的值
    bool   SetAt( const UINT nRow, const UINT nCol, const T val );  //!<设置栅格的某个点的值
    bool Fill( const T val );                 //!<用某值填充栅格
    void DestoryAll();                  //!<删除栅格内所有数据
    void DestoryDataBlock();            //!<删除栅格内的数据存储块，保留相应的宽、高信息
    //利用对于匹配点处理时不至于载入过多数据。
    bool IsValid() const;                     //!<判断是否存在数据块

    CmlTemplateMat<T>& operator=(CmlTemplateMat<T>& tmp); //!<复制拷贝函数

protected:
    UINT m_nW;                               //!<数据块宽
    UINT m_nH;                               //!<数据块高
    T* m_pData;                               //!<数据块头指针
    UINT m_nTSize;                               //!<栅格总数
    bool m_bIsNULL;                               //!<数据块是否为空
};

//---------------------------------------------------------------------------
//因不支持模板类的分离编译，故实现在.h中
/**
* @fn CmlTemplateMat
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlTemplateMat类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
template <typename T>
CmlTemplateMat<T>::CmlTemplateMat()
{
    m_nH = 0;
    m_nW = 0;
    m_bIsNULL = true;
    m_pData = NULL;
    m_nTSize = 0;
};
/**
* @fn CmlTemplateMat
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlTemplateMat类有参构造函数
* @param Mat 初始化矩阵
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
template <typename T>
CmlTemplateMat<T>::CmlTemplateMat( const CmlTemplateMat<T> &tmpMat )
{
    m_nH = 0;
    m_nW = 0;
    m_bIsNULL = true;
    m_pData = NULL;
    m_nTSize = 0;
    if( false == this->Initial( tmpMat.GetH(), tmpMat.GetW() ) )
    {
        return;
    }
    memcpy( this->GetData(), tmpMat.GetData(), this->GetTSize()*sizeof(T) );
};
/**
* @fn ~CmlTemplateMat
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlTemplateMat类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
template <typename T>
CmlTemplateMat<T>::~CmlTemplateMat()
{
    this->DestoryAll();
};
/**
* @fn Initial
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 初始化栅格的大小
* @param nH 栅格高
* @param nW 栅格宽
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
template <typename T>
bool CmlTemplateMat<T>::Initial( const UINT nH, const UINT nW )
{
    if( true != this->m_bIsNULL )
    {
        return false;
    }

    if( NULL == m_pData )
    {
        UINT nTSize = nW * nH;
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
        m_bIsNULL = false;
        memset( m_pData, 0, m_nTSize );
        return true;
    }
    else
    {
        return false;
    }
}

template <typename T>
UINT CmlTemplateMat<T>::GetH() const//!<得到栅格的高
{
    return m_nH;
}

template <typename T>
UINT CmlTemplateMat<T>::GetW() const//!<得到栅格的宽
{
    return m_nW;
}

template <typename T>
UINT CmlTemplateMat<T>::GetTSize() const//!<得到栅格的总数
{
    return m_nTSize;
}

template <typename T>
T* CmlTemplateMat<T>::GetData() const//!<得到栅格数据的头指针
{
    return m_pData;
}

template <typename T>
T CmlTemplateMat<T>::GetAt( const UINT nRow, const UINT nCol ) const//!<得到栅格的某个点的值
{
    assert( ( nRow < m_nH)&&( nCol < m_nW) );
    return m_pData[nRow * m_nW + nCol];
}

template <typename T>
bool CmlTemplateMat<T>::SetAt( const UINT nRow, const UINT nCol, const T val )//!<设置栅格的某个点的值
{
    if( (nRow >= m_nH) || ( nCol >= m_nW ) )
    {
        return false;
    }

    T* pTmp = m_pData + nRow * m_nW + nCol;
    *pTmp = val;
    return true;
}

template <typename T>
bool CmlTemplateMat<T>::Fill( const T val )//!<用某值填充栅格
{
    if( true == m_bIsNULL )
    {
        return false;
    }
    for( SINT i = 0; i < m_nH; ++i )
    {
        for( SINT k = 0; k < m_nW; ++i )
        {
            SetAt( i, k, val );
        }
    }
    return true;
}

template <typename T>
void CmlTemplateMat<T>::DestoryAll()//!<删除栅格内所有数据
{
    DestoryDataBlock();
    m_nW = 0;
    m_nH = 0;
    m_nTSize = 0;
}

template <typename T>
void CmlTemplateMat<T>::DestoryDataBlock()//!<删除栅格内的数据存储块，保留相应的宽、高信息
{
    if( NULL == m_pData )
    {
        return;
    }

    delete[] m_pData;
    m_pData = NULL;

    m_bIsNULL = true;
}

template <typename T>
bool CmlTemplateMat<T>::IsValid() const//!<判断是否存在数据块
{
    return (!m_bIsNULL);
}

template <typename T>
CmlTemplateMat<T>& CmlTemplateMat<T>::operator=( CmlTemplateMat<T>& tmp )//!<复制拷贝函数
{
    if( true != m_bIsNULL )
    {
        this->DestoryAll();
    }
    if( true == this->Initial( tmp.GetH(), tmp.GetW() ) )
    {
        memcpy( this->GetData(), tmp.GetData(), sizeof(T)*tmp.GetTSize() );
    }
    return *this;
};

//矩阵运算公共函数，矩阵以左上角为原点
typedef CmlTemplateMat<DOUBLE> CmlMat;    //!<矩阵（浮点型）

//使用矩阵函数前对传入的矩阵---无需----进行维度初始化！！！
/**
* @fn mlMatTrans
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩阵转置
* @param pMatIn 输入矩阵
* @param pMatOut 输出矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatTrans( CmlMat* pMatIn, CmlMat* pMatOut );
/**
* @fn mlMatAdd
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩阵加法
* @param pMatInA 输入矩阵A
* @param pMatInB 输入矩阵B
* @param pMatOutC 输出结果矩阵C
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatAdd( CmlMat* pMatInA, CmlMat* pMatInB, CmlMat* pMatOutC );
/**
* @fn mlMatSub
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩阵减法
* @param pMatInA 输入矩阵A
* @param pMatInB 输入矩阵B
* @param pMatOutC 输出结果矩阵C
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatSub( CmlMat* pMatInA, CmlMat* pMatInB, CmlMat* pMatOutC );
/**
* @fn mlMatMul
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩阵乘法
* @param pMatInA 输入矩阵A
* @param pMatInB 输入矩阵B
* @param pMatOutC 输出结果矩阵C
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatMul( CmlMat* pMatInA, CmlMat* pMatInB, CmlMat* pMatOutC );
/**
* @fn mlMatInv
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩阵求逆
* @param pMatIn 输入矩阵
* @param pMatOut 输出矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatInv( CmlMat* pMatIn,  CmlMat* pMatOut );
/**
* @fn mlMatDet
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 求矩阵行列式值
* @param pMat 输入矩阵
* @param dRes 行列式的值
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatDet( CmlMat* pMat, DOUBLE &dRes );
/**
* @fn mlMatSolveSVD
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 解方程A*x=B
* @param pMatInA 输入矩阵A
* @param pMatInB 输入矩阵B
* @param pMatOutX 输出结果矩阵X
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatCross3( CmlMat* pMatInA, CmlMat* pMatInB, CmlMat* pMatOutC );
/**
* @fn mlMatSVD
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief SVD分解，A=U*W*V'
* @param pMatA 输入矩阵A
* @param pMatU 输出矩阵U
* @param pMatW 输出矩阵W
* @param pMatV 输出矩阵V
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatSolveSVD( CmlMat* pMatInA, CmlMat* pMatInB, CmlMat* pMatOutX );
/**
* @fn mlMatCross3
* @date 2011.11.02
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 三维向量叉乘
* @param pMatInA 输入三维向量A
* @param pMatInB 输入三维向量B
* @param pMatOutC 输出结果矩阵C
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI(bool) mlMatSVD( CmlMat* pMatA, CmlMat* pMatU, CmlMat* pMatW, CmlMat* pMatV );
/**
* @fn CmlMat2CvMat
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 将CmlMat转化成CvMat
* @param pmlMat CmlMat型矩阵
* @param pcvMat CvMat型矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool mlMat2CvMat( CmlMat* pmlMat, CvMat* &pcvMat);
/**
* @fn CvMat2mlMat
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 将CvMat转化成CmlMat
* @param pcvMat CvMat型矩阵
* @param pmlMat CmlMat型矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CvMat2mlMat( CvMat* pcvMat, CmlMat* pmlMat );

#endif // MLMAT_H_INCLUDED
