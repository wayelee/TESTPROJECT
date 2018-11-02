
/**
\file       ZF2.h
            文件传输服务
            
            提供使用基于订阅发布模式文件传输服务的编程接口。
            
\author     刘西昌
\date       2012-07-27
\version    1.0
\note       WINDOWS/C
\par        修改记录 
            - 刘西昌 2012-07-27 模块建立
            
*/


#ifndef _ZF2_H__
#define _ZF2_H__


/****************************** 常量定义 ********************************************/
#define FS_MGC_HOST             "1L2X3C4"           /* 通配主机名                   */
#define FS_MGC_PROC             "4T3Y2Q1"           /* 通配进程名                   */
#define FS_MGC_TASK             0                   /* 通配任务码                   */
#define FS_MGC_OBJ              0                   /* 通配目标码                   */
/************************************************************************************/


/***************************** 结构定义 *********************************************/
/** 文件头 */
typedef struct _FS_FileHead
{
    unsigned int        uiFileType;                 /* 文件类型                     */
    unsigned int        uiTaskCode;                 /* 任务码                       */
    unsigned int        uiObjCode;                  /* 目标码                       */
}FS_FileHead;

/** 文件地址 */
typedef struct _FS_FileAddr
{
    char               szHostName[16];              /* 主机名                       */
    char               szProcName[8];               /* 进程名                       */
}FS_FileAddr;
/************************************************************************************/


/***************************** 函数声明 *********************************************/
#ifdef __cplusplus
extern "C" {
#endif

int fs_init();                                      /* 服务初始化                   */
int fs_putone(const FS_FileHead* pstFileHead,
              const char         szFileName[],
              char               scType,
              const void*        pvSndBuf,
              int                siSndLen,
              const FS_FileAddr* pstFileDest);      /* 发送一个文件                 */
int fs_puttwo(const FS_FileHead* pstFileHead,
              const char         szFileNameF[],
              char               scTypeF,
              const char         szFileNameS[],
              char               scTypeS,
              const void*        pvSndBuf,
              int                siSndLen,
              const FS_FileAddr* pstFileDest);      /* 发送两个文件                 */
int fs_putmulti(const FS_FileHead* pstFileHead,
                int                siFileNum,
                const char*        szFileName[],
                char               scType[],
                const void*        pvSndBuf,
                int                siSndLen,
                const FS_FileAddr* pstFileDest);    /* 发送多个文件                 */
int fs_getone(const FS_FileHead* pstFileHead,
              const char         szFileName[],
              const char         szFileNameS[]);    /* 获取单个文件                 */
int fs_getmulti(const FS_FileHead* pstFileHead,
                int                siFileNum,
                const char*        szFileName[],
                const char         szFileNameS[]);  /* 获取多个文件                 */
void fs_perror(int        siErrCode,
               const char szErrPre[]);              /* 获取错误说明                 */
char* fs_strerr(int siErrCode);                     /* 获取错误说明                 */

#ifdef __cplusplus
}
#endif
/************************************************************************************/



/****************************** 常量定义 ********************************************/
#define ZF_TASK_CODE_WILDCARD   0                   /* 通配任务码                   */
#define ZF_OBJ_CODE_WILDCARD    0                   /* 通配目标码                   */
/************************************************************************************/


/***************************** 结构定义 *********************************************/
/** 文件头 */
typedef FS_FileHead     ZF_FileHead;

/** 文件地址 */
typedef FS_FileAddr     ZF_FileAddr;
/************************************************************************************/


/***************************** 函数声明 *********************************************/
#ifdef __cplusplus
extern "C" {
#endif

int zf2_init();                                     /* 服务初始化                   */
int zf2_putone(const ZF_FileHead* pstFileHead,
               const char         szFileName[],
               char               scType);          /* 发送一个文件                 */
int zf2_puttwo(const ZF_FileHead* pstFileHead,
               const char         szFileNameF[],
               char               scTypeF,
               const char         szFileNameS[],
               char               scTypeS);         /* 发送两个文件                 */
int zf2_putmulti(const ZF_FileHead* pstFileHead,
                 int                siFileNum,
                 const char*        szFileName[],
                 char               scType[]);      /* 发送多个文件                 */
int zf2_getone(const ZF_FileHead* pstFileHead,
               const char         szFileName[],
               const char         szFileNameS[]);   /* 获取单个文件                 */
int zf2_getmulti(const ZF_FileHead* pstFileHead,
                 int                siFileNum,
                 const char*        szFileName[],
                 const char         szFileNameS[]); /* 获取多个文件                 */
void zf2_perror(int         siErrCode,
                const char  szErrPre[]);            /* 获取错误说明                 */
char* zf2_strerr(int siErrCode);                    /* 获取错误说明                 */

int zf2_init_();                                    /* 服务初始化                   */
int zf2_putone_(const ZF_FileHead* pstFileHead,
                const char         szFileName[],
                char               scType);         /* 发送一个文件                 */
int zf2_puttwo_(const ZF_FileHead* pstFileHead,
                const char         szFileNameF[],
                char               scTypeF,
                const char         szFileNameS[],
                char               scTypeS);        /* 发送两个文件                 */
int zf2_putmulti_(const ZF_FileHead* pstFileHead,
                  int                siFileNum,
                  const char*        szFileName[],
                  char               scType[]);     /* 发送多个文件                 */
int zf2_getone_(const ZF_FileHead* pstFileHead,
                const char         szFileName[],
                const char         szFileNameS[]);  /* 获取单个文件                 */
int zf2_getmulti_(const ZF_FileHead* pstFileHead,
                  int                siFileNum,
                  const char*        szFileName[],
                  const char         szFileNameS[]);/* 获取多个文件                 */
void zf2_perror_(int         siErrCode,
                 const char  szErrPre[]);           /* 获取错误说明                 */
char* zf2_strerr_(int siErrCode);                   /* 获取错误说明                 */

#ifdef __cplusplus
}
#endif
/************************************************************************************/


#endif
