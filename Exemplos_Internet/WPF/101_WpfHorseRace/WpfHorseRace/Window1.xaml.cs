using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfHorseRace
{
	public partial class Window1 : System.Windows.Window
	{
		public Window1()
		{
			InitializeComponent();
					
			this.raceTrack.ItemsSource = CreateRaceHorses();

			this.Loaded += delegate { this.StartRace(); };
			this.lnkStartNewRace.Click += delegate { this.StartRace(); };
		}

		void StartRace()
		{		
			foreach( RaceHorse raceHorse in this.raceTrack.Items )
				raceHorse.StartNewRace();
		}

		static List<RaceHorse> CreateRaceHorses()
		{
			List<RaceHorse> raceHorses = new List<RaceHorse>(6);

			raceHorses.Add( new RaceHorse( "Lucky Bell" ) );
			raceHorses.Add( new RaceHorse( "Sweet Fate" ) );
			raceHorses.Add( new RaceHorse( "Mr. Kentucky" ) );
			raceHorses.Add( new RaceHorse( "Fresh Spice" ) );
			raceHorses.Add( new RaceHorse( "Bluegrass" ) );
			raceHorses.Add( new RaceHorse( "Kit Madison" ) );

			return raceHorses;
		}
	}
}