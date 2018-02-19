using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Mogre;

namespace SmokeyCheeseCompany
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void RenterTargetControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _ogre.ViewportSize = e.NewSize;
        }

        private void Window1_OnLoaded(object sender, RoutedEventArgs e)
        {
            _ogre.InitOgre();

            _list.ItemsSource = new List<string>
                                    {
                                        "Things to do with cheese.",
                                        "Lastest smokey news."
                                    };
        }

        private void _ogre_OnInitialised(object sender, RoutedEventArgs e)
        {
            SceneManager sceneMgr = _ogre.SceneManager;

            SceneNode node = sceneMgr.RootSceneNode.CreateChildSceneNode();

            // smoke
            ParticleSystem smokeSystem = sceneMgr.CreateParticleSystem(
                "fountain1",
                "Examples/Smoke");

            node.AttachObject(smokeSystem);
            node.Translate(new Vector3(-150,-200,-400));
        }

        private void Window1_OnClosing(object sender, CancelEventArgs e)
        {
            _ogre.Dispose();
        }
    }
}
