#pragma once
#include "afxwin.h"


// finestra di dialogo CDlgHelp

class CDlgHelp : public CDialog
{
	DECLARE_DYNAMIC(CDlgHelp)

public:
	CDlgHelp(CWnd* pParent = NULL);   // costruttore standard
	virtual ~CDlgHelp();

// Dati della finestra di dialogo
	enum { IDD = IDD_DIALOG_HELP };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // Supporto DDX/DDV

	DECLARE_MESSAGE_MAP()
public:
	CEdit m_edit_text;
	virtual BOOL OnInitDialog();
};
