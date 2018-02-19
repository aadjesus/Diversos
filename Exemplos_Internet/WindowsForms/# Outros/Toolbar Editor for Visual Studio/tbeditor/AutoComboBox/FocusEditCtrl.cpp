/*|*\
|*|  File:      FocusEditCtrl.h
|*|  
|*|  By:        James R. Twine, Rubin And Associates, Inc.
|*|             Copyright 1988, Rubin And Associates, Inc.
|*|  Date:      Friday, October 09, 1998
|*|             
|*|             This implementes an Edit Control that provides
|*|             a custom background color to indicate focus.
|*|             (This class uses static variables for the colors,
|*|             so all controls that uses this class will use the
|*|             same colors).
|*|				Portions of this code are derived from CDropEdit
|*|				by Chris Losinger.  (See comments below)
|*|             
|*|             These Changes May Be Freely Incorporated Into 
|*|             Projects Of Any Type Subject To The Following 
|*|             Conditions:
|*|             
|*|             o This Header Must Remain In This File, And Any
|*|               Files Derived From It
|*|             o Do Not Misrepresent The Origin Of Any Parts Of 
|*|               This Code (IOW, Do Not Claim You Wrote It)
|*|             
|*|             A "Mention In The Credits", Or Similar Acknowledgement,
|*|             Is *NOT* Required.  It Would Be Nice, Though! :)
|*|             
|*|  Revisions: 
|*|             xx/xx/99 Incorporated The Ability To "Filter" The Input.
|*|	
\*|*/
//	CDropEdit
//	Copyright 1997 Chris Losinger
//
//	This code is freely distributable and modifiable, as long as credit
//	is given to where it's due. Watch, I'll demonstrate :
//
//	shortcut expansion code modified from :
//	CShortcut, 1996 Rob Warner

#include	"StdAfx.h"
#include	"FocusEditCtrl.h"
#include ".\focuseditctrl.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

static	COLORREF	g_crATColor = RGB( 0x00, 0x00, 0x00 );	// Active (Focused) Text Color
static	COLORREF	g_crABColor = RGB( 0xFF, 0xFF, 0x00 );	// Active (Focused) Background Color

#define AU_MINFILTER_DEFAULT    2   //number of chars to begin the search
#define AU_MAXSIZELIST_DEFAULT  15  // max size of list-box
#define AU_DELAYTIME_DEFAULT    320 // miliseconds
/////////////////////////////////////////////////////////////////////////////
// CFocusEditCtrl

CFocusEditCtrl::CFocusEditCtrl()	:
	m_bExcludeMask( false ),
	m_bIncludeMask( false ),
	m_bEnableAutoComplete ( false)
{
	m_brBGBrush.CreateSolidBrush( g_crABColor );			// Create Active BG Brush
	
	m_nFilterMin	= AU_MINFILTER_DEFAULT;
	m_nMaxSizeList	= AU_MAXSIZELIST_DEFAULT;
	m_uTimeDelay	= AU_DELAYTIME_DEFAULT;
	
	m_bPCmbBox		= FALSE;
	return;													// Done!
}


CFocusEditCtrl::~CFocusEditCtrl()
{
	if(::IsWindow(m_list))
		m_list.DestroyWindow();
}


void	CFocusEditCtrl::EnableFileDropping( bool bEnable )
{
	DragAcceptFiles( bEnable ? TRUE : FALSE );				// Set DragAcceptFiles Flag

	return;													// Done!
}


BEGIN_MESSAGE_MAP(CFocusEditCtrl, CEdit)
	//{{AFX_MSG_MAP(CFocusEditCtrl)
	ON_WM_CREATE()
	ON_WM_DROPFILES()
	ON_WM_CTLCOLOR_REFLECT()
	ON_WM_ERASEBKGND()
	ON_WM_TIMER()
	ON_CONTROL_REFLECT_EX(EN_KILLFOCUS, OnKillFocusReflect)
	ON_CONTROL_REFLECT_EX(EN_SETFOCUS, OnSetFocusReflect)
	ON_MESSAGE( WM_CUT, OnCut )
	ON_MESSAGE( WM_COPY, OnCopy )
	ON_MESSAGE( WM_PASTE, OnPaste )
	ON_MESSAGE( WM_CLEAR, OnClear )
	ON_MESSAGE( WM_UNDO, OnUndo )
//	ON_MESSAGE( WM_SELECTALL, OnSelectAll )
	//}}AFX_MSG_MAP
	ON_WM_CHAR()
	ON_WM_GETDLGCODE()
	ON_WM_KEYDOWN()
	ON_WM_KILLFOCUS()
	ON_WM_MOVING()
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFocusEditCtrl message handlers

HBRUSH CFocusEditCtrl::CtlColor(CDC* pDC, UINT /*nCtlColor*/ ) 
{
	DWORD	dwStyle = GetStyle();

	if( ( GetFocus() == this ) && ( !( dwStyle &			// If We Have The Focus
			WS_DISABLED ) ) && ( !( dwStyle & 				// And Control Is Not Disabled
			ES_READONLY ) ) )								// And Control Is Not ReadOnly
	{
		pDC -> SetTextColor( g_crATColor );					// Set Text Color
		pDC -> SetBkColor( g_crABColor );					// Set Background Color

		return( (HBRUSH)m_brBGBrush );						// Return Custom BG Brush
	}
	return( NULL );											// Do Default
}


BOOL CFocusEditCtrl::OnEraseBkgnd(CDC* pDC) 
{
	DWORD	dwStyle = GetStyle();
	CRect	rClient;
	BOOL	bStatus = TRUE;

	if( ( GetFocus() == this ) && ( !( dwStyle &			// If We Have The Focus
			WS_DISABLED ) ) && ( !( dwStyle & 				// And Control Is Not Disabled
			ES_READONLY ) ) )								// And Control Is Not ReadOnly
	{
		GetClientRect( &rClient );							// Get Our Area
		pDC -> FillSolidRect( rClient, g_crABColor );		// Repaint Background
	}
	else
	{
		bStatus = CEdit::OnEraseBkgnd( pDC );				// Do Default
	}
	return( bStatus );										// Return Status
}


BOOL	CFocusEditCtrl::OnKillFocusReflect( void ) 
{
	Invalidate();											// Cause Background To Repaint

	return( FALSE );											// Pass On To Parent
}


BOOL	CFocusEditCtrl::OnSetFocusReflect() 
{
	Invalidate();											// Cause Background To Repaint

	return( FALSE );											// Pass On To Parent
}


int		CFocusEditCtrl::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
	if (CEdit::OnCreate(lpCreateStruct) == -1)
		return -1;
	
	DragAcceptFiles(TRUE);
	
	return 0;
}		   


void	CFocusEditCtrl::SetExcludeMask( LPCTSTR cpMask )
{
	if( cpMask )											// If Mask Specified
	{
		//
		//	If This ASSERT Fires, You Need To Unset The Include
		//	Mask Before Setting The Exclude Mask.  Use 
		//	SetIncludeMask( NULL ) To Unset The Include Mask.
		//
		_ASSERTE( !m_bIncludeMask );						// (In)Sanity Check
		m_sExcludeCharMask = cpMask;						// Set It
		m_bExcludeMask = true;								// Set Exclude Mask
	}
	else													// If Mask Not Specified
	{
		m_sExcludeCharMask = _T("");						// Clear It
		m_bExcludeMask = false;								// Unset Exclude Mask
	}

	if(!m_list.m_hWnd)
		VERIFY(m_list.CreateEx(WS_EX_CONTROLPARENT|WS_EX_TOOLWINDOW|WS_EX_WINDOWEDGE|WS_EX_TOPMOST,"ListBox","",LBS_NOTIFY|WS_POPUP|WS_BORDER|WS_VISIBLE|WS_VSCROLL,CRect(0,0,0,0),GetParent(),0,this));
	else
		m_list.ResetContent();

	m_list.ShowWindow(SW_HIDE);

	m_list.SetFont(GetFont());//corrige a fonte do list-box

	return;													// Done!
}


// CAutoListBox
BEGIN_MESSAGE_MAP(CAutoListBox, CListBox)
	//{{AFX_MSG_MAP(CAutoListBox)
	ON_WM_KILLFOCUS()
	ON_WM_KEYDOWN()
	ON_WM_LBUTTONDOWN()
	ON_WM_CREATE()
	ON_WM_LBUTTONDBLCLK()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

BOOL CAutoListBox::PreTranslateMessage(MSG* pMsg)
{
	if(pMsg->message == WM_KEYDOWN)
		if(pMsg->wParam == VK_RETURN ||
			pMsg->wParam == VK_TAB    ||
			pMsg->wParam == VK_ESCAPE )
		{
			m_pEdit->SetFocus();
			return TRUE;
		}
		return CListBox::PreTranslateMessage(pMsg);
}

void CAutoListBox::OnKillFocus(CWnd* pNewWnd) 
{
	if(pNewWnd == this)
		return;

	CListBox::OnKillFocus(pNewWnd);
	ShowWindow(SW_HIDE);
	ResetContent();
}

void CAutoListBox::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags) 
{
	CListBox::OnKeyDown(nChar, nRepCnt, nFlags);

	int nPos = GetCurSel();

	if(nPos == LB_ERR)
		return;

	CString strCidSel;

	GetText(nPos,strCidSel);
	m_pEdit->SetWindowText(strCidSel);

}

void CAutoListBox::OnLButtonDown(UINT nFlags, CPoint point) 
{
	CListBox::OnLButtonDown(nFlags, point);


	int nPos = GetCurSel();
	if(nPos == LB_ERR)
		return;
	CString strCidSel;

	GetText(nPos,strCidSel);
	m_pEdit->SetWindowText(strCidSel);

}



int CAutoListBox::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
	if (CListBox::OnCreate(lpCreateStruct) == -1)
		return -1;

	m_pEdit = (CEdit*)lpCreateStruct->lpCreateParams;	
	return 0;
}

void CAutoListBox::OnLButtonDblClk(UINT nFlags, CPoint point) 
{
	CListBox::OnLButtonDblClk(nFlags, point);
	ShowWindow(SW_HIDE);
	ResetContent();
}

void	CFocusEditCtrl::SetIncludeMask( LPCTSTR cpMask )
{
	if( cpMask )											// If Mask Specified
	{
		//
		//	If This ASSERT Fires, You Need To Unset The Exclude
		//	Mask Before Setting The Include Mask.  Use 
		//	SetExcludeMask( NULL ) To Unset The Exclude Mask.
		//
		_ASSERTE( !m_bExcludeMask );						// (In)Sanity Check
		m_sIncludeCharMask = cpMask;						// Set It
		m_bIncludeMask = true;								// Set Include Mask
	}
	else													// If Mask Not Specified
	{
		m_sIncludeCharMask = _T("");						// Clear It
		m_bIncludeMask = false;								// Unset Include Mask
	}
	return;													// Done!
}


BOOL	CFocusEditCtrl::PreTranslateMessage(MSG* pMsg) 
{
	if( ( pMsg -> message ) == WM_CHAR )					// If A Char Message
	{
		TCHAR	cKey = (TCHAR)( pMsg -> wParam );			// Get Key (Character In Question)
		
		//
		//	If A Paste Operation...
		//
//		if( ( ( cKey == VK_INSERT ) && ( ::GetAsyncKeyState(
		if( ( ( cKey == VK_INSERT ) && ( ::GetKeyState(
				VK_SHIFT ) & 0x8000 ) ) || ( ( cKey == 
//				'v' ) && ( ::GetAsyncKeyState( 
				'v' ) && ( ::GetKeyState( 
				VK_CONTROL ) & 0x8000 ) ) )					// If A Paste Operation
		{
			Paste();										// Call Paste Handler
			return( TRUE );									// Handled This Message (Eat Keystroke)
		}
		if( ( m_bIncludeMask ) &&							// If Using An Include Mask
				( !m_sIncludeCharMask.IsEmpty() ) )			// And Including Certain Characters
		{
			if( ( cKey != VK_BACK ) && ( !::_tcschr(		// If Not A Backspace And
					m_sIncludeCharMask, cKey ) ) )			// If Key Is Not In The Include Mask
			{										 
				return( TRUE );								// Handled This Message (Eat Keystroke)
			}
		}
		else if( ( m_bExcludeMask ) &&						// If Using An Exclude Mask
				( !m_sExcludeCharMask.IsEmpty() ) )			// And Excluding Certain Characters
		{
			if( ::_tcschr( m_sExcludeCharMask, cKey ) )		// If Key Is In The Exclude Mask
			{										 
				return( TRUE );								// Handled This Message (Eat Keystroke)
			}
		}
	}
	return( CEdit::PreTranslateMessage( pMsg ) );			// Do Default If Not Handled
}


void	CFocusEditCtrl::StripFilteredChars( LPTSTR cpBuffer )
{
	int		iChars = ::_tcslen( cpBuffer );
	int		iSizeofChar = sizeof( TCHAR );

	if( ( m_bIncludeMask ) &&								// If Using An Include Mask
			( !m_sIncludeCharMask.IsEmpty() ) )				// And Including Certain Characters
	{
		TCHAR	*pWhere = NULL;

		pWhere = ::_tcsspnp( cpBuffer, m_sIncludeCharMask );// Look For Characters Not In Include Mask
		while( pWhere )										// While "Bad" Characters Found
		{
			int		iLeft = ( ( ( iChars - 1 ) * 
							iSizeofChar ) - ( pWhere - 
							cpBuffer ) );					// Calculate The Amount Of Memory To Move

			::memmove( pWhere, ( pWhere + 1 ), iLeft );		// Shift Memory Over
			pWhere = ::_tcsspnp( cpBuffer, 
					m_sIncludeCharMask );					// Look For Characters Not In Include Mask
		}
	}
	else if( ( m_bExcludeMask ) &&							// If Using An Exclude Mask
			( !m_sExcludeCharMask.IsEmpty() ) )				// And Excluding Certain Characters
	{
		TCHAR	*pWhere = NULL;

		pWhere = ::_tcspbrk( cpBuffer, m_sExcludeCharMask );// Look For A "Bad" Character
		while( pWhere )										// While "Bad" Characters Found
		{
			int		iLeft = ( ( ( iChars - 1 ) * 
							iSizeofChar ) - ( pWhere - 
							cpBuffer ) );					// Calculate The Amount Of Memory To Move

			::memmove( pWhere, ( pWhere + 1 ), iLeft );		// Shift Memory Over
			pWhere = ::_tcspbrk( cpBuffer, 
					m_sExcludeCharMask );					// Find Next "Bad" Character
		}
	}
	return;													// Done!
}


LRESULT CFocusEditCtrl::OnPaste( WPARAM, LPARAM )
{
	if( OpenClipboard() )									// Open The Clipboard
	{
#if defined( _UNICODE )
		HANDLE	hClipData = ::GetClipboardData( 
						CF_UNICODETEXT );					// Get Unicode Text Data From Clipboard
#else
		HANDLE	hClipData = ::GetClipboardData( CF_TEXT );	// Get ANSI Text Data From Clipboard
#endif
		if( hClipData )										// If Obtained
		{
			LPCTSTR cpText = (LPCTSTR)::GlobalLock( 
							hClipData );					// Lock Memory Into String Pointer
			if( cpText )									// If Pointer To Text Obtained
			{
				TCHAR	*cpBuffer = new TCHAR[ ::GlobalSize( 
								hClipData ) ];				// Allocate Buffer

				if( cpBuffer )								// If Buffer Allocated
				{
					::_tcscpy( cpBuffer, cpText );			// Copy Over The Text
					StripFilteredChars( cpBuffer );			// Remove Any Bad Characters
					ReplaceSel( cpBuffer, TRUE );			// Replace Selection
					
					delete [] cpBuffer;						// Free Buffer
				}
				::GlobalUnlock( (LPVOID)cpText );			// Release Memory Pointer
 			}
		}
		::CloseClipboard();									// Close The Clipboard
	}
	else
	{
		::MessageBeep( MB_ICONEXCLAMATION );				// Beep At User
		TRACE( _T( "FocusEditCtrl: Warning!  Failed To Open The Clipboard For A Paste Operation!\n" ) );
	}
	return( 0 );											// Done!
}


LRESULT CFocusEditCtrl::OnCut( WPARAM, LPARAM )
{
	return( Default() );									// Do Default Behaviour
}


LRESULT CFocusEditCtrl::OnCopy( WPARAM, LPARAM )
{
	return( Default() );									// Do Default Behaviour
}


LRESULT CFocusEditCtrl::OnClear( WPARAM, LPARAM )
{
	return( Default() );									// Do Default Behaviour
}


LRESULT CFocusEditCtrl::OnUndo( WPARAM, LPARAM )
{
	return( Default() );									// Do Default Behaviour
}



/*|*\
|*|  JRT:
|*|  Original code was part of CDropEdit 
|*|  Copyright 1997 Chris Losinger
\*|*/
void	CFocusEditCtrl::OnDropFiles( HDROP hdDropInfo )
{
	UINT		uiFiles = 0;
	TCHAR		*pBuffer = new TCHAR[ _MAX_PATH ];			// Allocate Buffer

	if( ( !pBuffer ) || ( !hdDropInfo ) )					// If No Drop Info Or Buffer
	{
		if( pBuffer )										// If Buffer Was Allocated
		{
			delete [] pBuffer;								// Free Buffer
		}
		return;												// Stop Here
	}				 
	uiFiles = ::DragQueryFile( hdDropInfo, (UINT)-1, NULL,
			0 );											// Get Files Dropped
	//
	//	Note!  Only Interested In The First File!
	//
	if( uiFiles )											// If One Or More Files Dropped
	{													
		::memset( pBuffer, 0, ( sizeof( TCHAR ) * 
				_MAX_PATH ) );								// Init Buffer
		::DragQueryFile( hdDropInfo, 0, pBuffer, 
				_MAX_PATH );								// Get File Path
	}
	::DragFinish( hdDropInfo );								// Done With Drop Info

/*
	CString expandedFile = ExpandShortcut(firstFile);

	// if that worked, we should have a real file name
	if (expandedFile!="") 
		firstFile=expandedFile;
	struct _stat buf;
	// get some info about that file
	int result = _stat( firstFile, &buf );
	if( result == 0 ) {

		// verify that we have a dir (if we want dirs)
		if ((buf.st_mode & _S_IFDIR) == _S_IFDIR) {
			if (m_bUseDir)
				SetWindowText(firstFile);

		// verify that we have a file (if we want files)
		} else if ((buf.st_mode & _S_IFREG) == _S_IFREG) {
			if (!m_bUseDir)
				SetWindowText(firstFile);
		}
	}
*/
	StripFilteredChars( pBuffer );							// Strip The Filtered Characters
	ReplaceSel( pBuffer, TRUE );							// Replace Selection With FilePath Text
	
	delete [] pBuffer;										// Free Buffer

	return;													// Done!
}


/*|*\
|*|  JRT:
|*|  original code was part of CShortcut 
|*|  1996 by Rob Warner
|*|  rhwarner@southeast.net
|*|  http://users.southeast.net/~rhwarner
\*|*/
/*
CString CFocusEditCtrl::ExpandShortcut(CString &inFile)
{
	CString outFile = "";

    // Make sure we have a path
    ASSERT(inFile != _T(""));

    IShellLink* psl;
    HRESULT hres;
    LPTSTR lpsz = inFile.GetBuffer(MAX_PATH);

    // Create instance for shell link
    hres = ::CoCreateInstance(CLSID_ShellLink, NULL, CLSCTX_INPROC_SERVER,
        IID_IShellLink, (LPVOID*) &psl);
    if (SUCCEEDED(hres))
    {
        // Get a pointer to the persist file interface
        IPersistFile* ppf;
        hres = psl->QueryInterface(IID_IPersistFile, (LPVOID*) &ppf);
        if (SUCCEEDED(hres))
        {
            // Make sure it's ANSI
            WORD wsz[MAX_PATH];
            ::MultiByteToWideChar(CP_ACP, 0, lpsz, -1, wsz, MAX_PATH);

            // Load shortcut
            hres = ppf->Load(wsz, STGM_READ);
            if (SUCCEEDED(hres)) {
				WIN32_FIND_DATA wfd;
				// find the path from that
				HRESULT hres = psl->GetPath(outFile.GetBuffer(MAX_PATH), 
								MAX_PATH,
								&wfd, 
								SLGP_UNCPRIORITY);

				outFile.ReleaseBuffer();
            }
            ppf->Release();
        }
        psl->Release();
    }

	inFile.ReleaseBuffer();

	// if this fails, outFile == ""
    return outFile;
}
*/

#pragma warning (disable : 4018)	// '<':  signed/unsigned mismatch
#pragma warning (disable : 4100)	// unreferenced formal parameter
#pragma warning (disable : 4127)	// conditional expression is constant
#pragma warning (disable : 4244)	// conv from X to Y, possible loss of data
#pragma warning (disable : 4310)	// cast truncates constant value
#pragma warning (disable : 4505)	// X: unreference local function removed
#pragma warning (disable : 4510)	// X: default ctor could not be generated
#pragma warning (disable : 4511)	// X: copy constructor could not be generated
#pragma warning (disable : 4512)	// assignment operator could not be generated
#pragma warning (disable : 4514)	// debug symbol exceeds 255 chars
#pragma warning (disable : 4610)	// union X can never be instantiated
#pragma warning (disable : 4663)	// to explicitly spec class template X use ...
#pragma warning (disable : 4710)	// function 'XXX' not expanded
#pragma	warning	(disable : 4786)	// X: identifier truncated to '255' chars


void CFocusEditCtrl::OnChar(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	if (!(GetStyle()& ES_MULTILINE))
	{
		switch(nChar)
		{ /* nChar */
		case VK_RETURN:
			GetParent()->SendMessage(UWM_EDIT_COMPLETE_MSG, 
				GetDlgCtrlID(), (LPARAM)this);
			return;
		case VK_TAB:
			GetParent()->SendMessage(UWM_EDIT_COMPLETE_MSG, 
				GetDlgCtrlID(), (LPARAM)this);
			return;
		} /* nChar */
	}
	CEdit::OnChar(nChar, nRepCnt, nFlags);
	if (m_bEnableAutoComplete)
		SetTimer(AU_EV_SEARCH,m_uTimeDelay,NULL);
}

UINT CFocusEditCtrl::OnGetDlgCode()
{
	UINT uRet=CEdit::OnGetDlgCode();

	if (LPMSG pMsg = (LPMSG)GetCurrentMessage()->lParam){
			if ((pMsg->message == WM_KEYDOWN  || pMsg->message==WM_CHAR) && ((pMsg->wParam == VK_RETURN)||(pMsg->wParam == VK_TAB)))
			// Only set DLGC_WANTALLKEYS if it is for a WM_KEYDOWN or a WM_CHAR with a VK_RETURN key.
			// This way all other standard keys, like tab, is handled as default.
			// Have to check for WM_CHAR also because otherwise I get a beep sound for the return key.
				uRet |= DLGC_WANTALLKEYS;
		} return uRet;

}


void CFocusEditCtrl::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	
	if(nChar == VK_DOWN&&!m_bPCmbBox)
		if(::IsWindow(m_list))
		{
			if (m_list.IsWindowVisible())
				m_list.SetFocus();
			else
				GetParent()->SendMessage(UWM_EDIT_COMPLETE_MSG, 
				GetDlgCtrlID(), (LPARAM)this);
			return;
		}
	if(nChar == VK_UP&&!m_bPCmbBox)
	{
		GetParent()->PostMessage(WM_NEXTDLGCTL,1,0);
		return;
	}
	CEdit::OnKeyDown(nChar, nRepCnt, nFlags);
}

void CFocusEditCtrl::OnTimer(UINT nIDEvent)
{
	if(nIDEvent == AU_EV_SEARCH)
		KillTimer(nIDEvent);


	CString strFilter;
	bool bFound;
	int nBegin,nEnd;

	GetWindowText(strFilter);

	GetSel(nBegin,nEnd);

	if(nBegin != nEnd || strFilter.GetLength() <= (int)m_nFilterMin)
	{ //se o tamanho do texto é muito pequeno
		if(::IsWindow(m_list))
		{
			m_list.ShowWindow(SW_HIDE);
			m_list.ResetContent();
		}
		return;
	}

	bFound = OnSearch(strFilter);

	if(!bFound)
	{
		if(::IsWindow(m_list))
		{
			if(m_list.IsWindowVisible())
				m_list.ShowWindow(SW_HIDE);
			if(m_list.GetCount()>0)
				m_list.ResetContent();
		}
		return;
	}
	if(!::IsWindow(m_list))
		return;

	if(m_list.GetCount() || m_list.GetCount() == LB_ERR)
		m_list.ResetContent();

	OnFillList(); //call virtual

	int nRows = m_list.GetCount();
	if(nRows > 1)
	{
		CSize sz;
		CRect rctList;
		GetTextExtentPoint32(GetDC()->m_hDC,"A",1,&sz);
		GetWindowRect(rctList);

		if(!(m_list.GetStyle()&WS_POPUP))
			GetParent()->ScreenToClient(rctList);

		if((UINT)nRows > m_nMaxSizeList)
			nRows = m_nMaxSizeList;

		m_list.MoveWindow(rctList.left,rctList.bottom,rctList.Width(),sz.cy*nRows);
		m_list.ShowWindow(SW_SHOWNA);
	}
	else if(m_list.IsWindowVisible())
		m_list.ShowWindow(SW_HIDE);

	CString strNewText;
	m_list.GetText(0,strNewText);	

	SetWindowText(strNewText);
	SetSel(nBegin,-1);


	CEdit::OnTimer(nIDEvent);
}

void CFocusEditCtrl::PreSubclassWindow()
{
	if(!m_list.m_hWnd)
		VERIFY(m_list.CreateEx(WS_EX_CONTROLPARENT|/*WS_EX_TOOLWINDOW|*/WS_EX_WINDOWEDGE|WS_EX_TOPMOST,"ListBox","",LBS_NOTIFY|/*WS_POPUP|*/WS_BORDER|WS_VISIBLE|WS_VSCROLL|WS_CHILD,CRect(0,0,0,0),GetParent(),0,this));
	else
		m_list.ResetContent();

	CFont font;

	m_list.SetFont(CFont::FromHandle((HFONT)::GetStockObject(DEFAULT_GUI_FONT)));//corrige a fonte do list-box

	m_list.ShowWindow(SW_HIDE);

	CEdit::PreSubclassWindow();
}

void CFocusEditCtrl::OnKillFocus(CWnd* pNewWnd)
{
	CEdit::OnKillFocus(pNewWnd);

	if(::IsWindow(m_list) && pNewWnd != NULL && pNewWnd->m_hWnd != m_list.m_hWnd && m_list.IsWindowVisible()) //se o novo foco nao for a list-box
	{
		m_list.ShowWindow(SW_HIDE);
		m_list.ResetContent();
	}

}

bool CFocusEditCtrl::OnSearch(LPCTSTR szFilter)
{
	for(int nIndex = 0;nIndex < m_strList.size();nIndex++)
	{
		if(_tcsnicmp(m_strList[nIndex],szFilter,_tcslen(szFilter)) == 0)
			return true;
	}
	return false;
}

void CFocusEditCtrl::OnFillList()
{
	CString szFilter;
	GetWindowText(szFilter);
	for(int nIndex = 0;nIndex < m_strList.size();nIndex++)
	{
		if(_tcsnicmp(m_strList[nIndex],szFilter,_tcslen(szFilter)) == 0)
			m_list.AddString(m_strList[nIndex]);

	}
}
void CFocusEditCtrl::OnMoving(UINT fwSide, LPRECT pRect)
{
	CEdit::OnMoving(fwSide, pRect);

	// TODO: Add your message handler code here
}
