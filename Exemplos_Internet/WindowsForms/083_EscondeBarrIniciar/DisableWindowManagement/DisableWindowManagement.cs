using System;
using System.Runtime.InteropServices;

namespace DisableWindowManagement
{
	public class DisableWindowManagement
	{	
		[DllImport("user32.dll")]
		private static extern IntPtr FindWindow(string className, string windowText);

		/// <summary>
		/// This slightly non-standard version of FindWindowEx is the key to
		/// making this solution work.
		/// </summary>
		/// <param name="parentHwnd"></param>
		/// <param name="childAfterHwnd"></param>
		/// <param name="className">use an IntPtr instead of a string for the className, this causes the ATOM code to be invoked.</param>
		/// <param name="windowText"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

		[DllImport("user32.dll")]
		private static extern int ShowWindow(IntPtr hwnd, int command);



		private const int SW_HIDE = 0;
		private const int SW_SHOW = 1;

		IntPtr hwndTaskBar;
		IntPtr hwndTaskBarButton;


		/// <summary>
		/// This constructor gets the window handles for the Taskbar and the
		/// Start menu globe.
		/// </summary>
		public DisableWindowManagement()
		{
			this.hwndTaskBar = FindWindow("Shell_TrayWnd", "");
			this.hwndTaskBarButton = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);


		}

		/// <summary>
		/// This method hides both the Taskbar and the Globe.
		/// </summary>
		public void DisableTaskBar()
		{
			ShowWindow(this.hwndTaskBar, SW_HIDE);
			ShowWindow(this.hwndTaskBarButton, SW_HIDE);
		}

		/// <summary>
		/// This method shows both the Taskbar and the Globe.
		/// </summary>
		public void EnableTaskBar()
		{
			ShowWindow(this.hwndTaskBar, SW_SHOW);
			ShowWindow(this.hwndTaskBarButton, SW_SHOW);
		}
 
	}
}
