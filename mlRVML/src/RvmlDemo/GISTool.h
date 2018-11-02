/************************************************************
  Copyright (C), 2011-2012, PMRS Lab, IRSA, CAS
  文件名称: GISTool.h
  创建日期: 2011.11.6
  作    者: 李巍
  描    述: 地形分析计算
  版本编号：1.0
  修改历史:   <作者>   <时间>   <版本编号>   <描述>

***********************************************************/
#ifndef GISTOOL_H
#define GISTOOL_H
#include "gdal_header.h"
#include "gdal/gdal.h"
#include "gdal/gdal_alg.h"
#include "gdal/cpl_conv.h"
#include "gdal/cpl_string.h"
#include "gdal/ogr_api.h"
#include "gdal/ogr_srs_api.h"

/*************************************************
  函数名称:    mlComeputeContour
  作    者:   李巍
  功能描述：   等高线生成
  输    入：
  参数1：等高距
  参数2：DEM文件路径
  参数3：输出的shape文件路径
  参数4：是否自定义Nodata
  参数5：如果bCNodata设置为true，则dNodata 的值在计算时被当做无效值对待
  参数6：生成shape文件高程的属性名称，默认为elev
  输    出： 返回值：1，正常执行；-1，gdal版本过低；-2，等高距设置有误；-3，输入文件名有误；-4 ，输出文件名有误
  版本编号：   1.0
  修改历史：   <作者>   <时间>   <版本号>   <描述>
  *************************************************/
int mlComeputeContour(double dHinterval,   char* strSrcfilename,   char* strDstfilename ,bool bCNodata = FALSE, double dNodata = 0.0,  char* cAttrib = "elev");
#endif // GISTOOL_H
