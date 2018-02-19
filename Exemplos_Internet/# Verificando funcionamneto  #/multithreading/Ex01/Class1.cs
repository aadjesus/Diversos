using System;
using System.Threading;

namespace test
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
			
			Class1 c1 = new Class1();


			Console.ReadLine();

			c1.StopThreads = true;
		}

		private bool _stopThreads = false;

		public bool StopThreads
		{
			set
			{
				_stopThreads  = value;
			}
		}


		// shared memory variable between the two threads
		private string _threadOutput = ""; 

		/// <summary>
		/// Thread 1, Displays that we are in thread 1
		/// </summary>
		void DisplayThread1()
		{
			while (_stopThreads == false)
			{
				lock (this)
				{
					Console.WriteLine("Display Thread 1");
					_threadOutput = "Hello Thread1";
					Thread.Sleep(1000);  // simulate a lot of processing

					// tell the user what thread we are in thread #1
					Console.WriteLine("Thread 1 Output --> {0}", _threadOutput);
				}
			}

		}

		/// <summary>
		/// Thread 2, Displays that we are in thread 2
		/// </summary>
		void DisplayThread2()
		{
			while (_stopThreads == false)
			{
				lock (this)
				{
					Console.WriteLine("Display Thread 2");
					_threadOutput = "Hello Thread2";
					Thread.Sleep(1000);  // simulate a lot of processing

					// tell the user we are in thread #2
					Console.WriteLine("Thread 2 Output --> {0}", _threadOutput);
				}
			}
		}

		AutoResetEvent _blockThread1 = new AutoResetEvent(false);
		AutoResetEvent _blockThread2 = new AutoResetEvent(true);

		void DisplayThread_1()
		{
			while (_stopThreads == false)
			{
				   // block thread 1  while the other is executing
				    _blockThread1.WaitOne();

				    // Set was called to free the block on thread 1, continue executing the code
					Console.WriteLine("Display Thread 1");
					_threadOutput = "Hello Thread 1";
					Thread.Sleep(1000);  // simulate a lot of processing

					// tell the user what thread we are in thread #1
					Console.WriteLine("Thread 1 Output --> {0}", _threadOutput);
				   
				    // finished executing the code in thread 1, so unblock thread 2
					_blockThread2.Set();
			}

		}

		/// <summary>
		/// Thread 2, Displays that we are in thread 2
		/// </summary>
		void DisplayThread_2()
		{
			while (_stopThreads == false)
			{
				// block thread 2  while the other is executing
					_blockThread2.WaitOne();

				// Set was called to free the block on thread 2, continue executing the code
					Console.WriteLine("Display Thread 2");
					_threadOutput = "Hello Thread 2";
					Thread.Sleep(1000);  // simulate a lot of processing

					// tell the user we are in thread #2
					Console.WriteLine("Thread 2 Output --> {0}", _threadOutput);

				// finished executing the code in thread 2, so unblock thread 1
				    _blockThread1.Set();
			}
		}


		Class1()
		{
			// set up and start threads;
//			Thread thread1 = new Thread(new ThreadStart(DisplayThread1));
//			Thread thread2 = new Thread(new ThreadStart(DisplayThread2));

			Thread thread1 = new Thread(new ThreadStart(DisplayThread_1));
			Thread thread2 = new Thread(new ThreadStart(DisplayThread_2));

			// start them
			thread1.Start();
			thread2.Start();
		}


	}
}
