#ifndef __SIFTMATCH_H
#define __SIFTMATCH_H

#include <vector>
using namespace std;
#if defined _WIN32 || defined _WIN64
#include "targetver.h"
#endif

////////////////////////
#if defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64
#define SIFTMATCH_CDECL __cdecl
#define SIFTMATCH_STDCALL __stdcall
#else
#define SIFTMATCH_CDECL
#define SIFTMATCH_STDCALL
#endif

#ifndef SIFTMATCH_EXTERN_C
#ifdef __cplusplus
#define SIFTMATCH_EXTERN_C extern "C"
#define SIFTMATCH_DEFAULT(val) = val
#else
#define SIFTMATCH_EXTERN_C
#define SIFTMATCH_DEFAULT(val)
#endif
#endif

#if ( defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64 ) && (defined MLSIFTMATCH_EXPORTS)
#define SIFTMATCH_EXPORTS __declspec(dllexport)
#else
#define SIFTMATCH_EXPORTS
#endif

#ifndef SIFTMATCH_API
#define SIFTMATCH_API(rettype) SIFTMATCH_EXTERN_C SIFTMATCH_EXPORTS rettype SIFTMATCH_CDECL
#endif
//------------------------------------
SIFTMATCH_API( int ) SiftMatch(char* pLeft,int nWL,int nHL,int nByteL,char* pRight,int nWR,int nHR,int nByteR,double **pdData , int nMaxCheck, double dRatio);
SIFTMATCH_API( void ) FreePtr( double **pdData );


SIFTMATCH_API( int ) SiftMatchVector(char* pLeft,int nWL,int nHL,int nByteL,char* pRight,int nWR,int nHR,int nByteR,vector<double> &vXL,vector<double> &vYL,vector<double> &vXR,vector<double> &vYR,int nMaxCheck, double dRatio);
#endif


