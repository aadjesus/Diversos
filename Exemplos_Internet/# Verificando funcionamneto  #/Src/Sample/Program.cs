using System;
using System.Collections.Generic;
using System.Text;

using DynamicProperties;

namespace Sample
{
  class Program
  {
    static void Main(string[] args)
    {
      // Simple properties sample...

      // String property:
      DynamicProperty<string> stringProperty = new SimpleProperty<string>("StringValue");
      stringProperty.Value = "Hello";
      Console.WriteLine("String property value: {0}", stringProperty.Value);

      // Boolean property:
      DynamicProperty<bool> boolProperty = new SimpleProperty<bool>(false);
      boolProperty.Value = true;
      Console.WriteLine("Boolean property value: {0}", boolProperty.Value);

      // Integer property:
      DynamicProperty<int> intProperty = new SimpleProperty<int>(20);
      intProperty.Value = 100;
      Console.WriteLine("Integer property value: {0}", intProperty.Value);


      // Complex properties sample...

      // Create complex dynamic property and add child properties:
      DynamicProperty<object> complexProperty = new ComplexProperty<object>();
      complexProperty.AddProperty("StringProperty", stringProperty);
      complexProperty.AddProperty("BooleanProperty", boolProperty);
      complexProperty.AddProperty("IntegerProperty", intProperty);

      // We can get or set value of child properties now:
      complexProperty.SetValue<string>("StringProperty", "Child String Value");
      Console.WriteLine("Complex property child string: {0}", complexProperty.GetValue<string>("StringProperty"));

      complexProperty.SetValue<bool>("BooleanProperty", false);
      Console.WriteLine("Complex property child booean: {0}", complexProperty.GetValue<bool>("BooleanProperty"));

      complexProperty.SetValue<int>("IntegerProperty", 200);
      Console.WriteLine("Complex property child integer: {0}", complexProperty.GetValue<int>("IntegerProperty"));


      // Customer details sample...

      CustomerDetails customer = new CustomerDetails();
      customer.FirstName = "Jack";
      customer.LastName = "Smith";

      string firstName = customer.GetValue<string>("FirstName");
      string lastName = customer.GetValue<string>("LastName");

      string fullName = String.Format("Customer full name: {0} {1}", firstName, lastName);

      Console.WriteLine(fullName);

      customer.AddProperty("IsAuthenticated", new SimpleProperty<bool>(false));

      Console.WriteLine("Customer authenticated: {0}", customer.GetValue<bool>("IsAuthenticated"));

      customer.SetValue("IsAuthenticated", true);
      Console.WriteLine("Customer authenticated: {0}", customer.GetValue<bool>("IsAuthenticated"));

      customer.RemoveProperty("IsAuthenticated");

      Console.WriteLine("Customer has IsAuthenticated property: {0}", customer.HasProperty("IsAuthenticated"));

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
  }
}