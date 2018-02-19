#pragma once
#include "afxwin.h"
#include "filteredit/FilterEdit.h"

// finestra di dialogo CDlgNewToolbar

class CDlgNewToolbar : public CDialog
{
	CFilterEdit	m_edit_tID;
	CFilterEdit	m_edit_rx;
	CFilterEdit	m_edit_ry;
	CComboBox	m_combo_vcType;

	UINT		m_iDocType;
	UINT		m_iType;
	int			m_iRx;
	int			m_iRy;

	CString		m_strTId;
	CString		m_strBid;
	DECLARE_DYNAMIC(CDlgNewToolbar)

public:
	CDlgNewToolbar(CWnd* pParent = NULL);   // costruttore standard
	virtual ~CDlgNewToolbar();

// Dati della finestra di dialogo
	enum { IDD = IDD_DIALOG_NEWTOOLBAR };

protected:
	virtual BOOL OnInitDialog();
	virtual void DoDataExchange(CDataExchange* pDX);    // Supporto DDX/DDV

	DECLARE_MESSAGE_MAP()


	afx_msg void OnBnClickedOk();

public:

	void GetNewToolbar(st_toolbar &toolbarr);
	void GetNewBitmap(st_bitmap &bitmap);
	void SetDocType(int iDocType){m_iDocType = iDocType;};
	

	CStatic m_static_text;
};
