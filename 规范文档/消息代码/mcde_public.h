
/**
\file       mcde_public.h
\brief      MCDE进程的全局变量定义模块

\author     xyh
\date       2012-09-26
\version    1.0
\note       UNIX/C
\par        修改记录
- zzl    2012-09-06  模块建立
*/

#ifndef  _MC_PUBLIC_H
#define  _MC_PUBLIC_H


// #define  DELAYTIME_STATION           10            /** 多站传输时延                         */

// #define  CJTIMEDiF_GRAY              15            /** 灰度图（导航、避障）左右目采集时间差 ——— 20121023原方案 5s */
// #define  CJTIMEDiF_FULL              10            /** 全色图左右目采集时间差 ——— 20121023原方案 8s               */
// #define  CJTIMEDiF_RGB               30            /** 彩色图左右目采集时间差 ——— 20121023原方案 17s ---20s       */
//
// #define  FRAMESUM_GRAY               1192          /** 灰度图（导航、避障）单幅图像总帧数   */
// #define  FRAMESUM_FULL               1450          /** 全景全色图 单幅图像总帧数            */
// #define  FRAMESUM_RGB                5791          /** 全景彩色图 单幅图像总帧数            */

#define  ERROR_RTFRAMES              0.1           /** 相机角度遥测提取两帧的允许误差       */

#define  ICNI_CAMERALABEL_LEN        4             /** 相机标识长度                         */
#define  ICNI_REFFRAMELABEL_LEN      6             /** 坐标系标识长度                       */




///*------------结构定义------------------*/

/**巡视器图像选优匹配消息结构 */
typedef  struct  _ST_IMSI_TMIMGSTAT
{
    ST_MsgHead	stMsgHead;		                              /** 消息头                                     */
    Uint32		  uiStandID;		                              /** 站点序号                                   */
    Uint32    	uiFileType;		                              /** 图像文件类型                               */
    Uint32		  uiIMGIndex;		                              /** 图像序号                                   */
    Float64		  dA;		                                      /** 相机指向展开角度                           */
    Float64		  dH;		                                      /** 相机指向偏航角                             */
    Float64		  dP;		                                      /** 相机指向俯仰角                             */
    Uint8		    ucMatchType;		                            /** 匹配标志(0：已成功匹配;1：仅左目;2：仅右目)*/
    Sint8 		  szIMGFileName_Z[ICNI_MAX_FILENAME_LEN];	    /** 左目图像名称                               */
    Uint32		  uiLostFrames_Z;		                          /** 左目图像丢帧数                             */
    ST_Time		  stTakePhoTime_Z;		                        /** 左目成像时间                               */
    Uint32		  uiTakePhoTimeLJ_Z;		                      /** 左目成像时间（累计秒）                     */
    Sint8		    szIMGFileName_Y[ICNI_MAX_FILENAME_LEN];	    /** 右目图像名称                               */
    Uint32		  uiLostFrames_Y;		                          /** 右目成像丢帧数                             */
    ST_Time		  stTakePhoTime_Y;		                        /** 右目成像时间                               */
    Uint32		  uiTakePhoTimeLJ_Y;		                      /** 右目成像时间（累计秒）                     */

}ST_IMSI_TMIMGSTAT;



/**遥操作子系统文件头公共信息结构*/
typedef  struct  _ST_YCZFILEHEAD
{
    Sint8		             szTaskName[ICNI_MISSNAME_LEN];               /**	任务名称                                   */
	  Sint8		             szObjName[ICNI_OBJNAME_LEN];                 /**	目标代号                                   */
	  Uint32		           uiTaskCode;                                  /**	任务码                                     */
	  Uint32		           uiObjCode;          		                      /**	目标码                                     */
	  Uint32		           uiFileType;                                  /**	产品类型码                                 */
	  Uint32		           uiStandID;					                          /**	站点序号                                $  */
	  ST_Time		           stBJTime;					                          /** 生产北京时间                               */
	  ST_Time		           stTaskTime;					                        /**	生产任务时间                               */
	  Sint8		             szPrtFileName[ICNI_MAX_FILENAME_LEN];        /** 产品标识（文件名）                         */
	  Uint32		           uiPrtSeqID;					                        /**	产品流水号目前按信源、相机类型分别排队 ??? */
	  Sint8 		           szRemark[12]; 				                        /**	备注[可选]                                 */

}ST_YCZFILEHEAD;


/**图像类TIFF格式附属文件公共信息结构*/
typedef  struct  _ST_IMGFILEHEAD
{
    Sint8                szStation[ICNI_MAX_DESTNAME_LEN];            /**  产品来源（测站标识）                  */
    Sint8			           szCameraMark[ICNI_CAMERALABEL_LEN];          /**  相机标识                              */
    Uint8		  	         ucLeftRightID;		                            /**  左右目标识：0：单目；1：左目；2：右目 */
    Uint8		             ucOpt;                             		      /**  选优标识                          @   */
    Sint8		             szSSMatchFile[ICNI_MAX_FILENAME_LEN];		    /**  同站配对文件（可选）              @   */
    Sint8		             szOptMatchFile[ICNI_MAX_FILENAME_LEN];		    /**  选优配对文件（可选）              @   */
    Uint8		  	         ucModel;		                                  /**  成像模式                              */
    Sint8                szModel[15];
    ST_Time		           stTakePhoTime;		                            /**  成像时间                              */
    Uint32		           uiTakePhoTimeLJ;		                          /**  成像时间（累计秒）星上时              */
    Float64		           dCamBGZY;		                                /**  相机曝光参数（图像增益）          $   */
    Float64	  	         dCamImgTime;			                            /**  相机曝光参数（曝光时间）          $   */
    Uint8		  	         ucPixBit;	                                  /**  像素位数                              */
    Uint32		           uiWidth;	                                    /**  图像分辨率（宽）                      */
    Uint32		           uiHeight;	       	                          /**  图像分辨率（高）                      */
    Sint8		  	         szRefFrame[ICNI_REFFRAMELABEL_LEN];	        /**  坐标系标识                        $   */
    Float64		           dLog;		                                    /**  成像时航天器位置经度              $   */
    Float64		           dLat;		                                    /**  成像时航天器位置维度              $   */
    Float64		           dHeight;	                                    /**  成像时航天器位置高度              $   */
    Float64		           dPosX;		                                    /**  成像时航天器位置X                 $   */
    Float64		           dPosY;		                                    /**  成像时航天器位置Y                 $   */
    Float64		           dPosZ;		                                    /**  成像时航天器位置Z                 $   */
    Float64		           dRoll;	                                      /**  -180~180	成像时航天器姿态（滚动） $   */
    Float64		           dPitch;	                                    /**  -180~180	成像时航天器姿态（俯仰） $   */
    Float64		           dAzimuth;	                                  /**  -180~180	成像时航天器姿态（偏航） $   */
    Float64		           dSunAL;		                                  /**  当地太阳高度角                    $   */
    Float64		           dSunEL;		                                  /**  当地太阳方位角                    $   */
    Float64		           dSunAB;		                                  /**  车体太阳高度角                    $   */
    Float64		           dSunEB;		                                  /**  车体太阳方位角                    $   */
    Float64		           dA;		                                      /**  相机指向展开角度（可选）          $   */
    Sint8                szAFlag[10];                                 /**  相机指向展开角度数据来源标识      $   */
    Float64		           dH;		                                      /**  相机指向偏航角                    $   */
    Sint8                szHFlag[10];                                 /**  相机指向偏航角数据来源标识        $   */
    Float64		           dP;		                                      /**  相机指向俯仰角                    $   */
    Sint8                szPFlag[10];                                 /**  相机指向俯仰角数据来源标识        $   */
    Uint32		           uiLostFrames;                                /**  丢帧数                                */

}ST_IMGFILEHEAD;


/** 定义指针结构 */
typedef struct _ST_LINK
{
	  ST_YCZFILEHEAD*			 pstYCZFileHead;                              /** 遥操作子系统文件头公共信息结构_指针     */
    ST_IMGFILEHEAD*			 pstImgFileHead;                              /** 图像类TIFF格式附属文件公共信息结构_指针 */
    Uint32		           uiSysTime;		                                /** 系统时间(累积秒)                        */
    Uint8                ucQueryDB;                                   /** 已填写数据库查询内容 0:未填, 1:已填     */
	  struct _ST_LINK*	   pre;
	  struct _ST_LINK*	   next;

}ST_LINK;


/** 定义队列结构 */
typedef struct _ST_QUE
{
	  pthread_mutex_t		   Mutex;
	  ST_LINK*			       head;
	  ST_LINK*			       tail;
	  ST_LINK*			       find;

}ST_QUE;


/** 巡视器相对定位结果文件结构 */
typedef struct  _ST_IFLIRAPAST
{
	  ST_YCZFILEHEAD		   stFileHead;						                      /** 文件头                                 */
    Sint8		             szMacroNum[ICNI_TKPLANNUM_LEN];	            /** 整体规划序号                           */
    Sint8		             szCyNum[ICNI_CYPLANNUM_LEN];  	              /** 周期规划序号                           */
    Sint8		             szUnNum[ICNI_UNPLANNUM_LEN];  	              /** 单元规划序号                           */
    Sint8		             szPosRefFrame[ICNI_REFFRAMELABEL_LEN];       /** 位置输出坐标系标识                     */
    Float64		           dCurWorkCSX;		                              /** 工作坐标系原点在月面全局坐标系下的X    */
    Float64		           dCurWorkCSY;		                              /** 工作坐标系原点在月面全局坐标系下的Y    */
    Float64		           dCurWorkCSZ;		                              /** 工作坐标系原点在月面全局坐标系下的Z    */
    Uint8		             ucLocateModel;	                              /** 定位模式                               */
    Uint8		             ucLocateOpt;		                              /** 选优结果标识                           */
    Sint8		             szDataTime[25];     	                        /** 定位历元时间（任务时）格式：xxxx-xx-xxTxx:xx:xx.xxxx */
    Float64		           dX;		                                      /** 工作坐标系点X位置                      */
    Float64		           dY;		                                      /** 工作坐标系点Y位置                      */
    Float64		           dZ;		                                      /** 工作坐标系点Z位置                      */
    Float64		           dLog;	                                      /** -180-180	经度                         */
    Float64		           dLat;	                                      /** -90-90	纬度                           */
    Float64		           dHeight;		                                  /** 高度                                   */
    Float64		           dRoll;		                                    /** X轴旋转角度(滚动)                      */
    Float64		           dPitch;		                                  /** Y轴旋转角度（俯仰）                    */
    Float64		           dYaw;		                                    /** Z轴旋转角度（偏航）                    */
    Float64		           dRMSX;		                                    /** X位置方差                              */
    Float64		           dRMSY;		                                    /** Y位置方差                              */
    Float64		           dRMSZ;		                                    /** Z位置方差                              */
    Float64		           dRMS;		                                    /** 总的位置方差                           */
    Float64		           dRMSRoll;	                                  /** 滚动角方差                             */
    Float64		           dRMSPitch;	                                  /** 俯仰角方差                             */
    Float64		           dRMSYaw;		                                  /** 偏航角方差                             */
    Float64		           dRMSAtt;		                                  /** 姿态方差                               */
    Float64		           dSunAL;	                                    /** 当地太阳方位角                         */
    Float64		           dSunEL;		                                  /** 当地太阳高度角                         */
    Float64		           dSunAB;		                                  /** 车体太阳方位角                         */
    Float64		           dSunEB;		                                  /** 车体太阳高度角                         */

}ST_IFLIRAPAST;


/** 查询数据库结果信息结构 */
typedef  struct  _ST_QUERYDBINFOR
{
    Uint32		           uiTakePhoTimeLJ;		                         /**  成像时间（累计秒）星上时              */
    Uint32		           uiFileType;                                 /**	产品类型码                            */
    Uint32		           uiStandID;					                         /**	站点序号                              */
    Uint8		  	         ucLeftRightID;		                           /**  左右目标识：0：单目；1：左目；2：右目 */
    Float64		           dCamBGZY;		                               /**  相机曝光参数（图像增益）              */
    Float64	  	         dCamImgTime;			                           /**  相机曝光参数（曝光时间）              */
    Sint8		  	         szRefFrame[ICNI_REFFRAMELABEL_LEN];	       /**  坐标系标识                            */
    Float64		           dLog;		                                   /**  成像时航天器位置经度                  */
    Float64		           dLat;		                                   /**  成像时航天器位置维度                  */
    Float64		           dHeight;	                                   /**  成像时航天器位置高度                  */
    Float64		           dPosX;		                                   /**  成像时航天器位置                      */
    Float64		           dPosY;		                                   /**  成像时航天器位置                      */
    Float64		           dPosZ;		                                   /**  成像时航天器位置                      */
    Float64		           dRoll;	                                     /**  -180~180	成像时航天器姿态（滚动）    */
    Float64		           dPitch;	                                   /**  -180~180	成像时航天器姿态（俯仰）    */
    Float64		           dAzimuth;	                                 /**  -180~180	成像时航天器姿态（偏航）    */
    Float64		           dSunAL;		                                 /**  当地太阳高度角                        */
    Float64		           dSunEL;		                                 /**  当地太阳方位角                        */
    Float64		           dSunAB;		                                 /**  车体太阳高度角                        */
    Float64		           dSunEB;		                                 /**  车体太阳方位角                        */
    Float64		           dA;		                                     /**  相机指向展开角度（可选）              */
    Sint8                szAFlag[10];                                /**  相机指向展开角度数据来源标识          */
    Float64		           dH;		                                     /**  相机指向偏航角                        */
    Sint8                szHFlag[10];                                /**  相机指向偏航角数据来源标识            */
    Float64		           dP;		                                     /**  相机指向俯仰角                        */
    Sint8                szPFlag[10];                                /**  相机指向俯仰角数据来源标识            */

}ST_QUERYDBINFOR;


/** 图像基础信息结构 */
typedef  struct  _ST_BASEINFOR
{
	  Uint32		           uiTakePhoTimeLJ;		                         /**  成像时间（累计秒）星上时              */
	  ST_Time		           stTakePhoTime;		                           /**  成像时间                              */
    Uint32		           uiLostFrames;                               /**  丢帧数                                */

}ST_BASEINFOR;




/**--------------------------全局变量-----------------------------------**/

extern    Uint32              g_uiTaskCode;                           /**  任务码                  */
extern    Uint32              g_uiObjCode;                            /**  目标码                  */
extern    Sint32              g_siRunLp;                              /**  运行标志                */
extern    Sint32              g_siQueID;                              /**  电文队列ID              */


extern    ST_QUE              g_queMatch[3];                          /**  匹配信息队列[0]导航;[1]全景;[2]避障*/
extern    Uint8               g_ucMatchFlag[3];                       /**  匹配标志([0]导航;[1]全景;[2]避障)----- 0:等待匹配；1：正在匹配  */

extern    Sint8               g_szHostName[256];                      /**  主机名                  */
extern    Sint8               g_szFileRsltName[128];                  /**	 写图像文件的路径        */
extern    Sint8               g_strLoadPath[128];                     /**  图像附属文件存储物理地址*/


extern    Uint32              g_uiIMGIndex[3];                        /**  图像统计计数 [0]导航;[1]全景;[2]避障  */
extern    Uint32              g_uiFrameRate;                          /** 数传帧频 */


extern    Uint32              g_uiMatchTimeGray;                      /**  8位灰度图像（导航、避障）左右目采集时间差   */
extern    Uint32              g_uiMatchTimeFull;                      /**  全景全色图像左右目采集时间差   */
extern    Uint32              g_uiMatchTimeColor;                     /**  全景彩色图像左右目采集时间差   */
extern    Uint32              g_uiWaitTimeGray;                       /**  8位灰度图（导航、避障）匹配等待时长   */
extern    Uint32              g_uiWaitTimeFull;                       /**  全景全色图像匹配等待时长   */
extern    Uint32              g_uiWaitTimeColor;                      /**  全景全色图像匹配等待时长   */

#endif
