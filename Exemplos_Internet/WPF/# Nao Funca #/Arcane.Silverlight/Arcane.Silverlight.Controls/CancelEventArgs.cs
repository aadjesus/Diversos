using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ServiceModel;

namespace Arcane.Silverlight.Controls
{
    public class CancelEventArgs : EventArgs
    {
        private bool cancel;

        public CancelEventArgs()
            : this(false)
        {
        }

        public CancelEventArgs(bool cancel)
        {
            this.cancel = cancel;
        }

        public bool Cancel
        {
            get
            {
                return this.cancel;
            }
            set
            {
                this.cancel = value;
            }
        }
    }




}
