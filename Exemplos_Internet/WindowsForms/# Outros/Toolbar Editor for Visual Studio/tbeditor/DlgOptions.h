#pragma once
#include "afxwin.h"
#include "afxcmn.h"
#include "ReportCtrl\ReportCtrl.h"

// finestra di dialogo CDlgOptions

class CDlgOptions : public CDialog
{
	DECLARE_DYNAMIC(CDlgOptions)

public:
	CDlgOptions(CWnd* pParent = NULL);   // costruttore standard
	virtual ~CDlgOptions();

// Dati della finestra di dialogo
	enum { IDD = IDD_DIALOG_OPTIONS };

protected:

	CEdit		m_edit_tempf;
	CEdit		m_edit_imgLib;
	CButton		m_check_replaceof;
	CButton		m_check_autosize;
	CReportCtrl	m_list_idfile;

	void ReadFiles();
	void WriteFiles();

	virtual BOOL OnInitDialog();
	virtual void DoDataExchange(CDataExchange* pDX);    // Supporto DDX/DDV

	DECLARE_MESSAGE_MAP()

	afx_msg void OnBnClickedButtonBrwtemp();
	afx_msg void OnBnClickedButtonBrwimglib();
	afx_msg void OnBnClickedButtonApply();
	afx_msg void OnBnClickedButtonCancel();
	afx_msg void OnBnClickedButtonAddfile();
	afx_msg void OnBnClickedButtonDelfile();
public:
	
	CSpinButtonCtrl m_spin_mruSize;
	CStatic m_static_mruSize;
};
