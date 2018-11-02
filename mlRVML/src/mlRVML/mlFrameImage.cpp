/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlFrameImage.cpp
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像处理源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#include "mlFrameImage.h"
#include "mlTypeConvert.h"
#include "mlPhgProc.h"
#include <algorithm>

/**
* @fn CmlFrameImage
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlFrameImage类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool calcHazOrigPt( DOUBLE &dTmpx, DOUBLE &dTmpy, DOUBLE dOriTmpX, DOUBLE dOriTmpY, DOUBLE df );

CmlFrameImage::CmlFrameImage()
{
    //ctor
}

/**
* @fn ~CmlFrameImage
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief CmlFrameImage类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlFrameImage::~CmlFrameImage()
{
    m_DataBlock.Clear();
}
/**
* @fn LoadFile
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 读取图像并直接存入对应的block中
* @param FileName 文件路径
* @param nType 文件类型
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::LoadFile( const SCHAR *FileName, SINT nType )
{
    this->CmlGdalDataset::LoadFile( FileName );

    m_DataBlock.Clear();

    if( this->GetBands() == 3 )
    {
        if( true == this->CmlGdalDataset::GetRasterGrayBlock( 1,2,3, 0, 0, this->GetWidth(), this->GetHeight(), 1, &m_DataBlock ))
        {
            m_DataBlock.m_pGdalData = this;
            m_DataBlock.SetXOffSet( 0 );//(CmlGdalDataset*)
            m_DataBlock.SetYOffSet( 0 );
            m_DataBlock.SetZoom( 1 );
        }
        else
        {
            return false;
        }
    }
    else
    {
        if( true == this->CmlGdalDataset::GetRasterGrayBlock( (UINT)1, (UINT)0, (UINT)0, this->GetWidth(), this->GetHeight(), (UINT)1, &m_DataBlock ) )
        {
            m_DataBlock.m_pGdalData = this;
            m_DataBlock.SetXOffSet( 0 );
            m_DataBlock.SetYOffSet( 0 );
            m_DataBlock.SetZoom( 1 );
        }
        else
        {
            return false;
        }
    }
    return true;
}
/**
* @fn GetUnDistortImg
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像畸变校正
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::GetUnDistortImg( CAMTYPE nCamType, DOUBLE dZoomCoef )
{

    CmlRasterBlock tmpBlock;
//    tmpBlock.InitialImg( this->GetHeight(), this->GetWidth() );
    this->GetUnDistortImg( &m_DataBlock, &tmpBlock, nCamType, dZoomCoef );
    DOUBLE dXOff = (tmpBlock.GetW() - m_DataBlock.GetW()) / 2.0;
    DOUBLE dYOff = (tmpBlock.GetH() - m_DataBlock.GetH()) / 2.0;

    this->m_DataBlock.FreeData();
    this->m_DataBlock.InitialImg( tmpBlock.GetH(), tmpBlock.GetW() );

    memcpy( m_DataBlock.GetData(), tmpBlock.GetData(), tmpBlock.GetW()*tmpBlock.GetH()*tmpBlock.GetBytes() );
//    m_DataBlock = tmpBlock;

    m_nHeight = tmpBlock.GetH();
    m_nWidth = tmpBlock.GetW();
    m_InOriPara.x += dXOff;
    m_InOriPara.y += dYOff;
    m_InOriPara.k1 = 0;
    m_InOriPara.k2 = 0;
    m_InOriPara.k3 = 0;
    m_InOriPara.p1 = 0;
    m_InOriPara.p2 = 0;
    m_InOriPara.alpha = 0;
    m_InOriPara.beta = 0;

    return true;
}
/**
* @fn GetUnDistortImg
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像畸变校正
* @param pInImg 输入图像
* @param pOutImg 输出图像
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/

bool CmlFrameImage::GetUnDistortImg( CmlRasterBlock* pInImg,  CmlRasterBlock* pOutImg, CAMTYPE nCamType, DOUBLE dZoomCoef )
{
    if( !pInImg->IsValid() )
    {
        bool bVal = pInImg->InitialImg( this->GetHeight(), this->GetWidth() );
        if( false == bVal )
        {
            return false;
        }
    }
    else
    {
        if( ( pInImg->GetW() != this->GetWidth() )||( pInImg->GetH() != this->GetHeight() ) )
        {
            return false;
        }
    }

    CRasterPt2D Coordinate;
//    if( false == Coordinate.Initial(pInImg->GetH(),pInImg->GetW()) )
//    {
//        return false;
//    }
    bool H1,H2;
    H1 = mlGetDistortionCoordinate( pInImg , &Coordinate, nCamType, dZoomCoef );

    //////////////////////////////////////////////


    if(true == H1)
    {
        H2 = mlGrayInterpolation( pInImg,&Coordinate,pOutImg,0);
        return H2;
    }
    else
    {
        return false;
    }
}
/**
* @fn SmoothByGuassian
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像去噪高斯滤波
* @param nTemplateSize 滤波模板大小
* @param dCoef 滤波核参数,一般以0.8为宜
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::SmoothByGuassian( SINT nTemplateSize, DOUBLE dCoef )
{
    if( m_DataBlock.IsValid() == false )
    {
        return false;
    }

    IplImage* pImg = NULL;
    if( false == CmlRBlock2IplImg( &m_DataBlock, pImg  ) )
    {
        return false;
    }
    IplImage* pSTempImg = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_8U, 1 );
    cvSmooth( pImg, pSTempImg, CV_GAUSSIAN, nTemplateSize, nTemplateSize, dCoef );

    if( false == IplImage2CmlRBlock( pSTempImg, &m_DataBlock ) )
    {
        cvReleaseImage( &pSTempImg );
        cvReleaseImage( &pImg );
        return false;
    }
    else
    {
        cvReleaseImage( &pSTempImg );
        cvReleaseImage( &pImg );
        return true;
    }
}

/**
* @fn SmoothByGuassian
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像去噪高斯滤波
* @param nTemplateSize 滤波模板大小
* @param dCoef 滤波核参数,一般以0.8为宜
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::SmoothByGuassian(  CmlRasterBlock &clsBlock, SINT nTemplateSize, DOUBLE dCoef )
{
    if( clsBlock.IsValid() == false )
    {
        return false;
    }

    IplImage* pImg = NULL;
    if( false == CmlRBlock2IplImg( &clsBlock, pImg  ) )
    {
        return false;
    }
    IplImage* pSTempImg = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_8U, 1 );
    cvSmooth( pImg, pSTempImg, CV_GAUSSIAN, nTemplateSize, nTemplateSize, dCoef );

    if( false == IplImage2CmlRBlock( pSTempImg, &clsBlock ) )
    {
        cvReleaseImage( &pSTempImg );
        cvReleaseImage( &pImg );
        return false;
    }
    else
    {
        cvReleaseImage( &pSTempImg );
        cvReleaseImage( &pImg );
        return true;
    }
}
/**
* @fn ExtractFeatPtByForstner
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 面阵相机影像Forstner方法提取特征点
* @param nGridSize 格网大小
* @param nPtNum 欲提取的点数
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::ExtractFeatPtByForstner( SINT nGridSize, SINT nPtNum, DOUBLE dThresCoef, bool bIsRemoveAbPixel  )
{
    return this->ExtractFeatPtByForstner( m_DataBlock, m_vecFeaPtsList, nGridSize, nPtNum, dThresCoef, bIsRemoveAbPixel );
}


bool CmlFrameImage::UnDisCorToPlaneFrame( Pt2d pt, InOriPara inPara, Pt2d &ptRes, CAMTYPE nCamType )
{
    return UnDisCorToPlaneFrame( pt, inPara, this->GetHeight(), ptRes, nCamType );
}
bool calcPanOrigPt( Pt2d ptDistRes, Pt2d &ptUnDisRes, InOriPara inOri )
{
    DOUBLE dx = ptDistRes.X / inOri.f;
    DOUBLE dy = ptDistRes.Y / inOri.f;
    DOUBLE dCurXRes, dCurYRes;
    dCurXRes = dCurYRes = 0.0;
    DOUBLE dThres = 0.01 / inOri.f;
    UINT nItera = 0;
    do
    {
        DOUBLE dR2 = dx*dx + dy*dy;
        DOUBLE dR4 = dR2 * dR2;
        DOUBLE dR6 = dR2 * dR4;
        DOUBLE dR2Tox = 2 * dx;
        DOUBLE dR2Toy = 2 * dy;

        DOUBLE dR4Tox = 2 * dR2 * dR2Tox;
        DOUBLE dR4Toy = 2 * dR2 * dR2Toy;

        DOUBLE dR6Tox = 3 * dR2 * dR2 * dR2Toy;
        DOUBLE dR6Toy = 3 * dR2 * dR2 * dR2Toy;

        DOUBLE dXK1K2K5Tox = ( inOri.k1 * dR2Tox + inOri.k2 * dR4Tox + inOri.k3 * dR6Tox ) * dx + ( 1 + inOri.k1 * dR2 + inOri.k2 * dR4 + inOri.k3 * dR6 );
        DOUBLE dXK1K2K5Toy = ( inOri.k1 * dR2Toy + inOri.k2 * dR4Toy + inOri.k3 * dR6Toy ) * dx;

        DOUBLE dYK1K2K5Tox = ( inOri.k1 * dR2Tox + inOri.k2 * dR4Tox + inOri.k3 * dR6Tox ) * dy;
        DOUBLE dYK1K2K5Toy = ( inOri.k1 * dR2Toy + inOri.k2 * dR4Toy + inOri.k3 * dR6Toy ) * dy + ( 1 + inOri.k1 * dR2 + inOri.k2 * dR4 + inOri.k3 * dR6 );

        DOUBLE dXP1P2Tox = 2 * inOri.p1 * dy + inOri.p2 * ( dR2Tox + 4*dx);
        DOUBLE dXP1P2Toy = 2 * inOri.p1 * dx + inOri.p2 * ( dR2Toy + 0);

        DOUBLE dYP1P2Tox = inOri.p1 * ( dR2Tox + 0) + 2 * inOri.p2 * dy;
        DOUBLE dYP1P2Toy = inOri.p1 * ( dR2Toy + 4*dy) + 2 * inOri.p2 * dx;

        DOUBLE dXdTox = dXK1K2K5Tox + dXP1P2Tox;
        DOUBLE dXdToy = dXK1K2K5Toy + dXP1P2Toy;

        DOUBLE dYdTox = dYK1K2K5Tox + dYP1P2Tox;
        DOUBLE dYdToy = dYK1K2K5Toy + dYP1P2Toy;

        DOUBLE dF2x = inOri.f * ( dXdTox + inOri.dSkew * dYdTox );
        DOUBLE dF2y = inOri.f * ( dXdToy + inOri.dSkew * dYdToy );

        DOUBLE dG2x = inOri.f2 * ( dYdTox );
        DOUBLE dG2y = inOri.f2 * ( dYdToy );

        DOUBLE dFunF = ( 1 + inOri.k1 * dR2 + inOri.k2 * dR4 + inOri.k3 * dR6 )* dx + 2 * inOri.p1 * dx * dy + inOri.p2 * ( dR2 + 2*dx*dx ) - dx;
        DOUBLE dFunG = ( 1 + inOri.k1 * dR2 + inOri.k2 * dR4 + inOri.k3 * dR6 )* dy + inOri.p1 * ( dR2 + 2*dy*dy ) + 2 * inOri.p2 * dx * dy - dy;

        DOUBLE dTmpCoef = dG2x * dF2y - dF2x * dG2y;

        dCurXRes = ( dFunF * dG2y - dFunG * dF2y ) / dTmpCoef;
        dCurYRes = ( dFunG * dF2x - dFunF * dG2x ) / dTmpCoef;

        dx += dCurXRes;
        dy += dCurYRes;
        if( nItera++ > 100 )
        {
            return false;
        }
    }
    while((dCurXRes > dThres)||( dCurYRes > dThres) );

    ptUnDisRes.X = dx * inOri.f;
    ptUnDisRes.Y = dy * inOri.f;

    return true;

}
bool CmlFrameImage::UnDisCorToPlaneFrame( Pt2d pt, InOriPara inPara, UINT nHeight, Pt2d &ptRes, CAMTYPE nCamType )
{
    if( ( nCamType == Nav_Cam )||( nCamType == Haz_Cam ) )
    {
        ptRes = pt;
        DOUBLE ddx, ddy, dx, dy, r;
        double x0 = inPara.x;
        double y0 = nHeight - inPara.y - 1;

        /*换算成像主点为中心的焦平面坐标*/

        ddx = ( pt.X - x0 ) * inPara.dPixelS;
        ddy = ((nHeight-pt.Y-1) - y0) * inPara.dPixelS ;
        r = ddx * ddx + ddy * ddy ;
        dx = ddx * (r * (inPara.k1 + inPara.k2 * r + inPara.k3 * r * r) + inPara.alpha) + inPara.beta * ddy +
             inPara.p1*(r + 2.0 * ddx * ddx) + 2.0 * inPara.p2 * ddx * ddy;
        dy = ddy * (r * (inPara.k1 + inPara.k2 * r + inPara.k3 * r * r)) + 2.0 * inPara.p1 * ddx * ddy + inPara.p2 * (r + 2.0 * ddy * ddy);
        /*得到改正后坐标*/
        ptRes.X = (ddx + dx) / inPara.dPixelS;
        ptRes.Y = (ddy + dy) / inPara.dPixelS;

        if( nCamType == Haz_Cam )//避障相机模型
        {
            DOUBLE dr = sqrt( ptRes.X*ptRes.X + ptRes.Y*ptRes.Y );
            DOUBLE dTmpRes = dr/inPara.f;
            ptRes.X = ptRes.X * tan( dTmpRes ) / (dTmpRes);
            ptRes.Y = ptRes.Y * tan( dTmpRes ) / (dTmpRes);
        }
        return true;
    }
    else if( nCamType == Pan_Cam )
    {
        Pt2d ptCur, ptTmpRes;
        ptCur.X = pt.X - inPara.x;
        ptCur.Y = pt.Y - inPara.y;

        if( true == calcPanOrigPt( ptCur, ptTmpRes, inPara ) )
        {
            ptRes.X = ptTmpRes.X + inPara.x;
            ptRes.Y = ptTmpRes.Y + inPara.y;
            return true;
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }
}
bool CmlFrameImage::AddDisCorToPlaneFrame( Pt2d ptxyOrig, UINT nHeight, InOriPara inPara, ExOriPara exPara, CAMTYPE nCamType, Pt2d &ptRes )
{
    Pt2d ptInPers;
//    CmlMat matOPK;
//    OPK2RMat( &exPara.ori, &matOPK );
//
//    getxyFromXYZ( ptInPers, ptXYZ, exPara.pos, matOPK, inPara.f, inPara.f );
    ptInPers = ptxyOrig;
    if( ( nCamType == Nav_Cam )||( nCamType == Haz_Cam ) )
    {
        UINT DisX, DisY;
        Pt2d tempXY ;
        DOUBLE OriX, OriY;
        DOUBLE DisXX,DisYY;
        DOUBLE FS, FL, dSx, dSy, dLx, dLy, dx, dy, r1, r2, r, eq1;
        DOUBLE x0 ,y0 ,k1, k2, k3, p1, p2, alph, bata;

        InOriPara InOri = inPara;
        ExOriPara ExOri = exPara;

        k1 = InOri.k1;
        k2 = InOri.k2;
        k3 = InOri.k3;
        p1 = InOri.p1;
        p2 = InOri.p2;
        alph = InOri.alpha;
        bata = InOri.beta;


        if( nCamType == Haz_Cam )
        {
            //-----------------------------------------
            DOUBLE dDireX = ptInPers.X;
            DOUBLE dDireY = ptInPers.Y;
            DOUBLE dTmpDis = sqrt(dDireX*dDireX + dDireY*dDireY);
            DOUBLE dUnitX = dDireX / dTmpDis;
            DOUBLE dUnitY = dDireY / dTmpDis;

            UINT nLength = UINT(dTmpDis + 0.0001 );
            Pt2d ptCurOrig, ptCurRes;
            ptCurRes.X += 0.5;
            ptCurRes.Y += 0.5;

            if( nLength > 1 )
            {
                for( UINT i = 0; i <= nLength; ++i )
                {
                    ptCurOrig.X = i * dUnitX;
                    ptCurOrig.Y = i * dUnitY;
                    calcHazOrigPt( ptCurRes.X, ptCurRes.Y, ptCurOrig.X, ptCurOrig.Y, InOri.f );
                }
            }

            calcHazOrigPt( ptCurRes.X, ptCurRes.Y, ptInPers.X, ptInPers.Y, InOri.f );
            //----------------------------------------
            ptInPers = ptCurRes;
        }

        DisXX = ptInPers.X * InOri.dPixelS;
        DisYY = ptInPers.Y * InOri.dPixelS;

        OriX = DisXX;
        OriY = DisYY;

        SINT nItera = 0;
        do
        {
            /**所求导数只适用于左下角坐标系！！！！**/
            r1 = OriX;
            r2 = OriY;
            r = r1*r1+r2*r2;
            eq1 = k1 + k2 * r + k3 * r * r;

            FS = r1 * ( r * eq1 + alph ) + bata * r2 + p1*( r + 2.0 * r1 * r1 ) + 2.0 * p2 * r1 * r2 + OriX - DisXX;
            FL = r2 * ( r * eq1 ) + 2.0 * p1 * r1 * r2 + p2 * ( r + 2.0 * r2* r2 ) + OriY - DisYY;

            dSx = r * eq1 + alph + r1 * ( 2.0 * r1 * eq1 + r * ( k2 * 2.0 * r1 + 2.0 * k3 * r * 2.0 * r1 ) ) + p1 * 6.0 * r1 + 2.0 * p2 * r2 + 1.0;
            dSy = r1 * ( 2.0 * r2 * eq1 + r * ( k2 * 2.0 * r2 + 2.0 * k3 * r * 2.0 * r2 ) ) + bata + p1 * 2.0 * r2 + 2.0 * p2 * r1;
            dLx = r2 * 2.0 * r1 * eq1 + r2 * r * ( k2 * 2.0 * r1 + 2.0 * k3 * r * 2.0 * r1 ) + 2.0 * p1 * r2 + p2 * 2.0 * r1;
            dLy = r * eq1 + r2 * 2.0 * r2 * eq1 + r2 * r * ( k2 * 2.0 * r2 + 2.0 * k3 * r * 2.0 * r2 ) + 2.0 * p1 * r1 + p2 * 6.0 * r2 + 1.0;

            dx = ( FS * dLy - FL * dSy ) / ( dSy * dLx - dSx * dLy );
            dy = ( FS * dLx - FL * dSx ) / ( dSx * dLy - dSy * dLx );

            OriX = dx + OriX;
            OriY = dy + OriY;

            nItera++;
            if ( nItera > 100 )
            {
                cout<<"Undistoration is failed!\n";
                break;
            }
        }
        while( ( abs ( dx ) > 0.0001 ) || ( abs ( dy ) > 0.0001 ) );
        OriX /= InOri.dPixelS;
        OriY /= InOri.dPixelS;
        ptRes.X = OriX;// + InOri.x;
        ptRes.Y = OriY ;//+ ( nHeight - 1- InOri.y );/**将计算出的像点坐标转回左上角坐标系**/
//        ptRes.Y = nHeight - 1 - ptRes.Y;

        return true;
    }
    else if( nCamType == Pan_Cam )
    {

    }
    else
    {
        return false;
    }

    return true;
}
/**
* @fn UnDisCorToPlaneFrame
* @date 2011.11.20
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算畸变改正后坐标并转换成像平面坐标系
* @param imgInList 图像点坐标（左上角坐标系）
* @param inPara 内定向参数
* @param imgOutList 像平面坐标x,y
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::UnDisCorToPlaneFrame(vector<Pt2d>& imgInList, InOriPara& inPara, vector<Pt2d>& imgOutList, CAMTYPE nCamType )
{
    return UnDisCorToPlaneFrame( imgInList, inPara, this->GetHeight(), imgOutList, nCamType );
}
bool CmlFrameImage::UnDisCorToPlaneFrame(vector<Pt2d>& imgInList, InOriPara& inPara, UINT nHeight, vector<Pt2d>& imgOutList, CAMTYPE nCamType )
{
    Pt2d imgPt2d, tempXY;

    for(UINT i = 0; i < imgInList.size(); i++)
    {
        /*换算成像主点为中心的焦平面坐标*/
        imgPt2d = imgInList.at(i) ;

        this->UnDisCorToPlaneFrame( imgPt2d, inPara, nHeight, tempXY, nCamType );
        imgOutList.push_back(tempXY);
    }
    return true;
}
/**
* @fn UnDisCorToPicCoord
* @date 2011.11.20
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算畸变改正后坐标.转换成图像坐标系
* @param imgInList 图像点坐标（左上角坐标系）
* @param inPara 内定向参数
* @param imgOutList 畸变矫正后图像点坐标（左上角坐标系）
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::UnDisCorToPicCoord(vector<Pt2d>& imgInList, InOriPara& inPara,vector<Pt2d>& imgOutList, CAMTYPE nCamType )
{
    this->UnDisCorToPlaneFrame( imgInList, inPara, imgOutList, nCamType );
    double x0 = inPara.x;
    double y0 = this->GetHeight() - inPara.y - 1;
    Pt2d imgPt2d;
    for(UINT i = 0; i < imgOutList.size(); i++)
    {
        /*换算成图像坐标*/
        imgPt2d = imgOutList.at(i) ;
        imgPt2d.X = imgPt2d.X + x0;
        imgPt2d.Y = imgPt2d.Y + y0;
        imgPt2d.Y = this->GetHeight() - imgPt2d.Y - 1;
        imgOutList.at(i) = imgPt2d;
        //imgOutList.push_back( imgPt2d );
    }
    return true;
}
bool calcHazOrigPt( DOUBLE &dTmpx, DOUBLE &dTmpy, DOUBLE dOriTmpX, DOUBLE dOriTmpY, DOUBLE df )
{
    UINT nItera = 0;
    DOUBLE dCurCoefx = 0;
    DOUBLE dCurCoefy = 0;
    do
    {
        DOUBLE dr = sqrt(dTmpx*dTmpx+dTmpy*dTmpy);
        DOUBLE dU = dr/df;
        DOUBLE dTanU = tan(dU);

        DOUBLE dFunc_f = dTmpx*dTanU / (dU) - dOriTmpX;
        DOUBLE dFunc_g = dTmpy*dTanU / (dU) - dOriTmpY;

        DOUBLE dDiff_u2x = dTmpx / (df * dr );
        DOUBLE dDiff_u2y = dTmpy / (df * dr );
        DOUBLE dDiff_TanU2x = (1 / (cos(dU)*cos(dU))) * dDiff_u2x;
        DOUBLE dDiff_TanU2y = (1 / (cos(dU)*cos(dU))) * dDiff_u2y;

        DOUBLE dDiff_f2x = (dTanU/dU) + (dTmpx*((dDiff_TanU2x)*dU-dTanU*dDiff_u2x)) / ((dU*dU));
        DOUBLE dDiff_f2y = (dTmpx*((dDiff_TanU2y)*dU-dTanU*dDiff_u2y)) / ((dU*dU));

        DOUBLE dDiff_g2x = (dTmpy*((dDiff_TanU2x)*dU-dTanU*dDiff_u2x)) / ((dU*dU));
        DOUBLE dDiff_g2y = (dTanU/dU) + (dTmpy*((dDiff_TanU2y)*dU-dTanU*dDiff_u2y)) / ((dU*dU));

        DOUBLE dTmpCoef = dDiff_g2x*dDiff_f2y-dDiff_f2x*dDiff_g2y;

        dCurCoefx = ( dFunc_f*dDiff_g2y - dFunc_g*dDiff_f2y ) / dTmpCoef;
        dCurCoefy= ( dFunc_g*dDiff_f2x - dFunc_f*dDiff_g2x ) / dTmpCoef;
        dTmpx += dCurCoefx;
        dTmpy += dCurCoefy;

    }while(( ( fabs( dCurCoefx) > 0.01  )||( fabs( dCurCoefy) > 0.01 ) )&&(nItera++ < 100 ));


    return true;
}
/**
* @fn mlGetDistortionCoordinate
* @date 2011.11.20
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算畸变改正后点坐标
* @param pDisImg 原始畸变影像
* @param pCoordinate 畸变矫正后坐标点
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::mlGetDistortionCoordinate( CmlRasterBlock *pDisImg , CRasterPt2D * pCoordinate, CAMTYPE nCamType, DOUBLE dZoomCoef )
{
    if( ( false == pDisImg->IsValid() ) )
    {
        return false;
    }
    if( ( nCamType == Nav_Cam )||( nCamType == Haz_Cam ) )
    {
        UINT DisX, DisY;
        Pt2d tempXY ;
        DOUBLE OriX, OriY;
        DOUBLE DisXX,DisYY;
        DOUBLE FS, FL, dSx, dSy, dLx, dLy, dx, dy, r1, r2, r, eq1;
        DOUBLE x0 ,y0 ,k1, k2, k3, p1, p2, alph, bata;

        InOriPara InOri = ((CmlFrameImage*)pDisImg->m_pGdalData)->m_InOriPara;
        ExOriPara ExOri = ((CmlFrameImage*)pDisImg->m_pGdalData)->m_ExOriPara;

        x0 = InOri.x ;
        y0 = (pDisImg->GetH()-1-InOri.y);/**将像主点转成左下角坐标系进行计算**/
        k1 = InOri.k1;
        k2 = InOri.k2;
        k3 = InOri.k3;
        p1 = InOri.p1;
        p2 = InOri.p2;
        alph = InOri.alpha;
        bata = InOri.beta;

        CRasterPt2D tmpRaster2D;
        tmpRaster2D.Initial( pDisImg->GetH(), pDisImg->GetW() );
        for( DisX = 0; DisX < pDisImg->GetW(); DisX++ )
        {
            for( DisY = 0; DisY < pDisImg->GetH(); DisY++ )
            {
                Pt2d ptCur;
                ptCur.X = DisX;
                ptCur.Y = DisY;
                tmpRaster2D.SetAt( DisY, DisX, ptCur );
            }
        }
        DOUBLE df = InOri.f;
        CmlRasterBlock imgTmpBlock;
        if( nCamType == Haz_Cam )
        {
            //-----------------------------------------
            UINT nNewH = UINT( pDisImg->GetH()*dZoomCoef + 0.0001 );
            UINT nNewW = UINT( pDisImg->GetW()*dZoomCoef + 0.0001 );
            UINT nOrigBytes = pDisImg->GetBytes();
            DOUBLE dYOff = ( nNewH - pDisImg->GetH() ) / 2.0;
            DOUBLE dXOff = ( nNewW - pDisImg->GetW() ) / 2.0;
            imgTmpBlock.InitialImg( nNewH, nNewW, nOrigBytes );

            tmpRaster2D.DestoryAll();
            tmpRaster2D.Initial( nNewH, nNewW );
            for( DisX = 0; DisX < nNewW; DisX++ )
            {
                for( DisY = 0; DisY < nNewH; DisY++ )
                {
                    Pt2d ptCur;
                    ptCur.X = DisX - dXOff;
                    ptCur.Y = DisY - dYOff;
                    tmpRaster2D.SetAt( DisY, DisX, ptCur );
                }
            }
    //        x0 += dXOff;
    //        y0 = (pDisImg->GetH()-1-InOri.y+dXOff);

            if( pCoordinate->IsValid() == false )
            {
                pCoordinate->Initial( nNewH, nNewW );
            }

            //----------------------------------------
    //        for( UINT i = 0; i < nNewH; ++i )
    //        {
    //            for( UINT j = 0; j < nNewW; ++j )
    //            {
    //                Pt2d ptCur = tmpRaster2D.GetAt( i, j );
    //
    //                DOUBLE dOriTmpX, dOriTmpY;
    //                dOriTmpX = ptCur.X - x0 ;
    //                dOriTmpY = pDisImg->GetH() - 1 - ptCur.Y - y0;
    //
    //                DOUBLE dTmpx, dTmpy;
    //                dTmpx = dOriTmpX;
    //                dTmpy = dOriTmpY;
    //
    //                calcHazOrigPt( dTmpx, dTmpy, dOriTmpX, dOriTmpY, df );
    //
    //                dTmpx += x0;
    //                dTmpy += y0;
    //
    //                ptCur.X = dTmpx;
    //                ptCur.Y = pDisImg->GetH() - 1 - dTmpy;
    //                tmpRaster2D.SetAt( i, j, ptCur );
    //
    //            }
    //        }


            //----------------------------------------
            Pt2i ptCent, ptLT, ptLB, ptRT, ptRB;
            ptCent.X = nNewW / 2;
            ptCent.Y = nNewH / 2;
            ptLT = ptRB = ptCent;
            MLRect rect;
            rect.dXMax = nNewW - 0.5;
            rect.dXMin = -0.5;
            rect.dYMax = nNewH - 0.5;
            rect.dYMin = -0.5;
            while( true )
            {
                //左边条带
                for( UINT i = ptLT.Y; i <= ptRB.Y; ++i )
                {
                    Pt2d ptCur = tmpRaster2D.GetAt( i, ptLT.X );

                    DOUBLE dOriTmpX, dOriTmpY;
                    dOriTmpX = ptCur.X ;
                    dOriTmpY = pDisImg->GetH() - 1 - ptCur.Y ;

                    DOUBLE dTmpx, dTmpy;

                    dTmpx = dOriTmpX - x0;
                    dTmpy = dOriTmpY - y0;
                    dOriTmpX = dTmpx;
                    dOriTmpY = dTmpy;

                    Pt2d ptCurTmp = tmpRaster2D.GetAt( i, ptLT.X+1 );
                    dTmpx = ptCurTmp.X - x0;
                    dTmpy = pDisImg->GetH() - 1 - ptCurTmp.Y - y0 ;

                    if( i == ptLT.Y )
                    {
                        ptCurTmp = tmpRaster2D.GetAt( i+1, ptLT.X+1 );
                        dTmpx = ptCurTmp.X - x0;
                        dTmpy = pDisImg->GetH() - 1 - ptCurTmp.Y - y0 ;
                    }
                    if( i == ptRB.Y )
                    {
                        ptCurTmp = tmpRaster2D.GetAt( i-1, ptLT.X+1 );
                        dTmpx = ptCurTmp.X - x0;
                        dTmpy = pDisImg->GetH() - 1 - ptCurTmp.Y - y0 ;
                    }

                    calcHazOrigPt( dTmpx, dTmpy, dOriTmpX, dOriTmpY, df );

                    dTmpx += x0;
                    dTmpy += y0;

                    ptCur.X = dTmpx;
                    ptCur.Y = pDisImg->GetH() - 1 - dTmpy;
                    tmpRaster2D.SetAt( i, ptLT.X, ptCur );
                }
                for( UINT i = ptLT.Y; i <= ptRB.Y; ++i )
                {
                    Pt2d ptCur = tmpRaster2D.GetAt( i, ptRB.X );
                    DOUBLE dOriTmpX, dOriTmpY;
                    dOriTmpX = ptCur.X ;
                    dOriTmpY = pDisImg->GetH() - 1 - ptCur.Y ;

                    DOUBLE dTmpx, dTmpy;

                    dTmpx = dOriTmpX - x0;
                    dTmpy = dOriTmpY - y0;
                    dOriTmpX = dTmpx;
                    dOriTmpY = dTmpy;

                    Pt2d ptCurTmp = tmpRaster2D.GetAt( i, ptRB.X-1 );
                    dTmpx = ptCurTmp.X - x0;
                    dTmpy = pDisImg->GetH() - 1 - ptCurTmp.Y - y0 ;

                    if( i == ptLT.Y )
                    {
                        ptCurTmp = tmpRaster2D.GetAt( i+1, ptRB.X-1 );
                        dTmpx = ptCurTmp.X - x0;
                        dTmpy = pDisImg->GetH() - 1 - ptCurTmp.Y - y0 ;
                    }
                    if( i == ptRB.Y )
                    {
                        ptCurTmp = tmpRaster2D.GetAt( i-1, ptRB.X-1 );
                        dTmpx = ptCurTmp.X - x0;
                        dTmpy = pDisImg->GetH() - 1 - ptCurTmp.Y - y0 ;
                    }

                    calcHazOrigPt( dTmpx, dTmpy, dOriTmpX, dOriTmpY, df );

                    dTmpx += x0;
                    dTmpy += y0;


                    ptCur.X = dTmpx;
                    ptCur.Y = pDisImg->GetH() - 1 - dTmpy;
                    tmpRaster2D.SetAt(  i, ptRB.X, ptCur );
                }
                //上条带
                for( UINT i = (ptLT.X+1); i <= (ptRB.X-1); ++i )
                {
                    Pt2d ptCur = tmpRaster2D.GetAt( ptLT.Y, i );
                    DOUBLE dOriTmpX, dOriTmpY;
                    dOriTmpX = ptCur.X ;
                    dOriTmpY = pDisImg->GetH() - 1 - ptCur.Y ;

                    DOUBLE dTmpx, dTmpy;

                    dTmpx = dOriTmpX - x0;
                    dTmpy = dOriTmpY - y0;
                    dOriTmpX = dTmpx;
                    dOriTmpY = dTmpy;

                    Pt2d ptCurTmp = tmpRaster2D.GetAt( ptLT.Y + 1, i );
                    dTmpx = ptCurTmp.X - x0;
                    dTmpy = pDisImg->GetH() - 1 - ptCurTmp.Y - y0 ;

                    calcHazOrigPt( dTmpx, dTmpy, dOriTmpX, dOriTmpY, df );

                    dTmpx += x0;
                    dTmpy += y0;

                    ptCur.X = dTmpx;
                    ptCur.Y = pDisImg->GetH() - 1 - dTmpy;
                    tmpRaster2D.SetAt( ptLT.Y, i, ptCur );
                }
                //下条带
                for( UINT i = (ptLT.X+1); i <= (ptRB.X-1); ++i )
                {
                    Pt2d ptCur = tmpRaster2D.GetAt( ptRB.Y, i );
                    DOUBLE dOriTmpX, dOriTmpY;
                    dOriTmpX = ptCur.X ;
                    dOriTmpY = pDisImg->GetH() - 1 - ptCur.Y ;

                    DOUBLE dTmpx, dTmpy;

                    dTmpx = dOriTmpX - x0;
                    dTmpy = dOriTmpY - y0;
                    dOriTmpX = dTmpx;
                    dOriTmpY = dTmpy;

                    Pt2d ptCurTmp = tmpRaster2D.GetAt( ptRB.Y - 1, i );
                    dTmpx = ptCurTmp.X - x0;
                    dTmpy = pDisImg->GetH() - 1 - ptCurTmp.Y - y0 ;

                    calcHazOrigPt( dTmpx, dTmpy, dOriTmpX, dOriTmpY, df );

                    dTmpx += x0;
                    dTmpy += y0;

                    ptCur.X = dTmpx;
                    ptCur.Y = pDisImg->GetH() - 1 - dTmpy;
                    tmpRaster2D.SetAt( ptRB.Y, i, ptCur );
                }
                ptLT.X -= 1;
                ptLT.Y -= 1;
                ptRB.X += 1;
                ptRB.Y += 1;
                if( (ptLT.X < 0 )&&(ptLT.Y < 0 )&&(ptRB.X >=nNewW)&&(ptRB.Y >= nNewH) )
                {
                    break;
                }
                if( ptLT.X < 0 )
                {
                    ptLT.X = 0;
                }
                if( ptLT.Y < 0 )
                {
                    ptLT.Y = 0;
                }
                if( ptRB.X >= nNewW )
                {
                    ptRB.X = nNewW - 1;
                }
                if( ptRB.Y >= nNewH )
                {
                    ptRB.Y = nNewH- 1;
                }
    //            cout << ptLT.X << "  " << ptLT.Y << "  " << ptRB.X << "  " << ptRB.Y << "\n";
            }
        }
        if( pCoordinate->IsValid() == false )
        {
            pCoordinate->Initial( pDisImg->GetH(), pDisImg->GetW() );
        }

        x0 = x0 * InOri.dPixelS;
        y0 = y0 * InOri.dPixelS;/**将像主点转成左下角坐标系进行计算**/
        for(DisX = 0; DisX < tmpRaster2D.GetW(); DisX++)
        {
            for(DisY = 0; DisY < tmpRaster2D.GetH(); DisY++)
            {
                Pt2d ptCur = tmpRaster2D.GetAt( DisY, DisX );

                DisXX = ptCur.X * InOri.dPixelS;
                DisYY = ( pDisImg->GetH() - 1 - ptCur.Y )* InOri.dPixelS;

                OriX = DisXX;
                OriY = DisYY;

                SINT nItera = 0;
                do
                {
                    /**所求导数只适用于左下角坐标系！！！！**/
                    r1 = OriX - x0;
                    r2 = OriY - y0;
                    r = r1*r1+r2*r2;
                    eq1 = k1 + k2 * r + k3 * r * r;

                    FS = r1 * ( r * eq1 + alph ) + bata * r2 + p1*( r + 2.0 * r1 * r1 ) + 2.0 * p2 * r1 * r2 + OriX - DisXX;
                    FL = r2 * ( r * eq1 ) + 2.0 * p1 * r1 * r2 + p2 * ( r + 2.0 * r2* r2 ) + OriY - DisYY;

                    dSx = r * eq1 + alph + r1 * ( 2.0 * r1 * eq1 + r * ( k2 * 2.0 * r1 + 2.0 * k3 * r * 2.0 * r1 ) ) + p1 * 6.0 * r1 + 2.0 * p2 * r2 + 1.0;
                    dSy = r1 * ( 2.0 * r2 * eq1 + r * ( k2 * 2.0 * r2 + 2.0 * k3 * r * 2.0 * r2 ) ) + bata + p1 * 2.0 * r2 + 2.0 * p2 * r1;
                    dLx = r2 * 2.0 * r1 * eq1 + r2 * r * ( k2 * 2.0 * r1 + 2.0 * k3 * r * 2.0 * r1 ) + 2.0 * p1 * r2 + p2 * 2.0 * r1;
                    dLy = r * eq1 + r2 * 2.0 * r2 * eq1 + r2 * r * ( k2 * 2.0 * r2 + 2.0 * k3 * r * 2.0 * r2 ) + 2.0 * p1 * r1 + p2 * 6.0 * r2 + 1.0;

                    dx = ( FS * dLy - FL * dSy ) / ( dSy * dLx - dSx * dLy );
                    dy = ( FS * dLx - FL * dSx ) / ( dSx * dLy - dSy * dLx );

                    OriX = dx + OriX;
                    OriY = dy + OriY;

                    nItera++;
                    if ( nItera > 100 )
                    {
                        cout<<"Undistoration is failed!\n";
                        break;
                    }
                }
                while( ( abs ( dx ) > 0.0001 ) || ( abs ( dy ) > 0.0001 ) );
                OriX /= InOri.dPixelS;
                OriY /= InOri.dPixelS;
                tempXY.X = OriX;
                tempXY.Y = pDisImg->GetH()-1-OriY;/**将计算出的像点坐标转回左上角坐标系**/
                pCoordinate->SetAt( DisY, DisX, tempXY );/**按照左上角坐标系存储**/

            }
        }
        return true;
    }
    else if( nCamType == Pan_Cam )
    {
        UINT DisX, DisY;
        Pt2d tempXY ;
        DOUBLE OriX, OriY;
        DOUBLE DisXX,DisYY;
        DOUBLE FS, FL, dSx, dSy, dLx, dLy, dx, dy, r1, r2, r, eq1;
        DOUBLE x0 ,y0 ,k1, k2, k3, p1, p2, alph, bata, skew;

        InOriPara InOri = ((CmlFrameImage*)pDisImg->m_pGdalData)->m_InOriPara;
        ExOriPara ExOri = ((CmlFrameImage*)pDisImg->m_pGdalData)->m_ExOriPara;

        x0 = InOri.x ;
        y0 = InOri.y ;
        k1 = InOri.k1;
        k2 = InOri.k2;
        k3 = InOri.k3;
        p1 = InOri.p1;
        p2 = InOri.p2;
        alph = InOri.alpha;
        bata = InOri.beta;
        skew = InOri.dSkew;

        CRasterPt2D tmpRaster2D;
        tmpRaster2D.Initial( pDisImg->GetH(), pDisImg->GetW() );
        for( DisX = 0; DisX < pDisImg->GetW(); DisX++ )
        {
            for( DisY = 0; DisY < pDisImg->GetH(); DisY++ )
            {
                Pt2d ptCur;
                ptCur.X = DisX;
                ptCur.Y = DisY;
                tmpRaster2D.SetAt( DisY, DisX, ptCur );
            }
        }
        DOUBLE df = InOri.f;
        DOUBLE df2 = InOri.f2;
        CmlRasterBlock imgTmpBlock;

        if( pCoordinate->IsValid() == false )
        {
            pCoordinate->Initial( pDisImg->GetH(), pDisImg->GetW() );
        }

        for(DisX = 0; DisX < tmpRaster2D.GetW(); DisX++)
        {
            for(DisY = 0; DisY < tmpRaster2D.GetH(); DisY++)
            {
                Pt2d ptCur = tmpRaster2D.GetAt( DisY, DisX );

                DOUBLE dTempX = (ptCur.X - x0)/ df;
                DOUBLE dTempY = (ptCur.Y - y0)/ df;

                DOUBLE dR2 = dTempX * dTempX + dTempY * dTempY;
                DOUBLE dR4 = dR2 * dR2;
                DOUBLE dR6 = dR2 * dR4;

                DOUBLE dXd = ( (1 + k1*dR2 + k2*dR4 + k3*dR6) * dTempX + 2 * p1 * dTempX * dTempY + p2 * ( dR2 + 2 * dTempX*dTempX ) );
                DOUBLE dYd = ( (1 + k1*dR2 + k2*dR4 + k3*dR6) * dTempY + p1 * ( dR2 + 2 * dTempY*dTempY ) + 2 * p2 * dTempX * dTempY );

                tempXY.X = df*( dXd + skew * dYd ) + x0;
                tempXY.Y = df2 * dYd + y0;

                pCoordinate->SetAt( DisY, DisX, tempXY );/**按照左上角坐标系存储**/

            }
        }
        return true;
    }
    else
    {
        return false;
    }



}

/**
* @fn mlGrayInterpolation
* @date 2011.11.20
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 图像灰度内插
* @param pImg 原始影像
* @param pCoordinate 内插前非整形坐标点
* @param pDisImg 内插后影像
* @param nOptions 灰度内插类型
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::mlGrayInterpolation( CmlRasterBlock *pImg , CRasterPt2D *pCoordinate, CmlRasterBlock *pDisImg, int nOptions )
{
    if( !pImg->IsValid() )
    {
        return false;
    }
    bool H = false;
    if(0 == nOptions)
    {
        H = mlBilinearInterpolation(pImg,pCoordinate,pDisImg);
    }
    else
    {
        /**其他灰度内插方法接口**/
    }
    return H;
}

/**
* @fn mlGetBilinearValue
* @date 2012.01.08
* @author 吴凯 wukai@irsa.ac.cn
* @brief 进行影像灰度值的双线性内插.（左上角坐标系）
* @param pImg 原始影像
* @param tempXY 像点坐标
* @version 1.0
* @return 灰度内插后对应的影像灰度值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
BYTE CmlFrameImage::mlGetBilinearValue(CmlRasterBlock *pImg , Pt2d tempXY)
{
    if( !pImg->IsValid() )
    {
        return false;
    }
    BYTE b_Value = 255 ;
    DOUBLE LeftX , RightX , BottomY , TopY , dx ,dy , dGrayVal ;
    int nHeight = pImg->GetH();
    int nWidth = pImg->GetW();
    if( ( tempXY.X <= -0.5 )|| ( tempXY.X >= nWidth-0.5 ) || ( tempXY.Y <= -0.5 ) || ( tempXY.Y >= nHeight-0.5 ) )
    {
        /**若图像坐标超出图像范围，则该点值为零**/
        b_Value = 255 ;
    }
    else
    {
        /**当图像X坐标位于边缘0.5个像素内是，该像素赋值为边缘像素的值**/
        if(tempXY.X < 0)
        {
            tempXY.X = 0;
        }
        else if (tempXY.X > nWidth -1)
        {
            tempXY.X = nWidth - 1;
        }
        /**当图像Y坐标位于边缘0.5个像素内是，该像素赋值为边缘像素的值**/
        if(tempXY.Y < 0)
        {
            tempXY.Y = 0;
        }
        else if (tempXY.Y > nHeight - 1)
        {
            tempXY.Y = nHeight - 1;
        }
        LeftX = floor( tempXY.X + ML_ZERO);
        RightX = ceil( tempXY.X - ML_ZERO);
        BottomY = ceil(tempXY.Y - ML_ZERO);
        TopY = floor(tempXY.Y + ML_ZERO );
        dx = tempXY.X - LeftX;
        dy = BottomY - tempXY.Y ;
//
        if( LeftX < 0 )
        {
            LeftX = 0;
        }
        if( RightX >= SINT( nWidth ) )
        {
            RightX = nWidth - 1;
        }
        if( BottomY >= SINT( nHeight ) )
        {
            BottomY = nHeight - 1;
        }
        if( TopY < 0 )
        {
            TopY = 0;
        }

        dGrayVal = ( 1 - dx ) * ( 1 - dy ) * pImg->GetAt( BottomY, LeftX ) + dx * ( 1 - dy ) * pImg->GetAt( BottomY, RightX ) +
                   dx * dy * pImg->GetAt( TopY, RightX ) + ( 1 - dx ) * dy * pImg->GetAt( TopY, LeftX );
        b_Value = BYTE( dGrayVal + 0.5 );

        if( (dGrayVal + 0.5 ) >= 255 )
        {
            b_Value = 255;
        }
    }
    return b_Value ;
}
/**
* @fn mlGetNearValue
* @date 2012.01.08
* @author 吴凯 wukai@irsa.ac.cn
* @brief 计算某点在影像上的最临近像素点的值
* @param pImg 原始影像
* @param tempXY 像点坐标
* @version 1.0
* @return 最临近像素点的值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
BYTE CmlFrameImage::mlGetNearValue(CmlRasterBlock *pImg , Pt2d tempXY)
{
    if( !pImg->IsValid() )
    {
        return false;
    }
    BYTE b_Value = 255 ;
    int nHeight = pImg->GetH();
    int nWidth = pImg->GetW();
    if( (tempXY.X <= -0.5 )|| ( tempXY.X >= nWidth-0.5 ) || ( tempXY.Y <= -0.5 ) || ( tempXY.Y >= nHeight-0.5 ) )
    {
        /**若图像坐标超出图像范围，则该点值为零**/
        return  b_Value;
    }
    else
    {
        DOUBLE dGrayVal ;
        SINT nX , nY ;
        nX = (SINT)(tempXY.X) ;
        nY = (SINT)(tempXY.Y) ;
        pImg->GetDoubleVal(nY , nX , dGrayVal) ;
        if((dGrayVal>=0) && (dGrayVal <= 255))
        {
            b_Value = (BYTE)dGrayVal ;
        }
        return b_Value ;
    }
}
/**
* @fn mlBilinearInterpolation
* @date 2011.11.20
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 双线性内插
* @param pImg 原始影像
* @param pCoordinate 内插前非整形坐标点
* @param pDisImg 内插后影像
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
bool CmlFrameImage::mlBilinearInterpolation( CmlRasterBlock *pImg , CRasterPt2D *pCoordinate, CmlRasterBlock *pDisImg )
{
    if( ( pImg == NULL )||( pCoordinate == NULL )||( pDisImg == NULL ))
    {
        return false;
    }
    if( ( false == pImg->IsValid() )||( false == pCoordinate->IsValid() ) )
    {
        return false;
    }
    if( pDisImg->IsValid() == false )
    {
        pDisImg->InitialImg( pCoordinate->GetH(), pCoordinate->GetW(), pImg->GetBytes() );
    }

    UINT nRow, nCol;
    SINT LeftX, RightX, TopY, BottomY ;
    double dx, dy, dGrayVal;
    Pt2d tempXY;
    UINT nHeight = pImg->GetH();
    UINT nWidth = pImg->GetW();
//    if( ( pCoordinate->GetH() != nHeight )||( pCoordinate->GetW() != nWidth ) ||( pDisImg->GetH() != nHeight )||( pDisImg->GetW() != nWidth ) )
//    {
//        return false;
//    }

    for(nRow = 0; nRow < pCoordinate->GetH(); nRow++)
    {
        for(nCol = 0; nCol < pCoordinate->GetW(); nCol++)
        {
            tempXY = pCoordinate->GetAt( nRow, nCol );
            if( ( tempXY.X <= -0.5 )|| ( tempXY.X >= nWidth-0.5 ) || ( tempXY.Y <= -0.5 ) || ( tempXY.Y >= nHeight-0.5 ) )
            {
                /**若图像坐标超出图像范围，则该点值为零**/
                pDisImg->SetAt( nRow, nCol, 255 );
            }
            else
            {
                /**当图像X坐标位于边缘0.5个像素内是，该像素赋值为边缘像素的值**/
                if(tempXY.X < 0)
                {
                    tempXY.X = 0;
                }
                else if (tempXY.X > nWidth -1)
                {
                    tempXY.X = nWidth - 1;
                }
                /**当图像Y坐标位于边缘0.5个像素内是，该像素赋值为边缘像素的值**/
                if(tempXY.Y < 0)
                {
                    tempXY.Y = 0;
                }
                else if (tempXY.Y > nHeight - 1)
                {
                    tempXY.Y = nHeight - 1;
                }


                LeftX = floor( tempXY.X + ML_ZERO );
                RightX = ceil( tempXY.X - ML_ZERO);
                BottomY = ceil(tempXY.Y - ML_ZERO);
                TopY = floor(tempXY.Y + ML_ZERO );
                dx = tempXY.X - LeftX;
                dy = BottomY - tempXY.Y ;

                if( LeftX < 0 )
                {
                    LeftX = 0;
                }
                if( RightX >= SINT( nWidth ) )
                {
                    RightX = nWidth - 1;
                }
                if( BottomY >= SINT( nHeight ) )
                {
                    BottomY = nHeight - 1;
                }
                if( TopY < 0 )
                {
                    TopY = 0;
                }

                dGrayVal = ( 1 - dx ) * ( 1 - dy ) * pImg->GetAt( BottomY, LeftX ) + dx * ( 1 - dy ) * pImg->GetAt( BottomY, RightX ) +
                           dx * dy * pImg->GetAt( TopY, RightX ) + ( 1 - dx ) * dy * pImg->GetAt( TopY, LeftX );
                BYTE bCurVal = BYTE( dGrayVal + 0.5 );
                if( (dGrayVal + 0.5 ) >= 255 )
                {
                    bCurVal = 255;
                }

                pDisImg->SetAt(nRow, nCol, dGrayVal);
            }
        }
    }

    return true;
}

/**
* @fn FindPtinVecPts
* @date 2012.02
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 整形点查找函数，mlCleanDeadPixel中调用
* @param pt2i 待查找的点
* @param vecpt2i 查找集合
* @param index 点索引，若查找失败返回容器的容量大小
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/

bool  FindPtinVecPts(Pt2i pt2i, vector<Pt2i> vecpt2i, int& index)
{
    for(UINT i=0; i<vecpt2i.size(); i++)
    {
        if(pt2i.X == vecpt2i[i].X && pt2i.Y == vecpt2i[i].Y)
        {
            index = i;
            return true;
        }

    }
    index = vecpt2i.size();
    return false;
}

/**
* @fn comparept2i
* @date 2012.02
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief comparept2i 定义Pt2i类型比较函数，以便使用STL中的排序函数
* @param a Pt2i对象
* @param b Pt2i对象
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/

bool comparept2i(const Pt2i a,const Pt2i b)
{
    if(a.X != b.X)
    {
        return a.X < b.X;
    }
    else
        return a.Y < b.Y;
}

/**
* @fn mlCleanDeadPixel
* @date 2012.02
* @author 张重阳 zhangchy@irsa.ac.cn
* @brief 线阵影像坏点去除
* @param strImgPathIn 输入图像路径
* @param strImgPathOut 输出图像路径
* @param vecDeadPix 坏点位置
* @version 1.0
* @retval TRUE 成功
* @retval FALSE 失败
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/

bool CmlFrameImage::mlCleanDeadPix( const char* strImgPathIn, const char* strImgPathOut, vector<Pt2i> vecDeadPix )
{
    CmlGdalDataset clsGDataSetIn,clsGDataSetOut;
    if( false == clsGDataSetIn.LoadFile( strImgPathIn ))            //载入坏点图像
    {
        SCHAR strMsg[] = "Fail to Load Image!\n";
        LOGAddErrorMsg(strMsg);
        return false;
    }

    UINT nW = clsGDataSetIn.GetWidth();    //获取图像宽
    UINT nH = clsGDataSetIn.GetHeight();    //获取图像高
    UINT nBands = clsGDataSetIn.GetBands();  //获取图像波段数
    clsGDataSetOut.CreateFile( strImgPathOut, nW, nH, nBands, clsGDataSetIn.GetGDTType(), "GTIFF" );
    DOUBLE dtop,dbottom,dleft,dright,dcenter;
    SINT row,col;
    std::sort(vecDeadPix.begin(),vecDeadPix.end(),comparept2i);     //对输入的坏点序列排序

    for( UINT i = 0; i < clsGDataSetIn.GetBands(); ++i )
    {
        CmlRasterBlock clsBlock;
        if( false == clsGDataSetIn.GetRasterOriginBlock( (i+1), (UINT)0, (UINT)0, nW, nH, (UINT)1, &clsBlock ) )       //获取数据块
        {
            continue;
        }
        ///////////////////////////////////////////
        for(UINT j=0; j<vecDeadPix.size(); j++)
        {
            row = vecDeadPix[j].Y;
            col = vecDeadPix[j].X;
            Pt2i pt_top;           //坏点上、下、左、右四邻域像素
            Pt2i pt_bottom;
            Pt2i pt_left;
            Pt2i pt_right;

            pt_top.X = col;
            pt_top.Y = row-1;
            pt_bottom.X = col;
            pt_bottom.Y = row+1;
            pt_left.X = col-1;
            pt_left.Y = row;
            pt_right.X = col+1;
            pt_right.Y = row;
            int index = 0;

            double weightSum = 4;

            if(FindPtinVecPts(pt_top,vecDeadPix,index)  && vecDeadPix[index].byType == 0)   //邻域点是坏点且未赋值
            {
                dtop = 0;
                weightSum--;

            }
            else
            {
                clsBlock.GetDoubleVal(row-1,col,dtop);

            }

            if(FindPtinVecPts(pt_bottom,vecDeadPix,index)  && vecDeadPix[index].byType == 0)   //邻域点是坏点且未赋值
            {
                dbottom = 0;
                weightSum--;
            }
            else
            {
                clsBlock.GetDoubleVal(row+1,col,dbottom);
            }

            if(FindPtinVecPts(pt_left,vecDeadPix,index)  && vecDeadPix[index].byType == 0)   //邻域点是坏点且未赋值
            {
                dleft = 0;
                weightSum --;
            }
            else
            {
                clsBlock.GetDoubleVal(row,col-1,dleft);
            }

            if(FindPtinVecPts(pt_right,vecDeadPix,index)  && vecDeadPix[index].byType == 0)   //邻域点是坏点且未赋值
            {
                dright = 0;
                weightSum --;
            }
            else
            {
                clsBlock.GetDoubleVal(row,col+1,dright);

            }


            dcenter = 1.0/weightSum * (dtop+dbottom+dleft+dright);
            clsBlock.SetDoubleVal(row,col,dcenter);
            vecDeadPix[j].byType = 1;         //坏点赋值后，标记byType为1

        }

        ///////////////////////////////////////////


        if( false == clsGDataSetOut.SaveBlockToFile( (i+1), 0, 0, &clsBlock ) )                //写图像输出
        {
            SCHAR strMsg[] = "Fail to Save Image!\n";
            LOGAddErrorMsg(strMsg);
            return false;
        }
    }

    LOGAddSuccessQuitMsg();
    return true;
}

bool CmlFrameImage::ExtractFeatPtByForstner(CmlRasterBlock &InputImg, vector<Pt2i> &vecFeaPts, SINT nGridSize,  SINT nPtNum , DOUBLE dThresCoef, bool bIsRemoveAbPixel )
{
    if( vecFeaPts.size() != 0 )
    {
        vecFeaPts.clear();
    }
    if( nGridSize <= 0 )
    {
        nGridSize = 1;
    }

    IplImage* pImg = NULL;
    if( false == CmlRBlock2IplImg( &InputImg, pImg  ) )
    {
        return false;
    }

    IplImage* pSTempImg = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_8U, 1 );
    cvSmooth( pImg, pSTempImg, CV_GAUSSIAN, 3, 3, 0.8 );
    IplImage* pTempDoubleImg = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    DOUBLE dScale = 1.0 / 255.0;
    cvConvertScale( pSTempImg, pTempDoubleImg, dScale, 0 );
    cvReleaseImage( &pSTempImg );

    DOUBLE dKX[3] = { 0.5, 0, -0.5 };
    CvMat KernalX = cvMat( 1, 3, CV_64F, dKX );
    DOUBLE dKY[3] = { 0.5, 0, -0.5 };
    CvMat KernalY = cvMat( 3, 1, CV_64F, dKY );

    IplImage* pImageX = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    IplImage* pImageY = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );

    cvFilter2D( pTempDoubleImg, pImageX, &KernalX, cvPoint( 1,0) );
    cvFilter2D( pTempDoubleImg, pImageY, &KernalY, cvPoint( 0,1) );
    cvReleaseImage(&pTempDoubleImg);

    SINT nRange = 9;
    SINT nThresh = 1.5;


    IplImage* pImageX2 = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvMul( pImageX, pImageX, pImageX2 );
    cvSmooth( pImageX2, pImageX2, CV_GAUSSIAN, nRange,nRange,nThresh );

    IplImage* pImageY2 = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvMul( pImageY, pImageY, pImageY2 );
    cvSmooth( pImageY2, pImageY2, CV_GAUSSIAN, nRange,nRange,nThresh );

    IplImage* pImageXY = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvMul( pImageX, pImageY, pImageXY );
    cvSmooth( pImageXY, pImageXY, CV_GAUSSIAN, nRange,nRange,nThresh );
    cvReleaseImage( &pImageX );
    cvReleaseImage( &pImageY );

    IplImage* pImageX2Y2 = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvMul( pImageX2, pImageY2, pImageX2Y2 );

    IplImage* pImageXY2 = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvAdd( pImageX2, pImageY2, pImageXY2 );
    cvReleaseImage( &pImageX2 );
    cvReleaseImage( &pImageY2 );

    IplImage* pImageXY22 = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvMul( pImageXY, pImageXY, pImageXY22 );
    cvReleaseImage( &pImageXY );


    IplImage* pImageTTemp = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvSub(pImageX2Y2, pImageXY22, pImageTTemp );
    cvReleaseImage( &pImageX2Y2 );
    cvReleaseImage( &pImageXY22 );

    IplImage* pImageResponse = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvDiv(pImageTTemp, pImageXY2, pImageResponse );
    cvSmooth( pImageResponse, pImageResponse, CV_GAUSSIAN, nRange,nRange,2);

    IplImage* pImageResGradX = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    IplImage* pImageResGradY = cvCreateImage( cvGetSize( pImg ), IPL_DEPTH_64F, 1 );
    cvFilter2D( pImageResponse, pImageResGradX, &KernalX, cvPoint(1,0) );
    cvFilter2D( pImageResponse, pImageResGradY, &KernalY, cvPoint(0,1) );

    DOUBLE dResThreshold = cvMean( pImageResponse );
    dResThreshold *= dThresCoef;

    vector<SINT> vecTempPts;
    for (  SINT j = 0; j < pImg->width - 1; ++j 	 )
    {
        for (SINT i = 0 ; i < pImg->height -1 ; ++i)
        {
            DOUBLE* pdXData = (DOUBLE*)(pImageResGradX->imageData + pImageResGradX->widthStep*i);
            DOUBLE* pdYData = (DOUBLE*)(pImageResGradY->imageData + pImageResGradY->widthStep*i);
            SINT nWS = pImageResGradY->widthStep;
            if ( ( pdXData[j] < 0 )&&
                    ( (pdXData)[j+1] > 0 )&&
                    ( pdYData[j] < 0 )&&
                    ( (pdYData+nWS/8)[j] > 0 )&&
                    ( (( DOUBLE*)(pImageResponse->imageData + pImageResponse->widthStep*i))[j] > dResThreshold ) )
            {
                vecTempPts.push_back( j );
                vecTempPts.push_back( i );
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////
    SINT nGridW, nGridH;
    if ( pImg->width % nGridSize == 0 )
    {
        nGridW = pImg->width / nGridSize;
    }
    else
        nGridW = pImg->width / nGridSize + 1;
    if ( pImg->height % nGridSize == 0 )
    {
        nGridH = pImg->height / nGridSize;
    }
    else
        nGridH = pImg->height / nGridSize + 1;
    SLONG nTempGSize = nGridH*nGridW;
    SINT* pGrid = new SINT[nTempGSize*2];
    DOUBLE* pValGrid = new DOUBLE[nTempGSize];

    for ( SINT i = 0; i < nTempGSize; ++i )
    {
        *(pGrid+2*i) = -1;
        *(pGrid+2*i+1) = -1;
        *(pValGrid+i) = -9999.0;
    }

    for ( UINT i = 0; i < vecTempPts.size(); i = i+2 )
    {
        SINT nx = vecTempPts[i];
        SINT ny = vecTempPts[i+1];

        SINT nNoW = nx / nGridSize;
        SINT nNoH = ny / nGridSize;
        SINT nNo = nNoH * nGridW + nNoW;
        DOUBLE dTempRespon = ((DOUBLE*)(pImageResponse->imageData + pImageResponse->widthStep*ny))[nx];

        if ( dTempRespon > *(pValGrid+nNo ) )
        {
            *(pValGrid+nNo ) = dTempRespon;
            *(pGrid + 2*nNo ) = nx;
            *(pGrid + 2*nNo+1 ) = ny;
        }
    }
    vector<SINT> vecTempVal;
    vector<DOUBLE> vecTempResVal;
    for ( UINT i = 0; i < nTempGSize; i++ )
    {
        if ( *(pValGrid+i) > -999.0 )
        {
            vecTempResVal.push_back( *(pValGrid+i) );
            vecTempVal.push_back( *(pGrid+2*i) );
            vecTempVal.push_back( *(pGrid+2*i+1) );
        }
    }
    if( SINT(vecTempVal.size() / 2) < (nPtNum) )
    {
        for( UINT i = 0; i < vecTempVal.size(); i += 2 )
        {
            Pt2i ptTemp;
            ptTemp.X = vecTempVal.at(i);
            ptTemp.Y = vecTempVal.at(i+1);
            vecFeaPts.push_back( ptTemp );
        }
    }
    else
    {
        if( nPtNum != 0 )
        {
            vector<DOUBLE> vecTSort(vecTempResVal);
            std::sort( vecTSort.begin(),  vecTSort.end() );//从小到大排序
            SINT nT = vecTSort.size();

            DOUBLE dValThresh = vecTSort[nT-nPtNum-1];
            for( UINT j = 0; j < vecTempVal.size() / 2; ++j )
            {
                if( vecTempResVal[j] > dValThresh )
                {
                    Pt2i ptTemp;
                    ptTemp.X = vecTempVal[2*j];
                    ptTemp.Y = vecTempVal[2*j+1];
                    vecFeaPts.push_back( ptTemp );
                }
            }
        }
        else
        {
            for( UINT j = 0; j < vecTempVal.size() / 2; ++j )
            {
                Pt2i ptTemp;
                ptTemp.X = vecTempVal[2*j];
                ptTemp.Y = vecTempVal[2*j+1];
                if( true == bIsRemoveAbPixel )
                {
                    DOUBLE dValTemp = 0;
                    if( true == InputImg.GetDoubleVal( ptTemp.Y, ptTemp.X, dValTemp ) )
                    {
                        if( fabs( dValTemp - 255 ) < ML_ZERO )
                        {
                            continue;
                        }
                    }
                }
                vecFeaPts.push_back( ptTemp );

            }
        }
    }

    delete[] pGrid;
    delete[] pValGrid;

    cvReleaseImage(&pImg);
    cvReleaseImage(&pImageTTemp);
    cvReleaseImage(&pImageXY2);
    cvReleaseImage(&pImageResponse);
    cvReleaseImage(&pImageResGradX);
    cvReleaseImage(&pImageResGradY);



    return true;
}
bool CmlFrameImage::GrayTensile( CmlRasterBlock* pBlock, UINT nMin, UINT nMax )
{
    if(( pBlock == NULL )||( false == pBlock->IsValid() ))
    {
        return false;
    }

    BYTE max_val = 0;
    BYTE min_val = 255;
    BYTE tmp_val;
    //查找影像最大灰度值与最小灰度值
    for( UINT i = 0; i < pBlock->GetH(); i++ )
    {
        for( UINT j = 0; j < pBlock->GetW(); j++ )
        {
            tmp_val = pBlock->GetAt(i,j);
            if( tmp_val > max_val )
            {
                max_val = tmp_val;
            }
            if( tmp_val < min_val )
            {
                min_val = tmp_val;
            }
        }
    }
    //若数据块只有一个灰度值的情况
    if( max_val == min_val )
    {
        return true;
    }
    else if( ( nMax == max_val )&&( nMin == min_val ) )
    {
        return true;
    }
    else
    {
        //进行灰度拉伸
        for( UINT i = 0; i < pBlock->GetH(); i++ )
        {
            for( UINT j = 0; j < pBlock->GetW(); j++ )
            {
                tmp_val = pBlock->GetAt( i, j );
                tmp_val = ( tmp_val - min_val )  * ( nMax - nMin ) / ( max_val - min_val );
                if( tmp_val > nMax )
                {
                    tmp_val = nMax;
                }
                else if( tmp_val < nMin )
                {
                    tmp_val = nMin;
                }
                pBlock->SetAt( i, j, tmp_val );
            }
        }
    }
    return true;
}
bool CmlFrameImage::GrayTensile( UINT nMin, UINT nMax )
{
    return this->GrayTensile( &m_DataBlock, nMin, nMax );
}

bool CmlFrameImage::SaveFile( const char* strOutPath )
{
    if( false == this->m_DataBlock.IsValid() )
    {
        return false;
    }

    CmlGdalDataset clsGdalDSet;
    string strPath( strOutPath );
    SINT nPos = strPath.rfind( ".");
//    SINT nL = strPath.length();

    if( (nPos+1) >= strPath.length() )
    {
        return false;
    }
    string strPre;
    strPre.assign( strPath, (nPos+1), strPath.length() );

    string strOutPrefix;
    if( 0 == strcmp( strPre.c_str(), "bmp") )
    {
        strOutPrefix = "BMP";
    }
    else if( 0 == strcmp( strPre.c_str(), "tif") )
    {
        strOutPrefix = "GTIFF";
    }
    else if( 0 == strcmp( strPre.c_str(), "jpg") )
    {
        strOutPrefix = "JPEG";
    }

    if( false == clsGdalDSet.CreateFile( strOutPath, m_DataBlock.GetW(), m_DataBlock.GetH(), 1, m_DataBlock.GetGDTType(), strOutPrefix.c_str() ) )
    {
        return false;
    }

    if( false == clsGdalDSet.SaveBlockToFile( UINT(1), UINT(0), UINT(0), &m_DataBlock ) )
    {
        return false;
    }
    return true;
}

//直方图均衡化
bool CmlFrameImage::HistogramEqualize()
{
    if( false == this->m_DataBlock.IsValid() )
    {
        return false;
    }
    IplImage* pTempIn = NULL;

    if( false == CmlRBlock2IplImg( &this->m_DataBlock, pTempIn  ) )
    {
        return false;
    }
    cvEqualizeHist( pTempIn, pTempIn );
    if( false == IplImage2CmlRBlock( pTempIn, &this->m_DataBlock ) )
    {
		cvReleaseImage( &pTempIn );
        return false;
    }
	cvReleaseImage( &pTempIn );
    return true;

}

//栅格影像拉伸,dThresh0.0015表示截取0.15%灰度
bool CmlFrameImage::RasterGrayStrench(CmlRasterBlock *rasterBlock,double dThresh)
{
	using namespace cv;

	if (false == rasterBlock->IsValid())
	{
		return false;
	}

	IplImage* pTemIn =NULL;
	if (false == CmlRBlock2IplImg(rasterBlock,pTemIn))
	{
		return false;
	}
	
	//获取总点数
	double dTotalPts = (rasterBlock->GetW()) * (rasterBlock->GetH());
	int minLevel =-1;
	int maxLevel =-1;
	//统计直方图
	int HistogramBins =256;
	float HistogramRange1[2] = {0,255};
	float *HistogramRange[1] = {&HistogramRange1[0]};

	CvHistogram *Histogram1;
	Histogram1 = cvCreateHist(1,&HistogramBins,CV_HIST_ARRAY,HistogramRange);
	cvCalcHist(&pTemIn, Histogram1);
	cvReleaseImage(&pTemIn);

	double CumulativeNumber =0.0;
	double CDFArray[256];
	CvMat *LookupTableMatrix = cvCreateMat(1,256,CV_8UC1);

	//统计灰度范围下限
	CumulativeNumber =0;
	for (int i=0; i<HistogramBins; i++)
	{
		CumulativeNumber = CumulativeNumber +cvQueryHistValue_1D(Histogram1,i);
		CDFArray[i] = CumulativeNumber;
		if ((CumulativeNumber/dTotalPts) >dThresh)
		{
			minLevel =i-1;
			if (minLevel <0)
			{
				minLevel =0;
			}
			break;
		}	
	}
	
	//统计灰度上限
	CumulativeNumber =0;
	for (int i=255; i>0; i--)
	{
		CumulativeNumber = CumulativeNumber +cvQueryHistValue_1D(Histogram1,i);
		CDFArray[i] = CumulativeNumber;
		if ((CumulativeNumber/dTotalPts) >dThresh)
		{
			maxLevel =i+1;
			if (maxLevel>255)
			{
				minLevel =255;
			}
			break;
		}	
	}

	if (minLevel >maxLevel)
	{
		printf("Error in RasterGrayStrench: minLevel > maxLevel!\n");
		return false;
	}

	//灰度拉伸
	double p1=0.0;//乘系数
	double p2= 0.0;//加系数

	p1 = 255 / (double(maxLevel)-(double)minLevel);
	p2= -(double)minLevel *p1;

	double tmp_val =0.0;
	for (UINT i =0; i<rasterBlock->GetH(); i++)
	{
		for (UINT j=0;j<rasterBlock->GetW();j++)
		{
			tmp_val = double(rasterBlock->GetAt(i,j));
			tmp_val =tmp_val * p1 +p2;
			if (tmp_val>255)
			{
				tmp_val =255;
			}
			else if (tmp_val <0)
			{
				tmp_val =0;
			}
			rasterBlock->SetAt(i,j,BYTE(tmp_val));
		}
	}
	
	return true;
}
bool CmlFrameImage::DrawCrossMark( vector<Pt2i> vecPts, UINT nLineLength, UINT nLineWidth )
{
    if( false == this->m_DataBlock.IsValid() )
    {
        return false;
    }
    IplImage* pTempIn = NULL;

    if( false == CmlRBlock2IplImg( &this->m_DataBlock, pTempIn  ) )
    {
        return false;
    }

    UINT nHalfSize = nLineLength / 2;
    for( UINT i = 0; i < vecPts.size(); ++i )
    {
        Pt2i ptCur = vecPts[i];
        cvLine( pTempIn, cvPoint( (ptCur.X - nHalfSize), ptCur.Y ), cvPoint( (ptCur.X + nHalfSize), ptCur.Y ), CV_RGB( 0, 255, 0 ), nLineWidth );
        cvLine( pTempIn, cvPoint( ptCur.X, ( ptCur.Y - nHalfSize)), cvPoint( ptCur.X, ( ptCur.Y + nHalfSize)), CV_RGB( 0, 255, 0 ), nLineWidth );
    }
    if( false == IplImage2CmlRBlock( pTempIn, &this->m_DataBlock) )
    {
        return false;
    }
    return true;

}
bool CmlFrameImage::WallisFilter( UINT nTemplateSize, DOUBLE dExpectMean, DOUBLE dExpectVar, DOUBLE dCoefA, DOUBLE dCoefAlpha )
{
    CmlRasterBlock clsRasterB;
    if( false == clsRasterB.InitialImg( m_DataBlock.GetH(), m_DataBlock.GetW(), m_DataBlock.GetBytes() ) )
    {
        return false;
    }
    UINT nHalfSize = nTemplateSize / 2;
    UINT nH = clsRasterB.GetH();
    UINT nW = clsRasterB.GetW();

    clsRasterB.SetGDTType( GDT_Byte );
    if( ( nH < (2*nHalfSize+1) )||( nW < (2*nHalfSize+1) ) )
    {
        return false;
    }
    for( UINT i = 0; i < nH; ++i )
    {
        for( UINT j = 0; j < nW; ++j )
        {
            DOUBLE dCurVal = -1;
            if( false ==  m_DataBlock.GetDoubleVal( i, j, dCurVal ) )
            {
                continue;
            }
            if( ( i < nHalfSize )||( i > (nH-1-nHalfSize) )||( j < nHalfSize )||( j > ( nW - 1 - nHalfSize) ) )
            {
                clsRasterB.SetDoubleVal( i, j , dCurVal );
            }
            else
            {
                DOUBLE dTempMean = 0;
                DOUBLE dTempVar = 0;
                for( UINT h = i-nHalfSize; h <= i+nHalfSize; ++h )
                {
                    for( UINT k = j-nHalfSize; k <= j+nHalfSize; ++k )
                    {
                        DOUBLE dTemp = 0;
                        if( true == m_DataBlock.GetDoubleVal( h, k, dTemp ) )
                        {
                            dTempMean += dTemp;
                        }
                    }
                }
                dTempMean /= ( 2*nHalfSize + 1 )*( 2*nHalfSize + 1 );

                for( UINT h = i-nHalfSize; h <= i+nHalfSize; ++h )
                {
                    for( UINT k = j-nHalfSize; k <= j+nHalfSize; ++k )
                    {
                        DOUBLE dTemp = 0;
                        if( true == m_DataBlock.GetDoubleVal( h, k, dTemp ) )
                        {
                            dTempVar += (dTemp - dTempMean)*(dTemp - dTempMean);
                        }
                    }
                }
                dTempVar /=  ( 2*nHalfSize + 1 )*( 2*nHalfSize + 1 );
                dTempVar = sqrt( dTempVar );

                DOUBLE dTempCurVal = dCoefAlpha*dExpectMean + ( 1- dCoefAlpha )*dTempMean + (dCurVal - dTempMean)*(  dCoefA*dExpectVar / (dCoefA*dTempVar + dExpectVar ) );
                if( dTempCurVal < 0 )
                {
                    dTempCurVal = 0;
                }
                else if( dTempCurVal > 255 )
                {
                    dTempCurVal = 255;
                }
                clsRasterB.SetDoubleVal( i, j, dTempCurVal );
            }
        }
    }
    memcpy( (void*)m_DataBlock.GetData(), (void*)clsRasterB.GetData(), (clsRasterB.GetBytes()*clsRasterB.GetW()*clsRasterB.GetH() ) );

    return true;
}
bool CmlFrameImage::PtSelectionByGrid( vector<Pt2d> vecPts, UINT nImgW, UINT nImgH, UINT nGridW, UINT nGridH, vector<Pt2d> &vecOutRes )
{
    vector<bool> vecFlags;
    if( true == this->PtSelectionByGrid( vecPts, nImgW, nImgH, nGridW, nGridH, vecFlags ) )
    {
        for( UINT i = 0; i < vecFlags.size(); ++i )
        {
            if( true == vecFlags[i] )
            {
                vecOutRes.push_back( vecPts[i] );
            }
        }
        return true;
    }
    else
    {
        return false;
    }
}
bool CmlFrameImage::PtSelectionByGrid( vector<Pt2d> vecPts, UINT nImgW, UINT nImgH, UINT nGridW, UINT nGridH, vector<bool> &vecFlags )
{
    if( 0 != vecFlags.size() )
    {
        vecFlags.clear();
    }
    if( ( nImgW == 0 )||( nImgH == 0 )||( nGridW == 0 )||( nGridH == 0 ) )
    {
        return false;
    }
    if( ( nGridH > nImgH ) || ( nGridW > nImgW ) )
    {
        return false;
    }
    UINT nGridWCount =  nImgW / nGridW;
    if( ( nImgW % nGridW ) != 0 )
    {
        ++nGridWCount;
    }
    UINT nGridHCount =  nImgH / nGridH;
    if( ( nImgH % nGridH ) != 0 )
    {
        ++nGridHCount;
    }
    SINT* pFlags = new SINT[nGridHCount*nGridWCount];
    DOUBLE* pDisVal = new DOUBLE[nGridHCount*nGridWCount];
    for( UINT i = 0; i < nGridHCount*nGridWCount; ++i )
    {
        pFlags[i] = -1;
        pDisVal[i] = 10e10;
    }
    for( UINT i = 0; i < vecPts.size(); ++i )
    {
        vecFlags.push_back( false );

        Pt2d ptCur = vecPts[i];
        if( ( ptCur.X > (nImgW-1) ) || ( ptCur.X < 0 )||( ptCur.Y > (nImgH-1) ) || ( ptCur.Y < 0 ) )
        {
            continue;
        }

        UINT nTX = UINT( ptCur.X + ML_ZERO );
        UINT nTY = UINT( ptCur.Y + ML_ZERO );

        UINT nTW = nTX / nGridW;
        if( nTW >= nGridWCount )
        {
            nTW = ( nGridWCount-1);
        }
        UINT nTH = nTY / nGridH;
        if( nTH >= nGridHCount )
        {
            nTH = ( nGridHCount-1);
        }
        Pt2d ptCent;
        if( nTW < ( nGridWCount - 1 ) )
        {
            ptCent.X = (nTW+0.5)*nGridW;
        }
        else
        {
            ptCent.X = ( nTW*nGridW + nImgW - 1 ) / 2.0;
        }
        if( nTH < ( nGridHCount - 1 ) )
        {
            ptCent.Y = (nTH+0.5)*nGridH;
        }
        else
        {
            ptCent.Y = ( nTH*nGridH + nImgH - 1 ) / 2.0;
        }
        UINT nIndex = nTW+nTH*nGridWCount;

        DOUBLE dTempDis = DisIn2Pts( ptCent, ptCur );
        if( dTempDis < pDisVal[nIndex] )
        {
            pFlags[nIndex] = i;
            pDisVal[nIndex] = dTempDis;
        }
    }
    for( UINT i = 0; i < nGridHCount*nGridWCount; ++i )
    {
        if( pFlags[i] != -1 )
        {
            vecFlags[pFlags[i]] = true ;
        }
    }
    delete[] pFlags;
    delete[] pDisVal;

    return true;
}
bool CmlFrameImage::imgpyramid(CmlRasterBlock *InputImg, vector<CmlRasterBlock> &OutputImg,SINT nSize)
{

    IplImage* pImg = NULL;
    if( false == CmlRBlock2IplImg( InputImg, pImg  ) )
    {
        return false;
    }
    for(SINT i=0; i<=nSize; i++)
    {
        SINT ndcoef = SINT(pow(2.0, i));
        IplImage* pSTempImg = cvCreateImage( cvSize( pImg->width/ndcoef, pImg->height/ndcoef ), IPL_DEPTH_8U, 1 );
        cvResize(pImg, pSTempImg, CV_INTER_AREA );
        CmlRasterBlock TempImg;
        if( true == IplImage2CmlRBlock( pSTempImg, &TempImg ) )
        {
            this->SmoothByGuassian(TempImg,11, 0.6);
            OutputImg.push_back(TempImg);
            cvReleaseImage( &pSTempImg );
        }
        else
        {
            cvReleaseImage( &pSTempImg );
            return false;
        }
    }
    return true;
}
bool CmlFrameImage::EdgeDetectionByCanny( void* pImg, vector<Pt2i> &vecPts, DOUBLE dThresHold1, DOUBLE dThresHold2, UINT nMaxPts )
{
    IplImage* pImgIn = (IplImage*)pImg;
    IplImage* pImgOut = cvCreateImage( cvGetSize( pImgIn ), IPL_DEPTH_8U, 1 );
    cvCanny( pImgIn, pImgOut, dThresHold1, dThresHold2 );

    CvSize cs = cvGetSize( pImgIn );
    UINT nW = cs.width;
    UINT nH = cs.height;
    UINT nIdx = 0;
    for( UINT i = 0; i < nH; ++i )
    {
        for( UINT j = 0; j < nW; ++j )
        {
            BYTE* pdXData = (BYTE*)(pImgOut->imageData + pImgOut->widthStep*i);
            BYTE bCurVal = pdXData[j];
            if( bCurVal > 208 )
            {
                Pt2i ptCur;
                ptCur.X = j;
                ptCur.Y = i;
                vecPts.push_back(ptCur);
            }
        }
    }
    cvReleaseImage( &pImgOut );
    //------------------------------
    if( nMaxPts == 0 )
    {
        return vecPts.size();
    }
    if( vecPts.size() > nMaxPts )
    {
        DOUBLE dCoef = ( vecPts.size() * 1.0 ) / ( nMaxPts * 1.0 );
        if( dCoef >= 1.0 )
        {
            vector<Pt2i> vecTmpPts;
            for( UINT i = 0; i < nMaxPts; ++i )
            {
                UINT nIdxCur = UINT( dCoef * i + 0.5 );
                if( nIdxCur < vecPts.size()-1 )
                {
                    vecTmpPts.push_back( vecPts[nIdxCur]);
                }
            }
            vecPts.clear();
            vecPts = vecTmpPts;
        }
    }
    return vecPts.size();
}
bool CmlFrameImage::EdgeDetectionByCanny( const SCHAR* strInPath, vector<Pt2i> &vecPts, DOUBLE dThresHold1, DOUBLE dThresHold2, UINT nMaxPts )
{
    IplImage* pImg = NULL;
    pImg = cvLoadImage( strInPath, CV_LOAD_IMAGE_GRAYSCALE );
    if( pImg == NULL )
    {
        return false;
    }
    if( false == EdgeDetectionByCanny( (void*)pImg, vecPts, dThresHold1, dThresHold2, nMaxPts ) )
    {
        return false;
    }
    cvReleaseImage( &pImg );
    return true;
}
