/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlBase.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 工程公共函数定义头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef _MLBASE_H
#define _MLBASE_H

#include "../../include/mlTypes.h"
#include "mlMat.h"


/*******************************************************
函数名称：栅格公共类,以2d点为元素的矩阵
作    者：万文辉
功能描述：所有栅格均以左上角角为原点，横向为x，纵向为y
输    入:
输    出:
版本编号:   1.0
修改历史:    <作者>    <时间>   <版本编号>    <描述>
********************************************************/

typedef CmlTemplateMat<Pt2d> CRasterPt2D;//!<Pt2d型矩阵结构体

/**
* @struct tagmlRect
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩形类公共结构
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagmlRect
{
    DOUBLE dXMin;//!<左上角X坐标
    DOUBLE dYMin;//!<左上角Y坐标
    DOUBLE dXMax;//!<右下角X坐标
    DOUBLE dYMax;//!<右下角Y坐标
    tagmlRect()
    {
        dXMin = dYMin = dXMax = dYMax = 0.0;
    }
} MLRect;

/**
* @fn PtInRect
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 判断某点是否在矩形框内
* @param pt 某Pt2i型点
* @param rect 矩形框
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool PtInRect( Pt2i pt, MLRect rect );
/**
* @fn PtInRect
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 判断某点是否在矩形框内
* @param pt 某Pt2d型点
* @param rect 矩形框
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool PtInRect( Pt2d pt, MLRect rect );


/**
* @fn OPK2RMat
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 旋转角OPK转换到旋转矩阵
* @param pOri 旋转角OPK
* @param pRMat 旋转矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool OPK2RMat( OriAngle* pOri, CmlMat* pRMat );
/**
* @fn OPK2RMat
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 旋转矩阵转换到旋转角OPK
* @param pRMat 旋转矩阵
* @param pOri 旋转角OPK
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool RMat2OPK( CmlMat* pRMat, OriAngle* pOri );

/**
* @class CmlLogRecord
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief LOG信息记录类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlLogRecord
{
public:
    /**
    * @fn CmlLogRecord
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlLogRecord类空参构造函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    CmlLogRecord();
    /**
    * @fn ~CmlLogRecord
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief CmlLogRecord类析构函数
    * @version 1.0
    * @return 无返回值
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    ~CmlLogRecord();

public:
    /**
    * @fn Open
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 初始化输出文件的路径，及是否需要打印在屏幕上.同时输入此次日志的头信息
    * @param strFilePath 文件路径
    * @param bIsNeedPrSINTOnScreen 屏幕打印选项
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool Open( const SCHAR*  strFilePath, bool bIsNeedPrSINTOnScreen = true );
    /**
    * @fn Close
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 关闭文件
    * @retval TRUE 成功
    * @retval FALSE 失败
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    bool Close();
    /**
    * @fn AddSuccessQuitMsg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 函数运行正常至退出消息函数
    * @param cFuncName 函数名
    * @param cFileName 文件名
    * @param nLineNum 函数在文件中的位置
    * @return 无返回值
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void AddSuccessQuitMsg( const SCHAR* cFuncName, const SCHAR* cFileName, UINT nLineNum );
    /**
    * @fn AddErrorMsg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 函数运行遭遇预知错误消息函数
    * @param cMsg 消息
    * @param cFuncName 函数名
    * @param cFileName 文件名
    * @param nLineNum 函数在文件中的位置
    * @return 无返回值
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void AddErrorMsg( SCHAR* cMsg, const SCHAR* cFuncName, const SCHAR* cFileName, UINT nLineNum );
    /**
    * @fn AddExceptionMsg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 函数运行出现异常消息函数
    * @param cMsg 消息
    * @param cFuncName 函数名
    * @param cFileName 文件名
    * @param nLineNum 函数在文件中的位置
    * @return 无返回值
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void AddExceptionMsg( SCHAR* cMsg, const SCHAR* cFuncName, const SCHAR* cFileName, UINT nLineNum );
    /**
    * @fn AddNoticeMsg
    * @date 2011.11.02
    * @author 万文辉 whwan@irsa.ac.cn
    * @brief 函数运行出现提示消息函数
    * @param cMsg 消息
    * @param cFuncName 函数名
    * @param cFileName 文件名
    * @param nLineNum 函数在文件中的位置
    * @return 无返回值
    * @version 1.0
    * @par 修改历史：
    * <作者>    <时间>   <版本编号>    <修改原因>\n
    */
    void AddNoticeMsg( SCHAR* cMsg, const SCHAR* cFuncName, const SCHAR* cFileName, UINT nLineNum );

    bool   m_bIsValid;//!<判断是否有效
private:
    string m_strTotalMsg;//!<全部消息
    string m_strFilePath;//!<文件路径

    bool   m_bIsNeedPrSINTOnScreen;//!<是否进行屏幕打印

    FILE*  m_pOutPutFile;//!<输出文件路径

};

extern CmlLogRecord  g_clsLog;//!<LOG信息记录


#define LOGAddSuccessQuitMsg( )         if( g_clsLog.m_bIsValid == true ){ g_clsLog.AddSuccessQuitMsg( __PRETTY_FUNCTION__, __FILE__, __LINE__ );};
#define LOGAddErrorMsg( strMsg )        if( g_clsLog.m_bIsValid == true ){ g_clsLog.AddErrorMsg( strMsg, __PRETTY_FUNCTION__, __FILE__, __LINE__ );};
#define LOGAddExceptionMsg( strMsg )    if( g_clsLog.m_bIsValid == true ){ g_clsLog.AddExceptionMsg( strMsg, __PRETTY_FUNCTION__, __FILE__, __LINE__ );};
#define LOGAddNoticeMsg( strMsg )       if( g_clsLog.m_bIsValid == true ){ g_clsLog.AddNoticeMsg( strMsg, __PRETTY_FUNCTION__, __FILE__, __LINE__ );};






#endif
