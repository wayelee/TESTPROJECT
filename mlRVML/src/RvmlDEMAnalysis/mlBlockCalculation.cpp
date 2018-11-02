/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlBlockCalculation.cpp
* @date 2011.12.23
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 影像分块源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/

#include "mlBlockCalculation.h"

/**
* @fn CmlBlockCalculation
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlBlockCalculation类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/

CmlBlockCalculation::CmlBlockCalculation()
{
    nBlockW = 0;
    nBlockH = 0;
    nColCount = 0;
    nRowCount = 0;
    nColThres = 3300;
    nRowThres = 3300;
    nOverlayW = 200;
    nOverlayH = 200;
    nLastBlockW = 0;
    nLastBlockH = 0;
}
/**
* @fn CmlBlockCalculation
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlBlockCalculation类有参构造函数
* @param nColThreshold 影像列方向分块大小阈值
* @param nRowThresold 影像行方向分块大小阈值
* @param nBlockOverlayW 影像按列方向初始分块大小
* @param nBlockOverlayH 影像按行方向初始分块大小
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlBlockCalculation::CmlBlockCalculation( const UINT nColThreshold, const UINT nRowThresold, const UINT nBlockOverlayW, const UINT nBlockOverlayH)
{
    nBlockW = 0;
    nBlockH = 0;
    nColCount = 0;
    nRowCount = 0;
    nColThres = nColThreshold;
    nRowThres = nRowThresold;
    nOverlayW = nBlockOverlayW;
    nOverlayH = nBlockOverlayH;
    nLastBlockW = 0;
    nLastBlockH = 0;
}
/**
* @fn ~CmlBlockCalculation
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlBlockCalculation类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlBlockCalculation::~CmlBlockCalculation()
{

}

UINT CmlBlockCalculation::GetBlockW() const //!<得到影像块的宽
{
    return this->nBlockW;
}

UINT CmlBlockCalculation::GetBlockH() const //!<得到影像块的高
{
    return this->nBlockH;
}

UINT CmlBlockCalculation::GetLastBlockW() const//!<得到影像分块最后一块的宽
{
    return this->nLastBlockW;
}

UINT CmlBlockCalculation::GetLastBlockH() const//!<得到影像分块最后一块的高
{
    return this->nLastBlockH;
}

UINT CmlBlockCalculation::GetColCount() const//!<得到影像列方向（X方向）的分块数
{
    return this->nColCount;
}

UINT CmlBlockCalculation::GetRowCount() const//!<得到影像行方向（Y方向）的分块数
{
    return this->nRowCount;
}

/**
* @fn CalBlock
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算影像沿行（列）方向分块的大小的块数
* @param nImgLength 影像行（列）方向的长度
* @param nBlockLength 影像按行（列）方向初始分块大小
* @param nThres 影像按行（列）方向分块阈值
* @param nOverlay 相邻影像块之间的重叠区大小
* @param nBlockL 影像按行（列）方向分块实际大小（含重叠区）
* @param nCount 影像按行或列方向分块块数
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlBlockCalculation::CalBlock( const UINT nImgLength, const UINT nBlockLength ,const UINT nThres,const UINT nOverlay, UINT &nBlockL, UINT &nCount, UINT &nLastBlockL)//
{
    if( ( nImgLength == 0 ) || ( nBlockLength == 0 ) || ( nThres == 0 ) )
    {
        SCHAR strErr[] = "Error:Parameters' value less than zero!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    if( (nBlockLength > nThres) || (nOverlay >= nBlockLength) )
    {
        SCHAR strErr[] = "Error:Block size or Overlay is incorrect!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    DOUBLE dCount = 1.0 * ( nImgLength - nOverlay ) / ( nBlockLength - nOverlay );
    //计算出的分块数小于1的情况
    if (dCount <= 1)
    {
        nCount = 1;
        nBlockL = nImgLength;
        nLastBlockL = nBlockL;
        return true;
    }
    //计算出的分块剩余较小一块的情况
    else if( ( dCount - floor( dCount ) ) <= 0.15 )
    {
        nCount = floor( dCount );
    }
    else
    {
        nCount = ceil( dCount );
    }

    nBlockL = ceil( 1.0 * ( nImgLength - nOverlay ) / nCount + nOverlay ) ;
    if( nBlockL > nThres )
    {
        nCount = nCount + 1;
        nBlockL = ceil( 1.0 * ( nImgLength - nOverlay ) / nCount + nOverlay ) ;

    }
    nLastBlockL = nImgLength - ( nBlockL - nOverlay ) * ( nCount - 1 );
    return true;
}

/**
* @fn CalBlockCol
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算影像沿列（X）方向分块的大小的块数
* @param nImgWidth 影像宽
* @param nBlockWidth 影像初始分块宽
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool  CmlBlockCalculation::CalBlockCol( const UINT nImgWidth, const UINT nBlockWidth )
{
    return CmlBlockCalculation::CalBlock( nImgWidth, nBlockWidth, this->nColThres,this->nOverlayW,this->nBlockW, this->nColCount, this->nLastBlockW);
}

/**
* @fn CalBlockRow
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算影像沿列（X）方向分块的大小的块数
* @param nImgHeight 影像高
* @param nBlockHeight 影像初始分块高
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool  CmlBlockCalculation::CalBlockRow( const UINT nImgHeight, const UINT nBlockHeight )
{
    return CmlBlockCalculation::CalBlock( nImgHeight, nBlockHeight, this->nRowThres, this->nOverlayH, this->nBlockH, this->nRowCount, this->nLastBlockH );
}

/**
* @fn GetBlock
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算影像沿行（列）方向某一块的起始位置
* @param nNum 某块的块号（从0开始）
* @param nBlockL 影像沿行（列）方向的影像块大小
* @param nCount 影像按行或列方向分块块数
* @param nOverlayL 影像沿行（列）方向的重叠区大小
* @return  影像沿行（列）方向某块的起始位置
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
SINT CmlBlockCalculation::GetBlock( const UINT nNum, const UINT nBlockL, const UINT nCount, const UINT nOverlayL ) const
{
    if ( nNum >= nCount )
    {
        SCHAR strErr[] = "Error : Block Start Row/Col  Out of Bounds!\n" ;
        LOGAddErrorMsg(strErr) ;
        return -1;
    }
    else
    {
        return nNum * ( nBlockL - nOverlayL );
    }
}

/**
* @fn GetStartCol
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算影像按列（X）方向某一分块的起始列号
* @param nColNum 某块沿列方向的块号（从0开始）
* @return  影像按列（X）方向某一分块的起始列号
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
SINT CmlBlockCalculation::GetStartCol( const UINT nColNum ) const
{
    return CmlBlockCalculation::GetBlock( nColNum, this->nBlockW, this->nColCount, this->nOverlayW );
}

/**
* @fn GetStartRow
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算影像按行（Y）方向某一分块的起始行号
* @param nColNum 某块沿行方向的块号（从0开始）
* @return  影像按行（Y）方向某一分块的起始行号
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
SINT CmlBlockCalculation::GetStartRow( const UINT nRowNum ) const
{
    return CmlBlockCalculation::GetBlock( nRowNum, this->nBlockH, this->nRowCount, this->nOverlayH );
}
/**
* @fn GetBlockPos
* @date 2011.12.16
* @author 万文辉 whwan@irsa.ac.cn
* @brief 计算某一影像块的起始位置和大小
* @param nXIndex 影像块列方向索引
* @param nYIndex 影像块行方向索引
* @param ptOrig 影像块起始点位置
* @param nW  影像块宽
* @param nH  影像块高
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlBlockCalculation::GetBlockPos( UINT nXIndex, UINT nYIndex, Pt2i &ptOrig, UINT &nW, UINT &nH  )
{
    if( ( nXIndex >= nColCount )||( nYIndex >= nRowCount ) )
    {
        return false;
    }
    //计算块影像起始点坐标
    ptOrig.X = GetStartCol(nXIndex);
    ptOrig.Y = GetStartRow(nYIndex);

    if( nXIndex == GetColCount() - 1 )//列方向最后一块的情况
    {
        nW = GetLastBlockW();
    }
    else
    {
        nW = GetBlockW();
    }
    if( nYIndex == GetRowCount() - 1 )//行方向最后一块的情况
    {
        nH = GetLastBlockH();
    }
    else
    {
        nH = GetBlockH();
    }
    return true;
}
