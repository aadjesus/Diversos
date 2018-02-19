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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;

namespace CycleTracks
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();

            // Setup backgroundworker to close the window automatically
            BackgroundWorker closeIt = new BackgroundWorker();
            closeIt.DoWork += (sender, e) =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                };
            closeIt.RunWorkerCompleted += (sender, e) =>
                {
                    this.Close();
                };

            // When window is loaded, start backgroundworker
            this.Loaded += (sender, e) =>
                {
                    closeIt.RunWorkerAsync();
                };
        }

        //private void ShowMainWindow(object sender, EventArgs e)
        //{
        //    MainWindow main = new MainWindow();
        //    main.Show();
        //}

        //private void CloseSplashScreen(object sender, MouseButtonEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
