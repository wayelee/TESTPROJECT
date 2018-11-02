#ifndef _RANSAC_HEADER_FILE__
#define _RANSAC_HEADER_FILE__

#include <vector>
using namespace std;
#if defined _WIN32 || defined _WIN64
#include "targetver.h"
#endif

////////////////////////
#if defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64
#define RANSAC_CDECL __cdecl
#define RANSAC_STDCALL __stdcall
#else
#define RANSAC_CDECL
#define RANSAC_STDCALL
#endif

#ifndef RANSAC_EXTERN_C
#ifdef __cplusplus
#define RANSAC_EXTERN_C extern "C"
#define RANSAC_DEFAULT(val) = val
#else
#define RANSAC_EXTERN_C
#define RANSAC_DEFAULT(val)
#endif
#endif

#if ( defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64 ) && (defined MLRANSAC_EXPORTS)
#define RANSAC_EXPORTS __declspec(dllexport)
#else
#define RANSAC_EXPORTS
#endif

#ifndef RANSAC_API
#define RANSAC_API(rettype) RANSAC_EXTERN_C RANSAC_EXPORTS rettype RANSAC_CDECL
#endif
//------------------------------------


RANSAC_API( bool ) getRanSacPts( vector<double> vecXL, vector<double> vecYL, vector<double> vecXR, vector<double> vecYR, vector<bool> &vecFlags, double dSigma, double dMinItera = 1000 );

RANSAC_API( int ) getRanSacPtsVT( double* pPtData , int nPtNum, bool** pbData, double dSigma, double dMinItera = 1000 );

RANSAC_API( void ) FreeRansacPtr( bool **pbData );



#endif
