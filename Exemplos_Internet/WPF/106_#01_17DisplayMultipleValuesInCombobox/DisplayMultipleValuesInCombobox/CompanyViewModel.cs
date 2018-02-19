using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace DisplayMultipleValuesInCombobox
{
    public class CompanyViewModel : INotifyPropertyChanged
    {
        DataSource _objDataSource = new DataSource();

        #region Private 

        private ObservableCollection<Location> _location = new ObservableCollection<Location>();
        
        #endregion

        #region Methods for combobox

        //Loads the Location Entity Data
        public void LoadData()
        {
            CompanyLocations = new ObservableCollection<Location>(_objDataSource.LocationList());

        }

        public ObservableCollection<Location> CompanyLocations
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                OnPropertyChanged("CompanyLocations");
            }
        }

        #endregion

        #region General Propertychange methods

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        protected void OnPropertyChanged(string PropertyName)
        {
            VerifyPropertyName(PropertyName);

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                throw new Exception(msg);
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
