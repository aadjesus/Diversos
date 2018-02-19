using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace ShapeShow.SystemConfig
{
    sealed class KeyboardManager
    {
        #region Private Members

        private delegate IntPtr HookHandlerDelegate(int nCode, IntPtr wParam, ref KBHookStruct lParam);
        private static HookHandlerDelegate callbackPtr;
        private static IntPtr hookPtr = IntPtr.Zero;
        private const int LowLevelKeyboardHook = 13;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookHandlerDelegate callbackPtr, IntPtr hInstance, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref KBHookStruct lParam);

        #endregion

        public static void DisableSystemKeys()
        {
            if (callbackPtr == null)
            {
                callbackPtr = new HookHandlerDelegate(KeyboardHookHandler);
            }

            if (hookPtr == IntPtr.Zero)
            {
                // Note: This does not work in the VS host environment.  To run in debug mode:
                // Project -> Properties -> Debug -> Uncheck "Enable the Visual Studio hosting process"
                IntPtr hInstance = Marshal.GetHINSTANCE(Application.Current.GetType().Module);
                hookPtr = SetWindowsHookEx(LowLevelKeyboardHook, callbackPtr, hInstance, 0);
            }
        }

        public static void EnableSystemKeys()
        {
            if (hookPtr != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hookPtr);
                hookPtr = IntPtr.Zero;
            }
        }

        private static IntPtr KeyboardHookHandler(int nCode, IntPtr wParam, ref KBHookStruct lParam)
        {
            if (nCode == 0)
            {
                if (((lParam.vkCode == 0x09) && (lParam.flags == 0x20)) ||  // Alt+Tab
                ((lParam.vkCode == 0x1B) && (lParam.flags == 0x20)) ||      // Alt+Esc
                ((lParam.vkCode == 0x1B) && (lParam.flags == 0x00)) ||      // Ctrl+Esc
                ((lParam.vkCode == 0x5B) && (lParam.flags == 0x01)) ||      // Left Windows Key
                ((lParam.vkCode == 0x5C) && (lParam.flags == 0x01)) ||      // Right Windows Key
                ((lParam.vkCode == 0x73) && (lParam.flags == 0x20)) ||      // Alt+F4
                ((lParam.vkCode == 0x20) && (lParam.flags == 0x20)))        // Alt+Space
                {
                    return new IntPtr(1);
                }
            }

            return CallNextHookEx(hookPtr, nCode, wParam, ref lParam);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KBHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
    }
}
