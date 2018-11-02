/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlCoordTrans.cpp
* @date 2011.11.18
* @author 张重阳
* @brief 坐标转换算法类实现文件
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*/
#include "mlCoordTrans.h"
/**
 *@fn CmlCoordTrans()
 *@date 2011.11
 *@author 张重阳
 *@brief 坐标转换类构造函数
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
*/
CmlCoordTrans::CmlCoordTrans()
{
    //ctor
}


/**
 *@fn CmlCoordTrans()
 *@date 2011.11
 *@author 张重阳
 *@brief 坐标转换类析构函数
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因> \n
*/
CmlCoordTrans::~CmlCoordTrans()
{
    //dtor
}


/**
 *@fn mlCoordTransResult
 *@date 2012.02
 *@author 张重阳
 *@brief 根据给定的旋转矩阵和平移向量求解转换后的坐标
 *@param pArr 传入坐标数组指针
 *@param nDim 坐标维数
 *@param pRotateMat 旋转矩阵指针
 *@param pTransVec 平移向量指针
 *@param pTransResult 坐标转换后结果
 *@param nflag 转换状态参数 默认为0
         对于转换关系 B = R*A + T (R为旋转矩阵，T为平移向量)
         当nflag=0时，表示由A求B；
         当nflag为其他值，表示由B求A
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/

bool CmlCoordTrans::mlCoordTransResult(DOUBLE* pArr, SINT nDim, CmlMat* pRotateMat, DOUBLE* pTransVec, DOUBLE* pTransResult,SINT nflag)
{
    if(nDim != SINT(pRotateMat->GetH())  && nDim != SINT(pRotateMat->GetW()))          //判断矩阵是否为方阵
    {
//        cout << "coordinates dimension and matrix doesn't match!\n" << endl;
//        SCHAR strErr[] = "mlCamCalib error : iteration fails to converge within the threshhold \t" ;
//        LOGAddErrorMsg(strErr) ;
        SCHAR strErr[] = "coordinates dimension and matrix doesn't match!\n";
        LOGAddErrorMsg(strErr);
        return false;

    }
    else
    {
       // CmlMat cordinateMat, resultMat;
        CmlMat matCoordinates;
        CmlMat matResult;
        matCoordinates.Initial(nDim, 1);
        matResult.Initial(nDim,1);
        for(SINT i = 0; i < nDim; i++)
        {
            matCoordinates.SetAt(i,0,*(pArr + i));

        }
        if(nflag == 0)               // B = R*A + T  由A求B
        {
            mlMatMul(pRotateMat, &matCoordinates,&matResult);
            for(SINT i = 0; i < nDim; i++)
            {
                *(pTransResult + i) = matResult.GetAt(i,0) + *(pTransVec + i);
            }

        }
        else               // B = R*A + T  由B求A
        {
            CmlMat matsub,matTransVec,matRInv;
            matTransVec.Initial(nDim,1);
            for(SINT i = 0; i < nDim; i++)
            {
                matTransVec.SetAt(i,0,*(pTransVec+i));

            }
            mlMatSub(&matCoordinates,&matTransVec,&matsub);
            mlMatInv(pRotateMat,&matRInv);      //矩阵求逆
            mlMatMul(&matRInv,&matsub,&matResult);
            for(SINT j = 0; j < nDim; j++)
            {
                *(pTransResult+j) = matResult.GetAt(j,0);
            }
        }

//        LOGAddSuccessQuitMsg();
        return true;
    }

}


/**
 *@fn mlCalcTransMatrixByXYZ
 *@date 2011.11
 *@author 张重阳
 *@brief 本函数实现月固系到局部坐标系转换关系求解
 *@param pLocResult 着陆点在月固系下得精确定位结果
 *@param pTransMat  存储转换的旋转矩阵
 *@param pTransVec  存储平移向量
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/


bool CmlCoordTrans::mlCalcTransMatrixByXYZ(Pt3d* pLocResult,CmlMat* pTransMat, DOUBLE* pTransVec)
{
	if ( false == pTransMat->IsValid())
	{
      //空间坐标转换到经纬度
        DOUBLE dfai = 0.0;         //纬度
        DOUBLE dL = 0.0;
        pTransMat->Initial(3,3);
        DOUBLE dR = sqrt( (pLocResult->X) * (pLocResult->X)
                + (pLocResult->Y)*(pLocResult->Y) + (pLocResult->Z)*(pLocResult->Z) );

        if (pLocResult->X == 0 && pLocResult->Z ==0 )    //着陆点X=0时的特殊情况  pTransMat表示已知局部坐标求解月固系下坐标的矩阵
        {
            if(pLocResult->Y > 0)    //东经90度
            {
                dfai = Deg2Rad(0);
                dL = Deg2Rad(90.0);
            }
            else                       //pLocResult->Y < 0 西经
            {
                dfai = Deg2Rad(0.0);
                dL = Deg2Rad(-90.0);
            }
        }
        else
        {
          //空间坐标转换到经纬度
             dfai = asin(pLocResult->Z/dR);          //纬度
             dL = atan(pLocResult->Y / pLocResult->X);  //经度
        }

        //旋转矩阵计算
        pTransMat->SetAt(0,0,-cos(dL) * sin(dfai));
        pTransMat->SetAt(0,1,-sin(dL));
        pTransMat->SetAt(0,2,-cos(dL) * cos(dfai));
        pTransMat->SetAt(1,0,-sin(dL) * sin(dfai));
        pTransMat->SetAt(1,1,cos(dL));
        pTransMat->SetAt(1,2,-sin(dL) * cos(dfai));
        pTransMat->SetAt(2,0,cos(dfai));
        pTransMat->SetAt(2,1,0);
        pTransMat->SetAt(2,2,-sin(dfai));

        //平移量计算
        *pTransVec = pLocResult->X;
        *(pTransVec + 1) = pLocResult->Y;
        *(pTransVec + 2) = pLocResult->Z;

        LOGAddSuccessQuitMsg();
        return true;
    }
	else
	{
	  //  LOGAddNoticeMsg( pLog, "s" );
        SCHAR strMsg[] = "Error in pTransMat!\n";
        LOGAddErrorMsg(strMsg);
        return false;
	}
}

/**
 *@fn mlCalcTransMatrixByLatLong
 *@date 2011.11
 *@author 张重阳
 *@brief 本函数根据定位的经纬度实现月固系到局部坐标系转换关系求解
 *@param dLat 定位的纬度  单位为度   范围为-90度～90度  北纬为正 南纬为负
 *@param dLong 定位的经度 单位为度   范围为-180-180度  东经为正 西经为负
 *@param pTransMat  存储转换的旋转矩阵
 *@param pTransVec  存储平移向量
 *@retval TRUE 成功
 *@retval FALSE 失败
 *@version 1.0
 *@par 修改历史：<作者>    <时间>   <版本编号>    <修改原因>\n

*/
bool CmlCoordTrans::mlCalcTransMatrixByLatLong(double dLat,double dLong,CmlMat* pTransMat, DOUBLE* pTransVec)
{
    double dfai = Deg2Rad(dLat);  //纬度弧度；
    double dL = Deg2Rad(dLong);   //经度弧度
    pTransMat->Initial(3,3);

    pTransMat->SetAt(0,0,-cos(dL) * sin(dfai));
    pTransMat->SetAt(0,1,-sin(dL));
    pTransMat->SetAt(0,2,-cos(dL) * cos(dfai));
    pTransMat->SetAt(1,0,-sin(dL) * sin(dfai));
    pTransMat->SetAt(1,1,cos(dL));
    pTransMat->SetAt(1,2,-sin(dL) * cos(dfai));
    pTransMat->SetAt(2,0,cos(dfai));
    pTransMat->SetAt(2,1,0);
    pTransMat->SetAt(2,2,-sin(dfai));

    double X,Y,Z;           //根据经纬度计算X Y Z
    X = ML_MoonRadius*cos(dfai)*cos(dL);
    Y = ML_MoonRadius*cos(dfai)*sin(dL);
    Z = ML_MoonRadius*sin(dfai);

    *pTransVec = X;
    *(pTransVec+1) = Y;
    *(pTransVec+2) = Z;

    LOGAddSuccessQuitMsg();
    return true;


}




