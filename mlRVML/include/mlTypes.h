/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlTypes.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief ml工程中公共定义头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef _MLTYPES_H
#define _MLTYPES_H

#include "assert.h"
#include <fstream>
#include <iostream>
#include <iomanip>
#include "stdlib.h"
#include "stdio.h"
#include "assert.h"
#include "float.h"
#include "math.h"
#include <string>
#include "string.h"
#include <vector>
using namespace std;

#if defined _WIN32|| defined _WIN64
#include "targetver.h"
#include <windows.h>

#define log2(d) ( log10(d) / log10(2.0) ) 

#endif 

#if(_MSC_VER >= 1400 && defined _M_X64) || (__GNUC__ >= 4 && defined __x86_64__)
#if defined WIN64
#include <intrin.h>
#endif
#include <emmintrin.h>  /*SSE2指令集头文件*/
#endif


/*满足跨平台移植需求定义导出函数*/
#if defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64
#define ML_CDECL __cdecl
#define ML_STDCALL __stdcall
#else
#define ML_CDECL
#define ML_STDCALL
#endif

#ifndef ML_EXTERN_C
#ifdef __cplusplus
#define ML_EXTERN_C extern "C"
#define ML_DEFAULT(val) = val
#else
#define ML_EXTERN_C
#define ML_DEFAULT(val)
#endif
#endif

#if ( defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64 ) && defined MLRVML_EXPORTS
#define ML_EXPORTS __declspec(dllexport)
#else
#define ML_EXPORTS
#endif

#ifndef MLAPI
#define MLAPI(rettype) ML_EXTERN_C ML_EXPORTS rettype ML_CDECL
#endif


typedef unsigned char                UCHAR; //!<无符号8位整数
typedef char                            SCHAR;    //!<有符号8位整数
typedef unsigned short int          USHORT;  //!<无符号16位整数
typedef signed short int            SSHORT;   //!<有符号16位整数

typedef unsigned int                 UINT;     //!<无符号32位整数
typedef signed int                   SINT;      //!<有符号32位整数

#if !defined _WIN32 && !defined _WIN64
typedef unsigned long long int    ULONG;     //!<无符号64位整数
#endif

typedef signed long long int       SLONG;     //!<有符号64位整数

typedef float                         FLOAT;     //!<32位浮点型
typedef double                        DOUBLE;    //!<64位浮点型

typedef UCHAR                           BYTE;   //!<无符号8位字符型

enum camModelType {Affine = 0 , NonAffine = 1} ;//!<定义相机畸变模型类别
enum camProjection {backProjOff = 0 , backProjOn = 1} ;//!<定义相机标定模式
enum delErrorType {delOff = 0 , delOn = 1} ;//!<定义是否开启粗差剔除模式

#define BUFFER_RADIUS 2 //!>定义缓冲区半径


typedef enum enImgType
{
    /*! Unknown or unspecified type */ 		    T_Unknown = 0,
    /*! Eight bit unsigned integer */           T_Byte = 1,
    /*! Sixteen bit unsigned integer */         T_UInt16 = 2,
    /*! Sixteen bit signed integer */           T_Int16 = 3,
    /*! Thirty two bit unsigned integer */      T_UInt32 = 4,
    /*! Thirty two bit signed integer */        T_Int32 = 5,
    /*! Thirty two bit floating point */        T_Float32 = 6,
    /*! Sixty four bit floating point */        T_Float64 = 7,
    T_TypeCount = 12          /* maximum type # + 1 */
}ImgDotType;
/**
* @struct tagRECT
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩形结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagMLRECT
{
    DOUBLE    left;//!<左上角X坐标
    DOUBLE    top;//!<左上角Y坐标
    DOUBLE    right;//!<右下角X坐标
    DOUBLE    bottom;//!<右下角Y坐标
    tagMLRECT()
    {
        left = top = right = bottom = 0.0;
    }
} DbRect;
/**
* @struct tagmlRect
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 矩形类公共结构
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagmlRect
{
    DOUBLE dXMin;//!<左上角X坐标
    DOUBLE dYMin;//!<左上角Y坐标
    DOUBLE dXMax;//!<右下角X坐标
    DOUBLE dYMax;//!<右下角Y坐标
    tagmlRect()
    {
        dXMin = dYMin = dXMax = dYMax = 0.0;
    }
} MLRect;
/*定义公共常量*/
#define ML_PI   3.1415926535897932384626433832795
#define ML_E    2.7182818284590452353602874713526
#define ML_LOG2 0.69314718055994530941723212145818
#define ML_DOUBLEMIN 1.0e-300
#define ML_ZERO 1.0e-7
#define ML_MoonRadius 1738000
/*定义常用函数*/
#define ML_SWAP(a,b,t) ( (t) = (a), (a) = (b), (b) = (t) )

#ifndef MIN
#define MIN(a,b) ( (a) > (b) ? (b) : (a) )
#endif

#ifndef MAX
#define MAX(a,b) ( (a) < (b) ? (b) : (a) )
#endif

#ifndef UINT_MAX
#define UINT_MAX 4294967295
#endif

#ifndef DOUBLE_MAX
#define DOUBLE_MAX 1.0e308
#endif

#ifndef DOUBLE_MIN
#define DOUBLE_MIN -1.0e308
#endif

#define RGB2GRAY( r,g,b ) (((b)*117 + (g)*601 + (r)*306 ) >> 10 )

/**
* @brief 角度转弧度
*/
#define Deg2Rad(d) ( d / 180.0 * ML_PI ) //!<角度转弧度

/**
* @brief 弧度转角度
*/
#define Rad2Deg(d) ( d * 180.0 / ML_PI ) //!<弧度转角度

/*****************************************************************
函数名称：图像信息公共结构，包括内外方位元素
作    者：万文辉
功能描述：所有图像均以左下角为原点，横向为x，纵向为y。外方位元素角元素单位为弧度
输    入：
输    出：
版本编号:   1.0
修改历史:    <作者>    <时间>   <版本编号>    <描述>
******************************************************************/
/**
* @struct OriAngle
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 外方位角元素
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
struct OriAngle
{
    DOUBLE omg;//!<绕X轴转角
    DOUBLE phi;//!<绕Y轴转角
    DOUBLE kap;//!<绕Z轴转角
    OriAngle()
    {
        omg = phi = kap = 0.0;
    }
};

/**
* @struct PT2
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 二维点结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
template <typename T>
struct PT2
{
    T X, Y;//!<点X,Y坐标
    ULONG lID;//!<点序号
    BYTE byType;
    BYTE byIsMatch;
    PT2()
    {
        memset(this, 0, 2*sizeof(T));
        lID = 0;
        byType = 0;
        byIsMatch = 0;
    }
};
/**
* @struct PT3
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 三维点结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
template <typename T>
struct PT3
{
    T X, Y, Z;//!<点X,Y,Z坐标
    ULONG lID;//!<点序号

    PT3()
    {
        memset(this, 0, 3*sizeof(T));
        lID = 0;
    }
};

typedef PT2<DOUBLE> Pt2d;//!<二维DOUBLE型结构体
typedef PT2<FLOAT> Pt2f;//!<二维FLOAT型结构体
typedef PT2<SLONG> Pt2l;//!<二维SLONG型结构体
typedef PT2<SINT> Pt2i;//!<二维SINT型结构体

typedef PT3<BYTE>  Pt3b;//!<三维BYTE型结构体
typedef PT3<FLOAT> Pt3f;//!<三维FLOAT型结构体
typedef PT3<DOUBLE> Pt3d;//!<三维FLOAT型结构体




/**
* @struct struExteriorOrientation
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 外方位元素结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct struExteriorOrientation
{
    Pt3d pos;//!<外方位线元素
    OriAngle ori;//!<外方位角元素
} ExOriPara;
/**
* @struct enCamType
* @date 2011.11.02
* @author 吴凯 wukai@irsa.ac.cn
* @brief 相机类型
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef enum enCamType
{
    NULL_Cam = 0,//!<无类型
    Pan_Cam = 1, //!<全景相机
    Nav_Cam = 2, //!<导航相机
    Haz_Cam = 3  //!<避障相机

} CAMTYPE;
/**
* @struct struSINTeriorOrientation
* @date 2011.11.02
* @author 吴凯 wukai@irsa.ac.cn
* @brief 相机内参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/


typedef struct struSINTeriorOrientation
{
    DOUBLE f;//!<焦距
    DOUBLE f2;//!<焦距
    DOUBLE x;//!<像主点坐标x
    DOUBLE y;//!<像主点坐标y
    DOUBLE k1;//!<径向畸变系数k1
    DOUBLE k2;//!<径向畸变系数k2
    DOUBLE k3;//!<径向畸变系数k3
    DOUBLE p1;//!<径向畸变系数p1
    DOUBLE p2;//!<径向畸变系数p2
    DOUBLE alpha;//!<正交改正系数alpha
    DOUBLE beta;//!<正交改正系数beta
    DOUBLE dPixelS;//!<像素大小，单位为毫米
    UINT   nType;//!<畸变模型类型，其中，1表示为普通的畸变模型，即切向畸变及径向畸变，此模型下主点位置均以像素为单位
    DOUBLE dSkew;
    CAMTYPE camType;//!<相机类型
    struSINTeriorOrientation()
    {
        f = x = y = k1 = k2 = k3 = p1 = p2 = alpha = beta = dSkew = 0.0;
        dPixelS = 1.0;
        nType = 1;
    }

} InOriPara;


/**
* @struct tagStruFrameImageInfo
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief  单幅影像信息结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagStruFrameImageInfo
{
    SINT nSiteID;//!<站点序号
    SINT nRollID;//!<影像圈号
    SINT nImgID;//!<影像序号
    UINT nCamID;//!<相机类型
    UINT nW;//!<影像宽
    UINT nH;//!<影像高
    string strName;//!<影像名
    string strImgPath;//!<影像路径
    InOriPara inOri;//!<影像内方位
    ExOriPara exOri;//!<影像外方位
    UINT nImgType; //!<影像类型，1为原始影像，2为畸变校正影像
    UINT nImgIndex;
    tagStruFrameImageInfo()
    {
        nSiteID = -1;
        nRollID = -1;
        nCamID = 0;
        nImgID = -1;
        strImgPath = "";
        nImgType = 0;
        nImgIndex = 0;
        nW = nH = 0;
    }

} FrameImgInfo;

/**
* @struct tagStruStereoImageInfo
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 立体影像信息结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagStruStereoImageInfo
{
    SINT nSiteID;//!<站点序号
    SINT nRollID;//!<影像圈号
    SINT nImgID;//!<影像序号
    UINT nCamID;//!<相机类型

    FrameImgInfo imgLInfo;
    FrameImgInfo imgRInfo;
    SINT nStereoLevel;
    tagStruStereoImageInfo()
    {
        nSiteID = -1;
        nRollID = -1;
        nImgID = -1;
        nStereoLevel = 0;//!<表示影像类型，1为原始影像，2为核线影像对
    }
} StereoSet;
/**
* @struct tagImgPtSe
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 单幅影像和匹配点结构
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagImgPtSet
{
    FrameImgInfo imgInfo;
    vector< Pt2d > vecPts;
    vector< Pt2d > vecAddPts;
} ImgPtSet;

/**
* @struct struStereoMatch
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 立体匹配点结构
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct struStereoMatch: public Pt3d
{
    //由三维点结构派生，包含了左影像点，右影像点（即七点结构）
    Pt2d ptLInImg;//!<左影像点坐标
    Pt2d ptRInImg;//!<右影像点坐标
    DOUBLE dRelaCoef;//!<匹配系数
    struStereoMatch()
    {
        dRelaCoef = 0.0;
    }
} StereoMatchPt;

/**
* @struct ObstacleMapPara
* @date 2011.12.18
* @author 李巍
* @brief 计算障碍分布图计算参数结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
struct ObstacleMapPara
{
    DOUBLE dSlopeCoef;    //!<坡度对障碍图的系数
    DOUBLE dMaxSlope;    //!<最大坡度门限
    // dSlopCoef * SlopeValue / dMaxSlope 为障碍代价函数贡献值，以下类似
    DOUBLE dRoughnessCoef;    //!<粗糙度系数
    DOUBLE dMaxRoughness;    //!<最大粗糙度门限
    DOUBLE dStepCoef;    //!<阶梯系数
    DOUBLE dMaxStep;    //!<最大阶梯系数
    DOUBLE dMaxObstacleValue;    //!<最大障碍代价门限
    //障碍代价函数超过该值则视之为障碍
};


/**
* @struct struLineEo
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像原始测控数据线元素
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct struLineEo
{
    DOUBLE dEoTime;//!<外方位元素时间
    Pt3d pos;//!<外方位线元素
} LineEo;

/**
* @struct tagCE1IOPara
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像原始测控数据角元素
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct struAngleEo
{
    DOUBLE dEoTime;//!<外方位元素时间
    OriAngle ori;//!<外方位角元素
} AngleEo;

/**
* @struct tagCE1IOPara
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CE-1卫星影像内定向参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagCE1IOPara
{
    DOUBLE f;//!<焦距
    DOUBLE s0;//!<摄影中心列号
    DOUBLE l0;//!<摄影中心行号
    DOUBLE pixsize;//!<像素大小（米）
    DOUBLE x0;//!<焦平面x坐标（米）
    DOUBLE y0;//!<焦平面y坐标（米）
    UINT nCCD_line;//!<前视、下视或后视影像成像时所用的CCD行
    bool upflag;//!<上下行标志，上行为true，下行为flase
    UINT nSample;//!<影像宽
    UINT nLine;//!<影像高

    tagCE1IOPara()
    {
        x0 = 0.0000113;
        y0 = -0.0000112;
        s0 = 255.5;
        l0 = 511.5;
        pixsize = 0.000014;
        f = 0.023334;
        nSample = 512;
    }
} CE1IOPara;
/**
* @struct tagCE2IOPara
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CE-2卫星影像内定向参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagCE2IOPara
{
    DOUBLE f;//!<焦距
    DOUBLE s0;//!<摄影中心的列号
    DOUBLE pixsize;//!<像素大小（米）
    DOUBLE x0;//!<焦平面x坐标（米）
    DOUBLE y0;//!<焦平面y坐标（米）
    DOUBLE AngleDeg;//!<前视或下视与主光轴的夹角(角度)
    bool upflag;//!<影像宽上下行标志，上行为true，下行为false
    UINT nSample;//!<影像宽
    UINT nLine;//!<影像高
    tagCE2IOPara()
    {
        AngleDeg = 0;
        s0 = 3071.5;
        x0 = 0.0000093;
        y0 = 0.0000000;
        pixsize = 0.0000101;
        f = 0.1443;
        nSample = 6144;
    }
} CE2IOPara;
/**
* @struct tagStruSatProj
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像DEM及DOM生成工程参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagStruSatProj
{
    string sLimgPath;//!<左影像路径
    string sRimgPath;//!<右影像路径
    CE1IOPara CE1LimgIO;//!<CE-1卫星左影像内定向参数
    CE1IOPara CE1RimgIO;//!<CE-1卫星右影像内定向参数
    CE2IOPara CE2LimgIO;//!<CE-2卫星左影像内定向参数
    CE2IOPara CE2RimgIO;//!<CE-2卫星右影像内定向参数
    vector<ExOriPara> LimgEo;//!<左影像外方位元素
    vector<ExOriPara> RimgEo;//!<右影像外方位元素
    string sDemPath;//!<DEM生成路径
    string sDomPath;//!<DEM生成路径
    //vector<StereoMatchPtt> vecPts;//!<匹配点及物方坐标
    vector<Pt3d> XYZRms;//!<物方坐标精度
} SatProj;

/**
* @struct tagBlockOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像分块参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagBlockOptions
{
    UINT nColThres;//!<影像列方向分块大小阈值
    UINT nRowThres;//!<影像行方向分块大小阈值
    UINT nOverlayW;//!<影像列方向重叠区大小
    UINT nOverlayH;//!<影像行方向重叠区大小
    UINT nBlockWidth;//!<影像初始分块宽
    UINT nBlockHeight;//!<影像初始分块高
    tagBlockOptions()
    {
        nColThres = 2000;
        nRowThres = 2000;
        nOverlayW = 200;
        nOverlayH = 200;
        nBlockWidth = 512;
        nBlockHeight = 512;
    }

} BlockOptions;

/**
* @struct tagSatOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像制图参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagSatOptions
{
    string sMissionName;//!<任务名称
    UINT nMaxCheck;//!<Sift匹配最大检查点数量
    DOUBLE dRatio;//!<Sift匹配比率
    DOUBLE dSigma;//!<Ransac仿射变换算法限差
    UINT nMinItera;//!<Ransac仿射变换算法最小迭代次数
    DOUBLE dThresh;//!<Ransac单应矩阵算法阈值
    DOUBLE dConfidence;//!<Ransac单应矩阵算法置信度
    UINT nStep;//!<密集匹配步长
    SINT nRadiusX;//!<密集匹配范围X方向半径大小
    SINT nRadiusY;//!<密集匹配范围Y方向半径大小
    SINT nTemplateSize;//!<密集匹配模板大小
    DOUBLE dCoef;//!< 相关系数阈值
    SINT nXOffSet;//!<密集匹配X方向偏移量
    SINT nYOffSet;//!<密集匹配Y方向偏移量
    UINT nBands;//!<影像波段数
    DOUBLE nodata;//!<DEM或DOM的无数据区值
    BlockOptions BlockOps;//!<影像分块参数
    DOUBLE XResolution;//!<DEM及DOM在X方向分辨率
    DOUBLE YResolution;//!<DEM及DOM在Y方向分辨率
    bool bLeftBaseFlag;//!<DOM生成参照图像
    DOUBLE B0;//!<投影割角
    tagSatOptions()
    {
        nMaxCheck = 200;
        dRatio = 0.5;
        dSigma = 5.5;
        nMinItera = 2000;
        dThresh = 1.0;
        dConfidence = 0.99;
        nStep = 5;
        nRadiusX = nRadiusY = 5;
        nTemplateSize = 5;
        dCoef = 0.75;
        nXOffSet = nYOffSet = 0;
        nBands = 1;
        nodata = 999999;
        XResolution = 150;
        YResolution = 150;
        bLeftBaseFlag = true;
        B0 = 35;
    }
} SatOptions;

/**
* @struct tagSiftMatchParaOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief Sift匹配参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagSiftMatchParaOptions
{
    UINT nMaxCheck;//!<Sift匹配最大检查点数量
    DOUBLE dRatio;//!<Sift匹配比率
    tagSiftMatchParaOptions()
    {
        nMaxCheck = 200;
        dRatio = 0.5;
    }
} SiftMatchPara;
/**
* @struct tagASiftMatchParaOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief Sift匹配参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagASiftMatchParaOptions
{
    SINT  nNumTilts ;//!<ASift匹配仿射变换角度变换参数
    tagASiftMatchParaOptions()
    {
        nNumTilts = 8;
    }
} ASiftMatchPara;
/**
* @struct tagRanSACAffineParaOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief RANSAC（仿射变换模型）参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagRanSACAffineParaOptions
{
    DOUBLE dSigma;//!<RANSAC阈值
    DOUBLE dMinItera;//!<RANSAC最小迭代次数
    tagRanSACAffineParaOptions()
    {
        dSigma = 1.0;
        dMinItera = 1000;
    }
} RANSACAffineModPara;
/**
* @struct tagRanSACHomeParaOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief RANSAC（单应矩阵模型）参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagRanSACHomeParaOptions
{
    DOUBLE dConfidence;//!<RANSAC置信度参数
    DOUBLE dThres;//!<RANSAC阈值
    tagRanSACHomeParaOptions()
    {
        dConfidence = 0.99;
        dThres = 3;
    }
} RANSACHomePara;
/**
* @struct tagLocalByMatchOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 基于匹配的定位算法参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagLocalByMatchOptions
{
    SiftMatchPara stuSiftPara;//!<Sift匹配参数
    RANSACAffineModPara stuRANSACPara;//!<RANSAC参数
} LocalByMatchOpts;

/**
* @struct tagMatchInRegPara
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 区域模板匹配参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagMatchInRegPara
{
    DOUBLE dXMin;//!<X方向区域最小视差
    DOUBLE dYMin;//!<Y方向区域最大视差
    DOUBLE dXMax;//!<X方向区域最小视差
    DOUBLE dYMax;//!<Y方向区域最大视差
    DOUBLE dXOff;//!<X方向区域偏移值
    DOUBLE dYOff;//!<X方向区域偏移值
    UINT nTempSize;//!<模板大小
    DOUBLE dCoefThres;//!<相关系数阈值

    tagMatchInRegPara()
    {
        dXMin = -200.0;
        dXMax = 0.0;
        dYMin = -2;
        dYMax = 2;
        dXOff = dYOff = 0.0;
        nTempSize = 15;
        dCoefThres = 0.75;
    }
} MatchInRegPara;
/**
* @struct tagLocalBy2SitesOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像制图参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagLocalBy2SitesOptions
{
    DOUBLE dCamH;//!<相机高度
    ASiftMatchPara stuASiftPara;//!<ASift匹配参数
    DOUBLE dFSiteDis;//!<前站距离参数
    DOUBLE dESiteDis;//!<后站距离参数
    MatchInRegPara stuMatchPara;//!<区域匹配参数
    tagLocalBy2SitesOptions()
    {
        dCamH = 1.5;
        dFSiteDis = 12.0;
        dESiteDis = 4.0;
    }
}
LocalBy2SitesOpts;

/**
* @struct tagLocalBySequenceImgOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像制图参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagLocalBySequenceImgOptions
{
    DOUBLE dZoomCoef;//!<用于匹配的缩放系数
    SiftMatchPara stuSiftPara;//!<Sift匹配参数
    RANSACAffineModPara stuRANSACPara;//!<Ransac参数
}
LocalBySeqImgOpts;
/**
* @struct tagLocalBySequenceImgOptions
* @date 2011.12.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 卫星影像制图参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagLocalByLanderOpts
{
    MatchInRegPara stuMatchPara;//!<区域匹配参数
    SiftMatchPara stuASiftPara;//!<ASift匹配参数
    RANSACAffineModPara stuRANSACPara;//!<RANSAC粗差剔除参数
}
LocalByLanderOpts;

/**
* @struct tagBaseOptions
* @date 2011.12.18
* @author 彭嫚
* @brief 长基线分析参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagBaseOptions
{
    DOUBLE dFixBase;//!<全景相机固定基线长
    DOUBLE dPixel;//!<像素匹配误差
    DOUBLE dTarget;//!<目标距离
    UINT nWidth;//!<全景相机像幅宽度
    DOUBLE dThresHold;//!<牛顿迭代法阈值
    UINT nIterTime;//!<牛顿迭代法迭代次数
    tagBaseOptions()
    {
        dThresHold=0.000001;
        nIterTime=60;
    }
} BaseOptions;


/**
* @struct tagWideOptions
* @date 2011.12.18
* @author 彭嫚
* @brief 长基线影像制图参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct  tagWideOptions
{
    UINT nMaxCheck;//!<Sift匹配最大检查点数量
    DOUBLE dRatio;//!<Sift匹配比率
    DOUBLE dThresh;//!<Ransac算法阈值
    DOUBLE dConfidence;//!<Ransac算法置信度度
    SINT nStep;//!<密集匹配格网大小
    SINT nRadiusX;//!<密集匹配范围X方向半径大小
    SINT nRadiusY;//!<密集匹配范围Y方向半径大小
    SINT nTemplateSize;//!<密集匹配模板大小
    DOUBLE dCoef;//!< 相关系数阈值
    bool bIsUsingFeatPt;//!< 使用特征点或密集点生成地形
    bool bIsUsingPartion;//!< 逐个像对生成DEM或所有像对生成整个区域的DEM
    SINT nXOffSet;//!<密集匹配X方向偏移量
    SINT nYOffSet;//!<密集匹配Y方向偏移量
    UINT nBands;//!<影像波段数
    DOUBLE nodata;//!<DEM或DOM的无数据区值
    DOUBLE XResolution;//!<DEM及DOM在X方向分辨率
    DOUBLE YResolution;//!<DEM及DOM在Y方向分辨率
    tagWideOptions()
    {
        nMaxCheck = 200;
        dRatio = 0.6;
        dThresh = 0.05;
        dConfidence = 0.99;
        nStep = 5;
        nRadiusX = 5;
        nRadiusY = 2;
        nTemplateSize = 13;
        dCoef = 0;
        bIsUsingFeatPt = false;
        bIsUsingPartion = false;
        nXOffSet = nYOffSet = 0;
        nBands = 1;
        nodata = 999999;
        XResolution = 0.1;
        YResolution = 0.1;
    }
} WideOptions;


/**
* @struct tagTransMat
* @date 2011.12.18
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 坐标转换关系矩阵 包含旋转矩阵和平移向量
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagTransMat
{

    SINT nDim;         //!<坐标转换维数 默认为3 切勿另改它值！
    DOUBLE dMat[9];   //!<旋转矩阵3*3  逐行排列
    DOUBLE dVec[3];   //!<平移向量
    tagTransMat()
    {
        nDim = 3;
        dMat[0] = 0.0;
        dMat[1] = 0.0;
        dMat[2] = 0.0;
        dMat[3] = 0.0;
        dMat[4] = 0.0;
        dMat[5] = 0.0;
        dMat[6] = 0.0;
        dMat[7] = 0.0;
        dMat[8] = 0.0;

        dVec[0] = 0.0;
        dVec[1] = 0.0;
        dVec[2] = 0.0;
    }


} TransMat;

/**
* @struct tagExtractFeature
* @date 2011.12.18
* @author 万文辉 whwan@irsa.ac.cn
* @brief 特征点提取参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagExtractFeature
{
    SINT nGridSize;//!<特征点提取时影像划分格网大小
    SINT nPtMaxNum;//!<特征点提取的最大数目
    DOUBLE dThresCoef;
    tagExtractFeature()
    {
        nGridSize = 10;
        nPtMaxNum = 0;
        dThresCoef = 1.0;
    }
} ExtractFeatureOpt;


typedef struct tagGaussianFilter
{
    SINT nTemplateSize;//!<滤波模板大小
    DOUBLE dCoef;//!<滤波核参数
    tagGaussianFilter()
    {
        nTemplateSize = 7;
        dCoef = 0.8;
    }
} GaussianFilterOpt;

typedef struct tagmlRectSearch
{
    DOUBLE dXMin;//!<左上角X坐标
    DOUBLE dYMin;//!<左上角Y坐标
    DOUBLE dXMax;//!<右下角X坐标
    DOUBLE dYMax;//!<右下角Y坐标
    tagmlRectSearch()
    {
        dXMin = dYMin = dXMax = dYMax = 0.0;
    }
} MLRectSearch;

/**
* @struct RMS2d
* @date 2011.12.18
* @author 万文辉 whwan@irsa.ac.cn
* @brief 2维点误差
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
typedef struct tagStu2dRMS
{
    ULONG lID;
    DOUBLE rmsX;
    DOUBLE rmsY;
    DOUBLE rmsAll;
    tagStu2dRMS()
    {
        lID = 0;//!<点号
        rmsX = 0;//!<点X方向误差
        rmsY = 0;//!<点Y方向误差
        rmsAll = 0;//!<点总误差
    }
} RMS2d;

typedef struct tagFilterPara
{
    UINT nFilterSize;
    DOUBLE dThresCoef;
    SINT nThres;
    tagFilterPara()
    {
        nFilterSize = 5;
        dThresCoef = 0.1;
        nThres = 5;
    }
}MedFilterOpts;
typedef struct tagWallisFilterPara
{
    UINT nTemplateSize;
    DOUBLE dExpectMean;
    DOUBLE dExpectVar;
    DOUBLE dCoefA;
    DOUBLE dCoefAlpha;
    tagWallisFilterPara()
    {
        nTemplateSize = 43;
        dExpectMean = 128;
        dExpectVar = 200;
        dCoefA = 60;
        dCoefAlpha = 0.4;
    }
}WallisFPara;

typedef struct tagInstallMatrix
{
    DOUBLE dOriMatrix[9];
    DOUBLE dPosMatrix[3];
    tagInstallMatrix()
    {
        for(UINT i = 0; i < 3; ++i )
        {
            dPosMatrix[i] = 0;
            dOriMatrix[3*i] = 0;
            dOriMatrix[3*i+1] = 0;
            dOriMatrix[3*i+2] = 0;
        }
    }
}stuInsMat;


typedef struct stuPolygon2d
{
    ULONG nID;
    vector<Pt2d> vecVectex;
}Polygon2d;

typedef struct stuPolygon3d
{
    ULONG nID;
    vector<Pt3d> vecVectex;
}Polygon3d;
typedef struct stuBlockDeal
{
    UINT nBlockH;
    vector<UINT> vecnDisp;
}stuBlockDeal;





#endif


























