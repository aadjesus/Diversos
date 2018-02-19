#pragma once
#include "stdafx.h"
#ifndef MYSTRUCTS
#define MYSTRUCTS

// Structs and helpers class
struct  st_bitmap
{
	CString id;
	CString path;
};

struct st_toolbar_button
{
	CString id;
	CString	type;
	CString imgPath;
	int		nPosImg;
	BOOL	bChkId; //there are a lot of IDs defined out of resource.h 
	//so I assume that buttons found in .rc file had already ù
	//their ids defined somewhere...
};

struct st_toolbar
{
	CString id;
	int rx,ry;
	UINT iType;
	vector <st_toolbar_button> buttons;
};

#endif

// Old, good global functions :)

BOOL IsFile(LPCTSTR File);
BOOL IsDirectory(LPCTSTR Directory);