using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace PhysicsHost.DataAccess
{
    /// <summary>
    /// This class is another part (partial) to the 
    /// LINQ to SQL generated class within the 
    /// Northwind.designer.cs file. This class
    /// provides the validation that is used
    /// by the WPF databindings
    /// </summary>
    public partial class Order : IDataErrorInfo
    {
        #region Data
        private StringBuilder combinedError = new StringBuilder(500);
        #endregion

        #region IDataErrorInfo Members

        /// <summary>
        /// Return the full list of validation 
        /// errors for this object
        /// </summary>
        public string Error
        {
            get
            {
                return combinedError.ToString();
            }
        }

        /// <summary>
        /// Validates a particular column, and returns a 
        /// string representing the current error
        /// </summary>
        /// <param name="columnName">The property name to validate</param>
        /// <returns>A string representing the current error</returns>
        public string this[string columnName]
        {
            get
            {
                string result = null;
                combinedError = new StringBuilder(500);

                //basically we need a case for each property you wish to validate
                switch (columnName)
                {

                    case "ShipName":
                        if (string.IsNullOrEmpty(this.ShipName))
                        {
                            result = "ShipName cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.ShipName) &&
                            this.ShipName.Length >= 15)
                        {
                            result = "ShipName should be <= 15 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "ShipAddress":
                        if (string.IsNullOrEmpty(this.ShipAddress))
                        {
                            result = "ShipAddress cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.ShipAddress) &&
                            this.ShipAddress.Length >= 30)
                        {
                            result = "ShipAddress should be <= 30 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "ShipCity":
                        if (string.IsNullOrEmpty(this.ShipCity))
                        {
                            result = "ShipCity cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.ShipCity) &&
                            this.ShipCity.Length >= 10)
                        {
                            result = "ShipCity should be <= 10 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "ShipRegion":
                        if (string.IsNullOrEmpty(this.ShipRegion))
                        {
                            result = "ShipRegion cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.ShipRegion) &&
                            this.ShipRegion.Length >= 15)
                        {
                            result = "ShipRegion should be <= 15 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "ShipPostalCode":
                        if (string.IsNullOrEmpty(this.ShipPostalCode))
                        {
                            result = "ShipPostalCode cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.ShipPostalCode) &&
                            this.ShipPostalCode.Length > 10)
                        {
                            result = "ShipPostalCode should be <= 10 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "ShipCountry":
                        if (string.IsNullOrEmpty(this.ShipCountry))
                        {
                            result = "ShipCountry cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.ShipCountry) &&
                            this.ShipCountry.Length > 10)
                        {
                            result = "ShipCountry should be <= 10 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;
                }
                return result;
            }
        }
        #endregion
    }
}
