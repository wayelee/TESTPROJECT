#ifndef CMLBA_H
#define CMLBA_H
#include "mlTypes.h"

#define MAX_DENSE_MAT_ELE_NUM 4000


bool getCoefA_B( Pt2d pt, ExOriPara* pCamEx, DOUBLE fx, DOUBLE fy, Pt3d ptXYZ, DOUBLE* dCoefA_B );
bool getCoefA_A( Pt2d pt, ExOriPara* pCamEx, DOUBLE fx, DOUBLE fy, Pt3d ptXYZ, DOUBLE* dCoefA_A );

class CmlBA
{
public:
    CmlBA();
    virtual ~CmlBA();

    bool BA_Common( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts, \
                    vector<ExOriPara> &vecImgResErr, vector<Pt3d> &vec3dPtsResErr, Pt2d &ptTotalImgResErr );

    bool BA_Common( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts );


//    bool BA_Common1( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts );
//
    bool BA_WithEpiImg1N1( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts );

    bool BA_WithEpiImg1N1( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts, \
                                   vector<ExOriPara> &vecImgResErr, vector<Pt3d> &vec3dPtsResErr, Pt2d &ptTotalImgResErr );

    bool BA_WithEpiImg1N1( vector<InOriPara> vecImgInOris, vector< vector<Pt2d> > vecImgPts, vector<bool> vecbImgIsFixed, vector<bool> vecb3dPtIsFixed, vector<ExOriPara> &vecImgExOris, vector< Pt3d > &vec3dPts );

 //   bool BA_With

protected:
private:
};

#endif // CMLBA_H
