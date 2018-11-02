#ifndef CPTFILESRW_H
#define CPTFILESRW_H
#include "../../include/mlTypes.h"
#include <map>

class CPtFilesRW
{
    public:
        CPtFilesRW();
        virtual ~CPtFilesRW();

        bool LoadImgPtSet( string strPath, ImgPtSet &clsImgSet );
        bool SaveImgPtSet( string strPath, ImgPtSet &clsImgSet );

        bool LoadGCPPts( string strPath, vector<Pt3d> &vecImgGCPs );
        bool SaveLocalRes( const SCHAR* strPath, ExOriPara localRes );
        bool SaveLocalRes( const SCHAR* strPath, Pt3d ptlocalRes, DOUBLE dAccuracy );


    protected:
    private:
};

#endif // CPTFILESRW_H
