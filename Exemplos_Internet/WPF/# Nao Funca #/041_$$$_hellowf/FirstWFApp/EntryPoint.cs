using System;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.Runtime;
using System.Xml;

namespace FirstWFApp
{
	class EntryPoint
	{
		public static void Main()
		{
			// Create an instance of WorkflowRuntime, which will execute and coordinate 
			// all of our workflow activities.
			using( WorkflowRuntime workflowRuntime = new WorkflowRuntime() )
			{
				// Tell the Workflow runtime where to find our custom activity types.
				TypeProvider typeProvider = new TypeProvider( workflowRuntime );
				typeProvider.AddAssemblyReference( "FirstWFLibrary.dll" );
				workflowRuntime.AddService( typeProvider );

				// Activate the Workflow runtime.
				workflowRuntime.StartRuntime();

				// Load the XAML file which contains the declaration of our simple workflow
				// and create an instance of it.  Once it is loaded, the workflow is started
				// so that the activities in it will execute.
				WorkflowInstance workflowInstance;
				using( XmlTextReader xmlReader = new XmlTextReader( @"..\..\HelloUserWorkflow.xaml" ) )
				{
					workflowInstance = workflowRuntime.CreateWorkflow( xmlReader );
					workflowInstance.Start();
				}				

				// The ReadConsoleLine activity uses a "bookmark" to indicate that it must
				// wait for external input before it can complete.  In this case, the 
				// external input is the user typing in his/her name into the console window.
				//
				// Note: If this was a more sophisticated app, we could "passivate" the workflow
				// here (i.e. save it to a database and remove it from memory).  Once the external
				// input arrives (let's say a file is created in a network folder), the passivated 
				// workflow could then be "resumed" and continue on with its processing.
				// The duration between passivation and resumption could be hours, days, weeks, etc.
				// The workflow could even be resumed on a different computer from the one on which 
				// it was passivated.
				string userName = Console.ReadLine();
				workflowInstance.EnqueueItem( "getUserName", userName, null, null );
				
				// Pause here so that the workflow can display the greeting.
				Console.ReadLine();
								
				// Tear down all of the Workflow services and runtime.
				// (This is probably redundant since the 'runtime' object is in
				// a using block).
				workflowRuntime.StopRuntime();
			}
		}
	}
}