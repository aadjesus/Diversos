using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DayNightMode
{
    public class Window1ViewModel : ViewModelBase
    {
        public ICommand DayModeButtonCommand { get; set; }

        public Window1ViewModel()
        {
            // initialize command
            DayModeButtonCommand = new DayModeButtonCommand(this);
        }

        public string DayModeButtonText
        {
            get { return (string)GetValue(DayModeButtonTextProperty); }
            set 
            { 
                SetValue(DayModeButtonTextProperty, value);
                OnPropertyChanged("DayModeButtonText");
            }
        }

        public static readonly DependencyProperty DayModeButtonTextProperty =
            DependencyProperty.Register("DayModeButtonText", typeof(string), typeof(Window1ViewModel), new UIPropertyMetadata(""));

        public void DayModeButtonClicked()
        {
            // Set day mode to the opposite of what it was.
            IocContainer.Resolve<AppInfo>().DayMode = !IocContainer.Resolve<AppInfo>().DayMode;

            // change text of button
            if (DayMode)
            {
                DayModeButtonText = "Press for Night Mode";
            }
            else
            {
                DayModeButtonText = "Press for Day Mode";
            }
        }

        
    }
}
