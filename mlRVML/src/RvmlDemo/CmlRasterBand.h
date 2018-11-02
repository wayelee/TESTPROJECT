/************************************************************
  Copyright (C), 2011-2012, PMRS Lab, IRSA, CAS
  文件名称: CmlRasterBand.h
  创建日期: 2011.11.6
  作    者: 李巍
  描    述: CmlRasterband类与CmlRasterDataBuffer类的声明
  版本编号：1.0
  修改历史:   <作者>   <时间>   <版本编号>   <描述>

***********************************************************/
#ifndef RASTERBAND_H
#define RASTERBAND_H
#include"gdal_header.h"
#include"CmlRasterDataset.h"

// CmlRasterBand类，以GDALRasterBand为基础，简单封装读写数据的功能
class CmlRasterBand
{
public:
    /*************************************************
      函数名称:    CmlRasterBand
      作    者:   李巍
      功能描述：   CmlRasterBand的构造函数
      输    入：  GDALRasterBand对象
      输    出：  无
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    CmlRasterBand(GDALRasterBand * pband);

    // raster波段像素值的大小，以字节为单位
    int m_nPixelDataSize;

    // GDAL数据类型结构
    GDALDataType m_DataType;
public:
    /*************************************************
      函数名称:    GetRasterDataType
      作    者:   李巍
      功能描述：   返回m_pBand的data类型
      输    入：  无
      输    出：  GDALDataType类型
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    GDALDataType GetRasterDataType();

    /*************************************************
      函数名称:    GetPixelValue
      作    者:   李巍
      功能描述：   在波段上读取指定位置的像素值
      输    入：  x： 行号，
                 y： 列号；   波段左上角位置为（0，0）
      输    出：  返回值： 负数表示执行失败，否则成功，
                 Data：读取到的数据
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    int GetPixelValue(int x, int y, double &Data);

    /*************************************************
      函数名称:    SetPixelValue
      作    者:   李巍
      功能描述：   在波段上写入指定位置的像素值
      输    入：  x： 行号，
                 y： 列号；波段左上角位置为（0，0），
                 Data：写入的数据
      输    出：  返回值： 负数表示执行失败，否则成功
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    int SetPixelValue(int x, int y, double Data);

    /*************************************************
      函数名称:    ReadRasterBlock
      作    者:   李巍
      功能描述：   在波段上读取指定位置指定大小的像素值数组
      输    入：  xLocation： 行号，
                 yLocation： 列号； 波段左上角位置为（0，0），
                 xSize：x方向大小，
                 ySize： y方向大小，
                 pData：存放数据的内存地址
      输    出：  返回值： 负数表示执行失败，否则成功
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    int ReadRasterBlock(int xLocation,int yLocation,int xSize,int ySize,void* pData);

    /*************************************************
      函数名称:    WriteRasterBlock
      作    者:   李巍
      功能描述：   在波段上写入指定位置指定大小的像素值数组
      输    入：  xLocation： 行号，
                 yLocation： 列号；波段左上角位置为（0，0），
                 xSize：x方向大小，
                 ySize： y方向大小，
                 pData：写入数据的内存地址
      输    出：  返回值： 负数表示执行失败，否则成功
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    int WriteRasterBlock(int xLocation,int yLocation,int xSize,int ySize,void* pData);

    /*************************************************
      函数名称:    GetDataset
      作    者:   李巍
      功能描述：   返回m_pBand的GDALDataset对象
      输    入：  无
      输    出：  返回m_pBand的GDALDataset对象
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    GDALDataset* GetDataset();

    /*************************************************
      函数名称:    GetDataset
      作    者:   李巍
      功能描述：   返回m_pBand的GDALDataset对象
      输    入：  无
      输    出：  返回m_pBand的GDALDataset对象
      版本编号：   1.0
      修改历史：   <作者>   <时间>   <版本号>   <描述>
      *************************************************/
    GDALRasterBand* GetGDALRasterBand();

private:
    // GDAL波段类
    GDALRasterBand *m_pBand;

};

template  <typename T>
class CmlRasterDataBuffer
{
public:
    CmlRasterDataBuffer(void * pdata ,long Row = 0, long Col = 0);

    CmlRasterDataBuffer();
    ~CmlRasterDataBuffer();
    long m_row;
    long m_col;

public:
    T* Buffer;
};
template  <typename T> CmlRasterDataBuffer<T>::CmlRasterDataBuffer( )
{
    Buffer = NULL;
}
template  <typename T> CmlRasterDataBuffer<T>::CmlRasterDataBuffer(void *pdata,long Row,long Col)
{
    Buffer = (T*)pdata;
    m_row = Row;
    m_col = Col;
}

template  <typename T>CmlRasterDataBuffer<T>::~CmlRasterDataBuffer()
{

}

#endif // RASTERBAND_H
