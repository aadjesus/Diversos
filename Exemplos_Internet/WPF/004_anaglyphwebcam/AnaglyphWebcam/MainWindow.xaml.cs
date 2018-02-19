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
using CatenaLogic.Windows.Presentation.WebcamPlayer;

namespace AnaglyphWebcam
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Create default devices
            SelectedWebcam1MonikerString = (CapDevice.DeviceMonikers.Length > 0) ? CapDevice.DeviceMonikers[0].MonikerString : "";
            SelectedWebcam2MonikerString = (CapDevice.DeviceMonikers.Length > 1) ? CapDevice.DeviceMonikers[1].MonikerString : "";
        }

        /// <summary>
        /// Wrapper for the SelectedWebcam dependency property
        /// </summary>
        public CapDevice SelectedWebcam1
        {
            get { return (CapDevice)GetValue(SelectedWebcam1Property); }
            set { SetValue(SelectedWebcam1Property, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedWebcam.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedWebcam1Property =
            DependencyProperty.Register("SelectedWebcam1", typeof(CapDevice), typeof(MainWindow), new UIPropertyMetadata(null));

        /// <summary>
        /// Wrapper for the SelectedWebcamMonikerString dependency property
        /// </summary>
        public string SelectedWebcam1MonikerString
        {
            get { return (string)GetValue(SelectedWebcam1MonikerStringProperty); }
            set { SetValue(SelectedWebcam1MonikerStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedWebcamMonikerString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedWebcam1MonikerStringProperty = DependencyProperty.Register("SelectedWebcam1MonikerString", typeof(string),
            typeof(MainWindow), new UIPropertyMetadata("", new PropertyChangedCallback(SelectedWebcam1MonikerString_Changed)));
        
        /// <summary>
        /// Invoked when the SelectedWebcamMonikerString dependency property has changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private static void SelectedWebcam1MonikerString_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get typed sender
            MainWindow typedSender = sender as MainWindow;
            if (typedSender != null)
            {
                // Get new value
                string newMonikerString = e.NewValue as string;

                // Update the device
                if (typedSender.SelectedWebcam1 == null)
                {
                    // Create it
                    typedSender.SelectedWebcam1 = new CapDevice("");
                }

                // Now set the moniker string
                typedSender.SelectedWebcam1.MonikerString = newMonikerString;
            }
        }

        /// <summary>
        /// Wrapper for the SelectedWebcam dependency property
        /// </summary>
        public CapDevice SelectedWebcam2
        {
            get { return (CapDevice)GetValue(SelectedWebcam2Property); }
            set { SetValue(SelectedWebcam2Property, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedWebcam.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedWebcam2Property =
            DependencyProperty.Register("SelectedWebcam2", typeof(CapDevice), typeof(MainWindow), new UIPropertyMetadata(null));

        /// <summary>
        /// Wrapper for the SelectedWebcamMonikerString dependency property
        /// </summary>
        public string SelectedWebcam2MonikerString
        {
            get { return (string)GetValue(SelectedWebcam2MonikerStringProperty); }
            set { SetValue(SelectedWebcam2MonikerStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedWebcamMonikerString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedWebcam2MonikerStringProperty = DependencyProperty.Register("SelectedWebcam2MonikerString", typeof(string),
            typeof(MainWindow), new UIPropertyMetadata("", new PropertyChangedCallback(SelectedWebcam2MonikerString_Changed)));

        /// <summary>
        /// Invoked when the SelectedWebcamMonikerString dependency property has changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private static void SelectedWebcam2MonikerString_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get typed sender
            MainWindow typedSender = sender as MainWindow;
            if (typedSender != null)
            {
                // Get new value
                string newMonikerString = e.NewValue as string;

                // Update the device
                if (typedSender.SelectedWebcam2 == null)
                {
                    // Create it
                    typedSender.SelectedWebcam2 = new CapDevice("");
                }

                // Now set the moniker string
                typedSender.SelectedWebcam2.MonikerString = newMonikerString;
            }
        }
    }
}
