
/**
\file       DUP.h
\brief      双工服务
            
            定义双工服务的编程接口。
            
\author     刘西昌
\date       2009-11-18
\version    1.0
\note       WINDOSW/C
\par        修改记录 
            - 刘西昌 2009-11-18 模块建立
            
*/


#ifndef _DUP_H__
#define _DUP_H__


/************************** 函数声明 ********************************/
#ifdef __cplusplus
extern "C" {
#endif

int dup_open();                             /* 打开双工伪设备       */
int dup_close(int siDupFd);                 /* 关闭双工伪设备       */
int getdupsts(int           siDupFd,
              unsigned int* puiDupSts);     /* 获取双工状态         */

#ifdef __cplusplus
}
#endif
/********************************************************************/


#endif
