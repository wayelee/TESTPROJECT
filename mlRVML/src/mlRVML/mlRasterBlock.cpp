/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlRasterBlock.cpp
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 栅格数据块处理源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#include "mlBase.h"
#include "mlGdalDataset.h"
#include "mlRasterBlock.h"
#include "mlGeoRaster.h"
/**
* @fn CmlRasterBlock
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlRasterBlock类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlRasterBlock::CmlRasterBlock()
{
    m_pGdalData = NULL;
    m_nXOffSet = m_nYOffSet = m_nZoom = m_nBytes = m_nHeight = m_nWidth = 0;

}
/**
* @fn CmlRasterBlock
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 栅格数据块初始化函数
* @param nXOffSet  栅格数据块X偏移量
* @param nYOffSet  栅格数据块Y偏移量
* @param nW  栅格数据块宽
* @param nH  栅格数据块高
* @param nBytes 栅格数据块位数
* @param nZoom  栅格数据块缩放系数
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/

CmlRasterBlock::CmlRasterBlock( SINT nXOffSet, SINT nYOffSet, UINT nW, UINT nH, UINT nBytes, UINT nZoom )
{
    m_pGdalData = NULL;

    this->InitialImg( nH, nW, nBytes );
    m_nXOffSet = nXOffSet;
    m_nYOffSet = nYOffSet;
    m_nZoom = nZoom;
    m_nBytes = nBytes;
}
/**
* @fn ~CmlRasterBlock
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlRasterBlock类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlRasterBlock::~CmlRasterBlock()
{
    m_nXOffSet = 0;
    m_nYOffSet = 0;
}
/**
* @fn InitialImg
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 初始化栅格内存函数
* @param nW  栅格数据块宽
* @param nH  栅格数据块高
* @param nBytes 栅格数据块位数
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlRasterBlock::InitialImg( UINT nH, UINT nW, UINT nBytes )
{
    if ( true == this->Initial( nH, nW*nBytes ) )
    {
        m_nBytes = nBytes;
        m_nHeight = nH;
        m_nWidth = nW;
        return true;
    }
    else
    {
        return false;
    }
}
/**
* @fn ReSet
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 清空数据，不删除内存
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlRasterBlock::ReSet()
{
    memset( m_pData, m_nTSize, 0 );

    return true;
}
/**
* @fn Clear
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 重置结构，清空数据，销毁信息
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlRasterBlock::Clear()
{
    this->DestoryAll();
    m_nXOffSet = 0;
    m_nYOffSet = 0;
    m_nZoom = 1;

    return true;
}

bool CmlRasterBlock::SetXOffSet( SINT nXOffSet )
{
    m_nXOffSet = nXOffSet;
    return true;
}
bool CmlRasterBlock::SetYOffSet( SINT nYOffSet )
{
    m_nYOffSet = nYOffSet;
    return true;
}
bool CmlRasterBlock::SetZoom( UINT nZoom )
{
    m_nZoom = nZoom;
    return true;
}
/**
* @fn GetPtrAt
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 得到某像素数据值的头指针
* @param nH  栅格数据块高
* @param nW  栅格数据块宽
* @return 某像素数据值的头指针
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
BYTE* CmlRasterBlock::GetPtrAt( const UINT nH, const UINT nW ) const
{
    if( ( nH >= m_nHeight )||( nW >= m_nWidth )||( this->IsValid() == false ) )
    {
        return NULL;
    }

    return ( m_pData + ( m_nWidth * nH + nW )* m_nBytes );
}
/**
* @fn SetPtrAt
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 设置某像素数据值
* @param nH  栅格数据块高
* @param nW  栅格数据块宽
* @param pVal 待设置值的BYTE型头指针
* @return 某像素数据值的头指针
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlRasterBlock::SetPtrAt( const UINT nH, const UINT nW, BYTE* pVal )
{
    BYTE* pDes = GetPtrAt( nH, nW );
    if( pDes != NULL )
    {
        memcpy( pDes, pVal, m_nBytes );
    }
    else
    {
        return false;
    }
    return true;
}
/**
* @fn GetAt
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 得到栅格的某个点数据域第一个字节值，若block为单字节类型，则为像素值
* @param nRow  栅格数据行号
* @param nCol 栅格数据块列号
* @return 栅格的某个点数据域第一个字节值
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
BYTE CmlRasterBlock::GetAt( const UINT nRow, const UINT nCol )
{
    if( ( nRow >= m_nHeight )||( nCol >= m_nWidth ) )
    {
        return 0;
    }
    else
    {
        return m_pData[m_nBytes*( nRow * m_nWidth + nCol )];
    }

}
/**
* @fn SetAt
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 设置栅格的某个点数据域第一个字节值，若block为单字节类型，则为设置像素值
* @param nRow  栅格数据行号
* @param nCol 栅格数据块列号
* @param val 栅格的某个点数据域第一个字节值
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlRasterBlock::SetAt( const UINT nRow, const UINT nCol, BYTE val )  //设置栅格的某个点数据域第一个字节值，若block为单字节类型，则为设置像素值
{
    if( (nRow >= m_nHeight) || ( nCol >= m_nWidth ) )
    {
        return false;
    }

    BYTE* pTmp = m_pData + m_nBytes*( nRow * m_nWidth + nCol);
    *pTmp = val;
    return true;
}
/**
* @fn GetDoubleVal
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 得到数据块内像素的值（强制转换成double值）
* @param nH  数据块高
* @param nW  数据块宽
* @param dVal 像素值
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/

bool CmlRasterBlock::GetDoubleVal( const UINT nH, const UINT nW, double &dVal ) const
{
    if( ( nH >= m_nHeight ) ||( nW >= m_nWidth ) )
    {
        return false;
    }
    BYTE* pVal = this->GetPtrAt( nH, nW );

    switch( m_GDTType )
    {
    case GDT_Byte:
        dVal = ( (double)(*pVal) );
        break;
    case GDT_Int16:
        dVal = ((double)(*((SSHORT*)pVal)));
        break;
    case GDT_UInt16:
        dVal = ((double)(*((USHORT*)pVal)));
        break;
    case GDT_Int32:
        dVal = ((double)(*((SINT*)pVal)));
        break;
    case GDT_UInt32:
        dVal = ((double)(*((UINT*)pVal)));
        break;
    case GDT_Float32:
        dVal = ((double)(*((FLOAT*)pVal)));
        break;
    case GDT_Float64:
        dVal = ((double)(*((DOUBLE*)pVal)));
        break;
    default:
        return false;
        break;
    }

    return true;
}
/**
    * @fn SetDoubleVal
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 设置数据块内像素的值（将double值转换为对应类型的值）
    * @param nH  数据块高
    * @param nW  数据块宽
    * @param dVal 像素值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
bool CmlRasterBlock::SetDoubleVal( const UINT nH, const UINT nW, double dVal )
{
    if( ( nH >= m_nHeight ) ||( nW >= m_nWidth ) )
    {
        return false;
    }
    BYTE* pVal = this->GetPtrAt( nH, nW );

    BYTE bTempValByte;
    SSHORT nTempValSShort;
    USHORT nTempValUShort;
    SINT nTempValSInt;
    UINT nTempValUInt;
    FLOAT dTempValFloat;

    switch( m_GDTType )
    {
        case GDT_Byte:
            bTempValByte = (BYTE)(dVal+ML_ZERO);
            memcpy( pVal, &bTempValByte, sizeof(BYTE) );
            break;
        case GDT_Int16:
            nTempValSShort = (SSHORT)(dVal+ML_ZERO);
            memcpy( pVal, &nTempValSShort, sizeof(SSHORT) );
            break;
        case GDT_UInt16:
            nTempValUShort = (USHORT)(dVal+ML_ZERO);
            memcpy( pVal, &nTempValUShort, sizeof(USHORT) );
            break;
        case GDT_Int32:
            nTempValSInt = (SINT)(dVal+ML_ZERO);
            memcpy( pVal, &nTempValSInt, sizeof(SINT) );
            break;
        case GDT_UInt32:
            nTempValUInt = (UINT)(dVal+ML_ZERO);
            memcpy( pVal, &nTempValUInt, sizeof(UINT) );
            break;
        case GDT_Float32:
            dTempValFloat = (FLOAT)(dVal);
            memcpy( pVal, &dTempValFloat, sizeof(FLOAT) );
            break;
        case GDT_Float64:
            memcpy( pVal, &dVal, sizeof(DOUBLE) );
        default:
            break;
    }
    return true;
}
/**
    * @fn GetGeoXYZ
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 得到带坐标信息的数据块内的三维坐标值
    * @param nH  行号
    * @param nW  列号
    * @param ptXYZ 三维坐标值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
bool CmlRasterBlock::GetGeoXYZ( const UINT nH, const UINT nW, Pt3d& ptXYZ ) const
{
    if( ( m_pGdalData == NULL )||( m_pGdalData->m_bIsGeoFile != true) )
    {
        return false;
    }
    CmlGeoRaster* pGeoRaster = (CmlGeoRaster*)m_pGdalData;
    ptXYZ.X = (m_nXOffSet + nW)*pGeoRaster->m_dXResolution + pGeoRaster->m_PtOrigin.X ;
    ptXYZ.Y = (m_nYOffSet + nH)*pGeoRaster->m_dYResolution + pGeoRaster->m_PtOrigin.Y ;
    if ( ( true == GetDoubleVal( nH, nW, ptXYZ.Z ) )&&( fabs( ptXYZ.Z - pGeoRaster->GetNoDataVal()) > ML_ZERO ) )
    {
        return true;
    }
    else
    {
        return false;
    }
}
bool CmlRasterBlock::GetGeoXYZ  ( const UINT nH, const UINT nW, Pt2d ptOrig, const DOUBLE dXRes, const DOUBLE dYRes, const DOUBLE dNoVal, Pt3d& ptXYZ ) const
{
    ptXYZ.X = (m_nXOffSet + nW)*dXRes + ptOrig.X ;
    ptXYZ.Y = (m_nYOffSet + nH)*dYRes + ptOrig.Y ;
    if ( ( true == GetDoubleVal( nH, nW, ptXYZ.Z ) )&&( fabs( ptXYZ.Z - dNoVal ) > ML_ZERO ) )
    {
        return true;
    }
    else
    {
        return false;
    }
}
/**
    * @fn GetGeoZ
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 得到带坐标信息的数据块内的三维高程值
    * @param dX  列号
    * @param dY  行号
    * @param dZ 三维高程值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
bool CmlRasterBlock::GetGeoZ( const DOUBLE dX, const DOUBLE dY, DOUBLE &dZ ) const
{
    if( ( m_pGdalData == NULL )||( m_pGdalData->m_bIsGeoFile != true) )
    {
        return false;
    }
    CmlGeoRaster* pGeoRaster = (CmlGeoRaster*)m_pGdalData;

    double dXCoord = ( dX - pGeoRaster->m_PtOrigin.X ) / pGeoRaster->m_dXResolution ;
    double dYCoord = ( dY - pGeoRaster->m_PtOrigin.Y ) / pGeoRaster->m_dYResolution ;

    if( ( dXCoord < 0 ) || ( dXCoord > ( m_nW - 1) ) || ( dYCoord < 0 ) || ( dYCoord > ( m_nH - 1) ) )
    {
        return false;
    }
    Pt2i ptLT, ptRT, ptLB, ptRB;
    ptLT.X = SINT( dXCoord + ML_ZERO );
    ptLT.Y = SINT( dYCoord + ML_ZERO );
    ptRT.X = ptLT.X + 1;
    ptRT.Y = ptLT.Y;
    ptLB.X = ptLT.X;
    ptLB.Y = ptLT.Y + 1;
    ptRB.X = ptRT.X;
    ptRB.Y = ptLB.Y;

    Pt3d ptZLT, ptZRT, ptZLB, ptZRB;
    if( GetGeoXYZ( ptLT.Y, ptLT.X, ptZLT ) &&  GetGeoXYZ( ptRT.Y, ptRT.X, ptZRT ) &&  GetGeoXYZ( ptLB.Y, ptLB.X, ptZLB ) &&  GetGeoXYZ( ptRB.Y, ptRB.X, ptZRB )  )
    {
        dZ = ( ptRB.X - dXCoord )*( ptRB.Y - dYCoord )* ptZLT.Z +
             ( dXCoord - ptLT.X )*( ptRB.Y - dYCoord )* ptZRT.Z +
             ( ptRB.X - dXCoord )*( dYCoord - ptLT.Y )* ptZLB.Z +
             ( dXCoord - ptLT.X )*( dYCoord - ptLT.Y )* ptZRB.Z ;

        return true;
    }
    else
    {
        return false;
    }

}
bool CmlRasterBlock::GetGeoZ  ( const DOUBLE dX, const DOUBLE dY, Pt2d ptOrig, const DOUBLE dXRes, const DOUBLE dYRes, const DOUBLE dNoVal, DOUBLE &dZ ) const
{

    double dXCoord = ( dX - ptOrig.X ) / dXRes ;
    double dYCoord = ( dY - ptOrig.Y ) / dYRes ;

    if( ( dXCoord < 0 ) || ( dXCoord > ( m_nW - 1) ) || ( dYCoord < 0 ) || ( dYCoord > ( m_nH - 1) ) )
    {
        return false;
    }
    Pt2i ptLT, ptRT, ptLB, ptRB;
    ptLT.X = SINT( dXCoord + ML_ZERO );
    ptLT.Y = SINT( dYCoord + ML_ZERO );
    ptRT.X = ptLT.X + 1;
    ptRT.Y = ptLT.Y;
    ptLB.X = ptLT.X;
    ptLB.Y = ptLT.Y + 1;
    ptRB.X = ptRT.X;
    ptRB.Y = ptLB.Y;

    Pt3d ptZLT, ptZRT, ptZLB, ptZRB;
    if( GetGeoXYZ( ptLT.Y, ptLT.X, ptOrig, dXRes, dYRes, dNoVal, ptZLT ) &&  GetGeoXYZ( ptRT.Y, ptRT.X, ptOrig, dXRes, dYRes, dNoVal, ptZRT ) && \
        GetGeoXYZ( ptLB.Y, ptLB.X, ptOrig, dXRes, dYRes, dNoVal, ptZLB ) &&  GetGeoXYZ( ptRB.Y, ptRB.X, ptOrig, dXRes, dYRes, dNoVal, ptZRB )  )
    {
        dZ = ( ptRB.X - dXCoord )*( ptRB.Y - dYCoord )* ptZLT.Z +
             ( dXCoord - ptLT.X )*( ptRB.Y - dYCoord )* ptZRT.Z +
             ( ptRB.X - dXCoord )*( dYCoord - ptLT.Y )* ptZLB.Z +
             ( dXCoord - ptLT.X )*( dYCoord - ptLT.Y )* ptZRB.Z ;

        return true;
    }
    else
    {
        return false;
    }
}
bool CmlRasterBlock::FreeData()
{
    this->DestoryAll();
    return true;
}
bool CmlRasterBlock::GetSubRasterBlock( UINT nX, UINT nY, UINT nXSize, UINT nYSize, CmlRasterBlock &clsSubBlocks )
{
    if( this->IsValid() == false )
    {
        return false;
    }
    if( clsSubBlocks.IsValid() == false )
    {
        clsSubBlocks.InitialImg( nYSize, nXSize, this->GetBytes() );
        clsSubBlocks.SetGDTType( GetGDTType() );
    }
    for( UINT i = nY; i <= ( nY + nYSize ); ++i )
    {
        if( i >= GetH() )
        {
            continue;
        }
        for( UINT j = nX; j <= (nX + nXSize); ++j )
        {
            if( j >= GetW() )
            {
                continue;
            }
            DOUBLE dCurVal = 0;
            GetDoubleVal( i, j , dCurVal );
            clsSubBlocks.SetDoubleVal( (i-nY), (j-nX), dCurVal );
        }
    }
    return true;
}
bool CmlRasterBlock::GetCentRasterBlock( CmlRasterBlock clsBlocks, UINT &nX, UINT &nY, CmlRasterBlock &clsSubBlock )
{
    UINT nHMin, nHMax, nWMin, nWMax;
    nHMin = nWMin = 0;
    nHMax = clsBlocks.GetH() - 1;
    nWMax = clsBlocks.GetW() - 1;

    for( UINT i = 0; i < clsBlocks.GetH(); ++i )
    {
        bool bIsNull = true;
        for( UINT j = 0; j < clsBlocks.GetW(); ++j )
        {
            DOUBLE dCurVal = -99;
            clsBlocks.GetDoubleVal( i, j, dCurVal );
            if( fabs( dCurVal - 255) > ML_ZERO )
            {
                bIsNull = false;
            }
            if( false == bIsNull )
            {
                nHMin = i;
                break;
            }
        }
        if( bIsNull == false )
        {
            break;
        }
    }

    for( UINT i = clsBlocks.GetH() - 1; i >= 0; --i )
    {
        bool bIsNull = true;
        for( UINT j = 0; j < clsBlocks.GetW(); ++j )
        {
            DOUBLE dCurVal = -99;
            clsBlocks.GetDoubleVal( i, j, dCurVal );
            if( fabs( dCurVal - 255) > ML_ZERO )
            {
                bIsNull = false;
            }
            if( false == bIsNull )
            {
                nHMax = i;
                break;
            }
        }
        if( bIsNull == false )
        {
            break;
        }
    }

    for( UINT j = clsBlocks.GetW() - 1; j >= 0; --j )
    {
        bool bIsNull = true;
        for( UINT i = 0; i < clsBlocks.GetW(); ++i )
        {
            DOUBLE dCurVal = -99;
            clsBlocks.GetDoubleVal( i, j, dCurVal );
            if( fabs( dCurVal - 255) > ML_ZERO )
            {
                bIsNull = false;
            }
            if( false == bIsNull )
            {
                nWMax = j;
                break;
            }
        }
        if( bIsNull == false )
        {
            break;
        }
    }

    for( UINT j = 0; j < clsBlocks.GetW(); ++j )
    {
        bool bIsNull = true;
        for( UINT i = 0; i < clsBlocks.GetW(); ++i )
        {
            DOUBLE dCurVal = -99;
            clsBlocks.GetDoubleVal( i, j, dCurVal );
            if( fabs( dCurVal - 255) > ML_ZERO )
            {
                bIsNull = false;
            }
            if( false == bIsNull )
            {
                nWMin = j;
                break;
            }
        }
        if( bIsNull == false )
        {
            break;
        }
    }
    nX = nWMin;
    nY = nHMin;
    return clsBlocks.GetSubRasterBlock( nWMin, nHMin, (nWMax - nWMin + 1), (nHMax - nHMin + 1), clsSubBlock );
}
