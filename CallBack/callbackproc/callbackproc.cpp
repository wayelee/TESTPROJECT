// callbackproc.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "callbackproc.h"
#include <sstream>
#include <time.h>  

fnCallBackFunc m_func;
std::string m_strPath ="";
//int nCount =0;

// This is an example of an exported function.
CALLBACKPROC_API void Register_Callback(fnCallBackFunc func)
{
	m_func=func;
	
	//int count = 0;
	//time_t rawtime;
	//struct tm * timeinfo;

	//
	//// let's send 10 messages to the subscriber
	//while(count < 10)
	//{
	//	// format the message
	//	time ( &rawtime );
	//    timeinfo = localtime ( &rawtime );
	//	std::stringstream ss;
	//	ss<<timeinfo->tm_year
	//		<<"/"<<timeinfo->tm_mon
	//		<<"/"<<timeinfo->tm_mday
	//		<<"/"<<timeinfo->tm_hour
	//		<<"/"<<timeinfo->tm_min
	//		<<"/"<<timeinfo->tm_sec;
	//	std::string str = ss.str();

	//	int n = str.length();
	//	char *buf = new char[strlen(str.c_str())+1];
	//	strcpy(buf, str.c_str());

	//	// call the callback function
	//	func(buf,n);
	//	
	//	count++;

	//	// Sleep for 2 seconds
	//	Sleep(1000);
	//}
}

CALLBACKPROC_API void SetPath(char* value)
{
	std::string str(value);
	m_strPath = str;
	printf("callback: %s\n", str.c_str());
}

CALLBACKPROC_API void StartRv()
{
	std::string str;
	int n;
	time_t rawtime;
	struct tm * timeinfo;

	//²âÊÔ´«ÈëÂ·¾¶
	n = m_strPath.length();
	char *buf0 = new char[strlen(m_strPath.c_str())+1];
	strcpy_s(buf0, n+1,m_strPath.c_str());

	m_func(buf0,n);

	int nCount =0;
	while(1)
	{
		// format the message
		time ( &rawtime );
	    timeinfo = localtime( &rawtime );
		std::stringstream ss;
		ss<<nCount
			<<" "<<timeinfo->tm_year+1900
			<<"/"<<timeinfo->tm_mon+1
			<<"/"<<timeinfo->tm_mday
			<<" "<<timeinfo->tm_hour
			<<":"<<timeinfo->tm_min
			<<":"<<timeinfo->tm_sec;
		str = ss.str();
		
		n = str.length();
		char *buf = new char[strlen(str.c_str())+1];
		strcpy_s(buf,n+1,str.c_str());

		m_func(buf,n);

		nCount++;
		// Sleep for 2 seconds
		Sleep(1000);
	}
}





