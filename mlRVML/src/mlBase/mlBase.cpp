/************************************************************************
Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
文件名称:   mlBase.cpp
创建日期:   2011.11.02
作    者:   万文辉
描    述:   ml工程中公共函数的实现
版本编号:   1.0
修改历史:    <作者>    <时间>   <版本编号>    <描述>


************************************************************************/
#include "../../include/mlBase.h"

//aaaaaaaa
//bbbbbbbb

//qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq
//11111111111111111
//22222222222222222

test2
//旋转角OPK转换成旋转矩阵
test1
ML_EXTERN_C bool OPK2RMat( OriAngle* pOri, CmlMat* pRMat )
{
    if( true == pRMat->IsValid() )
    {
        pRMat->Initial( 3, 3 );
    }
    else
        return false;

    double dSOmg = sin( Deg2Rad(pOri->omg) );
    double dSPhi = sin( Deg2Rad(pOri->phi) );
    double dSKap = sin( Deg2Rad(pOri->kap) );

eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
    double dCOmg = cos( Deg2Rad(pOri->omg) );
    double dCPhi = cos( Deg2Rad(pOri->phi) );
    double dCKap = cos( Deg2Rad(pOri->kap) );

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

ML_EXTERN_C bool OPK2RMat( OriAngle* pOri, CmlMat* pRMat )
{
}

//旋转矩阵转换到旋转角OPK
ML_EXTERN_C bool RMat2OPK( CmlMat* pRMat, OriAngle* pOri )
{
    pOri->omg = Rad2Deg( atan2( -pRMat->GetAt(1, 2), pRMat->GetAt(2,2) ) );
    pOri->phi = Rad2Deg( atan2( pRMat->GetAt(0, 2), sqrt(pRMat->GetAt(1, 2)*pRMat->GetAt(1, 2)+ pRMat->GetAt(2, 2)*pRMat->GetAt(2, 2)) ) );
    pOri->kap = Rad2Deg( atan2( -pRMat->GetAt(0,1), pRMat->GetAt(0,0) ) );

    return true;
};

/************************************************************
*************************************************************/
CLogRecord::CLogRecord()
{
    m_bIsNeedPrintOnScreen = true;

}
CLogRecord::~CLogRecord()
{

}
bool CLogRecord::InitialPara(  bool bIsNeedPrintOnScreen, string strHeadInfo )
{
    m_bIsNeedPrintOnScreen = bIsNeedPrintOnScreen;
    m_strTotalMsg.append( strHeadInfo );
    return true ;
}

bool CLogRecord::InitialPara(  bool bIsNeedPrintOnScreen, string strHeadInfo )
{
    m_bIsNeedPrintOnScreen = bIsNeedPrintOnScreen;
    m_strTotalMsg.append( strHeadInfo );
    return true ;
}

void CLogRecord::AddSuccessQuitMsg( const char* cFuncName, const char* cFileName, int nLineNum )
{
    string strMsg = "Success  ;";

    string strFileName(cFileName);
    string strFuncName(cFuncName);
    string strStatus( "Quit Function" );
    char strLine[10];
    sprintf( strLine, "%d", nLineNum );

    strMsg.append( strStatus );
    strMsg += ";  ";
    strMsg.append( strFuncName );
    strMsg += ";  ";
    strMsg.append( strFileName );
    strMsg +="; Line  ";
    strMsg.append( strLine );
    strMsg += '\n';

    if( m_bIsNeedPrintOnScreen )
    {
        cout << strMsg;
    }
    m_strTotalMsg.append( strMsg );
    return;

}


void CLogRecord::AddErrorMsg( char* cMsg, const char* cFuncName, const char* cFileName, int nLineNum )
{
    string strTmpMsg = "Error    ;";

    string strFileName(cFileName);
    string strFuncName(cFuncName);
    string strStatus( cMsg );
    char strLine[10];
    sprintf( strLine, "%d", nLineNum );

    strTmpMsg.append( strStatus );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFuncName );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFileName );
    strTmpMsg +="; Line  ";
    strTmpMsg.append( strLine );
    strTmpMsg += '\n';

    if( m_bIsNeedPrintOnScreen )
    {
        cout << strTmpMsg;
    }
    m_strTotalMsg.append( strTmpMsg );
    return;
}

void CLogRecord::AddExceptionMsg( char* cMsg, const char* cFuncName, const char* cFileName, int nLineNum )
{
    string strTmpMsg = "Exception; ";

    string strFileName(cFileName);
    string strFuncName(cFuncName);
    string strStatus( cMsg );
    char strLine[10];
    sprintf( strLine, "%d", nLineNum );

    strTmpMsg.append( strStatus );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFuncName );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFileName );
    strTmpMsg +="; Line  ";
    strTmpMsg.append( strLine );
    strTmpMsg += '\n';

    if( m_bIsNeedPrintOnScreen )
    {
        cout << strTmpMsg;
    }
    m_strTotalMsg.append( strTmpMsg );
    return;
}

void CLogRecord::AddNoticeMsg( char* cMsg, const char* cFuncName, const char* cFileName, int nLineNum )
{
    string strTmpMsg = "Notice   ; ";

    string strFileName(cFileName);
    string strFuncName(cFuncName);
    string strStatus( cMsg );
    char strLine[10];
    sprintf( strLine, "%d", nLineNum );

    strTmpMsg.append( strStatus );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFuncName );
    strTmpMsg += ";  ";
    strTmpMsg.append( strFileName );
    strTmpMsg +="; Line  ";
    strTmpMsg.append( strLine );
    strTmpMsg += '\n';

    if( m_bIsNeedPrintOnScreen )
    {
        cout << strTmpMsg;
    }
    m_strTotalMsg.append( strTmpMsg );
    return;
}

bbbbbbbbbbbbbbbbbbbbbbbbbbbb
{
}

ddddddddddddddddddddddddddd
{
}

bool CLogRecord::OutPutToFile(  char* strFilePath  )
{
    FILE* pF = fopen( strFilePath, "w+" );

    if( pF != NULL )
    {
        fprintf( pF, "%s", m_strTotalMsg.c_str() );
        fclose( pF );

        return true;
    }
    else
    {
        return false;
    }
}

qqqqqqqqqqqqqqqqqqqqqqqqqqqq
{
}

















