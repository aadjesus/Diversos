using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Globalization;

namespace MaterialGroupsScreenSaver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : System.Windows.Application
    {

        void OnStartup(Object sender, StartupEventArgs e)
        {
            string[] args = e.Args;
            if (args.Length > 0)
            {
                // Get the 2 character command line argument
                string arg = args[0].ToLower(CultureInfo.InvariantCulture).Trim().Substring(0, 2);
                switch (arg)
                {
                    case "/c":
                        // Show the options dialog
                        Settings settings = new Settings();
                        settings.Show();
                        break;
                    case "/p":
                        // Don't do anything for preview
                        Application.Current.Shutdown();
                        break;
                    case "/s":
                        // Show screensaver form
                        ShowScreensaver();
                        break;
                    default:
                        Application.Current.Shutdown();
                        break;
                }
            }
            else
            {
                // If no arguments were passed in, show the screensaver
                ShowScreensaver();
            }



        }

        /// <summary>
        /// Shows screen saver by creating one instance of Window1 for each monitor.
        /// 
        /// Note: uses WinForms's Screen class to get monitor info.
        /// </summary>
        void ShowScreensaver()
        {
            //creates window on primary screen
            Window1 primaryWindow = new Window1();
            primaryWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            System.Drawing.Rectangle location = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            primaryWindow.WindowState = WindowState.Maximized;

            //creates window on other screens
            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (screen == System.Windows.Forms.Screen.PrimaryScreen)
                    continue;

                Window1 window = new Window1();
                window.WindowStartupLocation = WindowStartupLocation.Manual;
                location = screen.Bounds;

                //covers entire monitor
                window.Left = location.X - 7;
                window.Top = location.Y - 7;
                window.Width = location.Width + 14;
                window.Height = location.Height + 14;

            }

            //show non-primary screen windows
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window != primaryWindow)
                    window.Show();
            }

            ///shows primary screen window last
            primaryWindow.Show();
        }




    }


}