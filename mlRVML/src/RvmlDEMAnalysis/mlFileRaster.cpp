/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlFileRaster.cpp
* @date 2011.11.02
* @author 刘召芹 liuzq@irsa.ac.cn
* @brief 影像文件读写源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/

#include "mlFileRaster.h"

/**
* @fn CmlFileRaster
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlFileRaster类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlFileRaster::CmlFileRaster()
{
    //ctor
}
/**
* @fn ~CmlFileRaster
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlFileRaster类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlFileRaster::~CmlFileRaster()
{
    //dtor
}
/**
* @fn SaveBlockToFile
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 将栅格影像块存入文件
* @param nBands 栅格影像波段号
* @param FileName 文件名称
* @param nXOffSet 栅格影像X方向偏移量
* @param nYOffSet 栅格影像Y方向偏移量
* @param pImgBlock 栅格影像块
* @param nBlockBand 目标文件波段号
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFileRaster::SaveBlockToFile(SINT nBand, SINT nXOffSet, SINT nYOffSet, CmlRasterBlock* pImgBlock, SINT nBlockBand)
{
    return true;
}
/**
* @fn CreateFile
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 创建栅格影像文件
* @param FileName 文件名称
* @param nW 栅格影像宽
* @param nH 栅格影像高
* @param nBands 栅格影像波段数
* @param nBytes 栅格影像比特数
* @param BitsOut 生成图像类型
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFileRaster::CreateFile(SCHAR *FileName, SINT nW,SINT nH,SINT nBands,SINT nBytes,SCHAR *BitsOut)
{
    return true;
}

/**
* @fn LoadFile
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 加载栅格影像文件
* @param FileName 文件名称
* @param nType 影像原点位置标识
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFileRaster::LoadFile(SCHAR *FileName, SINT nType)
{
    return true;
}

///** @brief GetHight
//  *
//  * @todo: document this function
//  */
//SINT CmlFileRaster::GetHight() const
//{
//
//}
//
///** @brief GetWidth
//  *
//  * @todo: document this function
//  */
//SINT CmlFileRaster::GetWidth() const
//{
//
//}
//
///** @brief GetBytes
//  *
//  * @todo: document this function
//  */
//SINT CmlFileRaster::GetBytes() const
//{
//
//}
//
///** @brief GetBands
//  *
//  * @todo: document this function
//  */
//SINT CmlFileRaster::GetBands() const
//{
//
//}
//
