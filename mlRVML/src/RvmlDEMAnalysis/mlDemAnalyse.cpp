/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlDemAnalyse.cpp
* @date 2011.11.18
* @author 李巍 liwei@irsa.ac.cn
* @brief  地形分析模块类源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/

#include "mlDemAnalyse.h"
using namespace std;
#define DEMANALYSENODATA -99999
#define DOUBLETOLERANCE 0.00001

CPL_CVSID("$Id: gdal_contour.cpp 21191 2010-12-03 20:02:34Z rouault $");
/**
* @fn CmlDemAnalyse
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief CmlDemAnalyse类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlDemAnalyse::CmlDemAnalyse()
{
    //ctor
}
/*
CmlDemAnalyse::CmlDemAnalyse(CmlGdalDataset * pSrcDataset)
 {
     m_pSrcDataset = pSrcDataset;
 }

 CmlDemAnalyse::CmlDemAnalyse(CmlRasterBlock * pSrcRasterBlock)
 {
     m_pSrcRasterBlock = pSrcRasterBlock;
 }
 */
/**
* @fn ~CmlDemAnalyse
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief CmlDemAnalyse类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlDemAnalyse::~CmlDemAnalyse()
{
    //dtor
}

/**
* @fn ComputeNorthViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算北方向上的通视域
* @param nxlocation 视点x坐标
* @param nyLocation 视点y坐标
* @param dViewHight  视点距离地面的坐标
* @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeNorthViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE  dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation;
    SINT nRY= nYLocation - 1;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRY >= 0)
    {
        //当邻近视点的时候,必定可见
        if(nRY == nYLocation -1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX;
            pt1.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue + (nRY - nYLocation)* (pt1.dz - dViewPointRealValue)/(pt1.ly - nYLocation) ;
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
        {
            dActualZ = DEMANALYSENODATA;
        }
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //cout<< nRX<<", "<<nRY<< ", " <<fVisibleHeight<< ", "<<dActualZ <<", "<< (SINT)IsVisible<<endl;
        nRY --;
    }
    return 1;

}

/**
* @fn ComputeSouthViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算南方向上的通视域
* @param nxlocation 视点x坐标
* @param nyLocation 视点y坐标
* @param dViewHight  视点距离地面的坐标
* @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeSouthViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE  dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation;
    SINT nRY= nYLocation + 1;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRY < nHeight)
    {
        //当邻近视点的时候,必定可见
        if(nRY == nYLocation + 1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX;
            pt1.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue + (nRY - nYLocation)* (pt1.dz - dViewPointRealValue)/(pt1.ly - nYLocation) ;
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
        {
            dActualZ = DEMANALYSENODATA;
        }
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //cout<< nRX<<", "<<nRY<< ", " <<fVisibleHeight<< ", "<<dActualZ <<", "<< (SINT)IsVisible<<endl;
        nRY ++;
    }
    return 1;
}

/**
* @fn ComputeEastViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算东方向上的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeEastViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation + 1;
    SINT nRY= nYLocation;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRX < nWidth)
    {
        //当邻近视点的时候,必定可见
        if(nRX == nXLocation + 1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX - 1;
            pt1.ly = nRY;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue + (nRX - nXLocation)* (pt1.dz - dViewPointRealValue)/(pt1.lx - nXLocation) ;
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
        {
            dActualZ = DEMANALYSENODATA;
        }
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        // cout<< nRX<<", "<<nRY<< ", " <<fVisibleHeight<< ", "<<dActualZ <<", "<< (SINT)IsVisible<<endl;
        nRX ++;
    }
    return 1;
}

/**
* @fn ComputeWestViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算西方向上的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeWestViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation - 1;
    SINT nRY= nYLocation;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRX >= 0)
    {
        //当邻近视点的时候,必定可见
        if(nRX == nXLocation - 1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX + 1;
            pt1.ly = nRY;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue + (nRX - nXLocation)* (pt1.dz - dViewPointRealValue)/(pt1.lx - nXLocation) ;
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
        {
            dActualZ = DEMANALYSENODATA;
        }
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        // cout<< nRX<<", "<<nRY<< ", " <<fVisibleHeight<< ", "<<dActualZ <<", "<< (SINT)IsVisible<<endl;
        nRX --;
    }
    return 1;
}

/**
* @fn ComputeNorthEastViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算东北方向上的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeNorthEastViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation + 1;
    SINT nRY= nYLocation - 1;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRX < nWidth && nRY >=0)
    {
        //当邻近视点的时候,必定可见
        if(nRX == nXLocation + 1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX - 1;
            pt1.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue + (nRX - nXLocation)* (pt1.dz - dViewPointRealValue)/(pt1.lx - nXLocation) ;
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
        {
            dActualZ = DEMANALYSENODATA;
        }
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        nRX ++;
        nRY --;
    }
    return 1;
}

/**
* @fn ComputeSouthEastViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算东南方向上的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeSouthEastViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    SINT nWidth = m_pSrcRasterBlock->GetW();
    SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation + 1;
    SINT nRY= nYLocation + 1;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRX < nWidth && nRY < nHeight)
    {
        //当邻近视点的时候,必定可见
        if(nRX == nXLocation + 1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX - 1;
            pt1.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue + (nRX - nXLocation)* (pt1.dz - dViewPointRealValue)/(pt1.lx - nXLocation) ;
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
        {
            dActualZ = DEMANALYSENODATA;
        }
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        nRX ++;
        nRY ++;
    }
    return 1;
}

/**
* @fn ComputeNorthWestViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算西北方向上的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeNorthWestViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation - 1;
    SINT nRY= nYLocation - 1;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRX >= 0 && nRY >= 0)
    {
        //当邻近视点的时候,必定可见
        if(nRX == nXLocation - 1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX + 1;
            pt1.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue + (nRX - nXLocation)* (pt1.dz - dViewPointRealValue)/(pt1.lx - nXLocation) ;
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
        {
            dActualZ = DEMANALYSENODATA;
        }
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        nRX --;
        nRY --;
    }
    return 1;
}

/**
* @fn ComputeSouthWestViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算西南方向上的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeSouthWestViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation - 1;
    SINT nRY= nYLocation + 1;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRX >= 0 && nRY < nHeight)
    {
        //当邻近视点的时候,必定可见
        if(nRX == nXLocation - 1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX + 1;
            pt1.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue + (nRX - nXLocation)* (pt1.dz - dViewPointRealValue)/(pt1.lx - nXLocation) ;
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
        {
            dActualZ = DEMANALYSENODATA;
        }
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        nRX --;
        nRY ++;
    }
    return 1;
}

/**
* @fn ComputeEightDirectionViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算八个方向上的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeEightDirectionViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    ComputeNorthViewShed(nXLocation, nYLocation, dViewHight , pBlockOut, pVisibleHeightBloc);
    ComputeSouthViewShed(nXLocation, nYLocation, dViewHight , pBlockOut, pVisibleHeightBloc);
    ComputeEastViewShed(nXLocation, nYLocation, dViewHight , pBlockOut, pVisibleHeightBloc);
    ComputeWestViewShed(nXLocation, nYLocation, dViewHight , pBlockOut, pVisibleHeightBloc);
    ComputeNorthEastViewShed(nXLocation, nYLocation, dViewHight , pBlockOut, pVisibleHeightBloc);
    ComputeNorthWestViewShed(nXLocation, nYLocation, dViewHight , pBlockOut, pVisibleHeightBloc);
    ComputeSouthEastViewShed(nXLocation, nYLocation, dViewHight , pBlockOut, pVisibleHeightBloc);
    ComputeSouthWestViewShed(nXLocation, nYLocation, dViewHight , pBlockOut, pVisibleHeightBloc);
    return 1;
}

/**
* @fn Compute1stQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第一象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::Compute1stQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation + 2;
    SINT nRY= nYLocation -1;
    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;
    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;
    while(nRX < nWidth)
    {
        while( nYLocation - nRY <  nRX - nXLocation && nRY >= 0)
        {
            // 用于跟视点计算参考面的两个参考点
            POINTRASTER pt1,pt2;
            pt1.lx = nRX - 1;
            pt1.ly = nRY;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            pt2.lx = nRX - 1;
            pt2.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt2.ly,pt2.lx,pt2.dz);

            DOUBLE z;
            ComputeZValueInReferencePlan(pt1.lx,pt1.ly,pt1.dz,
                                         pt2.lx,pt2.ly,pt2.dz,
                                         nXLocation,nYLocation,dViewPointRealValue,
                                         nRX,nRY, z);
            fVisibleHeight = z;
            //计算点实际高度
            DOUBLE dActualZ ;
            m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
            if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
            {
                dActualZ = DEMANALYSENODATA;
            }
            //如果计算点实际高度大于可见最小高度
            if(dActualZ > fVisibleHeight)
            {
                IsVisible = 255;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                fVisibleHeight = dActualZ;
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //如果计算点高度小于可见高度
            else
            {
                IsVisible = 0;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            nRY --;
        }
        nRY = nYLocation -1;
        nRX++;
    }
    return 1;
}

/**
* @fn Compute2ndQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第二象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::Compute2ndQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation + 1;
    SINT nRY= nYLocation - 2;
    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;
    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;
    while(nRY >= 0)
    {
        while( nYLocation - nRY >  nRX - nXLocation && nRX < nWidth)
        {
            // 用于跟视点计算参考面的两个参考点
            POINTRASTER pt1,pt2;
            pt1.lx = nRX;
            pt1.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            pt2.lx = nRX - 1;
            pt2.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt2.ly,pt2.lx,pt2.dz);

            DOUBLE z;
            ComputeZValueInReferencePlan(pt1.lx,pt1.ly,pt1.dz,
                                         pt2.lx,pt2.ly,pt2.dz,
                                         nXLocation,nYLocation,dViewPointRealValue,
                                         nRX,nRY, z);
            fVisibleHeight = z;
            //计算点实际高度
            DOUBLE dActualZ ;
            m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
            if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
            {
                dActualZ = DEMANALYSENODATA;
            }
            //如果计算点实际高度大于可见最小高度
            if(dActualZ > fVisibleHeight)
            {
                IsVisible = 255;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                fVisibleHeight = dActualZ;
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //如果计算点高度小于可见高度
            else
            {
                IsVisible = 0;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            nRX ++;
        }
        nRY --;
        nRX = nXLocation + 1;
    }
    return 1;
}

/**
* @fn Compute3rdQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第三象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::Compute3rdQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation - 1;
    SINT nRY= nYLocation - 2;
    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;
    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;
    while(nRY >= 0)
    {
        while( nYLocation - nRY > nXLocation - nRX && nRX >= 0)
        {
            // 用于跟视点计算参考面的两个参考点
            POINTRASTER pt1,pt2;
            pt1.lx = nRX;
            pt1.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            pt2.lx = nRX + 1;
            pt2.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt2.ly,pt2.lx,pt2.dz);

            DOUBLE z;
            ComputeZValueInReferencePlan(pt1.lx,pt1.ly,pt1.dz,
                                         pt2.lx,pt2.ly,pt2.dz,
                                         nXLocation,nYLocation,dViewPointRealValue,
                                         nRX,nRY, z);
            fVisibleHeight = z;
            //计算点实际高度
            DOUBLE dActualZ ;
            m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
            if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
            {
                dActualZ = DEMANALYSENODATA;
            }
            //如果计算点实际高度大于可见最小高度
            if(dActualZ > fVisibleHeight)
            {
                IsVisible = 255;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                fVisibleHeight = dActualZ;
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //如果计算点高度小于可见高度
            else
            {
                IsVisible = 0;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            nRX --;
        }
        nRY --;
        nRX = nXLocation - 1;
    }
    return 1;
}

/**
* @fn Compute4thQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第四象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::Compute4thQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    //SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation - 2;
    SINT nRY= nYLocation - 1;
    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;
    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;
    while(nRX >= 0)
    {
        while( nYLocation - nRY < nXLocation - nRX && nRY >= 0)
        {
            // 用于跟视点计算参考面的两个参考点
            POINTRASTER pt1,pt2;
            pt1.lx = nRX + 1;
            pt1.ly = nRY;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            pt2.lx = nRX + 1;
            pt2.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt2.ly,pt2.lx,pt2.dz);

            DOUBLE z;
            ComputeZValueInReferencePlan(pt1.lx,pt1.ly,pt1.dz,
                                         pt2.lx,pt2.ly,pt2.dz,
                                         nXLocation,nYLocation,dViewPointRealValue,
                                         nRX,nRY, z);
            fVisibleHeight = z;
            //计算点实际高度
            DOUBLE dActualZ ;
            m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
            if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
            {
                dActualZ = DEMANALYSENODATA;
            }
            //如果计算点实际高度大于可见最小高度
            if(dActualZ > fVisibleHeight)
            {
                IsVisible = 255;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                fVisibleHeight = dActualZ;
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //如果计算点高度小于可见高度
            else
            {
                IsVisible = 0;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            nRY --;
        }
        nRY = nYLocation - 1;;
        nRX --;
    }
    return 1;
}

/**
* @fn Compute5thQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第五象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::Compute5thQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation - 2;
    SINT nRY= nYLocation + 1;
    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;
    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;
    while(nRX >= 0)
    {
        while(  nRY - nYLocation < nXLocation - nRX && nRY < nHeight)
        {
            // 用于跟视点计算参考面的两个参考点
            POINTRASTER pt1,pt2;
            pt1.lx = nRX + 1;
            pt1.ly = nRY;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            pt2.lx = nRX + 1;
            pt2.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt2.ly,pt2.lx,pt2.dz);

            DOUBLE z;
            ComputeZValueInReferencePlan(pt1.lx,pt1.ly,pt1.dz,
                                         pt2.lx,pt2.ly,pt2.dz,
                                         nXLocation,nYLocation,dViewPointRealValue,
                                         nRX,nRY, z);
            fVisibleHeight = z;
            //计算点实际高度
            DOUBLE dActualZ ;
            m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
            if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
            {
                dActualZ = DEMANALYSENODATA;
            }
            //如果计算点实际高度大于可见最小高度
            if(dActualZ > fVisibleHeight)
            {
                IsVisible = 255;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                fVisibleHeight = dActualZ;
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //如果计算点高度小于可见高度
            else
            {
                IsVisible = 0;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            nRY ++;
        }
        nRY = nYLocation + 1;;
        nRX --;
    }
    return 1;
}

/**
* @fn Compute6thQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第六象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::Compute6thQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    //SINT nWidth = m_pSrcRasterBlock->GetW();
    SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation - 1;
    SINT nRY= nYLocation + 2;
    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;
    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;
    while(nRY < nHeight)
    {
        while(  nRY - nYLocation > nXLocation - nRX && nRX >= 0)
        {
            // 用于跟视点计算参考面的两个参考点
            POINTRASTER pt1,pt2;
            pt1.lx = nRX + 1;
            pt1.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            pt2.lx = nRX;
            pt2.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt2.ly,pt2.lx,pt2.dz);

            DOUBLE z;
            ComputeZValueInReferencePlan(pt1.lx,pt1.ly,pt1.dz,
                                         pt2.lx,pt2.ly,pt2.dz,
                                         nXLocation,nYLocation,dViewPointRealValue,
                                         nRX,nRY, z);
            fVisibleHeight = z;
            //计算点实际高度
            DOUBLE dActualZ ;
            m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
            if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
            {
                dActualZ = DEMANALYSENODATA;
            }
            //如果计算点实际高度大于可见最小高度
            if(dActualZ > fVisibleHeight)
            {
                IsVisible = 255;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                fVisibleHeight = dActualZ;
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //如果计算点高度小于可见高度
            else
            {
                IsVisible = 0;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            nRX --;
        }
        nRY ++;
        nRX = nXLocation - 1;
    }
    return 1;
}

/**
* @fn Compute7thQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第七象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::Compute7thQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    SINT nWidth = m_pSrcRasterBlock->GetW();
    SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation + 1;
    SINT nRY= nYLocation + 2;
    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;
    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;
    while(nRY < nHeight)
    {
        while(  nRY - nYLocation > nRX - nXLocation && nRX < nWidth)
        {
            // 用于跟视点计算参考面的两个参考点
            POINTRASTER pt1,pt2;
            pt1.lx = nRX - 1;
            pt1.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            pt2.lx = nRX;
            pt2.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt2.ly,pt2.lx,pt2.dz);

            DOUBLE z;
            ComputeZValueInReferencePlan(pt1.lx,pt1.ly,pt1.dz,
                                         pt2.lx,pt2.ly,pt2.dz,
                                         nXLocation,nYLocation,dViewPointRealValue,
                                         nRX,nRY, z);
            fVisibleHeight = z;
            //计算点实际高度
            DOUBLE dActualZ ;
            m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
            if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
            {
                dActualZ = DEMANALYSENODATA;
            }
            //如果计算点实际高度大于可见最小高度
            if(dActualZ > fVisibleHeight)
            {
                IsVisible = 255;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                fVisibleHeight = dActualZ;
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //如果计算点高度小于可见高度
            else
            {
                IsVisible = 0;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            nRX ++;
        }
        nRY ++;
        nRX = nXLocation + 1;
    }
    return 1;
}

/**
* @fn Compute8thQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第八象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::Compute8thQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{
    SINT nWidth = m_pSrcRasterBlock->GetW();
    SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);
    // 用于计算的高度是地形高度加上视点高度
    DOUBLE dViewPointRealValue = dZValue + dViewHight;
    if(pBlockOut == NULL || pBlockOut->GetGDTType() != GDT_Byte)
    {
        return -1;
    }
    if(pVisibleHeightBloc ==NULL || pVisibleHeightBloc->GetGDTType() != GDT_Float32)
    {
        return -2;
    }
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation + 2;
    SINT nRY= nYLocation + 1;
    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;
    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;
    while(nRX < nWidth)
    {
        while(  nRY - nYLocation < nRX - nXLocation && nRY < nHeight)
        {
            // 用于跟视点计算参考面的两个参考点
            POINTRASTER pt1,pt2;
            pt1.lx = nRX - 1;
            pt1.ly = nRY - 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            pt2.lx = nRX - 1;
            pt2.ly = nRY;
            pVisibleHeightBloc->GetDoubleVal(pt2.ly,pt2.lx,pt2.dz);

            DOUBLE z;
            ComputeZValueInReferencePlan(pt1.lx,pt1.ly,pt1.dz,
                                         pt2.lx,pt2.ly,pt2.dz,
                                         nXLocation,nYLocation,dViewPointRealValue,
                                         nRX,nRY, z);
            fVisibleHeight = z;
            //计算点实际高度
            DOUBLE dActualZ ;
            m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
            if(fabs(m_pSrcRasterBlock->m_pGdalData->GetNoDataVal() - dActualZ) < DOUBLETOLERANCE)
            {
                dActualZ = DEMANALYSENODATA;
            }
            //如果计算点实际高度大于可见最小高度
            if(dActualZ > fVisibleHeight)
            {
                IsVisible = 255;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                fVisibleHeight = dActualZ;
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //如果计算点高度小于可见高度
            else
            {
                IsVisible = 0;
                pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
                pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
            }
            //if(IsVisible == 255)
            //cout<< nRX<<", "<<nRY<< ", " <<fVisibleHeight<< ", "<<dActualZ <<", "<< (SINT)IsVisible<<endl;
            nRY ++;
        }
        nRY = nYLocation  + 1;
        nRX ++;
    }
    return 1;
}

/**
* @fn ComputeEightQuadrantViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算八个象限的通视域
* @param nxlocation， 视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight，  视点距离地面的坐标
* @param pBlockOut，  计算的可视矩阵，传入像素类型为GDT_Byte;
* @param VisibleHeightBloc, 计算的可视参考高度，传入像素类型为GDT_Float32
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeEightQuadrantViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc)
{

    if( ( 1 == Compute1stQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,  pVisibleHeightBloc) )&&
            ( 1 == Compute2ndQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,  pVisibleHeightBloc) )&&
            ( 1 == Compute3rdQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,  pVisibleHeightBloc) )&&
            ( 1 == Compute4thQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,  pVisibleHeightBloc) )&&
            ( 1 == Compute5thQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,  pVisibleHeightBloc) )&&
            ( 1 == Compute6thQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,  pVisibleHeightBloc) )&&
            ( 1 == Compute7thQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,  pVisibleHeightBloc) )&&
            ( 1 == Compute8thQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,  pVisibleHeightBloc) ) )
    {
        return 1;
    }
    else
    {
        return -1;
    }

}

/**
* @fn ComputeZValueInReferencePlan
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据三个参考点确定的平面，计算水平位置在平面投影上的高程值
* @param nx1， 第1点的x坐标
* @param ny1， 第1点的y坐标
* @param dz1， 第1点的z坐标
* @param nx2， 第2点的x坐标
* @param ny2， 第2点的y坐标
* @param dz2， 第2点的z坐标
* @param nx3， 第3点的x坐标
* @param ny3， 第3点的y坐标
* @param dz3， 第3点的z坐标
* @param nx， 计算点的x坐标
* @param ny， 计算点的y坐标
* @param dz， 计算点的z坐标
* @retval true 成功
* @retval false 失败，三点共线
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlDemAnalyse::ComputeZValueInReferencePlan(SINT nx1, SINT ny1, DOUBLE dz1,
        SINT nx2, SINT ny2, DOUBLE dz2,
        SINT nx3, SINT ny3, DOUBLE dz3,
        SINT nx, SINT ny, DOUBLE &dz)
{
    SINT x21 = nx2 - nx1;
    SINT y21 = ny2 - ny1;
    DOUBLE z21 = dz2 - dz1;
    SINT x31 = nx3 - nx1;
    SINT y31 = ny3 - ny1;
    DOUBLE z31 = dz3 - dz1;
    if((x21 * y31 - x31 * y21) == 0)
    {
        return false;
    }
    dz = dz1 - (DOUBLE)((nx - nx1)*(y21 * z31 - y31 * z21) + (ny - ny1)*(z21 * x31 - z31 * x21)) / (x21 * y31 - x31 * y21);
    return true;
}

/**
* @fn ComputeViewShed
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算通视域
* @param nxlocation,  视点x坐标
* @param nyLocation， 视点y坐标
* @param dViewHight， 视点距离地面的坐标
* @param pBlockOut， 计算出的可视矩阵
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeViewShed(SINT nXLocation,SINT nYLocation, DOUBLE dViewHight, CmlRasterBlock* &pBlockOut)
{

    if(m_pSrcRasterBlock == NULL)
    {
        return -1;
    }
    //矩阵的宽/高
    SINT nWidth = m_pSrcRasterBlock->GetW();
    SINT nHeight = m_pSrcRasterBlock->GetH();
    DOUBLE dZValue ;
    if(nXLocation < 0|| nYLocation < 0 || nXLocation >= nWidth || nYLocation >= nHeight)
    {
        return -1;
    }
    m_pSrcRasterBlock->GetDoubleVal(nYLocation,nXLocation, dZValue);

    // 用于计算的高度是地形高度加上视点高度
    // DOUBLE dViewPointRealValue = dZValue + dViewHight;
    //cout << nXLocation <<", " << nYLocation << ", " << dViewPointRealValue << endl;
    CmlRasterBlock* pVisibleHeightBloc ;
    try
    {
        //用于记录的最小可见高度的矩阵
        pVisibleHeightBloc = new CmlRasterBlock(0,0,nWidth,nHeight,4,1);
        pVisibleHeightBloc->SetGDTType(GDT_Float32);
    }
    catch(...)
    {
        return -1;
    }
    // 计算八个方向上的可视矩阵
    ComputeEightDirectionViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,pVisibleHeightBloc);
    // 计算八个象限的可视矩阵
    ComputeEightQuadrantViewShed( nXLocation, nYLocation,  dViewHight , pBlockOut ,pVisibleHeightBloc);


    DOUBLE dddd ;
    pBlockOut->GetDoubleVal(2,3,dddd);
    //cout << dddd << ", "<< endl;
    pBlockOut->GetDoubleVal(1,3,dddd);
    //cout << dddd << ", "<< endl;
    pBlockOut->GetDoubleVal(0,3,dddd);
    //cout << dddd << ", "<< endl;
    pBlockOut->GetDoubleVal(3,2,dddd);
    //cout << dddd << ", "<< endl;
    pBlockOut->GetDoubleVal(3,5,dddd);
    //cout << dddd << ", "<< endl;
    pBlockOut->GetDoubleVal(3,6,dddd);
    //cout << dddd << ", "<< endl;
    pBlockOut->GetDoubleVal(3,4,dddd);
    // cout << dddd << ", "<< endl;
    pBlockOut->GetDoubleVal(3,7,dddd);
    //cout << dddd << ", "<< endl;
    delete pVisibleHeightBloc;
    return 1;

    /*
    //计算点的x方向与y方向的坐标
    SINT nRX = nXLocation;
    SINT nRY= nYLocation - 1;

    // 视点可见的最小高度
    FLOAT fVisibleHeight = DEMANALYSENODATA;

    // 标记是否可视，可视为255，否则为0
    BYTE IsVisible = 0;

    //计算正北方向的可视点
    while(nRY >= 0)
    {
        //当临近视点的时候
        if(nRY == nYLocation -1)
        {
            fVisibleHeight = DEMANALYSENODATA;
        }
        else
        {
            // 计算最小高度的前一个参考点
            POINTRASTER pt1;
            pt1.lx = nRX;
            pt1.ly = nRY + 1;
            pVisibleHeightBloc->GetDoubleVal(pt1.ly,pt1.lx,pt1.dz);
            fVisibleHeight = dViewPointRealValue - (nRY - nYLocation)/(pt1.ly - nYLocation)
                    * (pt1.dz - dViewPointRealValue);
        }

        //计算点实际高度
        DOUBLE dActualZ ;
        m_pSrcRasterBlock->GetDoubleVal(nRY,nRX,dActualZ);
        //如果计算点实际高度大于可见最小高度
        if(dActualZ > fVisibleHeight)
        {
            IsVisible = 255;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            fVisibleHeight = dActualZ;
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        //如果计算点高度小于可见高度
        else
        {
            IsVisible = 0;
            pBlockOut->SetPtrAt(nRY,nRX,&IsVisible);
            pVisibleHeightBloc->SetPtrAt(nRY,nRX,(BYTE*)&fVisibleHeight);
        }
        nRY --;
    }
    */
    // 计算正南方方向的可视点

}
/**
* @fn ComputeViewShedInterface
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口，根据输入DEM数据和视点坐标，计算通视图
* @param sInputDEM,  输入DEM文件路径
* @param nxLocation, 视点x坐标
* @param nyLocation, 视点y坐标
* @param ViewHight，  视点距离地面的高度
* @param sDestDEM，   输出文件路径
* @param InverseHeight， 是否将高程反转
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeViewShedInterface( const SCHAR * sInputDEM, SINT nXLocation ,SINT nYLocation, DOUBLE dViewHight, const SCHAR * sDestDEM, bool InverseHeight)
{
    /*
    CmlGeoRaster gdataset;
    gdataset.LoadGeoFile("/home/lw/Desktop/hghgh3.tif");
    CmlRasterBlock RasterBlock;
    gdataset.GetRasterOriginBlock(1,0,0,gdataset.GetWidth(),gdataset.GetHeight() ,1,&RasterBlock);
    CmlGeoRaster destdataset;
    destdataset.CreateGeoFile("/home/lw/Desktop/hghgh3viewshed.tif",gdataset.m_PtOrigin,gdataset.m_dXResolution,gdataset.m_dYResolution
                              ,gdataset.GetHeight(),gdataset.GetWidth(),1,GDT_Byte,-99999);
    CmlDemAnalyse demanaly(&RasterBlock);
    CmlRasterBlock* pOutBlock;
    demanaly.m_ComputeViewShed(3,3,300,pOutBlock);
    destdataset.SaveBlockToFile(1,0,0,pOutBlock);
    */
    CmlGeoRaster gdataset;
    if(gdataset.LoadGeoFile(sInputDEM) == false )
    {
        return -2;
    }
    CmlRasterBlock RasterBlock;
    //将图像数据读入内存失败返回-1
    if(false == gdataset.GetRasterOriginBlock( (UINT)1, (UINT)0,(UINT)0,gdataset.GetWidth(),gdataset.GetHeight() , (UINT)1,&RasterBlock))
    {
        return -1;
    }

    CmlGeoRaster destdataset;
    //创建图像失败返回-2
    if(false == destdataset.CreateGeoFile(sDestDEM,gdataset.m_PtOrigin,gdataset.m_dXResolution,gdataset.m_dYResolution
                                          ,gdataset.GetHeight(),gdataset.GetWidth(),1,GDT_Byte,DEMANALYSENODATA))
    {
        return -2;
    }
    // 如果高程反转应该将每个高程乘以-1
    if(InverseHeight == true)
    {
        for(int i = 0; i< RasterBlock.GetW(); i++)
        {
            for(int j = 0; j< RasterBlock.GetH(); j++)
            {
                DOUBLE dValue;
                RasterBlock.GetDoubleVal(j,i,dValue);
                // cout<< dValue <<";";
                dValue = -1*dValue;
                RasterBlock.SetDoubleVal(j,i,dValue);
                //RasterBlock.GetDoubleVal(j,i,dValue);
                //cout<< dValue <<endl;
            }
        }
    }
    this->m_pSrcRasterBlock = &RasterBlock;
    //创建可视域内存失败
    CmlRasterBlock* pBlockOut;
    try
    {
        //用于输出的可视矩阵
        pBlockOut = new CmlRasterBlock(0,0,gdataset.GetWidth(),gdataset.GetHeight(),1,1);
        pBlockOut->SetGDTType(GDT_Byte);
    }
    catch(...)
    {
        return -3;
    }
    // 计算可视矩阵出错
    if(ComputeViewShed(nXLocation,nYLocation,dViewHight,pBlockOut) < 0)
    {
        return -4;
    }
    destdataset.SaveBlockToFile(1,0,0,pBlockOut);
    delete pBlockOut;
    return 1;
}

/**
* @fn ComputeSlopeInterface
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，输出坡度
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeSlopeInterface(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize,DOUBLE dZfactor)
{
    CmlGeoRaster SourceDataset;
    if(SourceDataset.LoadGeoFile(sInputDEM) == false)
    {
        return -1;
    }
    CmlGeoRaster Destdataset;
    if( false == Destdataset.CreateGeoFile(sDestDEM,SourceDataset.m_PtOrigin,SourceDataset.m_dXResolution,SourceDataset.m_dYResolution
                                           ,SourceDataset.GetHeight(),SourceDataset.GetWidth(),1,GDT_Float64,DEMANALYSENODATA))
    {
        return -1;
    }
    DOUBLE dGridResolution = fabs(SourceDataset.m_dXResolution);
    if(ComputeSlope(&SourceDataset,nWindowSize,&Destdataset,dGridResolution,dZfactor) < 0)
    {
        return -2;
    }
    return 1;
}

SINT CmlDemAnalyse::ComputeSlope(CmlGdalDataset* pSrcDataset, SINT nWindowSize, CmlGdalDataset* pDestDataset, DOUBLE dGridResolution, DOUBLE dZfactor)
{
    // 计算窗口最小为3*3
    if(nWindowSize < 3 || nWindowSize % 2 == 0)
    {
        return -1;
    }
    if(pSrcDataset == NULL || pDestDataset == NULL)
    {
        return -2;
    }
    SINT nimgWidth = pSrcDataset->GetWidth();
    SINT nimgHeight = pSrcDataset->GetHeight();

    // 影像分块类 4000*4000
    // 分块重叠为计算窗口大小减一
    CmlBlockCalculation* pBC = new CmlBlockCalculation(4000,4000,nWindowSize-1,nWindowSize-1);
    // 3500*3500 进行计算
    pBC->CalBlockCol(nimgWidth,3500);
    pBC->CalBlockRow(nimgHeight,3500);
    SINT nColCount = pBC->GetColCount();
    SINT nRowCount = pBC->GetRowCount();

    for(SINT i = 0; i < nRowCount; i++)
    {
        for(SINT j = 0; j< nColCount; j++)
        {
            SINT nBlockWidth = pBC->GetBlockW();
            SINT nBlockHeight = pBC->GetBlockH();

            if(j == nColCount - 1)
            {
                nBlockWidth = pBC->GetLastBlockW();
            }
            if(i == nRowCount -1)
            {
                nBlockHeight = pBC->GetLastBlockH();
            }
            UINT nTopLeftx = pBC->GetStartCol(j);
            UINT nTopLefty = pBC->GetStartRow(i);
            CmlRasterBlock*  pDEMBlock = new CmlRasterBlock();
            pSrcDataset->GetRasterOriginBlock( (UINT)1,nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,(UINT)1,pDEMBlock); ;
            //CmlRasterBlock* pResultBlock = new CmlRasterBlock(nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,8,1);
            CmlRasterBlock* pResultBlock = new CmlRasterBlock();
            pDestDataset->GetRasterOriginBlock( (UINT)1,nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,(UINT)1,pResultBlock); ;

            ComputeSlopeBlock( pDEMBlock,  nWindowSize,  pResultBlock ,  dGridResolution,   dZfactor);
            SINT nSaveBlockXOffset = nWindowSize / 2;
            SINT nSaveBlockYOffset = nWindowSize / 2;
            SINT nSaveBlockWidth = nBlockWidth - nSaveBlockXOffset;
            SINT nSaveBlockHeight = nBlockHeight - nSaveBlockYOffset ;
            if(j == 0)
            {
                nSaveBlockXOffset = 0;
                nSaveBlockWidth = nBlockWidth;
            }
            if(i == 0)
            {
                nSaveBlockYOffset = 0;
                nSaveBlockHeight = nBlockHeight;
            }
            pDestDataset->SaveBlockToFile(1,nTopLeftx,nTopLefty,pResultBlock, nSaveBlockXOffset,nSaveBlockYOffset,nSaveBlockWidth,nSaveBlockHeight);
            /*
                        for(SINT m = 0; m< pResultBlock->GetW(); m++)
                            for(SINT n = 0; n< pResultBlock->GetH(); n++)
                            {
                                DOUBLE ddd;
                                pResultBlock->GetDoubleVal(n,m,ddd);
                                cout<< ddd<< endl;
                            }
            */
            delete pDEMBlock;
            delete pResultBlock;
        }
    }
    delete pBC;
    return 1;

}

SINT CmlDemAnalyse::ComputeSlopeBlock(CmlRasterBlock* pDEMBlock, SINT nWindowSize, CmlRasterBlock* pResultBlock ,DOUBLE dGridResolution, DOUBLE dZfactor)
{
    SINT nW;
    SINT nH;
    nW = pDEMBlock->GetW();
    nH = pDEMBlock->GetH();
    //DOUBLE DEMNODatavalue = pDEMBlock->m_pGdalData->GetNoDataVal();
    //逐像素循环
    for(SINT i = 0; i <pDEMBlock->GetW(); i++)
    {
        for(SINT j = 0; j< pDEMBlock->GetH(); j++)
        {
            //生成计算窗口矩阵
            CmlMat CalMat;
            CalMat.Initial(nWindowSize,nWindowSize);
            SINT m = 0 - (SINT)(nWindowSize / 2);
            SINT n = 0 - (SINT)(nWindowSize / 2);
            SINT nXLocation;
            SINT nYLocation;

            while(m <= (SINT)nWindowSize / 2)
            {
                nXLocation = i + m;
                while (n <= (SINT)nWindowSize / 2 )
                {
                    nYLocation = j + n;
                    DOUBLE dz;
                    if(nXLocation < 0 || nXLocation >= nW || nYLocation < 0 || nYLocation >= nH)
                    {
                        pDEMBlock->GetDoubleVal(j,i, dz);
                    }
                    else
                    {
                        pDEMBlock->GetDoubleVal(nYLocation,nXLocation,dz);
                    }
                    // nodata 一律用中心点位置替代
                    if(fabs(dz - pDEMBlock->m_pGdalData->GetNoDataVal())< DOUBLETOLERANCE )
                    {
                        pDEMBlock->GetDoubleVal(j,i,dz);
                    }
                    CalMat.SetAt(n + (SINT)(nWindowSize / 2), m + (SINT)(nWindowSize / 2), dz);
                    // 计算DA，DB，DC的值
                    n ++;
                }
                n = 0 - (SINT)(nWindowSize / 2);
                m++;
            }

            // cout<<endl;
            DOUBLE dAcoef,dBcoef,dCcoef;
            DOUBLE dDEMCenterZvalue ;
            dDEMCenterZvalue = CalMat.GetAt(CalMat.GetW() / 2 , CalMat.GetH() /2);
            DOUBLE dResult;
            // 如果中心高程为Nodata，则坡度值为Nodata
            if(fabs(dDEMCenterZvalue - pDEMBlock->m_pGdalData->GetNoDataVal()) < DOUBLETOLERANCE)
            {
                dResult = pResultBlock->m_pGdalData->GetNoDataVal();
            }
            else
            {
                FitPlaneByMatrix(  &CalMat,   dAcoef,   dBcoef,   dCcoef , dGridResolution ,dZfactor );
                dResult = atanl(sqrt(dAcoef * dAcoef + dBcoef * dBcoef)) * 180 / ML_PI;
            }
            pResultBlock->SetPtrAt(j,i,(BYTE*)&dResult);
            /*
                        if(dResult > 85)
                        {
                                    cout << endl;
                                    for(SINT s =  0; s< CalMat.GetH(); s++)
                                    {
                                        for(SINT t = 0; t < CalMat.GetW(); t++)
                                        {
                                            cout<<CalMat.GetAt(s,t)<<",";
                                        }
                                         cout << endl;
                                    }
                        }
                        */
            // cout << dResult << endl;
            dResult = 0;
        }
    }
    return 1;
}

SINT CmlDemAnalyse::FitPlaneByMatrix(CmlMat* pMatrix, DOUBLE& dAcoef, DOUBLE& dBcoef, DOUBLE& dCcoef ,DOUBLE dGridResolution,DOUBLE dZfactor)
{

    //CmlMat dd;//  dd.mlMatSolveSVD
    CmlMat MatA, MatB, MatOut;
    MatA.Initial(3,3);
    MatB.Initial(3,1);

    DOUBLE dTvalue00 = 0;
    DOUBLE dTvalue01 = 0;
    DOUBLE dTvalue02 = 0;
    DOUBLE dTvalue10 = 0;
    DOUBLE dTvalue11 = 0;
    DOUBLE dTvalue12 = 0;
    DOUBLE dTvalue20 = 0;
    DOUBLE dTvalue21 = 0;
    //DOUBLE dTvalue22 = 0;
    DOUBLE dTvalueB00 = 0;
    DOUBLE dTvalueB10 = 0;
    DOUBLE dTvalueB20 = 0;

    SINT nOffset = pMatrix->GetW() / 2;

    for(SINT i = 0; i < pMatrix->GetW(); i++)
    {
        for(SINT j = 0; j < pMatrix->GetH(); j++)
        {
            dTvalue00  +=  (i - nOffset) * dGridResolution * (i - nOffset) * dGridResolution;
            dTvalue01  +=  (i - nOffset) * dGridResolution * (j - nOffset) * dGridResolution;
            dTvalue02  +=  (i - nOffset) * dGridResolution;
            dTvalue10  +=  (i - nOffset) * dGridResolution * (j - nOffset) * dGridResolution;
            dTvalue11  +=  (j - nOffset) * dGridResolution * (j - nOffset) * dGridResolution;
            dTvalue12  +=  (j - nOffset) * dGridResolution;
            dTvalue20  +=  (i - nOffset) * dGridResolution;
            dTvalue21  +=  (j - nOffset) * dGridResolution;
            dTvalueB00 +=  (i - nOffset) * dGridResolution * pMatrix->GetAt(j,i) * dZfactor;
            dTvalueB10 +=  (j - nOffset) * dGridResolution * pMatrix->GetAt(j,i) * dZfactor;
            dTvalueB20 += pMatrix->GetAt(j,i) * dZfactor;
        }
    }
    MatA.SetAt(0,0,dTvalue00);
    MatA.SetAt(0,1,dTvalue01);
    MatA.SetAt(0,2,dTvalue02);
    MatA.SetAt(1,0,dTvalue10);
    MatA.SetAt(1,1,dTvalue11);
    MatA.SetAt(1,2,dTvalue12);
    MatA.SetAt(2,0,dTvalue20);
    MatA.SetAt(2,1,dTvalue21);
    MatA.SetAt(2,2,pMatrix->GetW() * pMatrix->GetH());

    MatB.SetAt(0,0,dTvalueB00);
    MatB.SetAt(1,0,dTvalueB10);
    MatB.SetAt(2,0,dTvalueB20);

    /*
       DOUBLE dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
               dTvalue +=  (i - nOffset) * dGridResolution * (i - nOffset) * dGridResolution;
              // cout << (i - nOffset) << "," <<  (j - nOffset) << endl;

           }
       }
       MatA.SetAt(0,0,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
               dTvalue  +=  (i - nOffset) * dGridResolution * (j - nOffset) * dGridResolution;
           }
       }
       MatA.SetAt(0,1,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
               dTvalue  +=  (i - nOffset)* dGridResolution;
           }
       }
       MatA.SetAt(0,2,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
                dTvalue  +=  (i - nOffset)* dGridResolution * (j - nOffset) * dGridResolution;
           }
       }
       MatA.SetAt(1,0,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
                dTvalue  +=  (j - nOffset)* dGridResolution * (j - nOffset) * dGridResolution;
           }
       }
       MatA.SetAt(1,1,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
                dTvalue  +=  (j - nOffset) * dGridResolution;
           }
       }
       MatA.SetAt(1,2,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
                dTvalue  +=  (i - nOffset)* dGridResolution;
           }
       }
       MatA.SetAt(2,0,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
                dTvalue  +=   (j - nOffset) * dGridResolution;
           }
       }
       MatA.SetAt(2,1,dTvalue);
       MatA.SetAt(2,2,pMatrix->GetW() * pMatrix->GetH());


      // MatB.Initial(3,1);
       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
                dTvalue  +=   (i - nOffset) * dGridResolution * pMatrix->GetAt(j,i) * dZfactor;
           }
       }
       MatB.SetAt(0,0,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
                dTvalue  +=   (j - nOffset) * dGridResolution * pMatrix->GetAt(j,i) * dZfactor;
           }
       }
       MatB.SetAt(1,0,dTvalue);

       dTvalue = 0;
       for(SINT i = 0; i < pMatrix->GetW(); i++)
       {
           for(SINT j = 0; j < pMatrix->GetH(); j++)
           {
                dTvalue  += pMatrix->GetAt(j,i) * dZfactor;
           }
       }
       MatB.SetAt(2,0,dTvalue);
    */
    if(!mlMatSolveSVD(&MatA,&MatB,&MatOut))
    {
        return -1;
    }
    dAcoef = MatOut.GetAt(0,0);
    dBcoef = MatOut.GetAt(1,0);
    dCcoef = MatOut.GetAt(2,0);
    return 1;
    //mlMatSolveSVD()
}

SINT CmlDemAnalyse::ComputeRoughnessValue(CmlMat* pMatrix, DOUBLE dAcoef, DOUBLE dBcoef, DOUBLE dCcoef ,DOUBLE dGridResolution,  DOUBLE& dRoughnessValue, DOUBLE dZfactor)
{
    if(pMatrix->GetW() <=0 || pMatrix->GetH() <=0)
    {
        return -1;
    }
    // 拟合平面的理论值
    DOUBLE dTvalue;
    //  真是高程值
    DOUBLE dRealValue;
    //高程到平面距离差的和
    DOUBLE dTotalDiff = 0;
    SINT nOffset = pMatrix->GetW() / 2;
    for(SINT i = 0; i < pMatrix->GetW(); i++)
    {
        for(SINT j = 0; j < pMatrix->GetH(); j++)
        {
            dRealValue = pMatrix->GetAt(j,i) * dZfactor;
            dTvalue = dAcoef * (i - nOffset) * dGridResolution + dBcoef * (j - nOffset) * dGridResolution + dCcoef;
            DOUBLE dH = fabs(dRealValue - dTvalue) / sqrt(dAcoef * dAcoef + dBcoef * dBcoef + 1);
            dTotalDiff += dH;
            //dTvalue +=  (i - nOffset) * dGridResolution * (i - nOffset) * dGridResolution;
            // cout << (i - nOffset) << "," <<  (j - nOffset) << endl;
        }
    }
    dRoughnessValue = dTotalDiff / (pMatrix->GetW() * pMatrix->GetH());
    return 1;
}

SINT CmlDemAnalyse::ComputeStepValue(CmlMat* pMatrix, DOUBLE& dDiffZ, DOUBLE dZfactor)
{
    if(pMatrix->GetW() <=0 || pMatrix->GetH() <=0)
    {
        return -1;
    }
    DOUBLE dMax = -999999;
    DOUBLE dMin =  999999;
    for(SINT i = 0; i < pMatrix->GetW(); i++)
    {
        for(SINT j = 0; j < pMatrix->GetH(); j++)
        {
            DOUBLE dRealValue = pMatrix->GetAt(j,i) * dZfactor;
            if(dRealValue > dMax)
            {
                dMax = dRealValue;
            }
            if(dRealValue < dMin)
            {
                dMin = dRealValue;
            }
        }
    }
    dDiffZ = dMax - dMin;
    return 1;
}

/**
* @fn ComputeSlopeAspectInterface
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，输出坡向
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeSlopeAspectInterface(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor)
{
    CmlGeoRaster SourceDataset;
    if(SourceDataset.LoadGeoFile(sInputDEM) == false)
    {
        return -1;
    }
    CmlGeoRaster Destdataset;
    if( false == Destdataset.CreateGeoFile(sDestDEM,SourceDataset.m_PtOrigin,SourceDataset.m_dXResolution,SourceDataset.m_dYResolution
                                           ,SourceDataset.GetHeight(),SourceDataset.GetWidth(),1,GDT_Float64,DEMANALYSENODATA))
    {
        return -1;
    }
    DOUBLE dGridResolution = fabs(SourceDataset.m_dXResolution);
    if(ComputeSlopeAspect(&SourceDataset,nWindowSize,&Destdataset,dGridResolution,dZfactor) < 0)
    {
        return -2;
    }
    return 1;
}

SINT CmlDemAnalyse::ComputeSlopeAspect(CmlGdalDataset* pSrcDataset, SINT nWindowSize, CmlGdalDataset* pDestDataset, DOUBLE dGridResolution, DOUBLE dZfactor)
{
    // 计算窗口最小为3*3
    if(nWindowSize < 3 || nWindowSize % 2 == 0)
    {
        return -1;
    }
    if(pSrcDataset == NULL || pDestDataset == NULL)
    {
        return -2;
    }
    SINT nimgWidth = pSrcDataset->GetWidth();
    SINT nimgHeight = pSrcDataset->GetHeight();

    // 影像分块类 4000*4000
    // 分块重叠为计算窗口大小减一
    CmlBlockCalculation* pBC = new CmlBlockCalculation(4000,4000,nWindowSize-1,nWindowSize-1);
    // 3500*3500 进行计算
    pBC->CalBlockCol(nimgWidth,3500);
    pBC->CalBlockRow(nimgHeight,3500);
    SINT nColCount = pBC->GetColCount();
    SINT nRowCount = pBC->GetRowCount();

    for(SINT i = 0; i < nRowCount; i++)
    {
        for(SINT j = 0; j< nColCount; j++)
        {
            SINT nBlockWidth = pBC->GetBlockW();
            SINT nBlockHeight = pBC->GetBlockH();

            if(j == nColCount - 1)
            {
                nBlockWidth = pBC->GetLastBlockW();
            }
            if(i == nRowCount -1)
            {
                nBlockHeight = pBC->GetLastBlockH();
            }
            SINT nTopLeftx = pBC->GetStartCol(j);
            SINT nTopLefty = pBC->GetStartRow(i);
            CmlRasterBlock*  pDEMBlock = new CmlRasterBlock();
            pSrcDataset->GetRasterOriginBlock((UINT)1,nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,(UINT)1,pDEMBlock); ;
            //CmlRasterBlock* pResultBlock = new CmlRasterBlock(nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,8,1);
            CmlRasterBlock* pResultBlock = new CmlRasterBlock();
            pDestDataset->GetRasterOriginBlock((UINT)1,nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,(UINT)1,pResultBlock);
            ComputeSlopeAspectBlock( pDEMBlock,  nWindowSize,  pResultBlock ,  dGridResolution,   dZfactor);
            SINT nSaveBlockXOffset = nWindowSize / 2;
            SINT nSaveBlockYOffset = nWindowSize / 2;
            SINT nSaveBlockWidth = nBlockWidth - nSaveBlockXOffset;
            SINT nSaveBlockHeight = nBlockHeight - nSaveBlockYOffset ;
            if(j == 0)
            {
                nSaveBlockXOffset = 0;
                nSaveBlockWidth = nBlockWidth;
            }
            if(i == 0)
            {
                nSaveBlockYOffset = 0;
                nSaveBlockHeight = nBlockHeight;
            }
            pDestDataset->SaveBlockToFile(1,nTopLeftx,nTopLefty,pResultBlock, nSaveBlockXOffset,nSaveBlockYOffset,nSaveBlockWidth,nSaveBlockHeight);
            /*
                        for(SINT m = 0; m< pResultBlock->GetW(); m++)
                            for(SINT n = 0; n< pResultBlock->GetH(); n++)
                            {
                                DOUBLE ddd;
                                pResultBlock->GetDoubleVal(n,m,ddd);
                                cout<< ddd<< endl;
                            }
            */
            delete pDEMBlock;
            delete pResultBlock;
        }
    }
    delete pBC;
    return 1;

}

SINT CmlDemAnalyse::ComputeSlopeAspectBlock(CmlRasterBlock* pDEMBlock, SINT nWindowSize, CmlRasterBlock* pResultBlock,DOUBLE dGridResolution, DOUBLE dZfactor)
{
    SINT nW;
    SINT nH;
    nW = pDEMBlock->GetW();
    nH = pDEMBlock->GetH();
    //DOUBLE DEMNODatavalue = pDEMBlock->m_pGdalData->GetNoDataVal();
    //逐像素循环
    for(SINT i = 0; i <pDEMBlock->GetW(); i++)
    {
        for(SINT j = 0; j< pDEMBlock->GetH(); j++)
        {
            //生成计算窗口矩阵
            CmlMat CalMat;
            CalMat.Initial(nWindowSize,nWindowSize);
            SINT m = 0 - (SINT)(nWindowSize / 2);
            SINT n = 0 - (SINT)(nWindowSize / 2);
            SINT nXLocation;
            SINT nYLocation;

            while(m <= (SINT)nWindowSize / 2)
            {
                nXLocation = i + m;
                while (n <= (SINT)nWindowSize / 2 )
                {
                    nYLocation = j + n;
                    DOUBLE dz;
                    if(nXLocation < 0 || nXLocation >= nW || nYLocation < 0 || nYLocation >= nH)
                    {
                        pDEMBlock->GetDoubleVal(j,i, dz);
                    }
                    else
                    {
                        pDEMBlock->GetDoubleVal(nYLocation,nXLocation,dz);
                    }
                    // nodata 一律用中心点位置替代
                    if(fabs(dz - pDEMBlock->m_pGdalData->GetNoDataVal())< DOUBLETOLERANCE )
                    {
                        pDEMBlock->GetDoubleVal(j,i,dz);
                    }
                    CalMat.SetAt(n + (SINT)(nWindowSize / 2), m + (SINT)(nWindowSize / 2), dz);
                    // 计算DA，DB，DC的值
                    n ++;
                }
                n = 0 - (SINT)(nWindowSize / 2);
                m++;
            }

            // cout<<endl;
            DOUBLE dAcoef,dBcoef,dCcoef;
            DOUBLE dDEMCenterZvalue ;
            dDEMCenterZvalue = CalMat.GetAt(CalMat.GetW() / 2 , CalMat.GetH() /2);
            DOUBLE dResult;
            // 如果中心高程为Nodata，则坡度值为Nodata
            if(fabs(dDEMCenterZvalue - pDEMBlock->m_pGdalData->GetNoDataVal()) < DOUBLETOLERANCE)
            {
                dResult = pResultBlock->m_pGdalData->GetNoDataVal();
            }
            else
            {
                FitPlaneByMatrix(  &CalMat,   dAcoef,   dBcoef,   dCcoef , dGridResolution, dZfactor );
                DOUBLE dfx = dAcoef;
                DOUBLE dfy = dBcoef;
                // fy == 0  时
                if(fabs(dfy)<DOUBLETOLERANCE)
                {
                    // fx == 0
                    if(fabs(dfx) < DOUBLETOLERANCE)
                    {
                        dResult = 0;
                    }
                    else// fx > 0
                        if(dfx > 0)
                        {
                            dResult = 90;
                        }
                        else// fx <0
                        {
                            dResult = 270;
                        }
                }
                else// fy > 0
                    if(dfy > 0)
                    {
                        // fx == 0
                        if(fabs(dfx) < DOUBLETOLERANCE)
                        {
                            dResult = 0;
                        }
                        else// fx > 0
                            if(dfx > 0)
                            {
                                dResult = atanl(dfx / dfy) * 180 / ML_PI;
                            }
                            else// fx <0
                            {
                                dResult = atanl(dfx / dfy) * 180 / ML_PI + 360;
                            }
                    }
                    else// fy <0
                    {
                        // fx == 0
                        if(fabs(dfx) < DOUBLETOLERANCE)
                        {
                            dResult = 180;
                        }
                        else// fx > 0
                            if(dfx > 0)
                            {
                                dResult = atanl(dfx / dfy) * 180 / ML_PI + 180;
                            }
                            else// fx <0
                            {
                                dResult = atanl(dfx / dfy) * 180 / ML_PI + 180;
                            }
                    }
                // dResult = atanl(sqrt(dAcoef * dAcoef + dBcoef * dBcoef)) * 180 / ML_PI;
            }
            //clockwise
            if(!fabs(dResult - pResultBlock->m_pGdalData->GetNoDataVal()) < DOUBLETOLERANCE && !fabs(dResult) < DOUBLETOLERANCE)
            {
                dResult = 0-dResult +360;
            }
            pResultBlock->SetPtrAt(j,i,(BYTE*)&dResult);
            dResult = 0;
        }
    }
    return 1;
}

SINT CmlDemAnalyse::ComputeObstacleMapBlock(CmlRasterBlock* pDEMBlock, SINT nWindowSize, CmlRasterBlock* pResultBlock, DOUBLE dGridResolution, DOUBLE dZfactor,
        ObstacleMapPara ObPara)
{
    SINT nW;
    SINT nH;
    nW = pDEMBlock->GetW();
    nH = pDEMBlock->GetH();
    //逐像素循环
    for(SINT i = 0; i <pDEMBlock->GetW(); i++)
    {
        for(SINT j = 0; j< pDEMBlock->GetH(); j++)
        {
            //生成计算窗口矩阵
            CmlMat CalMat;
            CalMat.Initial(nWindowSize,nWindowSize);
            SINT m = 0 - (SINT)(nWindowSize / 2);
            SINT n = 0 - (SINT)(nWindowSize / 2);
            SINT nXLocation;
            SINT nYLocation;

            while(m <= (SINT)nWindowSize / 2)
            {
                nXLocation = i + m;
                while (n <= (SINT)nWindowSize / 2 )
                {
                    nYLocation = j + n;
                    DOUBLE dz;
                    if(nXLocation < 0 || nXLocation >= nW || nYLocation < 0 || nYLocation >= nH)
                    {
                        pDEMBlock->GetDoubleVal(j,i, dz);
                    }
                    else
                    {
                        pDEMBlock->GetDoubleVal(nYLocation,nXLocation,dz);
                    }
                    // nodata 一律用中心点位置替代
                    if(fabs(dz - pDEMBlock->m_pGdalData->GetNoDataVal())< DOUBLETOLERANCE )
                    {
                        pDEMBlock->GetDoubleVal(j,i,dz);
                    }
                    CalMat.SetAt(n + (SINT)(nWindowSize / 2), m + (SINT)(nWindowSize / 2), dz);
                    // 计算DA，DB，DC的值
                    n ++;
                }
                n = 0 - (SINT)(nWindowSize / 2);
                m++;
            }

            // cout<<endl;
            DOUBLE dAcoef,dBcoef,dCcoef;
            DOUBLE dDEMCenterZvalue ;
            dDEMCenterZvalue = CalMat.GetAt(CalMat.GetW() / 2 , CalMat.GetH() /2);
            //坡度值
            DOUBLE dResult;
            //粗糙度值
            DOUBLE dRoughnessResult;
            //阶梯高差
            DOUBLE dDiffZ;
            // 阶梯障碍值
            DOUBLE dStepValue;
            // 是否障碍,255表示非障碍，0表示障碍
            BYTE byteResult = 255;
            // 如果中心高程为Nodata，则是障碍
            if(fabs(dDEMCenterZvalue - pDEMBlock->m_pGdalData->GetNoDataVal()) < DOUBLETOLERANCE)
            {
                //dResult = pResultBlock->m_pGdalData->GetNoDataVal();
                byteResult = 0;
            }
            else
            {
                // 判断是否障碍时，只考虑坡度/粗糙度/最大高差是否超标，不考虑三者归一化后的贡献值
                FitPlaneByMatrix(  &CalMat,   dAcoef,   dBcoef,   dCcoef , dGridResolution, dZfactor);
                dResult = atanl(sqrt(dAcoef * dAcoef + dBcoef * dBcoef)) * 180 / ML_PI;
                ComputeRoughnessValue(&CalMat,dAcoef,dBcoef,dCcoef,dGridResolution, dRoughnessResult,dZfactor);
                ComputeStepValue(&CalMat,dDiffZ,dZfactor);
                dStepValue = dGridResolution * dRoughnessResult * dDiffZ;
                if(dResult > ObPara.dMaxSlope)
                {
                    byteResult = 0;
                }
                if(dRoughnessResult > ObPara.dMaxRoughness)
                {
                    byteResult = 0;
                }
                if(dStepValue > ObPara.dMaxStep)
                {
                    byteResult = 0;
                }
                DOUBLE dObstacleValue = ObPara.dSlopeCoef * dResult / ObPara.dMaxSlope +
                                        ObPara.dRoughnessCoef * dRoughnessResult / ObPara.dMaxRoughness +
                                        ObPara.dStepCoef * dStepValue / ObPara.dMaxStep;
                if(dObstacleValue > ObPara.dMaxObstacleValue)
                {
                    byteResult = 0 ;
                }
                //cout << dResult << ",  " << dRoughnessResult << ",  " << dDiffZ << ",  " << dStepValue<< endl;
            }
            pResultBlock->SetPtrAt(j,i,(BYTE*)&byteResult);
        }
    }

    return 1;
}

SINT CmlDemAnalyse::ComputeObstacleMap(CmlGdalDataset* pSrcDataset, SINT nWindowSize, CmlGdalDataset* pDestDataset, DOUBLE dGridResolution, DOUBLE dZfactor,
                                       ObstacleMapPara ObPara)
{
    // 计算窗口最小为3*3
    if(nWindowSize < 3 || nWindowSize % 2 == 0)
    {
        return -1;
    }
    if(pSrcDataset == NULL || pDestDataset == NULL)
    {
        return -2;
    }
    if(fabs(ObPara.dMaxSlope) < DOUBLETOLERANCE ||
            fabs(ObPara.dMaxStep) < DOUBLETOLERANCE ||
            fabs(ObPara.dMaxRoughness) < DOUBLETOLERANCE)
    {
        return -3;
    }
    SINT nimgWidth = pSrcDataset->GetWidth();
    SINT nimgHeight = pSrcDataset->GetHeight();

    // 影像分块类 4000*4000
    // 分块重叠为计算窗口大小减一
    CmlBlockCalculation* pBC = new CmlBlockCalculation(4000,4000,nWindowSize-1,nWindowSize-1);
    // 3500*3500 进行计算
    pBC->CalBlockCol(nimgWidth,3500);
    pBC->CalBlockRow(nimgHeight,3500);
    SINT nColCount = pBC->GetColCount();
    SINT nRowCount = pBC->GetRowCount();

    for(SINT i = 0; i < nRowCount; i++)
    {
        for(SINT j = 0; j< nColCount; j++)
        {
            SINT nBlockWidth = pBC->GetBlockW();
            SINT nBlockHeight = pBC->GetBlockH();

            if(j == nColCount - 1)
            {
                nBlockWidth = pBC->GetLastBlockW();
            }
            if(i == nRowCount -1)
            {
                nBlockHeight = pBC->GetLastBlockH();
            }
            SINT nTopLeftx = pBC->GetStartCol(j);
            SINT nTopLefty = pBC->GetStartRow(i);
            CmlRasterBlock*  pDEMBlock = new CmlRasterBlock();
            pSrcDataset->GetRasterOriginBlock((UINT)1,nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,(UINT)1,pDEMBlock); ;
            //CmlRasterBlock* pResultBlock = new CmlRasterBlock(nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,8,1);
            CmlRasterBlock* pResultBlock = new CmlRasterBlock();
            pDestDataset->GetRasterOriginBlock((UINT)1,nTopLeftx,nTopLefty,nBlockWidth,nBlockHeight,(UINT)1,pResultBlock);
            ComputeObstacleMapBlock(pDEMBlock,  nWindowSize,  pResultBlock ,  dGridResolution,   dZfactor,ObPara);
            SINT nSaveBlockXOffset = nWindowSize / 2;
            SINT nSaveBlockYOffset = nWindowSize / 2;
            SINT nSaveBlockWidth = nBlockWidth - nSaveBlockXOffset;
            SINT nSaveBlockHeight = nBlockHeight - nSaveBlockYOffset ;
            if(j == 0)
            {
                nSaveBlockXOffset = 0;
                nSaveBlockWidth = nBlockWidth;
            }
            if(i == 0)
            {
                nSaveBlockYOffset = 0;
                nSaveBlockHeight = nBlockHeight;
            }
            pDestDataset->SaveBlockToFile(1,nTopLeftx,nTopLefty,pResultBlock, nSaveBlockXOffset,nSaveBlockYOffset,nSaveBlockWidth,nSaveBlockHeight);
            /*
                        for(SINT m = 0; m< pResultBlock->GetW(); m++)
                            for(SINT n = 0; n< pResultBlock->GetH(); n++)
                            {
                                DOUBLE ddd;
                                pResultBlock->GetDoubleVal(n,m,ddd);
                                cout<< ddd<< endl;
                            }
            */
            delete pDEMBlock;
            delete pResultBlock;
        }
    }
    delete pBC;
    return 1;
}

/**
* @fn ComputeObstacleMapInterface
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，障碍图参数计算障碍图
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
* @param ObPara: 障碍图参数结构体
* @retval 1 成功
* @retval 其他 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeObstacleMapInterface(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor,ObstacleMapPara ObPara)
{
    CmlGeoRaster SourceDataset;
    if(SourceDataset.LoadGeoFile(sInputDEM) == false)
    {
        return -1;
    }
    CmlGeoRaster Destdataset;
    if( false == Destdataset.CreateGeoFile(sDestDEM,SourceDataset.m_PtOrigin,SourceDataset.m_dXResolution,SourceDataset.m_dYResolution
                                           ,SourceDataset.GetHeight(),SourceDataset.GetWidth(),1,GDT_Byte,DEMANALYSENODATA))
    {
        return -1;
    }
    DOUBLE dGridResolution = fabs(SourceDataset.m_dXResolution);
    if(ComputeObstacleMap(&SourceDataset,nWindowSize,&Destdataset,dGridResolution,dZfactor ,ObPara) < 0)
    {
        return -2;
    }
    return 1;
}

/**
* @fn ComputeContourInterface
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据和等高距，计算等高线图
* @param dHinterval,  等高距
* @param strSrcfilename, 输入的DEM文件路径
* @param strDstfilename, 输出的shape文件路径
* @param bCNodata， 表示是否自定义Nodata值
* @param dNodata， 如果bCNodata设置为true，则dNodata 的值在计算时被当做无效值对待
* @param cAttrib，生成shape文件高程的属性名称，默认为elev
* @retval 1 成功
* @retval -1 失败，gdal版本过低
* @retval -2 失败，等高距设置有误
* @retval -3 失败，输入文件有误
* @retval -4 失败，输出文件有误
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
SINT CmlDemAnalyse::ComputeContourInterface(DOUBLE dHinterval, SCHAR* strSrcfilename, SCHAR* strDstfilename ,bool bCNodata , DOUBLE dNodata ,SCHAR* strAttrib )
{
    GDALDatasetH	hSrcDS;
    SINT b3D = FALSE, bIgnoreNoData = FALSE;
    SINT bNoDataSet = bCNodata;
    SINT nBandIn = 1;
    DOUBLE dfNoData = dNodata;
    DOUBLE dfInterval = 0.0,dfOffset = 0.0;
    const SCHAR *pszSrcFilename = NULL;
    const SCHAR *pszDstFilename = NULL;
    const SCHAR *pszElevAttrib = NULL;
    const SCHAR *pszFormat = "ESRI Shapefile";
    DOUBLE adfFixedLevels[1000];
    SINT    nFixedLevelCount = 0;
    const SCHAR *pszNewLayerName = "contour";
    SINT bQuiet = FALSE;
    GDALProgressFunc pfnProgress = NULL;

    /* Check that we are running against at least GDAL 1.4 */
    /* Note to developers : if we use newer API, please change the requirement */
    if (atoi(GDALVersionInfo("VERSION_NUM")) < 1400)
    {
        // gdal 版本过低
        return -1;
    }

    GDALAllRegister();
    OGRRegisterAll();

    pszElevAttrib = strAttrib;
    dfInterval = dHinterval;
    if( dfInterval == 0.0 && nFixedLevelCount == 0 )
    {
        // 等高距为零
        return -2;
    }
    pszSrcFilename = strSrcfilename;
    if (pszSrcFilename == NULL)
    {
        //  源文件输入有误
        return -3;
    }
    pszDstFilename = strDstfilename;
    if (pszDstFilename == NULL)
    {
        // 目的文件输入有误
        return -4;
    }

    if (!bQuiet)
    {
        pfnProgress = GDALTermProgress;
    }


    /* -------------------------------------------------------------------- */
    /*      Open source raster file.                                        */
    /* -------------------------------------------------------------------- */
    GDALRasterBandH hBand;
    hSrcDS = GDALOpen( pszSrcFilename, GA_ReadOnly );
    if( hSrcDS == NULL )
    {
        // 源文件输入有误
        return -3;
    }
    nBandIn = 1;
    hBand = GDALGetRasterBand( hSrcDS, nBandIn );
    if( hBand == NULL )
    {
        CPLError( CE_Failure, CPLE_AppDefined,
                  "Band %d does not exist on dataset.",
                  nBandIn );
        // 源文件输入有误
        return -3;
    }
    if( !bNoDataSet && !bIgnoreNoData )
    {
        dfNoData = GDALGetRasterNoDataValue( hBand, &bNoDataSet );
    }

    /* -------------------------------------------------------------------- */
    /*      Try to get a coordinate system from the raster.                 */
    /* -------------------------------------------------------------------- */
    OGRSpatialReferenceH hSRS = NULL;

    const SCHAR *pszWKT = GDALGetProjectionRef( hSrcDS );

    if( pszWKT != NULL && strlen(pszWKT) != 0 )
    {
        hSRS = OSRNewSpatialReference( pszWKT );
    }
    /* -------------------------------------------------------------------- */
    /*      Create the outputfile.                                          */
    /* -------------------------------------------------------------------- */
    OGRDataSourceH hDS;
    OGRSFDriverH hDriver = OGRGetDriverByName( pszFormat );
    OGRFieldDefnH hFld;
    OGRLayerH hLayer;

    hDS = OGR_Dr_CreateDataSource( hDriver, pszDstFilename, NULL );
    if( hDS == NULL )
    {
        // 目的文件输入有误
        return -4;
    }


    hLayer = OGR_DS_CreateLayer( hDS, pszNewLayerName, hSRS,
                                 b3D ? wkbLineString25D : wkbLineString,
                                 NULL );
    if( hLayer == NULL )
    {
        // 目的文件输有误
        return -4;
    }

    hFld = OGR_Fld_Create( "ID", OFTInteger );
    OGR_Fld_SetWidth( hFld, 8 );
    OGR_L_CreateField( hLayer, hFld, FALSE );
    OGR_Fld_Destroy( hFld );

    if( pszElevAttrib )
    {
        hFld = OGR_Fld_Create( pszElevAttrib, OFTReal );
        OGR_Fld_SetWidth( hFld, 12 );
        OGR_Fld_SetPrecision( hFld, 3 );
        OGR_L_CreateField( hLayer, hFld, FALSE );
        OGR_Fld_Destroy( hFld );
    }

    /* -------------------------------------------------------------------- */
    /*      Invoke.                                                         */
    /* -------------------------------------------------------------------- */
    CPLErr eErr;

    eErr = GDALContourGenerate( hBand, dfInterval, dfOffset,
                                nFixedLevelCount, adfFixedLevels,
                                bNoDataSet, dfNoData, hLayer,
                                OGR_FD_GetFieldIndex( OGR_L_GetLayerDefn( hLayer ),
                                        "ID" ),
                                (pszElevAttrib == NULL) ? -1 :
                                OGR_FD_GetFieldIndex( OGR_L_GetLayerDefn( hLayer ),
                                        pszElevAttrib ),
                                pfnProgress, NULL );

    OGR_DS_Destroy( hDS );
    GDALClose( hSrcDS );

    if (hSRS)
    {
        OSRDestroySpatialReference( hSRS );
    }
    GDALDestroyDriverManager();
    OGRCleanupAll();
    return 1;
}
















