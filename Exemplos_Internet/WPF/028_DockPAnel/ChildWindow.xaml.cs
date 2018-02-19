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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlyingWindows
{
    /// <summary>
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : UserControl
    {
        public event ModeChangedEventHandler ModeChanged;
        private WindowsContainer.WindowLayoutMode _Mode = WindowsContainer.WindowLayoutMode.Tiled;
        private Rect _StandardRect;

        public Rect StandardRect
        {
            get { return _StandardRect; }
            set { _StandardRect = value; }
        }

        public WindowsContainer.WindowLayoutMode Mode
        {
            get { return _Mode; }
            set 
            {
                _Mode = value;

                SetMode(value);
            }
        }

        public void SetVerticalTitle()
        {
            SetVerticalTitle(this.Width <= this.Title.Height + 15);
        }

        public void SetVerticalTitle(bool on)
        {
            if (on)
            {
                Transform tr = new RotateTransform(270);
                AllContents.LayoutTransform = tr;
            }
            else
            {
                AllContents.LayoutTransform = null;
            }
        }

        /// <summary>
        /// Change any visual aspect depending on the mode.
        /// </summary>
        /// <param name="mode"></param>
        protected virtual void SetMode(WindowsContainer.WindowLayoutMode mode)
        {
            switch (mode)
            {
                case WindowsContainer.WindowLayoutMode.Tiled:
                    //SetVerticalTitle(true);
                    break;
                case WindowsContainer.WindowLayoutMode.Detailed:
                    //SetVerticalTitle(false);
                    break;
            }
        }

        public ChildWindow()
        {
            InitializeComponent();

            Title.ModeChanged += new ModeChangedEventHandler(Title_ModeChanged);
        }

        void Title_ModeChanged(object sender, ModeChangedEventArgs args)
        {
            this.Mode = args.LayoutMode;

            if (ModeChanged != null)
            {
                ModeChanged(this, args);
            }
        }
    }

    public delegate void ModeChangedEventHandler(object sender, ModeChangedEventArgs args);

    public class ModeChangedEventArgs:EventArgs
    {
        private WindowsContainer.WindowLayoutMode _LayoutMode;

        public WindowsContainer.WindowLayoutMode LayoutMode
        {
            get { return _LayoutMode; }
            private set { _LayoutMode = value; }
        } 
        public ModeChangedEventArgs(WindowsContainer.WindowLayoutMode mode)
            : base()
        {
            LayoutMode = mode;
        }
    }
}
