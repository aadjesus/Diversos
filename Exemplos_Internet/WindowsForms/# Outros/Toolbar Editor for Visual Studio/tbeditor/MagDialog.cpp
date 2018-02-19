////////////////////////////////////////////////////////////////////////////////////////
//	CMagDialog
//
//	A "magnetic" class derived from CDialog
//  this class enable dialogs to dock side by side
//
//  This code may be used in compiled form in any way you desire. This file may be
//  redistributed unmodified by any means PROVIDING it is not sold for profit without
//  the authors written consent, and providing that this notice and the authors name 
//  is included. If the source code in  this file is used in any commercial application 
//  then acknowledgement must be made to the author of this file .
//
//  This file is provided "as is" with no expressed or implied warranty.
// 
//  written by Francesco Aruta (a.francesco@mclink.it) 
//  http://www.ilpanda.com 
// 
////////////////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "TBarGenAddin.h"
#include "MagDialog.h"

// finestra di dialogo CMagDialog

IMPLEMENT_DYNAMIC(CMagDialog, CDialog)
CMagDialog::CMagDialog(CWnd* pParent /*=NULL*/)
	: CDialog(CMagDialog::IDD, pParent)
{
	m_pMagParentDlg = NULL;
	nEdge			= DKDLG_NONE;
	m_dwMagType		= DKDLG_NONE;
	m_bDocked		= FALSE;
	m_bDisablePosFix= FALSE;
}

CMagDialog::CMagDialog(UINT nIDTemplate,CWnd* pParent)
 : CDialog(nIDTemplate, pParent)
{
	m_pMagParentDlg = NULL;
	nEdge			= DKDLG_NONE;
	m_dwMagType		= DKDLG_NONE;
	m_bDocked		= FALSE;
	m_bDisablePosFix= FALSE;
}

CMagDialog::~CMagDialog()
{
}

void CMagDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CMagDialog, CDialog)
	ON_WM_MOVING()
	ON_WM_WINDOWPOSCHANGING()
END_MESSAGE_MAP()


// gestori di messaggi CMagDialog

void CMagDialog::OnMoving(UINT fwSide, LPRECT pRect)
{
	CDialog::OnMoving(fwSide, pRect);

	CRect dlgRect;
	for (size_t nIndex=0;nIndex < m_dkDialogs.size();nIndex++)
	{
		if (m_dkDialogs[nIndex].bDocked)
		{
		
			m_dkDialogs[nIndex].pWnd->GetWindowRect(dlgRect);
			
			switch(m_dkDialogs[nIndex].nEdge) 
			{
			case DKDLG_LEFT:
				if (m_dkDialogs[nIndex].delta == 0)
					m_dkDialogs[nIndex].delta = pRect->top - dlgRect.top;
				dlgRect.MoveToXY(pRect->left-(dlgRect.right-dlgRect.left),(int)pRect->top-m_dkDialogs[nIndex].delta);
				break;
			case DKDLG_RIGHT:
				if (m_dkDialogs[nIndex].delta == 0)
					m_dkDialogs[nIndex].delta = pRect->top - dlgRect.top;
				dlgRect.MoveToXY(pRect->right,(int)pRect->top-m_dkDialogs[nIndex].delta);
				break;
			case DKDLG_TOP:
				if (m_dkDialogs[nIndex].delta == 0)
					m_dkDialogs[nIndex].delta = pRect->left - dlgRect.left;
				dlgRect.MoveToXY((int)pRect->left-m_dkDialogs[nIndex].delta,pRect->top-(dlgRect.bottom-dlgRect.top));
				break;
			case DKDLG_BOTTOM:
				if (m_dkDialogs[nIndex].delta == 0)
					m_dkDialogs[nIndex].delta = pRect->left - dlgRect.left;
				dlgRect.MoveToXY((int)pRect->left-m_dkDialogs[nIndex].delta,pRect->bottom);
				break;
			default:
				dlgRect;
			}

			m_dkDialogs[nIndex].pWnd->MoveMagDialog(dlgRect,TRUE,TRUE);
		}
	}


	// Magnetic field

	if (m_pMagParentDlg != NULL)
	{
		CPoint curPl,curPr,curPt,curPb;
		int rHight,rWidth;
		CRect rectParent,rectLeft,rectRight,rectTop,rectBottom;

		rHight	= 50;
		rWidth	= 50;

		m_pMagParentDlg->GetWindowRect(rectParent);

		// Magnetic fields!
		rectRight	= CRect(rectParent.right-rHight,rectParent.top-rWidth,
			rectParent.right+rHight,rectParent.bottom+rWidth);
		rectLeft	= CRect(rectParent.left-rHight,rectParent.top-rWidth,
			rectParent.left+rHight,rectParent.bottom+rWidth);
		rectTop		= CRect(rectParent.left-rWidth,rectParent.top-rHight,
			rectParent.right+rWidth,rectParent.top+rHight);
		rectBottom	= CRect(rectParent.left-rWidth,rectParent.bottom-rHight,
			rectParent.right+rWidth,rectParent.bottom+rHight);

		curPl		= CPoint(pRect->left,pRect->top+(pRect->bottom-pRect->top)/2);		//Left
		curPr		= CPoint(pRect->right,pRect->top+(pRect->bottom-pRect->top)/2);		//Right
		curPt		= CPoint(pRect->left+(pRect->right-pRect->left)/2,pRect->bottom);	//Top
		curPb		= CPoint(pRect->left+(pRect->right-pRect->left)/2,pRect->top);		//Bottom

		if (m_bDocked)
		{
			if ((nEdge == DKDLG_RIGHT) && 
				!rectRight.PtInRect(curPl))
			{
				m_pMagParentDlg->UnDockMagneticDialog(this);
				nEdge		= DKDLG_NONE;
				m_bDocked	= FALSE;
			}
			if ((nEdge == DKDLG_LEFT )&& 
				!rectLeft.PtInRect(curPr))
			{
				m_pMagParentDlg->UnDockMagneticDialog(this);
				nEdge		= DKDLG_NONE;
				m_bDocked	= FALSE;
			}
			if ((nEdge == DKDLG_TOP )&& 
				!rectTop.PtInRect(curPt))
			{
				m_pMagParentDlg->UnDockMagneticDialog(this);
				nEdge		= DKDLG_NONE;
				m_bDocked	= FALSE;
			}
			if ((nEdge == DKDLG_BOTTOM )&& 
				!rectBottom.PtInRect(curPb))
			{
				m_pMagParentDlg->UnDockMagneticDialog(this);
				nEdge		= DKDLG_NONE;
				m_bDocked	= FALSE;
			}

		}
		else
		{
			if ((m_dwMagType == DKDLG_RIGHT || m_dwMagType == DKDLG_ANY) && 
				rectRight.PtInRect(curPl))
			{
				m_pMagParentDlg->DockMagneticDialog(this,DKDLG_RIGHT);
				nEdge		= DKDLG_RIGHT;	
				m_bDocked	= TRUE;
			}

			if ((m_dwMagType == DKDLG_LEFT || m_dwMagType == DKDLG_ANY) && 
				rectLeft.PtInRect(curPr))
			{
				m_pMagParentDlg->DockMagneticDialog(this,DKDLG_LEFT);
				nEdge		= DKDLG_LEFT;	
				m_bDocked	= TRUE;
			}

			if ((m_dwMagType == DKDLG_TOP || m_dwMagType == DKDLG_ANY) && 
				rectTop.PtInRect(curPt))
			{
				m_pMagParentDlg->DockMagneticDialog(this,DKDLG_TOP);
				nEdge		= DKDLG_TOP;	
				m_bDocked	= TRUE;
			}
			if ((m_dwMagType == DKDLG_BOTTOM || m_dwMagType == DKDLG_ANY) && 
				rectBottom.PtInRect(curPb))
			{
				m_pMagParentDlg->DockMagneticDialog(this,DKDLG_BOTTOM);
				nEdge		= DKDLG_BOTTOM;	
				m_bDocked	= TRUE;
			}


		}

	}

}

void CMagDialog::EnableMagnetic(DWORD dwMagType,CMagDialog* pMagParentDlg)
{
	m_dwMagType		= dwMagType;
	m_pMagParentDlg	= pMagParentDlg;
}

void CMagDialog::AddMagneticDialog(CMagDialog* pDialog,BOOL bDocked,DWORD dwMagWhere)
{
	dk_window dkWnd;

	dkWnd.pWnd		= pDialog;
	dkWnd.dwMagType = DKDLG_ANY;
	dkWnd.bDocked	= bDocked;
	dkWnd.nEdge		= dwMagWhere;
	dkWnd.delta		= 0;

	m_dkDialogs.push_back(dkWnd);
}

void CMagDialog::DockMagneticDialog(CMagDialog* pDialog,DWORD nEdge)
{
	for (size_t nIndex=0;nIndex < m_dkDialogs.size();nIndex++)
	{
		if (m_dkDialogs[nIndex].pWnd == pDialog)
		{
			m_dkDialogs[nIndex].nEdge	= nEdge;
			m_dkDialogs[nIndex].bDocked = TRUE;
		}
	}
}

void CMagDialog::UnDockMagneticDialog(CMagDialog* pDialog)
{
	for (size_t nIndex=0;nIndex < m_dkDialogs.size();nIndex++)
	{
		if (m_dkDialogs[nIndex].pWnd == pDialog)
		{
			m_dkDialogs[nIndex].nEdge	= DKDLG_NONE;
			m_dkDialogs[nIndex].bDocked = FALSE;
			m_dkDialogs[nIndex].delta	= 0;
		}
	}
}


void CMagDialog::OnWindowPosChanging(WINDOWPOS* lpwndpos)
{
	if ((m_bDocked) &&					// This is a fix on movement... 
		(m_pMagParentDlg != NULL) &&	// I need it only if the windows is docked and I'm trying to move it:
		(m_bDisablePosFix == FALSE))	// when this window is dragged by parent window I don't need this fix
	{
		CRect tmpRect,rectParent;
		GetWindowRect(tmpRect);
		m_pMagParentDlg->GetWindowRect(rectParent);
		if (nEdge == DKDLG_RIGHT)
		{
			tmpRect.MoveToXY(rectParent.right,lpwndpos->y);
		}
		if (nEdge == DKDLG_LEFT )
		{
			tmpRect.MoveToXY(rectParent.left-(tmpRect.right-tmpRect.left),lpwndpos->y);
		}
		if (nEdge == DKDLG_TOP )
		{
			tmpRect.MoveToXY(lpwndpos->x,rectParent.top-(tmpRect.bottom-tmpRect.top));
		}
		if (nEdge == DKDLG_BOTTOM )
		{
			tmpRect.MoveToXY(lpwndpos->x,rectParent.bottom);
		}
				
		lpwndpos->x		= tmpRect.left;
		lpwndpos->y		= tmpRect.top;
		lpwndpos->cx	= tmpRect.Width();
		lpwndpos->cy	= tmpRect.Height();

		m_pMagParentDlg->UpdateMagPosition(this);
	}
	m_bDisablePosFix = FALSE;

	CDialog::OnWindowPosChanging(lpwndpos);

}

void CMagDialog::MoveMagDialog(LPCRECT lpRect, BOOL bRepaint, BOOL bDisablePosFix)
{
	m_bDisablePosFix = TRUE;
	MoveWindow(lpRect,bRepaint);
}

void CMagDialog::UpdateMagPosition(CMagDialog* pDialog)
{
	for (size_t nIndex=0;nIndex < m_dkDialogs.size();nIndex++)
	{
		if (m_dkDialogs[nIndex].pWnd == pDialog)
		{
			m_dkDialogs[nIndex].delta	= 0;
		}
	}
}