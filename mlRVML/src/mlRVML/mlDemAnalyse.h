/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlDemAnalyse.h
* @date 2011.11.18
* @author 李巍 liwei@irsa.ac.cn
* @brief  地形分析模块类头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef CMLDEMANALYSE_H
#define CMLDEMANALYSE_H
#include "mlGdalDataset.h"
#include "mlRasterBlock.h"
#include "mlGeoRaster.h"
#include "mlBlockCalculation.h"

#include "gdal_header.h"
#include "gdal/gdal.h"
#include "gdal/gdal_alg.h"
#include "gdal/cpl_conv.h"
#include "gdal/cpl_string.h"
#include "gdal/ogr_api.h"
#include "gdal/ogr_srs_api.h"

/**
* @struct POINTRASTER
* @date 2011.11.18
* @author 李巍 liwei@irsa.ac.cn
* @brief 存取地形数据点结构
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/
struct POINTRASTER
{
        SLONG lx;
        SLONG ly;
        DOUBLE dz;
};

/**
* @class CmlDemAnalyse
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 地形分析处理类
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/
class CmlDemAnalyse
{
    public:
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
        CmlDemAnalyse();
        //CmlDemAnalyse(CmlGdalDataset* pSrcDataset);
       //CmlDemAnalyse(CmlRasterBlock* pSreRasterBlock);
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
        virtual ~CmlDemAnalyse();

    /**
    * @fn ComputeViewShedInterface
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 公开接口 根据输入DEM数据和视点坐标，计算通视图
    * @param sInputDEM 输入DEM文件路径
    * @param nxLocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param ViewHight 视点距离地面的高度
    * @param sDestDEM 输出文件路径
    * @param InverseHeight 是否将高程反转
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeViewShedInterface( const SCHAR * sInputDEM, SINT nxLocation ,SINT nyLocation, DOUBLE dViewHight, const SCHAR * sDestDEM , bool InverseHeight = false);

    /**
    * @fn ComputeSlopeInterface
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 公开接口 输入DEM数据，和计算窗口大小，输出坡度
    * @param sInputDEM 输入DEM文件路径
    * @param nWindowSize 计算窗口大小
    * @param sDestDEM 输出文件路径
    * @param dZfactor 高程缩放因子，即从DEM取出来的值乘以dZfactor为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT ComputeSlopeInterface(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor );

    /**
    * @fn ComputeSlopeAspectInterface
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 公开接口 输入DEM数据，和计算窗口大小，输出坡向
    * @param sInputDEM  输入DEM文件路径
    * @param nWindowSize 计算窗口大小
    * @param sDestDEM 输出文件路径
    * @param dZfactor 高程缩放因子，即从DEM取出来的值乘以dZfactor为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeSlopeAspectInterface(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor);

    /**
    * @fn ComputeObstacleMapInterface
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 公开接口 输入DEM数据，和计算窗口大小，障碍图参数计算障碍图
    * @param sInputDEM  输入DEM文件路径
    * @param nWindowSize 计算窗口大小
    * @param sDestDEM 输出文件路径
    * @param dZfactor 高程缩放因子，即从DEM取出来的值乘以dZfactor为真实高程值
    * @param ObPara 障碍图参数结构体
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeObstacleMapInterface(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor,ObstacleMapPara ObPara);

    /**
    * @fn ComputeContourInterface
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 公开接口， 输入DEM数据和等高距，计算等高线图
    * @param dHinterval  等高距
    * @param strSrcfilename 输入的DEM文件路径
    * @param strDstfilename 输出的shape文件路径
    * @param bCNodata 表示是否自定义Nodata值
    * @param dNodata 如果bCNodata设置为true，则dNodata 的值在计算时被当做无效值对待
    * @param cAttrib生成shape文件高程的属性名称，默认为elev
    * @retval 1 成功
    * @retval -1 失败，gdal版本过低
    * @retval -2 失败，等高距设置有误
    * @retval -3 失败，输入文件有误
    * @retval -4 失败，输出文件有误
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeContourInterface(DOUBLE dHinterval,   SCHAR* strSrcfilename,   SCHAR* strDstfilename ,bool bCNodata = FALSE, DOUBLE dNodata = 0.0,  SCHAR* cAttrib = "elev");

    protected:
    private:
    // 计算直线方向通视
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
        SINT ComputeNorthViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
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
        SINT ComputeSouthViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn ComputeEastViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算东方向上的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeEastViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn ComputeWestViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算西方向上的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut 计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeWestViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn ComputeNorthEastViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算东北方向上的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut 计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeNorthEastViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn ComputeSouthEastViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算东南方向上的通视域
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
        SINT ComputeSouthEastViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn ComputeNorthWestViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算西北方向上的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight  视点距离地面的坐标
    * @param pBlockOut 计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeNorthWestViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn ComputeSouthWestViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算西南方向上的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeSouthWestViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn ComputeEightDirectionViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算八个方向上的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeEightDirectionViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);

     //计算象限通视
    /**
    * @fn Compute1stQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第一象限的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT Compute1stQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn Compute2ndQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第二象限的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight  视点距离地面的坐标
    * @param pBlockOut 计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT Compute2ndQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn Compute3rdQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第三象限的通视域
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
        SINT Compute3rdQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn Compute4thQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第四象限的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut 计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT Compute4thQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn Compute5thQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第五象限的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT Compute5thQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn Compute6thQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第六象限的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT Compute6thQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn Compute7thQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第七象限的通视域
    * @param nxlocation 视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut  计算的可视矩阵，传入像素类型为GDT_Byte;
    * @param VisibleHeightBloc 计算的可视参考高度，传入像素类型为GDT_Float32
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT Compute7thQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn Compute8thQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算第八象限的通视域
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
        SINT Compute8thQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);
    /**
    * @fn ComputeEightQuadrantViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算八个象限的通视域
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
        SINT ComputeEightQuadrantViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight ,CmlRasterBlock* pBlockOut , CmlRasterBlock* pVisibleHeightBloc);

    /**
    * @fn ComputeZValueInReferencePlan
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据三个参考点确定的平面，计算水平位置在平面投影上的高程值
    * @param nx1 第1点的x坐标
    * @param ny1 第1点的y坐标
    * @param dz1 第1点的z坐标
    * @param nx2 第2点的x坐标
    * @param ny2 第2点的y坐标
    * @param dz2 第2点的z坐标
    * @param nx3 第3点的x坐标
    * @param ny3 第3点的y坐标
    * @param dz3 第3点的z坐标
    * @param nx 计算点的x坐标
    * @param ny 计算点的y坐标
    * @param dz 计算点的z坐标
    * @retval true 成功
    * @retval false 失败，三点共线
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        bool ComputeZValueInReferencePlan(SINT nx1, SINT ny1, DOUBLE dz1,
                                            SINT nx2, SINT ny2, DOUBLE dz2,
                                            SINT nx3, SINT ny3, DOUBLE dz3,
                                            SINT nx, SINT ny, DOUBLE &dz);
    /**
    * @fn ComputeViewShed
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 根据输入的处于rasterblock上的坐标以及视点的高度计算通视域
    * @param nxlocation  视点x坐标
    * @param nyLocation 视点y坐标
    * @param dViewHight 视点距离地面的坐标
    * @param pBlockOut 计算出的可视矩阵
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeViewShed(SINT nxlocation,SINT nyLocation, DOUBLE dViewHight, CmlRasterBlock* &pBlockOut);

    /**
    * @fn ComputeSlopeBlock
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 输入DEM数据块,和计算窗口大小，输出坡度数据块
    * @param pDEMBlock 输入DEM数据块
    * @param nWindowSize 计算窗口大小
    * @param pResultBlock 用于输出坡度数据块
    * @param dGridResolution 格网分辨率
    * @param dZfactor 高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT ComputeSlopeBlock(CmlRasterBlock* pDEMBlock, SINT nWindowSize, CmlRasterBlock* pResultBlock,DOUBLE dGridResolution, DOUBLE dZfactor);

    /**
    * @fn FitPlaneByMatrix
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 输入矩阵，计算拟合平面的参数
    * @param matrix 输入的矩阵值
    * @param Acoef 表示 平面 Ax + By + C = z的X系数，其坐标原点为矩阵中心点
    * @param Bcoef 表示 平面 Ax + By + C = z的Y系数，其坐标原点为矩阵中心点
    * @param Ccoef 表示 平面 Ax + By + C = z的C，其坐标原点为矩阵中心点
    * @param dGridResolution 格网分辨率
    * @param dZfactor 高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT FitPlaneByMatrix(CmlMat* pMatrix, DOUBLE& dAcoef, DOUBLE& dBcoef , DOUBLE& dCcoef ,DOUBLE dGridResolution,DOUBLE dZfactor);


    /**
    * @fn ComputeRoughnessValue
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 输入矩阵，拟合平面的参数,计算中心点的roughness值
    * @param matrix 输入的矩阵值
    * @param Acoef 表示 平面 Ax + By + C = z的X系数，其坐标原点为矩阵中心点
    * @param Bcoef 表示 平面 Ax + By + C = z的Y系数，其坐标原点为矩阵中心点
    * @param Ccoef 表示 平面 Ax + By + C = z的C，其坐标原点为矩阵中心点
    * @param dGridResolution 格网分辨率
    * @param dRoughnessValue 计算得到的粗糙度值
    * @param dZfactor 高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
        SINT ComputeRoughnessValue(CmlMat* pMatrix, DOUBLE dAcoef, DOUBLE dBcoef, DOUBLE dCcoef ,DOUBLE dGridResolution, DOUBLE& dRoughnessValue, DOUBLE dZfactor);



    /**
    * @fn ComputeStepValue
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief  输入矩阵，计算最大值与最小值之差
    * @param matrix 输入的矩阵值
    * @param dDiffZ 计算得到的高程差
    * @param dZfactor 高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT ComputeStepValue(CmlMat* pMatrix, DOUBLE& dDiffZ, DOUBLE dZfactor);


    /**
    * @fn ComputeSlope
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 输入DEM数据，和计算窗口大小，输出坡度
    * @param pSrcDataset 输入dem数据
    * @param nWindowSize 计算窗口大小
    * @param pDestDataset 用于输出坡度数据
    * @param dGridResolution 格网分辨率
    * @param dZfactor 高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT ComputeSlope(CmlGdalDataset* pSrcDataset, SINT nWindowSize, CmlGdalDataset* pDestDataset,DOUBLE dGridResolution, DOUBLE dZfactor);


    /**
    * @fn ComputeSlopeAspectBlock
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief   输入DEM数据块，和计算窗口大小，输出坡向数据块
    * @param pDEMBlock 输入DEM数据块
    * @param nWindowSize 计算窗口大小
    * @param pResultBlock 用于输出坡向数据块
    * @param dGridResolution 格网分辨率
    * @param dZfactor 高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT ComputeSlopeAspectBlock(CmlRasterBlock* pDEMBlock, SINT nWindowSize, CmlRasterBlock* pResultBlock,DOUBLE dGridResolution, DOUBLE dZfactor);

    /**
    * @fn ComputeSlopeAspect
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief 输入DEM数据，和计算窗口大小，输出坡向
    * @param pSrcDataset 输入dem数据
    * @param nWindowSize 计算窗口大小
    * @param pDestDataset 输出坡向数据
    * @param dGridResolution 格网分辨率
    * @param dZfactor  高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT ComputeSlopeAspect(CmlGdalDataset* pSrcDataset, SINT nWindowSize, CmlGdalDataset* pDestDataset, DOUBLE dGridResolution, DOUBLE dZfactor);

    /**
    * @fn ComputeObstacleMapBlock
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief  输入DEM数据块，和计算窗口大小，输出障碍图数据块
    * @param pDEMBlock 输入DEM数据块
    * @param nWindowSize 计算窗口大小
    * @param pResultBlock 障碍图数据块
    * @param dGridResolution 格网分辨率
    * @param dZfactor 高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @param ObPara，障碍图参数结构体
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT ComputeObstacleMapBlock(CmlRasterBlock* pDEMBlock, SINT nWindowSize, CmlRasterBlock* pResultBlock, DOUBLE dGridResolution, DOUBLE dZfactor,
                                     ObstacleMapPara ObPara);

    /**
    * @fn ComputeObstacleMap
    * @date 2011.11.02
    * @author 李巍 liwei@irsa.ac.cn
    * @brief  输入DEM数据，和计算窗口大小，输出障碍图
    * @param pSrcDataset 输入dem数据
    * @param nWindowSize 计算窗口大小
    * @param pDestDataset 输出障碍图数据
    * @param dGridResolution 格网分辨率
    * @param dZfactor 高程缩放因子，即 从DEM取出来的值 乘以 dZfactor 为真实高程值
    * @param ObPara，障碍图参数结构体
    * @retval 1 成功
    * @retval 其他 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */

        SINT ComputeObstacleMap(CmlGdalDataset* pSrcDataset, SINT nWindowSize, CmlGdalDataset* pDestDataset, DOUBLE dGridResolution, DOUBLE dZfactor,
                                               ObstacleMapPara ObPara);

        //用于计算通视分析计算的栅格数据块
        CmlRasterBlock* m_pSrcRasterBlock;

        //用于坡度破向及粗糙度的计算的栅格数据库
        CmlGdalDataset* m_pSrcDataset;


};

#endif // CMLDEMANALYSE_H
