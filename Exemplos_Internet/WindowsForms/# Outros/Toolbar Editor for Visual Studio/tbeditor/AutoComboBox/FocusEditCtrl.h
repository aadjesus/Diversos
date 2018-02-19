#if !defined(AFX_FOCUSEDITCTRL_H__9CECF3E6_5C4C_11D2_90CC_00104B2C8FCC__INCLUDED_)
#define AFX_FOCUSEDITCTRL_H__9CECF3E6_5C4C_11D2_90CC_00104B2C8FCC__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
/*|*\
|*|  File:      FocusEditCtrl.h
|*|  
|*|  By:        James R. Twine, Rubin And Associates, Inc.
|*|             Copyright 1988, Rubin And Associates, Inc.
|*|  Date:      Friday, October 09, 1998
|*|             
|*|             This implementes an Edit Control that provides
|*|             a custom background color to indicate focus.
|*|             (This class uses static variables for the colors,
|*|             so all controls that uses this class will use the
|*|             same colors).
|*|				Portions of this code are derived from CDropEdit
|*|				by Chris Losinger.  (See comments below)
|*|             
|*|             These Changes May Be Freely Incorporated Into 
|*|             Projects Of Any Type Subject To The Following 
|*|             Conditions:
|*|             
|*|             o This Header Must Remain In This File, And Any
|*|               Files Derived From It
|*|             o Do Not Misrepresent The Origin Of Any Parts Of 
|*|               This Code (IOW, Do Not Claim You Wrote It)
|*|             
|*|             A "Mention In The Credits", Or Similar Acknowledgement,
|*|             Is *NOT* Required.  It Would Be Nice, Though! :)
|*|             
|*|  Revisions: 
|*|             xx/xx/99 Incorporated The Ability To "Filter" The Input.
|*|	
\*|*/
//	CDropEdit
//	Copyright 1997 Chris Losinger
//
//	This code is freely distributable and modifiable, as long as credit
//	is given to where it's due. Watch, I'll demonstrate :
//
//	shortcut expansion code modified from :
//	CShortcut, 1996 Rob Warner

/////////////////////////////////////////////////////////////////////////////
// CFocusEditCtrl window

/****************************************************************************
*                              UWM_EDIT_COMPLETE
* Inputs:
*       WPARAM: Control ID of the control whose edit completed
*    LPARAM: CWnd * of the control whose edit completed
* Result: LRESULT
*       Logically void, 0, always
* Effect: 
*       Posted/Sent to the parent of this control to indicate that the
*    edit has completed, either by the user typing <Enter> or focus leaving
****************************************************************************/

#define AU_EV_FIRST     1
#define AU_EV_SEARCH    1

#ifndef UWM_EDIT_COMPLETE_MSG
#define UWM_EDIT_COMPLETE_MSG 7485
#endif
/////////////////////////////////////////////////////////////////////////////
// CAutoEdit window
class CAutoListBox : public CListBox
{
	friend class CAutoEdit;
public:
	CAutoListBox()
	{
		m_pEdit = NULL;
	}
protected:
	//{{AFX_MSG(CAutoListBox)
	afx_msg void OnKillFocus(CWnd* pNewWnd);
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg int  OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnLButtonDblClk(UINT nFlags, CPoint point);
	//}}AFX_MSG
public:

	//{{AFX_VIRTUAL(CAutoListBox)
public:
	virtual BOOL PreTranslateMessage(MSG *pMsg);
protected:
	//}}AFX_VIRTUAL
protected:
	CEdit *m_pEdit;


	DECLARE_MESSAGE_MAP()

};

class CFocusEditCtrl : public CEdit
{
// Construction
public:
	CFocusEditCtrl();
	void	EnableFileDropping( bool bEnable = true );		// Enable Files To Be Dropped

	//
	//	The Exclude Mask Should Be A String Of Characters
	//	To PREVENT The Entry Of.
	//
	void	SetExcludeMask( LPCTSTR cpMask = NULL );		// Set/Clear The Exclude Mask

	//
	//	The Include Mask Should Be A String Of Characters
	//	To ALLOW The Entry Of.
	//
	void	SetIncludeMask( LPCTSTR cpMask = NULL );		// Set/Clear The Include Mask

	//
	//	Use This Function To Remove Invalid Characters From 
	//	A String *BEFORE* Using SetWindowText(...).
	//
	void	StripFilteredChars( LPTSTR cpBuffer );			// Remove Bad Characters From Specified String

	inline UINT GetFilterMin()
	{ return m_nFilterMin; }

	inline UINT GetTimeDelay()
	{ return m_uTimeDelay;}

	inline UINT GetMaxSizeList()
	{ return m_nMaxSizeList; }

	inline void SetFilterMin(UINT iMin)
	{ m_nFilterMin = iMin; }

	inline void SetTimeDelay(UINT uDelay)
	{ m_uTimeDelay = uDelay;}

	inline void SetMaxSizeList(UINT nSize)
	{ m_nMaxSizeList = nSize ;}

	inline void EnableAutoComplete(BOOL bAutoComplete)
	{ m_bEnableAutoComplete = bAutoComplete;}

	inline void IsParentCombobox(bool bPCmbBox)
	{ m_bPCmbBox = bPCmbBox;}

	inline void AddAutoCompleteString(CString str)
	{ m_strList.push_back(str);}

	inline void ClearAutoCompleteString()
	{ m_strList.clear();}

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFocusEditCtrl)
	public:
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CFocusEditCtrl();

	// Generated message map functions
protected:
	//{{AFX_MSG(CFocusEditCtrl)
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnDropFiles(HDROP dropInfo);
	afx_msg HBRUSH CtlColor(CDC* pDC, UINT nCtlColor);
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	afx_msg BOOL OnKillFocusReflect();
	afx_msg BOOL OnSetFocusReflect();
	//}}AFX_MSG

	afx_msg	LRESULT	OnPaste( WPARAM, LPARAM );				// Overidden Paste Handler
	afx_msg	LRESULT	OnCut( WPARAM, LPARAM );				// Overidden Cut Handler
	afx_msg	LRESULT	OnCopy( WPARAM, LPARAM );				// Overidden Copy Handler
	afx_msg	LRESULT	OnClear( WPARAM, LPARAM );				// Overidden Delete Handler
	afx_msg	LRESULT	OnUndo( WPARAM, LPARAM );				// Overidden Undo Handler
	afx_msg void OnChar(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg UINT OnGetDlgCode();
	afx_msg void OnTimer(UINT nIDEvent);
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	//	afx_msg	LRESULT	OnSelectAll( WPARAM, LPARAM );			// Overidden Select All Handler

	
	DECLARE_MESSAGE_MAP()
	
	vector <CString> m_strList;								// List of strings
	CString	m_sExcludeCharMask;								// Exclusion Character Mask
	CString	m_sIncludeCharMask;								// Inclusion Character Mask
	CBrush	m_brBGBrush;									// Background Brush
	bool	m_bExcludeMask;									// Exclude Mask Flag
	bool	m_bIncludeMask;									// Include Mask Flag
	BOOL	m_bEnableAutoComplete;							// AutoComplete feature ON/OFF
	bool	m_bPCmbBox;										//
	UINT	m_nFilterMin;										// Min of Chars to begin the Search
	UINT	m_uTimeDelay;										// Delay to Begin the Search
	UINT	m_nMaxSizeList;

	CAutoListBox m_list;									// fill  list in OnFillList

	void OnFillList(); // fill the member m_lis
	bool OnSearch(LPCTSTR szFilter);   // if don't found return LB_ERR

	private:
		CFocusEditCtrl(const CFocusEditCtrl& rSrc);
		CFocusEditCtrl&		operator = (const CFocusEditCtrl& rSrc);


protected:
	virtual void PreSubclassWindow();
public:
	afx_msg void OnKillFocus(CWnd* pNewWnd);
	afx_msg void OnMoving(UINT fwSide, LPRECT pRect);
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FOCUSEDITCTRL_H__9CECF3E6_5C4C_11D2_90CC_00104B2C8FCC__INCLUDED_)
