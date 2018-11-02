// CallbackSample.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <string>
#include <windows.h>
#include <string>

#include "callbackproc.h"

// Callback function to print message receive from DLL
void CALLBACK MyCallbackFunc(char* value,int n)
{
	std::string str(value);
	printf("callback: %s\n", str.c_str());
}

int _tmain(int argc, _TCHAR* argv[])
{
	// Register the callback to the DLL
	Register_Callback(MyCallbackFunc);
	StartRv();

	return 0;
}