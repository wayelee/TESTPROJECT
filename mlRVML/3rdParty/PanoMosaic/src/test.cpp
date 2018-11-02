#include <iostream>
#include "../Panorama.h"

using namespace std;

int main()
{

////////////////////////测试PanoramaMosaic

    char* OriImagePath[13];

    OriImagePath[0] = "opencv_stitching";
    OriImagePath[1] = "0.bmp";
    OriImagePath[2] = "1.bmp";
    OriImagePath[3] = "2.bmp";
    OriImagePath[4] = "3.bmp";
    OriImagePath[5] = "4.bmp";
    OriImagePath[6] = "5.bmp";
    OriImagePath[7] = "6.bmp";
    OriImagePath[8] = "7.bmp";
    OriImagePath[9] = "8.bmp";
    OriImagePath[10] = "9.bmp";

    OriImagePath[11] = "--warp";
    OriImagePath[12] = "cylindrical";
    OriImagePath[13] = "--match_conf";
    OriImagePath[14] = "0.7";

/*
    OriImagePath[0] = "opencv_stitching";
    OriImagePath[1] = "0.bmp";
    OriImagePath[2] = "1.bmp";
    OriImagePath[3] = "2.bmp";
    OriImagePath[4] = "3.bmp";
    OriImagePath[5] = "--warp";
    OriImagePath[6] = "cylindrical";
    OriImagePath[7] = "--match_conf";
    OriImagePath[8] = "0.5";
*/
//    PanoramaMosaic(9, OriImagePath);
    PanoramaMosaic(15, OriImagePath);
    cout<<"PanoramaMosaic Finished!"<<endl;





    return 0;
}
