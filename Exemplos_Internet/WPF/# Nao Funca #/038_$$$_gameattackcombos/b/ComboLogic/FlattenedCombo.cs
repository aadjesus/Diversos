using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GG.GameAttackCombos.Logic {

	/// <summary>
	/// Represents a flattened combo.
	/// </summary>
	public class FlattenedCombo : INotifyPropertyChanged {

		// Caches any recently calculated display sequence for this flattened combo.
		private string _displaySequence;
		
		// Holds the current value of the IsCompleted property.
		private bool _isCompleted;


		#region Properties

		/// <summary>
		/// A list of the aspects this flattened combo has within the game
		/// (e.g. Xbox 360 Achievement, Playstation 3 Trophy, complete chain, etc.).
		/// </summary>
		public List<string> Aspects { get; private set; }

		/// <summary>
		/// The sequence of commands that make up this flattened combo.
		/// </summary>
		public List<Command> CommandSequence { get; private set; }

		/// <summary>
		/// The sequence of commands for display.
		/// </summary>
		public string DisplaySequence {
			get {
				if (_displaySequence == null) {
					// Build a comma-separated string of command IDs.
					StringBuilder CommandIds = new StringBuilder(CommandSequence[0].Id);
					for (int i = 1; i < CommandSequence.Count; i++) {
						CommandIds.Append(",").Append(CommandSequence[i].Id);
					}
					_displaySequence = CommandIds.ToString();
				}
				return _displaySequence;
			}
		}

		/// <summary>
		/// A flag indicating whether or not this flattened combo is completed.
		/// </summary>
		public bool IsCompleted {
			get { return _isCompleted; }
			set {
				_isCompleted = value;
				OnPropertyChanged(new PropertyChangedEventArgs("IsCompleted"));
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes an instance of FlattenedCombo.
		/// </summary>
		public FlattenedCombo() {
			Aspects = new List<string>();
		}

		/// <summary>
		/// Initializes an instance of FlattenedCombo with a sequence of commands and a list of aspects.
		/// </summary>
		public FlattenedCombo(List<Command> commandSequence, params string[] aspects)
			: this() {
			CommandSequence = commandSequence;
			if (aspects != null && aspects.Length > 0) {
				Aspects.AddRange(aspects);
			}
		}

		/// <summary>
		/// Initializes an instance of FlattenedCombo with a sequence of commands.
		/// </summary>
		public FlattenedCombo(List<Command> commandSequence)
			: this(commandSequence, null) {
		}

		#endregion

		#region INotifyPropertyChanged Members

		/// <summary>
		/// Notifies subscribers of a property change.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		/// <summary>
		/// Raises the PropertyChanged event.
		/// </summary>
		/// <param name="e">The event arguments to raise with.</param>
		protected void OnPropertyChanged(PropertyChangedEventArgs e) {
			PropertyChangedEventHandler Handler = PropertyChanged;
			if (Handler != null) {
				Handler(this, e);
			}
		}

	}


	/// <summary>
	/// A comparer of FlattenedCombos by their DisplaySequence properties.
	/// </summary>
	public class DisplaySequenceComparer : IComparer<FlattenedCombo> {

		#region IComparer<FlattenedCombo> Members

		public int Compare(FlattenedCombo x, FlattenedCombo y) {
			return x.DisplaySequence.CompareTo(y.DisplaySequence);
		}

		#endregion

	}

}