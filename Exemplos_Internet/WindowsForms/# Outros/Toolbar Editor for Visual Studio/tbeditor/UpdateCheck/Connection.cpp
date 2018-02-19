// VERSION 2.0b by Arf (06/06)
// http://www.ilpanda.com
// arf@ilpanda.com
// please read the notice in connection.cpp

#include "stdafx.h"
#include "connection.h"

CConnection::CConnection(void)
{
	//dwTimeOut			=10000;
	m_hInternetSession	=NULL;
	m_hHttpConnection	=NULL;
	m_hHttpFile			=NULL;
	m_bAbort			=FALSE;
	m_strVerb			="GET";
	m_dwAccessType		=INTERNET_OPEN_TYPE_PRECONFIG;
}

CConnection::~CConnection(void)
{
	if (m_hHttpFile)
	{
		::InternetCloseHandle(m_hHttpFile);
		m_hHttpFile = NULL;
	}
	if (m_hHttpConnection)
	{
		::InternetCloseHandle(m_hHttpConnection);
		m_hHttpConnection = NULL;
	}
	if (m_hInternetSession)
	{
		::InternetCloseHandle(m_hInternetSession);
		m_hInternetSession = NULL;
	}
}

BOOL CConnection::Connect()
{


	ASSERT(m_hInternetSession == NULL);
    m_hInternetSession = ::InternetOpen(AfxGetAppName(), m_dwAccessType, NULL, NULL, 0);
	if (m_hInternetSession == NULL)
	{
		TRACE(_T("Failed in call to InternetOpen, Error:%d\n"), ::GetLastError());
		throw "GENERICERROR";
		return FALSE;
	}

  
	if (m_bAbort)
	{
		throw "ANNULLATO";
		return FALSE;
	}
	 
	if (::InternetSetStatusCallback(m_hInternetSession, OnStatusCallBack) == INTERNET_INVALID_STATUS_CALLBACK)
	{
		TRACE(_T("Failed in call to InternetSetStatusCallback, Error:%d\n"), ::GetLastError());
		throw "GENERICERROR";
		return FALSE;
	}

  //Should we exit the thread
	if (m_bAbort)
	{
		throw "ANNULLATO";
		return FALSE;
	}
    

  //Make the connection to the HTTP server          
	ASSERT(m_hHttpConnection == NULL);
	if (m_sUserName.GetLength())
		m_hHttpConnection = ::InternetConnect(m_hInternetSession, m_sServer, m_nPort, m_sUserName, 
											m_sPassword, INTERNET_SERVICE_HTTP, 0, (DWORD) this);
	else
		m_hHttpConnection = ::InternetConnect(m_hInternetSession, m_sServer, m_nPort, NULL, 
											NULL, INTERNET_SERVICE_HTTP, 0, (DWORD) this);
	if (m_hHttpConnection == NULL)
	{
		TRACE(_T("Failed in call to InternetConnect, Error:%d\n"), ::GetLastError());
		throw "FAILEDCONNECTSERVER";
		return FALSE;
	}

  //Should we exit the thread
	if (m_bAbort)
	{
		throw "ANNULLATO";
		return FALSE;
	}

 

  //Issue the request to read the file
	DWORD dwParameters;
	LPCTSTR ppszAcceptTypes[2];
	ppszAcceptTypes[0] = _T("*/*");  //We support accepting any mime file type since this is a simple download of a file
	ppszAcceptTypes[1] = NULL;
	ASSERT(m_hHttpFile == NULL); //INTERNET_FLAG_SECURE | INTERNET_FLAG_RELOAD|INTERNET_FLAG_DONT_CACHE
	if (m_nPort == (INTERNET_PORT) 443)
		dwParameters=INTERNET_FLAG_SECURE | INTERNET_FLAG_RELOAD | INTERNET_FLAG_DONT_CACHE | INTERNET_FLAG_KEEP_CONNECTION;
	else
		dwParameters= INTERNET_FLAG_RELOAD | INTERNET_FLAG_DONT_CACHE | INTERNET_FLAG_KEEP_CONNECTION;

	m_hHttpFile = HttpOpenRequest(m_hHttpConnection,m_strVerb, m_sObject, NULL, NULL, ppszAcceptTypes, dwParameters, (DWORD) this);
	if (m_hHttpFile == NULL)
	{
		TRACE(_T("Failed in call to HttpOpenRequest, Error:%d\n"), ::GetLastError());
		throw "FAILEDCONNECTSERVER";
		return FALSE;
	}

	return TRUE;
}

BOOL CConnection::Close(void)
{
	if (m_hHttpFile)
	{
		::InternetCloseHandle(m_hHttpFile);
		m_hHttpFile = NULL;
	}
	if (m_hHttpConnection)
	{
		::InternetCloseHandle(m_hHttpConnection);
		m_hHttpConnection = NULL;
	}
	if (m_hInternetSession)
	{
		::InternetCloseHandle(m_hInternetSession);
		m_hInternetSession = NULL;
	}

	return TRUE;
}

BOOL CConnection::SendRequest(CString strHeaders, LPVOID strFormData, DWORD lenght)
{
	BOOL bSend = ::HttpSendRequest(m_hHttpFile, strHeaders , strHeaders.GetLength(),strFormData, lenght);
	if (!bSend)
	{
		TRACE(_T("Failed in call to HttpSendRequest, Error:%d\n"), ::GetLastError());
		throw "FAILEDCONNECTSERVER";
		return FALSE;
	}

	return bSend;
}

BOOL CConnection::QueryInfo(DWORD dwInfoLevel, CString& str, LPDWORD lpInfo)
{
	
	TCHAR szStatusCode[32];
	DWORD dwInfoSize = 32;
	if (!HttpQueryInfo(m_hHttpFile, dwInfoLevel, szStatusCode, &dwInfoSize, NULL))
	{
		TRACE(_T("Failed in call to HttpQueryInfo for HTTP query status code, Error:%d\n"), ::GetLastError());
		throw "INVALIDSERVERRESPONSE";
		return FALSE;
	}
	
	str=szStatusCode;

	return TRUE;
}

BOOL CConnection::ClosepFile(void)
{
	/*if (pFile)
	{
		pFile->Close();
		delete pFile;
		pFile=NULL;
	}*/
	return TRUE;
}

BOOL CConnection::OpenRequest(CString str_page)
{
	
	LPCTSTR ppszAcceptTypes[2];
	ppszAcceptTypes[0] = _T("*/*");  //We support accepting any mime file type since this is a simple download of a file
	ppszAcceptTypes[1] = NULL;
	
	m_sObject=str_page;
	
	if (m_hHttpFile)
	{
		if (!::InternetCloseHandle(m_hHttpFile))
			AfxMessageBox("this shouldn't be happen...");
		m_hHttpFile = NULL;
	}
	
	DWORD dwParameters;
	if (m_nPort == (INTERNET_PORT) 443)
		dwParameters=INTERNET_FLAG_SECURE | INTERNET_FLAG_RELOAD | INTERNET_FLAG_DONT_CACHE | INTERNET_FLAG_KEEP_CONNECTION;
	else
		dwParameters= INTERNET_FLAG_RELOAD | INTERNET_FLAG_DONT_CACHE | INTERNET_FLAG_KEEP_CONNECTION;

	m_hHttpFile = HttpOpenRequest(m_hHttpConnection,"POST", m_sObject, NULL, NULL, ppszAcceptTypes,dwParameters, (DWORD) this);
	if (m_hHttpFile == NULL)
	{
		TRACE(_T("Failed in call to HttpOpenRequest, Error:%d\n"), ::GetLastError());
		throw "FAILEDCONNECTSERVER";
		return FALSE;
	}
	
	return TRUE;
}
BOOL CConnection::ClosepConnection(void)
{

	return TRUE;
}

BOOL CConnection::ReadString(CString& str,DWORD dwBytesToRead)
{
	DWORD dwBytesRead = 0;

	if (!m_hHttpFile)
		return FALSE;
	
	char* szReadBuf;
	szReadBuf= (char*) new char[dwBytesToRead];
	if (!m_bAbort)
	{
		if (!::InternetReadFile(m_hHttpFile, szReadBuf, dwBytesToRead, &dwBytesRead))
		{
			TRACE(_T("Failed in call to InternetReadFile, Error:%d\n"), ::GetLastError());
			throw "ERRORREADFILE";
			return FALSE;
		}
	}
	else
		throw "CANCELLED";

	str=szReadBuf;
	str=str.Left(dwBytesRead);
	delete []szReadBuf;
	return dwBytesRead;
}

void CConnection::Abort(void)
{
	m_bAbort=TRUE;

	if (m_hHttpFile)
	{
		::InternetCloseHandle(m_hHttpFile);
		m_hHttpFile = NULL;
	}
	if (m_hHttpConnection)
	{
		::InternetCloseHandle(m_hHttpConnection);
		m_hHttpConnection = NULL;
	}
	if (m_hInternetSession)
	{
		::InternetCloseHandle(m_hInternetSession);
		m_hInternetSession = NULL;
	}

}

BOOL CConnection::SetUrl(CString sURLToDownload)
{
	m_sURLToDownload=sURLToDownload;
	ASSERT(m_sURLToDownload.GetLength()); //Did you forget to specify the file to download
	if (!AfxParseURL(m_sURLToDownload, m_dwServiceType, m_sServer, m_sObject, m_nPort))
	{
		//Try sticking "http://" before it
		m_sURLToDownload = _T("http://") + m_sURLToDownload;
		if (!AfxParseURL(m_sURLToDownload, m_dwServiceType, m_sServer, m_sObject, m_nPort))
		{
			TRACE(_T("Failed to parse the URL: %s\n"), m_sURLToDownload);
			return FALSE;
		}
	}
	return TRUE;
}

void CALLBACK CConnection::OnStatusCallBack(HINTERNET hInternet, DWORD dwContext, DWORD dwInternetStatus, 
                                                  LPVOID lpvStatusInformation, DWORD dwStatusInformationLength)
{
  switch (dwInternetStatus)
  {
    case INTERNET_STATUS_RESOLVING_NAME:
    {
//      SetStatus(IDS_HTTPDOWNLOAD_RESOLVING_NAME, (LPCTSTR) lpvStatusInformation);
      break;
    }
    case INTERNET_STATUS_NAME_RESOLVED:
    {
 //     SetStatus(IDS_HTTPDOWNLOAD_RESOLVED_NAME, (LPCTSTR) lpvStatusInformation);
      break;
    }
    case INTERNET_STATUS_CONNECTING_TO_SERVER:
    {
   //   SetStatus(IDS_HTTPDOWNLOAD_CONNECTING, (LPCTSTR) lpvStatusInformation);
      break;
    }
    case INTERNET_STATUS_CONNECTED_TO_SERVER:
    {
     // SetStatus(IDS_HTTPDOWNLOAD_CONNECTED, (LPCTSTR) lpvStatusInformation);
      break;
    }
    case INTERNET_STATUS_REDIRECT:
    {
      //SetStatus(IDS_HTTPDOWNLOAD_REDIRECTING, (LPCTSTR) lpvStatusInformation);
      break;
    }
    default:
    {
      break;
    }
  }
}