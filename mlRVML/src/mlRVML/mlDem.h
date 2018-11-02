#ifndef MLDEM_H
#define MLDEM_H

#include "mlGdalDataset.h"


class CmlDem : public CmlGdalDataset
{
    public:
        CmlDem();
        virtual ~CmlDem();
        double m_dResolution ;
        Pt2d m_PtOrigin ;
    protected:
    private:
};

#endif // MLDEM_H
