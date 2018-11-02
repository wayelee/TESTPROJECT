/************************************************************
  Copyright (C), 2011-2012, PMRS Lab, IRSA, CAS
  文件名称: CmlRasterband.cpp
  创建日期: 2011.11.6
  作    者: 李巍
  描    述: CmlRasterband类与CmlRasterDataBuffer类的实现
  版本编号：1.0
  修改历史:   <作者>   <时间>   <版本编号>   <描述>

***********************************************************/
#include "CmlRasterBand.h"



/*
   RasterBand class implement code
*/
/*************************************************
  函数名称:    CmlRasterBand
  作    者:   李巍
  功能描述：   CmlRasterBand的构造函数
  输    入：  GDALRasterBand对象
  输    出：  无
  版本编号：   1.0
  修改历史：   <作者>   <时间>   <版本号>   <描述>
  *************************************************/
CmlRasterBand::CmlRasterBand(GDALRasterBand* pband)
{
    m_pBand = pband;
    m_DataType = this->GetRasterDataType();
    m_nPixelDataSize = 0;
    switch (m_DataType)
    {
    case GDT_Unknown:
        m_nPixelDataSize = 0;
        break;
    case GDT_Byte:
        m_nPixelDataSize = 1;
        break;
    case GDT_UInt16:
        m_nPixelDataSize = 2;
        break;
    case GDT_Int16:
        m_nPixelDataSize = 2;
        break;
    case GDT_UInt32:
        m_nPixelDataSize = 4;
        break;
    case GDT_Int32:
        m_nPixelDataSize = 4;
        break;
    case GDT_Float32:
        m_nPixelDataSize = 4;
        break;
    case GDT_Float64:
        m_nPixelDataSize = 8;
        break;
    case GDT_CInt16:
        m_nPixelDataSize = 16;
        break;
    case GDT_CInt32:
        m_nPixelDataSize = 32;
        break;
    case GDT_CFloat32:
        m_nPixelDataSize = 32;
        break;
    case GDT_CFloat64:
        m_nPixelDataSize = 64;
        break;
    default:
        m_nPixelDataSize = 0;
        break;
    }
}

/*************************************************
  函数名称:    GetRasterDataType
  作    者:   李巍
  功能描述：   返回m_pBand的data类型
  输    入：  无
  输    出：  GDALDataType类型
  版本编号：   1.0
  修改历史：   <作者>   <时间>   <版本号>   <描述>
  *************************************************/
GDALDataType CmlRasterBand::GetRasterDataType()
{
    if(m_pBand != NULL)
        return m_pBand->GetRasterDataType();
    else
        return GDT_Unknown;
}

/*************************************************
  函数名称:    GetDataset
  作    者:   李巍
  功能描述：   返回m_pBand的GDALDataset对象
  输    入：  无
  输    出：  返回m_pBand的GDALDataset对象
  版本编号：   1.0
  修改历史：   <作者>   <时间>   <版本号>   <描述>
  *************************************************/
GDALDataset* CmlRasterBand::GetDataset()
{
    if(m_pBand == NULL)
    {
        return NULL;
    }
    try
    {
        GDALDataset* pDataset = m_pBand->GetDataset();
        return pDataset;
    }
    catch(...)
    {
        return NULL;
    }
}

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
int CmlRasterBand::GetPixelValue(int xLocation, int yLocation, double &Data)
{
    try
    {
        char *pData= new char[1*m_nPixelDataSize];
        CPLErr err ;
        err = m_pBand->RasterIO(GF_Read,xLocation,yLocation,1,1, &pData,1,1,m_pBand->GetRasterDataType(),0,0);
        switch (m_DataType)
        {
        case GDT_Unknown:
        {
            return -1;
            break;
        }
        case GDT_Byte:
        {
            CmlRasterDataBuffer<char> bf(pData);
            // bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_UInt16:
        {
            CmlRasterDataBuffer<unsigned short> bf(pData);
            //  bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_Int16:
        {
            CmlRasterDataBuffer<short> bf(pData);
            //  bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_UInt32:
        {
            CmlRasterDataBuffer<int32_t> bf(pData);
            //    bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_Int32:
        {
            CmlRasterDataBuffer<unsigned int> bf(pData);
            //    bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_Float32:
        {
            CmlRasterDataBuffer<float> bf(pData);
            //  bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_Float64:
        {
            CmlRasterDataBuffer<double> bf(pData);
            //   bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_CInt16:
        {
            CmlRasterDataBuffer<int16_t> bf(pData);
            //    bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_CInt32:
        {
            CmlRasterDataBuffer<int32_t> bf(pData);
            //    bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_CFloat32:
        {
            CmlRasterDataBuffer<float> bf(pData);
            //   bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        case GDT_CFloat64:
        {
            CmlRasterDataBuffer<double> bf(pData);
            //   bf.Buffer = pData;
            Data = bf.Buffer[1];
            break;
        }
        default:
            m_nPixelDataSize = 0;
            break;
        }
        delete []pData;
        if(err == CE_Failure)
            return -1;
        else
            return err;

    }
    catch(...)
    {
        return -1;
    }
}

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
int CmlRasterBand::SetPixelValue(int xLocation, int yLocation, double Data)
{
    try
    {
        void *pData= new char[1*m_nPixelDataSize];
        switch (m_DataType)
        {
        case GDT_Unknown:
        {
            return -1;
            break;
        }
        case GDT_Byte:
        {
            CmlRasterDataBuffer<char> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_UInt16:
        {
            CmlRasterDataBuffer<unsigned short> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_Int16:
        {
            CmlRasterDataBuffer<short> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_UInt32:
        {
            CmlRasterDataBuffer<int32_t> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_Int32:
        {
            CmlRasterDataBuffer<unsigned int> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_Float32:
        {
            CmlRasterDataBuffer<float> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_Float64:
        {
            CmlRasterDataBuffer<double> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_CInt16:
        {
            CmlRasterDataBuffer<int16_t> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_CInt32:
        {
            CmlRasterDataBuffer<int32_t> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_CFloat32:
        {
            CmlRasterDataBuffer<float> bf(pData);
            bf.Buffer[1] = Data ;
            break;
        }
        case GDT_CFloat64:
        {
            CmlRasterDataBuffer<double> bf(pData);
            bf.Buffer[1] = Data  ;
            break;
        }
        default:
        {
            return -1;
            break;
        }
        }

        CPLErr err ;
        err = m_pBand->RasterIO(GF_Write,xLocation,yLocation,1,1,pData,1,1,m_pBand->GetRasterDataType(),0,0);
        m_pBand->FlushCache();
        if(err == CE_Failure)
            return -1;
        else
            return err;
    }
    catch(...)
    {
        return -1;
    }

}

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
int CmlRasterBand::ReadRasterBlock(int xLocation,int yLocation,int xSize,int ySize,void* pData)
{
    try
    {
        CPLErr err ;
        err = m_pBand->RasterIO(GF_Read,xLocation,yLocation,xSize,ySize,pData,xSize,ySize,m_pBand->GetRasterDataType(),0,0);

        if(err == CE_Failure)
            return -1;
        else
            return err;
    }
    catch(...)
    {
        return -1;
    }
}

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
int CmlRasterBand:: WriteRasterBlock(int xLocation,int yLocation,int xSize,int ySize,void* pData)
{
    try
    {
        CPLErr err ;
        err = m_pBand->RasterIO(GF_Write,xLocation,yLocation,xSize,ySize,pData,xSize,ySize,m_pBand->GetRasterDataType(),0,0);
        if(err == CE_Failure)
            return -1;
        else
            return err;
    }
    catch(...)
    {
        return -1;
    }
}

/*************************************************
  函数名称:    GetDataset
  作    者:   李巍
  功能描述：   返回m_pBand的GDALDataset对象
  输    入：  无
  输    出：  返回m_pBand的GDALDataset对象
  版本编号：   1.0
  修改历史：   <作者>   <时间>   <版本号>   <描述>
  *************************************************/
GDALRasterBand* CmlRasterBand::GetGDALRasterBand()
{
    return m_pBand;
}
