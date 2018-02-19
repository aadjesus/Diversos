
namespace GestureLib
{
    #region Using Statements
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    #endregion

    #region Enum
    /// <summary>
    /// 
    /// </summary>
    public enum ENtrGestureType: ushort
    {
        ScrollV,
        ScrollH,
        Zoom,
        Rotate,
        DoubleTap
    };
    #endregion

    #region Structs
    [StructLayout(LayoutKind.Explicit)]
    public struct TNtrGestures
    {
        [FieldOffset(0)]public bool ReceiveZoom;
        [FieldOffset(1)]public bool UseUserZoomSettings;
        [FieldOffset(2)]public bool ReceiveScroll;
        [FieldOffset(3)]public bool UseUserScrollSettings;
        [FieldOffset(4)]public bool ReceiveFingersDoubleTap;
        [FieldOffset(5)]public bool UseUserFingersDoubleTapSettings;
        [FieldOffset(6)]public bool ReceiveRotate;
        [FieldOffset(7)]public bool UseUserRotateSettings;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TNtrGestureZoom
    {
        public double mAmount;
        public ushort X;
        public ushort Y;
        public ushort Width;
        public ushort Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TNtrGestureScrollV
    {
        public double mAmount;
        public ushort X;
        public ushort Y;
        public ushort Width;
        public ushort Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TNtrGestureScrollH
    {
        public double mAmount;
        public ushort X;
        public ushort Y;
        public ushort Width;
        public ushort Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TNtrGestureRotate
    {
        public double mAmount;
        public ushort X;
        public ushort Y;
        public ushort Width;
        public ushort Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TNtrGestureDoubleTap
    {
        public ushort X;
        public ushort Y;
        public ushort Width;
        public ushort Height;
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public sealed class Gestures
    {
        #region Constants
        public const string NTR_WM_GESTURE_NAME = "NtrOnGestureWindowsMessage";
        #endregion

        #region DllImports
        [DllImport("NtrigISV.dll", EntryPoint="?NtrGesturesConnect@@YAPAXXZ", CharSet = CharSet.Auto)]
        public static extern IntPtr NtrGesturesConnect();

        [DllImport("NtrigISV.dll", EntryPoint = "?NtrGesturesDisconnect@@YAXPAX@Z", CharSet = CharSet.Auto)]
        public static extern void NtrGesturesDisconnect(IntPtr ntrHandle);

        [DllImport("NtrigISV.dll", EntryPoint = "?NtrGesturesRegister@@YA_NPAXPAU_TNtrGestures@@PAUHWND__@@@Z", CharSet = CharSet.Auto)]
        public static extern bool NtrGesturesRegister(IntPtr ntrHandle, ref TNtrGestures ntrGestures, IntPtr hWnd);

        [DllImport("NtrigISV.dll", EntryPoint = "?NtrGesturesUnRegister@@YAXXZ", CharSet = CharSet.Auto)]
        public static extern void NtrGesturesUnRegister();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);
        #endregion

        #region Members
        private IntPtr ntrHandle = IntPtr.Zero;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Gestures()
        {
        }
        #endregion

        #region RegisterGestureWinMessage Method
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int RegisterGestureWinMessage()
        {
            return RegisterWindowMessage(NTR_WM_GESTURE_NAME);
        }
        #endregion

        #region Connect Method
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            this.ntrHandle = NtrGesturesConnect();

            if (this.ntrHandle != IntPtr.Zero)
                return true;

            return false;
        }
        #endregion

        #region Disconnect Method
        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            NtrGesturesDisconnect(this.ntrHandle);
        }
        #endregion

        #region RegisterGestures Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="ntrGestures"></param>
        /// <returns></returns>
        public bool RegisterGestures(IntPtr hwnd, TNtrGestures ntrGestures)
        {
            if (hwnd == IntPtr.Zero)
                return false;

            if (this.ntrHandle == IntPtr.Zero)
                return false;

            return NtrGesturesRegister(this.ntrHandle, ref ntrGestures, hwnd);
        }
        #endregion

        #region ProcessMessage Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public GestureData ProcessMessage(Message msg)
        {
            if (msg == null)
                return null;

            ENtrGestureType gType = (ENtrGestureType)msg.WParam;

            GestureData gData = null;

            switch (gType)
            {
                case ENtrGestureType.Zoom:
                    GestureZoomData gZoomData = new GestureZoomData(msg.WParam);
                    gZoomData.ZoomStruct = (TNtrGestureZoom)msg.GetLParam(typeof(TNtrGestureZoom));
                    gData = gZoomData;
                    break;

                case ENtrGestureType.Rotate:
                    GestureRotateData gRotateData = new GestureRotateData(msg.WParam);
                    gRotateData.RotateStruct = (TNtrGestureRotate)msg.GetLParam(typeof(TNtrGestureRotate));
                    gData = gRotateData;
                    break;

                case ENtrGestureType.ScrollH:
                    GestureScrollHData gScrollHData = new GestureScrollHData(msg.WParam);
                    gScrollHData.ScrollHStruct = (TNtrGestureScrollH)msg.GetLParam(typeof(TNtrGestureScrollH));
                    gData = gScrollHData;
                    break;

                case ENtrGestureType.ScrollV:
                    GestureScrollVData gScrollVData = new GestureScrollVData(msg.WParam);
                    gScrollVData.ScrollVStruct = (TNtrGestureScrollV)msg.GetLParam(typeof(TNtrGestureScrollV));
                    gData = gScrollVData;
                    break;
            }

            return gData;
        }
        #endregion

        #region ProcessMessage Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lParam"></param>
        /// <param name="wParam"></param>
        /// <returns></returns>
        public GestureData ProcessMessage(IntPtr lParam, IntPtr wParam)
        {
            ENtrGestureType gType = (ENtrGestureType)wParam;

            GestureData gData = null;

            switch (gType)
            {
                case ENtrGestureType.Zoom:
                    GestureZoomData gZoomData = new GestureZoomData(wParam);
                    gZoomData.ZoomStruct = (TNtrGestureZoom)Marshal.PtrToStructure(lParam, typeof(TNtrGestureZoom));
                    gData = gZoomData;
                    break;

                case ENtrGestureType.Rotate:
                    GestureRotateData gRotateData = new GestureRotateData(wParam);
                    gRotateData.RotateStruct = (TNtrGestureRotate)Marshal.PtrToStructure(lParam, typeof(TNtrGestureRotate));
                    gData = gRotateData;
                    break;

                case ENtrGestureType.ScrollH:
                    GestureScrollHData gScrollHData = new GestureScrollHData(wParam);
                    gScrollHData.ScrollHStruct = (TNtrGestureScrollH)Marshal.PtrToStructure(lParam, typeof(TNtrGestureScrollH));
                    gData = gScrollHData;
                    break;

                case ENtrGestureType.ScrollV:
                    GestureScrollVData gScrollVData = new GestureScrollVData(wParam);
                    gScrollVData.ScrollVStruct = (TNtrGestureScrollV)Marshal.PtrToStructure(lParam, typeof(TNtrGestureScrollV));
                    gData = gScrollVData;
                    break;
            }

            return gData;
        }
        #endregion
    } // end of class
} // end of namespace