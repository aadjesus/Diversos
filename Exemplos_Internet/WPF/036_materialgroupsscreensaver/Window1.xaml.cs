using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DemoDev;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace MaterialGroupsScreenSaver
{
    /// <summary>
    /// Interaction logic for Window1
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();
        }


        void OnLoaded(object sender, EventArgs e)
        {
            //Initialize background color with BackgroundColor application setting
            TypeConverter colorTypeConverter = TypeDescriptor.GetConverter(typeof(Color));
            Background = new SolidColorBrush((Color)colorTypeConverter.ConvertFrom(Properties.Settings.Default.BackgroundColor));

            //Start animations
            Storyboard s;

            s = (Storyboard)this.FindResource("ImageBrushOne");
            this.BeginStoryboard(s);
            s = (Storyboard)this.FindResource("ImageBrushTwo");
            this.BeginStoryboard(s);
            s = (Storyboard)this.FindResource("ImageBrushThree");
            this.BeginStoryboard(s);
            s = (Storyboard)this.FindResource("ImageBrushFour");
            this.BeginStoryboard(s);
#if !DEBUG
            Topmost = true;
            MouseMove += new MouseEventHandler(PhotoStack_MouseMove);
            MouseDown += new MouseButtonEventHandler(PhotoStack_MouseDown);
            KeyDown += new KeyEventHandler(PhotoStack_KeyDown);
#endif
        }

        void PhotoStack_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void PhotoStack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void PhotoStack_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.MouseDevice.GetPosition(this);
            // Set IsActive and MouseLocation only the first time this event is called.
            if (!isActive)
            {
                mousePosition = currentPosition;
                isActive = true;
            }
            else
            {
                // If the mouse has moved significantly since first call, close.
                if ((Math.Abs(mousePosition.X - currentPosition.X) > 10) ||
                    (Math.Abs(mousePosition.Y - currentPosition.Y) > 10))
                {
                    Application.Current.Shutdown();
                }
            }
        }


        bool isActive;
        Point mousePosition;
    }
}