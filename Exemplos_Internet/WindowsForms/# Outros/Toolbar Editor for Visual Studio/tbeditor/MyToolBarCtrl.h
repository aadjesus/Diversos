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

#pragma once

// CMyToolBarCtrl message map. 
//
// WM_BUTTONMOVED this message is cast to the parent window 
// when a button is moved to a new position
// SendMessage(WM_BUTTONMOVED, nmh.idFrom, (LPARAM)&nmh)
// nmh is a variable of NMHBT structure, really simple
// and (I hope ;)) self explained.

#define	WM_BUTTONMOVED	WM_USER +1

struct NMHBT  
{
	UINT nButtonFrom;
	UINT nButtonTo;
	UINT idFrom;
	UINT code;
	HWND hwndFrom;
};
class CMyToolBarCtrl : public CToolBarCtrl
{
	DECLARE_DYNAMIC(CMyToolBarCtrl)

public:
	CMyToolBarCtrl();
	virtual ~CMyToolBarCtrl();

protected:
	DECLARE_MESSAGE_MAP()

private:
	int			m_nHitTest;
	int			m_nButtonTo;
	int			m_nButtonFrom;
	CPoint		m_BeginDragPoint;
	BOOL		m_bDragging;
	CRect		m_InsertPosRect;

	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnTbnBeginDrag(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnTbnEndDrag(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);

	BOOL DrawIndicator(CPoint point);
	BOOL DragDetectPlus(CWnd* Handle, CPoint p);

public:

	
};


