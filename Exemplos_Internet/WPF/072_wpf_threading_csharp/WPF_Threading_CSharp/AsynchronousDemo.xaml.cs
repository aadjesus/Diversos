using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel;

namespace WPF_Threading_CSharp
{
	public partial class AsynchronousDemo : Window
	{
		#region Private Matters

		private System.ComponentModel.BackgroundWorker backgroundWorker = new System.ComponentModel.BackgroundWorker();

		#endregion

		#region Constructor

		/// <summary>
		/// Create new instance of AsynchronousDemo and do some setup
		/// </summary>
		public AsynchronousDemo()
		{
			InitializeComponent();

			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);

			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.WorkerReportsProgress = true;

			NameScope.SetNameScope(this, new NameScope());
			lastStackPanel.RegisterName("wpfProgressBar", wpfProgressBar);
		}

		#endregion

		#region Synchronous Using Delegate Demo

		delegate string SomeLongRunningMethodHandler(int rowsToIterate); 

		/// <summary>
		/// Handles click event for synchronousStart button. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SynchronousStart_Click(object sender, System.Windows.RoutedEventArgs e) 
		{ 
			this.synchronousCount.Text = ""; 
    
			SomeLongRunningMethodHandler synchronousFunctionHandler = default(SomeLongRunningMethodHandler); 
    
			synchronousFunctionHandler = new SomeLongRunningMethodHandler(this.SomeLongRunningSynchronousMethod); 
    
			string returnValue = synchronousFunctionHandler.Invoke(1000000000); 
    
			this.synchronousCount.Text = "Processing completed. " + returnValue + " rows processed."; 
		}

		/// <summary>
		/// Used to simulate a long running function such as database call 
		/// or the iteration of many rows. 
		/// </summary>
		/// <param name="rowsToIterate"></param>
		/// <returns></returns>
		private string SomeLongRunningSynchronousMethod(int rowsToIterate)
		{
			double cnt = 0;

			for (long i = 0; i <= rowsToIterate; i++)
			{
				cnt = cnt + 1;
			}

			return cnt.ToString();
		} 

		#endregion

		#region Asynchronous Demo

		delegate string AsyncMethodHandler(int rowsToIterate);
		delegate void UpdateUIHandler(string rowsupdated);

		/// <summary> 
		/// Handles click event for asynchronousStart button. 
		/// </summary> 
		/// <param name="sender"></param> 
		/// <param name="e"></param> 
		/// <remarks></remarks> 
		private void AsynchronousStart_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.asynchronousCount.Text = "";
			this.visualIndicator.Text = "Processing, Please Wait....";
			this.visualIndicator.Visibility = Visibility.Visible;
			AsyncMethodHandler caller = default(AsyncMethodHandler);

			caller = new AsyncMethodHandler(this.SomeLongRunningAsynchronousMethod);

			// open new thread with callback method 
			caller.BeginInvoke(1000000000, CallbackMethod, null);
		}

		/// <summary> 
		/// Called when BeginInvoke is finished running. 
		/// </summary> 
		/// <param name="ar"></param> 
		/// <remarks></remarks> 
		protected void CallbackMethod(IAsyncResult ar)
		{
			try
			{
				// Retrieve the delegate. 
				AsyncResult result = (AsyncResult)ar;
				AsyncMethodHandler caller = (AsyncMethodHandler)result.AsyncDelegate;

				// Because this method is running from secondary thread it 
				// can never access ui objects because they are created 
				// on the primary thread. Uncomment the next line and 
				// run this demo to see for yourself. 
				// This.asynchronousCount.Text = caller.EndInvoke(ar) 

				// Call EndInvoke to retrieve the results. 
				string returnValue = caller.EndInvoke(ar);

				// Still on secondary thread, must update ui on primary thread 
				UpdateUI(returnValue);
			}
			catch (Exception ex)
			{
				string exMessage = null;
				exMessage = "Error: " + ex.Message;
				UpdateUI(exMessage);
			}
		}

		/// <summary> 
		/// Setup delegate to update ui with results 
		/// </summary> 
		/// <param name="rowsUpdated"></param> 
		/// <remarks></remarks> 
		public void UpdateUI(string rowsUpdated)
		{
			// Get back to primary thread to update ui 
			UpdateUIHandler uiHandler = new UpdateUIHandler(UpdateUIIndicators);
			string results = rowsUpdated;
			
			// Run new thread off Dispatched (primary thread) 
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
		}

		/// <summary> 
		/// Update UI from Dispatcher Thread 
		/// </summary> 
		/// <param name="rowsupdated"></param> 
		/// <remarks></remarks> 
		public void UpdateUIIndicators(string rowsupdated)
		{
			// update user interface controls from primary UI thread 
			this.visualIndicator.Text = "Processing Completed.";

			this.asynchronousCount.Text = rowsupdated + " rows processed.";
		}

		/// <summary> 
		/// Used to simulate a long running function such as database call 
		/// or the iteration of many rows. 
		/// </summary> 
		/// <param name="rowsToIterate"></param> 
		/// <returns>Number of iterations processed as string</returns> 
		/// <remarks></remarks> 
		private string SomeLongRunningAsynchronousMethod(int rowsToIterate)
		{
			double cnt = 0;

			for (long i = 0; i <= rowsToIterate; i++)
			{
				cnt = cnt + 1;
			}

			return cnt.ToString();
		} 

		#endregion

		#region Asynchronous Event-Based Demo

		/// <summary> 
		/// Handles click event for wpfAsynchronousStart button. 
		/// </summary> 
		/// <param name="sender"></param> 
		/// <param name="e"></param> 
		/// <remarks></remarks> 
		private void WPFAsynchronousStart_Click(object sender, System.Windows.RoutedEventArgs e) 
		{ 
			this.wpfCount.Text = ""; 
			this.wpfAsynchronousStart.IsEnabled = false; 
			this.wpfAsynchronousCancel.IsEnabled = true; 
		    
			// Calls DoWork on secondary thread 
			this.backgroundWorker.RunWorkerAsync(); 
		    
			// RunWorkerAsync returns immediately, start progress bar 
			wpfProgressBarAndText.Visibility = Visibility.Visible; 
		} 

		/// <summary> 
		/// Runs on secondary thread. 
		/// </summary> 
		/// <param name="sender"></param> 
		/// <param name="e"></param> 
		/// <remarks></remarks> 
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e) 
		{
			// call long running process and get result 
			e.Result = this.SomeLongRunningMethodWPF();
		    
			// Cancel if cancel button was clicked. 
			if (this.backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
				return;
			} 
		} 

		/// <summary> 
		/// Method is called everytime backgroundWorker.ReportProgress is called which triggers ProgressChanged event. 
		/// </summary> 
		/// <param name="sender"></param> 
		/// <param name="e"></param> 
		/// <remarks></remarks> 
		private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) 
		{ 
			// Update UI with % completed. 
			this.wpfCount.Text = e.ProgressPercentage.ToString() + "% processed."; 
		} 

		/// <summary> 
		/// Called when DoWork has completed. 
		/// </summary> 
		/// <param name="sender"></param> 
		/// <param name="e"></param> 
		/// <remarks></remarks> 
		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) 
		{ 
			// Back on primary thread, can access ui controls 
			wpfProgressBarAndText.Visibility = Visibility.Collapsed;

			if (e.Cancelled)
			{
				this.wpfCount.Text = "Process Cancelled.";
			}
			else
			{
				this.wpfCount.Text = "Processing completed. " + (string)e.Result + " rows processed.";
			}
		    
			this.myStoryboard.Stop(this.lastStackPanel); 
		    
			this.wpfAsynchronousStart.IsEnabled = true; 
		    
			this.wpfAsynchronousCancel.IsEnabled = false; 
		} 

		/// <summary> 
		/// Handles click event for cancel button. 
		/// </summary> 
		/// <param name="sender"></param> 
		/// <param name="e"></param> 
		/// <remarks></remarks> 
		private void WPFAsynchronousCancel_Click(object sender, System.Windows.RoutedEventArgs e) 
		{ 
			// Cancel the asynchronous operation. 
			this.backgroundWorker.CancelAsync(); 
		    
			// Enable the Start button. 
			this.wpfAsynchronousStart.IsEnabled = true; 
		    
			// Disable the Cancel button. 
			this.wpfAsynchronousCancel.IsEnabled = false; 
		} 

		/// <summary> 
		/// Used to simulate a long running function such as database call 
		/// or the iteration of many rows. 
		/// </summary> 
		/// <returns></returns> 
		/// <remarks></remarks> 
		private string SomeLongRunningMethodWPF() 
		{ 
			int iteration = (int)1000000000 / 100; 
			double cnt = 0; 
		    
			for (int i = 0; i <= 1000000000; i++) 
			{ 
		        
				// don't continue if cancel button clicked 
				if (this.backgroundWorker.CancellationPending)
				{
					return "";
				} 
		        
				cnt = cnt + 1; 
		        
				// report progress of loop 
				if ((i % iteration == 0) & (backgroundWorker != null) && backgroundWorker.WorkerReportsProgress) 
				{ 
					backgroundWorker.ReportProgress(i / iteration); 
				} 
			} 
			return cnt.ToString(); 
		} 

		#endregion
	}	
}
