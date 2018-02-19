using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Mogre;

using OgreLib;

namespace OgreHead
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private SceneNode _ogreNode;
        private Entity _ogreMesh;
        private SceneNode _fountainNode;
        private Viewport _viewport;

        public Window1()
        {
            App.Current.Exit += Current_Exit;

            InitializeComponent();
        }

        void Current_Exit(object sender, ExitEventArgs e)
        {
            RenterTargetControl.Source = null;

            _ogreImage.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ogreImage.InitOgreAsync();
        }

        private void RenterTargetControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_ogreImage == null) return;

            _ogreImage.ViewportSize = e.NewSize;
        }

        void _image_InitScene(object sender, RoutedEventArgs e)
        {
 	    // start the scene fade in animation
            var sb = (Storyboard)this.Resources["StartEngine"];
            sb.Begin(this);

            // get the Ogre scene manager
            SceneManager sceneMgr = _ogreImage.SceneManager;

            // ceate a light
            Light l = sceneMgr.CreateLight("MainLight");

            // Accept default settings: point light, white diffuse, just set position
            // NB I could attach the light to a SceneNode if I wanted it to move automatically with
            //  other objects.
            l.Position = new Vector3(20F, 80F, 50F);

            // load the "ogre head mesh" resource.
            _ogreMesh = sceneMgr.CreateEntity("ogre", "ogrehead.mesh");

            // create a node for the "ogre head mesh"
            _ogreNode = sceneMgr.RootSceneNode.CreateChildSceneNode("ogreNode");
            _ogreNode.AttachObject(_ogreMesh);

            // Create shared node for the particle effects
            _fountainNode = sceneMgr.RootSceneNode.CreateChildSceneNode();

            // Set nonvisible timeout
            ParticleSystem.DefaultNonVisibleUpdateTimeout = 5;

            // update the partical systems
            _cb_Click(null, null);
        }

        void _image_PreRender(object sender, System.EventArgs e)
        {
	    // if the viewport has changed reload the special effects
            if (_ogreImage.Camera.Viewport != _viewport)
            {
                _viewport = _ogreImage.Camera.Viewport;

                _cbCompositor_Click(_cbBloom, null);
                _cbCompositor_Click(_cbGlass, null);
                _cbCompositor_Click(_cbOldTV, null);
            }

            // rotote the "ogre head"
            _ogreNode.Rotate(new Vector3(0, 1, 0), new Degree(0.1f));
        }

        private void RenterTargetControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Clicked");
        }

        private void _cb_Click(object sender, RoutedEventArgs e)
        {
            SceneManager sceneMgr = _ogreImage.SceneManager;
            if (sceneMgr == null) return;

            _fountainNode.RemoveAndDestroyAllChildren();
            sceneMgr.DestroyAllParticleSystems();

            SceneNode fNode;

            if (_cbFireworks.IsChecked == true) //isChecked is nullable
            {
                ParticleSystem pSys1 = sceneMgr.CreateParticleSystem("fireworks", "Examples/Fireworks");

                _fountainNode.CreateChildSceneNode().AttachObject(pSys1);
            }

            if (_cbPurpleFountain.IsChecked == true)
            {
                // fountain 1
                ParticleSystem pSys2 = sceneMgr.CreateParticleSystem("fountain1",
                                                                     "Examples/PurpleFountain");

                // Point the fountain at an angle
                fNode = _fountainNode.CreateChildSceneNode();
                fNode.Translate(200, -100, 0);
                fNode.Rotate(Vector3.UNIT_Z, new Degree(20));
                fNode.AttachObject(pSys2);

                // fountain 2
                ParticleSystem pSys3 = sceneMgr.CreateParticleSystem("fountain2",
                    "Examples/PurpleFountain");
                // Point the fountain at an angle
                fNode = _fountainNode.CreateChildSceneNode();
                fNode.Translate(-200, -100, 0);
                fNode.Rotate(Vector3.UNIT_Z, new Degree(-20));
                fNode.AttachObject(pSys3);
            }

            if (_cbRain.IsChecked == true)
            {
                // Create a rainstorm
                ParticleSystem pSys4 = sceneMgr.CreateParticleSystem("rain",
                                                                     "Examples/Rain");
                TextureHelper.CalculateAllAlphas(pSys4);

                SceneNode rNode = sceneMgr.RootSceneNode.CreateChildSceneNode();
                rNode.Translate(0, 1000, 0);
                rNode.AttachObject(pSys4);
                // Fast-forward the rain so it looks more natural
                pSys4.FastForward(5);
            }

            if (_cbAureola.IsChecked == true)
            {
                // Aureola around Ogre perpendicular to the ground
                ParticleSystem pSys5 = sceneMgr.CreateParticleSystem("aureola",
                                                                     "Examples/Aureola");
                sceneMgr.RootSceneNode.CreateChildSceneNode().AttachObject(pSys5);
            }
        }

        private void _cbOgreMaterials_Click(object sender, RoutedEventArgs e)
        {
            SceneManager sceneMgr = _ogreImage.SceneManager;
            if (sceneMgr == null) return;

            const uint CUSTOM_SHININESS = 1;
            const uint CUSTOM_DIFFUSE = 2;
            const uint CUSTOM_SPECULAR = 3;

            if (_cbOgreMaterials.IsChecked == true)
            {
                // Set common material, but define custom parameters to change colours
                // See Example-Advanced.material for how these are finally bound to GPU parameters
                SubEntity sub;

                // eyes
                sub = _ogreMesh.GetSubEntity(0);
                sub.MaterialName = ("Examples/CelShading");
                sub.SetCustomParameter(CUSTOM_SHININESS, new Vector4(35.0f, 0.0f, 0.0f, 0.0f));
                sub.SetCustomParameter(CUSTOM_DIFFUSE, new Vector4(1.0f, 0.3f, 0.3f, 1.0f));
                sub.SetCustomParameter(CUSTOM_SPECULAR, new Vector4(1.0f, 0.6f, 0.6f, 1.0f));

                // skin
                sub = _ogreMesh.GetSubEntity(1);
                sub.MaterialName = ("Examples/CelShading");
                sub.SetCustomParameter(CUSTOM_SHININESS, new Vector4(10.0f, 0.0f, 0.0f, 0.0f));
                sub.SetCustomParameter(CUSTOM_DIFFUSE, new Vector4(0.0f, 0.5f, 0.0f, 1.0f));
                sub.SetCustomParameter(CUSTOM_SPECULAR, new Vector4(0.3f, 0.5f, 0.3f, 1.0f));

                // earring
                sub = _ogreMesh.GetSubEntity(2);
                sub.MaterialName = ("Examples/CelShading");
                sub.SetCustomParameter(CUSTOM_SHININESS, new Vector4(25.0f, 0.0f, 0.0f, 0.0f));
                sub.SetCustomParameter(CUSTOM_DIFFUSE, new Vector4(1.0f, 1.0f, 0.0f, 1.0f));
                sub.SetCustomParameter(CUSTOM_SPECULAR, new Vector4(1.0f, 1.0f, 0.7f, 1.0f));

                // teeth
                sub = _ogreMesh.GetSubEntity(3);
                sub.MaterialName = ("Examples/CelShading");
                sub.SetCustomParameter(CUSTOM_SHININESS, new Vector4(20.0f, 0.0f, 0.0f, 0.0f));
                sub.SetCustomParameter(CUSTOM_DIFFUSE, new Vector4(1.0f, 1.0f, 0.7f, 1.0f));
                sub.SetCustomParameter(CUSTOM_SPECULAR, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            }
            else
            {
                sceneMgr.DestroyEntity(_ogreMesh);
                _ogreMesh = sceneMgr.CreateEntity("ogre", "ogrehead.mesh");
                _ogreNode.AttachObject(_ogreMesh);
            }
        }

        private void _cbCompositor_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;

            string name = null;
            if (checkBox == _cbBloom)
                name = "Bloom";
            if (checkBox == _cbGlass)
                name = "Glass";
            if (checkBox == _cbOldTV)
                name = "Old TV";

            if (name != null)
            {
                if (checkBox.IsChecked == true)
                {
                    CompositorInstance instance = CompositorManager.Singleton.AddCompositor(_viewport, name, 0);
                    if (instance != null)
                        instance. Enabled = true;
                }
                else
                {
                    CompositorManager.Singleton.RemoveCompositor(_viewport, name);
                }
            }
        }

        private void OgreImage_ResourceLoadItemProgress(object sender, ResourceLoadEventArgs e)
        {
            _progressName.Text = e.Name;

            // scale the progress bar
            _progressScale.ScaleX = e.Progress;
        }

        private void StartEngine_Completed(object sender, EventArgs e)
        {
            _loading.Visibility = Visibility.Collapsed;
        }
    }
}
