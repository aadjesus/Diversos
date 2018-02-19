using System.Windows;
using System.Linq;

namespace WhiteBoxSecurity
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            string[] xxx = new string[] { "1", "2", "3", "5", "6", "7", "8" };
            var xa = from a in xxx
                     where a.Equals("10")
                     select a;
            if (xa.Count() == 1)
            {

            }
        }
    }
}
