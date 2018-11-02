/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlGeoRaster.h
* @date 2011.11.21
* @author 万文辉 whwan@irsa.ac.cn
* @brief 地理栅格类实现头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CMLGEORASTER_H
#define CMLGEORASTER_H

#include "mlGdalDataset.h"
//#define  DEM_BLOCK_SIZE 40000000
#define  DEM_BLOCK_SIZE 20000000

class CmlGeoRaster : public CmlGdalDataset
{
public:
    /**
     *@fn CmlGeoRaster()
     *@date 2011.11
     *@author 万文辉
     *@brief CmlGeoRaster类构造函数
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    CmlGeoRaster();
    /**
     *@fn ～CmlGeoRaster
     *@date 2011.11
     *@author 万文辉
     *@brief CmlGeoRaster类析构函数
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    virtual ~CmlGeoRaster();
    double m_dXResolution ; //!<X方向分辨率
    double m_dYResolution ;//!< Y方向分辨率
    //一般而言，Y方向分辨率为负数，且绝对值等于X方向分辨率
    Pt2d m_PtOrigin ;         //!<左上角原点坐标
public:
    /**
     *@fn ASCIIDemToGeoTiff()
     *@date 2011.11
     *@author 万文辉
     *@brief ASCII格式DEM转为GeoTiff
     *@param strPathASCII ASCII格式DEM路径
     *@param strOutPathGeoTiff 转换后GeoTiff文件路径
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool ASCIIDemToGeoTiff( char* strPathASCII, char* strOutPathGeoTiff );
    /**
     *@fn GeoTiffToASCIIDem()
     *@date 2011.11
     *@author 万文辉
     *@brief GeoTiff格式DEM转为ASCII格式
     *@param strOutPathGeoTiff GeoTiff文件路径
     *@param strPathASCII 转换后ASCII格式DEM路径
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool GeoTiffToASCIIDem( char* strOutPathGeoTiff, char* strPathASCII );
    /**
     *@fn LoadGeoFile
     *@date 2011.11
     *@author 万文辉
     *@brief 载入带地理坐标的栅格数据
     *@param sPath 载入文件路径
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool LoadGeoFile( const SCHAR* sPath );
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
    bool CreateGeoFile( const SCHAR* sPath, Pt2d ptLL, DOUBLE dXResolution,DOUBLE dYResolution, SINT nH, SINT nW, SINT nBands, GDALDataType GDTType, double dNoData );
    /**
     *@fn SaveToGeoFile
     *@date 2011.11
     *@author 万文辉
     *@brief 将点坐标存入Gdal文件中
     *@param vecZVal 左上角、行顺序存储的Z值数组
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool SaveToGeoFile( vector<double> &vecZVal );
    /**
     *@fn SaveToGeoFile()
     *@date 2011.11
     *@author 万文辉
     *@brief 将点坐标存入Gdal文件中，为了由点直接生成DEM。注意，所写入的硬盘文件暂定为double型TIFF
     *@param 左上角、行顺序存储的Z值数组
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool SaveToGeoFile( double* pZVal );
    /**
     *@fn SaveToGeoFile
     *@date 2011.11
     *@author 万文辉
     *@brief 将Block数据存入Gdal文件中.注意，Block类型应该同硬盘文件类型相同，否则会出错
     *@param nBand 写入波段号
     *@param nXOffSet 写入文件起始X坐标
     *@param nYOffSet 写入文件其实Y坐标
     *@param pBlock 写入数据
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool SaveToGeoFile( int nBand, int nXOffSet, int nYOffSet, CmlRasterBlock* pBlock );

    bool GetGeoInfo( const SCHAR* strGeoFile, Pt2d &ptOrig, DOUBLE &dXRes, DOUBLE &dYRes, UINT &nW, UINT &nH, DOUBLE &dNoDataVal );

    bool SetGeoInfo( const SCHAR* strGeoFile, Pt2d ptOrig, DOUBLE dXRes, DOUBLE dYRes, DOUBLE dNoDataVal );
protected:
private:
    double m_dGdalTransPara[6];
};

#endif // CMLGEORASTER_H
