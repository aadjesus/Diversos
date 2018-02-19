using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Collections;

using System.ComponentModel;

using WPFLocalize.Cultures;

namespace WPFLocalize
{
    public partial class UserControl2 : INotifyPropertyChanged
    {
		public UserControl2()
        {
#if DEBUG
            //only perform the following fix if we are in the designer
            // - turns out that while the default ctor is not run when editing the usercontrol,
            //   it is run when the usercontrol has been added to a window/page
            // NB. The Visual Studio designer might return null for Application.Current, in which case this solution would not work
            //     http://msdn.microsoft.com/en-us/library/bb546934.aspx
            if (DesignerProperties.GetIsInDesignMode(this) && Application.Current != null)
            {
                Uri resourceLocater = new System.Uri("/WPFLocalize;component/ResourceDictionary1.xaml", UriKind.Relative);
                ResourceDictionary dictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                //add the resourcedictionary that contains our Resources ODP to App.Current (which is actually the Designer / Blend)
                if (!Application.Current.Resources.MergedDictionaries.Contains(dictionary))
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
            }
#endif
            this.InitializeComponent();
		}

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // centralise the notification that a property has changed.
        protected virtual void OnPropertyChanged(string strPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
        }

        #endregion
	}
}