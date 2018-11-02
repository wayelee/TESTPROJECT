#ifndef __PANODATA__
#define __PANODATA__

using namespace std;

typedef unsigned char           UCHAR;  //!>无符号8位整数
typedef char                    SCHAR;  //!>有符号8位整数
typedef unsigned short int      USHORT; //!>无符号16位整数
typedef signed short int        SSHORT; //!>有符号16位整数
typedef unsigned int            UINT;   //!>无符号32位整数
typedef signed int              SINT;   //!>有符号32位整数
#if defined _WIN32 || defined WIN32 || defined _WIN64 || defined WIN64 

#else
typedef unsigned long long int  ULONG;  //!>无符号64位整数
#endif

typedef signed long long int    SLONG;  //!>有符号64位整数
typedef float                   FLOAT;  //!>32位浮点型
typedef double                  DOUBLE; //!>64位浮点型
typedef UCHAR                   BYTE;   //!>无符号8位字符型

/**
* @struct PT2
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 二维点结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
//template <typename T>
//struct PT2
//{
//    T X, Y;//!>点X,Y坐标
//    ULONG lID;//!>点序号
//    BYTE byType;
//    PT2()
//    {
//        memset(this, 0, 2*sizeof(T));
//        lID = 0;
//        byType = 0;
//    }
//};

//typedef PT2<DOUBLE> Pt2d;//!>二维DOUBLE型结构体
//
//typedef struct tagPanoPt3d
//{
//    double X, Y, Z;//!>三维点结构
//    string ID;//!>三维点点号
//
//    tagPanoPt3d()
//    {
//        memset(this, 0, 3*sizeof(double));
//    }
//} PanoPt3d;

//typedef enum enPanoCamType
//{
//    NULL_Cam = 0,//!>无类型
//    Pan_Cam = 1, //!>全景相机
//    Nav_Cam = 2  //!>导航相机
//
//} PANOCAMTYPE;
//
//
//struct PanoOriAngle
//{
//    DOUBLE omg;//!>绕X轴转角
//    DOUBLE phi;//!>绕Y轴转角
//    DOUBLE kap;//!>绕Z轴转角
//    PanoOriAngle()
//    {
//        omg = phi = kap = 0.0;
//    }
//};
//
//typedef struct struPanoExteriorOrientation
//{
//    PanoPt3d pos;//!>外方位线元素
//    PanoOriAngle ori;//!>外方位角元素
//} PanoExOriPara;
//
//typedef struct struPanoSINTeriorOrientation
//{
//    DOUBLE f;//!>焦距
//    DOUBLE x;//!>像主点坐标x
//    DOUBLE y;//!>像主点坐标y
//    DOUBLE k1;//!>径向畸变系数k1
//    DOUBLE k2;//!>径向畸变系数k2
//    DOUBLE k3;//!>径向畸变系数k3
//    DOUBLE p1;//!>径向畸变系数p1
//    DOUBLE p2;//!>径向畸变系数p2
//    DOUBLE alpha;//!>正交改正系数alpha
//    DOUBLE beta;//!>正交改正系数beta
//    struPanoSINTeriorOrientation()
//    {
//        f = x = y = k1 = k2 = k3 = p1 = p2 = alpha = beta = 0.0;
//    }
//
//} PanoInOriPara;
//
//typedef struct tagPanoCamInfo
//{
//    PANOCAMTYPE CamType;
//    PanoInOriPara inOriL;
//    PanoInOriPara inOriR;
//    SINT nLW;
//    SINT nLH;
//    SINT nRW;
//    SINT nRH;
//}PANOCAMINFO;
//
//
//typedef struct tagPanoStruOriImageInfo
//{
//    SINT nSiteID;//!>站点序号
//    PANOCAMTYPE CamType;//!>相机类型
//    UINT nW;//!>影像宽
//    UINT nH;//!>影像高
//    SINT nRollID;//!>影像圈号
//    SINT nImgID;//!>影像序号
//    string strName;//!>影像名
//    string strImgPath;//!>影像路径
//    PanoInOriPara inOri;//!>影像内方位
//    PanoExOriPara exOri;//!>影像外方位
//    SINT nImgLevel; //!>影像类型，1为原始影像，2为核线影像
//    UINT nImgIndex;//!>影像index
//    tagPanoStruOriImageInfo()
//    {
//        nSiteID = -1;
//        CamType = NULL_Cam;
//        nRollID = -1;
//        nImgID = -1;
//        strImgPath = "";
//        nImgLevel = -1;
//        nImgIndex = 0;
//    }
//
//} OriImgInfo;

//typedef struct tagImgPtSet
//{
//    OriImgInfo imgInfo;
//    vector< Pt2d > vecPts;
//}panoImgPtSet;

#endif
