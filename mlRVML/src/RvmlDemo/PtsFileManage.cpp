#include "PtsFileManage.h"

CmlPtsFileManage::CmlPtsFileManage()
{
    //ctor
}

CmlPtsFileManage::~CmlPtsFileManage()
{
    //dtor
}
bool CmlPtsFileManage::LoadIndex( const string strPath )
{
    m_mapIndex.clear();

    SINT nStrPos = strPath.rfind(".");

    string sTempPath;
    sTempPath.assign(strPath, nStrPos, strPath.length() );
    if( 0 != strcmp( sTempPath.c_str(), ".idx" ) )
    {
        return false;
    }
    FILE* pF = NULL;
    pF = fopen( strPath.c_str(), "r" );
    if( pF != NULL )
    {
        char cHead[128];
        fscanf( pF, "%s\n", cHead );
        if( 0 != strcmp( cHead, "ML_IMAGE_TEMP_INDEX_FILE" ) )
        {
            return false;
        }
        double dVersion;
        fscanf( pF, "%lf\n", &dVersion );

        int nImageNum;
        fscanf( pF, "%d\n", &nImageNum );

        for( int i = 0; i < nImageNum; ++i )
        {
            char strID[128];
            UINT nID;
            if( EOF != fscanf( pF, "%s %d\n", strID, &nID ) )
            {
                m_mapIndex.insert( map<string, UINT> :: value_type( strID, nID ) );
            }
            else
            {
                break;
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

//传入相对路径(相对索引文件的子路径) 包含站号及名字的路径，比如：/StereoImg/1/L_1.tif

bool CmlPtsFileManage::CreateIndex( const string strPath, vector<string> vecImgPath )
{
    m_mapIndex.clear();

    SINT nStrPos = strPath.rfind(".");

    string sTempPath;
    sTempPath.assign(strPath, nStrPos, strPath.length() );
    if( 0 != strcmp( sTempPath.c_str(), ".idx" ) )
    {
        return false;
    }
    FILE* pF = NULL;
    pF = fopen( strPath.c_str(), "w" );
    if( pF != NULL )
    {
        fprintf( pF, "%s\n", "ML_IMAGE_TEMP_INDEX_FILE" );

        double dVersion = 1.0;
        fprintf( pF, "%lf\n", dVersion );

        int nImageNum = vecImgPath.size();
        if( nImageNum > 8999 )
        {
            return false;
        }

        fprintf( pF, "%d\n", nImageNum );

        for( int i = 0; i < nImageNum; ++i )
        {
            string strTemp = vecImgPath.at(i);
            UINT nID = ( 1001 + i );
            fprintf( pF, "%s %d\n", strTemp.c_str(), nID );

            m_mapIndex.insert( map<string, UINT> :: value_type( strTemp, nID ) );

        }
        fclose(pF);
        return true;
    }
    else
    {
        return false;
    }
}
UINT CmlPtsFileManage::GetIDByStrPath( string strPath )
{
    map<string, UINT>::iterator it = m_mapIndex.find( strPath );
    if( it != m_mapIndex.end() )
    {
        return it->second;
    }
    else
        return UINT_MAX;
}

