#include "camCalibIO.h"

CCamCalibIO::CCamCalibIO()
{
    nCamNum = 0 ;
}

CCamCalibIO::~CCamCalibIO()
{

}

bool CCamCalibIO::readCamSignPts(string m_strReferencePtsFile)
{
//    const SCHAR* path = m_strReferencePtsFile.c_str();
    std::ifstream stmOpen(m_strReferencePtsFile.c_str());
    if(stmOpen.is_open())
    {
        string str1 ;
        getline(stmOpen , str1) ;
        if(str1[0] == '*')
        {
            string str2 , str3 , str4 ,str5 ,str6 , str7 , str8 , str9 ;
            // 读取文件头
            stmOpen >> str2 ;
            if(str2 == "szTaskName")
            {
                stmOpen >> strTaskName >> str3 ;
            }
            else
            {
                return false ;
            }
            if(str3 == "szObjectName")
            {
                stmOpen >> strObjName >> str4 ;
            }
            else
            {
                return false ;
            }
            if(str4 == "szTaskCode")
            {
                stmOpen >> strTaskCode >> str5 ;
            }
            else
            {
                return false ;
            }
            if(str5 == "szObjectCode")
            {
                stmOpen >> strObjCode >> str6 ;
            }
            else
            {
                return false ;
            }
            if(str6 == "szCreator")
            {
                stmOpen >> strCreator >> str7 ;
            }
            else
            {
                return false ;
            }
            if(str7 == "dtCreateTime")
            {
                stmOpen >> strCreateTime >> str8 ;
            }
            else
            {
                return false ;
            }
            if(str8 == "szEdition")
            {
                stmOpen >> strEdition >> str9 ;
            }
            else
            {
                return false ;
            }
            if(str9 == "szRemark")
            {
                stmOpen >> strRemark ;
            }
            else
            {
                return false ;
            }
//            stmOpen >> str2 >> strTaskCode ;
//            stmOpen >> str2 >> strObjCode ;
//            stmOpen >> str2 >> strCreator ;
//            stmOpen >> str2 >> strCreateTime ;
//            stmOpen >> str2 >> strEdition ;
//            stmOpen >> str2 >> strRemark ;
            getline(stmOpen , str1) ;
            getline(stmOpen , str1) ;
        }
        else
        {
            return false ;
        }
        if(str1[0] == '*')
        {
            // 读取是单相机还是立体相机
            stmOpen >> nCamNum >> m_nW >> m_nH ;
            if(nCamNum == 1)
            {
                double dy ;
                stmOpen >> inLCamPara.f >> inLCamPara.x >> dy >> inLCamPara.k1 >> inLCamPara.k2 >> inLCamPara.k3
                >> inLCamPara.p1 >> inLCamPara.p2 >> inLCamPara.alpha >> inLCamPara.beta ;
                inLCamPara.y = m_nH -1 - dy ;
            }
            else if(nCamNum == 2)
            {
                double dLefty , dRighty ;
                stmOpen >> inLCamPara.f >> inLCamPara.x >> dLefty >> inLCamPara.k1 >> inLCamPara.k2 >> inLCamPara.k3
                >> inLCamPara.p1 >> inLCamPara.p2 >> inLCamPara.alpha >> inLCamPara.beta
                >> inRCamPara.f >> inRCamPara.x >> dRighty >> inRCamPara.k1 >> inRCamPara.k2 >> inRCamPara.k3
                >> inRCamPara.p1 >> inRCamPara.p2 >> inRCamPara.alpha >> inRCamPara.beta ;
                inLCamPara.y = m_nH -1 - dLefty ;
                inRCamPara.y = m_nH -1 - dRighty ;
            }
            getline(stmOpen , str1) ;
            getline(stmOpen , str1) ;
        }
        else
        {
            return false ;
        }
        if(str1[0] == '*')
        {
            // 读取相机标识
            stmOpen >> strCameraMark ;
            getline(stmOpen , str1) ;
            getline(stmOpen , str1) ;
        }
        else
        {
            return false ;
        }
        if(str1[0] == '*')
        {
            //读取坐标系标识
            stmOpen >> strFrame ;
            getline(stmOpen ,str1) ;
            getline(stmOpen , str1) ;
        }
        else
        {
            return false ;
        }
        if(str1[0] == '*')
        {
            if(nCamNum == 1)
            {
                stmOpen >>  nLGcp ;
            }
            else if(nCamNum == 2)
            {
                stmOpen >> nLGcp >> nRGcp ;
            }
            getline(stmOpen ,str1) ;
            getline(stmOpen , str1) ;
        }
        else
        {
            return false ;
        }
        if(str1[0] == '*')
        {
            Pt2d ptImg ;
            Pt3d ptObj ;
            for(SINT i = 0 ; i < nLGcp ; i++)
            {
                double dy ;
                stmOpen >> ptImg.lID >> ptObj.X >> ptObj.Y >> ptObj.Z >> ptImg.X >> dy ;
                ptObj.lID = ptImg.lID ;
                ptImg.Y = m_nH - 1 - dy ;
                vecLImg2DPts.push_back(ptImg) ;
                vecObj3DPts.push_back(ptObj) ;
            }
            if(nCamNum == 2)
            {
                for(SINT i = 0 ; i < nRGcp ; i++)
                {
                    double dy ;
                    stmOpen >> ptImg.lID >> ptObj.X >> ptObj.Y >> ptObj.Z >> ptImg.X >> dy ;
                    ptImg.Y = m_nH - 1 - dy ;
                    vecRImg2DPts.push_back(ptImg) ;
                }
            }
        }
        else
        {
            return false ;
        }
    }
    else
    {
        return false ;
    }
    stmOpen.close() ;
    return true ;
}

bool CCamCalibIO::writeCamInfoFile(string m_strCamInfoFile)
{
    std::ofstream stmSave(m_strCamInfoFile.c_str()) ;
    if(stmSave.is_open())
    {
        string str1 , str2 ;
        str1 = "********************FileHead********************\n" ;
        stmSave << str1 ;
        stmSave << "TaskName        " << strTaskName << "\n" ;
        stmSave << "ObjectName        " << strObjName << "\n" ;
        stmSave << "TaskCode        " << strTaskCode << "\n" ;
        stmSave << "ObjectCode        " << strObjCode << "\n" ;
        stmSave << "Creator        " << strCreator << "\n" ;
        stmSave << "CreateTime        " << strCreateTime << "\n" ;
        stmSave << "Edition        " << strEdition << "\n" ;
        stmSave << "Remark        " << strRemark << "\n" ;
        str2 = "********************FileHead End********************\n" ;
        stmSave << str2 ;
        stmSave << "CameraMark         " << strCameraMark << "\n" ;
        stmSave << "SingleDoubleID        " << nCamNum << "\n" ;
        stmSave << "ImageWidth        " << m_nW << "\n" ;
        stmSave << "ImageHeight        " << m_nH << "\n" ;
        stmSave << inLCamPara.f << "        " << inLCamPara.x << "        " << (m_nH -1 - inLCamPara.y) << "\n"
        << inLCamPara.k1 << "        " << inLCamPara.k2 << "        " << inLCamPara.k3 << "        " << inLCamPara.p1
        << "        " << inLCamPara.p2 << "        " << inLCamPara.alpha << "        " << inLCamPara.beta << "\n"
        << "        " << exLCamPara.pos.X << "        " << exLCamPara.pos.Y << "        " << exLCamPara.pos.Z
        << "        " << Rad2Deg(exLCamPara.ori.omg) << "        " << Rad2Deg(exLCamPara.ori.phi)<< "        "
        << Rad2Deg(exLCamPara.ori.kap) << "\n" ;
        if(nCamNum == 2)
        {
            stmSave << inRCamPara.f << "        " << inRCamPara.x << "        " << (m_nH -1 - inRCamPara.y) << "\n"
            << inRCamPara.k1 << "        " << inRCamPara.k2 << "        " << inRCamPara.k3 << "        " << inRCamPara.p1
            << "        " << inRCamPara.p2 << "        " << inRCamPara.alpha << "        " << inRCamPara.beta << "\n"
            << "        " << exRCamPara.pos.X << "        " << exRCamPara.pos.Y << "        " << exRCamPara.pos.Z
            << "        " << Rad2Deg(exRCamPara.ori.omg) << "        " << Rad2Deg(exRCamPara.ori.phi)<< "        "
            << Rad2Deg(exRCamPara.ori.kap) << "\n" << "        " << exStereoPara.pos.X<< "        " << exStereoPara.pos.Y
            << "        " << exStereoPara.pos.Z << "        " << Rad2Deg(exStereoPara.ori.omg) << "        "
            << Rad2Deg(exStereoPara.ori.phi) << "        " << Rad2Deg(exStereoPara.ori.kap) << "\n" ;
        }
    }
    else
    {
        return false ;
    }
    stmSave.close() ;
    return true ;
}

bool CCamCalibIO::writeAccuracyFile(string m_strAccuracyFile)
{
    std::ofstream stmSave(m_strAccuracyFile.c_str()) ;
    if(stmSave.is_open())
    {
        string str1 , str2 ;
        str1 = "********************FileHead********************\n" ;
        stmSave << str1 ;
        stmSave << "TaskName        " << strTaskName << "\n" ;
        stmSave << "ObjectName        " << strObjName << "\n" ;
        stmSave << "TaskCode        " << strTaskCode << "\n" ;
        stmSave << "ObjectCode        " << strObjCode << "\n" ;
        stmSave << "Creator        " << strCreator << "\n" ;
        stmSave << "CreateTime        " << strCreateTime << "\n" ;
        stmSave << "Edition        " << strEdition << "\n" ;
        stmSave << "Remark        " << strRemark << "\n" ;
        str2 = "********************FileHead End********************\n" ;
        stmSave << str2 ;
        stmSave << "CameraMark         " << strCameraMark << "\n" ;
        stmSave << "SingleDoubleID        " << nCamNum << "\n" ;
        if(nCamNum == 1)
        {
            nLGcp = vecLImg2DPts.size() ;
            stmSave << nLGcp << "\n" ;
            stmSave << "nID\t\t" << "X\t\t" << "Y\t\t"<< "Z\t\t"
            << "dx\t\t" << "dy\t\t" << "dz\n" ;
            DOUBLE dxSum ,dySum ;
            dxSum = dySum = 0.0 ;
            for(SINT i = 0 ; i < nLGcp ; i++)
            {
                Pt2d ptImg = vecLImg2DPts[i] ;
                Pt3d ptError = vecErrorPts[i] ;
                stmSave << ptImg.lID<< "\t\t" <<  ptImg.X<< "\t\t" << (m_nH -1 -ptImg.Y) << "\t\t"
                << 0 << "\t\t" << ptError.X << "\t\t" << ptError.Y <<
                "\t\t" << 0 << "\n" ;
                dxSum += ptError.X * ptError.X ;
                dySum += ptError.Y * ptError.Y ;
            }
            dxSum = sqrt(dxSum/nLGcp) ;
            dySum = sqrt(dySum/nLGcp) ;
            stmSave << "image mean square error :    ddx = " << dxSum << " ddy =  " << dySum << "\n";
        }
        else if(nCamNum == 2)
        {
            nLGcp = vecObj3DPts.size() ;
            stmSave << nLGcp << "\n" ;
            stmSave << "nID                 " << "X                    " << "Y                 "<< "Z                  "
            << "dx                  " << "dy                    " << "dz                    " ;
            DOUBLE dxSum,dySum,dzSum;
            dxSum = dySum = dzSum =0;
            for(SINT i = 0 ; i < nLGcp ; i++)
            {
                Pt3d ptObj = vecObj3DPts[i] ;
                Pt3d ptError = vecErrorPts[i] ;
                stmSave << ptObj.lID<< "                    " <<  ptObj.X<< "                    " << ptObj.Y << "                    "
                << ptObj.Z << "                    " << ptError.X << "                    " << ptError.Y
                << "                    " << ptError.Z << "\n" ;
                dxSum += ptError.X*ptError.X;
                dySum += ptError.Y*ptError.Y;
                dzSum += ptError.Z*ptError.Z;
            }
            dxSum = sqrt(dxSum/ nLGcp) ;
            dySum = sqrt(dySum/ nLGcp) ;
            dzSum = sqrt(dzSum/ nLGcp) ;
            stmSave << "object mean square error :    ddx = " << dxSum << " ddy =  " << dySum << "ddz =  " << dzSum << "\n";
        }
        stmSave.close() ;
    }
    return true ;
}
