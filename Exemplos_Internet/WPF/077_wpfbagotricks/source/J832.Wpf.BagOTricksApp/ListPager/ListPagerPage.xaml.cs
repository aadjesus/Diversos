using System;
using System.Collections;
using System.Windows.Controls;

namespace Microsoft.Samples.KMoore.WPFSamples.ListPager
{

    public partial class ListPagerPage : Page
    {

        public ListPagerPage()
        {
            InitializeComponent();

            setItems(100);
        }

        private void Click(object sender, EventArgs args)
        {
            setItems();
        }

        private void setItems()
        {
            setItems(m_random.Next(5, 15) * 3);
        }

        private void setItems(int number)
        {
            IList values = GetStrings(number);

            ListPager lp = (ListPager)this.FindResource("listPager");

            lp.ItemsSource = values;
        }

        private static string[] GetStrings(int count)
        {
            string[] strings = new string[count];

            for (int i = 0; i < count; i++)
            {
                strings[i] = "Item " + (i + 1).ToString();
            }
            return strings;
        }

        private readonly Random m_random = new Random();

    }
}