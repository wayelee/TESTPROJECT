/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlBlockCalculation.h
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 影像分块头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef _MLBLOCKCALCULATION_H
#define _MLBLOCKCALCULATION_H
#include "mlBase.h"

/**
* @class CmlBlockCalculation
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 影像分块处理类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlBlockCalculation
{
public :
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
    CmlBlockCalculation();
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
    CmlBlockCalculation( const UINT nColThreshold, const UINT nRowThresold, const UINT nBlockOverlayW, const UINT nBlockOverlayH );
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
    ~CmlBlockCalculation();
private:
    UINT nBlockW;//!<影像块的宽
    UINT nBlockH;//!<影像块的高
    UINT nColCount;//!<影像列方向的分块数
    UINT nRowCount;//!<影像行方向的分块数
    UINT nColThres;//!<影像列方向分块大小阈值
    UINT nRowThres;//!<影像行方向分块大小阈值
    UINT nOverlayW;//!<影像列方向重叠区大小
    UINT nOverlayH;//!<影像行方向重叠区大小
    UINT nLastBlockW;//!<影像列方向最后一块影像块大小
    UINT nLastBlockH;//!<影像行方向最后一块影像块大小
public:
    UINT GetBlockW() const;//!<得到影像块的宽
    UINT GetBlockH() const;//!<得到影像块的高
    UINT GetLastBlockW() const;//!<得到影像分块最后一块的宽
    UINT GetLastBlockH() const;//!<得到影像分块最后一块的高
    UINT GetColCount() const;//!<得到影像列方向（X方向）的分块数
    UINT GetRowCount() const;//!<得到影像行方向（Y方向）的分块数
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
    bool CalBlockCol( const UINT nImgWidth, const UINT nBlockWidth );
    /**
    * @fn CalBlockRow
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 计算影像沿行（Y）方向分块的大小的块数
    * @param nImgHeight 影像高
    * @param nBlockHeight 影像初始分块高
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool CalBlockRow( const UINT nImgHeight, const UINT nBlockHeight );
    /**
    * @fn GetStartCol
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 计算影像按列（X）方向某一分块的起始列号
    * @param nColNum 某块沿列方向的块号（从0开始）
    * @return 影像按列（X）方向某一分块的起始列号
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    SINT GetStartCol( const UINT nColNum ) const;
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
    SINT GetStartRow( const UINT nRowNum ) const;

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
    bool GetBlockPos( UINT nXIndex, UINT nYIndex, Pt2i &ptOrig, UINT &nW, UINT &nH );
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
    bool  CalBlock( const UINT nImgLength, const UINT nBlockLength, const UINT nThres, const UINT nOverlay, UINT &nBlockL, UINT &nCount, UINT &nLastBlockL );
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
    SINT  GetBlock( const UINT nNum, const UINT nBlockL, const UINT nCount, const UINT nOverlayL ) const ;
private:
};

#endif
