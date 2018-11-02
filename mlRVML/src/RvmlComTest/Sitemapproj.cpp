#include "Sitemapproj.h"
#include <fstream>
#include <algorithm>

#include "PtsFileManage.h"
#include "PtFilesRW.h"
#include "../../src/mlRVML/mlStereoProc.h"
#include "../../src/mlRVML/mlSiteMapping.h"
#include "../../src/mlRVML/mlWBaseProc.h"
#include "../../3rdParty/mlSiftMatch/SiftMatch.h"

CmlSiteMapProj::CmlSiteMapProj()
{
    m_bIsValid = false;
}

CmlSiteMapProj::~CmlSiteMapProj()
{
    //dtor
}
CmlSiteMapProj::CmlSiteMapProj( string strPath )
{
    this->LoadProj( strPath );
}
void CmlSiteMapProj::GetImgSet(vector<StereoSet> &vecStereoImg)
{

    vecStereoImg = m_vecStereoImgSet;
}

void CmlSiteMapProj::GetProjPath(string &strProjPath)
{
    strProjPath = m_strProjPath;
}

void CmlSiteMapProj::GetFrmCamInfo(vector<FrameCamInfo> &vecFrmCamInfo)
{
    vecFrmCamInfo = m_vecFrmCamInfo;
}
void CmlSiteMapProj::GetFrmImgInfo(vector<FrameImgInfo> &vecFrmImgInfo)
{
    vecFrmImgInfo = m_vecFrmImgInfo;
}
bool CmlSiteMapProj::LoadProj( string strPath )
{
    fstream stm;
    stm.open( strPath.c_str() );

    vector<FrameCamInfo> vecTempCameraInfo;
    vector<FrameImgInfo> vecTempFrmImgInfo;
    vector<StereoSet> vecStereoInfo;
    if( stm )
    {
        char cHead[128];
        stm >> cHead;

        char *cFormatHead = "ML_SITE_INFO_FILE";

        if( 0 != strcmp( cHead, cFormatHead )  )
        {
            stm.close();
            return false;
        }

        double dVersion = 0;
        stm >> dVersion;

        while( !stm.eof() )
        {
            char cTempHead[128];
            stm >> cTempHead;

            if( 0 == strcmp( cTempHead, "FILED_FRAME_CAMERA_INFO") )
            {
                int nCamNum = 0;
                stm >> nCamNum;

                for( int i = 0; i < nCamNum; ++i )
                {
                    FrameCamInfo camInfo;
                    stm >> camInfo.nCamID >> camInfo.nW >> camInfo.nH >> camInfo.inOri.f >> camInfo.inOri.x >> camInfo.inOri.y \
                    >> camInfo.inOri.k1 >> camInfo.inOri.k2 >> camInfo.inOri.k3 >> camInfo.inOri.p1 >> camInfo.inOri.p2 >> camInfo.inOri.alpha >> camInfo.inOri.beta;
                    vecTempCameraInfo.push_back( camInfo );
                }
                continue;
            }

            if( 0 == strcmp( cTempHead, "FILED_FRAME_IMAGE_INFO" ) )
            {
                int nImgNum = 0;
                stm >> nImgNum;
                for( int i = 0; i < nImgNum; ++i )
                {
                    FrameImgInfo frmImgInfo;
                    char sTempPath[256];
                    stm >> frmImgInfo.strName >> frmImgInfo.nCamID >> frmImgInfo.exOri.pos.X >> frmImgInfo.exOri.pos.Y >> frmImgInfo.exOri.pos.Z \
                    >> frmImgInfo.exOri.ori.omg >> frmImgInfo.exOri.ori.phi >> frmImgInfo.exOri.ori.kap >> frmImgInfo.nImgType;

                    frmImgInfo.exOri.ori.omg = Deg2Rad( frmImgInfo.exOri.ori.omg );
                    frmImgInfo.exOri.ori.phi = Deg2Rad( frmImgInfo.exOri.ori.phi );
                    frmImgInfo.exOri.ori.kap = Deg2Rad( frmImgInfo.exOri.ori.kap );

                    vecTempFrmImgInfo.push_back( frmImgInfo );
                }
                continue;
            }

            if( 0 == strcmp( cTempHead, "FIELD_STEREO_INFO" ) )
            {
                int nStereoInfo = 0;
                stm >> nStereoInfo;
                for( int i = 0; i < nStereoInfo; ++i )
                {
                    char sTempPathL[256], sTempPathR[256];
                    StereoSet sTempSet;
                    stm >> sTempSet.imgLInfo.strName >> sTempSet.imgRInfo.strName >> sTempSet.nSiteID >> sTempSet.nRollID >> sTempSet.nImgID >> sTempSet.nStereoLevel;
                    vecStereoInfo.push_back( sTempSet );
                }
            }
        }
        stm.close();
    }
    else
    {
        return false;
    }

    m_vecFrmCamInfo = vecTempCameraInfo;

    SINT nStrPos = strPath.rfind("/");
    string sTempPath;
    sTempPath.assign(strPath, 0, nStrPos );

    for( int i = 0; i < vecTempFrmImgInfo.size(); ++i )
    {
        FrameImgInfo *pFrmInfo = &vecTempFrmImgInfo[i];
        UINT nCurCamID = pFrmInfo->nCamID;

        for( int j = 0; j < m_vecFrmCamInfo.size(); ++j )
        {
            FrameCamInfo* pCurCamInfo = &m_vecFrmCamInfo[j];
            if(  pCurCamInfo->nCamID = nCurCamID )
            {
                pFrmInfo->inOri = pCurCamInfo->inOri;
                pFrmInfo->nH = pCurCamInfo->nH;
                pFrmInfo->nW = pCurCamInfo->nW;
            }
        }

        string strTempProjPath = sTempPath;
        strTempProjPath.append( pFrmInfo->strName );
        pFrmInfo->strImgPath = strTempProjPath;
    }
    m_vecFrmImgInfo = vecTempFrmImgInfo;

    CmlPtsFileManage clsPtFiles;
    SINT nStrTempidxPos = strPath.rfind(".");

    string sTempIndexPath;
    sTempIndexPath.assign(strPath, 0, nStrTempidxPos );
    sTempIndexPath += ".idx";

    vector<string> vecTempNamePath;
    for( int i = 0; i < m_vecFrmImgInfo.size(); ++i )
    {
        vecTempNamePath.push_back( m_vecFrmImgInfo[i].strName );
    }

    if( false == clsPtFiles.LoadIndex( sTempIndexPath ) )
    {
        if( false == clsPtFiles.CreateIndex( sTempIndexPath, vecTempNamePath ) )
        {
            return false;
        }
    }

    for( int i = 0; i < m_vecFrmImgInfo.size(); ++i )
    {
        string strName = m_vecFrmImgInfo[i].strName;

        unsigned int nIndex = 0;
        nIndex = clsPtFiles.GetIDByStrPath( strName );

        if( nIndex != UINT_MAX )
        {
            m_vecFrmImgInfo[i].nImgIndex = nIndex;
        }

    }

    for( int i = 0; i < vecStereoInfo.size(); ++i )
    {
        string strL, strR;
        StereoSet *pCurStereoSet = &vecStereoInfo[i];

        strL = pCurStereoSet->imgLInfo.strName;
        strR = pCurStereoSet->imgRInfo.strName;

        for( int j = 0; j < m_vecFrmImgInfo.size(); ++j )
        {
            FrameImgInfo *pFrmCurInfo = &m_vecFrmImgInfo[j];

            if( strL == m_vecFrmImgInfo[j].strName )
            {
                pFrmCurInfo->nSiteID = pCurStereoSet->nSiteID;
                pFrmCurInfo->nRollID = pCurStereoSet->nRollID;
                pFrmCurInfo->nImgID = pCurStereoSet->nImgID;
//                int nTPos = pFrmCurInfo->strName.rfind( "/" );
//                string strTempName;
//                strTempName.assign( pFrmCurInfo->strName, nTPos, strlen( pFrmCurInfo->strName.c_str() ) );
//
                pCurStereoSet->imgLInfo = *pFrmCurInfo;

            }
            if( strR == m_vecFrmImgInfo[j].strName )
            {
                pFrmCurInfo->nSiteID = pCurStereoSet->nSiteID;
                pFrmCurInfo->nRollID = pCurStereoSet->nRollID;
                pFrmCurInfo->nImgID = pCurStereoSet->nImgID;
//                int nTPos = pFrmCurInfo->strName.rfind( "/" );
//                string strTempName;
//                strTempName.assign( pFrmCurInfo->strName, nTPos, strlen( pFrmCurInfo->strName.c_str() ) );
//
                pCurStereoSet->imgRInfo = *pFrmCurInfo;
            }
        }
    }

    m_vecStereoImgSet = vecStereoInfo;


//
//        ///////////////////////////////////////////////////////////////
//
//        //////////////////////////////////////////////////////////////////////

    //分站（SITE）排列
    vector<SINT> vecSiteNo;
    vector<SINT>::iterator it;
    for( int i = 0; i < m_vecStereoImgSet.size(); ++i )
    {
        StereoSet* pSet = &m_vecStereoImgSet[i];
        it = std::find( vecSiteNo.begin(), vecSiteNo.end() , pSet->nSiteID );
        if( it == vecSiteNo.end() )
        {
            vecSiteNo.push_back( pSet->nSiteID );
        }
    }
    vector< vector<SINT> > vecSiteRoll;
    for( int i = 0; i < m_vecStereoImgSet.size(); ++i )
    {
        vector<SINT> vecRollID;
        vector<SINT>::iterator it;
        for( int j = 0; j < m_vecStereoImgSet.size(); ++j )
        {
            StereoSet* pSet = &m_vecStereoImgSet[j];
            if( pSet->nSiteID == vecSiteNo[i] )
            {
                it = std::find( vecRollID.begin(), vecRollID.end() , pSet->nRollID );
                if( it == vecRollID.end() )
                {
                    vecRollID.push_back( pSet->nRollID );
                }
            }
        }
        vecSiteRoll.push_back( vecRollID );
    }
    for( int i = 0; i < vecSiteNo.size(); ++i )
    {
        SiteImgSet siteSet;
        siteSet.nSiteID = vecSiteNo[i];
        for( int j = 0; j < vecSiteRoll[i].size(); ++j )
        {
            RollImgSet rollSet;
            rollSet.nRollID = (vecSiteRoll[i])[j];
            for( int k = 0; k < m_vecStereoImgSet.size(); ++k )
            {
                StereoSet* pSet = &m_vecStereoImgSet[k];
                if( ( pSet->nSiteID == vecSiteNo[i] )&&( pSet->nRollID == rollSet.nRollID) )
                {
                    rollSet.vecStereoSet.push_back( *pSet );
                }
            }
            siteSet.vecRollSet.push_back( rollSet );
        }
        m_vecSiteImgSet.push_back( siteSet );
    }
    m_bIsValid = true;
    m_strProjPath = strPath;
    return true;
}
bool CmlSiteMapProj::GetAllFrmInfo( vector<FrameImgInfo> &vecFrmInfos )
{
    vecFrmInfos = m_vecFrmImgInfo;
    return true;
}
SiteImgSet* CmlSiteMapProj::GetSiteSet( int nSiteID )
{
    if( nSiteID < 0 )
    {
        return NULL;
    }
    else
    {
        for( int i = 0; i < m_vecSiteImgSet.size(); ++i )
        {
            if( nSiteID == m_vecSiteImgSet[i].nSiteID )
            {
                return &(m_vecSiteImgSet.at(i));
            }
        }
        return NULL;
    }
}
RollImgSet* CmlSiteMapProj::GetRollSet( SiteImgSet* pSiteSet, int nRollID )
{
    if( ( pSiteSet != NULL )&&( nRollID >= 0  ) )
    {
        for( int i = 0; i < pSiteSet->vecRollSet.size(); ++i )
        {
            if(nRollID == pSiteSet->vecRollSet[i].nRollID )
            {
                return &(pSiteSet->vecRollSet.at(i));
            }
        }
        return NULL;
    }
    else
    {
        return NULL;
    }
}
StereoSet* CmlSiteMapProj::GetStereoSet( RollImgSet* pRollSet, int nImgID )
{
    if( ( pRollSet != NULL )&&( nImgID >= 0  ) )
    {
        for( int i = 0; i < pRollSet->vecStereoSet.size(); ++i )
        {
            if(nImgID == pRollSet->vecStereoSet[i].nImgID )
            {
                return &(pRollSet->vecStereoSet.at(i));
            }
        }
        return NULL;
    }
    else
    {
        return NULL;
    }
}
bool CmlSiteMapProj::GetDealSet( int nSiteID, int nRollID, int nImgID, vector<StereoSet> &vecDealSSet )
{
    if( nSiteID < 0 )
    {
        vecDealSSet = m_vecStereoImgSet;
    }
    else
    {
        SiteImgSet* pTempSite = GetSiteSet( nSiteID );
        if( pTempSite != NULL )
        {
            if( nRollID < 0 )
            {
                for( int i = 0; i < pTempSite->vecRollSet.size(); ++i )
                {
                    RollImgSet* pTempRollSet = &(pTempSite->vecRollSet.at(i));
                    for( int j = 0; j < pTempRollSet->vecStereoSet.size(); ++j )
                    {
                        StereoSet* pTempStereoSet = &( pTempRollSet->vecStereoSet.at(j));
                        vecDealSSet.push_back( *pTempStereoSet );
                    }
                }
            }
            else
            {
                RollImgSet* pTempRollSet = GetRollSet( pTempSite, nRollID );
                if( pTempRollSet != NULL )
                {
                    if( nImgID < 0 )
                    {
                        for( int j = 0; j < pTempRollSet->vecStereoSet.size(); ++j )
                        {
                            StereoSet* pTempStereoSet = &( pTempRollSet->vecStereoSet.at(j));
                            vecDealSSet.push_back( *pTempStereoSet );
                        }
                    }
                    else
                    {
                        StereoSet* pTempStereoSet = GetStereoSet( pTempRollSet, nImgID );
                        if( pTempStereoSet != NULL )
                        {
                            vecDealSSet.push_back( *pTempStereoSet );
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
    }
    return true;
}
// 若SiteID 和 RollID为负数，则表示选择了所有的站或角度的影像进行处理
bool CmlSiteMapProj::CreateDemAndDom( const double dXLT, const double dYLT, const double dXRB, const double dYRB, const double dResolution, \
                                      const SINT nSiteID, const SINT nRollID, const SINT nImgID, const bool bIsUsingFeatPt, const bool bIsUsingPartition, \
                                      const char* strDemPath, const char* strDomPath  )
{

    vector<StereoSet> vecStereoSet;
    if ( false == this->GetDealSet( nSiteID, nRollID, nImgID, vecStereoSet ))
    {
        return false;
    }

    CmlSiteMapping clsSiteMapping;
    vector<string> vecStrMatchFiles;
    string strFoldDirectory;
    int nPos = m_strProjPath.rfind("/");
    strFoldDirectory.assign( m_strProjPath, 0, nPos );
    strFoldDirectory.append( "/MatchRes/" );


    for( int i = 0; i < vecStereoSet.size(); ++i )
    {
        StereoSet* pSSet = &vecStereoSet.at(i);
        int nTempSiteID = pSSet->nSiteID;
        char cSiteID[20];
        sprintf( cSiteID, "%d", nTempSiteID );
        string strTempFDirect = strFoldDirectory;
        strTempFDirect.append( cSiteID );

        if( access( strTempFDirect.c_str(), 0 ) == -1 )
        {
            if( mkdir( strTempFDirect.c_str(), 0777) )
            {
                return false;
            }
        }

        if( true == bIsUsingFeatPt )
        {
            strTempFDirect.append( "/" );
            string strTLPath = strTempFDirect;
            string strTRPath = strTempFDirect;

            SINT nTTPos = pSSet->imgLInfo.strName.rfind( "/");
            string strLTempHead;
            strLTempHead.assign( pSSet->imgLInfo.strName, nTTPos+1, pSSet->imgLInfo.strName.length() );
            SINT nTTBPos = strLTempHead.rfind( "." );
            string strLTempFinal;
            strLTempFinal.assign( strLTempHead, 0, nTTBPos );
            strTLPath.append( strLTempFinal );
            strTLPath.append( ".fmf" );
            vecStrMatchFiles.push_back( strTLPath );

            nTTPos = pSSet->imgRInfo.strName.rfind( "/");
            string strRTempHead;
            strRTempHead.assign( pSSet->imgRInfo.strName, nTTPos+1, pSSet->imgRInfo.strName.length() );
            nTTBPos = strRTempHead.rfind( "." );
            string strRTempFinal;
            strRTempFinal.assign( strRTempHead, 0, nTTBPos );

            strTRPath.append( strRTempFinal );
            strTRPath.append( ".fmf" );
            vecStrMatchFiles.push_back( strTRPath );
        }
        else
        {
//          strTempFDirect.append( "/" );
//            string strTLPath = strTempFDirect;
//            string strTRPath = strTempFDirect;
//
//            SINT nTTPos = pSSet->imgLInfo.strName.rfind( "/");
//            string strLTempHead;
//            strLTempHead.assign( pSSet->imgLInfo.strName, nTTPos, pSSet->imgLInfo.strName.length() );
//
//
//            strTLPath.append( strLTempHead );
//            strTLPath.append( ".dmf" );
//            vecStrMatchFiles.push_back( strTLPath );
//
//            nTTPos = pSSet->imgRInfo.strName.rfind( "/");
//            string strRTempHead;
//            strRTempHead.assign( pSSet->imgRInfo.strName, nTTPos, pSSet->imgRInfo.strName.length() );
//
//            strTRPath.append( strRTempHead );
//            strTRPath.append( ".dmf" );
//            vecStrMatchFiles.push_back( strTRPath );
        }
    }
    if( true == bIsUsingPartition )
    {
//        Pt2d ptLT, ptRB;
//        ptLT.X = dXLT;
//        ptLT.Y = dYLT;
//        ptRB.X = dXRB;
//        ptRB.Y = dYRB;
//        clsSiteMapping.MapByMultiBlock( vecStereoSet, vecStrMatchFiles, ptLT, ptRB, dResolution, strDemPath, strDomPath, bIsUsingFeatPt );
    }
    else
    {
        Pt2d ptLT, ptRB;
        ptLT.X = dXLT;
        ptLT.Y = dYLT;
        ptRB.X = dXRB;
        ptRB.Y = dYRB;

        vector< ImgPtSet > vecImgPtSets;
        CPtFilesRW clsPtRW;
        for( int i = 0; i < vecStrMatchFiles.size(); ++i )
        {
            ImgPtSet imgPSet;
            clsPtRW.LoadImgPtSet( vecStrMatchFiles[i], imgPSet );
            vecImgPtSets.push_back( imgPSet );
        }
        mlMapByInteBlock( vecStereoSet, vecImgPtSets, ptLT, ptRB, dResolution, strDemPath, strDomPath );

        for( int i = 0; i < vecStrMatchFiles.size(); ++i )
        {
            clsPtRW.SaveImgPtSet( vecStrMatchFiles[i], vecImgPtSets[i] );
        }

        return true;
 //       clsSiteMapping.MapByInteBlock( vecStereoSet, vecStrMatchFiles, ptLT, ptRB, dResolution, strDemPath, strDomPath, bIsUsingFeatPt );

    }
}

bool CmlSiteMapProj:: CreateWideDem(vector<StereoSet> vecStereoSet, WideOptions WidePara,vector<ImgPtSet>& vecFPtSetL, vector<ImgPtSet>& vecFPtSetR, vector<ImgPtSet>& vecDPtSetL, vector<ImgPtSet>& vecDPtSetR, SCHAR *strDemFile)
{

    vector<string> vecStrMatchFiles;
    string strFoldDirectory;
    int nPos = m_strProjPath.rfind("/");
    strFoldDirectory.assign( m_strProjPath, 0, nPos );
    strFoldDirectory.append( "/MatchRes/" );


    for( int i = 0; i < vecStereoSet.size(); ++i )
    {
        StereoSet* pSSet = &vecStereoSet.at(i);
        int nTempSiteID = pSSet->nSiteID;
        char cSiteID[20];
        sprintf( cSiteID, "%d", nTempSiteID );
        string strTempFDirect = strFoldDirectory;
         strTempFDirect.append( cSiteID );

        if( access( strTempFDirect.c_str(), 0 ) == -1 )
        {
            if( mkdir( strTempFDirect.c_str(), 0777) )
            {
                return false;
            }
        }


        strTempFDirect.append( "/" );
        string strTLPath = strTempFDirect;
        string strTRPath = strTempFDirect;

        SINT nTTPos = pSSet->imgLInfo.strName.rfind("/");
        string strLTempHead;
        strLTempHead.assign(pSSet->imgLInfo.strName, nTTPos+1,pSSet->imgLInfo.strName.length());
        SINT nTTBPos = strLTempHead.rfind(".");
        string strLTempFinal;
        strLTempFinal.assign(strLTempHead, 0, nTTBPos);
        strTLPath.append( strLTempFinal);
        strTLPath.append( ".dmf" );
        vecStrMatchFiles.push_back( strTLPath );

        nTTPos = pSSet->imgRInfo.strName.rfind("/");
        strLTempHead;
        strLTempHead.assign(pSSet->imgRInfo.strName, nTTPos+1,pSSet->imgRInfo.strName.length());
        nTTBPos = strLTempHead.rfind(".");
        strLTempFinal;
        strLTempFinal.assign(strLTempHead, 0, nTTBPos);
        strTRPath.append( strLTempFinal);
        strTRPath.append( ".dmf" );
        vecStrMatchFiles.push_back( strTRPath );


    }
    mlWideBaseMapping(vecStereoSet,WidePara,vecFPtSetL, vecFPtSetR, vecDPtSetL, vecDPtSetR, strDemFile);
    //将匹配的点输出到文件中
    CPtFilesRW clsPtRW;
    for( int i = 0; i < vecStereoSet.size(); ++i )
    {
        StereoSet* pStereoSet = &vecStereoSet.at(i);
        string strLCurPath = vecStrMatchFiles[2*i];
        string strRCurPath = vecStrMatchFiles[2*i+1];

        //如果已经生成dmf文件，则退出
        ImgPtSet tempPtL = vecDPtSetL[i];
        ImgPtSet tempPtR = vecDPtSetR[i];


        //将ImgPtSet数据写入两个dmf文件中
        clsPtRW.SaveImgPtSet(strLCurPath, tempPtL);
        clsPtRW.SaveImgPtSet(strRCurPath, tempPtR);


    }

    return true;
}


bool CmlSiteMapProj::Mosaic(vector<char*>vecParam, char *cOutputFile)
{
    bool bNeedAddPts = false;
    CmlSiteMapping clsSiteMapping;
    vector<string> vecStrMatchFiles;
    string strFoldDirectory;
    int nPos = m_strProjPath.rfind("/");
    strFoldDirectory.assign( m_strProjPath, 0, nPos );
    strFoldDirectory.append( "/MatchRes" );

    string strTempFDirect = strFoldDirectory;
    for( int i = 0; i < m_vecFrmImgInfo.size(); ++i )
    {
        FrameImgInfo* pFrmInfo = &m_vecFrmImgInfo[i];
        int nTempSiteID = pFrmInfo->nSiteID;
        if( nTempSiteID != -1 )
        {
            char cSiteID[20];
            sprintf( cSiteID, "%d", nTempSiteID );

            strTempFDirect.append( cSiteID );
        }

        if( access( strTempFDirect.c_str(), 0 ) == -1 )
        {
            if( mkdir( strTempFDirect.c_str(), 0777) )
            {
                return false;
            }
        }

        strTempFDirect.append( "/" );
        string strTLPath = strTempFDirect;
        string strTRPath = strTempFDirect;

        SINT nTTPos = pFrmInfo->strName.rfind( "/");
        string strLTempHead;
        strLTempHead.assign( pFrmInfo->strName, nTTPos+1, pFrmInfo->strName.length() );
        SINT nTTBPos = strLTempHead.rfind( "." );
        string strLTempFinal;
        strLTempFinal.assign( strLTempHead, 0, nTTBPos );
        strTLPath.append( strLTempFinal );
        strTLPath.append( ".fmf" );
        vecStrMatchFiles.push_back( strTLPath );

    }
    vector< ImgPtSet > vecImgPtSets;
    CPtFilesRW clsPtRW;
    int nPtSize = 0;
    for( int i = 0; i < vecStrMatchFiles.size(); ++i )
    {
        ImgPtSet imgPSet;
        clsPtRW.LoadImgPtSet( vecStrMatchFiles[i], imgPSet );
        nPtSize += imgPSet.vecPts.size();
        vecImgPtSets.push_back( imgPSet );
    }

//    for(int i = 0; i < vecImgPtSets.size(); i++)
//    {
//        vecImgPtSets[i].vecPts.clear();
//    }
    if(nPtSize == 0)
    {
        mlPanoMatchPts(vecParam, m_vecFrmImgInfo, vecImgPtSets, cOutputFile, bNeedAddPts);
        if(!bNeedAddPts)
        {
            mlPanoMosic(vecParam, m_vecFrmImgInfo, vecImgPtSets, cOutputFile);
        }

        vector<Pt2d> vecTest;
        for( int i = 0; i < vecImgPtSets.size(); ++i )
        {
            for(int j = 0; j < vecImgPtSets[i].vecPts.size(); ++j )
            {
                vecTest.push_back(vecImgPtSets[i].vecPts[j]);
            }
        }


    }
    else
    {
        mlPanoMosic(vecParam, m_vecFrmImgInfo, vecImgPtSets, cOutputFile);
    }


//        shu chu vector

    for( int i = 0; i < vecStrMatchFiles.size(); ++i )
    {
        clsPtRW.SaveImgPtSet( vecStrMatchFiles[i], vecImgPtSets[i] );
    }

    return true;

}




bool CmlSiteMapProj::LocalByBundleResection( const SCHAR* strGCPPath, const SCHAR* strResOutPath  )
{
    vector<Pt3d> vecGcps;
    CPtFilesRW clsPtRW;
    clsPtRW.LoadGCPPts( strGCPPath, vecGcps );

    vector<ImgPtSet> vecImgPts;

    string strFoldDirectory;
    int nPos = m_strProjPath.rfind("/");
    strFoldDirectory.assign( m_strProjPath, 0, nPos );
    strFoldDirectory.append( "/MatchRes/" );

    vector<string> vecMarkedfiles;
    for( SINT i = 0; i < this->m_vecStereoImgSet.size(); ++i )
    {
        StereoSet* pSSet = &m_vecStereoImgSet.at(i);
        int nTempSiteID = pSSet->nSiteID;
        char cSiteID[20];
        sprintf( cSiteID, "%d", nTempSiteID );
        string strTempFDirect = strFoldDirectory;
        strTempFDirect.append( cSiteID );

        if( access( strTempFDirect.c_str(), 0 ) == -1 )
        {
            if( mkdir( strTempFDirect.c_str(), 0777) )
            {
                return false;
            }
        }

        strTempFDirect.append( "/" );
        string strTLPath = strTempFDirect;
        string strTRPath = strTempFDirect;

        SINT nTTPos = pSSet->imgLInfo.strName.rfind( "/");
        string strLTempHead;
        strLTempHead.assign( pSSet->imgLInfo.strName, nTTPos+1, pSSet->imgLInfo.strName.length() );
        SINT nTTBPos = strLTempHead.rfind( "." );
        string strLTempFinal;
        strLTempFinal.assign( strLTempHead, 0, nTTBPos );
        strTLPath.append( strLTempFinal );
        strTLPath.append( ".fmf" );
        ImgPtSet imgSetL, imgSetR;
        clsPtRW.LoadImgPtSet( strTLPath, imgSetL );

        vecImgPts.push_back( imgSetL );

        nTTPos = pSSet->imgRInfo.strName.rfind( "/");
        string strRTempHead;
        strRTempHead.assign( pSSet->imgRInfo.strName, nTTPos+1, pSSet->imgRInfo.strName.length() );
        nTTBPos = strRTempHead.rfind( "." );
        string strRTempFinal;
        strRTempFinal.assign( strRTempHead, 0, nTTBPos );

        strTRPath.append( strRTempFinal );
        strTRPath.append( ".fmf" );

        clsPtRW.LoadImgPtSet( strTRPath, imgSetR );

        vecImgPts.push_back( imgSetR);

        ///////////////////////////////////////////////////////////////////

    }

    Pt3d ptLocalRes;
    DOUBLE dAccuracy;
    mlLocalByIntersection( vecGcps, vecImgPts, ptLocalRes, dAccuracy );

    clsPtRW.SaveLocalRes( strResOutPath, ptLocalRes, dAccuracy );

}
bool CmlSiteMapProj::LocalInTwoSite( SINT nStartSiteID, SINT nEndSiteID, const SCHAR* strOutPath )
{
    vector<StereoSet> vecFrontStereoSet;
    if( false == this->GetDealSet( nStartSiteID, -1, -1, vecFrontStereoSet ) )
    {
        return false;
    }
    vector<StereoSet> vecEndStereoSet;
    if( false == this->GetDealSet( nEndSiteID, -1, -1, vecEndStereoSet ) )
    {
        return false;
    }
    Pt3d ptLocalRes;
    DOUBLE dAccuracy = 0;
    vector<ImgPtSet> vecFPts, vecEPts;
    LocalBy2SitesOpts localBy2SOpts;
    if( true == mlLocalIn2Site( vecFrontStereoSet, vecEndStereoSet, vecFPts, vecEPts, localBy2SOpts, ptLocalRes, dAccuracy ) )
    {
        CPtFilesRW clsPtRW;
        clsPtRW.SaveLocalRes( strOutPath, ptLocalRes, dAccuracy );
        return true;
    }
    else
    {
        return false;
    }
}

//bool CmlSiteMapProj:: CreateDisMap( const UINT nGrid, const UINT nRowRadius, const UINT nColRadius, const UINT nTempleSize,  \
//                                    const SINT nSiteID, const SINT nRollID, const SINT nImgID, const bool bIsUsingFeatPt, \
//                                    DOUBLE dCoef)
//{
//
//    vector<StereoSet> vecStereoSet;
//    if ( false == this->GetDealSet( nSiteID, nRollID, nImgID, vecStereoSet ))
//    {
//        return false;
//    }
//
//    CmlSiteMapping clsSiteMapping;
//    vector<string> vecStrMatchFiles;
//    string strFoldDirectory;
//    int nPos = m_strProjPath.rfind("/");
//    strFoldDirectory.assign( m_strProjPath, 0, nPos );
//    strFoldDirectory.append( "/MatchRes/" );
//
//
//    for( int i = 0; i < vecStereoSet.size(); ++i )
//    {
//        StereoSet* pSSet = &vecStereoSet.at(i);
//        int nTempSiteID = pSSet->nSiteID;
//        char cSiteID[20];
//        sprintf( cSiteID, "%d", nTempSiteID );
//        string strTempFDirect = strFoldDirectory;
//        strTempFDirect.append( cSiteID );
//
//        if( access( strTempFDirect.c_str(), 0 ) == -1 )
//        {
//            if( mkdir( strTempFDirect.c_str(), 0777) )
//            {
//                return false;
//            }
//        }
//
//        if( true == bIsUsingFeatPt )
//        {
//            strTempFDirect.append( "/" );
//            string strTLPath = strTempFDirect;
//            string strTRPath = strTempFDirect;
//            strTLPath.append( pSSet->imgLInfo.strName );
//            strTLPath.append( ".fmf" );
//            vecStrMatchFiles.push_back( strTLPath );
//
//            strTRPath.append( pSSet->imgRInfo.strName );
//            strTRPath.append( ".fmf" );
//            vecStrMatchFiles.push_back( strTRPath );
//        }
//        else
//        {
//
//            strTempFDirect.append( "/" );
//            string strTLPath = strTempFDirect;
//            string strTRPath = strTempFDirect;
//            strTLPath.append( pSSet->imgLInfo.strName );
//            strTLPath.append( ".dmf" );
//            vecStrMatchFiles.push_back( strTLPath );
//
//            strTRPath.append( pSSet->imgRInfo.strName );
//            strTRPath.append( ".dmf" );
//            vecStrMatchFiles.push_back( strTRPath );
//        }
//    }
//
//    //clsSiteMapping.DisMapByMultiBlock( vecStereoSet, vecStrMatchFiles, nGrid, nRowRadius, nColRadius, nTempleSize, dCoef, bIsUsingFeatPt );
//
//}

bool CmlSiteMapProj::CreateDmf(vector<StereoSet>vecStereoSet, WideOptions WidePara, vector<ImgPtSet>& vecDPtL, vector<ImgPtSet>& vecDPtR)
{
    vector<string> vecStrMatchFiles;
    string strFoldDirectory;
    int nPos = m_strProjPath.rfind("/");
    strFoldDirectory.assign( m_strProjPath, 0, nPos );
    strFoldDirectory.append( "/MatchRes/" );


    for( int i = 0; i < vecStereoSet.size(); ++i )
    {
        StereoSet* pSSet = &vecStereoSet.at(i);
        int nTempSiteID = pSSet->nSiteID;
        char cSiteID[20];
        sprintf( cSiteID, "%d", nTempSiteID );
        string strTempFDirect = strFoldDirectory;
         strTempFDirect.append( cSiteID );

        if( access( strTempFDirect.c_str(), 0 ) == -1 )
        {
            if( mkdir( strTempFDirect.c_str(), 0777) )
            {
                return false;
            }
        }


        strTempFDirect.append( "/" );
        string strTLPath = strTempFDirect;
        string strTRPath = strTempFDirect;

        SINT nTTPos = pSSet->imgLInfo.strName.rfind("/");
        string strLTempHead;
        strLTempHead.assign(pSSet->imgLInfo.strName, nTTPos+1,pSSet->imgLInfo.strName.length());
        SINT nTTBPos = strLTempHead.rfind(".");
        string strLTempFinal;
        strLTempFinal.assign(strLTempHead, 0, nTTBPos);
        strTLPath.append( strLTempFinal);
        strTLPath.append( ".dmf" );
        vecStrMatchFiles.push_back( strTLPath );

        nTTPos = pSSet->imgRInfo.strName.rfind("/");
        strLTempHead;
        strLTempHead.assign(pSSet->imgRInfo.strName, nTTPos+1,pSSet->imgRInfo.strName.length());
        nTTBPos = strLTempHead.rfind(".");
        strLTempFinal;
        strLTempFinal.assign(strLTempHead, 0, nTTBPos);
        strTRPath.append( strLTempFinal);
        strTRPath.append( ".dmf" );
        vecStrMatchFiles.push_back( strTRPath );


    }

    CPtFilesRW clsPtWrite;

    for( int i = 0; i < vecStereoSet.size(); ++i )
    {
        StereoSet* pStereoSet = &vecStereoSet.at(i);
        string strLCurPath = vecStrMatchFiles[2*i];
        string strRCurPath = vecStrMatchFiles[2*i+1];


        ImgPtSet tempPtL = vecDPtL[i];
        ImgPtSet tempPtR = vecDPtR[i];

        mlDenseMatch(pStereoSet, WidePara, tempPtL, tempPtR);

        //将ImgPtSet数据写入两个dmf文件中
        clsPtWrite.SaveImgPtSet(strLCurPath, tempPtL);
        clsPtWrite.SaveImgPtSet(strRCurPath, tempPtR);


    }

    return true;


}


bool CmlSiteMapProj::LocalBySeqImg( const SCHAR* strSatDom, const SCHAR* strOutPath )
{
    vector<FrameImgInfo> vecFrameInfo;
    for( int i = 0; i < m_vecStereoImgSet.size(); ++i )
    {
        StereoSet* pSet = &m_vecStereoImgSet.at(i);

        vecFrameInfo.push_back( pSet->imgLInfo );
    }
    Pt3d ptLocalRes;
    DOUBLE dAccuracy = 0;
    return 1;//mlLocalBySeqence( vecFrameInfo, strSatDom, ptLocalRes, dAccuracy );

}
bool CmlSiteMapProj::GetSiteCenter(Pt3d& ptCenter)
{
    int nSize ;
    if(!(nSize = m_vecStereoImgSet.size()))
    {
        return false ;
    }
    else
    {
        double dXSum , dYSum ;
        dXSum = dYSum = 0.0 ;
        for(int i = 0 ; i< nSize ; i++)
        {
            StereoSet steTemp = m_vecStereoImgSet[i] ;
            dXSum += steTemp.imgLInfo.exOri.pos.X ;
            dYSum += steTemp.imgLInfo.exOri.pos.Y ;
        }
        ptCenter.X = dXSum/nSize ;
        ptCenter.Y = dYSum/nSize ;
    }
}
bool CmlSiteMapProj::CreateImage( const SCHAR* strPathL, const SCHAR* strPathR, const SCHAR* strOutL, const SCHAR* strOutR )
{
//    CmlFrameImage clsImgL, clsImgR;
//    clsImgL.LoadFile( strPathL );
//    clsImgR.LoadFile( strPathR );
//    vector<double> vXL, vYL, vXR, vYR;
//
//    int nNum = SiftMatch( (char*)clsImgL.m_DataBlock.GetData(), clsImgL.GetWidth(), clsImgL.GetHeight(), 1, (char*)clsImgR.m_DataBlock.GetData(), clsImgR.GetWidth(), clsImgR.GetHeight(), 1, vXL, vYL, vXR, vYR, 400, 0.6 );
//    if( nNum <= 0 )
//    {
//        return false;
//    }
//    else
//    {
//        FILE* pFL = fopen( strOutL, "w" );
//        FILE* pFR = fopen( strOutR, "w" );
//        for( int i = 0; i < vXL.size(); ++i )
//        {
//            fprintf( pFL, "%lf  %lf\n", vXL[i], vYL[i] );
//            fprintf( pFR, "%lf  %lf\n", vXR[i], vYR[i] );
//        }
//        fclose( pFL );
//        fclose( pFR );
//    }
}

