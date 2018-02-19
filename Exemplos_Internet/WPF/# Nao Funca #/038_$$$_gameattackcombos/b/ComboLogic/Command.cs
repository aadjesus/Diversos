using System.ComponentModel;

namespace GG.GameAttackCombos.Logic {

	/// <summary>
	/// Represents a command used to form an attack combo.
	/// </summary>
	public class Command : INotifyPropertyChanged {

		// Property backing fields.
		private Button _mappedButton;


		#region Properties

		/// <summary>
		/// The ID for this command, used to uniquely identify it.
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// The name of this command.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// The button this command is mapped to currently.
		/// </summary>
		public Button MappedButton {
			get { return _mappedButton; }
			set {
				if (_mappedButton != value) {
					_mappedButton = value;
					NotifyPropertyChanged("MappedButton");
				}
			}
		}

		#endregion


		/// <summary>
		/// Initializes an instance of Command.
		/// </summary>
		public Command() { }

		/// <summary>
		/// Initializes an instance of Command.
		/// </summary>
		/// <param name="id">The ID of this command.</param>
		/// <param name="name">The name of this command.</param>
		public Command(string id, string name) 
			: this() {
			Id = id;
			Name = name;
		}

		/// <summary>
		/// Overriden to include the command's name.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("Command: {0}", Name);
		}


		/// <summary>
		/// Raises the PropertyChanged event for the specified property.
		/// </summary>
		/// <param name="propertyName">The name of the property that changed.</param>
		protected virtual void NotifyPropertyChanged(string propertyName) {
			PropertyChangedEventHandler Handler = PropertyChanged;
			if (Handler != null) {
				Handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		
	}

}