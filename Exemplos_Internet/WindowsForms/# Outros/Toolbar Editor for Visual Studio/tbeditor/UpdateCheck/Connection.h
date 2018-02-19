// VERSION 2.0b by Arf (06/06)
// http://www.ilpanda.com
// arf@ilpanda.com
// Helper class for inet connections. Still in beta stage!
// You can use it, improve it and translate it in english!
// There aren't restriction is use but please send me any improvement to this code: it will
// be really apreciated! :)

#pragma once
#include <afxinet.h>

class CConnection
{
public:
	CString m_sURLToDownload;
	CString m_sFileToDownloadInto;
	CString m_sUserName;
	CString m_sPassword;


	CConnection(void);
	~CConnection(void);
private:
	static void CALLBACK OnStatusCallBack(HINTERNET hInternet, DWORD dwContext, DWORD dwInternetStatus, 
                                        LPVOID lpvStatusInformation, DWORD dwStatusInformationLength);
public:
	BOOL Connect();
	BOOL OpenRequest(CString str_page);
	BOOL Close(void);
	BOOL SendRequest(CString strHeaders, LPVOID strFormData, DWORD lenght);
	BOOL QueryInfo(DWORD dwInfoLevel, CString& str, LPDWORD lpInfo);
	BOOL ClosepFile(void);
	BOOL ClosepConnection(void);
	BOOL ReadString(CString& str,DWORD dwBytesToRead = 1024);
	void Abort(void);

protected:
	CString       m_sError;
	CString       m_sServer; 
	CString       m_sObject; 
	CString       m_sFilename;
	CString		  m_strVerb;
	INTERNET_PORT m_nPort;
	DWORD         m_dwServiceType;
	HINTERNET     m_hInternetSession;
	HINTERNET     m_hHttpConnection;
	HINTERNET     m_hHttpFile;
	BOOL          m_bAbort;
	BOOL          m_bSafeToClose;
	DWORD		  m_dwAccessType;
public:
	BOOL SetUrl(CString sURLToDownload);
};
