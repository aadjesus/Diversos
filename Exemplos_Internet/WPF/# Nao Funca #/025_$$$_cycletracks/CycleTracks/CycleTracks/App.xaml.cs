using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace CycleTracks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ShowSplashWindow(object sender, StartupEventArgs e)
        {
          XceedDeploymentLicense.SetLicense();
          // Commented out to facilitate debugging
          SplashScreen splash = new SplashScreen();
          splash.Show();
        }
    }
}
