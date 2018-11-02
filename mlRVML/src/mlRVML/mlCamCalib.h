/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlCamCalib.h
* @date 2012.01
* @author 吴凯 wukai@irsa.ac.cn
* @brief 工程中相机标定模块头文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef _MLCAMCALIB_H
#define _MLCAMCALIB_H
#include "mlBase.h"
#include "mlMat.h"

#define MAX_THRESH 999999
#define MIN_THRESH -999999

// 非线性迭代阈值结构体
typedef struct structTermCriteria
{
    SINT nIterMax ; // 迭代次数
    DOUBLE dThreshValue ; // 迭代阈值
    structTermCriteria()
    {
        nIterMax = 30 ;
        dThreshValue = 0.05 ;
    }
} TermCriteria ;

/**
* @class CmlCamCalib
* @date 2011.11
* @author 吴凯 wukai@irsa.ac.cn
* @brief 相机标定类定义
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
*/

class CmlCamCalib
{
public :
    camModelType camModel ;// 畸变校正模型（8参数 ,10参数）
    TermCriteria dltTerm ; // dlt迭代阈值
    TermCriteria colinearityTerm ; // 严格模型迭代阈值
private :
    vector<Pt2d> imgPtsList ; // 标志点像方坐标序列
    vector<Pt3d> objPtsList ; // 标识点物方坐标序列
    ExOriPara exPara ;  // 相机外方外元素(单相机)
    InOriPara inPara ;  // 相机内方位元素(单相机)
    SINT m_nW ; // 影像宽度
    SINT m_nH ; // 影像高度
    SINT nGcp ; // 影像标志点数量
    bool bInParaInitFlag ; // 标识相机参数是否初始化
    DOUBLE* pLPara ;   // DLT 的11个线性变换参数
// 接口函数
public :
/**
 *@fn CmlCamCalib()
 *@date 2011.11
 *@author 吴凯
 *@brief 相机标定类构造函数
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
*/
    CmlCamCalib() ;
    /**
 *@fn CmlCamCalib()
 *@date 2011.11
 *@author 吴凯
 *@brief 相机标定类析构函数
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
*/
    ~CmlCamCalib() ;
/**
 *@fn mlSingleCamCalib
 *@date 2012.02
 *@author 吴凯
 *@brief 单相机标定函数
 *@param vecImgPts  控制点像方坐标序列
 *@param vecObjPts 控制点物方坐标序列
 *@param nW  影像宽度
 *@param nH  影像高度
 *@param inPara 相机内参数
 *@param exPara  相机外参数
 *@param vecErrorPts  控制点像方残差序列
 *@param bFlag 判断给定相机初始参数再标定还是检查点精度检查
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool  mlSingleCamCalib(vector<Pt2d>& vecImgPts , const vector<Pt3d>& vecObjPts ,  SINT nW , SINT nH ,
                           InOriPara& inPara , ExOriPara& exPara , vector<Pt3d>& vecErrorPts , bool bFlag = 1);
/**
 *@fn mlStereoCamCalib()
 *@date 2012.02
 *@author 吴凯
 *@brief 双相机标定函数
 *@param vecLImgPts  控制点左相机像方坐标序列
 *@param vecRImgPts  控制点右相机像方坐标序列
 *@param vecObjPts  控制点物方坐标序列
 *@param nW  影像宽度
 *@param nH 影像高度
 *@param inLPara  左相机内参数
 *@param inRPara  右相机内参数
 *@param exLPara  左相机外参数
 *@param exRPara 右相机外参数
 *@param exStereoPara  立体相机相对方位参数
 *@param vecErrorPts  控制点物方残差序列
 *@param bFlag 判断给定相机初始参数再标定还是检查点精度检查
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool  mlStereoCamCalib(const vector<Pt2d>& vecLImgPts ,const vector<Pt2d>& vecRImgPts , vector<Pt3d>& vecObjPts ,
                            SINT nW , SINT nH , InOriPara& inLPara , InOriPara& inRPara , ExOriPara& exLPara ,
                            ExOriPara& exRPara , ExOriPara& exStereoPara, vector<Pt3d>& vecErrorPts , bool bFlag = 1);
/**
 *@fn backProjection
 *@date 2012.02
 *@author 吴凯
 *@brief 后方交会（坐标系统转换后再解算）
 *@param imgPtsList 控制点像方坐标序列
 *@param objPtsList 控制点物方坐标序列
 *@param inPara 相机内参数
 *@param exPara 相机外参数
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool backProjection(const vector<Pt2d>& imgPtsList , const vector<Pt3d>& objPtsList ,
                                   InOriPara& inPara , ExOriPara& exPara) ;
private :
/**
 *@fn homographPtsInit()
 *@date 2012.02
 *@author 吴凯
 *@brief 相机标定参数初始化
 *@param vecImgPts  控制点像方坐标序列
 *@param vecObjPts  控制点物方坐标序列
 *@param inCamPara 相机初始参数
 *@param nW  相机宽度
 *@param nH  相机高度
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool homographPtsInit(const vector<Pt2d>& vecImgPts, const vector<Pt3d>& vecObjPts, InOriPara& inCamPara ,SINT nW , SINT nH) ;
/**
 *@fn dltCalib()
 *@date 2012.02
 *@author 吴凯
 *@brief 相机直接线性变换标定
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool dltCalib() ;
/**
 *@fn colinearityCalib()
 *@date 2012.02
 *@author 吴凯
 *@brief 相机严格共线模型标定
 *@param camType 相机畸变模型选项 默认为10参数模型
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool colinearityCalib(camModelType camType = Affine) ;
/**
 *@fn imgErrorCheck()
 *@date 2012.02
 *@author 吴凯
 *@brief 相机标定像方精度检查
 *@param vecImgPts  检查点像方坐标序列
 *@param vecObjPts  检查点物方坐标序列
 *@param inPara  相机内参数
 *@param exPara  相机外参数
 *@param vecErrorPts 检查点像方残差序列
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool imgErrorCheck(const vector<Pt2d>& vecImgPts, const vector<Pt3d>& vecObjPts, InOriPara inPara ,
                       ExOriPara exPara , vector<Pt3d>& vecErrorPts);
/**
 *@fn objErrorCheck()
 *@date 2012.02
 *@author 吴凯
 *@brief  相机标定物方精度检查
 *@param vecLImgPts 检查点左相机像方坐标序列
 *@param vecRImgPts 检查点右相机像方坐标序列
 *@param vecObjPts  检查点物方坐标序列
 *@param inLPara  左相机内参数
 *@param inRPara  右相机内参数
 *@param exLPara  左相机外参数
 *@param exRPara  右相机外参数
 *@param vecErrorPts  检查点物方残差序列
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool objErrorCheck(const vector<Pt2d>& vecLImgPts , const vector<Pt2d>& vecRImgPts ,const vector<Pt3d>& vecObjPts ,
                        InOriPara inLPara , InOriPara inRPara , ExOriPara exLPara , ExOriPara exRPara , vector<Pt3d>& vecErrorPts) ;
/**
 *@fn dltParaInit()
 *@date 2012.02
 *@author 吴凯
 *@brief 直接线性标定初始化
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool dltParaInit() ;
/**
 *@fn dltParaIterSolve
 *@date 2012.02
 *@author 吴凯
 *@brief 直接线性相机标定参数解求
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool dltParaIterSolve();
/**
 *@fn dltPara2InPara
 *@date 2012.02
 *@author 吴凯
 *@brief 直接线性变换参数解算内方位参数
 *@param inPara
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    void dltPara2InPara(InOriPara& inPara) ;
/**
 *@fn dltPara2ExPara
 *@date 2012.02
 *@author 吴凯
 *@brief 直接线性变换参数解算外方位参数
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool dltPara2ExPara() ;
/**
 *@fn POK2RMat()
 *@date 2012.02
 *@author 吴凯
 *@brief 角度转换为旋转矩阵 (基于控制场标定坐标系的旋转矩阵，相对于工程基础类omg-phi-kap(x,y,z),两坐标系统框架omg,phi意义相同，kap方向相反)
 *@param pOri 角元素指针   转角phi-omg-kap转角系统，此时phi相对z轴，omg相对x轴，kap相对y轴
 *@param pRMat 旋转矩阵指针
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool POK2RMat( OriAngle* pOri, CmlMat* pRMat ) ;
/**
 *@fn imgPtRectify()
 *@date 2012.02
 *@author 吴凯
 *@brief 图像点畸变校正
 *@param ptXY 校正前点
 *@param inPara 相机内参数
 *@param ptNewXY 校正后点
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    void imgPtRectify(Pt2d ptXY , InOriPara inPara , Pt2d& ptNewXY) ;
/**
 *@fn objPOKPtReproject()
 *@date 2012.02
 *@author 吴凯
 *@brief pok坐标系统框架下物方点反投影到像方点
 *@param ptXYZ 物方点
 *@param ptS 相机外方位线元素
 *@param pOpkMat 相机角元素变换矩阵
 *@param inPara 相机内参数
 *@param ptNewXYZ 物方点对应的像方投影点
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    void objPOKPtReproject(Pt3d ptXYZ , Pt3d ptS , CmlMat pOpkMat ,InOriPara inPara , Pt2d& ptNewXYZ ) ;
/**
 *@fn objPtReproject()
 *@date 2012.02
 *@author 吴凯
 *@brief opk坐标系统框架下物方点反投影到像方点
 *@param ptXYZ 物方点
 *@param ptS 相机外方位线元素
 *@param pOpkMat 相机角元素变换矩阵
 *@param inPara 相机内参数
 *@param ptNewXYZ 物方点对应的像方投影点
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    void objPtReproject(Pt3d ptXYZ , Pt3d ptS , CmlMat pOpkMat ,InOriPara inPara , Pt2d& ptNewXYZ ) ;
/**
 *@fn forwardIntersection()
 *@date 2012.02
 *@author 吴凯
 *@brief 前方交会函数
 *@param imgLPt 左像点图像坐标
 *@param imgRPt 右像点图像坐标
 *@param exLPara 左片外方位元素
 *@param inLPara 左片内方位元素
 *@param exRPara 右片外方位元素
 *@param inRPara 右片内方位元素
 *@param objPt 前方交会物方点坐标
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool forwardIntersection(Pt2d imgLPt , Pt2d imgRPt , ExOriPara exLPara , InOriPara inLPara ,
                                ExOriPara exRPara , InOriPara inRPara , Pt3d& objPt ) ;
/**
 *@fn stereoParaSolve()
 *@date 2012.02
 *@author 吴凯
 *@brief 双相机相对方位参数解算
 *@param exLPara  左相机外参数
 *@param exRPara  右相机外参数
 *@param exStereoPara  双相机相对参数
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.1
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n
*/
    bool stereoParaSolve(ExOriPara exLPara , ExOriPara exRPara , ExOriPara& exStereoPara) ;
} ;

#endif
