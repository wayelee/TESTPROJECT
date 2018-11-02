/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlGdalDataset.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief GDAL图像基础操作头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/

#ifndef CGDALDATASET_H
#define CGDALDATASET_H

#include "mlFileRaster.h"
/**
* @struct ImageSqlTYPE
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 图像波段排列方式
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef enum  {
	BSQ = 0,//!<BSQ排列方式
	BIP = 1,//!<BIP排列方式
	BIL = 2//!<BIL排列方式
}ImageSqlTYPE;
/**
* @struct geoImgTransTYPE
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 坐标变换信息
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
enum geoImgTransTYPE	{
	geoImgTranNONE,//!<无坐标变换
	geoImgTranOFFSETSCALE,//!<平移缩放坐标变换
	geoImgTranMATRIX//!<旋转矩阵坐标变换
};
/**
* @struct geoImgCacheINFO
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 栅格类头信息
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
struct geoImgCacheINFO	{
	SINT tile_wid;
	SINT tile_hei;

	SLONG imgWid;//!<影像宽
	SLONG imgHei;//!<影像高
	// same as raw image
	SSHORT samplesPerPixel;//!<每像素波段数
	// for DEM, 2 per sample
	// maybe different with raw, eg. 11 bit to 8 bit
	SSHORT bytesPerPixel;//!<每像素比特数
	// DPI
	SSHORT xResolution;//!<X方向分辨率
	// DPO
	SSHORT yResolution;//!<Y方向分辨率
    // use trfMatrix or tie point
	geoImgTransTYPE	transType;//!<影像变换类型

	/////// TIFFTAG_GEOTIEPOSINTS ///////
	DOUBLE i0;//!<影像点
	DOUBLE j0;//!<影像点
	DOUBLE k0;//!<影像点
	DOUBLE x0;//!<模型空间点
	DOUBLE y0;//!<模型空间点
	DOUBLE z0;//!<模型空间点
	// for dem
	DOUBLE xScale;//!<x方向缩放系数
	DOUBLE yScale;//!<y方向缩放系数
	DOUBLE zScale;//!<z方向缩放系数

	/////////// ModelTransformationTag /////////
	DOUBLE trfMatrix[4][4];	//!<旋转矩阵

    // if tranformed to 8 bit from 11 bit, this value also tranfromed
	DOUBLE	minSampleValue;
	// if tranformed to 8 bit from 11 bit, this value also tranfromed
	DOUBLE	maxSampleValue;
	DOUBLE	max_min;

	DOUBLE	noDataValue;//!<无数据区值
};
/**
* @class CmlGdalDataset
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 以GDAL为基础的栅格处理类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlGdalDataset : public CmlFileRaster
{
    public:
        /**
        * @fn CmlGdalDataset
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief CmlGdalDataset类空参构造函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        CmlGdalDataset();
        /**
        * @fn ~CmlGdalDataset
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief CmlGdalDataset类析构函数
        * @version 1.0
        * @return 无返回值
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        virtual ~CmlGdalDataset();

    public:

    public:
        virtual UINT GetBands() const {return m_nBands;};//!<得到影像波段数
        virtual UINT GetBytes() const {return m_nBytes;};//!<得到影像比特数
        virtual UINT GetWidth() const {return m_nWidth;};//!<得到影像宽
        virtual UINT GetHeight() const {return m_nHeight;};//!<得到影像高
        GDALDataset* GetGdalDataSet() {return m_pDataset;};//!<得到影像数据头指针

        /**
        * @fn LoadFile
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 载入栅格文件头信息
        * @param FileName 文件路径
        * @param nType 文件类型
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        virtual bool LoadFile( const SCHAR *FileName, const SINT nType=0) ;
        /**
        * @fn CreateFile
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 在磁盘中创建栅格文件
        * @param FileName 文件名称
        * @param nW 栅格影像宽
        * @param nH 栅格影像高
        * @param nBands 栅格影像波段数
        * @param GDTType 栅格影像生成类型
        * @param BitsOut 生成图像类型
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        virtual bool CreateFile( const SCHAR *FileName, const UINT nW,const UINT nH, const UINT nBands, const GDALDataType GDTType, const SCHAR *BitsOut);
        /**
        * @fn SaveBlockToFile
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 将block数据存入对应的磁盘文件
        * @param nBand 栅格影像波段号
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
        virtual bool SaveBlockToFile( const UINT nBand, const UINT nXOffSet, const UINT nYOffSet, CmlRasterBlock* pImgBlock, const UINT nBlockBand = 1);
        /**
        * @fn SaveBlockToFile
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 将block数据存入对应的磁盘文件
        * @param nBand 栅格影像波段号
        * @param nXOffSet 栅格影像X方向偏移量
        * @param nYOffSet 栅格影像Y方向偏移量
        * @param pImgBlock 栅格影像块
        * @param nBlockXOffSet 待存储数据块在block中X方向偏移量
        * @param nBlockYOffSet 待存储数据块在block中Y方向偏移量
        * @param nW 待存储数据块宽
        * @param nH 待存储数据块高
        * @param nBlockBand 目标文件波段号
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        bool SaveBlockToFile( const UINT nBand, const UINT nXOffSet, const UINT nYOffSet, CmlRasterBlock* pImgBlock, const UINT nBlockXOffSet, \
                                             const UINT nBlockYOffSet, const UINT nW, const UINT nH, const UINT nBlockBand = 1 );
        /**
        * @fn GetRasterGrayBlock
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 在GdalDataSet中得到某个波段某个位置下类型强制GDT_BYTE类型的影像块
        * @param nBand 影像波段号(以1开始)
        * @param nXOffSet 影像块X方向起点
        * @param nYOffSet 影像块Y方向起点
        * @param nW 影像块宽
        * @param nH 影像块高
        * @param nZoom 影像块缩放系数
        * @param pImgBlock 栅格影像块
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        bool GetRasterGrayBlock( const UINT nBand, const UINT nXOffSet, const UINT nYOffSet, const UINT nW, const UINT nH, const UINT nZoom,CmlRasterBlock* pImgBlock );
        /**
        * @fn GetRasterGrayBlock
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 在GdalDataSet中得到某个波段某个位置下类型强制GDT_BYTE类型的影像块
        * @param nBand 影像波段号(以1开始)
        * @param nXOffSet 影像块X方向起点
        * @param nYOffSet 影像块Y方向起点
        * @param nW 影像块宽
        * @param nH 影像块高
        * @param nZoom 影像块缩放系数
        * @param pImgBlock 栅格影像块
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        bool GetRasterGrayBlock( const UINT nBand, const UINT nXOffSet, const UINT nYOffSet, const UINT nW, const UINT nH, const DOUBLE dZoom,CmlRasterBlock* pImgBlock );
        /**
        * @fn GetRasterGrayBlock
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 在GdalDataSet中得到某三个波段（强制GDT_BYTE），将其当成R、G、B波段后合成block，block类型强制为GDT_BYTE，1波段
        * @param nBandR 影像块R波段所取影像波段号
        * @param nBandG 影像块G波段所取影像波段号
        * @param nBandB 影像块B波段所取影像波段号
        * @param nXOffSet 影像块X方向起点
        * @param nYOffSet 影像块Y方向起点
        * @param nW 影像块宽
        * @param nH 影像块高
        * @param nZoom 影像块缩放系数
        * @param pImgBlock 栅格影像块
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        bool GetRasterGrayBlock( const UINT nBandR, const UINT nBandG, const UINT nBandB, const UINT nXOffSet, const UINT nYOffSet, const UINT nW, const UINT nH, const UINT nZoom,CmlRasterBlock* pImgBlock );
        /**
        * @fn GetRasterRGBBlock
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 在GdalDataSet中得到某三个波段
        * @param nBandR 影像块R波段所取影像波段号
        * @param nBandG 影像块G波段所取影像波段号
        * @param nBandB 影像块B波段所取影像波段号
        * @param nXOffSet 影像块X方向起点
        * @param nYOffSet 影像块Y方向起点
        * @param nW 影像块宽
        * @param nH 影像块高
        * @param nZoom 影像块缩放系数
        * @param pImgBlock 栅格影像块
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        bool GetRasterRGBBlock( const UINT nBandR, const UINT nBandG, const UINT nBandB, const UINT nXOffSet, const UINT nYOffSet, const UINT nW, const UINT nH, const UINT nZoom,CmlRasterBlock* pImgBlock );
        /**
        * @fn GetRasterOriginBlock
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 在GdalDataSet中得到个波段，将其原始数据导入block中，block类型同DataSet相同
        * @param nBand 影像波段号(以1开始)
        * @param nXOffSet 影像块X方向起点
        * @param nYOffSet 影像块Y方向起点
        * @param nW 影像块宽
        * @param nH 影像块高
        * @param nZoom 影像块SINT型缩放系数
        * @param pImgBlock 栅格影像块
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        bool GetRasterOriginBlock( const UINT nBand, const UINT nXOffSet, const UINT nYOffSet, const UINT nW, const UINT nH, const UINT nZoom,CmlRasterBlock* pImgBlock );
        /**
        * @fn GetRasterOriginBlock
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 在GdalDataSet中得到个波段，将其原始数据导入block中，block类型同DataSet相同
        * @param nBand 影像波段号(以1开始)
        * @param nXOffSet 影像块X方向起点
        * @param nYOffSet 影像块Y方向起点
        * @param nW 影像块宽
        * @param nH 影像块高
        * @param dZoom 影像块DOUBLE型缩放系数
        * @param pImgBlock 栅格影像块
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        bool GetRasterOriginBlock( const UINT nBand, const UINT nXOffSet, const UINT nYOffSet, const UINT nW, const UINT nH, const DOUBLE dZoom,CmlRasterBlock* pImgBlock );
        /**
        * @fn GetNoDataVal
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 获取DataSet中的空值
        * @retval m_dNoDataValue 空值
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        DOUBLE GetNoDataVal() const { return m_dNoDataValue;};//!<得到DataSet中空值的代表值
        /**
        * @fn SetNoDataVal
        * @date 2011.11.02
        * @author 万文辉 whwan@irsa.ac.cn
        * @brief 设置DataSet中空值的代表值
        * @param dNoData 所需设定空值代表值
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史：
        * <作者>    <时间>   <版本编号>    <修改原因>\n
        */
        bool  SetNoDataVal( DOUBLE dNoData ) ;

        GDALDataType GetGDTType( ) const { return  m_dataType;};//!<得到数据类型

        bool m_bIsGeoFile;//!<判断是否为含有地理坐标的文件
    protected:


    private:
        UINT m_nBands;//!<影像波段数
        UINT m_nBytes;//!<影像存储位数
        UINT m_nWidth;//!<影像宽度
        UINT m_nHeight;//!<影像高度
        USHORT	m_bitsPerSample;//!<每波段的每像素占字节数，=m_dataType
        USHORT	*m_minSampleValues;
	    USHORT	*m_maxSampleValues;
        USHORT	*m_max_mins;
	    DOUBLE	m_dNoDataValue;//!<空值

        UINT m_tile_wid;
        UINT m_tile_hei;
        bool  m_isTiled;

        //8bit=1,16bit=2;
        GDALDataType	m_dataType;//!<单个像素所占Bit数
        GDALColorInterp m_colorInterp;//!<颜色存储类型
        geoImgCacheINFO	m_cacheInfo;//!<m_cacheInfo.samplesPerPixel为每像素波段数
        GDALDataset		*m_pDataset;//!<数据块头指针
        GDALRasterBand	**m_ppBands;//!<GDAL数据头指针

};
/**
* @struct ImgPt
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 影像点匹配结构
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
struct ImgPt: public Pt2d
{
    //影像点匹配结构,存储了点号、点的二维坐标与图像指针
    CmlGdalDataset* pImg;  //!<影像点所在影像指针
    ImgPt()
    {
        pImg = NULL;
    }
};


#endif // CGDALDATASET_H
