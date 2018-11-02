/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlDomProc.h
* @date 2011.12.18
* @author 吴凯  wukai@irsa.ac.cn
* @brief  单站地形dom生成功能模块类头文件(包括多视角下地形生成)
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef MLDOMPROC_H
#define MLDOMPROC_H

#include "mlGeoRaster.h"
//#include "mlRasterProc.h"
#include "mlBase.h"
#include "mlFrameImage.h"
#include "mlDemProc.h"


#ifndef DOM_NODATA
#define DOM_NODATA 255
#endif
typedef struct tagStructDomUnit
{
    BYTE b_Value ; // 图像灰度值
    double dMinDis ; // 距离图像边缘的最小值
    bool bFlag ; // 标识是否赋值
    int nImg ; // 索引计数
} structDomUnit ;

#define ClipBuffer 0

class CmlDomProc //: public CmlRasterProc
{
public: // variable
    Pt2d ptOrigin ; // dom 左上角点地理坐标
    DOUBLE dRes ; // dom 分辨率
    SINT m_nHeight ; // dom 栅格数据高度
    SINT m_nWidth ;// dom 栅格数据宽度
    SINT m_nImgHeight ; // 图像数据高度
    SINT m_nImgWidth ; // 图像数据宽度
    SINT m_nBlockNum ; // dom数据处理分块数
private:
    vector<Pt3d> vecImgPos ; // 影像外方位线元素集合
public:  //
    /**
     *@fn CmlDomProc()
     *@date 2011.11
     *@author 吴凯
     *@brief DOM处理类构造函数
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    CmlDomProc() ;
    /**
     *@fn ~CmlDomProc()
     *@date 2011.11
     *@author 吴凯
     *@brief DOM处理类析构函数
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    virtual ~CmlDomProc() ;
    /**
     *@fn createOrthoImage()
     *@date 2011.11
     *@author 吴凯
     *@brief   序列影像生成正射影像
     *@param vecStereoImgInfo  序列影像信息
     *@param strDemFilePath  dem 文件路径
     *@param strDomFilePath  dom 文件路径
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool createOrthoImage(vector<StereoSet>& vecStereoImgInfo , string strDemFilePath , string strDomFilePath, ImgDotType imgType = T_Float64 ) ;
private :
    /**
     *@fn fillDomBlock()
     *@date 2011.11
     *@author 吴凯
     *@brief   对dom 分块数据赋值
     *@param vecStereoImgInfo  序列影像信息
     *@param *pDem  分块dem栅格数据
     *@param vecStereoImgInfo  序列影像信息
     *@param nXOffset  dom 沿x方向偏移量
     *@param nYOffset  dom 沿y方向偏移量
     *@param nWidth  dem 分块宽度
     *@param nHeight dom 分块高度
     *@param *pDem  对应于分块dem的dom灰度值数据
     * @retval TRUE 成功
     * @retval FALSE 失败
     * @version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool fillDomBlock(vector<StereoSet>& vecStereoImgInfo , DOUBLE* pDem , SINT nXOffset , SINT nYOffset , SINT nWidth ,
                      SINT nHeight , DOUBLE* pDom) ;
    /**
     *@fn projOrthoMapVal()
     *@date 2011.11
     *@author 吴凯
     *@brief   反投影到正射影像， 取得相应范围的影像值
     *@param structCamInfo  单影像信息
     *@param nImgNo  影像序号
     *@param nXOffset  dom 沿x方向偏移量
     *@param nYOffset  dom 沿y方向偏移量
     *@param nW  dem 分块宽度
     *@param nH  dom 分块高度
     *@param *pDem  对应于分块dem的dom灰度值数据
     *@param *pUnitBlock   dom 灰度值结构体单元
     * @retval TRUE 成功
     * @retval FALSE 失败
     * @version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool projOrthoMapVal(StereoSet& structCamInfo , SINT nImgNo , SINT nXOffset , SINT nYOffset ,  SINT  nW , SINT nH , DOUBLE*  pDem ,
                         structDomUnit* pUnitBlock) ;
    /**
     *@fn fillProjPolyVal()
     *@date 2011.11
     *@author 吴凯
     *@brief  对DEM范围内的覆盖下的区域进行赋值
     *@param exPara 影像外方位元素
     *@param nImgNo 影像序号
     *@param inPara 影像内方位元素
     *@param nH  dom 分块高度
     *@param nW  dom 分块宽度
     *@param *pDem  对应于分块dem的dom灰度值数据
     *@param pRasterProc 影像栅格数据信息
     *@param pUnitBlock dom 灰度值结构体单元
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool fillProjPolyVal(ExOriPara exPara , SINT nImgNo , InOriPara inPara  , SINT nXOffset , SINT nYOffset , SINT  nW ,  SINT nH ,
                         DOUBLE*  pDem , CmlGdalDataset* pRasterProc , structDomUnit* pUnitBlock) ;
    /**
     *@fn projObjPt2ImgPt()
     *@date 2011.11
     *@author 吴凯
     *@brief  反投影地面点，取得相应的影像点
     *@param ptXs 影像线元素
     *@param ptObjX 物方点的物方点坐标
     *@param R 旋转矩阵
     *@param inPara 影像内方位元素
     *@param ptImgX 物方点对应的像方坐标
     *@retval TRUE 成功
     *@retval FALSE 失败
     *@version 1.0
     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
     */
    bool projObjPt2ImgPt(Pt3d ptXs , Pt3d ptObjX , DOUBLE* R , InOriPara& inPara , Pt2d& ptImgX) ;
//    /**
//     *@fn calCrossVal()
//     *@date 2011.11
//     *@author 吴凯
//     *@brief  对DEM范围内的覆盖下的区域进行赋值
//     *@param ptObj  物方点地理坐标
//     *@param ptBack  影像相对的外方位线元素
//     *@param exPara 影像外方位元素
//     *@retval 向量相乘值，据此判断向量交叉角度
//     *@version 1.0
//     *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
//     */
//    double calCrossVal(Pt3d ptObj , Pt3d ptBack , ExOriPara exPara) ;
};

#endif
