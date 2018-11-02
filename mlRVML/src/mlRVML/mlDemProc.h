/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlDemProc.h
* @date 2011.12.18
* @author 吴凯  wukai@irsa.ac.cn
* @brief  单站地形dem生成功能模块类头文件(包括多视角下地形生成)
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef MLDEMPROC_H
#define MLDEMPROC_H

#include "mlTypes.h"
#include "mlKringing.h"
#include "mlGeoRaster.h"

#define  DEM_NO_DATA  9999999999

class CmlDemProc //: public CmlRasterProc
{
public:
    vector<Pt3d> m_vec3DPts ;  // 3维点云
    vector<Pt3d> vecAddPts ;   // 空旷处补点
    vector<Pt2d> m_vecHoles ; // 单站空洞
    vector<Polygon3d> m_vecRegions;
    DbRect m_dbDemRect ;  // 指定DEM范围
    DOUBLE m_dDemRes ; //DEM分辨率
    ULONG  m_lPtsNum ; // 点云数量
    DbRect m_dbRectXY ; // 生成DEM的点云XY分布范围
    DbRect m_dbRectZ ; // 生成DEM的点云Z分布范围
    CmlTIN* m_pTin ;  // 三角网Tin索引
    SINT nNumTriangle ; // 三角网三角形个数
    triangulateio* m_pTri ; // DEM点云构建的Tin结构
    SINT m_nGlobalW ; // 全局DEM宽度
    SINT m_nGlobalH ; // 全局DEM高度
    SINT m_nW ; // DEM宽度
    SINT m_nH ; // DEM高度
    SINT  m_nBlockNum ; // 分块数
    DOUBLE m_dScale ; // 地理坐标缩放系数
public:
    /**
     *@fn CmlDemProc()
     *@date 2011.11
     *@author 吴凯
     *@brief DEM处理类构造函数
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    CmlDemProc();

    /**
     *@fn ～CmlDemProc()
     *@date 2011.11
     *@author 吴凯
     *@brief DEM处理类析构函数
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    virtual ~CmlDemProc();

    /**
     *@fn Resample()
     *@date 2011.11
     *@author 吴凯
     *@brief 重采样函数
     *@param nType 采样类型
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    virtual bool Resample(SINT nType);

    // 接口函数，
public:
    /**
    * @fn
    * @date 2011.11.02
    * @author 吴凯 wukai@irsa.ac.cn
    * @brief 三维点云生成指定范围DEM，并存入geotiff文件
    * @param vec3DPts 三维点云
    * @param dbDemRect 指定DEM生成范围
    * @param dRes DEM分辨率
    * @param strGeoFilePath 文件路径
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlWriteToGeoFile(vector<Pt3d>& vec3DPts , DbRect dbDemRect , DOUBLE dRes , const SCHAR* strGeoFilePath, ImgDotType imgType = T_Float64, string strTriFile = "" ) ;


    bool mlWriteRegionToGeoFile(vector<Pt3d>& vec3DPts , DbRect dbDemRect , DOUBLE dRes , vector<Polygon3d> &vecRegions, const SCHAR* strGeoFilePath, ImgDotType imgType = T_Float64, string strTriFile = "" ) ;
    /**
    * @fn
    * @date 2011.11.02
    * @author 吴凯 wukai@irsa.ac.cn
    * @brief 三维点云全部生成DEM，并存入geotiff文件
    * @param vec3DPts 三维点云
    * @param dRes DEM分辨率
    * @param strGeoFilePath 文件路径
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool mlWriteToGeoFile(vector<Pt3d>& vec3DPts , DOUBLE dRes , const SCHAR* strGeoFilePath ) ;

	bool mlWriteToGeoFile(vector<Pt3d>& vec3DPts , DOUBLE dRes , const SCHAR* strGeoFilePath, ImgDotType imgType, string strTriFile ) ;

private:
    /**
    * @fn mlGetRangeFromPts（）
    * @date 2011.11.02
    * @author 吴凯 wukai@irsa.ac.cn
    * @brief  根据点云及指定矩形范围输出dem范围
    * @param vec3DPts 三维点云
    * @param dbDemRect 指定矩形范围
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool getRangeFromPts(vector<Pt3d>& vec3DPts, DbRect dbDemRect) ; // 确定制定dem范围的参数信息
    /**
     * @fn mlGetRangeFromPts（）
     * @date 2011.11.02
     * @author 吴凯 wukai@irsa.ac.cn
     * @brief  根据点云输出dem范围
     * @param vec3DPts 三维点云
     * @retval TRUE 成功
     * @retval FALSE 失败
     * @version 1.0
     * @par 修改历史：
     * <作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool getRangeFromPts(vector<Pt3d>& vec3DPts) ;  // 确定dem参数信息
    /**
     * @fn mlGetRangeFromPts（）
     * @date 2011.11.02
     * @author 吴凯 wukai@irsa.ac.cn
     * @brief 根据3维点云得到规则格网dem的高程数据
     * @param pTin 3维点云对应的Tin
     * @param *pKring 克里金插值类
     * @param dbDemRect dem分布范围
     * @param nW 格网dem列数
     * @param nH 格网dem行数
     * @param *pDem 规则格网dem高程数据
     * @retval TRUE 成功
     * @retval FALSE 失败
     * @version 1.0
     * @par 修改历史：
     * <作者>    <时间>   <版本编号>    <修改原因>\n
     */
//    bool mlCreateRasterFrom3DPts() ;
    bool createRasterFrom3DPts(CmlTIN* pTin , CmlKringing* pKring , DbRect dbDemRect , SINT nW , SINT nH , double* pDem) ;

    /**
     * @fn mlDelDulPts()
     * @date 2011.11.02
     * @author 吴凯 wukai@irsa.ac.cn
     * @brief 剔除点云过度密集的点
     * @param vec3DPts 三维点云
     * @retval TRUE 成功
     * @retval FALSE 失败
     * @version 1.0
     * @par 修改历史：
     * <作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool delDulPts(vector<Pt3d>& vec3DPts) ;

    bool delDulPtsByIdx(vector<Pt3d>& vec3DPts) ;

    /**
     * @fn denseGridPts()
     * @date 2011.11.02
     * @author 吴凯 wukai@irsa.ac.cn
     * @brief 根据dem的分辨率自适应加密点云
     * @param vec3DPts 三维点云
     * @param dbDemRect 点云的分布范围
     * @param dDemTopRes 加密初始自适应加密阈值
     * @param dDemTopRes 三角网加密层数
     * @param nParamidRatio 分层加密参数的比例系数
     * @retval TRUE 成功
     * @retval FALSE 失败
     * @version 1.0
     * @par 修改历史：
     * <作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool denseGridPts(vector<Pt3d>& vec3DPts , DbRect dbDemRect , double dDemTopRes , int nParamidNum , int nParamidRatio) ;

    /**
     * @fn densePartion3DPts()
     * @date 2011.11.02
     * @author 吴凯 wukai@irsa.ac.cn
     * @brief 点云加密分块处理
     * @param vec3DPts 分块的三维点云
     * @param pTin 点云对应的Tin结构
     * @param *pKring 克里金插值类
     * @param nParamidRatio 分层加密参数的比例系数
     * @retval TRUE 成功
     * @retval FALSE 失败
     * @version 1.0
     * @par 修改历史：
     * <作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool densePartion3DPts(vector<Pt3d>& vec3DPts , CmlTIN* pTin , CmlKringing* pKring , SINT nPyramidRatio);
    /**
     * @fn SmoothBySmoothFilter()
     * @date 2011.11.02
     * @author 吴凯 wukai@irsa.ac.cn
     * @brief dem 平滑滤波
     * @param nGridSize 滤波模板大小
     * @param nW dem对应的列数
     * @param nH dem对应的行数
     * @param *pDem 规则格网dem高程数据
     * @retval TRUE 成功
     * @retval FALSE 失败
     * @version 1.0
     * @par 修改历史：
     * <作者>    <时间>   <版本编号>    <修改原因>\n
     */
    bool SmoothBySmoothFilter(int nGridSize , int nW , int nH , double* pDem) ;
};

#endif // MLDEMPROC_H
