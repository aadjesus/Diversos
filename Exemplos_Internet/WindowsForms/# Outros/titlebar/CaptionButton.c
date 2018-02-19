//
//
//
#include <windows.h>
#include <tchar.h>

static TCHAR szPropName[] = _T("CustomCaptionSubclassPtr");

#define MAX_TITLE_BUTTONS	8
#define B_EDGE				2

typedef struct
{
	UINT	uCmd;			//command to send when clicked
	int		nRightBorder;	//Pixels between this button and buttons to the right
	HBITMAP hBmp;			//Bitmap to display

	BOOL	fPressed;		//Private.
	
} CaptionButton;

typedef struct
{
	CaptionButton buttons[MAX_TITLE_BUTTONS];
	int			nNumButtons;
	BOOL		fMouseDown;
	WNDPROC		wpOldProc;

	int			iActiveButton;

} CustomCaption;

static CustomCaption *GetCustomCaption(HWND hwnd)
{
	return (CustomCaption *)GetProp(hwnd, szPropName);
}

static void RedrawNC(HWND hwnd)
{
	SetWindowPos(hwnd, 0, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE | SWP_DRAWFRAME);
}

static int CalcTopEdge(HWND hwnd)
{
	DWORD dwStyle = GetWindowLong(hwnd, GWL_STYLE);

	if(dwStyle & WS_THICKFRAME)
		return GetSystemMetrics(SM_CYSIZEFRAME);
	else
		return GetSystemMetrics(SM_CYFIXEDFRAME);
}

static int CalcRightEdge(HWND hwnd)
{
	DWORD dwStyle = GetWindowLong(hwnd, GWL_STYLE);

	if(dwStyle & WS_THICKFRAME)
		return GetSystemMetrics(SM_CXSIZEFRAME);
	else
		return GetSystemMetrics(SM_CXFIXEDFRAME);

}

//
//	Return the starting position of the first inserted button.
//	We need to take into account the size of the close button,
//	The minimize, maximize buttons etc
//
//	-------------------------------------------------\
//                                        [-][+] [X] |
//                                       ^
//                                       |
//                   Return is pos       |--- Ret  --|
//
//
static int GetRightEdgeOffset(CustomCaption *ctp, HWND hwnd)
{
	DWORD dwStyle   = GetWindowLong(hwnd, GWL_STYLE);
	DWORD dwExStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

	int nButSize = 0;
	int nSysButSize;

	if(dwExStyle & WS_EX_TOOLWINDOW)
	{
		nSysButSize = GetSystemMetrics(SM_CXSMSIZE) - B_EDGE;

		if(dwStyle & WS_SYSMENU)	
			nButSize += nSysButSize + B_EDGE;

		return nButSize + CalcRightEdge(hwnd);
	}
	else
	{
		nSysButSize = GetSystemMetrics(SM_CXSIZE) - B_EDGE;

		// Window has Close [X] button. This button has a 2-pixel
		// border on either size
		if(dwStyle & WS_SYSMENU)	
		{
			nButSize += nSysButSize + B_EDGE;
		}

		// If either of the minimize or maximize buttons are shown,
		// Then both will appear (but may be disabled)
		// This button pair has a 2 pixel border on the left
		if(dwStyle & (WS_MINIMIZEBOX | WS_MAXIMIZEBOX) )
		{
			nButSize += B_EDGE + nSysButSize * 2;
		}
		// A window can have a question-mark button, but only
		// if it doesn't have any min/max buttons
		else if(dwExStyle & WS_EX_CONTEXTHELP)
		{
			nButSize += B_EDGE + nSysButSize;
		}
		
		// Now calculate the size of the border...aggghh!
		return nButSize + CalcRightEdge(hwnd);
	}
}


//
//	Input  - rect is coords of window rectangle
//			 (in screen or relative coords)
//
//	Output - rect is adjusted to describe button rectangle
//
static void GetButtonRect(CustomCaption *ctp, HWND hwnd, int idx, RECT *rect, BOOL fWindowRelative)
{
	int i, re_start;
	int cxBut, cyBut;

	if(GetWindowLong(hwnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW)
	{
		cxBut = GetSystemMetrics(SM_CXSMSIZE);
		cyBut = GetSystemMetrics(SM_CYSMSIZE);
	}
	else
	{
		cxBut = GetSystemMetrics(SM_CXSIZE);
		cyBut = GetSystemMetrics(SM_CYSIZE);
	}

	// right-edge starting point of inserted buttons
	re_start = GetRightEdgeOffset(ctp, hwnd);

	GetWindowRect(hwnd, rect);
	
	if(fWindowRelative)
		OffsetRect(rect, -rect->left, -rect->top);

	//Find the correct button - but take into
	//account all other buttons.
	for(i = 0; i <= idx; i++)
	{
		re_start += ctp->buttons[i].nRightBorder + cxBut - B_EDGE;
	}

	rect->left   = rect->right  - re_start;
	rect->top    = rect->top  + CalcTopEdge(hwnd) +  B_EDGE;
	rect->right  = rect->left + cxBut - B_EDGE;
	rect->bottom = rect->top  + cyBut - B_EDGE*2;
}

static LRESULT Caption_NCHitTest(CustomCaption *ctp, HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	RECT  rect;
	POINT pt;
	int   i;
	UINT  ret;

	pt.x = (short)LOWORD(lParam);
	pt.y = (short)HIWORD(lParam);

	ret = CallWindowProc(ctp->wpOldProc, hwnd, WM_NCHITTEST, wParam, lParam);

	//If the mouse is in the caption, then check to
	//see if it is over one of our buttons
	if(ret == HTCAPTION)
	{
		for(i = 0; i < ctp->nNumButtons; i++)
		{
			GetButtonRect(ctp, hwnd, i, &rect, FALSE);
			InflateRect(&rect, 0, B_EDGE);

			//If the mouse is in any custom button, then
			//We need to override the default behaviour.
			if(PtInRect(&rect, pt))
			{
				return HTBORDER;
			}
		}
	}

	return ret;
}

//
//	If hBitmap is MONOCHROME then 
//	whites will be drawn transparently, 
//  blacks will be drawn normally
//	So, it will look just like a caption button
//
//	If hBitmap is > 2 colours, then no transparent
//  drawing will take place....i.e. DIY!
//
static void CenterBitmap(HDC hdc, RECT *rc, HBITMAP hBitmap)
{
	BITMAP bm;
	int cx;
	int cy;   
	HDC memdc;
	HBITMAP hOldBM;
	RECT  rcDest = *rc;   
	POINT p;
	SIZE  delta;
	COLORREF colorOld;

	if(hBitmap == NULL) 
		return;

	// center bitmap in caller's rectangle   
	GetObject(hBitmap, sizeof bm, &bm);   
	
	cx = bm.bmWidth;
	cy = bm.bmHeight;

	delta.cx = (rc->right-rc->left - cx) / 2;
	delta.cy = (rc->bottom-rc->top - cy) / 2;
	
	if(rc->right-rc->left > cx)
	{
		SetRect(&rcDest, rc->left+delta.cx, rc->top + delta.cy, 0, 0);   
		rcDest.right = rcDest.left + cx;
		rcDest.bottom = rcDest.top + cy;
		p.x = 0;
		p.y = 0;
	}
	else
	{
		p.x = -delta.cx;   
		p.y = -delta.cy;
	}
   
	// select checkmark into memory DC
	memdc = CreateCompatibleDC(hdc);
	hOldBM = (HBITMAP)SelectObject(memdc, hBitmap);
   
	// set BG color based on selected state   
	colorOld = SetBkColor(hdc, GetSysColor(COLOR_3DFACE));

	BitBlt(hdc, rcDest.left, rcDest.top, rcDest.right-rcDest.left, rcDest.bottom-rcDest.top, memdc, p.x, p.y, SRCCOPY);

	// restore
	SetBkColor(hdc, colorOld);
	SelectObject(memdc, hOldBM);
	DeleteDC(memdc);

}

static LRESULT Caption_NCPaint(CustomCaption *ctp, HWND hwnd, HRGN hrgn)
{
	RECT rect, rect1;
	BOOL fRegionOwner = FALSE;
	int i;
	HDC hdc;
	UINT uButType;

	int x, y;

	HRGN hrgn1;

	GetWindowRect(hwnd, &rect);

	x = rect.left;
	y = rect.top;

	//Create a region which covers the whole window. This
	//must be in screen coordinates
	if(hrgn == (HRGN)1 || hrgn == 0)
	{
		hrgn = CreateRectRgnIndirect(&rect);
		fRegionOwner = TRUE;
	}

	// Clip our custom buttons out of the way...
	for(i = 0; i < ctp->nNumButtons; i++)
	{
		//Get button rectangle in screen coords
		GetButtonRect(ctp, hwnd, i, &rect1, FALSE);

		hrgn1 = CreateRectRgnIndirect(&rect1);

		//Cut out a button-shaped hole
		CombineRgn(hrgn, hrgn, hrgn1, RGN_XOR);

		DeleteObject(hrgn1);
	}

	//
	//	Call the default window procedure with our modified window region!
	//	(REGION MUST BE IN SCREEN COORDINATES)
	//
	CallWindowProc(ctp->wpOldProc, hwnd, WM_NCPAINT, (WPARAM)hrgn, 0);
	
	//
	//	Now paint our custom buttons in the holes that are
	//	left by our clipping. All drawing in the Non-client area
	//  is window-relative (Not in screen coords)
	//

	hdc = GetWindowDC(hwnd);

	// Draw buttons in a loop
	for(i = 0; i < ctp->nNumButtons; i++)
	{
		//Get Button rect in window coords
		GetButtonRect(ctp, hwnd, i, &rect1, TRUE);
		
		if(ctp->buttons[i].hBmp)
			uButType = DFCS_BUTTONPUSH;
		else
			uButType = DFCS_BUTTONPUSH;

		if(ctp->buttons[i].fPressed)
			DrawFrameControl(hdc, &rect1, DFC_BUTTON,  uButType | DFCS_PUSHED);
			//DrawFrameControl(hdc, &rect1, DFC_CAPTION, DFCS_CAPTIONRESTORE | DFCS_PUSHED);
		else
			DrawFrameControl(hdc, &rect1, DFC_BUTTON, uButType);
			//DrawFrameControl(hdc, &rect1, DFC_CAPTION, DFCS_CAPTIONRESTORE);

		//Now draw the bitmap!
		InflateRect(&rect1, -2, -2);
		rect1.right--;
		rect1.bottom--;

		if(ctp->buttons[i].fPressed)
			OffsetRect(&rect1, 1, 1);

		CenterBitmap(hdc, &rect1, ctp->buttons[i].hBmp);
	}

	ReleaseDC(hwnd, hdc);
	
	if(fRegionOwner)
		DeleteObject(hrgn);
	
	return 0;
}

//
//	This is a generic message handler used by WM_SETTEXT and WM_NCACTIVATE.
//	It works by turning off the WS_VISIBLE style, calling
//	the original window procedure, then turning WS_VISIBLE back on.
//
//	This prevents the original wndproc from redrawing the caption.
//	Last of all, we paint the caption ourselves with the inserted buttons
//
static LRESULT Caption_Wrapper(CustomCaption *ctp, HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	UINT ret;
	DWORD dwStyle;

	dwStyle = GetWindowLong(hwnd, GWL_STYLE);

	//Turn OFF WS_VISIBLE, so that WM_NCACTIVATE does not
	//paint our window caption...
	SetWindowLong(hwnd, GWL_STYLE, dwStyle & ~WS_VISIBLE);
	
	//Do the default thing..
	ret = CallWindowProc(ctp->wpOldProc, hwnd, msg, wParam, lParam);
	
	//Restore the original style
	SetWindowLong(hwnd, GWL_STYLE, dwStyle);

	//Paint the whole window frame + caption
	Caption_NCPaint(ctp, hwnd, (HRGN)1);
	return ret;
}

static LRESULT Caption_NCActivate(CustomCaption *ctp, HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	return Caption_Wrapper(ctp, hwnd, WM_NCACTIVATE, wParam, lParam);
}

static LRESULT Caption_SetText(CustomCaption *ctp, HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	return Caption_Wrapper(ctp, hwnd, WM_SETTEXT, wParam, lParam);
}

//
//	NonClient Left Button Down.
//	Mouse is in screen coordinates
//
static LRESULT Caption_NCLButtonDown(CustomCaption *ctp, HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	int i;
	RECT rect;
	POINT pt;
	
	pt.x = (short)LOWORD(lParam);
	pt.y = (short)HIWORD(lParam);

	//If mouse has been clicked in caption
	//(Note: the NCHITTEST handler changes HTCAPTION to HTBORDER
	//       if we are over an inserted button)
	if(wParam == HTBORDER)
	{
		for(i = 0; i < ctp->nNumButtons; i++)
		{
			GetButtonRect(ctp, hwnd, i, &rect, FALSE);
			InflateRect(&rect, 0, 2);
			
			//if clicked in a custom button
			if(PtInRect(&rect, pt))
			{
				ctp->iActiveButton = i;
				ctp->buttons[i].fPressed = TRUE;
				ctp->fMouseDown = TRUE;
				
				SetCapture(hwnd);
				
				RedrawNC(hwnd);
				
				return 0;
			}
		}
	}

	return CallWindowProc(ctp->wpOldProc, hwnd, msg, wParam, lParam);
}

//
//	Left-button UP. Coords are CLIENT relative
//
static LRESULT Caption_LButtonUp(CustomCaption *ctp, HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	RECT rect;
	POINT pt;
	
	pt.x = (short)LOWORD(lParam);
	pt.y = (short)HIWORD(lParam);
	ClientToScreen(hwnd, &pt);

	if(ctp->fMouseDown)
	{
		ReleaseCapture();

		GetButtonRect(ctp, hwnd, ctp->iActiveButton, &rect, FALSE);
		InflateRect(&rect, 0, 2);

		//if clicked in a custom button
		if(PtInRect(&rect, pt))
		{
			UINT uCmd = ctp->buttons[ctp->iActiveButton].uCmd;
			SendMessage(hwnd, WM_COMMAND, uCmd, MAKELPARAM(pt.x, pt.y));
		}

		ctp->buttons[ctp->iActiveButton].fPressed = FALSE;
		ctp->fMouseDown = FALSE;

		RedrawNC(hwnd);
						
		return 0;
	}

	return CallWindowProc(ctp->wpOldProc, hwnd, WM_LBUTTONUP, wParam, lParam);
}

static LRESULT Caption_MouseMove(CustomCaption *ctp, HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	RECT rect;
	POINT pt;
	BOOL fPressed;
	
	pt.x = (short)LOWORD(lParam);
	pt.y = (short)HIWORD(lParam);
	ClientToScreen(hwnd, &pt);
	
	if(ctp->fMouseDown)
	{
		GetButtonRect(ctp, hwnd, ctp->iActiveButton, &rect, FALSE);
		InflateRect(&rect, 0, 2);
		
		fPressed = PtInRect(&rect, pt);
		
		if(fPressed != ctp->buttons[ctp->iActiveButton].fPressed)
		{
			ctp->buttons[ctp->iActiveButton].fPressed ^= 1;
			RedrawNC(hwnd);
		}
		
		return 0;
	}

	return CallWindowProc(ctp->wpOldProc, hwnd, WM_MOUSEMOVE, wParam, lParam);
}

//Replacement window procedure
static LRESULT CALLBACK NewWndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	CustomCaption *ctp = GetCustomCaption(hwnd);
	WNDPROC oldproc = ctp->wpOldProc;
	
	switch(msg)
	{
	//clean up when window is destroyed
	case WM_NCDESTROY:
		HeapFree(GetProcessHeap(), 0, ctp);
		break;

	case WM_NCHITTEST:
		return Caption_NCHitTest(ctp, hwnd, wParam, lParam);

	//These three messages all cause the caption to
	//be repainted, so we have to handle all three to properly
	//support inserted buttons
	case WM_NCACTIVATE:
		return Caption_NCActivate(ctp, hwnd, wParam, lParam);

	case WM_SETTEXT:
		return Caption_SetText(ctp, hwnd, wParam, lParam);

	case WM_NCPAINT:
		return Caption_NCPaint(ctp, hwnd, (HRGN)wParam);

	//Mouse support
	case WM_NCLBUTTONDBLCLK:
	case WM_NCLBUTTONDOWN:
		return Caption_NCLButtonDown(ctp, hwnd, msg, wParam, lParam);

	case WM_LBUTTONUP:
		return Caption_LButtonUp(ctp, hwnd, wParam, lParam);

	case WM_MOUSEMOVE:
		return Caption_MouseMove(ctp, hwnd, wParam, lParam);
	}

	//call the old window procedure
	return CallWindowProc(oldproc, hwnd, msg, wParam, lParam);
}

//
//	Insert a button into specified window's titlebar
//
BOOL WINAPI Caption_InsertButton(HWND hwnd, UINT uCmd, int nBorder, HBITMAP hBmp)
{
	CustomCaption *ctp = GetCustomCaption(hwnd);
	int idx;

	// If this window doesn't have any buttons yet,
	// then perform the subclass and allocate structures etc. 
	if(ctp == 0)
	{
		//allocate memory for our subclass information
		ctp = HeapAlloc(GetProcessHeap(), 0, sizeof(CustomCaption));

		ctp->nNumButtons  = 0;
		ctp->fMouseDown   = FALSE;

		//assign this to the window in question
		SetProp(hwnd, szPropName, (HANDLE)ctp);

		//subclass the window
		ctp->wpOldProc = (WNDPROC)SetWindowLong(hwnd, GWL_WNDPROC, (LONG)NewWndProc);
	}

	idx = ctp->nNumButtons++;

	ctp->buttons[idx].hBmp			= hBmp;
	ctp->buttons[idx].nRightBorder  = nBorder;
	ctp->buttons[idx].uCmd			= uCmd;
	ctp->buttons[idx].fPressed      = FALSE;
	
	return TRUE;
}

//
//	Remove the last inserted button from window's titlebar
//
BOOL WINAPI Caption_RemoveButton(HWND hwnd)
{
	CustomCaption *ctp = GetCustomCaption(hwnd);

	if(ctp == 0)
		return FALSE;

	if(ctp->nNumButtons > 0)
	{
		ctp->nNumButtons--;
		RedrawNC(hwnd);
	}

	return TRUE;
}