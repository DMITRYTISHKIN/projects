// ConsoleApplication1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "windows.h"
#include "string.h"
#include "iostream" 
using namespace std;


int _tmain(int argc, _TCHAR* argv[])
{
	HANDLE hPipe = CreateNamedPipe("\\\\.\\pipe\\$MyPipe$",
		PIPE_ACCESS_DUPLEX,
		PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_NOWAIT,
		PIPE_UNLIMITED_INSTANCES, 256, 256, 5000, NULL
		);
	DWORD  cbRead;
	char pBuffer[256];
	ConnectNamedPipe("\\\\.\\pipe\\$MyPipe$", NULL);
	while (true){
		if (ReadFile(hPipe, pBuffer, 512, &cbRead, NULL)){
			cout << pBuffer << endl;
		}
	}

	return 0;
}

