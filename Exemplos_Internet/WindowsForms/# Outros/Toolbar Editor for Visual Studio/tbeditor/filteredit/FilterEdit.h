#if !defined(AFX_FILTEREDIT_H__222BE616_C621_4D59_A5F8_B69FCA4D4D50__INCLUDED_)
#define AFX_FILTEREDIT_H__222BE616_C621_4D59_A5F8_B69FCA4D4D50__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// FilterEdit.h : header file
//

#ifndef _tstoi
#ifdef _UNICODE
#define _tstoi _wtoi
#else
#define _tstof atof
#define _tstoi atoi
#endif
#endif

#ifdef FILTER_EDIT_BOOST
#include <boost/regex.hpp>
#ifdef _UNICODE
typedef boost::wregex CRegEx;
#else
typedef boost::regex CRegEx;
#endif
#else
#include "DfaRegEx.h"

typedef CDfaRegEx CRegEx;
#endif

#ifndef TTS_BALLOON
#define TTS_BALLOON 0x40
#endif

/////////////////////////////////////////////////////////////////////////////
// CFilterEdit window

class CFilterEdit : public CEdit
{
// Operations
public:
	CFilterEdit (const bool bProcessChars = true);
	CFilterEdit (const TCHAR *pszRegEx, const bool bProcessChars = true);
	CFilterEdit (CRegEx *pRegEx, const bool bProcessChars = true);
	virtual ~CFilterEdit ();

	bool SetRegEx (const TCHAR *pszRegEx);
	bool IsInputValid (const bool bSetInputValid = true);
	const CString &GetProposedText () const;
	virtual void Increment (const int iInc) {};

	void SetProcessChars (const bool bProcessChars);
	bool GetProcessChars () const;

	void EscapeString (CString &str) const;

	// Display related functions
	void SetWavyLineOnError (const bool bWavyLineOnError);
	bool GetWavyLineOnError () const;
	void SetWavyLineColour (const COLORREF crWavyLine);
	void SetBackgroundColourFocus (const COLORREF crBkFocus);
	void SetForegroundColourFocus (const COLORREF crFgFocus);
	void SetBackgroundColourOK (const COLORREF crBkOK);
	void SetForegroundColourOK (const COLORREF crFgOK);
	void SetBackgroundColourError (const COLORREF crBkError);
	void SetForegroundColourError (const COLORREF crFgError);

	// Tooltip functions
	void CreateToolTip (CWnd *pParent, const TCHAR *pszText,
		const int iIconType = 1, const TCHAR *pszTitle = _T("Data Format"));
	void SetToolTipText (const TCHAR *pszText);
	void SetToolTipTitle (const int iIconType, const TCHAR *pszTitle);
	void SetDisplayTTonNull (const bool bDisplayTTonNull);
	bool GetDisplayTTonNull () const;

protected:
	bool IsTextValid (const TCHAR *pszText);
	virtual bool SyntaxCheck (const TCHAR *pszText);
	virtual bool SemanticCheck (const TCHAR *pszText);
	bool ValidateChar (UINT nChar);
	bool Match (const TCHAR *pszText);

	bool CanDelete ();
	bool CanPaste ();
	void ResetValid ();
	void SetInputValid (const bool bInputValid);

	void DisplayToolTip (const bool bDisplay);

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFilterEdit)
	protected:
	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);
	//}}AFX_VIRTUAL

	// Generated message map functions
protected:
	//{{AFX_MSG(CFilterEdit)
	afx_msg void OnChar(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnKeyUp(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnKillFocus(CWnd* pNewWnd);
	afx_msg void OnSetFocus(CWnd* pOldWnd);
	afx_msg HBRUSH CtlColor(CDC *pDC, UINT nCtlColor);
	afx_msg void OnPaint();
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()

private:
	bool m_bProcessChars;
	bool m_bRegExOwner;
	CRegEx *m_pRegEx;

	bool m_bInputValid;
	bool m_bControlDown;
	CString m_strProposedText;

	// Display related members
	bool m_bWavyLineOnError;
	COLORREF m_crWavyLineColour;
	COLORREF m_crFgFocus;
	COLORREF m_crBkFocus;
	COLORREF m_crFgOK;
	COLORREF m_crBkOK;
	COLORREF m_crFgError;
	COLORREF m_crBkError;
	CBrush m_brFocus;
	CBrush m_brOK;
	CBrush m_brError;
	bool m_bDisplayTTonNull;
	CToolTipCtrl m_ToolTip;

	void SetDefaults ();
	void ConstructRegEx ();

	CFilterEdit (CFilterEdit const &); // No copy construction.
	CFilterEdit &operator = (CFilterEdit const &); // No assignment.
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FILTEREDIT_H__222BE616_C621_4D59_A5F8_B69FCA4D4D50__INCLUDED_)
