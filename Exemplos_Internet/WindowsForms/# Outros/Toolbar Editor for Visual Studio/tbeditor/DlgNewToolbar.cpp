// DlgNewToolbar.cpp : file di implementazione
//

#include "stdafx.h"
#include "TBarGenAddin.h"
#include "DlgNewToolbar.h"
#include ".\dlgnewtoolbar.h"
#include "TBarGenAddinDlg.h"


// finestra di dialogo CDlgNewToolbar

IMPLEMENT_DYNAMIC(CDlgNewToolbar, CDialog)
CDlgNewToolbar::CDlgNewToolbar(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgNewToolbar::IDD, pParent)
{
	m_iType = 0;
	m_iRx	= 0;
	m_iRy	= 0;
}

CDlgNewToolbar::~CDlgNewToolbar()
{
}

void CDlgNewToolbar::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_TID, m_edit_tID);
	DDX_Control(pDX, IDC_EDIT_RX, m_edit_rx);
	DDX_Control(pDX, IDC_EDIT_RY, m_edit_ry);
	DDX_Control(pDX, IDC_COMBO_VCTYPE, m_combo_vcType);
	DDX_Control(pDX, IDC_STATIC_TEXT, m_static_text);
}


BEGIN_MESSAGE_MAP(CDlgNewToolbar, CDialog)
	ON_BN_CLICKED(IDOK, OnBnClickedOk)
END_MESSAGE_MAP()


// gestori di messaggi CDlgNewToolbar

void CDlgNewToolbar::OnBnClickedOk()
{
	CString strTMP;
	
	m_edit_rx.GetWindowText(strTMP);
	m_iRx = atoi(strTMP);

	m_edit_ry.GetWindowText(strTMP);
	m_iRy = atoi(strTMP);

	m_edit_tID.GetWindowText(m_strTId);
	m_strBid = m_strTId;

	m_iType = m_combo_vcType.GetCurSel();
	
	OnOK();
}

void CDlgNewToolbar::GetNewToolbar(st_toolbar &toolbar)
{
	toolbar.id		= m_strTId;
	toolbar.rx		= m_iRx;
	toolbar.ry		= m_iRy;
	toolbar.iType	= m_iType;
}

void CDlgNewToolbar::GetNewBitmap(st_bitmap &bitmap)
{
	bitmap.id		= m_strBid;
}

BOOL CDlgNewToolbar::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_combo_vcType.SetCurSel(0);

	m_edit_rx.SetRegEx("[0-9]{1,2}");
	m_edit_ry.SetRegEx("[0-9]{1,2}");
	m_edit_tID.SetRegEx("[A-Za-z0-9_]{1,10}");

	m_edit_ry.CreateToolTip(this,"Only numbers [1-99]");
	m_edit_rx.CreateToolTip(this,"Only numbers [1-99]");
	m_edit_tID.CreateToolTip(this,"Special characters not allowed");
	
	if (m_iDocType == CTBarGenAddinDlg::ID_BMPDOC)
	{
		m_edit_tID.EnableWindow(FALSE);
		m_edit_tID.SetWindowText("IMAGELIST");
		m_combo_vcType.EnableWindow(FALSE);
		m_combo_vcType.ShowWindow(SW_HIDE);
		m_static_text.SetWindowText("Imagelist properties");
		SetWindowText("Imagelist properties");
	}
	m_edit_rx.SetWindowText("16");
	m_edit_ry.SetWindowText("16");

	
	return TRUE; 
}
