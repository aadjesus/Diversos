using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using MohammadDayyanCalendar;
using System.Windows.Threading;

namespace Clock
{
	public partial class Window1
	{
        System.Timers.Timer timer = new System.Timers.Timer(1000);

        public Window1()
        {
            InitializeComponent();

            MDCalendar mdCalendar = new MDCalendar();
            DateTime date = DateTime.Now;
            TimeZone time = TimeZone.CurrentTimeZone;
            TimeSpan difference = time.GetUtcOffset(date);
            uint currentTime = mdCalendar.Time() + (uint)difference.TotalSeconds;
            persianCalendar.Content = mdCalendar.Date("Y/m/D  W", currentTime, true);
            christianityCalendar.Content = mdCalendar.Date("P Z/e/d", currentTime, false);

            persianCalendar1.Content = mdCalendar.Date("Y/m/D  W", currentTime, true);
            christianityCalendar1.Content = mdCalendar.Date("P Z/e/d", currentTime, false);

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //http://thispointer.spaces.live.com/blog/cns!74930F9313F0A720!252.entry?_c11_blogpart_blogpart=blogview&_c=blogpart#permalink
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                secondHand.Angle = DateTime.Now.Second * 6; secondHand1.Angle = DateTime.Now.Second * 6;
                minuteHand.Angle = DateTime.Now.Minute * 6; minuteHand1.Angle = DateTime.Now.Minute * 6;
                hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5); hourHand1.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5);
            }));
        }

        private void rootLayout_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
	}
}