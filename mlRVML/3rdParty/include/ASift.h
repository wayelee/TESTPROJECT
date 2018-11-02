#ifndef CASIFT_H
#define CASIFT_H

#include <string>
#include <vector>

using namespace std;
#if defined _WIN32 || defined _WIN64
#include "targetver.h"
#endif

////////////////////////
#if defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64
#define ASIFT_CDECL __cdecl
#define ASIFT_STDCALL __stdcall
#else
#define ASIFT_CDECL
#define ASIFT_STDCALL
#endif

#ifndef ASIFT_EXTERN_C
#ifdef __cplusplus
#define ASIFT_EXTERN_C extern "C"
#define ASIFT_DEFAULT(val) = val
#else
#define ASIFT_EXTERN_C
#define ASIFT_DEFAULT(val)
#endif
#endif

#if ( defined WIN32 || defined _WIN32 || defined WIN64 || defined _WIN64 ) && (defined MLASIFT_EXPORTS)
#define ASIFT_EXPORTS __declspec(dllexport)
#else
#define ASIFT_EXPORTS
#endif

#ifndef ASIFT_API
#define ASIFT_API(rettype) ASIFT_EXTERN_C ASIFT_EXPORTS rettype ASIFT_CDECL
#endif

ASIFT_API(int) ASiftMatch( unsigned char* pL, int nLW, int nLH, unsigned char* pR, int nRW, int nRH, double* &pLx, double* &pLy,double* &pRx,double* &pRy, int nNumTilts  );

ASIFT_API(bool) freeASiftData( double* &pData );

#endif // CASIFT_H
