// Win32Project1.cpp: определяет точку входа для приложения.
//
/* СОЗДАТЬ ПАЙП ЧАТ ЧЕРЕЗ СООБЩЕНИЯ, КОЛИЧЕСТВО 1*/

#include "stdafx.h"

#include<TCHAR.H>
#include "Win32Project1.h"
#include "stdio.h"
#include <iostream>
#define MAX_LOADSTRING 100
#define ID_STR_LINE 9001
#define ID_BTN_SEND 2124
#define ID_BTN_CONNECT 2125
#define ID_STR_LINECON 9002
using namespace std;
// Глобальные переменные:
HINSTANCE hInst;								// текущий экземпляр
wchar_t szTitle[MAX_LOADSTRING];					// Текст строки заголовка
wchar_t szWindowClass[MAX_LOADSTRING];			// имя класса главного окна
HINSTANCE hDllInstance;
wchar_t g_pBuffer[256];
wchar_t g_lNamePipe[256];
HANDLE g_hPipe;
DWORD cbRead;
DWORD cbWritten;
// Отправить объявления функций, включенных в этот модуль кода:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPTSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

 	// TODO: разместите код здесь.
	MSG msg;
	HACCEL hAccelTable;

	// Инициализация глобальных строк
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_WIN32PROJECT1, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Выполнить инициализацию приложения:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WIN32PROJECT1));

	// Цикл основного сообщения:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
	return (int) msg.wParam;
}



//
//  ФУНКЦИЯ: MyRegisterClass()
//
//  НАЗНАЧЕНИЕ: регистрирует класс окна.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WIN32PROJECT1));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_BTNSHADOW);
	wcex.lpszMenuName = NULL;// MAKEINTRESOURCE(IDC_WIN32PROJECT1);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

//
//   ФУНКЦИЯ: InitInstance(HINSTANCE, int)
//
//   НАЗНАЧЕНИЕ: сохраняет обработку экземпляра и создает главное окно.
//
//   КОММЕНТАРИИ:
//
//        В данной функции дескриптор экземпляра сохраняется в глобальной переменной, а также
//        создается и выводится на экран главное окно программы.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   HWND hWnd;
  // HANDLE hMutex;
   
   CHAR PP[] = "1";
   hInst = hInstance; // Сохранить дескриптор экземпляра в глобальной переменной

   hWnd = CreateWindow(szWindowClass, L"My First Win32 Window", WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, 430, 120, NULL, NULL, hInstance, NULL);

   CreateWindowEx(WS_EX_STATICEDGE, L"BUTTON", L"Connect", WS_CHILD | WS_VISIBLE, 280, 40, 120, 25, hWnd, (HMENU)ID_BTN_CONNECT, hInstance, NULL); // ДЛЯ ЛАБЫ ИСПОЛЬЗОВАТЬ !!! RECEIVERS (RECHEID,RICHEDIT)КАРОЧИ ЧТО ТО ПОДОБНОЕ)!!!
   CreateWindowEx(WS_EX_CLIENTEDGE, L"EDIT", L"\\\\.\\pipe\\$MyPipe$", WS_CHILD | WS_VISIBLE, 10, 40, 260, 30, hWnd, (HMENU)ID_STR_LINECON, hInstance, NULL); //дЛЯ НАС УЧЕСТЬ |CBS_AUTOHSCROLL

   CreateWindowEx(WS_EX_STATICEDGE,L"BUTTON", L"Send to server", WS_CHILD | WS_VISIBLE, 280, 12, 120, 25, hWnd,(HMENU)ID_BTN_SEND,hInstance,NULL ); // ДЛЯ ЛАБЫ ИСПОЛЬЗОВАТЬ !!! RECEIVERS (RECHEID,RICHEDIT)КАРОЧИ ЧТО ТО ПОДОБНОЕ)!!!
   CreateWindowEx(WS_EX_CLIENTEDGE, L"EDIT", L"", WS_CHILD| WS_VISIBLE, 10, 10 ,260,30, hWnd, (HMENU)ID_STR_LINE, hInstance, NULL ); //дЛЯ НАС УЧЕСТЬ |CBS_AUTOHSCROLL
   if (!hWnd)
   {
      return FALSE;
   }



   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  ФУНКЦИЯ: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  НАЗНАЧЕНИЕ:  обрабатывает сообщения в главном окне.
//
//  WM_COMMAND	- обработка меню приложения
//  WM_PAINT	-Закрасить главное окно
//  WM_DESTROY	 - ввести сообщение о выходе и вернуться.
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) // функция инициализирует само окно
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message)
	{
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Разобрать выбор в меню:
		switch (wmId)
		{
		case ID_BTN_SEND:
			GetDlgItemText(hWnd, ID_STR_LINE, (wchar_t*)g_pBuffer, 512);
			g_hPipe = CreateFile(g_lNamePipe, GENERIC_READ | GENERIC_WRITE,
				0, NULL, OPEN_EXISTING, 0, NULL);
			WriteFile(g_hPipe, (wchar_t*)g_pBuffer, _tcslen(g_pBuffer) * 2, &cbWritten, NULL);
			CloseHandle(g_hPipe);
			break;
		case ID_BTN_CONNECT:
			GetDlgItemText(hWnd, ID_STR_LINECON, (wchar_t*)g_lNamePipe, 512);
			g_hPipe = CreateFile(g_lNamePipe, GENERIC_READ | GENERIC_WRITE,
				0, NULL, OPEN_EXISTING, 0, NULL);
			if (g_hPipe == INVALID_HANDLE_VALUE){
				//MessageBox(hWnd, (wchar_t*)g_lNamePipe, L"Info Connect", MB_OK);
				return false;
			} else MessageBox(hWnd, L"Соединение установлено", L"Info Connect", MB_OK);
			break;
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);																	
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		// TODO: добавьте любой код отрисовки...
		EndPaint(hWnd, &ps);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// Обработчик сообщений для окна "О программе".
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}
