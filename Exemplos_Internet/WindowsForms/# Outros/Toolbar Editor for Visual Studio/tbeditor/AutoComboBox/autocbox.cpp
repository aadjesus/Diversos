/**********************************************************************
 *
 * autocbox.cpp
 *
 * programmed by e.e.l.mitchell, fort myers, fl (2003)
 * $Date: 2005/07/29 00:05:41 $
 *
 * version 1.2
 *
 * see header file for description of routine
 *
***********************************************************************/

#include "stdafx.h"
#include <cassert>
#include <vector>       // this could be precompiled in StdAfx.h

#include "autocbox.h"
#include ".\autocbox.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

////////////////////////////////////////////////////////////////////////
//
// CAutoComboBox. the drop down list will only accept strings that
// match one of the rows in the combobox
//
CAutoComboBox::CAutoComboBox(AutoComboBoxType acbt)
 : m_autoComboBoxType(acbt)
{
    m_bAutoComplete = true;
    m_iSelectedRow = -1;
    m_bReverseEntry = false;
    m_bEditHeightSet = false;
}
////////////////////////////////////////////////////////////////////////
//
// remove the heap strings that we remembered
//
CAutoComboBox::~CAutoComboBox()
{
}
BEGIN_MESSAGE_MAP(CAutoComboBox, CComboBox)
    ON_CONTROL_REFLECT(CBN_EDITUPDATE, OnEditUpdate)
    ON_CONTROL_REFLECT(CBN_SELCHANGE, OnSelChange)
    ON_CONTROL_REFLECT(CBN_DROPDOWN, OnCbnDropdown)
	ON_MESSAGE(UWM_EDIT_COMPLETE_MSG,OnEditComplete)
	ON_WM_CTLCOLOR()
	ON_CONTROL_REFLECT(CBN_KILLFOCUS, OnCbnKillfocus)
	ON_WM_KILLFOCUS()
END_MESSAGE_MAP()

////////////////////////////////////////////////////////////////////////
//
// CAutoComboBox message handlers
//
////////////////////////////////////////////////////////////////////////
//
// find the longest string in the combobox list and set the width of the
// drop down part so it can all be seen
//
int
CAutoComboBox::SetDroppedWidthForTextLengths()
{
    int iPixLength = 0;
    CString sLine;

    CDC* pDC = GetDC();
    assert(pDC);

    // find longest string in the combo box
    for(int i = 0; i < GetCount(); ++i) {
        // get the item text for this row
        GetLBText(i, sLine);
        CSize sz = pDC->GetTextExtent(sLine);
        // find the maximum length of line in pixels
        iPixLength = max(iPixLength, sz.cx);
    }
    // set the dropped width of the list box so that every item is
    // completely visible
    SetDroppedWidth(iPixLength);
    return iPixLength;
}
////////////////////////////////////////////////////////////////////////
//
// this saves a possible assert when accessing a row that has an empty
// string. this overloads the same routine in CComboBox as protection
//
void
CAutoComboBox::GetLBText(int nIndex, CString& rString) const
{
    // if the text length is zero, just fill the string directly
    if(GetLBTextLen(nIndex) == 0) {
        rString = _T("");
        return;
    }
    else {
        CComboBox::GetLBText(nIndex, rString);
        return;
    }
}
void
CAutoComboBox::GetLBText(int nIndex,int nSubItem, CString& rString) const
{
	if (nSubItem ==0)
	{
		if(GetLBTextLen(nIndex) == 0) {
			rString = _T("");
			return;
		}
		else {
			CComboBox::GetLBText(nIndex, rString);
			return;
		}
	}
	else if (nSubItem < m_vecEndPixels.size())
	{
		rString = m_vecRows[nIndex][nSubItem -1];
	}

}
////////////////////////////////////////////////////////////////////////
//
// the normal combobox won't have more than one column so this is an
// easier access routine than having to provide an empty array of strings
//
int
CAutoComboBox::AddRow(const TCHAR* pString)
{
    CStringsVec extraCols;      // empty set of extra columns

    return AddRow(pString, extraCols);
}
////////////////////////////////////////////////////////////////////////
//
// it's probably simpler to put all the columns of text into a single
// vector of strings rather than special case the first column
//
int
CAutoComboBox::AddRow(const CStringsVec& allCols)
{
    // we must have been given at least one column of text. the string
    // inside may be empty however
    assert(allCols.size() > 0);

    // to prevent a disaster in release mode
    if(allCols.size() == 0) return CB_ERR;

    CString sFirstString = allCols[0];
    // note if the incoming vector is empty, then this assignment will
    // never finish
    CStringsVec extraCols(allCols.begin() + 1, allCols.end());

    return AddRow(sFirstString, extraCols);
}
////////////////////////////////////////////////////////////////////////
//
// the first string and the extra column text is added to the list box.
// the extra column info has to be added first to the vector holding
// columns 2 thru the end. then the string is added to the combobox which
// will trigger a WM_MEASUREITEM message due to the owner draw variable
// attribute. in this we have to be able to figure out the pixel width
// of each column. returns the item index where the row was added
//
int
CAutoComboBox::AddRow(const TCHAR* pString, const CStringsVec& extraCols)
{
    CStringsVec vecNewRow;

    // if this is the first time, then we can set the size of the pixel
    // column and the extra strings vector array
    if(m_vecEndPixels.size() == 0) {
        SetColCount(extraCols.size() + 1);
    }
    // we must set the column count before adding rows so that the column
    // end vector is sized correctly. the extra cols vector must always
    // be the same size
    assert(m_vecEndPixels.size() == extraCols.size() + 1);

    for(UINT iCol = 0; iCol < extraCols.size(); ++iCol) {
        vecNewRow.push_back(extraCols[iCol]);
    }
    m_vecRows.push_back(vecNewRow);
    int iRow = AddString(pString);

    // we must have added it to the same index
    assert((iRow + 1) == m_vecRows.size());

    return iRow;
}
////////////////////////////////////////////////////////////////////////
//
// the string for the row in the combobox is deleted and also the slot
// in the vector of row text so that the extra columns are kept in synch
// with the prime column. returns the same as ComboBox::DeleteString(...)
//
int
CAutoComboBox::DeleteRow(int iRow)
{
    assert(iRow < (int) m_vecRows.size());
    // get rid of the vector slot that corresponds to this row
    m_vecRows.erase(m_vecRows.begin() + iRow);
    // and delete the text from the combobox itself
    return CComboBox::DeleteString(iRow);
}
////////////////////////////////////////////////////////////////////////
//
// deprecated routine replaced by DeleteRow(...)
//
int CAutoComboBox::DeleteString(UINT uRow)
{
    return DeleteRow(uRow);
}
////////////////////////////////////////////////////////////////////////
//
// the text in the edit buffer has changed so look for a single match
// and if found restore this but highlight the nonmatching part so
// that any subsequent character can overwrite it
//
void
CAutoComboBox::OnEditUpdate() 
{
    CString line;               // partial line entered by user
    CString sMatchedText;       // holds full line from list

    // get the text from the user input
    GetWindowText(line);
    int iHiLightStart = line.GetLength();

    // if the line is empty
    if(line.GetLength() == 0) {
        // make sure the dropdown list is closed when back to
        // zero characters in the edit box
        ShowDropDown(FALSE);

        // empty the selection
        SetWindowText(_T(""));

        // cancel any previous selection
        m_iSelectedRow = -1;

        // turn on autocomplete again turned off by deletes
        m_bAutoComplete = true;
        return;
    }
    // if we have seen a delete or back key we leave the text alone so
    // that the user can continue to go backwards to correct an entry
    if(!m_bAutoComplete) {
        // but only for one character
        m_bAutoComplete = true;

        // if reversing entry, must keep insertion point on right
        if(m_bReverseEntry) {
            SetEditSel(0, 0);
        }
        return;
    }
    // if a single match, we can display that and identify the mismatch
    m_iSelectedRow = FindUniqueMatchedItem(line, sMatchedText);

    // if we found a unique matching row
    if(m_iSelectedRow >= 0) {

        // drop down the list part of the combo box
        ShowDropDown(TRUE);

        // we try this so that the selection occurs AFTER the dropdown
        // box has been filled
        PostMessage(CB_SETCURSEL, m_iSelectedRow, 0);

        // now we have to also remove the selection from the edit box
        // since we may want to continue to add to the string and
        // set out own highlight on the text we have added from the match
        //
        int iStartChar = 0;
        int iEndChar = -1;

        if(!m_bReverseEntry) {
            // straight entry, we want to highlight the added text from
            // the end of the input line text to the end of the field
            iStartChar = line.GetLength();
        }
        else {
            // reverse entry, we want to highlight the added text
            // on the left - from char 0 to match less line length
            iEndChar = sMatchedText.GetLength() - line.GetLength();
        }
        PostMessage(CB_SETEDITSEL, 0, MAKELPARAM(iStartChar, iEndChar));
		
		CStringsVec		sendVect;
		CString			strTmp;
		for(int iCol = 0; iCol < m_vecEndPixels.size(); ++iCol)
		{
			GetLBText(m_iSelectedRow,iCol,strTmp);
			sendVect.push_back(strTmp);
		}
		GetParent()->SendMessage(CBN_ITEMCHANGED,(WPARAM)&sendVect,NULL);
        // set the window text to the match
        line = sMatchedText;
    }
    // if this text won't match any string and we are emulating
    // a drop list so the selection is forced
    else if(sMatchedText.IsEmpty() && m_autoComboBoxType == ACB_DROP_LIST) {

        // alert the user to no match
        MessageBeep(MB_ICONEXCLAMATION);

        // if we are not entering text backwards
        if(!m_bReverseEntry) {
            // remove the last character typed in error. at the end
            line.Delete(line.GetLength() - 1);
        }
        else {
            // remove the last character typed in error (at the beginning)
            // which will be in the first position
            line.Delete(0);
        }
        assert(iHiLightStart > 0);
        iHiLightStart -= 1;

        // restore the line since the closeup cleared the edit box
        SetWindowText(line);
    }
    else {
        // the list box will be closed to show that there is no match
        ShowDropDown(FALSE);

        // restore the line since the closeup cleared the edit box
        SetWindowText(line);
    }
    // if we are not entering text backwards
    if(!m_bReverseEntry) {
        // now set the selection to beyond the last character which
        // moves the caret to the end
        SetEditSel(iHiLightStart, -1);
    }
    else {
        // now set the insertion caret to the beginning so we
        // build up the text from the right
        SetEditSel(0, 0);
    }
}
////////////////////////////////////////////////////////////////////////
//
// find the row of the list box that matches. if more than one
// row matches at the beginning, this routine will fail
//
// inputs
//
// line         text user has typed in
//
// outputs
//
// matchedLine  row of list box if a  prefix or postfix match that
//              is the shortest of the matching strings
//
// returns      zero based index of row or -1 on fail. the shortest
//              matched string is also returned
//
int
CAutoComboBox::FindUniqueMatchedItem(const CString &line
 , CString &sMatchedLine)
{
    // remove any previous text
    sMatchedLine.Empty();
    int iMatchedCount = 0;
    int iMatchedItem = CB_ERR;
    CString sItem;

    for(int iItem = 0; iItem < GetCount(); ++iItem) {

        // get this row from the list
        GetLBText(iItem, sItem);

        // if an exact match, we can use this even though it may not be
        // unique. ie. with 273 and 273A, we should select the 273 as
        // a unique match
        if(!sItem.CompareNoCase(line)) {
            sMatchedLine = sItem;
            return iItem;
        }
        // if the string contains the prefix/postfix in the line
        if(LineInString(line, sItem)) {

            // save the string if it's the first or the smallest
            if(sMatchedLine.IsEmpty()
                    || sItem.GetLength() < sMatchedLine.GetLength()) {
                sMatchedLine = sItem;
                iMatchedItem = iItem;
            }
            // bump count of matched items
            iMatchedCount += 1;
        }
    }
    // if we have single exact match, return the index else error
    return (iMatchedCount == 1) ? iMatchedItem : CB_ERR;
}
////////////////////////////////////////////////////////////////////////
//
// we need to check for backspace or delete. when we see
// one of these characters, we don't do the autocomplete
// since the same text would be written over the deleted stuff
//
BOOL
CAutoComboBox::PreTranslateMessage(MSG* pMsg) 
{
    // if we have a key pressed and we are autocompleting
    if(pMsg->message == WM_KEYDOWN) {
        int key = pMsg->wParam;
        if(m_bAutoComplete) {
            // if delete or backspace, set to not autocomplete on
            // the next real character
            if(key == VK_DELETE || key == VK_BACK) m_bAutoComplete = false;
        }
#if 0 // removed check for OS version
        if(key == VK_RETURN) {
            // only do the following with OS of NT or later
            OSVERSIONINFO osvi;
            BOOL bOsVersionInfoEx;
            ZeroMemory(&osvi, sizeof osvi);
            bOsVersionInfoEx = GetVersionEx((LPOSVERSIONINFO) &osvi);
            if(bOsVersionInfoEx && osvi.dwPlatformId == VER_PLATFORM_WIN32_NT) {
            // we would like to close the dropdown list automatically when
            // the "enter" is hit so that it is then passed on to the default
            // button of the dialog. unfortunately, on Win95/98/ME systems
            // the ShowDropDown(FALSE) can sometimes change the selection.
            //
            // is doesn't show as a problem every timne. only with numbers
            // that are contained within each other like 1 and 100, 5
            // and 5252. There is no problem if the OS is NT or XP
            //
            // this is considered as a bug in the combobox internals and is
            // documented as Microsoft incident number SRX030506602024
            //
            // if we have the enter key, close the drop down here so that
            // we only have to hit enter once
                ShowDropDown(FALSE);
            }
        }
#endif // 0
    }
    return CComboBox::PreTranslateMessage(pMsg);
}
////////////////////////////////////////////////////////////////////////
//
//  if the text in the edit box corresponds to one of the row items
//  then the index is returned, else the return will be -1
//
int
CAutoComboBox::GetSelectedRow()
{
    int iCount = GetCount();
    // if we have an impossible selection
    if(m_iSelectedRow >= iCount) {
        m_iSelectedRow = -1;
    }
    return m_iSelectedRow;
}
////////////////////////////////////////////////////////////////////////
//
// the selection in the combobox has changed so we remember which row
// is displayed
//
void
CAutoComboBox::OnSelChange() 
{
	CStringsVec		sendVect;
	CString			strTmp;
	for(int iCol = 0; iCol < m_vecEndPixels.size(); ++iCol)
	{
		GetLBText(GetCurSel(),iCol,strTmp);
		sendVect.push_back(strTmp);
	}
	GetParent()->SendMessage(CBN_ITEMCHANGED,(WPARAM)&sendVect,NULL);
}
////////////////////////////////////////////////////////////////////////
//
// this will prevent the mouse arrow from disappearing when text is
// typed into the combobox. fix from Mark Doubson
//
void
CAutoComboBox::OnCbnDropdown()
{
    SetCursor(LoadCursor(NULL, IDC_ARROW));
}
////////////////////////////////////////////////////////////////////////
//
// change the number of columns that will be shown in the dropdown. the
// vector holds the starting pixel position of the extra columns. ie
// those for cols 2 thru the end. all the row information is deleted
// and the combobox reset so that we can maintain synchronism between
// the text held in the combobox and that for the other columns in
// the same row
//
void
CAutoComboBox::SetColCount(int iCols)
{
    assert(iCols > 0);

    // the combobox must be owner draw, variable for this class to work
    assert(GetStyle() & CBS_OWNERDRAWVARIABLE);
    // and also must have the strings style since the first column
    // is contained inside the combobox memory itself
    assert(GetStyle() & CBS_HASSTRINGS);

    ResetContent();

    // now size everything for the new column count
    m_vecEndPixels.resize(iCols);
}
////////////////////////////////////////////////////////////////////////
//
// make sure that there is no info saved inside the class or the combobox
//
void
CAutoComboBox::ResetContent()
{
    // remove all existing content from the combobox itself
    CComboBox::ResetContent();

    // clear out the existing info from the vectors for the other columns
    m_vecEndPixels.clear();
    m_vecRows.clear();
}
////////////////////////////////////////////////////////////////////////
//
// make sure that there is no memory of a previous selection
//
void
CAutoComboBox::ResetSelection()
{
    CComboBox::SetCurSel(-1);
    m_iSelectedRow = -1;
}
////////////////////////////////////////////////////////////////////////
//
// check whether the line is part of the string. we may be testing for
// a match on the left (normal) or a match on the right (reverse)
//
bool
CAutoComboBox::LineInString(const CString& rsLine
 , const CString& rsString) const
{
    CString sPart;      // holds right or left possible match

    // pick out the part of the string that is the same length
    // as the input line so far
    if(m_bReverseEntry) {
       sPart = rsString.Right(rsLine.GetLength());
    }
    else {
       sPart = rsString.Left(rsLine.GetLength());
    }
    // return true if the line matches the extracted part
    return !rsLine.CompareNoCase(sPart);
}
////////////////////////////////////////////////////////////////////////
//
// draw the row in the list box part of the combobox
//
void
CAutoComboBox::DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct) 
{
    // get the CDC object from the draw item struct
    CDC* pDC = CDC::FromHandle(lpDrawItemStruct->hDC);
    CWnd* pWndDropDown = pDC->GetWindow();

    // ?? do we need this
    assert(pWndDropDown != this);

    CRect rcText = lpDrawItemStruct->rcItem;
    int iItemID = lpDrawItemStruct->itemID;
    int iItemAction = lpDrawItemStruct->itemAction;
    int iItemState = lpDrawItemStruct->itemState;
    CString sText;
    UINT iCol = 0;      // required because of MFC bug

    // sometimes the itemid is -1 which will cause the vector
    // index to fail later on. suggested by atilla via codeproject
    if(iItemID == CB_ERR) return;

    // this allows us to change the rectangle for each field
    CRect rcField = rcText;

    switch(iItemAction) {

    case ODA_DRAWENTIRE:
    case ODA_SELECT:
        // draw the whole line. get the text we'll draw
        GetLBText(iItemID, sText);

        // if this item is selected, choose the blueish color for 
        // the highlight. otherwise, just use the default
        if(iItemState & ODS_SELECTED) {
            pDC->SetBkColor(GetSysColor(COLOR_HIGHLIGHT));
            pDC->SetTextColor(GetSysColor(COLOR_HIGHLIGHTTEXT));
        }
        else {
            pDC->SetBkColor(GetSysColor(COLOR_WINDOW));
            pDC->SetTextColor(GetSysColor(COLOR_WINDOWTEXT));
        }
        // actually draw the text!
        pDC->ExtTextOut(rcText.left, rcText.top, ETO_OPAQUE, &rcText
         , sText, sText.GetLength(), NULL);

        // now output the other columns, if any
        for(iCol = 1; iCol < m_vecEndPixels.size(); ++iCol) {
            if (iCol != 2)
			{
				sText = m_vecRows[iItemID][iCol - 1];
				rcField.left = m_vecEndPixels[iCol - 1];

				pDC->ExtTextOut(rcField.left, rcField.top, ETO_OPAQUE, &rcField
				, sText, sText.GetLength(), NULL);
			}
        }
        // check if we need to show selection state
        if(iItemState & ODS_FOCUS) {
            // set the focus state (no toggle)
            pDC->DrawFocusRect(&rcText);
        }
        break;

    case ODA_FOCUS:
        // toggle the focus state. we assume that the focus is set
        // and then unset so that it can be toggled.
        pDC->DrawFocusRect(&rcText);
        break;
    }
    // TRACE(_T("DrawItem Action=%d, ID=%d, State=0x%x, Rect(%x,%x,%x,%x)\n")
    //  , iItemAction, iItemID, iItemState, rcText.left, rcText.right
    //  , rcText.top, rcText.bottom);
}
////////////////////////////////////////////////////////////////////////
//
// we need to set the height of the line
//
void
CAutoComboBox::MeasureItem(LPMEASUREITEMSTRUCT lpMeasureItemStruct) 
{
    TEXTMETRIC tm;
    CClientDC dc(this);
    CDC* pDC = &dc;

    // we must do this to ensure that the device context is using the
    // combobox font when we use it to size the text height and width
    // this code comes from the CheckComboBox by Magnus Egelberg via
    // the CodeProject
    CFont* pFont = dc.SelectObject(GetFont());
    // can this ever be null and if so does it matter???
    if(pFont) {
        pDC->GetTextMetrics(&tm);
        lpMeasureItemStruct->itemHeight = tm.tmHeight + tm.tmExternalLeading;

        // this is needed since the WM_MEASUREITEM message is sent before
        // MFC hooks everything up if used in a dialog. adjust the edit
        // part the combobox now, once. if we don't change the height, the
        // owner draw combobox is 28 pixels high and the regular one is 24
        // pixels high. note we add 2 pixels to the nominal height of the
        // text in the list box part
        if(!m_bEditHeightSet) {
            m_bEditHeightSet = true;
            SetItemHeight(-1, lpMeasureItemStruct->itemHeight + 2);
        }
    }
    int itemID = lpMeasureItemStruct->itemID;
    const CStringsVec& vecRow = m_vecRows[itemID];
    int iColCount = m_vecEndPixels.size();

    CString sItem;      // contents of field
    GetLBText(itemID, sItem);

    assert(itemID < (int) m_vecRows.size());
    assert(iColCount == vecRow.size() + 1);
    assert(iColCount >= 1);

    // start pixel of column coming up
    int iOldStart = 0;
    int iNewStart = 0;

    for(int iCol = 0; iCol < iColCount; ++iCol) {
			// how much space will this text take up
		CSize sizePixWidth = pDC->GetOutputTextExtent(sItem);

		// leave some blank space for a separator. may have to
		// adjust for lowball values from the text extent processor
		int cxNewField = (int) (sizePixWidth.cx + 5);

		// the end of the column field must take into account the
		// largest field in the column
		int iOldEnd = m_vecEndPixels[iCol];

		int cxOldField = iOldEnd - iOldStart;

		// compare field widths and take the larger
		if(cxNewField > cxOldField) {
			cxOldField = cxNewField;
		}
		// for the next field
		iNewStart += cxOldField;

		// if the last column we can test the list box width
		if(iCol == iColCount - 1) {
			// how wide is the current list box
			int iDroppedWidth = GetDroppedWidth();

			// if not as wide as this row
			if(iDroppedWidth < iNewStart) {
				// make it wider to the end of the last row and add
				// the scroll width in case there is a scroll bar
				SetDroppedWidth(iNewStart
				+ 4 + GetSystemMetrics(SM_CXVSCROLL));
			}
		}
		// save the old start position of the next field so we can
		// calculate the field width of the next column
		iOldStart = m_vecEndPixels[iCol];

		// the end of this field is the new start for the next field
		m_vecEndPixels[iCol] = iNewStart;

		// if we have any extra columns
		if(iCol < (int) vecRow.size()) {
			// now look at columns 2 thru the end
			sItem = vecRow[iCol];
		}
    }
    // set the width for this row
    lpMeasureItemStruct->itemWidth = iNewStart;

    // put back the font here. can it be null??
    pDC->SelectObject(pFont);
    int iRet = ReleaseDC(pDC);
}



HBRUSH CAutoComboBox::OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor)
{
	if (nCtlColor == CTLCOLOR_EDIT)
	{
		//[ASCII 160][ASCII 160][ASCII 160]Edit control
		if (m_edit.GetSafeHwnd() == NULL)
			m_edit.SubclassWindow(pWnd->GetSafeHwnd());
		m_edit.IsParentCombobox(TRUE);
	}
	HBRUSH hbr = CComboBox::OnCtlColor(pDC, pWnd, nCtlColor);
	return hbr;
}

LRESULT CAutoComboBox::OnEditComplete(WPARAM wParam, LPARAM lParam)
{
	return GetParent()->SendMessage(UWM_EDIT_COMPLETE_MSG,wParam,lParam);
}
void CAutoComboBox::OnCbnKillfocus()
{
	Invalidate();
}

void CAutoComboBox::OnKillFocus(CWnd* pNewWnd)
{
	CComboBox::OnKillFocus(pNewWnd);

	Invalidate();
}
