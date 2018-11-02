#ifndef CMLPROJMANAGE_H
#define CMLPROJMANAGE_H

#include "mlBase.h"
//typedef enum enProjectType
//{
//    SITE = 0,//单站点、多站点、全景
//    SATELLITE = 1//卫星影像工程
//
//}PROTYPE;
//
////typedef enum enCamType
////{
////    Pan = 0, //全景相机
////    Nav = 1  //导航相机
////
////}CAMTYPE;
///*********************************************
//地面影像工程文件结构
//相机数量
//相机类型1  左内参数 右内参数
//相机类型1  左内参数 右内参数
//.......
//影像数量
//站点号 相机类型 圈号 影像顺序号 左文件名 右文件名 左影像外方位 右影像外方位 影像级别
//站点号 相机类型 圈号 影像顺序号 左文件名 右文件名 左影像外方位 右影像外方位 影像级别
//.......
//
//**********************************************/
//typedef struct tagStruStereoImageInfo
//{
//    SINT nSiteID;
//    SINT nCamType;
//    SINT nRollID;
//    SINT nImgID;
//    string strLImgPath;
//    string strRImgPath;
//    ExOriPara exOriL;
//    ExOriPara exOriR;
//    SINT nLevel;
//}STRUSTEREOINFO;
///*********************************************
//卫星影像工程文件结构
//相机数量
//相机类型1 内参数
//相机类型2 内参数
//相机类型3 内参数
//影像文件名
//影像时间文件名
//线元素参数文件名
//角元素参数文件名
//**********************************************/
//
//class CmlProjManage
//{
//    public:
//        CmlProjManage();
//        virtual ~CmlProjManage();
//
//    public:
//        bool OpenProject(SCHAR *strFileName, PROTYPE nType);
//        /////////////////////////////////////////////////////////////
//        vector<InOriPara> m_vecInOriPara;
//        vector<STRUSTEREOINFO> m_vecStereoInfo;
//
//        /////////////////////////////////////////////////////////////
//
//        string m_strSatImgFileName;
//        vector< double > m_vecImgTime;
//        vector< ExOriPara > m_vecPos;//取其中omg存储时间
//        vector< ExOriPara > m_vecAngle;//取其中X存储时间
//
//        ///
//
//    protected:
//    private:
//};

#endif // CMLPROJMANAGE_H
