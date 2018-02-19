using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.ServiceModel;
using System.Threading;

namespace DrawMe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool s_IsServer = false;
        protected override void OnStartup(StartupEventArgs e)
        {
        }

        public static bool s_bShutDownServerThread = false;
        private static System.Threading.Thread s_serverThread;
        public static void StartServer()
        {
            s_bShutDownServerThread = false;
            s_serverThread = new Thread(new ThreadStart(DrawMeServerThread.Run));
            s_serverThread.Start();
        }

        public static void StopServer()
        {
            s_bShutDownServerThread = true;
            if (s_serverThread != null)
            {
                s_serverThread.Join(1000);
            }
        }
    }
}
