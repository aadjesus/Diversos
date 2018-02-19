#include <windows.h>
#include <tchar.h>
#include "resource.h"
#include "CaptionButton.h"

TCHAR szAppName[] = _T("Custom Titlebar");

HBITMAP hBmp1, hBmp2;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	TCHAR szText[80];

	switch(msg)
	{
	case WM_CREATE:

		// These must be MONOCHROME bitmaps if you want them
		// to be drawn nicely.
		hBmp1 = NULL ;//LoadBitmap(GetModuleHandle(0), MAKEINTRESOURCE(IDB_BITMAP1));
		hBmp2 = NULL ;//LoadBitmap(GetModuleHandle(0), MAKEINTRESOURCE(IDB_BITMAP2));

		Caption_InsertButton(hwnd, 2, 2, hBmp2);
		Caption_InsertButton(hwnd, 1, 0, hBmp1);
		return 0;

	case WM_COMMAND:
		
		wsprintf(szText, _T("%s %d [%d, %d]"), szAppName, wParam, LOWORD(lParam), HIWORD(lParam));
		SetWindowText(hwnd, szText);
		return 0;

	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;

	case WM_CLOSE:
		DestroyWindow(hwnd);
		return 0;
	}

	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrev, LPSTR lpCmdLine, int iShowCmd)
{
	HWND		hwnd;
	MSG			msg;
	WNDCLASSEX	wc;

	//Window class for the main application parent window
	wc.cbSize			= sizeof(wc);
	wc.style			= 0;
	wc.lpfnWndProc	    = WndProc;
	wc.cbClsExtra		= 0;
	wc.cbWndExtra		= 0;
	wc.hInstance		= hInst;
	wc.hIcon			= LoadIcon(0, MAKEINTRESOURCE(IDI_APPLICATION));
	wc.hCursor		    = LoadCursor (NULL, IDC_ARROW);
	wc.hbrBackground	= (HBRUSH)(COLOR_APPWORKSPACE+1);
	wc.lpszMenuName	    = 0;
	wc.lpszClassName	= szAppName;
	wc.hIconSm		    = LoadIcon (NULL, IDI_APPLICATION);

	RegisterClassEx(&wc);

	hwnd = CreateWindowEx(WS_EX_CLIENTEDGE,// | WS_EX_TOOLWINDOW,
				szAppName,				// window class name
				szAppName,				// window caption
				WS_OVERLAPPEDWINDOW,	// window style
				//WS_OVERLAPPED | WS_SYSMENU | WS_CAPTION,

				CW_USEDEFAULT,			// initial x position
				CW_USEDEFAULT,			// initial y position
				560,					// initial x size
				320,					// initial y size
				NULL,					// parent window handle
				NULL,					// use window class menu
				hInst,					// program instance handle
				NULL);					// creation parameters

	ShowWindow(hwnd, iShowCmd);

	while(GetMessage(&msg, NULL,0,0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;
}