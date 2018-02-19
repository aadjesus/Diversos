using System;
using System.Workflow.ComponentModel;
using System.Workflow.Runtime;

namespace FirstWFLibrary
{
	#region PromptForUserName

	/// <summary>
	/// Asks the user for their name.
	/// </summary>
	public class PromptForUserName : Activity
	{
		protected override ActivityExecutionStatus Execute( ActivityExecutionContext executionContext )
		{
			Console.Write( "Please enter your name and press Enter: " );
			return ActivityExecutionStatus.Closed;
		}
	}

	#endregion // PromptForUserName

	#region ReadConsoleLine

	/// <summary>
	/// An activity which represents reading a line of text from the console.
	/// </summary>
	public class ReadConsoleLine : Activity
	{
		#region InputText Property

		private string inputText;
		public string InputText
		{
			get { return this.inputText; }
		}

		#endregion // InputText Property

		#region Execute [override]

		protected override ActivityExecutionStatus Execute( ActivityExecutionContext executionContext )
		{
			// Create a WorkflowQueue, which allows this activity to "bookmark" where it should
			// continue executing once the external input arrives (in this case, a string is read
			// from the console).
			WorkflowQueue workflowQueue = this.GetWorkflowQueue( executionContext );

			// Attach a handler which processes the external input.
			workflowQueue.QueueItemAvailable += ProcessQueueItemAvailable;

			// Attach a handler which cleans up after the input has been processed.
			workflowQueue.QueueItemAvailable += CloseActivity;

			// Indicate to the Workflow runtime that this activity is logically still executing, 
			// even though it will not occupy time on a thread until external input arrives.
			return ActivityExecutionStatus.Executing;
		}

		#endregion // Execute [override]

		#region Event Handlers

		void ProcessQueueItemAvailable( object sender, QueueEventArgs e )
		{
			// The external input has arrived, so wake up and process it.
			WorkflowQueue workflowQueue = this.GetWorkflowQueue( sender as ActivityExecutionContext );
			if( workflowQueue.Count > 0 )
				this.inputText = workflowQueue.Dequeue() as string;
		}

		void CloseActivity( object sender, QueueEventArgs e )
		{
			// The external input has arrived and been processed, so throw away the WorkflowQueue
			// that we used, and tell the Workflow runtime that the activity is finished.
			ActivityExecutionContext executionContext = sender as ActivityExecutionContext;
			WorkflowQueuingService queuingService = executionContext.GetService<WorkflowQueuingService>();
			queuingService.DeleteWorkflowQueue( this.Name );
			executionContext.CloseActivity();
		}

		#endregion // Event Handlers

		#region Private Helpers

		// Helper method which returns a WorkflowQueue.
		WorkflowQueue GetWorkflowQueue( ActivityExecutionContext executionContext )
		{
			WorkflowQueue queue;
			WorkflowQueuingService queuingService = executionContext.GetService<WorkflowQueuingService>();			
			if( queuingService.Exists( this.Name ) )
				queue = queuingService.GetWorkflowQueue( this.Name );
			else
				queue = queuingService.CreateWorkflowQueue( this.Name, true );

			return queue;
		}

		#endregion // Private Helpers
	}

	#endregion // ReadConsoleLine

	#region GreetUser

	/// <summary>
	/// Prints a greeting to the user.
	/// </summary>
	public class GreetUser : Activity
	{
		public static readonly DependencyProperty UserNameProperty;

		static GreetUser()
		{
			UserNameProperty = DependencyProperty.Register( 
				"UserName", 
				typeof( string ), 
				typeof( GreetUser ) );
		}

		// UserName is a dependency property so that it can be bound to the
		// InputText property of the ReadConsoleLine activity.
		public string UserName
		{
			get { return (string)GetValue( UserNameProperty ); }
			set { SetValue( UserNameProperty, value ); }
		}

		protected override ActivityExecutionStatus Execute( ActivityExecutionContext executionContext )
		{
			string greeting = String.Format( "Hello, {0}!", this.UserName );
			Console.WriteLine( greeting );

			return ActivityExecutionStatus.Closed;
		}
	}

	#endregion // GreetUser
}