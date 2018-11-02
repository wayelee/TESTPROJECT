/*M///////////////////////////////////////////////////////////////////////////////////////
//
//  IMPORTANT: READ BEFORE DOWNLOADING, COPYING, INSTALLING OR USING.
//
//  By downloading, copying, installing or using the software you agree to this license.
//  If you do not agree to this license, do not download, install,
//  copy or use the software.
//
//
//                          License Agreement
//                For Open Source Computer Vision Library
//
// Copyright (C) 2000-2008, Intel Corporation, all rights reserved.
// Copyright (C) 2009, Willow Garage Inc., all rights reserved.
// Third party copyrights are property of their respective owners.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
//   * Redistribution's of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//
//   * Redistribution's in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//
//   * The name of the copyright holders may not be used to endorse or promote products
//     derived from this software without specific prior written permission.
//
// This software is provided by the copyright holders and contributors "as is" and
// any express or implied warranties, including, but not limited to, the implied
// warranties of merchantability and fitness for a particular purpose are disclaimed.
// In no event shall the Intel Corporation or contributors be liable for any direct,
// indirect, incidental, special, exemplary, or consequential damages
// (including, but not limited to, procurement of substitute goods or services;
// loss of use, data, or profits; or business interruption) however caused
// and on any theory of liability, whether in contract, strict liability,
// or tort (including negligence or otherwise) arising in any way out of
// the use of this software, even if advised of the possibility of such damage.
//
//M*/

// We follow to methods described in these two papers:
// 1) Construction of panoramic mosaics with global and local alignment.
//    Heung-Yeung Shum and Richard Szeliski. 2000.
// 2) Eliminating Ghosting and Exposure Artifacts in Image Mosaics.
//    Matthew Uyttendaele, Ashley Eden and Richard Szeliski. 2001.
// 3) Automatic Panoramic Image Stitching using Invariant Features.
//    Matthew Brown and David G. Lowe. 2007.

#include "precomp.hpp"
#include "util.hpp"
#include "warpers.hpp"
#include "blenders.hpp"
#include "seam_finders.hpp"
#include "motion_estimators.hpp"
#include "exposure_compensate.hpp"
#include "Panorama.h"
#include "panodata.h"
#include "matchers.hpp"
#include "SiftMatch.h"
#include "fstream"

#define ML_PI   3.1415926535897932384626433832795

using namespace std;
using namespace cv;

bool testPano()
{
	return true;
}

void printUsage()
{
	cout <<
		"Rotation model images stitcher.\n\n"
		"opencv_stitching img1 img2 [...imgN] [flags]\n\n"
		"Flags:\n"
		"  --preview\n"
		"      Run stitching in the preview mode. Works faster than usual mode,\n"
		"      but output image will have lower resolution.\n"
		"  --try_gpu (yes|no)\n"
		"      Try to use GPU. The default value is 'no'. All default values\n"
		"      are for CPU mode.\n"
		"\nMotion Estimation Flags:\n"
		"  --work_megapix <float>\n"
		"      Resolution for image registration step. The default is 0.6 Mpx.\n"
		"  --match_conf <float>\n"
		"      Confidence for feature matching step. The default is 0.65.\n"
		"  --conf_thresh <float>\n"
		"      Threshold for two images are from the same panorama confidence.\n"
		"      The default is 1.0.\n"
		"  --ba (ray|focal_ray)\n"
		"      Bundle adjustment cost function. The default is 'focal_ray'.\n"
		"  --wave_correct (no|yes)\n"
		"      Perform wave effect correction. The default is 'yes'.\n"
		"\nCompositing Flags:\n"
		"  --warp (plane|cylindrical|spherical)\n"
		"      Warp surface type. The default is 'spherical'.\n"
		"  --seam_megapix <float>\n"
		"      Resolution for seam estimation step. The default is 0.1 Mpx.\n"
		"  --seam (no|voronoi|gc_color|gc_colorgrad)\n"
		"      Seam estimation method. The default is 'gc_color'.\n"
		"  --compose_megapix <float>\n"
		"      Resolution for compositing step. Use -1 for original resolution.\n"
		"      The default is -1.\n"
		"  --expos_comp (no|gain|gain_blocks)\n"
		"      Exposure compensation method. The default is 'gain_blocks'.\n"
		"  --blend (no|feather|multiband)\n"
		"      Blending method. The default is 'multiband'.\n"
		"  --blend_strength <float>\n"
		"      Blending strength from [0,100] range. The default is 5.\n"
		"  --output <result_img>\n"
		"      The default is 'result.png'.\n";
}


//vector<string> img_names;
int nBrightAdjust = 0;
int nContrastAdjust = 0;
bool bBrightContrastAdjust = false;
bool preview = false;
bool try_gpu = false;
double work_megapix = 0.8/*0.6*/;
double seam_megapix = 0.1;
double compose_megapix = -1;
int ba_space = BundleAdjuster::/*RAY_SPACE*/FOCAL_RAY_SPACE;
float conf_thresh = 1.f;
bool wave_correct = true;
int warp_type = Warper::SPHERICAL;
int expos_comp_type = ExposureCompensator::GAIN_BLOCKS;
float match_conf = 0.65f;
int seam_find_type = SeamFinder::GC_COLOR;
int blend_type = Blender::MULTI_BAND;
float blend_strength = 5;
string result_name = "result.png";




int parseCmdArgs(vector<char*> &PanoramaMosaicParam)
{

	if(PanoramaMosaicParam.size() == 1)
	{
		printUsage();
		return -1;
	}

	for (size_t i = 1; i < PanoramaMosaicParam.size(); ++i)
	{
		if (string(PanoramaMosaicParam[i]) == "--help" || string(PanoramaMosaicParam[i]) == "/?")
		{
			printUsage();
			return -1;
		}
		else if (string(PanoramaMosaicParam[i]) == "--preview")
		{
			preview = true;
		}
		else if (string(PanoramaMosaicParam[i]) == "--brightness_adjust")
		{
			nBrightAdjust = atof(PanoramaMosaicParam[i + 1]);
		}
		else if (string(PanoramaMosaicParam[i]) == "--contrast_adjust")
		{
			nContrastAdjust = atof(PanoramaMosaicParam[i + 1]);
		}
		else if (string(PanoramaMosaicParam[i]) == "--try_gpu")
		{
			if (string(PanoramaMosaicParam[i + 1]) == "no")
				try_gpu = false;
			else if (string(PanoramaMosaicParam[i + 1]) == "yes")
				try_gpu = true;
			else
			{
				cout << "Bad --try_gpu flag value\n";
				return -1;
			}
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--work_megapix")
		{
			work_megapix = atof(PanoramaMosaicParam[i + 1]);
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--seam_megapix")
		{
			seam_megapix = atof(PanoramaMosaicParam[i + 1]);
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--compose_megapix")
		{
			compose_megapix = atof(PanoramaMosaicParam[i + 1]);
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--result")
		{
			result_name = PanoramaMosaicParam[i + 1];
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--match_conf")
		{
			match_conf = static_cast<float>(atof(PanoramaMosaicParam[i + 1]));
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--ba")
		{
			if (string(PanoramaMosaicParam[i + 1]) == "ray")
				ba_space = BundleAdjuster::RAY_SPACE;
			else if (string(PanoramaMosaicParam[i + 1]) == "focal_ray")
				ba_space = BundleAdjuster::FOCAL_RAY_SPACE;
			else
			{
				cout << "Bad bundle adjustment space\n";
				return -1;
			}
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--conf_thresh")
		{
			conf_thresh = static_cast<float>(atof(PanoramaMosaicParam[i + 1]));
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--wave_correct")
		{
			if (string(PanoramaMosaicParam[i + 1]) == "no")
				wave_correct = false;
			else if (string(PanoramaMosaicParam[i + 1]) == "yes")
				wave_correct = true;
			else
			{
				cout << "Bad --wave_correct flag value\n";
				return -1;
			}
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--warp")
		{
			if (string(PanoramaMosaicParam[i + 1]) == "plane")
				warp_type = Warper::PLANE;
			else if (string(PanoramaMosaicParam[i + 1]) == "cylindrical")
				warp_type = Warper::CYLINDRICAL;
			else if (string(PanoramaMosaicParam[i + 1]) == "spherical")
				warp_type = Warper::SPHERICAL;
			else
			{
				cout << "Bad warping method\n";
				return -1;
			}
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--expos_comp")
		{
			if (string(PanoramaMosaicParam[i + 1]) == "no")
				expos_comp_type = ExposureCompensator::NO;
			else if (string(PanoramaMosaicParam[i + 1]) == "gain")
				expos_comp_type = ExposureCompensator::GAIN;
			else if (string(PanoramaMosaicParam[i + 1]) == "gain_blocks")
				expos_comp_type = ExposureCompensator::GAIN_BLOCKS;
			else
			{
				cout << "Bad exposure compensation method\n";
				return -1;
			}
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--seam")
		{
			if (string(PanoramaMosaicParam[i + 1]) == "no")
				seam_find_type = SeamFinder::NO;
			else if (string(PanoramaMosaicParam[i + 1]) == "voronoi")
				seam_find_type = SeamFinder::VORONOI;
			else if (string(PanoramaMosaicParam[i + 1]) == "gc_color")
				seam_find_type = SeamFinder::GC_COLOR;
			else if (string(PanoramaMosaicParam[i + 1]) == "gc_colorgrad")
				seam_find_type = SeamFinder::GC_COLOR_GRAD;
			else
			{
				cout << "Bad seam finding method\n";
				return -1;
			}
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--blend")
		{
			if (string(PanoramaMosaicParam[i + 1]) == "no")
				blend_type = Blender::NO;
			else if (string(PanoramaMosaicParam[i + 1]) == "feather")
				blend_type = Blender::FEATHER;
			else if (string(PanoramaMosaicParam[i + 1]) == "multiband")
				blend_type = Blender::MULTI_BAND;
			else
			{
				cout << "Bad blending method\n";
				return -1;
			}
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--blend_strength")
		{
			blend_strength = static_cast<float>(atof(PanoramaMosaicParam[i + 1]));
			i++;
		}
		else if (string(PanoramaMosaicParam[i]) == "--output")
		{
			result_name = PanoramaMosaicParam[i + 1];
			i++;
		}
		//        else
		//            img_names.push_back(PanoramaMosaicParam[i]);
	}
	if (preview)
	{
		compose_megapix = 0.6;
	}
	return 0;
}

////    根据外方位置平地平线
////    vector<cv::Mat> vecInputMat;
////    int nRotAngNo = 5;
////    for(int i = 0; i < cImageList.size(); i++)
////    {
////        cv::Mat mImg;
////        cv::Mat mDestImg;
////        cv::Mat mAffineRotmat;
////
////        int nDestImgW;
////        int nDestImgH;
////
////        double dRotAngDeg;
////        double dRotAngRad;
////
////        Point2f ptCenter;
////        mImg = imread(img_names[i]);
////
////        ptCenter.x = mImg.cols/2;
////        ptCenter.y = mImg.rows/2;
////        dRotAngDeg = dExtParam[nRotAngNo] + 180;
////        dRotAngRad = dRotAngDeg / 180.0 * ML_PI;
////
////        nDestImgW = (int)(MAX(abs(mImg.cols * cos(dRotAngRad) - mImg.rows * sin(dRotAngRad)), abs(mImg.cols * cos(dRotAngRad) + mImg.rows * sin(dRotAngRad))));
////        nDestImgH = (int)(MAX(abs(mImg.cols * sin(dRotAngRad) - mImg.rows * cos(dRotAngRad)), abs(mImg.cols * sin(dRotAngRad) + mImg.rows * cos(dRotAngRad))));
////
////        mAffineRotmat = getRotationMatrix2D(ptCenter, dRotAngDeg, 1);
////
////        int nOffWidth = (nDestImgW - (int)mImg.cols) / 2;
////        int nOffHeigh = (nDestImgH - (int)mImg.rows) / 2;
////
////        mAffineRotmat.at<double>(0, 2) += nOffWidth;
////        mAffineRotmat.at<double>(1, 2) += nOffHeigh;
////
////        warpAffine(mImg, mDestImg, mAffineRotmat, Size(nDestImgW, nDestImgH));
////        //imwrite("rotated.jpg", mDestImg);
////
////        nRotAngNo += 6;
////
////    }


bool sortMintoMax(const MatchPt & mpt1, const MatchPt & mpt2)
{
	return mpt1.y < mpt2.y;
}


//确定原始图像相对位置函数--四邻域
void CalNearPairsFour(int nLoopImgNum, int nLoopNum, vector< pair<int, int> > &vecNearPairs)
{
	int nImgNum = nLoopImgNum * nLoopNum;//总共的图像数

	vector< pair<int,int> > near_pairs_tmp;//具有重复的匹配对，如1-2，2-1
	//    vector< pair<int,int> > near_pairs;//去掉重复后的匹配对

	//确定原始图像顺序
	for(int i = 1; i <= nImgNum; ++i)
		///for(int i = 0; i < nImgNum; i++)
	{
		int nLoopID = (int)(i / (nLoopImgNum+0.5)) + 1;

		if((i%nLoopImgNum != 0)&&(i%nLoopImgNum != 1))//中间部分的图像
		{
			for(int a = i-nLoopImgNum; a <= i-nLoopImgNum; a++)//上一层
			{
				if(a>0&&a<=nImgNum)
				{
					//near_pairs.push_back(make_pair(i,a));
					near_pairs_tmp.push_back(make_pair(i-1,a-1));
				}
			}
			for(int b = i-1; b <= i+1; b+=2)//中间层
			{
				if(b>0&&b<=nImgNum)
				{
					//near_pairs.push_back(make_pair(i,b));
					near_pairs_tmp.push_back(make_pair(i-1,b-1));
				}
			}
			for(int c = i+nLoopImgNum; c <= i+nLoopImgNum; c++)//下一层
			{
				if(c>0&&c<=nImgNum)
				{
					//near_pairs.push_back(make_pair(i,c));
					near_pairs_tmp.push_back(make_pair(i-1,c-1));
				}
			}
		}

		else if(i == 1)//左上角
		{
			for(int d = i+1; d<i+2; d++)
			{
				if(d>0&&d<=nImgNum)
					//near_pairs.push_back(make_pair(i,d));
					near_pairs_tmp.push_back(make_pair(i-1,d-1));
			}
			for(int e = i+nLoopImgNum; e<=i+nLoopImgNum; e++)
			{
				if(e>0&&e<=nImgNum)
					//near_pairs.push_back(make_pair(i,e));
					near_pairs_tmp.push_back(make_pair(i-1,e-1));
			}
			///if(nLoopImgNum*(nLoopID+1)>0&&nLoopImgNum*(nLoopID+1)<=nImgNum)
			//near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID+1)));
			if(nLoopImgNum*(nLoopID+1)>0&&nLoopImgNum*(nLoopID)<=nImgNum)
				near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID)-1));

		}
		else if(i == nLoopImgNum)//右上角
		{
			for(int f = i-1; f<=i; f+=2)
			{
				if(f>0&&f<=nImgNum)
					//near_pairs.push_back(make_pair(i, f));
					near_pairs_tmp.push_back(make_pair(i-1, f-1));
			}
			for(int g = i+nLoopImgNum; g <= i+nLoopImgNum; g++)
			{
				if(g>0&&g<=nImgNum)
					//near_pairs.push_back(make_pair(i, g));
					near_pairs_tmp.push_back(make_pair(i-1, g-1));
			}
			//            //near_pairs.push_back(make_pair(i,1));
			//            near_pairs_tmp.push_back(make_pair(i-1,1-1));
		}
		else if(i == nLoopImgNum*(nLoopNum-1)+1)//左下角
		{
			for(int h = i+1; h<=i+1; h+=2)
			{
				if(h>0&&h<=nImgNum)
					//near_pairs.push_back(make_pair(i, h));
					near_pairs_tmp.push_back(make_pair(i-1, h-1));
			}
			for(int k = i-nLoopImgNum; k<=i-nLoopImgNum; k++)
			{
				if(k>0&&k<=nImgNum)
					//near_pairs.push_back(make_pair(i, k));
					near_pairs_tmp.push_back(make_pair(i-1, k-1));
			}
			//near_pairs.push_back(make_pair(i,nLoopImgNum*nLoopNum));
			near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*nLoopNum-1));
		}
		else if(i == nLoopImgNum*nLoopNum)//右下角
		{
			for(int l = i-1; l<i; l++)
			{
				if(l>0&&l<=nImgNum)
					//near_pairs.push_back(make_pair(i, l));
					near_pairs_tmp.push_back(make_pair(i-1, l-1));
			}
			for(int m = i-nLoopImgNum; m<=i-nLoopImgNum; m++)
			{
				if(m>0&&m<=nImgNum)
					//near_pairs.push_back(make_pair(i, m));
					near_pairs_tmp.push_back(make_pair(i-1, m-1));
			}
			//            if(nLoopImgNum*(nLoopID-2)+1>0&&nLoopImgNum*(nLoopID-2)+1<=nImgNum)
			//                //near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID-2)+1));
			//                near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID-2)+1-1));
		}
		else if((i==nLoopImgNum*(nLoopID-1)+1)//左边边界
			&&(i!=1)
			&&(i!=nLoopImgNum*(nLoopNum-1)+1))
		{
			for(int n = i-nLoopImgNum; n<=i-nLoopImgNum; n++)
			{
				if(n>0&&n<=nImgNum)
					//near_pairs.push_back(make_pair(i, n));
					near_pairs_tmp.push_back(make_pair(i-1, n-1));
			}
			for(int o = i+1; o<=i+1; o+=2)
			{
				if(o>0&&o<=nImgNum)
					//near_pairs.push_back(make_pair(i,o));
					near_pairs_tmp.push_back(make_pair(i-1,o-1));
			}
			for(int p = i+nLoopImgNum; p<=i+nLoopImgNum; p++)
			{
				if(p>0&&p<=nImgNum)
					//near_pairs.push_back(make_pair(i,p));
					near_pairs_tmp.push_back(make_pair(i-1,p-1));
			}
			near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID)-1));
			//            if(nLoopImgNum*(nLoopID+1)>0&&nLoopImgNum*(nLoopID+1)<=nImgNum)
			//                //near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID+1)));
			//                near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID+1)-1));
		}
		else if((i == nLoopImgNum*nLoopID)//右边边界
			&&(i!=nLoopImgNum)
			&&(i!=nLoopImgNum*nLoopNum))
		{
			for(int q = i-nLoopImgNum; q<=i-nLoopImgNum; q++)
			{
				if(q>0&&q<=nImgNum)
					//near_pairs.push_back(make_pair(i, q));
					near_pairs_tmp.push_back(make_pair(i-1, q-1));
			}
			for(int r = i-1; r<=i; r+=2)
			{
				if(r>0&&r<=nImgNum)
					//near_pairs.push_back(make_pair(i, r));
					near_pairs_tmp.push_back(make_pair(i-1, r-1));
			}
			for(int s = i+nLoopImgNum; s <= i+nLoopImgNum; s++)
			{
				if(s>0&&s<=nImgNum)
					//near_pairs.push_back(make_pair(i, s));
					near_pairs_tmp.push_back(make_pair(i-1, s-1));
			}
			//            if(nLoopImgNum*(nLoopID-1)+1>0&&nLoopImgNum*(nLoopID-1)+1<=nImgNum)
			//                //near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID-2)+1));
			//                near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID-2)+1-1));
		}

	}

	//去掉near_pairs中的重复匹配
	vector< pair<int, int> >::iterator itDup1;
	vector< pair<int, int> >::iterator itDup2;
	for(itDup1 = near_pairs_tmp.end(); itDup1 != near_pairs_tmp.begin(); itDup1--)
	{
		for(itDup2 = near_pairs_tmp.begin(); itDup2 != near_pairs_tmp.end(); itDup2++)
		{
			if(itDup1->first==itDup2->second && itDup1->second == itDup2->first)
			{
				near_pairs_tmp.erase(itDup1);
			}
		}
	}
	vecNearPairs = near_pairs_tmp;

}



//确定原始图像相对位置函数--八邻域
void CalNearPairsEight(int nLoopImgNum, int nLoopNum, vector< pair<int, int> > &vecNearPairs)
{
	int nImgNum = nLoopImgNum * nLoopNum;//总共的图像数

	vector< pair<int,int> > near_pairs_tmp;//具有重复的匹配对，如1-2，2-1
	//    vector< pair<int,int> > near_pairs;//去掉重复后的匹配对

	//确定原始图像顺序
	for(int i = 1; i <= nImgNum; ++i)
		///for(int i = 0; i < nImgNum; i++)
	{
		int nLoopID = (int)(i / (nLoopImgNum+0.5)) + 1;

		if((i%nLoopImgNum != 0)&&(i%nLoopImgNum != 1))//中间部分的图像
		{
			for(int a = i-nLoopImgNum-1; a <= i-nLoopImgNum+1; a++)
			{
				if(a>0&&a<=nImgNum)
				{
					//near_pairs.push_back(make_pair(i,a));
					near_pairs_tmp.push_back(make_pair(i-1,a-1));
				}
			}
			for(int b = i-1; b <= i+1; b+=2)
			{
				if(b>0&&b<=nImgNum)
				{
					//near_pairs.push_back(make_pair(i,b));
					near_pairs_tmp.push_back(make_pair(i-1,b-1));
				}
			}
			for(int c = i+nLoopImgNum-1; c <= i+nLoopImgNum+1; c++)
			{
				if(c>0&&c<=nImgNum)
				{
					//near_pairs.push_back(make_pair(i,c));
					near_pairs_tmp.push_back(make_pair(i-1,c-1));
				}
			}
		}

		else if(i == 1)//左上角
		{
			for(int d = i+1; d<i+2; d++)
			{
				if(d>0&&d<=nImgNum)
					//near_pairs.push_back(make_pair(i,d));
					near_pairs_tmp.push_back(make_pair(i-1,d-1));
			}
			for(int e = i+nLoopImgNum-1; e<=i+nLoopImgNum+1; e++)
			{
				if(e>0&&e<=nImgNum)
					//near_pairs.push_back(make_pair(i,e));
					near_pairs_tmp.push_back(make_pair(i-1,e-1));
			}
			if(nLoopImgNum*(nLoopID+1)>0&&nLoopImgNum*(nLoopID+1)<=nImgNum)
				//near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID+1)));
				near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID+1)-1));

		}
		else if(i == nLoopImgNum)//右上角
		{
			for(int f = i-1; f<=i+1; f+=2)
			{
				if(f>0&&f<=nImgNum)
					//near_pairs.push_back(make_pair(i, f));
					near_pairs_tmp.push_back(make_pair(i-1, f-1));
			}
			for(int g = i+nLoopImgNum-1; g <= i+nLoopImgNum; g++)
			{
				if(g>0&&g<=nImgNum)
					//near_pairs.push_back(make_pair(i, g));
					near_pairs_tmp.push_back(make_pair(i-1, g-1));
			}
			//near_pairs.push_back(make_pair(i,1));
			near_pairs_tmp.push_back(make_pair(i-1,1-1));
		}
		else if(i == nLoopImgNum*(nLoopNum-1)+1)//左下角
		{
			for(int h = i-1; h<=i+1; h+=2)
			{
				if(h>0&&h<=nImgNum)
					//near_pairs.push_back(make_pair(i, h));
					near_pairs_tmp.push_back(make_pair(i-1, h-1));
			}
			for(int k = i-nLoopImgNum; k<=i-nLoopImgNum+1; k++)
			{
				if(k>0&&k<=nImgNum)
					//near_pairs.push_back(make_pair(i, k));
					near_pairs_tmp.push_back(make_pair(i-1, k-1));
			}
			//near_pairs.push_back(make_pair(i,nLoopImgNum*nLoopNum));
			near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*nLoopNum-1));
		}
		else if(i == nLoopImgNum*nLoopNum)//右下角
		{
			for(int l = i-1; l<i; l++)
			{
				if(l>0&&l<=nImgNum)
					//near_pairs.push_back(make_pair(i, l));
					near_pairs_tmp.push_back(make_pair(i-1, l-1));
			}
			for(int m = i-nLoopImgNum-1; m<=i-nLoopImgNum+1; m++)
			{
				if(m>0&&m<=nImgNum)
					//near_pairs.push_back(make_pair(i, m));
					near_pairs_tmp.push_back(make_pair(i-1, m-1));
			}
			if(nLoopImgNum*(nLoopID-2)+1>0&&nLoopImgNum*(nLoopID-2)+1<=nImgNum)
				//near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID-2)+1));
				near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID-2)+1-1));
		}
		else if((i==nLoopImgNum*(nLoopID-1)+1)//左边边界
			&&(i!=1)
			&&(i!=nLoopImgNum*(nLoopNum-1)+1))
		{
			for(int n = i-nLoopImgNum; n<=i-nLoopImgNum+1; n++)
			{
				if(n>0&&n<=nImgNum)
					//near_pairs.push_back(make_pair(i, n));
					near_pairs_tmp.push_back(make_pair(i-1, n-1));
			}
			for(int o = i-1; o<=i+1; o+=2)
			{
				if(o>0&&o<=nImgNum)
					//near_pairs.push_back(make_pair(i,o));
					near_pairs_tmp.push_back(make_pair(i-1,o-1));
			}
			for(int p = i+nLoopImgNum-1; p<=i+nLoopImgNum+1; p++)
			{
				if(p>0&&p<=nImgNum)
					//near_pairs.push_back(make_pair(i,p));
					near_pairs_tmp.push_back(make_pair(i-1,p-1));
			}
			if(nLoopImgNum*(nLoopID+1)>0&&nLoopImgNum*(nLoopID+1)<=nImgNum)
				//near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID+1)));
				near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID+1)-1));
		}
		else if((i == nLoopImgNum*nLoopID)//右边边界
			&&(i!=nLoopImgNum)
			&&(i!=nLoopImgNum*nLoopNum))
		{
			for(int q = i-nLoopImgNum-1; q<=i-nLoopImgNum+1; q++)
			{
				if(q>0&&q<=nImgNum)
					//near_pairs.push_back(make_pair(i, q));
					near_pairs_tmp.push_back(make_pair(i-1, q-1));
			}
			for(int r = i-1; r<=i+1; r+=2)
			{
				if(r>0&&r<=nImgNum)
					//near_pairs.push_back(make_pair(i, r));
					near_pairs_tmp.push_back(make_pair(i-1, r-1));
			}
			for(int s = i+nLoopImgNum-1; s <= i+nLoopImgNum; s++)
			{
				if(s>0&&s<=nImgNum)
					//near_pairs.push_back(make_pair(i, s));
					near_pairs_tmp.push_back(make_pair(i-1, s-1));
			}
			if(nLoopImgNum*(nLoopID-1)+1>0&&nLoopImgNum*(nLoopID-1)+1<=nImgNum)
				//near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID-2)+1));
				near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID-2)+1-1));
		}

	}

	//去掉near_pairs中的重复匹配
	vector< pair<int, int> >::iterator itDup1;
	vector< pair<int, int> >::iterator itDup2;
	for(itDup1 = near_pairs_tmp.end(); itDup1 != near_pairs_tmp.begin(); itDup1--)
	{
		for(itDup2 = near_pairs_tmp.begin(); itDup2 != near_pairs_tmp.end(); itDup2++)
		{
			if(itDup1->first==itDup2->second && itDup1->second == itDup2->first)
			{
				near_pairs_tmp.erase(itDup1);
			}
		}
	}
	vecNearPairs = near_pairs_tmp;

}



int BrightnessContrastAdjust(double dBrightnessPara, double dContrastPara, string sSrcImgPath, Mat &nDstImg)
{
	Mat mSrcImg = imread(sSrcImgPath);
	nDstImg = Mat::zeros(mSrcImg.size(), mSrcImg.type());
	mSrcImg.convertTo(nDstImg, -1, dContrastPara, dBrightnessPara);

	return 0;
}


bool PanoMatchSurf(vector<char*> &cParamList, vector<string> &cImageList, vector<double> &dExtParam, char* cOutputFile, vector<PanoMatchInfo> &vecPanoMatchInfo, bool &bNeedAddPts)
{
	int64 app_start_time = getTickCount();      //开始计时
	cv::setBreakOnError(true);
	vector<string> img_names;                   //原始图像文件名

	for(size_t imgnum = 0; imgnum < cImageList.size(); imgnum++)//从形参中获取图像名
	{
		img_names.push_back(cImageList[imgnum]);//以后只对img_names进行处理
	}
	vector<Mat> vecBrightContrastAdjustedImg(img_names.size());   //用于放置亮度和对比度调整后的图像

	int retval = parseCmdArgs(cParamList);      //处理全景拼接的参数
	if (retval)
		return retval;                          //判断参数是否正确

	int num_images = static_cast<int>(img_names.size());//原始图像数目
	if (num_images < 2)                         //判断原始图像数目是否正确（应大于等于2）
	{
		LOGLN("Need more images");
		return -1;
	}

	double work_scale = 1, seam_scale = 1, compose_scale = 1;//拼接参数初始化
	bool is_work_scale_set = false, is_seam_scale_set = false, is_compose_scale_set = false;//拼接参数初始化

	if(nBrightAdjust != 0 || nContrastAdjust != 0)
	{
		Mat mAdjustedImg;
		char cFileName[100];

		for(size_t i = 0; i < img_names.size(); i++)
		{
			BrightnessContrastAdjust(nBrightAdjust, nContrastAdjust, img_names[i], mAdjustedImg);
			vecBrightContrastAdjustedImg[i] = mAdjustedImg.clone();
			sprintf(cFileName, "/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site5/Adjusted_%d.bmp",i);
			imwrite( cFileName, mAdjustedImg);
			int mm = 0;
		}
		bBrightContrastAdjust = true;

	}



	LOGLN("Finding features...");//Step1.提取特征点
	int64 t = getTickCount();//开始计时

	vector<ImageFeatures> vecFeatures(num_images);//用于存放原始图像特征点
	SurfFeaturesFinder finder(try_gpu);//采用Surf提取特征点，是否使用GPU，默认为否
	Mat full_img, img;//原始图像矩阵

	vector<Mat> images(num_images);//用来存放原始图像矩阵数组，后续处理中大小回经过重采样
	vector<Mat> mOriImg(num_images);//用来存放原始图像矩阵数组，后续处理中不变
	vector<Size> full_img_sizes(num_images);//用于存放原始图像大小
	double seam_work_aspect = 1;
	int nLoopNum = 1;//从第一圈开始(非第0圈)
	for (int i = 0; i < num_images; ++i)
	{
		if(bBrightContrastAdjust)
		{
			full_img = vecBrightContrastAdjustedImg[i];//如果经过亮度或对比度的调整，则用调整后的图像
		}
		else
		{
			full_img = imread(img_names[i]);//如果没有经过调整，则直接读取原图像
		}

		full_img_sizes[i] = full_img.size();//读取原始图像大小

		mOriImg[i] = full_img.clone();//对full_img进行拷贝，存入mOriImg中，因为后续会对full_img进行重采样

		if (full_img.empty())//判断图像是否正确打开
		{
			LOGLN("Can't open image " << img_names[i]);
			return -1;
		}
		if (work_megapix < 0)
		{
			img = full_img;
			work_scale = 1;
			is_work_scale_set = true;
		}
		else
		{
			if (!is_work_scale_set)
			{
				work_scale = min(1.0, sqrt(work_megapix * 1e6 / full_img.size().area()));
				is_work_scale_set = true;
				cout<<work_scale;
			}
			resize(full_img, img, Size(), work_scale, work_scale);
		}
		if (!is_seam_scale_set)
		{
			seam_scale = min(1.0, sqrt(seam_megapix * 1e6 / full_img.size().area()));
			seam_work_aspect = seam_scale / work_scale;
			is_seam_scale_set = true;
		}

		finder(img, vecFeatures[i]);//对img进行特征点提取，将特征赋予ImageFeatures中
		vecFeatures[i].img_idx = i;//ImageFeature 内部图像index，从0依次向后排序。
		vecFeatures[i].ori_img_size = full_img.size();//图像大小，不经过采样
		vecFeatures[i].export_idx = vecPanoMatchInfo[i].nSrcImgIdx;//接受的外部传入index，4位,从1001到9999
		vecFeatures[i].nLoopID = vecPanoMatchInfo[i].nLoopID;//该图像位于第几圈
		vecFeatures[i].nLoopImgID = vecPanoMatchInfo[i].nLoopImgID;//该图像在本圈内的序号
		vecFeatures[i].bExpMatches = true;//是否输出匹配
		vecFeatures[i].bAddPt = false;//是否需要人工加点

		if(nLoopNum < vecFeatures[i].nLoopID)//计算该站原始图像的圈数
			nLoopNum = vecFeatures[i].nLoopID;

		LOGLN("Features in image #" << i+1 << ": " << vecFeatures[i].keypoints.size());
		cout<<endl<<"Feature struct"<<endl;
		cout<<vecFeatures[i].img_idx<<endl;
		cout<<"size.width= "<<vecFeatures[i].img_size.width<<endl;
		cout<<"size.height= "<<vecFeatures[i].img_size.height<<endl;

		resize(full_img, img, Size(), seam_scale, seam_scale);
		images[i] = img.clone();//images存放变化后的原始图像

	}


	vector<int> vecSingleLoopImgNum;//用于存放单圈中的图像数目
	for(int m= 1; m <= nLoopNum; m++)
	{
		int nLoopImgNum = 0;
		for(int n = 0; n < num_images; ++n)
		{
			if(vecFeatures[n].nLoopID == m)
			{
				nLoopImgNum++;
			}
		}
		vecSingleLoopImgNum.push_back(nLoopImgNum);//每圈有几张图像，可能有每圈图像数不一样的情况
	}

	for(size_t i = 0; i < vecFeatures.size(); i++)
	{
		vecFeatures[i].nLoopImgNum = vecSingleLoopImgNum[0];
		vecFeatures[i].nLoopNum = vecSingleLoopImgNum.size();
	}




	full_img.release();
	img.release();

	LOGLN("Finding features, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");//Step1.提取特征点结束

	LOG("Pairwise matching");//Step2.特征点匹配
	t = getTickCount();//匹配阶段计时
	vector<MatchesInfo> pairwise_matches;//用于存放匹配结果，匹配结构为MatchesInfo
	BestOf2NearestMatcher matcher(try_gpu, match_conf);//最邻近匹配，是否使用GPU，和匹配阈值
	matcher(vecFeatures, pairwise_matches);//输入特征点数组，输出匹配点结构数组
	LOGLN("Pairwise matching, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");//Step2.特征点匹配阶段所用时间

	// Leave only images we are sure are from the same panorama
	vector<int> indices;//
	indices = leaveBiggestComponent(vecFeatures, pairwise_matches, conf_thresh, images, mOriImg, match_conf);

	vector<Mat> img_subset;
	vector<string> img_names_subset;
	vector<Size> full_img_sizes_subset;
	for (size_t i = 0; i < indices.size(); ++i)
	{
		img_names_subset.push_back(img_names[indices[i]]);
		img_subset.push_back(images[indices[i]]);
		full_img_sizes_subset.push_back(full_img_sizes[indices[i]]);
	}

	images = img_subset;
	img_names = img_names_subset;
	full_img_sizes = full_img_sizes_subset;

	// Check if we still have enough images
	num_images = static_cast<int>(img_names.size());
	if (num_images < 2)
	{
		LOGLN("Need more images");
		return -1;
	}


	//计算原始图像间的相对位置
	int nLoopImgNums = vecFeatures[0].nLoopImgNum;//一圈有几张图像
	int nLoopNums = vecFeatures[0].nLoopNum;//一共有几圈
	vector< pair<int,int> > vecNearpairs;//去掉重复后的匹配对
	CalNearPairsFour(nLoopImgNums, nLoopNums, vecNearpairs);//只对上下左右四邻域匹配


	PanoMatchInfo struPanoMatchesInfo;
	vector<cv::Point2f> vecCvPt2f;
	vector<PanoMatchInfo> vecPanoMatchInfoTest;
	vector<PanoMatchInfo> vecPanoMatchesOut;
	vector<PanoMatchInfo>::iterator itPM;


	//为传出结构赋值
	for(size_t i = 0; i < pairwise_matches.size(); i++)
	{
		if(pairwise_matches[i].confidence > 1 )
		{
			struPanoMatchesInfo.nSrcImgIdx = pairwise_matches[i].exp_idx_l;
			struPanoMatchesInfo.nDstImgIdx = pairwise_matches[i].exp_idx_r;
			struPanoMatchesInfo.nMatchPtNum = pairwise_matches[i].num_inliers;
			struPanoMatchesInfo.sImgPathSrc = pairwise_matches[i].sImgPathSrc;
			struPanoMatchesInfo.sImgPathDst = pairwise_matches[i].sImgPathDst;
			struPanoMatchesInfo.nInIdxSrc = pairwise_matches[i].src_img_idx;
			struPanoMatchesInfo.nInIdxDst = pairwise_matches[i].dst_img_idx;

			for(size_t j = 0; j < pairwise_matches[i].vecMatchPtsL.size(); j++)
			{
				MatchPt fPtL;
				fPtL.x = pairwise_matches[i].vecMatchPtsL[j].x;
				fPtL.y = pairwise_matches[i].vecMatchPtsL[j].y;
				fPtL.ptIdx = pairwise_matches[i].vecMatchPtsL[j].ptIdx;
				fPtL.ptImgIdxL = pairwise_matches[i].vecMatchPtsL[j].ptImgIdxL;
				fPtL.ptImgIdxR = pairwise_matches[i].vecMatchPtsL[j].ptImgIdxR;
				struPanoMatchesInfo.vecMatchPtsSrc.push_back(fPtL);

				MatchPt fPtR;
				fPtR.x = pairwise_matches[i].vecMatchPtsR[j].x;
				fPtR.y = pairwise_matches[i].vecMatchPtsR[j].y;
				fPtR.ptIdx = pairwise_matches[i].vecMatchPtsR[j].ptIdx;
				fPtR.ptImgIdxL = pairwise_matches[i].vecMatchPtsR[j].ptImgIdxL;
				fPtR.ptImgIdxR = pairwise_matches[i].vecMatchPtsR[j].ptImgIdxR;
				struPanoMatchesInfo.vecMatchPtsDst.push_back(fPtR);
			}

			size_t nSort = 0;
			bool bRepeat = false;

			if(vecPanoMatchesOut.size() > 0)//去掉重复结构
			{
				while(nSort!=vecPanoMatchesOut.size())
				{
					unsigned int nSrcIdx = vecPanoMatchesOut[nSort].nSrcImgIdx;
					unsigned int nDstIdx = vecPanoMatchesOut[nSort].nDstImgIdx;
					if((struPanoMatchesInfo.nSrcImgIdx == nSrcIdx) &&
						(struPanoMatchesInfo.nDstImgIdx == nDstIdx))
					{
						bRepeat = true;
					}
					nSort++;
				}
				if(bRepeat == false)
				{
					vecPanoMatchesOut.push_back(struPanoMatchesInfo);
				}

			}
			else
			{
				vecPanoMatchesOut.push_back(struPanoMatchesInfo);
			}
			struPanoMatchesInfo.vecMatchPtsSrc.clear();
			struPanoMatchesInfo.vecMatchPtsDst.clear();
		}

	}


	//modify begin
	vector<MatchPt> vecAllMatchSrc;
	vector<MatchPt> vecAllMatchDst;
	for(size_t i = 0; i < vecPanoMatchesOut.size(); i++)
	{
		for(size_t j = 0; j < vecPanoMatchesOut[i].vecMatchPtsSrc.size(); j++)
		{
			vecAllMatchSrc.push_back(vecPanoMatchesOut[i].vecMatchPtsSrc[j]);
		}

		for(size_t m = 0; m < vecPanoMatchesOut[i].vecMatchPtsDst.size(); m++)
		{
			vecAllMatchDst.push_back(vecPanoMatchesOut[i].vecMatchPtsDst[m]);
		}
	}


	for(size_t i = 0; i < vecPanoMatchInfo.size(); i++)
	{
		for(size_t j = 0; j < vecAllMatchSrc.size(); j++)
		{
			if(vecPanoMatchInfo[i].nSrcImgIdx == vecAllMatchSrc[j].ptImgIdxL)
			{
				vecPanoMatchInfo[i].vecMatchPtsSrc.push_back(vecAllMatchSrc[j]);

			}
			if(vecPanoMatchInfo[i].nSrcImgIdx == vecAllMatchDst[j].ptImgIdxL)
			{
				vecPanoMatchInfo[i].vecMatchPtsSrc.push_back(vecAllMatchDst[j]);
			}
		}
	}

	//modify end






	/*****************************************************/
	//    ofstream testOut;
	//    testOut.open("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site4/testoutpanoinfo.txt", ios::app);
	//    for(int i = 0; i < vecPanoMatchesOut.size(); i++)
	//    {
	//        testOut<<"nInIdxSrc = "<<vecPanoMatchesOut[i].nInIdxSrc<<"   "<<"nInIdxDst = "<<vecPanoMatchesOut[i].nInIdxDst<<endl;
	//        testOut<<"nSrcImgIdx = "<<vecPanoMatchesOut[i].nSrcImgIdx<<"   "<<"nDstImgIdx = "<<vecPanoMatchesOut[i].nDstImgIdx<<endl;
	//        testOut<<"sImgPathSrc = "<<vecPanoMatchesOut[i].sImgPathSrc<<"   "<<"sImgPathDst = "<<vecPanoMatchesOut[i].sImgPathDst<<endl;
	//        for(int j = 0; j < vecPanoMatchesOut[i].vecInIdxDst.size(); j++)
	//        {
	//            testOut<<vecPanoMatchesOut[i].vecInIdxDst[j]<<"  ";
	//        }
	//        testOut<<endl;
	//    }
	/*****************************************************/
	int nn=0;





	cout<<"Match finished!"<<endl;

	return true;
}




bool PanoMosaic(vector<char*> &cParamList, vector<string> &sImageList, vector<double> &dExtParam, vector<PanoMatchInfo> vecPanoMatchPts, char* cOutputFile)
{
	int64 app_start_time = getTickCount();
	cv::setBreakOnError(true);

	vector<string> vecImgNames;

	for(size_t i = 0; i < sImageList.size(); i++)
	{
		vecImgNames.push_back(sImageList[i]);
	}


	int retval = parseCmdArgs(cParamList);
	if (retval)
		return retval;

	// 判断是否有足够的图像
	int num_images = static_cast<int>(vecImgNames.size());
	if (num_images < 2)
	{
		LOGLN("Need more images");
		return -1;
	}

	double work_scale = 1, seam_scale = 1, compose_scale = 1;
	bool is_work_scale_set = false, is_seam_scale_set = false, is_compose_scale_set = false;


	LOGLN("Finding features...");
	int64 t = getTickCount();

	vector<ImageFeatures> features(num_images);

	SurfFeaturesFinder finder(try_gpu);
	Mat full_img, img;
	vector<cv::Mat> vecImgFeatMat;

	vector<Mat> images(num_images);
	vector<Mat> mOriImg(num_images);
	vector<Size> full_img_sizes(num_images);
	double seam_work_aspect = 1;

	for (int i = 0; i < num_images; ++i)
	{
		full_img = imread(vecImgNames[i]);
		full_img_sizes[i] = full_img.size();

		mOriImg[i] = full_img.clone();

		if (full_img.empty())
		{
			LOGLN("Can't open image " << vecImgNames[i]);
			return -1;
		}
		if (work_megapix < 0)
		{
			img = full_img;
			work_scale = 1;
			is_work_scale_set = true;
		}
		else
		{
			if (!is_work_scale_set)
			{
				work_scale = min(1.0, sqrt(work_megapix * 1e6 / full_img.size().area()));
				is_work_scale_set = true;
				cout<<work_scale;
			}
			resize(full_img, img, Size(), work_scale, work_scale);
		}
		if (!is_seam_scale_set)
		{
			seam_scale = min(1.0, sqrt(seam_megapix * 1e6 / full_img.size().area()));
			seam_work_aspect = seam_scale / work_scale;
			is_seam_scale_set = true;
		}

		features[i].img_idx = i;
		features[i].img_size = img.size();
		features[i].ori_img_size = full_img.size();
		features[i].sImgPathSrc = vecImgNames[i];

		vecImgFeatMat.push_back(img);

		resize(full_img, img, Size(), seam_scale, seam_scale);
		images[i] = img.clone();

	}

	/***************************************************/

	cv::Mat descriptor;
	cv::Mat mGrayImage;
	cv::Mat mDescriptor;
	ImageFeatures struImgFeature;
	cv::SurfDescriptorExtractor sdeExtractor;


	//将所有点赋给features
	for(size_t i = 0; i < vecPanoMatchPts.size(); i++)
	{
		for(size_t j = 0; j < features.size(); j++)
		{
			if(vecPanoMatchPts[i].sImgPathSrc == features[j].sImgPathSrc)
			{
				features[j].export_idx = vecPanoMatchPts[i].nSrcImgIdx;

				cv::KeyPoint ptKey;
				cv::Point2f ptCv;
				vector<cv::KeyPoint> vecKeyPt;
				float fResizePtX;
				float fResizePtY;
				for(size_t m = 0; m < vecPanoMatchPts[i].vecMatchPtsSrc.size(); m++)
				{

					float fX = vecPanoMatchPts[i].vecMatchPtsSrc[m].x;
					float fY = vecPanoMatchPts[i].vecMatchPtsSrc[m].y;
					float fRizImgW = features[j].img_size.width;
					float fRizImgH = features[j].img_size.height;
					float fOriImgW = features[j].ori_img_size.width;
					float fOriImgH = features[j].ori_img_size.height;
					fResizePtX = fX*fRizImgW/fOriImgW;
					fResizePtY = fY*fRizImgH/fOriImgH;
					ptCv.x = fResizePtX;
					ptCv.y = fResizePtY;
					ptKey = KeyPoint(ptCv, 1, 1, 0, -1);

					features[j].keypoints.push_back(ptKey);
					vecPanoMatchPts[i].vecMatchPtsSrc[m].x = fResizePtX;
					vecPanoMatchPts[i].vecMatchPtsSrc[m].y = fResizePtY;

				}
				features[j].AddPanoMatches = vecPanoMatchPts[i];

				features[j].bAddPt = true;

			}
		}
	}

	vector<unsigned long long int> vecTe;
	unsigned long long int unTe;
	for(size_t i = 0; i < features.size(); i++)
	{
		for(size_t m = 0; m < features[i].AddPanoMatches.vecMatchPtsSrc.size(); m++)
		{
			unTe = features[i].AddPanoMatches.vecMatchPtsSrc[m].ptIdx;
			vecTe.push_back(unTe);
		}
		vecTe.clear();
	}



	int nBool;
	for(size_t i = 0; i < features.size(); i++)
	{
		nBool = (int)features[i].bAddPt + (int)features[i+1].bAddPt;
	}

	for(size_t i = 0; i < features.size(); i++)
	{
		if(nBool == 0)
		{
			features[i].bAddPt = false;
		}
		else
		{
			features[i].bAddPt = true;
		}

	}

	/***************************************************/

	full_img.release();
	img.release();

	LOGLN("Finding features, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	LOG("Pairwise matching");
	t = getTickCount();
	vector<MatchesInfo> pairwise_matches;
	bool bOnlyReadMatchPts = true;
	///BestOf2NearestMatcher matcher(try_gpu, match_conf);
	///matcher(features, pairwise_matches);
	BestOf2NearestMatcher matcher(try_gpu, match_conf);
	matcher(features, pairwise_matches, bOnlyReadMatchPts);
	LOGLN("Pairwise matching, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");


	/*****************************************************/
	ofstream testi("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site5/test_pairwise.txt", ios::app);
	if(testi.fail())
		cout << "fail" << endl;
	for(size_t i = 0; i < pairwise_matches.size(); i++)
	{
		if(pairwise_matches[i].num_inliers!=0)
		{
			testi<<"src = "<<pairwise_matches[i].src_img_idx<<"  "<<pairwise_matches[i].dst_img_idx<<endl;
			testi<<"num_inliers = "<<pairwise_matches[i].num_inliers<<endl;
			testi<<"confidence = "<<pairwise_matches[i].confidence<<endl;
		}

	}
	testi.close();


	/*****************************************************/
	// Leave only images we are sure are from the same panorama
	/*    vector<int> indices = leaveBiggestComponent(features, pairwise_matches, conf_thresh);*///2011-11-25
	vector<int> indices;
	for (int i = 0; i < num_images; ++i)
	{
		indices.push_back(i);
	}

	vector<Mat> img_subset;
	vector<string> img_names_subset;
	vector<Size> full_img_sizes_subset;
	for (size_t i = 0; i < indices.size(); ++i)
	{
		img_names_subset.push_back(vecImgNames[indices[i]]);
		img_subset.push_back(images[indices[i]]);
		full_img_sizes_subset.push_back(full_img_sizes[indices[i]]);
	}

	images = img_subset;
	vecImgNames = img_names_subset;
	full_img_sizes = full_img_sizes_subset;

	// Check if we still have enough images
	num_images = static_cast<int>(vecImgNames.size());
	if (num_images < 2)
	{
		LOGLN("Need more images");
		return false;
	}


	LOGLN("Estimating rotations...");
	t = getTickCount();
	HomographyBasedEstimator estimator;
	vector<CameraParams> cameras;
	estimator(features, pairwise_matches, cameras);
	LOGLN("Estimating rotations, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");


	for (size_t i = 0; i < cameras.size(); ++i)
	{
		Mat R;
		cameras[i].R.convertTo(R, CV_32F);
		cameras[i].R = R;
		LOGLN("Initial focal length #" << indices[i]+1 << ": " << cameras[i].focal);
	}

	LOG("Bundle adjustment");
	t = getTickCount();
	BundleAdjuster adjuster(ba_space, conf_thresh);
	adjuster(features, pairwise_matches, cameras);
	LOGLN("Bundle adjustment, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	// Find median focal length
	vector<double> focals;
	for (size_t i = 0; i < cameras.size(); ++i)
	{
		LOGLN("Camera #" << indices[i]+1 << " focal length: " << cameras[i].focal);
		focals.push_back(cameras[i].focal);
	}

	nth_element(focals.begin(), focals.begin() + focals.size()/2, focals.end());

	float warped_image_scale = static_cast<float>(focals[focals.size() / 2]);

	if (wave_correct)
	{
		LOGLN("Wave correcting...");
		t = getTickCount();
		vector<Mat> rmats;
		for (size_t i = 0; i < cameras.size(); ++i)
			rmats.push_back(cameras[i].R);
		waveCorrect(rmats);
		for (size_t i = 0; i < cameras.size(); ++i)
		{
			cameras[i].R = rmats[i];
		}

		LOGLN("Wave correcting, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	}

	LOGLN("Warping images (auxiliary)... ");
	t = getTickCount();

	vector<Point> corners(num_images);
	vector<Mat> masks_warped(num_images);
	vector<Mat> images_warped(num_images);
	vector<Size> sizes(num_images);
	vector<Mat> masks(num_images);

	// Preapre images masks
	for (int i = 0; i < num_images; ++i)
	{
		masks[i].create(images[i].size(), CV_8U);
		masks[i].setTo(Scalar::all(255));
	}

	// Warp images and their masks
	Ptr<Warper> warper = Warper::createByCameraFocal(static_cast<float>(warped_image_scale * seam_work_aspect), warp_type);
	for (int i = 0; i < num_images; ++i)
	{
		corners[i] = warper->warp(images[i], static_cast<float>(cameras[i].focal * seam_work_aspect), cameras[i].R, images_warped[i]);
		sizes[i] = images_warped[i].size();
		warper->warp(masks[i], static_cast<float>(cameras[i].focal * seam_work_aspect), cameras[i].R, masks_warped[i], INTER_NEAREST, BORDER_CONSTANT);
	}

	vector<Mat> images_warped_f(num_images);
	for (int i = 0; i < num_images; ++i)
		images_warped[i].convertTo(images_warped_f[i], CV_32F);

	LOGLN("Warping images, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	LOGLN("Exposure compensation (feed)...");
	t = getTickCount();
	Ptr<ExposureCompensator> compensator = ExposureCompensator::createDefault(expos_comp_type);
	compensator->feed(corners, images_warped, masks_warped);
	LOGLN("Exposure compensation (feed), time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	LOGLN("Finding seams...");
	t = getTickCount();
	Ptr<SeamFinder> seam_finder = SeamFinder::createDefault(seam_find_type);
	seam_finder->find(images_warped_f, corners, masks_warped);
	LOGLN("Finding seams, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	// Release unused memory
	images.clear();
	images_warped.clear();
	images_warped_f.clear();
	masks.clear();

	LOGLN("Compositing...");
	t = getTickCount();

	Mat img_warped, img_warped_s;
	Mat dilated_mask, seam_mask, mask, mask_warped;
	Ptr<Blender> blender;
	double compose_seam_aspect = 1;
	double compose_work_aspect = 1;

	for (int img_idx = 0; img_idx < num_images; ++img_idx)
	{
		LOGLN("Compositing image #" << indices[img_idx]+1);

		// Read image and resize it if necessary
		full_img = imread(vecImgNames[img_idx]);
		if (!is_compose_scale_set)
		{
			if (compose_megapix > 0)
				compose_scale = min(1.0, sqrt(compose_megapix * 1e6 / full_img.size().area()));
			is_compose_scale_set = true;

			// Compute relative scales
			compose_seam_aspect = compose_scale / seam_scale;
			compose_work_aspect = compose_scale / work_scale;

			// Update warped image scale
			warped_image_scale *= static_cast<float>(compose_work_aspect);
			warper = Warper::createByCameraFocal(warped_image_scale, warp_type);

			// Update corners and sizes
			for (int i = 0; i < num_images; ++i)
			{
				// Update camera focal
				cameras[i].focal *= compose_work_aspect;

				// Update corner and size
				Size sz = full_img_sizes[i];
				if (abs(compose_scale - 1) > 1e-1)
				{
					sz.width = cvRound(full_img_sizes[i].width * compose_scale);
					sz.height = cvRound(full_img_sizes[i].height * compose_scale);
				}
				Rect roi = warper->warpRoi(sz, static_cast<float>(cameras[i].focal), cameras[i].R);
				corners[i] = roi.tl();
				sizes[i] = roi.size();
			}
		}
		if (abs(compose_scale - 1) > 1e-1)
			resize(full_img, img, Size(), compose_scale, compose_scale);
		else
			img = full_img;
		full_img.release();
		Size img_size = img.size();

		// Warp the current image
		warper->warp(img, static_cast<float>(cameras[img_idx].focal), cameras[img_idx].R,
			img_warped);

		// Warp the current image mask
		mask.create(img_size, CV_8U);
		mask.setTo(Scalar::all(255));
		warper->warp(mask, static_cast<float>(cameras[img_idx].focal), cameras[img_idx].R, mask_warped,
			INTER_NEAREST, BORDER_CONSTANT);

		// Compensate exposure
		compensator->apply(img_idx, corners[img_idx], img_warped, mask_warped);

		img_warped.convertTo(img_warped_s, CV_16S);
		img_warped.release();
		img.release();
		mask.release();

		dilate(masks_warped[img_idx], dilated_mask, Mat());
		resize(dilated_mask, seam_mask, mask_warped.size());
		mask_warped = seam_mask & mask_warped;

		if (static_cast<Blender*>(blender) == 0)
		{
			blender = Blender::createDefault(blend_type);
			Size dst_sz = resultRoi(corners, sizes).size();
			float blend_width = sqrt(static_cast<float>(dst_sz.area())) * blend_strength / 100.f;
			if (blend_width < 1.f)
				blender = Blender::createDefault(Blender::NO);
			else if (blend_type == Blender::MULTI_BAND)
			{
				MultiBandBlender* mb = dynamic_cast<MultiBandBlender*>(static_cast<Blender*>(blender));
				mb->setNumBands(static_cast<int>(ceil(log(blend_width)/log(2.)) - 1.));
				LOGLN("Multi-band blender, number of bands: " << mb->numBands());
			}
			else if (blend_type == Blender::FEATHER)
			{
				FeatherBlender* fb = dynamic_cast<FeatherBlender*>(static_cast<Blender*>(blender));
				fb->setSharpness(1.f/blend_width);
				LOGLN("Feather blender, number of bands: " << fb->sharpness());
			}
			blender->prepare(corners, sizes);
		}

		// Blend the current image
		blender->feed(img_warped_s, mask_warped, corners[img_idx]);
	}

	Mat result, result_mask;
	blender->blend(result, result_mask);

	LOGLN("Compositing, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	imwrite(cOutputFile, result);

	LOGLN("Finished, total time: " << ((getTickCount() - app_start_time) / getTickFrequency()) << " sec");
	return true;
}

bool PanoMatchPoint(vector<char*> &cParamList, vector<string> &cImageList, vector<double> &dExtParam, char* cOutputFile, vector<PanoMatchInfo> &vecPanoMatchInfo, bool &bNeedAddPts)
{
	int64 app_start_time = getTickCount();
	cv::setBreakOnError(true);
	vector<string> img_names;

	for(size_t imgnum = 0; imgnum < cImageList.size(); imgnum++)
	{
		img_names.push_back(cImageList[imgnum]);
	}

	int retval = parseCmdArgs(cParamList);
	if (retval)
		return retval;
	// Check if have enough images
	int num_images = static_cast<int>(img_names.size());
	if (num_images < 2)
	{
		LOGLN("Need more images");
		return -1;
	}

	double work_scale = 1, seam_scale = 1, compose_scale = 1;
	bool is_work_scale_set = false, is_seam_scale_set = false, is_compose_scale_set = false;

	LOGLN("Finding features...");
	int64 t = getTickCount();

	vector<ImageFeatures> vecFeatures(num_images);
	SurfFeaturesFinder finder(try_gpu);
	Mat full_img, img;

	vector<Mat> images(num_images);
	vector<Mat> mOriImg(num_images);
	vector<Size> full_img_sizes(num_images);
	double seam_work_aspect = 1;

	for (int i = 0; i < num_images; ++i)
	{
		full_img = imread(img_names[i]);
		full_img_sizes[i] = full_img.size();

		mOriImg[i] = full_img.clone();

		if (full_img.empty())
		{
			LOGLN("Can't open image " << img_names[i]);
			return -1;
		}
		if (work_megapix < 0)
		{
			img = full_img;
			work_scale = 1;
			is_work_scale_set = true;
		}
		else
		{
			if (!is_work_scale_set)
			{
				work_scale = min(1.0, sqrt(work_megapix * 1e6 / full_img.size().area()));
				is_work_scale_set = true;
				cout<<work_scale;
			}
			resize(full_img, img, Size(), work_scale, work_scale);
		}
		if (!is_seam_scale_set)
		{
			seam_scale = min(1.0, sqrt(seam_megapix * 1e6 / full_img.size().area()));
			seam_work_aspect = seam_scale / work_scale;
			is_seam_scale_set = true;
		}
		//vecFeatures[i].descriptors
		finder(img, vecFeatures[i]);
		vecFeatures[i].img_idx = i;
		vecFeatures[i].ori_img_size = full_img.size();
		vecFeatures[i].export_idx = vecPanoMatchInfo[i].nSrcImgIdx;
		vecFeatures[i].bExpMatches = true;
		vecFeatures[i].bAddPt = false;
		LOGLN("Features in image #" << i+1 << ": " << vecFeatures[i].keypoints.size());

		cout<<endl<<"Feature struct"<<endl;
		cout<<vecFeatures[i].img_idx<<endl;
		cout<<"size.width= "<<vecFeatures[i].img_size.width<<endl;
		cout<<"size.height= "<<vecFeatures[i].img_size.height<<endl;

		resize(full_img, img, Size(), seam_scale, seam_scale);
		images[i] = img.clone();

		/////////////////////////////////////////////////////
		cout<<"seam_scale: "<<seam_scale;
		IplImage pTest;
		pTest = IplImage(images[i]);
		cout<<endl<<"Height = "<<pTest.height<<endl;
		cout<<"Width = "<<pTest.width<<endl;

		IplImage pOTest;
		pOTest = IplImage(mOriImg[i]);
		cout<<endl<<"OHeight = "<<pOTest.height<<endl;
		cout<<"OWidth = "<<pOTest.width<<endl;
		/////////////////////////////////////////////////////


	}

	full_img.release();
	img.release();

	LOGLN("Finding features, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	LOG("Pairwise matching");
	t = getTickCount();
	vector<MatchesInfo> pairwise_matches;
	BestOf2NearestMatcher matcher(try_gpu, match_conf/*, cControlPointPath*/);
	matcher(vecFeatures, pairwise_matches);
	LOGLN("Pairwise matching, time: " << ((getTickCount() - t) / getTickFrequency()) << " sec");

	// Leave only images we are sure are from the same panorama
	vector<int> indices;
	indices = leaveBiggestComponent(vecFeatures, pairwise_matches, conf_thresh, images, mOriImg, match_conf);


	vector<Mat> img_subset;
	vector<string> img_names_subset;
	vector<Size> full_img_sizes_subset;
	for (size_t i = 0; i < indices.size(); ++i)
	{
		img_names_subset.push_back(img_names[indices[i]]);
		img_subset.push_back(images[indices[i]]);
		full_img_sizes_subset.push_back(full_img_sizes[indices[i]]);
	}

	images = img_subset;
	img_names = img_names_subset;
	full_img_sizes = full_img_sizes_subset;

	// Check if we still have enough images
	num_images = static_cast<int>(img_names.size());
	if (num_images < 2)
	{
		LOGLN("Need more images");
		return -1;
	}


	PanoMatchInfo struPanoMatchesInfo;
	vector<cv::Point2f> vecCvPt2f;
	vector<PanoMatchInfo> vecPanoMatchInfoTest;
	vector<PanoMatchInfo> vecPanoMatchesOut;
	vector<PanoMatchInfo>::iterator itPM;



	//为传出结构赋值
	for(size_t i = 0; i < pairwise_matches.size(); i++)
	{
		if(pairwise_matches[i].confidence > 1 )
		{
			struPanoMatchesInfo.nSrcImgIdx = pairwise_matches[i].exp_idx_l;
			struPanoMatchesInfo.nDstImgIdx = pairwise_matches[i].exp_idx_r;
			struPanoMatchesInfo.nMatchPtNum = pairwise_matches[i].num_inliers;
			struPanoMatchesInfo.sImgPathSrc = pairwise_matches[i].sImgPathSrc;
			struPanoMatchesInfo.sImgPathDst = pairwise_matches[i].sImgPathDst;
			struPanoMatchesInfo.nInIdxSrc = pairwise_matches[i].src_img_idx;
			struPanoMatchesInfo.nInIdxDst = pairwise_matches[i].dst_img_idx;

			for(size_t j = 0; j < pairwise_matches[i].vecMatchPtsL.size(); j++)
			{
				MatchPt fPtL;
				fPtL.x = pairwise_matches[i].vecMatchPtsL[j].x;
				fPtL.y = pairwise_matches[i].vecMatchPtsL[j].y;
				fPtL.ptIdx = pairwise_matches[i].vecMatchPtsL[j].ptIdx;
				fPtL.ptImgIdxL = pairwise_matches[i].vecMatchPtsL[j].ptImgIdxL;
				fPtL.ptImgIdxR = pairwise_matches[i].vecMatchPtsL[j].ptImgIdxR;
				struPanoMatchesInfo.vecMatchPtsSrc.push_back(fPtL);

				MatchPt fPtR;
				fPtR.x = pairwise_matches[i].vecMatchPtsR[j].x;
				fPtR.y = pairwise_matches[i].vecMatchPtsR[j].y;
				fPtR.ptIdx = pairwise_matches[i].vecMatchPtsR[j].ptIdx;
				fPtR.ptImgIdxL = pairwise_matches[i].vecMatchPtsR[j].ptImgIdxL;
				fPtR.ptImgIdxR = pairwise_matches[i].vecMatchPtsR[j].ptImgIdxR;
				struPanoMatchesInfo.vecMatchPtsDst.push_back(fPtR);
			}
			vecPanoMatchInfoTest.push_back(struPanoMatchesInfo);

			///vecPanoMatchInfo.push_back(struPanoMatchesInfo);
			size_t nSort = 0;
			bool bRepeat = false;

			if(vecPanoMatchesOut.size() > 0)//去掉重复结构
			{
				while(nSort!=vecPanoMatchesOut.size())
				{
					unsigned int nSrcIdx = vecPanoMatchesOut[nSort].nSrcImgIdx;
					unsigned int nDstIdx = vecPanoMatchesOut[nSort].nDstImgIdx;
					if((struPanoMatchesInfo.nSrcImgIdx == nSrcIdx) &&
						(struPanoMatchesInfo.nDstImgIdx == nDstIdx))
					{
						bRepeat = true;
					}
					nSort++;
				}
				if(bRepeat == false)
				{
					vecPanoMatchesOut.push_back(struPanoMatchesInfo);
				}

			}
			else
			{
				vecPanoMatchesOut.push_back(struPanoMatchesInfo);
			}
			struPanoMatchesInfo.vecMatchPtsSrc.clear();
			struPanoMatchesInfo.vecMatchPtsDst.clear();
		}

	}

	//单圈全景处理
	if(vecPanoMatchesOut.size() == num_images)//如果每幅影像都参与匹配.则处理最后一副影像和第一幅的关系
	{
		int nSrc1, nSrc2, nDst1, nDst2, nFind;//处理边界，如最后一幅影像和第一幅影像
		for(size_t i = 0; i < vecPanoMatchesOut.size()-1; i++)
		{
			for(size_t j = i+1; j < vecPanoMatchesOut.size(); j++)
			{
				if(vecPanoMatchesOut[i].nSrcImgIdx == vecPanoMatchesOut[j].nSrcImgIdx)
				{
					nSrc1 = i;
					nSrc2 = j;
				}
				if(vecPanoMatchesOut[i].nDstImgIdx == vecPanoMatchesOut[j].nDstImgIdx)
				{
					nDst1 = i;
					nDst2 = j;
				}
			}
		}

		if(nSrc1 == nDst1 || nSrc1 == nDst2)
		{
			nFind = nSrc1;
		}
		else if(nSrc2 == nDst1 || nSrc2 == nDst2)
		{
			nFind = nSrc2;
		}
		else
		{
			return false;
		}

		unsigned int nChangeIdx;
		nChangeIdx = vecPanoMatchesOut[nFind].nSrcImgIdx;
		vecPanoMatchesOut[nFind].nSrcImgIdx = vecPanoMatchesOut[nFind].nDstImgIdx;
		vecPanoMatchesOut[nFind].nDstImgIdx = nChangeIdx;
		vector<MatchPt> vecChange;
		for(size_t i = 0; i < vecPanoMatchesOut[nFind].vecMatchPtsSrc.size(); i++)
		{
			vecPanoMatchesOut[nFind].vecMatchPtsSrc[i].ptIdx = vecPanoMatchesOut[nFind].nSrcImgIdx* 10e11 + vecPanoMatchesOut[nFind].nDstImgIdx * 10e7 + i + 1;
			vecPanoMatchesOut[nFind].vecMatchPtsDst[i].ptIdx = vecPanoMatchesOut[nFind].nSrcImgIdx* 10e11 + vecPanoMatchesOut[nFind].nDstImgIdx * 10e7 + i + 1;
		}

		vecChange = vecPanoMatchesOut[nFind].vecMatchPtsSrc;
		vecPanoMatchesOut[nFind].vecMatchPtsSrc.clear();
		vecPanoMatchesOut[nFind].vecMatchPtsSrc = vecPanoMatchesOut[nFind].vecMatchPtsDst;
		vecPanoMatchesOut[nFind].vecMatchPtsDst.clear();
		vecPanoMatchesOut[nFind].vecMatchPtsDst = vecChange;
		vecChange.clear();

		bNeedAddPts = false;
	}
	else //如果有图像没有参与匹配，则判断最后一幅影像和第一幅影像是否在已匹配的像对中，若没有则不处理
	{
		for(itPM = vecPanoMatchesOut.begin(); itPM != vecPanoMatchesOut.end(); itPM++)
		{
			if(itPM->nInIdxDst - itPM->nInIdxDst == 1-num_images)
			{
				int nSrc1, nSrc2, nDst1, nDst2, nFind;//处理边界，如最后一幅影像和第一幅影像
				for(size_t i = 0; i < vecPanoMatchesOut.size()-1; i++)
				{
					for(size_t j = i+1; j < vecPanoMatchesOut.size(); j++)
					{
						if(vecPanoMatchesOut[i].nSrcImgIdx == vecPanoMatchesOut[j].nSrcImgIdx)
						{
							nSrc1 = i;
							nSrc2 = j;
						}
						if(vecPanoMatchesOut[i].nDstImgIdx == vecPanoMatchesOut[j].nDstImgIdx)
						{
							nDst1 = i;
							nDst2 = j;
						}
					}
				}

				if(nSrc1 == nDst1 || nSrc1 == nDst2)
				{
					nFind = nSrc1;
				}
				else if(nSrc2 == nDst1 || nSrc2 == nDst2)
				{
					nFind = nSrc2;
				}
				else
				{
					return false;
				}

				unsigned int nChangeIdx;
				nChangeIdx = vecPanoMatchesOut[nFind].nSrcImgIdx;
				vecPanoMatchesOut[nFind].nSrcImgIdx = vecPanoMatchesOut[nFind].nDstImgIdx;
				vecPanoMatchesOut[nFind].nDstImgIdx = nChangeIdx;
				vector<MatchPt> vecChange;
				for(size_t i = 0; i < vecPanoMatchesOut[nFind].vecMatchPtsSrc.size(); i++)
				{
					vecPanoMatchesOut[nFind].vecMatchPtsSrc[i].ptIdx = vecPanoMatchesOut[nFind].nSrcImgIdx* 10e11 + vecPanoMatchesOut[nFind].nDstImgIdx * 10e7 + i + 1;
					vecPanoMatchesOut[nFind].vecMatchPtsDst[i].ptIdx = vecPanoMatchesOut[nFind].nSrcImgIdx* 10e11 + vecPanoMatchesOut[nFind].nDstImgIdx * 10e7 + i + 1;
				}

				vecChange = vecPanoMatchesOut[nFind].vecMatchPtsSrc;
				vecPanoMatchesOut[nFind].vecMatchPtsSrc.clear();
				vecPanoMatchesOut[nFind].vecMatchPtsSrc = vecPanoMatchesOut[nFind].vecMatchPtsDst;
				vecPanoMatchesOut[nFind].vecMatchPtsDst.clear();
				vecPanoMatchesOut[nFind].vecMatchPtsDst = vecChange;
				vecChange.clear();
			}
		}

		bNeedAddPts = true;

	}

	//modify
	vector<MatchPt> vecAllMatchSrc;
	vector<MatchPt> vecAllMatchDst;
	for(size_t i = 0; i < vecPanoMatchesOut.size(); i++)
	{
		for(size_t j = 0; j < vecPanoMatchesOut[i].vecMatchPtsSrc.size(); j++)
		{
			vecAllMatchSrc.push_back(vecPanoMatchesOut[i].vecMatchPtsSrc[j]);
		}

		for(size_t m = 0; m < vecPanoMatchesOut[i].vecMatchPtsDst.size(); m++)
		{
			vecAllMatchDst.push_back(vecPanoMatchesOut[i].vecMatchPtsDst[m]);
		}
	}


	for(size_t i = 0; i < vecPanoMatchInfo.size(); i++)
	{
		for(size_t j = 0; j < vecAllMatchSrc.size(); j++)
		{
			if(vecPanoMatchInfo[i].nSrcImgIdx == vecAllMatchSrc[j].ptImgIdxL)
			{
				vecPanoMatchInfo[i].vecMatchPtsSrc.push_back(vecAllMatchSrc[j]);

			}
			if(vecPanoMatchInfo[i].nSrcImgIdx == vecAllMatchDst[j].ptImgIdxL)
			{
				vecPanoMatchInfo[i].vecMatchPtsSrc.push_back(vecAllMatchDst[j]);
			}
		}
	}

	//modify end

	cout<<"Match finished!";

	return true;


}

bool PanoMatchSift(vector<char*> &cParamList, vector<string> &cImageList, vector<double> &dExtParam, char* cOutputFile, vector<PanoMatchInfo> &vecPanoMatchInfo, bool &bNeedAddPts)
{
	int64 app_start_time = getTickCount();      //开始计时
	cv::setBreakOnError(true);
	vector<string> vecImgList;                   //原始图像文件名

	for(size_t imgnum = 0; imgnum < cImageList.size(); imgnum++)//从形参中获取图像名
	{
		vecImgList.push_back(cImageList[imgnum]);//以后只对img_names进行处理
	}
	vector<Mat> vecBrightContrastAdjustedImg(vecImgList.size());   //用于放置亮度和对比度调整后的图像

	int retval = parseCmdArgs(cParamList);      //处理全景拼接的参数
	if (retval)
		return retval;                          //判断参数是否正确

	int num_images = static_cast<int>(vecImgList.size());//原始图像数目
	if (num_images < 2)                         //判断原始图像数目是否正确（应大于等于2）
	{
		LOGLN("Need more images");
		return false;
	}


	if(nBrightAdjust != 0 || nContrastAdjust != 0)
	{
		Mat mAdjustedImg;
		char cFileName[100];

		for(size_t i = 0; i < vecImgList.size(); i++)
		{
			BrightnessContrastAdjust(nBrightAdjust, nContrastAdjust, vecImgList[i], mAdjustedImg);
			vecBrightContrastAdjustedImg[i] = mAdjustedImg.clone();
			sprintf(cFileName,"/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site5/Adjusted_%d.bmp",i);
			imwrite( cFileName, mAdjustedImg);
			int mm = 0;
		}
		bBrightContrastAdjust = true;
	}

	Mat mFullImg, img;//原始图像矩阵
	int nLoopNum = 1;//从第一圈开始(非第0圈)
	int nLoopImgNums = 0;
	int nLoopNums = 0;
	vector< pair<int, int> > vecNearPairs;//
	vector<ImageFeatures> vecFeatures(num_images);//用于存放原始图像特征点
	for (int i = 0; i < num_images; ++i)
	{
		if(bBrightContrastAdjust)
		{
			mFullImg = vecBrightContrastAdjustedImg[i];//如果经过亮度或对比度的调整，则用调整后的图像
		}
		else
		{
			mFullImg = imread(vecImgList[i]);//如果没有经过调整，则直接读取原图像
		}

		vecFeatures[i].img_idx = i;//ImageFeature 内部图像index，从0依次向后排序。
		vecFeatures[i].ori_img_size = mFullImg.size();//图像大小，不经过采样
		vecFeatures[i].export_idx = vecPanoMatchInfo[i].nSrcImgIdx;//接受的外部传入index，4位,从1001到9999
		vecFeatures[i].nLoopID = vecPanoMatchInfo[i].nLoopID;//该图像位于第几圈
		vecFeatures[i].nLoopImgID = vecPanoMatchInfo[i].nLoopImgID;//该图像在本圈内的序号
		vecFeatures[i].bExpMatches = true;//是否输出匹配
		vecFeatures[i].bAddPt = false;//是否需要人工加点

		vecFeatures[i].nByte = (int)mFullImg.elemSize1()*8;//单像素比特数，一般为8bits

		if(nLoopNum < vecFeatures[i].nLoopID)//计算该站原始图像的圈数
			nLoopNum = vecFeatures[i].nLoopID;

		vector<int> vecSingleLoopImgNum;//用于存放单圈中的图像数目
		for(int m= 1; m <= nLoopNum; m++)
		{
			int nLoopImgNum = 0;
			for(int n = 0; n < num_images; ++n)
			{
				if(vecFeatures[n].nLoopID == m)
				{
					nLoopImgNum++;
				}
			}
			vecSingleLoopImgNum.push_back(nLoopImgNum);//每圈有几张图像，可能有每圈图像数不一样的情况
		}
		nLoopImgNums = vecSingleLoopImgNum[0];

	}

	CalNearPairsFour(nLoopImgNums, nLoopNum, vecNearPairs);//只对上下左右四邻域匹配

	vector<MatchPt> vecAllMatchPtsSrc;
	vector<MatchPt> vecAllMatchPtsDst;
	for(size_t i = 0; i < vecNearPairs.size(); i++)
	{
		cout<<"load image"<<endl;
		char *cLImgPath = const_cast<char*>(vecImgList[vecNearPairs[i].first].c_str());
		char *cRImgPath = const_cast<char*>(vecImgList[vecNearPairs[i].second].c_str());
		IplImage *iplLImg = cvLoadImage(cLImgPath, 0);
		IplImage *iplRImg = cvLoadImage(cRImgPath, 0);
		cout<<"load image finish"<<endl;
		int nLW = vecFeatures[vecNearPairs[i].first].ori_img_size.width;
		int nLH = vecFeatures[vecNearPairs[i].first].ori_img_size.height;
		int nRW = vecFeatures[vecNearPairs[i].second].ori_img_size.width;
		int nRH = vecFeatures[vecNearPairs[i].second].ori_img_size.height;
		int nLByte = vecFeatures[vecNearPairs[i].first].nByte;
		int nRByte = vecFeatures[vecNearPairs[i].second].nByte;
		vector<double> vxl, vyl, vxr, vyr;
		MatchesInfo miRanPts;
		int nMaxCheck = 200;
		double dRatio = 0.4;
		cout<<"sift start"<<endl;
		int nPtNum = SiftMatch(iplLImg->imageData, nLW, nLH, nLByte, iplRImg->imageData, nRW, nRH, nRByte, vxl, vyl, vxr, vyr, nMaxCheck, dRatio);
		cout<<vecNearPairs[i].first<<"    "<<vecNearPairs[i].second<<"   "<<nPtNum<<"   SIFT Done!"<<endl;

		vector< pair<int, int> > vecNoMatchPtPair;
		double dThresh = 1;
		double dConfidence = 0.99;
		cout<<"ransac start"<<endl;
		int nRanPts = RansacHomo( vxl, vyl, vxr, vyr, miRanPts, dThresh, dConfidence, vecFeatures[vecNearPairs[i].first], vecFeatures[vecNearPairs[i].second], vecNoMatchPtPair);
		miRanPts.num_inliers = nRanPts;


		vecAllMatchPtsSrc.insert(vecAllMatchPtsSrc.begin(), miRanPts.vecMatchPtsL.begin(), miRanPts.vecMatchPtsL.end());
		vecAllMatchPtsDst.insert(vecAllMatchPtsDst.begin(), miRanPts.vecMatchPtsR.begin(), miRanPts.vecMatchPtsR.end());
		cout<<vecNearPairs[i].first<<"    "<<vecNearPairs[i].second<<"   "<<nRanPts<<"  RANSAC Done!"<<endl<<endl;

		int nm = 0;
	}


	for(size_t i = 0; i < vecPanoMatchInfo.size(); i++)
	{
		for(size_t j = 0; j < vecAllMatchPtsSrc.size(); j++)
		{
			if(vecPanoMatchInfo[i].nSrcImgIdx == vecAllMatchPtsSrc[j].ptImgIdxL)
			{
				vecPanoMatchInfo[i].vecMatchPtsSrc.push_back(vecAllMatchPtsSrc[j]);

			}
			if(vecPanoMatchInfo[i].nSrcImgIdx == vecAllMatchPtsDst[j].ptImgIdxL)
			{
				vecPanoMatchInfo[i].vecMatchPtsSrc.push_back(vecAllMatchPtsDst[j]);
			}
		}
	}


	vector<MatchPt> vecAllMatchSrc;
	vector<MatchPt> vecAllMatchDst;
	for(size_t i = 0; i < vecPanoMatchInfo.size(); i++)
	{
		for(size_t j = 0; j < vecAllMatchSrc.size(); j++)
		{
			if(vecPanoMatchInfo[i].nSrcImgIdx == vecAllMatchSrc[j].ptImgIdxL)
			{
				vecPanoMatchInfo[i].vecMatchPtsSrc.push_back(vecAllMatchSrc[j]);

			}
			if(vecPanoMatchInfo[i].nSrcImgIdx == vecAllMatchDst[j].ptImgIdxL)
			{
				vecPanoMatchInfo[i].vecMatchPtsSrc.push_back(vecAllMatchDst[j]);
			}
		}
	}


	cout<<"Match finished!"<<endl;

	return true;
}


int RansacHomo(vector<double> vxl, vector<double> vyl, vector<double> vxr, vector<double> vyr,\
			   MatchesInfo &miRanPts, double dThresh, double dConfidence, ImageFeatures ifLImg, ImageFeatures ifRImg, vector< pair<int, int> > vecNoMatchPtPair)
{
	UINT nCount = 0;
	if(vxl.size()>0)
	{
		std::vector<cv::Point2f> Lpts,Rpts;
		vector<uchar> Mask0;
		MatchPt ptLMatch;
		MatchPt ptRMatch;
		double dOverlapWSum = 0;
		double dOverlapHSum = 0;
		//转换成cv::Point2f格式匹配点
		for (UINT i = 0; i< vxl.size(); i++)
		{
			Lpts.push_back(cv::Point2f(vxl[i],vyl[i]));
			Rpts.push_back(cv::Point2f(vxr[i],vyr[i]));
		}
		//RANSAC去除粗差，Mask0标记是否为粗差点
		cv::Mat fundamentalMatrix = cv::findFundamentalMat(Lpts, Rpts, cv::FM_RANSAC, dThresh, dConfidence, Mask0);

		for ( UINT j = 0; j < vxl.size(); j++ )
		{
			if( (SINT)Mask0[j] == 1 )
			{
				ptLMatch.x = Lpts[j].x;
				ptLMatch.y = Lpts[j].y;
				ptLMatch.ptImgIdxL = ifLImg.export_idx;
				ptLMatch.ptImgIdxR = ifRImg.export_idx;
				ptLMatch.ptIdx = ifLImg.export_idx * 10e11 + ifRImg.export_idx  * 10e7 + j + 1;
				ptRMatch.x = Rpts[j].x;
				ptRMatch.y = Rpts[j].y;
				ptRMatch.ptImgIdxL = ifRImg.export_idx;
				ptRMatch.ptImgIdxR = ifLImg.export_idx;
				ptRMatch.ptIdx = ptLMatch.ptIdx;
				miRanPts.vecMatchPtsL.push_back(ptLMatch);
				miRanPts.vecMatchPtsR.push_back(ptRMatch);

				//计算重叠区大小
				dOverlapWSum += ifLImg.ori_img_size.width - ptLMatch.x + ptRMatch.x;//由所有点计算得到的重叠区宽度之和
				dOverlapHSum += MIN(ptLMatch.y, ptRMatch.y) + MIN(ifLImg.ori_img_size.height-ptLMatch.y, ifRImg.ori_img_size.height-ptRMatch.y);//由所有点计算得到的重叠区高度之和


				nCount++;
			}
			else
			{
				continue;
			}
		}



		//将匹配点数目减少至20个以内，以加快后续光束法平差速度------------
		if(nCount >= 20)//只对大于20个点的匹配对筛点
		{
			double dOverlapW = dOverlapWSum / nCount;//重叠区宽度
			double dOverlapH = dOverlapHSum / nCount;//重叠区高度
			int nGridStep = 3;//把重叠区分为3*3格网
			double dGridW = dOverlapW / nGridStep;//格网大小（宽）
			double dGridH = dOverlapH / nGridStep;//格网大小（高）
			vector< vector<MatchPt> > vecGridPt(nGridStep*nGridStep);//用于存放不同格网内的点
			double dOvlpULX = ifLImg.ori_img_size.width - dOverlapW;//重叠区左上角的X坐标
			double dOvlpULY = ifLImg.ori_img_size.height - dOverlapH;//重叠区左上角的Y坐标
			vector<MatchPt> vecReducedLPts;//存放左片筛减后的点
			vector<MatchPt> vecReducedRPts;//存放右片筛减后的点
			int nReducedPtsNum = 0;

			for(size_t u = 0; u < miRanPts.vecMatchPtsL.size(); u++)//只对左片筛点，右片根据左片点的id查找到同名点
			{

				int nGridX = (int)(abs((miRanPts.vecMatchPtsL[u].x - dOvlpULX)) / dGridW);//计算该点位于第几个格网
				int nGridY = (int)(abs((miRanPts.vecMatchPtsL[u].y - dOvlpULY)) / dGridH);

				if(nGridX>=0&&nGridX<=2&&nGridY>=0&&nGridY<=2)
				{
					vecGridPt[nGridX+nGridY*nGridStep].push_back(miRanPts.vecMatchPtsL[u]);//将位于同一个格网的点压入同一个vector中，总共有9个格网
				}

			}

			for(size_t i = 0; i < vecGridPt.size(); i++)
			{
				if(vecGridPt[i].size()>=2)
				{
					nReducedPtsNum += 2;
				}
				else
				{
					nReducedPtsNum += vecGridPt[i].size();
				}
			}


			if(nReducedPtsNum <= 4)//如果原来大于20个点经筛除后少于4对（计算姿态必须有4对同名点），则在有点的网格取4个点，以满足要求
			{
				//由于经筛减后总点数少于4个，所以需要对每一个格网取4个点，并将所有格网中的点压入同一个vecReducedLPts，替换筛点前的vecMatchPtsL
				for(size_t i = 0; i < vecGridPt.size(); i++)
				{

					if(vecGridPt[i].size()>=4)//如果一个格网中的点多于4个，则取其中y坐标相差最大的两个
					{
						std::sort(vecGridPt[i].begin(), vecGridPt[i].end(), SortYMinToMax);
						vecReducedLPts.push_back(vecGridPt[i][0]);
						vecReducedLPts.push_back(vecGridPt[i][1]);
						vecReducedLPts.push_back(vecGridPt[i][2]);
						vecReducedLPts.push_back(vecGridPt[i][vecGridPt[i].size()-1]);
					}
					else //如果一个格网中的点少于4个，则取其中所有的点
					{
						//for(size_t j = 0; j < )
						vecReducedLPts.insert(vecReducedLPts.end(), vecGridPt[i].begin(), vecGridPt[i].end());
					}

				}
			}
			else//如果原来大于20个点经筛除后多于4对（计算姿态必须有四对同名点），则在每一个网格只取2个点，以满足分布均匀
			{
				//对每一个格网只取2个点，并将所有格网中的点压入同一个vecReducedLPts，替换筛点前的vecMatchPtsL
				for(size_t i = 0; i < vecGridPt.size(); i++)
				{

					if(vecGridPt[i].size()>=2)//如果一个格网中的点多于2个，则取其中y坐标相差最大的两个
					{
						std::sort(vecGridPt[i].begin(), vecGridPt[i].end(), SortYMinToMax);
						vecReducedLPts.push_back(vecGridPt[i][0]);
						vecReducedLPts.push_back(vecGridPt[i][vecGridPt[i].size()-1]);
					}
					else if(vecGridPt[i].size() == 1)//如果一个格网中的点少于2个，则取其中所有的点
					{
						vecReducedLPts.push_back(vecGridPt[i][0]);
					}

				}
			}



			//如果原来大于20个点经筛除后少于4个（），则允许在某些网格内的点多余两个
			if(vecReducedLPts.size() <= 4)
			{

			}

			//根据左片筛减后的点，在右片根据16位id找到同名点，压入同一个vecReducedRPts，替换筛点前的vecMatchPtsR
			for(size_t i = 0; i < vecReducedLPts.size(); i++)
			{
				for(size_t j = 0; j < miRanPts.vecMatchPtsR.size(); j++)
				{
					if(vecReducedLPts[i].ptIdx == miRanPts.vecMatchPtsR[j].ptIdx)//根据id找到右片同名点
					{
						vecReducedRPts.push_back(miRanPts.vecMatchPtsR[j]);
					}
				}
			}

			//替换原有的点
			miRanPts.vecMatchPtsL = vecReducedLPts;
			miRanPts.vecMatchPtsR = vecReducedRPts;

			nReducedPtsNum = 0;



			//            ofstream testOut;
			//            testOut.open("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site4/testreduceSIFT20.txt", ios::app);
			//            for(int i = 0; i < vecGridPt.size(); i++)
			//            {
			//                testOut<<endl<<"GridNo"<<i<<endl;
			//                testOut<<"("<<dOvlpULX +i*dGridW<<", "<< dOvlpULY +i*dGridH<<")"<<"---"<<"("<<dOvlpULX +(i+1)*dGridW<<", "<< dOvlpULY +(i+1)*dGridH<<")"<<endl;
			//                for(int b = 0; b < vecGridPt[i].size(); b++)
			//                {
			//                    testOut<<"x = "<<vecGridPt[i][b].x<<"     "<<"y = "<<vecGridPt[i][b].y<<endl;
			//                }
			//                testOut<<endl;
			//                testOut<<endl;
			//            }

			int enend = 0;
		}
	}

	else
	{
		vector< pair<int, int> > vecNoMatchPtPair;
		pair<int, int> pirNoMatchPtPair;
		pirNoMatchPtPair.first = ifLImg.img_idx;
		pirNoMatchPtPair.second = ifRImg.img_idx;
		vecNoMatchPtPair.push_back(pirNoMatchPtPair);
	}




	return nCount;
}














