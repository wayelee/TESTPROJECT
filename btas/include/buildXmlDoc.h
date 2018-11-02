
#ifndef __BUILDXMLDOC_H__
#define __BUILDXMLDOC_H__

/*#include "vld.h" 测试内存溢出*/

#include "cml_global.h"

#ifdef __cplusplus
extern "C" {
#endif 
 

/*
*说  明：在内存中创建一个空的xml文档，可以使用cmlPut*
         系列函数向该文档中添加内容。
*参  数：pscSchemaName 文档对应schema的名称，是以0结尾的字符串。
*返回值：成功返回文档句柄，出错返回小于0的值 。
*/
extern CmlDocHandler cmlNewEmptyDoc(const Sint8 *pscSchemaName);


/*
*说明：向文档指定位置添加（或修改）字符串类型的元素，如果指定元素不存在，
       添加新元素，如果指定元素已存在则覆盖原内容。
*参数：handle     文档句柄，cmlNewEmptyDoc函数的返回值。
*      pscPath    XPath表达式，定位文档中的唯一元素。
*      pscContent 数据地址
*      siSize     数据长度
*返回值:成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutString(CmlDocHandler handle,const Sint8* pscPath, const Sint8* pscContent, Sint32 siSize);


/*
*说  明：向文档指定位置添加（或修改）二进制类型的元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数:handle     文档句柄，cmlNewEmptyDoc函数的返回值。
*       pscPath    XPath表达式，定位文档中的唯一元素。
*       pscContent 数据地址
*       siSize   数据长度
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutBinary(CmlDocHandler handle,const Sint8* pscPath, const void* pscContent,  Sint32 siSize);



/*
*说  明：向文档指定位置添加（或修改）一个1字节有符号整型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle   文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath  XPath表达式，定位文档中的唯一元素。
*        scValue  要添加的整型值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutByte(CmlDocHandler handle,const Sint8* pscPath, Sint8 scValue);



/*
*说  明：向文档指定位置添加（或修改）一个1字节无符号整型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle   文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath  XPath表达式，定位文档中的唯一元素。
*        ucValue  要添加的整型值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutUByte(CmlDocHandler handle,const Sint8* pscPath, Uint8 ucValue);




/*
*说  明：向文档指定位置添加（或修改）一个2字节有符号整型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle   文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath  XPath表达式，定位文档中的唯一元素。
*        ssValue  要添加的整数值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutShort(CmlDocHandler handle,const Sint8* pscPath, Sint16 ssValue);


/*
*说  明：向文档指定位置添加（或修改）一个2字节无符号整型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle   文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath  XPath表达式，定位文档中的唯一元素。
*        usValue  要添加的整数值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutUShort(CmlDocHandler handle,const Sint8* pscPath, Uint16 usValue);



/*
*说  明：向文档指定位置添加（或修改）一个4字节有符号整型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle  文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath XPath表达式，定位文档中的唯一元素。
*        siValue 要添加的整数值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutInt(CmlDocHandler handle,const Sint8* pscPath, Sint32 siValue);


/*
*说  明：向文档指定位置添加（或修改）一个4字节无符号整型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle  文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath XPath表达式，定位文档中的唯一元素。
*        uiValue 要添加的整数值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutUInt(CmlDocHandler handle,const Sint8* pscPath, Uint32 uiValue);


/*
*说  明：向文档指定位置添加（或修改）一个8字节有符号整型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle   文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath  XPath表达式，定位文档中的唯一元素。
*        slValue  要添加的整数值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutLong(CmlDocHandler handle,const Sint8* pscPath, Sint64 slValue);



/*
*说  明：向文档指定位置添加（或修改）一个8字节无符号整型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle   文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath  XPath表达式，定位文档中的唯一元素。
*        ulValue  要添加的整数值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutULong(CmlDocHandler handle,const Sint8* pscPath, Uint64 ulValue);



/*
*说  明：向文档指定位置添加（或修改）一个单精度浮点型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle   文档句柄，cmlNewEmptyDoc函数的返回值。
*        pPath    XPath表达式，定位文档中的唯一元素。
*        fValue   要添加的浮点数值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutFloat(CmlDocHandler handle,const Sint8* pscPath, Float32 fValue);




/*
*说  明：向文档指定位置添加（或修改）一个双精度浮点型元素，如果指定元素不存在，则添加新元素
         如果指定元素已存在则覆盖原内容。
*参  数: handle   文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscPath  XPath表达式，定位文档中的唯一元素。
*        dValue   要添加的浮点数值
*返回值：成功返回0，出错返回小于0的值
*/
extern Sint32 cmlPutDouble(CmlDocHandler handle,const Sint8* pscPath, Float64 dValue);





/*
*说  明：从文档句柄中取出整个xml文档的数据，数据未压缩。
*参  数：handle    文档句柄，cmlNewEmptyDoc函数的返回值。
         ppcData   存放数据地址的指针，由调用者负责释放pData所指向的空间，
		     psiSize   数据长度

*返回值：正确返回0，出错返回小于0的值;
*/
/*extern int cmlDumpRawDocToMem(CmlDocHandler handle,Sint8** ppcData, Sint32* psiSize);*/



/*
*说  明：将未压缩的文档数据存放到文件中。
*参  数：handle      文档句柄，cmlNewEmptyDoc函数的返回值。
         pscFileName 存放文档内容的文件名。
 
*返回值：正确返回0，出错返回小于0的值;
*/
/*extern int cmlDumpRawDocToFile(CmlDocHandler handle,const Sint8* pscFileName);*/


/*
*说  明：从文档句柄中取出整个xml文档的数据，数据经过压缩。
*参  数：handle  文档句柄，cmlNewEmptyDoc函数的返回值。
         ppcData   存放数据地址的指针，由调用者负责释放pData所指向的空间，
         psiSize   存放数据长度的地址
*返回值：正确返回0,出错返回小于0的值.
*/
/*extern int cmlDumpCompressDocToMem(CmlDocHandler handle,Sint8** ppcData,Sint32* psiSize);*/



/*
*说  明：将压缩后的文档数据存放到文件中。
*参  数：handle    文档句柄，cmlNewEmptyDoc函数的返回值。
         pFileName 存放文档内容的文件名。
  
*返回值：正确返回0,出错返回小于0的值.
*/
/*extern int cmlDumpCompressDocToFile(CmlDocHandler handle,const Sint8* pscFileName);*/


/*
*说明：从文档句柄中取出整个xml文档的数据
*参数：handle    文档句柄，cmlNewEmptyDoc函数的返回值。
*      ppcData   存放数据的指针地址。
       psiSize   存放数据长度的地址
*      siIfCompress 0 不压缩，1压缩
*返回值：正确返回0,出错返回小于0的值.
*/
extern Sint32 cmlDumpDocToMem(CmlDocHandler handle,Sint8** ppcData, Sint32* psiSize, Sint32  siIfCompress);
 


/*
*说明：从文档句柄中取出整个xml文档的数据
*参  数：handle      文档句柄，cmlNewEmptyDoc函数的返回值。
*        pscFileName 存放文档内容的文件名。
*        siIfCompress 0 不压缩，1压缩
*返回值：正确返回0,出错返回小于0的值.
*/

extern Sint32 cmlDumpDocToFile(CmlDocHandler handle,const Sint8* pscFileName ,Sint32 siIfCompress);
 


#ifdef __cplusplus
}
#endif


#endif /*__BUILDXMLDOC_H__*/

