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
#include <algorithm>
#include <functional>
#include "matchers.hpp"
#include "util.hpp"

#include "fstream"

using namespace std;
using namespace cv;
using namespace cv::gpu;


//////////////////////////////////////////////////////////////////////////////

void FeaturesFinder::operator ()(const Mat &image, ImageFeatures &features)
{
	find(image, features);
	features.img_size = image.size();
	//features.img = image.clone();
}

//////////////////////////////////////////////////////////////////////////////

namespace
{
	class CpuSurfFeaturesFinder : public FeaturesFinder
	{
	public:
		CpuSurfFeaturesFinder(double hess_thresh, int num_octaves, int num_layers,
			int num_octaves_descr, int num_layers_descr)
		{
			detector_ = new SurfFeatureDetector(hess_thresh, num_octaves, num_layers);
			extractor_ = new SurfDescriptorExtractor(num_octaves_descr, num_layers_descr);
		}

	protected:
		void find(const Mat &image, ImageFeatures &features);

	private:
		Ptr<FeatureDetector> detector_;
		Ptr<DescriptorExtractor> extractor_;
	};


	class GpuSurfFeaturesFinder : public FeaturesFinder
	{
	public:
		GpuSurfFeaturesFinder(double hess_thresh, int num_octaves, int num_layers,
			int num_octaves_descr, int num_layers_descr)
		{
			surf_.keypointsRatio = 0.1f;
			surf_.hessianThreshold = hess_thresh;
			surf_.extended = false;
			num_octaves_ = num_octaves;
			num_layers_ = num_layers;
			num_octaves_descr_ = num_octaves_descr;
			num_layers_descr_ = num_layers_descr;
		}

	protected:
		void find(const Mat &image, ImageFeatures &features);

	private:
		SURF_GPU surf_;
		int num_octaves_, num_layers_;
		int num_octaves_descr_, num_layers_descr_;
	};


	void CpuSurfFeaturesFinder::find(const Mat &image, ImageFeatures &features)
	{
		Mat gray_image;
		CV_Assert(image.depth() == CV_8U);
		cvtColor(image, gray_image, CV_BGR2GRAY);
		detector_->detect(gray_image, features.keypoints);
		extractor_->compute(gray_image, features.keypoints, features.descriptors);
	}


	void GpuSurfFeaturesFinder::find(const Mat &image, ImageFeatures &features)
	{
		GpuMat gray_image;
		CV_Assert(image.depth() == CV_8U);
		cvtColor(GpuMat(image), gray_image, CV_BGR2GRAY);

		GpuMat d_keypoints;
		GpuMat d_descriptors;
		surf_.nOctaves = num_octaves_;
		surf_.nOctaveLayers = num_layers_;
		surf_(gray_image, GpuMat(), d_keypoints);

		surf_.nOctaves = num_octaves_descr_;
		surf_.nOctaveLayers = num_layers_descr_;
		surf_(gray_image, GpuMat(), d_keypoints, d_descriptors, true);
		surf_.downloadKeypoints(d_keypoints, features.keypoints);

		d_descriptors.download(features.descriptors);
	}
} // anonymous namespace


SurfFeaturesFinder::SurfFeaturesFinder(bool try_use_gpu, double hess_thresh, int num_octaves, int num_layers,
									   int num_octaves_descr, int num_layers_descr)
{
	if (try_use_gpu && getCudaEnabledDeviceCount() > 0)
		impl_ = new GpuSurfFeaturesFinder(hess_thresh, num_octaves, num_layers, num_octaves_descr, num_layers_descr);
	else
		impl_ = new CpuSurfFeaturesFinder(hess_thresh, num_octaves, num_layers, num_octaves_descr, num_layers_descr);
}


void SurfFeaturesFinder::find(const Mat &image, ImageFeatures &features)
{
	(*impl_)(image, features);
}


//////////////////////////////////////////////////////////////////////////////

MatchesInfo::MatchesInfo() : src_img_idx(-1), dst_img_idx(-1), num_inliers(0), confidence(0) {}

MatchesInfo::MatchesInfo(const MatchesInfo &other)
{
	*this = other;
}

const MatchesInfo& MatchesInfo::operator =(const MatchesInfo &other)
{
	src_img_idx = other.src_img_idx;
	dst_img_idx = other.dst_img_idx;
	matches = other.matches;
	inliers_mask = other.inliers_mask;
	num_inliers = other.num_inliers;
	H = other.H.clone();
	confidence = other.confidence;

	exp_idx_l = other.exp_idx_l;
	exp_idx_r = other.exp_idx_r;
	sImgPathSrc = other.sImgPathSrc;
	sImgPathDst = other.sImgPathDst;
	vecSrcPt = other.vecSrcPt;
	vecMatchPtsL = other.vecMatchPtsL;
	vecMatchPtsR = other.vecMatchPtsR;

	return *this;
}


//////////////////////////////////////////////////////////////////////////////

struct DistIdxPair
{
	bool operator<(const DistIdxPair &other) const
	{
		return dist < other.dist;
	}
	double dist;
	int idx;
};


struct MatchPairsBody
{
	MatchPairsBody(const MatchPairsBody& other)
		: matcher(other.matcher), features(other.features),
		pairwise_matches(other.pairwise_matches), near_pairs(other.near_pairs) {}

	MatchPairsBody(FeaturesMatcher &matcher, const vector<ImageFeatures> &features,
		vector<MatchesInfo> &pairwise_matches, vector<pair<int,int> > &near_pairs)
		: matcher(matcher), features(features),
		pairwise_matches(pairwise_matches), near_pairs(near_pairs) {}

	//MatchPairsBody body(*this, features, pairwise_matches, near_pairs);//转到209行

	//    void operator ()(const BlockedRange &r) const
	//    {
	//        const int num_images = static_cast<int>(features.size());
	//        for (int i = r.begin(); i < r.end(); ++i)
	//        {
	//            int from = near_pairs[i].first;
	//            int to = near_pairs[i].second;
	//            int pair_idx = from*num_images + to;
	//
	//            matcher(features[from], features[to], pairwise_matches[pair_idx]);//转到480行
	//            pairwise_matches[pair_idx].src_img_idx = from;
	//            pairwise_matches[pair_idx].dst_img_idx = to;
	//
	//            size_t dual_pair_idx = to*num_images + from;
	//
	//            pairwise_matches[dual_pair_idx] = pairwise_matches[pair_idx];
	//            pairwise_matches[dual_pair_idx].src_img_idx = to;
	//            pairwise_matches[dual_pair_idx].dst_img_idx = from;
	//
	//            if (!pairwise_matches[pair_idx].H.empty())
	//                pairwise_matches[dual_pair_idx].H = pairwise_matches[pair_idx].H.inv();
	//
	//            for (size_t j = 0; j < pairwise_matches[dual_pair_idx].matches.size(); ++j)
	//                swap(pairwise_matches[dual_pair_idx].matches[j].queryIdx,
	//                     pairwise_matches[dual_pair_idx].matches[j].trainIdx);
	//            LOG(".");
	//        }
	//    }

	void operator ()(const BlockedRange &r) const
	{
		const int num_images = static_cast<int>(features.size());
		for (int i = r.begin(); i < r.end(); ++i)
			//for (int i = 0; i < near_pairs.size(); ++i)
		{
			int from = near_pairs[i].first;
			int to = near_pairs[i].second;
			int pair_idx = from*num_images + to;

			matcher(features[from], features[to], pairwise_matches[pair_idx]);//matcher为结构体中成员，定义在line276
			pairwise_matches[pair_idx].src_img_idx = from;
			pairwise_matches[pair_idx].dst_img_idx = to;

			size_t dual_pair_idx = to*num_images + from;

			pairwise_matches[dual_pair_idx] = pairwise_matches[pair_idx];
			pairwise_matches[dual_pair_idx].src_img_idx = to;
			pairwise_matches[dual_pair_idx].dst_img_idx = from;

			if (!pairwise_matches[pair_idx].H.empty())
				pairwise_matches[dual_pair_idx].H = pairwise_matches[pair_idx].H.inv();

			for (size_t j = 0; j < pairwise_matches[dual_pair_idx].matches.size(); ++j)
				swap(pairwise_matches[dual_pair_idx].matches[j].queryIdx,
				pairwise_matches[dual_pair_idx].matches[j].trainIdx);
			LOG(".");
		}
	}


	FeaturesMatcher &matcher;
	const vector<ImageFeatures> &features;
	vector<MatchesInfo> &pairwise_matches;
	vector<pair<int,int> > &near_pairs;

private:
	void operator =(const MatchPairsBody&);
};


//void FeaturesMatcher::operator ()(const vector<ImageFeatures> &features, vector<MatchesInfo> &pairwise_matches)
//{
//    const int num_images = static_cast<int>(features.size());
//
//    vector<pair<int,int> > near_pairs;
//    for (int i = 0; i < num_images - 1; ++i)
//        for (int j = i + 1; j < num_images; ++j)
//            near_pairs.push_back(make_pair(i, j));
//
//    pairwise_matches.resize(num_images * num_images);
//    MatchPairsBody body(*this, features, pairwise_matches, near_pairs);
//
//    if (is_thread_safe_)
//        parallel_for(BlockedRange(0, static_cast<int>(near_pairs.size())), body);
//    else
//        body(BlockedRange(0, static_cast<int>(near_pairs.size())));
//    LOGLN("");
//}

void FeaturesMatcher::operator ()(const vector<ImageFeatures> &features, vector<MatchesInfo> &pairwise_matches, bool bOnlyReadMatchPts)
{
	const int num_images = static_cast<int>(features.size());

	vector<pair<int,int> > near_pairs;
	for (int i = 0; i < num_images - 1; ++i)
		for (int j = i + 1; j < num_images; ++j)
			near_pairs.push_back(make_pair(i, j));

	pairwise_matches.resize(num_images * num_images);
	MatchPairsBody body(*this, features, pairwise_matches, near_pairs);

	if (is_thread_safe_)
		parallel_for(BlockedRange(0, static_cast<int>(near_pairs.size())), body);
	else
		body(BlockedRange(0, static_cast<int>(near_pairs.size())));
	LOGLN("");
}



//固定图像顺序的匹配
void FeaturesMatcher::operator ()(const vector<ImageFeatures> &features, vector<MatchesInfo> &pairwise_matches)//匹配入口
{
	const int num_images = static_cast<int>(features.size());
	int nLoopImgNum = features[0].nLoopImgNum;//一圈有几张图像
	int nLoopNum = features[0].nLoopNum;//一共有几圈
	vector< pair<int,int> > near_pairs;//去掉重复后的匹配对
	calNearPairs(nLoopImgNum, nLoopNum, near_pairs);

	////    int nImgNum = nLoopImgNum * nLoopNum;//总共的图像数
	////
	////    vector< pair<int,int> > near_pairs_tmp;//具有重复的匹配对，如1-2，2-1
	////    vector< pair<int,int> > near_pairs;//去掉重复后的匹配对
	//////    for (int i = 0; i < num_images - 1; ++i)
	//////        for (int j = i + 1; j < num_images; ++j)
	//////            near_pairs.push_back(make_pair(i, j));
	////
	//////确定原始图像顺序
	////    for(int i = 1; i <= nImgNum; ++i)
	////    ///for(int i = 0; i < nImgNum; i++)
	////    {
	////        int nLoopID = (int)(i / (nLoopImgNum+0.5)) + 1;
	////
	////        if((i%nLoopImgNum != 0)&&(i%nLoopImgNum != 1))//中间部分的图像
	////        {
	////            for(int a = i-nLoopImgNum-1; a <= i-nLoopImgNum+1; a++)
	////            {
	////                if(a>0&&a<=nImgNum)
	////                {
	////                    //near_pairs.push_back(make_pair(i,a));
	////                    near_pairs_tmp.push_back(make_pair(i-1,a-1));
	////                }
	////            }
	////            for(int b = i-1; b <= i+1; b+=2)
	////            {
	////                if(b>0&&b<=nImgNum)
	////                {
	////                    //near_pairs.push_back(make_pair(i,b));
	////                    near_pairs_tmp.push_back(make_pair(i-1,b-1));
	////                }
	////            }
	////            for(int c = i+nLoopImgNum-1; c <= i+nLoopImgNum+1; c++)
	////            {
	////                if(c>0&&c<=nImgNum)
	////                {
	////                    //near_pairs.push_back(make_pair(i,c));
	////                    near_pairs_tmp.push_back(make_pair(i-1,c-1));
	////                }
	////            }
	////        }
	////
	////        else if(i == 1)//左上角
	////        {
	////            for(int d = i+1; d<i+2;d++)
	////            {
	////                if(d>0&&d<=nImgNum)
	////                //near_pairs.push_back(make_pair(i,d));
	////                near_pairs_tmp.push_back(make_pair(i-1,d-1));
	////            }
	////            for(int e = i+nLoopImgNum-1;e<=i+nLoopImgNum+1;e++)
	////            {
	////                if(e>0&&e<=nImgNum)
	////                //near_pairs.push_back(make_pair(i,e));
	////                near_pairs_tmp.push_back(make_pair(i-1,e-1));
	////            }
	////            if(nLoopImgNum*(nLoopID+1)>0&&nLoopImgNum*(nLoopID+1)<=nImgNum)
	////                //near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID+1)));
	////                near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID+1)-1));
	////
	////        }
	////        else if(i == nLoopImgNum)//右上角
	////        {
	////            for(int f = i-1;f<=i+1;f+=2)
	////            {
	////                if(f>0&&f<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, f));
	////                    near_pairs_tmp.push_back(make_pair(i-1, f-1));
	////            }
	////            for(int g = i+nLoopImgNum-1; g <= i+nLoopImgNum; g++)
	////            {
	////                if(g>0&&g<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, g));
	////                    near_pairs_tmp.push_back(make_pair(i-1, g-1));
	////            }
	////            //near_pairs.push_back(make_pair(i,1));
	////            near_pairs_tmp.push_back(make_pair(i-1,1-1));
	////        }
	////        else if(i == nLoopImgNum*(nLoopNum-1)+1)//左下角
	////        {
	////            for(int h = i-1; h<=i+1; h+=2)
	////            {
	////                if(h>0&&h<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, h));
	////                    near_pairs_tmp.push_back(make_pair(i-1, h-1));
	////            }
	////            for(int k = i-nLoopImgNum; k<=i-nLoopImgNum+1; k++)
	////            {
	////                if(k>0&&k<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, k));
	////                    near_pairs_tmp.push_back(make_pair(i-1, k-1));
	////            }
	////            //near_pairs.push_back(make_pair(i,nLoopImgNum*nLoopNum));
	////            near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*nLoopNum-1));
	////        }
	////        else if(i == nLoopImgNum*nLoopNum)//右下角
	////        {
	////            for(int l = i-1;l<i;l++)
	////            {
	////                if(l>0&&l<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, l));
	////                    near_pairs_tmp.push_back(make_pair(i-1, l-1));
	////            }
	////            for(int m = i-nLoopImgNum-1; m<=i-nLoopImgNum+1; m++)
	////            {
	////                if(m>0&&m<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, m));
	////                    near_pairs_tmp.push_back(make_pair(i-1, m-1));
	////            }
	////            if(nLoopImgNum*(nLoopID-2)+1>0&&nLoopImgNum*(nLoopID-2)+1<=nImgNum)
	////                //near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID-2)+1));
	////                near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID-2)+1-1));
	////        }
	////        else if((i==nLoopImgNum*(nLoopID-1)+1)//左边边界
	////                &&(i!=1)
	////                &&(i!=nLoopImgNum*(nLoopNum-1)+1))
	////        {
	////            for(int n = i-nLoopImgNum; n<=i-nLoopImgNum+1; n++)
	////            {
	////                if(n>0&&n<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, n));
	////                    near_pairs_tmp.push_back(make_pair(i-1, n-1));
	////            }
	////            for(int o = i-1; o<=i+1;o+=2)
	////            {
	////                if(o>0&&o<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i,o));
	////                    near_pairs_tmp.push_back(make_pair(i-1,o-1));
	////            }
	////            for(int p = i+nLoopImgNum-1; p<=i+nLoopImgNum+1; p++)
	////            {
	////                if(p>0&&p<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i,p));
	////                    near_pairs_tmp.push_back(make_pair(i-1,p-1));
	////            }
	////            if(nLoopImgNum*(nLoopID+1)>0&&nLoopImgNum*(nLoopID+1)<=nImgNum)
	////                //near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID+1)));
	////                near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID+1)-1));
	////        }
	////        else if((i == nLoopImgNum*nLoopID)//右边边界
	////                &&(i!=nLoopImgNum)
	////                &&(i!=nLoopImgNum*nLoopNum))
	////        {
	////            for(int q = i-nLoopImgNum-1; q<=i-nLoopImgNum+1; q++)
	////            {
	////                if(q>0&&q<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, q));
	////                    near_pairs_tmp.push_back(make_pair(i-1, q-1));
	////            }
	////            for(int r = i-1;r<=i+1;r+=2)
	////            {
	////                if(r>0&&r<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, r));
	////                    near_pairs_tmp.push_back(make_pair(i-1, r-1));
	////            }
	////            for(int s = i+nLoopImgNum-1; s <= i+nLoopImgNum; s++)
	////            {
	////                if(s>0&&s<=nImgNum)
	////                    //near_pairs.push_back(make_pair(i, s));
	////                    near_pairs_tmp.push_back(make_pair(i-1, s-1));
	////            }
	////            if(nLoopImgNum*(nLoopID-1)+1>0&&nLoopImgNum*(nLoopID-1)+1<=nImgNum)
	////                //near_pairs.push_back(make_pair(i,nLoopImgNum*(nLoopID-2)+1));
	////                near_pairs_tmp.push_back(make_pair(i-1,nLoopImgNum*(nLoopID-2)+1-1));
	////
	////        }
	////
	////    }
	////
	////
	//////去掉near_pairs中的重复匹配
	////    vector< pair<int, int> >::iterator itDup1;
	////    vector< pair<int, int> >::iterator itDup2;
	////    for(itDup1 = near_pairs_tmp.end(); itDup1 != near_pairs_tmp.begin(); itDup1--)
	////    {
	////        for(itDup2 = near_pairs_tmp.begin(); itDup2 != near_pairs_tmp.end(); itDup2++)
	////        {
	////            if(itDup1->first==itDup2->second && itDup1->second == itDup2->first)
	////            {
	////                near_pairs_tmp.erase(itDup1);
	////            }
	////        }
	////    }
	////    near_pairs =near_pairs_tmp;

	pairwise_matches.resize(num_images * num_images);
	//    pairwise_matches.resize(near_pairs.size());

	MatchPairsBody body(*this, features, pairwise_matches, near_pairs);//转到209行

	/*****************************************************/
	//    ofstream test2;
	//    test2.open("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site4/testoutnearnodup.txt", ios::app);
	//    for(int i = 0; i < near_pairs.size(); i++)
	//    {
	//        test2<<near_pairs[i].first<<"  "<<near_pairs[i].second<<endl;
	//    }
	//
	//    test2<<"nLoopImgNum= "<<nLoopImgNum<<"  nLoopNum= "<<nLoopNum<<"  nImgNum= "<<nImgNum<<endl;
	/*****************************************************/

	if (is_thread_safe_)
		parallel_for(BlockedRange(0, static_cast<int>(near_pairs.size())), body);
	else
		body(BlockedRange(0, static_cast<int>(near_pairs.size())));
	LOGLN("");
}

void FeaturesMatcher::calNearPairs(int nLoopImgNum, int nLoopNum, vector< pair<int, int> > &vecNearPairs)
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
	int nm = 0;
}


//////////////////////////////////////////////////////////////////////////////

namespace
{
	class PairLess
	{
	public:
		bool operator()(const pair<int,int>& l, const pair<int,int>& r) const
		{
			return l.first < r.first || (l.first == r.first && l.second < r.second);
		}
	};
	typedef set<pair<int,int>,PairLess> MatchesSet;

	// These two classes are aimed to find features matches only, not to
	// estimate homography

	class CpuMatcher : public FeaturesMatcher
	{
	public:
		CpuMatcher(float match_conf) : FeaturesMatcher(true), match_conf_(match_conf) {}
		void match(const ImageFeatures &features1, const ImageFeatures &features2, MatchesInfo& matches_info);

	private:
		float match_conf_;
	};


	class GpuMatcher : public FeaturesMatcher
	{
	public:
		GpuMatcher(float match_conf) : match_conf_(match_conf) {}
		void match(const ImageFeatures &features1, const ImageFeatures &features2, MatchesInfo& matches_info);

	private:
		float match_conf_;
		GpuMat descriptors1_, descriptors2_;
		GpuMat train_idx_, distance_, all_dist_;
	};


	void CpuMatcher::match(const ImageFeatures &features1, const ImageFeatures &features2, MatchesInfo& matches_info)
	{
		matches_info.matches.clear();

		FlannBasedMatcher matcher;
		vector< vector<DMatch> > pair_matches;
		MatchesSet matches;

		// Find 1->2 matches

		// Find k best matches for each query descriptor (in increasing order of distances).
		// compactResult is used when mask is not empty. If compactResult is false matches
		// vector will have the same size as queryDescriptors rows. If compactResult is true
		// matches vector will not contain matches for fully masked out query descriptors.
		matcher.knnMatch(features1.descriptors, features2.descriptors, pair_matches, 2);
		for (size_t i = 0; i < pair_matches.size(); ++i)
		{
			if (pair_matches[i].size() < 2)
				continue;
			const DMatch& m0 = pair_matches[i][0];
			const DMatch& m1 = pair_matches[i][1];
			if (m0.distance < (1.f - match_conf_) * m1.distance)
			{
				matches_info.matches.push_back(m0);
				matches.insert(make_pair(m0.queryIdx, m0.trainIdx));
			}
		}

		// Find 2->1 matches
		pair_matches.clear();
		matcher.knnMatch(features2.descriptors, features1.descriptors, pair_matches, 2);
		for (size_t i = 0; i < pair_matches.size(); ++i)
		{
			if (pair_matches[i].size() < 2)
				continue;
			const DMatch& m0 = pair_matches[i][0];
			const DMatch& m1 = pair_matches[i][1];
			if (m0.distance < (1.f - match_conf_) * m1.distance)
				if (matches.find(make_pair(m0.trainIdx, m0.queryIdx)) == matches.end())
					matches_info.matches.push_back(DMatch(m0.trainIdx, m0.queryIdx, m0.distance));
		}

		/*****************************************************/
		ofstream test1;
		test1.open("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site4/testoutcpu.txt", ios::app);
		test1<<"nLoopID: "<<features1.nLoopID<<"   nLoopImgID: "<<features1.nLoopImgID<<"   img_idx: "<<features1.img_idx<<endl<< \
			"nLoopID: "<<features2.nLoopID<<"   nLoopImgID: "<<features2.nLoopImgID<<"   img_idx: "<<features2.img_idx<<endl<<endl;
		/*****************************************************/




	}


	void GpuMatcher::match(const ImageFeatures &features1, const ImageFeatures &features2, MatchesInfo& matches_info)
	{
		matches_info.matches.clear();
		descriptors1_.upload(features1.descriptors);
		descriptors2_.upload(features2.descriptors);
		BruteForceMatcher_GPU< L2<float> > matcher;
		vector< vector<DMatch> > pair_matches;
		MatchesSet matches;

		// Find 1->2 matches
		matcher.knnMatch(descriptors1_, descriptors2_, train_idx_, distance_, all_dist_, 2);
		matcher.knnMatchDownload(train_idx_, distance_, pair_matches);
		for (size_t i = 0; i < pair_matches.size(); ++i)
		{
			if (pair_matches[i].size() < 2)
				continue;
			const DMatch& m0 = pair_matches[i][0];
			const DMatch& m1 = pair_matches[i][1];
			if (m0.distance < (1.f - match_conf_) * m1.distance)
			{
				matches_info.matches.push_back(m0);
				matches.insert(make_pair(m0.queryIdx, m0.trainIdx));
			}
		}

		// Find 2->1 matches
		pair_matches.clear();
		matcher.knnMatch(descriptors2_, descriptors1_, train_idx_, distance_, all_dist_, 2);
		matcher.knnMatchDownload(train_idx_, distance_, pair_matches);
		for (size_t i = 0; i < pair_matches.size(); ++i)
		{
			if (pair_matches[i].size() < 2)
				continue;
			const DMatch& m0 = pair_matches[i][0];
			const DMatch& m1 = pair_matches[i][1];
			if (m0.distance < (1.f - match_conf_) * m1.distance)
				if (matches.find(make_pair(m0.trainIdx, m0.queryIdx)) == matches.end())
					matches_info.matches.push_back(DMatch(m0.trainIdx, m0.queryIdx, m0.distance));
		}
	}

} // anonymous namespace

// 跟进Match_1 Step1
static int image_name = 0;
BestOf2NearestMatcher::BestOf2NearestMatcher(bool try_use_gpu, float match_conf, int num_matches_thresh1, int num_matches_thresh2)
{
	if (try_use_gpu && getCudaEnabledDeviceCount() > 0)
		impl_ = new GpuMatcher(match_conf);
	else
		impl_ = new CpuMatcher(match_conf);

	is_thread_safe_ = impl_->isThreadSafe();
	num_matches_thresh1_ = num_matches_thresh1;
	num_matches_thresh2_ = num_matches_thresh2;
}



//重要：CPU匹配函数
void BestOf2NearestMatcher::match(const ImageFeatures &features1, const ImageFeatures &features2,
								  MatchesInfo &matches_info)
{
	(*impl_)(features1, features2, matches_info);

	/*****************************************************/

	ofstream test;
	test.open("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site5/testoutpt.txt", ios::app);
	//    test<<"nLoopID: "<<features1.nLoopID<<"   nLoopImgID: "<<features1.nLoopImgID<<"   img_idx: "<<features1.img_idx<<endl<< \
	//    "nLoopID: "<<features2.nLoopID<<"   nLoopImgID: "<<features2.nLoopImgID<<"   img_idx: "<<features2.img_idx<<endl<<endl;

	/*****************************************************/





	if(!features1.bAddPt)//是否输出匹配点
	{
		// Check if it makes sense to find homography
		if (matches_info.matches.size() < static_cast<size_t>(num_matches_thresh1_))
			return;

		// Construct point-point correspondences for homography estimation
		Mat src_points(1, matches_info.matches.size(), CV_32FC2);
		Mat dst_points(1, matches_info.matches.size(), CV_32FC2);
		for (size_t i = 0; i < matches_info.matches.size(); ++i)
		{
			const DMatch& m = matches_info.matches[i];
			Point2f p = features1.keypoints[m.queryIdx].pt;
			p.x -= features1.img_size.width * 0.5f;
			p.y -= features1.img_size.height * 0.5f;

			src_points.at<Point2f>(0, i) = p;

			p = features2.keypoints[m.trainIdx].pt;

			p.x -= features2.img_size.width * 0.5f;
			p.y -= features2.img_size.height * 0.5f;
			dst_points.at<Point2f>(0, i) = p;

		}

		cout<<endl<<"src_points:"<<endl;
		LOG(src_points);
		cout<<endl;
		cout<<endl<<"dst_points:"<<endl;
		LOG(dst_points);
		cout<<endl;
		LOG(matches_info.matches.size());

		// Find pair-wise motion
		matches_info.H = findHomography(src_points, dst_points, matches_info.inliers_mask, CV_RANSAC);

		// Find number of inliers
		matches_info.num_inliers = 0;
		for(size_t i = 0; i < matches_info.inliers_mask.size(); ++i)
		{
			if (matches_info.inliers_mask[i])
				matches_info.num_inliers++;

			LOG(matches_info.inliers_mask[i]);
		}

		matches_info.confidence = matches_info.num_inliers / (8 + 0.3*matches_info.matches.size());

		// Check if we should try to refine motion
		if (matches_info.num_inliers < num_matches_thresh2_)
			return;


		//只对内点计算单应矩阵
		src_points.create(1, matches_info.num_inliers, CV_32FC2);//用于存放左片点
		dst_points.create(1, matches_info.num_inliers, CV_32FC2);//用于存放右片点
		int inlier_idx = 0;//内点数量
		signed int nPtNum = 0;

		vector<Point2f> vecOriPtL;
		vector<Point2f> vecOriPtR;

		MatchPt struMptL;
		MatchPt struMptR;

		double dOverlapWSum = 0;//采用对所有点计算得到的重叠区做平均处理，这是所有点计算得到重叠区的宽度之和
		double dOverlapHSum = 0;//采用对所有点计算得到的重叠区做平均处理，这是所有点计算得到重叠区的高度之和

		for (size_t i = 0; i < matches_info.matches.size(); ++i)
		{
			if(!matches_info.inliers_mask[i])
				continue;

			const DMatch& m = matches_info.matches[i];

			Point2f p = features1.keypoints[m.queryIdx].pt;

			Point2f ptOriginLeft = p;

			ptOriginLeft.x = ptOriginLeft.x * features1.ori_img_size.width/features1.img_size.width;
			ptOriginLeft.y = ptOriginLeft.y * features1.ori_img_size.height/features1.img_size.height;
			p.x -= features1.img_size.width * 0.5f;
			p.y -= features1.img_size.height * 0.5f;
			struMptL.x = ptOriginLeft.x;
			struMptL.y = ptOriginLeft.y;
			struMptL.ptIdx = features1.export_idx * 10e11 + features2.export_idx  * 10e7 + nPtNum + 1;

			struMptL.ptImgIdxL = features1.export_idx;
			struMptL.ptImgIdxR = features2.export_idx;
			matches_info.exp_idx_l = features1.export_idx;
			matches_info.exp_idx_r = features2.export_idx;
			matches_info.sImgPathSrc = features1.sImgPathSrc;
			matches_info.sImgPathDst = features2.sImgPathSrc;
			matches_info.vecMatchPtsL.push_back(struMptL);

			vecOriPtL.push_back(ptOriginLeft);

			src_points.at<Point2f>(0, inlier_idx) = p;

			p = features2.keypoints[m.trainIdx].pt;
			Point2f ptOriginRight = p;

			ptOriginRight.x = ptOriginRight.x * features2.ori_img_size.width/features2.img_size.width;
			ptOriginRight.y = ptOriginRight.y * features2.ori_img_size.width/features2.img_size.height;

			p.x -= features2.img_size.width * 0.5f;
			p.y -= features2.img_size.height * 0.5f;
			struMptR.x = ptOriginRight.x;
			struMptR.y = ptOriginRight.y;
			struMptR.ptIdx = struMptL.ptIdx;
			///struMptR.ptImgIdxL = features1.export_idx;
			///struMptR.ptImgIdxR = features2.export_idx;
			struMptR.ptImgIdxL = features2.export_idx;
			struMptR.ptImgIdxR = features1.export_idx;

			matches_info.vecMatchPtsR.push_back(struMptR);

			vecOriPtR.push_back(ptOriginRight);

			dst_points.at<Point2f>(0, inlier_idx) = p;

			//计算重叠区大小
			dOverlapWSum += features1.ori_img_size.width - struMptL.x + struMptR.x;//由所有点计算得到的重叠区宽度之和
			dOverlapHSum += MIN(struMptL.y, struMptR.y) + MIN(features1.ori_img_size.height-struMptL.y, features2.ori_img_size.height-struMptR.y);//由所有点计算得到的重叠区高度之和


			inlier_idx++;
			nPtNum++;
		}
		image_name++;
		cout<<endl<<"refine src_points:"<<endl;
		LOG(src_points);
		cout<<endl;
		cout<<endl<<"refine dst_points:"<<endl;
		LOG(dst_points);
		cout<<endl;
		LOG(matches_info.matches.size());


		// Rerun motion estimation on inliers only
		matches_info.H = findHomography(src_points, dst_points, CV_RANSAC);
		cout<<endl<<"matches_info.H";
		LOG(matches_info.H);

		//将匹配点数目减少至20个以内，以加快后续光束法平差速度------------
		if(inlier_idx >= 20)//只对大于20个点的匹配对筛点
		{
			double dOverlapW = dOverlapWSum / matches_info.matches.size();//重叠区宽度
			double dOverlapH = dOverlapHSum / matches_info.matches.size();//重叠区高度
			int nGridStep = 3;//把重叠区分为3*3格网
			double dGridW = dOverlapW / nGridStep;//格网大小（宽）
			double dGridH = dOverlapH / nGridStep;//格网大小（高）
			vector< vector<MatchPt> > vecGridPt(nGridStep*nGridStep);//用于存放不同格网内的点
			double dOvlpULX = features1.ori_img_size.width - dOverlapW;//重叠区左上角的X坐标
			double dOvlpULY = features1.ori_img_size.height - dOverlapH;//重叠区左上角的Y坐标
			vector<MatchPt> vecReducedLPts;//存放左片筛减后的点
			vector<MatchPt> vecReducedRPts;//存放右片筛减后的点

			test<<"inlierNum = "<<inlier_idx<<endl;
			test<<"matches_info.vecMatchPtsL = " << matches_info.vecMatchPtsL.size()<<endl;
			for(size_t u = 0; u < matches_info.vecMatchPtsL.size(); u++)//只对左片筛点，右片根据左片点的id查找到同名点
			{

				int nGridX = (int)(abs((matches_info.vecMatchPtsL[u].x - dOvlpULX)) / dGridW);//计算该点位于第几个格网
				int nGridY = (int)(abs((matches_info.vecMatchPtsL[u].y - dOvlpULY)) / dGridH);

				test<<u<<"  "<<"nGridX+nGridY*nGridStep  "<<nGridX+nGridY*nGridStep<<"  nGridX = "<<nGridX<<"  nGridY = "<<nGridY<<"  "<<nGridStep \
					<<"  x = "<<matches_info.vecMatchPtsL[u].x<<"   y = " <<matches_info.vecMatchPtsL[u].y \
					<<"  dOvlpULX = "<<dOvlpULX<<"  dOvlpULY = "<<dOvlpULY<<endl;

				if(nGridX>=0&&nGridX<=2&&nGridY>=0&&nGridY<=2)
				{
					vecGridPt[nGridX+nGridY*nGridStep].push_back(matches_info.vecMatchPtsL[u]);//将位于同一个格网的点压入同一个vector中，总共有9个格网
				}

			}

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

			//根据左片筛减后的点，在右片根据16位id找到同名点，压入同一个vecReducedRPts，替换筛点前的vecMatchPtsR
			for(size_t i = 0; i < vecReducedLPts.size(); i++)
			{
				for(size_t j = 0; j < matches_info.vecMatchPtsR.size(); j++)
				{
					if(vecReducedLPts[i].ptIdx == matches_info.vecMatchPtsR[j].ptIdx)//根据id找到右片同名点
					{
						vecReducedRPts.push_back(matches_info.vecMatchPtsR[j]);
					}
				}
			}

			//替换原有的点
			matches_info.vecMatchPtsL = vecReducedLPts;
			matches_info.vecMatchPtsR = vecReducedRPts;

			ofstream testOut;
			testOut.open("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site4/testreduce20.txt", ios::app);
			for(int i = 0; i < vecGridPt.size(); i++)
			{
				testOut<<endl<<"GridNo"<<i<<endl;
				testOut<<"("<<dOvlpULX +i*dGridW<<", "<< dOvlpULY +i*dGridH<<")"<<"---"<<"("<<dOvlpULX +(i+1)*dGridW<<", "<< dOvlpULY +(i+1)*dGridH<<")"<<endl;
				for(int b = 0; b < vecGridPt[i].size(); b++)
				{
					testOut<<"x = "<<vecGridPt[i][b].x<<"     "<<"y = "<<vecGridPt[i][b].y<<endl;
				}
				testOut<<endl;
				testOut<<endl;
			}

			int enend = 0;
		}





	}


	else//输入外部匹配点进行匹配
	{
		// Check if it makes sense to find homography
		//if (matches_info.matches.size() < static_cast<size_t>(num_matches_thresh1_))
		//    return;

		int nMatchPtNum = 0;


		// Construct point-point correspondences for homography estimation
		vector<MatchPt> vecAll1;
		vector<MatchPt> vecAll2;
		MatchPt mptAll1;
		MatchPt mptAll2;

		for(size_t i = 0; i < features1.AddPanoMatches.vecMatchPtsSrc.size(); i++)
		{
			mptAll1.x = features1.AddPanoMatches.vecMatchPtsSrc[i].x;
			mptAll1.y = features1.AddPanoMatches.vecMatchPtsSrc[i].y;
			mptAll1.ptIdx = features1.AddPanoMatches.vecMatchPtsSrc[i].ptIdx;
			vecAll1.push_back(mptAll1);
		}

		for(size_t i = 0; i < features2.AddPanoMatches.vecMatchPtsSrc.size(); i++)
		{
			mptAll2.x = features2.AddPanoMatches.vecMatchPtsSrc[i].x;
			mptAll2.y = features2.AddPanoMatches.vecMatchPtsSrc[i].y;
			mptAll2.ptIdx = features2.AddPanoMatches.vecMatchPtsSrc[i].ptIdx;
			vecAll2.push_back(mptAll2);
		}
		bool bMatch = false;
		for(size_t i = 0; i < vecAll1.size(); i++)
		{
			for(size_t j = 0; j < vecAll2.size(); j++)
			{
				if(vecAll1[i].ptIdx == vecAll2[j].ptIdx)
				{
					nMatchPtNum++;
					bMatch = true;
				}
			}
		}
		matches_info.nMatchesNum = nMatchPtNum;

		Mat src_points(1, matches_info.nMatchesNum, CV_32FC2);
		Mat dst_points(1, matches_info.nMatchesNum, CV_32FC2);

		int nCount = 0;

		//查找内点
		matches_info.num_inliers = 0;
		for(size_t i = 0; i < vecAll1.size(); i++)
		{
			for(size_t j = 0; j < vecAll2.size(); j++)
			{
				if(vecAll1[i].ptIdx == vecAll2[j].ptIdx)
				{
					Point2f pM1;
					Point2f pM2;
					pM1.x = vecAll1[i].x;
					pM1.y = vecAll1[i].y;
					pM1.x -= features1.img_size.width * 0.5f;
					pM1.y -= features1.img_size.height * 0.5f;
					src_points.at<Point2f>(0, nCount) = pM1;

					pM2.x = vecAll2[j].x;
					pM2.y = vecAll2[j].y;
					pM2.x -= features2.img_size.width * 0.5f;
					pM2.y -= features2.img_size.height * 0.5f;
					dst_points.at<Point2f>(0, nCount) = pM2;

					matches_info.num_inliers++;
					//matches_info.inliers_mask.push_back(1);
					nCount++;
				}
			}
		}

		if(bMatch)
		{
			matches_info.confidence = 10;
			matches_info.H = findHomography(src_points, dst_points, CV_RANSAC);
		}
		else
		{
			matches_info.confidence = 0;
		}

		///matches_info.confidence = matches_info.num_inliers / (8 + 0.3*matches_info.matches.size());

		/*****************************************************/

		//    ofstream test;
		//    test.open("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site5/testout.txt", ios::app);
		//    test<<"nLoopID: "<<features1.nLoopID<<"   nLoopImgID: "<<features1.nLoopImgID<<"   img_idx: "<<features1.img_idx<<endl<< \
		//    "nLoopID: "<<features2.nLoopID<<"   nLoopImgID: "<<features2.nLoopImgID<<"   img_idx: "<<features2.img_idx<<endl<<endl;

		/*****************************************************/
		/*****************************************************/

		ofstream test;
		test.open("/home/pmrs-05/rvml/DL/CODE/trunk/program/TestProj/PanoMosaic/site5/test_readpts.txt", ios::app);
		test<<features1.img_idx<<"   "<<"Read src_points: "<<src_points<<endl;
		test<<features2.img_idx<<"   "<<"Read dst_points: "<<dst_points<<endl<<endl;
		/*****************************************************/




		cout<<endl<<"Read src_points:"<<endl;
		LOG(src_points);
		cout<<endl;
		cout<<endl<<"Read dst_points:"<<endl;
		LOG(dst_points);
		cout<<endl;
		LOG(matches_info.matches.size());

	}

}

bool SortYMinToMax(const MatchPt & mpt1, const MatchPt & mpt2)
{
	return mpt1.y < mpt2.y;
}


