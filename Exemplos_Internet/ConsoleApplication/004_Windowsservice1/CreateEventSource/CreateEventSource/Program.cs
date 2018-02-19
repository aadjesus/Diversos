using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateEventSource
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string eventSource = "File Monitor Service";
                System.Diagnostics.EventLog.CreateEventSource(eventSource, "Application");
                Console.WriteLine("Complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during creation of the event log source.");
                Console.WriteLine("Did you run this program as Administrator?\n");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
