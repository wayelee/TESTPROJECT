/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlRasterBlock.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 栅格数据块处理头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef CMLRASTERBLOCK_H
#define CMLRASTERBLOCK_H


/*************************
多波段的图像数据块，波段的组织以BSQ实现（一个波段接一个波段存储）(暂无实现)
**************************/
#include "mlBase.h"
#include"gdal_header.h"



class CmlGdalDataset;
/**
* @class CmlRasterBlock
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 程序中处理栅格时处理单元的类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlRasterBlock : protected CmlTemplateMat<BYTE>
{
public:
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
    CmlRasterBlock();
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

    ~CmlRasterBlock();
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
    CmlRasterBlock( const SINT nXOffSet, const SINT nYOffSet, const UINT nW, const UINT nH, const UINT nBytes, const UINT nZoom );

public:
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
    bool InitialImg( const UINT nH, const UINT nW, const UINT nBytes = 1 );
    /**
    * @fn ReSet
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 删除数据，不删除内存空间
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool ReSet();
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
    bool Clear();

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
    BYTE* GetPtrAt( const UINT nH, const UINT nW ) const;
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
    //其原理是取得数据的头指针（BYTE型），将需要设置的值转换成BYTE指针代入即可
    bool  SetPtrAt( const UINT nH, const UINT nW, BYTE* pVal );
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
    BYTE  GetAt( const UINT nRow, const UINT nCol );
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
    bool  SetAt( const UINT nRow, const UINT nCol, BYTE val );
    /**
    * @fn GetH
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 得到栅格block的高
    * @return 栅格block的高
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    UINT GetH() const
    {
        return m_nHeight;
    };
    /**
    * @fn GetW
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 得到栅格block的宽
    * @return 栅格block的宽
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    UINT GetW() const
    {
        return m_nWidth;
    };
    /**
    * @fn GetData
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 得到栅格block数据的头指针
    * @return 栅格block数据的头指针
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    BYTE* GetData() const
    {
        return m_pData;
    };
    /**
    * @fn GetTPixelSize
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 得到栅格block数据块内的所有像素值（点数量）
    * @return 点数量
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    SINT GetTPixelSize() const
    {
        return ( m_nTSize / m_nBytes );
    };
    /**
    * @fn IsValid
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 判断该数据块是否有值
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool IsValid() const
    {
        return (!m_bIsNULL);
    };

    bool SetXOffSet( SINT nXOffSet );//!<设置X偏移量
    bool SetYOffSet( SINT nXOffSet );//!<设置Y偏移量
    bool SetZoom( UINT nZoom );

    SINT GetXOffSet()const//!<得到X偏移量
    {
        return m_nXOffSet;
    };
    SINT GetYOffSet()const//!<得到Y偏移量
    {
        return m_nYOffSet;
    };
    UINT GetZoom() const//!<得到缩放系数
    {
        return m_nZoom;
    };
    UINT GetBytes() const//!<得到比特数
    {
        return m_nBytes;
    };
    /**
    * @fn SetGDTType
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 设置数据块内数据类型
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool SetGDTType( GDALDataType gType )
    {
        return (m_GDTType = gType);
    };
    /**
    * @fn SetGDTType
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 得到数据块内数据类型
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    GDALDataType GetGDTType()const
    {
        return m_GDTType;
    };
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
    bool GetDoubleVal  ( const UINT nH, const UINT nW, double &dVal )const;
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
    bool SetDoubleVal ( const UINT nH, const UINT nW, double dVal );
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
    bool GetGeoXYZ  ( const UINT nH, const UINT nW, Pt3d& ptXYZ ) const;

    bool GetGeoXYZ  ( const UINT nH, const UINT nW, Pt2d ptOrig, const DOUBLE dXRes, const DOUBLE dYRes, const DOUBLE dNoVal, Pt3d& ptXYZ ) const;
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
    bool GetGeoZ  ( const DOUBLE dX, const DOUBLE dY, DOUBLE &dZ ) const;

    bool GetGeoZ  ( const DOUBLE dX, const DOUBLE dY, Pt2d ptOrig, const DOUBLE dXRes, const DOUBLE dYRes, const DOUBLE dNoVal, DOUBLE &dZ ) const;

    bool FreeData();

    bool GetSubRasterBlock( UINT nX, UINT nY, UINT nXSize, UINT nYSize, CmlRasterBlock &clsSubBlocks );

    bool GetCentRasterBlock( CmlRasterBlock clsBlocks, UINT &nX, UINT &nY, CmlRasterBlock &clsSubBlock );
    CmlGdalDataset *m_pGdalData;//!<数据头指针
private:

    UINT m_nWidth;//!<数据块宽
    UINT m_nHeight;//!<数据块高
    SINT m_nXOffSet;//!<数据块X偏移量
    SINT m_nYOffSet;//!<数据块Y偏移量
    UINT m_nZoom;//!<数据块缩放系数
    UINT m_nBytes;//!<数据块比特数
    GDALDataType m_GDTType;//!<数据块类型

};

typedef struct stuGeoRasterBlock
{
    CmlRasterBlock* pImgBlockData;
    DOUBLE dXRes;
    DOUBLE dYRes;
    DOUBLE dNoVal;
    Pt2d ptOrig;
	stuGeoRasterBlock()
	{
		pImgBlockData = NULL;
		dXRes = dYRes = dNoVal = 0;
	}
}GeoRasBlock;
#endif // CMLRASTERBLOCK_H
