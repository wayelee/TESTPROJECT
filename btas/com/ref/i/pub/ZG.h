
/**
\file       ZG.h
\brief      全局段服务
            
            提供使用全局段服务的编程接口。
            
\author     刘西昌
\date       2012-08-16
\version    1.0
\note       WINDOWS/C
\par        修改记录 
            - 刘西昌    2012-08-16  模块建立
            
*/


#ifndef _ZG_H__
#define _ZG_H__


#include "BTAS_TYPES.h"
#include "STRTIME.h"


/****************************** 常量定义 ************************************/
#define CN_RDONLY               0               /* 读写模式：只读           */
#define CN_RDWR                 1               /* 读写模式：读写           */
/****************************************************************************/


/***************************** 函数声明 *************************************/
#ifdef __cplusplus
extern "C" {
#endif

Sint32 zg_init(Uint32 uiMisCode,
               Uint32 uiObjCode,
               Uint32 uiRwMode);                /* 服务初始化               */
void zg_exit(Uint32 uiMisCode, 
             Uint32 uiObjCode);                 /* 服务退出                 */
Sint32 zg_gethostname(Sint8 szHostName[]);      /* 获取主机名               */
#define zg_getHostName(p1) zg_gethostname(p1)
Sint32 zg_gethoststat(Uint32* puiHostStat);     /* 获取主备状态             */
#define zg_getMainBack(p1) zg_gethoststat(p1)
Sint32 zg_getmisname(Uint32 uiMisCode,
                     Uint32 uiObjCode,
                     Sint8  szMisName[]);       /* 获取任务名               */
#define zg_getMisName(p1,p2,p3) zg_getmisname(p1,p2,p3)
Sint32 zg_getobjname(Uint32 uiMisCode,
                     Uint32 uiObjCode,
                     Sint8  szObjName[]);       /* 获取目标名               */
#define zg_getObjName(p1,p2,p3) zg_getobjname(p1,p2,p3)
Sint32 zg_getdt(Uint32  uiMisCode,
                Uint32  uiObjCode,
                Sint32* psiDt);                 /* 获取任务时差             */
#define zg_getDt(p1,p2,p3) zg_getdt(p1,p2,p3)
Sint32 zg_setdt(Uint32 uiMisCode,
                Uint32 uiObjCode,
                Sint32 siDt);                   /* 设置任务时差             */
#define zg_setDt(p1,p2,p3) zg_setdt(p1,p2,p3) 
Sint32 zg_getphase(Uint32 uiMisCode,
                   Uint32 uiObjCode,
                   Sint8  szPhase[]);           /* 获取轨道段标             */
#define zg_getPhase(p1,p2,p3) zg_getphase(p1,p2,p3)
Sint32 zg_setphase(Uint32      uiMisCode,
                   Uint32      uiObjCode,
                   const Sint8 szPhase[]);      /* 设置轨道段标             */
#define zg_setPhase(p1,p2,p3) zg_setphase(p1,p2,p3)
Sint32 zg_getrevnum(Uint32  uiMisCode,
                    Uint32  uiObjCode,
                    Uint32* puiRevNum);         /* 获取轨道圈号             */
#define zg_getRevNum(p1,p2,p3) zg_getrevnum(p1,p2,p3)
Sint32 zg_setrevnum(Uint32 uiMisCode,
                    Uint32 uiObjCode,
                    Uint32 uiRevNum);           /* 设置轨道圈号             */
#define zg_setRevNum(p1,p2,p3) zg_setrevnum(p1,p2,p3)
Sint32 zg_gett0flag(Uint32  uiMisCode,
                    Uint32  uiObjCode,
                    Uint32* puiT0Flag);         /* 获取T0接收标志           */
#define zg_getT0Flag(p1,p2,p3) zg_gett0flag(p1,p2,p3)
Sint32 zg_sett0flag(Uint32 uiMisCode,
                    Uint32 uiObjCode,
                    Uint32 uiT0Flag);           /* 设置T0接收标志           */
#define zg_setT0Flag(p1,p2,p3) zg_sett0flag(p1,p2,p3)
Sint32 zg_getthtf(Uint32  uiMisCode,
                  Uint32  uiObjCode,
                  Uint32* puiThTf);             /* 获取理论TF               */
#define zg_getThTf(p1,p2,p3) zg_getthtf(p1,p2,p3)
Sint32 zg_setthtf(Uint32 uiMisCode,
                  Uint32 uiObjCode,
                  Uint32 uiThTf);               /* 设置理论TF               */
#define zg_setThTf(p1,p2,p3) zg_setthtf(p1,p2,p3)
Sint32 zg_getstationcode(Uint32      uiMisCode,
                         Uint32      uiObjCode,
                         const Sint8 szSName[],
                         Uint32*     uiSCode);  /* 通过站名取站码           */
#define zg_getStationCode(p1,p2,p3,p4) zg_getstationcode(p1,p2,p3,p4)
Sint32 zg_getstationname(Uint32 uiMisCode,
                         Uint32 uiObjCode,
                         Uint32 uiSCode,
                         Sint8  szSName[]);     /* 通过站码取站名           */
#define zg_getStationName(p1,p2,p3,p4) zg_getstationname(p1,p2,p3,p4)
Sint32 zg_getint(Uint32  uiMisCode,
                 Uint32  uiObjCode,
                 Uint32  uiIndex,
                 Sint8*  pscValid,
                 Sint32* psiValue);             /* 获取整数型信息           */
Sint32 zg_setint(Uint32 uiMisCode,
                 Uint32 uiObjCode,
                 Uint32 uiIndex,
                 Sint8  scValid,
                 Sint32 siValue);               /* 设置整数型信息           */
Sint32 zg_getdouble(Uint32   uiMisCode,
                    Uint32   uiObjCode,
                    Uint32   uiIndex,
                    Sint8*   pscValid,
                    Float64* pdValue);          /* 获取浮点型信息           */
Sint32 zg_setdouble(Uint32  uiMisCode,
                    Uint32  uiObjCode,
                    Uint32  uiIndex,
                    Sint8   scValid,
                    Float64 dValue);            /* 设置浮点型信息           */
Sint32 zg_getstring(Uint32 uiMisCode,
                    Uint32 uiObjCode,
                    Uint32 uiIndex,
                    Sint8* pscValid,
                    Sint8  szValue[]);          /* 获取字符串型信息         */
Sint32 zg_setstring(Uint32      uiMisCode,
                    Uint32      uiObjCode,
                    Uint32      uiIndex,
                    Sint8       scValid,
                    const Sint8 szValue[]);     /* 设置字符串型信息         */
Sint32 zg_getvec(Uint32   uiMisCode,
                 Uint32   uiObjCode,
                 Uint32   uiIndex,
                 Sint8*   pscValid,
                 Float64* pdValue1,
                 Float64* pdValue2,
                 Float64* pdValue3);            /* 获取向量型信息           */
Sint32 zg_setvec(Uint32  uiMisCode,
                 Uint32  uiObjCode,
                 Uint32  uiIndex,
                 Sint8   scValid,
                 Float64 dValue1,
                 Float64 dValue2,
                 Float64 dValue3);              /* 设置向量型信息           */
Sint32 zg_getevt(Uint32   uiMisCode,
                 Uint32   uiObjCode,
                 Uint32   uiIndex,
                 Sint8*   pscValid,
                 ST_Time* pstValue);            /* 获取事件型信息           */
Sint32 zg_setevt(Uint32   uiMisCode,
                 Uint32   uiObjCode,
                 Uint32   uiIndex,
                 Sint8    scValid,
                 ST_Time* pstValue);            /* 设置事件型信息           */
Sint32 zg_getevtflag(Uint32 uiMisCode,
                     Uint32 uiObjCode,
                     Uint32 uiIndex,
                     Sint8* pscFlag);           /* 获取事件型信息标志       */
#define zg_getTmEvtFlag(p1,p2,p3,p4) zg_getevtflag(p1,p2,p3,p4)
Sint32 zg_setevtflag(Uint32 uiMisCode,
                     Uint32 uiObjCode,
                     Uint32 uiIndex,
                     Sint8  scFlag);            /* 设置事件型信息标志       */
#define zg_setTmEvtFlag(p1,p2,p3,p4) zg_setevtflag(p1,p2,p3,p4)
Sint32 zg_getevttime(Uint32   uiMisCode,
                     Uint32   uiObjCode,
                     Uint32   uiIndex,
                     ST_Time* pstTime);         /* 获取事件型信息值         */
#define zg_getTmEvtTime(p1,p2,p3,p4) zg_getevttime(p1,p2,p3,p4)
Sint32 zg_setevttime(Uint32   uiMisCode,
                     Uint32   uiObjCode,
                     Uint32   uiIndex,
                     ST_Time* pstTime);         /* 设置事件型信息值         */
#define zg_setTmEvtTime(p1,p2,p3,p4) zg_setevttime(p1,p2,p3,p4)
void zg_perror(Sint32       siErrCode,
               const Sint8* pszErrPre);         /* 获取错误说明             */
Sint8* zg_strerr(Sint32 siErrCode);             /* 获取错误说明             */
Sint32 zg_getcompath(const Sint8 szString[],
                     Sint8       szPath[]);     /* 获取公共执行物理路径     */
Sint32 zg_getcomlpath(const Sint8 szString[],
                      Sint8       szPath[]);    /* 获取公共执行逻辑路径     */
Sint32 zg_getmispath(Uint32      uiMisCode,
                     const Sint8 szString[],
                     Sint8       szPath[]);     /* 获取任务执行物理路径     */
Sint32 zg_getmislpath(Uint32      uiMisCode,
                      const Sint8 szString[],
                      Sint8       szPath[]);    /* 获取任务执行逻辑路径     */
Sint32 zg_getobjpath(Uint32      uiMisCode,
                     Uint32      uiObjCode,
                     const Sint8 szString[],
                     Sint8       szPath[]);     /* 获取目标执行物理路径     */
Sint32 zg_getobjlpath(Uint32      uiMisCode,
                      Uint32      uiObjCode,
                      const Sint8 szString[],
                      Sint8       szPath[]);    /* 获取目标执行逻辑路径     */
Sint32 zg_getobjlist(Uint32 uiMisCode,
                     Uint32 uiObjList[]);       /* 获取目标列表             */

#ifdef __cplusplus
}
#endif
/****************************************************************************/

#endif
