using System;
using System.Windows;

namespace ShapeShow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SystemConfig.KeyboardManager.DisableSystemKeys();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SystemConfig.KeyboardManager.EnableSystemKeys();
            base.OnExit(e);
        }
    }
}
