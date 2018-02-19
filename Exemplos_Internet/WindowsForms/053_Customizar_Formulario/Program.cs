using System;
using System.Windows.Forms;

namespace BorderOverride
{
    public class Program
    {
        public static void Main( string[] args )
        {
            Application.EnableVisualStyles();
            Application.Run( new MainWindow() );
        }
    }
}
