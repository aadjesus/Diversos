//------------------------------------------
// WhatColor.cs (c) 2007 by Charles Petzold
//------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

class WhatColor : Window
{
    // Define external Win32 functions.
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT pt);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateDC(string strDriver, string strDevice,
                                         string strOutput, IntPtr pData);
    [DllImport("gdi32.dll")]
    public static extern bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern int GetPixel(IntPtr hdc, int x, int y);

    // Fields.
    TextBlock txtblkCoords;
    TextBlock txtblkRgbHex;
    TextBlock txtblkRgbDec;
    Color clrLast;
    POINT ptLast;

    [STAThread]
    public static void Main()
    {
        Application app = new Application();
        app.Run(new WhatColor());
    }
    public WhatColor()
    {
        // Define window characteristics.
        Title = "What Color?";
        Width = 120;
        Height = 75;
        WindowStyle = WindowStyle.SingleBorderWindow;
        ResizeMode = ResizeMode.CanMinimize;
        Topmost = true;

        // Create UniformGrid for TextBlock elements.
        UniformGrid unigrid = new UniformGrid();
        unigrid.Rows = 3;
        Content = unigrid;

        txtblkCoords = new TextBlock();
        txtblkCoords.HorizontalAlignment = HorizontalAlignment.Center;
        unigrid.Children.Add(txtblkCoords);

        txtblkRgbHex = new TextBlock();
        txtblkRgbHex.HorizontalAlignment = HorizontalAlignment.Center;
        unigrid.Children.Add(txtblkRgbHex);

        txtblkRgbDec = new TextBlock();
        txtblkRgbDec.HorizontalAlignment = HorizontalAlignment.Center;
        unigrid.Children.Add(txtblkRgbDec);

        // Set a timer for 0.1 seconds.
        DispatcherTimer tmr = new DispatcherTimer();
        tmr.Interval = TimeSpan.FromMilliseconds(100);
        tmr.Tick += TimerOnTick;
        tmr.Start();
    }
    void TimerOnTick(object sender, EventArgs args)
    {
        // Get the current mouse position in screen coordinates.
        POINT pt;
        GetCursorPos(out pt);

        // If it's different from the last, update the display.
        if (ptLast.X != pt.X || ptLast.Y != pt.Y)
        {
            txtblkCoords.Text = String.Format("({0}, {1})", pt.X, pt.Y);
            ptLast = pt;
        }

        // Call three API functions to get the pixel color.
        IntPtr hdcScreen = CreateDC("Display", null, null, IntPtr.Zero);
        int cr = GetPixel(hdcScreen, pt.X, pt.Y);
        DeleteDC(hdcScreen);

        // Convert a Win32 COLORREF to a .NET Color object.
        Color clr = Color.FromRgb((byte) (cr & 0x000000FF),
                                  (byte)((cr & 0x0000FF00) >> 8),
                                  (byte)((cr & 0x00FF0000) >> 16));

        // Only update if there's a new color.
        if (clr != clrLast)
        {
            txtblkRgbHex.Text = String.Format("{0:X2}-{1:X2}-{2:X2}", 
                                              clr.R, clr.G, clr.B);
            txtblkRgbDec.Text = String.Format("{0}-{1}-{2}", 
                                              clr.R, clr.G, clr.B);
            clrLast = clr;
        }
    }
}
