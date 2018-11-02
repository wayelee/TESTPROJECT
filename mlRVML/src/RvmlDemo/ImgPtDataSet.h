#ifndef CIMGPTDATASET_H
#define CIMGPTDATASET_H

#include "../../include/mlTypes.h"
#include <map>

class CImgPtDataSet
{
    public:
        CImgPtDataSet();
        virtual ~CImgPtDataSet();

        bool SetPtData( string strPath );
        bool SavePtData(string strPath);

        bool FindPtByID( ULONG lID, Pt2d &ptRes );
        bool FindAutoPtByIndex( UINT nIndex, Pt2d &ptRes );
        bool FindManualPtByIndex( UINT nIndex, Pt2d &ptRes );

        bool FindAutoPtIndexByID( ULONG lID, UINT &nIndex );
        bool FindManualPtIndexByID( ULONG lID, UINT &nIndex );


        bool AddPt( ULONG lID, double dx, double dy );

        bool DelPtByID( ULONG lID );
        bool DelAutoPtByIndex( UINT nIndex );
        bool DelManualPtByIndex( UINT nIndex );

//        bool EditPtByID( ULONG lID, double dx, double dy );
        bool EditAutoPtByIndxe( UINT nIndex, double dx, double dy , ULONG lID);
        bool EditManualPtByIndxe( UINT nIndex, double dx, double dy );

        bool ClearData();
        ImgPtSet m_ImgPtSet;
    protected:
    private:
        bool buildMap();


        map<ULONG, UINT > m_mapAutoPtData;
        map<ULONG, UINT > m_mapManualPtData;
};

#endif // CIMGPTDATASET_H
