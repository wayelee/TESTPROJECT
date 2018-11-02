#include "PtFilesRW.h"

CPtFilesRW::CPtFilesRW()
{
    //ctor
}

CPtFilesRW::~CPtFilesRW()
{
    //dtor
}
bool CPtFilesRW::LoadGCPPts( string strPath, vector<Pt3d> &vecImgGCPs )
{
    string strInPath( strPath );
    SINT nStrPos = strInPath.rfind(".");

    string sTempPath;
    sTempPath.assign(strInPath, nStrPos, strInPath.length() );

    if ( 0 != strcmp( sTempPath.c_str(), ".gcp" ))
    {
        return false;
    }

    FILE* pF = NULL;
    pF = fopen( strPath.c_str(), "r" );
    if( pF != NULL )
    {
        char cHead[128];
        fscanf( pF, "%s\n", cHead );
        if( 0 != strcmp( cHead, "ML_GCP_POINT_FILE" ) )
        {
            return false;
        }
        double dVersion = 1.0;
        fscanf( pF, "%lf\n", &dVersion );

        SINT nNum;
        fscanf( pF, "%d\n", &nNum );

        for( int i = 0; i < nNum; ++i )
        {
            Pt3d pt;
            fscanf( pF, "%llu %lf %lf %lf\n", &pt.lID, &pt.X, &pt.Y, &pt.Z );
            vecImgGCPs.push_back( pt );
        }

        fclose( pF );
        return true;
    }
    else
    {
        return false;
    }
}
//bool CPtFilesRW::WriteLocalRes( const char* strPath, ExOriPara localRes )
//{
//
//}
bool CPtFilesRW::SaveLocalRes( const char* strPath, Pt3d ptlocalRes, DOUBLE dAccuracy )
{

    string strInPath( strPath );
    SINT nStrPos = strInPath.rfind(".");

    string sTempPath;
    if( nStrPos < 0 )
    {
        return false;
    }
    sTempPath.assign(strInPath, nStrPos, strInPath.length() );

    if ( 0 != strcmp( sTempPath.c_str(), ".lrf" ))
    {
        return false;
    }

    FILE* pF = NULL;
    pF = fopen( strPath, "w" );
    if( pF != NULL )
    {
        fprintf( pF, "%s\n", "ML_LOCALIZATION_RESULT_FILE" );

        double dVersion = 1.0;
        fprintf( pF, "%lf\n", dVersion );

        fprintf( pF, "%lf %lf %lf %lf\n", ptlocalRes.X, ptlocalRes.Y, ptlocalRes.Z, dAccuracy );
        fclose( pF );
        return true;
    }
    else
    {
        return false;
    }
}
bool CPtFilesRW::LoadImgPtSet( string strPath, ImgPtSet &clsImgSet )
{
    SINT nStrPos = strPath.rfind(".");

    string sTempPath;
    sTempPath.assign(strPath, nStrPos, strPath.length() );
    if( ( 0 != strcmp( sTempPath.c_str(), ".fmf" ))&&( 0 != strcmp( sTempPath.c_str(), ".dmf" ) )&&( 0 != strcmp( sTempPath.c_str(), ".tpf" ) ) )
    {
        return false;
    }
    FILE* pF = NULL;
    pF = fopen( strPath.c_str(), "r" );
    if( pF != NULL )
    {
        char cHead[128];
        fscanf( pF, "%s\n", cHead );
        if( ( 0 != strcmp( cHead, "ML_FEATURE_MATCHING_POINT_FILE" ) )&&
            ( 0 != strcmp( cHead, "ML_DENSE_MATCHING_POINT_FILE" ) )&&
            ( 0 != strcmp( cHead, "ML_TIE_POINT_FILE" ) ) )
        {
            return false;
        }
        DOUBLE dVersion;
        fscanf( pF, "%lf\n", &dVersion );

        ExOriPara &tmpExOri = clsImgSet.imgInfo.exOri;
        fscanf( pF, "%lf %lf %lf %lf %lf %lf\n", &tmpExOri.pos.X, &tmpExOri.pos.Y, &tmpExOri.pos.Z, &tmpExOri.ori.omg, &tmpExOri.ori.phi, &tmpExOri.ori.kap );
        tmpExOri.ori.omg = Deg2Rad( tmpExOri.ori.omg );
        tmpExOri.ori.phi = Deg2Rad( tmpExOri.ori.phi );
        tmpExOri.ori.kap = Deg2Rad( tmpExOri.ori.kap );

        fscanf( pF, "%d %d\n", &clsImgSet.imgInfo.nW, &clsImgSet.imgInfo.nH );

        InOriPara &tmpInOri = clsImgSet.imgInfo.inOri;

        fscanf( pF, "%lf %lf %lf\n", &tmpInOri.f, &tmpInOri.x, &tmpInOri.y );
        fscanf( pF, "%lf %lf %lf\n", &tmpInOri.k1, &tmpInOri.k2, &tmpInOri.k3 );
        fscanf( pF, "%lf %lf\n", &tmpInOri.p1, &tmpInOri.p2 );
        fscanf( pF, "%lf %lf\n", &tmpInOri.alpha, &tmpInOri.beta );

        fscanf( pF, "%d\n", &clsImgSet.imgInfo.nImgIndex );

        SINT nPtNum = 0;
        fscanf( pF, "%d\n", &nPtNum );

        for( int i = 0; i < nPtNum; ++i )
        {
            Pt2d ptCur;
            fscanf( pF, "%llu %lf %lf %d\n", &ptCur.lID, &ptCur.X, &ptCur.Y, &ptCur.byType );
            if( ptCur.byType == 1 )
            {
                clsImgSet.vecPts.push_back( ptCur );
            }
            if( ptCur.byType == 2 )
            {
                clsImgSet.vecAddPts.push_back( ptCur );
            }

        }

        fclose(pF);
        return true;
    }
    else
    {
        return false;
    }
}

bool CPtFilesRW::SaveImgPtSet( string strPath, ImgPtSet &clsImgSet )
{
    SINT nStrPos = strPath.rfind(".");

    string sTempPath;

    sTempPath.assign(strPath, nStrPos, strPath.length() );

    if( ( 0 != strcmp( sTempPath.c_str(), ".fmf" ))&&( 0 != strcmp( sTempPath.c_str(), ".dmf" ) )&&( 0 != strcmp( sTempPath.c_str(), ".tpf" ) ) )
    {
        return false;
    }
    FILE* pF = NULL;

    pF = fopen( strPath.c_str(), "w" );

    if( pF != NULL )
    {
        if( 0 == strcmp( sTempPath.c_str(), ".fmf" ) )
        {
            fprintf( pF, "%s\n", "ML_FEATURE_MATCHING_POINT_FILE" );
        }
        if( 0 == strcmp( sTempPath.c_str(), ".dmf" ) )
        {
            fprintf( pF, "%s\n", "ML_DENSE_MATCHING_POINT_FILE" );
        }
        if( 0 == strcmp( sTempPath.c_str(), ".tpf" ) )
        {
            fprintf( pF, "%s\n", "ML_TIE_POINT_FILE" );
        }

        DOUBLE dVersion = 1.0;
        fprintf( pF, "%lf\n", dVersion );

        ExOriPara tmpExOri = clsImgSet.imgInfo.exOri;
        fprintf( pF, "%lf %lf %lf %lf %lf %lf\n", tmpExOri.pos.X, tmpExOri.pos.Y, tmpExOri.pos.Z, Rad2Deg( tmpExOri.ori.omg ), Rad2Deg( tmpExOri.ori.phi ), Rad2Deg( tmpExOri.ori.kap ) );

        fprintf( pF, "%d %d\n", clsImgSet.imgInfo.nW, clsImgSet.imgInfo.nH );

        InOriPara tmpInOri = clsImgSet.imgInfo.inOri;

        fprintf( pF, "%lf %lf %lf\n", tmpInOri.f, tmpInOri.x, tmpInOri.y );
        fprintf( pF, "%lf %lf %lf\n", tmpInOri.k1, tmpInOri.k2, tmpInOri.k3 );
        fprintf( pF, "%lf %lf\n", tmpInOri.p1, tmpInOri.p2 );
        fprintf( pF, "%lf %lf\n", tmpInOri.alpha, tmpInOri.beta );

        fprintf( pF, "%d\n", clsImgSet.imgInfo.nImgIndex );

        SINT nPtNum = clsImgSet.vecPts.size() + clsImgSet.vecAddPts.size();
        fprintf( pF, "%d\n", nPtNum );

        for( int i = 0; i < clsImgSet.vecPts.size(); ++i )
        {
            Pt2d ptCur = clsImgSet.vecPts[i];
            fprintf( pF, "%llu %lf %lf %d\n", ptCur.lID, ptCur.X, ptCur.Y, ptCur.byType );
        }
        for( int i = 0; i < clsImgSet.vecAddPts.size(); ++i )
        {
            Pt2d ptCur = clsImgSet.vecAddPts[i];
            fprintf( pF, "%llu %lf %lf %d\n", ptCur.lID, ptCur.X, ptCur.Y, ptCur.byType );
        }

        fclose(pF);
        return true;
    }
    else
    {
        return false;
    }
}

bool CPtFilesRW::SaveRegionPt(string strPath, vector<Pt3d> &vecObjPts, vector<DOUBLE> &vecCorr)
{
    SINT nPtNum = vecObjPts.size();
    SINT nPtNums = vecCorr.size();
    if((nPtNum ==0) || (nPtNums==0) || (nPtNum!=nPtNums))
    {
        return false;
    }
    SINT nStrPos = strPath.rfind(".");

    string sTempPath;

    sTempPath.assign(strPath, nStrPos, strPath.length() );

    if( 0 != strcmp( sTempPath.c_str(), ".mrf" ))
    {
        return false;
    }
    FILE* pF = NULL;

    pF = fopen( strPath.c_str(), "w" );

    if( pF != NULL )
    {

        if( 0 == strcmp( sTempPath.c_str(), ".mrf" ) )
        {
            fprintf( pF, "%s\n", "ML_MATCH_CORRELATION_FILE" );
        }

        DOUBLE dVersion = 1.0;
        fprintf( pF, "%lf\n", dVersion );

        fprintf( pF, "%d\n", nPtNum );

        for( int i = 0; i < nPtNum; ++i )
        {
            Pt3d ptCur = vecObjPts[i];
            DOUBLE dCorr = vecCorr[i];
            fprintf( pF, "%lf %lf %lf %lf\n", ptCur.X, ptCur.Y, ptCur.Z, dCorr);
        }

        fclose(pF);
        return true;
    }
    else
    {
        return false;
    }
}
