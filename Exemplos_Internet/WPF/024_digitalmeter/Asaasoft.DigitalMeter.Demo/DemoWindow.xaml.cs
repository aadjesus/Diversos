using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Asaasoft.DigitalMeter.Demo
{
    /// <summary>
    /// Interaction logic for DemoWindow.xaml
    /// </summary>
    public partial class DemoWindow : Window
    {
        #region Class Member Variables
        Random random = new Random();
        #endregion

        #region Constructor
        public DemoWindow()
        {
            InitializeComponent();

            RunDigitalMeter(digitalMeter1, digitalMeterStatusTextBlock1, 10000);
            RunDigitalMeter(digitalMeter2, digitalMeterStatusTextBlock2, 100);
            RunDigitalMeter(digitalMeter3, digitalMeterStatusTextBlock3, 1000);
            RunDigitalMeter(digitalMeter4, digitalMeterStatusTextBlock4, 1000000);
            RunDigitalMeter(digitalMeter5, digitalMeterStatusTextBlock5, 10);
            RunDigitalMeter(digitalMeter6, digitalMeterStatusTextBlock6, 1000000);
        }
        #endregion

        #region Private Methods
        private void RunDigitalMeter( DigitalMeter digitalMeter, TextBlock associatedTextBlock, int max )
        {
            associatedTextBlock.Foreground = digitalMeter.Foreground;
            associatedTextBlock.Background = digitalMeter.Background;
            associatedTextBlock.Text = "Digital Meter is not started";
            digitalMeter.ValueChanged += delegate( object sender, ValueChangedEventArgs e )
            {
                associatedTextBlock.Text = string.Format("Old Value={0,-10} \t New Value={1,-10}", e.OldValue, e.NewValue);
            };

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds( random.Next( 1, 5 ) );
            dispatcherTimer.Tick += delegate
            {
                digitalMeter.Value = random.Next(0, max);
            };
            dispatcherTimer.Start();
        }
        #endregion
    }
}
