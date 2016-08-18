// serverpipe.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "windows.h"
#include "string.h"
#include "iostream" 
#include<TCHAR.H>
#include <strsafe.h>
using namespace std;

#define CONNECTING_STATE 0 
#define READING_STATE 1 
#define WRITING_STATE 2 
#define INSTANCES 5 
#define PIPE_TIMEOUT 5000
#define BUFSIZE 128

 
BOOL Read[INSTANCES];
OVERLAPPED oOverlap[INSTANCES];
HANDLE hEvents[INSTANCES];
wchar_t hBuffer[256];

int _tmain(VOID)
{
	setlocale(LC_ALL, "Russian");
	DWORD WaitEvent;
	LPTSTR hPipename = TEXT("\\\\.\\pipe\\$MyPipe$");
	HANDLE hPipe[INSTANCES];
	for (DWORD i = 0; i < INSTANCES; i++){
		hEvents[i] = CreateEvent(NULL, TRUE, TRUE, NULL);
		oOverlap[i].hEvent = hEvents[i];
		hPipe[i] = CreateNamedPipe(
			hPipename, PIPE_ACCESS_DUPLEX | FILE_FLAG_OVERLAPPED, PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE,
			INSTANCES, BUFSIZE, 0, 0, NULL);
		ConnectNamedPipe(hPipe[i], &oOverlap[i]);
	}
	while (true){
		printf("!!!");
		WaitEvent = WaitForMultipleObjects(INSTANCES, hEvents, FALSE, INFINITE);
		printf("!!!");
		int i = WaitEvent - WAIT_OBJECT_0;
		ResetEvent(hEvents[i]);
		DWORD Inf1;
		if (GetOverlappedResult(hPipe[i], &oOverlap[i], &Inf1, TRUE) == 0){
			
		}
		else{
			if (Read[i] == false){
				if (ReadFile(hPipe[i], hBuffer, BUFSIZE, NULL, &oOverlap[i]) == 0){
					printf("lol");
				}
				Read[i] = true;
			}
			else{
				printf("pop");
				Read[i] = false;
				_tprintf(TEXT("[%d] %s\n"), hPipe[i], hBuffer);
				DisconnectNamedPipe(hPipe[i]);
				ConnectNamedPipe(hPipe[i], &oOverlap[i]);
			}
		}

	}
	return 0;





	/*
	DWORD i, dwWait, cbRet;
	BOOL fSuccess;
	LPTSTR lpszPipename = TEXT("\\\\.\\pipe\\$MyPipe$");

	for (i = 0; i < INSTANCES; i++)
	{

		hEvents[i] = CreateEvent(NULL, TRUE, TRUE, NULL);

		Pipe[i].oOverlap.hEvent = hEvents[i];

		Pipe[i].hPipeInst = CreateNamedPipe(
			lpszPipename, PIPE_ACCESS_DUPLEX | FILE_FLAG_OVERLAPPED, PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT, 
			INSTANCES, BUFSIZE*sizeof(TCHAR), BUFSIZE*sizeof(TCHAR), PIPE_TIMEOUT, NULL);

		Pipe[i].fPendingIO = ConnectNamedPipe(Pipe[i].hPipeInst, &Pipe[i].oOverlap);

		Pipe[i].dwState = Pipe[i].fPendingIO ? CONNECTING_STATE : READING_STATE;
	}

	while (true)
	{
		dwWait = WaitForMultipleObjects(INSTANCES, hEvents, FALSE, INFINITE); 

		i = dwWait - WAIT_OBJECT_0;  

		if (Pipe[i].fPendingIO)
		{
			fSuccess = GetOverlappedResult(Pipe[i].hPipeInst, &Pipe[i].oOverlap, &cbRet, FALSE);

			switch (Pipe[i].dwState)
			{
			case CONNECTING_STATE:
				Pipe[i].dwState = READING_STATE;
				break;
			case READING_STATE:
				if (!fSuccess)
				{
					DisconnectAndReconnect(i);
					continue;
				}
				Pipe[i].dwState = READING_STATE;
				break;
			default: return 0;
			}
		}


		switch (Pipe[i].dwState)
		{
		case READING_STATE:
			fSuccess = ReadFile(Pipe[i].hPipeInst, Pipe[i].chBuffer, BUFSIZE*sizeof(TCHAR), &Pipe[i].cbRead, &Pipe[i].oOverlap);

			_tprintf(TEXT("[%d] %s\n"), Pipe[i].hPipeInst, Pipe[i].chBuffer);
			if (fSuccess && Pipe[i].cbRead != 0)
			{
				Pipe[i].fPendingIO = FALSE;
				Pipe[i].dwState = READING_STATE;

				DisconnectAndReconnect(i);
				continue;
			}

			if (!fSuccess)
			{
				Pipe[i].fPendingIO = TRUE;
				continue;
			}

			DisconnectAndReconnect(i);
			break;
		default: return 0;
		}
	}

	return 0;*/
}
/*
VOID DisconnectAndReconnect(DWORD i)
{
	if (!DisconnectNamedPipe(Pipe[i].hPipeInst))
	{
		printf("DisconnectNamedPipe failed with %d.\n", GetLastError());
	}
	Pipe[i].fPendingIO = ConnectNamedPipe(Pipe[i].hPipeInst, &Pipe[i].oOverlap);
	Pipe[i].dwState = Pipe[i].fPendingIO ? CONNECTING_STATE : READING_STATE;
}*/