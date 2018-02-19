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
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;

namespace CursorPosition
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;            
        }

        #region methods

        //http://www.geekpedia.com/tutorial146_Get-screen-cursor-coordinates.html
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref System.Drawing.Point lpPoint);
        ///
        float coefficient = 0.02f;

        void timer_Tick(object sender, EventArgs e)
        {
            // New point that will be updated by the function with the current coordinates
            System.Drawing.Point mouseposition = new System.Drawing.Point();
            // Call the function and pass the Point, defPnt
            GetCursorPos(ref mouseposition);

            if (mouseposition.X < this.Left)
                mamadScaleTransform.ScaleX = 1;
            else
                mamadScaleTransform.ScaleX = -1;

            this.Left += (mouseposition.X - this.Left) * coefficient;
            this.Top += (mouseposition.Y - this.Top) * coefficient;
        }

        #endregion
    }
}