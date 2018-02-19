using System;
using System.Diagnostics;
using System.Windows;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.Query;
using System.Xml.XLinq;
using System.Configuration;

namespace LINQWinFXApplication1
{
    /// <summary>
    /// Interaction logic for MyApp.xaml
    /// </summary>

    public partial class MyApp : Application
    {

        void AppStartup(object sender, StartupEventArgs args)
        {
            Window1 mainWindow = new Window1();
            mainWindow.Show();
        }

    }
}