/************************************************************
  Copyright (C), 2011-2012, PMRS Lab, IRSA, CAS
  文件名称: CmlRasterDataset.h
  创建日期: 2011.11.6
  作    者: 李巍
  描    述: CmlRasterDataset类的头文件
  版本编号：1.0
  修改历史:   <作者>   <时间>   <版本编号>   <描述>

***********************************************************/
#ifndef RASTERDATASET_H
#define RASTERDATASET_H
#include"gdal_header.h"

class CmlRasterBand ;

// CmlRasterDataset类，以GDALDataset为基础，简单封装存储和新建图片的功能
class CmlRasterDataset
{
public:
    /*************************************************
      函数名称:    CmlRasterDataset
      作    者:   李巍
      功能描述：   CmlRasterDataset的构造函数
      输    入：  FilePath：栅格文件路径
      输    出：  无
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    CmlRasterDataset(char* FilePath);

    /*************************************************
      函数名称:    CmlRasterDataset
      作    者:   李巍
      功能描述：   CmlRasterDataset的构造函数
      输    入：  pdataset：GDALDataset对象
      输    出：  无
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    CmlRasterDataset( GDALDataset* pdataset);

    /*************************************************
      函数名称:    ~CmlRasterDataset
      作    者:   李巍
      功能描述：   CmlRasterDataset的析构函数，用字符串构造函数生成的对象析构时会delete掉m_pDataset，
                 用GDALDataset构造函数生成对象不会delete掉m_pDataset，需要使用者在自行delete
      输    入：  无
      输    出：  无
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    virtual ~CmlRasterDataset();

public:
    /*************************************************
      函数名称:    GetRasterWidth
      作    者:   李巍
      功能描述：   获得m_pDataset栅格数据的宽度
      输    入：  无
      输    出：  返回值：m_pDataset栅格数据的宽度，负数表示获取失败
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    int GetRasterWidth();

    /*************************************************
      函数名称:    GetRasterHeight
      作    者:   李巍
      功能描述：   获得m_pDataset栅格数据的高度
      输    入：  无
      输    出：  返回值：m_pDataset栅格数据的高度，负数表示获取失败
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    int GetRasterHeight();

    /*************************************************
      函数名称:    GetRasterBand
      作    者:   李巍
      功能描述：   获取m_pDataset
      输    入：  index： 需要获取的GDALRasterBand的索引，起始索引从1开始
      输    出：  返回值：指定索引的GDALRasterBand的指针
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    GDALRasterBand* GetRasterBand(int index);

    /*************************************************
      函数名称:    SaveAsFunc
      作    者:   李巍
      功能描述：   将CmlRasterDataset存储到硬盘
      输    入：  FilePath： 存储文件路径的完整路径，
                 FileType： 指定的文件格式，可支持格式暂时包括“bmp”,"tif","jpg"
      输    出：  返回值：返回true表示成功，false表示失败
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    bool SaveAsFunc(char* FilePath,char* FileType);

    /*************************************************
      函数名称:    CreateNewRasterDataset
      作    者:   李巍
      功能描述：   在指定路径新建栅格数据
      输    入：  FilePath： 存储文件路径的完整路径，
                 FileType： 指定的文件格式，可支持格式暂时包括“bmp”,"tif","jpg"
                 DataType： 栅格数据的数据类型
                 BandCount： 新建数据的波段数
                 xSize： 新建数据的宽度
                 ySize： 新建数据的高度
      输    出：  返回值：返回true表示成功，false表示失败
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    bool CreateNewRasterDataset(char* FilePath,char* FileType, GDALDataType DataType,int BandCount,int xSize,int ySize);

    /*************************************************
      函数名称:    GetDataset
      作    者:   李巍
      功能描述：   返回m_pDataset
      输    入：  无
      输    出：  返回值： m_pDataset的指针
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    GDALDataset* GetDataset();
private:
    //
    GDALDataset * m_pDataset;
    bool m_NeedReleaseDataset;
};



#endif // RASTERIOCLASS_H
