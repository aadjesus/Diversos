using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using Autofac;

namespace DayNightMode
{
    public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            // without this condition the designer has trouble loading the window.  Not necessary for run time,
            // but is nice so that you can see your changes in the designer.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                IocContainer.Resolve<AppInfo>().DayNightModeChanged += new Action<bool>(DayNightMode_Changed);
            }
        }

        // this handles the action so that we can update the DayMode property in this class. 
        // the DayMode property in this class is the one that the windows are bound to.
        private void DayNightMode_Changed(bool result)
        {
            DayMode = result;
        }

        public bool DayMode
        {
            get { return (bool)GetValue(DayModeProperty); }
            set 
            { 
                SetValue(DayModeProperty, value);
                OnPropertyChanged("DayMode");
            }
        }

        // this is to make use of the UpdateSourceTrigger attribute in the xaml code.
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public static readonly DependencyProperty DayModeProperty =
            DependencyProperty.Register("DayMode", typeof(bool), typeof(ViewModelBase), new UIPropertyMetadata(false));


        public event PropertyChangedEventHandler PropertyChanged;

    }
}
