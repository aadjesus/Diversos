////////////////////////////////////////////////////////////////
//    Copyright (C) 2006-200 N-trig Innovative Techonolgies
//
//	NtrigISV.h
//
//	Interface to low level N-trig digitizer functions.
//  Allows control over MTG 
//  as well as notification callback.
//
////////////////////////////////////////////////////////////////
#ifdef NTRIGISV_EXPORTS
#define NTRIGISV_API __declspec(dllexport)
#else
#define NTRIGISV_API __declspec(dllimport)
#endif

#define NTR_WM_GESTURE		L"NtrOnGestureWindowsMessage"

enum ENtrGestureType{ScrollV,ScrollH,Zoom,Rotate,DoubleTap};

typedef void *NTRHANDLE;

typedef struct _TNtrGestures
{   
	bool ReceiveZoom;
	bool UseUserZoomSettings; 
	bool ReceiveScroll;
	bool UseUserScrollSettings;
	bool ReceiveFingersDoubleTap;
	bool UseUserFingersDoubleTapSettings;
	bool ReceiveRotate;
	bool UseUserRotateSettings;
} TNtrGestures;

typedef struct _TNtrGestureZoom
{
	double mAmount;
	unsigned short X;
	unsigned short Y;
	unsigned short Width;
	unsigned short Height;
} TNtrGestureZoom;

typedef struct _TNtrGestureScrollV
{
	double mAmount;
	unsigned short X;
	unsigned short Y;
	unsigned short Width;
	unsigned short Height;
} TNtrGestureScrollV;

typedef struct _TNtrGestureScrollH
{
	double mAmount;
	unsigned short X;
	unsigned short Y;
	unsigned short Width;
	unsigned short Height;
} TNtrGestureScrollH;

typedef struct TNtrGestureRotate
{
	double mAmount;
	unsigned short X;
	unsigned short Y;
	unsigned short Width;
	unsigned short Height;
} TNtrGestureRotate;

typedef struct _TNtrGestureDoubleTap
{
	unsigned short X;
	unsigned short Y;
	unsigned short Width;
	unsigned short Height;
} TNtrGestureDoubleTap;

NTRIGISV_API NTRHANDLE NtrGesturesConnect ();
NTRIGISV_API void NtrGesturesDisconnect (NTRHANDLE Handle);
NTRIGISV_API bool NtrGesturesRegister (NTRHANDLE Handle, TNtrGestures *Gestures, HWND hWnd);
NTRIGISV_API void NtrGesturesUnRegister ();