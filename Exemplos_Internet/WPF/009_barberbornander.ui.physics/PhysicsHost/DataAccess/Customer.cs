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
    public partial class Customer : IDataErrorInfo
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

                    case "ContactName":
                        if (string.IsNullOrEmpty(this.ContactName))
                        {
                            result = "ContactName cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.ContactName) &&
                            this.ContactName.Length >= 15)
                        {
                            result = "ContactName should be <= 15 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "ContactTitle":
                        if (string.IsNullOrEmpty(this.ContactTitle))
                        {
                            result = "ContactTitle cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.ContactTitle) &&
                            this.ContactTitle.Length >= 15)
                        {
                            result = "ContactTitle should be <= 15 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "Address":
                        if (string.IsNullOrEmpty(this.Address))
                        {
                            result = "Address cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.Address) &&
                            this.Address.Length >= 30)
                        {
                            result = "Address should be <= 30 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "City":
                        if (string.IsNullOrEmpty(this.City))
                        {
                            result = "City cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.City) &&
                            this.City.Length >= 10)
                        {
                            result = "City should be <= 10 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "Region":
                        if (string.IsNullOrEmpty(this.Region))
                        {
                            result = "Region cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.Region) && 
                            this.Region.Length >= 15)
                        {
                            result = "Region should be <= 15 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "PostalCode":
                        if (string.IsNullOrEmpty(this.PostalCode))
                        {
                            result = "PostalCode cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.PostalCode) &&
                            this.PostalCode.Length > 10)
                        {
                            result = "PostalCode should be <= 10 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "Country":
                        if (string.IsNullOrEmpty(this.Country))
                        {
                            result = "Country cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.Country) &&
                            this.Country.Length > 10)
                        {
                            result = "Country should be <= 10 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "Phone":
                        if (string.IsNullOrEmpty(this.Phone))
                        {
                            result = "Phone cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.Phone) &&
                            this.Phone.Length > 15)
                        {
                            result = "Phone should be <= 15 chars";
                            combinedError.Append(result + "\r\n");
                        }
                        break;

                    case "Fax":
                        if (string.IsNullOrEmpty(this.Fax))
                        {
                            result = "Fax cant be empty";
                            combinedError.Append(result + "\r\n");
                        }
                        if (!string.IsNullOrEmpty(this.Fax) &&
                            this.Fax.Length > 15)
                        {
                            result = "Fax should be <= 15 chars";
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
