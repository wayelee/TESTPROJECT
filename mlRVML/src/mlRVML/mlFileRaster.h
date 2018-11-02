/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlFileRaster.h
* @date 2011.11.02
* @author 刘召芹 liuzq@irsa.ac.cn
* @brief 影像文件读写头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef CFILEIMAGE_H
#define CFILEIMAGE_H

#include"gdal_header.h"
#include "mlBase.h"
#include "mlRasterBlock.h"

/**
* @class CmlFileRaster
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 栅格抽象类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlFileRaster
{
public:
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
    CmlFileRaster();
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
    virtual ~CmlFileRaster();

public:
    virtual UINT GetBands() const = 0;//!<得到栅格影像波段数
    virtual UINT GetBytes() const = 0;//!<得到栅格影像比特数
    virtual UINT GetWidth() const = 0;//!<得到栅格影像宽
    virtual UINT GetHeight() const = 0;//!<得到栅格影像高

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
    virtual bool LoadFile(SCHAR *FileName, SINT nType=0);//nType 0--影像原点在左上角 1--影像原点在左下角
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
    virtual bool CreateFile(SCHAR *FileName, SINT nW,SINT nH,SINT nBands,SINT nBytes,SCHAR *BitsOut);
    /**
    * @fn SaveBlockToFile
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 将栅格影像块存入文件
    * @param nBand 栅格影像波段号
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
    virtual bool SaveBlockToFile( SINT nBand, SINT nXOffSet, SINT nYOffSet, CmlRasterBlock* pImgBlock, SINT nBlockBand);

    string m_strFileName;//!<文件名
protected:
private:
};


#endif // CFILEIMAGE_H
