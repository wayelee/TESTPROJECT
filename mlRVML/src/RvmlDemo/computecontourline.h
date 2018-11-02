#ifndef COMPUTECONTOURLINE_H
#define COMPUTECONTOURLINE_H
#include"contourline.h"
#include<vector>
#include"CmlRasterBand.h"
using namespace std;

struct temple
{
        int a;
        int b;
        int Direction;
};
struct DataDocument
{
    float ltx;
    float lty;
    float gridx;
    float gridy;
    int row;
    int col;
    float zoom;
    float zmax;
    float zmin;
    double ** matrix;
    float distant;
    vector <ContourLine*> LineSet;
};


class ComputeContourLine
{
public:
    ComputeContourLine();
    ~ComputeContourLine();
    DataDocument Doc;
    bool **flagx,**flagy;

    int GetDataInfo(CmlRasterBand* pBand,double interval);
    //高斯滤波
    void  Gaussfilter();
    void  cacculation();//计算生成等高线
    void  ScanLine(float cru_height);
    float dist(POINTNEW P1, POINTNEW P2);
    temple RecruScan(ContourLine *newline,temple tem, float h, double **mat);
    float intersectornot(int arctype,float h,int i,int j,double **mat,POINTNEW P);
    void  Integration(int count);
    void ComputeLines();
};



#endif // COMPUTECONTOURLINE_H
