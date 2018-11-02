/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlPtsManage.h
* @date 2012.02.10
* @author 万文辉 whwan@irsa.ac.cn
* @brief 匹配点管理头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef CMLPTSMANAGE_H
#define CMLPTSMANAGE_H

#include "mlBase.h"
/**
* @class CmlPtsManage
* @date 2012.02.10
* @author 万文辉 whwan@irsa.ac.cn
* @brief 匹配点管理类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlPtsManage
{
public:
    /**
    * @fn CmlPtsManage
    * @date 2012.02.10
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlPtsManage类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlPtsManage();

    /**
    * @fn ~CmlPtsManage
    * @date 2012.02.10
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlPtsManage类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    virtual ~CmlPtsManage();

    /**
    * @fn GetPairPts
    * @date 2012.02.10
    * @author  万文辉 whwan@irsa.ac.cn
    * @brief 将左影像匹配点和右影像匹配点合并
    * @param clsImgLPts 左影像匹配点
    * @param clsImgRPts 右影像匹配点
    * @param vecMatchPts 左右匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool GetPairPts( ImgPtSet &clsImgLPts, ImgPtSet &clsImgRPts, vector<StereoMatchPt> &vecMatchPts );

    /**
    * @fn SplitPairPts
    * @date 2012.02.10
    * @author  万文辉 whwan@irsa.ac.cn
    * @brief 将成对匹配点分开
    * @param frmInfoL 左影像信息
    * @param frmInfoR 右影像信息
    * @param vecAutoMatchPts 左右同名匹配点
    * @param clsImgLPts 左影像匹配点
    * @param clsImgRPts 右影像匹配点
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool SplitPairPts( FrameImgInfo frmInfoL, FrameImgInfo frmInfoR, vector<StereoMatchPt> &vecAutoMatchPts, ImgPtSet &clsImgLPts, ImgPtSet &clsImgRPts );

    /**
    * @fn GetNewStereoPtID
    * @date 2012.02.10
    * @author  万文辉 whwan@irsa.ac.cn
    * @brief 获取匹配点的编号
    * @param clsLPts 左影像信息
    * @param clsRPts 右影像信息
    * @param lID 匹配点的编号
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool GetNewStereoPtID( ImgPtSet &clsLPts, ImgPtSet &clsRPts, ULONG &lID  );

    /**
    * @fn GetNewSinglePtID
    * @date 2012.02.10
    * @author  万文辉 whwan@irsa.ac.cn
    * @brief 获取单个点的编号
    * @param clsImgPts 影像信息
    * @param lID 匹配点的编号
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool GetNewSinglePtID(  ImgPtSet &clsImgPts, ULONG &lID  );


protected:
private:
};

#endif // CMLPTSMANAGE_H
