#ifndef CMLPTSFILEMANAGE_H
#define CMLPTSFILEMANAGE_H

#include "../../include/mlTypes.h"
#include <map>

class CmlPtsFileManage
{
    public:
        CmlPtsFileManage();
        virtual ~CmlPtsFileManage();

        bool LoadIndex( const string strPath );
        bool CreateIndex( const string strPath, vector<string> vecImgPath );

        UINT GetIDByStrPath( string strPath );

////////////////////////////////////////////////////




        string m_strIndexPath;

    protected:
    private:
        map<string, UINT> m_mapIndex;


};

#endif // CMLPTSFILEMANAGE_H
