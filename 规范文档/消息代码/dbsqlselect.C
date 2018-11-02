/**
\brief  查询数据库获取图像描述信息
\param[in]
\return -1 错误；0 正确
*/
Uint32  MCDE_QueryDB( ST_YCZFILEHEAD* pstYCZFileHead, ST_IMGFILEHEAD* pstImgFileHead, ST_QUERYDBINFOR* pstQueDB)
{
    Sint32      siRet;
    Sint8       szLog[ICNI_MAX_LOG_LEN];                             /*  日志信息*/
    Sint8       szRECVTIME[256];                                     /*  查询结果在数据库的入库时间*/
    Sint32      siRsltNum;
    Sint32      siRetRslt;
    DbHandler   Handler= NULL;
    Sint32      siFindFlag =0;
    Uint32      uiFileType = 0;                                      /* 产品类型码 */
    Sint8       szSelectStr[1024];
    Sint8       szWhereStr[512];
    Sint32      siSelTime = 0;
    Float64     dRslt[5];
    Float64     dRsltTemp[3];
    Float64     dSunAngle[4];
    Sint8       szAFlag[10];
    Sint8       szHFlag[10];
    Sint8       szPFlag[10];
    Uint32      uiCameraType = 0;                                    /* DHZ:1; DHY:2; QJZ:3; QJY:4; BZZ:5; BZY:6*/
    Sint32      siHour,siMinute,siSecond,siMSecond;
    Sint32      siHour1,siMinute1,siSecond1,siMSecond1;
    Sint8       szName[256];
    Uint32      uiFileNum;
    Sint8       szFileName[64];
    Sint8       szFileNameS[64];
    Sint32      i,i1;
    ZF_FileHead msghead;
    Sint8       szPathFileName[256];                                 /* 带路径的文件名 */
    Sint8       szStrTemp[512];
    Uint32      uiStationID = 0;
    Uint32      uiQueRTFraNum = 1;                                   /* 需要查询遥测参数的帧数 */
    Uint32      uiNum = 0;
    Uint8       ucBZImg = 0;                                         /* 是否为避障相机图像—— 0:否;1:是*/
    ST_IFLIRAPAST stFileRapast;


    Uint32		uiCJtime;
    ST_Time     stCJtime;
    ST_Time     stCJtime1;


    memset(szAFlag,0,sizeof(szAFlag));
    memset(szHFlag,0,sizeof(szHFlag));
    memset(szPFlag,0,sizeof(szPFlag));

    memset(szLog,0,sizeof(szLog));
    memset(szRECVTIME, 0, sizeof(szRECVTIME));

    memset(szFileName,0,64);
    memset(szFileNameS,0,64);

	if(pstYCZFileHead ==NULL )
		return -1;
	if(pstImgFileHead ==NULL )
		return -1;


    /** 判断图像相机类型，初始化各类参数及查询条件 */

    /** 判断相机类型 */
    switch(pstYCZFileHead->uiFileType)
    {
    	  case IFLMCYSDHX:   /** 导航相机图像 */
    	  {
            /** 如果是左导航图像，相机类型=1，获取遥测查询参数名称，设置查询时长12秒 */
            siRet = strcmp(pstImgFileHead->szCameraMark, "DHZ");
            if(siRet == 0)
            {
                uiCameraType = 1;
                strcpy(szSelectStr,"XTMK058,XTMK057,XTMJ004,XTMJ006,XTMJ008 ");
                siSelTime = 12;
            }
            /** 如果是右导航图像，相机类型=2，获取遥测查询参数名称，设置查询时长20秒 */
            else
            {
                siRet = strcmp(pstImgFileHead->szCameraMark, "DHY");
                if(siRet == 0)
                {
                    uiCameraType = 2;
                    strcpy(szSelectStr,"XTMK064,XTMK063,XTMJ004,XTMJ006,XTMJ008 ");
                    siSelTime = 20;
                    uiQueRTFraNum = 2;
                }
            }

            /** 写入导航相机图像文件类型标识 */
            uiFileType = IFLI_YSDHX;
    	  }
    	  break;

    	  case IFLMCYSQJX:   /** 全景相机图像 */
   	  	{
  	  	 	  /** 如果是左全景图像，相机类型=3，设置查询时长12秒 */
  	  	 	  siRet = strcmp(pstImgFileHead->szCameraMark, "QJZ");
            if(siRet == 0)
            {
            	  uiCameraType = 3;
                siSelTime = 12;
            }
            /** 如果是右全景图像，相机类型=4，设置查询时长20秒 */
            else
            {
            	  siRet = strcmp(pstImgFileHead->szCameraMark, "QJY");
                if(siRet == 0)
                {
            	      uiCameraType = 4;
                    siSelTime = 20;
                    uiQueRTFraNum = 2;
                }
            }

  	  	 	  /** 获取遥测查询参数名称 */
  	  	 	  strcpy(szSelectStr,"XTMJ004,XTMJ006,XTMJ008 ");

  	  	 	  /** 写入全景相机图像文件类型标识 */
  	  	 	  uiFileType = IFLI_YSQJX;
    	  }
    	  break;

    	  case IFLMCYSBZX:   /** 避障相机图像*/
    	  {
            /** 避障相机标识置1 */
            ucBZImg = 1;

            /** 如果是左避障图像，相机类型=5，获取遥测查询参数名称，设置查询时长12秒 */
            siRet = strcmp(pstImgFileHead->szCameraMark, "BZZ");
            if(siRet == 0)
            {
                uiCameraType = 5;
                strcpy(szSelectStr,"XTMK070,XTMK069 ");
                siSelTime = 12;
            }
            /** 如果是右避障图像，相机类型=6，获取遥测查询参数名称，设置查询时长20秒 */
            else
            {
                siRet = strcmp(pstImgFileHead->szCameraMark, "BZY");
                if(siRet == 0)
                {
                    uiCameraType = 6;
                    strcpy(szSelectStr,"XTMK076,XTMK075 ");
                    siSelTime = 20;
                }
            }

            /** 写入避障相机图像文件类型标识 */
            uiFileType = IFLI_YSBZX;
    	  }
    	  break;

    	  default:
    	  	 siRet = -1;
    	  	break;
    }

  	if(siRet <0)
  	{
  		LOG('E', "MCDE 生成图像文件头公共信息时相机类别错误");
        return -1;
  	}


    memset(szWhereStr,0,sizeof(szWhereStr));
    memset(&stFileRapast, 0, sizeof(ST_IFLIRAPAST));


    /** 初始化遥测查询参数：相机参数、相机安装角度 */
    dRslt[0] = 0;
    dRslt[1] = 0;
    for(i = 0; i < 3; i++)
    {
    	  dRslt[i+2] = 99999;
    	  dRsltTemp[i] = 99999;
    }

    /** 初始化太阳高度角、方位角 */
    for(i = 0; i < 4; i++)
    	  dSunAngle[i] = 99999;



  	/** 调用数据库服务，获得遥测相机相关信息以及成像时的位置姿态等信息 */


    /** 图像的成像时间大于0，认为有效，可以进行数据库查询 */
    if(pstImgFileHead->uiTakePhoTimeLJ > 0)
    {

    	  /** 遥测参数查询----------begin------ *//////////////////

    	  /** 构造遥测参数查询条件 */
    	  sprintf(szWhereStr, "TIME <= %u and  TIME >= %u  and  UCPRI=1  order by RECVTIME desc ",pstImgFileHead->uiTakePhoTimeLJ,(pstImgFileHead->uiTakePhoTimeLJ-siSelTime));
    	  printf("参数的查询条件：%s \n",szWhereStr);


    	  Handler = dbNew();
	      if(Handler == NULL)
    	  {
        	  return -1;
    	  }

        /** 查询数据库实时工程遥测数据 */
        siRsltNum = dbQueryData(Handler, IMSI_TMGCJG_SS,g_uiTaskCode,g_uiObjCode,szSelectStr, szWhereStr);
    	  printf("工程数据查询siRsltNum=%d; \n",siRsltNum);


    	  /** 工程遥测数据查询结果无效，查询数传遥测数据 */
    	  if( siRsltNum <= 0 )
    	  {
    		  sprintf(szLog, "IMSI_TMGCJG_SS工程遥测数据查询异常,返回错误码%d", siRsltNum );
    		  LOG('W', szLog);

    		  siRsltNum = dbQueryData(Handler, IMSI_TMSCQW_XS,g_uiTaskCode,g_uiObjCode,szSelectStr, szWhereStr);
    	    printf("器务数据查询件：%s;%s;  %d \n",szSelectStr,szWhereStr,siRsltNum);
    	  }

    	  if(siRsltNum > 0)
    	  {
    		    /** 遥测数据查询结果有效，获取参数查询结果 */

    	      /** 循环遍历查询出的数据帧内容 */
    	      while(!dbEof(Handler))
    		    {
    	    	    siFindFlag =1;


    	          /** 按相机类型，分别获取查询参数结果 */
    	          /** 参数值无效，将查询标识置为0，并填入无效标识 */

    	          switch(uiCameraType)
  				      {
  				          case 1:              /** 左导航相机图像DHZ*/
  				          {

  				  	          siRetRslt = dbGetDouble(Handler,  "XTMK058" , &dRslt[0]);
  				  	          printf("@1--%d;%lf ",siRetRslt,dRslt[0]);

  				  	          if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[0] = 0;
  				  	          }
  				  	          siRetRslt = dbGetDouble(Handler,  "XTMK057" , &dRslt[1]);
  				  	          printf("@1--%d;%lf ",siRetRslt,dRslt[1]);
  				  	          if(siRetRslt < 0 )
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[1] = 0;
  				  	          }
  				  	          siRetRslt = dbGetDouble(Handler,  "XTMJ004" , &dRslt[2]);
  				  	          printf("@1--%d;%lf ",siRetRslt,dRslt[2]);
  				  	          if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[2] = 99999;
  				  	          }
					              siRetRslt = dbGetDouble(Handler, "XTMJ006" , &dRslt[3]);
					              printf("@1--%d;%lf ",siRetRslt,dRslt[3]);
					              if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[3] = 99999;
  				  	          }
					              siRetRslt = dbGetDouble(Handler, "XTMJ008" , &dRslt[4]);
					              printf("@1--%d;%lf ",siRetRslt,dRslt[4]);
					              if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[4] = 99999;
  				  	          }
  				          }
  				          break;

  				          case 2:             /** 右导航相机图像DHY */
  				          {
					              siRetRslt = dbGetDouble(Handler,  "XTMK064" , &dRslt[0]);
					              printf("@11--%d;%lf ",siRetRslt,dRslt[0]);
					              if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[0] = 0;
  				  	          }
  				  	          siRetRslt = dbGetDouble(Handler,  "XTMK063" ,&dRslt[1]);
  				  	          printf("@12--%d;%lf ",siRetRslt,dRslt[1]);
  				  	          if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[1] = 0;
  				  	          }
  				  	          siRetRslt = dbGetDouble(Handler,  "XTMJ004" ,&dRslt[2]);
  				  	          printf("@13--%d;%lf ",siRetRslt,dRslt[2]);
  				  	          if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[2] = 99999;
  				  	          }
					              siRetRslt = dbGetDouble(Handler,  "XTMJ006", &dRslt[3]);
					              printf("@14--%d;%lf ",siRetRslt,dRslt[3]);
					              if(siRetRslt < 0)
  				              {
  				                  siFindFlag =0;
  				                  dRslt[3] = 99999;
  				              }
					              siRetRslt = dbGetDouble(Handler,  "XTMJ008" , &dRslt[4]);
					              printf("@15--%d;%lf ",siRetRslt,dRslt[4]);
					              if(siRetRslt < 0)
  				              {
  				               		siFindFlag =0;
  				               		dRslt[4] = 99999;
  				              }

				            }
  				          break;

  				          case 3:             /** 全景A相机图像QJZ */
  				          case 4:             /** 全景B相机图像QJY */
  				          {
  				  	          siRetRslt = dbGetDouble(Handler,  "XTMJ004" , &dRslt[2]);
  				  	          if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[2] = 99999;
  				  	          }
  				  	          printf("@13--%d;%lf ",siRetRslt,dRslt[2]);
					              siRetRslt = dbGetDouble(Handler,  "XTMJ006" ,&dRslt[3]);
					              if(siRetRslt < 0)
  				              {
  				              	siFindFlag =0;
  				              	dRslt[3] = 99999;
  				              }
					              printf("@14--%d;%lf ",siRetRslt,dRslt[3]);
					              siRetRslt = dbGetDouble(Handler,  "XTMJ008" ,&dRslt[4]);
					              if(siRetRslt < 0)
  				              {
  				              	siFindFlag =0;
  				              	dRslt[4] = 99999;
  				              }
					              printf("@15-%d;%lf ",siRetRslt,dRslt[4]);

  				          }
  				          break;

  				          case 5:             /** 左避障相机图像BZZ */
  				          {
   				  	          siRetRslt = dbGetDouble(Handler,  "XTMK070" , &dRslt[0]);
  				  	          if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[0] = 0;
  				  	          }
  				  	          printf("@1--%d;%lf ",siRetRslt,dRslt[0]);
  				  	          siRetRslt = dbGetDouble(Handler,  "XTMK069", &dRslt[1]);
  				  	          if(siRetRslt < 0)
  				  	          {
  				  	          	siFindFlag =0;
  				  	          	dRslt[1] = 0;
  				  	          }
  				  	          printf("@1--%d;%lf ",siRetRslt,dRslt[1]);

  				  	      }
  				   	      break;

                    case 6:             /** 右避障相机图像BZY */
                    {
  				  	         siRetRslt = dbGetDouble(Handler,  "XTMK076" , &dRslt[0]);
  				  	         if(siRetRslt < 0)
  				  	         {
  				  	         	siFindFlag =0;
  				  	         	dRslt[0] = 0;
  				  	         }
  				  	         printf("@1--%d;%lf ",siRetRslt,dRslt[0]);
  				  	         siRetRslt = dbGetDouble(Handler,  "XTMK075" , &dRslt[1]);
  				  	         if(siRetRslt < 0)
  				  	         {
  				  	         	siFindFlag =0;
  				  	         	dRslt[1] = 0;
  				  	         }
  				  	         printf("@1--%d;%lf ",siRetRslt,dRslt[1]);

  				          }
  				          break;

  				          default:
  				  	          siRetRslt = -1;
  				   	      break;
  				      }

  				      /** 如果所有参数均为有效值 */
  				      if(siFindFlag ==1)
  				      {
  				      	  /** 累计遍历数据帧数 */
  				      	  uiNum ++;

  				      	  /** 判断当前已查询数据个数是否与相应相机类型图像的查询帧个数相等，相等即退出循环 */
  				      	  if(uiNum == uiQueRTFraNum)
  				      	      break;

  				      	  /** 如果不相等，保存已查处的参数值。 */
  				      	  else
  				      	  {
  				      	  	  dRsltTemp[0] = dRslt[2];
  				      	  	  dRsltTemp[1] = dRslt[3];
  				      	  	  dRsltTemp[2] = dRslt[4];
  				      	  }
  				      }

				        dbNext(Handler);

    		    }



    		    /** 填写相机角度一致性标识 */

    		    /** 如果当前为避障相机图像，一致性标识为“NONE” */
    		    if(ucBZImg == 1)
    		    {
    		        strcpy(szAFlag, "NONE");
    		    	  strcpy(szHFlag, "NONE");
    		    	  strcpy(szPFlag, "NONE");
    		    }
    		    else /** 非避障相机图像，判断当前查询数据帧个数 */
    		    {
     		        /** 如果个数为0，一致性标识为“NONE” */
     		        if(uiNum == 0)
    		        {
    		        	  strcpy(szAFlag, "NONE");
    		        	  strcpy(szHFlag, "NONE");
    		        	  strcpy(szPFlag, "NONE");
    		        }
    		        /** 如果个数为1，一致性标识为“UNJUDGE” */
    		        else if(uiNum == 1)
    		        {

    		        	  strcpy(szAFlag, "UNJUDGE");
    		        	  strcpy(szHFlag, "UNJUDGE");
    		        	  strcpy(szPFlag, "UNJUDGE");

    		        }
    		        /** 如果个数为2,比较各类参数两帧结果的一致性 */
    		        else if(uiNum == 2)
    		        {
    		        	  if( abs(dRsltTemp[0] - dRslt[2]) <  ERROR_RTFRAMES )
    		        	  {
    		        	  	  /** 如果一致，标识为“TM” */
    		        	  	  strcpy(szAFlag, "TM");
    		        	  }
    		        	  else
    		        	  {
    		        	  	  /** 如果不一致，标识为“OVERFLOW” */
    		        	  	  strcpy(szAFlag, "OVERFLOW");
    		        	  }

    		        	  if( abs(dRsltTemp[1] - dRslt[3]) <  ERROR_RTFRAMES )
    		        	  {
    		        	  	  strcpy(szHFlag, "TM");
    		        	  }
    		        	  else
    		        	  {
    		        	  	  strcpy(szHFlag, "OVERFLOW");
    		        	  }

    		        	  if( abs(dRsltTemp[2] - dRslt[4]) <  ERROR_RTFRAMES )
    		        	  {
    		        	  	  strcpy(szPFlag, "TM");
    		        	  }
    		        	  else
    		        	  {
    		        	  	  strcpy(szPFlag, "OVERFLOW");
    		        	  }

    		        }

    		        /** 查询数据帧个数为2，使用先查询出的参数结果（即后下传的遥测数据） */
    		        if(uiQueRTFraNum == 2)
    		        {
    		              dRslt[2] = dRsltTemp[0];
  				          dRslt[3] = dRsltTemp[1];
  				          dRslt[4] = dRsltTemp[2];
  				      }

    		    }
    		    /////////////////////


    	  }

    	  /**  遥测数据查询结果无效，发送查询错误日志  */
    	  else
    	  {
    	        sprintf(szLog, "IMSI_TMSCQW_XS数传遥测数据查询异常,返回错误码%d", siRsltNum );
    	        LOG('E', szLog);

    	        strcpy(szAFlag, "NONE");
    		    strcpy(szHFlag, "NONE");
    		    strcpy(szPFlag, "NONE");
    	  }
    	  dbFree(Handler);
  		  /////////////////------------ 遥测参数查询 -------- end ----------///////////////////////////



    	  /** 站点信息查询数据库 *///-------- begin --------//

    	  /** 获取查询参数 */
    	  strcpy(szSelectStr,"uiStationID");

  		  /** 将成像时间由累计秒转化为“年月日时分秒”的格式 */
  		  siHour = pstImgFileHead->stTakePhoTime.uiSecond/(10000*3600);
  		  siMinute = (pstImgFileHead->stTakePhoTime.uiSecond/10000 -siHour*3600)/60;
  		  siSecond = pstImgFileHead->stTakePhoTime.uiSecond/10000 -siHour*3600- siMinute*60;
  		  siMSecond = pstImgFileHead->stTakePhoTime.uiSecond/10 -siHour*3600000- siMinute*60000;

    	  /** 获取查询条件 */
    	  sprintf(szWhereStr,"stCreateTime <= '%04d-%02d-%02d %02d:%02d:%02d.%03d'  order by RECVTIME desc ",
    	          pstImgFileHead->stTakePhoTime.usYear, pstImgFileHead->stTakePhoTime.usMonth, pstImgFileHead->stTakePhoTime.usDay,
    	          siHour, siMinute, siSecond, siMSecond);


    	  Handler = dbNew();
	      if(Handler == NULL)
    	  {
        	  return -1;
    	  }

    	  /** 获取数据库查询结果 */
    	  siRsltNum = dbQueryData(Handler, IMSI_STATIONCREATE,g_uiTaskCode,g_uiObjCode,szSelectStr, szWhereStr);
    	  printf("\n 站点查询 siRsltNum = %d, ", siRsltNum);

    	  if(siRsltNum > 0)
    	  {
    		  /** 结果有效，在查询到的数据中获取站点参数信息 */
    	      while(!dbEof(Handler))
    		    {
    		    	  siRetRslt = dbGetUInt(Handler,  "uiStationID" , &uiStationID);

    		    	  printf("%d,\n ", uiStationID);

    		    	  if(siRetRslt < 0)
        		    	  dbNext(Handler);
    		    	  else
    		    	      break;
    		    }
    	   }
    	   /** 结果无效，发送查询错误日志 */
    	   else
    	   {
    	        sprintf(szLog, "IMSI_STATIONCREATE站点查询异常,返回错误码%d", siRsltNum );
    	        LOG('E', szLog);
    	   }
    	  dbFree(Handler);
    	  ////////站点查询 -------- end --------//



  	    /** 查询巡视器位置姿态参数————IFLI_RAPSAT文件查询 *////////////--------- begin --------//	//////////

  		  memset(szWhereStr,0,sizeof(szWhereStr));

    	  Handler = dbNew();
	      if(Handler == NULL)
    	  {
            return -1;
    	  }

//    	  sprintf(szWhereStr,"stTaskTime <= '%04d-%02d-%02d %02d:%02d:%02d.%03d'  order by RECVTIME desc ",
//    	          pstImgFileHead->stTakePhoTime.usYear, pstImgFileHead->stTakePhoTime.usMonth, pstImgFileHead->stTakePhoTime.usDay,
//    	          siHour, siMinute, siSecond, siMSecond+20);

		  /** 设置文件查询条件 */
		  sprintf(szWhereStr,"uiStandID = %d  order by RECVTIME desc ",uiStationID);

    	  /** 获取数据库复核条件的文件包结果 */
    	  siRsltNum = dbQueryFile(Handler,IFLI_RAPSAT,g_uiTaskCode,g_uiObjCode, szWhereStr);
    	  printf("文件查询 siRsltNum = %d \n ", siRsltNum);

    	  if(siRsltNum > 0)
    	  {
    	  	  /** 结果有效，在查询到的数据中检索文件包查找IFLI_RAPSAT文件 */
    	      while(!dbEof(Handler))
    	      {
    	    	    /** 获取文件包中文件个数 */
        		    strcpy(szName, "uiFileNum");
        		    siRetRslt = dbGetUInt(Handler, szName, &uiFileNum);
        		    if(siRetRslt < 0)
        		    {
        		    	  dbNext(Handler);
        		        continue;
        		    }
        		    printf("%d, ", uiFileNum);

        		    /** 获取系统文件名*/
	    	        strcpy(szName, "szFileNameS");
		            siRetRslt = dbGetString(Handler, szName, szFileNameS, sizeof(szFileNameS));
		            if(siRetRslt < 0)
		            {
		                dbNext(Handler);
        		        continue;
		            }
		            printf("%s, ", szFileNameS);

		            /** 获取用户文件名 */
        		    for(i1 = 0; i1 < uiFileNum; i1++)
        		    {
        		        sprintf(szName, "szFileName[%d]", i1+1);
        		        siRetRslt = dbGetString(Handler, szName, szFileName, sizeof(szFileName));
        		        if(siRetRslt < 0)
        		        {
        		        	continue;
        		        }
        		        printf("%s, ", szFileName);

        		        /** 获取带路径的文件名 */
        		        memset(szPathFileName,0,sizeof(szPathFileName));
        		        sprintf(szPathFileName,"%s/%s",g_szFileRsltName,szFileName);
				        printf("查询文件szFileName=%s;",szFileName);

					    /** 解析文件发布消息 */
					    msghead.uiFileType = 5141;
					    msghead.uiTaskCode = g_uiTaskCode;
					    msghead.uiObjCode =  g_uiObjCode;

					    siRetRslt = zf2_getone(&msghead, szPathFileName, szFileNameS);
					    if(siRetRslt == 0)
					    {
					        break;
					    }
        		    }

				    /** 解析IFLI_RAPSAT文件，获取相应位置姿态参数值 */
				    siRetRslt = MCDE_cmlIFLIRAPAST(szPathFileName,&stFileRapast);

    	            /** 参数获取成功，打印查询到的文件名称 */
    	            if(siRetRslt < 0)
    	            {
					    sprintf(szLog, "解析IFLI_RAPSAT文件名称异常，错误码%d", siRetRslt);
    	                LOG('E', szLog);
				    }
				    else
				    {
					    sprintf(szLog, "获取IFLI_RAPSAT文件名称为%s", szFileName);
    	                LOG('R', szLog);
					          break;
				        }

				        dbNext(Handler);

    	      }
    	  }
    	  else
    	  {
    	        sprintf(szLog, "使用站点号%d 查询IFLI_RAPSAT文件异常,返回错误码%d", uiStationID, siRsltNum );
    	        LOG('E', szLog);
    	  }
    	  dbFree(Handler);
    	  ///// 文件查询 --------- end --------//



  	      /** 实时太阳高度角、方位角查询 *///--------- begin --------//

  	      memset(&stCJtime,0,sizeof(stCJtime));
  	      memset(&stCJtime1,0,sizeof(stCJtime1));

		  /** 设置查询参数 */
		  strcpy(szSelectStr,"dSunAL,dSunEL,dSunAB,dSunEB");


		  /** 获取成像的北京时 */
		  uiCJtime = pstImgFileHead->uiTakePhoTimeLJ;

		  if(g_uiObjCode == 0x18fb)//验证器采集时间为UTC时，需要转换成北京时：增加8小时
		  {
			   uiCJtime = uiCJtime + 8*3600.0;
		  }

		  /** 成像时间转化为“年月日时分秒”的格式 */
		  get_miscj_time(uiCJtime,&stCJtime);
		  get_miscj_time((uiCJtime-12),&stCJtime1);
		  printf(" uiCJtime = %d, -12=%d;%d;%d;\n", uiCJtime,(uiCJtime-12),stCJtime.uiSecond,stCJtime1.uiSecond);

		  siHour = stCJtime.uiSecond/(10000*3600);
  		  siMinute = (stCJtime.uiSecond/10000 -siHour*3600)/60;
  		  siSecond = stCJtime.uiSecond/10000 -siHour*3600- siMinute*60;
  		  siMSecond = stCJtime.uiSecond/10 -siHour*3600000- siMinute*60000;

  		  siHour1 = stCJtime1.uiSecond/(10000*3600);
  		  siMinute1 = (stCJtime1.uiSecond/10000 -siHour1*3600)/60;
  		  siSecond1 = stCJtime1.uiSecond/10000 -siHour1*3600- siMinute1*60;
  		  siMSecond1 = stCJtime1.uiSecond/10 -siHour1*3600000- siMinute1*60000;

          printf("CJTime = %04d-%02d-%02d %02d:%02d:%02d.%03d \n", stCJtime.usYear, stCJtime.usMonth, stCJtime.usDay, siHour, siMinute, siSecond, siMSecond);
  		  printf("CJTime1 = %04d-%02d-%02d %02d:%02d:%02d.%03d \n", stCJtime1.usYear, stCJtime1.usMonth, stCJtime1.usDay, siHour1, siMinute1, siSecond1, siMSecond1);

    	  /** 设置查询条件：查询成像时间到之前的12秒内 */
    	  sprintf(szWhereStr,"stTime <= '%04d-%02d-%02d %02d:%02d:%02d.%03d' and  stTime >= '%04d-%02d-%02d %02d:%02d:%02d.%03d'   order by RECVTIME desc ",
    	          stCJtime.usYear, stCJtime.usMonth, stCJtime.usDay, siHour, siMinute, siSecond, siMSecond,
    	          stCJtime1.usYear, stCJtime1.usMonth, stCJtime1.usDay, siHour1, siMinute1, siSecond1, siMSecond1);


    	  Handler = dbNew();
	      if(Handler == NULL)
    	  {
        	  return -1;
    	  }

    	  /** 获取数据库查询结果 */
    	  siRsltNum = dbQueryData(Handler, IMSI_RAPSAT,g_uiTaskCode,g_uiObjCode,szSelectStr, szWhereStr);
    	  printf(" 实时太阳角查询 siRsltNum = %d, \n", siRsltNum);

    	  if(siRsltNum > 0)
    	  {
    		  /** 结果有效，在查询到的数据中获取太阳参数信息，无效值为99999 */
    	      while(!dbEof(Handler))
    		    {
    		    	  siRetRslt = dbGetDouble(Handler,  "dSunAL" , &dSunAngle[0]);
    		    	  if(siRetRslt < 0)
    		    	  {
    		    	      dSunAngle[0] = 99999;
        		    	  dbNext(Handler);
        		      }
        		      printf("***dSunAL:  %lf ",dSunAngle[0]);

  				  	  siRetRslt = dbGetDouble(Handler,  "dSunEL" ,&dSunAngle[1]);
    		    	  if(siRetRslt < 0)
    		    	  {
        		    	  dSunAngle[1] = 99999;
        		    	  dbNext(Handler);
        		      }
        		      printf("***dSunEL:  %lf ",dSunAngle[1]);

					  siRetRslt = dbGetDouble(Handler,  "dSunAB" ,&dSunAngle[2]);
    		    	  if(siRetRslt < 0)
    		    	  {
        		    	  dSunAngle[2] = 99999;
        		    	  dbNext(Handler);
        		      }
        		      printf("***dSunAB:  %lf ",dSunAngle[2]);

				      siRetRslt = dbGetDouble(Handler,  "dSunEB" ,&dSunAngle[3]);
    		    	  if(siRetRslt < 0)
    		    	  {
        		    	  dSunAngle[3] = 99999;
        		    	  dbNext(Handler);
        		      }
        		      printf("***dSunEB:  %lf \n",dSunAngle[3]);

    		    	  if(siRetRslt >= 0)
    		    	      break;
    		    }
    		}
    		else
    		{
    	        /** 结果无效，返回错误日志 */
    	        sprintf(szLog, "IMSI_RAPSAT太阳角度查询异常,返回错误码%d", siRsltNum );
    	        LOG('E', szLog);
    	  }
    	  dbFree(Handler);

    	  ////////实时太阳角查询 -------- end --------//


    }

    /** 图像的成像时间异常，不进行数据库查询，生成错误日志 */
    else
    {
    	  LOG('E', "图像的成像时间异常，无法进行车体及相机信息查询！");
    }


    /** 查询结果写入结构 */

    pstQueDB->uiTakePhoTimeLJ = pstImgFileHead->uiTakePhoTimeLJ;
    pstQueDB->uiFileType      = uiFileType;
    pstQueDB->uiStandID       = uiStationID;
    pstQueDB->ucLeftRightID   = pstImgFileHead->ucLeftRightID;
    pstQueDB->dCamBGZY        = dRslt[0];
	pstQueDB->dCamImgTime     = dRslt[1];

	pstQueDB->dA = dRslt[2];
	strcpy(pstQueDB->szAFlag,szAFlag);
	pstQueDB->dH = dRslt[3];
	strcpy(pstQueDB->szHFlag,szHFlag);
	pstQueDB->dP = dRslt[4];
	strcpy(pstQueDB->szPFlag,szPFlag);

 	strcpy(pstQueDB->szRefFrame,stFileRapast.szPosRefFrame);	       /*坐标系标识*/

    pstQueDB->dPosX 	 = stFileRapast.dX;        	 /*成像时航天器位置经度*/
    pstQueDB->dPosY 	 = stFileRapast.dY;        	 /*成像时航天器位置维度*/
    pstQueDB->dPosZ 	 = stFileRapast.dZ;       	 /*成像时航天器位置高度*/
    pstQueDB->dLog	   = stFileRapast.dLog;        /*成像时航天器位置*/
    pstQueDB->dLat	   = stFileRapast.dLat;        /*成像时航天器位置*/
    pstQueDB->dHeight  = stFileRapast.dHeight;     /*成像时航天器位置*/

    pstQueDB->dRoll    = stFileRapast.dRoll;       /*-180~180	成像时航天器姿态（滚动）*/
    pstQueDB->dPitch   = stFileRapast.dPitch;      /*-180~180	成像时航天器姿态（俯仰）*/
    pstQueDB->dAzimuth = stFileRapast.dYaw;        /*-180~180	成像时航天器姿态（偏航）*/

    pstQueDB->dSunAL	 = dSunAngle[0];      /*当地太阳方位角*/
    pstQueDB->dSunEL 	 = dSunAngle[1];      /*当地太阳高度角*/
    pstQueDB->dSunAB 	 = dSunAngle[2];      /*车体太阳方位角*/
    pstQueDB->dSunEB 	 = dSunAngle[3];      /*车体太阳高度角*/

  	return uiFileType;

}