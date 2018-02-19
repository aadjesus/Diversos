using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;
using J832.Wpf;

namespace Microsoft.Samples.KMoore.WPFSamples.AnimatingTilePanel
{
    public partial class AnimatingTilePanelPage : Page
    {
        public AnimatingTilePanelPage()
        {
            InitializeComponent();

            this.LayoutUpdated += delegate(object sender, EventArgs e)
            {
                if (Panel == null)
                {
                    Panel = FindElementOfType<AnimatingTilePanel>(m_itemsControl);
                }
            };

            m_itemsControl.ItemsSource = m_colors;
            m_stackPanelCommands.DataContext = m_colors;

        }

        public static readonly DependencyProperty PanelProperty =
            DependencyProperty.Register("Panel", typeof(AnimatingTilePanel), typeof(AnimatingTilePanelPage));

        public AnimatingTilePanel Panel
        {
            get
            {
                return (AnimatingTilePanel)GetValue(PanelProperty);
            }
            set
            {
                SetValue(PanelProperty, value);
            }
        }

        private readonly DemoCollection<Brush> m_colors = DemoCollection<Brush>.Create(
            new Brush[] { Brushes.Red, Brushes.Orange, Brushes.Yellow, Brushes.Green, Brushes.Blue, Brushes.Violet }, 48, 0, 96);

        private static T FindElementOfType<T>(DependencyObject element) where T : DependencyObject
        {
            T found = element as T;

            if (found != null)
            {
                return found;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                found = FindElementOfType<T>(VisualTreeHelper.GetChild(element, i));
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }
    }
}