#ifndef CSATMAPPROJ_H
#define CSATMAPPROJ_H
#include "imageviewer.h"
#include "../../include/mlTypes.h"

class CmlSatMapProj
{
public:
    CmlSatMapProj( );
    CmlSatMapProj( string strPath );
    virtual ~CmlSatMapProj();
    bool LoadProj( SatProj &pro, SatOptions &ops, string projPath, string demPath, string domPath, DOUBLE dRes, bool bBaseFlag );
    bool SatMapping( string projPath, string demPath, string domPath, DOUBLE dRes, bool bBaseFlag, bool bUsePts );
    /**
        * @fn ReadCE1InOri
        * @date 2011.12.16
        * @author 刘一良 ylliu@irsa.ac.cn
        * @brief 读入CE-1卫星影像内定向参数文件
        * @param path 文件路径
        * @param CE1img CE-1卫星影像
        * @param CE1imgIO CE-1卫星影像内定向参数
        * @retval TRUE 成功
        * @retval FALSE 失败
        * @version 1.0
        * @par 修改历史:
        * <作者>  <时间>  <版本编号>  <描述>\n
        */
    bool ReadCE1InOri( const SCHAR *path, CE1IOPara &CE1imgIO );
    /**
    * @fn ReadCE2InOri
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 读入CE-2卫星影像内定向参数文件
    * @param path 文件路径
    * @param CE2img CE-2卫星影像
    * @param CE2imgIO CE-2卫星影像内定向参数
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool ReadCE2InOri( const SCHAR *path, CE2IOPara &CE2imgIO );
    /**
    * @fn ReadImgScanTime
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 读入卫星影像扫描线时间文件
    * @param path 文件路径
    * @param pImgTime 卫星影像扫描线时间
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool ReadImgScanTime( const SCHAR *path, vector<DOUBLE> &vecImgTime );
//    /**
//    * @fn ReadEo
//    * @date 2011.12.16
//    * @author 刘一良 ylliu@irsa.ac.cn
//    * @brief 读入卫星影像原始测控数据文件
//    * @param path 文件路径
//    * @param pEo 卫星影像原始测控数据
//    * @retval TRUE 成功
//    * @retval FALSE 失败
//    * @version 1.0
//    * @par 修改历史:
//    * <作者>  <时间>  <版本编号>  <描述>\n
//    */
//    bool ReadEo( const SCHAR *path, CmlMat *pEo );
    /**
    * @fn ReadLineEo
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 读入卫星影像原始测控数据文件外方位线元素
    * @param path 文件路径
    * @param pEo 外方位线元素
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool ReadLineEo( const SCHAR *path, vector<LineEo> &vecLineEo );
    /**
    * @fn ReadAngleEo
    * @date 2011.12.16
    * @author 刘一良 ylliu@irsa.ac.cn
    * @brief 读入卫星影像原始测控数据文件外方位角元素
    * @param path 文件路径
    * @param pEo 卫星影像原始测控数据外方位角元素
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool ReadAngleEo( const SCHAR *path, vector<AngleEo> &vecAngleEo );
    bool ReadPts( const SCHAR *path, vector<StereoMatchPt> &vecPts );

protected:
private:
};
#endif //CSATMAPPROJ_H
