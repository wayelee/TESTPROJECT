#ifndef PTSOPERATION_H
#define PTSOPERATION_H

#include "../../include/mlTypes.h"
#include "ImgPtDataSet.h"
#include "PtFilesRW.h"
class CPtsOperation
{
public:
    CPtsOperation();
    ~CPtsOperation();
public:

    CPtFilesRW m_PtFileRW;
    bool LoadLFeatpts( string strPath );
    bool LoadRFeatpts( string strPath );

    bool ClearLFeatpts();
    bool ClearRFeatpts();

    bool AddLPtInDataset( double dx, double dy );
    bool AddRPtInDataset( double dx, double dy );

    bool EditLPtInDataset( ULONG lID, double dx, double dy );
    bool EditRPtInDataset( ULONG lID, double dx, double dy );

    bool DelLPtInDataset( ULONG lID );
    bool DelRPtInDataset( ULONG lID );

    CImgPtDataSet m_ptLFeatDataSet;
    CImgPtDataSet m_ptRFeatDataSet;

    ImgPtSet m_ptLImgPtSet;
    ImgPtSet m_ptRImgPtSet;
};

#endif // PTSOPERATION_H
