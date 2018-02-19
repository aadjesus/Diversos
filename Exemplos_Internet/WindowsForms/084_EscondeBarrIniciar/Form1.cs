using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace howto_hide_taskbar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [Flags]
        private enum SetWindowPosFlags : uint
        {
            SWP_ASYNCWINDOWPOS = 0x4000,
            SWP_DEFERERASE = 0x2000,
            SWP_DRAWFRAME = 0x0020,
            SWP_FRAMECHANGED = 0x0020,
            SWP_HIDEWINDOW = 0x0080,
            SWP_NOACTIVATE = 0x0010,
            SWP_NOCOPYBITS = 0x0100,
            SWP_NOMOVE = 0x0002,
            SWP_NOOWNERZORDER = 0x0200,
            SWP_NOREDRAW = 0x0008,
            SWP_NOREPOSITION = 0x0200,
            SWP_NOSENDCHANGING = 0x0400,
            SWP_NOSIZE = 0x0001,
            SWP_NOZORDER = 0x0004,
            SWP_SHOWWINDOW = 0x0040,
        }

        [DllImport("user32", EntryPoint = "FindWindowA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);

        private enum GetWindowCmd : uint {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }

        // The taskbar's window handle.
        private IntPtr TaskbarHWnd, StartButtonHWnd;

        // Hide the taskbar and start button.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Get the taskbar's and start button's window handles.
            TaskbarHWnd = FindWindow("Shell_traywnd", "");
            StartButtonHWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);

            // Hide the taskbar and start button.
            SetWindowPos(TaskbarHWnd, IntPtr.Zero, 0, 0, 0, 0,
                SetWindowPosFlags.SWP_HIDEWINDOW);
            SetWindowPos(StartButtonHWnd, IntPtr.Zero, 0, 0, 0, 0,
                SetWindowPosFlags.SWP_HIDEWINDOW);

            // Maximize.
            this.Bounds = Screen.PrimaryScreen.Bounds;
        }

        // Restore the taskbar and start button.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetWindowPos(TaskbarHWnd, IntPtr.Zero, 0, 0, 0, 0,
                SetWindowPosFlags.SWP_SHOWWINDOW);
            SetWindowPos(StartButtonHWnd, IntPtr.Zero, 0, 0, 0, 0,
                SetWindowPosFlags.SWP_HIDEWINDOW);
        }
    }
}
