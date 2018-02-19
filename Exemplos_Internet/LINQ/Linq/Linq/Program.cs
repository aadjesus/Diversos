using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Espresso;

namespace Linq
{
    class Contact
    {
        public string Name;
        public string City;
        public string Phone;
        public int Level;
    }

    class Program
    {
        static void Main(string[] args)
        {

            var contacts = new Contact[] 
            {
                new Contact() {Name = "Matt", City = "Redmond", Phone="555-3691", Level=1},
                new Contact(){Name = "Jomo", City = "Seattle", Phone="555-8822", Level=5},
                new Contact(){Name = "Anders", City = "Seattle", Phone="555-7029", Level=20},
                new Contact(){Name = "Luca", City = "Kirkland", Phone="555-6266", Level=2},
                new Contact(){Name = "Dinesh", City = "Redmond", Phone="555-8000", Level=3}
            };

            Expressor express = new Expressor();
            express.IgnoreCase = true;

            Console.WriteLine("All Contacts");
            ObjectDumper.Write(contacts);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter filter expression for contacts (or 'quit' to end)");
                Console.WriteLine("example: city = 'Redmond'");
                Console.Write(">");
                string filter = Console.ReadLine();

                if (string.Compare(filter, "quit", true) == 0)
                    break;

                if (filter != null && filter.Length > 0)
                {
                    try
                    {
                        var filterFunc = express.MakeFunc<Contact, bool>(filter);

                        Console.WriteLine();
                        Console.WriteLine("Results...");
                        Console.WriteLine();
                        ObjectDumper.Write(contacts.Where(filterFunc));
                    }
                    catch (ParseException p)
                    {
                        Console.WriteLine("Error ({0},{1}): {2}", p.Location.Line, p.Location.Column, p.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}", e.Message);
                    }
                }
            }
        }
    }
}
