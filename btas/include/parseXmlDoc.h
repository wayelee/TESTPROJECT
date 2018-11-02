
#ifndef __PARSEXMLDOC_H__
#define __PARSEXMLDOC_H__

#include "cml_global.h"


#ifdef __cplusplus
extern "C" {
#endif 
 
/*
*说明：解析XML文档数据，返回文档句柄，可用cmlGet系列函数取数据。
       不管是压缩的数据还是未压缩的数据，都可用该函数解析。
*参数： pucContent  数据地址。
        siSize      数据长度。
*返回值：成功返回文档句柄，失败返回 0 。
*/
extern CmlDocHandler cmlLoadDocFromMem(const Sint8 *pucContent,Sint32 siSize);


/*
*说明：解析XML文件，返回文档句柄，可用后续的cmlGet系列函数取数据。
*参数： pscFileName 数据地址。
*返回值：成功返回文档句柄，失败返回 0 。
*/
extern CmlDocHandler cmlLoadDocFromFile(const Sint8 *pscFileName);


/*
*说明：获取文档中某元素的内容，该元素实际数据类型为字符串。
*参数：handle      文档指针
*      pscPath     XPath表达式，定位文档中的唯一元素。
*      pucContent  存储数据的缓冲区。
*      siSize      缓冲区长度，用户应确保缓冲区足够容纳实际数据，否则函数返回-1。
                   缓冲区长度应比实际数据长度大1，因为该函数要在字符串末尾加上结束符。
*返回值:成功返回字符串长度，失败返回-1
*/
extern Sint32 cmlGetString(CmlDocHandler handle,const Sint8* pscPath,  Sint8* pucContent, Sint32 siSize);



/*
*说明：获取文档中某元素的内容，该元素实际数据类型为二进制数据。
*参数:handle     文档指针
*     pscPath    XPath表达式，定位文档中的唯一元素。
*     pucContent 数据缓冲地址
*     siSize    。
*返回值：成功返回二进制数据长度，失败返回-1。
*/
extern Sint32 cmlGetBinary(CmlDocHandler handle,const Sint8* pscPath, Sint8* pucContent, Sint32 siSize);



/*
*说明：获取文档中某元素的内容，该元素实际数据类型为8位有符号整型。
*参数:handle   文档指针
*     pscPath  XPath表达式，定位文档中的唯一元素。
*     pscValue 存储结果的地址。
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetByte(CmlDocHandler handle,const Sint8* pscPath, Sint8 *pscValue);


/*
*说明：获取文档中某元素的内容，该元素实际数据类型为8位无符号整型。
*参数:handle   文档指针
*     pscPath  XPath表达式，定位文档中的唯一元素。
*     pucValue 存储结果的地址。
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetUByte(CmlDocHandler handle,const Sint8* pscPath, Uint8 *pucValue);



/*
*说明：获取文档中某元素的内容，该元素实际数据类型为16位有符号整型。
*参数:handle   文档指针
*     pscPath  XPath表达式，定位文档中的唯一元素。
*     pssValue 存储结果的地址。
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetShort(CmlDocHandler handle,const Sint8* pscPath,  Sint16 *pssValue);



/*
*说明：获取文档中某元素的内容，该元素实际数据类型为16位无符号整型。
*参数:handle   文档指针
*     pscPath  XPath表达式，定位文档中的唯一元素。
*     pusValue 存储结果的地址。
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetUShort(CmlDocHandler handle,const Sint8* pscPath,  Uint16 *pusValue);




/*
*说明：获取文档中某元素的内容，该元素实际数据类型为32位有符号整型。
*参数:handle   文档指针
*     pscPath  XPath表达式，定位文档中的唯一元素。
*     psiValue 存储结果的地址。
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetInt(CmlDocHandler handle,const Sint8* pscPath,  Sint32 *psiValue);



/*
*说明：获取文档中某元素的内容，该元素实际数据类型为32位无符号整型。
*参数:handle   文档指针
*     pscPath  XPath表达式，定位文档中的唯一元素。
*     puiValue 存储结果的地址。
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetUInt(CmlDocHandler handle,const Sint8* pscPath,  Uint32 *puiValue);


/*
*说明：获取文档中某元素的内容，该元素实际数据类型为64位有符号整型。
*参数:handle   文档指针
*     pscPath  XPath表达式，定位文档中的唯一元素。
*     pslValue 存储结果的地址。
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetLong(CmlDocHandler handle,const Sint8* pscPath,  Sint64 *pslValue);


/*
*说明：获取文档中某元素的内容，该元素实际数据类型为64位无符号整型。
*参数:handle   文档指针
*     pscPath  XPath表达式，定位文档中的唯一元素。
*     pulValue 存储结果的地址。
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetULong(CmlDocHandler handle,const Sint8* pscPath,  Uint64 *pulValue);



/*
*说明：获取文档中某元素的内容，该元素实际数据类型为单精度浮点型。
*参数:handle     文档指针
*     pscPath    XPath表达式，定位文档中的唯一元素。
*     pfValue    存储结果的地址
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetFloat(CmlDocHandler handle,const Sint8* pscPath,  Float32 *pfValue);



/*
*说明：获取文档中某元素的内容，该元素实际数据类型为双精度浮点型。
*参数:handle     文档指针
*     pscPath    XPath表达式，定位文档中的唯一元素。
*     pdValue    存储结果的地址
*返回值：成功返回0，失败返回-1
*/
extern Sint32 cmlGetDouble(CmlDocHandler handle,const Sint8* pscPath, Float64 *pdValue);


/*
*说明：   经该函数处理后，压缩和非压缩的文档都成为非压缩的。
*参数:    pcInput    输入数据地址   
				  siInputSize 输入数据长度
				  pcOutput 输出缓冲地址
				  siOutputSize 输出缓冲长度
				  psiUncompressSize  输出数据实际长度
*返回值：成功返回0，失败返回-1
*/
 extern Sint32 unzipCml(const Sint8* pcInput, 
				  const Sint32 siInputSize,
				  Sint8* pcOutput,
				  Sint32 siOutputSize,
				  Sint32* psiUncompressSize);
				  

#ifdef __cplusplus
}
#endif


#endif /*__PARSEXMLDOC_H__*/

