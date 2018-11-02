/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlTypeConvert.cpp
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 类型转换源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#include "mlTypeConvert.h"
/**
* @fn IplImage2CmlRBlock
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 将IplImage型变量转化成CmlSBlock型变量
* @param pIplImg  IplImage型变量
* @param pSBlock  CmlSBlock型变量
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool IplImage2CmlRBlock( const IplImage* pIplImg, CmlRasterBlock* pSBlock )
{
    if(( pIplImg == NULL)||( pSBlock == NULL ) )
    {
        return false;
    }
    if( pSBlock->IsValid() == true )
    {
        if( ( SINT(pSBlock->GetH()) != pIplImg->height ) || ( SINT(pSBlock->GetW()) != pIplImg->width ) )
        {
            return false;
        }
    }
    else
    {
        pSBlock->InitialImg( pIplImg->height, pIplImg->width );
    }

    if( 0 == (pIplImg->width % 4 ) ) //为列宽四的倍数，则图像中不需要字节补齐
    {
        if( pIplImg->origin == 0 )//为顶左结构，同pSBlock 相同
        {
            memcpy(  (void*)(pSBlock->GetData()), (void*)(pIplImg->imageData ), sizeof(BYTE)*pSBlock->GetTPixelSize() );

        }
        else//为底左结构，同pSBlock 不同
        {
            for( UINT i = 0; i < pSBlock->GetH(); ++i )
            {
                for( UINT j = 0; j < pSBlock->GetW(); ++j )
                {
                    memcpy( (void*)(pSBlock->GetData() + i*pSBlock->GetW()), (void*)(pIplImg->imageData + ( pIplImg->height - 1 - i )*pIplImg->widthStep ), sizeof(uchar)*pSBlock->GetW() );
                }
            }
        }
    }
    else//不为列宽四的倍数，则图像中存在字节补齐，转换时需要去除
    {
        if( pIplImg->origin == 0 )//为顶左结构，同pSBlock 定义相同
        {
            for( UINT i = 0; i < pSBlock->GetH(); ++i )
            {
                for( UINT j = 0; j < pSBlock->GetW(); ++j )
                {
                    memcpy( (void*)(pSBlock->GetData() + i*pSBlock->GetW()), (void*)(pIplImg->imageData + i*pIplImg->widthStep ), sizeof(uchar)*pSBlock->GetW() );
                }
            }

        }
        else//为底左结构，同pSBlock 不定义相同
        {
            for( UINT i = 0; i < pSBlock->GetH(); ++i )
            {
                for( UINT j = 0; j < pSBlock->GetW(); ++j )
                {
                    memcpy( (void*)(pSBlock->GetData() + i*pSBlock->GetW()), (void*)(pIplImg->imageData + ( pSBlock->GetH() - 1 - i )*pIplImg->widthStep ), sizeof(uchar)*pSBlock->GetW() );
                }
            }
        }
    }
    return true;

}

/**
* @fn CmlSBlock2IplImg
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 将CmlSBlock型变量转化成IplImage型变量
* @param pSBlock  CmlSBlock型变量
* @param pIplImg  IplImage型变量
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlRBlock2IplImg( const CmlRasterBlock* pSBlock, IplImage* &pIplImg  )
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
    pIplImg->origin = 0; //设置为顶左结构

    if( 0 == (pIplImg->width % 4 ) ) //为列宽四的倍数，则图像中不需要字节补齐
    {
        memcpy(  (void*)(pIplImg->imageData ), (void*)(pSBlock->GetData()), sizeof(uchar)*pSBlock->GetTPixelSize() );
    }
    else//不为列宽四的倍数，则图像中存在字节补齐，转换时需要去除
    {
        for( UINT i = 0; i < pSBlock->GetH(); ++i )
        {
            for( UINT j = 0; j < pSBlock->GetW(); ++j )
            {
                memcpy( (void*)(pIplImg->imageData + i*pIplImg->widthStep ), (void*)(pSBlock->GetData() + i*pSBlock->GetW()), sizeof(uchar)*pSBlock->GetW() );
            }
        }
    }
    return true;

}




