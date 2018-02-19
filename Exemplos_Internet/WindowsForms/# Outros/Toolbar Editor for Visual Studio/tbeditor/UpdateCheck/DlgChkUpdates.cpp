// DlgChkUpdates.cpp : file di implementazione
//

#include "stdafx.h"
#include "DlgChkUpdates.h"
#include "Connection.h"

// finestra di dialogo CDlgChkUpdates

IMPLEMENT_DYNAMIC(CDlgChkUpdates, CDialog)
CDlgChkUpdates::CDlgChkUpdates(CWnd* pParent /*=NULL*/)
	: CDialog(CDlgChkUpdates::IDD, pParent)
{
	m_static_link.SetURL("http://www.p-network.net/dn_nuke/Projects/ToolbarEditorforvisualstudio/tabid/56/Default.aspx");
}

CDlgChkUpdates::~CDlgChkUpdates()
{
}

void CDlgChkUpdates::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_MSG, m_static_msg);
	DDX_Control(pDX, IDC_STATIC_LINK, m_static_link);
}


BEGIN_MESSAGE_MAP(CDlgChkUpdates, CDialog)
	ON_WM_TIMER()
	ON_BN_CLICKED(IDOK, &CDlgChkUpdates::OnBnClickedOk)
END_MESSAGE_MAP()



BOOL CDlgChkUpdates::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_static_msg.SetWindowText("Wait...");
	
	SetWindowText("Checking for updates");

	SetTimer(IDCHECKUPD,1500,NULL);

	return TRUE;  
}

void CDlgChkUpdates::SetMsg(CString strMsg,COLORREF crText,COLORREF crBkgnd)
{
	if (crText != 0)
		m_static_msg.SetTextColor(crText);

	if (crBkgnd != 0)
		m_static_msg.SetBkColor(crBkgnd);

	m_static_msg.SetWindowText(strMsg);
	m_static_msg.RedrawWindow();
}

void CDlgChkUpdates::SetUrl(CString strUrl,CString strText)
{
	
	m_static_link.SetURL(strUrl);

	if (strText.IsEmpty())
		m_static_link.SetWindowText(strUrl);
	else
    	m_static_link.SetWindowText(strText);

	m_static_link.SetAutoSize(TRUE);
	m_static_link.RedrawWindow();
}
void CDlgChkUpdates::OnTimer(UINT nIDEvent)
{
	if (nIDEvent == IDCHECKUPD)
		CheckUpdates();

	CDialog::OnTimer(nIDEvent);
}

int CDlgChkUpdates::CheckUpdates()
{
	CConnection		cns;
	CString strResponse,strHeaders;
	
	KillTimer(IDCHECKUPD);
	GetDlgItem(IDOK)->EnableWindow(TRUE);
	try
	{
		SetMsg("Connecting to server ...");
		cns.SetUrl(ID_UPDATESERVER);
		cns.Connect();

		BOOL bResult;
		bResult = cns.SendRequest(strHeaders,NULL,0);
		if (bResult)
		{
			CString tmp_somecode;
			while (cns.ReadString(tmp_somecode) != NULL) 
				strResponse=strResponse+tmp_somecode;
		}
		else
		{
			strResponse = _T("|link error|");
			SetMsg("Error connecting to server",RGB(255,0,0));
			cns.Close();
		}
	}
	catch( CInternetException * e )
	{
		cns.Close();
		strResponse = _T("|link error|");
		e->Delete();
		SetMsg("Error connecting to server",RGB(255,0,0));
		return 0;
	}
	catch(const char*)
	{
		cns.Close();
		strResponse = _T("|link error|");
		SetMsg("Error connecting to server",RGB(255,0,0));
		return 0;
	}

	if (strResponse[0] != '|')
	{
		SetMsg("Error connecting to server",RGB(255,0,0));
		return 0; //bad format
	}

	vector <CString> strUpdComp;
	strUpdComp = CStringSplit(strResponse,'|');
	if (strUpdComp.size() == 3) //|VERS|VERS_STR|DOWN_URL|
	{
		double nCurrentVersion;
		double nUpdatedVersion;

		nCurrentVersion=strtod(FILE_VERSION_STR,NULL);
		nUpdatedVersion=strtod(strUpdComp[0],NULL);

		if (nUpdatedVersion > nCurrentVersion)
		{
			CString strTMP;
			SetMsg("New version available!",RGB(70,200,70));
			strTMP.Format("%s %s","Download",strUpdComp[1]);
			SetUrl(strUpdComp[2],strTMP);

		}
		else
		{
			SetMsg("No update available yet!",RGB(255,100,0));
		}
	}
	else
	{
		SetMsg("Error connecting to server",RGB(255,0,0));
		return 0;
	}
	return 1;
}

vector <CString> CDlgChkUpdates::CStringSplit(CString string,char chs)
{
	vector <CString> resValue;
	CString tmp;
	int ind=0;	//counter

	if (string[0] == chs) {
		string=string.Right(string.GetLength()-1); 
	}
	while ((ind=string.Find(chs)) != -1)
	{
		tmp=string.Left(ind);
		resValue.push_back(tmp);
		string=string.Right(string.GetLength()-(ind+1));
	}
	return resValue;
}

BOOL CDlgChkUpdates::IsTimeToCheck(int nDays)
{
	CString strLastCheckDate;
	COleDateTime tLastCheck;
	COleDateTime tCurrTime = COleDateTime::GetCurrentTime();
	
	strLastCheckDate = AfxGetApp()->GetProfileString("Config","AutoChkDays","");
	if (strLastCheckDate.IsEmpty())
	{
		strLastCheckDate = tCurrTime.Format("%Y/%m/%d");
		AfxGetApp()->WriteProfileString("Config","AutoChkDays",strLastCheckDate);
		return TRUE;
	}
	tLastCheck.ParseDateTime(strLastCheckDate);

	COleDateTimeSpan tTimeSpan = tCurrTime - tLastCheck;
	if (tTimeSpan.GetDays() >= nDays || tTimeSpan.GetDays() < 0)
	{
		strLastCheckDate = tCurrTime.Format("%Y/%m/%d");
		AfxGetApp()->WriteProfileString("Config","AutoChkDays",strLastCheckDate);
		return TRUE;
	}
	else
	{
		return FALSE;
	}
	
	return TRUE;
}
void CDlgChkUpdates::OnBnClickedOk()
{
	// TODO: aggiungere qui il codice per la gestione della notifica del controllo.
	OnOK();
}
