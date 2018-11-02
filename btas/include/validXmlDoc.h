#ifndef __BACCXMLGLOBAL_H__
#define __BACCXMLGLOBAL_H__


#include "cml_global.h"


#ifdef __cplusplus
extern "C" {
#endif 


/*
*说  明：验证xml文档是否合法。
*参  数：handle 文档句柄
*返回值: 0 合法，<0 不合法
*/
extern Sint32 cmlValidateDoc(CmlDocHandler handle);


/*
*作用：添加验证文件物理路径
*参数：物理路径
*返回值： 无
*/

extern void cmlAddXsdDir(const Sint8* psiXsdPath);





#ifdef __cplusplus
}
#endif 


#endif //__BACCXMLGLOBAL_H__


