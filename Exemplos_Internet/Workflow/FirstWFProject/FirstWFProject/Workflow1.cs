using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Collections.Generic;


namespace FirstWFProject
{
	public sealed partial class Workflow1: SequentialWorkflowActivity
	{
        public Dictionary<int, string> cities =
                new Dictionary<int, string>();
        
        public string FromCity;
        public string ToCity;
        
        public Workflow1()
		{
			InitializeComponent();

            // Add cities in cities collection 
            cities.Add(1, "medina");
            cities.Add(2, "london");
            cities.Add(3, "houston");
            cities.Add(4, "karachi");
            cities.Add(5, "tokyo");
            cities.Add(6, "singapore");
            cities.Add(7, "istanbul");
            cities.Add(8, "seattle");
		}

        private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Enter Passenger Name : ");
            string passengerName = Console.ReadLine();

            Console.WriteLine("Enter Departure City :");
            this.FromCity = Console.ReadLine();
            Console.WriteLine("Enter Destination City :");
            this.ToCity = Console.ReadLine();

        }

        private void codeActivity2_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Flight Booking Confirmed!");
            Console.ReadKey();
        }

        private void codeActivity3_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Sorry, No Such Flight Exists!");
            Console.ReadKey();
        }

	}

}
