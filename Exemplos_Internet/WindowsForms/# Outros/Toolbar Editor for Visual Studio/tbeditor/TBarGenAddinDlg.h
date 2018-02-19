// TBarGenAddinDlg.h : header file
//

#pragma once
#include "afxcmn.h"
#include "MyToolBarCtrl.h"
#include "BCMenu\BCMenu.h"
#include "ReportCtrl\ReportCtrl.h"
#include "AutoComboBox\autocbox.h"
#include "ColorButton\ColorButton.h"
#include "TrueColorToolBar\TrueColorToolBar.h"
#include "DlgLibPreview.h"

#include "afxwin.h"

#define	ID_RECENTFILEPOS	4		//where to put recent file submenu
#define IDS_TOOLBAR_RCMARKER	"// Toolbar"	//marker to search for toolbars

typedef vector<CString> CSTRINGVECTOR;
typedef vector<CString>::iterator CSTRINGVECTORIT;
// CTBarGenAddinDlg dialog
class CTBarGenAddinDlg : public CMagDialog
{
// Construction
public:
	CTBarGenAddinDlg(CWnd* pParent = NULL);	// standard constructor
	~CTBarGenAddinDlg();
// Dialog Data
	enum { IDD = IDD_TBARGENADDIN_DIALOG };
	enum {	ID_VS71	= 0,
			ID_VS6	= 1
		 };
	
	enum {	
			ID_RESDOC	= 0,
			ID_BMPDOC	= 1
		};
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON				m_hIcon;
	BCMenu				m_mainMenu;
	BCMenu				m_floatPop;
	BCMenu				m_mruMenu;
	//BCMenu*				m_pMruMenu;
	CEdit				m_edit_bmpfile;
	CEdit				m_edit_rx;
	CEdit				m_edit_ry;
	CStatic				m_static_picture;
	CStatic				m_static_toolbar;
	CStatic				m_static_cnt;
	CButton				m_check_tcbmp;
	CButton				m_check_grbmp;
	CButton				m_check_htbmp;
	CButton				m_check_maskcorner;
	CButton				m_check_maskcolor;

	CSpinButtonCtrl		m_spin_cnt;
	
	CComboBox			m_combo_bpp;
	CAutoComboBox		m_combo_resourceName;
	CReportCtrl			m_list_buttons;
	CReportCtrl			m_list_library;
	CColorButton		m_button_color;
	CColorButton		m_button_maskcolor;
	CMyToolBarCtrl		m_ctrl_toolbar;
	CTrueColorToolBar	m_main_toolbar;

	st_bitmap			m_bitmap;
	st_toolbar			m_toolbar;

	CImageList*			m_imageList;
	CImageList*			m_libImageList;
	CImageList*			m_DragImageList;
	
	CWnd*				m_pDragWnd;
	CWnd*				m_pDropWnd;
	CString				m_strResourceIncFile;
	CString				m_strTmpDir;
	CString				m_strImgLib;
	CString				m_strResourceFile;
	CString				m_strResourceName;
	CString				m_strIdsFile;
	CString				m_strGrBmpFile;
	CString				m_strTcBmpFile;
	CString				m_strHtBmpFile;
	
	BOOL				m_bReplaceOFile;
	BOOL				m_bAutoResize;
	BOOL				m_bDragging;		//Is dragging?
	BOOL				m_bIsNewToolbar;    //Are we working with a new toolbar?

	int					m_nDropIndex;
	int					m_nDragIndex;

	int					m_iDocType;


	CDlgLibPreview		m_dlgLibPreview;
	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg void OnBnClickedButtonTop();
	afx_msg void OnBnClickedButtonUp();
	afx_msg void OnBnClickedButtonDown();
	afx_msg void OnBnClickedButtonBottom();
	afx_msg void OnDropFiles(HDROP hDropInfo);
	afx_msg void OnBnClickedButtonRefresh();
	afx_msg void OnBnClickedButtonColor();
	afx_msg void OnBnClickedButtonSave();
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnBnClickedButtonSaveas();
	afx_msg void OnFileOpen();
	afx_msg void OnEditimageResize();
	afx_msg void OnBnClickedButtonGenerate();
	afx_msg void OnAboutAbouttbargenaddin();
	afx_msg void OnNMRclickListButtons(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnBnClickedButtonScanresource();
	afx_msg void OnImageResize();
	afx_msg void OnPopupAddnew();
	afx_msg void OnPopupDelete();
	afx_msg void OnBnClickedButtonTcopt();
	afx_msg void OnBnClickedButtonGropt();
	afx_msg void OnFileExit();
	afx_msg void OnFileNew();
	afx_msg void OnDestroy();
	afx_msg void OnFileSavebitmap();
	afx_msg void OnAcceleratorCanc();
	afx_msg void OnAcceleratorRefresh();
	afx_msg void OnStnClickedStaticImglib();
	afx_msg void OnToolsOptions();
	afx_msg void OnBnClickedButtonHtopt();
	afx_msg void OnAboutHelp();
	afx_msg void OnToolbarCopytoclipboard();
	afx_msg void OnToolbarMergeids();
	afx_msg void OnToolbarGenerate();
	afx_msg void OnPopupInsertnew();
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg LRESULT OnTbButtonMove(WPARAM,LPARAM);
	afx_msg LRESULT OnEditComplete(WPARAM,LPARAM);
	afx_msg LRESULT OnSmallButtonClicked(WPARAM,LPARAM);
	afx_msg BOOL OnToolTipText(UINT, NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnLvnBegindragListLibrary(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnLvnBegindragListButtons(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnMRUList(UINT nID);
	afx_msg void OnAboutCheckforupdates();
	afx_msg void OnBnClickedButtonTest();
	afx_msg void OnBnClickedCheckMaskcorner();
	afx_msg void OnBnClickedCheckMaskbutton();
	afx_msg void OnUpdatePopupSaveimageinlibrary(CCmdUI *pCmdUI);
	afx_msg void OnPopupSaveimageinlibrary();
	DECLARE_MESSAGE_MAP()

private:
	BOOL OpenProject(CString strFile);
	COLORREF GetFirstPixelColor(CBitmap* bmp);
	BOOL OpenResource(CString strFile,CString strResourceID);
	BOOL ParseResourceFile(CString strFile,CString strResourceID);
	int LoadFromFile(CString strFileName,st_toolbar_button* tmp_button,int nPos = -1);
	void CreateDibBitmap(CBitmap* bmpIn,CBitmap *bmpOut, WORD BitCount = 32);
	void GetImageFromList(CImageList *lstImages, CBitmap* destBitmap);
	void GetImageFromList(CImageList *lstImages, int nImage, CBitmap* destBitmap,COLORREF rgbBackColor = 0);
	BOOL WriteToolBarToResourceFile(CString strResourceFile,CString strResourceID);
	BOOL WriteNewToolBarToResourceFile(CString strResourceFile);
	BOOL ReadIds(CString strFile,CSTRINGVECTOR &strIDS);
	BOOL ReadIds(CString strFile,CString strResourceFile,CSTRINGVECTOR &strIDS);
	BOOL WriteIdsToResource(CString strFile);
	BOOL ResizeImage(CString strFileName, CString &strDestFile);
	BOOL ReadIdsFiles(vector <CString>& vfiles);
	BOOL FileOpen(CString strFileName = "");
	void LoadImgLibrary(CString strDirectory);
	void ResetContent(BOOL bResetIds = TRUE);
	void DropItemOnList(CReportCtrl* pDragList,CReportCtrl* pDropList);
	void InitMRUList();
	
public:
	void SetTempFolder(CString strTmpDir){
		m_strTmpDir = strTmpDir;}
	void SetImgLib(CString strImgLib){
		m_strImgLib = strImgLib;}
	void SetReplaceOF(BOOL bReplaceOFile){
		m_bReplaceOFile = bReplaceOFile;}
	void SetAutoResize(BOOL bAutoResize){
		m_bAutoResize = bAutoResize;}

	/*afx_msg void OnMoving(UINT fwSide, LPRECT pRect);*/

	CButton m_button_scanresource;
};
