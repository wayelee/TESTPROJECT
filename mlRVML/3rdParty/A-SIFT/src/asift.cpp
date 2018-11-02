#include "ASift.h"

#include "demo_lib_sift.h"
#include "compute_asift_keypoints.h"
#include "compute_asift_matches.h"
#include "io_png/io_png.h"
int ASiftMatch( unsigned char* pL, int nLW, int nLH, unsigned char* pR, int nRW, int nRH, double* &pLx, double* &pLy,double* &pRx,double* &pRy, int nNumTilts  )
{
	int verb = 0;
	siftPar siftPara;
	default_sift_parameters(siftPara);

	int nTSizeL = nLH * nLW;
	int nTSizeR = nRH * nRW;

    float* pImgL = new float[nTSizeL];
    float* pImgR = new float[nTSizeR];

    for( int i = 0; i < nTSizeL; ++i )
    {
        pImgL[i] = (float)(pL[i]);
    }
    for( int i = 0; i < nTSizeR; ++i )
    {
        pImgR[i] = (float)(pR[i]);
    }
	vector<float> vecLImg( pImgL, pImgL+nTSizeL );
	vector<float> vecRImg( pImgR, pImgR+nTSizeR );
    delete[] pImgL;
    delete[] pImgR;

//////////////////////////////////////////////////////
    int nL = vecLImg.size();
    int nR = vecRImg.size();

//    for( int i = 0; i < vecLImg.size(); ++i )
//    {
//        cout << vecRImg[i] << "  ";
//    }
////////////////////////////////////////////////////////
	vector< vector< keypointslist > > keys1;
	vector< vector< keypointslist > > keys2;

	int nKey1 = compute_asift_keypoints( vecLImg, nLW, nLH, nNumTilts, verb, keys1, siftPara );
 	int nKey2 = compute_asift_keypoints( vecRImg, nRW, nRH, nNumTilts, verb, keys2, siftPara );

	matchingslist matchings;

	int nMatchs = compute_asift_matches(nNumTilts, nNumTilts, nLW, nLH, nRW,
		nRH, verb, keys1, keys2, matchings, siftPara );


    if( ( pLx != NULL )||( pLy != NULL )||( pRx != NULL )||( pRy != NULL ) )
    {
        return 0;
    }
    else
    {
        pLx = new double[nMatchs];
        pLy = new double[nMatchs];
        pRx = new double[nMatchs];
        pRy = new double[nMatchs];
    }

	for ( int i = 0; i < nMatchs; ++i )
	{
		pLx[i] = matchings[i].first.x;
		pLy[i] = matchings[i].first.y;
		pRx[i] = matchings[i].second.x;
		pRy[i] = matchings[i].second.y;
	}

	//////////////////////////
//    int band_w = 20; // insert a black band of width band_w between the two images for better visibility
//    int w1, w2, h1, h2;
//    w1 = nLW;
//    w2 = nRW;
//    h1 = nLH;
//    h2 = nRH;
//
//	int wo =  MAX(w1,w2);
//	int ho = h1+h2+band_w;
//	int zoom1, zoom2;
//	zoom1 = zoom2 = 1;
//
//	float *opixelsASIFT = new float[wo*ho];
//
//	for(int j = 0; j < (int) ho; j++)
//		for(int i = 0; i < (int) wo; i++)  opixelsASIFT[j*wo+i] = 255;
//
//	/////////////////////////////////////////////////////////////////// Copy both images to output
//	for(int j = 0; j < (int) h1; j++)
//		for(int i = 0; i < (int) w1; i++)  opixelsASIFT[j*wo+i] = vecLImg[j*w1+i];
//
//	for(int j = 0; j < (int) h2; j++)
//		for(int i = 0; i < (int) (int)w2; i++)  opixelsASIFT[(h1 + band_w + j)*wo + i] = vecRImg[j*w2 + i];
//
//	//////////////////////////////////////////////////////////////////// Draw matches
//	matchingslist::iterator ptr = matchings.begin();
//	for(int i=0; i < (int) matchings.size(); i++, ptr++)
//	{
//		draw_line(opixelsASIFT, (int) (zoom1*ptr->first.x), (int) (zoom1*ptr->first.y),
//				  (int) (zoom2*ptr->second.x), (int) (zoom2*ptr->second.y) + h1 + band_w, 255.0f, wo, ho);
//	}
//
//	///////////////////////////////////////////////////////////////// Save imgOut
//	write_png_f32( "/home/wan/pdfDoc/t.bmp", opixelsASIFT, wo, ho, 1);
//
//	delete[] opixelsASIFT; /*memcheck*/

	/////////////////////////

	return nMatchs;
};
bool freeASiftData( double* &pData )
{
    if( pData != NULL )
    {
        delete[] pData;
        pData = NULL;
    }
    return true;
};
