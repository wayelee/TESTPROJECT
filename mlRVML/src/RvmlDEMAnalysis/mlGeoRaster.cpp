/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlGeoRaster.cpp
* @date 2011.11.21
* @author 万文辉 whwan@irsa.ac.cn
* @brief 地理栅格类实现源文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/

#include "mlGeoRaster.h"
/**
 *@fn CmlGeoRaster
 *@date 2011.11
 *@author 万文辉
 *@brief CmlGeoRaster类构造函数
 *@version 1.0
 *@par 修改历史：
 <作者>    <时间>   <版本编号>    <修改原因>\n
 */
CmlGeoRaster::CmlGeoRaster()
{
    //ctor
    for( int i = 0; i < 6; ++i )
    {
        m_dGdalTransPara[i] = 0;
    }
    m_dXResolution = m_dYResolution = 1.0;
    m_PtOrigin.X = m_PtOrigin.Y = 0;
}
/**
 *@fn ～CmlGeoRaster
 *@date 2011.11
 *@author 万文辉
 *@brief CmlGeoRaster类析构函数
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
 */
CmlGeoRaster::~CmlGeoRaster()
{
    //dtor
}
/**
 *@fn ASCIIDemToGeoTiff
 *@date 2011.11
 *@author 万文辉
 *@brief ASCII格式DEM转为GeoTiff
 *@param strPathASCII ASCII格式DEM路径
 *@param strOutPathGeoTiff 转换后GeoTiff文件路径
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
 */
bool CmlGeoRaster::ASCIIDemToGeoTiff( char* strPathASCII, char* strOutPathGeoTiff )
{
    fstream stm;
    stm.open( strPathASCII );
    if(!stm)
    {
        return false;
    }

    string sTemp;
    int nCol, nRow;
    double dResolution, dNoData, dLX, dLY;
    stm >> sTemp >> nCol >> sTemp >> nRow ;
    stm >> sTemp >> dLX >> sTemp >> dLY ;
    stm >> sTemp >> dResolution >> sTemp >> dNoData ;

    double* pdZVal = new double[nRow*nCol];

    for( int i = 0; i < nRow; ++i )
    {
        for( int j = 0; j < nCol; ++j )
        {
            double* pdZ = &pdZVal[i*nCol+j];
            double dZ;
            stm >> dZ;
            *pdZ = dZ;
        }
    }
    Pt2d ptLL;
    ptLL.X = dLX;
    ptLL.Y = dLY + dResolution*(nRow-1);

    CmlGeoRaster dem;

    dem.CreateGeoFile( strOutPathGeoTiff, ptLL, dResolution, -dResolution, nRow, nCol, 1, GDT_Float64, dNoData );
    dem.SaveToGeoFile( pdZVal );

    delete[] pdZVal;
    return true;

}
/**
 *@fn GeoTiffToASCIIDem
 *@date 2011.11
 *@author 万文辉
 *@brief GeoTiff格式DEM转为ASCII格式
 *@param strOutPathGeoTiff GeoTiff文件路径
 *@param strPathASCII 转换后ASCII格式DEM路径
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
 */
bool CmlGeoRaster::GeoTiffToASCIIDem( char* strOutPathGeoTiff, char* strPathASCII )
{
    return true;
}
/**
 *@fn LoadGeoFile
 *@date 2011.11
 *@author 万文辉
 *@brief 载入带地理坐标的栅格数据
 *@param sPath 载入文件路径
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
 */
bool CmlGeoRaster::LoadGeoFile( const SCHAR* sPath )
{
    if ( false == this->LoadFile( sPath ))
    {
        return false;
    }
    m_bIsGeoFile = true;
    GetGdalDataSet()->GetGeoTransform( m_dGdalTransPara );
    m_dXResolution = m_dGdalTransPara[1];
    m_dYResolution = m_dGdalTransPara[5];
    m_PtOrigin.X = m_dGdalTransPara[0];
    m_PtOrigin.Y = m_dGdalTransPara[3];

    return true;

}
/**
 *@fn CreateGeoFile
 *@date 2011.11
 *@author 万文辉
 *@brief 创建带地理坐标的栅格数据硬盘文件
 *@param sPath 创建文件路径
 *@param ptLL 左上角坐标
 *@param dXResolution X方向分辨率
 *@param dYResolution Y方向分辨率
 *@param nH 生成图像高
 *@param nW 生成图像宽
 *@param nBands 波段数
 *@param GDTType 栅格类型
 *@param dNoData 空值
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
bool CmlGeoRaster::CreateGeoFile( const SCHAR* sPath, Pt2d ptLL, double dXResolution,double dYResolution, int nH, int nW, int nBands, GDALDataType GDTType, double dNoData )
{
    if( GetGdalDataSet() != NULL )
    {
        return false;
    }
    if( false == this->CreateFile( sPath, nW, nH, nBands, GDTType, "GTiff" ) )
    {
        return false;
    }
    if( GetGdalDataSet() == NULL )
    {
        return false;
    }
    m_dXResolution = dXResolution;
    m_dYResolution = dYResolution;
    m_PtOrigin = ptLL;

    m_dGdalTransPara[1] = m_dXResolution;
    m_dGdalTransPara[5] = m_dYResolution;
    m_dGdalTransPara[0] = m_PtOrigin.X;
    m_dGdalTransPara[3] = m_PtOrigin.Y;

    GetGdalDataSet()->SetGeoTransform( m_dGdalTransPara );
    SetNoDataVal(dNoData);
    return true;
}
/**
 *@fn
 *@date 2011.11
 *@author 万文辉
 *@brief 将点坐标存入Gdal文件中
 *@param vecZVal 左上角、行顺序存储的Z值数组
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
bool CmlGeoRaster::SaveToGeoFile( vector<double> &vecZVal )
{
    //为了由点直接生成DEM。注意，所写入的硬盘文件暂定为double型TIFF
    if( vecZVal.size() != (UINT) GetHeight()*GetWidth() )
    {
        return false;
    }
    CmlRasterBlock block;
    int nW = GetWidth();
    int nH = GetHeight();
    block.InitialImg( nH, nW, 8 );
    block.SetGDTType( GDT_Float64 );
    for( int i = 0; i < nH; ++i )
    {
        for( int j = 0; j < nW; ++j )
        {
            block.SetPtrAt( i, j, (BYTE*)(&vecZVal[i*nW+j]));
        }
    }

    this->SaveBlockToFile( 1, 0, 0, &block, 1 );
    return true;
}
/**
 *@fn
 *@date 2011.11
 *@author 万文辉
 *@brief 将点坐标存入Gdal文件中
 *@param 左上角、行顺序存储的Z值数组
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
bool CmlGeoRaster::SaveToGeoFile( double* pZVal )
{
    //为了由点直接生成DEM。注意，所写入的硬盘文件暂定为double型TIFF
    CmlRasterBlock block;
    int nW = GetWidth();
    int nH = GetHeight();
    block.InitialImg( nH, nW, 8 );
    block.SetGDTType( GDT_Float64 );
    for( int i = 0; i < nH; ++i )
    {
        for( int j = 0; j < nW; ++j )
        {
            block.SetPtrAt( i, j, (BYTE*)(pZVal+i*nW+j));
        }
    }

//    double dVal ;
//    for( int i = 0; i < nH; ++i )
//    {
//        for( int j = 0; j < nW; ++j )
//        {
//            block.GetDoubleVal( i, j , dVal);
//            cout << dVal << "   ";
//        }
//    }

    this->SaveBlockToFile( 1, 0, 0, &block, 1 );
    return true;
}
/**
 *@fn
 *@date 2011.11
 *@author 万文辉
 *@brief 将Block数据存入Gdal文件中.
 *@param nBand 写入波段号
 *@param nXOffSet 写入文件起始X坐标
 *@param nYOffSet 写入文件其实Y坐标
 *@param pBlock 写入数据
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
 */
bool CmlGeoRaster::SaveToGeoFile(int nBand, int nXOffSet, int nYOffSet, CmlRasterBlock* pBlock )
{
    //注意，Block类型应该同硬盘文件类型相同，否则会出错
    this->SaveBlockToFile( nBand, nXOffSet, nYOffSet, pBlock, 1 );
    return true;
}


