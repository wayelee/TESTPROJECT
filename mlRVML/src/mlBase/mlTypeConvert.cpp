#include "../../include/mlTypeConvert.h"

/*****************************************************************
函数名称：CmlMat2CvMat
作    者：万文辉
功能描述：将CmlMat转化成CvMat
输    入：CmlMat* pmlMat
输    出：CvMat* pcvMat
版本编号：
修改历史： <作者>      <时间>      < 版本号>     <描述>
*****************************************************************/
bool mlMat2CvMat( CmlMat* pmlMat, CvMat* &pcvMat)
{
    if( ( pmlMat->GetH() != pcvMat->rows )||( pmlMat->GetW() != pcvMat->cols ) )
    {
        return false;
    }
    else
    {
        memcpy( (void*)pcvMat->data.ptr, (void*)pmlMat->GetData(), sizeof(double)*pmlMat->GetTSize() );
        return true;
    }

}

/*****************************************************************
函数名称：CvMat2mlMat
作    者：万文辉
功能描述：将CmlMat转化成CvMat
输    入：CvMat* pcvMat
输    出：CmlMat* pmlMat
版本编号：
修改历史： <作者>      <时间>      < 版本号>     <描述>
*****************************************************************/
bool CvMat2mlMat( CvMat* pcvMat, CmlMat* pmlMat )
{
    if( ( pmlMat->GetH() != pcvMat->rows )||( pmlMat->GetW() != pcvMat->cols ) )
    {
        return false;
    }
    else
    {
        memcpy( (void*)pmlMat->GetData(), (void*)pcvMat->data.ptr, sizeof(double)*pmlMat->GetTSize() );
        return true;
    }
}


/*****************************************************************
函数名称：IplImage2CmlSBlock
作    者：万文辉
功能描述：将IplImage转化成CmlSBlock
输    入：IplImage* pIplImg
输    出：CmlSBlock* pSBlock
版本编号：
修改历史： <作者>      <时间>      < 版本号>     <描述>
*****************************************************************/
bool IplImage2CmlSBlock( IplImage* pIplImg, CmlSBlock* pSBlock )
{
    if(( pIplImg == NULL)||( pSBlock == NULL ) )
    {
        return false;
    }
    if( pSBlock->IsValid() == true )
    {
        return false;
    }

    pSBlock->Initial( pIplImg->height, pIplImg->width );

    if( 0 == (pIplImg->width % 4 ) ) //为列宽四的倍数，则图像中不需要字节补齐
    {
        if( pIplImg->origin == 0 )//为顶左结构，同pSBlock 不相同
        {
            for( int i = 0; i < pSBlock->GetH(); ++i )
            {
                for( int j = 0; j < pSBlock->GetW(); ++j )
                {
                    memcpy( (void*)(pSBlock->GetData() + i*pSBlock->GetW()), (void*)(pIplImg->imageData + ( pSBlock->GetH() - 1 - i )*pIplImg->widthStep ), sizeof(uchar)*pSBlock->GetW() );
                }
            }
        }
        else//为底左结构，同pSBlock 相同
        {
            memcpy(  (void*)(pSBlock->GetData()), (void*)(pIplImg->imageData ), sizeof(uchar)*pSBlock->GetTSize() );
        }
    }
    else//不为列宽四的倍数，则图像中存在字节补齐，转换时需要去除
    {
        if( pIplImg->origin == 0 )//为顶左结构，同pSBlock 定义不同
        {
            for( int i = 0; i < pSBlock->GetH(); ++i )
            {
                for( int j = 0; j < pSBlock->GetW(); ++j )
                {
                    memcpy( (void*)(pSBlock->GetData() + i*pSBlock->GetW()), (void*)(pIplImg->imageData + ( pSBlock->GetH() - 1 - i )*pIplImg->widthStep ), sizeof(uchar)*pSBlock->GetW() );
                }
            }
        }
        else//为底左结构，同pSBlock 定义相同
        {
            for( int i = 0; i < pSBlock->GetH(); ++i )
            {
                for( int j = 0; j < pSBlock->GetW(); ++j )
                {
                    memcpy( (void*)(pSBlock->GetData() + i*pSBlock->GetW()), (void*)(pIplImg->imageData + i*pIplImg->widthStep ), sizeof(uchar)*pSBlock->GetW() );
                }
            }
        }
    }
    return true;

}

/*****************************************************************
函数名称：CmlSBlock2IplImg
作    者：万文辉
功能描述：将CmlSBlock转化成IplImage
输    入：CmlSBlock* pSBlock
输    出：IplImage* pIplImg ,代入为空值指针
版本编号：
修改历史： <作者>      <时间>      < 版本号>     <描述>
*****************************************************************/
bool CmlSBlock2IplImg( CmlSBlock* pSBlock, IplImage* &pIplImg  )
{
    if(( pIplImg != NULL)||( pSBlock == NULL ) )
    {
        return false;
    }
    if( ( pSBlock->IsValid() == false ) )
    {
        return false;
    };

    CvSize s;
    s.height = pSBlock->GetH();
    s.width = pSBlock->GetW();

    pIplImg = cvCreateImage( s, IPL_DEPTH_8U, 1 );
    pIplImg->origin = 1; //设置为底左结构

    if( 0 == (pIplImg->width % 4 ) ) //为列宽四的倍数，则图像中不需要字节补齐
    {
        memcpy(  (void*)(pIplImg->imageData ), (void*)(pSBlock->GetData()), sizeof(uchar)*pSBlock->GetTSize() );
    }
    else//不为列宽四的倍数，则图像中存在字节补齐，转换时需要去除
    {
        for( int i = 0; i < pSBlock->GetH(); ++i )
        {
            for( int j = 0; j < pSBlock->GetW(); ++j )
            {
                memcpy( (void*)(pIplImg->imageData + i*pIplImg->widthStep ), (void*)(pSBlock->GetData() + i*pSBlock->GetW()), sizeof(uchar)*pSBlock->GetW() );
            }
        }
    }
    return true;

}




