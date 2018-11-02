/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlWBaseProc.cpp
* @date 2011.11.18
* @author 彭嫚 pengman@irsa.ac.cn
* @brief 长基线制图类源文件
* @version 1.1
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
* 彭嫚  2011.11.29  1.0  转化为c++框架
*/
#include "mlWBaseProc.h"
#include "mlFrameImage.h"
#include "mlStereoProc.h"
#include "mlSiteMapping.h"
#include "mlTypes.h"
#include "SiftMatch.h"
#include "mlCamCalib.h"
using namespace cv;
using namespace std;

/**
* @fn CmlWBaseProc()
* @date 2011.12.1
* @author 彭嫚
* @brief 空参构造函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CmlWBaseProc::CmlWBaseProc()
{
    //ctor
}

/**
* @fn ~CmlWBaseProc()
* @date 2011.12.1
* @author 彭嫚
* @brief 析构函数
* @version 1.1
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
CmlWBaseProc::~CmlWBaseProc()
{
    //dtor
}
/** @brief WideBaseMappSUINT
  *
  * @todo: document this function
  */

//bool CmlWBaseProc::WideBaseMapping(char *nFileNameIn,char *FileNameOut)
//{
//
//    return true;
//}
/**
* @fn WideBaseAnalysis
* @date 2011.12.1
* @author 彭嫚
* @brief 长基线制图最优基线分析
* @param NavCamPara,导航相机参数
* @param PanCamPara,全景相机参数
* @param AnaPara,全景相机参数
* @param OptiBase,计算的最优基线
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
bool CmlWBaseProc::WideBaseAnalysis(InOriPara NavCamPara, InOriPara PanCamPara, BaseOptions AnaPara,double &OptiBase)
{

    //根据输入文件导入计算参数，计算最优基线
    DOUBLE *dBestBase;
//    DOUBLE dOptiBase;

    //判断是否输入相机参数
    if((NavCamPara.f == 0)|| (NavCamPara.x == 0) ||(NavCamPara.y == 0))
    {
        SCHAR strErr[] = "Parameters of Navcam Camera are incorrect!";
        LOGAddErrorMsg(strErr);
        return false;
    }
    if((PanCamPara.f == 0)|| (PanCamPara.x == 0) ||(PanCamPara.y == 0))
    {
        SCHAR strErr[] = "Parameters of Pancam Camera are incorrect!";
        LOGAddErrorMsg(strErr);
        return false;
    }
    if((AnaPara.dThresHold != 0.000001)|| (AnaPara.nIterTime < 60))
    {
        SCHAR strErr[] = "Iterative parameters are incorrect!";
        LOGAddErrorMsg(strErr);
        return false;
    }

    OptiBase=this->mlBestBase(NavCamPara,PanCamPara, OptiBase,AnaPara.dFixBase,AnaPara.dPixel,AnaPara.dTarget, AnaPara.nWidth,dBestBase,AnaPara.dThresHold, AnaPara.nIterTime);

    if(OptiBase > 0)
    {
        LOGAddSuccessQuitMsg();
        return true;
    }
    else
    {
        SCHAR strErr[] = "Option base is not calculated!";
        LOGAddErrorMsg(strErr);
        return false;
    }


}
/**
* @fn WideBaseMapping
* @date 2011.12.1
* @author 彭嫚
* @brief 长基线制图
* @param vecStereoSet,立体像对
* @param WidePara，长基线匹配结构体参数
* @param vecFPtSetL，输出的左影像特征点
* @param vecFPtSetR，输出的右影像特征点
* @param vecDPtSetL，输出的左影像密集点
* @param vecDPtSetR，输出的右影像密集点
* @param strDemFile，生成DEM文件路径
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/

bool CmlWBaseProc::WideBaseMapping(vector<StereoSet> vecStereoSet, WideOptions WidePara,vector<ImgPtSet>& vecFPtSetL, vector<ImgPtSet>& vecFPtSetR, vector<ImgPtSet>& vecDPtSetL, vector<ImgPtSet>& vecDPtSetR, SCHAR *strDemFile)
{
    bool bIsUsingPartion=WidePara.bIsUsingPartion;
//    bool bIsUsingFeatPt=WidePara.bIsUsingPartion;

    string demstr;
    int nPos = string(strDemFile).rfind("/");
    demstr.assign( string(strDemFile), 0, nPos );
//    int tt=vecStereoSet.size();

    //判断特征点ImgPtSet中的自动匹配点是否存在，不存在则需匹配
    vector<Pt3d> vecFObjs, vecDObjs, vec3dPts;
    if(bIsUsingPartion==true)
    {
        //每对影像分别生成DEM
        for(UINT i=0; i<vecStereoSet.size(); i++)
        {

            int nNuml = vecFPtSetL[i].vecPts.size();
//            int nNumr = vecFPtSetR[i].vecPts.size();
            if(nNuml>0)
            {
                //利用已有点密集匹配
                StereoSet* pStereoSet = &vecStereoSet[i];
                CmlFrameImage clsImgL, clsImgR;
                clsImgL.LoadFile(pStereoSet->imgLInfo.strImgPath.c_str());
                clsImgR.LoadFile(pStereoSet->imgRInfo.strImgPath.c_str());

                CmlPtsManage clsPtsManage;
                vector<StereoMatchPt> vecMatchPts;
                clsPtsManage.GetPairPts(vecFPtSetL[i], vecFPtSetR[i], vecMatchPts);

                //传入左右影像块和特征点进行密集匹配

                return false;
            }
            else
            {
                StereoSet* pStereoSet = &vecStereoSet[i];
                vector<Pt3d> vec3dPt;
                CmlFrameImage clsImgL, clsImgR;

                FrameImgInfo ImgLinfo, ImgRinfo;
                ImgLinfo = pStereoSet->imgLInfo;
                ImgRinfo = pStereoSet->imgRInfo;

                clsImgL.LoadFile(ImgLinfo.strImgPath.c_str());
                clsImgR.LoadFile(ImgRinfo.strImgPath.c_str());

                //特征点匹配
                vector<Pt2d> vecFLPts, vecFRPts, vecDLPts, vecDRPts;
                vector<Pt3d> vecFObj, vecDObj;
                WideFeaMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, vecFLPts, vecFRPts);
                SCHAR strMsgFea[] = "finishing wide baseline feature matching!";
                LOGAddNoticeMsg(strMsgFea);

                //密集匹配
                vector<Pt2d> temFPtL, temFPtR;
                temFPtL = vecFLPts;
                temFPtR = vecFRPts;
                WideDenseMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, temFPtL, temFPtR, vecDLPts, vecDRPts);

                SCHAR strMsgDense[] = "finishing wide baseline dense matching!";
                LOGAddNoticeMsg(strMsgDense);
                // 前方交会生成三维点
                WideBase3Ds(pStereoSet, vecFLPts, vecFRPts, vecFObj);
                WideBase3Ds(pStereoSet, vecDLPts, vecDRPts, vecDObj);

                //利用特征点对密集点进行滤波
                WidePtsFilter(vecFObj, vecDObj, vec3dPt);

                //将密集点转为ImgPtSet
                vecDPtSetL[i].imgInfo = pStereoSet->imgLInfo;
                vecDPtSetR[i].imgInfo = pStereoSet->imgRInfo;

                Pt2d tempLPt, tempRPt;
                for(UINT k=0; k<vecDLPts.size(); k++)
                {
                    tempLPt.X = vecDLPts[k].X;
                    tempLPt.Y = vecDLPts[k].Y;
                    tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
                    tempLPt.byType = 1;
                    tempRPt.X = vecDRPts[k].X;
                    tempRPt.Y = vecDRPts[k].Y;
                    tempRPt.lID = tempLPt.lID;
                    tempRPt.byType = 1;
                    vecDPtSetL[i].vecPts.push_back(tempLPt);
                    vecDPtSetR[i].vecPts.push_back(tempRPt);
                }
                //根据给定的全体DEM的生成路径获取每对影像DEM的路径
                demstr.append( "/" );
                string strTLPath = demstr;
//                SINT nTTPos = demstr.rfind("/");
//                string strLTempHead;
//                strLTempHead.assign(demstr, nTTPos+1,demstr.length());
//                SINT nTTBPos = strLTempHead.rfind(".");
//                string strLTempFinal;
//                strLTempFinal.assign(strLTempHead, 0, nTTBPos);
//                strTLPath.append( strLTempFinal);
                char tt[128];
                sprintf(tt,"%d",i);
                strTLPath.append( string(tt));
                strTLPath.append( ".tif" );

                CmlDemProc cls;
                SCHAR *sdem = const_cast<char*>(strTLPath.c_str());
                cls.mlWriteToGeoFile(vec3dPt, WidePara.XResolution, sdem);
            }
        }
    }
    else
    {
        //分别对每个像对进行匹配
        for(UINT i=0; i<vecStereoSet.size(); i++)
        {

            int nNuml = vecFPtSetL[i].vecPts.size();
//            int nNumr = vecFPtSetR[i].vecPts.size();
            if(nNuml>0)
            {
                //利用已有点密集匹配
                StereoSet* pStereoSet = &vecStereoSet[i];
                CmlFrameImage clsImgL, clsImgR;
                clsImgL.LoadFile(pStereoSet->imgLInfo.strImgPath.c_str());
                clsImgR.LoadFile(pStereoSet->imgRInfo.strImgPath.c_str());

                //对已有匹配点配对
                CmlPtsManage clsPtsManage;
                vector<StereoMatchPt> vecMatchPts;
                clsPtsManage.GetPairPts(vecFPtSetL[i], vecFPtSetR[i], vecMatchPts);

                //传入左右影像块和特征点进行密集匹配
                vector<Pt2d> vecFLPts, vecFRPts, vecDLPts, vecDRPts;
                StereoMatchPt tempMatch;
                Pt2d tempLPt, tempRPt;
                for(UINT j=0; j<vecMatchPts.size(); j++)
                {
                    tempMatch = vecMatchPts[i];
                    tempLPt.X = tempMatch.ptLInImg.X;
                    tempLPt.Y = tempMatch.ptLInImg.Y;
                    vecFLPts.push_back(tempLPt);
                    tempRPt.X = tempMatch.ptRInImg.X;
                    tempRPt.Y = tempMatch.ptRInImg.Y;
                    vecFRPts.push_back(tempRPt);
                }
                WideDenseMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, vecFLPts, vecFRPts, vecDLPts, vecDRPts);

                // 前方交会生成三维点
                WideBase3Ds(pStereoSet, vecFLPts, vecFRPts, vecFObjs);
                WideBase3Ds(pStereoSet, vecDLPts, vecDRPts, vecDObjs);
                WidePtsFilter(vecFObjs, vecDObjs, vec3dPts);

                //将密集点转为ImgPtSet
                vecDPtSetL[i].imgInfo = pStereoSet->imgLInfo;
                vecDPtSetR[i].imgInfo = pStereoSet->imgRInfo;

                //将左右影像获得的密集点转成ImgPtSet格式
                for(UINT k=0; k<vecDLPts.size(); k++)
                {
                    tempLPt.X = vecDLPts[i].X;
                    tempLPt.Y = vecDLPts[i].Y;
                    tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
                    tempRPt.X = vecDRPts[i].X;
                    tempRPt.Y = vecDRPts[i].Y;
                    tempRPt.lID = tempLPt.lID;
                    vecDPtSetL[i].vecPts.push_back(tempLPt);
                    vecDPtSetR[i].vecPts.push_back(tempRPt);
                }

                return true;
            }
            else
            {

                StereoSet* pStereoSet = &vecStereoSet[i];
                CmlFrameImage clsImgL, clsImgR;

                FrameImgInfo ImgLinfo, ImgRinfo;
                ImgLinfo = pStereoSet->imgLInfo;
                ImgRinfo = pStereoSet->imgRInfo;

                clsImgL.LoadFile(ImgLinfo.strImgPath.c_str());
                clsImgR.LoadFile(ImgRinfo.strImgPath.c_str());

                //特征点匹配
                vector<Pt2d> vecFLPts, vecFRPts, vecDLPts, vecDRPts;
                WideFeaMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, vecFLPts, vecFRPts);
                SCHAR strMsgFea[] = "finishing wide baseline feature matching!";
                LOGAddNoticeMsg(strMsgFea);

                //密集匹配
                vector<Pt2d> temFPtL, temFPtR;
                temFPtL = vecFLPts;
                temFPtR = vecFRPts;
                if( false == WideDenseMatch(clsImgL.m_DataBlock, clsImgR.m_DataBlock, WidePara, temFPtL, temFPtR, vecDLPts, vecDRPts) )
                {
                    return false;
                }
                if( vecDLPts.size() != vecDRPts.size() )
                {
                    return false;
                }

                SCHAR strMsgDense[] = "finishing wide baseline dense matching!";
                LOGAddNoticeMsg(strMsgDense);
                // 前方交会生成三维点
                WideBase3Ds(pStereoSet, vecFLPts, vecFRPts, vecFObjs);
                WideBase3Ds(pStereoSet, vecDLPts, vecDRPts, vecDObjs);

                //利用特征点对密集点进行滤波
                WidePtsFilter(vecFObjs, vecDObjs, vec3dPts);

                //将密集点转为ImgPtSet
                vecDPtSetL[i].imgInfo = pStereoSet->imgLInfo;
                vecDPtSetR[i].imgInfo = pStereoSet->imgRInfo;

                Pt2d tempLPt, tempRPt;
                for(UINT k=0; k<vecDLPts.size(); k++)
                {
                    tempLPt.X = vecDLPts[k].X;
                    tempLPt.Y = vecDLPts[k].Y;
                    tempLPt.lID = pStereoSet->imgLInfo.nImgIndex * 10e11 + pStereoSet->imgRInfo.nImgIndex * 10e7 + k + 1;
                    tempLPt.byType = 1;
                    tempRPt.X = vecDRPts[k].X;
                    tempRPt.Y = vecDRPts[k].Y;
                    tempRPt.lID = tempLPt.lID;
                    tempRPt.byType = 1;
                    vecDPtSetL[i].vecPts.push_back(tempLPt);
                    vecDPtSetR[i].vecPts.push_back(tempRPt);
                }

            }
        }
        //根据所有像对获得三维密集点生成DEM
        CmlDemProc clsDemProc;
        clsDemProc.mlWriteToGeoFile(vec3dPts, WidePara.XResolution, strDemFile);


    }

    return true;
}

/**
* @fn WideBase3Ds
* @date 2011.12.1
* @author 彭嫚
* @brief 长基线点元生成
* @param vecStereoSet,立体像对
* @param vecLPts，左影像匹配点
* @param vecRPts，右影像匹配点
* @param vec3ds，生成点云文件
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
bool CmlWBaseProc::WideBase3Ds(StereoSet* pStereoSet, vector<Pt2d>& vecLPts, vector<Pt2d>& vecRPts, vector<Pt3d>& vec3ds)
{

    //根据StereoSet获取内外方位元素
    CmlFrameImage clsImgL, clsImgR;
    if((!clsImgL.LoadFile( pStereoSet->imgLInfo.strImgPath.c_str() )) || (!clsImgR.LoadFile( pStereoSet->imgRInfo.strImgPath.c_str() )))
    {
        SCHAR strErr[] = "Fail to load image file!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }

    vector<StereoMatchPt> MatchPts;
    SINT nCountl = vecLPts.size();
    SINT nCountr = vecRPts.size();
    StereoMatchPt tempPts;

    if((nCountl ==0) || (nCountr ==0) )
    {
        SCHAR strErr[] = "Input matched points are invalid!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    if (nCountl != nCountr)
    {
        SCHAR strErr[] = "Input matched points are not the same!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    clsImgL.m_InOriPara = pStereoSet->imgLInfo.inOri;
    clsImgL.m_ExOriPara = pStereoSet->imgLInfo.exOri;

    // clsImgR.LoadFile( pStereoSet->imgRInfo.strImgPath.c_str() );
    clsImgR.m_InOriPara = pStereoSet->imgRInfo.inOri;
    clsImgR.m_ExOriPara = pStereoSet->imgRInfo.exOri;

    CmlStereoProc clsStereoProc;
    clsStereoProc.m_pDataL = &clsImgL;
    clsStereoProc.m_pDataR = &clsImgR;


    for(int i=0; i< nCountl; i++)
    {
        tempPts.ptLInImg.X = vecLPts[i].X;
        tempPts.ptLInImg.Y = vecLPts[i].Y;
        tempPts.ptRInImg.X = vecRPts[i].X;
        tempPts.ptRInImg.Y = vecRPts[i].Y;
        MatchPts.push_back(tempPts);
    }

    //前方交会计算三维坐标
    clsStereoProc.mlInterSectionInFrameImg( MatchPts );
    SINT nCount =  MatchPts.size();
    for( int i = 0; i < nCount; ++i )
    {
        vec3ds.push_back( MatchPts[i] );
    }
//    LOGAddSuccessQuitMsg();
    return true;

}


//bool CmlWBaseProc::WideBase3Ds(StereoSet* pStereoSetf, StereoSet* pStereoSets, vector<Pt2d>& vecLPts, vector<Pt2d>& vecRPts, vector<Pt3d>& vec3ds)
//{
//    //根据StereoSet获取内外方位元素
//    CmlFrameImage clsImgL, clsImgR;
//
//    if((!clsImgL.LoadFile( pStereoSetf->imgLInfo.strImgPath.c_str() )) || (!clsImgR.LoadFile( pStereoSetf->imgRInfo.strImgPath.c_str() )))
//    {
//        SCHAR strErr[] = "Fail to load image file!\n" ;
//        LOGAddErrorMsg(strErr) ;
//        return false;
//    }
//
//    vector<StereoMatchPt> MatchPts;
//    SINT nCountl = vecLPts.size();
//    SINT nCountr = vecLPts.size();
//    StereoMatchPt tempPts;
//
//    if((nCountl ==0) || (nCountr ==0) )
//    {
//        SCHAR strErr[] = "Input matched points are invalid!\n" ;
//        LOGAddErrorMsg(strErr) ;
//        return false;
//    }
//    if (nCountl != nCountr)
//    {
//        SCHAR strErr[] = "Input matched points are not the same!\n" ;
//        LOGAddErrorMsg(strErr) ;
//        return false;
//    }
//    //clsImgL.LoadFile( pStereoSetf->imgLInfo.strImgPath.c_str() );
//    clsImgL.m_InOriPara = pStereoSetf->imgLInfo.inOri;
//    clsImgL.m_ExOriPara = pStereoSetf->imgLInfo.exOri;
//
//    //clsImgR.LoadFile( pStereoSets->imgLInfo.strImgPath.c_str() );
//    clsImgR.m_InOriPara = pStereoSets->imgLInfo.inOri;
//    clsImgR.m_ExOriPara = pStereoSets->imgLInfo.exOri;
//
//    CmlStereoProc clsStereoProc;
//    clsStereoProc.m_pDataL = &clsImgL;
//    clsStereoProc.m_pDataR = &clsImgR;
//
//
//    for(int i=0; i< nCountl; i++)
//    {
//        tempPts.ptLInImg.X = vecLPts[i].X;
//        tempPts.ptLInImg.Y = vecLPts[i].Y;
//        tempPts.ptRInImg.X = vecRPts[i].X;
//        tempPts.ptRInImg.Y = vecRPts[i].Y;
//        MatchPts.push_back(tempPts);
//    }
//
//    //前方交会计算三维坐标
//    clsStereoProc.mlInterSectionInFrameImg( MatchPts );
//    UINT nCount =  MatchPts.size();
//    for( int i = 0; i < nCount; ++i )
//    {
//        vec3ds.push_back( MatchPts[i] );
//    }
//    return true;
//}
/**
* @fn WideDenseMatch
* @date 2011.12.1
* @author 彭嫚
* @brief 密集匹配
* @param LOriImg,待匹配左影像
* @param ROriImg，待匹配右影像
* @param WidePara，长基线匹配相关参数
* @param vecFPtL，左影像特征点
* @param vecFPtR，右影像特征点
* @param vecMPtL，左影像密集点
* @param vecMPtL，右影像密集点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
bool CmlWBaseProc::WideDenseMatch(CmlRasterBlock LOriImg,CmlRasterBlock ROriImg, WideOptions WidePara, vector<Pt2d> &vecFPtLs, vector<Pt2d> &vecFPtRs, vector<Pt2d> &vecMPtLs, vector<Pt2d> &vecMPtRs)
{

    //调用特征匹配函数
    //WideFeaMatch(LOriImg, ROriImg, WidePara, vecFPtLs, vecFPtRs);
    //cout<<vecFPtLs.size()<<endl;

    //判断输入影像是否为空
    if((sizeof(LOriImg) == 0) || (sizeof(ROriImg) == 0))
    {
        SCHAR strErr[] = "Images are not loaded accurately!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    //使用特征点生成核线影像
    CmlRasterBlock pLEpiImg, pREpiImg;
    CmlMat LH,  RH;
    LH.Initial(3, 3);
    RH.Initial(3, 3);
    mlWideEpiImg(vecFPtLs, vecFPtRs, &LOriImg, &ROriImg, &pLEpiImg, &pREpiImg, LH, RH);


    //将特征点转换成核线影像的点
    vector<Pt2d> vecEPtsL,  vecEPtsR;
    mlOriPtToEpi(vecFPtLs, vecFPtRs, LH, RH, vecEPtsL, vecEPtsR);


    //对核线影像block进行高斯滤波
    CmlFrameImage clsFrame;
    clsFrame.SmoothByGuassian(pLEpiImg, 7, 0.6);
    clsFrame.SmoothByGuassian(pREpiImg, 7, 0.6);

    //将特征点的vector转成StereoMatchPt格式
    vector<StereoMatchPt> vecPts;
    StereoMatchPt tempPt;
    UINT nNum = vecEPtsL.size();
    Pt2d temptl, temptr;
    for(UINT i=0; i < nNum; i++)
    {
        temptl = vecEPtsL[i];
        tempPt.ptLInImg.X = temptl.X;
        tempPt.ptLInImg.Y = temptl.Y;

        temptr = vecEPtsR[i];
        tempPt.ptRInImg.X = temptr.X;
        tempPt.ptRInImg.Y = temptr.Y;
        vecPts.push_back(tempPt);

    }

    //在核线影像上进行密集匹配
    vector<Pt2d> vecEDPtsL, vecEDPtsR;
    CmlStereoProc clsStereo;
    bool bDense;
    bDense = clsStereo.mlDenseMatch(&pLEpiImg, &pREpiImg, vecPts, WidePara, vecEDPtsL, vecEDPtsR);
    if(!bDense)
    {
        return false;
    }

    //核线影像上的密集点反算到原始影像
    mlEpiPtToOri(vecEDPtsL, vecEDPtsR, LH, RH, vecMPtLs, vecMPtRs);

    return true;


}

/**
* @fn WideFeaMatch
* @date 2011.12.1
* @author 彭嫚
* @brief 特征匹配
* @param LOriImg,待匹配左影像
* @param ROriImg，待匹配右影像
* @param WidePara，长基线匹配相关参数
* @param vecPtL，左影像特征点
* @param vecPtR，右影像特征点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
bool CmlWBaseProc:: WideFeaMatch(CmlRasterBlock LOriImg,CmlRasterBlock ROriImg, WideOptions WidePara, vector<Pt2d> &vecPtL, vector<Pt2d> &vecPtR)
{
    //判断输入影像是否为空
    if((sizeof(LOriImg) == 0) || (sizeof(ROriImg) == 0))
    {
        return false;
    }

    UINT nMaxCheck = WidePara.nMaxCheck;
    DOUBLE dRatio = WidePara.dRatio;
    DOUBLE dThresh = WidePara.dThresh;
    DOUBLE dConfidence = WidePara.dConfidence;
    CmlStereoProc clsStereo;

    //sift匹配
    vector<double> vxl;
    vector<double> vyl;
    vector<double> vxr;
    vector<double> vyr;

    UINT nPtNum = SiftMatchVector((char*)LOriImg.GetData( ),LOriImg.GetW(),LOriImg.GetH(),8,(char*)ROriImg.GetData( ),ROriImg.GetW(),ROriImg.GetH(),8,vxl,vyl,vxr,vyr,nMaxCheck,dRatio);

    if(nPtNum <=0 )
    {
        return false;
    }
    vector<StereoMatchPt> MatchPts;
    SINT nCount;
    StereoMatchPt tempPts;
    for(UINT i=0; i<nPtNum; i++)
    {
        tempPts.ptLInImg.X = vxl[i];
        tempPts.ptLInImg.Y = vyl[i];
        tempPts.ptRInImg.X = vxr[i];
        tempPts.ptRInImg.Y = vyr[i];
        MatchPts.push_back(tempPts);
    }

    //删除重复元素
    nCount = clsStereo.mlGetRansacPts(MatchPts,dThresh,dConfidence);
    clsStereo.mlUniquePt(MatchPts);

    nCount = MatchPts.size();

    if(nCount<=0)
    {
        return false;
    }
    Pt2d temptl, temptr;
    for(int i=0; i<nCount; i++)
    {

        temptl.X = MatchPts[i].ptLInImg.X;
        temptl.Y = MatchPts[i].ptLInImg.Y;
        vecPtL.push_back(temptl);
        temptr.X = MatchPts[i].ptRInImg.X;
        temptr.Y = MatchPts[i].ptRInImg.Y;
        vecPtR.push_back(temptr);
    }

    return true;

}
/**
* @fn WidePtsFilter
* @date 2011.12.1
* @author 彭嫚
* @brief 对长基线密集点的三维点云滤波去除粗差
* @param vecFPts,特征点三维点云数据
* @param vecDPts,密集点三维点云数据
* @param vec3dPts, 总的三维点云数据
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/
bool CmlWBaseProc::WidePtsFilter(vector<Pt3d>& vecFPts, vector<Pt3d>& vecDPts, vector<Pt3d>& vec3dPts)
{
    //利用特征点云获得矩形范围
    DbRect rectFea;
    SINT nNum, nNumDense ;
    nNum = vecFPts.size();
    nNumDense = vecDPts.size();
    if((nNum<=0) || (nNumDense <=0))
    {

        return false ;
    }
    Pt3d ptTemp ;
    for(SINT i = 0 ; i < nNum ; i++)
    {
        ptTemp = vecFPts[i] ;
        rectFea.left = MIN(ptTemp.X , rectFea.left) ;
        rectFea.right = MAX(ptTemp.X , rectFea.right) ;
        rectFea.bottom = MIN(ptTemp.Y , rectFea.bottom) ;
        rectFea.top = MAX(ptTemp.Y , rectFea.top) ;
    }

    vector<Pt3d> vecDPtf;


    //利用特征点云的范围去掉密集匹配获得的离散点
    for(SINT i = 0 ; i < nNumDense ; i++)
    {
        ptTemp = vecDPts[i];
        if((ptTemp.X >= rectFea.left)&&(ptTemp.X <= rectFea.right)&&(ptTemp.Y >= rectFea.bottom)&&(ptTemp.Y <= rectFea.top))
        {
            vecDPtf.push_back(ptTemp);
        }

    }

    //将两组点集合成利用地形连续性去掉粗差，保证生成地形正确
    vector<Pt3d>   vecTemp;
    vecTemp.insert(vecTemp.end(),   vecFPts.begin(),   vecFPts.end());
    vecTemp.insert(vecTemp.end(),   vecDPtf.begin(),   vecDPtf.end());

    //将地形滤波后的点云导出
    vec3dPts = vecTemp;
    return true;

}

/**
* @fn mlFunAcc
* @date 2011.12.1
* @author 彭嫚
* @brief 测图精度函数的一阶导数和二阶导数
* @param dFixBase,导航相机固定基线长
* @param dBase,基线长函数变量
* @param dFunAcc,一阶导数和二阶导数
* @param mlNav,导航相机参数
* @param mlPan,全景相机参数
* @param dPixel,像素匹配误差
* @param dTarget,目标距离
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/

void CmlWBaseProc::mlFunAcc(DOUBLE dFixBase,DOUBLE dBase,DOUBLE dFunAcc[2],InOriPara mlNav, InOriPara mlPan,DOUBLE dPixel, DOUBLE dTarget)
{
    DOUBLE dFocaln = mlNav.f;
    DOUBLE dFocalp = mlPan.f;


    //DOUBLE dMinbase = 1.0;
    //DOUBLE dMaxbase = (dFocalp/ nWidth) * dTarget;

    DOUBLE dCoeFir = pow(dTarget,2)*dPixel*dPixel/(8*dFixBase*dFixBase*dFocaln*dFocaln);
    DOUBLE dCoeSec = pow(dTarget,4)*dPixel*dPixel/(dFocalp*dFocalp);

    dFunAcc[0] = 0.5*(pow(dCoeFir*dBase*dBase+dCoeSec/dBase/dBase,-0.5))*(2*dCoeFir*dBase - 2*dCoeSec/dBase/dBase/dBase);
    dFunAcc[1] = -0.25*(pow(dCoeFir*dBase*dBase+dCoeSec/dBase/dBase,-1.5))*(pow((2*dCoeFir*dBase - 2*dCoeSec/dBase/dBase/dBase),2))+0.5*(pow(dCoeFir*dBase*dBase+dCoeSec/dBase/dBase,-0.5))*(2*dCoeFir+6*dCoeSec/dBase/dBase/dBase/dBase);

}

/**
* @fn mlNewTon
* @date 2011.11.22
* @author 彭嫚
* @brief 牛顿迭代法求解函数值
* @param dFixBase,导航相机固定基线长
* @param dBase,基线长函数变量
* @param dThreshold,阈值大小
* @param dIterTime,迭代次数
* @param mlNav,导航相机参数
* @param mlPan,全景相机参数
* @param dPixel,像素匹配误差
* @param dTarget,目标距离
* @retval TRUE 成功
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/

DOUBLE CmlWBaseProc::mlNewTon(DOUBLE dFixBase,DOUBLE * dBase,DOUBLE dThresHold, DOUBLE dIterTime,InOriPara mlNav, InOriPara mlPan,DOUBLE dPixel, DOUBLE dTarget)
{
    UINT nK,nL;
    DOUBLE dFun[2], dD,dP,dBase0;
    DOUBLE dBase1 = 0;
    nL = dIterTime;
    nK = 1;
    dBase0 = *dBase;
    mlFunAcc(dFixBase,dBase0,dFun, mlNav, mlPan, dPixel, dTarget);
    dD = dThresHold+1.0;
    dBase = 0;

    while ((dD>=dThresHold) && (nL!=0))
    {
        if (fabs(dFun[1])+1.0 ==1.0)
        {
            printf("err\n");
            return(-1);
        }
        dBase1=dBase0-dFun[0]/dFun[1];
        mlFunAcc(dFixBase,dBase1, dFun, mlNav, mlPan, dPixel, dTarget);
        dD=fabs(dBase1-dBase0);
        dP=fabs(dFun[0]);
        if(dP>dD)
            dD = dP;

        dBase0 = dBase1;
        nL = nL-1;

    }
    *dBase = dBase1;
    nK = dIterTime-1;
    return(*dBase);
}

/**
* @fn mlBestBase
* @date 2011.12.1
* @author 彭嫚
* @brief 计算最优基线长
* @param mlNav,导航相机参数
* @param mlPan, 全景相机参数
* @param dOptiBase,最优基线长
* @param dFixBase,导航相机固定基线长
* @param dPixel,像素匹配误差
* @param dTarget,目标距离
* @param nWidth,影像宽度
* @param dBase,基线长函数变量
* @param dThreshold,阈值大小
* @param dIterTime,迭代次数
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/

DOUBLE CmlWBaseProc::mlBestBase(InOriPara mlNav, InOriPara mlPan,DOUBLE dOptiBase,DOUBLE dFixBase, DOUBLE dPixel, DOUBLE dTarget, UINT nWidth,DOUBLE * dBase,DOUBLE dThresHold, DOUBLE dIterTime)
{

    DOUBLE dFocalp = mlPan.f;


    DOUBLE dMinbase = 1.0;
    DOUBLE dMaxbase = (dFocalp/ nWidth/dPixel) * dTarget;

    dBase = &dMinbase;
    dOptiBase = mlNewTon(dFixBase,dBase, dThresHold, dIterTime, mlNav, mlPan, dPixel, dTarget);

    if((dOptiBase < dMaxbase)&&(dOptiBase > 0))
        return(dOptiBase);

    else
        return 0;

}

/**
* @fn mlOriPtToEpi
* @date 2011.12.1
* @author 彭嫚
* @brief 将原始影像的匹配点转到核线影像上的点
* @param vecOPtsL，原始左影像匹配点
* @param vecOPtsR，原始右影像匹配点`
* @param LeftHomo，左影像透视矩阵
* @param RightHomo，右影像透视矩阵
* @param vecEPtsL,左核线影像匹配点
* @param vecEPtsR,右核线影像匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/

bool CmlWBaseProc::mlOriPtToEpi(const vector<Pt2d>& vecOPtsL, const vector<Pt2d>& vecOPtsR,CmlMat& LeftHomo, CmlMat& RightHomo, vector<Pt2d>& vecEPtsL, vector<Pt2d>& vecEPtsR)
{
    //判断输入的匹配点是否为空并且相等
    if ((vecOPtsL.size()==0)||(vecOPtsR.size()==0)||(vecOPtsL.size()!=vecOPtsR.size()))
    {
        return false;
    }

    //判断透视矩阵是否等于0
    int nNuml=0, nNumr=0;
    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j <3; j++)
        {
            if(LeftHomo.GetAt(i,j)==0)
                nNuml++;

        }

    }

    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j <3; j++)
        {
            if(RightHomo.GetAt(i,j)==0)
                nNumr++;

        }

    }

    if(nNuml==9||nNumr==9)
    {
        return false;
    }

    CmlMat  MatOPtsL, MatOPtsR, MatEpiL, MatEpiR;
    MatOPtsL.Initial(3,1);
    MatOPtsR.Initial(3,1);
    MatEpiL.Initial(3,1);
    MatEpiR.Initial(3,1);


    //根据透视变换的几何关系，透视矩阵逆矩阵M*[xepi yepi 1]=[tXori tYori t]进行转换
    for(UINT i=0; i<vecOPtsL.size(); i++)
    {
        Pt2d TemPtL, TemPtR, TemPtEL, TemPtER;
        //将左边影像的点进行转换
        TemPtL=vecOPtsL[i];
        MatOPtsL.SetAt(0,0,TemPtL.X);
        MatOPtsL.SetAt(1,0,TemPtL.Y);
        MatOPtsL.SetAt(2,0,1);
        mlMatMul(&LeftHomo,&MatOPtsL,&MatEpiL);
        if(MatEpiL.GetAt(2,0) ==0)
        {
            return false;
        }
        TemPtEL.X=MatEpiL.GetAt(0,0)/MatEpiL.GetAt(2,0);
        TemPtEL.Y=MatEpiL.GetAt(1,0)/MatEpiL.GetAt(2,0);
        vecEPtsL.push_back(TemPtEL);

        //将右边影像的点进行转换
        TemPtR=vecOPtsR[i];
        MatOPtsR.SetAt(0,0,TemPtR.X);
        MatOPtsR.SetAt(1,0,TemPtR.Y);
        MatOPtsR.SetAt(2,0,1);
        mlMatMul(&RightHomo,&MatOPtsR,&MatEpiR);
        if(MatEpiR.GetAt(2,0) ==0)
        {
            return false;
        }
        TemPtER.X=MatEpiR.GetAt(0,0)/MatEpiR.GetAt(2,0);
        TemPtER.Y=MatEpiR.GetAt(1,0)/MatEpiR.GetAt(2,0);
        vecEPtsR.push_back(TemPtER);

    }

    return true;


}


/**
* @fn mlEpiPtToOri
* @date 2011.12.1
* @author 彭嫚
* @brief 将核线影像的匹配点转到原始影像上的点
* @param vecEPtsL,左核线影像匹配点
* @param vecEPtsR,右核线影像匹配点
* @param LeftHomo，左影像透视矩阵
* @param RightHomo，右影像透视矩阵
* @param vecOPtsL，原始左影像匹配点
* @param vecOPtsR，原始右影像匹配点
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/

bool CmlWBaseProc::mlEpiPtToOri(const vector<Pt2d>& vecEPtsL, const vector<Pt2d>& vecEPtsR,CmlMat& LeftHomo, CmlMat& RightHomo, vector<Pt2d>& vecOPtsL, vector<Pt2d>& vecOPtsR)
{
    //判断输入的匹配点是否相等
    if ((vecEPtsL.size()==0)||(vecEPtsR.size()==0)||(vecEPtsL.size()!=vecEPtsR.size()))
    {
        return false;
    }


    //判断透视矩阵是否等于0
    int nNuml=0, nNumr=0;
    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j <3; j++)
        {
            if(LeftHomo.GetAt(i,j)==0)
                nNuml++;

        }

    }

    for(int i = 0; i < 3; i++)
    {
        for(int j = 0; j <3; j++)
        {
            if(RightHomo.GetAt(i,j)==0)
                nNumr++;

        }

    }

    if(nNuml==9||nNumr==9)
    {
        return false;
    }


    //初始化透视矩阵的逆矩阵LeftHomoTran,RightHomoTran; 初始化核线影像上和原始原始影像上的匹配点矩阵MatEPTs,MatOri
    CmlMat LeftHomoTran, RightHomoTran, MatEPtsL, MatEPtsR, MatOriL, MatOriR;
    LeftHomoTran.Initial(3, 3);
    RightHomoTran.Initial(3,3);
    MatEPtsL.Initial(3, 1);
    MatEPtsR.Initial(3, 1);
    MatOriL.Initial(3, 1);
    MatOriR.Initial(3, 1);

    if(mlMatInv(&LeftHomo,&LeftHomoTran)&&mlMatInv(&RightHomo,&RightHomoTran))
    {
        for(UINT i=0; i<vecEPtsL.size(); i++)
        {
            Pt2d TemPtL, TemPtR, TemPtOL, TemPtOR;
            //根据透视变换的几何关系，透视矩阵的逆矩阵M*[xepi yepi 1]=[tXori tYori t]将左边影像的点进行转换
            TemPtL=vecEPtsL[i];
            MatEPtsL.SetAt(0,0,TemPtL.X);
            MatEPtsL.SetAt(1,0,TemPtL.Y);
            MatEPtsL.SetAt(2,0,1);
            mlMatMul(&LeftHomoTran,&MatEPtsL,&MatOriL);
            if(MatOriL.GetAt(2,0) ==0)
            {
                return false;
            }
            TemPtOL.X=MatOriL.GetAt(0,0)/MatOriL.GetAt(2,0);
            TemPtOL.Y=MatOriL.GetAt(1,0)/MatOriL.GetAt(2,0);
            vecOPtsL.push_back(TemPtOL);
            //将右边影像的点进行转换
            TemPtR=vecEPtsR[i];
            MatEPtsR.SetAt(0,0,TemPtR.X);
            MatEPtsR.SetAt(1,0,TemPtR.Y);
            MatEPtsR.SetAt(2,0,1);
            mlMatMul(&RightHomoTran,&MatEPtsR,&MatOriR);
            if(MatOriR.GetAt(2,0) ==0)
            {
                return false;
            }
            TemPtOR.X=MatOriR.GetAt(0,0)/MatOriR.GetAt(2,0);
            TemPtOR.Y=MatOriR.GetAt(1,0)/MatOriR.GetAt(2,0);
            vecOPtsR.push_back(TemPtOR);
        }

        return true;
    }
    else
    {
        return false;
    }

}


/**
* @fn mlWideEpiImg
* @date 2011.12.9
* @author 彭嫚
* @brief 根据正确匹配的点计算基础矩阵生成长基线影像的核线影像
* @param vecEPtsL,左影像匹配像点
* @param vecEPtsR,右影像匹配像点
* @param pLOriImg,原始左影像
* @param pROriImg,原始右影像
* @param pLEpiImg,左影像核线影像
* @param pREpiImg,右影像核线影像
* @param LeftHomo,左影像单应矩阵
* @param RightHomo,右影像单应矩阵
* @retval TRUE 成功
* @retval false 失败
* @version 1.1
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*
*/

bool CmlWBaseProc::mlWideEpiImg(const vector<Pt2d>&vecEPtsL, const vector<Pt2d>& vecEPtsR,  CmlRasterBlock* pLOriImg, CmlRasterBlock* pROriImg, CmlRasterBlock* pLEpiImg, CmlRasterBlock* pREpiImg, CmlMat& LeftHomo, CmlMat& RightHomo)
{

    //判断输入的匹配点是否相等
    if ((vecEPtsL.size() == 0) || (vecEPtsR.size() == 0) || (vecEPtsL.size()!=vecEPtsR.size()) )
    {
        return false;
    }
    int nNum = vecEPtsL.size();

    IplImage* pOriImgL = NULL;
    IplImage* pOriImgR = NULL;


    vector<cv::Point2f> points1, points2;

    //将vector类型转成opencv格式Point2f
    for(int i=0 ; i< nNum; i++)
    {

        points1.push_back(cv::Point2f(vecEPtsL[i].X,vecEPtsL[i].Y));
        points2.push_back(cv::Point2f(vecEPtsR[i].X,vecEPtsR[i].Y));
    }

    //使用7点算法计算像对间的基础矩阵
    cv::Mat FundMatri = cv::findFundamentalMat(points1, points2, cv::FM_7POINT, 3, 0.99);

    if(CmlRBlock2IplImg(pLOriImg, pOriImgL)&&CmlRBlock2IplImg( pROriImg, pOriImgR))
    {
        cv::Mat ImgL, ImgR, rectified1, rectified2, H1, H2;

        //将iplImg转成mat类型
        ImgL =  pOriImgL;
        ImgR =  pOriImgR;

        //计算校正变换
        stereoRectifyUncalibrated(points1, points2, FundMatri, ImgL.size(), H1, H2);

        //生成左影像的核线影像
        warpPerspective(ImgL, rectified1, H1, ImgL.size());


        //将Mat转成IplImage
        IplImage *pEpiImgL  = new IplImage(rectified1);
        //IplImage转成RasterBlock
        IplImage2CmlRBlock(pEpiImgL,pLEpiImg);

        warpPerspective(ImgR, rectified2, H2, ImgR.size());

        IplImage  *pEpiImgR  = new IplImage(rectified2);
        IplImage2CmlRBlock(pEpiImgR,pREpiImg);

        //将透视矩阵转成CvMat类型
        CvMat MatHL = H1;
        CvMat MatHR = H2;
        CvMat2mlMat(&MatHL, &LeftHomo);
        CvMat2mlMat(&MatHR, &RightHomo);

//        if( pEpiImgL != NULL)
//        {
//            cvReleaseImage( &pEpiImgL );
//        }
//
//        if( pEpiImgR != NULL)
//        {
//            cvReleaseImage( &pEpiImgR );
//        }

        return true;
    }
    return false;

}







