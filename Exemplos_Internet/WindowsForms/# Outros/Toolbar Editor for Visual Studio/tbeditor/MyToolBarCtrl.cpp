// CMyToolBarCtrl aka DragDropToolbar v 0.7
// Written by Francesco Aruta (a.francesco@mclink.it). 
// http://www.ilpanda.com (maybe there will be some page about this control in 
// the future...
// This version is still not functional at 100%
// I didn't try it but i suppose that it can't handle the push button message well
// Feel free to use this code as you prefer but please give credit (not needed but 
// really apreciated :))

// Credits:
// Thank to the guy who wrote the original DrawIndicator and DragDetectPlus routines.
// I'm sorry but I can't find his name anywhere (i found this piece of code on deja
// (http://www.deja.com) but I lost all the information about the author)

#include "stdafx.h"
#include "TBarGenAddin.h"
#include "MyToolBarCtrl.h"


// CMyToolBarCtrl

#define	INDICATOR_WIDTH			4
#define INDICATOR_COLOR			26

IMPLEMENT_DYNAMIC(CMyToolBarCtrl, CToolBarCtrl)
CMyToolBarCtrl::CMyToolBarCtrl()
{
	m_bDragging		= FALSE;
	m_InsertPosRect = CRect(0,0,0,0);
	m_nButtonFrom	= -1;
	m_nButtonTo		= -1;
}

CMyToolBarCtrl::~CMyToolBarCtrl()
{
}


BEGIN_MESSAGE_MAP(CMyToolBarCtrl, CToolBarCtrl)
	ON_WM_MOUSEMOVE()
	ON_NOTIFY_REFLECT(TBN_BEGINDRAG, OnTbnBeginDrag)
	ON_NOTIFY_REFLECT(TBN_ENDDRAG, OnTbnEndDrag)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
END_MESSAGE_MAP()



// gestori di messaggi CMyToolBarCtrl


void CMyToolBarCtrl::OnMouseMove(UINT nFlags, CPoint point)
{
	// This code added to do extra check - shouldn't be strictly necessary!
	if( !(nFlags & MK_LBUTTON) )
		m_bDragging = false;

	if( m_bDragging )
	{
		int nButtonTo = HitTest(&point);
		if (nButtonTo >=0)
			m_nButtonTo = nButtonTo;

		CPoint ptDragImage(point);
		ClientToScreen(&ptDragImage);
		GetImageList()->DragMove(ptDragImage);
		DrawIndicator(point);

	}
	m_BeginDragPoint = point; 

	CToolBarCtrl::OnMouseMove(nFlags, point);
}

void CMyToolBarCtrl::OnTbnBeginDrag(NMHDR *pNMHDR, LRESULT *pResult)
{
	pResult = 0;
}

void CMyToolBarCtrl::OnTbnEndDrag(NMHDR *pNMHDR, LRESULT *pResult)
{
	pResult = 0;
}


BOOL CMyToolBarCtrl::DragDetectPlus(CWnd* Handle, CPoint p)
{
	CRect DragRect;
	MSG Msg;
	BOOL bResult = FALSE;
	Handle->ClientToScreen(&p);
	DragRect.TopLeft() = p;
	DragRect.BottomRight() = p;
	InflateRect(DragRect, GetSystemMetrics(SM_CXDRAG), GetSystemMetrics(SM_CYDRAG));
	BOOL bDispatch = TRUE;
	Handle->SetCapture();
	while (!bResult && bDispatch)
	{
		if (PeekMessage(&Msg, *Handle, 0, 0, PM_REMOVE))
		{
			switch (Msg.message) {
				case WM_MOUSEMOVE:
					bResult = !(PtInRect(DragRect, Msg.pt));
					break;
				case WM_RBUTTONUP:
				case WM_LBUTTONUP:
				case WM_CANCELMODE:
					bDispatch = FALSE;
					break;
				case WM_QUIT:
					ReleaseCapture();
					return FALSE;
				default:
					TranslateMessage(&Msg);
					DispatchMessage(&Msg);
			}
		}
		else
			Sleep(0);
	}
	ReleaseCapture();
	return bResult;
}

void CMyToolBarCtrl::OnLButtonDown(UINT nFlags, CPoint point)
{
	CToolBarCtrl::OnLButtonDown(nFlags, point);	
	if( DragDetectPlus(this, point) ) 
	{
		m_bDragging=true;

		TBBUTTON button;
		m_nButtonTo = HitTest(&point);

		m_nButtonFrom = m_nButtonTo;
		
		GetButton(m_nButtonFrom,&button);
		if (button.fsStyle == TBSTYLE_BUTTON)
		{
			IMAGEINFO imgInfo;
			GetImageList()->GetImageInfo(button.iBitmap,&imgInfo);
			GetImageList()->BeginDrag(button.iBitmap, CPoint(imgInfo.rcImage.right,0));
			GetImageList()->DragEnter(NULL, point);
			DrawIndicator(point);
			SetCapture();
		}
		else
		{
			m_bDragging = FALSE;
		}
	}
}

void CMyToolBarCtrl::OnLButtonUp(UINT nFlags, CPoint point)
{
	CToolBarCtrl::OnLButtonUp(nFlags, point);
	
	if( m_bDragging )
	{

		if( GetCapture() == this )
			ReleaseCapture();

		
		GetImageList()->DragLeave(CWnd::GetDesktopWindow());
		GetImageList()->EndDrag();

		RedrawWindow();

		if ((m_nButtonFrom==m_nButtonTo) || 
			(m_nButtonTo == -1))
			return;

	
		CString str;
		str.Format("%d\n",m_nButtonTo);
		TRACE(str);

		MoveButton(m_nButtonFrom,m_nButtonTo);

		NMHBT nmh;
		nmh.code		= WM_BUTTONMOVED;
		nmh.hwndFrom	= GetSafeHwnd();
		nmh.idFrom		= GetDlgCtrlID();
		nmh.nButtonFrom = m_nButtonFrom;
		nmh.nButtonTo	= m_nButtonTo;

		GetParent()->SendMessage(WM_BUTTONMOVED, nmh.idFrom, (LPARAM)&nmh);
		
	}
}

BOOL CMyToolBarCtrl::DrawIndicator(CPoint point)
{

	CRect rect;

	if( GetItemRect( 0, &rect ) )
	{
		//hitinfo.pt.y = rect.top;
	}

	int nBtn = HitTest(&point);

	CRect newInsertPosRect;

	if( nBtn >= 0 ) 
	{

		BOOL bRet = GetItemRect(nBtn,&rect);

		newInsertPosRect = CRect(rect.left-1,rect.top,rect.left-1+INDICATOR_WIDTH, rect.bottom);

		if (m_nButtonFrom <= nBtn)
		{
			if( point.x >= rect.right-rect.Width()/2 ) //&& point.x < rect.right )
			{
				newInsertPosRect.MoveToX(rect.right-1);
				m_nButtonTo= nBtn;
			}
			else
				m_nButtonTo= nBtn-1;
		}
		if (m_nButtonFrom >= nBtn)
		{
			if( point.x >= rect.right-rect.Width()/2 ) //&& point.x < rect.right )
			{
				newInsertPosRect.MoveToX(rect.right-1);
				m_nButtonTo= nBtn+1;
			}
			else
				m_nButtonTo= nBtn;
		}

		if (nBtn == m_nButtonFrom)
			m_nButtonTo = nBtn;


		if( newInsertPosRect != m_InsertPosRect )
		{
			// Remove the current indicator by invalidate the rectangle (forces repaint)
			InvalidateRect(&m_InsertPosRect); //,false);

			// Update to new insert indicator position...
			m_InsertPosRect = newInsertPosRect;
		}  

		// Create a simple device context in which we initialize the pen and brush
		// that we will use for drawing the new indicator...

		CClientDC dc( this );

		CBrush brush(GetSysColor(INDICATOR_COLOR));
		CPen   pen(PS_SOLID,1,GetSysColor(INDICATOR_COLOR));

		CBrush* pOldBrush = dc.SelectObject( &brush );
		CPen* pOldPen = dc.SelectObject( &pen );

		// Draw the insert indicator
		dc.Rectangle(m_InsertPosRect);

		dc.SelectObject(pOldPen);
		dc.SelectObject(pOldBrush);

	}
	else
	{
		m_nButtonTo = -1;
		InvalidateRect(&m_InsertPosRect);
		m_InsertPosRect = CRect(0,0,0,0);
	
	}




	return true; // success
}
