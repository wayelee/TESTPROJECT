
/**
\file       ZL.h
\brief      日志服务
            
            提供使用日志服务的编程接口。
            
\author     刘西昌
\date       2009-11-18
\version    1.0
\note       WINDOWS/C
\par        修改记录 
            - 刘西昌 2009-11-18 模块建立
            
*/


#ifndef _ZL_H__
#define _ZL_H__


/********************************** 函数声明 ****************************/
#ifdef __cplusplus
extern "C" {
#endif

int zl_init();                                  /* 服务初始化           */
int zl_send(unsigned char ucLogType,
            unsigned int  uiMisCode,
            unsigned int  uiObjCode,
            const char    szLogContent[]);      /* 发送日志             */
void zl_perror(int        siErrCode,
               const char szErrPre[]);          /* 获取错误说明         */
char* zl_strerr(int siErrCode);                 /* 获取错误说明         */

#ifdef __cplusplus
}
#endif
/************************************************************************/


#endif
