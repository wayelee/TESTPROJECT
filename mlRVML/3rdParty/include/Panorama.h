/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file Panorama.h
* @date 2012.1.21
* @author 梁健
* @brief 全景图像拼接导出头文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#ifndef PANORAMA_H_INCLUDED
#define PANORAMA_H_INCLUDED

#include <string.h>
#include <vector>
using namespace std;

#if defined _WIN32 || defined _WIN64
#include "targetver.h"
#endif
//--------------------------------------
////////////////////////
#if defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64
#define PANOMOSAIC_CDECL __cdecl
#define PANOMOSAIC_STDCALL __stdcall
#else
#define PANOMOSAIC_CDECL
#define PANOMOSAIC_STDCALL
#endif

#ifndef PANOMOSAIC_EXTERN_C
#ifdef __cplusplus
#define PANOMOSAIC_EXTERN_C extern "C"
#define PANOMOSAIC_DEFAULT(val) = val
#else
#define PANOMOSAIC_EXTERN_C
#define PANOMOSAIC_DEFAULT(val)
#endif
#endif

#if ( defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64 ) && (defined MLPANOMOSAIC_EXPORTS)
#define PANOMOSAIC_EXPORTS __declspec(dllexport)
#else
#define PANOMOSAIC_EXPORTS
#endif

#ifndef PANOMOSAIC_API
#define PANOMOSAIC_API(rettype) PANOMOSAIC_EXTERN_C PANOMOSAIC_EXPORTS rettype PANOMOSAIC_CDECL
#endif

//--------------------------------------
/**
* @strut MatchPt
* @date 2012.1.21
* @author 梁健
* @brief 匹配点结构
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
struct MatchPt
{
	float x, y;//控制点x，y坐标
	unsigned int ptImgIdxL;//控制点src图像序号，四位，从1001～NNNN
	unsigned int ptImgIdxR;//控制点dst图像序号，四位，从1001～NNNN
	unsigned long long int ptIdx;//控制点ID，十六位，如：1001100200000001
};


/**
* @strut PanoMatchInfo
* @date 2012.1.21
* @author 梁健
* @brief 全景匹配结构
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
struct PanoMatchInfo
{
	//PanoMatchInfo(){}
	int nInIdxSrc;//内部src序号，从0～N
	int nInIdxDst;//内部dst序号，从0～N（待删）
	unsigned int nSrcImgIdx;//外部src序号，四位，从1001～NNNN
	unsigned int nDstImgIdx;//外部dst序号，四位，从1001～NNNN
	int nMatchPtNum;//匹配点个数
	string sImgPathSrc;//src图像路径
	string sImgPathDst;//dst图像路径
	vector<MatchPt> vecMatchPtsSrc;//src匹配点结构数组
	vector<MatchPt> vecMatchPtsDst;//dst匹配点结构数组
	int nLoopID;//影像圈号
	int nLoopImgID;//影像序号
	vector<int> vecInIdxDst;//与本张图像匹配的所有图像内部序号，从0～N（取代nInIdxDst）

};

/**
* @fn PanoMatchSurf(vector<char*> &cParamList, vector<string> &cImageList, vector<double> &dExtParam, char* cOutputFile, vector<PanoMatchInfo> &vecPanoMatchInfo, bool &bNeedAddPts)
* @date 2012.1.21
* @author 梁健
* @brief 生成原始图像间两两匹配点文件
* @param cParamList 全景拼接参数
* @param cImageList 原始图像路径
* @param dExtParam 原始图像外方位元素
* @param cOutputFile 输出匹配点文件路径
* @param vecPanoMatchInfo 传出的匹配结构
* @param bNeedAddPts 是否需要人工添加点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
PANOMOSAIC_API(bool) PanoMatchSurf(vector<char*> &cParamList, vector<string> &cImageList, vector<double> &dExtParam, char* cOutputFile, vector<PanoMatchInfo> &vecPanoMatchInfo, bool &bNeedAddPts);

PANOMOSAIC_API(bool) PanoMatchSift(vector<char*> &cParamList, vector<string> &cImageList, vector<double> &dExtParam, char* cOutputFile, vector<PanoMatchInfo> &vecPanoMatchInfo, bool &bNeedAddPts);

PANOMOSAIC_API( bool ) PanoMatchPoint(vector<char*> &cParamList, vector<string> &cImageList, vector<double> &dExtParam, char* cOutputFile, vector<PanoMatchInfo> &vecPanoMatchInfo, bool &bNeedAddPts);

/**
* @fn PanoMosaic(vector<char*> &cParamList, vector<string> &sImageList, vector<double> &dExtParam, vector<PanoMatchInfo> vecPanoMatchPts, char* cOutputFile)
* @date 2012.1.21
* @author 梁健
* @brief 全景拼接函数
* @param cParamList 全景拼接参数
* @param sImageList 原始图像路径
* @param dExtParam 原始图像外方位元素
* @param vecPanoMatchPts 原始图像间的匹配点（认为已经满足条件）
* @param cOutputFile 输出全景图像路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
PANOMOSAIC_API(bool) PanoMosaic(vector<char*> &cParamList, vector<string> &sImageList, vector<double> &dExtParam, vector<PanoMatchInfo> vecPanoMatchPts, char* cOutputFile);

#endif // PANORAMA_H_INCLUDED


