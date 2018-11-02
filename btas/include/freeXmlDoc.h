#ifndef __FREEXMLDOC_H__
#define __FREEXMLDOC_H__

#include "cml_global.h"

#ifdef __cplusplus
extern "C" {
#endif 


/*
*说  明： 释放handler所占资源，使用baccNewEmptyXmlDoc、
          baccNewDocFromRaw、baccNewDocFromZip、baccNewDocFromFile
          等四个函数创建的xml文档句柄均需要调用该函数释放资源，
          否则会导致内存泄露。
*参  数： handle 文档句柄
*返回值： 无
*/
extern void cmlFreeDocHandler(CmlDocHandler handle);


#ifdef __cplusplus
}
#endif 

#endif /*__FREEXMLDOC_H__*/

