/**
* @copyright Copyright(C), 2011-2012, PMRS Lab, IRSA, CAS
* @file mlTypeConvert.h
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 类型转换头文件
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
#ifndef _ML_TYPECONVERT_H_
#define _ML_TYPECONVERT_H_

#include "mlBase.h"
#include "mlRasterBlock.h"
#include "opencv.h"

/**
* @fn IplImage2CmlRBlock
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 将IplImage型变量转化成CmlSBlock型变量
* @param pIplImg  IplImage型变量
* @param pSBlock  CmlSBlock型变量
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool IplImage2CmlRBlock( const IplImage* pIplImg, CmlRasterBlock* pSBlock );
/**
* @fn CmlSBlock2IplImg
* @date 2011.11.02
* @author 万文辉 whwan@irsa.ac.cn
* @brief 将CmlSBlock型变量转化成IplImage型变量
* @param pSBlock  CmlSBlock型变量
* @param pIplImg  IplImage型变量
* @version 1.0
* @par 修改历史:
* <作者>  <时间>  <版本编号>  <描述>\n
*/
bool CmlRBlock2IplImg( const CmlRasterBlock* pSBlock, IplImage* &pIplImg  );

#endif
