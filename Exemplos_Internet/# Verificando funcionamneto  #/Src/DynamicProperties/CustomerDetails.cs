using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicProperties
{
  /// <summary>
  /// Customer properties.
  /// </summary>
  public class CustomerDetails : ComplexProperty<object>
  {
    // constructors...
    public CustomerDetails()
    {
      AddProperty("FirstName", new SimpleProperty<string>(String.Empty));
      AddProperty("LastName", new SimpleProperty<string>(String.Empty));
    }

    // public properties...
    public string FirstName
    {
      get { return GetValue<string>("FirstName"); }
      set { SetValue<string>("FirstName", value); }
    }
    public string LastName
    {
      get { return GetValue<string>("LastName"); }
      set { SetValue<string>("LastName", value); }
    }
  }
}