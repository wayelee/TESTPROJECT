/************************************************************
  Copyright (C), 2011-2012, PMRS Lab, IRSA, CAS
  文件名称: CmlRasterDataset.cpp
  创建日期: 2011.11.6
  作    者: 李巍
  描    述: CmlRasterDataset类的实现文件
  版本编号：1.0
  修改历史:   <作者>   <时间>   <版本编号>   <描述>

***********************************************************/
#include "CmlRasterDataset.h"
#include "CmlRasterBand.h"

/*
  RasterDataset class implement code
*/
CmlRasterDataset::CmlRasterDataset(char* FilePath)
{
    GDALAllRegister();
    m_NeedReleaseDataset = true;
    m_pDataset = (GDALDataset*)GDALOpen(FilePath,GA_Update);
}

CmlRasterDataset::CmlRasterDataset(GDALDataset *pdataset)
{
    m_NeedReleaseDataset = false;
    m_pDataset = pdataset;
}
CmlRasterDataset::~CmlRasterDataset()
{
    if(m_pDataset !=NULL && m_NeedReleaseDataset)
    {
        GDALClose(m_pDataset);
    }
}

int CmlRasterDataset::GetRasterWidth()
{
    try
    {
        if(m_pDataset != NULL)
            return m_pDataset->GetRasterXSize();
        else
            return -1;
    }
    catch(...)
    {
        return -1;
    }

}

int CmlRasterDataset:: GetRasterHeight()
{
    try
    {
        if(m_pDataset !=NULL)
            return m_pDataset->GetRasterYSize();
        else
            return -1;
    }
    catch(...)
    {
        return -1;
    }

}

GDALRasterBand*  CmlRasterDataset::GetRasterBand(int index)
{
    try
    {
        GDALRasterBand *band = m_pDataset->GetRasterBand(index);
        // CmlRasterBand* rband = new CmlRasterBand(band);
        return band;
    }
    catch(...)
    {
        return NULL;
    }

}

bool CmlRasterDataset::SaveAsFunc(char* FilePath,char* FileType)
{
    try
    {
        GDALDriver *poDriver =NULL;
        poDriver = GetGDALDriverManager()->GetDriverByName(FileType);
        if(poDriver == NULL)
            return false;
        GDALDataset *poDestDataset;
        poDestDataset = poDriver->CreateCopy(FilePath,m_pDataset,true,0,0,0);
        if(poDestDataset == NULL)
            return false;
        poDestDataset->FlushCache();
        delete poDriver;
        delete poDestDataset;
        //  CmlRasterDataset* rdataset = new CmlRasterDataset(poDestDataset);
        //  return rdataset;
        return true;
    }
    catch(...)
    {
        return false;
    }
}

bool CmlRasterDataset::CreateNewRasterDataset(char* FilePath,char* FileType, GDALDataType DataType,int BandCount,int xSize,int ySize)
{
    try
    {
        GDALDriver *poDriver =NULL;
        poDriver = GetGDALDriverManager()->GetDriverByName(FileType);
        if(poDriver == NULL)
            return false;
        GDALDataset* poDestDataset;
        poDestDataset = poDriver->Create(FilePath,xSize,ySize,BandCount,DataType,0);
        if(poDestDataset == NULL)
            return false;
        poDestDataset->FlushCache();
        //CmlRasterDataset * rdataset = new CmlRasterDataset(poDestDataset);
        delete poDriver;
        delete poDestDataset;
        return true;
    }
    catch(...)
    {
        return false;
    }
}
GDALDataset* CmlRasterDataset::GetDataset()
{
    return m_pDataset;
}
