
/**
\file       ZD.h
            数据库查询服务
            
            提供使用数据库查询服务的编程接口。
            
\author     刘西昌
\date       2012-09-06
\version    1.0
\note       WINDOWS/C
\par        修改记录 
            - 刘西昌 2012-09-06 模块建立
            
*/


#ifndef _ZD_H__
#define _ZD_H__


#include "BTAS_TYPES.h"


/***************************** 结构定义 *********************************************/
typedef void*   DbHandler;                          /* 数据库查询句柄               */
/************************************************************************************/


/***************************** 函数声明 *********************************************/
#ifdef __cplusplus
extern "C" {
#endif

DbHandler dbNew();                                  /* 创建查询句柄                 */
void dbFree(DbHandler Handler);                     /* 关闭查询句柄                 */
Sint32 dbQueryFile(DbHandler   Handler, 
                   Uint32      uiFileType, 
                   Uint32      uiTaskCode, 
                   Uint32      uiObjCode,
                   const Sint8 szCond[]);           /* 查询文件                     */
Sint32 dbQueryData(DbHandler   Handler, 
                   Uint32      uiDataType, 
                   Uint32      uiTaskCode, 
                   Uint32      uiObjCode,
                   const Sint8 szPara[],
                   const Sint8 szCond[]);           /* 查询数据                     */
Sint32 dbGetByte(DbHandler   Handler, 
                 const Sint8 szName[], 
                 Sint8*      pscValue);             /* 获取查询结果(8位有符号整型)  */
Sint32 dbGetUByte(DbHandler   Handler, 
                  const Sint8 szName[], 
                  Uint8*      pucValue);            /* 获取查询结果(8位无符号整型)  */
Sint32 dbGetShort(DbHandler   Handler, 
                  const Sint8 szName[], 
                  Sint16*     pssValue);            /* 获取查询结果(16位有符号整型) */
Sint32 dbGetUShort(DbHandler   Handler, 
                   const Sint8 szName[], 
                   Uint16*     pusValue);           /* 获取查询结果(16位无符号整型) */
Sint32 dbGetInt(DbHandler   Handler, 
                const Sint8 szName[], 
                Sint32*     psiValue);              /* 获取查询结果(32位有符号整型) */
Sint32 dbGetUInt(DbHandler   Handler, 
                 const Sint8 szName[], 
                 Uint32*     puiValue);             /* 获取查询结果(32位无符号整型) */
Sint32 dbGetLong(DbHandler   Handler, 
                 const Sint8 szName[], 
                 Sint64*     pslValue);             /* 获取查询结果(64位有符号整型) */
Sint32 dbGetULong(DbHandler   Handler, 
                  const Sint8 szName[], 
                  Uint64*     pulValue);            /* 获取查询结果(64位无符号整型) */
Sint32 dbGetFloat(DbHandler   Handler, 
                  const Sint8 szName[], 
                  Float32*    pfValue);             /* 获取查询结果(单精度浮点型)   */
Sint32 dbGetDouble(DbHandler   Handler, 
                   const Sint8 szName[], 
                   Float64*    pdValue);            /* 获取查询结果(双精度浮点型)   */
Sint32 dbGetString(DbHandler   Handler, 
                   const Sint8 szName[], 
                   Sint8       szValue[], 
                   Sint32      siLen);              /* 获取查询结果(字符串型)       */
Sint32 dbNext(DbHandler Handler);                   /* 后移游标                     */
Sint32 dbPrev(DbHandler Handler);                   /* 前移游标                     */
Sint32 dbSeek(DbHandler Handler, 
              Sint32    siOffset, 
              Sint32    siWhence);                  /* 定位游标                     */
Sint32 dbTell(DbHandler Handler);                   /* 获取游标                     */
Sint32 dbEof(DbHandler Handler);                    /* 结束判断                     */
Sint32 dbCount(DbHandler Handler);                  /* 获取查询记录数               */
Sint32 dbDumpToFile(DbHandler   Handler, 
                    const Sint8 szFileName[]);      /* 存储查询结果至文件           */

#ifdef __cplusplus
}
#endif
/************************************************************************************/


#endif
