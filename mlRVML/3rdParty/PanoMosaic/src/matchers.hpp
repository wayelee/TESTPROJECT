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
#ifndef __OPENCV_MATCHERS_HPP__
#define __OPENCV_MATCHERS_HPP__

#include "precomp.hpp"
#include "Panorama.h"

//struct MatchPt
//{
//    cv::Point2f pt;
//    unsigned int ptImgIdxL;
//    unsigned int ptImgIdxR;
//    unsigned long long int ptIdx;
//};

////CPU匹配函数参数->Panorama.h
struct ImageFeatures
{
	int img_idx;                                //ImageFeature 内部图像index，从0依次向后排序。

	cv::Size img_size;                          //图像大小，可能经过采样
	std::vector<cv::KeyPoint> keypoints;        //图像控制点数组
	cv::Mat descriptors;                        //描述符

	cv::Size ori_img_size;                      //图像大小，不经过采样
	int nLoopID;                                //图像圈号
	int nLoopImgID;                             //图像圈内的序号
	int nLoopImgNum;                            //一圈有几幅图像
	int nLoopNum;                               //一共有几圈
	int nByte;                                  //波段比特数，一般为8bits
	bool bAddPt;                                //是否需要加点
	bool bExpMatches;
	string sImgPathSrc;                         //Src图像路径
	string sImgPathDst;                         //Dst图像路径
	unsigned int export_idx;                    //接受的外部传入index，4位
	PanoMatchInfo AddPanoMatches;

	///char* cProjectFile;
};

class MatchPair
{
public:
	int m_from;
	int m_to;
};

class OverlapSize
{
public:
	int m_Width;
	int m_Height;
};

class OverlapGrid
{
public:
	int m_from;
	int m_to;

	int m_Width;
	int m_Height;
};

class FeaturesFinder
{
public:
	virtual ~FeaturesFinder() {}
	void operator ()(const cv::Mat &image, ImageFeatures &features);

protected:
	virtual void find(const cv::Mat &image, ImageFeatures &features) = 0;
};


class SurfFeaturesFinder : public FeaturesFinder
{
public:
	SurfFeaturesFinder(bool try_use_gpu = true, double hess_thresh = 300.0,
		int num_octaves = 3, int num_layers = 4,
		int num_octaves_descr = 4, int num_layers_descr = 2);

protected:
	void find(const cv::Mat &image, ImageFeatures &features);

	cv::Ptr<FeaturesFinder> impl_;
};

//////////////////CPU匹配函数参数
struct MatchesInfo
{
	MatchesInfo();
	MatchesInfo(const MatchesInfo &other);
	const MatchesInfo& operator =(const MatchesInfo &other);

	int src_img_idx, dst_img_idx;       // Images indices (optional)
	std::vector<cv::DMatch> matches;
	std::vector<uchar> inliers_mask;    // Geometrically consistent matches mask 判断该点是否为内点，是为1，否为0
	int num_inliers;                    // Number of geometrically consistent matches 内点的数量
	cv::Mat H;                          // Estimated homography 单应矩阵
	double confidence;                  // Confidence two images are from the same panorama 置信度

	unsigned int exp_idx_l, exp_idx_r;
	int nMatchesNum;
	string sImgPathSrc;
	string sImgPathDst;
	std::vector<cv::KeyPoint> vecSrcPt; // src_img_idx图像上的匹配点
	std::vector<MatchPt> vecMatchPtsL;  // src图像上的匹配点数组
	std::vector<MatchPt> vecMatchPtsR;  // dst图像上的匹配点数组
};


class FeaturesMatcher
{
public:
	virtual ~FeaturesMatcher() {}
	void operator ()(const ImageFeatures &features1, const ImageFeatures &features2,
		MatchesInfo& matches_info)
	{
		match(features1, features2, matches_info);
	}
	//matcher(features[from], features[to], pairwise_matches[pair_idx]);//matcher为结构体中成员，定义在line276
	void operator ()(const std::vector<ImageFeatures> &features, std::vector<MatchesInfo> &pairwise_matches);
	void operator ()(const std::vector<ImageFeatures> &features, std::vector<MatchesInfo> &pairwise_matches, bool bOnlyReadMatchPts);//与匹配时括号重载的方式不一样

	bool isThreadSafe() const
	{
		return is_thread_safe_;
	}

protected:
	FeaturesMatcher(bool is_thread_safe = false) : is_thread_safe_(is_thread_safe) {}

	virtual void match(const ImageFeatures &features1, const ImageFeatures &features2,
		MatchesInfo& matches_info) = 0;

	void calNearPairs(int nLoopImgNum, int nLoopNum, vector< pair<int, int> > &vecNearPairs);//nLoopImgNum:一圈有几张图像; nLoopNum:一共有几圈

	bool is_thread_safe_;
};


class BestOf2NearestMatcher : public FeaturesMatcher
{
public:
	BestOf2NearestMatcher(bool try_use_gpu = true, float match_conf = 0.45f, int num_matches_thresh1 = 6,
		int num_matches_thresh2 = 6);


protected:
	void match(const ImageFeatures &features1, const ImageFeatures &features2, MatchesInfo &matches_info);
	void match_read(const ImageFeatures &features1, const ImageFeatures &features2, MatchesInfo &matches_info);//读取外部匹配点
	//bool SortYMinToMax(const MatchPt & mpt1, const MatchPt & mpt2);//按点的Y坐标从小到大排序

	int num_matches_thresh1_;
	int num_matches_thresh2_;
	cv::Ptr<FeaturesMatcher> impl_;
};

bool SortYMinToMax(const MatchPt & mpt1, const MatchPt & mpt2);


int RansacHomo( vector<double> vxl, vector<double> vyl, vector<double> vxr, vector<double> vyr,\
			   MatchesInfo &vecRanPts, double dThresh, double dConfidence, ImageFeatures ifLImg, ImageFeatures ifRImg, vector< pair<int, int> > vecNoMatchPtPair);


#endif // __OPENCV_MATCHERS_HPP__
