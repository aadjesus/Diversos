﻿using System;
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
using System.Windows.Interop;

namespace ScrollControl
{

    /// <summary>
    /// Window1 simply holds a single instance of 
    /// <see cref="SortingControl">SortingControl</see>
    /// </summary>
    public partial class Window1 : Window
    {
        #region Ctor
        public Window1()
        {
            InitializeComponent();
        }
        #endregion
    }
}
