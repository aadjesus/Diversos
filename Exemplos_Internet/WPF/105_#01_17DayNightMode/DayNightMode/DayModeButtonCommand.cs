using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DayNightMode
{
    public class DayModeButtonCommand : ICommand
    {
        private Window1ViewModel _viewModel;

        public DayModeButtonCommand(Window1ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            // no reason to disable button.  so this should always return true.
            return true;
        }

        // not used, but is part of the ICommand interface
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _viewModel.DayModeButtonClicked();
        }
    }
}
