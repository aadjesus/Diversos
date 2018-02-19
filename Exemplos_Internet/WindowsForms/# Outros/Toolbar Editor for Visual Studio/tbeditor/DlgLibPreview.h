#pragma once
#include "afxcmn.h"
#include "MagDialog.h"
#include "ReportCtrl\ReportCtrl.h"
#include "afxwin.h"

// finestra di dialogo CDlgLibPreview

class CDlgLibPreview : public CMagDialog
{
	CSize		m_iSize;
	CString		m_strImgLib;
	CImageList	m_libImageList;
	CImageList	m_libImageListSmall;
	CListCtrl	m_list_libpreview;

	DECLARE_DYNAMIC(CDlgLibPreview)

public:
	CDlgLibPreview(CWnd* pParent = NULL);   // costruttore standard
	virtual ~CDlgLibPreview();

// Dati della finestra di dialogo
	enum { IDD = IDD_DIALOG_LIBPREVIEW };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // Supporto DDX/DDV

	DECLARE_MESSAGE_MAP()
private:
	void PopulatePreviewList(CString strDirectory,CSize iconSize);

public:
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedButtonRefresh();
	afx_msg void OnBnClickedCheckActsize();
	CButton m_check_actualsize;
	CButton m_check_reportview;
	CButton m_check_filterbysize;
	CComboBox m_combo_size;
	afx_msg void OnCbnSelchangeComboIconsize();
	};
