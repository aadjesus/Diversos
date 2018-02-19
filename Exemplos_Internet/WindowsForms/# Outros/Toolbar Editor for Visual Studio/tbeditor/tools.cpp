#include "tools.h"
#include "stdafx.h"

BOOL IsDirectory(LPCTSTR Directory)
{
	DWORD dwAttr=GetFileAttributes(Directory);
	if(dwAttr==INVALID_FILE_ATTRIBUTES)
		return FALSE;
	return (dwAttr & FILE_ATTRIBUTE_DIRECTORY);
}

BOOL IsFile(LPCTSTR File)
{
	DWORD dwAttr=GetFileAttributes(File);
	if(dwAttr==INVALID_FILE_ATTRIBUTES)
		return FALSE;
	return (!(dwAttr & FILE_ATTRIBUTE_DIRECTORY));
}