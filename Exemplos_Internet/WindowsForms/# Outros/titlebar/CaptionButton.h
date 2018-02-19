#ifndef CAPTIONBUTTON_INCLUDED
#define CAPTIONBUTTON_INCLUDED

#ifdef __cplusplus
extern "C" {
#endif

BOOL WINAPI Caption_InsertButton(HWND hwnd, UINT uCmd, int nBorder, HBITMAP hBmp);
BOOL WINAPI Caption_RemoveButton(HWND hwnd);

#ifdef __cplusplus
}
#endif

#endif