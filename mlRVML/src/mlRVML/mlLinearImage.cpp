/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlLinearImage.cpp
* @date 2011.11.18
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 线阵卫星影像处理源文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/

#include "mlLinearImage.h"
#define ML_PI   3.1415926535897932384626433832795
#define Moon_Radius 1737400

/**
* @fn CmlLinearImage
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlLinearImage类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlLinearImage::CmlLinearImage()
{
    //ctor
}
/**
* @fn ~CmlLinearImage
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlLinearImage类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlLinearImage::~CmlLinearImage()
{
    //dtor
}
/**
* @fn CmlCE1LinearImage
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlCE1LinearImage类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlCE1LinearImage::CmlCE1LinearImage()
{

}
/**
* @fn ~CmlCE1LinearImage
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlCE1LinearImage类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlCE1LinearImage::~CmlCE1LinearImage()
{

}
/**
* @fn CmlCE2LinearImage
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlCE2LinearImage类空参构造函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlCE2LinearImage::CmlCE2LinearImage()
{

}
/**
* @fn ~CmlCE2LinearImage
* @date 2011.12.16
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CmlCE2LinearImage类析构函数
* @version 1.0
* @return 无返回值
* @par 修改历史：
* <作者>    <时间>   <版本编号>    <修改原因>\n
*/
CmlCE2LinearImage::~CmlCE2LinearImage()
{

}
/**
* @fn mlCE1InOrietation
* @date 2011.11.20
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CE-1卫星影像内定向
* @param vecPtsList 像点坐标
* @param pSatSio 内定向参数结构体
* @param vecXY 内定向后坐标
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE1LinearImage::mlCE1InOrietation( vector<Pt2d> &vecPtsList, CE1IOPara *pSatSio, vector<Pt2d> &vecXY )
{
    //CE-1卫星影像内定向，将卫星影像匹配同名点行列号转换为像平面坐标
    //vecPtsList 输入时为像点行列号坐标，输出时为焦平面坐标
    //vecPtsList第一列是列号，第二列是行号,（左上角坐标系）
    /*计算x坐标，为一常量*/
    UINT i;
    DOUBLE x_val;
    Pt2d tmpSL, tmpXY;
    x_val = ( pSatSio->l0 - pSatSio->nCCD_line ) * pSatSio->pixsize - pSatSio->x0;
    if ( (pSatSio->nSample == 0) || (pSatSio->pixsize == 0) )
    {
        return false;
    }
    //计算y坐标，因上行下行而不同/
    //上行时，由南向北飞行，X为飞行方向，向上，Y垂直于X向左
    else if ( false == pSatSio->upflag )
    {
        for( i = 0; i < vecPtsList.size(); i++ )
        {
            tmpSL = vecPtsList.at(i);
            tmpXY.X = x_val;
            tmpXY.Y = ( pSatSio->s0 - tmpSL.X ) * pSatSio->pixsize + pSatSio->y0;
            vecXY.push_back(tmpXY);
        }
    }
    //下行时，由北向南飞行，X为飞行方向，向下，Y垂直于X向右
    else if( true == pSatSio->upflag )
    {
        for( i = 0; i < vecPtsList.size(); i++ )
        {
            tmpSL = vecPtsList.at(i);
            tmpXY.X = x_val;
            tmpXY.Y = ( tmpSL.X - pSatSio->s0 ) * pSatSio->pixsize - pSatSio->y0;
            vecXY.push_back(tmpXY);
        }
    }
    else
    {
        return false;
    }
    return true;
}
/**
* @fn mlCE2InOrietation
* @date 2012.03.01
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CE-2卫星影像内定向
* @param vecPtsList 输入时为像点行列号坐标，输出时为焦平面坐标
* @param pSatSio 卫星影像内定向参数结构体
* @param vecXY 计算出的焦平面坐标
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE2LinearImage::mlCE2InOrietation( vector<Pt2d> &vecPtsList, CE2IOPara *pSatSio, vector<Pt2d> &vecXY )
{
    //将卫星影像匹配同名点行列号转换为像平面坐标
    //计算x坐标，为一常量
    UINT i;
    DOUBLE x_val;
    Pt2d tmpSL, tmpXY;
    if ( ( pSatSio->AngleDeg == 0 ) || (pSatSio->f == 0) || (pSatSio->nSample == 0) || (pSatSio->pixsize == 0) )
    {
        return false;
    }
    else
    {
        x_val = pSatSio->x0 - tan( pSatSio->AngleDeg * ML_PI / 180.0 ) * pSatSio->f;
    }
    //计算y坐标，因上行下行而不同/
    //下行时，由北向南飞行，X为飞行方向，向上，Y垂直于X向右
    if ( !pSatSio->upflag )
    {
        for( i = 0; i < vecPtsList.size(); i++ )
        {
            tmpSL = vecPtsList.at(i);
            tmpXY.Y = ( tmpSL.X - pSatSio->s0 ) * pSatSio->pixsize - pSatSio->y0;
            tmpXY.X = x_val;
            vecXY.push_back(tmpXY);

        }
    }
    //上行时，由南向北飞行，X为飞行方向，向上，Y垂直于X向右
    else if( pSatSio->upflag )
    {
        for( i = 0; i < vecPtsList.size(); i++ )
        {
            tmpSL = vecPtsList.at(i);
            //tmpXY.Y = pSatSio->y0 - ( tmpSL.X - pSatSio->s0 ) * pSatSio->pixsize;
            tmpXY.Y = ( pSatSio->s0 - tmpSL.X  ) * pSatSio->pixsize;
            tmpXY.X = x_val;
            vecXY.push_back(tmpXY);
        }
    }
    else
    {
        return false;
    }
    return true;
}
/**
* @fn mlCloseVal
* @date 2011.11.22
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 计算最临近值
* @param A 待搜索数列
* @param trueval 某给定值
* @return indx 数列A中与trueval最接近值的下标
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
UINT mlCloseVal( CmlMat *A, DOUBLE trueval )
{
    //搜索数列中与某一给定值最邻近的值，并返回该值在数列A中的下标
    DOUBLE minval = abs( A->GetAt( 0, 0 ) - trueval );
    UINT indx = 0;
    for( UINT i = 0; i < A->GetH()-1; i++ )
    {
        if( abs( A->GetAt( i + 1, 0 ) - trueval ) < minval )
        {
            minval = abs( A->GetAt( i + 1 ,0) - trueval );
            indx = i + 1;
        }
        else
        {
            continue;
        }
    }
    return indx;
}
/**
* @fn mlPolyA
* @date 2011.11.22
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 构造关于时间的三次多项式矩阵
* @param pTemp_time 一维时间数组t
* @param MatA 构造的三次多项式矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool mlPolyA( CmlMat *pTemp_time, CmlMat* MatA )
{
    //根据给定的一维的时间数组构造三次多项式矩阵
    for( UINT i = 0; i < pTemp_time->GetH(); i++ )
    {
        MatA->SetAt( i, 0, 1 ); //多项式常数项t0
        MatA->SetAt( i, 1, pTemp_time->GetAt(i,0)); //多项式一次项t1
        MatA->SetAt( i, 2, MatA->GetAt( i, 1 ) * MatA->GetAt( i, 1 ) ); //多项式二次项t2
        MatA->SetAt( i, 3, MatA->GetAt( i, 2 ) * MatA->GetAt( i, 1 ) );  //多项式三次项t3
    }
    return true;
}
/**
* @fn mlinterEo
* @date 2011.11.22
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 给定外方位元素及其对应时间，根据影像时间内插出影像每一行的外方位
* @param pImg_time 影像时间序列
* @param pElement 某一类外方位元素序列
* @param pTime 外方位元素序列对应的时间序列
* @param pEo 内插后的影像外方位元素
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool mlinterEo(CmlMat *pTime, CmlMat *pElement, CmlMat *pImg_time, CmlMat *pEo)
{
    UINT nScanline = pImg_time->GetH();
    //查找与img_time[0]值最邻近的time的位置time[start_pos];
    UINT start_pos = mlCloseVal(pTime, pImg_time->GetAt(0,0));
    //查找与img_time[nscanline-1]值最邻近的time的位置time[end_pos];
    UINT end_pos = mlCloseVal( pTime, pImg_time->GetAt(nScanline-1,0));
    UINT length = end_pos-start_pos+1;
    DOUBLE time_offset = (pImg_time->GetAt(0,0) < pImg_time->GetAt(nScanline-1,0))?pImg_time->GetAt(0,0):pImg_time->GetAt(nScanline-1,0);
    CmlMat MatL, Temp_time;
    MatL.Initial( length, 1 );
    Temp_time.Initial(length,1);
    UINT i;
    for( i = 0; i < length; i++ )
    {
        //构造时间向量
        Temp_time.SetAt(i, 0, ( pTime->GetAt(i + start_pos,0)-time_offset ) );
        //构造L矩阵
        MatL.SetAt( i, 0, pElement->GetAt(i + start_pos,0) );
    }

    CmlMat MatA, MatAT, MatATA, MatATL, InvATA, MatX;
    MatA.Initial( length, 4 );
    //构造A矩阵
    mlPolyA( &Temp_time, &MatA );
    //求解inv(ATA)*ATL,得到X矩阵，即多项式拟合系数
    mlMatTrans( &MatA, &MatAT );
    mlMatMul( &MatAT, &MatL, &MatATL );
    mlMatMul( &MatAT, &MatA, &MatATA );
    mlMatInv( &MatATA, &InvATA );
    mlMatMul( &InvATA, &MatATL, &MatX );

    //根据影像时间，计算每一行扫描线外方位
    CmlMat MatA1;
    MatA1.Initial( nScanline, 4 );
    CmlMat pImg_temptime;
    pImg_temptime.Initial( pImg_time->GetH(), pImg_time->GetW() );
    for( i = 0; i < pImg_time->GetH(); i++ )
    {
        pImg_temptime.SetAt( i, 0, (pImg_time->GetAt(i,0)-time_offset) );
    }
    //构成多项式矩阵
    mlPolyA( &pImg_temptime, &MatA1 );
    //A*X = EO
    mlMatMul( &MatA1, &MatX, pEo );
    return true;
}
/**
* @fn mlGetEop
* @date 2011.11.22
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 轨道测控数据多项式内插外方位元素
* @param vecLineEo 原始测控数据中线元素
* @param vecAngleEo 原始测控数据中角元素
* @param vecImg_time 卫星影像扫描行获取时间
* @param vecEo 内插后外方位
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlLinearImage::mlGetEop( vector<LineEo> &vecLineEo, vector<AngleEo> &vecAngleEo, vector<DOUBLE> &vecImg_time, vector<ExOriPara> *vecEo )
{
    //利用扫描行之间的相关性构建高次多项式对轨道测控数据进行插值，得到每条扫描行的外方位元素
    //矩阵初始化
    if( (vecLineEo.size()==0)||(vecAngleEo.size()==0)||(vecImg_time.size()==0) )
    {
        SCHAR strErr[] = "There's no exterior orientation parameters!Please check the files!\n" ;
        LOGAddErrorMsg(strErr) ;
        return false;
    }
    CmlMat MatImg_time, MatLine_time,MatOriXs,MatOriYs,MatOriZs,MatAngle_time,MatOriPitch,MatOriRoll,MatOriYaw;
    MatImg_time.Initial(vecImg_time.size(),1);
    MatLine_time.Initial(vecLineEo.size(),1);
    MatOriXs.Initial(vecLineEo.size(),1);
    MatOriYs.Initial(vecLineEo.size(),1);
    MatOriZs.Initial(vecLineEo.size(),1);
    MatAngle_time.Initial(vecAngleEo.size(),1);
    MatOriPitch.Initial(vecAngleEo.size(),1);
    MatOriRoll.Initial(vecAngleEo.size(),1);
    MatOriYaw.Initial(vecAngleEo.size(),1);

    UINT i;
    LineEo tmpLineEo;
    AngleEo tmpAngleEo;
    //读影像时间
    for(i = 0; i < vecImg_time.size(); i++)
    {
        MatImg_time.SetAt(i,0,vecImg_time[i]);
    }
    //读外方位线元素
    for(i = 0; i < vecLineEo.size(); i++)
    {
        tmpLineEo = vecLineEo.at(i);
        MatLine_time.SetAt(i,0,tmpLineEo.dEoTime);
        MatOriXs.SetAt(i,0,tmpLineEo.pos.X);
        MatOriYs.SetAt(i,0,tmpLineEo.pos.Y);
        MatOriZs.SetAt(i,0,tmpLineEo.pos.Z);
    }
    //读外方位角元素
    for(i = 0; i < vecAngleEo.size(); i++)
    {
        tmpAngleEo = vecAngleEo.at(i);
        MatAngle_time.SetAt(i,0,tmpAngleEo.dEoTime);
        MatOriPitch.SetAt(i,0,tmpAngleEo.ori.phi);
        MatOriRoll.SetAt(i,0,tmpAngleEo.ori.omg);
        MatOriYaw.SetAt(i,0,tmpAngleEo.ori.kap);
    }

    CmlMat MatXs, MatYs, MatZs, MatPtich, MatRoll, MatYaw;
    //分别内插六类外方位
    mlinterEo(&MatLine_time, &MatOriXs, &MatImg_time,  &MatXs);
    mlinterEo(&MatLine_time, &MatOriYs, &MatImg_time,  &MatYs);
    mlinterEo(&MatLine_time, &MatOriZs, &MatImg_time, &MatZs);
    mlinterEo(&MatAngle_time, &MatOriPitch, &MatImg_time,  &MatPtich);
    mlinterEo(&MatAngle_time, &MatOriRoll, &MatImg_time,  &MatRoll);
    mlinterEo(&MatAngle_time, &MatOriYaw, &MatImg_time,  &MatYaw);
    //写入ExOriPara外方位结构体
    ExOriPara tmp;
    for(i = 0; i < MatImg_time.GetH(); i++)
    {
        tmp.pos.X = MatXs.GetAt( i, 0 );
        tmp.pos.Y = MatYs.GetAt( i, 0 );
        tmp.pos.Z = MatZs.GetAt( i, 0 );
        tmp.ori.phi = MatPtich.GetAt( i, 0 );
        tmp.ori.omg = MatRoll.GetAt( i, 0 );
        tmp.ori.kap = MatYaw.GetAt( i, 0 );
        vecEo->push_back(tmp);
    }

    return true;
}
/**
* @fn mlBLH2XYZ
* @date 2011.11.29
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 将物方三维点由月球大地坐标系转成月固坐标系下的空间直角坐标
* @param blhPts 月球大地坐标系坐标
* @param xyzPts 月固坐标系下的空间直角坐标
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlLinearImage::mlBLH2XYZ(Pt3d& blhPts, Pt3d& xyzPts)
{
    //将物方三维点由月球大地坐标系（Latitude,Longitude,Height）转成月固坐标系下的XYZ.精度为precision(10)
    xyzPts.Z = ( blhPts.Z + Moon_Radius ) * sin( Deg2Rad(blhPts.Y) );
    xyzPts.X = ( blhPts.Z + Moon_Radius ) * cos( Deg2Rad(blhPts.Y) ) * cos( Deg2Rad(blhPts.X) );
    xyzPts.Y = ( blhPts.Z + Moon_Radius ) * cos( Deg2Rad(blhPts.Y) ) * sin( Deg2Rad(blhPts.X) );
    return true;
}
/**
* @fn mlXYZ2BLH
* @date 2011.11.29
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 将物方三维点由月固坐标系下的空间直角坐标转成月球大地坐标系
* @param xyzPts 月固坐标系下的XYZ坐标
* @param blhPts 月球大地坐标系（Longitude,Latitude,Height）坐标
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlLinearImage::mlXYZ2BLH(Pt3d &xyzPts, Pt3d &blhPts)
{
    //将物方三维点由月固坐标系下的XYZ转成月球大地坐标系(Latitude,Longitude,Height).精度为precision(10)
    blhPts.X = Rad2Deg(atan2(xyzPts.Y,xyzPts.X));
    blhPts.Y = Rad2Deg(atan2(xyzPts.Z,sqrt(xyzPts.X*xyzPts.X+xyzPts.Y*xyzPts.Y)));
    blhPts.Z = sqrt(xyzPts.X*xyzPts.X+xyzPts.Y*xyzPts.Y+xyzPts.Z*xyzPts.Z)-Moon_Radius;
    return true;
}
/**
* @fn mlCE1OPK2RMat
* @date 2011.11.30
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CE-1卫星影像外方位角元素转旋转矩阵
* @param pitch 俯仰角
* @param roll 翻滚角
* @param yaw 航偏角
* @param pRMat 旋转矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE1LinearImage::mlCE1OPK2RMat( DOUBLE &pitch, DOUBLE &roll, DOUBLE &yaw, CmlMat* pRMat )
{
    if( false == pRMat->IsValid() )
    {
        pRMat->Initial( 3, 3 );
    }
    if( ( pRMat->GetH() != 3) || ( pRMat->GetW() != 3 ) )
    {
        return false;
    }
    //计算三角函数
    DOUBLE dSP = sin( Deg2Rad(pitch) );
    DOUBLE dSR = sin( Deg2Rad(roll) );
    DOUBLE dSY = sin( Deg2Rad(yaw) );

    DOUBLE dCP = cos( Deg2Rad(pitch) );
    DOUBLE dCR = cos( Deg2Rad(roll) );
    DOUBLE dCY = cos( Deg2Rad(yaw) );

    //求旋转矩阵每一个元素值
    pRMat->SetAt( 0, 0, (dCP*dCY - dSP*dSR*dSY) );
    pRMat->SetAt( 0, 1, (-dCP*dSY - dSP*dSR*dCY) );
    pRMat->SetAt( 0, 2, -dSP*dCR );

    pRMat->SetAt( 1, 0, dCR*dSY );
    pRMat->SetAt( 1, 1, dCR*dCY );
    pRMat->SetAt( 1, 2, -dSR );

    pRMat->SetAt( 2, 0, ( dSP*dCY + dCP*dSR*dSY ) );
    pRMat->SetAt( 2, 1, ( -dSP*dSY + dCP*dSR*dCY ) );
    pRMat->SetAt( 2, 2, dCP*dCR );
    return true;

}
/**
* @fn mlCE1OPK2RMat
* @date 2011.11.30
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 将CE-1卫星影像六类外方位角元素转成线元素及旋转矩阵的形式
* @param vecEo 外方位元素
* @param vecXsYsZs 线元素
* @param vecR 旋转矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE1LinearImage::mlCE1OPK2RMat( vector<ExOriPara> &vecEo, vector<Pt3d> & vecXsYsZs, vector<MatrixR> &vecR )
{
    if( vecEo.size() == 0 )
    {
        return false;
    }
    DOUBLE pitch, roll, yaw;
    Pt3d XsYsZs;
    CmlMat mattR;
    mattR.Initial( 3, 3 );
    MatrixR tmpR;
    for ( UINT i = 0; i < vecEo.size(); i++ )
    {
        //读写外方位线元素
        XsYsZs.X = vecEo[i].pos.X;
        XsYsZs.Y = vecEo[i].pos.Y;
        XsYsZs.Z = vecEo[i].pos.Z;
        vecXsYsZs.push_back(XsYsZs);
        //读外方位角元素
        pitch = vecEo[i].ori.phi;
        roll = vecEo[i].ori.omg;
        yaw = vecEo[i].ori.kap;
        //将欧拉角转旋转矩阵
        mlCE1OPK2RMat( pitch, roll, yaw, &mattR );
        tmpR.matR = mattR;
        vecR.push_back( tmpR );
    }
    return true;
}
/**
* @fn mlCE2OPK2RMat
* @date 2012.3.3
* @author 刘一良 ylliu@irsa.ac.cn
* @brief CE-2卫星影像外方位角元素转旋转矩阵
* @param angX 绕X轴夹角
* @param angY 绕Y轴夹角
* @param angZ 绕Z轴夹角
* @param pRMat 旋转矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE2LinearImage::mlCE2OPK2RMat( DOUBLE &angX, DOUBLE &angY, DOUBLE &angZ, CmlMat* pRMat )
{
    if( false == pRMat->IsValid() )
    {
        pRMat->Initial( 3, 3 );
    }
    if( ( pRMat->GetH() != 3) || ( pRMat->GetW() != 3 ) )
    {
        return false;
    }
    //计算三角函数
    DOUBLE dSX = sin( Deg2Rad(angX) );
    DOUBLE dSY = sin( Deg2Rad(angY) );
    DOUBLE dSZ = sin( Deg2Rad(angZ) );

    DOUBLE dCX = cos( Deg2Rad(angX) );
    DOUBLE dCY = cos( Deg2Rad(angY) );
    DOUBLE dCZ = cos( Deg2Rad(angZ) );

    //求旋转矩阵每一个元素值
//    Ra=[ 1.0000   -0.0008    0.0011;
//    0.0008    1.0000    0.0008;
//   -0.0011   -0.0008    1.0000];
    CmlMat pR;
    pR.Initial(3,3);
    pR.SetAt( 0, 0, (dCY*dCZ - dSY*dSX*dSZ) );
    pR.SetAt( 0, 1, (dCY*dSZ + dSY*dSX*dCZ) );
    pR.SetAt( 0, 2, (-dSY*dCX ) );

    pR.SetAt( 1, 0, (-dCX*dSZ) );
    pR.SetAt( 1, 1, dCX*dCZ ) ;
    pR.SetAt( 1, 2, dSX  );

    pR.SetAt( 2, 0, ( dSY*dCZ + dCY*dSX*dSZ ) );
    pR.SetAt( 2, 1, ( dSY*dSZ - dCY*dSX*dCZ ) );
    pR.SetAt( 2, 2, dCY*dCX );

    CmlMat pRa;//相机安置矩阵
    pRa.Initial(3,3);
    pRa.SetAt( 0, 0, 1.0 );
    pRa.SetAt( 0, 1, -0.0008 );
    pRa.SetAt( 0, 2, 0.0011 );

    pRa.SetAt( 1, 0, 0.0008 );
    pRa.SetAt( 1, 1, 1.0 ) ;
    pRa.SetAt( 1, 2, 0.0008  );

    pRa.SetAt( 2, 0, -0.0011 );
    pRa.SetAt( 2, 1, -0.0008 );
    pRa.SetAt( 2, 2, 1.0 );

    mlMatMul(&pR,&pRa,pRMat);
    return true;

}
/**
* @fn mlCE2OPK2RMat
* @date 2011.11.30
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 将CE-2卫星影像六类外方位角元素转成线元素及旋转矩阵的形式
* @param vecEo 外方位元素
* @param vecXsYsZs 线元素
* @param vecR 旋转矩阵
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE2LinearImage::mlCE2OPK2RMat( vector<ExOriPara> &vecEo, vector<Pt3d> & vecXsYsZs, vector<MatrixR> &vecR )
{
    DOUBLE angX, angY, angZ;
    Pt3d XsYsZs;
    CmlMat mattR;
    mattR.Initial( 3, 3 );
    MatrixR tmpR;
    for ( UINT i = 0; i < vecEo.size(); i++ )
    {
        //读写外方位线元素
        XsYsZs.X = vecEo[i].pos.X;
        XsYsZs.Y = vecEo[i].pos.Y;
        XsYsZs.Z = vecEo[i].pos.Z;
        vecXsYsZs.push_back(XsYsZs);
        //读外方位角元素
        angX = vecEo[i].ori.phi;
        angY = vecEo[i].ori.omg;
        angZ = vecEo[i].ori.kap;
        //将欧拉角转旋转矩阵
        mlCE2OPK2RMat( angX, angY, angZ, &mattR );
        tmpR.matR = mattR;
        vecR.push_back( tmpR );
    }
    return true;
}
/**
* @fn mlGetCE1DOMCoordinate
* @date 2011.12.07
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 生成CE-1卫星影像DOM格网在原图像上的x,y坐标
* @param OriSatImg 原始影像
* @param vecR 影像外方位旋转矩阵
* @param vecXsYsZs 影像外方位线元素
* @param f 相机焦距
* @param vecPtXYZ 物方三维坐标
* @param pCE1IOP 内方位元素
* @param nWidth 卫星影像宽度
* @param nHeight 影像高度
* @param thresh 计算出的x坐标与理论值之差的阈值
* @param ImgSL 卫星影像DOM格网在原图像上的x,y坐标
* @param trueline 影像坐标真实行
* @param range 搜索范围
* @param thresh 阈值
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE1LinearImage::mlGetCE1DOMCoordinate(CmlRasterBlock &OriSatImg, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, DOUBLE f, vector<Pt3d> &vecPtXYZ, UINT nWidth, UINT nHeight, CRasterPt2D &ImgSL, DOUBLE trueline, UINT range, DOUBLE thresh )
{
    if( (!OriSatImg.IsValid()) || (vecR.size() == 0) || (vecXsYsZs.size() == 0) || (vecPtXYZ.size() == 0) || (nWidth <= 0) || (nHeight <= 0) )
    {
        return false;
    }
    /*二分法计算像点坐标,默认是上行*/
    UINT nStart, nEnd, nMiddle, nCycle, kStart, kLength, k, indx, nX, nY;
    UINT nScanline = OriSatImg.GetH();
    ExOriPara ExOri;
    Pt3d blhPts,xyzPts;
    Pt2d xyPts,tempXY;
    CmlMat RMat, MatImgX,MatImgY;
    CmlPhgProc PhgProc;
    CmlCE1LinearImage ce1;
    //逐三维点搜索计算
    for( nY = 0; nY < nHeight; nY++ )
        for( nX = 0; nX < nWidth; nX++ )
        {
            tempXY.X = -1;
            tempXY.Y = -1;
            ImgSL.SetAt( nY, nX, tempXY );
            nEnd = nScanline - 1;
            nStart = 0;
            //二分法搜索计算
            for ( nCycle = 0; nCycle < log2( DOUBLE(nScanline - 1 )); nCycle++ )
            {
                nMiddle = SINT( ( nStart + nEnd ) / 2 );//取整
                //取出外方位元素
                ExOri.pos = vecXsYsZs.at( nMiddle );
                RMat = vecR[ nMiddle ].matR;
                //取出物方三维点
                xyzPts = vecPtXYZ.at( nY * nWidth + nX );
                //ce1.mlBLH2XYZ( blhPts, xyzPts );//将BLH转成XYZ
                //根据共线方程由物方点计算像方x，y
                PhgProc.mlReproject( &xyPts, &xyzPts, &ExOri, f, f,&RMat );
                //当与真实值接近时，停止二分搜索，并以该行外方位为中心开辟搜索窗口，逐行外方位计算像方点x,y
                if( abs( xyPts.X - trueline ) < thresh )
                {
                    //当前搜索行左搜索区间长度小于range的情况
                    if ( nMiddle + range > nScanline - 1 )
                    {
                        MatImgX.Initial( nScanline - nMiddle + range , 1 );
                        MatImgY.Initial( nScanline - nMiddle + range , 1 );
                        kStart = nMiddle - range;
                        kLength = nScanline - nMiddle + range ;
                    }
                    //当前搜索行右搜索区间长度小于range的情况
                    else if ( nMiddle - range < 0 )
                    {
                        MatImgX.Initial( nMiddle + range + 1 , 1 );
                        MatImgY.Initial( nMiddle + range + 1 , 1 );
                        kStart = 0;
                        kLength = nMiddle + range + 1;
                    }
                    //当前搜索行左右搜索区间长度都不小于range的情况
                    else
                    {
                        MatImgX.Initial( 2 * range + 1,1 );
                        MatImgY.Initial( 2 * range + 1,1 );
                        kStart = nMiddle - range ;
                        kLength = 2 * range + 1;
                    }
                    //对搜索区间内每一行外方位计算像方x，y
                    for( k = kStart; k < ( kStart + kLength ); k++ )
                    {

                        ExOri.pos = vecXsYsZs.at( k );
                        RMat = vecR[ k ].matR;
                        PhgProc.mlReproject( &xyPts, &xyzPts, &ExOri, f, f, &RMat);//共线方程计算x，y
                        MatImgX.SetAt( k - kStart, 0, abs( xyPts.X - trueline ) ) ;
                        MatImgY.SetAt( k - kStart, 0, xyPts.Y ) ;
                    }
                    //对搜索区间内计算所得x值进行比较，找到最接近真实值的值
                    indx = mlCloseVal( &MatImgX, 0 );
                    //存储像平面坐标y值
                    tempXY.X = MatImgY.GetAt( indx, 0 );
                    //存储图像坐标Y值，即行号
                    tempXY.Y = indx + kStart;
                    //ImgSL存储计算出的Sample、Line，（其中x方向，即tempXY.Y已转换成行号）
                    ImgSL.SetAt( nY, nX, tempXY );
                    break;
                }
                //小于真实值时，搜索前半段
                else if( xyPts.X < trueline )
                {
                    nEnd = nMiddle;
                }
                //大于真实值时，搜索后半段
                else
                {
                    nStart = nMiddle;
                }
            }//二分法迭代计算
        }//遍历每个点
    return true;
}
/**
* @fn mlGetCE2DOMCoordinate
* @date 2011.12.07
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 生成CE-2卫星影像DOM格网在原图像上的x,y坐标
* @param OriSatImg 原始影像
* @param vecR 影像外方位旋转矩阵
* @param vecXsYsZs 影像外方位线元素
* @param f 相机焦距
* @param vecPtXYZ 物方三维坐标
* @param pCE1IOP 内方位元素
* @param nWidth 卫星影像宽度
* @param nHeight 影像高度
* @param thresh 计算出的x坐标与理论值之差的阈值
* @param ImgSL 卫星影像DOM格网在原图像上的x,y坐标
* @param trueline 影像坐标真实行
* @param range 搜索范围
* @param thresh 阈值
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE2LinearImage::mlGetCE2DOMCoordinate(CmlRasterBlock &OriSatImg, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, DOUBLE f, vector<Pt3d> &vecPtXYZ, UINT nWidth, UINT nHeight, CRasterPt2D &ImgSL, DOUBLE trueline, UINT range, DOUBLE thresh )
{
    if( (!OriSatImg.IsValid()) || (vecR.size() == 0) || (vecXsYsZs.size() == 0) || (vecPtXYZ.size() == 0) || (nWidth <= 0) || (nHeight <= 0) )
    {
        return false;
    }
    /*二分法计算像点坐标,默认是上行*/
    UINT nStart, nEnd, nMiddle, nCycle, kStart, kLength, k, indx, nX, nY;
    UINT nScanline = OriSatImg.GetH();
    ExOriPara ExOri;
    Pt3d blhPts,xyzPts;
    Pt2d xyPts,tempXY;
    CmlMat RMat, MatImgX,MatImgY;
    CmlPhgProc PhgProc;
    CmlCE1LinearImage ce1;
    //逐三维点搜索计算
    for( nY = 0; nY < nHeight; nY++ )
        for( nX = 0; nX < nWidth; nX++ )
        {
            tempXY.X = -1;
            tempXY.Y = -1;
            ImgSL.SetAt( nY, nX, tempXY );
            nEnd = nScanline - 1;
            nStart = 0;
            //二分法搜索计算
            for ( nCycle = 0; nCycle < log2( DOUBLE(nScanline - 1) ); nCycle++ )
            {
                nMiddle = SINT( ( nStart + nEnd ) / 2 );//取整
                //取出外方位元素
                ExOri.pos = vecXsYsZs.at( nMiddle );
                RMat = vecR[ nMiddle ].matR;
                //取出物方三维点
                xyzPts = vecPtXYZ.at( nY * nWidth + nX );
                //ce1.mlBLH2XYZ( blhPts, xyzPts );//将BLH转成XYZ
                //根据共线方程由物方点计算像方x，y
                PhgProc.mlReproject( &xyPts, &xyzPts, &ExOri, f, f,&RMat );
                //当与真实值接近时，停止二分搜索，并以该行外方位为中心开辟搜索窗口，逐行外方位计算像方点x,y
                if( abs( xyPts.X - trueline ) < thresh )
                {
                    //当前搜索行左搜索区间长度小于range的情况
                    if ( nMiddle + range > nScanline - 1 )
                    {
                        MatImgX.Initial( nScanline - nMiddle + range , 1 );
                        MatImgY.Initial( nScanline - nMiddle + range , 1 );
                        kStart = nMiddle - range;
                        kLength = nScanline - nMiddle + range ;
                    }
                    //当前搜索行右搜索区间长度小于range的情况
                    else if ( nMiddle - range < 0 )
                    {
                        MatImgX.Initial( nMiddle + range + 1 , 1 );
                        MatImgY.Initial( nMiddle + range + 1 , 1 );
                        kStart = 0;
                        kLength = nMiddle + range + 1;
                    }
                    //当前搜索行左右搜索区间长度都不小于range的情况
                    else
                    {
                        MatImgX.Initial( 2 * range + 1,1 );
                        MatImgY.Initial( 2 * range + 1,1 );
                        kStart = nMiddle - range ;
                        kLength = 2 * range + 1;
                    }
                    //对搜索区间内每一行外方位计算像方x，y
                    for( k = kStart; k < ( kStart + kLength ); k++ )
                    {

                        ExOri.pos = vecXsYsZs.at( k );
                        RMat = vecR[ k ].matR;
                        PhgProc.mlReproject( &xyPts, &xyzPts, &ExOri, f, f, &RMat);//共线方程计算x，y
                        MatImgX.SetAt( k - kStart, 0, abs( xyPts.X - trueline ) ) ;
                        MatImgY.SetAt( k - kStart, 0, xyPts.Y ) ;
                    }
                    //对搜索区间内计算所得x值进行比较，找到最接近真实值的值
                    indx = mlCloseVal( &MatImgX, 0 );
                    //存储像平面坐标y值
                    tempXY.X = MatImgY.GetAt( indx, 0 );
                    //存储图像坐标Y值，即行号
                    tempXY.Y = indx + kStart;
                    //ImgSL存储计算出的Sample、Line，（其中x方向，即tempXY.Y已转换成行号）
                    ImgSL.SetAt( nY, nX, tempXY );
                    break;
                }
                //小于真实值时，搜索前半段
                else if( xyPts.X > trueline )
                {
                    nEnd = nMiddle;
                }
                //大于真实值时，搜索后半段
                else
                {
                    nStart = nMiddle;
                }
            }//二分法迭代计算
        }//遍历每个点
    return true;
}
/**
* @fn mlGetCE1DOM
* @date 2011.12.07
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 生成CE-1卫星影像DOM
* @param OriSatImg 原始CE-1卫星影像
* @param vecR 影像外方位旋转矩阵
* @param vecXsYsZs 影像外方位线元素
* @param pCE1IOP 内方位元素
* @param vecPtXYZ 三维物方点
* @param range 外方位搜索范围
* @param thresh 计算出的x坐标与理论值之差的阈值
* @param SatDom CE-1卫星影像DOM
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE1LinearImage::mlGetCE1DOM( CmlRasterBlock &OriSatImg, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, CE1IOPara *pCE1IOP, vector<Pt3d> &vecPtXYZ, CmlRasterBlock &SatDom, UINT range, DOUBLE thresh )
{
    if( ( !OriSatImg.IsValid() ) || ( !SatDom.IsValid() ) || (vecR.size() == 0) || (vecXsYsZs.size() == 0) || (vecPtXYZ.size() == 0)  )
    {
        return false;
    }
    SINT nX, nY;
    CRasterPt2D ImgSL;
    ImgSL.Initial(SatDom.GetH(),SatDom.GetW());
    DOUBLE trueline = ( pCE1IOP->l0 - pCE1IOP->nCCD_line ) * pCE1IOP->pixsize-pCE1IOP->x0;
    /**二分法计算像点坐标,默认是上行**/
    CmlCE1LinearImage ce;
    ce.mlGetCE1DOMCoordinate( OriSatImg, vecR, vecXsYsZs, pCE1IOP->f, vecPtXYZ, SatDom.GetW(), SatDom.GetH(), ImgSL, trueline, range, thresh );

    /****若下行，列号镜像*****/
    Pt2d tempXY;
    SINT nDemH = SatDom.GetH();
    SINT nDemW = SatDom.GetW();
    //上行时，将像平面坐标y值转换成列号
    if ( pCE1IOP->upflag )
    {
        for( nY = 0; nY < nDemH; nY++ )
        {
            for( nX = 0; nX < nDemW; nX++ )
            {
                tempXY = ImgSL.GetAt( nY, nX );
                //坐标超限时，赋值-1
                if( (tempXY.Y < 0) || (tempXY.Y >= OriSatImg.GetH()) )
                {
                    tempXY.X = -1;
                }
                else
                {
                    tempXY.X =  pCE1IOP->s0 + ( tempXY.X + pCE1IOP->y0 ) / pCE1IOP->pixsize ;
                }
                ImgSL.SetAt( nY, nX, tempXY );
            }
        }
    }
    //下行时，将像平面坐标y值转换成列号
    else
    {
        for( nY = 0; nY < nDemH; nY++ )
        {
            for( nX = 0; nX < nDemW; nX++ )
            {
                tempXY = ImgSL.GetAt( nY, nX );
                //坐标超限时，赋值-1
                if( ( tempXY.Y < 0 ) || ( tempXY.Y >= OriSatImg.GetH() ) )
                {
                    tempXY.X = -1;
                }
                else
                {
                    tempXY.X = pCE1IOP->s0 - ( tempXY.X + pCE1IOP->y0 ) / pCE1IOP->pixsize;
                }
                ImgSL.SetAt( nY, nX, tempXY );
            }
        }
    }
    /***双线性内插***/
    CmlFrameImage frm;
    frm.mlGrayInterpolation( &OriSatImg , &ImgSL, &SatDom, 0 );
    return true;
}
/**
* @fn mlGetCE2DOM
* @date 2011.12.07
* @author 刘一良 ylliu@irsa.ac.cn
* @brief 生成CE-2卫星影像DOM
* @param OriSatImg 原始CE-1卫星影像
* @param vecR 影像外方位旋转矩阵
* @param vecXsYsZs 影像外方位线元素
* @param pCE2IOP 内方位元素
* @param vecPtXYZ 卫星影像物方点
* @param SatDom CE-2卫星影像DOM
* @param range 外方位搜索范围
* @param thresh 计算出的x坐标与理论值之差的阈值
* @retval TRUE 成功
* @retval FALSE 失败
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlCE2LinearImage::mlGetCE2DOM( CmlRasterBlock &OriSatImg, vector<MatrixR> &vecR, vector<Pt3d> &vecXsYsZs, CE2IOPara *pCE2IOP, vector<Pt3d> &vecPtXYZ, CmlRasterBlock &SatDom, UINT range, DOUBLE thresh )
{
    if( ( !OriSatImg.IsValid() ) || ( !SatDom.IsValid() ) || (vecR.size() == 0) || (vecXsYsZs.size() == 0) || (vecPtXYZ.size() == 0)  )
    {
        return false;
    }
    SINT nX, nY;
    CRasterPt2D ImgSL;
    ImgSL.Initial(SatDom.GetH(),SatDom.GetW());
    DOUBLE trueline = pCE2IOP->x0 - tan( pCE2IOP->AngleDeg * ML_PI / 180.0 ) * pCE2IOP->f;

    /**二分法计算像点坐标,默认是上行**/
    CmlCE2LinearImage ce;
    ce.mlGetCE2DOMCoordinate( OriSatImg, vecR, vecXsYsZs, pCE2IOP->f, vecPtXYZ, SatDom.GetW(), SatDom.GetH(), ImgSL, trueline, range, thresh );

    /****若下行，列号镜像*****/
    Pt2d tempXY;
    SINT nDemH = SatDom.GetH();
    SINT nDemW = SatDom.GetW();
    //上行时，将像平面坐标y值转换成列号
    if ( pCE2IOP->upflag )
    {
        for( nY = 0; nY < nDemH; nY++ )
        {
            for( nX = 0; nX < nDemW; nX++ )
            {
                tempXY = ImgSL.GetAt( nY, nX );
                //坐标超限时，赋值-1
                if( (tempXY.Y < 0) || (tempXY.Y >= OriSatImg.GetH()) )
                {
                    tempXY.X = -1;
                }
                else
                {
                    tempXY.X = pCE2IOP->s0 + ( pCE2IOP->y0 - tempXY.X ) / pCE2IOP->pixsize;
                }
                ImgSL.SetAt( nY, nX, tempXY );
            }
        }
    }
    //下行时，将像平面坐标y值转换成列号
    else
    {
        for( nY = 0; nY < nDemH; nY++ )
        {
            for( nX = 0; nX < nDemW; nX++ )
            {
                tempXY = ImgSL.GetAt( nY, nX );
                //坐标超限时，赋值-1
                if( ( tempXY.Y < 0 ) || ( tempXY.Y >= OriSatImg.GetH() ) )
                {
                    tempXY.X = -1;
                }
                else
                {
                    tempXY.X = pCE2IOP->s0 + ( pCE2IOP->y0 + tempXY.X ) / pCE2IOP->pixsize;
                }
                ImgSL.SetAt( nY, nX, tempXY );
            }
        }
    }
    /***双线性内插***/
    CmlFrameImage frm;
    frm.mlGrayInterpolation( &OriSatImg , &ImgSL, &SatDom, 0 );
    return true;
}
