#ifndef CMLRASTERPROC_H
#define CMLRASTERPROC_H

#include "mlGdalDataset.h"

class CmlRasterProc
{
    public:
        CmlRasterProc();
        CmlRasterProc( CmlGdalDataset *pDataset );
        virtual ~CmlRasterProc();

        virtual bool Resample(SINT nType);

        CmlGdalDataset *m_pDataset;

    protected:
    private:

};

#endif // CMLRASTERPROC_H
