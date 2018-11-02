#ifndef __SIFTMATCH_H
#define __SIFTMATCH_H

#include <vector>
using namespace std;
///int SiftMatch(uchar* pLeft,int nWL,int nHL,int nByteL,uchar* pRight,int nWR,int nHR,int nByteR,vector<double> &vXL,vector<double> &vYL,vector<double> &vXR,vector<double> &vYR);
///extern double descr_dist_sq( struct feature* f1, struct feature* f2 );
//int SiftMatch(char** ParamList, vector<double> &vXL,vector<double> &vYL,vector<double> &vXR,vector<double> &vYR,int nMaxCheck, double dRatio);
int SiftMatch(char* pLeft,int nWL,int nHL,int nByteL,char* pRight,int nWR,int nHR,int nByteR,vector<double> &vXL,vector<double> &vYL,vector<double> &vXR,vector<double> &vYR,int nMaxCheck, double dRatio);


#endif


