#ifndef CAMCALIBCONVERT_H
#define CAMCALIBCONVERT_H
#include "../../include/mlTypes.h"
class CCamCalibIO
{
public :
    vector<Pt2d> vecLImg2DPts ;
    vector<Pt2d> vecRImg2DPts ;
    vector<Pt3d> vecObj3DPts ;
    vector<Pt3d> vecErrorPts ;
    SINT m_nW ;
    SINT m_nH ;
    InOriPara inLCamPara ;
    InOriPara inRCamPara ;
    ExOriPara exLCamPara ;
    ExOriPara exRCamPara ;
    ExOriPara exStereoPara ;
    int nCamNum ;
private :
    string strTaskName ;
    string strObjName ;
    string strTaskCode ;
    string strObjCode ;
    string strCreator ;
    string strCreateTime ;
    string strEdition ;
    string strRemark ;
    string strCameraMark ;
    string strFrame ;
    SINT nLGcp ;
    SINT nRGcp ;
public :
    CCamCalibIO() ;
    ~CCamCalibIO() ;
    bool readCamSignPts(string m_strReferencePtsFile) ;
    bool writeCamInfoFile(string m_strCamInfoFile) ;
    bool writeAccuracyFile(string m_strAccuracyFile) ;
} ;
#endif
