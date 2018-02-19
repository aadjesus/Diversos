using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace PhysicsHost
{
    #region STRUCTS for P/Invoke
    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    };
    #endregion

    public class GlassEffect
    {
        #region P/Invoke
        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, 
            ref MARGINS pMarInset);
        

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern bool DwmIsCompositionEnabled();
        #endregion

        #region DPs
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled",
                typeof(Boolean),
                typeof(GlassEffect),
                new FrameworkPropertyMetadata(OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject element, Boolean value)
        {
            element.SetValue(IsEnabledProperty, value);
        }
        public static Boolean GetIsEnabled(DependencyObject element)
        {
            return (Boolean)element.GetValue(IsEnabledProperty);
        }

        public static void OnIsEnabledChanged(DependencyObject obj, 
            DependencyPropertyChangedEventArgs args)
        {
            if ((bool)args.NewValue == true)
            {
                Window wnd = (Window)obj;
                wnd.Loaded += new RoutedEventHandler(wnd_Loaded);
            }
        }
        

        static void wnd_Loaded(object sender, RoutedEventArgs e)
        {
            Window wnd = (Window)sender;
            Brush originalBackground = wnd.Background;
            wnd.Background = Brushes.Transparent;
            try
            {
                IntPtr mainWindowPtr = new WindowInteropHelper(wnd).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                mainWindowSrc.CompositionTarget.BackgroundColor = 
                    Color.FromArgb(0, 0, 0, 0);

                MARGINS margins = new MARGINS();
                margins.cxLeftWidth = -1;
                margins.cxRightWidth = -1;
                margins.cyTopHeight = -1;
                margins.cyBottomHeight = -1;

                DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
            }
            catch (DllNotFoundException)
            {
                wnd.Background = originalBackground;
            }
        }
        #endregion
    }

}
