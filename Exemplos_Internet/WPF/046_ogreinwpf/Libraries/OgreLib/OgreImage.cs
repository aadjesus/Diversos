using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Mogre;

namespace OgreLib
{
    public partial class OgreImage : D3DImage,
                                     ISupportInitialize
    {
        private delegate void MethodInvoker();

        private Root _root;
        private TexturePtr _texture;
        private RenderWindow _renderWindow;
        private Camera _camera;
        private Viewport _viewport;
        private SceneManager _sceneManager;
        private RenderTarget _renTarget;
        private int _reloadRenderTargetTime;
        private bool _imageSourceValid;
        private Thread _currentThread;
        private DispatcherTimer _timer;
        private bool _eventAttatched;

        #region IDisposable Members

        public void Dispose()
        {
            IsFrontBufferAvailableChanged -= _isFrontBufferAvailableChanged;

            DetachRenderTarget(true, true);

            if (_currentThread != null)
            {
                _currentThread.Abort();
            }

            if (_root != null)
            {
                DisposeRenderTarget();
                CompositorManager.Singleton.RemoveAll();

                _root.Dispose();
                _root = null;
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        #region ISupportInitialize Members

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            if (AutoInitialise)
            {
                InitOgre();
            }
        }

        #endregion

        protected bool _InitOgre()
        {
            lock (this)
            {
                IntPtr hWnd = IntPtr.Zero;

                foreach (PresentationSource source in PresentationSource.CurrentSources)
                {
                    var hwndSource = (source as HwndSource);
                    if (hwndSource != null)
                    {
                        hWnd = hwndSource.Handle;
                        break;
                    }
                }

                if (hWnd == IntPtr.Zero) return false;

                CallResourceItemLoaded(new ResourceLoadEventArgs("Engine", 0));

                // load the OGRE engine
                //
                _root = new Root();

                // configure resource paths from : "resources.cfg" file
                //
                var configFile = new ConfigFile();
                configFile.Load("resources.cfg", "\t:=", true);

                // Go through all sections & settings in the file
                //
                ConfigFile.SectionIterator seci = configFile.GetSectionIterator();

                // Normally we would use the foreach syntax, which enumerates the values, 
                // but in this case we need CurrentKey too;
                while (seci.MoveNext())
                {
                    string secName = seci.CurrentKey;

                    ConfigFile.SettingsMultiMap settings = seci.Current;
                    foreach (var pair in settings)
                    {
                        string typeName = pair.Key;
                        string archName = pair.Value;
                        ResourceGroupManager.Singleton.AddResourceLocation(archName, typeName, secName);
                    }
                }

                // Configures the application and creates the Window
                // A window HAS to be created, even though we'll never use it.
                //
                bool foundit = false;
                foreach (RenderSystem rs in _root.GetAvailableRenderers())
                {
                    if (rs == null) continue;

                    _root.RenderSystem = rs;
                    String rname = _root.RenderSystem.Name;
                    if (rname == "Direct3D9 Rendering Subsystem")
                    {
                        foundit = true;
                        break;
                    }
                }

                if (!foundit)
                    return false;

                _root.RenderSystem.SetConfigOption("Full Screen", "No");
                _root.RenderSystem.SetConfigOption("Video Mode", "640 x 480 @ 32-bit colour");

                _root.Initialise(false);

                var misc = new NameValuePairList();
                misc["externalWindowHandle"] = hWnd.ToString();
                _renderWindow = _root.CreateRenderWindow("OgreImageSource Windows", 0, 0, false, misc);
                _renderWindow.IsAutoUpdated = false;

                InitResourceLoad();
                ResourceGroupManager.Singleton.InitialiseAllResourceGroups();

                this.Dispatcher.Invoke(
                    (MethodInvoker)delegate
                                       {
                                           if (CreateDefaultScene)
                                           {
                                               //----------------------------------------------------- 
                                               // 4 Create the SceneManager
                                               // 
                                               //		ST_GENERIC = octree
                                               //		ST_EXTERIOR_CLOSE = simple terrain
                                               //		ST_EXTERIOR_FAR = nature terrain (depreciated)
                                               //		ST_EXTERIOR_REAL_FAR = paging landscape
                                               //		ST_INTERIOR = Quake3 BSP
                                               //----------------------------------------------------- 
                                               _sceneManager = _root.CreateSceneManager(SceneType.ST_GENERIC, "SceneMgr");
                                               _sceneManager.AmbientLight = new ColourValue(0.5f, 0.5f, 0.5f);

                                               //----------------------------------------------------- 
                                               // 5 Create the camera 
                                               //----------------------------------------------------- 
                                               _camera = _sceneManager.CreateCamera("DefaultCamera");
                                               _camera.Position = new Vector3(0f, 0f, 100f);
                                               // Look back along -Z
                                               _camera.LookAt(new Vector3(0f, 0f, -300f));
                                               _camera.NearClipDistance = 5;
                                           }

                                           IsFrontBufferAvailableChanged += _isFrontBufferAvailableChanged;

                                           if (Initialised != null)
                                               Initialised(this, new RoutedEventArgs());

                                           ReInitRenderTarget();
                                           AttachRenderTarget(true);

                                           OnFrameRateChanged(this.FrameRate);

                                           _currentThread = null;
                                       });

                return true;
            }
        }


        public bool InitOgre()
        {
            try
            {
                return _InitOgre();
            }
            catch (Exception)
            {
                return true;
            }
        }

        public Thread InitOgreAsync(ThreadPriority priority, RoutedEventHandler completeHandler)
        {
            if (completeHandler != null)
                Initialised += completeHandler;

            _currentThread = new Thread(() => _InitOgre())
                                 {
                                     Priority = priority
                                 };
            _currentThread.Start();

            return _currentThread;
        }

        public void InitOgreAsync()
        {
            InitOgreAsync(ThreadPriority.Normal, null);
        }

        public event RoutedEventHandler Initialised;
        public event EventHandler PreRender;
        public event EventHandler PostRender;

        protected void RenderFrame()
        {
            if ((_camera != null) && (_viewport == null))
            {
                _viewport = _renTarget.AddViewport(_camera);
                _viewport.BackgroundColour = new ColourValue(0.0f, 0.0f, 0.0f, 0.0f);
            }

            if (PreRender != null)
                PreRender(this, EventArgs.Empty);

            _root.RenderOneFrame();

            if (PostRender != null)
                PostRender(this, EventArgs.Empty);
        }

        protected void DisposeRenderTarget()
        {
            if (_renTarget != null)
            {
                CompositorManager.Singleton.RemoveCompositorChain(_viewport);
                _renTarget.RemoveAllListeners();
                _renTarget.RemoveAllViewports();
                _root.RenderSystem.DestroyRenderTarget(_renTarget.Name);
                _renTarget = null;
                _viewport = null;
            }

            if (_texture != null)
            {
                TextureManager.Singleton.Remove(_texture.Handle);
                _texture.Dispose();
                _texture = null;
            }
        }

        protected void ReInitRenderTarget()
        {
            DetachRenderTarget(true, false);
            DisposeRenderTarget();

            _texture = TextureManager.Singleton.CreateManual(
                "OgreImageSource RenderTarget",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME,
                TextureType.TEX_TYPE_2D,
                (uint)ViewportSize.Width, (uint)ViewportSize.Height,
                0, Mogre.PixelFormat.PF_A8R8G8B8,
                (int)TextureUsage.TU_RENDERTARGET);

            _renTarget = _texture.GetBuffer().GetRenderTarget();

            _reloadRenderTargetTime = 0;
        }

        public Root Root
        {
            get { return _root; }
        }

        public SceneManager SceneManager
        {
            get { return _sceneManager; }
        }

        public Camera Camera
        {
            get { return _camera; }
        }

        protected virtual void AttachRenderTarget(bool attachEvent)
        {
            if (!_imageSourceValid)
            {
                Lock();
                try
                {
                    IntPtr surface;
                    _renTarget.GetCustomAttribute("DDBACKBUFFER", out surface);
                    SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface);

                    _imageSourceValid = true;
                }
                finally
                {
                    Unlock();
                }
            }

            if (attachEvent)
                UpdateEvents(true);
        }

        protected virtual void DetachRenderTarget(bool detatchSurface, bool detatchEvent)
        {
            if (detatchSurface && _imageSourceValid)
            {
                Lock();
                SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
                Unlock();

                _imageSourceValid = false;
            }

            if (detatchEvent)
                UpdateEvents(false);
        }

        protected virtual void UpdateEvents(bool attach)
        {
            _eventAttatched = attach;
            if (attach)
            {
                if (_timer != null)
                    _timer.Tick += _rendering;
                else
                    CompositionTarget.Rendering += _rendering;
            }
            else
            {
                if (_timer != null)
                    _timer.Tick -= _rendering;
                else
                    CompositionTarget.Rendering -= _rendering;
            }
        }

        private void _isFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsFrontBufferAvailable)
                AttachRenderTarget(true); // might not succeed
            else
                // need to keep old surface attached because it's the only way to get the front buffer active event.
                DetachRenderTarget(false, true);
        }

        private void _rendering(object sender, EventArgs e)
        {
            if (_root == null) return;

            if (IsFrontBufferAvailable)
            {
                if (MogreWpf.Interop.D3D9RenderSystem.IsDeviceLost(_renderWindow))
                {
                    _renderWindow.Update(); // try restore
                    _reloadRenderTargetTime = -1;

                    if (MogreWpf.Interop.D3D9RenderSystem.IsDeviceLost(_renderWindow))
                        return;
                }

                long durationTicks = ResizeRenderTargetDelay.TimeSpan.Ticks;

                // if the new next ReInitRenderTarget() interval is up
                if (((_reloadRenderTargetTime < 0) || (durationTicks <= 0))
                    // negative time will be reloaded immediatly
                    ||
                    ((_reloadRenderTargetTime > 0) &&
                     (Environment.TickCount >= (_reloadRenderTargetTime + durationTicks))))
                {
                    ReInitRenderTarget();
                }

                if (!_imageSourceValid)
                    AttachRenderTarget(false);

                Lock();
                RenderFrame();
                AddDirtyRect(new Int32Rect(0, 0, PixelWidth, PixelHeight));
                Unlock();
            }
        }

        private void OnFrameRateChanged(int? newFrameRate)
        {
            bool wasAttached = _eventAttatched;
            UpdateEvents(false);

            if (newFrameRate == null)
            {
                if (_timer != null)
                {
                    _timer.Tick -= _rendering;
                    _timer.Stop();
                    _timer = null;
                }
            }
            else
            {
                if (_timer == null)
                    _timer = new DispatcherTimer();

                _timer.Interval = new TimeSpan(1000 / newFrameRate.Value);
                _timer.Start();
            }

            if (wasAttached)
                UpdateEvents(true);
        }
    }
}