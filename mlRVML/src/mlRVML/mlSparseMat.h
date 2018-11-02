#ifndef CMLSPARSEMAT_H
#define CMLSPARSEMAT_H
#include "cs.h"
#include "mlTypes.h"

typedef struct stuSparEle
{
    UINT nRow;
    UINT nCol;
    DOUBLE dVal;
}SparEle;

class CmlSparseMat
{
    public:
        CmlSparseMat();
        virtual ~CmlSparseMat();
    public:
        bool Initial( UINT nRow, UINT nCol );
        UINT GetH();
        UINT GetW();
        bool SetAt( UINT nRow, UINT nCol, DOUBLE dVal );
        DOUBLE GetAt( UINT nRow, UINT nCol );
        bool DestoryAll();

        bool QRSolve( vector<DOUBLE> vecBVal, vector<DOUBLE> &vecXVal );
    protected:
    private:

        cs* vecConvToCs( vector<SparEle> vecSpaEle );

    //------------------------------------------------
        UINT m_nWidth;
        UINT m_nHeight;
        bool m_bIsValid;

        vector<SparEle> m_vecSparEle;
};

#endif // CMLSPARSEMAT_H
