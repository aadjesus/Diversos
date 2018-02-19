using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using Microsoft.Win32;
using System.Windows.Input;
using MVVM;

namespace AssessmentChooser.ViewModel
{
    /// <summary>The ViewModel for the application's MainWindow view</summary>
    public class MainViewModel : CloseableViewModel
    {
        //-------------------------------------------------------------------------------------------------------------------------
        #region Construction

        public MainViewModel()
        {
            base.DisplayName = "Job Chooser";
        }

        #endregion 
        //-------------------------------------------------------------------------------------------------------------------------
        #region Presentation Properties

        #endregion // Commands
        //-------------------------------------------------------------------------------------------------------------------------
        #region Private Methods


        #endregion
        //-------------------------------------------------------------------------------------------------------------------------
    }
}