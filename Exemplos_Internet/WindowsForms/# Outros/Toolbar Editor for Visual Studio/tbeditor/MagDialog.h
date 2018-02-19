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
#pragma once



class CMagDialog : public CDialog
{
	struct dk_window 
	{
		CMagDialog*	pWnd;
		BOOL		bDocked;
		DWORD		nEdge;
		DWORD		dwMagType;
		int			delta;
	};

	CMagDialog*	m_pMagParentDlg;
	DWORD		m_dwMagType;
	DWORD		nEdge;
	BOOL		m_bDocked;
	BOOL		m_bDisablePosFix;

	vector <dk_window> m_dkDialogs;
	DECLARE_DYNAMIC(CMagDialog)

public:
	CMagDialog(CWnd* pParent = NULL);  
	CMagDialog(UINT nIDTemplate,CWnd* pParent = NULL);

	virtual ~CMagDialog();

	enum { IDD = IDD_MAGDIALOG };
	enum	{	DKDLG_NONE	= 0,
		DKDLG_ANY	= 1,
		DKDLG_LEFT	= 2,
		DKDLG_RIGHT	= 4,
		DKDLG_TOP	= 8,
		DKDLG_BOTTOM=16
	};

protected:
	afx_msg void OnMoving(UINT fwSide, LPRECT pRect);
	afx_msg void OnWindowPosChanging(WINDOWPOS* lpwndpos);
	virtual void DoDataExchange(CDataExchange* pDX);    // Supporto DDX/DDV

	DECLARE_MESSAGE_MAP()

private:
	void UnDockMagneticDialog(CMagDialog* pDialog);
	void DockMagneticDialog(CMagDialog* pDialog,DWORD nEdge);
	void UpdateMagPosition(CMagDialog* pDialog);
	void MoveMagDialog(LPCRECT lpRect, BOOL bRepaint, BOOL bDisablePosFix);

public:
	void EnableMagnetic(DWORD dwMagType,CMagDialog* pMagParentDlg);
	void AddMagneticDialog(CMagDialog* pDialog,BOOL bDocked = FALSE,DWORD dwMagWhere =0);
	

};
