using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace FlipTile3D
{
    /// <summary>
    /// Source code origin Kevin Moore, Kevin's WPF Bag-o-Tricks
    /// http://j832.com/bagotricks/
    /// </summary>
    public class CompositionTargetRenderingListener : DispatcherObject, IDisposable
    {
        #region Data
        private bool isListening;
        private bool disposed;
        #endregion

        #region Ctor
        public CompositionTargetRenderingListener() { }
        #endregion

        #region Public Methods
        public void StartListening()
        {
            requireAccessAndNotDisposed();

            if (!isListening)
            {
                isListening = true;
                CompositionTarget.Rendering += compositionTarget_Rendering;
            }
        }

        public void StopListening()
        {
            requireAccessAndNotDisposed();

            if (isListening)
            {
                isListening = false;
                CompositionTarget.Rendering -= compositionTarget_Rendering;
            }
        }

        public void WireParentLoadedUnloaded(FrameworkElement parent)
        {
            requireAccessAndNotDisposed();
            Util.RequireNotNull(parent, "parent");

            parent.Loaded += delegate(object sender, RoutedEventArgs e)
            {
                this.StartListening();
            };

            parent.Unloaded += delegate(object sender, RoutedEventArgs e)
            {
                this.StopListening();
            };
        }

        public bool IsDisposed
        {
            get
            {
                VerifyAccess();
                return disposed;
            }
        }

        public event EventHandler Rendering;

        protected virtual void OnRendering(EventArgs args)
        {
            requireAccessAndNotDisposed();

            EventHandler handler = Rendering;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void Dispose()
        {
            requireAccessAndNotDisposed();
            StopListening();

            Delegate[] invocationlist = Rendering.GetInvocationList();
            foreach (Delegate d in invocationlist)
            {
                Rendering -= (EventHandler)d;
            }

            disposed = true;
        }
        #endregion

        #region Private Methods

        [DebuggerStepThrough]
        private void requireAccessAndNotDisposed()
        {
            VerifyAccess();
            if (disposed)
            {
                throw new ObjectDisposedException(string.Empty);
            }
        }

        private void compositionTarget_Rendering(object sender, EventArgs e)
        {
            OnRendering(e);
        }


        #endregion
    }
}
