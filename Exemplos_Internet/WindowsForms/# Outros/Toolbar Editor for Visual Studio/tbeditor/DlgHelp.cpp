// DlgHelp.cpp : file di implementazione
//

#include "stdafx.h"
#include "TBarGenAddin.h"
#include "DlgHelp.h"
#include ".\dlghelp.h"

#define ID_HELP_TEXT	"Toolbar Editor accelerators list:\r\n \
F1\t\tHelp: open this window\r\n \
F5\t\tRefresh toolbar preview\r\n \
F7\t\tGenerate toolbar\r\n \
CANC\t\tDelete current selected button in buttons' list\r\n \
CTRL+G\t\tGenerate toolbar\r\n \
CTRL+S\t\tSave bitmap\r\n \
CTRL+0\t\tOpen resource file\r\n \
CTRL+N\t\tNew toolbar\r\n \
CTRL+X\t\tExit\r\n\r\n \
During drag and drop operations, hold CTRL key \r\n \
to replace image in button.\
"
// finestra di dialogo CDlgHelp

IMPLEMENT_DYNAMIC(CDlgHelp, CDialog)
CDlgHelp::CDlgHelp(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgHelp::IDD, pParent)
{
}

CDlgHelp::~CDlgHelp()
{
}

void CDlgHelp::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_TEXT, m_edit_text);
}


BEGIN_MESSAGE_MAP(CDlgHelp, CDialog)
END_MESSAGE_MAP()


// gestori di messaggi CDlgHelp

BOOL CDlgHelp::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_edit_text.SetWindowText(ID_HELP_TEXT);

	return TRUE;  
}
