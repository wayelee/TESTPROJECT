
/**
\file       ZF.h
\brief      文件传输服务
            
            提供文件传输服务的编程接口。
            
\author     刘西昌
\date       2009-11-18
\version    1.0
\note       WINDOSW/C
\par        修改记录 
            - 刘西昌 2009-11-18 模块建立
            
*/


#ifndef _ZF_H__
#define _ZF_H__


/************************** 函数声明 ********************************/
#ifdef __cplusplus
extern "C" {
#endif

int zf_init();                              /* 服务初始化           */
int zf_get(unsigned int uiMisCode,
           unsigned int uiObjCode,
           const char   szHost[],
           const char   szRemFile[],
           const char   szLocFile[],
           char         scType);            /* 下载文件             */
int zf_put(unsigned int uiMisCode,
           unsigned int uiObjCode,
           const char   szHost[],
           const char   szRemFile[],
           const char   szLocFile[],
           char         scType);            /* 上传文件             */
void zf_perror(int        siErrCode,
               const char szErrPre[]);      /* 获取错误说明         */
char* zf_strerr(int siErrCode);             /* 获取错误说明         */

#ifdef __cplusplus
}
#endif
/********************************************************************/


#endif
