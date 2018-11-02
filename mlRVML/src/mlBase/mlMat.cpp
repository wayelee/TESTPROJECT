/************************************************************************
Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
文件名称:   mlMat.cpp
创建日期:   2011.11.02
作    者:   万文辉
描    述:   ml工程中矩阵运算实现
版本编号:   1.0
修改历史:    <作者>    <时间>   <版本编号>    <描述>


************************************************************************/
#include "../../include/mlBase.h"
#include "../../include/mlTypeConvert.h"

/*矩阵转置*/
ML_EXTERN_C bool mlMatTrans(  CmlMat* pMatIn, CmlMat* pMatOut )
{
    if(  false == pMatIn->IsValid() )
    {
        return false;
    }
    if( false == pMatOut->IsValid() )
    {
        pMatOut->Initial( pMatIn->GetW(), pMatIn->GetH() );
    }
    else
    {
        if( ( pMatIn->GetH() != pMatOut->GetW() )||( pMatIn->GetW() != pMatOut->GetH() ) )
        {
            return false;
        }
    }


    for( int i = 0; i < pMatOut->GetH(); ++i )
    {
        for( int j = 0; j < pMatOut->GetW(); ++j )
        {
            pMatOut->SetAt( i, j, pMatIn->GetAt( j, i ) );
        }
    }
    return true;
};

/*矩阵加法*/
ML_EXTERN_C bool mlMatAdd( CmlMat* pMatInA, CmlMat* pMatInB, CmlMat* pMatOutC )
{
    if( ( false == pMatInA->IsValid() )||( false == pMatInB->IsValid() ) )
    {
        return false;
    }
    if( false == pMatOutC->IsValid() )
    {
        pMatOutC->Initial( pMatInA->GetH(), pMatInA->GetW() );
    }
    else
    {
        if( ( pMatInA->GetH() != pMatOutC->GetH() )||( pMatInA->GetW() != pMatOutC->GetW() ) )
        {
            return false;
        }
    }

    if( ( pMatInA->GetH() != pMatInB->GetH() )||( pMatInA->GetW() != pMatInB->GetW() ) )
    {
        return false;
    };

     for( int i = 0; i < pMatOutC->GetH(); ++i )
    {
        for( int j = 0; j < pMatOutC->GetW(); ++j )
        {
            pMatOutC->SetAt( i, j, ( pMatInA->GetAt( i, j ) +  pMatInB->GetAt( i, j ) ) );
        }
    }
    return true;
};

/*矩阵减法*/
ML_EXTERN_C bool mlMatSub( CmlMat* pMatInA, CmlMat* pMatInB, CmlMat* pMatOutC )
{
    if( ( false == pMatInA->IsValid() )||( false == pMatInB->IsValid() ) )
    {
        return false;
    }
    if( false == pMatOutC->IsValid() )
    {
        pMatOutC->Initial( pMatInA->GetH(), pMatInA->GetW() );
    }
    else
    {
        if( ( pMatInA->GetH() != pMatOutC->GetH() )||( pMatInA->GetW() != pMatOutC->GetW() ) )
        {
            return false;
        }
    }

    for( int i = 0; i < pMatOutC->GetH(); ++i )
    {
        for( int j = 0; j < pMatOutC->GetW(); ++j )
        {
            pMatOutC->SetAt( i, j, ( pMatInA->GetAt( i, j ) -  pMatInB->GetAt( i, j ) ) );
        }
    }
    return true;
};

/*矩阵乘法*/
ML_EXTERN_C bool mlMatMul( CmlMat* pMatInA, CmlMat* pMatInB, CmlMat* pMatOutC )
{
    if( ( false == pMatInA->IsValid() )||( false == pMatInB->IsValid() ) )
    {
        return false;
    }
    if( pMatInA->GetW() != pMatInB->GetH() )
    {
        return false;
    };
    if( false == pMatOutC->IsValid() )
    {
        pMatOutC->Initial( pMatInA->GetH(), pMatInB->GetW() );
    }
    else
    {
        if( ( pMatInA->GetH() != pMatOutC->GetH() )||( pMatInB->GetW() != pMatOutC->GetW() ) )
        {
            return false;
        }
    }


    for( int i = 0; i < pMatOutC->GetH(); ++i )
    {
        for( int j = 0; j < pMatOutC->GetW(); ++j )
        {
            double dTmp = 0;
            for( int k = 0; k < pMatInA->GetW(); ++k )
            {
                dTmp += ( pMatInA->GetAt( i, k ) * pMatInB->GetAt( k, j ) );
            }
            pMatOutC->SetAt( i, j, dTmp );
        }
    }
    return true;
};
/*矩阵求逆*/
ML_EXTERN_C bool mlMatInv( CmlMat* pMatIn, CmlMat* pMatOut )
{
    if( ( false == pMatIn->IsValid() ) )
    {
        return false;
    }
    if( pMatIn->GetH() != pMatIn->GetW() )
    {
        return false;
    }

    if( false == pMatOut->IsValid() )
    {
        pMatOut->Initial( pMatIn->GetH(),pMatIn->GetH() );
    }
    else
    {
        if( ( pMatOut->GetH() != pMatIn->GetH() )||( pMatOut->GetW() != pMatIn->GetW() ) )
        {
            return false;
        }
    }

    CvMat* pIn = cvCreateMat( pMatIn->GetH(), pMatIn->GetW(), CV_64F );
    CvMat* pOut = cvCreateMat( pMatIn->GetH(), pMatIn->GetW(), CV_64F );

    mlMat2CvMat( pMatIn, pIn );
    cvInv( pIn, pOut, CV_QR );
    CvMat2mlMat( pOut, pMatOut );

    cvReleaseMat( &pIn );
    cvReleaseMat( &pOut );

    return true;
};

/*矩阵行列式*/
ML_EXTERN_C bool mlMatDet( CmlMat* pMat, double &dRes )
{
    if( ( false == pMat->IsValid() )||( pMat->GetH() != pMat->GetW() ) )
    {
        return false;
    }

    CvMat* pIn = cvCreateMat( pMat->GetH(), pMat->GetW(), CV_64F );

    mlMat2CvMat( pMat, pIn );
    dRes = cvmDet(pIn);

    cvReleaseMat( &pIn );
    return true;

};

/*解方程A*x=B*/
ML_EXTERN_C bool mlMatSolveSVD(  CmlMat* pMatInA,  CmlMat* pMatInB, CmlMat* pMatOutX )
{
    if( ( false == pMatInA->IsValid() )||( false == pMatInB->IsValid() )||( true == pMatOutX->IsValid() ) )
    {
        return false;
    }
    if( ( pMatInA->GetH()!= pMatInB->GetH())||( pMatInB->GetW() != 1 ) )
    {
        return false;
    }

    pMatOutX->Initial( pMatInA->GetW(), 1 );

    CvMat* pInA = cvCreateMat( pMatInA->GetH(), pMatInA->GetW(), CV_64F );
    CvMat* pInB = cvCreateMat( pMatInB->GetH(), pMatInB->GetW(), CV_64F );
    CvMat* pInX = cvCreateMat( pMatInA->GetW(), pMatInB->GetW(), CV_64F );

    mlMat2CvMat( pMatInA, pInA );
    mlMat2CvMat( pMatInB, pInB );

    cvSolve( pInA, pInB, pInX, CV_SVD );

    CvMat2mlMat( pInX, pMatOutX );

    cvReleaseMat( &pInA );
    cvReleaseMat( &pInB );
    cvReleaseMat( &pInX );

    return true;

};
/*SVD分解，A=U*W*V'*/
ML_EXTERN_C bool mlMatSVD( CmlMat* pMatA, CmlMat* pMatU, CmlMat* pMatW, CmlMat* pMatV )
{
    if( ( pMatA == NULL )||( pMatU == NULL ) || ( pMatW == NULL ) ||( pMatV == NULL ) )
    {
        return false;
    }
    if( ( pMatA->IsValid() == false )||( pMatU->IsValid() == true )||( pMatW->IsValid() == true )||( pMatV->IsValid() == true ) )
    {
        return false;
    }

    CvMat* pCvMatA = cvCreateMat( pMatA->GetH(), pMatA->GetW(), CV_64F );

    mlMat2CvMat( pMatA, pCvMatA );

    CvMat* pCvMatU = cvCreateMat( pMatA->GetH(), pMatA->GetH(), CV_64F );
    CvMat* pCvMatW = cvCreateMat( pMatA->GetH(), pMatA->GetW(), CV_64F );
    CvMat* pCvMatV = cvCreateMat( pMatA->GetW(), pMatA->GetW(), CV_64F );

//    for( int i = 0; i < pCvMatA->rows; ++i )
//    {
//        for( int j = 0; j < pCvMatA->cols; ++j  )
//        {
//            cout << cvmGet( pCvMatA, i, j ) << "  ";
//        }
//        cout << '\n';
//    }

    cvSVD( pCvMatA, pCvMatW, pCvMatU, pCvMatV );

    if( ( pMatU->IsValid() == true )||( pMatW->IsValid() == true )||( pMatV->IsValid() == true ) )
    {
        return false;
    }
    pMatU->Initial( pMatA->GetH(), pMatA->GetH() );
    pMatW->Initial( pMatA->GetH(), pMatA->GetW() );
    pMatV->Initial( pMatA->GetW(), pMatA->GetW() );

    CvMat2mlMat( pCvMatU, pMatU );
    CvMat2mlMat( pCvMatW, pMatW );
    CvMat2mlMat( pCvMatV, pMatV );

    cvReleaseMat( &pCvMatA );
    cvReleaseMat( &pCvMatU );
    cvReleaseMat( &pCvMatW );
    cvReleaseMat( &pCvMatV );

    return true;
}

/*三维向量叉乘*/
ML_EXTERN_C bool mlMatCross3(CmlMat* pMatInA , CmlMat* pMatInB , CmlMat* pMatOutC)
{
     if( ( false == pMatInA->IsValid() )||( false == pMatInB->IsValid() ) )
    {
        return false;
    }
    if( (pMatInA->GetW() != 1) || (pMatInB->GetW() != 1) || (pMatInA->GetH() != 3) || (pMatInB->GetH() != 3) )
    {
        return false;
    }
    if( false == pMatOutC->IsValid() )
    {
        pMatOutC->Initial( 3, 3 );
    }
    else
    {
        if( ( 3 != pMatOutC->GetH() ) || ( 3 != pMatOutC->GetW() ) )
        {
            return false;
        }
    }
    //(b1c2-b2c1,c1a2-a1c2,a1b2-a2b1)
    pMatOutC->SetAt( 0, 0, pMatInA->GetAt(1, 0) * pMatInB->GetAt(2, 0) - pMatInB->GetAt(1, 0) * pMatInA->GetAt(2, 0) );
    pMatOutC->SetAt( 1, 0, pMatInA->GetAt(2, 0) * pMatInB->GetAt(0, 0) - pMatInA->GetAt(0, 0) * pMatInB->GetAt(2, 0) );
    pMatOutC->SetAt( 2, 0, pMatInA->GetAt(0, 0) * pMatInB->GetAt(1, 0) - pMatInB->GetAt(0, 0) * pMatInA->GetAt(1, 0) );

    return true;

}







