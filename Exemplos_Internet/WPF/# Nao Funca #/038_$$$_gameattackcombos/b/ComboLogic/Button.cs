namespace GG.GameAttackCombos.Logic {
	
	/// <summary>
	/// Represents a button for a platform.
	/// </summary>
	public class Button {

		#region Properties

		/// <summary>
		/// The ID of this button; used to identify it.
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// The platform for this button (e.g. PS3, Xbox360, etc).
		/// </summary>
		public string Platform { get; private set; }

		/// <summary>
		/// The resource key for an icon that represents this button.
		/// </summary>
		public string IconKey { get; private set; }

		#endregion


		/// <summary>
		/// Initializes an instance of Button.
		/// </summary>
		public Button() { }

		/// <summary>
		/// Initializes an instance of Button.
		/// </summary>
		/// <param name="id">The ID of this button.</param>
		/// <param name="platform">The platform for this button.</param>
		public Button(string id, string platform) 
			: this() {
			Id = id;
			Platform = platform;
		}

		/// <summary>
		/// Initializes an instance of Button.
		/// </summary>
		/// <param name="id">The ID of this button.</param>
		/// <param name="platform">The platform for this button.</param>
		/// <param name="iconKey">The resource key of an icon for this button.</param>
		public Button(string id, string platform, string iconKey) 
			: this(id, platform) {
			IconKey = iconKey;
		}

		/// <summary>
		/// Overriden to include the button's name.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("Button: {0}-{1}", Platform, Id);
		}

	}

}