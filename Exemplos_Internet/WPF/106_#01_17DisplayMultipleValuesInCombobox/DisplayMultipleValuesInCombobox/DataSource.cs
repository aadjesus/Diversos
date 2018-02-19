using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DisplayMultipleValuesInCombobox
{
   public class DataSource
    {
       ObservableCollection<Location> _location = new ObservableCollection<Location>();


       public ObservableCollection<Location> LocationList()
       {
           _location.Clear();
           LocationData();
           return _location;
       }

       #region Data Source

       private void LocationData()
       {
           _location.Add(new Location { LocationId = 1,CompanyName = "Microsoft", Country = "India", State = "Karnataka", City = "Bangalore" });
           _location.Add(new Location { LocationId = 2, CompanyName = "Oracle", Country = "USA", State = "North Carolina", City = "Midland" });
           _location.Add(new Location { LocationId = 3, CompanyName = "Sun Micro Systems", Country = "India", State = "Maharashtra", City = "Bombay" });
           _location.Add(new Location { LocationId = 4, CompanyName = "Robert Bosch", Country = "USA", State = "Tennessee", City = "Grand Prairie" });
           _location.Add(new Location { LocationId = 5, CompanyName = "IBM", Country = "India", State = "West Bengal", City = "Calcutta" });
           _location.Add(new Location { LocationId = 6, CompanyName = "Google", Country = "USA", State = "Ohio", City = "El Paso" });
       }

       #endregion
    }

   
}
