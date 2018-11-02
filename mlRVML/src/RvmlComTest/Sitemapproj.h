#ifndef CSITEMAPPROJ_H
#define CSITEMAPPROJ_H
//#include "imageviewer.h"
#include "../../include/mlTypes.h"
#include "../../include/mlRVML.h"

#ifndef NULL_VALUE
#define NULL_VALUE -99999
#endif

#ifndef ALL_VALUE
#define ALL_VALUE -1
#endif
typedef struct tagFrameCamInfo
{
    UINT nCamID;
    InOriPara inOri;
    UINT nW;
    UINT nH;
    tagFrameCamInfo()
    {
        nCamID = 0;
        nW = nH = 0;
    }
}FrameCamInfo;


typedef struct tagStruRollImage
{
    SINT nRollID;
    vector<StereoSet> vecStereoSet;
    tagStruRollImage()
    {
        nRollID = NULL_VALUE;
    }
}RollImgSet;

typedef struct tagStruSiteImage
{
    SINT nSiteID;
    vector<RollImgSet> vecRollSet;
    tagStruSiteImage()
    {
        nSiteID = NULL_VALUE;
    }
}SiteImgSet;

class CmlSiteMapProj
{
    public:
        CmlSiteMapProj( );
        CmlSiteMapProj( string strPath );
        virtual ~CmlSiteMapProj();

        bool LoadProj( const string strPath );
        SiteImgSet* GetSiteSet( int nSiteID );
        RollImgSet* GetRollSet( SiteImgSet* pSiteSet, int nRollID );
        StereoSet* GetStereoSet( RollImgSet* pRollSet, int nImgID );

        bool GetDealSet( int nSiteID, int nRollID, int nImgID, vector<StereoSet> &vecDealSSet );

        bool GetAllFrmInfo( vector<FrameImgInfo> &vecFrmInfos );
        void GetImgSet(vector<StereoSet> &vecStereoImg);
        void GetProjPath(string &strProjPath);
        void GetFrmCamInfo(vector<FrameCamInfo> &vecFrmCamInfo);
        void GetFrmImgInfo(vector<FrameImgInfo> &vecFrmImgInfo);
    public:
        bool CreateDemAndDom( const double dXLT, const double dYLT, const double dXRB, const double dYRB, const double dResolution, \
                                    const SINT nSiteID, const SINT nRollID, const SINT nImgID, const bool bIsUsingFeatPt, const bool bIsUsingPartition, \
                                    const SCHAR* strDemPath, const SCHAR* strDomPath ); // 若SiteID 和 RollID为负数，则表示选择了所有的站或角度的影像进行处理

        bool LocalByBundleResection( const SCHAR* strGCPPath, const SCHAR* strResOutPath  );

        bool LocalInTwoSite( SINT nStartSiteID, SINT nEndSiteID, const SCHAR* strOutPath );

        bool Mosaic( vector<char*> vecPara, char *cOutputFile);

        bool CreateWideDem(vector<StereoSet> vecStereoSet, WideOptions WidePara,vector<ImgPtSet>& vecFPtSetL, vector<ImgPtSet>& vecFPtSetR, vector<ImgPtSet>& vecDPtSetL, vector<ImgPtSet>& vecDPtSetR, SCHAR *strDemFile);

        bool CreateDmf(vector<StereoSet>vecStereoSet, WideOptions WidePara, vector<ImgPtSet>& vecDPtL, vector<ImgPtSet>& vecDPtR);

        bool LocalBySeqImg( const SCHAR* strSatDom, const SCHAR* strOutPath );

        bool GetSiteCenter(Pt3d& ptCenter) ;

        bool CreateImage( const SCHAR* strPathL, const SCHAR* strPathR, const SCHAR* strOutL, const SCHAR* strOutR );

    protected:
    private:
        string m_strProjPath;
        bool m_bIsValid;

        vector<SiteImgSet> m_vecSiteImgSet;
        vector<StereoSet> m_vecStereoImgSet;

        vector<FrameCamInfo> m_vecFrmCamInfo;
        vector<FrameImgInfo> m_vecFrmImgInfo;

//        bool getMarkedPt( const string strControlPtPath, vector<Pt2d> &vecPts, \
//                                           vector<InOriPara> &vecInOri, vector<ExOriPara> &vecExOri, vector<Pt3d> &vecGCPs );


};

#endif // CSITEMAPPROJ_H
