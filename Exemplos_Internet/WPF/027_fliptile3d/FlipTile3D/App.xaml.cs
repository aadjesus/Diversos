using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace FlipTile3D
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Data
        private Boolean areDisciplesOpen = false;
        #endregion

        #region Public Properties
        public Boolean AreDisciplesOpen
        {
            get { return this.areDisciplesOpen;}
            set { this.areDisciplesOpen = value; }
        }
        #endregion
    }
}
