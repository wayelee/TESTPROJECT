/*****************************************************************
库名称：SIFT匹配函数库
作    者：行星制图与遥感研究室(PMRS_LAB)
功能描述：两幅图像SIFT匹配，输出匹配点的像素坐标，所有图像均以左上角为原点，横向为x，纵向为y
输    入：
输    出：
版本编号:   1.0
修改历史:    <作者>    <时间>   <版本编号>    <描述>
******************************************************************/

#ifndef __MLSIFTMATCH_H
#define __MLSIFTMATCH_H

#include <vector>
using namespace std;


int mlSiftMatch(char* pLeft,int nWL,int nHL,int nByteL,char* pRight,int nWR,int nHR,int nByteR,int &nPtNum, double* &pXL,double* &pYL,double* &pXR,double* &pYR,int nMaxCheck=200, double dRatio=0.49);

/*****************************************************************
函数名称：mlSiftMatch
作    者：行星制图与遥感研究室(PMRS_LAB)
功能描述：SIFT匹配，目前只支持8bit图像，像素坐标均以左上角为原点，横向为x，向下为y
输    入：   
			pLeft:    左图像指针
			nWL:      左图像宽
			nHL:      左图像高
			nByteL;   左图像位数，8、16、24
			pRight:   右图像指针
			nWR:      右图像宽
			nHR:      右图像高
			nByteR:   右图像位数，8、16、24
            nMaxCheck:SIFT匹配参数
            dRatio:   SIFT匹配参数
输    出：
			vXL:      匹配点左图像x坐标
			vYL:      匹配点左图像y坐标
			vXR:      匹配点右图像x坐标
			vYR:      匹配点右图像y标
返回值：    int       匹配点数量
版本编号:   1.0
修改历史:    彭嫚   2011.12.17  1.0    8bit图像SIFT匹配
******************************************************************/



bool freeData( double* pData);

#endif
