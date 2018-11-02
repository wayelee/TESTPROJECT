#include "Ptsoperation.h"

CPtsOperation::CPtsOperation()
{
}
CPtsOperation::~CPtsOperation()
{
}
bool CPtsOperation::LoadLFeatpts( string strPath )
{
    if(false == m_PtFileRW.LoadImgPtSet(strPath, m_ptLImgPtSet))
    {
        return false;
    }
    //if( false == m_ptLFeatDataSet.SetPtData(m_ptLImgPtSet))
    {
     //   return false;
    }
    return true;
}
bool CPtsOperation::ClearLFeatpts()
{
    m_ptLFeatDataSet.ClearData();
    m_ptLImgPtSet.vecAddPts.clear();
    m_ptLImgPtSet.vecPts.clear();
    return true;
}
bool CPtsOperation::EditLPtInDataset(ULONG lID, double dx, double dy)
{

}
