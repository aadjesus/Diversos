// DlgLibPreview.cpp : file di implementazione
//

#include "stdafx.h"
#include "TBarGenAddin.h"
#include "DlgLibPreview.h"
#include "ximage.h"


// finestra di dialogo CDlgLibPreview

IMPLEMENT_DYNAMIC(CDlgLibPreview, CMagDialog)
CDlgLibPreview::CDlgLibPreview(CWnd* pParent /*=NULL*/)
	: CMagDialog(CDlgLibPreview::IDD, pParent)
{
}

CDlgLibPreview::~CDlgLibPreview()
{
}

void CDlgLibPreview::DoDataExchange(CDataExchange* pDX)
{
CMagDialog::DoDataExchange(pDX);
DDX_Control(pDX, IDC_LIST_LIBPREVIEW, m_list_libpreview);
DDX_Control(pDX, IDC_CHECK_ACTSIZE, m_check_actualsize);
DDX_Control(pDX, IDC_CHECK_RPTVIEW, m_check_reportview);
DDX_Control(pDX, IDC_CHECK_FILTERSIZE, m_check_filterbysize);
DDX_Control(pDX, IDC_COMBO_ICONSIZE, m_combo_size);
	}


BEGIN_MESSAGE_MAP(CDlgLibPreview, CMagDialog)
	ON_BN_CLICKED(IDC_BUTTON_REFRESH, OnBnClickedButtonRefresh)
	ON_BN_CLICKED(IDC_CHECK_ACTSIZE, OnBnClickedCheckActsize)
	ON_CBN_SELCHANGE(IDC_COMBO_ICONSIZE, &CDlgLibPreview::OnCbnSelchangeComboIconsize)
END_MESSAGE_MAP()


// gestori di messaggi CDlgLibPreview

void CDlgLibPreview::PopulatePreviewList(CString strDirectory,CSize iconSize)
{
	CxImage			img;
	CBitmap			bmp;
	COLORREF		rgbColorMask;
	HANDLE			hSearch; 
	int				irx,iry;
	RGBQUAD			rgbq;
	WIN32_FIND_DATA FileData; 

	irx = iconSize.cx;
	iry = iconSize.cy;

	BOOL fFinished = FALSE;

	m_list_libpreview.DeleteAllItems();

	m_libImageList.DeleteImageList();
	m_libImageList.Create(irx, iry,ILC_MASK | ILC_COLOR32,0,0);

	m_libImageListSmall.DeleteImageList();
	m_libImageListSmall.Create(16,16,ILC_MASK | ILC_COLOR32,0,0);
	
	m_list_libpreview.SetImageList(&m_libImageList,LVSIL_NORMAL);
	m_list_libpreview.SetImageList(&m_libImageListSmall,LVSIL_SMALL);

	hSearch = FindFirstFile(strDirectory+"*.*", &FileData); 
	if (hSearch == INVALID_HANDLE_VALUE) 
	{ 
		return;
	} 

	int nPos	= 0;
	int imgPos	= 0;
	int nFld	= 0;
	int nFldImgPos = -1;
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
			{
				if (img.Load(strDirectory+FileData.cFileName,imgType))
				{
					if (img.GetBpp() != 24)
						img.IncreaseBpp(24);

					if (((m_check_filterbysize.GetCheck() == BST_CHECKED) &&
						(img.GetWidth() == iconSize.cx) &&
						(img.GetHeight() == iconSize.cy)) ||
						(m_check_filterbysize.GetCheck() != BST_CHECKED))
					{
						CRect rect;
						CxImage tmpImg;
						LONG cnx,cny;
		
						rgbq = img.GetPixelColor(0,0,false);
						rgbColorMask = RGB(rgbq.rgbRed,rgbq.rgbGreen,rgbq.rgbBlue);
	
						if (m_check_actualsize.GetCheck() == BST_CHECKED)
						{
							//do not resample image: just add "transparent" borders
							if ((img.GetWidth() <= iconSize.cx ) &&
								(img.GetHeight() <= iconSize.cy)) 
							{
								cnx = (LONG)iconSize.cx/2;
								cny = (LONG)iconSize.cy/2;

								rect.top	= (LONG)(cny-img.GetHeight()/2);
								rect.bottom = (LONG)(cny+img.GetHeight()/2);
								rect.left	= (LONG)(cnx-img.GetWidth()/2);
								rect.right	= (LONG)(cnx+img.GetWidth()/2);
							}
							else
							{
								rect.top	= 0;
								rect.bottom = iconSize.cy;
								rect.left	= 0;
								rect.right	= iconSize.cx;
							}

							tmpImg.Create(iconSize.cx,iconSize.cy,32,CXIMAGE_FORMAT_BMP);
							tmpImg.SelectionCreate();
							tmpImg.SelectionAddRect(rect);

							int x,y;
							for (x=0;x<iconSize.cx;x++)
								for(y=0;y<iconSize.cy;y++)
								{
									if (tmpImg.SelectionIsInside(x,y))
									{
										rgbq = img.GetPixelColor(x-rect.left,y-rect.top,false);
										tmpImg.SetPixelColor(x,y,rgbq,false);
									}
									else
									{
										tmpImg.SetPixelColor(x,y,rgbColorMask);
									}
								}
						}
						else
						{
							img.Resample(iconSize.cx,iconSize.cy,2,&tmpImg);
						}

						bmp.Attach(tmpImg.MakeBitmap());
						imgPos = m_list_libpreview.GetImageList(LVSIL_NORMAL)->Add(&bmp,rgbColorMask);
						
						CxImage tmpImgSmall;
						img.Resample(16,16,1,&tmpImgSmall);
						imgPos = m_list_libpreview.GetImageList(LVSIL_SMALL)->Add(CBitmap::FromHandle(tmpImgSmall.MakeBitmap()),rgbColorMask);
						
						nPos = m_list_libpreview.GetItemCount();
						m_list_libpreview.InsertItem(nPos,"",imgPos);
						m_list_libpreview.SetItemText(nPos,0,FileData.cFileName);
						//m_list_libpreview.SetItemImage(nPos,0,imgPos);
	
						img.Destroy();
						bmp.Detach();
					}
				}
			}
		}
		else
		{
			if (nFldImgPos < 0)
			{
				CxImage imgFldresampled;
				CBitmap bmpFld16;
				CBitmap bmpFldresampled;
				CxImage img;
				bmpFldresampled.LoadBitmap(IDB_BITMAP_FOLDER32);
				img.CreateFromHBITMAP((HBITMAP)bmpFldresampled.GetSafeHandle());
				img.Resample(iconSize.cx,iconSize.cy,1,&imgFldresampled);
				bmpFldresampled.DeleteObject();
				bmpFldresampled.Attach(imgFldresampled.MakeBitmap());
				bmpFld16.LoadBitmap(IDB_BITMAP_FOLDER16);
				nFldImgPos = m_list_libpreview.GetImageList(LVSIL_NORMAL)->Add(&bmpFldresampled,RGB(255,0,255));
				m_list_libpreview.GetImageList(LVSIL_SMALL)->Add(&bmpFld16,RGB(255,0,255));
			}
			if ((nFldImgPos >= 0) &&
				(CString(FileData.cFileName) != "."))
			{
				m_list_libpreview.InsertItem(nFld,"",nFldImgPos);
				m_list_libpreview.SetItemText(nFld,0,FileData.cFileName);
				nFld++;
			}
		}
		if (!FindNextFile(hSearch, &FileData)) 
		{
			fFinished = TRUE;
		}
	} 


	FindClose(hSearch);
}

BOOL CDlgLibPreview::OnInitDialog()
{
	CMagDialog::OnInitDialog();

	m_combo_size.SetCurSel(1); //32x32

	m_iSize.cx = 32;
	m_iSize.cy = 32;

	

	char buffer[_MAX_PATH];
	GetModuleFileName(NULL,buffer,_MAX_PATH);

	CPath path;
	path.SetPath(buffer);

	m_strImgLib = AfxGetApp()->GetProfileString("config","imgLib",path.GetLocation()+"imgLib");
	
	m_list_libpreview.InsertColumn(0,"test");

	PopulatePreviewList(m_strImgLib,m_iSize);
	return TRUE;  
}

void CDlgLibPreview::OnBnClickedButtonRefresh()
{
	PopulatePreviewList(m_strImgLib,m_iSize);
}

void CDlgLibPreview::OnBnClickedCheckActsize()
{
	
}

void CDlgLibPreview::OnCbnSelchangeComboIconsize()
{
	switch(m_combo_size.GetCurSel())
	{
	case 0:
		{
			m_iSize.cx = 48;
			m_iSize.cy = 48;
		}
		break;
	case 1:
		{
			m_iSize.cx = 32;
			m_iSize.cy = 32;
		}
		break;
	case 2:
		{
			m_iSize.cx = 16;
			m_iSize.cy = 16;
		}
		break;
	default:
		m_iSize.cx = 32;
		m_iSize.cy = 32;
		m_combo_size.SetCurSel(1); //32x32

		break;
	}

	PopulatePreviewList(m_strImgLib,m_iSize);
	
}
