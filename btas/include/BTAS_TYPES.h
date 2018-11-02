/*
    Copyright (C) 2007 BACC
*/


/* General data types used in btas2 system*/

#ifndef _BTAS_TYPES_H
#define _BTAS_TYPES_H



typedef unsigned char	  Uint8;
typedef  char	          Sint8;
typedef unsigned short	Uint16;
typedef  short	        Sint16;
typedef unsigned int	  Uint32;
typedef          int	  Sint32;

typedef enum{False,True} Bool;

/* Figure out how to support 64-bit datatypes */
#if !defined(__STRICT_ANSI__)
#if defined(__osf__) || defined(_LP64) /* Tru64 ,solaris10*/
#define BTAS_HAS_64BIT_TYPE	long
#elif defined(__GNUC__) || defined(VMS) || defined(__MWERKS__) || defined(__SUNPRO_C) || defined(__DECC)
#define BTAS_HAS_64BIT_TYPE	long long
#elif defined(_MSC_VER) /* VC++ */
#define BTAS_HAS_64BIT_TYPE	__int64
#endif
#endif /* !__STRICT_ANSI__ */


/* The 64-bit type isn't available on EPOC/Symbian OS */
#ifdef __SYMBIAN32__
#undef BTAS_HAS_64BIT_TYPE
#endif

/* The 64-bit datatype isn't supported on all platforms */
#ifdef BTAS_HAS_64BIT_TYPE
typedef unsigned BTAS_HAS_64BIT_TYPE Uint64;
typedef BTAS_HAS_64BIT_TYPE          Sint64;
#else

/* This is really just a hack to prevent the compiler from complaining */
typedef struct 
{
	Uint32 hi;
	Uint32 lo;
} Uint64, Sint64;
#endif


/* Make sure the types really have the right sizes */
#define BTAS_COMPILE_TIME_ASSERT(name, x)               \
       typedef int BTAS_dummy_ ## name[(x) * 2 - 1]


BTAS_COMPILE_TIME_ASSERT(Uint8,  sizeof(Uint8) == 1);
BTAS_COMPILE_TIME_ASSERT(Sint8,  sizeof(Sint8) == 1);
BTAS_COMPILE_TIME_ASSERT(Uint16, sizeof(Uint16) == 2);
BTAS_COMPILE_TIME_ASSERT(Sint16, sizeof(Sint16) == 2);
BTAS_COMPILE_TIME_ASSERT(Uint32, sizeof(Uint32) == 4);
BTAS_COMPILE_TIME_ASSERT(Sint32, sizeof(Sint32) == 4);
BTAS_COMPILE_TIME_ASSERT(Uint64, sizeof(Uint64) == 8);
BTAS_COMPILE_TIME_ASSERT(Sint64, sizeof(Sint64) == 8);


typedef enum 
{
	DUMMY_ENUM_VALUE
} BTAS_DUMMY_ENUM;

BTAS_COMPILE_TIME_ASSERT(enum, sizeof(BTAS_DUMMY_ENUM) == sizeof(int));

typedef float   Float32 ;

typedef double  Float64 ;



#undef BTAS_COMPILE_TIME_ASSERT

#endif
