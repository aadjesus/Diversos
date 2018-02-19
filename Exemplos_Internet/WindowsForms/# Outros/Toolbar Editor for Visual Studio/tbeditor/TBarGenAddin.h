// TBarGenAddin.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols



#define ID_UPDATESERVER				"http://www.ilpanda.com/tbeditor/update.ini"
#define	FILE_VERSION_STR			"1.500"
#define ID_PVERSION					1500		// version
#define ID_MAX_MRU					10			// max mru
#define _TBEDITORFLDNAME			"tbeditor"
// CTBarGenAddinApp:
// See TBarGenAddin.cpp for the implementation of this class
//

class CTBarGenAddinApp : public CWinApp
{
private:
	HACCEL		m_haccel;
public:
	enum UINT {
		ERROR_LOW	= 1,
		ERROR_MID	= 2,
		ERROR_HIGH	= 3,
		ERROR_DEBUG	= 5
	};

	CTBarGenAddinApp();

// Overrides
	public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()


	BOOL EmptyTempDir();
public:

	CString GetMru(int n);
	void LogError(CString str_error, UINT error_lvl);
	void GetMruDisplayName(CString &strName,int iMRU, LPCTSTR szCurDir,int nCurDir);
	int GetMruSize();


private:
	virtual BOOL ProcessMessageFilter(int code, LPMSG lpMsg);
};

extern CTBarGenAddinApp theApp;