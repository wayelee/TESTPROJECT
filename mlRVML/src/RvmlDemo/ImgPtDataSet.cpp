#include "ImgPtDataSet.h"
#include "PtFilesRW.h"
CImgPtDataSet::CImgPtDataSet()
{
    //ctor
}

CImgPtDataSet::~CImgPtDataSet()
{
    //dtor
}
bool CImgPtDataSet::SetPtData( string strPath )
{
    CPtFilesRW ptRW;
    if( false == ptRW.LoadImgPtSet( strPath, m_ImgPtSet ))
    {
        return false;
    }
    buildMap();
    return true;
}
bool CImgPtDataSet::SavePtData(string strPath)
{
    CPtFilesRW ptRW;
    if( false == ptRW.SaveImgPtSet( strPath, m_ImgPtSet ))
    {
        return false;
    }
    return true;
}

bool CImgPtDataSet::FindPtByID(  ULONG lID, Pt2d &ptRes)
{
    map<ULONG, UINT>::iterator itAuto = m_mapAutoPtData.find( lID );
    if( itAuto != m_mapAutoPtData.end() )
    {
        UINT nIndex = itAuto->second;
        return ( FindAutoPtByIndex( nIndex, ptRes ) );
    }
    else
    {
        map<ULONG, UINT>::iterator itManual = m_mapManualPtData.find( lID );
        if( itManual != m_mapManualPtData.end() )
        {
            UINT nIndex = itManual->second;
            return ( FindManualPtByIndex( nIndex, ptRes) );
        }
        else
        {
            return false;
        }
    }
}
bool CImgPtDataSet::FindAutoPtIndexByID( ULONG lID, UINT &nIndex )
{
    map<ULONG, UINT>::iterator itAuto = m_mapAutoPtData.find( lID );
    if( itAuto != m_mapAutoPtData.end() )
    {
        nIndex = itAuto->second;
        return true;
    }
    else
    {
        return false;
    }
}
bool CImgPtDataSet::FindManualPtIndexByID( ULONG lID, UINT &nIndex )
{
    map<ULONG, UINT>::iterator itManual = m_mapManualPtData.find( lID );
    if( itManual != m_mapManualPtData.end() )
    {
        nIndex = itManual->second;
        return ( true );
    }
    else
    {
        return false;
    }
}
bool CImgPtDataSet::FindAutoPtByIndex( UINT nIndex, Pt2d &ptRes )
{
    if( nIndex >= m_ImgPtSet.vecPts.size() )
    {
        return false;
    }
    ptRes = (m_ImgPtSet.vecPts.at(nIndex));
    return true;
}
bool CImgPtDataSet::FindManualPtByIndex( UINT nIndex, Pt2d &ptRes )
{
    if( nIndex >= m_ImgPtSet.vecAddPts.size())
    {
        return false;
    }
    ptRes = (m_ImgPtSet.vecAddPts.at(nIndex));
    return true;
}
bool CImgPtDataSet::ClearData()
{
    m_mapAutoPtData.clear();
    m_mapManualPtData.clear();

    m_ImgPtSet.vecAddPts.clear();
    m_ImgPtSet.vecPts.clear();
    return true;
}

bool CImgPtDataSet::AddPt( ULONG lID, double dx, double dy )
{
    Pt2d ptCur;
    ptCur.X = dx;
    ptCur.Y = dy;
    ptCur.lID = lID;
    ptCur.byType = 2;
    pair<map<ULONG,UINT>::iterator,bool> res = m_mapManualPtData.insert( map<ULONG, UINT>::value_type( lID, m_ImgPtSet.vecAddPts.size()));
    if( res.second == true )
    {
        m_ImgPtSet.vecAddPts.push_back(ptCur);
    }
    else
    {
        return false;
    }
}

bool CImgPtDataSet::DelPtByID( ULONG lID )
{
    map<ULONG, UINT>::iterator it = m_mapAutoPtData.find( lID );
    if( it != m_mapAutoPtData.end() )
    {
        return  DelAutoPtByIndex( it->second );
    }
    it = m_mapManualPtData.find(lID );
    if( it != m_mapManualPtData.end() )
    {
        return DelManualPtByIndex( it->second );
    }
    return false;
}
bool CImgPtDataSet::DelAutoPtByIndex( UINT nIndex )
{
    if(( nIndex < 0  )||( nIndex >= m_ImgPtSet.vecPts.size()) )
    {
        return false;
    }

    vector<Pt2d>::iterator it = m_ImgPtSet.vecPts.begin();
    it += nIndex;
    m_ImgPtSet.vecPts.erase( it );
    m_mapAutoPtData.clear();

    for( UINT i = 0; i < m_ImgPtSet.vecPts.size(); ++i )
    {
        Pt2d ptCur = m_ImgPtSet.vecPts[i];
        m_mapAutoPtData.insert( map<ULONG, UINT> ::value_type( ptCur.lID, i ) );
    }
    return true;
}
bool CImgPtDataSet::DelManualPtByIndex( UINT nIndex )
{
    if(( nIndex < 0  )||( nIndex >= m_ImgPtSet.vecAddPts.size()) )
    {
        return false;
    }
    vector<Pt2d>::iterator it = m_ImgPtSet.vecAddPts.begin();
    it += nIndex;
    m_ImgPtSet.vecAddPts.erase( it );
    m_mapManualPtData.clear();

    for( UINT i = 0; i < m_ImgPtSet.vecAddPts.size(); ++i )
    {
        Pt2d ptCur = m_ImgPtSet.vecAddPts[i];
        m_mapManualPtData.insert( map<ULONG, UINT> ::value_type( ptCur.lID, i ) );
    }
    return true;
}

//bool CImgPtDataSet::EditPtByID( ULONG lID, double dx, double dy )
//{



//}
bool CImgPtDataSet::EditAutoPtByIndxe( UINT nIndex, double dx, double dy , ULONG lID)
{
    if(nIndex >= m_ImgPtSet.vecPts.size())
    {
        return false;
    }
    if(false == DelAutoPtByIndex(nIndex ))
    {
        return false;
    }
    return AddPt(lID, dx,dy );
}

bool CImgPtDataSet::EditManualPtByIndxe( UINT nIndex, double dx, double dy )
{
    if(nIndex >= m_ImgPtSet.vecAddPts.size())
    {
        return false;
    }

    m_ImgPtSet.vecAddPts.at(nIndex).X = dx;
    m_ImgPtSet.vecAddPts.at(nIndex).Y = dy;
    return true;
}

bool CImgPtDataSet::buildMap()
{
    m_mapAutoPtData.clear();
    m_mapManualPtData.clear();

    for( UINT i = 0; i < m_ImgPtSet.vecPts.size(); ++i )
    {
        Pt2d ptCur = m_ImgPtSet.vecPts[i];
        m_mapAutoPtData.insert( map<ULONG, UINT> ::value_type( ptCur.lID, i ) );
    }
    for( UINT i = 0; i < m_ImgPtSet.vecAddPts.size(); ++i )
    {
        Pt2d ptCur = m_ImgPtSet.vecAddPts[i];
        m_mapManualPtData.insert( map<ULONG, UINT> ::value_type( ptCur.lID, i ) );
    }
    return true;
}
