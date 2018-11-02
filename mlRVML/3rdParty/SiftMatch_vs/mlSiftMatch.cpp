#include <stdio.h>
#include <vector>
#include "include/mlSiftMatch.h"

#include "sift.h"
extern "C"
{

    #include "kdtree.h"
    #include "imgfeatures.h"

}

#include "utils.h"
#include "xform.h"


#include <cv.h>
#include <cxcore.h>
#include <highgui.h>
//release版本时设置迭代器检查参数为0
#define _SECURE_SCL 0
using namespace std;


int mlSiftMatch(char* pLeft,int nWL,int nHL,int nByteL,char* pRight,int nWR,int nHR,int nByteR,int &nPtNum, double* &pXL,double* &pYL,double* &pXR,double* &pYR,int nMaxCheck, double dRatio)
{
    //将内存块转成IplImage
    IplImage* img1, * img2;
    //img1 = cvLoadImage( ParamList[1] , 1 );
    //img2 = cvLoadImage( ParamList[2] , 1 );

    int KDTREE_BBF_MAX_NN_CHKS = nMaxCheck;
    double NN_SQ_DIST_RATIO_THR = dRatio;

    CvSize sl;
    sl.height = nHL;
    sl.width = nWL;

    img1 = cvCreateImage( sl, IPL_DEPTH_8U, 1 );
    img1->origin = 0; //设置为顶左结构

    if( 0 == (img1->width % 4 ) ) //为列宽四的倍数，则图像中不需要字节补齐
    {
        memcpy((void*)(img1->imageData ), (void*)(pLeft), nWL*nHL);
    }
    else//不为列宽四的倍数，则图像中存在字节补齐，转换时需要去除
    {
        for( int i = 0; i < nHL; ++i )
        {
            for( int j = 0; j < nWL; ++j )
            {
                memcpy( (void*)(img1->imageData + i*img1->widthStep ), (void*)(pLeft+ i*nWL), nWL);
            }
        }
    }

    CvSize sr;
    sr.height = nHR;
    sr.width = nWR;

    img2 = cvCreateImage( sr, IPL_DEPTH_8U, 1 );
    img2->origin = 0; //设置为顶左结构

    if( 0 == (img2->width % 4 ) ) //为列宽四的倍数，则图像中不需要字节补齐
    {
        memcpy(  (void*)(img2->imageData ), (void*)(pRight), nWR*nHR);
    }
    else//不为列宽四的倍数，则图像中存在字节补齐，转换时需要去除
    {
        for( int i = 0; i < nHR; ++i )
        {
            for( int j = 0; j < nWR; ++j )
            {
                memcpy( (void*)(img2->imageData + i*img2->widthStep ), (void*)(pRight+ i*nWR), nWR);
            }
        }
    }

  struct feature* feat1, * feat2, * feat;
  struct feature** nbrs;
  struct kd_node* kd_root;
  CvPoint pt1, pt2;
  double d0, d1;
  int n1, n2, k, i, m = 0;


  n1 = sift_features( img1, &feat1 );
  n2 = sift_features( img2, &feat2 );
  kd_root = kdtree_build( feat2, n2 );

  vector<double> vXL;
  vector<double> vYL;
  vector<double> vXR;
  vector<double> vYR;

  for( i = 0; i < n1; i++ )
    {
      feat = feat1 + i;
      k = kdtree_bbf_knn( kd_root, feat, 2, &nbrs, KDTREE_BBF_MAX_NN_CHKS );
      if( k == 2 )
	{
	  d0 = descr_dist_sq( feat, nbrs[0] );
	  d1 = descr_dist_sq( feat, nbrs[1] );
	  if( d0 < d1 * NN_SQ_DIST_RATIO_THR )
	    {
	      pt1 = cvPoint( cvRound( feat->x ), cvRound( feat->y ) );
	      pt2 = cvPoint( cvRound( nbrs[0]->x ), cvRound( nbrs[0]->y ) );
          vXL.push_back((double)pt1.x);
          vYL.push_back((double)pt1.y);
          vXR.push_back((double)pt2.x);
          vYR.push_back((double)pt2.y);
	      m++;
	      feat1[i].fwd_match = nbrs[0];
	    }
	}
      free( nbrs );
    }


  cvReleaseImage( &img1 );
  cvReleaseImage( &img2 );
  kdtree_release( kd_root );
  free( feat1 );
  free( feat2 );

  nPtNum = m;
  if( pXL != NULL )
  {
	  pXL = new double[m];
  }
  if( pYL != NULL )
  {
	  pYL = new double[m];
  }
  if( pXR != NULL )
  {
	  pXR = new double[m];
  }
  if( pYR != NULL )
  {
	  pYR = new double[m];
  }
  for( int i = 0; i < vXL.size(); ++i )
  {
	  pXL[i] = vXL[i];
	  pYL[i] = vYL[i];
	  pXR[i] = vXR[i];
	  pYR[i] = vYR[i];
  }
  return m;
}
bool freeData( double* pData)
{
	if( pData != NULL )
	{
		delete[] pData;
		return true;
	}
	else
	{
		return false;
	}
}