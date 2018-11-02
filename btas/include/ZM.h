
/**
\file       ZM.h
\brief      电文传输服务
            
            提供使用电文传输服务的编程接口。
            
\author     刘西昌
\date       2008-03-10
\version    1.0
\note       WINDOWS/C
\par        修改记录 
            - 刘西昌 2008-03-10 模块建立
            
*/


#ifndef _ZM_H__
#define _ZM_H__


/****************************** 常量定义 ************************************/
#define MS_MGC_HOST             "1L2X3C4"   /* 通配主机名                   */
#define MS_MGC_PROC             "4T3Y2Q1"   /* 通配进程名                   */
#define MS_MGC_TASK             0           /* 通配任务码                   */
#define MS_MGC_OBJ              0           /* 通配目标码                   */
#define MS_SUB_LOC              0X1U        /* 订阅类型：本地               */
#define MS_SUB_MAS              0X2U        /* 订阅类型：网络主机           */
#define MS_SUB_SLA              0X4U        /* 订阅类型：网络备机           */
#define MS_PRO_RT               1           /* 协议类型：实时消息协议       */
#define MS_PRO_RE               2           /* 协议类型：可靠消息协议       */
/****************************************************************************/


/***************************** 结构定义 *************************************/
/** 订阅信息 */
typedef struct _MS_SubInfo
{
    unsigned int        uiMsgType;          /* 消息类型                     */
    unsigned int        uiTaskCode;         /* 任务码                       */
    unsigned int        uiObjCode;          /* 目标码                       */
    unsigned int        uiSubType;          /* 订阅类型                     */
    char                szHostName[16];     /* 主机名                       */
    char                szProcName[8];      /* 进程名                       */
    unsigned int        uiRate;             /* 接收概率                     */
}MS_SubInfo;

/** 消息头 */
typedef struct _MS_MsgHead
{
    unsigned int        uiMsgType;          /* 消息类型                     */
    unsigned int        uiTaskCode;         /* 任务码                       */
    unsigned int        uiObjCode;          /* 目标码                       */
}MS_MsgHead;

/** 消息地址 */
typedef struct _MS_MsgAddr
{
    char                szHostName[16];     /* 主机名                       */
    char                szProcName[8];      /* 进程名                       */
}MS_MsgAddr;
/****************************************************************************/


/***************************** 函数声明 *************************************/
#ifdef __cplusplus
extern "C" {
#endif

int ms_init();                              /* 服务初始化                   */
int ms_open(unsigned int uiSubNum,
            MS_SubInfo   stSubInfo[],
            unsigned int uiQueBufLen);      /* 打开队列                     */
int ms_close(int siQueID);                  /* 关闭队列                     */
int ms_send(MS_MsgHead        stMsgHead,
            const void*       pvSndBuf,
            int               siSndLen,
            unsigned int      uiProType,
            const MS_MsgAddr* pstMsgDest);  /* 发送消息                     */
int ms_sendex(MS_MsgHead        stMsgHead,
              const void*       pvSndBuf,
              int               siSndLen,
              unsigned int      uiProType,
              const MS_MsgAddr* pstMsgSour,
              const MS_MsgAddr* pstMsgDest);/* 发送消息                     */
int ms_recv(int         siQueID,
            MS_MsgHead* pstMsgHead,
            void*       pvBuf,
            int         siBufLen,
            MS_MsgAddr* pstMsgSour);        /* 非阻塞接收                   */
int ms_recvw(int         siQueID,
             MS_MsgHead* pstMsgHead,
             void*       pvBuf,
             int         siBufLen,
             MS_MsgAddr* pstMsgSour);       /* 阻塞接收                    */
int ms_recvm(unsigned int uiQueIDNum,
             int          siQueID[],
             MS_MsgHead*  pstMsgHead,
             void*        pvBuf,
             int          siBufLen,
             MS_MsgAddr*  pstMsgSour);      /* 多队列阻塞接收               */
int ms_recva(int         siQueID,
             MS_MsgHead* pstMsgHead,
             void*       pvBuf,
             int         siBufLen,
             MS_MsgAddr* pstMsgSour);       /* 异步接收                     */
int ms_setaio(int siQueID, 
              int (*AioFunc)(int));         /* 设置异步I/O                  */
int ms_ctlaio(int siHow);                   /* 控制异步I/O                  */
int ms_getrate(int          siQueID, 
               unsigned int uiMsgType);     /* 获取接收概率                 */
int ms_setrate(int          siQueID, 
               unsigned int uiMsgType,
               unsigned int uiRate);        /* 设置接收概率                 */
int ms_gethname(char* pszHName,
                int   siLen);               /* 获取处理机名                 */
void ms_perror(int        siErrCode,
               const char szErrPre[]);      /* 获取错误说明                 */
char* ms_strerr(int siErrCode);             /* 获取错误说明                 */

#ifdef __cplusplus
}
#endif
/****************************************************************************/


/****************************************************************************/
/****************************************************************************/
/****************************** 常量定义 ************************************/
#define ZM_LOC                  0           /* 队列类型：本地队列           */
#define ZM_REM_MAS              1           /* 队列类型：网络队列(只收主机) */
#define ZM_REM_SLA              2           /* 队列类型：网络队列(只收备机) */
#define ZM_REM_DUP              3           /* 队列类型：网络队列(主备均收) */
#define ZM_TASK_CODE_WILDCARD   0           /* 通配任务码                   */
#define ZM_OBJ_CODE_WILDCARD    0           /* 通配目标码                   */
/****************************************************************************/


/***************************** 结构定义 *************************************/
/* ZM地址 */
typedef struct _ZM_Addr
{
    unsigned int        uiHostIP;           /* 主机IP                       */
    unsigned int        uiProcID;           /* 进程ID                       */
    char                szProcName[8];      /* 进程名                       */
}ZM_Addr;

/* 电文头 */
typedef MS_MsgHead      ZM_MsgHead;
/****************************************************************************/


/***************************** 函数声明 *************************************/
#ifdef __cplusplus
extern "C" {
#endif

int zm_init();                              /* 服务初始化                   */
int zm_open(unsigned int uiMsgTypeNum,
            unsigned int uiMsgType[],
            unsigned int uiQueType,
            unsigned int uiTaskCode,
            unsigned int uiObjCode,
            unsigned int uiQueBufLen);      /* 打开队列                     */
int zm_close(int siQueID);                  /* 关闭队列                     */
int zm_send(ZM_MsgHead  stMsgHead,
            const void* pvSndBuf,
            int         siSndLen);          /* 发送电文                     */
int zm_recv(int         siQueID,
            ZM_MsgHead* pstMsgHead,
            void*       pvBuf,
            int         siBufLen,
            ZM_Addr*    pstAddr);           /* 非阻塞接收                   */
int zm_recvw(int         siQueID,
             ZM_MsgHead* pstMsgHead,
             void*       pvBuf,
             int         siBufLen,
             ZM_Addr*    pstAddr);          /* 阻塞接收                     */
int zm_recvm(unsigned int uiQueIDNum,
             int          siQueID[],
             ZM_MsgHead*  pstMsgHead,
             void*        pvBuf,
             int          siBufLen,
             ZM_Addr*     pstAddr);         /* 多队列阻塞接收               */
int zm_recva(int          siQueID,
             ZM_MsgHead*  pstMsgHead,
             void*        pvBuf,
             int          siBufLen,
             ZM_Addr*     pstAddr);         /* 异步接收                     */
int zm_setaio(int siQueID, 
              int (*AioFunc)(int));         /* 设置异步I/O                  */
int zm_ctlaio(int siHow);                   /* 控制异步I/O                  */
int zm_gethname_(char* pszHName,
                 int   siLen);              /* 获取处理机名                 */
void zm_perror(int        siErrCode,
               const char szErrPre[]);      /* 获取错误说明                 */
char* zm_strerr(int siErrCode);             /* 获取错误说明                 */

#ifdef __cplusplus
}
#endif
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/


#endif
