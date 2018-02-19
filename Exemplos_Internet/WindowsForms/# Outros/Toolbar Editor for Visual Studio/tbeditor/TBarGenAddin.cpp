// TBarGenAddin.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "TBarGenAddin.h"
#include "TBarGenAddinDlg.h"
#include "DlgHelp.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CTBarGenAddinApp

BEGIN_MESSAGE_MAP(CTBarGenAddinApp, CWinApp)
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
//	ON_COMMAND(ID_ACCELERATOR_HELP, OnAcceleratorHelp)
END_MESSAGE_MAP()


// CTBarGenAddinApp construction

CTBarGenAddinApp::CTBarGenAddinApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CTBarGenAddinApp object

CTBarGenAddinApp theApp;


// CTBarGenAddinApp initialization

BOOL CTBarGenAddinApp::InitInstance()
{
	// InitCommonControls() is required on Windows XP if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	InitCommonControls();

	CWinApp::InitInstance();

	// Standard initialization
	// If you are not using these features and wish to reduce the size
	// of your final executable, you should remove from the following
	// the specific initialization routines you do not need
	// Change the registry key under which our settings are stored
	// TODO: You should modify this string to be something appropriate
	// such as the name of your company or organization
	SetRegistryKey(_T("Arf Toolbar Editor"));
	int iMruSize;
	iMruSize = AfxGetApp()->GetProfileInt("config","mrusize",4);
	LoadStdProfileSettings(iMruSize);
	EmptyTempDir();
	
	m_haccel=LoadAccelerators(AfxGetInstanceHandle(), 
        MAKEINTRESOURCE(IDR_ACCELERATORS));

	CTBarGenAddinDlg dlg;
	m_pMainWnd = &dlg;

	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}

BOOL CTBarGenAddinApp::EmptyTempDir()
{

	CString			strDirectory;
	HANDLE			hSearch; 
	WIN32_FIND_DATA FileData; 

	BOOL fFinished = FALSE;

	strDirectory = AfxGetApp()->GetProfileString("config","tempFolder","");
	if (strDirectory.IsEmpty())
		return FALSE;
	hSearch = FindFirstFile(strDirectory+"*.*", &FileData); 
	if (hSearch == INVALID_HANDLE_VALUE) 
	{ 
		return FALSE;
	} 

	while (!fFinished) 
	{ 

		if (!(FileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
		{
			//TRACE(strDirectory+FileData.cFileName+"\n");
			DeleteFile(strDirectory+FileData.cFileName);
		}
		if (!FindNextFile(hSearch, &FileData)) 
		{
			fFinished = TRUE;
		}
	} 


	FindClose(hSearch);
	return TRUE;
}
BOOL CTBarGenAddinApp::ProcessMessageFilter(int code, LPMSG lpMsg)
{
	if(m_haccel && m_pMainWnd)
    {
        if (::TranslateAccelerator(m_pMainWnd->m_hWnd, m_haccel, lpMsg)) 
            return(TRUE);
    }
	
    return CWinApp::ProcessMessageFilter(code, lpMsg);
}

CString CTBarGenAddinApp::GetMru(int n)
{
	if (m_pRecentFileList == NULL)
        return "";
	
	if (n >= m_pRecentFileList->GetSize())
		return "";

	return m_pRecentFileList->m_arrNames[n];
}

void CTBarGenAddinApp::GetMruDisplayName(CString &strName,int iMRU,LPCTSTR szCurDir,int nCurDir)
{
	strName = "";

	if (m_pRecentFileList == NULL)
		return;

	if (iMRU >= m_pRecentFileList->GetSize())
		return;

	m_pRecentFileList->GetDisplayName(strName,iMRU,szCurDir,nCurDir);
}

int CTBarGenAddinApp::GetMruSize()
{
	if (m_pRecentFileList == NULL)
		return 0;

	return m_pRecentFileList->GetSize();
}

// This function will be filled in the future...
void CTBarGenAddinApp::LogError(CString str_error, UINT error_lvl)
{
}