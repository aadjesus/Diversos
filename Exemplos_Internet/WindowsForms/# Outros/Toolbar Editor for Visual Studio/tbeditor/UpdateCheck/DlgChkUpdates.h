#pragma once
#include "afxwin.h"
#include "..\TBarGenAddin.h"
#include "..\Label\Label.h"
#include "..\HyperLink\HyperLink.h"

// finestra di dialogo CDlgChkUpdates

class CDlgChkUpdates : public CDialog
{
private:
	enum	{	IDCHECKUPD = 1		//Check for update
			};
	vector <CString> CStringSplit(CString string,char chs);
	
	DECLARE_DYNAMIC(CDlgChkUpdates)

public:
	CDlgChkUpdates(CWnd* pParent = NULL);   // costruttore standard
	virtual ~CDlgChkUpdates();

// Dati della finestra di dialogo
	enum { IDD = IDD_DIALOG_CHECK };
	
protected:
	CLabel		m_static_msg;
	CHyperLink	m_static_link;

	virtual BOOL OnInitDialog();
	virtual void DoDataExchange(CDataExchange* pDX);    // Supporto DDX/DDV

	afx_msg void OnTimer(UINT nIDEvent);

	DECLARE_MESSAGE_MAP()

public:
	static BOOL IsTimeToCheck(int nDays);
	void SetMsg(CString strMsg,COLORREF crText=0,COLORREF crBkgnd=0);
    void SetUrl(CString strUrl,CString strText);
	int CheckUpdates();
	
	afx_msg void OnBnClickedOk();
};
