/*
名称: 	STRTIME.h
文档:   BTAS2/IRID/01/1.00
日期: 	2007-12-20
描述: 	时间结构定义
系统: 	Windows/Unix
*/

#ifndef  _STRTIME_H_
#define  _STRTIME_H_

#include "BTAS_TYPES.h"

typedef struct _ST_Time
{
    Uint16			                usYear     ;  /* 年    */
    Uint16							usMonth    ;  /* 月    */
    Uint16  						usDay      ;  /* 日    */
    Uint16  						usTemp    ;   /* <optional>预留字符 */
    Uint32  						uiSecond   ;  /* 当日累积秒,0.1ms */
}ST_Time;

#endif

