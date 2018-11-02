#ifndef CONTOURLINE_H
#define CONTOURLINE_H
#include <vector>
using namespace std;

struct POINTNEW
{
        float x;
        float y;
};
class ContourLine
{
public:
    ContourLine();
    ~ContourLine();
    float height;
    vector <POINTNEW> m_Points;

};

#endif // CONTOURLINE_H
