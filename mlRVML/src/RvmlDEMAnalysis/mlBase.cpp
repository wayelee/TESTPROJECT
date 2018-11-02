/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlBase.cpp
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 工程公共函数实现源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/

#include "mlBase.h"

CmlLogRecord g_clsLog ;
/************************************************************
*************************************************************/
CmlLogRecord::CmlLogRecord()
{
    m_bIsNeedPrSINTOnScreen = true;
    m_bIsValid = false;

    m_pOutPutFile = NULL;

}
CmlLogRecord::~CmlLogRecord()
{

}
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
bool CmlLogRecord::Open( const SCHAR*  strFilePath, bool bIsNeedPrSINTOnScreen )
{
    if( m_bIsValid == true )
    {
        return false;
    }

    m_bIsNeedPrSINTOnScreen = bIsNeedPrSINTOnScreen;

    m_pOutPutFile = fopen( strFilePath, "w");

    if( m_pOutPutFile != NULL )
    {
        m_bIsValid = true;
        return true;
    }
    else
    {
        return false;
    }

}//初始化输出文件的路径，及是否需要打印在屏幕上.同时输入此次日志的头信息
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
bool CmlLogRecord::Close()
{
    m_strFilePath.clear();
    m_strTotalMsg.clear();
    if( m_pOutPutFile != NULL )
    {
        fclose( m_pOutPutFile );
    }
    m_bIsValid = false;
    return true;
}
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
void CmlLogRecord::AddSuccessQuitMsg( const SCHAR* cFuncName, const SCHAR* cFileName, UINT nLineNum )
{
    string strMsg = "Success  ;";

    string strFileName(cFileName);
    string strFuncName(cFuncName);
    string strStatus( "Quit Function" );
    SCHAR strLine[10];
    sprintf( strLine, "%d", nLineNum );

    strMsg.append( strStatus );
    strMsg += ";  ";
    strMsg.append( strFuncName );
    strMsg += ";  ";
    strMsg.append( strFileName );
    strMsg +="; Line  ";
    strMsg.append( strLine );
    strMsg += '\n';

    if( m_bIsNeedPrSINTOnScreen )
    {
        cout << strMsg;
    }
    if( m_bIsValid )
    {
        fprintf( m_pOutPutFile, "%s", strMsg.c_str() );
    }

    m_strTotalMsg.append( strMsg );
    return;
}
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
void CmlLogRecord::AddErrorMsg( SCHAR* cMsg, const SCHAR* cFuncName, const SCHAR* cFileName, UINT nLineNum )
{
    string strTmpMsg = "Error    ;";

    string strFileName(cFileName);
    string strFuncName(cFuncName);
    string strStatus( cMsg );
    SCHAR strLine[10];
    sprintf( strLine, "%d", nLineNum );

    strTmpMsg.append( strStatus );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFuncName );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFileName );
    strTmpMsg +="; Line  ";
    strTmpMsg.append( strLine );
    strTmpMsg += '\n';

    if( m_bIsNeedPrSINTOnScreen )
    {
        cout << strTmpMsg;
    }

    if( m_bIsValid )
    {
        fprintf( m_pOutPutFile, "%s", strTmpMsg.c_str() );
    }

    m_strTotalMsg.append( strTmpMsg );
    return;
}
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
void CmlLogRecord::AddExceptionMsg( SCHAR* cMsg, const SCHAR* cFuncName, const SCHAR* cFileName, UINT nLineNum )
{
    string strTmpMsg = "Exception; ";

    string strFileName(cFileName);
    string strFuncName(cFuncName);
    string strStatus( cMsg );
    SCHAR strLine[10];
    sprintf( strLine, "%d", nLineNum );

    strTmpMsg.append( strStatus );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFuncName );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFileName );
    strTmpMsg +="; Line  ";
    strTmpMsg.append( strLine );
    strTmpMsg += '\n';

    if( m_bIsNeedPrSINTOnScreen )
    {
        cout << strTmpMsg;
    }

    if( m_bIsValid )
    {
        fprintf( m_pOutPutFile, "%s", strTmpMsg.c_str() );
    }

    m_strTotalMsg.append( strTmpMsg );
    return;
}
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
void CmlLogRecord::AddNoticeMsg( SCHAR* cMsg, const SCHAR* cFuncName, const SCHAR* cFileName, UINT nLineNum )
{
    string strTmpMsg = "Notice   ; ";

    string strFileName(cFileName);
    string strFuncName(cFuncName);
    string strStatus( cMsg );
    SCHAR strLine[10];
    sprintf( strLine, "%d", nLineNum );

    strTmpMsg.append( strStatus );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFuncName );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFileName );
    strTmpMsg +="; Line  ";
    strTmpMsg.append( strLine );
    strTmpMsg += '\n';

    if( m_bIsNeedPrSINTOnScreen )
    {
        cout << strTmpMsg;
    }

    if( m_bIsValid )
    {
        fprintf( m_pOutPutFile, "%s", strTmpMsg.c_str() );
    }

    m_strTotalMsg.append( strTmpMsg );
    return;
}
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
bool PtInRect( Pt2i pt, MLRect rect )
{
    if( ( pt.X < rect.dXMin )||( pt.X > rect.dXMax )||( pt.Y < rect.dYMin )||( pt.Y > rect.dYMax ) )
    {
        return false;
    }
    else
    {
        return true;
    }
}
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
bool PtInRect( Pt2d pt, MLRect rect )
{
    if( ( pt.X < rect.dXMin )||( pt.X > rect.dXMax )||( pt.Y < rect.dYMin )||( pt.Y > rect.dYMax ) )
    {
        return false;
    }
    else
    {
        return true;
    }
}
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
bool OPK2RMat( OriAngle* pOri, CmlMat* pRMat )
{
    if( false == pRMat->IsValid() )
    {
        pRMat->Initial( 3, 3 );
    }
    if( ( pRMat->GetH() != 3) || ( pRMat->GetW() != 3 ) )
    {
        return false;
    }

    DOUBLE dSOmg = sin( (pOri->omg) );
    DOUBLE dSPhi = sin( (pOri->phi) );
    DOUBLE dSKap = sin( (pOri->kap) );

    DOUBLE dCOmg = cos( (pOri->omg) );
    DOUBLE dCPhi = cos( (pOri->phi) );
    DOUBLE dCKap = cos( (pOri->kap) );

    pRMat->SetAt( 0, 0, dCPhi*dCKap );
    pRMat->SetAt( 0, 1, -dCPhi*dSKap );
    pRMat->SetAt( 0, 2, dSPhi );

    pRMat->SetAt( 1, 0, ( dCOmg*dSKap + dSOmg*dSPhi*dCKap ) );
    pRMat->SetAt( 1, 1, ( dCOmg*dCKap - dSOmg*dSPhi*dSKap ) );
    pRMat->SetAt( 1, 2, -dSOmg*dCPhi );

    pRMat->SetAt( 2, 0, ( dSOmg*dSKap - dCOmg*dSPhi*dCKap ) );
    pRMat->SetAt( 2, 1, ( dSOmg*dCKap + dCOmg*dSPhi*dSKap ) );
    pRMat->SetAt( 2, 2, dCOmg*dCPhi );

    return true;

}

/**
* @fn RMat2OPK
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
bool RMat2OPK( CmlMat* pRMat, OriAngle* pOri )
{
    pOri->omg = ( atan2( -pRMat->GetAt(1, 2), pRMat->GetAt(2,2) ) );
    pOri->phi = ( atan2( pRMat->GetAt(0, 2), sqrt(pRMat->GetAt(1, 2)*pRMat->GetAt(1, 2)+ pRMat->GetAt(2, 2)*pRMat->GetAt(2, 2)) ) );
    pOri->kap = ( atan2( -pRMat->GetAt(0,1), pRMat->GetAt(0,0) ) );

    return true;
};

















