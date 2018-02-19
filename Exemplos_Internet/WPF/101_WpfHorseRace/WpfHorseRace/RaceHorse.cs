using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace WpfHorseRace
{
	/// <summary>
	/// Represents a horse in a race.
	/// </summary>
	public class RaceHorse : INotifyPropertyChanged
	{
		#region Data

		// Static fields
		readonly static Random random;
		static RaceHorse raceWinner = null;

		// Instance fields
		readonly DispatcherTimer timer = new DispatcherTimer();
		readonly string name;
		int percentComplete;

		#endregion // Data

		#region Constructors

		static RaceHorse()
		{
			RaceHorse.random = new Random( DateTime.Now.Millisecond );
		}

		public RaceHorse( string name )
		{
			this.name = name;
			this.percentComplete = 0;
					
			this.timer.Tick += this.timer_Tick;			
		}

		#endregion // Constructors

		#region Public Properties

		public bool IsFinished
		{
			get { return this.PercentComplete >= 100; }
		}

		public bool IsWinner
		{
			get { return RaceHorse.raceWinner == this; }
		}

		public string Name
		{
			get { return this.name; }
		}

		public int PercentComplete
		{
			get { return this.percentComplete; }
			private set
			{
				if( this.percentComplete == value )
					return;

				if( value < 0 || value > 100 )
					throw new ArgumentOutOfRangeException( "PercentComplete" );

				bool wasFinished = this.IsFinished;

				this.percentComplete = value;

				this.RaisePropertyChanged( "PercentComplete" );

				if( wasFinished != this.IsFinished )
				{		
					if( this.IsFinished && RaceHorse.raceWinner == null )
					{
						RaceHorse.raceWinner = this;
						this.RaisePropertyChanged( "IsWinner" );
					}

					this.RaisePropertyChanged( "IsFinished" );	
				}

				// In case this horse was the previous winner and a new race has begun,
				// notify the world that the IsWinner property has changed on this horse.
				if( wasFinished && value == 0 )
					this.RaisePropertyChanged( "IsWinner" );
			}
		}

		#endregion // Public Properties

		#region Public Methods

		public void StartNewRace()
		{
			// When a race begins, remove a reference to the previous winner.
			if( RaceHorse.raceWinner != null )
				RaceHorse.raceWinner = null;

			// Put the horse back at the start of the track.
			this.PercentComplete = 0;

			// Give the horse a random "speed" to run at.
			this.timer.Interval = TimeSpan.FromMilliseconds( RaceHorse.random.Next( 20, 100 ) );

			// Start the DispatcherTimer, which ticks when the horse should "move."
			if( ! this.timer.IsEnabled )
				this.timer.Start();
		}

		#endregion // Public Methods		

		#region timer_Tick

		void timer_Tick( object sender, EventArgs e )
		{
			if( !this.IsFinished )
				++this.PercentComplete;

			if( this.IsFinished )
				this.timer.Stop();
		}

		#endregion // timer_Tick

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged( string propertyName )
		{
			if( this.PropertyChanged != null )
				this.PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
		}

		#endregion		
	}
}