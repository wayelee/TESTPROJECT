/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlRasterMosaic.h
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 块拼接头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/

#ifndef CMLRASTERMOSAIC_H
#define CMLRASTERMOSAIC_H

#include "mlGdalDataset.h"
#include "gdal_header.h"
#include "gdal/gdal_priv.h"
#include "gdal/gdal.h"
#include "gdal/gdalwarper.h"
#include "gdal/ogr_spatialref.h"
#include "gdal/ogr_api.h"
#include "gdal/cpl_string.h"
//#include "opencv.hpp"
#include "Panorama.h"

/**
* @class CmlRasterMosaic
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 块拼接类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlRasterMosaic
{

public:

    vector<string> files;
    string out_file;
    int bTargetAlignedPixels;
    int nForcePixels, nForceLines, bQuiet;
    int bEnableDstAlpha, bEnableSrcAlpha;
    int bVRT;
    int bCreateOutput;
    int i;
    int bHasGotErr;
    int bCropToCutline;
    int bOverwrite;
    int bMulti;

    const char *cFormat;
    char **cInputFiles;
    char *cOutputFile;

    void *hTransformArg, *hGenImgProjArg, *hApproxArg;
    char **cWarpOptions;
    double dErrorThreshold;
    double dWarpMemoryLimit;
    double m_dMinX, m_dMinY, m_dMaxX, m_dMaxY;
    double m_dXRes, m_dYRes;
    char **cCreateOptions;

    const char *cSrcNodata;
    const char *cDstNodata;

    char **cTO;
    char *pszCutlineDSName;
    char *cCLayer, *pszCWHERE, *pszCSQL;
    void *hCutline;
    GDALDataType eOutputType, eWorkingType;
    GDALResampleAlg eResampleAlg;
    GDALTransformerFunc pfnTransformer;
    GDALDatasetH hDstDS;


public:

    /**
    * @fn CmlRasterMosaic
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 拼接构造函数
    * @param inputFiles 输入文件路径
    * @param outputFile 输出文件路径
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    CmlRasterMosaic(vector<string> inputFiles, string outputFile)
    {
        files = inputFiles;
        out_file = outputFile;
    }

    /**
    * @fn CmlRasterMosaic
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 拼接空参构造函数
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    CmlRasterMosaic()
    {
    }

    /**
    * @fn ~CmlRasterMosaic
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 拼接析构函数
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    virtual ~CmlRasterMosaic()
    {

    }

    /**
    * @fn mlDEMMosaic
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief DEM拼接函数，利用输入文件的地理信息
    * @param cInputFiles 输入DEM
    * @param cOutputFile 输出DEM
    * @param dXRes X方向分辨率
    * @param dYRes Y方向分辨率
    * @param nResample  采样方法对应的值
    * @param nDisCultLine  裁切线值
    * @retval 1 成功
    * @retval 0 失败
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    int mlDEMMosaic(vector<string> cInputFiles, const SCHAR* cOutputFile, DOUBLE dXRes, DOUBLE dYRes, SINT nResample, SINT nDisCultLine);


private:

    /**
    * @fn mlInitCoordSys
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 初始化输入DEM坐标
    * @param cInputCoordSys 输入坐标系统
    * @return 坐标系统标识
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    char *mlInitCoordSys( const char *cInputCoordSys );

    /**
    * @fn mlCreateOutputFile
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 创建输出文件
    * @param cSrcFiles 输入源文件
    * @param cFilename 输出文件
    * @param cFormat 输出图像格式
    * @param papszTO 输出文件坐标系
    * @param cCreateOptions 创建图像选项
    * @param eDT 波段每个像素的字节
    * @param nInputFileNum 输入文件数量
    * @retval GDALDatasetH 返回
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    GDALDatasetH mlCreateOutputFile(char **cSrcFiles, const char *cFilename, const char *cFormat, char **papszTO, char ***cCreateOptions, GDALDataType eDT, int nInputFileNum);


    void LoadCutline( const char *pszCutlineDSName, const char *pszCLayer, const char *pszCWHERE, const char *pszCSQL, void **phCutlineRet );


    void TransformCutlineToSource( GDALDatasetH hSrcDS, void *hCutline, char ***ppapszWarpOptions, char **papszTO_In );
    //bool mlDeSeam();
};

/**
* @struct GeoTransformStruct
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 仿射变换参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
struct GeoTransformStruct
{
    char* cFileName;//!<输入文件名
    int nID;//!<输入DEM的ID
    double dTopLeftX;//!<左上角X坐标
    double dTopLeftY;//!<左上角Y坐标
    double dXRes;//!<X方向分辨率
    double dYRes;//!<Y方向分辨率
    int nXSize;//!<原始长度
    int nYSize;//!<原始宽度
    double dLowRightX;//!<右下角X坐标
    double dLowRightY;//!<右下角X坐标
};

/**
* @struct OverlapStruct
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 重叠区矩形范围参数
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
struct OverlapStruct
{
    pair<int, int> pirID;//!<点的ID号
    double dTopLeftX;//!<左上角X坐标
    double dTopLeftY;//!<左上角Y坐标
    double dLowRightX;//!<右下角X坐标
    double dLowRightY;//!<右下角X坐标
};


/**
* @class CmlInvCyilindricalProject
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 反圆柱投影类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlInvCyilindricalProject
{
public:
    cv::Size size;
    double focal;
    double r[9];
    double rinv[9];
    double scale;
public:
    /**
    * @fn mlSetTransformation
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 透视变换
    * @param 透视变换矩阵
    * @retval 无
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    void mlSetTransformation(const cv::Mat& R);

    /**
    * @fn mlMapForward
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 正向圆柱投影
    * @param x 投影前X坐标
    * @param y 投影前Y坐标
    * @param u 投影后X坐标
    * @param v 投影后Y坐标
    * @retval 无
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    void mlMapForward(double x, double y, double &u, double &v)
    {
        x -= size.width * 0.5f;
        y -= size.height * 0.5f;

        float x_ = r[0] * x + r[1] * y + r[2] * focal;
        float y_ = r[3] * x + r[4] * y + r[5] * focal;
        float z_ = r[6] * x + r[7] * y + r[8] * focal;

        u = scale * atan2f(x_, z_);
        v = scale * y_ / sqrtf(x_ * x_ + z_ * z_);
    }

    /**
    * @fn mlMapBackward
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 反向圆柱投影
    * @param u 反向投影前X坐标
    * @param v 反向投影前Y坐标
    * @param x 反向投影后X坐标
    * @param y 反向投影后Y坐标
    * @retval 无
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    void mlMapBackward(double u, double v, double &x, double &y)
    {
        float x_ = sinf(u / scale);
        float y_ = v / scale;
        float z_ = cosf(u / scale);

        float z;
        x = rinv[0] * x_ + rinv[1] * y_ + rinv[2] * z_;
        y = rinv[3] * x_ + rinv[4] * y_ + rinv[5] * z_;
        z = rinv[6] * x_ + rinv[7] * y_ + rinv[8] * z_;

        x = focal * x / z + size.width * 0.5f;
        y = focal * y / z + size.height * 0.5f;
    }
};


/**
* @class CmlPano2Prespective
* @date 2011.11.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 全景影像生成透视影像
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlPano2Prespective
{
public:
    /**
    * @fn CmlPano2Prespective
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 全景影像生成透视影像构造函数
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    CmlPano2Prespective()
    {

    }

    /**
    * @fn ~CmlPano2Prespective
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 全景影像生成透视影像构造函数
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    virtual ~CmlPano2Prespective()
    {

    }

public:
    int m_nPanoW;
    int m_nPanoH;
    cv::Size src_size_;
    CmlInvCyilindricalProject projector_;
public:
    /**
    * @fn mlPano2Prespective
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 正向圆柱投影
    * @param cInputPanoFile 输入全景图像
    * @param cOutputImage 输出透视图像
    * @param ptOriginal 全景图像中待生成透视图像的左上角坐标
    * @param nPanoW 全景图像的宽度
    * @param nPanoH 全景图像的高度
    * @param dFocus 相机焦距
    * @retval 无
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlPano2Prespective( const SCHAR *cInputPanoFile, const SCHAR * cOutputImage, Pt2i ptOriginal, SINT nPanoW, SINT nPanoH, DOUBLE dFocus);

};

/**
* @struct struImageFeatures
* @date 2011.01.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 全景拼接库定义的特征点结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
//struct ImageFeatures
//{
//    int img_idx;
//    cv::Size img_size;
//    std::vector<cv::KeyPoint> keypoints;
//    cv::Mat descriptors;
//};

/**
* @struct struMatchesInfo
* @date 2011.01.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 全景拼接库定义的特征点结构体
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
//struct MatchesInfo
//{
//    MatchesInfo();
//    MatchesInfo(const MatchesInfo &other);
//    const MatchesInfo& operator =(const MatchesInfo &other);
//
//    int src_img_idx, dst_img_idx;       // Images indices (optional)
//    std::vector<cv::DMatch> matches;
//    std::vector<uchar> inliers_mask;    // Geometrically consistent matches mask
//    int num_inliers;                    // Number of geometrically consistent matches
//    cv::Mat H;                          // Estimated homography
//    double confidence;                  // Confidence two images are from the same panorama
//
//    std::vector<cv::KeyPoint> vecSrcPt;  // src_img_idx图像上的匹配点
//
//};


/**
* @class CmlPanoInterface
* @date 2011.01.18
* @author 梁健 liangjian@irsa.ac.cn
* @brief 与全景拼接库的接口转换类
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
class CmlPanoInterface
{
public:
    //bool mlFirstRun(vector<char*> &cParamList, vector<ImgPtSet> &vecOriImage, vector<double> &dExtParam, char* cOutputFile, vector<PanoMatchInfo> &vecPanoMatchInfo);
    /**
    * @fn mlExportMatchPts
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 输出相邻图像间的匹配点
    * @param vecParam 生成全景图像的参数
    * @param vecFrmInfo 原始图像信息
    * @param vecImgPtSets 输出点信息
    * @param strOutPath 输出点文件路径
    * @retval 无
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlExportMatchPts(vector<char*> vecParam, vector<FrameImgInfo> vecFrmInfo, vector<ImgPtSet> &vecImgPtSets, char* strOutPath, bool &bNeedAddPts);

    /**
    * @fn mlImportMatchPts
    * @date 2011.11.22
    * @author 梁健 liangjian@irsa.ac.cn
    * @brief 导入相邻图像间的匹配点
    * @param vecParam 生成全景图像的参数
    * @param vecFrmInfo 原始图像信息
    * @param vecImgPtSets 输出点信息
    * @param strOutPath 生成全景图像路径
    * @retval 无
    * @version 1.0
    * @par 修改历史:
    * <作者>  <时间>  <版本编号>  <描述>\n
    */
    bool mlImportMatchPts(vector<char*> vecParam, vector<FrameImgInfo> vecFrmInfo, vector<ImgPtSet> &vecImgPtSets, char* strOutPath);

public:
    CmlPanoInterface()
    {
    }
    virtual ~CmlPanoInterface()
    {
    }

private:
    bool mlLoadPrjFile(string sPrjPath);



};

////class DjSets
////{
////public:
////    DjSets(int n = 0)
////    {
////        create(n);
////    }
////
////    void create(int n);
////    int find(int elem);
////    int merge(int set1, int set2);
////
////    std::vector<int> parent;
////    std::vector<int> size;
////
////private:
////    std::vector<int> rank_;
////};
////
////
////class FeaturesMatcher
////{
////public:
////    virtual ~FeaturesMatcher() {}
////
////    void operator ()(const struImageFeatures &features1, const struImageFeatures &features2, struMatchesInfo& matches_info)
////    {
////        match(features1, features2, matches_info);
////    }
////
////    void operator ()(const std::vector<struImageFeatures> &features, std::vector<struMatchesInfo> &pairwise_matches);
////
////    bool isThreadSafe() const
////    {
////        return is_thread_safe_;
////    }
////
////protected:
////    FeaturesMatcher(bool is_thread_safe = false) : is_thread_safe_(is_thread_safe) {}
////
////    virtual void match(const struImageFeatures &features1, const struImageFeatures &features2,
////                       struMatchesInfo& matches_info) = 0;
////
////    bool is_thread_safe_;
////};
////
////
////class BestOf2NearestMatcher : public FeaturesMatcher
////{
////public:
////    BestOf2NearestMatcher(bool try_use_gpu = true, float match_conf = 0.55f, int num_matches_thresh1 = 6, int num_matches_thresh2 = 6);
////
////protected:
////    void match(const struImageFeatures &features1, const struImageFeatures &features2, struMatchesInfo &matches_info);
////    int num_matches_thresh1_;
////    int num_matches_thresh2_;
////    cv::Ptr<FeaturesMatcher> impl_;
////};
////

























#endif // CMLRASTERMOSAIC_H

