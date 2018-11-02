#include "mlSparseMat.h"

CmlSparseMat::CmlSparseMat()
{
    m_nWidth = 0;
    m_nHeight = 0;
    m_bIsValid = false;;
}

CmlSparseMat::~CmlSparseMat()
{
    m_nHeight = m_nWidth = 0;
}
bool CmlSparseMat::Initial( UINT nRow, UINT nCol )
{
    if( true == m_bIsValid )
    {
        return false;
    }
    m_nHeight = nRow;
    m_nWidth = nCol;

    return true;
}
UINT CmlSparseMat::GetH()
{
    return m_nHeight;
}
UINT CmlSparseMat::GetW()
{
    return m_nWidth;
}
bool CmlSparseMat::SetAt( UINT nRow, UINT nCol, DOUBLE dVal )
{
    SparEle spEl;
    spEl.nRow = nRow;
    spEl.nCol = nCol;
    spEl.dVal = dVal;
    m_vecSparEle.push_back( spEl );
    return true;
}
DOUBLE CmlSparseMat::GetAt( UINT nRow, UINT nCol )
{
    return true;
}
bool CmlSparseMat::DestoryAll()
{
    m_nWidth = m_nHeight = 0;
    m_vecSparEle.clear();
    m_bIsValid = false;
    return true;
}
bool CmlSparseMat::QRSolve( vector<DOUBLE> vecBVal, vector<DOUBLE> &vecXVal )
{
    if( vecBVal.size() != m_nHeight )
    {
        return false;
    }
    vecXVal.clear();
    //-----------------------

    //-----------------------
    cs* pCSMat = vecConvToCs( m_vecSparEle );
    DOUBLE *pB = new DOUBLE[max(m_nWidth, m_nHeight)];
    for( UINT i = 0; i < vecBVal.size(); ++i )
    {
        pB[i] = vecBVal[i];
    }

    cs* pA = cs_compress (pCSMat);
    cs_spfree (pCSMat) ;

    csi nOrder = 3;
    if( 1 == cs_qrsol( nOrder, pA, pB ) )
    {
        cs_spfree (pA);
        for( UINT i = 0; i < m_nWidth; ++i )
        {
            vecXVal.push_back( pB[i] );
        }
        delete[] pB;
        return true;
    }
    else
    {
        cs_spfree (pA);
        delete[] pB;
        return false;
    }

}

cs* CmlSparseMat::vecConvToCs( vector<SparEle> vecSpaEle )
{
    cs *T ;
    T = cs_spalloc (0, 0, 1, 1, 1) ;                    /* allocate result */
    DOUBLE i, j;
    DOUBLE x;
    for( UINT nIdx = 0; nIdx < vecSpaEle.size(); ++nIdx )
    {
        SparEle spCur = vecSpaEle[nIdx];
        i = spCur.nRow;
        j = spCur.nCol;
        x = spCur.dVal;
        if (!cs_entry (T, (csi) i, (csi) j, x))
        {
            return (cs_spfree (T)) ;
        }
    }
    return (T) ;
}
