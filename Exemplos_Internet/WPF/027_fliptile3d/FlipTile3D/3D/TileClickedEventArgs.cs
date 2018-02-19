using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FlipTile3D
{
    public delegate void TileClickedEventHandler(Object sender, TileClickedEventArgs args);


    public class TileClickedEventArgs : RoutedEventArgs
    {
        #region Data
        public String Url { get; private set; }
        #endregion

        #region Ctor
        public TileClickedEventArgs(String url, RoutedEvent routedEvent) 
            : base(routedEvent)
        {
            this.Url = url;
        }
        #endregion
    }
}
