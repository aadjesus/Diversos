using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCFServiceLibrary2;
using MaintainingSessionState;
using System.Configuration;
//using System.;

namespace ManualHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            // ServiceType/Uri(Base Address)
            // Below you can find multiple Endpoints defined for this host
            // Similarly 'n' hosts can be defined and 'm' endpoints can be assigned to it...
            // In that case m*n endpoints will be created...
            // In the same case at the maximum of m*n Uri's will be created...
            // It is possible to have same Uri to service 2 different endpoints, it will
            // be true if same service exposes 2 different contracts, here the 2 different endpoints point to the 
            // same service but exposed through different service contracts...
            ServiceHost host = new ServiceHost(typeof(MathService), new Uri(ConfigurationManager.AppSettings["baseAddress"]));

            // Implemented Contract/Binding Type/Relative URI...
            // Now Multiple Endpoints of the above defined host are defined here...
            // This signifies that now the client can query to the host through 
            // 2 different Uris...
            BasicHttpBinding httpBinding = new BasicHttpBinding();
            host.AddServiceEndpoint(typeof(WCFServiceLibrary2.IMath), httpBinding, "MathService");
            host.AddServiceEndpoint(typeof(WCFServiceLibrary2.IEnglish), new BasicHttpBinding(), "EnglishService");

            // Open the Host to listen for clients...
            host.Open();
            Console.WriteLine("MathService is listening on the following...");

            // Print the Address & Binding Information
            foreach (ServiceEndpoint e in host.Description.Endpoints)
            {
                Console.WriteLine("{0} ({1})", e.Address.ToString(), e.Binding.Name);
            }
            Console.WriteLine("\nPress [Enter] to terminate.");
            Console.ReadLine();
        }
    }
}
