// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 LIBZMSGDLL_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何其他项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// LIBZMSGDLL_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。
#ifdef LIBZMSGDLL_EXPORTS
#define LIBZMSGDLL_API __declspec(dllexport)
#else
#define LIBZMSGDLL_API __declspec(dllimport)
#endif

#include <stdio.h>
#include <string.h>
#include <iostream>
#include <fstream>
#include "ZD.h"
#include "ZE.h"
#include "ZF2.h"
#include "ZM.h"
#include "ZG.h"
#include "CML.h"

using namespace std;

#define IFLI_RNAVIP              5051    //导航点确定结果文件
#define IFLI_RPACPA              5054;   //路径单元规划控制参数文件
#define IFLI_YSDHX               5000;   //导航相机原始图像文件
#define IFLI_YSQJX               5001;    //全景相机原始图像文件
#define IFLI_YSBZX               5002;    //避障相机原始图像文件
#define IFLI_ZS                      5016;    //DOM文件
#define IFLI_FYQJX               5026;    //巡视器全景相机原始彩色复原图像文件
#define IFLI_GC_100CM            5027;    //100厘米量级DEM地形文件
#define IFLI_GC_10CM             5028;    //10厘米量级DEM地形文件
#define IFLI_GC_1CM              5029;    //1厘米量级DEM地形文件
#define IFLI_GC_HIGH             5030;    //1厘米以下高分辨率DEM地形文件
#define IMSI_STATIONCREATE       3503;    //站点记录文件

char            filename[256];
int             qid;
//ZD查询文件
typedef void (CALLBACK * fnCallBackDatabase)(char* filename,int nName,char* filenames,int nNames,int uiFileTypes,int uiTaskCode,int uiObjCode);
typedef void (CALLBACK * fnCallBackStationID)(int uiStationID);
//zg全局段服务
typedef void (CALLBACK * zgCallBackVec)(Float64 value1,Float64 value2,Float64 value3);

#ifdef __cplusplus
extern "C" {
#endif 
	//ZD相关
	//查询站点记录数量
	LIBZMSGDLL_API int zdFileCount( int filetype,char* querysql,HWND pwnd);
	LIBZMSGDLL_API int zdFile( int filetype,char* querysql,HWND pwnd);
	LIBZMSGDLL_API void Register_CallBack_Database(fnCallBackDatabase func);
	//ZD查询站点信息
	LIBZMSGDLL_API int zdStationID();
	LIBZMSGDLL_API void Register_CallBack_StationID(fnCallBackStationID func);
	//zf相关，获取文件
	LIBZMSGDLL_API int zfget( char* filename,char* filenames,int uiFileTypes,int uiTaskCode,int uiObjCode);
	//zf相关，在线监听文件
	LIBZMSGDLL_API int zfRecvw( char* strDirecPath,HWND pwnd);
	LIBZMSGDLL_API int zfclose();
	//zg相关，全局段服务
	LIBZMSGDLL_API int zg_vec( int uiTaskCode,int uiObjCode,int vecindex);
	LIBZMSGDLL_API void Register_CallBack_Vec(zgCallBackVec func);

#ifdef __cplusplus
}
#endif 
