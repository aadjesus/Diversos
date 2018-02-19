using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisplayMultipleValuesInCombobox
{
    public class Location
    {
        public int _locationid = 0;
        public string _companyname = string.Empty;
        public string _country = string.Empty;
        public string _state = string.Empty;
        public string _city = string.Empty;
       

        public int LocationId
        {
            get
            {
                return _locationid;
            }
            set
            {
                _locationid = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return _companyname;
            }
            set
            {
                _companyname = value;
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }

        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }
    }
}
