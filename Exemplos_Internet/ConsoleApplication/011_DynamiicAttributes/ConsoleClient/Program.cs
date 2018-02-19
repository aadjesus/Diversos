using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicAttributes;
using System.ComponentModel;

namespace ConsoleClient
{
    public class Program
    {

        private static void DisplayRoles(Person person)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Displaying roles required to access Person with ID: {0}", person.ID.ToString());
            Console.ResetColor();

            SecuredAttribute securedAttrib = TypeDescriptor.GetAttributes(person).Cast<Attribute>().SingleOrDefault(a => a.GetType().Name == typeof(SecuredAttribute).Name) as SecuredAttribute;

            if (securedAttrib != null)
            {
                string[] roles = securedAttrib.Roles;
                if (roles.Length > 0)
                {
                    Console.WriteLine("Instance Level Roles");
                    Console.WriteLine("====================");
                    string strRoles = roles.Aggregate((r1, r2) => r1 + "," + r2);
                    Console.WriteLine("Roles: {0}", roles);
                }
            }
            person.GetType().GetProperties().ToList().ForEach
                (propInfo =>
                {
                    PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(person).Cast<PropertyDescriptor>().SingleOrDefault(p => propInfo.Name == p.Name);

                    //if there is a property descriptor defined for the current property then proceed to read roles defined for this property
                    if (propDescriptor != null)
                    {
                        SecuredAttribute attrib = propDescriptor.Attributes.Cast<Attribute>().SingleOrDefault
                                            (p => p.GetType().Name == typeof(SecuredAttribute).Name) as SecuredAttribute;

                        //ensure that the property has been Secured.
                        if (attrib != null)
                        {
                            string[] roles = attrib.Roles;
                            if (roles.Length > 0)
                            {
                                Console.WriteLine(Environment.NewLine + "Property Name:" + propInfo.Name);
                                Console.WriteLine("==============");
                                string strRoles = roles.Aggregate((r1, r2) => r1 + "," + r2);
                                Console.WriteLine("Roles: {0}", strRoles);
                            }
                        }
                    }
                }
                 );

        }
        
        static void Main(string[] args)
        {
            Person person1 = new Person { ID = 1, Age = 23, Name = "Jack Smith" };
            TypeDescriptor.AddProvider(new CustomTypeDescriptionProvider<Person>(TypeDescriptor.GetProvider(typeof(Person))), person1);

            Person person2 = new Person { ID = 2, Age = 24, Name = "Jane Smith" };
            TypeDescriptor.AddProvider(new CustomTypeDescriptionProvider<Person>(TypeDescriptor.GetProvider(typeof(Person))), person2);

            DisplayRoles(person1);
            DisplayRoles(person2);
        }
    }

}
