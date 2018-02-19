using System.Windows.Controls;
using J832.Wpf;

namespace Microsoft.Samples.KMoore.WPFSamples.Zap
{
    public partial class ZapPage : Page
    {
        public ZapPage()
        {
            InitializeComponent();

            m_tabItemColors.DataContext = m_strings;
        }

        private readonly DemoCollection<string> m_strings =
            DemoCollection<string>.Create(new string[] { "red", "orange", "yellow", "green", "blue", "violet" }, 6, 0, 12);

    }
}