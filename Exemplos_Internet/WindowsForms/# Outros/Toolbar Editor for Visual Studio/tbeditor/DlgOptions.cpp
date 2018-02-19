// DlgOptions.cpp : file di implementazione
//

#include "stdafx.h"
#include "TBarGenAddin.h"
#include "DlgOptions.h"
#include "TBarGenAddinDlg.h"
#include ".\dlgoptions.h"


// finestra di dialogo CDlgOptions

IMPLEMENT_DYNAMIC(CDlgOptions, CDialog)
CDlgOptions::CDlgOptions(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgOptions::IDD, pParent)
{
}

CDlgOptions::~CDlgOptions()
{
}

void CDlgOptions::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_CHECK_REPLACEOF, m_check_replaceof);
	DDX_Control(pDX, IDC_CHECK_AUTORESIZE, m_check_autosize);
	DDX_Control(pDX, IDC_EDIT_TEMPF, m_edit_tempf);
	DDX_Control(pDX, IDC_EDIT_IMAGE_LIB, m_edit_imgLib);
	DDX_Control(pDX, IDC_LIST_IDFILE, m_list_idfile);
	DDX_Control(pDX, IDC_SPIN_MRUSIZE, m_spin_mruSize);
	DDX_Control(pDX, IDC_STATIC_MRUSIZE, m_static_mruSize);
}


BEGIN_MESSAGE_MAP(CDlgOptions, CDialog)
	ON_BN_CLICKED(IDC_BUTTON_BRWTEMP, OnBnClickedButtonBrwtemp)
	ON_BN_CLICKED(IDC_BUTTON_BRWIMGLIB, OnBnClickedButtonBrwimglib)
	ON_BN_CLICKED(IDC_BUTTON_APPLY, OnBnClickedButtonApply)
	ON_BN_CLICKED(IDC_BUTTON_CANCEL, OnBnClickedButtonCancel)
	ON_BN_CLICKED(IDC_BUTTON_ADDFILE, OnBnClickedButtonAddfile)
	ON_BN_CLICKED(IDC_BUTTON_DELFILE, OnBnClickedButtonDelfile)
END_MESSAGE_MAP()



void CDlgOptions::OnBnClickedButtonBrwtemp()
{
	BROWSEINFO bi;	  

	char szPath[MAX_PATH];

	bi.hwndOwner=m_hWnd;	
	bi.pidlRoot=NULL;	 
	bi.pszDisplayName=NULL;
	bi.lpszTitle="Select temp folder";	 
	bi.ulFlags=BIF_RETURNONLYFSDIRS;	
	bi.lpfn=NULL;
	bi.lParam=NULL;    
	LPITEMIDLIST pidl=SHBrowseForFolder(&bi);
	if (pidl != NULL)	 
	{
		SHGetPathFromIDList(pidl,szPath);	   
		LPMALLOC pMalloc;
		SHGetMalloc(&pMalloc);		
		pMalloc->Free(pidl);
		int nIndexOfLastChar=(int)strlen(szPath)-1;
		if(szPath[nIndexOfLastChar] == '\\')	  
		{
			szPath[nIndexOfLastChar]='\0';		
		}
		strcat(szPath,"\\");
		m_edit_tempf.SetWindowText(szPath);
	}
}

void CDlgOptions::OnBnClickedButtonBrwimglib()
{
	BROWSEINFO bi;	  

	char szPath[MAX_PATH];

	bi.hwndOwner=m_hWnd;	
	bi.pidlRoot=NULL;	 
	bi.pszDisplayName=NULL;
	bi.lpszTitle="Select image library folder";	 
	bi.ulFlags=BIF_RETURNONLYFSDIRS;	
	bi.lpfn=NULL;
	bi.lParam=NULL;    
	LPITEMIDLIST pidl=SHBrowseForFolder(&bi);
	if (pidl != NULL)	 
	{
		SHGetPathFromIDList(pidl,szPath);	   
		LPMALLOC pMalloc;
		SHGetMalloc(&pMalloc);		
		pMalloc->Free(pidl);
		int nIndexOfLastChar=(int)strlen(szPath)-1;
		if(szPath[nIndexOfLastChar] == '\\')	  
		{
			szPath[nIndexOfLastChar]='\0';		
		}
		strcat(szPath,"\\");
		m_edit_imgLib.SetWindowText(szPath);
	}
}

void CDlgOptions::OnBnClickedButtonApply()
{
	CString strTempFolder,strImgLib;
	int iMruSize;

	m_edit_tempf.GetWindowText(strTempFolder);
	m_edit_imgLib.GetWindowText(strImgLib);

	if (strImgLib[strImgLib.GetLength()-1] != '\\')
		strImgLib+="\\";

	if (strTempFolder[strTempFolder.GetLength()-1] != '\\')
		strTempFolder+="\\";

	iMruSize = m_spin_mruSize.GetPos();

	AfxGetApp()->WriteProfileString("config","tempFolder",strTempFolder);
	AfxGetApp()->WriteProfileString("config","imgLib",strImgLib);
	AfxGetApp()->WriteProfileInt("config","autosize",m_check_autosize.GetCheck());
	AfxGetApp()->WriteProfileInt("config","replaceof",m_check_replaceof.GetCheck());
	AfxGetApp()->WriteProfileInt("config","mrusize",iMruSize);
	AfxGetApp()->WriteProfileInt("config","version",ID_PVERSION);

	CTBarGenAddinDlg* dlg;
	dlg = (CTBarGenAddinDlg*) GetParent();

	dlg->SetAutoResize(m_check_autosize.GetCheck());
	dlg->SetReplaceOF(m_check_replaceof.GetCheck());
	dlg->SetTempFolder(strTempFolder);
	dlg->SetImgLib(strImgLib);

	WriteFiles();

	EndDialog(IDOK);
}

BOOL CDlgOptions::OnInitDialog()
{
	CDialog::OnInitDialog();

	int iMruSize = 0;
	char buffer[_MAX_PATH];

	SHGetSpecialFolderPath(
		0,       // Hwnd
		buffer, // String buffer.
		CSIDL_APPDATA, // CSLID of folder
		FALSE );

	CPath path;
	path.SetPath(buffer,TRUE);
	path.SetPath(path.GetLocation()+_TBEDITORFLDNAME,TRUE);
	m_edit_tempf.SetWindowText(
		AfxGetApp()->GetProfileString("config","tempFolder",path.GetLocation()+"temp"));
	
	m_edit_imgLib.SetWindowText(
		AfxGetApp()->GetProfileString("config","imgLib",path.GetLocation()+"imgLib"));

	m_check_autosize.SetCheck(
		AfxGetApp()->GetProfileInt("config","autosize",0));
	
	m_check_replaceof.SetCheck(
		AfxGetApp()->GetProfileInt("config","replaceof",1));

	if (AfxGetApp()->GetProfileInt("config","version",0) < ID_PVERSION)
	{
		GetDlgItem(IDC_BUTTON_CANCEL)->EnableWindow(FALSE);
	}

	iMruSize = AfxGetApp()->GetProfileInt("config","mrusize",4);

	m_spin_mruSize.SetBuddy(&m_static_mruSize);
	m_spin_mruSize.SetPos(iMruSize);
	m_spin_mruSize.SetRange(0,ID_MAX_MRU);
	
	CString strHead;
	CRect	rect;
	int		col1;

	m_list_idfile.GetWindowRect(rect);
	col1 = rect.Width()- 4;
	strHead.Format("file,%d;",(int)col1);
	m_list_idfile.SetColumnHeader(strHead);

	ReadFiles();
	return TRUE;  
}

void CDlgOptions::OnBnClickedButtonCancel()
{
	EndDialog(IDCANCEL);
}

void CDlgOptions::OnBnClickedButtonAddfile()
{
	CString strFilters = "Header File (*.h)|*.h|All Files (*.*)|*.*||";

	CFileDialog fileDlg(TRUE, "Header File", "*.h",
		OFN_HIDEREADONLY, strFilters, this);


	if( fileDlg.DoModal ()==IDOK )
	{

		CString fileName = fileDlg.GetPathName();
		m_list_idfile.InsertItem(m_list_idfile.GetItemCount(),fileName);

	}
}

void CDlgOptions::OnBnClickedButtonDelfile()
{
	m_list_idfile.DeleteItem(m_list_idfile.GetCurSel(),TRUE);
}

void CDlgOptions::ReadFiles()
{
	CStdioFile fin;
	CString strLine;
	char buffer[_MAX_PATH];

	SHGetSpecialFolderPath(
		0,       // Hwnd
		buffer, // String buffer.
		CSIDL_APPDATA, // CSLID of folder
		FALSE );

	CPath path;
	path.SetPath(buffer,TRUE);
	path.SetPath(path.GetLocation()+_TBEDITORFLDNAME,TRUE);

	try
	{
		if (!fin.Open(path.GetLocation()+"idfiles.txt",CFile::modeRead | CFile::typeText))
		{
			m_list_idfile.InsertItem(m_list_idfile.GetItemCount(),path.GetLocation()+"afxres.h");
			return ;
		}

		while(fin.ReadString(strLine))
		{
			m_list_idfile.InsertItem(m_list_idfile.GetItemCount(),strLine);
		}
		fin.Close();
	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();
		return ;
	}
	return ;
}

void CDlgOptions::WriteFiles()
{
	CStdioFile fout;
	CString strLine;
	char buffer[_MAX_PATH];

	SHGetSpecialFolderPath(
		0,       // Hwnd
		buffer, // String buffer.
		CSIDL_APPDATA, // CSLID of folder
		FALSE );

	CPath path;
	path.SetPath(buffer,TRUE);
	path.SetPath(path.GetLocation()+_TBEDITORFLDNAME,TRUE);
	try
	{
		if (!fout.Open(path.GetLocation()+"idfiles.txt",CFile::modeCreate| CFile::modeReadWrite | CFile::typeText))
		{
			return ;
		}

		for (int i=0;i<m_list_idfile.GetItemCount();i++)
		{
			strLine = m_list_idfile.GetItemText(i,0);
			fout.WriteString(strLine+"\n");
		}
		fout.Close();
	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();
		return ;
	}
	return ;
}