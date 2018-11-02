#ifndef _RANSAC_HEADER_FILE__
#define _RANSAC_HEADER_FILE__

#include <vector>
using namespace std;


bool getRanSacPts( vector<double> vecXL, vector<double> vecYL, vector<double> vecXR, vector<double> vecYR, vector<bool> &vecFlags, double dSigma, double dMinItera = 1000 );







#endif
