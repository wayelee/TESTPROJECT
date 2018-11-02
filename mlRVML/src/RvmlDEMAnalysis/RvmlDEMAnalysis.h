#ifndef _MLRVML_H_
#define _MLRVML_H_

#include "mlTypes.h"

/**
* @fn mlComputeInsightMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口，根据输入DEM数据和视点坐标，计算通视图
* @param sInputDEM,  输入DEM文件路径
* @param nxLocation, 视点x坐标
* @param nyLocation, 视点y坐标
* @param ViewHight，  视点距离地面的高度
* @param sDestDEM，   输出文件路径
* @param InverseHeight， 是否将高程反转
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeInsightMap( const SCHAR * sInputDEM, SINT nxLocation ,SINT nyLocation, DOUBLE dViewHight, const SCHAR * sDestDEM, bool InverseHeight );

/**
* @fn mlComputeSlopeMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，输出坡度
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeSlopeMap(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor);
/**
* @fn mlComputeSlopeAspectMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，输出坡向
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeSlopeAspectMap(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor );
/**
* @fn mlComputeBarrierMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据，和计算窗口大小，障碍图参数计算障碍图
* @param sInputDEM,  输入DEM文件路径
* @param nWindowSize, 计算窗口大小
* @param sDestDEM, 输出文件路径
* @param dZfactor： 高程缩放因子，即从DEM取出来的值乘以 dZfactor 为真实高程值
* @param ObPara: 障碍图参数结构体
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeBarrierMap(SCHAR * sInputDEM, SCHAR * sDestDEM, SINT nWindowSize ,DOUBLE dZfactor,ObstacleMapPara ObPara);
/**
* @fn mlComputeContourMap
* @date 2011.11.02
* @author 李巍 liwei@irsa.ac.cn
* @brief 公开接口， 输入DEM数据和等高距，计算等高线图
* @param dHinterval,  等高距
* @param strSrcfilename, 输入的DEM文件路径
* @param strDstfilename, 输出的shape文件路径
* @param bCNodata， 表示是否自定义Nodata值
* @param dNodata， 如果bCNodata设置为true，则dNodata 的值在计算时被当做无效值对待
* @param cAttrib，生成shape文件高程的属性名称，默认为elev
 *@retval TRUE 成功
 *@retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
MLAPI( bool )  mlComputeContourMap(DOUBLE dHinterval, SCHAR* strSrcfilename, SCHAR* strDstfilename ,bool bCNodata = false , DOUBLE dNodata = 0.0 ,SCHAR* strAttrib = "elev" );

#endif


