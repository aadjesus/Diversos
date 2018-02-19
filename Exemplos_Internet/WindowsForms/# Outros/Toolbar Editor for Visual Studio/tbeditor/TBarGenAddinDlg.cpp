// TBarGenAddinDlg.cpp : implementation file
//


#include "stdafx.h"
#include "TBarGenAddin.h"
#include "TBarGenAddinDlg.h"
#include "Quantize\Quantize.h"
#include "HyperLink\HyperLink.h"
#include "ximage.h"
#include "DlgOptions.h"
#include "afxwin.h"
#include "DlgNewToolbar.h"
#include "DlgHelp.h"
#include "UpdateCheck\DlgChkUpdates.h"
#include ".\tbargenaddindlg.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CMagDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	CHyperLink	m_static_link;
	CHyperLink	m_static_linkforum;

	virtual BOOL OnInitDialog();
	DECLARE_MESSAGE_MAP()
public:

	afx_msg void OnBnClickedOk();
};

CAboutDlg::CAboutDlg() : CMagDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CMagDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_LINK, m_static_link);
	DDX_Control(pDX, IDC_STATIC_LINKFORUM, m_static_linkforum);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CMagDialog)
	ON_BN_CLICKED(IDOK, &CAboutDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// CTBarGenAddinDlg dialog


BOOL CAboutDlg::OnInitDialog()
{
	CMagDialog::OnInitDialog();
	
	m_static_link.SetURL("http://www.p-network.net/dn_nuke/Projects/ToolbarEditorforvisualstudio/tabid/56/Default.aspx");
	m_static_linkforum.SetURL("http://www.p-network.net/dn_nuke/Forum/tabid/54/Default.aspx");
	m_static_linkforum.SetWindowText("Support Forum");
	return TRUE;  
}

void CAboutDlg::OnBnClickedOk()
{
	OnOK();
}


CTBarGenAddinDlg::CTBarGenAddinDlg(CWnd* pParent /*=NULL*/)
	: CMagDialog(CTBarGenAddinDlg::IDD, pParent)
{
	m_hIcon					= AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_imageList				= new CImageList;
	m_libImageList			= new CImageList;
	m_pDropWnd				= NULL;
	m_bDragging				= FALSE;
	m_bIsNewToolbar			= FALSE;
	m_nDragIndex			= -1;
	m_nDropIndex			= -1;
	m_DragImageList			= NULL;
	m_strResourceIncFile	= "resource.h";
	m_iDocType				= ID_RESDOC;

}

CTBarGenAddinDlg::~CTBarGenAddinDlg()
{
	delete m_imageList;
	delete m_libImageList;
}

void CTBarGenAddinDlg::DoDataExchange(CDataExchange* pDX)
{
	CMagDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_BUTTONS, m_list_buttons);
	DDX_Control(pDX, IDC_LIST_LIBRARY, m_list_library);
	DDX_Control(pDX, IDC_BUTTON_COLOR, m_button_color);
	DDX_Control(pDX, IDC_STATIC_PICTURE, m_static_picture);
	DDX_Control(pDX, IDC_STATIC_TOOLBAR, m_static_toolbar);
	DDX_Control(pDX, IDC_EDIT_BMPFILE, m_edit_bmpfile);
	DDX_Control(pDX, IDC_COMBO_BPP, m_combo_bpp);
	DDX_Control(pDX, IDC_COMBO_RESOURCENAME, m_combo_resourceName);
	DDX_Control(pDX, IDC_CHECK_TCBMP, m_check_tcbmp);
	DDX_Control(pDX, IDC_CHECK_GRBMP, m_check_grbmp);
	DDX_Control(pDX, IDC_CHECK_HTBMP, m_check_htbmp);
	DDX_Control(pDX, IDC_SPIN_CNT, m_spin_cnt);
	DDX_Control(pDX, IDC_STATIC_CNT, m_static_cnt);
	DDX_Control(pDX, IDC_EDIT_ERX, m_edit_rx);
	DDX_Control(pDX, IDC_EDIT_ERY, m_edit_ry);
	DDX_Control(pDX, IDC_BUTTON_MASKCOLOR, m_button_maskcolor);
	DDX_Control(pDX, IDC_CHECK_MASKCORNER, m_check_maskcorner);
	DDX_Control(pDX, IDC_CHECK_MASKBUTTON, m_check_maskcolor);
	DDX_Control(pDX, IDC_BUTTON_SCANRESOURCE, m_button_scanresource);
}

BEGIN_MESSAGE_MAP(CTBarGenAddinDlg, CMagDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_DROPFILES()
	ON_WM_MOUSEMOVE()
	ON_BN_CLICKED(IDC_BUTTON_TOP, OnBnClickedButtonTop)
	ON_BN_CLICKED(IDC_BUTTON_UP, OnBnClickedButtonUp)
	ON_BN_CLICKED(IDC_BUTTON_DOWN, OnBnClickedButtonDown)
	ON_BN_CLICKED(IDC_BUTTON_BOTTOM, OnBnClickedButtonBottom)
	ON_BN_CLICKED(IDC_BUTTON_REFRESH, OnBnClickedButtonRefresh)
	ON_BN_CLICKED(IDC_BUTTON_COLOR, OnBnClickedButtonColor)
	ON_BN_CLICKED(IDC_BUTTON_SAVEAS, OnBnClickedButtonSaveas)
	ON_BN_CLICKED(IDC_BUTTON_SAVE, OnBnClickedButtonSave)
	ON_BN_CLICKED(IDC_BUTTON_GENERATE, OnBnClickedButtonGenerate)
	ON_BN_CLICKED(IDC_BUTTON_SCANRESOURCE, OnBnClickedButtonScanresource)
	ON_MESSAGE(WM_BUTTONMOVED,OnTbButtonMove)
	ON_MESSAGE(WM_EDIT_COMMITTED,OnEditComplete)
	ON_MESSAGE(WM_BUTTON_LCLICKED,OnSmallButtonClicked)
	ON_NOTIFY(NM_RCLICK, IDC_LIST_BUTTONS, OnNMRclickListButtons)
	ON_COMMAND(ID_FILE_OPEN, OnFileOpen)
	ON_COMMAND(ID_EDITIMAGE_RESIZE, OnEditimageResize)
	ON_COMMAND(ID_ABOUT_ABOUTTBARGENADDIN, OnAboutAbouttbargenaddin)
	ON_COMMAND(ID_IMAGE_RESIZE, OnImageResize)
	ON_COMMAND(ID_POPUP_ADDNEW, OnPopupAddnew)
	ON_COMMAND(ID_POPUP_DELETE, OnPopupDelete)
	ON_COMMAND(ID_TOOLS_OPTIONS, OnToolsOptions)
	ON_STN_CLICKED(IDC_STATIC_IMGLIB, OnStnClickedStaticImglib)
	ON_WM_LBUTTONUP()
	ON_NOTIFY(LVN_BEGINDRAG, IDC_LIST_LIBRARY, OnLvnBegindragListLibrary)
	ON_NOTIFY(LVN_BEGINDRAG, IDC_LIST_BUTTONS, OnLvnBegindragListButtons)
	ON_BN_CLICKED(IDC_BUTTON_TCOPT, OnBnClickedButtonTcopt)
	ON_BN_CLICKED(IDC_BUTTON_GROPT, OnBnClickedButtonGropt)
	ON_COMMAND(ID_FILE_EXIT, OnFileExit)
	ON_COMMAND(ID_FILE_SAVEBITMAP, OnFileSavebitmap)
	ON_NOTIFY_EX_RANGE(TTN_NEEDTEXTW, 0, 0xFFFF, OnToolTipText)
	ON_NOTIFY_EX_RANGE(TTN_NEEDTEXTA, 0, 0xFFFF, OnToolTipText)
	ON_COMMAND(ID_ACCELERATOR_CANC, OnAcceleratorCanc)
	ON_COMMAND(ID_ACCELERATOR_REFRESH, OnAcceleratorRefresh)
	ON_COMMAND(ID_FILE_NEW, OnFileNew)
	ON_COMMAND_RANGE(ID_FILE_MRU_FILE1, ID_FILE_MRU_FILE1+ID_MAX_MRU, OnMRUList)
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BUTTON_HTOPT, OnBnClickedButtonHtopt)
	ON_COMMAND(ID_ABOUT_HELP, OnAboutHelp)
//	ON_COMMAND(ID_ACCELERATOR_HELP, OnAcceleratorHelp)
	ON_COMMAND(ID_POPUP_INSERTNEW, OnPopupInsertnew)
	ON_COMMAND(ID_TOOLBAR_COPYTOCLIPBOARD, OnToolbarCopytoclipboard)
	ON_COMMAND(ID_TOOLBAR_MERGEIDS, OnToolbarMergeids)
	ON_COMMAND(ID_TOOLBAR_GENERATE, OnToolbarGenerate)
	ON_COMMAND(ID_ABOUT_CHECKFORUPDATES, OnAboutCheckforupdates)
	ON_BN_CLICKED(IDC_BUTTON_TEST, OnBnClickedButtonTest)
	ON_BN_CLICKED(IDC_CHECK_MASKCORNER, &CTBarGenAddinDlg::OnBnClickedCheckMaskcorner)
	ON_BN_CLICKED(IDC_CHECK_MASKBUTTON, &CTBarGenAddinDlg::OnBnClickedCheckMaskbutton)
	ON_UPDATE_COMMAND_UI(ID_POPUP_SAVEIMAGEINLIBRARY, &CTBarGenAddinDlg::OnUpdatePopupSaveimageinlibrary)
	ON_COMMAND(ID_POPUP_SAVEIMAGEINLIBRARY, &CTBarGenAddinDlg::OnPopupSaveimageinlibrary)
END_MESSAGE_MAP()


// CTBarGenAddinDlg message handlers

BOOL CTBarGenAddinDlg::OnInitDialog()
{
	CMagDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	CRect rect;
	CString strHead;
	int larg,col1,col2,col3;

	m_list_buttons.GetWindowRect(rect);

	larg = rect.Width()-2;
	col1 = (larg - 2)/ 100 *25;
	col2 = (larg - 2)/ 100 *30;
	col3 = (larg - 2)/ 100 *45;

	strHead.Format("Type,%d;Id,%d;Image,%d;", 
		(int)col1,(int)col2,(int)col3);
	
	CSTRINGVECTOR vType;
	vType.push_back("BUTTON");
	vType.push_back("SEPARATOR");

	m_list_buttons.SetColumnHeader(strHead);
	m_list_buttons.SetSortable(FALSE);
	m_list_buttons.SetEditable(TRUE);

	m_list_buttons.SetColoumnEditStyle(0,RC_COMBO_CTRL);
	m_list_buttons.SetColoumnEditStyle(1,RC_EDIT_CTRL);
	m_list_buttons.SetColoumnEditStyle(2,RC_BUTTON_CTRL);
	
	m_list_buttons.SetComboStrings(vType);
	m_list_buttons.SetButtonCaption("...");

	m_list_library.GetWindowRect(rect);
	col1 = rect.Width()- 4;
	strHead.Format("image,%d;",(int)col1);
	m_list_library.SetColumnHeader(strHead);

	m_ctrl_toolbar.Create(CBRS_TOOLTIPS |CCS_NORESIZE |CCS_ADJUSTABLE /*|CCS_NOPARENTALIGN*/ ,
		CRect(0,0,0,0),this,IDR_TOOLBAR_PREVIEW); 
	m_ctrl_toolbar.ShowWindow(SW_HIDE);

	m_static_toolbar.GetWindowRect(&rect);
	ScreenToClient(&rect);
	m_ctrl_toolbar.MoveWindow(rect);

	m_button_color.SetColor(RGB(192,192,192));
	m_button_maskcolor.SetColor(RGB(192,192,192));
	m_combo_bpp.SetCurSel(0);

	m_mainMenu.LoadMenu(IDR_MAINFRAME);
	m_mainMenu.LoadToolTCbar(IDR_MAINFRAME,IDR_MAINFRAME_TC);
	m_mainMenu.LoadToolTCbar(IDR_MENUTOOLBARHELPER,IDB_BITMAP_TBHELPER_TC);
	SetMenu(&m_mainMenu);

	m_mruMenu.CreatePopupMenu();

	InitMRUList();

	m_main_toolbar.Create(this);
	m_main_toolbar.LoadToolBar(IDR_MAINFRAME);
	m_main_toolbar.LoadTrueColorToolBar(16,IDR_MAINFRAME_TC,IDR_MAINFRAME_HT,IDR_MAINFRAME_GR);
	
	m_main_toolbar.SetBarStyle(m_main_toolbar.GetBarStyle() |
		CBRS_TOOLTIPS | CBRS_FLYBY  );

	CRect rcClientStart;
	CRect rcClientNow;
	GetClientRect(rcClientStart);
	RepositionBars(AFX_IDW_CONTROLBAR_FIRST, AFX_IDW_CONTROLBAR_LAST,
		0, reposQuery, rcClientNow);

	CPoint ptOffset(rcClientNow.left - rcClientStart.left,
		rcClientNow.top - rcClientStart.top);

	CRect  rcChild;
	CWnd* pwndChild = GetWindow(GW_CHILD);
	while (pwndChild)
	{
		pwndChild->GetWindowRect(rcChild);
		ScreenToClient(rcChild);
		rcChild.OffsetRect(ptOffset);
		pwndChild->MoveWindow(rcChild, FALSE);
		pwndChild = pwndChild->GetNextWindow();
	}

	// Adjust the dialog window dimensions
	CRect rcWindow;
	GetWindowRect(rcWindow);
	rcWindow.right += rcClientStart.Width() - rcClientNow.Width();
	rcWindow.bottom += rcClientStart.Height() - rcClientNow.Height();
	MoveWindow(rcWindow, FALSE);

	// And position the control bars
	RepositionBars(AFX_IDW_CONTROLBAR_FIRST, AFX_IDW_CONTROLBAR_LAST, 0);



	char buffer[_MAX_PATH];

	SHGetSpecialFolderPath(
		0,       // Hwnd
		buffer, // String buffer.
		CSIDL_APPDATA, // CSLID of folder
		FALSE );

	CPath path;
	path.SetPath(buffer,TRUE);
	path.SetPath(path.GetLocation()+_TBEDITORFLDNAME,TRUE);

	if (!IsDirectory(path.GetLocation()))
	{
		::CreateDirectory(path.GetLocation(),NULL);
	}

	m_strTmpDir = AfxGetApp()->GetProfileString("config","tempFolder",path.GetLocation()+"temp");

	m_strImgLib = AfxGetApp()->GetProfileString("config","imgLib",path.GetLocation()+"imgLib");
	
	if (m_strImgLib[m_strImgLib.GetLength()-1] != '\\')
		m_strImgLib+="\\";
	if (m_strTmpDir[m_strTmpDir.GetLength()-1] != '\\')
		m_strTmpDir+="\\";

	if (!IsDirectory(m_strTmpDir))
	{
		::CreateDirectory(m_strTmpDir,NULL);
	}
	
	if (!IsDirectory(m_strImgLib))
	{
		::CreateDirectory(m_strImgLib,NULL);
	}
	
	m_bAutoResize	= AfxGetApp()->GetProfileInt("config","autosize",0);
	m_bReplaceOFile = AfxGetApp()->GetProfileInt("config","replaceof",1);

	if (AfxGetApp()->GetProfileInt("config","version",0) < ID_PVERSION)
	{
		AfxMessageBox("This is the first time that you are running Toolbar Editor.\nPlase check settings.",
			MB_ICONINFORMATION|MB_OK);
		CDlgOptions dlg;
		dlg.DoModal();
	}

	m_check_tcbmp.SetCheck(1);

	m_spin_cnt.SetRange(0,100);
	m_spin_cnt.SetBuddy(&m_static_cnt);

	if (CDlgChkUpdates::IsTimeToCheck(10))
		OnAboutCheckforupdates();

	AddMagneticDialog(&m_dlgLibPreview,TRUE,DKDLG_RIGHT);

	m_dlgLibPreview.EnableMagnetic(DKDLG_ANY,this);
	m_dlgLibPreview.Create(IDD_DIALOG_LIBPREVIEW,this);

	m_check_maskcorner.SetCheck(BST_CHECKED);
	m_combo_resourceName.AddRow(" ");

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CTBarGenAddinDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CMagDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CTBarGenAddinDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CMagDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CTBarGenAddinDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


BOOL CTBarGenAddinDlg::OpenProject(CString strFile)
{
	CString strLine;
	CStdioFile fin;

	if (!fin.Open(strFile,CFile::modeRead | CFile::typeText))
		return FALSE;

	while (!fin.ReadString(strLine))
	{

	}

	return TRUE;
}


void CTBarGenAddinDlg::GetImageFromList(CImageList *lstImages, 
									  int nImage, CBitmap* destBitmap,COLORREF rgbBackColor)
{    

	CImageList tmpList;
	tmpList.Create(lstImages);
	tmpList.Copy( 0, nImage, ILCF_SWAP );

	IMAGEINFO lastImage;
	tmpList.GetImageInfo(0,&lastImage);

	CDC dcMem; dcMem.CreateCompatibleDC (GetWindowDC()); 

	CRect rect (lastImage.rcImage);
	destBitmap->CreateCompatibleBitmap (this->GetWindowDC(), 
		rect.Width (), rect.Height ());

	CBitmap* pBmpOld = dcMem.SelectObject (destBitmap);

	tmpList.DrawIndirect (&dcMem, 0, CPoint (0, 0), 
		CSize (rect.Width (), rect.Height ()), CPoint (0, 0),ILD_NORMAL,SRCCOPY,rgbBackColor);

	dcMem.SelectObject (pBmpOld);

}
//}

BOOL CTBarGenAddinDlg::ParseResourceFile(CString strFile,CString strResourceID)
{
	CString strLine;
	CStdioFile fin;
	CString strTMP;

	BOOL bFlag;
	int nCount		=0;
	int nResLength	=0;

	try
	{
		if (!fin.Open(strFile,CFile::modeRead | CFile::typeText))
			return FALSE;

		while (fin.ReadString(strLine))
		{
			nResLength = strLine.FindOneOf("\t ");
			if (nResLength > 0)
				strTMP = strLine.Left(nResLength);
			else
				strTMP = "";

			if (strTMP.Compare(strResourceID) == 0)
			{
				nCount = strLine.Find("BITMAP",nResLength);
				if(nCount >=0)
				{

					strTMP = strLine.Left(nCount);
					strTMP.Trim();
					m_bitmap.id = strTMP;

					int n = 0;
					int i = strLine.GetLength();
					while(i>=0)
					{
						if ((strLine[i]=='\t') || (strLine[i]==' ')) //search for first delimiter (shoul be a tab or a space)
						{
							n = i;
							i = 0; //exit from while
						}
						i--;
					}

					strTMP = strLine.Mid(n,(strLine.GetLength()-n));
					strTMP.Trim();
					strTMP.Trim("\"");
					m_bitmap.path = strTMP;
							
				}

				nCount = strLine.Find("TOOLBAR",nResLength);

				if(nCount >=0)
				{
					strTMP = strLine.Left(nCount);
					strTMP.Trim();
					m_toolbar.id = strTMP;

					strTMP = strLine.Right(strLine.GetLength()-(nCount+8));
					strTMP.Trim();
					strTMP.Trim('\t');
					int nCommaPos	= strTMP.ReverseFind(',');
					if (nCommaPos >0)
					{
						int n = 0;
						int i = nCommaPos-1;
						while(i>=0)
						{
							if ((strTMP[i]=='\t') || (strTMP[i]==' '))	//search for first delimiter 
																		//(shoul be a tab or a space)
							{
								n = i;
								i = 0; //exit from while
							}
							i--;
						}
						CString uga = strTMP.Mid(n,nCommaPos-n);
						m_toolbar.rx = atoi(strTMP.Mid(n,nCommaPos-n));
						m_toolbar.ry = atoi(strTMP.Right(strTMP.GetLength()-(nCommaPos+1)));
						bFlag = TRUE;

						if ((m_toolbar.rx == 0) || (m_toolbar.rx == 0))
						{
							AfxMessageBox("The resource file can't be correctly parsed.\nAborting.",MB_ICONEXCLAMATION|MB_OK);
							m_toolbar.rx	= 0;
							m_toolbar.rx	= 0;
							m_toolbar.id	= "";
							bFlag			= FALSE;
						}
					}
					else
					{
						m_toolbar.id	= "";
						bFlag			= FALSE;
					}

					while(bFlag)
					{
						if(fin.ReadString(strLine))
						{
							strLine.Trim();
							if (strLine == "END")
							{
								bFlag = FALSE;
							}
							else
							{
								BOOL bButtonFound = FALSE;
								st_toolbar_button tmp_button;
								strLine.Trim();
								if ((nCount = strLine.Find("BUTTON")) >=0)
								{
									strTMP = strLine.Right(strLine.GetLength()-(nCount+7));
									strTMP.Trim();
									tmp_button.id	= strTMP;
									tmp_button.type = "BUTTON";
									bButtonFound = TRUE;
								}

								if (((nCount = strLine.Find("SEPARATOR")) >=0) 
									&& (!bButtonFound)) //Someone can use SEPARATOR as ID also!
								{
									tmp_button.type = "SEPARATOR";
									bButtonFound = TRUE;
								}


								if(bButtonFound)
								{				
									tmp_button.bChkId = FALSE;
									m_toolbar.buttons.push_back(tmp_button);

								}
							}
						}
						else
						{
							bFlag = FALSE;
						}

					} //while(!bFlag)

				}
			}

		}
		fin.Close();

	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();
		return FALSE;
	}

	if (m_toolbar.buttons.size() < 1)
	{
		CString strTMP;
		strTMP.Format("Toolbar %s not found on specified resource file.",strResourceID);
		AfxMessageBox(strTMP,MB_ICONINFORMATION|MB_OK);
		return FALSE;
	}
	return TRUE;
}

BOOL CTBarGenAddinDlg::OpenResource(CString strFile,CString strResourceID)
{

	m_list_buttons.DeleteAllItems();

	ResetContent(FALSE);
	
	if (strResourceID.IsEmpty())
		return FALSE;

	if (!ParseResourceFile(strFile,strResourceID))
		return FALSE;
	
	CBitmap bmp;

	
	CPath path;
	CString strPath;
	COLORREF	rgbColorMask;
	RGBQUAD		rgbQuadColorMask;
	RGBQUAD		rgbQuadColorBlack;
	
	rgbQuadColorBlack.rgbBlue	= 0;
	rgbQuadColorBlack.rgbGreen	= 0;
	rgbQuadColorBlack.rgbRed	= 0;
	
	path.SetPath(strFile);

	strPath = path.GetLocation()+"\\"+m_bitmap.path;
	strPath.Replace("\\\\","\\");

	m_bitmap.path = strPath;
	path.SetPath(strPath);

	m_strTcBmpFile = path.GetLocation()+path.GetFileTitle()+"_TC.bmp";
	m_strGrBmpFile = path.GetLocation()+path.GetFileTitle()+"_GR.bmp";
	m_strHtBmpFile = path.GetLocation()+path.GetFileTitle()+"_HT.bmp";
	CxImage bmpImg;
	
	if (IsFile(m_strTcBmpFile)) // load true color bitmap! :)
	{
		strPath = m_strTcBmpFile;
	}
	if (bmpImg.Load(strPath))
	{
		if (bmpImg.GetBpp() != 24)
			bmpImg.IncreaseBpp(24);
		m_edit_bmpfile.SetWindowText(m_bitmap.path);
		m_imageList->DeleteImageList();
		m_imageList->Create(m_toolbar.rx, m_toolbar.ry,ILC_MASK | ILC_COLOR32,0,50);
		//rgbColorMask = GetFirstPixelColor(CBitmap::FromHandle(bmpImg));
		
		if(m_check_maskcorner.GetCheck() != BST_CHECKED)
			rgbQuadColorMask = CxImage::RGBtoRGBQUAD(m_button_maskcolor.GetColor());
		else
			rgbQuadColorMask = bmpImg.GetPixelColor(0,0,false);

		rgbColorMask = RGB(rgbQuadColorMask.rgbRed,rgbQuadColorMask.rgbGreen,rgbQuadColorMask.rgbBlue);
		m_imageList->Add(CBitmap::FromHandle(bmpImg.MakeBitmap()),rgbColorMask);
		//m_imageList->SetBkColor(rgbColorMask);

		m_list_buttons.SetImageList(m_imageList);


		UINT nBtn = 0;
		for(UINT i=0; i < m_toolbar.buttons.size();i++)
		{
			int nPos = m_list_buttons.GetItemCount();
			m_list_buttons.InsertItem(nPos,"",-1);

			m_list_buttons.SetItemText(nPos,0,m_toolbar.buttons[i].type);
			m_list_buttons.SetItemText(nPos,1,m_toolbar.buttons[i].id);

			if (m_toolbar.buttons[i].type == "BUTTON")
			{
				CBitmap bmp;
				CImage img;
				CString strFileName;
				
				CImageList tmp_imglist;
				tmp_imglist.Create(m_imageList);
				
				GetImageFromList(&tmp_imglist,nBtn,&bmp,rgbColorMask);
				img.Attach(HBITMAP(bmp));
				strFileName.Format("%s%s.bmp",
					m_strTmpDir,m_toolbar.buttons[i].id);
				img.Save(strFileName);
				
				m_list_buttons.SetItemText(nPos,2,strFileName);
				m_list_buttons.SetItemImage(nPos,2,nBtn);
				m_toolbar.buttons[i].nPosImg = nBtn;
				m_toolbar.buttons[i].imgPath = strFileName;
				nBtn++;
			}
			else
			{
				m_toolbar.buttons[i].nPosImg = -1;
			}
		}
	}
	else
	{
		if (!bmpImg.Load(m_bitmap.path))
		{
			AfxMessageBox("cannot load default toolbar bitmap.\nAborting.",
				MB_ICONERROR,MB_OK);
			return FALSE;
		}

		if (bmpImg.GetBpp() != 24)
			bmpImg.IncreaseBpp(24);

		m_edit_bmpfile.SetWindowText(m_bitmap.path);
		m_imageList->DeleteImageList();
		m_imageList->Create(m_toolbar.rx, m_toolbar.ry,ILC_MASK | ILC_COLOR32,0,50);
		//rgbColorMask = GetFirstPixelColor(CBitmap::FromHandle(bmpImg));

		if(m_check_maskcorner.GetCheck() != BST_CHECKED)
			rgbQuadColorMask = CxImage::RGBtoRGBQUAD(m_button_maskcolor.GetColor());
		else
			rgbQuadColorMask = bmpImg.GetPixelColor(0,0,false);

		rgbColorMask = RGB(rgbQuadColorMask.rgbRed,rgbQuadColorMask.rgbGreen,rgbQuadColorMask.rgbBlue);
		m_imageList->Add(CBitmap::FromHandle(bmpImg.MakeBitmap()),rgbColorMask);

		m_list_buttons.SetImageList(m_imageList);
		
		UINT nBtn = 0;
		for(UINT i=0; i < m_toolbar.buttons.size();i++)
		{
			int nPos = m_list_buttons.GetItemCount();
			m_list_buttons.InsertItem(nPos,"",-1);

			m_list_buttons.SetItemText(nPos,0,m_toolbar.buttons[i].type);
			m_list_buttons.SetItemText(nPos,1,m_toolbar.buttons[i].id);

			if (m_toolbar.buttons[i].type == "BUTTON")
			{
				CBitmap bmp;
				CImage img;
				CString strFileName;

				GetImageFromList(m_imageList,nBtn,&bmp,rgbColorMask);
				img.Attach(HBITMAP(bmp));
				strFileName.Format("%s%s.bmp",
					m_strTmpDir,m_toolbar.buttons[i].id);
				img.Save(strFileName);
				
				m_list_buttons.SetItemText(nPos,2,strFileName);
				m_list_buttons.SetItemImage(nPos,2,nBtn);
				m_toolbar.buttons[i].nPosImg = nBtn;
				m_toolbar.buttons[i].imgPath = strFileName;
				nBtn++;
			}
			else
			{
				m_toolbar.buttons[i].nPosImg = -1;
			}
		}
	
	}
	
	return TRUE;
}
void CTBarGenAddinDlg::OnBnClickedButtonTop()
{
	int nPos = m_list_buttons.GetCurSel();
	m_list_buttons.MoveToTop(nPos);

	if ((nPos) > 0)
	{
		vector <st_toolbar_button>::iterator itr = m_toolbar.buttons.begin();
		st_toolbar_button button_tmp = m_toolbar.buttons[nPos];
		m_toolbar.buttons.erase(itr+nPos);
		m_toolbar.buttons.insert(itr,button_tmp);
	}
}

void CTBarGenAddinDlg::OnBnClickedButtonUp()
{
	int nPos = m_list_buttons.GetCurSel();
	m_list_buttons.MoveUp(nPos);

	if ((nPos) > 0)
	{
		vector <st_toolbar_button>::iterator itr = m_toolbar.buttons.begin();
		st_toolbar_button button_tmp = m_toolbar.buttons[nPos];
		m_toolbar.buttons.erase(itr+nPos);
		m_toolbar.buttons.insert(itr+nPos-1,button_tmp);
	}
}

void CTBarGenAddinDlg::OnBnClickedButtonDown()
{
	int nPos = m_list_buttons.GetCurSel();
	m_list_buttons.MoveDown(nPos);

	if ((nPos) < (int)m_toolbar.buttons.size()-1)
	{
		vector <st_toolbar_button>::iterator itr = m_toolbar.buttons.begin();
		st_toolbar_button button_tmp = m_toolbar.buttons[nPos];
		m_toolbar.buttons.erase(itr+nPos);
		m_toolbar.buttons.insert(itr+nPos+1,button_tmp);
	}
}

void CTBarGenAddinDlg::OnBnClickedButtonBottom()
{
	int nPos = m_list_buttons.GetCurSel();
	m_list_buttons.MoveToBottom(nPos);

	if ((nPos) > 0)
	{
		vector <st_toolbar_button>::iterator itr = m_toolbar.buttons.begin();
		st_toolbar_button button_tmp = m_toolbar.buttons[nPos];
		m_toolbar.buttons.erase(itr+nPos);
		m_toolbar.buttons.push_back(button_tmp);
	}
}

void CTBarGenAddinDlg::OnDropFiles(HDROP hDropInfo)
{
	
	UINT Count=0;
	TCHAR szFile[MAX_PATH]=_T("");

	m_list_buttons.EnableWindow(FALSE);
	m_list_buttons.SetRedraw(FALSE);
	
	CPoint Pt;
	DragQueryPoint(hDropInfo,&Pt);
	Count=DragQueryFile(hDropInfo,-1,NULL,0);
	
	for(UINT i=0;i<Count;i++)
	{
		_tcscpy(szFile,_T(""));
		DragQueryFile(hDropInfo,i,szFile,sizeof(szFile));
		if(IsFile(szFile))
		{
			st_toolbar_button tmp_button;
			LoadFromFile(szFile,&tmp_button);

			m_toolbar.buttons.push_back(tmp_button);
		}

	}
	m_list_buttons.SetRedraw(TRUE);
	m_list_buttons.EnableWindow(TRUE);


	//CMagDialog::OnDropFiles(hDropInfo);
}

int CTBarGenAddinDlg::LoadFromFile(CString strFileName,st_toolbar_button* tmp_button,int nPos)
{
	CxImage img;
	COLORREF rgbColorMask;
		
	if ((m_imageList->m_hImageList == NULL) || strFileName.IsEmpty())
	{
		// have to create :P (but i'm lazy..)
		return FALSE;
	}

	if (m_bAutoResize)
	{
		CString strFilePathOut;
		if (ResizeImage(strFileName,strFilePathOut))
		{
			strFileName = strFilePathOut;
		}
	}
	if(img.Load(strFileName))
	{
		int imgPos;
		CBitmap tmpBMP;
		
		RGBQUAD rgbq;

		if(img.GetBpp() != 24)
			img.IncreaseBpp(24);

		RGBQUAD rgbQuadColorMask;

		if(m_check_maskcorner.GetCheck() != BST_CHECKED)
			rgbQuadColorMask = CxImage::RGBtoRGBQUAD(m_button_maskcolor.GetColor());
		else
			rgbQuadColorMask = img.GetPixelColor(0,0,false);

		rgbColorMask = RGB(rgbQuadColorMask.rgbRed,rgbQuadColorMask.rgbGreen,rgbQuadColorMask.rgbBlue);
		tmpBMP.Attach(img.MakeBitmap());
		imgPos = m_imageList->Add(&tmpBMP,rgbColorMask);

		if (nPos == -1)
			nPos = m_list_buttons.GetItemCount();

		m_list_buttons.InsertItem(nPos,strFileName,-1);
		m_list_buttons.SetItemText(nPos,0,"BUTTON");
		
		m_list_buttons.SetItemText(nPos,2,strFileName);
		m_list_buttons.SetItemImage(nPos,2,imgPos);

		if (tmp_button != NULL)
		{
			TCHAR szTmp[MAX_PATH];
			memset(szTmp,0,MAX_PATH);
			_stprintf(szTmp,_T("ID_BUTTON_%s"),_tcsupr((char*)_tcsrchr(strFileName,'\\')+1));
			*(_tcschr(szTmp,'.'))='\0';
			tmp_button->id		= szTmp;
			tmp_button->type	= "BUTTON";
			tmp_button->nPosImg = imgPos;
			tmp_button->imgPath = strFileName;
			tmp_button->bChkId	= TRUE;
			// we will create an id only if we are loading the image for the first time...
			tmp_button->id.Trim();
			tmp_button->id.Replace(' ','_');
			tmp_button->id.MakeUpper();
			m_list_buttons.SetItemText(nPos,1,tmp_button->id); 
		}

		img.Destroy();
		return imgPos;
	}
	else
		return -1;
}
void CTBarGenAddinDlg::OnBnClickedButtonRefresh()
{
	if (m_imageList->m_hImageList != NULL)
	{
		if (!m_imageList->DeleteImageList())
		{
			AfxMessageBox("Error deleting imagelist.\nAborting",
				MB_ICONERROR|MB_OK);
		}
	}

	TBBUTTON *TbButtons;
	TbButtons = new TBBUTTON[m_toolbar.buttons.size()];


	m_list_buttons.SetRedraw(FALSE);
	m_list_buttons.EnableWindow(FALSE);
	m_list_buttons.DeleteAllItems();
	for (int j = m_ctrl_toolbar.GetButtonCount();j>0;j--)
	{
		m_ctrl_toolbar.DeleteButton(j-1);
	}
	
	if (!m_imageList->Create(m_toolbar.rx, m_toolbar.ry,ILC_MASK | ILC_COLOR32,0,50))
	{
		AfxMessageBox("Cannot create preview toolbar control\nAborting.",MB_ICONERROR|MB_OK);
		return;
	};

	UINT nBtn = 0;
	for(UINT i=0; i < m_toolbar.buttons.size();i++)
	{

		if (m_toolbar.buttons[i].type == "BUTTON")
		{
			if (!m_toolbar.buttons[i].imgPath.IsEmpty())
			{
				LoadFromFile(m_toolbar.buttons[i].imgPath,NULL);
				m_toolbar.buttons[i].nPosImg = nBtn;
			}
			else
			{
				m_toolbar.buttons[i].nPosImg = -1;
				int nPos = m_list_buttons.GetItemCount();

				m_list_buttons.InsertItem(nPos,"",-1);
				m_list_buttons.SetItemText(nPos,0,"BUTTON");
				m_list_buttons.SetItemText(nPos,2,"");
				m_list_buttons.SetItemImage(nPos,2,-1);

			}
			m_list_buttons.SetItemText(i,1,m_toolbar.buttons[i].id);
			TbButtons[i].iString	= -1;
			TbButtons[i].iBitmap	= m_toolbar.buttons[i].nPosImg;
			TbButtons[i].idCommand	=  i;
			TbButtons[i].fsStyle	= TBSTYLE_BUTTON; 
			TbButtons[i].fsState	= TBSTATE_ENABLED; 
			nBtn++;
		}
		else
		{
			int nPos = m_list_buttons.GetItemCount();
			m_list_buttons.InsertItem(nPos,"",-1);
			m_list_buttons.SetItemText(nPos,0,m_toolbar.buttons[i].type);
			
			TbButtons[i].iString	= -1;
			TbButtons[i].iBitmap	= -1;
			TbButtons[i].idCommand	= i;
			TbButtons[i].fsStyle	= TBSTYLE_SEP; 
			TbButtons[i].fsState	= TBSTATE_ENABLED; 

		}

	}

	m_imageList->SetBkColor(m_button_color.GetColor());
	m_list_buttons.SetImageList(m_imageList);

	m_list_buttons.SetRedraw(TRUE);
	m_list_buttons.EnableWindow(TRUE);

	if (m_toolbar.buttons.size() > 0) //this will need to avoid problems with AddButtons when size = 0
	{
		m_ctrl_toolbar.SetImageList(m_imageList);
		m_ctrl_toolbar.SetButtonSize(CSize(m_toolbar.ry,m_toolbar.rx));
		m_ctrl_toolbar.AddButtons((int)m_toolbar.buttons.size(),TbButtons);
	}
		
	m_ctrl_toolbar.ShowWindow(SW_SHOW);
	
	
	CBitmap bmpToolbar;
	GetImageFromList(m_imageList,&bmpToolbar);
	m_static_picture.SetBitmap(HBITMAP(bmpToolbar.Detach()));
	m_static_picture.Invalidate();
	
	CString strTMP;
	strTMP.Format("%d",m_toolbar.rx);
	m_edit_rx.SetWindowText(strTMP);
	strTMP.Format("%d",m_toolbar.ry);
	m_edit_ry.SetWindowText(strTMP);

	delete [] TbButtons;

	m_list_buttons.Invalidate(TRUE);


}

void CTBarGenAddinDlg::OnBnClickedButtonColor()
{
	
}


void CTBarGenAddinDlg::GetImageFromList(CImageList *lstImages, CBitmap* destBitmap)
{    

	CImageList tmpList;
	tmpList.Create(lstImages);


	IMAGEINFO lastImage;
	tmpList.GetImageInfo(0,&lastImage);

	CDC dcMem; 
	
	dcMem.CreateCompatibleDC(GetWindowDC()); 

	CRect rect (lastImage.rcImage);

	destBitmap->CreateCompatibleBitmap (this->GetWindowDC(), 
		rect.Width () * tmpList.GetImageCount(), rect.Height ());

	CBitmap* pBmpOld = dcMem.SelectObject (destBitmap);

	for (int i=0;i<tmpList.GetImageCount();i++)
	{
		tmpList.DrawIndirect (&dcMem, i, CPoint(i*rect.Width (), 0), 
			CSize (rect.Width (), rect.Height ()), CPoint (0, 0));
	}

	dcMem.SelectObject (pBmpOld);
}

COLORREF CTBarGenAddinDlg::GetFirstPixelColor(CBitmap* bmp)
{
	CDC hdcScreen,hdcCompatible;
	hdcScreen.CreateDC(_T("DISPLAY"), NULL, NULL, NULL); 
	hdcCompatible.CreateCompatibleDC(&hdcScreen);
	hdcCompatible.SelectObject(bmp);
	
	return  hdcCompatible.GetPixel(0,0);
}

LRESULT CTBarGenAddinDlg::OnTbButtonMove(WPARAM wParam,LPARAM lResult)
{
	NMHBT*	nmh;
	nmh		= (NMHBT*) lResult;
	st_toolbar_button buttonFrom;

	vector <st_toolbar_button>::iterator itr = m_toolbar.buttons.begin();
	buttonFrom	= m_toolbar.buttons[nmh->nButtonFrom];
	m_toolbar.buttons.erase(itr+nmh->nButtonFrom);
	itr = m_toolbar.buttons.begin();
	m_toolbar.buttons.insert(itr+nmh->nButtonTo,buttonFrom);

	OnBnClickedButtonRefresh();
	return TRUE;
}
void CTBarGenAddinDlg::OnMouseMove(UINT nFlags, CPoint point)
{
	if (m_bDragging)
	{
		CPoint pt(point);    
		ClientToScreen(&pt); 
		if (m_DragImageList != NULL)
		{
			m_DragImageList->DragMove(pt); 
			m_DragImageList->DragShowNolock(false);
		}
		CWnd* pDropWnd = WindowFromPoint (pt);
		ASSERT(pDropWnd);
		
		if (pDropWnd != m_pDropWnd)
		{
			//If we drag over the CListCtrl header, turn off the 
			// hover highlight
			if (m_nDropIndex != -1) 
			{
				TRACE("m_nDropIndex is -1\n");
				CListCtrl* pList = (CListCtrl*)m_pDropWnd;
				VERIFY (pList->SetItemState (m_nDropIndex, 0, 
					LVIS_DROPHILITED));
				// redraw item
				VERIFY (pList->RedrawItems (m_nDropIndex, 
					m_nDropIndex));
				pList->UpdateWindow ();
				m_nDropIndex = -1;
			}
			else //If we drag out of the CListCtrl altogether
			{
				TRACE("m_nDropIndex is not -1\n");
				CListCtrl* pList = (CListCtrl*)m_pDropWnd;
				int i = 0;
				int nCount = pList->GetItemCount();
				for(i = 0; i < nCount; i++)
				{
					pList->SetItemState(i, 0, LVIS_DROPHILITED);
				}
				pList->RedrawItems(0, nCount);
				pList->UpdateWindow();
			}
		}

		// Save current window pointer as the CListCtrl we are dropping onto
		m_pDropWnd = pDropWnd;

		// Convert from screen coordinates to drop target client coordinates
		pDropWnd->ScreenToClient(&pt);

		if(pDropWnd->IsKindOf(RUNTIME_CLASS (CListCtrl)))
		{            
			UINT uFlags;
			CListCtrl* pList = (CListCtrl*)pDropWnd;

			// Turn off hilight for previous drop target
			pList->SetItemState (m_nDropIndex, 0, LVIS_DROPHILITED);
			// Redraw previous item
			pList->RedrawItems (m_nDropIndex, m_nDropIndex);

			// Get the item that is below cursor
			m_nDropIndex = ((CListCtrl*)pDropWnd)->HitTest(pt, &uFlags);
			// Highlight it
			pList->SetItemState(m_nDropIndex, LVIS_DROPHILITED, 
				LVIS_DROPHILITED);
			// Redraw item
			pList->RedrawItems(m_nDropIndex, m_nDropIndex);
			pList->UpdateWindow();
		}
		// Lock window updates
		if (m_DragImageList != NULL)
			m_DragImageList->DragShowNolock(true);

	}
	CMagDialog::OnMouseMove(nFlags, point);
}

void CTBarGenAddinDlg::OnBnClickedButtonSaveas()
{
	CString strFilters = "Bitmap File (*.bmp)|*.bmp|All Files (*.*)|*.*||";

	CFileDialog fileDlg(FALSE, "Bitmap File", "*.bmp",
		 OFN_HIDEREADONLY, strFilters, this);


	if( fileDlg.DoModal ()==IDOK )
	{
 
		CString fileName = fileDlg.GetPathName();
		m_edit_bmpfile.SetWindowText(fileName);

	}

}

void CTBarGenAddinDlg::CreateDibBitmap(CBitmap* bmpIn,CBitmap *bmpOut, WORD BitCount)
{
	BITMAPINFO  lpbi; 
	BITMAP		bmp;

	LPBYTE pBits;
	HBITMAP hTargetBitmap;

	bmpIn->GetBitmap(&bmp);
	lpbi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	lpbi.bmiHeader.biWidth = bmp.bmWidth;
	lpbi.bmiHeader.biHeight = bmp.bmHeight;
	lpbi.bmiHeader.biBitCount = BitCount;
	lpbi.bmiHeader.biCompression = BI_RGB;
	lpbi.bmiHeader.biSizeImage = bmp.bmHeight * bmp.bmWidth;
	lpbi.bmiHeader.biPlanes = 1;
	lpbi.bmiHeader.biXPelsPerMeter = 0;
	lpbi.bmiHeader.biYPelsPerMeter = 0;
	lpbi.bmiHeader.biClrUsed = 0;
	lpbi.bmiHeader.biClrImportant = 0;


	HDC hDC = ::GetDC(NULL); 
	ASSERT(hDC); 
	hTargetBitmap = CreateDIBSection( hDC, &lpbi, DIB_RGB_COLORS, (void**)&pBits, NULL, 0); 
	
	

	::ReleaseDC(NULL, hDC);

	CDC memDc; 
	if (!memDc.CreateCompatibleDC(NULL)) 
	{ 
		ASSERT(FALSE); 
	} 

	CDC targetDc; 
	if (!targetDc.CreateCompatibleDC(NULL)) 
	{
		ASSERT(FALSE); 
	} //4.2 Select source bitmap into one DC, target into another HBITMAP 

	HBITMAP hOldBitmap1 = (HBITMAP)::SelectObject(memDc.GetSafeHdc(), bmpIn->GetSafeHandle()); 
	HBITMAP hOldBitmap2 = (HBITMAP)::SelectObject(targetDc.GetSafeHdc(), hTargetBitmap); 

	//4.3 Copy source bitmap into the target one target

	targetDc.BitBlt(0, 0, bmp.bmWidth, bmp.bmHeight, &memDc, 0, 0, SRCCOPY); 

	//4.4 Restore device contexts 

	::SelectObject(memDc.GetSafeHdc(), hOldBitmap1); 
	::SelectObject(targetDc.GetSafeHdc(), hOldBitmap2); 

	memDc.DeleteDC(); 
	targetDc.DeleteDC();

	bmpOut->Attach(hTargetBitmap);
}

void CTBarGenAddinDlg::OnBnClickedButtonSave()
{
	CBitmap bmpToolbar;
	RGBQUAD* ppal = NULL;
	int nColors,nBits;
	CString strFileName;
	OnBnClickedButtonRefresh();

	if (m_toolbar.id.IsEmpty() || m_bitmap.id.IsEmpty())
		return;
	GetImageFromList(m_imageList,&bmpToolbar);
	m_edit_bmpfile.GetWindowText(strFileName);


	CxImage ximg;
	ximg.CreateFromHBITMAP(HBITMAP(bmpToolbar.GetSafeHandle()));
	
	switch (m_combo_bpp.GetCurSel())
	{
	case 0: // 16 colors
		{
			nBits	= 4;
			nColors = 16;
			break;
		}
	case 1: // 256 colors
		{
			nBits	= 8;
			nColors = 256;
			break;
		}
	case 2: // true colors
		{
			nBits	= 24;
			nColors = 0;
			break;
		}
	default:
		nBits	= 4;
		nColors = 16;
	}

	if (nColors > 0)
	{
		CQuantizer q(nColors,nBits);
		q.ProcessImage(ximg.GetDIB());
		ppal=(RGBQUAD*)calloc(nColors*sizeof(RGBQUAD),1);
		q.SetColorTable(ppal);
		ximg.DecreaseBpp(nBits,true,ppal,nColors);
		free(ppal);
	}
	
	if (ximg.Save(strFileName,CXIMAGE_FORMAT_BMP))
	{
		AfxMessageBox("Bitmap saved",MB_ICONINFORMATION | MB_OK);
	}
}

void CTBarGenAddinDlg::OnNMRclickListButtons(NMHDR *pNMHDR, LRESULT *pResult)
{
	CPoint point;
	GetCursorPos(&point);

	BCMenu *Pop;
	m_floatPop.LoadMenu(IDR_MENU_POPUP);
	m_floatPop.LoadToolTCbar(IDR_MENUTOOLBARHELPER,IDB_BITMAP_TBHELPER_TC);
	Pop=(BCMenu *)m_floatPop.GetSubMenu(0);
	Pop->TrackPopupMenu(0,point.x,point.y,this,NULL);
	m_floatPop.DestroyMenu();
}



void CTBarGenAddinDlg::OnBnClickedButtonGenerate()
{
	OnBnClickedButtonRefresh();
	if (m_bIsNewToolbar)
	{
		if ((!WriteNewToolBarToResourceFile(m_strResourceFile)
			|| (!WriteIdsToResource(m_strIdsFile))))
		{
			AfxMessageBox("Can't update resource file!",MB_ICONEXCLAMATION|MB_OK);
			return;
		}
	}
	else
	{
		if ((!WriteToolBarToResourceFile(m_strResourceFile,m_strResourceName)
			|| (!WriteIdsToResource(m_strIdsFile))))
		{
			AfxMessageBox("Can't update resource file!",MB_ICONEXCLAMATION|MB_OK);
			return;
		}
	}

	if (m_bReplaceOFile)
	{
		// make a backup copy
		if (CopyFile(m_strResourceFile,m_strResourceFile+".old",FALSE) &&
			CopyFile(m_strIdsFile,m_strIdsFile+".old",FALSE))
		{
			if(m_bReplaceOFile)
			{
				CopyFile(m_strTmpDir+"tmp_res.txt",m_strResourceFile,FALSE);
				CopyFile(m_strTmpDir+m_strResourceIncFile,m_strIdsFile,FALSE);
			}
		}
		else
		{
			AfxMessageBox("Can't make a backup copy: aborting",MB_ICONEXCLAMATION|MB_OK);
			return;
		}
	}

	// Generate bitmap file
	
	CBitmap bmpToolbar;
	RGBQUAD* ppal = NULL;
	int nColors,nBits;
	CString strFileName;

	GetImageFromList(m_imageList,&bmpToolbar);
	strFileName = m_bitmap.path;

	CxImage ximg,ximg_tc,ximg_gr,ximg_ht;
	ximg.CreateFromHBITMAP(HBITMAP(bmpToolbar.GetSafeHandle()));
	ximg_tc.CreateFromHANDLE(ximg.CopyToHandle());
	ximg_gr.CreateFromHANDLE(ximg.CopyToHandle());
	ximg_ht.CreateFromHANDLE(ximg.CopyToHandle());
	// 16 colors
	nBits	= 4;
	nColors = 16;
	
	CQuantizer q(nColors,nBits);
	q.ProcessImage(ximg.GetDIB());
	ppal=(RGBQUAD*)calloc(nColors*sizeof(RGBQUAD),1);
	q.SetColorTable(ppal);
	ximg.DecreaseBpp(nBits,false,ppal,nColors);
	free(ppal);
	
	if (!ximg.Save(strFileName,CXIMAGE_FORMAT_BMP))
	{
		AfxMessageBox("Can't save bitmap",MB_ICONINFORMATION | MB_OK);
		return;
	}
	
	if (m_check_grbmp.GetCheck())
	{
		ximg_gr.GrayScale();
		if (!ximg_gr.Save(m_strGrBmpFile,CXIMAGE_FORMAT_BMP))
		{
			AfxMessageBox("Can't save grayed bitmap",MB_ICONINFORMATION | MB_OK);
			return;
		}
	}

	if (m_check_tcbmp.GetCheck())
	{
		if (!ximg_tc.Save(m_strTcBmpFile,CXIMAGE_FORMAT_BMP))
		{
			AfxMessageBox("Can't save true color bitmap",MB_ICONINFORMATION | MB_OK);
			return;
		}
	}
	
	if (m_check_htbmp.GetCheck())
	{
		int iContrast;
		CString strTMP;
		RGBQUAD oldColor,newColor,tmpColor;

		m_static_cnt.GetWindowText(strTMP);
		iContrast = atol(strTMP);
		//this will be a little tricky:
		oldColor = ximg_ht.GetPixelColor(0,0); // first.. get first pixel color (used as transparent one)
		ximg_ht.Light(0,iContrast); //then change contrast 75% more...
		newColor = ximg_ht.GetPixelColor(0,0); // get new color
		for(long y=0;y< (long)ximg_ht.GetHeight();y++) //and replace it!
			for(long x=0;x<(long)ximg_ht.GetWidth();x++)
			{
				tmpColor = ximg_ht.GetPixelColor(x,y);
				if ((tmpColor.rgbBlue == newColor.rgbBlue)&&
					(tmpColor.rgbGreen == newColor.rgbGreen) &&
					(tmpColor.rgbRed == newColor.rgbRed))
				{
					ximg_ht.SetPixelColor(x,y,oldColor);
				}
			}

		if (!ximg_ht.Save(m_strHtBmpFile,CXIMAGE_FORMAT_BMP))
		{
			AfxMessageBox("Can't save \"hot\" bitmap",MB_ICONINFORMATION | MB_OK);
			return;
		}

	}

	AfxMessageBox("Toolbar correctly merged with project!",MB_OK|MB_ICONINFORMATION);

}

BOOL CTBarGenAddinDlg::WriteToolBarToResourceFile(CString strResourceFile,CString strResourceID)
{
	CString strLine;
	CStdioFile fin,fout;
	CString strTMP;

	BOOL bFlag;
	int nCount		=0;
	int nResLength	=0;
	char ch;
	try
	{

		if (!fin.Open(strResourceFile,CFile::modeRead | CFile::typeText))
			return FALSE;

		if (!fout.Open(m_strTmpDir+"tmp_res.txt",CFile::modeCreate|CFile::modeWrite| CFile::typeText))
			return FALSE;

		CString	strToolbarHead;
		nResLength = strResourceID.GetLength();
		while (fin.ReadString(strLine))
		{
			strTMP = strLine.Left(nResLength); //I need one more char...
			if (strLine.GetLength() > nResLength)
				ch = strLine[nResLength];   //...the last one to avoid partial string match
			else
				ch = 0;
			if ((strTMP.Compare(strResourceID) == 0)
				&& ((ch == ' ') || (ch == '\t'))) //good separators are space and tab
			{
				nCount = strLine.Find("TOOLBAR",nResLength);
				
				if(nCount >=0)
				{
					bFlag = TRUE;
					strToolbarHead = strLine;
					while(bFlag)
					{
						if(fin.ReadString(strLine))
						{
							if (strLine == "END") //move "fin" to the end of toolbar..
							{
								bFlag = FALSE;
							}
						}
						else
						{
							fin.Close();
							fout.Close();
							bFlag = FALSE; // something was wrong!
							return FALSE;
						}
					} //while(!bFlag)

					if (strToolbarHead.IsEmpty())
					{
						strTMP.Format("%s\tTOOLBAR\t%d,%d\n",
							strResourceID,m_toolbar.rx,m_toolbar.ry);
						fout.WriteString(strTMP);
					}
					else
					{
						fout.WriteString(strToolbarHead+"\n");
					}
					fout.WriteString("BEGIN\n");
					for(UINT nBtn =0;nBtn<m_toolbar.buttons.size();nBtn++)
					{
						if (m_toolbar.buttons[nBtn].type == "BUTTON")
						{
							strTMP.Format("\tBUTTON\t%s\n",
								m_toolbar.buttons[nBtn].id);
						}
						else
						{
							strTMP = "\tSEPARATOR\n";
						}

						fout.WriteString(strTMP);
					}
					fout.WriteString("END\n");
				}
				else
				{
					fout.WriteString(strLine+"\n");
				}
			}
			else
			{
				fout.WriteString(strLine+"\n");
			}
		}
		fin.Close();
		fout.Close();

	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();
		return FALSE;
	}

	return TRUE;
}

BOOL CTBarGenAddinDlg::WriteNewToolBarToResourceFile(CString strResourceFile)
{
	CString strLine,strMarker,strLastLine;
	CStdioFile fin,fout;
	CString strTMP,strToolbarHead,strBitmapHead;
	CPath pathHelper;
	BOOL bFlag;
	BOOL bToolbarWritten;
	try
	{
		strMarker		= IDS_TOOLBAR_RCMARKER;
		bToolbarWritten = FALSE;

		if (!fin.Open(strResourceFile,CFile::modeRead | CFile::typeText))
			return FALSE;

		if (!fout.Open(m_strTmpDir+"tmp_res.txt",CFile::modeCreate|CFile::modeWrite| CFile::typeText))
			return FALSE;

		while (fin.ReadString(strLine))
		{
			if ((strLine.Compare(strMarker) == 0) && 
				!bToolbarWritten) //look for marker
			{
				bFlag = TRUE;
				fout.WriteString(strMarker+"\n"); //we need the marker! :)
				while(bFlag)  //move "fin" to the end of comments
				{
					if(fin.ReadString(strLine))
					{
						strTMP = strLine.Left(2);
						if (strLine == "//")
						{
							bFlag = FALSE;
							fout.WriteString(strLine+"\n");
						}
						else
						{
							strLastLine = strLine; //put to memory.. so I can write on fout
							bFlag = TRUE;
						}
					}
					else
					{
						fin.Close();
						fout.Close();
						bFlag = FALSE; // something was wrong!
						return FALSE;
					}
				} //while(!bFlag)

				pathHelper.SetPath(m_bitmap.path); //just 'couse i'm lazy 
				switch(m_toolbar.iType) //switch between  types 
				{
				case ID_VS71:
					{
						strToolbarHead.Format("%s\tTOOLBAR\t%d,%d",
							m_toolbar.id,m_toolbar.rx,m_toolbar.ry);
						strBitmapHead.Format("%s\tBITMAP\t\"res\\\\%s\"",
							m_bitmap.id,pathHelper.GetFileName());
						break;
					}
				case ID_VS6:
					{
						strToolbarHead.Format("%s\tTOOLBAR DISCARDABLE\t%d,%d",
							m_toolbar.id,m_toolbar.rx,m_toolbar.ry);
						strBitmapHead.Format("%s\tBITMAP  MOVEABLE PURE\t\"res\\\\%s\"",
							m_bitmap.id,pathHelper.GetFileName());
						break;
					}					
				default:
					strToolbarHead.Format("%s\tTOOLBAR\t%d,%d",
						m_toolbar.id,m_toolbar.rx,m_toolbar.ry);
					strBitmapHead.Format("%s\tBITMAP\t\"res\\\\%s\"",
						m_bitmap.id,pathHelper.GetFileName());
				}
				
				
				fout.WriteString(strBitmapHead+"\n\n"); //write bitmap: yup I know this isn't the right section.. 
				fout.WriteString(strToolbarHead+"\n");	//and then write the toolbar 
				fout.WriteString("BEGIN\n");
				for(UINT nBtn =0;nBtn<m_toolbar.buttons.size();nBtn++)
				{
					if (m_toolbar.buttons[nBtn].type == "BUTTON")
					{
						strTMP.Format("\tBUTTON\t%s\n",
							m_toolbar.buttons[nBtn].id);
					}
					else
					{
						strTMP = "\tSEPARATOR\n";
					}

					fout.WriteString(strTMP);
				}
				fout.WriteString("END\n");

				fout.WriteString(strLastLine+"\n");
				
				bToolbarWritten = TRUE; //this will avoid double toolbars when there are more marker
			}
			else
			{
				fout.WriteString(strLine+"\n");
			}
		}
		fin.Close();
		fout.Close();

	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();
		return FALSE;
	}

	return TRUE;
}
BOOL CTBarGenAddinDlg::ReadIds( CString strFile,CString strResourceFile,CSTRINGVECTOR &strIDS )
{
	CString strLine;
	CStdioFile fResIn;
	CSTRINGVECTOR strToolbars;
	CSTRINGVECTOR strTMPIDs;

	strIDS.clear();
	try
	{
		if (!fResIn.Open(strResourceFile,CFile::modeRead|CFile::typeText))
		{
			AfxMessageBox("Cannot open resource file.\nAborting",
				MB_ICONERROR|MB_OK);
			return FALSE;
		}
		if(!IsFile(strResourceFile))
		{
			CString strError;
			strError.Format("Can't find %s.\nTBE need this file to parse IDs.",
				strFile);
			AfxMessageBox(strError,MB_ICONERROR,MB_OK);
			return FALSE;
		}

		while (fResIn.ReadString(strLine))
		{
			//Odd filter but quick and dirty :)
			if (strLine.Find(" TOOLBAR ") >= 0)
				strToolbars.push_back(strLine);

		}
		fResIn.Close();

		ReadIds(strFile,strTMPIDs);
		
		for(size_t i=0;i<strTMPIDs.size();i++)
		{
			for(size_t j=0;j<strToolbars.size();j++)
				if (strToolbars[j].Find(strTMPIDs[i])>=0)
				{
					//avoid duplicates
					CSTRINGVECTORIT it;
					if ((it =std::find(strIDS.begin(),strIDS.end(),strTMPIDs[i])) == strIDS.end())
					{
						strIDS.push_back(strTMPIDs[i]);
					}
				}
		}
	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();
		fResIn.Close();
		return FALSE;
	}

	return TRUE;
}

BOOL CTBarGenAddinDlg::ReadIds( CString strFile,CSTRINGVECTOR &strIDS )
{
	CString strLine;
	CStdioFile fin;
	CString strTMP;



	int nCount		=0;
	int nResLength	=0;

	strIDS.clear();

	if(!IsFile(strFile))
	{
		CString strError;
		strError.Format("Can't find %s.\nTBE need this file to parse IDs.",
			strFile);
		AfxMessageBox(strError,MB_ICONERROR,MB_OK);
		return FALSE;
	}

	try
	{
		if (!fin.Open(strFile,CFile::modeRead | CFile::typeText))
		{
			AfxMessageBox("Cannot open resource.h file.\nAborting",
				MB_ICONERROR|MB_OK);
			return FALSE;
		}
			

		while (fin.ReadString(strLine))
		{
			strTMP = strLine.Left(7); //Lenght of "#define"
			if (strTMP.Compare("#define") == 0)
			{
				strTMP = strLine.Right(strLine.GetLength()-7);
				strTMP.Trim();
				nCount = strTMP.FindOneOf(" \t");
				if (nCount > 0) 
				{
					strTMP = strTMP.Left(nCount);
					strTMP.Trim();
					strIDS.push_back(strTMP);

				}
			}
		}

		fin.Close();

	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();
		fin.Close();
		return FALSE;
	}
	return TRUE;
}

void CTBarGenAddinDlg::OnAboutAbouttbargenaddin()
{
	CAboutDlg dlg;
	dlg.DoModal();
}

void CTBarGenAddinDlg::OnBnClickedButtonScanresource()
{
	CString resourceName;

	if (m_bIsNewToolbar)
	{
		if(AfxMessageBox("You are currently working on a new toolbar.\nDo you want really scan resource?",
						 MB_YESNO,MB_ICONQUESTION) == IDNO)
		{
			return;
		}
	}
	m_combo_resourceName.GetWindowText(resourceName);
	m_list_buttons.DeleteAllItems();

	if(!OpenResource(m_strResourceFile,resourceName))
	{
		AfxMessageBox("Cannot scan resource file",MB_ICONEXCLAMATION|MB_OK);
		return;
	}
	m_strResourceName	= resourceName;
	OnBnClickedButtonRefresh();
}

BOOL CTBarGenAddinDlg::FileOpen(CString strFileName)
{

	if (strFileName.IsEmpty())
	{
		CString strFilters = "Resource File (*.rc)|*.rc|Bitmap Files (*.bmp)|*.bmp||";
		CFileDialog fileDlg(TRUE, "Resource File", "*.rc",
			OFN_FILEMUSTEXIST| OFN_HIDEREADONLY, strFilters, this);
		if( fileDlg.DoModal ()==IDOK )
		{
			strFileName = fileDlg.GetPathName();
		}
		else
		{
			return FALSE;
		}
	}


	ResetContent(); //Reset to default

	CString fileName;
	CPath	path;

	path.SetPath(strFileName);
	if(path.GetExtension().CompareNoCase(".rc")==0)
	{
		fileName.Format("Resource file: %s",path.GetFileName());	
		m_strResourceFile	= strFileName;
		m_strIdsFile		= path.GetLocation()+m_strResourceIncFile;	
		m_iDocType			= ID_RESDOC;
		CSTRINGVECTOR vIds;
		if (ReadIds(m_strIdsFile,m_strResourceFile,vIds))
		{
			for (UINT i = 0;i<vIds.size();i++)
			{
				m_combo_resourceName.AddRow(vIds[i]);
			}
		}
	}
	else
	{
		fileName.Format("Imagelist file: %s",path.GetFileName());
		m_iDocType			= ID_BMPDOC;
	}
	
	SetWindowText(fileName);
	AfxGetApp()->AddToRecentFileList(m_strResourceFile);

	InitMRUList(); //Refresh MRU menu


	return TRUE;

}

void CTBarGenAddinDlg::OnFileOpen()
{
	CDlgNewToolbar dlg;
	CString strTMP;
	if(FileOpen())
	{
		switch (m_iDocType)
		{
		case ID_RESDOC:
			strTMP.Format("%d",m_toolbar.rx);
			m_edit_rx.SetWindowText(strTMP);
			strTMP.Format("%d",m_toolbar.ry);
			m_edit_ry.SetWindowText(strTMP);

			m_edit_rx.EnableWindow(FALSE);
			m_edit_ry.EnableWindow(FALSE);
			m_button_scanresource.EnableWindow(TRUE);
			break;
		case ID_BMPDOC:
			m_button_scanresource.EnableWindow(FALSE);
			m_edit_rx.EnableWindow(TRUE);
			m_edit_ry.EnableWindow(TRUE);
			dlg.SetDocType(ID_RESDOC);
			dlg.DoModal();

			break;
		default:
			break;
		}
	}
	

}

LRESULT CTBarGenAddinDlg::OnEditComplete(WPARAM wParam,LPARAM lParam)
{
	CString strText;
	int row,col;

	row = (int) wParam;
	col = (int) lParam;
	
	strText = m_list_buttons.GetItemText(row,col);
	switch(col) 
	{
	case 0:		//Type
		{
			m_toolbar.buttons[row].type = strText;
			if (strText.Compare("SEPARATOR") == 0)
			{
				m_list_buttons.SetItemText(row,1,_T(""));
				m_list_buttons.SetItemText(row,2,_T(""));
				m_list_buttons.SetItemImage(row,2,-1);
				m_toolbar.buttons[row].id = "";
				m_toolbar.buttons[row].imgPath = "";
				m_toolbar.buttons[row].nPosImg = -1;
			}
			break;
		}
	case 1:		//ID
		{
			if (m_toolbar.buttons[row].type == "BUTTON")
			{
				strText.Trim();
				strText.Replace(" ","_");
				m_toolbar.buttons[row].id		= strText;
				m_toolbar.buttons[row].bChkId	= TRUE;
			}
			break;
		}
	case 2:		//Image Path
		{
			if (m_toolbar.buttons[row].type == "BUTTON")
			{
				m_toolbar.buttons[row].imgPath = strText;
			}
			break;
		}
	default:
		return false;
		break;
	}

	return true;
}

BOOL CTBarGenAddinDlg::ResizeImage(CString strFileName, CString &strDestFile)
{
	CxImage img;
	CPath filePath;
	filePath.SetPath(strFileName);

	if(img.Load(strFileName))
	{
		img.Resample(m_toolbar.rx,m_toolbar.ry);
		strDestFile = m_strTmpDir+filePath.GetFileName();
		return img.Save(strDestFile,CXIMAGE_FORMAT_BMP);
	}
	return FALSE;
}


void CTBarGenAddinDlg::OnEditimageResize()
{
	CString strFilePath,strFilePathOut;
	int nPos = m_list_buttons.GetCurSel();
	if (nPos == -1)
		return;

	strFilePath = m_list_buttons.GetItemText(nPos,2);
	if (ResizeImage(strFilePath,strFilePathOut))
	{
		m_list_buttons.SetItemText(nPos,2,strFilePathOut);
		m_toolbar.buttons[nPos].imgPath = strFilePathOut;
	}
}
void CTBarGenAddinDlg::OnImageResize()
{
	OnEditimageResize();
}

LRESULT CTBarGenAddinDlg::OnSmallButtonClicked(WPARAM wParam,LPARAM lParam)
{
	CString strFilters = "Bitmap File (*.bmp)|*.bmp|Gif File (*.gif)|*.gif|Jpg File (*.jpg)|*.jpg|Tiff File (*.tiff)|*.tiff|All Files (*.*)|*.*||";

	CFileDialog fileDlg(TRUE, "Bitmap File", "*.bmp",
		OFN_HIDEREADONLY, strFilters, this);


	if( fileDlg.DoModal ()==IDOK )
	{

		int nPos = (int) wParam;
		CString fileName = fileDlg.GetPathName();
		m_list_buttons.SetItemText(nPos,2,fileName);
		m_toolbar.buttons[nPos].imgPath = fileName;
		OnBnClickedButtonRefresh();
	}

	return 1;
}
void CTBarGenAddinDlg::OnPopupAddnew()
{
	CString strTMP;
	int nPos = m_list_buttons.GetItemCount();
	strTMP.Format("IDR_BUTTON_%d",nPos);
	
	st_toolbar_button tmp_button;
	
	tmp_button.type		= "BUTTON";
	tmp_button.id		= strTMP;
	tmp_button.imgPath	= "";
	tmp_button.nPosImg	= -1;
	tmp_button.bChkId	= TRUE;

	m_list_buttons.InsertItem(nPos,"",-1);
	m_list_buttons.SetItemText(nPos,0,tmp_button.type);
	m_list_buttons.SetItemText(nPos,1,tmp_button.id);

	m_toolbar.buttons.push_back(tmp_button);
}

void CTBarGenAddinDlg::OnPopupInsertnew()
{
	CString strTMP;
	int nPos = m_list_buttons.GetCurSel();
	strTMP.Format("IDR_BUTTON_%d",
		m_toolbar.buttons.size()); 

	st_toolbar_button tmp_button;

	tmp_button.type		= "BUTTON";
	tmp_button.id		= strTMP;
	tmp_button.imgPath	= "";
	tmp_button.nPosImg	= -1;
	tmp_button.bChkId	= TRUE;

	m_list_buttons.InsertItem(nPos,"",-1);
	m_list_buttons.SetItemText(nPos,0,tmp_button.type);
	m_list_buttons.SetItemText(nPos,1,tmp_button.id);

	m_toolbar.buttons.insert(m_toolbar.buttons.begin()+nPos,tmp_button);
}

void CTBarGenAddinDlg::OnPopupDelete()
{
	int nPos = m_list_buttons.GetCurSel();
	if (nPos <0)
		return;

	if (AfxMessageBox("Do you want to delete the selected item?",MB_ICONQUESTION|MB_YESNO) == IDYES)
	{
		m_list_buttons.DeleteItem(nPos,TRUE);
		m_toolbar.buttons.erase(m_toolbar.buttons.begin()+nPos);
		OnBnClickedButtonRefresh();
		int nBtnCount = m_list_buttons.GetItemCount();
		if (nPos >= nBtnCount)
			nPos = nBtnCount;

		m_list_buttons.SetCurSel(nPos);
		m_list_buttons.SetFocus();
	}
}

BOOL CTBarGenAddinDlg::ReadIdsFiles(CSTRINGVECTOR& vfiles)
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
			vfiles.push_back(path.GetLocation()+"afxres.h");
			return TRUE;
		}

		while(fin.ReadString(strLine))
		{
			strLine.Trim();
			vfiles.push_back(strLine);
		}
		fin.Close();
	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();
		return TRUE;
	}
	return TRUE;
}

BOOL CTBarGenAddinDlg::WriteIdsToResource(CString strFile)
{
	CString strLine;
	CStdioFile fin,fout;
	CString strTMP;
	CSTRINGVECTOR vIds;
	CSTRINGVECTOR vIdsFiles;


	int nCount			=0;
	int nResLength		=0;
	long nStartIds		=0;
	long nStartResIds	=0;
	long nCurrentId		=0;
	long nCurrentResIds =0;

	try
	{
		if(m_iDocType != ID_RESDOC)
			return FALSE;

		if (!ReadIdsFiles(vIdsFiles))
			return FALSE;

		if (!ReadIds(m_strIdsFile,vIds))
			return FALSE;

		for (size_t j=0;j<vIdsFiles.size();j++)
		{
			if (!ReadIds(vIdsFiles[j],vIds))
			{
				CString strMsg;
				strMsg.Format("Can't open %s file for IDs parsing.\nDo you want to abort?",vIdsFiles[j]);
				if(AfxMessageBox(strMsg,MB_ICONQUESTION|MB_YESNO) == IDYES)
					return FALSE;
			}
		}

		if (!fin.Open(strFile,CFile::modeRead | CFile::typeText))
		{
			CString strError;
			strError.Format("Can't open %s",strFile);
			theApp.LogError(strError,CTBarGenAddinApp::ERROR_HIGH);
			return FALSE;
		}

		if (!fout.Open(m_strTmpDir+m_strResourceIncFile,CFile::modeCreate|CFile::modeWrite | CFile::typeText))
		{
			CString strError;
			strError.Format("Can't open %s",m_strTmpDir+m_strResourceIncFile);
			theApp.LogError(strError,CTBarGenAddinApp::ERROR_HIGH);
			return FALSE;
		}

		// scan for new command id value
		while (fin.ReadString(strLine)) 
		{
			if ((nCount=strLine.Find("_APS_NEXT_COMMAND_VALUE") )> 0)
			{
				strTMP = strLine.Right(strLine.GetLength()-(nCount+24));
				strTMP.Trim();
				nStartIds = atol(strTMP);
			}

			if ((nCount=strLine.Find("_APS_NEXT_RESOURCE_VALUE" ))>0)
			{
				strTMP = strLine.Right(strLine.GetLength()-(nCount+24));
				strTMP.Trim();
				nStartResIds = atol(strTMP);
			}
		}
		if (!nStartIds)
		{
			// we don't found a value...
			CString strError;
			strError.Format("%s is malformed. TBE need both _APS_NEXT_COMMAND_VALUE and _APS_NEXT_RESOURCE_VALUE defined!",m_strResourceIncFile);
			theApp.LogError(strError,CTBarGenAddinApp::ERROR_HIGH);

			fin.Close();
			fout.Close();
			return FALSE;
		}

		fin.SeekToBegin();
		nCurrentId = nStartIds;
		nCurrentResIds = nStartResIds;
		while (fin.ReadString(strLine))
		{
			if (strLine.Compare("// Next default values for new objects") == 0)
			{
				if (m_bIsNewToolbar) //check for bitmap & toolbar id..
				{
					//check bitmap  && toolbar id
					CSTRINGVECTORIT fnd;
					fnd = find(vIds.begin(), vIds.end(), m_bitmap.id);
					if (fnd == vIds.end())
					{
						CString strNewId;
						strNewId.Format("#define %s        %d\n",
							m_bitmap.id,nCurrentResIds);
						fout.WriteString(strNewId);
						vIds.push_back(m_bitmap.id);	//now is defined!
						nCurrentResIds++;
					}

				}

				for (UINT nBtn =0;nBtn < m_toolbar.buttons.size();nBtn++)
				{
					if ((m_toolbar.buttons[nBtn].type == "BUTTON")
						&&(m_toolbar.buttons[nBtn].bChkId))
					{
						CSTRINGVECTORIT fnd;
						strTMP = m_toolbar.buttons[nBtn].id;
						fnd = find(vIds.begin(), vIds.end(), strTMP);
						if (fnd == vIds.end())
						{
							CString strNewId;
							strNewId.Format("#define %s        %d\n",
								strTMP,nCurrentId);
							fout.WriteString(strNewId);
							vIds.push_back(strTMP); //now is defined!
							nCurrentId++;
						}
					}
				}

				fout.WriteString("\n"); 
				fout.WriteString("// Next default values for new objects\n");
			}
			else
			{
				if (strLine.Find("_APS_NEXT_COMMAND_VALUE") > 0)
				{
					CString strOld,strNew;
					nCurrentId+=1;
					strOld.Format("%d",nStartIds);
					strNew.Format("%d",nCurrentId);
					strLine.Replace(strOld,strNew);
				}
				if (strLine.Find("_APS_NEXT_RESOURCE_VALUE") > 0)
				{
					CString strOld,strNew;
					nCurrentResIds+=1;
					strOld.Format("%d",nStartResIds);
					strNew.Format("%d",nCurrentResIds);
					strLine.Replace(strOld,strNew);
				}
				fout.WriteString(strLine+"\n");
			}
		}

		fin.Close();
		fout.Close();

	}
	catch (CFileException *e) 
	{
		e->ReportError();
		e->Delete();

		CString strError;
		strError.Format("Error in WriteIdsToResource function");
		theApp.LogError(strError,CTBarGenAddinApp::ERROR_DEBUG);

		return FALSE;
	}
	return TRUE;
}
void CTBarGenAddinDlg::OnToolsOptions()
{
	CDlgOptions dlg;
	dlg.DoModal();
}


void CTBarGenAddinDlg::OnLButtonUp(UINT nFlags, CPoint point)
{
	if (m_bDragging)
	{ 
		ReleaseCapture ();
		m_bDragging = FALSE;
		m_libImageList->DragLeave (GetDesktopWindow ());
		m_libImageList->EndDrag ();
		
		CPoint pt (point); 
		ClientToScreen (&pt); 
		CWnd* pDropWnd = WindowFromPoint (pt);
		ASSERT (pDropWnd); 
		if (pDropWnd->IsKindOf(RUNTIME_CLASS(CListCtrl))
			&& m_pDragWnd->IsKindOf(RUNTIME_CLASS(CListCtrl)))
		{
			((CReportCtrl*)pDropWnd)->SetItemState (m_nDropIndex, 0, LVIS_DROPHILITED);
			m_pDropWnd =  pDropWnd; 
			DropItemOnList((CReportCtrl*)m_pDragWnd, (CReportCtrl*) m_pDropWnd); 
	
		}
	}

	m_DragImageList = NULL; //reset :)
	CMagDialog::OnLButtonUp(nFlags, point);
}

void CTBarGenAddinDlg::LoadImgLibrary(CString strDirectory)
{
	CxImage			img;
	CBitmap			bmp;
	COLORREF		rgbColorMask;
	HANDLE			hSearch; 
	int				irx,iry;
	RGBQUAD			rgbq;
	WIN32_FIND_DATA FileData; 

	irx = iry = 15;
	BOOL fFinished = FALSE;

	m_list_library.DeleteAllItems();
	m_libImageList->DeleteImageList();
	m_libImageList->Create(irx, iry,ILC_MASK | ILC_COLOR32,0,0);
	m_list_library.SetImageList(m_libImageList);

	hSearch = FindFirstFile(strDirectory+"*.*", &FileData); 
	if (hSearch == INVALID_HANDLE_VALUE) 
	{ 
		return;
	} 

	int nPos	= 0;
	int imgPos	= 0;
	while (!fFinished) 
	{ 
		if (!(FileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
		{
			CPath	filePath;
			BOOL	bImage	= FALSE;
			DWORD	imgType = 0;
			filePath.SetPath(strDirectory+FileData.cFileName);

			if (filePath.GetExtension() == ".gif")
			{
				bImage = TRUE;
				imgType = CXIMAGE_FORMAT_GIF;
			}

			if (filePath.GetExtension() == ".bmp")
			{
				bImage = TRUE;
				imgType = CXIMAGE_FORMAT_BMP;
			}

			if (filePath.GetExtension() == ".jpg")
			{
				bImage = TRUE;
				imgType = CXIMAGE_FORMAT_JPG;
			}
			if (filePath.GetExtension() == ".png")
			{
				bImage = TRUE;
				imgType = CXIMAGE_FORMAT_PNG;
			}
			if (bImage)
				if (img.Load(strDirectory+FileData.cFileName,imgType))
				{
					if (img.GetBpp() != 24)
						img.IncreaseBpp(24);

					img.Resample(irx,iry);
					rgbq = img.GetPixelColor(0,0,false);
					rgbColorMask = RGB(rgbq.rgbRed,rgbq.rgbGreen,rgbq.rgbBlue);
					bmp.Attach(img.MakeBitmap());
					imgPos = m_list_library.GetImageList()->Add(&bmp,rgbColorMask);

					nPos = m_list_library.GetItemCount();
					m_list_library.InsertItem(nPos,"",imgPos);
					m_list_library.SetItemText(nPos,0,FileData.cFileName);
					m_list_library.SetItemImage(nPos,0,imgPos);

					img.Destroy();
					bmp.Detach();
				}
		}
		if (!FindNextFile(hSearch, &FileData)) 
		{
			fFinished = TRUE;
		}
	} 


	FindClose(hSearch);


}
void CTBarGenAddinDlg::OnStnClickedStaticImglib()
{
	LoadImgLibrary(m_strImgLib);
}

void CTBarGenAddinDlg::OnLvnBegindragListLibrary(NMHDR *pNMHDR, LRESULT *pResult)
{
		LPNMHEADER phdr = reinterpret_cast<LPNMHEADER>(pNMHDR);
		
		CPoint point;
		GetCursorPos(&point);
		ClientToScreen(&point);
		m_DragImageList = m_libImageList;
		m_DragImageList->BeginDrag(phdr->iItem, CPoint(10,10));
		m_DragImageList->DragEnter(GetDesktopWindow(), point);
	
		m_bDragging		= TRUE;    //we are in a drag and drop operation
		m_nDropIndex	= -1;    //we don't have a drop index yet
		m_nDragIndex	= phdr->iItem;
		m_pDragWnd		= &m_list_library; //make note of which list we are dragging from
		m_pDropWnd		= &m_list_library;    //at present the drag list is the drop list
	
		SetCapture();
	
		*pResult = 0;
}

void CTBarGenAddinDlg::OnLvnBegindragListButtons(NMHDR *pNMHDR, LRESULT *pResult)
{
		LPNMHEADER phdr = reinterpret_cast<LPNMHEADER>(pNMHDR);
		
		CPoint point;
		GetCursorPos(&point);
		ClientToScreen(&point);
		m_DragImageList = m_imageList;
		m_DragImageList->BeginDrag(m_toolbar.buttons[phdr->iItem].nPosImg, CPoint(10,10));
		m_DragImageList->DragEnter(GetDesktopWindow(), point);
	
		m_bDragging		= TRUE;    //we are in a drag and drop operation
		m_nDropIndex	= -1;    //we don't have a drop index yet
		m_nDragIndex	= phdr->iItem;
		m_pDragWnd		= &m_list_buttons; //make note of which list we are dragging from
		m_pDropWnd		= &m_list_buttons;    //at present the drag list is the drop list
	
		SetCapture();
	
		*pResult = 0;
}

void CTBarGenAddinDlg::DropItemOnList(CReportCtrl* pDragList,CReportCtrl* pDropList)
{
	if ((pDragList == &m_list_library) &&
		(pDropList == &m_list_buttons)) 
		{
			if ((GetKeyState(VK_CONTROL) < 0) &&	// Is ctrl key down?
				(m_nDropIndex >= 0))				// and is a valid drop index?
			{
				CString strFile; //Yes.. we must replace only the image of button
				st_toolbar_button tmp_button;
				if (m_toolbar.buttons[m_nDropIndex].type == "SEPARATOR")
					return;
				strFile = m_list_library.GetItemText(m_nDragIndex,0);
				if (LoadFromFile(m_strImgLib+strFile,&tmp_button,m_nDropIndex) >= 0)
				{
					tmp_button.id		= m_toolbar.buttons[m_nDropIndex].id; //copy old data to new button
					tmp_button.nPosImg	= m_toolbar.buttons[m_nDropIndex].nPosImg;
					tmp_button.bChkId	= m_toolbar.buttons[m_nDropIndex].bChkId;
					tmp_button.type		= m_toolbar.buttons[m_nDropIndex].type;
					m_toolbar.buttons.erase(m_toolbar.buttons.begin()+m_nDropIndex); //delete old button
					m_toolbar.buttons.insert(m_toolbar.buttons.begin()+m_nDropIndex,tmp_button); // insert new button
				}
			}
			else
			{

				CString strFile;
				st_toolbar_button tmp_button;
				strFile = m_list_library.GetItemText(m_nDragIndex,0);
				if (m_nDropIndex == -1)
				{
					if (m_bIsNewToolbar)
					{
						if (LoadFromFile(m_strImgLib+strFile,&tmp_button) >= 0)
						{
							m_toolbar.buttons.push_back(tmp_button);
						}
					}
					else
					{
						AfxMessageBox("You must create a new toolbar first.",
							MB_OK|MB_ICONINFORMATION);
						return;
					}
				}
				else
				{
					if (LoadFromFile(m_strImgLib+strFile,&tmp_button,m_nDropIndex) >= 0)
					{
						m_toolbar.buttons.insert(m_toolbar.buttons.begin()+m_nDropIndex,tmp_button);
					}
				}
			}
		}
	if ((pDragList == &m_list_buttons) && //Drag from button list
		(pDropList == &m_list_buttons) && // Drop on button list 
		(m_nDropIndex >= 0) && //valid drop index and..
		(m_nDragIndex != m_nDropIndex)) //.. not the same of drag 
		{ 
			NMHBT	nmh;
			nmh.nButtonFrom = m_nDragIndex;
			nmh.nButtonTo	= m_nDropIndex;
			OnTbButtonMove(0,(LPARAM)&nmh); //Yes I know... but in this way there is only one swap function ;)
		}

		OnBnClickedButtonRefresh();
		
}
void CTBarGenAddinDlg::OnBnClickedButtonTcopt()
{
	CString strFilters = "Bitmap File (*.bmp)|*.bmp|All Files (*.*)|*.*||";

	CFileDialog fileDlg(FALSE, "Bitmap File", "*.bmp",
		OFN_HIDEREADONLY, strFilters, this);


	if( fileDlg.DoModal ()==IDOK )
	{

		CString fileName = fileDlg.GetPathName();
		m_strTcBmpFile = fileName;
	}
}

void CTBarGenAddinDlg::OnBnClickedButtonGropt()
{
	CString strFilters = "Bitmap File (*.bmp)|*.bmp|All Files (*.*)|*.*||";

	CFileDialog fileDlg(FALSE, "Bitmap File", "*.bmp",
		OFN_HIDEREADONLY, strFilters, this);


	if( fileDlg.DoModal ()==IDOK )
	{

		CString fileName = fileDlg.GetPathName();
		m_strGrBmpFile = fileName;
	}
}

void CTBarGenAddinDlg::OnBnClickedButtonHtopt()
{
	CString strFilters = "Bitmap File (*.bmp)|*.bmp|All Files (*.*)|*.*||";

	CFileDialog fileDlg(FALSE, "Bitmap File", "*.bmp",
		OFN_HIDEREADONLY, strFilters, this);


	if( fileDlg.DoModal ()==IDOK )
	{

		CString fileName = fileDlg.GetPathName();
		m_strHtBmpFile = fileName;
	}
}

void CTBarGenAddinDlg::OnFileExit()
{
	EndDialog(IDOK);
}

void CTBarGenAddinDlg::OnFileSavebitmap()
{
	OnBnClickedButtonSave();
}

BOOL CTBarGenAddinDlg::OnToolTipText(UINT, NMHDR* pNMHDR, LRESULT* pResult)
{
	ASSERT(pNMHDR->code == TTN_NEEDTEXTA || pNMHDR->code == TTN_NEEDTEXTW);

	// allow top level routing frame to handle the message
	if (GetRoutingFrame() != NULL)
		return FALSE;

	// need to handle both ANSI and UNICODE versions of the message
	TOOLTIPTEXTA* pTTTA = (TOOLTIPTEXTA*)pNMHDR;
	TOOLTIPTEXTW* pTTTW = (TOOLTIPTEXTW*)pNMHDR;
	TCHAR szFullText[256];
	CString cstTipText;
	CString cstStatusText;

	UINT nID = (UINT)pNMHDR->idFrom;
	if (pNMHDR->code == TTN_NEEDTEXTA && (pTTTA->uFlags & TTF_IDISHWND) ||
		pNMHDR->code == TTN_NEEDTEXTW && (pTTTW->uFlags & TTF_IDISHWND))
	{
		// idFrom is actually the HWND of the tool
		nID = ((UINT)(WORD)::GetDlgCtrlID((HWND)nID));
	}

	if (nID != 0) // will be zero on a separator
	{
		AfxLoadString(nID, szFullText);
		// this is the command id, not the button index
		AfxExtractSubString(cstTipText, szFullText, 1, '\n');
		AfxExtractSubString(cstStatusText, szFullText, 0, '\n');
	}

	// Non-UNICODE Strings only are shown in the tooltip window...
	if (pNMHDR->code == TTN_NEEDTEXTA)
		lstrcpyn(pTTTA->szText, cstTipText,
		(sizeof(pTTTA->szText)/sizeof(pTTTA->szText[0])));
	else
		_mbstowcsz(pTTTW->szText, cstTipText,
		(sizeof(pTTTW->szText)/sizeof(pTTTW->szText[0])));
	*pResult = 0;

	// bring the tooltip window above other popup windows
	::SetWindowPos(pNMHDR->hwndFrom, HWND_TOP, 0, 0, 0, 0,
		SWP_NOACTIVATE|SWP_NOSIZE|SWP_NOMOVE);


	return TRUE;    // message was handled
}

void CTBarGenAddinDlg::OnAcceleratorCanc()
{
	if (&m_list_buttons == GetFocus()) //yup... I know.. isn't the best idea... 
	{
		OnPopupDelete();
	}
}

void CTBarGenAddinDlg::OnAcceleratorRefresh()
{
	OnBnClickedButtonRefresh();
}

void CTBarGenAddinDlg::OnFileNew()
{
	// Reset to default
	ResetContent();
	
	// We need a .rc file opened!
	if(m_strResourceFile.IsEmpty())
	{
		if (!FileOpen())
			return;
	}

	CDlgNewToolbar dlg;
	if (dlg.DoModal() == IDOK)
	{
		dlg.GetNewToolbar(m_toolbar);
		dlg.GetNewBitmap(m_bitmap);

		m_imageList->Create(m_toolbar.rx, m_toolbar.ry,ILC_MASK | ILC_COLOR32,0,50);
		m_list_buttons.SetImageList(m_imageList);

		CPath resPath;
		resPath.SetPath(m_strResourceFile);
		if (!IsDirectory(resPath.GetLocation()))
		{
			if (!CreateDirectory(resPath.GetLocation()+"res\\",NULL))
			{
				DWORD dw;
				if ((dw = GetLastError()) != ERROR_ALREADY_EXISTS)
				{
					LPVOID lpMsgBuf;
					if (!FormatMessage( 
						FORMAT_MESSAGE_ALLOCATE_BUFFER | 
						FORMAT_MESSAGE_FROM_SYSTEM | 
						FORMAT_MESSAGE_IGNORE_INSERTS,
						NULL,
						dw,
						MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
						(LPTSTR) &lpMsgBuf,
						0,
						NULL ))
					{
						AfxMessageBox("Error creating res directory!\nAborting.",MB_OK|MB_ICONERROR);
						return;
					}

					::MessageBox( NULL, (LPCTSTR)lpMsgBuf, "Error", MB_OK | MB_ICONINFORMATION );
					LocalFree( lpMsgBuf );
					return;
				}
			}
		}

		CString strBmpFileName,strTMP;
		strBmpFileName	= m_bitmap.id;
		strBmpFileName.MakeLower();
		
		m_bitmap.path	= resPath.GetLocation()+"res\\"+strBmpFileName+".bmp";

		m_strTcBmpFile = resPath.GetLocation()+"res\\"+strBmpFileName+"_TC.bmp";
		m_strGrBmpFile = resPath.GetLocation()+"res\\"+strBmpFileName+"_GR.bmp";
		m_strHtBmpFile = resPath.GetLocation()+"res\\"+strBmpFileName+"_HT.bmp";

		m_combo_resourceName.SetWindowText(m_toolbar.id);
		m_edit_bmpfile.SetWindowText(m_bitmap.path);
		strTMP.Format("%d",m_toolbar.rx);
		m_edit_rx.SetWindowText(strTMP);
		strTMP.Format("%d",m_toolbar.ry);
		m_edit_ry.SetWindowText(strTMP);

		m_bIsNewToolbar = TRUE;
	}
}

void CTBarGenAddinDlg::ResetContent(BOOL bResetIds)
{
	m_bIsNewToolbar = FALSE;
	
	m_imageList->DeleteImageList();
	m_list_buttons.DeleteAllItems();
	if (bResetIds)
		m_combo_resourceName.ResetContent();

	m_toolbar.buttons.clear();
	m_toolbar.id	= "";
	m_toolbar.rx	= 0;
	m_toolbar.ry	= 0;
	m_toolbar.iType = ID_VS71;

	m_bitmap.id		= "";
	m_bitmap.path	= "";

}

void CTBarGenAddinDlg::InitMRUList()
{
	//ID_FILE_MRU_FILE1
	BCMenu* pMenu = m_mainMenu.GetSubBCMenu("Recent File");
	CString strTMP;

	TCHAR szCurDir[_MAX_PATH];
	DWORD dwDirLen = GetCurrentDirectory(_MAX_PATH, szCurDir);
	if( dwDirLen == 0 || dwDirLen >= _MAX_PATH )
		return;	// Path too long

	int nCurDir = lstrlen(szCurDir);
	ASSERT(nCurDir >= 0);
	szCurDir[nCurDir] = '\\';
	szCurDir[++nCurDir] = '\0';


	if (pMenu) //This function is used to refresh also.. so.. the menu was created?
	{
		int numitems=pMenu->GetMenuItemCount(); //yup.. we have a submenu.. 
		for(int i=numitems-1;i>=0;--i)			// delete old list
			pMenu->DeleteMenu(i,MF_BYPOSITION);
		
		for (int nIndex =0;nIndex< ((CTBarGenAddinApp*)AfxGetApp())->GetMruSize();nIndex++) //and create a new one..
		{
			((CTBarGenAddinApp*)AfxGetApp())->GetMruDisplayName(strTMP,nIndex,szCurDir,nCurDir);
			if (!strTMP.IsEmpty()) // Only if is not empty
				pMenu->AppendMenu(MF_STRING,ID_FILE_MRU_FILE1+nIndex,strTMP);
		}
	}
	else
	{
		BCMenu* pFileMenu = (BCMenu*) m_mainMenu.GetSubMenu("&File");
		for (int nIndex =0;nIndex< ((CTBarGenAddinApp*)AfxGetApp())->GetMruSize();nIndex++)
		{
			((CTBarGenAddinApp*)AfxGetApp())->GetMruDisplayName(strTMP,nIndex,szCurDir,nCurDir);
			if (!strTMP.IsEmpty()) // Only if is not empty
				m_mruMenu.AppendMenu(MF_STRING,ID_FILE_MRU_FILE1+nIndex,strTMP);
		}
		pFileMenu->InsertMenu(ID_RECENTFILEPOS,MF_BYPOSITION | MF_POPUP, (UINT) m_mruMenu.m_hMenu, "Recent File");
	}
}
void CTBarGenAddinDlg::OnDestroy()
{
	CMagDialog::OnDestroy();
}

void CTBarGenAddinDlg::OnMRUList(UINT nID)
{
	CString strFile;
	strFile = ((CTBarGenAddinApp*)AfxGetApp())->GetMru(nID - ID_FILE_MRU_FILE1);
	FileOpen(strFile);
} 

void CTBarGenAddinDlg::OnAboutHelp()
{
	CDlgHelp dlg;
	dlg.DoModal();
}



void CTBarGenAddinDlg::OnToolbarCopytoclipboard()
{
	// prepare the toolbar text
	CString strToolbarText,strToolbarHead,strTMP;
	switch(m_toolbar.iType) //switch between  types 
	{
	case ID_VS71:
		{
			strToolbarHead.Format("%s\tTOOLBAR\t%d,%d",
				m_toolbar.id,m_toolbar.rx,m_toolbar.ry);
			break;
		}
	case ID_VS6:
		{
			strToolbarHead.Format("%s\tTOOLBAR DISCARDABLE\t%d,%d",
				m_toolbar.id,m_toolbar.rx,m_toolbar.ry);
			break;
		}					
	default:
		strToolbarHead.Format("%s\tTOOLBAR\t%d,%d",
			m_toolbar.id,m_toolbar.rx,m_toolbar.ry);
	}

	strToolbarText = strToolbarHead+"\n";
	strToolbarText += "BEGIN\n";
	for(UINT nBtn =0;nBtn<m_toolbar.buttons.size();nBtn++)
	{
		if (m_toolbar.buttons[nBtn].type == "BUTTON")
		{
			strTMP.Format("\tBUTTON\t%s\n",
				m_toolbar.buttons[nBtn].id);
		}
		else
		{
			strTMP = "\tSEPARATOR\n";
		}

		strToolbarText +=strTMP;
	}
	strToolbarText +="END\n";

	if (OpenClipboard()) //for this section you can refer to Tom Archer's article on codeproject
	{
		EmptyClipboard();
		HGLOBAL hClipboardData;
		hClipboardData = GlobalAlloc(GMEM_DDESHARE, 
			strToolbarText.GetLength()+1);

		char * pchData;
		pchData = (char*)GlobalLock(hClipboardData);
		strcpy(pchData, LPCSTR(strToolbarText));
		GlobalUnlock(hClipboardData);
		SetClipboardData(CF_TEXT,hClipboardData);
		CloseClipboard();
	}
}

void CTBarGenAddinDlg::OnToolbarMergeids()
{
	if (!WriteIdsToResource(m_strIdsFile))
	{
		AfxMessageBox("Can't update resource file!",MB_ICONEXCLAMATION|MB_OK);
		return;
	}
}

void CTBarGenAddinDlg::OnToolbarGenerate()
{
	OnBnClickedButtonGenerate();
}


void CTBarGenAddinDlg::OnAboutCheckforupdates()
{
	CDlgChkUpdates dlg;
	dlg.DoModal();
}

void CTBarGenAddinDlg::OnBnClickedButtonTest()
{
	CRect wRect,lRect,libRect;

	GetWindowRect(wRect);
	m_list_library.GetWindowRect(lRect);
	wRect.right = lRect.left-2;

	MoveWindow(wRect);

	m_dlgLibPreview.GetWindowRect(libRect);
	libRect.MoveToXY(wRect.right,(int)wRect.top+(wRect.bottom-wRect.top)/2-(libRect.bottom-libRect.top)/2);
	m_dlgLibPreview.MoveWindow(libRect);
	m_dlgLibPreview.ShowWindow(SW_SHOW);
}

//void CTBarGenAddinDlg::OnMoving(UINT fwSide, LPRECT pRect)
//{
//	CMagDialog::OnMoving(fwSide, pRect);
//
//	CRect libRect;
//	m_dlgLibPreview.GetWindowRect(libRect);
//	libRect.MoveToXY(pRect->right,(int)pRect->top+(pRect->bottom-pRect->top)/2-(libRect.bottom-libRect.top)/2);
//	m_dlgLibPreview.MoveWindow(libRect);
//}

void CTBarGenAddinDlg::OnBnClickedCheckMaskcorner()
{
	if(m_check_maskcorner.GetCheck() != BST_CHECKED)
	{
		m_button_maskcolor.EnableWindow(TRUE);
		m_check_maskcolor.SetCheck(BST_CHECKED);
	}
	else
	{
		m_button_maskcolor.EnableWindow(FALSE);
		m_check_maskcolor.SetCheck(BST_UNCHECKED);
	}
}

void CTBarGenAddinDlg::OnBnClickedCheckMaskbutton()
{
	if(m_check_maskcolor.GetCheck() != BST_CHECKED)
	{
		m_button_maskcolor.EnableWindow(FALSE);
		m_check_maskcorner.SetCheck(BST_CHECKED);
	}
	else
	{
		m_button_maskcolor.EnableWindow(TRUE);
		m_check_maskcorner.SetCheck(BST_UNCHECKED);
	}
}

void CTBarGenAddinDlg::OnUpdatePopupSaveimageinlibrary(CCmdUI *pCmdUI)
{
	int nPos = m_list_buttons.GetCurSel();

	
	st_toolbar_button tmp_button;

	tmp_button = m_toolbar.buttons[nPos];

	if (tmp_button.type== "BUTTON")
	{
		pCmdUI->Enable(TRUE);
	}
	else
	{
		pCmdUI->Enable(FALSE);
	}
}

void CTBarGenAddinDlg::OnPopupSaveimageinlibrary()
{
	int nPos = m_list_buttons.GetCurSel();
	int nImage = m_list_buttons.GetItemImage(nPos,2);

}
